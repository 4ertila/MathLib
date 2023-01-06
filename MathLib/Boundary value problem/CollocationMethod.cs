using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathLib.Objects;
using MathLib.SolvingLinearSystem;

namespace MathLib.BoundaryValueProblem
{
    public abstract class CollocationMethod
    {
        public static Function Solve(Function f, Function p, Function q, double a, double b,
                            double alpha, double beta, double A, 
                            double delta, double gamma, double B, double[] x)
        {
            int n = x.Length;
            VectorFunction phi = new VectorFunction(n + 1);
            VectorFunction phiDer = new VectorFunction(n + 1);
            VectorFunction phi2Der = new VectorFunction(n + 1);
            double c, d;
            if (beta != 0)
            {
                c = (B * beta - A * gamma) / (beta * delta + gamma * (beta * b - alpha - beta * a));
                d = (A - (alpha + beta * a) * c) / beta;
            }
            else
            {
                c = (A * gamma - B * beta) / (gamma * alpha + beta * (a * gamma - delta - gamma * b));
                d = (B - (delta + gamma * b) * c) / gamma;
            }
            phi[0] = new Function($"{c}*x+{d}", "x");
            phiDer[0] = new Function($"{c}", "x");
            phi2Der[0] = new Function($"0", "x");

            double pi;
            int id1, id2;
            if (alpha == 0)
            {
                id1 = 1;
                id2 = 2;

                pi = -(b - a) * (delta * id2 + gamma * (b - a)) / (delta * id1 + gamma * (b - a));

                phi[1] = new Function($"{pi}*(x-{a})^{id1}+(x-{a})^{id2}", "x");
                phiDer[1] = new Function($"{pi}+{id2}*(x-{a})^{id2 - 1}", "x");
                phi2Der[1] = new Function($"{id2 * (id2 - 1)}", "x");
            }
            else
            {
                id1 = 2;
                id2 = 3;

                pi = -(b - a) * (delta * id2 + gamma * (b - a)) / (delta * id1 + gamma * (b - a));

                phi[1] = new Function($"{pi}*(x-{a})^{id1}+(x-{a})^{id2}", "x");
                phiDer[1] = new Function($"{pi * id1}*(x-{a})^({id1 - 1})+{id2}*(x-{a})^{id2 - 1}", "x");
                phi2Der[1] = new Function($"{pi * id1 * (id1 - 1)}*(x-{a})^({id1 - 2})+({id2 * (id2 - 1)})*(x-{a})^({id2 - 2})", "x");
            }

            for (int i = 2; i < n + 1; i++)
            {
                id1++;
                id2++;

                pi = -(b - a) * (delta * id2 + gamma * (b - a)) / (delta * id1 + gamma * (b - a));

                phi[i] = new Function($"{pi}*(x-{a})^{id1}+(x-{a})^{id2}", "x");
                phiDer[i] = new Function($"{pi * id1}*(x-{a})^{id1 - 1}+{id2}*(x-{a})^{id2 - 1}", "x");
                phi2Der[i] = new Function($"{pi * id1 * (id1 - 1)}*(x-{a})^{id1 - 2}+{id2 * (id2 - 1)}*(x-{a})^{id2 - 2}", "x");
            }

            Matrix matrixA = new Matrix(n, n);
            Vector vectorB = new Vector(n);
            double pValue;
            double qValue;
            for (int i = 0; i < n; i++)
            {
                phi["x"] = phiDer["x"] = phi2Der["x"] = f["x"] = p["x"] = q["x"] = x[i];
                pValue = p.Calculate();
                qValue = q.Calculate();
                for (int j = 0; j < n; j++)
                {
                    matrixA[i, j] = phi2Der[j + 1].Calculate() + 
                                    pValue * phiDer[j + 1].Calculate() + 
                                    qValue * phi[j + 1].Calculate();
                }
                vectorB[i] = f.Calculate() -
                       phi2Der[0].Calculate() -
                       pValue * phiDer[0].Calculate() -
                       qValue * phi[0].Calculate();
            }

            Vector C = LU_Method.Solve(matrixA, vectorB);

            Function result = new Function(phi[0]);
            for(int i = 1; i < n + 1; i++)
            {
                result += C[i - 1] * phi[i];
            }

            return result;
        }

        public static Function Solve(Function f, Function p, Function q,
                                     VectorFunction phi, VectorFunction phiDer, VectorFunction phi2Der,
                                     double a, double b, double alpha, double beta, double A,
                                     double delta, double gamma, double B, double[] x)
        {
            int n = phi.dim;
            Matrix matrixA = new Matrix(n, n);
            Vector vectorB = new Vector(n);
            double pValue;
            double qValue;
            for (int i = 0; i < n; i++)
            {
                phi["x"] = phiDer["x"] = phi2Der["x"] = f["x"] = p["x"] = q["x"] = x[i];
                pValue = p.Calculate();
                qValue = q.Calculate();
                for (int j = 0; j < n; j++)
                {
                    matrixA[i, j] = phi2Der[j + 1].Calculate() +
                                    pValue * phiDer[j + 1].Calculate() +
                                    qValue * phi[j + 1].Calculate();
                }
                vectorB[i] = f.Calculate() -
                       phi2Der[0].Calculate() -
                       pValue * phiDer[0].Calculate() -
                       qValue * phi[0].Calculate();
            }

            Vector C = LU_Method.Solve(matrixA, vectorB);

            Function result = new Function(phi[0]);
            for (int i = 1; i < n + 1; i++)
            {
                result += C[i - 1] * phi[i];
            }

            return result;
        }
    }
}
