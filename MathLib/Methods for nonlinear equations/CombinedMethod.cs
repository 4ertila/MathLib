using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathLib.Objects;
using static System.Math;

namespace MathLib.SolvingNonlinearEquations
{
    public abstract class CombinedMethod
    {
        public static double Solve(double a, double b, double eps, Function f, Function df,
                                bool positiveSecondDeriavative, bool positiveFirstDeriavative)
        {
            Dictionary<string, double> xPrev = new Dictionary<string, double>() { { "x", 0 } };
            Dictionary<string, double> yPrev = new Dictionary<string, double>() { { "x", 0 } };

            if (positiveSecondDeriavative && positiveFirstDeriavative)
            {
                double df0 = df.Calculate(new Dictionary<string, double>() { { "x", b } });
                double y = b;
                double x = a;

                do
                {
                    yPrev["x"] = y;
                    xPrev["x"] = x;

                    x = x - f.Calculate(xPrev) * (y - x) / (f.Calculate(yPrev) - f.Calculate(xPrev));
                    y = y - f.Calculate(xPrev) / df0;
                }
                while (Abs(x - y) > eps);

                return x;
            }
            else if(!positiveFirstDeriavative && !positiveSecondDeriavative)
            {
                double df0 = df.Calculate(new Dictionary<string, double>() { { "x", b } });
                double y = b;
                double x = a;

                do
                {
                    yPrev["x"] = y;
                    xPrev["x"] = x;

                    x = x + f.Calculate(xPrev) * (y - x) / (f.Calculate(xPrev) - f.Calculate(yPrev));
                    y = y + f.Calculate(yPrev) / df0;
                }
                while (Abs(x - y) > eps);

                return x;
            }
            else if(positiveFirstDeriavative && !positiveSecondDeriavative)
            {
                double df0 = df.Calculate(new Dictionary<string, double>() { { "x", b } });
                double y = b;
                double x = a;

                do
                {
                    yPrev["x"] = -y;
                    xPrev["x"] = -x;

                    x = x + f.Calculate(xPrev) * (y - x) / (f.Calculate(xPrev) - f.Calculate(yPrev));
                    y = y + f.Calculate(yPrev) / df0;
                }
                while (Abs(x - y) > eps);

                return x;
            }
            else
            {
                double df0 = df.Calculate(new Dictionary<string, double>() { { "x", b } });
                double y = b;
                double x = a;

                do
                {
                    yPrev["x"] = -y;
                    xPrev["x"] = -x;

                    x = x - f.Calculate(xPrev) * (y - x) / (f.Calculate(yPrev) - f.Calculate(xPrev));
                    y = y - f.Calculate(yPrev) / df0;
                }
                while (Abs(x - y) > eps);

                return x;
            }
        }
    }
}
