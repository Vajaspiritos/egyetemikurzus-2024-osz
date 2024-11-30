using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AI_Practice
{
    
    public class Neuron
    {
        private float _value;
        private float _bias;
        private float _preactivation_value;
        private readonly float[] _weights;
        public float Value { get=> _value; set => _value=validate(value); }
        public float Bias { get => _bias; set => _bias=validate(value); }
        public float PreActivation_Value { get => _preactivation_value; set => _preactivation_value = validate(value); }

        public float this[int i] { 
        
            get => _weights[i];
            set => _weights[i] = validate(value);
        }

        public int WeightCount() { 
        return this._weights.Length;
        }

        public static Neuron operator +(Neuron first, Neuron second)
        {
            if(first is null || second is null) throw new Exception("Neuron összeadásnál valamelyik null");
            if (first.WeightCount() != second.WeightCount()) throw new Exception("Cant add Neurons together, because they have different weightlength");

            first.Value += second.Value;
            first.Bias += second.Bias;
            for (int i = 0; i < first.WeightCount(); i++)
            {
                first[i] += second[i];
            }

            return first;
        }

        public static Neuron operator *(Neuron N, float learnig_rate) {
            if (N is null||learnig_rate<=0) throw new Exception("Can't multiply Neuron with learning rate");

            
            N.Bias = N.Bias*learnig_rate;
            for (int i = 0; i < N.WeightCount(); i++)
            {
                
                N[i] = N[i]*learnig_rate;
                
            }

            return N;

        }

        private float validate(float value) {

            if (float.IsInfinity(value)) throw new NotFiniteNumberException("Tries to set as Infinite");
            if (float.IsNaN(value)) throw new Exception("Tried to set as NaN");
            if (float.Abs(value) < 1 / 10_000_000) throw new Exception("Values are vanishing");
            if (float.Abs(value) > 10_000_000) throw new Exception("Values are exploading");
            return value;
        
        }


        public Neuron(int Num_of_weights, bool initialize_with_zero = false)
        {
            try
            {
                if (Num_of_weights < 0) throw new Exception("Invaliad weight count given");
                
                this.Bias = initialize_with_zero ? 0 : (float)Random.Shared.NextDouble()*2-1;
                this._weights = new float[Num_of_weights];
                for (int i = 0; i < Num_of_weights; i++)
                {
                    this[i] = (initialize_with_zero) ? 0 : (float)Random.Shared.NextDouble()*2-1;

                }
            }
            catch (Exception e) {
                throw new Exception("Error when initalizing a Neuron: "+e.Message);
            }

        }

        public void clear(bool full = false)
        {
            try
            {
                this.Value = 0;
                this.PreActivation_Value = 0;
                if (full)
                {
                    this.Bias = 0;
                    for (int i = 0; i < this._weights.Length; i++)
                    {
                        this[i] = 0;
                    }
                }
            }catch (Exception e) { 
                throw new Exception("Error when clearing Neuron"+e.Message); 
            }
        }













        

        // Ha szakmai gyakornokot keresnek, nekem jó lenne eggyet már találnom....
        // AD over
    }
}



