using UnityEngine;
using UnityEngine.UI;

public class TexturePropertyOldUI : PineUIComponent
{
	public RectTransform root;

	public Button Background;

	public RawImage Texture;

	public Button Revert;

	public Image Revert_Icon;

	public Button Export;

	public Image Export_Icon;

	public Button Edit;

	public Image Edit_Icon;

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
