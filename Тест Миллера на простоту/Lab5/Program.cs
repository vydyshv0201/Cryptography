using System;
using System.Collections.Generic;
using System.Numerics;

namespace Lab5
{
    class Program
    {
        static void Main(string[] args)
        {

            //Может прнимать простое число за составное, но не наоборот
            int[] q = {2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97};

            int t = 10;
            BigInteger n;
            bool flag;

            do
            {
                n = Prime(q);
                //Console.WriteLine(n);
                //n = 7; //Пример
                flag = Test(n, t, q);
                if(!flag)
                {
                    Console.WriteLine(n + " - составное число");
                }
                else
                {
                    Console.WriteLine(n + " - простое число");
                }
            } while (!flag);       
        
        }

        public static BigInteger Prime(int[] q)
        {
            bool flag = false;
            BigInteger n=0;
            while (!flag)
            {
                BigInteger m = 0;
                
                do
                {
                    Random rnd = new Random();
                    Random rnd2 = new Random();

                    m += BigInteger.Pow(q[rnd.Next(0, q.Length)], rnd2.Next(1, 100));
                    

                } while (m < BigInteger.Pow(2, 126));
                if (m <= BigInteger.Pow(2, 127) - 1 )
                {
                    flag = true;
                    n = 2 * m + 1;
                }
                
            }
            

            return n;
        }

        public static bool Test(BigInteger n, int t, int[] q) //Тест Миллера
        {
            List<int> r = new List<int>();
            BigInteger[] a = new BigInteger[t];
            BigInteger n2 = n-1;
            Random rnd = new Random();

            for (int i = 0; i <q.Length; i++)
            {
                if (n2 % q[i] == 0)
                    r.Add(q[i]);

                while (n2 % q[i] == 0)
                {
                    n2 /= q[i];
                }
            }

            for (int i=0; i<t; i++) 
            {
                a[i] = randomnum2();
                
                if (BigInteger.ModPow(a[i], n - 1, n) != 1)
                {
                    return false;
                }
            }

            for (int i=0; i<r.Count; i++)
            {
                for (int j = 0; j < t; j++)
                {
                    if (BigInteger.ModPow(a[j], (n - 1)/r[i], n) != 1)
                    {
                        break;
                    }
                    Console.Write("Вероятно, ");
                    return false;
                }
            }

            return true;
        }

        public static BigInteger randomnum2()
        {
            BigInteger R;
            Random random = new Random();
            string text = "";

            for (int i = 0; i < 128; i++)
            {
                text = text + Convert.ToString(random.Next(0, 2));
            }

            R = StringToBinaryBigInteger(text);


            return R;
        }

        public static BigInteger StringToBinaryBigInteger(string binary)
        {
            BigInteger result = 0;

            foreach (char c in binary)
            {
                result <<= 1;
                result += c == '1' ? 1 : 0;
            }

            return result;
        }

    }
}
