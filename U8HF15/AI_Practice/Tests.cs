using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Practice
{
    public static class Tests
    {

        public static void testN_plus_N() {

            Neuron N1 = new Neuron(2, true);
            Neuron N2 = new Neuron(2, true);
            Neuron N3 = new Neuron(2, true);

            if (N1 != N2) throw new Exception("N1 != N2");
            if ((N1 + N2) != N3) throw new Exception("N1+N2 =N3");
            N1.Value = 1;
            N3.Value = 1;
            if (N1 + N2 != N3) throw new Exception("N1+N2 =N3  with N1 value 1");

            N2.Bias = -30;
            N3.Bias = -30;
            if (N1 + N2 != N3) throw new Exception("N1+N2 =N3 with also bias -30");
            N1.Bias= 0;
            N1[0] = -5;
            N3[0] = -5;
            N2[1] = 4.324f;
            N3[1] = 4.324f;


            if ((N1 + N2) != N3) throw new Exception("N1+N2 =N3 with weight changes N1:"+N1.ToString()+"\n N3:"+N3.ToString());

        }

        public static void testN_times_LR() {
            
            Neuron neuron = new Neuron(2, true);
            neuron.Value = 1;
            neuron.Bias = -0.5f;
            neuron[0] = 3;
            neuron[1] = -0;
            
            float learningrate = 0.5f;
           
            Neuron neuron2 = new Neuron(2, true);
            neuron2.Value = 1;
            neuron2.Bias = -0.25f;
            neuron2[0] = 1.5f;
            neuron2[1] = -0;

           
            
            if ((neuron * learningrate) != neuron2) throw new Exception("test Neuron * learingrate failed");

        }

        public static void testForward() {

            Network Net = new Network(0.1f,Functions.NO,Functions.NO,null);
            Net.Add(2, Functions.ReLu, Functions.D_ReLu);
            Net.Add(2, Functions.ReLu, Functions.D_ReLu);
            Net.Add(1, Functions.ReLu, Functions.D_ReLu);
            Net.Build();
            //Net.Print();
            Net.Layers[0][0].Value = 1;
            Net.Layers[0][0].Bias = 1;
            Net.Layers[0][0][0] = 1;
            Net.Layers[0][0][1] = 1;
            Net.Layers[0][1].Value = 1;
            Net.Layers[0][1].Bias = 1;
            Net.Layers[0][1][0] = 1;
            Net.Layers[0][1][1] = 1;
            Net.Layers[1][0].Value = 1;
            Net.Layers[1][0].Bias = 1;
            Net.Layers[1][0][0] = 1;
            Net.Layers[1][1].Value = 1;
            Net.Layers[1][1].Bias = 1;
            Net.Layers[1][1][0] = 1;
            Net.Layers[2][0].Value = 1;
            Net.Layers[2][0].Bias = 1;

            Net.Forward([1f,1f]);
            if (Net.Layers[Net.Layers.Length - 1].Fetch()[0] != 7) throw new Exception("Forward test failed");




        }

        public static void testBackward() {

            List<Data> dataset = new List<Data>() { 
            new Data([1,0],[1])
            };
        
            Network test = new Network(0.1f,Functions.NO,Functions.NO,dataset);
            test.Add(2, Functions.ReLu, Functions.D_ReLu);
            test.Add(2, Functions.ReLu, Functions.D_ReLu);
            test.Add(1, Functions.ReLu, Functions.D_ReLu);
            test.Build();

            test.Layers[0][0].Bias = 0.3f;
            test.Layers[0][1].Bias = -0.2f;
            test.Layers[1][0].Bias = 0.8f;
            test.Layers[1][1].Bias = -0.7f;
            test.Layers[2][0].Bias = 0.5f;

            test.Layers[0][0][0] = -0.2f;
            test.Layers[0][0][1] = 0.5f;
            test.Layers[0][1][0] = -0.7f;
            test.Layers[0][1][1] = 0.3f;
            test.Layers[1][0][0] = -0.2f;
            test.Layers[1][1][0] = 0.1f;

            test.Train(1, 1, true);
            test.Print();

        }

    }
}
