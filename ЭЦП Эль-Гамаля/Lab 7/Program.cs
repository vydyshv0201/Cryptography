using System;
using System.Collections.Generic;
using System.Numerics;

namespace Lab_7
{
    class Program
    {
        static List<int> r = new List<int>();
        static void Main(string[] args)
        {

            //Может прнимать простое число за составное, но не наоборот
            int[] q = { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97 };

            int t = 10;
            BigInteger p, g, k, d, a, s, z, a_, R, h;

            bool flag = true;

            h = 18;

            Console.WriteLine("h = " + h);

            do
            {
                p = Prime(q);
                //p = 67853;
                flag = Test(p, t, q);
                
            } while (!flag);

            Console.WriteLine("p = " + p);

            g = PorEl(p, r);
            //g = 2;

            Console.WriteLine("g = " + g);

            do
            {
                k = randomnum2();
                //k = 15;

            } while (k<=1 || k>=p-1 || NOD(p-1, k) != 1);

            Console.WriteLine("k = " + k);

            d = BigInteger.ModPow(g, k, p);

            Console.WriteLine("d = " + d);

            do
            {
                a = randomnum2();
                //a = 55;

            } while (a <= 1 || a >= p - 1 || NOD(p - 1, a) != 1);

            Console.WriteLine("a = " + a);

            s = BigInteger.ModPow(g, a, p);

            Console.WriteLine("s = " + s);

            EuclidRas(a, p-1, out a_, out z);

            while (a_ < 0)
            {
                a_ += p - 1;
            }

            Console.WriteLine("a^-1 = " + a_);

            R = ((h - s * k) * a_) % (p - 1);

            while (R < 0)
            {
                R += p - 1;
            }

            Console.WriteLine("r = " + R);

            if ((BigInteger.ModPow(d, s, p) * BigInteger.ModPow(s, R, p))%p == BigInteger.ModPow(g, h, p))
            {
                Console.WriteLine("Подпись принимается!");
            }
            else
            {
                Console.WriteLine("Подпись НЕ принимается!");
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
                    r.Add(q[i]);

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
                    r.Clear();
                    return false;
                }
            }

            for (int i = 0; i < r.Count; i++)
            {
                bool flag1 = false;
                for (int j = 0; j < t; j++)
                {
                    if (BigInteger.ModPow(a[j], (n - 1) / r[i], n) != 1)
                    {
                        flag1 = true;
                        break;
                    }

                }
                if (!flag1)
                {
                    r.Clear();
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

        public static BigInteger NOD(BigInteger a, BigInteger b)
        {
            BigInteger r;

            r = a % b;
            while (r != 0)
            {
                a = b;
                b = r;
                r = a % b;
            }

            return b;
        }

        static BigInteger EuclidRas(BigInteger a, BigInteger b, out BigInteger x, out BigInteger y)
        {
            if (a == 0)
            {
                x = 0;
                y = 1;
                return b;
            }

            BigInteger gcd = EuclidRas(b % a, a, out x, out y);

            BigInteger newY = x;
            BigInteger newX = y - (b / a) * x;

            x = newX;
            y = newY;
            return gcd;
        }

    }
}
