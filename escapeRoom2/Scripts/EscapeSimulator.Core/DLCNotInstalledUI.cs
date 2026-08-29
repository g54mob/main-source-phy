using UnityEngine.UI;

public class DLCNotInstalledUI : PineUIComponent
{
	public Toggle root;

	public RawImage DLCImage;

	public Image Gradient;

	public Text DLCName;

	public Image RoundedOverlay;

	public Image SelectionFrame;

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
			PineUI.addToggleListeners(root);
		}
	}
}
