using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AI_Practice.CommandFiles
{
    public class Showcase : CommandInterface
    {
        public string Name => "showcase";

        public string Description => "felveszi vagy visszajátsza a tanulási folymatot amit vizualizál is";

        public string LongDescription => "A parancs első kiadásakor, elindítja a tanulási folymat felvételét\n a második kiadáskor betölti a visszajátszást.\n EZ A PARANCS CSAKIS 2 INPUT ÉS 1 OUTPUT NEURONOS NETWORKÖKNÉL MŰKÖDIK\n A neuronok értékei [0,1] között fog látszani, a bal alsó saroktól indulva, ahol [-1,1] között lesznek az értékek vizualizálva, piros ha negatív, zöld ha pozitív.\n az alap felbontás értéke 100, ez azt jelenti, hogy minden training epoch után a program végigmegy egy 100^2 mapon, ez rendkívül drága folyamat! A felbontás opcionálisan megadható paraméterként.\n 'Showcase 10' egy tizes felbontású monitorozást fog elkezdeni. (visszajátszáskor a felbontás állításának nincs szerepe). \n egy egyszerü modelnél sem ajánlott 1000 nél magasabbra állítani a felbontást";

        public void Run(object[]? args)
        {
            
          
            Network ai = Commands.AI;
            if (args.Length > 0) { 
                ai.MonitorSize = Convert.ToInt32(args[0]);
            }

            if (ai.Monitoring)
            {
                Process p = new Process();
                p.StartInfo.FileName = "AI_Monitor.exe";
                p.Start();
                ai.Monitoring = false;

            }
            else {
                File.Delete("Log_Monitor.AIPractice");
                ai.Monitoring = true;
            }

        }
    }
}
