using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Practice
{
    internal class PPO
    {
        private float discount_factor;
        public int timestep_per_episode;
        public List<Step[]> Episodes = new List<Step[]>();
        public List<float> Log_Loss = new List<float>();
        private Func<PPO, Step[]> playground;
        private int episodes_to_collect;
        public Network CRITIC;
        public Network ACTOR;
        private float epsilon;


       
        

        public PPO(Network CRITIC,Network ACTOR, Func<PPO, Step[]> playground, int step_per_ep = 10,float epsilon=0.2f, float discount_factor=0.99f, int eps_to_collect = 2) { 
        this.CRITIC = CRITIC;
            this.ACTOR = ACTOR;
            this.playground = playground;
            this.timestep_per_episode = step_per_ep;
            this.epsilon = epsilon;
            this.episodes_to_collect = eps_to_collect;
            this.discount_factor = discount_factor;
        
        }

        void CollectEpisodes() {

            for (int i = 0; i < this.episodes_to_collect; ++i) {

                this.Episodes.Add(playground(this));
            
            }

        }

        public void train() {

            CollectEpisodes();

         
            

            foreach (Step[] episode in Episodes)
            {
                
                float[] advantages = Functions.GAE(episode,this.discount_factor);
                float[] rewards = episode.Select(x => x.reward).ToArray();
                this.CRITIC.Backward(this.CRITIC.DLossFunction(advantages,rewards));
                
                float max_Loss = 0f;
                for (int i = 0; i < advantages.Length; i++) {

                    ACTOR.Forward(episode[i].states);
                    float[] new_output = ACTOR.Layers[ACTOR.Layers.Length - 1].Fetch();
                    float[] new_actions = new float[new_output.Length/2];
                    for (int j = 0; j< new_output.Length / 2; j++) {

                        new_actions[j] = Functions.BoxMuller(new_output[j], new_output[new_output.Length / 2 + j]);    

                    }
                    float[] new_logs = Functions.ToLogProbs(new_actions, new_output);
                   
                    float[] ratios = new float[new_logs.Length];
                    
                    for (int j = 0; j < new_logs.Length; j++) {

                        ratios[j] = new_logs[j] / episode[i].logprobs[j] ;
                    
                    }

                    float loss = Functions.PPO_PolicyLoss(ratios,advantages,this.epsilon);
                    if (loss < max_Loss) { 
                    max_Loss = loss ;
                    }
                    float[] mus = new float[ratios.Length];
                    float[] sigmas = new float[ratios.Length];
                    for (int k = 0; k < ratios.Length*2; k++) {
                        if (k < ratios.Length)
                        {
                            mus[k] = new_output[k] ;
                        }else sigmas[k-ratios.Length] = new_output[k];
                    }


                    float[] D_losses = Functions.D_PPO_PolicyLoss(episode[i].actions, ratios, advantages[i],this.epsilon);
                    float[] D_mu_and_sigma_losses = Functions.D_Mu_and_sigma(D_losses, advantages[i], episode[i].actions,mus,sigmas);
                    ACTOR.Backward(D_mu_and_sigma_losses);
                    

                    
                
                }
                Log_Loss.Add(max_Loss);
                







            }
            CRITIC.ApplyDeltas();
            ACTOR.ApplyDeltas();


            this.Episodes.Clear();
        }

    }
}
