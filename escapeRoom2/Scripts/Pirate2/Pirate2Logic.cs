using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Pirate2Logic : LevelLogic, ISaveable
{
	public class KrakenCannon
	{
		[DontSave]
		public Turnable[] turnables;

		[DontSave]
		public Slot[] slots;

		[DontSave]
		public Slot cannonSlot;

		[DontSave]
		public TweenState cannonLid;

		[DontSave]
		public GameObject cannonFirePoint;

		[DontSave]
		public ParticleSystem cannonEffect;

		[DontSave]
		public List<int> turnablesSolution;

		[DontSave]
		public Dictionary<Slot, Item[]> runeSlotSolution;

		[DontSave]
		public TweenState solvedTs;

		[DontSave]
		public Animator solvedAnim;

		[DontSave]
		public GameObject[] obstacles;

		[DontSave]
		public MaterialState[] alredyInsertedRunes;

		[DontSave]
		public ParticleSystem hitKraken;

		[DontSave]
		public TweenState[] rotateTurnablesTs;

		[DontSave]
		public Sequence fireCannonSeq;

		[DontSave]
		public GameObject cannonBallEffect;

		public bool checkSolution()
		{
			for (int i = 0; i < turnables.Length; i++)
			{
				if (turnables[i].value != turnablesSolution[i])
				{
					Debug.Log($"turnable {i} value is {turnables[i].value} but should be {turnablesSolution[i]}");
					return false;
				}
			}
			for (int j = 0; j < slots.Length; j++)
			{
				if (runeSlotSolution.ContainsKey(slots[j]))
				{
					Item[] array = runeSlotSolution[slots[j]];
					foreach (Item item in array)
					{
						Debug.Log(slots[j]?.ToString() + " WHAT IS THIS " + item);
					}
					bool flag = false;
					array = runeSlotSolution[slots[j]];
					for (int k = 0; k < array.Length; k++)
					{
						if (array[k] == slots[j].insertedItem)
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						Debug.Log($"rune slot {slots[j]} does not contain {slots[j].insertedItem}");
						return false;
					}
				}
				else if (slots[j].insertedItem != null)
				{
					Debug.Log($"non rune slot {slots[j]} contains rune but shouldnt");
					return false;
				}
			}
			return true;
		}
	}

	public class CombatDummy
	{
		[DontSave]
		public Slidable rightHandSlider;

		[DontSave]
		public Slidable leftHandSlider;

		[DontSave]
		public TweenState leftHand;

		[DontSave]
		public TweenState rightHand;

		[DontSave]
		public List<int> solution;
	}

	public enum PathLandmark
	{
		Buoys = 0,
		Storm = 1,
		North = 2,
		Glow = 3,
		Bell = 4,
		FollowGhost = 5,
		Island = 6
	}

	public sealed class BoardDissolveTimer : Timer
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

	public sealed class LanternButtonsResetTimer : Timer
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

	public sealed class ShowIslandTimer : Timer
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

	public sealed class ShakeScreenTimer : Timer
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

	public sealed class HideFinalFogTimer : Timer
	{
		public override byte getTypeId()
		{
			return 4;
		}

		public override void writeData(FastBinaryWriter writer)
		{
		}

		public override void readData(FastBinaryReader reader)
		{
		}
	}

	public sealed class FinishLevelTimer : Timer
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

	public sealed class UnlockGraffitiWheelTimer : Timer
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

	public sealed class CannonFlipFireSwitchTimer : Timer
	{
		public int cannonIndex;

		public int index;

		public override byte getTypeId()
		{
			return 7;
		}

		public CannonFlipFireSwitchTimer()
		{
		}

		public CannonFlipFireSwitchTimer(int cannonIndex, int index)
		{
			this.cannonIndex = cannonIndex;
			this.index = index;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in cannonIndex, default(FastBinaryWriter.ForPrimitives));
			writer.Write(in index, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			cannonIndex = reader.ReadInt32();
			index = reader.ReadInt32();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("cannonIndex: " + $"{cannonIndex}");
			stringBuilder.Append("index: " + $"{index}");
			return stringBuilder.ToString();
		}
	}

	public sealed class CannonThrowBallTimer : Timer
	{
		public int cannonIndex;

		public int index;

		public override byte getTypeId()
		{
			return 8;
		}

		public CannonThrowBallTimer()
		{
		}

		public CannonThrowBallTimer(int cannonIndex, int index)
		{
			this.cannonIndex = cannonIndex;
			this.index = index;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in cannonIndex, default(FastBinaryWriter.ForPrimitives));
			writer.Write(in index, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			cannonIndex = reader.ReadInt32();
			index = reader.ReadInt32();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("cannonIndex: " + $"{cannonIndex}");
			stringBuilder.Append("index: " + $"{index}");
			return stringBuilder.ToString();
		}
	}

	public sealed class CannonStartAnimTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 9;
		}

		public CannonStartAnimTimer()
		{
		}

		public CannonStartAnimTimer(int index)
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

	public sealed class KrakenHitTimer : Timer
	{
		public enum KrakenState
		{
			HitEffect = 0,
			HitEffectTurnOff = 1,
			KrakenDrownAnim = 2,
			RemoveTentacle = 3,
			KrakenRise = 4,
			KrakenPlaySequence = 5,
			Symbol = 6
		}

		public KrakenState state;

		public int index;

		public float sequenceGoal;

		public int symbol;

		public override byte getTypeId()
		{
			return 10;
		}

		public KrakenHitTimer()
		{
		}

		public KrakenHitTimer(KrakenState state, int index, float sequenceGoal, int symbol)
		{
			this.state = state;
			this.index = index;
			this.sequenceGoal = sequenceGoal;
			this.symbol = symbol;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			int value = (int)state;
			writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
			writer.Write(in index, default(FastBinaryWriter.ForPrimitives));
			writer.Write(in sequenceGoal, default(FastBinaryWriter.ForPrimitives));
			writer.Write(in symbol, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			state = (KrakenState)reader.ReadInt32();
			index = reader.ReadInt32();
			sequenceGoal = reader.ReadSingle();
			symbol = reader.ReadInt32();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("state: " + $"{state}");
			stringBuilder.AppendLine("index: " + $"{index}");
			stringBuilder.AppendLine("sequenceGoal: " + $"{sequenceGoal}");
			stringBuilder.Append("symbol: " + $"{symbol}");
			return stringBuilder.ToString();
		}
	}

	public sealed class SolveCombatBoxTimer : Timer
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

	public sealed class knotsLockDelayTimer : Timer
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

	public sealed class ropeLockDelayTimer : Timer
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

	public sealed class BarrelAnimTimer : Timer
	{
		public override byte getTypeId()
		{
			return 14;
		}

		public override void writeData(FastBinaryWriter writer)
		{
		}

		public override void readData(FastBinaryReader reader)
		{
		}
	}

	public sealed class BarrelAnim2Timer : Timer
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

	public sealed class KrakenCinematicTimer : Timer
	{
		public int cinematicIndex;

		public int index;

		public override byte getTypeId()
		{
			return 16;
		}

		public KrakenCinematicTimer()
		{
		}

		public KrakenCinematicTimer(int cinematicIndex, int index)
		{
			this.cinematicIndex = cinematicIndex;
			this.index = index;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in cinematicIndex, default(FastBinaryWriter.ForPrimitives));
			writer.Write(in index, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			cinematicIndex = reader.ReadInt32();
			index = reader.ReadInt32();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("cinematicIndex: " + $"{cinematicIndex}");
			stringBuilder.Append("index: " + $"{index}");
			return stringBuilder.ToString();
		}
	}

	public sealed class GhostShipTimer : Timer
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

	public sealed class MastCutsceneTimer : Timer
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

	private enum RPCType
	{
		LanternGood = 0,
		LanternWrong = 1,
		UnlockGraffitiWheel = 2,
		GraffitiChestGood = 3,
		GraffitiChestReset = 4,
		SolveCannon1 = 5,
		SolveCannon2 = 6,
		SolveCannon3 = 7,
		SwordBarrel = 8,
		CombatChest = 9,
		CaptainsDesk = 10
	}

	private enum LevelPredicate
	{
		GhostShip = 0,
		North = 1,
		Buoys = 2,
		Bells = 3
	}

	private enum Puzzle
	{
		[PuzzleInfo("%PuzzleP2_1%", false)]
		LireChest = 0,
		[PuzzleInfo("%PuzzleP2_2%", false)]
		RopeLinesChest = 1,
		[PuzzleInfo("%PuzzleP2_3%", false)]
		RuneKey = 2,
		[PuzzleInfo("%PuzzleP2_4%", false)]
		RuneChest = 3,
		[PuzzleInfo("%PuzzleP2_5%", false)]
		Cannon1 = 4,
		[PuzzleInfo("%PuzzleP2_6%", false)]
		SwordBarrel = 5,
		[PuzzleInfo("%PuzzleP2_7%", false)]
		FlagsChest = 6,
		[PuzzleInfo("%PuzzleP2_8%", false)]
		Cannon2 = 7,
		[PuzzleInfo("%PuzzleP2_9%", false)]
		Lanterns = 8,
		[PuzzleInfo("%PuzzleP2_10%", false)]
		Swordfighting = 9,
		[PuzzleInfo("%PuzzleP2_11%", false)]
		CaptainsTable = 10,
		[PuzzleInfo("%PuzzleP2_12%", false)]
		Cannon3 = 11,
		[PuzzleInfo("%PuzzleP2_13%", false)]
		Sailing = 12
	}

	private enum LireChestHint
	{
		LookAtChest = 0,
		LookAtGrate = 1,
		LookAtNote = 2,
		SolutionPart = 3
	}

	private enum RopeLinesChestHint
	{
		LookAtChest = 0,
		RotateRope = 1,
		CompareRope = 2,
		SolutionPart = 3
	}

	private enum RuneKeyHint
	{
		PickUpKey = 0,
		PlaceKey = 1
	}

	private enum RuneChestHint
	{
		RotateMast = 0,
		FindChest = 1,
		PushRune = 2,
		PushR = 3,
		PushU = 4,
		PushTheRest = 5
	}

	private enum Cannon1Hint
	{
		PickUpChestRunes = 0,
		PickUpKnotRune = 1,
		MoveRuneBoard = 2,
		CorrectPlacement = 3,
		RotateCannon = 4,
		PlaceRunes = 5,
		PickUpCannonBall = 6,
		PlaceCannonBall = 7
	}

	private enum SwordBarrelHint
	{
		PickUpBoxDaggers = 0,
		PickUpCrateWeapons = 1,
		PickUpBarrelWeapons = 2,
		CorrectPlacement = 3,
		PlaceLongest = 4,
		PlaceMiddle = 5,
		PlaceShortest = 6
	}

	private enum FlagsChestHint
	{
		LookAtChest = 0,
		LookAtFlags = 1,
		SolutionPart = 2
	}

	private enum Cannon2Hint
	{
		PickUpChestRunes = 0,
		PickUpBoard = 1,
		PlaceBoard = 2,
		BoardCorrectPlacement = 3,
		PlaceShield = 4,
		RotateCannonAndPlace = 5,
		RotateCannon = 6,
		PlaceRunes = 7,
		PickUpCannonBall = 8,
		PlaceCannonBall = 9
	}

	private enum LanternsHint
	{
		PickUpPaper = 0,
		LookAtLanterns = 1,
		SolutionPart = 2
	}

	private enum SwordfightingHint
	{
		PickUpNotes = 0,
		Solve1 = 1,
		Solve2 = 2,
		Solve3 = 3,
		Solve4 = 4
	}

	private enum CaptainsTableHint
	{
		PickUpSwordKey = 0,
		PickUpBoxKey = 1,
		PickUpCabinetKey = 2,
		PickUpDrawerKey = 3,
		RotateGlobe = 4,
		SolutionPart = 5
	}

	private enum Cannon3Hint
	{
		PickUpCannonBall = 0,
		PlaceCannonBall = 1
	}

	private enum SailingHint
	{
		PickUpPaper = 0,
		LowerSails = 1,
		FollowLights = 2,
		Storm = 3,
		GoNorth = 4,
		Fog = 5,
		RingTheBell = 6,
		FollowShip = 7
	}

	[DontSave]
	public float dialSpeed;

	private bool graffitiChestCollected;

	[DontSave]
	public float graffitiWheelSpeed;

	[DontSave]
	private readonly int[] GraffitiChestSolution = new int[4] { 2, 0, 1, 3 };

	private int[] graffitiChestCurrent = new int[4] { -1, -1, -1, -1 };

	private int graffitiChestIndex;

	[DontSave]
	private List<KrakenCannon> krakenCannons;

	[DontSave]
	private float[] krakenCinematicDurations = new float[4] { 8.58f, 8f, 6.3f, 1.3f };

	private List<bool> krakenCannonRunesSolved = new List<bool>();

	private bool isKrakenDown;

	[DontSave]
	public float cannonBallForce;

	private int lanternButtonCounter;

	private bool swayKraken;

	private float swayKrakenT = 0.5f;

	private Vector3 krakenPivot;

	[DontSave]
	private List<CombatDummy> combatDummies = new List<CombatDummy>();

	private List<List<int>> combatDummiesCurrentValues = new List<List<int>>();

	[DontSave]
	private readonly int[][] CombatBoxSolutions = new int[4][]
	{
		new int[2] { 2, 0 },
		new int[2],
		new int[2] { 0, 1 },
		new int[2] { 1, 1 }
	};

	[DontSave]
	public TweenState limperChestLockTween;

	[DontSave]
	public Ref<Lock, Item> limperChestLock;

	[DontSave]
	public Ref<Zoomable> limperChestLockZoom;

	[DontSave]
	public Turnable[] limperChestTurnables;

	[DontSave]
	public Ref<Sequence, Switch3D> limperChestSequence;

	[DontSave]
	public Dial wheel;

	[DontSave]
	public Switch3D sailsSwitch;

	[DontSave]
	public MaterialState[] sails;

	[DontSave]
	public ParticleSystem shipMovingEffect;

	[DontSave]
	public Ref<GameObject, Transform> compassNeedle;

	[DontSave]
	public Transform north;

	[DontSave]
	public Switch3D bell;

	[DontSave]
	public TweenState bellTs;

	[DontSave]
	public Transform landmarksPivot;

	private float landmarksPivotRotation;

	private PathLandmark currentLandmarkTarget;

	[DontSave]
	public List<PathLandmark> landmarks;

	[DontSave]
	public Ref<GameObject, Transform> ship;

	[DontSave]
	private const float LandmarksFailTimeOut = 5f;

	private float landmarksTimer;

	private bool isStormSolved;

	private bool isGlowSolved;

	private bool overrideSails;

	private bool prevSails;

	[DontSave]
	public Ref<GameObject, Transform> buoysCenter;

	[DontSave]
	public GameObject[] buoys;

	[DontSave]
	private const float BuoysAngleDelta = 15f;

	[DontSave]
	private const float BuoysTimeOut = 5f;

	[DontSave]
	public Ref<GameObject, ParticleSystem> storm;

	[DontSave]
	private const float NorthAngleDelta = 10f;

	[DontSave]
	private const float NorthTimeOut = 5f;

	[DontSave]
	public ParticleSystem seaGlow;

	[DontSave]
	private const int BellRingsTarget = 3;

	private int bellRings;

	[DontSave]
	public Ref<GameObject, Transform, TweenState> ghostShip;

	[DontSave]
	public ParticleSystem ghostShipSplash;

	[DontSave]
	public Transform ghostShipNearTarget;

	[DontSave]
	public Transform ghostShipFarTarget;

	[DontSave]
	private const float GhostShipAngleDelta = 15f;

	[DontSave]
	public Ref<GameObject, Sequence> island;

	[DontSave]
	public ParticleSystem finalFog;

	[DontSave]
	public ParticleSystem shipDriveEffect;

	private float shipTargetTilt;

	[DontSave]
	private Quaternion shipParentStartRotation;

	private float mainRopeSequenceTime = 1f;

	private bool interactedWithPuley;

	private float compasAngle = 30f;

	[Header("Generated Variables")]
	[DontSave]
	public GameObject globe;

	[DontSave]
	public GameObject travelHint;

	[DontSave]
	public GameObject[] swordfightingHints;

	[DontSave]
	public GameObject lireHint;

	[DontSave]
	public GameObject[] krakenHitSounds;

	[DontSave]
	public RefArray<GameObject, AnimationSampler> krakenCinematics;

	[DontSave]
	public MaterialState sailsSwitchMaterialState;

	[DontSave]
	public Switch3D endPortal;

	[DontSave]
	public GameObject endScreenBoat;

	[DontSave]
	public Transform shipParent;

	[DontSave]
	public Sequence ghostShipSequence;

	[DontSave]
	public GameObject[] respawnColliders;

	[DontSave]
	public Item mastKey;

	[DontSave]
	public Ref<Jigsaw, Transform> runeJigsaw;

	[DontSave]
	public GameObject[] runeBoard2Children;

	[DontSave]
	public GameObject[] runeBoard1Children;

	[DontSave]
	public TweenState mainRopeGearTween;

	[DontSave]
	public MaterialState[] ropeMatStates;

	[DontSave]
	public TweenState capTableLid;

	[DontSave]
	public Slot[] correctKeySlots;

	[DontSave]
	public TweenState[] correctKeyTs;

	[DontSave]
	public Item[] captainKeys;

	[DontSave]
	public Slot[] keySlots;

	[DontSave]
	public Ref<Item, GameObject> cannonBallEnd_p;

	[DontSave]
	public TweenState TeeJoint_003;

	[DontSave]
	public Ref<GameObject> runeBoard1_pItem;

	[DontSave]
	public Ref<GameObject> runeBoard2_pItem;

	[DontSave]
	public Ref<JigsawPiece, GameObject, MaterialState, Transform> runeBoard1_p;

	[DontSave]
	public Ref<JigsawPiece, GameObject, MaterialState, Transform> runeBoard2_p;

	[DontSave]
	public Renderer[] telescopeRenderers;

	[DontSave]
	public MaterialState telescopeMS;

	[DontSave]
	public Item telescope;

	[DontSave]
	public TweenState[] combatLeftHands;

	[DontSave]
	public TweenState[] combatRightHands;

	[DontSave]
	public Slidable[] combatLeftHandsSlider;

	[DontSave]
	public Slidable[] combatRightHandsSlider;

	[DontSave]
	public Switch3D captainsDoor1;

	[DontSave]
	public Switch3D captainsDoor2;

	[DontSave]
	public Item combatBox;

	[DontSave]
	public Switch3D lanternShelf1;

	[DontSave]
	public Switch3D lanternShelf2;

	[DontSave]
	public Switch3D[] lanternSolution;

	[DontSave]
	public Switch3D[] lanternButtons;

	[DontSave]
	public TweenState lanternLockTween;

	[DontSave]
	public Item lanternLock;

	[DontSave]
	public Zoomable lanternZoom;

	[DontSave]
	public Ref<GameObject, Transform> kraken;

	[DontSave]
	public ParticleSystem hitKrakenEffect;

	[DontSave]
	public Ref<Sequence, GameObject> krakenSequence;

	[DontSave]
	public RefArray<GameObject, Transform> krakenSymbols;

	[DontSave]
	public ParticleSystem cannonEffect2;

	[DontSave]
	public ParticleSystem cannonEffect1;

	[DontSave]
	public GameObject[] tentacle3Obstacles;

	[DontSave]
	public GameObject[] tentacle2Obstacles;

	[DontSave]
	public GameObject[] tentacle1Obstacles;

	[DontSave]
	public RefArray<TweenState, GameObject, Animator> tentacleTSs;

	[DontSave]
	public Turnable[] knotsTurnables;

	[DontSave]
	public Switch3D knotsChestLid;

	[DontSave]
	public Ref<Lock, Item> knotsLock;

	[DontSave]
	public Ref<Zoomable> knotsLockZoom;

	[DontSave]
	public Item[] swords;

	[DontSave]
	public Turnable barrelTurnable;

	[DontSave]
	public RefArray<Slot, TweenState> swordSlots;

	[DontSave]
	public TweenState swordBarrelLid;

	[DontSave]
	public TweenState barrelRotateTs;

	[DontSave]
	public Ref<TweenState, GameObject> otherTentaclesTs;

	[DontSave]
	public Animation[] otherTentaclesAnimations;

	[DontSave]
	public GameObject cannon2FirePoint;

	[DontSave]
	public GameObject cannon1FirePoint;

	[DontSave]
	public TweenState cannon2Lid;

	[DontSave]
	public TweenState cannon1Lid;

	[DontSave]
	public RefArray<Item, MaterialState, GameObject> cannonRunes;

	[DontSave]
	public Slot[] runeSlots2;

	[DontSave]
	public Slot[] runeSlots1;

	[DontSave]
	public Turnable[] canonTurnables2;

	[DontSave]
	public Turnable[] canonTurnables;

	[DontSave]
	public TweenState cannonRecoil1;

	[DontSave]
	public TweenState cannonRecoil2;

	[DontSave]
	public MaterialState alredyInsertedRune1;

	[DontSave]
	public MaterialState alredyInsertedRune2;

	[DontSave]
	public MaterialState alredyInsertedRune3;

	[DontSave]
	public Slot cannonSlot2;

	[DontSave]
	public Slot cannonSlot;

	[DontSave]
	public Switch3D[] graffitiChestSwitches;

	[DontSave]
	public Switch3D graffitiChestLid;

	[DontSave]
	public Ref<Transform, Item> graffitiChest;

	[DontSave]
	public GameObject graffitiChestHookedPosition;

	[DontSave]
	public Sequence[] ropeSequences;

	[DontSave]
	public Dial ropeDial;

	[DontSave]
	public RefArray<GameObject, Transform> rightRopeMarks1;

	[DontSave]
	public RefArray<GameObject, Transform> rightRopeMarks2;

	[DontSave]
	public RefArray<GameObject, Transform> leftRopeMarks1;

	[DontSave]
	public RefArray<GameObject, Transform> leftRopeMarks2;

	[DontSave]
	public Transform ropeWheely1;

	[DontSave]
	public Transform ropeWheely2;

	[DontSave]
	public Transform ropeWheely3;

	[DontSave]
	public Transform ropeWheely4;

	[DontSave]
	public Transform ropeWheely5;

	[DontSave]
	public Switch3D ropeChestLid;

	[DontSave]
	public Ref<Lock, Zoomable> ropeLock;

	[DontSave]
	public Turnable[] ropeChestTurnables;

	[DontSave]
	public Switch3D smallChest1Kvaka;

	[DontSave]
	public Switch3D smallChest1Lid;

	[DontSave]
	public Switch3D smallChest2Kvaka;

	[DontSave]
	public Switch3D smallChest2Lid;

	[DontSave]
	public Switch3D backChestKvaka;

	[DontSave]
	public Switch3D backChestLid;

	[DontSave]
	public TweenState[] mainRopeTs;

	[DontSave]
	public Slot mainRopeKeySlot;

	[DontSave]
	public Turnable mainRopeTurnable;

	[DontSave]
	public Sequence mainRopeSequence;

	[DontSave]
	public Zoomable mainRopeZoomable;

	[DontSave]
	public Zoomable ropesZoomable;

	[DontSave]
	public GameObject krakenRune1;

	[DontSave]
	public GameObject krakenRune2;

	[DontSave]
	public GameObject krakenRune3;

	[DontSave]
	public ParticleSystem hitKraken1;

	[DontSave]
	public ParticleSystem hitKraken2;

	[DontSave]
	public RefArray<Item, GameObject> cannonBalls;

	[DontSave]
	public Transform nonShipObjects;

	[DontSave]
	public Transform mainRopeChestParent;

	[DontSave]
	public Ref<Animator> krakenHeadAnimator;

	[DontSave]
	public Ref<Animator> krakenBodyAnimator;

	[DontSave]
	public TweenState[] cannonRotateTurnablesTs1;

	[DontSave]
	public TweenState[] cannonRotateTurnablesTs2;

	[DontSave]
	public Ref<Sequence, GameObject> cannonFireSeq1;

	[DontSave]
	public Ref<Sequence, GameObject> cannonFireSeq2;

	[DontSave]
	public GameObject lanternHint;

	[DontSave]
	public TweenState[] combatBoxTs1;

	[DontSave]
	public TweenState[] combatBoxTs2;

	[DontSave]
	public Slot cannonShieldSlot;

	[DontSave]
	public Ref<GameObject, MaterialState> cannonShield;

	[DontSave]
	public Ref<MaterialState, GameObject> cannonShieldItem;

	[DontSave]
	public MaterialState[] cannonShieldCylinder;

	[DontSave]
	public Transform[] runeBoardPiecesTransform;

	[DontSave]
	public MaterialState[] runeBoardPiecesState;

	[DontSave]
	public Transform runeBoardTarget1;

	[DontSave]
	public Transform runeBoardTarget2;

	[DontSave]
	public GameObject smallChest1;

	[DontSave]
	public GameObject smallChest2;

	[DontSave]
	public GameObject brokenLeverCutscene;

	[DontSave]
	public AnimationSampler brokenLeverCutsceneAnimation;

	[DontSave]
	public GameObject brokenLeverParent;

	[DontSave]
	public AnimationSampler brokenLeverParentAnimation;

	[DontSave]
	public GameObject brokenLeverDummy;

	[DontSave]
	public GameObject brokenLeverPickup;

	public override void onRPCCalled(int type)
	{
		if (type == 0)
		{
			solveLantern();
		}
		if (type == 1)
		{
			Switch3D[] array = lanternButtons;
			foreach (Switch3D switch3D in array)
			{
				if (switch3D.state == Switch3DState.On)
				{
					game.startSwitch(switch3D);
				}
				switch3D.targetable = true;
			}
			game.startTimer(new LanternButtonsResetTimer(), 0.3f);
		}
		if (type == 2)
		{
			unlockGraffitiWheel();
		}
		if (type == 3)
		{
			solveGraffitiChest();
		}
		if (type == 4)
		{
			resetGraffitiChest();
		}
		if (type == 5)
		{
			solveCannon(0);
		}
		if (type == 6)
		{
			solveCannon(1);
		}
		if (type == 7)
		{
			solveCannon(2);
		}
		if (type == 8)
		{
			solveSwordBarrel();
		}
		if (type == 9)
		{
			solveCombatBox();
		}
		if (type == 10)
		{
			solveCaptainsTable();
		}
	}

	public override void onInit()
	{
		initRopePulleys();
		initKraken();
		initCombatDummies();
		krakenPivot = kraken.Get<Transform>(0f).position;
		initLandmarks();
		mainRopeSequence.setTime(mainRopeSequenceTime);
		mastKey.targetable = false;
		cannonRunes[3].Get<Item>(0).targetable = false;
		cannonRunes[5].Get<Item>(0).targetable = false;
		Switch3D[] array = graffitiChestSwitches;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = false;
		}
		cannonRunes[6].Get<Item>(0).hasRigidbody = false;
		swords[5].hasRigidbody = false;
		endScreenBoat.SetActive(value: false);
	}

	public override void onInitPredicates()
	{
		game.registerPredicate(LevelPredicate.GhostShip, () => landmarks[(int)currentLandmarkTarget] == PathLandmark.FollowGhost && ghostShipCondition());
		game.registerPredicate(LevelPredicate.North, () => landmarks[(int)currentLandmarkTarget] == PathLandmark.North && northCondition());
		game.registerPredicate(LevelPredicate.Buoys, () => landmarks[(int)currentLandmarkTarget] == PathLandmark.Buoys && buoysCondition());
		game.registerPredicate(LevelPredicate.Bells, () => landmarks[(int)currentLandmarkTarget] == PathLandmark.Bell && bellCondition());
	}

	public override void onLevelPredicateDone(int type, int doneCounter)
	{
		if (type == 0)
		{
			ghostShipSolved();
		}
		if (type == 1)
		{
			northSolved();
		}
		if (type == 2)
		{
			buoysSolved();
		}
		if (type == 3)
		{
			bellSolved();
		}
	}

	public override void onUpdate()
	{
		if (runeBoard1_p.Get<GameObject>(0f).activeSelf)
		{
			int num = -1;
			for (int i = 0; i < runeBoardPiecesTransform.Length; i++)
			{
				if (Vector3.Distance(runeBoardPiecesTransform[i].position, runeBoardTarget1.position) < 0.08f)
				{
					num = i;
				}
				else
				{
					runeBoardPiecesState[i].transitionTo("Glow", 2f, 0f);
				}
			}
			for (int j = 0; j < runeBoardPiecesTransform.Length; j++)
			{
				runeBoardPiecesState[j].transitionTo("Glow", 2f, 0f);
			}
			if (num != -1)
			{
				runeBoardPiecesState[num - 1].transitionTo("Glow", 2f);
				runeBoardPiecesState[num + 1].transitionTo("Glow", 2f);
				runeBoardPiecesState[num - 7].transitionTo("Glow", 2f);
				runeBoardPiecesState[num + 7].transitionTo("Glow", 2f);
				runeBoardPiecesState[num + 14].transitionTo("Glow", 2f);
			}
		}
		else if (runeBoard2_p.Get<GameObject>(0f).activeSelf)
		{
			int num2 = -1;
			for (int k = 0; k < runeBoardPiecesTransform.Length; k++)
			{
				if (Vector3.Distance(runeBoardPiecesTransform[k].position, runeBoardTarget2.position) < 0.08f)
				{
					num2 = k;
				}
				else
				{
					runeBoardPiecesState[k].transitionTo("Glow", 2f, 0f);
				}
			}
			for (int l = 0; l < runeBoardPiecesTransform.Length; l++)
			{
				runeBoardPiecesState[l].transitionTo("Glow", 2f, 0f);
			}
			if (num2 != -1)
			{
				if (num2 >= 0 && num2 < runeBoardPiecesState.Length)
				{
					runeBoardPiecesState[num2].transitionTo("Glow", 2f);
				}
				if (num2 + 1 >= 0 && num2 + 1 < runeBoardPiecesState.Length)
				{
					runeBoardPiecesState[num2 + 1].transitionTo("Glow", 2f);
				}
				if (num2 + 2 >= 0 && num2 + 2 < runeBoardPiecesState.Length)
				{
					runeBoardPiecesState[num2 + 2].transitionTo("Glow", 2f);
				}
				if (num2 - 7 >= 0 && num2 - 7 < runeBoardPiecesState.Length)
				{
					runeBoardPiecesState[num2 - 7].transitionTo("Glow", 2f);
				}
				if (num2 - 8 >= 0 && num2 - 8 < runeBoardPiecesState.Length)
				{
					runeBoardPiecesState[num2 - 8].transitionTo("Glow", 2f);
				}
			}
		}
		else
		{
			for (int m = 0; m < runeBoardPiecesTransform.Length; m++)
			{
				runeBoardPiecesState[m].transitionTo("Glow", 2f, 0f);
			}
		}
		bool flag = sailsSwitch.state == Switch3DState.On && (sailsSwitch.targetable || overrideSails);
		MaterialState[] array = sails;
		foreach (MaterialState materialState in array)
		{
			int num3 = (flag ? 1 : 0);
			if (materialState.getTargetWeight("SailsDown") != (float)num3)
			{
				materialState.transitionToStateAdditive("SailsDown", 0.3f, num3);
			}
			int num4 = ((flag && currentLandmarkTarget == PathLandmark.Storm) ? 1 : 0);
			if (materialState.getTargetWeight("SailsSpeed") != (float)num3)
			{
				materialState.transitionToStateAdditive("SailsSpeed", 0.5f, num4);
			}
		}
		if (shipDriveEffect.isPlaying && !flag)
		{
			if (prevSails)
			{
				PineFmod.playOneShotSound("event:/Sound Effects/05 Misc/Vehicles/Boat/Boat_Sails");
			}
			prevSails = false;
			shipDriveEffect.Stop();
		}
		if (!shipDriveEffect.isPlaying && flag)
		{
			if (!prevSails)
			{
				PineFmod.playOneShotSound("event:/Sound Effects/05 Misc/Vehicles/Boat/Boat_Sails");
			}
			prevSails = true;
			shipDriveEffect.Play();
		}
		updateKraken();
		compassNeedle.Get<Transform>(0f).localRotation = Quaternion.Euler(0f, 0f, 0f);
		Vector3 vector = compassNeedle.Get<Transform>(0f).InverseTransformPoint(north.position);
		float num5 = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
		compassNeedle.Get<Transform>(0f).localRotation = Quaternion.Euler(60f, 0f, num5);
		shipTargetTilt = Mathf.MoveTowards(shipTargetTilt, 0f, 0.05f);
		Quaternion quaternion = Quaternion.AngleAxis(shipTargetTilt, Vector3.right);
		shipParent.localRotation = shipParentStartRotation * quaternion;
		landmarksPivot.rotation = Quaternion.Euler(0f, landmarksPivotRotation, 0f);
		Vector3 to = buoysCenter.Get<Transform>(0f).position - ship.Get<Transform>(0f).position;
		float num6 = Vector3.Angle(ship.Get<Transform>(0f).right, to);
		if (currentLandmarkTarget == PathLandmark.Buoys)
		{
			if (num6 <= 15f && sailsSwitch.state == Switch3DState.On)
			{
				landmarksTimer += Time.deltaTime;
			}
			else
			{
				landmarksTimer = 0f;
			}
		}
		else if (currentLandmarkTarget == PathLandmark.Storm || currentLandmarkTarget == PathLandmark.Glow)
		{
			landmarksTimer += Time.deltaTime;
			if (landmarksTimer >= 5f)
			{
				landmarksTimer = 0f;
			}
		}
		else if (currentLandmarkTarget == PathLandmark.North)
		{
			if (Mathf.Abs(num5 - 90f) <= 10f && sailsSwitch.state == Switch3DState.On)
			{
				landmarksTimer += Time.deltaTime;
			}
			else
			{
				landmarksTimer = 0f;
			}
		}
		else if (currentLandmarkTarget == PathLandmark.FollowGhost)
		{
			Vector3 to2 = ghostShip.Get<Transform>(0f).position - ship.Get<Transform>(0f).position;
			float num7 = Vector3.Angle(ship.Get<Transform>(0f).right, to2);
			if (sailsSwitch.state == Switch3DState.On && num7 < 15f)
			{
				landmarksTimer += Time.deltaTime;
			}
			else
			{
				Vector3.Distance(ghostShip.Get<Transform>(0f).position, ship.Get<Transform>(0f).position);
				_ = 150f;
			}
		}
		if ((int)currentLandmarkTarget < landmarks.Count && landmarkCondition(landmarks[(int)currentLandmarkTarget]))
		{
			landmarkSolved(landmarks[(int)currentLandmarkTarget]);
		}
		onRuneBoardUpdate();
		updateRopes();
	}

	public override void onAddToInventory(Item item)
	{
		if (item == graffitiChest.Get<Item>(0f) && !graffitiChestCollected)
		{
			graffitiChestCollected = true;
			Switch3D[] array = graffitiChestSwitches;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].targetable = true;
			}
			mainRopeTurnable.targetable = false;
			mainRopeZoomable.targetable = false;
			graffitiChest.Get<Item>(0f).targetable = true;
		}
	}

	public override void onUnlock(Lock targetLock)
	{
		if (targetLock == ropeLock.Get<Lock>(0))
		{
			game.startTimer(new ropeLockDelayTimer(), 0.5f);
		}
		if (targetLock == knotsLock.Get<Lock>(0))
		{
			game.startTimer(new knotsLockDelayTimer(), 0.5f);
		}
		if (targetLock == limperChestLock.Get<Lock>(0))
		{
			solveLimperChest();
		}
	}

	public override void onJigsaw(Jigsaw jigsaw, JigsawPiece piece, JigsawEvent jigsawEvent)
	{
		if (jigsawEvent == JigsawEvent.PiecePlaced)
		{
			onBoardJigsaw(piece);
		}
	}

	public override void onSwitch3D(Switch3D targetSwitch, Switch3DEvent switchEvent)
	{
		if (targetSwitch == smallChest1Kvaka && switchEvent == Switch3DEvent.On)
		{
			smallChest1Lid.tweenState.transitionTo("Down", 1f, 0.2f);
			smallChest1Lid.targetable = true;
			smallChest1Kvaka.targetable = false;
		}
		if (targetSwitch == smallChest2Kvaka && switchEvent == Switch3DEvent.On)
		{
			smallChest2Lid.tweenState.transitionTo("Down", 1f, 0.2f);
			smallChest2Lid.targetable = true;
			smallChest2Kvaka.targetable = false;
		}
		if (targetSwitch == backChestKvaka && switchEvent == Switch3DEvent.On)
		{
			backChestLid.tweenState.transitionTo("Down", 1f, 0.2f);
			backChestLid.targetable = true;
			backChestKvaka.targetable = false;
		}
		int num = krakenCannons.FindIndex((KrakenCannon c) => c.cannonLid == targetSwitch);
		int index = UnityUtils.getIndex(graffitiChestSwitches, targetSwitch);
		if (num >= 0 && switchEvent == Switch3DEvent.On)
		{
			krakenCannons[num].cannonSlot.targetable = true;
		}
		if (UnityUtils.contains(lanternButtons, targetSwitch) && switchEvent == Switch3DEvent.On)
		{
			targetSwitch.targetable = false;
			lanternButtonCounter++;
			if (lanternButtonCounter >= 3)
			{
				Switch3D[] array = lanternButtons;
				for (int num2 = 0; num2 < array.Length; num2++)
				{
					array[num2].targetable = false;
				}
				if (checkLanternSolution())
				{
					game.callRPC(RPCType.LanternGood);
				}
				else
				{
					game.callRPC(RPCType.LanternWrong);
				}
				lanternButtonCounter = 0;
			}
		}
		if (targetSwitch == sailsSwitch && switchEvent == Switch3DEvent.Start)
		{
			sailsSwitch.gameObject.SetActive(value: false);
			brokenLeverParent.SetActive(value: true);
			brokenLeverParentAnimation.play();
			brokenLeverCutscene.SetActive(value: true);
			brokenLeverCutsceneAnimation.play();
			game.startTimer(new MastCutsceneTimer(), 6f);
		}
		if (targetSwitch == sailsSwitch && switchEvent != Switch3DEvent.Start)
		{
			onSailsSwitch(switchEvent);
		}
		else if (targetSwitch == bell && switchEvent == Switch3DEvent.Start)
		{
			bellTs.setWeight("NewState", 0f);
			bellTs.transitionTo("NewState", 0.75f);
			bell.targetable = false;
			if (isGlowSolved)
			{
				isGlowSolved = false;
				seaGlow.Stop();
			}
		}
		else if (index >= 0 && switchEvent == Switch3DEvent.On)
		{
			graffitiChestCurrent[graffitiChestIndex] = index;
			graffitiChestSwitches[index].targetable = false;
			graffitiChestIndex++;
			if (graffitiChestIndex >= GraffitiChestSolution.Length)
			{
				if (checkGraffitiChestSolution())
				{
					game.callRPC(RPCType.GraffitiChestGood);
				}
				else
				{
					game.callRPC(RPCType.GraffitiChestReset);
				}
			}
		}
		else if (targetSwitch == endPortal)
		{
			game.levelCompleted();
		}
		if (targetSwitch == smallChest1Lid && switchEvent == Switch3DEvent.Start && !swords[6].targetable && swords[6].slot == null)
		{
			swords[6].targetable = true;
			swords[7].targetable = true;
			game.changeExamineRotation(smallChest1, new Vector2(0f, 40f), 0.5f);
		}
		if (targetSwitch == smallChest2Lid && switchEvent == Switch3DEvent.Start && !captainKeys[2].targetable && captainKeys[2].slot == null)
		{
			captainKeys[2].targetable = true;
			game.changeExamineRotation(smallChest2, new Vector2(0f, 40f), 0.5f);
		}
		if (targetSwitch == graffitiChestLid && switchEvent == Switch3DEvent.Start)
		{
			game.changeExamineRotation(graffitiChest.Get<Item>(0f).gameObject, new Vector2(0f, 70f), 0.5f);
		}
	}

	public override void onTool(Item tool, ToolContext context)
	{
		Debug.Log("tool state: " + context.state);
		if (!(tool == telescope))
		{
			return;
		}
		if (context.state == ToolState.Start)
		{
			telescopeMS.transitionToDuration("Transparent", 0.25f);
			Renderer[] array = telescopeRenderers;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].enabled = false;
			}
		}
		if (context.state == ToolState.End)
		{
			telescopeMS.transitionToDuration("Default", 0.25f);
			Renderer[] array = telescopeRenderers;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].enabled = true;
			}
		}
	}

	public override void onDialMoved(Dial dial, MoveEvent moveEvent)
	{
		if (dial == ropeDial)
		{
			onRopeDial(dial.angleChange, interacting: true);
		}
		else if (dial == wheel)
		{
			compasAngle += dial.angleChange * 0.15f;
			compasAngle = Mathf.Repeat(compasAngle, 360f);
			landmarksPivotRotation += 0.03f * dial.angleChange;
			shipTargetTilt = Mathf.MoveTowards(shipTargetTilt, Mathf.Sign(dial.angleChange) * 1.5f, 0.1f);
			Debug.Log($"onDialMoved - shipTargetTilt: {shipTargetTilt}");
		}
	}

	public override void onTurnableMoved(Turnable turnable, MoveEvent moveEvent)
	{
		Turnable turnable2 = Array.Find(canonTurnables, (Turnable t) => t == turnable);
		Turnable turnable3 = Array.Find(canonTurnables2, (Turnable t) => t == turnable);
		if (turnable2 != null && moveEvent == MoveEvent.Released)
		{
			checkCorrectCannonRunes(0);
		}
		if (turnable3 != null && moveEvent == MoveEvent.Released)
		{
			checkCorrectCannonRunes(1);
		}
		if (turnable == mainRopeTurnable && !graffitiChestCollected)
		{
			float num = turnable.value - turnable.lastValue;
			if (num > 180f)
			{
				num -= 360f;
			}
			if (num < -180f)
			{
				num += 360f;
			}
			mainRopeSequence.sequenceTime += num * 0.001f;
			mainRopeSequence.sequenceTime = Mathf.Clamp01(mainRopeSequence.sequenceTime);
			mainRopeSequence.setTime(mainRopeSequence.sequenceTime);
			mainRopeSequenceTime = mainRopeSequence.sequenceTime;
			graffitiChest.Get<Item>(0f).targetable = graffitiChest.Get<Transform>(0).parent == mainRopeChestParent && mainRopeSequence.sequenceTime > 0.65f;
			if (mainRopeSequence.sequenceTime < 0.001f)
			{
				game.setParent(graffitiChest.Get<Transform>(0), mainRopeChestParent);
			}
		}
	}

	public override void onSlidableMoved(Slidable slidable, MoveEvent moveEvent)
	{
		onCombatSlidables(slidable, moveEvent);
	}

	public override void onSlot(Slot targetSlot)
	{
		if (targetSlot == cannonShieldSlot)
		{
			solveCannonShield();
		}
		if (targetSlot == mainRopeKeySlot)
		{
			game.callRPC(RPCType.UnlockGraffitiWheel);
		}
		Slot[] array = keySlots;
		foreach (Slot slot in array)
		{
			if (targetSlot == slot && checkCaptainsTableSolution())
			{
				game.callRPC(RPCType.CaptainsDesk);
			}
		}
		int num = krakenCannons.FindIndex((KrakenCannon c) => c.cannonSlot == targetSlot);
		if (num >= 0 && krakenCannonRunesSolved[num])
		{
			onCannonSlot(num);
		}
		for (int num2 = 0; num2 < swordSlots.Length; num2++)
		{
			Slot slot2 = swordSlots[num2].Get<Slot>(0);
			if (targetSlot == slot2)
			{
				TweenState tweenState = swordSlots[num2].Get<TweenState>(0f);
				if (tweenState != null)
				{
					tweenState.transitionTo("In", 2f);
				}
				if (checkSwordBarrel())
				{
					game.callRPC(RPCType.SwordBarrel);
				}
			}
		}
		Slot slot3 = Array.Find(runeSlots1, (Slot s) => s == targetSlot);
		Slot slot4 = Array.Find(runeSlots2, (Slot s) => s == targetSlot);
		if (slot3 != null)
		{
			checkCorrectCannonRunes(0);
		}
		if (slot4 != null)
		{
			checkCorrectCannonRunes(1);
		}
	}

	public override void onRemoveFromSlot(Slot targetSlot, Item item)
	{
		for (int i = 0; i < swordSlots.Length; i++)
		{
			Slot slot = swordSlots[i].Get<Slot>(0);
			if (targetSlot == slot)
			{
				TweenState tweenState = swordSlots[i].Get<TweenState>(0f);
				if (tweenState != null)
				{
					tweenState.setWeight("In", 0f);
				}
				if (checkSwordBarrel())
				{
					game.callRPC(RPCType.SwordBarrel);
				}
			}
		}
	}

	public override void onTweenTransitionDone(TweenState tweenState, string state)
	{
		if (tweenState == bellTs)
		{
			bell.targetable = true;
			if (currentLandmarkTarget == PathLandmark.Bell)
			{
				bellRings++;
			}
			else
			{
				landmarksTimer = 0f;
			}
		}
		if (tweenState == combatBoxTs1[0])
		{
			TweenState[] array = combatBoxTs2;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].transitionTo("Down");
			}
		}
		if (tweenState == limperChestLockTween)
		{
			limperChestSequence.Get<Sequence>(0).play(-1f, 0.6f);
			limperChestSequence.Get<Switch3D>(0f).targetable = true;
			limperChestLock.Get<Item>(0f).targetable = true;
			limperChestLock.Get<Item>(0f).hasRigidbody = true;
			game.increaseZoomCounter(limperChestLockZoom.Get<Zoomable>());
		}
		if (tweenState == lanternLockTween)
		{
			game.increaseZoomCounter(lanternZoom);
			lanternLock.targetable = true;
			lanternLock.hasRigidbody = true;
			lanternShelf1.targetable = true;
			lanternShelf2.targetable = true;
			game.finishPuzzle(Puzzle.Lanterns);
		}
		if (tweenState == TeeJoint_003)
		{
			knotsChestLid.sequence.play(-1f, 0.6f);
			knotsChestLid.targetable = true;
			knotsLock.Get<Item>(0f).targetable = true;
			knotsLock.Get<Item>(0f).hasRigidbody = true;
			cannonRunes[3].Get<Item>(0).targetable = true;
			cannonRunes[5].Get<Item>(0).targetable = true;
		}
		if (!(tweenState == otherTentaclesTs.Get<TweenState>(0)))
		{
			return;
		}
		kraken.Get<GameObject>(0).SetActive(value: false);
		krakenSequence.Get<GameObject>(0f).SetActive(value: false);
		otherTentaclesTs.Get<GameObject>(0f).SetActive(value: false);
		foreach (Ref<TweenState, GameObject, Animator> item in tentacleTSs)
		{
			item.Get<GameObject>(0f).SetActive(value: false);
		}
	}

	public override void onSequenceDone(Sequence sequence)
	{
		if (sequence == ghostShipSequence)
		{
			ghostShipSequence.play(0f, -1f, 0.2f);
		}
	}

	public override void onTimerDone(Timer timer)
	{
		if (timer is ShakeScreenTimer)
		{
			ghostShip.Get<GameObject>(0).SetActive(value: false);
			game.screenShake(0.4f, 0.1f, 0.1f);
			sailsSwitch.targetable = false;
			wheel.targetable = false;
		}
		if (timer is HideFinalFogTimer)
		{
			finalFog.Stop();
		}
		if (timer is FinishLevelTimer)
		{
			game.levelCompleted();
			endScreenBoat.SetActive(value: true);
		}
		if (timer is ShowIslandTimer)
		{
			game.startTimer(new ShakeScreenTimer(), 3f);
			island.Get<GameObject>(0).SetActive(value: true);
			island.Get<Sequence>(0f).play();
			overrideSails = false;
			PineFmod.playOneShotSound("event:/Sound Effects/05 Misc/Vehicles/Boat/Boat_Beaching");
			game.finishPuzzle(Puzzle.Sailing);
			game.startTimer(new FinishLevelTimer(), 8f);
		}
		if (timer is GhostShipTimer)
		{
			krakenCinematics[3].Get<GameObject>(0).SetActive(value: false);
			ghostShipSequence.play(-1f, -1f, 0.1f);
		}
		if (timer is KrakenCinematicTimer timer2)
		{
			onKrakenCinematicTimerDone(timer2);
		}
		if (timer is LanternButtonsResetTimer)
		{
			Switch3D[] array = lanternButtons;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].targetable = true;
			}
			return;
		}
		if (timer is UnlockGraffitiWheelTimer)
		{
			mainRopeTurnable.targetable = true;
			mainRopeGearTween.transitionTo("NewState");
			TweenState[] array2 = mainRopeTs;
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i].transitionTo("NewState");
			}
			return;
		}
		if (timer is CannonStartAnimTimer timer3)
		{
			onCannonStartAnimTimerDone(timer3);
			return;
		}
		if (timer is CannonFlipFireSwitchTimer timer4)
		{
			onCannonFlipFireSwitchTimerDone(timer4);
			return;
		}
		if (timer is CannonThrowBallTimer timer5)
		{
			onCannonThrowBallTimerDone(timer5);
			return;
		}
		if (timer is KrakenHitTimer timer6)
		{
			onKrakenHitTimerDone(timer6);
			return;
		}
		if (timer is SolveCombatBoxTimer)
		{
			captainKeys[3].targetable = true;
			TweenState[] array2 = combatBoxTs1;
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i].transitionTo("NewState");
			}
			return;
		}
		if (timer is knotsLockDelayTimer)
		{
			solveKnotsChest();
			return;
		}
		if (timer is ropeLockDelayTimer)
		{
			solveRopeChest();
			return;
		}
		if (timer is BarrelAnimTimer)
		{
			game.startTimer(new BarrelAnim2Timer(), 0.5f);
			barrelRotateTs.transitionTo("NewState", 0.5f);
			swordBarrelLid.transitionTo("Open", 0.5f);
			{
				foreach (Ref<Slot, TweenState> swordSlot in swordSlots)
				{
					swordSlot.Get<TweenState>(0f).transitionTo("In", 2f, 0.3f);
				}
				return;
			}
		}
		if (timer is BarrelAnim2Timer)
		{
			foreach (Ref<Slot, TweenState> swordSlot2 in swordSlots)
			{
				swordSlot2.Get<TweenState>(0f).transitionTo("In");
			}
			return;
		}
		if (timer is BoardDissolveTimer timer7)
		{
			onBoardDissolveTimerDone(timer7);
		}
		else if (timer is MastCutsceneTimer)
		{
			brokenLeverCutscene.SetActive(value: false);
			brokenLeverDummy.SetActive(value: false);
			brokenLeverPickup.SetActive(value: true);
		}
	}

	public override void onMaterialTransitionDone(MaterialState tweenState, string state)
	{
		if (tweenState == cannonShield.Get<MaterialState>(0f))
		{
			cannonShield.Get<GameObject>(0).SetActive(value: false);
		}
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void solveCannonShield()
	{
		cannonShield.Get<MaterialState>(0f).setWeight("Dissolve", 0.3f);
		cannonShield.Get<MaterialState>(0f).transitionTo("Dissolve", 0.3f);
		cannonShieldItem.Get<MaterialState>(0).setWeight("Dissolve", 0.3f);
		cannonShieldItem.Get<MaterialState>(0).transitionTo("Dissolve", 0.3f);
		MaterialState[] array = cannonShieldCylinder;
		foreach (MaterialState obj in array)
		{
			obj.setWeight("Dissolve", 0.3f);
			obj.transitionTo("Dissolve", 0.3f);
		}
		Turnable[] array2 = canonTurnables2;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].targetable = true;
		}
		Slot[] array3 = runeSlots2;
		for (int i = 0; i < array3.Length; i++)
		{
			array3[i].targetable = true;
		}
	}

	private void initRopePulleys()
	{
		mainRopeTurnable.targetable = false;
		if (!interactedWithPuley)
		{
			ropeSequences[0].setTime(0.4f);
			ropeSequences[1].setTime(0.4f);
		}
		ropeWheely1.localEulerAngles = new Vector3(0f, 90f, ropeWheely1.eulerAngles.z);
		ropeWheely2.localEulerAngles = new Vector3(0f, 90f, ropeWheely1.eulerAngles.z);
		ropeWheely3.localEulerAngles = new Vector3(0f, 90f, ropeWheely3.eulerAngles.z);
		ropeWheely4.localEulerAngles = new Vector3(0f, 90f, ropeWheely1.eulerAngles.z);
		ropeWheely5.localEulerAngles = new Vector3(0f, 90f, ropeWheely5.eulerAngles.z);
	}

	private void onRopeDial(float angleChange, bool interacting)
	{
		interactedWithPuley = true;
		float sequenceTime = ropeSequences[0].sequenceTime;
		ropeSequences[0].sequenceTime = Mathf.Clamp(ropeSequences[0].sequenceTime + angleChange * dialSpeed * 0.01f, 0f, 1f);
		ropeSequences[0].setTime(ropeSequences[0].sequenceTime);
		ropeSequences[1].sequenceTime = Mathf.Clamp(ropeSequences[1].sequenceTime + angleChange * dialSpeed * 0.01f, 0f, 1f);
		ropeSequences[1].setTime(ropeSequences[1].sequenceTime);
		for (int i = 0; i < ropeMatStates.Length; i++)
		{
			ropeMatStates[i].setState("Move", ropeSequences[0].sequenceTime);
		}
		if (ropeSequences[0].sequenceTime != sequenceTime)
		{
			ropeWheely1.localEulerAngles = new Vector3(0f, 90f, ropeWheely1.eulerAngles.z + angleChange * 0.6f);
			ropeWheely2.localEulerAngles = new Vector3(0f, 90f, ropeWheely1.eulerAngles.z - angleChange * 0.3f);
			ropeWheely3.localEulerAngles = new Vector3(0f, 90f, ropeWheely3.eulerAngles.z - angleChange * 0.6f);
			ropeWheely4.localEulerAngles = new Vector3(0f, 90f, ropeWheely1.eulerAngles.z - angleChange * 0.3f);
			ropeWheely5.localEulerAngles = new Vector3(0f, 90f, ropeWheely5.eulerAngles.z + angleChange * 0.6f);
		}
	}

	private void updateRopes()
	{
		if (!ropesZoomable.targetable)
		{
			return;
		}
		foreach (Ref<GameObject, Transform> item in leftRopeMarks1)
		{
			bool flag = item.Get<Transform>(0f).position.y > ropeWheely2.position.y && item.Get<Transform>(0f).position.y <= ropeWheely1.position.y;
			if (item.Get<GameObject>(0).activeSelf != flag)
			{
				item.Get<GameObject>(0).SetActive(flag);
			}
		}
		foreach (Ref<GameObject, Transform> item2 in leftRopeMarks2)
		{
			bool flag2 = item2.Get<Transform>(0f).position.y > ropeWheely2.position.y && item2.Get<Transform>(0f).position.y <= ropeWheely3.position.y;
			if (item2.Get<GameObject>(0).activeSelf != flag2)
			{
				item2.Get<GameObject>(0).SetActive(flag2);
			}
		}
		foreach (Ref<GameObject, Transform> item3 in rightRopeMarks1)
		{
			bool flag3 = item3.Get<Transform>(0f).position.y > ropeWheely4.position.y && item3.Get<Transform>(0f).position.y <= ropeWheely3.position.y;
			if (item3.Get<GameObject>(0).activeSelf != flag3)
			{
				item3.Get<GameObject>(0).SetActive(flag3);
			}
		}
		foreach (Ref<GameObject, Transform> item4 in rightRopeMarks2)
		{
			bool flag4 = item4.Get<Transform>(0f).position.y > ropeWheely4.position.y && item4.Get<Transform>(0f).position.y <= ropeWheely5.position.y;
			if (item4.Get<GameObject>(0).activeSelf != flag4)
			{
				item4.Get<GameObject>(0).SetActive(flag4);
			}
		}
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void solveRopeChest()
	{
		ropeChestLid.tweenState.transitionTo("Down", ropeChestLid.transitionSpeed, 0.2f);
		ropeChestLid.targetable = true;
		Turnable[] array = ropeChestTurnables;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = false;
		}
		ropeLock.Get<Zoomable>(0f).targetable = false;
		ropesZoomable.targetable = false;
		ropeDial.targetable = false;
		mastKey.targetable = true;
		game.finishPuzzle(Puzzle.RopeLinesChest);
	}

	[DebugButton(null, Tint.Default, PostClickAction.HideButton, 0, new object[] { })]
	private void unlockGraffitiWheel()
	{
		mainRopeZoomable.targetable = true;
		game.startTimer(new UnlockGraffitiWheelTimer(), 0.3f);
		game.finishPuzzle(Puzzle.RuneKey);
	}

	[DebugButton(null, Tint.Default, PostClickAction.HideButton, 0, new object[] { })]
	private void debugGetGraffitiChest()
	{
		game.addItemToInventory(graffitiChest.Get<Item>(0f).gameObject);
	}

	private bool checkGraffitiChestSolution()
	{
		bool flag = true;
		for (int i = 0; i < GraffitiChestSolution.Length; i++)
		{
			flag &= graffitiChestCurrent[i] == GraffitiChestSolution[i];
		}
		return flag;
	}

	private void resetGraffitiChest()
	{
		graffitiChestIndex = 0;
		for (int i = 0; i < graffitiChestCurrent.Length; i++)
		{
			graffitiChestCurrent[i] = -1;
		}
		Switch3D[] array = graffitiChestSwitches;
		foreach (Switch3D switch3D in array)
		{
			switch3D.targetable = true;
			game.startSwitch(switch3D);
		}
	}

	[DebugButton(null, Tint.Default, PostClickAction.HideButton, 0, new object[] { })]
	private void solveGraffitiChest()
	{
		graffitiChestLid.targetable = true;
		graffitiChestLid.tweenState.transitionTo("Down", 2f, 0.2f);
		game.finishPuzzle(Puzzle.RuneChest);
	}

	private void initKraken()
	{
		krakenCannons = new List<KrakenCannon>();
		krakenCannons.Add(new KrakenCannon
		{
			turnables = canonTurnables,
			slots = runeSlots1,
			cannonSlot = cannonSlot,
			cannonLid = cannon1Lid,
			cannonFirePoint = cannon1FirePoint,
			cannonEffect = cannonEffect1,
			solvedTs = tentacleTSs[0].Get<TweenState>(0),
			solvedAnim = tentacleTSs[0].Get<Animator>(0u),
			obstacles = tentacle1Obstacles,
			hitKraken = hitKraken1,
			alredyInsertedRunes = new MaterialState[2] { alredyInsertedRune1, alredyInsertedRune2 },
			turnablesSolution = new List<int> { 1, 3, 0 },
			runeSlotSolution = new Dictionary<Slot, Item[]>
			{
				{
					runeSlots1[7],
					new Item[2]
					{
						cannonRunes[1],
						cannonRunes[6]
					}
				},
				{
					runeSlots1[4],
					new Item[1] { cannonRunes[2] }
				},
				{
					runeSlots1[6],
					new Item[2]
					{
						cannonRunes[0],
						cannonRunes[5]
					}
				}
			},
			rotateTurnablesTs = cannonRotateTurnablesTs1,
			fireCannonSeq = cannonFireSeq1.Get<Sequence>(0),
			cannonBallEffect = cannonFireSeq1.Get<GameObject>(0f)
		});
		krakenCannons.Add(new KrakenCannon
		{
			turnables = canonTurnables2,
			slots = runeSlots2,
			cannonSlot = cannonSlot2,
			cannonLid = cannon2Lid,
			cannonFirePoint = cannon2FirePoint,
			cannonEffect = cannonEffect2,
			solvedTs = tentacleTSs[1].Get<TweenState>(0),
			solvedAnim = tentacleTSs[1].Get<Animator>(0u),
			obstacles = tentacle2Obstacles,
			hitKraken = hitKraken2,
			alredyInsertedRunes = new MaterialState[1] { alredyInsertedRune3 },
			turnablesSolution = new List<int> { 0, 1, 2 },
			runeSlotSolution = new Dictionary<Slot, Item[]>
			{
				{
					runeSlots2[0],
					new Item[2]
					{
						cannonRunes[3],
						cannonRunes[4]
					}
				},
				{
					runeSlots2[4],
					new Item[2]
					{
						cannonRunes[1],
						cannonRunes[6]
					}
				},
				{
					runeSlots2[5],
					new Item[2]
					{
						cannonRunes[0],
						cannonRunes[5]
					}
				},
				{
					runeSlots2[11],
					new Item[2]
					{
						cannonRunes[3],
						cannonRunes[4]
					}
				}
			},
			rotateTurnablesTs = cannonRotateTurnablesTs2,
			fireCannonSeq = cannonFireSeq2.Get<Sequence>(0),
			cannonBallEffect = cannonFireSeq2.Get<GameObject>(0f)
		});
		krakenCannons.Add(new KrakenCannon
		{
			turnables = Array.Empty<Turnable>(),
			slots = Array.Empty<Slot>(),
			cannonSlot = cannonSlot,
			cannonLid = cannon1Lid,
			cannonFirePoint = cannon1FirePoint,
			cannonEffect = cannonEffect1,
			solvedTs = tentacleTSs[2].Get<TweenState>(0),
			solvedAnim = tentacleTSs[2].Get<Animator>(0u),
			obstacles = tentacle3Obstacles,
			hitKraken = hitKraken1,
			alredyInsertedRunes = new MaterialState[0],
			turnablesSolution = new List<int>(),
			runeSlotSolution = new Dictionary<Slot, Item[]>(),
			rotateTurnablesTs = cannonRotateTurnablesTs1,
			fireCannonSeq = cannonFireSeq1.Get<Sequence>(0),
			cannonBallEffect = cannonFireSeq1.Get<GameObject>(0f)
		});
		foreach (KrakenCannon krakenCannon in krakenCannons)
		{
			_ = krakenCannon;
			krakenCannonRunesSolved.Add(item: false);
		}
	}

	private void updateKraken()
	{
		krakenHeadAnimator.Get<Animator>().SetBool("isDown", isKrakenDown);
		krakenBodyAnimator.Get<Animator>().SetBool("isDown", isKrakenDown);
		foreach (Ref<Item, GameObject> cannonBall in cannonBalls)
		{
			if (cannonBall.Get<Item>(0).itemRespawn == ItemRespawn.NeverRespawn && cannonBall.Get<Item>(0).transform.position.y < -10f)
			{
				cannonBall.Get<Item>(0).hasRigidbody = false;
			}
		}
		bool flag = krakenSequence.Get<Sequence>(0).sequenceTime == krakenSequence.Get<Sequence>(0).targetSequenceTime && krakenSequence.Get<Sequence>(0).sequenceTime < krakenSequence.Get<Sequence>(0).sequenceDuration;
		if (flag)
		{
			if (!swayKraken)
			{
				krakenPivot = kraken.Get<Transform>(0f).position;
				swayKrakenT = 0.5f;
			}
			else
			{
				kraken.Get<Transform>(0f).position = Vector3.Lerp(krakenPivot + kraken.Get<Transform>(0f).right * 5f, krakenPivot - kraken.Get<Transform>(0f).right * 5f, Mathf.PingPong(swayKrakenT, 1f));
				swayKrakenT += Time.deltaTime * 0.25f;
			}
		}
		swayKraken = flag;
	}

	private void onCannonSlot(int cannonIndex)
	{
		krakenCannons[cannonIndex].cannonSlot.targetable = false;
		krakenCannons[cannonIndex].cannonSlot.insertedItem.targetable = false;
		krakenCannons[cannonIndex].cannonLid.transitionTo("NewState", 1f, 0f);
		game.startTimer(new CannonFlipFireSwitchTimer(cannonIndex, 0), 0.1f);
	}

	private void onCannonFlipFireSwitchTimerDone(CannonFlipFireSwitchTimer timer)
	{
		if (timer.index == 0)
		{
			onCannonFireSwitch(timer.cannonIndex);
		}
	}

	private void onCannonFireSwitch(int cannonIndex)
	{
		Item insertedItem = krakenCannons[cannonIndex].cannonSlot.insertedItem;
		if (!(insertedItem == null))
		{
			if (insertedItem == cannonBallEnd_p.Get<Item>(0))
			{
				cannonIndex = 2;
				krakenCannonRunesSolved[2] = true;
			}
			if (cannonIndex == 0 && krakenCannonRunesSolved[1])
			{
				game.startTimer(new CannonStartAnimTimer(2), 1f);
				return;
			}
			krakenCannons[cannonIndex].cannonSlot.targetable = false;
			game.startTimer(new CannonStartAnimTimer(cannonIndex), 1f);
			game.startTimer(new KrakenCinematicTimer(cannonIndex, 0), 1f);
		}
	}

	private void onCannonStartAnimTimerDone(CannonStartAnimTimer timer)
	{
		game.startTimer(new CannonThrowBallTimer(timer.index, 0), 1f);
		TweenState[] rotateTurnablesTs = krakenCannons[timer.index].rotateTurnablesTs;
		for (int i = 0; i < rotateTurnablesTs.Length; i++)
		{
			rotateTurnablesTs[i].transitionTo("NewState");
		}
	}

	private void onKrakenCinematicTimerDone(KrakenCinematicTimer timer)
	{
		if (timer.index == 0)
		{
			krakenCinematics[timer.cinematicIndex].Get<GameObject>(0).SetActive(value: true);
			krakenCinematics[timer.cinematicIndex].Get<AnimationSampler>(0f).play();
			game.startTimer(new KrakenCinematicTimer(timer.cinematicIndex, 1), krakenCinematicDurations[timer.cinematicIndex]);
		}
		else if (timer.index == 1)
		{
			krakenCinematics[timer.cinematicIndex].Get<GameObject>(0).SetActive(value: false);
		}
	}

	private void checkCorrectCannonRunes(int cannonIndex, bool solve = false)
	{
		if (!(krakenCannons[cannonIndex].checkSolution() || solve) || krakenCannonRunesSolved[cannonIndex])
		{
			return;
		}
		krakenCannonRunesSolved[cannonIndex] = true;
		Slot[] slots = krakenCannons[cannonIndex].slots;
		foreach (Ref<Item, MaterialState, GameObject> cannonRune in cannonRunes)
		{
			Slot[] array = slots;
			foreach (Slot slot in array)
			{
				if (cannonRune.Get<Item>(0) == slot.insertedItem)
				{
					cannonRune.Get<MaterialState>(0f).transitionTo("NewState", 2f);
					slot.insertedItem.targetable = false;
				}
				slot.targetable = false;
			}
		}
		if (krakenCannons[cannonIndex].turnables.Length > 3)
		{
			for (int j = 0; j < 3; j++)
			{
				krakenCannons[cannonIndex].turnables[j].targetable = false;
			}
		}
		MaterialState[] alredyInsertedRunes = krakenCannons[cannonIndex].alredyInsertedRunes;
		for (int i = 0; i < alredyInsertedRunes.Length; i++)
		{
			alredyInsertedRunes[i].transitionTo("NewState", 2f);
		}
		krakenCannons[cannonIndex].cannonLid.transitionTo("NewState");
		krakenCannons[cannonIndex].cannonSlot.targetable = true;
	}

	private void solveCannon(int cannonIndex)
	{
		KrakenCannon krakenCannon = krakenCannons[cannonIndex];
		GameObject[] obstacles = krakenCannon.obstacles;
		for (int i = 0; i < obstacles.Length; i++)
		{
			obstacles[i].SetActive(value: false);
		}
		float sequenceGoal = 1.2f;
		int symbol = 1;
		if (cannonIndex < 2)
		{
			Slot[] slots = krakenCannon.slots;
			foreach (Ref<Item, MaterialState, GameObject> cannonRune in cannonRunes)
			{
				Slot[] array = slots;
				foreach (Slot slot in array)
				{
					if (cannonRune.Get<Item>(0) == slot.insertedItem)
					{
						cannonRune.Get<MaterialState>(0f).transitionTo("Black", 2f);
					}
				}
			}
		}
		MaterialState[] alredyInsertedRunes = krakenCannons[cannonIndex].alredyInsertedRunes;
		for (int i = 0; i < alredyInsertedRunes.Length; i++)
		{
			alredyInsertedRunes[i].transitionTo("Black", 2f);
		}
		switch (cannonIndex)
		{
		case 0:
		{
			barrelTurnable.targetable = true;
			foreach (Ref<Slot, TweenState> swordSlot in swordSlots)
			{
				swordSlot.Get<Slot>(0).targetable = true;
			}
			game.removeItemFromInventory(runeBoard1_pItem.Get<GameObject>());
			runeBoard1_p.Get<MaterialState>(0u).transitionTo("Dissolve");
			game.startTimer(new BoardDissolveTimer(), 1f);
			obstacles = runeBoard1Children;
			for (int i = 0; i < obstacles.Length; i++)
			{
				obstacles[i].SetActive(value: false);
			}
			game.deselectJigsaw(runeBoard1_p.Get<JigsawPiece>(0).jigsaw);
			respawnColliders[0].SetActive(value: false);
			respawnColliders[1].SetActive(value: false);
			cannonRunes[6].Get<Item>(0).hasRigidbody = true;
			swords[5].hasRigidbody = true;
			break;
		}
		case 1:
			sequenceGoal = 3.7f;
			symbol = 2;
			krakenCannons[0].cannonLid.transitionTo("NewState");
			krakenCannons[0].cannonSlot.targetable = true;
			captainsDoor1.targetable = true;
			captainsDoor2.targetable = true;
			respawnColliders[3].SetActive(value: false);
			break;
		case 2:
			if (krakenCannonRunesSolved[cannonIndex])
			{
				sequenceGoal = krakenSequence.Get<Sequence>(0).sequenceDuration;
				symbol = -1;
				Animation[] array2 = otherTentaclesAnimations;
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i].enabled = false;
				}
				respawnColliders[2].SetActive(value: false);
			}
			break;
		}
		if (krakenCannonRunesSolved[cannonIndex])
		{
			game.startTimer(new KrakenHitTimer(KrakenHitTimer.KrakenState.HitEffect, cannonIndex, sequenceGoal, symbol), 0.9f);
		}
	}

	private void onKrakenHitTimerDone(KrakenHitTimer timer)
	{
		int index = timer.index;
		if (timer.state == KrakenHitTimer.KrakenState.HitEffect)
		{
			krakenCannons[index].hitKraken.Play();
			krakenHitSounds[index % 2].SetActive(value: true);
			game.startTimer(new KrakenHitTimer(KrakenHitTimer.KrakenState.HitEffectTurnOff, index, timer.sequenceGoal, timer.symbol), 0.7f);
		}
		else if (timer.state == KrakenHitTimer.KrakenState.HitEffectTurnOff)
		{
			krakenCannons[index].cannonBallEffect.SetActive(value: false);
			game.startTimer(new KrakenHitTimer(KrakenHitTimer.KrakenState.KrakenDrownAnim, index, timer.sequenceGoal, timer.symbol), 0.3f);
		}
		else if (timer.state == KrakenHitTimer.KrakenState.KrakenDrownAnim)
		{
			isKrakenDown = true;
			game.startTimer(new KrakenHitTimer(KrakenHitTimer.KrakenState.RemoveTentacle, index, timer.sequenceGoal, timer.symbol), 1f);
		}
		else if (timer.state == KrakenHitTimer.KrakenState.RemoveTentacle)
		{
			krakenCannons[index].solvedTs.transitionToDuration("Down", 3f);
			krakenCannons[index].solvedAnim.SetBool("isDown", value: true);
			game.startTimer(new KrakenHitTimer(KrakenHitTimer.KrakenState.KrakenPlaySequence, index, timer.sequenceGoal, timer.symbol), 0.4f);
		}
		else if (timer.state == KrakenHitTimer.KrakenState.KrakenPlaySequence)
		{
			if (timer.index == 2)
			{
				otherTentaclesTs.Get<TweenState>(0).transitionToDuration("Down", 3f);
			}
			krakenSequence.Get<Sequence>(0).play(-1f, timer.sequenceGoal);
			if (timer.index == 0)
			{
				game.finishPuzzle(Puzzle.Cannon1);
			}
			if (timer.index == 1)
			{
				game.finishPuzzle(Puzzle.Cannon2);
			}
			if (timer.index == 2)
			{
				game.finishPuzzle(Puzzle.Cannon3);
			}
			game.startTimer(new KrakenHitTimer(KrakenHitTimer.KrakenState.KrakenRise, index, timer.sequenceGoal, timer.symbol), 1.4f);
		}
		else if (timer.state == KrakenHitTimer.KrakenState.KrakenRise)
		{
			krakenHitSounds[index % 2].SetActive(value: false);
			isKrakenDown = false;
			game.startTimer(new KrakenHitTimer(KrakenHitTimer.KrakenState.Symbol, index, timer.sequenceGoal, timer.symbol), 1f);
		}
		else
		{
			if (timer.state != KrakenHitTimer.KrakenState.Symbol)
			{
				return;
			}
			for (int i = 0; i < krakenSymbols.Length; i++)
			{
				krakenSymbols[i].Get<GameObject>(0).SetActive(i == timer.symbol);
				if (i == index)
				{
					tentacleTSs[i].Get<GameObject>(0f).SetActive(value: false);
				}
			}
		}
	}

	private void onBoardJigsaw(JigsawPiece piece)
	{
		if (runeBoard1_p.Get<GameObject>(0f).activeSelf && piece == runeBoard2_p.Get<JigsawPiece>(0))
		{
			runeBoard1_p.Get<MaterialState>(0u).transitionTo("Dissolve");
			GameObject[] array = runeBoard1Children;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetActive(value: false);
			}
			game.startTimer(new BoardDissolveTimer(), 1f);
		}
		if (runeBoard2_p.Get<GameObject>(0f).activeSelf && piece == runeBoard1_p.Get<JigsawPiece>(0))
		{
			runeBoard2_p.Get<GameObject>(0f).SetActive(value: false);
			runeBoard2_p.Get<MaterialState>(0u).transitionTo("Dissolve");
			GameObject[] array = runeBoard2Children;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetActive(value: false);
			}
		}
	}

	private void onBoardDissolveTimerDone(BoardDissolveTimer timer)
	{
		runeBoard1_p.Get<GameObject>(0f).SetActive(value: false);
		runeBoard1_pItem.Get<GameObject>().SetActive(value: false);
	}

	private void onRuneBoardUpdate()
	{
		if (runeBoard1_p.Get<GameObject>(0f).activeSelf)
		{
			runeJigsaw.Get<Jigsaw>(0).areaLimits = new Vector2(0.3f, 0.55f);
		}
		else
		{
			runeJigsaw.Get<Jigsaw>(0).areaLimits = new Vector2(0.55f, 0.45f);
		}
	}

	[DebugButton(null, Tint.Default, PostClickAction.HideButton, 0, new object[] { })]
	private void getAllRunes()
	{
		foreach (Ref<Item, MaterialState, GameObject> cannonRune in cannonRunes)
		{
			game.addItemToInventory(cannonRune.Get<Item>(0).gameObject);
		}
	}

	[DebugButton(null, Tint.Default, PostClickAction.HideButton, 0, new object[] { 0, 1, 2 })]
	private void debugSolveKraken(int index)
	{
		checkCorrectCannonRunes(index, solve: true);
		game.startTimer(new CannonThrowBallTimer(index, 0), 1f);
	}

	private void onCannonThrowBallTimerDone(CannonThrowBallTimer timer)
	{
		if (timer.index == 0)
		{
			int cannonIndex = timer.cannonIndex;
			KrakenCannon krakenCannon = krakenCannons[cannonIndex];
			Slot slot = krakenCannon.cannonSlot;
			if (slot.insertedItem != null)
			{
				slot.insertedItem.gameObject.SetActive(value: false);
				slot.insertedItem.slot = null;
				slot.insertedItem = null;
			}
			krakenCannon.cannonEffect.Play();
			((cannonIndex == 1) ? cannonRecoil2 : cannonRecoil1).transitionTo("NewState");
			krakenCannon.fireCannonSeq.setTime(0f);
			krakenCannon.cannonBallEffect.SetActive(value: true);
			krakenCannon.fireCannonSeq.play(-1f, -1f, 2f);
			krakenCannon.cannonEffect.Play();
			PineFmod.playOneShotSoundAttached("event:/Sound Effects/04 Items/Weapons/Cannon/Cannon_Fire", krakenCannon.cannonFirePoint);
			if (cannonIndex != 2 || krakenCannonRunesSolved[cannonIndex])
			{
				game.startTimer(new CannonThrowBallTimer(cannonIndex, 1), 0.55f);
			}
			else
			{
				game.startTimer(new CannonThrowBallTimer(cannonIndex, 2), 2.5f);
			}
		}
		else if (timer.index == 1)
		{
			if (timer.cannonIndex == 0)
			{
				game.callRPC(RPCType.SolveCannon1);
			}
			else if (timer.cannonIndex == 1)
			{
				game.callRPC(RPCType.SolveCannon2);
			}
			else if (timer.cannonIndex == 2)
			{
				game.callRPC(RPCType.SolveCannon3);
			}
		}
		else if (timer.index == 2)
		{
			krakenCannons[2].cannonBallEffect.SetActive(value: false);
			krakenCannons[2].cannonLid.transitionTo("NewState");
			krakenCannons[2].cannonSlot.targetable = true;
		}
	}

	[DebugButton(null, Tint.Default, PostClickAction.HideButton, 0, new object[] { })]
	private void debugGetSwords()
	{
		Item[] array = swords;
		foreach (Item item in array)
		{
			if (!game.isInInventory(item.gameObject) && item.slot == null)
			{
				game.addItemToInventory(item.gameObject);
			}
		}
	}

	private bool checkSwordBarrel()
	{
		bool flag = true;
		for (int i = 0; i < swordSlots.Length; i += 3)
		{
			Slot slot = swordSlots[i].Get<Slot>(0);
			Slot slot2 = swordSlots[i + 1].Get<Slot>(0);
			Slot slot3 = swordSlots[i + 2].Get<Slot>(0);
			if (slot.insertedItem != null || slot2.insertedItem != null || slot3.insertedItem != null)
			{
				slot.gameObject.SetActive(slot.insertedItem != null);
				slot2.gameObject.SetActive(slot2.insertedItem != null);
				slot3.gameObject.SetActive(slot3.insertedItem != null);
			}
			else
			{
				slot.gameObject.SetActive(value: true);
				slot2.gameObject.SetActive(value: true);
				slot3.gameObject.SetActive(value: true);
			}
			if (flag && !checkSlotIsCorrect(slot))
			{
				flag = false;
			}
			if (flag && !checkSlotIsCorrect(slot2))
			{
				flag = false;
			}
			if (flag && !checkSlotIsCorrect(slot3))
			{
				flag = false;
			}
		}
		return flag;
		static bool checkSlotIsCorrect(Slot slot4)
		{
			if (slot4.insertedItem != null && slot4.acceptItems.Length != 0)
			{
				return true;
			}
			if (slot4.insertedItem == null && slot4.rejectItems.Length != 0)
			{
				return true;
			}
			return false;
		}
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void solveSwordBarrel()
	{
		game.startTimer(new BarrelAnimTimer(), 0.5f);
		foreach (Ref<Slot, TweenState> swordSlot in swordSlots)
		{
			swordSlot.Get<Slot>(0).targetable = false;
			if (swordSlot.Get<Slot>(0).insertedItem != null)
			{
				swordSlot.Get<Slot>(0).insertedItem.targetable = false;
			}
		}
		barrelTurnable.targetable = false;
		game.finishPuzzle(Puzzle.SwordBarrel);
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void solveKnotsChest()
	{
		knotsLockZoom.Get<Zoomable>().targetable = false;
		Turnable[] array = knotsTurnables;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = false;
		}
		TeeJoint_003.transitionTo("NewState");
		game.increaseZoomCounter(knotsLockZoom.Get<Zoomable>().gameObject);
		game.finishPuzzle(Puzzle.FlagsChest);
	}

	private bool checkLanternSolution()
	{
		Switch3D[] array = lanternButtons;
		foreach (Switch3D button in array)
		{
			if (button.state == Switch3DState.On && !Array.Exists(lanternSolution, (Switch3D x) => x == button))
			{
				return false;
			}
			if (button.state == Switch3DState.Off && Array.Exists(lanternSolution, (Switch3D x) => x == button))
			{
				return false;
			}
		}
		return true;
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void solveLantern()
	{
		Switch3D[] array = lanternButtons;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = false;
		}
		lanternZoom.targetable = false;
		lanternLockTween.transitionTo("NewState");
	}

	private void initCombatDummies()
	{
		for (int i = 0; i < combatLeftHands.Length; i++)
		{
			combatDummies.Add(new CombatDummy
			{
				rightHandSlider = combatRightHandsSlider[i],
				leftHandSlider = combatLeftHandsSlider[i],
				leftHand = combatLeftHands[i],
				rightHand = combatRightHands[i],
				solution = new List<int>(CombatBoxSolutions[i])
			});
			combatDummiesCurrentValues.Add(new List<int>
			{
				combatRightHandsSlider[i].closestSnapPointIndex,
				combatLeftHandsSlider[i].closestSnapPointIndex
			});
			combatLeftHands[i].setWeight("NewState", combatLeftHandsSlider[i].value);
			combatRightHands[i].setWeight("NewState", combatRightHandsSlider[i].value);
		}
		captainKeys[3].targetable = false;
	}

	private void onCombatSlidables(Slidable slidable, MoveEvent moveEvent)
	{
		for (int i = 0; i < combatDummies.Count; i++)
		{
			CombatDummy combatDummy = combatDummies[i];
			if (!(slidable == combatDummy.leftHandSlider) && !(slidable == combatDummy.rightHandSlider))
			{
				continue;
			}
			if (slidable == combatDummy.leftHandSlider)
			{
				combatDummy.leftHand.setWeight("NewState", slidable.value);
				combatDummiesCurrentValues[i][1] = slidable.closestSnapPointIndex;
			}
			else
			{
				combatDummy.rightHand.setWeight("NewState", slidable.value);
				combatDummiesCurrentValues[i][0] = slidable.closestSnapPointIndex;
			}
			if (moveEvent == MoveEvent.Snapped)
			{
				Debug.Log($"solution is: ({combatDummiesCurrentValues[i][0]},{combatDummiesCurrentValues[i][1]}), should be ({combatDummy.solution[0]},{combatDummy.solution[1]})");
				if (checkCombatBoxSolution())
				{
					game.callRPC(RPCType.CombatChest);
				}
			}
		}
	}

	private bool checkCombatBoxSolution()
	{
		for (int i = 0; i < combatDummies.Count; i++)
		{
			if (!checkSolution(i))
			{
				return false;
			}
		}
		return true;
		bool checkSolution(int dummyIndex)
		{
			CombatDummy combatDummy = combatDummies[dummyIndex];
			for (int j = 0; j < combatDummy.solution.Count; j++)
			{
				if (combatDummy.solution[j] != combatDummiesCurrentValues[dummyIndex][j])
				{
					return false;
				}
			}
			return true;
		}
	}

	[DebugButton(null, Tint.Default, PostClickAction.HideButton, 0, new object[] { })]
	private void getCombatBox()
	{
		game.addItemToInventory(combatBox.gameObject);
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void solveCombatBox()
	{
		foreach (CombatDummy combatDummy in combatDummies)
		{
			combatDummy.rightHandSlider.targetable = false;
			combatDummy.leftHandSlider.targetable = false;
		}
		game.startTimer(new SolveCombatBoxTimer(), 0.5f);
		game.changeExamineRotation(combatBox.gameObject, new Vector2(90f, 60f), 0.5f);
		game.finishPuzzle(Puzzle.Swordfighting);
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void solveLimperChest()
	{
		limperChestLockZoom.Get<Zoomable>().targetable = false;
		Turnable[] array = limperChestTurnables;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = false;
		}
		limperChestLockTween.transitionTo("NewState");
		game.finishPuzzle(Puzzle.LireChest);
	}

	[DebugButton(null, Tint.Default, PostClickAction.HideButton, 0, new object[] { })]
	private void getAllKeys()
	{
		Item[] array = captainKeys;
		foreach (Item item in array)
		{
			game.addItemToInventory(item.gameObject);
		}
	}

	private void initLandmarks()
	{
		currentLandmarkTarget = PathLandmark.Buoys;
		shipParentStartRotation = shipParent.localRotation;
		landmarks = new List<PathLandmark>();
		landmarks.Add(PathLandmark.Buoys);
		landmarks.Add(PathLandmark.Storm);
		landmarks.Add(PathLandmark.North);
		landmarks.Add(PathLandmark.Glow);
		landmarks.Add(PathLandmark.Bell);
		landmarks.Add(PathLandmark.FollowGhost);
	}

	private void onSailsSwitch(Switch3DEvent switchEvent)
	{
		if (switchEvent == Switch3DEvent.On)
		{
			shipMovingEffect.Play();
		}
		else
		{
			shipMovingEffect.Stop();
		}
		if (isStormSolved)
		{
			isStormSolved = false;
			if (storm.Get<ParticleSystem>(0f).IsAlive())
			{
				storm.Get<ParticleSystem>(0f).Stop();
			}
		}
	}

	private bool landmarkCondition(PathLandmark landmark)
	{
		return landmark switch
		{
			PathLandmark.Buoys => false, 
			PathLandmark.Storm => stormCondition(), 
			PathLandmark.North => false, 
			PathLandmark.Glow => glowCondition(), 
			PathLandmark.Bell => false, 
			PathLandmark.FollowGhost => false, 
			_ => false, 
		};
	}

	private void landmarkSolved(PathLandmark landmark)
	{
		switch (landmark)
		{
		case PathLandmark.Storm:
			stormSolved();
			break;
		case PathLandmark.Glow:
			glowSolved();
			break;
		case PathLandmark.Buoys:
		case PathLandmark.North:
		case PathLandmark.Bell:
		case PathLandmark.FollowGhost:
		case PathLandmark.Island:
			break;
		}
	}

	private bool buoysCondition()
	{
		return landmarksTimer >= 5f;
	}

	private void buoysSolved()
	{
		currentLandmarkTarget = PathLandmark.Storm;
		landmarksTimer = 0f;
		storm.Get<GameObject>(0).SetActive(value: true);
		storm.Get<ParticleSystem>(0f).Play();
		Debug.Log("Buoys Solved!");
	}

	private bool stormCondition()
	{
		return true;
	}

	private void stormSolved()
	{
		currentLandmarkTarget = PathLandmark.North;
		landmarksTimer = 0f;
		isStormSolved = true;
		Debug.Log("Storm Solved!");
	}

	private bool northCondition()
	{
		return landmarksTimer >= 5f;
	}

	private void northSolved()
	{
		currentLandmarkTarget = PathLandmark.Glow;
		landmarksTimer = 0f;
		seaGlow.Play();
		Debug.Log("North Solved!");
	}

	private bool glowCondition()
	{
		return true;
	}

	private void glowSolved()
	{
		currentLandmarkTarget = PathLandmark.Bell;
		landmarksTimer = 0f;
		isGlowSolved = true;
		Debug.Log("Glow Solved!");
	}

	private bool bellCondition()
	{
		return bellRings == 3;
	}

	[DebugButton(null, Tint.Default, (PostClickAction)0, 0, new object[] { }, postClickAction = PostClickAction.HideButton)]
	private void debugSolveBell()
	{
		bellSolved();
	}

	private void bellSolved()
	{
		currentLandmarkTarget = PathLandmark.FollowGhost;
		landmarksTimer = 0f;
		ghostShip.Get<GameObject>(0).SetActive(value: true);
		game.setParent(ghostShip.Get<Transform>(0f), landmarksPivot);
		Debug.Log("Bell Solved!");
		ghostShip.Get<TweenState>(0u).transitionTo("NewState", 0.14f, 0.89f);
		PineFmod.playOneShotSound("event:/Sound Effects/05 Misc/Water/Big_Water_Splash 2");
		ghostShipSplash.Play();
		overrideSails = true;
		sailsSwitch.targetable = false;
		if (sailsSwitch.state == Switch3DState.Off)
		{
			game.startSwitch(sailsSwitch);
		}
		sailsSwitchMaterialState.transitionToDuration("Dissolve", 2f);
		game.startTimer(new KrakenCinematicTimer(3, 0), 0.1f);
		game.startTimer(new GhostShipTimer(), 4f);
	}

	private bool ghostShipCondition()
	{
		return landmarksTimer > 6f;
	}

	[DebugButton(null, Tint.Default, PostClickAction.HideButton, 0, new object[] { })]
	private void ghostShipSolved()
	{
		currentLandmarkTarget = PathLandmark.Island;
		landmarksTimer = 0f;
		game.startTimer(new ShowIslandTimer(), 1f);
		game.startTimer(new HideFinalFogTimer(), 4f);
		finalFog.Play();
		Debug.Log("Ghost Ship Solved!");
	}

	private bool checkCaptainsTableSolution()
	{
		Slot[] array = keySlots;
		foreach (Slot slot in array)
		{
			Slot slot2 = slot;
			if (slot2.insertedItem != null && !Array.Exists(correctKeySlots, (Slot x) => x == slot2))
			{
				return false;
			}
			if (slot2.insertedItem == null && Array.Exists(correctKeySlots, (Slot x) => x == slot2))
			{
				return false;
			}
		}
		return true;
	}

	[DebugButton(null, Tint.Default, PostClickAction.HideButton, 0, new object[] { })]
	private void solveCaptainsTable()
	{
		capTableLid.transitionTo("Down", 1f, 1f, playSound: true, 0.7f);
		Slot[] array = keySlots;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = false;
		}
		for (int j = 0; j < correctKeySlots.Length; j++)
		{
			correctKeySlots[j].insertedItem.targetable = false;
			correctKeyTs[j].transitionTo("NewState", 1.5f);
		}
		game.finishPuzzle(Puzzle.CaptainsTable);
	}

	public override void onInitHints()
	{
		game.setPuzzleConditions(Puzzle.RuneKey, Puzzle.RopeLinesChest);
		game.setPuzzleConditions(Puzzle.RuneChest, Puzzle.RuneKey);
		game.setPuzzleConditions(Puzzle.Cannon1, Puzzle.LireChest, Puzzle.RuneChest);
		game.setPuzzleConditions(Puzzle.SwordBarrel, Puzzle.Cannon1);
		game.setPuzzleConditions(Puzzle.FlagsChest, Puzzle.Cannon1);
		game.setPuzzleConditions(Puzzle.Cannon2, Puzzle.SwordBarrel, Puzzle.FlagsChest);
		game.setPuzzleConditions(Puzzle.Lanterns, Puzzle.Cannon2);
		game.setPuzzleConditions(Puzzle.Swordfighting, Puzzle.Cannon2);
		game.setPuzzleConditions(Puzzle.CaptainsTable, Puzzle.Lanterns, Puzzle.Swordfighting);
		game.setPuzzleConditions(Puzzle.Cannon3, Puzzle.CaptainsTable);
		game.setPuzzleConditions(Puzzle.Sailing, Puzzle.Cannon3);
		game.setRelevantObjectsForPuzzle(Puzzle.LireChest, limperChestLock.Get<Item>(0f).gameObject, lireHint);
		game.setRelevantObjectsForPuzzle(Puzzle.RopeLinesChest, ropeLock.Get<Zoomable>(0f).gameObject, ropeDial.gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.RuneKey, mastKey.gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.RuneChest, mainRopeTurnable.gameObject, graffitiChest.Get<Item>(0f).gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.Cannon1, cannonRunes[0].Get<GameObject>(0u), cannonRunes[1].Get<GameObject>(0u), cannonRunes[2].Get<GameObject>(0u));
		game.setRelevantObjectsForPuzzle(Puzzle.SwordBarrel, swords[0].gameObject, swords[1].gameObject, swords[2].gameObject, swords[3].gameObject, swords[4].gameObject, swords[5].gameObject, swords[6].gameObject, swords[7].gameObject, barrelTurnable.gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.FlagsChest, knotsLock.Get<Item>(0f).gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.Cannon2, cannonRunes[3].Get<GameObject>(0u), cannonRunes[6].Get<GameObject>(0u), cannonRunes[5].Get<GameObject>(0u), cannonRunes[4].Get<GameObject>(0u), runeBoard2_pItem.Get<GameObject>(), cannonShieldItem.Get<GameObject>(0f));
		game.setRelevantObjectsForPuzzle(Puzzle.Lanterns, lanternHint, lanternLock.gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.Swordfighting, swordfightingHints[0], swordfightingHints[1], combatBox.gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.CaptainsTable, captainKeys[0].gameObject, captainKeys[1].gameObject, captainKeys[2].gameObject, captainKeys[3].gameObject, captainKeys[4].gameObject, globe);
		game.setRelevantObjectsForPuzzle(Puzzle.Cannon3, cannonBallEnd_p.Get<GameObject>(0f));
		game.setRelevantObjectsForPuzzle(Puzzle.Sailing, travelHint, wheel.gameObject, sailsSwitch.gameObject);
		game.setHintCondition(Puzzle.RopeLinesChest, RopeLinesChestHint.LookAtChest, () => game.wasLookedAtCurrentPuzzle(ropeLock.Get<Zoomable>(0f).gameObject));
		game.setHintCondition(Puzzle.RuneKey, RuneKeyHint.PickUpKey, () => game.wasAddedToInventoryDuringCurrentPuzzle(mastKey.gameObject));
		game.setHintCondition(Puzzle.RuneChest, RuneChestHint.RotateMast, () => game.wasAddedToInventoryDuringCurrentPuzzle(graffitiChest.Get<Item>(0f).gameObject) || game.wasLookedAtCurrentPuzzle(graffitiChest.Get<Item>(0f).gameObject));
		game.setHintCondition(Puzzle.RuneChest, RuneChestHint.FindChest, () => game.wasAddedToInventoryDuringCurrentPuzzle(graffitiChest.Get<Item>(0f).gameObject) || game.wasLookedAtCurrentPuzzle(graffitiChest.Get<Item>(0f).gameObject));
		game.setHintCondition(Puzzle.Cannon1, Cannon1Hint.PickUpChestRunes, () => game.wasAddedToInventoryDuringCurrentPuzzle(cannonRunes[0].Get<GameObject>(0u)) && game.wasAddedToInventoryDuringCurrentPuzzle(cannonRunes[1].Get<GameObject>(0u)));
		game.setHintCondition(Puzzle.Cannon1, Cannon1Hint.PickUpKnotRune, () => game.wasAddedToInventoryDuringCurrentPuzzle(cannonRunes[2].Get<GameObject>(0u)));
		game.setHintCondition(Puzzle.Cannon2, Cannon2Hint.PickUpChestRunes, () => game.wasAddedToInventoryDuringCurrentPuzzle(cannonRunes[3].Get<GameObject>(0u)) && game.wasAddedToInventoryDuringCurrentPuzzle(cannonRunes[6].Get<GameObject>(0u)) && game.wasAddedToInventoryDuringCurrentPuzzle(cannonRunes[5].Get<GameObject>(0u)));
		game.setHintCondition(Puzzle.Cannon2, Cannon2Hint.PickUpBoard, () => game.wasAddedToInventoryDuringCurrentPuzzle(cannonRunes[4].Get<GameObject>(0u)) && game.wasAddedToInventoryDuringCurrentPuzzle(cannonShieldItem.Get<GameObject>(0f)) && game.wasAddedToInventoryDuringCurrentPuzzle(runeBoard2_pItem.Get<GameObject>()));
		game.setHintCondition(Puzzle.Cannon2, Cannon2Hint.PlaceBoard, () => runeBoard2_p.Get<GameObject>(0f).activeSelf);
		game.setHintCondition(Puzzle.Cannon2, Cannon2Hint.PlaceShield, () => cannonShieldSlot.isUnlocked);
		game.setHintCondition(Puzzle.Lanterns, LanternsHint.PickUpPaper, () => game.wasAddedToInventoryDuringCurrentPuzzle(lanternHint));
		game.setHintCondition(Puzzle.Swordfighting, SwordfightingHint.PickUpNotes, () => game.wasAddedToInventoryDuringCurrentPuzzle(swordfightingHints[0]) && game.wasAddedToInventoryDuringCurrentPuzzle(swordfightingHints[1]) && game.wasAddedToInventoryDuringCurrentPuzzle(combatBox.gameObject));
		game.setHintCondition(Puzzle.CaptainsTable, CaptainsTableHint.PickUpSwordKey, () => game.wasAddedToInventoryDuringCurrentPuzzle(captainKeys[3].gameObject));
		game.setHintCondition(Puzzle.CaptainsTable, CaptainsTableHint.PickUpBoxKey, () => game.wasAddedToInventoryDuringCurrentPuzzle(captainKeys[0].gameObject) && game.wasAddedToInventoryDuringCurrentPuzzle(captainKeys[2].gameObject));
		game.setHintCondition(Puzzle.CaptainsTable, CaptainsTableHint.PickUpCabinetKey, () => game.wasAddedToInventoryDuringCurrentPuzzle(captainKeys[4].gameObject));
		game.setHintCondition(Puzzle.CaptainsTable, CaptainsTableHint.PickUpDrawerKey, () => game.wasAddedToInventoryDuringCurrentPuzzle(captainKeys[1].gameObject));
		game.setHintCondition(Puzzle.Cannon3, Cannon3Hint.PickUpCannonBall, () => game.wasAddedToInventoryDuringCurrentPuzzle(cannonBallEnd_p.Get<GameObject>(0f)));
	}

	public virtual void save(FastBinaryWriter writer)
	{
		writer.Write(in graffitiChestCollected, default(FastBinaryWriter.ForPrimitives));
		writer.WriteArray(graffitiChestCurrent, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.Write(in graffitiChestIndex, default(FastBinaryWriter.ForPrimitives));
		writer.WriteList(krakenCannonRunesSolved, delegate(FastBinaryWriter w, bool e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.Write(in isKrakenDown, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in lanternButtonCounter, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in swayKraken, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in swayKrakenT, default(FastBinaryWriter.ForPrimitives));
		writer.WriteVector3(in krakenPivot);
		writer.WriteList(combatDummiesCurrentValues, delegate(FastBinaryWriter w, List<int> e)
		{
			w.WriteList(e, delegate(FastBinaryWriter fastBinaryWriter, int value2)
			{
				fastBinaryWriter.Write(in value2, default(FastBinaryWriter.ForPrimitives));
			});
		});
		writer.Write(in landmarksPivotRotation, default(FastBinaryWriter.ForPrimitives));
		int value = (int)currentLandmarkTarget;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in landmarksTimer, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in isStormSolved, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in isGlowSolved, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in overrideSails, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in prevSails, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in bellRings, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in shipTargetTilt, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in mainRopeSequenceTime, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in interactedWithPuley, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in compasAngle, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void load(FastBinaryReader reader)
	{
		graffitiChestCollected = reader.ReadBoolean();
		graffitiChestCurrent = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		graffitiChestIndex = reader.ReadInt32();
		krakenCannonRunesSolved = reader.ReadList((FastBinaryReader r) => r.ReadBoolean());
		isKrakenDown = reader.ReadBoolean();
		lanternButtonCounter = reader.ReadInt32();
		swayKraken = reader.ReadBoolean();
		swayKrakenT = reader.ReadSingle();
		krakenPivot = reader.ReadVector3();
		combatDummiesCurrentValues = reader.ReadList((FastBinaryReader r) => r.ReadList((FastBinaryReader fastBinaryReader) => fastBinaryReader.ReadInt32()));
		landmarksPivotRotation = reader.ReadSingle();
		currentLandmarkTarget = (PathLandmark)reader.ReadInt32();
		landmarksTimer = reader.ReadSingle();
		isStormSolved = reader.ReadBoolean();
		isGlowSolved = reader.ReadBoolean();
		overrideSails = reader.ReadBoolean();
		prevSails = reader.ReadBoolean();
		bellRings = reader.ReadInt32();
		shipTargetTilt = reader.ReadSingle();
		mainRopeSequenceTime = reader.ReadSingle();
		interactedWithPuley = reader.ReadBoolean();
		compasAngle = reader.ReadSingle();
	}

	public virtual void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		int position = reader.Position;
		bool flag = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "graffitiChestCollected",
			fieldValue = $"{flag}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int[] array = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "graffitiChestCurrent[" + ((array == null) ? string.Empty : array.Length.ToString()) + "]",
			fieldValue = (((array == null) ? "null" : string.Join(", ", array)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "graffitiChestIndex",
			fieldValue = $"{num}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<bool> list = reader.ReadList((FastBinaryReader r) => r.ReadBoolean());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "krakenCannonRunesSolved[" + ((list == null) ? string.Empty : list.Count.ToString()) + "]",
			fieldValue = (((list == null) ? "null" : string.Join(", ", list)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag2 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "isKrakenDown",
			fieldValue = $"{flag2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num2 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "lanternButtonCounter",
			fieldValue = $"{num2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag3 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "swayKraken",
			fieldValue = $"{flag3}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num3 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "swayKrakenT",
			fieldValue = $"{num3}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Vector3 vector = reader.ReadVector3();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "krakenPivot",
			fieldValue = $"{vector}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<List<int>> list2 = reader.ReadList((FastBinaryReader r) => r.ReadList((FastBinaryReader fastBinaryReader) => fastBinaryReader.ReadInt32()));
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "combatDummiesCurrentValues[" + ((list2 == null) ? string.Empty : list2.Count.ToString()) + "]",
			fieldValue = (((list2 == null) ? "null" : string.Join(", ", list2)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num4 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "landmarksPivotRotation",
			fieldValue = $"{num4}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		PathLandmark pathLandmark = (PathLandmark)reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "currentLandmarkTarget",
			fieldValue = $"{pathLandmark}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num5 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "landmarksTimer",
			fieldValue = $"{num5}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag4 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "isStormSolved",
			fieldValue = $"{flag4}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag5 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "isGlowSolved",
			fieldValue = $"{flag5}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag6 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "overrideSails",
			fieldValue = $"{flag6}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag7 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "prevSails",
			fieldValue = $"{flag7}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num6 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "bellRings",
			fieldValue = $"{num6}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num7 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "shipTargetTilt",
			fieldValue = $"{num7}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num8 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "mainRopeSequenceTime",
			fieldValue = $"{num8}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag8 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "interactedWithPuley",
			fieldValue = $"{flag8}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num9 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "compasAngle",
			fieldValue = $"{num9}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
	}

	public override Timer getTimer(byte id)
	{
		return id switch
		{
			0 => new BoardDissolveTimer(), 
			1 => new LanternButtonsResetTimer(), 
			2 => new ShowIslandTimer(), 
			3 => new ShakeScreenTimer(), 
			4 => new HideFinalFogTimer(), 
			5 => new FinishLevelTimer(), 
			6 => new UnlockGraffitiWheelTimer(), 
			7 => new CannonFlipFireSwitchTimer(), 
			8 => new CannonThrowBallTimer(), 
			9 => new CannonStartAnimTimer(), 
			10 => new KrakenHitTimer(), 
			11 => new SolveCombatBoxTimer(), 
			12 => new knotsLockDelayTimer(), 
			13 => new ropeLockDelayTimer(), 
			14 => new BarrelAnimTimer(), 
			15 => new BarrelAnim2Timer(), 
			16 => new KrakenCinematicTimer(), 
			17 => new GhostShipTimer(), 
			18 => new MastCutsceneTimer(), 
			_ => null, 
		};
	}
}
