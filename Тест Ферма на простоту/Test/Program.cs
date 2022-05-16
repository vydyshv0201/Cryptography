using System;
using System.Collections;
using System.Numerics;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            BigInteger n;

            int flag = 0;
            int kol = 0, bit = 128;
            double p, k, kp, sr=0;

            while (flag != 10)
            {
                n = randomnum();
                if (isPrime(n))
                {
                    Console.WriteLine(n + " - простое число     Перебрано до него - " + kol);
                    sr += kol;
                    flag +=1;
                    kol = 0;
                }
                else
                {
                    kol += 1;
                }
            }

            kp = (Math.Pow(2, bit) / Math.Log(Math.Pow(2, bit))) - (Math.Pow(2, bit - 1) / Math.Log(Math.Pow(2, bit - 1))); //кол-во простых чисел в диапазоне 2^128 - 2^(128-1)
            p = kp / (Math.Pow(2, bit)- Math.Pow(2, bit-1)); // Вероятность выбрать случайно простое число
            p = p * 2; //если поиск среди нечетных чисел
            k = 1/p; //ожидаемое кол-во перебранных чисел до получения простого
            Console.WriteLine("Среднее ожидаемое k = " + Math.Round(k)) ;
            Console.WriteLine("Среднее ожидаемое k' = " + Math.Round(sr/10));


        }

        public static Boolean isPrime(BigInteger num)
        {
            for (int i = 2; i <= 50; i++) 
            {
                BigInteger a =0;
                while (a<2 || a> num - 2)
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

            for (int i=0; i<126; i++)
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

            for (int i = 0; i < 128; i++)
            {
                text = text + Convert.ToString(random.Next(0, 2));
            }

            R = StringToBinaryBigInteger(text);


            return R;
        }

    }
}
