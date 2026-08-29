using UnityEngine;
using UnityEngine.UI;

public class TogglePropertyUI : PineUIComponent
{
	public RectTransform root;

	public Button Label;

	public Text Label_Text;

	public Toggle Toggle;

	public Image Toggle_Background;

	public Image Toggle_Background_Image;

	public Image Toggle_Background_Checkmark;

	public Image Toggle_Background_Semicheck;

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
