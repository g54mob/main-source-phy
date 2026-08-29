using UnityEngine;
using UnityEngine.UI;

public class PasswordHintPropertyUI : PineUIComponent
{
	public RectTransform root;

	public Text Text;

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
