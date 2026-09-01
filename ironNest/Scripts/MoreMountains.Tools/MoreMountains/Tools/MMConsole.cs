using UnityEngine;

namespace MoreMountains.Tools
{
	public class MMConsole : MonoBehaviour
	{
		protected string _messageStack;

		protected int _numberOfMessages;

		protected bool _messageStackHasBeenDisplayed;

		protected int _largestMessageLength;

		protected int _marginTop;

		protected int _marginLeft;

		protected int _padding;

		protected int _fontSize;

		protected int _characterHeight;

		protected int _characterWidth;

		protected virtual void OnGUI()
		{
		}

		public virtual void SetFontSize(int fontSize)
		{
		}

		public virtual void SetScreenOffset(int top = 10, int left = 10)
		{
		}

		public virtual void SetMessage(string newMessage)
		{
		}

		public virtual void AddMessage(string newMessage)
		{
		}
	}
}
