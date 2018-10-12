using System;

namespace QR_decomposition
{

    class Program
    {

        static void Main(string[] args)
        {
            double[,] matrix = new double[4, 3] { { 1, -1, 4},
                                                  { 1, 4, -2},
                                                  { 1, 4, 2 },
                                                  { 1, -1, 0} };
            double[,] matrixB = new double[4, 1] { {10},
                                                  { 5},
                                                  { -6 },
                                                  { 15} };

            Decomposition decompose = new Decomposition(matrix);
            double[] res = decompose.gramSchmidtProcess(decompose.col, decompose.row, matrix, matrixB);

            Console.ReadKey();
        }
    }
}
