using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace methodOfRotationForEigenvalues
{
    public class Jacobi
    {

        public void JacobiEigenValVec(double[,] a, int maxsize, int n, double epsilon, out double[,] eigenval, out double[,] eigenvec)
        {
            int i, j, p, q, flag;
            double[,] d = new double[maxsize, maxsize];
            double[,] s = new double[maxsize, maxsize];
            double[,] sl = new double[maxsize, maxsize];
            double[,] slt = new double[maxsize, maxsize];
            double[,] temp = new double[maxsize, maxsize];
            double theta, max;

            //Initialization of matrix d and s
            for (i = 1; i <= n; i++)
            {
                for (j = 1; j <= n; j++)
                {
                    d[i, j] = a[i, j];
                    s[i, j] = 0.0;
                }
            }
            for (i = 1; i <= n; i++) s[i, i] = 1.0;

            do
            {
                flag = 0;

                //find largest off-diagonal element
                i = 1; j = 2;
                max = Math.Abs(d[1, 2]);
                for (p = 1; p <= n; p++)
                {
                    for (q = 1; q <= n; q++)
                    {
                        if (p != q)
                        {
                            if (max < Math.Abs(d[p, q]))
                            {
                                max = Math.Abs(d[p, q]);
                                i = p;
                                j = q;
                            }
                        }
                    }
                }

                if (d[i, i] == d[j, j])
                {
                    if (d[i, i] > 0) theta = Math.PI / 4.0;
                    else theta = -Math.PI / 4.0;
                }
                else
                {
                    theta = 0.5 * Math.Atan(2.0 * d[i, j] / (d[i, i] - d[j, j]));
                }

                //Construction of the matrix sl slt
                for (p = 1; p <= n; p++)
                {
                    for (q = 1; q <= n; q++)
                    {
                        sl[p, q] = 0.0;
                        slt[p, q] = 0.0;
                    }
                }

                for (p = 1; p <= n; p++)
                {
                    sl[p, p] = 1.0;
                    slt[p, p] = 1.0;
                }

                sl[i, i] = Math.Cos(theta); sl[j, j] = sl[i, i];

                sl[j, i] = Math.Sin(theta); sl[i, j] = -sl[j, i];

                slt[i, i] = sl[i, i]; slt[j, j] = sl[j, j];
                slt[i, j] = sl[j, i]; slt[j, i] = sl[i, j];

                //Product of slt and d
                for (i = 1; i <= n; i++)
                {
                    for (j = 1; j <= n; j++)
                    {
                        temp[i, j] = 0.0;
                        for (p = 1; p <= n; p++)
                        {
                            temp[i, j] += slt[i, p] * d[p, j];
                        }
                    }
                }

                //Product of temp and sl: d = slt*d*sl
                for (i = 1; i <= n; i++)
                {
                    for (j = 1; j <= n; j++)
                    {
                        d[i, j] = 0.0;
                        for (p = 1; p <= n; p++)
                        {
                            d[i, j] += temp[i, p] * sl[p, j];
                        }
                    }
                }

                //Product of s and sl
                for (i = 1; i <= n; i++)
                {
                    for (j = 1; j <= n; j++)
                    {
                        temp[i, j] = 0.0;
                        for (p = 1; p <= n; p++)
                        {
                            temp[i, j] += s[i, p] * sl[p, j];
                        }
                    }
                }

                //Product of slt and d
                for (i = 1; i <= n; i++)
                {
                    for (j = 1; j <= n; j++)
                    {
                        s[i, j] = temp[i, j];
                    }
                }

                //check if d is diagonal matrix
                //Product of slt and d
                for (i = 1; i <= n; i++)
                {
                    for (j = 1; j <= n; j++)
                    {
                        if (i != j)
                        {
                            if (Math.Abs(d[i, j]) > epsilon)
                            {
                                flag = 1;
                            }
                        }
                    }
                }

            } while (flag == 1);

            eigenval = d;
            eigenvec = s;
        }

        public void testJacobi()
        {
            int nCol = 4;
            int maxMatrixSize = 10;
            double Epsilon = 1.0e-04;
            double[,] A = new double[maxMatrixSize, maxMatrixSize];
            double[,] eigenValues = new double[maxMatrixSize, maxMatrixSize];
            double[,] eigenVectors = new double[maxMatrixSize, maxMatrixSize];


            A[1, 1] = 1; A[1, 2] = 2; A[1, 3] = 3; A[1, 4] = 4;
            A[2, 1] = 2; A[2, 2] = -3; A[2, 3] = 3; A[2, 4] = 4;
            A[3, 1] = 3; A[3, 2] = 3; A[3, 3] = 4; A[3, 4] = 5;
            A[4, 1] = 4; A[4, 2] = 4; A[4, 3] = 5; A[4, 4] = 0;

            for (int i = 1; i <= nCol; i++)
            {
                for (int j = 1; j <= nCol; j++)
                {
                    if (j != nCol)
                    {
                        Console.Write(A[i, j].ToString("0.000000") + "\t");
                    }
                    else Console.WriteLine(A[i, j].ToString("0.000000"));
                }

                JacobiEigenValVec(A, maxMatrixSize, nCol, Epsilon, out eigenValues, out eigenVectors);

                Console.WriteLine("Eigenvalues: ");
                for (i = 1; i <= nCol; i++)
                {
                    Console.WriteLine(eigenValues[i, i].ToString("0.000000"));
                }

                Console.WriteLine("Eigenvectors: ");
                for (int j = 1; j <= nCol; j++)
                {
                    for (i = 1; i <= nCol; i++)
                    {
                        if (i != nCol)
                        {
                            Console.Write(eigenVectors[i, j].ToString("0.000000") + "\t\t");
                        }
                        else Console.WriteLine(eigenVectors[i, j].ToString("0.000000"));
                    }
                }
            }
        }
    }
}
