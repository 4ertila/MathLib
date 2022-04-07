using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathLib.Objects;
using static System.Math;

namespace MathLib.SolvingNonlinearSystem
{
    public abstract class DescentMethod
    {
        public delegate double Function(Vector x);

        public static Vector Solve(double eps, Function f, Function g, 
                                    Function dfdx, Function dfdy,
                                    Function dgdx, Function dgdy)
        {
            Vector xPrev = new Vector(2);
            Vector x = new Vector(new double[] { 1, 1 });
            Vector pPrev = new Vector(2);
            Vector p = new Vector(2);
            double a = 1;
            double phi;
            double phiPrev;

            do
            {
                a = 1;
                xPrev[0] = x[0];
                xPrev[1] = x[1];

                p[0] = - 2 * f(xPrev) * dfdx(xPrev) - 2 * g(xPrev) * dgdx(xPrev);
                p[1] = - 2 * f(xPrev) * dfdy(xPrev) - 2 * g(xPrev) * dgdy(xPrev);

                x[0] = xPrev[0] + a * p[0];
                x[1] = xPrev[1] + a * p[1];

                phi = Pow(f(x), 2) + Pow(g(x), 2);
                phiPrev = Pow(f(xPrev), 2) + Pow(g(xPrev), 2);

                while (phi > phiPrev)
                {
                    a /= 2;

                    x[0] = xPrev[0] + a * p[0];
                    x[1] = xPrev[1] + a * p[1];

                    phi = Pow(f(x), 2) + Pow(g(x), 2);
                }
            }
            while ((x - xPrev).Norm() > eps);

            return x;
        }            
    }
}
