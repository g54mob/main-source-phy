using System;
using System.Collections.Generic;
using System.Text;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;

public class Dracula3Logic : LevelLogic, ISaveable
{
	public sealed class BirdCage1Transition : Transition
	{
		public override byte getTypeId()
		{
			return 0;
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

	public sealed class OpenBalconyDoorTimer : Timer
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

	public sealed class SolvePipesTimer : Timer
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

	public sealed class OpenBathroomDoorTimer : Timer
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

	public sealed class SolvePendantTextTimer : Timer
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

	public sealed class DisablePendantTextTimer : Timer
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

	public sealed class BedButtons1Timer : Timer
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

	public sealed class BedButtons2Timer : Timer
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

	public sealed class SolveBedChestTimer : Timer
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

	public sealed class SolveBC2Timer : Timer
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

	public sealed class BC2DoorSoundTimer : Timer
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

	public sealed class BC1DoorSoundTimer : Timer
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

	public sealed class BC1DoorTimer : Timer
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

	public sealed class EnterDarkWorldTimer : Timer
	{
		public NetPlayerId playerId;

		public override byte getTypeId()
		{
			return 13;
		}

		public EnterDarkWorldTimer()
		{
		}

		public EnterDarkWorldTimer(NetPlayerId playerId)
		{
			this.playerId = playerId;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.WriteNetPlayerId(playerId);
		}

		public override void readData(FastBinaryReader reader)
		{
			playerId = reader.ReadNetPlayerId();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("playerId: " + $"{playerId}");
			return stringBuilder.ToString();
		}
	}

	public sealed class EnterDarkWorldTransitionTimer : Timer
	{
		public NetPlayerId playerId;

		public override byte getTypeId()
		{
			return 14;
		}

		public EnterDarkWorldTransitionTimer()
		{
		}

		public EnterDarkWorldTransitionTimer(NetPlayerId playerId)
		{
			this.playerId = playerId;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.WriteNetPlayerId(playerId);
		}

		public override void readData(FastBinaryReader reader)
		{
			playerId = reader.ReadNetPlayerId();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("playerId: " + $"{playerId}");
			return stringBuilder.ToString();
		}
	}

	public sealed class ExitDarkWorldTimer : Timer
	{
		public NetPlayerId playerId;

		public override byte getTypeId()
		{
			return 15;
		}

		public ExitDarkWorldTimer()
		{
		}

		public ExitDarkWorldTimer(NetPlayerId playerId)
		{
			this.playerId = playerId;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.WriteNetPlayerId(playerId);
		}

		public override void readData(FastBinaryReader reader)
		{
			playerId = reader.ReadNetPlayerId();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("playerId: " + $"{playerId}");
			return stringBuilder.ToString();
		}
	}

	public sealed class ExitDarkWorldTransitionTimer : Timer
	{
		public NetPlayerId playerId;

		public override byte getTypeId()
		{
			return 16;
		}

		public ExitDarkWorldTransitionTimer()
		{
		}

		public ExitDarkWorldTransitionTimer(NetPlayerId playerId)
		{
			this.playerId = playerId;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.WriteNetPlayerId(playerId);
		}

		public override void readData(FastBinaryReader reader)
		{
			playerId = reader.ReadNetPlayerId();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("playerId: " + $"{playerId}");
			return stringBuilder.ToString();
		}
	}

	public sealed class DiveEffectTimer : Timer
	{
		public NetPlayerId playerId;

		public override byte getTypeId()
		{
			return 17;
		}

		public DiveEffectTimer()
		{
		}

		public DiveEffectTimer(NetPlayerId playerId)
		{
			this.playerId = playerId;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.WriteNetPlayerId(playerId);
		}

		public override void readData(FastBinaryReader reader)
		{
			playerId = reader.ReadNetPlayerId();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("playerId: " + $"{playerId}");
			return stringBuilder.ToString();
		}
	}

	public sealed class DarkWorldCutSceneTimer : Timer
	{
		public NetPlayerId playerId;

		public override byte getTypeId()
		{
			return 18;
		}

		public DarkWorldCutSceneTimer()
		{
		}

		public DarkWorldCutSceneTimer(NetPlayerId playerId)
		{
			this.playerId = playerId;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.WriteNetPlayerId(playerId);
		}

		public override void readData(FastBinaryReader reader)
		{
			playerId = reader.ReadNetPlayerId();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("playerId: " + $"{playerId}");
			return stringBuilder.ToString();
		}
	}

	public sealed class EnablePipeEditingTimer : Timer
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

	public sealed class PipesTargetableTimer : Timer
	{
		public override byte getTypeId()
		{
			return 20;
		}

		public override void writeData(FastBinaryWriter writer)
		{
		}

		public override void readData(FastBinaryReader reader)
		{
		}
	}

	public sealed class WaterParticles1Timer : Timer
	{
		public override byte getTypeId()
		{
			return 21;
		}

		public override void writeData(FastBinaryWriter writer)
		{
		}

		public override void readData(FastBinaryReader reader)
		{
		}
	}

	public sealed class WaterParticles2Timer : Timer
	{
		public override byte getTypeId()
		{
			return 22;
		}

		public override void writeData(FastBinaryWriter writer)
		{
		}

		public override void readData(FastBinaryReader reader)
		{
		}
	}

	public sealed class WaterParticles3Timer : Timer
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

	public sealed class MusicBoxTurnTimer : Timer
	{
		public override byte getTypeId()
		{
			return 24;
		}

		public override void writeData(FastBinaryWriter writer)
		{
		}

		public override void readData(FastBinaryReader reader)
		{
		}
	}

	public sealed class WaterRipplesTimer : Timer
	{
		public Transform animTransform;

		public override byte getTypeId()
		{
			return 25;
		}

		public WaterRipplesTimer()
		{
		}

		public WaterRipplesTimer(Transform animTransform)
		{
			this.animTransform = animTransform;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.WriteComponent(animTransform);
		}

		public override void readData(FastBinaryReader reader)
		{
			animTransform = reader.ReadComponent<Transform>();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("animTransform: " + $"{animTransform}");
			return stringBuilder.ToString();
		}
	}

	public sealed class PendantFloatTimer : Timer
	{
		public Transform animTransform;

		public override byte getTypeId()
		{
			return 26;
		}

		public PendantFloatTimer()
		{
		}

		public PendantFloatTimer(Transform animTransform)
		{
			this.animTransform = animTransform;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.WriteComponent(animTransform);
		}

		public override void readData(FastBinaryReader reader)
		{
			animTransform = reader.ReadComponent<Transform>();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("animTransform: " + $"{animTransform}");
			return stringBuilder.ToString();
		}
	}

	public sealed class Locations1Timer : Timer
	{
		public override byte getTypeId()
		{
			return 27;
		}

		public override void writeData(FastBinaryWriter writer)
		{
		}

		public override void readData(FastBinaryReader reader)
		{
		}
	}

	public sealed class Locations2Timer : Timer
	{
		public override byte getTypeId()
		{
			return 28;
		}

		public override void writeData(FastBinaryWriter writer)
		{
		}

		public override void readData(FastBinaryReader reader)
		{
		}
	}

	public sealed class SolvePendantTimer : Timer
	{
		public override byte getTypeId()
		{
			return 29;
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
			return 30;
		}

		public override void writeData(FastBinaryWriter writer)
		{
		}

		public override void readData(FastBinaryReader reader)
		{
		}
	}

	public sealed class SolvePainting1Timer : Timer
	{
		public override byte getTypeId()
		{
			return 31;
		}

		public override void writeData(FastBinaryWriter writer)
		{
		}

		public override void readData(FastBinaryReader reader)
		{
		}
	}

	public sealed class SolvePainting2Timer : Timer
	{
		public override byte getTypeId()
		{
			return 32;
		}

		public override void writeData(FastBinaryWriter writer)
		{
		}

		public override void readData(FastBinaryReader reader)
		{
		}
	}

	public sealed class ManholesTimer : Timer
	{
		public int opening;

		public override byte getTypeId()
		{
			return 33;
		}

		public ManholesTimer()
		{
		}

		public ManholesTimer(int opening)
		{
			this.opening = opening;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in opening, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			opening = reader.ReadInt32();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("opening: " + $"{opening}");
			return stringBuilder.ToString();
		}
	}

	public sealed class PipeRisingSoundTimer : Timer
	{
		public override byte getTypeId()
		{
			return 34;
		}

		public override void writeData(FastBinaryWriter writer)
		{
		}

		public override void readData(FastBinaryReader reader)
		{
		}
	}

	public sealed class BookcaseLockDelayTimer : Timer
	{
		public override byte getTypeId()
		{
			return 35;
		}

		public override void writeData(FastBinaryWriter writer)
		{
		}

		public override void readData(FastBinaryReader reader)
		{
		}
	}

	public sealed class SolveJewelryBoxTimer : Timer
	{
		public override byte getTypeId()
		{
			return 36;
		}

		public override void writeData(FastBinaryWriter writer)
		{
		}

		public override void readData(FastBinaryReader reader)
		{
		}
	}

	public sealed class BookCaseSound1Timer : Timer
	{
		public override byte getTypeId()
		{
			return 37;
		}

		public override void writeData(FastBinaryWriter writer)
		{
		}

		public override void readData(FastBinaryReader reader)
		{
		}
	}

	public sealed class BookCaseSound2Timer : Timer
	{
		public override byte getTypeId()
		{
			return 38;
		}

		public override void writeData(FastBinaryWriter writer)
		{
		}

		public override void readData(FastBinaryReader reader)
		{
		}
	}

	public sealed class BalconyCutSceneTimer : Timer
	{
		public override byte getTypeId()
		{
			return 39;
		}

		public override void writeData(FastBinaryWriter writer)
		{
		}

		public override void readData(FastBinaryReader reader)
		{
		}
	}

	public sealed class SolveDarkWorldCutSceneTimer : Timer
	{
		public override byte getTypeId()
		{
			return 40;
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
			return 41;
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

	public sealed class BookFireTimer : Timer
	{
		public int index;

		public Renderer[] rendererBook;

		public override byte getTypeId()
		{
			return 42;
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

	public enum FadeActionType
	{
		PaintingTimer1 = 0,
		PaintingTimer2 = 1,
		RipPaintings = 2
	}

	private enum Puzzle
	{
		[PuzzleInfo("%PuzzleD3_1%", false)]
		BedChest = 0,
		[PuzzleInfo("%PuzzleD3_2%", false)]
		MazeCage = 1,
		[PuzzleInfo("%PuzzleD3_3%", false)]
		Bookshelf = 2,
		[PuzzleInfo("%PuzzleD3_4%", false)]
		PictureCage = 3,
		[PuzzleInfo("%PuzzleD3_5%", false)]
		Mirror = 4,
		[PuzzleInfo("%PuzzleD3_6%", false)]
		PaintingCode = 5,
		[PuzzleInfo("%PuzzleD3_7%", false)]
		Map = 6,
		[PuzzleInfo("%PuzzleD3_8%", false)]
		BalconyStatues = 7,
		[PuzzleInfo("%PuzzleD3_9%", false)]
		BathroomBoxes = 8,
		[PuzzleInfo("%PuzzleD3_10%", false)]
		Pendant = 9,
		[PuzzleInfo("%PuzzleD3_11%", false)]
		BathroomHandles = 10,
		[PuzzleInfo("%PuzzleD3_12%", false)]
		PipeBlocks = 11,
		[PuzzleInfo("%PuzzleD3_13%", false)]
		Bathtub = 12,
		[PuzzleInfo("%PuzzleD3_14%", true)]
		DarkWorld = 13,
		[PuzzleInfo("%PuzzleD3_15%", false)]
		Exit = 14
	}

	private enum BedChestHint
	{
		LookAtLock = 0,
		LookAtBedCeiling = 1,
		MatchSymbols = 2,
		Symbol1 = 3
	}

	private enum MazeCageHint
	{
		LookAtMaze = 0,
		RotateMaze = 1,
		Solve = 2
	}

	private enum BookshelfHint
	{
		GetBook = 0,
		LookAtSymbols = 1,
		LookAtBook = 2,
		Solve = 3
	}

	private enum PictureCageHint
	{
		GetPiece1 = 0,
		GetPiece2 = 1,
		GetPiece3 = 2,
		GetPiece4 = 3,
		GetPiece56 = 4,
		PlacePieces = 5,
		Solution1 = 6
	}

	private enum MirrorHint
	{
		LookAtMirror = 0,
		LookAtPaintings = 1,
		LookAtPaintingTroughMirror = 2,
		MatchPaintings = 3,
		Solution1 = 4
	}

	private enum PaintingCodeHint
	{
		LookAtLock = 0,
		LookAtSymbols = 1,
		Solution1 = 2
	}

	private enum MapHint
	{
		GetKey = 0,
		OpenDoor = 1,
		GetJournal = 2,
		LookAtMap = 3,
		LookAtNames = 4,
		LookTroughTelescope = 5,
		Solution1 = 6
	}

	private enum BalconyStatuesHint
	{
		GetBat = 0,
		GetRaven = 1,
		GetEagle = 2,
		GetHeron = 3,
		GetOwl = 4,
		LookAtFence = 5,
		LookAtPattern = 6,
		Solution1 = 7
	}

	private enum BathroomBoxesHint
	{
		GetKey = 0,
		OpenDoor = 1,
		GetMusicBox = 2,
		GetHandle = 3,
		PlaceHandle = 4,
		OpenMusicBox = 5,
		GetBoxKey = 6,
		GetBox = 7,
		PlaceKey = 8
	}

	private enum PendantHint
	{
		GetPendantAndHint = 0,
		TurnPendant = 1,
		TurnDial = 2,
		MatchHintToDial = 3,
		SolutionStart = 4
	}

	private enum BathroomHandlesHint
	{
		LookAtHandles = 0,
		LookAtHands = 1,
		MatchHandToHandle = 2,
		Solution1 = 3
	}

	private enum PipeBlocksHint
	{
		GetBlocks = 0,
		LookAtBlocks = 1,
		Solution1 = 2
	}

	private enum BathtubHint
	{
		RunWater = 0,
		PlacePendant = 1,
		EnterDarkWorld = 2
	}

	private enum DarkWorldHint
	{
		LookAtCandleSymbol = 0,
		MPLookAtCandleSymbol = 1,
		LookAtPlayerSymbol = 2,
		MatchCandle1 = 3,
		MatchCandle2 = 4,
		ReleaseKey = 5
	}

	private enum ExitHint
	{
		GetKey = 0,
		PlaceKey = 1
	}

	private enum LevelPredicate
	{
		DialBirdcage = 0,
		MusicBox = 1,
		SolvePendant = 2,
		SolveDarkWorld = 3,
		FireplaceLocations = 4,
		BedChest = 5,
		Paintings = 6,
		FourthHanlde = 7,
		BalconySlots = 8,
		ReleasePipes = 9,
		SolvePipes = 10,
		ImagesBirdcage = 11,
		SolvePendantText = 12,
		ResetPendantText = 13
	}

	private enum RPC
	{
		ResetBed = 0
	}

	[DontSave]
	private Exposure exposure;

	[DontSave]
	public Item bedLockGO;

	[DontSave]
	public GameObject bedLockShakeCollider;

	private float musicBoxBaseline;

	private float musicBoxAddition;

	private int musicBoxCurrentLevel;

	private int musicBoxCurrentDial;

	private readonly float[] MusicBoxBreach = new float[3] { 20f, 30f, 40f };

	private bool musicBoxBreached;

	private bool musicBoxReset;

	private float musicBoxOutput;

	private bool musicBoxSolved;

	private float changeAfterBreach;

	private const float MaxChangeAfterBreach = 10f;

	private readonly string pendantChars = "NAMOWDER";

	private readonly string pendantSolution = "DROWNME";

	private bool solvedPendantText;

	private bool pendantFloated;

	private float pendantLightCounter = -1f;

	private bool pendantToBright;

	private bool solvedPaintings;

	private bool fireplaceSolved;

	[DontSave]
	private Dictionary<GameObject, int[][]> statueToPattern;

	private readonly int[] balconySlotIndexes = new int[5] { 2, 6, 12, 18, 22 };

	private int bathroomFourthHandleCurrent;

	private bool bathroomFourthHandleSolved;

	private Item[] pipesVisiblyInSlot;

	private bool pipesLifted;

	private bool pipesSolved;

	private bool bathSolved;

	private bool pendantPlaced;

	[DontSave]
	private EventInstance[] manholeInstances;

	[DontSave]
	private EventInstance[] pipeRisingInstances;

	[DontSave]
	private EventInstance waterInstance;

	[DontSave]
	private Interactive[] allInteractives;

	private bool isInDarkWorld;

	private bool playersShouldSparkle;

	public readonly float DarkWorldTransitionTimeOut = 2.5f;

	private List<NetPlayerId> playersInDarkWorld = new List<NetPlayerId>();

	[Min(0f)]
	public readonly float DarkDiveEffectDelay = 0.9f;

	private Dictionary<Interactive, bool> interactiveToTargetable = new Dictionary<Interactive, bool>();

	private bool solvedDarkWorld;

	private float startingLineWidth;

	private int darkWorldCountMP;

	private bool darkWorldIsMp;

	private bool shouldToggleDarkLines;

	private readonly float toggleInterval = 0.1f;

	private float timer;

	[DontSave]
	public ParticleSystem darkWorldDust;

	[DontSave]
	public GameObject solveDarkWorldCutScene;

	[DontSave]
	private Vector3 exitKeyOriginalPos;

	private float exitKeyFloatOffset;

	private const float ExitKeyFloatMaxOffset = 0.025f;

	[DontSave]
	private EventInstance darkWorldInstance;

	[DontSave]
	private EventInstance keyLightInstance;

	private Vector3 darkWorldPlayerLastPos;

	private bool darkWorldPlayerMoved;

	private bool solvedBc2Up;

	[DontSave]
	private float bc2RayDistance;

	private readonly float bc2SphereRadius = 0.004f;

	private int bc2EdgeIndex;

	[DontSave]
	private EventInstance ballSoundInstance;

	private const float BallStopSoundDelayTimeout = 0.1f;

	private float ballStopSoundDelay;

	private bool ballPlayedHit;

	[DontSave]
	public EventInstance bookCaseInstance;

	[DontSave]
	public Renderer[] darkWorldDisableRenderers;

	[DontSave]
	private ProbeReferenceVolume probeRefVolume;

	private bool isFading;

	[DontSave]
	private float startingExposure;

	public readonly float minExposure = 5f;

	private bool isTransitioningToLighting;

	[DontSave]
	public StudioEventEmitter musicEmitter;

	[DontSave]
	public StudioEventEmitter fireplaceEmitter;

	[DontSave]
	private EventReference soundStep;

	[DontSave]
	public Ref<GameObject, Switch3D> levelExitRef;

	private int currentPlayerCount;

	[DontSave]
	public Collider starKeyCollider;

	private float starKeyLastMissTime;

	private bool starKeyPicked;

	private float achievementTimer = -1f;

	[Header("Generated Variables")]
	[DontSave]
	public Renderer[] telescopeRenderers;

	[DontSave]
	public MaterialState telescopeMS;

	[DontSave]
	public Item telescope;

	[DontSave]
	public TweenState[] pipeSlotParents;

	[DontSave]
	public TweenState chestLockPart;

	[DontSave]
	public RefArray<MaterialState, GameObject> exitKeyMaterialStates;

	[DontSave]
	public Item pendantHint;

	[DontSave]
	public TweenState[] pendantPins;

	[DontSave]
	public GameObject[] manholes;

	[DontSave]
	public Sequence manholeSequence;

	[DontSave]
	public Sequence risingPipeSequence;

	[DontSave]
	public Turnable bc1Turnable;

	[DontSave]
	public MaterialState[] pendantMSs;

	[DontSave]
	public TweenState birdcage1Door;

	[DontSave]
	public TweenState birdcage1DoorGear;

	[DontSave]
	public Item[] birdcageTiles;

	[DontSave]
	public Slot[] birdcageSlots;

	[DontSave]
	public TweenState musicBoxKeyLid;

	[DontSave]
	public Transform bc2SliderTarget;

	[DontSave]
	public Switch3D exitKeyButton;

	[DontSave]
	public Switch3D bathtubValve;

	[DontSave]
	public Ref<Trigger, GameObject> pendantTrigger;

	[DontSave]
	public GameObject pendantSurfaceCollider;

	[DontSave]
	public Ref<TweenState, Transform, GameObject> pendantFloatDownTS;

	[DontSave]
	public Ref<Sequence, Transform, GameObject> pendantSolvedFloatSequence;

	[DontSave]
	public Ref<ParticleSystem, Transform> waterBubbles;

	[DontSave]
	public Ref<ParticleSystem, Transform> waterRipples;

	[DontSave]
	public ParticleSystem[] waterFlowParticles;

	[DontSave]
	public Item keyBalcony;

	[DontSave]
	public Renderer[] normalPaintingsMaterialStates;

	[DontSave]
	public Material blackPaintingMaterial;

	[DontSave]
	public Item[] pipeCubes;

	[DontSave]
	public Ref<Sequence, GameObject> EnterDarkWorldCutscene;

	[DontSave]
	public TweenState bc2Tween;

	[DontSave]
	public GameObject[] darkWorldFire;

	[DontSave]
	public RefArray<GameObject, Transform> darkWorldPlayerVFX;

	[DontSave]
	public Transform bc2TopFinal;

	[DontSave]
	public SlidableGraph bc2TopGraph;

	[DontSave]
	public Dial bc2TopDial;

	[DontSave]
	public GameObject bc2SliderPoint;

	[DontSave]
	public Transform bc2RotatingSlidingPiece;

	[DontSave]
	public TweenState bc2PressurePlateTS;

	[DontSave]
	public Zoomable bc2RotatingZoomable;

	[DontSave]
	public TweenState exitDoor;

	[DontSave]
	public Slot exitSlot;

	[DontSave]
	public Item exitKey;

	[DontSave]
	public GameObject[] darkLines;

	[DontSave]
	public GameObject[] darkLinesPSParents;

	[DontSave]
	public ParticleSystem[] darkLinesPSs;

	[DontSave]
	public RefArray<Trigger, GameObject> darkTriggers;

	[DontSave]
	private LineRenderer[] darkLineRenderers;

	private bool[] correctDarkPlacement;

	[DontSave]
	public GameObject[] darkLinesMP;

	[DontSave]
	public GameObject[] darkLinesPSParentsMP;

	[DontSave]
	public ParticleSystem[] darkLinesPSsMP;

	[DontSave]
	public RefArray<Trigger, GameObject> darkTriggersMP;

	[DontSave]
	private LineRenderer[] darkLineRenderersMP;

	private bool[] correctDarkPlacementMP;

	[DontSave]
	public GameObject darkSphereSound;

	[DontSave]
	public Ref<GameObject, Transform> darkSphere;

	[DontSave]
	public GameObject insideDarkworld;

	[DontSave]
	public Lock bedSideLock;

	[DontSave]
	public Turnable[] bedLockTurnables;

	[DontSave]
	public Zoomable bedLockZoomable;

	[DontSave]
	public TweenState bedLockTS;

	[DontSave]
	public TweenState bedDrawerTs;

	[DontSave]
	public Switch3D bedDrawer;

	[DontSave]
	public Jigsaw fireplaceJigsaw;

	[DontSave]
	public GameObject combatJournal;

	[DontSave]
	public Ref<TweenState, Zoomable, GameObject> rotatePainting;

	[DontSave]
	public GameObject[] fireplacePlateSolutions;

	[DontSave]
	public Renderer[] fireplacePlates;

	[DontSave]
	public Item heronStatue;

	[DontSave]
	public Volume volumeNormal;

	[DontSave]
	public GameObject normalLightingParent;

	[DontSave]
	public GameObject darkLightingParent;

	[DontSave]
	public Ref<AnimationSampler, GameObject> balconyCutScene;

	[DontSave]
	public TweenState balconyTrapDoors;

	[DontSave]
	public Switch3D balconyDoors;

	[DontSave]
	public Slot balconySlot;

	[DontSave]
	public Item[] sketchPapers;

	[DontSave]
	public Switch3D jewelryBoxLid;

	[DontSave]
	public Slot jewelryBoxSlot;

	[DontSave]
	public Item jewelryBox;

	[DontSave]
	public Switch3D bathFluidSw;

	[DontSave]
	public ParticleSystem[] bathDiveEffects;

	[DontSave]
	public MaterialState bathFluidMs;

	[DontSave]
	public TweenState bathFluidTS;

	[DontSave]
	public GameObject bathRunningWater;

	[DontSave]
	public Slot doorSlot;

	[DontSave]
	public Switch3D bathroomDoor;

	[DontSave]
	public TweenState chandelier;

	[DontSave]
	public Slot[] pipeSlots;

	[DontSave]
	public Switch3D[] bathroomFourthHandleSolution;

	[DontSave]
	public Switch3D[] bathroomHandles;

	[DontSave]
	public Item[] balconyStatues;

	[DontSave]
	public Slot[] balconySlots;

	[DontSave]
	public Sequence[] balconySlotSequences;

	[DontSave]
	public TweenState[] balconyBottomRowTweens;

	[DontSave]
	public TweenState[] balconyTopRowTweens;

	[DontSave]
	public Item musicBoxKey;

	[DontSave]
	public Slot musicBoxSlot;

	[DontSave]
	public GameObject musicBoxSlotHandleWorld;

	[DontSave]
	public GameObject musicBoxSlotHandleInventory;

	[DontSave]
	public Item musicBoxSlotHandle;

	[DontSave]
	public Dial[] musicBoxDials;

	[DontSave]
	public TweenState musicBoxLid;

	[DontSave]
	public Switch3D chestLid;

	[DontSave]
	public Zoomable chestLockZoomable;

	[DontSave]
	public GameObject[] bedPoses;

	[DontSave]
	public Switch3D[] chestSolutionButtons;

	[DontSave]
	public Item chestLock;

	[DontSave]
	public TweenState chestLockerTween;

	[DontSave]
	public Switch3D[] chestLockButtons;

	[DontSave]
	public TweenState bookcaseCabinetDoors;

	[DontSave]
	public GameObject bookcaseBook;

	[DontSave]
	public Turnable[] bookcaseTurnables;

	[DontSave]
	public Lock bookcaseLock;

	[DontSave]
	public GameObject[] nonRippedPaintings;

	[DontSave]
	public GameObject[] rippedPaintings;

	[DontSave]
	public Switch3D[] paintingsToBeFlipped;

	[DontSave]
	public Switch3D[] paintingsSliders;

	[DontSave]
	public GameObject mirror;

	[DontSave]
	public Ref<Dial, Transform, GameObject> pendantDial;

	[DontSave]
	public Ref<Item, Transform, GameObject> pendant;

	[DontSave]
	public GameObject pendantBase;

	[DontSave]
	public Ref<Sequence, GameObject> pendantSequence;

	[DontSave]
	public Text locketText;

	[DontSave]
	public GameObject locketUI;

	[DontSave]
	public ParticleSystem darkSphereBurstPS;

	[DontSave]
	public ParticleSystem darkKeyLensflare;

	[DontSave]
	public MeshRenderer paintingsMirrorMR;

	[DontSave]
	public Material paintingsMirrorBrokenMat;

	[DontSave]
	public GameObject[] disableInDarkWorld;

	[DontSave]
	public Transform[] footstepsInDarkWorld;

	[DontSave]
	public GameObject vasePetalsVFX;

	[DontSave]
	public GameObject vasePetalsPickupGO;

	[DontSave]
	public Trigger fireLogsTrigger;

	[DontSave]
	public RefArray<GameObject, MaterialState, Item, Transform> fireLog;

	[DontSave]
	public RefArray<GameObject, Item, Transform> fireBook;

	[DontSave]
	public ParticleSystem fireLogVFX;

	[DontSave]
	public Transform fireLogVFXTransform;

	public override void onInitHints()
	{
		game.setPuzzleType(Puzzle.DarkWorld, Game.Puzzle.Type.CoopAsk);
		game.setPuzzleConditions(Puzzle.PictureCage, Puzzle.BedChest, Puzzle.Bookshelf);
		game.setPuzzleConditions(Puzzle.PaintingCode, Puzzle.Mirror);
		game.setPuzzleConditions(Puzzle.Map, Puzzle.PaintingCode);
		game.setPuzzleConditions(Puzzle.BalconyStatues, Puzzle.MazeCage, Puzzle.PictureCage, Puzzle.Map);
		game.setPuzzleConditions(Puzzle.BathroomBoxes, Puzzle.BalconyStatues);
		game.setPuzzleConditions(Puzzle.Pendant, Puzzle.BathroomBoxes);
		game.setPuzzleConditions(Puzzle.BathroomHandles, Puzzle.BalconyStatues);
		game.setPuzzleConditions(Puzzle.PipeBlocks, Puzzle.BathroomHandles);
		game.setPuzzleConditions(Puzzle.Bathtub, Puzzle.PipeBlocks, Puzzle.Pendant);
		game.setPuzzleConditions(Puzzle.DarkWorld, Puzzle.Bathtub);
		game.setPuzzleConditions(Puzzle.Exit, Puzzle.DarkWorld);
		game.setRelevantObjectsForPuzzle(Puzzle.BedChest, chestLockZoomable.gameObject, bedPoses[0], bedPoses[1]);
		game.setRelevantObjectsForPuzzle(Puzzle.MazeCage, bc2RotatingZoomable.gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.Bookshelf, bookcaseBook);
		game.setRelevantObjectsForPuzzle(Puzzle.Bookshelf, Array.ConvertAll(bookcaseTurnables, (Turnable t) => t.gameObject));
		game.setRelevantObjectsForPuzzle(Puzzle.PictureCage, bc1Turnable.gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.PictureCage, Array.ConvertAll(birdcageTiles, (Item t) => t.gameObject));
		game.setRelevantObjectsForPuzzle(Puzzle.Mirror, mirror);
		game.setRelevantObjectsForPuzzle(Puzzle.Mirror, Array.ConvertAll(paintingsSliders, (Switch3D s) => s.gameObject));
		game.setRelevantObjectsForPuzzle(Puzzle.PaintingCode, bedLockZoomable.gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.Map, keyBalcony.gameObject, balconySlot.gameObject, rotatePainting.Get<GameObject>(0u), combatJournal, telescope.gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.BalconyStatues, Array.ConvertAll(balconyStatues, (Item s) => s.gameObject));
		game.setRelevantObjectsForPuzzle(Puzzle.BalconyStatues, Array.ConvertAll(balconySlots, (Slot s) => s.gameObject));
		game.setRelevantObjectsForPuzzle(Puzzle.BathroomBoxes, doorSlot.gameObject, doorSlot.acceptItems[0].gameObject, musicBoxLid.gameObject, musicBoxSlotHandle.gameObject, musicBoxKey.gameObject, jewelryBox.gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.Pendant, pendant.Get<GameObject>(0u), pendantHint.gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.BathroomHandles, Array.ConvertAll(bathroomHandles, (Switch3D s) => s.gameObject));
		game.setRelevantObjectsForPuzzle(Puzzle.PipeBlocks, Array.ConvertAll(pipeCubes, (Item s) => s.gameObject));
		game.setRelevantObjectsForPuzzle(Puzzle.Bathtub, bathtubValve.gameObject, pendant.Get<GameObject>(0u));
		game.setRelevantObjectsForPuzzle(Puzzle.DarkWorld, bathFluidSw.gameObject, darkWorldDisableRenderers[0].gameObject, darkWorldDisableRenderers[1].gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.Exit, exitKey.gameObject);
		game.setHintCondition(Puzzle.BedChest, BedChestHint.LookAtLock, () => game.wasLookedAtCurrentPuzzle(chestLockZoomable.gameObject));
		game.setHintCondition(Puzzle.MazeCage, MazeCageHint.LookAtMaze, () => game.wasLookedAtCurrentPuzzle(bc2RotatingZoomable.gameObject));
		game.setHintCondition(Puzzle.MazeCage, MazeCageHint.RotateMaze, () => bc2TopDial.value > 0);
		game.setHintCondition(Puzzle.Bookshelf, BookshelfHint.GetBook, () => game.wasAddedToInventoryDuringCurrentPuzzle(bookcaseBook));
		game.setHintCondition(Puzzle.PictureCage, PictureCageHint.GetPiece1, () => game.wasAddedToInventoryDuringCurrentPuzzle(birdcageTiles[5].gameObject));
		game.setHintCondition(Puzzle.PictureCage, PictureCageHint.GetPiece2, () => game.wasAddedToInventoryDuringCurrentPuzzle(birdcageTiles[3].gameObject));
		game.setHintCondition(Puzzle.PictureCage, PictureCageHint.GetPiece3, () => game.wasAddedToInventoryDuringCurrentPuzzle(birdcageTiles[2].gameObject));
		game.setHintCondition(Puzzle.PictureCage, PictureCageHint.GetPiece4, () => game.wasAddedToInventoryDuringCurrentPuzzle(birdcageTiles[0].gameObject));
		game.setHintCondition(Puzzle.PictureCage, PictureCageHint.GetPiece56, () => game.wasAddedToInventoryDuringCurrentPuzzle(birdcageTiles[1].gameObject) && game.wasAddedToInventoryDuringCurrentPuzzle(birdcageTiles[4].gameObject));
		game.setHintCondition(Puzzle.PictureCage, PictureCageHint.PlacePieces, delegate
		{
			bool flag = false;
			Slot[] array = birdcageSlots;
			foreach (Slot slot in array)
			{
				flag |= slot.insertedItem != null;
			}
			return flag;
		});
		game.setHintCondition(Puzzle.PaintingCode, PaintingCodeHint.LookAtLock, () => game.wasLookedAtCurrentPuzzle(bedLockZoomable.gameObject));
		game.setHintCondition(Puzzle.Map, MapHint.GetKey, () => balconySlot.isUnlocked || game.wasAddedToInventoryDuringCurrentPuzzle(keyBalcony.gameObject));
		game.setHintCondition(Puzzle.Map, MapHint.OpenDoor, () => balconySlot.isUnlocked);
		game.setHintCondition(Puzzle.Map, MapHint.GetJournal, () => game.wasAddedToInventoryDuringCurrentPuzzle(combatJournal));
		game.setHintCondition(Puzzle.Map, MapHint.LookAtMap, () => game.wasLookedAtCurrentPuzzle(rotatePainting.Get<GameObject>(0u)));
		game.setHintCondition(Puzzle.BalconyStatues, BalconyStatuesHint.GetBat, () => game.wasAddedToInventoryDuringCurrentPuzzle(balconyStatues[2].gameObject));
		game.setHintCondition(Puzzle.BalconyStatues, BalconyStatuesHint.GetRaven, () => game.wasAddedToInventoryDuringCurrentPuzzle(balconyStatues[4].gameObject));
		game.setHintCondition(Puzzle.BalconyStatues, BalconyStatuesHint.GetEagle, () => game.wasAddedToInventoryDuringCurrentPuzzle(balconyStatues[1].gameObject));
		game.setHintCondition(Puzzle.BalconyStatues, BalconyStatuesHint.GetHeron, () => game.wasAddedToInventoryDuringCurrentPuzzle(balconyStatues[3].gameObject));
		game.setHintCondition(Puzzle.BalconyStatues, BalconyStatuesHint.GetOwl, () => game.wasAddedToInventoryDuringCurrentPuzzle(balconyStatues[0].gameObject));
		game.setHintCondition(Puzzle.BathroomBoxes, BathroomBoxesHint.GetKey, () => musicBoxSolved || doorSlot.isUnlocked || game.wasAddedToInventoryDuringCurrentPuzzle(doorSlot.acceptItems[0].gameObject));
		game.setHintCondition(Puzzle.BathroomBoxes, BathroomBoxesHint.OpenDoor, () => musicBoxSolved || doorSlot.isUnlocked);
		game.setHintCondition(Puzzle.BathroomBoxes, BathroomBoxesHint.GetMusicBox, () => musicBoxSolved || game.wasAddedToInventoryDuringCurrentPuzzle(musicBoxLid.gameObject));
		game.setHintCondition(Puzzle.BathroomBoxes, BathroomBoxesHint.GetHandle, () => musicBoxSolved || musicBoxSlot.isUnlocked || game.wasAddedToInventoryDuringCurrentPuzzle(musicBoxSlotHandle.gameObject));
		game.setHintCondition(Puzzle.BathroomBoxes, BathroomBoxesHint.PlaceHandle, () => musicBoxSolved || musicBoxSlot.isUnlocked);
		game.setHintCondition(Puzzle.BathroomBoxes, BathroomBoxesHint.OpenMusicBox, () => musicBoxSolved);
		game.setHintCondition(Puzzle.BathroomBoxes, BathroomBoxesHint.GetBox, () => game.wasAddedToInventoryDuringCurrentPuzzle(musicBoxKey.gameObject));
		game.setHintCondition(Puzzle.Pendant, PendantHint.GetPendantAndHint, () => game.wasAddedToInventoryDuringCurrentPuzzle(pendant.Get<GameObject>(0u)) && game.wasAddedToInventoryDuringCurrentPuzzle(pendantHint.gameObject));
		game.setHintCondition(Puzzle.Pendant, PendantHint.TurnPendant, () => pendantDial.Get<Dial>(0).value > 0);
		game.setHintCondition(Puzzle.Pendant, PendantHint.TurnDial, () => pendantDial.Get<Dial>(0).value > 0);
		game.setHintCondition(Puzzle.PipeBlocks, PipeBlocksHint.GetBlocks, delegate
		{
			bool flag = true;
			Item[] array = pipeCubes;
			foreach (Item item in array)
			{
				flag &= game.wasAddedToInventoryDuringCurrentPuzzle(item.gameObject);
			}
			return flag;
		});
		game.setHintCondition(Puzzle.PipeBlocks, PipeBlocksHint.LookAtBlocks, delegate
		{
			bool flag = false;
			Item[] array = pipeCubes;
			foreach (Item item in array)
			{
				flag |= game.wasLookedAtCurrentPuzzle(item.gameObject);
			}
			return flag;
		});
		game.setHintCondition(Puzzle.Bathtub, BathtubHint.RunWater, () => bathSolved);
		game.setHintCondition(Puzzle.Bathtub, BathtubHint.PlacePendant, () => pendantPlaced);
		game.setHintCondition(Puzzle.DarkWorld, DarkWorldHint.LookAtCandleSymbol, () => darkWorldIsMp);
		game.setHintCondition(Puzzle.DarkWorld, DarkWorldHint.MPLookAtCandleSymbol, () => !darkWorldIsMp || game.getPlayerCount() > 2);
		game.setHintCondition(Puzzle.DarkWorld, DarkWorldHint.MatchCandle1, () => darkWorldIsMp || correctDarkPlacement[2] || correctDarkPlacementMP[2]);
		game.setHintCondition(Puzzle.DarkWorld, DarkWorldHint.MatchCandle2, () => darkWorldIsMp || correctDarkPlacement[1] || correctDarkPlacementMP[1]);
		game.setHintCondition(Puzzle.Exit, ExitHint.GetKey, () => game.wasAddedToInventoryDuringCurrentPuzzle(exitKey.gameObject));
	}

	public override void onInitPredicates()
	{
		game.registerPredicate(LevelPredicate.DialBirdcage, () => (bc2SliderTarget.position - bc2RotatingSlidingPiece.position).sqrMagnitude < 6.25E-06f && !game.isAnyPlayerInteracting(bc2TopDial.gameObject));
		game.registerPredicate(LevelPredicate.MusicBox, () => musicBoxCurrentLevel >= MusicBoxBreach.Length && !musicBoxSolved);
		game.registerPredicate(LevelPredicate.SolvePendant, () => checkPendantSolution());
		game.registerPredicate(LevelPredicate.SolveDarkWorld, 2f, false, () => checkDarkWorldSolved());
		game.registerPredicate(LevelPredicate.FireplaceLocations, () => checkFireplaceLocations());
		game.registerPredicate(LevelPredicate.BedChest, () => checkBedSolution());
		game.registerPredicate(LevelPredicate.Paintings, () => checkPaintingsSolution());
		game.registerPredicate(LevelPredicate.FourthHanlde, () => checkFourthHandle());
		game.registerPredicate(LevelPredicate.BalconySlots, () => checkBalconySlots());
		game.registerPredicate(LevelPredicate.ReleasePipes, 0.1f, true, () => checkPipesRelease());
		game.registerPredicate(LevelPredicate.SolvePipes, () => checkPipesSolved());
		game.registerPredicate(LevelPredicate.ImagesBirdcage, () => checkBirtCage1Solved());
		game.registerPredicate(LevelPredicate.SolvePendantText, () => locketText.text == pendantSolution);
		game.registerPredicate(LevelPredicate.ResetPendantText, 0.1f, true, () => checkResetPendant());
	}

	public override void onLevelPredicateDone(int type, int doneCounter)
	{
		if (type == 0)
		{
			solveBc2Top();
		}
		if (type == 1)
		{
			solveMusicBox();
		}
		if (type == 2)
		{
			solvePendant();
		}
		if (type == 3)
		{
			solveDarkWorld();
		}
		if (type == 4)
		{
			solveFireplaceLocations();
		}
		if (type == 5)
		{
			solveBedChest();
		}
		if (type == 6)
		{
			solvePaintings();
		}
		if (type == 7)
		{
			solveFourthHandle();
		}
		if (type == 8)
		{
			solveBalcony();
		}
		if (type == 9)
		{
			for (int i = 0; i < pipeSlots.Length; i++)
			{
				releasePipeFromSlot(i);
			}
		}
		if (type == 10)
		{
			solvePipes();
		}
		if (type == 11)
		{
			solveBirdcage1();
		}
		if (type == 12)
		{
			pendantDial.Get<Dial>(0).targetable = false;
			game.startTimer(new SolvePendantTextTimer(), 0.5f);
		}
		if (type == 13)
		{
			pendantDial.Get<Dial>(0).targetable = false;
			game.startTimer(new DisablePendantTextTimer(), 1f, 0.4f);
		}
	}

	public override void onRPCCalled(int type)
	{
		if (type == 0)
		{
			resetBedButtons();
		}
	}

	public override void onInit()
	{
		Interactive[] interactives = musicBoxDials;
		Interactive.linkInteractives(interactives);
		Interactive.linkInteractives(new List<Interactive>(birdcageSlots) { bc1Turnable }.ToArray());
		volumeNormal.GetComponent<Volume>().profile.TryGet<Exposure>(out exposure);
		startingExposure = exposure.fixedExposure.value;
		musicBoxDials[0].gameObject.SetActive(value: false);
		musicBoxKey.targetable = false;
		musicBoxSlotHandleWorld.SetActive(value: true);
		musicBoxSlotHandleInventory.SetActive(value: false);
		GameObject[] array = rippedPaintings;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(value: false);
		}
		array = nonRippedPaintings;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(value: true);
		}
		heronStatue.targetable = false;
		statueToPattern = new Dictionary<GameObject, int[][]>
		{
			{
				balconyStatues[0].gameObject,
				new int[2][]
				{
					new int[5] { -2, -1, 0, 1, 2 },
					new int[3] { -2, -1, 0 }
				}
			},
			{
				balconyStatues[1].gameObject,
				new int[2][]
				{
					new int[4] { -1, 0, 1, 3 },
					new int[6] { -3, -2, -1, 0, 1, 2 }
				}
			},
			{
				balconyStatues[2].gameObject,
				new int[2][]
				{
					new int[6] { -4, -2, -1, 0, 1, 3 },
					new int[6] { -3, -2, -1, 0, 1, 2 }
				}
			},
			{
				balconyStatues[3].gameObject,
				new int[2][]
				{
					new int[5] { -4, -2, -1, 0, 1 },
					new int[3] { -3, -2, -1 }
				}
			},
			{
				balconyStatues[4].gameObject,
				new int[2][]
				{
					new int[4] { -2, -1, 0, 1 },
					new int[6] { -4, -3, -2, -1, 0, 1 }
				}
			}
		};
		pendantTrigger.Get<GameObject>(0f).SetActive(value: false);
		bathRunningWater.SetActive(value: false);
		bathFluidSw.targetable = false;
		darkLineRenderers = new LineRenderer[darkLines.Length];
		for (int j = 0; j < darkLines.Length; j++)
		{
			darkLineRenderers[j] = darkLines[j].GetComponent<LineRenderer>();
		}
		darkLineRenderersMP = new LineRenderer[darkLinesMP.Length];
		for (int k = 0; k < darkLinesMP.Length; k++)
		{
			darkLineRenderersMP[k] = darkLinesMP[k].GetComponent<LineRenderer>();
		}
		startingLineWidth = darkLineRenderers[0].startWidth;
		pipesVisiblyInSlot = new Item[pipeSlots.Length];
		for (int l = 0; l < pipeSlots.Length; l++)
		{
			game.setParent(pipeCubes[l].transform, pipeSlotParents[l].transform);
			pipeSlots[l].targetable = false;
			pipeCubes[l].targetable = false;
		}
		bathtubValve.targetable = false;
		pendantFloatDownTS.Get<GameObject>(0u).SetActive(value: false);
		pendantSolvedFloatSequence.Get<GameObject>(0u).SetActive(value: false);
		pendantSurfaceCollider.SetActive(value: false);
		manholeInstances = new EventInstance[manholes.Length];
		for (int m = 0; m < manholes.Length; m++)
		{
			manholeInstances[m] = PineFmod.createInstance("event:/Sound Effects/04 Items/Metal/Metal Concrete/Metal_Drag_On_Concrete");
			PineFmod.set3DAttributes(manholeInstances[m], PineFmod.to3DAttributes(manholes[m].transform));
		}
		pipeRisingInstances = new EventInstance[2];
		pipeRisingInstances[0] = PineFmod.createInstance("event:/Sound Effects/04 Items/Concrete & Rock/Drag Brick");
		PineFmod.set3DAttributes(pipeRisingInstances[0], PineFmod.to3DAttributes(risingPipeSequence.transform));
		pipeRisingInstances[1] = PineFmod.createInstance("event:/Sound Effects/04 Items/Metal/Metal_Creak_01");
		PineFmod.set3DAttributes(pipeRisingInstances[1], PineFmod.to3DAttributes(risingPipeSequence.transform));
		waterInstance = PineFmod.createInstance("event:/Sound Effects/03 Interactable/Levers/Bathtub/Bathtub_Loop");
		PineFmod.set3DAttributes(waterInstance, PineFmod.to3DAttributes(bathFluidSw.transform));
		if (game.netPlayers.Count == 1)
		{
			darkWorldCountMP = 3;
		}
		else if (game.netPlayers.Count == 2)
		{
			darkWorldCountMP = 4;
		}
		else if (game.netPlayers.Count >= 3)
		{
			darkWorldCountMP = 5;
		}
		insideDarkworld.SetActive(value: false);
		darkSphereSound.SetActive(value: false);
		foreach (Ref<Trigger, GameObject> darkTrigger in darkTriggers)
		{
			darkTrigger.Get<GameObject>(0f).SetActive(value: false);
		}
		correctDarkPlacement = new bool[darkTriggers.Length];
		foreach (Ref<Trigger, GameObject> item in darkTriggersMP)
		{
			item.Get<GameObject>(0f).SetActive(value: false);
		}
		correctDarkPlacementMP = new bool[darkTriggersMP.Length];
		if (game.getPlayerCount() == 1)
		{
			initDarkWorldSp();
		}
		else
		{
			initDarkWorldMp();
		}
		foreach (Ref<MaterialState, GameObject> exitKeyMaterialState in exitKeyMaterialStates)
		{
			exitKeyMaterialState.Get<MaterialState>(0).setState("Glow");
		}
		darkWorldInstance = PineFmod.createInstance("event:/Sound Effects/05 Misc/Magic/Magic_Walk_Loop");
		soundStep = game.soundStepDefault;
		keyLightInstance = PineFmod.createInstance("event:/Sound Effects/05 Misc/Magic/Bathtub_Light_Magic_Loop");
		PineFmod.set3DAttributes(keyLightInstance, PineFmod.to3DAttributes(darkSphere.Get<Transform>(0f)));
		allInteractives = game.levelContainer.GetComponentsInChildren<Interactive>(includeInactive: true);
		balconyStatues[1].targetable = false;
		exitKeyOriginalPos = exitKey.transform.position;
		bc2EdgeIndex = 0;
		ballSoundInstance = PineFmod.createInstance("event:/Sound Effects/04 Items/Metal/Small_Metal_Item/Small_Metal_Item_Roll");
		PineFmod.set3DAttributes(ballSoundInstance, PineFmod.to3DAttributes(bc2TopGraph.transform));
		bookCaseInstance = PineFmod.createInstance("event:/Sound Effects/03 Interactable/Drawers/Wood_Drawer_01_Loop");
		PineFmod.set3DAttributes(bookCaseInstance, PineFmod.to3DAttributes(bookcaseCabinetDoors.transform));
		currentPlayerCount = game.getPlayerCount();
	}

	public override void onConvertToSinglePlayer()
	{
		foreach (Ref<GameObject, Transform> item in darkWorldPlayerVFX)
		{
			item.Get<GameObject>(0).SetActive(value: false);
		}
		for (int num = playersInDarkWorld.Count - 1; num >= 0; num--)
		{
			exitDarkWorld(playersInDarkWorld[num]);
		}
		if (pendantPlaced)
		{
			bathFluidSw.targetable = true;
		}
		playersInDarkWorld.Clear();
		initDarkWorldSp();
		if (solvedDarkWorld)
		{
			return;
		}
		if (!darkWorldIsMp)
		{
			for (int i = 0; i < darkTriggers.Length; i++)
			{
				darkTriggers[i].Get<GameObject>(0f).SetActive(value: true);
			}
		}
		else
		{
			for (int j = 0; j < darkTriggersMP.Length; j++)
			{
				darkTriggersMP[j].Get<GameObject>(0f).SetActive(value: false);
			}
		}
	}

	private void initDarkWorldSp()
	{
		darkWorldIsMp = false;
		GameObject[] array = darkLinesMP;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(value: false);
		}
		array = darkLines;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(value: true);
		}
		for (int j = 0; j < darkLinesPSParentsMP.Length; j++)
		{
			darkLinesPSParentsMP[j].SetActive(value: false);
		}
		for (int k = 0; k < darkLinesPSParents.Length; k++)
		{
			darkLinesPSParents[k].SetActive(value: true);
		}
	}

	private void initDarkWorldMp()
	{
		darkWorldIsMp = true;
		GameObject[] array = darkLines;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(value: false);
		}
		for (int j = 0; j < darkLinesMP.Length; j++)
		{
			darkLinesMP[j].SetActive(j < darkWorldCountMP);
		}
		for (int k = 0; k < darkLinesPSParents.Length; k++)
		{
			darkLinesPSParents[k].SetActive(value: false);
		}
		for (int l = 0; l < darkLinesPSParentsMP.Length; l++)
		{
			darkLinesPSParentsMP[l].SetActive(l < darkWorldCountMP);
		}
	}

