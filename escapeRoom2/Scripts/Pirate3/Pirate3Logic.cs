using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FMOD;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.VFX;

public class Pirate3Logic : LevelLogic, ISaveable
{
	public sealed class DartArrivedTransition : Transition
	{
		public int index;

		public Transform newParent;

		public override byte getTypeId()
		{
			return 0;
		}

		public DartArrivedTransition()
		{
		}

		public DartArrivedTransition(int index, Transform newParent)
		{
			this.index = index;
			this.newParent = newParent;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			base.writeData(writer);
			writer.Write(in index, default(FastBinaryWriter.ForPrimitives));
			writer.WriteComponent(newParent);
		}

		public override void readData(FastBinaryReader reader)
		{
			base.readData(reader);
			index = reader.ReadInt32();
			newParent = reader.ReadComponent<Transform>();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("index: " + $"{index}");
			stringBuilder.Append("newParent: " + $"{newParent}");
			return stringBuilder.ToString();
		}
	}

	public sealed class GraphInitTimer : Timer
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

	public sealed class CutsceneMiddleTimer : Timer
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

	public sealed class GetCrowbarBackTimer : Timer
	{
		public override byte getTypeId()
		{
			return 3;
		}

		public override void writeData(FastBinaryWriter writer)
		{
		}

		public override void readData(FastBinaryReader reader)
		{
		}
	}

	public sealed class SetSymbolsCodeTransition : Transition
	{
		public bool reset;

		public override byte getTypeId()
		{
			return 4;
		}

		public SetSymbolsCodeTransition()
		{
		}

		public SetSymbolsCodeTransition(bool reset)
		{
			this.reset = reset;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			base.writeData(writer);
			writer.Write(in reset, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			base.readData(reader);
			reset = reader.ReadBoolean();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("reset: " + $"{reset}");
			return stringBuilder.ToString();
		}
	}

	public sealed class ResetSymbolsSliderTransition : Transition
	{
		public override byte getTypeId()
		{
			return 5;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			base.writeData(writer);
		}

		public override void readData(FastBinaryReader reader)
		{
			base.readData(reader);
		}
	}

	public sealed class FishingRodFlyInTransition : Transition
	{
		public override byte getTypeId()
		{
			return 6;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			base.writeData(writer);
		}

		public override void readData(FastBinaryReader reader)
		{
			base.readData(reader);
		}
	}

	public sealed class HashSliderDoneTransition : Transition
	{
		public int index;

		public override byte getTypeId()
		{
			return 7;
		}

		public HashSliderDoneTransition()
		{
		}

		public HashSliderDoneTransition(int index)
		{
			this.index = index;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			base.writeData(writer);
			writer.Write(in index, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			base.readData(reader);
			index = reader.ReadInt32();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("index: " + $"{index}");
			return stringBuilder.ToString();
		}
	}

	public sealed class FishingRodFlyBackTimer : Timer
	{
		public Vector3 rodPos;

		public Quaternion rodRot;

		public Vector3 rodScale;

		public Item fCatch;

		public Vector3 fStartScale;

		public override byte getTypeId()
		{
			return 8;
		}

		public FishingRodFlyBackTimer()
		{
		}

		public FishingRodFlyBackTimer(Vector3 rodPos, Quaternion rodRot, Vector3 rodScale, Item fCatch, Vector3 fStartScale)
		{
			this.rodPos = rodPos;
			this.rodRot = rodRot;
			this.rodScale = rodScale;
			this.fCatch = fCatch;
			this.fStartScale = fStartScale;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.WriteVector3(in rodPos);
			writer.WriteQuaternion(in rodRot);
			writer.WriteVector3(in rodScale);
			writer.WriteComponent(fCatch);
			writer.WriteVector3(in fStartScale);
		}

		public override void readData(FastBinaryReader reader)
		{
			rodPos = reader.ReadVector3();
			rodRot = reader.ReadQuaternion();
			rodScale = reader.ReadVector3();
			fCatch = reader.ReadComponent<Item>();
			fStartScale = reader.ReadVector3();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("rodPos: " + $"{rodPos}");
			stringBuilder.AppendLine("rodRot: " + $"{rodRot}");
			stringBuilder.AppendLine("rodScale: " + $"{rodScale}");
			stringBuilder.AppendLine("fCatch: " + $"{fCatch}");
			stringBuilder.Append("fStartScale: " + $"{fStartScale}");
			return stringBuilder.ToString();
		}
	}

	public sealed class GunpowderFlyInTransition : Transition
	{
		public override byte getTypeId()
		{
			return 9;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			base.writeData(writer);
		}

		public override void readData(FastBinaryReader reader)
		{
			base.readData(reader);
		}
	}

	public sealed class GunpowderFlyBackTimer : Timer
	{
		public Vector3 pos;

		public Quaternion rot;

		public Vector3 scale;

		public override byte getTypeId()
		{
			return 10;
		}

		public GunpowderFlyBackTimer()
		{
		}

		public GunpowderFlyBackTimer(Vector3 pos, Quaternion rot, Vector3 scale)
		{
			this.pos = pos;
			this.rot = rot;
			this.scale = scale;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.WriteVector3(in pos);
			writer.WriteQuaternion(in rot);
			writer.WriteVector3(in scale);
		}

		public override void readData(FastBinaryReader reader)
		{
			pos = reader.ReadVector3();
			rot = reader.ReadQuaternion();
			scale = reader.ReadVector3();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("pos: " + $"{pos}");
			stringBuilder.AppendLine("rot: " + $"{rot}");
			stringBuilder.Append("scale: " + $"{scale}");
			return stringBuilder.ToString();
		}
	}

	public sealed class MagnetsDoneTransition : Transition
	{
		public override byte getTypeId()
		{
			return 11;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			base.writeData(writer);
		}

		public override void readData(FastBinaryReader reader)
		{
			base.readData(reader);
		}
	}

	public class TrailGunpowder
	{
		[DontSave]
		public Trackable trackable;

		[DontSave]
		public Renderer renderer;

		[DontSave]
		public MaterialState dissolve;

		[DontSave]
		public GameObject trail;

		[DontSave]
		public GameObject start;

		[DontSave]
		public Transform startTransform;

		[DontSave]
		public GameObject end;

		[DontSave]
		public Transform endTransform;

		[DontSave]
		public GameObject particles;

		[DontSave]
		public Transform particlesTransform;

		[DontSave]
		public TweenState tween;

		[DontSave]
		public List<int> startTrails;

		[DontSave]
		public List<int> endTrails;
	}

	public class FishingCatchCollection
	{
		public FishingWaterCatchType waterCatchType;

		public FishingBaitType baitType;

		public List<Item> catches;
	}

	public enum FishingWaterCatchType
	{
		None = 0,
		ShallowSnail = 1,
		MediumCrab = 2,
		DeepFish = 3
	}

	public enum FishingBaitType
	{
		None = 0,
		Special = 1,
		Truba = 2,
		Star = 3,
		Rose = 4,
		Long = 5,
		FivePetal = 6
	}

	private enum LevelPredicate
	{
		Magnets = 0,
		Snakes = 1,
		SliderColumn = 2,
		Hash = 3,
		SymbolsChest = 4,
		Parrot = 5,
		Safe = 6,
		DartColumnKeys = 7,
		Darts = 8,
		Gunpowder = 9
	}

	public sealed class OnStartGunpowderPacket : Packet
	{
		public List<bool> gunpowderStates;

		public override byte getTypeId()
		{
			return 0;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.WriteList(gunpowderStates, delegate(FastBinaryWriter w, bool e)
			{
				w.Write(in e, default(FastBinaryWriter.ForPrimitives));
			});
		}

		public override void readData(FastBinaryReader reader)
		{
			gunpowderStates = reader.ReadList((FastBinaryReader r) => r.ReadBoolean());
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("gunpowderStates: " + ToStringHelper.Stringify(gunpowderStates, (bool e) => $"{e}"));
			return stringBuilder.ToString();
		}
	}

	private enum Puzzle
	{
		[PuzzleInfo("%PuzzleP3_1%", false)]
		WallSlider = 0,
		[PuzzleInfo("%PuzzleP3_2%", false)]
		Snakes = 1,
		[PuzzleInfo("%PuzzleP3_3%", false)]
		SunMaze = 2,
		[PuzzleInfo("%PuzzleP3_4%", false)]
		Sextant = 3,
		[PuzzleInfo("%PuzzleP3_5%", false)]
		Fishing = 4,
		[PuzzleInfo("%PuzzleP3_6%", false)]
		CrabKey = 5,
		[PuzzleInfo("%PuzzleP3_7%", false)]
		Parrot = 6,
		[PuzzleInfo("%PuzzleP3_8%", false)]
		SkullLock = 7,
		[PuzzleInfo("%PuzzleP3_9%", false)]
		Bridge = 8,
		[PuzzleInfo("%PuzzleP3_10%", false)]
		SymbolsChest = 9,
		[PuzzleInfo("%PuzzleP3_11%", false)]
		Medallions = 10,
		[PuzzleInfo("%PuzzleP3_12%", false)]
		Darts = 11,
		[PuzzleInfo("%PuzzleP3_13%", false)]
		Magnets = 12,
		[PuzzleInfo("%PuzzleP3_14%", false)]
		Barrels = 13
	}

	private enum WallSliderHint
	{
		LookAtPuzzle = 0,
		Slide1 = 1,
		Slide2 = 2,
		Slide3 = 3,
		Slide4 = 4,
		Slide5 = 5,
		Slide6 = 6
	}

	private enum SnakesHint
	{
		LookAtSnakes = 0,
		NoticePattern = 1,
		PlaceSnake = 2,
		Others = 3
	}

	private enum SunMazeHint
	{
		LookAtPuzzle = 0,
		Move1 = 1,
		Move2 = 2,
		Move3 = 3,
		Move4 = 4,
		Move5 = 5,
		Move6 = 6,
		Move7 = 7
	}

	private enum SextantHint
	{
		PickUpMazeGem = 0,
		PlaceGem = 1,
		LookAtSextant = 2,
		MoveSextant = 3,
		LineUpSextant = 4,
		FindSymbols = 5,
		OneSolution = 6
	}

	private enum FishingHint
	{
		PickUpSextantGem = 0,
		PlaceGem = 1,
		PickUpFishingRod = 2,
		FlowerForSmall = 3,
		PickUpFlower = 4,
		FlowerIsBait = 5,
		Fish = 6,
		GetCatch = 7,
		CrabIsMiddle = 8,
		FishForCrab = 9
	}

	private enum CrabKeyHint
	{
		GetCrabKey = 0,
		PlaceCrabKey = 1
	}

	private enum ParrotHint
	{
		ParrotTalks = 0,
		Move1 = 1,
		Move2 = 2,
		Move3 = 3
	}

	private enum SkullLockHint
	{
		LookAtPuzzle = 0,
		Rotate = 1
	}

	private enum BridgeHint
	{
		PickUpGems = 0,
		MakeRedBridge = 1,
		PickUpGem = 2,
		PlaceBlueGems = 3,
		PickUpRedGem = 4,
		PlaceRedGemCenter = 5
	}

	private enum SymbolsChestHint
	{
		FindHint = 0,
		GetSextantSymbol = 1,
		GetChestSymbols = 2,
		GetBridgeSymbol = 3,
		PlaceAllSymbols = 4,
		SolutionPart = 5,
		MoveSlidable = 6
	}

	private enum MedallionsHint
	{
		PickUpJournal = 0,
		GetCoin1 = 1,
		GetCoin2 = 2,
		GetCoin3 = 3,
		GetCoin4 = 4,
		PlaceCoins = 5
	}

	private enum DartsHint
	{
		MovePillars = 0,
		ShootDarts = 1
	}

	private enum MagnetsHint
	{
		PickUpWallSliderMagnet = 0,
		CrowbarOpen = 1,
		LookAtPuzzle = 2,
		XMagnetRotatesInner = 3,
		PickUpKey = 4,
		PlaceKey = 5
	}

	private enum BarrelsHint
	{
		PullLever = 0,
		PlacePowder1 = 1,
		PlacePowder2 = 2,
		PlacePowder3 = 3,
		ExplodeBarrels = 4
	}

	private int hashSolvedCount;

	private bool hash3Done;

	private float slidableColumnAcceptDistance = 0.1f;

	private bool slider1OnTarget;

	private bool slider2OnTarget;

	private float sextantCurrentZAngle;

	private float sextantCurrentXAngle = -180f;

	private bool isFirstBridgeGemPlaced;

	private bool isSecondBridgeGemPlaced;

	public int symbolsCurrentIndex;

	public int[] symbolsCurrentCode = new int[7];

	private float[] symbolsCodeAngles = new float[5] { 0f, -72f, -288f, -144f, -215f };

	private List<int> safePoints = new List<int> { 18, 6, 6, 18, 18, 6, 18, 6 };

	private bool dartColumnsRaised;

	private int dartsArrived;

	private List<Collider> dartTargets = new List<Collider>();

	private bool areDartsReset;

	public List<bool> trailGunpowderStates = new List<bool>();

	public List<bool> trailGunpowderFireFromStarts = new List<bool>();

	[DontSave]
	private List<TrailGunpowder> trailGunpowders;

	private float gunpowderDropTimer;

	private int currentGunpowderDroplet;

	private int nextGunpowderFloorPowder;

	private Vector3 gunpowderStartingLocalPos;

	private List<TrailGunpowder> trailGunpowderOnFire = new List<TrailGunpowder>();

	private bool gunpowderAnimActive;

	private int currentGunpowderIndex;

	private Vector2 sextantTargetRot;

	public float fishingHookThrowMultiplier;

	private List<FishingCatchCollection> fishingCatchesLeft;

	private Item fishingCatchOnHook;

	private Vector3 throwingHookScale;

	private FishingWaterCatchType currentWaterType;

	private bool fishingAnimActive;

	private bool collectedFishingKey;

	[DontSave]
	public ParticleSystem fishingVFX;

	[DontSave]
	public Transform fishingVFXTransform;

	[DontSave]
	private EventInstance[] sliderChestTurnableSound;

	[DontSave]
	private EventInstance[] magnetSound;

	private bool firstFrame = true;

	private bool magnetOpen;

	[Header("Generated Variables")]
	[DontSave]
	public GameObject plankHint;

	[DontSave]
	public GameObject fishingJournal;

	[DontSave]
	public Item crabKeyItem;

	[DontSave]
	public Zoomable parrotBoardZoom;

	[DontSave]
	public Ref<Transform, GameObject> fishingRodImposter;

	[DontSave]
	public TweenState doorTween;

	[DontSave]
	public Slot doorSlot;

	[DontSave]
	public GameObject[] hookBaits;

	[DontSave]
	public GameObject[] imposterHookBaits;

	[DontSave]
	public Slot baitSlot;

	[DontSave]
	public Item crabKey;

	[DontSave]
	public Item[] largeSnails;

	[DontSave]
	public Item[] mediumSnails;

	[DontSave]
	public Item[] smallSnails;

	[DontSave]
	public Item[] largeCrabs;

	[DontSave]
	public Item[] mediumCrabs;

	[DontSave]
	public Item[] smallCrabs;

	[DontSave]
	public Item[] largeFish;

	[DontSave]
	public Item[] mediumFish;

	[DontSave]
	public Item[] smallFish;

	[DontSave]
	public Interactive[] fishingDeepTriggers;

	[DontSave]
	public Interactive[] fishingMediumTriggers;

	[DontSave]
	public Interactive[] fishingShallowTriggers;

