using UnityEngine;
using UnityEngine.UI;

public class HierarchyItemUI : PineUIComponent
{
	public RectTransform root;

	public LayoutElement Indent;

	public Button Button;

	public Button Button_Collapse;

	public Image Button_Collapse_Image;

	public Text Button_PropName;

	public Image Button_PropIcon_Image;

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
			PineUI.addButtonListeners(Button);
			PineUI.addButtonListeners(Button_Collapse);
		}
	}
}
