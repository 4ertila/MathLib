using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathLib.Objects;

namespace MathLib.SolvingLinearSystem
{
    public abstract class SweepMethod
    {
        public static Vector Solve(Matrix A, Vector r)
        {
            int n = A.rows;
            double[] c = new double[n];
            double[] b = new double[n];
            double[] d = new double[n];

            for (int i = 0; i < n; i++)
            {
                c[i] = A[i, i];
            }

            b[0] = 0;
            for (int i = 1; i < n; i++)
            {
                b[i] = A[i, i - 1];
            }

            d[n - 1] = 0;
            for (int i = 0; i < n - 1; i++)
            {
                d[i] = A[i, i + 1];
            }

            double[] delta = new double[n];
            double[] lambda = new double[n];

            delta[0] = -d[0] / c[0];
            lambda[0] = r[0] / c[0];
            for (int i = 1; i < n; i++)
            {
                delta[i] = -d[i] / (c[i] + b[i] * delta[i - 1]);
                lambda[i] = (r[i] - b[i] * lambda[i - 1]) / (c[i] + b[i] * delta[i - 1]);
            }

            Vector x = new Vector(n);

            x[n - 1] = lambda[n - 1];
            for (int i = n - 2; i >= 0; i--)
            {
                x[i] = delta[i] * x[i + 1] + lambda[i];
            }

            return x;
        }

        public static double[] Solve(double[,] A, double[] r)
        {
            int n = A.GetLength(0);
            double[] c = new double[n];
            double[] b = new double[n];
            double[] d = new double[n];

            for (int i = 0; i < n; i++)
            {
                c[i] = A[i, i];
            }

            b[0] = 0;
            for (int i = 1; i < n; i++)
            {
                b[i] = A[i, i - 1];
            }

            d[n - 1] = 0;
            for (int i = 0; i < n - 1; i++)
            {
                d[i] = A[i, i + 1];
            }

            double[] delta = new double[n];
            double[] lambda = new double[n];

            delta[0] = -d[0] / c[0];
            lambda[0] = r[0] / c[0];
            for (int i = 1; i < n; i++)
            {
                delta[i] = -d[i] / (c[i] + b[i] * delta[i - 1]);
                lambda[i] = (r[i] - b[i] * lambda[i - 1]) / (c[i] + b[i] * delta[i - 1]);
            }

            double[] x = new double[n];

            x[n - 1] = lambda[n - 1];
            for (int i = n - 2; i >= 0; i--)
            {
                x[i] = delta[i] * x[i + 1] + lambda[i];
            }

            return x;
        }
    }
}
