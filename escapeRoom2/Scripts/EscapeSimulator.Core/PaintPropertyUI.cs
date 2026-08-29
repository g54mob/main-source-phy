using UnityEngine;
using UnityEngine.UI;

public class PaintPropertyUI : PineUIComponent
{
	public RectTransform root;

	public Button Paint;

	public Text Paint_Text;

	public Button Revert;

	public Text Revert_Text;

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
			PineUI.addButtonListeners(Paint);
			PineUI.addButtonListeners(Revert);
		}
	}
}
