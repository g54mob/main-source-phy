using TMPro;
using UnityEngine;

public class DropdownObjectSelector : MonoBehaviour
{
	public RectTransform m_Content;

	public RectTransform m_ItemPrefab;

	private void Start()
	{
		TMP_Dropdown componentInParent = GetComponentInParent<TMP_Dropdown>();
		if ((bool)componentInParent && (bool)m_Content && componentInParent.value != -1)
		{
			m_Content.anchoredPosition = new Vector2(0f, (float)componentInParent.value * m_ItemPrefab.sizeDelta.y);
		}
	}
}
