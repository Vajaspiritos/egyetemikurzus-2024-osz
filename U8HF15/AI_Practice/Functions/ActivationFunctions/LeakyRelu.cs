using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Practice.Functions.ActivationFunctions
{
    public static partial class ActivationFunctions
    {
        public static float Alpha = 0.01f;
        public static float LeadkyRelu(float x)
        {
            return x >= 0f ? x : Alpha * x;
        }

        public static float D_LeadkyRelu(float x)
        {
            return x >= 0f ? 1f : Alpha;
        }
    }
}
