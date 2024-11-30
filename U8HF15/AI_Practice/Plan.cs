using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Practice
{
    internal struct Plan
    {
        public readonly int NeuronCount;
        public readonly Func<float, float> Function;
        public readonly Func<float, float> DFunction;

        public Plan(int Number_of_Neurons, Func<float, float> Function, Func<float, float> DFunction) {
            if (Number_of_Neurons < 1 || Function == null || DFunction==null) throw new Exception("The Plan has failed");

        this.Function = Function;
        this.DFunction = DFunction;
        this.NeuronCount = Number_of_Neurons;
        
        }

    }
}