	[DontSave]
	public Transform fishingHookPosition;

	[DontSave]
	public Item fishingRod;

	[DontSave]
	public Ref<Transform, GameObject> fishingHook;

	[DontSave]
	public Slidable sextantSlidable;

	[DontSave]
	public Item magnetSlotKey;

	[DontSave]
	public TweenState magnetDoor;

	[DontSave]
	public Slot magnetSlot;

	[DontSave]
	public Switch3D dartLever;

	[DontSave]
	public Sequence dartLeverSequence;

	[DontSave]
	public Zoomable sextantZoom;

	[DontSave]
	public Transform[] sextantRotators;

	[DontSave]
	public Trackable sextantTrackable;

	[DontSave]
	public TweenState hashDoor;

	[DontSave]
	public Zoomable hashZoomable;

	[DontSave]
	public Transform spawnPoint;

	[DontSave]
	public SlidableGraph[] hashSlidables;

	[DontSave]
	public Transform[] magnetCylinderPointers;

	[DontSave]
	public Transform[] magnetOpenPointers;

	[DontSave]
	public Transform[] magnetCylinders;

	[DontSave]
	public Item[] magnets;

	[DontSave]
	public ParticleSystem bigMagnetEffect;

	[DontSave]
	public TweenState sextantCamTween;

	[DontSave]
	public Transform sextantCamera;

	[DontSave]
	public Camera sextantCam1;

	[DontSave]
	public Camera sextantCam2;

	[DontSave]
	public int[] parrotSolution;

	[DontSave]
	public TweenState redGemDoors;

	[DontSave]
	public TweenState columnPuzzleSolvedTween;

	[DontSave]
	public Slot[] snakeSlots;

	[DontSave]
	public RefArray<Item, Transform> snakes;

	[DontSave]
	public RefArray<Trackable, Renderer, TweenState, GameObject> gunpowderTrailPowders;

	[DontSave]
	public MaterialState[] gunpowderDissolve;

	[DontSave]
	public MaterialState[] gunpowderTrailHover;

	[DontSave]
	public RefArray<ParticleSystem, GameObject> gunpowderBarrelExplosions;

	[DontSave]
	public TweenState[] gunPowderBarrelMoveAnim;

	[DontSave]
	public GameObject gunpowderSpark;

	[DontSave]
	public Item gunpowderPouch;

	[DontSave]
	public Ref<TweenState, Transform, GameObject> gunpowderPouchImpostor;

	[DontSave]
	public Ref<Transform> gunpowderPouchImpostorRendTarget;

	[DontSave]
	public ParticleSystem gunpowderPouchParticle;

	[DontSave]
	public Switch3D gunpowderStartFlameSwitch;

	[DontSave]
	public TweenState endDoor;

	[DontSave]
	public BoxCollider[] dartTargetColliders;

	[DontSave]
	public RefArray<GameObject, Transform, Collider> darts;

	[DontSave]
	public ParticleSystem[] dartEffect;

	[DontSave]
	public GameObject[] dartOrigins;

	[DontSave]
	public TweenState dartColumnBotRiser;

	[DontSave]
	public SlidableGraph dartColumnSlidableGraph;

	[DontSave]
	public Turnable[] dartColumnTurnables;

	[DontSave]
	public Transform[] dartOriginalParents;

	[DontSave]
	public Slot[] dartColumnCoinSlots;

	[DontSave]
	public TweenState[] dartColumnRisers;

	[DontSave]
	public ParticleSystem[] dartColumnParticles;

	[DontSave]
	public Item[] dartColumnTokens;

	[DontSave]
	public GameObject[] dartColumnTokensColliders;

	[DontSave]
	public TweenState safeDoor;

	[DontSave]
	public TweenState[] safeBoltTweens;

	[DontSave]
	public Dial[] safeDials;

	[DontSave]
	public TweenState[] safePinsSkulls;

	[DontSave]
	public TweenState[] safePins;

	[DontSave]
	public TweenState parrotDoor;

	[DontSave]
	public SlidableGraph parrotSlidableGraph;

	[DontSave]
	public Npc parrotNpc;

	[DontSave]
	public GameObject[] sliderColumnSolution;

	[DontSave]
	public GameObject[] sliderColumnEdgeNodes;

	[DontSave]
	public Turnable[] slidableColumnTurnables;

	[DontSave]
	public SlidableGraph sliderColumnSlidableGraph;

	[DontSave]
	public Transform sliderColumnRotator1;

	[DontSave]
	public Transform sliderColumnRotator2;

	[DontSave]
	public Item treasureHint;

	[DontSave]
	public Switch3D treasureHintSwitch;

	[DontSave]
	public Turnable[] sextantTurnables;

	[DontSave]
	public Item[] symbolKeys;

	[DontSave]
	public Ref<Zoomable, Lock> sextantChestZoom;

	[DontSave]
	public Switch3D sextantChestLid;

	[DontSave]
	public Item blueGem;

	[DontSave]
	public Item redGem;

	[DontSave]
	public Slot redGemSlot;

	[DontSave]
	public Slot blueGemSlot;

	public TweenState redGemOpen;

	public TweenState blueGemOpen;

	[DontSave]
	public Item StoneKey;

	[DontSave]
	public Slot StoneKeySlot;

	[DontSave]
	public RefArray<Slot, TweenState> crowbarSlots;

	[DontSave]
	public ParticleSystem[] crowbarSlotEffects;

	[DontSave]
	public Item[] crateLids;

	[DontSave]
	public Ref<GameObject, BoxCollider> crowbar;

	[DontSave]
	public SlidableGraph symbolsSlidableGraph;

	[DontSave]
	public TweenState symbolsHandleTs;

	[DontSave]
	public Slot[] symbolSlots;

	[DontSave]
	public TweenState symbolsChestPlatformTweenState;

	[DontSave]
	public Zoomable symbolsChestZoomable;

	[DontSave]
	public TweenState openCircleDoor;

	[DontSave]
	public Transform[] symbolsCode;

	[DontSave]
	public int[] symbolsChestSolution;

	[DontSave]
	public TweenState symbolsChestLid;

	[DontSave]
	public Transform symbolsChestSliderStartPos;

	[DontSave]
	public Transform symbolsGrafPiece;

	[DontSave]
	public Item[] fivePetalFlowers;

	[DontSave]
	public RefArray<GameObject, Switch3D> fivePetalFlowersHide;

	[DontSave]
	public Item[] longFlowers;

	[DontSave]
	public RefArray<GameObject, Switch3D> longFlowersHide;

	[DontSave]
	public Item[] roseLikeFlowers;

	[DontSave]
	public RefArray<GameObject, Switch3D> roseLikeFlowersHide;

	[DontSave]
	public Item[] starFlowers;

	[DontSave]
	public RefArray<GameObject, Switch3D> starFlowerHide;

	[DontSave]
	public Item[] trubaFlowers;

	[DontSave]
	public RefArray<GameObject, Switch3D> trubaFlowerHide;

	[DontSave]
	public Item[] specialFlowers;

	public GameObject centerPathBlocker1;

	public GameObject centerPathBlocker2;

	[DontSave]
	public TweenState fishingRodAnim;

	[DontSave]
	public ParticleSystem openCircleDoorEffect;

	[DontSave]
	public ParticleSystem fishingDoorEffect;

	[DontSave]
	public ParticleSystem safeDoorEffect;

	[DontSave]
	public ParticleSystem snakesOpenEffect;

	[DontSave]
	public Switch3D levelExit;

	[DontSave]
	public GameObject cutsceneMiddle;

	[DontSave]
	public AnimationSampler cutsceneMiddleAnimation;

	[DontSave]
	public Sequence magnetsDoorSequence;

	private bool magnetsSolved;

	[DontSave]
	public Zoomable dartLeverBaseZoomable;

	[DontSave]
	public VisualEffect[] magnetsVisualEffect;

	[DontSave]
	public Transform fishingSpawnPoint;

	[DontSave]
	public GameObject bridgeRespawnTrigger;

	public bool gunpowderFire(int trailIndex, Vector3 startingPosition)
	{
		TrailGunpowder trailGunpowder = trailGunpowders[trailIndex];
		if (trailGunpowder.dissolve.getTargetWeight("NewState") == 1f)
		{
			return false;
		}
		float num = Vector3.Distance(startingPosition, trailGunpowder.startTransform.position);
		float num2 = Vector3.Distance(startingPosition, trailGunpowder.endTransform.position);
		trailGunpowderFireFromStarts[trailIndex] = num < num2;
		game.setParent(trailGunpowder.particlesTransform, trailGunpowderFireFromStarts[trailIndex] ? trailGunpowder.startTransform : trailGunpowder.endTransform);
		trailGunpowder.particlesTransform.localPosition = Vector3.zero;
		trailGunpowder.tween.transitionTo(trailGunpowderFireFromStarts[trailIndex] ? "ToEnd" : "ToStart", 2f);
		trailGunpowder.particles.SetActive(value: true);
		trailGunpowderStates[trailIndex] = true;
		return true;
	}

	public void gunpowderOnTweenDone(int trailIndex, List<TrailGunpowder> trailGunpowderOnFire, List<TrailGunpowder> trailGunpowders)
	{
		TrailGunpowder trailGunpowder = trailGunpowders[trailIndex];
		trailGunpowder.particles.SetActive(value: false);
		trailGunpowder.dissolve.setWeight("NewState");
		trailGunpowder.trackable.targetable = true;
		List<int> list = (trailGunpowderFireFromStarts[trailIndex] ? trailGunpowder.endTrails : trailGunpowder.startTrails);
		for (int i = 0; i < list.Count; i++)
		{
			int num = list[i];
			if (!trailGunpowderOnFire.Contains(trailGunpowders[num]) && gunpowderFire(num, (trailGunpowderFireFromStarts[trailIndex] ? trailGunpowder.endTransform : trailGunpowder.startTransform).position))
			{
				trailGunpowderOnFire.Add(trailGunpowders[num]);
			}
		}
	}

	public override void onInitPredicates()
	{
		game.registerPredicate(LevelPredicate.Magnets, () => checkMagnets());
		game.registerPredicate(LevelPredicate.Snakes, () => checkSnakes());
		game.registerPredicate(LevelPredicate.SliderColumn, () => checkSliderColumn());
		game.registerPredicate(LevelPredicate.Hash, () => checkHash());
		game.registerPredicate(LevelPredicate.SymbolsChest, () => checkSymbolsChest());
		game.registerPredicate(LevelPredicate.Parrot, () => checkParrot());
		game.registerPredicate(LevelPredicate.Safe, () => checkSafe());
		game.registerPredicate(LevelPredicate.DartColumnKeys, () => checkDartColumnKeys());
		game.registerPredicate(LevelPredicate.Darts, () => checkDarts());
		game.registerPredicate(LevelPredicate.Gunpowder, () => checkGunpowder());
	}

	public override void onLevelPredicateDone(int type, int doneCounter)
	{
		switch (type)
		{
		case 0:
			solveMagnets();
			break;
		case 1:
			solveSnakes();
			break;
		case 2:
			solveSliderColumn();
			break;
		case 3:
			solveHash();
			break;
		case 4:
			solveSymbolsChest();
			break;
		case 5:
			solveParrot();
			break;
		case 6:
			solveSafe();
			break;
		case 7:
			solveDartColumnKeys();
			break;
		case 8:
			solveDarts();
			break;
		case 9:
			solveGunpowder();
			break;
		}
	}

	public override void onInit()
	{
		game.syncPlayerRays = true;
		List<Interactive> list = new List<Interactive>();
		SlidableGraph[] array = hashSlidables;
		foreach (SlidableGraph slidableGraph in array)
		{
			list.Add(slidableGraph.piecesList[0]);
		}
		Interactive.linkInteractives(list.ToArray());
		List<Interactive> list2 = new List<Interactive>();
		Turnable[] array2 = slidableColumnTurnables;
		foreach (Turnable item in array2)
		{
			list2.Add(item);
		}
		foreach (SlidableGraphPiece pieces in sliderColumnSlidableGraph.piecesList)
		{
			list2.Add(pieces);
		}
		Interactive.linkInteractives(list2.ToArray());
		initSliderColumn();
		dartColumnsActivate(activate: false);
		initGunpowder();
		initHash();
		initSextant();
		initFishingRod();
		safeDials[0].setValue(5);
		safeDials[1].setValue(1);
		safeDials[2].setValue(6);
		safeDials[3].setValue(9);
		sliderChestTurnableSound = new EventInstance[7];
		for (int j = 0; j < sliderChestTurnableSound.Length; j++)
		{
			sliderChestTurnableSound[j] = PineFmod.createInstance("event:/Sound Effects/03 Interactable/Drawers/Wood_Old_Drawer_Start");
			PineFmod.set3DAttributes(sliderChestTurnableSound[j], PineFmod.to3DAttributes(symbolsCode[j]));
		}
		magnetSound = new EventInstance[2];
		for (int k = 0; k < magnetSound.Length; k++)
		{
			magnetSound[k] = PineFmod.createInstance("event:/Sound Effects/04 Items/Metal/Metal Concrete/Metal_Drag_On_Concrete");
			PineFmod.set3DAttributes(magnetSound[k], PineFmod.to3DAttributes(magnetCylinders[k]));
		}
	}

	public override void onUpdate()
	{
		bool flag = game.isInAnyPlayerHand(gunpowderPouch);
		for (int i = 0; i < gunpowderTrailPowders.Length; i++)
		{
			gunpowderTrailPowders[i].Get<Trackable>(0).targetable = flag || !trailGunpowderStates[i];
		}
		bool flag2 = game.isSelectedInPCMode(gunpowderPouch.gameObject) && !game.isInTopZoom(gunpowderPouch.gameObject);
		MaterialState[] array = gunpowderTrailHover;
		for (int j = 0; j < array.Length; j++)
		{
			array[j].transitionTo("Default", 3f);
		}
		int num = -1;
		float num2 = 99f;
		for (int k = 0; k < trailGunpowders.Count; k++)
		{
			if (trailGunpowders[k].trackable.GetComponentInChildren<Collider>().Raycast(game.playerViewRay, out var hitInfo, 10f) && Game.inReach(game.playerViewRay.origin, hitInfo.point, 2.5f) && hitInfo.distance < num2 && (flag2 || !trailGunpowderStates[k]))
			{
				num = k;
				num2 = hitInfo.distance;
			}
		}
		if (num != -1)
		{
			gunpowderTrailHover[num].transitionTo("Hover", 3f);
		}
		Interactive[] array2 = fishingDeepTriggers;
		for (int j = 0; j < array2.Length; j++)
		{
			array2[j].targetable = game.isSelectedInPCMode(fishingRod.gameObject);
		}
		array2 = fishingMediumTriggers;
		for (int j = 0; j < array2.Length; j++)
		{
			array2[j].targetable = game.isSelectedInPCMode(fishingRod.gameObject);
		}
		array2 = fishingShallowTriggers;
		for (int j = 0; j < array2.Length; j++)
		{
			array2[j].targetable = game.isSelectedInPCMode(fishingRod.gameObject);
		}
		updateSliderColumn();
		updateSafe();
		onMagnetUpdate();
		sextantRotators[0].localRotation = Quaternion.Lerp(sextantRotators[0].localRotation, Quaternion.Euler(0f, 0f, sextantTargetRot.x), Time.deltaTime * 10f);
		sextantRotators[1].localRotation = Quaternion.Lerp(sextantRotators[1].localRotation, Quaternion.Euler(sextantTargetRot.y, 0f, 0f), Time.deltaTime * 10f);
		if (slider1OnTarget)
		{
			sliderColumnRotator1.localEulerAngles += new Vector3(0f, 0f, 30f) * Time.deltaTime;
		}
		if (slider2OnTarget)
		{
			sliderColumnRotator2.localEulerAngles += new Vector3(0f, 0f, 30f) * Time.deltaTime;
		}
		bool flag3 = false;
		if (game.topZoomContextSafe(out var context) && context.selected == sextantZoom.gameObject)
		{
			flag3 = true;
		}
		sextantCam1.enabled = flag3 || firstFrame;
		sextantCam2.enabled = flag3 || firstFrame;
		if (flag3 || firstFrame)
		{
			updateSextantCameraCut(sextantCam1, new Rect(0.5f, 0f, 1f, 1f));
			updateSextantCameraCut(sextantCam2, new Rect(0f, 0f, 0.5f, 1f));
			firstFrame = false;
		}
	}

