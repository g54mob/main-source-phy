using UnityEngine;

public class ContentCatagoryUI : MonoBehaviour
{
	public LocalizeText text;

	private string m_categoryName;

	public string CategoryName => m_categoryName;

	public void Initialize(string categoryName)
	{
		m_categoryName = categoryName;
		text.LocaleID = categoryName;
	}

	public int GetChildIndex()
	{
		return base.transform.childCount + 1;
	}
}
