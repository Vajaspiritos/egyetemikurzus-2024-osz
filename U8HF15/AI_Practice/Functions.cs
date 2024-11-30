using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Practice
{
    public static class Functions
    {

        public static float BoxMuller(float mu, float sigma) {

            float PI = 2f * (float)Math.PI * (float)Random.Shared.NextDouble();
            float r = (float)Math.Sqrt(-2d*Math.Log(Random.Shared.NextDouble()));
            float u = r*(float)Math.Cos(PI);
            return mu+sigma *u;
        }

        public static float ReLu(float x) {
            return (x < 0f) ? 0f : x;
        }

        public static float[] GAE(Step[] Episode, float discount_factor, float lambda = 0.95f) { 
            float[] res = new float[Episode.Length];

            int T = Episode.Length;
            float tmp = 0;

            for (int t = T-1; t >= 0; t--) {
                float next = (t == T - 1) ? 0 : Episode[t + 1].critic_estimation;
                float delta = Episode[t].reward + discount_factor * next - Episode[t].critic_estimation;

                tmp = delta + discount_factor * lambda * tmp;
                res[t] = tmp;
            }

            return res;
        
        }

        public static float[] ToLogProbs(float[] probabilities,float[] output) {
            //feltételezzük, output első fele a mu és második fele a sigma
            float[] res = new float[probabilities.Length];
            for (int i = 0; i < probabilities.Length; i++) {
                int j = i + probabilities.Length;
                res[i] = (float)(-(Math.Pow(probabilities[i] - output[i], 2) / (2d * Math.Pow(output[j], 2))) - Math.Log(output[j] * Math.Sqrt(2d * Math.PI)));
            
            }
            return res;
        }

        public static float PPO_PolicyLoss(float[] ratios, float[] Advantages, float epsilon) {
            float sum = 0f;　
            for (int i = 0; i < ratios.Length; i++) {
                sum += Math.Min(ratios[i] * Advantages[i], Math.Clamp(ratios[i],1f-epsilon,1f+epsilon) * Advantages[i]);
            }

            return  -1f / (float)ratios.Length * sum;
        
        
        }

        public static float[] D_PPO_PolicyLoss(float[] pis, float[] ratios, float advantage, float epsilon) {
            
            float[] res = new float[pis.Length];

            for (int i = 0; i < pis.Length; i++)
            {
                float clip = Math.Clamp(ratios[i], 1f - epsilon, 1f + epsilon);

                float a = (ratios[i] * advantage <= clip * advantage) ? 1f : 0f;
                float b = (clip * advantage < ratios[i] * advantage) ? 1f : 0f;
                float c = (1 - epsilon <= ratios[i] && ratios[i] <= 1 + epsilon) ? 1f : 0f;

                //float together = -1f * (a * advantage + b * advantage * c);
                //pozitív szorzés, miután a backpropagation SGD-t használ
                float together = 1f * (a * advantage + b * advantage * c);




                res[i] = together * (1f/pis[i]);
            
            }
            return res;       
            
        }

        public static float[] D_Mu_and_sigma(float[] gradients, float advantage, float[] actions, float[] mus, float[] sigmas) {
            float[] res = new float[actions.Length];
            float[] res2 = new float[actions.Length];
            for (int i = 0; i < actions.Length; i++)
            {
                res[i] = gradients[i]*advantage*  ((actions[i] - mus[i]) / (float)Math.Pow(sigmas[i], 2));
                res2[i] = gradients[i] * advantage*(-1f/sigmas[i]  + (float)Math.Pow((actions[i] - mus[i]), 2) / (float)Math.Pow(sigmas[i],3));
            
            }
            return res.Concat(res2).ToArray();
            
        }
        public static float Tanh(float x) {

            return ((2f/(1f+(float)Math.Exp(-2f*x)))-1f);
        }

        public static float D_Tanh(float x) {

            return 1f - (Tanh(x)* Tanh(x));

        }
        public static float D_ReLu(float x) { 
        return x < 0f ? 0f : 1f;
        }

        public static float Sigmoid(float x) {

            return (1f / (1f+(float)Math.Exp(-x)));
        
        }

        public static float Alpha = 0.01f;
        public static float LeakyReLu(float x)
        {
            return x >= 0f ? x : Alpha * x;
        }

     
        public static float D_LeakyReLu(float x)
        {
            return x >= 0f ? 1f : Alpha;
        }

        public static float D_Sigmoid(float x) { 
        
            return Sigmoid(x)*(1f-Sigmoid(x));
        
        }

        public static float D_Sigmoid_alt(float x) {
            return x * (1f - x);
        }

        public static float[] MSE(float[] Preds, float[] Reals) {
            float sum = 0f;
            for (int i = 0; i < Reals.Length; i++) {
                sum += (Reals[i] - Preds[i])* (Reals[i] - Preds[i]);
            }
            return [(float)(1f/(float)Preds.Length) * sum];
        
        }

        public static float[] D_MSE(float[] Preds, float[] Reals)
        {
            float[] res = new float[Preds.Length];
            for (int k = 0; k < res.Length; k++)
            {
                res[k] = (2f/(float)Preds.Length *(Preds[k] - Reals[k]));
            }
            return res;

        }
            public static float[] MAE(float[] Preds, float[] Reals) {
            float[] res = new float[Preds.Length];
            for (int k = 0; k < res.Length; k++)
            {
                res[k] = (Preds[k] - Reals[k]);
            }
            return [res.Sum(x=>float.Abs(x))/(float)res.Length];
        
        }

        public static float[] D_MAE(float[] Preds, float[] Reals) {
            float[] res = new float[Preds.Length];

            for (int i = 0; i < Preds.Length; i++)
            {
                if (Preds[i] < Reals[i]) res[i] = -1f / (float)res.Length;
                if (Preds[i] > Reals[i]) res[i] = 1f / (float)res.Length;
                if (Preds[i] == Reals[i]) res[i] = 0f;                           
            }
            return res;
        }

        public static float[] NO(float[] Preds, float[] Reals) {

            float[] res = new float[Preds.Length];
            for (int k = 0; k < res.Length; k++)
            {
                res[k] = (Reals[k] - Preds[k]);
               // Console.WriteLine(Reals[k]+" - " + Preds[k]);
            }
            return res;
        }

    }
}
