using UnityEngine;
using UnityEngine.UI;

public class EditorMenuUI : PineUIComponent
{
	public Canvas root;

	public Button Background;

	public Button ExitSave;

	public Text ExitSave_Text;

	public Button Close;

	public Text Close_Text;

	public Button StringInputWarning;

	public Image StringInputWarning_Black;

	public Image StringInputWarning_Header;

	public Text StringInputWarning_Header_Title;

	public Text StringInputWarning_LoadRoomMissingCustomModelsText;

	public Image StringInputWarning_Value;

	public InputField StringInputWarning_Value_Input;

	public Text StringInputWarning_Value_Input_Text;

	public Button StringInputWarning_OK;

	public Text StringInputWarning_OK_Text;

	public Button StringInputWarning_CancelRight;

	public Text StringInputWarning_CancelRight_Text;

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
			PineUI.addButtonListeners(ExitSave);
			PineUI.addButtonListeners(Close);
			PineUI.addButtonListeners(StringInputWarning);
			PineUI.addButtonListeners(StringInputWarning_OK);
			PineUI.addButtonListeners(StringInputWarning_CancelRight);
		}
	}
}
