using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : PineUIComponent
{
	public Canvas root;

	public Button Background;

	public Text Title;

	public Image Spinner_Spinner;

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
