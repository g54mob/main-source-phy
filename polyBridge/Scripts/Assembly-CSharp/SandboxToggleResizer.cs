using UnityEngine;

[ExecuteAlways]
public class SandboxToggleResizer : MonoBehaviour
{
	public RectTransform m_RectTransform;

	public RectTransform m_LabelRectTransform;

	private static float MIN_HEIGHT = 20f;

	public void OnEnable()
	{
		Update();
	}

	public void Update()
	{
		m_RectTransform.sizeDelta = new Vector2(m_RectTransform.sizeDelta.x, Mathf.Max(MIN_HEIGHT, m_LabelRectTransform.sizeDelta.y));
	}
}
