using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathLib.Objects
{
    public class RationalMatrix
    {
        private RationalNumber[,] values;
        public int columns { private set; get; }
        public int rows { private set; get; }

        public RationalNumber this[int i, int j]
        {
            set
            {
                values[i, j] = new RationalNumber(value);
            }
            get
            {
                return new RationalNumber(values[i, j]);
            }
        }

        public RationalMatrix()
        {
            values = null;
            columns = 0;
            rows = 0;
        }
        public RationalMatrix(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
            values = new RationalNumber[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    values[i, j] = new RationalNumber();
                }
            }
        }
        public RationalMatrix(RationalNumber[,] values)
        {
            rows = values.GetLength(0);
            columns = values.GetLength(1);
            this.values = new RationalNumber[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    this.values[i, j] = new RationalNumber(values[i, j]);
                }
            }
        }
        public RationalMatrix(RationalMatrix matrix)
        {
            rows = matrix.rows;
            columns = matrix.columns;
            values = new RationalNumber[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    values[i, j] = new RationalNumber(matrix[i, j]);
                }
            }
        }
        public RationalMatrix(RationalVector[] vectors)
        {
            rows = vectors.Length;
            columns = vectors[0].dim;
            values = new RationalNumber[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    values[i, j] = new RationalNumber(vectors[i][j]);
                }
            }
        }

        public RationalNumber Min()
        {
            RationalNumber min = new RationalNumber(int.MaxValue);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (values[i, j] < min)
                    {
                        min = values[i, j];
                    }
                }
            }
            return min;
        }

        public RationalNumber Max()
        {
            RationalNumber max = new RationalNumber(int.MinValue);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (values[i, j] > max)
                    {
                        max = values[i, j];
                    }
                }
            }
            return max;
        }


        public RationalMatrix AddColumn(RationalVector vector, int newMatrixId)
        {
            if (vector == null)
            {
                return new RationalMatrix(values);
            }

            RationalNumber[,] outMatrix = new RationalNumber[vector.dim, columns + 1];
            for (int i = 0, k = 0; i < columns + 1; i++)
            {
                if (i == newMatrixId)
                {
                    for (int j = 0; j < vector.dim; j++)
                    {
                        outMatrix[j, i] = new RationalNumber(vector[j]);
                    }
                }
                else
                {
                    for (int j = 0; j < rows; j++)
                    {
                        outMatrix[j, i] = new RationalNumber(values[j, k]);
                    }
                    k++;
                }
            }
            return new RationalMatrix(outMatrix);
        }

        public RationalMatrix AddColumns(RationalVector[] vector, int[] newMatrixId)
        {
            if (vector == null || newMatrixId == null)
            {
                return new RationalMatrix(values);
            }

            RationalNumber[,] outMatrix = new RationalNumber[vector[0].dim, columns + vector.Length];
            for (int i = 0, k = 0; i < columns + vector.Length; i++)
            {
                if (newMatrixId.Contains(i))
                {
                    int id = Array.BinarySearch(newMatrixId, i);
                    for (int j = 0; j < vector[0].dim; j++)
                    {
                        outMatrix[j, i] = new RationalNumber(vector[id][j]);
                    }
                }
                else
                {
                    for (int j = 0; j < rows; j++)
                    {
                        outMatrix[j, i] = new RationalNumber(values[j, k]);
                    }
                    k++;
                }
            }
            return new RationalMatrix(outMatrix);
        }
        public RationalMatrix AddColumns(RationalNumber[,] values, int[] newMatrixId)
        {
            if (values == null || newMatrixId == null)
            {
                return new RationalMatrix(values);
            }

            RationalNumber[,] outMatrix = new RationalNumber[values.GetLength(0), columns + values.GetLength(1)];
            for (int i = 0, k = 0; i < columns + values.GetLength(1); i++)
            {
                if (newMatrixId.Contains(i))
                {
                    int id = Array.BinarySearch(newMatrixId, i);
                    for (int j = 0; j < values.GetLength(0); j++)
                    {
                        outMatrix[j, i] = new RationalNumber(values[id, j]);
                    }
                }
                else
                {
                    for (int j = 0; j < rows; j++)
                    {
                        outMatrix[j, i] = new RationalNumber(this.values[j, k]);
                    }
                    k++;
                }
            }
            return new RationalMatrix(outMatrix);
        }

        public RationalMatrix DeleteColumn(int id)
        {
            RationalNumber[,] outMatrix = new RationalNumber[rows, columns - 1];
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
            return new RationalMatrix(outMatrix);
        }

        public RationalMatrix DeleteColumns(int[] id)
        {
            RationalNumber[,] outMatrix = new RationalNumber[rows, columns - id.Length];
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
            return new RationalMatrix(outMatrix);
        }

        public RationalMatrix SwapColumns(int id1, int id2)
        {
            RationalNumber[,] outMatrix = new RationalNumber[rows, columns];
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
            return new RationalMatrix(outMatrix);
        }


        public RationalMatrix AddRow(RationalVector vector, int newMatrixId)
        {
            if (vector == null)
            {
                return new RationalMatrix(values);
            }

            RationalNumber[,] outMatrix = new RationalNumber[rows + 1, vector.dim];
            for (int i = 0, k = 0; i < rows + 1; i++)
            {
                if (i == newMatrixId)
                {
                    for (int j = 0; j < vector.dim; j++)
                    {
                        outMatrix[i, j] = new RationalNumber(vector[j]);
                    }
                }
                else
                {
                    for (int j = 0; j < columns; j++)
                    {
                        outMatrix[i, j] = new RationalNumber(values[k, j]);
                    }
                    k++;
                }
            }
            return new RationalMatrix(outMatrix);
        }

        public RationalMatrix AddRows(RationalVector[] vector, int[] newMatrixId)
        {
            if (vector == null || newMatrixId == null)
            {
                return new RationalMatrix(values);
            }

            RationalNumber[,] outMatrix = new RationalNumber[rows + vector.Length, vector[0].dim];
            for (int i = 0, k = 0; i < rows + vector.Length; i++)
            {
                if (newMatrixId.Contains(i))
                {
                    int id = Array.BinarySearch(newMatrixId, i);
                    for (int j = 0; j < vector[0].dim; j++)
                    {
                        outMatrix[i, j] = new RationalNumber(vector[j][id]);
                    }
                }
                else
                {
                    for (int j = 0; j < rows; j++)
                    {
                        outMatrix[i, j] = new RationalNumber(values[k, j]);
                    }
                    k++;
                }
            }
            return new RationalMatrix(outMatrix);
        }
        public RationalMatrix AddRows(RationalNumber[,] values, int[] newMatrixId)
        {
            if (values == null || newMatrixId == null)
            {
                return new RationalMatrix(values);
            }

            RationalNumber[,] outMatrix = new RationalNumber[rows + values.GetLength(0), values.GetLength(1)];
            for (int i = 0, k = 0; i < rows + values.GetLength(0); i++)
            {
                if (newMatrixId.Contains(i))
                {
                    int id = Array.BinarySearch(newMatrixId, i);
                    for (int j = 0; j < values.GetLength(1); j++)
                    {
                        outMatrix[i, j] = new RationalNumber(values[j, id]);
                    }
                }
                else
                {
                    for (int j = 0; j < rows; j++)
                    {
                        outMatrix[i, j] = new RationalNumber(this.values[k, j]);
                    }
                    k++;
                }
            }
            return new RationalMatrix(outMatrix);
        }

        public RationalMatrix SwapRows(int id1, int id2)
        {
            RationalNumber[,] outMatrix = new RationalNumber[rows, columns];
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
            return new RationalMatrix(outMatrix);
        }


        public RationalNumber Determinant()
        {
            if (rows == columns)
            {
                RationalNumber[,] coefMatrix = new RationalNumber[rows, columns];
                Array.Copy(values, coefMatrix, rows * columns);
                RationalNumber det = new RationalNumber(1);
                RationalNumber c;
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
                                return new RationalNumber();
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
                return null;
            }
        }

        public int Rank()
        {
            RationalNumber[,] coefMatrix = new RationalNumber[rows, columns];
            Array.Copy(values, coefMatrix, rows * columns);
            int r = 0;
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

        public RationalMatrix Transpose()
        {
            RationalNumber[,] outMatrix = new RationalNumber[columns, rows];

            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    outMatrix[i, j] = new RationalNumber(values[j, i]);
                }
            }

            return new RationalMatrix(outMatrix);
        }

        public RationalNumber Norm()
        {
            RationalNumber r = new RationalNumber();
            RationalNumber maxRational = new RationalNumber();
            for (int i = 0; i < rows; i++)
            {
                r = new RationalNumber();
                for (int j = 0; j < columns; j++)
                {
                    r += RationalNumber.Abs(values[i, j]);
                }
                if (r > maxRational)
                {
                    maxRational = r;
                }
            }
            return maxRational;
        }

        public RationalNumber Step()
        {
            if (rows == columns)
            {
                RationalNumber outRational = new RationalNumber();
                for (int i = 0; i < rows; i++)
                {
                    outRational += values[i, i];
                }
                return outRational;
            }
            else
            {
                return null;
            }
        }

        public RationalMatrix InverseMatrix()
        {
            if (Determinant() != 0)
            {
                RationalNumber[,] coefMatrix = new RationalNumber[rows, columns];
                Array.Copy(values, coefMatrix, rows * columns);
                RationalNumber[,] inverseMatrix = new RationalNumber[rows, columns];
                RationalNumber c;
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        if (j != i)
                        {
                            inverseMatrix[i, j] = new RationalNumber();
                        }
                    }
                    inverseMatrix[i, i] = new RationalNumber(1);
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
                    c = coefMatrix[i, i].ReverseRational();
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

                return new RationalMatrix(inverseMatrix);
            }
            else
            {
                return null;
            }
        }

        public static RationalMatrix operator *(RationalMatrix matrix1, RationalMatrix matrix2)
        {
            if (matrix1.columns == matrix2.rows)
            {
                RationalNumber[,] outMatrix = new RationalNumber[matrix1.rows, matrix2.columns];
                for (int i = 0; i < matrix1.rows; i++)
                {
                    for (int j = 0; j < matrix2.columns; j++)
                    {
                        outMatrix[i, j] = new RationalNumber();
                        for (int k = 0; k < matrix1.columns; k++)
                        {
                            outMatrix[i, j] += matrix1[i, k] * matrix2[k, j];
                        }
                    }
                }
                return new RationalMatrix(outMatrix);
            }
            else
            {
                throw new ArgumentException("the number of columns of matrix 1 does not equal with the number of rows of matrix 2 ");
            }
        }
        public static RationalMatrix operator *(RationalMatrix matrix, RationalNumber value)
        {
            RationalNumber[,] outMatrix = new RationalNumber[matrix.rows, matrix.columns];
            for (int i = 0; i < matrix.rows; i++)
            {
                for (int j = 0; j < matrix.columns; j++)
                {
                    outMatrix[i, j] = matrix[i, j] * value;
                }
            }
            return new RationalMatrix(outMatrix);
        }
        public static RationalMatrix operator *(RationalNumber value, RationalMatrix matrix)
        {
            RationalNumber[,] outMatrix = new RationalNumber[matrix.rows, matrix.columns];
            for (int i = 0; i < matrix.rows; i++)
            {
                for (int j = 0; j < matrix.columns; j++)
                {
                    outMatrix[i, j] = matrix[i, j] * value;
                }
            }
            return new RationalMatrix(outMatrix);
        }
        public static RationalMatrix operator *(RationalMatrix matrix, int value)
        {
            RationalNumber[,] outMatrix = new RationalNumber[matrix.rows, matrix.columns];
            for (int i = 0; i < matrix.rows; i++)
            {
                for (int j = 0; j < matrix.columns; j++)
                {
                    outMatrix[i, j] = matrix[i, j] * value;
                }
            }
            return new RationalMatrix(outMatrix);
        }
        public static RationalMatrix operator *(int value, RationalMatrix matrix)
        {
            RationalNumber[,] outMatrix = new RationalNumber[matrix.rows, matrix.columns];
            for (int i = 0; i < matrix.rows; i++)
            {
                for (int j = 0; j < matrix.columns; j++)
                {
                    outMatrix[i, j] = matrix[i, j] * value;
                }
            }
            return new RationalMatrix(outMatrix);
        }
        public static RationalVector operator *(RationalMatrix matrix, RationalVector vector)
        {
            if (matrix.columns == vector.dim)
            {
                RationalNumber[] outVector = new RationalNumber[matrix.rows];
                for (int i = 0; i < matrix.rows; i++)
                {
                    outVector[i] = new RationalNumber();
                    for (int j = 0; j < matrix.columns; j++)
                    {
                        outVector[i] += matrix[i, j] * vector[j];
                    }
                }
                return new RationalVector(outVector);
            }
            else
            {
                throw new ArgumentException("The number of columns of matrix1 does not equal dimnesions of vector");
            }
        }
        public static RationalVector operator *(RationalVector vector, RationalMatrix matrix)
        {
            if (matrix.columns == vector.dim && matrix.rows == 1)
            {
                RationalNumber[] outVector = new RationalNumber[matrix.rows];
                for (int i = 0; i < matrix.rows; i++)
                {
                    outVector[i] = new RationalNumber();
                    for (int j = 0; j < matrix.columns; j++)
                    {
                        outVector[i] += matrix[i, j] * vector[j];
                    }
                }
                return new RationalVector(outVector);
            }
            else
            {
                throw new ArgumentException("The number of columns of matrix1 does not equal dimnesions of vector");
            }
        }

        public static RationalMatrix operator +(RationalMatrix matrix1, RationalMatrix matrix2)
        {
            if (matrix1.rows == matrix2.rows && matrix1.columns == matrix2.columns)
            {
                RationalNumber[,] outMatrix = new RationalNumber[matrix1.rows, matrix1.columns];
                for (int i = 0; i < matrix1.rows; i++)
                {
                    for (int j = 0; j < matrix2.columns; j++)
                    {
                        outMatrix[i, j] = matrix1[i, j] + matrix2[i, j];
                    }
                }
                return new RationalMatrix(outMatrix);
            }
            else
            {
                throw new ArgumentException("The size of matrix1 is not the same as the size of matrix2 ");
            }
        }

        public static RationalMatrix operator -(RationalMatrix matrix1, RationalMatrix matrix2)
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

        public static bool operator ==(RationalMatrix matrix1, RationalMatrix matrix2)
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

        public static bool operator !=(RationalMatrix matrix1, RationalMatrix matrix2)
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
