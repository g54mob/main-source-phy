using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class PanelResizeHorizontal : MonoBehaviour
{
	public RectTransform rectTransform;

	public HorizontalLayoutGroup m_HorizontalLayoutGroup;

	public TextMeshProUGUI[] m_SecondaryText;

	public int m_MinWidth;

	public void OnEnable()
	{
		Update();
	}

	private void Update()
	{
		float num = 0f;
		int num2 = 0;
		for (int i = 0; i < m_HorizontalLayoutGroup.transform.childCount; i++)
		{
			Transform child = m_HorizontalLayoutGroup.transform.GetChild(i);
			bool flag = false;
			LayoutElement component = child.GetComponent<LayoutElement>();
			if (component != null && component.ignoreLayout)
			{
				flag = true;
			}
			if (child.gameObject.activeSelf && !flag)
			{
				TextMeshProUGUI component2 = child.GetComponent<TextMeshProUGUI>();
				if (component != null)
				{
					num += component.preferredWidth;
				}
				else if (component2 != null)
				{
					component2.ForceMeshUpdate();
					num += component2.renderedWidth;
				}
				else
				{
					num += child.GetComponent<RectTransform>().sizeDelta.x;
				}
				num2++;
			}
		}
		float b = (float)(m_HorizontalLayoutGroup.padding.left + m_HorizontalLayoutGroup.padding.right) + num + m_HorizontalLayoutGroup.spacing * (float)(num2 - 1);
		if (m_SecondaryText != null)
		{
			TextMeshProUGUI[] secondaryText = m_SecondaryText;
			foreach (TextMeshProUGUI textMeshProUGUI in secondaryText)
			{
				if (textMeshProUGUI != null)
				{
					b = Mathf.Max(textMeshProUGUI.renderedWidth, b);
				}
			}
		}
		rectTransform.sizeDelta = new Vector2(Mathf.Max(m_MinWidth, b), rectTransform.sizeDelta.y);
	}

	public void ForceUpdate()
	{
		TextMeshProUGUI[] secondaryText = m_SecondaryText;
		foreach (TextMeshProUGUI textMeshProUGUI in secondaryText)
		{
			if (textMeshProUGUI != null)
			{
				textMeshProUGUI.ForceMeshUpdate();
			}
		}
		Update();
		LayoutRebuilder.ForceRebuildLayoutImmediate(base.transform.GetComponent<RectTransform>());
	}
}
