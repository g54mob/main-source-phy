using UnityEngine;
using UnityEngine.UI;

public class TargetArrayElementUI : PineUIComponent
{
	public RectTransform root;

	public Button Button;

	public RawImage Button_Icon;

	public Button Remove;

	public Image Remove_Icon;

	public Image IndexLabel;

	public Text IndexLabel_Text;

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
			PineUI.addButtonListeners(Button);
			PineUI.addButtonListeners(Remove);
		}
	}
}
