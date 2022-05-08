using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace MathLib.Аpproximation.NumericalDifferentiation
{
    public abstract class InterpolationMethod : Approximation
    {
        public abstract class FirstDeriavative
        {
            public static double[] SecondOrderApproximate(double[] x, Function f)
            {
                int n = x.Length;
                double[] y = new double[n];
                for (int i = 0; i < n; i++)
                {
                    y[i] = f(x[i]);
                }

                double[] deriavative = new double[n];
                for(int i = 1; i < n - 1; i++)
                {
                    deriavative[i - 1] = y[i - 1] * (2 * x[i - 1] - x[i] - x[i + 1]) / ((x[i] - x[i - 1]) * (x[i + 1] - x[i - 1]))
                                         - y[i] * (x[i - 1] - x[i + 1]) / ((x[i] - x[i - 1]) * (x[i + 1] - x[i]))
                                         + y[i + 1] * (x[i - 1] - x[i]) / ((x[i + 1] - x[i]) * (x[i + 1] - x[i - 1]));

                    deriavative[i] = y[i - 1] * (x[i] - x[i + 1]) / ((x[i] - x[i - 1]) * (x[i + 1] - x[i - 1]))
                                     - y[i] * (2 * x[i] - x[i - 1] - x[i + 1]) / ((x[i] - x[i - 1]) * (x[i + 1] - x[i]))
                                     + y[i + 1] * (x[i] - x[i - 1]) / ((x[i + 1] - x[i]) * (x[i + 1] - x[i - 1]));

                    deriavative[i + 1] = y[i - 1] * (x[i + 1] - x[i]) / ((x[i] - x[i - 1]) * (x[i + 1] - x[i - 1]))
                                     - y[i] * (x[i + 1] - x[i - 1]) / ((x[i] - x[i - 1]) * (x[i + 1] - x[i]))
                                     + y[i + 1] * (2 * x[i + 1] - x[i] - x[i - 1]) / ((x[i + 1] - x[i]) * (x[i + 1] - x[i - 1]));
                }

                return deriavative;
            }

            public static double[] FirstOrderApproximate(double[] x, Function f)
            {
                int n = x.Length;
                double[] y = new double[n];
                for (int i = 0; i < n; i++)
                {
                    y[i] = f(x[i]);
                }

                double[] deriavative = new double[n];
                for (int i = 1; i < n; i++)
                {
                    deriavative[i] = (y[i] - y[i - 1]) / (x[i] - x[i - 1]);
                }
                deriavative[0] = (y[1] - y[0]) / (x[1] - x[0]);

                return deriavative;
            }
        }

        public abstract class SecondDeriavative
        {
            public static double[] Approximate(double[] x, Function f)
            {
                int n = x.Length;
                double[] y = new double[n];
                for (int i = 0; i < n; i++)
                {
                    y[i] = f(x[i]);
                }

                double[] deriavative = new double[n];
                for (int i = 1; i < n - 1; i += 3)
                {
                    deriavative[i - 1] = deriavative[i] = deriavative[i + 1] =
                        ((y[i + 1] - y[i]) / (x[i + 1] - x[i]) - (y[i] - y[i - 1]) / (x[i] - x[i - 1])) /
                        (x[i + 1] - x[i - 1]) * 2;
                }
                deriavative[n - 3] = deriavative[n - 2] = deriavative[n - 1] =
                        ((y[n - 1] - y[n - 2]) / (x[n - 1] - x[n - 2]) - (y[n - 2] - y[n - 3]) / (x[n - 2] - x[n - 3])) /
                        (x[n - 1] - x[n - 3]) * 2;
                return deriavative;
            }
        }
    }
}
