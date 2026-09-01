using Landfall.TABS.GameMode;
using UnityEngine;

public class DMOnlyEnableInMultiplayer : MonoBehaviour
{
	public enum MPEnableMode
	{
		EnableInMP = 0,
		EnableInSP = 1
	}

	public MPEnableMode enableMode;

	private void Start()
	{
		BaseGameMode currentGameMode = ServiceLocator.GetService<GameModeService>().CurrentGameMode;
		bool flag = false;
		flag = currentGameMode is OnlineMultiplayerGameMode || currentGameMode is LocalMultiplayerGameMode;
		switch (enableMode)
		{
		case MPEnableMode.EnableInMP:
			base.gameObject.SetActive(flag);
			break;
		case MPEnableMode.EnableInSP:
			base.gameObject.SetActive(!flag);
			break;
		}
	}
}
