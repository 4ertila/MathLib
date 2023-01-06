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
        public static double Solve(double a, double b, double eps, Function f, Function df)
        {
            Dictionary<string, double> variableA = new Dictionary<string, double>() { { "x", a } };
            Dictionary<string, double> xPrev = new Dictionary<string, double>() { { "x", a } };

            double x = a - f.Calculate(variableA) / df.Calculate(variableA);
            double df0 = df.Calculate(variableA);

            do
            {
                xPrev["x"] = x;

                x = x - f.Calculate(xPrev) / df.Calculate(xPrev);
            }
            while (Abs(x - xPrev["x"]) > eps);

            return x;
        }
    }
}
