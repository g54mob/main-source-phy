using System;
using Landfall.TABS.TeamEdge;
using Landfall.TABS.WinConditions;
using Landfall.TABS_Input;
using TFBGames;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Landfall.TABS
{
	public class MapSettingsUI : MonoBehaviour
	{
		public TMP_Dropdown dropdown;

		public TextMeshProUGUI helpText;

		public GameObject SettingsIcon;

		public GameObject LevelNameText;

		public UIMovementAnimation buttonAnimation;

		public SimpleButton button;

		public GameObject dPadGlyph;

		private bool isLevelName;

		private PlayerActions m_playerActions;

		private WinConditionPresenter m_winConditionPresenter;

		[SerializeField]
		private Selectable m_firstSelectable;

		private InputService m_inputService;

		private GlyphService m_iconService;

		private bool isEditing;

		private CanvasToggle[] canvasToggles;

		public bool IsEditing
		{
			get
			{
				return isEditing;
			}
			private set
			{
				isEditing = value;
			}
		}

		public static bool IsOpen { get; private set; }

		public event Action MapSettingsOpened;

		public event Action MapSettingsClosed;

		private void Awake()
		{
			canvasToggles = GetComponentsInChildren<CanvasToggle>();
		}

		private void Start()
		{
			m_inputService = ServiceLocator.GetService<InputService>();
			m_iconService = ServiceLocator.GetService<GlyphService>();
			m_inputService.InputChanged += OnInputChanged;
			buttonAnimation.OnCompleteState01 += Close;
			buttonAnimation.OnCompleteState02 += Open;
			m_playerActions = PlayerActions.Instance;
			m_winConditionPresenter = UnityEngine.Object.FindObjectOfType<WinConditionPresenter>();
			buttonAnimation.OnCompleteState01 += m_winConditionPresenter.ShowConditions;
			buttonAnimation.OnCompleteState02 += m_winConditionPresenter.HideConditions;
			if (CampaignPlayerDataHolder.CurrentGameModeState == GameModeState.Campaign)
			{
				isLevelName = true;
				SettingsIcon.SetActive(value: false);
				if (dPadGlyph != null)
				{
					dPadGlyph.SetActive(value: false);
				}
				LevelNameText.SetActive(value: true);
				button.m_selectedScale = 1f;
			}
			if (InstancedHandler<EscapeMenuHandler>.Instance != null)
			{
				InstancedHandler<EscapeMenuHandler>.Instance.EscapeMenuClosed += OnEscapeMenuClosed;
			}
		}

		private void OnEscapeMenuClosed()
		{
			if (IsOpen)
			{
				if (dropdown.IsExpanded)
				{
					dropdown.Hide();
				}
				m_firstSelectable.Select();
			}
		}

		public void ToggleButton()
		{
			if (!isLevelName)
			{
				if (!IsOpen)
				{
					SetToggleCanvas(state: true);
				}
				buttonAnimation.ToggleState();
			}
		}

		public void EditTeamLine()
		{
			UnityEngine.Object.FindObjectOfType<SceneSettings>().StartLineEditing();
			helpText.gameObject.SetActive(value: true);
			helpText.text = GetHelpText();
			isEditing = true;
		}

		private string GetHelpText()
		{
			SceneSettings sceneSettings = UnityEngine.Object.FindObjectOfType<SceneSettings>();
			string text = "";
			string text2 = "";
			string actionGlyph = m_iconService.GetActionGlyph(PlayerActions.Instance.m_placementZoomOut, InputType.Controller, PlayerActions.Instance.LastDeviceStyle);
			string actionGlyph2 = m_iconService.GetActionGlyph(PlayerActions.Instance.m_placementZoomIn, InputType.Controller, PlayerActions.Instance.LastDeviceStyle);
			string actionGlyph3 = m_iconService.GetActionGlyph(PlayerActions.Instance.m_placeUnit, InputType.Controller, PlayerActions.Instance.LastDeviceStyle);
			switch (sceneSettings.m_EdgeType)
			{
			case EdgeType.Line:
				text = "Scroll to rotate" + Environment.NewLine + "Left click to confirm";
				text2 = $"{actionGlyph} | {actionGlyph2} to rotate \n {actionGlyph3} to confirm";
				break;
			case EdgeType.Circle:
				text = "Scroll to change size" + Environment.NewLine + "Left click to confirm";
				text2 = $"{actionGlyph} | {actionGlyph2} to change size \n {actionGlyph3} to confirm";
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			if (PlayerActions.Instance.InputType != InputType.Controller)
			{
				return text;
			}
			return text2;
		}

		public void StopEditing()
		{
			UnityEngine.Object.FindObjectOfType<PlacementUI>();
			helpText.gameObject.SetActive(value: false);
			isEditing = false;
			SetToggleCanvas(state: true);
		}

		public void SetEdgeType(int type)
		{
			if (UnityEngine.Object.FindObjectOfType<SceneSettings>().SetType((EdgeType)type))
			{
				EditTeamLine();
			}
		}

		public void UpdateUI()
		{
			dropdown.value = (int)UnityEngine.Object.FindObjectOfType<SceneSettings>().m_EdgeType;
		}

		public void SwapTeamEdge()
		{
			UnityEngine.Object.FindObjectOfType<SceneSettings>().SwapTeamEdge();
		}

		public void ToggleOpenClose()
		{
			UIScreenInputBlocker.AnimatedMenuTransitionStart();
			if (!IsOpen)
			{
				SetToggleCanvas(state: true);
			}
			buttonAnimation.ToggleState();
		}

		private void Open()
		{
			IsOpen = true;
			this.MapSettingsOpened?.Invoke();
			UIScreenInputBlocker.AnimatedMenuTransitionEnd();
			m_inputService.OnUIOpen();
			if (!EscapeMenuHandler.InMenu)
			{
				m_firstSelectable.Select();
			}
		}

		private void Close()
		{
			IsOpen = false;
			this.MapSettingsClosed?.Invoke();
			UIScreenInputBlocker.AnimatedMenuTransitionEnd();
			m_inputService.OnUIClose();
			if (dropdown != null)
			{
				dropdown.Hide();
			}
			if (!EscapeMenuHandler.InMenu)
			{
				EventSystem.current.SetSelectedGameObject(null);
			}
			SetToggleCanvas(state: false);
		}

		private void SetToggleCanvas(bool state)
		{
			if (canvasToggles != null)
			{
				CanvasToggle[] array = canvasToggles;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].SetCanvasToggle(state);
				}
			}
		}

		private void OnInputChanged(InputType inputType)
		{
			if (IsOpen || isEditing)
			{
				switch (inputType)
				{
				case InputType.Controller:
					m_firstSelectable.Select();
					break;
				case InputType.Keyboard:
				case InputType.Any:
					EventSystem.current.SetSelectedGameObject(null);
					break;
				default:
					throw new ArgumentOutOfRangeException("inputType", inputType, null);
				}
				helpText.text = GetHelpText();
			}
		}

		private void OnDestroy()
		{
			buttonAnimation.OnCompleteState01 -= m_winConditionPresenter.ShowConditions;
			buttonAnimation.OnCompleteState02 -= m_winConditionPresenter.HideConditions;
			if (m_inputService != null)
			{
				m_inputService.InputChanged -= OnInputChanged;
			}
			if (buttonAnimation != null)
			{
				buttonAnimation.OnCompleteState01 -= Close;
				buttonAnimation.OnCompleteState02 -= Open;
			}
			if (InstancedHandler<EscapeMenuHandler>.Instance != null)
			{
				InstancedHandler<EscapeMenuHandler>.Instance.EscapeMenuClosed -= OnEscapeMenuClosed;
			}
			IsOpen = false;
		}
	}
}
