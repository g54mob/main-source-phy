using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class LabelValueResizer : MonoBehaviour
{
	public RectTransform rectTransform;

	public TextMeshProUGUI[] m_Labels;

	public TextMeshProUGUI[] m_Values;

	public HorizontalLayoutGroup m_ValueHorizontalLayoutGroup;

	public float m_Padding;

	public void OnEnable()
	{
		Update();
	}

	private void Update()
	{
		float num = 0f;
		float num2 = 0f;
		for (int i = 0; i < m_Labels.Length; i++)
		{
			if (m_Labels[i].gameObject.activeInHierarchy && m_Labels[i].preferredWidth > num)
			{
				num = m_Labels[i].preferredWidth;
			}
		}
		for (int j = 0; j < m_Values.Length; j++)
		{
			if (m_Values[j].gameObject.activeInHierarchy && m_Values[j].preferredWidth > num2)
			{
				num2 = m_Values[j].preferredWidth;
			}
		}
		if (m_ValueHorizontalLayoutGroup != null && m_ValueHorizontalLayoutGroup.gameObject.activeInHierarchy)
		{
			num2 = Mathf.Max(num2, m_ValueHorizontalLayoutGroup.preferredWidth);
		}
		rectTransform.offsetMin = new Vector2(0f - (num + m_Padding), rectTransform.offsetMin.y);
		rectTransform.offsetMax = new Vector2(num2 + m_Padding, rectTransform.offsetMax.y);
	}

	public void ForceUpdate()
	{
		Update();
	}
}
