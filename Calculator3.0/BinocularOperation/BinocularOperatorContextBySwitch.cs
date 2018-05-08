/// Solution of Project 1 of C# Class in LasalleCollege Montreal  2018-02-21
/// Using simple Factory + Strategy Mode with C# Create a MS-like simple Calculator
/// Comparison of function mode, switch case mode and simple factory + strategy mode
/// Gilbert Zhang 
/// ca.qc.gilbert@gmail.com

namespace Calculator.BinocularOperation
{
    class BinocularOperatorContextBySwitch
    {
        IBinocularOperator @operator;

        public BinocularOperatorContextBySwitch(string operaotrStr)
        {
            switch (operaotrStr)
            {
                case "+":
                    @operator = new Add();
                    break;
                case "-":
                    @operator = new Subtractcs();
                    break;
                case "*":
                    @operator = new Multiply();
                    break;
                case "/":
                    @operator = new Divide();
                    break;
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
