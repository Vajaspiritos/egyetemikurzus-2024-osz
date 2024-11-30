
using AI_Practice;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
internal class Program
{


    private static void Main(string[] args)
    {

        float learning_rate = 0.1f;


        /*
        List<Data> Dataset = new List<Data>();
        
       // Dataset.Add(new Data([0, 0], [0]));
       // Dataset.Add(new Data([0, 1], [1]));
       // Dataset.Add(new Data([1, 0], [1]));
       // Dataset.Add(new Data([1, 1], [0]));


        
        for (int i = 0; i < 500; i++) {
            // float num1 = (float)Math.Floor(Random.Shared.NextDouble()*100);
            // float num2 = (float)Math.Floor(Random.Shared.NextDouble()*100);
            // Console.WriteLine(num1+"|"+num2);
            float num1 = (float)i;
            Dataset.Add(new Data([num1], [(float)Math.Sqrt(num1)]));
        }
        
        


        Network AI = new Network(learning_rate, Functions.MSE, Functions.D_MSE, Dataset);
        AI.Add(1,Functions.LeakyReLu, Functions.D_LeakyReLu);
        AI.Add(4,Functions.LeakyReLu, Functions.D_LeakyReLu);
        AI.Add(1,Functions.LeakyReLu, Functions.D_LeakyReLu);
        AI.Build();
        */
        

        Network AI = new Network(learning_rate, null, null, null);
        AI.Add(2, Functions.Tanh, Functions.D_Tanh);
        AI.Add(2, Functions.Tanh, Functions.D_Tanh);
        AI.Add(2, Functions.Tanh, Functions.D_Tanh);
        AI.Add(2, Functions.Tanh, Functions.D_Tanh);
        AI.Build();

         Network critic = new Network(learning_rate, Functions.MSE, Functions.D_MSE, null);
         critic.Add(2, Functions.Tanh, Functions.D_Tanh);
         critic.Add(2, Functions.Tanh, Functions.D_Tanh);
         critic.Add(2, Functions.Tanh, Functions.D_Tanh);
         critic.Add(1, Functions.Tanh, Functions.D_Tanh);
         critic.Build();

         PPO ppo = new PPO(critic,AI,Enviroment.Balls,10,0.2f,0.99f,10);
         int zeros = 0;



         for (int i = 0; i < 3_000; i++)
         {
             ppo.train();

         }

         Enviroment.Play_Balls(ppo);
        


        //Tests.testN_plus_N();
        //Tests.testN_times_LR();
        //Tests.testForward();
        //Tests.testBackward();

        //Commands.Listen(AI,null);

    }
}