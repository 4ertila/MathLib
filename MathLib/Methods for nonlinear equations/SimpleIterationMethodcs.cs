using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace MathLib.SolvingNonlinearEquations
{
    public abstract class SimpleIterationMethod
    {
        public delegate double Function(double x);

        public static double Solve(double a, double b, double eps, Function phi)
        {
            double xPrev;
            double x = a;
            do
            {
                xPrev = x;
                x = phi(xPrev);
            }
            while (Abs(x - xPrev) > eps);

            return x;
        }
    }
}
