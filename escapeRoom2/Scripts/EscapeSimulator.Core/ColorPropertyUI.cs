using UnityEngine;
using UnityEngine.UI;

public class ColorPropertyUI : PineUIComponent
{
	public RectTransform root;

	public Button Label;

	public Text Label_Text;

	public Button Button;

	public Image Button_Checkerboard;

	public Image Button_Color;

	public Image Button_Color_Transparency;

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
