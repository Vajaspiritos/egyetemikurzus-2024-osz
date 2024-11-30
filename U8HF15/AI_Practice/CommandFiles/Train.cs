using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Practice.CommandFiles
{
    public class Train : CommandInterface
    {
        public string Name => "train";

        public string Description => "adott számú epochon keresztül tanítja a modelt";

        public string LongDescription => "Adott számú epochon keresztül tanítja a modelt. ezt a számot paraméterben lehet megadni:\nTrain 3000\na fenti parancs 3000 epochon keresztül fogja tanítani a modellt";

        public void Run(object[]? args)
        {
            if (args.Length ==0) throw new ArgumentException("Nem elegendő a paraméterek száma");
            int x = Convert.ToInt32(args[0]);
            Network ai = Commands.AI;
            ai.LOG.AddRange(ai.Train(x));
            Console.WriteLine("Training befejezve");
            Console.ReadKey();
        }
    }
}
