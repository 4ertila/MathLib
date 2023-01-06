using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathLib.Objects;

namespace MathLib.Objects
{
    public class VectorFunction
    {
        public VectorFunction(Function[] functions, IEnumerable<KeyValuePair<string, double>> variables)
        {
            dim = functions.Length;
            this.functions = new Function[dim];
            for (int i = 0; i < dim; i++)
            {
                this.functions[i] = new Function(functions[i]);
            }

            this.variables = new Dictionary<string, double>();
            foreach(KeyValuePair<string, double> variable in variables)
            {
                this.variables.Add(variable.Key, variable.Value);
            }
        }
        public VectorFunction(Function[] functions, IEnumerable<string> variableNames)
        {
            dim = functions.Length;
            this.functions = new Function[dim];
            for (int i = 0; i < dim; i++)
            {
                this.functions[i] = new Function(functions[i]);
            }

            variables = new Dictionary<string, double>();
            foreach(string name in variableNames)
            {
                variables.Add(name, double.NaN);
            }
        }
        public VectorFunction(Function[] functions, params string[] variableNames)
        {
            dim = functions.Length;
            this.functions = new Function[dim];
            for (int i = 0; i < dim; i++)
            {
                this.functions[i] = new Function(functions[i]);
            }

            variables = new Dictionary<string, double>();
            foreach (string name in variableNames)
            {
                variables.Add(name, double.NaN);
            }
        }
        public VectorFunction(Function[] functions)
        {
            dim = functions.Length;
            this.functions = new Function[dim];
            variables = new Dictionary<string, double>();
            for (int i = 0; i < dim; i++)
            {
                this.functions[i] = new Function(functions[i]);

                foreach(var variable in functions[i].Variables)
                {
                    if(!variables.ContainsKey(variable.Key))
                    {
                        variables.Add(variable.Key, double.NaN);
                    }
                }
            }
        }
        public VectorFunction(VectorFunction vectorFunction)
        {
            dim = vectorFunction.dim;
            functions = new Function[dim];
            for (int i = 0; i < dim; i++)
            {
                functions[i] = vectorFunction.functions[i];
            }

            variables = new Dictionary<string, double>(vectorFunction.variables);
        }
        public VectorFunction(int dim)
        {
            this.dim = dim;
            functions = new Function[dim];
            variables = new Dictionary<string, double>();
        }
        public VectorFunction()
        {
            dim = 0;
            functions = new Function[dim];
            variables = new Dictionary<string, double>();
        }

        private Function[] functions;
        private Dictionary<string, double> variables;
        public int dim { private set; get; }

        public double this[string variableName]
        {
            get 
            { 
                return variables[variableName];
            }
            set
            {
                variables[variableName] = value;

                for (int i = 0; i < dim; i++)
                {
                    functions[i][variableName] = value;
                }
            }
        }

        public Function this[int i]
        {
            set
            {
                functions[i] = new Function(value);

                foreach (var variable in value.Variables)
                {
                    if (!variables.ContainsKey(variable.Key))
                    {
                        variables.Add(variable.Key, double.NaN);
                    }
                }
            }
            get
            {
                return new Function(functions[i]);
            }
        }

        public IEnumerable<KeyValuePair<string, double>> Variables => variables;

        public IEnumerable<Function> Functions => functions;

        public void Init(Function[] functions, IEnumerable<KeyValuePair<string, double>> variables)
        {
            dim = functions.Length;
            this.functions = new Function[dim];
            for (int i = 0; i < dim; i++)
            {
                this.functions[i] = new Function(functions[i]);
            }

            this.variables = new Dictionary<string, double>();
            foreach (KeyValuePair<string, double> variable in variables)
            {
                this.variables.Add(variable.Key, variable.Value);
            }
        }
        public void Init(Function[] functions, IEnumerable<string> variableNames)
        {
            dim = functions.Length;
            this.functions = new Function[dim];
            for (int i = 0; i < dim; i++)
            {
                this.functions[i] = new Function(functions[i]);
            }

            variables = new Dictionary<string, double>();
            foreach (string name in variableNames)
            {
                variables.Add(name, double.NaN);
            }
        }
        public void Init(VectorFunction vectorFunction)
        {
            dim = vectorFunction.dim;
            functions = new Function[dim];
            for (int i = 0; i < dim; i++)
            {
                functions[i] = vectorFunction.functions[i];
            }

            variables = new Dictionary<string, double>(vectorFunction.variables);
        }

        public Vector Calculate()
        {
            Vector result = new Vector(dim);

            for (int i = 0; i < dim; i++)
            {
                result[i] = functions[i].Calculate(variables);
            }

            return result;
        }
        public Vector Calculate(IEnumerable<KeyValuePair<string, double>> variableValues)
        {
            foreach (KeyValuePair<string, double> variable in variableValues)
            {
                variables[variable.Key] = variable.Value;
            }

            return Calculate();
        }
    }
}
