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

namespace AI_Practice
{

     static class Commands
    {
       public static Dictionary<string, Action<Network, string[]>> COMMANDS = new Dictionary<string, Action<Network, string[]>>() {
            {"train",Train},
            {"test",Test},  
            {"print",Print},
            {"error",Error},
            {"exit",Exit},
            {"loss",Loss},
            {"showcase",Showcase}
        
        };
        public static List<float[]> LOG = new List<float[]>();
        private static int MonitorSize = 100;

       public static void Listen(Network ai, string[] tmp) {

            while (true)
            {

                Console.Clear();
                Console.WriteLine("A model sikeresen elkészült.");
                Console.WriteLine("Lehetöségek:");
                Console.WriteLine("     train: egy számot vár és annyi epochon keresztül trainel");
                Console.WriteLine("     test: egy inputot vár szoközökkel elválasztva és kiirja az eredmenyt");
                Console.WriteLine("     print: kiprinteli a modelt");
                Console.WriteLine("     error: megmutatja a LOG-ban összegyűjtött error-okat");
                Console.WriteLine("     exit: Exits from the application");
                Console.WriteLine("     loss: Vizualizálja a loss-t");
                Console.WriteLine("     showcase: Vizualizálja az AI tanulását, csak 2 inputtal és 1 outputtal működik");
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

                    Commands.COMMANDS[head].Invoke(ai, paramss);
                }
                catch (Exception e) { Console.WriteLine("Nem sikerült a parancsott futtatni: " + e.ToString()); Console.ReadKey(); }



            }

        }
        static void Print(Network ai, string[] tmp) {

            ai.Print();
            Console.ReadKey();
        }

        static void Showcase(Network ai, string[] tmp) {
            if (tmp.Length > 0) {
                MonitorSize = Convert.ToInt32(tmp[0]);
            }
            if (ai.Monitoring)
            {
                Process p = new Process();
                p.StartInfo.FileName = "AI_Monitor.exe";
                p.Start();
                ai.Monitoring = false;
            }
            else
            {
                File.Delete("Log_Monitor.AIPractice");
                ai.Monitoring = true;
            }
            
        }

       

        public static void Log_for_Monitor(Network ai)
        {
            
            
           
            try
            {
                StreamWriter sw = new StreamWriter("Log_Monitor.AIPractice",true);

                for (int y = 0; y < MonitorSize; y++)
                {

                    for (int x = 0; x < MonitorSize; x++)
                    {
                        float step = 1f / (float)MonitorSize;
                        float v1 = x*step;
                        float v2 = 1f-(y*step);
                        float res = ai.Test([v1,v2])[0];
                        //feltételezzük a szánt output [-1,1] közé esik
                        int value = Math.Clamp((int)(res * 255),-255,255);
                        sw.Write(value);
                        if(x != MonitorSize) sw.Write(",");

                    }
                   if(y!= MonitorSize) sw.Write('\n');

                }
                sw.WriteLine(";");
                sw.Close();
            } catch (Exception e){
                throw new Exception("Failed to monitor AI because: "+e.Message);
            }

        }

        static void Loss(Network ai, object b) {

            StreamWriter sw = new StreamWriter("Log_Loss.AIPractice");
            
            foreach (var list in LOG) {

                sw.WriteLine(String.Join(",",list));
            
            }
            sw.Close();
            Process p = new Process();
            p.StartInfo.FileName= "Loss_Graph.exe";
            p.Start();
        
        }

        static void Exit(object a, object b) {

            Environment.Exit(0);

        }
        static void Error(Network ai, string[] x) {
            if (LOG.Count == 0) Console.WriteLine("A Log üres.");
            for (int i = 0; i < LOG.Count; i++) { 
            Console.WriteLine((i+1)+".   Menet eredménye:");
                for (int j = 0; j < LOG[i].Length; j++) {
                    Console.WriteLine("      " + (j + 1) + ". " + LOG[i][j]);
                }
            
            }
            Console.ReadKey();
        
        }
         static void Train(Network ai, string[] x)
        {
              LOG.AddRange(  ai.Train(Convert.ToInt32(x[0]))  );
                Console.WriteLine("Training befejezve");
                Console.ReadKey();
            
        }
        static void Test(Network ai,  string[] x)
        {          

           float[] res= ai.Test(x.Select(float.Parse).ToArray());
            Console.WriteLine("Eredmeny:");
            foreach(float f in res) Console.WriteLine(f);
            Console.ReadKey();
        }




       

    }
    
}
