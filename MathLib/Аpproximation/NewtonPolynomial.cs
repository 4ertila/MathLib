using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathLib.Аpproximation
{
    public abstract class NewtonPolynomial : Approximation
    {
        public static double[] Approximate(double[] x, double[] y)
        {
            int n = x.Length;
            double[] a = new double[n];
            double[] fPrev;
            double[] f = new double[n];
            double[] tArr = new double[0];
            double tValue = 0;
            Array.Copy(y, f, n);
            int i = 0;

            do
            {
                for (int k = 0; k < i; k++)
                {
                    tValue = 0;
                    Combinations(ref tValue, i - k, tArr);
                    a[k] += tValue * f[0];
                }
                a[i] += f[0];
                tArr = tArr.Append(x[i]).ToArray();

                fPrev = f;

                f = new double[fPrev.Length - 1];
                for (int j = 0; j < f.Length; j++)
                {
                    f[j] = (fPrev[j + 1] - fPrev[j]) / (x[j + i + 1] - x[j]);
                }

                i++;
            }
            while (i < n);
            a[n - 1] = fPrev[0];

            return a;
        }

        public static double[] Approximate(double[] x, Function y)
        {
            int n = x.Length;
            double[] a = new double[n];
            double[] fPrev;
            double[] f = new double[n];
            double[] tArr = new double[0];
            double tValue = 0;
            for(int j = 0; j < n; j++)
            {
                f[j] = y(x[j]);
            }
            int i = 0;

            do
            {
                for (int k = 0; k < i; k++)
                {
                    tValue = 0;
                    Combinations(ref tValue, i - k, tArr);
                    a[k] += tValue * f[0];
                }
                a[i] += f[0];
                tArr = tArr.Append(x[i]).ToArray();

                fPrev = f;

                f = new double[fPrev.Length - 1];
                for (int j = 0; j < f.Length; j++)
                {
                    f[j] = (fPrev[j + 1] - fPrev[j]) / (x[j + i + 1] - x[j]);
                }

                i++;
            }
            while (i < n);
            a[n - 1] = fPrev[0];

            return a;
        }
    }
}
