using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathLib.Objects;

namespace MathLib.SolvingNonlinearSystem
{
    public abstract class TangentMethod
    {
        public static Vector Solve(VectorFunction f, MatrixFunction jacobiMatrix, double eps)
        {
            IEnumerable<KeyValuePair<string, double>> variables = f.Variables;
            int variablesCount = variables.Count();
            MatrixFunction I = new MatrixFunction(jacobiMatrix);
            Vector x = Vector.IdentityVector(I.rows);
            Vector xPrev = new Vector();
            Dictionary<string, double> variable_xPrev = new Dictionary<string, double>();
            foreach(KeyValuePair<string, double> variable in variables)
            {
                variable_xPrev.Add(variable.Key, 0);
            }

            do
            {
                xPrev.Init(x);
                foreach (var variable in variables.Select((value, i) => new { value, i }))
                {
                    variable_xPrev[variable.value.Key] = xPrev[variable.i];
                }

                x = xPrev - I.Calculate(variable_xPrev).InverseMatrix() * f.Calculate(variable_xPrev);
            }
            while ((x - xPrev).Norm() >= eps);

            return x;
        }
    }
}
