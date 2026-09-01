namespace Landfall.TABS
{
	public interface IPlacementUI
	{
		bool SelectUnit(DatabaseID id, UnitButton btn);

		bool SelectFaction(DatabaseID id, FactionButton btn);

		bool WasLastSelectedFactionIndex(DatabaseID id);
	}
}
