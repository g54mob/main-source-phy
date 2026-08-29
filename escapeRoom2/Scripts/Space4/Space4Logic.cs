using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;

public class Space4Logic : LevelLogic, ISaveable
{
	public sealed class DoorOpenSoundTimer : Timer
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

	public sealed class EndDoorDissolveTimer : Timer
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

	public sealed class PlantSymbolReappearTimer : Timer
	{
		public int plantIndex;

		public int symbolIndex;

		public bool sun;

		public override byte getTypeId()
		{
			return 2;
		}

		public PlantSymbolReappearTimer()
		{
		}

		public PlantSymbolReappearTimer(int plantIndex, int symbolIndex, bool sun)
		{
			this.plantIndex = plantIndex;
			this.symbolIndex = symbolIndex;
			this.sun = sun;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in plantIndex, default(FastBinaryWriter.ForPrimitives));
			writer.Write(in symbolIndex, default(FastBinaryWriter.ForPrimitives));
			writer.Write(in sun, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			plantIndex = reader.ReadInt32();
			symbolIndex = reader.ReadInt32();
			sun = reader.ReadBoolean();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("plantIndex: " + $"{plantIndex}");
			stringBuilder.AppendLine("symbolIndex: " + $"{symbolIndex}");
			stringBuilder.Append("sun: " + $"{sun}");
			return stringBuilder.ToString();
		}
	}

