using UnityEngine;
using UnityEngine.UI;

public class WorkshopUI : PineUIComponent
{
	public Canvas root;

	public LevelPickerUI LevelPicker;

	public Transform OpenEditorModal;

	public Image OpenEditorModal_Background;

	public Button OpenEditorModal_Background_no;

	public Text OpenEditorModal_Background_no_Text;

	public Button OpenEditorModal_Background_yes;

	public Text OpenEditorModal_Background_yes_Text;

	public Transform DeleteRoom;

	public Image DeleteRoom_Background;

	public Button DeleteRoom_Background_no;

	public Text DeleteRoom_Background_no_Text;

	public Button DeleteRoom_Background_yes;

	public Text DeleteRoom_Background_yes_Text;

	public Transform CopyRoomModal;

	public Image CopyRoomModal_Background;

	public Button CopyRoomModal_Background_no;

	public Text CopyRoomModal_Background_no_Text;

	public Button CopyRoomModal_Background_yes;

	public Text CopyRoomModal_Background_yes_Text;

	public Transform PlayWorkshopTutorial;

	public Image PlayWorkshopTutorial_Background;

	public Button PlayWorkshopTutorial_Background_no;

	public Text PlayWorkshopTutorial_Background_no_Text;

	public Button PlayWorkshopTutorial_Background_yes;

	public Text PlayWorkshopTutorial_Background_yes_Text;

	public Button DEBUG_DownloadAll;

	public Text DEBUG_DownloadAll_Text;

	public Button DEBUG_DownloadPopular;

	public Text DEBUG_DownloadPopular_Text;

	public Button DEBUG_UnsubscribeAll;

	public Text DEBUG_UnsubscribeAll_Text;

	public Button DEBUG_UnlistBadRooms;

	public Text DEBUG_UnlistBadRooms_Text;

	public Button DEBUG_SubscribeFile;

	public Text DEBUG_SubscribeFile_Text;

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
			PineUI.addButtonListeners(OpenEditorModal_Background_no);
			PineUI.addButtonListeners(OpenEditorModal_Background_yes);
			PineUI.addButtonListeners(DeleteRoom_Background_no);
			PineUI.addButtonListeners(DeleteRoom_Background_yes);
			PineUI.addButtonListeners(CopyRoomModal_Background_no);
			PineUI.addButtonListeners(CopyRoomModal_Background_yes);
			PineUI.addButtonListeners(PlayWorkshopTutorial_Background_no);
			PineUI.addButtonListeners(PlayWorkshopTutorial_Background_yes);
			PineUI.addButtonListeners(DEBUG_DownloadAll);
			PineUI.addButtonListeners(DEBUG_DownloadPopular);
			PineUI.addButtonListeners(DEBUG_UnsubscribeAll);
			PineUI.addButtonListeners(DEBUG_UnlistBadRooms);
			PineUI.addButtonListeners(DEBUG_SubscribeFile);
		}
	}
}
