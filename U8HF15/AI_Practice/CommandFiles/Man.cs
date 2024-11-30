using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Practice.CommandFiles
{
    public class Man : CommandInterface
    {
        public string Name => "man";

        public string Description => "Hoszabb leírást ad a parancsokról";

        public string LongDescription => "Kiíratja a hoszabb és részletesebb leírását a paraméterben megadott parancsnak";

        public void Run(object[]? args)
        {
            if (args.Length == 0) throw new ArgumentException("Nem elegendő a paraméterek száma");

            if (!Commands.COMMANDS.ContainsKey(args[0] as string))
            {
                Console.WriteLine("Sajnos nincs ilyen parancs :(");
                Console.ReadKey();
                return;
            }
            else {
                Console.WriteLine(Commands.COMMANDS[args[0] as string].LongDescription);
                Console.ReadKey();
            
            }

        }
    }
}
