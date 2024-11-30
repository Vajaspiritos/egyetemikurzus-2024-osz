using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Practice.CommandFiles
{
    public interface CommandInterface
    {

        string Name { get; }
        string Description { get; }

        string LongDescription { get; }

        void Run(object[]? args);
        

    }
}
