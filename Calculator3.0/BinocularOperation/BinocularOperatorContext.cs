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
