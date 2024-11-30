using AI_Practice.CommandFiles;
using AI_Practice.FunctionFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AI_Practice
{
    public static class Functions
    {
        public static readonly Dictionary<string, FunctionInterface>   ActivationFunctions = new();
        public static readonly Dictionary<string, LossInterface>       LossFunctions       = new();

        
        
        static Functions() {
            
            var activation = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(y => typeof(FunctionInterface).IsAssignableFrom(y) && !y.IsInterface && !y.IsAbstract);

            var loss = AppDomain.CurrentDomain.GetAssemblies()
               .SelectMany(x => x.GetTypes())
               .Where(y => typeof(LossInterface).IsAssignableFrom(y) && !y.IsInterface && !y.IsAbstract);

            foreach (var type in activation)
            {
                
                if (Activator.CreateInstance(type) is FunctionInterface instance)
                {
                   
                }
            }


        }



    }
}
