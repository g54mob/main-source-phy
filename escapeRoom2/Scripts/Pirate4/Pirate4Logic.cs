using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using FMODUnity;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.VFX;

public class Pirate4Logic : LevelLogic, ISaveable
{
	public sealed class PillarsCutsceneTimer : Timer
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

	public sealed class PushTilesOutTransition : Transition
	{
		public int index;

		public override byte getTypeId()
		{
			return 1;
		}

		public PushTilesOutTransition()
		{
		}

		public PushTilesOutTransition(int index)
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

	public sealed class PillarsCompleteTimer : Timer
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

	public sealed class SolveTailsTimer : Timer
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

	public sealed class CastleDustTimer : Timer
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

	public sealed class EndLevelAnimTimer : Timer
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

	public sealed class SnakeDoorTimer : Timer
	{
		public bool first;

		public override byte getTypeId()
		{
			return 6;
		}

		public SnakeDoorTimer()
		{
		}

		public SnakeDoorTimer(bool first)
		{
			this.first = first;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in first, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			first = reader.ReadBoolean();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("first: " + $"{first}");
			return stringBuilder.ToString();
		}
	}

	public sealed class TrapPostProcessingTimer : Timer
	{
		public float valueMotionBlurFrom;

		public float valueMotionBlurTo;

		public float valueVignetteFrom;

		public float valueVignetteTo;

		public NetPlayerId player;

		public override byte getTypeId()
		{
			return 7;
		}

		public TrapPostProcessingTimer()
		{
		}

		public TrapPostProcessingTimer(float valueMotionBlurFrom, float valueMotionBlurTo, float valueVignetteFrom, float valueVignetteTo, NetPlayerId player)
		{
			this.valueMotionBlurFrom = valueMotionBlurFrom;
			this.valueMotionBlurTo = valueMotionBlurTo;
			this.valueVignetteFrom = valueVignetteFrom;
			this.valueVignetteTo = valueVignetteTo;
			this.player = player;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in valueMotionBlurFrom, default(FastBinaryWriter.ForPrimitives));
			writer.Write(in valueMotionBlurTo, default(FastBinaryWriter.ForPrimitives));
			writer.Write(in valueVignetteFrom, default(FastBinaryWriter.ForPrimitives));
			writer.Write(in valueVignetteTo, default(FastBinaryWriter.ForPrimitives));
			writer.WriteNetPlayerId(player);
		}

		public override void readData(FastBinaryReader reader)
		{
			valueMotionBlurFrom = reader.ReadSingle();
			valueMotionBlurTo = reader.ReadSingle();
			valueVignetteFrom = reader.ReadSingle();
			valueVignetteTo = reader.ReadSingle();
			player = reader.ReadNetPlayerId();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("valueMotionBlurFrom: " + $"{valueMotionBlurFrom}");
			stringBuilder.AppendLine("valueMotionBlurTo: " + $"{valueMotionBlurTo}");
			stringBuilder.AppendLine("valueVignetteFrom: " + $"{valueVignetteFrom}");
			stringBuilder.AppendLine("valueVignetteTo: " + $"{valueVignetteTo}");
			stringBuilder.Append("player: " + $"{player}");
			return stringBuilder.ToString();
		}
	}

	public sealed class TrapFallTimer : Timer
	{
		public NetPlayerId player;

		public override byte getTypeId()
		{
			return 8;
		}

		public TrapFallTimer()
		{
		}

		public TrapFallTimer(NetPlayerId player)
		{
			this.player = player;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.WriteNetPlayerId(player);
		}

		public override void readData(FastBinaryReader reader)
		{
			player = reader.ReadNetPlayerId();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("player: " + $"{player}");
			return stringBuilder.ToString();
		}
	}

	public class Node
	{
		public int index;

		public Dictionary<int, int> neighborNodes;

		public Node(int index)
		{
			neighborNodes = new Dictionary<int, int>();
			this.index = index;
		}
	}

	public class QuestGraph
	{
		public List<Node> nodes;

		public QuestGraph()
		{
			nodes = new List<Node>();
			for (int i = 0; i < 19; i++)
			{
				nodes.Add(new Node(i));
			}
			nodes[0].neighborNodes.Add(1, 5);
			nodes[0].neighborNodes.Add(2, 1);
			nodes[0].neighborNodes.Add(3, 4);
			nodes[1].neighborNodes.Add(2, 0);
			nodes[1].neighborNodes.Add(0, 2);
			nodes[2].neighborNodes.Add(0, 1);
			nodes[2].neighborNodes.Add(1, 3);
			nodes[2].neighborNodes.Add(2, 15);
			nodes[3].neighborNodes.Add(1, 2);
			nodes[3].neighborNodes.Add(0, 4);
			nodes[3].neighborNodes.Add(3, 15);
			nodes[4].neighborNodes.Add(1, 0);
			nodes[4].neighborNodes.Add(3, 6);
			nodes[4].neighborNodes.Add(2, 8);
			nodes[4].neighborNodes.Add(0, 3);
			nodes[5].neighborNodes.Add(3, 0);
			nodes[5].neighborNodes.Add(0, 7);
			nodes[6].neighborNodes.Add(3, 4);
			nodes[6].neighborNodes.Add(1, 7);
			nodes[7].neighborNodes.Add(1, 6);
			nodes[7].neighborNodes.Add(0, 5);
			nodes[7].neighborNodes.Add(2, 9);
			nodes[8].neighborNodes.Add(2, 4);
			nodes[8].neighborNodes.Add(0, 9);
			nodes[8].neighborNodes.Add(1, 14);
			nodes[9].neighborNodes.Add(2, 7);
			nodes[9].neighborNodes.Add(0, 8);
			nodes[9].neighborNodes.Add(3, 10);
			nodes[10].neighborNodes.Add(3, 9);
			nodes[10].neighborNodes.Add(1, 11);
			nodes[10].neighborNodes.Add(0, 14);
			nodes[11].neighborNodes.Add(1, 10);
			nodes[11].neighborNodes.Add(0, 12);
			nodes[12].neighborNodes.Add(0, 11);
			nodes[12].neighborNodes.Add(1, 13);
			nodes[13].neighborNodes.Add(1, 12);
			nodes[13].neighborNodes.Add(3, 16);
			nodes[13].neighborNodes.Add(2, 18);
			nodes[14].neighborNodes.Add(0, 10);
			nodes[14].neighborNodes.Add(1, 8);
			nodes[14].neighborNodes.Add(2, 16);
			nodes[15].neighborNodes.Add(3, 3);
			nodes[15].neighborNodes.Add(1, 16);
			nodes[15].neighborNodes.Add(2, 2);
			nodes[15].neighborNodes.Add(0, 17);
			nodes[16].neighborNodes.Add(1, 15);
			nodes[16].neighborNodes.Add(3, 13);
			nodes[16].neighborNodes.Add(2, 14);
			nodes[17].neighborNodes.Add(0, 15);
		}

		public int bfsDist(int source, int dest)
		{
			if (source == dest)
			{
				return 0;
			}
			Queue<int> queue = new Queue<int>();
			HashSet<int> hashSet = new HashSet<int>();
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			queue.Enqueue(source);
			hashSet.Add(source);
			while (queue.Count > 0)
			{
				int num = queue.Dequeue();
				if (num == dest)
				{
					int num2 = 1;
					for (int num3 = dictionary[dest]; num3 != source; num3 = dictionary[num3])
					{
						num2++;
					}
					return num2;
				}
				foreach (int value in nodes[num].neighborNodes.Values)
				{
					if (!hashSet.Contains(value))
					{
						hashSet.Add(value);
						queue.Enqueue(value);
						dictionary[value] = num;
					}
				}
			}
			return -1;
		}
	}

	public class Snake
	{
		public bool hasHead;

		public bool hasTail;

		public List<SnakePart> parts;
	}

	public class SnakePart
	{
		public int id;

		public bool isFixed;
	}

	public struct RopeCollisionData
	{
		public GameObject hitObject;

		public int dirSign;

		public Vector3 bendingPoint;

		public float prevAngle;
	}

	private enum LevelPredicate
	{
		Pillars = 0,
		Quest = 1,
		Castle = 2,
		SnakeDoor0 = 3,
		SnakeDoor1 = 4,
		Tails = 5,
		Path = 6,
		Stakes = 7,
		FinalSlots = 8,
		Maze = 9,
		SpyGlassDoor = 10,
		Ladder = 11
	}

	private enum Puzzle
	{
		[PuzzleInfo("%PuzzleP4_1%", false)]
		StakeSkulls = 0,
		[PuzzleInfo("%PuzzleP4_2%", true)]
		LadderKnot = 1,
		[PuzzleInfo("%PuzzleP4_3%", false)]
		ZigZag = 2,
		[PuzzleInfo("%PuzzleP4_4%", false)]
		Pillargrimage = 3,
		[PuzzleInfo("%PuzzleP4_5%", false)]
		SailTheLine = 4,
		[PuzzleInfo("%PuzzleP4_6%", false)]
		Ascension = 5,
		[PuzzleInfo("%PuzzleP4_7%", false)]
		HandCave = 6,
		[PuzzleInfo("%PuzzleP4_8%", false)]
		Ropes = 7,
		[PuzzleInfo("%PuzzleP4_9%", false)]
		Maze = 8,
		[PuzzleInfo("%PuzzleP4_10%", false)]
		ConnectSnakes = 9,
		[PuzzleInfo("%PuzzleP4_11%", false)]
		EyeForAnEye = 10
	}

	private enum StakeSkullsHint
	{

	}

	private enum LadderKnotHint
	{
		LookAtTurnables = 0,
		Turn1 = 1,
		Turn2 = 2,
		Turn3 = 3,
		Turn4 = 4,
		Turn5 = 5,
		Turn1MP = 6,
		Turn2MP = 7,
		Turn3MP = 8,
		Turn4MP = 9,
		Turn5MP = 10
	}

	private enum ZigZagHint
	{

	}

	private enum PillargrimageHint
	{

	}

	private enum SailTheLineHint
	{
		GetMap = 0,
		LookAtShip = 1,
		LinesExplained = 2,
		ShipToTreasure = 3
	}

	private enum AscensionHint
	{
		GetSkullReward = 0,
		GetTravelReward = 1,
		FindItem = 2,
		GetSailReward = 3,
		PlaceAll = 4
	}

	private enum HandCaveHint
	{
		GetTool = 0,
		UseTool = 1,
		RotateTool = 2
	}

	private enum RopesHint
	{

	}

	private enum MazeHint
	{
		GetPiece1 = 0,
		GetPiece2 = 1,
		GetPiece3 = 2,
		GetPieces = 3,
		OneSolution = 4
	}

	private enum ConnectSnakesHint
	{
		GetPiece1 = 0,
		GetPiece2 = 1,
		GetPiece3 = 2,
		PlacePieces = 3,
		SolutionHint = 4
	}

	private enum EyeForAnEyeHint
	{
		ReadWall = 0,
		GetHookSword = 1,
		GetSmile = 2,
		GetLever = 3,
		PlaceLeverGetLeg = 4,
		GetEye = 5,
		GetPendant = 6,
		OneSolution = 7
	}

	[DontSave]
	private System.Random random;

	[DontSave]
	public Ref<GameObject, Switch3D> levelExitRef;

	[DontSave]
	public Switch3D bridgeLever;

	[DontSave]
	public GameObject bridgeBlocker;

	[DontSave]
	public Switch3D mapChestSwitch1;

	[DontSave]
	public Switch3D mapChestSwitch2;

	[DontSave]
	public Item[] mapChestItems;

	[DontSave]
	public Slot[] castleSlots;

	[DontSave]
	public Item[] castleItems;

	[DontSave]
	public TweenState castleTweenState;

	[DontSave]
	public ParticleSystem castleDust;

	[DontSave]
	public ParticleSystem castleFire;

	[DontSave]
	public Item spyglass;

	[DontSave]
	public Dial[] spyglassDials;

	[DontSave]
	public Switch3D[] spyglassButtons;

	[DontSave]
	public Switch3D[] correctSpyglassButtons;

	[DontSave]
	public TweenState spyglassDoor;

	[DontSave]
	public ParticleSystem spyglassDoorPs;

	private bool spyglassPickedUp;

	[Header("Trap")]
	[DontSave]
	public Trigger trapTrigger;

	[DontSave]
	public Ref<CutScene, GameObject> trapCutScene;

	[DontSave]
	public Ref<CutScene, GameObject> pillarsCutScene;

	[DontSave]
	public TweenState trapTweenState;

	[DontSave]
	public ParticleSystem trapFallDust;

