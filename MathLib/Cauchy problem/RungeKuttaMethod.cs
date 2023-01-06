using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathLib.Objects;
using static System.Math;

namespace MathLib.CauchyProblem
{
    public abstract class RungeKuttaMethod
    {
        public static double[] Approximate(Function f, int p, double[] alpha, double[] A, double u0,
                                         double[,] beta, double a, double b, int n)
        {
            double[] result = new double[n];
            double[] phi = new double[p];
            double y = u0;
            double xPrev = a;
            double yPrev;
            double h = (b - a) / n;

            for (int k = 1; k <= n; k++)
            {
                yPrev = y;

                f["x"] = xPrev;
                f["u1"] = yPrev;
                phi[0] = h * f.Calculate();
                y += A[0] * phi[0];
                for (int i = 1; i < p; i++)
                {
                    f["x"] = xPrev + alpha[i - 1] * h;
                    f["u1"] = yPrev;
                    for (int j = 0; j < i; j++)
                    {
                        f["u1"] += beta[i - 1, j] * phi[j];
                    }
                    phi[i] = h * f.Calculate();

                    y += A[i] * phi[i];
                }
                result[k - 1] = y;
                xPrev += h;
            }

            return result;
        }

        public static double Approximate(Function f, int p, double[] alpha, double[] A, double u0,
                                         double[,] beta, double a, double b, double eps)
        {
            int n = 50;
            double[] phi = new double[p];
            double y = 0;
            double yPrev;
            double h = (b - a) / n;
            double Y;
            double delta;

            do
            {
                n *= 2;
                Y = y;
                y = u0;
                h = (b - a) / n;

                for (int k = 1; k <= n; k++)
                {
                    yPrev = y;

                    f["x"] = a + k * h;
                    f["u1"] = yPrev;
                    phi[0] = h * f.Calculate();
                    y += A[0] * phi[0];
                    for (int i = 1; i < p; i++)
                    {
                        f["x"] = a + k * h + alpha[i - 1] * h;
                        f["u1"] = yPrev;
                        for (int j = 0; j < i; j++)
                        {
                            f["u1"] += beta[i - 1, j] * phi[j];
                        }
                        phi[i] = h * f.Calculate();

                        y += A[i] * phi[i];
                    }
                }

                delta = (Y - y) / (Pow(2, p) - 1);
            }
            while (Abs(delta) >= eps);

            return Y + delta;
        }

        public static Vector Approximate(VectorFunction f, int p, double[] alpha, double[] A, Vector u0,
                                         double[,] beta, double a, double b, double eps)
        {
            int n = 50;
            Vector[] phi = new Vector[p];
            Vector y = new Vector(f.dim);
            Vector yPrev;
            double h = (b - a) / n;
            Vector Y;
            Vector delta;
            Vector tVector;

            do
            {
                n *= 2;
                Y = y;
                y = u0;
                h = (b - a) / n;

                for (int k = 1; k <= n; k++)
                {
                    yPrev = y;

                    f["x"] = a + k * h;
                    for(int i = 0; i < f.dim; i++)
                    {
                        f[$"u{i + 1}"] = yPrev[i];
                    }
                    phi[0] = h * f.Calculate();

                    y += A[0] * phi[0];

                    for (int i = 1; i < p; i++)
                    {
                        f["x"] = a + k * h + alpha[i - 1] * h;
                        tVector = yPrev;
                        for (int j = 0; j < i; j++)
                        {
                            tVector += beta[i - 1, j] * phi[j];
                        }
                        for (int j = 0; j < f.dim; j++)
                        {
                            f[$"u{j + 1}"] = tVector[j];
                        }
                        phi[i] = h * f.Calculate();

                        y += A[i] * phi[i];
                    }
                }

                delta = (Y - y) * (1 / (Pow(2, p) - 1));
            }
            while (delta.Norm() >= eps);

            return Y + delta;
        }

        public static Vector[] Approximate(VectorFunction f, int p, double[] alpha, double[] A, Vector u0,
                                         double[,] beta, double a, double b, int n)
        {
            Vector[] phi = new Vector[p];
            Vector[] result = new Vector[n];
            Vector y = new Vector(f.dim);
            Vector yPrev;
            double h = (b - a) / n;
            Vector tVector;

            y = u0;
            h = (b - a) / n;

            for (int k = 1; k <= n; k++)
            {
                yPrev = y;

                f["x"] = a + k * h;
                for (int i = 0; i < f.dim; i++)
                {
                    f[$"u{i + 1}"] = yPrev[i];
                }
                phi[0] = h * f.Calculate();

                y += A[0] * phi[0];

                

                for (int i = 1; i < p; i++)
                {
                    f["x"] = a + k * h + alpha[i - 1] * h;
                    tVector = yPrev;
                    for (int j = 0; j < i; j++)
                    {
                        tVector += beta[i - 1, j] * phi[j];
                    }
                    for (int j = 0; j < f.dim; j++)
                    {
                        f[$"u{j + 1}"] = tVector[j];
                    }
                    phi[i] = h * f.Calculate();

                    y += A[i] * phi[i];
                }

                result[k - 1] = new Vector(y);
            }

            return result;
        }
    }
}
