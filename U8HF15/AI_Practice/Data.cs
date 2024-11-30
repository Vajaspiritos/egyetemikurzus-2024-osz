using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Practice
{
    internal struct Data
    {
        public float[] Inputs;
        public float[] Outputs;

        public Data(float[] inputs, float[] outputs) { 
        this.Inputs = inputs;
        this.Outputs = outputs;
        
        }
    }
}
