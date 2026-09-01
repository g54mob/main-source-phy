using System;

namespace Ookii.Dialogs
{
	public class HyperlinkClickedEventArgs : EventArgs
	{
		private string _href;

		public string Href
		{
			get
			{
				return _href;
			}
		}

		public HyperlinkClickedEventArgs(string href)
		{
			_href = href;
		}
	}
}
