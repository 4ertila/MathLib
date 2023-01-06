using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathLib.Objects;

namespace MathLib.Аpproximation
{
    public abstract class NewtonPolynomial
    {
        public static double Approximate(double[] xValues, double[] yValues, double x)
        {
            int n = xValues.Length;
            double[] fPrev;
            double[] f = new double[n];
            double result = 0;
            double tValue;
            Array.Copy(yValues, f, n);
            int i = 0;

            do
            {
                tValue = 1;
                for (int k = 0; k < i; k++)
                {
                    tValue *= x - xValues[k];
                }
                result += tValue * f[0];

                fPrev = f;

                f = new double[fPrev.Length - 1];
                for (int j = 0; j < f.Length; j++)
                {
                    f[j] = (fPrev[j + 1] - fPrev[j]) / (xValues[j + i + 1] - xValues[j]);
                }

                i++;
            }
            while (i < n);

            return result;
        }

        public static Polynomial Approximate(double[] x, double[] y)
        {
            int n = x.Length;
            double[] fPrev;
            double[] f = new double[n];
            Polynomial result = new Polynomial();
            Polynomial tPolynomial;
            Array.Copy(y, f, n);
            int i = 0;

            do
            {
                tPolynomial = new Polynomial(new double[] { 1 });
                for (int k = 0; k < i; k++)
                {
                    tPolynomial *= new Polynomial(new double[] { -x[k], 1 });
                }
                result += tPolynomial * f[0];

                fPrev = f;

                f = new double[fPrev.Length - 1];
                for (int j = 0; j < f.Length; j++)
                {
                    f[j] = (fPrev[j + 1] - fPrev[j]) / (x[j + i + 1] - x[j]);
                }

                i++;
            }
            while (i < n);

            return result;
        }

        public static Polynomial Approximate(double[] x, Function y)
        {
            int n = x.Length;
            double[] fPrev;
            double[] f = new double[n];
            Polynomial result = new Polynomial();
            Polynomial tPolynomial;
            for (int j = 0; j < n; j++)
            {
                y["x"] = x[j];
                f[j] = y.Calculate();
            }

            for(int i = 0; i < n; i++)
            { 
                tPolynomial = new Polynomial(new double[] { 1 });
                for (int k = 0; k < i; k++)
                {
                    tPolynomial *= new Polynomial(new double[] { -x[k], 1 });
                }
                result += tPolynomial * f[0];

                fPrev = f;

                f = new double[fPrev.Length - 1];
                for (int j = 0; j < f.Length; j++)
                {
                    f[j] = (fPrev[j + 1] - fPrev[j]) / (x[j + i + 1] - x[j]);
                }
            }

            return result;
        }
    }
}
