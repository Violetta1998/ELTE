using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QR_decomposition
{
    public class Decomposition
    {
        double[,] matrixA;
        public int row;
        public int col;

        public Decomposition(double[,] matrixA)
        {
            this.matrixA = matrixA;
            this.row = matrixA.GetLength(0);
            this.col = matrixA.GetLength(1);
        }

        public double euclideanNorm(double[,] matrix)
        {
            double sum = 0.0;
            int m = matrix.GetLength(0);
            int n = matrix.GetLength(1);
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    sum += Math.Pow(matrix[i, j], 2);
                }
            }
            sum = Math.Pow(sum, 0.5);
            return sum;
        }

        public double[,] multiplyMatrix(double[,] A, double[,] B)
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

        public double[,] multiplyMatrixOnNumber(double number, double[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = matrix[i, j] * number;
                }
            }
            return matrix;
        }

        public double scalarMultiplication(double[,] matrixQ, double[,] a)
        {
            double res = 0.0;
            for (int i = 0; i < matrixQ.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    res = res + matrixQ[0, j] * a[i, 0];
                }
            }
            return res;
        }

        public double[,] addMatrix(double[,] A, double[,] B)
        {
            double[,] C = new double[A.GetLength(0), A.GetLength(1)];
            for (int i = 0; i < A.GetLength(0); i++)
            {
                for (int j = 0; j < A.GetLength(1); j++)
                {
                    C[i, j] = A[i, j] + B[i, j];
                }
            }
            return C;
        }

        public double[,] substractMatrix(double[,] A, double[,] B)
        {
            double[,] C = new double[A.GetLength(0), A.GetLength(1)];
            for (int i = 0; i < A.GetLength(0); i++)
            {
                for (int j = 0; j < A.GetLength(1); j++)
                {
                    C[i, j] = A[i, j] - B[i, j];
                }
            }
            return C;
        }

        public double[,] extractColumn(double[,] matrix, int number)
        {
            int row = matrix.GetLength(0);
            double[,] a = new double[row, 1];
            for (int i = 0; i < row; i++)
            {
                a[i, 0] = matrix[i, number];
            }
            return a;
        }

        public double[,] transposeMatrix(double[,] matrix)
        {
            double[,] newMatrix = new double[matrix.GetLength(1), matrix.GetLength(0)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    newMatrix[j, i] = matrix[i, j];
                }
            }
            return newMatrix;
        }

        public double[,] gramSchmidtProcess(int col, int row, double[,] matrixA)
        {
            double[,] matrixR = new double[col, col];
            double[,] matrixQ = new double[row, col];


            double[,] a = extractColumn(matrixA, 0);
            matrixR[0, 0] = euclideanNorm(a);
            a = multiplyMatrixOnNumber(1 / matrixR[0, 0], a);

            for (int i = 0; i < row; i++)
            {
                matrixQ[i, 0] = a[i, 0];
               // Console.WriteLine(matrixQ[i, 0]);
            }


            double[,] q;
            for (int j = 1; j < col; j++)
            {
                for (int i = 0; i < row; i++)
                {
                    if (i == j)//главная диагональ
                    {
                        int num = 0;
                        double[,] result = new double[matrixQ.GetLength(0), 1];
                        while (num < i)
                        {
                            q = extractColumn(matrixQ, num);
                            result = addMatrix(multiplyMatrixOnNumber(matrixR[num, i], q), result);
                            num++;
                        }
                        a = extractColumn(matrixA, i);
                        result = substractMatrix(a, result);
                        matrixR[i, i] = euclideanNorm(result);
                        a = multiplyMatrixOnNumber(1 / matrixR[i, i], result);

                        for (int s = 0; s < row; s++)
                        {
                            matrixQ[s, i] = a[s, 0];
                        }
                    }
                    if (i > j && i < matrixR.GetLength(0))//так как снизу все нули
                    {
                        matrixR[j, i] = 0;
                    }
                    if (i < j)
                    {
                        a = extractColumn(matrixA, j);
                        q = extractColumn(matrixQ, i);
                        matrixR[i, j] = scalarMultiplication(q, a);
                    }
                    else { continue; }
                }
            }

            for(int i = 0; i < matrixQ.GetLength(0); i++)
            {
                for(int j = 0; j < matrixQ.GetLength(1); j++)
                {
                    Console.Write(matrixQ[i, j] + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine();

            for (int i = 0; i < matrixR.GetLength(0); i++)
            {
                for (int j = 0; j < matrixR.GetLength(1); j++)
                {
                    Console.Write(matrixR[i, j] + " ");
                }
                Console.WriteLine();
            }

            return matrixR;
        }
    }
}
