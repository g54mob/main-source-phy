namespace Landfall.TABS
{
	public interface IUILayoutElement
	{
		void SetupFaction(FactionButton.FactionButtonData factionButtonData, ExpandedFactionUI expandedFactionUI);

		void SetupUnit(UnitButton.UnitButtonData unitButtonData);
	}
}
