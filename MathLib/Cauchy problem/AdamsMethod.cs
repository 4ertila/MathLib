using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathLib.Objects;

namespace MathLib.CauchyProblem
{
    public abstract class AdamsMethod
    {
        public static double[] Approximate(Function f, double a, double b, double u0, int n)
        {
            double[] result = new double[n];
            double h;
            double delta;
            double y = 0;
            double[] yPrev = new double[4];
            double[] alpha = new double[] { 0.5, 0.5, 1 };
            double[] A = new double[] { (double)1 / 6, (double)1 / 3, (double)1 / 3, (double)1 / 6 };
            double[,] beta = new double[,] { { (double)1 / 2, 0, 0 }, { 0, (double)1 / 2, 0 }, { 0, 0, 1 } };
            double f1, f2, f3, f4;

            h = (b - a) / n;

            double[] startValues = RungeKuttaMethod.Approximate(f, 4, alpha, A, u0, beta, a, a + 3 * h, 3);
            yPrev[0] = u0;
            for (int i = 0; i < startValues.Length; i++)
            {
                yPrev[i + 1] = startValues[i];
                result[i] = startValues[i];
            }

            for (int i = 4; i <= n; i++)
            {
                f["x"] = a + (i - 4) * h;
                f["u1"] = yPrev[0];
                f1 = f.Calculate();

                f["x"] = a + (i - 3) * h;
                f["u1"] = yPrev[1];
                f2 = f.Calculate();

                f["x"] = a + (i - 2) * h;
                f["u1"] = yPrev[2];
                f3 = f.Calculate();

                f["x"] = a + (i - 1) * h;
                f["u1"] = yPrev[3];
                f4 = f.Calculate();

                y = yPrev[3] + h / 24 * (55 * f4 - 59 * f3 + 37 * f2 - 9 * f1);

                result[i - 1] = y;

                yPrev[0] = yPrev[1];
                yPrev[1] = yPrev[2];
                yPrev[2] = yPrev[3];
                yPrev[3] = y;
            }

            return result;
        }

        public static double Approximate(Function f, double a, double b, double u0, double eps)
        {
            int n = 50;
            double h;
            double Y;
            double delta;
            double y = 0;
            double[] yPrev = new double[4];
            double[] alpha = new double[] { 0.5, 0.5, 1 };
            double[] A = new double[] { (double)1 / 6, (double)1 / 3, (double)1 / 3, (double)1 / 6 };
            double[,] beta = new double[,] { { (double)1 / 2, 0, 0 }, { 0, (double)1 / 2, 0 }, { 0, 0, 1 } };
            double f1, f2, f3, f4;

            do
            {
                n *= 2;
                h = (b - a) / n;
                Y = y;

                double[] startValues = RungeKuttaMethod.Approximate(f, 4, alpha, A, u0, beta, a, a + 3 * h, 3);
                yPrev[0] = u0;
                for (int i = 0; i < startValues.Length; i++)
                {
                    yPrev[i + 1] = startValues[i];
                }

                for (int i = 4; i <= n; i++)
                {
                    f["x"] = a + (i - 4) * h;
                    f["u1"] = yPrev[0];
                    f1 = f.Calculate();

                    f["x"] = a + (i - 3) * h;
                    f["u1"] = yPrev[1];
                    f2 = f.Calculate();

                    f["x"] = a + (i - 2) * h;
                    f["u1"] = yPrev[2];
                    f3 = f.Calculate();

                    f["x"] = a + (i - 1) * h;
                    f["u1"] = yPrev[3];
                    f4 = f.Calculate();

                    y = yPrev[3] + h / 24 * (55 * f4 - 59 * f3 + 37 * f2 - 9 * f1);

                    yPrev[0] = yPrev[1];
                    yPrev[1] = yPrev[2];
                    yPrev[2] = yPrev[3];
                    yPrev[3] = y;
                }

                delta = (Y - y) / 15;
            }
            while (Math.Abs(delta) >= eps);

            return Y + delta;
        }
    }
}
