
using AI_Practice;
using AI_Practice.Functions.ActivationFunctions;
using AI_Practice.Functions.LossFunctions;
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

        
        
        


        Network AI = new Network(learning_rate, LossFunctions.MSE, LossFunctions.D_MSE, Dataset);
        AI.Add(2,ActivationFunctions.Tanh, ActivationFunctions.D_Tanh);
        AI.Add(4, ActivationFunctions.Tanh, ActivationFunctions.D_Tanh);
        AI.Add(1, ActivationFunctions.Sigmoid, ActivationFunctions.D_Sigmoid);
        AI.Build();
        Console.WriteLine("A model sikeresen elkészült.");
        



        Commands.Listen(AI);

    }
}