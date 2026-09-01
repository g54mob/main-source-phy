using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class uConsoleGUI : MonoBehaviour
{
	public GameObject m_ConsoleEventSystem;

	public RectTransform m_PanelRectTransform;

	public Image m_PanelImage;

	public TextMeshProUGUI m_LogText;

	public TMP_InputField m_InputField;

	public TextMeshProUGUI m_InputFieldText;

	private RectTransform m_InputFieldRectTransform;

	private int m_LogScrollOffset;

	private void Start()
	{
		m_PanelRectTransform.anchoredPosition = new Vector2(0f, 0f);
		m_PanelRectTransform.sizeDelta = new Vector2(Screen.width, 0f);
		m_InputField = GetComponentInChildren<TMP_InputField>();
		m_InputFieldRectTransform = m_InputField.GetComponent<RectTransform>();
		m_InputField.DeactivateInputField();
		m_LogScrollOffset = 0;
	}

	private void Update()
	{
		MaybeInstantiateEventSystem();
		MaybeDeactivateInputField();
		UpdateDimensions();
		UpdateWithCustomizationSettings();
		Animate(Time.unscaledDeltaTime);
	}

	public void ScrollLogUp()
	{
		m_LogScrollOffset++;
		if (m_LogScrollOffset > uConsoleLog.GetNumLines())
		{
			m_LogScrollOffset = uConsoleLog.GetNumLines();
		}
		RefreshLogText();
	}

	public void ScrollLogDown()
	{
		m_LogScrollOffset--;
		if (m_LogScrollOffset < 0)
		{
			m_LogScrollOffset = 0;
		}
		RefreshLogText();
	}

	public void ScrollLogUpMax()
	{
		m_LogScrollOffset = uConsoleLog.GetNumLines();
		RefreshLogText();
	}

	public void ScrollLogDownMax()
	{
		m_LogScrollOffset = 0;
		RefreshLogText();
	}

	public void RefreshLogText()
	{
		int num = ComputeMaxDisplayLinesForLog();
		int num2 = uConsoleLog.GetNumLines() - m_LogScrollOffset - num;
		if (num2 < 0)
		{
			num2 = 0;
		}
		m_LogText.text = "";
		for (int i = num2; i < uConsoleLog.GetNumLines() - m_LogScrollOffset; i++)
		{
			m_LogText.text += "\n";
			m_LogText.text += uConsoleLog.GetLine(i);
		}
	}

	public void InputFieldSetFocus()
	{
		if (!m_InputField.isFocused)
		{
			m_InputField.ActivateInputField();
			m_InputField.Select();
			StartCoroutine(MoveTextEnd_NextFrame());
		}
	}

	private IEnumerator MoveTextEnd_NextFrame()
	{
		yield return 0;
		m_InputField.MoveTextEnd(shift: false);
	}

	public string InputFieldGetText()
	{
		return m_InputField.text;
	}

	public void InputFieldClearText()
	{
		m_InputField.text = "";
		m_InputField.MoveTextStart(shift: false);
	}

	public void InputFieldSetText(string text)
	{
		m_InputField.text = text;
	}

	public void InputFieldMoveCaretToEnd()
	{
		m_InputField.MoveTextEnd(shift: false);
	}

	public void InputFieldDeactivate()
	{
		m_InputField.DeactivateInputField();
	}

	private void Animate(float deltaTimeSeconds)
	{
		if (uConsole.IsOn())
		{
			float num = uConsole.m_Instance.m_ConsoleHeightNormalized * (float)Screen.height;
			if (Mathf.Approximately(m_PanelRectTransform.sizeDelta.y, num))
			{
				return;
			}
			float y = CalculatePixelsMovedForAnimation(deltaTimeSeconds, uConsole.m_Instance.m_SecondsToAnimateDown);
			if (m_PanelRectTransform.sizeDelta.y < num)
			{
				m_PanelRectTransform.sizeDelta += new Vector2(0f, y);
				if (m_PanelRectTransform.sizeDelta.y >= num)
				{
					m_PanelRectTransform.sizeDelta = new Vector2(m_PanelRectTransform.sizeDelta.x, num);
				}
			}
		}
		else if (m_PanelRectTransform.sizeDelta.y > 0f)
		{
			float b = CalculatePixelsMovedForAnimation(deltaTimeSeconds, uConsole.m_Instance.m_SecondsToAnimateUp);
			m_PanelRectTransform.sizeDelta -= new Vector2(0f, Mathf.Min(m_PanelRectTransform.sizeDelta.y, b));
		}
	}

	private float CalculatePixelsMovedForAnimation(float deltaSeconds, float fullAnimateSeconds)
	{
		float num = (float)Screen.height * uConsole.m_Instance.m_ConsoleHeightNormalized;
		float num2 = Mathf.Clamp(deltaSeconds / fullAnimateSeconds, 0f, 1f);
		return num * num2;
	}

	private void UpdateDimensions()
	{
		m_PanelRectTransform.sizeDelta = new Vector2(Screen.width, m_PanelRectTransform.sizeDelta.y);
		m_InputFieldRectTransform.sizeDelta = new Vector2(Screen.width, uConsole.m_Instance.m_InputFieldHeight);
	}

	private void UpdateWithCustomizationSettings()
	{
		m_LogText.font = uConsole.m_Instance.m_LogFont;
		m_LogText.fontSize = uConsole.m_Instance.m_LogFontSize;
		m_LogText.color = uConsole.m_Instance.m_LogFontColor;
		m_InputFieldText.font = uConsole.m_Instance.m_InputFieldFont;
		m_InputFieldText.color = uConsole.m_Instance.m_InputFieldFontColor;
		m_InputFieldText.fontSize = uConsole.m_Instance.m_InputFieldFontSize;
		m_PanelImage.color = uConsole.m_Instance.m_LogBackGroundColor;
		m_InputField.image.color = uConsole.m_Instance.m_InputFieldBackGroundColor;
	}

	private int ComputeMaxDisplayLinesForLog()
	{
		return 32;
	}

	private void MaybeInstantiateEventSystem()
	{
		if (!(EventSystem.current != null))
		{
			GameObject gameObject = Object.Instantiate(m_ConsoleEventSystem);
			if ((bool)gameObject)
			{
				gameObject.name = m_ConsoleEventSystem.name;
				gameObject.transform.parent = base.transform;
			}
		}
	}

	private void MaybeDeactivateInputField()
	{
		if (!uConsole.IsOn())
		{
			m_InputField.DeactivateInputField();
		}
	}
}
