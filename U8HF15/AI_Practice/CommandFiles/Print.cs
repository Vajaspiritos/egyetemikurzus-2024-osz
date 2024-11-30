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
        public string LongDescription => "Kiiratja: \n  -a jelenlegi modelt layerenként\n   -a jelnlegi model deltáit\nA delták egy helyes trainig kör után le vannak nullázva, így a parancs diagnosztikai célokat szolgál. Hiba esetén a delták nem nullázódnak";

        public void Run(object[]? args)
        {

            Network ai = Commands.AI;
            ai.Print();
            Console.ReadKey();

        }
    }
}
