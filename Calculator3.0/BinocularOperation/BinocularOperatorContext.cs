/// Solution of Project 1 of C# Class in LasalleCollege Montreal  2018-02-21
/// Using simple Factory + Strategy Mode with C# Create a MS-like simple Calculator
/// Comparison of function mode, switch case mode and simple factory + strategy mode
/// Gilbert Zhang 
/// ca.qc.gilbert@gmail.com

namespace Calculator.BinocularOperation
{
    using System.Collections.Generic;

    class BinocularOperatorContext
    {
        private Dictionary<string, IBinocularOperator> dictionary;

        IBinocularOperator @operator;

        public BinocularOperatorContext(string operaotrStr)
        {
            dictionary = new Dictionary<string, IBinocularOperator>();
            dictionary.Add("+", new Add());
            dictionary.Add("-", new Subtractcs());
            dictionary.Add("*", new Multiply());
            dictionary.Add("/", new Divide());

            if (dictionary.TryGetValue(operaotrStr, out IBinocularOperator tempOperator))
            {
                @operator = tempOperator;
            }
        }

        public double Calculate(double firstNumber, double secondNumber)
        {
            double result = 0;
            if (@operator != null)
            {
                result = @operator.Calculate(firstNumber, secondNumber);
            }

            return result;
        }
    }
}
