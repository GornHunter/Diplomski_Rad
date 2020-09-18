using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomRandomNumberGenerators
{
    public class LinearCongruentialGenerator
    {
        public int Next(int seed, int min, int max)
        {
            seed += Guid.NewGuid().GetHashCode();
            int temp = ((1927 * seed + 393) % (max - min));

            return temp <= 0 ? -temp + min : temp + min;
        }
    }
}
