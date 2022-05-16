using System;

namespace lab6
{
    class Program
    {
        static void Main(string[] args)
        {
            uint a1 = 0xfedcba98;
            uint a0 = 0x76543210;
            uint[] Keys = KeysMas("ffeeddccbbaa99887766554433221100f0f1f2f3f4f5f6f7f8f9fafbfcfdfeff");

            

            Console.Write(" (a1, a0) = (" + Convert.ToString(a1, 16) + ", " + Convert.ToString(a0, 16) + ") =");

            var enc = encrypt(a1, a0, Keys);
            a1 = enc.Item1;
            a0 = enc.Item2;

            Console.WriteLine("(" + Convert.ToString(a1, 16) + ", " + Convert.ToString(a0, 16) + ")");
            Console.Write(" (a1, a0) = (" + Convert.ToString(a1, 16) + ", " + Convert.ToString(a0, 16) + ") =");

            var dec = decrypt(a1, a0, Keys);
            a1 = dec.Item1;
            a0 = dec.Item2;

            Console.WriteLine("(" + Convert.ToString(a1, 16) + ", " + Convert.ToString(a0, 16) + ")");

            Console.ReadKey();

        }
        static (uint, uint) encrypt(uint a1, uint a0, uint[] Keys)
        {
            uint value;

            for (int i = 0; i <= 23; i++)
            {
                value = a0;
                a0 = a1 ^ func_g(a0, Keys[i % 8]);
                a1 = value;
            }

            for (int i = 24; i <= 30; i++)
            {
                value = a0;
                a0 = a1 ^ func_g(a0, Keys[7 - (i % 8)]);
                a1 = value;
            }

            a1 = a1 ^ func_g(a0, Keys[0]);

            return (a1, a0);
        }
        static (uint, uint) decrypt(uint a1, uint a0, uint[] Keys)
        {
            uint value;

            a1 = a1 ^ func_g(a0, Keys[0]);

            for (int i = 30; i >= 24; i--)
            {
                value = a1;
                a1 = a0 ^ func_g(a1, Keys[7 - (i % 8)]);
                a0 = value;
            }

            for (int i = 23; i >= 0; i--)
            {
                value = a1;
                a1 = a0 ^ func_g(a1, Keys[i % 8]);
                a0 = value;
            }
            return (a1, a0);
        }

        static uint func_g(uint a0, uint key)
        {
            uint result = a0 + key;
            result = Replace(result);
            uint sd = result >> 21;
            result = (result << 11) + sd;
            return result;
        }


        static uint[] KeysMas(string key)
        {
            uint[] keys = new uint[8];

            for (int i = 0; i < 8; i++)
            {
                string val = "";
                for (int j = 0; j < 8; j++)
                {
                    val += key[8*i+j];
                }
                keys[i] = Convert.ToUInt32(val, 16);
            }

            return keys;
        }

        static uint Replace(uint value)
        {
            uint[,] blocks = new uint[,]
            {
                {12, 4, 6, 2, 10, 5, 11, 9, 14, 8, 13, 7, 0, 3, 15, 1},
                {6, 8, 2, 3, 9, 10, 5, 12, 1, 14, 4, 7, 11, 13, 0, 15},
                {11, 3, 5, 8, 2, 15, 10, 13, 14, 1, 7, 4, 12, 9, 6, 0},
                {12, 8, 2, 1, 13, 4, 15, 6, 7, 0, 10, 5, 3, 14, 9, 11},
                {7, 15, 5, 10, 8, 1, 6, 13, 0, 9, 3, 14, 11, 4, 2, 12},
                {5, 13, 15, 6, 9, 2, 12, 10, 11, 7, 8, 1, 4, 3, 14, 0},
                {8, 14, 2, 5, 6, 9, 1, 12, 15, 4, 11, 0, 13, 10, 3, 7},
                {1, 7, 14, 13, 0, 5, 8, 3, 4, 15, 10, 6, 9, 12, 11, 2},
            };

            uint mask = 0b1111;
            uint result = 0;

            for (int i = 0; i < 8; i++)
            {
                uint fourbit = (value >> (i * 4)) & mask;
                result += blocks[i, fourbit] << (i * 4);
            }

            return result;
        }
    }
}
