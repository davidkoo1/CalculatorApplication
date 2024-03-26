using System.Text.RegularExpressions;

namespace Contracts
{
    public class Calculator
    {
        private Dictionary<char, int> precedence = new Dictionary<char, int>()
        {
            {'+', 1},
            {'-', 1},
            {'*', 2},
            {'/', 2}
        };

        private async Task<string> SetExpressionAsync(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
                throw new InvalidOperationException("Expression is not set.");
            if (expression.Count(_ => _.Equals('(')) != expression.Count(_ => _.Equals(')')))
                throw new Exception("Use ( or ) correct");

            string[] patternsToReplace = { " ", @"\(-" };
            string[] replacements = { "", "(0-" };

            for (int i = 0; i < patternsToReplace.Length; i++)
            {
                expression = Regex.Replace(expression, patternsToReplace[i], replacements[i]);
            }

            if (expression.StartsWith("-"))
            {
                expression = "0" + expression;
            }

            return await Task.FromResult(expression);
        }

        private bool IsOperator(char ch)
        {
            return precedence.ContainsKey(ch);
        }

        private int ComparePrecedence(char op1, char op2)
        {
            return precedence[op1] - precedence[op2];
        }

        private double ApplyOp(char op, double b, double a)
        {
            switch (op)
            {
                case '+': return a + b;
                case '-': return a - b;
                case '*': return a * b;
                case '/':
                    if (b == 0) throw new DivideByZeroException("Cannot divide by zero.");
                    return a / b;
                default: throw new ArgumentException("Invalid operator");
            }
        }

        public async Task<double> Evaluate(string expression)
        {
            expression = await SetExpressionAsync(expression);

            Stack<double> values = new Stack<double>();
            Stack<char> ops = new Stack<char>();
            bool lastWasOperator = true;

            for (int i = 0; i < expression.Length; i++)
            {
                char ch = expression[i];

                if (ch == '(')
                {
                    ops.Push(ch);
                    lastWasOperator = true;
                }
                else if (char.IsDigit(ch) || (lastWasOperator && ch == '-'))
                {
                    string num = ch.ToString();
                    while (i + 1 < expression.Length && (char.IsDigit(expression[i + 1]) || expression[i + 1] == '.'))
                    {
                        num += expression[++i];
                    }
                    values.Push(double.Parse(num));
                    lastWasOperator = false;
                }
                else if (ch == ')')
                {
                    while (ops.Count > 0 && ops.Peek() != '(')
                    {
                        values.Push(ApplyOp(ops.Pop(), values.Pop(), values.Pop()));
                    }
                    ops.Pop();
                    lastWasOperator = false;
                }
                else if (IsOperator(ch))
                {
                    while (ops.Count > 0 && IsOperator(ops.Peek()) && ComparePrecedence(ops.Peek(), ch) >= 0)
                    {
                        values.Push(ApplyOp(ops.Pop(), values.Pop(), values.Pop()));
                    }
                    ops.Push(ch);
                    lastWasOperator = true;
                }
                else
                {
                    throw new ArgumentException("Invalid character in expression");
                }
            }

            while (ops.Count > 0)
            {
                values.Push(ApplyOp(ops.Pop(), values.Pop(), values.Pop()));
            }
            if(ops.Count == 0 && values.Count > 1)
            {
                throw new ArgumentException("Invalid character in expression");
            }
            return values.Pop();
        }
    }
}
