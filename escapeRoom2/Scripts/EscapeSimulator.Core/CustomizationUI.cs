using UnityEngine;
using UnityEngine.UI;

public class CustomizationUI : PineUIComponent
{
	public Canvas root;

	public Button Background;

	public ES2FrameController Frame;

	public Transform JoystickPointer;

	public Image CustomizationContent;

	public Image CustomizationContent_BG;

	public RectTransform CustomizationContent_VariantsPanel;

	public Image CustomizationContent_VariantsPanel_BG;

	public Image CustomizationContent_VariantsPanel_VariantContent;

	public CCCategoryTemplateUI CustomizationContent_VariantsPanel_VariantContent_CCCategoryTemplate;

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
			PineUI.addButtonListeners(Background);
		}
	}
}
