using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace MathLib.SolvingNonlinearEquations
{
    public abstract class CombinedMethod
    {
        public delegate double Function(double x);

        public static double Solve(double a, double b, double eps, Function f, Function df,
                                bool positiveSecondDeriavative, bool positiveFirstDeriavative)
        {
            if (positiveSecondDeriavative && positiveFirstDeriavative)
            {
                double df0 = df(b);
                double y = b;
                double yPrev;
                double x = a;
                double xPrev;

                do
                {
                    yPrev = y;
                    xPrev = x;

                    x = xPrev - f(xPrev) * (yPrev - xPrev) / (f(yPrev) - f(xPrev));
                    y = yPrev - f(yPrev) / df0;
                }
                while (Abs(x - y) > eps);

                return x;
            }
            else if(!positiveFirstDeriavative && !positiveSecondDeriavative)
            {
                double df0 = df(b);
                double y = b;
                double yPrev;
                double x = a;
                double xPrev;

                do
                {
                    yPrev = y;
                    xPrev = x;

                    x = xPrev + f(xPrev) * (yPrev - xPrev) / (f(xPrev) - f(yPrev));
                    y = yPrev + f(yPrev) / df0;
                }
                while (Abs(x - y) > eps);

                return x;
            }
            else if(positiveFirstDeriavative && !positiveSecondDeriavative)
            {
                double df0 = df(b);
                double y = b;
                double yPrev;
                double x = a;
                double xPrev;

                do
                {
                    yPrev = y;
                    xPrev = x;

                    x = xPrev + f(-xPrev) * (yPrev - xPrev) / (f(-xPrev) - f(-yPrev));
                    y = yPrev + f(-yPrev) / df0;
                }
                while (Abs(x - y) > eps);

                return x;
            }
            else
            {
                double df0 = df(b);
                double y = b;
                double yPrev;
                double x = a;
                double xPrev;

                do
                {
                    yPrev = y;
                    xPrev = x;

                    x = xPrev - f(-xPrev) * (yPrev - xPrev) / (f(-yPrev) - f(-xPrev));
                    y = yPrev - f(-yPrev) / df0;
                }
                while (Abs(x - y) > eps);

                return x;
            }
        }
    }
}
