using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class LobbyLogic : LevelLogic
{
	[Serializable]
	public class PackShots
	{
		public string packName;

		public List<Shot> shots;

		public List<PictureLoadingData> loadingDatas = new List<PictureLoadingData>();
	}

	[Serializable]
	public class Shot
	{
		public GameObject frame;

		public Renderer image;

		public Switch3D cycle;

		public Switch3D trash;

		public TMP_Text counter;
	}

	public class PictureLoadingData
	{
		public Task<byte[]> task;

		public int packIndex;

		public int roomIndex;

		public int imageIndex;
	}

	private class LoadedLevelShot
	{
		public NetPlayerId ownerId;

		public int packIndex;

		public int roomIndex;

		public int imageIndex;

		public Texture2D highQualityTexture;

		public byte[] highQualityTextureBytes;

		public Texture2D lowQualityTexture;

		public byte[] lowQualityTextureBytes;
	}

	public sealed class TokenPuzzleChangePiecesLayerTimer : Timer
	{
		public int tokenPuzzleIndex;

		public override byte getTypeId()
		{
			return 0;
		}

		public TokenPuzzleChangePiecesLayerTimer()
		{
		}

		public TokenPuzzleChangePiecesLayerTimer(int tokenPuzzleIndex)
		{
			this.tokenPuzzleIndex = tokenPuzzleIndex;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in tokenPuzzleIndex, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			tokenPuzzleIndex = reader.ReadInt32();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("tokenPuzzleIndex: " + $"{tokenPuzzleIndex}");
			return stringBuilder.ToString();
		}
	}

	public sealed class CCTeleportTimer : Timer
	{
		public override byte getTypeId()
		{
			return 1;
		}

		public override void writeData(FastBinaryWriter writer)
		{
		}

		public override void readData(FastBinaryReader reader)
		{
		}
	}

	[Serializable]
	public class TokenPuzzle
	{
		public string levelName;

		public Zoomable zoomable;

		public Jigsaw jigsaw;

		public List<JigsawPiece> jigsawPieces;

		public List<MaterialState> jigsawPiecesMS;

		public List<MeshRenderer> jigsawPiecesMR;

		public TweenState frameFade;
	}

	[Serializable]
	public class ExclamationInteractive
	{
		public Interactive interactive;

		public GameObject exclamation;

		[HideInInspector]
		public Vector3 exclamationOGPosition;
	}

	public sealed class LobbyImageListPacket : Packet
	{
		public List<int> packIndexes;

		public List<int> roomIndexes;

		public List<int> imageIndexes;

		public override byte getTypeId()
		{
			return 0;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.WriteList(packIndexes, delegate(FastBinaryWriter w, int e)
			{
				w.Write(in e, default(FastBinaryWriter.ForPrimitives));
			});
			writer.WriteList(roomIndexes, delegate(FastBinaryWriter w, int e)
			{
				w.Write(in e, default(FastBinaryWriter.ForPrimitives));
			});
			writer.WriteList(imageIndexes, delegate(FastBinaryWriter w, int e)
			{
				w.Write(in e, default(FastBinaryWriter.ForPrimitives));
			});
		}

		public override void readData(FastBinaryReader reader)
		{
			packIndexes = reader.ReadList((FastBinaryReader r) => r.ReadInt32());
			roomIndexes = reader.ReadList((FastBinaryReader r) => r.ReadInt32());
			imageIndexes = reader.ReadList((FastBinaryReader r) => r.ReadInt32());
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("packIndexes: " + ToStringHelper.Stringify(packIndexes, (int e) => $"{e}"));
			stringBuilder.AppendLine("roomIndexes: " + ToStringHelper.Stringify(roomIndexes, (int e) => $"{e}"));
			stringBuilder.Append("imageIndexes: " + ToStringHelper.Stringify(imageIndexes, (int e) => $"{e}"));
			return stringBuilder.ToString();
		}
	}

	public sealed class LobbyCurrentImageListPacket : Packet
	{
		public List<int> currentSelectedImages;

		public override byte getTypeId()
		{
			return 1;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.WriteList(currentSelectedImages, delegate(FastBinaryWriter w, int e)
			{
				w.Write(in e, default(FastBinaryWriter.ForPrimitives));
			});
		}

		public override void readData(FastBinaryReader reader)
		{
			currentSelectedImages = reader.ReadList((FastBinaryReader r) => r.ReadInt32());
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("currentSelectedImages: " + ToStringHelper.Stringify(currentSelectedImages, (int e) => $"{e}"));
			return stringBuilder.ToString();
		}
	}

	public sealed class LobbyImageRequestPacket : Packet
	{
		public int packIndex;

		public int roomIndex;

		public int imageIndex;

		public override byte getTypeId()
		{
			return 2;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in packIndex, default(FastBinaryWriter.ForPrimitives));
			writer.Write(in roomIndex, default(FastBinaryWriter.ForPrimitives));
			writer.Write(in imageIndex, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			packIndex = reader.ReadInt32();
			roomIndex = reader.ReadInt32();
			imageIndex = reader.ReadInt32();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("packIndex: " + $"{packIndex}");
			stringBuilder.AppendLine("roomIndex: " + $"{roomIndex}");
			stringBuilder.Append("imageIndex: " + $"{imageIndex}");
			return stringBuilder.ToString();
		}
	}

	public sealed class LobbyImageResponsePacket : Packet
	{
		public int packIndex;

		public int roomIndex;

		public int imageIndex;

		public byte[] imageBytes;

		public override byte getTypeId()
		{
			return 3;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in packIndex, default(FastBinaryWriter.ForPrimitives));
			writer.Write(in roomIndex, default(FastBinaryWriter.ForPrimitives));
			writer.Write(in imageIndex, default(FastBinaryWriter.ForPrimitives));
			writer.WriteByteArray(imageBytes);
		}

		public override void readData(FastBinaryReader reader)
		{
			packIndex = reader.ReadInt32();
			roomIndex = reader.ReadInt32();
			imageIndex = reader.ReadInt32();
			imageBytes = reader.ReadByteArray();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("packIndex: " + $"{packIndex}");
			stringBuilder.AppendLine("roomIndex: " + $"{roomIndex}");
			stringBuilder.AppendLine("imageIndex: " + $"{imageIndex}");
			stringBuilder.Append("imageBytes: " + ToStringHelper.Stringify(imageBytes, (byte e) => $"{e}"));
			return stringBuilder.ToString();
		}
	}

	private class LevelImageId
	{
		public int packIndex;

		public int roomIndex;

		public int imageIndex;
	}

	private static readonly List<LoadedLevelShot> levelFinishedTextures = new List<LoadedLevelShot>();

	private static readonly string[] levelFinishedTexturesSuffixes = new string[5] { "", "_2", "_3", "_4", "_5" };

	private const string levelFinishedTexturesTag = "[Image Loading]";

	public Menu es2Menu;

	public GameObject levelPicker;

	public CharacterPose entrancePose;

	public Zoomable roomsZoomable;

	public Zoomable darkestPuzzlesZoomable;

	public Switch3D darkestPuzzlesLockedSwitch;

	public Switch3D telephoneSwitch;

	[Header("Level Shots")]
	public List<PackShots> packShotsList;

	private HashSet<NetPlayerId> syncedPlayersLastFrame = new HashSet<NetPlayerId>();

	private readonly List<LevelImageId> localLevelFinishedTextures = new List<LevelImageId>();

	private readonly List<LevelImageId> localTexturesToRequestFromHost = new List<LevelImageId>();

	private LevelImageId currentlyRequestedTextureLevelId;

	private readonly List<LobbyImageRequestPacket> clientImageRequests = new List<LobbyImageRequestPacket>();

	private int[] currentSelectedImages = new int[12];

	public GameObject[] packImagesJigsawPieces;

	[Header("Token Puzzles")]
	public List<TokenPuzzle> tokenPuzzles;

	private bool isJigsawPuzzleDirty;

	public List<Sequence> jigsawSolvedSequence;

	public List<Lock> jigsawLocks;

	public List<TweenState> jigsawLockTS;

	[Header("Exclamations")]
	public ExclamationInteractive obeliskExclamationInteractive;

	public List<ExclamationInteractive> jigsawsExclamationInteractives;

	public List<ExclamationInteractive> shotsExclamationInteractives;

	public List<ExclamationInteractive> trophyExclamationInteractives;

	private List<ExclamationInteractive> allExclamationInteractives;

	public float ExclamationDistance = 0.025f;

	public float ExclamationSinMagnitude = 0.5f;

	public float ExclamationSpeed = 4f;

	public float ExclamationMinScale = 0.025f;

	public float ExclamationMaxScale = 0.03f;

	[Header("Trophies")]
	public List<Item> trophyItems;

	[Header("Secret Level")]
	public GameObject hiddingPlane;

	public Slot[] hiddenSlots;

	public Item[] starKeys;

	public TweenState hiddenBookDoor;

	public Slot hiddenBookSlot;

	public TweenState hiddenDoor;

	public GameObject secretRoom;

	public GameObject secretRoomNavmeshObstacle;

	public TextMeshProUGUI secretRoomTimer;

	private DateTime secretRoomTargetDate = new DateTime(2025, 11, 20, 18, 0, 0);

	[Header("Obelisk")]
	public TweenState obeliskFloorDoorTS;

	[Header("Character Customization")]
	public Switch3D mirrorSwitch;

	public CharacterPose mirrorPose;

	private float achievementTimer = -1f;

	public List<CharacterPose> sitPoses;

	[Header("Generated Variables")]
	public Zoomable PiratePuzzleZoom;

	public Zoomable SpacePuzzleZoom;

	public Zoomable DraculaPuzzleZoom;

	public Text PirateTokenCount;

	public Text DraculaTokenCount;

	public Text SpaceTokenCount;

	public CharacterPose initialSittingPose;

	private static string levelFinishedTexturesFolder => Path.Combine(Application.persistentDataPath, "FinishLevelImages");

	public override void onTimerDone(Timer timer)
	{
		if (timer is CCTeleportTimer)
		{
			game.teleportPlayer(mirrorPose.poseMetarig.position, new Vector2(0f, 125f));
		}
		if (!(timer is TokenPuzzleChangePiecesLayerTimer { tokenPuzzleIndex: not -1 } tokenPuzzleChangePiecesLayerTimer) || tokenPuzzleChangePiecesLayerTimer.tokenPuzzleIndex >= tokenPuzzles.Count)
		{
			return;
		}
		foreach (JigsawPiece jigsawPiece in tokenPuzzles[tokenPuzzleChangePiecesLayerTimer.tokenPuzzleIndex].jigsawPieces)
		{
			jigsawPiece.gameObject.layer = LayerMask.NameToLayer("Default");
		}
	}

	public override void onInit()
	{
		es2Menu.init(game, initialSittingPose, roomsZoomable, darkestPuzzlesZoomable);
		init();
		initLoadingLevelFinishedTextures();
		PlayerSave.flush();
		setTokenPiecesVisibility(tokenPuzzles[0], zoomed: false);
		setTokenPiecesVisibility(tokenPuzzles[4], zoomed: false);
		setTokenPiecesVisibility(tokenPuzzles[8], zoomed: false);
	}

	public override void onUnlock(Lock targetLock)
	{
		for (int i = 0; i < jigsawLocks.Count; i++)
		{
			if (targetLock == jigsawLocks[i])
			{
				jigsawLockTS[i].transitionTo("Down");
				hiddingPlane.SetActive(value: false);
			}
		}
	}

	public override void onSlot(Slot slot)
	{
		if (Array.IndexOf(hiddenSlots, slot) != -1)
		{
			bool flag = true;
			for (int i = 0; i < hiddenSlots.Length; i++)
			{
				if (!hiddenSlots[i].isUnlocked)
				{
					flag = false;
				}
			}
			if (flag)
			{
				hiddenBookDoor.transitionTo("Down");
			}
		}
		if (slot == hiddenBookSlot)
		{
			hiddenDoor.transitionTo("Down", 0.5f);
			secretRoom.SetActive(value: true);
			secretRoomNavmeshObstacle.SetActive(value: false);
		}
	}

	public override void onJigsaw(Jigsaw jigsaw, JigsawPiece piece, JigsawEvent jigsawEvent)
	{
		switch (jigsawEvent)
		{
		case JigsawEvent.AllPiecesSnapped:
		{
			for (int i = 0; i < 3; i++)
			{
				if (jigsaw == tokenPuzzles[4 * i].jigsaw)
				{
					jigsawSolvedSequence[i].play();
					game.increaseZoomCounter(tokenPuzzles[4 * i].zoomable);
				}
			}
			break;
		}
		case JigsawEvent.PieceReleased:
		case JigsawEvent.PieceSnapped:
		{
			List<PlayerSave.TokenState> lobbyJigsawTokens = PlayerSave.getProgress().lobbyJigsawTokens;
			int num = 0;
			{
				foreach (TokenPuzzle tokenPuzzle in tokenPuzzles)
				{
					if (jigsaw == tokenPuzzle.jigsaw)
					{
						isJigsawPuzzleDirty = true;
					}
					foreach (JigsawPiece jigsawPiece in tokenPuzzle.jigsawPieces)
					{
						PlayerSave.TokenState tokenState = lobbyJigsawTokens[num];
						tokenState.tokenPosition = jigsawPiece.transform.position;
						tokenState.tokenRotation = jigsawPiece.transform.rotation;
						num++;
					}
				}
				break;
			}
		}
		}
	}

	public override void onSwitch3D(Switch3D targetSwitch, Switch3DEvent switchEvent)
	{
		if (targetSwitch == telephoneSwitch && game.hasAuthority(targetSwitch))
		{
			game.openHostOptions();
			game.saveAchievement("ACHIEVEMENT_CALL_A_FRIEND");
		}
		else if (targetSwitch == mirrorSwitch)
		{
			if (game.hasAuthority(targetSwitch))
			{
				es2Menu.doFade(delegate
				{
					game.handleCustomModeEnter(es2Menu.ccCustomMode);
				});
				game.saveAchievement("ACHIEVEMENT_CHARACTER_CUSTOMIZED");
			}
		}
		else
		{
			if (!game.isHost())
			{
				return;
			}
			for (int num = 0; num < packShotsList.Count; num++)
			{
				for (int num2 = 0; num2 < packShotsList[num].shots.Count; num2++)
				{
					if (targetSwitch == packShotsList[num].shots[num2].cycle && switchEvent == Switch3DEvent.Start)
					{
						int levelCompleteImagesCount = getLevelCompleteImagesCount(num, num2);
						if (levelCompleteImagesCount > 1)
						{
							currentSelectedImages[4 * num + num2] = (currentSelectedImages[4 * num + num2] + 1) % levelCompleteImagesCount;
							setLevelCompleteImage(num, num2);
							game.session.send(new LobbyCurrentImageListPacket
							{
								currentSelectedImages = new List<int>(currentSelectedImages)
							});
						}
					}
					else if (!(targetSwitch == packShotsList[num].shots[num2].trash))
					{
					}
				}
			}
		}
	}

	private int getLevelCompleteImagesCount(int packIndex, int levelIndex)
	{
		int num = 0;
		foreach (LoadedLevelShot levelFinishedTexture in levelFinishedTextures)
		{
			if (!(levelFinishedTexture.ownerId != game.session.hostPlayerId) && levelFinishedTexture.packIndex == packIndex && levelFinishedTexture.roomIndex == levelIndex)
			{
				num++;
			}
		}
		return num;
	}

	private bool setLevelCompleteImage(int packIndex, int roomIndex)
	{
		if (4 * packIndex + roomIndex >= currentSelectedImages.Length)
		{
			return false;
		}
		int num = currentSelectedImages[4 * packIndex + roomIndex];
		if (num == -1)
		{
			return false;
		}
		LoadedLevelShot cachedLevelFinishedTexture = getCachedLevelFinishedTexture(game.session.hostPlayerId, packIndex, roomIndex, num);
		if (cachedLevelFinishedTexture == null)
		{
			return false;
		}
		Texture2D highQualityTexture = cachedLevelFinishedTexture.highQualityTexture;
		PackShots packShots = packShotsList[packIndex];
		packShots.shots[roomIndex].frame.SetActive(value: true);
		packShots.shots[roomIndex].image.material.mainTexture = highQualityTexture;
		packShots.shots[roomIndex].image.material.SetTexture("_EmissiveColorMap", highQualityTexture);
		float num2 = (float)highQualityTexture.width / (float)highQualityTexture.height;
		float num3 = packShots.shots[roomIndex].image.transform.localScale.x / packShots.shots[roomIndex].image.transform.localScale.z;
		Vector2 one = Vector2.one;
		Vector2 zero = Vector2.zero;
		if (num2 > num3)
		{
			one.x = num3 / num2;
			zero.x = 0.5f - one.x / 2f;
		}
		else
		{
			one.y = num2 / num3;
			zero.y = 0.5f - one.y / 2f;
		}
		packShots.shots[roomIndex].image.material.mainTextureScale = one;
		packShots.shots[roomIndex].image.material.mainTextureOffset = zero;
		packShots.shots[roomIndex].image.material.SetTextureScale("_EmissiveColorMap", one);
		packShots.shots[roomIndex].image.material.SetTextureOffset("_EmissiveColorMap", zero);
		return true;
	}

	private void deleteLevelCompleteImage(int packIndex, int levelIndex)
	{
	}

	public override void onZoomEnter(GameObject zoomed)
	{
		int num = allExclamationInteractives.FindIndex((ExclamationInteractive z) => z.interactive.gameObject == zoomed);
		if (num >= 0)
		{
			allExclamationInteractives[num].exclamation.SetActive(value: false);
		}
		for (int num2 = 0; num2 < tokenPuzzles.Count; num2++)
		{
			if (zoomed == tokenPuzzles[num2].zoomable.gameObject)
			{
				setTokenPiecesVisibility(tokenPuzzles[num2], zoomed: true);
				break;
			}
		}
	}

	public override void onZoomLeave(GameObject zoomed)
	{
		for (int i = 0; i < tokenPuzzles.Count; i++)
		{
			if (zoomed == tokenPuzzles[i].zoomable.gameObject)
			{
				setTokenPiecesVisibility(tokenPuzzles[i], zoomed: false);
				break;
			}
		}
	}

	public override void onAddToInventory(Item item)
	{
		int num = allExclamationInteractives.FindIndex((ExclamationInteractive z) => z.interactive.gameObject == item.gameObject);
		if (num >= 0)
		{
			allExclamationInteractives[num].exclamation.SetActive(value: false);
		}
	}

	public override void onUpdate()
	{
		onUpdateAchievement();
		mirrorSwitch.targetable = game.localPlayerData.characterPoseContext.poseState == CharacterPoseState.None;
		foreach (ExclamationInteractive allExclamationInteractive in allExclamationInteractives)
		{
			allExclamationInteractive.exclamation.transform.LookAt(game.getCameraPosition(), Vector3.up);
			allExclamationInteractive.exclamation.transform.position = Vector3.Lerp(allExclamationInteractive.exclamationOGPosition + Vector3.up * ExclamationDistance, allExclamationInteractive.exclamationOGPosition + Vector3.down * ExclamationDistance, ExclamationSinMagnitude * Mathf.Sin(ExclamationSpeed * Time.time) + ExclamationSinMagnitude);
			allExclamationInteractive.exclamation.transform.localScale = Vector3.Lerp(Vector3.one * ExclamationMinScale, Vector3.one * ExclamationMaxScale, ExclamationSinMagnitude * Mathf.Cos(ExclamationSpeed * Time.time + ExclamationSpeed * 0.5f) + ExclamationSinMagnitude);
		}
		bool flag = game.isInTopZoom(DraculaPuzzleZoom.gameObject) || game.isInTopZoom(PiratePuzzleZoom.gameObject) || game.isInTopZoom(SpacePuzzleZoom.gameObject);
		Text draculaTokenCount = DraculaTokenCount;
		Color color = DraculaTokenCount.color;
		float? a = Mathf.Lerp(DraculaTokenCount.color.a, flag ? 0f : 1f, 20f * Time.deltaTime);
		draculaTokenCount.color = color.With(null, null, null, a);
		Text pirateTokenCount = PirateTokenCount;
		Color color2 = PirateTokenCount.color;
		a = Mathf.Lerp(PirateTokenCount.color.a, flag ? 0f : 1f, 20f * Time.deltaTime);
		pirateTokenCount.color = color2.With(null, null, null, a);
		Text spaceTokenCount = SpaceTokenCount;
		Color color3 = SpaceTokenCount.color;
		a = Mathf.Lerp(SpaceTokenCount.color.a, flag ? 0f : 1f, 20f * Time.deltaTime);
		spaceTokenCount.color = color3.With(null, null, null, a);
		if (isJigsawPuzzleDirty)
		{
			PlayerSave.flush();
			isJigsawPuzzleDirty = false;
		}
		foreach (TokenPuzzle tokenPuzzle in tokenPuzzles)
		{
			for (int i = 0; i < tokenPuzzle.jigsawPieces.Count; i++)
			{
				tokenPuzzle.jigsawPiecesMR[i].enabled = tokenPuzzle.jigsawPiecesMS[i].getWeight("Transparent") != 1f;
			}
			tokenPuzzle.frameFade.GetComponent<Renderer>().shadowCastingMode = ((tokenPuzzle.frameFade.getWeight("Fade") < 0.2f) ? ShadowCastingMode.On : ShadowCastingMode.Off);
			tokenPuzzle.frameFade.transform.GetChild(0).GetComponent<Renderer>().shadowCastingMode = ((tokenPuzzle.frameFade.getWeight("Fade") < 0.2f) ? ShadowCastingMode.On : ShadowCastingMode.Off);
		}
		secretRoomTimer.text = GetTimeDifference(secretRoomTargetDate);
		updateLevelShots();
	}

	public static string GetTimeDifference(DateTime futureDate)
	{
		DateTime now = DateTime.Now;
		if (futureDate > now)
		{
			TimeSpan timeSpan = futureDate.Subtract(now);
			return $"{timeSpan.Days:D2}d {timeSpan.Hours:D2}h {timeSpan.Minutes:D2}m {timeSpan.Seconds:D2}s";
		}
		return "00:00:00:00";
	}

	private void initLoadingLevelFinishedTextures()
	{
		for (int i = 0; i < packShotsList.Count; i++)
		{
			PackShots packShots = packShotsList[i];
			for (int j = 0; j < packShots.shots.Count; j++)
			{
				RoomDatabase.Pack pack = RoomDatabase.getPack(packShots.packName);
				if (j >= pack.rooms.Count)
				{
					break;
				}
				currentSelectedImages[4 * i + j] = -1;
				bool flag = false;
				PlayerSave.FinishState finishState = PlayerSave.getFinishState(pack.rooms[j].name);
				if (finishState != PlayerSave.FinishState.Finished && finishState != PlayerSave.FinishState.Trophy)
				{
					continue;
				}
				for (int k = 0; k < levelFinishedTexturesSuffixes.Length; k++)
				{
					string arg = levelFinishedTexturesSuffixes[k];
					string path = $"{packShots.packName}{j + 1}{arg}.png";
					string text = Path.Combine(levelFinishedTexturesFolder, path);
					if (File.Exists(text))
					{
						if (!flag)
						{
							flag = true;
							currentSelectedImages[4 * i + j] = k;
						}
						localLevelFinishedTextures.Add(new LevelImageId
						{
							packIndex = i,
							roomIndex = j,
							imageIndex = k
						});
						if (getCachedLevelFinishedTexture(game.session.localPlayerId, i, j, k) != null)
						{
							Debug.Log(string.Format("{0} Cached texture found | Pack {1}, Level {2}, Image {3}", "[Image Loading]", i, j, k));
						}
						else
						{
							Debug.Log("[Image Loading] Loading level finished image: " + text);
							packShots.loadingDatas.Add(new PictureLoadingData
							{
								task = File.ReadAllBytesAsync(text),
								packIndex = i,
								roomIndex = j,
								imageIndex = k
							});
						}
					}
				}
			}
		}
	}

	private LoadedLevelShot getCachedLevelFinishedTexture(NetPlayerId ownerId, int packIndex, int levelIndex, int imageIndex)
	{
		foreach (LoadedLevelShot levelFinishedTexture in levelFinishedTextures)
		{
			if (!(levelFinishedTexture.ownerId != ownerId) && levelFinishedTexture.packIndex == packIndex && levelFinishedTexture.roomIndex == levelIndex && levelFinishedTexture.imageIndex == imageIndex)
			{
				return levelFinishedTexture;
			}
		}
		return null;
	}

	private void updateLevelShots()
	{
		bool flag = game.isHost() && game.isInTopZoom(shotsExclamationInteractives[0].interactive.gameObject);
		for (int i = 0; i < packShotsList.Count; i++)
		{
			for (int j = 0; j < packShotsList[i].shots.Count; j++)
			{
				int levelCompleteImagesCount = getLevelCompleteImagesCount(i, j);
				packShotsList[i].shots[j].cycle.gameObject.SetActive(flag && levelCompleteImagesCount > 1);
				packShotsList[i].shots[j].trash.gameObject.SetActive(value: false);
				packShotsList[i].shots[j].counter.gameObject.SetActive(flag && levelCompleteImagesCount > 1);
				if (4 * i + j < currentSelectedImages.Length)
				{
					packShotsList[i].shots[j].counter.text = currentSelectedImages[4 * i + j] + 1 + "/" + levelCompleteImagesCount;
				}
				if (!setLevelCompleteImage(i, j))
				{
					packShotsList[i].shots[j].frame.SetActive(value: false);
				}
			}
		}
		if (game.isHost())
		{
			updateLevelShotsLoadingImages();
			updateLevelShotsSyncedPlayers();
			updateLevelShotsResponding();
		}
		else
		{
			updateLevelShotsRequesting();
		}
	}

	private void updateLevelShotsLoadingImages()
	{
		foreach (PackShots packShots in packShotsList)
		{
			for (int num = packShots.loadingDatas.Count - 1; num >= 0; num--)
			{
				PictureLoadingData pictureLoadingData = packShots.loadingDatas[num];
				if (pictureLoadingData.task.IsCompletedSuccessfully)
				{
					loadLobbyShotImage(game.session.localPlayerId, pictureLoadingData.packIndex, pictureLoadingData.roomIndex, pictureLoadingData.imageIndex, pictureLoadingData.task.Result);
					packShots.loadingDatas.RemoveAt(num);
				}
			}
		}
	}

	private void updateLevelShotsSyncedPlayers()
	{
		HashSet<NetPlayerId> hashSet = new HashSet<NetPlayerId>();
		foreach (NetPlayerData player in game.session.players)
		{
			if (game.session.isLocalPlayer(player.id))
			{
				continue;
			}
			if (player.isSynced)
			{
				hashSet.Add(player.id);
			}
			if (syncedPlayersLastFrame.Contains(player.id) || !hashSet.Contains(player.id))
			{
				continue;
			}
			List<int> list = new List<int>();
			List<int> list2 = new List<int>();
			List<int> list3 = new List<int>();
			foreach (LevelImageId localLevelFinishedTexture in localLevelFinishedTextures)
			{
				list.Add(localLevelFinishedTexture.packIndex);
				list2.Add(localLevelFinishedTexture.roomIndex);
				list3.Add(localLevelFinishedTexture.imageIndex);
			}
			game.session.sendToSpecificPlayerFromHost(new LobbyImageListPacket
			{
				packIndexes = list,
				roomIndexes = list2,
				imageIndexes = list3
			}, player.id);
			game.session.sendToSpecificPlayerFromHost(new LobbyCurrentImageListPacket
			{
				currentSelectedImages = new List<int>(currentSelectedImages)
			}, player.id);
		}
		syncedPlayersLastFrame = hashSet;
	}

	private void updateLevelShotsRequesting()
	{
		if (localTexturesToRequestFromHost.Count == 0 || currentlyRequestedTextureLevelId != null)
		{
			return;
		}
		int num = localTexturesToRequestFromHost.Count - 1;
		while (num >= 0)
		{
			LevelImageId levelImageId = localTexturesToRequestFromHost[num];
			if (getCachedLevelFinishedTexture(game.session.hostPlayerId, levelImageId.packIndex, levelImageId.roomIndex, levelImageId.imageIndex) != null)
			{
				Debug.Log(string.Format("{0} Cached Level Image | Pack {1}, Level {2}, Image {3}", "[Image Loading]", levelImageId.packIndex, levelImageId.roomIndex, levelImageId.imageIndex));
				localTexturesToRequestFromHost.RemoveAt(num);
				num--;
				continue;
			}
			localTexturesToRequestFromHost.RemoveAt(num);
			currentlyRequestedTextureLevelId = levelImageId;
			Debug.Log(string.Format("{0} Requesting Level Image | Pack {1}, Level {2}, Image {3}", "[Image Loading]", levelImageId.packIndex, levelImageId.roomIndex, levelImageId.imageIndex));
			game.session.sendToSpecificPlayerFromClient(new LobbyImageRequestPacket
			{
				packIndex = levelImageId.packIndex,
				roomIndex = levelImageId.roomIndex,
				imageIndex = levelImageId.imageIndex
			}, game.session.hostPlayerId);
			break;
		}
	}

	private void updateLevelShotsResponding()
	{
		for (int num = clientImageRequests.Count - 1; num >= 0; num--)
		{
			LobbyImageRequestPacket lobbyImageRequestPacket = clientImageRequests[num];
			LoadedLevelShot cachedLevelFinishedTexture = getCachedLevelFinishedTexture(game.session.localPlayerId, lobbyImageRequestPacket.packIndex, lobbyImageRequestPacket.roomIndex, lobbyImageRequestPacket.imageIndex);
			if (cachedLevelFinishedTexture != null)
			{
				Debug.Log(string.Format("{0} Responding Level Image | Pack {1}, Level {2}, Image {3}", "[Image Loading]", lobbyImageRequestPacket.packIndex, lobbyImageRequestPacket.roomIndex, lobbyImageRequestPacket.imageIndex));
				game.session.sendToSpecificPlayerFromHost(new LobbyImageResponsePacket
				{
					packIndex = lobbyImageRequestPacket.packIndex,
					roomIndex = lobbyImageRequestPacket.roomIndex,
					imageIndex = lobbyImageRequestPacket.imageIndex,
					imageBytes = cachedLevelFinishedTexture.lowQualityTextureBytes
				}, lobbyImageRequestPacket.senderId);
				clientImageRequests.RemoveAt(num);
			}
		}
	}

	private void loadLobbyShotImage(NetPlayerId ownerId, int packIndex, int roomIndex, int imageIndex, byte[] imageBytes)
	{
		if (imageBytes == null)
		{
			return;
		}
		Texture2D texture2D = new Texture2D(2, 2);
		if (texture2D.LoadImage(imageBytes))
		{
			Texture2D texture2D2 = texture2D;
			byte[] lowQualityTextureBytes = imageBytes;
			if (game.session.isLocalPlayer(ownerId))
			{
				texture2D2 = resizeTexture(texture2D, texture2D.width, texture2D.height);
				lowQualityTextureBytes = texture2D2.EncodeToJPG();
			}
			else
			{
				currentlyRequestedTextureLevelId = null;
			}
			if (getCachedLevelFinishedTexture(ownerId, packIndex, roomIndex, imageIndex) == null)
			{
				levelFinishedTextures.Add(new LoadedLevelShot
				{
					ownerId = ownerId,
					packIndex = packIndex,
					roomIndex = roomIndex,
					imageIndex = imageIndex,
					highQualityTexture = texture2D,
					highQualityTextureBytes = imageBytes,
					lowQualityTexture = texture2D2,
					lowQualityTextureBytes = lowQualityTextureBytes
				});
				Debug.Log(string.Format("{0} Loaded Level Image | Pack {1}, Level {2}, Image {3}", "[Image Loading]", packIndex, roomIndex, imageIndex));
			}
		}
	}

	private Texture2D resizeTexture(Texture2D original, int newWidth, int newHeight)
	{
		RenderTexture renderTexture = (RenderTexture.active = new RenderTexture(newWidth, newHeight, 24));
		Graphics.Blit(original, renderTexture);
		Texture2D texture2D = new Texture2D(newWidth, newHeight, TextureFormat.RGB24, mipChain: false);
		texture2D.ReadPixels(new Rect(0f, 0f, newWidth, newHeight), 0, 0);
		texture2D.Apply();
		RenderTexture.active = null;
		renderTexture.Release();
		return texture2D;
	}

	public override void onPacket(Packet packet)
	{
		if (packet is LobbyImageListPacket lobbyImageListPacket)
		{
			int count = lobbyImageListPacket.packIndexes.Count;
			Debug.Log(string.Format("{0} Received {1} with {2} images.", "[Image Loading]", "LobbyImageListPacket", count));
			for (int i = 0; i < count; i++)
			{
				localTexturesToRequestFromHost.Add(new LevelImageId
				{
					packIndex = lobbyImageListPacket.packIndexes[i],
					roomIndex = lobbyImageListPacket.roomIndexes[i],
					imageIndex = lobbyImageListPacket.imageIndexes[i]
				});
			}
		}
		else if (packet is LobbyImageRequestPacket lobbyImageRequestPacket)
		{
			Debug.Log(string.Format("{0} Received {1} | Pack {2}, Level {3}, Image {4}", "[Image Loading]", "LobbyImageRequestPacket", lobbyImageRequestPacket.packIndex, lobbyImageRequestPacket.roomIndex, lobbyImageRequestPacket.imageIndex));
			clientImageRequests.Add(lobbyImageRequestPacket);
		}
		else if (packet is LobbyImageResponsePacket lobbyImageResponsePacket)
		{
			Debug.Log(string.Format("{0} Received {1} | Pack {2}, Level {3}, Image {4}", "[Image Loading]", "LobbyImageResponsePacket", lobbyImageResponsePacket.packIndex, lobbyImageResponsePacket.roomIndex, lobbyImageResponsePacket.imageIndex));
			loadLobbyShotImage(game.session.hostPlayerId, lobbyImageResponsePacket.packIndex, lobbyImageResponsePacket.roomIndex, lobbyImageResponsePacket.imageIndex, lobbyImageResponsePacket.imageBytes);
		}
		else if (packet is LobbyCurrentImageListPacket lobbyCurrentImageListPacket)
		{
			Debug.Log(string.Format("{0} Received {1} | CurrentImages {2}", "[Image Loading]", "LobbyCurrentImageListPacket", lobbyCurrentImageListPacket.currentSelectedImages));
			for (int j = 0; j < currentSelectedImages.Length && j < lobbyCurrentImageListPacket.currentSelectedImages.Count; j++)
			{
				currentSelectedImages[j] = lobbyCurrentImageListPacket.currentSelectedImages[j];
			}
		}
	}

	public override void onPose(NetPlayerId playerId, CharacterPose characterPose, CharacterPoseState poseEvent)
	{
		if (playerId == game.session.localPlayerId && sitPoses.Contains(characterPose))
		{
			switch (poseEvent)
			{
			case CharacterPoseState.TransitionIn:
				achievementTimer = Time.time;
				break;
			case CharacterPoseState.TransitionOut:
				achievementTimer = -1f;
				break;
			}
		}
	}

	private void onUpdateAchievement()
	{
		if (achievementTimer > 0f && Time.time - achievementTimer > 10f)
		{
			achievementTimer = -1f;
			game.saveAchievement("ACHIEVEMENT_RELAX");
		}
	}

	[DebugButton(null, Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void getAllTokenPuzzlePieces()
	{
		foreach (TokenPuzzle tokenPuzzle in tokenPuzzles)
		{
			for (int i = 0; i < tokenPuzzle.jigsawPieces.Count; i++)
			{
				tokenPuzzle.jigsawPieces[i].gameObject.SetActive(value: true);
			}
		}
	}

	private void setTokenPiecesVisibility(TokenPuzzle tokenPuzzle, bool zoomed)
	{
		int num = tokenPuzzles.IndexOf(tokenPuzzle);
		if (num == -1)
		{
			return;
		}
		int num2 = num / 4;
		for (int i = 0; i < tokenPuzzles.Count; i++)
		{
			int num3 = i / 4;
			bool flag = zoomed && num3 != num2;
			tokenPuzzles[i].frameFade.transitionTo(flag ? "Fade" : "Default", 2f);
			for (int j = 0; j < tokenPuzzles[i].jigsawPieces.Count; j++)
			{
				bool flag2 = (!zoomed && tokenPuzzles[i].jigsawPieces[j].isSnapped) || (zoomed && num3 == num2);
				tokenPuzzles[i].jigsawPiecesMS[j].transitionTo(flag2 ? "Default" : "Transparent", 2f);
				if (zoomed && num3 == num2)
				{
					tokenPuzzles[i].jigsawPieces[j].gameObject.layer = LayerMask.NameToLayer("Zoom");
				}
				else
				{
					game.startTimer(new TokenPuzzleChangePiecesLayerTimer(i), 0.2f);
				}
			}
		}
	}

	private bool levelStateChanged(string levelId)
	{
		bool result = false;
		PlayerSave.LevelState levelState = PlayerSave.ensureAndGetLevelState(levelId);
		PlayerSave.FinishState finishState = PlayerSave.getFinishState(levelId);
		if (levelState.lastFinishStateCheckedInMenu != finishState && (levelState.lastFinishStateCheckedInMenu < PlayerSave.FinishState.Finished || finishState < PlayerSave.FinishState.Finished))
		{
			result = true;
		}
		return result;
	}

	private bool levelTokensChanged(string levelId)
	{
		bool result = false;
		PlayerSave.LevelState levelState = PlayerSave.ensureAndGetLevelState(levelId);
		int num = PlayerSave.countCollectedTokens(levelId);
		if (levelState.lastNumberOfTokensCheckedInMenu != num)
		{
			result = true;
		}
		return result;
	}

	private void init()
	{
		List<RoomDatabase.Room> allRooms = RoomDatabase.getAllRooms();
		List<RoomDatabase.Room> allRegularRooms = RoomDatabase.getAllRegularRooms();
		List<RoomDatabase.Room> allDarkestPuzzlesRooms = RoomDatabase.getAllDarkestPuzzlesRooms();
		allExclamationInteractives = new List<ExclamationInteractive>();
		allExclamationInteractives.Add(obeliskExclamationInteractive);
		allExclamationInteractives.AddRange(jigsawsExclamationInteractives);
		allExclamationInteractives.AddRange(shotsExclamationInteractives);
		allExclamationInteractives.AddRange(trophyExclamationInteractives);
		foreach (ExclamationInteractive allExclamationInteractive in allExclamationInteractives)
		{
			allExclamationInteractive.exclamation.SetActive(value: false);
			allExclamationInteractive.exclamationOGPosition = allExclamationInteractive.exclamation.transform.position;
		}
		bool flag = false;
		foreach (RoomDatabase.Room item in allRooms)
		{
			PlayerSave.FinishState finishState = PlayerSave.getFinishState(item.name);
			flag = flag || finishState == PlayerSave.FinishState.Finished || finishState == PlayerSave.FinishState.Trophy;
		}
		flag = true;
		darkestPuzzlesLockedSwitch.targetable = (darkestPuzzlesLockedSwitch.blocksRaycasts = !flag);
		darkestPuzzlesZoomable.targetable = flag;
		bool flag2 = false;
		foreach (RoomDatabase.Room item2 in allRooms)
		{
			flag2 |= levelStateChanged(item2.name);
		}
		if (flag2)
		{
			obeliskExclamationInteractive.exclamation.SetActive(value: true);
		}
		bool flag3 = false;
		foreach (RoomDatabase.Room item3 in allRegularRooms)
		{
			flag3 |= levelStateChanged(item3.name);
		}
		if (flag3)
		{
			shotsExclamationInteractives[0].exclamation.SetActive(value: true);
		}
		bool flag4 = true;
		int num = 0;
		foreach (RoomDatabase.Room item4 in allDarkestPuzzlesRooms)
		{
			if (PlayerSave.getFinishState(item4.name) != PlayerSave.FinishState.Finished)
			{
				flag4 = false;
			}
			else
			{
				num++;
			}
		}
		Debug.Log("Darkest solved count = " + num);
		if (flag4)
		{
			obeliskFloorDoorTS.transitionTo("Down");
			hiddingPlane.SetActive(value: false);
		}
		for (int i = 0; i < tokenPuzzles.Count; i++)
		{
			RoomDatabase.Room room = RoomDatabase.getRoom(tokenPuzzles[i].levelName);
			int num2 = PlayerSave.countCollectedTokens(room.name);
			for (int j = 0; j < tokenPuzzles[i].jigsawPieces.Count; j++)
			{
				tokenPuzzles[i].jigsawPieces[j].gameObject.SetActive(j < num2);
			}
			if (levelTokensChanged(room.name) && i / 4 < jigsawsExclamationInteractives.Count)
			{
				jigsawsExclamationInteractives[i / 4].exclamation.SetActive(value: true);
			}
		}
		int num3 = 32;
		SpaceTokenCount.text = string.Format("T {0}/{1}", PlayerSave.getTokenCountForPack("Space"), num3);
		PirateTokenCount.text = string.Format("T {0}/{1}", PlayerSave.getTokenCountForPack("Pirate"), num3);
		DraculaTokenCount.text = string.Format("T {0}/{1}", PlayerSave.getTokenCountForPack("Dracula"), num3);
		List<PlayerSave.TokenState> lobbyJigsawTokens = PlayerSave.getProgress().lobbyJigsawTokens;
		for (int k = lobbyJigsawTokens.Count; k < 3 * num3; k++)
		{
			lobbyJigsawTokens.Add(new PlayerSave.TokenState
			{
				tokenId = k,
				isTokenInitialized = false,
				tokenPosition = Vector3.zero,
				tokenRotation = Quaternion.identity
			});
		}
		int num4 = 0;
		foreach (TokenPuzzle tokenPuzzle in tokenPuzzles)
		{
			foreach (JigsawPiece jigsawPiece in tokenPuzzle.jigsawPieces)
			{
				PlayerSave.TokenState tokenState = lobbyJigsawTokens[num4];
				if (tokenState.isTokenInitialized)
				{
					jigsawPiece.transform.position = tokenState.tokenPosition;
					jigsawPiece.transform.rotation = tokenState.tokenRotation;
					bool isSnapped = jigsawPiece.isSnapped;
					jigsawPiece.targetable = !isSnapped;
					jigsawPiece.blocksRaycasts = !isSnapped;
				}
				else
				{
					tokenState.tokenPosition = jigsawPiece.transform.position;
					tokenState.tokenRotation = jigsawPiece.transform.rotation;
					tokenState.isTokenInitialized = true;
				}
				num4++;
			}
		}
		for (int l = 0; l < 3; l++)
		{
			if (tokenPuzzles[4 * l].jigsaw.areAllPiecesSnapped())
			{
				jigsawSolvedSequence[l].play();
			}
		}
		foreach (int item5 in PlayerSave.getProgress().starKeyFound)
		{
			if (item5 < starKeys.Length)
			{
				starKeys[item5].gameObject.SetActive(value: true);
				hiddingPlane.SetActive(value: false);
			}
		}
		for (int m = 0; m < 3; m++)
		{
			bool flag5 = true;
			for (int n = 0; n < 4; n++)
			{
				if (PlayerSave.getFinishState(tokenPuzzles[4 * m + n].levelName) != PlayerSave.FinishState.Trophy)
				{
					flag5 = false;
				}
			}
			if (flag5)
			{
				trophyItems[m].gameObject.SetActive(value: true);
			}
		}
		trophyItems[3].gameObject.SetActive(flag4);
		string[] array = new string[3] { "Dracula", "Space", "Pirate" };
		bool active = true;
		for (int num5 = 0; num5 < array.Length; num5++)
		{
			if (PlayerSave.getTokenCountForPack(array[num5]) != 32)
			{
				active = false;
			}
		}
		trophyItems[4].gameObject.SetActive(active);
		foreach (RoomDatabase.Room item6 in allRooms)
		{
			PlayerSave.LevelState levelState = PlayerSave.ensureAndGetLevelState(item6.name);
			levelState.lastFinishStateCheckedInMenu = PlayerSave.getFinishState(item6.name);
			levelState.lastNumberOfTokensCheckedInMenu = PlayerSave.countCollectedTokens(item6.name);
		}
	}

	[DebugButton(null, Tint.Default, (PostClickAction)0, 0, new object[] { }, label = "Unlock Obelisk Floor Door", postClickAction = PostClickAction.HideButton)]
	private void DebugUnlockObeliskFloorDoor()
	{
		obeliskFloorDoorTS.transitionTo("Down");
		hiddingPlane.SetActive(value: false);
	}

	[DebugButton(null, Tint.Default, (PostClickAction)0, 0, new object[] { }, label = "Solve Jigsaws", postClickAction = PostClickAction.HideButton)]
	private void DebugSolveJigsaws()
	{
		for (int i = 0; i < 3; i++)
		{
			jigsawSolvedSequence[i].play();
		}
	}

	[DebugButton(null, Tint.Default, (PostClickAction)0, 0, new object[] { }, label = "Solve Jigsaw Locks", postClickAction = PostClickAction.HideButton)]
	private void DebugSolveJigsawLocks()
	{
		for (int i = 0; i < 3; i++)
		{
			jigsawSolvedSequence[i].play();
			jigsawLockTS[i].transitionTo("Down");
		}
		hiddingPlane.SetActive(value: false);
	}

	[DebugButton(null, Tint.Default, (PostClickAction)0, 0, new object[] { }, label = "Show Trophies", postClickAction = PostClickAction.HideButton)]
	private void DebugShowTrophies()
	{
		foreach (Item trophyItem in trophyItems)
		{
			trophyItem.gameObject.SetActive(value: true);
		}
	}

	[DebugButton(null, Tint.Default, (PostClickAction)0, 0, new object[] { }, label = "Show Star Keys", postClickAction = PostClickAction.HideButton)]
	private void DebugShowStarKeys()
	{
		Item[] array = starKeys;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].gameObject.SetActive(value: true);
		}
	}

	[DebugButton(null, Tint.Default, (PostClickAction)0, 0, new object[] { }, label = "Get Book", postClickAction = PostClickAction.HideButton)]
	private void DebugGetBook()
	{
		game.addItemToInventory(hiddenBookSlot.acceptItems[0].gameObject);
	}

	[DebugButton(null, Tint.Default, (PostClickAction)0, 0, new object[] { }, label = "Set Secret Room Timer to Now", postClickAction = PostClickAction.HideButton)]
	private void DebugSetTimer()
	{
		secretRoomTargetDate = DateTime.Now.AddSeconds(10.0);
	}

	[DebugButton(null, Tint.Default, (PostClickAction)0, 0, new object[] { }, label = "Show Level Complete Screenshots", postClickAction = PostClickAction.HideButton)]
	private void DebugShowLevelScreenshots()
	{
		GameObject[] array = packImagesJigsawPieces;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(value: true);
		}
	}

	public override int getPacketCount()
	{
		return Lobby.getPacketCount();
	}

	public override Packet getPacket(byte id)
	{
		return Lobby.getPacket(id);
	}

	public override Timer getTimer(byte id)
	{
		return id switch
		{
			0 => new TokenPuzzleChangePiecesLayerTimer(), 
			1 => new CCTeleportTimer(), 
			_ => null, 
		};
	}
}
