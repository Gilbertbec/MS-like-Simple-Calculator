namespace Calculator.MemoryOperation
{
	using System.Collections.Generic;

	class MemoryOperatorContext
	{
		private Dictionary<string, IMemoryOperator> dictionary;

		IMemoryOperator @operator;

		public MemoryOperatorContext(string operaotrStr)
		{
			dictionary = new Dictionary<string, IMemoryOperator>();
			dictionary.Add("MS", new StoreMemory());
			dictionary.Add("M+", new AddMemory());
			dictionary.Add("M-", new SubtractMemory());
			dictionary.Add("MR", new RecallMemory());
			dictionary.Add("MC", new ClearMemory());

			if (dictionary.TryGetValue(operaotrStr, out IMemoryOperator tempOperator))
			{
				@operator = tempOperator;
			}
		}

		public double Calculate(double value, double memory)
		{
			double result = 0;
			if (@operator != null)
			{
				result = @operator.Calculate(value, memory);
			}

			return result;
		}
	}
}
