﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathLib.SolvingLinearSystem;
using static System.Math;

namespace MathLib.Аpproximation
{
    public abstract class LeastSquareMethod
    {
        public static double[] Approximate(double[] x, double[] y, int m)
        {
            int n = x.Length;
            double[,] S = new double[m + 1, m + 1];
            double[] t = new double[m + 1];
            for(int i = 0; i < m + 1; i++)
            {
                for (int j = 0; j < m + 1; j++)
                {
                    for (int k = 0; k < n; k++)
                    {
                        S[i, j] += Pow(x[k], i + j);
                    }
                }

                for (int j = 0; j < n; j++)
                {
                    t[i] += y[j] * Pow(x[j], i);
                }
            }

            return GaussMethod.Solve(S, t);
        }
    }
}
