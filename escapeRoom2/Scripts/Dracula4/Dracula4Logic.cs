using System;
using System.Collections.Generic;
using System.Text;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

public class Dracula4Logic : LevelLogic, ISaveable
{
	private enum HandAnimation
	{
		Pointing = 0,
		Give = 1,
		HoldingOrb = 2,
		HoldingKey = 3,
		ReleaseKey = 4
	}

	[Serializable]
	public class VampireObject
	{
		[DontSave]
		public GameObject worldObject;

		[DontSave]
		public MaterialState highlightState;

		[DontSave]
		public Texture eyeLetter;
	}

	private enum TileSymbol
	{
		None = 0,
		Helmet = 1,
		Man = 2,
		Vampire = 3,
		Moon = 4,
		Sun = 5,
		Stars = 6,
		Apple = 7,
		Tree = 8,
		Key = 9
	}

	public sealed class PossessionCutsceneTimer : Timer
	{
		public int phase;

		public override byte getTypeId()
		{
			return 0;
		}

		public PossessionCutsceneTimer()
		{
		}

		public PossessionCutsceneTimer(int phase)
		{
			this.phase = phase;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in phase, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			phase = reader.ReadInt32();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("phase: " + $"{phase}");
			return stringBuilder.ToString();
		}
	}

	public sealed class CoffinLidTimer : Timer
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

	public sealed class CoffinStakesOffTimer : Timer
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

	public sealed class ReleaseHandKeyTimer : Timer
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

	public sealed class ReleaseEyeKeyLockTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 4;
		}

		public ReleaseEyeKeyLockTimer()
		{
		}

		public ReleaseEyeKeyLockTimer(int index)
		{
			this.index = index;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in index, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			index = reader.ReadInt32();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("index: " + $"{index}");
			return stringBuilder.ToString();
		}
	}

	public sealed class UnlockGateTimer : Timer
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

	public sealed class SunSlotTimer : Timer
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

	public sealed class MoonSlotTimer : Timer
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

	public sealed class StartCoffinSwingTimer : Timer
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

	public sealed class CoffinFallTimer : Timer
	{
		public override byte getTypeId()
		{
			return 9;
		}

		public override void writeData(FastBinaryWriter writer)
		{
		}

		public override void readData(FastBinaryReader reader)
		{
		}
	}

	public sealed class SolveMisspellingTimer : Timer
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

	public sealed class SolveTileGraphTimer : Timer
	{
		public int tileGraphIndex;

		public override byte getTypeId()
		{
			return 11;
		}

		public SolveTileGraphTimer()
		{
		}

		public SolveTileGraphTimer(int tileGraphIndex)
		{
			this.tileGraphIndex = tileGraphIndex;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in tileGraphIndex, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			tileGraphIndex = reader.ReadInt32();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("tileGraphIndex: " + $"{tileGraphIndex}");
			return stringBuilder.ToString();
		}
	}

	public sealed class TileCutSceneTimer : Timer
	{
		public int tileGraphIndex;

		public Vector3 startCameraPosition;

		public Quaternion startCameraRotation;

		public Vector3 finalCameraPosition;

		public Quaternion finalCameraRotation;

		public override byte getTypeId()
		{
			return 12;
		}

		public TileCutSceneTimer()
		{
		}

		public TileCutSceneTimer(int tileGraphIndex, Vector3 startCameraPosition, Quaternion startCameraRotation, Vector3 finalCameraPosition, Quaternion finalCameraRotation)
		{
			this.tileGraphIndex = tileGraphIndex;
			this.startCameraPosition = startCameraPosition;
			this.startCameraRotation = startCameraRotation;
			this.finalCameraPosition = finalCameraPosition;
			this.finalCameraRotation = finalCameraRotation;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in tileGraphIndex, default(FastBinaryWriter.ForPrimitives));
			writer.WriteVector3(in startCameraPosition);
			writer.WriteQuaternion(in startCameraRotation);
			writer.WriteVector3(in finalCameraPosition);
			writer.WriteQuaternion(in finalCameraRotation);
		}

		public override void readData(FastBinaryReader reader)
		{
			tileGraphIndex = reader.ReadInt32();
			startCameraPosition = reader.ReadVector3();
			startCameraRotation = reader.ReadQuaternion();
			finalCameraPosition = reader.ReadVector3();
			finalCameraRotation = reader.ReadQuaternion();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("tileGraphIndex: " + $"{tileGraphIndex}");
			stringBuilder.AppendLine("startCameraPosition: " + $"{startCameraPosition}");
			stringBuilder.AppendLine("startCameraRotation: " + $"{startCameraRotation}");
			stringBuilder.AppendLine("finalCameraPosition: " + $"{finalCameraPosition}");
			stringBuilder.Append("finalCameraRotation: " + $"{finalCameraRotation}");
			return stringBuilder.ToString();
		}
	}

	public sealed class OpenReceiverDoorTimer : Timer
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

	public sealed class BellsIncorrectTimer : Timer
	{
		public int bellOrder;

		public override byte getTypeId()
		{
			return 14;
		}

		public BellsIncorrectTimer()
		{
		}

		public BellsIncorrectTimer(int bellOrder)
		{
			this.bellOrder = bellOrder;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in bellOrder, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			bellOrder = reader.ReadInt32();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("bellOrder: " + $"{bellOrder}");
			return stringBuilder.ToString();
		}
	}

	public sealed class BellsSolvedTimer : Timer
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

	public sealed class BellsTargetableTimer : Timer
	{
		public override byte getTypeId()
		{
			return 16;
		}

		public override void writeData(FastBinaryWriter writer)
		{
		}

		public override void readData(FastBinaryReader reader)
		{
		}
	}

	public sealed class ResetUpsideDownProgressTimer : Timer
	{
		public override byte getTypeId()
		{
			return 17;
		}

		public override void writeData(FastBinaryWriter writer)
		{
		}

		public override void readData(FastBinaryReader reader)
		{
		}
	}

	public sealed class SolveSkullsTimer : Timer
	{
		public override byte getTypeId()
		{
			return 18;
		}

		public override void writeData(FastBinaryWriter writer)
		{
		}

		public override void readData(FastBinaryReader reader)
		{
		}
	}

	public sealed class ReceiverSound1Timer : Timer
	{
		public int instanceIndex;

		public override byte getTypeId()
		{
			return 19;
		}

		public ReceiverSound1Timer()
		{
		}

		public ReceiverSound1Timer(int instanceIndex)
		{
			this.instanceIndex = instanceIndex;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in instanceIndex, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			instanceIndex = reader.ReadInt32();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("instanceIndex: " + $"{instanceIndex}");
			return stringBuilder.ToString();
		}
	}

	public sealed class ReceiverSound2Timer : Timer
	{
		public int instanceIndex;

		public override byte getTypeId()
		{
			return 20;
		}

		public ReceiverSound2Timer()
		{
		}

		public ReceiverSound2Timer(int instanceIndex)
		{
			this.instanceIndex = instanceIndex;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in instanceIndex, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			instanceIndex = reader.ReadInt32();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("instanceIndex: " + $"{instanceIndex}");
			return stringBuilder.ToString();
		}
	}

	public sealed class UvLockBoxTimer : Timer
	{
		public int currentPieceIndex;

		public override byte getTypeId()
		{
			return 21;
		}

		public UvLockBoxTimer()
		{
		}

		public UvLockBoxTimer(int currentPieceIndex)
		{
			this.currentPieceIndex = currentPieceIndex;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in currentPieceIndex, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			currentPieceIndex = reader.ReadInt32();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("currentPieceIndex: " + $"{currentPieceIndex}");
			return stringBuilder.ToString();
		}
	}

	private enum Puzzle
	{
		[PuzzleInfo("%PuzzleD4_1%", true)]
		Hanging = 0,
		[PuzzleInfo("%PuzzleD4_2%", false)]
		Eternus = 1,
		[PuzzleInfo("%PuzzleD4_3%", false)]
		Spheres = 2,
		[PuzzleInfo("%PuzzleD4_4%", false)]
		MapBox = 3,
		[PuzzleInfo("%PuzzleD4_5%", false)]
		OpenCage = 4,
		[PuzzleInfo("%PuzzleD4_6%", false)]
		TilesEasy = 5,
		[PuzzleInfo("%PuzzleD4_7%", false)]
		TilesHard = 6,
		[PuzzleInfo("%PuzzleD4_8%", false)]
		OpenHallway = 7,
		[PuzzleInfo("%PuzzleD4_9%", false)]
		Vents = 8,
		[PuzzleInfo("%PuzzleD4_10%", false)]
		Beam = 9,
		[PuzzleInfo("%PuzzleD4_11%", true)]
		Knight = 10,
		[PuzzleInfo("%PuzzleD4_12%", false)]
		Bells = 11,
		[PuzzleInfo("%PuzzleD4_13%", false)]
		Stakes = 12,
		[PuzzleInfo("%PuzzleD4_14%", false)]
		Skulls = 13
	}

	private enum HangingHint
	{
		TurnDial = 0,
		LookAtSymbols = 1,
		FigureOutSymbols = 2,
		SolveFirst = 3,
		SolveOthers = 4
	}

	private enum EternusHint
	{
		LookAtLock = 0,
		MoveSlidables = 1,
		LookAtSword = 2,
		SolveSword = 3,
		SolveOthers = 4
	}

	private enum SpheresHint
	{
		LookAtHands = 0,
		HandsPointing = 1,
		GetSphere1 = 2,
		GetSphere2 = 3,
		GetSphere3 = 4,
		Solve = 5
	}

	private enum MapBoxHint
	{
		GetBox = 0,
		MoveInteractables = 1,
		LookAtTexture = 2,
		Solve = 3
	}

	private enum OpenCageHint
	{
		GetHandsKey = 0,
		GetBoxKey = 1,
		Solve = 2
	}

	private enum TilesEasyHint
	{
		GetGemAndKey = 0,
		PlaceKeyGetScroll = 1,
		PlaceGemGetEye = 2,
		LookWithEye = 3,
		MatchEyeSymbols = 4,
		MatchScrollToTiles = 5,
		FigureOutSymbols = 6,
		SolveFirstSymbols = 7,
		SolveOthers = 8
	}

	private enum TilesHardHint
	{
		GetKey = 0,
		PlaceKey = 1,
		GetScroll = 2,
		MatchScrollToTiles = 3,
		SolveFirstSymbols = 4,
		SolveOthers = 5
	}

	private enum OpenHallwayHint
	{
		GetKey = 0,
		Solve = 1
	}

	private enum VentsHint
	{
		GetInsence = 0,
		GetStakeHolder = 1,
		LookAtSmoke = 2,
		SolveFirst = 3,
		SolveOthers = 4
	}

	private enum BeamHint
	{
		MoveSlider = 0,
		LookAtBeam = 1,
		Solve = 2
	}

	private enum KnightHint
	{
		LookAtPainting = 0,
		LookAtTurnables = 1,
		MatchTurnables = 2,
		MoveSliderAndTurnable = 3
	}

	private enum BellsHint
	{
		LookAtBells = 0,
		SolveFirst = 1,
		SolveOthers = 2
	}

	private enum StakesHint
	{
		GetHolderStake = 0,
		GetBagStakeAndHint = 1,
		GetStatueStake = 2,
		Solve = 3
	}

	private enum SkullsHint
	{
		GetSharptoothSkull = 0,
		GetNoNoseSkull = 1,
		GetImpaledSkull = 2,
		GetDraculaSkull = 3,
		GetCyclopsSkull = 4,
		GetSteelSkull = 5,
		Solve = 6
	}

	private enum LevelPredicate
	{
		Hanging = 0,
		Eternus = 1,
		Spheres = 2,
		MapBox = 3,
		OpenCage = 4,
		TilesEasy = 5,
		TilesHard = 6,
		Vents = 7,
		Possession = 8,
		WallDials = 9,
		Bells = 10,
		Stakes = 11,
		Skulls = 12
	}

	[Header("Hands")]
	[DontSave]
	public List<Item> orbs;

	[DontSave]
	public List<Slot> orbSlots;

	[DontSave]
	public List<AnimationSampler> animatedHands;

	[DontSave]
	public List<Transform> handForeArms;

	[DontSave]
	public List<AnimationClip> handAnimations;

	[DontSave]
	public AnimationSampler animatedHandKey;

	private float[] animatedHandSamples = new float[3];

	private const float DistToHandOpen = 2f;

	[DontSave]
	public Animator[] handsAnimators;

	[DontSave]
	public Transform[] handsForearms;

	private HandAnimation[] handAnimationState = new HandAnimation[3];

	[DontSave]
	public StudioEventEmitter[] handEmitters;

	[DontSave]
	public StudioEventEmitter keyHandEmitter;

	[Header("Eye")]
	[DontSave]
	public Item[] eyeKeys;

	[DontSave]
	public Slot[] eyeKeySlots;

	private bool[] eyeKeySlotsUnlocked = new bool[2];

	[DontSave]
	public TweenState[] eyeKeyLockTSs;

	[DontSave]
	public Switch3D[] eyeDoors;

	[DontSave]
	public Item[] eyeLocks;

	[DontSave]
	public Item eye;

	[DontSave]
	public MeshRenderer eyeMR;

	[DontSave]
	private Material eyeMRMat;

	private bool wasSelected;

	private int eyeLastSelectedIndex = -1;

	private int eyeCurrentSelectedIndex = -1;

	private float eyeAlpha;

	[DontSave]
	public List<VampireObject> vampireObjects;

	[Header("Misspelling")]
	[DontSave]
	public List<Slidable> misspellingSlidables;

	[DontSave]
	public Zoomable misspellingZoomable;

	[DontSave]
	public Sequence misspellingLidSequence;

	[DontSave]
	public Switch3D misspellingLidSwitch;

	[DontSave]
	public EventInstance misspellingLidInstance;

	private readonly int[] misspellingSolution = new int[3] { 1, 3, 4 };

	[Header("Tiles")]
	[DontSave]
	public SlidableGraph[] tileGraphs;

	[DontSave]
	public TweenState tileGraphTweenState;

	private readonly List<TileSymbol> TilesPieceSymbols0 = new List<TileSymbol>
	{
		TileSymbol.Helmet,
		TileSymbol.Helmet,
		TileSymbol.Man,
		TileSymbol.Man,
		TileSymbol.Sun
	};

	private readonly List<TileSymbol> TilesPieceSymbols1 = new List<TileSymbol>
	{
		TileSymbol.Apple,
		TileSymbol.Key,
		TileSymbol.Man,
		TileSymbol.Moon,
		TileSymbol.Tree,
		TileSymbol.Stars,
		TileSymbol.Vampire,
		TileSymbol.Vampire,
		TileSymbol.Apple
	};

	private readonly List<TileSymbol[]> TileSolutions0 = new List<TileSymbol[]> { new TileSymbol[15]
	{
		TileSymbol.None,
		TileSymbol.None,
		TileSymbol.None,
		TileSymbol.Sun,
		TileSymbol.Helmet,
		TileSymbol.Man,
		TileSymbol.None,
		TileSymbol.None,
		TileSymbol.Helmet,
		TileSymbol.Man,
		TileSymbol.None,
		TileSymbol.None,
		TileSymbol.None,
		TileSymbol.None,
		TileSymbol.None
	} };

	private readonly List<TileSymbol[]> TileSolutions1 = new List<TileSymbol[]>
	{
		new TileSymbol[16]
		{
			TileSymbol.None,
			TileSymbol.Vampire,
			TileSymbol.Man,
			TileSymbol.Moon,
			TileSymbol.None,
			TileSymbol.Apple,
			TileSymbol.Stars,
			TileSymbol.None,
			TileSymbol.None,
			TileSymbol.Key,
			TileSymbol.None,
			TileSymbol.None,
			TileSymbol.Tree,
			TileSymbol.Apple,
			TileSymbol.None,
			TileSymbol.Vampire
		},
		new TileSymbol[16]
		{
			TileSymbol.Vampire,
			TileSymbol.Man,
			TileSymbol.None,
			TileSymbol.Moon,
			TileSymbol.None,
			TileSymbol.Apple,
			TileSymbol.Stars,
			TileSymbol.None,
			TileSymbol.None,
			TileSymbol.Key,
			TileSymbol.None,
			TileSymbol.None,
			TileSymbol.Tree,
			TileSymbol.Apple,
			TileSymbol.None,
			TileSymbol.Vampire
		},
		new TileSymbol[16]
		{
			TileSymbol.None,
			TileSymbol.None,
			TileSymbol.None,
			TileSymbol.Moon,
			TileSymbol.None,
			TileSymbol.Apple,
			TileSymbol.Stars,
			TileSymbol.Vampire,
			TileSymbol.Man,
			TileSymbol.Key,
			TileSymbol.None,
			TileSymbol.None,
			TileSymbol.Tree,
			TileSymbol.Apple,
			TileSymbol.None,
			TileSymbol.Vampire
		},
		new TileSymbol[16]
		{
			TileSymbol.None,
			TileSymbol.None,
			TileSymbol.None,
			TileSymbol.Moon,
			TileSymbol.None,
			TileSymbol.Apple,
			TileSymbol.Stars,
			TileSymbol.None,
			TileSymbol.None,
			TileSymbol.Key,
			TileSymbol.Vampire,
			TileSymbol.Man,
			TileSymbol.Tree,
			TileSymbol.Apple,
			TileSymbol.None,
			TileSymbol.Vampire
		}
	};

	[DontSave]
	public Zoomable[] tileGraphZoomables;

	private bool[] tileGraphsSolved = new bool[2];

	[DontSave]
	public TweenState coffinTS;

	[DontSave]
	public TweenState[] chainTSs;

	private bool coffinSwing;

	private float coffinCounter;

	private float coffinSwingDirection;

	[DontSave]
	public GameObject unbrokenFloor;

	[DontSave]
	public GameObject brokenFloor;

	[DontSave]
	public VisualEffect coffinDust;

	[DontSave]
	public ParticleSystem coffinDropVFX;

	[DontSave]
	public RefArray<GameObject, Transform> tileCutscenes;

	private const float CoffinSwingStartWeight = 0.55f;

	private const float CoffinSwingLength = 0.28f;

	[Header("Bells")]
	[DontSave]
	public List<Switch3D> bells;

	[DontSave]
	private List<Sequence> bellSequences;

	private readonly List<int> BellCorrectOrder = new List<int> { 1, 4, 3, 6, 2, 0, 5 };

	private List<int> bellCurrentOrder = new List<int> { -1, -1, -1, -1, -1, -1, -1 };

	private int bellCurrentIndex;

	private const float BellAnimationDuration = 1f;

	[DontSave]
	public TweenState bellsCompartmentTS;

	[Header("Wall")]
	[DontSave]
	public Transform[] wallSlidableEndNodes;

	[DontSave]
	public Slidable[] wallSlidables;

	[DontSave]
	public Transform[] wallPiecesInner;

	[DontSave]
	public Transform[] wallPiecesOuter;

	[DontSave]
	public Dial[] wallDialsOuter;

	[DontSave]
	private Quaternion[] wallPiecesInnerOGAngles;

	[DontSave]
	private Quaternion[] wallPiecesOuterOGAngles;

	private readonly float[] WallPiecesInnerLockAngles = new float[2] { 0f, 180f };

	private bool[] wallPiecesInnerLocked = new bool[2];

	private float[] wallPiecesInnerCurrentAngles = new float[2] { 90f, -45f };

	private Quaternion[] wallDialsLastRotations;

	private bool[] wallPinCorrectRotation = new bool[4];

	[DontSave]
	public TweenState wallRewardTS;

	private bool wallDialsSolved;

	[Header("Skulls")]
	[DontSave]
	public Slot[] skullSlots;

	[DontSave]
	public Item[] skulls;

	[DontSave]
	public TweenState[] skullButtonTSs;

	[Header("Incense")]
	[DontSave]
	public GameObject[] incenseVents;

	[DontSave]
	public ParticleSystem[] incenseVentsVFX;

	[DontSave]
	public Ref<ParticleSystem, Transform, GameObject> incenseSmokeVFX;

	[DontSave]
	public Ref<ParticleSystemForceField, Transform> incenseForce;

	[DontSave]
	public Item incenseObject;

	[DontSave]
	public Turnable[] incenseLTs;

	[DontSave]
	public Ref<Sequence, GameObject> incenseContainerSequence;

	[DontSave]
	public GameObject incenseContainerSound;

	private const float incenseSmokeVelocity = 25f;

	private const float incenseExitSmokeDelay = 1f;

	private readonly int[] incenseLTSolution = new int[4] { 3, 0, 1, 2 };

	[DontSave]
	public Switch3D lockboxBoxHandle;

	[DontSave]
	public Switch3D lockboxBoxTop;

	[DontSave]
	public TweenState lockboxBoxTopAnim;

	[DontSave]
	public Item lockboxItem;

	[Header("Possession")]
	[DontSave]
	public Ref<LineRenderer, MaterialState, GameObject> possessionTrail;

	[DontSave]
	public Ref<LineRenderer, GameObject> possessionLastingTrail;

	private readonly Vector3 possessionTrailYOffset = Vector3.up * 0.01f;

	[DontSave]
	public Zoomable possessionZoomable;

	[DontSave]
	public HorizontalSlider possessionFigueHS;

	[DontSave]
	public Transform possessionFigureStartPoint;

	private bool possessionMoving;

	private bool possessionMovedOnce;

	private Vector3 possessionPreviousPosition;

	[DontSave]
	public Ref<LineRenderer, MaterialState, GameObject, Transform> PossesionLine;

	[DontSave]
	public Transform possessionForceFieldEffect;

	[DontSave]
	public Transform possessionWallHitEffect;

	[DontSave]
	private EventInstance[] receiverButtonDoorInstances;

	[DontSave]
	private string[] receiverButtonDoorOneShots;

	[DontSave]
	public MaterialState possessionStartMS;

	[DontSave]
	public MaterialState possessionEndMS;

	[DontSave]
	public ParticleSystem possessionStartPS;

	[DontSave]
	public ParticleSystem possessionEndPS;

	[DontSave]
	public Ref<ParticleSystem, GameObject> possessionExplosionVFX;

	[DontSave]
	public Ref<TweenState, GameObject> possessionExplosionAnimationTween;

	[DontSave]
	public MaterialState possessionExplosionLampTween;

	[DontSave]
	public MaterialState possessionExplosionStatueTween;

	[DontSave]
	public MeshRenderer possessionExplosionLampMR;

	[DontSave]
	public MeshRenderer possessionExplosionStatueMR;

	[DontSave]
	public BoxCollider possessionExplosionStatueCol;

	[DontSave]
	public ParticleSystem possessionExplosionLampVFX;

	[DontSave]
	public GameObject possessionCutscene;

	[DontSave]
	public GameObject possessionCutsceneOutro;

	[Header("UV Lockbox")]
	[DontSave]
	public Item uvLockbox;

	[DontSave]
	public Switch3D uvLockboxSwitch;

	[DontSave]
	public Transform[] uvLockboxPieces;

	[DontSave]
	public Transform[] uvLockboxCorrectPieces;

	[DontSave]
	public HorizontalSlider uvLockboxMovingPiece;

	[DontSave]
	public Transform uvLockboxMovingPieceVisual;

	private int uvLockboxCurrentPiece;

	[DontSave]
	public TweenState uvLockboxTop;

	[DontSave]
	public MeshRenderer[] uvLockboxCircleMRs;

	public readonly Vector2 uvLockboxCircleMapX = new Vector2(0.22f, -0.215f);

	public readonly Vector2 uvLockboxCircleMapY = new Vector2(-0.22f, 0.215f);

	public readonly Vector2 uvLockboxPieceMapX = new Vector2(-0.08f, 0.08f);

	public readonly Vector2 uvLockboxPieceMapY = new Vector2(-0.08f, 0.085f);

	private bool slider1Completed;

	private bool slider2Completed;

	public MaterialState uvSlider1Glow;

	public MaterialState uvSlider2Glow;

	private bool[] lockboxPartsMoved = new bool[2];

	[Header("Altair")]
	[DontSave]
	public Slot altairGemSlot;

	[DontSave]
	public TweenState altairState;

	[DontSave]
	public Item altairGem;

	[Header("Coffin")]
	[DontSave]
	public Item draculaHead;

	[DontSave]
	public Slot[] stakeSlots;

	[DontSave]
	public List<Slot> stakeSlotsAll;

	[DontSave]
	public ParticleSystem[] stakeSlotsAllVFX;

	[DontSave]
	public GameObject coffinLid;

	[DontSave]
	public Ref<TweenState, GameObject> coffinLidState;

	[DontSave]
	public Ref<TweenState, GameObject> coffinLidStakesTween;

	[DontSave]
	public Item[] stakes;

	[DontSave]
	public GameObject stakesHint;

	[DontSave]
	public GameObject coffinRewardKey;

	[DontSave]
	public GameObject coffinRewardKeyWorld;

	[DontSave]
	public GameObject coffinRewardKeyPickup;

	[DontSave]
	public Ref<ParticleSystem, GameObject> coffinLidVFX;

	[DontSave]
	public Ref<Light, GameObject> coffinLidLight;

	[Header("UpsideDown")]
	[DontSave]
	public Dial upsideDownDial;

	[DontSave]
	public GameObject[] upsideDownHints;

	private const float FadeDelay = 1f;

	private const float FadeDuration = 1f;

	private readonly int[] upsideDownSolutions = new int[3] { 2, 6, 3 };

	private int upsideDownProgress;

	[DontSave]
	public TweenState[] upsideDownProgressTSs;

	private bool upsideDownCorrectSequence = true;

	[DontSave]
	public Item[] upsideDownTargetableOnSolved;

	[DontSave]
	public Interactive[] upsideDownInteractiveOnSolved;

	[Header("Misc")]
	[DontSave]
	public TweenState[] exitGateTSs;

	[DontSave]
	public TweenState keyGateTS;

	[DontSave]
	public GameObject keyGateObstacle;

	[DontSave]
	public Slot keySlot;

	[DontSave]
	public Slot moonSlot;

	[DontSave]
	public Item moonItem;

	[DontSave]
	public Slot sunSlot;

	[DontSave]
	public Item sunItem;

	[DontSave]
	public GameObject[] scrolls;

	[DontSave]
	public TweenState moonTS;

	[DontSave]
	public TweenState sunTS;

	[DontSave]
	public Zoomable sunZoomable;

	[DontSave]
	public Zoomable moonZoomable;

	[DontSave]
	public Ref<TweenState, GameObject> fadeOutImage;

	[DontSave]
	public GameObject eyeBeam;

	[DontSave]
	public SpawnPointToPose[] spawnPosePoints;

	public bool hangingSolved;

	private bool doSolvePossession;

	[Header("Generated Variables")]
	[DontSave]
	public Switch3D ReceiverButton;

	[DontSave]
	public Sequence ReceiverButtonHolder;

	[DontSave]
	public TweenState ReceiverDoor;

	[DontSave]
	public TweenState ReceiverLightTS;

	[DontSave]
	public GameObject exitPlane;

	[DontSave]
	public TweenState exitPlaneTS;

	[DontSave]
	public Switch3D exitPlaneSwitch;

	public override void onInitHints()
	{
		game.setPuzzleType(Puzzle.Hanging, Game.Puzzle.Type.CoopAsk);
		game.setPuzzleConditions(Puzzle.Eternus, default(Puzzle));
		game.setPuzzleConditions(Puzzle.Spheres, Puzzle.Eternus);
		game.setPuzzleConditions(Puzzle.OpenCage, Puzzle.Spheres, Puzzle.MapBox);
		game.setPuzzleConditions(Puzzle.TilesEasy, Puzzle.OpenCage);
		game.setPuzzleConditions(Puzzle.TilesHard, Puzzle.TilesEasy);
		game.setPuzzleConditions(Puzzle.OpenHallway, Puzzle.TilesHard);
		game.setPuzzleConditions(Puzzle.Vents, Puzzle.OpenHallway);
		game.setPuzzleConditions(Puzzle.Beam, Puzzle.OpenHallway);
		game.setPuzzleConditions(Puzzle.Knight, Puzzle.OpenHallway);
		game.setPuzzleConditions(Puzzle.Bells, Puzzle.OpenHallway);
		game.setPuzzleConditions(Puzzle.Stakes, Puzzle.Vents, Puzzle.Beam);
		game.setPuzzleConditions(Puzzle.Skulls, Puzzle.Knight, Puzzle.Bells, Puzzle.Stakes);
		game.setRelevantObjectsForPuzzle(Puzzle.Hanging, upsideDownDial.gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.Eternus, misspellingZoomable.gameObject, vampireObjects[8].worldObject, vampireObjects[6].worldObject, vampireObjects[7].worldObject);
		game.setRelevantObjectsForPuzzle(Puzzle.Spheres, orbs[0].gameObject, orbs[1].gameObject, orbs[2].gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.MapBox, uvLockbox.gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.OpenCage, eyeKeys[0].gameObject, eyeKeys[1].gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.TilesEasy, sunItem.gameObject, sunSlot.gameObject, altairGem.gameObject, altairGemSlot.gameObject, scrolls[0], eye.gameObject, tileGraphZoomables[0].gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.TilesHard, moonItem.gameObject, moonSlot.gameObject, scrolls[1], eye.gameObject, tileGraphZoomables[1].gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.OpenHallway, keySlot.gameObject, coffinRewardKey);
		game.setRelevantObjectsForPuzzle(Puzzle.Vents, incenseObject.gameObject, lockboxItem.gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.Beam, possessionZoomable.gameObject, possessionFigueHS.gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.Knight, wallDialsOuter[0].gameObject, wallDialsOuter[1].gameObject, wallDialsOuter[2].gameObject, wallSlidables[0].gameObject, wallSlidables[1].gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.Bells, Array.ConvertAll(bells.ToArray(), (Switch3D b) => b.gameObject));
		game.setRelevantObjectsForPuzzle(Puzzle.Stakes, lockboxItem.gameObject, stakes[0].gameObject, stakes[1].gameObject, stakes[2].gameObject, stakesHint);
		game.setRelevantObjectsForPuzzle(Puzzle.Skulls, Array.ConvertAll(skulls, (Item s) => s.gameObject));
		game.setHintCondition(Puzzle.Hanging, HangingHint.TurnDial, () => upsideDownDial.value > 0);
		game.setHintCondition(Puzzle.Hanging, HangingHint.SolveFirst, () => upsideDownSolutions[0] == upsideDownSolutions[0]);
		game.setHintCondition(Puzzle.Eternus, EternusHint.LookAtLock, () => game.wasLookedAtCurrentPuzzle(misspellingZoomable.gameObject));
		game.setHintCondition(Puzzle.Eternus, EternusHint.MoveSlidables, delegate
		{
			bool flag = false;
			foreach (Slidable misspellingSlidable in misspellingSlidables)
			{
				flag |= misspellingSlidable.value > 0f;
			}
			return flag;
		});
		game.setHintCondition(Puzzle.Eternus, EternusHint.LookAtSword, () => game.wasLookedAtCurrentPuzzle(vampireObjects[7].worldObject));
		game.setHintCondition(Puzzle.Eternus, EternusHint.SolveSword, () => misspellingSlidables[0].closestSnapPointIndex == misspellingSolution[0]);
		game.setHintCondition(Puzzle.Spheres, SpheresHint.GetSphere1, () => game.wasAddedToInventoryDuringCurrentPuzzle(orbs[0].gameObject));
		game.setHintCondition(Puzzle.Spheres, SpheresHint.GetSphere2, () => game.wasAddedToInventoryDuringCurrentPuzzle(orbs[1].gameObject));
		game.setHintCondition(Puzzle.Spheres, SpheresHint.GetSphere3, () => game.wasAddedToInventoryDuringCurrentPuzzle(orbs[2].gameObject));
		game.setHintCondition(Puzzle.MapBox, MapBoxHint.GetBox, () => game.wasAddedToInventoryDuringCurrentPuzzle(uvLockbox.gameObject));
		game.setHintCondition(Puzzle.MapBox, MapBoxHint.MoveInteractables, () => lockboxPartsMoved[0] && lockboxPartsMoved[1]);
		game.setHintCondition(Puzzle.OpenCage, OpenCageHint.GetHandsKey, () => game.wasAddedToInventoryDuringCurrentPuzzle(eyeKeys[0].gameObject));
		game.setHintCondition(Puzzle.OpenCage, OpenCageHint.GetBoxKey, () => game.wasAddedToInventoryDuringCurrentPuzzle(eyeKeys[1].gameObject));
		game.setHintCondition(Puzzle.TilesEasy, TilesEasyHint.GetGemAndKey, () => game.wasAddedToInventoryDuringCurrentPuzzle(altairGem.gameObject) && game.wasAddedToInventoryDuringCurrentPuzzle(sunItem.gameObject));
		game.setHintCondition(Puzzle.TilesEasy, TilesEasyHint.PlaceKeyGetScroll, () => sunSlot.isUnlocked && game.wasAddedToInventoryDuringCurrentPuzzle(scrolls[0]));
		game.setHintCondition(Puzzle.TilesEasy, TilesEasyHint.PlaceGemGetEye, () => altairGemSlot.isUnlocked && game.wasAddedToInventoryDuringCurrentPuzzle(eye.gameObject));
		game.setHintCondition(Puzzle.TilesEasy, TilesEasyHint.MatchScrollToTiles, () => game.wasLookedAtCurrentPuzzle(tileGraphZoomables[0].gameObject) && game.wasLookedAtCurrentPuzzle(scrolls[0]));
		game.setHintCondition(Puzzle.TilesHard, TilesHardHint.GetKey, () => game.wasAddedToInventoryDuringCurrentPuzzle(moonItem.gameObject));
		game.setHintCondition(Puzzle.TilesHard, TilesHardHint.PlaceKey, () => moonSlot.isUnlocked);
		game.setHintCondition(Puzzle.TilesHard, TilesHardHint.GetScroll, () => game.wasAddedToInventoryDuringCurrentPuzzle(scrolls[1]));
		game.setHintCondition(Puzzle.TilesHard, TilesHardHint.MatchScrollToTiles, () => game.wasLookedAtCurrentPuzzle(tileGraphZoomables[1].gameObject) && game.wasLookedAtCurrentPuzzle(scrolls[1]));
		game.setHintCondition(Puzzle.OpenHallway, OpenHallwayHint.GetKey, () => game.wasAddedToInventoryDuringCurrentPuzzle(coffinRewardKey));
		game.setHintCondition(Puzzle.Vents, VentsHint.GetInsence, () => game.wasAddedToInventoryDuringCurrentPuzzle(incenseObject.gameObject));
		game.setHintCondition(Puzzle.Vents, VentsHint.GetStakeHolder, () => game.wasAddedToInventoryDuringCurrentPuzzle(lockboxItem.gameObject));
		game.setHintCondition(Puzzle.Vents, VentsHint.SolveFirst, () => incenseLTs[3].value == incenseLTSolution[3]);
		game.setHintCondition(Puzzle.Beam, BeamHint.MoveSlider, () => possessionMovedOnce);
		game.setHintCondition(Puzzle.Knight, KnightHint.LookAtTurnables, delegate
		{
			bool flag = false;
			Dial[] array = wallDialsOuter;
			foreach (Dial dial in array)
			{
				flag |= dial.value > 0;
			}
			return flag;
		});
		game.setHintCondition(Puzzle.Bells, BellsHint.LookAtBells, () => bellCurrentOrder[0] >= 0);
		game.setHintCondition(Puzzle.Stakes, StakesHint.GetHolderStake, () => game.wasAddedToInventoryDuringCurrentPuzzle(stakes[2].gameObject));
		game.setHintCondition(Puzzle.Stakes, StakesHint.GetBagStakeAndHint, () => game.wasAddedToInventoryDuringCurrentPuzzle(stakes[1].gameObject) && game.wasAddedToInventoryDuringCurrentPuzzle(stakesHint));
		game.setHintCondition(Puzzle.Stakes, StakesHint.GetStatueStake, () => game.wasAddedToInventoryDuringCurrentPuzzle(stakes[0].gameObject));
		game.setHintCondition(Puzzle.Skulls, SkullsHint.GetSharptoothSkull, () => game.wasAddedToInventoryDuringCurrentPuzzle(skulls[3].gameObject));
		game.setHintCondition(Puzzle.Skulls, SkullsHint.GetNoNoseSkull, () => game.wasAddedToInventoryDuringCurrentPuzzle(skulls[4].gameObject));
		game.setHintCondition(Puzzle.Skulls, SkullsHint.GetImpaledSkull, () => game.wasAddedToInventoryDuringCurrentPuzzle(skulls[5].gameObject));
		game.setHintCondition(Puzzle.Skulls, SkullsHint.GetDraculaSkull, () => game.wasAddedToInventoryDuringCurrentPuzzle(skulls[2].gameObject));
		game.setHintCondition(Puzzle.Skulls, SkullsHint.GetCyclopsSkull, () => game.wasAddedToInventoryDuringCurrentPuzzle(skulls[1].gameObject));
		game.setHintCondition(Puzzle.Skulls, SkullsHint.GetSteelSkull, () => game.wasAddedToInventoryDuringCurrentPuzzle(skulls[0].gameObject));
	}

	public override void onInitPredicates()
	{
		game.registerPredicate(LevelPredicate.Hanging, () => checkHanging());
		game.registerPredicate(LevelPredicate.Eternus, () => checkEternus());
		game.registerPredicate(LevelPredicate.Spheres, () => checkOrbs());
		game.registerPredicate(LevelPredicate.MapBox, () => checkMapBox());
		game.registerPredicate(LevelPredicate.OpenCage, () => checkOpenCage());
		game.registerPredicate(LevelPredicate.TilesEasy, () => checkTiles(0));
		game.registerPredicate(LevelPredicate.TilesHard, () => checkTiles(1));
		game.registerPredicate(LevelPredicate.Vents, () => checkVents());
		game.registerPredicate(LevelPredicate.Possession, () => checkPossession());
		game.registerPredicate(LevelPredicate.WallDials, () => checkWallDials());
		game.registerPredicate(LevelPredicate.Bells, () => checkBells());
		game.registerPredicate(LevelPredicate.Stakes, () => checkStakes());
		game.registerPredicate(LevelPredicate.Skulls, () => checkSkulls());
	}

	public override void onLevelPredicateDone(int type, int doneCounter)
	{
		switch (type)
		{
		case 0:
			solveHanging();
			break;
		case 1:
			solveEternus();
			break;
		case 2:
			solveOrbs();
			break;
		case 3:
			solveMapBox();
			break;
		case 4:
			solveOpenCage();
			break;
		case 5:
			solveTiles(0);
			break;
		case 6:
			solveTiles(1);
			break;
		case 7:
			solveVents();
			break;
		case 8:
			solvePossession();
			break;
		case 9:
			solveWallDials();
			break;
		case 10:
			solveBells();
			break;
		case 11:
			solveStakes();
			break;
		case 12:
			solveSkulls();
			break;
		}
	}

	public override void onInit()
	{
		Interactive.linkInteractives(wallDialsOuter[0], wallSlidables[0]);
		Interactive.linkInteractives(wallDialsOuter[1], wallSlidables[1]);
		Interactive.linkInteractives(uvLockboxMovingPiece, uvLockboxSwitch);
		Interactive.linkInteractives(uvLockboxSwitch, uvLockboxMovingPiece);
		Interactive[] interactives = bells.ToArray();
		Interactive.linkInteractives(interactives);
		if (game.getPlayerCount() > 1)
		{
			upsideDownHints[0].SetActive(value: false);
			if (game.getPlayerCount() <= upsideDownHints.Length)
			{
				upsideDownHints[game.getPlayerCount() - 1].SetActive(value: true);
			}
			else
			{
				upsideDownHints[upsideDownHints.Length - 1].SetActive(value: true);
			}
		}
		Item[] array = upsideDownTargetableOnSolved;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = false;
		}
		interactives = upsideDownInteractiveOnSolved;
		for (int i = 0; i < interactives.Length; i++)
		{
			interactives[i].targetable = false;
		}
		eyeKeys[0].targetable = false;
		eye.targetable = false;
		Switch3D[] array2 = eyeDoors;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].targetable = false;
		}
		eyeLocks[0].targetable = false;
		eyeLocks[0].hasRigidbody = false;
		eyeLocks[1].targetable = false;
		eyeLocks[1].hasRigidbody = false;
		foreach (VampireObject vampireObject in vampireObjects)
		{
			if (vampireObject.highlightState != null)
			{
				vampireObject.highlightState.setState("Transparent");
			}
		}
		eyeMRMat = eyeMR.materials[1];
		possessionFigueHS.setLocalPosition(possessionFigureStartPoint.localPosition);
		Vector3[] positions = new Vector3[1] { possessionFigureStartPoint.position + possessionTrailYOffset };
		possessionTrail.Get<LineRenderer>(0).SetPositions(positions);
		stakes[0].targetable = false;
		skulls[3].targetable = false;
		receiverButtonDoorInstances = new EventInstance[2];
		receiverButtonDoorInstances[0] = PineFmod.createInstance("event:/Sound Effects/04 Items/Mechanical, Machines & Motors/Metal_Mechanical_Loop_08");
		PineFmod.set3DAttributes(receiverButtonDoorInstances[0], PineFmod.to3DAttributes(ReceiverButtonHolder.transform));
		receiverButtonDoorInstances[1] = PineFmod.createInstance("event:/Sound Effects/04 Items/Mechanical, Machines & Motors/Metal_Mechanical_Loop_02");
		PineFmod.set3DAttributes(receiverButtonDoorInstances[1], PineFmod.to3DAttributes(ReceiverButtonHolder.transform));
		receiverButtonDoorOneShots = new string[2];
		receiverButtonDoorOneShots[0] = "event:/Sound Effects/03 Interactable/Doors/Metal_Door_Slam_01";
		receiverButtonDoorOneShots[1] = "event:/Sound Effects/04 Items/Metal/Metal Hit 01";
		wallPiecesInnerOGAngles = new Quaternion[wallPiecesInner.Length];
		wallPiecesInnerOGAngles[0] = wallPiecesInner[0].localRotation;
		wallPiecesInnerOGAngles[1] = wallPiecesInner[1].localRotation;
		wallPiecesOuterOGAngles = new Quaternion[wallPiecesOuter.Length];
		wallPiecesOuterOGAngles[0] = wallPiecesOuter[0].localRotation;
		wallPiecesOuterOGAngles[1] = wallPiecesOuter[1].localRotation;
		wallPiecesOuterOGAngles[2] = wallPiecesOuter[2].localRotation;
		wallDialsLastRotations = new Quaternion[wallDialsOuter.Length];
		wallDialsLastRotations[0] = wallDialsOuter[0].transform.localRotation;
		wallDialsLastRotations[1] = wallDialsOuter[1].transform.localRotation;
		wallDialsLastRotations[2] = wallDialsOuter[2].transform.localRotation;
		wallPiecesInner[0].Rotate(Vector3.right, wallPiecesInnerCurrentAngles[0]);
		wallPiecesInner[1].Rotate(Vector3.right, wallPiecesInnerCurrentAngles[1]);
		wallPiecesOuter[0].Rotate(Vector3.right, -90f);
		wallPiecesOuter[1].Rotate(Vector3.right, -135f);
		wallPiecesOuter[2].Rotate(Vector3.right, 225f);
		wallSlidableEndNodes[0].localPosition = new Vector3(1.556f, wallSlidableEndNodes[0].localPosition.y, wallSlidableEndNodes[0].localPosition.z);
		wallSlidableEndNodes[1].localPosition = new Vector3(1.556f, wallSlidableEndNodes[1].localPosition.y, wallSlidableEndNodes[1].localPosition.z);
		sunItem.targetable = false;
		moonItem.targetable = false;
		skulls[1].targetable = false;
		draculaHead.targetable = false;
		bellSequences = new List<Sequence>();
		foreach (Switch3D bell in bells)
		{
			bellSequences.Add(bell.GetComponent<Sequence>());
		}
		stakes[2].targetable = false;
		incenseContainerSound.SetActive(value: false);
		misspellingLidInstance = PineFmod.createInstance("event:/Sound Effects/04 Items/Mechanical, Machines & Motors/Wood_Mechanical_Loop");
		PineFmod.set3DAttributes(misspellingLidInstance, PineFmod.to3DAttributes(misspellingLidSequence.transform));
		eyeKeys[1].targetable = false;
		uvLockboxPieces[uvLockboxCurrentPiece].localPosition = uvLockboxMovingPiece.transform.localPosition;
	}

	public override void onConvertToSinglePlayer()
	{
		for (int i = 0; i < upsideDownHints.Length; i++)
		{
			upsideDownHints[i].SetActive(i == 0);
		}
		if (!game.isHost())
		{
			game.handlePoseLeaveLocal(game.localPlayerData.characterPoseContext.pose);
		}
	}

	public override void onUpdate()
	{
		for (int i = 0; i < animatedHands.Count; i++)
		{
			if (!(orbSlots[i].insertedItem != null))
			{
				Game.GamePlayerData gamePlayerData = game.getPlayerWithItemInInventory(orbs[i].gameObject) ?? game.getPlayerWithItemInInventory(orbs[i].transform.parent.gameObject);
				Vector3 obj = ((gamePlayerData != null) ? (gamePlayerData.lastTransformPose.position + Vector3.up * 1.6f) : orbs[i].transform.position);
				Vector3 normalized = (obj - handForeArms[i].position).normalized;
				handForeArms[i].rotation = Quaternion.LookRotation(normalized) * Quaternion.Euler(-90f, 0f, 180f);
				float num = Vector3.Distance(obj, orbSlots[i].transform.position);
				ItemState itemState = game.getItemState(orbs[i]);
				bool flag = itemState == ItemState.SlotAnimation || itemState == ItemState.Slot;
				if (num <= 2f && animatedHands[i].clip != handAnimations[1] && (game.isAnyPlayerHolding(orbs[i]) || flag))
				{
					animatedHands[i].clip = handAnimations[1];
					orbSlots[i].targetable = true;
				}
				else if ((num > 2f && animatedHands[i].clip == handAnimations[1]) || (!game.isAnyPlayerHolding(orbs[i]) && !flag))
				{
					animatedHands[i].clip = handAnimations[0];
					orbSlots[i].targetable = false;
				}
				if (animatedHands[i].clip == handAnimations[1])
				{
					animatedHandSamples[i] = Mathf.Repeat(animatedHandSamples[i] + Time.deltaTime, 1f);
					animatedHands[i].unitTime = animatedHandSamples[i];
				}
			}
		}
		if (animatedHandKey.clip == handAnimations[4])
		{
			animatedHandKey.unitTime = Mathf.MoveTowards(animatedHandKey.unitTime, 1f, Time.deltaTime);
		}
		for (int j = 0; j < handsAnimators.Length; j++)
		{
			Quaternion rotation = handsForearms[j].rotation;
			if (orbSlots[j].insertedItem != null)
			{
				handAnimationState[j] = HandAnimation.HoldingOrb;
			}
			else
			{
				Game.GamePlayerData gamePlayerData2 = game.getPlayerWithItemInInventory(orbs[j].gameObject) ?? game.getPlayerWithItemInInventory(orbs[j].transform.parent.gameObject);
				Vector3 obj2 = ((gamePlayerData2 != null) ? (gamePlayerData2.lastTransformPose.position + Vector3.up * 1.6f) : orbs[j].transform.position);
				Quaternion to = Quaternion.LookRotation((obj2 - handsForearms[j].position).normalized) * Quaternion.Euler(-90f, 0f, 180f);
				if (handAnimationState[j] == HandAnimation.Give)
				{
					to = handsForearms[j].parent.rotation;
				}
				handsForearms[j].rotation = Quaternion.RotateTowards(handsForearms[j].rotation, to, 180f * Time.deltaTime);
				float num2 = Vector3.Distance(obj2, orbSlots[j].transform.position);
				ItemState itemState2 = game.getItemState(orbs[j]);
				bool flag2 = itemState2 == ItemState.SlotAnimation || itemState2 == ItemState.Slot;
				if (num2 <= 2f && handAnimationState[j] != HandAnimation.Give && (game.isAnyPlayerHolding(orbs[j]) || flag2))
				{
					handAnimationState[j] = HandAnimation.Give;
					orbSlots[j].targetable = true;
				}
				else if ((num2 > 2f && handAnimationState[j] == HandAnimation.Give) || (!game.isAnyPlayerHolding(orbs[j]) && !flag2))
				{
					handAnimationState[j] = HandAnimation.Pointing;
					orbSlots[j].targetable = false;
				}
			}
			handsAnimators[j].SetBool("Greed", handAnimationState[j] == HandAnimation.Give);
			handsAnimators[j].SetBool("HoldingOrb", handAnimationState[j] == HandAnimation.HoldingOrb);
			float num3 = Quaternion.Angle(rotation, handsForearms[j].rotation);
			if (handAnimationState[j] == HandAnimation.Give || (handAnimationState[j] == HandAnimation.Pointing && num3 > 0.5f))
			{
				PineFmod.playOrContinue(handEmitters[j]);
			}
			else
			{
				PineFmod.stop(handEmitters[j]);
			}
		}
		eyeCurrentSelectedIndex = -1;
		if (game.isSelectedInPCMode(eye.gameObject) && !game.isInTopZoom(eye.gameObject))
		{
			wasSelected = true;
			VampireObject vampireObject = null;
			foreach (VampireObject vampireObject2 in vampireObjects)
			{
				if (vampireObject == null)
				{
					Collider[] componentsInChildren = vampireObject2.worldObject.GetComponentsInChildren<Collider>();
					for (int k = 0; k < componentsInChildren.Length; k++)
					{
						if (componentsInChildren[k].Raycast(game.playerViewRay, out var hitInfo, 10f) && Game.inReach(game.playerViewRay.origin, hitInfo.point, 10f))
						{
							vampireObject = vampireObject2;
							break;
						}
					}
				}
				if (vampireObject2 == vampireObject)
				{
					eyeCurrentSelectedIndex = vampireObjects.IndexOf(vampireObject2);
					if (vampireObject2.highlightState != null)
					{
						vampireObject2.highlightState.transitionTo("Default", 10f);
					}
				}
				else if (vampireObject2.highlightState != null)
				{
					vampireObject2.highlightState.transitionTo("Faded", 10f);
				}
			}
		}
		else if (wasSelected)
		{
			wasSelected = false;
			foreach (VampireObject vampireObject3 in vampireObjects)
			{
				if (vampireObject3.highlightState != null)
				{
					vampireObject3.highlightState.transitionTo("Transparent", 10f);
				}
			}
		}
		if (eyeCurrentSelectedIndex != eyeLastSelectedIndex)
		{
			eyeAlpha = Mathf.MoveTowards(eyeAlpha, 0f, 4f * Time.deltaTime);
			if (eyeAlpha == 0f)
			{
				eyeLastSelectedIndex = eyeCurrentSelectedIndex;
				if (eyeLastSelectedIndex != -1)
				{
					eyeMRMat.SetTexture("_EmissiveColorMap", vampireObjects[eyeLastSelectedIndex].eyeLetter);
					PineFmod.playOneShotSoundAttached("event:/Sound Effects/05 Misc/Air/Short_Air_Woosh", vampireObjects[eyeLastSelectedIndex].worldObject);
				}
			}
		}
		else if (eyeLastSelectedIndex != -1)
		{
			eyeAlpha = Mathf.MoveTowards(eyeAlpha, 1f, 6f * Time.deltaTime);
		}
		eyeMRMat.SetColor("_EmissiveColor", new Color(eyeAlpha, eyeAlpha, eyeAlpha, 1f));
		if (!game.isInAnyPlayerInventory(incenseObject.gameObject))
		{
			var (num4, gameObject, num5) = findNearestVent();
			incenseSmokeVFX.Get<Transform>(0f).rotation = Quaternion.identity;
			if (num4 <= 0.75f)
			{
				Vector3 normalized2 = (gameObject.transform.position - incenseForce.Get<Transform>(0f).position).normalized;
				incenseForce.Get<ParticleSystemForceField>(0).directionX = normalized2.x * 25f;
				incenseForce.Get<ParticleSystemForceField>(0).directionY = normalized2.y * 25f;
				incenseForce.Get<ParticleSystemForceField>(0).directionZ = normalized2.z * 25f;
				if (!incenseSmokeVFX.Get<ParticleSystem>(0).isStopped)
				{
					incenseSmokeVFX.Get<ParticleSystem>(0).Stop();
				}
				if (!incenseVentsVFX[num5].isPlaying)
				{
					incenseVentsVFX[num5].Play();
					incenseVentsVFX[num5 + 4].Play();
				}
			}
			else
			{
				incenseForce.Get<ParticleSystemForceField>(0).directionX = 0f;
				incenseForce.Get<ParticleSystemForceField>(0).directionY = 25f;
				incenseForce.Get<ParticleSystemForceField>(0).directionZ = 0f;
				if (!incenseSmokeVFX.Get<ParticleSystem>(0).isPlaying)
				{
					incenseSmokeVFX.Get<ParticleSystem>(0).Play();
					ParticleSystem[] array = incenseVentsVFX;
					for (int k = 0; k < array.Length; k++)
					{
						array[k].Stop();
					}
				}
			}
		}
		if (!wallDialsSolved)
		{
			for (int l = 0; l < wallDialsOuter.Length; l++)
			{
				Vector3 vector = wallDialsLastRotations[l] * Vector3.forward;
				Vector3 to2 = wallDialsOuter[l].transform.localRotation * Vector3.forward;
				float num6 = Vector3.SignedAngle(vector, to2, Vector3.up);
				if (!(Mathf.Abs(num6) > 0f))
				{
					continue;
				}
				wallPiecesOuter[l].Rotate(Vector3.right, num6);
				wallDialsLastRotations[l] = wallDialsOuter[l].transform.localRotation;
				if (l < 2)
				{
					if (!wallPiecesInnerLocked[l])
					{
						wallPiecesInner[l].Rotate(Vector3.right, num6);
						wallPiecesInnerCurrentAngles[l] += num6;
					}
					if (Mathf.Abs(Mathf.DeltaAngle(wallPiecesInnerCurrentAngles[l], WallPiecesInnerLockAngles[l])) < 1f)
					{
						wallPinCorrectRotation[l] = true;
						Vector3 localPosition = wallSlidableEndNodes[l].localPosition;
						localPosition.x = 1.614f;
						wallSlidableEndNodes[l].localPosition = localPosition;
					}
					else
					{
						wallPinCorrectRotation[l] = false;
						Vector3 localPosition2 = wallSlidableEndNodes[l].localPosition;
						localPosition2.x = 1.556f;
						wallSlidableEndNodes[l].localPosition = localPosition2;
					}
				}
			}
		}
		if (coffinSwing)
		{
			float num7 = 0.55f + Mathf.PingPong(coffinCounter, 0.28f);
			float f = num7 - coffinTS.findStateByName("Down").weight;
			if (Mathf.Sign(f) != Mathf.Sign(coffinSwingDirection))
			{
				PineFmod.playOneShotSoundAttached("event:/Sound Effects/04 Items/Wood/Casket/Casket_Swing", coffinLid);
			}
			coffinSwingDirection = f;
			coffinTS.setWeight("Down", num7);
			coffinCounter = Mathf.Repeat(coffinCounter + Time.deltaTime * 0.15f, 0.56f);
		}
		for (int m = 0; m < uvLockboxPieces.Length; m++)
		{
			if ((!slider1Completed || m != 0) && (!slider2Completed || m != 1))
			{
				float num8 = ((m == 0) ? 0f : 0.0156f);
				float x = Mathf.Lerp(uvLockboxCircleMapX.x, uvLockboxCircleMapX.y, Mathf.InverseLerp(uvLockboxPieceMapX.x, uvLockboxPieceMapX.y, uvLockboxPieces[m].localPosition.x));
				float y = Mathf.Lerp(uvLockboxCircleMapY.x, uvLockboxCircleMapY.y, Mathf.InverseLerp(uvLockboxPieceMapY.x, uvLockboxPieceMapY.y, uvLockboxPieces[m].localPosition.y + num8));
				uvLockboxCircleMRs[m].material.SetTextureOffset("_BaseColorMap", new Vector2(x, y));
			}
		}
		(float, GameObject, int) findNearestVent()
		{
			float num9 = float.PositiveInfinity;
			GameObject item = null;
			int item2 = -1;
			for (int n = 0; n < incenseVents.Length; n++)
			{
				float num10 = Vector3.Distance(incenseForce.Get<Transform>(0f).position, incenseVents[n].transform.position);
				if (num10 < num9)
				{
					num9 = num10;
					item = incenseVents[n];
					item2 = n;
				}
			}
			return (num9, item, item2);
		}
	}

	public override void onSlot(Slot targetSlot)
	{
		int num = orbSlots.IndexOf(targetSlot);
		int num2 = stakeSlotsAll.IndexOf(targetSlot);
		if (num >= 0)
		{
			targetSlot.insertedItem.targetable = false;
		}
		else if (UnityUtils.contains(eyeKeySlots, targetSlot))
		{
			int num3 = Array.IndexOf(eyeKeySlots, targetSlot);
			eyeKeySlotsUnlocked[num3] = true;
			eyeKeyLockTSs[num3].transitionTo("Open");
			game.startTimer(new ReleaseEyeKeyLockTimer(num3), 1f);
		}
		else if (UnityUtils.contains(skullSlots, targetSlot))
		{
			int num4 = Array.IndexOf(skullSlots, targetSlot);
			skullButtonTSs[num4].transitionTo("Down");
		}
		else if (targetSlot == keySlot)
		{
			game.startTimer(new UnlockGateTimer(), 0.5f);
			keyGateObstacle.SetActive(value: false);
			game.finishPuzzle(Puzzle.OpenHallway);
		}
		else if (targetSlot == sunSlot)
		{
			sunSlot.targetable = false;
			sunSlot.insertedItem.targetable = false;
			game.startTimer(new SunSlotTimer(), 0.1f);
		}
		else if (targetSlot == moonSlot)
		{
			moonSlot.targetable = false;
			moonSlot.insertedItem.targetable = false;
			game.startTimer(new MoonSlotTimer(), 0.1f);
		}
		else if (targetSlot == altairGemSlot)
		{
			altairState.transitionTo("Open");
			eye.targetable = true;
		}
		if (num2 != -1)
		{
			stakeSlotsAllVFX[num2].Play();
		}
	}

	public override void onRemoveFromSlot(Slot targetSlot, Item item)
	{
		if (UnityUtils.contains(skullSlots, targetSlot))
		{
			int num = Array.IndexOf(skullSlots, targetSlot);
			skullButtonTSs[num].transitionTo("Default");
		}
	}

	public override void onSlidableMoved(Slidable slidable, MoveEvent moveEvent)
	{
		if (UnityUtils.contains(wallSlidables, slidable))
		{
			int num = Array.IndexOf(wallSlidables, slidable);
			wallPiecesInnerLocked[num] = slidable.value >= 0.5f && wallPinCorrectRotation[num];
		}
	}

	public override void onSwitch3D(Switch3D targetSwitch, Switch3DEvent switchEvent)
	{
		if (targetSwitch == lockboxBoxHandle && switchEvent == Switch3DEvent.Start)
		{
			lockboxBoxHandle.targetable = false;
			lockboxBoxTop.targetable = true;
			lockboxBoxTopAnim.transitionTo("Start", 2f, 1f, playSound: true, 0.5f);
		}
		else if (targetSwitch == lockboxBoxTop && switchEvent == Switch3DEvent.Start)
		{
			lockboxBoxTop.targetable = false;
			lockboxItem.targetable = true;
		}
		if (targetSwitch == ReceiverButton)
		{
			if (switchEvent == Switch3DEvent.Start)
			{
				stakes[0].targetable = true;
				skulls[3].targetable = true;
				possessionFigueHS.targetable = false;
				possessionZoomable.targetable = false;
				game.startTimer(new OpenReceiverDoorTimer(), 1f);
				PossesionLine.Get<GameObject>(0u).SetActive(value: false);
				possessionEndMS.transitionTo("Default", 2f);
				possessionStartMS.transitionTo("Default", 2f);
				possessionStartPS.Stop();
				possessionEndPS.Stop();
				game.finishPuzzle(Puzzle.Beam);
			}
			if (switchEvent == Switch3DEvent.On)
			{
				ReceiverButton.targetable = false;
			}
		}
		int num = bells.IndexOf(targetSwitch);
		if (num >= 0 && switchEvent == Switch3DEvent.Start)
		{
			bellCurrentOrder[bellCurrentIndex] = num;
			Debug.Log($"pressed: {num} at index {bellCurrentIndex}, correct: {BellCorrectOrder[bellCurrentIndex]}");
			bellSequences[num].sequenceTime = 0f;
			bellSequences[num].play(-1f, 1f);
			foreach (Switch3D bell in bells)
			{
				bell.targetable = false;
			}
			float num2 = 1f;
			if (bellCurrentOrder[bellCurrentIndex] != BellCorrectOrder[bellCurrentIndex])
			{
				int num3 = BellCorrectOrder.IndexOf(num);
				Debug.Log($"incorrect, pressed bell is at order {num3}");
				num2 *= 2f;
				game.startTimer(new BellsIncorrectTimer(num3), 1f);
				bellCurrentIndex = 0;
			}
			else
			{
				bellCurrentIndex++;
			}
			if (bellCurrentIndex < bellCurrentOrder.Count)
			{
				game.startTimer(new BellsTargetableTimer(), num2);
			}
		}
		if (targetSwitch == uvLockboxSwitch && switchEvent == Switch3DEvent.Start)
		{
			if (!lockboxPartsMoved[1])
			{
				lockboxPartsMoved[1] = true;
			}
			uvLockboxCurrentPiece = (uvLockboxCurrentPiece + 1) % uvLockboxPieces.Length;
			uvLockboxMovingPiece.targetable = false;
			game.startTimer(new UvLockBoxTimer(uvLockboxCurrentPiece), 1f / uvLockboxSwitch.transitionSpeed);
		}
		if (targetSwitch == exitPlaneSwitch && switchEvent == Switch3DEvent.Start)
		{
			game.levelCompleted();
		}
	}

	public override void onDialMoved(Dial dial, MoveEvent moveEvent)
	{
		if (moveEvent == MoveEvent.Released && dial == upsideDownDial)
		{
			upsideDownCorrectSequence &= dial.value == upsideDownSolutions[upsideDownProgress];
			upsideDownProgressTSs[upsideDownProgress++].transitionTo("Open", 2.5f);
			if (upsideDownProgress == upsideDownSolutions.Length && !upsideDownCorrectSequence)
			{
				upsideDownDial.targetable = false;
				game.startTimer(new ResetUpsideDownProgressTimer(), 0.5f);
				upsideDownProgress = 0;
				upsideDownCorrectSequence = true;
			}
		}
	}

	public override void onHorizontalSliderMoved(HorizontalSlider slider, int pointIndex, MoveEvent moveEvent)
	{
		if (slider == possessionFigueHS)
		{
			switch (moveEvent)
			{
			case MoveEvent.Moved:
				if (!possessionMoving)
				{
					possessionStart();
				}
				else
				{
					possessionMove();
				}
				break;
			case MoveEvent.Released:
				possessionEnd(pointIndex);
				break;
			}
		}
		else
		{
			if (!(slider == uvLockboxMovingPiece))
			{
				return;
			}
			uvLockboxPieces[uvLockboxCurrentPiece].localPosition = uvLockboxMovingPiece.transform.localPosition;
			if (moveEvent == MoveEvent.Released)
			{
				if (Vector3.Distance(uvLockboxPieces[0].localPosition, uvLockboxCorrectPieces[0].localPosition) < 0.01f)
				{
					uvSlider1Glow.transitionTo("Glow", 2f);
					slider1Completed = true;
				}
				if (Vector3.Distance(uvLockboxPieces[1].localPosition, uvLockboxCorrectPieces[1].localPosition) < 0.01f)
				{
					uvSlider2Glow.transitionTo("Glow", 2f);
					slider2Completed = true;
				}
				if (!lockboxPartsMoved[0])
				{
					lockboxPartsMoved[0] = true;
				}
			}
		}
	}

	public override void onAddToInventory(Item item)
	{
		if (item == incenseObject)
		{
			incenseSmokeVFX.Get<ParticleSystem>(0).Stop();
			incenseSmokeVFX.Get<GameObject>(0u).SetActive(value: false);
			ParticleSystem[] array = incenseVentsVFX;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Stop();
			}
		}
		else if (item == eye)
		{
			eyeBeam.SetActive(value: true);
		}
		else if (item.gameObject == coffinRewardKey)
		{
			coffinRewardKeyPickup.SetActive(value: true);
			coffinRewardKeyWorld.SetActive(value: false);
		}
		else if (item == draculaHead)
		{
			draculaHead.transform.localScale = Vector3.one;
		}
	}

	public override void onRemoveFromInventory(Item item)
	{
		if (item == incenseObject)
		{
			incenseSmokeVFX.Get<GameObject>(0u).SetActive(value: true);
			incenseSmokeVFX.Get<ParticleSystem>(0).Play();
		}
		else if (item == eye)
		{
			eyeBeam.SetActive(value: false);
		}
	}

	public override void onSequenceDone(Sequence sequence)
	{
		if (sequence == incenseContainerSequence.Get<Sequence>(0))
		{
			stakes[2].targetable = true;
			incenseContainerSound.SetActive(value: false);
		}
		else if (sequence == misspellingLidSequence)
		{
			PineFmod.stop(misspellingLidInstance, FMOD.Studio.STOP_MODE.IMMEDIATE);
			PineFmod.playOneShotSoundAttached("event:/Sound Effects/04 Items/Metal/Metal Pickup 01", misspellingLidSequence.gameObject);
			misspellingLidSwitch.targetable = true;
		}
	}

	public override void onTweenTransitionDone(TweenState tweenState, string state)
	{
		if (tweenState == fadeOutImage.Get<TweenState>(0))
		{
			if (state == "Black")
			{
				hangingSolved = true;
				game.handlePoseLeaveLocal(game.localPlayerData.characterPoseContext.pose);
				fadeOutImage.Get<TweenState>(0).transitionToDuration("Default", 1f, 1f, playSound: true, 1f);
			}
			else
			{
				fadeOutImage.Get<GameObject>(0f).SetActive(value: false);
			}
		}
	}

	public override void onTimerDone(Timer timer)
	{
		if (timer is ReleaseHandKeyTimer)
		{
			eyeKeys[0].targetable = true;
			eyeKeys[0].hasRigidbody = true;
			PineFmod.stop(keyHandEmitter);
			return;
		}
		if (timer is ReleaseEyeKeyLockTimer { index: var index })
		{
			eyeLocks[index].targetable = true;
			eyeLocks[index].hasRigidbody = true;
			return;
		}
		if (timer is UnlockGateTimer)
		{
			keyGateTS.transitionTo("Open", 0.5f);
			return;
		}
		if (timer is SunSlotTimer)
		{
			sunTS.transitionTo("Open");
			sunZoomable.targetable = true;
			return;
		}
		if (timer is MoonSlotTimer)
		{
			moonTS.transitionTo("Open");
			moonZoomable.targetable = true;
			return;
		}
		if (timer is StartCoffinSwingTimer)
		{
			coffinSwing = true;
			return;
		}
		if (timer is CoffinFallTimer)
		{
			unbrokenFloor.SetActive(value: false);
			brokenFloor.SetActive(value: true);
			coffinDust.Play();
			coffinDropVFX.Play();
			{
				foreach (Slot item in stakeSlotsAll)
				{
					item.targetable = true;
				}
				return;
			}
		}
		if (timer is SolveMisspellingTimer)
		{
			game.increaseZoomCounter(misspellingZoomable.gameObject);
			misspellingZoomable.targetable = false;
			misspellingLidSequence.play(-1f, misspellingLidSequence.sequenceDuration);
			PineFmod.start(misspellingLidInstance);
			return;
		}
		if (timer is SolveTileGraphTimer { tileGraphIndex: var tileGraphIndex })
		{
			solveTileGraph(tileGraphIndex);
			return;
		}
		if (timer is TileCutSceneTimer { tileGraphIndex: var tileGraphIndex2 })
		{
			tileCutscenes[tileGraphIndex2].Get<GameObject>(0).SetActive(value: false);
			return;
		}
		if (timer is OpenReceiverDoorTimer)
		{
			ReceiverDoor.transitionTo("Open");
			return;
		}
		if (timer is BellsIncorrectTimer { bellOrder: var bellOrder })
		{
			for (int i = 0; i < bellOrder; i++)
			{
				int index2 = BellCorrectOrder[i];
				bellSequences[index2].sequenceTime = 0f;
				bellSequences[index2].play(-1f, 1f);
				PineFmod.playOneShotSoundAttached(bells[index2].soundTurnOn, bells[i].gameObject);
			}
			return;
		}
		if (timer is BellsSolvedTimer)
		{
			bellsCompartmentTS.transitionToDuration("Open", 4f);
			return;
		}
		if (timer is BellsTargetableTimer)
		{
			foreach (Switch3D bell in bells)
			{
				bell.targetable = true;
			}
			return;
		}
		if (timer is ResetUpsideDownProgressTimer)
		{
			TweenState[] array = upsideDownProgressTSs;
			for (int j = 0; j < array.Length; j++)
			{
				array[j].transitionTo("Default", 2.5f);
			}
			upsideDownDial.targetable = true;
		}
		else if (timer is SolveSkullsTimer)
		{
			game.levelCompleted();
		}
		else if (timer is ReceiverSound1Timer { instanceIndex: var instanceIndex })
		{
			float duration = ((instanceIndex == 0) ? 0.75f : 0.25f);
			PineFmod.stop(receiverButtonDoorInstances[instanceIndex], FMOD.Studio.STOP_MODE.IMMEDIATE);
			PineFmod.playOneShotSoundAttached(receiverButtonDoorOneShots[instanceIndex], ReceiverButtonHolder.gameObject);
			PineFmod.start(receiverButtonDoorInstances[Mathf.Abs(instanceIndex - 1)]);
			game.startTimer(new ReceiverSound2Timer(Mathf.Abs(instanceIndex - 1)), duration);
		}
		else if (timer is ReceiverSound2Timer { instanceIndex: var instanceIndex2 })
		{
			PineFmod.stop(receiverButtonDoorInstances[instanceIndex2], FMOD.Studio.STOP_MODE.IMMEDIATE);
			PineFmod.playOneShotSoundAttached(receiverButtonDoorOneShots[instanceIndex2], ReceiverButtonHolder.gameObject);
		}
		else if (timer is CoffinLidTimer)
		{
			coffinLidLight.Get<GameObject>(0f).SetActive(value: false);
			coffinLidState.Get<GameObject>(0f).SetActive(value: false);
		}
		else if (timer is CoffinStakesOffTimer)
		{
			coffinLidStakesTween.Get<GameObject>(0f).SetActive(value: false);
		}
		else if (timer is PossessionCutsceneTimer possessionCutsceneTimer)
		{
			if (possessionCutsceneTimer.phase == -1)
			{
				possessionCutscene.SetActive(value: true);
				game.startTimer(new PossessionCutsceneTimer(possessionCutsceneTimer.phase + 1), 1.5f);
			}
			else if (possessionCutsceneTimer.phase == 0)
			{
				ReceiverLightTS.transitionTo("On", 2f);
				ReceiverButtonHolder.play(-1f, ReceiverButtonHolder.sequenceDuration, 0.6f);
				if (ReceiverButtonHolder.sequenceTime != ReceiverButtonHolder.sequenceDuration)
				{
					PineFmod.start(receiverButtonDoorInstances[0]);
					game.startTimer(new ReceiverSound1Timer(0), 0.25f);
				}
				game.startTimer(new PossessionCutsceneTimer(possessionCutsceneTimer.phase + 1), 2f);
			}
			else if (possessionCutsceneTimer.phase == 1)
			{
				possessionCutscene.SetActive(value: false);
				game.startTimer(new PossessionCutsceneTimer(possessionCutsceneTimer.phase + 2), 1f);
			}
			else if (possessionCutsceneTimer.phase == 3)
			{
				possessionCutsceneOutro.SetActive(value: true);
				possessionExplosionLampTween.transitionTo("Down");
				possessionExplosionStatueTween.transitionTo("Down");
				possessionExplosionLampVFX.Stop();
				game.startTimer(new PossessionCutsceneTimer(possessionCutsceneTimer.phase + 1), 1f);
			}
			else if (possessionCutsceneTimer.phase == 4)
			{
				possessionExplosionLampMR.enabled = false;
				possessionExplosionStatueMR.enabled = false;
				possessionExplosionVFX.Get<GameObject>(0f).SetActive(value: true);
				possessionExplosionVFX.Get<ParticleSystem>(0).Play();
				possessionExplosionAnimationTween.Get<GameObject>(0f).SetActive(value: true);
				possessionExplosionAnimationTween.Get<TweenState>(0).transitionTo("Down");
				possessionTrail.Get<GameObject>(0u).SetActive(value: false);
				possessionLastingTrail.Get<GameObject>(0f).SetActive(value: false);
				PossesionLine.Get<GameObject>(0u).SetActive(value: false);
				PineFmod.playOneShotSoundAttached("event:/Sound Effects/04 Items/Concrete & Rock/Rock_Explode", possessionCutsceneOutro);
				game.startTimer(new PossessionCutsceneTimer(possessionCutsceneTimer.phase + 1), 1f);
			}
			else if (possessionCutsceneTimer.phase == 5)
			{
				possessionCutsceneOutro.SetActive(value: false);
				possessionExplosionStatueCol.enabled = false;
			}
		}
		else if (timer is UvLockBoxTimer uvLockBoxTimer)
		{
			uvLockboxPieces[uvLockBoxTimer.currentPieceIndex].localPosition = uvLockboxMovingPiece.transform.localPosition;
			uvLockboxMovingPiece.targetable = true;
		}
	}

	public override void onTimerUpdate(Timer timer)
	{
		if (timer is CoffinLidTimer coffinLidTimer)
		{
			if (coffinLidTimer.unitTime < 0.2f)
			{
				float t = 5f * coffinLidTimer.unitTime;
				coffinLidLight.Get<Light>(0).range = Mathf.Lerp(0f, 1.2f, t);
			}
			else if (coffinLidTimer.unitTime > 0.6f && coffinLidTimer.unitTime < 0.8f)
			{
				float t2 = 5f * (coffinLidTimer.unitTime - 0.6f);
				coffinLidLight.Get<Light>(0).range = Mathf.Lerp(1.2f, 5f, t2);
			}
			else if (coffinLidTimer.unitTime > 0.8f)
			{
				float t3 = 5f * (coffinLidTimer.unitTime - 0.8f);
				coffinLidLight.Get<Light>(0).range = Mathf.Lerp(5f, 0f, t3);
			}
		}
		else if (timer is TileCutSceneTimer tileCutSceneTimer)
		{
			if (tileCutSceneTimer.unitTime < 0.2f)
			{
				float t4 = 5f * tileCutSceneTimer.unitTime;
				tileCutscenes[tileCutSceneTimer.tileGraphIndex].Get<Transform>(0f).position = Vector3.Lerp(tileCutSceneTimer.startCameraPosition, tileCutSceneTimer.finalCameraPosition, t4);
				tileCutscenes[tileCutSceneTimer.tileGraphIndex].Get<Transform>(0f).rotation = Quaternion.Lerp(tileCutSceneTimer.startCameraRotation, tileCutSceneTimer.finalCameraRotation, t4);
			}
			else if (tileCutSceneTimer.unitTime > 0.8f)
			{
				float t5 = 5f * (tileCutSceneTimer.unitTime - 0.8f);
				tileCutscenes[tileCutSceneTimer.tileGraphIndex].Get<Transform>(0f).position = Vector3.Lerp(tileCutSceneTimer.finalCameraPosition, game.headPov.position, t5);
				tileCutscenes[tileCutSceneTimer.tileGraphIndex].Get<Transform>(0f).rotation = Quaternion.Lerp(tileCutSceneTimer.finalCameraRotation, game.headPov.rotation, t5);
			}
		}
		else if (timer is UvLockBoxTimer uvLockBoxTimer)
		{
			uvLockboxMovingPiece.setLocalPosition(Vector3.Lerp(uvLockboxMovingPiece.transform.localPosition, uvLockboxPieces[uvLockBoxTimer.currentPieceIndex].localPosition, timer.unitTime));
			uvLockboxMovingPieceVisual.localRotation = Quaternion.Lerp(uvLockboxMovingPieceVisual.localRotation, uvLockboxPieces[uvLockBoxTimer.currentPieceIndex].localRotation, timer.unitTime);
		}
	}

	[DebugButton(null, Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void stopHanging()
	{
		game.handlePoseLeaveLocal(game.localPlayerData.characterPoseContext.pose);
		misspellingSlidables[0].targetable = true;
		misspellingSlidables[1].targetable = true;
		misspellingSlidables[2].targetable = true;
		misspellingZoomable.targetable = true;
		Item[] array = upsideDownTargetableOnSolved;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = true;
		}
		Interactive[] array2 = upsideDownInteractiveOnSolved;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].targetable = true;
		}
	}

	[DebugButton(null, Tint.Default, (PostClickAction)0, 0, new object[] { })]
	public void getAllOrbs()
	{
		foreach (Item orb in orbs)
		{
			game.addItemToInventory(orb.gameObject);
		}
	}

	[DebugButton(null, Tint.Default, (PostClickAction)0, 0, new object[] { })]
	public void getHandsKey()
	{
		eyeKeys[0].targetable = true;
		game.addItemToInventory(eyeKeys[0].gameObject);
	}

	[DebugButton(null, Tint.Default, (PostClickAction)0, 0, new object[] { 0, 1 })]
	public void solveTileGraph(int tileGraphIndex)
	{
		tileGraphsSolved[tileGraphIndex] = true;
		game.increaseZoomCounter(tileGraphZoomables[tileGraphIndex].gameObject);
		tileGraphZoomables[tileGraphIndex].targetable = false;
		foreach (SlidableGraphPiece pieces in tileGraphs[tileGraphIndex].piecesList)
		{
			pieces.targetable = false;
		}
		if (tileGraphIndex == 0)
		{
			moonItem.targetable = true;
			TweenState[] array = chainTSs;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].transitionTo("Down", 0.25f);
			}
			tileGraphTweenState.transitionTo("Down", 1f, 1f, playSound: true, 2f);
			coffinTS.transitionTo("Down", 0.25f, 0.55f);
			game.startTimer(new StartCoffinSwingTimer(), 2.2f);
		}
		else if (tileGraphsSolved[0] && tileGraphsSolved[1])
		{
			skulls[1].targetable = true;
			coffinSwing = false;
			coffinTS.transitionTo("Down", 0.25f);
			Debug.Log("coffin down weight:" + coffinTS.findStateByName("Down").weight);
			float duration = (0.9f - coffinTS.findStateByName("Down").weight) * 4f;
			game.startTimer(new CoffinFallTimer(), duration);
		}
		if (tileGraphIndex == 0)
		{
			game.finishPuzzle(Puzzle.TilesEasy);
		}
		else
		{
			game.finishPuzzle(Puzzle.TilesHard);
		}
	}

	private void possessionStart()
	{
		possessionMoving = true;
		if (!possessionMovedOnce)
		{
			possessionMovedOnce = true;
		}
		Vector3[] positions = new Vector3[1] { possessionFigueHS.transform.localPosition + possessionTrailYOffset };
		possessionPreviousPosition = possessionFigueHS.transform.localPosition;
		Vector3[] positions2 = new Vector3[possessionTrail.Get<LineRenderer>(0).positionCount];
		possessionLastingTrail.Get<LineRenderer>(0).positionCount = possessionTrail.Get<LineRenderer>(0).GetPositions(positions2);
		possessionLastingTrail.Get<LineRenderer>(0).SetPositions(positions2);
		possessionTrail.Get<LineRenderer>(0).positionCount = 1;
		possessionTrail.Get<LineRenderer>(0).SetPositions(positions);
		PossesionLine.Get<LineRenderer>(0).positionCount = 1;
		PossesionLine.Get<LineRenderer>(0).SetPositions(positions);
		possessionEndMS.transitionTo("Default", 2f);
		possessionStartMS.transitionTo("Default", 2f);
		possessionStartPS.Stop();
		possessionEndPS.Stop();
	}

	private void possessionMove()
	{
		if (possessionFigueHS.transform.localPosition != possessionPreviousPosition)
		{
			Vector3 vector = possessionPreviousPosition;
			Vector3 localPosition = possessionFigueHS.transform.localPosition;
			do
			{
				vector = (possessionPreviousPosition = Vector3.MoveTowards(vector, localPosition, 0.01f));
				possessionTrail.Get<LineRenderer>(0).positionCount++;
				possessionTrail.Get<LineRenderer>(0).SetPosition(possessionTrail.Get<LineRenderer>(0).positionCount - 1, vector + possessionTrailYOffset);
			}
			while (vector != localPosition);
		}
	}

	private void possessionEnd(int pointIndex)
	{
		possessionMoving = false;
		Vector3[] array = new Vector3[possessionTrail.Get<LineRenderer>(0).positionCount];
		possessionTrail.Get<LineRenderer>(0).GetPositions(array);
		Vector3[] array2 = ReduceAndSmoothLine(new List<Vector3>(array), 4, 0.1f).ToArray();
		int num = array2.Length;
		possessionTrail.Get<LineRenderer>(0).positionCount = array2.Length;
		possessionTrail.Get<LineRenderer>(0).SetPositions(array2);
		NavMeshQueryFilter filter = new NavMeshQueryFilter
		{
			agentTypeID = NavMesh.GetSettingsByIndex(2).agentTypeID,
			areaMask = 1
		};
		possessionForceFieldEffect.position = new Vector3(100f, -100f, 100f);
		possessionWallHitEffect.position = new Vector3(100f, -100f, 100f);
		bool flag = true;
		for (int i = 0; i < num - 1; i++)
		{
			array2[i + 1] += 1f * (float)i / (float)num * 0.02f * Vector3.back;
			if (NavMesh.Raycast(PossesionLine.Get<Transform>((short)0).TransformPoint(array2[i]), PossesionLine.Get<Transform>((short)0).TransformPoint(array2[i + 1]), out var hit, filter))
			{
				flag = false;
				if (hit.mask != 0)
				{
					possessionForceFieldEffect.position = hit.position;
					possessionWallHitEffect.position = new Vector3(100f, -100f, 100f);
				}
				else
				{
					Vector3 position = hit.position;
					position.y = PossesionLine.Get<Transform>((short)0).TransformPoint(array2[i + 1]).y;
					possessionWallHitEffect.position = position;
					possessionForceFieldEffect.position = new Vector3(100f, -100f, 100f);
				}
				num = i + 1;
				break;
			}
		}
		Vector3[] array3 = new Vector3[num];
		Array.Copy(array2, array3, num);
		if (pointIndex == 0 && flag)
		{
			array3[num - 1] = PossesionLine.Get<Transform>((short)0).InverseTransformPoint(possessionEndMS.transform.position);
		}
		else if (Vector3.Distance(possessionWallHitEffect.position, PossesionLine.Get<Transform>((short)0).TransformPoint(array3[num - 1])) < 1f)
		{
			array3[num - 1] = PossesionLine.Get<Transform>((short)0).InverseTransformPoint(possessionWallHitEffect.position);
		}
		PossesionLine.Get<LineRenderer>(0).positionCount = num;
		array3[0] = PossesionLine.Get<Transform>((short)0).InverseTransformPoint(possessionStartMS.transform.position);
		PossesionLine.Get<LineRenderer>(0).SetPositions(array3);
		PineFmod.playOneShotSoundAttached("event:/Sound Effects/05 Misc/Magic/Woosh_1", possessionZoomable.gameObject);
		possessionLastingTrail.Get<LineRenderer>(0).positionCount = 0;
		possessionStartPS.Play();
		possessionEndPS.Play();
		Debug.Log("point: " + pointIndex + ", canSolve: " + flag);
		if (pointIndex == 0 && flag)
		{
			doSolvePossession = true;
			return;
		}
		ReceiverLightTS.transitionTo("Default", 2f);
		ReceiverButtonHolder.play(-1f, 0f);
		if (ReceiverButtonHolder.sequenceTime != 0f)
		{
			PineFmod.start(receiverButtonDoorInstances[1]);
			game.startTimer(new ReceiverSound1Timer(1), 0.75f);
		}
		ReceiverButton.targetable = false;
		possessionFigueHS.setLocalPosition(possessionFigureStartPoint.localPosition);
	}

	public List<Vector3> ReduceAndSmoothLine(List<Vector3> points, int smoothingFactor, float minDistance)
	{
		return SmoothLine(ReducePoints(points, minDistance), smoothingFactor);
	}

	private List<Vector3> SmoothLine(List<Vector3> points, int smoothingFactor)
	{
		List<Vector3> list = new List<Vector3>();
		for (int i = 0; i < points.Count - 1; i++)
		{
			Vector3 p = ((i == 0) ? points[i] : points[i - 1]);
			Vector3 p2 = points[i];
			Vector3 p3 = points[i + 1];
			Vector3 p4 = ((i + 2 < points.Count) ? points[i + 2] : points[i + 1]);
			for (int j = 0; j < smoothingFactor; j++)
			{
				float t = (float)j / (float)smoothingFactor;
				Vector3 item = CatmullRom(p, p2, p3, p4, t);
				list.Add(item);
			}
		}
		list.Add(points[points.Count - 1]);
		return list;
	}

	private List<Vector3> ReducePoints(List<Vector3> points, float minDistance)
	{
		List<Vector3> list = new List<Vector3>();
		if (points.Count == 0)
		{
			return list;
		}
		list.Add(points[0]);
		for (int i = 1; i < points.Count - 1; i++)
		{
			if (Vector3.Distance(list[list.Count - 1], points[i]) > minDistance)
			{
				list.Add(points[i]);
			}
		}
		list.Add(points[points.Count - 1]);
		return list;
	}

	private Vector3 CatmullRom(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
	{
		float num = t * t;
		float num2 = num * t;
		return 0.5f * (2f * p1 + (-p0 + p2) * t + (2f * p0 - 5f * p1 + 4f * p2 - p3) * num + (-p0 + 3f * p1 - 3f * p2 + p3) * num2);
	}

	[DebugButton(null, Tint.Default, (PostClickAction)0, 0, new object[] { })]
	public void getAllStakes()
	{
		Item[] array = stakes;
		foreach (Item item in array)
		{
			item.targetable = true;
			game.addItemToInventory(item.gameObject);
		}
	}

	private bool checkWallDials()
	{
		bool flag = false;
		Dial[] array = wallDialsOuter;
		foreach (Dial dial in array)
		{
			flag |= game.isAnyPlayerInteracting(dial.gameObject);
		}
		Slidable[] array2 = wallSlidables;
		foreach (Slidable slidable in array2)
		{
			flag |= game.isAnyPlayerInteracting(slidable.gameObject);
		}
		bool flag2 = true;
		for (int j = 0; j < wallPiecesOuter.Length; j++)
		{
			flag2 &= Quaternion.Angle(wallPiecesOuter[j].localRotation, wallPiecesOuterOGAngles[j]) < 2f;
		}
		for (int k = 0; k < wallPiecesInner.Length; k++)
		{
			flag2 &= Quaternion.Angle(wallPiecesInner[k].localRotation, wallPiecesInnerOGAngles[k]) < 2f;
		}
		if (flag2)
		{
			return !flag;
		}
		return false;
	}

	private void solveWallDials()
	{
		wallDialsSolved = true;
		wallRewardTS.transitionTo("Open");
		Array.ForEach(wallDialsOuter, delegate(Dial x)
		{
			x.targetable = false;
		});
		Array.ForEach(wallSlidables, delegate(Slidable x)
		{
			x.targetable = false;
		});
		game.finishPuzzle(Puzzle.Knight);
	}

	private bool checkOrbs()
	{
		bool flag = true;
		foreach (Slot orbSlot in orbSlots)
		{
			flag &= orbSlot.insertedItem != null;
		}
		return flag;
	}

	private void solveOrbs()
	{
		animatedHandKey.clip = handAnimations[4];
		foreach (Slot orbSlot in orbSlots)
		{
			orbSlot.targetable = false;
		}
		foreach (Item orb in orbs)
		{
			orb.targetable = false;
		}
		PineFmod.playOrContinue(keyHandEmitter);
		game.startTimer(new ReleaseHandKeyTimer(), handAnimations[4].length);
		game.finishPuzzle(Puzzle.Spheres);
	}

	private bool checkOpenCage()
	{
		if (eyeKeySlotsUnlocked[0])
		{
			return eyeKeySlotsUnlocked[1];
		}
		return false;
	}

	private void solveOpenCage()
	{
		Switch3D[] array = eyeDoors;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = true;
		}
		altairGem.targetable = true;
		sunItem.targetable = true;
		game.finishPuzzle(Puzzle.OpenCage);
	}

	private bool checkSkulls()
	{
		return Array.TrueForAll(skullSlots, (Slot slot) => slot.isUnlocked);
	}

	private void solveSkulls()
	{
		exitGateTSs[0].transitionTo("Open", 0.5f);
		exitGateTSs[1].transitionTo("Open", 0.5f);
		exitPlane.SetActive(value: true);
		exitPlaneTS.setWeight("Scale", 1f);
		exitPlaneTS.transitionTo("Scale", 0.5f, 0f);
		Slot[] array = skullSlots;
		foreach (Slot slot in array)
		{
			slot.targetable = false;
			if (slot.insertedItem != null)
			{
				slot.insertedItem.targetable = false;
			}
		}
		game.startTimer(new SolveSkullsTimer(), 2f);
		game.finishPuzzle(Puzzle.Skulls);
	}

	private bool checkStakes()
	{
		return Array.TrueForAll(stakeSlots, (Slot slot) => slot.isUnlocked);
	}

	private void solveStakes()
	{
		coffinLid.SetActive(value: false);
		draculaHead.targetable = true;
		coffinLidState.Get<GameObject>(0f).SetActive(value: true);
		coffinLidState.Get<TweenState>(0).transitionTo("Down", 0.2f);
		coffinLidVFX.Get<GameObject>(0f).SetActive(value: true);
		coffinLidVFX.Get<ParticleSystem>(0).Play();
		coffinLidLight.Get<GameObject>(0f).SetActive(value: true);
		coffinLidLight.Get<Light>(0).range = 0f;
		game.startTimer(new CoffinLidTimer(), 5f);
		game.startTimer(new CoffinStakesOffTimer(), 4.5f);
		foreach (Slot item in stakeSlotsAll)
		{
			item.targetable = false;
		}
		Item[] array = stakes;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = false;
		}
		coffinLidStakesTween.Get<TweenState>(0).transitionTo("Down", 4f);
		game.finishPuzzle(Puzzle.Stakes);
	}

	private bool checkEternus()
	{
		bool flag = true;
		for (int i = 0; i < misspellingSlidables.Count; i++)
		{
			flag &= misspellingSlidables[i].closestSnapPointIndex == misspellingSolution[i];
		}
		return flag;
	}

	private void solveEternus()
	{
		foreach (Slidable misspellingSlidable in misspellingSlidables)
		{
			misspellingSlidable.targetable = false;
		}
		game.startTimer(new SolveMisspellingTimer(), 0.5f);
		game.finishPuzzle(Puzzle.Eternus);
	}

	private bool checkTiles(int tileGraphIndex)
	{
		SlidableGraph slidableGraph = tileGraphs[tileGraphIndex];
		List<TileSymbol[]> list = ((tileGraphIndex == 0) ? TileSolutions0 : TileSolutions1);
		List<TileSymbol> list2 = ((tileGraphIndex == 0) ? TilesPieceSymbols0 : TilesPieceSymbols1);
		foreach (SlidableGraphPiece pieces in slidableGraph.piecesList)
		{
			if (game.isAnyPlayerInteracting(pieces.gameObject))
			{
				return false;
			}
		}
		for (int i = 0; i < list.Count; i++)
		{
			bool flag = true;
			for (int j = 0; j < slidableGraph.nodes.Count; j++)
			{
				TileSymbol tileSymbol = TileSymbol.None;
				if (slidableGraph.tryGetPiece(slidableGraph.nodes[j], out var piece))
				{
					tileSymbol = list2[slidableGraph.piecesList.IndexOf(piece)];
				}
				flag &= list[i][j] == tileSymbol;
			}
			if (flag)
			{
				return true;
			}
		}
		return false;
	}

	private void solveTiles(int tileGraphIndex)
	{
		game.startTimer(new SolveTileGraphTimer(tileGraphIndex), 0.5f);
		game.startTimer(new TileCutSceneTimer(tileGraphIndex, game.getCameraPosition(), game.getCameraRotation(), tileCutscenes[tileGraphIndex].Get<Transform>(0f).position, tileCutscenes[tileGraphIndex].Get<Transform>(0f).rotation), 3f);
		tileCutscenes[tileGraphIndex].Get<GameObject>(0).SetActive(value: true);
		tileCutscenes[tileGraphIndex].Get<Transform>(0f).position = game.getCameraPosition();
		tileCutscenes[tileGraphIndex].Get<Transform>(0f).rotation = game.getCameraRotation();
	}

	private bool checkVents()
	{
		Turnable[] array = incenseLTs;
		foreach (Turnable turnable in array)
		{
			if (game.isAnyPlayerInteracting(turnable.gameObject))
			{
				return false;
			}
		}
		bool flag = true;
		for (int j = 0; j < incenseLTs.Length; j++)
		{
			flag &= incenseLTs[j].value == incenseLTSolution[j];
		}
		return flag;
	}

	private void solveVents()
	{
		incenseContainerSequence.Get<Sequence>(0).play(0f, incenseContainerSequence.Get<Sequence>(0).sequenceDuration);
		incenseContainerSound.SetActive(value: true);
		Turnable[] array = incenseLTs;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = false;
		}
		game.changeExamineRotation(incenseContainerSequence.Get<GameObject>(0f), new Vector2(0f, 0f), 0.5f);
		game.finishPuzzle(Puzzle.Vents);
	}

	private bool checkBells()
	{
		return bellCurrentIndex >= bellCurrentOrder.Count;
	}

	private void solveBells()
	{
		Debug.Log("Bells Solved!");
		foreach (Switch3D bell in bells)
		{
			bell.targetable = false;
		}
		game.startTimer(new BellsSolvedTimer(), 1f);
		game.finishPuzzle(Puzzle.Bells);
	}

	private bool checkHanging()
	{
		if (upsideDownProgress == upsideDownSolutions.Length)
		{
			return upsideDownCorrectSequence;
		}
		return false;
	}

	private void solveHanging()
	{
		fadeOutImage.Get<GameObject>(0f).SetActive(value: true);
		fadeOutImage.Get<TweenState>(0).transitionToDuration("Black", 1f, 1f, playSound: true, 0.3f);
		upsideDownDial.targetable = false;
		misspellingZoomable.targetable = true;
		misspellingSlidables[0].targetable = true;
		misspellingSlidables[1].targetable = true;
		misspellingSlidables[2].targetable = true;
		Item[] array = upsideDownTargetableOnSolved;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = true;
		}
		Interactive[] array2 = upsideDownInteractiveOnSolved;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].targetable = true;
		}
		game.screenShake(0.5f, 0.1f, 0.1f);
		SpawnPointToPose[] array3 = spawnPosePoints;
		foreach (SpawnPointToPose spawnPointToPose in array3)
		{
			if (spawnPointToPose != null)
			{
				spawnPointToPose.active = false;
			}
		}
		game.finishPuzzle(Puzzle.Hanging);
	}

	private bool checkPossession()
	{
		return doSolvePossession;
	}

	[DebugButton(null, Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void solvePossession()
	{
		ReceiverButton.targetable = true;
		possessionForceFieldEffect.position = new Vector3(100f, -100f, 100f);
		possessionWallHitEffect.position = new Vector3(100f, -100f, 100f);
		possessionFigueHS.targetable = false;
		possessionZoomable.targetable = false;
		possessionEndMS.transitionTo("Connected", 2f);
		possessionStartMS.transitionTo("Connected", 2f);
		game.startTimer(new PossessionCutsceneTimer(-1), 0.5f);
	}

	private bool checkMapBox()
	{
		if (slider1Completed && slider2Completed && uvSlider1Glow.getWeight("Glow") > 0.99f)
		{
			return uvSlider2Glow.getWeight("Glow") > 0.99f;
		}
		return false;
	}

	private void solveMapBox()
	{
		uvLockboxMovingPiece.targetable = false;
		uvLockboxSwitch.targetable = false;
		eyeKeys[1].targetable = true;
		uvLockboxTop.transitionTo("Down");
		game.changeExamineRotation(uvLockbox.gameObject, new Vector2(0f, -40f), 0.5f);
		game.finishPuzzle(Puzzle.MapBox);
	}

	[DebugButton(null, Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void giveEyeCaseKey()
	{
		game.addItemToInventory(eyeKeys[1].gameObject);
	}

	[DebugButton(null, Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void giveWallReward()
	{
		wallRewardTS.transitionTo("Open");
	}

	[DebugButton(null, Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void openHallwayDoors()
	{
		keyGateTS.GetComponentInChildren<NavMeshObstacle>().enabled = false;
		keyGateTS.gameObject.SetActive(value: false);
		skulls[1].targetable = true;
	}

	[DebugButton(null, Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void getSunAndMoon()
	{
		game.addItemToInventory(moonSlot.acceptItems[0].gameObject);
		game.addItemToInventory(sunSlot.acceptItems[0].gameObject);
	}

	[DebugButton(null, Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void getStakes()
	{
		game.addItemToInventory(stakeSlots[0].acceptItems[0].gameObject);
		game.addItemToInventory(stakeSlots[0].acceptItems[1].gameObject);
		game.addItemToInventory(stakeSlots[0].acceptItems[2].gameObject);
	}

	[DebugButton(null, Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void getAllSkulls()
	{
		Item[] array = skulls;
		foreach (Item item in array)
		{
			game.addItemToInventory(item.gameObject);
		}
	}

	[DebugButton(null, Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void getAltairGem()
	{
		game.addItemToInventory(altairGem.gameObject);
	}

	public virtual void save(FastBinaryWriter writer)
	{
		writer.WriteArray(animatedHandSamples, delegate(FastBinaryWriter w, float e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteArray(handAnimationState, delegate(FastBinaryWriter w, HandAnimation e)
		{
			int value = (int)e;
			w.Write(in value, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteArray(eyeKeySlotsUnlocked, delegate(FastBinaryWriter w, bool e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.Write(in wasSelected, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in eyeLastSelectedIndex, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in eyeCurrentSelectedIndex, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in eyeAlpha, default(FastBinaryWriter.ForPrimitives));
		writer.WriteArray(tileGraphsSolved, delegate(FastBinaryWriter w, bool e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.Write(in coffinSwing, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in coffinCounter, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in coffinSwingDirection, default(FastBinaryWriter.ForPrimitives));
		writer.WriteList(bellCurrentOrder, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.Write(in bellCurrentIndex, default(FastBinaryWriter.ForPrimitives));
		writer.WriteArray(wallPiecesInnerLocked, delegate(FastBinaryWriter w, bool e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteArray(wallPiecesInnerCurrentAngles, delegate(FastBinaryWriter w, float e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteArray(wallDialsLastRotations, delegate(FastBinaryWriter w, Quaternion e)
		{
			w.WriteQuaternion(in e);
		});
		writer.WriteArray(wallPinCorrectRotation, delegate(FastBinaryWriter w, bool e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.Write(in wallDialsSolved, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in possessionMoving, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in possessionMovedOnce, default(FastBinaryWriter.ForPrimitives));
		writer.WriteVector3(in possessionPreviousPosition);
		writer.Write(in uvLockboxCurrentPiece, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in slider1Completed, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in slider2Completed, default(FastBinaryWriter.ForPrimitives));
		writer.WriteComponent(uvSlider1Glow);
		writer.WriteComponent(uvSlider2Glow);
		writer.WriteArray(lockboxPartsMoved, delegate(FastBinaryWriter w, bool e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.Write(in upsideDownProgress, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in upsideDownCorrectSequence, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in hangingSolved, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in doSolvePossession, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void load(FastBinaryReader reader)
	{
		animatedHandSamples = reader.ReadArray((FastBinaryReader r) => r.ReadSingle());
		handAnimationState = reader.ReadArray((FastBinaryReader r) => (HandAnimation)r.ReadInt32());
		eyeKeySlotsUnlocked = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		wasSelected = reader.ReadBoolean();
		eyeLastSelectedIndex = reader.ReadInt32();
		eyeCurrentSelectedIndex = reader.ReadInt32();
		eyeAlpha = reader.ReadSingle();
		tileGraphsSolved = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		coffinSwing = reader.ReadBoolean();
		coffinCounter = reader.ReadSingle();
		coffinSwingDirection = reader.ReadSingle();
		bellCurrentOrder = reader.ReadList((FastBinaryReader r) => r.ReadInt32());
		bellCurrentIndex = reader.ReadInt32();
		wallPiecesInnerLocked = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		wallPiecesInnerCurrentAngles = reader.ReadArray((FastBinaryReader r) => r.ReadSingle());
		wallDialsLastRotations = reader.ReadArray((FastBinaryReader r) => r.ReadQuaternion());
		wallPinCorrectRotation = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		wallDialsSolved = reader.ReadBoolean();
		possessionMoving = reader.ReadBoolean();
		possessionMovedOnce = reader.ReadBoolean();
		possessionPreviousPosition = reader.ReadVector3();
		uvLockboxCurrentPiece = reader.ReadInt32();
		slider1Completed = reader.ReadBoolean();
		slider2Completed = reader.ReadBoolean();
		uvSlider1Glow = reader.ReadComponent<MaterialState>();
		uvSlider2Glow = reader.ReadComponent<MaterialState>();
		lockboxPartsMoved = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		upsideDownProgress = reader.ReadInt32();
		upsideDownCorrectSequence = reader.ReadBoolean();
		hangingSolved = reader.ReadBoolean();
		doSolvePossession = reader.ReadBoolean();
	}

	public virtual void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		int position = reader.Position;
		float[] array = reader.ReadArray((FastBinaryReader r) => r.ReadSingle());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "animatedHandSamples[" + ((array == null) ? string.Empty : array.Length.ToString()) + "]",
			fieldValue = (((array == null) ? "null" : string.Join(", ", array)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		HandAnimation[] array2 = reader.ReadArray((FastBinaryReader r) => (HandAnimation)r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "handAnimationState[" + ((array2 == null) ? string.Empty : array2.Length.ToString()) + "]",
			fieldValue = (((array2 == null) ? "null" : string.Join(", ", array2)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool[] array3 = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "eyeKeySlotsUnlocked[" + ((array3 == null) ? string.Empty : array3.Length.ToString()) + "]",
			fieldValue = (((array3 == null) ? "null" : string.Join(", ", array3)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "wasSelected",
			fieldValue = $"{flag}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "eyeLastSelectedIndex",
			fieldValue = $"{num}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num2 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "eyeCurrentSelectedIndex",
			fieldValue = $"{num2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num3 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "eyeAlpha",
			fieldValue = $"{num3}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool[] array4 = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "tileGraphsSolved[" + ((array4 == null) ? string.Empty : array4.Length.ToString()) + "]",
			fieldValue = (((array4 == null) ? "null" : string.Join(", ", array4)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag2 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "coffinSwing",
			fieldValue = $"{flag2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num4 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "coffinCounter",
			fieldValue = $"{num4}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num5 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "coffinSwingDirection",
			fieldValue = $"{num5}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<int> list = reader.ReadList((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "bellCurrentOrder[" + ((list == null) ? string.Empty : list.Count.ToString()) + "]",
			fieldValue = (((list == null) ? "null" : string.Join(", ", list)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num6 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "bellCurrentIndex",
			fieldValue = $"{num6}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool[] array5 = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "wallPiecesInnerLocked[" + ((array5 == null) ? string.Empty : array5.Length.ToString()) + "]",
			fieldValue = (((array5 == null) ? "null" : string.Join(", ", array5)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float[] array6 = reader.ReadArray((FastBinaryReader r) => r.ReadSingle());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "wallPiecesInnerCurrentAngles[" + ((array6 == null) ? string.Empty : array6.Length.ToString()) + "]",
			fieldValue = (((array6 == null) ? "null" : string.Join(", ", array6)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Quaternion[] array7 = reader.ReadArray((FastBinaryReader r) => r.ReadQuaternion());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "wallDialsLastRotations[" + ((array7 == null) ? string.Empty : array7.Length.ToString()) + "]",
			fieldValue = (((array7 == null) ? "null" : string.Join(", ", array7)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool[] array8 = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "wallPinCorrectRotation[" + ((array8 == null) ? string.Empty : array8.Length.ToString()) + "]",
			fieldValue = (((array8 == null) ? "null" : string.Join(", ", array8)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag3 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "wallDialsSolved",
			fieldValue = $"{flag3}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag4 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "possessionMoving",
			fieldValue = $"{flag4}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag5 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "possessionMovedOnce",
			fieldValue = $"{flag5}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Vector3 vector = reader.ReadVector3();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "possessionPreviousPosition",
			fieldValue = $"{vector}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num7 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "uvLockboxCurrentPiece",
			fieldValue = $"{num7}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag6 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "slider1Completed",
			fieldValue = $"{flag6}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag7 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "slider2Completed",
			fieldValue = $"{flag7}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		MaterialState arg = reader.ReadComponent<MaterialState>();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "uvSlider1Glow",
			fieldValue = $"{arg}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		MaterialState arg2 = reader.ReadComponent<MaterialState>();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "uvSlider2Glow",
			fieldValue = $"{arg2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool[] array9 = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "lockboxPartsMoved[" + ((array9 == null) ? string.Empty : array9.Length.ToString()) + "]",
			fieldValue = (((array9 == null) ? "null" : string.Join(", ", array9)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num8 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "upsideDownProgress",
			fieldValue = $"{num8}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag8 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "upsideDownCorrectSequence",
			fieldValue = $"{flag8}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag9 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "hangingSolved",
			fieldValue = $"{flag9}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag10 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "doSolvePossession",
			fieldValue = $"{flag10}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
	}

	public override Timer getTimer(byte id)
	{
		return id switch
		{
			0 => new PossessionCutsceneTimer(), 
			1 => new CoffinLidTimer(), 
			2 => new CoffinStakesOffTimer(), 
			3 => new ReleaseHandKeyTimer(), 
			4 => new ReleaseEyeKeyLockTimer(), 
			5 => new UnlockGateTimer(), 
			6 => new SunSlotTimer(), 
			7 => new MoonSlotTimer(), 
			8 => new StartCoffinSwingTimer(), 
			9 => new CoffinFallTimer(), 
			10 => new SolveMisspellingTimer(), 
			11 => new SolveTileGraphTimer(), 
			12 => new TileCutSceneTimer(), 
			13 => new OpenReceiverDoorTimer(), 
			14 => new BellsIncorrectTimer(), 
			15 => new BellsSolvedTimer(), 
			16 => new BellsTargetableTimer(), 
			17 => new ResetUpsideDownProgressTimer(), 
			18 => new SolveSkullsTimer(), 
			19 => new ReceiverSound1Timer(), 
			20 => new ReceiverSound2Timer(), 
			21 => new UvLockBoxTimer(), 
			_ => null, 
		};
	}
}
