using UnityEngine;
using UnityEngine.UI;

public class FinishLevelCanvasUI : PineUIComponent
{
	public Canvas root;

	public Button Background;

	public Image SafeAreas_PoseSelection;

	public Image SafeAreas_PoseSelection_WorkshopModal_TextModal;

	public Text SafeAreas_PoseSelection_WorkshopModal_TextModal_EpisodeNumberText;

	public Text SafeAreas_PoseSelection_WorkshopModal_TextModal_RoomRate;

	public Button SafeAreas_PoseSelection_WorkshopModal_TextModal_RoomRate_ThumbsDown;

	public Image SafeAreas_PoseSelection_WorkshopModal_TextModal_RoomRate_ThumbsDown_ThumbsDownEnabled;

	public Image SafeAreas_PoseSelection_WorkshopModal_TextModal_RoomRate_ThumbsDown_ThumbsDownDisabled;

	public Image SafeAreas_PoseSelection_WorkshopModal_TextModal_RoomRate_ThumbsDown_BtnImg;

	public Button SafeAreas_PoseSelection_WorkshopModal_TextModal_RoomRate_ThumbsUp;

	public Image SafeAreas_PoseSelection_WorkshopModal_TextModal_RoomRate_ThumbsUp_ThumbsUpEnabled;

	public Image SafeAreas_PoseSelection_WorkshopModal_TextModal_RoomRate_ThumbsUp_ThumbsUpDisabled;

	public Image SafeAreas_PoseSelection_WorkshopModal_TextModal_RoomRate_ThumbsUp_BtnImg;

	public Text SafeAreas_PoseSelection_WorkshopModal_TextModal_DescriptionStartText;

	public Text SafeAreas_PoseSelection_WorkshopModal_TextModal_DescriptionFinishText;

	public Image SafeAreas_PoseSelection_LeftPanel;

	public Image SafeAreas_PoseSelection_LeftPanel_Smile2D;

	public Image SafeAreas_PoseSelection_LeftPanel_Smile2D_Image;

	public RectTransform SafeAreas_TopModal;

	public Text SafeAreas_TopModal_TitleText;

	public Image SafeAreas_TopModal_AwardsModal;

	public Image SafeAreas_TopModal_AwardsModal_Sticker_Medal;

	public Text SafeAreas_TopModal_AwardsModal_Sticker_Number;

	public Image SafeAreas_TopModal_AwardsModal_Trophy_Medal;

	public Text SafeAreas_TopModal_AwardsModal_Trophy_Number;

	public Image SafeAreas_TopModal_AwardsModal_Token_Medal;

	public Text SafeAreas_TopModal_AwardsModal_Token_Number;

	public RectTransform SafeAreas_TopModal_AwardsModal_Time;

	public Image SafeAreas_TopModal_AwardsModal_Time_Medal;

	public Text SafeAreas_TopModal_AwardsModal_Time_Number;

	public Image SafeAreas_FinalScreen;

	public Text SafeAreas_FinalScreen_TopModal_TitleText;

	public RawImage SafeAreas_FinalScreen_FinishLevelImageBackground;

	public RawImage SafeAreas_FinalScreen_FinishLevelImageMask;

	public RawImage SafeAreas_FinalScreen_FinishLevelImageMask_FinishLevelImage;

	public Image Flash;

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
			PineUI.addButtonListeners(Background);
			PineUI.addButtonListeners(SafeAreas_PoseSelection_WorkshopModal_TextModal_RoomRate_ThumbsDown);
			PineUI.addButtonListeners(SafeAreas_PoseSelection_WorkshopModal_TextModal_RoomRate_ThumbsUp);
		}
	}
}
