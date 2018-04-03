namespace Calculator.MemoryOperation
{
	class SubtractMemory : IMemoryOperator
	{
		public double Calculate(double value, double memory)
		{
			memory -= value;

			return memory;
		}
	}
}
