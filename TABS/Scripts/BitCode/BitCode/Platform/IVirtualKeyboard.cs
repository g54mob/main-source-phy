namespace BitCode.Platform
{
	public interface IVirtualKeyboard : IPlatformService
	{
		bool IsShowing { get; }

		void Show(string prompt, string initialText, uint maxTextLength, KeyboardClosedEventHandler onClosed);
	}
}
