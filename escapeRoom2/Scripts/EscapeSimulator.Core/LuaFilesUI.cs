using UnityEngine.UI;

public class LuaFilesUI : PineUIComponent
{
	public Image root;

	public LuaScriptButtonUI Container_LuaScriptButton;

	public Button Container_AddFileButton;

	public Text Container_AddFileButton_Text;

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
			PineUI.addButtonListeners(Container_AddFileButton);
		}
	}
}
