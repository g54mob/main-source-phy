using Landfall.TABS_Input;
using UnityEngine.UI;

namespace TFBGames
{
	public interface IUISelectionItem
	{
		Selectable Selectable { get; }

		void HandleInput(PlayerActions actions);
	}
}
