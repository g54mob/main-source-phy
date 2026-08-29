using UnityEngine;
using UnityEngine.UI;

public class FrameUI : PineUIComponent
{
	public FrameController root;

	public VisualControlUI FrameControl;

	public Transform BotRight;

	public Transform BotLeft;

	public Text Title;

	public Transform TopRight;

	public Transform TopLeft;

	public Text Hint;

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