	[DontSave]
	public float trapTweenStateSpeed = 0.5f;

	[DontSave]
	public Sequence bridgeOpenSequence;

	[DontSave]
	public Transform trapPostCutscenePosition;

	[DontSave]
	public Transform trapRemovePlayersFromBridgePoint;

	[DontSave]
	public GameObject trapPostProcessing;

	[DontSave]
	public Volume trapPostProcessingVolume;

	[DontSave]
	public GameObject floorColliderToDisableWhenTrapActivates;

	private bool playerUsedTrap;

	[Header("Impaling Puzzle")]
	[DontSave]
	public Slot[] impalingSlots;

	[DontSave]
	public TweenState[] impalingSlotTweens;

	[DontSave]
	public Slot[] impalingSlotSolution;

	[DontSave]
	public TweenState impalingChestTS;

	private readonly (int, int)[] impalingSlotPairs = new(int, int)[9]
	{
		(0, 13),
		(1, 8),
		(2, 11),
		(3, 14),
		(4, 9),
		(5, 12),
		(6, 16),
		(7, 15),
		(10, 17)
	};

	[Header("Betrayal Puzzle")]
	[DontSave]
	public Item betrayalLamp;

	[DontSave]
	public RefArray<MaterialState, Transform> betrayalTexts;

	[DontSave]
	public GameObject[] betrayalParticles;

	[DontSave]
	public RefArray<VisualEffect, Transform> betrayalLampLines;

	[DontSave]
	public Vector3 playerEffectOffset = Vector3.up;

	private bool[] visualEffectsPlaying = new bool[6];

	private const float SkeletonTextVisibilityDistance = 2.5f;

	[DontSave]
	public float SkeletonLampEffectVisibilityDistance = 2.5f;

	[Header("Quest Puzzle")]
	[DontSave]
	public TweenState[] questBtnDoors;

	[DontSave]
	public Switch3D[] questSwitches;

	[DontSave]
	public Zoomable questZoomable;

	[DontSave]
	public TweenState shipAnim;

	private const int questStartingNode = 4;

	private int questCurrentNode = 4;

	[DontSave]
	public TweenState questOpenDoors;

	[DontSave]
	private QuestGraph questGraph = new QuestGraph();

	[Header("Snake Puzzle")]
	[DontSave]
	public Item[] snakePlocice1;

	[DontSave]
	public Item[] snakePlocice2;

	[DontSave]
	public Item[] snakePlocice;

	[DontSave]
	public Switch3D[] snakeSwitches;

	[DontSave]
	public TweenState[] snakeFlipStates;

	[DontSave]
	public Slot[] snakeSlots;

	[DontSave]
	public MaterialState[] snakeParts;

	[DontSave]
	public MaterialState[] snakePartsFixed;

	private bool[] snakeStates = new bool[6] { false, true, false, false, false, false };

	private bool firstSnakesCompleted;

	private List<Snake> startedSnakes;

	private List<Snake> completedSnakes;

	[DontSave]
	public TweenState cover;

	[DontSave]
	public ParticleSystem snakesDoorPs;

	[DontSave]
	public ParticleSystem pushPlocicePs;

	[DontSave]
	public ParticleSystem fallPlocicePs;

	public readonly int[][,] snakePlociceData = new int[6][,]
	{
		new int[3, 2]
		{
			{ 1, 0 },
			{ 0, 1 },
			{ 6, 0 }
		},
		new int[3, 2]
		{
			{ 1, 5 },
			{ 0, 0 },
			{ 5, 1 }
		},
		new int[3, 2]
		{
			{ 6, 5 },
			{ 0, 0 },
			{ 1, 1 }
		},
		new int[3, 2]
		{
			{ 1, 0 },
			{ 0, 6 },
			{ 1, 0 }
		},
		new int[3, 2]
		{
			{ 0, 5 },
			{ 1, 0 },
			{ 0, 1 }
		},
		new int[3, 2]
		{
			{ 5, 0 },
			{ 0, 6 },
			{ 5, 0 }
		}
	};

	public readonly int[][,] snakePartsIndex = new int[6][,]
	{
		new int[3, 2]
		{
			{ 0, 0 },
			{ 0, 0 },
			{ 1, 0 }
		},
		new int[3, 2]
		{
			{ 3, 2 },
			{ 0, 0 },
			{ 4, 3 }
		},
		new int[3, 2]
		{
			{ 6, 7 },
			{ 0, 0 },
			{ 5, 5 }
		},
		new int[3, 2]
		{
			{ 9, 0 },
			{ 0, 8 },
			{ 9, 0 }
		},
		new int[3, 2]
		{
			{ 0, 11 },
			{ 10, 0 },
			{ 0, 10 }
		},
		new int[3, 2]
		{
			{ 13, 0 },
			{ 0, 12 },
			{ 14, 0 }
		}
	};

	public readonly int[][,] snakeFixedData = new int[7][,]
	{
		new int[3, 2]
		{
			{ 0, 6 },
			{ 0, 0 },
			{ 0, 5 }
		},
		new int[3, 2]
		{
			{ 0, 6 },
			{ 1, 0 },
			{ 0, 1 }
		},
		new int[3, 2]
		{
			{ 0, 0 },
			{ 5, 0 },
			{ 0, 0 }
		},
		new int[3, 2]
		{
			{ 0, 0 },
			{ 0, 6 },
			{ 0, 0 }
		},
		new int[3, 2]
		{
			{ 6, 0 },
			{ 0, 0 },
			{ 5, 0 }
		},
		new int[3, 2]
		{
			{ 0, 6 },
			{ 0, 0 },
			{ 0, 6 }
		},
		new int[3, 2]
		{
			{ 0, 0 },
			{ 5, 0 },
			{ 0, 0 }
		}
	};

