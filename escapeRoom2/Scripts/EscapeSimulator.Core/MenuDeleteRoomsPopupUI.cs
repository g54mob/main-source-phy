using UnityEngine;
using UnityEngine.UI;

public class MenuDeleteRoomsPopupUI : PineUIComponent
{
	public Button root;

	public Text title;

	public Text description;

	public DeleteLevelUI DeleteLevelUI;

	public Button BackButton;

	public Slider TotalSpaceSlider;

	public Button AcceptButton;

	public RectTransform AcceptControllerHint;

	public Image AcceptControllerHint_NotInteractable;

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
			PineUI.addButtonListeners(root);
			PineUI.addButtonListeners(BackButton);
			PineUI.addSliderListeners(TotalSpaceSlider);
			PineUI.addButtonListeners(AcceptButton);
		}
	}
}
