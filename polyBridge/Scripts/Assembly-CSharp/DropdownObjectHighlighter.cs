using UnityEngine;

public class DropdownObjectHighlighter : MonoBehaviour
{
	public GameObject m_Highlight;

	public PointerEvents m_PointerEvents;

	private void Start()
	{
		m_PointerEvents.RegisterOnHoverChangeDelegate(OnHoverChange);
		m_Highlight.SetActive(value: false);
	}

	private void OnHoverChange(bool hover)
	{
		m_Highlight.SetActive(hover);
	}
}
