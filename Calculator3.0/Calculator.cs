/// Solution of Project 1 of C# Class in LasalleCollege Montreal  2018-02-21
/// Using simple Factory + Strategy Mode with C# Create a MS-like simple Calculator
/// Comparison of function mode, switch case mode and simple factory + strategy mode
/// Gilbert Zhang ca.qc.gilbert@gmail.com
namespace Calculator
{
	using System;
	using System.Linq;
	using System.Windows.Forms;

	public partial class Calculator : Form
	{
		StateMachine _StateMachine = new StateMachine();

		public Calculator()
		{
			InitializeComponent();
		}

		private void Calculator_Load(object sender, EventArgs e)
		{
			BindEvent();
		}

		/// <summary>
		/// Bind events to the buttons
		/// </summary>
		private void BindEvent()
		{
			foreach (var item in this.Controls)
			{
				if (item is Button)
				{
					((Button)item).Click += new EventHandler(PressingButton);
				}
			}
		}

		/// <summary>
		/// According to the user input, go to the response process
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PressingButton(object sender, EventArgs e)
		{
			string btnText = ((Button)sender).Text;
			string result = string.Empty;

			string[] numbersArray = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "." };
			string[] binocularOperatorArray = new string[] { "+", "-", "*", "/" };
			string[] monocularOperatorArray = new string[] { "±", "√", "%", "1/x" };
			string[] memoryOperatorArray = new string[] { "MS", "M+", "M-", "MC", "MR" };
			string[] modificationArray = new string[] { "C", "<--", "CE" };

			_StateMachine.BtnText = btnText;
			if (numbersArray.Contains(btnText))
			{
				result = _StateMachine.SetInputBuffer();
			}
			else if (binocularOperatorArray.Contains(btnText))
			{
				result = _StateMachine.ExcuteBinocularOperation();
			}
			else if (monocularOperatorArray.Contains(btnText))
			{
				result = _StateMachine.ExcuteMonocularOperation(txtResult.Text);
			}
			else if (memoryOperatorArray.Contains(btnText))
			{
				result = _StateMachine.ExcuteMemoryOperation(txtResult.Text);
				btnMC.Enabled = btnMR.Enabled = _StateMachine.Memory != 0;
			}
			else if (modificationArray.Contains(btnText))
			{
				ModificationOperation(btnText);
				return;
			}
			else if (btnText == "=")
			{
				result = _StateMachine.GetResult(btnText).ToString();
			}

			txtResult.Text = result;
			txtRecorder.Text = _StateMachine.InputtedRecorder;
			_StateMachine.TxtOfLastButton = btnText;
		}

		/// <summary>
		/// ModificationOperation
		/// </summary>
		/// <param name="text"></param>
		private void ModificationOperation(string text)
		{
			if (text == "C")
			{
				_StateMachine.ResetState();
				txtResult.Text = "0";
				txtRecorder.Clear();
			}
			else if (text == "<--")
			{
				if (_StateMachine.TxtOfLastButton == "=")
				{
					_StateMachine.TxtOfLastButton = "=";
					return;
				}

				Backspace();
			}
			else if (text == "CE")
			{
				ClearError();
			}
		}

		/// <summary>
		/// Backspace
		/// </summary>
		private void Backspace()
		{
			string result = string.Empty;

			if (_StateMachine.StateOfCalculated)
			{
				return;
			}

			_StateMachine.NumberInputtingBuffer.SetValue = txtResult.Text;

			string currentNumberText = txtResult.Text;
			if (currentNumberText.Length > 0)
			{
				currentNumberText = currentNumberText.Substring(0, currentNumberText.Length - 1);
			}

			if (currentNumberText.Length == 0)
			{
				txtResult.Text = "0";
			}
			else
			{
				txtResult.Text = currentNumberText;
			}

			_StateMachine.NumberInputtingBuffer.SetValue = currentNumberText;
			_StateMachine.InputtingEcho.Clear().Append(currentNumberText);
		}

		/// <summary>
		/// ClearError
		/// </summary>
		private void ClearError()
		{
			_StateMachine.NumberInputtingBuffer.Clear();
			txtResult.Text = "0";
			_StateMachine.InputtingEcho.Clear();
		}

		/// <summary>
		/// Prevent keyboard input
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtResult_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = true;
		}
	}
}