	public sealed class SymbolBlinkTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 3;
		}

		public SymbolBlinkTimer()
		{
		}

		public SymbolBlinkTimer(int index)
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

	public sealed class SymbolEffectTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 4;
		}

		public SymbolEffectTimer()
		{
		}

		public SymbolEffectTimer(int index)
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

	public sealed class LevelEndCutsceneTimer : Timer
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

	public sealed class RuinsSolvedTimer : Timer
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

	public sealed class SpaceSolvedTimer : Timer
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

	public sealed class DominoSolvedTimer : Timer
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

	public sealed class WayfinderSolvedTimer : Timer
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

	public sealed class TracingSolvedTimer : Timer
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

	public sealed class Tablet3ResetTimer : Timer
	{
		public int i;

		public override byte getTypeId()
		{
			return 11;
		}

		public Tablet3ResetTimer()
		{
		}

		public Tablet3ResetTimer(int i)
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

	public sealed class IntroCinematicTimer : Timer
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

	public sealed class WayfinderButtonTimer : Timer
	{
		public bool done;

		public int index;

		public override byte getTypeId()
		{
			return 13;
		}

		public WayfinderButtonTimer()
		{
		}

		public WayfinderButtonTimer(bool done, int index)
		{
			this.done = done;
			this.index = index;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in done, default(FastBinaryWriter.ForPrimitives));
			writer.Write(in index, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			done = reader.ReadBoolean();
			index = reader.ReadInt32();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("done: " + $"{done}");
			stringBuilder.Append("index: " + $"{index}");
			return stringBuilder.ToString();
		}
	}

	public sealed class RoomLightTimer : Timer
	{
		public int phase;

		public override byte getTypeId()
		{
			return 14;
		}

		public RoomLightTimer()
		{
		}

		public RoomLightTimer(int phase)
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

	public sealed class PlantCustomTimer : Timer
	{
		public int phase;

		public override byte getTypeId()
		{
			return 15;
		}

		public PlantCustomTimer()
		{
		}

		public PlantCustomTimer(int phase)
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

	public sealed class PlantAnimationTimer : Timer
	{
		public AnimationSampler sampler;

		public float from;

		public float target = 1f;

		public override byte getTypeId()
		{
			return 16;
		}

		public PlantAnimationTimer()
		{
		}

		public PlantAnimationTimer(AnimationSampler sampler, float from, float target)
		{
			this.sampler = sampler;
			this.from = from;
			this.target = target;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.WriteComponent(sampler);
			writer.Write(in from, default(FastBinaryWriter.ForPrimitives));
			writer.Write(in target, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			sampler = reader.ReadComponent<AnimationSampler>();
			from = reader.ReadSingle();
			target = reader.ReadSingle();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("sampler: " + $"{sampler}");
			stringBuilder.AppendLine("from: " + $"{from}");
			stringBuilder.Append("target: " + $"{target}");
			return stringBuilder.ToString();
		}
	}

	public sealed class PlantAnimationReverseTimer : Timer
	{
		public AnimationSampler sampler;

		public float from;

		public float target = 1f;

		public override byte getTypeId()
		{
			return 17;
		}

		public PlantAnimationReverseTimer()
		{
		}

		public PlantAnimationReverseTimer(AnimationSampler sampler, float from, float target)
		{
			this.sampler = sampler;
			this.from = from;
			this.target = target;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.WriteComponent(sampler);
			writer.Write(in from, default(FastBinaryWriter.ForPrimitives));
			writer.Write(in target, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			sampler = reader.ReadComponent<AnimationSampler>();
			from = reader.ReadSingle();
			target = reader.ReadSingle();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("sampler: " + $"{sampler}");
			stringBuilder.AppendLine("from: " + $"{from}");
			stringBuilder.Append("target: " + $"{target}");
			return stringBuilder.ToString();
		}
	}

	public sealed class PlantSkinnedMeshAnimationTimer : Timer
	{
		public SkinnedMeshRenderer renderer;

		public float from;

		public float target = 1f;

		public override byte getTypeId()
		{
			return 18;
		}

		public PlantSkinnedMeshAnimationTimer()
		{
		}

		public PlantSkinnedMeshAnimationTimer(SkinnedMeshRenderer renderer, float from, float target)
		{
			this.renderer = renderer;
			this.from = from;
			this.target = target;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.WriteComponent(renderer);
			writer.Write(in from, default(FastBinaryWriter.ForPrimitives));
			writer.Write(in target, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			renderer = reader.ReadComponent<SkinnedMeshRenderer>();
			from = reader.ReadSingle();
			target = reader.ReadSingle();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("renderer: " + $"{renderer}");
			stringBuilder.AppendLine("from: " + $"{from}");
			stringBuilder.Append("target: " + $"{target}");
			return stringBuilder.ToString();
		}
	}

	public sealed class PlantSkinnedMeshAnimationReverseTimer : Timer
	{
		public SkinnedMeshRenderer renderer;

		public float from;

		public float target = 1f;

		public override byte getTypeId()
		{
			return 19;
		}

		public PlantSkinnedMeshAnimationReverseTimer()
		{
		}

		public PlantSkinnedMeshAnimationReverseTimer(SkinnedMeshRenderer renderer, float from, float target)
		{
			this.renderer = renderer;
			this.from = from;
			this.target = target;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.WriteComponent(renderer);
			writer.Write(in from, default(FastBinaryWriter.ForPrimitives));
			writer.Write(in target, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			renderer = reader.ReadComponent<SkinnedMeshRenderer>();
			from = reader.ReadSingle();
			target = reader.ReadSingle();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("renderer: " + $"{renderer}");
			stringBuilder.AppendLine("from: " + $"{from}");
			stringBuilder.Append("target: " + $"{target}");
			return stringBuilder.ToString();
		}
	}

	public sealed class MonitorDissolveTimer : Timer
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

	public sealed class LevelFinishTimer : Timer
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

	public sealed class PowerGridFinishTimer : Timer
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

	public sealed class ActivateObjectOnDelayTimer : Timer
	{
		public GameObject obj;

		public override byte getTypeId()
		{
			return 23;
		}

		public ActivateObjectOnDelayTimer()
		{
		}

		public ActivateObjectOnDelayTimer(GameObject obj)
		{
			this.obj = obj;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.WriteGameObject(obj);
		}

		public override void readData(FastBinaryReader reader)
		{
			obj = reader.ReadGameObject();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("obj: " + $"{obj}");
			return stringBuilder.ToString();
		}
	}

	public sealed class DisableObjectOnDelayTimer : Timer
	{
		public GameObject obj;

		public override byte getTypeId()
		{
			return 24;
		}

		public DisableObjectOnDelayTimer()
		{
		}

		public DisableObjectOnDelayTimer(GameObject obj)
		{
			this.obj = obj;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.WriteGameObject(obj);
		}

		public override void readData(FastBinaryReader reader)
		{
			obj = reader.ReadGameObject();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("obj: " + $"{obj}");
			return stringBuilder.ToString();
		}
	}

	public sealed class EndCutsceneEmissionTimer : Timer
	{
		public MaterialState state;

		public override byte getTypeId()
		{
			return 25;
		}

		public EndCutsceneEmissionTimer()
		{
		}

		public EndCutsceneEmissionTimer(MaterialState state)
		{
			this.state = state;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.WriteComponent(state);
		}

		public override void readData(FastBinaryReader reader)
		{
			state = reader.ReadComponent<MaterialState>();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("state: " + $"{state}");
			return stringBuilder.ToString();
		}
	}

	private enum LevelPredicate
	{
		Plant0Solved0 = 0,
		Plant0Solved1 = 1,
		Plant1Solved = 2,
		Plant2Solved0 = 3,
		Plant2Solved1 = 4,
		Plant3Solved = 5,
		Tablet1 = 6,
		Tablet2 = 7,
		Tablet3 = 8,
		Tablets = 9,
		BioTracing = 10,
		Domino = 11,
		Wayfinder = 12,
		Space = 13,
		Projection = 14,
		Partition = 15,
		ExitSymbols = 16
	}

	public enum EndSphereState
	{
		InitialRotation = 0,
		EndingInitialRotation = 1,
		SecondSphereInitialRotation = 2,
		DissolveDoor = 3,
		FirstArm = 4,
		RetractArm = 5,
		UndisolveDoor = 6,
		SpeedUp = 7,
		TopSpeed = 8
	}

	private enum Puzzle
	{
		[PuzzleInfo("%PuzzleS4_1%", false)]
		TriangleTerminal = 0,
		[PuzzleInfo("%PuzzleS4_2%", false)]
		FracturedTelescope = 1,
		[PuzzleInfo("%PuzzleS4_3%", false)]
		Perspective = 2,
		[PuzzleInfo("%PuzzleS4_4%", false)]
		StarMap = 3,
		[PuzzleInfo("%PuzzleS4_5%", false)]
		Astrobotany = 4,
		[PuzzleInfo("%PuzzleS4_6%", false)]
		Industry = 5,
		[PuzzleInfo("%PuzzleS4_7%", false)]
		Query = 6,
		[PuzzleInfo("%PuzzleS4_8%", false)]
		StarTablet = 7,
		[PuzzleInfo("%PuzzleS4_9%", false)]
		Faraway = 8
	}

	private enum TriangleTerminalHint
	{
		TTSee = 0,
		TTFirstOne = 1,
		TTSecondOne = 2
	}

	private enum FracturedTelescopeHint
	{
		PickupBall = 0,
		TerminalSee = 1,
		TerminalButtons = 2,
		TerminalNumbers = 3,
		TerminalFirstOne = 4
	}

	private enum StarMapHint
	{
		MapSee = 0,
		MapInteract = 1,
		MapMatch = 2
	}

	private enum AstrobotanyHint
	{
		PickupDiscs = 0,
		PlantsMove = 1,
		PlantsSymbols = 2,
		DiscOrder = 3
	}

	private enum PerspectiveHint
	{
		RuinsSee = 0,
		RuinsMove = 1,
		RuinsProject = 2,
		FirstOne = 3
	}

	private enum StarTabletHint
	{
		TabletSwitches = 0,
		TabletWeb = 1,
		TabletDials = 2,
		PlaceSwitches = 3,
		SolveSwitches = 4,
		PlaceWeb = 5,
		SolveWeb = 6,
		PlaceDials = 7,
		SolveDials = 8
	}

	private enum IndustryHint
	{
		PickupOpener = 0,
		PlaceOpener = 1,
		InteractWith = 2,
		Place = 3,
		NeedToClearAll = 4,
		FirstOne = 5
	}

	private enum QueryHint
	{
		PickupOpener = 0,
		PlaceOpener = 1,
		InteractWith = 2,
		MoveSwitch = 3,
		FirstOne = 4,
		SecondOne = 5
	}

	private enum FarawayHint
	{
		GetCore = 0,
		InsertCore = 1,
		GetMirror = 2,
		ConnectSwitches = 3,
		Pathway = 4,
		FirstOne = 5
	}

	[DontSave]
	private bool[] dominoSolution = new bool[6] { true, true, true, false, false, false };

	[DontSave]
	public Switch3D[] powerSwitches;

	[DontSave]
	public MaterialState[] powerStates_1;

	[DontSave]
	public MaterialState[] powerStates_2;

	[DontSave]
	public MaterialState[] powerStates_3;

	[DontSave]
	public MaterialState[] powerStates_4;

	[DontSave]
	public MaterialState[] powerStates_5;

	private List<MaterialState[]> powerStates;

	[DontSave]
	private List<int> powerSwitchesUpsideDown = new List<int> { 1, 6, 18, 22, 40, 42, 45, 58 };

	private int[,] powerGrid = new int[10, 10];

	[DontSave]
	private List<int[,]> powerRequirements = new List<int[,]>
	{
		new int[4, 5]
		{
			{ 0, 0, 0, 0, 1 },
			{ 0, 2, 2, 0, 0 },
			{ 0, 3, 2, 3, 0 },
			{ 0, 0, 0, 0, 0 }
		},
		new int[5, 5]
		{
			{ 1, 0, 0, 0, 1 },
			{ 0, 0, 3, 0, 0 },
			{ 0, 2, 2, 2, 0 },
			{ 0, 0, 3, 0, 0 },
			{ 1, 0, 0, 0, 1 }
		},
		new int[5, 5]
		{
			{ 0, 0, 0, 0, 1 },
			{ 0, 3, 2, 0, 0 },
			{ 0, 0, 2, 2, 0 },
			{ 1, 0, 3, 0, 0 },
			{ 1, 0, 0, 0, 1 }
		},
		new int[3, 5]
		{
			{ 0, 0, 0, 0, 0 },
			{ 0, 2, 3, 2, 0 },
			{ 0, 0, 0, 0, 0 }
		},
		new int[4, 4]
		{
			{ 0, 0, 0, 0 },
			{ 0, 2, 2, 0 },
			{ 0, 3, 2, 0 },
			{ 0, 0, 0, 0 }
		}
	};

	private bool pattern0Found;

	private bool pattern1Found;

	[DontSave]
	public MaterialState[] plantFloorGlows;

	[DontSave]
	public GameObject levelEndCutscene;

	[DontSave]
	public GameObject coreOpenCutscene;

	[DontSave]
	public GameObject buttonsOpenCutscene;

	[DontSave]
	public AnimationSampler endDoorSampler;

	[DontSave]
	public MaterialState[] endDoorSymbolStates;

	[DontSave]
	public MaterialState[] endDoorDissolves;

	[DontSave]
	public GameObject coreSlotGraphic;

	[DontSave]
	public GameObject endFlare;

	[DontSave]
	public GameObject[] endRays;

	[DontSave]
	public GameObject[] endEffects;

	[DontSave]
	public GameObject[] doorRays;

	[DontSave]
	public GameObject[] doorEffects;

	public HDAdditionalLightData firstPuzzleVolume;

	public Light firstPuzzleLight;

	public Light firstPuzzlePointLight;

	[DontSave]
	public GameObject holoball;

	[DontSave]
	public GameObject waterVapour;

	public const float LIGHT_START_INTENSITY = 5000f;

	public const float LIGHT_START_VOLUME_MULTIPLIER = 0.11f;

	public const float LIGHT_POINT_TARGET_INTENSITY = 600f;

	[DontSave]
	public GameObject[] objectsToSetActiveOnStart;

	[DontSave]
	public MaterialState waterDrawingMaterialState;

	[DontSave]
	public Ref<GameObject, MaterialState> spaceDissolveMaterialState;

	[DontSave]
	public GameObject armAnimationAudio;

	[DontSave]
	public Transform alienCameraTransform;

	[DontSave]
	public Volume ruinsVolume;

	[DontSave]
	public Image fadeout;

	[DontSave]
	public AnimationSampler ruinsPuzzleArmingAnimation;

	[DontSave]
	public AnimationSampler ruinsPuzzleHandleAnimation;

	[DontSave]
	public TweenState ruinsPuzzleRiseUpTweenState;

	public const float LIGHT_END_INTENSITY = 1215.307f;

	[DontSave]
	public Item[] tablets;

	[DontSave]
	public Slot[] tabletSlots;

	[DontSave]
	public Zoomable ruinsZoomable;

	[DontSave]
	public TweenState ruinsRotateOnSolveTweenState;

	[DontSave]
	public Turnable ruinsHandle;

	public Zoomable partitionZoomable;

	private bool partitionSolved;

	private bool partitionFlash;

	[DontSave]
	public Switch3D[] dominoSwitches;

	[DontSave]
	public TweenState[] dominoLockTSs;

	[DontSave]
	public MaterialState[] dominoSelectorMatStates;

	[DontSave]
	public LineRenderer dominoLaser;

	[DontSave]
	public GameObject dominoLaserHit;

	[DontSave]
	public Transform[] dominoLaserTargets;

	private const int dominoCount = 6;

	private const int dominoSegments = 9;

	public bool[] dominoLockStates = new bool[6];

	public bool[] dominoActiveSegments = new bool[9];

	[DontSave]
	private readonly bool[,] dominoCombinations = new bool[6, 9]
	{
		{ false, true, true, true, true, false, true, true, true },
		{ true, true, false, true, true, false, true, false, true },
		{ true, false, true, true, true, true, true, false, true },
		{ false, true, true, true, true, true, true, true, false },
		{ true, true, true, true, false, true, false, true, true },
		{ true, false, false, false, true, true, true, true, true }
	};

	[DontSave]
	public Zoomable tracingZoomable;

	[DontSave]
	public Switch3D[] bioButtons;

	[DontSave]
	public MaterialState[] bioButtonsMS;

	[DontSave]
	public Switch3D[] bioPlatesButtons;

	[DontSave]
	public MaterialState[] bioPlatesMS;

	[DontSave]
	public GameObject[] bioPlatesCircle;

	[DontSave]
	public MaterialState[] bioPillarsMS;

	[DontSave]
	private readonly int[] bioPillarIndices = new int[14]
	{
		2, 3, 15, 16, 20, 29, 34, 35, 39, 43,
		45, 51, 63, 65
	};

	private int[] bioButtonCurrent = new int[3] { -1, -1, -1 };

	private int bioButtonSelected = -1;

	[DontSave]
	public GameObject[] bioLaserRoot;

	[DontSave]
	public Transform[] bioLaserRootTransform;

	[DontSave]
	public LineRenderer[] bioLaser0LR;

	[DontSave]
	public Transform[] bioLaser0Tran;

	[DontSave]
	public LineRenderer[] bioLaser1LR;

	[DontSave]
	public Transform[] bioLaser1Tran;

	private Vector3 plantDistanceRef;

	private Vector3 plantAngleBaseVector;

	private float[] plantAngleRefs;

	private const int plantSunMax = 5;

	private const int plantWaterMax = 5;

	public readonly int plantCount = 4;

	public int[] plantSunValues = new int[5];

	public int[] plantWaterValues = new int[5];

	[DontSave]
	private readonly float[] plantSunDistances = new float[5] { 3.9f, 4.9f, 5.9f, 6.9f, 100f };

	[DontSave]
	private readonly float[] plantAngles = new float[5] { 69.8f, 83.2f, 96.5f, 109.6f, 270f };

	public int[] plantStates = new int[4];

	[Header("Plant Puzzle")]
	[DontSave]
	public GameObject[] plants;

	[DontSave]
	public GameObject[] plantValueHiders;

	[DontSave]
	public GameObject plantDistanceRefObject;

	[DontSave]
	public GameObject[] plantAngleRefObjects;

	[DontSave]
	public GameObject[] plantSuns_1;

	[DontSave]
	public GameObject[] plantWaters_1;

	[DontSave]
	public GameObject[] plantSuns_2;

	[DontSave]
	public GameObject[] plantWaters_2;

	[DontSave]
	public GameObject[] plantSuns_3;

	[DontSave]
	public GameObject[] plantWaters_3;

	[DontSave]
	public GameObject[] plantSuns_4;

	[DontSave]
	public GameObject[] plantWaters_4;

	[DontSave]
	public Item plantReward;

	[DontSave]
	public RefArray<Item, MaterialState> plantItems;

	[DontSave]
	public Slot[] plantSlots;

	[DontSave]
	public AnimationSampler[] plantAnimations;

	[DontSave]
	public SkinnedMeshRenderer[] plantSkinnedMeshAnimations;

	private List<GameObject[]> plantSuns = new List<GameObject[]>();

	private List<GameObject[]> plantWaters = new List<GameObject[]>();

	[DontSave]
	public TweenState plantHintDispenser;

	[DontSave]
	public Item[] plantPlates;

	private int plantHintDispenserState;

	[Header("Wayfinder Puzzle")]
	private const int wayfinderRuneCount = 4;

	[DontSave]
	public Zoomable wayfinderZoomable;

	[DontSave]
	public Mesh[] wayfinderRuneMeshes;

	public MeshFilter[] wayfinderRuneFilters;

	[DontSave]
	public Switch3D[] wayfinderRuneLeftButtons;

	[DontSave]
	public Switch3D[] wayfinderRuneRightButtons;

	private int[] wayfinderRuneStates = new int[4];

	[DontSave]
	private readonly int[] wayfinderRuneSolution = new int[4] { 0, 1, 2, 3 };

	[DontSave]
	private bool wayfinderCubeZooming;

	private HashSet<GameObject> correctWayfinderSymbols = new HashSet<GameObject>();

	[Header("Tablet 1")]
	[DontSave]
	public GameObject[] tabletPickupParents;

	private bool tablet1Solved;

	[DontSave]
	public Dial[] tablet1Dials;

	private const int tablet1CorrectThreshold = 3;

	[Header("Tablet 2")]
	[DontSave]
	public GameObject[] tablet2Icospheres;

	[DontSave]
	public Switch3D[] tablet2Switches;

	private bool[] tablet2States = new bool[19];

	private bool[] tablet2Solution = new bool[19];

	private bool tablet2Solved;

	[DontSave]
	public RefArray<MeshRenderer, MaterialState> tablet2Lines;

	[DontSave]
	private (int, int)[] tablet2LineMap = new(int, int)[42]
	{
		(8, 9),
		(5, 6),
		(4, 5),
		(3, 4),
		(1, 4),
		(0, 4),
		(0, 3),
		(0, 1),
		(1, 2),
		(1, 5),
		(2, 5),
		(2, 6),
		(6, 10),
		(6, 11),
		(5, 10),
		(5, 9),
		(4, 9),
		(4, 8),
		(3, 8),
		(3, 7),
		(7, 8),
		(8, 12),
		(7, 12),
		(8, 13),
		(9, 13),
		(9, 14),
		(9, 10),
		(10, 11),
		(11, 15),
		(10, 15),
		(10, 14),
		(14, 15),
		(15, 18),
		(14, 18),
		(17, 18),
		(14, 17),
		(13, 14),
		(12, 13),
		(13, 17),
		(13, 16),
		(12, 16),
		(16, 17)
	};

	[Header("Tablet 3")]
	private int symbolsUsed;

	[DontSave]
	public Switch3D[] tablet3Switches;

	[DontSave]
	public RefArray<GameObject, Transform> tablet3Symbols;

	[DontSave]
	public GameObject tablet3StartSymbol;

	[DontSave]
	public GameObject[] tablet3Counters;

	private List<GameObject> tablet3NextSymbols = new List<GameObject>();

	private List<GameObject> tablet3GeneratedSymbols = new List<GameObject>();

	private List<int> tablet3Sequence = new List<int>();

	private bool tablet3Solved;

	private List<int> tablet3SequenceCorrect = new List<int> { 4, 3, 4 };

	[DontSave]
	public GameObject[] projectionRuinPieces;

	private int[] projectionRotations;

	private HashSet<GameObject> projectionCylinders = new HashSet<GameObject>();

	private HashSet<GameObject> projectionPillars = new HashSet<GameObject>();

	private HashSet<GameObject> projectionPyramids = new HashSet<GameObject>();

	[DontSave]
	public MaterialState[] projectionZoomsMs_1;

	[DontSave]
	public MaterialState[] projectionZoomsMs_2;

	[DontSave]
	public MaterialState[] projectionZoomsMs_3;

	[DontSave]
	public MaterialState[] projectionZoomsMs_4;

	private int[] projectionZoom_1Nodes = new int[3] { 6, 3, 0 };

	private int[] projectionZoom_2Nodes = new int[3] { 4, 3, 5 };

	private int[] projectionZoom_3Nodes = new int[3] { 8, 5, 2 };

	private int[] projectionZoom_4Nodes = new int[3] { 2, 1, 0 };

	[DontSave]
	public SlidableGraph projectionSlidableGraph;

	[DontSave]
	public Ref<Item, TweenState> ruinsPuzzleReward;

	[DontSave]
	private Dictionary<int, (HashSet<GameObject>, int[])> projectionSolution = new Dictionary<int, (HashSet<GameObject>, int[])>();

	[DontSave]
	public Switch3D[] symbolSwitches;

	[DontSave]
	public GameObject[] symbolEffects;

	private bool[] symbolStates;

	private List<HashSet<int>> symbolSolutions;

	private int symbolsHighlighted;

	private bool symbolsDone;

	[DontSave]
	public Zoomable spaceZoomable;

	[DontSave]
	public Swapper spaceSwapper;

	[DontSave]
	public GameObject[] spaceJigsawPieces;

	[DontSave]
	public SwapperPiece[] spaceJigsawSwapperPiece;

	[DontSave]
	public MaterialState[] spacePieceMSs;

	[DontSave]
	public GameObject[] spacePieceSelectors;

	private GameObject spaceSelectedPiece;

	[DontSave]
	private MaterialState spaceHoveringMS;

	[DontSave]
	public Ref<GameObject, Transform> spaceTargetRenderer;

	[DontSave]
	public Joystick spaceJoystick;

	[DontSave]
	public GameObject[] spaceJigsawSolution;

	private Vector3 spaceTargetRendererPosition;

	private int[] spaceOccupiedSpaces = new int[9] { 3, 6, 5, 7, 0, 8, 4, 1, 2 };

	[DontSave]
	public GameObject alienWorldContainer;

	public EndSphereState endSphereState;

	private EndSphereState endSphereStateLastFrame;

	private float sphereChangeTime;

	private float plantTimer;

	private float currentLightIntensity = 4f;

	private bool plantLastFrameWasInteracting;

	private int plantLastInteractedIndex = -1;

	private float starKeyLastMissTime;

	private bool starKeyPicked;

	[DontSave]
	public GameObject sphereSlowDown;

	private bool levelEnded;

	private bool ruinsArmed;

	private HashSet<Item> pickedUpPlantHints = new HashSet<Item>();

	[Header("Generated Variables")]
	[DontSave]
	public GameObject[] puzzlesNeoAudio;

	[DontSave]
	public GameObject endPlane;

	[DontSave]
	public Switch3D endPlaneSwitch;

	[DontSave]
	public GameObject[] ExitGateSymbols;

	[DontSave]
	public Slot coreSlot;

	[DontSave]
	public Slot dominoTopSlot;

	[DontSave]
	public TweenState monitorClose;

	[DontSave]
	public GameObject monitorKey;

	[DontSave]
	public MaterialState monitorOpenDissolve;

	[DontSave]
	public TweenState DominosAnim;

	[DontSave]
	public TweenState monitorSphereOpen;

	[DontSave]
	public TweenState DominoAnim;

	[DontSave]
	public Ref<ParticleSystem, GameObject> dominoParticles;

	[DontSave]
	public Item AlienMirror_p;

	[DontSave]
	public MaterialState Key_lowAnim;

	[DontSave]
	public GameObject EmitterPlanes_p;

	[DontSave]
	public TweenState MirrorPillarAnimation;

	[DontSave]
	public TweenState SphereDrop;

	[DontSave]
	public Animation sphereAnimationTopSpeed;

	[DontSave]
	public Animation sphereAnimationSpeedup;

	[DontSave]
	public Animation sphereArmAnimation;

	[DontSave]
	public MaterialState sphereDisolveDoor;

	[DontSave]
	public Animation sphereAnimation2;

	[DontSave]
	public Animation sphereAnimation1;

	[DontSave]
	public Turnable RuinsRotator;

	[DontSave]
	public TweenState bioComputerAnimation;

	[DontSave]
	public ParticleSystem bioComputerParticles;

	[DontSave]
	public Slot SlotTriangle;

	[DontSave]
	public GameObject[] centralSpheres;

	[DontSave]
	public TweenState buttonsTabletUp;

	[DontSave]
	public Zoomable symbolsPillarZoom;

	[DontSave]
	public Zoomable DominoZoom;

	[DontSave]
	public Interactive[] interactablesToActivateOnPower;

	[DontSave]
	public GameObject[] SelectorHighlights;

	[DontSave]
	public Light partitionLight;

	[DontSave]
	public TweenState[] SymbolsPillarPuzzleAnims;

	[DontSave]
	public RefArray<ParticleSystem, GameObject> emitPuzzleTops;

	[DontSave]
	public Light mainLight;

	[DontSave]
	public TweenState[] puzzlesNeoAnimations;

	[DontSave]
	public GameObject cinematicIntro;

	[DontSave]
	public TweenState powerPuzzleAnimation;

	[DontSave]
	public ParticleSystem powerPuzzleParticles;

	public override void onInitPredicates()
	{
		game.registerPredicate(LevelPredicate.Plant0Solved0, () => checkPlant(0, 0));
		game.registerPredicate(LevelPredicate.Plant0Solved1, () => checkPlant(0, 1));
		game.registerPredicate(LevelPredicate.Plant1Solved, () => checkPlant(1, 0));
		game.registerPredicate(LevelPredicate.Plant2Solved0, () => checkPlant(2, 0));
		game.registerPredicate(LevelPredicate.Plant2Solved1, () => checkPlant(2, 1));
		game.registerPredicate(LevelPredicate.Plant3Solved, () => checkPlant(3, 0));
		game.registerPredicate(LevelPredicate.Tablet1, () => checkTablet1());
		game.registerPredicate(LevelPredicate.Tablet2, () => checkTablet2());
		game.registerPredicate(LevelPredicate.Tablet3, () => checkTablet3());
		game.registerPredicate(LevelPredicate.Tablets, () => checkTablets());
		game.registerPredicate(LevelPredicate.BioTracing, () => checkBioTracing());
		game.registerPredicate(LevelPredicate.Domino, () => checkDomino());
		game.registerPredicate(LevelPredicate.Wayfinder, () => checkWayfinder());
		game.registerPredicate(LevelPredicate.Space, () => checkSpace());
		game.registerPredicate(LevelPredicate.Projection, () => checkProjection());
		game.registerPredicate(LevelPredicate.Partition, () => checkPartition());
		game.registerPredicate(LevelPredicate.ExitSymbols, () => checkExitSymbols());
	}

	public override void onLevelPredicateDone(int type, int doneCounter)
	{
		switch (type)
		{
		case 0:
			solvePlant(0, 0);
			break;
		case 1:
			solvePlant(0, 1);
			break;
		case 2:
			solvePlant(1, 0);
			break;
		case 3:
			solvePlant(2, 0);
			break;
		case 4:
			solvePlant(2, 1);
			break;
		case 5:
			solvePlant(3, 0);
			break;
		case 6:
			solveTablet1();
			break;
		case 7:
			solveTablet2();
			break;
		case 8:
			solveTablet3();
			break;
		case 9:
			solveTablets();
			break;
		case 10:
			solveBioTracing();
			break;
		case 11:
			solveDomino();
			break;
		case 12:
			solveWayfinder();
			break;
		case 13:
			solveSpace();
			break;
		case 14:
			solveProjection();
			break;
		case 15:
			solvePartition();
			break;
		case 16:
			solveExitSymbols();
			break;
		}
	}

	private bool checkExitSymbols()
	{
		return ExitGateSymbols.All((GameObject x) => x.activeInHierarchy);
	}

	[DebugButton("Finish level sequence", Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void solveExitSymbols()
	{
		symbolsDone = true;
		Array.ForEach(symbolSwitches, delegate(Switch3D x)
		{
			x.targetable = false;
		});
		levelEnded = true;
		sphereSlowDown.SetActive(value: false);
		centralSpheres[3].SetActive(value: true);
		game.startTimer(new LevelEndCutsceneTimer(), 2f);
		buttonsTabletUp.transitionToDuration("Default", 1f, 1f, playSound: true, 1f);
		MirrorPillarAnimation.transitionToDuration("Default", 1f, 1f, playSound: true, 1f);
		game.startTimer(new ActivateObjectOnDelayTimer(endFlare), 4f);
		game.startTimer(new EndDoorDissolveTimer(), 10.5f);
		endDoorSampler.play(1f, 12f);
		game.startTimer(new DoorOpenSoundTimer(), 12f);
		for (int num = 0; num < endRays.Length; num++)
		{
			game.startTimer(new ActivateObjectOnDelayTimer(endRays[num]), (float)num * 0.5f + 4f);
			game.startTimer(new ActivateObjectOnDelayTimer(endEffects[num]), (float)num * 0.5f + 4f);
		}
		for (int num2 = 0; num2 < doorEffects.Length; num2++)
		{
			game.startTimer(new EndCutsceneEmissionTimer(endDoorSymbolStates[num2]), (float)num2 * 0.66f + 8f);
			game.startTimer(new ActivateObjectOnDelayTimer(doorEffects[num2]), (float)num2 * 0.66f + 8f);
			game.startTimer(new ActivateObjectOnDelayTimer(doorRays[num2]), (float)num2 * 0.5f + 8f);
		}
		game.startTimer(new LevelFinishTimer(), 16f);
	}

	private bool checkPartition()
	{
		int num = 0;
		for (int i = 0; i < powerRequirements.Count; i++)
		{
			if (findPattern(i))
			{
				num++;
			}
		}
		return num == 5;
	}

	[DebugButton("Solve Partition Puzzle", Tint.Blue, PostClickAction.ReturnToGame | PostClickAction.HideButton, 0, new object[] { }, priority = 1)]
	private void solvePartition()
	{
		partitionSolved = true;
		partitionZoomable.targetable = false;
		Array.ForEach(powerSwitches, delegate(Switch3D x)
		{
			x.targetable = false;
		});
		game.startTimer(new PowerGridFinishTimer(), 1f);
	}

	private bool checkProjection()
	{
		foreach (SlidableGraphPiece pieces in projectionSlidableGraph.piecesList)
		{
			if (game.isAnyPlayerInteracting(pieces.gameObject))
			{
				return false;
			}
		}
		bool result = true;
		for (int i = 0; i < projectionSlidableGraph.nodes.Count; i++)
		{
			if (projectionSlidableGraph.tryGetPiece(projectionSlidableGraph.nodes[i], out var piece) && piece.gameObject != null)
			{
				int num = Array.IndexOf(projectionRuinPieces, piece.gameObject);
				if (projectionSolution[i].Item1 == null || !projectionSolution[i].Item1.Contains(piece.gameObject) || Array.IndexOf(projectionSolution[i].Item2, projectionRotations[num]) == -1)
				{
					result = false;
					break;
				}
			}
			else if (projectionSolution[i].Item1 != null)
			{
				result = false;
				break;
			}
		}
		return result;
	}

	private void solveProjection()
	{
		foreach (SlidableGraphPiece pieces in projectionSlidableGraph.piecesList)
		{
			pieces.targetable = false;
		}
		game.startTimer(new RuinsSolvedTimer(), 1f);
	}

	private bool checkSpace()
	{
		bool result = true;
		for (int i = 0; i < spaceJigsawPieces.Length; i++)
		{
			if (Vector3.Distance(spaceJigsawPieces[i].transform.position, spaceJigsawSolution[i].transform.position) > 0.1f)
			{
				result = false;
			}
		}
		return result;
	}

	private void solveSpace()
	{
		SwapperPiece[] array = spaceJigsawSwapperPiece;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = false;
		}
		game.startTimer(new SpaceSolvedTimer(), 1f);
	}

	private bool checkWayfinder()
	{
		bool flag = true;
		for (int i = 0; i < wayfinderRuneStates.Length; i++)
		{
			flag &= wayfinderRuneStates[i] == wayfinderRuneSolution[i];
		}
		return flag;
	}

	private void solveWayfinder()
	{
		Switch3D[] array = wayfinderRuneRightButtons;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = false;
		}
		game.startTimer(new WayfinderSolvedTimer(), 1f);
	}

	private bool checkDomino()
	{
		bool flag = true;
		for (int i = 0; i < 6; i++)
		{
			flag &= dominoSolution[i] == dominoLockStates[i];
		}
		return flag;
	}

	private void solveDomino()
	{
		Switch3D[] array = dominoSwitches;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = false;
		}
		game.startTimer(new DominoSolvedTimer(), 1f);
	}

	private bool checkBioTracing()
	{
		bool result = true;
		MaterialState[] array = bioPillarsMS;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].getTargetWeight("Dissolve") < 1f)
			{
				result = false;
			}
		}
		return result;
	}

	private void solveBioTracing()
	{
		game.startTimer(new TracingSolvedTimer(), 1f);
		Switch3D[] array = bioButtons;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = false;
		}
		array = bioPlatesButtons;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = false;
		}
		GameObject[] array2 = bioLaserRoot;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].SetActive(value: false);
		}
	}

	private bool checkTablets()
	{
		if ((tablet1Solved && tablet2Solved && tablet3Solved) & SymbolsPillarPuzzleAnims.All((TweenState x) => x.getWeight("OpenPillar") < 0.01f))
		{
			return endSphereState == EndSphereState.InitialRotation;
		}
		return false;
	}

	private void solveTablets()
	{
		endSphereState = EndSphereState.EndingInitialRotation;
		game.finishPuzzle(Puzzle.StarTablet);
	}

	private bool checkTablet3()
	{
		bool result = tablet3Sequence.Count == 3;
		for (int i = 0; i < tablet3Sequence.Count; i++)
		{
			if (tablet3Sequence[i] != tablet3SequenceCorrect[i])
			{
				result = false;
			}
		}
		return result;
	}

	private void solveTablet3()
	{
		tablet3Solved = true;
		Switch3D[] array = tablet3Switches;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = false;
		}
		endTablet3Pillar();
	}

	private bool checkTablet2()
	{
		bool flag = true;
		for (int i = 0; i < tablet2States.Length; i++)
		{
			flag &= tablet2Solution[i] == tablet2States[i];
		}
		return flag;
	}

	private void solveTablet2()
	{
		tablet2Solved = true;
		Switch3D[] array = tablet2Switches;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = false;
		}
		endTablet2Pillar();
	}

	private bool checkTablet1()
	{
		Dial[] array = tablet1Dials;
		foreach (Dial dial in array)
		{
			if (game.isAnyPlayerInteracting(dial.gameObject))
			{
				return false;
			}
		}
		bool result = true;
		if (tablet1Dials[0].value >= 69 || tablet1Dials[0].value <= 63)
		{
			result = false;
		}
		if ((tablet1Dials[1].value >= 43 || tablet1Dials[1].value <= 37) && (tablet1Dials[1].value >= 93 || tablet1Dials[1].value <= 87))
		{
			result = false;
		}
		if (tablet1Dials[2].value >= 66 || tablet1Dials[2].value <= 60)
		{
			result = false;
		}
		return result;
	}

	private void solveTablet1()
	{
		tablet1Solved = true;
		Dial[] array = tablet1Dials;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = false;
		}
		endTablet1Pillar();
	}

	private bool checkPlant(int plantIndex, int plantStep)
	{
		if (plantTimer > 0f)
		{
			return false;
		}
		switch (plantIndex)
		{
		case 0:
			if (plantStep == 0)
			{
				if (plantStates[plantIndex] == 0 && plantSunValues[plantIndex] == 1)
				{
					return plantWaterValues[plantIndex] == 3;
				}
				return false;
			}
			if (plantStates[plantIndex] == 2 && plantSunValues[plantIndex] == 5)
			{
				return plantWaterValues[plantIndex] == 4;
			}
			return false;
		case 1:
			if (plantStates[plantIndex] == 0 && plantSunValues[plantIndex] == 3)
			{
				return plantWaterValues[plantIndex] == 5;
			}
			return false;
		case 2:
			if (plantStep == 0)
			{
				if (plantStates[plantIndex] == 0 && plantSunValues[plantIndex] == 1)
				{
					return plantWaterValues[plantIndex] == 4;
				}
				return false;
			}
			if (plantStates[plantIndex] == 2 && plantSunValues[plantIndex] == 4)
			{
				return plantWaterValues[plantIndex] == 1;
			}
			return false;
		case 3:
			if (plantStates[plantIndex] == 0 && plantSunValues[plantIndex] == 1)
			{
				return plantWaterValues[plantIndex] == 1;
			}
			return false;
		default:
			return false;
		}
	}

	private void solvePlant(int plantIndex, int plantStep)
	{
		switch (plantIndex)
		{
		case 0:
			if (plantStep == 0)
			{
				plantStates[plantIndex] = 1;
				game.startTimer(new PlantAnimationTimer(plantAnimations[0], 0f, 0.3f), 1f);
				game.startTimer(new PlantSkinnedMeshAnimationTimer(plantSkinnedMeshAnimations[1], 0f, 1f), 1f);
				PineFmod.playOneShotSoundAttached("event:/Sound Effects/03 Interactable/Doors/Alien_Door_Open/Alien_Door_End", plants[plantIndex]);
				plantSlots[0].targetable = true;
			}
			else
			{
				plantStates[plantIndex] = 3;
				game.startTimer(new PlantAnimationTimer(plantAnimations[0], 0.57f, 1f), 1f);
				PineFmod.playOneShotSoundAttached("event:/Sound Effects/03 Interactable/Doors/Alien_Door_Open/Alien_Door_End", plants[plantIndex]);
				plantItems.Get<Item>(3, 0).targetable = true;
			}
			break;
		case 1:
			plantStates[plantIndex] = 1;
			game.startTimer(new PlantAnimationTimer(plantAnimations[1], 0f, 1f), 1f);
			game.startTimer(new PlantSkinnedMeshAnimationTimer(plantSkinnedMeshAnimations[2], 0f, 1f), 1f);
			PineFmod.playOneShotSoundAttached("event:/Sound Effects/03 Interactable/Doors/Alien_Door_Open/Alien_Door_End", plants[plantIndex]);
			plantItems.Get<Item>(0, 0).targetable = true;
			break;
		case 2:
			if (plantStep == 0)
			{
				plantStates[plantIndex] = 1;
				game.startTimer(new PlantAnimationTimer(plantAnimations[2], 0f, 0.25f), 1f);
				game.startTimer(new PlantSkinnedMeshAnimationTimer(plantSkinnedMeshAnimations[4], 0f, 1f), 0.75f, 0.25f);
				PineFmod.playOneShotSoundAttached("event:/Sound Effects/03 Interactable/Doors/Alien_Door_Open/Alien_Door_End", plants[plantIndex]);
				plantSlots[1].targetable = true;
			}
			else
			{
				plantStates[plantIndex] = 3;
				game.startTimer(new PlantAnimationTimer(plantAnimations[2], 0.316f, 1f), 1f);
				plantItems.Get<Item>(1, 0).gameObject.SetActive(value: true);
				plantItems.Get<MaterialState>(1, 0f).setState("Dissolve");
				plantItems.Get<MaterialState>(1, 0f).transitionToDuration("Default", 0.5f);
				PineFmod.playOneShotSoundAttached("event:/Sound Effects/03 Interactable/Doors/Alien_Door_Open/Alien_Door_End", plants[plantIndex]);
				plantItems.Get<Item>(1, 0).targetable = true;
			}
			break;
		case 3:
			plantStates[plantIndex] = 1;
			game.startTimer(new PlantAnimationTimer(plantAnimations[3], 0f, 0.5f), 1f);
			game.startTimer(new PlantSkinnedMeshAnimationTimer(plantSkinnedMeshAnimations[5], 0f, 1f), 0.75f, 0.25f);
			PineFmod.playOneShotSoundAttached("event:/Sound Effects/03 Interactable/Doors/Alien_Door_Open/Alien_Door_End", plants[plantIndex]);
			plantSlots[2].targetable = true;
			break;
		}
	}

	private void powerOnSwitch(int index)
	{
		pattern0Found = false;
		pattern1Found = false;
		int num = index % 8;
		int num2 = index / 8;
		if (powerGrid[num2 + 1, num + 1] == 0)
		{
			game.setIsHoverable(powerSwitches[index], isHoverable: false);
			powerSwitches[index].materialState.transitionTo("On", 3f);
			powerGrid[num2 + 1, num + 1] = (powerSwitchesUpsideDown.Contains(index) ? 3 : 2);
		}
		else
		{
			game.setIsHoverable(powerSwitches[index], isHoverable: true);
			powerSwitches[index].materialState.transitionTo("Default", 3f);
			powerGrid[num2 + 1, num + 1] = 0;
		}
		for (int i = 0; i < powerRequirements.Count; i++)
		{
			if (findPattern(i))
			{
				Array.ForEach(powerStates[i], delegate(MaterialState x)
				{
					x.transitionTo("On");
				});
				if (i == 0)
				{
					pattern0Found = true;
				}
				if (i == 1)
				{
					pattern1Found = true;
				}
			}
			else
			{
				Array.ForEach(powerStates[i], delegate(MaterialState x)
				{
					x.transitionTo("Default", 3f);
				});
			}
		}
	}

	private bool findPattern(int gridIndex)
	{
		int y = powerRequirements[gridIndex].GetLength(0);
		int x = powerRequirements[gridIndex].GetLength(1);
		int i;
		for (i = 0; i < 10 - y + 1; i++)
		{
			int j;
			for (j = 0; j < 10 - x + 1; j++)
			{
				if (isMatch())
				{
					return true;
				}
			}
			bool isMatch()
			{
				for (int k = 0; k < y; k++)
				{
					for (int l = 0; l < x; l++)
					{
						if (powerRequirements[gridIndex][k, l] != 1 && powerGrid[i + k, j + l] != powerRequirements[gridIndex][k, l])
						{
							return false;
						}
					}
				}
				return true;
			}
		}
		return false;
	}

	public void roomLightSequence()
	{
		for (int i = 0; i < puzzlesNeoAnimations.Length; i++)
		{
			openPuzzle(i);
		}
		powerPuzzleAnimation.transitionTo("OpenNeo", 1f, 0f, playSound: true, 12f);
		game.startTimer(new ActivateObjectOnDelayTimer(puzzlesNeoAudio[3]), 12f);
		powerPuzzleParticles.Stop();
		Array.ForEach(plantFloorGlows, delegate(MaterialState x)
		{
			x.transitionToDuration("NewState", 2f);
		});
		cinematicIntro.SetActive(value: true);
		game.startTimer(new IntroCinematicTimer(), cinematicIntro.GetComponent<Animation>().clip.length);
		TweenState[] symbolsPillarPuzzleAnims = SymbolsPillarPuzzleAnims;
		for (int num = 0; num < symbolsPillarPuzzleAnims.Length; num++)
		{
			symbolsPillarPuzzleAnims[num].transitionTo("OpenPillar", 1f, 1f, playSound: true, 5f);
			game.startTimer(new ActivateObjectOnDelayTimer(puzzlesNeoAudio[5]), 5f);
			game.startTimer(new ActivateObjectOnDelayTimer(puzzlesNeoAudio[6]), 5f);
			game.startTimer(new ActivateObjectOnDelayTimer(puzzlesNeoAudio[7]), 5f);
		}
		Interactive[] array = interactablesToActivateOnPower;
		for (int num = 0; num < array.Length; num++)
		{
			array[num].targetable = true;
		}
	}

	private void openPuzzle(int index)
	{
		float[] array = new float[5] { 2.4f, 3.7f, 5f, 6f, 7.7f };
		puzzlesNeoAnimations[index].transitionTo("OpenNeo", 1f, 1f, playSound: true, array[index]);
		game.startTimer(new ActivateObjectOnDelayTimer(puzzlesNeoAudio[index]), array[index]);
		if (emitPuzzleTops[index] != null)
		{
			emitPuzzleTops.Get<GameObject>(index, 0f).SetActive(value: true);
			emitPuzzleTops.Get<ParticleSystem>(index, 0).Play();
		}
	}

	private void closePuzzle(int index, float delay)
	{
		puzzlesNeoAudio[index].SetActive(value: false);
		game.startTimer(new ActivateObjectOnDelayTimer(puzzlesNeoAudio[index]), delay);
		puzzlesNeoAnimations[index].transitionTo("OpenNeo", 1f, 0f, playSound: true, delay);
		emitPuzzleTops[index]?.Get<ParticleSystem>(index).Stop();
	}

	public void dominoUpdateLocks()
	{
		for (int i = 0; i < 6; i++)
		{
			dominoLockStates[i] = false;
			bool flag = false;
			bool flag2 = true;
			for (int j = 0; j < 9; j++)
			{
				flag |= dominoActiveSegments[j];
				flag2 &= !(dominoCombinations[i, j] ^ dominoActiveSegments[j]) || dominoCombinations[i, j];
			}
			if (flag && flag2)
			{
				dominoLockStates[i] = true;
			}
		}
		for (int k = 0; k < 6; k++)
		{
			dominoLockTSs[k].transitionToDuration(dominoLockStates[k] ? "Open" : "Default", 0.5f);
		}
	}

	private void dominoUpdateLaser()
	{
		int num = 0;
		for (int i = 0; i < dominoLockStates.Length && dominoLockStates[i] == dominoSolution[i]; i++)
		{
			num++;
		}
		dominoLaser.SetPositions(new Vector3[2]
		{
			dominoLaser.transform.position,
			dominoLaserTargets[num].position
		});
		dominoLaserHit.transform.position = dominoLaserTargets[num].position;
	}

	private void bioCheckPillars()
	{
		int i;
		for (i = 0; i < bioPlatesMS.Length; i++)
		{
			bioPlatesButtons[i].targetable = (bioButtonSelected != -1 && !Array.Exists(bioPillarIndices, (int index) => index == i)) || Array.Exists(bioButtonCurrent, (int index) => index == i);
			bool flag = bioButtonSelected != -1 && bioPlatesButtons[i].targetable;
			bioPlatesMS[i].transitionTo(flag ? "Highlight" : "Default", 4f);
		}
		MaterialState[] array = bioPillarsMS;
		for (int num = 0; num < array.Length; num++)
		{
			array[num].transitionTo("Default");
		}
		if (bioButtonCurrent[0] != -1)
		{
			int num2 = bioButtonCurrent[0] % 9;
			int num3 = bioButtonCurrent[0] / 9;
			for (int num4 = 1; num4 < 9; num4++)
			{
				int num5 = (num2 + num4) % 9;
				int num6 = (num3 + num4) % 9;
				BioTryDissolvePillar(9 * num3 + num5);
				BioTryDissolvePillar(9 * num6 + num2);
			}
			bioLaserRoot[0].SetActive(value: true);
			bioLaserRootTransform[0].position = bioPlatesButtons[bioButtonCurrent[0]].transform.position;
			for (int num7 = 0; num7 < bioLaser0LR.Length; num7++)
			{
				if (Physics.Raycast(bioLaser0Tran[num7].position, bioLaser0Tran[num7].up, out var hitInfo))
				{
					bioLaser0LR[num7].SetPosition(0, bioLaser0Tran[num7].position);
					bioLaser0LR[num7].SetPosition(1, hitInfo.point);
				}
			}
		}
		else
		{
			bioLaserRoot[0].SetActive(value: false);
		}
		if (bioButtonCurrent[1] != -1)
		{
			int num8 = bioButtonCurrent[1] % 9;
			int num9 = bioButtonCurrent[1] / 9;
			for (int num10 = 1; num10 < 8; num10++)
			{
				if (num9 + num10 < 9 && num8 + num10 < 9)
				{
					BioTryDissolvePillar(9 * (num9 + num10) + (num8 + num10));
				}
				if (num9 + num10 < 9 && num8 - num10 >= 0)
				{
					BioTryDissolvePillar(9 * (num9 + num10) + (num8 - num10));
				}
				if (num9 - num10 >= 0 && num8 + num10 < 9)
				{
					BioTryDissolvePillar(9 * (num9 - num10) + (num8 + num10));
				}
				if (num9 - num10 >= 0 && num8 - num10 >= 0)
				{
					BioTryDissolvePillar(9 * (num9 - num10) + (num8 - num10));
				}
			}
			bioLaserRoot[1].SetActive(value: true);
			bioLaserRootTransform[1].position = bioPlatesButtons[bioButtonCurrent[1]].transform.position;
			for (int num11 = 0; num11 < bioLaser1LR.Length; num11++)
			{
				if (Physics.Raycast(bioLaser1Tran[num11].position, bioLaser1Tran[num11].up, out var hitInfo2))
				{
					bioLaser1LR[num11].SetPosition(0, bioLaser1Tran[num11].position);
					bioLaser1LR[num11].SetPosition(1, hitInfo2.point);
				}
			}
		}
		else
		{
			bioLaserRoot[1].SetActive(value: false);
		}
		if (bioButtonCurrent[2] != -1)
		{
			int num12 = bioButtonCurrent[2] % 9;
			int num13 = bioButtonCurrent[2] / 9;
			if (num13 + 1 < 9)
			{
				BioTryDissolvePillar(9 * (num13 + 1) + num12);
				if (num12 + 1 < 9)
				{
					BioTryDissolvePillar(9 * (num13 + 1) + (num12 + 1));
				}
				if (num12 - 1 >= 0)
				{
					BioTryDissolvePillar(9 * (num13 + 1) + (num12 - 1));
				}
			}
			if (num13 - 1 >= 0)
			{
				BioTryDissolvePillar(9 * (num13 - 1) + num12);
				if (num12 + 1 < 9)
				{
					BioTryDissolvePillar(9 * (num13 - 1) + (num12 + 1));
				}
				if (num12 - 1 >= 0)
				{
					BioTryDissolvePillar(9 * (num13 - 1) + (num12 - 1));
				}
			}
			if (num12 + 1 < 9)
			{
				BioTryDissolvePillar(9 * num13 + (num12 + 1));
			}
			if (num12 - 1 >= 0)
			{
				BioTryDissolvePillar(9 * num13 + (num12 - 1));
			}
			bioLaserRoot[2].SetActive(value: true);
			bioLaserRootTransform[2].position = bioPlatesButtons[bioButtonCurrent[2]].transform.position;
		}
		else
		{
			bioLaserRoot[2].SetActive(value: false);
		}
		void BioTryDissolvePillar(int plateIndex)
		{
			for (int j = 0; j < bioPillarIndices.Length; j++)
			{
				if (bioPillarIndices[j] == plateIndex)
				{
					bioPillarsMS[j].transitionTo("Dissolve");
				}
			}
		}
	}

	public void plantUpdateValues(GameObject[] plants)
	{
		for (int i = 0; i < plants.Length; i++)
		{
			Vector3 position = plants[i].transform.position;
			position.y = 0f;
			float num = Vector3.Distance(position, plantDistanceRef);
			plantSunValues[i] = 5;
			for (int j = 0; j < plantSunDistances.Length; j++)
			{
				if (num > plantSunDistances[j])
				{
					plantSunValues[i] = 5 - j - 1;
				}
			}
			float num2 = 180f - Vector3.Angle(plantAngleBaseVector, position);
			plantWaterValues[i] = 1;
			for (int k = 0; k < plantAngles.Length; k++)
			{
				if (num2 > plantAngles[k])
				{
					plantWaterValues[i] = 2 + k;
				}
			}
		}
	}

	public void plantCalculateReferences(GameObject distanceRefObject, GameObject[] angleRefObjects)
	{
		plantDistanceRef = distanceRefObject.transform.position;
		plantDistanceRef.y = 0f;
		plantAngleRefs = new float[angleRefObjects.Length];
		Vector3 vector = (plantAngleBaseVector = distanceRefObject.transform.position - distanceRefObject.transform.right - distanceRefObject.transform.position);
		for (int i = 0; i < angleRefObjects.Length; i++)
		{
			Vector3 to = angleRefObjects[i].transform.position - distanceRefObject.transform.position;
			float num = Vector3.Angle(vector, to);
			plantAngleRefs[i] = num;
		}
	}

	public void plantUpdateSymbols(int plantIndex)
	{
		for (int i = 0; i < plantSunValues[plantIndex]; i++)
		{
			game.startTimer(new PlantSymbolReappearTimer(plantIndex, i, sun: true), 0.15f * (float)i);
		}
		for (int j = 0; j < plantWaterValues[plantIndex]; j++)
		{
			game.startTimer(new PlantSymbolReappearTimer(plantIndex, j, sun: false), 0.15f * (float)j);
		}
	}

	private void plantOnAddItemToInventory(Item item)
	{
		if (item == plantItems.Get<Item>(0, 0))
		{
			plantStates[1] = 2;
			game.startTimer(new PlantSkinnedMeshAnimationReverseTimer(plantSkinnedMeshAnimations[2], 1f, 0f), 1f);
			PineFmod.playOneShotSoundAttached("event:/Sound Effects/03 Interactable/Doors/Alien_Door_Open/Alien_Door_End", plants[1]);
		}
		if (item == plantItems.Get<Item>(3, 0))
		{
			plantStates[0] = 4;
		}
		if (item == plantItems.Get<Item>(1, 0))
		{
			plantStates[2] = 4;
		}
	}

	private void plantOnSlot(Slot slot)
	{
		if (slot == dominoTopSlot)
		{
			openDomino();
		}
		if (slot == coreSlot)
		{
			endSphereState = EndSphereState.RetractArm;
			armAnimationAudio.SetActive(value: true);
			coreSlotGraphic.SetActive(value: false);
		}
		if (slot == plantSlots[0])
		{
			plantItems.Get<Item>(0, 0).targetable = false;
			plantStates[0] = 2;
			game.startTimer(new PlantSkinnedMeshAnimationReverseTimer(plantSkinnedMeshAnimations[1], 1f, 0f), 1f);
			game.startTimer(new PlantCustomTimer(0), 1f);
			game.startTimer(new PlantSkinnedMeshAnimationTimer(plantSkinnedMeshAnimations[1], 0f, 1f), 1f, 2f);
			game.startTimer(new PlantAnimationTimer(plantAnimations[0], 0.3f, 0.57f), 1f, 3f);
			game.startTimer(new PlantSkinnedMeshAnimationTimer(plantSkinnedMeshAnimations[0], 0f, 1f), 0.5f, 3f);
			game.startTimer(new PlantSkinnedMeshAnimationReverseTimer(plantSkinnedMeshAnimations[0], 1f, 0f), 0.5f, 3.5f);
		}
		if (slot == plantSlots[1])
		{
			plantItems.Get<Item>(3, 0).targetable = false;
			plantStates[2] = 2;
			game.startTimer(new PlantSkinnedMeshAnimationReverseTimer(plantSkinnedMeshAnimations[4], 1f, 0f), 1f);
			game.startTimer(new PlantCustomTimer(1), 1f);
			game.startTimer(new PlantSkinnedMeshAnimationTimer(plantSkinnedMeshAnimations[4], 0f, 1f), 1f, 2f);
			game.startTimer(new PlantAnimationTimer(plantAnimations[2], 0.25f, 0.316f), 1f, 3f);
			game.startTimer(new PlantSkinnedMeshAnimationTimer(plantSkinnedMeshAnimations[3], 0f, 1f), 0.5f, 3f);
			game.startTimer(new PlantSkinnedMeshAnimationReverseTimer(plantSkinnedMeshAnimations[3], 1f, 0f), 0.5f, 3.5f);
		}
		if (slot == plantSlots[2])
		{
			plantItems.Get<Item>(1, 0).targetable = false;
			plantStates[3] = 2;
			game.startTimer(new PlantSkinnedMeshAnimationReverseTimer(plantSkinnedMeshAnimations[5], 1f, 0f), 1f);
			game.startTimer(new PlantAnimationTimer(plantAnimations[3], 0.5f, 0.75f), 1f);
			game.startTimer(new PlantCustomTimer(2), 1f);
			game.startTimer(new PlantCustomTimer(3), 3f);
			game.startTimer(new PlantSkinnedMeshAnimationTimer(plantSkinnedMeshAnimations[5], 0f, 1f), 1f, 2f);
			game.startTimer(new PlantAnimationTimer(plantAnimations[3], 0.75f, 1f), 1f, 2f);
		}
		if (slot == tabletSlots[0])
		{
			endTablet1Pillar();
		}
		if (slot == tabletSlots[1])
		{
			endTablet2Pillar();
		}
		if (slot == tabletSlots[2])
		{
			endTablet3Pillar();
		}
		[DebugButton("Open Domino Puzzle", Tint.Default, (PostClickAction)0, 0, new object[] { })]
		void openDomino()
		{
			puzzlesNeoAudio[0].SetActive(value: false);
			game.startTimer(new ActivateObjectOnDelayTimer(puzzlesNeoAudio[0]), 0.5f);
			DominoAnim.transitionTo("OpenNeo", 1f, 1f, playSound: true, 0.5f);
			dominoParticles.Get<GameObject>(0f).SetActive(value: true);
			dominoParticles.Get<ParticleSystem>(0).Play();
		}
	}

	private void onPlantTimerDone(int phase)
	{
		if (phase == 0)
		{
			plantItems.Get<MaterialState>(0, 0f).transitionToDuration("Dissolve");
		}
		if (phase == 1)
		{
			plantItems.Get<MaterialState>(3, 0f).transitionToDuration("Dissolve");
		}
		if (phase == 2)
		{
			plantItems.Get<MaterialState>(1, 0f).transitionToDuration("Dissolve");
		}
		if (phase == 3)
		{
			plantItems.Get<Item>(1, 0).gameObject.SetActive(value: false);
			plantItems.Get<Item>(2, 0).gameObject.SetActive(value: true);
			plantItems.Get<MaterialState>(2, 0f).setState("Dissolve");
			plantItems.Get<MaterialState>(2, 0f).transitionToDuration("Default", 2f);
			game.finishPuzzle(Puzzle.Astrobotany);
		}
	}

	private void updateWayfinderRunes(int index, int dir)
	{
		wayfinderRuneRightButtons[index].targetable = false;
		game.setIsHoverable(wayfinderRuneRightButtons[index], isHoverable: false);
		wayfinderRuneRightButtons[index].tweenState.transitionToDuration("Shrink", 0.05f);
		game.startTimer(new WayfinderButtonTimer(done: false, index), 0.075f);
		game.startTimer(new WayfinderButtonTimer(done: true, index), 0.125f);
		wayfinderRuneStates[index] += dir;
		wayfinderRuneStates[index] = ((wayfinderRuneStates[index] == -1) ? 3 : wayfinderRuneStates[index]);
		wayfinderRuneStates[index] = ((wayfinderRuneStates[index] != 4) ? wayfinderRuneStates[index] : 0);
	}

	[DebugButton("Solve Wayfinder", Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void wayfinderSolvePuzzle()
	{
		game.increaseZoomCounter(wayfinderZoomable.gameObject);
		wayfinderZoomable.targetable = false;
		Array.ForEach(wayfinderRuneLeftButtons, delegate(Switch3D x)
		{
			x.targetable = false;
		});
		Array.ForEach(wayfinderRuneRightButtons, delegate(Switch3D x)
		{
			x.targetable = false;
		});
		closePuzzle(1, 0.5f);
		tablets[1].gameObject.SetActive(value: true);
		tabletPickupParents[1].SetActive(value: true);
	}

	private void projectionCheckSolution()
	{
		List<int[]> list = new List<int[]> { projectionZoom_1Nodes, projectionZoom_2Nodes, projectionZoom_3Nodes, projectionZoom_4Nodes };
		List<MaterialState[]> list2 = new List<MaterialState[]> { projectionZoomsMs_1, projectionZoomsMs_2, projectionZoomsMs_3, projectionZoomsMs_4 };
		Array.ForEach(projectionZoomsMs_1, delegate(MaterialState x)
		{
			x.transitionToDuration("Fade", 0.2f);
		});
		Array.ForEach(projectionZoomsMs_2, delegate(MaterialState x)
		{
			x.transitionToDuration("Fade", 0.2f);
		});
		Array.ForEach(projectionZoomsMs_3, delegate(MaterialState x)
		{
			x.transitionToDuration("Fade", 0.2f);
		});
		Array.ForEach(projectionZoomsMs_4, delegate(MaterialState x)
		{
			x.transitionToDuration("Fade", 0.2f);
		});
		for (int num = 0; num < list.Count; num++)
		{
			for (int num2 = 0; num2 < list[num].Length; num2++)
			{
				if (!projectionSlidableGraph.tryGetPiece(projectionSlidableGraph.nodes[list[num][num2]], out var piece) || !(piece.gameObject != null))
				{
					continue;
				}
				if (projectionCylinders.Contains(piece.gameObject))
				{
					list2[num][0].transitionToDuration("Default", 0.2f);
				}
				if (projectionPyramids.Contains(piece.gameObject))
				{
					if (Math.Abs(Vector3.Dot(list2[num][1].transform.up, piece.transform.GetChild(0).right)) < 0.5f)
					{
						list2[num][1].transitionToDuration("Default", 0.2f);
					}
					if (Math.Abs(Vector3.Dot(list2[num][2].transform.right, piece.transform.GetChild(0).right)) > 0.5f)
					{
						list2[num][2].transitionToDuration("Default", 0.2f);
					}
				}
				if (projectionPillars.Contains(piece.gameObject))
				{
					if (Math.Abs(Vector3.Dot(list2[num][5].transform.right, piece.transform.GetChild(0).GetChild(0).right)) < 0.5f)
					{
						list2[num][5].transitionToDuration("Default", 0.2f);
					}
					if (Vector3.Dot(list2[num][4].transform.up, piece.transform.GetChild(0).GetChild(0).up) > 0.5f)
					{
						list2[num][4].transitionToDuration("Default", 0.2f);
					}
					if (Vector3.Dot(list2[num][3].transform.up, piece.transform.GetChild(0).GetChild(0).up) > 0.5f)
					{
						list2[num][3].transitionToDuration("Default", 0.2f);
					}
				}
			}
		}
	}

	[DebugButton("Solve Ruins/Projection Puzzle", Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void ruinsSolvePuzzle()
	{
		game.increaseZoomCounter(ruinsZoomable.gameObject);
		ruinsZoomable.targetable = false;
		ruinsHandle.targetable = false;
		RuinsRotator.targetable = false;
		projectionSlidableGraph.piecesList.ForEach(delegate(SlidableGraphPiece x)
		{
			x.targetable = false;
		});
		ruinsPuzzleReward.Get<Item>(0).gameObject.SetActive(value: true);
		game.startTimer(new PlantAnimationReverseTimer(ruinsPuzzleArmingAnimation, 1f, 0f), 1f);
		game.startTimer(new PlantAnimationReverseTimer(ruinsPuzzleHandleAnimation, 1f, 0f), 1f);
		closePuzzle(2, 0.5f);
	}

	public override void onInit()
	{
		initPowerPuzzle();
		initSpawnPoint();
		initWayfinderPuzzle();
		initPartitionPuzzle();
		initDominoPuzzle();
		initTracingPuzzle();
		initPlantPuzzle();
		initTablet();
		initTablet2();
		initTablet3();
		initProjectionPuzzle();
		initSymbolPuzzle();
		initSpacePuzzle();
		initMisc();
		void initDominoPuzzle()
		{
			dominoLaser.SetPositions(new Vector3[2]
			{
				dominoLaser.transform.position,
				dominoLaserTargets[0].position
			});
			dominoUpdateLocks();
		}
		void initMisc()
		{
			waterDrawingMaterialState.setState("Off");
		}
		void initPartitionPuzzle()
		{
			Interactive[] array = interactablesToActivateOnPower;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].targetable = false;
			}
			mainLight.intensity = 1000f;
			powerPuzzleAnimation.setWeight("OpenNeo", 1f);
		}
		void initPlantPuzzle()
		{
			plantCalculateReferences(plantDistanceRefObject, plantAngleRefObjects);
			plantSuns.Add(plantSuns_1);
			plantSuns.Add(plantSuns_2);
			plantSuns.Add(plantSuns_3);
			plantSuns.Add(plantSuns_4);
			plantWaters.Add(plantWaters_1);
			plantWaters.Add(plantWaters_2);
			plantWaters.Add(plantWaters_3);
			plantWaters.Add(plantWaters_4);
			plantUpdateSymbols(0);
			plantUpdateSymbols(1);
			plantUpdateSymbols(2);
			plantUpdateSymbols(3);
		}
		void initPowerPuzzle()
		{
			powerStates = new List<MaterialState[]> { powerStates_1, powerStates_2, powerStates_3, powerStates_4, powerStates_5 };
		}
		void initProjectionPuzzle()
		{
			projectionRotations = new int[projectionRuinPieces.Length];
			projectionRotations[1] = 1;
			projectionRotations[2] = 1;
			projectionCylinders.Add(projectionRuinPieces[0]);
			projectionPyramids.Add(projectionRuinPieces[1]);
			projectionPyramids.Add(projectionRuinPieces[4]);
			projectionPillars.Add(projectionRuinPieces[2]);
			projectionPillars.Add(projectionRuinPieces[3]);
			projectionSolution.Add(0, (projectionPillars, new int[1]));
			projectionSolution.Add(1, (null, new int[1]));
			projectionSolution.Add(2, (null, new int[1]));
			projectionSolution.Add(3, (projectionCylinders, new int[4] { 0, 1, 2, 3 }));
			projectionSolution.Add(4, (null, new int[1]));
			projectionSolution.Add(5, (projectionPyramids, new int[2] { 0, 2 }));
			projectionSolution.Add(6, (null, new int[1]));
			projectionSolution.Add(7, (projectionPyramids, new int[4] { 1, 2, 3, 4 }));
			projectionSolution.Add(8, (projectionPillars, new int[1] { 1 }));
			projectionCheckSolution();
		}
		void initSpacePuzzle()
		{
			spaceTargetRendererPosition = spaceTargetRenderer.Get<Transform>(0f).position;
		}
		void initSpawnPoint()
		{
			game.targetRotation = new Vector2(225.5f, 0f);
		}
		void initSymbolPuzzle()
		{
			symbolStates = new bool[symbolSwitches.Length];
			symbolSolutions = new List<HashSet<int>>
			{
				new HashSet<int> { 12, 13, 41, 44, 65 },
				new HashSet<int> { 37, 52, 81 },
				new HashSet<int> { 88, 97, 102 },
				new HashSet<int> { 37, 54, 84 }
			};
		}
		static void initTablet()
		{
		}
		void initTablet2()
		{
			foreach (Ref<MeshRenderer, MaterialState> tablet2Line in tablet2Lines)
			{
				tablet2Line.Get<MaterialState>(0f).setState("Disabled");
			}
			tablet2States = new bool[tablet2Icospheres.Length];
			tablet2Solution = new bool[tablet2Icospheres.Length];
			tablet2Solution[1] = true;
			tablet2Solution[6] = true;
			tablet2Solution[9] = true;
			tablet2Solution[5] = true;
		}
		void initTablet3()
		{
			tablet3NextSymbols.Add(tablet3StartSymbol);
		}
		void initTracingPuzzle()
		{
			GameObject[] array = bioPlatesCircle;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetActive(value: false);
			}
			array = bioLaserRoot;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetActive(value: false);
			}
			for (int j = 0; j < bioPlatesButtons.Length; j++)
			{
				bioPlatesButtons[j].targetable = false;
			}
		}
		void initWayfinderPuzzle()
		{
			Item[] array = tablets;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].gameObject.SetActive(value: false);
			}
			updateWayfinderRunes(0, 1);
			updateWayfinderRunes(0, 1);
			updateWayfinderRunes(1, -1);
			updateWayfinderRunes(2, 1);
			updateWayfinderRunes(3, 1);
			updateWayfinderRunes(3, -1);
		}
	}

	public override void onTool(Item tool, ToolContext context)
	{
		if (!(tool == AlienMirror_p) || starKeyPicked)
		{
			return;
		}
		if (context.state == ToolState.Start)
		{
			starKeyLastMissTime = Time.time;
		}
		else if (context.state == ToolState.Update)
		{
			if (Vector3.Angle(new Vector3(336f, 213f, 0f), game.getCameraRotation().eulerAngles) > 1f)
			{
				starKeyLastMissTime = Time.time;
			}
			if (Time.time - starKeyLastMissTime > 30f && !starKeyPicked)
			{
				starKeyPicked = true;
				game.showStarKeyPopup(2);
				game.killAllPointers();
				game.increaseZoomCounter(AlienMirror_p.gameObject);
			}
		}
	}

	public override void onUpdate()
	{
		updateEndSphere();
		if (partitionSolved)
		{
			currentLightIntensity = Mathf.MoveTowards(currentLightIntensity, 1215.307f, Time.deltaTime * 200f);
			game.updateLevelLightIntensity(mainLight, () => currentLightIntensity);
		}
		else
		{
			game.updateLevelLightIntensity(mainLight, () => currentLightIntensity);
		}
		updateAlienCamera();
		updatePlantPuzzle();
		updateJoystick();
		void updateAlienCamera()
		{
			if (game.isInInventory(AlienMirror_p.gameObject))
			{
				alienCameraTransform.position = game.getCameraPosition();
				alienCameraTransform.rotation = game.getCameraRotation();
				alienCameraTransform.Rotate(Vector3.forward, 50f);
			}
		}
		void updateJoystick()
		{
			float x = spaceJoystick.currentRotation.x * Time.deltaTime;
			float z = spaceJoystick.currentRotation.y * Time.deltaTime;
			Vector3 position = spaceTargetRenderer.Get<Transform>(0f).position + 0.2f * new Vector3(x, 0f, z);
			float min = spaceTargetRendererPosition.x - 20f;
			float max = spaceTargetRendererPosition.x + 20f;
			float min2 = spaceTargetRendererPosition.z - 20f;
			float max2 = spaceTargetRendererPosition.z + 20f;
			position.x = Math.Clamp(position.x, min, max);
			position.z = Math.Clamp(position.z, min2, max2);
			spaceTargetRenderer.Get<Transform>(0f).position = position;
		}
		void updatePlantPuzzle()
		{
			plantUpdateValues(plants);
			bool flag = false;
			for (int i = 0; i < plants.Length; i++)
			{
				GameObject gameObject = plants[i];
				if (game.isDraggingDraggableOnAnyPointer(gameObject))
				{
					flag = true;
					plantLastInteractedIndex = i;
				}
			}
			if (flag)
			{
				plantTimer = 1f;
				plantLastFrameWasInteracting = true;
				Array.ForEach(plantSuns[plantLastInteractedIndex], delegate(GameObject x)
				{
					x.SetActive(value: false);
				});
				Array.ForEach(plantWaters[plantLastInteractedIndex], delegate(GameObject x)
				{
					x.SetActive(value: false);
				});
				game.cancelTimers((PlantSymbolReappearTimer x) => x.plantIndex == plantLastInteractedIndex);
			}
			else
			{
				plantTimer -= Time.deltaTime;
				if (plantLastFrameWasInteracting)
				{
					plantUpdateSymbols(plantLastInteractedIndex);
					plantLastInteractedIndex = -1;
					plantLastFrameWasInteracting = false;
				}
			}
			if (plantTimer <= 0f)
			{
				plantTimer = Math.Max(plantTimer, -5f);
			}
		}
	}

	public override void onMaterialTransitionDone(MaterialState tweenState, string state)
	{
		if (tweenState == spaceDissolveMaterialState.Get<MaterialState>(0f) && spaceDissolveMaterialState.Get<MaterialState>(0f).getTargetWeight("Appear") == 0f)
		{
			spaceDissolveMaterialState.Get<GameObject>(0).SetActive(value: false);
		}
		if (!(tweenState == plantFloorGlows[0]))
		{
			return;
		}
		if (plantFloorGlows[0].getTargetWeight("NewState") >= 0.5f)
		{
			Array.ForEach(plantFloorGlows, delegate(MaterialState x)
			{
				x.transitionToDuration("Default", 2f);
			});
		}
		else
		{
			Array.ForEach(plantFloorGlows, delegate(MaterialState x)
			{
				x.transitionToDuration("NewState", 2f);
			});
		}
	}

	private void updateEndSphere()
	{
		GameObject obj = centralSpheres[0];
		EndSphereState endSphereState = this.endSphereState;
		obj.SetActive(endSphereState == EndSphereState.InitialRotation || endSphereState == EndSphereState.EndingInitialRotation);
		GameObject obj2 = centralSpheres[1];
		endSphereState = this.endSphereState;
		obj2.SetActive(endSphereState == EndSphereState.SecondSphereInitialRotation || endSphereState == EndSphereState.DissolveDoor || endSphereState == EndSphereState.FirstArm || endSphereState == EndSphereState.RetractArm || endSphereState == EndSphereState.UndisolveDoor);
		centralSpheres[2].SetActive(this.endSphereState == EndSphereState.SpeedUp);
		centralSpheres[3].SetActive(this.endSphereState == EndSphereState.TopSpeed);
		if (this.endSphereState != endSphereStateLastFrame)
		{
			sphereChangeTime = Time.time;
			endSphereStateLastFrame = this.endSphereState;
		}
		if (this.endSphereState == EndSphereState.InitialRotation)
		{
			sphereAnimation1.clip.SampleAnimation(sphereAnimation1.gameObject, Time.time % sphereAnimation1.clip.length);
		}
		else if (this.endSphereState == EndSphereState.EndingInitialRotation)
		{
			float t = (Time.time - sphereChangeTime) / 2f;
			float num = Mathf.Lerp(sphereChangeTime % sphereAnimation1.clip.length, sphereAnimation1.clip.length, t);
			Debug.Log(num + " " + sphereAnimation1.clip.length + " " + t);
			sphereAnimation1.clip.SampleAnimation(sphereAnimation1.gameObject, num);
			if (num >= sphereAnimation1.clip.length - 0.0001f)
			{
				this.endSphereState = EndSphereState.SecondSphereInitialRotation;
				coreOpenCutscene.SetActive(value: true);
				game.startTimer(new DisableObjectOnDelayTimer(coreOpenCutscene), 5f);
			}
		}
		else if (this.endSphereState == EndSphereState.SecondSphereInitialRotation)
		{
			float t2 = (Time.time - sphereChangeTime) / 2f;
			float num2 = Mathf.Lerp(0f, sphereAnimation2.clip.length, t2);
			sphereAnimation2.clip.SampleAnimation(sphereAnimation2.gameObject, num2);
			if (num2 >= sphereAnimation2.clip.length - 0.0001f)
			{
				this.endSphereState = EndSphereState.DissolveDoor;
				PineFmod.playOneShotSoundAttached("event:/Sound Effects/05 Misc/Magic/Woosh_2", sphereDisolveDoor.gameObject);
			}
		}
		else if (this.endSphereState == EndSphereState.DissolveDoor)
		{
			float num3 = (Time.time - sphereChangeTime) / 2f;
			sphereDisolveDoor.setState("Dissolve", Mathf.Min(1f, num3));
			if (num3 >= 1f)
			{
				this.endSphereState = EndSphereState.FirstArm;
				armAnimationAudio.SetActive(value: true);
				SphereDrop.transitionToDuration("Drop", 2f);
			}
		}
		else if (this.endSphereState == EndSphereState.FirstArm)
		{
			float num4 = (Time.time - sphereChangeTime) / 2f;
			float time = Mathf.Lerp(0f, sphereArmAnimation.clip.length, num4);
			sphereArmAnimation.clip.SampleAnimation(sphereArmAnimation.gameObject, time);
			if (num4 >= 1f)
			{
				armAnimationAudio.SetActive(value: false);
			}
		}
		else if (this.endSphereState == EndSphereState.RetractArm)
		{
			float t3 = (Time.time - sphereChangeTime) / 2f;
			float num5 = Mathf.Lerp(sphereArmAnimation.clip.length, 0f, t3);
			sphereArmAnimation.clip.SampleAnimation(sphereArmAnimation.gameObject, num5);
			if (num5 <= 0.0001f)
			{
				this.endSphereState = EndSphereState.UndisolveDoor;
				PineFmod.playOneShotSoundAttached("event:/Sound Effects/05 Misc/Magic/Woosh_2", sphereDisolveDoor.gameObject);
				armAnimationAudio.SetActive(value: false);
			}
		}
		else if (this.endSphereState == EndSphereState.UndisolveDoor)
		{
			float num6 = (Time.time - sphereChangeTime) / 2f;
			sphereDisolveDoor.setState("Dissolve", Mathf.Max(0f, 1f - num6));
			if (num6 >= 1f)
			{
				this.endSphereState = EndSphereState.SpeedUp;
			}
		}
		else if (this.endSphereState == EndSphereState.SpeedUp)
		{
			float t4 = (Time.time - sphereChangeTime) / sphereAnimationSpeedup.clip.length;
			float num7 = Mathf.Lerp(0f, sphereAnimationSpeedup.clip.length, t4);
			sphereAnimationSpeedup.clip.SampleAnimation(sphereAnimationSpeedup.gameObject, num7);
			if (num7 >= sphereAnimationSpeedup.clip.length - 0.0001f)
			{
				this.endSphereState = EndSphereState.TopSpeed;
				SphereDrop.transitionToDuration("Drop", 2f);
				buttonsTabletUp.transitionToDuration("Open", 2f, 1f, playSound: true, 4.1f);
				buttonsOpenCutscene.SetActive(value: true);
				game.startTimer(new DisableObjectOnDelayTimer(buttonsOpenCutscene), 6f);
				MirrorPillarAnimation.transitionToDuration("Open", 2f, 1f, playSound: true, 0.5f);
				EmitterPlanes_p.SetActive(value: true);
				AlienMirror_p.gameObject.SetActive(value: true);
				alienWorldContainer.SetActive(value: true);
			}
		}
		else if (this.endSphereState == EndSphereState.TopSpeed)
		{
			if (!levelEnded)
			{
				centralSpheres[3].SetActive(value: false);
				sphereSlowDown.SetActive(value: true);
			}
			if (centralSpheres[3].activeSelf)
			{
				sphereAnimationTopSpeed.clip.SampleAnimation(centralSpheres[3], Time.time % sphereAnimationTopSpeed.clip.length);
			}
		}
	}

	public override void onTimerUpdate(Timer timer)
	{
		if (timer is MonitorDissolveTimer)
		{
			float weight = timer.time / timer.duration;
			monitorOpenDissolve.setState("Dissolve", weight);
			spaceDissolveMaterialState.Get<MaterialState>(0f).transitionToDuration("Appear", 2f);
			spaceDissolveMaterialState.Get<GameObject>(0).SetActive(value: true);
		}
		else if (timer is IntroCinematicTimer)
		{
			float t = timer.time / timer.duration;
			mainLight.intensity = t * 1215.307f;
			game.updateLevelLightIntensity(firstPuzzlePointLight, () => t * 600f / 12.565f);
			game.updateLevelLightIntensity(firstPuzzleLight, () => (1f - t) * 5000f);
			firstPuzzleVolume.volumetricDimmer = (1f - t) * 0.11f;
			if (t > 0.1f)
			{
				Array.ForEach(plants, delegate(GameObject x)
				{
					x.SetActive(value: true);
				});
				plantUpdateSymbols(0);
				plantUpdateSymbols(1);
				plantUpdateSymbols(2);
				plantUpdateSymbols(3);
				holoball.SetActive(value: true);
				waterVapour.SetActive(value: true);
				Array.ForEach(objectsToSetActiveOnStart, delegate(GameObject x)
				{
					x.SetActive(value: true);
				});
				waterDrawingMaterialState.setState("Default");
				if (spaceDissolveMaterialState.Get<MaterialState>(0f).getTargetWeight("Dissolve") == 0f)
				{
					spaceDissolveMaterialState.Get<MaterialState>(0f).transitionToDuration("Dissolve", 2f);
				}
			}
			if (timer.time > timer.duration / 2f)
			{
				partitionLight.range = 0f;
			}
		}
		if (timer is PlantAnimationTimer plantAnimationTimer)
		{
			float num = timer.time / timer.duration;
			plantAnimationTimer.sampler.setUnitTime(plantAnimationTimer.from + (plantAnimationTimer.target - plantAnimationTimer.from) * num);
		}
		if (timer is PlantAnimationReverseTimer plantAnimationReverseTimer)
		{
			float num2 = timer.time / timer.duration;
			plantAnimationReverseTimer.sampler.setUnitTime(plantAnimationReverseTimer.from - (0f - plantAnimationReverseTimer.target + plantAnimationReverseTimer.from) * num2);
		}
		if (timer is PlantSkinnedMeshAnimationTimer plantSkinnedMeshAnimationTimer)
		{
			float num3 = timer.time / timer.duration;
			plantSkinnedMeshAnimationTimer.renderer.SetBlendShapeWeight(0, plantSkinnedMeshAnimationTimer.from + (plantSkinnedMeshAnimationTimer.target - plantSkinnedMeshAnimationTimer.from) * num3 * 100f);
		}
		if (timer is PlantSkinnedMeshAnimationReverseTimer plantSkinnedMeshAnimationReverseTimer)
		{
			float num4 = timer.time / timer.duration;
			plantSkinnedMeshAnimationReverseTimer.renderer.SetBlendShapeWeight(0, plantSkinnedMeshAnimationReverseTimer.from * 100f - (0f - plantSkinnedMeshAnimationReverseTimer.target + plantSkinnedMeshAnimationReverseTimer.from) * num4 * 100f);
		}
	}

	public override void onTimerDone(Timer timer)
	{
		if (timer is DoorOpenSoundTimer)
		{
			PineFmod.playOneShotSoundAttached("event:/Sound Effects/05 Misc/Air/Deep_Air_Woosh", endDoorSampler.gameObject);
		}
		if (timer is DisableObjectOnDelayTimer disableObjectOnDelayTimer)
		{
			disableObjectOnDelayTimer.obj.SetActive(value: false);
		}
		if (timer is EndCutsceneEmissionTimer endCutsceneEmissionTimer)
		{
			endCutsceneEmissionTimer.state.transitionToDuration("Glow");
		}
		if (timer is EndDoorDissolveTimer)
		{
			Array.ForEach(endDoorDissolves, delegate(MaterialState x)
			{
				x.transitionToDuration("Dissolve", 0.5f);
			});
		}
		if (timer is ActivateObjectOnDelayTimer activateObjectOnDelayTimer)
		{
			activateObjectOnDelayTimer.obj.SetActive(value: true);
		}
		if (timer is SymbolEffectTimer symbolEffectTimer)
		{
			symbolEffects[symbolEffectTimer.index].SetActive(value: true);
		}
		if (timer is SymbolBlinkTimer symbolBlinkTimer)
		{
			bool flag = symbolSwitches[symbolBlinkTimer.index].materialState.getTargetWeight("Solved") < 0.5f;
			symbolSwitches[symbolBlinkTimer.index].materialState.setState(flag ? "Solved" : "Default");
		}
		if (timer is PlantSymbolReappearTimer plantSymbolReappearTimer)
		{
			if (plantSymbolReappearTimer.sun)
			{
				plantSuns[plantSymbolReappearTimer.plantIndex][plantSymbolReappearTimer.symbolIndex].SetActive(value: true);
			}
			else
			{
				plantWaters[plantSymbolReappearTimer.plantIndex][plantSymbolReappearTimer.symbolIndex].SetActive(value: true);
			}
		}
		if (timer is TracingSolvedTimer)
		{
			game.finishPuzzle(Puzzle.Industry);
			tracingSolvePuzzle();
		}
		if (timer is DominoSolvedTimer)
		{
			game.finishPuzzle(Puzzle.Query);
			dominoSolvePuzzle();
		}
		if (timer is WayfinderSolvedTimer)
		{
			game.finishPuzzle(Puzzle.FracturedTelescope);
			wayfinderSolvePuzzle();
		}
		if (timer is SpaceSolvedTimer)
		{
			game.finishPuzzle(Puzzle.StarMap);
			spaceSolvePuzzle();
		}
		if (timer is RuinsSolvedTimer)
		{
			game.finishPuzzle(Puzzle.Perspective);
			ruinsSolvePuzzle();
		}
		if (timer is Tablet3ResetTimer tablet3ResetTimer)
		{
			tablet3GeneratedSymbols.ForEach(delegate(GameObject x)
			{
				x.SetActive(!x.activeSelf);
			});
			if (tablet3ResetTimer.i == 5)
			{
				Array.ForEach(tablet3Switches, delegate(Switch3D x)
				{
					x.targetable = true;
				});
				tablet3GeneratedSymbols.ForEach(delegate(GameObject x)
				{
					x.SetActive(value: false);
				});
				tablet3GeneratedSymbols.Clear();
				tablet3NextSymbols = new List<GameObject> { tablet3StartSymbol };
			}
		}
		if (timer is WayfinderButtonTimer wayfinderButtonTimer)
		{
			if (!wayfinderButtonTimer.done)
			{
				wayfinderRuneFilters[wayfinderButtonTimer.index].mesh = wayfinderRuneMeshes[wayfinderRuneStates[wayfinderButtonTimer.index]];
				wayfinderRuneRightButtons[wayfinderButtonTimer.index].tweenState.transitionToDuration("Default", 0.05f);
			}
			else
			{
				wayfinderRuneRightButtons[wayfinderButtonTimer.index].targetable = true;
				game.setIsHoverable(wayfinderRuneRightButtons[wayfinderButtonTimer.index], isHoverable: true);
			}
		}
		if (timer is PowerGridFinishTimer)
		{
			game.finishPuzzle(Puzzle.TriangleTerminal);
			game.increaseZoomCounter(partitionZoomable.gameObject);
			roomLightSequence();
		}
		if (timer is PlantCustomTimer plantCustomTimer)
		{
			onPlantTimerDone(plantCustomTimer.phase);
		}
		else if (timer is IntroCinematicTimer)
		{
			cinematicIntro.SetActive(value: false);
		}
		_ = timer is RoomLightTimer;
		if (timer is LevelEndCutsceneTimer)
		{
			game.finishPuzzle(Puzzle.Faraway);
			levelEndCutscene.SetActive(value: true);
			Array.ForEach(symbolEffects, delegate(GameObject x)
			{
				x.SetActive(value: false);
			});
		}
		if (timer is LevelFinishTimer)
		{
			levelEndCutscene.SetActive(value: false);
			game.levelCompleted();
		}
	}

	private void tracingSolvePuzzle()
	{
		game.increaseZoomCounter(tracingZoomable.gameObject);
		tracingZoomable.targetable = false;
		bioComputerAnimation.transitionToDuration("Default", 2f);
		puzzlesNeoAudio[4].SetActive(value: false);
		game.startTimer(new ActivateObjectOnDelayTimer(puzzlesNeoAudio[4]), 0.01f);
		tablets[2].gameObject.SetActive(value: true);
		tabletPickupParents[2].SetActive(value: true);
		SlotTriangle.gameObject.SetActive(value: false);
	}

	public override void onSlidableGraphReleased(SlidableGraph.OnPieceInteraction context)
	{
		if (context.graph == projectionSlidableGraph)
		{
			projectionCheckSolution();
		}
	}

	[DebugButton("Open Bio Computer", Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void openBioComp()
	{
		bioComputerAnimation.transitionTo("OpenNeo", 1f, 1f, playSound: true, 0.5f);
	}

	public override void onSlot(Slot targetSlot)
	{
		plantOnSlot(targetSlot);
		if (targetSlot == SlotTriangle)
		{
			game.startTimer(new ActivateObjectOnDelayTimer(puzzlesNeoAudio[4]), 0.5f);
			bioComputerAnimation.transitionTo("OpenNeo", 1f, 1f, playSound: true, 0.5f);
			bioComputerParticles.Play();
			Key_lowAnim.transitionTo("Dissolve");
		}
	}

	[DebugButton("Unlock Projection Puzzle:", Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void unlockProjectionPuzzle()
	{
		Array.ForEach(SymbolsPillarPuzzleAnims, delegate(TweenState x)
		{
			x.setState("Default");
		});
		game.activateSlotWithItemNonSynced(coreSlot, coreSlot.acceptItems[0]);
	}

	public override void onSwitch3D(Switch3D targetSwitch, Switch3DEvent switchEvent)
	{
		if (targetSwitch == endPlaneSwitch && switchEvent == Switch3DEvent.Start)
		{
			game.levelCompleted();
		}
		if (switchEvent != Switch3DEvent.Start)
		{
			return;
		}
		int num = -1;
		if ((num = Array.IndexOf(dominoSwitches, targetSwitch)) != -1)
		{
			dominoActiveSegments[num] = !dominoActiveSegments[num];
			dominoUpdateLocks();
			dominoUpdateLaser();
			SelectorHighlights[num].SetActive(dominoActiveSegments[num]);
		}
		else if ((num = Array.IndexOf(powerSwitches, targetSwitch)) != -1)
		{
			powerOnSwitch(num);
		}
		else if ((num = Array.IndexOf(bioButtons, targetSwitch)) != -1)
		{
			if (bioButtonCurrent[num] != -1)
			{
				bioPlatesCircle[bioButtonCurrent[num]].SetActive(value: false);
				bioButtonCurrent[num] = -1;
			}
			if (bioButtonSelected != -1)
			{
				bioButtonsMS[bioButtonSelected].transitionTo("Default", 4f);
			}
			bioButtonSelected = num;
			bioButtonsMS[bioButtonSelected].transitionTo("Selected", 4f);
			bioCheckPillars();
		}
		else if ((num = Array.IndexOf(bioPlatesButtons, targetSwitch)) != -1)
		{
			int num2 = -1;
			for (int i = 0; i < bioButtonCurrent.Length; i++)
			{
				if (num == bioButtonCurrent[i])
				{
					bioButtonsMS[i].transitionTo("Default", 4f);
					bioButtonCurrent[i] = -1;
					bioPlatesCircle[num].SetActive(value: false);
					num2 = i;
				}
			}
			if (bioButtonSelected != -1)
			{
				bioButtonsMS[bioButtonSelected].transitionTo("Disabled", 4f);
				bioButtonCurrent[bioButtonSelected] = num;
				bioPlatesCircle[num].SetActive(value: true);
				bioButtonSelected = -1;
			}
			else if (num2 != -1)
			{
				bioButtonSelected = num2;
				bioButtonsMS[bioButtonSelected].transitionTo("Selected", 4f);
			}
			bioCheckPillars();
		}
		else if ((num = Array.IndexOf(wayfinderRuneLeftButtons, targetSwitch)) != -1)
		{
			updateWayfinderRunes(num, -1);
		}
		else if ((num = Array.IndexOf(wayfinderRuneRightButtons, targetSwitch)) != -1)
		{
			updateWayfinderRunes(num, 1);
		}
		else if ((num = Array.IndexOf(tablet3Switches, targetSwitch)) != -1)
		{
			tablet3Sequence.Add(num);
			if (tablet3Sequence.Count <= 3)
			{
				foreach (GameObject item in new List<GameObject>(tablet3NextSymbols))
				{
					tablet3NextSymbols.RemoveAt(0);
					for (int j = 0; j < item.transform.childCount; j++)
					{
						Ref<GameObject, Transform> obj = tablet3Symbols[num + 5 * symbolsUsed];
						symbolsUsed++;
						obj.Get<Transform>(0f).position = item.transform.GetChild(j).position;
						obj.Get<GameObject>(0).SetActive(value: true);
						tablet3GeneratedSymbols.Add(obj);
						tablet3NextSymbols.Add(obj);
					}
				}
				return;
			}
			Array.ForEach(tablet3Switches, delegate(Switch3D x)
			{
				x.targetable = false;
			});
			for (int num3 = 0; num3 < 6; num3++)
			{
				tablet3Sequence.Clear();
				symbolsUsed = 0;
				int i2 = num3;
				game.startTimer(new Tablet3ResetTimer(i2), 0.3f * (float)num3);
			}
		}
		else if ((num = Array.IndexOf(tablet2Switches, targetSwitch)) != -1)
		{
			tablet2States[num] = !tablet2States[num];
			tablet2Icospheres[num].SetActive(tablet2States[num]);
			for (int num4 = 0; num4 < tablet2Lines.Length; num4++)
			{
				tablet2Lines.Get<MaterialState>(num4, 0f).setState((tablet2States[tablet2LineMap[num4].Item1] && tablet2States[tablet2LineMap[num4].Item2]) ? "Default" : "Disabled");
			}
		}
		else
		{
			if ((num = Array.IndexOf(symbolSwitches, targetSwitch)) == -1)
			{
				return;
			}
			symbolStates[num] = !symbolStates[num];
			symbolSwitches[num].materialState.setState(symbolStates[num] ? "Active" : "Default");
			string text = "";
			HashSet<int> hashSet = new HashSet<int>();
			for (int num5 = 0; num5 < symbolStates.Length; num5++)
			{
				if (symbolStates[num5])
				{
					hashSet.Add(num5);
					text = text + num5 + ", ";
				}
			}
			Debug.Log(text);
			for (int num6 = 0; num6 < symbolSolutions.Count; num6++)
			{
				if (symbolSolutions[num6].SetEquals(hashSet))
				{
					int num7 = num6;
					if (num6 == 3)
					{
						num7 = 1;
					}
					ExitGateSymbols[num7].SetActive(value: true);
					if (!ExitGateSymbols.All((GameObject x) => x.activeInHierarchy))
					{
						game.startTimer(new SymbolEffectTimer(num7), 1f);
					}
					List<int> list = new List<int>(hashSet);
					for (int num8 = 0; num8 < list.Count; num8++)
					{
						symbolSwitches[list[num8]].targetable = false;
						symbolSwitches[list[num8]].materialState.setState("Solved");
						game.startTimer(new SymbolBlinkTimer(list[num8]), 0.25f);
						game.startTimer(new SymbolBlinkTimer(list[num8]), 0.5f);
						game.startTimer(new SymbolBlinkTimer(list[num8]), 0.75f);
						game.startTimer(new SymbolBlinkTimer(list[num8]), 1f);
						symbolStates[list[num8]] = false;
					}
				}
			}
		}
	}

	private void dominoSolvePuzzle()
	{
		game.increaseZoomCounter(DominoZoom.gameObject);
		DominoZoom.targetable = false;
		DominoAnim.transitionTo("OpenNeo", 1f, 0f, playSound: true, 0.5f);
		dominoParticles.Get<ParticleSystem>(0).Stop();
		dominoTopSlot.gameObject.SetActive(value: false);
		dominoLaser.positionCount = 0;
		tablets[0].gameObject.SetActive(value: true);
		tabletPickupParents[0].SetActive(value: true);
	}

	public override void onZoomEnter(GameObject zoomedItem)
	{
		if (zoomedItem == tracingZoomable)
		{
			tracingZoomable.targetable = false;
		}
		else if (zoomedItem == ruinsZoomable.gameObject)
		{
			projectionSlidableGraph.piecesList.ForEach(delegate(SlidableGraphPiece x)
			{
				x.targetPriority = 3;
			});
		}
	}

	public override void onZoomLeave(GameObject zoomedItem)
	{
		if (zoomedItem == tracingZoomable)
		{
			tracingZoomable.targetable = true;
		}
		else if (zoomedItem == ruinsZoomable.gameObject)
		{
			projectionSlidableGraph.piecesList.ForEach(delegate(SlidableGraphPiece x)
			{
				x.targetPriority = -1;
			});
		}
	}

	private void endTablet1Pillar()
	{
		if (tabletSlots[0].isUnlocked && tablet1Solved)
		{
			Array.ForEach(tablet1Dials, delegate(Dial x)
			{
				x.targetable = false;
			});
			game.increaseZoomCounter(interactablesToActivateOnPower[2]);
			interactablesToActivateOnPower[2].targetable = false;
			SymbolsPillarPuzzleAnims[0].transitionTo("OpenPillars", 1f, 0f, playSound: true, 0.5f);
			puzzlesNeoAudio[5].SetActive(value: false);
			game.startTimer(new ActivateObjectOnDelayTimer(puzzlesNeoAudio[5]), 0.5f);
		}
	}

	private void endTablet2Pillar()
	{
		if (tabletSlots[1].isUnlocked && tablet2Solved)
		{
			Array.ForEach(tablet2Switches, delegate(Switch3D x)
			{
				x.targetable = false;
			});
			game.increaseZoomCounter(symbolsPillarZoom);
			symbolsPillarZoom.targetable = false;
			SymbolsPillarPuzzleAnims[1].transitionTo("OpenPillars", 1f, 0f, playSound: true, 0.5f);
			puzzlesNeoAudio[6].SetActive(value: false);
			game.startTimer(new ActivateObjectOnDelayTimer(puzzlesNeoAudio[6]), 0.5f);
		}
	}

	private void endTablet3Pillar()
	{
		if (tabletSlots[2].isUnlocked && tablet3Solved)
		{
			Array.ForEach(tablet3Switches, delegate(Switch3D x)
			{
				x.targetable = false;
			});
			game.increaseZoomCounter(interactablesToActivateOnPower[1]);
			interactablesToActivateOnPower[1].targetable = false;
			SymbolsPillarPuzzleAnims[2].transitionTo("OpenPillars", 1f, 0f, playSound: true, 0.5f);
			puzzlesNeoAudio[7].SetActive(value: false);
			game.startTimer(new ActivateObjectOnDelayTimer(puzzlesNeoAudio[7]), 0.5f);
		}
	}

	public override void onSwapper(Swapper swapper, Swapper.SwapperEvent swapperEvent)
	{
		SwapperPiece getCurrentSelected = swapperEvent.getCurrentSelected;
		if (getCurrentSelected != null)
		{
			int num = Array.IndexOf(spaceJigsawPieces, getCurrentSelected.gameObject);
			game.setIsHoverable(getCurrentSelected, isHoverable: false);
			spacePieceSelectors[num].SetActive(value: true);
			spacePieceMSs[num].setState("Selected");
		}
		if (swapperEvent.isReset)
		{
			int num2 = Array.IndexOf(spaceJigsawPieces, getCurrentSelected.gameObject);
			spacePieceMSs[num2].setState("Default");
			game.setIsHoverable(getCurrentSelected, isHoverable: true);
			spacePieceSelectors[num2].SetActive(value: false);
		}
		if (swapperEvent.didSwap && swapper == spaceSwapper)
		{
			int num3 = Array.IndexOf(spaceJigsawPieces, swapperEvent.first.gameObject);
			int num4 = Array.IndexOf(spaceJigsawPieces, swapperEvent.second.gameObject);
			spacePieceMSs[num3].setState("Default");
			spacePieceMSs[num4].setState("Default");
			spacePieceSelectors[num3].SetActive(value: false);
			spacePieceSelectors[num4].SetActive(value: false);
			game.setIsHoverable(swapperEvent.first, isHoverable: true);
			game.setIsHoverable(swapperEvent.second, isHoverable: true);
		}
	}

	[DebugButton("Solve space puzzle", Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void spaceSolvePuzzle()
	{
		game.increaseZoomCounter(spaceZoomable);
		spaceZoomable.targetable = false;
		spaceJoystick.targetable = false;
		spaceSwapper.targetable = false;
		Array.ForEach(spaceJigsawSwapperPiece, delegate(SwapperPiece x)
		{
			x.targetable = false;
		});
		game.startTimer(new MonitorDissolveTimer(), 1f, 0.5f);
		monitorKey.SetActive(value: true);
	}

	public override void onJoystickMoved(Joystick joystick, MoveEvent moveEvent)
	{
	}

	public override void onTweenTransitionDone(TweenState tweenState, string state)
	{
		if (tweenState == puzzlesNeoAnimations[2] && !ruinsArmed)
		{
			ruinsArmed = true;
			ruinsPuzzleArmingAnimation.play(1f, 2f);
			ruinsPuzzleHandleAnimation.play(1f, 2f);
		}
		if (tweenState == monitorClose)
		{
			game.startTimer(new MonitorDissolveTimer(), 1f, 0.5f);
		}
	}

	public override void onAddToInventory(Item item)
	{
		plantOnAddItemToInventory(item);
		if (item.gameObject == monitorKey)
		{
			game.increaseZoomCounter(spaceZoomable.gameObject);
			closePuzzle(0, 0.5f);
		}
		else
		{
			_ = item == ruinsPuzzleReward.Get<Item>(0);
		}
		if (item == AlienMirror_p)
		{
			MirrorPillarAnimation.transitionToDuration("Default");
		}
		int num = Array.IndexOf(plantPlates, item);
		if (num != -1 && !pickedUpPlantHints.Contains(plantPlates[num]))
		{
			pickedUpPlantHints.Add(plantPlates[num]);
			TweenState tweenState = plantHintDispenser;
			int num2 = ++plantHintDispenserState;
			tweenState.transitionToDuration(num2.ToString(), 0.5f);
		}
	}

	public override void onInitAfterLoad()
	{
		if (centralSpheres[1].activeSelf)
		{
			sphereChangeTime = Time.time;
			sphereAnimation2.clip.SampleAnimation(centralSpheres[1], sphereAnimation2.clip.length);
			sphereArmAnimation.clip.SampleAnimation(sphereArmAnimation.gameObject, sphereArmAnimation.clip.length);
		}
	}

	public override void onInitHints()
	{
		game.setPuzzleConditions(Puzzle.Astrobotany, default(Puzzle));
		game.setPuzzleConditions(Puzzle.Perspective, default(Puzzle));
		game.setPuzzleConditions(Puzzle.FracturedTelescope, default(Puzzle));
		game.setPuzzleConditions(Puzzle.StarMap, default(Puzzle));
		game.setPuzzleConditions(Puzzle.Industry, Puzzle.Astrobotany);
		game.setPuzzleConditions(Puzzle.Query, Puzzle.StarMap);
		game.setPuzzleConditions(Puzzle.StarTablet, Puzzle.FracturedTelescope, Puzzle.Industry, Puzzle.Query);
		game.setPuzzleConditions(Puzzle.Faraway, Puzzle.StarTablet, Puzzle.Perspective);
		game.setRelevantObjectsForPuzzle(Puzzle.TriangleTerminal, Array.ConvertAll(powerSwitches, (Switch3D x) => x.gameObject));
		game.setRelevantObjectsForPuzzle(Puzzle.Astrobotany, plants);
		game.setRelevantObjectsForPuzzle(Puzzle.Astrobotany, Array.ConvertAll(plantPlates, (Item x) => x.gameObject));
		game.setRelevantObjectsForPuzzle(Puzzle.Perspective, projectionRuinPieces);
		game.setRelevantObjectsForPuzzle(Puzzle.Perspective, RuinsRotator.gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.FracturedTelescope, holoball, wayfinderZoomable.gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.FracturedTelescope, Array.ConvertAll(wayfinderRuneRightButtons, (Switch3D x) => x.gameObject));
		game.setRelevantObjectsForPuzzle(Puzzle.StarMap, spaceZoomable.gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.StarMap, spaceJigsawPieces);
		game.setRelevantObjectsForPuzzle(Puzzle.Industry, tracingZoomable.gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.Industry, plantItems.Get<Item>(2, 0).gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.Industry, Array.ConvertAll(bioButtons, (Switch3D x) => x.gameObject));
		game.setRelevantObjectsForPuzzle(Puzzle.Industry, Array.ConvertAll(bioPlatesButtons, (Switch3D x) => x.gameObject));
		game.setRelevantObjectsForPuzzle(Puzzle.Query, monitorKey);
		game.setRelevantObjectsForPuzzle(Puzzle.Query, Array.ConvertAll(dominoSwitches, (Switch3D x) => x.gameObject));
		game.setRelevantObjectsForPuzzle(Puzzle.StarTablet, Array.ConvertAll(tablets, (Item x) => x.gameObject));
		game.setRelevantObjectsForPuzzle(Puzzle.StarTablet, Array.ConvertAll(tabletSlots, (Slot x) => x.gameObject));
		game.setRelevantObjectsForPuzzle(Puzzle.Faraway, AlienMirror_p.gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.Faraway, Array.ConvertAll(symbolSwitches, (Switch3D x) => x.gameObject));
		game.setHintCondition(Puzzle.TriangleTerminal, TriangleTerminalHint.TTSee, () => game.wasLookedAtCurrentPuzzle(partitionZoomable.gameObject));
		game.setHintCondition(Puzzle.TriangleTerminal, TriangleTerminalHint.TTFirstOne, () => pattern0Found);
		game.setHintCondition(Puzzle.TriangleTerminal, TriangleTerminalHint.TTSecondOne, () => pattern1Found);
		game.setHintCondition(Puzzle.FracturedTelescope, FracturedTelescopeHint.PickupBall, () => game.wasAddedToInventoryDuringCurrentPuzzle(holoball));
		game.setHintCondition(Puzzle.FracturedTelescope, FracturedTelescopeHint.TerminalSee, () => game.wasLookedAtCurrentPuzzle(wayfinderZoomable.gameObject));
		game.setHintCondition(Puzzle.FracturedTelescope, FracturedTelescopeHint.TerminalFirstOne, () => wayfinderRuneStates[0] == 0);
		game.setHintCondition(Puzzle.Perspective, PerspectiveHint.RuinsSee, () => game.wasLookedAtCurrentPuzzle(ruinsZoomable.gameObject));
		game.setHintCondition(Puzzle.StarMap, StarMapHint.MapSee, () => game.wasLookedAtCurrentPuzzle(spaceZoomable.gameObject));
		game.setHintCondition(Puzzle.Astrobotany, AstrobotanyHint.PickupDiscs, () => game.wasAnyAddedToInventoryDuringCurrentPuzzle(Array.ConvertAll(plantPlates, (Item x) => x.gameObject)));
		game.setHintCondition(Puzzle.Industry, IndustryHint.PickupOpener, () => game.wasAddedToInventoryDuringCurrentPuzzle(Key_lowAnim.gameObject));
		game.setHintCondition(Puzzle.Industry, IndustryHint.PlaceOpener, () => SlotTriangle.insertedItem != null);
		game.setHintCondition(Puzzle.Industry, IndustryHint.InteractWith, () => game.wasLookedAtCurrentPuzzle(tracingZoomable.gameObject));
		game.setHintCondition(Puzzle.Query, QueryHint.PickupOpener, () => game.wasAddedToInventoryDuringCurrentPuzzle(monitorKey));
		game.setHintCondition(Puzzle.Query, QueryHint.PlaceOpener, () => dominoTopSlot.insertedItem != null);
		game.setHintCondition(Puzzle.Query, QueryHint.InteractWith, () => game.wasLookedAtCurrentPuzzle(DominoZoom.gameObject));
		game.setHintCondition(Puzzle.Query, QueryHint.FirstOne, () => dominoLockStates[6]);
		game.setHintCondition(Puzzle.Query, QueryHint.SecondOne, () => dominoLockStates[6] && dominoLockStates[8]);
		game.setHintCondition(Puzzle.StarTablet, StarTabletHint.TabletSwitches, () => game.wasAddedToInventoryDuringCurrentPuzzle(tablets[1].gameObject));
		game.setHintCondition(Puzzle.StarTablet, StarTabletHint.TabletWeb, () => game.wasAddedToInventoryDuringCurrentPuzzle(tablets[2].gameObject));
		game.setHintCondition(Puzzle.StarTablet, StarTabletHint.TabletDials, () => game.wasAddedToInventoryDuringCurrentPuzzle(tablets[0].gameObject));
		game.setHintCondition(Puzzle.StarTablet, StarTabletHint.PlaceSwitches, () => tabletSlots[1].insertedItem != null);
		game.setHintCondition(Puzzle.StarTablet, StarTabletHint.PlaceWeb, () => tabletSlots[2].insertedItem != null);
		game.setHintCondition(Puzzle.StarTablet, StarTabletHint.PlaceDials, () => tabletSlots[0].insertedItem != null);
		game.setHintCondition(Puzzle.StarTablet, StarTabletHint.SolveSwitches, () => tablet2Solved);
		game.setHintCondition(Puzzle.StarTablet, StarTabletHint.SolveWeb, () => tablet3Solved);
		game.setHintCondition(Puzzle.StarTablet, StarTabletHint.SolveDials, () => tablet1Solved);
		game.setHintCondition(Puzzle.Faraway, FarawayHint.GetCore, () => game.wasAddedToInventoryDuringCurrentPuzzle(ruinsPuzzleReward.Get<Item>(0).gameObject));
		game.setHintCondition(Puzzle.Faraway, FarawayHint.InsertCore, () => coreSlot.insertedItem != null);
		game.setHintCondition(Puzzle.Faraway, FarawayHint.GetMirror, () => game.wasAddedToInventoryDuringCurrentPuzzle(AlienMirror_p.gameObject));
	}

	[DebugButton("Set Tablet 1 To Solved", Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void setTablet1Solved()
	{
		tablet1Solved = true;
	}

	[DebugButton("Set Tablet 2 to Solved", Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void setTablet2Solved()
	{
		tablet2Solved = true;
	}

	[DebugButton("Set Tablet 3 to Solved", Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void setTablet3Solved()
	{
		tablet3Solved = true;
		tablet3Sequence = new List<int>(tablet3SequenceCorrect);
	}

	[DebugButton("Set All Tablets to Solved", Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void setAllTabletsSolved()
	{
		setTablet1Solved();
		setTablet2Solved();
		setTablet3Solved();
	}

	[DebugButton("Get All Tablets", Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void getAllTablets()
	{
		Array.ForEach(tablets, delegate(Item x)
		{
			game.addItemToInventory(x.gameObject);
		});
	}

	[DebugButton("Get Plant Reward", Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void getPlantReward()
	{
		game.addItemToInventory(plantItems.Get<Item>(2, 0).gameObject);
	}

	[DebugButton("Buttons Riseup", Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void buttonsRiseUp()
	{
		buttonsTabletUp.transitionTo("Open", 1f, 1f, playSound: true, 1.5f);
		buttonsOpenCutscene.SetActive(value: true);
		game.startTimer(new DisableObjectOnDelayTimer(buttonsOpenCutscene), 3f);
		TweenState[] symbolsPillarPuzzleAnims = SymbolsPillarPuzzleAnims;
		for (int i = 0; i < symbolsPillarPuzzleAnims.Length; i++)
		{
			symbolsPillarPuzzleAnims[i].transitionTo("Default");
		}
	}

	[DebugButton("Get Space Core", Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void getSpaceCore()
	{
		game.addItemToInventory(ruinsPuzzleReward.Get<Item>(0).gameObject);
	}

	[DebugButton("Get Circular Artifact", Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void getCircularArtifact()
	{
		game.addItemToInventory(dominoTopSlot.acceptItems[0].gameObject);
	}

	public virtual void save(FastBinaryWriter writer)
	{
		writer.WriteList(powerStates, delegate(FastBinaryWriter w, MaterialState[] e)
		{
			w.WriteArray(e, delegate(FastBinaryWriter writer2, MaterialState component)
			{
				writer2.WriteComponent(component);
			});
		});
		writer.WriteArray2D(powerGrid, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.Write(in pattern0Found, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in pattern1Found, default(FastBinaryWriter.ForPrimitives));
		writer.WriteComponent(firstPuzzleVolume);
		writer.WriteComponent(firstPuzzleLight);
		writer.WriteComponent(firstPuzzlePointLight);
		writer.WriteComponent(partitionZoomable);
		writer.Write(in partitionSolved, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in partitionFlash, default(FastBinaryWriter.ForPrimitives));
		writer.WriteArray(dominoLockStates, delegate(FastBinaryWriter w, bool e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteArray(dominoActiveSegments, delegate(FastBinaryWriter w, bool e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteArray(bioButtonCurrent, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.Write(in bioButtonSelected, default(FastBinaryWriter.ForPrimitives));
		writer.WriteVector3(in plantDistanceRef);
		writer.WriteVector3(in plantAngleBaseVector);
		writer.WriteArray(plantAngleRefs, delegate(FastBinaryWriter w, float e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteArray(plantSunValues, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteArray(plantWaterValues, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteArray(plantStates, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteList(plantSuns, delegate(FastBinaryWriter w, GameObject[] e)
		{
			w.WriteArray(e, delegate(FastBinaryWriter writer2, GameObject gameObject)
			{
				writer2.WriteGameObject(gameObject);
			});
		});
		writer.WriteList(plantWaters, delegate(FastBinaryWriter w, GameObject[] e)
		{
			w.WriteArray(e, delegate(FastBinaryWriter writer2, GameObject gameObject)
			{
				writer2.WriteGameObject(gameObject);
			});
		});
		writer.Write(in plantHintDispenserState, default(FastBinaryWriter.ForPrimitives));
		writer.WriteArray(wayfinderRuneFilters, delegate(FastBinaryWriter w, MeshFilter e)
		{
			w.WriteComponent(e);
		});
		writer.WriteArray(wayfinderRuneStates, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteHashSet(correctWayfinderSymbols, delegate(FastBinaryWriter w, GameObject e)
		{
			w.WriteGameObject(e);
		});
		writer.Write(in tablet1Solved, default(FastBinaryWriter.ForPrimitives));
		writer.WriteArray(tablet2States, delegate(FastBinaryWriter w, bool e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteArray(tablet2Solution, delegate(FastBinaryWriter w, bool e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.Write(in tablet2Solved, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in symbolsUsed, default(FastBinaryWriter.ForPrimitives));
		writer.WriteList(tablet3NextSymbols, delegate(FastBinaryWriter w, GameObject e)
		{
			w.WriteGameObject(e);
		});
		writer.WriteList(tablet3GeneratedSymbols, delegate(FastBinaryWriter w, GameObject e)
		{
			w.WriteGameObject(e);
		});
		writer.WriteList(tablet3Sequence, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.Write(in tablet3Solved, default(FastBinaryWriter.ForPrimitives));
		writer.WriteList(tablet3SequenceCorrect, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteArray(projectionRotations, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteHashSet(projectionCylinders, delegate(FastBinaryWriter w, GameObject e)
		{
			w.WriteGameObject(e);
		});
		writer.WriteHashSet(projectionPillars, delegate(FastBinaryWriter w, GameObject e)
		{
			w.WriteGameObject(e);
		});
		writer.WriteHashSet(projectionPyramids, delegate(FastBinaryWriter w, GameObject e)
		{
			w.WriteGameObject(e);
		});
		writer.WriteArray(projectionZoom_1Nodes, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteArray(projectionZoom_2Nodes, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteArray(projectionZoom_3Nodes, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteArray(projectionZoom_4Nodes, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteArray(symbolStates, delegate(FastBinaryWriter w, bool e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteList(symbolSolutions, delegate(FastBinaryWriter w, HashSet<int> e)
		{
			w.WriteHashSet(e, delegate(FastBinaryWriter fastBinaryWriter, int value2)
			{
				fastBinaryWriter.Write(in value2, default(FastBinaryWriter.ForPrimitives));
			});
		});
		writer.Write(in symbolsHighlighted, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in symbolsDone, default(FastBinaryWriter.ForPrimitives));
		writer.WriteGameObject(spaceSelectedPiece);
		writer.WriteVector3(in spaceTargetRendererPosition);
		writer.WriteArray(spaceOccupiedSpaces, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		int value = (int)endSphereState;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		value = (int)endSphereStateLastFrame;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in sphereChangeTime, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in plantTimer, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in currentLightIntensity, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in plantLastFrameWasInteracting, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in plantLastInteractedIndex, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in starKeyLastMissTime, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in starKeyPicked, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in levelEnded, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in ruinsArmed, default(FastBinaryWriter.ForPrimitives));
		writer.WriteHashSet(pickedUpPlantHints, delegate(FastBinaryWriter w, Item e)
		{
			w.WriteComponent(e);
		});
	}

	public virtual void load(FastBinaryReader reader)
	{
		powerStates = reader.ReadList((FastBinaryReader r) => r.ReadArray((FastBinaryReader reader2) => reader2.ReadComponent<MaterialState>()));
		powerGrid = reader.ReadArray2D((FastBinaryReader r) => r.ReadInt32());
		pattern0Found = reader.ReadBoolean();
		pattern1Found = reader.ReadBoolean();
		firstPuzzleVolume = reader.ReadComponent<HDAdditionalLightData>();
		firstPuzzleLight = reader.ReadComponent<Light>();
		firstPuzzlePointLight = reader.ReadComponent<Light>();
		partitionZoomable = reader.ReadComponent<Zoomable>();
		partitionSolved = reader.ReadBoolean();
		partitionFlash = reader.ReadBoolean();
		dominoLockStates = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		dominoActiveSegments = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		bioButtonCurrent = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		bioButtonSelected = reader.ReadInt32();
		plantDistanceRef = reader.ReadVector3();
		plantAngleBaseVector = reader.ReadVector3();
		plantAngleRefs = reader.ReadArray((FastBinaryReader r) => r.ReadSingle());
		plantSunValues = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		plantWaterValues = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		plantStates = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		plantSuns = reader.ReadList((FastBinaryReader r) => r.ReadArray((FastBinaryReader reader2) => reader2.ReadGameObject()));
		plantWaters = reader.ReadList((FastBinaryReader r) => r.ReadArray((FastBinaryReader reader2) => reader2.ReadGameObject()));
		plantHintDispenserState = reader.ReadInt32();
		wayfinderRuneFilters = reader.ReadArray((FastBinaryReader r) => r.ReadComponent<MeshFilter>());
		wayfinderRuneStates = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		correctWayfinderSymbols = reader.ReadHashSet((FastBinaryReader r) => r.ReadGameObject());
		tablet1Solved = reader.ReadBoolean();
		tablet2States = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		tablet2Solution = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		tablet2Solved = reader.ReadBoolean();
		symbolsUsed = reader.ReadInt32();
		tablet3NextSymbols = reader.ReadList((FastBinaryReader r) => r.ReadGameObject());
		tablet3GeneratedSymbols = reader.ReadList((FastBinaryReader r) => r.ReadGameObject());
		tablet3Sequence = reader.ReadList((FastBinaryReader r) => r.ReadInt32());
		tablet3Solved = reader.ReadBoolean();
		tablet3SequenceCorrect = reader.ReadList((FastBinaryReader r) => r.ReadInt32());
		projectionRotations = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		projectionCylinders = reader.ReadHashSet((FastBinaryReader r) => r.ReadGameObject());
		projectionPillars = reader.ReadHashSet((FastBinaryReader r) => r.ReadGameObject());
		projectionPyramids = reader.ReadHashSet((FastBinaryReader r) => r.ReadGameObject());
		projectionZoom_1Nodes = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		projectionZoom_2Nodes = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		projectionZoom_3Nodes = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		projectionZoom_4Nodes = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		symbolStates = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		symbolSolutions = reader.ReadList((FastBinaryReader r) => r.ReadHashSet((FastBinaryReader fastBinaryReader) => fastBinaryReader.ReadInt32()));
		symbolsHighlighted = reader.ReadInt32();
		symbolsDone = reader.ReadBoolean();
		spaceSelectedPiece = reader.ReadGameObject();
		spaceTargetRendererPosition = reader.ReadVector3();
		spaceOccupiedSpaces = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		endSphereState = (EndSphereState)reader.ReadInt32();
		endSphereStateLastFrame = (EndSphereState)reader.ReadInt32();
		sphereChangeTime = reader.ReadSingle();
		plantTimer = reader.ReadSingle();
		currentLightIntensity = reader.ReadSingle();
		plantLastFrameWasInteracting = reader.ReadBoolean();
		plantLastInteractedIndex = reader.ReadInt32();
		starKeyLastMissTime = reader.ReadSingle();
		starKeyPicked = reader.ReadBoolean();
		levelEnded = reader.ReadBoolean();
		ruinsArmed = reader.ReadBoolean();
		pickedUpPlantHints = reader.ReadHashSet((FastBinaryReader r) => r.ReadComponent<Item>());
	}

	public virtual void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		int position = reader.Position;
		List<MaterialState[]> list = reader.ReadList((FastBinaryReader r) => r.ReadArray((FastBinaryReader reader2) => reader2.ReadComponent<MaterialState>()));
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "powerStates[" + ((list == null) ? string.Empty : list.Count.ToString()) + "]",
			fieldValue = (((list == null) ? "null" : string.Join(", ", list)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int[,] array = reader.ReadArray2D((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "powerGrid[" + ((array == null) ? string.Empty : array.Length.ToString()) + "]",
			fieldValue = (((array == null) ? "null" : "NOT SUPPORTED") ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "pattern0Found",
			fieldValue = $"{flag}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag2 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "pattern1Found",
			fieldValue = $"{flag2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		HDAdditionalLightData arg = reader.ReadComponent<HDAdditionalLightData>();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "firstPuzzleVolume",
			fieldValue = $"{arg}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Light arg2 = reader.ReadComponent<Light>();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "firstPuzzleLight",
			fieldValue = $"{arg2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Light arg3 = reader.ReadComponent<Light>();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "firstPuzzlePointLight",
			fieldValue = $"{arg3}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Zoomable arg4 = reader.ReadComponent<Zoomable>();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "partitionZoomable",
			fieldValue = $"{arg4}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag3 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "partitionSolved",
			fieldValue = $"{flag3}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag4 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "partitionFlash",
			fieldValue = $"{flag4}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool[] array2 = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "dominoLockStates[" + ((array2 == null) ? string.Empty : array2.Length.ToString()) + "]",
			fieldValue = (((array2 == null) ? "null" : string.Join(", ", array2)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool[] array3 = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "dominoActiveSegments[" + ((array3 == null) ? string.Empty : array3.Length.ToString()) + "]",
			fieldValue = (((array3 == null) ? "null" : string.Join(", ", array3)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int[] array4 = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "bioButtonCurrent[" + ((array4 == null) ? string.Empty : array4.Length.ToString()) + "]",
			fieldValue = (((array4 == null) ? "null" : string.Join(", ", array4)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "bioButtonSelected",
			fieldValue = $"{num}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Vector3 vector = reader.ReadVector3();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "plantDistanceRef",
			fieldValue = $"{vector}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Vector3 vector2 = reader.ReadVector3();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "plantAngleBaseVector",
			fieldValue = $"{vector2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float[] array5 = reader.ReadArray((FastBinaryReader r) => r.ReadSingle());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "plantAngleRefs[" + ((array5 == null) ? string.Empty : array5.Length.ToString()) + "]",
			fieldValue = (((array5 == null) ? "null" : string.Join(", ", array5)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int[] array6 = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "plantSunValues[" + ((array6 == null) ? string.Empty : array6.Length.ToString()) + "]",
			fieldValue = (((array6 == null) ? "null" : string.Join(", ", array6)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int[] array7 = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "plantWaterValues[" + ((array7 == null) ? string.Empty : array7.Length.ToString()) + "]",
			fieldValue = (((array7 == null) ? "null" : string.Join(", ", array7)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int[] array8 = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "plantStates[" + ((array8 == null) ? string.Empty : array8.Length.ToString()) + "]",
			fieldValue = (((array8 == null) ? "null" : string.Join(", ", array8)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<GameObject[]> list2 = reader.ReadList((FastBinaryReader r) => r.ReadArray((FastBinaryReader reader2) => reader2.ReadGameObject()));
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "plantSuns[" + ((list2 == null) ? string.Empty : list2.Count.ToString()) + "]",
			fieldValue = (((list2 == null) ? "null" : string.Join(", ", list2)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<GameObject[]> list3 = reader.ReadList((FastBinaryReader r) => r.ReadArray((FastBinaryReader reader2) => reader2.ReadGameObject()));
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "plantWaters[" + ((list3 == null) ? string.Empty : list3.Count.ToString()) + "]",
			fieldValue = (((list3 == null) ? "null" : string.Join(", ", list3)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num2 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "plantHintDispenserState",
			fieldValue = $"{num2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		MeshFilter[] array9 = reader.ReadArray((FastBinaryReader r) => r.ReadComponent<MeshFilter>());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "wayfinderRuneFilters[" + ((array9 == null) ? string.Empty : array9.Length.ToString()) + "]",
			fieldValue = (((array9 == null) ? "null" : string.Join(", ", (IEnumerable<MeshFilter>)array9)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int[] array10 = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "wayfinderRuneStates[" + ((array10 == null) ? string.Empty : array10.Length.ToString()) + "]",
			fieldValue = (((array10 == null) ? "null" : string.Join(", ", array10)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		HashSet<GameObject> hashSet = reader.ReadHashSet((FastBinaryReader r) => r.ReadGameObject());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "correctWayfinderSymbols[" + ((hashSet == null) ? string.Empty : hashSet.Count.ToString()) + "]",
			fieldValue = (((hashSet == null) ? "null" : string.Join(", ", hashSet)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag5 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "tablet1Solved",
			fieldValue = $"{flag5}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool[] array11 = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "tablet2States[" + ((array11 == null) ? string.Empty : array11.Length.ToString()) + "]",
			fieldValue = (((array11 == null) ? "null" : string.Join(", ", array11)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool[] array12 = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "tablet2Solution[" + ((array12 == null) ? string.Empty : array12.Length.ToString()) + "]",
			fieldValue = (((array12 == null) ? "null" : string.Join(", ", array12)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag6 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "tablet2Solved",
			fieldValue = $"{flag6}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num3 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "symbolsUsed",
			fieldValue = $"{num3}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<GameObject> list4 = reader.ReadList((FastBinaryReader r) => r.ReadGameObject());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "tablet3NextSymbols[" + ((list4 == null) ? string.Empty : list4.Count.ToString()) + "]",
			fieldValue = (((list4 == null) ? "null" : string.Join(", ", list4)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<GameObject> list5 = reader.ReadList((FastBinaryReader r) => r.ReadGameObject());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "tablet3GeneratedSymbols[" + ((list5 == null) ? string.Empty : list5.Count.ToString()) + "]",
			fieldValue = (((list5 == null) ? "null" : string.Join(", ", list5)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<int> list6 = reader.ReadList((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "tablet3Sequence[" + ((list6 == null) ? string.Empty : list6.Count.ToString()) + "]",
			fieldValue = (((list6 == null) ? "null" : string.Join(", ", list6)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag7 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "tablet3Solved",
			fieldValue = $"{flag7}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<int> list7 = reader.ReadList((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "tablet3SequenceCorrect[" + ((list7 == null) ? string.Empty : list7.Count.ToString()) + "]",
			fieldValue = (((list7 == null) ? "null" : string.Join(", ", list7)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int[] array13 = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "projectionRotations[" + ((array13 == null) ? string.Empty : array13.Length.ToString()) + "]",
			fieldValue = (((array13 == null) ? "null" : string.Join(", ", array13)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		HashSet<GameObject> hashSet2 = reader.ReadHashSet((FastBinaryReader r) => r.ReadGameObject());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "projectionCylinders[" + ((hashSet2 == null) ? string.Empty : hashSet2.Count.ToString()) + "]",
			fieldValue = (((hashSet2 == null) ? "null" : string.Join(", ", hashSet2)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		HashSet<GameObject> hashSet3 = reader.ReadHashSet((FastBinaryReader r) => r.ReadGameObject());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "projectionPillars[" + ((hashSet3 == null) ? string.Empty : hashSet3.Count.ToString()) + "]",
			fieldValue = (((hashSet3 == null) ? "null" : string.Join(", ", hashSet3)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		HashSet<GameObject> hashSet4 = reader.ReadHashSet((FastBinaryReader r) => r.ReadGameObject());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "projectionPyramids[" + ((hashSet4 == null) ? string.Empty : hashSet4.Count.ToString()) + "]",
			fieldValue = (((hashSet4 == null) ? "null" : string.Join(", ", hashSet4)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int[] array14 = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "projectionZoom_1Nodes[" + ((array14 == null) ? string.Empty : array14.Length.ToString()) + "]",
			fieldValue = (((array14 == null) ? "null" : string.Join(", ", array14)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int[] array15 = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "projectionZoom_2Nodes[" + ((array15 == null) ? string.Empty : array15.Length.ToString()) + "]",
			fieldValue = (((array15 == null) ? "null" : string.Join(", ", array15)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int[] array16 = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "projectionZoom_3Nodes[" + ((array16 == null) ? string.Empty : array16.Length.ToString()) + "]",
			fieldValue = (((array16 == null) ? "null" : string.Join(", ", array16)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int[] array17 = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "projectionZoom_4Nodes[" + ((array17 == null) ? string.Empty : array17.Length.ToString()) + "]",
			fieldValue = (((array17 == null) ? "null" : string.Join(", ", array17)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool[] array18 = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "symbolStates[" + ((array18 == null) ? string.Empty : array18.Length.ToString()) + "]",
			fieldValue = (((array18 == null) ? "null" : string.Join(", ", array18)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<HashSet<int>> list8 = reader.ReadList((FastBinaryReader r) => r.ReadHashSet((FastBinaryReader fastBinaryReader) => fastBinaryReader.ReadInt32()));
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "symbolSolutions[" + ((list8 == null) ? string.Empty : list8.Count.ToString()) + "]",
			fieldValue = (((list8 == null) ? "null" : string.Join(", ", list8)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num4 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "symbolsHighlighted",
			fieldValue = $"{num4}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag8 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "symbolsDone",
			fieldValue = $"{flag8}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		GameObject arg5 = reader.ReadGameObject();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "spaceSelectedPiece",
			fieldValue = $"{arg5}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Vector3 vector3 = reader.ReadVector3();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "spaceTargetRendererPosition",
			fieldValue = $"{vector3}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int[] array19 = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "spaceOccupiedSpaces[" + ((array19 == null) ? string.Empty : array19.Length.ToString()) + "]",
			fieldValue = (((array19 == null) ? "null" : string.Join(", ", array19)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		EndSphereState endSphereState = (EndSphereState)reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "endSphereState",
			fieldValue = $"{endSphereState}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		EndSphereState endSphereState2 = (EndSphereState)reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "endSphereStateLastFrame",
			fieldValue = $"{endSphereState2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num5 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "sphereChangeTime",
			fieldValue = $"{num5}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num6 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "plantTimer",
			fieldValue = $"{num6}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num7 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "currentLightIntensity",
			fieldValue = $"{num7}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag9 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "plantLastFrameWasInteracting",
			fieldValue = $"{flag9}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num8 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "plantLastInteractedIndex",
			fieldValue = $"{num8}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num9 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "starKeyLastMissTime",
			fieldValue = $"{num9}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag10 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "starKeyPicked",
			fieldValue = $"{flag10}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag11 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "levelEnded",
			fieldValue = $"{flag11}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag12 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "ruinsArmed",
			fieldValue = $"{flag12}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		HashSet<Item> hashSet5 = reader.ReadHashSet((FastBinaryReader r) => r.ReadComponent<Item>());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "pickedUpPlantHints[" + ((hashSet5 == null) ? string.Empty : hashSet5.Count.ToString()) + "]",
			fieldValue = (((hashSet5 == null) ? "null" : string.Join(", ", hashSet5)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
	}

	public override Timer getTimer(byte id)
	{
		return id switch
		{
			0 => new DoorOpenSoundTimer(), 
			1 => new EndDoorDissolveTimer(), 
			2 => new PlantSymbolReappearTimer(), 
			3 => new SymbolBlinkTimer(), 
			4 => new SymbolEffectTimer(), 
			5 => new LevelEndCutsceneTimer(), 
			6 => new RuinsSolvedTimer(), 
			7 => new SpaceSolvedTimer(), 
			8 => new DominoSolvedTimer(), 
			9 => new WayfinderSolvedTimer(), 
			10 => new TracingSolvedTimer(), 
			11 => new Tablet3ResetTimer(), 
			12 => new IntroCinematicTimer(), 
			13 => new WayfinderButtonTimer(), 
			14 => new RoomLightTimer(), 
			15 => new PlantCustomTimer(), 
			16 => new PlantAnimationTimer(), 
			17 => new PlantAnimationReverseTimer(), 
			18 => new PlantSkinnedMeshAnimationTimer(), 
			19 => new PlantSkinnedMeshAnimationReverseTimer(), 
			20 => new MonitorDissolveTimer(), 
			21 => new LevelFinishTimer(), 
			22 => new PowerGridFinishTimer(), 
			23 => new ActivateObjectOnDelayTimer(), 
			24 => new DisableObjectOnDelayTimer(), 
			25 => new EndCutsceneEmissionTimer(), 
			_ => null, 
		};
	}
}
