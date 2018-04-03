namespace Calculator.MonocularOperation
{
	using System;

	class SquareRoot : IMonocularOperator
	{
		public double Calculate(double value)
		{
			return Math.Sqrt(value);
		}
	}
}
