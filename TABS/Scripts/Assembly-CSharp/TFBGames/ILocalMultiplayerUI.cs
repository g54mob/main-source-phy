using Landfall.TABS;
using Landfall.TABS.GameState;

namespace TFBGames
{
	public interface ILocalMultiplayerUI
	{
		void SetCurrentTeam(Team team);

		void SetPlayerProfile(Player player, PlayerProfile profile);

		void SetPlayerStatus(Player player, LocalMultiplayerPlayerStatus status);

		void UpdateGameState(GameState state);

		void UpdateTime(float time);
	}
}
