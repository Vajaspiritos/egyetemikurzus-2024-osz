using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace AI_Practice.CommandFiles
{
    internal class Save : CommandInterface
    {
        public string Name => "save";

        public string Description => "Elmenti a jellenlegi modellt";

        public string LongDescription => "Elmenti a jelenlegi modellt (amiket a print parancsal is látni lehet) egy Modell.txt fájlba, paraméterben megadhato egy szöveg amit a Modellután fog írni így nem írja felül az előzőt.\n a 2. paraméternek ha 'dataset' et állítjuk, akkor a jellenlegi dataset-et menthetjük le egy json fájlba";

        public void Run(object[]? args)
        {
            
            Network ai = Commands.AI;
            string x="";
           if(args.Length>0) x= args[0] as string;
           
            
            string path = "Modell" + (x == "" ? "_" + x : "") + ".txt";
            if (x == "dataset")
            {

                path = "Dataset.json";
            }

            StreamWriter sw = new StreamWriter(path);


            if (x == "dataset")
            {
              
                sw.WriteLine(JsonSerializer.Serialize(ai.Dataset));
            }else  sw.WriteLine(ai.GetModell);

            sw.Close();
            Console.WriteLine("A mentés sikeres volt");
            Console.ReadKey();
        }
    }
}
