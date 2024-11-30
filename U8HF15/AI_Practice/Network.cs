using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Practice
{
    internal class Network
    {
        readonly float LearningRate;
        private bool Network_is_built_flag;
        public Func<float[], float[], float[]> LossFunction;
        public Func<float[], float[], float[]> DLossFunction;
        private List<Plan> Network_Plan = new List<Plan>();
        private readonly List<Data> Dataset;
        public Layer[] Layers;
        public Layer[] Deltas;
        public bool Monitoring = false;

        public Network(float learning_rate, Func<float[], float[], float[]> lossFunction, Func<float[], float[], float[]> dLossFunction, List<Data> dataset)
        {

            LearningRate = Math.Max(0f, learning_rate);
            LossFunction = lossFunction;
            DLossFunction = dLossFunction;
            Dataset = dataset;
        }

        public void Add(int Number_of_Neurons, Func<float, float> Function, Func<float, float> DFunction) {
            if (Network_is_built_flag) throw new Exception("Network is already built");
            Network_Plan.Add(new Plan(Number_of_Neurons, Function,DFunction));
        }

        public void Build() {
            if (Network_is_built_flag) throw new Exception("Network is already built");
            Layers = new Layer[Network_Plan.Count];
            Deltas = new Layer[Network_Plan.Count];

            for (int i = 0; i < Network_Plan.Count; i++) {
                int neurons = Network_Plan[i].NeuronCount;
                int weights = (i==Network_Plan.Count-1)?0:Network_Plan[i+1].NeuronCount;
                

                Layers[i] = new Layer(neurons, weights, Network_Plan[i].Function,i==0?null:Layers[i-1]);
                Deltas[i] = new Layer(neurons, weights, Network_Plan[i].DFunction,i==0?null:Deltas[i-1],true);
            
            
            }
            this.Network_is_built_flag = true;
        }

        public void Forward(float[] input) {

            Layers[0].Feed(input);
            Layer tmp = Layers[0];
            for (int i = 0; i < Layers.Length-1; i++) {
                tmp =tmp* Layers[i + 1];
            }
        
        }

        public void Backward(float[] DError) {
            int last = Layers.Length-1;
            Layer.ComputeOutputDelta(Layers[last], Deltas[last], DError);
            Layer tmp = Deltas[last];

            for (int i = last - 1; i >= 0; i--) { 
            
            tmp = Layers[i]/tmp;
            
            }

        }

        public float[] Test(float[] input) {

            Forward(input);
            return Layers[Layers.Length - 1].Fetch();
        
        }

        public void Print() {


            for (int i = 0; i < Layers.Length; i++)
            {
                Console.WriteLine((i + 1) + ". Layer:");
                for (int j = 0; j < Layers[i].NeuronCount(); j++)
                {

                    Console.WriteLine("     " + (j + 1) + ". Neuron:");
                    Console.WriteLine("         Value:" + Layers[i][j].Value);
                    Console.WriteLine("         Bias:" + Layers[i][j].Bias);
                    Console.WriteLine("         Weights:");
                    for (int k = 0; k < Layers[i][j].WeightCount(); k++)
                    {

                        Console.WriteLine("               " + (k + 1) + ".Weight: " + Layers[i][j][k]);

                    }
                }
            }
            //return; //majd vegyem már ki mert rosszul mutatna ha benne hagynám
            for (int i = 0; i < Deltas.Length; i++)
            {
                Console.WriteLine((i + 1) + ". Layer:");
                for (int j = 0; j < Deltas[i].NeuronCount(); j++)
                {

                    Console.WriteLine("     " + (j + 1) + ". Neuron:");
                    Console.WriteLine("         Value:" + Deltas[i][j].Value);
                    Console.WriteLine("         Bias:" + Deltas[i][j].Bias);
                    Console.WriteLine("         Weights:");
                    for (int k = 0; k < Deltas[i][j].WeightCount(); k++)
                    {

                        Console.WriteLine("               " + (k + 1) + ".Weight: " + Deltas[i][j][k]);

                    }
                }
            }



        }

        public List<float[]> Train(int epoch, int batch_size = 1,bool no_clear = false) {
            List<float[]> Loss = new List<float[]>();
            for (int i = 0; i < epoch; i++) {
                float max = 0f;
                for (int j = 0; j < Dataset.Count; j++) {

                    Forward(Dataset[j].Inputs);
                    float[] res = Layers[Layers.Length - 1].Fetch();

                    if (max < LossFunction(res, Dataset[j].Outputs).Max()) max = LossFunction(res, Dataset[j].Outputs).Max();
                   
                    res = DLossFunction(res, Dataset[j].Outputs);
                    Backward(res);
                    
                    if ((j + 1) % batch_size == 0)
                    {
                        ApplyDeltas(no_clear);
                       
                    }

                    


                }
                Loss.Add([max]);
                if (this.Monitoring) Commands.Log_for_Monitor(this);

            }
            return Loss;
        }

        public void ApplyDeltas(bool no_clear=false) {

            for (int k = 0; k < Layers.Length; k++)
            {
                //Print();
                Layers[k] = Layers[k] + (Deltas[k] * this.LearningRate);
                // Print();

            }
            if (!no_clear)
            {
                for (int k = 0; k < Layers.Length; k++)
                {

                    Layers[k].clear();
                    Deltas[k].clear(true);
                }
            }


        }

    

    }
}
