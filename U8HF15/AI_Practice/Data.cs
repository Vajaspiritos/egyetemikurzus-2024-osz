using AI_Practice.CommandFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AI_Practice
{
    public record Data
    {
       
        readonly public float[] Inputs;  
        readonly public float[] Outputs;     
        public Data(float[] inputs, float[] outputs) { 
        this.Inputs = (float[])inputs.Clone();
        this.Outputs = (float[])outputs.Clone();
        
        }
    }
}
