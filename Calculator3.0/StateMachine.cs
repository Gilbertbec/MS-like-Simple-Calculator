/// Solution of Project 1 of C# Class in LasalleCollege Montreal  2018-02-21
/// Using simple Factory + Strategy Mode with C# Create a MS-like simple Calculator
/// Comparison of function mode, switch case mode and simple factory + strategy mode
/// Gilbert Zhang 
/// ca.qc.gilbert@gmail.com

namespace Calculator
{
    using global::Calculator.BinocularOperation;
    using global::Calculator.MemoryOperation;
    using global::Calculator.MonocularOperation;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    class StateMachine
    {
        /// <summary>
		/// Defination of global variables
		/// </summary>
        double _Memory = 0;

        bool _StateOfNumberInputting = false;

        bool _StateOfNumberInputted = false;

        bool _StateOperatorInputted = false;

        bool _StateOfCalculated = false;

        string _TxtOfLastButton = string.Empty;

        double _LastNumber = 0d;

        string _BtnText;

        NumberInputtingBuffer _numberInputtingBuffer = new NumberInputtingBuffer();

        StringBuilder _inputtingEcho = new StringBuilder();

        StringBuilder _inputtedRecorder = new StringBuilder();

        Stack<double> _numberStack = new Stack<double>();

        Stack<string> _operatorSatck = new Stack<string>();

        Stack<double> _resultStack = new Stack<double>();

        string[] numbersArray = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "." };

        string[] binocularOperatorArray = new string[] { "+", "-", "*", "/" };

        string[] monocularOperatorArray = new string[] { "±", "√", "%", "1/x" };

        /// <summary>
		/// Defination of public global variables
		/// </summary>
        public string BtnText
        {
            set { _BtnText = value; }
        }

        public double Memory
        {
            get { return _Memory; }
        }

        public string InputtedRecorder
        {
            get { return _inputtedRecorder.ToString(); }
        }

        public bool StateOfCalculated
        {
            get { return _StateOfCalculated; }
        }

        public bool StateOperatorInputted
        {
            get { return _StateOperatorInputted; }
        }

        public string[] BinocularOperatorArray
        {
            get { return binocularOperatorArray; }
        }

        public string[] NumbersArray
        {
            get { return numbersArray; }
        }

        public StringBuilder InputtingEcho
        {
            get { return _inputtingEcho; }
            set { _inputtingEcho = value; }
        }

        public string TxtOfLastButton
        {
            get { return _TxtOfLastButton; }
            set { _TxtOfLastButton = value; }
        }

        public Stack<double> NumberStack
        {
            get { return _numberStack; }
            set { _numberStack = value; }
        }

        public NumberInputtingBuffer NumberInputtingBuffer
        {
            get { return _numberInputtingBuffer; }
            set { _numberInputtingBuffer = value; }
        }

        public string SetInputBuffer()
        {
            if (_StateOfCalculated && _TxtOfLastButton == "=")
            {
                ResetState();
            }

            if (_TxtOfLastButton == "=")
            {
                ResetState();
            }

            if (_StateOperatorInputted)
            {
                _StateOperatorInputted = false;
                _numberInputtingBuffer = new NumberInputtingBuffer();
            }

            _StateOfNumberInputting = true;


            if (_BtnText == "." && _numberInputtingBuffer.GetValue != null && _numberInputtingBuffer.GetValue.Contains("."))
            {
                return _numberInputtingBuffer.GetValue;
            }

            string inputEcho = _numberInputtingBuffer.Append(_BtnText);
            _inputtingEcho.Clear().Append(inputEcho);

            _TxtOfLastButton = _BtnText;

            return inputEcho;
        }

        public void SaveNumberInputted()
        {
            double inputedNumber = _numberInputtingBuffer.ToDouble();
            _numberStack.Push(inputedNumber);

            _inputtedRecorder.Append(inputedNumber);

            _inputtingEcho.Clear();
            _numberInputtingBuffer.Clear();

            _StateOfNumberInputted = true;
            _StateOfNumberInputting = false;
        }

        public string ExcuteBinocularOperation()
        {
            if (_StateOfCalculated)
            {
                _numberStack.Clear();

                if (_resultStack.Count > 0)
                {
                    double resultAsFirstNumber = _resultStack.Peek();
                    _numberStack.Push(resultAsFirstNumber);

                    if (_TxtOfLastButton == "=")
                    {
                        _inputtedRecorder.Append(resultAsFirstNumber);
                    }
                }
                _StateOfCalculated = false;
            }

            if (_StateOfNumberInputting)
            {
                SaveNumberInputted();
            }

            if (_numberStack.Count == 0)
            {
                _numberStack.Push(0d);
                _inputtedRecorder.Append("0");
            }


            string operatorStr = _BtnText;
            string operatr = string.Empty;
            //When Monocular operator been continuous clicked
            if (_operatorSatck.Count > 0 && monocularOperatorArray.Contains(_TxtOfLastButton))
            {
                operatr = _operatorSatck.Peek();
                if (operatr == _TxtOfLastButton)
                {
                    _inputtedRecorder.Remove(_inputtedRecorder.Length - _TxtOfLastButton.Length, _TxtOfLastButton.Length);
                }
            }

            //when Binocular operator been continuous clicked
            if (_operatorSatck.Count > 0 && binocularOperatorArray.Contains(_TxtOfLastButton))
            {
                operatr = _operatorSatck.Peek();
                if (operatr == _TxtOfLastButton)
                {
                    _inputtedRecorder.Remove(_inputtedRecorder.Length - _TxtOfLastButton.Length, _TxtOfLastButton.Length);
                }
            }

            _StateOperatorInputted = true;

            _TxtOfLastButton = _BtnText;
            string inputEcho = _BtnText;

            if (_numberStack.Count == 2)
            {
                inputEcho = GetResultForBinocularOperation().ToString();
            }

            _inputtedRecorder.Append(operatorStr);
            string sss = _inputtedRecorder.ToString();

            _operatorSatck.Push(operatorStr);

            return inputEcho;
        }

