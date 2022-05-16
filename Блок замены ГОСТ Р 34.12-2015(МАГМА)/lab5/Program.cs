using System;

namespace lab5
{
    class Program
    {
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

            for (int i=0; i<8; i++)
            {
                uint fourbit = (value >> (i * 4)) & mask;
                result += blocks[i, fourbit] << (i * 4);
            }

            return result;
        }
        static void Main(string[] args)
        {
            uint value1 = 0xfdb97531;
            uint value2 = 0x2a196f34;
            uint value3 = 0xebd9f03a;
            uint value4 = 0xb039bb3d;

            Console.WriteLine("t(" + Convert.ToString(value1, 16) + ") = " + Convert.ToString(Replace(value1), 16));
            Console.WriteLine("t(" + Convert.ToString(value2, 16) + ") = " + Convert.ToString(Replace(value2), 16));
            Console.WriteLine("t(" + Convert.ToString(value3, 16) + ") = " + Convert.ToString(Replace(value3), 16));
            Console.WriteLine("t(" + Convert.ToString(value4, 16) + ") = " + Convert.ToString(Replace(value4), 16));

        }
    }
}
