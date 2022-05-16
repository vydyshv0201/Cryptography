using System;
using System.Collections.Generic;

namespace Lab1
{
    class Program
    {
        static void Расстояние(int l)
        {
            if (l / 100 > 0)
                Console.Write(l + "  ");
            else if (l / 10 > 0)
                Console.Write(l + "   ");
            else
                Console.Write(l + "    ");
        }
        static int Вычитание(int a, int b)
        {
            int c, i=0;
            List<int> A_nb = new List<int> ();
            List<int> B_nb = new List<int>();

            while (a != b)
            {
                i += 1;
                A_nb.Add(a);
                B_nb.Add(b);

                if (b > a)
                {
                    c = a;
                    a = b;
                    b = c;
                    i += 1;
                    A_nb.Add(a);
                    B_nb.Add(b);
                }
                a = a-b;
            }
            i += 1;
            A_nb.Add(a);
            B_nb.Add(b);

            Console.Write("______________________________________________");
            for (int j = 7; j <= i; j++)
                Console.Write("_____");
            Console.WriteLine();
            Console.WriteLine(" Вариант алгоритма Евклида с вычитанием");
            Console.Write("______________________________________________");
            for (int j = 7; j <= i; j++)
                Console.Write("_____");
            Console.WriteLine();
            Console.Write(" Номер итерации   ");

            for (int j = 1; j <= i; j++)
                Расстояние(j);

            Console.Write("\n  a               ");
            for (int j = 0; j < i; j++)
                Расстояние(A_nb[j]);

            Console.Write("\n  b               ");
            for (int j = 0; j < i; j++)
                Расстояние(B_nb[j]);
            Console.WriteLine();
            Console.Write("______________________________________________");
            for (int j = 7; j <= i; j++)
                Console.Write("_____");
            Console.WriteLine("\n");
            return i;

        }

        static int Деление(int a, int b)
        {
            int c, i = 1, r;
            List<int> A_nb = new List<int>();
            List<int> B_nb = new List<int>();

            A_nb.Add(a);
            B_nb.Add(b);
            if (b > a)
            {
                c = a;
                a = b;
                b = c;
                i += 1;
                A_nb.Add(a);
                B_nb.Add(b);
            }
            r = a % b;
            while (r != 0) 
            {
                a = b;
                b = r;
                r = a % b;
                i += 1;
                A_nb.Add(a);
                B_nb.Add(b);
            }
            a = b;
            i += 1;
            A_nb.Add(a);
            B_nb.Add(b);

            Console.Write("________________________________________________");
            for (int j = 7; j <= i; j++)
                Console.Write("_____");
            Console.WriteLine();
            Console.WriteLine(" Вариант алгоритма Евклида с делением с остатком");
            Console.Write("________________________________________________");
            for (int j = 7; j <= i; j++)
                Console.Write("_____");
            Console.WriteLine();
            Console.Write(" Номер итерации   ");

            for (int j = 1; j <= i; j++)
                Расстояние(j);
            Console.Write("\n  a               ");

            for (int j = 0; j < i; j++)
                Расстояние(A_nb[j]);

            Console.Write("\n  b               ");
            for (int j = 0; j < i; j++)
                Расстояние(B_nb[j]);

            Console.WriteLine();
            Console.Write("________________________________________________");
            for (int j = 7; j <= i; j++)
                Console.Write("_____");
            Console.WriteLine("\n");
            return i;
        }
        

        static void Main(string[] args)
        {
            int a1 = 18, b1 = 35,
                a2 = 329, b2 = 826,
                a3 = 26, b3 = 738,
                a4 = 288, b4 = 15;
            int[] per = new int[8];


            per[0] = Вычитание(a1, b1);
            per[1] = Деление(a1, b1);
            per[2] = Вычитание(a2, b2);
            per[3] = Деление(a2, b2);
            per[4] = Вычитание(a3, b3);
            per[5] = Деление(a3, b3);
            per[6] = Вычитание(a4, b4);
            per[7] = Деление(a4, b4);

            Console.WriteLine("__________________________________________________________________");
            Console.WriteLine("     a    b      Вариант алгоритма    Вариант алгоритма Евклида с");
            Console.WriteLine("               Евклида с вычитанием      делением с остатком    ");
            Console.WriteLine("__________________________________________________________________");
            Console.Write(" а  ");

            Расстояние(a1);
            Расстояние(b1);
            Console.Write("         ");
            Расстояние(per[0]);
            Console.Write("                     ");
            Console.WriteLine(per[1]);

            Console.Write(" б  ");
            Расстояние(a2);
            Расстояние(b2);
            Console.Write("         ");
            Расстояние(per[2]);
            Console.Write("                     ");
            Console.WriteLine(per[3]);

            Console.Write(" в  ");
            Расстояние(a3);
            Расстояние(b3);
            Console.Write("         ");
            Расстояние(per[4]);
            Console.Write("                     ");
            Console.WriteLine(per[5]);

            Console.Write(" г  ");
            Расстояние(a4);
            Расстояние(b4);
            Console.Write("         ");
            Расстояние(per[6]);
            Console.Write("                     ");
            Console.WriteLine(per[7]);
            Console.WriteLine("__________________________________________________________________");

            Console.ReadKey();
        }
    }
}
