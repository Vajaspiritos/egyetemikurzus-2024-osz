using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Practice.CommandFiles
{
    public class Error : CommandInterface
    {
        public string Name => "error";

        public string Description => "kiiratja a Loss értékeket a képernyőre.";

        public string LongDescription => "Kiiratja az összes eddigi feljegyzett Loss értéket a képernyőre, Sok training Epoch után ez eltarthat egy kis ideig. ha megadunk valamit 2. paraméterben, akkor a Model_ERROR.txt fájlba kis kiírja ezeket";

        public void Run(object[]? args)
        {
            
            bool save = false;
           
            Network ai = Commands.AI;
            if (args.Length > 0) save = true;
            
            StreamWriter sw = new StreamWriter("Model_ERROR.txt");
            if (ai.LOG.Count == 0) { 
                Console.WriteLine("A Log üres.");
                if (save) sw.WriteLine("A Log üres.");
            }
            for (int i = 0; i < ai.LOG.Count; i++)
            {
                Console.WriteLine((i + 1) + ".   Menet eredménye:");
                if (save) sw.WriteLine((i + 1) + ".   Menet eredménye:");
                for (int j = 0; j < ai.LOG[i].Length; j++)
                {
                    Console.WriteLine("      " + (j + 1) + ". " + ai.LOG[i][j]);
                    if (save) sw.WriteLine("      " + (j + 1) + ". " + ai.LOG[i][j]);
                }

            }
            sw.Close();
            Console.ReadKey();
        }
    }
}
