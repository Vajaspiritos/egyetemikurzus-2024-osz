using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Practice.CommandFiles
{
    public class Loss : CommandInterface
    {
        public string Name => "loss";

        public string Description => "vizualizálja a model teljesítményét.";

        public string LongDescription => "Megmutatja egy [0,1] intervallumon minden epoch után a loss értékét.\nMinél közelebb van ez az érték a 0-hoz annál jobban teljesít a modellünk.\namennyiben nem látni semmit a gráfon, az azt indikálja, hogy a loss >1";

        public void Run(object[]? args)
        {
           
            StreamWriter sw = new StreamWriter("Log_Loss.AIPractice");
            Network ai = Commands.AI;
            foreach (var list in ai.LOG)
            {

                sw.WriteLine(String.Join(",", list));

            }
            sw.Close();
            Process p = new Process();
            p.StartInfo.FileName = "Loss_Graph.exe";
            p.Start();

        }
    }
}
