using Landfall.TABS_Input;

namespace Landfall.TABS.Workshop
{
	public interface IBattleCreatorMenu
	{
		bool AllowPageChange { get; }

		void Open(BattleCreatorState state, object data);

		void Close();

		bool IsOpen();

		void Init(BattleCreatorTabsUIHandler tabsHandler);

		void Init(CustomContentOverlaysManager overlay);

		bool NavigateUIWithController(PlayerActions playerActions);
	}
}
