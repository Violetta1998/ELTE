using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace methodOfRotation
{

    /*
     Решить СЛАУ с симметричной, положительно определённой матрицей методом вращений 
         */

    class Program
    {
        static double[,] rotationMethod(double [,]M, int row, int col)
        {
            double[,] newM = new double[row, col];

            double znamen = Math.Pow((Math.Pow(M[0, 0], 2) + Math.Pow(M[1, 0], 2)), 0.5);
            double c1 = M[0, 0] / znamen;
            double s1 = M[1, 0] / znamen;

            for (int i = 0; i < row - 1; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    newM[0, j] = c1 * M[0, j] + s1 * M[i + 1, j];//1ую строку умножаем на c1, вторую (третью) на s1 и складываем
                    newM[i + 1, j] = Math.Round(c1 * M[i + 1, j], 10) + Math.Round((-1 * s1) * M[0, j], 10);//1ую строку умножаем на -s1, вторую (третью) на c1 и складываем

                    for (int k = i + 1; k < row - 1; k++) // остальные строки те же
                    {
                        newM[i + 2, j] = M[i + 2, j];
                    }

                }

                for (int ss = 0; ss < col; ss++)
                {
                    M[0, ss] = newM[0, ss];//меняем 1 строку исходной матрицы
                }

                if (i < (row - 2))
                {
                    znamen = Math.Pow((Math.Pow(newM[0, 0], 2) + Math.Pow(M[i + 2, 0], 2)), 0.5);
                    c1 = newM[0, 0] / znamen;
                    s1 = M[i + 2, 0] / znamen;
                }
            }
            return newM;
        }

        static void Main(string[] args)
        {
            //double[,] M = new double[3, 4] { {2, -1, 0, 4 }, 
            //                                 { -1, 2, -1, 1}, 
            //                                 { 0, -1, 2, 5} };

            double[,] M = new double[4, 5] { {1, -2, 3, 4, 10 },//исходная матрица, последний столбец - столбец B
                                             { 2, 1, -4, 3, 2},
                                             { 3, -4, -1, -2, 7},
                                             { 4, 3, 2, -1, 6} };

            int row = M.GetLength(0);
            int col = M.GetLength(1);
            double[,] resM = new double[row, col];//результирующая матрица
           
            resM = rotationMethod(M, row, col);//1 шаг, после которого зануляются an1 элементы

            int step = 1;
            for (int s = 0; s < row - 1; s++)//повторяем процесс n-1 раз
            {
                double[,] tempM = new double[row - 1, col - 1];

                for (int j = 0; j < row - 1; j++)//заполняем матрицу меньшего размера
                {
                    for (int k = 0; k < col - 1; k++)
                    {
                        tempM[j, k] = resM[j + step, k + step];
                    }
                }

                tempM = rotationMethod(tempM, row - 1, col - 1);

                int countRow = M.GetLength(0) - row + 1;
                for (int j = 0; j < row - 1; j++)//заполняем результирующую матрицу новыми значениями
                {
                    int countCol = M.GetLength(1) - col + 1;
                    for (int k = 0; k < col - 1; k++)
                    {
                        resM[countRow, countCol] = tempM[j, k];
                        countCol++;
                    }
                    countRow++;
                }

                row--;
                col--;
                step++;
            }

            row = M.GetLength(0);
            col = M.GetLength(1);
            //метод обратного хода
            double[] resX = new double[col-1];
            for (int i = row-1; i >= 0; i--)
            {
                double s = 0;
                for(int j = i+1; j< row; j++)
                {
                    s += resM[i, j] * resX[j];
                }
                resX[i] = (resM[i, row] - s) / resM[i, i];
            }

            Console.WriteLine("Полученный результат: ");
            for (int i = 0; i < resX.Length; i++)
            {
                Console.WriteLine(resX[i]);
            }

            Console.ReadKey();
            
        }
    }
}
