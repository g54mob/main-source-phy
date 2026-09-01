using Landfall.TABC;
using Landfall.TABS;
using TwitchUnitInfo;
using UnityEngine;

[CreateAssetMenu(menuName = "Services/TABSTwitchHandler", fileName = "TABSTwitchHandler")]
public class TABSTwitchHandler : ServiceAsset
{
	[SerializeField]
	public TwitchUnitHandler UnitHandler;

	private bool waitForTwitchHandlerToBeSetup = true;

	public void Disconnect()
	{
		UnitHandler.NameHandler.Clear();
	}

	public void HandleNewUnit(Unit unit, TABCUnitUI unitUI)
	{
		UnitHandler.NewUnit(unit, unitUI);
	}
}
