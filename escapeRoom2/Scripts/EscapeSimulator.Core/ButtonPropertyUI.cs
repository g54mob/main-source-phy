using UnityEngine;
using UnityEngine.UI;

public class ButtonPropertyUI : PineUIComponent
{
	public RectTransform root;

	public Button Button;

	public Text Button_Text;

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
