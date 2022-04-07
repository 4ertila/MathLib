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
    public abstract class HermitianPolynomial : Approximation
    {
        public static double[] Approximate(double[] x, double[] f, double[,] df)
        {
            int n = f.Length;
            for(int i = 0; i < df.GetLength(0); i++)
            {
                for(int j = 0; j < df.GetLength(1); j++)
                {
                    if(!double.IsNaN(df[i, j]))
                    {
                        n++;
                    }
                }
            }
            double[,] H = new double[n, n];
            double[] b = new double[n];

            for(int i = 0; i < f.Length; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    H[i, j] = Pow(x[i], n - j - 1);
                }
                b[i] = f[i];
            }

            for (int i = 0, h = f.Length; i < df.GetLength(0); i++)
            {
                for (int j = 0; j < f.Length; j++)
                {
                    if (!double.IsNaN(df[i, j]))
                    {
                        for (int k = 0; k < n - i - 1; k++)
                        {
                            H[h, k] = 1;
                            for (int p = 0; p <= i; p++)
                            {
                                H[h, k] *= n - 1 - k - p;
                            }
                            H[h, k] *= Pow(x[j], n - 2 - i - k);
                            b[h] = df[i, j];
                        }
                        h++;
                    }
                }
            }

            return GaussMethod.Solve(H, b).Reverse().ToArray();
        }
    }
}
