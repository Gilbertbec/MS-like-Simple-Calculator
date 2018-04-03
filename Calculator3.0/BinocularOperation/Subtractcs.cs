namespace Calculator.BinocularOperation
{
	public class Subtractcs : IBinocularOperator
	{
		public double Calculate(double val1 = 0, double val2 = 0)
		{
			double firstNumber = val1;
			double secondNumber = val2;
			double result = 0;

			result = firstNumber - secondNumber;

			return result;
		}
	}
}
