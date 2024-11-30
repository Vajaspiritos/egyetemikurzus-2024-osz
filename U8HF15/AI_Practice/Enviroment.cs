using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Practice
{
    internal static class Enviroment
    {

        public static void Play_Balls(PPO agent) {

            int X = Random.Shared.Next(1, 100);
            int GOAL = Random.Shared.Next(1, 100);
            int points = 0;
            int tick = 0;
            while (true) {
                
                    tick++;
                    float[] input = [1f / (float)X, 1f / (float)GOAL];
                    float reward = 0f;
                    Step step = new Step(input);


                    agent.ACTOR.Forward(input);
                    float[] output = agent.ACTOR.Layers[agent.ACTOR.Layers.Length - 1].Fetch();
                    float action = Functions.BoxMuller(output[0], output[1]);
                    step.actions = [action];
                    step.logprobs = Functions.ToLogProbs([action], output);
                    if (action >= 0.5f) X++;
                    if (action <= -0.5f) X--;
                    if (X <= 0) X = 1;
                    if (X >= 101) X = 100;
                    if (X == GOAL)
                    {
                        reward += 0.5f;
                        GOAL = Random.Shared.Next(1, 100);
                        points++;
                    }
                    if (Math.Abs(X - GOAL) < 10) reward += 0.1f;
                    reward -= ((float)Math.Abs(X - GOAL) / 200f);
                    step.reward = reward;


                    Console.Clear();
                    for (int i = 1; i < 101; i++)
                    {

                        if (i == X) { Console.Write("X"); }
                        else
                        if (i == GOAL) { Console.Write("O"); }
                        else Console.Write("-");


                    }
                    Console.Write("\n\n");
                    Console.WriteLine("Reward: " + reward);
                    Console.WriteLine("Points: " + points);
                    Console.WriteLine("Elapsed time: " + tick);
                Thread.Sleep(100);

            }




        }
        public static Step[] Balls(PPO agent) {
            Step[] steps = new Step[agent.timestep_per_episode];
            int X = Random.Shared.Next(1, 100);
            int GOAL = Random.Shared.Next(1, 100);
            
            for (int i = 0; i < agent.timestep_per_episode; i++)
            {
                float[] input = [1f / (float)X, 1f / (float)GOAL];
                float reward = 0f;
                Step step = new Step(input);

                agent.CRITIC.Forward(input);
                step.critic_estimation = agent.CRITIC.Layers[agent.CRITIC.Layers.Length - 1].Fetch()[0];
               
                agent.ACTOR.Forward(input);
                float[] output =  agent.ACTOR.Layers[agent.ACTOR.Layers.Length - 1].Fetch();
                float action = Functions.BoxMuller(output[0], output[1]);
                step.actions = [action];
                step.logprobs = Functions.ToLogProbs([action], output);
                if (action >= 0.5f) X++; 
                if (action <= -0.5f) X--;
                if (X <= 0) X = 1;
                if (X >= 101) X = 100;
                if (X == GOAL) {
                    reward += 0.5f;
                    GOAL = Random.Shared.Next(1, 100);
                    
                }
                if (Math.Abs(X - GOAL) < 10) reward += 0.1f;
                reward -= ((float)Math.Abs(X - GOAL) / 200f);
                step.reward = reward;
                steps[i] = step;


            }
            return steps;

        }

        



    }
}
