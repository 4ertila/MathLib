using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathLib.Objects;

namespace MathLib.SolvingLinearSystem
{
    public abstract class GaussMethod
    {
        public static ComplexVector ComplexSolve(ComplexMatrix A, ComplexVector b)
        {
            int n = A.rows;
            Matrix reA = A.ReMatrix();
            Matrix imA = A.ImMatrix();
            Vector reB = b.ReVector();
            Vector imB = b.ImVector();

            Matrix C = new Matrix(2 * n, 2 * n);
            Vector d = new Vector(2 * n);
            
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    C[i, j] = reA[i, j];
                    C[i + n, j + n] = reA[i, j];
                    C[i, j + n] = -imA[i, j];
                    C[i + n, j] = imA[i, j];
                }
                d[i] = reB[i];
                d[i + n] = imB[i];
            }

            Vector y = Solve(C, d);

            ComplexVector x = new ComplexVector(n);
            for(int i = 0; i < n; i++)
            {
                x[i] = new ComplexNumber(y[i], y[i + n]);
            }
            return x;
        }

        public static ComplexVector GaussComplexSolve(ComplexMatrix coefComplexMatrix, ComplexVector freeComplexVector)
        {
            int rows = coefComplexMatrix.rows;
            int columns = coefComplexMatrix.columns;
            ComplexMatrix сComplexMatrix = new ComplexMatrix(coefComplexMatrix);
            ComplexVector fComplexVector = new ComplexVector(freeComplexVector);
            ComplexNumber tValue;

            for (int i = 0; i < rows - 1; i++)
            {
                if (сComplexMatrix[i, i] == 0)
                {
                    for (int j = i + 1; j < rows; j++)
                    {
                        if (сComplexMatrix[j, i] != 0)
                        {
                            for (int k = 0; k < columns; k++)
                            {
                                сComplexMatrix[i, k] += сComplexMatrix[j, k];
                            }
                            fComplexVector[i] += fComplexVector[j];
                            break;
                        }
                    }
                }
                for (int j = i + 1; j < rows; j++)
                {
                    if (сComplexMatrix[j, i] != 0)
                    {
                        tValue = сComplexMatrix[j, i] / сComplexMatrix[i, i];
                        for (int k = i; k < columns; k++)
                        {
                            сComplexMatrix[j, k] -= сComplexMatrix[i, k] * tValue;
                        }
                        fComplexVector[j] -= fComplexVector[i] * tValue;
                    }
                }
            }

            ComplexVector solutionComplexVector = new ComplexVector(columns);
            for (int i = columns - 1; i >= 0; i--)
            {
                for (int j = columns - 1; j > i; j--)
                {
                    fComplexVector[i] -= сComplexMatrix[i, j] * solutionComplexVector[j];
                }
                solutionComplexVector[i] = fComplexVector[i] / сComplexMatrix[i, i];
            }
            return solutionComplexVector;
        }

        public static Vector Solve(Matrix coefMatrix, Vector freeVector)
        {
            int rows = coefMatrix.rows;
            int columns = coefMatrix.columns;
            Matrix сMatrix = new Matrix(coefMatrix);
            Vector fVector = new Vector(freeVector);
            double tValue;

            for (int i = 0; i < rows - 1; i++)
            {
                if (сMatrix[i, i] == 0)
                {
                    for (int j = i + 1; j < rows; j++)
                    {
                        if (сMatrix[j, i] != 0)
                        {
                            for (int k = 0; k < columns; k++)
                            {
                                сMatrix[i, k] += сMatrix[j, k];
                            }
                            fVector[i] += fVector[j];
                            break;
                        }
                    }
                }
                for (int j = i + 1; j < rows; j++)
                {
                    if (сMatrix[j, i] != 0)
                    {
                        tValue = сMatrix[j, i] / сMatrix[i, i];
                        for (int k = i; k < columns; k++)
                        {
                            сMatrix[j, k] -= сMatrix[i, k] * tValue;
                        }
                        fVector[j] -= fVector[i] * tValue;
                    }
                }
            }

            Vector solutionVector = new Vector(columns);
            for (int i = columns - 1; i >= 0; i--)
            {
                for (int j = columns - 1; j > i; j--)
                {
                    fVector[i] -= сMatrix[i, j] * solutionVector[j];
                }
                solutionVector[i] = fVector[i] / сMatrix[i, i];
            }
            return solutionVector;
        }

        public static double[] Solve(double[,] coefMatrix, double[] freeVector)
        {
            int rows = coefMatrix.GetLength(0);
            int columns = coefMatrix.GetLength(1);
            double[,] сMatrix = new double[rows, columns];
            double[] fVector = new double[freeVector.Length];
            double[] solutionVector = new double[columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    сMatrix[i, j] = coefMatrix[i, j];
                }
                fVector[i] = freeVector[i];
            }
            double c;

            for (int i = 0; i < rows - 1; i++)
            {
                if (сMatrix[i, i] == 0)
                {
                    for (int j = i + 1; j < rows; j++)
                    {
                        if (сMatrix[j, i] != 0)
                        {
                            for (int k = 0; k < columns; k++)
                            {
                                сMatrix[i, k] += сMatrix[j, k];
                            }
                            fVector[i] += fVector[j];
                            break;
                        }
                    }
                }
                for (int j = i + 1; j < rows; j++)
                {
                    if (сMatrix[j, i] != 0)
                    {
                        c = сMatrix[j, i] / сMatrix[i, i];
                        for (int k = i; k < columns; k++)
                        {
                            сMatrix[j, k] -= сMatrix[i, k] * c;
                        }
                        fVector[j] -= fVector[i] * c;
                    }
                }
            }

            for (int i = columns - 1; i >= 0; i--)
            {
                for (int j = columns - 1; j > i; j--)
                {
                    fVector[i] -= сMatrix[i, j] * solutionVector[j];
                }
                solutionVector[i] = fVector[i] / сMatrix[i, i];
            }
            return solutionVector;
        }

        public static object Solve(RationalMatrix coefMatrix, RationalVector freeVector)
        {
            int rank = coefMatrix.Rank();
            //проверка на существование решения
            if (rank == coefMatrix.AddColumn(freeVector, coefMatrix.columns).Rank())
            {
                int rows = coefMatrix.rows;
                int columns = coefMatrix.columns;

                //проверка единственности решения
                if (rank < columns)
                {
                    RationalNumber[,] сMatrix = new RationalNumber[rows, columns];
                    RationalNumber[] fVector = new RationalNumber[freeVector.dim];
                    RationalNumber[,] solutionVector = new RationalNumber[rows, columns - rank];
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < columns; j++)
                        {
                            сMatrix[i, j] = new RationalNumber(coefMatrix[i, j]);
                        }
                        fVector[i] = new RationalNumber(freeVector[i]);
                    }
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < columns - rank; j++)
                        {
                            solutionVector[i, j] = new RationalNumber();
                        }
                    }
                    RationalNumber c;

                    for (int i = 0; i < rows - 1; i++)
                    {
                        for (int g = i; g < columns;)
                        {
                            if (coefMatrix[i, g] == 0)
                            {
                                for (int j = i + 1; j < rows; j++)
                                {
                                    if (coefMatrix[j, i] != 0)
                                    {
                                        for (int k = 0; k < columns; k++)
                                        {
                                            сMatrix[i, k] += сMatrix[j, k];
                                        }
                                        fVector[i] += fVector[j];
                                        break;
                                    }
                                    else if (j == rows - 1)
                                    {
                                        g++;
                                    }
                                }
                            }
                            else
                            {
                                for (int j = g + 1; j < rows; j++)
                                {
                                    c = coefMatrix[j, g] / coefMatrix[i, g];
                                    for (int k = i; k < columns; k++)
                                    {
                                        сMatrix[j, k] -= сMatrix[i, k] * c;
                                    }
                                    fVector[j] -= fVector[i] * c;
                                }
                                break;
                            }
                        }
                    }



                    return solutionVector;
                }
                else
                {
                    RationalNumber[,] сMatrix = new RationalNumber[rows, columns];
                    RationalNumber[] fVector = new RationalNumber[freeVector.dim];
                    RationalNumber[] solutionVector = new RationalNumber[columns];
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < columns; j++)
                        {
                            сMatrix[i, j] = new RationalNumber(coefMatrix[i, j]);
                        }
                        fVector[i] = new RationalNumber(freeVector[i]);
                    }
                    RationalNumber c;

                    for (int i = 0; i < rows - 1; i++)
                    {
                        if (сMatrix[i, i] == 0)
                        {
                            for (int j = i + 1; j < rows; j++)
                            {
                                if (сMatrix[j, i] != 0)
                                {
                                    for (int k = 0; k < columns; k++)
                                    {
                                        сMatrix[i, k] += сMatrix[j, k];
                                    }
                                    fVector[i] += fVector[j];
                                    break;
                                }
                            }
                        }
                        for (int j = i + 1; j < rows; j++)
                        {
                            if (сMatrix[j, i] != 0)
                            {
                                c = сMatrix[j, i] / сMatrix[i, i];
                                for (int k = i; k < columns; k++)
                                {
                                    сMatrix[j, k] -= сMatrix[i, k] * c;
                                }
                                fVector[j] -= fVector[i] * c;
                            }
                        }
                    }

                    for (int i = rank - 1; i >= 0; i--)
                    {
                        for (int j = rank - 1; j > i; j--)
                        {
                            fVector[i] -= сMatrix[i, j] * solutionVector[j];
                        }
                        solutionVector[i] = fVector[i] / сMatrix[i, i];
                    }
                    return new RationalVector(solutionVector);
                }
            }
            else
            {
                return null;
            }
        }
    }
}
