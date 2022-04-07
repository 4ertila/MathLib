using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathLib.Objects;

namespace MathLib.SolvingLinearSystem
{
    public abstract class GaussJordanMethod
    {
        public static Vector Solve(Matrix A, Vector b)
        {
            int rows = A.Rows;
            int columns = A.Columns;
            Matrix cMatrix = new Matrix(A);
            Vector fVector = new Vector(b);
            double tValue;

            for (int i = 0; i < rows - 1; i++)
            {
                if (cMatrix[i, i] == 0)
                {
                    for (int j = i + 1; j < rows; j++)
                    {
                        if (cMatrix[j, i] != 0)
                        {
                            for (int k = 0; k < columns; k++)
                            {
                                cMatrix[i, k] += cMatrix[j, k];
                            }
                            fVector[i] += fVector[j];
                            break;
                        }
                    }
                }
                for (int j = i + 1; j < rows; j++)
                {
                    if (cMatrix[j, i] != 0)
                    {
                        tValue = cMatrix[j, i] / cMatrix[i, i];
                        for (int k = i; k < columns; k++)
                        {
                            cMatrix[j, k] -= cMatrix[i, k] * tValue;
                        }
                        fVector[j] -= fVector[i] * tValue;
                    }
                }
            }

            for (int i = columns - 1; i > 0; i--)
            {
                for (int j = i - 1; j >= 0; j--)
                {
                    if (cMatrix[j, i] != 0)
                    {
                        tValue = cMatrix[j, i] / cMatrix[i, i];
                        cMatrix[j, i] -= cMatrix[i, i] * tValue;
                        fVector[j] -= fVector[i] * tValue;
                    }
                }
            }

            Vector solutionVector = new Vector(columns);
            for (int i = 0; i < columns; i++)
            {
                solutionVector[i] = fVector[i] / cMatrix[i, i];
            }
            return solutionVector;
        }
    }
}
