using System.Collections.Generic;
using BitCode.Networking;
using Landfall.TABS;
using Landfall.TABS_Input;
using LevelCreator;
using TFBGames;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class MainMenuButtons : UIComponentMainMenu
{
	[SerializeField]
	private GameObject m_FirstSelectedButton;

	[SerializeField]
	private Transform m_ButtonsContainer;

	[SerializeField]
	private Button m_UnitCreatorButton;

	[SerializeField]
	private Button m_CustomContentButton;

	[SerializeField]
	private GameObject m_CustomContentNewContentGraphic;

	[SerializeField]
	private Button m_WorkshopButton;

	[SerializeField]
	private Button m_MultiplayerButton;

	[SerializeField]
	private Button m_ProjectMarsButton;

	[SerializeField]
	private IntroSequence m_IntroSequence;

	[SerializeField]
	private PostLerper m_PostEffects;

	[SerializeField]
	private FadeLerper m_FadeEffects;

	private GameObject m_LastSelectedButton;

	private Button[] m_MenuButtons;

	private InputService m_InputService;

	private bool m_AutoSelectButton;

	private ModalPanel m_ModalPanel;

	private IAccountPermissions m_AccountPermissions;

	public static MainMenuButtons Instance { get; private set; }

	public bool AllowAnimation => allowAnimation;

	protected override void Awake()
	{
		base.Awake();
		Instance = this;
		m_InputService = ServiceLocator.GetService<InputService>();
		if (m_FirstSelectedButton != null)
		{
			m_LastSelectedButton = m_FirstSelectedButton;
		}
		if (m_UnitCreatorButton != null)
		{
			m_UnitCreatorButton.gameObject.SetActive(value: true);
			m_UnitCreatorButton.onClick.AddListener(TABSSceneManager.LoadUnitCreator);
			m_UnitCreatorButton.interactable = true;
		}
		if (m_CustomContentButton != null)
		{
			m_CustomContentButton.gameObject.SetActive(value: true);
			m_CustomContentButton.interactable = true;
		}
		if (m_IntroSequence != null)
		{
			m_IntroSequence.OnIntroSequenceFinished.AddListener(IntroSequenceFinished);
		}
		m_MenuButtons = GetComponentsInChildren<Button>();
		Button[] menuButtons = m_MenuButtons;
		for (int i = 0; i < menuButtons.Length; i++)
		{
			menuButtons[i].onClick.AddListener(DoBackgroundBlur);
		}
		allowAnimation = false;
		m_ModalPanel = ServiceLocator.GetService<ModalPanel>();
		if (m_ModalPanel != null)
		{
			m_ModalPanel.OnPopUpClose += SetSelectedButton;
		}
		m_AccountPermissions = ServiceLocator.GetService<IAccountPermissions>();
	}

	protected override void Start()
	{
		base.Start();
		RebuildNavigation();
		ServiceLocator.GetService<CursorVisibilityController>().SetLockStateAndVisibility(CursorLockMode.None, visible: true);
		PostProcessVolume postProcessVolume = Object.FindObjectOfType<PostProcessVolume>();
		if (postProcessVolume != null)
		{
			m_PostEffects = postProcessVolume.gameObject.AddComponent<PostLerper>();
		}
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		if (m_InputService != null)
		{
			m_InputService.InputChanged += OnInputChange;
			OnInputChange(PlayerActions.Instance.InputType);
		}
		if (stateCodeAnimation != null)
		{
			stateCodeAnimation.InPlayed += SetSelectedButton;
		}
	}

	private new void OnDisable()
	{
		if (m_InputService != null)
		{
			m_InputService.InputChanged -= OnInputChange;
		}
		if (stateCodeAnimation != null)
		{
			stateCodeAnimation.InPlayed -= SetSelectedButton;
		}
	}

	public void OpenMultiplayerMenu()
	{
		if (m_ProjectMarsButton != null)
		{
			m_ProjectMarsButton.onClick.Invoke();
		}
	}

	protected override void OnOpen()
	{
		SetButtonsInteractive(interactive: true);
		base.OnOpen();
		if (allowAnimation)
		{
			if (m_PostEffects != null && m_FadeEffects != null)
			{
				m_FadeEffects.fadeValue = 0f;
				m_PostEffects.dofAmount = 0f;
			}
			else
			{
				Debug.LogError("Menu blur or fade reference missing");
			}
		}
		UpdateCustomContentNewGraphic();
	}

	protected override void OnClose()
	{
		SetButtonsInteractive(interactive: false);
		if (EventSystem.current != null)
		{
			m_LastSelectedButton = EventSystem.current.currentSelectedGameObject;
		}
		base.OnClose();
	}

	private void SetButtonsInteractive(bool interactive)
	{
		foreach (Transform item in m_ButtonsContainer)
		{
			Button componentInChildren = item.GetComponentInChildren<Button>();
			if (componentInChildren != null)
			{
				componentInChildren.interactable = interactive;
			}
		}
	}

	protected override void OnReceivedInvitation(IGameInvitation invite)
	{
	}

	private void RebuildNavigation()
	{
		IList<Selectable> selectableChildren = m_ButtonsContainer.GetSelectableChildren();
		if (selectableChildren != null)
		{
			UIHelpers.CreateExplicitLinearNavigation(selectableChildren, horizontal: false);
		}
	}

	private void SetSelectedButton()
	{
		if (!(EventSystem.current == null) && autoHighlightSelectableWhenActive && !m_ModalPanel.IsPopupOpen && !(m_LastSelectedButton == null) && allowAnimation)
		{
			EventSystem.current.SetSelectedGameObject(m_LastSelectedButton);
		}
	}

	private void IntroSequenceFinished()
	{
		allowAnimation = true;
		PlayInAnimation();
	}

	private void PlayInAnimation()
	{
		if (stateCodeAnimation != null)
		{
			stateCodeAnimation.PlayIn();
		}
	}

	private void DoBackgroundBlur()
	{
		if (m_PostEffects != null && m_FadeEffects != null)
		{
			m_FadeEffects.fadeValue = 1f;
			m_PostEffects.dofAmount = 1f;
		}
		else
		{
			Debug.LogError("Menu blur or fade reference missing");
		}
	}

	private void OnInputChange(InputType inputType)
	{
		switch (inputType)
		{
		case InputType.Controller:
			autoHighlightSelectableWhenActive = true;
			m_LastSelectedButton = m_FirstSelectedButton;
			SetSelectedButton();
			break;
		case InputType.Keyboard:
		case InputType.Any:
			autoHighlightSelectableWhenActive = false;
			break;
		}
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		Instance = null;
		if (m_IntroSequence != null)
		{
			m_IntroSequence.OnIntroSequenceFinished.RemoveAllListeners();
		}
		if (m_MenuButtons != null)
		{
			Button[] menuButtons = m_MenuButtons;
			for (int i = 0; i < menuButtons.Length; i++)
			{
				menuButtons[i].onClick.RemoveAllListeners();
			}
		}
		if (m_ModalPanel != null)
		{
			m_ModalPanel.OnPopUpClose -= SetSelectedButton;
		}
	}

	private void UpdateCustomContentNewGraphic()
	{
		if (!(m_CustomContentNewContentGraphic != null))
		{
			return;
		}
		DMNewContentManager.HasNewContent(delegate(bool hasNewContent)
		{
			if (m_CustomContentNewContentGraphic != null)
			{
				m_CustomContentNewContentGraphic.SetActive(hasNewContent);
			}
		});
	}

	public void GoToCredits()
	{
		TABSSceneManager.LoadCreditsScene();
	}

	public void GoToLevelCreator()
	{
		if (!m_AccountPermissions.IsSignedIn)
		{
			m_ModalPanel.PopUp("POPUP_NOT_SIGNED_IN_TO_CREATE");
			return;
		}
		StartMenu.SetBackButtonState(StartMenu.StartMenuBackState.ToMainMenu);
		TABSSceneManager.LoadLevelCreator(DMEditor.StartState.New);
	}
}
