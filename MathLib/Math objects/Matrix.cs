using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;
using MathLib.SolvingLinearSystem;

namespace MathLib.Objects
{
    public class Matrix
    {
        private double[,] values;
        private int columns = 0;
        private int rows = 0;

        public int Rows
        {
            get
            {
                return rows;
            }
            set { }
        }

        public int Columns
        {
            get
            {
                return columns;
            }
        }

        public double this[int i, int j]
        {
            get
            {
                return values[i, j];
            }
            set
            {
                values[i, j] = value;
            }
        }


        public Matrix()
        {
            values = null;
            columns = 0;
            rows = 0;
        }

        public Matrix(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
            values = new double[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    values[i, j] = new double();
                }
            }
        }

        public Matrix(double[,] values)
        {
            rows = values.GetLength(0);
            columns = values.GetLength(1);
            this.values = new double[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    this.values[i, j] = values[i, j];
                }
            }
        }

        public Matrix(Matrix matrix)
        {
            rows = matrix.Rows;
            columns = matrix.Columns;
            values = new double[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    values[i, j] = matrix[i, j];
                }
            }
        }

        public Matrix(Vector[] vectors)
        {
            rows = vectors.Length;
            columns = vectors[0].Dim;
            values = new double[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    values[i, j] = vectors[i][j];
                }
            }
        }

        public void Init(Matrix matrix)
        {
            columns = matrix.Columns;
            rows = matrix.Rows;
            values = new double[rows, columns];
            for(int i = 0; i < rows; i++)
            {
                for(int j = 0; j < columns; j++)
                {
                    values[i, j] = matrix[i, j];
                }
            }
        }


        public double Min(out int row, out int column)
        {
            double min = double.MaxValue;
            row = 0;
            column = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (values[i, j] < min)
                    {
                        min = values[i, j];
                        row = i;
                        column = j;
                    }
                }
            }
            return min;
        }

        public double AbsMin(out int row, out int column)
        {
            double min = double.MaxValue;
            row = 0;
            column = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (Abs(values[i, j]) < min)
                    {
                        min = values[i, j];
                        row = i;
                        column = j;
                    }
                }
            }
            return min;
        }

        public double MinInRow(int row, out int column)
        {
            double min = double.MaxValue;
            column = 0;
            for (int i = 0; i < columns; i++)
            {
                if (values[row, i] < min)
                {
                    min = values[row, i];
                    column = i;
                }
            }
            return min;
        }

        public double MinInColumn(int column, out int row)
        {
            double min = double.MaxValue;
            row = 0;
            for (int i = 0; i < rows; i++)
            {
                if (values[i, column] < min)
                {
                    min = values[i, column];
                    row = i;
                }
            }
            return min;
        }

        public double OffDiagonalAbsMin(out int row, out int column)
        {
            double min = double.MaxValue;
            row = 0;
            column = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (Abs(values[i, j]) < min && i != j)
                    {
                        min = values[i, j];
                        row = i;
                        column = j;
                    }
                }
            }
            return min;
        }


        public double Max(out int row, out int column)
        {
            double max = double.MinValue;
            row = 0;
            column = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (values[i, j] > max)
                    {
                        max = values[i, j];
                        row = i;
                        column = j;
                    }
                }
            }
            return max;
        }

        public double MaxInRow(int row, out int column)
        {
            double max = double.MinValue;
            column = 0;
            for (int i = 0; i < columns; i++)
            {
                if (values[row, i] > max)
                {
                    max = values[row, i];
                    column = i;
                }
            }
            return max;
        }

        public double MaxInColumn(int column, out int row)
        {
            double max = double.MinValue;
            row = 0;
            for (int i = 0; i < rows; i++)
            {
                if (values[i, column] > max)
                {
                    max = values[i, column];
                    row = i;
                }
            }
            return max;
        }

        public double AbsMax(out int row, out int column)
        {
            double max = double.MinValue;
            row = 0;
            column = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (Abs(values[i, j]) > max)
                    {
                        max = values[i, j];
                        row = i;
                        column = j;
                    }
                }
            }
            return max;
        }

        public double OffDiagonalAbsMax(out int row, out int column)
        {
            double max = 0;
            row = 1;
            column = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (Abs(values[i, j]) > Abs(max) && i != j)
                    {
                        max = values[i, j];
                        row = i;
                        column = j;
                    }
                }
            }
            return max;
        }

        public double UpperDiagonalAbsMax(out int row, out int column)
        {
            double max = 0;
            row = 1;
            column = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = i + 1; j < columns; j++)
                {
                    if (Abs(values[i, j]) > Abs(max) && i != j)
                    {
                        max = values[i, j];
                        row = i;
                        column = j;
                    }
                }
            }
            return max;
        }

        public double DownDiagonalAbsMax(out int row, out int column)
        {
            double max = 0;
            row = 1;
            column = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = i + 1; j < columns; j++)
                {
                    if (Abs(values[j, i]) > Abs(max) && i != j)
                    {
                        max = values[j, i];
                        row = j;
                        column = i;
                    }
                }
            }
            return max;
        }


        public Vector GetColumn(int id)
        {
            Vector outVector = new Vector(rows);
            for(int i = 0; i < rows; i++)
            {
                outVector[i] = values[i, id];
            }
            return outVector;
        }

        public Matrix AddColumn(Vector vector)
        {
            if (vector is null)
            {
                throw new ArgumentException("Vector is null");
            }

            double[,] outMatrix = new double[vector.Dim, columns + 1];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    outMatrix[i, j] = values[i, j];
                }
                outMatrix[i, columns] = vector[i];
            }

            return new Matrix(outMatrix);
        }

        public Matrix AddColumn(Vector vector, int newMatrixId)
        {
            if (vector is null)
            {
                throw new ArgumentException("Vector is null");
            }

            double[,] outMatrix = new double[vector.Dim, columns + 1];
            for (int i = 0, k = 0; i < columns + 1; i++)
            {
                if (i == newMatrixId)
                {
                    for (int j = 0; j < vector.Dim; j++)
                    {
                        outMatrix[j, i] = vector[j];
                    }
                }
                else
                {
                    for (int j = 0; j < rows; j++)
                    {
                        outMatrix[j, i] = values[j, k];
                    }
                    k++;
                }
            }
            return new Matrix(outMatrix);
        }

        public Matrix AddColumns(Vector[] vector, int[] newMatrixId)
        {
            if (vector == null || newMatrixId == null)
            {
                return new Matrix(values);
            }

            double[,] outMatrix = new double[vector[0].Dim, columns + vector.Length];
            for (int i = 0, k = 0; i < columns + vector.Length; i++)
            {
                if (newMatrixId.Contains(i))
                {
                    int id = Array.BinarySearch(newMatrixId, i);
                    for (int j = 0; j < vector[0].Dim; j++)
                    {
                        outMatrix[j, i] = vector[id][j];
                    }
                }
                else
                {
                    for (int j = 0; j < rows; j++)
                    {
                        outMatrix[j, i] = values[j, k];
                    }
                    k++;
                }
            }
            return new Matrix(outMatrix);
        }

        public Matrix AddColumns(double[,] values, int[] newMatrixId)
        {
            if (values == null || newMatrixId == null)
            {
                return new Matrix(values);
            }

            double[,] outMatrix = new double[values.GetLength(0), columns + values.GetLength(1)];
            for (int i = 0, k = 0; i < columns + values.GetLength(1); i++)
            {
                if (newMatrixId.Contains(i))
                {
                    int id = Array.BinarySearch(newMatrixId, i);
                    for (int j = 0; j < values.GetLength(0); j++)
                    {
                        outMatrix[j, i] = values[id, j];
                    }
                }
                else
                {
                    for (int j = 0; j < rows; j++)
                    {
                        outMatrix[j, i] = this.values[j, k];
                    }
                    k++;
                }
            }
            return new Matrix(outMatrix);
        }

        public Matrix DeleteColumn(int id)
        {
            double[,] outMatrix = new double[rows, columns - 1];
            for (int i = 0, t = 0; i < columns; i++)
            {
                if (i != id)
                {
                    for (int j = 0; j < rows; j++)
                    {
                        outMatrix[j, t] = values[j, i];
                    }
                    t++;
                }
                else
                {
                    continue;
                }
            }
            return new Matrix(outMatrix);
        }

        public Matrix DeleteColumns(int[] id)
        {
            double[,] outMatrix = new double[rows, columns - id.Length];
            for (int i = 0, t = 0; i < columns; i++)
            {
                if (!id.Contains(i))
                {
                    for (int j = 0; j < rows; j++)
                    {
                        outMatrix[j, t] = values[j, i];
                    }
                    t++;
                }
                else
                {
                    continue;
                }
            }
            return new Matrix(outMatrix);
        }

        public Matrix SwapColumns(int id1, int id2)
        {
            double[,] outMatrix = new double[rows, columns];
            for (int i = 0; i < columns; i++)
            {
                if (i == id1)
                {
                    for (int j = 0; j < rows; j++)
                    {
                        outMatrix[j, i] = values[j, id2];
                    }
                }
                else if (i == id2)
                {
                    for (int j = 0; j < rows; j++)
                    {
                        outMatrix[j, i] = values[j, id1];
                    }
                }
                else
                {
                    for (int j = 0; j < rows; j++)
                    {
                        outMatrix[j, i] = values[j, i];
                    }
                }
            }
            return new Matrix(outMatrix);
        }


        public Vector GetRow(int id)
        {
            Vector outVector = new Vector(columns);
            for (int i = 0; i < columns; i++)
            {
                outVector[i] = values[id, i];
            }
            return outVector;
        }

        public Matrix AddRow(Vector vector)
        {
            if (vector is null)
            {
                throw new ArgumentException("Vector is null");
            }

            double[,] outMatrix = new double[vector.Dim + 1, columns];
            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    outMatrix[j, i] = values[j, i];
                }
                outMatrix[rows, i] = vector[i];
            }

            return new Matrix(outMatrix);
        }

        public Matrix AddRow(Vector vector, int newMatrixId)
        {
            if (vector is null)
            {
                throw new ArgumentException("Vector is null");
            }

            double[,] outMatrix = new double[rows + 1, vector.Dim];
            for (int i = 0, k = 0; i < rows + 1; i++)
            {
                if (i == newMatrixId)
                {
                    for (int j = 0; j < vector.Dim; j++)
                    {
                        outMatrix[i, j] = vector[j];
                    }
                }
                else
                {
                    for (int j = 0; j < columns; j++)
                    {
                        outMatrix[i, j] = values[k, j];
                    }
                    k++;
                }
            }
            return new Matrix(outMatrix);
        }

        public Matrix AddRows(Vector[] vector, int[] newMatrixId)
        {
            if (vector == null || newMatrixId == null)
            {
                return new Matrix(values);
            }

            double[,] outMatrix = new double[rows + vector.Length, vector[0].Dim];
            for (int i = 0, k = 0; i < rows + vector.Length; i++)
            {
                if (newMatrixId.Contains(i))
                {
                    int id = Array.BinarySearch(newMatrixId, i);
                    for (int j = 0; j < vector[0].Dim; j++)
                    {
                        outMatrix[i, j] = vector[j][id];
                    }
                }
                else
                {
                    for (int j = 0; j < rows; j++)
                    {
                        outMatrix[i, j] = values[k, j];
                    }
                    k++;
                }
            }
            return new Matrix(outMatrix);
        }

        public Matrix AddRows(double[,] values, int[] newMatrixId)
        {
            if (values == null || newMatrixId == null)
            {
                return new Matrix(values);
            }

            double[,] outMatrix = new double[rows + values.GetLength(0), values.GetLength(1)];
            for (int i = 0, k = 0; i < rows + values.GetLength(0); i++)
            {
                if (newMatrixId.Contains(i))
                {
                    int id = Array.BinarySearch(newMatrixId, i);
                    for (int j = 0; j < values.GetLength(1); j++)
                    {
                        outMatrix[i, j] = values[j, id];
                    }
                }
                else
                {
                    for (int j = 0; j < rows; j++)
                    {
                        outMatrix[i, j] = this.values[k, j];
                    }
                    k++;
                }
            }
            return new Matrix(outMatrix);
        }

        public Matrix DeleteRow(int id)
        {
            double[,] outMatrix = new double[rows - 1, columns];
            for (int i = 0, t = 0; i < rows; i++)
            {
                if (i != id)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        outMatrix[t, j] = values[i, j];
                    }
                    t++;
                }
                else
                {
                    continue;
                }
            }
            return new Matrix(outMatrix);
        }

        public Matrix DeleteRows(int[] id)
        {
            double[,] outMatrix = new double[rows - id.Length, columns];
            for (int i = 0, t = 0; i < rows; i++)
            {
                if (!id.Contains(i))
                {
                    for (int j = 0; j < columns; j++)
                    {
                        outMatrix[t, j] = values[i, j];
                    }
                    t++;
                }
                else
                {
                    continue;
                }
            }
            return new Matrix(outMatrix);
        }

        public Matrix SwapRows(int id1, int id2)
        {
            double[,] outMatrix = new double[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                if (i == id1)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        outMatrix[i, j] = values[id2, j];
                    }
                }
                else if (i == id2)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        outMatrix[i, j] = values[id1, j];
                    }
                }
                else
                {
                    for (int j = 0; j < columns; j++)
                    {
                        outMatrix[i, j] = values[i, j];
                    }
                }
            }
            return new Matrix(outMatrix);
        }

        public static void MaxEigenvalue(Matrix A, double eps, out double eigenvalue, out Vector eigenvector)
        {
            int dim = A.Rows;

            Vector xPrev;
            Vector x = A.GetColumn(0);

            double lambda = 0;
            double lambdaLast;

            do
            {
                xPrev = new Vector(x);
                x = A * xPrev;

                lambdaLast = lambda;
                lambda = Vector.ScalarProduct(x, x) / Vector.ScalarProduct(x, xPrev);

                x *= 1 / x.Norm();
            }
            while (Abs(lambda - lambdaLast) > eps);

            eigenvalue = lambda;
            eigenvector = x;
        }

        public static void MinEigenvalue(Matrix A, double eps, out double eigenvalue, out Vector eigenvector)
        {
            double lambda; 
            MaxEigenvalue(A.InverseMatrix(), eps, out lambda, out eigenvector);
            eigenvalue = 1 / lambda;
            eigenvector *= 1 / eigenvector.Norm();
        }

        public static void AllEigenValuesNVectors(Matrix A, double eps, out double[] eigenvalues, out Vector[] eigenvectors)
        {
            int dim = A.rows;
            eigenvalues = new double[dim];
            eigenvectors = new Vector[dim];
            Matrix Q;
            Matrix BPrev = new Matrix(A);
            Matrix B = new Matrix(A);
            Matrix X = IdentityMatrix(dim);
            int i, j;
            double a, s, c;

            do
            {
                BPrev = B;
                BPrev.OffDiagonalAbsMax(out i, out j);
                a = Atan(2 * BPrev[i, j] / (BPrev[i, i] - BPrev[j, j])) / 2;
                Q = IdentityMatrix(dim);
                s = Sin(a);
                c = Cos(a);
                Q[i, j] = Sign(i - j) * s;
                Q[j, i] = Sign(j - i) * s;
                Q[i, i] = Q[j, j] = c;
                X *= Q;
                B = Q.Transpose() * BPrev * Q;
            }
            while (Abs(B.OffDiagonalAbsMax(out _, out _)) > eps);

            for(int k = 0; k < dim; k++)
            {
                eigenvalues[k] = B[k, k];
                eigenvectors[k] = X.GetColumn(k);
            }
        }

        public static void AllEigenvalues(Matrix A, double eps, out double[] eigenvalues)
        {
            int dim = A.Rows;
            eigenvalues = new double[dim];
            Matrix L = new Matrix(dim, dim);
            Matrix U = new Matrix(dim, dim);
            Matrix C = new Matrix(A);

            do
            {
                for (int i = 0; i < dim; i++)
                {
                    L[i, i] = 1;
                    for (int j = 0; j < dim; j++)
                    {
                        if (i > j)
                        {
                            L[i, j] = C[i, j] / U[j, j];
                            for (int k = 0; k <= j - 1; k++)
                            {
                                L[i, j] -= U[k, j] * L[i, k] / U[j, j];
                            }
                        }
                        else
                        {
                            U[i, j] = C[i, j];
                            for (int k = 0; k <= i - 1; k++)
                            {
                                U[i, j] -= U[k, j] * L[i, k];
                            }
                        }
                    }
                }
                C = U * L;
            }
            while (Abs(C.DownDiagonalAbsMax(out _, out _)) >= eps);

            for (int k = 0; k < dim; k++)
            {
                eigenvalues[k] = C[k, k];
            }
        }

        public static Matrix IdentityMatrix(int dim)
        {
            double[,] outMatrix = new double[dim, dim];
            for(int i = 0; i < dim; i++)
            {
                outMatrix[i, i] = 1;
            }
            return new Matrix(outMatrix);
        }

        public double Determinant()
        {
            if (rows == columns)
            {
                double[,] coefMatrix = new double[rows, columns];
                Array.Copy(values, coefMatrix, rows * columns);
                double det = 1;
                double c;
                for (int i = 0; i < rows - 1; i++)
                {
                    if (coefMatrix[i, i] == 0)
                    {
                        for (int j = i + 1; j < rows; j++)
                        {
                            if (coefMatrix[j, i] != 0)
                            {
                                for (int k = 0; k < columns; k++)
                                {
                                    coefMatrix[i, k] += coefMatrix[j, k];
                                }
                                i--;
                                break;
                            }
                            else if (j == rows - 1)
                            {
                                return new double();
                            }
                        }
                    }
                    else
                    {
                        for (int j = i + 1; j < rows; j++)
                        {
                            if (coefMatrix[j, i] != 0)
                            {
                                c = coefMatrix[j, i] / coefMatrix[i, i];
                                for (int k = i; k < columns; k++)
                                {
                                    coefMatrix[j, k] -= coefMatrix[i, k] * c;
                                }
                            }
                        }
                    }
                }

                for (int i = 0; i < rows; i++)
                {
                    det *= coefMatrix[i, i];
                }

                return det;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public int Rank()
        {
            double[,] coefMatrix = new double[rows, columns];
            Array.Copy(values, coefMatrix, rows * columns);
            int r = 0;
            double c;

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
                                    coefMatrix[i, k] += coefMatrix[j, k];
                                }
                                i = g;
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
                        for (int j = i + 1; j < rows; j++)
                        {
                            c = coefMatrix[j, g] / coefMatrix[i, g];
                            for (int k = i; k < columns; k++)
                            {
                                coefMatrix[j, k] -= coefMatrix[i, k] * c;
                            }
                        }
                        break;
                    }
                }
            }

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (coefMatrix[i, j] != 0)
                    {
                        r++;
                        break;
                    }
                }
            }
            return r;
        }

        public Matrix Transpose()
        {
            double[,] outMatrix = new double[columns, rows];

            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    outMatrix[i, j] = values[j, i];
                }
            }

            return new Matrix(outMatrix);
        }

        public double Norm()
        {
            double r = new double();
            double maxRational = new double();
            for (int i = 0; i < rows; i++)
            {
                r = new double();
                for (int j = 0; j < columns; j++)
                {
                    r += Abs(values[i, j]);
                }
                if (r > maxRational)
                {
                    maxRational = r;
                }
            }
            return maxRational;
        }

        public double Step()
        {
            if (rows == columns)
            {
                double outRational = new double();
                for (int i = 0; i < rows; i++)
                {
                    outRational += values[i, i];
                }
                return outRational;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public Matrix InverseMatrix()
        {
            if (Determinant() != 0)
            {
                double[,] coefMatrix = new double[rows, columns];
                Array.Copy(values, coefMatrix, rows * columns);
                double[,] inverseMatrix = new double[rows, columns];
                double c;
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        if (j != i)
                        {
                            inverseMatrix[i, j] = new double();
                        }
                    }
                    inverseMatrix[i, i] = 1;
                }

                for (int i = 0; i <= rows - 1; i++)
                {
                    if (coefMatrix[i, i] == 0)
                    {
                        for (int j = i + 1; j < rows; j++)
                        {
                            if (coefMatrix[j, i] != 0)
                            {
                                for (int k = 0; k < columns; k++)
                                {
                                    coefMatrix[i, k] += coefMatrix[j, k];
                                    inverseMatrix[i, k] += inverseMatrix[j, k];
                                }
                                break;
                            }
                        }
                    }
                    //т.к. определитель не равен 0 диагональный элемент всегда можно поменять к значению не равному 0
                    c = 1 / coefMatrix[i, i];
                    for (int j = 0; j < columns; j++)
                    {
                        coefMatrix[i, j] *= c;
                        inverseMatrix[i, j] *= c;
                    }
                    for (int j = i + 1; j < rows; j++)
                    {
                        if (coefMatrix[j, i] != 0)
                        {
                            c = coefMatrix[j, i];
                            for (int k = 0; k < columns; k++)
                            {
                                coefMatrix[j, k] -= coefMatrix[i, k] * c;
                                inverseMatrix[j, k] -= inverseMatrix[i, k] * c;
                            }
                        }
                    }
                }

                for (int i = rows - 1; i > 0; i--)
                {
                    for (int j = i - 1; j >= 0; j--)
                    {
                        c = coefMatrix[j, i];
                        for (int k = columns - 1; k >= 0; k--)
                        {
                            inverseMatrix[j, k] -= inverseMatrix[i, k] * c;
                        }
                    }
                }

                return new Matrix(inverseMatrix);
            }
            else
            {
                return null;
            }
        }

        public static Matrix InverseMatrix1(Matrix A)
        {
            Matrix matrix = new Matrix();
            Vector e;
            for (int i = 0; i < A.Columns; i++)
            {
                e = new Vector(A.Rows);
                e[i] = 1;
                matrix = matrix.AddColumn(GaussMethod.Solve(A, e));
            }
            return matrix;
        }

        public static Matrix operator *(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.columns == matrix2.rows)
            {
                double[,] outMatrix = new double[matrix1.rows, matrix2.columns];
                for (int i = 0; i < matrix1.rows; i++)
                {
                    for (int j = 0; j < matrix2.columns; j++)
                    {
                        outMatrix[i, j] = new double();
                        for (int k = 0; k < matrix1.columns; k++)
                        {
                            outMatrix[i, j] += matrix1[i, k] * matrix2[k, j];
                        }
                    }
                }
                return new Matrix(outMatrix);
            }
            else
            {
                throw new ArgumentException("the number of columns of matrix 1 does not equal with the number of rows of matrix 2 ");
            }
        }

        public static Matrix operator *(Matrix matrix, double value)
        {
            double[,] outMatrix = new double[matrix.rows, matrix.columns];
            for (int i = 0; i < matrix.rows; i++)
            {
                for (int j = 0; j < matrix.columns; j++)
                {
                    outMatrix[i, j] = matrix[i, j] * value;
                }
            }
            return new Matrix(outMatrix);
        }

        public static Matrix operator *(double value, Matrix matrix)
        {
            double[,] outMatrix = new double[matrix.rows, matrix.columns];
            for (int i = 0; i < matrix.rows; i++)
            {
                for (int j = 0; j < matrix.columns; j++)
                {
                    outMatrix[i, j] = matrix[i, j] * value;
                }
            }
            return new Matrix(outMatrix);
        }

        public static Vector operator *(Matrix matrix, Vector vector)
        {
            if (matrix.columns == vector.Dim)
            {
                double[] outVector = new double[matrix.rows];
                for (int i = 0; i < matrix.rows; i++)
                {
                    outVector[i] = new double();
                    for (int j = 0; j < matrix.columns; j++)
                    {
                        outVector[i] += matrix[i, j] * vector[j];
                    }
                }
                return new Vector(outVector);
            }
            else
            {
                throw new ArgumentException("The number of columns of matrix1 does not equal dimnesions of vector");
            }
        }

        public static Vector operator *(Vector vector, Matrix matrix)
        {
            if (matrix.columns == vector.Dim && matrix.Rows == 1)
            {
                double[] outVector = new double[matrix.rows];
                for (int i = 0; i < matrix.rows; i++)
                {
                    outVector[i] = new double();
                    for (int j = 0; j < matrix.columns; j++)
                    {
                        outVector[i] += matrix[i, j] * vector[j];
                    }
                }
                return new Vector(outVector);
            }
            else
            {
                throw new ArgumentException("The number of columns of matrix1 does not equal dimnesions of vector");
            }
        }


        public static Matrix operator +(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.rows == matrix2.rows && matrix1.columns == matrix2.columns)
            {
                double[,] outMatrix = new double[matrix1.rows, matrix1.columns];
                for (int i = 0; i < matrix1.rows; i++)
                {
                    for (int j = 0; j < matrix2.columns; j++)
                    {
                        outMatrix[i, j] = matrix1[i, j] + matrix2[i, j];
                    }
                }
                return new Matrix(outMatrix);
            }
            else
            {
                throw new ArgumentException("The size of matrix1 is not the same as the size of matrix2 ");
            }
        }

        public static Matrix operator -(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.rows == matrix2.rows && matrix1.columns == matrix2.columns)
            {
                return matrix1 + matrix2 * (-1);
            }
            else
            {
                throw new ArgumentException("The size of matrix1 is not the same as the size of matrix2 ");
            }
        }

        public static bool operator ==(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.rows == matrix2.rows && matrix1.columns == matrix2.columns)
            {
                for (int i = 0; i < matrix1.rows; i++)
                {
                    for (int j = 0; j < matrix1.columns; j++)
                    {
                        if (matrix1[i, j] != matrix2[i, j])
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool operator !=(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.rows == matrix2.rows && matrix1.columns == matrix2.columns)
            {
                for (int i = 0; i < matrix1.rows; i++)
                {
                    for (int j = 0; j < matrix1.columns; j++)
                    {
                        if (matrix1[i, j] != matrix2[i, j])
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
