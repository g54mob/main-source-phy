using UnityEngine;

public class CustomLevelLoader : MonoBehaviour
{
	public GameStarter gameStarter;

	public ulong debugLoadLevelId;

	public static CustomLevelLoaderData data = new CustomLevelLoaderData();

	public static string lastPineWorkshopLevelName;

	private static string t;

	public LevelContainerEditor container;

	private WorkshopRoom room;

	private LoadRoomResult loadResult;

	private bool isMultiplayer;

	private UnpackedCustomRoom customRoomData;

	private LoadingCanvas.EndCondition loadingCondition;

	private float checkLoadingCustomModelsTime;

	public static string pineWorkshopLevelName
	{
		get
		{
			return t;
		}
		set
		{
			lastPineWorkshopLevelName = t;
			t = value;
			Debug.Log("PWLN: " + lastPineWorkshopLevelName + " | " + t);
		}
	}

	public static void initMenuData(string path, ulong id, string creator)
	{
		data.path = path;
		data.roomId = id;
		data.creator = creator;
		data.room = null;
	}

	private void Start()
	{
		RoomEditor.isUsingRoomEditor = false;
		loadingCondition = LoadingCanvas.get().enqueueEndCondition(Localization.lookupInDictionary("loadingCustomLevel"), LoadingCanvas.get().getTopEndCondition().title);
		isMultiplayer = Net.getSession() != null && Net.getSession().players.Count >= 2;
		Debug.Log(Menu.downloadedRoom);
		if (Menu.downloadedRoom != null)
		{
			customRoomData = Menu.downloadedRoom;
			Menu.downloadedRoom = null;
		}
		else if (!string.IsNullOrEmpty(pineWorkshopLevelName))
		{
			Debug.Log("Starting reading pine workshop level " + pineWorkshopLevelName);
			CustomRoomsLocalRepository customRoomsLocalRepository = CustomRoomsLocalRepository.get();
			customRoomData = customRoomsLocalRepository.readCustomRoom(pineWorkshopLevelName);
			pineWorkshopLevelName = null;
		}
		(room, loadResult) = RoomEditor.startLoadingWorkshopRoom(container, data.path, customRoomData);
		if (loadResult != LoadRoomResult.Success)
		{
			Debug.LogError("Error loading workshop room!");
			Menu.showRoomEditorOpenError = loadResult;
			if (isMultiplayer)
			{
				Net.getSession().send(new FailedLoadRoomEditorRoomPacket(), allowSendInMessageResponse: true);
				Net.getSession().leaveLobby();
			}
			PineSceneManager.loadScene("Lobby1", data.title, "", null);
		}
		else
		{
			Debug.Log("Loaded workshop room " + data.roomId);
			data.hideItemNameplates = room.roomData.hideItemNameplates;
			data.hidePlayerNameplates = room.roomData.hidePlayerNameplates;
			data.useNonLegacyFloorColliders = room.roomData.useNonLegacyFloorColliders;
			data.useProximityChat = room.roomData.useProximityChat;
			data.title = room.roomData.name;
			data.walkthrough = room.roomData.walkthrough;
		}
		customRoomData = null;
	}

	private void Update()
	{
		if (loadResult != LoadRoomResult.Success)
		{
			return;
		}
		if (room.roomData != null)
		{
			checkLoadingCustomModelsTime -= Time.deltaTime;
			if (checkLoadingCustomModelsTime < 0f)
			{
				checkLoadingCustomModelsTime = 1f;
				RoomEditor.updateLoadingScreenCustomModelNumbers(loadingCondition, room.context, Localization.lookupInDictionary("RoomEditor_LoadingCustomModels"));
			}
		}
		if (room.roomData != null && RoomEditor.updateLoadWorkshopRoomCustomModels(room.context))
		{
			RoomEditor.finishLoadingLevel(container, room);
			room.roomData = null;
			LoadingCanvas.get().changeHint(loadingCondition, Localization.lookupInDictionary("loadingCustomLevel"));
		}
		else if (room.roomData == null && RoomEditor.updateLoadWorkshopRoomAssets(room.context))
		{
			PineFmod.killSoundsInBus(PineFmod.getBus("bus:/Sound Effects"));
			PineFmod.killSoundsInBus(PineFmod.getBus("bus:/Music"));
			data.room = room;
			gameStarter.customRoomData = data;
			gameStarter.gameObject.SetActive(value: true);
			base.gameObject.SetActive(value: false);
			loadingCondition.done = true;
			data.room = null;
		}
	}

	private void OnDestroy()
	{
		if (loadResult == LoadRoomResult.Success && room.context.assetsBundles != null && room.context.assetsBundles != null)
		{
			foreach (AssetBundle assetsBundle in room.context.assetsBundles)
			{
				assetsBundle.Unload(unloadAllLoadedObjects: true);
			}
		}
		if (loadResult == LoadRoomResult.Success && room.context.assetsIconsBundle != null)
		{
			room.context.assetsIconsBundle.Unload(unloadAllLoadedObjects: true);
		}
	}
}
