using UnityEngine;
using UnityEngine.UI;

public class MaterialEditorUI : PineUIComponent
{
	public Canvas root;

	public Button Background;

	public Image Header;

	public Text Header_Title;

	public Button Header_QuitButton;

	public RectTransform Properties;

	public Button DuplicateButton;

	public Text DuplicateButton_Text;

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
			PineUI.addButtonListeners(Header_QuitButton);
			PineUI.addButtonListeners(DuplicateButton);
		}
	}
}