	private void updateSextantCameraCut(Camera camera, Rect rect)
	{
		camera.rect = new Rect(0f, 0f, 1f, 1f);
		camera.ResetProjectionMatrix();
		Rect rect2 = rect;
		if (rect2.x < 0f)
		{
			rect2.width += rect.x;
			rect2.x = 0f;
		}
		if (rect2.y < 0f)
		{
			rect2.height += rect.y;
			rect2.y = 0f;
		}
		rect2.width = Mathf.Clamp(Mathf.Min(1f - rect2.x, rect2.width), 1E-05f, 1f);
		rect2.height = Mathf.Clamp(Mathf.Min(1f - rect2.y, rect2.height), 1E-05f, 1f);
		Matrix4x4 projectionMatrix = camera.projectionMatrix;
		camera.rect = rect2;
		Matrix4x4 matrix4x = Matrix4x4.TRS(new Vector3(1f / rect2.width - 1f, 1f / rect2.height - 1f, 0f), Quaternion.identity, new Vector3(1f / rect2.width, 1f / rect2.height, 1f));
		Matrix4x4 matrix4x2 = Matrix4x4.TRS(new Vector3((0f - rect2.x) * 2f / rect2.width, (0f - rect2.y) * 2f / rect2.height, 0f), Quaternion.identity, Vector3.one);
		camera.projectionMatrix = matrix4x2 * matrix4x * projectionMatrix;
	}

	public override void onSwitch3D(Switch3D targetSwitch, Switch3DEvent switchEvent)
	{
		if (switchEvent == Switch3DEvent.Start)
		{
			checkFlowersSwitch(targetSwitch);
			if (targetSwitch == dartLever)
			{
				onDartLeverTweenDone();
			}
		}
		if (switchEvent == Switch3DEvent.On && targetSwitch == gunpowderStartFlameSwitch)
		{
			startGunpowderFireSynced();
		}
		if (switchEvent == Switch3DEvent.Release && targetSwitch == dartLever)
		{
			dartColumnsActivate(activate: false);
		}
		if (switchEvent == Switch3DEvent.Start && targetSwitch == levelExit)
		{
			game.levelCompleted();
		}
	}

	public override void onTweenTransitionDone(TweenState tweenState, string state)
	{
		if (tweenState == gunpowderPouchImpostor.Get<TweenState>(0))
		{
			trailGunpowderStates[currentGunpowderIndex] = false;
			gunpowderDissolve[currentGunpowderIndex].setWeight("NewState", 0f);
			game.startTimer(new GunpowderFlyBackTimer(gunpowderPouchImpostorRendTarget.Get<Transform>().position, gunpowderPouchImpostorRendTarget.Get<Transform>().rotation, gunpowderPouchImpostorRendTarget.Get<Transform>().localScale), 0.5f);
		}
		if (tweenState == fishingRodAnim)
		{
			onHookFlyOut();
		}
		if (tweenState == symbolsHandleTs)
		{
			activateSymbolsSlider();
		}
		if (tweenState == dartColumnRisers[0])
		{
			game.startTimer(new GraphInitTimer(), 0.1f);
		}
		if (tweenState == endDoor)
		{
			onGunpowderEndDoorTween();
		}
		for (int i = 0; i < trailGunpowders.Count; i++)
		{
			TrailGunpowder trailGunpowder = trailGunpowders[i];
			if (trailGunpowder.tween == tweenState)
			{
				gunpowderOnTweenDone(i, trailGunpowderOnFire, trailGunpowders);
				trailGunpowderOnFire.Remove(trailGunpowder);
				if (trailGunpowderOnFire.Count == 0)
				{
					completeGunpowderFire();
				}
				if (i == 9)
				{
					explodeBarrel(0);
				}
				if (i == 41)
				{
					explodeBarrel(1);
				}
				if (i == 43)
				{
					explodeBarrel(2);
				}
				if (i == 42)
				{
					explodeBarrel(3);
				}
			}
		}
	}

	public override void onSequenceDone(Sequence sequence)
	{
		if (sequence == dartLeverSequence)
		{
			if (sequence.targetSequenceTime == 4f)
			{
				dartLeverSequence.play(-1f, 3f);
				fireDarts();
			}
			else if (sequence.targetSequenceTime == 3f)
			{
				dartLever.targetable = dartColumnTurnables[0].targetable;
			}
		}
	}

	public override void onTimerUpdate(Timer timer)
	{
		if (timer is GunpowderFlyBackTimer gunpowderFlyBackTimer)
		{
			Transform transform = null;
			if (game.hasAuthority(gunpowderPouch))
			{
				GameObject impostorInHand = game.getImpostorInHand();
				if (impostorInHand != null)
				{
					transform = impostorInHand.transform;
				}
			}
			else
			{
				Game.GamePlayerData playerWithItemInInventory = game.getPlayerWithItemInInventory(gunpowderPouch.gameObject);
				if (playerWithItemInInventory != null)
				{
					GameObject inHandItemBubble = playerWithItemInInventory.inHandItemBubble;
					if (inHandItemBubble != null)
					{
						transform = inHandItemBubble.transform;
					}
				}
			}
			if (transform != null)
			{
				float t = gunpowderFlyBackTimer.time / gunpowderFlyBackTimer.duration;
				Vector3 b = new Vector3(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z);
				gunpowderPouchImpostorRendTarget.Get<Transform>().position = Vector3.Lerp(gunpowderFlyBackTimer.pos, transform.position, t);
				gunpowderPouchImpostorRendTarget.Get<Transform>().rotation = Quaternion.Lerp(gunpowderFlyBackTimer.rot, transform.rotation, t);
				gunpowderPouchImpostorRendTarget.Get<Transform>().localScale = Vector3.Lerp(gunpowderFlyBackTimer.scale, b, t);
			}
		}
		if (!(timer is FishingRodFlyBackTimer fishingRodFlyBackTimer))
		{
			return;
		}
		Transform transform2 = null;
		if (game.hasAuthority(fishingRod))
		{
			GameObject impostorInHand2 = game.getImpostorInHand();
			if (impostorInHand2 != null)
			{
				transform2 = impostorInHand2.transform;
			}
		}
		else
		{
			Game.GamePlayerData playerWithItemInInventory2 = game.getPlayerWithItemInInventory(fishingRod.gameObject);
			if (playerWithItemInInventory2 != null)
			{
				GameObject inHandItemBubble2 = playerWithItemInInventory2.inHandItemBubble;
				if (inHandItemBubble2 != null)
				{
					transform2 = inHandItemBubble2.transform;
				}
			}
		}
		if (transform2 != null)
		{
			float t2 = fishingRodFlyBackTimer.time / fishingRodFlyBackTimer.duration;
			Vector3 b2 = new Vector3(transform2.lossyScale.x, transform2.lossyScale.y, transform2.lossyScale.z);
			fishingRodImposter.Get<Transform>(0).position = Vector3.Lerp(fishingRodFlyBackTimer.rodPos, transform2.position, t2);
			fishingRodImposter.Get<Transform>(0).rotation = Quaternion.Lerp(fishingRodFlyBackTimer.rodRot, transform2.rotation, t2);
			fishingRodImposter.Get<Transform>(0).localScale = Vector3.Lerp(fishingRodFlyBackTimer.rodScale, b2, t2);
		}
		if (fishingRodFlyBackTimer.fCatch != null)
		{
			fishingRodFlyBackTimer.fCatch.transform.localScale = Vector3.Lerp(Vector3.zero, fishingRodFlyBackTimer.fStartScale, fishingRodFlyBackTimer.unitTime);
		}
	}

	public override void onTimerDone(Timer timer)
	{
		if (timer is HashSliderDoneTransition hashSliderDoneTransition)
		{
			hashSlidables[hashSliderDoneTransition.index].initPieces();
		}
		if (timer is GunpowderFlyBackTimer)
		{
			gunpowderPouchImpostor.Get<GameObject>(0u).SetActive(value: false);
			gunpowderPouch.unlockItemInteractions = game.hasAuthority(gunpowderPouch.gameObject);
			gunpowderPouch.hideItemInHand = false;
			gunpowderPouchImpostor.Get<TweenState>(0).setState("NewState", 0f);
			gunpowderAnimActive = false;
		}
		if (timer is GunpowderFlyInTransition)
		{
			gunpowderPouchImpostor.Get<TweenState>(0).transitionTo("NewState", 1.24f);
			gunpowderPouchParticle.Play();
		}
		if (timer is FishingRodFlyBackTimer fishingRodFlyBackTimer)
		{
			fishingRodImposter.Get<GameObject>(0f).SetActive(value: false);
			fishingRod.unlockItemInteractions = game.hasAuthority(fishingRod.gameObject);
			fishingRod.hideItemInHand = false;
			fishingRodAnim.setState("NewState", 0f);
			fishingAnimActive = false;
			if (fishingRodFlyBackTimer.fCatch != null)
			{
				fishingRodFlyBackTimer.fCatch.transform.localScale = fishingRodFlyBackTimer.fStartScale;
				fishingRodFlyBackTimer.fCatch.targetable = true;
				if (game.hasAuthority(fishingRod))
				{
					game.transitionItemToInventory(fishingRodFlyBackTimer.fCatch.gameObject);
					if (game.pcSelectedItem != -1)
					{
						game.removeSelectedItem(game.inventory[game.pcSelectedItem].topGameObject());
					}
				}
			}
		}
		if (timer is FishingRodFlyInTransition)
		{
			fishingRodAnim.transitionTo("NewState", 0.178f);
		}
		if (timer is ResetSymbolsSliderTransition)
		{
			onSymbolsChestSlot();
		}
		if (timer is SetSymbolsCodeTransition setSymbolsCodeTransition)
		{
			EventInstance[] array = sliderChestTurnableSound;
			for (int i = 0; i < array.Length; i++)
			{
				EventInstance instance = array[i];
				if (instance.getPlaybackState(out var state) == RESULT.OK && state == PLAYBACK_STATE.PLAYING)
				{
					PineFmod.stop(instance, STOP_MODE.ALLOWFADEOUT);
				}
			}
			if (!setSymbolsCodeTransition.reset)
			{
				checkSymbolsChestForReset();
			}
		}
		if (timer is GraphInitTimer)
		{
			dartColumnSlidableGraph.initPieces();
		}
		if (timer is DartArrivedTransition dartArrivedTransition)
		{
			onDartTimer(dartArrivedTransition.index, dartArrivedTransition.newParent);
		}
		if (timer is GetCrowbarBackTimer)
		{
			if (game.hasAuthority(crowbar.Get<GameObject>(0)))
			{
				game.addItemToInventory(crowbar.Get<GameObject>(0));
			}
			crowbar.Get<BoxCollider>(0f).enabled = true;
		}
		if (timer is CutsceneMiddleTimer)
		{
			cutsceneMiddle.SetActive(value: false);
		}
	}

