using UnityEngine;

public class TexArrayPropertyUI : PineUIComponent
{
	public RectTransform root;

	public TexturePropertyOldUI TexturePropertyOld;

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
