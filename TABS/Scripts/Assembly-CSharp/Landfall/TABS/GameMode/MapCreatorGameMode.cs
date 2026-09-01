namespace Landfall.TABS.GameMode
{
	public class MapCreatorGameMode : BaseGameMode
	{
		public override void Start()
		{
			base.Start();
			base.Brush = null;
			base.BattleBudget = null;
		}
	}
}
