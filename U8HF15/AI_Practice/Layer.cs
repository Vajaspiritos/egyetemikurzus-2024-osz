using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Practice
{
    public class Layer
    {

        public readonly Func<float,float> Activation_Function;
        private readonly Neuron[] Neurons;
        public readonly Layer Before;
        
        public Neuron this[int i]
        {
            get => Neurons[i]; 
            set => Neurons[i] = value;
        }
        public Layer(int Num_of_Neurons, int Num_of_Weights, Func<float, float> activation_Function,Layer layerBefore,bool initialize_with_zero = false)
        {
            Activation_Function = activation_Function;
            Neurons = new Neuron[Num_of_Neurons];
            for (int i = 0; i < Num_of_Neurons; i++)
            {
                Neurons[i] = new Neuron(Num_of_Weights,initialize_with_zero);
            }
            this.Before = layerBefore;

        }

        public int NeuronCount() {  return Neurons.Length; }


        public void Feed(float[] input)
        {
            if (this.Neurons.Length != input.Length) throw new Exception("Invalid input data size");
            for (int i = 0; i < input.Length; i++)
            {
                this[i].Value = input[i];

            }
        
        }

        public float[] Fetch()
        {
            float[] res = new float[this.Neurons.Length];
            for (int i = 0; i < res.Length; i++)
            {
                res[i] = this[i].Value;
            }
            return res;
        }
      public static Layer operator *(Layer L1, Layer L2)
        { // a forward propagation-höz
            for (int i = 0; i < L2.NeuronCount(); i++)
            {
                float sum = 0;
                for (int j = 0; j < L1.NeuronCount(); j++) {

                    sum += L1[j].Value * L1[j][i];
                
                }
                L2[i].PreActivation_Value = sum + L2[i].Bias;
                L2[i].Value = L2.Activation_Function(L2[i].PreActivation_Value);
            }

            return L2;
        }

        public static Layer operator *(Layer L, float learning_rate) {

            for (int i = 0; i < L.NeuronCount(); i++)
            {
                L[i] *= learning_rate;


            
            }

            return L;
        
        }

        public static Layer operator +(Layer L1, Layer L2) {

            for (int i = 0; i < L1.NeuronCount(); i++)
            {
                L1[i] += L2[i];
            
            }

            return L1;
        }

        public static void ComputeOutputDelta(Layer L, Layer LD, float[] Preds) {
            //Preds are the calulated loss derivatives
            for (int i = 0; i < L.NeuronCount(); i++)
            {
                LD[i].Value = Preds[i]* LD.Activation_Function(L[i].PreActivation_Value);
                LD[i].Bias -= LD[i].Value;     
            }
        
        }
        public static Layer operator /(Layer L1, Layer L2) {
            // LDi = Li / Ldi+1

            for (int i = 0; i < L1.NeuronCount(); i++) {

                float sum = 0;
                for (int j = 0; j < L1[i].WeightCount(); j++) {

                    sum += L1[i][j] * L2[j].Value;
                
                }
                float derivative = sum * L2.Before.Activation_Function(L1[i].PreActivation_Value);
                L2.Before[i].Value = derivative;
                for (int j = 0; j < L1[i].WeightCount(); j++) {
                    L2.Before[i][j] -= L1[i].Value * L2[j].Value;
                 
                }
                L2.Before[i].Bias -= derivative;
            
            }


            return L2.Before;
        }

        public void clear(bool full = false)
        {
            for(int i=0; i<this.NeuronCount(); i++)
            {

                this[i].clear(full);

            }

        }
    }
}
