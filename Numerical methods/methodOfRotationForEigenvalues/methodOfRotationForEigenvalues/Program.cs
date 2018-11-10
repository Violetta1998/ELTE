using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace methodOfRotationForEigenvalues
{
    class Program
    {
        static void Main(string[] args)
        {
            Jacobi jacobi = new Jacobi();
            jacobi.testJacobi();
            Console.ReadKey();


        }

    }
}
