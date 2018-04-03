namespace Calculator.MonocularOperation
{
	class Negate : IMonocularOperator
	{
		public double Calculate(double val)
		{
			return -val;
		}
	}
}
