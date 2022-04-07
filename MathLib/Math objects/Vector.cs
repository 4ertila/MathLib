using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace MathLib.Objects
{
    public class Vector
    {
        private double[] values;
        private int dim = 0;

        public int Dim
        {
            get
            {
                return dim;
            }
        }

        public double this[int i]
        {
            get
            {
                return values[i];
            }
            set
            {
                values[i] = value;
            }
        }

        public Vector()
        {
            values = null;
            dim = 0;
        }

        public Vector(int dim)
        {
            this.dim = dim;
            values = new double[dim];
            for (int i = 0; i < dim; i++)
            {
                values[i] = 0;
            }
        }

        public Vector(double[] values)
        {
            dim = values.Length;
            this.values = new double[dim];
            for (int i = 0; i < dim; i++)
            {
                this.values[i] = values[i];
            }
        }

        public Vector(List<double> values)
        {
            dim = values.Count;
            this.values = new double[dim];
            for (int i = 0; i < dim; i++)
            {
                this.values[i] = values[i];
            }
        }

        public Vector(Vector vector)
        {
            dim = vector.Dim;
            values = new double[dim];
            for (int i = 0; i < dim; i++)
            {
                values[i] = vector[i];
            }
        }

        public double[] ToArray()
        {
            double[] result = new double[dim];
            values.CopyTo(result, 0);
            return result;
        }

        public Vector Reverse()
        {
            return new Vector(values.Reverse().ToArray());
        }

        public void Init(Vector vector)
        {
            dim = vector.Dim;
            values = new double[dim];
            for(int i =0; i < dim; i++)
            {
                values[i] = vector[i];
            }
        }

        public Vector AddElement(double value, int newVectorId)
        {
            double[] outVector = new double[dim + 1];
            for (int i = 0, k = 0; i < dim + 1; i++)
            {
                if (i != newVectorId - 1)
                {
                    outVector[i] = values[k];
                    k++;
                }
                else
                {
                    outVector[i] = value;
                }
            }
            /*Array.Copy(vector, outVector, length);
            outVector[length] = rational;*/
            return new Vector(outVector);
        }

        public Vector DeleteElement()
        {
            double[] outDouble = new double[dim - 1];
            for (int i = 0; i < dim - 1; i++)
            {
                outDouble[i] = values[i];
            }
            return new Vector(outDouble);
        }

        public Vector DeleteElement(int id)
        {
            double[] outDouble = new double[dim - 1];
            for (int i = 0; i < dim; i++)
            {
                if (i != id - 1)
                {
                    outDouble[i] = values[i];
                }
            }
            return new Vector(outDouble);
        }

        public double Norm()
        {
            double norm = 0;
            for (int i = 0; i < dim; i++)
            {
                norm += Pow(values[i], 2);
            }
            return Sqrt(norm);
        }

        public double Min()
        {
            double min = double.MaxValue;
            for (int i = 0; i < dim; i++)
            {
                if (values[i] < min)
                {
                    min = values[i];
                }
            }
            return min;
        }

        public double Max()
        {
            double max = double.MinValue;
            for (int i = 0; i < dim; i++)
            {
                if (values[i] > max)
                {
                    max = values[i];
                }
            }
            return max;
        }

        public double AbsMax()
        {
            double max = 0;
            for (int i = 0; i < dim; i++)
            {
                if (Abs(values[i]) > Abs(max))
                {
                    max = values[i];
                }
            }
            return max;
        }

        public double Sum()
        {
            double result = 0;
            for(int i =0; i< dim; i++)
            {
                result += values[i];
            }
            return result;
        }

        public static Vector IdentityVector(int dim)
        {
            double[] outVector = new double[dim];
            for(int i = 0; i < dim; i++)
            {
                outVector[i] = 1;
            }
            return new Vector(outVector);
        }

        public static double ScalarProduct(Vector vector1, Vector vector2)
        {
            double outDouble = 0;
            for (int i = 0; i < vector1.dim; i++)
            {
                outDouble += vector1[i] * vector2[i];
            }
            return outDouble;
        }

        public static Vector VectorProduct(Vector vector1, Vector vector2)
        {
            if (vector1.dim == 3 && vector2.dim == 3)
            {
                double[] outDouble = new double[3];
                outDouble[0] = vector1[1] * vector2[2] - vector1[2] * vector2[1];
                outDouble[1] = vector1[0] * vector2[2] - vector1[2] * vector2[0];
                outDouble[2] = vector1[0] * vector2[1] - vector1[1] * vector2[0];
                return new Vector(outDouble);
            }
            else if (vector1.dim == 2 && vector2.dim == 2)
            {
                double[] outDouble = new double[3];
                outDouble[0] = 0;
                outDouble[1] = 0;
                outDouble[2] = vector1[0] * vector2[1] - vector1[1] * vector2[0];
                return new Vector(outDouble);
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

        /*private static Vector[] ChooseVector(int[] vectorsId, Vector[] vectors, int rank, int startId)
        {
            if (rank - 1 != 0)
            {
                Vector[] outVectors;
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
                Vector[] outVectors = new Vector[vectorsId.Length];
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
        public static Vector[] Basis(Vector[] vectors)
        {
            RationalMatrix matrix = new RationalMatrix(vectors);
            int r = matrix.Rank();
            return ChooseVector(new int[r], vectors, r, 0);
        }*/

        public override string ToString()
        {
            string outStr = "";
            for (int i = 0; i < values.Length; i++)
            {
                outStr += values[i] + " ";
            }
            return outStr;
        }

        public bool Equals(object obj, double eps)
        {
            if (!GetType().Equals(obj.GetType()) || obj == null)
            {
                return false;
            }
            else
            {
                Vector vector = (Vector)obj;
                for(int i = 0; i < dim; i++)
                {
                    if(Abs(values[i] - vector[i]) > eps)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public override bool Equals(object obj)
        {
            if(!GetType().Equals(obj.GetType()) || obj == null)
            {
                return false;
            }
            else
            {
                Vector v = (Vector)obj;
                return this == v;
            }
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public static Vector operator +(Vector vector1, Vector vector2)
        {
            if (vector1.dim == vector2.dim)
            {
                double[] outDouble = new double[vector1.dim];
                for (int i = 0; i < vector1.dim; i++)
                {
                    outDouble[i] = vector1[i] + vector2[i];
                }
                return new Vector(outDouble);
            }
            else
            {
                throw new Exception("lenght a is not equal length b");
            }
        }

        public static Vector operator -(Vector vector1, Vector vector2)
        {
            if (vector1.dim == vector2.dim)
            {
                double[] outDouble = new double[vector2.dim];
                for (int i = 0; i < vector1.dim; i++)
                {
                    outDouble[i] = vector1[i] - vector2[i];
                }
                return new Vector(outDouble);
            }
            else
            {
                throw new Exception("lenght a is not equal length b");
            }
        }

        public static double operator *(Vector vector1, Vector vector2)
        {
            double outDouble = 0;
            for (int i = 0; i < vector1.dim; i++)
            {
                outDouble += vector1[i] * vector2[i];
            }
            return outDouble;
        }

        public static Vector operator *(Vector vector1, double r)
        {
            double[] outDouble = new double[vector1.dim];
            for (int i = 0; i < vector1.dim; i++)
            {
                outDouble[i] = vector1[i] * r;
            }
            return new Vector(outDouble);
        }

        public static Vector operator *(double r, Vector vector1)
        {
            double[] outDouble = new double[vector1.dim];
            for (int i = 0; i < vector1.dim; i++)
            {
                outDouble[i] = vector1[i] * r;
            }
            return new Vector(outDouble);
        }

        public static bool operator ==(Vector vector1, Vector vector2)
        {
            if (vector1 is null || vector2 is null)
            {
                throw new ArgumentException("One of vectors is null");
            }
            else
            {
                if(vector1.dim != vector2.dim)
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

        public static bool operator !=(Vector vector1, Vector vector2)
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