        public string ExcuteMonocularOperation(string strCurrentNumber)
        {
            if (_TxtOfLastButton == "=")
            {
                _inputtedRecorder.Clear();
            }

            double currentNumber = 0d;

            if (double.TryParse(strCurrentNumber, out double tempCurrent))
            {
                currentNumber = tempCurrent;
            }

            MonocularOperatorContext monocularOperatorContext = new MonocularOperatorContext(_BtnText);
            double result = monocularOperatorContext.Calculate(Convert.ToDouble(currentNumber));

            NumberInputtingBuffer.Clear();
            InputtingEcho.Clear();
            NumberInputtingBuffer.Append(result.ToString());
            _StateOfNumberInputting = true;
            InputtingEcho.Append(result);
            return result.ToString();
        }

        public string ExcuteMemoryOperation(string strCurrentNumber)
        {
            //If the user has just completed a round of calculations, reset the state in order to start a new round of calculations
            if (_TxtOfLastButton == "=")
            {
                ResetState();
            }

            string result = string.Empty;
            double currentNumber = 0d;
            if (double.TryParse(strCurrentNumber, out double tempVale))
            {
                currentNumber = tempVale;
            }
            MemoryOperatorContext memoryOperatorContext = new MemoryOperatorContext(_BtnText);
            _Memory = memoryOperatorContext.Calculate(currentNumber, _Memory);

            result = _Memory.ToString();
            //InputtingEcho.Clear();    //Clear input screen cache
            //_inputtedRecorder.ToString();


            switch (_BtnText)
            {
                case "MS":
                case "M+":
                case "M-":
                    NumberInputtingBuffer.Clear();
                    break;
                case "MR":
                    NumberInputtingBuffer.Clear();
                    NumberInputtingBuffer.SetValue = _Memory.ToString();
                    _StateOfNumberInputting = true;
                    break;
                case "MC":
                    NumberInputtingBuffer.Clear();
                    break;
            }
            return result;
        }

        public double GetResult(string btnText)
        {
            if (_StateOfCalculated)
            {
                _numberStack.Clear();

                if (_resultStack.Count > 0)
                {
                    double resultAsFirstNumber = _resultStack.Peek();
                    _numberStack.Push(resultAsFirstNumber);
                }
            }

            if (_StateOfNumberInputting)
            {
                SaveNumberInputted();
            }

            if (_numberStack.Count == 0)
            {
                _numberStack.Push(0d);
                _inputtedRecorder.Append("0");
            }

            if (_numberStack.Count == 1 && _TxtOfLastButton != "=")
            {
                _inputtedRecorder = new StringBuilder();
                _numberStack.Push(_numberStack.Peek());
            }

            if (_numberStack.Count == 1 && _TxtOfLastButton == "=")
            {
                _inputtedRecorder = new StringBuilder();
                _numberStack.Push(_LastNumber);
            }

            _StateOfCalculated = true;

            if (_operatorSatck.Count > 0 && binocularOperatorArray.Contains(_operatorSatck.Peek()) && _numberStack.Count == 2)
            {
                _TxtOfLastButton = btnText;
                double result = GetResultForBinocularOperation();
                _inputtedRecorder.Clear();
                return result;
            }

            _StateOfCalculated = true;
            _TxtOfLastButton = btnText;
            return 0d;
        }

        public void ResetState()
        {
            _StateOfNumberInputting = false;
            _StateOfNumberInputted = false;
            _StateOperatorInputted = false;
            _StateOfCalculated = false;
            _TxtOfLastButton = string.Empty;
            _LastNumber = 0d;
            _numberInputtingBuffer = new NumberInputtingBuffer();
            _inputtingEcho = new StringBuilder();
            _inputtedRecorder = new StringBuilder();
            _numberStack = new Stack<double>();
            _operatorSatck = new Stack<string>();
            _resultStack = new Stack<double>();
        }

        double GetResultForBinocularOperation()
        {
            double secondNumber = _numberStack.Pop();
            double firstNumber = _numberStack.Pop();

            string operaotrStr = _operatorSatck.Peek();
            //way1
            //BinocularOperatorContext operatorContext = new BinocularOperatorContext(operaotrStr);
            //double result = operatorContext.Calculate(firstNumber, secondNumber);

            //way2
            //BinocularOperatorContextBySwitch operatorContext = new BinocularOperatorContextBySwitch(operaotrStr);
            //double result = operatorContext.Calculate(firstNumber, secondNumber);

            //way3
            double result = BinocularOperatorContextByFunction.GetResult(operaotrStr, firstNumber, secondNumber);
            _resultStack.Push(result);

            // To prepare numbers for user click = button continuously
            _StateOfCalculated = true;
            _LastNumber = secondNumber;

            return result;
        }
    }
}
