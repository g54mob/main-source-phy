using UnityEngine;
using UnityEngine.UI;

public class LevelUI : PineUIComponent
{
	public Button root;

	public RawImage LevelImage;

	public RawImage LevelImageOverlay;

	public RawImage Locked;

	public Image Locked_Image;

	public Image Gradient;

	public Image SelectionFrame;

	public Transform Spinner;

	public Text LevelName;

	public Text LevelTokensCollected;

	public Image TokenIcon;

	public Image LevelFinishIcon;

	public Image CurrentlyOpenedInEditor;

	public Image LevelTrophyIcon;

	public Image CustomLevelDotIcon;

	public Image CustomLevelNewIcon;

	public Image CustomLevelFinishIcon;

	public Image CustomLevelDownloadedIcon;

	public Image RoundedOverlay;

	public Transform Installing;

	public Slider Installing_InstallingSlider;

	public Image Installing_InstallingSlider_Background;

	public Image Installing_InstallingSlider_FillArea_Fill;

	public Text Installing_InstallingTxt;

	public Button NotInstalled;

	public Text NotInstalled_InstallingTxt;

	public Button Unsubscribe;

	public Image Unsubscribe_Text;

	public Text SaveDate;

	public Button DownloadLevelButton;

	public Image DownloadLevelButton_DownloadIcon;

	public Text PlayTime;

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
			PineUI.addButtonListeners(root);
			PineUI.addSliderListeners(Installing_InstallingSlider);
			PineUI.addButtonListeners(NotInstalled);
			PineUI.addButtonListeners(Unsubscribe);
			PineUI.addButtonListeners(DownloadLevelButton);
		}
	}
}
