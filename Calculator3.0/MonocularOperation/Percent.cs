namespace Calculator.MonocularOperation
{
	class Percent : IMonocularOperator
	{
		public double Calculate(double value)
		{
			return value / 100;
		}
	}
}
