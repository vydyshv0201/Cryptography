using System;
using System.Collections.Generic;
using System.Numerics;

//протокол подбрасывания монеты между двумя участниками на основании проблемы дискретного логарифмирования с безусловной связанностью.

namespace Lab_8
{
    class Program
    {
        static List<int> rr = new List<int>();
        static void Main(string[] args)
        {
            int[] q = { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97 };
            BigInteger p, g, b, x, r;

            int t = 10;

            bool flag = true;

            //Параметры протокола:
            Console.WriteLine("Параметры протокола: \n");
            do
            {
                //p = Prime(q);
                p = 23;
                //flag = Test(p, t, q);

            } while (!flag);

            //g = PorEl(p, rr);
            g = 5;

            Console.WriteLine("p = " + p);
            Console.WriteLine("g = " + g);

            Console.WriteLine("------------------------------------");

            //Этап привязки:

            Console.WriteLine("Этап привязки: \n");
            Console.WriteLine("Alice: ");

            b = 1;
            x = 11;

            r = BigInteger.ModPow(g, x, p);

            Console.WriteLine("Отправляю r = " + r);

            Console.WriteLine("\nBob: ");
            Console.WriteLine("Получил r = " + r);

            Console.WriteLine("------------------------------------");

            Console.WriteLine("Этап привязки: \n");
            Console.WriteLine("Alice: ");
            Console.WriteLine("Отправляю b = {0},  x = {1} \n", b, x);

            Console.WriteLine("Bob: ");
            Console.WriteLine("Получил b = {0},  x = {1}", b, x);
            Console.WriteLine("Проверяю r = g^x mod p");
            if (r == BigInteger.ModPow(g, x, p))
            {
                Console.WriteLine("Равенство верно");
            }
            else
            {
                Console.WriteLine("Равенство неверно");
            }

        }

        public static BigInteger Prime(int[] q)
        {
            bool flag = false;
            BigInteger n = 0;
            while (!flag)
            {
                BigInteger m = 0;

                do
                {
                    Random rnd = new Random();
                    Random rnd2 = new Random();

                    m += BigInteger.Pow(q[rnd.Next(0, q.Length)], rnd2.Next(1, 100));


                } while (m < BigInteger.Pow(2, 126));
                if (m <= BigInteger.Pow(2, 127) - 1)
                {
                    flag = true;
                    n = 2 * m + 1;
                }

            }


            return n;
        }

        public static bool Test(BigInteger n, int t, int[] q) //Тест Миллера
        {

            BigInteger[] a = new BigInteger[t];
            BigInteger n2 = n - 1;
            Random rnd = new Random();

            for (int i = 0; i < q.Length; i++)
            {
                if (n2 % q[i] == 0)
                    rr.Add(q[i]);

                while (n2 % q[i] == 0)
                {
                    n2 /= q[i];
                }
            }

            for (int i = 0; i < t; i++)
            {
                do
                {
                    a[i] = randomnum2();
                } while (a[i] >= n || a[i] <= 1);


                if (BigInteger.ModPow(a[i], n - 1, n) != 1)
                {
                    rr.Clear();
                    return false;
                }
            }

            for (int i = 0; i < rr.Count; i++)
            {
                bool flag1 = false;
                for (int j = 0; j < t; j++)
                {
                    if (BigInteger.ModPow(a[j], (n - 1) / rr[i], n) != 1)
                    {
                        flag1 = true;
                        break;
                    }

                }
                if (!flag1)
                {
                    rr.Clear();
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

        public static BigInteger PorEl(BigInteger p, List<int> r)
        {
            BigInteger g;
            bool flagp = false;
            do
            {
                flagp = false;
                do
                {
                    g = randomnum2();
                } while (g >= p - 1 || g <= 1);

                for (int i = 0; i < r.Count; i++)
                {
                    if (BigInteger.ModPow(g, (p - 1) / r[i], p) == 1)
                    {
                        flagp = true;
                        break;
                    }
                }

            } while (flagp);



            return g;
        }
    }
}
