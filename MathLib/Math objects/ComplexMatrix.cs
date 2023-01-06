using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathLib.Objects
{
    public class ComplexMatrix
    {
        private ComplexNumber[,] values;
        public int columns { private set; get; }
        public int rows { private set; get; }

        public ComplexNumber this[int i, int j]
        {
            get
            {
                return new ComplexNumber(values[i, j]);
            }
            set
            {
                values[i, j] = new ComplexNumber(value);
            }
        }

        public ComplexMatrix()
        {
            values = null;
            columns = 0;
            rows = 0;
        }
        public ComplexMatrix(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
            values = new ComplexNumber[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    values[i, j] = new ComplexNumber();
                }
            }
        }
        public ComplexMatrix(ComplexNumber[,] values)
        {
            rows = values.GetLength(0);
            columns = values.GetLength(1);
            this.values = new ComplexNumber[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    this.values[i, j] = new ComplexNumber(values[i, j]);
                }
            }
        }
        public ComplexMatrix(ComplexMatrix matrix)
        {
            rows = matrix.rows;
            columns = matrix.columns;
            values = new ComplexNumber[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    values[i, j] = new ComplexNumber(matrix[i, j]);
                }
            }
        }
        public ComplexMatrix(ComplexVector[] vectors)
        {
            rows = vectors.Length;
            columns = vectors[0].dim;
            values = new ComplexNumber[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    values[i, j] = new ComplexNumber(vectors[i][j]);
                }
            }
        }

        public ComplexMatrix AddColumn(ComplexVector vector, int newMatrixId)
        {
            if (vector == null)
            {
                return new ComplexMatrix(values);
            }

            ComplexNumber[,] outMatrix = new ComplexNumber[vector.dim, columns + 1];
            for (int i = 0, k = 0; i < columns + 1; i++)
            {
                if (i == newMatrixId)
                {
                    for (int j = 0; j < vector.dim; j++)
                    {
                        outMatrix[j, i] = new ComplexNumber(vector[j]);
                    }
                }
                else
                {
                    for (int j = 0; j < rows; j++)
                    {
                        outMatrix[j, i] = new ComplexNumber(values[j, k]);
                    }
                    k++;
                }
            }
            return new ComplexMatrix(outMatrix);
        }

        public ComplexMatrix AddColumns(ComplexVector[] vector, int[] newMatrixId)
        {
            if (vector == null || newMatrixId == null)
            {
                return new ComplexMatrix(values);
            }

            ComplexNumber[,] outMatrix = new ComplexNumber[vector[0].dim, columns + vector.Length];
            for (int i = 0, k = 0; i < columns + vector.Length; i++)
            {
                if (newMatrixId.Contains(i))
                {
                    int id = Array.BinarySearch(newMatrixId, i);
                    for (int j = 0; j < vector[0].dim; j++)
                    {
                        outMatrix[j, i] = new ComplexNumber(vector[id][j]);
                    }
                }
                else
                {
                    for (int j = 0; j < rows; j++)
                    {
                        outMatrix[j, i] = new ComplexNumber(values[j, k]);
                    }
                    k++;
                }
            }
            return new ComplexMatrix(outMatrix);
        }
        public ComplexMatrix AddColumns(ComplexNumber[,] values, int[] newMatrixId)
        {
            if (values == null || newMatrixId == null)
            {
                return new ComplexMatrix(values);
            }

            ComplexNumber[,] outMatrix = new ComplexNumber[values.GetLength(0), columns + values.GetLength(1)];
            for (int i = 0, k = 0; i < columns + values.GetLength(1); i++)
            {
                if (newMatrixId.Contains(i))
                {
                    int id = Array.BinarySearch(newMatrixId, i);
                    for (int j = 0; j < values.GetLength(0); j++)
                    {
                        outMatrix[j, i] = new ComplexNumber(values[id, j]);
                    }
                }
                else
                {
                    for (int j = 0; j < rows; j++)
                    {
                        outMatrix[j, i] = new ComplexNumber(this.values[j, k]);
                    }
                    k++;
                }
            }
            return new ComplexMatrix(outMatrix);
        }

        public ComplexMatrix AddRow(ComplexVector vector, int newMatrixId)
        {
            if (vector == null)
            {
                return new ComplexMatrix(values);
            }

            ComplexNumber[,] outMatrix = new ComplexNumber[rows + 1, vector.dim];
            for (int i = 0, k = 0; i < rows + 1; i++)
            {
                if (i == newMatrixId)
                {
                    for (int j = 0; j < vector.dim; j++)
                    {
                        outMatrix[i, j] = new ComplexNumber(vector[j]);
                    }
                }
                else
                {
                    for (int j = 0; j < columns; j++)
                    {
                        outMatrix[i, j] = new ComplexNumber(values[k, j]);
                    }
                    k++;
                }
            }
            return new ComplexMatrix(outMatrix);
        }

        public ComplexMatrix AddRows(ComplexVector[] vector, int[] newMatrixId)
        {
            if (vector == null || newMatrixId == null)
            {
                return new ComplexMatrix(values);
            }

            ComplexNumber[,] outMatrix = new ComplexNumber[rows + vector.Length, vector[0].dim];
            for (int i = 0, k = 0; i < rows + vector.Length; i++)
            {
                if (newMatrixId.Contains(i))
                {
                    int id = Array.BinarySearch(newMatrixId, i);
                    for (int j = 0; j < vector[0].dim; j++)
                    {
                        outMatrix[i, j] = new ComplexNumber(vector[j][id]);
                    }
                }
                else
                {
                    for (int j = 0; j < rows; j++)
                    {
                        outMatrix[i, j] = new ComplexNumber(values[k, j]);
                    }
                    k++;
                }
            }
            return new ComplexMatrix(outMatrix);
        }
        public ComplexMatrix AddRows(ComplexNumber[,] values, int[] newMatrixId)
        {
            if (values == null || newMatrixId == null)
            {
                return new ComplexMatrix(values);
            }

            ComplexNumber[,] outMatrix = new ComplexNumber[rows + values.GetLength(0), values.GetLength(1)];
            for (int i = 0, k = 0; i < rows + values.GetLength(0); i++)
            {
                if (newMatrixId.Contains(i))
                {
                    int id = Array.BinarySearch(newMatrixId, i);
                    for (int j = 0; j < values.GetLength(1); j++)
                    {
                        outMatrix[i, j] = new ComplexNumber(values[j, id]);
                    }
                }
                else
                {
                    for (int j = 0; j < rows; j++)
                    {
                        outMatrix[i, j] = new ComplexNumber(this.values[k, j]);
                    }
                    k++;
                }
            }
            return new ComplexMatrix(outMatrix);
        }

        public ComplexMatrix DeleteColumn(int id)
        {
            ComplexNumber[,] outMatrix = new ComplexNumber[rows, columns - 1];
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
            return new ComplexMatrix(outMatrix);
        }

        public ComplexMatrix DeleteColumns(int[] id)
        {
            ComplexNumber[,] outMatrix = new ComplexNumber[rows, columns - id.Length];
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
            return new ComplexMatrix(outMatrix);
        }

        public ComplexMatrix DeleteRow(int id)
        {
            ComplexNumber[,] outMatrix = new ComplexNumber[rows - 1, columns];
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
            return new ComplexMatrix(outMatrix);
        }

        public ComplexMatrix DeleteRows(int[] id)
        {
            ComplexNumber[,] outMatrix = new ComplexNumber[rows - id.Length, columns];
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
            return new ComplexMatrix(outMatrix);
        }

        public ComplexMatrix SwapColumns(int id1, int id2)
        {
            ComplexNumber[,] outMatrix = new ComplexNumber[rows, columns];
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
            return new ComplexMatrix(outMatrix);
        }

        public ComplexMatrix SwapRows(int id1, int id2)
        {
            ComplexNumber[,] outMatrix = new ComplexNumber[rows, columns];
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
            return new ComplexMatrix(outMatrix);
        }

        public Matrix ReMatrix()
        {
            Matrix outMatrix = new Matrix(rows, columns);
            for(int i = 0; i < rows; i++)
            {
                for(int j = 0; j < columns; j++)
                {
                    outMatrix[i, j] = values[i, j].re;
                }
            }
            return outMatrix;
        }

        public Matrix ImMatrix()
        {
            Matrix outMatrix = new Matrix(rows, columns);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    outMatrix[i, j] = values[i, j].im;
                }
            }
            return outMatrix;
        }

        public ComplexNumber Determinant()
        {
            if (rows == columns)
            {
                ComplexNumber[,] coefMatrix = new ComplexNumber[rows, columns];
                Array.Copy(values, coefMatrix, rows * columns);
                ComplexNumber det = new ComplexNumber(1, 0);
                ComplexNumber c;
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
                                return new ComplexNumber();
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
            ComplexNumber[,] coefMatrix = new ComplexNumber[rows, columns];
            Array.Copy(values, coefMatrix, rows * columns);
            int r = 0;
            ComplexNumber c;

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

        public ComplexMatrix Transpose()
        {
            ComplexNumber[,] outMatrix = new ComplexNumber[columns, rows];

            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    outMatrix[i, j] = new ComplexNumber(values[j, i]);
                }
            }

            return new ComplexMatrix(outMatrix);
        }

        public double Norm()
        {
            double r;
            double maxRational = 0;
            for (int i = 0; i < rows; i++)
            {
                r = 0;
                for (int j = 0; j < columns; j++)
                {
                    r += ComplexNumber.Abs(values[i, j]);
                }
                if (r > maxRational)
                {
                    maxRational = r;
                }
            }
            return maxRational;
        }

        public ComplexNumber Step()
        {
            if (rows == columns)
            {
                ComplexNumber outRational = new ComplexNumber();
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

        public ComplexMatrix InverseMatrix()
        {
            if (Determinant() != 0)
            {
                ComplexNumber[,] coefMatrix = new ComplexNumber[rows, columns];
                Array.Copy(values, coefMatrix, rows * columns);
                ComplexNumber[,] inverseMatrix = new ComplexNumber[rows, columns];
                ComplexNumber c;
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        if (j != i)
                        {
                            inverseMatrix[i, j] = new ComplexNumber();
                        }
                    }
                    inverseMatrix[i, i] = new ComplexNumber(1, 0);
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
                    c = coefMatrix[i, i].ReverseComplexNumber();
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

                return new ComplexMatrix(inverseMatrix);
            }
            else
            {
                return null;
            }
        }

        public static ComplexMatrix operator *(ComplexMatrix matrix1, ComplexMatrix matrix2)
        {
            if (matrix1.columns == matrix2.rows)
            {
                ComplexNumber[,] outMatrix = new ComplexNumber[matrix1.rows, matrix2.columns];
                for (int i = 0; i < matrix1.rows; i++)
                {
                    for (int j = 0; j < matrix2.columns; j++)
                    {
                        outMatrix[i, j] = new ComplexNumber();
                        for (int k = 0; k < matrix1.columns; k++)
                        {
                            outMatrix[i, j] += matrix1[i, k] * matrix2[k, j];
                        }
                    }
                }
                return new ComplexMatrix(outMatrix);
            }
            else
            {
                throw new ArgumentException("the number of columns of matrix 1 does not equal with the number of rows of matrix 2 ");
            }
        }
        public static ComplexMatrix operator *(ComplexMatrix matrix, ComplexNumber value)
        {
            ComplexNumber[,] outMatrix = new ComplexNumber[matrix.rows, matrix.columns];
            for (int i = 0; i < matrix.rows; i++)
            {
                for (int j = 0; j < matrix.columns; j++)
                {
                    outMatrix[i, j] = matrix[i, j] * value;
                }
            }
            return new ComplexMatrix(outMatrix);
        }
        public static ComplexMatrix operator *(ComplexNumber value, ComplexMatrix matrix)
        {
            ComplexNumber[,] outMatrix = new ComplexNumber[matrix.rows, matrix.columns];
            for (int i = 0; i < matrix.rows; i++)
            {
                for (int j = 0; j < matrix.columns; j++)
                {
                    outMatrix[i, j] = matrix[i, j] * value;
                }
            }
            return new ComplexMatrix(outMatrix);
        }
        public static ComplexMatrix operator *(ComplexMatrix matrix, int value)
        {
            ComplexNumber[,] outMatrix = new ComplexNumber[matrix.rows, matrix.columns];
            for (int i = 0; i < matrix.rows; i++)
            {
                for (int j = 0; j < matrix.columns; j++)
                {
                    outMatrix[i, j] = matrix[i, j] * value;
                }
            }
            return new ComplexMatrix(outMatrix);
        }
        public static ComplexMatrix operator *(int value, ComplexMatrix matrix)
        {
            ComplexNumber[,] outMatrix = new ComplexNumber[matrix.rows, matrix.columns];
            for (int i = 0; i < matrix.rows; i++)
            {
                for (int j = 0; j < matrix.columns; j++)
                {
                    outMatrix[i, j] = matrix[i, j] * value;
                }
            }
            return new ComplexMatrix(outMatrix);
        }
        public static ComplexVector operator *(ComplexMatrix matrix, ComplexVector vector)
        {
            if (matrix.columns == vector.dim)
            {
                ComplexNumber[] outVector = new ComplexNumber[matrix.rows];
                for (int i = 0; i < matrix.rows; i++)
                {
                    outVector[i] = new ComplexNumber();
                    for (int j = 0; j < matrix.columns; j++)
                    {
                        outVector[i] += matrix[i, j] * vector[j];
                    }
                }
                return new ComplexVector(outVector);
            }
            else
            {
                throw new ArgumentException("The number of columns of matrix1 does not equal dimnesions of vector");
            }
        }
        public static ComplexVector operator *(ComplexVector vector, ComplexMatrix matrix)
        {
            if (matrix.columns == vector.dim && matrix.rows == 1)
            {
                ComplexNumber[] outVector = new ComplexNumber[matrix.rows];
                for (int i = 0; i < matrix.rows; i++)
                {
                    outVector[i] = new ComplexNumber();
                    for (int j = 0; j < matrix.columns; j++)
                    {
                        outVector[i] += matrix[i, j] * vector[j];
                    }
                }
                return new ComplexVector(outVector);
            }
            else
            {
                throw new ArgumentException("The number of columns of matrix1 does not equal dimnesions of vector");
            }
        }

        public static ComplexMatrix operator +(ComplexMatrix matrix1, ComplexMatrix matrix2)
        {
            if (matrix1.rows == matrix2.rows && matrix1.columns == matrix2.columns)
            {
                ComplexNumber[,] outMatrix = new ComplexNumber[matrix1.rows, matrix1.columns];
                for (int i = 0; i < matrix1.rows; i++)
                {
                    for (int j = 0; j < matrix2.columns; j++)
                    {
                        outMatrix[i, j] = matrix1[i, j] + matrix2[i, j];
                    }
                }
                return new ComplexMatrix(outMatrix);
            }
            else
            {
                throw new ArgumentException("The size of matrix1 is not the same as the size of matrix2 ");
            }
        }

        public static ComplexMatrix operator -(ComplexMatrix matrix1, ComplexMatrix matrix2)
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

        public static bool operator ==(ComplexMatrix matrix1, ComplexMatrix matrix2)
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

        public static bool operator !=(ComplexMatrix matrix1, ComplexMatrix matrix2)
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
