using System;  
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace MathLib.OptimizationMethods
{
    public abstract class KnapsackProblem
    {
        public static int[] Solve(int[] c, int[] a, int b)
        {
            int n = c.Length;
            int[,] f = new int[n + 1, b + 1];

            int j;
            for(int i = 1; i < n + 1; i++)
            {
                for(j = 0; j < b + 1 - a[n - i]; j++)
                {
                    f[i, j] = Max(f[i - 1, j], c[n - i] + f[i - 1, j + a[n - i]]);
                }

                while (j < b + 1)
                {
                    f[i, j] = f[i - 1, j];
                    j++;
                }
            }

            int E = 0;
            int[] x = new int[n];
            for (int i = 0; i < n; i++)
            {
                if (E <= b + 1)
                {
                    if (f[n - i, E] == f[n - i - 1, E])
                    {
                        x[i] = 0;
                    }
                    else
                    {
                        x[i] = 1;
                    }
                }
                else
                {
                    x[i] = 0;
                }
                E += a[i] * x[i];
            }
            return x;
        }
    }
}
