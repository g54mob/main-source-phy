using UnityEngine.UI;

public class InteractionsPropertyUI : PineUIComponent
{
	public Image root;

	public Button Fold;

	public Image Fold_In;

	public Image Fold_Out;

	public Toggle None;

	public Image None_Background;

	public Image None_Checkmark;

	public Text None_Text;

	public Toggle Button;

	public Image Button_Background;

	public Image Button_Checkmark;

	public Text Button_Text;

	public Toggle Animation;

	public Image Animation_Background;

	public Image Animation_Checkmark;

	public Text Animation_Text;

	public Toggle Turnable;

	public Image Turnable_Background;

	public Image Turnable_Checkmark;

	public Text Turnable_Text;

	public Toggle Dial;

	public Image Dial_Background;

	public Image Dial_Checkmark;

	public Text Dial_Text;

	public Toggle Zoomable;

	public Image Zoomable_Background;

	public Image Zoomable_Checkmark;

	public Text Zoomable_Text;

	public Toggle CustomDrag;

	public Image CustomDrag_Background;

	public Image CustomDrag_Checkmark;

	public Text CustomDrag_Text;

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
			PineUI.addToggleListeners(None);
			PineUI.addToggleListeners(Button);
			PineUI.addToggleListeners(Animation);
			PineUI.addToggleListeners(Turnable);
			PineUI.addToggleListeners(Dial);
			PineUI.addToggleListeners(Zoomable);
			PineUI.addToggleListeners(CustomDrag);
		}
	}
}
