using Landfall.TABS_Input;
using UnityEngine;

public class SwitchTitleScreen : MonoBehaviour
{
	private PlayerActions playerActions;

	[SerializeField]
	private TABSBooter tabsBooter;

	private void Awake()
	{
		playerActions = PlayerActions.Instance;
	}

	private void Update()
	{
		if (playerActions != null && tabsBooter != null && playerActions.m_acceptSwitchLeftBumper.IsPressed && playerActions.m_acceptSwitchRightBumper.IsPressed)
		{
			tabsBooter.Init();
		}
	}
}
