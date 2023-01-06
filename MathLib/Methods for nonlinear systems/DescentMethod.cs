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
        private static void SetVariables(Dictionary<string, double> box, IEnumerable<KeyValuePair<string, double>> variables, Vector values)
        {
            foreach (KeyValuePair<string, double> variable in variables)
            {
                box.Add(variable.Key, variable.Value);
            }
        }

        public static Vector Solve(VectorFunction f, VectorFunction[] df, double eps)
        {
            int n = f.dim;
            string textPhi = "";
            IEnumerable<KeyValuePair<string, double>> variables = f.Variables;
            int variableCount = variables.Count();
            for (int i = 0; i < n - 1; i++)
            {
                textPhi += $"({f[i].infix})^2+";
            }
            textPhi += $"({f[n - 1].infix})^2";
            Function Phi = new Function(textPhi, f.Variables);

            Vector x = Vector.IdentityVector(variableCount);
            Vector xPrev = new Vector(variableCount);
            Vector pPrev = new Vector(variableCount);
            Vector p = new Vector(variableCount);
            double a = 1;
            double phi;
            double phiPrev;

            Dictionary<string, double> variable_xPrev = new Dictionary<string, double>();
            Dictionary<string, double> variable_x = new Dictionary<string, double>();
            foreach (KeyValuePair<string, double> variable in f.Variables)
            {
                variable_xPrev.Add(variable.Key, 0);
                variable_x.Add(variable.Key, 0);
            }

            do
            {
                a = 1;

                xPrev.Init(x);
                SetVariables(variable_xPrev, variables, x);

                p.Init(new Vector(variableCount));
                Vector tVector = f.Calculate(variable_xPrev);
                for (int i = 0; i < variableCount; i++)
                {
                    p[i] += -2 * Vector.ScalarProduct(tVector, df[i].Calculate(variable_xPrev));
                }

                x.Init(xPrev + a * p);
                SetVariables(variable_x, variables, x);

                phi = Phi.Calculate(variable_x);
                phiPrev = Phi.Calculate(variable_xPrev);

                while (phi > phiPrev)
                {
                    a /= 2;

                    x.Init(xPrev + a * p);
                    SetVariables(variable_x, variables, x);

                    phi = Phi.Calculate(variable_x);
                }
            }
            while ((x - xPrev).Norm() > eps);

            return x;
        }
    }
}
