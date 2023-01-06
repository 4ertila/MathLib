using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathLib.Objects;
using static System.Math;

namespace MathLib.CauchyProblem
{
    public abstract class SecondOrderScheme
    {
        public static double[] Approximate(double beta, Function f, double u0, double a, double b, int n)
        {
            double h = (b - a) / n;
            double y = u0;
            double yPrev;
            double xPrev = a;
            double[] result = new double[n];
            double fValue;

            for (int i = 1; i <= n; i++)
            {
                yPrev = y;

                f["x"] = xPrev;
                f["u1"] = yPrev;

                fValue = f.Calculate();

                f["x"] = xPrev + h / (2 * beta);
                f["u1"] = yPrev + h / (2 * beta) * fValue;

                y = yPrev + h * ((1 - beta) * fValue + beta * f.Calculate());
                result[i - 1] = y;

                xPrev += h;
            }

            return result;
        }

        public static double Approximate(double beta, Function f, double u0, double a, double b, double eps)
        {
            int n = 50;
            double h;
            double y = 0;
            double yPrev;
            double xPrev;
            double Y = 0;
            double delta;

            do
            {
                n *= 2;
                h = (b - a) / n;
                Y = y;
                y = u0;
                xPrev = a;

                for (int i = 1; i <= n; i++)
                {
                    yPrev = y;

                    f["x"] = xPrev;
                    f["u1"] = yPrev;

                    double fValue = f.Calculate();

                    f["x"] = xPrev + h / (2 * beta);
                    f["u1"] = yPrev + h / (2 * beta) * fValue;

                    y = yPrev + h * ((1 - beta) * fValue + beta * f.Calculate());

                    xPrev += h;
                }

                delta = (Y - y) / 3;
            }
            while (Abs(delta) >= eps);

            return Y + delta;
        }

        public static Vector Approximate(double beta, VectorFunction f, Vector u0, double a, double b, double eps)
        {
            int n = 50;
            double h;
            Vector delta;
            Vector y = new Vector(u0);
            Vector Y = new Vector();
            Vector yPrev = new Vector(f.dim);

            do
            {
                n *= 2;
                h = (b - a) / n;

                Y.Init(y);

                y.Init(u0);

                for (int i = 1; i <= n; i++)
                {
                    yPrev = y;

                    for(int j = 0; j < f.dim; j++)
                    {
                        f[$"u{j + 1}"] = yPrev[j];
                    }
                    f["x"] = a + (i - 1) * h;

                    Vector fValue = f.Calculate();

                    for (int j = 0; j < f.dim; j++)
                    {
                        f[$"u{j + 1}"] = yPrev[j] + h / (2 * beta) * fValue[j];
                    }
                    f["x"] = a + (i - 1) * h + h / (2 * beta);


                    y = yPrev + h * ((1 - beta) * fValue + beta * f.Calculate());
                }

                delta = (Y - y) * ((double)1 / 3);
            }
            while (delta.Norm() >= eps);

            return y;
        }

        public static Vector[] Approximate(double beta, VectorFunction f, Vector u0, double a, double b, int n)
        {
            double h;
            Vector y = new Vector(u0);
            Vector yPrev = new Vector(f.dim);
            Vector[] result = new Vector[n];

            h = (b - a) / n;
            y.Init(u0);

            for (int i = 1; i <= n; i++)
            {
                yPrev = y;

                for (int j = 0; j < f.dim; j++)
                {
                    f[$"u{j + 1}"] = yPrev[j];
                }
                f["x"] = a + (i - 1) * h;

                Vector fValue = f.Calculate();

                for (int j = 0; j < f.dim; j++)
                {
                    f[$"u{j + 1}"] = yPrev[j] + h / (2 * beta) * fValue[j];
                }
                f["x"] = a + (i - 1) * h + h / (2 * beta);


                y = yPrev + h * ((1 - beta) * fValue + beta * f.Calculate());

                result[i - 1] = new Vector(y);
            }

            return result;
        }
    }
}