	public override void onAddToInventory(Item item)
	{
		for (int i = 0; i < dartColumnTokens.Length; i++)
		{
			if (item == dartColumnTokens[i] && dartColumnTokensColliders[i] != null)
			{
				dartColumnTokensColliders[i].SetActive(value: false);
			}
		}
		if (item == blueGem)
		{
			columnPuzzleSolvedTween.transitionToStateAdditive("Raise", 0.33f, 0f);
			columnPuzzleSolvedTween.transitionToStateAdditive("NewState", 0.33f, 0f);
		}
		if (item == fishingCatchOnHook)
		{
			onPickedUpCatch();
		}
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void openFishingDoor()
	{
		doorTween.transitionTo("NewState");
		fishingDoorEffect.Play();
	}

	public override void onSlot(Slot targetSlot)
	{
		if (targetSlot == doorSlot)
		{
			doorTween.transitionTo("NewState");
			fishingDoorEffect.Play();
			game.finishPuzzle(Puzzle.CrabKey);
		}
		if (targetSlot == redGemSlot)
		{
			redGemOpen.transitionTo("NewState");
			centerPathBlocker1.SetActive(value: false);
		}
		if (targetSlot == blueGemSlot)
		{
			blueGemOpen.transitionTo("NewState");
			centerPathBlocker2.SetActive(value: false);
		}
		if (targetSlot == StoneKeySlot)
		{
			onBridgeCenterRedGemSlot();
		}
		if (Array.Exists(symbolSlots, (Slot x) => x == targetSlot))
		{
			resetSymbolsChest();
		}
		if (targetSlot == magnetSlot)
		{
			onMagnetSlot();
		}
		for (int num = 0; num < crowbarSlots.Length; num++)
		{
			if (targetSlot == crowbarSlots[num].Get<Slot>(0))
			{
				targetSlot.targetable = false;
				crowbarSlots[num].Get<TweenState>(0f).transitionTo("NewState", 3f);
				game.startTimer(new GetCrowbarBackTimer(), 0.5f);
				crateLids[num].targetable = true;
				crateLids[num].hasRigidbody = true;
				crowbar.Get<BoxCollider>(0f).enabled = false;
				Vector3 addForce = ((num == 0) ? new Vector3(-1.2f, 1.2f, 0f) : new Vector3(1.2f, 1.2f, 0f));
				crateLids[num].rbOverrides = new RigidbodyOverrides
				{
					velocity = Vector3.zero,
					addForce = addForce,
					addForceMode = ForceMode.Impulse
				};
				crowbarSlotEffects[num].Play();
				if (num == 0)
				{
					magnets[1].targetable = true;
					magnetsVisualEffect[1].enabled = true;
				}
			}
		}
	}

	public override void onRemoveFromSlot(Slot targetSlot, Item item)
	{
		if (Array.Exists(symbolSlots, (Slot x) => x == targetSlot))
		{
			resetSymbolsChest();
		}
	}

	public override void onUnlock(Lock targetLock)
	{
		if (targetLock == sextantChestZoom.Get<Lock>(0f))
		{
			solveSextantChest();
		}
	}

	public override void onSlidableMoved(Slidable slidable, MoveEvent moveEvent)
	{
		if (slidable == sextantSlidable)
		{
			onSextantSlidable();
		}
	}

	public override void onSlidableGraphArrivedToNode(SlidableGraph.ToNode context)
	{
		if (context.graph == symbolsSlidableGraph)
		{
			onSymbolsChestSlidableMoved(context.toNode);
		}
		int num = Array.IndexOf(hashSlidables, context.graph);
		if (num >= 0)
		{
			hashSliderOnNode(num, context.toNode, forceMove: false);
		}
	}

	public override void onSlidableGraphReleased(SlidableGraph.OnPieceInteraction context)
	{
		if (context.graph == sliderColumnSlidableGraph)
		{
			onSliderColumnSlidableReleased(context.piece);
		}
	}

	public override void onTrackable(Trackable trackable, TrackableEvent trackableEvent)
	{
		if (trackable == sextantTrackable)
		{
			sextantTargetRot -= new Vector2(trackableEvent.diff.x, trackableEvent.diff.y) * 70f;
			sextantTargetRot.x = Mathf.Clamp(sextantTargetRot.x, -110f, 85f);
			sextantTargetRot.y = Mathf.Clamp(sextantTargetRot.y, -40f, 40f);
		}
		if (trackableEvent.type != TrackableEventType.Start || trailGunpowderOnFire.Count != 0)
		{
			return;
		}
		for (int i = 0; i < trailGunpowders.Count; i++)
		{
			TrailGunpowder trailGunpowder = trailGunpowders[i];
			if (trailGunpowder.trackable == trackable)
			{
				if (!trailGunpowderStates[i])
				{
					trailGunpowderStates[i] = true;
					trailGunpowder.dissolve.transitionTo("NewState");
				}
				break;
			}
		}
	}

	public override void onTurnableMoved(Turnable turnable, MoveEvent moveEvent)
	{
		if (Array.IndexOf(slidableColumnTurnables, turnable) < 0)
		{
			return;
		}
		foreach (SlidableGraphPiece pieces in sliderColumnSlidableGraph.piecesList)
		{
			SlidableGraph.Edge edge = sliderColumnSlidableGraph.getEdge(pieces);
			if (edge == null)
			{
				UnityEngine.Debug.LogError($"Piece {pieces} is not on any edge!", pieces);
			}
			else if (Array.Exists(sliderColumnEdgeNodes, (GameObject node) => node == edge.startNode) && Array.Exists(sliderColumnEdgeNodes, (GameObject node) => node == edge.endNode))
			{
				float num = Vector3.Distance(pieces.transform.position, edge.startPoint);
				float num2 = Vector3.Distance(pieces.transform.position, edge.endPoint);
				GameObject gameObject = ((num < num2) ? edge.startNode : edge.endNode);
				Vector3 b = ((num < num2) ? edge.startPoint : edge.endPoint);
				SlidableGraphPiece slidableGraphPiece = ((pieces == sliderColumnSlidableGraph.piecesList[0]) ? sliderColumnSlidableGraph.piecesList[1] : sliderColumnSlidableGraph.piecesList[0]);
				if (Vector3.Distance(slidableGraphPiece.currentLinearPosition, b) <= slidableGraphPiece.colliderRadius + pieces.colliderRadius)
				{
					gameObject = ((gameObject == edge.startNode) ? edge.endNode : edge.startNode);
				}
				pieces.transform.position = gameObject.transform.position;
				game.setParent(pieces.transform, gameObject.transform.parent);
				sliderColumnSlidableGraph.initPieces();
			}
		}
	}

	public override void onNpcChoiceSelected(Npc npc, int lineIndex, int choiceIndex)
	{
		onParrotDialogueButton(lineIndex, choiceIndex);
	}

	public override void onAllPlayersZoomedOut(Interactive interactive)
	{
		if (interactive == parrotNpc)
		{
			parrotNpc.goTo(0);
		}
	}

	public override void onTool(Item tool, ToolContext context)
	{
		if (tool == gunpowderPouch)
		{
			onGunpowderPouch(context);
		}
		if (tool == fishingRod)
		{
			onFishingRodTool(context);
		}
	}

	[DebugButton(null, Tint.Default, PostClickAction.HideButton, 0, new object[] { })]
	private void debugGetGems()
	{
		game.addItemToInventory(redGem.gameObject);
		game.addItemToInventory(blueGem.gameObject);
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void onBridgeCenterRedGemSlot()
	{
		Slot[] array = symbolSlots;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = true;
		}
		symbolsChestPlatformTweenState.transitionTo("Raise", 0.3f);
		symbolsChestZoomable.targetable = true;
		cutsceneMiddle.SetActive(value: true);
		cutsceneMiddleAnimation.play();
		game.startTimer(new CutsceneMiddleTimer(), 3f);
		game.finishPuzzle(Puzzle.Bridge);
	}

	private bool checkSnakes()
	{
		bool result = true;
		for (int i = 0; i < snakeSlots.Length; i++)
		{
			if (snakeSlots[i].insertedItem != snakes[i].Get<Item>(0))
			{
				result = false;
			}
		}
		return result;
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void solveSnakes()
	{
		UnityEngine.Debug.Log("Solved snakes");
		foreach (Ref<Item, Transform> snake in snakes)
		{
			snake.Get<Item>(0).targetable = false;
		}
		Slot[] array = snakeSlots;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = false;
		}
		columnPuzzleSolvedTween.transitionTo("Raise", 0.33f);
		Turnable[] array2 = slidableColumnTurnables;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].targetable = true;
		}
		snakesOpenEffect.Play();
		game.finishPuzzle(Puzzle.Snakes);
	}

	private void initSextant()
	{
		sextantCamTween.setState("Up", 0.4f);
		sextantSlidable.setAtPercent(0.4f);
	}

	private void onSextantSlidable()
	{
		sextantCamTween.transitionTo("Up", 5f, sextantSlidable.value);
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void solveSextantChest()
	{
		game.increaseZoomCounter(sextantChestZoom.Get<Zoomable>(0));
		sextantChestZoom.Get<Zoomable>(0).targetable = false;
		sextantChestLid.tweenState.transitionTo("Down", sextantChestLid.transitionSpeed, 0.2f);
		sextantChestLid.targetable = true;
		Turnable[] array = sextantTurnables;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = false;
		}
		symbolKeys[0].targetable = true;
		redGem.targetable = true;
		game.finishPuzzle(Puzzle.Sextant);
	}

	private void initSliderColumn()
	{
		sliderColumnSlidableGraph.isEdgeBlockedPredicate = delegate(GameObject node, GameObject neighbourNode, SlidableGraphPiece _)
		{
			if (!Array.Exists(sliderColumnEdgeNodes, (GameObject x) => x == node) || !Array.Exists(sliderColumnEdgeNodes, (GameObject x) => x == neighbourNode))
			{
				return false;
			}
			if (Vector3.Distance(node.transform.position, neighbourNode.transform.position) < 0.08f)
			{
				return false;
			}
			UnityEngine.Debug.Log($"node ({node.name}) - neighbour ({neighbourNode})");
			return true;
		};
		int num = 0;
		List<SlidableGraph.Edge> list = new List<SlidableGraph.Edge>();
		for (int num2 = 0; num2 < sliderColumnSlidableGraph.edges.Count; num2++)
		{
			SlidableGraph.Edge edge = sliderColumnSlidableGraph.edges[num2];
			if (!(edge.startNode.transform.parent == edge.endNode.transform.parent))
			{
				UnityEngine.Debug.Log($"{++num} - Edge {edge} at index {num2} connects 2 movable parts.");
				if (list.Exists((SlidableGraph.Edge e) => e.startNode == edge.startNode && e.endNode == edge.endNode))
				{
					UnityEngine.Debug.LogError($"[Pillar Checker] Edge {edge} is a duplicate of an existing edge.", edge.startNode);
				}
				list.Add(edge);
			}
		}
		for (int num3 = 0; num3 < sliderColumnEdgeNodes.Length; num3++)
		{
			GameObject gameObject = sliderColumnEdgeNodes[num3];
			for (int num4 = 0; num4 < sliderColumnEdgeNodes.Length; num4++)
			{
				GameObject gameObject2 = sliderColumnEdgeNodes[num4];
				if (!(gameObject == gameObject2) && !(gameObject.transform.parent != gameObject2.transform.parent))
				{
					UnityEngine.Debug.Log("Checking node pair: " + gameObject.name + " - " + gameObject2.name);
					if (sliderColumnSlidableGraph.tryGetEdge(gameObject, gameObject2, out var edge2))
					{
						int num5 = sliderColumnSlidableGraph.edges.IndexOf(edge2);
						UnityEngine.Debug.LogError($"Edge {edge2} at index {num5} is invalid (connects two edge nodes on the same parent).", gameObject);
					}
				}
			}
		}
		List<(GameObject, bool)>[] array = new List<(GameObject, bool)>[4]
		{
			new List<(GameObject, bool)>(),
			new List<(GameObject, bool)>(),
			new List<(GameObject, bool)>(),
			new List<(GameObject, bool)>()
		};
		GameObject[] array2 = sliderColumnEdgeNodes;
		foreach (GameObject gameObject3 in array2)
		{
			for (int num7 = 0; num7 < slidableColumnTurnables.Length; num7++)
			{
				if (gameObject3.transform.IsChildOf(slidableColumnTurnables[num7].transform))
				{
					bool item = gameObject3.transform.position.y > slidableColumnTurnables[num7].transform.position.y;
					array[num7].Add((gameObject3, item));
					break;
				}
			}
		}
		int num8 = 0;
		for (int num9 = 0; num9 < array.Length; num9++)
		{
			List<(GameObject, bool)> obj = array[num9];
			Turnable turnable = slidableColumnTurnables[num9];
			UnityEngine.Debug.Log($"Turnable {turnable} has following edge nodes:", turnable);
			foreach (var (gameObject4, flag) in obj)
			{
				UnityEngine.Debug.Log($"Node: {gameObject4} | Is upper edge: {flag}", gameObject4);
				num8++;
			}
		}
		int num10 = 0;
		for (int num11 = 0; num11 < array.Length - 1; num11++)
		{
			List<(GameObject, bool)> obj2 = array[num11];
			List<(GameObject, bool)> list2 = array[num11 + 1];
			foreach (var item6 in obj2)
			{
				var (gameObject5, _) = item6;
				if (!item6.Item2)
				{
					continue;
				}
				foreach (var item7 in list2)
				{
					GameObject item2 = item7.Item1;
					bool item3 = item7.Item2;
					if (!item3)
					{
						SlidableGraph.Edge edge3 = sliderColumnSlidableGraph.getEdge(gameObject5, item2);
						if (edge3 == null)
						{
							UnityEngine.Debug.LogError("[Pillar Checker] Missing edge between " + gameObject5.name + " and " + item2.name, gameObject5);
						}
						else
						{
							int num12 = sliderColumnSlidableGraph.edges.IndexOf(edge3);
							UnityEngine.Debug.Log($"[Pillar Checker] Correct edge at index {num12} between {gameObject5.name} and {item2.name}", gameObject5);
						}
						int num13 = list.FindIndex((SlidableGraph.Edge e) => (e.startNode == edge3.startNode && e.endNode == edge3.endNode) || (e.startNode == edge3.endNode && e.endNode == edge3.startNode));
						if (num13 >= 0)
						{
							list.RemoveAt(num13);
						}
						num10++;
					}
				}
			}
		}
		foreach (SlidableGraph.Edge item8 in list)
		{
			UnityEngine.Debug.LogError("EDGE THAT SHOULD NOT BE HERE: " + item8, item8.startNode);
		}
		UnityEngine.Debug.Log("Total edges between turnables: " + num10);
		UnityEngine.Debug.Log("Total nodes checked: " + num8);
		List<(GameObject, bool)>[] array3 = array;
		for (int num6 = 0; num6 < array3.Length; num6++)
		{
			foreach (var item9 in array3[num6])
			{
				GameObject item4 = item9.Item1;
				bool item5 = item9.Item2;
				Vector3 position = item4.transform.position;
				if (item5)
				{
					position.y -= sliderColumnSlidableGraph.nodeRadius;
				}
				else
				{
					position.y += sliderColumnSlidableGraph.nodeRadius;
				}
				item4.transform.position = position;
			}
		}
	}

	private void updateSliderColumn()
	{
		foreach (SlidableGraphPiece pieces in sliderColumnSlidableGraph.piecesList)
		{
			Vector3 position = slidableColumnTurnables[0].transform.position;
			Vector3 worldPosition = new Vector3(position.x, pieces.transform.position.y, position.z);
			pieces.transform.LookAt(worldPosition);
		}
	}

	private void onSliderColumnSlidableReleased(SlidableGraphPiece piece)
	{
		Turnable[] array = slidableColumnTurnables;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = true;
		}
		SlidableGraph.Edge edge;
		if (sliderColumnSlidableGraph.tryGetNode(piece, out var node))
		{
			game.setParent(piece.transform, node.transform.parent);
		}
		else if (sliderColumnSlidableGraph.tryGetEdge(piece, out edge))
		{
			game.setParent(piece.transform, edge.startNode.transform.parent);
		}
	}

