using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathLib.Objects;

namespace MathLib.SolvingLinearSystem
{
    public abstract class SeidelMethod
    {
        public static Vector Solve(Matrix A, Vector b, double eps)
        {
            int rows = A.Rows;
            int columns = A.Columns;
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

            Matrix B = -1 * (L + D).InverseMatrix() * R;
            Vector c = (L + D).InverseMatrix() * b;

            Vector xPrev;
            Vector x = Vector.IdentityVector(rows);

            do
            {
                for (int i = 0; i < x.Dim; i++)
                {
                    Console.WriteLine($"x{i + 1}: " + x[i]);
                }
                xPrev = new Vector(x);
                for (int i = 0; i < rows; i++)
                {
                    x[i] = 0;
                    for (int j = 0; j < i; j++)
                    {
                        x[i] += B[i, j] * x[j];
                    }
                    for (int j = i; j < columns; j++)
                    {
                        x[i] += B[i, j] * xPrev[j];
                    }
                    x[i] += c[i];
                }
                Console.WriteLine((x - xPrev).Norm());
                Console.ReadKey();
            }
            while ((x - xPrev).Norm() >= eps);

            return x;
        }
    }
}
