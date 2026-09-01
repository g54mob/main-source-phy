using System.ComponentModel;
using System.Windows.Forms;

namespace Ookii.Dialogs
{
	public class OkButtonClickedEventArgs : CancelEventArgs
	{
		private string _input;

		private IWin32Window _inputBoxWindow;

		public string Input
		{
			get
			{
				return _input;
			}
		}

		public IWin32Window InputBoxWindow
		{
			get
			{
				return _inputBoxWindow;
			}
		}

		public OkButtonClickedEventArgs(string input, IWin32Window inputBoxWindow)
		{
			_input = input;
			_inputBoxWindow = inputBoxWindow;
		}
	}
}
