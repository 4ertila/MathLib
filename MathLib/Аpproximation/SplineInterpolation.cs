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
    public abstract class SplineInterpolation : Approximation
    {
        public static double[,] Approximate(double[] x, Function f)
        {
            int n = x.Length;
            double[] y = new double[n];
            for(int i = 0; i < n; i++)
            {
                y[i] = f(x[i]);
            }
            double h = x[1] - x[0];
            double[,] M = new double[n - 1, n];
            double[] b = new double[n - 1];
            for (int i = 1; i < n - 2; i++)
            {
                M[i, i - 1] = h / 6;
                M[i, i] = (x[i + 1] - x[i - 1]) / 3;
                M[i, i + 1] = h / 6;
            }
            for (int i = 1; i < n - 1; i++)
            {
                b[i - 1] = (y[i + 1] - y[i]) / h -
                           (y[i] - y[i - 1]) / h;
            }
            b[n - 2] = (y[n - 1] - y[n - 2]) / h -
                           (y[n - 2] - y[n - 3]) / h;
            M[0, 0] = M[n - 2, n - 2] = h * 2 / 3;
            M[0, 1] = M[n - 2, n - 3] = h / 6;

            double[] S = new double[n];
            Array.Copy(SweepMethod.Solve(M, b), S, n - 1);
            S[n - 1] = 0;
            double[,] A = new double[n - 1, 4];
            double k0, k1, k2, k3;
            for (int i = 0; i < n - 1; i++)
            {
                k0 = -S[i] / (6 * h);
                k1 = S[i + 1] / (6 * h);
                k2 = (y[i + 1] - y[i]) / h - h * (S[i + 1] - S[i]) / 6;
                k3 = y[i] - S[i] * Pow(h, 2) / 6;

                A[i, 3] = k0 + k1;
                A[i, 2] = -3 * x[i + 1] * k0 - 3 * x[i] * k1;
                A[i, 1] = 3 * Pow(x[i + 1], 2) * k0 + 3 * Pow(x[i], 2) * k1 + k2;
                A[i, 0] = -k0 * Pow(x[i + 1], 3) - k1 * Pow(x[i], 3) - k2 * x[i] + k3;
            }

            return A;
        }
        public static double[,] Approximate1(double[] x, double[] y)
        {
            int n = x.Length;
            double h = x[1] - x[0];
            double[,] M = new double[n - 2, n - 2];
            double[] b = new double[n - 1];
            for (int i = 1; i < n - 3; i++)
            {
                M[i, i - 1] = h / 6;
                M[i, i] = (x[i + 1] - x[i - 1]) / 3;
                M[i, i + 1] = h / 6;
            }
            for (int i = 1; i < n - 2; i++)
            {
                b[i - 1] = (y[i + 1] - y[i]) / h -
                           (y[i] - y[i - 1]) / h;
            }
            b[n - 2] = (y[n - 1] - y[n - 2]) / h -
                           (y[n - 2] - y[n - 3]) / h;
            M[n - 3, 0] = h / 6;
            M[n - 3, 1] = h * 2 / 3;
            M[0 ,0] = h * 2 / 3;
            M[0, 1] = M[n - 3, n - 3] = h / 6;
            for (int i = 0; i < n-2; i++)
            {
                for (int j = 0; j < n-2; j++)
                {
                    Console.Write(Round(M[i, j], 2) + " ");
                }
                Console.WriteLine();
            }
            double[] S = new double[n];
            Array.Copy(GaussMethod.Solve(M, b), S, n - 2);
            S[n - 1] = 0;
            double[,] A = new double[n - 1, 4];
            double k0, k1, k2, k3;
            for (int i = 0; i < n - 1; i++)
            {
                k0 = -S[i] / (6 * h);
                k1 = S[i + 1] / (6 * h);
                k2 = (y[i + 1] - y[i]) / h - h * (S[i + 1] - S[i]) / 6;
                k3 = y[i] - S[i] * Pow(h, 2) / 6;

                A[i, 3] = k0 + k1;
                A[i, 2] = -3 * x[i + 1] * k0 - 3 * x[i] * k1;
                A[i, 1] = 3 * Pow(x[i + 1], 2) * k0 + 3 * Pow(x[i], 2) * k1 + k2;
                A[i, 0] = -k0 * Pow(x[i + 1], 3) - k1 * Pow(x[i], 3) - k2 * x[i] + k3;
            }

            return A;
        }
        public static double[,] Approximate(double[] x, double[] y)
        {
            int n = x.Length;
            double h = x[1] - x[0];
            double[,] M = new double[n - 1, n];
            double[] b = new double[n - 1];
            for (int i = 1; i < n - 2; i++)
            {
                M[i, i - 1] = h / 6;
                M[i, i] = (x[i + 1] - x[i - 1]) / 3;
                M[i, i + 1] = h / 6;
            }
            for (int i = 1; i < n - 1; i++)
            {
                b[i - 1] = (y[i + 1] - y[i]) / h -
                           (y[i] - y[i - 1]) / h;
            }
            b[n - 2] = (y[n - 1] - y[n - 2]) / h -
                           (y[n - 2] - y[n - 3]) / h;
            M[0, 0] = M[n - 2, n - 2] = h * 2 / 3;
            M[0, 1] = M[n - 2, n - 3] = h / 6;

            double[] S = new double[n];
            Array.Copy(SweepMethod.Solve(M, b), S, n - 1);
            S[n - 1] = 0;
            double[,] A = new double[n - 1, 4];
            double k0, k1, k2, k3;
            for(int i = 0; i < n - 1; i++)
            {
                k0 = -S[i] / (6 * h);
                k1 = S[i + 1] / (6 * h);
                k2 = (y[i + 1] - y[i]) / h - h * (S[i + 1] - S[i]) / 6;
                k3 = y[i] - S[i] * Pow(h, 2) / 6;

                A[i, 3] = k0 + k1;
                A[i, 2] = -3 * x[i + 1] * k0 - 3 * x[i] * k1;
                A[i, 1] = 3 * Pow(x[i + 1], 2) * k0 + 3 * Pow(x[i], 2) * k1 + k2;
                A[i, 0] = -k0 * Pow(x[i + 1], 3) - k1 * Pow(x[i], 3) - k2 * x[i] + k3;
            }

            return A;
        }

        public abstract class Type1
        {
            public static double[,] Approximate(double[] x, double[] y)
            {
                int n = x.Length;
                Matrix M = new Matrix(n - 2, n - 2);
                Vector b = new Vector(n - 2);
                for(int i = 1; i < n - 1; i++)
                {
                    M[i, i - 1] = (x[i] - x[i - 1]) / 6;
                    M[i, i] = (x[i + 1] - x[i - 1]) / 3;
                    M[i, i + 1] = (x[i + 1] - x[i]) / 6;

                    b[i - 1] = (y[i + 1] - y[i]) / (x[i + 1] - x[i]) -
                               (y[i] - y[i - 1]) / (x[i] - x[i - 1]);
                }

                for(int i = 0; i < M.Rows; i++)
                {
                    for(int j = 0; j < M.Columns; j++)
                    {
                        Console.Write($"{Math.Round(M[i, j], 2)} ");
                    }
                    Console.Write($"| {b[i]} ");
                    Console.WriteLine();
                }
                return null;
            }
        }
    }
}
