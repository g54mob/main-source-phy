using Landfall.TABS;
using Landfall.TABS_Input;
using TFBGames;
using UnityEngine;

public class MainMenuUIHandler : MonoBehaviour
{
	public MainMenuState[] menuStates;

	public PostLerper post;

	public FadeLerper fade;

	[Space]
	public CodeAnimation mainAnim;

	[Space]
	public CodeAnimation campaignSelectorAnim;

	[Space]
	public CodeAnimation campaignMenuAnim;

	[Space]
	public CodeAnimation settingsAnim;

	[Space]
	public CodeAnimation levelSelectAnim;

	[Space]
	public CodeAnimation thanksAnim;

	[Space]
	public CodeAnimation roadmapAnim;

	[Space]
	public CodeAnimation workshopAnim;

	[Space]
	public CodeAnimation screenSizeAnim;

	[Space]
	public CodeAnimation multiplayerAnim;

	[Space]
	public CampaignLoaderUI campaignsHandler;

	public UISandboxLevelSelector sandboxHandler;

	public MultiplayerSettingsMenu multiplayerMenu;

	private MainMenuStateHandler stateHandler;

	public MenuState currentMenuState { get; private set; }

	private void Start()
	{
		stateHandler = MainMenuStateHandler.Instance;
		currentMenuState = stateHandler.m_CurrentMenuState;
	}

	private void Update()
	{
		if (currentMenuState != stateHandler.m_CurrentMenuState)
		{
			currentMenuState = stateHandler.m_CurrentMenuState;
			Toggle();
		}
	}

	private void Toggle()
	{
		for (int i = 0; i < menuStates.Length; i++)
		{
			if (menuStates[i].state == currentMenuState)
			{
				ToggleAnim(mainAnim, menuStates[i].mainIsOut);
				ToggleAnim(campaignSelectorAnim, menuStates[i].campaignSelectorIsOut);
				ToggleAnim(campaignMenuAnim, menuStates[i].campaignMenuIsOut);
				ToggleAnim(settingsAnim, menuStates[i].settingsIsOut);
				ToggleAnim(levelSelectAnim, menuStates[i].levelSelectIsOut);
				ToggleAnim(thanksAnim, menuStates[i].thanksIsOut);
				ToggleAnim(roadmapAnim, menuStates[i].roadmapIsOut);
				ToggleAnim(workshopAnim, menuStates[i].workshopIsOut);
				ToggleAnim(screenSizeAnim, menuStates[i].screenSizeIsOut);
				ToggleAnim(multiplayerAnim, menuStates[i].multiplayerIsOut);
				fade.fadeValue = (menuStates[i].isDark ? 1f : 0f);
				post.dofAmount = (menuStates[i].isBlurry ? 1f : 0f);
				menuStates[i].enterMenuEvent.Invoke();
			}
		}
		if (PlayerActions.Instance.InputType == InputType.Controller)
		{
			switch (currentMenuState)
			{
			case MenuState.Main:
				stateHandler.LastSelectable();
				break;
			case MenuState.LevelSelection:
				sandboxHandler.SelectFirstButtonInList();
				break;
			}
		}
	}

	private void ToggleAnim(CodeAnimation anim, bool isOut)
	{
		if (anim != null)
		{
			if (isOut && anim.currentState != CodeAnimationInstance.AnimationUse.In)
			{
				anim.PlayIn();
			}
			if (!isOut && anim.currentState != CodeAnimationInstance.AnimationUse.Out)
			{
				anim.PlayOut();
			}
		}
	}
}
