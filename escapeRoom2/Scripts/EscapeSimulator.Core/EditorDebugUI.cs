using UnityEngine;
using UnityEngine.UI;

public class EditorDebugUI : PineUIComponent
{
	public Canvas root;

	public Image TargetID;

	public Image TargetID_Background;

	public Text TargetID_Text;

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
		}
	}
}
