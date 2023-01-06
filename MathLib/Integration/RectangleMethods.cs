using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathLib.Objects;
using static System.Math;

namespace MathLib.Integration
{
    public abstract class RectangleMethods
    {
        public abstract class Left
        {
            public static double Approximate(double a, double b, Function f, double eps)
            {
                int n = 50;
                double h;
                double result = 0;
                double resultPrev = 0;
                double delta;

                do
                {
                    n *= 2;
                    h = (b - a) / n;

                    resultPrev = result;
                    result = 0;

                    for(int i = 0; i < n; i++)
                    {
                        f["x"] = a + h * i;
                        result += f.Calculate();
                    }
                    result *= h;

                    delta = resultPrev - result;
                }
                while (Abs(delta) >= eps);

                return resultPrev + delta;
            }
        }

        public abstract class Right
        {
            public static double Approximate(double a, double b, Function f, double eps)
            {
                int n = 50;
                double h;
                double result = 0;
                double resultPrev = 0;
                double delta;

                do
                {
                    n *= 2;
                    h = (b - a) / n;

                    resultPrev = result;
                    result = 0;

                    for (int i = 1; i <= n; i++)
                    {
                        f["x"] = a + h * i;
                        result += f.Calculate();
                    }
                    result *= h;

                    delta = resultPrev - result;
                }
                while (Abs(delta) >= eps);

                return resultPrev + delta;
            }
        }

        public abstract class Medium
        {
            public static double Approximate(double a, double b, Function f, double eps)
            {
                int n = 50;
                double h;
                double result = 0;
                double resultPrev = 0;
                double delta;

                do
                {
                    n *= 2;
                    h = (b - a) / n;

                    resultPrev = result;
                    result = 0;

                    for (int i = 0; i < n; i++)
                    {
                        f["x"] = a + h * i + h / 2;
                        result += f.Calculate();
                    }
                    result *= h;

                    delta = (resultPrev - result) / 3;
                }
                while (Abs(delta) >= eps);

                return resultPrev + delta;
            }
        }
    }
}
