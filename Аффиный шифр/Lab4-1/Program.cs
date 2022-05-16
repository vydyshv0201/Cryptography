using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Lab4_1
{
    class Program
    {
        static int Func(int a, int b, out int x, out int y)
        {
            if (a == 0)
            {
                x = 0;
                y = 1;
                return b;
            }

            int gcd = Func(b % a, a, out x, out y);

            int newY = x;
            int newX = y - (b / a) * x;

            x = newX;
            y = newY;
            return gcd;
        }
        static void Main(string[] args)
        {
            string RusAL = ".абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            string RusALZ = ".АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
            int m = RusAL.Length-1, a=0;
            bool flag= false;
            while (!flag)
            {
                Console.Write("Введите параметр a = ");
                a = Convert.ToInt32(Console.ReadLine());
                int a1 = a, m1 = m, r = -1;
                while (r != 0)
                {
                    r = m1 % a1;
                    m1 = a1;
                    a1 = r;
                }
                if (m1 == 1)
                    flag = true;
            }
            Console.Write("Введите параметр b = ");
            int b = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Зашифровать(1) или расшифровать(2) текст? ");
            int n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            string text = "";
            using (StreamReader sr = new StreamReader("text.txt", Encoding.Default))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    text = text + line;
                }
            }
            using (StreamWriter sr = new StreamWriter("text.txt", false, Encoding.Default))
            {
                sr.WriteLine(text);
            }

            if (n == 1)
            {
                for (int i = 0; i<text.Length; i++)
                {
                    int l=-1;
                    if (Regex.IsMatch(Convert.ToString(text[i]), @"^[а-яА-ЯёЁ]+$") == true)
                    {
                        int j = 0;
                        while (l == -1)
                        {
                            if (text[i] == RusAL[j])
                            {
                                l = j;
                                int k = (l * a + b) % m;
                                if (k == 0)
                                    k = 33;
                                Console.Write(RusAL[k]);
                            }   
                            if (text[i] == RusALZ[j])
                            {
                                l = j;
                                int k = (l * a + b) % m;
                                if (k == 0)
                                    k = 33;
                                Console.Write(RusALZ[k]);
                            }
                            j++;
                        }
                        
                    }
                    else
                        Console.Write(text[i]);
                }
            }
            else
            {
                int x, y;
                Func(a, m, out x, out y);
                a = x;
                if (a < 0)
                    a = a + m;
                for (int i = 0; i < text.Length; i++)
                {
                    int l = -1;
                    if (Regex.IsMatch(Convert.ToString(text[i]), @"^[а-яА-ЯёЁ]+$") == true)
                    {
                        int j = 0;
                        while (l == -1)
                        {
                            if (text[i] == RusAL[j])
                            {
                                l = j;
                                int k = (a * (l - b + m)) % m;
                                if (k == 0)
                                    k = 33;
                                Console.Write(RusAL[k]);
                            }
                            if (text[i] == RusALZ[j])
                            {
                                l = j;
                                int k = (a * (l - b + m)) % m;
                                if (k == 0)
                                    k = 33;
                                Console.Write(RusALZ[k]);
                            }
                            j++;
                        }
                    }
                    else
                        Console.Write(text[i]);
                }
            }
            Console.WriteLine("\n");
        }
    }
}
