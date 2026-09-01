using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_ModsScreenUI : MonoBehaviour
{
	public TextMeshProUGUI m_TextPrefab;

	public ToolTip m_TooltipTextPrefab;

	public Image m_SpritePrefab;

	public Button m_ButtonPrefab;

	private Dictionary<string, TextMeshProUGUI> m_TextDict = new Dictionary<string, TextMeshProUGUI>();

	private Dictionary<string, ToolTip> m_TooltipTextDict = new Dictionary<string, ToolTip>();

	private Dictionary<string, Image> m_SpriteDict = new Dictionary<string, Image>();

	private Dictionary<string, Button> m_ButtonDict = new Dictionary<string, Button>();

	private Dictionary<string, string> m_ButtonCallbacksDict = new Dictionary<string, string>();

	public void ResetToDefault()
	{
		foreach (string key in m_TextDict.Keys)
		{
			Object.Destroy(m_TextDict[key].gameObject);
		}
		m_TextDict.Clear();
		foreach (string key2 in m_TooltipTextDict.Keys)
		{
			Object.Destroy(m_TooltipTextDict[key2].gameObject);
		}
		m_TooltipTextDict.Clear();
		foreach (string key3 in m_SpriteDict.Keys)
		{
			Object.Destroy(m_SpriteDict[key3].gameObject);
		}
		m_SpriteDict.Clear();
		foreach (string key4 in m_ButtonDict.Keys)
		{
			Object.Destroy(m_ButtonDict[key4].gameObject);
		}
		m_ButtonDict.Clear();
		m_ButtonCallbacksDict.Clear();
	}

	public void OnButtonClicked(GameObject buttonObj)
	{
		if (m_ButtonCallbacksDict.ContainsKey(buttonObj.name))
		{
			ModApi.RunCallback(m_ButtonCallbacksDict[buttonObj.name]);
		}
	}

	public void CreateTextObject(string textId, int width, int height)
	{
		if (!m_TextDict.ContainsKey(textId) && !m_TooltipTextDict.ContainsKey(textId))
		{
			TextMeshProUGUI textMeshProUGUI = Object.Instantiate(m_TextPrefab, base.transform);
			textMeshProUGUI.name = textId;
			textMeshProUGUI.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
			textMeshProUGUI.gameObject.SetActive(value: true);
			m_TextDict.Add(textId, textMeshProUGUI);
		}
	}

	public void DestroyTextObject(string textId)
	{
		if (m_TextDict.ContainsKey(textId))
		{
			Object.Destroy(m_TextDict[textId].gameObject);
			m_TextDict.Remove(textId);
		}
		if (m_TooltipTextDict.ContainsKey(textId))
		{
			Object.Destroy(m_TooltipTextDict[textId].gameObject);
			m_TooltipTextDict.Remove(textId);
		}
	}

	public void UpdateTextString(string textId, string textStr)
	{
		if (m_TextDict.ContainsKey(textId))
		{
			m_TextDict[textId].text = textStr;
		}
		if (m_TooltipTextDict.ContainsKey(textId))
		{
			m_TooltipTextDict[textId].Set(textStr, null);
		}
	}

	public void UpdateTextAlignment(string textId, string horizontalAlign, string verticalAlign)
	{
		if (m_TextDict.ContainsKey(textId))
		{
			HorizontalAlignmentOptions horizontalAlignment = m_TextDict[textId].horizontalAlignment;
			if (horizontalAlign.ToUpper() == "LEFT")
			{
				horizontalAlignment = HorizontalAlignmentOptions.Left;
			}
			else if (horizontalAlign.ToUpper() == "CENTER")
			{
				horizontalAlignment = HorizontalAlignmentOptions.Center;
			}
			else if (horizontalAlign.ToUpper() == "RIGHT")
			{
				horizontalAlignment = HorizontalAlignmentOptions.Right;
			}
			else if (horizontalAlign.ToUpper() == "JUSTIFIED")
			{
				horizontalAlignment = HorizontalAlignmentOptions.Justified;
			}
			m_TextDict[textId].horizontalAlignment = horizontalAlignment;
			VerticalAlignmentOptions verticalAlignment = m_TextDict[textId].verticalAlignment;
			if (verticalAlign.ToUpper() == "TOP")
			{
				verticalAlignment = VerticalAlignmentOptions.Top;
			}
			else if (verticalAlign.ToUpper() == "MIDDLE")
			{
				verticalAlignment = VerticalAlignmentOptions.Middle;
			}
			else if (verticalAlign.ToUpper() == "BOTTOM")
			{
				verticalAlignment = VerticalAlignmentOptions.Bottom;
			}
			m_TextDict[textId].verticalAlignment = verticalAlignment;
		}
		if (m_TooltipTextDict.ContainsKey(textId))
		{
			HorizontalAlignmentOptions horizontalAlignment2 = m_TooltipTextDict[textId].m_Text.horizontalAlignment;
			if (horizontalAlign.ToUpper() == "LEFT")
			{
				horizontalAlignment2 = HorizontalAlignmentOptions.Left;
			}
			else if (horizontalAlign.ToUpper() == "CENTER")
			{
				horizontalAlignment2 = HorizontalAlignmentOptions.Center;
			}
			else if (horizontalAlign.ToUpper() == "RIGHT")
			{
				horizontalAlignment2 = HorizontalAlignmentOptions.Right;
			}
			else if (horizontalAlign.ToUpper() == "JUSTIFIED")
			{
				horizontalAlignment2 = HorizontalAlignmentOptions.Justified;
			}
			m_TooltipTextDict[textId].m_Text.horizontalAlignment = horizontalAlignment2;
			VerticalAlignmentOptions verticalAlignment2 = m_TooltipTextDict[textId].m_Text.verticalAlignment;
			if (verticalAlign.ToUpper() == "TOP")
			{
				verticalAlignment2 = VerticalAlignmentOptions.Top;
			}
			else if (verticalAlign.ToUpper() == "MIDDLE")
			{
				verticalAlignment2 = VerticalAlignmentOptions.Middle;
			}
			else if (verticalAlign.ToUpper() == "BOTTOM")
			{
				verticalAlignment2 = VerticalAlignmentOptions.Bottom;
			}
			m_TooltipTextDict[textId].m_Text.verticalAlignment = verticalAlignment2;
		}
	}

	public void UpdateTextPivot(string textId, float xPivot, float yPivot)
	{
		if (m_TextDict.ContainsKey(textId))
		{
			m_TextDict[textId].GetComponent<RectTransform>().pivot = new Vector2(xPivot, yPivot);
		}
		if (m_TooltipTextDict.ContainsKey(textId))
		{
			m_TooltipTextDict[textId].GetComponent<RectTransform>().pivot = new Vector2(xPivot, yPivot);
		}
	}

	public void UpdateTextScreenPos(string textId, float xScreenPos, float yScreenPos)
	{
		if (m_TextDict.ContainsKey(textId))
		{
			m_TextDict[textId].GetComponent<RectTransform>().anchorMin = new Vector2(xScreenPos, yScreenPos);
			m_TextDict[textId].GetComponent<RectTransform>().anchorMax = new Vector2(xScreenPos, yScreenPos);
			m_TextDict[textId].GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
		}
		if (m_TooltipTextDict.ContainsKey(textId))
		{
			m_TooltipTextDict[textId].GetComponent<RectTransform>().anchorMin = new Vector2(xScreenPos, yScreenPos);
			m_TooltipTextDict[textId].GetComponent<RectTransform>().anchorMax = new Vector2(xScreenPos, yScreenPos);
			m_TooltipTextDict[textId].GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
		}
	}

	public void UpdateTextFontSize(string textId, int fontSize)
	{
		if (m_TextDict.ContainsKey(textId))
		{
			m_TextDict[textId].fontSize = fontSize;
		}
		if (m_TooltipTextDict.ContainsKey(textId))
		{
			m_TooltipTextDict[textId].m_Text.fontSize = fontSize;
		}
	}

	public void UpdateTextColor(string textId, string colorStr)
	{
		if (m_TextDict.ContainsKey(textId))
		{
			Color color = Color.white;
			if (ColorUtility.TryParseHtmlString(colorStr, out color))
			{
				m_TextDict[textId].color = color;
			}
		}
		if (m_TooltipTextDict.ContainsKey(textId))
		{
			m_TooltipTextDict[textId].m_Text.color = Utils.GetColorFromHexCode(colorStr, Color.white);
		}
	}

	public void UpdateTextSetBackgroundActive(string textId, bool bgActive)
	{
		if (bgActive && m_TextDict.ContainsKey(textId) && !m_TooltipTextDict.ContainsKey(textId))
		{
			ToolTip toolTip = Object.Instantiate(m_TooltipTextPrefab, base.transform);
			toolTip.SetMaxWidthOverride(10000);
			toolTip.name = textId;
			CopyTransform(m_TextDict[textId].GetComponent<RectTransform>(), toolTip.GetComponent<RectTransform>());
			CopyText(m_TextDict[textId], toolTip.m_Text);
			toolTip.gameObject.SetActive(value: true);
			DestroyTextObject(textId);
			m_TooltipTextDict.Add(textId, toolTip);
		}
		if (!bgActive && m_TooltipTextDict.ContainsKey(textId) && !m_TextDict.ContainsKey(textId))
		{
			TextMeshProUGUI textMeshProUGUI = Object.Instantiate(m_TextPrefab, base.transform);
			textMeshProUGUI.name = textId;
			CopyTransform(m_TooltipTextDict[textId].GetComponent<RectTransform>(), textMeshProUGUI.GetComponent<RectTransform>());
			CopyText(m_TooltipTextDict[textId].m_Text, textMeshProUGUI);
			textMeshProUGUI.gameObject.SetActive(value: true);
			DestroyTextObject(textId);
			m_TextDict.Add(textId, textMeshProUGUI);
		}
	}

	private void CopyTransform(RectTransform source, RectTransform target)
	{
		target.sizeDelta = source.sizeDelta;
		target.anchorMin = source.anchorMin;
		target.anchorMax = source.anchorMax;
		target.anchoredPosition = Vector2.zero;
		target.pivot = source.pivot;
	}

	private void CopyText(TextMeshProUGUI source, TextMeshProUGUI target)
	{
		target.text = source.text;
		target.horizontalAlignment = source.horizontalAlignment;
		target.verticalAlignment = source.verticalAlignment;
		target.fontSize = source.fontSize;
		target.color = source.color;
	}

	public void UpdateTextSetBackgroundColor(string textId, string backgroundColorStr, string outlineColorStr)
	{
		if (m_TooltipTextDict.ContainsKey(textId))
		{
			m_TooltipTextDict[textId].m_Background.color = Utils.GetColorFromHexCode(backgroundColorStr, Color.white);
			m_TooltipTextDict[textId].m_Outline.color = Utils.GetColorFromHexCode(outlineColorStr, Color.white);
		}
	}

	public void UpdateTextMaxWidth(string textId, int maxWidth)
	{
		if (m_TooltipTextDict.ContainsKey(textId))
		{
			m_TooltipTextDict[textId].SetMaxWidthOverride(maxWidth);
		}
	}

	public void CreateSpriteObject(string spriteId, Sprite sprite, int width, int height)
	{
		if (!m_SpriteDict.ContainsKey(spriteId))
		{
			Image image = Object.Instantiate(m_SpritePrefab, base.transform);
			image.sprite = sprite;
			image.name = spriteId;
			image.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
			image.gameObject.SetActive(value: true);
			m_SpriteDict.Add(spriteId, image);
		}
	}

	public void DestroySpriteObject(string spriteId)
	{
		if (m_SpriteDict.ContainsKey(spriteId))
		{
			Object.Destroy(m_SpriteDict[spriteId].gameObject);
			m_SpriteDict.Remove(spriteId);
		}
	}

	public void UpdateSpriteImage(string spriteId, Sprite sprite)
	{
		if (m_SpriteDict.ContainsKey(spriteId))
		{
			m_SpriteDict[spriteId].sprite = sprite;
		}
	}

	public void UpdateSpritePivot(string spriteId, float xPivot, float yPivot)
	{
		if (m_SpriteDict.ContainsKey(spriteId))
		{
			m_SpriteDict[spriteId].GetComponent<RectTransform>().pivot = new Vector2(xPivot, yPivot);
		}
	}

	public void UpdateSpriteScreenPos(string spriteId, float xScreenPos, float yScreenPos)
	{
		if (m_SpriteDict.ContainsKey(spriteId))
		{
			m_SpriteDict[spriteId].GetComponent<RectTransform>().anchorMin = new Vector2(xScreenPos, yScreenPos);
			m_SpriteDict[spriteId].GetComponent<RectTransform>().anchorMax = new Vector2(xScreenPos, yScreenPos);
			m_SpriteDict[spriteId].GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
		}
	}

	public void UpdateSpriteColor(string spriteId, string colorStr)
	{
		if (m_SpriteDict.ContainsKey(spriteId))
		{
			m_SpriteDict[spriteId].color = Utils.GetColorFromHexCode(colorStr, Color.white);
		}
	}

	public void CreateButtonObject(string buttonId, string callback, int width, int height)
	{
		if (!m_ButtonDict.ContainsKey(buttonId))
		{
			Button button = Object.Instantiate(m_ButtonPrefab, base.transform);
			button.name = buttonId;
			button.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
			button.gameObject.SetActive(value: true);
			m_ButtonDict.Add(buttonId, button);
			if (!m_ButtonCallbacksDict.ContainsKey(buttonId))
			{
				m_ButtonCallbacksDict.Add(buttonId, callback);
			}
		}
	}

	public void DestroyButtonObject(string buttonId)
	{
		if (m_ButtonDict.ContainsKey(buttonId))
		{
			Object.Destroy(m_ButtonDict[buttonId].gameObject);
			m_ButtonDict.Remove(buttonId);
		}
	}

	public void UpdateButtonCallback(string buttonId, string callback)
	{
		if (m_ButtonCallbacksDict.ContainsKey(buttonId))
		{
			m_ButtonCallbacksDict[buttonId] = callback;
		}
	}

	public void UpdateButtonText(string buttonId, string buttonText)
	{
		if (m_ButtonDict.ContainsKey(buttonId))
		{
			m_ButtonDict[buttonId].GetComponentInChildren<TextMeshProUGUI>().text = buttonText;
		}
	}

	public void UpdateButtonImage(string buttonId, Sprite sprite)
	{
		if (m_ButtonDict.ContainsKey(buttonId))
		{
			m_ButtonDict[buttonId].image.sprite = sprite;
		}
	}

	public void UpdateButtonPivot(string buttonId, float xPivot, float yPivot)
	{
		if (m_ButtonDict.ContainsKey(buttonId))
		{
			m_ButtonDict[buttonId].GetComponent<RectTransform>().pivot = new Vector2(xPivot, yPivot);
		}
	}

	public void UpdateButtonScreenPos(string buttonId, float xScreenPos, float yScreenPos)
	{
		if (m_ButtonDict.ContainsKey(buttonId))
		{
			m_ButtonDict[buttonId].GetComponent<RectTransform>().anchorMin = new Vector2(xScreenPos, yScreenPos);
			m_ButtonDict[buttonId].GetComponent<RectTransform>().anchorMax = new Vector2(xScreenPos, yScreenPos);
			m_ButtonDict[buttonId].GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
		}
	}

	public void UpdateButtonColor(string buttonId, string normalColorStr, string hoverColorStr)
	{
		if (m_ButtonDict.ContainsKey(buttonId))
		{
			m_ButtonDict[buttonId].GetComponent<HighlightOnHover>().m_NormalColor = Utils.GetColorFromHexCode(normalColorStr, Color.white);
			m_ButtonDict[buttonId].GetComponent<HighlightOnHover>().m_HoverColor = Utils.GetColorFromHexCode(hoverColorStr, Color.white);
		}
	}

	public void UpdateButtonTextColor(string buttonId, string colorStr)
	{
		if (m_ButtonDict.ContainsKey(buttonId))
		{
			m_ButtonDict[buttonId].GetComponentInChildren<TextMeshProUGUI>().color = Utils.GetColorFromHexCode(colorStr, Color.white);
		}
	}

	public void UpdateButtonTooltipText(string buttonId, string tooltipStr)
	{
		if (m_ButtonDict.ContainsKey(buttonId))
		{
			if (m_ButtonDict[buttonId].targetGraphic.GetComponent<ToolTipText>() == null)
			{
				m_ButtonDict[buttonId].targetGraphic.gameObject.AddComponent<ToolTipText>();
			}
			m_ButtonDict[buttonId].targetGraphic.GetComponent<ToolTipText>().m_RawLocalizationKey = tooltipStr;
		}
	}

	public void UpdateButtonSetOutlineActive(string buttonId, bool outlineActive)
	{
		if (m_ButtonDict.ContainsKey(buttonId))
		{
			m_ButtonDict[buttonId].transform.GetChild(0).gameObject.SetActive(outlineActive);
		}
	}

	public void UpdateButtonAddHoverScale(string buttonId)
	{
		if (m_ButtonDict.ContainsKey(buttonId) && m_ButtonDict[buttonId].GetComponent<ButtonHoverScale>() == null)
		{
			m_ButtonDict[buttonId].gameObject.AddComponent<ButtonHoverScale>();
		}
	}

	public void UpdateButtonSetInteractable(string buttonId, bool interactable)
	{
		if (m_ButtonDict.ContainsKey(buttonId))
		{
			m_ButtonDict[buttonId].interactable = interactable;
		}
	}
}
