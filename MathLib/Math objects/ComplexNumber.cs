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
        public double Re;
        public double Im;

        public ComplexNumber(double Re, double Im)
        {
            this.Re = Re;
            this.Im = Im;
        }

        public ComplexNumber(ComplexNumber ComplexNumber)
        {
            Re = ComplexNumber.Re;
            Im = ComplexNumber.Im;
        }

        public ComplexNumber()
        {
            Re = 0;
            Im = 0;
        }

        public static double Abs(ComplexNumber ComplexNumber)
        {
            return Sqrt(Pow(ComplexNumber.Re, 2) * Pow(ComplexNumber.Im, 2));
        }

        public ComplexNumber Conjugate()
        {
            return new ComplexNumber(Re, -Im);
        }

        public ComplexNumber ReverseComplexNumber()
        {
            return new ComplexNumber(Conjugate() / (Pow(Re, 2) + Pow(Im, 2)));
        }

        public override string ToString()
        {
            if(Im < 0)
            {
                return Re.ToString() + Im.ToString() + "i";
            }
            else if(Im == 0)
            {
                return Re.ToString();
            }
            else
            {
                return Re.ToString() + "+" + Im.ToString() + "i";
            }
        }

        public static ComplexNumber operator +(ComplexNumber ComplexNumber1, ComplexNumber ComplexNumber2)
        {
            return new ComplexNumber(ComplexNumber1.Re + ComplexNumber2.Re, ComplexNumber1.Im + ComplexNumber2.Im);
        }

        public static ComplexNumber operator +(ComplexNumber ComplexNumber, double value)
        {
            return new ComplexNumber(ComplexNumber.Re + value, ComplexNumber.Im);
        }

        public static ComplexNumber operator +(double value, ComplexNumber ComplexNumber)
        {
            return new ComplexNumber(ComplexNumber.Re + value, ComplexNumber.Im);
        }

        public static ComplexNumber operator -(ComplexNumber ComplexNumber1, ComplexNumber ComplexNumber2)
        {
            return new ComplexNumber(ComplexNumber1.Re - ComplexNumber2.Re, ComplexNumber1.Im - ComplexNumber2.Im);
        }

        public static ComplexNumber operator -(ComplexNumber ComplexNumber, double value)
        {
            return new ComplexNumber(ComplexNumber.Re - value, ComplexNumber.Im);
        }

        public static ComplexNumber operator -(double value, ComplexNumber ComplexNumber)
        {
            return new ComplexNumber(value - ComplexNumber.Re, -ComplexNumber.Im);
        }

        public static ComplexNumber operator *(ComplexNumber ComplexNumber1, ComplexNumber ComplexNumber2)
        {
            return new ComplexNumber(ComplexNumber1.Re * ComplexNumber2.Re - ComplexNumber1.Im * ComplexNumber2.Im, 
                               ComplexNumber1.Re * ComplexNumber2.Im + ComplexNumber1.Im * ComplexNumber2.Re);
        }

        public static ComplexNumber operator *(ComplexNumber ComplexNumber, double value)
        {
            return new ComplexNumber(ComplexNumber.Re * value, ComplexNumber.Im * value);
        }

        public static ComplexNumber operator *(double value, ComplexNumber ComplexNumber)
        {
            return new ComplexNumber(ComplexNumber.Re * value, ComplexNumber.Im * value);
        }

        public static ComplexNumber operator /(ComplexNumber ComplexNumber1, ComplexNumber ComplexNumber2)
        {
            return new ComplexNumber(ComplexNumber1 * ComplexNumber2.Conjugate() / (Pow(ComplexNumber2.Re, 2) + Pow(ComplexNumber2.Im, 2)));
        }

        public static ComplexNumber operator /(ComplexNumber ComplexNumber, double value)
        {
            return new ComplexNumber(ComplexNumber.Re / value, ComplexNumber.Im / value);
        }

        public static ComplexNumber operator /(double value, ComplexNumber ComplexNumber)
        {
            return new ComplexNumber(value * ComplexNumber.Conjugate() / (Pow(ComplexNumber.Re, 2) + Pow(ComplexNumber.Im, 2)));
        }

        public static bool operator ==(ComplexNumber ComplexNumber1, ComplexNumber ComplexNumber2)
        {
            if(ComplexNumber1.Re == ComplexNumber2.Re && ComplexNumber1.Im == ComplexNumber2.Im)
            {
                return true;
            }
            return false;
        }

        public static bool operator ==(ComplexNumber ComplexNumber, double value)
        {
            if (ComplexNumber.Re == value && ComplexNumber.Im == 0)
            {
                return true;
            }
            return false;
        }

        public static bool operator ==(double value, ComplexNumber ComplexNumber)
        {
            if (ComplexNumber.Re == value && ComplexNumber.Im == 0)
            {
                return true;
            }
            return false;
        }

        public static bool operator !=(ComplexNumber ComplexNumber1, ComplexNumber ComplexNumber2)
        {
            if (ComplexNumber1.Re != ComplexNumber2.Re || ComplexNumber1.Im != ComplexNumber2.Im)
            {
                return true;
            }
            return false;
        }

        public static bool operator !=(ComplexNumber ComplexNumber, double value)
        {
            if (ComplexNumber.Re != value || ComplexNumber.Im != 0)
            {
                return true;
            }
            return false;
        }

        public static bool operator !=(double value, ComplexNumber ComplexNumber)
        {
            if (ComplexNumber.Re != value || ComplexNumber.Im != 0)
            {
                return true;
            }
            return false;
        }
    }
}
