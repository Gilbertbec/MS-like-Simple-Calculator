namespace Calculator.MonocularOperation
{
	class Fraction : IMonocularOperator
	{
		public double Calculate(double value)
		{
			return 1 / value;
		}
	}
}
