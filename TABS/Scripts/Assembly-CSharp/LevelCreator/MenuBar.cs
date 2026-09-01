using InControl;
using Landfall.TABS_Input;
using UnityEngine;
using UnityEngine.UI;

namespace LevelCreator
{
	public class MenuBar : DMUIPanel
	{
		[SerializeField]
		private RectTransform m_blur;

		[SerializeField]
		private GameObject m_quickSaveButton;

		[SerializeField]
		private Button m_quickSaveButtonButton;

		[SerializeField]
		private Image m_quickSaveButtonImage;

		[SerializeField]
		private Button m_toggleBarButton;

		[SerializeField]
		private CanvasGroup m_onScreenButtons;

		[SerializeField]
		private SaveMenu saveMenu;

		private float m_lerpTime = 0.35f;

		private void AssertionCheck()
		{
		}

		private void Start()
		{
			AssertionCheck();
			AssignInputs();
			m_toggleBarButton.onClick.AddListener(delegate
			{
				if (!base.HasFocus)
				{
					DMUIManager.Instance.OpenPanel(DMUIManager.UIPanels.TopBar);
				}
				else
				{
					DMUIManager.Instance.PopPanel();
				}
			});
			OnClose();
			TutorialPopUps.MenubarPopUp(this);
		}

		private void OnEnable()
		{
			saveMenu.OnOpenSaveMenu += OnClose;
		}

		private void OnDisable()
		{
			saveMenu.OnOpenSaveMenu -= OnClose;
		}

		private void OnInputTypeChanged(BindingSourceType obj)
		{
			PlayerActions instance = PlayerActions.Instance;
			m_toggleBarButton.Select();
			if (instance.InputType == InputType.Keyboard)
			{
				DMEditor.Instance.ShowCursor();
			}
			DMEditor.Instance.UpdateCursor();
		}

		private void Update()
		{
			m_onScreenButtons.interactable = base.HasFocus;
		}

		private void AssignInputs()
		{
			PlayerActions instance = PlayerActions.Instance;
			m_inputState.AddOnKeyDownListener(instance.m_enterExitBattle, delegate
			{
				DMUIManager.Instance.PopPanel();
			});
			m_inputState.AddOnKeyDownListener(instance.m_back, delegate
			{
				DMUIManager.Instance.PopPanel();
			});
		}

		public void QuickSave()
		{
			DMEditor.Instance.QuickSave();
			DMUIManager.Instance.PopAll();
		}

		private void UpdateQuickSaveButton()
		{
			bool active = DMEditor.HasSaveableLevelPath();
			m_quickSaveButton.SetActive(active);
			bool flag = DMEditor.Instance.HasDirtyLevelData();
			m_quickSaveButtonButton.interactable = flag;
			m_quickSaveButtonImage.color = (flag ? Color.white : Color.gray);
		}

		private void ToggleBarButtonRotation(bool open)
		{
			m_toggleBarButton.transform.GetChild(0).GetComponent<Image>().transform.LeanRotateZ(open ? 180 : 0, 0.5f).setEaseInOutBack();
		}

		public override void OnOpen()
		{
			base.OnOpen();
			DMEditor.Instance.toolBar.Hide();
			LeanTween.color(m_blur, Color.white, m_lerpTime);
			m_onScreenButtons.interactable = true;
			UpdateQuickSaveButton();
			ToggleBarButtonRotation(open: true);
			Utility.PlaySound("UI/Swosh", 1f, base.transform);
			PlayerActions.Instance.OnLastInputTypeChanged += OnInputTypeChanged;
		}

		public override void OnClose()
		{
			base.OnClose();
			DMEditor.Instance.toolBar.Show();
			LeanTween.color(m_blur, new Color(1f, 1f, 1f, 0f), m_lerpTime);
			m_onScreenButtons.interactable = false;
			ToggleBarButtonRotation(open: false);
			Utility.PlaySound("UI/Swosh", 1f, base.transform);
			PlayerActions.Instance.OnLastInputTypeChanged -= OnInputTypeChanged;
		}

		private void OnDestroy()
		{
			PlayerActions.Instance.OnLastInputTypeChanged -= OnInputTypeChanged;
		}
	}
}
