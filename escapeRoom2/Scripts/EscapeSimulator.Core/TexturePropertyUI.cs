using UnityEngine;
using UnityEngine.UI;

public class TexturePropertyUI : PineUIComponent
{
	public RectTransform root;

	public Button Label;

	public Text Label_Text;

	public Button TexturePreview;

	public Button TextureEdit;

	public Button TextureRevert;

	public Text TextureName;

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
			PineUI.addButtonListeners(TexturePreview);
			PineUI.addButtonListeners(TextureEdit);
			PineUI.addButtonListeners(TextureRevert);
		}
	}
}
