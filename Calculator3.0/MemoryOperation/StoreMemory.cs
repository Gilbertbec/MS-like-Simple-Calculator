namespace Calculator.MemoryOperation
{
    class StoreMemory : IMemoryOperator
    {
        public double Calculate(double value, double memory)
        {
            memory = value;

            return memory;
        }
    }
}
