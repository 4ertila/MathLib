using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace MathLib.Objects
{
    public class RationalNumber : IComparer<RationalNumber>
    {
        public int m { private set; get; }
        public int n { private set; get; }

        public RationalNumber(int m, int n)
        {
            this.m = m;
            if (n > 0)
            {
                this.n = n;
            }
            else if (n == 0)
            {
                throw new DivideByZeroException();
            }
            else if (n < 0)
            {
                this.m *= -1;
                this.n *= -1;
            }
            Reduction();
        }

        public RationalNumber(int m)
        {
            this.m = m;
            n = 1;
        }

        public RationalNumber(RationalNumber frac)
        {
            m = frac.m;
            n = frac.n;
        }

        public RationalNumber()
        {
            m = 0;
            n = 1;
        }

        public override string ToString()
        {
            return $"{m}/{n}";
        }

        public override bool Equals(object obj)
        {
            if (!GetType().Equals(obj.GetType()) || obj == null)
            {
                return false;
            }
            else
            {
                RationalNumber value = (RationalNumber)obj;
                return this == value;
            }
        }

        public override int GetHashCode()
        {
            return n^m;
        }

        public static int Nod(int a, int b)
        {
            if (a == 0 || b == 0)
            {
                return Math.Max(Math.Abs(a), Math.Abs(b));
            }
            else
            {
                if (b == 0) return a;

                return Nod(b, a % b);
            }
        }

        private void Reduction()
        {
            int nod = Nod(m, n);
            m /= nod;
            n /= nod;
        }

        public static RationalNumber Abs(RationalNumber frac)
        {
            return new RationalNumber(Math.Abs(frac.m), frac.n);
        }

        public static RationalNumber Max(RationalNumber frac1, RationalNumber frac2)
        {
            if (frac1 >= frac2)
            {
                return frac1;
            }
            else
            {
                return frac2;
            }
        }

        public RationalNumber ReverseRational()
        {
            return new RationalNumber(Math.Sign(m) * n, Math.Abs(m));
        }

        public RationalNumber OppositeRational()
        {
            return new RationalNumber(-m, n);
        }

        public int Compare(RationalNumber x, RationalNumber y)
        {
            if(x == y)
            {
                return 0;
            }
            if(x > y)
            {
                return 1;
            }
            return -1;

        }

        public static RationalNumber operator *(RationalNumber frac1, RationalNumber frac2)
        {
            return new RationalNumber(frac1.m * frac2.m, frac1.n * frac2.n);
        }
        public static RationalNumber operator *(RationalNumber frac, int value)
        {
            return new RationalNumber(frac.m * value, frac.n);
        }
        public static RationalNumber operator *(int value, RationalNumber frac)
        {
            return new RationalNumber(frac.m * value, frac.n);
        }

        public static RationalNumber operator /(RationalNumber frac1, RationalNumber frac2)
        {
            return new RationalNumber(frac1 * frac2.ReverseRational());
        }
        public static RationalNumber operator /(RationalNumber frac, int value)
        {
            return new RationalNumber(frac.m * Math.Sign(value), frac.n * Math.Abs(value));
        }
        public static RationalNumber operator /(int value, RationalNumber frac)
        {
            return new RationalNumber(value * frac.ReverseRational());
        }

        public static RationalNumber operator +(RationalNumber frac1, RationalNumber frac2)
        {
            return new RationalNumber(frac1.m * frac2.n + frac1.n * frac2.m, frac1.n * frac2.n);
        }
        public static RationalNumber operator +(RationalNumber frac, int value)
        {
            return new RationalNumber(frac.m + value * frac.n, frac.n);
        }
        public static RationalNumber operator +(int value, RationalNumber frac)
        {
            return new RationalNumber(frac.m + value * frac.n, frac.n);
        }

        public static RationalNumber operator -(RationalNumber frac1, RationalNumber frac2)
        {
            return new RationalNumber(frac1 + frac2.OppositeRational());
        }
        public static RationalNumber operator -(RationalNumber frac, int value)
        {
            return new RationalNumber(frac + (-value));
        }
        public static RationalNumber operator -(int value, RationalNumber frac)
        {
            return new RationalNumber((-value) + frac);
        }

        public static bool operator ==(RationalNumber frac1, RationalNumber frac2)
        {
            return frac1.m == frac2.m && frac1.n == frac2.n;
        }
        public static bool operator ==(RationalNumber frac, int value)
        {
            return frac.m == value && frac.n == 1;
        }
        public static bool operator ==(int value, RationalNumber frac)
        {
            return frac.m == value && frac.n == 1;
        }

        public static bool operator !=(RationalNumber frac1, RationalNumber frac2)
        {
            return frac1.m != frac2.m || frac1.n != frac2.n;
        }
        public static bool operator !=(RationalNumber frac, int value)
        {
            return frac.m != value || frac.n != 1;
        }
        public static bool operator !=(int value, RationalNumber frac)
        {
            return frac.m != value || frac.n != 1;
        }

        public static bool operator >(RationalNumber frac1, RationalNumber frac2)
        {
            return frac1.m * frac2.n > frac2.m * frac1.n;
        }
        public static bool operator >(RationalNumber frac, int value)
        {
            return frac.m > frac.n * value;
        }
        public static bool operator >(int value, RationalNumber frac)
        {
            return frac.n * value > frac.m;
        }

        public static bool operator <(RationalNumber frac1, RationalNumber frac2)
        {
            return frac1.m * frac2.n < frac2.m * frac1.n;
        }
        public static bool operator <(RationalNumber frac, int value)
        {
            return frac.m < frac.n * value;
        }
        public static bool operator <(int value, RationalNumber frac)
        {
            return frac.n * value < frac.m;
        }

        public static bool operator >=(RationalNumber frac1, RationalNumber frac2)
        {
            return frac1.m * frac2.n >= frac2.m * frac1.n;
        }
        public static bool operator >=(RationalNumber frac, int value)
        {
            return frac.m >= frac.n * value;
        }
        public static bool operator >=(int value, RationalNumber frac)
        {
            return frac.n * value >= frac.m;
        }

        public static bool operator <=(RationalNumber frac1, RationalNumber frac2)
        {
            return frac1.m * frac2.n <= frac2.m * frac1.n;
        }
        public static bool operator <=(RationalNumber frac, int value)
        {
            return frac.m <= frac.n * value;
        }
        public static bool operator <=(int value, RationalNumber frac)
        {
            return frac.n * value <= frac.m;
        }
    }
}
