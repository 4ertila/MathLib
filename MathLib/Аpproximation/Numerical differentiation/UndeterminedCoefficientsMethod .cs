using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathLib.SolvingLinearSystem;
using static System.Math;

namespace MathLib.Аpproximation.NumericalDifferentiation
{
    public abstract class UndeterminedCoefficientsMethod : Approximation
    {
        public static double[] Approximate(double[] x, Function f, int k)
        {
            int n = x.Length;
            double[,] A = new double[n, n];
            double[] b = new double[n];

            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    A[i, j] = Pow(x[j], i);
                }
                b[i] = 1;
            }

            for(int i = 0; i < k; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    b[j] *= j - i;
                }
            }
            /*for(int i = k; i < n; i++)
            {
                b[i] = 1;
                for(int j = i - k + 2; j < n - i; j++)
                {
                    b[i] *= j;
                }
            }*/

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write($"{Math.Round(A[i, j], 2)} ");
                }
                Console.Write($"| {b[i]} ");
                Console.WriteLine();
            }

            return GaussMethod.Solve(A, b);
        }
    }
}
