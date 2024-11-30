using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Practice.Functions.ActivationFunctions
{
    public static partial class ActivationFunctions
    {
        public static float Sigmoid(float x)
        {
            return 1f / (1f + (float)Math.Exp(-x));
        }

        public static float D_Sigmoid(float x)
        {
            return Sigmoid(x) * (1f - Sigmoid(x));
        }
    }
}
