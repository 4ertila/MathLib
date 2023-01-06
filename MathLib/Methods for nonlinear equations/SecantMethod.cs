using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathLib.Objects;
using static System.Math;

namespace MathLib.SolvingNonlinearEquations
{
    public abstract class SecantMethod
    {
        public static double Solve(double a, double b, double eps, Function f, bool positiveSecondDeriavative)
        {
            Dictionary<string, double> variableA = new Dictionary<string, double>() { { "x", a } };
            Dictionary<string, double> variableB = new Dictionary<string, double>() { { "x", b } };
            Dictionary<string, double> xPrev = new Dictionary<string, double>() { { "x", 0 } };

            if (positiveSecondDeriavative)
            {
                double x = a;

                do
                {
                    xPrev["x"] = x;

                    x = x - f.Calculate(xPrev) * (b - x) / (f.Calculate(variableB) - f.Calculate(xPrev));
                }
                while (Abs(x - xPrev["x"]) > eps);

                return x;
            }
            else
            {
                double x = b;

                do
                {
                    xPrev["x"] = x;

                    x = x - f.Calculate(xPrev) * (x - a) / (f.Calculate(xPrev) - f.Calculate(variableA));
                }
                while (Abs(x - xPrev["x"]) > eps);

                return x;
            }
        }
    }
}
