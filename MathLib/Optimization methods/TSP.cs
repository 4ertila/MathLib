using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathLib.Objects;

namespace MathLib.OptimizationMethods
{
    public abstract class TSP
    {
        public struct Step
        {
            public int u;
            public int v;
            public bool available;

            public Step(int u, int v, bool available)
            {
                this.u = u;
                this.v = v;
                this.available = available;
            }
        }

        private double SumSubstractMin(Matrix C)
        {
            Matrix C1 = new Matrix(C);
            double min;
            double sum = 0;

            for (int i = 0; i < C1.Rows; i++)
            {
                min = C1.MinInRow(i, out _);
                sum += min;
                if (min != 0)
                {
                    for (int j = 0; j < C1.Columns; j++)
                    {
                        C1[i, j] -= min;
                    }
                }
            }

            for (int i = 0; i < C1.Columns; i++)
            {
                min = C1.MinInColumn(i, out _);
                sum += min;
                if (min != 0)
                {
                    for (int j = 0; j < C1.Rows; j++)
                    {
                        C1[i, j] -= min;
                    }
                }
            }

            return sum;
        }

        public double Solve(Matrix C, List<Step> restrictions, Vector U, Vector V, int n)
        {
            if (C.Rows > 2)
            {
                Matrix C1 = new Matrix(C);
                double sum = 0;
                double min = double.MaxValue;
                double max = double.MinValue;
                double tValue;
                int u, v;
                u = 0;
                v = 1;

                for (int i = 0; i < C1.Rows; i++)
                {
                    min = C1.MinInRow(i, out _);
                    sum += min;
                    if (min != 0)
                    {
                        for (int j = 0; j < C1.Columns; j++)
                        {
                            C1[i, j] -= min;
                        }
                    }
                }

                for (int i = 0; i < C1.Columns; i++)
                {
                    min = C1.MinInColumn(i, out _);
                    sum += min;
                    if (min != 0)
                    {
                        for (int j = 0; j < C1.Rows; j++)
                        {
                            C1[i, j] -= min;
                        }
                    }
                }

                for (int i = 0; i < C1.Rows; i++)
                {
                    for (int j = 0; j < C1.Columns; j++)
                    {
                        if (C1[i, j] == 0)
                        {
                            C1[i, j] = double.PositiveInfinity;
                            tValue = SumSubstractMin(C1);
                            if (tValue > max)
                            {
                                max = tValue;
                                u = i;
                                v = j;
                            }
                            C1[i, j] = 0;
                        }
                    }
                }
                
                Matrix C2 = new Matrix(C.DeleteRow(u).DeleteColumn(v));
                double sum1, sum2;
                sum2 = SumSubstractMin(C2);
                C1[u, v] = double.PositiveInfinity;
                sum1 = SumSubstractMin(C1);
                if (sum1 <= sum2)
                {
                    restrictions.Add(new Step(u, v, true));
                    tValue = Solve(C2, restrictions, U.DeleteElement(u), V.DeleteElement(v), n);
                    if (tValue.Equals(double.NaN) || sum1 < tValue)
                    {
                        restrictions[restrictions.Count - 1] = new Step(u, v, false);
                        return Solve(C1, restrictions, U, V, n);
                    }
                    else
                    {
                        return tValue;
                    }
                }
                else
                {
                    restrictions.Add(new Step(u, v, false));
                    tValue = Solve(C1, restrictions, U, V, n);
                    if (tValue.Equals(double.NaN) || sum2 < tValue)
                    {
                        restrictions[restrictions.Count - 1] = new Step(u, v, true);
                        return Solve(C2, restrictions, U.DeleteElement(u), V.DeleteElement(v), n);
                    }
                    else
                    {
                        return tValue;
                    }
                }
            }
            else
            {
                restrictions.Add(new Step((int)U[0], (int)V[0], true));
                restrictions.Add(new Step((int)U[1], (int)V[1], true));
                double[] tArr = new double[n];
                for(int i = 0; i < restrictions.Count; i++)
                {
                    tArr[restrictions[i].v]++;
                }
                tArr[restrictions[0].u]++;
                for (int i = 0; i < n; i++)
                {
                    if(tArr[i] == 0)
                    {
                        return double.NaN;
                    }
                }
                return double.NaN;
            }
        }
    }
}
