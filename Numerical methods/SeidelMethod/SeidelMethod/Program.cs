using System;

namespace SeidelMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            // Матрица коеффциентов
            double[,] matrix = new double[3, 3] {
            { 10, 1, 1},
            { 2, 10, 1},
            { 2, 2, 10}
            };
            // матрица свободных членов
            double[] additional = new double[3] {
                12,
                13,
                14
            };

            Seidel i = new Seidel(matrix, additional, 0.0001);
            i.calculateMatrix();
            i.showResult();
            Console.ReadKey();
        }
    }
}
