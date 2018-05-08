/// Solution of Project 1 of C# Class in LasalleCollege Montreal  2018-02-21
/// Using simple Factory + Strategy Mode with C# Create a MS-like simple Calculator
/// Comparison of function mode, switch case mode and simple factory + strategy mode
/// Gilbert Zhang 
/// ca.qc.gilbert@gmail.com

namespace Calculator.BinocularOperation
{
    public class Divide : IBinocularOperator
    {
        public double Calculate(double val1 = 0, double val2 = 0)
        {
            double firstNumber = val1;
            double secondNumber = val2;
            double result = 0;

            result = firstNumber / secondNumber;

            return result;
        }
    }
}
