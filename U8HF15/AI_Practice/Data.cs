using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AI_Practice
{
    public struct Data
    {
        readonly private float[] _Inputs;
        readonly private float[] _Outputs;

        public IReadOnlyList<float> Inputs => _Inputs;
        public IReadOnlyList<float> Outputs => _Outputs;
        public Data(float[] inputs, float[] outputs) { 
        this._Inputs = (float[])inputs.Clone();
        this._Outputs = (float[])outputs.Clone();
        
        }
    }
}
