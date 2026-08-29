using System;
using System.Collections.Generic;
using System.Text;
using FMOD.Studio;
using UnityEngine;

public class Pirate1Logic : LevelLogic, ISaveable
{
	public sealed class DrinksColorTimer : Timer
	{
		public int index;

		public int valveIndex;

		public override byte getTypeId()
		{
			return 0;
		}

		public DrinksColorTimer()
		{
		}

		public DrinksColorTimer(int index, int valveIndex)
		{
			this.index = index;
			this.valveIndex = valveIndex;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in index, default(FastBinaryWriter.ForPrimitives));
			writer.Write(in valveIndex, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			index = reader.ReadInt32();
			valveIndex = reader.ReadInt32();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("index: " + $"{index}");
			stringBuilder.Append("valveIndex: " + $"{valveIndex}");
			return stringBuilder.ToString();
		}
	}

	public sealed class FishBoxLockDelayTimer : Timer
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

	public sealed class LanternsTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 2;
		}

		public LanternsTimer()
		{
		}

		public LanternsTimer(int index)
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

	public sealed class CompassTimer : Timer
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

	public sealed class BattleshipTimer : Timer
	{
		public int count;

		public override byte getTypeId()
		{
			return 4;
		}

		public BattleshipTimer()
		{
		}

		public BattleshipTimer(int count)
		{
			this.count = count;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in count, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			count = reader.ReadInt32();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("count: " + $"{count}");
			return stringBuilder.ToString();
		}
	}

	public sealed class DrummerSkullLeverTimer : Timer
	{
		public bool beforeFor;

		public bool inFor;

		public int index;

		public override byte getTypeId()
		{
			return 5;
		}

		public DrummerSkullLeverTimer()
		{
		}

		public DrummerSkullLeverTimer(bool beforeFor, bool inFor, int index)
		{
			this.beforeFor = beforeFor;
			this.inFor = inFor;
			this.index = index;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in beforeFor, default(FastBinaryWriter.ForPrimitives));
			writer.Write(in inFor, default(FastBinaryWriter.ForPrimitives));
			writer.Write(in index, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			beforeFor = reader.ReadBoolean();
			inFor = reader.ReadBoolean();
			index = reader.ReadInt32();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("beforeFor: " + $"{beforeFor}");
			stringBuilder.AppendLine("inFor: " + $"{inFor}");
			stringBuilder.Append("index: " + $"{index}");
			return stringBuilder.ToString();
		}
	}

	public sealed class DrummerToothClickTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 6;
		}

		public DrummerToothClickTimer()
		{
		}

		public DrummerToothClickTimer(int index)
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

	public sealed class GemShipTimer : Timer
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

	public sealed class DrinksValveTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 8;
		}

		public DrinksValveTimer()
		{
		}

		public DrinksValveTimer(int index)
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

	public sealed class DrinksCupTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 9;
		}

		public DrinksCupTimer()
		{
		}

		public DrinksCupTimer(int index)
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

	public sealed class PirateCupTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 10;
		}

		public PirateCupTimer()
		{
		}

		public PirateCupTimer(int index)
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

	public sealed class ExitKeysTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 11;
		}

		public ExitKeysTimer()
		{
		}

		public ExitKeysTimer(int index)
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

	public sealed class DiceMachineTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 12;
		}

		public DiceMachineTimer()
		{
		}

		public DiceMachineTimer(int index)
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

	public sealed class PourDrinksTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 13;
		}

		public PourDrinksTimer()
		{
		}

		public PourDrinksTimer(int index)
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

	public sealed class StartDrinkTimer : Timer
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

	public sealed class EndDrinkTimer : Timer
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

	public sealed class PaintingCenterTimer : Timer
	{
		public int index;

		public float startRot;

		public float targetRot;

		public override byte getTypeId()
		{
			return 16;
		}

		public PaintingCenterTimer()
		{
		}

		public PaintingCenterTimer(int index, float startRot, float targetRot)
		{
			this.index = index;
			this.startRot = startRot;
			this.targetRot = targetRot;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in index, default(FastBinaryWriter.ForPrimitives));
			writer.Write(in startRot, default(FastBinaryWriter.ForPrimitives));
			writer.Write(in targetRot, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			index = reader.ReadInt32();
			startRot = reader.ReadSingle();
			targetRot = reader.ReadSingle();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("index: " + $"{index}");
			stringBuilder.AppendLine("startRot: " + $"{startRot}");
			stringBuilder.Append("targetRot: " + $"{targetRot}");
			return stringBuilder.ToString();
		}
	}

	public sealed class MonkeyCymbalTimer : Timer
	{
		public int timeStampIndex;

		public override byte getTypeId()
		{
			return 17;
		}

		public MonkeyCymbalTimer()
		{
		}

		public MonkeyCymbalTimer(int timeStampIndex)
		{
			this.timeStampIndex = timeStampIndex;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in timeStampIndex, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			timeStampIndex = reader.ReadInt32();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("timeStampIndex: " + $"{timeStampIndex}");
			return stringBuilder.ToString();
		}
	}

	public sealed class MapDelayTimer : Timer
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

	private enum GemColor
	{
		Star = 0,
		Fish = 1,
		Circle = 2,
		None = -1
	}

	[Serializable]
	public class GemPointList
	{
		public List<GameObject> points;
	}

	private enum Drinks
	{
		Wine = 0,
		Juice = 1,
		Grog = 2,
		Beer = 3,
		None = 4
	}

	private enum SkeletonState
	{
		Chilling = 0,
		WantDrink = 1,
		Drink1 = 2,
		WaitForTake1 = 3,
		Drink2 = 4,
		WaitForTake2 = 5,
		Drink3 = 6,
		GiveKey = 7,
		Reset1 = 8,
		Reset2 = 9,
		NpcEnter = 10
	}

	public enum Drink
	{
		Wine = 0,
		Juice = 1,
		Grog = 2,
		Beer = 3,
		None = 4
	}

	public class DrinkCombination
	{
		public Drink Item1;

		public Drink Item2;
	}

	private enum DrinkEffect
	{
		None = -1,
		Bubbles = 0,
		Foam = 1,
		Vapour = 2
	}

	private enum RPCType
	{
		Skeleton = 0,
		SkeletonFail = 1,
		StorageDoors = 2,
		LanternsGood = 3,
		LanternsWrong = 4,
		Compass = 5,
		Dominos = 6,
		Battleship = 7,
		DrummerGood = 8,
		DrummerWrong = 9,
		Fishing = 10,
		SolveGemShip = 11,
		Dice = 12,
		ExitKeys = 13
	}

	private enum Puzzle
	{
		[PuzzleInfo("%PuzzleP1_1%", false)]
		Domino = 0,
		[PuzzleInfo("%PuzzleP1_2%", false)]
		Drum = 1,
		[PuzzleInfo("%PuzzleP1_3%", false)]
		BarDoor = 2,
		[PuzzleInfo("%PuzzleP1_4%", false)]
		TableNumbers = 3,
		[PuzzleInfo("%PuzzleP1_5%", false)]
		StorageDoors = 4,
		[PuzzleInfo("%PuzzleP1_6%", false)]
		Battleship = 5,
		[PuzzleInfo("%PuzzleP1_7%", false)]
		Dice = 6,
		[PuzzleInfo("%PuzzleP1_8%", false)]
		Skeleton = 7,
		[PuzzleInfo("%PuzzleP1_9%", false)]
		BiggestCatch = 8,
		[PuzzleInfo("%PuzzleP1_10%", false)]
		Medallions = 9,
		[PuzzleInfo("%PuzzleP1_11%", false)]
		Portraits = 10,
		[PuzzleInfo("%PuzzleP1_12%", false)]
		DoorKeys = 11
	}

	private enum DominoHint
	{
		FindJigsaw = 0,
		OpenJigsaw = 1,
		CorrectSymbols = 2,
		CorrectOrientation = 3,
		OneCorrect = 4
	}

	private enum DrumHint
	{
		PickupMonkey = 0,
		PickupTeeth = 1,
		PullLever = 2,
		TurnMonkeyKey = 3,
		MonkeyIsRythm = 4,
		PlaceTeeth = 5
	}

	private enum BarDoorHint
	{
		PickupJigsawSolution = 0,
		PickupDrumSolution = 1,
		PickupNorth = 2,
		PickupSouth = 3,
		PlaceInSlots = 4,
		RotateSlots = 5
	}

	private enum TableNumbersHint
	{
		PickupOrders = 0,
		LookAtMenu = 1,
		MenuOnTable = 2,
		PullLantern = 3,
		SymbolOrder = 4
	}

	private enum StorageDoorsHint
	{
		PlaceKey = 0
	}

	private enum BattleshipHint
	{
		FindBattleship = 0,
		ReadTheRules = 1,
		NoDiagonals = 2,
		FiveSolved = 3,
		OthersAreWhat = 4
	}

	private enum DiceHint
	{
		RollDice = 0,
		LookAtDie = 1,
		PickUpChest = 2,
		OneSolution = 3,
		WhatIsOther = 4
	}

	private enum SkeletonHint
	{
		PickupMug = 0,
		PickupRecipe = 1,
		TalkToSkelly = 2,
		MixADrink = 3,
		SolveFirst = 4,
		SolveSecond = 5,
		SolveThird = 6
	}

	private enum BiggestCatchHint
	{
		PickUpGemAndChest = 0,
		LookAtCatchSkeleton = 1,
		PlaceGem = 2,
		SolutionPart = 3
	}

	private enum MedallionsHint
	{
		PlaceInSlots = 0,
		DrawKnots = 1,
		DrawAnchors = 2,
		DrawHelms = 3,
		DrawAnchors2 = 4,
		DrawKnots2 = 5
	}

	private enum PortraitsHint
	{
		LookAtStatue = 0,
		PickUpPaper = 1,
		PickUpGem = 2,
		PlaceGem = 3,
		Ricky = 4,
		Devil = 5,
		Jack = 6,
		John = 7,
		EnterSolution = 8
	}

	private enum DoorKeysHint
	{
		PlaceKeys = 0
	}

	[Serializable]
	public class TableLanternSideNumbers
	{
		public GameObject[] numbers;
	}

	private readonly float[] monkeyCymbalTimeStamps = new float[5] { 0.527f, 1.579f, 2.316f, 3.474f, 4.685f };

	private bool solvedLanterns;

	[DontSave]
	public List<int> compassSolutionA;

	[DontSave]
	public List<int> compassSolutionB;

	[DontSave]
	private Dictionary<GameObject, Pose> dominoSolution = new Dictionary<GameObject, Pose>();

	[DontSave]
	private TweenState[][] battleshipFoundTweens = new TweenState[7][];

	private List<List<bool>> battleshipCurrentState = new List<List<bool>>
	{
		new List<bool> { false, false, false, false, false, false, false, true },
		new List<bool> { false, true, false, false, false, true, false, false },
		new List<bool> { false, false, false, false, false, false, false, false },
		new List<bool> { false, false, true, false, false, false, false, true },
		new List<bool> { false, false, false, false, false, false, false, false },
		new List<bool> { false, false, false, false, false, true, false, true },
		new List<bool> { false, true, false, false, false, false, false, false },
		new List<bool> { false, false, false, false, false, false, true, false }
	};

	private int battleshipCounter;

	private const int gemPointsCount = 12;

	[DontSave]
	private List<GemColor> gemSolution = new List<GemColor>
	{
		GemColor.Fish,
		GemColor.Star,
		GemColor.Fish,
		GemColor.Star,
		GemColor.Fish,
		GemColor.Circle,
		GemColor.Star,
		GemColor.Circle,
		GemColor.Star,
		GemColor.Fish,
		GemColor.Circle,
		GemColor.Circle
	};

	private List<GemColor> gemCurrentState = new List<GemColor>();

	private GemColor gemCurrentColor = GemColor.None;

	public List<GemPointList> gemPoints;

	private bool juiceDrinkDone;

	private bool grogDrinkDone;

	private bool beerWineDrinkDone;

	private readonly float skeletonAnimSpeed = 0.03212f;

	private readonly float[] skeletonAnimTime = new float[11]
	{
		0.125f,
		0.25f,
		0.405f,
		0.4375f,
		0.588f,
		0.625f,
		0.775f,
		0.866f,
		153f / 160f,
		0.975f,
		0.15f
	};

	private SkeletonState currentSkeletonState;

	private readonly DrinkCombination juiceCombination1 = new DrinkCombination
	{
		Item1 = Drink.Juice,
		Item2 = Drink.Wine
	};

	private readonly DrinkCombination juiceCombination2 = new DrinkCombination
	{
		Item1 = Drink.Juice,
		Item2 = Drink.Grog
	};

	private readonly DrinkCombination wineCombination1 = new DrinkCombination
	{
		Item1 = Drink.Wine,
		Item2 = Drink.Grog
	};

	private readonly DrinkCombination wineCombination2 = new DrinkCombination
	{
		Item1 = Drink.Wine,
		Item2 = Drink.Beer
	};

	private readonly DrinkCombination beerCombination1 = new DrinkCombination
	{
		Item1 = Drink.Beer,
		Item2 = Drink.Juice
	};

	private readonly DrinkCombination beerCombination2 = new DrinkCombination
	{
		Item1 = Drink.Beer,
		Item2 = Drink.Grog
	};

	private readonly DrinkCombination wineBeer = new DrinkCombination
	{
		Item1 = Drink.Wine,
		Item2 = Drink.Beer
	};

	private DrinkCombination[] currentDrink = new DrinkCombination[5]
	{
		new DrinkCombination
		{
			Item1 = Drink.None,
			Item2 = Drink.None
		},
		new DrinkCombination
		{
			Item1 = Drink.None,
			Item2 = Drink.None
		},
		new DrinkCombination
		{
			Item1 = Drink.None,
			Item2 = Drink.None
		},
		new DrinkCombination
		{
			Item1 = Drink.None,
			Item2 = Drink.None
		},
		new DrinkCombination
		{
			Item1 = Drink.None,
			Item2 = Drink.None
		}
	};

	private DrinkEffect[] drinkEffect = new DrinkEffect[5]
	{
		DrinkEffect.None,
		DrinkEffect.None,
		DrinkEffect.None,
		DrinkEffect.None,
		DrinkEffect.None
	};

	[DontSave]
	private EventInstance skeletonSoundInstance;

	[DontSave]
	private EventInstance skeletonDrinkSoundInstance;

	[DontSave]
	private EventInstance pourDrinkSoundInstance;

	private List<bool> exitKeysSolved = new List<bool> { false, false, false, false };

	private readonly List<int> exitKey3Solutions = new List<int> { 23, 23, 0, 23, 0, 0 };

	private readonly List<(float, float)> exitKey3AngleLimits = new List<(float, float)>
	{
		(0f, 290f),
		(70f, 115f),
		(50f, 160f),
		(-105f, 105f),
		(256f, 0f),
		(100f, 0f)
	};

	private int exitKey3CurrentStage;

	private readonly int[] fishBoxSolution = new int[4] { 2, 8, 7, 9 };

	private readonly int[] turnablesSolution = new int[4] { 2, 2, 5, 4 };

	private readonly int[,] portraitSolutions = new int[4, 2]
	{
		{ 5, 2 },
		{ 1, 4 },
		{ 2, 1 },
		{ 4, 5 }
	};

	private readonly int[] cheaterSolution = new int[3] { 2, 1, 3 };

	[Header("Generated Variables")]
	[DontSave]
	public GameObject portraitsGem;

	[DontSave]
	public GameObject rickyHint;

	[DontSave]
	public GameObject catchGem;

	[DontSave]
	public GameObject menuHint;

	[DontSave]
	public GameObject ordersHint;

	[DontSave]
	public GameObject[] drummerTeeth;

	[DontSave]
	public Ref<GameObject, Switch3D> endPlane;

	[DontSave]
	public TweenState battleshipWinDrawer;

	[DontSave]
	public Ref<GameObject, Sequence> mapOverlaySequence;

	[DontSave]
	public MaterialState mapEffect;

	[DontSave]
	public MaterialState mapEffectWave;

	[DontSave]
	public MaterialState mapOverlay;

	[DontSave]
	public MaterialState[] mapDrawings;

	[DontSave]
	public Slot[] mapSlots;

	[DontSave]
	public Zoomable battleshipZoom;

	[DontSave]
	public Turnable[] cheaterTurnables;

	[DontSave]
	public Switch3D cheaterChestLid;

	[DontSave]
	public RefArray<Item, Transform> cheaterDice;

	[DontSave]
	public GameObject barRespawn;

	[DontSave]
	public Zoomable compassZoom;

	[DontSave]
	public TweenState[] drummerPlaySoundTween;

	[DontSave]
	public TweenState[] drummerTeethTweens;

	[DontSave]
	public TweenState gemsLockedShipTween;

	[DontSave]
	public TweenState[] shipTweens1;

	[DontSave]
	public TweenState[] shipTweens2;

	[DontSave]
	public TweenState[] shipTweens3;

	[DontSave]
	public TweenState[] shipTweens4;

	[DontSave]
	public TweenState[] shipTweens5;

	[DontSave]
	public TweenState[] shipTweens6;

	[DontSave]
	public TweenState[] shipTweens7;

	[DontSave]
	public TweenState barDoor;

	[DontSave]
	public TweenState[] barDoorLocks;

	[DontSave]
	public Item[] compassItems;

	[DontSave]
	public Slot[] drummerSkullSolution;

	[DontSave]
	public Slot[] skullToothSlots;

	[DontSave]
	public TweenState monkeyAnimation;

	[DontSave]
	public GameObject monkey;

	[DontSave]
	public Switch3D monkeyKey;

	[DontSave]
	public MaterialState[] gemPieceSymbols;

	[DontSave]
	public TweenState exitDoor;

	[DontSave]
	public Zoomable key3Zoom;

	[DontSave]
	public Slot[] exitKeySlots;

	[DontSave]
	public Sequence exitKey3TurnableMover;

	[DontSave]
	public Turnable exitKey3Turnable;

	[DontSave]
	public Slot[] gemSlots;

	[DontSave]
	public Zoomable gemsLockerZoom;

	[DontSave]
	public Item[] gems;

	[DontSave]
	public SlidableGraph gemSlidableGraph;

	[DontSave]
	public SlidableGraphPiece gemSlidablePiece;

	[DontSave]
	public TweenState gemsLockerDoor;

	[DontSave]
	public TweenState[] portraitDrawers;

	[DontSave]
	public Turnable[] portraitTurnablesLeft;

	[DontSave]
	public Turnable[] portraitTurnablesRight;

	[DontSave]
	public Transform[] portraitCenters;

	[DontSave]
	public Switch3D fishBoxLid;

	[DontSave]
	public Turnable[] fishBoxTurnables;

	[DontSave]
	public TweenState drummerSkullTween;

	[DontSave]
	public Zoomable drummerZoomable;

	[DontSave]
	public Switch3D skullDrummerLever;

	[DontSave]
	public TweenState playSoundTime;

	[DontSave]
	public Item poemHint;

	[DontSave]
	public Switch3D[] battleshipButtons;

	[DontSave]
	public Item[] exitKeys;

	[DontSave]
	public TweenState dominoKeyDoor;

	[DontSave]
	public JigsawPiece[] dominoPieces;

	[DontSave]
	public Jigsaw dominoJigsaw;

	[DontSave]
	public GameObject dominoJigsawCollider;

	[DontSave]
	public Zoomable dominoBoardZoom;

	[DontSave]
	public Slot[] compassSlots;

	[DontSave]
	public Dial[] dominoCompassDials;

	[DontSave]
	public Slot storageSlot;

	[DontSave]
	public Switch3D storageDoor;

	[DontSave]
	public Item storageKey;

	[DontSave]
	public TweenState lanternsKeyDoorTweenState;

	[DontSave]
	public Sequence lanternsKeySequence;

	[DontSave]
	public Switch3D lanternsPulley;

	[DontSave]
	public Transform[] verticalJigsawSnaps;

	[DontSave]
	public Transform[] horizontalJigsawSnaps;

	[DontSave]
	public Ref<TweenState, Switch3D> chestHandle1;

	[DontSave]
	public Ref<TweenState, Switch3D> chestHandle2;

	[DontSave]
	public Ref<TweenState, Switch3D> chestOpen1;

	[DontSave]
	public Ref<TweenState, Switch3D> chestOpen2;

	[DontSave]
	public Item gemsHint;

	[DontSave]
	public Turnable[] portraitLockTurnables;

	[DontSave]
	public Lock portraitLock;

	[DontSave]
	public Zoomable portraitLockZoomable;

	[DontSave]
	public Item portraitLockItem;

	[DontSave]
	public TweenState portraitLockTs;

	[DontSave]
	public Slot[] diceSlots;

	[DontSave]
	public Transform[] cheaterDiceWeights;

	[DontSave]
	public Ref<Sequence, Switch3D> openPortaitsChest;

	[DontSave]
	public Slot[] mugSlots;

	[DontSave]
	public Switch3D[] barrelSwitches;

	[DontSave]
	public TweenState[] waterFlowEffect;

	[DontSave]
	public ParticleSystem[] ripples;

	[DontSave]
	public RefArray<ParticleSystem, GameObject> bubbles;

	[DontSave]
	public RefArray<TweenState, GameObject> foam3D;

	[DontSave]
	public RefArray<ParticleSystem, GameObject> vapour;

	[DontSave]
	public TweenState[] mugFuildAnims;

	[DontSave]
	public Slot removeDrinkSlot;

	[DontSave]
	public Ref<GameObject, TweenState> removeDrinkAnim;

	[DontSave]
	public ParticleSystem removeDrinkEffect;

	[DontSave]
	public ParticleSystem removeDrinkEffect2;

	[DontSave]
	public GameObject[] mugItems;

	[DontSave]
	public Npc skeletonNpc1;

	[DontSave]
	public Npc skeletonNpc2;

	[DontSave]
	public Npc skeletonNpc3;

	[DontSave]
	public TweenState skeletonTs;

	[DontSave]
	public Slot skeletonHandSlot;

	[DontSave]
	public Item skeletonHandKey;

	[DontSave]
	public ParticleSystem SkeletonDrinkAnim;

	[DontSave]
	public Item barHint;

	[DontSave]
	public Item[] barPlates;

	[DontSave]
	public Item fishBox;

	[DontSave]
	public Item[] mugs;

	[DontSave]
	public Item[] backBottles;

	[DontSave]
	public Item diceBox;

	[DontSave]
	public Switch3D[] tableLanternsPulley;

	[DontSave]
	public TweenState[] tableLanternsSides;

	private int[] tableLanternsCurrent = new int[4] { 0, 2, 1, 3 };

	[DontSave]
	private int[] tableLanternsCorrect = new int[4] { 2, 1, 3, 0 };

	[DontSave]
	public TableLanternSideNumbers[] tableLanternsSideNumbers;

	[DontSave]
	public RefArray<GameObject, Item, Transform, TweenState> food;

	[DontSave]
	public ParticleSystem foodVFXInventory;

	[DontSave]
	public Transform foodVFXTransformInventory;

	public override void onRPCCalled(int type)
	{
		if (type == 2)
		{
			solveStorageDoor();
		}
		if (type == 3)
		{
			solveLanterns();
		}
		if (type == 4)
		{
			wrongLanternsSolution();
		}
		if (type == 5)
		{
			solveCompass();
		}
		if (type == 6)
		{
			solveDominoes();
		}
		if (type == 7)
		{
			solveBattleship();
		}
		if (type == 8)
		{
			solveDrummerSkull();
		}
		if (type == 9)
		{
			skullDrummerLever.targetable = true;
			if (skullDrummerLever.state == Switch3DState.On)
			{
				game.startSwitch(skullDrummerLever);
			}
			playSoundTime.transitionTo("NewState", 2f, 0f);
			Slot[] array = skullToothSlots;
			foreach (Slot slot in array)
			{
				slot.targetable = true;
				if (slot.insertedItem != null)
				{
					slot.insertedItem.targetable = true;
				}
			}
		}
		if (type == 10)
		{
			game.startTimer(new FishBoxLockDelayTimer(), 0.5f);
		}
		if (type == 11)
		{
			solveGemShip();
		}
		if (type == 12)
		{
			solveCheaterDice();
		}
		if (type == 13)
		{
			solveExitKeys();
		}
		if (type == 0)
		{
			solveSkeleton();
		}
		if (type == 1)
		{
			skeletonFail();
		}
	}

	private void skeletonFail()
	{
		juiceDrinkDone = false;
		grogDrinkDone = false;
		beerWineDrinkDone = false;
		currentSkeletonState = SkeletonState.Reset1;
		skeletonTs.setWeight("NewState", 0.95f);
		skeletonTs.transitionTo("NewState", skeletonAnimSpeed, skeletonAnimTime[(int)currentSkeletonState]);
		PineFmod.start(skeletonSoundInstance);
		skeletonHandSlot.targetable = true;
		skeletonHandSlot.insertedItem.targetable = true;
	}

	private void solveSkeleton()
	{
		game.increaseZoomCounter(skeletonNpc2);
		skeletonNpc2.targetable = false;
		skeletonNpc3.targetable = true;
		skeletonHandKey.targetable = true;
		currentSkeletonState++;
		skeletonTs.transitionTo("NewState", skeletonAnimSpeed, skeletonAnimTime[(int)currentSkeletonState]);
		PineFmod.start(skeletonSoundInstance);
		game.finishPuzzle(Puzzle.Skeleton);
		skeletonHandSlot.targetable = false;
		skeletonHandSlot.insertedItem.targetable = false;
		skeletonHandSlot.targetable = false;
		skeletonHandSlot.insertedItem.targetable = false;
	}

	public override void onInit()
	{
		initDominos();
		initGemShip();
		skeletonTs.transitionTo("NewState", skeletonAnimSpeed, 0.125f);
		battleshipFoundTweens[0] = shipTweens1;
		battleshipFoundTweens[1] = shipTweens2;
		battleshipFoundTweens[2] = shipTweens3;
		battleshipFoundTweens[3] = shipTweens4;
		battleshipFoundTweens[4] = shipTweens5;
		battleshipFoundTweens[5] = shipTweens6;
		battleshipFoundTweens[6] = shipTweens7;
		for (int i = 0; i < mugItems.Length; i++)
		{
			if (!game.isInAnyPlayerInventory(mugItems[i]))
			{
				mugItems[i].SetActive(i < game.netPlayers.Count + 2);
			}
		}
		mapOverlay.setWeight("Off");
		skeletonSoundInstance = PineFmod.createInstance("event:/Sound Effects/05 Misc/Gore/Skeleton_Creak_Gore");
		PineFmod.set3DAttributes(skeletonSoundInstance, PineFmod.to3DAttributes(skeletonNpc1.transform));
		skeletonDrinkSoundInstance = PineFmod.createInstance("event:/Sound Effects/05 Misc/Gore/Skeleton_Drink");
		PineFmod.set3DAttributes(skeletonDrinkSoundInstance, PineFmod.to3DAttributes(skeletonNpc1.transform));
		pourDrinkSoundInstance = PineFmod.createInstance("event:/Sound Effects/03 Interactable/Levers/Bathtub/Bathtub_Loop");
		PineFmod.set3DAttributes(pourDrinkSoundInstance, PineFmod.to3DAttributes(removeDrinkSlot.transform));
		for (int j = 0; j < tableLanternsSideNumbers.Length; j++)
		{
			GameObject[] numbers = tableLanternsSideNumbers[j].numbers;
			for (int k = 0; k < numbers.Length; k++)
			{
				numbers[k].SetActive(value: false);
			}
			for (int l = 0; l < 4; l++)
			{
				tableLanternsSideNumbers[j].numbers[4 * l + tableLanternsCurrent[j]].SetActive(value: true);
			}
			if (tableLanternsCurrent[j] % 2 == 1)
			{
				tableLanternsSides[j].transitionTo((tableLanternsSides[j].getWeight("Down") < 0.1f) ? "Down" : "Default", 999f);
			}
		}
	}

	public override void onUpdate()
	{
		for (int i = 0; i < drinkEffect.Length; i++)
		{
			if (drinkEffect[i] == DrinkEffect.None)
			{
				turnOffDrinkParticles(i);
			}
			else
			{
				turnOnDrinkParticles(drinkEffect[i], i);
			}
		}
		onCheaterDiceUpdate();
		for (int j = 0; j < compassSlots.Length; j++)
		{
			compassSlots[j].transform.rotation = dominoCompassDials[j].transform.rotation;
		}
	}

	public override void onAddToInventory(Item item)
	{
		int num = Array.IndexOf(mugItems, item);
		if (num != -1 && drinkEffect[num] == DrinkEffect.None)
		{
			bubbles[num].Get<GameObject>(0f).SetActive(value: false);
			foam3D[num].Get<GameObject>(0f).SetActive(value: false);
			foam3D[num].Get<TweenState>(0).setWeight("On", 0f);
			vapour[num].Get<GameObject>(0f).SetActive(value: false);
		}
		if (item == compassItems[1])
		{
			dominoBoardZoom.targetable = false;
		}
	}

	public override void onUnlock(Lock targetLock)
	{
		if (targetLock == portraitLock)
		{
			solvePortraitChest();
		}
	}

	public override void onSwitch3D(Switch3D targetSwitch, Switch3DEvent switchEvent)
	{
		int num = Array.IndexOf(barrelSwitches, targetSwitch);
		if (num >= 0 && switchEvent == Switch3DEvent.Start && targetSwitch.state == Switch3DState.Off)
		{
			Slot slot = mugSlots[num];
			slot.targetable = false;
			if (slot.insertedItem != null)
			{
				slot.insertedItem.targetable = false;
				int num2 = Array.IndexOf(mugItems, mugSlots[num].insertedItem.gameObject);
				if (currentDrink[num2].Item1 == Drink.None)
				{
					mugFuildAnims[num2].transitionTo("NewState", 0.4f, 0.5f, playSound: true, 0.5f);
					currentDrink[num2].Item1 = (Drink)num;
				}
				else
				{
					mugFuildAnims[num2].transitionTo("NewState", 0.1f, 1f, playSound: true, 0.5f);
					currentDrink[num2].Item2 = (Drink)num;
				}
			}
			else
			{
				ripples[num].Play();
			}
			waterFlowEffect[num].transitionTo("Open");
			barrelSwitches[num].targetable = false;
		}
		if (targetSwitch == chestHandle1.Get<Switch3D>(0f) && switchEvent == Switch3DEvent.On)
		{
			chestHandle1.Get<Switch3D>(0f).targetable = false;
			chestOpen1.Get<TweenState>(0).transitionTo("Down", 1f, 0.2f);
			chestOpen1.Get<Switch3D>(0f).targetable = true;
		}
		if (targetSwitch == chestHandle2.Get<Switch3D>(0f) && switchEvent == Switch3DEvent.On)
		{
			chestHandle2.Get<Switch3D>(0f).targetable = false;
			chestOpen2.Get<TweenState>(0).transitionTo("Down", 1f, 0.2f);
			chestOpen2.Get<Switch3D>(0f).targetable = true;
		}
		int num3 = Array.IndexOf(battleshipButtons, targetSwitch);
		if ((switchEvent == Switch3DEvent.Off || switchEvent == Switch3DEvent.On || switchEvent == Switch3DEvent.Start) && num3 >= 0)
		{
			onBattleshipButton(num3, switchEvent == Switch3DEvent.Start);
		}
		if (targetSwitch == monkeyKey)
		{
			switch (switchEvent)
			{
			case Switch3DEvent.Start:
				monkeyKey.targetable = false;
				game.startTimer(new MonkeyCymbalTimer(0), monkeyCymbalTimeStamps[0]);
				break;
			case Switch3DEvent.On:
				monkeyAnimation.setState("NewState", 0f);
				monkeyKey.targetable = true;
				break;
			}
		}
		if (targetSwitch == endPlane.Get<Switch3D>(0f) && switchEvent == Switch3DEvent.Start)
		{
			game.levelCompleted();
		}
		if (targetSwitch == lanternsPulley && switchEvent == Switch3DEvent.On)
		{
			onLanternPulley();
		}
		if (targetSwitch == skullDrummerLever && switchEvent == Switch3DEvent.On)
		{
			onDrummerSkullLever();
		}
		if (targetSwitch == cheaterChestLid && switchEvent == Switch3DEvent.Start && !exitKeys[0].targetable && exitKeys[0].slot == null)
		{
			exitKeys[0].targetable = true;
			game.changeExamineRotation(diceBox.gameObject, new Vector2(0f, 40f), 0.5f);
		}
		if (targetSwitch == fishBoxLid && switchEvent == Switch3DEvent.Start && !gemsHint.targetable)
		{
			gemsHint.targetable = true;
			Item[] array = gems;
			foreach (Item item in array)
			{
				if (item.slot == null)
				{
					item.targetable = true;
				}
			}
			game.changeExamineRotation(fishBox.gameObject, new Vector2(0f, 0f), 0.5f);
		}
		int num4 = Array.IndexOf(tableLanternsPulley, targetSwitch);
		if (num4 >= 0)
		{
			switch (switchEvent)
			{
			case Switch3DEvent.On:
			{
				targetSwitch.targetable = false;
				game.startSwitch(targetSwitch);
				tableLanternsSides[num4].transitionTo((tableLanternsSides[num4].getWeight("Down") < 0.1f) ? "Down" : "Default", 2f);
				GameObject[] numbers = tableLanternsSideNumbers[num4].numbers;
				for (int i = 0; i < numbers.Length; i++)
				{
					numbers[i].SetActive(value: false);
				}
				for (int j = 0; j < 4; j++)
				{
					tableLanternsSideNumbers[num4].numbers[4 * j + tableLanternsCurrent[num4]].SetActive(value: true);
				}
				tableLanternsCurrent[num4] = (tableLanternsCurrent[num4] + 1) % 4;
				for (int k = 0; k < 4; k++)
				{
					tableLanternsSideNumbers[num4].numbers[4 * k + tableLanternsCurrent[num4]].SetActive(value: true);
				}
				break;
			}
			case Switch3DEvent.Off:
				targetSwitch.targetable = true;
				break;
			}
		}
		for (int l = 0; l < food.Length; l++)
		{
			if (targetSwitch.transform.parent.gameObject == food[l].Get<GameObject>(0) && switchEvent == Switch3DEvent.Start)
			{
				if (food[l].Get<TweenState>((short)0) != null)
				{
					food[l].Get<TweenState>((short)0).transitionTo("NewState", 4f);
				}
				else
				{
					OnFoodEaten(l);
				}
			}
		}
	}

	public override void onTweenTransitionDone(TweenState tweenState, string state)
	{
		if (tweenState == skeletonTs)
		{
			PineFmod.stop(skeletonSoundInstance, STOP_MODE.IMMEDIATE);
			if (currentSkeletonState == SkeletonState.Chilling)
			{
				skeletonTs.setWeight("NewState", 0f);
				skeletonTs.transitionTo("NewState", skeletonAnimSpeed, skeletonAnimTime[(int)currentSkeletonState]);
			}
			if (currentSkeletonState == SkeletonState.Reset2)
			{
				skeletonTs.setWeight("NewState", 0.25f);
				currentSkeletonState = SkeletonState.WantDrink;
			}
			if (currentSkeletonState == SkeletonState.Drink1 || currentSkeletonState == SkeletonState.Drink2 || currentSkeletonState == SkeletonState.Drink3)
			{
				int num = Array.IndexOf(mugItems, skeletonHandSlot.insertedItem.gameObject);
				if (checkDrinkCombination(juiceCombination1, num) || checkDrinkCombination(juiceCombination2, num))
				{
					juiceDrinkDone = true;
				}
				if (currentDrink[num].Item1 == Drink.Grog && currentDrink[num].Item2 == Drink.Grog)
				{
					grogDrinkDone = true;
				}
				if (checkDrinkCombination(wineBeer, num))
				{
					beerWineDrinkDone = true;
				}
				if (currentSkeletonState == SkeletonState.Drink3)
				{
					if (juiceDrinkDone && grogDrinkDone && beerWineDrinkDone)
					{
						game.callRPC(RPCType.Skeleton);
					}
					else
					{
						game.callRPC(RPCType.SkeletonFail);
					}
				}
				else
				{
					skeletonHandSlot.targetable = true;
					skeletonHandSlot.insertedItem.targetable = true;
				}
				currentDrink[num] = new DrinkCombination
				{
					Item1 = Drink.None,
					Item2 = Drink.None
				};
			}
		}
		if (tweenState == removeDrinkAnim.Get<TweenState>(0f))
		{
			removeDrinkAnim.Get<GameObject>(0).SetActive(value: false);
			removeDrinkSlot.targetable = true;
			removeDrinkSlot.insertedItem.targetable = true;
			PineFmod.stop(pourDrinkSoundInstance, STOP_MODE.ALLOWFADEOUT);
			int num2 = Array.IndexOf(mugItems, removeDrinkSlot.insertedItem.gameObject);
			currentDrink[num2] = new DrinkCombination
			{
				Item1 = Drink.None,
				Item2 = Drink.None
			};
			mugItems[num2].SetActive(value: true);
			mugFuildAnims[num2].setState("NewState", 0f);
			mugFuildAnims[num2].transitionTo("NewState", 1f, 0f);
		}
		int num3 = Array.IndexOf(waterFlowEffect, tweenState);
		if (num3 != -1)
		{
			if (tweenState.getWeight("Open") == 0f)
			{
				if (state == "Close")
				{
					waterFlowEffect[num3].setWeight("Open", 0f);
					waterFlowEffect[num3].setWeight("Close", 0f);
					Slot slot = mugSlots[num3];
					slot.targetable = true;
					if (slot.insertedItem != null)
					{
						slot.insertedItem.targetable = true;
					}
				}
			}
			else
			{
				game.startTimer(new PourDrinksTimer(num3), 1f);
			}
		}
		if (tweenState == portraitLockTs)
		{
			portraitLockItem.targetable = true;
			portraitLockItem.hasRigidbody = true;
			openPortaitsChest.Get<Sequence>(0).play(-1f, 0.3f, 0.6f);
			openPortaitsChest.Get<Switch3D>(0f).targetable = true;
		}
		TweenState.TweenStateRecord tweenStateRecord = tweenState.findStateByName(state);
		if (tweenStateRecord == null)
		{
			return;
		}
		int num4 = Array.IndexOf(drummerTeethTweens, tweenState);
		if (num4 >= 0)
		{
			if (tweenStateRecord.targetWeight > 0.9f)
			{
				onDrummerToothClick(num4);
			}
			if (tweenStateRecord.targetWeight < 0.1f && num4 == drummerTeethTweens.Length - 1)
			{
				onDrummerFinalToothReturned();
			}
		}
		if (Array.IndexOf(drummerPlaySoundTween, tweenState) >= 0)
		{
			onDrummerSoundTween(tweenState);
		}
		for (int i = 0; i < food.Length; i++)
		{
			if (tweenState == food[i].Get<TweenState>((short)0))
			{
				OnFoodEaten(i);
			}
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

	public override void onMaterialTransitionDone(MaterialState tweenState, string state)
	{
		if (tweenState == mapOverlay && !mapOverlaySequence.Get<GameObject>(0).activeInHierarchy)
		{
			mapOverlaySequence.Get<GameObject>(0).SetActive(value: true);
			mapOverlaySequence.Get<Sequence>(0f).play();
		}
	}

	public override void onTimerUpdate(Timer timer)
	{
		if (timer is PaintingCenterTimer paintingCenterTimer)
		{
			if (paintingCenterTimer.startRot == paintingCenterTimer.targetRot)
			{
				portraitCenters[paintingCenterTimer.index].localRotation = Quaternion.Euler(0f, 0f, Mathf.Lerp(paintingCenterTimer.startRot, paintingCenterTimer.targetRot + 360f, paintingCenterTimer.unitTime));
			}
			else
			{
				portraitCenters[paintingCenterTimer.index].localRotation = Quaternion.Euler(0f, 0f, Mathf.LerpAngle(paintingCenterTimer.startRot, paintingCenterTimer.targetRot, paintingCenterTimer.unitTime));
			}
		}
	}

	private bool checkDrinkCombination(DrinkCombination combination, int mugIndex)
	{
		if ((currentDrink[mugIndex].Item1 == combination.Item1 && currentDrink[mugIndex].Item2 == combination.Item2) || (currentDrink[mugIndex].Item1 == combination.Item2 && currentDrink[mugIndex].Item2 == combination.Item1))
		{
			return true;
		}
		return false;
	}

	private void turnOnDrinkParticles(DrinkEffect effect, int mugIndex)
	{
		if (effect == DrinkEffect.Bubbles && !bubbles[mugIndex].Get<ParticleSystem>(0).isPlaying)
		{
			bubbles[mugIndex].Get<GameObject>(0f).SetActive(value: true);
			bubbles[mugIndex].Get<ParticleSystem>(0).Play();
			ParticleSystem.MainModule main = bubbles[mugIndex].Get<ParticleSystem>(0).main;
			main.simulationSpeed = 0.3f;
			main.playOnAwake = true;
			main.prewarm = true;
		}
		else if (effect == DrinkEffect.Foam && !foam3D[mugIndex].Get<GameObject>(0f).activeSelf)
		{
			foam3D[mugIndex].Get<GameObject>(0f).SetActive(value: true);
			foam3D[mugIndex].Get<TweenState>(0).transitionTo("On");
		}
		else if (effect == DrinkEffect.Vapour && !vapour[mugIndex].Get<ParticleSystem>(0).isPlaying)
		{
			vapour[mugIndex].Get<GameObject>(0f).SetActive(value: true);
			vapour[mugIndex].Get<ParticleSystem>(0).Play();
			ParticleSystem.MainModule main2 = vapour[mugIndex].Get<ParticleSystem>(0).main;
			main2.simulationSpeed = 0.3f;
			main2.playOnAwake = true;
			main2.prewarm = true;
		}
	}

	private void turnOffDrinkParticles(int mugIndex)
	{
		if (bubbles[mugIndex].Get<ParticleSystem>(0).isPlaying)
		{
			ParticleSystem.MainModule main = bubbles[mugIndex].Get<ParticleSystem>(0).main;
			main.simulationSpeed = 0.9f;
			main.playOnAwake = false;
			main.prewarm = false;
			bubbles[mugIndex].Get<ParticleSystem>(0).Stop();
		}
		if (foam3D[mugIndex].Get<GameObject>(0f).activeSelf)
		{
			foam3D[mugIndex].Get<GameObject>(0f).SetActive(value: false);
			foam3D[mugIndex].Get<TweenState>(0).setWeight("On", 0f);
		}
		if (vapour[mugIndex].Get<ParticleSystem>(0).isPlaying)
		{
			ParticleSystem.MainModule main2 = vapour[mugIndex].Get<ParticleSystem>(0).main;
			main2.simulationSpeed = 10f;
			main2.playOnAwake = false;
			main2.prewarm = false;
			vapour[mugIndex].Get<ParticleSystem>(0).Stop();
		}
	}

	public override void onTimerDone(Timer timer)
	{
		if (timer is StartDrinkTimer)
		{
			int num = Array.IndexOf(mugItems, skeletonHandSlot.insertedItem.gameObject);
			drinkEffect[num] = DrinkEffect.None;
			if (mugFuildAnims[num].getWeight("NewState") != 0f)
			{
				SkeletonDrinkAnim.Play();
				PineFmod.start(skeletonDrinkSoundInstance);
				game.startTimer(new EndDrinkTimer(), 2.5f);
			}
			mugFuildAnims[num].transitionTo("NewState", 2f, 0f);
		}
		if (timer is EndDrinkTimer)
		{
			PineFmod.stop(skeletonDrinkSoundInstance, STOP_MODE.IMMEDIATE);
		}
		if (timer is PourDrinksTimer pourDrinksTimer)
		{
			waterFlowEffect[pourDrinksTimer.index].transitionTo("Close");
			game.startSwitch(barrelSwitches[pourDrinksTimer.index]);
			barrelSwitches[pourDrinksTimer.index].targetable = true;
			if (mugSlots[pourDrinksTimer.index].insertedItem != null)
			{
				int num2 = Array.IndexOf(mugItems, mugSlots[pourDrinksTimer.index].insertedItem.gameObject);
				if (currentDrink[num2].Item2 != Drink.None)
				{
					if (checkDrinkCombination(juiceCombination1, num2) || checkDrinkCombination(juiceCombination2, num2))
					{
						drinkEffect[num2] = DrinkEffect.Bubbles;
					}
					if (checkDrinkCombination(wineCombination1, num2) || checkDrinkCombination(wineCombination2, num2))
					{
						drinkEffect[num2] = DrinkEffect.Foam;
					}
					if (checkDrinkCombination(beerCombination1, num2) || checkDrinkCombination(beerCombination2, num2))
					{
						drinkEffect[num2] = DrinkEffect.Vapour;
					}
				}
			}
		}
		if (timer is DiceMachineTimer diceMachineTimer)
		{
			diceSlots[diceMachineTimer.index].targetable = true;
			cheaterDice[diceMachineTimer.index].Get<Item>(0).targetable = true;
		}
		if (timer is FishBoxLockDelayTimer)
		{
			solveFishBox();
		}
		else if (timer is LanternsTimer lanternsTimer)
		{
			onLanternsTimerDone(lanternsTimer.index);
		}
		else if (timer is CompassTimer)
		{
			onCompassTimerDone();
		}
		else if (timer is BattleshipTimer battleshipTimer)
		{
			onBattleshipTimer(battleshipTimer.count);
		}
		else if (timer is DrummerSkullLeverTimer drummerSkullLeverTimer)
		{
			onDrummerSkullLeverTimer(drummerSkullLeverTimer.beforeFor, drummerSkullLeverTimer.inFor, drummerSkullLeverTimer.index);
		}
		else if (timer is DrummerToothClickTimer drummerToothClickTimer)
		{
			onDrummerToothClickTimerDone(drummerToothClickTimer.index);
		}
		else if (timer is GemShipTimer)
		{
			onGemShipTimerDone();
		}
		else if (timer is ExitKeysTimer timer2)
		{
			onExitKeysTimerDone(timer2);
		}
		else if (timer is PaintingCenterTimer paintingCenterTimer)
		{
			portraitCenters[paintingCenterTimer.index].localRotation = Quaternion.Euler(0f, 0f, paintingCenterTimer.targetRot);
		}
		else if (timer is MonkeyCymbalTimer monkeyCymbalTimer)
		{
			PineFmod.playOneShotSoundAttached("event:/Sound Effects/04 Items/Metal/Cymbals/Monkey_Cymbals", monkeyAnimation.gameObject);
			int num3 = monkeyCymbalTimer.timeStampIndex + 1;
			if (num3 < monkeyCymbalTimeStamps.Length)
			{
				float duration = monkeyCymbalTimeStamps[num3] - monkeyCymbalTimeStamps[monkeyCymbalTimer.timeStampIndex];
				game.startTimer(new MonkeyCymbalTimer(num3), duration);
			}
		}
		else if (timer is MapDelayTimer)
		{
			mapEffect.transitionTo("Faded", 0.5f);
		}
	}

	public override void onSequenceDone(Sequence sequence)
	{
		if (sequence == exitKey3TurnableMover)
		{
			onKey3SeqDone();
		}
	}

	public override void onSlot(Slot targetSlot)
	{
		if (targetSlot == skeletonHandSlot)
		{
			currentSkeletonState++;
			skeletonTs.transitionTo("NewState", skeletonAnimSpeed, skeletonAnimTime[(int)currentSkeletonState]);
			PineFmod.start(skeletonSoundInstance);
			skeletonHandSlot.targetable = false;
			skeletonHandSlot.insertedItem.targetable = false;
			game.startTimer(new StartDrinkTimer(), 0.9f);
		}
		if (removeDrinkSlot == targetSlot)
		{
			targetSlot.targetable = false;
			targetSlot.insertedItem.targetable = false;
			int num = Array.IndexOf(mugItems, removeDrinkSlot.insertedItem.gameObject);
			targetSlot.ejectDuration = mugFuildAnims[num].getWeight("NewState") + 0.001f;
			removeDrinkAnim.Get<GameObject>(0).SetActive(value: true);
			removeDrinkAnim.Get<TweenState>(0f).setWeight("NewState", 1.001f - mugFuildAnims[num].getWeight("NewState"));
			removeDrinkAnim.Get<TweenState>(0f).transitionTo("NewState");
			if (currentDrink[num].Item1 != Drink.None)
			{
				removeDrinkEffect.Play();
				removeDrinkEffect2.Play();
				PineFmod.start(pourDrinkSoundInstance);
			}
			mugItems[num].SetActive(value: false);
			drinkEffect[num] = DrinkEffect.None;
			bubbles[num].Get<GameObject>(0f).SetActive(value: false);
			foam3D[num].Get<GameObject>(0f).SetActive(value: false);
			foam3D[num].Get<TweenState>(0).setWeight("On", 0f);
			vapour[num].Get<GameObject>(0f).SetActive(value: false);
		}
		int num2 = Array.IndexOf(mugSlots, targetSlot);
		if (num2 >= 0)
		{
			int num3 = Array.IndexOf(mugItems, mugSlots[num2].insertedItem.gameObject);
			if (currentDrink[num3].Item2 == Drink.None)
			{
				mugSlots[num2].targetable = false;
				mugSlots[num2].insertedItem.targetable = false;
				waterFlowEffect[num2].transitionTo("Open");
				barrelSwitches[num2].targetable = false;
				game.startSwitch(barrelSwitches[num2]);
			}
		}
		int num4 = Array.IndexOf(diceSlots, targetSlot);
		if (num4 >= 0)
		{
			Item item = cheaterDice[num4].Get<Item>(0);
			game.removeItemFromSlot(item);
			targetSlot.targetable = false;
			item.targetable = false;
			item.hasRigidbody = true;
			game.startTimer(new DiceMachineTimer(num4), 0.25f);
		}
		if (targetSlot == storageSlot && targetSlot.isUnlocked)
		{
			game.callRPC(RPCType.StorageDoors);
		}
		if (Array.IndexOf(compassSlots, targetSlot) >= 0)
		{
			checkCompass();
		}
		if (targetSlot == exitKeySlots[0] && targetSlot.isUnlocked)
		{
			solveKey1();
		}
		if (targetSlot == exitKeySlots[1] && targetSlot.isUnlocked)
		{
			solveKey2();
		}
		if (targetSlot == exitKeySlots[2] && targetSlot.isUnlocked)
		{
			onKey3Slot();
		}
		if (targetSlot == exitKeySlots[3] && targetSlot.isUnlocked)
		{
			solveKey4();
		}
		if (Array.Exists(gemSlots, (Slot x) => x == targetSlot))
		{
			onGemShipSlots();
		}
		int num5 = Array.IndexOf(mapSlots, targetSlot);
		if (num5 >= 0)
		{
			if (!mapOverlaySequence.Get<GameObject>(0).activeInHierarchy)
			{
				mapOverlay.transitionTo("Default");
			}
			game.startTimer(new MapDelayTimer(), 0.5f);
			mapEffectWave.setWeight("Final", 0f);
			mapEffectWave.transitionTo("Final", 0.4f);
			mapDrawings[num5].transitionTo("FadeIn", 0.4f);
		}
	}

	public override void onNpcStart(Npc npc)
	{
		if (npc == skeletonNpc1)
		{
			skeletonTs.setState("NewState", 0.1f);
			currentSkeletonState = SkeletonState.NpcEnter;
			skeletonTs.transitionTo("NewState", skeletonAnimSpeed, skeletonAnimTime[(int)currentSkeletonState]);
			PineFmod.start(skeletonSoundInstance);
		}
	}

	public override void onNpcExit(Npc npc)
	{
		if (npc == skeletonNpc1 && skeletonNpc1.targetable)
		{
			currentSkeletonState = SkeletonState.Chilling;
			skeletonTs.transitionTo("NewState", skeletonAnimSpeed, skeletonAnimTime[(int)currentSkeletonState]);
			PineFmod.start(skeletonSoundInstance);
		}
	}

	public override void onNpcChoiceSelected(Npc npc, int lineIndex, int choiceIndex)
	{
		if (npc == skeletonNpc1 || npc == skeletonNpc2 || npc == skeletonNpc3)
		{
			game.increaseZoomCounter(npc);
		}
	}

	public override void onRemoveFromSlot(Slot targetSlot, Item item)
	{
		if (targetSlot == skeletonHandSlot)
		{
			currentSkeletonState++;
			skeletonTs.transitionTo("NewState", skeletonAnimSpeed, skeletonAnimTime[(int)currentSkeletonState]);
			PineFmod.start(skeletonSoundInstance);
		}
	}

	public override void onTurnableMoved(Turnable turnable, MoveEvent moveEvent)
	{
		if (Array.IndexOf(cheaterTurnables, turnable) != -1 && moveEvent == MoveEvent.Snapped)
		{
			int i = 0;
			if (Array.TrueForAll(cheaterTurnables, (Turnable x) => x.value == cheaterSolution[i++]))
			{
				game.callRPC(RPCType.Dice);
			}
		}
		if (Array.IndexOf(fishBoxTurnables, turnable) != -1 && moveEvent == MoveEvent.Snapped)
		{
			int i2 = 0;
			if (Array.TrueForAll(fishBoxTurnables, (Turnable x) => x.value == fishBoxSolution[i2++]))
			{
				game.callRPC(RPCType.Fishing);
			}
		}
		int num = Array.IndexOf(portraitTurnablesRight, turnable);
		int num2 = Array.IndexOf(portraitTurnablesLeft, turnable);
		if (moveEvent == MoveEvent.Snapped && num != -1)
		{
			onPortraitTurnable(num);
		}
		if (moveEvent == MoveEvent.Snapped && num2 != -1)
		{
			onPortraitTurnable(num2);
		}
		if (moveEvent == MoveEvent.Released && turnable == exitKey3Turnable)
		{
			onKey3Turnable();
		}
	}

	public override void onDialMoved(Dial dial, MoveEvent moveEvent)
	{
		if (moveEvent == MoveEvent.Released && Array.IndexOf(dominoCompassDials, dial) >= 0)
		{
			checkCompass();
		}
	}

	public override void onJigsaw(Jigsaw jigsaw, JigsawPiece piece, JigsawEvent jigsawEvent)
	{
		if (jigsaw == dominoJigsaw && checkDominoes())
		{
			game.callRPC(RPCType.Dominos);
		}
	}

	public override void onSlidableGraphArrivedToNode(SlidableGraph.ToNode context)
	{
		if (context.graph == gemSlidableGraph)
		{
			onGemShipOnPoint(context);
		}
	}

	public override void onSlidableGraphReleased(SlidableGraph.OnPieceInteraction context)
	{
		if (context.graph == gemSlidableGraph && checkGemShip())
		{
			game.callRPC(RPCType.SolveGemShip);
		}
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void solveStorageDoor()
	{
		storageDoor.tweenState.transitionTo("Down", 0.6f);
		storageDoor.targetable = true;
		game.finishPuzzle(Puzzle.StorageDoors);
	}

	private void onLanternPulley()
	{
		lanternsPulley.targetable = false;
		if (lanternsPulley.state == Switch3DState.On)
		{
			game.startSwitch(lanternsPulley);
			if (checkLanterns())
			{
				game.callRPC(RPCType.LanternsGood);
			}
			else
			{
				game.callRPC(RPCType.LanternsWrong);
			}
		}
	}

	private bool checkLanterns()
	{
		for (int i = 0; i < tableLanternsCurrent.Length; i++)
		{
			if (tableLanternsCurrent[i] != tableLanternsCorrect[i])
			{
				return false;
			}
		}
		return true;
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void solveLanterns()
	{
		if (!solvedLanterns)
		{
			solvedLanterns = true;
			lanternsKeySequence.play(-1f, 0.5f);
			lanternsKeyDoorTweenState.transitionTo("Open", 2f, 1f, playSound: true, 0.5f);
			game.startTimer(new LanternsTimer(0), 1f);
			game.finishPuzzle(Puzzle.TableNumbers);
		}
	}

	private void wrongLanternsSolution()
	{
		lanternsKeySequence.play(-1f, lanternsKeySequence.sequenceDuration, 1.5f);
		game.startTimer(new LanternsTimer(1), lanternsKeySequence.sequenceDuration / 1.5f);
	}

	private void onLanternsTimerDone(int index)
	{
		switch (index)
		{
		case 0:
			storageKey.targetable = true;
			break;
		case 1:
			lanternsKeySequence.setTime(0f);
			lanternsPulley.targetable = true;
			break;
		}
	}

	[DebugButton(null, Tint.Default, PostClickAction.HideButton, 0, new object[] { })]
	private void debugGetCompassItems()
	{
		Item[] array = compassItems;
		foreach (Item item in array)
		{
			if (item.slot == null)
			{
				item.targetable = true;
				item.gameObject.SetActive(value: true);
				game.addItemToInventory(item.gameObject);
			}
		}
	}

	private void checkCompass()
	{
		bool flag = true;
		Slot[] array = compassSlots;
		foreach (Slot slot in array)
		{
			if (slot.insertedItem == null)
			{
				flag = false;
				break;
			}
			if (slot.insertedItem != slot.acceptItems[0])
			{
				flag = false;
				break;
			}
		}
		if (flag)
		{
			for (int j = 0; j < dominoCompassDials.Length; j++)
			{
				Dial dial = dominoCompassDials[j];
				if (dial.value != compassSolutionA[j] && dial.value != compassSolutionB[j])
				{
					flag = false;
					break;
				}
			}
		}
		if (flag)
		{
			game.callRPC(RPCType.Compass);
		}
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void solveCompass()
	{
		for (int i = 0; i < compassSlots.Length; i++)
		{
			Slot slot = compassSlots[i];
			if (slot.insertedItem != null)
			{
				slot.insertedItem.targetable = false;
			}
			slot.targetable = false;
			dominoCompassDials[i].targetable = false;
		}
		compassZoom.targetable = false;
		game.increaseZoomCounter(compassZoom);
		barRespawn.SetActive(value: false);
		game.startTimer(new CompassTimer(), 0.7f);
		TweenState[] array = barDoorLocks;
		for (int j = 0; j < array.Length; j++)
		{
			array[j].transitionTo("NewState", 1.5f);
		}
		game.increaseZoomCounter(skeletonNpc1);
		skeletonNpc1.targetable = false;
		skeletonNpc2.targetable = true;
		currentSkeletonState = SkeletonState.WantDrink;
		skeletonTs.transitionTo("NewState", skeletonAnimSpeed, skeletonAnimTime[(int)currentSkeletonState]);
		PineFmod.start(skeletonSoundInstance);
		skeletonHandSlot.targetable = true;
		Item[] array2 = barPlates;
		for (int j = 0; j < array2.Length; j++)
		{
			array2[j].targetable = true;
		}
		fishBox.targetable = true;
		array2 = mugs;
		for (int j = 0; j < array2.Length; j++)
		{
			array2[j].targetable = true;
		}
		Turnable[] array3 = fishBoxTurnables;
		for (int j = 0; j < array3.Length; j++)
		{
			array3[j].targetable = true;
		}
		array2 = backBottles;
		for (int j = 0; j < array2.Length; j++)
		{
			array2[j].targetable = true;
		}
		Switch3D[] array4 = barrelSwitches;
		for (int j = 0; j < array4.Length; j++)
		{
			array4[j].targetable = true;
		}
		barHint.targetable = true;
		game.finishPuzzle(Puzzle.BarDoor);
	}

	private void onCompassTimerDone()
	{
		barDoor.transitionTo("Down");
	}

	private void initDominos()
	{
		JigsawPiece[] componentsInChildren = dominoJigsaw.gameObject.GetComponentsInChildren<JigsawPiece>();
		foreach (JigsawPiece jigsawPiece in componentsInChildren)
		{
			Vector3 localPosition = jigsawPiece.transform.localPosition;
			Quaternion localRotation = jigsawPiece.transform.localRotation;
			dominoSolution[jigsawPiece.gameObject] = new Pose(localPosition, localRotation);
		}
	}

	private bool checkDominoes()
	{
		JigsawPiece[] array = dominoPieces;
		foreach (JigsawPiece jigsawPiece in array)
		{
			Transform[] array2 = horizontalJigsawSnaps;
			if (Mathf.Abs(jigsawPiece.transform.localEulerAngles.z) < 0.1f || Mathf.Abs(jigsawPiece.transform.localEulerAngles.z - 180f) < 0.1f || Mathf.Abs(jigsawPiece.transform.localEulerAngles.z + 180f) < 0.1f)
			{
				array2 = verticalJigsawSnaps;
			}
			Transform transform = null;
			float num = float.PositiveInfinity;
			Transform[] array3 = array2;
			foreach (Transform transform2 in array3)
			{
				float sqrMagnitude = (transform2.position - jigsawPiece.transform.position).sqrMagnitude;
				if (sqrMagnitude < num)
				{
					transform = transform2;
					num = sqrMagnitude;
				}
			}
			jigsawPiece.transform.position = transform.transform.position;
		}
		array = dominoPieces;
		foreach (JigsawPiece jigsawPiece2 in array)
		{
			_ = dominoSolution[jigsawPiece2.gameObject];
			float num2 = Vector3.Distance(jigsawPiece2.transform.position, jigsawPiece2.originalPosition);
			float num3 = Quaternion.Angle(jigsawPiece2.transform.rotation, jigsawPiece2.originalRotation);
			if (num2 > 0.015f)
			{
				return false;
			}
			if (num3 > 5f)
			{
				return false;
			}
		}
		return true;
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void solveDominoes()
	{
		dominoJigsaw.targetable = false;
		dominoJigsawCollider.SetActive(value: false);
		dominoKeyDoor.transitionTo("Open");
		compassItems[1].targetable = true;
		JigsawPiece[] array = dominoPieces;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = false;
		}
		game.finishPuzzle(Puzzle.Domino);
	}

	private void onBattleshipButton(int buttonIndex, bool isAtStart)
	{
		int index = buttonIndex / 8;
		int index2 = buttonIndex % 8;
		battleshipCurrentState[index][index2] = battleshipButtons[buttonIndex].state == Switch3DState.On;
		if (isAtStart)
		{
			battleshipCurrentState[index][index2] = battleshipButtons[buttonIndex].state == Switch3DState.Off;
		}
		Debug.Log($"{buttonIndex}: {battleshipButtons[buttonIndex].state}");
		checkClaimedBattleships();
		int count = ++battleshipCounter;
		game.startTimer(new BattleshipTimer(count), 1f);
	}

	private void onBattleshipTimer(int count)
	{
		if (count == battleshipCounter && checkClaimedBattleships())
		{
			game.callRPC(RPCType.Battleship);
		}
	}

	private bool checkClaimedBattleships()
	{
		int num = 8;
		List<int> list = new List<int>();
		bool[,] array = new bool[num, num];
		for (int i = 0; i < num; i++)
		{
			for (int j = 0; j < num; j++)
			{
				if (battleshipCurrentState[j][i] && !array[j, i])
				{
					int num2 = detectShip(array, j, i);
					if (num2 > 0)
					{
						list.Add(num2);
					}
				}
			}
		}
		list.Sort();
		List<int> list2 = new List<int> { 2, 2, 3, 3, 4, 4, 5 };
		List<bool> list3 = new List<bool>(new bool[7]);
		int num3 = 0;
		for (int k = 0; k < list2.Count; k++)
		{
			if (num3 > list.Count - 1)
			{
				break;
			}
			for (int l = num3; l < list.Count; l++)
			{
				if (list[l] == list2[k])
				{
					list3[k] = true;
					num3 = l + 1;
					break;
				}
			}
		}
		for (int m = 0; m < list2.Count; m++)
		{
			if (num3 < list.Count && list[num3] == list2[m])
			{
				list3[m] = true;
				num3++;
			}
		}
		bool result = true;
		for (int n = 0; n < list3.Count; n++)
		{
			if (!list3[n])
			{
				result = false;
			}
			for (int num4 = 0; num4 < battleshipFoundTweens[n].Length; num4++)
			{
				battleshipFoundTweens[n][num4].transitionTo("Flipped", 2f, list3[n] ? 1f : 0f);
			}
		}
		return result;
		int detectShip(bool[,] visited, int startX, int startY)
		{
			int num5 = 8;
			int num6 = 1;
			visited[startX, startY] = true;
			bool isVertical = false;
			if (startX + 1 < num5 && battleshipCurrentState[startX + 1][startY])
			{
				isVertical = true;
				for (int num7 = startX + 1; num7 < num5 && battleshipCurrentState[num7][startY]; num7++)
				{
					if (visited[num7, startY])
					{
						return 0;
					}
					visited[num7, startY] = true;
					num6++;
				}
			}
			else
			{
				if (startY + 1 >= num5 || !battleshipCurrentState[startX][startY + 1])
				{
					return 1;
				}
				for (int num8 = startY + 1; num8 < num5 && battleshipCurrentState[startX][num8]; num8++)
				{
					if (visited[startX, num8])
					{
						return 0;
					}
					visited[startX, num8] = true;
					num6++;
				}
			}
			if (hasDiagonalNeighbors(startX, startY, num6, isVertical))
			{
				return 0;
			}
			return num6;
		}
		bool hasDiagonalNeighbors(int x, int y, int length, bool isVertical)
		{
			int num5 = 8;
			for (int num6 = 0; num6 < length; num6++)
			{
				int num7 = x;
				int num8 = y;
				if (isVertical)
				{
					num7 = x + num6;
				}
				else
				{
					num8 = y + num6;
				}
				if (num7 < num5 - 1 && num8 < num5 - 1 && battleshipCurrentState[num7 + 1][num8 + 1])
				{
					return true;
				}
				if (num7 > 0 && num8 > 0 && battleshipCurrentState[num7 - 1][num8 - 1])
				{
					return true;
				}
				if (num7 > 0 && num8 < num5 - 1 && battleshipCurrentState[num7 - 1][num8 + 1])
				{
					return true;
				}
				if (num7 < num5 - 1 && num8 > 0 && battleshipCurrentState[num7 + 1][num8 - 1])
				{
					return true;
				}
			}
			return false;
		}
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void solveBattleship()
	{
		Switch3D[] array = battleshipButtons;
		foreach (Switch3D switch3D in array)
		{
			if (!(switch3D == null))
			{
				switch3D.targetable = false;
			}
		}
		battleshipWinDrawer.transitionTo("Open");
		foreach (Ref<Item, Transform> item in cheaterDice)
		{
			item.Get<Item>(0).targetable = true;
		}
		game.increaseZoomCounter(battleshipZoom);
		battleshipZoom.targetable = false;
		game.finishPuzzle(Puzzle.Battleship);
	}

	private void onDrummerSkullLever()
	{
		Slot[] array = skullToothSlots;
		foreach (Slot slot in array)
		{
			slot.targetable = false;
			if (slot.insertedItem != null)
			{
				slot.insertedItem.targetable = false;
			}
		}
		skullDrummerLever.targetable = false;
		game.startTimer(new DrummerSkullLeverTimer(beforeFor: true, inFor: true, -1), 0.35f);
		playSoundTime.transitionTo("NewState", 0.4f);
	}

	private void onDrummerSkullLeverTimer(bool beforeFor, bool inFor, int index)
	{
		if (beforeFor)
		{
			for (int i = 0; i < drummerTeethTweens.Length; i++)
			{
				game.startTimer(new DrummerSkullLeverTimer(beforeFor: false, inFor: true, i), 0.3f * (float)i);
				game.startTimer(new DrummerSkullLeverTimer(beforeFor: false, inFor: false, i), 0.3f * (float)i + 0.2f);
			}
		}
		else if (inFor)
		{
			drummerTeethTweens[index].transitionTo("Down", 9f);
		}
		else if (skullToothSlots[index].insertedItem != null)
		{
			drummerPlaySoundTween[index].transitionTo("Hit", 9f);
		}
	}

	private void onDrummerToothClick(int index)
	{
		game.startTimer(new DrummerToothClickTimer(index), 0.1f);
	}

	private void onDrummerToothClickTimerDone(int index)
	{
		drummerTeethTweens[index].transitionTo("Down", 1f, 0f);
	}

	private void onDrummerSoundTween(TweenState tweenState)
	{
		if (tweenState.getWeight("Hit") > 0.9f)
		{
			tweenState.transitionTo("Hit", 9f, 0f);
		}
	}

	private void onDrummerFinalToothReturned()
	{
		if (checkDrummerSkull())
		{
			game.callRPC(RPCType.DrummerGood);
		}
		else
		{
			game.callRPC(RPCType.DrummerWrong);
		}
	}

	private bool checkDrummerSkull()
	{
		Slot[] array = skullToothSlots;
		foreach (Slot slot in array)
		{
			if (Array.Exists(drummerSkullSolution, (Slot x) => x == slot))
			{
				if (slot.insertedItem == null)
				{
					return false;
				}
			}
			else if (slot.insertedItem != null)
			{
				return false;
			}
		}
		return true;
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void solveDrummerSkull()
	{
		game.increaseZoomCounter(drummerZoomable);
		drummerZoomable.targetable = false;
		drummerSkullTween.transitionTo("OpenWide");
		compassItems[2].targetable = true;
		game.finishPuzzle(Puzzle.Drum);
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void solveFishBox()
	{
		fishBoxLid.targetable = true;
		fishBoxLid.sequence.play(-1f, 0.5f);
		Turnable[] array = fishBoxTurnables;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = false;
		}
		game.finishPuzzle(Puzzle.BiggestCatch);
	}

	private void onPortraitTurnable(int index)
	{
		int num = (portraitTurnablesLeft[index].value + portraitTurnablesRight[index].value + 2) % 6 - 1;
		if (portraitTurnablesLeft[index].value == portraitSolutions[index, 0] && portraitTurnablesRight[index].value == portraitSolutions[index, 1])
		{
			num = turnablesSolution[index];
		}
		else if (num == turnablesSolution[index])
		{
			num++;
		}
		float targetRot = Mathf.Repeat((float)num * 60f, 360f);
		game.startTimer(new PaintingCenterTimer(index, portraitCenters[index].localRotation.eulerAngles.z, targetRot), 0.2f);
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void solvePortraitChest()
	{
		Turnable[] array = portraitLockTurnables;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = false;
		}
		game.increaseZoomCounter(portraitLockZoomable);
		portraitLockZoomable.targetable = false;
		portraitLockTs.transitionTo("NewState");
		game.finishPuzzle(Puzzle.Portraits);
	}

	[DebugButton(null, Tint.Default, PostClickAction.HideButton, 0, new object[] { })]
	private void debugGetGems()
	{
		Item[] array = gems;
		foreach (Item item in array)
		{
			if (item.slot == null)
			{
				game.addItemToInventory(item.gameObject);
			}
		}
	}

	private void initGemShip()
	{
		for (int i = 0; i < 12; i++)
		{
			gemCurrentState.Add(GemColor.None);
		}
	}

	private void onGemShipSlots()
	{
		bool flag = true;
		Slot[] array = gemSlots;
		foreach (Slot slot in array)
		{
			if (slot.insertedItem == null || !Array.Exists(slot.acceptItems, (Item x) => x == slot.insertedItem))
			{
				flag = false;
				break;
			}
		}
		if (flag)
		{
			solveGemShipSlots();
		}
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void debugSolveGemShipSlots()
	{
		gemsLockedShipTween.transitionTo("NewState");
		Slot[] array = gemSlots;
		foreach (Slot slot in array)
		{
			if (game.isInInventory(slot.acceptItems[0]))
			{
				game.removeItemFromInventory(slot.acceptItems[0].gameObject);
			}
			if (slot.insertedItem == null)
			{
				game.activateSlotWithItemNonSynced(slot, slot.acceptItems[0]);
			}
			slot.targetable = false;
			slot.insertedItem.targetable = false;
		}
	}

	private void solveGemShipSlots()
	{
		gemsLockedShipTween.transitionTo("NewState");
		Slot[] array = gemSlots;
		foreach (Slot obj in array)
		{
			obj.targetable = false;
			obj.insertedItem.targetable = false;
		}
	}

	private void onGemShipOnPoint(SlidableGraph.ToNode context)
	{
		int num = gemSlidableGraph.nodes.IndexOf(context.toNode);
		if (num == 15)
		{
			return;
		}
		if (num >= 12)
		{
			int num2 = num - 12;
			Slot slot = gemSlots[num2];
			if (slot.insertedItem == null)
			{
				gemCurrentColor = GemColor.None;
			}
			else
			{
				int num3 = Array.IndexOf(gems, slot.insertedItem);
				gemCurrentColor = (GemColor)num3;
			}
			for (int i = 0; i < gemPieceSymbols.Length; i++)
			{
				gemPieceSymbols[i].transitionTo((i == (int)gemCurrentColor) ? "Glow" : "Default", 3f);
			}
			Debug.Log($"Gem color: {gemCurrentColor}");
		}
		else
		{
			for (int j = 0; j < gemPoints.Count; j++)
			{
				gemPoints[j].points[num].SetActive(j == (int)gemCurrentColor);
			}
			gemCurrentState[num] = gemCurrentColor;
		}
	}

	private bool checkGemShip()
	{
		for (int i = 0; i < 12; i++)
		{
			if (gemCurrentState[i] != gemSolution[i])
			{
				return false;
			}
		}
		return true;
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void solveGemShip()
	{
		game.increaseZoomCounter(gemsLockerZoom);
		gemsLockerZoom.targetable = false;
		gemSlidablePiece.targetable = false;
		game.startTimer(new GemShipTimer(), 1f);
		game.finishPuzzle(Puzzle.Medallions);
	}

	private void onGemShipTimerDone()
	{
		gemsLockerDoor.transitionTo("Open");
		exitKeys[3].targetable = true;
	}

	[DebugButton("Get Dice", Tint.Default, PostClickAction.HideButton, 0, new object[] { })]
	private void debugGetDice()
	{
		foreach (Ref<Item, Transform> item in cheaterDice)
		{
			game.addItemToInventory(item.Get<Item>(0).gameObject);
			item.Get<Item>(0).targetable = true;
		}
	}

	private void onCheaterDiceUpdate()
	{
		for (int i = 0; i < cheaterDice.Length; i++)
		{
			Ref<Item, Transform> obj = cheaterDice[i];
			Transform transform = cheaterDiceWeights[i];
			if (obj.Get<Item>(0).hasRigidbody && obj.Get<Item>(0).TryGetComponent<Rigidbody>(out var component))
			{
				component.centerOfMass = obj.Get<Transform>(0f).InverseTransformPoint(transform.transform.position) * 1.5f;
				Debug.DrawLine(obj.Get<Transform>(0f).position, obj.Get<Transform>(0f).position + obj.Get<Transform>(0f).TransformDirection(component.centerOfMass), Color.red);
			}
		}
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void solveCheaterDice()
	{
		cheaterChestLid.targetable = true;
		cheaterChestLid.tweenState.transitionTo("Down", 1f, 0.3f);
		Turnable[] array = cheaterTurnables;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = false;
		}
		game.finishPuzzle(Puzzle.Dice);
	}

	[DebugButton(null, Tint.Default, PostClickAction.HideButton, 0, new object[] { })]
	private void debugGetExitKeys()
	{
		Item[] array = exitKeys;
		foreach (Item item in array)
		{
			if (item.slot == null)
			{
				game.addItemToInventory(item.gameObject);
			}
		}
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void solveKey1()
	{
		exitKeysSolved[0] = true;
		checkExitKeysSolved();
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void solveKey4()
	{
		exitKeysSolved[3] = true;
		checkExitKeysSolved();
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void solveKey2()
	{
		exitKeysSolved[1] = true;
		checkExitKeysSolved();
	}

	private void onKey3Slot()
	{
		exitKeySlots[2].targetable = false;
		exitKeySlots[2].insertedItem.targetable = false;
		key3Zoom.targetable = true;
		exitKey3Turnable.targetable = true;
	}

	private void onKey3Turnable()
	{
		if (exitKey3Turnable.value != exitKey3Solutions[exitKey3CurrentStage])
		{
			return;
		}
		exitKey3CurrentStage++;
		if (exitKey3CurrentStage < exitKey3Solutions.Count)
		{
			exitKey3Turnable.targetable = false;
			exitKey3TurnableMover.play(-1f, exitKey3CurrentStage, 2f);
			exitKey3Turnable.useAngleLimits = true;
			if (exitKey3CurrentStage < exitKey3Solutions.Count)
			{
				exitKey3Turnable.angleLimit1 = exitKey3AngleLimits[exitKey3CurrentStage].Item1;
				exitKey3Turnable.angleLimit2 = exitKey3AngleLimits[exitKey3CurrentStage].Item2;
			}
		}
	}

	private void onKey3SeqDone()
	{
		if (exitKey3CurrentStage == exitKey3Solutions.Count - 1)
		{
			solveKey3();
		}
		else
		{
			exitKey3Turnable.targetable = true;
		}
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void solveKey3()
	{
		exitKeysSolved[2] = true;
		game.increaseZoomCounter(key3Zoom);
		key3Zoom.targetable = false;
		checkExitKeysSolved();
	}

	private void checkExitKeysSolved()
	{
		game.startTimer(new ExitKeysTimer(0), 1f);
	}

	private void solveExitKeys()
	{
		game.finishPuzzle(Puzzle.DoorKeys);
		exitDoor.transitionTo("Open");
		game.startTimer(new ExitKeysTimer(1), 0.5f);
	}

	private void onExitKeysTimerDone(ExitKeysTimer timer)
	{
		if (timer.index == 0)
		{
			bool flag = true;
			foreach (bool item in exitKeysSolved)
			{
				if (!item)
				{
					flag = false;
				}
			}
			if (flag)
			{
				game.callRPC(RPCType.ExitKeys);
			}
		}
		else if (timer.index == 1)
		{
			game.levelCompleted();
			endPlane.Get<GameObject>(0).SetActive(value: true);
		}
	}

	public override void onInitHints()
	{
		game.setPuzzleConditions(Puzzle.BarDoor, Puzzle.Domino, Puzzle.Drum);
		game.setPuzzleConditions(Puzzle.StorageDoors, Puzzle.TableNumbers);
		game.setPuzzleConditions(Puzzle.Battleship, Puzzle.StorageDoors);
		game.setPuzzleConditions(Puzzle.Dice, Puzzle.Battleship);
		game.setPuzzleConditions(Puzzle.Skeleton, Puzzle.BarDoor);
		game.setPuzzleConditions(Puzzle.BiggestCatch, Puzzle.BarDoor);
		game.setPuzzleConditions(Puzzle.Medallions, Puzzle.BarDoor, Puzzle.BiggestCatch);
		game.setPuzzleConditions(Puzzle.DoorKeys, Puzzle.Dice, Puzzle.Skeleton, Puzzle.Medallions, Puzzle.Portraits);
		game.setRelevantObjectsForPuzzle(Puzzle.Domino, dominoBoardZoom.gameObject, dominoJigsaw.gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.Drum, monkey, skullDrummerLever.gameObject, drummerTeeth[0], drummerTeeth[1], drummerTeeth[2], drummerTeeth[3], drummerTeeth[4]);
		game.setRelevantObjectsForPuzzle(Puzzle.BarDoor, compassZoom.gameObject, compassItems[0].gameObject, compassItems[1].gameObject, compassItems[2].gameObject, compassItems[3].gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.TableNumbers, ordersHint, menuHint, lanternsPulley.gameObject, tableLanternsPulley[0].gameObject, tableLanternsPulley[1].gameObject, tableLanternsPulley[2].gameObject, tableLanternsPulley[3].gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.StorageDoors, storageKey.gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.Battleship, battleshipZoom.gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.Dice, cheaterDice[0].Get<Item>(0).gameObject, cheaterDice[1].Get<Item>(0).gameObject, cheaterDice[2].Get<Item>(0).gameObject, diceBox.gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.Skeleton, mugItems[0], mugItems[1], mugItems[2], mugItems[3], mugItems[4], barHint.gameObject, skeletonNpc1.gameObject, skeletonNpc2.gameObject, skeletonNpc3.gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.BiggestCatch, catchGem, fishBox.gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.Medallions, gemsHint.gameObject, gems[0].gameObject, gems[1].gameObject, gems[2].gameObject, gemSlidablePiece.gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.Portraits, rickyHint, portraitsGem, portraitTurnablesLeft[0].gameObject, portraitTurnablesLeft[1].gameObject, portraitTurnablesLeft[2].gameObject, portraitTurnablesLeft[3].gameObject, portraitTurnablesRight[0].gameObject, portraitTurnablesRight[1].gameObject, portraitTurnablesRight[2].gameObject, portraitTurnablesRight[3].gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.DoorKeys, exitKeys[0].gameObject, exitKeys[1].gameObject, exitKeys[2].gameObject, exitKeys[3].gameObject, exitKey3Turnable.gameObject);
		game.setHintCondition(Puzzle.Domino, DominoHint.OpenJigsaw, () => game.wasLookedAtCurrentPuzzle(dominoBoardZoom.gameObject));
		game.setHintCondition(Puzzle.Drum, DrumHint.PickupMonkey, () => game.wasAddedToInventoryDuringCurrentPuzzle(monkey));
		game.setHintCondition(Puzzle.Drum, DrumHint.PickupTeeth, () => game.wasAddedToInventoryDuringCurrentPuzzle(drummerTeeth[0]) && game.wasAddedToInventoryDuringCurrentPuzzle(drummerTeeth[1]));
		game.setHintCondition(Puzzle.BarDoor, BarDoorHint.PickupNorth, () => game.wasAddedToInventoryDuringCurrentPuzzle(compassItems[0].gameObject));
		game.setHintCondition(Puzzle.BarDoor, BarDoorHint.PickupSouth, () => game.wasAddedToInventoryDuringCurrentPuzzle(compassItems[1].gameObject));
		game.setHintCondition(Puzzle.BarDoor, BarDoorHint.PlaceInSlots, () => compassSlots[0].isUnlocked && compassSlots[1].isUnlocked && compassSlots[2].isUnlocked && compassSlots[3].isUnlocked);
		game.setHintCondition(Puzzle.TableNumbers, TableNumbersHint.PickupOrders, () => game.wasAddedToInventoryDuringCurrentPuzzle(ordersHint));
		game.setHintCondition(Puzzle.TableNumbers, TableNumbersHint.LookAtMenu, () => game.wasLookedAtCurrentPuzzle(menuHint));
		game.setHintCondition(Puzzle.Dice, DiceHint.PickUpChest, () => game.wasAddedToInventoryDuringCurrentPuzzle(diceBox.gameObject));
		game.setHintCondition(Puzzle.Skeleton, SkeletonHint.PickupMug, () => game.wasAnyAddedToInventoryDuringCurrentPuzzle(mugItems));
		game.setHintCondition(Puzzle.Skeleton, SkeletonHint.PickupRecipe, () => game.wasAddedToInventoryDuringCurrentPuzzle(barHint.gameObject));
		game.setHintCondition(Puzzle.Skeleton, SkeletonHint.TalkToSkelly, () => game.wasLookedAtCurrentPuzzle(skeletonNpc1.gameObject) || game.wasLookedAtCurrentPuzzle(skeletonNpc2.gameObject) || game.wasLookedAtCurrentPuzzle(skeletonNpc3.gameObject));
		game.setHintCondition(Puzzle.BiggestCatch, BiggestCatchHint.PickUpGemAndChest, () => game.wasAddedToInventoryDuringCurrentPuzzle(catchGem) && game.wasAddedToInventoryDuringCurrentPuzzle(fishBox.gameObject));
		game.setHintCondition(Puzzle.Medallions, MedallionsHint.PlaceInSlots, () => gemSlots[0].isUnlocked && gemSlots[1].isUnlocked && gemSlots[2].isUnlocked);
		game.setHintCondition(Puzzle.Portraits, PortraitsHint.PickUpPaper, () => game.wasAddedToInventoryDuringCurrentPuzzle(rickyHint));
		game.setHintCondition(Puzzle.Portraits, PortraitsHint.PickUpGem, () => game.wasAddedToInventoryDuringCurrentPuzzle(portraitsGem));
	}

	public virtual void save(FastBinaryWriter writer)
	{
		writer.Write(in solvedLanterns, default(FastBinaryWriter.ForPrimitives));
		writer.WriteList(battleshipCurrentState, delegate(FastBinaryWriter w, List<bool> e)
		{
			w.WriteList(e, delegate(FastBinaryWriter fastBinaryWriter, bool value2)
			{
				fastBinaryWriter.Write(in value2, default(FastBinaryWriter.ForPrimitives));
			});
		});
		writer.Write(in battleshipCounter, default(FastBinaryWriter.ForPrimitives));
		writer.WriteList(gemCurrentState, delegate(FastBinaryWriter w, GemColor e)
		{
			int value2 = (int)e;
			w.Write(in value2, default(FastBinaryWriter.ForPrimitives));
		});
		int value = (int)gemCurrentColor;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		writer.WriteList(gemPoints, delegate(FastBinaryWriter w, GemPointList e)
		{
			w.WriteGemPointList(e);
		});
		writer.Write(in juiceDrinkDone, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in grogDrinkDone, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in beerWineDrinkDone, default(FastBinaryWriter.ForPrimitives));
		value = (int)currentSkeletonState;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		writer.WriteArray(currentDrink, delegate(FastBinaryWriter w, DrinkCombination e)
		{
			w.WriteDrinkCombination(e);
		});
		writer.WriteArray(drinkEffect, delegate(FastBinaryWriter w, DrinkEffect e)
		{
			int value2 = (int)e;
			w.Write(in value2, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteList(exitKeysSolved, delegate(FastBinaryWriter w, bool e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.Write(in exitKey3CurrentStage, default(FastBinaryWriter.ForPrimitives));
		writer.WriteArray(tableLanternsCurrent, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
	}

	public virtual void load(FastBinaryReader reader)
	{
		solvedLanterns = reader.ReadBoolean();
		battleshipCurrentState = reader.ReadList((FastBinaryReader r) => r.ReadList((FastBinaryReader fastBinaryReader) => fastBinaryReader.ReadBoolean()));
		battleshipCounter = reader.ReadInt32();
		gemCurrentState = reader.ReadList((FastBinaryReader r) => (GemColor)r.ReadInt32());
		gemCurrentColor = (GemColor)reader.ReadInt32();
		gemPoints = reader.ReadList((FastBinaryReader r) => r.ReadGemPointList());
		juiceDrinkDone = reader.ReadBoolean();
		grogDrinkDone = reader.ReadBoolean();
		beerWineDrinkDone = reader.ReadBoolean();
		currentSkeletonState = (SkeletonState)reader.ReadInt32();
		currentDrink = reader.ReadArray((FastBinaryReader r) => r.ReadDrinkCombination());
		drinkEffect = reader.ReadArray((FastBinaryReader r) => (DrinkEffect)r.ReadInt32());
		exitKeysSolved = reader.ReadList((FastBinaryReader r) => r.ReadBoolean());
		exitKey3CurrentStage = reader.ReadInt32();
		tableLanternsCurrent = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
	}

	public virtual void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		int position = reader.Position;
		bool flag = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "solvedLanterns",
			fieldValue = $"{flag}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<List<bool>> list = reader.ReadList((FastBinaryReader r) => r.ReadList((FastBinaryReader fastBinaryReader) => fastBinaryReader.ReadBoolean()));
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "battleshipCurrentState[" + ((list == null) ? string.Empty : list.Count.ToString()) + "]",
			fieldValue = (((list == null) ? "null" : string.Join(", ", list)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "battleshipCounter",
			fieldValue = $"{num}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<GemColor> list2 = reader.ReadList((FastBinaryReader r) => (GemColor)r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "gemCurrentState[" + ((list2 == null) ? string.Empty : list2.Count.ToString()) + "]",
			fieldValue = (((list2 == null) ? "null" : string.Join(", ", list2)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		GemColor gemColor = (GemColor)reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "gemCurrentColor",
			fieldValue = $"{gemColor}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<GemPointList> list3 = reader.ReadList((FastBinaryReader r) => r.ReadGemPointList());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "gemPoints[" + ((list3 == null) ? string.Empty : list3.Count.ToString()) + "]",
			fieldValue = (((list3 == null) ? "null" : string.Join(", ", list3)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag2 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "juiceDrinkDone",
			fieldValue = $"{flag2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag3 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "grogDrinkDone",
			fieldValue = $"{flag3}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag4 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "beerWineDrinkDone",
			fieldValue = $"{flag4}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		SkeletonState skeletonState = (SkeletonState)reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "currentSkeletonState",
			fieldValue = $"{skeletonState}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		DrinkCombination[] array = reader.ReadArray((FastBinaryReader r) => r.ReadDrinkCombination());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "currentDrink[" + ((array == null) ? string.Empty : array.Length.ToString()) + "]",
			fieldValue = (((array == null) ? "null" : string.Join(", ", (IEnumerable<DrinkCombination>)array)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		DrinkEffect[] array2 = reader.ReadArray((FastBinaryReader r) => (DrinkEffect)r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "drinkEffect[" + ((array2 == null) ? string.Empty : array2.Length.ToString()) + "]",
			fieldValue = (((array2 == null) ? "null" : string.Join(", ", array2)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<bool> list4 = reader.ReadList((FastBinaryReader r) => r.ReadBoolean());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "exitKeysSolved[" + ((list4 == null) ? string.Empty : list4.Count.ToString()) + "]",
			fieldValue = (((list4 == null) ? "null" : string.Join(", ", list4)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num2 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "exitKey3CurrentStage",
			fieldValue = $"{num2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int[] array3 = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "tableLanternsCurrent[" + ((array3 == null) ? string.Empty : array3.Length.ToString()) + "]",
			fieldValue = (((array3 == null) ? "null" : string.Join(", ", array3)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
	}

	public override Timer getTimer(byte id)
	{
		return id switch
		{
			0 => new DrinksColorTimer(), 
			1 => new FishBoxLockDelayTimer(), 
			2 => new LanternsTimer(), 
			3 => new CompassTimer(), 
			4 => new BattleshipTimer(), 
			5 => new DrummerSkullLeverTimer(), 
			6 => new DrummerToothClickTimer(), 
			7 => new GemShipTimer(), 
			8 => new DrinksValveTimer(), 
			9 => new DrinksCupTimer(), 
			10 => new PirateCupTimer(), 
			11 => new ExitKeysTimer(), 
			12 => new DiceMachineTimer(), 
			13 => new PourDrinksTimer(), 
			14 => new StartDrinkTimer(), 
			15 => new EndDrinkTimer(), 
			16 => new PaintingCenterTimer(), 
			17 => new MonkeyCymbalTimer(), 
			18 => new MapDelayTimer(), 
			_ => null, 
		};
	}
}
