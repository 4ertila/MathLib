using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;
using MathLib.Objects;

namespace MathLib.SolvingLinearSystem
{
    public abstract class SquareRootMethod
    {
        public static Vector Solve(Matrix A, Vector b)
        {
            int rows = A.Rows;
            int columns = A.Columns;
            Matrix D = new Matrix(rows, columns);
            Matrix S = new Matrix(rows, columns);

            for (int i = 0; i < rows; i++)
            {
                D[i, i] = A[i, i];
                for(int j = 0; j <= i - 1; j++)
                {
                    D[i, i] -= Pow(S[j, i], 2) * D[j, j];
                }
                D[i, i] = Sign(D[i, i]);

                S[i, i] = A[i, i];
                for (int j = 0; j <= i - 1; j++)
                {
                    S[i, i] -= Pow(S[j, i], 2) * D[j, j];
                }
                S[i, i] = Sqrt(Abs(S[i, i]));

                for (int j = i + 1; j < columns; j++)
                {
                    S[i, j] = A[i, j] / (D[i, i] * S[i, i]);
                    for (int k = 0; k <= i - 1; k++)
                    {
                        S[i, j] -= S[k, i] * D[k, k] * S[k, j] / (D[i, i] * S[i, i]);
                    }
                }
            }

            Matrix STD = S.Transpose() * D;
            Vector y = new Vector(rows);
            for (int i = 0; i < rows; i++)
            {
                y[i] = b[i] / STD[i, i];
                for (int j = 0; j <= i - 1; j++)
                {
                    y[i] -= STD[i, j] * y[j] / STD[i, i];
                }
            }

            Vector x = new Vector(rows);
            for (int i = 0; i < rows; i++)
            {
                int k = rows - 1 - i;
                x[k] = y[k] / S[k, k];
                for (int j = k + 1; j < rows; j++)
                {
                    x[k] -= S[k, j] * x[j] / S[k, k];
                }
            }

            return x;
        }
    }
}
