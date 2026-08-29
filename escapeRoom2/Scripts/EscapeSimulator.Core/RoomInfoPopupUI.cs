using UnityEngine;
using UnityEngine.UI;

public class RoomInfoPopupUI : PineUIComponent
{
	public RoomInfoPopup root;

	public Image Info;

	public Text Info_LevelName;

	public RawImage Info_LevelImage;

	public Text Info_AuthorName;

	public Text Info_DiskSize;

	public UndraggableRect Info_DescriptionScrollView;

	public Image Info_DescriptionScrollView_Viewport;

	public ToggleGroup Info_DescriptionScrollView_Viewport_Content;

	public Text Info_DescriptionScrollView_Viewport_Content_Decription;

	public Image Info_DescriptionScrollView_ScrollbarVertical;

	public Image Info_DescriptionScrollView_ScrollbarVertical_SlidingArea_Handle;

	public Text Info_Difficulty;

	public Text Info_NumOfPlayers;

	public Text Info_Playtime;

	public Text Info_Language;

	public Text Info_Themes;

	public Button Info_BackButton;

	public Image Info_BackControllerHint;

	public Button Info_LevelActionButton_Download;

	public Text Info_LevelActionButton_Download_Text;

	public Text Info_LevelActionButton_Installing;

	public Button Info_LevelActionButton_Play;

	public Text Info_LevelActionButton_Play_Text;

	public Image Info_LevelActionButton_DownloadControllerHint_ImageHint;

	public Text Info_LevelActionButton_DownloadControllerHint_Text;

	public Text Info_LevelActionButton_InstallingController;

	public Image Info_LevelActionButton_PlayControllerHint_HintImage;

	public Text Info_LevelActionButton_PlayControllerHint_Text;

	public Button Info_LevelActionButton_DisabledPlay;

	public Image Info_LevelActionButton_DisabledPlay_Text;

	public RectTransform Info_DeleteControllerHint;

	public RectTransform Info_DownloadControllerHint;

	public RectTransform Info_PlayControllerHint;

	public Button Info_DeleteButton;

	public Button Info_DownloadButton;

	public Button Info_PlayButton;

	public Text Info_DownloadingHint;

	public Image DeleteRoomModal;

	public Button DeleteRoomModal_no;

	public Button DeleteRoomModal_yes;

	public Image DeleteRoomModal_NoControllerHint;

	public Image DeleteRoomModal_YesControllerHint;

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
			PineUI.addButtonListeners(Info_BackButton);
			PineUI.addButtonListeners(Info_LevelActionButton_Download);
			PineUI.addButtonListeners(Info_LevelActionButton_Play);
			PineUI.addButtonListeners(Info_LevelActionButton_DisabledPlay);
			PineUI.addButtonListeners(Info_DeleteButton);
			PineUI.addButtonListeners(Info_DownloadButton);
			PineUI.addButtonListeners(Info_PlayButton);
			PineUI.addButtonListeners(DeleteRoomModal_no);
			PineUI.addButtonListeners(DeleteRoomModal_yes);
		}
	}
}
