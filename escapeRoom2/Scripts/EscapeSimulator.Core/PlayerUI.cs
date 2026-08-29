using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : PineUIComponent
{
	public RectTransform root;

	public Text Index;

	public RawImage Image;

	public Button Kick;

	public Image username_PlatformIcon;

	public Text username_Name;

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
			PineUI.addButtonListeners(Kick);
		}
	}
}
