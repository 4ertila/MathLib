using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathLib.SolvingLinearSystem;

namespace MathLib.Аpproximation.NumericalDifferentiation
{
    public abstract class SplineInterpolation : Approximation
    {
        public abstract class SecondDeriavative
        {
            public static double[] Approximate(double[] x, Function f)
            {
                int n = x.Length;
                double[] y = new double[n];
                for (int i = 0; i < n; i++)
                {
                    y[i] = f(x[i]);
                }
                double A = ((y[2] - y[1]) / (x[2] - x[1]) 
                           - (y[1] - y[0]) / (x[1] - x[0]))
                           / (x[2] - x[0]);
                double B = ((y[n - 1] - y[n - 2]) / (x[n - 1] - x[n - 2])
                           - (y[n - 2] - y[n - 3]) / (x[n - 2] - x[n - 3]))
                           / (x[n - 1] - x[n - 3]);
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

                double[] deriavative = new double[n];
                for (int i = 0; i < n; i++)
                {
                    deriavative[i] = s[i];
                }

                return deriavative;
            }

            public static double[] Approximate(double[] x, double[] y)
            {
                int n = x.Length;
                double A = ((y[2] - y[1]) / (x[2] - x[1])
                           - (y[1] - y[0]) / (x[1] - x[0]))
                           / (x[2] - x[0]);
                double B = ((y[n - 1] - y[n - 2]) / (x[n - 1] - x[n - 2])
                           - (y[n - 2] - y[n - 3]) / (x[n - 2] - x[n - 3]))
                           / (x[n - 1] - x[n - 3]);
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

                double[] deriavative = new double[n];
                for (int i = 0; i < n; i++)
                {
                    deriavative[i] = s[i];
                }

                return deriavative;
            }
        }

        public abstract class FirstDeriavative
        {
            public static double[] Approximate(double[] x, Function f)
            {
                int n = x.Length;
                double[] y = new double[n];
                for (int i = 0; i < n; i++)
                {
                    y[i] = f(x[i]);
                }
                double A = (y[1] - y[0]) / (x[1] - x[0]);
                double B = (y[n - 1] - y[n - 2]) / (x[n - 1] - x[n - 2]);
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

                double[] deriavative = new double[n];
                for (int i = 0; i < n - 1; i++)
                {
                    deriavative[i] = (y[i + 1] - y[i]) / (x[i + 1] - x[i]) -
                        (x[i + 1] - x[i]) * (2 * s[i] + s[i + 1]) / 6;
                }
                deriavative[n - 1] = (y[n - 1] - y[n - 2]) / (x[n - 1] - x[n - 2]) -
                    (x[n - 1] - x[n - 2]) * (-s[n - 2] - 2 * s[n - 1]) / 6;

                return deriavative;
            }

            public static double[] Approximate(double[] x, double[] y)
            {
                int n = x.Length;
                double A = (y[1] - y[0]) / (x[1] - x[0]);
                double B = (y[n - 1] - y[n - 2]) / (x[n - 1] - x[n - 2]);
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

                double[] deriavative = new double[n];
                for (int i = 0; i < n - 1; i++)
                {
                    deriavative[i] = (y[i + 1] - y[i]) / (x[i + 1] - x[i]) -
                        (x[i + 1] - x[i]) * (2 * s[i] + s[i + 1]) / 6;
                }
                deriavative[n - 1] = (y[n - 1] - y[n - 2]) / (x[n - 1] - x[n - 2]) -
                    (x[n - 1] - x[n - 2]) * (-s[n - 2] - 2 * s[n - 1]) / 6;

                return deriavative;
            }
        }
    }
}
