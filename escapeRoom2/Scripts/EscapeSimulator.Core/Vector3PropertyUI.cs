using UnityEngine;
using UnityEngine.UI;

public class Vector3PropertyUI : PineUIComponent
{
	public RectTransform root;

	public Button Label;

	public Text Label_Text;

	public Image XValue;

	public Button XValue_Drag;

	public Image XValue_Drag_Icon;

	public InputField XValue_Input;

	public Text XValue_Input_Text;

	public Image YValue;

	public Button YValue_Drag;

	public Image YValue_Drag_Icon;

	public InputField YValue_Input;

	public Text YValue_Input_Text;

	public Image ZValue;

	public Button ZValue_Drag;

	public Image ZValue_Drag_Icon;

	public InputField ZValue_Input;

	public Text ZValue_Input_Text;

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
