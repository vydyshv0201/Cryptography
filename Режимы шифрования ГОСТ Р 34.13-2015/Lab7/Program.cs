using System;
using System.Collections.Generic;

namespace lab7
{
    class Program
    {
        static void Main(string[] args)
        {
            ulong[] P = Input("92def06b3c130a59db54c704f8189d204a98fb2e67a8024c8912409b17b57e41"); //открытый текст
            uint[] Keys = KeysMas("ffeeddccbbaa99887766554433221100f0f1f2f3f4f5f6f7f8f9fafbfcfdfeff"); //ключ
            string IV_1 = "1234567890abcdef234567890abcdef134567890abcdef12"; //синхропосылки
            string IV_2 = "1234567890abcdef234567890abcdef1";



            ECB(P, Keys); //Режим простой замены
            CBC(P, Keys, IV_1); //Режим простой замены с зацеплением
            OFB(P, Keys, IV_2);//Режим гаммирования с обратной связью по выходу
            Console.ReadKey();

        }

        static void OFB(ulong[] P, uint[] Keys, string IV) //Режим гаммирования с обратной связью по выходу
        {
            string R = IV;
            Console.WriteLine("Режим гаммирования с обратной связью по выходу");
            ulong[] c = new ulong[P.Length];
            for (int i = 0; i < P.Length; i++)
            {
                Console.Write("P" + (i + 1) + ": ");
                Console.Write(Convert.ToString(unchecked((long)P[i]), 16));

                string R0 = R.Substring(0, 16);
                c[i] = encrypt(Convert.ToUInt64(R0, 16), Keys);
                R = R.Remove(0, 16) + Convert.ToString(unchecked((long)c[i]), 16);
                c[i] = c[i] ^ P[i];
                Console.WriteLine(" Шифрование: " + Convert.ToString(unchecked((long)c[i]), 16));
            }

            R = IV;

            for (int i = 0; i < P.Length; i++)
            {
                Console.Write("P" + (i + 1) + ": ");
                Console.Write(Convert.ToString(unchecked((long)P[i]), 16));

                string R0 = R.Substring(0, 16);
                P[i] = encrypt(Convert.ToUInt64(R0, 16), Keys);
                R = R.Remove(0, 16) + Convert.ToString(unchecked((long)P[i]), 16);
                P[i] = c[i] ^ P[i];
                Console.WriteLine(" Расшифрование: " + Convert.ToString(unchecked((long)P[i]), 16));
            }
            Console.WriteLine();
        }

        static void CBC(ulong[] P,uint[] Keys, string IV) //Режим простой замены с зацеплением
        {
            string R = IV;
            Console.WriteLine("Режим простой замены с зацеплением");
            ulong[] c = new ulong[P.Length];
            for (int i = 0; i<P.Length; i++)
            {
                Console.Write("P" + (i + 1) + ": ");
                Console.Write(Convert.ToString(unchecked((long)P[i]), 16));

                string R0 = R.Substring(0, 16);
                c[i] = Convert.ToUInt64(R0, 16) ^ P[i];
                c[i] = encrypt(c[i], Keys);
                R = R.Remove(0, 16) + Convert.ToString(unchecked((long)c[i]), 16);
                Console.WriteLine(" Шифрование: " + Convert.ToString(unchecked((long)c[i]), 16));
            }

            R = IV;

            for (int i = 0; i < P.Length; i++)
            {
                Console.Write("P" + (i + 1) + ": ");
                Console.Write(Convert.ToString(unchecked((long)P[i]), 16));

                string R0 = R.Substring(0, 16);
                P[i] = decrypt(c[i], Keys);
                P[i] = Convert.ToUInt64(R0, 16) ^ P[i];
                R = R.Remove(0, 16) + Convert.ToString(unchecked((long)c[i]), 16);
                Console.WriteLine(" Расшифрование: " + Convert.ToString(unchecked((long)P[i]), 16));
            }
            Console.WriteLine();
        }

        static void ECB(ulong[] P, uint[] Keys) //Режим простой замены
        {

            Console.WriteLine("Режим простой замены");
            for (int i = 0; i < P.Length; i++)
            {
                Console.WriteLine("P" + (i + 1) + ":");

                Console.WriteLine (Convert.ToString(unchecked((long)P[i]), 16));

                ulong enc = encrypt(P[i], Keys);

                Console.WriteLine("Шифрование: " + Convert.ToString(unchecked((long)enc), 16));

                ulong dec = decrypt(enc, Keys);

                Console.WriteLine("Расшифрование: " + Convert.ToString(unchecked((long)dec), 16));
            }
            Console.WriteLine();
        }

        static ulong[] Input(string text)
        {
            int S = 64;
            List<ulong> mas = new List<ulong>();
            if (text.Length % (S / 4) != 0)
            {
                text += "1";
                while (text.Length % (S / 4) != 0)
                    text += "0";
            }
            while (text.Length >= S/4)
            {
                mas.Add(Convert.ToUInt64(text.Substring(text.Length - S / 4), 16));
                text = text.Substring(0, text.Length - S / 4);
            }
            mas.Reverse();
            return mas.ToArray();
        }

        static (uint, uint) PMas(ulong value)
        {
            string P = Convert.ToString(unchecked((long)value), 16);
            uint a1, a0;
            string val2 = "", val1 = "";

            for (int i = 0; i < 16; i++)
            {
                
                if (i < 8)
                    val1 += P[i];
                else
                    val2 += P[i];

            }

            a1 = Convert.ToUInt32(val1, 16);
            a0 = Convert.ToUInt32(val2, 16);

            return (a1, a0);
        }

        static ulong encrypt(ulong P, uint[] Keys)
        {
            var a = PMas(P);
            uint a1 = a.Item1;
            uint a0 = a.Item2;
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

            return Convert.ToUInt64(Convert.ToString(a1, 16) + Convert.ToString(a0, 16), 16);
        }

        static ulong decrypt(ulong P, uint[] Keys)
        {
            var a = PMas(P);
            uint a1 = a.Item1;
            uint a0 = a.Item2;
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
            return Convert.ToUInt64(Convert.ToString(a1, 16) + Convert.ToString(a0, 16), 16);
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
                    val += key[8 * i + j];
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
