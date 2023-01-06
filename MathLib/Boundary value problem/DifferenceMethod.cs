using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathLib.Objects;
using MathLib.SolvingLinearSystem;

namespace MathLib.BoundaryValueProblem
{
    public abstract class DifferenceMethod
    {
        public static double[] Solve(Function f, Function p, Function q, double a, double b,
                            double alpha, double beta, double A,
                            double delta, double gamma, double B, int n)
        {
            double h = (b - a) / n;
            Matrix matrixA = new Matrix(n + 1, n + 1);
            Vector vectorB = new Vector(n + 1);

            matrixA[0, 0] = beta * h - alpha;
            matrixA[0, 1] = alpha;
            matrixA[n, n - 1] = -delta;
            matrixA[n, n] = gamma * h + delta;

            vectorB[0] = A * h;
            vectorB[n] = B * h;

            double pValue;
            for(int i = 1; i < n; i++)
            {
                f["x"] = p["x"] = q["x"] = a + i * h;
                pValue = p.Calculate();

                matrixA[i, i - 1] = 2 - pValue * h;
                matrixA[i, i] = -4 + 2 * h * h * q.Calculate();
                matrixA[i, i + 1] = 2 + h * pValue;

                vectorB[i] = 2 * h * h * f.Calculate();
            }

            return SweepMethod.Solve(matrixA, vectorB).Values;
        }

