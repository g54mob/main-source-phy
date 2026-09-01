using System;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardLevelDot : MonoBehaviour
{
	public Button m_Button;

	public Image m_Image;

	private Action<string> m_Callback;

	[NonSerialized]
	public string m_LevelID;

	private void Start()
	{
		m_Button.onClick.AddListener(OnClick);
	}

	public void SetCallback(Action<string> callback, string levelID)
	{
		m_Callback = callback;
		m_LevelID = levelID;
	}

	private void OnClick()
	{
		m_Callback?.Invoke(m_LevelID);
	}
}
