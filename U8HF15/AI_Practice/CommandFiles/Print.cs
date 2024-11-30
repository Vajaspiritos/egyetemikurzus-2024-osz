using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Practice.CommandFiles
{
    internal class Print : CommandInterface
    {
        public string Name => "print";

        public string Description => "Kiiratja a jellenlegi modelt";
        public string LongDescription => "Kiiratja: \n  -a jelenlegi modelt layerenként\n   -a jelnlegi model deltáit\nA delták egy helyes trainig kör után le vannak nullázva, így a parancs diagnosztikai célokat szolgál. Hiba esetén a delták nem nullázódnak \n ha paraméterben megadjuk, hogy dataset akkor a dataset-et fogja kiiratni";

        public void Run(object[]? args)
        {

            Network ai = Commands.AI;
            if (args.Length > 0 && args[0] as string == "dataset") {

                foreach (var data in ai.Dataset)
                {
                    Console.Write("Inputs:[");
                    Console.Write(String.Join(',',data.Inputs));
                    Console.Write("] Outputs:[");
                    Console.Write(String.Join(',', data.Outputs));
                    Console.Write("]\n");
                }

            } else if (args.Length == 0) {
                ai.Print();
            }else Console.WriteLine("Nem lehet ilyet kiiratni");
            Console.ReadKey();

        }
    }
}
