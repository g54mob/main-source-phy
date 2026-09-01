using System.Collections;
using Landfall.TABS_Input;
using TMPro;
using UIStateManager;
using UnityEngine;
using UnityEngine.Events;

public class IntroSequence : MonoBehaviour
{
	public static CampaignInfo justBeatCampaignInfo;

	public MainMenuStateHandler m_menuStateHandler;

	public float introTime;

	public float landfallTime;

	public float tabsTime;

	public CodeAnimation backGroundAnim;

	public CodeAnimation menuAnim;

	public GameObject landfall;

	public GameObject tabs;

	public string landfallSound;

	public string tabsSound;

	public string swoshSound;

	public InterfaceStateManager interfaceStateManager;

	public CampaignThanksHandler campaignThanksUI;

	private MainMenuTimeHandler mainMenuTimeHandler;

	private bool done;

	private bool showScreenSize;

	public static bool hasBeenInMenu;

	public TextMeshProUGUI thankTitle;

	public TextMeshProUGUI thankText;

	public UnityEvent OnIntroSequenceFinished;

	private bool backgroundAnimEnded;

	private bool menuAnimEnded;

	public bool AnimationsFinished
	{
		get
		{
			if (backgroundAnimEnded)
			{
				return menuAnimEnded;
			}
			return false;
		}
	}

	private void Awake()
	{
		OnIntroSequenceFinished = new UnityEvent();
		mainMenuTimeHandler = GetComponent<MainMenuTimeHandler>();
		if (backGroundAnim != null)
		{
			backGroundAnim.animations[0]?.endEvent.AddListener(delegate
			{
				backgroundAnimEnded = true;
				CheckBothAnimationsFinishedForFreeze();
			});
		}
		if (menuAnim != null)
		{
			menuAnim.animations[0]?.endEvent.AddListener(delegate
			{
				menuAnimEnded = true;
				CheckBothAnimationsFinishedForFreeze();
			});
		}
	}

	private void CheckBothAnimationsFinishedForFreeze()
	{
		if (AnimationsFinished && !(mainMenuTimeHandler == null) && mainMenuTimeHandler.FreezeRequested)
		{
			mainMenuTimeHandler.Freeze();
		}
	}

	private void Start()
	{
		StartCoroutine(StartIntro());
	}

	private void Update()
	{
		if (Input.anyKeyDown || hasBeenInMenu)
		{
			CancelAnim();
		}
	}

	private void CancelAnim()
	{
		if (!done)
		{
			done = true;
			hasBeenInMenu = true;
			StopAllCoroutines();
			if (backGroundAnim.currentState != CodeAnimationInstance.AnimationUse.Out)
			{
				backGroundAnim.PlayOut();
			}
			StartCoroutine(EnterMenu());
			ServiceLocator.GetService<MusicHandler>().PlayMenuMusic();
		}
	}

	private IEnumerator EnterMenu()
	{
		yield return new WaitForSecondsRealtime(0.5f);
		OnIntroSequenceFinished.Invoke();
		if (!string.IsNullOrEmpty(justBeatCampaignInfo.ThankYouTitle))
		{
			ShowThankScreen();
		}
		else if (menuAnim.currentState != CodeAnimationInstance.AnimationUse.In && m_menuStateHandler != null)
		{
			if (showScreenSize)
			{
				m_menuStateHandler.GoToMenu(MenuState.ScreenSize);
			}
			else
			{
				menuAnim.PlayIn();
			}
			if (PlayerActions.Instance.InputType == InputType.Controller)
			{
				m_menuStateHandler.LastSelectable();
			}
			m_menuStateHandler.AddControllerSwitchEvent();
		}
	}

	private void ShowThankScreen()
	{
		if (interfaceStateManager != null && campaignThanksUI != null)
		{
			campaignThanksUI.SetCampaignInfoText(justBeatCampaignInfo);
			interfaceStateManager.OpenUIComponent(campaignThanksUI);
			justBeatCampaignInfo.ThankYouTitle = "";
		}
	}

	private IEnumerator StartIntro()
	{
		yield return new WaitForSecondsRealtime(introTime);
		landfall.SetActive(value: true);
		ServiceLocator.GetService<SoundPlayer>().PlaySoundEffect(landfallSound, 1f, base.transform.position);
		yield return new WaitForSecondsRealtime(landfallTime);
		landfall.SetActive(value: false);
		tabs.SetActive(value: true);
		ServiceLocator.GetService<SoundPlayer>().PlaySoundEffect(tabsSound, 1f, base.transform.position);
		yield return new WaitForSecondsRealtime(tabsTime);
		CancelAnim();
		ServiceLocator.GetService<SoundPlayer>().PlaySoundEffect(swoshSound, 1f, base.transform.position);
	}
}
