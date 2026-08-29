using UnityEngine;
using UnityEngine.UI;

public class SkyTexturePropertyUI : PineUIComponent
{
	public RectTransform root;

	public Button Label;

	public Text Label_Text;

	public Button TexButton;

	public RawImage Texture;

	public Button Revert;

	public Image Revert_Icon;

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
			PineUI.addButtonListeners(Label);
		}
	}
}
