using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3_1
{
    class Program
    {
        static void Euler(int value)
        {
            int n = 0;
            if (value > 1)
            {
                for (int j = 1; j < value; j++)
                {
                    int r = -1, a = value, b = j;

                    while (r != 0)
                    {
                        r = a % b;
                        a = b;
                        b = r;
                    }
                    if (a == 1)
                        n++;
                }
            }
            else if (value == 1)
                n = 1;
            else
                Console.WriteLine("Введите натуральное число");

            Console.WriteLine("mu(" + value + ")=" + n);
        }

        static void Euclid(int a, int b)
        {
            int x, y, r=-1, gcd, a1=a, b1=b;
            while (r != 0)
            {
                r = a1 % b1;
                a1 = b1;
                b1 = r;
            }
            if (a1 == 1)
            {
                gcd = Func(a, b, out x, out y);
                Console.WriteLine($"{a} * {x} + {b} * {y} = {gcd}");
                Console.WriteLine($"{a}^(-1) mod {b} = {x} mod {b}\n");
            }
            else
                Console.WriteLine("Обратного элемента не существует\n");
        }

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

        static void Ost(List<string[]> list)
        {

            for (int i=0; i<list.Count; i++)
            {
                List<int[]> listval = new List<int[]>();
                int M=1;
                int[] MM = new int[3] ;
                int[] MMM = new int[3];
                for (int j=0; j<list[i].Length; j++)
                {
                    int a = 0, c = 0, m = 0, st = 0, n = 0;
                    if (list[i][j][0] == 'x')
                        a = 1;
                    else
                        while (list[i][j][n] != 'x')
                        {
                            int r = list[i][j][n] - '0';
                            a = a * 10 + r;
                            n++;
                        }
                    n += 4;
                    while ((list[i][j][n] != '(' ) && (list[i][j][n] != '^'))
                    {
                        c = c * 10 + (list[i][j][n] - '0');
                        n++;
                    }
                    if (list[i][j][n] == '^')
                    {
                        n += 2;
                        string k = "";
                        while (list[i][j][n] != ')')
                        {
                            k = k + list[i][j][n];
                            n++;
                        }
                        st = Convert.ToInt32(k);
                        n++;
                    }
                    n += 5;
                    while (list[i][j][n] != ')')
                    {
                        m = m * 10 + (list[i][j][n] - '0');
                        n++;
                    }
       

                    if (st == -1)
                    {
                        int x, y;
                        Func(c, m, out x, out y);
                        c = x;
                        while (c < 0)
                            c += m;
                    }

                    while (a != 1 )
                    {
                        


                        while (a <= m)
                        {
                            int y = m / a;
                            a = a*(y+1);
                            c = c*(y + 1);
                        }
                        while ((a - m) > 0)
                        {
                            a = a-m;
                        }
                        while ((c - m) > 0)
                        {
                            c =c- m;
                        }
                        int a1 = a, c1 = c, r = -1;
                        while (r != 0)
                        {
                            if (c1 > a1)
                            {
                                int f;
                                f = a1;
                                a1 = c1;
                                c1 = f;
                            }
                            r = a1 % c1;
                            a1 = c1;
                            c1 = r;

                        }
                        a /= a1;
                        c /= a1;
                        if (a == c)
                        {
                            a = 1;
                            c = 1;
                        }
                        
                    }
      
                    listval.Add(new int[] {a, c, m});

                    M = M * m;

                }

                if (list[i].Length == 2)
                {
                    int y1, y2, x;
                    MM[0] = M / listval[0][2];
                    MM[1] = M / listval[1][2];
                    Func(MM[0], listval[0][2], out MMM[0], out y1);
                    Func(MM[1], listval[1][2], out MMM[1], out y2);
                    while (MMM[0] < 0)
                        MMM[0] += listval[0][2];
                    while (MMM[1] < 0)
                        MMM[1] += listval[1][2];
                    x = listval[0][1] * MM[0] * MMM[0] + listval[1][1] * MM[1] * MMM[1];
                    while ((x - M) > 0)
                        x = x - M;
                    Console.WriteLine($"x = {x} mod {M}\n");
                }
                else
                {
                    int y1, y2, y3, x;
                    MM[0] = M / listval[0][2];
                    MM[1] = M / listval[1][2];
                    MM[2] = M / listval[2][2];
                    Func(MM[0], listval[0][2], out MMM[0], out y1);
                    Func(MM[1], listval[1][2], out MMM[1], out y2);
                    Func(MM[2], listval[2][2], out MMM[2], out y3);
                    while (MMM[0] < 0)
                        MMM[0] += listval[0][2];
                    while (MMM[1] < 0)
                        MMM[1] += listval[1][2];
                    while (MMM[2] < 0)
                        MMM[2] += listval[2][2];
                    x = listval[0][1] * MM[0] * MMM[0] + listval[1][1] * MM[1] * MMM[1] + listval[2][1] * MM[2] * MMM[2];
                    while ((x - M) > 0)
                        x = x - M;
                    Console.WriteLine($"x = {x} mod {M}\n");
                }
            }

        }


        static void Main(string[] args)
        {
            int[] Task1 = new int[] {13,17,9,16,6,24,227,725,94836};
            int[] Task2 = new int[] {2,33, 3,256, 16,89, 21,15};
            List<string[]> Task3 = new List<string[]>() ;
            Task3.Add(new string[] { "3x = 1(mod 4)", "2x = 3(mod 5)" });
            Task3.Add(new string[] { "3x = 5(mod 7)", "2x = 1(mod 3)" });
            Task3.Add(new string[] { "6x = 7(mod 11)", "3x = 1(mod 5)" });
            Task3.Add(new string[] { "5x = 3(mod 7)", "7x = 2(mod 9)" });
            Task3.Add(new string[] { "2x = 4(mod 5)", "3x = 5(mod 7)", "5x = 3(mod 8)" });
            Task3.Add(new string[] { "x = 2(mod 3)", "6x = 5(mod 11)", "3x = 2(mod 4)" });
            Task3.Add(new string[] { "x = 2^(-1)(mod 7)", "4x = 11(mod 13)", "16x = 5(mod 19)" });
            Task3.Add(new string[] { "x = 13(mod 17)", "3x = 8(mod 121)", "3x = 2(mod 4)" });


            Console.WriteLine("Задание №3:\n");
            for (int i = 0;  i < Task1.Length; i++)
              Euler(Task1[i]);

            Console.WriteLine("\nЗадание №9:\n");
            for (int i = 0; i < Task2.Length; i += 2)
              Euclid(Task2[i], Task2[i+1]);

            Console.WriteLine("Задание №11:\n");
            Ost(Task3);

            Console.ReadKey();

        }
    }
}
