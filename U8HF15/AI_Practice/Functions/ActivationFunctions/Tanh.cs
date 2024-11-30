using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Practice.Functions.ActivationFunctions
{
    public static partial class ActivationFunctions
    {
        public static float Tanh(float x)
        {
            return 2f / (1f + (float)Math.Exp(-2f * x)) - 1f;
        }

        public static float D_Tanh(float x)
        {
            return 1f - Tanh(x) * Tanh(x);
        }
    }
}
