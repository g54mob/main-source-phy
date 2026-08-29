using UnityEngine;
using UnityEngine.UI;

public class RoomToolUI : PineUIComponent
{
	public Canvas root;

	public RectTransform Icons;

	public Button Plus;

	public Image Plus_Icon;

	public Button Delete;

	public Image Delete_Icon;

	public Button WallTexture;

	public RawImage WallTexture_Icon;

	public Button FloorTexture;

	public RawImage FloorTexture_Icon;

	public Button CeilingTexture;

	public RawImage CeilingTexture_Icon;

	public Image Background;

	public Button Home;

	public Image Home_Icon;

	public Button Paint;

	public Image Paint_Icon;

	public Image PropsContent;

	public RectTransform Content;

	public RoomToolButtonUI AddRoomButton;

	public RoomToolButtonUI DeleteRoomButton;

	public RectTransform WallsButtons;

	public RoomToolButtonUI WallsButtons_RoomToolButton;

	public RectTransform FloorButtons;

	public RoomToolButtonUI FloorButtons_RoomToolButton;

	public RectTransform CeilingButtons;

	public RoomToolButtonUI CeilingButtons_RoomToolButton;

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
			PineUI.addButtonListeners(Home);
			PineUI.addButtonListeners(Paint);
		}
	}
}
