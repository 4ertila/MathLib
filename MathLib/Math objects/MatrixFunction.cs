using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathLib.Objects
{
    public class MatrixFunction
    {
        public MatrixFunction(Function[,] functions, IEnumerable<string> variableNames)
        {
            rows = functions.GetLength(0);
            columns = functions.GetLength(1);

            this.functions = new Function[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    this.functions[i, j] = functions[i, j];
                }
            }

            variables = new Dictionary<string, double>();
            foreach (string variableName in variableNames)
            {
                variables.Add(variableName, double.NaN);
            }
        }
        public MatrixFunction(MatrixFunction matrixFunction)
        {
            columns = matrixFunction.columns;
            rows = matrixFunction.rows;

            variables = new Dictionary<string, double>(matrixFunction.variables);

            functions = new Function[rows, columns];
            for(int i = 0; i < rows; i++)
            {
                for(int j = 0; j < columns; j++)
                {
                    functions[i, j] = matrixFunction[i, j];
                }
            }
        }
        public MatrixFunction()
        {
            columns = rows = 0;
            variables = new Dictionary<string, double>();
            functions = new Function[0, 0];
        }

        public int columns { private set; get; }
        public int rows { private set; get; }
        private Dictionary<string, double> variables;
        private Function[,] functions;

        public double this[string variableName]
        {
            get
            {
                return variables[variableName];
            }
            set
            { 
                variables[variableName] = value;
            }
        }

        public Function this[int i, int j]
        {
            get
            {
                return new Function(functions[i, j]);
            }
            set
            {
                functions[i, j] = value;
            }
        }

        public IEnumerable<KeyValuePair<string, double>> Variables => variables;

        public Matrix Calculate()
        {
            Matrix result = new Matrix(rows, columns);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    result[i, j] = functions[i, j].Calculate();
                }
            }

            return result;
        }
        public Matrix Calculate(IEnumerable<KeyValuePair<string, double>> variableValues)
        {
            foreach (KeyValuePair<string, double> variable in variableValues)
            {
                variables[variable.Key] = variable.Value;
            }

            return Calculate();
        }
    }
}
