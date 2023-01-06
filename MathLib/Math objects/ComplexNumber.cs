using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace MathLib.Objects
{
    public class ComplexNumber
    {
        public double re;
        public double im;

        public ComplexNumber(double re, double im)
        {
            this.re = re;
            this.im = im;
        }
        public ComplexNumber(ComplexNumber ComplexNumber)
        {
            re = ComplexNumber.re;
            im = ComplexNumber.im;
        }
        public ComplexNumber()
        {
            re = 0;
            im = 0;
        }

        public static double Abs(ComplexNumber ComplexNumber)
        {
            return Sqrt(Pow(ComplexNumber.re, 2) * Pow(ComplexNumber.im, 2));
        }

        public ComplexNumber Conjugate()
        {
            return new ComplexNumber(re, -im);
        }

        public ComplexNumber ReverseComplexNumber()
        {
            return new ComplexNumber(Conjugate() / (Pow(re, 2) + Pow(im, 2)));
        }

        public override string ToString()
        {
            if(im < 0)
            {
                return re.ToString() + im.ToString() + "i";
            }
            else if(im == 0)
            {
                return re.ToString();
            }
            else
            {
                return re.ToString() + "+" + im.ToString() + "i";
            }
        }

        public static ComplexNumber operator +(ComplexNumber ComplexNumber1, ComplexNumber ComplexNumber2)
        {
            return new ComplexNumber(ComplexNumber1.re + ComplexNumber2.re, ComplexNumber1.im + ComplexNumber2.im);
        }
        public static ComplexNumber operator +(ComplexNumber ComplexNumber, double value)
        {
            return new ComplexNumber(ComplexNumber.re + value, ComplexNumber.im);
        }
        public static ComplexNumber operator +(double value, ComplexNumber ComplexNumber)
        {
            return new ComplexNumber(ComplexNumber.re + value, ComplexNumber.im);
        }

        public static ComplexNumber operator -(ComplexNumber ComplexNumber1, ComplexNumber ComplexNumber2)
        {
            return new ComplexNumber(ComplexNumber1.re - ComplexNumber2.re, ComplexNumber1.im - ComplexNumber2.im);
        }
        public static ComplexNumber operator -(ComplexNumber ComplexNumber, double value)
        {
            return new ComplexNumber(ComplexNumber.re - value, ComplexNumber.im);
        }
        public static ComplexNumber operator -(double value, ComplexNumber ComplexNumber)
        {
            return new ComplexNumber(value - ComplexNumber.re, -ComplexNumber.im);
        }

        public static ComplexNumber operator *(ComplexNumber ComplexNumber1, ComplexNumber ComplexNumber2)
        {
            return new ComplexNumber(ComplexNumber1.re * ComplexNumber2.re - ComplexNumber1.im * ComplexNumber2.im, 
                               ComplexNumber1.re * ComplexNumber2.im + ComplexNumber1.im * ComplexNumber2.re);
        }
        public static ComplexNumber operator *(ComplexNumber ComplexNumber, double value)
        {
            return new ComplexNumber(ComplexNumber.re * value, ComplexNumber.im * value);
        }
        public static ComplexNumber operator *(double value, ComplexNumber ComplexNumber)
        {
            return new ComplexNumber(ComplexNumber.re * value, ComplexNumber.im * value);
        }

        public static ComplexNumber operator /(ComplexNumber ComplexNumber1, ComplexNumber ComplexNumber2)
        {
            return new ComplexNumber(ComplexNumber1 * ComplexNumber2.Conjugate() / (Pow(ComplexNumber2.re, 2) + Pow(ComplexNumber2.im, 2)));
        }
        public static ComplexNumber operator /(ComplexNumber ComplexNumber, double value)
        {
            return new ComplexNumber(ComplexNumber.re / value, ComplexNumber.im / value);
        }
        public static ComplexNumber operator /(double value, ComplexNumber ComplexNumber)
        {
            return new ComplexNumber(value * ComplexNumber.Conjugate() / (Pow(ComplexNumber.re, 2) + Pow(ComplexNumber.im, 2)));
        }

        public static bool operator ==(ComplexNumber ComplexNumber1, ComplexNumber ComplexNumber2)
        {
            if(ComplexNumber1.re == ComplexNumber2.re && ComplexNumber1.im == ComplexNumber2.im)
            {
                return true;
            }
            return false;
        }
        public static bool operator ==(ComplexNumber ComplexNumber, double value)
        {
            if (ComplexNumber.re == value && ComplexNumber.im == 0)
            {
                return true;
            }
            return false;
        }
        public static bool operator ==(double value, ComplexNumber ComplexNumber)
        {
            if (ComplexNumber.re == value && ComplexNumber.im == 0)
            {
                return true;
            }
            return false;
        }

        public static bool operator !=(ComplexNumber ComplexNumber1, ComplexNumber ComplexNumber2)
        {
            if (ComplexNumber1.re != ComplexNumber2.re || ComplexNumber1.im != ComplexNumber2.im)
            {
                return true;
            }
            return false;
        }
        public static bool operator !=(ComplexNumber ComplexNumber, double value)
        {
            if (ComplexNumber.re != value || ComplexNumber.im != 0)
            {
                return true;
            }
            return false;
        }
        public static bool operator !=(double value, ComplexNumber ComplexNumber)
        {
            if (ComplexNumber.re != value || ComplexNumber.im != 0)
            {
                return true;
            }
            return false;
        }
    }
}