        public abstract class EllipticalType
        {
            public static double[,] Solve(Function f, Function phi, Vector[,] Gh, double eps = double.NaN)
            {
                double h1 = Gh[0, 1][0] - Gh[0, 0][0];
                double h2 = Gh[1, 0][1] - Gh[0, 0][1];
                int n = Gh.GetLength(0);
                int m = Gh.GetLength(1);
                double[,] result = new double[n, m];

                for(int i = 0; i < m; i++)
                {
                    phi["x"] = Gh[0, i][0];
                    phi["y"] = Gh[0, i][1];
                    result[0, i] = phi.Calculate();

                    phi["x"] = Gh[n - 1, i][0];
                    phi["y"] = Gh[n - 1, i][1];
                    result[n - 1, i] = phi.Calculate();
                }

                for(int i = 1; i < n - 1; i++)
                {
                    phi["x"] = Gh[i, 0][0];
                    phi["y"] = Gh[i, 0][1];
                    result[i, 0] = phi.Calculate();

                    phi["x"] = Gh[i, m - 1][0];
                    phi["y"] = Gh[i, m - 1][1];
                    result[i, m - 1] = phi.Calculate();
                }

                Matrix A = new Matrix((m - 2) * (n - 2));
                Vector b = new Vector((m - 2) * (n - 2));

                double H1 = 1 / h1 / h1;
                double H2 = 1 / h2 / h2;
                int downRow = (n - 3) * (m - 2);
                for (int i = 2; i < m - 2; i++)
                {
                    b[i - 1] -= result[0, i] / H1;

                    A[i - 1, i - 1] += -2 * (H1 + H2);
                    A[i - 1, m - 3 + i] += H1;
                    A[i - 1, i - 2] += H2;
                    A[i - 1, i] += H2;

                    b[(m - 2) * (n - 3) + i - 1] -= result[n - 1, i] * H1;

                    A[downRow + i - 1, downRow + i - 1] += -2 * (H1 + H2);
                    A[downRow + i - 1, (n - 4) * (m - 2) + i - 1] += H1;
                    A[downRow + i - 1, downRow + i] += H2;
                    A[downRow + i - 1, downRow + i - 2] += H2;
                }

                b[0] -= result[1, 0] * H2 + result[0, 1] * H1;
                b[m - 3] -= result[1, m - 1] * H2 + result[0, m - 1] * H1;
                b[(n - 3) * (m - 2)] -= result[n - 2, 0] * H2 + result[n - 1, 1] * H1;
                b[(n - 2) * (m - 2) - 1] -= result[n - 2, m - 1] * H2 + result[n - 1, m - 2] * H1;

                A[0, 0] += -2 * (H1 + H2);
                A[0, m - 2] += H2;
                A[0, 1] += H1;

                A[m - 3, m - 3] += -2 * (H1 + H2);
                A[m - 3, m - 4] += H2;
                A[m - 3, (m - 2) * 2 - 1] += H1;

                A[(m - 2) * (n - 3), (m - 2) * (n - 3)] += -2 * (H1 + H2);
                A[(m - 2) * (n - 3), (m - 2) * (n - 3) + 1] += H2;
                A[(m - 2) * (n - 3), (m - 2) * (n - 4)] += H1;

                A[(m - 2) * (n - 2) - 1, (m - 2) * (n - 2) - 1] += -2 * (H1 + H2);
                A[(m - 2) * (n - 2) - 1, (m - 2) * (n - 3) - 1] += H2;
                A[(m - 2) * (n - 2) - 1, (m - 2) * (n - 2) - 2] += H1;

                for (int i = 2; i < n - 2; i++)
                {
                    b[(m - 2) * (i - 1)] -= result[i, 0] * H2;
                    b[(m - 2) * (i - 1) + m - 3] -= result[i, m - 1] * H2;

                    A[(m - 2) * (i - 1), (m - 2) * (i - 1)] += -2 * (H1 + H2);
                    A[(m - 2) * (i - 1), (m - 2) * (i - 1) + 1] += H2;
                    A[(m - 2) * (i - 1), (m - 2) * (i - 2)] += H1;
                    A[(m - 2) * (i - 1), (m - 2) * i] += H1;

                    A[(m - 2) * (i - 1) + m - 3, (m - 2) * (i - 1) + m - 3] += -2 * (H1 + H2);
                    A[(m - 2) * (i - 1) + m - 3, (m - 2) * (i - 1) + m - 2] += H2;
                    A[(m - 2) * (i - 1) + m - 3, (m - 2) * (i - 2) + m - 3] += H1;
                    A[(m - 2) * (i - 1) + m - 3, (m - 2) * i + m - 3] += H1;
                }

                for (int i = 1, k = m - 2; i < n - 3; i++)
                {
                    for (int j = 1; j < m - 3; j++, k++)
                    {
                        f["x"] = Gh[i, j][0];
                        f["y"] = Gh[i, j][1];
                        b[k] += (-1) * f.Calculate();
                        
                        A[(m - 2) * i + j, (m - 2) * i + j] += -2 * (H1 + H2);
                        A[(m - 2) * i + j, (m - 2) * i + j + 1] += H2;
                        A[(m - 2) * i + j, (m - 2) * i + j - 1] += H2;
                        A[(m - 2) * i + j, (m - 2) * (i - 1) + j] += H1;
                        A[(m - 2) * i + j, (m - 2) * (i + 1) + j] += H1;
                    }
                }

                Vector resultInner;
                if (!double.IsNaN(eps))
                {
                    resultInner = JacobiMethod.Solve(A, b, eps);
                }
                else
                {
                    resultInner = GaussMethod.Solve(A, b);
                }
                for(int i = 1; i < n - 1; i++)
                {
                    for(int j = 1; j < m - 1; j++)
                    {
                        result[i, j] = resultInner[(n - 2) * (i - 1) + j - 1];
                    }
                }

                return result;
            }
        }

