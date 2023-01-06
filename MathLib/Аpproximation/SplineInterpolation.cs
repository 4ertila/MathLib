using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathLib.Objects;
using MathLib.SolvingLinearSystem;
using static System.Math;

namespace MathLib.Аpproximation
{
    public abstract class SplineInterpolation
    {
        public abstract class Type1
        {
            public static Polynomial[] Approximate(double[] x, double[] y)
            {
                int n = x.Length;
                double[,] M = new double[n - 1, n - 1];
                double[] b = new double[n - 1];
                for (int i = 1; i < n - 2; i++)
                {
                    M[i, i - 1] = (x[i] - x[i - 1]) / 6;
                    M[i, i] = (x[i + 1] - x[i - 1]) / 3;
                    M[i, i + 1] = (x[i + 1] - x[i]) / 6;
                }
                for (int i = 1; i < n - 1; i++)
                {
                    b[i - 1] = (y[i + 1] - y[i]) / (x[i + 1] - x[i]) -
                               (y[i] - y[i - 1]) / (x[i] - x[i - 1]);
                }
                b[n - 2] = (y[1] - y[n - 1]) / (x[1] - x[0]) -
                           (y[n - 1] - y[n - 2]) / (x[n - 1] - x[n - 2]);

                M[0, 0] = (x[2] - x[0]) / 3;
                M[0, 1] = (x[2] - x[1]) / 6;
                M[0, n - 2] = (x[1] - x[0]) / 6;
                M[n - 2, n - 3] = (x[n - 2] - x[n - 3]) / 6;
                M[n - 2, n - 2] = (x[n - 1] - x[n - 3]) / 3;
                M[n - 2, 0] = (x[1] - x[0]) / 6;

                double[] s = new double[n];
                double[] tArr = GaussMethod.Solve(M, b);
                for (int i = 0; i < tArr.Length; i++)
                {
                    s[i + 1] = tArr[i];
                }
                s[0] = s[n - 1];

                tArr = new double[4];
                Polynomial[] result = new Polynomial[n - 1];
                double k0, k1, k2, k3;
                double h;
                for (int i = 0; i < n - 1; i++)
                {
                    h = x[i + 1] - x[i];

                    k0 = -s[i] / (6 * h);
                    k1 = s[i + 1] / (6 * h);
                    k2 = (y[i + 1] - y[i]) / h - h * (s[i + 1] - s[i]) / 6;
                    k3 = y[i] - s[i] * Pow(h, 2) / 6;

                    tArr[3] = k0 + k1;
                    tArr[2] = -3 * x[i + 1] * k0 - 3 * x[i] * k1;
                    tArr[1] = 3 * Pow(x[i + 1], 2) * k0 + 3 * Pow(x[i], 2) * k1 + k2;
                    tArr[0] = -k0 * Pow(x[i + 1], 3) - k1 * Pow(x[i], 3) - k2 * x[i] + k3;
                    result[i] = new Polynomial(tArr);
                }

                return result;
            }

            public static Polynomial[] Approximate(double[] x, Function f)
            {
                int n = x.Length;
                double[] y = new double[n];
                for(int i = 0; i < n; i++)
                {
                    f["x"] = x[i];
                    y[i] = f.Calculate();
                }

                return Approximate(x, y);
            }
        }

        public abstract class Type2
        {
            public static Polynomial[] Approximate(double[] x, Function f, double A, double B)
            {
                int n = x.Length;
                double[] y = new double[n];
                for(int i = 0; i < n; i++)
                {
                    f["x"] = x[i];
                    y[i] = f.Calculate();
                }

                return Approximate(x, y, A, B);
            }

