using System;
using System.Numerics;

namespace Lab6
{
    class Program
    {
        static void Main(string[] args)
        {
            BigInteger p, q, n, fin, e, d, nod, z, y, x, x1, x2, x_;

            while (true)
            {
                p = randomnum();
            
                if (isPrime(p))
                {
                    Console.WriteLine("p = " + p.ToString("X"));
                    break;
                }
            }
            
            while (true)
            {
                q = randomnum();
                if (isPrime(q))
                {
                    Console.WriteLine("q = " + q.ToString("X"));
                   break;
                }
            }
            n = p * q;
            Console.WriteLine("n = " + n.ToString("X"));
            fin = (p - 1) * (q - 1);
            Console.WriteLine("fi(n) = " + fin.ToString("X"));
            do
            {
                e = randomnum2();
              
                nod = NOD(fin, e);
            } while (e >= fin || e<=1 || nod!=1);
            Console.WriteLine("e = " + e.ToString("X"));
            Console.WriteLine("NOD (fin, e) = " + NOD(fin, e));

            EuclidRas(e, fin, out d, out z);
            while (d < 0)
                d = d + fin;
            Console.WriteLine("d = " + d.ToString("X"));

            x = randomnum3();
            
            Console.WriteLine("x = " + x.ToString("X"));
            y = BigInteger.ModPow(x, e, n);

            Console.WriteLine("y = " + y.ToString("X"));

            x1 = BigInteger.ModPow(y, d%(p-1), p);
            x2 = BigInteger.ModPow(y, d % (q - 1), q);
            Console.WriteLine("x1 = " + x1.ToString("X"));
            Console.WriteLine("x2 = " + x2.ToString("X"));

            x_ = KTO(x1, x2, p, q);

            
            if (x_ < 0)
            {
                Console.WriteLine("Число вышло за диапазон!!!!!!");
            }
           
                
            Console.WriteLine("x' = " + x_.ToString("X"));

        }

        public static BigInteger KTO(BigInteger x1, BigInteger x2, BigInteger p, BigInteger q)
        {
            BigInteger M1, M2, M, M1_, M2_, y1, y2, x;
            

            M = p * q;

            M1 = M / p;
            M2 = M / q;

            EuclidRas(M1, p, out M1_, out y1);
            EuclidRas(M2, q, out M2_, out y2);
            M1_ = M1_ % p;
            M2_ = M2_ % q;
            while (M1 < 0)
                M1_ += p;
            while (M2 < 0)
                M2_ += q;
            x = ((x1 * M1*M1_)%M + (x2*M2*M2_)%M)%M;
            

            return x;
        }

        public static Boolean isPrime(BigInteger num)
        {
            for (int i = 2; i <= 50; i++)
            {
                BigInteger a = 0;
                while (a < 2 || a > num - 2)
                {
                    a = randomnum2();
                }
                var n = BigInteger.ModPow(a, num - 1, num);
                if (n != 1)
                {
                    return false;
                }

            }
            return true;
        }



        public static BigInteger randomnum()
        {
            BigInteger R;
            Random random = new Random();
            string text = "1";

            for (int i = 0; i < 510; i++)
            {
                text = text + Convert.ToString(random.Next(0, 2));
            }

            text = text + "1";

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

        public static BigInteger randomnum2()
        {
            BigInteger R;
            Random random = new Random();
            string text = "";

            for (int i = 0; i < 512; i++)
            {
                text = text + Convert.ToString(random.Next(0, 2));
            }

            R = StringToBinaryBigInteger(text);


            return R;
        }

        public static BigInteger randomnum3()
        {
            BigInteger R;
            Random random = new Random();
            string text = "";

            for (int i = 0; i < 530; i++)
            {
                text = text + Convert.ToString(random.Next(0, 2));
            }

            R = StringToBinaryBigInteger(text);


            return R;
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
