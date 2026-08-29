using UnityEngine;
using UnityEngine.UI;

public class EditorFadeUI : PineUIComponent
{
	public Canvas root;

	public Button Background;

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
