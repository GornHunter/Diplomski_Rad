using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomRandomNumberGenerators
{
    public class MiddleSquareWeylSequence
    {
        public uint Next(ulong x, ulong w, ulong s)
        {
            x *= x;
            x += (w += s);
            x = (x >> 32) | (x << 32);

            return (uint)x;
        }
    }
}
