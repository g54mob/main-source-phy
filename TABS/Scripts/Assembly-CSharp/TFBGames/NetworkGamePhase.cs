namespace TFBGames
{
	public enum NetworkGamePhase
	{
		Initializing = 0,
		EnteringPlacement = 1,
		Placement = 2,
		Battle = 3,
		BattleEnded = 4,
		ReadyForPlacement = 5,
		PrematurelyEndingBattle = 6,
		Disconnected = 7,
		RequestBattleEnd = 8
	}
}
