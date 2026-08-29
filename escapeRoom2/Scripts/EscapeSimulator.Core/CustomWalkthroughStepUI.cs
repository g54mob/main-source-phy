using UnityEngine.UI;

public class CustomWalkthroughStepUI : PineUIComponent
{
	public Toggle root;

	public Image StepTitle;

	public Text StepTitle_Text;

	public InputField StepTitle_InputField;

	public Text StepTitle_InputField_Placeholder;

	public Text StepTitle_InputField_Text;

	public Button DeleteButton;

	public Image DeleteButton_Trashcan;

	public Image DeleteButton_RedTrashcan;

	public CustomWalkthroughSingleStepUI CustomWalkthroughSingleStepUI;

	public Button AddStepButton;

	public Image AddStepButton_StepNumber;

	public Text AddStepButton_StepNumber_Text;

	public Text AddStepButton_StepText;

	public Image AddStepButton_SelectedHighlight;

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
			PineUI.addButtonListeners(AddStepButton);
		}
	}
}
