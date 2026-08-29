using System;
using System.Collections.Generic;
using System.Text;
using FMOD;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.VFX;

public class Dracula1Logic : LevelLogic, ISaveable
{
	public sealed class FireChestPaperTimer : Timer
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

	public sealed class CrowMirrorTimer : Timer
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

	public sealed class StarBoxTimer : Timer
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

	public sealed class StatueSolvedTimer : Timer
	{
		public int statueIndex;

		public override byte getTypeId()
		{
			return 3;
		}

		public StatueSolvedTimer()
		{
		}

		public StatueSolvedTimer(int statueIndex)
		{
			this.statueIndex = statueIndex;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in statueIndex, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			statueIndex = reader.ReadInt32();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("statueIndex: " + $"{statueIndex}");
			return stringBuilder.ToString();
		}
	}

	public sealed class SymbolPointerCorrectTimer : Timer
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

	public sealed class SymbolPointerNotCorrectTimer : Timer
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

	public sealed class Well1Timer : Timer
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

	public sealed class Well2_1Timer : Timer
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

	public sealed class Well2_2Timer : Timer
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

	public sealed class Well3Timer : Timer
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

	public sealed class WellSolveTimer : Timer
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

	public sealed class CarriageDoorSolvedTimer : Timer
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

	public sealed class FinalDoorTimer : Timer
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

	public sealed class DirtEffectTimer : Timer
	{
		public Transform effectTransform;

		public VisualEffect effectVisuals;

		public Transform holeTransform;

		public override byte getTypeId()
		{
			return 13;
		}

		public DirtEffectTimer()
		{
		}

		public DirtEffectTimer(Transform effectTransform, VisualEffect effectVisuals, Transform holeTransform)
		{
			this.effectTransform = effectTransform;
			this.effectVisuals = effectVisuals;
			this.holeTransform = holeTransform;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.WriteComponent(effectTransform);
			writer.WriteComponent(effectVisuals);
			writer.WriteComponent(holeTransform);
		}

		public override void readData(FastBinaryReader reader)
		{
			effectTransform = reader.ReadComponent<Transform>();
			effectVisuals = reader.ReadComponent<VisualEffect>();
			holeTransform = reader.ReadComponent<Transform>();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("effectTransform: " + $"{effectTransform}");
			stringBuilder.AppendLine("effectVisuals: " + $"{effectVisuals}");
			stringBuilder.Append("holeTransform: " + $"{holeTransform}");
			return stringBuilder.ToString();
		}
	}

	public sealed class HoleEffectTimer : Timer
	{
		public int effectIndex;

		public Item shovel;

		public override byte getTypeId()
		{
			return 14;
		}

		public HoleEffectTimer()
		{
		}

		public HoleEffectTimer(int effectIndex, Item shovel)
		{
			this.effectIndex = effectIndex;
			this.shovel = shovel;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in effectIndex, default(FastBinaryWriter.ForPrimitives));
			writer.WriteComponent(shovel);
		}

