using UnityEngine;
using UnityEngine.UI;

public class TargetArrayPropertyUI : PineUIComponent
{
	public RectTransform root;

	public Button Label;

	public Text Label_Text;

	public RectTransform TargetArrayAdd;

	public Button TargetArrayAdd_Button;

	public Image TargetArrayAdd_Button_Icon;

	public TargetArrayElementUI TargetArrayElement;

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
