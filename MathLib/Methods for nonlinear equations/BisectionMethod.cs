using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathLib.Objects;
using static System.Math;

namespace MathLib.SolvingNonlinearEquations
{
    public abstract class BisectionMethod
    {
		private static Dictionary<string, double> variableA = new Dictionary<string, double>() { { "x", 0 } };
		private static Dictionary<string, double> variableB = new Dictionary<string, double>() { { "x", 0 } };
		private static Dictionary<string, double> variableMed = new Dictionary<string, double>() { { "x", 0 } };

		private static double g(double x, Function f, List<double> s)
        {
			double result = f.Calculate(new Dictionary<string, double>() { {"x", x} });
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

		public static List<double> Solve1(double a, double b, double eps, Function f)
        {
			List<double> solves = new List<double>();

			int n = 100;
			double h = (b - a) / n;
			while(h > eps)
            {
				n *= 2;
				h = (b - a) / n;
            }

			f["x"] = a;
			double prevY = f.Calculate();
			double currentY;
			for(int i = 1; i <= n; i++)
            {
				f["x"] = a + i * h;
				currentY = f.Calculate();
				if(currentY == 0)
                {
					solves.Add(a + i * h);
                }
				else if(prevY == 0)
                {
					solves.Add(a + (i - 1) * h);
                }
				else if(currentY * prevY < 0)
                {
					solves.Add(a + i * h - h / 2);
                }

				prevY = currentY;
            }

			return solves;
        }

		public static List<double> Solve(double a, double b, double eps, Function f)
        {
			List<double> solve = new List<double>();
			double left = a;
			double right = b;
			variableA["x"] = a;
			variableB["x"] = b;
			if (f.Calculate(variableA) >= 0 && f.Calculate(variableB) <= 0 ||
				f.Calculate(variableA) <= 0 && f.Calculate(variableB) >= 0)
			{
				double med = 0;
				if (f.Calculate(variableA) == 0)
				{
					solve.Add(a);
					NextSolve(a, b, eps, f, ref solve);
				}
				else if (f.Calculate(variableB) == 0)
				{
					solve.Add(b);
					NextSolve(a, b, eps, f, ref solve);
				}
				while (Abs(left - right) > eps)
				{
					med = (left + right) / 2;
					variableMed["x"] = med;
					if (f.Calculate(variableMed) > 0 && f.Calculate(variableB) < 0 ||
						f.Calculate(variableMed) < 0 && f.Calculate(variableB) > 0)
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
