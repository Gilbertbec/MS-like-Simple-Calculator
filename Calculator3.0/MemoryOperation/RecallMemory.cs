using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.MemoryOperation
{
	class RecallMemory : IMemoryOperator
	{
		public double Calculate(double value, double memory)
		{
			return memory;
		}
	}
}
