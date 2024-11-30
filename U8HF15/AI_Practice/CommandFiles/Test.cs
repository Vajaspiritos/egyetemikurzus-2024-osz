using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Practice.CommandFiles
{
    public class Test : CommandInterface
    {
        public string Name => "test";

        public string Description => "Model tesztelésére alkalmas";

        public string LongDescription => "A model betanítása után tudjuk használni saját input megadására.\n 'Test 0 1 0.5' - egy 3 inputos modellen alkalmazandó, az eredményt, futtatás után kiírja a képernyőre";

        public void Run(object[]? args)
        {
            if (args.Length ==0) throw new ArgumentException("Nem elegendő a paraméterek száma");
            Network ai = Commands.AI;
            string[] x = (args as string[]);
            float[] res = ai.Test(x.Select(float.Parse).ToArray());
            Console.WriteLine("Eredmeny:");
            foreach (float f in res) Console.WriteLine(f);
            Console.ReadKey();
        }
    }
}
