using UnityEngine;
using UnityEngine.UI;

public class FrameControlUI : PineUIComponent
{
	public RectTransform root;

	public Button Button;

	public Image Button_KeyboardHint;

	public Text Button_Text;

	public Button ControllerHint;

	public ControllerButtonImage ControllerHint_ControllerHint;

	public Text ControllerHint_Text;

	public Transform Spinner;

	public Text Text;

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
			PineUI.addButtonListeners(ControllerHint);
		}
	}
}
