using UnityEngine;
using UnityEngine.UI;

public class EmotesPickerUI : PineUIComponent
{
	public RectTransform root;

	public EmotePickUI Center_EmotePick;

	public Text Center_ItemName;

	public RectTransform Center_Arrow;

	public object data;

	private bool isInitialized;

	protected override void Awake()
	{
		init();
	}

	public void init()
	{
		if (!isInitialized)
		{
			isInitialized = true;
		}
	}
}
