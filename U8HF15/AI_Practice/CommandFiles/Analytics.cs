using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Practice.CommandFiles
{
    public class Analytics : CommandInterface
    {
        public string Name => "analytics";

        public string Description => "információt add vissza a dataset-ről";

        public string LongDescription => "Analízálja a dataset-et és információkat közöl róla";

        public void Run(object[]? args)
        {
            Network ai = Commands.AI;

            List<float[]> outputs = new();
            for (int j= 0,i=0; j < ai.Dataset[0].Outputs.Length; j++){

                float[] output = ai.Dataset.SelectMany(f => f.Outputs).Where(f => (i++)%ai.Dataset[0].Outputs.Length == j).ToArray();
                outputs.Add(output);
            }

            var maxes = outputs.Select(f=> f.Max()).ToList();
            var mins = outputs.Select(f => f.Min()).ToList();
            var Averages = outputs.Select(f => f.Average()).ToList();

            Console.WriteLine("Maximumok:"+String.Join("|",maxes));
            Console.WriteLine("Minimumok:"+String.Join("|",mins));
            Console.WriteLine("Átlagok:"+String.Join("|",Averages));

            var outputgroups = ai.Dataset.GroupBy(f => f.Outputs[0]).OrderBy(g=>g.Key).Reverse().ToList();
            Console.WriteLine("Dataset");
            foreach (var group in outputgroups) {
                foreach (var data in group) {
                    Console.WriteLine("Inputs: "+String.Join("|", data.Inputs)+"      Outputs: "+ String.Join("|", data.Outputs));
                }
            }
            Console.ReadKey();
        }
    }
}
