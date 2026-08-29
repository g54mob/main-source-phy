using UnityEngine;
using UnityEngine.UI;

public class HintPropertyUI : PineUIComponent
{
	public RectTransform root;

	public Text Text;

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
