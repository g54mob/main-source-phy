using System;

namespace TFBGames
{
	public interface IMenuWithEvents
	{
		bool IsOpen { get; }

		int EnableWithMenusParameter { get; }

		event Action<IMenuWithEvents> MenuOpened;

		event Action<IMenuWithEvents> MenuClosed;

		event Action<IMenuWithEvents, int> EnableWithMenusParameterChanged;
	}
}
