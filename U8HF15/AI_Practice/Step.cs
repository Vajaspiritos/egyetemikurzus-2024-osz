using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Practice
{
    public struct Step
    {
        public float[] states;
        public float[] actions;
        public float[] logprobs;
        public float reward;
        public float critic_estimation;
        //Ha akarnék flag-eket tenni, akkor itt kéne megtenni elsősorban.
        
         

        public Step(float[] states) { 
        this.states = states;
        
        }



    }
}
