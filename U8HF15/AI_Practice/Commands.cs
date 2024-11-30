using AI_Practice.CommandFiles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Windows.Input;

namespace AI_Practice
{

     static class Commands
    {


        public static Dictionary<string, CommandInterface> COMMANDS = new();
        public static Network AI;
        static Commands() {


            var commands = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(y => typeof(CommandInterface).IsAssignableFrom(y)&& !y.IsInterface && !y.IsAbstract);
            foreach (var type in commands) {

                if (Activator.CreateInstance(type) is CommandInterface command)
                {
                    COMMANDS[command.Name] = command;
                }

            }
        }
       
           

       public static void Listen(Network ai) {
            AI = ai;
            while (true)
            {

                Console.Clear();
                
                Console.WriteLine("Lehetöségek:");
                foreach (var comm in COMMANDS.Values) {
                    Console.WriteLine($"     {comm.Name}: {comm.Description}");
                }
                Console.WriteLine("\n\n\n");

                string command = Console.ReadLine();
                string[] bits = command.Split(" ");
                string head = bits[0];
                string[] paramss = bits.Skip(1).ToArray();
                if (!Commands.COMMANDS.ContainsKey(head))
                {
                    Console.WriteLine("Sajnos nincs ilyen parancs :(");
                    Console.ReadKey();
                    continue;
                }

                try
                {

                    Commands.COMMANDS[head].Run(paramss);
                }
                catch (Exception e) { Console.WriteLine("Nem sikerült a parancsott futtatni: " + e.ToString()); Console.ReadKey(); }



            }

        }
        
       

    }
    
}
