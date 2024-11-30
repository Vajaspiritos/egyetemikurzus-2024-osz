using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AI_Practice.Functions.LossFunctions
{
    public static partial class LossFunctions
    {
        public static float[] MAE(float[] Preds, float[] Reals)
        {
            float[] res = new float[Preds.Length];
            for (int k = 0; k < res.Length; k++)
            {
                res[k] = Preds[k] - Reals[k];
            }
            return [res.Sum(x => float.Abs(x)) / res.Length];
             
        }

        public static float[] D_MAE(float[] Preds, float[] Reals)
        {
            float[] res = new float[Preds.Length];

            for (int i = 0; i < Preds.Length; i++)
            {
                if (Preds[i] < Reals[i]) res[i] = -1f / res.Length;
                if (Preds[i] > Reals[i]) res[i] = 1f / res.Length;
                if (Preds[i] == Reals[i]) res[i] = 0f;
            }
            return res;
        }
    }
}
