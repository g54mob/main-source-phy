using System.Collections;
using BitCode.Networking;
using InControl;
using Landfall.TABS_Input;
using TFBGames;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Landfall.TABS
{
	public class RoadmapHandler : UIComponentMainMenu
	{
		public RectTransform[] m_Pages;

		public GameObject m_RightNextButton;

		public GameObject m_LeftNextButton;

		[SerializeField]
		private GameObject m_RightNextButtonSmall;

		[SerializeField]
		private GameObject m_LeftNextButtonSmall;

		public Selectable m_JulyImageSelectable;

		public Selectable m_FactionsImageButtonSelectable;

		[SerializeField]
		private TextMeshProUGUI m_selectFooterUpdateInfo;

		[SerializeField]
		private TextMeshProUGUI m_selectUpdateInfo;

		[SerializeField]
		private TextMeshProUGUI m_selectReadMoreInfo;

		[SerializeField]
		private MainMenuUIHandler m_mainMenuUIHandler;

		[SerializeField]
		private float buttonSwapAspectRatioThreshold = 1.7f;

		private Button m_RightNextSelectableButton;

		private Button m_LeftNextSelectableButton;

		public float spring = 1f;

		public float drag = 5f;

		private bool useSmallButtons;

		private int m_currentPage;

		private PlayerActions m_playerActions;

		private GlyphService glyphService;

		private const string GamepadSelectText = "Press {0} to visit the Landfall website";

		private const string KeyboardSelectText = "Click on the image to visit the Landfall website";

		private const string KeyboardImageSelectText = "Click to open in browser";

		private string acceptGlyphText;

		public RectTransform CurrentPageTransform
		{
			get
			{
				if (m_currentPage < 0 || m_currentPage >= m_Pages.Length)
				{
					return null;
				}
				return m_Pages[m_currentPage];
			}
		}

		protected override void Awake()
		{
			base.Awake();
			for (int i = 1; i < m_Pages.Length; i++)
			{
				m_Pages[i].gameObject.SetActive(value: false);
				m_Pages[i].anchoredPosition = new Vector3(2000f, 0f, 0f);
			}
			m_RightNextSelectableButton = m_RightNextButton.GetComponent<Button>();
			m_LeftNextSelectableButton = m_LeftNextButton.GetComponent<Button>();
			m_playerActions = PlayerActions.Instance;
			float num = (float)Screen.width / (float)Screen.height;
			useSmallButtons = num < buttonSwapAspectRatioThreshold;
		}

		protected override void Start()
		{
			base.Start();
			UpdateText(m_playerActions.InputType, m_playerActions.LastDeviceStyle);
			UpdateRightLeftButtons(m_currentPage);
			m_selectFooterUpdateInfo.gameObject.SetActive(value: true);
			m_selectUpdateInfo.gameObject.SetActive(value: true);
			m_selectReadMoreInfo.gameObject.SetActive(value: true);
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			glyphService = ServiceLocator.GetService<GlyphService>();
			InputService service = ServiceLocator.GetService<InputService>();
			if (service != null)
			{
				service.InputChanged += OnInputSourceChanged;
				service.InputDeviceStyleChanged += OnInputDeviceStyleChanged;
			}
			m_JulyImageSelectable.gameObject.GetComponent<RoadmapTextImage>().ShowDetails();
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			InputService service = ServiceLocator.GetService<InputService>();
			if (service != null)
			{
				service.InputChanged -= OnInputSourceChanged;
				service.InputDeviceStyleChanged -= OnInputDeviceStyleChanged;
			}
		}

		protected override void Update()
		{
			base.Update();
			if (PlayerActions.Instance.m_back.WasPressed && base.IsActive)
			{
				GoBack();
			}
			if (m_playerActions.m_pageRight.WasPressed)
			{
				m_RightNextSelectableButton.onClick.Invoke();
			}
			if (m_playerActions.m_pageLeft.WasPressed)
			{
				m_LeftNextSelectableButton.onClick.Invoke();
			}
		}

		protected override void OnReceivedInvitation(IGameInvitation invite)
		{
			if (base.IsActive)
			{
				GoBack();
			}
		}

		private void OnInputSourceChanged(InputType type)
		{
			UpdateText(type, m_playerActions.LastDeviceStyle);
			if ((!(m_mainMenuUIHandler != null) || m_mainMenuUIHandler.currentMenuState == MenuState.Roadmap) && type == InputType.Controller)
			{
				SelectFirstButton();
			}
		}

		private void OnInputDeviceStyleChanged(InputDeviceStyle deviceStyle)
		{
			UpdateText(m_playerActions.InputType, deviceStyle);
		}

		private void UpdateText(InputType type, InputDeviceStyle deviceStyle)
		{
			acceptGlyphText = glyphService.GetActionGlyph(m_playerActions.m_accept, InputType.Controller, deviceStyle);
			string message = "Please set the text references for Roadmap menu";
			if (m_selectUpdateInfo != null)
			{
				string text = ((type == InputType.Controller) ? "" : "Click to open in browser");
				m_selectUpdateInfo.text = text;
			}
			else
			{
				Debug.LogError(message);
			}
			if (m_selectFooterUpdateInfo != null)
			{
				string text2 = ((type == InputType.Controller) ? $"Press {acceptGlyphText} to visit the Landfall website" : "Click on the image to visit the Landfall website");
				m_selectFooterUpdateInfo.text = text2;
			}
			else
			{
				Debug.LogError(message);
			}
		}

		public void SelectFirstButton()
		{
			if ((!(m_mainMenuUIHandler != null) || (m_mainMenuUIHandler.currentMenuState == MenuState.Roadmap && m_playerActions.InputType == InputType.Controller)) && m_playerActions.InputType == InputType.Controller)
			{
				if (IsRightArrowActive())
				{
					EventSystem.current.SetSelectedGameObject(m_JulyImageSelectable.gameObject);
				}
				else if (IsLeftArrowActive())
				{
					m_FactionsImageButtonSelectable.Select();
				}
			}
		}

		public void MovePage(int addedIndex)
		{
			if (addedIndex == 0)
			{
				return;
			}
			int num = m_currentPage + addedIndex;
			if (num <= m_Pages.Length - 1 && num >= 0)
			{
				StopAllCoroutines();
				if (addedIndex > 0)
				{
					MoveOutLeft(m_Pages[m_currentPage]);
				}
				else if (addedIndex < 0)
				{
					MoveOutRight(m_Pages[m_currentPage]);
				}
				MoveIn(m_Pages[num]);
				m_currentPage = num;
				UpdateRightLeftButtons(m_currentPage);
				SelectFirstButton();
			}
		}

		private void UpdateRightLeftButtons(int index)
		{
			if (m_Pages.Length == 1)
			{
				SetRightButtonsActive(active: false);
				SetLeftButtonsActive(active: false);
			}
			else if (index == 0)
			{
				SetRightButtonsActive(active: true);
				SetLeftButtonsActive(active: false);
			}
			else if (index == m_Pages.Length - 1)
			{
				SetRightButtonsActive(active: false);
				SetLeftButtonsActive(active: true);
			}
			else
			{
				SetRightButtonsActive(active: true);
				SetLeftButtonsActive(active: true);
			}
		}

		private void MoveOutLeft(RectTransform page)
		{
			StartCoroutine(MovePageTo(page, new Vector3(-2000f, 0f, 0f), setDisabled: true));
		}

		private void MoveOutRight(RectTransform page)
		{
			StartCoroutine(MovePageTo(page, new Vector3(2000f, 0f, 0f), setDisabled: true));
		}

		private void MoveIn(RectTransform page)
		{
			page.gameObject.SetActive(value: true);
			StartCoroutine(MovePageTo(page, Vector3.zero));
		}

		private IEnumerator MovePageTo(RectTransform page, Vector3 pos, bool setDisabled = false)
		{
			float travelTime = 2f;
			Vector2 velocity = Vector3.zero;
			float time = 0f;
			while (time < travelTime)
			{
				yield return null;
				float num = Mathf.Clamp(Time.unscaledDeltaTime, 0f, 0.0166f);
				time += num;
				Vector2 anchoredPosition = page.anchoredPosition;
				velocity += spring * num * ((Vector2)pos - anchoredPosition);
				velocity -= drag * num * velocity;
				anchoredPosition += velocity * num;
				page.anchoredPosition = anchoredPosition;
			}
			if (setDisabled)
			{
				page.gameObject.SetActive(value: false);
			}
		}

		private bool IsRightArrowActive()
		{
			if (m_RightNextSelectableButton != null && m_RightNextButton != null && m_RightNextButton.activeInHierarchy)
			{
				return true;
			}
			if (m_RightNextButtonSmall != null)
			{
				return m_RightNextButtonSmall.activeInHierarchy;
			}
			return false;
		}

		private bool IsLeftArrowActive()
		{
			if (m_LeftNextSelectableButton != null && m_LeftNextButton != null && m_LeftNextButton.activeInHierarchy)
			{
				return true;
			}
			if (m_LeftNextButtonSmall != null)
			{
				return m_LeftNextButtonSmall.activeInHierarchy;
			}
			return false;
		}

		private void SetRightButtonsActive(bool active)
		{
			if (m_RightNextButton != null)
			{
				m_RightNextButton.SetActive(!useSmallButtons && active);
			}
			if (m_RightNextButtonSmall != null)
			{
				m_RightNextButtonSmall.SetActive(useSmallButtons && active);
			}
		}

		private void SetLeftButtonsActive(bool active)
		{
			if (m_LeftNextButton != null)
			{
				m_LeftNextButton.SetActive(!useSmallButtons && active);
			}
			if (m_LeftNextButtonSmall != null)
			{
				m_LeftNextButtonSmall.SetActive(useSmallButtons && active);
			}
		}

		public void GoBack()
		{
			base.Close();
		}

		protected override void OnOpen()
		{
			base.OnOpen();
			SelectFirstButton();
		}

		protected override void OnClose()
		{
			base.OnClose();
		}
	}
}
