using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace MathLib.Objects
{
    public abstract class Operators
    {
        public static string[] operators = new string[]
        {
            "+", "-", "*", "/", "^",
            "cos", "sin"
        };
        public static string Derivative(string operatorName,
                                  string arg1, string arg1Deriavative,
                                  string arg2, string arg2Deriavative,
                                  string variable)
        {
            if(arg1Deriavative == "0" && arg2Deriavative == "0")
            {
                return "0";
            }
            switch (operatorName)
            {
                case "*":
                    if (arg1Deriavative == "0")
                    {
                        return "(" + arg2Deriavative + ")" + "*(" + arg1 + ")";
                    }
                    else if(arg2Deriavative == "0")
                    {
                        return "(" + arg1Deriavative + ")" + "*(" + arg2 + ")";
                    }
                    else
                    {
                        return "(" + arg2Deriavative + ")" + "*(" + arg1 + ")" + "+"
                        + "(" + arg1Deriavative + ")" + "*(" + arg2 + ")";
                    }
                case "+":
                    if (arg1Deriavative == "0")
                    {
                        return arg2Deriavative;
                    }
                    else if (arg2Deriavative == "0")
                    {
                        return arg1Deriavative;
                    }
                    else
                    {
                        return arg2Deriavative + "+" + arg1Deriavative;
                    }
                case "-":
                    if (arg1Deriavative == "0")
                    {
                        return "(-1)*("+arg2Deriavative+")";
                    }
                    else if (arg2Deriavative == "0")
                    {
                        return arg1Deriavative;
                    }
                    else
                    {
                        return "("+arg1Deriavative + ")-(" + arg2Deriavative+")";
                    }
                case "/":
                    if (!arg1.Contains(variable))
                    {
                        return"(-1)*("+arg1+")*("+arg2Deriavative+")" + "/" + "(" + arg2 + ")^(2)";
                    }
                    else if (!arg2.Contains(variable))
                    {
                        return "(" + arg1Deriavative + ")" + "/" + "(" + arg2 + ")";
                    }
                    else
                    {
                        return "(" + "(" + arg1Deriavative + ")" + "*(" + arg2 + ")" +
                            "-"
                        + "(" + arg2Deriavative + ")" + "*(" + arg1 + ")" + ")" + "/" +
                            "(" + arg2 + ")^(2)";
                    }
                case "^":
                    if (arg1.Contains(variable))
                    {
                        if(arg2Deriavative  == "0")
                        {
                            /*return (double.Parse(arg2)).ToString()+"*("+arg1+")^("+ (double.Parse(arg2) - 1).ToString()+")"
                                + "*("+arg1Deriavative+")";*/
                            return $"({arg2})" + "*(" + arg1 + ")^(" + arg2 + "-1)"
                                + "*(" + arg1Deriavative + ")";
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        if (arg2Deriavative == "0")
                        {
                            return "("+arg1+")^("+arg2+")";
                        }
                        else
                        {
                            return $"({arg2})" + "*(" + arg1 + ")^(" + arg2 + "-1)"
                                + "*(" + arg1Deriavative + ")";
                        }
                    }
                    break;
                default:
                    return null;
            }
        }
        public static string Derivative(string operatorName,
                                  string arg, string argDeriavative,
                                  string variable)
        {
            switch (operatorName)
            {
                case "sin":
                    if (argDeriavative == "0")
                    {
                        return "0";
                    }
                    else
                    {
                        if (argDeriavative.Contains("+") || argDeriavative.Contains("-"))
                        {
                            return "(" + argDeriavative + ")*" + "cos(" + arg + ")";
                        }
                        else
                        {
                            return argDeriavative + "*" + "cos(" + arg + ")";
                        }
                    }
                case "cos":
                    {
                        if (argDeriavative == "0")
                        {
                            return "0";
                        }
                        else
                        {
                            return "(-1)*(" + argDeriavative + ")*" + "sin(" + arg + ")";
                        }
                    }
                default:
                    return null;
            }
        }
    }

    public class Function
    {
        public Function(string infix, IEnumerable<KeyValuePair<string, double>> variables)
        {
            this.infix = infix;
            this.variables = new Dictionary<string, double>();
            foreach (var variable in variables)
            {
                this.variables.Add(variable.Key, variable.Value);
            }

            ToPostfix(infix);
        }
        public Function(string infix, IEnumerable<string> variableNames)
        {
            this.infix = infix;
            variables = new Dictionary<string, double>();
            foreach(string name in variableNames)
            {
                variables.Add(name, double.NaN);
            }

            ToPostfix(infix);
        }
        public Function(string infix, params string[] variableNames)
        {
            this.infix = infix;
            variables = new Dictionary<string, double>();
            foreach (string name in variableNames)
            {
                variables.Add(name, double.NaN);
            }

            ToPostfix(infix);
        }
        public Function(string infix)
        {
            this.infix = infix;
            variables = new Dictionary<string, double>();

            ToPostfix(infix);
        }
        public Function(Function function)
        {
            infix = function.infix;
            postfix = function.postfix;

            postfixNodes = new string[function.postfixNodes.Length];
            function.postfixNodes.CopyTo(postfixNodes, 0);

            variables = new Dictionary<string, double>(function.variables);
        }
        public Function()
        {
            postfix = "";
            infix = "";
            variables = new Dictionary<string, double>();
        }

        private string postfix;
        public string infix { private set; get; }
        private string[] standartOperators = new string[] { "+", "-", "(", ")", "*", "/", "^", "|" };
        private string[] postfixNodes;
        private Dictionary<string, double> standartConstants = new Dictionary<string, double>()
        {
            { "pi", PI },
            { "e", E }
        };
        private Dictionary<string, double> variables;
        public double lastFunctionValue { private set; get; } = double.NaN;

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

        public IEnumerable<KeyValuePair<string, double>> Variables => variables;

        public void Init(string infix, IEnumerable<KeyValuePair<string, double>> variables)
        {
            this.infix = infix;

            this.variables = new Dictionary<string, double>();
            foreach (var variable in variables)
            {
                this.variables.Add(variable.Key, variable.Value);
            }

            ToPostfix(infix);
        }
        public void Init(string infix, IEnumerable<string> variableNames)
        {
            this.infix = infix;

            variables = new Dictionary<string, double>();
            foreach (string name in variableNames)
            {
                variables.Add(name, double.NaN);
            }

            ToPostfix(infix);
        }
        public void Init(string infix)
        {
            this.infix = infix;

            variables = new Dictionary<string, double>();

            ToPostfix(infix);
        }
        public void Init(Function function)
        {
            infix = function.infix;
            postfix = function.postfix;

            variables = new Dictionary<string, double>(function.variables);
        }

        public double Calculate()
        {
            Stack<double> valuesQueue = new Stack<double>();
            double tValue;

            for (int i = 0; i < postfixNodes.Length - 1; i++)
            {
                if (double.TryParse(postfixNodes[i], out tValue) || variables.TryGetValue(postfixNodes[i], out tValue) || 
                    standartConstants.TryGetValue(postfixNodes[i], out tValue))
                {
                    valuesQueue.Push(tValue);
                }
                else
                {
                    CalculateOperator(postfixNodes[i], out tValue, valuesQueue);
                    valuesQueue.Push(tValue);
                }
            }

            lastFunctionValue = valuesQueue.Pop();
            return lastFunctionValue;
        }
        public double Calculate(IEnumerable<KeyValuePair<string, double>> variableValues)
        {
            foreach (KeyValuePair<string, double> variable in variableValues)
            {
                variables[variable.Key] = variable.Value;
            }
            return Calculate();
        }

        public Function Differentiate(string variable)
        {
            Stack<(string, string)> valuesQueue = new Stack<(string, string)>();
            string[] nodes = postfix.Split(' ');
            double tValue;
            for (int i = 0; i < nodes.Length - 1; i++)
            {
                if(nodes[i] == variable)
                {
                    valuesQueue.Push((nodes[i], "1"));
                }
                else if (double.TryParse(nodes[i], out _) || standartConstants.ContainsKey(nodes[i]) ||
                                         variables.ContainsKey(nodes[i]))
                {
                    valuesQueue.Push((nodes[i], "0"));
                }
                else if(standartOperators.Contains(nodes[i]))
                {
                    /*if (nodes[i] == "-" && valuesQueue.Count == 1)
                    {
                        var operand1 = valuesQueue.Pop();
                        valuesQueue.Push(("(-1)*(" + operand1.Item1 + ")", "(-1)*(" + operand1.Item2 + ")"));
                    }
                    else if(nodes[i] == "-" && standartOperators.Contains(nodes[i+1]))
                    {
                        var operand1 = valuesQueue.Pop();
                        valuesQueue.Push(("(-1)*(" + operand1.Item1 + ")", "(-1)*(" + operand1.Item2 + ")"));
                    }
                    else
                    {*/
                        var operand1 = valuesQueue.Pop();
                        var operand2 = valuesQueue.Pop();
                        valuesQueue.Push(("(" + operand2.Item1 + ")" + nodes[i] + "(" + operand1.Item1 + ")", Operators.Derivative(nodes[i],
                                                               operand2.Item1, operand2.Item2,
                                                               operand1.Item1, operand1.Item2, variable)));
                    //}
                }
                else
                {
                    var operand = valuesQueue.Pop();
                    valuesQueue.Push((nodes[i] + "("+operand.Item1+")", Operators.Derivative(nodes[i],
                                                           operand.Item1, operand.Item2,variable)));
                }
            }

            return new Function(valuesQueue.Pop().Item2, variables);
        }

        private void CalculateOperator(string operatorName, out double value, Stack<double> queue)
        {
            double tValue;
            switch(operatorName)
            {
                case "+":
                    value = queue.Pop() + queue.Pop();
                    break;

                case "*":
                    value = queue.Pop() * queue.Pop();
                    break;

                case "/":
                    value = 1 / (queue.Pop() / queue.Pop());
                    break;

                case "-":
                    value = -queue.Pop() + queue.Pop();
                    break;

                case "^":
                    tValue = queue.Pop();
                    value = Pow(queue.Pop(), tValue);
                    break;

                case "abs":
                    value = Abs(queue.Pop());
                    break;

                case "sqrt":
                    value = Sqrt(queue.Pop());
                    break;

                case "ln":
                    value = Log(queue.Pop());
                    break;

                case "sin":
                    value = Sin(queue.Pop());
                    break;
                case "asin":
                    value = Asin(queue.Pop());
                    break;
                case "sinh":
                    value = Sinh(queue.Pop());
                    break;

                case "cos":
                    value = Cos(queue.Pop());
                    break;
                case "acos":
                    value = Acos(queue.Pop());
                    break;
                case "cosh":
                    value = Cosh(queue.Pop());
                    break;

                case "tan":
                    value = Tan(queue.Pop());
                    break;
                case "atan":
                    value = Atan(queue.Pop());
                    break;
                case "tanh":
                    value = Tanh(queue.Pop());
                    break;

                case "cot":
                    value = 1 / Tan(queue.Pop());
                    break;
                case "acot":
                    value = PI / 2 - Atan(queue.Pop());
                    break;
                case "coth":
                    value = (Exp(queue.Pop()) + Exp(-queue.Pop())) / (Exp(queue.Pop()) - Exp(-queue.Pop()));
                    break;

                default:
                    value = double.NaN;
                    break;
            }
        }

        private void ToPostfix(string infix)
        {
            int pos = 0;
            postfix = "";
            Stack<string> operators = new Stack<string>();
            string tStr = "";
            bool isAbsOpen = false;

            while (pos < infix.Length)
            {
                if(char.IsDigit(infix[pos]))
                {
                    while (pos < infix.Length && (char.IsDigit(infix[pos]) || infix[pos] == ','))
                    {
                        postfix += infix[pos];
                        pos++;
                    }
                    if(pos < infix.Length - 3 && infix[pos] == 'E')
                    {
                        postfix += "E" + infix[pos + 1];
                        pos += 2;

                        while (pos < infix.Length && char.IsDigit(infix[pos]))
                        {
                            postfix += infix[pos];
                            pos++;
                        }
                    }
                    postfix += ' ';
                }

                else if(infix[pos] == ' ')
                {
                    pos++;
                }

                else if(standartOperators.Contains(infix[pos].ToString()))
                {
                    if (infix[pos] == ')')
                    {
                        while (operators.Count > 0 && operators.Peek() != "(")
                        {
                            postfix += operators.Pop() + ' ';
                        }
                        operators.Pop();
                        pos++;
                    }
                    else if (infix[pos] == '|')
                    {
                        if (isAbsOpen)
                        {
                            while (operators.Count > 0 && operators.Peek() != "|")
                            {
                                postfix += operators.Pop() + ' ';
                            }
                            isAbsOpen = false;
                            operators.Pop();
                            operators.Push("abs");
                            pos++;
                        }
                        else
                        {
                            isAbsOpen = true;
                            operators.Push("|");
                            pos++;
                        }
                    }
                    else if (infix[pos] == '(')
                    {
                        operators.Push(infix[pos].ToString());
                        pos++;
                    }
                    else
                    {
                        if (infix[pos] == '-')
                        {
                            if (pos == 0)
                            {
                                postfix += "-1 ";
                                operators.Push("*");
                                pos++;
                            }
                            else if (standartOperators.Contains(infix[pos - 1].ToString()) && infix[pos - 1] !=')')
                            {
                                postfix += "-1 ";
                                while (operators.Count > 0 && GetPriority("*") <= GetPriority(operators.Peek()) && operators.Peek() != "(")
                                {
                                    postfix += operators.Pop() + ' ';
                                }
                                operators.Push("*");
                                pos++;
                            }
                            else
                            {
                                while (operators.Count > 0 && GetPriority(infix[pos].ToString()) <= GetPriority(operators.Peek()) && operators.Peek() != "(")
                                {
                                    postfix += operators.Pop() + ' ';
                                }
                                operators.Push(infix[pos].ToString());
                                pos++;
                            }
                        }
                        else
                        {
                            while (operators.Count > 0 && GetPriority(infix[pos].ToString()) <= GetPriority(operators.Peek()) && operators.Peek() != "(")
                            {
                                postfix += operators.Pop() + ' ';
                            }
                            operators.Push(infix[pos].ToString());
                            pos++;
                        }
                    }
                }

                else if(infix[pos] >= 'a' && infix[pos] <= 'z')
                {
                    tStr = "";
                    while(pos < infix.Length && !standartOperators.Contains(infix[pos].ToString()) && infix[pos] != ' ')
                    {
                        tStr += infix[pos];
                        pos++;
                    }
                    if (variables.ContainsKey(tStr) || standartConstants.ContainsKey(tStr))
                    {
                        postfix += tStr + ' ';
                    }
                    else
                    {
                        operators.Push(tStr);
                    }
                }
            }
            while(operators.Count > 0)
            {
                postfix += operators.Pop() + ' ';
            }

            postfixNodes = postfix.Split(' ');
        }

        private byte GetPriority(string operatorName)
        {
            switch (operatorName)
            {
                case "|": return 0;
                case "(": return 0;
                case "+": return 2;
                case "-": return 3;
                case "*": return 4;
                case "/": return 4;
                case "^": return 5;
                default: return 6;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Function function)
            {
                return infix == function.infix;
            }
            return false;
        }

        public static Function operator +(Function f1, Function f2)
        {
            Function result = new Function();

            result.infix = $"{f1.infix}+({f2.infix})";
            result.postfix = f1.postfix + f2.postfix + "+ ";
            result.postfixNodes = result.postfix.Split(' ');
            result.variables = f1.variables.Union(f2.variables)
                                           .GroupBy(g => g.Key)
                                           .ToDictionary(x => x.Key, x => x.First().Value);

            return result;
        }

        public static Function operator -(Function f1, Function f2)
        {
            Function result = new Function();

            result.infix = $"{f1.infix}-({f2.infix})";
            result.postfix = f1.postfix + f2.postfix + "- ";
            result.postfixNodes = result.postfix.Split(' ');
            result.variables = f1.variables.Union(f2.variables)
                                           .GroupBy(g => g.Key)
                                           .ToDictionary(x => x.Key, x => x.First().Value);

            return result;
        }
        public static Function operator -(Function f)
        {
            Function result = new Function();

            result.infix = $"-({f.infix})";
            result.postfix = f.postfix + "-1 * ";
            result.postfixNodes = result.postfix.Split(' ');
            result.variables = f.Variables.ToDictionary(x => x.Key, x => x.Value);

            return result;
        }

        public static Function operator *(Function f1, Function f2)
        {
            Function result = new Function();

            result.infix = $"({f1.infix})*({f2.infix})";
            result.postfix = f1.postfix + f2.postfix + "* ";
            result.postfixNodes = result.postfix.Split(' ');
            result.variables = f1.variables.Union(f2.variables)
                                           .GroupBy(g => g.Key)
                                           .ToDictionary(x => x.Key, x => x.First().Value);
            return result;
        }
        public static Function operator *(Function f, double value)
        {
            Function result = new Function();

            if (value < 0)
            {
                result.infix = $"({f.infix})*({value})";
                result.postfix = f.postfix + $"{value} * ";
                result.variables = f.Variables.ToDictionary(x => x.Key, x => x.Value);
            }
            else if(value > 0)
            {
                result.infix = $"({f.infix})*{value}";
                result.postfix = f.postfix + $"{value} * ";
                result.variables = f.Variables.ToDictionary(x => x.Key, x => x.Value);
            }
            else
            {
                result.infix = "0";
                result.postfix = "0 ";
                result.variables = new Dictionary<string, double>();
            }
            result.postfixNodes = result.postfix.Split(' ');

            return result;
        }
        public static Function operator *(double value, Function f)
        {
            Function result = new Function();

            if(value == 0)
            {
                result.infix = "0";
                result.postfix = "0 ";
                result.variables = new Dictionary<string, double>();
            }
            else
            {
                result.infix = $"{value}*({f.infix})";
                result.postfix = f.postfix + $"{value} * ";
                result.variables = f.Variables.ToDictionary(x => x.Key, x => x.Value);
            }
            result.postfixNodes = result.postfix.Split(' ');

            return result;
        }

        public static Function operator /(Function f1, Function f2)
        {
            Function result = new Function();

            result.infix = $"({f1.infix})/({f2.infix})";
            result.postfix = f1.postfix + f2.postfix + "/ ";
            result.postfixNodes = result.postfix.Split(' ');
            result.variables = f1.variables.Union(f2.variables)
                                           .GroupBy(g => g.Key)
                                           .ToDictionary(x => x.Key, x => x.First().Value);

            return result;
        }
    }
}