	[DebugButton(null, Tint.Default, PostClickAction.HideButton, 0, new object[] { })]
	private void debugSetupDarkWorld()
	{
		openBathroomDoor();
		solvePipes();
		solvePendant();
	}

	public override void onUpdate()
	{
		if (achievementTimer > 0f && Time.time - achievementTimer > 10f)
		{
			achievementTimer = -1f;
			game.saveAchievement("ACHIEVEMENT_D3_POWER_NAP");
		}
		if (starKeyCollider.Raycast(game.playerViewRay, out var _, 100f))
		{
			if (Time.time - starKeyLastMissTime > 30f && !starKeyPicked)
			{
				starKeyPicked = true;
				game.showStarKeyPopup(1);
			}
		}
		else
		{
			starKeyLastMissTime = Time.time;
		}
		if (!musicBoxSolved)
		{
			if (musicBoxReset)
			{
				musicBoxOutput = Mathf.MoveTowards(musicBoxOutput, 0f, Time.deltaTime);
				if (musicBoxOutput == 0f)
				{
					musicBoxReset = false;
				}
			}
			musicBoxLid.setWeight("Down", musicBoxOutput);
		}
		if (solvedPendantText)
		{
			pendantDial.Get<Transform>(0f).Rotate(Vector3.up * 8f);
			if (game.isInTopZoom(pendant.Get<GameObject>(0u)) && Mathf.RoundToInt(pendantDial.Get<Transform>(0f).localRotation.eulerAngles.y) % 40 == 0)
			{
				PineFmod.playOneShotSoundAttached(pendantDial.Get<Dial>(0).soundTurnHotspot, pendantDial.Get<GameObject>(0u));
			}
		}
		if (pendantFloatDownTS.Get<GameObject>(0u).activeSelf)
		{
			waterBubbles.Get<Transform>(0f).position = pendantFloatDownTS.Get<Transform>(0f).GetChild(0).position;
			waterRipples.Get<Transform>(0f).position = new Vector3(pendantFloatDownTS.Get<Transform>(0f).position.x, waterRipples.Get<Transform>(0f).position.y, pendantFloatDownTS.Get<Transform>(0f).position.z);
		}
		if (pendantSurfaceCollider.activeInHierarchy)
		{
			pendantSurfaceCollider.layer = (game.isSelectedInPCMode(pendant.Get<GameObject>(0u)) ? LayerMask.NameToLayer("Default") : LayerMask.NameToLayer("Ignore Raycast"));
		}
		if (pendantLightCounter >= 0f)
		{
			if (pendantLightCounter >= 1f)
			{
				pendantToBright = !pendantToBright;
				pendantLightCounter = 0f;
				MaterialState[] array = pendantMSs;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].transitionTo(pendantToBright ? "Bright" : "On");
				}
			}
			else
			{
				pendantLightCounter += Time.deltaTime;
			}
		}
		if (!solvedDarkWorld)
		{
			if (playersInDarkWorld.Count > 0)
			{
				if (!darkWorldIsMp)
				{
					for (int j = 0; j < darkLineRenderers.Length; j++)
					{
						LineRenderer obj = darkLineRenderers[j];
						float startWidth = (darkLineRenderers[j].endWidth = Mathf.MoveTowards(darkLineRenderers[j].startWidth, correctDarkPlacement[j] ? 0f : startingLineWidth, startingLineWidth * Time.deltaTime));
						obj.startWidth = startWidth;
					}
				}
				else
				{
					for (int k = 0; k < darkWorldCountMP; k++)
					{
						LineRenderer obj2 = darkLineRenderersMP[k];
						float startWidth = (darkLineRenderersMP[k].endWidth = Mathf.MoveTowards(darkLineRenderersMP[k].startWidth, correctDarkPlacementMP[k] ? 0f : startingLineWidth, startingLineWidth * Time.deltaTime));
						obj2.startWidth = startWidth;
					}
				}
				float num3 = 0f;
				if (!darkWorldIsMp)
				{
					for (int l = 0; l < correctDarkPlacement.Length; l++)
					{
						if (correctDarkPlacement[l])
						{
							num3 += 1f / (float)correctDarkPlacement.Length;
						}
					}
				}
				else
				{
					for (int m = 0; m < darkWorldCountMP; m++)
					{
						if (correctDarkPlacementMP[m])
						{
							num3 += 1f / (float)darkWorldCountMP;
						}
					}
				}
				Vector3 target = Vector3.one * Mathf.Lerp(0.5f, 0.2f, num3);
				darkSphere.Get<Transform>(0f).localScale = Vector3.MoveTowards(darkSphere.Get<Transform>(0f).localScale, target, 0.5f * Time.deltaTime);
			}
		}
		else if (shouldToggleDarkLines)
		{
			timer += Time.deltaTime;
			if (!darkWorldIsMp)
			{
				for (int n = 0; n < darkLines.Length; n++)
				{
					Color color = Color.Lerp(Color.red, Color.white, Mathf.InverseLerp(0f, toggleInterval, timer));
					darkLineRenderers[n].startColor = color;
					darkLineRenderers[n].endColor = color;
					if (timer > toggleInterval)
					{
						bool flag = UnityEngine.Random.value > 0.5f;
						if (flag)
						{
							darkLineRenderers[n].startColor = Color.red;
							darkLineRenderers[n].endColor = Color.red;
						}
						darkLines[n].SetActive(flag);
					}
				}
			}
			else
			{
				for (int num4 = 0; num4 < darkWorldCountMP; num4++)
				{
					Color color2 = Color.Lerp(Color.red, Color.white, Mathf.InverseLerp(0f, toggleInterval, timer));
					darkLineRenderersMP[num4].startColor = color2;
					darkLineRenderersMP[num4].endColor = color2;
					if (timer > toggleInterval)
					{
						bool flag2 = UnityEngine.Random.value > 0.5f;
						if (flag2)
						{
							darkLineRenderersMP[num4].startColor = Color.red;
							darkLineRenderersMP[num4].endColor = Color.red;
						}
						darkLinesMP[num4].SetActive(flag2);
					}
				}
			}
			if (timer > toggleInterval)
			{
				timer = 0f;
			}
		}
		if (isInDarkWorld)
		{
			PineFmod.set3DAttributes(darkWorldInstance, PineFmod.to3DAttributes(game.playerRig));
			bool flag3 = !UnityUtils.closeEnough(darkWorldPlayerLastPos, game.localPlayerData.lastTransformPose.position);
			if (darkWorldPlayerMoved != flag3)
			{
				if (flag3)
				{
					PineFmod.start(darkWorldInstance);
				}
				else
				{
					PineFmod.stop(darkWorldInstance, FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
				}
			}
			darkWorldPlayerLastPos = game.localPlayerData.lastTransformPose.position;
			darkWorldPlayerMoved = flag3;
		}
		if (!darkWorldIsMp)
		{
			for (int num5 = 0; num5 < darkLinesPSs.Length; num5++)
			{
				if (darkLinesPSs[num5].isPlaying && correctDarkPlacement[num5])
				{
					darkLinesPSs[num5].Stop();
				}
				else if (!darkLinesPSs[num5].isPlaying && !correctDarkPlacement[num5])
				{
					darkLinesPSs[num5].Play();
				}
			}
		}
		else
		{
			for (int num6 = 0; num6 < darkWorldCountMP; num6++)
			{
				if (darkLinesPSsMP[num6].isPlaying && correctDarkPlacementMP[num6])
				{
					darkLinesPSsMP[num6].Stop();
				}
				else if (!darkLinesPSsMP[num6].isPlaying && !correctDarkPlacementMP[num6])
				{
					darkLinesPSsMP[num6].Play();
				}
			}
		}
		if (game.getPlayerCount() > 1)
		{
			for (int num7 = 0; num7 < game.netPlayers.Count && num7 < darkWorldPlayerVFX.Length; num7++)
			{
				bool flag4 = playersShouldSparkle || playersInDarkWorld.Contains(game.netPlayers[num7].id);
				darkWorldPlayerVFX[num7].Get<GameObject>(0).SetActive(flag4);
				darkWorldPlayerVFX[num7].Get<Transform>(0f).position = game.netPlayers[num7].models.transform.position;
				Transform[] componentsInChildren = game.netPlayers[num7].models.GetComponentsInChildren<Transform>(includeInactive: true);
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].gameObject.layer = LayerMask.NameToLayer(flag4 ? "SpecialIgnoreInMain" : "Characters");
				}
				UnityEngine.Debug.Log($"onPlayer {game.localPlayerData.id}, effect for player {game.netPlayers[num7].id} isOn: {flag4}");
			}
		}
		if (game.getPlayerCount() > currentPlayerCount)
		{
			for (int num8 = playersInDarkWorld.Count - 1; num8 >= 0; num8--)
			{
				exitDarkWorld(playersInDarkWorld[num8]);
			}
			playersInDarkWorld.Clear();
			currentPlayerCount = game.getPlayerCount();
		}
		if (!solvedBc2Up)
		{
			Vector3 vector = Vector3.down * Time.deltaTime * 0.25f;
			if (tryFindCorrespondingNodeOverride(bc2TopGraph, bc2TopGraph.piecesList[0], isLinearPosition: false, out var correspondingNode) && bc2TryGetNextEdge(correspondingNode, vector, out var nextEdgeIndex))
			{
				bc2EdgeIndex = nextEdgeIndex;
			}
			if (bc2EdgeIndex >= 0)
			{
				SlidableGraph.Edge edge = bc2TopGraph.edges[bc2EdgeIndex];
				Vector3 position = bc2RotatingSlidingPiece.position;
				Vector3 onNormal = edge.endPoint - edge.startPoint;
				float value = Vector3.Dot(position + Vector3.Project(vector, onNormal) - edge.startPoint, onNormal.normalized) / onNormal.magnitude;
				bc2RotatingSlidingPiece.position = Vector3.Lerp(edge.startPoint, edge.endPoint, Mathf.Clamp01(value));
				if (Mathf.Clamp01(value) == 1f || Mathf.Clamp01(value) == 0f)
				{
					if (!ballPlayedHit)
					{
						PineFmod.playOneShotSoundAttached("event:/Sound Effects/04 Items/Metal/Small_Metal_Item/Small_Metal_Item_Hit", bc2TopGraph.gameObject);
						ballPlayedHit = true;
					}
				}
				else
				{
					ballPlayedHit = false;
				}
				if (Vector3.Distance(position, bc2RotatingSlidingPiece.position) > 0.001f)
				{
					ballStopSoundDelay = 0f;
					if (ballSoundInstance.getPlaybackState(out var state) == RESULT.OK && state != PLAYBACK_STATE.PLAYING)
					{
						PineFmod.start(ballSoundInstance);
					}
				}
				else if (ballStopSoundDelay >= 0.1f)
				{
					PineFmod.stop(ballSoundInstance, FMOD.Studio.STOP_MODE.IMMEDIATE);
				}
				else
				{
					ballStopSoundDelay += Time.deltaTime;
				}
			}
		}
		else
		{
			bc2RotatingSlidingPiece.position = Vector3.MoveTowards(bc2RotatingSlidingPiece.position, bc2TopFinal.position, Time.deltaTime * 0.01f);
		}
		updateTransitionToLighting();
		if (!solvedDarkWorld || exitKeyButton.gameObject.activeSelf)
		{
			float t = Mathf.SmoothStep(0f, 1f, Mathf.PingPong(exitKeyFloatOffset, 1f));
			exitKey.transform.position = Vector3.Lerp(exitKeyOriginalPos - Vector3.up * 0.025f, exitKeyOriginalPos + Vector3.up * 0.025f, t);
			exitKeyFloatOffset = Mathf.Repeat(exitKeyFloatOffset + Time.deltaTime * 0.5f, 2f);
		}
		static bool tryFindCorrespondingNodeOverride(SlidableGraph graph, SlidableGraphPiece piece, bool isLinearPosition, out GameObject reference)
		{
			float num9 = float.PositiveInfinity;
			reference = null;
			foreach (GameObject node in graph.nodes)
			{
				float num10 = Vector3.Distance(isLinearPosition ? piece.currentLinearPosition : piece.transform.position, node.transform.position);
				if (!(num10 >= num9))
				{
					num9 = num10;
					reference = node;
				}
			}
			return num9 < graph.nodeRadius;
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(bc2SliderPoint.transform.position + Vector3.down * bc2RayDistance, bc2SphereRadius);
	}

	public override void onSwitch3D(Switch3D targetSwitch, Switch3DEvent switchEvent)
	{
		if (chestLockButtons.Any((Switch3D x) => x == targetSwitch) && (switchEvent == Switch3DEvent.On || switchEvent == Switch3DEvent.Off) && !checkBedSolution())
		{
			game.callRPC(RPC.ResetBed);
		}
		if (Array.IndexOf(bathroomHandles, targetSwitch) >= 0 && switchEvent == Switch3DEvent.Start)
		{
			if (bathroomFourthHandleSolution[bathroomFourthHandleCurrent] == targetSwitch)
			{
				bathroomFourthHandleCurrent++;
			}
			else if (targetSwitch == bathroomFourthHandleSolution[0])
			{
				bathroomFourthHandleCurrent = 1;
			}
			else
			{
				bathroomFourthHandleCurrent = 0;
			}
		}
		if (targetSwitch == bathtubValve && switchEvent == Switch3DEvent.Start)
		{
			addWaterToBath();
		}
		if (targetSwitch == bathFluidSw && switchEvent == Switch3DEvent.Start)
		{
			if (playersInDarkWorld.Contains(bathFluidSw.authorityPlayerId))
			{
				exitDarkWorld(bathFluidSw.authorityPlayerId);
			}
			else
			{
				enterDarkWorld(bathFluidSw.authorityPlayerId);
			}
		}
		if (targetSwitch == exitKeyButton && switchEvent == Switch3DEvent.Start)
		{
			exitKey.targetable = true;
			exitKey.hasRigidbody = true;
			foreach (Ref<MaterialState, GameObject> exitKeyMaterialState in exitKeyMaterialStates)
			{
				exitKeyMaterialState.Get<MaterialState>(0).setState("Glow", 0f);
				exitKeyMaterialState.Get<GameObject>(0f).layer = LayerMask.NameToLayer("Default");
			}
			targetSwitch.gameObject.SetActive(value: false);
			PineFmod.stop(keyLightInstance, FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
		}
		if (targetSwitch == levelExitRef.Get<Switch3D>(0f))
		{
			game.levelCompleted();
		}
	}

	public override void onSlot(Slot targetSlot)
	{
		if (targetSlot == musicBoxSlot)
		{
			targetSlot.insertedItem.gameObject.SetActive(value: false);
			targetSlot.gameObject.SetActive(value: false);
			musicBoxDials[0].gameObject.SetActive(value: true);
		}
		else if (targetSlot == jewelryBoxSlot)
		{
			game.changeExamineRotation(jewelryBox.gameObject, new Vector2(0f, 40f), 0.3f);
			solveJewelryBox();
		}
		int num = Array.IndexOf(balconySlots, targetSlot);
		if (num >= 0)
		{
			onBalconySlot(num);
		}
		if (targetSlot == balconySlot && targetSlot.insertedItem == targetSlot.acceptItems[0])
		{
			game.startTimer(new OpenBalconyDoorTimer(), 0.5f);
		}
		int num2 = Array.IndexOf(pipeSlots, targetSlot);
		if (num2 >= 0)
		{
			pipeSlots[num2].targetable = manholeSequence.sequenceTime > 0.5f;
			if (targetSlot.insertedItem != null)
			{
				pipesVisiblyInSlot[num2] = targetSlot.insertedItem;
			}
		}
		if (doorSlot == targetSlot && targetSlot.insertedItem == targetSlot.acceptItems[0])
		{
			game.startTimer(new OpenBathroomDoorTimer(), 0.5f);
		}
		if (exitSlot == targetSlot && targetSlot.insertedItem == targetSlot.acceptItems[0])
		{
			exitDoor.transitionTo("Down", 0.5f, 1f, playSound: true, 1f);
			levelExitRef.Get<GameObject>(0).SetActive(value: true);
			game.finishPuzzle(Puzzle.Exit);
		}
	}

	public override void onRemoveFromSlot(Slot targetSlot, Item item)
	{
		int num = Array.IndexOf(balconySlots, targetSlot);
		if (num >= 0)
		{
			onBalconySlot(num, item.gameObject);
		}
	}

	public override void onUnlock(Lock targetLock)
	{
		if (targetLock == bookcaseLock)
		{
			game.startTimer(new BookcaseLockDelayTimer(), 0.2f);
		}
		else if (targetLock == bedSideLock)
		{
			solveBedLock();
		}
	}

	public override void onDialMoved(Dial dial, MoveEvent moveEvent)
	{
		int index = UnityUtils.getIndex(musicBoxDials, dial);
		if (index >= 0 && !musicBoxSolved && !musicBoxReset)
		{
			float num = Mathf.Max(0f, dial.angleChange * -0.025f);
			if (musicBoxCurrentDial == index && !musicBoxBreached)
			{
				musicBoxAddition += num;
				if (musicBoxBaseline + musicBoxAddition >= MusicBoxBreach[musicBoxCurrentLevel])
				{
					musicBoxBreached = true;
					changeAfterBreach = 0f;
					musicBoxBaseline = MusicBoxBreach[musicBoxCurrentLevel];
					musicBoxCurrentLevel++;
				}
			}
			float num2 = musicBoxBaseline + musicBoxAddition;
			if (musicBoxBreached || musicBoxCurrentDial != index)
			{
				changeAfterBreach += num;
				float num3 = (Mathf.Sin(changeAfterBreach * 2f) * 0.5f + 0.5f) * 2.5f;
				num2 = musicBoxBaseline + ((num < 0f) ? 0f : num3);
				if (changeAfterBreach >= 10f)
				{
					musicBoxReset = true;
					musicBoxBreached = false;
					changeAfterBreach = 0f;
					musicBoxBaseline = 0f;
					musicBoxCurrentLevel = 0;
					musicBoxAddition = 0f;
					musicBoxCurrentDial = 0;
				}
			}
			musicBoxOutput = num2 / 180f;
		}
		if (index >= 0 && moveEvent == MoveEvent.Released)
		{
			changeAfterBreach = 0f;
			if (musicBoxBreached)
			{
				musicBoxCurrentDial = Mathf.Abs(musicBoxCurrentDial - 1);
				musicBoxAddition = 0f;
				musicBoxBreached = false;
			}
		}
		if (moveEvent != MoveEvent.Snapped || !(pendantDial.Get<Dial>(0) == dial) || solvedPendantText)
		{
			return;
		}
		enablePendantText();
		if (dial.value == 0 || locketText.text.Length >= pendantChars.Length)
		{
			disablePendantText();
			return;
		}
		if (dial.value != 0 && locketText.text.Length < pendantPins.Length)
		{
			pendantPins[locketText.text.Length].transitionTo("Hide");
		}
		locketText.text += pendantChars[dial.value - 1];
	}

	public override void onZoomEnter(GameObject zoomedItem)
	{
		if (pendant.Get<GameObject>(0u) == zoomedItem)
		{
			locketUI.SetActive(locketText.text.Length > 0);
		}
		if (zoomedItem == musicBoxSlotHandle.gameObject)
		{
			musicBoxSlotHandleWorld.SetActive(value: false);
			musicBoxSlotHandleInventory.SetActive(value: true);
		}
	}

	public override void onZoomLeave(GameObject zoomedItem)
	{
		if (pendant.Get<GameObject>(0u) == zoomedItem)
		{
			locketUI.SetActive(value: false);
		}
		if (zoomedItem == musicBoxSlotHandle.gameObject && !game.isInInventory(musicBoxSlotHandle.gameObject))
		{
			musicBoxSlotHandleWorld.SetActive(value: true);
			musicBoxSlotHandleInventory.SetActive(value: false);
		}
	}

	public override void onTrigger(Trigger trigger, TriggerEvent triggerEvent)
	{
		if (triggerEvent.type == TriggerEventType.Started)
		{
			dracula3OnTriggerEnter();
		}
		else if (triggerEvent.type == TriggerEventType.Ended)
		{
			dracula3OnTriggerExit();
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
		void dracula3OnTriggerEnter()
		{
			int num = darkTriggers.IndexOf(trigger, 0);
			if (num >= 0)
			{
				UnityEngine.Debug.Log($"Dark world trigger enter {num}");
				correctDarkPlacement[num] = true;
			}
			num = darkTriggersMP.IndexOf(trigger, 0);
			if (num >= 0)
			{
				UnityEngine.Debug.Log($"Dark world trigger enter {num}");
				correctDarkPlacementMP[num] = true;
			}
			if (trigger == pendantTrigger.Get<Trigger>(0))
			{
				bool flag = checkPendantSolution(fromTrigger: true);
				bool flag2 = !pendantFloated && pendantSurfaceCollider.activeInHierarchy;
				Transform transform = (flag ? pendantSolvedFloatSequence.Get<Transform>(0f) : pendantFloatDownTS.Get<Transform>(0f));
				if (flag2)
				{
					pendantFloated = true;
					transform.position = new Vector3(pendant.Get<Transform>(0f).position.x, transform.position.y, pendant.Get<Transform>(0f).position.z);
					transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, pendant.Get<Transform>(0f).rotation.eulerAngles.y, pendant.Get<Transform>(0f).rotation.eulerAngles.z);
					waterBubbles.Get<Transform>(0f).position = pendant.Get<Transform>(0f).position;
					waterRipples.Get<Transform>(0f).position = new Vector3(pendant.Get<Transform>(0f).position.x, waterRipples.Get<Transform>(0f).position.y, pendant.Get<Transform>(0f).position.z);
					pendant.Get<GameObject>(0u).SetActive(value: false);
					transform.gameObject.SetActive(value: true);
					if (!flag)
					{
						pendantFloatDownTS.Get<TweenState>(0).transitionTo("Down", 0.5f);
					}
					waterBubbles.Get<ParticleSystem>(0).Play();
					waterRipples.Get<ParticleSystem>(0).Play();
					PineFmod.playOneShotSoundAttached("event:/Sound Effects/05 Misc/Water/Small_Splash", transform.gameObject);
				}
				if (flag)
				{
					if (flag2)
					{
						pendantSolvedFloatSequence.Get<Sequence>(0).play(-1f, pendantSolvedFloatSequence.Get<Sequence>(0).sequenceDuration);
					}
					else
					{
						pendantBase.SetActive(value: false);
						pendantSequence.Get<GameObject>(0f).SetActive(value: true);
						pendantSequence.Get<Sequence>(0).play(-1f, pendantSequence.Get<Sequence>(0).sequenceDuration);
					}
				}
				else if (flag2)
				{
					game.startTimer(new WaterRipplesTimer(transform), 1f);
				}
			}
		}
		void dracula3OnTriggerExit()
		{
			int num = darkTriggers.IndexOf(trigger, 0);
			if (num >= 0 && !solvedDarkWorld)
			{
				UnityEngine.Debug.Log($"Dark world trigger exit {num}");
				correctDarkPlacement[num] = false;
			}
			num = darkTriggersMP.IndexOf(trigger, 0);
			if (num >= 0 && !solvedDarkWorld)
			{
				UnityEngine.Debug.Log($"Dark world trigger exit {num}");
				correctDarkPlacementMP[num] = false;
			}
		}
	}

	public override void onAddToInventory(Item item)
	{
		int num = Array.IndexOf(pipesVisiblyInSlot, item);
		if (num >= 0)
		{
			pipesVisiblyInSlot[num] = null;
			enablePipeSlot(num);
		}
		else if (item == pendant.Get<Item>(0))
		{
			pendantFloated = false;
		}
		else if (item == musicBoxSlotHandle)
		{
			musicBoxSlotHandleWorld.SetActive(value: false);
			musicBoxSlotHandleInventory.SetActive(value: true);
		}
		if (item.gameObject == vasePetalsPickupGO)
		{
			vasePetalsVFX.SetActive(value: false);
		}
		if (item == exitKey)
		{
			darkKeyLensflare.Stop();
		}
	}

	public override void onRemoveFromInventory(Item item)
	{
		if (item == musicBoxSlotHandle && !game.isInTopZoom(musicBoxLid.gameObject))
		{
			musicBoxSlotHandleWorld.SetActive(value: true);
			musicBoxSlotHandleInventory.SetActive(value: false);
		}
	}

	public override void onTool(Item tool, ToolContext context)
	{
		UnityEngine.Debug.Log("tool state: " + context.state);
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

	public override void onTweenTransitionDone(TweenState tweenState, string state)
	{
		if (tweenState == birdcage1Door)
		{
			balconyStatues[1].targetable = true;
		}
		else if (tweenState == exitDoor)
		{
			game.levelCompleted();
		}
		else if (tweenState == chestLockerTween)
		{
			chestLockPart.transitionTo("Down");
		}
		else if (tweenState == chestLockPart)
		{
			chestLid.targetable = true;
			game.startSwitch(chestLid);
			chestLock.hasRigidbody = true;
			chestLock.targetable = true;
		}
		else if (tweenState == musicBoxKeyLid)
		{
			musicBoxKey.targetable = true;
		}
		else if (tweenState == bc2Tween)
		{
			balconyStatues[4].targetable = true;
		}
		else if (tweenState == bedLockTS)
		{
			bedLockGO.hasRigidbody = true;
			bedLockGO.targetable = true;
			bedDrawerTs.transitionTo("Open");
			bedDrawer.targetable = true;
			keyBalcony.targetable = true;
		}
	}

	public override void onMaterialTransitionDone(MaterialState materialState, string state)
	{
		if (materialState == bathFluidMs)
		{
			bathFluidSw.targetable = !solvedDarkWorld;
			pendant.Get<GameObject>(0u).SetActive(value: false);
			pendantFloatDownTS.Get<GameObject>(0u).SetActive(value: false);
			pendantSolvedFloatSequence.Get<GameObject>(0u).SetActive(value: false);
		}
		for (int i = 0; i < fireLog.Length; i++)
		{
			if (materialState == fireLog[i].Get<MaterialState>(0f))
			{
				game.setParent(fireLogVFXTransform, game.levelContainerTransform);
				fireLog[i].Get<GameObject>(0).SetActive(value: false);
			}
		}
	}

	public override void onSequenceDone(Sequence sequence)
	{
		if (sequence == manholeSequence)
		{
			if (sequence.sequenceTime == sequence.sequenceDuration)
			{
				for (int i = 0; i < pipeSlots.Length; i++)
				{
					pipeSlots[i].gameObject.SetActive(value: true);
					releasePipeFromSlot(i);
				}
				game.startTimer(new PipesTargetableTimer(), 1f);
				PineFmod.stop(manholeInstances[2], FMOD.Studio.STOP_MODE.IMMEDIATE);
				PineFmod.stop(manholeInstances[3], FMOD.Studio.STOP_MODE.IMMEDIATE);
			}
			else if (sequence.sequenceTime == 0f)
			{
				risingPipeSequence.play(-1f, risingPipeSequence.sequenceDuration);
				PineFmod.start(pipeRisingInstances[0]);
				game.startTimer(new PipeRisingSoundTimer(), 2f);
				PineFmod.stop(manholeInstances[0], FMOD.Studio.STOP_MODE.IMMEDIATE);
				PineFmod.stop(manholeInstances[1], FMOD.Studio.STOP_MODE.IMMEDIATE);
			}
		}
		else if (sequence == risingPipeSequence)
		{
			bathtubValve.targetable = true;
			PineFmod.stop(pipeRisingInstances[1], FMOD.Studio.STOP_MODE.IMMEDIATE);
			PineFmod.playOneShotSoundAttached("event:/Sound Effects/04 Items/Metal/Metal_Hit_03", risingPipeSequence.gameObject);
		}
	}

	public override void onTimerDone(Timer timer)
	{
		if (timer is OpenBalconyDoorTimer)
		{
			openBalconyDoor();
		}
		else if (timer is SolvePipesTimer)
		{
			manholeSequence.play(-1f, 0f);
			PineFmod.start(manholeInstances[2]);
			PineFmod.start(manholeInstances[3]);
			game.startTimer(new ManholesTimer(0), manholeSequence.sequenceDuration * 0.5f);
		}
		else if (timer is OpenBathroomDoorTimer)
		{
			openBathroomDoor();
		}
		else if (timer is SolvePendantTextTimer)
		{
			solvePendantText();
		}
		else if (timer is DisablePendantTextTimer)
		{
			disablePendantText();
			pendantDial.Get<Dial>(0).targetable = true;
			pendantDial.Get<Dial>(0).setValue(0);
		}
		else if (timer is BedButtons1Timer)
		{
			Switch3D[] array = chestLockButtons;
			foreach (Switch3D switch3D in array)
			{
				if (switch3D.state == Switch3DState.On)
				{
					game.startSwitch(switch3D);
				}
			}
			game.startTimer(new BedButtons2Timer(), 0.5f);
		}
		else if (timer is BedButtons2Timer)
		{
			Switch3D[] array = chestLockButtons;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].targetable = true;
			}
		}
		else if (timer is SolveBedChestTimer)
		{
			game.increaseZoomCounter(chestLockZoomable.gameObject);
			chestLockerTween.transitionTo("Down");
		}
		else if (timer is SolveBC2Timer)
		{
			game.increaseZoomCounter(bc2RotatingZoomable.gameObject);
			bc2RotatingZoomable.targetable = false;
			bc2Tween.transitionTo("Open", 1f, 1f, playSound: true, 0.5f);
			game.startTimer(new BC2DoorSoundTimer(), 0.75f);
		}
		else if (timer is BC2DoorSoundTimer)
		{
			PineFmod.playOneShotSoundAttached("event:/Sound Effects/04 Items/Metal/Small Metal Box/Small_Metal_Box_Hit", bc2Tween.gameObject);
		}
		else if (timer is BC1DoorSoundTimer)
		{
			PineFmod.playOneShotSoundAttached("event:/Sound Effects/04 Items/Metal/Small Metal Box/Small_Metal_Box_Hit", birdcage1Door.gameObject);
		}
		else if (timer is EnterDarkWorldTimer enterDarkWorldTimer)
		{
			onEnterDarkWorldTimerDone(enterDarkWorldTimer.playerId);
		}
		else if (timer is EnterDarkWorldTransitionTimer enterDarkWorldTransitionTimer)
		{
			onEnterDarkWorldTransitionTimerDone(enterDarkWorldTransitionTimer.playerId);
		}
		else if (timer is ExitDarkWorldTimer exitDarkWorldTimer)
		{
			onExitDarkWorldTimerDone(exitDarkWorldTimer.playerId);
		}
		else if (timer is ExitDarkWorldTransitionTimer exitDarkWorldTransitionTimer)
		{
			onExitDarkWorldTransitionTimerDone(exitDarkWorldTransitionTimer.playerId);
		}
		else if (timer is DiveEffectTimer diveEffectTimer)
		{
			if (game.session.isLocalPlayer(diveEffectTimer.playerId))
			{
				ParticleSystem[] array2 = bathDiveEffects;
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i].Play();
				}
			}
		}
		else if (timer is DarkWorldCutSceneTimer darkWorldCutSceneTimer)
		{
			if (game.session.isLocalPlayer(darkWorldCutSceneTimer.playerId))
			{
				EnterDarkWorldCutscene.Get<GameObject>(0f).SetActive(value: false);
			}
		}
		else if (timer is EnablePipeEditingTimer)
		{
			enablePipeEditing();
		}
		else if (timer is PipesTargetableTimer)
		{
			Item[] array3 = pipeCubes;
			for (int i = 0; i < array3.Length; i++)
			{
				array3[i].targetable = true;
			}
		}
		else if (timer is WaterParticles1Timer)
		{
			waterFlowParticles[6].Stop();
			PineFmod.stop(waterInstance, FMOD.Studio.STOP_MODE.IMMEDIATE);
			PineFmod.playOneShotSoundAttached("event:/Sound Effects/03 Interactable/Levers/Bathtub/Bathtub_End", waterFlowParticles[6].gameObject);
		}
		else if (timer is WaterParticles2Timer)
		{
			waterFlowParticles[3].Stop();
		}
		else if (timer is WaterParticles3Timer)
		{
			pendantSurfaceCollider.SetActive(pendantTrigger.Get<GameObject>(0f).activeSelf);
			ParticleSystem[] array2 = waterFlowParticles;
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i].Stop();
			}
		}
		else if (timer is WaterRipplesTimer waterRipplesTimer)
		{
			waterRipples.Get<ParticleSystem>(0).Stop();
			game.startTimer(new PendantFloatTimer(waterRipplesTimer.animTransform), 1f);
		}
		else if (timer is PendantFloatTimer { animTransform: var animTransform })
		{
			pendant.Get<Transform>(0f).position = animTransform.GetChild(0).position;
			pendant.Get<Transform>(0f).rotation = animTransform.GetChild(0).rotation;
			pendant.Get<Item>(0).hasRigidbody = false;
			pendant.Get<GameObject>(0u).SetActive(value: true);
			animTransform.gameObject.SetActive(value: false);
			pendantFloatDownTS.Get<TweenState>(0).setWeight("Down", 0f);
			pendantFloatDownTS.Get<TweenState>(0).setWeight("Default", 1f);
			waterBubbles.Get<ParticleSystem>(0).Stop();
		}
		else if (timer is Locations1Timer)
		{
			Zoomable zoomable = rotatePainting.Get<Zoomable>(0f);
			game.increaseZoomCounter(zoomable.gameObject);
			zoomable.targetable = false;
			game.startTimer(new Locations2Timer(), 1f);
		}
		else if (timer is Locations2Timer)
		{
			rotatePainting.Get<TweenState>(0).transitionTo("Up");
			heronStatue.targetable = true;
		}
		else if (timer is SolvePendantTimer)
		{
			bathFluidMs.transitionTo("Darken");
			waterBubbles.Get<ParticleSystem>(0).Stop();
			waterRipples.Get<ParticleSystem>(0).Stop();
			PineFmod.playOneShotSoundAttached("event:/Sound Effects/05 Misc/Magic/Water_Transformation", bathFluidMs.gameObject);
		}
		else if (timer is LightingTimer)
		{
			isTransitioningToLighting = false;
		}
		else if (timer is SolvePainting1Timer)
		{
			fade(0.22f, FadeActionType.PaintingTimer2);
		}
		else if (timer is SolvePainting2Timer)
		{
			fade(0.3f, FadeActionType.RipPaintings);
		}
		else if (timer is ManholesTimer manholesTimer)
		{
			if (manholesTimer.opening == 1)
			{
				PineFmod.stop(manholeInstances[0], FMOD.Studio.STOP_MODE.IMMEDIATE);
				PineFmod.stop(manholeInstances[1], FMOD.Studio.STOP_MODE.IMMEDIATE);
				PineFmod.start(manholeInstances[2]);
				PineFmod.start(manholeInstances[3]);
			}
			else
			{
				PineFmod.start(manholeInstances[0]);
				PineFmod.start(manholeInstances[1]);
				PineFmod.stop(manholeInstances[2], FMOD.Studio.STOP_MODE.IMMEDIATE);
				PineFmod.stop(manholeInstances[3], FMOD.Studio.STOP_MODE.IMMEDIATE);
			}
		}
		else if (timer is PipeRisingSoundTimer)
		{
			PineFmod.stop(pipeRisingInstances[0], FMOD.Studio.STOP_MODE.IMMEDIATE);
			PineFmod.playOneShotSoundAttached("event:/Sound Effects/04 Items/Concrete & Rock/Rock_Hit_01", risingPipeSequence.gameObject);
			PineFmod.start(pipeRisingInstances[1]);
		}
		else if (timer is BookcaseLockDelayTimer)
		{
			solveBookcase();
		}
		else if (timer is SolveJewelryBoxTimer)
		{
			jewelryBoxLid.targetable = true;
			game.startSwitch(jewelryBoxLid);
		}
		else if (timer is BookCaseSound1Timer)
		{
			PineFmod.playOneShotSoundAttached("event:/Sound Effects/03 Interactable/Drawers/Wood_Drawer_01_End", bookcaseCabinetDoors.gameObject);
		}
		else if (timer is BookCaseSound2Timer)
		{
			PineFmod.playOneShotSoundAttached("event:/Sound Effects/03 Interactable/Drawers/Wood_Drawer_01_End", bookcaseCabinetDoors.gameObject);
			PineFmod.stop(bookCaseInstance, FMOD.Studio.STOP_MODE.IMMEDIATE);
		}
		else if (timer is BirdCage1Transition)
		{
			birdcage1DoorGear.transitionTo("Open", 2f);
			game.startTimer(new BC1DoorTimer(), 0.5f);
		}
		else if (timer is BC1DoorTimer)
		{
			birdcage1Door.transitionTo("Open");
			game.startTimer(new BC1DoorSoundTimer(), 0.25f);
			PineFmod.stop(bc1Turnable.soundEmitter);
			PineFmod.playOneShotSoundAttached(bc1Turnable.endSound, bc1Turnable.gameObject);
		}
		else if (timer is BalconyCutSceneTimer)
		{
			balconyCutScene.Get<GameObject>(0f).SetActive(value: false);
		}
		else if (timer is SolveDarkWorldCutSceneTimer)
		{
			solveDarkWorldCutScene.SetActive(value: false);
			shouldToggleDarkLines = false;
			GameObject[] array4 = darkLines;
			for (int i = 0; i < array4.Length; i++)
			{
				array4[i].SetActive(value: false);
			}
			array4 = darkLinesMP;
			for (int i = 0; i < array4.Length; i++)
			{
				array4[i].SetActive(value: false);
			}
			darkSphere.Get<GameObject>(0).SetActive(value: false);
			darkSphereSound.SetActive(value: false);
			exitKeyButton.gameObject.SetActive(value: true);
			game.setParent(exitKey.transform, game.levelContainer.transform);
			game.setParent(exitKeyButton.transform, game.levelContainer.transform);
			darkSphereBurstPS.Play();
			darkKeyLensflare.Stop();
			bathFluidMs.transitionTo("Default");
			PineFmod.playOneShotSoundAttached("event:/Sound Effects/05 Misc/Magic/Water_Transformation", bathFluidMs.gameObject);
			for (int num = playersInDarkWorld.Count - 1; num >= 0; num--)
			{
				exitDarkWorld(playersInDarkWorld[num]);
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

	public override void onTimerUpdate(Timer timer)
	{
		if (timer is DisablePendantTextTimer)
		{
			pendantDial.Get<Transform>(0f).localRotation = Quaternion.Lerp(pendantDial.Get<Transform>(0f).localRotation, Quaternion.identity, timer.unitTime);
		}
		if (timer is FadeTimer fadeTimer)
		{
			if (fadeTimer.fadingOut)
			{
				exposure.fixedExposure.value = Mathf.Lerp(startingExposure, minExposure, fadeTimer.unitTime);
			}
			else
			{
				exposure.fixedExposure.value = Mathf.Lerp(minExposure, startingExposure, fadeTimer.unitTime);
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

	public override void onPose(NetPlayerId playerId, CharacterPose characterPose, CharacterPoseState poseEvent)
	{
		if (playerId == game.session.localPlayerId && (characterPose.gameObject == bedPoses[0] || characterPose.gameObject == bedPoses[1]))
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

	[DebugButton(null, Tint.DarkGreen, PostClickAction.HideButton, 0, new object[] { })]
	private void solveBookcase()
	{
		if (!(bookcaseCabinetDoors.getWeight("Open") > 0f))
		{
			UnityEngine.Debug.Log("Solved bookcase");
			Turnable[] array = bookcaseTurnables;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].targetable = false;
			}
			bookcaseCabinetDoors.transitionTo("Open", 0.5f);
			PineFmod.playOneShotSoundAttached("event:/Sound Effects/03 Interactable/Drawers/Wood_Drawer_01_Start", bookcaseCabinetDoors.gameObject);
			PineFmod.start(bookCaseInstance);
			game.startTimer(new BookCaseSound1Timer(), 0.76f);
			game.startTimer(new BookCaseSound2Timer(), 1.44f);
			game.finishPuzzle(Puzzle.Bookshelf);
		}
	}

	private bool checkBedSolution()
	{
		return checkFlippedSolutions(chestLockButtons, chestSolutionButtons);
	}

	private void resetBedButtons()
	{
		int num = 0;
		Switch3D[] array = chestLockButtons;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].state == Switch3DState.On)
			{
				num++;
			}
		}
		if (num >= 4)
		{
			array = chestLockButtons;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].targetable = false;
			}
			game.startTimer(new BedButtons1Timer(), 1f / chestLockButtons[0].transitionSpeed);
		}
	}

	[DebugButton(null, Tint.DarkGreen, PostClickAction.HideButton, 0, new object[] { })]
	private void solveBedChest()
	{
		chestLockZoomable.targetable = false;
		Switch3D[] array = chestLockButtons;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = false;
		}
		game.startTimer(new SolveBedChestTimer(), 0.5f);
		game.finishPuzzle(Puzzle.BedChest);
	}

	[DebugButton(null, Tint.DarkGreen, PostClickAction.HideButton, 0, new object[] { })]
	private void solveMusicBox()
	{
		musicBoxSolved = true;
		game.changeExamineRotation(musicBoxLid.gameObject, new Vector2(0f, 40f), 0.5f);
		musicBoxLid.transitionTo("Down", 1f, 1f, playSound: true, 0.5f);
		musicBoxKeyLid.transitionTo("Down", 1f, 1f, playSound: true, 1f);
	}

	[DebugButton(null, Tint.DarkGreen, PostClickAction.HideButton, 0, new object[] { })]
	private void solveJewelryBox()
	{
		jewelryBoxSlot.targetable = false;
		if (jewelryBoxSlot.insertedItem != null)
		{
			jewelryBoxSlot.insertedItem.targetable = false;
		}
		pendant.Get<Item>(0).targetable = true;
		pendantHint.targetable = true;
		game.startTimer(new SolveJewelryBoxTimer(), 0.5f);
		if (!pendantHint.gameObject.activeSelf)
		{
			pendantHint.gameObject.SetActive(value: true);
		}
		if (!pendant.Get<GameObject>(0u).activeSelf)
		{
			pendant.Get<GameObject>(0u).SetActive(value: true);
		}
		game.finishPuzzle(Puzzle.BathroomBoxes);
	}

	private bool checkResetPendant()
	{
		if (!solvedPendantText && locketText.text != pendantSolution)
		{
			return locketText.text.Length == pendantPins.Length;
		}
		return false;
	}

	private void disablePendantText()
	{
		UnityEngine.Debug.Log("disable pendant text");
		for (int i = (solvedPendantText ? pendantChars.Length : 0); i < pendantPins.Length; i++)
		{
			pendantPins[i].transitionTo("Default");
		}
		locketUI.SetActive(value: false);
		locketText.text = "";
	}

	private void enablePendantText()
	{
		if (!locketUI.activeSelf && game.isInTopZoom(pendant.Get<GameObject>(0u)))
		{
			UnityEngine.Debug.Log("enable pendant text");
			locketUI.SetActive(value: true);
			if (solvedPendantText)
			{
				locketText.text = Localization.lookupInDictionary("Dracula3_MP_DrownMe");
			}
		}
	}

	private void solvePendantText()
	{
		if (!solvedPendantText)
		{
			solvedPendantText = true;
			locketText.text = Localization.lookupInDictionary("Dracula3_MP_DrownMe");
			for (int i = 0; i < pendantChars.Length; i++)
			{
				pendantPins[i].transitionTo("Hide");
			}
			pendantLightCounter = 0f;
			MaterialState[] array = pendantMSs;
			for (int j = 0; j < array.Length; j++)
			{
				array[j].transitionTo("On");
			}
			game.finishPuzzle(Puzzle.Pendant);
		}
	}

	private bool checkPendantSolution(bool fromTrigger = false)
	{
		if (solvedPendantText && bathSolved)
		{
			return pendantTrigger.Get<Trigger>(0).state || fromTrigger;
		}
		return false;
	}

	[DebugButton(null, Tint.Default, PostClickAction.HideButton, 0, new object[] { })]
	private void getSolvedPendant()
	{
		pendant.Get<Item>(0).targetable = true;
		game.addItemToInventory(pendant.Get<GameObject>(0u));
		solvePendantText();
	}

	[DebugButton(null, Tint.DarkGreen, PostClickAction.HideButton, 0, new object[] { })]
	private void solvePendant()
	{
		UnityEngine.Debug.Log("Solved Pendant");
		pendantPlaced = true;
		if (game.isInInventory(pendant.Get<GameObject>(0u)))
		{
			game.removeItemFromInventory(pendant.Get<GameObject>(0u));
		}
		pendant.Get<Item>(0).targetable = false;
		pendantTrigger.Get<GameObject>(0f).SetActive(value: false);
		pendantSurfaceCollider.SetActive(value: false);
		game.startTimer(new SolvePendantTimer(), 2f);
		if (!darkWorldIsMp)
		{
			for (int i = 0; i < darkTriggers.Length; i++)
			{
				darkTriggers[i].Get<GameObject>(0f).SetActive(value: true);
			}
		}
		else
		{
			for (int j = 0; j < darkTriggersMP.Length; j++)
			{
				darkTriggersMP[j].Get<GameObject>(0f).SetActive(j < darkWorldCountMP);
			}
		}
		game.finishPuzzle(Puzzle.Bathtub);
	}

	private bool checkPaintingsSolution()
	{
		if (solvedPaintings)
		{
			return false;
		}
		return checkFlippedSolutions(paintingsSliders, paintingsToBeFlipped);
	}

	[DebugButton(null, Tint.DarkGreen, PostClickAction.HideButton, 0, new object[] { })]
	private void solvePaintings()
	{
		if (!solvedPaintings)
		{
			solvedPaintings = true;
			Switch3D[] array = paintingsSliders;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].targetable = false;
			}
			fade(0.3f, FadeActionType.PaintingTimer1);
			PineFmod.playOneShotSound("event:/Sound Effects/05 Misc/Magic/Picture_Scratches");
			UnityEngine.Debug.Log("Solved Paintings");
			game.finishPuzzle(Puzzle.Mirror);
		}
	}

	[DebugButton(null, Tint.DarkGreen, PostClickAction.HideButton, 0, new object[] { })]
	private void solveBedLock()
	{
		bedLockZoomable.targetable = false;
		bedLockTS.transitionTo("Open");
		Turnable[] array = bedLockTurnables;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = false;
		}
		bedLockShakeCollider.SetActive(value: false);
		game.increaseZoomCounter(bedLockZoomable.gameObject);
		game.finishPuzzle(Puzzle.PaintingCode);
	}

	private bool checkFireplaceLocations()
	{
		bool flag = true;
		bool flag2 = false;
		for (int i = 0; i < fireplacePlates.Length; i++)
		{
			flag2 |= game.isAnyPlayerInteracting(fireplacePlates[i].gameObject);
			bool flag3 = Vector3.Distance(fireplacePlates[i].transform.position, fireplacePlateSolutions[i].transform.position) > 0.04f;
			if (flag && flag3)
			{
				flag = false;
			}
		}
		if (flag && !fireplaceSolved)
		{
			return !flag2;
		}
		return false;
	}

	[DebugButton(null, Tint.DarkGreen, PostClickAction.HideButton, 0, new object[] { })]
	private void solveFireplaceLocations()
	{
		if (!fireplaceSolved)
		{
			fireplaceSolved = true;
			game.startTimer(new Locations1Timer(), 0.5f);
			game.finishPuzzle(Puzzle.Map);
		}
	}

	private void onBalconySlot(int slotIndex, GameObject removedObject = null)
	{
		int slotPositionIndex = balconySlotIndexes[slotIndex];
		GameObject key = ((removedObject == null) ? balconySlots[slotIndex].insertedItem.gameObject : removedObject);
		int totalCorrect = 0;
		tweenRow(balconyTopRowTweens, firstRow: true, ref totalCorrect);
		tweenRow(balconyBottomRowTweens, firstRow: false, ref totalCorrect);
		void tweenRow(TweenState[] balconyRowTweens, bool firstRow, ref int reference)
		{
			int[] array = statueToPattern[key][(!firstRow) ? 1u : 0u];
			for (int i = 0; i < array.Length; i++)
			{
				int num = array[i] + slotPositionIndex;
				if (num >= 0 && num < balconyRowTweens.Length)
				{
					bool flag = balconyRowTweens[num].findStateByName("Down").targetWeight == 1f;
					balconyRowTweens[num].transitionTo(flag ? "Default" : "Down", 2f);
				}
			}
			for (int j = 0; j < balconyRowTweens.Length; j++)
			{
				if (balconyRowTweens[j].findStateByName("Down").targetWeight == 1f)
				{
					reference++;
				}
			}
		}
	}

	[DebugButton(null, Tint.Default, PostClickAction.HideButton, 0, new object[] { })]
	private void openBalconyDoor()
	{
		balconySlot.targetable = false;
		balconyDoors.targetable = true;
		game.startSwitch(balconyDoors);
		if (balconySlot.insertedItem != null)
		{
			balconySlot.insertedItem.targetable = false;
		}
	}

	[DebugButton(null, Tint.Default, PostClickAction.HideButton, 0, new object[] { })]
	private void debugGetBalconyStatues()
	{
		Item[] array = balconyStatues;
		foreach (Item item in array)
		{
			item.targetable = true;
			game.addItemToInventory(item.gameObject);
		}
	}

	private bool checkBalconySlots()
	{
		return Array.TrueForAll(balconySlots, (Slot x) => game.checkSlotSolution(x, x.acceptItems[0]));
	}

	[DebugButton(null, Tint.DarkGreen, PostClickAction.HideButton, 0, new object[] { })]
	private void solveBalcony()
	{
		Slot[] array = balconySlots;
		foreach (Slot slot in array)
		{
			slot.targetable = false;
			if (slot.insertedItem != null)
			{
				slot.insertedItem.targetable = false;
			}
		}
		Sequence[] array2 = balconySlotSequences;
		foreach (Sequence sequence in array2)
		{
			sequence.play(-1f, sequence.sequenceDuration);
		}
		balconyCutScene.Get<GameObject>(0f).SetActive(value: true);
		game.startTimer(new BalconyCutSceneTimer(), balconyCutScene.Get<AnimationSampler>(0).clip.length);
		balconyTrapDoors.transitionTo("Open", 1f, 1f, playSound: true, 1f);
		UnityEngine.Debug.Log("Solved Balcony");
		game.finishPuzzle(Puzzle.BalconyStatues);
	}

	[DebugButton(null, Tint.Default, PostClickAction.HideButton, 0, new object[] { })]
	private void openBathroomDoor()
	{
		bathroomDoor.targetable = true;
		game.startSwitch(bathroomDoor);
	}

	private bool checkFourthHandle()
	{
		if (!bathroomFourthHandleSolved)
		{
			return bathroomFourthHandleCurrent >= bathroomFourthHandleSolution.Length;
		}
		return false;
	}

	[DebugButton(null, Tint.DarkGreen, PostClickAction.HideButton, 0, new object[] { })]
	private void solveFourthHandle()
	{
		if (!bathroomFourthHandleSolved)
		{
			bathroomFourthHandleSolved = true;
			for (int i = 0; i < bathroomHandles.Length; i++)
			{
				bathroomHandles[i].tweenState.transitionTo("Disable", 0.5f, 1f, playSound: true, 0.5f);
				bathroomHandles[i].targetable = false;
			}
			game.startTimer(new EnablePipeEditingTimer(), 1f, 0.5f);
			game.finishPuzzle(Puzzle.BathroomHandles);
		}
	}

	private void enablePipeEditing()
	{
		if (!pipesLifted)
		{
			pipesLifted = true;
			manholeSequence.play(-1f, manholeSequence.sequenceDuration);
			PineFmod.start(manholeInstances[0]);
			PineFmod.start(manholeInstances[1]);
			game.startTimer(new ManholesTimer(1), manholeSequence.sequenceDuration * 0.5f);
		}
	}

	private void releasePipeFromSlot(int slotIndex)
	{
		if (!pipesSolved)
		{
			Slot slot = pipeSlots[slotIndex];
			if (!(slot.insertedItem == null))
			{
				Item insertedItem = slot.insertedItem;
				game.removeItemFromSlot(insertedItem);
				slot.targetable = false;
				insertedItem.hasRigidbody = false;
				game.setParent(insertedItem.transform, pipeSlotParents[slotIndex].transform);
				pipeSlotParents[slotIndex].transitionTo("Up", 1.5f);
			}
		}
	}

	private void enablePipeSlot(int slotIndex)
	{
		pipeSlots[slotIndex].targetable = true;
		pipeSlotParents[slotIndex].setState("Up", 0f);
	}

	private bool checkPipesAllIn()
	{
		bool flag = true;
		Slot[] array = pipeSlots;
		foreach (Slot slot in array)
		{
			flag &= slot.insertedItem != null;
		}
		return flag;
	}

	private bool checkPipesSolved()
	{
		bool flag = true;
		Slot[] array = pipeSlots;
		foreach (Slot slot in array)
		{
			flag &= game.checkSlotSolution(slot, slot.acceptItems[0]);
		}
		return flag;
	}

	private bool checkPipesRelease()
	{
		if (bathroomFourthHandleSolved && manholeSequence.sequenceTime > manholeSequence.sequenceDuration - 0.1f && checkPipesAllIn())
		{
			return !checkPipesSolved();
		}
		return false;
	}

	[DebugButton(null, Tint.DarkGreen, PostClickAction.HideButton, 0, new object[] { })]
	private void solvePipes()
	{
		if (pipesSolved)
		{
			return;
		}
		pipesSolved = true;
		Slot[] array = pipeSlots;
		foreach (Slot slot in array)
		{
			slot.targetable = false;
			if (slot.insertedItem != null)
			{
				slot.insertedItem.targetable = false;
			}
		}
		game.startTimer(new SolvePipesTimer(), 1f);
		game.finishPuzzle(Puzzle.PipeBlocks);
	}

	private void addWaterToBath()
	{
		if (!bathSolved)
		{
			UnityEngine.Debug.Log("Bath solved");
			bathSolved = true;
			pendantTrigger.Get<GameObject>(0f).SetActive(value: true);
			bathFluidTS.transitionTo("Up", 0.5f);
			bathRunningWater.SetActive(value: true);
			bathtubValve.targetable = false;
			ParticleSystem[] array = waterFlowParticles;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Play();
			}
			PineFmod.start(waterInstance);
			PineFmod.playOneShotSoundAttached("event:/Sound Effects/03 Interactable/Levers/Bathtub/Bathtub_Start", waterFlowParticles[6].gameObject);
			game.startTimer(new WaterParticles1Timer(), 1.8f);
			game.startTimer(new WaterParticles2Timer(), 1.9f);
			game.startTimer(new WaterParticles3Timer(), 2f);
		}
	}

	[DebugButton(null, Tint.Default, PostClickAction.ReturnToGame, 0, new object[] { })]
	private void enterDarkWorld(NetPlayerId playerId)
	{
		playersInDarkWorld.Add(playerId);
		bathFluidSw.targetable = false;
		game.startTimer(new EnterDarkWorldTimer(playerId), 1f);
		playDarkWorldSequence(playerId);
		if (game.session.isLocalPlayer(playerId))
		{
			isInDarkWorld = true;
			UnityEngine.Debug.Log($"{game.session.localPlayerId} Entered Dark World");
		}
	}

	[DebugButton(null, Tint.Default, PostClickAction.ReturnToGame, 0, new object[] { })]
	private void exitDarkWorld(NetPlayerId playerId)
	{
		playersInDarkWorld.Remove(playerId);
		bathFluidSw.targetable = false;
		game.startTimer(new ExitDarkWorldTimer(playerId), 1f);
		playDarkWorldSequence(playerId);
		if (game.session.isLocalPlayer(playerId))
		{
			UnityEngine.Debug.Log($"{game.session.localPlayerId} Exited Dark World");
		}
	}

	private void playDarkWorldSequence(NetPlayerId playerId)
	{
		game.startTimer(new DiveEffectTimer(playerId), DarkDiveEffectDelay);
		game.startTimer(new DarkWorldCutSceneTimer(playerId), EnterDarkWorldCutscene.Get<Sequence>(0).sequenceDuration);
		if (game.session.isLocalPlayer(playerId))
		{
			EnterDarkWorldCutscene.Get<Sequence>(0).play(0f, EnterDarkWorldCutscene.Get<Sequence>(0).sequenceDuration);
			EnterDarkWorldCutscene.Get<GameObject>(0f).SetActive(value: true);
		}
	}

	private void onEnterDarkWorldTimerDone(NetPlayerId playerId)
	{
		game.startTimer(new EnterDarkWorldTransitionTimer(playerId), DarkWorldTransitionTimeOut);
		if (!game.session.isLocalPlayer(playerId))
		{
			return;
		}
		Interactive[] array = allInteractives;
		foreach (Interactive interactive in array)
		{
			if (!(interactive.gameObject == bathFluidSw.gameObject) && !(interactive.gameObject == exitKey.gameObject) && !(interactive.gameObject == exitKeyButton.gameObject))
			{
				interactiveToTargetable[interactive] = interactive.targetable;
				UnityEngine.Debug.Log(interactive.name + ", " + interactive.targetable);
				interactive.targetable = false;
			}
		}
		GameObject[] array2 = darkWorldFire;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].SetActive(value: true);
		}
		Renderer[] array3 = darkWorldDisableRenderers;
		for (int i = 0; i < array3.Length; i++)
		{
			array3[i].enabled = false;
		}
		array2 = disableInDarkWorld;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].SetActive(value: false);
		}
		Transform[] array4 = footstepsInDarkWorld;
		for (int i = 0; i < array4.Length; i++)
		{
			array4[i].position = 100f * Vector3.up;
		}
		insideDarkworld.SetActive(value: true);
		darkSphereSound.SetActive(darkSphere.Get<GameObject>(0).activeSelf);
		normalLightingParent.SetActive(value: false);
		darkLightingParent.SetActive(value: true);
		darkWorldDust.Play();
		if (!exitKey.hasRigidbody)
		{
			darkKeyLensflare.Play();
		}
		PineFmod.SetParameter(musicEmitter, "D3_Dark_World_Switch", 1f);
		PineFmod.SetParameter(fireplaceEmitter, "D3_Fireplace", 1f);
		game.soundStepDefault = default(EventReference);
		playersShouldSparkle = true;
	}

	private void onEnterDarkWorldTransitionTimerDone(NetPlayerId playerId)
	{
		bathFluidSw.targetable = true;
		if (game.session.isLocalPlayer(playerId))
		{
			darkWorldPlayerLastPos = game.localPlayerData.lastTransformPose.position;
		}
	}

	private void onExitDarkWorldTimerDone(NetPlayerId playerId)
	{
		game.startTimer(new ExitDarkWorldTransitionTimer(playerId), DarkWorldTransitionTimeOut);
		if (!game.session.isLocalPlayer(playerId))
		{
			return;
		}
		foreach (KeyValuePair<Interactive, bool> item in interactiveToTargetable)
		{
			UnityEngine.Debug.Log(item.Key.name + ", " + item.Value);
			item.Key.targetable = item.Value;
		}
		insideDarkworld.SetActive(value: false);
		darkSphereSound.SetActive(value: false);
		GameObject[] array = darkWorldFire;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(value: false);
		}
		Renderer[] array2 = darkWorldDisableRenderers;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].enabled = true;
		}
		array = disableInDarkWorld;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(value: true);
		}
		Transform[] array3 = footstepsInDarkWorld;
		for (int i = 0; i < array3.Length; i++)
		{
			array3[i].position = Vector3.zero;
		}
		normalLightingParent.SetActive(value: true);
		darkLightingParent.SetActive(value: false);
		darkWorldDust.Stop();
		PineFmod.SetParameter(musicEmitter, "D3_Dark_World_Switch", 0f);
		PineFmod.SetParameter(fireplaceEmitter, "D3_Fireplace", 0f);
		game.soundStepDefault = soundStep;
		if (solvedDarkWorld && exitKeyButton.gameObject.activeSelf)
		{
			PineFmod.start(keyLightInstance);
		}
		playersShouldSparkle = false;
	}

	private void onExitDarkWorldTransitionTimerDone(NetPlayerId playerId)
	{
		bathFluidSw.targetable = !solvedDarkWorld;
		if (game.session.isLocalPlayer(playerId))
		{
			isInDarkWorld = false;
		}
	}

	private bool checkDarkWorldSolved()
	{
		bool flag = true;
		if (!darkWorldIsMp)
		{
			bool[] array = correctDarkPlacement;
			foreach (bool flag2 in array)
			{
				flag = flag && flag2;
			}
		}
		else
		{
			for (int j = 0; j < darkWorldCountMP; j++)
			{
				flag = flag && correctDarkPlacementMP[j];
			}
		}
		return playersInDarkWorld.Count > 0 && flag;
	}

	[DebugButton(null, Tint.DarkGreen, PostClickAction.HideButton, 0, new object[] { })]
	private void solveDarkWorld()
	{
		if (!solvedDarkWorld)
		{
			solvedDarkWorld = true;
			UnityEngine.Debug.Log("Solved the Dark World");
			bathFluidSw.targetable = false;
			exitKeyButton.targetable = true;
			shouldToggleDarkLines = true;
			for (int i = 0; i < darkLineRenderers.Length; i++)
			{
				LineRenderer obj = darkLineRenderers[i];
				float startWidth = (darkLineRenderers[i].endWidth = startingLineWidth);
				obj.startWidth = startWidth;
			}
			for (int j = 0; j < darkWorldCountMP; j++)
			{
				LineRenderer obj2 = darkLineRenderersMP[j];
				float startWidth = (darkLineRenderersMP[j].endWidth = startingLineWidth);
				obj2.startWidth = startWidth;
			}
			solveDarkWorldCutScene.SetActive(value: true);
			game.startTimer(new SolveDarkWorldCutSceneTimer(), 4f);
			game.finishPuzzle(Puzzle.DarkWorld);
		}
	}

	private bool checkBirtCage1Solved()
	{
		return Array.TrueForAll(birdcageSlots, (Slot x) => game.checkSlotSolution(x, x.acceptItems[0]));
	}

	[DebugButton(null, Tint.DarkGreen, PostClickAction.HideButton, 0, new object[] { })]
	private void solveBirdcage1()
	{
		Slot[] array = birdcageSlots;
		foreach (Slot slot in array)
		{
			slot.targetable = false;
			if (slot.insertedItem != null)
			{
				slot.insertedItem.targetable = false;
			}
		}
		bc1Turnable.targetable = false;
		Game obj = game;
		BirdCage1Transition transition = new BirdCage1Transition();
		Transform obj2 = bc1Turnable.transform;
		Quaternion? rotation = Quaternion.Euler(0f, 0f, 55f);
		obj.startTransitionLocal(transition, obj2, 1f, 0f, null, rotation);
		PineFmod.playOneShotSoundAttached(bc1Turnable.startSound, bc1Turnable.gameObject);
		PineFmod.play(bc1Turnable.soundEmitter);
		game.finishPuzzle(Puzzle.PictureCage);
	}

	[DebugButton(null, Tint.DarkGreen, PostClickAction.HideButton, 0, new object[] { })]
	private void solveBc2Top()
	{
		solvedBc2Up = true;
		bc2TopDial.targetable = false;
		bc2TopDial.transform.localRotation = Quaternion.Euler(90f, 52.23083f, 0f);
		bc2PressurePlateTS.transitionToDuration("Down", 0.5f, 1f, playSound: true, 0.25f);
		game.startTimer(new SolveBC2Timer(), 1f);
		PineFmod.stop(ballSoundInstance, FMOD.Studio.STOP_MODE.IMMEDIATE);
		game.finishPuzzle(Puzzle.MazeCage);
	}

	private bool bc2TryGetNextEdge(GameObject node, Vector3 movement, out int nextEdgeIndex)
	{
		float num = 360f;
		nextEdgeIndex = -1;
		for (int i = 0; i < bc2TopGraph.edges.Count; i++)
		{
			SlidableGraph.Edge edge = bc2TopGraph.edges[i];
			if (node == edge.startNode || node == edge.endNode)
			{
				Vector3 vector = ((node == edge.startNode) ? edge.startPoint : edge.endPoint);
				float num2 = Vector3.Angle((((node == edge.startNode) ? edge.endPoint : edge.startPoint) - vector).normalized, movement);
				if (num2 < num)
				{
					num = num2;
					nextEdgeIndex = i;
				}
			}
		}
		return nextEdgeIndex >= 0;
	}

	private bool checkFlippedSolutions(Switch3D[] allButtons, Switch3D[] flippedSolutions)
	{
		foreach (Switch3D slider in allButtons)
		{
			if (flippedSolutions.Any((Switch3D x) => x == slider))
			{
				if (slider.state == Switch3DState.Off)
				{
					return false;
				}
			}
			else if (slider.state == Switch3DState.On)
			{
				return false;
			}
		}
		return true;
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
		case FadeActionType.RipPaintings:
		{
			GameObject[] array = rippedPaintings;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetActive(value: true);
			}
			array = nonRippedPaintings;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetActive(value: false);
			}
			Renderer[] array2 = normalPaintingsMaterialStates;
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i].material = blackPaintingMaterial;
			}
			paintingsMirrorMR.material = paintingsMirrorBrokenMat;
			break;
		}
		case FadeActionType.PaintingTimer1:
		case FadeActionType.PaintingTimer2:
			break;
		}
	}

	private void onFadeActionComplete(FadeActionType actionType)
	{
		switch (actionType)
		{
		case FadeActionType.PaintingTimer1:
			game.startTimer(new SolvePainting1Timer(), 0.2f);
			break;
		case FadeActionType.PaintingTimer2:
			game.startTimer(new SolvePainting2Timer(), 0.3f);
			break;
		case FadeActionType.RipPaintings:
			break;
		}
	}

	private void updateTransitionToLighting()
	{
		if (probeRefVolume == null && ProbeReferenceVolume.instance.isInitialized)
		{
			isTransitioningToLighting = true;
		}
		if (isTransitioningToLighting)
		{
			game.startTimer(new LightingTimer(), 2f);
		}
	}

	[DebugButton("Get Birdcage Tiles", Tint.Default, PostClickAction.HideButton, 0, new object[] { })]
	private void debugGetBirdcageTiles()
	{
		Item[] array = birdcageTiles;
		foreach (Item item in array)
		{
			item.targetable = true;
			game.addItemToInventory(item.gameObject);
		}
	}

	[DebugButton("Get Music Box Handle", Tint.Default, PostClickAction.HideButton, 0, new object[] { })]
	private void debugGetMusicBoxHandle()
	{
		game.addItemToInventory(musicBoxSlotHandle.gameObject);
	}

	[DebugButton("Get Jewelry Box Key", Tint.Default, PostClickAction.HideButton, 0, new object[] { })]
	private void debugGetJewelryBoxKey()
	{
		game.addItemToInventory(musicBoxKey.gameObject);
	}

	[DebugButton("Get Exit Key", Tint.Default, PostClickAction.HideButton, 0, new object[] { })]
	private void debugGetExitKey()
	{
		exitKey.targetable = true;
		foreach (Ref<MaterialState, GameObject> exitKeyMaterialState in exitKeyMaterialStates)
		{
			exitKeyMaterialState.Get<MaterialState>(0).setState("Glow", 0f);
			exitKeyMaterialState.Get<GameObject>(0f).layer = LayerMask.NameToLayer("Default");
		}
		darkKeyLensflare.gameObject.SetActive(value: false);
		solvedDarkWorld = true;
		exitKeyButton.gameObject.SetActive(value: false);
		game.addItemToInventory(exitKey.gameObject);
	}

	public virtual void save(FastBinaryWriter writer)
	{
		writer.Write(in musicBoxBaseline, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in musicBoxAddition, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in musicBoxCurrentLevel, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in musicBoxCurrentDial, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in musicBoxBreached, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in musicBoxReset, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in musicBoxOutput, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in musicBoxSolved, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in changeAfterBreach, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in solvedPendantText, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in pendantFloated, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in pendantLightCounter, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in pendantToBright, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in solvedPaintings, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in fireplaceSolved, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in bathroomFourthHandleCurrent, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in bathroomFourthHandleSolved, default(FastBinaryWriter.ForPrimitives));
		writer.WriteArray(pipesVisiblyInSlot, delegate(FastBinaryWriter w, Item e)
		{
			w.WriteComponent(e);
		});
		writer.Write(in pipesLifted, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in pipesSolved, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in bathSolved, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in pendantPlaced, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in isInDarkWorld, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in playersShouldSparkle, default(FastBinaryWriter.ForPrimitives));
		writer.WriteList(playersInDarkWorld, delegate(FastBinaryWriter w, NetPlayerId e)
		{
			w.WriteNetPlayerId(e);
		});
		writer.WriteDictionary(interactiveToTargetable, delegate(FastBinaryWriter w, Interactive k)
		{
			w.WriteComponent(k);
		}, delegate(FastBinaryWriter w, bool v)
		{
			w.Write(in v, default(FastBinaryWriter.ForPrimitives));
		});
		writer.Write(in solvedDarkWorld, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in startingLineWidth, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in darkWorldCountMP, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in darkWorldIsMp, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in shouldToggleDarkLines, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in timer, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in exitKeyFloatOffset, default(FastBinaryWriter.ForPrimitives));
		writer.WriteVector3(in darkWorldPlayerLastPos);
		writer.Write(in darkWorldPlayerMoved, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in solvedBc2Up, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in bc2EdgeIndex, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in ballStopSoundDelay, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in ballPlayedHit, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in isFading, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in isTransitioningToLighting, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in currentPlayerCount, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in starKeyLastMissTime, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in starKeyPicked, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in achievementTimer, default(FastBinaryWriter.ForPrimitives));
		writer.WriteArray(correctDarkPlacement, delegate(FastBinaryWriter w, bool e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteArray(correctDarkPlacementMP, delegate(FastBinaryWriter w, bool e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
	}

	public virtual void load(FastBinaryReader reader)
	{
		musicBoxBaseline = reader.ReadSingle();
		musicBoxAddition = reader.ReadSingle();
		musicBoxCurrentLevel = reader.ReadInt32();
		musicBoxCurrentDial = reader.ReadInt32();
		musicBoxBreached = reader.ReadBoolean();
		musicBoxReset = reader.ReadBoolean();
		musicBoxOutput = reader.ReadSingle();
		musicBoxSolved = reader.ReadBoolean();
		changeAfterBreach = reader.ReadSingle();
		solvedPendantText = reader.ReadBoolean();
		pendantFloated = reader.ReadBoolean();
		pendantLightCounter = reader.ReadSingle();
		pendantToBright = reader.ReadBoolean();
		solvedPaintings = reader.ReadBoolean();
		fireplaceSolved = reader.ReadBoolean();
		bathroomFourthHandleCurrent = reader.ReadInt32();
		bathroomFourthHandleSolved = reader.ReadBoolean();
		pipesVisiblyInSlot = reader.ReadArray((FastBinaryReader r) => r.ReadComponent<Item>());
		pipesLifted = reader.ReadBoolean();
		pipesSolved = reader.ReadBoolean();
		bathSolved = reader.ReadBoolean();
		pendantPlaced = reader.ReadBoolean();
		isInDarkWorld = reader.ReadBoolean();
		playersShouldSparkle = reader.ReadBoolean();
		playersInDarkWorld = reader.ReadList((FastBinaryReader r) => r.ReadNetPlayerId());
		interactiveToTargetable = reader.ReadDictionary((FastBinaryReader kr) => kr.ReadComponent<Interactive>(), (FastBinaryReader vr) => vr.ReadBoolean());
		solvedDarkWorld = reader.ReadBoolean();
		startingLineWidth = reader.ReadSingle();
		darkWorldCountMP = reader.ReadInt32();
		darkWorldIsMp = reader.ReadBoolean();
		shouldToggleDarkLines = reader.ReadBoolean();
		timer = reader.ReadSingle();
		exitKeyFloatOffset = reader.ReadSingle();
		darkWorldPlayerLastPos = reader.ReadVector3();
		darkWorldPlayerMoved = reader.ReadBoolean();
		solvedBc2Up = reader.ReadBoolean();
		bc2EdgeIndex = reader.ReadInt32();
		ballStopSoundDelay = reader.ReadSingle();
		ballPlayedHit = reader.ReadBoolean();
		isFading = reader.ReadBoolean();
		isTransitioningToLighting = reader.ReadBoolean();
		currentPlayerCount = reader.ReadInt32();
		starKeyLastMissTime = reader.ReadSingle();
		starKeyPicked = reader.ReadBoolean();
		achievementTimer = reader.ReadSingle();
		correctDarkPlacement = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		correctDarkPlacementMP = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
	}

	public virtual void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		int position = reader.Position;
		float num = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "musicBoxBaseline",
			fieldValue = $"{num}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num2 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "musicBoxAddition",
			fieldValue = $"{num2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num3 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "musicBoxCurrentLevel",
			fieldValue = $"{num3}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num4 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "musicBoxCurrentDial",
			fieldValue = $"{num4}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "musicBoxBreached",
			fieldValue = $"{flag}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag2 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "musicBoxReset",
			fieldValue = $"{flag2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num5 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "musicBoxOutput",
			fieldValue = $"{num5}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag3 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "musicBoxSolved",
			fieldValue = $"{flag3}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num6 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "changeAfterBreach",
			fieldValue = $"{num6}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag4 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "solvedPendantText",
			fieldValue = $"{flag4}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag5 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "pendantFloated",
			fieldValue = $"{flag5}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num7 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "pendantLightCounter",
			fieldValue = $"{num7}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag6 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "pendantToBright",
			fieldValue = $"{flag6}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag7 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "solvedPaintings",
			fieldValue = $"{flag7}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag8 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "fireplaceSolved",
			fieldValue = $"{flag8}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num8 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "bathroomFourthHandleCurrent",
			fieldValue = $"{num8}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag9 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "bathroomFourthHandleSolved",
			fieldValue = $"{flag9}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Item[] array = reader.ReadArray((FastBinaryReader r) => r.ReadComponent<Item>());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "pipesVisiblyInSlot[" + ((array == null) ? string.Empty : array.Length.ToString()) + "]",
			fieldValue = (((array == null) ? "null" : string.Join(", ", (IEnumerable<Item>)array)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag10 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "pipesLifted",
			fieldValue = $"{flag10}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag11 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "pipesSolved",
			fieldValue = $"{flag11}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag12 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "bathSolved",
			fieldValue = $"{flag12}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag13 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "pendantPlaced",
			fieldValue = $"{flag13}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag14 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "isInDarkWorld",
			fieldValue = $"{flag14}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag15 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "playersShouldSparkle",
			fieldValue = $"{flag15}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<NetPlayerId> list = reader.ReadList((FastBinaryReader r) => r.ReadNetPlayerId());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "playersInDarkWorld[" + ((list == null) ? string.Empty : list.Count.ToString()) + "]",
			fieldValue = (((list == null) ? "null" : string.Join(", ", list)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Dictionary<Interactive, bool> dictionary = reader.ReadDictionary((FastBinaryReader kr) => kr.ReadComponent<Interactive>(), (FastBinaryReader vr) => vr.ReadBoolean());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "interactiveToTargetable[" + ((dictionary == null) ? string.Empty : dictionary.Count.ToString()) + "]",
			fieldValue = (((dictionary == null) ? "null" : string.Join(", ", dictionary)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag16 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "solvedDarkWorld",
			fieldValue = $"{flag16}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num9 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "startingLineWidth",
			fieldValue = $"{num9}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num10 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "darkWorldCountMP",
			fieldValue = $"{num10}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag17 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "darkWorldIsMp",
			fieldValue = $"{flag17}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag18 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "shouldToggleDarkLines",
			fieldValue = $"{flag18}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num11 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "timer",
			fieldValue = $"{num11}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num12 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "exitKeyFloatOffset",
			fieldValue = $"{num12}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Vector3 vector = reader.ReadVector3();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "darkWorldPlayerLastPos",
			fieldValue = $"{vector}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag19 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "darkWorldPlayerMoved",
			fieldValue = $"{flag19}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag20 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "solvedBc2Up",
			fieldValue = $"{flag20}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num13 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "bc2EdgeIndex",
			fieldValue = $"{num13}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num14 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "ballStopSoundDelay",
			fieldValue = $"{num14}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag21 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "ballPlayedHit",
			fieldValue = $"{flag21}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag22 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "isFading",
			fieldValue = $"{flag22}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag23 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "isTransitioningToLighting",
			fieldValue = $"{flag23}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num15 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "currentPlayerCount",
			fieldValue = $"{num15}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num16 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "starKeyLastMissTime",
			fieldValue = $"{num16}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag24 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "starKeyPicked",
			fieldValue = $"{flag24}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num17 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "achievementTimer",
			fieldValue = $"{num17}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool[] array2 = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "correctDarkPlacement[" + ((array2 == null) ? string.Empty : array2.Length.ToString()) + "]",
			fieldValue = (((array2 == null) ? "null" : string.Join(", ", array2)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool[] array3 = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "correctDarkPlacementMP[" + ((array3 == null) ? string.Empty : array3.Length.ToString()) + "]",
			fieldValue = (((array3 == null) ? "null" : string.Join(", ", array3)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
	}

	public override Timer getTimer(byte id)
	{
		return id switch
		{
			0 => new BirdCage1Transition(), 
			1 => new OpenBalconyDoorTimer(), 
			2 => new SolvePipesTimer(), 
			3 => new OpenBathroomDoorTimer(), 
			4 => new SolvePendantTextTimer(), 
			5 => new DisablePendantTextTimer(), 
			6 => new BedButtons1Timer(), 
			7 => new BedButtons2Timer(), 
			8 => new SolveBedChestTimer(), 
			9 => new SolveBC2Timer(), 
			10 => new BC2DoorSoundTimer(), 
			11 => new BC1DoorSoundTimer(), 
			12 => new BC1DoorTimer(), 
			13 => new EnterDarkWorldTimer(), 
			14 => new EnterDarkWorldTransitionTimer(), 
			15 => new ExitDarkWorldTimer(), 
			16 => new ExitDarkWorldTransitionTimer(), 
			17 => new DiveEffectTimer(), 
			18 => new DarkWorldCutSceneTimer(), 
			19 => new EnablePipeEditingTimer(), 
			20 => new PipesTargetableTimer(), 
			21 => new WaterParticles1Timer(), 
			22 => new WaterParticles2Timer(), 
			23 => new WaterParticles3Timer(), 
			24 => new MusicBoxTurnTimer(), 
			25 => new WaterRipplesTimer(), 
			26 => new PendantFloatTimer(), 
			27 => new Locations1Timer(), 
			28 => new Locations2Timer(), 
			29 => new SolvePendantTimer(), 
			30 => new LightingTimer(), 
			31 => new SolvePainting1Timer(), 
			32 => new SolvePainting2Timer(), 
			33 => new ManholesTimer(), 
			34 => new PipeRisingSoundTimer(), 
			35 => new BookcaseLockDelayTimer(), 
			36 => new SolveJewelryBoxTimer(), 
			37 => new BookCaseSound1Timer(), 
			38 => new BookCaseSound2Timer(), 
			39 => new BalconyCutSceneTimer(), 
			40 => new SolveDarkWorldCutSceneTimer(), 
			41 => new FadeTimer(), 
			42 => new BookFireTimer(), 
			_ => null, 
		};
	}
}
