using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace MathLib.SolvingNonlinearEquations
{
    public abstract class SecantMethod
    {
        public delegate double Function(double x);

        public static double Solve(double a, double b, double eps, Function f, bool positiveSecondDeriavative)
        {
            if (positiveSecondDeriavative)
            {
                double x = a;
                double xPrev;

                do
                {
                    xPrev = x;
                    x = xPrev - f(xPrev) * (b - xPrev) / (f(b) - f(xPrev));
                }
                while (Abs(x - xPrev) > eps);

                return x;
            }
            else
            {
                double x = b;
                double xPrev;

                do
                {
                    xPrev = x;
                    x = xPrev - f(xPrev) * (xPrev - a) / (f(xPrev) - f(a));
                }
                while (Abs(x - xPrev) > eps);

                return x;
            }

        }
    }
}
