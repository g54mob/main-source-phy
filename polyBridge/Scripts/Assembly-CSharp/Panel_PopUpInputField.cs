using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_PopUpInputField : MonoBehaviour
{
	[Header("Header")]
	public TextMeshProUGUI m_Title;

	[Header("Body")]
	public RectTransform m_PanelRootRectTransform;

	public RectTransform m_InputFieldRectTransform;

	public TMP_InputField m_InputField;

	public Scrollbar m_ScrollBar;

	public Image m_ScrollBarBackground;

	[Header("Footer")]
	public Button m_CancelButton;

	public Button m_OkButton;

	[NonSerialized]
	public Action<string> m_OnOkDelegate;

	private bool m_FilterInputForFilenames;

	private bool m_FilterInputForDirectories;

	private bool m_EnterEndEdit;

	private const int PANEL_HEIGHT_REGULAR = 120;

	private const int INPUTFIELD_HEIGHT_REGULAR = 25;

	private const int PANEL_HEIGHT_LARGE = 250;

	private const int INPUTFIELD_HEIGHT_LARGE = 160;

	private void Awake()
	{
		m_CancelButton.onClick.AddListener(OnCancel);
		m_OkButton.onClick.AddListener(OnOk);
		m_InputField.onValueChanged.AddListener(delegate
		{
			OnValueChanged();
		});
	}

	private void OnEnable()
	{
		InterfaceAudio.Play("ui_menubar_gen_on");
		ActivePanels.Add(base.gameObject);
	}

	private void OnDisable()
	{
		ActivePanels.Remove(base.gameObject);
	}

	private void Update()
	{
		ProcessInput();
		m_InputField.ActivateInputField();
		m_ScrollBar.targetGraphic.enabled = m_ScrollBar.size < 0.98f;
		m_ScrollBarBackground.enabled = m_ScrollBar.targetGraphic.enabled;
	}

	public void SetRegularSize()
	{
		m_PanelRootRectTransform.sizeDelta = new Vector2(m_PanelRootRectTransform.sizeDelta.x, 120f);
		m_InputFieldRectTransform.sizeDelta = new Vector2(m_InputFieldRectTransform.sizeDelta.x, 25f);
		m_InputField.lineType = TMP_InputField.LineType.SingleLine;
		m_ScrollBar.gameObject.SetActive(value: false);
		m_EnterEndEdit = true;
	}

	public void SetLargeSize()
	{
		m_PanelRootRectTransform.sizeDelta = new Vector2(m_PanelRootRectTransform.sizeDelta.x, 250f);
		m_InputFieldRectTransform.sizeDelta = new Vector2(m_InputFieldRectTransform.sizeDelta.x, 160f);
		m_InputField.lineType = TMP_InputField.LineType.MultiLineNewline;
		m_ScrollBar.gameObject.SetActive(value: true);
		m_EnterEndEdit = false;
	}

	public void FilterForFilenames(bool enable)
	{
		m_FilterInputForFilenames = enable;
	}

	public void FilterForDirectories(bool enable)
	{
		m_FilterInputForDirectories = enable;
	}

	private void OnCancel()
	{
		Close();
		InterfaceAudio.Play("ui_menu_cancel");
	}

	private void OnOk()
	{
		if (string.IsNullOrEmpty(m_InputField.text.Trim()))
		{
			InterfaceAudio.Play("ui_error");
			return;
		}
		m_OnOkDelegate?.Invoke(m_InputField.text.Trim());
		Close();
		InterfaceAudio.Play("ui_menu_accept");
	}

	private void OnValueChanged()
	{
		if (m_FilterInputForFilenames)
		{
			m_InputField.text = Utils.RemoveInvalidCharsFromFilename(m_InputField.text);
		}
		if (m_FilterInputForDirectories)
		{
			m_InputField.text = Utils.RemoveInvalidCharsFromPath(m_InputField.text);
		}
	}

	private void Close()
	{
		GameUI.m_Instance.m_PopUpInputField.gameObject.SetActive(value: false);
	}

	private void ProcessInput()
	{
		if (GameStateCommonInput.IgnoreKeyboardInputForPanel(base.gameObject))
		{
			return;
		}
		if (m_EnterEndEdit && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || GamepadManager.ButtonJustPressed(GamepadButtonType.NORTH)))
		{
			OnOk();
		}
		if (Input.GetKeyDown(KeyCode.Escape) || GamepadManager.ButtonJustPressed(GamepadButtonType.EAST))
		{
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.EAST))
			{
				Game.ForceIgnoreNextSelection();
			}
			InterfaceAudio.Play("ui_window_close");
			OnCancel();
		}
	}
}