        public abstract class ParabolicType
        {
            public static double[,] Solve(Function f, Function g, Function mu1,
                                          Function mu2, Vector[,] Gh, double p)
            {
                double h1 = Gh[0, 1][0] - Gh[0, 0][0];
                double h2 = Gh[1, 0][1] - Gh[0, 0][1];
                int n = Gh.GetLength(0);
                int m = Gh.GetLength(1);
                double[,] result = new double[n, m];

                for (int i = 0; i < n; i++)
                {
                    mu2["t"] = Gh[i, m - 1][1];
                    result[i, m - 1] = mu2.Calculate();

                    mu1["t"] = Gh[i, 0][1];
                    result[i, 0] = mu1.Calculate();
                }

                for (int i = 1; i < m - 1; i++)
                {
                    f["x"] = Gh[0, i][0];
                    result[0, i] = f.Calculate();
                }

                Matrix A = new Matrix((m - 2) * (n - 1));
                Vector b = new Vector((m - 2) * (n - 1));

                double H = 1 / h1 / h1;
                double tau = 1 / h2;
                for (int i = 1; i < n - 1; i++)
                {
                    g["x"] = Gh[i, 1][0];
                    g["t"] = Gh[i, 1][1];
                    b[(m - 2) * (n - 2 - i)] += g.Calculate() +
                                                (1 - p) * result[i, 0] * H +
                                                p * result[i + 1, 0] * H;

                    A[(m - 2) * (n - 2 - i), (m - 2) * (n - 1 - i)] += -tau + (1 - p) * 2 * H;
                    A[(m - 2) * (n - 2 - i), (m - 2) * (n - 2 - i)] += tau + p * 2 * H;
                    A[(m - 2) * (n - 2 - i), (m - 2) * (n - 1 - i) + 1] += -(1 - p) * H;
                    A[(m - 2) * (n - 2 - i), (m - 2) * (n - 2 - i) + 1] += -p * H;

                    g["x"] = Gh[i, m - 2][0];
                    g["t"] = Gh[i, m - 2][1];
                    b[(m - 2) * (n - 2 - i) + m - 3] += g.Calculate() +
                                                (1 - p) * result[i, m - 1] * H +
                                                p * result[i + 1, m - 1] * H;

                    A[(m - 2) * (n - 2 - i) + m - 3, (m - 2) * (n - 1 - i) + m - 3] += -tau + (1 - p) * 2 * H;
                    A[(m - 2) * (n - 2 - i) + m - 3, (m - 2) * (n - 2 - i) + m - 3] += tau + p * 2 * H;
                    A[(m - 2) * (n - 2 - i) + m - 3, (m - 2) * (n - 1 - i) + m - 4] += -(1 - p) * H;
                    A[(m - 2) * (n - 2 - i) + m - 3, (m - 2) * (n - 2 - i) + m - 4] += -p * H;
                }

                g["x"] = Gh[0, 1][0];
                g["t"] = Gh[0, 1][1];
                b[(m - 2) * (n - 2)] += g.Calculate() +
                                        (-(1 - p) * 2 * H + tau) * result[0, 1] +
                                        (1 - p) * H * result[0, 0] +
                                        (1 - p) * H * result[0, 2] +
                                        p * H * result[1, 0];

                A[(m - 2) * (n - 2), (m - 2) * (n - 2)] += tau + p * 2 * H;
                A[(m - 2) * (n - 2), (m - 2) * (n - 2) + 1] += -p * H;


                g["x"] = Gh[0, m - 2][0];
                g["t"] = Gh[0, m - 2][1];
                b[(m - 2) * (n - 1) - 1] += g.Calculate() +
                                            (-(1 - p) * 2 * H + tau) * result[0, m - 2] +
                                            (1 - p) * H * result[0, m - 3] +
                                            (1 - p) * H * result[0, m - 1] +
                                            p * H * result[1, m - 1];

                A[(m - 2) * (n - 1) - 1, (m - 2) * (n - 1) - 1] += tau + p * 2 * H;
                A[(m - 2) * (n - 1) - 1, (m - 2) * (n - 1) - 2] += -p * H;


                for (int i = 2; i < m - 2; i++)
                {
                    g["x"] = Gh[0, i][0];
                    g["t"] = Gh[0, i][1];
                    b[(m - 2) * (n - 2) + i - 1] += g.Calculate() +
                                                    (-(1 - p) * 2 * H + tau) * result[0, i] +
                                                    (1 - p) * H * result[0, i + 1] +
                                                    (1 - p) * H * result[0, i - 1];

                    A[(m - 2) * (n - 2) + i - 1, (m - 2) * (n - 2) + i - 1] += tau + p * 2 * H;
                    A[(m - 2) * (n - 2) + i - 1, (m - 2) * (n - 2) + i] += -p * H;
                    A[(m - 2) * (n - 2) + i - 1, (m - 2) * (n - 2) + i - 2] += -p * H;
                }

                for (int i = 1; i < n - 1; i++)
                {
                    for (int j = 2; j < m - 2; j++)
                    {
                        g["x"] = Gh[i, j][0];
                        g["t"] = Gh[i, j][1];
                        b[(m - 2) * (n - 2 - i) + j - 1] += g.Calculate();

                        A[(m - 2) * (n - 2 - i) + j - 1, (m - 2) * (n - 1 - i) + j - 1] += -tau + (1 - p) * 2 * H;
                        A[(m - 2) * (n - 2 - i) + j - 1, (m - 2) * (n - 1 - i) + j] += -(1 - p) * H;
                        A[(m - 2) * (n - 2 - i) + j - 1, (m - 2) * (n - 1 - i) + j - 2] += -(1 - p) * H;
                        A[(m - 2) * (n - 2 - i) + j - 1, (m - 2) * (n - 2 - i) + j - 1] += tau + p * 2 * H;
                        A[(m - 2) * (n - 2 - i) + j - 1, (m - 2) * (n - 2 - i) + j] += -p * H;
                        A[(m - 2) * (n - 2 - i) + j - 1, (m - 2) * (n - 2 - i) + j - 2] += -p * H;

                    }
                }

                Vector resultInner;
                if (p == 1)
                {
                    resultInner = SweepMethod.Solve(A, b);
                }
                else
                {
                    resultInner = GaussMethod.Solve(A, b);
                }
                for (int i = n - 1; i > 0; i--)
                {
                    for (int j = 1; j < m - 1; j++)
                    {
                        result[i, j] = resultInner[(m - 2) * (n - 1 - i) + j - 1];
                    }
                }

                return result;
            }
        }