		public override void readData(FastBinaryReader reader)
		{
			effectIndex = reader.ReadInt32();
			shovel = reader.ReadComponent<Item>();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("effectIndex: " + $"{effectIndex}");
			stringBuilder.Append("shovel: " + $"{shovel}");
			return stringBuilder.ToString();
		}
	}

	public sealed class HoleEffect2Timer : Timer
	{
		public GameObject effect;

		public DecalProjector decal;

		public override byte getTypeId()
		{
			return 15;
		}

		public HoleEffect2Timer()
		{
		}

		public HoleEffect2Timer(GameObject effect, DecalProjector decal)
		{
			this.effect = effect;
			this.decal = decal;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.WriteGameObject(effect);
			writer.WriteComponent(decal);
		}

		public override void readData(FastBinaryReader reader)
		{
			effect = reader.ReadGameObject();
			decal = reader.ReadComponent<DecalProjector>();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("effect: " + $"{effect}");
			stringBuilder.Append("decal: " + $"{decal}");
			return stringBuilder.ToString();
		}
	}

	public sealed class DecalFadeTimer : Timer
	{
		public DecalProjector decalProjector;

		public override byte getTypeId()
		{
			return 16;
		}

		public DecalFadeTimer()
		{
		}

		public DecalFadeTimer(DecalProjector decalProjector)
		{
			this.decalProjector = decalProjector;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.WriteComponent(decalProjector);
		}

		public override void readData(FastBinaryReader reader)
		{
			decalProjector = reader.ReadComponent<DecalProjector>();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("decalProjector: " + $"{decalProjector}");
			return stringBuilder.ToString();
		}
	}

	public sealed class CarriageDoorAnimationTimer : Timer
	{
		public float y;

		public Quaternion[] initialRotation;

		public override byte getTypeId()
		{
			return 17;
		}

		public CarriageDoorAnimationTimer()
		{
		}

		public CarriageDoorAnimationTimer(float y, Quaternion[] initialRotation)
		{
			this.y = y;
			this.initialRotation = initialRotation;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in y, default(FastBinaryWriter.ForPrimitives));
			writer.WriteArray(initialRotation, delegate(FastBinaryWriter w, Quaternion e)
			{
				w.WriteQuaternion(in e);
			});
		}

		public override void readData(FastBinaryReader reader)
		{
			y = reader.ReadSingle();
			initialRotation = reader.ReadArray((FastBinaryReader r) => r.ReadQuaternion());
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("y: " + $"{y}");
			stringBuilder.Append("initialRotation: " + ToStringHelper.Stringify(initialRotation, (Quaternion e) => $"{e}"));
			return stringBuilder.ToString();
		}
	}

	public sealed class DecalFadeOutTimer : Timer
	{
		public DecalProjector decalProjector;

		public override byte getTypeId()
		{
			return 18;
		}

		public DecalFadeOutTimer()
		{
		}

		public DecalFadeOutTimer(DecalProjector decalProjector)
		{
			this.decalProjector = decalProjector;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.WriteComponent(decalProjector);
		}

		public override void readData(FastBinaryReader reader)
		{
			decalProjector = reader.ReadComponent<DecalProjector>();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("decalProjector: " + $"{decalProjector}");
			return stringBuilder.ToString();
		}
	}

	public sealed class ExitDoorGearsTimer : Timer
	{
		public override byte getTypeId()
		{
			return 19;
		}

		public override void writeData(FastBinaryWriter writer)
		{
		}

		public override void readData(FastBinaryReader reader)
		{
		}
	}

	public sealed class ExitDoorGearCorrectSlotTimer : Timer
	{
		public int gearIndex;

		public int direction;

		public override byte getTypeId()
		{
			return 20;
		}

		public ExitDoorGearCorrectSlotTimer()
		{
		}

		public ExitDoorGearCorrectSlotTimer(int gearIndex, int direction)
		{
			this.gearIndex = gearIndex;
			this.direction = direction;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in gearIndex, default(FastBinaryWriter.ForPrimitives));
			writer.Write(in direction, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			gearIndex = reader.ReadInt32();
			direction = reader.ReadInt32();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("gearIndex: " + $"{gearIndex}");
			stringBuilder.Append("direction: " + $"{direction}");
			return stringBuilder.ToString();
		}
	}

	public sealed class SymbolsGearTransition : Transition
	{
		public override byte getTypeId()
		{
			return 21;
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

	public sealed class FireTransition : Transition
	{
		public override byte getTypeId()
		{
			return 22;
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

	public sealed class FireTimer : Timer
	{
		public override byte getTypeId()
		{
			return 23;
		}

		public override void writeData(FastBinaryWriter writer)
		{
		}

		public override void readData(FastBinaryReader reader)
		{
		}
	}

	public sealed class StatueRotationTransition : Transition
	{
		public override byte getTypeId()
		{
			return 24;
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

	public sealed class SymbolsPointerTransition : Transition
	{
		public int currentSymbol;

		public override byte getTypeId()
		{
			return 25;
		}

		public SymbolsPointerTransition()
		{
		}

		public SymbolsPointerTransition(int currentSymbol)
		{
			this.currentSymbol = currentSymbol;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			base.writeData(writer);
			writer.Write(in currentSymbol, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			base.readData(reader);
			currentSymbol = reader.ReadInt32();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("currentSymbol: " + $"{currentSymbol}");
			return stringBuilder.ToString();
		}
	}

	public sealed class ExitDoorGearsTransition : Transition
	{
		public override byte getTypeId()
		{
			return 26;
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

	public sealed class ChestOpenTransition : Transition
	{
		public override byte getTypeId()
		{
			return 27;
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

	public sealed class DigUpItemTransition : Transition
	{
		public override byte getTypeId()
		{
			return 28;
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

	public sealed class ShovelFlyInTransition : Transition
	{
		public int shovelIndex;

		public Item shovel;

		public Vector3 position;

		public override byte getTypeId()
		{
			return 29;
		}

		public ShovelFlyInTransition()
		{
		}

		public ShovelFlyInTransition(int shovelIndex, Item shovel, Vector3 position)
		{
			this.shovelIndex = shovelIndex;
			this.shovel = shovel;
			this.position = position;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			base.writeData(writer);
			writer.Write(in shovelIndex, default(FastBinaryWriter.ForPrimitives));
			writer.WriteComponent(shovel);
			writer.WriteVector3(in position);
		}

		public override void readData(FastBinaryReader reader)
		{
			base.readData(reader);
			shovelIndex = reader.ReadInt32();
			shovel = reader.ReadComponent<Item>();
			position = reader.ReadVector3();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("shovelIndex: " + $"{shovelIndex}");
			stringBuilder.AppendLine("shovel: " + $"{shovel}");
			stringBuilder.Append("position: " + $"{position}");
			return stringBuilder.ToString();
		}
	}

	public sealed class ShovelFlyBackTimer : Timer
	{
		public int shovelIndex;

		public Item shovel;

		public Vector3 shovelStartDigingPos;

		public Quaternion shovelStartDigingRot;

		public Vector3 shovelStartDigingScale;

		public override byte getTypeId()
		{
			return 30;
		}

		public ShovelFlyBackTimer()
		{
		}

		public ShovelFlyBackTimer(int shovelIndex, Item shovel, Vector3 shovelStartDigingPos, Quaternion shovelStartDigingRot, Vector3 shovelStartDigingScale)
		{
			this.shovelIndex = shovelIndex;
			this.shovel = shovel;
			this.shovelStartDigingPos = shovelStartDigingPos;
			this.shovelStartDigingRot = shovelStartDigingRot;
			this.shovelStartDigingScale = shovelStartDigingScale;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in shovelIndex, default(FastBinaryWriter.ForPrimitives));
			writer.WriteComponent(shovel);
			writer.WriteVector3(in shovelStartDigingPos);
			writer.WriteQuaternion(in shovelStartDigingRot);
			writer.WriteVector3(in shovelStartDigingScale);
		}

		public override void readData(FastBinaryReader reader)
		{
			shovelIndex = reader.ReadInt32();
			shovel = reader.ReadComponent<Item>();
			shovelStartDigingPos = reader.ReadVector3();
			shovelStartDigingRot = reader.ReadQuaternion();
			shovelStartDigingScale = reader.ReadVector3();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("shovelIndex: " + $"{shovelIndex}");
			stringBuilder.AppendLine("shovel: " + $"{shovel}");
			stringBuilder.AppendLine("shovelStartDigingPos: " + $"{shovelStartDigingPos}");
			stringBuilder.AppendLine("shovelStartDigingRot: " + $"{shovelStartDigingRot}");
			stringBuilder.Append("shovelStartDigingScale: " + $"{shovelStartDigingScale}");
			return stringBuilder.ToString();
		}
	}

	[Serializable]
	public class StatueSpikes
	{
		[DontSave]
		public RefArray<MaterialState, Transform> spikes;

		[DontSave]
		public Transform[] retractedPositionsRefs;

		[DontSave]
		public Transform[] correctPositionsRefs;

		[NonSerialized]
		[DontSave]
		public Vector3[] originalPositions;
	}

	[Serializable]
	public class DiggableItem
	{
		[Tooltip("Object that can be dug up.")]
		[DontSave]
		public Ref<GameObject, Transform> gameObjectRef;

		[Tooltip("Defines minimum required distance between player's shovel attempt and this item that will trigger the dig event.")]
		public float digRadius = 2f;

		[Tooltip("How much should this item be raised up from its initial position when dug up.")]
		public float digRaiseDistance = 2f;
	}

	private enum FireMovement
	{
		StayUp = 0,
		StayCenter = 1,
		StayDown = 2,
		MoveUp = 3,
		MoveDown = 4
	}

	private enum FirePosition
	{
		Down = 0,
		Center = 1,
		Up = 2
	}

	private enum FireState
	{
		Moving = 0,
		LetterPause = 1
	}

	[Serializable]
	public enum MovementType
	{
		Walk = 0,
		Run = 1,
		Jump = 2,
		Sit = 3
	}

	[Serializable]
	public class AnimationPoint
	{
		public GameObject worldWaypoint;

		public Transform worldWaypointRef;

		public MovementType type;

		public float duration;
	}

	private enum LevelPredicate
	{
		Statue1Solved = 0,
		Statue2Solved = 1,
		StatuesSolved = 2,
		CarriageDoor = 3,
		Liars = 4,
		VanHelsing = 5,
		MiniStatues = 6,
		ExitDoor = 7,
		WellSliders = 8,
		StarSlidableTargetable = 9
	}

	private enum RPC
	{
		ResetLiars = 0
	}

	private enum Puzzle
	{
		[PuzzleInfo("%PuzzleD1_1%", true)]
		Carriage = 0,
		[PuzzleInfo("%PuzzleD1_2%", false)]
		Gargoyles = 1,
		[PuzzleInfo("%PuzzleD1_3%", false)]
		Flames = 2,
		[PuzzleInfo("%PuzzleD1_4%", false)]
		SymbolChest = 3,
		[PuzzleInfo("%PuzzleD1_5%", false)]
		BlueFlame = 4,
		[PuzzleInfo("%PuzzleD1_6%", false)]
		StarChest = 5,
		[PuzzleInfo("%PuzzleD1_7%", false)]
		Statues = 6,
		[PuzzleInfo("%PuzzleD1_8%", false)]
		HeavenHell = 7,
		[PuzzleInfo("%PuzzleD1_9%", true)]
		MirrorChest = 8,
		[PuzzleInfo("%PuzzleD1_10%", false)]
		Mirror = 9,
		[PuzzleInfo("%PuzzleD1_11%", true)]
		Whisp = 10,
		[PuzzleInfo("%PuzzleD1_12%", true)]
		Bloodmoon = 11
	}

	private enum CarriageHint
	{
		CleanWindow = 0,
		LookAtSymbols = 1,
		LookAtGraph = 2,
		MovePieces = 3,
		Solve = 4
	}

	private enum GargoylesHint
	{
		LookAtHint = 0,
		LookAtLiars = 1,
		LookAtLock = 2,
		MatchSymbols = 3,
		SolveFirst = 4,
		SolveOthers = 5
	}

	private enum FlamesHint
	{
		GetHint = 0,
		MatchFireToLetter = 1,
		OpenTurnables = 2,
		Solve = 3
	}

	private enum SymbolChestHint
	{
		GetBox = 0,
		LookAtSlidables = 1,
		FirstSlidableSymbol = 2,
		LookAtNumber = 3,
		SolveFirst = 4,
		SolveOthers = 5
	}

	private enum BlueFlameHint
	{
		GetShovel = 0,
		GetHint = 1,
		LookAtLanterns = 2,
		LookTroughLantern = 3,
		DigStar = 4,
		Solve = 5
	}

	private enum StarChestHint
	{
		UnlockStar = 0,
		GetBox = 1,
		PlaceStar = 2,
		Solve = 3
	}

	private enum StatuesHint
	{
		GetItems = 0,
		PlaceItems = 1,
		TurnDial = 2,
		SolveFirst = 3,
		SolveOther = 4
	}

	private enum HeavenHellHint
	{
		GetStatues = 0,
		GetKey = 1,
		UnlockMusoleum = 2,
		LookAtText = 3,
		LookAtStructure = 4,
		SolveAngel = 5,
		SolveDevil = 6
	}

	private enum MirrorChestHint
	{
		GetKeyAndFlask = 0,
		GetHammer = 1,
		GetStake = 2,
		UnlockBox = 3,
		PlaceTools = 4
	}

	private enum MirrorHint
	{
		GetMirror = 0,
		LookAtSlidables = 1,
		EnterCrowMode = 2,
		LookAtSymbol = 3,
		SolveFirst = 4
	}

	private enum WhispHint
	{
		GetBag = 0,
		GetBook = 1,
		LookAtNpc = 2,
		Solve = 3
	}

	private enum BloodmoonHint
	{
		GetWellMoon = 0,
		GetLiarsMoon = 1,
		GetStatuesMoon = 2,
		GetStarBoxMoon = 3,
		PlaceMoons = 4,
		Solve = 5
	}

	[Header("Wall Numbers Lock")]
	[DontSave]
	public Ref<Lock, TweenState, GameObject> wallNumbersChestLockRef;

	[DontSave]
	public Ref<Switch3D, Transform> wallNumbersChestLid;

	[DontSave]
	public AnimationSampler[] wallNumbersChestAnimationSamplers;

	[DontSave]
	public Ref<Slidable, GameObject> wallNumbersSlidableLeftRef;

	[DontSave]
	public Ref<Slidable, GameObject> wallNumbersSlidableRightRef;

	[DontSave]
	public TweenState[] wallNumberRocks;

	[DontSave]
	private readonly int[] SlidableSymbolsNumbersLeft = new int[5] { 0, 3, 4, 8, 5 };

	[DontSave]
	private readonly int[] SlidableSymbolsNumbersRight = new int[5] { 1, 6, 9, 2, 7 };

	private bool symbolSlidablesInteractedWith;

	[Header("Carriage Door")]
	[DontSave]
	public SlidableGraph carriageDoorSlidabeGraph;

	[DontSave]
	private readonly int[] CarriageDoorSlidableGraphCorrectNodes = new int[5] { 5, 4, 1, 6, 2 };

	[DontSave]
	public Ref<GameObject, Zoomable> carriageDoorZoomableRef;

	[DontSave]
	public TweenState carriageDoorTweenState;

	[DontSave]
	public Switch3D carriageDoorSwitch;

	[DontSave]
	public Paintable[] carriagePaintables;

	[DontSave]
	public EventInstance carriageDoorSolvedInstance;

	[DontSave]
	public Ref<Switch3D, Sequence> carriageDoorLockedRef;

	[DontSave]
	public Ref<Switch3D, Sequence> carriageDoorLockedRefSecond;

	[DontSave]
	public Ref<Switch3D, Sequence> mausoleumDoorOne;

	[DontSave]
	public Ref<Switch3D, Sequence> mausoleumDoorTwo;

	[DontSave]
	public Ref<Switch3D, Sequence> smallMauseoleumDoorOne;

	[DontSave]
	public Ref<Switch3D, Sequence> smallMauseoleumDoorTwo;

	[DontSave]
	public Ref<Switch3D, Sequence> mainDoorCircleOne;

	[DontSave]
	public Ref<Switch3D, Sequence> mainDoorCircleTwo;

	private bool carriageDoorOpened;

	[Header("Fire Symbols Lock")]
	[DontSave]
	public Ref<Zoomable, GameObject> fireChestZoomableRef;

	[DontSave]
	public Lock fireChestLock;

	[DontSave]
	public Switch3D fireChestTurnablesSwitch;

	[DontSave]
	public Switch3D fireChestLid;

	[DontSave]
	public TweenState fireChestLockSwitchTS;

	[DontSave]
	public TweenState fireChestLockPartTS;

	[Header("Van Helsing Tools")]
	[DontSave]
	public RefArray<Item, GameObject> vanHelsingTools;

	[DontSave]
	public TweenState[] vanHelsingToolTSs;

	[DontSave]
	public List<Slot> vanHelsingToolSlots;

	[DontSave]
	public AnimationSampler vanHelsingSecretDrawerAnimationSampler;

	[DontSave]
	public Ref<Item, GameObject> vanHelsingLockKeyRef;

	[DontSave]
	public Slot vanHelsingLockSlotWrong;

	[DontSave]
	public Slot vanHelsingLockSlot;

	[DontSave]
	public Item vanHelsingLockItem;

	[DontSave]
	public Switch3D vanHelsingLockLocked;

	[DontSave]
	public TweenState vanHelsingLockTS;

	[DontSave]
	public Switch3D vanHelsingBoxTop;

	[DontSave]
	public TweenState vanHelsingBoxTopHatchTS;

	[DontSave]
	public Switch3D vanHelsingBoxMiscDrawer;

	[Header("Statues")]
	[DontSave]
	public Ref<Item, GameObject> gargoyleDoorKeyRef;

	[DontSave]
	public Slot gargoyleDoorKeySlot;

	[DontSave]
	public Item gargoyleDoorLockItem;

	[DontSave]
	public TweenState gargoyleDoorLockTS;

	[DontSave]
	public Switch3D[] gargoyleDoors;

	[DontSave]
	public Switch3D gargoyleLockLocked;

	[DontSave]
	public Sequence[] statueSequences;

	[DontSave]
	public EventInstance[] statueSoundInstances;

	[DontSave]
	public RefArray<Slot, Transform> statueSlots;

	[DontSave]
	public RefArray<GameObject, Item> statueKeys;

	[DontSave]
	public RefArray<GameObject, Dial, Transform> statueDials;

	[DontSave]
	public TweenState[] statueDialHandleTSs;

	[DontSave]
	public RefArray<Zoomable, GameObject> statueZoomables;

	[DontSave]
	private readonly int[] StatueCorrectKeys = new int[2] { 0, 1 };

	private bool[] StatueSolved = new bool[2];

	private bool[] statueDialWasInteracted = new bool[2];

	[DontSave]
	public StatueSpikes[] statueSpikes;

	private const float SpikeTransitionLength = 0.25f;

	private const float SpikeColorTransitionSpeed = 4f;

	private const float SpikeCorrectDelta = 0.0035f;

	[DontSave]
	public Slot[] miniStatueSlots;

	[DontSave]
	public TweenState[] miniStatueSlotTSs;

	[DontSave]
	public Sequence miniStatuesSequence;

	[DontSave]
	public Item miniStatueGargoyle;

	[DontSave]
	public Item miniStatueAngel;

	[DontSave]
	public EventInstance miniStatueSolvedInstance;

	private bool miniStatuesSolved;

	[Header("Liars")]
	[DontSave]
	public Slidable symbolsSlidable;

	[DontSave]
	public RefArray<GameObject, Transform> symbolsPointerEmpties;

	[DontSave]
	public Ref<GameObject, Transform> symbolsPointerGearRef;

	[DontSave]
	public Ref<GameObject, Transform> symbolsPointerRef;

	[DontSave]
	public Ref<Zoomable, GameObject> symbolsLockZoomableRef;

	[DontSave]
	public Ref<Item, Transform, GameObject> symbolsLockItemRef;

	[DontSave]
	public TweenState symbolsLockTS;

	[DontSave]
	public Switch3D[] symbolsDoors;

	[DontSave]
	public GameObject symbolsHintPaperObject;

	[DontSave]
	private EventInstance symbolsPointerSoundInstance;

	private bool symbolsSolved;

	private int currentSymbol;

	[DontSave]
	private readonly bool[] SymbolsCorrectSolution = new bool[6] { false, false, true, false, false, true };

	private bool[] symbolsCurrentSolution = new bool[6];

	[Header("Crow Mirror")]
	[DontSave]
	public Ref<GameObject, Transform, Animation> crowRef;

	[DontSave]
	public Ref<GameObject, Transform, MaterialState, Item> crowMirrorRef;

	[DontSave]
	private MaterialState crowMirrorMS;

	private bool crowMirrorPickedUp;

	[DontSave]
	public Switch3D crowMirrorSwitch;

	[DontSave]
	public CustomMode crowMode;

	private bool crowModeWasEntered;

	private float crowMirrorDecalFade;

	[DontSave]
	public List<Collider> crowBlockColliders;

	[DontSave]
	public Collider crowCollider;

	[DontSave]
	public float CrowVerticalSpeed = 2.5f;

	private float crowCurrentHeight = 5f;

	private const float CrowHeight = 6f;

	private Vector3 crowLastPosition;

	private bool crowMoving;

	[Header("Exit Door")]
	[DontSave]
	public RefArray<GameObject, Item> exitDoorKeys;

	[DontSave]
	public List<Slot> exitDoorSlots;

	[DontSave]
	public List<TweenState> exitDoorHolderTSs;

	[DontSave]
	public RefArray<GameObject, Transform> exitDoorGears;

	[DontSave]
	public Sequence exitDoorSequence;

	[DontSave]
	public Ref<GameObject, Switch3D> levelExitRef;

	[DontSave]
	private EventInstance[] exitDoorGearsSoundInstances;

	private bool exitDoorSolved;

	[Header("Well")]
	[DontSave]
	public List<HorizontalSlider> wellSliders;

	[DontSave]
	private readonly int[] WellSlidersSolutions = new int[4] { 1, 0, 2, 3 };

	[DontSave]
	public GameObject wellBook;

	[DontSave]
	public GameObject wellBag;

	private int[] wellSlidersCurrent = new int[4] { -1, -1, -1, -1 };

	[DontSave]
	public GameObject wellLid;

	[DontSave]
	public RefArray<Item, Transform> wellLidPieces;

	[DontSave]
	public Zoomable[] wellLockZoomables;

	[DontSave]
	public GameObject wellAmbiance;

	private bool wellSolved;

	[DontSave]
	public float WellSequenceDelay = 0.5f;

	[DontSave]
	public float WellSmokeDelay = 0.025f;

	private bool whispRising;

	[DontSave]
	public float WhispFloatTimOut = 2f;

	private float whispFloatTimer;

	[DontSave]
	public float WhispFloatOffset = -0.1f;

	private Vector3 whispRefPos;

	private bool movingDown;

	private bool wellPiecePickedUp;

	[DontSave]
	public VisualEffect whispAppearVFX;

	[DontSave]
	public MaterialState whispMoonMS;

	[DontSave]
	public TweenState whispMoonMover;

	[DontSave]
	public GameObject whispPushSphere;

	private bool whispMoverMoved = true;

	[Header("Star Box")]
	[DontSave]
	public Item starBox;

	[DontSave]
	public Ref<Item, TweenState> starBoxKeyRef;

	[DontSave]
	public Slidable starSlidable;

	[DontSave]
	public Dial starBoxDial;

	[DontSave]
	public Slot starBoxSlot;

	[DontSave]
	public Slot starBoxSlotWrong;

	[DontSave]
	public Sequence starBoxDialSequence;

	[DontSave]
	public TweenState starBoxDrawerTS;

	private const float StarBoxDrawerOpenTo = 0.2f;

	private float starBoxDrawerPercent;

	[Header("Flames")]
	[DontSave]
	public RefArray<Collider, Transform> flameDecalBoxes;

	[DontSave]
	public List<Collider> flameLanternBoxes;

	[DontSave]
	public List<ParticleSystem> flames;

	[DontSave]
	public Collider[] flameBlockingColliders;

	[DontSave]
	public GameObject flamePaperHint;

	[Header("Shovel digging settings")]
	[DontSave]
	public RefArray<Transform, GameObject, AnimationSampler> shovelPool;

	[DontSave]
	public Transform[] shovelAnimationMoving;

	[DontSave]
	public RefArray<GameObject, Transform, TweenState> dirtHolePool;

	[DontSave]
	public RefArray<VisualEffect, GameObject, Transform> dirtEffectPool;

	[DontSave]
	public RefArray<DecalProjector, GameObject> dirtDecalPool;

	[DontSave]
	public RefArray<GameObject, Item> shovels;

	public GameObject prevShovelDecalGo;

	public DecalProjector prevShovelDecal;

	[DontSave]
	private const float DirtHoleTSDuration = 1.667f;

	private Vector3 shovelStartOffsetPos;

	private Quaternion shovelStartOffsetRot;

	[DontSave]
	[Min(0f)]
	public float DirtEffectDelay;

	[DontSave]
	public Vector3 DirtEffectOffset;

	[Tooltip("How long should the item wait to move from under the ground after shovel started digging?")]
	[DontSave]
	public float shovelItemMoveDelay = 0.5f;

	[Tooltip("How long should the item move from under the ground to above surface?")]
	[DontSave]
	public float shovelItemMoveDuration = 0.75f;

	[Tooltip("How long should will the hole fade out?")]
	[DontSave]
	public float dirtHoleFadeOutDuration = 1f;

	[Tooltip("How long should the decal wait before starting to fade in?")]
	[DontSave]
	public float dirtHoleDecalFadeInDelay;

	[Tooltip("How long should will the decal fade in?")]
	[DontSave]
	public float dirtHoleDecalFadeInDuration = 1f;

	[Tooltip("A list of items that can be dug up with a shovel.")]
	[DontSave]
	public List<DiggableItem> diggableItems;

	private bool[] diggableItemWasDug;

	[Header("Shovel Gizmos")]
	[Tooltip("Color of the gizmos that displays dig radius of each diggable item.")]
	[DontSave]
	public Color digRadiusGizmosColor = new Color(0f, 1f, 0f, 0.5f);

	[Tooltip("Color of the gizmos that displays where a diggable item will end up when dug.")]
	[DontSave]
	public Color digRaiseDistanceGizmosColor = new Color(1f, 0f, 1f, 0.5f);

	[Tooltip("Radius of the sphere gizmos that displays where a diggable item will end up when dug.")]
	[DontSave]
	public float digRaiseDistanceGizmosRadius = 0.1f;

	[Header("Fire")]
	[DontSave]
	public Ref<GameObject, Transform, ParticleSystem> leftFireRef;

	[DontSave]
	public RefArray<GameObject, Transform> leftFirePoints;

	[DontSave]
	public Ref<GameObject, Transform, ParticleSystem> rightFireRef;

	[DontSave]
	public RefArray<GameObject, Transform> rightFirePoints;

	private const float FireDuration = 2.1f;

	private const float FireLetterPause = 1.5f;

	private FireState fireState = FireState.LetterPause;

	[DontSave]
	private FireMovement[] LeftFireMovements = new FireMovement[5]
	{
		FireMovement.StayCenter,
		FireMovement.StayUp,
		FireMovement.StayDown,
		FireMovement.StayDown,
		FireMovement.MoveDown
	};

	[DontSave]
	private FireMovement[] RightFireMovements = new FireMovement[5]
	{
		FireMovement.StayDown,
		FireMovement.MoveDown,
		FireMovement.StayUp,
		FireMovement.MoveDown,
		FireMovement.MoveDown
	};

	private int fireMovementIndex;

	[DontSave]
	public Ref<Item, Transform, GameObject> firePaperRef;

	[Header("Rat")]
	[DontSave]
	public Ref<AnimationSampler, Transform> ratRef;

	private float[] ratAnimationCurrentTime;

	private Quaternion[] ratAnimationLookToPointRot;

	[DontSave]
	public List<AnimationPoint> ratAnimation;

	[DontSave]
	public AnimationClip ratWalk;

	[DontSave]
	public AnimationClip ratWalkLeft;

	[DontSave]
	public AnimationClip ratWalkRight;

	[DontSave]
	public AnimationClip ratRun;

	[DontSave]
	public AnimationClip ratRunLeft;

	[DontSave]
	public AnimationClip ratRunRight;

	[DontSave]
	public AnimationClip ratJump;

	[DontSave]
	public AnimationClip ratSit;

	[DontSave]
	public Npc wellNpc;

	private int ratLastAP;

	private int ratTargetAP = 1;

	[Header("Crows")]
	[DontSave]
	public RefArray<AnimationSampler, Transform> crowAnimSamplers;

	private float[] crowSamples = new float[3] { 0f, 0.5f, 0.85f };

	[DontSave]
	public AnimationClip crowFlyClip;

	[DontSave]
	public Sequence crowFlySequence;

	private bool crowsFlewAway;

	[Header("Extra Audio")]
	[DontSave]
	public RefArray<Item, GameObject> bottleCaps;

	[DontSave]
	private System.Random random;

	private const int exitAnswer = 5;

	private const int lastQuestion = 4;

	private const int firstQuestion = 1;

	private const int correctAnswerIndex = 500;

	private int[] wellNpcOrder;

	private int currentNpcIndex;

	private bool wasTeleportedOut;

	[DontSave]
	public Trigger fireLogsTrigger;

	[DontSave]
	public RefArray<GameObject, MaterialState, Item, Transform> fireLog;

	[DontSave]
	public ParticleSystem fireLogVFX;

	[DontSave]
	public Transform fireLogVFXTransform;

	[Header("Generated Variables")]
	public GameObject[] disableInSplitscreen;

	[DontSave]
	public VisualEffect whispVE;

	[DontSave]
	public Ref<ParticleSystem, Transform> WhispParticles02PSRef;

	[DontSave]
	public GameObject WhispParticles02;

	[DontSave]
	public GameObject WhispParticles02Colliders;

	[DontSave]
	public Sequence whispSequence;

	[DontSave]
	public Light whispLight;

	[DontSave]
	public ParticleSystem whispExplosionSmoke;

	[DontSave]
	public ParticleSystem whispDust;

	[DontSave]
	public GameObject whispCutSceneGameObject;

	public override void onInitPredicates()
	{
		game.registerPredicate(LevelPredicate.Statue1Solved, () => isStatueSolved(0) && !game.isAnyPlayerInteracting(statueDials[0].Get<GameObject>(0)));
		game.registerPredicate(LevelPredicate.Statue2Solved, () => isStatueSolved(1) && !game.isAnyPlayerInteracting(statueDials[1].Get<GameObject>(0)));
		game.registerPredicate(LevelPredicate.StatuesSolved, () => isStatueSolved(0) && isStatueSolved(1) && !game.isAnyPlayerInteracting(statueDials[0].Get<GameObject>(0)) && !game.isAnyPlayerInteracting(statueDials[1].Get<GameObject>(0)));
		game.registerPredicate(LevelPredicate.CarriageDoor, () => checkCarriageDoorSolved());
		game.registerPredicate(LevelPredicate.Liars, () => checkLiarsCorrect());
		game.registerPredicate(LevelPredicate.VanHelsing, () => checkVanHelsingBoxSolved());
		game.registerPredicate(LevelPredicate.MiniStatues, () => checkMiniStatues());
		game.registerPredicate(LevelPredicate.ExitDoor, () => checkExitDoorSolved());
		game.registerPredicate(LevelPredicate.WellSliders, () => checkWellSlidersSolved());
		game.registerPredicate(LevelPredicate.StarSlidableTargetable, 0.01f, true, () => canStarBePlaced());
	}

	public override void onLevelPredicateDone(int type, int doneCounter)
	{
		if (type == 0)
		{
			solveStatue(0);
		}
		if (type == 1)
		{
			solveStatue(1);
		}
		if (type == 2)
		{
			game.finishPuzzle(Puzzle.Statues);
		}
		if (type == 3)
		{
			carriageDoorSolved();
		}
		if (type == 4)
		{
			solveLiars();
		}
		if (type == 5)
		{
			solveVanHelsingBox();
		}
		if (type == 6)
		{
			solveMiniStatues();
		}
		if (type == 7)
		{
			solveExitDoor();
		}
		if (type == 8)
		{
			wellSolveSliders();
		}
		if (type == 9)
		{
			setStarSlotsTargetable(canPlace: true);
		}
	}

	public override void onLevelPredicateUndone(int type)
	{
		if (type == 9)
		{
			setStarSlotsTargetable(canPlace: false);
		}
	}

	public override void onRPCCalled(int type)
	{
		if (type == 0)
		{
			for (int i = 0; i < symbolsCurrentSolution.Length; i++)
			{
				symbolsCurrentSolution[i] = false;
			}
			game.startTimer(new SymbolPointerNotCorrectTimer(), 1.5f);
		}
	}

	public override void onInit()
	{
		shovelStartOffsetPos = shovelAnimationMoving[0].localPosition;
		shovelStartOffsetRot = shovelAnimationMoving[0].localRotation;
		Switch3D[] array = gargoyleDoors;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = false;
		}
		mausoleumDoorOne.Get<Switch3D>(0).targetable = true;
		mausoleumDoorTwo.Get<Switch3D>(0).targetable = true;
		gargoyleDoorLockItem.targetable = false;
		gargoyleDoorLockItem.hasRigidbody = false;
		array = symbolsDoors;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = false;
		}
		smallMauseoleumDoorOne.Get<Switch3D>(0).targetable = true;
		smallMauseoleumDoorTwo.Get<Switch3D>(0).targetable = true;
		symbolsPointerSoundInstance = PineFmod.createInstance("event:/Sound Effects/04 Items/Mechanical, Machines & Motors/Metal_Small_Mechanical_Loop");
		PineFmod.set3DAttributes(symbolsPointerSoundInstance, PineFmod.to3DAttributes(symbolsPointerRef));
		moveSymbolsPointer(0);
		foreach (GameObject item in dirtHolePool.Iterate<GameObject>(0))
		{
			item.SetActive(value: false);
		}
		foreach (VisualEffect item2 in dirtEffectPool.Iterate<VisualEffect>(0))
		{
			item2.Stop();
		}
		foreach (DecalProjector item3 in dirtDecalPool.Iterate<DecalProjector>(0))
		{
			item3.fadeFactor = 0f;
		}
		diggableItemWasDug = new bool[diggableItems.Count];
		starBox.targetable = false;
		starBoxKeyRef.Get<Item>(0).targetable = false;
		starSlidable.targetable = true;
		starBoxSlot.targetable = false;
		starBoxSlotWrong.targetable = false;
		WhispParticles02.SetActive(value: false);
		wellAmbiance.SetActive(value: false);
		whispCutSceneGameObject.SetActive(value: false);
		random = new System.Random(game.getSyncedRandomSeed());
		whispMoonMS.setState("Transparent");
		crowLastPosition = crowRef.Get<Transform>(0f).position;
		crowCurrentHeight = crowLastPosition.y;
		crowRef.Get<GameObject>(0).SetActive(value: false);
		crowMirrorRef.Get<GameObject>(0).SetActive(value: false);
		crowMirrorMS = crowMirrorRef.Get<MaterialState>(0u);
		fireChestLid.targetable = false;
		leftFireRef.Get<ParticleSystem>(0u).Stop();
		rightFireRef.Get<ParticleSystem>(0u).Stop();
		leftFireRef.Get<Transform>(0f).position = leftFirePoints[1].Get<Transform>(0f).position;
		rightFireRef.Get<Transform>(0f).position = rightFirePoints[1].Get<Transform>(0f).position;
		game.startTimer(new FireTimer(), 1.5f);
		carriageDoorSolvedInstance = PineFmod.createInstance("event:/Sound Effects/04 Items/Mechanical, Machines & Motors/Wood_Mechanical_Loop");
		PineFmod.set3DAttributes(carriageDoorSolvedInstance, PineFmod.to3DAttributes(carriageDoorSlidabeGraph.transform));
		wallNumberRocks[3].setState("Selected");
		wallNumberRocks[6].setState("Selected");
		wallNumbersChestLid.Get<Switch3D>(0).targetable = false;
		foreach (ParticleSystem flame in flames)
		{
			flame.Stop();
		}
		vanHelsingBoxTop.targetable = false;
		vanHelsingLockItem.targetable = false;
		vanHelsingLockItem.hasRigidbody = false;
		miniStatueGargoyle.targetable = false;
		miniStatueAngel.targetable = false;
		gargoyleDoorKeyRef.Get<Item>(0).targetable = false;
		for (int j = 0; j < statueSpikes.Length; j++)
		{
			statueSpikes[j].originalPositions = new Vector3[statueSpikes[j].spikes.Length];
			for (int k = 0; k < statueSpikes[j].spikes.Length; k++)
			{
				statueSpikes[j].originalPositions[k] = statueSpikes[j].spikes[k].Get<Transform>(0f).position;
			}
			statueDials[j].Get<Dial>(0f).targetable = false;
		}
		miniStatueSolvedInstance = PineFmod.createInstance("event:/Sound Effects/04 Items/Concrete & Rock/Rock_Drag_02");
		PineFmod.set3DAttributes(miniStatueSolvedInstance, PineFmod.to3DAttributes(miniStatueSlotTSs[15].transform));
		statueSoundInstances = new EventInstance[statueSequences.Length];
		for (int l = 0; l < statueSoundInstances.Length; l++)
		{
			statueSoundInstances[l] = PineFmod.createInstance("event:/Sound Effects/04 Items/Concrete & Rock/Rock_Drag_02");
			PineFmod.set3DAttributes(statueSoundInstances[l], PineFmod.to3DAttributes(statueSequences[l].transform));
		}
		ratAnimationCurrentTime = new float[ratAnimation.Count];
		ratAnimationLookToPointRot = new Quaternion[ratAnimation.Count];
		ratAnimationLookToPointRot[ratLastAP] = Quaternion.Euler(ratRef.Get<Transform>(0f).forward);
		ratAnimationLookToPointRot[ratTargetAP] = Quaternion.LookRotation(ratAnimation[ratTargetAP].worldWaypointRef.position - ratAnimation[ratLastAP].worldWaypointRef.position);
		exitDoorGearsSoundInstances = new EventInstance[exitDoorGears.Length];
		for (int m = 0; m < exitDoorGearsSoundInstances.Length; m++)
		{
			exitDoorGearsSoundInstances[m] = PineFmod.createInstance("event:/Sound Effects/04 Items/Mechanical, Machines & Motors/Metal_Mechanical_Loop_08");
			PineFmod.set3DAttributes(exitDoorGearsSoundInstances[m], PineFmod.to3DAttributes(exitDoorGears[m].Get<Transform>(0f)));
		}
		if (game.isSplitscreen)
		{
			GameObject[] array2 = disableInSplitscreen;
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i].SetActive(value: false);
			}
		}
	}

	public override void onUpdate()
	{
		starBoxDrawerTS.setState("Open", starBoxDrawerPercent);
		RaycastHit hitInfo;
		if (crowMirrorPickedUp)
		{
			crowCurrentHeight = Mathf.MoveTowards(crowCurrentHeight, 6f, Time.deltaTime * CrowVerticalSpeed);
			Game.GamePlayerData playerWithItemInInventory = game.getPlayerWithItemInInventory(crowMirrorRef.Get<GameObject>(0));
			Vector3 origin = playerWithItemInInventory?.lastTransformPose.position ?? crowMirrorRef.Get<Transform>(0f).position;
			Vector3 vector = new Vector3(origin.x, crowCurrentHeight, origin.z);
			Vector3 normalized = (vector - crowRef.Get<Transform>(0f).position).normalized;
			if (normalized != Vector3.zero)
			{
				Ray ray = new Ray(crowRef.Get<Transform>(0f).position, normalized);
				Ray ray2 = new Ray(origin, Vector3.up);
				foreach (Collider crowBlockCollider in crowBlockColliders)
				{
					if (crowBlockCollider.Raycast(ray2, out hitInfo, 10f) && crowBlockCollider.Raycast(ray, out var hitInfo2, 1f))
					{
						Plane plane = new Plane(hitInfo2.normal, hitInfo2.point);
						float num = Vector3.Distance(crowRef.Get<Transform>(0f).position, plane.ClosestPointOnPlane(crowRef.Get<Transform>(0f).position));
						Vector3 vector2 = hitInfo2.point + hitInfo2.normal * num;
						vector.x = vector2.x;
						vector.z = vector2.z;
						break;
					}
				}
			}
			crowRef.Get<Transform>(0f).position = vector;
			if (playerWithItemInInventory != null)
			{
				Quaternion to = Quaternion.Euler(Vector3.up * playerWithItemInInventory.lastTransformPose.rotation.eulerAngles.y);
				crowRef.Get<Transform>(0f).localRotation = Quaternion.RotateTowards(crowRef.Get<Transform>(0f).localRotation, to, Time.deltaTime * 300f);
			}
			if (!UnityUtils.closeEnough(vector.x, crowLastPosition.x) || !UnityUtils.closeEnough(vector.z, crowLastPosition.z))
			{
				if (!crowMoving)
				{
					crowMoving = true;
					crowRef.Get<Animation>(0u).Play("rig_Crow|Fly");
				}
			}
			else if (crowMoving)
			{
				crowMoving = false;
				crowRef.Get<Animation>(0u).Play("rig_Crow|Flying");
			}
			crowLastPosition = crowRef.Get<Transform>(0f).position;
			crowMirrorDecalFade = Mathf.Max(Mathf.PingPong(Time.time, 2f) * 0.5f, 0.1f);
			crowMirrorMS.setState("Bright", Mathf.Lerp(0f, 3f, crowMirrorDecalFade));
		}
		crowMirrorSwitch.targetable = game.isInInventory(crowMirrorRef.Get<Item>((short)0));
		Span<Vector3> span = stackalloc Vector3[8];
		for (int i = 0; i < flameLanternBoxes.Count; i++)
		{
			bool flag = false;
			Vector3 origin2 = game.playerViewRay.origin;
			for (int j = 0; j < flameDecalBoxes.Length; j++)
			{
				Bounds bounds = flameDecalBoxes[j].Get<Collider>(0).bounds;
				Vector3 min = bounds.min;
				Vector3 max = bounds.max;
				span[0].Set(min.x, min.y, min.z);
				span[1].Set(min.x, min.y, max.z);
				span[2].Set(min.x, max.y, min.z);
				span[3].Set(min.x, max.y, max.z);
				span[4].Set(max.x, min.y, min.z);
				span[5].Set(max.x, min.y, max.z);
				span[6].Set(max.x, max.y, min.z);
				span[7].Set(max.x, max.y, max.z);
				bool flag2 = true;
				for (int k = 0; k < span.Length; k++)
				{
					Vector3 direction = span[k] - origin2;
					Ray ray3 = new Ray(origin2, direction);
					if (!flameLanternBoxes[i].bounds.IntersectRay(ray3))
					{
						flag2 = false;
						break;
					}
				}
				bool flag3 = false;
				Vector3 direction2 = flameDecalBoxes[j].Get<Transform>(0f).position - origin2;
				Ray ray4 = new Ray(origin2, direction2);
				for (int l = 0; l < flameBlockingColliders.Length; l++)
				{
					if (flameBlockingColliders[l].Raycast(ray4, out hitInfo, direction2.magnitude))
					{
						flag3 = true;
						break;
					}
				}
				flag = flag || (flag2 && !flag3);
			}
			if (flag)
			{
				if (!flames[i].isEmitting)
				{
					flames[i].Play();
				}
			}
			else if (flames[i].isEmitting)
			{
				flames[i].Stop();
			}
		}
		for (int m = 0; m < statueSlots.Length; m++)
		{
			if (StatueSolved[m])
			{
				continue;
			}
			Item insertedItem = statueSlots[m].Get<Slot>(0).insertedItem;
			if (insertedItem != null && UnityUtils.closeEnough(insertedItem.transform.position, statueSlots[m].Get<Slot>(0).pivot.position))
			{
				Collider[] componentsInChildren = insertedItem.GetComponentsInChildren<Collider>();
				for (int n = 0; n < statueSpikes[m].spikes.Length; n++)
				{
					Ref<MaterialState, Transform> obj = statueSpikes[m].spikes[n];
					Vector3 position = statueSpikes[m].retractedPositionsRefs[n].position;
					Vector3 direction3 = new Vector3(position.x, insertedItem.transform.position.y, insertedItem.transform.position.z) - position;
					Ray ray5 = new Ray(position, direction3);
					float num2 = direction3.magnitude;
					Collider[] array = componentsInChildren;
					for (int num3 = 0; num3 < array.Length; num3++)
					{
						if (array[num3].Raycast(ray5, out var hitInfo3, 1f))
						{
							if (hitInfo3.distance < num2)
							{
								num2 = hitInfo3.distance;
							}
							float magnitude = (statueSpikes[m].originalPositions[n] - position).magnitude;
							if (num2 > magnitude)
							{
								num2 = magnitude;
							}
						}
					}
					obj.Get<Transform>(0f).position = Vector3.MoveTowards(obj.Get<Transform>(0f).position, position + direction3.normalized * num2, Time.deltaTime * 0.5f);
					if (Vector3.Distance(obj.Get<Transform>(0f).position, statueSpikes[m].correctPositionsRefs[n].position) <= 0.0035f)
					{
						obj.Get<MaterialState>(0).transitionTo("Correct", 4f);
					}
					else
					{
						obj.Get<MaterialState>(0).transitionTo("Default", 4f);
					}
				}
			}
			else if (insertedItem == null)
			{
				for (int num4 = 0; num4 < statueSpikes[m].spikes.Length; num4++)
				{
					Ref<MaterialState, Transform> obj2 = statueSpikes[m].spikes[num4];
					obj2.Get<Transform>(0f).position = Vector3.MoveTowards(obj2.Get<Transform>(0f).position, statueSpikes[m].originalPositions[num4], Time.deltaTime * 0.5f);
					obj2.Get<MaterialState>(0).transitionTo("Default", 4f);
				}
			}
		}
		if (!whispRising && WhispParticles02.activeSelf)
		{
			if (whispFloatTimer >= WhispFloatTimOut)
			{
				whispFloatTimer = 0f;
				movingDown = !movingDown;
			}
			else
			{
				whispFloatTimer += Time.deltaTime;
			}
			float t = Mathf.InverseLerp(0f, WhispFloatTimOut, whispFloatTimer);
			float num5 = Mathf.Lerp(movingDown ? WhispFloatOffset : 0f, movingDown ? 0f : WhispFloatOffset, t);
			WhispParticles02PSRef.Get<Transform>(0f).localPosition = whispRefPos + Vector3.up * num5;
		}
		if (ratAnimationCurrentTime[ratTargetAP] >= ratAnimation[ratTargetAP].duration)
		{
			ratLastAP = ratTargetAP;
			ratTargetAP = (int)Mathf.Repeat(ratTargetAP + 1, ratAnimation.Count);
			AnimationPoint animationPoint = ratAnimation[ratLastAP];
			AnimationPoint animationPoint2 = ratAnimation[ratTargetAP];
			ratAnimationCurrentTime[ratTargetAP] = 0f;
			Vector3 vector3 = animationPoint2.worldWaypointRef.position - animationPoint.worldWaypointRef.position;
			ratAnimationLookToPointRot[ratTargetAP] = Quaternion.LookRotation(vector3);
			float deltaAngle = Vector3.SignedAngle(animationPoint2.worldWaypointRef.position, vector3, ratRef.Get<Transform>(0f).forward);
			ratRef.Get<AnimationSampler>(0).clip = getRatClip(animationPoint2.type, deltaAngle);
		}
		else
		{
			AnimationPoint animationPoint3 = ratAnimation[ratLastAP];
			AnimationPoint animationPoint4 = ratAnimation[ratTargetAP];
			float t2 = ratAnimationCurrentTime[ratTargetAP] / animationPoint4.duration;
			ratRef.Get<Transform>(0f).position = Vector3.Lerp(animationPoint3.worldWaypointRef.position, animationPoint4.worldWaypointRef.position, t2);
			ratRef.Get<AnimationSampler>(0).unitTime = Mathf.Repeat(ratAnimationCurrentTime[ratTargetAP], 1f);
			float num6 = ((animationPoint4.duration < ratWalkLeft.length) ? animationPoint4.duration : ratWalkLeft.length);
			float num7 = Mathf.Clamp01(ratAnimationCurrentTime[ratTargetAP] * 2f / num6);
			ratRef.Get<Transform>(0f).rotation = Quaternion.Lerp(ratAnimationLookToPointRot[ratLastAP], ratAnimationLookToPointRot[ratTargetAP], num7);
			if (ratRef.Get<AnimationSampler>(0).clip != ratWalk && num7 >= 1f)
			{
				ratRef.Get<AnimationSampler>(0).clip = getRatClip(animationPoint4.type, 0f);
			}
			ratAnimationCurrentTime[ratTargetAP] += Time.deltaTime;
		}
		for (int num8 = 0; num8 < crowAnimSamplers.Length; num8++)
		{
			float num9 = 1f - (float)num8 * 0.2f;
			crowSamples[num8] = Mathf.Repeat(crowSamples[num8] + Time.deltaTime * num9, crowAnimSamplers[num8].Get<AnimationSampler>(0).clip.length);
			crowAnimSamplers[num8].Get<AnimationSampler>(0).unitTime = crowSamples[num8] / crowAnimSamplers[num8].Get<AnimationSampler>(0).clip.length;
		}
		if (!crowsFlewAway)
		{
			float num10 = float.MaxValue;
			for (int num11 = 0; num11 < crowAnimSamplers.Length; num11++)
			{
				foreach (Game.GamePlayerData allPlayer in game.getAllPlayers())
				{
					float num12 = Vector3.Distance(allPlayer.lastTransformPose.position, crowAnimSamplers[num11].Get<Transform>(0f).position);
					if (num12 < num10)
					{
						num10 = num12;
					}
				}
			}
			if (num10 < 4f)
			{
				crowsFlewAway = true;
				crowFlySequence.play(-1f, crowFlySequence.sequenceDuration);
			}
		}
		else
		{
			for (int num13 = 0; num13 < crowAnimSamplers.Length; num13++)
			{
				crowAnimSamplers[num13].Get<AnimationSampler>(0).clip = crowFlyClip;
			}
		}
	}

	public override void onTimerUpdate(Timer timer)
	{
		if (timer is ShovelFlyBackTimer shovelFlyBackTimer)
		{
			Transform transform = null;
			if (game.hasAuthority(shovelFlyBackTimer.shovel))
			{
				GameObject impostorInHand = game.getImpostorInHand();
				if (impostorInHand != null)
				{
					transform = impostorInHand.transform.GetChild(0).GetChild(0);
				}
			}
			else
			{
				Game.GamePlayerData playerWithItemInInventory = game.getPlayerWithItemInInventory(shovelFlyBackTimer.shovel.gameObject);
				if (playerWithItemInInventory != null)
				{
					GameObject inHandItemBubble = playerWithItemInInventory.inHandItemBubble;
					if (inHandItemBubble != null)
					{
						transform = inHandItemBubble.transform.GetChild(0).GetChild(0);
					}
				}
			}
			if (transform != null)
			{
				Transform obj = shovelAnimationMoving[shovelFlyBackTimer.shovelIndex];
				float t = shovelFlyBackTimer.time / shovelFlyBackTimer.duration;
				Vector3 b = new Vector3(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z);
				obj.position = Vector3.Lerp(shovelFlyBackTimer.shovelStartDigingPos, transform.position, t);
				obj.rotation = Quaternion.Lerp(shovelFlyBackTimer.shovelStartDigingRot, transform.rotation, t);
				obj.localScale = Vector3.Lerp(shovelFlyBackTimer.shovelStartDigingScale, b, t);
			}
		}
		if (timer is StarBoxTimer)
		{
			float a = ((timer.time <= 1f) ? 0f : 0.17f);
			float b2 = ((timer.time <= 1f) ? 0.17f : 0.2f);
			float t2 = ((timer.time <= 1f) ? timer.time : (timer.time - 1f));
			starBoxDrawerPercent = Mathf.Lerp(a, b2, t2);
		}
		if (timer is DecalFadeTimer decalFadeTimer)
		{
			decalFadeTimer.decalProjector.fadeFactor = timer.unitTime;
		}
		if (timer is DecalFadeOutTimer decalFadeOutTimer)
		{
			decalFadeOutTimer.decalProjector.fadeFactor = 1f - timer.unitTime;
		}
		if (timer is CarriageDoorAnimationTimer carriageDoorAnimationTimer)
		{
			for (int i = 0; i < carriageDoorSlidabeGraph.piecesList.Count; i++)
			{
				SlidableGraphPiece slidableGraphPiece = carriageDoorSlidabeGraph.piecesList[i];
				slidableGraphPiece.transform.localRotation = carriageDoorAnimationTimer.initialRotation[i] * Quaternion.Euler(0f, 180f * timer.unitTime, 0f);
				slidableGraphPiece.transform.localPosition = new Vector3(slidableGraphPiece.transform.localPosition.x, carriageDoorAnimationTimer.y * (1f - timer.unitTime), slidableGraphPiece.transform.localPosition.z);
			}
		}
		if (timer is ExitDoorGearsTimer)
		{
			for (int j = 0; j < exitDoorGears.Length; j++)
			{
				float num = Time.deltaTime * 5f * 60f;
				exitDoorGears[j].Get<Transform>(0f).localRotation *= Quaternion.Euler(0f, 0f, (j < 2) ? (0f - num) : num);
			}
		}
		if (timer is ExitDoorGearCorrectSlotTimer { gearIndex: var gearIndex, direction: var direction })
		{
			float num2 = 180f;
			float to = num2 + (float)(direction * 179);
			exitDoorGears[gearIndex].Get<Transform>(0f).localRotation = Quaternion.Euler(90f, 0f, Mathf.SmoothStep(num2, to, timer.unitTime));
		}
		if (timer is HoleEffect2Timer { effect: var effect } holeEffect2Timer)
		{
			_ = holeEffect2Timer.decal;
			MaterialState[] componentsInChildren = effect.GetComponentsInChildren<MaterialState>();
			for (int k = 0; k < componentsInChildren.Length; k++)
			{
				componentsInChildren[k].setWeight("Transparent", timer.unitTime);
			}
		}
	}

	public override void onTimerDone(Timer timer)
	{
		if (timer is FireChestPaperTimer)
		{
			game.setParent(firePaperRef.Get<Transform>(0f), game.levelContainerTransform);
			firePaperRef.Get<Item>(0).hasRigidbody = true;
		}
		if (timer is CrowMirrorTimer)
		{
			crowMirrorRef.Get<GameObject>(0).SetActive(value: true);
			vanHelsingSecretDrawerAnimationSampler.play();
		}
		if (timer is StarBoxTimer)
		{
			starBoxDial.setValue(55);
			starBoxDial.targetable = true;
		}
		if (timer is StatueSolvedTimer { statueIndex: var statueIndex })
		{
			game.increaseZoomCounter(statueZoomables[statueIndex].Get<Zoomable>(0));
			statueZoomables[statueIndex].Get<Zoomable>(0).targetable = false;
			if (statueIndex == 0)
			{
				miniStatueGargoyle.targetable = true;
				miniStatueAngel.targetable = true;
			}
			else
			{
				gargoyleDoorKeyRef.Get<Item>(0).targetable = true;
			}
			PineFmod.start(statueSoundInstances[statueIndex]);
		}
		if (timer is SymbolPointerCorrectTimer)
		{
			game.increaseZoomCounter(symbolsLockZoomableRef.Get<Zoomable>(0));
			symbolsLockZoomableRef.Get<Zoomable>(0).targetable = false;
			game.setParent(symbolsLockItemRef.Get<Transform>(0f), game.levelContainerTransform);
			symbolsLockItemRef.Get<Item>(0).targetable = true;
			symbolsLockItemRef.Get<Item>(0).hasRigidbody = true;
			Switch3D[] array = symbolsDoors;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].targetable = true;
			}
			smallMauseoleumDoorOne.Get<Switch3D>(0).targetable = false;
			smallMauseoleumDoorTwo.Get<Switch3D>(0).targetable = false;
		}
		if (timer is SymbolPointerNotCorrectTimer)
		{
			if (symbolsPointerSoundInstance.getPlaybackState(out var state) == RESULT.OK && state != PLAYBACK_STATE.PLAYING)
			{
				PineFmod.start(symbolsPointerSoundInstance);
			}
			moveSymbolsPointer(0);
			symbolsSlidable.targetable = true;
		}
		if (timer is Well1Timer)
		{
			wellLid.SetActive(value: false);
			for (int j = 0; j < wellLidPieces.Length; j++)
			{
				Ref<Item, Transform> obj = wellLidPieces[j];
				game.setParent(obj.Get<Transform>(0f), game.levelContainerTransform);
				obj.Get<Item>(0).hasRigidbody = true;
			}
			ColorUtility.TryParseHtmlString("#40ACD4", out var color);
			whispLight.color = color;
			whispLight.colorTemperature = 5500f;
			whispSequence.play(-1f, whispSequence.sequenceDuration);
			game.startTimer(new Well2_1Timer(), WellSmokeDelay);
			game.startTimer(new Well2_2Timer(), whispSequence.sequenceKeyFrames[0].time);
		}
		if (timer is Well2_1Timer)
		{
			PineFmod.playOneShotSound("event:/Sound Effects/05 Misc/Magic/Well_Explode");
			whispExplosionSmoke.Play();
		}
		if (timer is Well2_2Timer)
		{
			whispRising = true;
			WhispParticles02.SetActive(value: true);
			whispVE.Play();
			whispDust.Play();
		}
		if (timer is Well3Timer)
		{
			whispCutSceneGameObject.SetActive(value: false);
			wellNpc.targetable = true;
		}
		if (timer is WellSolveTimer)
		{
			exitDoorKeys[2].Get<GameObject>(0).SetActive(value: true);
			whispMoonMS.transitionToDuration("Default", 0.6f);
		}
		if (timer is CarriageDoorAnimationTimer)
		{
			PineFmod.stop(carriageDoorSolvedInstance, STOP_MODE.IMMEDIATE);
		}
		if (timer is CarriageDoorSolvedTimer)
		{
			carriageDoorTweenState.transitionTo("Open");
			carriageDoorLockedRef.Get<Switch3D>(0).targetable = false;
			game.increaseZoomCounter(carriageDoorZoomableRef.Get<Zoomable>(0f));
			carriageDoorZoomableRef.Get<Zoomable>(0f).targetable = false;
			vanHelsingLockKeyRef.Get<Item>(0).targetable = true;
			vanHelsingTools.Get<Item>(0, 0).targetable = true;
			carriageDoorOpened = true;
		}
		if (timer is ExitDoorGearCorrectSlotTimer exitDoorGearCorrectSlotTimer && !exitDoorSolved)
		{
			PineFmod.stop(exitDoorGearsSoundInstances[exitDoorGearCorrectSlotTimer.gearIndex], STOP_MODE.IMMEDIATE);
		}
		if (timer is FinalDoorTimer)
		{
			levelExitRef.Get<GameObject>(0).SetActive(value: true);
			game.levelCompleted();
			setupLevelComplete();
		}
		if (timer is DirtEffectTimer dirtEffectTimer)
		{
			dirtEffectTimer.effectTransform.localPosition = dirtEffectTimer.holeTransform.localPosition + DirtEffectOffset;
			dirtEffectTimer.effectVisuals.Play();
		}
		if (timer is HoleEffectTimer holeEffectTimer)
		{
			GameObject gameObject = dirtHolePool[holeEffectTimer.effectIndex].Get<GameObject>(0);
			DecalProjector decalProjector = dirtDecalPool[holeEffectTimer.effectIndex].Get<DecalProjector>(0);
			Transform transform = shovelAnimationMoving[holeEffectTimer.effectIndex];
			prevShovelDecal = decalProjector;
			prevShovelDecalGo = gameObject;
			game.startTimer(new ShovelFlyBackTimer(holeEffectTimer.effectIndex, holeEffectTimer.shovel, transform.position, transform.rotation, transform.localScale), 0.5f);
		}
		if (timer is ShovelFlyBackTimer shovelFlyBackTimer)
		{
			shovelFlyBackTimer.shovel.unlockItemInteractions = true;
			shovelFlyBackTimer.shovel.hideItemInHand = false;
			shovelPool[shovelFlyBackTimer.shovelIndex].Get<GameObject>(0f).SetActive(value: false);
			shovelAnimationMoving[shovelFlyBackTimer.shovelIndex].localScale = shovelFlyBackTimer.shovelStartDigingScale;
		}
		if (timer is HoleEffect2Timer { effect: var effect })
		{
			MaterialState[] componentsInChildren = effect.GetComponentsInChildren<MaterialState>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].setWeight("Transparent", 0f);
			}
			effect.GetComponent<TweenState>().setState("Dug", 0f);
			effect.SetActive(value: false);
		}
		if (timer is SymbolsGearTransition)
		{
			PineFmod.stop(symbolsPointerSoundInstance, STOP_MODE.IMMEDIATE);
		}
		if (timer is FireTransition fireTransition)
		{
			fireTransition.transform.GetComponent<ParticleSystem>().Stop();
		}
		if (timer is FireTimer)
		{
			if (fireState == FireState.Moving)
			{
				fireState = FireState.LetterPause;
				fireMovementIndex = (int)Mathf.Repeat(fireMovementIndex + 1, LeftFireMovements.Length);
				moveFire(leftFireRef, leftFirePoints, LeftFireMovements[fireMovementIndex], flaming: false);
				moveFire(rightFireRef, rightFirePoints, RightFireMovements[fireMovementIndex], flaming: false);
			}
			else if (fireState == FireState.LetterPause)
			{
				fireState = FireState.Moving;
				moveFire(leftFireRef, leftFirePoints, LeftFireMovements[fireMovementIndex], flaming: true);
				moveFire(rightFireRef, rightFirePoints, RightFireMovements[fireMovementIndex], flaming: true);
			}
			float duration = ((fireState == FireState.LetterPause) ? 1.5f : 2.1f);
			game.startTimer(new FireTimer(), duration);
		}
		if (timer is StatueRotationTransition statueRotationTransition)
		{
			statueRotationTransition.transform.GetComponent<Dial>().setValue(0);
		}
		if (timer is SymbolsPointerTransition symbolsPointerTransition)
		{
			PineFmod.playOneShotSoundAttached("event:/Sound Effects/04 Items/Locks/Lock_Open", symbolsPointerRef);
			if (symbolsPointerTransition.currentSymbol < SymbolsCorrectSolution.Length)
			{
				PineFmod.stop(symbolsPointerSoundInstance, STOP_MODE.IMMEDIATE);
			}
		}
		if (timer is ExitDoorGearsTransition exitDoorGearsTransition && !exitDoorSolved)
		{
			int num = exitDoorGears.IndexOf(exitDoorGearsTransition.transform, 0f);
			PineFmod.stop(exitDoorGearsSoundInstances[num], STOP_MODE.IMMEDIATE);
		}
		if (timer is ShovelFlyInTransition { shovelIndex: var shovelIndex } shovelFlyInTransition)
		{
			Ref<GameObject, Transform, TweenState> obj2 = dirtHolePool[shovelIndex];
			Ref<DecalProjector, GameObject> obj3 = dirtDecalPool[shovelIndex];
			obj2.Get<TweenState>(0u).transitionToDuration("Dug", 1.667f, 0.73f);
			game.startTimer(new DecalFadeTimer(obj3.Get<DecalProjector>(0)), dirtHoleDecalFadeInDuration, dirtHoleDecalFadeInDelay);
			game.startTimer(new HoleEffectTimer(shovelIndex, shovelFlyInTransition.shovel), 1.2f);
			shovelPool[shovelIndex].Get<AnimationSampler>(0u).enabled = true;
			if (tryGetEffectFromPool(out var nextEffect))
			{
				game.startTimer(new DirtEffectTimer(nextEffect.Get<Transform>(0u), nextEffect.Get<VisualEffect>(0), obj2.Get<Transform>(0f)), DirtEffectDelay);
			}
			PineFmod.playOneShotSoundAttached("event:/Sound Effects/04 Items/Wood/Shovel/Wood_Shovel_Dig", obj2.Get<GameObject>(0));
		}
		if (timer is DigUpItemTransition digUpItemTransition)
		{
			PineFmod.playOneShotSoundAttached("event:/Sound Effects/05 Misc/Magic/Woosh_1", digUpItemTransition.transform.gameObject);
		}
		bool tryGetEffectFromPool(out Ref<VisualEffect, GameObject, Transform> reference)
		{
			reference = null;
			for (int k = 0; k < dirtEffectPool.Length; k++)
			{
				Ref<VisualEffect, GameObject, Transform> obj4 = dirtEffectPool[k];
				if (!obj4.Get<VisualEffect>(0).HasAnySystemAwake())
				{
					reference = obj4;
				}
			}
			if (reference == null)
			{
				UnityEngine.Debug.Log("Not enough dit effects in the object pool");
			}
			return reference != null;
		}
	}

	public override void onTrigger(Trigger trigger, TriggerEvent triggerEvent)
	{
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
		}
	}

	public override void onUnlock(Lock targetLock)
	{
		if (targetLock == wallNumbersChestLockRef.Get<Lock>(0))
		{
			game.changeExamineRotation(wallNumbersChestLockRef.Get<GameObject>(0u), Vector2.zero, 0.25f);
			wallNumbersChestLockRef.Get<TweenState>(0f).transitionTo("Open", 1f, 1f, playSound: true, 0.25f);
			wallNumbersChestLid.Get<Switch3D>(0).tweenState.transitionTo("Down", 1f, 0.3f, playSound: true, 0.75f);
			game.finishPuzzle(Puzzle.SymbolChest);
		}
		else if (targetLock == fireChestLock)
		{
			fireChestLockSwitchTS.transitionTo("Open", 2f, 1f, playSound: true, 0.25f);
		}
	}

	public override void onSlot(Slot targetSlot)
	{
		int num = exitDoorSlots.IndexOf(targetSlot);
		int num2 = vanHelsingToolSlots.IndexOf(targetSlot);
		int index = UnityUtils.getIndex(miniStatueSlots, targetSlot);
		int num3 = statueSlots.IndexOf(targetSlot, 0);
		if (num2 >= 0)
		{
			vanHelsingToolTSs[num2].transitionTo("Open", 2f);
		}
		else if (targetSlot == gargoyleDoorKeySlot && targetSlot.insertedItem == gargoyleDoorKeyRef.Get<Item>(0))
		{
			gargoyleDoorLockTS.transitionTo("Open", 2f, 1f, playSound: true, gargoyleDoorKeySlot.rotateKeyTurnDuration);
		}
		else if (targetSlot == vanHelsingLockSlot && targetSlot.insertedItem == vanHelsingLockKeyRef.Get<Item>(0))
		{
			vanHelsingLockTS.transitionTo("Open", 2f, 1f, playSound: true, vanHelsingLockSlot.rotateKeyTurnDuration);
			vanHelsingLockSlotWrong.targetable = false;
		}
		else if (targetSlot == starBoxSlot && UnityUtils.contains(targetSlot.acceptItems, targetSlot.insertedItem))
		{
			starSlidable.targetable = false;
			starBoxSlot.targetable = false;
			starBoxSlotWrong.targetable = false;
			starBoxKeyRef.Get<Item>(0).targetable = false;
			starBoxDialSequence.play(-1f, starBoxDialSequence.sequenceDuration);
			game.startTimer(new StarBoxTimer(), 1.5f, 0.5f);
			game.finishPuzzle(Puzzle.StarChest);
		}
		else if (num >= 0)
		{
			int num4 = exitDoorSlots.FindIndex((Slot slot) => slot.acceptItems[0] == targetSlot.insertedItem);
			if (num4 != num)
			{
				exitDoorHolderTSs[num].transitionTo(num4.ToString(), 2f);
				Game obj = game;
				ExitDoorGearsTransition transition = new ExitDoorGearsTransition();
				Transform obj2 = exitDoorGears[num];
				Quaternion? rotation = Quaternion.Euler(90f, 0f, (num < 2) ? 90 : 270);
				obj.startTransitionLocal(transition, obj2, 0.5f, 0f, null, rotation);
			}
			else
			{
				game.startTimer(new ExitDoorGearCorrectSlotTimer(num, (num >= 2) ? 1 : (-1)), 0.5f);
			}
			if (!checkExitDoorSolved())
			{
				PineFmod.start(exitDoorGearsSoundInstances[num]);
			}
		}
		else if (index >= 0)
		{
			miniStatueSlotTSs[index].transitionTo("Down", 2f);
		}
		else if (num3 >= 0)
		{
			statueDialHandleTSs[num3].transitionTo("Out", 2f);
		}
	}

	public override void onRemoveFromSlot(Slot targetSlot, Item item)
	{
		int num = exitDoorSlots.IndexOf(targetSlot);
		int num2 = vanHelsingToolSlots.IndexOf(targetSlot);
		int index = UnityUtils.getIndex(miniStatueSlots, targetSlot);
		int num3 = statueSlots.IndexOf(targetSlot, 0);
		if (num >= 0)
		{
			exitDoorHolderTSs[num].transitionTo("Default", 2f);
			Game obj = game;
			Transform obj2 = exitDoorGears[num];
			Quaternion? rotation = Quaternion.Euler(90f, 0f, 180f);
			obj.startTransitionLocal(obj2, 0.5f, 0f, null, rotation);
		}
		else if (num2 >= 0)
		{
			vanHelsingToolTSs[num2].transitionTo("Default", 2f);
		}
		else if (index >= 0)
		{
			miniStatueSlotTSs[index].transitionTo("Default", 2f);
		}
		else if (num3 >= 0)
		{
			statueDials[num3].Get<Dial>(0f).targetable = false;
			statueDialHandleTSs[num3].transitionTo("Default", 2f);
			Game obj3 = game;
			StatueRotationTransition transition = new StatueRotationTransition();
			Transform obj4 = statueDials[num3].Get<Transform>(0u);
			Quaternion? rotation = statueDials[num3].Get<Dial>(0f).originalRotation;
			obj3.startTransitionLocal(transition, obj4, 0.25f, 0f, null, rotation);
		}
	}

	public override void onSlidableMoved(Slidable slidable, MoveEvent moveEvent)
	{
		if (slidable == symbolsSlidable && moveEvent == MoveEvent.Released && !symbolsSolved && (double)Mathf.Abs(slidable.value - 0.5f) > 0.25)
		{
			symbolsCurrentSolution[currentSymbol] = slidable.value > 0.5f;
			UnityEngine.Debug.Log($"current symbol {currentSymbol}: answer = {symbolsCurrentSolution[currentSymbol]}");
			int num = currentSymbol + 1;
			UnityEngine.Debug.Log(slidable.value);
			moveSymbolsPointer(num);
			if (num >= SymbolsCorrectSolution.Length)
			{
				slidable.targetable = false;
				if (symbolsPointerSoundInstance.getPlaybackState(out var state) == RESULT.OK && state != PLAYBACK_STATE.PLAYING)
				{
					PineFmod.start(symbolsPointerSoundInstance);
				}
				Game obj = game;
				SymbolsGearTransition transition = new SymbolsGearTransition();
				Transform obj2 = symbolsPointerGearRef.Get<Transform>(0f);
				Quaternion? rotation = symbolsPointerGearRef.Get<Transform>(0f).localRotation * Quaternion.Euler(0f, 90f, 0f);
				obj.startTransitionLocal(transition, obj2, 1f, 0f, null, rotation);
				if (!checkLiarsCorrect())
				{
					game.callRPC(RPC.ResetLiars);
				}
			}
		}
		else if (slidable == wallNumbersSlidableLeftRef.Get<Slidable>(0) || slidable == wallNumbersSlidableRightRef.Get<Slidable>(0))
		{
			int num2 = SlidableSymbolsNumbersLeft[wallNumbersSlidableLeftRef.Get<Slidable>(0).closestSnapPointIndex];
			int num3 = SlidableSymbolsNumbersRight[wallNumbersSlidableRightRef.Get<Slidable>(0).closestSnapPointIndex];
			for (int i = 0; i < wallNumberRocks.Length; i++)
			{
				string record = ((i == num2 || i == num3) ? "Selected" : "Default");
				wallNumberRocks[i].transitionTo(record, 1.5f);
			}
			if (!symbolSlidablesInteractedWith)
			{
				symbolSlidablesInteractedWith = true;
			}
		}
		else if (slidable == starSlidable)
		{
			starBoxKeyRef.Get<TweenState>(0f).setWeight("Open", slidable.value);
		}
	}

	public override void onHorizontalSliderMoved(HorizontalSlider slider, int pointIndex, MoveEvent moveEvent)
	{
		if (moveEvent == MoveEvent.Released)
		{
			int num = wellSliders.IndexOf(slider);
			if (num >= 0 && !wellSolved)
			{
				wellSlidersCurrent[num] = pointIndex;
				UnityEngine.Debug.Log($"slider with index[{num}] at point: {pointIndex}, with offset: {slider.offset}");
			}
		}
	}

	public override void onAddToInventory(Item item)
	{
		if (item == crowMirrorRef.Get<Item>((short)0) && !crowMirrorPickedUp)
		{
			crowMirrorPickedUp = true;
			crowRef.Get<GameObject>(0).SetActive(value: true);
			crowRef.Get<Transform>(0f).position = crowMirrorRef.Get<Transform>(0f).position;
			crowCurrentHeight = crowRef.Get<Transform>(0f).position.y;
		}
		else if (bottleCaps.Contains(item, 0))
		{
			int i = bottleCaps.IndexOf(item, 0);
			PineFmod.playOneShotSoundAttached("event:/Sound Effects/04 Items/Glass/Small_Bottle_Open_Cork", bottleCaps[i].Get<GameObject>(0f));
		}
		else if (statueKeys.Contains(item, 0f))
		{
			int num = statueKeys.IndexOf(item, 0f);
			wallNumbersChestAnimationSamplers[num].play();
		}
		else if (!wellPiecePickedUp && item == exitDoorKeys[2].Get<Item>(0f))
		{
			wellPiecePickedUp = true;
			whispSequence.play(-1f, 0f);
		}
	}

	private void setupLevelComplete()
	{
		if (game.getPlayerCount() <= 3)
		{
			carriageDoorTweenState.transitionTo("Open", 1000f, 0f);
			carriageDoorSwitch.setValue(0f);
			carriageDoorSwitch.targetable = true;
		}
	}

	public override void onSwitch3D(Switch3D targetSwitch, Switch3DEvent switchEvent)
	{
		if (targetSwitch == crowMirrorSwitch)
		{
			enterCrowView();
		}
		else if (UnityUtils.contains(gargoyleDoors, targetSwitch))
		{
			Sequence component = targetSwitch.GetComponent<Sequence>();
			switch (switchEvent)
			{
			case Switch3DEvent.On:
				component.play(0f, 1f);
				break;
			case Switch3DEvent.Off:
				component.play(1f, component.sequenceDuration);
				break;
			}
		}
		else if (targetSwitch == levelExitRef.Get<Switch3D>(0f))
		{
			game.levelCompleted();
			setupLevelComplete();
		}
		else if (targetSwitch == gargoyleLockLocked && switchEvent == Switch3DEvent.Start)
		{
			if (gargoyleLockLocked.TryGetComponent<Sequence>(out var component2))
			{
				component2.play(0f, component2.sequenceDuration);
			}
		}
		else if (targetSwitch == vanHelsingLockLocked && switchEvent == Switch3DEvent.Start)
		{
			if (vanHelsingLockLocked.TryGetComponent<Sequence>(out var component3))
			{
				component3.play(0f, component3.sequenceDuration);
			}
		}
		else if (targetSwitch == carriageDoorLockedRef.Get<Switch3D>(0) && switchEvent == Switch3DEvent.Start)
		{
			carriageDoorLockedRef.Get<Sequence>(0f).play(0f, carriageDoorLockedRef.Get<Sequence>(0f).sequenceDuration);
		}
		else if (targetSwitch == carriageDoorLockedRefSecond.Get<Switch3D>(0) && switchEvent == Switch3DEvent.Start)
		{
			carriageDoorLockedRefSecond.Get<Sequence>(0f).play(0f, carriageDoorLockedRefSecond.Get<Sequence>(0f).sequenceDuration);
		}
		else if (targetSwitch == mausoleumDoorOne.Get<Switch3D>(0) && switchEvent == Switch3DEvent.Start)
		{
			mausoleumDoorOne.Get<Sequence>(0f).play(0f, mausoleumDoorOne.Get<Sequence>(0f).sequenceDuration);
		}
		else if (targetSwitch == mausoleumDoorTwo.Get<Switch3D>(0) && switchEvent == Switch3DEvent.Start)
		{
			mausoleumDoorTwo.Get<Sequence>(0f).play(0f, mausoleumDoorTwo.Get<Sequence>(0f).sequenceDuration);
		}
		else if (targetSwitch == smallMauseoleumDoorOne.Get<Switch3D>(0) && switchEvent == Switch3DEvent.Start)
		{
			smallMauseoleumDoorOne.Get<Sequence>(0f).play(0f, smallMauseoleumDoorOne.Get<Sequence>(0f).sequenceDuration);
		}
		else if (targetSwitch == smallMauseoleumDoorTwo.Get<Switch3D>(0) && switchEvent == Switch3DEvent.Start)
		{
			smallMauseoleumDoorTwo.Get<Sequence>(0f).play(0f, smallMauseoleumDoorTwo.Get<Sequence>(0f).sequenceDuration);
		}
		else if (targetSwitch == mainDoorCircleOne.Get<Switch3D>(0) && switchEvent == Switch3DEvent.Start)
		{
			mainDoorCircleOne.Get<Sequence>(0f).play(0f, mainDoorCircleOne.Get<Sequence>(0f).sequenceDuration);
		}
		else if (targetSwitch == mainDoorCircleTwo.Get<Switch3D>(0) && switchEvent == Switch3DEvent.Start)
		{
			mainDoorCircleTwo.Get<Sequence>(0f).play(0f, mainDoorCircleTwo.Get<Sequence>(0f).sequenceDuration);
		}
		else
		{
			if (!(targetSwitch == vanHelsingBoxTop) || switchEvent != Switch3DEvent.Start)
			{
				return;
			}
			if (targetSwitch.state == Switch3DState.On)
			{
				vanHelsingBoxMiscDrawer.targetable = false;
				if (vanHelsingBoxMiscDrawer.state == Switch3DState.On)
				{
					game.startSwitch(vanHelsingBoxMiscDrawer);
				}
			}
			else if (targetSwitch.state == Switch3DState.Off)
			{
				vanHelsingBoxMiscDrawer.targetable = true;
			}
		}
	}

	public override void onDialMoved(Dial dial, MoveEvent moveEvent)
	{
		if (dial == starBoxDial && dial.hasValueChanged)
		{
			starBoxDrawerPercent = Mathf.Clamp01(starBoxDrawerPercent - dial.angleChange / 360f);
		}
		int num = statueDials.IndexOf(dial, 0f);
		if (num >= 0 && statueSlots[num].Get<Slot>(0).insertedItem != null && !statueDialWasInteracted[num])
		{
			statueDialWasInteracted[num] = true;
		}
	}

	public override void onNpcChoiceSelected(Npc npc, int lineIndex, int choiceIndex)
	{
		UnityEngine.Debug.Log(lineIndex + " " + choiceIndex);
		if (lineIndex == 5)
		{
			game.increaseZoomCounter(wellNpc);
		}
		else if (npc.getChoice(lineIndex, choiceIndex).goToIndex == 500)
		{
			if (currentNpcIndex == 4)
			{
				wellDialogSolved();
			}
			else
			{
				npc.goTo(wellNpcOrder[++currentNpcIndex]);
			}
		}
		else
		{
			resetNpc();
		}
		void resetNpc()
		{
			wellNpcOrder = npc.getRandomizedLines(random.Next(), 1, 5);
			currentNpcIndex = 1;
			npc.goTo(wellNpcOrder[currentNpcIndex]);
		}
	}

	public override void onCustomModeLeave(CustomMode mode)
	{
		if (mode == crowMode)
		{
			PineFmod.playOneShotSoundAttached("event:/Sound Effects/05 Misc/Magic/Crow_Mirror_Off", game.playerRig.gameObject);
		}
	}

	public override void onTool(Item tool, ToolContext context)
	{
		UnityEngine.Debug.Log($"{game.name} {tool} {context} {context.state}");
		if (context.state == ToolState.Start && shovels.Contains(tool, 0f))
		{
			digHoleUsingShovel(context.currentTargetHitPoint, tool);
		}
	}

	public override void onMaterialTransitionDone(MaterialState tweenState, string state)
	{
		if (tweenState == whispMoonMS)
		{
			exitDoorKeys.Get<Item>(2, 0f).targetable = true;
			whispPushSphere.SetActive(value: false);
			whispMoonMover.transitionToDuration("Moved", 2.5f);
		}
		for (int i = 0; i < fireLog.Length; i++)
		{
			if (tweenState == fireLog[i].Get<MaterialState>(0f))
			{
				game.setParent(fireLogVFXTransform, game.levelContainerTransform);
				fireLog[i].Get<GameObject>(0).SetActive(value: false);
			}
		}
	}

	public override void onTweenTransitionDone(TweenState tweenState, string state)
	{
		int num = dirtHolePool.IndexOf(tweenState, 0u);
		int index = UnityUtils.getIndex(statueDialHandleTSs, tweenState);
		if (num >= 0)
		{
			return;
		}
		if (tweenState == wallNumbersChestLockRef.Get<TweenState>(0f))
		{
			wallNumbersChestLid.Get<Switch3D>(0).targetable = true;
		}
		else if (tweenState == fireChestLockSwitchTS)
		{
			game.increaseZoomCounter(fireChestZoomableRef.Get<Zoomable>(0));
			fireChestZoomableRef.Get<Zoomable>(0).targetable = false;
			if (fireChestLock.TryGetComponent<Item>(out var component))
			{
				component.targetable = true;
				component.hasRigidbody = true;
			}
			fireChestLockPartTS.transitionTo("Open");
			game.finishPuzzle(Puzzle.Flames);
		}
		else if (tweenState == fireChestLockPartTS)
		{
			fireChestLid.targetable = true;
			game.startSwitch(fireChestLid);
			game.startTimer(new FireChestPaperTimer(), 0.8f);
		}
		else if (tweenState == gargoyleDoorLockTS)
		{
			gargoyleDoorLockItem.targetable = true;
			gargoyleDoorLockItem.hasRigidbody = true;
			Switch3D[] array = gargoyleDoors;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].targetable = true;
			}
			mausoleumDoorOne.Get<Switch3D>(0).targetable = false;
			mausoleumDoorTwo.Get<Switch3D>(0).targetable = false;
			gargoyleLockLocked.targetable = false;
		}
		else if (tweenState == vanHelsingLockTS)
		{
			vanHelsingLockItem.targetable = true;
			vanHelsingLockItem.hasRigidbody = true;
			vanHelsingLockLocked.targetable = false;
			vanHelsingBoxTopHatchTS.transitionTo("Open", 2f);
		}
		else if (tweenState == vanHelsingBoxTopHatchTS)
		{
			game.startSwitch(vanHelsingBoxTop);
			vanHelsingBoxTop.targetable = true;
		}
		else if (index >= 0 && tweenState.findStateByName(state).weight == 1f)
		{
			statueDials[index].Get<Dial>(0f).targetable = true;
		}
		else if (tweenState == whispMoonMover)
		{
			whispMoonMover.transitionToDuration(whispMoverMoved ? "Default" : "Moved", 2.5f);
			whispMoverMoved = !whispMoverMoved;
		}
	}

	public override void onSequenceDone(Sequence sequence)
	{
		int index = UnityUtils.getIndex(statueSequences, sequence);
		if (sequence == whispSequence)
		{
			if (sequence.sequenceTime == sequence.sequenceDuration)
			{
				whispRising = false;
				whispRefPos = WhispParticles02PSRef.Get<Transform>(0f).localPosition;
				game.startTimer(new Well3Timer(), 1f);
			}
			else
			{
				WhispParticles02.SetActive(value: false);
			}
		}
		else if (index >= 0)
		{
			PineFmod.stop(statueSoundInstances[index], STOP_MODE.IMMEDIATE);
			PineFmod.playOneShotSoundAttached("event:/Sound Effects/04 Items/Concrete & Rock/Place_Statue", statueSequences[index].gameObject);
		}
		else if (sequence == miniStatuesSequence)
		{
			PineFmod.stop(miniStatueSolvedInstance, STOP_MODE.IMMEDIATE);
			PineFmod.playOneShotSoundAttached("event:/Sound Effects/04 Items/Concrete & Rock/Rock_Hit_01", miniStatueSlotTSs[15].gameObject);
		}
		else if (sequence == exitDoorSequence)
		{
			EventInstance[] array = exitDoorGearsSoundInstances;
			for (int i = 0; i < array.Length; i++)
			{
				PineFmod.stop(array[i], STOP_MODE.IMMEDIATE);
			}
			game.startTimer(new FinalDoorTimer(), 1f);
		}
	}

	private bool checkVanHelsingBoxSolved()
	{
		bool flag = true;
		foreach (Slot vanHelsingToolSlot in vanHelsingToolSlots)
		{
			flag &= game.checkSlotSolution(vanHelsingToolSlot, vanHelsingToolSlot.acceptItems[0]);
		}
		return flag;
	}

	private void solveVanHelsingBox()
	{
		vanHelsingToolSlots.ForEach(delegate(Slot x)
		{
			x.targetable = false;
		});
		foreach (Item item in vanHelsingTools.Iterate<Item>(0))
		{
			item.targetable = false;
		}
		game.startTimer(new CrowMirrorTimer(), 0.5f);
		game.finishPuzzle(Puzzle.MirrorChest);
	}

	private void moveSymbolsPointer(int symbolIndex)
	{
		if (symbolIndex >= 0 && symbolIndex <= SymbolsCorrectSolution.Length && !symbolsSolved)
		{
			currentSymbol = symbolIndex;
			PineFmod.playOneShotSoundAttached("event:/Sound Effects/04 Items/Locks/Lock_Open", symbolsPointerRef.Get<GameObject>(0));
			Game obj = game;
			SymbolsPointerTransition transition = new SymbolsPointerTransition(currentSymbol);
			Transform obj2 = symbolsPointerRef.Get<Transform>(0f);
			Quaternion? rotation = symbolsPointerEmpties[symbolIndex].Get<Transform>(0f).localRotation;
			obj.startTransitionLocal(transition, obj2, 0.2f, 0f, null, rotation);
		}
	}

	private bool checkLiarsCorrect()
	{
		bool flag = true;
		for (int i = 0; i < SymbolsCorrectSolution.Length; i++)
		{
			flag &= symbolsCurrentSolution[i] == SymbolsCorrectSolution[i];
		}
		return flag;
	}

	[DebugButton("Solve Veritas Lock", Tint.DarkGreen, PostClickAction.ReturnToGame, 0, new object[] { })]
	private void solveLiars()
	{
		symbolsSlidable.targetable = false;
		symbolsSolved = true;
		symbolsLockTS.transitionTo("Open", 2f, 1f, playSound: true, 1f);
		game.startTimer(new SymbolPointerCorrectTimer(), 1f);
		game.finishPuzzle(Puzzle.Gargoyles);
	}

	private bool isStatueSolved(int statueIndex)
	{
		if (statueKeys.IndexOf(statueSlots[statueIndex].Get<Slot>(0).insertedItem, 0f) != StatueCorrectKeys[statueIndex])
		{
			return false;
		}
		bool flag = true;
		for (int i = 0; i < statueSpikes[statueIndex].spikes.Length; i++)
		{
			flag &= Vector3.Distance(statueSpikes[statueIndex].spikes[i].Get<Transform>(0f).position, statueSpikes[statueIndex].correctPositionsRefs[i].position) <= 0.0035f;
		}
		return flag;
	}

	private void solveStatue(int statueIndex)
	{
		StatueSolved[statueIndex] = true;
		statueSequences[statueIndex].play(-1f, statueSequences[statueIndex].sequenceDuration);
		statueDials[statueIndex].Get<Dial>(0f).targetable = false;
		statueSlots[statueIndex].Get<Slot>(0).targetable = false;
		statueSlots[statueIndex].Get<Slot>(0).insertedItem.targetable = false;
		game.startTimer(new StatueSolvedTimer(statueIndex), 0.5f);
	}

	private bool checkMiniStatues()
	{
		if (game.checkSlotSolution(miniStatueSlots[15], miniStatueGargoyle))
		{
			return game.checkSlotSolution(miniStatueSlots[7], miniStatueAngel);
		}
		return false;
	}

	private void solveMiniStatues()
	{
		miniStatueAngel.targetable = false;
		miniStatueGargoyle.targetable = false;
		Slot[] array = miniStatueSlots;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = false;
		}
		exitDoorKeys.Get<Item>(1, 0f).targetable = true;
		miniStatuesSequence.play(-1f, miniStatuesSequence.sequenceDuration);
		PineFmod.start(miniStatueSolvedInstance);
		miniStatuesSolved = true;
		game.finishPuzzle(Puzzle.HeavenHell);
	}

	private void digHoleUsingShovel(Vector3 shovelPosition, Item shovel)
	{
		string text = game.name;
		Vector3 vector = shovelPosition;
		UnityEngine.Debug.Log(text + " " + vector.ToString());
		if (tryDigUpItemAt(out var diggableItem))
		{
			diggableItem.gameObjectRef.Get<Transform>(0f).position = new Vector3(shovelPosition.x, diggableItem.gameObjectRef.Get<Transform>(0f).position.y, shovelPosition.z);
			if (diggableItem.gameObjectRef.Get<GameObject>(0).TryGetComponent<Item>(out var component))
			{
				component.targetable = true;
			}
			game.startTransitionGlobal(new DigUpItemTransition(), diggableItem.gameObjectRef, shovelItemMoveDuration, shovelItemMoveDelay + 1f, diggableItem.gameObjectRef.Get<Transform>(0f).position + new Vector3(0f, diggableItem.digRaiseDistance, 0f));
			if (diggableItemWasDug[0] && diggableItemWasDug[1])
			{
				setStarSlotsTargetable(canPlace: false);
				game.finishPuzzle(Puzzle.BlueFlame);
			}
		}
		shovel.hideItemInHand = true;
		if (!tryGetHoleIndexFromPool(out var nextHoleIndex))
		{
			return;
		}
		Ref<GameObject, Transform, TweenState> obj = dirtHolePool[nextHoleIndex];
		obj.Get<GameObject>(0).SetActive(value: true);
		shovelPool[nextHoleIndex].Get<GameObject>(0f).SetActive(value: true);
		shovelPool[nextHoleIndex].Get<AnimationSampler>(0u).enabled = false;
		obj.Get<Transform>(0f).position = new Vector3(shovelPosition.x, obj.Get<Transform>(0f).position.y, shovelPosition.z);
		obj.Get<Transform>(0f).rotation = Quaternion.Euler(obj.Get<Transform>(0f).eulerAngles.x, game.headPov.eulerAngles.y + 150f, obj.Get<Transform>(0f).eulerAngles.z);
		Transform transform = shovelAnimationMoving[nextHoleIndex];
		Vector3 position = transform.position;
		Quaternion rotation = transform.rotation;
		Vector3 localScale = transform.localScale;
		Transform transform2 = null;
		if (game.hasAuthority(shovel))
		{
			GameObject impostorInHand = game.getImpostorInHand();
			if (impostorInHand != null)
			{
				transform2 = impostorInHand.transform.GetChild(0).GetChild(0);
			}
		}
		else
		{
			Game.GamePlayerData playerWithItemInInventory = game.getPlayerWithItemInInventory(shovel.gameObject);
			if (playerWithItemInInventory != null)
			{
				GameObject inHandItemBubble = playerWithItemInInventory.inHandItemBubble;
				if (inHandItemBubble != null)
				{
					transform2 = inHandItemBubble.transform.GetChild(0).GetChild(0).GetChild(0);
				}
			}
		}
		if (transform2 != null)
		{
			transform.position = transform2.position;
			transform.rotation = transform2.rotation;
			transform.localScale = new Vector3(transform2.lossyScale.x, transform2.lossyScale.y, transform2.lossyScale.z);
		}
		game.startTransitionGlobal(new ShovelFlyInTransition(nextHoleIndex, shovel, shovelPosition), transform, 1f, 0f, position, rotation, localScale);
		if (prevShovelDecal != null)
		{
			game.startTimer(new DecalFadeOutTimer(prevShovelDecal), dirtHoleFadeOutDuration);
			game.startTimer(new HoleEffect2Timer(prevShovelDecalGo, prevShovelDecal), dirtHoleFadeOutDuration);
		}
		bool tryDigUpItemAt(out DiggableItem reference)
		{
			reference = null;
			for (int num = diggableItems.Count - 1; num >= 0; num--)
			{
				if (!((shovelPosition - diggableItems[num].gameObjectRef.Get<Transform>(0f).position).magnitude > diggableItems[num].digRadius) && !diggableItemWasDug[num])
				{
					reference = diggableItems[num];
					diggableItemWasDug[num] = true;
					break;
				}
			}
			return reference != null;
		}
		bool tryGetHoleIndexFromPool(out int reference)
		{
			reference = dirtHolePool.FindIndex<GameObject>((GameObject dirtHole) => !dirtHole.activeSelf);
			if (reference < 0)
			{
				UnityEngine.Debug.Log("Not enough holes in the object pool");
			}
			return reference >= 0;
		}
	}

	private void OnDrawGizmos()
	{
		drawDiggableItemGizmos();
	}

	private void drawDiggableItemGizmos()
	{
		if (diggableItems == null)
		{
			return;
		}
		foreach (DiggableItem diggableItem in diggableItems)
		{
			Gizmos.color = digRadiusGizmosColor;
			Gizmos.DrawSphere(diggableItem.gameObjectRef.Get<Transform>(0f).position, diggableItem.digRadius);
			Gizmos.color = digRaiseDistanceGizmosColor;
			if (diggableItem.gameObjectRef.Get<GameObject>(0).TryGetComponent<MeshFilter>(out var component))
			{
				Gizmos.DrawMesh(component.sharedMesh, component.transform.position + Vector3.up * diggableItem.digRaiseDistance, component.transform.rotation, component.transform.lossyScale);
			}
			else
			{
				Gizmos.DrawSphere(diggableItem.gameObjectRef.Get<GameObject>(0).transform.position + Vector3.up * diggableItem.digRaiseDistance, digRaiseDistanceGizmosRadius);
			}
		}
	}

	private bool canStarBePlaced()
	{
		if (diggableItemWasDug[0])
		{
			return UnityUtils.closeEnough(starSlidable.value, 1f, 0.1f);
		}
		return false;
	}

	private void setStarSlotsTargetable(bool canPlace)
	{
		starBoxSlot.targetable = canPlace;
		starBoxSlotWrong.targetable = !canPlace;
	}

	private bool checkWellSlidersSolved()
	{
		bool flag = true;
		for (int i = 0; i < wellSliders.Count; i++)
		{
			flag &= wellSlidersCurrent[i] == WellSlidersSolutions[i];
		}
		return flag;
	}

	[DebugButton("Solve Well Sliders", Tint.DarkGreen, PostClickAction.ReturnToGame, 0, new object[] { })]
	private void wellSolveSliders()
	{
		if (wellSolved)
		{
			return;
		}
		UnityEngine.Debug.Log("Well solved!");
		wellSolved = true;
		game.killAllPointers();
		foreach (HorizontalSlider wellSlider in wellSliders)
		{
			wellSlider.targetable = false;
		}
		Zoomable[] array = wellLockZoomables;
		foreach (Zoomable zoomable in array)
		{
			game.increaseZoomCounter(zoomable);
			zoomable.targetable = false;
		}
		wellAmbiance.SetActive(value: true);
		whispCutSceneGameObject.SetActive(value: true);
		game.startTimer(new Well1Timer(), WellSequenceDelay);
		game.finishPuzzle(Puzzle.Mirror);
	}

	private void wellDialogSolved()
	{
		UnityEngine.Debug.Log("Dialog whisp solved!");
		game.increaseZoomCounter(wellNpc);
		WhispParticles02PSRef.Get<ParticleSystem>(0).Stop();
		WhispParticles02Colliders.SetActive(value: false);
		whispVE.Stop();
		wellAmbiance.SetActive(value: false);
		whispAppearVFX.Play();
		game.startTimer(new WellSolveTimer(), 0.2f);
		PineFmod.playOneShotSoundAttached("event:/Sound Effects/05 Misc/Magic/Crow_Mirror_Off", WhispParticles02);
		game.finishPuzzle(Puzzle.Whisp);
	}

	private void moveFire(Ref<GameObject, Transform, ParticleSystem> fire, RefArray<GameObject, Transform> firePoints, FireMovement fireMovement, bool flaming)
	{
		FirePosition i = FirePosition.Center;
		FirePosition i2 = FirePosition.Center;
		switch (fireMovement)
		{
		case FireMovement.StayUp:
			i = (i2 = FirePosition.Up);
			break;
		case FireMovement.StayCenter:
			i = (i2 = FirePosition.Center);
			break;
		case FireMovement.StayDown:
			i = (i2 = FirePosition.Down);
			break;
		case FireMovement.MoveUp:
			i = FirePosition.Down;
			i2 = FirePosition.Up;
			break;
		case FireMovement.MoveDown:
			i = FirePosition.Up;
			i2 = FirePosition.Down;
			break;
		}
		if (flaming)
		{
			fire.Get<ParticleSystem>(0u).Play();
			game.startTransitionGlobal(new FireTransition(), fire.Get<Transform>(0f), 2.1f, 0f, firePoints[(int)i2].Get<Transform>(0f).position);
		}
		else
		{
			game.startTransitionGlobal(fire.Get<Transform>(0f), 1.25f, 0.25f, firePoints[(int)i].Get<Transform>(0f).position);
		}
	}

	[DebugButton("Solve Carriage Door", Tint.DarkGreen, PostClickAction.ReturnToGame, 0, new object[] { })]
	private bool checkCarriageDoorSolved()
	{
		bool flag = true;
		for (int i = 0; i < carriageDoorSlidabeGraph.piecesList.Count; i++)
		{
			if (carriageDoorSlidabeGraph.tryGetNode(carriageDoorSlidabeGraph.piecesList[i], out var node))
			{
				int num = carriageDoorSlidabeGraph.nodes.IndexOf(node);
				flag &= CarriageDoorSlidableGraphCorrectNodes[i] == num;
			}
			else
			{
				flag = false;
			}
		}
		return flag;
	}

	private void carriageDoorSolved()
	{
		for (int i = 0; i < carriageDoorSlidabeGraph.piecesList.Count; i++)
		{
			GameObject gameObject = carriageDoorSlidabeGraph.nodes[CarriageDoorSlidableGraphCorrectNodes[i]];
			carriageDoorSlidabeGraph.piecesList[i].transform.position = gameObject.transform.position;
		}
		if (game.isInTopZoom(carriageDoorZoomableRef))
		{
			game.killAllPointers();
		}
		foreach (SlidableGraphPiece pieces in carriageDoorSlidabeGraph.piecesList)
		{
			pieces.targetable = false;
		}
		Quaternion[] array = new Quaternion[carriageDoorSlidabeGraph.piecesList.Count];
		for (int j = 0; j < array.Length; j++)
		{
			SlidableGraphPiece slidableGraphPiece = carriageDoorSlidabeGraph.piecesList[j];
			array[j] = slidableGraphPiece.transform.localRotation;
		}
		game.startTimer(new CarriageDoorAnimationTimer(carriageDoorSlidabeGraph.piecesList[0].transform.localPosition.y, array), 2f);
		game.startTimer(new CarriageDoorSolvedTimer(), 2.25f);
		PineFmod.start(carriageDoorSolvedInstance);
		game.finishPuzzle(Puzzle.Carriage);
	}

	private void enterCrowView()
	{
		if (game.isInInventory(crowMirrorRef.Get<Item>((short)0)))
		{
			crowModeWasEntered = true;
			game.increaseZoomCounter(crowMirrorRef.Get<Item>((short)0));
			game.removeSelectedItem(crowMirrorRef.Get<GameObject>(0));
			game.handleCustomModeEnter(crowMode, shouldAllPlayersEnter: false);
			PineFmod.playOneShotSoundAttached("event:/Sound Effects/05 Misc/Magic/Crow_Mirror_On", crowMirrorRef.Get<GameObject>(0));
		}
	}

	private bool checkExitDoorSolved()
	{
		return exitDoorSlots.FindAll((Slot slot) => game.checkSlotSolution(slot, slot.acceptItems[0])).Count >= exitDoorSlots.Count;
	}

	private void solveExitDoor()
	{
		exitDoorSolved = true;
		foreach (Slot exitDoorSlot in exitDoorSlots)
		{
			exitDoorSlot.targetable = false;
		}
		foreach (Item item in exitDoorKeys.Iterate<Item>(0f))
		{
			item.targetable = false;
		}
		EventInstance[] array = exitDoorGearsSoundInstances;
		for (int i = 0; i < array.Length; i++)
		{
			PineFmod.start(array[i]);
		}
		exitDoorSequence.play(-1f, exitDoorSequence.sequenceDuration);
		game.startTimer(new ExitDoorGearsTimer(), exitDoorSequence.sequenceDuration);
		game.finishPuzzle(Puzzle.Bloodmoon);
	}

	private AnimationClip getRatClip(MovementType type, float deltaAngle)
	{
		AnimationClip result = ratWalk;
		switch (type)
		{
		case MovementType.Walk:
			result = ((!UnityUtils.closeEnough(deltaAngle, 0f)) ? ((!(deltaAngle > 0f)) ? ratWalkRight : ratWalkLeft) : ratWalk);
			break;
		case MovementType.Run:
			result = ((!UnityUtils.closeEnough(deltaAngle, 0f)) ? ((!(deltaAngle > 0f)) ? ratRunRight : ratRunLeft) : ratRun);
			break;
		case MovementType.Jump:
			result = ratJump;
			break;
		case MovementType.Sit:
			result = ratSit;
			break;
		}
		return result;
	}

	public override void onInitHints()
	{
		game.setPuzzleConditions(Puzzle.StarChest, Puzzle.BlueFlame);
		game.setPuzzleConditions(Puzzle.Statues, Puzzle.SymbolChest);
		game.setPuzzleConditions(Puzzle.HeavenHell, Puzzle.Statues);
		game.setPuzzleConditions(Puzzle.MirrorChest, Puzzle.Carriage, Puzzle.Flames);
		game.setPuzzleConditions(Puzzle.Mirror, Puzzle.MirrorChest);
		game.setPuzzleConditions(Puzzle.Whisp, Puzzle.Mirror);
		game.setPuzzleConditions(Puzzle.Bloodmoon, Puzzle.Whisp, Puzzle.HeavenHell, Puzzle.Gargoyles, Puzzle.StarChest);
		game.setRelevantObjectsForPuzzle(Puzzle.Carriage, carriageDoorZoomableRef.Get<GameObject>(0), vanHelsingTools.Get<GameObject>(0, 0f), vanHelsingLockKeyRef.Get<GameObject>(0f));
		game.setRelevantObjectsForPuzzle(Puzzle.Gargoyles, symbolsLockItemRef.Get<GameObject>(0u), symbolsHintPaperObject);
		game.setRelevantObjectsForPuzzle(Puzzle.Flames, fireChestZoomableRef.Get<GameObject>(0f), firePaperRef.Get<GameObject>(0u));
		game.setRelevantObjectsForPuzzle(Puzzle.SymbolChest, wallNumbersChestLockRef.Get<GameObject>(0u), wallNumbersSlidableRightRef.Get<GameObject>(0f), wallNumbersSlidableLeftRef, statueKeys.Get<GameObject>(0, 0), statueKeys.Get<GameObject>(1, 0));
		game.setRelevantObjectsForPuzzle(Puzzle.BlueFlame, shovels.Get<GameObject>(0, 0), shovels.Get<GameObject>(1, 0), diggableItems[0].gameObjectRef.Get<GameObject>(0), diggableItems[1].gameObjectRef.Get<GameObject>(0));
		game.setRelevantObjectsForPuzzle(Puzzle.StarChest, diggableItems[0].gameObjectRef.Get<GameObject>(0), diggableItems[1].gameObjectRef.Get<GameObject>(0));
		game.setRelevantObjectsForPuzzle(Puzzle.Statues, statueKeys.Get<GameObject>(0, 0), statueKeys.Get<GameObject>(1, 0), statueZoomables.Get<GameObject>(0, 0f), statueZoomables.Get<GameObject>(1, 0f));
		game.setRelevantObjectsForPuzzle(Puzzle.HeavenHell, gargoyleDoorKeyRef.Get<GameObject>(0f));
		game.setRelevantObjectsForPuzzle(Puzzle.MirrorChest, vanHelsingLockKeyRef.Get<GameObject>(0f), vanHelsingTools.Get<GameObject>(0, 0f), vanHelsingTools.Get<GameObject>(1, 0f), vanHelsingTools.Get<GameObject>(2, 0f));
		game.setRelevantObjectsForPuzzle(Puzzle.Mirror, crowMirrorRef.Get<GameObject>(0));
		game.setRelevantObjectsForPuzzle(Puzzle.Whisp, WhispParticles02);
		game.setRelevantObjectsForPuzzle(Puzzle.Bloodmoon, exitDoorKeys.Get<GameObject>(0, 0), exitDoorKeys.Get<GameObject>(1, 0), exitDoorKeys.Get<GameObject>(2, 0), exitDoorKeys.Get<GameObject>(3, 0));
		game.setHintCondition(Puzzle.Carriage, CarriageHint.CleanWindow, () => game.getPlayerCount() > 1);
		game.setHintCondition(Puzzle.Carriage, CarriageHint.LookAtGraph, () => game.wasLookedAtCurrentPuzzle(carriageDoorZoomableRef.Get<GameObject>(0)));
		game.setHintCondition(Puzzle.Gargoyles, GargoylesHint.LookAtHint, () => game.wasAddedToInventoryDuringCurrentPuzzle(symbolsHintPaperObject));
		game.setHintCondition(Puzzle.Gargoyles, GargoylesHint.LookAtLock, () => game.wasLookedAtCurrentPuzzle(symbolsLockZoomableRef.Get<GameObject>(0f)));
		game.setHintCondition(Puzzle.Flames, FlamesHint.GetHint, () => game.wasAddedToInventoryDuringCurrentPuzzle(firePaperRef.Get<GameObject>(0u)));
		game.setHintCondition(Puzzle.Flames, FlamesHint.OpenTurnables, () => fireChestTurnablesSwitch.state == Switch3DState.On);
		game.setHintCondition(Puzzle.SymbolChest, SymbolChestHint.GetBox, () => game.wasAddedToInventoryDuringCurrentPuzzle(wallNumbersChestLockRef.Get<GameObject>(0u)));
		game.setHintCondition(Puzzle.SymbolChest, SymbolChestHint.FirstSlidableSymbol, () => symbolSlidablesInteractedWith || wallNumbersChestLockRef.Get<Lock>(0).currentValues[0] == wallNumbersChestLockRef.Get<Lock>(0).password[0]);
		game.setHintCondition(Puzzle.SymbolChest, SymbolChestHint.LookAtNumber, () => wallNumbersChestLockRef.Get<Lock>(0).currentValues[0] == wallNumbersChestLockRef.Get<Lock>(0).password[0]);
		game.setHintCondition(Puzzle.SymbolChest, SymbolChestHint.SolveFirst, () => wallNumbersChestLockRef.Get<Lock>(0).currentValues[0] == wallNumbersChestLockRef.Get<Lock>(0).password[0]);
		game.setHintCondition(Puzzle.BlueFlame, BlueFlameHint.GetShovel, () => game.wasAnyAddedToInventoryDuringCurrentPuzzle(shovels.ToArray<GameObject>(0)));
		game.setHintCondition(Puzzle.BlueFlame, BlueFlameHint.GetHint, () => game.wasAddedToInventoryDuringCurrentPuzzle(flamePaperHint));
		game.setHintCondition(Puzzle.BlueFlame, BlueFlameHint.LookAtLanterns, () => diggableItemWasDug[0] || diggableItemWasDug[1]);
		game.setHintCondition(Puzzle.BlueFlame, BlueFlameHint.LookTroughLantern, () => diggableItemWasDug[0] || diggableItemWasDug[1]);
		game.setHintCondition(Puzzle.BlueFlame, BlueFlameHint.DigStar, () => diggableItemWasDug[1]);
		game.setHintCondition(Puzzle.StarChest, StarChestHint.UnlockStar, () => starBoxSlot.isUnlocked || starBoxSlot.targetable);
		game.setHintCondition(Puzzle.StarChest, StarChestHint.GetBox, () => starBoxSlot.isUnlocked || game.wasAddedToInventoryDuringCurrentPuzzle(starBox.gameObject));
		game.setHintCondition(Puzzle.StarChest, StarChestHint.PlaceStar, () => starBoxSlot.isUnlocked);
		game.setHintCondition(Puzzle.Statues, StatuesHint.GetItems, () => game.wasAnyAddedToInventoryDuringCurrentPuzzle(statueKeys.ToArray<GameObject>(0)));
		game.setHintCondition(Puzzle.Statues, StatuesHint.PlaceItems, () => statueSlots[0].Get<Slot>(0).isUnlocked && statueSlots[1].Get<Slot>(0).isUnlocked);
		game.setHintCondition(Puzzle.Statues, StatuesHint.TurnDial, () => statueDialWasInteracted[0] || statueDialWasInteracted[1]);
		game.setHintCondition(Puzzle.Statues, StatuesHint.SolveFirst, () => StatueSolved[0]);
		game.setHintCondition(Puzzle.HeavenHell, HeavenHellHint.GetStatues, () => gargoyleDoorKeySlot.isUnlocked || (game.wasAddedToInventoryDuringCurrentPuzzle(miniStatueAngel.gameObject) && game.wasAddedToInventoryDuringCurrentPuzzle(miniStatueGargoyle.gameObject)));
		game.setHintCondition(Puzzle.HeavenHell, HeavenHellHint.GetKey, () => gargoyleDoorKeySlot.isUnlocked || game.wasAddedToInventoryDuringCurrentPuzzle(gargoyleDoorKeyRef.Get<GameObject>(0f)));
		game.setHintCondition(Puzzle.HeavenHell, HeavenHellHint.UnlockMusoleum, () => gargoyleDoorKeySlot.isUnlocked);
		game.setHintCondition(Puzzle.HeavenHell, HeavenHellHint.SolveAngel, () => game.checkSlotSolution(miniStatueSlots[7], miniStatueAngel));
		game.setHintCondition(Puzzle.MirrorChest, MirrorChestHint.GetKeyAndFlask, () => game.wasAddedToInventoryDuringCurrentPuzzle(vanHelsingTools.Get<GameObject>(0, 0f)) && game.wasAddedToInventoryDuringCurrentPuzzle(vanHelsingLockKeyRef.Get<GameObject>(0f)));
		game.setHintCondition(Puzzle.MirrorChest, MirrorChestHint.GetHammer, () => game.wasAddedToInventoryDuringCurrentPuzzle(vanHelsingTools.Get<GameObject>(2, 0f)));
		game.setHintCondition(Puzzle.MirrorChest, MirrorChestHint.GetStake, () => game.wasAddedToInventoryDuringCurrentPuzzle(vanHelsingTools.Get<GameObject>(1, 0f)));
		game.setHintCondition(Puzzle.MirrorChest, MirrorChestHint.UnlockBox, () => vanHelsingLockSlot.isUnlocked);
		game.setHintCondition(Puzzle.Mirror, MirrorHint.GetMirror, () => crowModeWasEntered || game.wasAddedToInventoryDuringCurrentPuzzle(crowMirrorRef.Get<GameObject>(0)));
		game.setHintCondition(Puzzle.Mirror, MirrorHint.LookAtSlidables, () => game.wasAnyLookedAtCurrentPuzzle(Array.ConvertAll(wellLockZoomables, (Zoomable z) => z.gameObject)));
		game.setHintCondition(Puzzle.Mirror, MirrorHint.EnterCrowMode, () => crowModeWasEntered);
		game.setHintCondition(Puzzle.Whisp, WhispHint.GetBag, () => game.wasAddedToInventoryDuringCurrentPuzzle(wellBag));
		game.setHintCondition(Puzzle.Whisp, WhispHint.GetBook, () => game.wasAddedToInventoryDuringCurrentPuzzle(wellBook));
		game.setHintCondition(Puzzle.Whisp, WhispHint.LookAtNpc, () => game.wasLookedAtCurrentPuzzle(wellNpc.gameObject));
		game.setHintCondition(Puzzle.Bloodmoon, BloodmoonHint.GetWellMoon, () => game.wasAddedToInventoryDuringCurrentPuzzle(exitDoorKeys[2].Get<GameObject>(0)));
		game.setHintCondition(Puzzle.Bloodmoon, BloodmoonHint.GetLiarsMoon, () => game.wasAddedToInventoryDuringCurrentPuzzle(exitDoorKeys[3].Get<GameObject>(0)));
		game.setHintCondition(Puzzle.Bloodmoon, BloodmoonHint.GetStatuesMoon, () => game.wasAddedToInventoryDuringCurrentPuzzle(exitDoorKeys[1].Get<GameObject>(0)));
		game.setHintCondition(Puzzle.Bloodmoon, BloodmoonHint.GetStarBoxMoon, () => game.wasAddedToInventoryDuringCurrentPuzzle(exitDoorKeys[0].Get<GameObject>(0)));
	}

	[DebugButton("Get Van Helsing Key", Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void debugGetVanHelsingKey()
	{
		vanHelsingLockKeyRef.Get<Item>(0).targetable = true;
		game.addItemToInventory(vanHelsingLockKeyRef.Get<GameObject>(0f));
	}

	[DebugButton("Get Van Helsing Tools", Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void debugGetVanHelsingTools()
	{
		foreach (Ref<Item, GameObject> vanHelsingTool in vanHelsingTools)
		{
			vanHelsingTool.Get<Item>(0).targetable = true;
			game.addItemToInventory(vanHelsingTool.Get<GameObject>(0f));
		}
	}

	[DebugButton("Get Statue Key", Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void debugGetStatueKey()
	{
		gargoyleDoorKeyRef.Get<Item>(0).targetable = true;
		game.addItemToInventory(gargoyleDoorKeyRef.Get<Item>(0).gameObject);
	}

	[DebugButton("Get Statue Items", Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void debugGetStatueItems()
	{
		foreach (GameObject item in statueKeys.Iterate<GameObject>(0))
		{
			game.addItemToInventory(item);
		}
	}

	[DebugButton("Get Mini Statues", Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void debugGetMiniStatues()
	{
		miniStatueAngel.targetable = true;
		miniStatueGargoyle.targetable = true;
		game.addItemToInventory(miniStatueAngel.gameObject);
		game.addItemToInventory(miniStatueGargoyle.gameObject);
	}

	[DebugButton("Get Mirror", Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void debugGetMirror()
	{
		game.addItemToInventory(crowMirrorRef);
	}

	[DebugButton("Get Shovel", Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void debugGetShovel()
	{
		game.addItemToInventory(shovels[0]);
	}

	[DebugButton("Get Star Key and Box", Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void debugGetStarKeyAndBox()
	{
		starBox.targetable = true;
		starBoxKeyRef.Get<Item>(0).targetable = true;
		game.addItemToInventory(starBox.gameObject);
		game.addItemToInventory(starBoxKeyRef.Get<Item>(0).gameObject);
	}

	[DebugButton("Get Exit Door Keys", Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void debugGetExitDoorKeys()
	{
		foreach (GameObject item in exitDoorKeys.Iterate<GameObject>(0))
		{
			game.addItemToInventory(item);
		}
	}

	public virtual void save(FastBinaryWriter writer)
	{
		writer.Write(in symbolSlidablesInteractedWith, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in carriageDoorOpened, default(FastBinaryWriter.ForPrimitives));
		writer.WriteArray(StatueSolved, delegate(FastBinaryWriter w, bool e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteArray(statueDialWasInteracted, delegate(FastBinaryWriter w, bool e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.Write(in miniStatuesSolved, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in symbolsSolved, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in currentSymbol, default(FastBinaryWriter.ForPrimitives));
		writer.WriteArray(symbolsCurrentSolution, delegate(FastBinaryWriter w, bool e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.Write(in crowMirrorPickedUp, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in crowModeWasEntered, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in crowMirrorDecalFade, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in crowCurrentHeight, default(FastBinaryWriter.ForPrimitives));
		writer.WriteVector3(in crowLastPosition);
		writer.Write(in crowMoving, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in exitDoorSolved, default(FastBinaryWriter.ForPrimitives));
		writer.WriteArray(wellSlidersCurrent, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.Write(in wellSolved, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in whispRising, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in whispFloatTimer, default(FastBinaryWriter.ForPrimitives));
		writer.WriteVector3(in whispRefPos);
		writer.Write(in movingDown, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in wellPiecePickedUp, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in whispMoverMoved, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in starBoxDrawerPercent, default(FastBinaryWriter.ForPrimitives));
		writer.WriteGameObject(prevShovelDecalGo);
		writer.WriteComponent(prevShovelDecal);
		writer.WriteVector3(in shovelStartOffsetPos);
		writer.WriteQuaternion(in shovelStartOffsetRot);
		writer.WriteArray(diggableItemWasDug, delegate(FastBinaryWriter w, bool e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		int value = (int)fireState;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in fireMovementIndex, default(FastBinaryWriter.ForPrimitives));
		writer.WriteArray(ratAnimationCurrentTime, delegate(FastBinaryWriter w, float e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteArray(ratAnimationLookToPointRot, delegate(FastBinaryWriter w, Quaternion e)
		{
			w.WriteQuaternion(in e);
		});
		writer.Write(in ratLastAP, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in ratTargetAP, default(FastBinaryWriter.ForPrimitives));
		writer.WriteArray(crowSamples, delegate(FastBinaryWriter w, float e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.Write(in crowsFlewAway, default(FastBinaryWriter.ForPrimitives));
		writer.WriteArray(wellNpcOrder, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.Write(in currentNpcIndex, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in wasTeleportedOut, default(FastBinaryWriter.ForPrimitives));
		writer.WriteArray(disableInSplitscreen, delegate(FastBinaryWriter w, GameObject e)
		{
			w.WriteGameObject(e);
		});
	}

	public virtual void load(FastBinaryReader reader)
	{
		symbolSlidablesInteractedWith = reader.ReadBoolean();
		carriageDoorOpened = reader.ReadBoolean();
		StatueSolved = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		statueDialWasInteracted = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		miniStatuesSolved = reader.ReadBoolean();
		symbolsSolved = reader.ReadBoolean();
		currentSymbol = reader.ReadInt32();
		symbolsCurrentSolution = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		crowMirrorPickedUp = reader.ReadBoolean();
		crowModeWasEntered = reader.ReadBoolean();
		crowMirrorDecalFade = reader.ReadSingle();
		crowCurrentHeight = reader.ReadSingle();
		crowLastPosition = reader.ReadVector3();
		crowMoving = reader.ReadBoolean();
		exitDoorSolved = reader.ReadBoolean();
		wellSlidersCurrent = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		wellSolved = reader.ReadBoolean();
		whispRising = reader.ReadBoolean();
		whispFloatTimer = reader.ReadSingle();
		whispRefPos = reader.ReadVector3();
		movingDown = reader.ReadBoolean();
		wellPiecePickedUp = reader.ReadBoolean();
		whispMoverMoved = reader.ReadBoolean();
		starBoxDrawerPercent = reader.ReadSingle();
		prevShovelDecalGo = reader.ReadGameObject();
		prevShovelDecal = reader.ReadComponent<DecalProjector>();
		shovelStartOffsetPos = reader.ReadVector3();
		shovelStartOffsetRot = reader.ReadQuaternion();
		diggableItemWasDug = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		fireState = (FireState)reader.ReadInt32();
		fireMovementIndex = reader.ReadInt32();
		ratAnimationCurrentTime = reader.ReadArray((FastBinaryReader r) => r.ReadSingle());
		ratAnimationLookToPointRot = reader.ReadArray((FastBinaryReader r) => r.ReadQuaternion());
		ratLastAP = reader.ReadInt32();
		ratTargetAP = reader.ReadInt32();
		crowSamples = reader.ReadArray((FastBinaryReader r) => r.ReadSingle());
		crowsFlewAway = reader.ReadBoolean();
		wellNpcOrder = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		currentNpcIndex = reader.ReadInt32();
		wasTeleportedOut = reader.ReadBoolean();
		disableInSplitscreen = reader.ReadArray((FastBinaryReader r) => r.ReadGameObject());
	}

	public virtual void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		int position = reader.Position;
		bool flag = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "symbolSlidablesInteractedWith",
			fieldValue = $"{flag}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag2 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "carriageDoorOpened",
			fieldValue = $"{flag2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool[] array = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "StatueSolved[" + ((array == null) ? string.Empty : array.Length.ToString()) + "]",
			fieldValue = (((array == null) ? "null" : string.Join(", ", array)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool[] array2 = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "statueDialWasInteracted[" + ((array2 == null) ? string.Empty : array2.Length.ToString()) + "]",
			fieldValue = (((array2 == null) ? "null" : string.Join(", ", array2)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag3 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "miniStatuesSolved",
			fieldValue = $"{flag3}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag4 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "symbolsSolved",
			fieldValue = $"{flag4}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "currentSymbol",
			fieldValue = $"{num}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool[] array3 = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "symbolsCurrentSolution[" + ((array3 == null) ? string.Empty : array3.Length.ToString()) + "]",
			fieldValue = (((array3 == null) ? "null" : string.Join(", ", array3)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag5 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "crowMirrorPickedUp",
			fieldValue = $"{flag5}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag6 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "crowModeWasEntered",
			fieldValue = $"{flag6}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num2 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "crowMirrorDecalFade",
			fieldValue = $"{num2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num3 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "crowCurrentHeight",
			fieldValue = $"{num3}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Vector3 vector = reader.ReadVector3();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "crowLastPosition",
			fieldValue = $"{vector}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag7 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "crowMoving",
			fieldValue = $"{flag7}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag8 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "exitDoorSolved",
			fieldValue = $"{flag8}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int[] array4 = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "wellSlidersCurrent[" + ((array4 == null) ? string.Empty : array4.Length.ToString()) + "]",
			fieldValue = (((array4 == null) ? "null" : string.Join(", ", array4)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag9 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "wellSolved",
			fieldValue = $"{flag9}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag10 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "whispRising",
			fieldValue = $"{flag10}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num4 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "whispFloatTimer",
			fieldValue = $"{num4}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Vector3 vector2 = reader.ReadVector3();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "whispRefPos",
			fieldValue = $"{vector2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag11 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "movingDown",
			fieldValue = $"{flag11}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag12 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "wellPiecePickedUp",
			fieldValue = $"{flag12}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag13 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "whispMoverMoved",
			fieldValue = $"{flag13}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num5 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "starBoxDrawerPercent",
			fieldValue = $"{num5}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		GameObject arg = reader.ReadGameObject();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "prevShovelDecalGo",
			fieldValue = $"{arg}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		DecalProjector arg2 = reader.ReadComponent<DecalProjector>();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "prevShovelDecal",
			fieldValue = $"{arg2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Vector3 vector3 = reader.ReadVector3();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "shovelStartOffsetPos",
			fieldValue = $"{vector3}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Quaternion quaternion = reader.ReadQuaternion();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "shovelStartOffsetRot",
			fieldValue = $"{quaternion}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool[] array5 = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "diggableItemWasDug[" + ((array5 == null) ? string.Empty : array5.Length.ToString()) + "]",
			fieldValue = (((array5 == null) ? "null" : string.Join(", ", array5)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		FireState fireState = (FireState)reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "fireState",
			fieldValue = $"{fireState}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num6 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "fireMovementIndex",
			fieldValue = $"{num6}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float[] array6 = reader.ReadArray((FastBinaryReader r) => r.ReadSingle());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "ratAnimationCurrentTime[" + ((array6 == null) ? string.Empty : array6.Length.ToString()) + "]",
			fieldValue = (((array6 == null) ? "null" : string.Join(", ", array6)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Quaternion[] array7 = reader.ReadArray((FastBinaryReader r) => r.ReadQuaternion());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "ratAnimationLookToPointRot[" + ((array7 == null) ? string.Empty : array7.Length.ToString()) + "]",
			fieldValue = (((array7 == null) ? "null" : string.Join(", ", array7)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num7 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "ratLastAP",
			fieldValue = $"{num7}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num8 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "ratTargetAP",
			fieldValue = $"{num8}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float[] array8 = reader.ReadArray((FastBinaryReader r) => r.ReadSingle());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "crowSamples[" + ((array8 == null) ? string.Empty : array8.Length.ToString()) + "]",
			fieldValue = (((array8 == null) ? "null" : string.Join(", ", array8)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag14 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "crowsFlewAway",
			fieldValue = $"{flag14}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int[] array9 = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "wellNpcOrder[" + ((array9 == null) ? string.Empty : array9.Length.ToString()) + "]",
			fieldValue = (((array9 == null) ? "null" : string.Join(", ", array9)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num9 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "currentNpcIndex",
			fieldValue = $"{num9}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag15 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "wasTeleportedOut",
			fieldValue = $"{flag15}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		GameObject[] array10 = reader.ReadArray((FastBinaryReader r) => r.ReadGameObject());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "disableInSplitscreen[" + ((array10 == null) ? string.Empty : array10.Length.ToString()) + "]",
			fieldValue = (((array10 == null) ? "null" : string.Join(", ", (IEnumerable<GameObject>)array10)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
	}

	public override Timer getTimer(byte id)
	{
		return id switch
		{
			0 => new FireChestPaperTimer(), 
			1 => new CrowMirrorTimer(), 
			2 => new StarBoxTimer(), 
			3 => new StatueSolvedTimer(), 
			4 => new SymbolPointerCorrectTimer(), 
			5 => new SymbolPointerNotCorrectTimer(), 
			6 => new Well1Timer(), 
			7 => new Well2_1Timer(), 
			8 => new Well2_2Timer(), 
			9 => new Well3Timer(), 
			10 => new WellSolveTimer(), 
			11 => new CarriageDoorSolvedTimer(), 
			12 => new FinalDoorTimer(), 
			13 => new DirtEffectTimer(), 
			14 => new HoleEffectTimer(), 
			15 => new HoleEffect2Timer(), 
			16 => new DecalFadeTimer(), 
			17 => new CarriageDoorAnimationTimer(), 
			18 => new DecalFadeOutTimer(), 
			19 => new ExitDoorGearsTimer(), 
			20 => new ExitDoorGearCorrectSlotTimer(), 
			21 => new SymbolsGearTransition(), 
			22 => new FireTransition(), 
			23 => new FireTimer(), 
			24 => new StatueRotationTransition(), 
			25 => new SymbolsPointerTransition(), 
			26 => new ExitDoorGearsTransition(), 
			27 => new ChestOpenTransition(), 
			28 => new DigUpItemTransition(), 
			29 => new ShovelFlyInTransition(), 
			30 => new ShovelFlyBackTimer(), 
			_ => null, 
		};
	}
}
