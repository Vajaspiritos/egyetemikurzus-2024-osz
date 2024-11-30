using AI_Practice.Functions;
using AI_Practice.Functions.ActivationFunctions;
using AI_Practice.Functions.LossFunctions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AI_Practice
{
    public class Network
    {
        public readonly float LearningRate;
        private bool Network_is_built_flag;
        public Func<float[], float[], float[]> LossFunction;
        public Func<float[], float[], float[]> DLossFunction;
        public List<float[]> LOG = new List<float[]>();
        private List<Plan> Network_Plan = new List<Plan>();
        public readonly List<Data> Dataset;
        public Layer[] Layers;
        public Layer[] Deltas;
        public bool Monitoring = false;
        public float MonitorSize = 100;

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

            Console.WriteLine(GetModell());
           


        }

        public string GetModell(bool with_derivative = true) {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Layers.Length; i++)
            {
                
                sb.AppendLine((i + 1) + ". Layer:");
                for (int j = 0; j < Layers[i].NeuronCount(); j++)
                {

                    sb.AppendLine("     " + (j + 1) + ". Neuron:");
                    sb.AppendLine("         Value:" + Layers[i][j].Value);
                    sb.AppendLine("         Bias:" + Layers[i][j].Bias);
                    sb.AppendLine("         Weights:");
                    for (int k = 0; k < Layers[i][j].WeightCount(); k++)
                    {

                        sb.AppendLine("               " + (k + 1) + ".Weight: " + Layers[i][j][k]);

                    }
                }
            }
            if (with_derivative)
            {
                sb.AppendLine("Delták:");
                for (int i = 0; i < Deltas.Length; i++)
                {
                    sb.AppendLine((i + 1) + ". Layer:");
                    for (int j = 0; j < Deltas[i].NeuronCount(); j++)
                    {

                        sb.AppendLine("     " + (j + 1) + ". Neuron:");
                        sb.AppendLine("         Value:" + Deltas[i][j].Value);
                        sb.AppendLine("         Bias:" + Deltas[i][j].Bias);
                        sb.AppendLine("         Weights:");
                        for (int k = 0; k < Deltas[i][j].WeightCount(); k++)
                        {

                            sb.AppendLine("               " + (k + 1) + ".Weight: " + Deltas[i][j][k]);

                        }
                    }
                }
            }
            return sb.ToString();
        }

        public List<float[]> Train(int epoch, int batch_size = 1,bool no_clear = false) {
            List<float[]> Loss = new List<float[]>();
            for (int i = 0; i < epoch; i++) {
                float max = 0f;
                for (int j = 0; j < Dataset.Count; j++) {

                    Forward((float[])Dataset[j].Inputs);
                    float[] res = Layers[Layers.Length - 1].Fetch();

                    if (max < LossFunction(res, (float[])Dataset[j].Outputs).Max()) max = LossFunction(res, (float[])Dataset[j].Outputs).Max();
                   
                    res = DLossFunction(res, (float[])Dataset[j].Outputs);
                    Backward(res);
                    
                    if ((j + 1) % batch_size == 0)
                    {
                        ApplyDeltas(no_clear);
                       
                    }

                    


                }
                Loss.Add([max]);
                if (this.Monitoring) Log_for_Monitor();

            }
            return Loss;
        }

        public void Log_for_Monitor()
        {
            try
            {
                StreamWriter sw = new StreamWriter("Log_Monitor.AIPractice", true);

                for (int y = 0; y < MonitorSize; y++)
                {

                    for (int x = 0; x < MonitorSize; x++)
                    {
                        float step = 1f / (float)MonitorSize;
                        float v1 = x * step;
                        float v2 = 1f - (y * step);
                        float res = Test([v1, v2])[0];
                        //feltételezzük a szánt output [-1,1] közé esik
                        int value = Math.Clamp((int)(res * 255), -255, 255);
                        sw.Write(value);
                        if (x != MonitorSize) sw.Write(",");

                    }
                    if (y != MonitorSize) sw.Write('\n');

                }
                sw.WriteLine(";");
                sw.Close();
            }
            catch (Exception e)
            {
                throw new Exception("Failed to monitor AI because: " + e.Message);
            }

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