	private bool checkSliderColumn()
	{
		foreach (SlidableGraphPiece pieces in sliderColumnSlidableGraph.piecesList)
		{
			if (game.isAnyPlayerInteracting(pieces.gameObject))
			{
				return false;
			}
		}
		float num = Vector3.Distance(sliderColumnSlidableGraph.piecesList[0].transform.position, sliderColumnSolution[0].transform.position);
		float num2 = Vector3.Distance(sliderColumnSlidableGraph.piecesList[1].transform.position, sliderColumnSolution[1].transform.position);
		slider1OnTarget = num < slidableColumnAcceptDistance;
		slider2OnTarget = num2 < slidableColumnAcceptDistance;
		if (num < slidableColumnAcceptDistance)
		{
			return num2 < slidableColumnAcceptDistance;
		}
		return false;
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void solveSliderColumn()
	{
		columnPuzzleSolvedTween.transitionToStateAdditive("NewState", 0.5f);
		dartColumnSlidableGraph.piecesList[0].targetable = false;
		dartColumnSlidableGraph.piecesList[1].targetable = false;
		Turnable[] array = slidableColumnTurnables;
		foreach (Turnable obj in array)
		{
			obj.targetable = false;
			obj.blocksRaycasts = false;
		}
		blueGem.targetable = true;
		game.finishPuzzle(Puzzle.SunMaze);
	}

	private void initHash()
	{
		hashSlidables[0].isEdgeBlockedPredicate = delegate(GameObject node, GameObject neighbourNode, SlidableGraphPiece _)
		{
			if (checkTargetNodes(0, node, neighbourNode, 1, 2) && isOnEdge(3, 1))
			{
				return true;
			}
			if (checkTargetNodes(0, node, neighbourNode, 2, 3) && isOnEdge(3, 0))
			{
				return true;
			}
			return (checkTargetNodes(0, node, neighbourNode, 3, 4) && isOnNode(3, 0)) ? true : false;
		};
		hashSlidables[1].isEdgeBlockedPredicate = (GameObject node, GameObject neighbourNode, SlidableGraphPiece _) => (checkTargetNodes(1, node, neighbourNode, 2, 3) && (isOnEdge(0, 0) || isOnEdge(0, 1)) && !isOnNode(0, 2)) ? true : false;
		hashSlidables[2].isEdgeBlockedPredicate = delegate(GameObject node, GameObject neighbourNode, SlidableGraphPiece _)
		{
			if (checkTargetNodes(2, node, neighbourNode, 2, 1) && isOnNode(3, 1))
			{
				return true;
			}
			if (checkTargetNodes(2, node, neighbourNode, 1, 2) && isOnAnyEdge(1, new List<int> { 0, 1, 2 }))
			{
				return true;
			}
			if (checkTargetNodes(2, node, neighbourNode, 2, 3) && isOnAnyEdge(5, new List<int> { 0, 1, 2 }))
			{
				return true;
			}
			return (checkTargetNodes(2, node, neighbourNode, 0, 1) && !isOnNode(5, 0) && isOnAnyEdge(5, new List<int> { 0, 1, 2 })) ? true : false;
		};
		hashSlidables[3].isEdgeBlockedPredicate = delegate(GameObject node, GameObject neighbourNode, SlidableGraphPiece _)
		{
			if (checkTargetNodes(3, node, neighbourNode, 0, 1))
			{
				if (!isOnNode(0, 0) && !isOnNode(0, 1) && !isOnNode(0, 3) && !isOnNode(0, 4))
				{
					return true;
				}
				if (isOnAnyEdge(2, new List<int> { 0, 1 }) && !isOnNode(2, 2))
				{
					return true;
				}
			}
			if (checkTargetNodes(3, node, neighbourNode, 1, 0) && !isOnNode(0, 0) && !isOnNode(0, 1) && !isOnNode(0, 3) && !isOnNode(0, 4))
			{
				return true;
			}
			return (checkTargetNodes(3, node, neighbourNode, 1, 2) && isOnAnyEdge(2, new List<int> { 0, 1 }) && !isOnNode(2, 2)) ? true : false;
		};
		hashSlidables[4].isEdgeBlockedPredicate = delegate(GameObject node, GameObject neighbourNode, SlidableGraphPiece _)
		{
			if (checkTargetNodes(4, node, neighbourNode, 1, 2) && !isOnNode(3, 2) && !isOnNode(3, 3) && !hash3Done)
			{
				return true;
			}
			return (checkTargetNodes(4, node, neighbourNode, 2, 3) && isOnEdge(1, 0)) ? true : false;
		};
		hashSlidables[5].isEdgeBlockedPredicate = delegate(GameObject node, GameObject neighbourNode, SlidableGraphPiece _)
		{
			if (checkTargetNodes(5, node, neighbourNode, 0, 1) && (!isOnNode(2, 0) || isOnAnyEdge(4, new List<int> { 0, 1 })))
			{
				return true;
			}
			if (checkTargetNodes(5, node, neighbourNode, 1, 2) && !isOnEdge(0, 3) && !isOnNode(0, 5) && !isOnEdge(0, 4))
			{
				return true;
			}
			return (checkTargetNodes(5, node, neighbourNode, 3, 4) && !isOnNode(0, 4) && !isOnNode(0, 5) && !isOnEdge(0, 4) && !isOnEdge(0, 3)) ? true : false;
		};
		bool checkTargetNodes(int slidableIndex, GameObject node, GameObject neighbourNode, int nodeIndex, int neighbourNodeIndex)
		{
			if (node == hashSlidables[slidableIndex].nodes[nodeIndex])
			{
				return neighbourNode == hashSlidables[slidableIndex].nodes[neighbourNodeIndex];
			}
			return false;
		}
		bool isOnAnyEdge(int slidableIndex, List<int> edgeIndexes)
		{
			if (hashSlidables[slidableIndex].tryGetEdge(hashSlidables[slidableIndex].piecesList[0], out var edge))
			{
				foreach (int edgeIndex in edgeIndexes)
				{
					if (edge == hashSlidables[slidableIndex].edges[edgeIndex])
					{
						UnityEngine.Debug.Log($"isOnAnyEdge {slidableIndex}, {edgeIndex}");
						return true;
					}
				}
			}
			return false;
		}
		bool isOnEdge(int slidableIndex, int edgeIndex)
		{
			SlidableGraph.Edge edge;
			bool flag = hashSlidables[slidableIndex].tryGetEdge(hashSlidables[slidableIndex].piecesList[0], out edge) && edge == hashSlidables[slidableIndex].edges[edgeIndex];
			UnityEngine.Debug.Log($"isOnEdge {slidableIndex}, {edgeIndex}: {flag}");
			return flag;
		}
		bool isOnNode(int slidableIndex, int nodeIndex)
		{
			GameObject node;
			bool flag = hashSlidables[slidableIndex].tryGetNode(hashSlidables[slidableIndex].piecesList[0], out node) && node == hashSlidables[slidableIndex].nodes[nodeIndex];
			UnityEngine.Debug.Log($"isOnNode {slidableIndex}, {nodeIndex}: {flag}");
			return flag;
		}
	}

	private void hashSliderOnNode(int sliderIndex, GameObject node, bool forceMove)
	{
		int num = hashSlidables[sliderIndex].nodes.IndexOf(node);
		bool flag = false;
		if (sliderIndex == 0 && num == 3)
		{
			flag = true;
		}
		if (sliderIndex == 1 && num == 4)
		{
			flag = true;
		}
		if (sliderIndex == 2 && num == 3)
		{
			flag = true;
		}
		if (sliderIndex == 3 && num == 2)
		{
			flag = true;
			hash3Done = true;
		}
		if (sliderIndex == 4 && num == 3)
		{
			flag = true;
		}
		if (sliderIndex == 5 && num == 4)
		{
			flag = true;
		}
		if (flag || forceMove)
		{
			game.killAllPointers();
			SlidableGraphPiece slidableGraphPiece = hashSlidables[sliderIndex].piecesList[0];
			int index = hashSlidables[sliderIndex].nodes.Count - 1;
			GameObject gameObject = hashSlidables[sliderIndex].nodes[index];
			slidableGraphPiece.targetable = false;
			game.startTransitionGlobal(new HashSliderDoneTransition(sliderIndex), slidableGraphPiece.transform, 2f, 0f, gameObject.transform.position);
			hashSolvedCount++;
		}
	}

	private bool checkHash()
	{
		return hashSolvedCount >= hashSlidables.Length;
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void solveHash()
	{
		game.increaseZoomCounter(hashZoomable);
		hashZoomable.targetable = false;
		hashDoor.transitionTo("Down", 0.5f);
		magnets[0].targetable = true;
		magnetsVisualEffect[0].enabled = true;
		for (int i = 0; i < hashSlidables.Length; i++)
		{
			hashSliderOnNode(i, null, forceMove: true);
		}
		game.finishPuzzle(Puzzle.WallSlider);
	}

	[DebugButton(null, Tint.Default, PostClickAction.HideButton, 0, new object[] { })]
	private void debugGetMagnetSlotKey()
	{
		game.addItemToInventory(magnetSlotKey.gameObject);
	}

	private bool checkMagnets()
	{
		if (magnetOpen)
		{
			return !magnetsSolved;
		}
		return false;
	}

	private void solveMagnets()
	{
		magnetsSolved = true;
		magnetSlot.targetable = true;
		magnetsDoorSequence.play();
		EventInstance[] array = magnetSound;
		for (int i = 0; i < array.Length; i++)
		{
			PineFmod.stop(array[i], STOP_MODE.IMMEDIATE);
		}
		Game obj = game;
		MagnetsDoneTransition transition = new MagnetsDoneTransition();
		Transform obj2 = magnetCylinders[0];
		Quaternion? rotation = Quaternion.identity;
		obj.startTransitionLocal(transition, obj2, 0.5f, 0f, null, rotation);
		Game obj3 = game;
		MagnetsDoneTransition transition2 = new MagnetsDoneTransition();
		Transform obj4 = magnetCylinders[1];
		rotation = Quaternion.Euler(0f, 0f, -120f);
		obj3.startTransitionLocal(transition2, obj4, 0.5f, 0f, null, rotation);
	}

	private void onMagnetUpdate()
	{
		magnetOpen = true;
		for (int i = 0; i < magnets.Length; i++)
		{
			if (magnetsSolved)
			{
				break;
			}
			Item item = magnets[i];
			Transform transform = magnetCylinders[i];
			float num = Vector3.Distance(item.transform.position, transform.position);
			Game.GamePlayerData player;
			bool flag = game.isAnyPlayerCarrying(item, out player);
			if (flag)
			{
				num = Vector3.Distance(player.lastTransformPose.position, transform.position);
			}
			if (num < 1f)
			{
				transform.Rotate(Vector3.forward, (i == 0) ? 2f : (-5f));
				magnetOpen = false;
				continue;
			}
			Vector3 fromDirection = magnetCylinderPointers[i].position - transform.position;
			fromDirection.y = 0f;
			fromDirection.Normalize();
			Vector3 toDirection = item.transform.position - transform.position;
			if (flag)
			{
				toDirection = player.lastTransformPose.position - transform.position;
			}
			toDirection.y = 0f;
			toDirection.Normalize();
			Quaternion quaternion = Quaternion.FromToRotation(fromDirection, toDirection) * transform.rotation;
			transform.rotation = Quaternion.RotateTowards(transform.rotation, quaternion, 50f * Time.deltaTime);
			bool num2 = Quaternion.Angle(transform.rotation, quaternion) > 1f;
			PLAYBACK_STATE state;
			bool flag2 = magnetSound[i].getPlaybackState(out state) == RESULT.OK && state == PLAYBACK_STATE.PLAYING;
			if (num2 && !flag2)
			{
				PineFmod.start(magnetSound[i]);
			}
			if (!num2)
			{
				PineFmod.stop(magnetSound[i], STOP_MODE.IMMEDIATE);
			}
			Vector3 vector = magnetCylinderPointers[i].position - transform.position;
			Vector3 to = magnetOpenPointers[i].position - transform.position;
			to.y = 0f;
			vector.y = 0f;
			vector.Normalize();
			to.Normalize();
			float num3 = Vector3.Angle(vector, to);
			magnetOpen &= num3 < 3f;
		}
		bool flag3 = !magnetsSolved && (game.isAnyPlayerCarrying(magnets[0]) || game.isAnyPlayerCarrying(magnets[1]));
		if (!bigMagnetEffect.isPlaying && flag3)
		{
			bigMagnetEffect.Play();
		}
		if (bigMagnetEffect.isPlaying && !flag3)
		{
			bigMagnetEffect.Stop(withChildren: true, ParticleSystemStopBehavior.StopEmittingAndClear);
		}
	}

	[DebugButton(null, Tint.Default, PostClickAction.HideButton, 0, new object[] { })]
	private void debugGetGunpowderPouch()
	{
		game.addItemToInventory(gunpowderPouch.gameObject);
		gunpowderPouch.targetable = true;
	}

	private void onMagnetSlot()
	{
		magnetDoor.transitionTo("NewState");
		gunpowderPouch.targetable = true;
		magnetSlot.targetable = false;
		magnetSlotKey.targetable = false;
		game.finishPuzzle(Puzzle.Magnets);
	}

	[DebugButton(null, Tint.Default, PostClickAction.HideButton, 0, new object[] { })]
	private void debugGetTileKeys()
	{
		Item[] array = symbolKeys;
		foreach (Item item in array)
		{
			item.gameObject.SetActive(value: true);
			item.targetable = true;
			game.addItemToInventory(item.gameObject);
		}
	}

	private void activateSymbolsSlider()
	{
		bool flag = symbolsHandleTs.getWeight("NewState") == 1f;
		symbolsSlidableGraph.piecesList[0].targetable = flag;
		if (flag)
		{
			symbolsSlidableGraph.initPieces();
		}
	}

	private void resetSymbolsChest()
	{
		symbolsCurrentIndex = 0;
		symbolsSlidableGraph.piecesList[0].targetable = false;
		symbolsCurrentCode = new int[7];
		float duration = ((symbolsHandleTs.getWeight("NewState") == 0f) ? 0f : 0.3f);
		for (int i = 0; i < symbolsCode.Length; i++)
		{
			if (symbolsCode[i].localRotation != Quaternion.Euler(0f, 0f, 0f))
			{
				PineFmod.start(sliderChestTurnableSound[i]);
			}
			Game obj = game;
			SetSymbolsCodeTransition transition = new SetSymbolsCodeTransition(reset: true);
			Transform obj2 = symbolsCode[i];
			Quaternion? rotation = Quaternion.Euler(0f, 0f, 0f);
			obj.startTransitionLocal(transition, obj2, duration, 0f, null, rotation);
		}
		Transform[] array = symbolsCode;
		foreach (Transform transform in array)
		{
			Game obj3 = game;
			SetSymbolsCodeTransition transition2 = new SetSymbolsCodeTransition(reset: true);
			Quaternion? rotation = Quaternion.Euler(0f, 0f, 0f);
			obj3.startTransitionLocal(transition2, transform, duration, 0f, null, rotation);
		}
		Game obj4 = game;
		ResetSymbolsSliderTransition transition3 = new ResetSymbolsSliderTransition();
		Transform obj5 = symbolsGrafPiece;
		Vector3? position = symbolsChestSliderStartPos.localPosition;
		obj4.startTransitionLocal(transition3, obj5, duration, 0f, position);
	}

	private void onSymbolsChestSlot()
	{
		bool flag = true;
		Slot[] array = symbolSlots;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].insertedItem == null)
			{
				flag = false;
				break;
			}
		}
		symbolsSlidableGraph.piecesList[0].targetable = flag;
		if (symbolsHandleTs.findStateByName("NewState").targetWeight == 1f)
		{
			activateSymbolsSlider();
		}
		symbolsHandleTs.transitionTo("NewState", 1f, flag ? 1f : 0f);
	}

	private void onSymbolsChestSlidableMoved(GameObject node)
	{
		if (symbolsCurrentIndex >= 7)
		{
			return;
		}
		int num = symbolsSlidableGraph.nodes.IndexOf(node);
		int num2 = Array.IndexOf(symbolKeys, symbolSlots[num].insertedItem);
		symbolsCurrentCode[symbolsCurrentIndex] = num2 + 1;
		float x = symbolsCodeAngles[symbolsCurrentCode[symbolsCurrentIndex]];
		Game obj = game;
		SetSymbolsCodeTransition transition = new SetSymbolsCodeTransition(reset: false);
		Transform obj2 = symbolsCode[symbolsCurrentIndex];
		Quaternion? rotation = Quaternion.Euler(x, 0f, 0f);
		obj.startTransitionLocal(transition, obj2, 0.3f, 0f, null, rotation);
		PineFmod.start(sliderChestTurnableSound[symbolsCurrentIndex]);
		symbolsCurrentIndex++;
		if (symbolsCurrentIndex == 7)
		{
			symbolsSlidableGraph.piecesList[0].targetable = false;
			if (game.hasAuthority(symbolsSlidableGraph.piecesList[0]))
			{
				game.killAllPointers();
			}
		}
	}

	private void checkSymbolsChestForReset()
	{
		if (symbolsCurrentIndex != 7)
		{
			return;
		}
		bool flag = true;
		for (int i = 0; i < symbolsChestSolution.Length; i++)
		{
			if (symbolsChestSolution[i] != symbolsCurrentCode[i])
			{
				flag = false;
			}
		}
		if (!flag)
		{
			resetSymbolsChest();
		}
	}

	private bool checkSymbolsChest()
	{
		if (symbolsCurrentIndex == 7)
		{
			bool result = true;
			for (int i = 0; i < symbolsChestSolution.Length; i++)
			{
				if (symbolsChestSolution[i] != symbolsCurrentCode[i])
				{
					result = false;
				}
			}
			return result;
		}
		return false;
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void solveSymbolsChest()
	{
		treasureHint.targetable = true;
		treasureHintSwitch.targetable = true;
		Slot[] array = symbolSlots;
		foreach (Slot slot in array)
		{
			slot.targetable = false;
			if (slot.insertedItem != null)
			{
				slot.insertedItem.targetable = false;
			}
		}
		symbolsChestLid.transitionTo("Down");
		symbolsChestZoomable.targetable = false;
		symbolsSlidableGraph.piecesList[0].targetable = false;
		game.increaseZoomCounter(symbolsChestZoomable);
		game.finishPuzzle(Puzzle.SymbolsChest);
	}

	private void onParrotDialogueButton(int orderIndex, int choiceIndex)
	{
		if ((orderIndex == 1 || orderIndex == 2 || orderIndex == 3) && choiceIndex == 1)
		{
			game.increaseZoomCounter(parrotNpc);
		}
	}

	private bool checkParrot()
	{
		foreach (SlidableGraphPiece pieces in parrotSlidableGraph.piecesList)
		{
			if (game.isAnyPlayerInteracting(pieces.gameObject))
			{
				return false;
			}
		}
		bool result = true;
		for (int i = 0; i < parrotSlidableGraph.piecesList.Count; i++)
		{
			SlidableGraphPiece piece = parrotSlidableGraph.piecesList[i];
			if (parrotSlidableGraph.tryGetNode(piece, out var node))
			{
				if (parrotSolution[i] != parrotSlidableGraph.nodes.IndexOf(node))
				{
					result = false;
				}
				continue;
			}
			result = false;
			break;
		}
		return result;
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void solveParrot()
	{
		game.increaseZoomCounter(parrotBoardZoom.gameObject);
		game.increaseZoomCounter(parrotNpc.gameObject);
		parrotDoor.transitionTo("Down");
		parrotNpc.targetable = false;
		parrotBoardZoom.targetable = false;
		symbolKeys[1].targetable = true;
		symbolKeys[2].targetable = true;
		game.finishPuzzle(Puzzle.Parrot);
	}

	private void updateSafe()
	{
		for (int i = 0; i < safeDials.Length; i++)
		{
			float weight = 0f;
			float num = 0f;
			if (safeDials[i].value == 0)
			{
				weight = 1f;
			}
			if (safeDials[i].value == 10)
			{
				num = 1f;
			}
			safePinsSkulls[i].transitionTo("Down", 7f, weight);
			safePins[i].transitionTo("Down", 7f, num);
			if (i == 0)
			{
				safeBoltTweens[i].transitionTo("Down", 7f, 1f - num);
			}
		}
	}

	private bool checkSafe()
	{
		Dial[] array = safeDials;
		foreach (Dial dial in array)
		{
			if (game.isAnyPlayerInteracting(dial.gameObject))
			{
				return false;
			}
		}
		for (int j = 1; j < safePins.Length; j++)
		{
			if (safePins[j].findStateByName("Down").targetWeight == 0f)
			{
				return false;
			}
		}
		return true;
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void solveSafe()
	{
		Dial[] array = safeDials;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = false;
		}
		safeDoor.transitionTo("Open");
		safeDoorEffect.Play();
		StoneKey.targetable = true;
		magnetSlotKey.targetable = true;
		game.finishPuzzle(Puzzle.SkullLock);
	}

	[DebugButton(null, Tint.Default, PostClickAction.HideButton, 0, new object[] { })]
	private void debugGetDartTokens()
	{
		Item[] array = dartColumnTokens;
		foreach (Item item in array)
		{
			item.targetable = true;
			game.addItemToInventory(item.gameObject);
		}
	}

	[DebugButton(null, Tint.Default, PostClickAction.HideButton, 0, new object[] { })]
	private void debugGetDartColumnKeys()
	{
		Item[] acceptItems = dartColumnCoinSlots[0].acceptItems;
		foreach (Item item in acceptItems)
		{
			if (item.slot == null)
			{
				item.targetable = true;
				game.addItemToInventory(item.gameObject);
			}
		}
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void debugGoToPillarsGraph()
	{
		redGemOpen.transitionTo("NewState");
		centerPathBlocker1.SetActive(value: false);
		solveDartColumnKeys();
		game.teleportPlayer(new Vector3(15f, -0.5f, -12f));
	}

	private bool checkDartColumnKeys()
	{
		bool result = true;
		Slot[] array = dartColumnCoinSlots;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].insertedItem == null)
			{
				result = false;
				break;
			}
		}
		return result;
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void solveDartColumnKeys()
	{
		dartColumnsRaised = true;
		dartColumnBotRiser.transitionTo("NewState");
		TweenState[] array = dartColumnRisers;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].transitionTo("Down");
		}
		ParticleSystem[] array2 = dartColumnParticles;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].Play();
		}
		dartLeverBaseZoomable.targetable = true;
		dartLeverSequence.play(0f, 3f);
		dartLever.targetable = true;
		dartColumnsActivate();
		game.finishPuzzle(Puzzle.Medallions);
	}

	private void onDartLeverTweenDone()
	{
		if (dartColumnsRaised)
		{
			dartLeverSequence.play(-1f, 4f, 3f);
			dartLever.targetable = false;
		}
	}

	private void dartColumnsActivate(bool activate = true)
	{
		for (int i = 0; i < dartColumnTurnables.Length; i++)
		{
			dartColumnSlidableGraph.piecesList[i].targetable = activate;
			dartColumnTurnables[i].targetable = activate;
		}
	}

	private void fireDarts()
	{
		dartTargets.Clear();
		resetDarts();
		for (int i = 0; i < darts.Length; i++)
		{
			Collider collider = darts[i].Get<Collider>(0u);
			collider.enabled = false;
			Vector3 value = darts[i].Get<Transform>(0f).right * 5f + darts[i].Get<Transform>(0f).position;
			float duration = 1f;
			Collider collider2 = null;
			if (Physics.Raycast(darts[i].Get<Transform>(0f).position, darts[i].Get<Transform>(0f).right, out var hitInfo, 5f, -1, QueryTriggerInteraction.Ignore))
			{
				collider2 = hitInfo.collider;
				value = hitInfo.point;
				duration = 0.1f * hitInfo.distance;
				UnityEngine.Debug.Log($"DART {i} SENT, collider {collider2.name}");
			}
			dartTargets.Add(collider2);
			game.startTransitionGlobal(new DartArrivedTransition(i, collider2?.transform), darts[i].Get<Transform>(0f), duration, 0f, value, null, null, Interpolation.Linear);
			collider.enabled = true;
		}
		areDartsReset = false;
		dartsArrived = 0;
	}

	private void onDartTimer(int dartIndex, Transform newParent)
	{
		dartsArrived++;
		if (newParent != null)
		{
			game.setParent(darts[dartIndex].Get<Transform>(0f), newParent);
		}
		bool active = false;
		TweenState[] array = dartColumnRisers;
		foreach (TweenState tweenState in array)
		{
			if (darts[dartIndex].Get<Transform>(0f).IsChildOf(tweenState.transform))
			{
				active = true;
			}
		}
		darts[dartIndex].Get<GameObject>(0).SetActive(active);
		dartColumnsActivate();
		dartEffect[dartIndex].Play();
	}

	private bool checkDartsSolution()
	{
		BoxCollider[] array = dartTargetColliders;
		foreach (BoxCollider boxCollider in array)
		{
			if (boxCollider == null)
			{
				return false;
			}
			bool flag = false;
			for (int num = dartTargets.Count - 1; num >= 0; num--)
			{
				if (dartTargets[num] != null && boxCollider.gameObject == dartTargets[num].gameObject)
				{
					flag = true;
					dartTargets.RemoveAt(num);
					break;
				}
			}
			if (!flag)
			{
				return false;
			}
		}
		return true;
	}

	private bool checkDarts()
	{
		if (dartsArrived >= 4)
		{
			return checkDartsSolution();
		}
		return false;
	}

	[DebugButton(null, Tint.Blue, PostClickAction.HideButton, 0, new object[] { false })]
	private void solveDarts()
	{
		UnityEngine.Debug.Log("Solved darts");
		dartColumnsActivate(activate: false);
		dartLever.targetable = false;
		openCircleDoor.transitionTo("NewState", 0.4f);
		openCircleDoorEffect.Play();
		dartLeverBaseZoomable.targetable = false;
		game.finishPuzzle(Puzzle.Darts);
	}

	private void resetDarts()
	{
		if (!areDartsReset)
		{
			for (int i = 0; i < darts.Length; i++)
			{
				game.setParent(darts[i].Get<Transform>(0f), dartOriginalParents[i]);
				darts[i].Get<Transform>(0f).position = dartOrigins[i].transform.position;
				darts[i].Get<Transform>(0f).rotation = dartOrigins[i].transform.rotation;
				darts[i].Get<GameObject>(0).SetActive(value: true);
			}
			areDartsReset = true;
		}
	}

	private void initGunpowder()
	{
		trailGunpowders = new List<TrailGunpowder>();
		foreach (Ref<Trackable, Renderer, TweenState, GameObject> gunpowderTrailPowder in gunpowderTrailPowders)
		{
			GameObject gameObject = gunpowderTrailPowder.Get<GameObject>((short)0).transform.Find("BruningVFX/pf_vfx-ult_xp-ckit_psys_oneshot_realisticFire-alpha")?.gameObject;
			GameObject gameObject2 = gunpowderTrailPowder.Get<GameObject>((short)0).transform.Find("Start")?.gameObject;
			GameObject gameObject3 = gunpowderTrailPowder.Get<GameObject>((short)0).transform.Find("End")?.gameObject;
			TrailGunpowder item = new TrailGunpowder
			{
				trail = gunpowderTrailPowder,
				trackable = gunpowderTrailPowder.Get<Trackable>(0),
				renderer = gunpowderTrailPowder.Get<Renderer>(0f),
				tween = gunpowderTrailPowder.Get<TweenState>(0u),
				start = gameObject2,
				startTransform = gameObject2.transform,
				end = gameObject3,
				endTransform = gameObject3.transform,
				particles = gameObject,
				particlesTransform = gameObject.transform,
				startTrails = new List<int>(),
				endTrails = new List<int>()
			};
			trailGunpowders.Add(item);
			trailGunpowderStates.Add(item: false);
			trailGunpowderFireFromStarts.Add(item: false);
		}
		for (int i = 0; i < trailGunpowders.Count; i++)
		{
			TrailGunpowder trailGunpowder = trailGunpowders[i];
			trailGunpowder.dissolve = gunpowderDissolve[i];
			if (!trailGunpowder.renderer.enabled)
			{
				trailGunpowderStates[i] = true;
				trailGunpowder.dissolve.setWeight("NewState");
			}
			trailGunpowder.renderer.enabled = true;
		}
		float num = 0.25f;
		foreach (TrailGunpowder trailGunpowder2 in trailGunpowders)
		{
			foreach (TrailGunpowder trailGunpowder3 in trailGunpowders)
			{
				if (trailGunpowder2 != trailGunpowder3)
				{
					if (trailGunpowder2.start != null && trailGunpowder3.start != null && Vector3.Distance(trailGunpowder2.startTransform.position, trailGunpowder3.startTransform.position) <= num)
					{
						trailGunpowder2.startTrails.Add(trailGunpowders.IndexOf(trailGunpowder3));
					}
					if (trailGunpowder2.start != null && trailGunpowder3.end != null && Vector3.Distance(trailGunpowder2.startTransform.position, trailGunpowder3.endTransform.position) <= num)
					{
						trailGunpowder2.startTrails.Add(trailGunpowders.IndexOf(trailGunpowder3));
					}
					if (trailGunpowder2.end != null && trailGunpowder3.start != null && Vector3.Distance(trailGunpowder2.endTransform.position, trailGunpowder3.startTransform.position) <= num)
					{
						trailGunpowder2.endTrails.Add(trailGunpowders.IndexOf(trailGunpowder3));
					}
					if (trailGunpowder2.end != null && trailGunpowder3.end != null && Vector3.Distance(trailGunpowder2.endTransform.position, trailGunpowder3.endTransform.position) <= num)
					{
						trailGunpowder2.endTrails.Add(trailGunpowders.IndexOf(trailGunpowder3));
					}
				}
			}
		}
	}

	private void onGunpowderPouch(ToolContext context)
	{
		if (gunpowderAnimActive || context.state != ToolState.Start || !(context.currentTarget != null))
		{
			return;
		}
		int num = -1;
		for (int i = 0; i < trailGunpowders.Count; i++)
		{
			if (trailGunpowders[i].trackable.gameObject == context.currentTarget.gameObject)
			{
				num = i;
				break;
			}
		}
		if (num == -1)
		{
			return;
		}
		if (trailGunpowderOnFire.Count != 0)
		{
			gunpowderPouch.unlockItemInteractions = game.hasAuthority(gunpowderPouch.gameObject);
			gunpowderPouch.hideItemInHand = false;
		}
		else if (trailGunpowderStates[num])
		{
			gunpowderPouchImpostor.Get<GameObject>(0u).SetActive(value: true);
			gunpowderPouchImpostor.Get<Transform>(0f).position = context.currentTarget.transform.position;
			gunpowderPouchImpostor.Get<Transform>(0f).rotation = context.currentTarget.transform.rotation * Quaternion.Euler(0f, 0f, 85.425f);
			Vector3 position = gunpowderPouchImpostorRendTarget.Get<Transform>().position;
			Quaternion rotation = gunpowderPouchImpostorRendTarget.Get<Transform>().rotation;
			Vector3 one = Vector3.one;
			Transform transform = null;
			if (game.hasAuthority(gunpowderPouch))
			{
				GameObject impostorInHand = game.getImpostorInHand();
				if (impostorInHand != null)
				{
					transform = impostorInHand.transform;
				}
			}
			else
			{
				Game.GamePlayerData playerWithItemInInventory = game.getPlayerWithItemInInventory(gunpowderPouch.gameObject);
				if (playerWithItemInInventory != null)
				{
					GameObject inHandItemBubble = playerWithItemInInventory.inHandItemBubble;
					if (inHandItemBubble != null)
					{
						transform = inHandItemBubble.transform;
					}
				}
			}
			if (transform != null)
			{
				gunpowderPouchImpostorRendTarget.Get<Transform>().position = transform.position;
				gunpowderPouchImpostorRendTarget.Get<Transform>().rotation = transform.rotation;
				gunpowderPouchImpostorRendTarget.Get<Transform>().localScale = transform.localScale;
			}
			gunpowderPouch.hideItemInHand = true;
			game.startTransitionGlobal(new GunpowderFlyInTransition(), gunpowderPouchImpostorRendTarget, 0.5f, 0f, position, rotation, one);
			for (int j = 0; j < gunpowderTrailPowders.Length; j++)
			{
				if (gunpowderTrailPowders[j].Get<Trackable>(0) == context.currentTarget)
				{
					currentGunpowderIndex = j;
					break;
				}
			}
			gunpowderAnimActive = true;
		}
		else
		{
			trailGunpowderStates[num] = true;
			trailGunpowders[num].dissolve.transitionTo("NewState");
			gunpowderPouch.unlockItemInteractions = game.hasAuthority(gunpowderPouch.gameObject);
			gunpowderPouch.hideItemInHand = false;
		}
	}

	public override void onPacket(Packet packet)
	{
		if (!(packet is OnStartGunpowderPacket onStartGunpowderPacket))
		{
			return;
		}
		UnityEngine.Debug.Log(string.Format("[Gunpowder] Received {0} | Count {1}", "OnStartGunpowderPacket", onStartGunpowderPacket.gunpowderStates.Count));
		for (int i = 0; i < onStartGunpowderPacket.gunpowderStates.Count && i < trailGunpowders.Count; i++)
		{
			if (trailGunpowderStates[i] != onStartGunpowderPacket.gunpowderStates[i])
			{
				UnityEngine.Debug.LogWarning("[Gunpowder] State missmatch: " + i);
			}
			trailGunpowderStates[i] = onStartGunpowderPacket.gunpowderStates[i];
			trailGunpowders[i].dissolve.transitionTo("NewState", 1f, trailGunpowderStates[i] ? 1f : 0f);
		}
		startGunpowderFire();
	}

	private void startGunpowderFireSynced()
	{
		if (!game.isHost())
		{
			return;
		}
		List<bool> list = new List<bool>();
		foreach (bool trailGunpowderState in trailGunpowderStates)
		{
			list.Add(trailGunpowderState);
		}
		game.session.send(new OnStartGunpowderPacket
		{
			gunpowderStates = list
		});
		startGunpowderFire();
	}

	private void startGunpowderFire()
	{
		trailGunpowderOnFire.Clear();
		if (gunpowderFire(0, gunpowderSpark.transform.position))
		{
			trailGunpowderOnFire.Add(trailGunpowders[0]);
			gunpowderStartFlameSwitch.targetable = false;
		}
		gunpowderStartFlameSwitch.tweenState.setState("Down", 0f);
		TweenState[] array = gunPowderBarrelMoveAnim;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].setWeight("NewState", 0f);
		}
	}

	private void completeGunpowderFire()
	{
		gunpowderStartFlameSwitch.targetable = true;
		foreach (TrailGunpowder trailGunpowder in trailGunpowders)
		{
			trailGunpowder.tween.setState("Default", 0f);
		}
	}

	private void explodeBarrel(int index)
	{
		gunpowderBarrelExplosions[index].Get<ParticleSystem>(0).Play();
		gunPowderBarrelMoveAnim[index].transitionTo("NewState");
		float targetWeight = endDoor.findStateByName("Up").targetWeight;
		endDoor.transitionTo("Up", 1.5f, targetWeight + 0.25f);
		UnityEngine.Debug.Log($"EXPLODE BARREL {index}");
		PineFmod.playOneShotSoundAttached("event:/Sound Effects/05 Misc/Explosions/Small_Explosion", gunpowderBarrelExplosions[index].Get<GameObject>(0f));
	}

	private void onGunpowderEndDoorTween()
	{
		float weight = endDoor.findStateByName("Up").weight;
		if (!(weight >= 0.8f) && weight > 0.1f)
		{
			endDoor.transitionTo("Up", 0.3f, 0f);
		}
	}

	private bool checkGunpowder()
	{
		return endDoor.getWeight("Up") >= 0.8f;
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void solveGunpowder()
	{
		UnityEngine.Debug.Log("SOLVED GUNPOWDER");
		PineFmod.playOneShotSound("event:/Sound Effects/04 Items/Weapons/Cannon/Cannon_Explosion");
		endDoor.transitionTo("Up", 2f);
		levelExit.targetable = true;
		game.finishPuzzle(Puzzle.Barrels);
		game.levelCompleted();
	}

	[DebugButton(null, Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void debugGetFishingRod()
	{
		game.addItemToInventory(fishingRod.gameObject);
	}

	private void checkFlowersSwitch(Switch3D targetSwitch)
	{
		for (int i = 0; i < fivePetalFlowersHide.Length; i++)
		{
			if (targetSwitch == fivePetalFlowersHide[i].Get<Switch3D>(0f))
			{
				if (game.hasAuthority(targetSwitch))
				{
					game.addItemToInventory(fivePetalFlowers[i].gameObject);
				}
				fivePetalFlowersHide[i].Get<GameObject>(0).SetActive(value: false);
			}
		}
		for (int j = 0; j < longFlowersHide.Length; j++)
		{
			if (targetSwitch == longFlowersHide[j].Get<Switch3D>(0f))
			{
				if (game.hasAuthority(targetSwitch))
				{
					game.addItemToInventory(longFlowers[j].gameObject);
				}
				longFlowersHide[j].Get<GameObject>(0).SetActive(value: false);
			}
		}
		for (int k = 0; k < roseLikeFlowersHide.Length; k++)
		{
			if (targetSwitch == roseLikeFlowersHide[k].Get<Switch3D>(0f))
			{
				if (game.hasAuthority(targetSwitch))
				{
					game.addItemToInventory(roseLikeFlowers[k].gameObject);
				}
				roseLikeFlowersHide[k].Get<GameObject>(0).SetActive(value: false);
			}
		}
		for (int l = 0; l < starFlowerHide.Length; l++)
		{
			if (targetSwitch == starFlowerHide[l].Get<Switch3D>(0f))
			{
				if (game.hasAuthority(targetSwitch))
				{
					game.addItemToInventory(starFlowers[l].gameObject);
				}
				starFlowerHide[l].Get<GameObject>(0).SetActive(value: false);
			}
		}
		for (int m = 0; m < trubaFlowerHide.Length; m++)
		{
			if (targetSwitch == trubaFlowerHide[m].Get<Switch3D>(0f))
			{
				if (game.hasAuthority(targetSwitch))
				{
					game.addItemToInventory(trubaFlowers[m].gameObject);
				}
				trubaFlowerHide[m].Get<GameObject>(0).SetActive(value: false);
			}
		}
	}

	private void initFishingRod()
	{
		List<Item> list = new List<Item>();
		list.AddRange(specialFlowers);
		list.AddRange(trubaFlowers);
		list.AddRange(starFlowers);
		list.AddRange(roseLikeFlowers);
		list.AddRange(longFlowers);
		list.AddRange(fivePetalFlowers);
		baitSlot.acceptItems = list.ToArray();
		fishingCatchesLeft = new List<FishingCatchCollection>();
		GameObject[] array = hookBaits;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(value: false);
		}
		array = imposterHookBaits;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(value: false);
		}
		throwingHookScale = fishingHook.Get<Transform>(0).lossyScale;
		addCatch(crabKey, FishingWaterCatchType.MediumCrab, FishingBaitType.Special);
		addCatches(smallFish, FishingWaterCatchType.DeepFish, FishingBaitType.FivePetal);
		addCatches(mediumFish, FishingWaterCatchType.DeepFish, FishingBaitType.Truba);
		addCatches(largeFish, FishingWaterCatchType.DeepFish, FishingBaitType.Rose);
		addCatches(mediumCrabs, FishingWaterCatchType.MediumCrab, FishingBaitType.Truba);
		addCatches(smallSnails, FishingWaterCatchType.ShallowSnail, FishingBaitType.FivePetal);
		addCatches(mediumSnails, FishingWaterCatchType.ShallowSnail, FishingBaitType.Truba);
		addCatches(largeSnails, FishingWaterCatchType.ShallowSnail, FishingBaitType.Rose);
		void addCatch(Item ccatch, FishingWaterCatchType catchType, FishingBaitType baitType)
		{
			addCatches(new Item[1] { ccatch }, catchType, baitType);
		}
		void addCatches(Item[] catches, FishingWaterCatchType catchType, FishingBaitType baitType)
		{
			FishingCatchCollection item = new FishingCatchCollection
			{
				waterCatchType = catchType,
				baitType = baitType,
				catches = catches.ToList()
			};
			fishingCatchesLeft.Add(item);
		}
	}

	private void onFishingRodTool(ToolContext context)
	{
		if (fishingAnimActive)
		{
			return;
		}
		if (context.state == ToolState.Start)
		{
			Game.GamePlayerData playerWithItemInInventory = game.getPlayerWithItemInInventory(fishingRod.gameObject);
			fishingRodImposter.Get<GameObject>(0f).SetActive(value: true);
			fishingRodImposter.Get<Transform>(0).position = playerWithItemInInventory.playerCameraRay.origin + playerWithItemInInventory.playerCameraRay.direction * 1f;
			fishingRodImposter.Get<Transform>(0).rotation = Quaternion.LookRotation(playerWithItemInInventory.playerCameraRay.direction) * Quaternion.Euler(0f, 90f, -60f);
			Vector3 position = fishingRodImposter.Get<Transform>(0).position;
			Quaternion rotation = fishingRodImposter.Get<Transform>(0).rotation;
			Vector3 one = Vector3.one;
			Transform transform = null;
			if (game.hasAuthority(fishingRod))
			{
				GameObject impostorInHand = game.getImpostorInHand();
				if (impostorInHand != null)
				{
					transform = impostorInHand.transform;
				}
			}
			else if (playerWithItemInInventory != null)
			{
				GameObject inHandItemBubble = playerWithItemInInventory.inHandItemBubble;
				if (inHandItemBubble != null)
				{
					transform = inHandItemBubble.transform;
				}
			}
			if (transform != null)
			{
				fishingRodImposter.Get<Transform>(0).position = transform.position;
				fishingRodImposter.Get<Transform>(0).rotation = transform.rotation;
				fishingRodImposter.Get<Transform>(0).localScale = transform.localScale;
			}
			fishingRod.hideItemInHand = true;
			fishingRodImposter.Get<GameObject>(0f).SetActive(value: true);
			game.startTransitionGlobal(new FishingRodFlyInTransition(), fishingRodImposter, 1f, 0f, position, rotation, one);
			if (baitSlot.insertedItem != null)
			{
				FishingBaitType baitType = getBaitType(baitSlot.insertedItem);
				if (baitType != FishingBaitType.None)
				{
					int num = (int)(baitType - 1);
					baitSlot.insertedItem.gameObject.SetActive(value: false);
					hookBaits[num].SetActive(value: true);
					imposterHookBaits[num].SetActive(value: true);
				}
			}
			fishingAnimActive = true;
			FishingWaterCatchType fishingWaterCatchType = FishingWaterCatchType.None;
			if (fishingShallowTriggers.Contains(context.target))
			{
				fishingWaterCatchType = FishingWaterCatchType.ShallowSnail;
			}
			if (fishingMediumTriggers.Contains(context.target))
			{
				fishingWaterCatchType = FishingWaterCatchType.MediumCrab;
			}
			if (fishingDeepTriggers.Contains(context.target))
			{
				fishingWaterCatchType = FishingWaterCatchType.DeepFish;
			}
			currentWaterType = fishingWaterCatchType;
			fishingVFXTransform.position = new Vector3(context.targetHitPoint.x, fishingVFXTransform.position.y, context.targetHitPoint.z);
			fishingVFX.Play();
		}
		else if (context.state == ToolState.End)
		{
			GameObject[] array = hookBaits;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetActive(value: false);
			}
			array = imposterHookBaits;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetActive(value: false);
			}
			if (baitSlot.insertedItem != null)
			{
				baitSlot.insertedItem.gameObject.SetActive(value: true);
			}
		}
	}

	[DebugButton(null, Tint.Default, PostClickAction.HideButton, 0, new object[] { })]
	private void DebugGetCrab()
	{
		game.addItemToInventory(crabKey.gameObject);
	}

	private void onHookFlyOut()
	{
		Item item = null;
		Vector3 fStartScale = Vector3.one;
		FishingBaitType fishingBaitType = FishingBaitType.None;
		if (baitSlot.insertedItem != null)
		{
			fishingBaitType = getBaitType(baitSlot.insertedItem);
			item = goFish(fishingBaitType, currentWaterType);
			if (item != null)
			{
				UnityEngine.Debug.Log("FISHED IN " + currentWaterType);
				item.targetable = false;
				game.setParent(item.transform, game.levelContainerTransform);
				item.transform.position = fishingSpawnPoint.position;
				item.transform.rotation = fishingSpawnPoint.rotation;
				item.transform.Rotate(0f, 90f, 0f, Space.Self);
				fStartScale = item.transform.localScale;
				item.transform.localScale = Vector3.zero;
				Item insertedItem = baitSlot.insertedItem;
				game.removeItemFromSlot(baitSlot.insertedItem);
				insertedItem.gameObject.SetActive(value: false);
				GameObject[] array = hookBaits;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].SetActive(value: false);
				}
				array = imposterHookBaits;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].SetActive(value: false);
				}
			}
		}
		game.startTimer(new FishingRodFlyBackTimer(fishingRodImposter.Get<Transform>(0).position, fishingRodImposter.Get<Transform>(0).rotation, fishingRodImposter.Get<Transform>(0).localScale, item, fStartScale), 0.5f);
	}

	[DebugButton(null, Tint.Default, (PostClickAction)0, 0, new object[] { }, postClickAction = PostClickAction.HideButton)]
	private void debugSolveFishing()
	{
		goFish(FishingBaitType.Special, FishingWaterCatchType.MediumCrab);
		DebugGetCrab();
	}

	private Item goFish(FishingBaitType bait, FishingWaterCatchType waterCatch)
	{
		if (waterCatch == FishingWaterCatchType.MediumCrab && bait == FishingBaitType.Special)
		{
			if (!collectedFishingKey)
			{
				collectedFishingKey = true;
				game.finishPuzzle(Puzzle.Fishing);
				return crabKey;
			}
			FishingCatchCollection catchCollection = fishingCatchesLeft.Find((FishingCatchCollection x) => x.baitType == bait && x.waterCatchType == waterCatch && x.catches.Count > 0);
			return findInCollection(catchCollection);
		}
		FishingCatchCollection catchCollection2 = fishingCatchesLeft.Find((FishingCatchCollection x) => x.baitType == bait && x.waterCatchType == waterCatch && x.catches.Count > 0);
		return findInCollection(catchCollection2);
		Item findInCollection(FishingCatchCollection fishingCatchCollection)
		{
			if (fishingCatchCollection != null)
			{
				Item item = fishingCatchCollection.catches.FirstOrDefault();
				if (item != null)
				{
					fishingCatchCollection.catches.Remove(item);
					if (fishingCatchCollection.catches.Count == 0)
					{
						fishingCatchesLeft.Remove(fishingCatchCollection);
					}
				}
				return item;
			}
			return null;
		}
	}

	private void onPickedUpCatch()
	{
		baitSlot.targetable = true;
		fishingCatchOnHook = null;
	}

	private FishingBaitType getBaitType(Item bait)
	{
		if (fivePetalFlowers.Contains(bait))
		{
			return FishingBaitType.FivePetal;
		}
		if (trubaFlowers.Contains(bait))
		{
			return FishingBaitType.Truba;
		}
		if (roseLikeFlowers.Contains(bait))
		{
			return FishingBaitType.Rose;
		}
		if (specialFlowers.Contains(bait))
		{
			return FishingBaitType.Special;
		}
		if (starFlowers.Contains(bait))
		{
			return FishingBaitType.Star;
		}
		if (longFlowers.Contains(bait))
		{
			return FishingBaitType.Long;
		}
		return FishingBaitType.None;
	}

	public override void onInitHints()
	{
		game.setPuzzleConditions(Puzzle.SunMaze, Puzzle.Snakes);
		game.setPuzzleConditions(Puzzle.Sextant, Puzzle.SunMaze);
		game.setPuzzleConditions(Puzzle.Fishing, Puzzle.Sextant);
		game.setPuzzleConditions(Puzzle.CrabKey, Puzzle.Fishing);
		game.setPuzzleConditions(Puzzle.Parrot, Puzzle.CrabKey);
		game.setPuzzleConditions(Puzzle.SkullLock, Puzzle.Sextant);
		game.setPuzzleConditions(Puzzle.Bridge, Puzzle.SkullLock);
		game.setPuzzleConditions(Puzzle.SymbolsChest, Puzzle.Sextant, Puzzle.Parrot, Puzzle.SkullLock);
		game.setPuzzleConditions(Puzzle.Medallions, Puzzle.SymbolsChest);
		game.setPuzzleConditions(Puzzle.Darts, Puzzle.Medallions);
		game.setPuzzleConditions(Puzzle.Magnets, Puzzle.WallSlider, Puzzle.SkullLock);
		game.setPuzzleConditions(Puzzle.Barrels, Puzzle.Magnets, Puzzle.Darts);
		game.setRelevantObjectsForPuzzle(Puzzle.WallSlider, hashSlidables[0].gameObject, hashSlidables[1].gameObject, hashSlidables[2].gameObject, hashSlidables[3].gameObject, hashSlidables[4].gameObject, hashSlidables[5].gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.Snakes, snakes[0].Get<Item>(0).gameObject, snakes[1].Get<Item>(0).gameObject, snakes[2].Get<Item>(0).gameObject, snakes[3].Get<Item>(0).gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.SunMaze, slidableColumnTurnables[0].gameObject, slidableColumnTurnables[1].gameObject, slidableColumnTurnables[2].gameObject, slidableColumnTurnables[3].gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.SymbolsChest, plankHint, symbolsChestZoomable.gameObject, symbolKeys[0].gameObject, symbolKeys[1].gameObject, symbolKeys[2].gameObject, symbolKeys[3].gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.Medallions, treasureHint.gameObject, dartColumnTokens[0].gameObject, dartColumnTokens[1].gameObject, dartColumnTokens[2].gameObject, dartColumnTokens[3].gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.Darts, dartOriginalParents[0].gameObject, dartOriginalParents[1].gameObject, dartOriginalParents[2].gameObject, dartOriginalParents[3].gameObject, dartColumnTurnables[0].gameObject, dartColumnTurnables[1].gameObject, dartColumnTurnables[2].gameObject, dartColumnTurnables[3].gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.Magnets, magnets[0].gameObject, magnets[1].gameObject, crowbar.Get<GameObject>(0), magnetSlotKey.gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.Barrels, gunpowderStartFlameSwitch.gameObject, gunpowderPouch.gameObject);
		game.setHintCondition(Puzzle.SymbolsChest, SymbolsChestHint.FindHint, () => game.wasAddedToInventoryDuringCurrentPuzzle(plankHint) || game.wasLookedAtCurrentPuzzle(plankHint));
		game.setHintCondition(Puzzle.SymbolsChest, SymbolsChestHint.GetSextantSymbol, () => game.wasAddedToInventoryDuringCurrentPuzzle(symbolKeys[0].gameObject));
		game.setHintCondition(Puzzle.SymbolsChest, SymbolsChestHint.GetChestSymbols, () => game.wasAddedToInventoryDuringCurrentPuzzle(symbolKeys[1].gameObject) && game.wasAddedToInventoryDuringCurrentPuzzle(symbolKeys[2].gameObject));
		game.setHintCondition(Puzzle.SymbolsChest, SymbolsChestHint.GetBridgeSymbol, () => game.wasAddedToInventoryDuringCurrentPuzzle(symbolKeys[3].gameObject));
		game.setHintCondition(Puzzle.Medallions, MedallionsHint.PickUpJournal, () => game.wasAddedToInventoryDuringCurrentPuzzle(treasureHint.gameObject));
		game.setHintCondition(Puzzle.Medallions, MedallionsHint.GetCoin1, () => game.wasAddedToInventoryDuringCurrentPuzzle(dartColumnTokens[1].gameObject));
		game.setHintCondition(Puzzle.Medallions, MedallionsHint.GetCoin2, () => game.wasAddedToInventoryDuringCurrentPuzzle(dartColumnTokens[0].gameObject));
		game.setHintCondition(Puzzle.Medallions, MedallionsHint.GetCoin3, () => game.wasAddedToInventoryDuringCurrentPuzzle(dartColumnTokens[2].gameObject));
		game.setHintCondition(Puzzle.Medallions, MedallionsHint.GetCoin4, () => game.wasAddedToInventoryDuringCurrentPuzzle(dartColumnTokens[3].gameObject));
		game.setHintCondition(Puzzle.Magnets, MagnetsHint.PickUpWallSliderMagnet, () => game.wasAddedToInventoryDuringCurrentPuzzle(magnets[0].gameObject));
		game.setHintCondition(Puzzle.Magnets, MagnetsHint.CrowbarOpen, () => game.wasAddedToInventoryDuringCurrentPuzzle(magnets[1].gameObject));
		game.setHintCondition(Puzzle.Magnets, MagnetsHint.PickUpKey, () => game.wasAddedToInventoryDuringCurrentPuzzle(magnetSlotKey.gameObject));
	}

	public virtual void save(FastBinaryWriter writer)
	{
		writer.Write(in hashSolvedCount, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in hash3Done, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in slidableColumnAcceptDistance, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in slider1OnTarget, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in slider2OnTarget, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in sextantCurrentZAngle, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in sextantCurrentXAngle, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in isFirstBridgeGemPlaced, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in isSecondBridgeGemPlaced, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in symbolsCurrentIndex, default(FastBinaryWriter.ForPrimitives));
		writer.WriteArray(symbolsCurrentCode, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteArray(symbolsCodeAngles, delegate(FastBinaryWriter w, float e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteList(safePoints, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.Write(in dartColumnsRaised, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in dartsArrived, default(FastBinaryWriter.ForPrimitives));
		writer.WriteList(dartTargets, delegate(FastBinaryWriter w, Collider e)
		{
			w.WriteComponent(e);
		});
		writer.Write(in areDartsReset, default(FastBinaryWriter.ForPrimitives));
		writer.WriteList(trailGunpowderStates, delegate(FastBinaryWriter w, bool e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteList(trailGunpowderFireFromStarts, delegate(FastBinaryWriter w, bool e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.Write(in gunpowderDropTimer, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in currentGunpowderDroplet, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in nextGunpowderFloorPowder, default(FastBinaryWriter.ForPrimitives));
		writer.WriteVector3(in gunpowderStartingLocalPos);
		writer.WriteList(trailGunpowderOnFire, delegate(FastBinaryWriter w, TrailGunpowder e)
		{
			w.WriteTrailGunpowder(e);
		});
		writer.Write(in gunpowderAnimActive, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in currentGunpowderIndex, default(FastBinaryWriter.ForPrimitives));
		writer.WriteVector2(in sextantTargetRot);
		writer.Write(in fishingHookThrowMultiplier, default(FastBinaryWriter.ForPrimitives));
		writer.WriteList(fishingCatchesLeft, delegate(FastBinaryWriter w, FishingCatchCollection e)
		{
			w.WriteFishingCatchCollection(e);
		});
		writer.WriteComponent(fishingCatchOnHook);
		writer.WriteVector3(in throwingHookScale);
		int value = (int)currentWaterType;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in fishingAnimActive, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in collectedFishingKey, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in firstFrame, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in magnetOpen, default(FastBinaryWriter.ForPrimitives));
		writer.WriteComponent(redGemOpen);
		writer.WriteComponent(blueGemOpen);
		writer.WriteGameObject(centerPathBlocker1);
		writer.WriteGameObject(centerPathBlocker2);
		writer.Write(in magnetsSolved, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void load(FastBinaryReader reader)
	{
		hashSolvedCount = reader.ReadInt32();
		hash3Done = reader.ReadBoolean();
		slidableColumnAcceptDistance = reader.ReadSingle();
		slider1OnTarget = reader.ReadBoolean();
		slider2OnTarget = reader.ReadBoolean();
		sextantCurrentZAngle = reader.ReadSingle();
		sextantCurrentXAngle = reader.ReadSingle();
		isFirstBridgeGemPlaced = reader.ReadBoolean();
		isSecondBridgeGemPlaced = reader.ReadBoolean();
		symbolsCurrentIndex = reader.ReadInt32();
		symbolsCurrentCode = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		symbolsCodeAngles = reader.ReadArray((FastBinaryReader r) => r.ReadSingle());
		safePoints = reader.ReadList((FastBinaryReader r) => r.ReadInt32());
		dartColumnsRaised = reader.ReadBoolean();
		dartsArrived = reader.ReadInt32();
		dartTargets = reader.ReadList((FastBinaryReader r) => r.ReadComponent<Collider>());
		areDartsReset = reader.ReadBoolean();
		trailGunpowderStates = reader.ReadList((FastBinaryReader r) => r.ReadBoolean());
		trailGunpowderFireFromStarts = reader.ReadList((FastBinaryReader r) => r.ReadBoolean());
		gunpowderDropTimer = reader.ReadSingle();
		currentGunpowderDroplet = reader.ReadInt32();
		nextGunpowderFloorPowder = reader.ReadInt32();
		gunpowderStartingLocalPos = reader.ReadVector3();
		trailGunpowderOnFire = reader.ReadList((FastBinaryReader r) => r.ReadTrailGunpowder());
		gunpowderAnimActive = reader.ReadBoolean();
		currentGunpowderIndex = reader.ReadInt32();
		sextantTargetRot = reader.ReadVector2();
		fishingHookThrowMultiplier = reader.ReadSingle();
		fishingCatchesLeft = reader.ReadList((FastBinaryReader r) => r.ReadFishingCatchCollection());
		fishingCatchOnHook = reader.ReadComponent<Item>();
		throwingHookScale = reader.ReadVector3();
		currentWaterType = (FishingWaterCatchType)reader.ReadInt32();
		fishingAnimActive = reader.ReadBoolean();
		collectedFishingKey = reader.ReadBoolean();
		firstFrame = reader.ReadBoolean();
		magnetOpen = reader.ReadBoolean();
		redGemOpen = reader.ReadComponent<TweenState>();
		blueGemOpen = reader.ReadComponent<TweenState>();
		centerPathBlocker1 = reader.ReadGameObject();
		centerPathBlocker2 = reader.ReadGameObject();
		magnetsSolved = reader.ReadBoolean();
	}

	public virtual void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		int position = reader.Position;
		int num = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "hashSolvedCount",
			fieldValue = $"{num}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "hash3Done",
			fieldValue = $"{flag}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num2 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "slidableColumnAcceptDistance",
			fieldValue = $"{num2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag2 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "slider1OnTarget",
			fieldValue = $"{flag2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag3 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "slider2OnTarget",
			fieldValue = $"{flag3}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num3 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "sextantCurrentZAngle",
			fieldValue = $"{num3}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num4 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "sextantCurrentXAngle",
			fieldValue = $"{num4}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag4 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "isFirstBridgeGemPlaced",
			fieldValue = $"{flag4}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag5 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "isSecondBridgeGemPlaced",
			fieldValue = $"{flag5}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num5 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "symbolsCurrentIndex",
			fieldValue = $"{num5}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int[] array = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "symbolsCurrentCode[" + ((array == null) ? string.Empty : array.Length.ToString()) + "]",
			fieldValue = (((array == null) ? "null" : string.Join(", ", array)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float[] array2 = reader.ReadArray((FastBinaryReader r) => r.ReadSingle());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "symbolsCodeAngles[" + ((array2 == null) ? string.Empty : array2.Length.ToString()) + "]",
			fieldValue = (((array2 == null) ? "null" : string.Join(", ", array2)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<int> list = reader.ReadList((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "safePoints[" + ((list == null) ? string.Empty : list.Count.ToString()) + "]",
			fieldValue = (((list == null) ? "null" : string.Join(", ", list)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag6 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "dartColumnsRaised",
			fieldValue = $"{flag6}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num6 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "dartsArrived",
			fieldValue = $"{num6}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<Collider> list2 = reader.ReadList((FastBinaryReader r) => r.ReadComponent<Collider>());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "dartTargets[" + ((list2 == null) ? string.Empty : list2.Count.ToString()) + "]",
			fieldValue = (((list2 == null) ? "null" : string.Join(", ", list2)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag7 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "areDartsReset",
			fieldValue = $"{flag7}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<bool> list3 = reader.ReadList((FastBinaryReader r) => r.ReadBoolean());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "trailGunpowderStates[" + ((list3 == null) ? string.Empty : list3.Count.ToString()) + "]",
			fieldValue = (((list3 == null) ? "null" : string.Join(", ", list3)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<bool> list4 = reader.ReadList((FastBinaryReader r) => r.ReadBoolean());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "trailGunpowderFireFromStarts[" + ((list4 == null) ? string.Empty : list4.Count.ToString()) + "]",
			fieldValue = (((list4 == null) ? "null" : string.Join(", ", list4)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num7 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "gunpowderDropTimer",
			fieldValue = $"{num7}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num8 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "currentGunpowderDroplet",
			fieldValue = $"{num8}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num9 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "nextGunpowderFloorPowder",
			fieldValue = $"{num9}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Vector3 vector = reader.ReadVector3();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "gunpowderStartingLocalPos",
			fieldValue = $"{vector}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<TrailGunpowder> list5 = reader.ReadList((FastBinaryReader r) => r.ReadTrailGunpowder());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "trailGunpowderOnFire[" + ((list5 == null) ? string.Empty : list5.Count.ToString()) + "]",
			fieldValue = (((list5 == null) ? "null" : string.Join(", ", list5)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag8 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "gunpowderAnimActive",
			fieldValue = $"{flag8}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num10 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "currentGunpowderIndex",
			fieldValue = $"{num10}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Vector2 vector2 = reader.ReadVector2();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "sextantTargetRot",
			fieldValue = $"{vector2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num11 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "fishingHookThrowMultiplier",
			fieldValue = $"{num11}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<FishingCatchCollection> list6 = reader.ReadList((FastBinaryReader r) => r.ReadFishingCatchCollection());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "fishingCatchesLeft[" + ((list6 == null) ? string.Empty : list6.Count.ToString()) + "]",
			fieldValue = (((list6 == null) ? "null" : string.Join(", ", list6)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Item arg = reader.ReadComponent<Item>();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "fishingCatchOnHook",
			fieldValue = $"{arg}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Vector3 vector3 = reader.ReadVector3();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "throwingHookScale",
			fieldValue = $"{vector3}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		FishingWaterCatchType fishingWaterCatchType = (FishingWaterCatchType)reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "currentWaterType",
			fieldValue = $"{fishingWaterCatchType}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag9 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "fishingAnimActive",
			fieldValue = $"{flag9}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag10 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "collectedFishingKey",
			fieldValue = $"{flag10}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag11 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "firstFrame",
			fieldValue = $"{flag11}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag12 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "magnetOpen",
			fieldValue = $"{flag12}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		TweenState arg2 = reader.ReadComponent<TweenState>();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "redGemOpen",
			fieldValue = $"{arg2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		TweenState arg3 = reader.ReadComponent<TweenState>();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "blueGemOpen",
			fieldValue = $"{arg3}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		GameObject arg4 = reader.ReadGameObject();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "centerPathBlocker1",
			fieldValue = $"{arg4}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		GameObject arg5 = reader.ReadGameObject();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "centerPathBlocker2",
			fieldValue = $"{arg5}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag13 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "magnetsSolved",
			fieldValue = $"{flag13}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
	}

	public override int getPacketCount()
	{
		return Pirate3.getPacketCount();
	}

	public override Packet getPacket(byte id)
	{
		return Pirate3.getPacket(id);
	}

	public override Timer getTimer(byte id)
	{
		return id switch
		{
			0 => new DartArrivedTransition(), 
			1 => new GraphInitTimer(), 
			2 => new CutsceneMiddleTimer(), 
			3 => new GetCrowbarBackTimer(), 
			4 => new SetSymbolsCodeTransition(), 
			5 => new ResetSymbolsSliderTransition(), 
			6 => new FishingRodFlyInTransition(), 
			7 => new HashSliderDoneTransition(), 
			8 => new FishingRodFlyBackTimer(), 
			9 => new GunpowderFlyInTransition(), 
			10 => new GunpowderFlyBackTimer(), 
			11 => new MagnetsDoneTransition(), 
			_ => null, 
		};
	}
}
