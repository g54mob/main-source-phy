using System;
using System.Collections.Generic;
using System.Text;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class Dracula2Logic : LevelLogic, ISaveable
{
	public enum LightScene
	{
		Default = 0,
		Normal = 1,
		Eventhaunt = 2,
		EventWindow = 3
	}

	public enum FadeActionType
	{
		ReleasePlayers = 0,
		DomeHead = 1,
		ShowWeapon = 2,
		HauntTable = 3,
		SolveKnights = 4
	}

	public class DiningTable
	{
		[DontSave]
		public GameObject table;

		[DontSave]
		public RefArray<Item, GameObject, Transform> goblets;

		[DontSave]
		public Slot[] gobletSlots;

		[DontSave]
		public TweenState gobletRotatingTablePart;

		[DontSave]
		public Ref<Turnable, GameObject, Transform> cake;

		[DontSave]
		public GameObject cakePiece;

		[DontSave]
		public Slot[] plateSlots;

		[DontSave]
		public Ref<GameObject, MaterialState, Transform> dome;

		[DontSave]
		public Item domeItem;

		[DontSave]
		public Turnable[] domeTurnables;

		[DontSave]
		public TweenState domeHandle;

		[DontSave]
		public Ref<Transform> lazySusan;

		[DontSave]
		public Renderer tableCloth;
	}

	public enum PlayerCountStyle
	{
		One = 0,
		Two = 1,
		Three = 2,
		FourPlus = 3
	}

	public enum PinColor
	{
		Green = 0,
		Red = 1,
		Blue = 2
	}

	public enum PinPattern
	{
		Cross = 0,
		Flower = 1,
		Snowflake = 2,
		Pizza = 3
	}

	public class PinHoleData
	{
		[DontSave]
		public Slot slot;

		[DontSave]
		public PinData pin;

		[DontSave]
		public List<PinHoleData> data;

		[DontSave]
		public List<int> stateIndex;
	}

	public class PinData
	{
		[DontSave]
		public GameObject pin;

		[DontSave]
		public PinColor color;

		[DontSave]
		public PinPattern pattern;
	}

	public enum KingsPieceType
	{
		Square = 0,
		Loop = 1,
		Virgo = 2,
		Sagittarius = 3
	}

	public enum KingsColorType
	{
		Blue = 0,
		Brown = 1,
		Gray = 2,
		Green = 3
	}

	public struct KingsPiece
	{
		public GameObject piece;

		public KingsPieceType topLeft;

		public KingsPieceType topRight;

		public KingsPieceType bottomLeft;

		public KingsPieceType bottomRight;
	}

	public sealed class AnimalsAllPulledUpDelayTimer : Timer
	{
		public override byte getTypeId()
		{
			return 0;
		}

		public override void writeData(FastBinaryWriter writer)
		{
		}

		public override void readData(FastBinaryReader reader)
		{
		}
	}

	public sealed class AnimalsEnableChainsTimer : Timer
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

	public sealed class JigsawSnappedTimer : Timer
	{
		public override byte getTypeId()
		{
			return 2;
		}

		public override void writeData(FastBinaryWriter writer)
		{
		}

		public override void readData(FastBinaryReader reader)
		{
		}
	}

	public sealed class FloorRespawnTimer : Timer
	{
		public Item item;

		public override byte getTypeId()
		{
			return 3;
		}

		public FloorRespawnTimer()
		{
		}

		public FloorRespawnTimer(Item item)
		{
			this.item = item;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.WriteComponent(item);
		}

		public override void readData(FastBinaryReader reader)
		{
			item = reader.ReadComponent<Item>();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("item: " + $"{item}");
			return stringBuilder.ToString();
		}
	}

	public sealed class PaintingButtonTimer : Timer
	{
		public int index;

		public float initialRotation;

		public float endRotation;

		public override byte getTypeId()
		{
			return 4;
		}

		public PaintingButtonTimer()
		{
		}

		public PaintingButtonTimer(int index, float initialRotation, float endRotation)
		{
			this.index = index;
			this.initialRotation = initialRotation;
			this.endRotation = endRotation;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in index, default(FastBinaryWriter.ForPrimitives));
			writer.Write(in initialRotation, default(FastBinaryWriter.ForPrimitives));
			writer.Write(in endRotation, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			index = reader.ReadInt32();
			initialRotation = reader.ReadSingle();
			endRotation = reader.ReadSingle();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("index: " + $"{index}");
			stringBuilder.AppendLine("initialRotation: " + $"{initialRotation}");
			stringBuilder.Append("endRotation: " + $"{endRotation}");
			return stringBuilder.ToString();
		}
	}

	public sealed class SolvePaintingTimer : Timer
	{
		public override byte getTypeId()
		{
			return 5;
		}

		public override void writeData(FastBinaryWriter writer)
		{
		}

		public override void readData(FastBinaryReader reader)
		{
		}
	}

	public sealed class LightingTimer : Timer
	{
		public override byte getTypeId()
		{
			return 6;
		}

		public override void writeData(FastBinaryWriter writer)
		{
		}

		public override void readData(FastBinaryReader reader)
		{
		}
	}

	public sealed class DomePickupTimer : Timer
	{
		public override byte getTypeId()
		{
			return 7;
		}

		public override void writeData(FastBinaryWriter writer)
		{
		}

		public override void readData(FastBinaryReader reader)
		{
		}
	}

	public sealed class SolveKnightsTimer : Timer
	{
		public override byte getTypeId()
		{
			return 8;
		}

		public override void writeData(FastBinaryWriter writer)
		{
		}

		public override void readData(FastBinaryReader reader)
		{
		}
	}

	public sealed class FireCutSceneTimer : Timer
	{
		public int step;

		public override byte getTypeId()
		{
			return 9;
		}

		public FireCutSceneTimer()
		{
		}

		public FireCutSceneTimer(int step)
		{
			this.step = step;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in step, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			step = reader.ReadInt32();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("step: " + $"{step}");
			return stringBuilder.ToString();
		}
	}

	public sealed class SolveFamilyTree1Timer : Timer
	{
		public override byte getTypeId()
		{
			return 10;
		}

		public override void writeData(FastBinaryWriter writer)
		{
		}

		public override void readData(FastBinaryReader reader)
		{
		}
	}

	public sealed class SolveFamilyTree2Timer : Timer
	{
		public override byte getTypeId()
		{
			return 11;
		}

		public override void writeData(FastBinaryWriter writer)
		{
		}

		public override void readData(FastBinaryReader reader)
		{
		}
	}

	public sealed class GobletLockDelayTimer : Timer
	{
		public override byte getTypeId()
		{
			return 12;
		}

		public override void writeData(FastBinaryWriter writer)
		{
		}

		public override void readData(FastBinaryReader reader)
		{
		}
	}

	public sealed class KnightDelayTimer : Timer
	{
		public override byte getTypeId()
		{
			return 13;
		}

		public override void writeData(FastBinaryWriter writer)
		{
		}

		public override void readData(FastBinaryReader reader)
		{
		}
	}

	public sealed class ChestOpenTimer : Timer
	{
		public int i;

		public override byte getTypeId()
		{
			return 14;
		}

		public ChestOpenTimer()
		{
		}

		public ChestOpenTimer(int i)
		{
			this.i = i;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in i, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			i = reader.ReadInt32();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("i: " + $"{i}");
			return stringBuilder.ToString();
		}
	}

	public sealed class KnifeBoxSoundTimer : Timer
	{
		public override byte getTypeId()
		{
			return 15;
		}

		public override void writeData(FastBinaryWriter writer)
		{
		}

		public override void readData(FastBinaryReader reader)
		{
		}
	}

	public sealed class FadeTimer : Timer
	{
		public FadeActionType actionType;

		public bool fadingOut;

		public override byte getTypeId()
		{
			return 16;
		}

		public FadeTimer()
		{
		}

		public FadeTimer(FadeActionType actionType, bool fadingOut)
		{
			this.actionType = actionType;
			this.fadingOut = fadingOut;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			int value = (int)actionType;
			writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
			writer.Write(in fadingOut, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			actionType = (FadeActionType)reader.ReadInt32();
			fadingOut = reader.ReadBoolean();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("actionType: " + $"{actionType}");
			stringBuilder.Append("fadingOut: " + $"{fadingOut}");
			return stringBuilder.ToString();
		}
	}

	public sealed class FoodCheckTimer : Timer
	{
		public int index;

		public Vector3 foodVelocity;

		public Rigidbody foodRB;

		public override byte getTypeId()
		{
			return 17;
		}

		public FoodCheckTimer()
		{
		}

		public FoodCheckTimer(int index, Vector3 foodVelocity, Rigidbody foodRB)
		{
			this.index = index;
			this.foodVelocity = foodVelocity;
			this.foodRB = foodRB;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in index, default(FastBinaryWriter.ForPrimitives));
			writer.WriteVector3(in foodVelocity);
			writer.WriteComponent(foodRB);
		}

		public override void readData(FastBinaryReader reader)
		{
			index = reader.ReadInt32();
			foodVelocity = reader.ReadVector3();
			foodRB = reader.ReadComponent<Rigidbody>();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("index: " + $"{index}");
			stringBuilder.AppendLine("foodVelocity: " + $"{foodVelocity}");
			stringBuilder.Append("foodRB: " + $"{foodRB}");
			return stringBuilder.ToString();
		}
	}

	public sealed class BookFireTimer : Timer
	{
		public int index;

		public Renderer[] rendererBook;

		public override byte getTypeId()
		{
			return 18;
		}

		public BookFireTimer()
		{
		}

		public BookFireTimer(int index, Renderer[] rendererBook)
		{
			this.index = index;
			this.rendererBook = rendererBook;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in index, default(FastBinaryWriter.ForPrimitives));
			writer.WriteArray(rendererBook, delegate(FastBinaryWriter w, Renderer e)
			{
				w.WriteComponent(e);
			});
		}

		public override void readData(FastBinaryReader reader)
		{
			index = reader.ReadInt32();
			rendererBook = reader.ReadArray((FastBinaryReader r) => r.ReadComponent<Renderer>());
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("index: " + $"{index}");
			stringBuilder.Append("rendererBook: " + ToStringHelper.Stringify(rendererBook, (Renderer e) => $"{e}"));
			return stringBuilder.ToString();
		}
	}

	private enum Puzzle
	{
		[PuzzleInfo("%PuzzleD2_1%", true)]
		Plates = 0,
		[PuzzleInfo("%PuzzleD2_2%", true)]
		TileBox = 1,
		[PuzzleInfo("%PuzzleD2_3%", true)]
		Cups = 2,
		[PuzzleInfo("%PuzzleD2_4%", false)]
		BoardGame = 3,
		[PuzzleInfo("%PuzzleD2_5%", false)]
		AnimalHeads = 4,
		[PuzzleInfo("%PuzzleD2_6%", false)]
		Pins = 5,
		[PuzzleInfo("%PuzzleD2_7%", false)]
		FamilyPaper = 6,
		[PuzzleInfo("%PuzzleD2_8%", false)]
		FamilyTree = 7,
		[PuzzleInfo("%PuzzleD2_9%", true)]
		Knights = 8,
		[PuzzleInfo("%PuzzleD2_10%", true)]
		Portrait = 9,
		[PuzzleInfo("%PuzzleD2_11%", false)]
		LetterKey = 10,
		[PuzzleInfo("%PuzzleD2_12%", false)]
		MasterKey = 11
	}

	private enum PlatesHint
	{
		GetPlates = 0,
		Solve = 1
	}

	private enum TileBoxHint
	{
		GetTileEnd = 0,
		GetTileStart = 1,
		GetTileMiddle = 2,
		MPGetAllTiles = 3,
		GetBox = 4,
		InsertTiles = 5,
		TileStartInserted = 6
	}

	private enum CupsHint
	{
		GetCup1 = 0,
		GetCup4 = 1,
		GetCup2 = 2,
		GetCup5 = 3,
		GetCup3 = 4,
		GobletToSlots = 5,
		GobletNumbers = 6
	}

	private enum BoardGameHint
	{
		GetKey = 0,
		InsertKey = 1,
		LookAtBoard = 2,
		MovePieces = 3
	}

	private enum AnimalHeadsHint
	{
		GetBook = 0,
		LookAtHeads = 1,
		FirstHead = 2,
		OtherHeads = 3
	}

	private enum PinsHint
	{
		GetPin1 = 0,
		GetPin2 = 1,
		GetPin3 = 2,
		GetPin4 = 3,
		PinsToSlots = 4,
		Connections = 5,
		FirstPin = 6,
		SecondPin = 7
	}

	private enum FamilyPaperHint
	{
		GetPiece12 = 0,
		GetPiece3 = 1,
		GetPiece45 = 2,
		PlacePieces = 3
	}

	private enum FamilyTreeHint
	{
		GetKnightsGameStatue = 0,
		GetAnimalHeadsStatue = 1,
		GetPinsStatue = 2,
		GetFamilyPaper = 3,
		GetFreeStatue = 4,
		LookAtStatues = 5,
		PlaceStatues = 6,
		FigureOutStatue = 7,
		FirstStatue = 8,
		SecondStatue = 9
	}

	private enum KnightsHint
	{
		LookAtDome = 0,
		TurnDome = 1,
		SolveDome = 2,
		TakeDome = 3,
		LookAtWeapons = 4,
		LookAtSymbols = 5,
		Solve = 6
	}

	private enum PortraitHint
	{
		LookAtArrow = 0,
		LookAtPortraitBack = 1,
		LookAtPortraitFront = 2,
		MatchPortraits = 3,
		Solve = 4
	}

	private enum LetterKeyHint
	{
		LookAtPainting = 0,
		ReadPaintingLetters = 1,
		SolveD = 2,
		OtherLetters = 3,
		Solve = 4
	}

	private enum MasterKeyHint
	{
		GetKey = 0,
		LookAtSlot = 1,
		TurnKey = 2,
		LookAtPieces = 3,
		MatchPieces = 4,
		Solve = 5
	}

	private enum LevelPredicate
	{
		SolvePainting = 0,
		SolveKnights = 1,
		SolvePinBox = 2,
		SolveChest = 3,
		SolveCandelabra = 4,
		Plates = 5,
		Goblets = 6,
		KnifeBox = 7,
		FamilyTree = 8,
		Animals = 9,
		KingsGame = 10,
		Dome = 11,
		DeusKey = 12
	}

	[DontSave]
	public bool DEBUGStandUpOnStart;

	[DontSave]
	public int DEBUGPlayerCount;

	[DontSave]
	private ProbeReferenceVolume probeRefVolume;

	[DontSave]
	private string startingLightingScenario;

	private LightScene targetLighting;

	private bool isTransitioningToLighting;

	[DontSave]
	[Range(0f, 1f)]
	public float blendingFactor = 0.5f;

	[DontSave]
	private Exposure exposure;

	[DontSave]
	private Exposure exposureHaunt;

	private bool isFading;

	[DontSave]
	private float startingExposure;

	[DontSave]
	private float startingExposureHaunt;

	private List<Item> respawningItems = new List<Item>();

	[DontSave]
	public Volume postVolume;

	[DontSave]
	public float minExposure;

	[DontSave]
	private const int TABLE_COUNT = 4;

	[DontSave]
	private DiningTable[] diningTables;

	[DontSave]
	private int[] domeSolution = new int[3] { 5, 2, 0 };

	private bool solvedGoblets;

	private bool[] pickedUpDomes = new bool[2];

	[DontSave]
	public MeshRenderer[] deliciousFoods;

	[DontSave]
	public MeshRenderer[] rottenFoods;

	[DontSave]
	private List<Interactive> allNonTableInteractives = new List<Interactive>();

	private HashSet<Interactive> itemsOnLazySusan = new HashSet<Interactive>();

	private bool solvedDomeSP;

	private bool solvedKnightsSp;

	private bool showDomeHead;

	private bool domePickedUp;

	public PlayerCountStyle currentPlayerStyle;

	public PlayerCountStyle startingPlayerStyle;

	[DontSave]
	private PinHoleData[] pinHoleDatas;

	[DontSave]
	private bool isPinBoxSolved;

	[DontSave]
	private List<PinData> pinDatas = new List<PinData>();

	[DontSave]
	private bool initedPinDatas;

	[DontSave]
	private KingsColorType[][] kingsKingdoms = new KingsColorType[6][]
	{
		new KingsColorType[6]
		{
			KingsColorType.Blue,
			KingsColorType.Blue,
			KingsColorType.Blue,
			KingsColorType.Brown,
			KingsColorType.Brown,
			KingsColorType.Brown
		},
		new KingsColorType[6]
		{
			KingsColorType.Blue,
			KingsColorType.Gray,
			KingsColorType.Blue,
			KingsColorType.Brown,
			KingsColorType.Brown,
			KingsColorType.Brown
		},
		new KingsColorType[6]
		{
			KingsColorType.Blue,
			KingsColorType.Gray,
			KingsColorType.Gray,
			KingsColorType.Green,
			KingsColorType.Brown,
			KingsColorType.Brown
		},
		new KingsColorType[6]
		{
			KingsColorType.Gray,
			KingsColorType.Gray,
			KingsColorType.Gray,
			KingsColorType.Green,
			KingsColorType.Green,
			KingsColorType.Brown
		},
		new KingsColorType[6]
		{
			KingsColorType.Gray,
			KingsColorType.Green,
			KingsColorType.Green,
			KingsColorType.Green,
			KingsColorType.Green,
			KingsColorType.Green
		},
		new KingsColorType[6]
		{
			KingsColorType.Gray,
			KingsColorType.Green,
			KingsColorType.Green,
			KingsColorType.Green,
			KingsColorType.Green,
			KingsColorType.Green
		}
	};

	[DontSave]
	private KingsPiece[] kingsPieces;

	[DontSave]
	private float kingsGameCheckingCurrent = 1f;

	[DontSave]
	public float kingsGameNodeDistance;

	private int currentAnimalsPull;

	private bool solvedAnimals;

	private bool domeSolved;

	private bool[] pickedUpWeapons = new bool[5];

	private ParticleSystem[] allFireParticles;

	private NetPlayerId treeSlotLastPlayer;

	private bool solvedFamilyTree;

	private bool isLocalPlayerInDome;

	private readonly int[] cubeIgnoreNext = new int[6] { 2, 2, 2, 2, 2, 3 };

	private int[] cubeCurrentStep = new int[6] { -1, -1, -1, -1, -1, -1 };

	private bool solvedPainting;

	private bool trappedPlayer;

	private bool isFireOff;

	public float paintingCubeSpeed;

	private int[] chestKeySolution = new int[4] { 1, 2, 3, 1 };

	private bool chestCurrentSolved;

	private bool solvedChest;

	private bool solvedCandelabra;

	private bool solvedExitDoor;

	private bool rotateExitKey;

	private bool candelabraSlotTried;

	[DontSave]
	public EventInstance exitDoorInstance;

	public float rotatingSpeed;

	[DontSave]
	public Ref<GameObject, Switch3D> levelExitRef;

	[DontSave]
	public SpawnPointToPose[] spawnPosePoints;

	[Header("Generated Variables")]
	[DontSave]
	public GameObject fireplaceObstacle;

	[DontSave]
	public GameObject[] chairBacks;

	[DontSave]
	public GameObject chairsDamagedParts;

	[DontSave]
	public Item p3ExtraPlate;

	[DontSave]
	public Ref<GameObject, Volume> trayHeadVolume;

	[DontSave]
	public GameObject[] mpRespawnAreaParents;

	[DontSave]
	public Transform fireplaceTeleportPoint;

	[DontSave]
	public CharacterPose domeHeadPose;

	[DontSave]
	public GameObject weaponDecals;

	[DontSave]
	public TweenState enableKingsTS;

	[DontSave]
	public TweenState solvedKingsTS;

	[DontSave]
	public Ref<Transform> exitDoorRotating;

	[DontSave]
	public Ref<Transform> middleTablePuzzle;

	[DontSave]
	public Ref<GameObject> windows;

	[DontSave]
	public Ref<GameObject> windowsBroken;

	[DontSave]
	public Ref<CollisionData, GameObject> floorCollisionData;

	[DontSave]
	public GameObject[] neckUnlocks;

	[DontSave]
	public GameObject[] neckLocks;

	[DontSave]
	public RefArray<Turnable, GameObject, Transform> cakes;

	[DontSave]
	public Volume volumeEventHaunt;

	[DontSave]
	public MaterialState[] kingsPiecesImages;

	[DontSave]
	public GameObject familyTreeFixedPiece;

	[DontSave]
	public Ref<TweenState, Transform, GameObject> cakeKnifeBox;

	[DontSave]
	public TweenState zoomablePicture;

	[DontSave]
	public Switch3D[] paintingCubeButtons;

	[DontSave]
	public RefArray<GameObject, Transform> paintingCubes;

	[DontSave]
	public Item[] placedStatues;

	[DontSave]
	public Sequence chestOpenSequence;

	[DontSave]
	public Item kingsGameStatue;

	[DontSave]
	public SlidableGraph kingsSlidableGraph;

	[DontSave]
	public Slot kingsSlot;

	[DontSave]
	public RefArray<GameObject, Collider> kingsGameSliders;

	[DontSave]
	public Zoomable kingsGameZoom;

	[DontSave]
	public Zoomable familyTreeBookstandZoom;

	[DontSave]
	public Jigsaw familyTreeJigsaw;

	[DontSave]
	public Item familyTreeWholePaper;

	[DontSave]
	public Ref<Sequence, Transform, GameObject> doorSequence;

	[DontSave]
	public GameObject[] exitKeyPieces;

	[DontSave]
	public SlidableGraph exitKeyGraph;

	[DontSave]
	public Switch3D[] exitKeySwitches;

	[DontSave]
	public Item animalsStatuette;

	[DontSave]
	public CharacterPose[] characterPoses;

	[DontSave]
	public Ref<GameObject, Trigger> domeRegionTrigger;

	[DontSave]
	public Switch3D chestLid;

	[DontSave]
	public GameObject fliesSound;

	[DontSave]
	public GameObject[] lightingScenes;

	[DontSave]
	public GameObject[] knifeBoxTileStarts;

	[DontSave]
	public GameObject[] knifeBoxTileMiddles;

	[DontSave]
	public GameObject[] knifeBoxTileEnds;

	[DontSave]
	public Slot[] knifeBoxSlots;

	[DontSave]
	public Switch3D knifeBoxLid;

	[DontSave]
	public GameObject allFires;

	[DontSave]
	public GameObject[] staticWeapons;

	[DontSave]
	public Zoomable fireplacePainting;

	[DontSave]
	public GameObject fireplacePaintingFront;

	[DontSave]
	public RefArray<Transform, GameObject> lazySusans;

	[DontSave]
	public GameObject[] knightSounds;

	[DontSave]
	public TweenState[] knights;

	[DontSave]
	public Item[] domeItems;

	[DontSave]
	public TweenState[] domeLidHandles;

	[DontSave]
	public MaterialState[] puzzleSigns;

	[DontSave]
	public TweenState exitDoorTween;

	[DontSave]
	public TweenState candelabraSlotTween;

	[DontSave]
	public Slot candelabraSlot;

	[DontSave]
	public Item candelabra;

	[DontSave]
	public Slot chestKeyWrongSlot;

	[DontSave]
	public Slot chestKeySlot;

	[DontSave]
	public Slidable[] keySlidables;

	[DontSave]
	public Item chestKey;

	[DontSave]
	public GameObject deusPainting;

	[DontSave]
	public TweenState paintingKeyDoor;

	[DontSave]
	public Switch3D[] paintingSolution;

	[DontSave]
	public Switch3D[] paintingButtons;

	[DontSave]
	public Ref<Trigger, Transform, GameObject> fireplaceTrigger;

	[DontSave]
	public RefArray<Light, GameObject> fireplaceLights;

	[DontSave]
	public ParticleSystem fire;

	[DontSave]
	public GameObject fireSound;

	[DontSave]
	public TweenState firepitSpikes;

	[DontSave]
	public GameObject fireCutScene;

	[DontSave]
	public GameObject[] weaponSounds;

	[DontSave]
	public Slot[] weaponSlots;

	[DontSave]
	public Item[] weapons;

	[DontSave]
	public Turnable[] domeCoopTurnables;

	[DontSave]
	public RefArray<GameObject, MaterialState, Transform> domes;

	[DontSave]
	public Turnable[] domeSoloTurnables;

	[DontSave]
	public Slot[] plateSlots4;

	[DontSave]
	public Slot[] plateSlots3;

	[DontSave]
	public Slot[] plateSlots2;

	[DontSave]
	public Slot[] plateSlots1;

	[DontSave]
	public GameObject[] plates2;

	[DontSave]
	public GameObject[] plates3;

	[DontSave]
	public GameObject[] plates4;

	[DontSave]
	public GameObject[] cakePieces;

	[DontSave]
	public TweenState[] gobletRotatingTableParts;

	[DontSave]
	public Slot[] gobletSlots4;

	[DontSave]
	public Slot[] gobletSlots3;

	[DontSave]
	public Slot[] gobletSlots2;

	[DontSave]
	public Slot[] gobletSlots1;

	[DontSave]
	public RefArray<Item, GameObject, Transform> goblets4;

	[DontSave]
	public RefArray<Item, GameObject, Transform> goblets3;

	[DontSave]
	public RefArray<Item, GameObject, Transform> goblets2;

	[DontSave]
	public GameObject[] diningTableObjects;

	[DontSave]
	public TweenState jackalope;

	[DontSave]
	public Switch3D[] animalChains;

	[DontSave]
	public GameObject animalChainsBook;

	[DontSave]
	public TweenState[] animalChainTweenStates;

	[DontSave]
	public Slot[] statueSlots;

	[DontSave]
	public TweenState[] statueSlotTSs;

	[DontSave]
	public Renderer[] tableClothRenderer;

	[DontSave]
	public Material[] tableClothHauntMaterials;

	[DontSave]
	public Zoomable pinBox;

	[DontSave]
	public Slot[] pinSlots;

	[DontSave]
	public TweenState pinBoxDrawer;

	[DontSave]
	public TweenState[] pinBoxStates;

	[DontSave]
	public GameObject[] pins;

	[DontSave]
	public RefArray<Item, GameObject, Transform> goblets1;

	[DontSave]
	public Trigger fireLogsTrigger;

	[DontSave]
	public GameObject fireLogsTriggerGO;

	[DontSave]
	public RefArray<GameObject, MaterialState, Item, Transform> fireLog;

	[DontSave]
	public RefArray<GameObject, Item, Transform> fireBook;

	[DontSave]
	public ParticleSystem fireLogVFX;

	[DontSave]
	public Transform fireLogVFXTransform;

	[DontSave]
	public RefArray<GameObject, Item, Transform, TweenState> food;

	[DontSave]
	public ParticleSystem foodVFX;

	[DontSave]
	public Transform foodVFXTransform;

	[DontSave]
	public ParticleSystem foodVFXInventory;

	[DontSave]
	public Transform foodVFXTransformInventory;

	public override void onInitHints()
	{
		game.setPuzzleType(Puzzle.Cups, Game.Puzzle.Type.CoopAsk);
		game.setPuzzleType(Puzzle.Knights, Game.Puzzle.Type.CoopAsk);
		game.setPuzzleType(Puzzle.Portrait, Game.Puzzle.Type.CoopAsk);
		game.setPuzzleConditions(Puzzle.Cups, Puzzle.TileBox);
		game.setPuzzleConditions(Puzzle.BoardGame, Puzzle.Cups);
		game.setPuzzleConditions(Puzzle.AnimalHeads, Puzzle.Cups);
		game.setPuzzleConditions(Puzzle.Pins, Puzzle.Cups);
		game.setPuzzleConditions(Puzzle.FamilyPaper, Puzzle.Cups);
		game.setPuzzleConditions(Puzzle.FamilyTree, Puzzle.FamilyPaper);
		game.setPuzzleConditions(Puzzle.Knights, Puzzle.FamilyTree);
		game.setPuzzleConditions(Puzzle.Portrait, Puzzle.Knights);
		game.setPuzzleConditions(Puzzle.LetterKey, Puzzle.Portrait);
		game.setPuzzleConditions(Puzzle.MasterKey, Puzzle.LetterKey);
		game.setRelevantObjectsForPuzzle(Puzzle.Plates, plates2);
		game.setRelevantObjectsForPuzzle(Puzzle.Plates, plates3);
		game.setRelevantObjectsForPuzzle(Puzzle.Plates, plates4);
		game.setRelevantObjectsForPuzzle(Puzzle.TileBox, cakeKnifeBox.Get<GameObject>(0u), lazySusans[0].Get<GameObject>(0f), lazySusans[1].Get<GameObject>(0f), lazySusans[2].Get<GameObject>(0f), lazySusans[3].Get<GameObject>(0f), knifeBoxSlots[0].acceptItems[0].gameObject, knifeBoxSlots[0].acceptItems[1].gameObject, knifeBoxSlots[0].acceptItems[2].gameObject, knifeBoxSlots[0].acceptItems[3].gameObject, knifeBoxSlots[0].rejectItems[0].gameObject, knifeBoxSlots[0].rejectItems[1].gameObject, knifeBoxSlots[0].rejectItems[2].gameObject, knifeBoxSlots[0].rejectItems[3].gameObject, knifeBoxSlots[0].rejectItems[4].gameObject, knifeBoxSlots[0].rejectItems[5].gameObject, knifeBoxSlots[0].rejectItems[6].gameObject, knifeBoxSlots[0].rejectItems[7].gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.Cups, lazySusans[0].Get<GameObject>(0f), lazySusans[1].Get<GameObject>(0f), lazySusans[2].Get<GameObject>(0f), lazySusans[3].Get<GameObject>(0f), goblets1[0].Get<GameObject>(0f), goblets1[1].Get<GameObject>(0f), goblets1[2].Get<GameObject>(0f), goblets1[3].Get<GameObject>(0f), goblets2[0].Get<GameObject>(0f), goblets2[1].Get<GameObject>(0f), goblets2[2].Get<GameObject>(0f), goblets2[3].Get<GameObject>(0f), goblets3[0].Get<GameObject>(0f), goblets3[1].Get<GameObject>(0f), goblets3[2].Get<GameObject>(0f), goblets3[3].Get<GameObject>(0f), goblets4[0].Get<GameObject>(0f), goblets4[1].Get<GameObject>(0f), goblets4[2].Get<GameObject>(0f), goblets4[3].Get<GameObject>(0f), cakePieces[0], cakePieces[1], cakePieces[2], cakePieces[3]);
		game.setRelevantObjectsForPuzzle(Puzzle.BoardGame, kingsGameZoom.gameObject, kingsSlot.gameObject, kingsSlot.acceptItems[0].gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.AnimalHeads, animalChainsBook, animalChains[0].gameObject, animalChains[1].gameObject, animalChains[2].gameObject, animalChains[3].gameObject, animalChains[4].gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.Pins, pinBox.gameObject, pins[0], pins[1], pins[2], pins[3], pins[4], pins[5], pins[6]);
		game.setRelevantObjectsForPuzzle(Puzzle.FamilyPaper, familyTreeBookstandZoom.gameObject, familyTreeJigsaw.gameObject, familyTreeJigsaw.pieces[0].linkedItem.gameObject, familyTreeJigsaw.pieces[1].linkedItem.gameObject, familyTreeJigsaw.pieces[2].linkedItem.gameObject, familyTreeJigsaw.pieces[3].linkedItem.gameObject, familyTreeJigsaw.pieces[4].linkedItem.gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.FamilyTree, familyTreeWholePaper.gameObject, statueSlots[0].acceptItems[0].gameObject, statueSlots[0].acceptItems[1].gameObject, statueSlots[0].acceptItems[2].gameObject, statueSlots[0].acceptItems[3].gameObject, statueSlots[0].rejectItems[0].gameObject, statueSlots[0].rejectItems[1].gameObject, statueSlots[0].rejectItems[2].gameObject, statueSlots[0].rejectItems[3].gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.Knights, domeItems[0].gameObject, domeItems[1].gameObject, domeSoloTurnables[0].gameObject, domeSoloTurnables[1].gameObject, domeSoloTurnables[2].gameObject, domeCoopTurnables[0].gameObject, domeCoopTurnables[1].gameObject, domeCoopTurnables[2].gameObject, weapons[0].gameObject, weapons[1].gameObject, weapons[2].gameObject, weapons[3].gameObject, weapons[4].gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.Portrait, fireplacePainting.gameObject, fireplacePaintingFront);
		game.setRelevantObjectsForPuzzle(Puzzle.LetterKey, chestKey.gameObject, deusPainting, chestKeySlot.gameObject, chestKeyWrongSlot.gameObject, keySlidables[0].gameObject, keySlidables[1].gameObject, keySlidables[2].gameObject, keySlidables[3].gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.MasterKey, candelabra.gameObject, exitKeyGraph.gameObject, candelabraSlot.gameObject);
		game.setHintCondition(Puzzle.Plates, PlatesHint.GetPlates, () => game.getPlayerCount() == 1 || Array.TrueForAll(plates2, (GameObject p) => game.wasAddedToInventoryDuringCurrentPuzzle(p)) || Array.TrueForAll(plates3, (GameObject p) => game.wasAddedToInventoryDuringCurrentPuzzle(p)) || Array.TrueForAll(plates4, (GameObject p) => game.wasAddedToInventoryDuringCurrentPuzzle(p)));
		game.setHintCondition(Puzzle.Plates, PlatesHint.Solve, () => game.getPlayerCount() == 1);
		game.setHintCondition(Puzzle.TileBox, TileBoxHint.GetTileEnd, () => game.getPlayerCount() > 1 || game.wasAnyAddedToInventoryDuringCurrentPuzzle(knifeBoxTileEnds));
		game.setHintCondition(Puzzle.TileBox, TileBoxHint.GetTileStart, () => game.getPlayerCount() > 1 || game.wasAnyAddedToInventoryDuringCurrentPuzzle(knifeBoxTileStarts));
		game.setHintCondition(Puzzle.TileBox, TileBoxHint.GetTileMiddle, () => game.getPlayerCount() > 1 || game.wasAnyAddedToInventoryDuringCurrentPuzzle(knifeBoxTileMiddles));
		game.setHintCondition(Puzzle.TileBox, TileBoxHint.MPGetAllTiles, () => game.getPlayerCount() == 1 || (game.wasAnyAddedToInventoryDuringCurrentPuzzle(knifeBoxTileStarts) && game.wasAnyAddedToInventoryDuringCurrentPuzzle(knifeBoxTileMiddles) && game.wasAnyAddedToInventoryDuringCurrentPuzzle(knifeBoxTileEnds)));
		game.setHintCondition(Puzzle.TileBox, TileBoxHint.GetBox, () => game.wasAddedToInventoryDuringCurrentPuzzle(cakeKnifeBox.Get<GameObject>(0u)));
		game.setHintCondition(Puzzle.TileBox, TileBoxHint.InsertTiles, delegate
		{
			bool flag = false;
			Slot[] array = knifeBoxSlots;
			foreach (Slot slot in array)
			{
				flag |= slot.insertedItem != null;
			}
			return flag;
		});
		game.setHintCondition(Puzzle.Cups, CupsHint.GetCup1, () => game.wasAnyAddedToInventoryDuringCurrentPuzzle(getDiningTable().goblets[0]));
		game.setHintCondition(Puzzle.Cups, CupsHint.GetCup4, () => game.wasAnyAddedToInventoryDuringCurrentPuzzle(getDiningTable().goblets[3]));
		game.setHintCondition(Puzzle.Cups, CupsHint.GetCup2, () => game.wasAnyAddedToInventoryDuringCurrentPuzzle(getDiningTable().goblets[1]));
		game.setHintCondition(Puzzle.Cups, CupsHint.GetCup5, () => game.wasAnyAddedToInventoryDuringCurrentPuzzle(getDiningTable().goblets[4]));
		game.setHintCondition(Puzzle.Cups, CupsHint.GetCup3, () => game.wasAddedToInventoryDuringCurrentPuzzle(getDiningTable().goblets[2]));
		game.setHintCondition(Puzzle.Cups, CupsHint.GobletToSlots, delegate
		{
			bool flag = false;
			Slot[] gobletSlots = getDiningTable().gobletSlots;
			foreach (Slot slot in gobletSlots)
			{
				flag |= slot.insertedItem != null;
			}
			return flag;
		});
		game.setHintCondition(Puzzle.BoardGame, BoardGameHint.GetKey, () => game.wasAddedToInventoryDuringCurrentPuzzle(kingsSlot.acceptItems[0].gameObject));
		game.setHintCondition(Puzzle.BoardGame, BoardGameHint.InsertKey, () => kingsSlot.isUnlocked);
		game.setHintCondition(Puzzle.BoardGame, BoardGameHint.LookAtBoard, () => game.wasLookedAtCurrentPuzzle(kingsGameZoom.gameObject));
		game.setHintCondition(Puzzle.AnimalHeads, AnimalHeadsHint.GetBook, () => game.wasAddedToInventoryDuringCurrentPuzzle(animalChainsBook));
		game.setHintCondition(Puzzle.AnimalHeads, AnimalHeadsHint.FirstHead, () => currentAnimalsPull > 0);
		game.setHintCondition(Puzzle.Pins, PinsHint.GetPin1, () => game.wasAddedToInventoryDuringCurrentPuzzle(pins[4]));
		game.setHintCondition(Puzzle.Pins, PinsHint.GetPin2, () => game.wasAddedToInventoryDuringCurrentPuzzle(pins[5]));
		game.setHintCondition(Puzzle.Pins, PinsHint.GetPin3, () => game.wasAddedToInventoryDuringCurrentPuzzle(pins[1]));
		game.setHintCondition(Puzzle.Pins, PinsHint.GetPin4, () => game.wasAddedToInventoryDuringCurrentPuzzle(pins[6]));
		game.setHintCondition(Puzzle.Pins, PinsHint.PinsToSlots, () => (bool)game.isInSlot(pins[4].GetComponent<Item>()) || (bool)game.isInSlot(pins[5].GetComponent<Item>()) || (bool)game.isInSlot(pins[1].GetComponent<Item>()) || (bool)game.isInSlot(pins[6].GetComponent<Item>()));
		game.setHintCondition(Puzzle.FamilyPaper, FamilyPaperHint.GetPiece12, () => game.wasAddedToInventoryDuringCurrentPuzzle(familyTreeJigsaw.pieces[1].linkedItem.gameObject) && game.wasAddedToInventoryDuringCurrentPuzzle(familyTreeJigsaw.pieces[2].linkedItem.gameObject));
		game.setHintCondition(Puzzle.FamilyPaper, FamilyPaperHint.GetPiece3, () => game.wasAddedToInventoryDuringCurrentPuzzle(familyTreeJigsaw.pieces[3].linkedItem.gameObject));
		game.setHintCondition(Puzzle.FamilyPaper, FamilyPaperHint.GetPiece45, () => game.wasAddedToInventoryDuringCurrentPuzzle(familyTreeJigsaw.pieces[0].linkedItem.gameObject) && game.wasAddedToInventoryDuringCurrentPuzzle(familyTreeJigsaw.pieces[4].linkedItem.gameObject));
		game.setHintCondition(Puzzle.FamilyPaper, FamilyPaperHint.PlacePieces, delegate
		{
			bool flag = false;
			for (int i = 0; i < 5; i++)
			{
				flag &= familyTreeJigsaw.pieces[0].gameObject.activeSelf;
			}
			return flag;
		});
		game.setHintCondition(Puzzle.FamilyTree, FamilyTreeHint.GetKnightsGameStatue, () => game.wasAddedToInventoryDuringCurrentPuzzle(kingsGameStatue.gameObject));
		game.setHintCondition(Puzzle.FamilyTree, FamilyTreeHint.GetAnimalHeadsStatue, () => game.wasAddedToInventoryDuringCurrentPuzzle(animalsStatuette.gameObject));
		game.setHintCondition(Puzzle.FamilyTree, FamilyTreeHint.GetPinsStatue, () => game.wasAddedToInventoryDuringCurrentPuzzle(statueSlots[0].rejectItems[3].gameObject));
		game.setHintCondition(Puzzle.FamilyTree, FamilyTreeHint.GetFamilyPaper, () => game.wasAddedToInventoryDuringCurrentPuzzle(familyTreeWholePaper.gameObject));
		game.setHintCondition(Puzzle.FamilyTree, FamilyTreeHint.GetFreeStatue, () => game.wasAddedToInventoryDuringCurrentPuzzle(placedStatues[1].gameObject));
		game.setHintCondition(Puzzle.FamilyTree, FamilyTreeHint.PlaceStatues, delegate
		{
			bool flag = true;
			Slot[] array = statueSlots;
			foreach (Slot slot in array)
			{
				flag &= slot.insertedItem != null;
			}
			return flag;
		});
		game.setHintCondition(Puzzle.FamilyTree, FamilyTreeHint.FirstStatue, () => statueSlots[7].isUnlocked);
		game.setHintCondition(Puzzle.FamilyTree, FamilyTreeHint.SecondStatue, () => statueSlots[7].isUnlocked && statueSlots[6].isUnlocked);
		game.setHintCondition(Puzzle.Knights, KnightsHint.TurnDome, () => getDomeTurnables()[0].value > 0 || getDomeTurnables()[1].value > 0 || getDomeTurnables()[2].value > 0);
		game.setHintCondition(Puzzle.Knights, KnightsHint.SolveDome, () => getDomeItem().targetable);
		game.setHintCondition(Puzzle.Knights, KnightsHint.TakeDome, () => game.wasAddedToInventoryDuringCurrentPuzzle(getDomeItem().gameObject));
		game.setHintCondition(Puzzle.Portrait, PortraitHint.LookAtArrow, () => fireplaceTrigger.Get<Trigger>(0).state);
		game.setHintCondition(Puzzle.Portrait, PortraitHint.LookAtPortraitBack, () => game.wasLookedAtCurrentPuzzle(fireplacePainting.gameObject));
		game.setHintCondition(Puzzle.Portrait, PortraitHint.LookAtPortraitFront, () => game.wasLookedAtCurrentPuzzle(fireplacePaintingFront));
		game.setHintCondition(Puzzle.LetterKey, LetterKeyHint.LookAtPainting, () => game.wasLookedAtCurrentPuzzle(deusPainting));
		game.setHintCondition(Puzzle.MasterKey, MasterKeyHint.GetKey, () => game.wasAddedToInventoryDuringCurrentPuzzle(candelabra.gameObject));
		game.setHintCondition(Puzzle.MasterKey, MasterKeyHint.LookAtSlot, () => candelabraSlotTried);
	}

	public override void onInitPredicates()
	{
		game.registerPredicate(LevelPredicate.SolvePainting, () => checkPaintingSolution());
		game.registerPredicate(LevelPredicate.SolveKnights, () => checkKnightsSolved());
		game.registerPredicate(LevelPredicate.SolvePinBox, () => checkPinBoxSolution());
		game.registerPredicate(LevelPredicate.SolveChest, () => chestKeySlot.isUnlocked && checkKeySolution());
		game.registerPredicate(LevelPredicate.SolveCandelabra, () => candelabraSlot.isUnlocked && checkExitKeySolution());
		game.registerPredicate(LevelPredicate.Plates, () => checkPlates());
		game.registerPredicate(LevelPredicate.Goblets, () => checkGoblets());
		game.registerPredicate(LevelPredicate.KnifeBox, () => checkKnifeBox());
		game.registerPredicate(LevelPredicate.FamilyTree, () => checkFamilyTree());
		game.registerPredicate(LevelPredicate.Animals, () => currentAnimalsPull == animalChains.Length);
		game.registerPredicate(LevelPredicate.KingsGame, () => !game.isAnyPlayerInteracting(kingsSlidableGraph.gameObject) && checkKingsGameSolution());
		game.registerPredicate(LevelPredicate.Dome, () => checkDomeSolution());
		game.registerPredicate(LevelPredicate.DeusKey, 0.01f, true, () => checkKeySolution());
	}

	public override void onLevelPredicateDone(int type, int doneCounter)
	{
		if (type == 0)
		{
			solvePainting();
		}
		if (type == 1)
		{
			game.startTimer(new KnightDelayTimer(), 0.2f);
		}
		if (type == 2)
		{
			solvePinBox();
		}
		if (type == 3)
		{
			solveChest();
		}
		if (type == 4)
		{
			solveCandelabra();
		}
		if (type == 5)
		{
			solvePlates();
		}
		if (type == 6)
		{
			game.startTimer(new GobletLockDelayTimer(), 0.2f);
		}
		if (type == 7)
		{
			solveKnifeBox();
		}
		if (type == 8)
		{
			putPlayerInDome();
			solveFamilyTree();
		}
		if (type == 9)
		{
			solveAnimals();
		}
		if (type == 10)
		{
			solveKingsGame();
		}
		if (type == 11)
		{
			solveDome();
		}
		if (type == 12)
		{
			chestKeyWrongSlot.targetable = false;
			chestKeySlot.targetable = true;
		}
	}

	public override void onLevelPredicateUndone(int type)
	{
		if (type == 12)
		{
			chestKeyWrongSlot.targetable = true;
			chestKeySlot.targetable = false;
		}
	}

	public override void onInit()
	{
		Interactive[] interactives = paintingCubeButtons;
		Interactive.linkInteractives(interactives);
		interactives = exitKeySwitches;
		Interactive.linkInteractives(interactives);
		int playerCount = game.getPlayerCount();
		if (playerCount >= 4)
		{
			currentPlayerStyle = PlayerCountStyle.FourPlus;
		}
		else
		{
			currentPlayerStyle = (PlayerCountStyle)(playerCount - 1);
		}
		startingPlayerStyle = currentPlayerStyle;
		Debug.Log("Starting player style: " + currentPlayerStyle);
		fireplaceTrigger.Get<GameObject>(0u).SetActive(value: false);
		postVolume.profile.TryGet<Exposure>(out exposure);
		volumeEventHaunt.profile.TryGet<Exposure>(out exposureHaunt);
		startingExposure = exposure.fixedExposure.value;
		startingExposureHaunt = exposureHaunt.fixedExposure.value;
		initDiningTable();
		chestKeyWrongSlot.targetable = true;
		chestKeySlot.targetable = false;
		initPinBox();
		kingsGameZoom.targetable = false;
		kingsSlidableGraph.isEdgeBlockedPredicate = delegate(GameObject node, GameObject neighbourNode, SlidableGraphPiece _)
		{
			if (kingsSlidableGraph.tryGetPiece(node, out var piece))
			{
				foreach (SlidableGraphPiece pieces in kingsSlidableGraph.piecesList)
				{
					if (!(pieces.gameObject == piece.gameObject) && kingsSlidableGraph.tryGetNode(pieces, out var node2))
					{
						Vector3 position = neighbourNode.transform.position;
						Vector3 position2 = node2.transform.position;
						float num3 = Mathf.Abs(position.x - position2.x);
						float num4 = Mathf.Abs(position.y - position2.y);
						float num5 = Mathf.Abs(position.z - position2.z);
						float num6 = 0.0001f;
						if (num3 <= kingsGameNodeDistance && num4 <= kingsGameNodeDistance && num5 <= kingsGameNodeDistance && (num3 != num6 || num4 != num6 || num5 != num6))
						{
							return true;
						}
					}
				}
			}
			return false;
		};
		kingsPieces = new KingsPiece[4];
		kingsPieces[0].piece = kingsSlidableGraph.piecesList[0].gameObject;
		kingsPieces[0].topLeft = KingsPieceType.Sagittarius;
		kingsPieces[0].topRight = KingsPieceType.Virgo;
		kingsPieces[0].bottomLeft = KingsPieceType.Square;
		kingsPieces[0].bottomRight = KingsPieceType.Square;
		kingsPieces[1].piece = kingsSlidableGraph.piecesList[1].gameObject;
		kingsPieces[1].topLeft = KingsPieceType.Square;
		kingsPieces[1].topRight = KingsPieceType.Sagittarius;
		kingsPieces[1].bottomLeft = KingsPieceType.Virgo;
		kingsPieces[1].bottomRight = KingsPieceType.Sagittarius;
		kingsPieces[2].piece = kingsSlidableGraph.piecesList[2].gameObject;
		kingsPieces[2].topLeft = KingsPieceType.Virgo;
		kingsPieces[2].topRight = KingsPieceType.Virgo;
		kingsPieces[2].bottomLeft = KingsPieceType.Square;
		kingsPieces[2].bottomRight = KingsPieceType.Sagittarius;
		kingsPieces[3].piece = kingsSlidableGraph.piecesList[3].gameObject;
		kingsPieces[3].topLeft = KingsPieceType.Loop;
		kingsPieces[3].topRight = KingsPieceType.Loop;
		kingsPieces[3].bottomLeft = KingsPieceType.Loop;
		kingsPieces[3].bottomRight = KingsPieceType.Loop;
		animalsStatuette.targetable = false;
		Item[] array = weapons;
		for (int num = 0; num < array.Length; num++)
		{
			array[num].gameObject.SetActive(value: false);
		}
		Slot[] array2 = weaponSlots;
		for (int num = 0; num < array2.Length; num++)
		{
			array2[num].gameObject.SetActive(value: false);
		}
		weaponDecals.SetActive(value: false);
		chairsDamagedParts.SetActive(value: false);
		allFireParticles = allFires.GetComponentsInChildren<ParticleSystem>();
		fireplacePainting.targetable = false;
		Switch3D[] array3 = paintingButtons;
		for (int num = 0; num < array3.Length; num++)
		{
			array3[num].targetable = false;
		}
		chestKey.targetable = false;
		for (int num2 = 0; num2 < paintingCubes.Length; num2++)
		{
			cubeCurrentStep[num2] = (int)(paintingCubes[num2].Get<Transform>(0f).localEulerAngles.z / 90f) % 4;
		}
		candelabra.targetable = false;
		exitDoorInstance = PineFmod.createInstance("event:/Sound Effects/04 Items/Mechanical, Machines & Motors/Metal_Mechanical_Loop_02");
		PineFmod.set3DAttributes(exitDoorInstance, PineFmod.to3DAttributes(doorSequence.Get<Transform>(0f)));
		interactives = game.levelContainer.GetComponentsInChildren<Interactive>();
		foreach (Interactive interactive in interactives)
		{
			if (!interactive.transform.IsChildOf(middleTablePuzzle.Get<Transform>()) && interactive.targetable)
			{
				allNonTableInteractives.Add(interactive);
				interactive.targetable = false;
			}
		}
		treeSlotLastPlayer = statueSlots[0].authorityPlayerId;
	}

	public override void onUpdate()
	{
		updateTransitionToLighting();
		if (rotateExitKey)
		{
			exitDoorRotating.Get<Transform>().Rotate(Vector3.up * Time.deltaTime * rotatingSpeed);
		}
	}

	public override void onRemoveFromInventory(Item item)
	{
	}

	public override void onSlot(Slot targetSlot)
	{
		if (Array.IndexOf(getDiningTable().plateSlots, targetSlot) != -1 && targetSlot.insertedItem != null && Array.IndexOf(targetSlot.acceptItems, targetSlot.insertedItem) != -1)
		{
			targetSlot.insertedItem.targetable = false;
			targetSlot.targetable = false;
		}
		if (Array.Exists(pinSlots, (Slot x) => x == targetSlot))
		{
			PinHoleData pinHoleData = Array.Find(pinHoleDatas, (PinHoleData x) => x.slot == targetSlot);
			PinData pinData = pinDatas.Find((PinData x) => x.pin == targetSlot.insertedItem.gameObject);
			if (pinData != null)
			{
				pinHoleData.pin = pinData;
			}
			updatePinBoxVisuals();
		}
		if (targetSlot == kingsSlot)
		{
			enableKingsTS.transitionToDuration("Open", 2.5f, 1f, playSound: true, 0.3f);
		}
		if (targetSlot == candelabraSlot)
		{
			if (candelabraSlot.insertedItem != null)
			{
				candelabraSlot.insertedItem.targetable = false;
			}
			if (!checkExitKeySolution())
			{
				candelabraSlot.targetable = false;
				candelabraSlotTween.transitionTo("Wrong", 1.5f);
			}
		}
		int index = UnityUtils.getIndex(statueSlots, targetSlot);
		if (index >= 0)
		{
			statueSlotTSs[index].transitionTo("Down", 4f);
			treeSlotLastPlayer = targetSlot.authorityPlayerId;
		}
	}

	public override void onRemoveFromSlot(Slot targetSlot, Item item)
	{
		if (Array.Exists(pinSlots, (Slot x) => x == targetSlot))
		{
			Array.Find(pinHoleDatas, (PinHoleData x) => x.slot == targetSlot).pin = null;
			updatePinBoxVisuals();
		}
		if (targetSlot == candelabraSlot)
		{
			candelabraSlotTween.setState("Wrong", 0f);
			candelabraSlotTween.setState("Correct", 0f);
			if (!candelabraSlotTried)
			{
				candelabraSlotTried = true;
			}
		}
		int index = UnityUtils.getIndex(statueSlots, targetSlot);
		if (index >= 0)
		{
			statueSlotTSs[index].transitionTo("Default", 4f);
		}
	}

	public override void onSwitch3D(Switch3D targetSwitch, Switch3DEvent switchEvent)
	{
		if (switchEvent != Switch3DEvent.Start)
		{
			return;
		}
		int num = Array.IndexOf(animalChains, targetSwitch);
		if (num >= 0)
		{
			if (num == 0)
			{
				currentAnimalsPull = 1;
			}
			else if (num == currentAnimalsPull)
			{
				currentAnimalsPull++;
			}
			else
			{
				currentAnimalsPull = 0;
			}
			targetSwitch.targetable = false;
			animalChainTweenStates[num].transitionTo("Down", targetSwitch.transitionSpeed, 0.5f);
			if (currentAnimalsPull != animalChains.Length)
			{
				bool flag = true;
				Switch3D[] array = animalChains;
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].targetable)
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					float num2 = 1f / targetSwitch.transitionSpeed;
					game.startTimer(new AnimalsAllPulledUpDelayTimer(), num2 + 0.3f);
				}
			}
		}
		for (int j = 0; j < paintingCubeButtons.Length; j++)
		{
			if (targetSwitch == paintingCubeButtons[j] && paintingCubeButtons[j].targetable)
			{
				int num3 = (cubeCurrentStep[j] + 1) % 4;
				if (num3 == cubeIgnoreNext[j])
				{
					num3 = (num3 + 1) % 4;
				}
				float num4 = (float)cubeCurrentStep[j] * 90f;
				float num5 = ((num3 == 0) ? 360f : 0f);
				float num6 = (float)num3 * 90f + 360f + num5;
				Debug.Log($"current step: {cubeCurrentStep[j]}, nextStep: {num3}, oldRot: {num4}, nextRot = {num6}");
				cubeCurrentStep[j] = num3;
				paintingCubeButtons[j].targetable = false;
				game.startTimer(new PaintingButtonTimer(j, num4, num6), 0.5f);
			}
		}
		if (targetSwitch == levelExitRef.Get<Switch3D>(0f))
		{
			game.levelCompleted();
		}
		for (int k = 0; k < food.Length; k++)
		{
			if (targetSwitch.transform.parent.gameObject == food[k].Get<GameObject>(0) && switchEvent == Switch3DEvent.Start)
			{
				if (food[k].Get<TweenState>((short)0) != null)
				{
					food[k].Get<TweenState>((short)0).transitionTo("NewState", 4f);
				}
				else
				{
					OnFoodEaten(k);
				}
			}
		}
	}

	public override void onAddToInventory(Item item)
	{
		int num = Array.IndexOf(domeItems, item);
		if (num >= 0 && !pickedUpDomes[num])
		{
			pickedUpDomes[num] = true;
			onDomePickUp();
		}
		if (!domePickedUp && getDomeItem() == item)
		{
			domePickedUp = true;
		}
		if (item.gameObject == getDiningTable().cakePiece)
		{
			getDiningTable().goblets[4].Get<Item>(0).targetable = true;
		}
		if (solvedFamilyTree)
		{
			int num2 = Array.IndexOf(weapons, item);
			if (num2 >= 0 && !pickedUpWeapons[num2])
			{
				pickedUpWeapons[num2] = true;
				characterPoses[num2].targetable = true;
			}
		}
	}

	public override void onTrigger(Trigger trigger, TriggerEvent triggerEvent)
	{
		if (domeRegionTrigger.Get<Trigger>(0f) == trigger)
		{
			onDomeAreaTrigger(triggerEvent);
		}
		if (!(fireLogsTrigger == trigger) || triggerEvent.type != TriggerEventType.Enter)
		{
			return;
		}
		foreach (Interactive item in triggerEvent.interactivesEnteredThisEvent)
		{
			for (int i = 0; i < fireLog.Length; i++)
			{
				if (item.gameObject == fireLog[i].Get<GameObject>(0))
				{
					fireLogVFXTransform.position = fireLog[i].Get<Transform>((short)0).position;
					fireLogVFX.Play();
					PineFmod.playOneShotSoundAttached("event:/Sound Effects/05 Misc/Fire/Item_Fire_Burn", fireLog[i].Get<GameObject>(0));
					fireLog[i].Get<MaterialState>(0f).transitionTo("Dissolve", 0.5f);
					fireLog[i].Get<Item>(0u).targetable = false;
					game.setParent(fireLogVFXTransform, fireLog[i].Get<Transform>((short)0));
					game.saveAchievement("ACHIEVEMENT_BURN_LOG");
				}
			}
			for (int j = 0; j < fireBook.Length; j++)
			{
				if (item.gameObject == fireBook[j].Get<GameObject>(0) || item.transform.parent.gameObject == fireBook[j].Get<GameObject>(0))
				{
					fireLogVFXTransform.position = fireBook[j].Get<Transform>(0u).position;
					fireLogVFX.Play();
					PineFmod.playOneShotSoundAttached("event:/Sound Effects/05 Misc/Fire/Item_Fire_Burn", fireBook[j].Get<GameObject>(0));
					Renderer[] componentsInChildren = fireBook[j].Get<GameObject>(0).GetComponentsInChildren<Renderer>();
					game.startTimer(new BookFireTimer(j, componentsInChildren), 1f);
					fireBook[j].Get<Item>(0f).targetable = false;
					game.setParent(fireLogVFXTransform, fireBook[j].Get<Transform>(0u));
				}
			}
		}
	}

	public override void onMaterialTransitionDone(MaterialState tweenState, string state)
	{
		for (int i = 0; i < fireLog.Length; i++)
		{
			if (tweenState == fireLog[i].Get<MaterialState>(0f))
			{
				game.setParent(fireLogVFXTransform, game.levelContainerTransform);
				fireLog[i].Get<GameObject>(0).SetActive(value: false);
			}
		}
	}

	public override void onPose(NetPlayerId playerId, CharacterPose characterPose, CharacterPoseState poseEvent)
	{
		if (playerId == game.localPlayerData.id)
		{
			switch (poseEvent)
			{
			case CharacterPoseState.TransitionIn:
				game.setPointerReach(1.25f);
				break;
			case CharacterPoseState.TransitionOut:
				game.resetPointerReach();
				break;
			}
		}
	}

	public override void onJigsaw(Jigsaw jigsaw, JigsawPiece piece, JigsawEvent jigsawEvent)
	{
		if (jigsawEvent == JigsawEvent.AllPiecesSnapped)
		{
			game.startTimer(new JigsawSnappedTimer(), 0.2f);
			familyTreeBookstandZoom.targetable = false;
			game.increaseZoomCounter(familyTreeBookstandZoom.gameObject);
		}
	}

	public override void onTweenTransitionDone(TweenState tweenState, string state)
	{
		if (tweenState == enableKingsTS)
		{
			foreach (Ref<GameObject, Collider> kingsGameSlider in kingsGameSliders)
			{
				kingsGameSlider.Get<Collider>(0f).enabled = true;
			}
			kingsGameZoom.targetable = true;
		}
		else if (tweenState == solvedKingsTS)
		{
			kingsGameStatue.targetable = true;
			game.increaseZoomCounter(kingsGameStatue.gameObject);
		}
		else if (tweenState == cakeKnifeBox.Get<TweenState>(0))
		{
			knifeBoxLid.targetable = true;
			foreach (Ref<Item, GameObject, Transform> goblet in getDiningTable().goblets)
			{
				if (goblet.Get<Transform>(0u).IsChildOf(cakeKnifeBox.Get<Transform>(0f)))
				{
					goblet.Get<Item>(0).targetable = true;
				}
			}
		}
		else if (tweenState == candelabraSlotTween)
		{
			float weight = candelabraSlotTween.findStateByName(state).weight;
			if (state == "Wrong" && weight == 1f)
			{
				candelabraSlotTween.transitionTo("Wrong", 2f, 0f);
			}
			else if (state == "Wrong" && weight == 0f)
			{
				candelabraSlot.targetable = true;
				if (candelabraSlot.insertedItem != null)
				{
					candelabraSlot.insertedItem.targetable = true;
					if (game.hasAuthority(candelabraSlot.insertedItem))
					{
						game.transitionItemToInventory(candelabraSlot.insertedItem.gameObject);
					}
				}
			}
			else if (state == "Correct" && weight == 1f)
			{
				rotateExitKey = true;
				doorSequence.Get<Sequence>(0).play(-1f, doorSequence.Get<Sequence>(0).sequenceDuration);
				PineFmod.start(exitDoorInstance);
			}
		}
		else if (tweenState == exitDoorTween)
		{
			game.levelCompleted();
		}
		else if (tweenState == jackalope)
		{
			animalsStatuette.targetable = true;
		}
		else if (tweenState == getDomeHandle())
		{
			getDomeItem().targetable = true;
		}
		for (int i = 0; i < food.Length; i++)
		{
			if (tweenState == food[i].Get<TweenState>((short)0))
			{
				OnFoodEaten(i);
			}
		}
	}

	public override void onSequenceDone(Sequence sequence)
	{
		if (sequence == chestOpenSequence)
		{
			game.startSwitch(chestLid);
			candelabra.targetable = true;
			chestLid.targetable = true;
		}
		else if (sequence == doorSequence.Get<Sequence>(0))
		{
			PineFmod.stop(exitDoorInstance, STOP_MODE.IMMEDIATE);
			PineFmod.playOneShotSoundAttached("event:/Sound Effects/04 Items/Metal/Metal_Hammer_Hit_01", doorSequence.Get<GameObject>(0u));
			solveExitDoor();
		}
	}

	public override void onTimerUpdate(Timer timer)
	{
		if (timer is PaintingButtonTimer { index: var index } paintingButtonTimer)
		{
			float z = Mathf.Lerp(paintingButtonTimer.initialRotation, paintingButtonTimer.endRotation, timer.unitTime);
			paintingCubes[index].Get<Transform>(0f).localRotation = Quaternion.Euler(0f, 0f, z);
		}
		if (timer is FadeTimer fadeTimer)
		{
			if (fadeTimer.fadingOut)
			{
				exposure.fixedExposure.value = Mathf.Lerp(startingExposure, minExposure, fadeTimer.unitTime);
				exposureHaunt.fixedExposure.value = Mathf.Lerp(startingExposureHaunt, minExposure, fadeTimer.unitTime);
			}
			else
			{
				exposure.fixedExposure.value = Mathf.Lerp(minExposure, startingExposure, fadeTimer.unitTime);
				exposureHaunt.fixedExposure.value = Mathf.Lerp(minExposure, startingExposureHaunt, fadeTimer.unitTime);
			}
		}
		if (timer is FoodCheckTimer foodCheckTimer)
		{
			if (foodCheckTimer.foodRB == null)
			{
				foodCheckTimer.foodRB = food[foodCheckTimer.index].Get<GameObject>(0).GetComponent<Rigidbody>();
			}
			else if (foodCheckTimer.foodRB.linearVelocity.magnitude > 0.1f)
			{
				Vector3 linearVelocity = foodCheckTimer.foodRB.linearVelocity;
				if (Vector3.Angle(linearVelocity, foodCheckTimer.foodVelocity) > 10f)
				{
					foodVFXTransform.position = food[foodCheckTimer.index].Get<Transform>(0u).position;
					foodVFX.Play();
					food[foodCheckTimer.index].Get<GameObject>(0).SetActive(value: false);
				}
				foodCheckTimer.foodVelocity = linearVelocity;
			}
		}
		if (timer is BookFireTimer { rendererBook: var rendererBook })
		{
			for (int i = 0; i < rendererBook.Length; i++)
			{
				rendererBook[i].material.SetFloat("_Dissolve", timer.unitTime);
			}
		}
	}

	public override void onTimerDone(Timer timer)
	{
		if (timer is PaintingButtonTimer paintingButtonTimer)
		{
			paintingCubeButtons[paintingButtonTimer.index].targetable = true;
		}
		if (timer is SolvePaintingTimer)
		{
			onSolvePaintingTimerDone();
		}
		if (timer is AnimalsAllPulledUpDelayTimer)
		{
			for (int i = 0; i < animalChains.Length; i++)
			{
				animalChainTweenStates[i].transitionTo("Down", animalChains[i].transitionSpeed);
			}
			game.startTimer(new AnimalsEnableChainsTimer(), 1f / animalChains[0].transitionSpeed);
		}
		else if (timer is AnimalsEnableChainsTimer)
		{
			Switch3D[] array = animalChains;
			for (int j = 0; j < array.Length; j++)
			{
				array[j].targetable = true;
			}
			TweenState[] array2 = animalChainTweenStates;
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j].setWeight("Down", 0f);
			}
		}
		else if (timer is JigsawSnappedTimer)
		{
			familyTreeJigsaw.gameObject.SetActive(value: false);
			familyTreeFixedPiece.SetActive(value: false);
			familyTreeWholePaper.gameObject.SetActive(value: true);
			game.finishPuzzle(Puzzle.FamilyPaper);
		}
		else if (timer is FloorRespawnTimer { item: var item })
		{
			if (!game.isInInventory(item.gameObject) && respawningItems.Contains(item) && game.hasAuthority(item))
			{
				game.transitionItemToInventory(item.gameObject);
			}
			respawningItems.Remove(item);
		}
		else if (timer is LightingTimer)
		{
			isTransitioningToLighting = false;
		}
		else if (timer is DomePickupTimer timer2)
		{
			onDomePickupTimerDone(timer2);
		}
		else if (timer is SolveKnightsTimer solveKnightsTimer)
		{
			onSolveKnightsTimerDone(solveKnightsTimer);
		}
		else if (timer is SolveFamilyTree1Timer)
		{
			fade(0.22f, FadeActionType.ShowWeapon);
		}
		else if (timer is SolveFamilyTree2Timer)
		{
			fade(0.3f, FadeActionType.DomeHead);
		}
		else if (timer is GobletLockDelayTimer)
		{
			solveGoblets();
		}
		else if (timer is KnightDelayTimer)
		{
			solveKnights();
		}
		else if (timer is ChestOpenTimer chestOpenTimer)
		{
			PineFmod.playOneShotSoundAttached("event:/Sound Effects/03 Interactable/Comblocks/Comblock_03", chestLid.gameObject);
			int i2 = chestOpenTimer.i;
			if (i2 < 2)
			{
				game.startTimer(new ChestOpenTimer(i2 + 1), 0.35f);
			}
		}
		else if (timer is KnifeBoxSoundTimer)
		{
			PineFmod.playOneShotSoundAttached("event:/Sound Effects/03 Interactable/Comblocks/Comblock_03", cakeKnifeBox.Get<GameObject>(0u));
		}
		else if (timer is FireCutSceneTimer fireCutSceneTimer)
		{
			if (fireCutSceneTimer.step == 0)
			{
				isFireOff = true;
				fire.Stop();
				fireSound.SetActive(value: false);
				foreach (Ref<Light, GameObject> fireplaceLight in fireplaceLights)
				{
					fireplaceLight.Get<GameObject>(0f).SetActive(value: false);
				}
				firepitSpikes.transitionTo("Down");
				fireplaceObstacle.SetActive(value: false);
				game.startTimer(new FireCutSceneTimer(1), 1.5f);
			}
			else
			{
				fireCutScene.SetActive(value: false);
			}
		}
		else if (timer is FadeTimer fadeTimer)
		{
			if (fadeTimer.fadingOut)
			{
				onFadeAction(fadeTimer.actionType);
				game.startTimer(new FadeTimer(fadeTimer.actionType, fadingOut: false), fadeTimer.duration);
			}
			else
			{
				exposure.fixedExposure.value = startingExposure;
				exposureHaunt.fixedExposure.value = startingExposureHaunt;
				onFadeActionComplete(fadeTimer.actionType);
				isFading = false;
			}
		}
		if (timer is BookFireTimer bookFireTimer)
		{
			game.setParent(fireLogVFXTransform, game.levelContainerTransform);
			fireBook[bookFireTimer.index].Get<GameObject>(0).SetActive(value: false);
		}
	}

	[DebugButton(null, Tint.Default, (PostClickAction)0, 0, new object[] { }, priority = 220, tint = Tint.Red)]
	public override void onConvertToSinglePlayer()
	{
		if (!solvedGoblets)
		{
			solvePlates();
			solveGoblets();
		}
		trayHeadVolume.Get<GameObject>(0).SetActive(value: false);
		if (showDomeHead && !domePickedUp)
		{
			game.setParent(domes[0].Get<Transform>(0u), getDiningTable().lazySusan.Get<Transform>());
			domes[0].Get<GameObject>(0).SetActive(value: true);
			domes[1].Get<GameObject>(0).SetActive(value: false);
		}
		if (isLocalPlayerInDome)
		{
			game.handlePoseLeaveLocal(domeHeadPose);
			isLocalPlayerInDome = false;
		}
		if (solvedDomeSP)
		{
			domes[0].Get<MaterialState>(0f).setState("ShowHint");
			domes[1].Get<MaterialState>(0f).setState("ShowHint");
		}
		if (isFireOff && !solvedPainting)
		{
			firepitSpikes.transitionTo("Down");
		}
		fireplaceObstacle.SetActive(value: false);
		fireplaceTrigger.Get<GameObject>(0u).SetActive(value: false);
		currentPlayerStyle = PlayerCountStyle.One;
	}

	private void transitionToLighting(LightScene targetScene)
	{
		isTransitioningToLighting = true;
		for (int i = 0; i < lightingScenes.Length; i++)
		{
			lightingScenes[i].SetActive(i == (int)targetScene);
		}
		probeRefVolume.lightingScenario = targetScene.ToString();
		targetLighting = targetScene;
		if (targetScene == LightScene.Eventhaunt)
		{
			allFires.SetActive(value: false);
			MeshRenderer[] array = deliciousFoods;
			for (int j = 0; j < array.Length; j++)
			{
				array[j].enabled = false;
			}
			array = rottenFoods;
			for (int j = 0; j < array.Length; j++)
			{
				array[j].enabled = true;
			}
			windows.Get<GameObject>().SetActive(value: false);
			windowsBroken.Get<GameObject>().SetActive(value: true);
		}
		fliesSound.SetActive(targetScene == LightScene.Eventhaunt);
	}

	private void updateTransitionToLighting()
	{
		if (probeRefVolume == null && ProbeReferenceVolume.instance.isInitialized)
		{
			probeRefVolume = ProbeReferenceVolume.instance;
			startingLightingScenario = probeRefVolume.lightingScenario;
			transitionToLighting(targetLighting);
			if (targetLighting >= LightScene.Eventhaunt)
			{
				volumeEventHaunt.weight = 1f;
				postVolume.weight = 0f;
			}
		}
		if (isTransitioningToLighting)
		{
			probeRefVolume.BlendLightingScenario(targetLighting.ToString(), blendingFactor);
			game.startTimer(new LightingTimer(), 2f);
		}
	}

	private void OnFoodEaten(int foodIndex)
	{
		foodVFXTransformInventory.position = food[foodIndex].Get<Transform>(0u).position;
		foodVFXInventory.Play();
		PineFmod.playOneShotSoundAttached("event:/Sound Effects/05 Misc/Voices/Human/Eating", game.playerRig.gameObject);
		if (game.isInInventory(food[foodIndex].Get<GameObject>(0)))
		{
			game.removeItemFromInventory(food[foodIndex].Get<GameObject>(0));
		}
		game.increaseZoomCounter(food[foodIndex].Get<GameObject>(0));
		food[foodIndex].Get<GameObject>(0).SetActive(value: false);
		food[foodIndex].Get<Transform>(0u).localScale = Vector3.zero;
		game.saveAchievement("ACHIEVEMENT_EAT_FOOD");
	}

	private void fade(float duration, FadeActionType actionType)
	{
		if (!isFading)
		{
			isFading = true;
			game.startTimer(new FadeTimer(actionType, fadingOut: true), duration / 2f);
		}
	}

	private void onFadeAction(FadeActionType actionType)
	{
		switch (actionType)
		{
		case FadeActionType.ReleasePlayers:
			releasePlayers();
			break;
		case FadeActionType.ShowWeapon:
			showWeapons();
			break;
		case FadeActionType.DomeHead:
			showDomeHead = true;
			hideFoodItemsOnLazySusan();
			game.setParent(getDome().Get<Transform>(0u), getDiningTable().lazySusan.Get<Transform>());
			getDome().Get<GameObject>(0).SetActive(value: true);
			if (currentPlayerStyle != PlayerCountStyle.One && isLocalPlayerInDome)
			{
				game.handlePoseEnterLocal(domeHeadPose, transitionIn: false);
				trayHeadVolume.Get<GameObject>(0).SetActive(value: true);
			}
			break;
		case FadeActionType.HauntTable:
			volumeEventHaunt.weight = 1f;
			postVolume.weight = 0f;
			getDiningTable().tableCloth.materials = tableClothHauntMaterials;
			break;
		case FadeActionType.SolveKnights:
			solveKnightsSp();
			if (currentPlayerStyle != PlayerCountStyle.One && isLocalPlayerInDome)
			{
				game.handlePoseLeaveLocal(domeHeadPose);
				trayHeadVolume.Get<GameObject>(0).SetActive(value: false);
				getDome().Get<MaterialState>(0f).setState("ShowHint", 0f);
			}
			allFires.SetActive(value: false);
			break;
		}
	}

	private void onFadeActionComplete(FadeActionType actionType)
	{
		switch (actionType)
		{
		case FadeActionType.ShowWeapon:
			game.startTimer(new SolveFamilyTree2Timer(), 0.3f);
			break;
		case FadeActionType.HauntTable:
			game.startTimer(new SolveFamilyTree1Timer(), 0.2f);
			break;
		case FadeActionType.ReleasePlayers:
		case FadeActionType.DomeHead:
		case FadeActionType.SolveKnights:
			break;
		}
	}

	private void OnDestroy()
	{
		if (probeRefVolume != null)
		{
			probeRefVolume.lightingScenario = startingLightingScenario;
		}
	}

	private void initDiningTable()
	{
		diningTables = new DiningTable[4];
		for (int i = 0; i < 4; i++)
		{
			diningTables[i] = new DiningTable
			{
				table = diningTableObjects[i],
				gobletRotatingTablePart = gobletRotatingTableParts[i],
				cakePiece = cakePieces[i],
				cake = cakes[i],
				lazySusan = lazySusans[i],
				tableCloth = tableClothRenderer[i]
			};
		}
		diningTables[0].gobletSlots = gobletSlots1;
		diningTables[0].goblets = goblets1;
		diningTables[0].plateSlots = plateSlots1;
		diningTables[0].dome = domes[0];
		diningTables[0].domeTurnables = domeSoloTurnables;
		diningTables[0].domeItem = domeItems[0];
		diningTables[0].domeHandle = domeLidHandles[0];
		diningTables[1].gobletSlots = gobletSlots2;
		diningTables[1].goblets = goblets2;
		diningTables[1].plateSlots = plateSlots2;
		diningTables[1].dome = domes[1];
		diningTables[1].domeTurnables = domeCoopTurnables;
		diningTables[1].domeItem = domeItems[1];
		diningTables[1].domeHandle = domeLidHandles[1];
		diningTables[2].gobletSlots = gobletSlots3;
		diningTables[2].goblets = goblets3;
		diningTables[2].plateSlots = plateSlots3;
		diningTables[2].dome = domes[1];
		diningTables[2].domeTurnables = domeCoopTurnables;
		diningTables[2].domeItem = domeItems[1];
		diningTables[2].domeHandle = domeLidHandles[1];
		diningTables[3].gobletSlots = gobletSlots4;
		diningTables[3].goblets = goblets4;
		diningTables[3].plateSlots = plateSlots4;
		diningTables[3].dome = domes[1];
		diningTables[3].domeTurnables = domeCoopTurnables;
		diningTables[3].domeItem = domeItems[1];
		diningTables[3].domeHandle = domeLidHandles[1];
		for (int j = 0; j < diningTableObjects.Length; j++)
		{
			diningTableObjects[j].SetActive(j == (int)currentPlayerStyle);
			diningTables[j].gobletRotatingTablePart.setState("Down");
			for (int k = 0; k < diningTables[j].goblets.Length; k++)
			{
				Ref<Item, GameObject, Transform> obj = diningTables[j].goblets[k];
				if (obj.Get<Transform>(0u).IsChildOf(diningTables[j].cake.Get<Transform>(0u)))
				{
					obj.Get<Item>(0).targetable = false;
				}
				if (obj.Get<Transform>(0u).IsChildOf(cakeKnifeBox.Get<Transform>(0f)))
				{
					obj.Get<Item>(0).targetable = false;
				}
				obj.Get<GameObject>(0f).SetActive(j == (int)currentPlayerStyle);
			}
		}
		game.setParent(cakeKnifeBox.Get<Transform>(0f), diningTables[(int)currentPlayerStyle].lazySusan.Get<Transform>());
		if (currentPlayerStyle == PlayerCountStyle.One)
		{
			diningTables[0].gobletRotatingTablePart.setState("Down", 0f);
			MaterialState[] array = puzzleSigns;
			for (int l = 0; l < array.Length; l++)
			{
				array[l].setState("Empty");
			}
		}
		CharacterPose[] array2 = characterPoses;
		foreach (CharacterPose obj2 in array2)
		{
			obj2.targetable = false;
			obj2.lockedInPose = true;
		}
		MeshRenderer[] array3 = rottenFoods;
		for (int l = 0; l < array3.Length; l++)
		{
			array3[l].enabled = false;
		}
		mpRespawnAreaParents[(int)currentPlayerStyle].SetActive(value: true);
	}

	private DiningTable getDiningTable()
	{
		return diningTables[(int)startingPlayerStyle];
	}

	private Ref<GameObject, MaterialState, Transform> getDome()
	{
		if (currentPlayerStyle != PlayerCountStyle.One)
		{
			return domes[1];
		}
		return domes[0];
	}

	private Turnable[] getDomeTurnables()
	{
		if (currentPlayerStyle != PlayerCountStyle.One)
		{
			return domeCoopTurnables;
		}
		return domeSoloTurnables;
	}

	private TweenState getDomeHandle()
	{
		if (currentPlayerStyle != PlayerCountStyle.One)
		{
			return domeLidHandles[1];
		}
		return domeLidHandles[0];
	}

	private Item getDomeItem()
	{
		if (currentPlayerStyle != PlayerCountStyle.One)
		{
			return domeItems[1];
		}
		return domeItems[0];
	}

	private bool checkKnifeBox()
	{
		return Array.TrueForAll(knifeBoxSlots, (Slot x) => x.isUnlocked);
	}

	[DebugButton(null, Tint.Green, (PostClickAction)0, 19, new object[] { })]
	private void solveKnifeBox()
	{
		game.changeExamineRotation(cakeKnifeBox.Get<GameObject>(0u), new Vector2(0f, -30f), 0.5f);
		cakeKnifeBox.Get<TweenState>(0).transitionTo("Open", 1f, 1f, playSound: true, 0.25f);
		knifeBoxLid.tweenState.transitionTo("Down", 0.25f, 0.25f, playSound: true, 0.65f);
		game.startTimer(new KnifeBoxSoundTimer(), 0.25f, 0.25f);
		Slot[] array = knifeBoxSlots;
		foreach (Slot slot in array)
		{
			slot.targetable = false;
			if (slot.insertedItem != null)
			{
				slot.insertedItem.targetable = false;
			}
		}
		game.finishPuzzle(Puzzle.TileBox);
	}

	private bool checkGoblets()
	{
		return Array.TrueForAll(getDiningTable().gobletSlots, (Slot x) => x.isUnlocked);
	}

	[DebugButton(null, Tint.Green, (PostClickAction)0, 19, new object[] { })]
	private void solveGoblets()
	{
		if (solvedGoblets)
		{
			return;
		}
		solvedGoblets = true;
		SpawnPointToPose[] array = spawnPosePoints;
		foreach (SpawnPointToPose spawnPointToPose in array)
		{
			if (spawnPointToPose != null)
			{
				spawnPointToPose.active = false;
			}
		}
		Slot[] gobletSlots = getDiningTable().gobletSlots;
		foreach (Slot slot in gobletSlots)
		{
			slot.targetable = false;
			if (slot.insertedItem != null)
			{
				slot.insertedItem.targetable = false;
			}
		}
		Debug.Log("Solved the goblets");
		game.finishPuzzle(Puzzle.Cups);
		transitionToLighting(LightScene.Normal);
		fade(1f, FadeActionType.ReleasePlayers);
	}

	[DebugButton(null, Tint.Default, PostClickAction.ReturnToGame | PostClickAction.HideButton, 1, new object[] { })]
	private void releasePlayers()
	{
		CharacterPose[] array = characterPoses;
		foreach (CharacterPose characterPose in array)
		{
			game.handlePoseLeaveLocal(characterPose);
			characterPose.targetable = true;
			characterPose.lockedInPose = false;
		}
		GameObject[] array2 = neckLocks;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].SetActive(value: false);
		}
		array2 = neckUnlocks;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].SetActive(value: true);
		}
		familyTreeBookstandZoom.targetable = true;
		pinBox.targetable = true;
		foreach (Interactive allNonTableInteractive in allNonTableInteractives)
		{
			allNonTableInteractive.targetable = true;
		}
		floorCollisionData.Get<GameObject>(0f).SetActive(value: false);
		p3ExtraPlate.targetable = true;
	}

	private bool checkPlates()
	{
		if (Array.TrueForAll(getDiningTable().plateSlots, (Slot x) => x.isUnlocked))
		{
			return game.getPlayerCount() > 1;
		}
		return false;
	}

	[DebugButton(null, Tint.Green, (PostClickAction)0, 20, new object[] { })]
	private void solvePlates()
	{
		Slot[] plateSlots = getDiningTable().plateSlots;
		foreach (Slot slot in plateSlots)
		{
			slot.targetable = false;
			if (slot.insertedItem != null)
			{
				slot.insertedItem.targetable = false;
			}
		}
		getDiningTable().gobletRotatingTablePart.transitionTo("Default");
		Debug.Log("Solved the plates");
		game.finishPuzzle(Puzzle.Plates);
	}

	private bool checkDomeSolution()
	{
		Turnable[] domeTurnables = getDomeTurnables();
		bool flag = false;
		Turnable[] array = domeTurnables;
		foreach (Turnable turnable in array)
		{
			flag |= game.isAnyPlayerInteracting(turnable.gameObject);
		}
		int value = domeTurnables[2].value;
		int num = (domeTurnables[1].value - value + domeTurnables[0].steps) % domeTurnables[0].steps;
		int num2 = (domeTurnables[0].value - value + domeTurnables[0].steps) % domeTurnables[0].steps;
		if (!flag && num2 == domeSolution[0])
		{
			return num == domeSolution[1];
		}
		return false;
	}

	[DebugButton(null, Tint.Green, (PostClickAction)0, 14, new object[] { })]
	private void solveDome()
	{
		solvedDomeSP = true;
		Debug.Log("Solved the dome");
		Turnable[] domeTurnables = getDomeTurnables();
		for (int i = 0; i < domeTurnables.Length; i++)
		{
			domeTurnables[i].targetable = false;
		}
		domeSolved = true;
		enableDomePickup();
		if (currentPlayerStyle == PlayerCountStyle.One)
		{
			getDome().Get<MaterialState>(0f).setState("ShowHint");
		}
		else if (isLocalPlayerInDome)
		{
			getDome().Get<MaterialState>(0f).transitionTo("ShowHint");
			trayHeadVolume.Get<GameObject>(0).SetActive(value: true);
		}
	}

	private void showWeapons()
	{
		Item[] array = weapons;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].gameObject.SetActive(value: true);
		}
		GameObject[] array2 = staticWeapons;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].SetActive(value: false);
		}
		Slot[] array3 = weaponSlots;
		for (int i = 0; i < array3.Length; i++)
		{
			array3[i].gameObject.SetActive(value: true);
		}
		array2 = weaponSounds;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].SetActive(value: true);
		}
		array2 = chairBacks;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].SetActive(value: false);
		}
		chairsDamagedParts.SetActive(value: true);
		weaponDecals.SetActive(value: true);
	}

	private void initPinBox()
	{
		pinDatas = new List<PinData>();
		addPinData(pins[0], PinColor.Blue, PinPattern.Snowflake);
		addPinData(pins[1], PinColor.Green, PinPattern.Snowflake);
		addPinData(pins[2], PinColor.Blue, PinPattern.Pizza);
		addPinData(pins[3], PinColor.Green, PinPattern.Flower);
		addPinData(pins[4], PinColor.Blue, PinPattern.Cross);
		addPinData(pins[5], PinColor.Red, PinPattern.Flower);
		addPinData(pins[6], PinColor.Blue, PinPattern.Flower);
		PinData pinData = addPinData(null, PinColor.Red, PinPattern.Cross);
		PinData pinData2 = addPinData(null, PinColor.Green, PinPattern.Cross);
		PinData pinData3 = addPinData(null, PinColor.Red, PinPattern.Pizza);
		PinData pinData4 = addPinData(null, PinColor.Red, PinPattern.Snowflake);
		PinData pinData5 = addPinData(null, PinColor.Green, PinPattern.Pizza);
		pinHoleDatas = new PinHoleData[12];
		int pinIndex = 0;
		initPinHoleData(null, pinSlots[0]);
		initPinHoleData(null, pinSlots[1]);
		initPinHoleData(pinData, null);
		initPinHoleData(null, pinSlots[2]);
		initPinHoleData(null, pinSlots[3]);
		initPinHoleData(pinData2, null);
		initPinHoleData(pinData3, null);
		initPinHoleData(null, pinSlots[4]);
		initPinHoleData(null, pinSlots[5]);
		initPinHoleData(pinData4, null);
		initPinHoleData(null, pinSlots[6]);
		initPinHoleData(pinData5, null);
		for (int i = 0; i < 12; i++)
		{
			pinHoleDatas[i].data = new List<PinHoleData>();
			pinHoleDatas[i].stateIndex = new List<int>();
			if (i + 3 < 12)
			{
				pinHoleDatas[i].data.Add(pinHoleDatas[i + 3]);
				pinHoleDatas[i].stateIndex.Add(i + 2);
			}
			if (i < 3 || i > 8)
			{
				int num = 2;
				if (i < 3)
				{
					num = 0;
				}
				if (i + 1 < 12 && i != 2)
				{
					pinHoleDatas[i].data.Add(pinHoleDatas[i + 1]);
					pinHoleDatas[i].stateIndex.Add(i + num);
				}
			}
		}
		initedPinDatas = true;
		checkPinBoxSolution();
		PinData addPinData(GameObject _pin, PinColor color, PinPattern pattern)
		{
			PinData pinData6 = new PinData
			{
				pin = _pin,
				color = color,
				pattern = pattern
			};
			pinDatas.Add(pinData6);
			return pinData6;
		}
		void initPinHoleData(PinData pin, Slot slot)
		{
			if (pinIndex <= 12)
			{
				pinHoleDatas[pinIndex++] = new PinHoleData
				{
					pin = pin,
					slot = slot
				};
			}
		}
	}

	private bool checkPinBoxSolution()
	{
		if (!initedPinDatas)
		{
			Debug.Log("checkPinBoxSolution before init");
			return false;
		}
		bool flag = true;
		PinHoleData[] array = pinHoleDatas;
		foreach (PinHoleData pinHoleData in array)
		{
			for (int j = 0; j < pinHoleData.data.Count; j++)
			{
				PinHoleData pinHoleData2 = pinHoleData.data[j];
				if (pinHoleData.pin == null || pinHoleData2.pin == null)
				{
					flag = false;
				}
				else if (flag)
				{
					flag = pinHoleData.pin.color == pinHoleData2.pin.color || pinHoleData.pin.pattern == pinHoleData2.pin.pattern;
				}
			}
		}
		return flag;
	}

	private void updatePinBoxVisuals()
	{
		PinHoleData[] array = pinHoleDatas;
		foreach (PinHoleData pinHoleData in array)
		{
			for (int j = 0; j < pinHoleData.data.Count; j++)
			{
				PinHoleData pinHoleData2 = pinHoleData.data[j];
				TweenState tweenState = pinBoxStates[pinHoleData.stateIndex[j]];
				if (pinHoleData.pin == null || pinHoleData2.pin == null)
				{
					tweenState.transitionTo("Default", 3f);
					continue;
				}
				bool flag = pinHoleData.pin.color == pinHoleData2.pin.color || pinHoleData.pin.pattern == pinHoleData2.pin.pattern;
				tweenState.transitionTo(flag ? "Correct" : "Wrong", 3f);
			}
		}
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 16, new object[] { })]
	private void solvePinBox()
	{
		if (isPinBoxSolved)
		{
			return;
		}
		isPinBoxSolved = true;
		pinBox.targetable = false;
		game.increaseZoomCounter(pinBox.gameObject);
		Slot[] array = pinSlots;
		foreach (Slot slot in array)
		{
			slot.targetable = false;
			if (slot.insertedItem != null)
			{
				slot.insertedItem.targetable = false;
			}
		}
		pinBoxDrawer.transitionTo("Down", 1f, 1f, playSound: true, 1f);
		game.finishPuzzle(Puzzle.Pins);
	}

	private bool checkKingsGameSolution()
	{
		Dictionary<(KingsPieceType, KingsColorType), MaterialState> dict = new Dictionary<(KingsPieceType, KingsColorType), MaterialState>();
		for (int i = 0; i < kingsSlidableGraph.piecesList.Count; i++)
		{
			kingsSlidableGraph.tryGetNode(kingsSlidableGraph.piecesList[i], out var node);
			int num = kingsSlidableGraph.nodes.IndexOf(node);
			int num2 = num / 5;
			int num3 = num % 5;
			countPiece(kingsPieces[i].topLeft, kingsKingdoms[num2][num3], kingsPiecesImages[i * 4]);
			countPiece(kingsPieces[i].topRight, kingsKingdoms[num2][num3 + 1], kingsPiecesImages[i * 4 + 1]);
			countPiece(kingsPieces[i].bottomLeft, kingsKingdoms[num2 + 1][num3], kingsPiecesImages[i * 4 + 2]);
			countPiece(kingsPieces[i].bottomRight, kingsKingdoms[num2 + 1][num3 + 1], kingsPiecesImages[i * 4 + 3]);
		}
		MaterialState[] array = kingsPiecesImages;
		foreach (MaterialState materialState in array)
		{
			materialState.transitionTo(dict.ContainsValue(materialState) ? "Shine" : "Default");
		}
		return dict.Count == 16;
		void countPiece(KingsPieceType sign, KingsColorType color, MaterialState state)
		{
			if (!dict.ContainsKey((sign, color)))
			{
				dict[(sign, color)] = state;
			}
			else
			{
				dict[(sign, color)] = null;
			}
		}
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 16, new object[] { })]
	private void solveKingsGame()
	{
		foreach (Ref<GameObject, Collider> kingsGameSlider in kingsGameSliders)
		{
			kingsGameSlider.Get<Collider>(0f).enabled = false;
		}
		kingsGameZoom.targetable = false;
		solvedKingsTS.transitionToDuration("Open", 2f, 1f, playSound: true, 0.1f);
		game.finishPuzzle(Puzzle.BoardGame);
	}

	private bool checkFamilyTree()
	{
		if (!solvedFamilyTree)
		{
			return Array.TrueForAll(statueSlots, (Slot x) => x.isUnlocked);
		}
		return false;
	}

	[DebugButton(null, Tint.Green, PostClickAction.ReturnToGame, 15, new object[] { })]
	private void solveFamilyTree()
	{
		if (solvedFamilyTree)
		{
			return;
		}
		solvedFamilyTree = true;
		Slot[] array = statueSlots;
		foreach (Slot slot in array)
		{
			slot.targetable = false;
			if (slot.insertedItem != null)
			{
				slot.insertedItem.targetable = false;
			}
		}
		for (int j = 0; j < characterPoses.Length - 1; j++)
		{
			game.handlePoseLeaveLocal(characterPoses[j]);
			characterPoses[j].targetable = false;
		}
		transitionToLighting(LightScene.Eventhaunt);
		fade(0.3f, FadeActionType.HauntTable);
		game.finishPuzzle(Puzzle.FamilyTree);
	}

	private void putPlayerInDome()
	{
		isLocalPlayerInDome = currentPlayerStyle != PlayerCountStyle.One && game.session.isLocalPlayer(treeSlotLastPlayer);
		if (DEBUGPlayerCount > 0)
		{
			isLocalPlayerInDome = !isLocalPlayerInDome;
		}
		Debug.Log(game.localPlayerData.id?.ToString() + " isInDome: " + isLocalPlayerInDome);
	}

	private void hideFoodItemsOnLazySusan()
	{
		foreach (Interactive item2 in itemsOnLazySusan)
		{
			Item item = item2 as Item;
			if (item != null && item.slot == null)
			{
				if ((!(item.gameObject == cakeKnifeBox.Get<GameObject>(0u)) || !solvedGoblets) && item.itemType != ItemType.Undefined && game.hasAuthority(item2.gameObject))
				{
					game.addItemToInventory(item2.gameObject);
				}
				else
				{
					item2.gameObject.SetActive(value: false);
				}
			}
		}
		domeRegionTrigger.Get<GameObject>(0).SetActive(value: false);
	}

	private void onDomeAreaTrigger(TriggerEvent triggerEvent)
	{
		itemsOnLazySusan = triggerEvent.interactivesInTrigger;
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 18, new object[] { })]
	private void solveAnimals()
	{
		if (!solvedAnimals)
		{
			solvedAnimals = true;
			Switch3D[] array = animalChains;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].targetable = false;
			}
			jackalope.transitionTo("Down", 1f, 1f, playSound: true, 1f);
			game.finishPuzzle(Puzzle.AnimalHeads);
		}
	}

	private bool checkKnightsSolved()
	{
		if (!domeSolved)
		{
			return false;
		}
		bool flag = true;
		flag &= weaponSlots[1].insertedItem == null;
		int i;
		for (i = 0; i < weaponSlots.Length; i++)
		{
			if (i != 1)
			{
				if (weaponSlots[i].insertedItem == null)
				{
					flag = false;
					break;
				}
				flag &= Array.Exists(weaponSlots[i].acceptItems, (Item x) => x == weaponSlots[i].insertedItem);
			}
		}
		return flag;
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 13, new object[] { })]
	private void solveKnights()
	{
		Slot[] array = weaponSlots;
		foreach (Slot slot in array)
		{
			slot.targetable = false;
			if (slot.insertedItem != null)
			{
				slot.insertedItem.targetable = false;
			}
		}
		TweenState[] array2 = knights;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].transitionTo("Down", 0.2f);
		}
		GameObject[] array3 = knightSounds;
		for (int i = 0; i < array3.Length; i++)
		{
			array3[i].SetActive(value: true);
		}
		game.startTimer(new SolveKnightsTimer(), 5.2f);
		game.finishPuzzle(Puzzle.Knights);
	}

	private void onSolveKnightsTimerDone(SolveKnightsTimer solveKnightsTimer)
	{
		ParticleSystem[] array = allFireParticles;
		foreach (ParticleSystem particleSystem in array)
		{
			if (particleSystem.isPlaying)
			{
				particleSystem.Stop();
			}
		}
		fireLogsTriggerGO.SetActive(value: false);
		fade(1f, FadeActionType.SolveKnights);
		transitionToLighting(LightScene.EventWindow);
	}

	private void solveKnightsSp()
	{
		solvedKnightsSp = true;
		getDome().Get<GameObject>(0).SetActive(value: false);
		showDomeHead = false;
		fireCutScene.SetActive(value: true);
		game.startTimer(new FireCutSceneTimer(0), 2f);
		Switch3D[] array = paintingButtons;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = true;
		}
		fireplacePainting.targetable = true;
	}

	private void enableDomePickup()
	{
		getDomeHandle().transitionTo("Up");
	}

	private void onDomePickUp()
	{
		if (currentPlayerStyle != PlayerCountStyle.One)
		{
			game.startTimer(new DomePickupTimer(), 5f);
		}
	}

	private void onDomePickupTimerDone(DomePickupTimer timer)
	{
		allFires.SetActive(value: false);
	}

	private void trapPersonInFireplace()
	{
		if (currentPlayerStyle != PlayerCountStyle.One)
		{
			Debug.Log($"Trap player: {game.localPlayerData.id} ");
			firepitSpikes.transitionTo("Up", 1.5f);
			fireplaceObstacle.SetActive(value: true);
			trappedPlayer = true;
		}
	}

	private void teleportPersonOutOfFireplace()
	{
		if (currentPlayerStyle != PlayerCountStyle.One)
		{
			Debug.Log($"Teleport player out: {game.localPlayerData.id} ");
			game.teleportPlayer(fireplaceTeleportPoint.position);
		}
	}

	private bool checkPaintingSolution()
	{
		bool flag = true;
		for (int i = 0; i < paintingCubes.Length; i++)
		{
			flag &= cubeCurrentStep[i] == 0;
		}
		return flag;
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 12, new object[] { })]
	private void solvePainting()
	{
		if (!solvedPainting)
		{
			solvedPainting = true;
			game.startTimer(new SolvePaintingTimer(), 0.5f);
		}
	}

	private void onSolvePaintingTimerDone()
	{
		zoomablePicture.transitionTo("Solved");
		chestKey.targetable = true;
		fireplacePainting.targetable = false;
		firepitSpikes.transitionTo("Down");
		fireplaceObstacle.SetActive(value: false);
		Switch3D[] array = paintingCubeButtons;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = false;
		}
		game.finishPuzzle(Puzzle.Portrait);
	}

	private bool checkKeySolution()
	{
		for (int i = 0; i < keySlidables.Length; i++)
		{
			if (keySlidables[i].closestSnapPointIndex != chestKeySolution[i])
			{
				return false;
			}
		}
		return true;
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 11, new object[] { })]
	private void solveChest()
	{
		if (!solvedChest)
		{
			solvedChest = true;
			chestKeySlot.targetable = false;
			chestKeyWrongSlot.targetable = false;
			if (chestKeySlot.insertedItem != null)
			{
				chestKeySlot.insertedItem.targetable = false;
			}
			Debug.Log("Solved chest");
			game.finishPuzzle(Puzzle.LetterKey);
			chestOpenSequence.play(-1f, chestOpenSequence.sequenceDuration, 2f);
			game.startTimer(new ChestOpenTimer(0), 0.5f);
		}
	}

	private bool checkExitKeySolution()
	{
		return Array.TrueForAll(exitKeySwitches, (Switch3D sw) => sw.state == Switch3DState.On);
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 10, new object[] { })]
	private void solveCandelabra()
	{
		if (!solvedCandelabra)
		{
			solvedCandelabra = true;
			if (candelabraSlot.insertedItem != null)
			{
				candelabraSlot.insertedItem.targetable = false;
			}
			candelabraSlot.targetable = false;
			candelabraSlotTween.setState("Wrong", 0f);
			candelabraSlotTween.transitionTo("Correct", 1f, 1f, playSound: true, 0.3f);
		}
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 10, new object[] { })]
	private void solveExitDoor()
	{
		if (!solvedExitDoor)
		{
			solvedExitDoor = true;
			rotateExitKey = false;
			exitDoorTween.transitionTo("Open");
			levelExitRef.Get<GameObject>(0).SetActive(value: true);
			game.finishPuzzle(Puzzle.MasterKey);
		}
	}

	[DebugButton("Next Table", Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void debugNextTable()
	{
		currentPlayerStyle = (PlayerCountStyle)((int)(currentPlayerStyle + 1) % Enum.GetNames(typeof(PlayerCountStyle)).Length);
		for (int i = 0; i < diningTableObjects.Length; i++)
		{
			diningTableObjects[i].SetActive(i == (int)currentPlayerStyle);
		}
		MaterialState[] array = puzzleSigns;
		for (int j = 0; j < array.Length; j++)
		{
			array[j].setState("Empty", (currentPlayerStyle == PlayerCountStyle.One) ? 1 : 0);
		}
	}

	[DebugButton("Get jigsaw pieces", Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void debugGetJigsawPieces()
	{
		foreach (Item applicableItem in familyTreeJigsaw.applicableItems)
		{
			game.addItemToInventory(applicableItem.gameObject);
		}
	}

	[DebugButton("Get goblets", Tint.Default, PostClickAction.HideButton, 0, new object[] { })]
	private void debugGetGoblets()
	{
		foreach (Ref<Item, GameObject, Transform> goblet in getDiningTable().goblets)
		{
			goblet.Get<Item>(0).targetable = true;
			game.addItemToInventory(goblet.Get<GameObject>(0f));
		}
	}

	[DebugButton("Get Pins", Tint.Default, PostClickAction.HideButton, 0, new object[] { })]
	private void debugGetPins()
	{
		GameObject[] array = pins;
		foreach (GameObject gameObject in array)
		{
			gameObject.GetComponent<Item>().targetable = true;
			game.addItemToInventory(gameObject);
		}
	}

	[DebugButton("Get Wooden Statuettes", Tint.Default, PostClickAction.HideButton, 0, new object[] { })]
	private void debugGetWoodenStatuettes()
	{
		Item[] acceptItems = statueSlots[0].acceptItems;
		foreach (Item item in acceptItems)
		{
			item.GetComponent<Item>().targetable = true;
			game.addItemToInventory(item.gameObject);
		}
		acceptItems = statueSlots[0].rejectItems;
		foreach (Item item2 in acceptItems)
		{
			item2.GetComponent<Item>().targetable = true;
			game.addItemToInventory(item2.gameObject);
		}
	}

	[DebugButton("Get Weapons", Tint.Default, PostClickAction.HideButton, 0, new object[] { })]
	private void debugGetWeapons()
	{
		Item[] array = weapons;
		foreach (Item item in array)
		{
			item.GetComponent<Item>().targetable = true;
			item.gameObject.SetActive(value: true);
			game.addItemToInventory(item.gameObject);
		}
		GameObject[] array2 = chairBacks;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].SetActive(value: false);
		}
		chairsDamagedParts.SetActive(value: true);
		weaponDecals.SetActive(value: true);
	}

	[DebugButton("Get Chest Key", Tint.Default, PostClickAction.HideButton, 0, new object[] { })]
	private void debugGetChestKey()
	{
		chestKey.GetComponent<Item>().targetable = true;
		game.addItemToInventory(chestKey.gameObject);
	}

	[DebugButton("Get Exit Key", Tint.Default, PostClickAction.HideButton, 0, new object[] { })]
	private void debugGetExitKey()
	{
		candelabra.GetComponent<Item>().targetable = true;
		game.addItemToInventory(candelabra.gameObject);
	}

	public virtual void save(FastBinaryWriter writer)
	{
		int value = (int)targetLighting;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in isTransitioningToLighting, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in isFading, default(FastBinaryWriter.ForPrimitives));
		writer.WriteList(respawningItems, delegate(FastBinaryWriter w, Item e)
		{
			w.WriteComponent(e);
		});
		writer.Write(in solvedGoblets, default(FastBinaryWriter.ForPrimitives));
		writer.WriteArray(pickedUpDomes, delegate(FastBinaryWriter w, bool e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteHashSet(itemsOnLazySusan, delegate(FastBinaryWriter w, Interactive e)
		{
			w.WriteComponent(e);
		});
		writer.Write(in solvedDomeSP, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in solvedKnightsSp, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in showDomeHead, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in domePickedUp, default(FastBinaryWriter.ForPrimitives));
		value = (int)currentPlayerStyle;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		value = (int)startingPlayerStyle;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in currentAnimalsPull, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in solvedAnimals, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in domeSolved, default(FastBinaryWriter.ForPrimitives));
		writer.WriteArray(pickedUpWeapons, delegate(FastBinaryWriter w, bool e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteArray(allFireParticles, delegate(FastBinaryWriter w, ParticleSystem e)
		{
			w.WriteComponent(e);
		});
		writer.WriteNetPlayerId(treeSlotLastPlayer);
		writer.Write(in solvedFamilyTree, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in isLocalPlayerInDome, default(FastBinaryWriter.ForPrimitives));
		writer.WriteArray(cubeCurrentStep, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.Write(in solvedPainting, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in trappedPlayer, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in isFireOff, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in paintingCubeSpeed, default(FastBinaryWriter.ForPrimitives));
		writer.WriteArray(chestKeySolution, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.Write(in chestCurrentSolved, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in solvedChest, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in solvedCandelabra, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in solvedExitDoor, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in rotateExitKey, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in candelabraSlotTried, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in rotatingSpeed, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void load(FastBinaryReader reader)
	{
		targetLighting = (LightScene)reader.ReadInt32();
		isTransitioningToLighting = reader.ReadBoolean();
		isFading = reader.ReadBoolean();
		respawningItems = reader.ReadList((FastBinaryReader r) => r.ReadComponent<Item>());
		solvedGoblets = reader.ReadBoolean();
		pickedUpDomes = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		itemsOnLazySusan = reader.ReadHashSet((FastBinaryReader r) => r.ReadComponent<Interactive>());
		solvedDomeSP = reader.ReadBoolean();
		solvedKnightsSp = reader.ReadBoolean();
		showDomeHead = reader.ReadBoolean();
		domePickedUp = reader.ReadBoolean();
		currentPlayerStyle = (PlayerCountStyle)reader.ReadInt32();
		startingPlayerStyle = (PlayerCountStyle)reader.ReadInt32();
		currentAnimalsPull = reader.ReadInt32();
		solvedAnimals = reader.ReadBoolean();
		domeSolved = reader.ReadBoolean();
		pickedUpWeapons = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		allFireParticles = reader.ReadArray((FastBinaryReader r) => r.ReadComponent<ParticleSystem>());
		treeSlotLastPlayer = reader.ReadNetPlayerId();
		solvedFamilyTree = reader.ReadBoolean();
		isLocalPlayerInDome = reader.ReadBoolean();
		cubeCurrentStep = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		solvedPainting = reader.ReadBoolean();
		trappedPlayer = reader.ReadBoolean();
		isFireOff = reader.ReadBoolean();
		paintingCubeSpeed = reader.ReadSingle();
		chestKeySolution = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		chestCurrentSolved = reader.ReadBoolean();
		solvedChest = reader.ReadBoolean();
		solvedCandelabra = reader.ReadBoolean();
		solvedExitDoor = reader.ReadBoolean();
		rotateExitKey = reader.ReadBoolean();
		candelabraSlotTried = reader.ReadBoolean();
		rotatingSpeed = reader.ReadSingle();
	}

	public virtual void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		int position = reader.Position;
		LightScene lightScene = (LightScene)reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "targetLighting",
			fieldValue = $"{lightScene}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "isTransitioningToLighting",
			fieldValue = $"{flag}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag2 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "isFading",
			fieldValue = $"{flag2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<Item> list = reader.ReadList((FastBinaryReader r) => r.ReadComponent<Item>());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "respawningItems[" + ((list == null) ? string.Empty : list.Count.ToString()) + "]",
			fieldValue = (((list == null) ? "null" : string.Join(", ", list)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag3 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "solvedGoblets",
			fieldValue = $"{flag3}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool[] array = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "pickedUpDomes[" + ((array == null) ? string.Empty : array.Length.ToString()) + "]",
			fieldValue = (((array == null) ? "null" : string.Join(", ", array)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		HashSet<Interactive> hashSet = reader.ReadHashSet((FastBinaryReader r) => r.ReadComponent<Interactive>());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "itemsOnLazySusan[" + ((hashSet == null) ? string.Empty : hashSet.Count.ToString()) + "]",
			fieldValue = (((hashSet == null) ? "null" : string.Join(", ", hashSet)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag4 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "solvedDomeSP",
			fieldValue = $"{flag4}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag5 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "solvedKnightsSp",
			fieldValue = $"{flag5}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag6 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "showDomeHead",
			fieldValue = $"{flag6}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag7 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "domePickedUp",
			fieldValue = $"{flag7}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		PlayerCountStyle playerCountStyle = (PlayerCountStyle)reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "currentPlayerStyle",
			fieldValue = $"{playerCountStyle}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		PlayerCountStyle playerCountStyle2 = (PlayerCountStyle)reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "startingPlayerStyle",
			fieldValue = $"{playerCountStyle2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "currentAnimalsPull",
			fieldValue = $"{num}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag8 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "solvedAnimals",
			fieldValue = $"{flag8}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag9 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "domeSolved",
			fieldValue = $"{flag9}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool[] array2 = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "pickedUpWeapons[" + ((array2 == null) ? string.Empty : array2.Length.ToString()) + "]",
			fieldValue = (((array2 == null) ? "null" : string.Join(", ", array2)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		ParticleSystem[] array3 = reader.ReadArray((FastBinaryReader r) => r.ReadComponent<ParticleSystem>());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "allFireParticles[" + ((array3 == null) ? string.Empty : array3.Length.ToString()) + "]",
			fieldValue = (((array3 == null) ? "null" : string.Join(", ", (IEnumerable<ParticleSystem>)array3)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		NetPlayerId arg = reader.ReadNetPlayerId();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "treeSlotLastPlayer",
			fieldValue = $"{arg}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag10 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "solvedFamilyTree",
			fieldValue = $"{flag10}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag11 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "isLocalPlayerInDome",
			fieldValue = $"{flag11}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int[] array4 = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "cubeCurrentStep[" + ((array4 == null) ? string.Empty : array4.Length.ToString()) + "]",
			fieldValue = (((array4 == null) ? "null" : string.Join(", ", array4)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag12 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "solvedPainting",
			fieldValue = $"{flag12}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag13 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "trappedPlayer",
			fieldValue = $"{flag13}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag14 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "isFireOff",
			fieldValue = $"{flag14}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num2 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "paintingCubeSpeed",
			fieldValue = $"{num2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int[] array5 = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "chestKeySolution[" + ((array5 == null) ? string.Empty : array5.Length.ToString()) + "]",
			fieldValue = (((array5 == null) ? "null" : string.Join(", ", array5)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag15 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "chestCurrentSolved",
			fieldValue = $"{flag15}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag16 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "solvedChest",
			fieldValue = $"{flag16}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag17 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "solvedCandelabra",
			fieldValue = $"{flag17}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag18 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "solvedExitDoor",
			fieldValue = $"{flag18}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag19 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "rotateExitKey",
			fieldValue = $"{flag19}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag20 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "candelabraSlotTried",
			fieldValue = $"{flag20}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num3 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "rotatingSpeed",
			fieldValue = $"{num3}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
	}

	public override Timer getTimer(byte id)
	{
		return id switch
		{
			0 => new AnimalsAllPulledUpDelayTimer(), 
			1 => new AnimalsEnableChainsTimer(), 
			2 => new JigsawSnappedTimer(), 
			3 => new FloorRespawnTimer(), 
			4 => new PaintingButtonTimer(), 
			5 => new SolvePaintingTimer(), 
			6 => new LightingTimer(), 
			7 => new DomePickupTimer(), 
			8 => new SolveKnightsTimer(), 
			9 => new FireCutSceneTimer(), 
			10 => new SolveFamilyTree1Timer(), 
			11 => new SolveFamilyTree2Timer(), 
			12 => new GobletLockDelayTimer(), 
			13 => new KnightDelayTimer(), 
			14 => new ChestOpenTimer(), 
			15 => new KnifeBoxSoundTimer(), 
			16 => new FadeTimer(), 
			17 => new FoodCheckTimer(), 
			18 => new BookFireTimer(), 
			_ => null, 
		};
	}
}
