using UnityEngine;
using UnityEngine.UI;

public class EditorMetaUI : PineUIComponent
{
	public Canvas root;

	public Button Background;

	public PublishInfoUI PublishInfo;

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
