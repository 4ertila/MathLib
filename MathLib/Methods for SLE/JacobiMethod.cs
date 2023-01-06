using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;
using MathLib.Objects;

namespace MathLib.SolvingLinearSystem
{
    public abstract class JacobiMethod
    {
        public static Vector Solve(Matrix A, Vector b, double eps)
        {
            int rows = A.rows;
            int columns = A.columns;
            Matrix L = new Matrix(rows, columns);
            Matrix D = new Matrix(rows, columns);
            Matrix R = new Matrix(rows, columns);

            for (int i = 0; i < rows; i++)
            {
                D[i, i] = A[i, i];
                for (int j = i + 1; j < columns; j++)
                {
                    R[i, j] = A[i, j];
                    L[j, i] = A[j, i];
                }
            }

            Matrix B = -1 * D.InverseMatrix() * (L + R);
            Vector c = D.InverseMatrix() * b;

            Vector xPrev = Vector.IdentityVector(rows);
            Vector x = B * xPrev + c;

            while ((x - xPrev).Norm() >= eps)
            {
                xPrev = new Vector(x);
                x = B * xPrev + c;
            }

            return x;
        }
    }
}
