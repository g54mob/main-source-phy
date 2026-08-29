using UnityEngine;
using UnityEngine.UI;

public class PasswordIndexPickerUI : PineUIComponent
{
	public RectTransform root;

	public Button Background;

	public Image ScrollRect;

	public RectTransform Content;

	public PasswordButtonUI PasswordButton;

	public Image Header;

	public Text Header_Text;

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
