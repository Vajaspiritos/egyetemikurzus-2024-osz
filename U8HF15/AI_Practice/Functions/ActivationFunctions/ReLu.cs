using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Practice.Functions.ActivationFunctions
{
    public static partial class ActivationFunctions
    {


        public static float ReLu(float x)
        {
            return x < 0f ? 0f : x;
        }

        public static float D_ReLu(float x)
        {
            return x < 0f ? 0f : 1f;
        }
    }
}
