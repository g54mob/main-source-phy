using UnityEngine;

public class TagButtonGroupUI : PineUIComponent
{
	public RectTransform root;

	public TagButtonUI TagButton;

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
