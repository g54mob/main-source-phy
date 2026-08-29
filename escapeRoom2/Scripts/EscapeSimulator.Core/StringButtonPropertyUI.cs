using UnityEngine;
using UnityEngine.UI;

public class StringButtonPropertyUI : PineUIComponent
{
	public RectTransform root;

	public Button Label;

	public Text Label_Text;

	public Image Value;

	public InputField Value_Input;

	public Text Value_Input_Text;

	public Button Button;

	public Image Button_Icon;

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
