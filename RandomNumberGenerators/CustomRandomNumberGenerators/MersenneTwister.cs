using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomRandomNumberGenerators
{
    public class MersenneTwister
    {
        const int MERS_W = 32;
        const int MERS_N = 624;
        const int MERS_M = 397;
        const int MERS_R = 31;
        const int MERS_U = 11;
        const int MERS_S = 7;
        const int MERS_T = 15;
        const int MERS_L = 18;
        const uint MERS_A = 0x9908B0DF;
        const uint MERS_B = 0x9D2C5680;
        const uint MERS_C = 0xEFC60000;

        uint[] mt = new uint[MERS_N];

        uint mti;

        private MersenneTwister() { }

        public MersenneTwister(uint seed)
        {
            Initialization(seed);
        }

        public void Initialization(uint seed)
        {
            mt[0] = seed;

            for (mti = 1; mti < MERS_N; mti++)
                mt[mti] = (1812433253U * (mt[mti - 1] ^ (mt[mti - 1] >> (MERS_W - 2))) + mti);
        }

        public int Next(int min, int max)
        {
            int r = (int)((max - min + 1) * Random()) + min;

            if (r > max)
                r = max;
            if (max < min)
                return -2147483648;

            return r;
        }

        public double Random()
        {
            uint r = RandomBits();
            if (BitConverter.IsLittleEndian)
            {
                byte[] i0 = BitConverter.GetBytes((r << 20));
                byte[] i1 = BitConverter.GetBytes((r >> 12) | 0x3FF00000);
                byte[] bytes = { i0[0], i0[1], i0[2], i0[3], i1[0], i1[1], i1[2], i1[3] };
                double f = BitConverter.ToDouble(bytes, 0);

                return f - 1.0;
            }

            return r * (1.0 / (0xFFFFFFFF + 1.0));
        }

        public uint RandomBits()
        {
            uint y;

            if (mti >= MERS_N || mti < 0)
            {
                const uint LOWER_MASK = 0x7FFFFFFF;
                const uint UPPER_MASK = 0x80000000;
                uint[] mag = { 0, MERS_A };

                int kk;

                for (kk = 0; kk < MERS_N - MERS_M; kk++)
                {
                    y = (mt[kk] & UPPER_MASK) | (mt[kk + 1] & LOWER_MASK);
                    mt[kk] = mt[kk + MERS_M] ^ (y >> 1) ^ mag[y & 1];
                }

                for (; kk < MERS_N - 1; kk++)
                {
                    y = (mt[kk] & UPPER_MASK) | (mt[kk + 1] & LOWER_MASK);
                    mt[kk] = mt[kk + (MERS_M - MERS_N)] ^ (y >> 1) ^ mag[y & 1];
                }

                y = (mt[MERS_N - 1] & UPPER_MASK) | (mt[0] & LOWER_MASK);
                mt[MERS_N - 1] = mt[MERS_M - 1] ^ (y >> 1) ^ mag[y & 1];
                mti = 0;
            }

            y = mt[mti++];

            y ^= y >> MERS_U;
            y ^= (y << MERS_S) & MERS_B;
            y ^= (y << MERS_T) & MERS_C;
            y ^= y >> MERS_L;

            return y;
        }
    }
}
