using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathLib.Objects;

namespace MathLib.SolvingNonlinearSystem
{
    public abstract class TangentMethod
    {
        public delegate double Function(Vector x);

        public static Vector Solve(double eps, Function[] f, Function[,] dfdx)
        {
            Matrix I = new Matrix(dfdx.GetLength(0), dfdx.GetLength(1));
            Vector fVector = new Vector(f.Length);
            Vector x = Vector.IdentityVector(I.Rows);
            Vector xPrev = new Vector();

            do
            {
                xPrev.Init(x);
                for(int i = 0; i < I.Rows; i++)
                {
                    for(int j = 0; j < I.Columns; j++)
                    {
                        I[i, j] = dfdx[i, j](xPrev);
                    }
                    fVector[i] = f[i](xPrev);
                }
                x = xPrev - I.InverseMatrix() * fVector;
            }
            while ((x - xPrev).Norm() >= eps);

            return x;
        }
    }
}
