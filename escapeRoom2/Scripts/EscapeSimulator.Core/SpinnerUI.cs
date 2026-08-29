using UnityEngine;
using UnityEngine.UI;

public class SpinnerUI : PineUIComponent
{
	public RectTransform root;

	public Image Loading1;

	public Image Loading2;

	public Image Loading3;

	public Image Loading4;

	public Image Gear;

	public Image[] Loading_List;

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
