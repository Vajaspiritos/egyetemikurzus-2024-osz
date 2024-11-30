
using AI_Practice;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
internal class Program
{


    private static void Main(string[] args)
    {

        float learning_rate = 0.1f;


        
        List<Data> Dataset = new List<Data>();
        
        Dataset.Add(new Data([0, 0], [0]));
        Dataset.Add(new Data([0, 1], [1]));
        Dataset.Add(new Data([1, 0], [1]));
        Dataset.Add(new Data([1, 1], [0]));


        
        for (int i = 0; i < 500 && false; i++) {
            // float num1 = (float)Math.Floor(Random.Shared.NextDouble()*100);
            // float num2 = (float)Math.Floor(Random.Shared.NextDouble()*100);
            // Console.WriteLine(num1+"|"+num2);
            float num1 = (float)i;
            Dataset.Add(new Data([num1], [(float)Math.Sqrt(num1)]));
        }
        
        


        Network AI = new Network(learning_rate, Functions.MSE, Functions.D_MSE, Dataset);
        AI.Add(2,Functions.Sigmoid, Functions.D_Sigmoid);
        AI.Add(4,Functions.Sigmoid, Functions.D_Sigmoid);
        AI.Add(1,Functions.Sigmoid, Functions.D_Sigmoid);
        AI.Build();
        Console.WriteLine("A model sikeresen elkészült.");




        //Tests.testN_plus_N();
        //Tests.testN_times_LR();
        //Tests.testForward();
        //Tests.testBackward();

        Commands.Listen(AI);

    }
}