	public readonly int[][,] snakeFixedPartsIndex = new int[7][,]
	{
		new int[3, 2]
		{
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 1 }
		},
		new int[3, 2]
		{
			{ 0, 3 },
			{ 2, 0 },
			{ 0, 2 }
		},
		new int[3, 2]
		{
			{ 0, 0 },
			{ 4, 0 },
			{ 0, 0 }
		},
		new int[3, 2]
		{
			{ 0, 0 },
			{ 0, 8 },
			{ 0, 0 }
		},
		new int[3, 2]
		{
			{ 7, 0 },
			{ 0, 0 },
			{ 6, 0 }
		},
		new int[3, 2]
		{
			{ 0, 9 },
			{ 0, 0 },
			{ 0, 10 }
		},
		new int[3, 2]
		{
			{ 0, 0 },
			{ 5, 0 },
			{ 0, 0 }
		}
	};

	[Header("Tail Puzzle")]
	[DontSave]
	public Switch3D[] tailSwitches;

	[DontSave]
	public TweenState[] tailTSs;

	[DontSave]
	public Ref<Sequence, Zoomable> tailDoorAnim;

	[DontSave]
	public TweenState[] tailTriangleSides;

	[DontSave]
	public MaterialState[] tailEyeGlows;

	[DontSave]
	public ParticleSystem snakesWallDustEffect;

	private int[] tailNodesCurrent = new int[12];

	[DontSave]
	private int[] tailStartNodes = new int[8] { 0, 1, 6, 8, 21, 22, 33, 34 };

	[DontSave]
	private int[] tailCorrectUpA = new int[7] { 1, 2, 0, 2, 1, 0, 1 };

	private int[] tailCorrectUpB = new int[7] { 1, 2, 0, 1, 1, 0, 2 };

	[DontSave]
	private int[] tailCorrectDown = new int[5] { 0, 1, 1, 0, 1 };

	[DontSave]
	private int[][] tailEdges = new int[38][]
	{
		new int[0],
		new int[2] { 3, 5 },
		new int[0],
		new int[0],
		new int[0],
		new int[2] { 36, -13 },
		new int[2] { 10, 11 },
		new int[0],
		new int[3] { 36, -14, -11 },
		new int[0],
		new int[4] { 18, -15, 20, -15 },
		new int[4] { 6, 7, 18, 19 },
		new int[0],
		new int[3] { 15, 36, -6 },
		new int[2] { 36, -3 },
		new int[4] { 13, 14, 19, 20 },
		new int[2] { 36, -6 },
		new int[0],
		new int[4] { 16, -14, 17, -14 },
		new int[4] { 9, 11, 17, 15 },
		new int[5] { 15, 9, -6, 10, -6 },
		new int[1] { 25 },
		new int[0],
		new int[0],
		new int[0],
		new int[2] { 37, -28 },
		new int[0],
		new int[0],
		new int[2] { 30, 37 },
		new int[2] { 37, -24 },
		new int[4] { 28, 29, 34, 35 },
		new int[3] { 29, 33, 35 },
		new int[0],
		new int[4] { 31, -29, 32, -29 },
		new int[2] { 30, 32 },
		new int[4] { 32, -29, 28, -29 },
		new int[7] { 4, -1, 5, -1, 13, -6, -15 },
		new int[4] { 25, 26, 28, -30 }
	};

	[DontSave]
	public Switch3D skeletonCageSwitch;

	[DontSave]
	public Slot skeletonCageLeverSlot;

	[DontSave]
	public Item skeletonCageLever;

	[DontSave]
	public TweenState lowerCage;

	[DontSave]
	public ParticleSystem lowerCagePs;

	[DontSave]
	public Switch3D openCageDoor;

	[DontSave]
	public Item[] skeletonItems;

	[DontSave]
	public TweenState[] skeletonItemTweenStates;

	[DontSave]
	public Slot[] skeletonSlots;

	[DontSave]
	public TweenState[] skeletonSlotsTs;

	[DontSave]
	public TweenState openFinalDoor;

	[DontSave]
	public GameObject finalEffectStrips;

	[DontSave]
	private VisualEffect[] finalEffectStripsVfx;

	[DontSave]
	public MaterialState finalDoorMs;

	[DontSave]
	public Sequence finalDoorSequence;

	[DontSave]
	public ParticleSystem finalDoorPs;

	private readonly int[] solution = new int[6] { 5, 3, 2, 1, 0, 4 };

	[Header("Ladder Puzzle")]
	[DontSave]
	public Turnable[] ladderTurnables;

	[DontSave]
	public GameObject ladderTurnableParent;

	[DontSave]
	public Zoomable ladderTurnableZoomable;

	[DontSave]
	public StudioEventEmitter ladderTurnableSound;

	[DontSave]
	public Turnable[] ladderTurnablesMp;

	[DontSave]
	public GameObject ladderTurnableMpParent;

	[DontSave]
	public Transform[] ladderParts;

	[DontSave]
	public Ladder ladder;

	[DontSave]
	public TweenState finishTopLadder;

	[DontSave]
	public ParticleSystem ladderRotatingVFX;

	private int ladderRotatingEffectCount;

	private float ladderPartsSoundTimer;

	private readonly int[] ladderCorrectValues = new int[5] { 5, 3, 4, 5, 2 };

	[DontSave]
	public Trigger[] pathTriggers;

	[DontSave]
	public Collider[] pathTriggerColliders;

	[DontSave]
	public TweenState[] pathTriggerTiles;

	[DontSave]
	public TweenState pathDoor;

	[DontSave]
	public ParticleSystem pathDoorDust;

	[DontSave]
	public Sequence pathOnWallSequence;

	[DontSave]
	public Sequence[] pathRotationCollumn;

	private Trigger pathLastTriggeredTrigger;

	private readonly int[] pathSequence = new int[8] { 3, 4, 5, 2, 3, 7, 1, 0 };

	private List<int> pathCurrentSequence = new List<int>();

	[DontSave]
	public Slot[] mazeSlots;

	[DontSave]
	public Item[] mazeItems;

	[DontSave]
	public TweenState mazeDoorTS;

	[Header("Pillar Puzzle")]
	[DontSave]
	public Trigger[] pillarTriggers;

	[DontSave]
	public TweenState pillarDoor;

	[DontSave]
	public Draggable[] pillarDraggables;

	[DontSave]
	public Transform[] pillarTransforms;

	private int pillarsPlaced;

	private const float pillarsHardResetLength = 16f;

	private Vector3[] pillarOriginalPositions = new Vector3[3];

	[DontSave]
	public Transform pillarMainEmpty;

	[DontSave]
	public Transform[] pillarRopeMainEmpties;

	[DontSave]
	public LineRenderer[] pillarRopes;

	[DontSave]
	public Transform[] pillarLengthMarkers;

	[DontSave]
	public Transform[] pillarLengthMarkerEmptiesUp;

	[DontSave]
	public Transform[] pillarLengthMarkerEmptiesDown;

	[DontSave]
	public Transform[] pillarRopeRotators;

	[DontSave]
	public ParticleSystem[] pillarTriggerVfx;

	[DontSave]
	public TweenState pillarColumnCollapseTweenState;

	private const float pillarRopeMaxLength = 10f;

	[DontSave]
	public GameObject[] pillarRopeCollidables;

	private List<RopeCollisionData>[] pillarRopeCollisionData;

	private readonly float[] pillarTargetLengths = new float[3] { 0.971f, 0.717f, 0.328f };

	private const float pillarTargetAllowedError = 0.11f;

	private bool pillarsSolved;

	private bool[] pillarRopesCorrect = new bool[3];

	[DontSave]
	public Collider starKeyCollider;

	private float starKeyLastMissTime;

	private bool starKeyPicked;

	[DontSave]
	public Transform debugTeleport;

	[DontSave]
	public Transform debugTeleport2;

	private void enableImpalingSlots()
	{
		(int, int)[] array = impalingSlotPairs;
		for (int i = 0; i < array.Length; i++)
		{
			(int, int) tuple = array[i];
			impalingSlots[tuple.Item1].targetable = impalingSlots[tuple.Item2].insertedItem == null;
			impalingSlots[tuple.Item1].blocksRaycasts = impalingSlots[tuple.Item2].insertedItem == null;
			impalingSlots[tuple.Item2].targetable = impalingSlots[tuple.Item1].insertedItem == null;
			impalingSlots[tuple.Item2].blocksRaycasts = impalingSlots[tuple.Item1].insertedItem == null;
		}
	}

	private bool checkQuest()
	{
		return questCurrentNode == 18;
	}

	[DebugButton(null, Tint.Default, (PostClickAction)0, 0, new object[] { }, label = "Solve Quest Puzzle", postClickAction = PostClickAction.HideButton, tint = Tint.Green)]
	private void solveQuest()
	{
		Switch3D[] array = questSwitches;
		foreach (Switch3D obj in array)
		{
			obj.targetable = false;
			obj.tweenState.setState("Up", 0.5f);
		}
		game.increaseZoomCounter(questZoomable);
		questZoomable.targetable = false;
		questOpenDoors.transitionTo("Open");
		castleItems[2].targetable = true;
		game.finishPuzzle(Puzzle.SailTheLine);
	}

	private bool checkCastle()
	{
		bool flag = true;
		Slot[] array = castleSlots;
		foreach (Slot slot in array)
		{
			flag &= slot.insertedItem == slot.acceptItems[0];
		}
		return flag;
	}

	[DebugButton(null, Tint.Default, (PostClickAction)0, 0, new object[] { }, postClickAction = PostClickAction.HideButton, tint = Tint.Green)]
	private void solveCastle()
	{
		castleFire.Play();
		castleTweenState.transitionTo("Open", 1f, 1f, playSound: true, 1f);
		game.startTimer(new CastleDustTimer(), 1f);
		spyglass.targetable = true;
		Array.ForEach(spyglassDials, delegate(Dial dial)
		{
			dial.targetable = true;
		});
		Array.ForEach(castleSlots, delegate(Slot slot)
		{
			slot.targetable = false;
		});
		Array.ForEach(castleItems, delegate(Item item)
		{
			item.targetable = false;
		});
		game.finishPuzzle(Puzzle.Ascension);
	}

	private void snakeCheckState()
	{
		MaterialState[] array = snakeParts;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].transitionTo("NewState", 3f, 0f);
		}
		array = snakePartsFixed;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].transitionTo("NewState", 3f, 0f);
		}
		startedSnakes = new List<Snake>();
		completedSnakes = new List<Snake>();
		List<int[,]> list = new List<int[,]>();
		List<int[,]> list2 = new List<int[,]>();
		for (int j = 0; j < snakeSlots.Length; j++)
		{
			int num = Array.IndexOf(snakePlocice, snakeSlots[j].insertedItem);
			if (num == -1)
			{
				list.Add(null);
				list2.Add(null);
				continue;
			}
			int[,] array2 = snakePlociceData[num];
			int[,] array3 = snakePartsIndex[num];
			if (snakeStates[j])
			{
				array2 = reverseData(array2);
				array3 = reverseData(array3);
			}
			list.Add(array2);
			list2.Add(array3);
		}
		checkParts(snakeFixedData[0], list[0], snakeFixedPartsIndex[0], list2[0], isLeftFixed: true, isRightFixed: false);
		checkParts(list[0], snakeFixedData[1], list2[0], snakeFixedPartsIndex[1], isLeftFixed: false, isRightFixed: true);
		checkParts(snakeFixedData[1], list[1], snakeFixedPartsIndex[1], list2[1], isLeftFixed: true, isRightFixed: false);
		checkParts(list[1], list[2], list2[1], list2[2], isLeftFixed: false, isRightFixed: false);
		checkParts(list[2], list[3], list2[2], list2[3], isLeftFixed: false, isRightFixed: false);
		checkParts(list[3], snakeFixedData[2], list2[3], snakeFixedPartsIndex[2], isLeftFixed: false, isRightFixed: true);
		resetSnakes();
		checkParts(snakeFixedData[3], list[4], snakeFixedPartsIndex[3], list2[4], isLeftFixed: true, isRightFixed: false);
		checkParts(list[4], snakeFixedData[4], list2[4], snakeFixedPartsIndex[4], isLeftFixed: false, isRightFixed: true);
		resetSnakes();
		checkParts(snakeFixedData[5], list[5], snakeFixedPartsIndex[5], list2[5], isLeftFixed: true, isRightFixed: false);
		checkParts(list[5], snakeFixedData[6], list2[5], snakeFixedPartsIndex[6], isLeftFixed: false, isRightFixed: true);
		resetSnakes();
		foreach (Snake completedSnake in completedSnakes)
		{
			for (int k = 0; k < completedSnake.parts.Count; k++)
			{
				(completedSnake.parts[k].isFixed ? snakePartsFixed[completedSnake.parts[k].id] : snakeParts[completedSnake.parts[k].id]).transitionTo("NewState", 3f);
			}
		}
		void checkParts(int[,] left, int[,] right, int[,] leftParts, int[,] rightParts, bool isLeftFixed, bool isRightFixed)
		{
			if (right == null || left == null)
			{
				resetSnakes();
			}
			else
			{
				for (int l = 0; l < 3; l++)
				{
					int num2 = left[l, 1];
					int num3 = right[l, 0];
					int leftPart = leftParts[l, 1];
					int rightPart = rightParts[l, 0];
					Snake snake = null;
					if (left[l, 1] != 0)
					{
						foreach (Snake startedSnake in startedSnakes)
						{
							if (startedSnake.parts.Any((SnakePart p) => p.id == leftPart && p.isFixed == isLeftFixed))
							{
								snake = startedSnake;
								break;
							}
						}
						if (snake == null)
						{
							snake = new Snake();
							snake.parts = new List<SnakePart>();
							snake.parts.Add(new SnakePart
							{
								id = leftPart,
								isFixed = isLeftFixed
							});
							if (num2 == 5)
							{
								snake.hasHead = true;
							}
							if (num2 == 6)
							{
								snake.hasTail = true;
							}
							startedSnakes.Add(snake);
						}
					}
					if (num2 != 0 && num3 != 0)
					{
						Snake snake2 = null;
						foreach (Snake startedSnake2 in startedSnakes)
						{
							if (startedSnake2.parts.Any((SnakePart p) => p.id == rightPart && p.isFixed == isRightFixed))
							{
								snake2 = startedSnake2;
								break;
							}
						}
						if (snake2 != null)
						{
							if (snake.hasTail)
							{
								snake2.hasTail = true;
							}
							if (snake.hasHead)
							{
								snake2.hasHead = true;
							}
							foreach (SnakePart part in snake.parts)
							{
								snake2.parts.Add(part);
							}
							startedSnakes.Remove(snake);
							snake = snake2;
						}
						else
						{
							snake.parts.Add(new SnakePart
							{
								id = rightPart,
								isFixed = isRightFixed
							});
							if (num3 == 5)
							{
								snake.hasHead = true;
							}
							if (num3 == 6)
							{
								snake.hasTail = true;
							}
						}
					}
				}
			}
		}
		void resetSnakes()
		{
			foreach (Snake startedSnake3 in startedSnakes)
			{
				if (startedSnake3.hasHead && startedSnake3.hasTail)
				{
					completedSnakes.Add(startedSnake3);
				}
			}
			startedSnakes = new List<Snake>();
		}
		static int[,] reverseData(int[,] data)
		{
			int[,] array4 = new int[3, 2];
			for (int l = 0; l < 3; l++)
			{
				array4[l, 0] = data[l, 1];
				array4[l, 1] = data[l, 0];
			}
			return array4;
		}
	}

	private bool checkSnakeDoor(int step)
	{
		if (step != (firstSnakesCompleted ? 1 : 0))
		{
			return false;
		}
		int num = (firstSnakesCompleted ? snakePartsFixed.Length : 5);
		int[] array = new int[9] { 0, 1, 8, 9, 10, 11, 12, 13, 14 };
		bool result = true;
		for (int i = 0; i < num; i++)
		{
			if (!checkIfCorrectPart(i, isFixed: true))
			{
				result = false;
			}
		}
		if (firstSnakesCompleted)
		{
			for (int j = 0; j < snakeParts.Length; j++)
			{
				if (!checkIfCorrectPart(j, isFixed: false))
				{
					result = false;
				}
			}
		}
		else
		{
			for (int k = 0; k < array.Length; k++)
			{
				if (!checkIfCorrectPart(array[k], isFixed: false))
				{
					result = false;
				}
			}
		}
		return result;
		bool checkIfCorrectPart(int num2, bool isFixed)
		{
			foreach (Snake completedSnake in completedSnakes)
			{
				foreach (SnakePart part in completedSnake.parts)
				{
					if (part.id == num2 && part.isFixed == isFixed)
					{
						return true;
					}
				}
			}
			return false;
		}
	}

	[DebugButton("Solve Snake Door", Tint.Default, PostClickAction.HideButton, 0, new object[] { 0, 1 })]
	private void solveSnakeDoor(int step)
	{
		if (step == 0)
		{
			for (int i = 0; i < snakeParts.Length; i++)
			{
				if (i < 2 || i > 7)
				{
					snakeParts[i].transitionTo("NewState", 3f);
				}
			}
			for (int j = 0; j < 6; j++)
			{
				snakePartsFixed[j].transitionTo("NewState", 3f);
			}
			cover.transitionTo("NewState", 0.5f);
			firstSnakesCompleted = true;
			return;
		}
		MaterialState[] array = snakeParts;
		for (int k = 0; k < array.Length; k++)
		{
			array[k].transitionTo("NewState", 2f);
		}
		array = snakePartsFixed;
		for (int k = 0; k < array.Length; k++)
		{
			array[k].transitionTo("NewState", 2f);
		}
		Slot[] array2 = snakeSlots;
		for (int k = 0; k < array2.Length; k++)
		{
			array2[k].targetable = false;
		}
		Item[] array3 = snakePlocice;
		for (int k = 0; k < array3.Length; k++)
		{
			array3[k].targetable = false;
		}
		game.startTimer(new SnakeDoorTimer(first: true), 0.75f);
		firstSnakesCompleted = true;
	}

	[DebugButton(null, Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void getSnakeItems1()
	{
		Item[] array = snakePlocice1;
		foreach (Item item in array)
		{
			game.addItemToInventory(item.gameObject);
		}
	}

	[DebugButton(null, Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void getSnakeItems2()
	{
		Item[] array = snakePlocice2;
		foreach (Item item in array)
		{
			game.addItemToInventory(item.gameObject);
		}
	}

	private bool checkTails()
	{
		bool flag = true;
		bool flag2 = true;
		bool flag3 = true;
		for (int i = 0; i < tailCorrectUpA.Length; i++)
		{
			if (tailNodesCurrent[i] != tailCorrectUpA[i])
			{
				flag = false;
			}
			if (tailNodesCurrent[i] != tailCorrectUpB[i])
			{
				flag2 = false;
			}
		}
		for (int j = 0; j < tailCorrectDown.Length; j++)
		{
			if (tailNodesCurrent[tailCorrectUpA.Length + j] != tailCorrectDown[j])
			{
				flag3 = false;
			}
		}
		return (flag || flag2) && flag3;
	}

	private void solveTails()
	{
		Switch3D[] array = tailSwitches;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = false;
		}
		game.startTimer(new SolveTailsTimer(), 1f);
	}

	[DebugButton(null, Tint.Default, (PostClickAction)0, 0, new object[] { }, label = "Solve Tail Door", postClickAction = PostClickAction.HideButton, tint = Tint.Green)]
	private void solveTailPuzzle()
	{
		skeletonCageLever.targetable = true;
		tailDoorAnim.Get<Sequence>(0).play();
		game.increaseZoomCounter(tailDoorAnim.Get<Zoomable>(0f).gameObject);
		tailDoorAnim.Get<Zoomable>(0f).targetable = false;
		snakesWallDustEffect.Play();
		game.finishPuzzle(Puzzle.ZigZag);
	}

	private void tailCheckGlows()
	{
		bool flag = true;
		bool flag2 = true;
		bool flag3 = true;
		for (int i = 0; i < tailCorrectUpA.Length; i++)
		{
			if (tailNodesCurrent[i] != tailCorrectUpA[i])
			{
				flag = false;
			}
			if (tailNodesCurrent[i] != tailCorrectUpB[i])
			{
				flag2 = false;
			}
		}
		for (int j = 0; j < tailCorrectDown.Length; j++)
		{
			if (tailNodesCurrent[tailCorrectUpA.Length + j] != tailCorrectDown[j])
			{
				flag3 = false;
			}
		}
		bool flag4 = flag || flag2;
		tailEyeGlows[0].transitionTo("Glow", 1f, flag3 ? 1 : 0);
		tailEyeGlows[1].transitionTo("Glow", 1f, flag4 ? 1 : 0);
		TweenState[] array = tailTriangleSides;
		for (int k = 0; k < array.Length; k++)
		{
			array[k].transitionTo("Up", 3f, 0f);
		}
		List<int> list = new List<int>();
		list.Add(36);
		list.Add(37);
		for (int l = 0; l < tailSwitches.Length; l++)
		{
			list.Add(3 * l + tailNodesCurrent[l]);
		}
		List<int> list2 = new List<int>();
		int[] array2 = tailStartNodes;
		foreach (int num in array2)
		{
			if (!list.Contains(num) || ((num == 21 || num == 22) && !list.Contains(25)))
			{
				continue;
			}
			List<int> list3 = new List<int>();
			list3.Add(num);
			int num2 = num;
			for (int m = 0; m < tailSwitches.Length; m++)
			{
				int num3 = -1;
				int[] array3 = tailEdges[num2];
				foreach (int num4 in array3)
				{
					if (num3 == -1 && num4 >= 0 && list.Contains(num4) && !list3.Contains(num4))
					{
						num3 = num4;
					}
					if (num3 != -1 && num4 < 0 && !list.Contains(-num4))
					{
						num3 = -1;
					}
					if (num3 != -1 && num4 >= 0 && num3 != num4)
					{
						break;
					}
				}
				if (num3 == -1)
				{
					break;
				}
				list3.Add(num3);
				num2 = num3;
			}
			foreach (int item in list3)
			{
				tailTriangleSides[item].transitionTo("Up", 3f);
			}
			if (list3.Count > 0)
			{
				list2.Add(list3[list3.Count - 1]);
			}
		}
		int num5 = 0;
		int num6 = 0;
		for (int num7 = 0; num7 < 21; num7++)
		{
			if (tailTriangleSides[num7].findStateByName("Up").targetWeight == 1f)
			{
				num5++;
			}
		}
		if (tailTriangleSides[36].findStateByName("Up").targetWeight == 1f)
		{
			num5++;
		}
		for (int num8 = 21; num8 < 36; num8++)
		{
			if (tailTriangleSides[num8].findStateByName("Up").targetWeight == 1f)
			{
				num6++;
			}
		}
		if (tailTriangleSides[37].findStateByName("Up").targetWeight == 1f)
		{
			num6++;
		}
		if (num5 == 8 && tailEyeGlows[1].findStateByName("Glow").targetWeight != 1f)
		{
			foreach (int item2 in list2)
			{
				if (item2 < 21 || item2 == 36)
				{
					tailTriangleSides[item2].transitionTo("Up", 3f, 0f);
				}
			}
		}
		if (num6 != 6 || tailEyeGlows[0].findStateByName("Glow").targetWeight == 1f)
		{
			return;
		}
		foreach (int item3 in list2)
		{
			if ((item3 >= 21 && item3 != 36) || item3 == 37)
			{
				tailTriangleSides[item3].transitionTo("Up", 3f, 0f);
			}
		}
	}

	[DebugButton("Get Skeleton Parts", Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void getSkeletonParts()
	{
		Item[] array = skeletonItems;
		foreach (Item item in array)
		{
			game.addItemToInventory(item.gameObject);
		}
	}

	private void pathOnTriggerEnter(int index)
	{
		pathTriggerTiles[index].transitionTo("In", 4f);
		if (!(pathLastTriggeredTrigger != null) || !(pathTriggers[index] == pathLastTriggeredTrigger))
		{
			switch (index)
			{
			case 0:
				pathRotationCollumn[0].play(0f, -1f, 4f);
				pathRotationCollumn[3].play(0f, -1f, 4f);
				break;
			case 1:
				pathRotationCollumn[1].play(0f, -1f, 4f);
				pathRotationCollumn[2].play(0f, -1f, 4f);
				break;
			case 2:
				pathRotationCollumn[0].play(0f, -1f, 4f);
				pathRotationCollumn[1].play(0f, -1f, 4f);
				break;
			case 3:
				pathRotationCollumn[2].play(0f, -1f, 4f);
				pathRotationCollumn[3].play(0f, -1f, 4f);
				break;
			case 4:
				pathRotationCollumn[3].play(0f, -1f, 4f);
				pathRotationCollumn[4].play(0f, -1f, 4f);
				break;
			case 5:
				pathRotationCollumn[0].play(0f, -1f, 4f);
				pathRotationCollumn[4].play(0f, -1f, 4f);
				break;
			case 6:
				pathRotationCollumn[1].play(0f, -1f, 4f);
				pathRotationCollumn[4].play(0f, -1f, 4f);
				break;
			case 7:
				pathRotationCollumn[2].play(0f, -1f, 4f);
				pathRotationCollumn[4].play(0f, -1f, 4f);
				break;
			}
			if (pathCurrentSequence.Count < pathSequence.Length && index == pathSequence[pathCurrentSequence.Count])
			{
				pathCurrentSequence.Add(index);
				pathLastTriggeredTrigger = pathTriggers[index];
			}
			else
			{
				pathCurrentSequence.Clear();
				pathLastTriggeredTrigger = null;
			}
			pathOnWallSequence.play(-1f, pathCurrentSequence.Count);
		}
	}

	private void pathOnTriggerExit(int index)
	{
		pathTriggerTiles[index].transitionTo("In", 4f, 0f);
	}

	private bool checkPath()
	{
		return pathCurrentSequence.Count == pathSequence.Length;
	}

	[DebugButton(null, Tint.Default, (PostClickAction)0, 0, new object[] { }, label = "Solve Path", postClickAction = PostClickAction.HideButton, tint = Tint.Green)]
	private void solvePath()
	{
		Array.ForEach(pathTriggerColliders, delegate(Collider x)
		{
			x.enabled = false;
		});
		pathDoor.transitionTo("Open");
		pathDoorDust.Play();
		game.finishPuzzle(Puzzle.Pillargrimage);
	}

	private void pillarRopeInit()
	{
		pillarOriginalPositions[0] = pillarRopeCollidables[0].transform.position;
		pillarOriginalPositions[1] = pillarRopeCollidables[1].transform.position;
		pillarOriginalPositions[2] = pillarRopeCollidables[2].transform.position;
		pillarRopes[0].positionCount = 2;
		pillarRopes[1].positionCount = 2;
		pillarRopes[2].positionCount = 2;
		pillarRopes[0].SetPositions(new Vector3[2]
		{
			pillarRopeMainEmpties[0].position,
			Vector3.zero
		});
		pillarRopes[1].SetPositions(new Vector3[2]
		{
			pillarRopeMainEmpties[1].position,
			Vector3.zero
		});
		pillarRopes[2].SetPositions(new Vector3[2]
		{
			pillarRopeMainEmpties[2].position,
			Vector3.zero
		});
		pillarRopeCollisionData = new List<RopeCollisionData>[pillarRopes.Length];
		pillarRopeCollisionData[0] = new List<RopeCollisionData>();
		pillarRopeCollisionData[1] = new List<RopeCollisionData>();
		pillarRopeCollisionData[2] = new List<RopeCollisionData>();
		for (int i = 0; i < 3; i++)
		{
			pillarRopeCollisionData[i].Add(new RopeCollisionData
			{
				hitObject = pillarRopeMainEmpties[i].gameObject,
				bendingPoint = pillarRopeMainEmpties[i].position,
				prevAngle = 0f
			});
			pillarRopeCollisionData[i].Add(new RopeCollisionData
			{
				hitObject = pillarRopeCollidables[i].gameObject,
				bendingPoint = pillarRopes[i].transform.position,
				prevAngle = 0f
			});
		}
	}

	private void pillarRopeUpdate(int index)
	{
		LineRenderer pillarRope = pillarRopes[index];
		updateBendingPoints();
		int i;
		for (i = 1; i < pillarRopeCollisionData[index].Count; i++)
		{
			Vector3 bendingPoint = pillarRopeCollisionData[index][i - 1].bendingPoint;
			Vector3 vector = pillarRopeCollisionData[index][i].bendingPoint - bendingPoint;
			float magnitude = vector.magnitude;
			vector = vector.normalized;
			RaycastHit[] array = Physics.RaycastAll(new Ray(bendingPoint, vector), magnitude, LayerMask.GetMask("RoomCustom"));
			if (array.Length != 0)
			{
				RaycastHit raycastHit = Array.Find(array, (RaycastHit x) => (x.collider.gameObject != pillarRopeCollidables[index] || i < pillarRopeCollisionData[index].Count - 1) && pillarRopeCollisionData[index][i].hitObject != x.collider.gameObject && pillarRopeCollisionData[index][i - 1].hitObject != x.collider.gameObject);
				if (raycastHit.collider != null)
				{
					GameObject gameObject = raycastHit.collider.gameObject;
					Vector3 vector2 = Vector3.Project(gameObject.GetComponent<Rigidbody>().position - bendingPoint, vector) + bendingPoint;
					vector2.y = pillarRopes[index].transform.position.y;
					Vector3 vector3 = new Vector3(vector.z, 0f, 0f - vector.x);
					Ray ray = new Ray(vector2 - vector3 * 1000f, vector3);
					Ray ray2 = new Ray(vector2 + vector3 * 1000f, -vector3);
					raycastHit.collider.Raycast(ray, out var hitInfo, float.PositiveInfinity);
					raycastHit.collider.Raycast(ray2, out var hitInfo2, float.PositiveInfinity);
					int num = ((Vector3.Distance(hitInfo.point, vector2) < Vector3.Distance(hitInfo2.point, vector2)) ? 1 : (-1));
					Vector3 point = ((num == 1) ? hitInfo : hitInfo2).point;
					pillarRopeCollisionData[index].Insert(i, new RopeCollisionData
					{
						hitObject = gameObject,
						dirSign = num,
						bendingPoint = point
					});
					i++;
				}
			}
		}
		removeBends();
		updateRopePositions();
		float num2 = getRopeLength();
		if (num2 > 16f)
		{
			pillarRopeCollisionData[0].RemoveRange(1, pillarRopeCollisionData[0].Count - 2);
			pillarRopeCollisionData[1].RemoveRange(1, pillarRopeCollisionData[1].Count - 2);
			pillarRopeCollisionData[2].RemoveRange(1, pillarRopeCollisionData[2].Count - 2);
			pillarTransforms[0].position = pillarOriginalPositions[0];
			pillarTransforms[1].position = pillarOriginalPositions[1];
			pillarTransforms[2].position = pillarOriginalPositions[2];
		}
		float num3 = num2 / 10f;
		if (float.IsNaN(num3))
		{
			num3 = 0f;
		}
		if (!pillarsSolved)
		{
			pillarRopeRotators[index].localRotation = Quaternion.Euler(0f, 0f, 8f * num3 * 360f);
			pillarLengthMarkers[index].position = Vector3.Lerp(pillarLengthMarkerEmptiesDown[index].position, pillarLengthMarkerEmptiesUp[index].position, num3);
			if (num3 >= pillarTargetLengths[index] - 0.11f && num3 <= pillarTargetLengths[index] + 0.11f)
			{
				pillarRopesCorrect[index] = true;
			}
		}
		float getRopeLength()
		{
			float num4 = 0f;
			for (int j = 1; j < pillarRope.positionCount; j++)
			{
				num4 += Vector3.Distance(pillarRope.GetPosition(j), pillarRope.GetPosition(j - 1));
			}
			return num4;
		}
		void removeBends()
		{
			for (int j = 1; j < pillarRopeCollisionData[index].Count - 1; j++)
			{
				RopeCollisionData item = pillarRopeCollisionData[index][j];
				RopeCollisionData ropeCollisionData = pillarRopeCollisionData[index][j - 1];
				RopeCollisionData ropeCollisionData2 = pillarRopeCollisionData[index][j + 1];
				if (!(Vector3.Angle(item.bendingPoint - ropeCollisionData.bendingPoint, ropeCollisionData2.bendingPoint - item.bendingPoint) > 90f) && !(item.prevAngle > 90f))
				{
					Vector3 bendingPoint2 = ropeCollisionData.bendingPoint;
					Vector3 onNormal = ropeCollisionData2.bendingPoint - bendingPoint2;
					Vector3 a = Vector3.Project(item.hitObject.GetComponent<Rigidbody>().position - bendingPoint2, onNormal) + bendingPoint2;
					a.y = pillarRopes[index].transform.position.y;
					Vector3 vector4 = new Vector3(onNormal.z, 0f, 0f - onNormal.x) * item.dirSign;
					Vector3 b = item.bendingPoint - 100f * vector4;
					Debug.DrawRay(item.bendingPoint, -vector4 * 100f, Color.green);
					if (Vector3.Distance(a, b) < Vector3.Distance(item.bendingPoint, b))
					{
						pillarRopeCollisionData[index].Remove(item);
						break;
					}
				}
			}
		}
		void updateBendingPoints()
		{
			for (int j = 0; j < pillarRopeCollisionData[index].Count - 2; j++)
			{
				RopeCollisionData ropeCollisionData = pillarRopeCollisionData[index][j];
				RopeCollisionData value = pillarRopeCollisionData[index][j + 1];
				RopeCollisionData ropeCollisionData2 = pillarRopeCollisionData[index][j + 2];
				Vector3 bendingPoint2 = ropeCollisionData.bendingPoint;
				Vector3 position = value.hitObject.transform.position;
				position.y = value.bendingPoint.y;
				Vector3 normalized = (ropeCollisionData2.bendingPoint - position).normalized;
				Vector3 onNormal = position + normalized * Vector3.Distance(ropeCollisionData.bendingPoint, position) - bendingPoint2;
				Vector3 vector4 = Vector3.Project(value.hitObject.GetComponent<Rigidbody>().position - bendingPoint2, onNormal) + bendingPoint2;
				vector4.y = pillarRopes[index].transform.position.y;
				Vector3 vector5 = new Vector3(onNormal.z, 0f, 0f - onNormal.x) * value.dirSign;
				Ray ray3 = new Ray(vector4 - vector5 * 1000f, vector5);
				value.hitObject.GetComponent<Collider>().Raycast(ray3, out var hitInfo3, float.PositiveInfinity);
				value.bendingPoint = hitInfo3.point;
				pillarRopeCollisionData[index][j + 1] = value;
			}
			List<RopeCollisionData> obj = pillarRopeCollisionData[index];
			RopeCollisionData value2 = obj[obj.Count - 1];
			value2.bendingPoint = pillarRopes[index].transform.position;
			List<RopeCollisionData> obj2 = pillarRopeCollisionData[index];
			obj2[obj2.Count - 1] = value2;
		}
		void updateRopePositions()
		{
			pillarRope.positionCount = 2;
			for (int j = 1; j < pillarRopeCollisionData[index].Count - 1; j++)
			{
				RopeCollisionData value = pillarRopeCollisionData[index][j];
				RopeCollisionData ropeCollisionData = pillarRopeCollisionData[index][j - 1];
				RopeCollisionData ropeCollisionData2 = pillarRopeCollisionData[index][j + 1];
				float num4 = Vector3.Angle(value.bendingPoint - ropeCollisionData.bendingPoint, ropeCollisionData2.bendingPoint - value.bendingPoint);
				for (int k = (int)(0f - num4) / 2; k < (int)num4 / 2; k++)
				{
					Vector3 vector4 = value.bendingPoint - value.hitObject.GetComponent<Rigidbody>().position;
					Vector3 vector5 = Quaternion.AngleAxis(k * value.dirSign, Vector3.up) * vector4;
					pillarRope.positionCount++;
					pillarRope.SetPosition(pillarRope.positionCount - 2, value.hitObject.GetComponent<Rigidbody>().position + vector5);
				}
				value.prevAngle = num4;
				pillarRopeCollisionData[index][j] = value;
			}
			pillarRope.SetPosition(pillarRope.positionCount - 1, pillarRope.transform.position);
		}
	}

	public override void onInitPredicates()
	{
		game.registerPredicate(LevelPredicate.Pillars, () => checkPillars());
		game.registerPredicate(LevelPredicate.Quest, () => checkQuest());
		game.registerPredicate(LevelPredicate.Castle, () => checkCastle());
		game.registerPredicate(LevelPredicate.SnakeDoor0, () => checkSnakeDoor(0));
		game.registerPredicate(LevelPredicate.SnakeDoor1, () => checkSnakeDoor(1));
		game.registerPredicate(LevelPredicate.Tails, () => checkTails());
		game.registerPredicate(LevelPredicate.Path, () => checkPath());
		game.registerPredicate(LevelPredicate.Stakes, () => checkStakes());
		game.registerPredicate(LevelPredicate.FinalSlots, () => checkFinalSlots());
		game.registerPredicate(LevelPredicate.Maze, () => checkMaze());
		game.registerPredicate(LevelPredicate.SpyGlassDoor, () => checkSpyGlassDoor());
		game.registerPredicate(LevelPredicate.Ladder, () => checkLadder());
	}

	public override void onLevelPredicateDone(int type, int doneCounter)
	{
		switch (type)
		{
		case 0:
			solvePillars();
			break;
		case 1:
			solveQuest();
			break;
		case 2:
			solveCastle();
			break;
		case 3:
			solveSnakeDoor(0);
			break;
		case 4:
			solveSnakeDoor(1);
			break;
		case 5:
			solveTails();
			break;
		case 6:
			solvePath();
			break;
		case 7:
			solveStakes();
			break;
		case 8:
			solveFinalSlots();
			break;
		case 9:
			solveMaze();
			break;
		case 10:
			solveSpyGlassDoor();
			break;
		case 11:
			solveLadder();
			break;
		}
	}

	public override void onInit()
	{
		random = new System.Random(game.getSyncedRandomSeed());
		tailInit();
		pillarRopeInit();
		ladderTurnableMpParent.SetActive(game.getPlayerCount() > 1);
		ladderTurnableParent.SetActive(game.getPlayerCount() == 1);
		snakeSwitches[1].tweenState.setState("Down");
		snakeFlipStates[1].setState("Flipped");
		foreach (Ref<VisualEffect, Transform> betrayalLampLine in betrayalLampLines)
		{
			betrayalLampLine.Get<VisualEffect>(0).Stop();
		}
		TweenState[] array = questBtnDoors;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].setWeight("NewState", 1f);
		}
		Switch3D[] array2 = questSwitches;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].tweenState.setWeight("Up", 1f);
		}
		finalEffectStripsVfx = finalEffectStrips.GetComponentsInChildren<VisualEffect>();
		void tailInit()
		{
			for (int j = 0; j < tailSwitches.Length; j++)
			{
				tailTSs[j].setState("0");
				tailNodesCurrent[j] = 0;
			}
			tailCheckGlows();
		}
	}

	public override void onFixedUpdate()
	{
		pillarRopeUpdate(0);
		pillarRopeUpdate(1);
		pillarRopeUpdate(2);
	}

	public override void onConvertToSinglePlayer()
	{
		solveLadder();
		if (bridgeLever.state == Switch3DState.Off)
		{
			game.startSwitch(bridgeLever);
		}
	}

	public override void onUpdate()
	{
		if (starKeyCollider.Raycast(game.playerViewRay, out var _, 100f))
		{
			if (Time.time - starKeyLastMissTime > 30f && !starKeyPicked)
			{
				starKeyPicked = true;
				game.showStarKeyPopup(0);
			}
		}
		else
		{
			starKeyLastMissTime = Time.time;
		}
		if (!spyglassPickedUp && game.isInAnyPlayerInventory(spyglass.gameObject))
		{
			spyglassPickedUp = true;
			castleTweenState.transitionTo("Default", 1f, 1f, playSound: true, 0.5f);
			game.startTimer(new CastleDustTimer(), 0.5f);
		}
		if (!ladder.enabled)
		{
			if (ladderTurnableParent.activeInHierarchy)
			{
				Quaternion localRotation = ladderParts[4].localRotation;
				ladderParts[4].localRotation = ladderTurnables[4].transform.localRotation * ladderTurnables[3].transform.localRotation;
				if (Quaternion.Angle(localRotation, ladderParts[4].localRotation) > 0.1f)
				{
					ladderPartsSoundTimer = 0.1f;
				}
				Quaternion localRotation2 = ladderParts[3].localRotation;
				ladderParts[3].localRotation = ladderTurnables[3].transform.localRotation * ladderTurnables[1].transform.localRotation;
				if (Quaternion.Angle(localRotation2, ladderParts[3].localRotation) > 0.1f)
				{
					ladderPartsSoundTimer = 0.1f;
				}
				Quaternion localRotation3 = ladderParts[2].localRotation;
				ladderParts[2].localRotation = ladderTurnables[0].transform.localRotation;
				if (Quaternion.Angle(localRotation3, ladderParts[2].localRotation) > 0.1f)
				{
					ladderPartsSoundTimer = 0.1f;
				}
				Quaternion localRotation4 = ladderParts[1].localRotation;
				ladderParts[1].localRotation = ladderTurnables[2].transform.localRotation * ladderTurnables[1].transform.localRotation;
				if (Quaternion.Angle(localRotation4, ladderParts[1].localRotation) > 0.1f)
				{
					ladderPartsSoundTimer = 0.1f;
				}
				Quaternion localRotation5 = ladderParts[0].localRotation;
				ladderParts[0].localRotation = ladderTurnables[4].transform.localRotation * ladderTurnables[0].transform.localRotation;
				if (Quaternion.Angle(localRotation5, ladderParts[0].localRotation) > 0.1f)
				{
					ladderPartsSoundTimer = 0.1f;
				}
			}
			else
			{
				for (int i = 0; i < ladderParts.Length; i++)
				{
					Quaternion localRotation6 = ladderParts[i].localRotation;
					ladderParts[i].localRotation = ladderTurnablesMp[i].transform.localRotation;
					if (Quaternion.Angle(localRotation6, ladderParts[i].localRotation) > 0.1f)
					{
						ladderPartsSoundTimer = 0.1f;
					}
				}
			}
			if (ladderPartsSoundTimer > 0f)
			{
				ladderPartsSoundTimer -= Time.deltaTime;
				PineFmod.playOrContinue(ladderTurnableSound);
			}
			else if (PineFmod.isPlaying(ladderTurnableSound))
			{
				PineFmod.stop(ladderTurnableSound);
			}
		}
		else if (PineFmod.isPlaying(ladderTurnableSound))
		{
			PineFmod.stop(ladderTurnableSound);
		}
		if (game.isAnyPlayerCarrying(betrayalLamp, out var player))
		{
			for (int j = 0; j < betrayalTexts.Length; j++)
			{
				bool flag = Vector3.Distance(betrayalTexts[j].Get<Transform>(0f).position, player.lastTransformPose.position) < 2.5f;
				betrayalTexts[j].Get<MaterialState>(0).transitionTo("NewState", 1f, (!flag) ? 1 : 0);
				betrayalParticles[j].SetActive(flag);
				bool flag2 = Vector3.Distance(betrayalTexts[j].Get<Transform>(0f).position, player.lastTransformPose.position) < SkeletonLampEffectVisibilityDistance;
				if (!visualEffectsPlaying[j] && flag2)
				{
					betrayalLampLines[j].Get<VisualEffect>(0).Play();
				}
				else if (visualEffectsPlaying[j] && !flag2)
				{
					betrayalLampLines[j].Get<VisualEffect>(0).Stop();
				}
				visualEffectsPlaying[j] = flag2;
				betrayalLampLines[j].Get<Transform>(0f).position = player.lastTransformPose.position + playerEffectOffset;
			}
			return;
		}
		for (int k = 0; k < betrayalTexts.Length; k++)
		{
			bool flag3 = Vector3.Distance(betrayalTexts[k].Get<Transform>(0f).position, betrayalLamp.transform.position) < 2.5f;
			betrayalTexts[k].Get<MaterialState>(0).transitionTo("NewState", 1f, (!flag3) ? 1 : 0);
			betrayalParticles[k].SetActive(flag3);
			bool flag4 = Vector3.Distance(betrayalTexts[k].Get<Transform>(0f).position, betrayalLamp.transform.position) < SkeletonLampEffectVisibilityDistance;
			if (!visualEffectsPlaying[k] && flag4)
			{
				betrayalLampLines[k].Get<VisualEffect>(0).Play();
			}
			else if (visualEffectsPlaying[k] && !flag4)
			{
				betrayalLampLines[k].Get<VisualEffect>(0).Stop();
			}
			visualEffectsPlaying[k] = flag4;
			betrayalLampLines[k].Get<Transform>(0f).position = betrayalLamp.transform.position;
		}
	}

	private bool checkPillars()
	{
		for (int i = 0; i < pillarDraggables.Length; i++)
		{
			if (game.isAnyPlayerInteracting(pillarDraggables[i].gameObject))
			{
				return false;
			}
		}
		if (!pillarsSolved && Array.TrueForAll(pillarRopesCorrect, (bool x) => x))
		{
			return pillarsPlaced == 3;
		}
		return false;
	}

	[DebugButton(null, Tint.Default, (PostClickAction)0, 0, new object[] { }, label = "Solve Pillars", postClickAction = (PostClickAction.ReturnToGame | PostClickAction.HideButton), tint = Tint.Green)]
	private void solvePillars()
	{
		pillarsSolved = true;
		pillarColumnCollapseTweenState.transitionToDuration("Complete", 2f);
		game.startTimer(new PillarsCompleteTimer(), 2f);
		pillarDoor.transitionToDuration("Open", 1f, 1f, playSound: true, 1f);
		pillarsCutScene.Get<GameObject>(0f).SetActive(value: true);
		game.startTimer(new PillarsCutsceneTimer(), 2f);
		game.finishPuzzle(Puzzle.Ropes);
	}

	public override void onTrigger(Trigger trigger, TriggerEvent triggerEvent)
	{
		if (triggerEvent.type == TriggerEventType.Started)
		{
			pirate4OnTriggerEnter();
		}
		else if (triggerEvent.type == TriggerEventType.Ended)
		{
			pirate4OnTriggerExit();
		}
		void pirate4OnTriggerEnter()
		{
			int num = -1;
			if (trigger == trapTrigger && !playerUsedTrap)
			{
				playerUsedTrap = true;
				bool flag = false;
				List<NetPlayerId> list = triggerEvent.playersEnteredThisEvent.ToList();
				list.Sort();
				Debug.Log(list[0]);
				if (game.localPlayerData.id == list[0])
				{
					trapCutScene.Get<GameObject>(0f).SetActive(value: true);
					flag = true;
				}
				if (!flag && game.getCameraPosition().z < trapRemovePlayersFromBridgePoint.position.z)
				{
					game.teleportPlayer(new Vector3(game.localPlayerData.lastTransformPose.position.x, game.localPlayerData.lastTransformPose.position.y, trapRemovePlayersFromBridgePoint.position.z));
				}
				game.startTimer(new TrapFallTimer(list[0]), 1f / trapTweenStateSpeed + 0.25f);
				trapTweenState.transitionTo("Down", trapTweenStateSpeed, 1f, playSound: true, 0.25f);
				trapFallDust.Play();
				floorColliderToDisableWhenTrapActivates.SetActive(value: false);
				bridgeBlocker.SetActive(value: true);
				bridgeOpenSequence.play();
				if (flag)
				{
					trapPostProcessing.SetActive(value: true);
					if (trapPostProcessingVolume.profile.TryGet<MotionBlur>(out var component))
					{
						component.intensity.value = 0f;
					}
					if (trapPostProcessingVolume.profile.TryGet<Vignette>(out var component2))
					{
						component2.intensity.value = 0f;
					}
				}
				game.startTimer(new TrapPostProcessingTimer(0f, 10f, 0f, 0.6f, list[0]), 0.3f);
				game.startTimer(new TrapPostProcessingTimer(10f, 0f, 0.6f, 0f, list[0]), 1f, 1f / trapTweenStateSpeed + 0.25f);
			}
			else if ((num = Array.IndexOf(pillarTriggers, trigger)) != -1)
			{
				pillarTriggerVfx[num].Play();
				pillarsPlaced++;
			}
			else if ((num = Array.IndexOf(pathTriggers, trigger)) != -1)
			{
				pathOnTriggerEnter(num);
			}
		}
		void pirate4OnTriggerExit()
		{
			int num = -1;
			if ((num = Array.IndexOf(pillarTriggers, trigger)) != -1)
			{
				pillarsPlaced--;
			}
			else if ((num = Array.IndexOf(pathTriggers, trigger)) != -1)
			{
				pathOnTriggerExit(num);
			}
		}
	}

	public override void onSlot(Slot targetSlot)
	{
		int num = -1;
		if ((num = Array.IndexOf(skeletonSlots, targetSlot)) != -1)
		{
			skeletonSlotsTs[num].transitionTo("NewState", 2f);
		}
		if ((num = Array.IndexOf(impalingSlots, targetSlot)) != -1)
		{
			impalingSlotTweens[num].transitionTo("In", 4f);
			enableImpalingSlots();
		}
		else if ((num = Array.IndexOf(snakeSlots, targetSlot)) != -1)
		{
			snakeCheckState();
		}
		else if (targetSlot == skeletonCageLeverSlot)
		{
			skeletonCageLeverSlot.targetable = false;
			skeletonCageSwitch.targetable = true;
		}
	}

	private bool checkFinalSlots()
	{
		bool result = true;
		for (int i = 0; i < skeletonSlots.Length; i++)
		{
			if (skeletonSlots[i].insertedItem == null || skeletonSlots[i].insertedItem != skeletonItems[solution[i]])
			{
				result = false;
			}
		}
		return result;
	}

	[DebugButton("Solve Final Slots", Tint.Green, PostClickAction.ReturnToGame | PostClickAction.HideButton, 0, new object[] { })]
	private void solveFinalSlots()
	{
		Slot[] array = skeletonSlots;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = false;
		}
		Item[] array2 = skeletonItems;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].targetable = false;
		}
		finalEffectStrips.SetActive(value: true);
		game.startTimer(new EndLevelAnimTimer(), 2f);
		PineFmod.playOneShotSoundAttached("event:/Sound Effects/05 Misc/Magic/Woosh_2", skeletonSlots[5].gameObject);
	}

	private bool checkStakes()
	{
		bool flag = true;
		Slot[] array = impalingSlotSolution;
		foreach (Slot slot in array)
		{
			flag &= slot.insertedItem != null;
		}
		return flag;
	}

	[DebugButton("Open Impaling Chest", Tint.Green, PostClickAction.ReturnToGame | PostClickAction.HideButton, 0, new object[] { })]
	private void solveStakes()
	{
		Slot[] array = impalingSlots;
		foreach (Slot slot in array)
		{
			slot.targetable = false;
			if (slot.insertedItem != null)
			{
				slot.insertedItem.targetable = false;
			}
		}
		castleItems[0].targetable = true;
		impalingChestTS.transitionToDuration("Open");
		game.finishPuzzle(Puzzle.StakeSkulls);
	}

	public override void onRemoveFromSlot(Slot slot, Item item)
	{
		int num = -1;
		if ((num = Array.IndexOf(skeletonSlots, slot)) != -1)
		{
			skeletonSlotsTs[num].transitionTo("Default", 2f);
		}
		else if ((num = Array.IndexOf(snakeSlots, slot)) != -1)
		{
			snakeCheckState();
		}
		if ((num = Array.IndexOf(impalingSlots, slot)) != -1)
		{
			impalingSlotTweens[num].setState("In", 0f);
			enableImpalingSlots();
		}
	}

	public override void onSwitch3D(Switch3D targetSwitch, Switch3DEvent switchEvent)
	{
		int num = Array.IndexOf(questSwitches, targetSwitch);
		if (num != -1)
		{
			switch (switchEvent)
			{
			case Switch3DEvent.Start:
				Array.ForEach(questSwitches, delegate(Switch3D x)
				{
					x.targetable = false;
				});
				break;
			case Switch3DEvent.Off:
			{
				questCurrentNode = questGraph.nodes[questCurrentNode].neighborNodes[num];
				for (int i = 0; i < questSwitches.Length; i++)
				{
					questSwitches[i].tweenState.transitionTo("Up", 2f, 0f);
				}
				break;
			}
			}
		}
		if (targetSwitch == mapChestSwitch1)
		{
			mapChestSwitch1.targetable = false;
			mapChestSwitch2.targetable = true;
		}
		if (targetSwitch == mapChestSwitch2)
		{
			Item[] array = mapChestItems;
			for (int num2 = 0; num2 < array.Length; num2++)
			{
				array[num2].targetable = switchEvent == Switch3DEvent.On;
			}
		}
		if (targetSwitch == skeletonCageSwitch && switchEvent == Switch3DEvent.On)
		{
			skeletonCageSwitch.targetable = false;
			openCageDoor.targetable = true;
			lowerCage.transitionTo("Down");
			lowerCagePs.Play();
		}
		if (targetSwitch == bridgeLever && switchEvent == Switch3DEvent.On)
		{
			bridgeOpenSequence.play(-1f, 0f);
			bridgeBlocker.SetActive(value: false);
			bridgeLever.targetable = false;
		}
		int num3 = -1;
		if (switchEvent == Switch3DEvent.Start)
		{
			if ((num3 = Array.IndexOf(snakeSwitches, targetSwitch)) != -1)
			{
				snakeStates[num3] = !snakeStates[num3];
				snakeFlipStates[num3].transitionTo("Flipped", 1f, snakeStates[num3] ? 1 : 0);
			}
			else if (targetSwitch == bridgeLever)
			{
				trapTrigger.enabled = false;
				bridgeLever.targetable = false;
			}
			else if ((num3 = Array.IndexOf(tailSwitches, targetSwitch)) != -1)
			{
				tailNodesCurrent[num3]++;
				tailNodesCurrent[num3] %= 3;
				tailTSs[num3].transitionToDuration(tailNodesCurrent[num3].ToString(), 0.1f);
				tailCheckGlows();
			}
			else if (targetSwitch == levelExitRef.Get<Switch3D>(0f))
			{
				game.levelCompleted();
			}
		}
	}

	public override void onTurnableMoved(Turnable turnable, MoveEvent moveEvent)
	{
		int num = -1;
		num = Array.IndexOf(ladderTurnables, turnable);
		if (num == -1)
		{
			num = Array.IndexOf(ladderTurnablesMp, turnable);
		}
		if (num != -1)
		{
			if (ladderRotatingEffectCount < 4 && moveEvent == MoveEvent.Moved && ladderRotatingEffectCount % 2 == 0)
			{
				ladderRotatingVFX.Play();
				ladderRotatingEffectCount++;
			}
			if (ladderRotatingEffectCount < 4 && moveEvent == MoveEvent.Snapped && ladderRotatingEffectCount % 2 == 1)
			{
				ladderRotatingEffectCount++;
			}
		}
	}

	public override void onSequenceDone(Sequence sequence)
	{
		if (sequence == finalDoorSequence)
		{
			finalDoorMs.transitionTo("NewState");
			VisualEffect[] array = finalEffectStripsVfx;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Stop();
			}
		}
	}

	public override void onMaterialTransitionDone(MaterialState materialState, string state)
	{
		if (materialState == finalDoorMs)
		{
			openFinalDoor.transitionTo("NewState", 0.25f);
			levelExitRef.Get<GameObject>(0).SetActive(value: true);
			finalDoorPs.Play();
			game.finishPuzzle(Puzzle.EyeForAnEye);
		}
	}

	public override void onTweenTransitionDone(TweenState tweenState, string state)
	{
		if (tweenState == shipAnim)
		{
			for (int i = 0; i < questSwitches.Length; i++)
			{
				if (questGraph.nodes[questCurrentNode].neighborNodes.ContainsKey(i))
				{
					questBtnDoors[i].transitionTo("NewState", 2f);
				}
			}
			shipAnim.setWeight("NewState", 0f);
		}
		for (int j = 0; j < questSwitches.Length; j++)
		{
			if (questSwitches[j].tweenState == tweenState && state == "Up")
			{
				if (tweenState.getWeight("Up") == 0f)
				{
					questBtnDoors[j].transitionTo("NewState", 2f, 0f);
				}
				else if (tweenState.getWeight("Up") == 1f)
				{
					questSwitches[j].targetable = true;
				}
			}
		}
		int num = Array.IndexOf(questBtnDoors, tweenState);
		if (num != -1)
		{
			if (tweenState.getWeight("NewState") == 0f)
			{
				shipAnim.transitionTo("NewState", 0.5f);
			}
			else if (tweenState.getWeight("NewState") == 1f)
			{
				questSwitches[num].tweenState.transitionTo("Up", 2f);
			}
		}
		if (tweenState == openFinalDoor)
		{
			game.levelCompleted();
		}
		if (Array.IndexOf(snakeFlipStates, tweenState) != -1)
		{
			snakeCheckState();
		}
		if (tweenState == cover && state == "NewState")
		{
			pushPlocicePs.Play();
			for (int k = 0; k < snakePlocice.Length; k++)
			{
				Item item = snakePlocice[k];
				game.removeItemFromSlot(item);
				item.hasRigidbody = false;
				game.startTransitionGlobal(new PushTilesOutTransition(k), item.transform, 0.1f, 0f, item.transform.position + Vector3.forward * 0.1f);
			}
		}
		if (tweenState == finishTopLadder && ladderTurnableZoomable.targetable)
		{
			ladderTurnableZoomable.targetable = false;
			game.increaseZoomCounter(ladderTurnableZoomable);
		}
	}

	private bool checkMaze()
	{
		bool result = true;
		for (int i = 0; i < mazeSlots.Length; i++)
		{
			int num = Array.IndexOf(mazeItems, mazeSlots[i].insertedItem);
			if (num != 2 && num != 3 && num != 4)
			{
				result = false;
			}
		}
		return result;
	}

	[DebugButton(null, Tint.Default, (PostClickAction)0, 0, new object[] { }, postClickAction = PostClickAction.HideButton, tint = Tint.Green)]
	private void solveMaze()
	{
		Slot[] array = mazeSlots;
		foreach (Slot slot in array)
		{
			slot.targetable = false;
			if (slot.insertedItem != null)
			{
				slot.insertedItem.targetable = false;
			}
		}
		mazeDoorTS.transitionTo("Open");
		snakePlocice[5].targetable = true;
		game.finishPuzzle(Puzzle.Maze);
	}

	private bool checkSpyGlassDoor()
	{
		bool result = true;
		Switch3D[] array = spyglassButtons;
		foreach (Switch3D switch3D in array)
		{
			bool flag = Array.IndexOf(correctSpyglassButtons, switch3D) != -1;
			if ((switch3D.state == Switch3DState.On && !flag) || (switch3D.state == Switch3DState.Off && flag))
			{
				result = false;
			}
		}
		return result;
	}

	[DebugButton(null, Tint.Default, (PostClickAction)0, 0, new object[] { }, label = "Solve Spyglass Door", postClickAction = PostClickAction.HideButton, tint = Tint.Green)]
	private void solveSpyGlassDoor()
	{
		spyglassDoor.transitionTo("NewState", 0.5f, 1f, playSound: true, 0.5f);
		spyglassDoorPs.Play();
		Switch3D[] array = spyglassButtons;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = false;
		}
		game.finishPuzzle(Puzzle.HandCave);
	}

	public override void onInitHints()
	{
		game.setPuzzleType(Puzzle.EyeForAnEye, Game.Puzzle.Type.CoopSilent);
		game.setPuzzleConditions(Puzzle.ZigZag, Puzzle.LadderKnot);
		game.setPuzzleConditions(Puzzle.Pillargrimage, Puzzle.LadderKnot);
		game.setPuzzleConditions(Puzzle.SailTheLine, Puzzle.LadderKnot);
		game.setPuzzleConditions(Puzzle.Ascension, Puzzle.StakeSkulls, Puzzle.Pillargrimage, Puzzle.SailTheLine);
		game.setPuzzleConditions(Puzzle.HandCave, Puzzle.Ascension);
		game.setPuzzleConditions(Puzzle.Ropes, Puzzle.HandCave);
		game.setPuzzleConditions(Puzzle.Maze, Puzzle.LadderKnot);
		game.setPuzzleConditions(Puzzle.ConnectSnakes, Puzzle.Ropes, Puzzle.Maze);
		game.setPuzzleConditions(Puzzle.EyeForAnEye, Puzzle.ConnectSnakes);
		game.setRelevantObjectsForPuzzle(Puzzle.StakeSkulls, impalingSlots[0].acceptItems[0].gameObject, impalingSlots[0].acceptItems[1].gameObject, impalingSlots[0].acceptItems[2].gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.LadderKnot, ladderTurnables[0].gameObject, ladderTurnables[1].gameObject, ladderTurnables[2].gameObject, ladderTurnables[3].gameObject, ladderTurnables[4].gameObject, ladderTurnablesMp[0].gameObject, ladderTurnablesMp[1].gameObject, ladderTurnablesMp[2].gameObject, ladderTurnablesMp[3].gameObject, ladderTurnablesMp[4].gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.ZigZag, tailSwitches[0].gameObject, tailSwitches[1].gameObject, tailSwitches[2].gameObject, tailSwitches[3].gameObject, tailSwitches[4].gameObject, tailSwitches[5].gameObject, tailSwitches[6].gameObject, tailSwitches[7].gameObject, tailSwitches[8].gameObject, tailSwitches[9].gameObject, tailSwitches[10].gameObject, tailSwitches[11].gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.SailTheLine, mapChestItems[2].gameObject, questSwitches[0].gameObject, questSwitches[1].gameObject, questSwitches[2].gameObject, questSwitches[3].gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.Ascension, castleItems[0].gameObject, castleItems[1].gameObject, castleItems[2].gameObject, castleItems[3].gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.HandCave, spyglass.gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.Ropes, pillarDraggables[0].gameObject, pillarDraggables[1].gameObject, pillarDraggables[2].gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.Maze, mazeItems[0].gameObject, mazeItems[1].gameObject, mazeItems[2].gameObject, mazeItems[3].gameObject, mazeItems[4].gameObject, mazeItems[5].gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.ConnectSnakes, snakePlocice[0].gameObject, snakePlocice[1].gameObject, snakePlocice[2].gameObject, snakePlocice[3].gameObject, snakePlocice[4].gameObject, snakePlocice[5].gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.EyeForAnEye, betrayalLamp.gameObject, skeletonItems[0].gameObject, skeletonItems[1].gameObject, skeletonItems[2].gameObject, skeletonItems[3].gameObject, skeletonItems[4].gameObject, skeletonItems[5].gameObject);
		game.setHintCondition(Puzzle.LadderKnot, LadderKnotHint.LookAtTurnables, () => game.getPlayerCount() > 1);
		game.setHintCondition(Puzzle.LadderKnot, LadderKnotHint.Turn1, () => game.getPlayerCount() > 1);
		game.setHintCondition(Puzzle.LadderKnot, LadderKnotHint.Turn2, () => game.getPlayerCount() > 1);
		game.setHintCondition(Puzzle.LadderKnot, LadderKnotHint.Turn3, () => game.getPlayerCount() > 1);
		game.setHintCondition(Puzzle.LadderKnot, LadderKnotHint.Turn4, () => game.getPlayerCount() > 1);
		game.setHintCondition(Puzzle.LadderKnot, LadderKnotHint.Turn5, () => game.getPlayerCount() > 1);
		game.setHintCondition(Puzzle.LadderKnot, LadderKnotHint.Turn1MP, () => game.getPlayerCount() == 1);
		game.setHintCondition(Puzzle.LadderKnot, LadderKnotHint.Turn2MP, () => game.getPlayerCount() == 1);
		game.setHintCondition(Puzzle.LadderKnot, LadderKnotHint.Turn3MP, () => game.getPlayerCount() == 1);
		game.setHintCondition(Puzzle.LadderKnot, LadderKnotHint.Turn4MP, () => game.getPlayerCount() == 1);
		game.setHintCondition(Puzzle.LadderKnot, LadderKnotHint.Turn5MP, () => game.getPlayerCount() == 1);
		game.setHintCondition(Puzzle.SailTheLine, SailTheLineHint.GetMap, () => game.wasAddedToInventoryDuringCurrentPuzzle(mapChestItems[2].gameObject));
		game.setHintCondition(Puzzle.Ascension, AscensionHint.GetSkullReward, () => game.wasAddedToInventoryDuringCurrentPuzzle(castleItems[0].gameObject));
		game.setHintCondition(Puzzle.Ascension, AscensionHint.FindItem, () => game.wasAddedToInventoryDuringCurrentPuzzle(castleItems[1].gameObject));
		game.setHintCondition(Puzzle.Ascension, AscensionHint.GetSailReward, () => game.wasAddedToInventoryDuringCurrentPuzzle(castleItems[2].gameObject));
		game.setHintCondition(Puzzle.Ascension, AscensionHint.GetTravelReward, () => game.wasAddedToInventoryDuringCurrentPuzzle(castleItems[3].gameObject));
		game.setHintCondition(Puzzle.HandCave, HandCaveHint.GetTool, () => game.wasAddedToInventoryDuringCurrentPuzzle(spyglass.gameObject));
		game.setHintCondition(Puzzle.Maze, MazeHint.GetPiece1, () => game.wasAddedToInventoryDuringCurrentPuzzle(mazeItems[4].gameObject));
		game.setHintCondition(Puzzle.Maze, MazeHint.GetPiece2, () => game.wasAddedToInventoryDuringCurrentPuzzle(mazeItems[3].gameObject));
		game.setHintCondition(Puzzle.Maze, MazeHint.GetPiece3, () => game.wasAddedToInventoryDuringCurrentPuzzle(mazeItems[2].gameObject));
		game.setHintCondition(Puzzle.Maze, MazeHint.GetPieces, () => game.wasAddedToInventoryDuringCurrentPuzzle(mazeItems[0].gameObject) && game.wasAddedToInventoryDuringCurrentPuzzle(mazeItems[1].gameObject) && game.wasAddedToInventoryDuringCurrentPuzzle(mazeItems[5].gameObject));
		game.setHintCondition(Puzzle.ConnectSnakes, ConnectSnakesHint.GetPiece1, () => game.wasAddedToInventoryDuringCurrentPuzzle(snakePlocice[5].gameObject));
		game.setHintCondition(Puzzle.ConnectSnakes, ConnectSnakesHint.GetPiece2, () => game.wasAddedToInventoryDuringCurrentPuzzle(snakePlocice[4].gameObject));
		game.setHintCondition(Puzzle.ConnectSnakes, ConnectSnakesHint.GetPiece3, () => game.wasAddedToInventoryDuringCurrentPuzzle(snakePlocice[3].gameObject));
		game.setHintCondition(Puzzle.EyeForAnEye, EyeForAnEyeHint.GetLever, () => game.wasAddedToInventoryDuringCurrentPuzzle(skeletonCageLever.gameObject));
	}

	[DebugButton("Skip Drawbridge Trap", Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void skipDrawbridgeTrap()
	{
		game.teleportPlayer(debugTeleport.position);
	}

	[DebugButton("Get Spyglass", Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void getSpyglass()
	{
		game.addItemToInventory(spyglass.gameObject);
		spyglass.targetable = true;
		Dial[] array = spyglassDials;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = true;
		}
	}

	[DebugButton("Get Castle Parts", Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void getCastleParts()
	{
		Item[] array = castleItems;
		foreach (Item item in array)
		{
			game.addItemToInventory(item.gameObject);
			item.targetable = true;
		}
	}

	private bool checkLadder()
	{
		for (int i = 0; i < ladderTurnables.Length; i++)
		{
			if (game.isAnyPlayerInteracting(ladderTurnables[i].gameObject))
			{
				return false;
			}
		}
		bool flag = true;
		for (int j = 0; j < ladderTurnables.Length; j++)
		{
			flag &= ladderTurnables[j].value == ladderCorrectValues[j];
		}
		bool flag2 = true;
		for (int k = 0; k < ladderTurnablesMp.Length; k++)
		{
			flag2 &= ladderTurnablesMp[k].value == ladderCorrectValues[k];
		}
		return flag || flag2;
	}

	[DebugButton(null, Tint.Default, (PostClickAction)0, 0, new object[] { }, postClickAction = PostClickAction.HideButton, tint = Tint.Green)]
	private void solveLadder()
	{
		finishTopLadder.transitionTo("NewState");
		Transform[] array = ladderParts;
		foreach (Transform transform in array)
		{
			Game obj = game;
			Quaternion? rotation = Quaternion.identity;
			obj.startTransitionLocal(transform, 0.3f, 0f, null, rotation);
		}
		Array.ForEach(ladderTurnables, delegate(Turnable x)
		{
			x.targetable = false;
		});
		Array.ForEach(ladderTurnablesMp, delegate(Turnable x)
		{
			x.targetable = false;
		});
		ladder.enabled = true;
		game.finishPuzzle(Puzzle.LadderKnot);
	}

	public virtual void save(FastBinaryWriter writer)
	{
		writer.Write(in spyglassPickedUp, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in playerUsedTrap, default(FastBinaryWriter.ForPrimitives));
		writer.WriteArray(visualEffectsPlaying, delegate(FastBinaryWriter w, bool e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.Write(in questCurrentNode, default(FastBinaryWriter.ForPrimitives));
		writer.WriteArray(snakeStates, delegate(FastBinaryWriter w, bool e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.Write(in firstSnakesCompleted, default(FastBinaryWriter.ForPrimitives));
		writer.WriteList(startedSnakes, delegate(FastBinaryWriter w, Snake e)
		{
			w.WriteSnake(e);
		});
		writer.WriteList(completedSnakes, delegate(FastBinaryWriter w, Snake e)
		{
			w.WriteSnake(e);
		});
		writer.WriteArray(tailNodesCurrent, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteArray(tailCorrectUpB, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.Write(in ladderRotatingEffectCount, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in ladderPartsSoundTimer, default(FastBinaryWriter.ForPrimitives));
		writer.WriteComponent(pathLastTriggeredTrigger);
		writer.WriteList(pathCurrentSequence, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.Write(in pillarsPlaced, default(FastBinaryWriter.ForPrimitives));
		writer.WriteArray(pillarOriginalPositions, delegate(FastBinaryWriter w, Vector3 e)
		{
			w.WriteVector3(in e);
		});
		writer.WriteArray(pillarRopeCollisionData, delegate(FastBinaryWriter w, List<RopeCollisionData> e)
		{
			w.WriteList(e, delegate(FastBinaryWriter writer2, RopeCollisionData data)
			{
				writer2.WriteRopeCollisionData(data);
			});
		});
		writer.Write(in pillarsSolved, default(FastBinaryWriter.ForPrimitives));
		writer.WriteArray(pillarRopesCorrect, delegate(FastBinaryWriter w, bool e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.Write(in starKeyLastMissTime, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in starKeyPicked, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void load(FastBinaryReader reader)
	{
		spyglassPickedUp = reader.ReadBoolean();
		playerUsedTrap = reader.ReadBoolean();
		visualEffectsPlaying = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		questCurrentNode = reader.ReadInt32();
		snakeStates = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		firstSnakesCompleted = reader.ReadBoolean();
		startedSnakes = reader.ReadList((FastBinaryReader r) => r.ReadSnake());
		completedSnakes = reader.ReadList((FastBinaryReader r) => r.ReadSnake());
		tailNodesCurrent = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		tailCorrectUpB = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		ladderRotatingEffectCount = reader.ReadInt32();
		ladderPartsSoundTimer = reader.ReadSingle();
		pathLastTriggeredTrigger = reader.ReadComponent<Trigger>();
		pathCurrentSequence = reader.ReadList((FastBinaryReader r) => r.ReadInt32());
		pillarsPlaced = reader.ReadInt32();
		pillarOriginalPositions = reader.ReadArray((FastBinaryReader r) => r.ReadVector3());
		pillarRopeCollisionData = reader.ReadArray((FastBinaryReader r) => r.ReadList((FastBinaryReader reader2) => reader2.ReadRopeCollisionData()));
		pillarsSolved = reader.ReadBoolean();
		pillarRopesCorrect = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		starKeyLastMissTime = reader.ReadSingle();
		starKeyPicked = reader.ReadBoolean();
	}

	public virtual void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		int position = reader.Position;
		bool flag = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "spyglassPickedUp",
			fieldValue = $"{flag}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag2 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "playerUsedTrap",
			fieldValue = $"{flag2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool[] array = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "visualEffectsPlaying[" + ((array == null) ? string.Empty : array.Length.ToString()) + "]",
			fieldValue = (((array == null) ? "null" : string.Join(", ", array)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "questCurrentNode",
			fieldValue = $"{num}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool[] array2 = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "snakeStates[" + ((array2 == null) ? string.Empty : array2.Length.ToString()) + "]",
			fieldValue = (((array2 == null) ? "null" : string.Join(", ", array2)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag3 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "firstSnakesCompleted",
			fieldValue = $"{flag3}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<Snake> list = reader.ReadList((FastBinaryReader r) => r.ReadSnake());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "startedSnakes[" + ((list == null) ? string.Empty : list.Count.ToString()) + "]",
			fieldValue = (((list == null) ? "null" : string.Join(", ", list)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<Snake> list2 = reader.ReadList((FastBinaryReader r) => r.ReadSnake());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "completedSnakes[" + ((list2 == null) ? string.Empty : list2.Count.ToString()) + "]",
			fieldValue = (((list2 == null) ? "null" : string.Join(", ", list2)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int[] array3 = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "tailNodesCurrent[" + ((array3 == null) ? string.Empty : array3.Length.ToString()) + "]",
			fieldValue = (((array3 == null) ? "null" : string.Join(", ", array3)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int[] array4 = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "tailCorrectUpB[" + ((array4 == null) ? string.Empty : array4.Length.ToString()) + "]",
			fieldValue = (((array4 == null) ? "null" : string.Join(", ", array4)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num2 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "ladderRotatingEffectCount",
			fieldValue = $"{num2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num3 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "ladderPartsSoundTimer",
			fieldValue = $"{num3}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Trigger arg = reader.ReadComponent<Trigger>();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "pathLastTriggeredTrigger",
			fieldValue = $"{arg}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<int> list3 = reader.ReadList((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "pathCurrentSequence[" + ((list3 == null) ? string.Empty : list3.Count.ToString()) + "]",
			fieldValue = (((list3 == null) ? "null" : string.Join(", ", list3)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num4 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "pillarsPlaced",
			fieldValue = $"{num4}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Vector3[] array5 = reader.ReadArray((FastBinaryReader r) => r.ReadVector3());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "pillarOriginalPositions[" + ((array5 == null) ? string.Empty : array5.Length.ToString()) + "]",
			fieldValue = (((array5 == null) ? "null" : string.Join(", ", array5)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<RopeCollisionData>[] array6 = reader.ReadArray((FastBinaryReader r) => r.ReadList((FastBinaryReader reader2) => reader2.ReadRopeCollisionData()));
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "pillarRopeCollisionData[" + ((array6 == null) ? string.Empty : array6.Length.ToString()) + "]",
			fieldValue = (((array6 == null) ? "null" : string.Join(", ", (IEnumerable<List<RopeCollisionData>>)array6)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag4 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "pillarsSolved",
			fieldValue = $"{flag4}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool[] array7 = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "pillarRopesCorrect[" + ((array7 == null) ? string.Empty : array7.Length.ToString()) + "]",
			fieldValue = (((array7 == null) ? "null" : string.Join(", ", array7)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num5 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "starKeyLastMissTime",
			fieldValue = $"{num5}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag5 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "starKeyPicked",
			fieldValue = $"{flag5}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
	}

	public override Timer getTimer(byte id)
	{
		return id switch
		{
			0 => new PillarsCutsceneTimer(), 
			1 => new PushTilesOutTransition(), 
			2 => new PillarsCompleteTimer(), 
			3 => new SolveTailsTimer(), 
			4 => new CastleDustTimer(), 
			5 => new EndLevelAnimTimer(), 
			6 => new SnakeDoorTimer(), 
			7 => new TrapPostProcessingTimer(), 
			8 => new TrapFallTimer(), 
			_ => null, 
		};
	}

	[CompilerGenerated]
	public override void onTimerUpdate(Timer timer)
	{
		if (timer is PillarsCutsceneTimer || timer is PushTilesOutTransition)
		{
			return;
		}
		if (timer is PillarsCompleteTimer timer2)
		{
			onPillarsCompleteTimerUpdate(timer2);
		}
		else if (!(timer is SolveTailsTimer) && !(timer is CastleDustTimer) && !(timer is EndLevelAnimTimer) && !(timer is SnakeDoorTimer))
		{
			if (timer is TrapPostProcessingTimer timer3)
			{
				onTrapPostProcessingTimerUpdate(timer3);
			}
			else
			{
				_ = timer is TrapFallTimer;
			}
		}
	}

	private void onPillarsCompleteTimerUpdate(PillarsCompleteTimer timer)
	{
		int i = 0;
		Array.ForEach(pillarRopeRotators, delegate(Transform x)
		{
			int num = ((i % 2 != 1) ? 1 : (-1));
			Quaternion localRotation = x.localRotation;
			localRotation.z += Time.deltaTime * 20f * (float)num;
			x.localRotation = localRotation;
		});
	}

	private void onTrapPostProcessingTimerUpdate(TrapPostProcessingTimer timer)
	{
		if (!(game.localPlayerData.id != timer.player))
		{
			if (trapPostProcessingVolume.profile.TryGet<MotionBlur>(out var component))
			{
				component.intensity.Interp(timer.valueMotionBlurFrom, timer.valueMotionBlurTo, timer.unitTime);
			}
			if (trapPostProcessingVolume.profile.TryGet<Vignette>(out var component2))
			{
				component2.intensity.Interp(timer.valueVignetteFrom, timer.valueVignetteTo, timer.unitTime);
			}
		}
	}

	[CompilerGenerated]
	public override void onTimerDone(Timer timer)
	{
		if (timer is PillarsCutsceneTimer timer2)
		{
			onPillarsCutsceneTimerDone(timer2);
		}
		else if (timer is PushTilesOutTransition transition)
		{
			onPushTilesOutTransitionDone(transition);
		}
		else if (!(timer is PillarsCompleteTimer))
		{
			if (timer is SolveTailsTimer timer3)
			{
				onSolveTailsTimerDone(timer3);
			}
			else if (timer is CastleDustTimer timer4)
			{
				onCastleDustTimerDone(timer4);
			}
			else if (timer is EndLevelAnimTimer timer5)
			{
				onEndLevelAnimTimerDone(timer5);
			}
			else if (timer is SnakeDoorTimer timer6)
			{
				onSnakeDoorTimerDone(timer6);
			}
			else if (timer is TrapPostProcessingTimer timer7)
			{
				onTrapPostProcessingTimerDone(timer7);
			}
			else if (timer is TrapFallTimer timer8)
			{
				onTrapFallTimerDone(timer8);
			}
		}
	}

	private void onPillarsCutsceneTimerDone(PillarsCutsceneTimer timer)
	{
		pillarsCutScene.Get<GameObject>(0f).SetActive(value: false);
	}

	private void onPushTilesOutTransitionDone(PushTilesOutTransition transition)
	{
		Item obj = snakePlocice[transition.index];
		obj.hasRigidbody = true;
		UnityEngine.Random.State state = UnityEngine.Random.state;
		UnityEngine.Random.InitState(random.Next());
		Vector3 normalized = new Vector3(UnityEngine.Random.Range(-30, 30), UnityEngine.Random.Range(-30, 30), 30f).normalized;
		obj.rbOverrides = new RigidbodyOverrides
		{
			addForce = normalized,
			addForceMode = ForceMode.Impulse
		};
		UnityEngine.Random.state = state;
		fallPlocicePs.Play();
	}

	private void onSolveTailsTimerDone(SolveTailsTimer timer)
	{
		solveTailPuzzle();
	}

	private void onCastleDustTimerDone(CastleDustTimer timer)
	{
		castleDust.Play();
	}

	private void onEndLevelAnimTimerDone(EndLevelAnimTimer timer)
	{
		finalDoorSequence.play();
		TweenState[] array = skeletonItemTweenStates;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].transitionToDuration("Dissolve", 6f, 0.8f);
		}
	}

	private void onSnakeDoorTimerDone(SnakeDoorTimer timer)
	{
		if (timer.first)
		{
			MaterialState[] array = snakeParts;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].transitionTo("Default", 2f);
			}
			array = snakePartsFixed;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].transitionTo("Default", 2f);
			}
			game.startTimer(new SnakeDoorTimer(first: false), 0.75f);
		}
		else
		{
			cover.transitionToStateAdditive("FullOpen");
			snakesDoorPs.Play();
			game.finishPuzzle(Puzzle.ConnectSnakes);
		}
	}

	private void onTrapPostProcessingTimerDone(TrapPostProcessingTimer timer)
	{
		if (timer.valueMotionBlurTo == 0f)
		{
			trapPostProcessing.SetActive(value: false);
		}
	}

	private void onTrapFallTimerDone(TrapFallTimer timer)
	{
		if (game.localPlayerData.id == timer.player)
		{
			game.teleportPlayer(trapPostCutscenePosition.position);
		}
		trapCutScene.Get<GameObject>(0f).SetActive(value: false);
	}
}
