using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathLib.Objects;
using MathLib.SolvingLinearSystem;
using MathLib.Integration;

namespace MathLib.BoundaryValueProblem
{
    public abstract class GalerkinMethod
    {
        private static double ScalarProduct(Function f1, Function f2, double a, double b, double eps)
        {
            return SimpsonsRule.Approximate(a, b, f1 * f2, eps);
        }

        public static Function Solve(Function f, Function p, Function q, 
                                     double a, double b, double alpha, double beta, double A,
                                     double delta, double gamma, double B, int n, double eps)
        {
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
                phiDer[i] = new Function($"{pi * id1}*(x-{a})^({id1 - 1})+{id2}*(x-{a})^{id2 - 1}", "x");
                phi2Der[i] = new Function($"{pi * id1 * (id1 - 1)}*(x-{a})^({id1 - 2})+({id2 * (id2 - 1)})*(x-{a})^({id2 - 2})", "x");
            }

            Matrix matrixA = new Matrix(n, n);
            Vector vectorB = new Vector(n);
            Function L, G;
            G = f - phi2Der[0] - p * phiDer[0] - q * phi[0];
            for (int i = 0; i < n; i++)
            {
                L = phi2Der[i + 1] + p * phiDer[i + 1] + q * phi[i + 1];
                for (int j = 0; j < n; j++)
                {
                    matrixA[i, j] = ScalarProduct(L, phi[j + 1], a, b, eps);
                }
                vectorB[i] = ScalarProduct(G, phi[i + 1], a, b, eps);
            }

            Vector C = LU_Method.Solve(matrixA, vectorB);

            Function result = new Function(phi[0]);
            for (int i = 1; i < n + 1; i++)
            {
                result += C[i - 1] * phi[i];
            }

            return result;
        }

        public static Function Solve(Function f, Function p, Function q, 
                                     VectorFunction phi, VectorFunction phiDer, VectorFunction phi2Der,
                                     double a, double b, double alpha, double beta, double A,
                                     double delta, double gamma, double B, double eps)
        {
            int n = phi.dim;
            Matrix matrixA = new Matrix(n - 1, n - 1);
            Vector vectorB = new Vector(n - 1);
            Function L, G;
            G = f - phi2Der[0] - p * phiDer[0] - q * phi[0];
            for (int i = 0; i < n - 1; i++)
            {
                L = phi2Der[i + 1] + p * phiDer[i + 1] + q * phi[i + 1];
                for (int j = 0; j < n - 1; j++)
                {
                    matrixA[i, j] = ScalarProduct(L, phi[j + 1], a, b, eps);
                }
                vectorB[i] = ScalarProduct(G, phi[i + 1], a, b, eps);
            }

            Vector C = LU_Method.Solve(matrixA, vectorB);

            Function result = new Function(phi[0]);
            for (int i = 1; i < n; i++)
            {
                result += C[i - 1] * phi[i];
            }

            return result;
        }
    }
}
