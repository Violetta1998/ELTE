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
           
            Decomposition decompose = new Decomposition(matrix);
            double[,] matrixR = decompose.gramSchmidtProcess(decompose.col, decompose.row, matrix);

            Console.ReadKey();
        }
    }
}
