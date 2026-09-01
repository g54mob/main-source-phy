using Landfall.TABS.GameMode;
using UnityEngine;

public class EnableOnSandbox : MonoBehaviour
{
	public GameObject ObjectToEnable;

	private void Start()
	{
		if (ServiceLocator.GetService<GameModeService>().CurrentGameMode.GetType() == typeof(SandboxGameMode))
		{
			ObjectToEnable.SetActive(value: true);
		}
	}
}
