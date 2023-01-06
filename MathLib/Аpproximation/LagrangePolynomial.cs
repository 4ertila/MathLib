using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathLib.Objects;
using static System.Math;
using System.Numerics;

namespace MathLib.Аpproximation
{
    public abstract class LagrangePolynomial
    {
        public static double Approximate(double[] xValues, double[] yValues, double x)
        {
            double result = 0;
            int n = xValues.Length;
            for(int i = 0; i < n; i++)
            {
                double tValue = 1;
                for (int j = 0; j < n; j++)
                {
                    if (j != i)
                    {
                        tValue *= (x - xValues[j]) / (xValues[i] - xValues[j]);
                    }
                }
                result += tValue * yValues[i];
            }
            return result;
        }

        public static Polynomial Approximate(double[] x, double[] y)
        {
            int n = x.Length;
            Polynomial result = new Polynomial();
            Polynomial tPolynomial;

            for (int i = 0; i < n; i++)
            {
                tPolynomial = new Polynomial(new double[] { 1 }) * y[i];

                for (int j = 0; j < n; j++)
                {
                    if(j != i)
                    {
                        tPolynomial *= new Polynomial(new double[] { -x[j], 1 }) / (x[i] - x[j]);
                    }
                }

                result += tPolynomial;
            }

            return result;
        }

        public static Polynomial Approximate(double[] x, Function f)
        {
            int n = x.Length;
            Polynomial result = new Polynomial();
            Polynomial tPolynomial;

            for (int i = 0; i < n; i++)
            {
                f["x"] = x[i];
                tPolynomial = new Polynomial(new double[] { 1 }) * f.Calculate();

                for (int j = 0; j < n; j++)
                {
                    if (j != i)
                    {
                        tPolynomial *= new Polynomial(new double[] { -x[j], 1 }) / (x[i] - x[j]);
                    }
                }

                result += tPolynomial;
            }

            return result;
        }
    }
}
