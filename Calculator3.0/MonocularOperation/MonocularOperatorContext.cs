namespace Calculator.MonocularOperation
{
	using System.Collections.Generic;

	class MonocularOperatorContext
	{
		private Dictionary<string, IMonocularOperator> dictionary;

		IMonocularOperator @operator;

		public MonocularOperatorContext(string operaotrStr)
		{
			dictionary = new Dictionary<string, IMonocularOperator>();
			dictionary.Add("±", new Negate());
			dictionary.Add("√", new SquareRoot());
			dictionary.Add("%", new Percent());
			dictionary.Add("1/x", new Fraction());

			if (dictionary.TryGetValue(operaotrStr, out IMonocularOperator tempOperator))
			{
				@operator = tempOperator;
			}
		}

		public double Calculate(double value)
		{
			double result = 0;
			if (@operator != null)
			{
				result = @operator.Calculate(value);
			}

			return result;
		}
	}
}
