using Landfall.TABS.GameMode;
using UnityEngine;

public class FireworkBug : MonoBehaviour
{
	private GameModeService gameModeService;

	private bool isRestricedInGameMode;

	private void Awake()
	{
		gameModeService = ServiceLocator.GetService<GameModeService>();
		if (gameModeService != null)
		{
			isRestricedInGameMode = gameModeService.IsGameModeRestricted();
		}
	}

	private void Start()
	{
		if (Bugs._DLC_ACTIVATED && ServiceLocator.GetService<GlobalSettingsHandler>().GetSettingsInstance("BUG_FIREWORKS").currentValue == 1 && !isRestricedInGameMode)
		{
			GetComponent<PushStick>().force *= 20f;
			base.transform.localScale *= 3f;
		}
	}
}
