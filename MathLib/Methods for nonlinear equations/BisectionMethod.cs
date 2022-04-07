using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace MathLib.SolvingNonlinearEquations
{
    public abstract class BisectionMethod
    {
		public delegate double Function(double x);

		private static double g(double x, Function f, List<double> s)
        {
			double result = f(x);
			for(int i = 0; i < s.Count; i++)
            {
				result /= (x - s[i]);
            }
			return result;
        }

		private static void NextSolve(double a, double b, double eps, Function f, ref List<double> solve)
        {
			if (g(a, f, solve) >= 0 && g(b, f, solve) <= 0 || g(a, f, solve) <= 0 && g(b, f, solve) >= 0)
			{
				double med = 0;
				double left = a;
				double right = b;
				if (g(a, f, solve) == 0)
				{
					solve.Add(a);
					NextSolve(a, b, eps, f, ref solve);
				}
				else if (g(b, f, solve) == 0)
				{
					solve.Add(b);
					NextSolve(a, b, eps, f, ref solve);
				}
				else
				{
					while (Abs(left - right) > eps)
					{
						med = (left + right) / 2;
						if (g(med, f, solve) > 0 && g(right, f, solve) < 0 || g(med, f, solve) < 0 && g(right, f, solve) > 0)
						{
							left = med;
						}
						else
						{
							right = med;
						}
					}
					solve.Add(med);
					NextSolve(a, b, eps, f, ref solve);
				}
			}
		}

		public static List<double> Solve(double a, double b, double eps, Function f)
        {
			List<double> solve = new List<double>();
			double left = a;
			double right = b;
			if (f(a) >= 0 && f(b) <= 0 || f(a) <= 0 && f(b) >= 0)
			{
				double med = 0;
				if (f(a) == 0)
				{
					solve.Add(a);
					NextSolve(a, b, eps, f, ref solve);
				}
				else if (f(b) == 0)
				{
					solve.Add(b);
					NextSolve(a, b, eps, f, ref solve);
				}
				while (Abs(left - right) > eps)
				{
					med = (left + right) / 2;
					if (f(med) > 0 && f(right) < 0 || f(med) < 0 && f(right) > 0)
					{
						left = med;
					}
					else
					{
						right = med;
					}
				}
				solve.Add(med);
				NextSolve(a, b, eps, f, ref solve);
			}
			return solve;
		}
    }
}
