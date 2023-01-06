using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathLib.Objects;

namespace MathLib.SolvingLinearSystem
{
    public abstract class LU_Method
    {
        public static double Determinant(Matrix A)
        {
            double det = 1;
            int rows = A.rows;
            int columns = A.columns;
            Matrix L = new Matrix(rows, columns);
            Matrix U = new Matrix(rows, columns);

            for (int i = 0; i < rows; i++)
            {
                L[i, i] = 1;
                for (int j = 0; j < columns; j++)
                {
                    if (i > j)
                    {
                        L[i, j] = A[i, j] / U[j, j];
                        for (int k = 0; k <= j - 1; k++)
                        {
                            L[i, j] -= U[k, j] * L[i, k] / U[j, j];
                        }
                    }
                    else
                    {
                        U[i, j] = A[i, j];
                        for (int k = 0; k <= i - 1; k++)
                        {
                            U[i, j] -= U[k, j] * L[i, k];
                        }
                    }
                }
            }

            for(int i = 0; i < rows; i++)
            {
                det *= L[i, i] * U[i, i];
            }

            return det;
        }

        public static Vector Solve(Matrix A, Vector b)
        {
            int rows = A.rows;
            int columns = A.columns;
            Matrix L = new Matrix(rows, columns);
            Matrix U = new Matrix(rows, columns);

            for (int i = 0; i < rows; i++)
            {
                L[i, i] = 1;
                for (int j = 0; j < columns; j++)
                {
                    if (i > j)
                    {
                        L[i, j] = A[i, j] / U[j, j];
                        for (int k = 0; k <= j - 1; k++)
                        {
                            L[i, j] -= U[k, j] * L[i, k] / U[j, j];
                        }
                    }
                    else
                    {
                        U[i, j] = A[i, j];
                        for (int k = 0; k <= i - 1; k++)
                        {
                            U[i, j] -= U[k, j] * L[i, k];
                        }
                    }
                }
            }

            Vector y = new Vector(rows);
            for (int i = 0; i < rows; i++)
            {
                y[i] = b[i];
                for (int j = 0; j <= i - 1; j++)
                {
                    y[i] -= L[i, j] * y[j];
                }
            }

            Vector x = new Vector(rows);
            for (int i = 0; i < rows; i++)
            {
                int k = rows - 1 - i;
                x[k] = y[k] / U[k, k];
                for (int j = k + 1; j < rows; j++)
                {
                    x[k] -= U[k, j] * x[j] / U[k, k];
                }
            }

            return x;
        }

        public static double[] Solve(double[,] A, double[] b)
        {
            int rows = A.GetLength(0);
            int columns = A.GetLength(1);
            double[,] L = new double[rows, columns];
            double[,] U = new double[rows, columns];
            double[] y = new double[rows];
            double[] x = new double[rows];

            for (int i = 0; i < rows; i++)
            {
                L[i, i] = 1;
                for (int j = 0; j < columns; j++)
                {
                    if (i > j)
                    {
                        L[i, j] = A[i, j] / U[j, j];
                        for (int k = 0; k <= j - 1; k++)
                        {
                            L[i, j] -= U[k, j] * L[i, k] / U[j, j];
                        }
                    }
                    else
                    {
                        U[i, j] = A[i, j];
                        for (int k = 0; k <= i - 1; k++)
                        {
                            U[i, j] -= U[k, j] * L[i, k];
                        }
                    }
                }
            }

            for(int i = 0; i < rows; i++)
            {
                y[i] = b[i];
                for(int j = 0; j <= i - 1; j++)
                {
                    y[i] -= L[i, j] * y[j];
                }
            }

            for (int i = 0; i < rows; i++)
            {
                int k = rows - 1 - i;
                x[k] = y[k] / U[k, k];
                for (int j = k + 1; j < rows; j++)
                {
                    x[k] -= U[k, j] * x[j] / U[k, k];
                }
            }

            return x;
        }
    }
}
