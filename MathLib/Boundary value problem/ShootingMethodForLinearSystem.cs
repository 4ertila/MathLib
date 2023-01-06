using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathLib.Objects;
using MathLib.CauchyProblem;

namespace MathLib.BoundaryValueProblem
{
    public abstract class ShootingMethodForLinearSystem
    {
        public static Vector[] Solve(Function f, Function p, Function q, double a, double b,
                            double alpha, double beta, double A,
                            double delta, double gamma, double B, int n,
                            double[] alpha_RK, double[] A_RK, double[,] beta_RK, int p_RK)
        {
            double la_0, la_1, lb_0, lb_1;
            la_0 = la_1 = lb_0 = lb_1 = 0;
            if (alpha != 0)
            {
                la_0 = 0;
                la_1 = A / alpha;
                lb_0 = 1;
                lb_1 = -beta / alpha;
            }
            else if (beta != 0)
            {
                la_0 = A / beta;
                la_1 = 0;
                lb_0 = -alpha / beta;
                lb_1 = 1;
            }

            Vector[] result = new Vector[n];  
            Vector[] v = null;
            Vector[] w = null;
            Vector u0 = new Vector(la_0, la_1);
            VectorFunction u = new VectorFunction(new Function[]{ new Function("u2", "u2", "u1", "x"),
            new Function($"{f.infix}-({p.infix})*u2-({q.infix})*u1", "u2", "u1", "x")});

            //v = RungeKuttaMethod.Approximate(u, p_RK, alpha_RK, A_RK, u0, beta_RK, a, b, n);
            v = SecondOrderScheme.Approximate(0.5, u, u0, a, b, n);

            u0.Init(lb_0, lb_1);
            u = new VectorFunction(new Function[]{ new Function("u2", "u2", "u1", "x"),
            new Function($"-({p.infix})*u2-({q.infix})*u1", "u2", "u1", "x")});

            //w = RungeKuttaMethod.Approximate(u, p_RK, alpha_RK, A_RK, u0, beta_RK, a, b, n);
            w = SecondOrderScheme.Approximate(0.5, u, u0, a, b, n);

            double c = (B - delta * v[n - 1][1] - gamma * v[n - 1][0]) / (delta * w[n - 1][1] + gamma * w[n - 1][0]);
            for(int i = 0; i < n; i++)
            {
                result[i] = v[i] + c * w[i];
            }
            return result;
        }

        public static double Solve(Function f, Function p, Function q, double a, double b,
                            double alpha, double beta, double A,
                            double delta, double gamma, double B, double eps,
                            double[] alpha_RK, double[] A_RK, double[,] beta_RK, int p_RK)
        {           
            double la_0, la_1, lb_0, lb_1;
            la_0 = la_1 = lb_0 = lb_1 = 0;
            if (alpha != 0)
            {
                la_0 = 0;
                la_1 = A / alpha;
                lb_0 = 1;
                lb_1 = -beta / alpha;
            }
            else if(beta!= 0)
            {
                la_0 = A / beta;
                la_1 = 0;
                lb_0 = -alpha/beta;
                lb_1 = 1;
            }

            Function u1 = new Function("u1", "u1");
            Function u2 = new Function("u2", "u2");
            Vector v = new Vector();
            Vector w = new Vector();
            Vector u0 = new Vector(la_0, la_1);
            VectorFunction u = new VectorFunction(new Function[]{ new Function("u2", "u2", "u1", "x"),
            f - p * u2 - q * u1 });

            v = RungeKuttaMethod.Approximate(u, p_RK, alpha_RK, A_RK, u0, beta_RK, a, b, eps / 2);

            u0.Init(lb_0, lb_1);
            u = new VectorFunction(new Function[]{ new Function("u2", "u2", "u1", "x"),
            -p * u2 - q * u1});

            w = RungeKuttaMethod.Approximate(u, p_RK, alpha_RK, A_RK, u0, beta_RK, a, b, eps / 2);

            double c = (B - delta * v[1] - gamma * v[0]) / (delta * w[1] + gamma * w[0]);

            return v[0] + c * w[0];
        }
    }
}
