using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Practice.Functions.LossFunctions
{
    public static partial class LossFunctions
    {
        public static float[] MSE(float[] Preds, float[] Reals)
        {
            float sum = 0f;
            for (int i = 0; i < Reals.Length; i++)
            {
                sum += (Reals[i] - Preds[i]) * (Reals[i] - Preds[i]);
            }
            return [(float)(1f / Preds.Length) * sum];
        }

        public static float[] D_MSE(float[] Preds, float[] Reals)
        {
            float[] res = new float[Preds.Length];
            for (int k = 0; k < res.Length; k++)
            {
                res[k] = 2f / Preds.Length * (Preds[k] - Reals[k]);
            }
            return res;
        }
    }
}
