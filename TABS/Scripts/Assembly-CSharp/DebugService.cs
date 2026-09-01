using TFBGames;
using UnityEngine;

public class DebugService : ServicePrefab
{
	private DebugMenu debugMenu;

	public bool HasUnlimitedUnitPoints => debugMenu.HasUnlimitedUnitPoints;

	public bool HasUnlockedProgress => debugMenu.HasUnlockedProgress;

	public bool HasUnlockedAllUnits => debugMenu.HasUnlockedAllUnits;

	public override void OnStart()
	{
		base.OnStart();
		Destroy();
		debugMenu = GetComponentInChildren<DebugMenu>();
	}

	private void OnEnable()
	{
		Destroy();
	}

	private void Destroy()
	{
		Object.Destroy(base.gameObject);
	}
}
