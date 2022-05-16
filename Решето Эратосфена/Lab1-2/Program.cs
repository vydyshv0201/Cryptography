using System;

namespace Lab1_2
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.Write("Введите произвольное число N: ");
            int n = Convert.ToInt32(Console.ReadLine());
            if (n<2)
                while (n < 2)
                {
                    Console.Write("Ошибка. Введите число N занаво: ");
                    n = Convert.ToInt32(Console.ReadLine());
                }
                
            int[] mas = new int[n-1];
            for (int i = 0; i < n - 1; i++)
                mas[i] = i + 2;
            int z = 2, root=2;
            if (n > 4)
                while (root == 2)
                {
                    if (z * z > n)
                        root = z - 1;
                    z++;
                }
            for (int j=2; j<=root; j++)
                for (int i = 0; i < n - 1; i++)
                    if (mas[i] % j == 0 && mas[i] != j)
                        mas[i] = 0;

            for (int i = 0; i < n - 1; i++)
                if (mas[i] != 0)
                    Console.Write(mas[i] + " ");

        }
    }
}
