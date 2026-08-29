using UnityEngine.UI;

public class LuaScriptButtonUI : PineUIComponent
{
	public Button root;

	public Text ScriptName;

	public Text Usages;

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
			PineUI.addButtonListeners(root);
		}
	}
}
