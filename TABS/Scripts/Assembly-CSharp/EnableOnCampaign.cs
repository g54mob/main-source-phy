using Landfall.TABS.GameMode;
using UnityEngine;

public class EnableOnCampaign : MonoBehaviour
{
	public GameObject ObjectToEnable;

	private void Start()
	{
		if (ServiceLocator.GetService<GameModeService>().CurrentGameMode.GetType() == typeof(CampaignGameMode))
		{
			ObjectToEnable.SetActive(value: true);
		}
	}
}
