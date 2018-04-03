/// Solution of Project 1 of C# Class in LasalleCollege Montreal  2018-02-21
/// Using simple Factory + Strategy Mode with C# Create a MS-like simple Calculator
/// Gilbert Zhang xixi2010@gamil.com
namespace Calculator
{
	public class NumberInputtingBuffer
	{
		private string _numberCache = string.Empty;

		public string GetValue
		{
			get { return _numberCache; }
		}

		public string SetValue
		{
			set { _numberCache = value; }
		}

		public string Append(string str)
		{
			if (_numberCache == string.Empty)
			{
				if (str == ".")
				{
					str = "0.";
					_numberCache = str;
				}
				else
				{
					_numberCache = str;
				}
			}
			else if (_numberCache != null && _numberCache.Length < 17)
			{
				_numberCache += str;
			}


			if (double.TryParse(_numberCache, out double tempDouble) && !_numberCache.Contains("."))
			{
				_numberCache = tempDouble.ToString();
			}

			return _numberCache;
		}

		public double ToDouble()
		{
			double result;

			if (double.TryParse(_numberCache, out result))
			{
				return result;
			}
			else
			{
				return 0d;
			}
		}

		public void Clear()
		{
			_numberCache = string.Empty;
		}
	}
}