            public static Polynomial[] Approximate(double[] x, double[] y, double A, double B)
            {
                int n = x.Length;
                double[,] M = new double[n - 2, n - 2];
                double[] b = new double[n - 2];
                for (int i = 1; i < n - 3; i++)
                {
                    M[i, i - 1] = (x[i] - x[i - 1]) / 6;
                    M[i, i] = (x[i + 1] - x[i - 1]) / 3;
                    M[i, i + 1] = (x[i + 1] - x[i]) / 6;
                }
                for (int i = 1; i < n - 1; i++)
                {
                    b[i - 1] = (y[i + 1] - y[i]) / (x[i + 1] - x[i]) -
                               (y[i] - y[i - 1]) / (x[i] - x[i - 1]);
                }
                b[0] -= 3 * ((y[1] - y[0]) / (x[1] - x[0]) - A) / (x[1] - x[0]);
                b[n - 3] -= 3 * (B - (y[n - 1] - y[n - 2]) / (x[n - 1] - x[n - 2])) / (x[n - 1] - x[n - 2]);

                M[0, 0] = (x[2] - x[0]) / 3 - 1 / 2;
                M[0, 1] = (x[2] - x[1]) / 6;
                M[n - 3, n - 4] = (x[n - 2] - x[n - 3]) / 6;
                M[n - 3, n - 3] = (x[n - 1] - x[n - 3]) / 3 - 1 / 2;

                double[] s = new double[n];
                double[] tArr = SweepMethod.Solve(M, b);
                for (int i = 0; i < tArr.Length; i++)
                {
                    s[i + 1] = tArr[i];
                }
                s[0] = 3 * ((y[1] - y[0]) / (x[1] - x[0]) - A) / (x[1] - x[0]) - s[1] / 2;
                s[n - 1] = 3 * (B - (y[n - 1] - y[n - 2]) / (x[n - 1] - x[n - 2])) / (x[n - 1] - x[n - 2]) - s[n - 2] / 2;

                Polynomial[] result = new Polynomial[n - 1];
                tArr = new double[4];
                double k0, k1, k2, k3;
                double h;
                for (int i = 0; i < n - 1; i++)
                {
                    h = x[i + 1] - x[i];

                    k0 = -s[i] / (6 * h);
                    k1 = s[i + 1] / (6 * h);
                    k2 = (y[i + 1] - y[i]) / h - h * (s[i + 1] - s[i]) / 6;
                    k3 = y[i] - s[i] * Pow(h, 2) / 6;

                    tArr[3] = k0 + k1;
                    tArr[2] = -3 * x[i + 1] * k0 - 3 * x[i] * k1;
                    tArr[1] = 3 * Pow(x[i + 1], 2) * k0 + 3 * Pow(x[i], 2) * k1 + k2;
                    tArr[0] = -k0 * Pow(x[i + 1], 3) - k1 * Pow(x[i], 3) - k2 * x[i] + k3;
                    result[i] = new Polynomial(tArr);
                }

                return result;
            }
        }

        public abstract class Type3
        {
            public static Polynomial[] Approximate(double[] x, Function f, double A, double B)
            {
                int n = x.Length;
                double[] y = new double[n];
                for (int i = 0; i < n; i++)
                {
                    f["x"] = x[i];
                    y[i] = f.Calculate();
                }

                return Approximate(x, y, A, B);
            }

            public static Polynomial[] Approximate(double[] x, double[] y, double A, double B)
            {
                int n = x.Length;
                double[,] M = new double[n - 2, n - 2];
                double[] b = new double[n - 2];
                for (int i = 1; i < n - 3; i++)
                {
                    M[i, i - 1] = (x[i] - x[i - 1]) / 6;
                    M[i, i] = (x[i + 1] - x[i - 1]) / 3;
                    M[i, i + 1] = (x[i + 1] - x[i]) / 6;
                }
                for (int i = 1; i < n - 1; i++)
                {
                    b[i - 1] = (y[i + 1] - y[i]) / (x[i + 1] - x[i]) -
                               (y[i] - y[i - 1]) / (x[i] - x[i - 1]);
                }
                b[0] -= A * (x[1] - x[0]) / 6;
                b[n - 3] -= B * (x[n - 1] - x[n - 2]) / 6;

                M[0, 0] = (x[2] - x[0]) / 3;
                M[0, 1] = (x[2] - x[1]) / 6;
                M[n - 3, n - 4] = (x[n - 2] - x[n - 3]) / 6;
                M[n - 3, n - 3] = (x[n - 1] - x[n - 3]) / 3;

                double[] s = new double[n];
                double[] tArr = SweepMethod.Solve(M, b);
                for (int i = 0; i < tArr.Length; i++)
                {
                    s[i + 1] = tArr[i];
                }
                s[0] = A;
                s[n - 1] = B;

                Polynomial[] result = new Polynomial[n - 1];
                tArr = new double[4];
                double k0, k1, k2, k3;
                double h;
                for (int i = 0; i < n - 1; i++)
                {
                    h = x[i + 1] - x[i];

                    k0 = -s[i] / (6 * h);
                    k1 = s[i + 1] / (6 * h);
                    k2 = (y[i + 1] - y[i]) / h - h * (s[i + 1] - s[i]) / 6;
                    k3 = y[i] - s[i] * Pow(h, 2) / 6;

                    tArr[3] = k0 + k1;
                    tArr[2] = -3 * x[i + 1] * k0 - 3 * x[i] * k1;
                    tArr[1] = 3 * Pow(x[i + 1], 2) * k0 + 3 * Pow(x[i], 2) * k1 + k2;
                    tArr[0] = -k0 * Pow(x[i + 1], 3) - k1 * Pow(x[i], 3) - k2 * x[i] + k3;
                    result[i] = new Polynomial(tArr);
                }

                return result;
            }
        }

