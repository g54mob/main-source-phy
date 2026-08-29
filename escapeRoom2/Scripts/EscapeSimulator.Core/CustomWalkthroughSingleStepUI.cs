using UnityEngine.UI;

public class CustomWalkthroughSingleStepUI : PineUIComponent
{
	public Toggle root;

	public Image StepNumber;

	public Text StepNumber_Text;

	public Text StepText;

	public InputField InputField;

	public Text InputField_Placeholder;

	public Text InputField_StepText;

	public Button DeleteButton;

	public Button DeleteButton_RedConfirm;

	public Image DeleteButton_Trashcan;

	public Image HideText;

	public Image HideText_Image;

	public Image SelectedHighlight;

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
			PineUI.addToggleListeners(root);
			PineUI.addButtonListeners(DeleteButton);
			PineUI.addButtonListeners(DeleteButton_RedConfirm);
		}
	}
}
