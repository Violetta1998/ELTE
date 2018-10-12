using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeidelMethod
{
    class Seidel
    {
        // Результирующая матрица
        private double[] resultMatrix;
        // Основная матрица и свободные члены
        private double[,] matrix;
        private double[] addtional;
        //кол-во итераций (точность)
        private double accuracy;

        public Seidel(double[,] Matrix, double[] FreeElements, double Accuracy)
        {
            this.matrix = Matrix;
            this.addtional = FreeElements;
            this.Accuracy = Accuracy;
        }

        public double[] getResultMatrix
        {
            get
            {
                if (resultMatrix != null)
                    return resultMatrix;
                else
                {
                    return new double[3] { 0, 0, 0 };
                }
            }
        }

       
        public double Accuracy
        {
            get
            {
                return accuracy;
            }
            set
            {
                if (value <= 0.0)
                    accuracy = 0.1;
                else
                    accuracy = value;
            }
        }

        public void calculateMatrix()
        {

            // общий вид:
            // [x1]   [ b1/a11 ]   / 0 x x \ 
            // [x2] = [ b2/a22 ] - | x 0 x |
            // [x3]   [ b3/a33 ]   \ x x 0 /
            // где x - делится на диагональый элемент первоначальной матрицы.
            // где b - эелементы из свободных членов
            // где а - элементы из матрицы

            // матрица коеффициентов + столбец свободных членов.
            double[,] a = new double[matrix.GetLength(0), matrix.GetLength(1) + 1];

            for (int i = 0; i < a.GetLength(0); i++) {
                for (int j = 0; j < a.GetLength(1) - 1; j++)
                {
                    a[i, j] = matrix[i, j];
                }
                    
            }

            for (int i = 0; i < a.GetLength(0); i++)
            {
                a[i, a.GetLength(1) - 1] = addtional[i];
            }

            double[] previousValues = new double[matrix.GetLength(0)];//хранит значение неизвестных на предыдущей итерации
            for (int i = 0; i < previousValues.GetLength(0); i++)
            {
                previousValues[i] = 0.0;
            }

            while (true)
            {
                // Введем вектор значений неизвестных на текущем шаге
                double[] currentValues = new double[a.GetLength(0)];

                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    currentValues[i] = a[i, a.GetLength(0)];

                    // Вычитаем сумму по всем отличным от i-ой неизвестным
                    for (int j = 0; j < a.GetLength(0); j++)
                    {
                        //значения, которые уже посчитаны на данной итерации
                        if (j < i)
                        {
                            currentValues[i] -= a[i, j] * currentValues[j];
                        }

                        //значения с прошлой итерации
                        if (j > i)
                        {
                            currentValues[i] -= a[i, j] * previousValues[j];
                        }
                    }
                    currentValues[i] /= a[i, i];
                }
                
                double differency = 0.0;

                for (int i = 0; i < a.GetLength(0); i++)
                {
                    differency += Math.Abs(currentValues[i] - previousValues[i]);
                }  

                //достигли точность?
                if (differency < accuracy)
                    break;

                previousValues = currentValues;
            }
            resultMatrix = previousValues;
        }

        public void showResult()
        {
            for (int i = 0; i < resultMatrix.Length; i++)
            {
                Console.WriteLine(" {0} ", resultMatrix[i]);
            }
        }
    }
}
