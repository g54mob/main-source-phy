using UnityEngine;
using UnityEngine.UI;

public class SoundPropertyUI : PineUIComponent
{
	public RectTransform root;

	public Image Label;

	public Text Label_Text;

	public Button Value;

	public Image Value_TextMask;

	public Text Value_TextMask_Text;

	public Image Value_Icon;

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
