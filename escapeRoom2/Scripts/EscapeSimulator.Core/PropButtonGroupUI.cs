using UnityEngine;

public class PropButtonGroupUI : PineUIComponent
{
	public RectTransform root;

	public PropButtonUI PropButton;

	public PropButtonUI PropButtonLogic;

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
