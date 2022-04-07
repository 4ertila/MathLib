using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathLib.Objects;

namespace MathLib.SolvingLinearSystem
{
    public abstract class SimpleIterationMethod
    {
        public static Vector Solve(Matrix A, Vector b, double eps)
        {
            Matrix E = Matrix.IdentityMatrix(A.Rows);
            Matrix B = E - 0.5 * E;
            double q = B.Norm();
            Vector c = 0.5 * A.InverseMatrix() * b;

            Vector xPrev = Vector.IdentityVector(A.Rows);
            Vector x = B * xPrev + c;

            while ((x - xPrev).Norm() >= (1 - q) * eps / q)
            {
                xPrev = x;
                x = B * xPrev + c;
            }

            return x;
        }
    }
}
