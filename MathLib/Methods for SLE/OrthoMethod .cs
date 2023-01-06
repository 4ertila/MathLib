using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathLib.Objects;

namespace MathLib.SolvingLinearSystem
{
    public abstract class OrthoMethod
    {
        public static Vector Solve(Matrix A, Vector b)
        {
            int rows = A.rows;
            int columns = A.columns;
            Matrix C = new Matrix(A);

            C = C.AddColumn(b * (-1));
            C = C.AddRow(new Vector(C.columns));
            C[rows, columns] = 1;

            Vector u = C.GetRow(0);

            Vector[] v = new Vector[columns + 1];
            v[0] = u * (1 / u.Norm());

            for (int i = 1; i < v.Length; i++)
            {
                u = C.GetRow(i);
                for (int j = 0; j <= i - 1; j++)
                {
                    u -= v[j] * Vector.ScalarProduct(v[j], C.GetRow(i));
                }
                v[i] = u * (1 / u.Norm());
            }
            return (u * (1 / u[columns])).RemoveElement();
        }
    }
}
