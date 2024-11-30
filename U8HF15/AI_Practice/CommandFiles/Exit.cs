using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Practice.CommandFiles
{
    internal class Exit : CommandInterface
    {
        public string Name => "exit";

        public string Description => "Kilép a programból";

        public string LongDescription => "Kilép a programból, Csak akkor használja, ha biztos ki akar lépni.";

        public void Run(object[]? args)
        {
            Environment.Exit(0);
        }
    }
}
