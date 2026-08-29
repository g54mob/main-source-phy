using UnityEngine;
using UnityEngine.UI;

public class SecretPopupUI : PineUIComponent
{
	public Canvas root;

	public Image Background;

	public Image Image;

	public CanvasGroup Controls;

	public Button Controls_Wishlist;

	public ControllerButtonImage Controls_Wishlist_BackControllerHint;

	public Text Controls_Wishlist_Text;

	public Button Controls_Later;

	public Text Controls_Later_Text;

	public ControllerButtonImage Controls_Later_Text_BackControllerHint;

	public Image Controls_Later_Text_Underline;

	public Button Controls_Close;

	public Text Controls_Close_Text;

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
			PineUI.addButtonListeners(Controls_Wishlist);
			PineUI.addButtonListeners(Controls_Later);
			PineUI.addButtonListeners(Controls_Close);
		}
	}
}
