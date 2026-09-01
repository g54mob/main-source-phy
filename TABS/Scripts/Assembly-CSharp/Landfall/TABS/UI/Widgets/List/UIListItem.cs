using Landfall.TABS.UI.Widgets.Fields;
using TFBGames;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Landfall.TABS.UI.Widgets.List
{
	public class UIListItem : MonoBehaviour, ISubmitHandler, IEventSystemHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
	{
		private LocalizeText m_displayText;

		private RectTransform m_rectTransform;

		private Image m_backgroundImage;

		[SerializeField]
		private Image m_ConditionTypeIconImage;

		[SerializeField]
		private UIPointerEvents m_inspectorButton;

		[SerializeField]
		private Selectable m_Selectable;

		private object m_userData;

		[SerializeField]
		private Button m_RemoveButton;

		[SerializeField]
		private GameObject m_RowSpacer;

		[SerializeField]
		private WinConditionsTeamPanel m_WinConditionsTeamPanel;

		[SerializeField]
		private UIScaleJiggle m_ScaleJiggle;

		[SerializeField]
		private UnityEvent m_OnSubmit;

		[SerializeField]
		private UnityEvent m_OnSelect;

		[SerializeField]
		private UnityEvent m_OnDeselect;

		[SerializeField]
		private UnityEvent m_OnPointerEnter;

		[SerializeField]
		private UnityEvent m_OnPointerExit;

		public float ElementHeight
		{
			get
			{
				return m_rectTransform.sizeDelta.y;
			}
			set
			{
				m_rectTransform.sizeDelta = new Vector2(m_rectTransform.sizeDelta.x, value);
			}
		}

		public RectTransform RectTransform => m_rectTransform;

		public Image BackgroundImage => m_backgroundImage;

		public Image ConditionIcon
		{
			get
			{
				return m_ConditionTypeIconImage;
			}
			set
			{
				m_ConditionTypeIconImage = value;
			}
		}

		public UIPointerEvents InspectorButton => m_inspectorButton;

		public Selectable Selectable => m_Selectable;

		public object UserData
		{
			get
			{
				return m_userData;
			}
			set
			{
				m_userData = value;
			}
		}

		public GameObject RemoveButton => m_RemoveButton.gameObject;

		public GameObject RowSpacer => m_RowSpacer;

		public UnityEvent OnSubmitCallback
		{
			get
			{
				return m_OnSubmit;
			}
			set
			{
				m_OnSubmit = value;
			}
		}

		public UnityEvent OnSelectCallback
		{
			get
			{
				return m_OnSelect;
			}
			set
			{
				m_OnSelect = value;
			}
		}

		public UnityEvent OnDeselectCallback
		{
			get
			{
				return m_OnDeselect;
			}
			set
			{
				m_OnDeselect = value;
			}
		}

		public UnityEvent OnPointerEnterCallback
		{
			get
			{
				return m_OnPointerEnter;
			}
			set
			{
				m_OnPointerEnter = value;
			}
		}

		public UnityEvent OnPointerExitCallback
		{
			get
			{
				return m_OnPointerExit;
			}
			set
			{
				m_OnPointerExit = value;
			}
		}

		public void SetDisplayText(string text, string[] args)
		{
			m_displayText.Args = args;
			m_displayText.LocaleID = text;
		}

		private void Awake()
		{
			m_rectTransform = GetComponent<RectTransform>();
			m_backgroundImage = GetComponent<Image>();
			m_displayText = GetComponentInChildren<LocalizeText>();
			if (m_RemoveButton != null)
			{
				m_RemoveButton.onClick.AddListener(RemoveFromList);
				m_RemoveButton.gameObject.SetActive(value: false);
			}
		}

		private void Start()
		{
			if (m_ScaleJiggle != null)
			{
				m_ScaleJiggle.AddClickForce();
			}
		}

		private void OnDestroy()
		{
			OnSelectCallback.RemoveAllListeners();
			OnDeselectCallback.RemoveAllListeners();
			OnSubmitCallback.RemoveAllListeners();
		}

		public void TriggerSubmit()
		{
			m_OnSubmit?.Invoke();
		}

		public void TriggerSelect()
		{
			m_OnSelect?.Invoke();
		}

		public void TriggerDeselect()
		{
			m_OnDeselect?.Invoke();
		}

		private void RemoveFromList()
		{
			if (m_WinConditionsTeamPanel != null)
			{
				m_WinConditionsTeamPanel.RemoveSelectedCondition(this);
			}
		}

		public void OnSubmit(BaseEventData eventData)
		{
			m_OnSubmit?.Invoke();
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			m_OnSubmit?.Invoke();
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			m_OnPointerEnter?.Invoke();
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			m_OnPointerExit?.Invoke();
		}
	}
}
