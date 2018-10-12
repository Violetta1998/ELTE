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

        public static double[,] multiplyMatrix(double[,] A, double[,] B)
        {

            if (A.GetLength(1) != B.GetLength(0)) throw new Exception("Матрицы нельзя перемножить");

            int ma = A.GetLength(0);
            int mb = B.GetLength(0);
            int nb = B.GetLength(1);

            double[,] resultMatrix = new double[ma, nb];

            for (int i = 0; i < A.GetLength(0); i++)
            {
                for (int j = 0; j < nb; j++)
                {
                    for (int k = 0; k < mb; k++)
                    {
                        resultMatrix[i, j] += A[i, k] * B[k, j];
                    }
                }
            }
            return resultMatrix;
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
            double[] y = new double[a.GetLength(0)];
            double[] d = extractColumn(E, count);
            double[] x = new double[a.GetLength(0)];
            double[,] result = new double[a.GetLength(0), a.GetLength(0)];

            while (count < a.GetLength(0))
            {
                d = extractColumn(E, count);
                y[0] = d[0];
                for (j = 1; j < MaxOrder; j++)
                {
                    y[j] = d[j];
                    for (i = 0; i < j; i++)
                    {
                        y[j] = y[j] - y[i] * l[j, i];
                    }
                }
                x[MaxOrder - 1] = y[MaxOrder - 1] / u[MaxOrder - 1, MaxOrder - 1];
                for (j = MaxOrder - 2; j >= 0; j--)
                {
                    x[j] = y[j];
                    for (i = MaxOrder - 1; i > j; i--)
                    {
                        x[j] = x[j] - x[i] * u[j, i];
                    }
                    x[j] = x[j] / u[j, j];
                 }
                for (int g = 0; g < x.Length; g++)
                {
                    result[g, count] = x[g];
                }
                count++;
               
            }
          
            return result;
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

            double[,] obrMatrix = decomposition(a, E);
            for(int i = 0; i < obrMatrix.GetLength(0); i++)
            {
                for(int j = 0; j < obrMatrix.GetLength(1); j++)
                {
                    Console.Write(obrMatrix[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("Результат: ");
            double[,] x = multiplyMatrix(obrMatrix, B);
            for (int i = 0; i < x.GetLength(0); i++)
            {
                for (int j = 0; j < x.GetLength(1); j++)
                {
                    Console.Write(x[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }
    }
}
