using System;

namespace TFBGames
{
	public interface ISystemKeyboard
	{
		event Action<string> InputStarted;

		event Action<string> InputCompleted;

		event Action<string> InputCancelled;

		event Action<string> InputError;

		void Show(KeyboardType keyboardType, string defaultText, string title, string description, int maxLength);
	}
}
