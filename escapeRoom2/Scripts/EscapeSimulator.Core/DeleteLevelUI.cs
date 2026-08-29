using UnityEngine.UI;

public class DeleteLevelUI : PineUIComponent
{
	public Toggle root;

	public RawImage LevelImage;

	public Image Gradient;

	public Text LevelName;

	public Image LevelTrophyIcon;

	public Image CustomLevelDotIcon;

	public Image CustomLevelFinishIcon;

	public Image SelectionFrame;

	public Button Marked;

	public Image RoundedOverlay;

	public Text FileSize;

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
			PineUI.addButtonListeners(Marked);
		}
	}
}
