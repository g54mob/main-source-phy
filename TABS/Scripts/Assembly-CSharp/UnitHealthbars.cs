using Landfall.TABC;
using Landfall.TABS;
using Landfall.TABS.GameMode;
using UnityEngine;

[CreateAssetMenu(menuName = "Services/UnitHealthBars")]
public class UnitHealthbars : ServiceAsset
{
	[SerializeField]
	private GameObject healthBar;

	private SettingsInstance m_healthBarOption;

	private GlobalSettingsHandler settingsHandler;

	public override void OnStart()
	{
		GetHealthBarSettings();
		base.OnStart();
	}

	private void GetHealthBarSettings()
	{
		settingsHandler = ServiceLocator.GetService<GlobalSettingsHandler>();
		if (settingsHandler != null)
		{
			m_healthBarOption = settingsHandler.GetSettingsInstance("GAMEPLAY_HEALTHBARS");
		}
	}

	public void HandleUnitSpawned(Unit unit)
	{
		BaseGameMode currentGameMode = ServiceLocator.GetService<GameModeService>().CurrentGameMode;
		if (!(currentGameMode is MainMenuGameMode) && !(currentGameMode is UnitCreatorGameMode))
		{
			if (m_healthBarOption == null)
			{
				GetHealthBarSettings();
			}
			if (m_healthBarOption != null && m_healthBarOption.currentValue == 1)
			{
				Object.Instantiate(healthBar).GetComponent<TABCUnitUI>().Init(unit);
			}
		}
	}

	private bool IsSandboxGamemode()
	{
		GameModeService service = ServiceLocator.GetService<GameModeService>();
		if (!service)
		{
			return false;
		}
		if (service.CurrentGameMode.GetType() != typeof(SandboxGameMode))
		{
			return false;
		}
		return true;
	}
}
