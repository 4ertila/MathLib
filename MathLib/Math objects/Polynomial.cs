using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathLib.Objects
{
    public class Polynomial
    {
        public Polynomial()
        {
            n = 0;
            degree = -1;
            coefficients = new double[n];
        }
        public Polynomial(int n)
        {
            degree = n - 1;
            coefficients = new double[n];
        }
        public Polynomial(params double[] coefficients)
        {
            n = coefficients.Length;
            degree = n - 1;
            this.coefficients = new double[n];
            Array.Copy(coefficients, this.coefficients, n);
        }
        public Polynomial(IEnumerable<double> coefficients)
        {
            n = coefficients.Count();
            degree = n - 1;
            this.coefficients = new double[n];
            int i = 0;
            foreach(double coefficient in coefficients)
            {
                this.coefficients[i] = coefficient;
                i++;
            }
        }

        public int degree { private set; get; }
        public int n { private set; get; }
        private double[] coefficients;

        public override string ToString()
        {
            string result = coefficients[0].ToString() + " ";
            for(int i = 1; i < n; i++)
            {
                if (coefficients[i] > 0)
                {
                    result += "+ " + coefficients[i] + $"*x^{i} ";
                }
                else if(coefficients[i] < 0)
                {
                    result += coefficients[i] + $"*x^{i} ";
                }
            }
            return result;
        }      

        public Function ToFunction()
        {
            return new Function(ToString(), "x");
        }

        public IEnumerable<double> Coefficients => coefficients;

        public Polynomial Integrate()
        {
            double[] integrateCoefficients = new double[n + 1];

            for (int i = 1; i <= n; i++)
            {
                integrateCoefficients[i] = coefficients[i - 1] / i;
            }

            return new Polynomial(integrateCoefficients);
        }
        public double Integrate(double a, double b)
        {
            double result = 0;
            double tValueB;
            double tValueA;

            for (int i = 0; i < n; i++)
            {
                tValueB = tValueA = coefficients[i] / (i + 1);
                for (int j = 0; j < i + 1; j++)
                {
                    tValueA *= a;
                    tValueB *= b;
                }
                result += tValueB - tValueA;
            }

            return result;
        }

        public Polynomial Differentiate()
        {
            double[] integrateCoefficients = new double[n - 1];

            for(int i = 1; i < n; i++)
            {
                integrateCoefficients[i - 1] = coefficients[i] * i; 
            }

            return new Polynomial(integrateCoefficients);
        }

        public static Polynomial Pow(Polynomial polynomial, int degree)
        {
            Polynomial result = new Polynomial(new double[] { 1 });
            for (int i = 0; i < degree; i++)
            {
                result *= polynomial;
            }
            return result;
        }

        public double Calculate(double x)
        {
            double result = 0;
            double tValue = 0;
            for(int i = 0; i < n; i++)
            {
                tValue = coefficients[i];
                for (int j = 0; j < i; j++)
                {
                    tValue *= x;
                }
                result += tValue;
            }
            return result;
        }

        public static Polynomial operator *(Polynomial polynomial, double value)
        {
            double[] newCoefficients = new double[polynomial.n];
            for(int i = 0; i < polynomial.n; i++)
            {
                newCoefficients[i] = polynomial.coefficients[i] * value;
            }
            return new Polynomial(newCoefficients);
        }
        public static Polynomial operator *(double value, Polynomial polynomial)
        {
            double[] newCoefficients = new double[polynomial.n];
            for (int i = 0; i < polynomial.n; i++)
            {
                newCoefficients[i] = polynomial.coefficients[i] * value;
            }
            return new Polynomial(newCoefficients);
        }
        public static Polynomial operator *(Polynomial polynomial1, Polynomial polynomial2)
        {
            double[] newCoefficients = new double[polynomial1.n + polynomial2.n - 1];
            Polynomial tPolynomial;
            for (int i = 0; i < polynomial2.n; i++)
            {
                tPolynomial = polynomial1 * polynomial2.coefficients[i];
                for (int j = 0; j < tPolynomial.n; j++)
                {
                    newCoefficients[j + i] += tPolynomial.coefficients[j];
                }
            }
            return new Polynomial(newCoefficients);
        }

        public static Polynomial operator /(Polynomial polynomial, double value)
        {
            double[] newCoefficients = new double[polynomial.n];
            for (int i = 0; i < polynomial.n; i++)
            {
                newCoefficients[i] = polynomial.coefficients[i] / value;
            }
            return new Polynomial(newCoefficients);
        }

        public static Polynomial operator +(Polynomial polynomial1, Polynomial polynomial2)
        {
            int minN = Math.Min(polynomial1.n, polynomial2.n);
            double[] newCoefficients = new double[Math.Max(polynomial1.n, polynomial2.n)];

            for (int i = 0; i < minN; i++)
            {
                newCoefficients[i] = polynomial1.coefficients[i] + polynomial2.coefficients[i];
            }

            if(polynomial1.n > polynomial2.n)
            {
                for (int i = minN; i < polynomial1.n; i++)
                {
                    newCoefficients[i] = polynomial1.coefficients[i];
                }
            }
            else
            {
                for (int i = minN; i < polynomial2.n; i++)
                {
                    newCoefficients[i] = polynomial2.coefficients[i];
                }
            }
            return new Polynomial(newCoefficients);
        }
        public static Polynomial operator +(Polynomial polynomial, double value)
        {
            double[] newCoefficients = polynomial.coefficients;
            newCoefficients[0] += value;
            return new Polynomial(newCoefficients);
        }
        public static Polynomial operator +(double value, Polynomial polynomial)
        {
            double[] newCoefficients = polynomial.coefficients;
            newCoefficients[0] += value;
            return new Polynomial(newCoefficients);
        }

        public static Polynomial operator -(Polynomial polynomial1, Polynomial polynomial2)
        {
            int minN = Math.Min(polynomial1.n, polynomial2.n);
            double[] newCoefficients = new double[Math.Max(polynomial1.n, polynomial2.n)];

            for (int i = 0; i < minN; i++)
            {
                newCoefficients[i] = polynomial1.coefficients[i] - polynomial2.coefficients[i];
            }

            if (polynomial1.n > polynomial2.n)
            {
                for (int i = minN; i < polynomial1.n; i++)
                {
                    newCoefficients[i] = polynomial1.coefficients[i];
                }
            }
            else
            {
                for (int i = minN; i < polynomial2.n; i++)
                {
                    newCoefficients[i] = -polynomial2.coefficients[i];
                }
            }
            return new Polynomial(newCoefficients);
        }
        public static Polynomial operator -(Polynomial polynomial, double value)
        {
            double[] newCoefficients = polynomial.coefficients;
            newCoefficients[0] -= value;
            return new Polynomial(newCoefficients);
        }

        public static bool operator ==(Polynomial polynomial1, Polynomial polynomial2)
        {
            if(polynomial1.n == polynomial2.n)
            {
                for(int i = 0; i < polynomial1.n; i++)
                {
                    if(polynomial1.coefficients[i] != polynomial2.coefficients[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public static bool operator !=(Polynomial polynomial1, Polynomial polynomial2)
        {
            if (polynomial1.n == polynomial2.n)
            {
                for (int i = 0; i < polynomial1.n; i++)
                {
                    if (polynomial1.coefficients[i] == polynomial2.coefficients[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
