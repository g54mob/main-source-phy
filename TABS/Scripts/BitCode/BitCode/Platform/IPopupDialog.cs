using System;

namespace BitCode.Platform
{
	public interface IPopupDialog : IPlatformService
	{
		bool IsShowing { get; }

		void Show(string title, string prompt, Action onClosed);
	}
}
