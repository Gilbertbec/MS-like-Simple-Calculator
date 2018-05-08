/// Solution of Project 1 of C# Class in LasalleCollege Montreal  2018-02-21
/// Using simple Factory + Strategy Mode with C# Create a MS-like simple Calculator
/// Comparison of function mode, switch case mode and simple factory + strategy mode
/// Gilbert Zhang 
/// ca.qc.gilbert@gmail.com

namespace Calculator.BinocularOperation
{
    static class BinocularOperatorContextByFunction
    {
        static public double GetResult(string operaotrStr, double val1, double val2)
        {
            switch (operaotrStr)
            {
                case "+":
                    return val1 + val2;
                case "-":
                    return val1 - val2;
                case "*":
                    return val1 * val2;
                case "/":
                    return val1 / val2;
            }
            return 0d;
        }
    }
}