        public abstract class Type4
        {
            public static Polynomial[] Approximate(double[] x, Function f)
            {
                int n = x.Length;
                double[] y = new double[n];
                for(int i = 0; i < n; i++)
                {
                    f["x"] = x[i];
                    y[i] = f.Calculate();
                }

                return Approximate(x, y);
            }

            public static Polynomial[] Approximate(double[] x, double[] y)
            {
                int n = x.Length;
                double[,] M = new double[n - 2, n - 2];
                double[] b = new double[n - 2];
                for (int i = 1; i < n - 3; i++)
                {
                    M[i, i - 1] = (x[i] - x[i - 1]) / 6;
                    M[i, i] = (x[i + 1] - x[i - 1]) / 3;
                    M[i, i + 1] = (x[i + 1] - x[i]) / 6;
                }
                for (int i = 1; i < n - 1; i++)
                {
                    b[i - 1] = (y[i + 1] - y[i]) / (x[i + 1] - x[i]) -
                               (y[i] - y[i - 1]) / (x[i] - x[i - 1]);
                }

                M[0, 0] = (x[2] - x[0]) / 3 + 1 + (x[1] - x[0]) / (x[2] - x[1]);
                M[0, 1] = (x[2] - x[1]) / 6 - (x[1] - x[0]) / (x[2] - x[1]);
                M[n - 3, n - 4] = (x[n - 2] - x[n - 3]) / 6 - (x[n - 1] - x[n - 2]) / (x[n - 2] - x[n - 3]);
                M[n - 3, n - 3] = (x[n - 1] - x[n - 3]) / 3 + 1 + (x[n - 1] - x[n - 2]) / (x[n - 2] - x[n - 3]);

                double[] s = new double[n];
                double[] tArr = SweepMethod.Solve(M, b);
                for (int i = 0; i < tArr.Length; i++)
                {
                    s[i + 1] = tArr[i];
                }
                s[0] = (1 + (x[1] - x[0]) / (x[2] - x[1])) * s[1] - (x[1] - x[0]) / (x[2] - x[1]) * s[2];
                s[n - 1] = (1 + (x[n - 1] - x[n - 2]) / (x[n - 2] - x[n - 3])) * s[n - 2] - (x[n - 1] - x[n - 2]) / (x[n - 2] - x[n - 3]) * s[n - 3];

                Polynomial[] result = new Polynomial[n - 1];
                tArr = new double[4];
                double k0, k1, k2, k3;
                double h;
                for (int i = 0; i < n - 1; i++)
                {
                    h = x[i + 1] - x[i];

                    k0 = -s[i] / (6 * h);
                    k1 = s[i + 1] / (6 * h);
                    k2 = (y[i + 1] - y[i]) / h - h * (s[i + 1] - s[i]) / 6;
                    k3 = y[i] - s[i] * Pow(h, 2) / 6;

                    tArr[3] = k0 + k1;
                    tArr[2] = -3 * x[i + 1] * k0 - 3 * x[i] * k1;
                    tArr[1] = 3 * Pow(x[i + 1], 2) * k0 + 3 * Pow(x[i], 2) * k1 + k2;
                    tArr[0] = -k0 * Pow(x[i + 1], 3) - k1 * Pow(x[i], 3) - k2 * x[i] + k3;
                    result[i] = new Polynomial(tArr);
                }

                return result;
            }
        }
    }
}
