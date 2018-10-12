using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU_decomposition
{
    class Program
    {
        public static double[] extractColumn(double[,] matrix, int number)
        {
            int row = matrix.GetLength(0);
            double[] a = new double[row];
            for (int i = 0; i < row; i++)
            {
                a[i] = matrix[i, number];
            }
            return a;
        }

        public static double[,] decomposition(double[,] a, double[,] E)
        {
            int i, j, k;
            double[,] l = new double[a.GetLength(0), a.GetLength(1)];
            double[,] u = new double[a.GetLength(0), a.GetLength(1)];
            int MaxOrder = a.GetLength(0);

            for (i = 0; i < MaxOrder; i++)
                l[i, i] = 1.0;

            for (j = 0; j < a.GetLength(0); j++)
            {
                for (i = 0; i < a.GetLength(1); i++)
                {
                    if (i >= j)
                    {
                        u[j, i] = a[j, i];
                        for (k = 0; k < j; k++)
                            u[j, i] = u[j, i] - u[k, i] * l[j, k];
                    }
                    if (i > j)
                    {
                        l[i, j] = a[i, j];
                        for (k = 0; k < j; k++)
                            l[i, j] = l[i, j] - u[k, j] * l[i, k];
                        l[i, j] = l[i, j] / u[j, j];
                    }
                }
            }
            /*Решение L*Y = E, U*X = Y*/
            int count = 0;
            double[,] y = new double[a.GetLength(0), a.GetLength(0)];
            double[] d = extractColumn(E, count);
            double[,] x = new double[a.GetLength(0), a.GetLength(0)];
            
            while(count < a.GetLength(0))
            {
                d = extractColumn(E, count);
                y[0, count] = d[count];
                for (j = 1; j < MaxOrder; j++)
                {
                    y[j, count] = d[j];
                    for (i = 0; i < j; i++)
                    {
                        y[j, count] = y[j, count] - y[i, count] * l[j, i];
                    }
                }
                x[count, MaxOrder - 1] = y[MaxOrder - 1, count] / u[MaxOrder - 1, MaxOrder - 1];
                count++;
            }
            count = 2;
            while (count >= 0)
            {
                d = extractColumn(y, count);
                for (j = MaxOrder -1; j >= 0; j--)
                {
                    x[j, count] = d[j];
                    for (i = MaxOrder - 1; i > j; i--)
                    {
                        x[j, count] = x[j, count] - x[i, count] * u[j, i];
                    }
                    x[j, count] = x[j, count] / u[j, j];
                }
                count--;
            }
            for( i = 0; i < x.GetLength(0); i++)
            {
                for (int g = 0; g < x.GetLength(1); g++)
                {
                    Console.Write(x[i, g] + " ");
                }
                Console.WriteLine();
            }
            
            return x;
        }


        static void Main(string[] args)
        {
            double[,] a = new double[4, 4] { { 10, 6, 2, 0 }, 
                                             { 5, 1, -2, 4 }, 
                                             { 3, 5, 1, -1 }, 
                                             { 0, 6, -2, 2 } };
            double[,] E = new double[4, 4];
            for(int i = 0; i < E.GetLength(0); i++)
            {
                for (int j = 0; j < E.GetLength(1); j++)
                {
                    if (i == j) E[i, j] = 1;
                    else E[i, j] = 0;
                }
            }

            double[,] B = new double[4, 1] { { 5 }, { 3 }, { 10 }, { 7 } };

            double[,] result = decomposition(a, E);
            
            Console.ReadKey();
        }
    }
}
