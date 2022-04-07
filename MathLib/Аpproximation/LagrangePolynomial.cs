using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace MathLib.Аpproximation
{
    public abstract class LagrangePolynomial : Approximation
    {       
        public static double[] Approximate(double[] x, double[] y)
        {
            int n = x.Length;
            double yi;
            double[] a = new double[n];
            double[] tArr = new double[n];
            double tValue;
            Array.Copy(x, tArr, n);

            for (int i = 0; i < n; i++)
            {
                yi = y[i];
                for(int j = 0; j < n; j++)
                {
                    if(j != i)
                    {
                        yi /= x[i] - x[j];
                    }
                }

                tArr[i] = 0;
                for (int k = 0; k < n - 1; k++)
                {
                    tValue = 0;
                    Combinations(ref tValue, n - k - 1, tArr);
                    a[k] += tValue * yi;
                }
                a[n - 1] += yi;
                tArr[i] = x[i];
            }

            return a;
        }

        public static double[] Approximate(double[] x, Function f)
        {
            int n = x.Length;
            double yi;
            double tValue;
            double[] a = new double[n];
            double[] tArr = new double[n];
            Array.Copy(x, tArr, n);

            for (int i = 0; i < n; i++)
            {
                yi = f(x[i]);
                for(int j = 0; j < n; j++)
                {
                    if(j != i)
                    {
                        yi /= x[i] - x[j];
                    }
                }

                tArr[i] = 0;
                for (int k = 0; k < n - 1; k++)
                {
                    tValue = 0;
                    Combinations(ref tValue, n - k - 1, tArr);
                    a[k] += tValue * yi;
                }
                a[n - 1] += yi;
                tArr[i] = x[i];
            }

            return a;
        }
    }
}
