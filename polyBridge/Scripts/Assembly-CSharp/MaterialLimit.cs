using TMPro;
using UnityEngine;

public class MaterialLimit : MonoBehaviour
{
	public TextMeshProUGUI m_Text;

	private int m_LastSetLimit = int.MaxValue;

	public void OnEnable()
	{
		if (GetComponent<RectTransform>().sizeDelta.x < 20f && Game.IsRunningOnSteamDeck())
		{
			GetComponent<RectTransform>().sizeDelta = new Vector2(16f, 16f);
			m_Text.fontSizeMax = 12f;
		}
	}

	public void Set(int limit)
	{
		if (limit != m_LastSetLimit)
		{
			m_Text.text = limit.ToString();
			m_LastSetLimit = limit;
		}
	}
}
