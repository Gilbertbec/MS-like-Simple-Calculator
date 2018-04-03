namespace Calculator.MemoryOperation
{
	class AddMemory : IMemoryOperator
	{
		public double Calculate(double value, double memory)
		{
			memory += value;

			return memory;
		}
	}
}
