using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;
using MathLib.Objects;

namespace MathLib.SolvingNonlinearEquations
{
    public abstract class TangentMethod
    {
        public delegate double Function(double x);

        public static double Solve(double a, double b, double eps, Function f, Function df)
        {
            double x = a - f(a) / df(0);
            double df0 = df(a);
            double xPrev;

            do
            {
                xPrev = x;
                x = xPrev - f(xPrev) / df0;
            }
            while (Abs(x - xPrev) > eps);

            return x;
        }
    }
}
