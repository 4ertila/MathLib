using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathLib.Objects
{
    public class ComplexVector
    {
        private ComplexNumber[] values;

        public int dim { private set; get; }

        public ComplexNumber this[int i]
        {
            get
            {
                return new ComplexNumber(values[i]);
            }
            set
            {
                values[i] = new ComplexNumber(value);
            }
        }

        public ComplexVector()
        {
            values = null;
            dim = 0;
        }
        public ComplexVector(int dim)
        {
            this.dim = dim;
            values = new ComplexNumber[dim];
            for (int i = 0; i < dim; i++)
            {
                values[i] = new ComplexNumber(0, 1);
            }
        }
        public ComplexVector(IEnumerable<ComplexNumber> vector)
        {
            dim = vector.Count();
            values = new ComplexNumber[dim];
            int i = 0;
            foreach(ComplexNumber complex in vector)
            {
                values[i] = complex;
                i++;
            }
        }
        public ComplexVector(ComplexVector vector)
        {
            dim = vector.dim;
            values = new ComplexNumber[dim];
            for (int i = 0; i < dim; i++)
            {
                values[i] = vector[i];
            }
        }
        public ComplexVector(IEnumerable<double> reVector, IEnumerable<double> imVector)
        {
            dim = reVector.Count();
            values = new ComplexNumber[dim];
            int i = 0;
            foreach(var complex in reVector.Zip(imVector, Tuple.Create))
            {
                values[i] = new ComplexNumber(complex.Item1, complex.Item2);
                i++;
            }
        }
        public ComplexVector(Vector reVector, Vector imVector)
        {
            dim = reVector.dim;
            values = new ComplexNumber[dim];
            for (int i = 0; i < dim; i++)
            {
                values[i] = new ComplexNumber(reVector[i], imVector[i]);
            }
        }

        public ComplexVector AddComplex(ComplexNumber complex, int newVectorId)
        {
            ComplexNumber[] outVector = new ComplexNumber[dim + 1];
            for (int i = 0, k = 0; i < dim + 1; i++)
            {
                if (i != newVectorId - 1)
                {
                    outVector[i] = values[k];
                    k++;
                }
                else
                {
                    outVector[i] = complex;
                }
            }
            /*Array.Copy(vector, outVector, length);
            outVector[length] = Complex;*/
            return new ComplexVector(outVector);
        }

        public ComplexVector DeleteComplex(int id)
        {
            ComplexNumber[] outComplex = new ComplexNumber[dim - 1];
            for (int i = 0; i < dim; i++)
            {
                if (i != id)
                {
                    outComplex[i] = values[i];
                }
            }
            return new ComplexVector(outComplex);
        }

        public Vector ReVector()
        {
            Vector outVector = new Vector(dim);
            for(int i = 0; i < dim; i++)
            {
                outVector[i] = values[i].re;
            }
            return outVector;
        }

        public Vector ImVector()
        {
            Vector outVector = new Vector(dim);
            for (int i = 0; i < dim; i++)
            {
                outVector[i] = values[i].im;
            }
            return outVector;
        }

        public ComplexNumber Norm()
        {
            ComplexNumber tNumber = new ComplexNumber();
            for (int i = 0; i < dim; i++)
            {
                tNumber += ComplexNumber.Abs(values[i]);
            }
            return tNumber;
        }

        public static ComplexNumber ScalarProduct(ComplexVector vector1, ComplexVector vector2)
        {
            ComplexNumber outComplex = new ComplexNumber(0, 1);
            for (int i = 0; i < vector1.dim; i++)
            {
                outComplex += vector1[i] * vector2[i];
            }
            return outComplex;
        }

        public static ComplexVector VectorProduct(ComplexVector vector1, ComplexVector vector2)
        {
            if (vector1.dim == 3 && vector2.dim == 3)
            {
                ComplexNumber[] outComplex = new ComplexNumber[3];
                outComplex[0] = vector1[1] * vector2[2] - vector1[2] * vector2[1];
                outComplex[1] = vector1[0] * vector2[2] - vector1[2] * vector2[0];
                outComplex[2] = vector1[0] * vector2[1] - vector1[1] * vector2[0];
                return new ComplexVector(outComplex);
            }
            else if (vector1.dim == 2 && vector2.dim == 2)
            {
                ComplexNumber[] outComplex = new ComplexNumber[3];
                outComplex[0] = new ComplexNumber();
                outComplex[1] = new ComplexNumber();
                outComplex[2] = vector1[0] * vector2[1] - vector1[1] * vector2[0];
                return new ComplexVector(outComplex);
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

        private static ComplexVector[] ChooseVector(int[] vectorsId, ComplexVector[] vectors, int rank, int startId)
        {
            if (rank - 1 != 0)
            {
                ComplexVector[] outVectors;
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
                ComplexVector[] outVectors = new ComplexVector[vectorsId.Length];
                ComplexMatrix checkMatrix;
                for (int i = startId; i < vectors.Length - rank + 1; i++)
                {
                    vectorsId[vectorsId.Length - rank] = i;
                    for (int j = 0; j < vectorsId.Length; j++)
                    {
                        outVectors[j] = vectors[vectorsId[j]];
                    }

                    checkMatrix = new ComplexMatrix(outVectors);
                    if (checkMatrix.Rank() == vectorsId.Length)
                    {
                        return outVectors;
                    }
                }
            }
            return null;
        }
        public static ComplexVector[] Basis(ComplexVector[] vectors)
        {
            ComplexMatrix matrix = new ComplexMatrix(vectors);
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

        public static ComplexVector operator +(ComplexVector vector1, ComplexVector vector2)
        {
            if (vector1.dim == vector2.dim)
            {
                ComplexNumber[] outComplex = new ComplexNumber[vector1.dim];
                for (int i = 0; i < vector1.dim; i++)
                {
                    outComplex[i] = vector1[i] + vector2[i];
                }
                return new ComplexVector(outComplex);
            }
            else
            {
                throw new Exception("lenght a is not equal length b");
            }
        }

        public static ComplexVector operator -(ComplexVector vector1, ComplexVector vector2)
        {
            if (vector1.dim == vector2.dim)
            {
                ComplexNumber[] outComplex = new ComplexNumber[vector2.dim];
                for (int i = 0; i < vector1.dim; i++)
                {
                    outComplex[i] = vector1[i] - vector2[i];
                }
                return new ComplexVector(outComplex);
            }
            else
            {
                throw new Exception("lenght a is not equal length b");
            }
        }

        public static ComplexNumber operator *(ComplexVector vector1, ComplexVector vector2)
        {
            ComplexNumber outComplex = new ComplexNumber(0, 1);
            for (int i = 0; i < vector1.dim; i++)
            {
                outComplex += vector1[i] * vector2[i];
            }
            return outComplex;
        }
        public static ComplexVector operator *(ComplexVector vector, ComplexNumber complex)
        {
            ComplexNumber[] outComplex = new ComplexNumber[vector.dim];
            for (int i = 0; i < vector.dim; i++)
            {
                outComplex[i] = vector[i] * complex;
            }
            return new ComplexVector(outComplex);
        }
        public static ComplexVector operator *(ComplexNumber complex, ComplexVector vector)
        {
            ComplexNumber[] outComplex = new ComplexNumber[vector.dim];
            for (int i = 0; i < vector.dim; i++)
            {
                outComplex[i] = vector[i] * complex;
            }
            return new ComplexVector(outComplex);
        }
        public static ComplexVector operator *(int value, ComplexVector vector)
        {
            ComplexNumber[] outComplex = new ComplexNumber[vector.dim];
            for (int i = 0; i < vector.dim; i++)
            {
                outComplex[i] = vector[i] * value;
            }
            return new ComplexVector(outComplex);
        }
        public static ComplexVector operator *(ComplexVector vector, int value)
        {
            ComplexNumber[] outComplex = new ComplexNumber[vector.dim];
            for (int i = 0; i < vector.dim; i++)
            {
                outComplex[i] = vector[i] * value;
            }
            return new ComplexVector(outComplex);
        }

        public static bool operator ==(ComplexVector vector1, ComplexVector vector2)
        {
            if (vector1 is null || vector2 is null)
            {
                throw new ArgumentException("One of vectors is null");
            }
            else
            {
                if (vector1.dim != vector2.dim)
                {
                    return false;
                }
                for (int i = 0; i < vector1.dim; i++)
                {
                    if (vector1[i] != vector2[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public static bool operator !=(ComplexVector vector1, ComplexVector vector2)
        {
            if (vector1 is null || vector2 is null)
            {
                throw new ArgumentException("One of vectors is null");
            }
            {
                if (vector1.dim != vector2.dim)
                {
                    return true;
                }
                for (int i = 0; i < vector1.dim; i++)
                {
                    if (vector1[i] == vector2[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
