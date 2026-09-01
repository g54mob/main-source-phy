using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class SandboxPanelResizer : MonoBehaviour
{
	public RectTransform rectTransform;

	public VerticalLayoutGroup m_VerticalLayoutGroup;

	public void OnEnable()
	{
		Update();
	}

	public void ForceUpdate()
	{
		Update();
		LayoutRebuilder.ForceRebuildLayoutImmediate(m_VerticalLayoutGroup.GetComponent<RectTransform>());
	}

	private void Update()
	{
		float num = 0f;
		int num2 = 0;
		for (int i = 0; i < m_VerticalLayoutGroup.transform.childCount; i++)
		{
			Transform child = m_VerticalLayoutGroup.transform.GetChild(i);
			bool flag = false;
			LayoutElement component = child.GetComponent<LayoutElement>();
			if (component != null && component.ignoreLayout)
			{
				flag = true;
			}
			if (child.gameObject.activeInHierarchy && !flag)
			{
				num += child.GetComponent<RectTransform>().sizeDelta.y;
				num2++;
			}
		}
		float y = (float)(m_VerticalLayoutGroup.padding.top + m_VerticalLayoutGroup.padding.bottom) + num + m_VerticalLayoutGroup.spacing * (float)(num2 - 1);
		rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, y);
	}
}
