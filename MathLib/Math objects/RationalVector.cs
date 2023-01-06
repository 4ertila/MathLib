using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathLib.Objects
{
    public class RationalVector
    {
        private RationalNumber[] values;

        public int dim { private set; get; } = 0;

        public RationalNumber this[int i]
        {
            set
            {
                values[i] = new RationalNumber(value);
            }
            get
            {
                return new RationalNumber(values[i]);
            }
        }

        public RationalVector()
        {
            values = null;
            dim = 0;
        }
        public RationalVector(int dim)
        {
            this.dim = dim;
            values = new RationalNumber[dim];
            for (int i = 0; i < dim; i++)
            {
                values[i] = new RationalNumber(0, 1);
            }
        }
        public RationalVector(IEnumerable<RationalNumber> vector)
        {
            dim = vector.Count();
            values = new RationalNumber[dim];
            int i = 0;
            foreach(RationalNumber rational in vector)
            {
                values[i] = rational;
                i++;
            }
        }
        public RationalVector(RationalVector vector)
        {
            dim = vector.dim;
            values = new RationalNumber[dim];
            for (int i = 0; i < dim; i++)
            {
                values[i] = vector[i];
            }
        }

        public RationalVector AddRational(RationalNumber rational, int newVectorId)
        {
            RationalNumber[] outVector = new RationalNumber[dim + 1];
            for (int i = 0, k = 0; i < dim + 1; i++)
            {
                if (i != newVectorId - 1)
                {
                    outVector[i] = values[k];
                    k++;
                }
                else
                {
                    outVector[i] = rational;
                }
            }
            /*Array.Copy(vector, outVector, length);
            outVector[length] = rational;*/
            return new RationalVector(outVector);
        }

        public RationalVector RemoveRational(int id)
        {
            RationalNumber[] outRational = new RationalNumber[dim - 1];
            for (int i = 0; i < dim; i++)
            {
                if (i != id - 1)
                {
                    outRational[i] = values[i];
                }
            }
            return new RationalVector(outRational);
        }

        public RationalNumber Norm()
        {
            RationalNumber tNumber = new RationalNumber();
            for (int i = 0; i < dim; i++)
            {
                tNumber += RationalNumber.Abs(values[i]);
            }
            return tNumber;
        }

        public RationalNumber Min()
        {
            RationalNumber min = new RationalNumber(int.MaxValue);
            for (int i = 0; i < dim; i++)
            {
                if (values[i] < min)
                {
                    min = values[i];
                }
            }
            return min;
        }

        public RationalNumber Max()
        {
            RationalNumber max = new RationalNumber(int.MinValue);
            for (int i = 0; i < dim; i++)
            {
                if (values[i] > max)
                {
                    max = values[i];
                }
            }
            return max;
        }

        public static RationalNumber ScalarProduct(RationalVector vector1, RationalVector vector2)
        {
            RationalNumber outRational = new RationalNumber(0, 1);
            for (int i = 0; i < vector1.dim; i++)
            {
                outRational += vector1[i] * vector2[i];
            }
            return outRational;
        }

        public static RationalVector VectorProduct(RationalVector vector1, RationalVector vector2)
        {
            if (vector1.dim == 3 && vector2.dim == 3)
            {
                RationalNumber[] outRational = new RationalNumber[3];
                outRational[0] = vector1[1] * vector2[2] - vector1[2] * vector2[1];
                outRational[1] = vector1[0] * vector2[2] - vector1[2] * vector2[0];
                outRational[2] = vector1[0] * vector2[1] - vector1[1] * vector2[0];
                return new RationalVector(outRational);
            }
            else if (vector1.dim == 2 && vector2.dim == 2)
            {
                RationalNumber[] outRational = new RationalNumber[3];
                outRational[0] = new RationalNumber();
                outRational[1] = new RationalNumber();
                outRational[2] = vector1[0] * vector2[1] - vector1[1] * vector2[0];
                return new RationalVector(outRational);
            }
            else if (vector1.dim != vector2.dim)
            {
                throw new Exception("length r1 is not equal length r2");
            }
            else
            {
                throw new Exception("Incorrect dimension");
            }
        }

        private static RationalVector[] ChooseVector(int[] vectorsId, RationalVector[] vectors, int rank, int startId)
        {
            if (rank - 1 != 0)
            {
                RationalVector[] outVectors;
                for (int i = startId; i < vectors.Length - rank + 1; i++)
                {
                    vectorsId[vectorsId.Length - rank] = i;
                    outVectors = ChooseVector(vectorsId, vectors, rank - 1, i + 1);
                    if (outVectors != null)
                    {
                        return outVectors;
                    }
                }
            }
            else
            {
                RationalVector[] outVectors = new RationalVector[vectorsId.Length];
                RationalMatrix checkMatrix;
                for (int i = startId; i < vectors.Length - rank + 1; i++)
                {
                    vectorsId[vectorsId.Length - rank] = i;
                    for (int j = 0; j < vectorsId.Length; j++)
                    {
                        outVectors[j] = vectors[vectorsId[j]];
                    }

                    checkMatrix = new RationalMatrix(outVectors);
                    if (checkMatrix.Rank() == vectorsId.Length)
                    {
                        return outVectors;
                    }
                }
            }
            return null;
        }

        public static RationalVector[] Basis(RationalVector[] vectors)
        {
            RationalMatrix matrix = new RationalMatrix(vectors);
            int r = matrix.Rank();
            return ChooseVector(new int[r], vectors, r, 0);
        }

        public override string ToString()
        {
            string outStr = "";
            for (int i = 0; i < values.Length; i++)
            {
                outStr += values[i] + " ";
            }
            return outStr;
        }

        public static RationalVector operator +(RationalVector vector1, RationalVector vector2)
        {
            if (vector1.dim == vector2.dim)
            {
                RationalNumber[] outRational = new RationalNumber[vector1.dim];
                for (int i = 0; i < vector1.dim; i++)
                {
                    outRational[i] = vector1[i] + vector2[i];
                }
                return new RationalVector(outRational);
            }
            else
            {
                throw new Exception("lenght a is not equal length b");
            }
        }

        public static RationalVector operator -(RationalVector vector1, RationalVector vector2)
        {
            if (vector1.dim == vector2.dim)
            {
                RationalNumber[] outRational = new RationalNumber[vector2.dim];
                for (int i = 0; i < vector1.dim; i++)
                {
                    outRational[i] = vector1[i] - vector2[i];
                }
                return new RationalVector(outRational);
            }
            else
            {
                throw new Exception("lenght a is not equal length b");
            }
        }

        public static RationalNumber operator *(RationalVector vector1, RationalVector vector2)
        {
            RationalNumber outRational = new RationalNumber(0, 1);
            for (int i = 0; i < vector1.dim; i++)
            {
                outRational += vector1[i] * vector2[i];
            }
            return outRational;
        }
        public static RationalVector operator *(RationalVector vector1, RationalNumber r)
        {
            RationalNumber[] outRational = new RationalNumber[vector1.dim];
            for (int i = 0; i < vector1.dim; i++)
            {
                outRational[i] = vector1[i] * r;
            }
            return new RationalVector(outRational);
        }
        public static RationalVector operator *(RationalNumber r, RationalVector vector1)
        {
            RationalNumber[] outRational = new RationalNumber[vector1.dim];
            for (int i = 0; i < vector1.dim; i++)
            {
                outRational[i] = vector1[i] * r;
            }
            return new RationalVector(outRational);
        }
        public static RationalVector operator *(int r, RationalVector vector1)
        {
            RationalNumber[] outRational = new RationalNumber[vector1.dim];
            for (int i = 0; i < vector1.dim; i++)
            {
                outRational[i] = vector1[i] * r;
            }
            return new RationalVector(outRational);
        }
        public static RationalVector operator *(RationalVector vector1, int r)
        {
            RationalNumber[] outRational = new RationalNumber[vector1.dim];
            for (int i = 0; i < vector1.dim; i++)
            {
                outRational[i] = vector1[i] * r;
            }
            return new RationalVector(outRational);
        }
    }
}
