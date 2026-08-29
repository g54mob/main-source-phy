using UnityEngine;
using UnityEngine.UI;

public class ES2FrameUI : PineUIComponent
{
	public ES2FrameController root;

	public Image BG;

	public Transform BotRight;

	public Transform BotCenter;

	public Transform BotLeft;

	public VisualControlUI FrameControl;

	public Image Title;

	public Text Title_Text;

	public Transform Tabs_Parent;

	public RectTransform Tabs_Parent_goLeft;

	public Image Tabs_Parent_goLeft_image;

	public RectTransform Tabs_Parent_goRight;

	public Image Tabs_Parent_goRight_image;

	public Image Tabs_Parent_BotBorder;

	public Image Tabs_Parent_BotBorder_RightGradient;

	public Image Tabs_Parent_BotBorder_LeftGradient;

	public Transform Content;

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
		}
	}
}