        public abstract class HyperbolicType
        {
            public static double[,] Solve(Function g, Function phi, Function psi,
                                          Function mu1, Function mu2, Vector[,] Gh)
            {
                double h1 = Gh[0, 1][0] - Gh[0, 0][0];
                double tau = Gh[1, 0][1] - Gh[0, 0][1];
                int n = Gh.GetLength(0);
                int m = Gh.GetLength(1);
                double[,] result = new double[n, m];

                for (int i = 1; i < m; i++)
                {
                    mu1["t"] = Gh[0, i][0];
                    result[0, i] = mu1.Calculate();

                    mu2["t"] = Gh[n - 1, i][0];
                    result[n - 1, i] = mu2.Calculate();
                }

                for (int i = 0; i < n; i++)
                {
                    phi["x"] = Gh[i, 0][1];
                    result[i, 0] = phi.Calculate();
                }

                Function phi2Der = phi.Differentiate("x").Differentiate("x");
                for (int i = 1; i < n - 1; i++)
                {
                    phi["x"] = psi["x"] = phi2Der["x"] = Gh[i, 0][1];
                    result[i, 1] = result[i, 0] + tau * psi.Calculate() + tau * tau / 2 * phi2Der.Calculate();
                }

                Matrix A = new Matrix((m - 2) * (n - 2));
                Vector b = new Vector((m - 2) * (n - 2));

                double delta = tau * tau / h1 / h1;
                for (int i = 1; i < n - 1; i++)
                {
                    g["x"] = Gh[i, 1][0];
                    g["t"] = Gh[i, 1][1];
                    b[(m - 2) * (n - 2 - i)] += g.Calculate() +
                                                -result[i + 1, 1] +
                                                delta * result[i, 0] +
                                                2 * (1 - delta) * result[i, 1] +
                                                -result[i - 1, 1];

                    A[(m - 2) * (n - 2 - i), (m - 2) * (n - 2 - i)] += -delta;
                }

                g["x"] = Gh[n - 1, 2][0];
                g["t"] = Gh[n - 1, 2][1];
                b[1] += g.Calculate() + 
                        -result[n - 2, 1] +
                        delta * result[n - 1, 2];

                A[1, 0] += -2 * (1 - delta);
                A[1, 1] += -delta;
                A[1, m - 2] += 1;

                g["x"] = Gh[1, 2][0];
                g["t"] = Gh[1, 2][1];
                b[(m - 2) * (n - 3) + 1] += g.Calculate() + 
                                            delta * result[1, 1] +
                                            -result[0, 2];

                A[(m - 2) * (n - 3) + 1, (m - 2) * (n - 3)] += -2 * (1 - delta);
                A[(m - 2) * (n - 3) + 1, (m - 2) * (n - 3) + 1] += -delta;
                A[(m - 2) * (n - 3) + 1, (m - 2) * (n - 4)] += 1;

                for (int i = 2; i < n - 2; i++)
                {
                    g["x"] = Gh[i, 2][0];
                    g["t"] = Gh[i, 2][1];
                    b[(m - 2) * (n - 2 - i) + 1] += g.Calculate() +
                                                    delta * result[i, 1];

                    A[(m - 2) * (n - 2 - i) + 1, (m - 2) * (n - 3 - i)] += 1;
                    A[(m - 2) * (n - 2 - i) + 1, (m - 2) * (n - 2 - i)] += -2 * (1 - delta);
                    A[(m - 2) * (n - 2 - i) + 1, (m - 2) * (n - 2 - i) + 1] += -delta;
                    A[(m - 2) * (n - 2 - i) + 1, (m - 2) * (n - 1 - i)] += 1;
                }

                for (int i = 3; i < m - 1; i++)
                {
                    g["x"] = Gh[n - 2, i][0];
                    g["t"] = Gh[n - 2, i][1];
                    b[i - 1] += g.Calculate() +
                                -result[n - 1, i];

                    A[i - 1, i - 3] += -delta;
                    A[i - 1, i - 2] += -2 * (1 - delta);
                    A[i - 1, i - 1] += -delta;
                    A[i - 1, m - 2 + i - 2] += 1;

                    g["x"] = Gh[1, i][0];
                    g["t"] = Gh[1, i][1];
                    b[(m - 2) * (n - 3) + i - 1] += g.Calculate() +
                                                    result[0, i];

                    A[(m - 2) * (n - 3) + i - 1, (m - 2) * (n - 4) + i - 2] += 1;
                    A[(m - 2) * (n - 3) + i - 1, (m - 2) * (n - 3) + i - 3] += -delta;
                    A[(m - 2) * (n - 3) + i - 1, (m - 2) * (n - 3) + i - 2] += -2 * (1 - delta);
                    A[(m - 2) * (n - 3) + i - 1, (m - 2) * (n - 3) + i - 1] += -delta;
                }

                for (int i = 2; i < n - 2; i++)
                {
                    for (int j = 3; j < m - 1; j++)
                    {
                        g["x"] = Gh[i, j][0];
                        g["t"] = Gh[i, j][1];
                        b[(m - 2) * (n - 2 - i) + j - 1] += g.Calculate();

                        A[(m - 2) * (n - 2 - i) + j - 1, (m - 2) * (n - 3 - i) + j - 2] += 1;
                        A[(m - 2) * (n - 2 - i) + j - 1, (m - 2) * (n - 2 - i) + j - 3] += -delta;
                        A[(m - 2) * (n - 2 - i) + j - 1, (m - 2) * (n - 2 - i) + j - 2] += -2 * (1 - delta);
                        A[(m - 2) * (n - 2 - i) + j - 1, (m - 2) * (n - 2 - i) + j - 1] += -delta;
                        A[(m - 2) * (n - 2 - i) + j - 1, (m - 2) * (n - 1 - i) + j - 2] += 1;
                    }
                }

                Vector resultInner = GaussMethod.Solve(A, b);
                for (int i = 1; i < n - 1; i++)
                {
                    for (int j = 2; j < m; j++)
                    {
                        result[i, j] = resultInner[(m - 2) * (n - 2 - i) + j - 2];
                    }
                }

                return result;
            }
        }
    }
}
