using System;
using System.Collections.Generic;
using System.Text;
using DigitalRuby.LightningBolt;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Space2Logic : LevelLogic, ISaveable
{
	private enum LevelPredicate
	{
		SolarPanels = 0
	}

	public sealed class CompartmentUnlockTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 0;
		}

		public CompartmentUnlockTimer()
		{
		}

		public CompartmentUnlockTimer(int index)
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

	public sealed class PlasmaCompleteTimer : Timer
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

	public sealed class AsteroidSolveTimer : Timer
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

	public sealed class HoloshipCompleteTimer : Timer
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

	public sealed class TokenErrorTimer : Timer
	{
		public bool isFinal;

		public override byte getTypeId()
		{
			return 4;
		}

		public TokenErrorTimer()
		{
		}

		public TokenErrorTimer(bool isFinal)
		{
			this.isFinal = isFinal;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in isFinal, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			isFinal = reader.ReadBoolean();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("isFinal: " + $"{isFinal}");
			return stringBuilder.ToString();
		}
	}

	public sealed class SolarPanelToggleTimer : Timer
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

	public sealed class SolarPanelZoomTimer : Timer
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

	public sealed class TokenIncorrectTimer : Timer
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

	public sealed class TokenUpdateSequenceTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 8;
		}

		public TokenUpdateSequenceTimer()
		{
		}

		public TokenUpdateSequenceTimer(int index)
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

	public sealed class TokenLoadingTimer : Timer
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

	public sealed class TokenCheckTimer : Timer
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

	public sealed class AuxNodeHighlightDelayTimer : Timer
	{
		public GameObject target;

		public override byte getTypeId()
		{
			return 11;
		}

		public AuxNodeHighlightDelayTimer()
		{
		}

		public AuxNodeHighlightDelayTimer(GameObject target)
		{
			this.target = target;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.WriteGameObject(target);
		}

		public override void readData(FastBinaryReader reader)
		{
			target = reader.ReadGameObject();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("target: " + $"{target}");
			return stringBuilder.ToString();
		}
	}

	public sealed class AuxNodeFlashTimer : Timer
	{
		public int index;

		public int stateInt;

		public override byte getTypeId()
		{
			return 12;
		}

		public AuxNodeFlashTimer()
		{
		}

		public AuxNodeFlashTimer(int index, int stateInt)
		{
			this.index = index;
			this.stateInt = stateInt;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in index, default(FastBinaryWriter.ForPrimitives));
			writer.Write(in stateInt, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			index = reader.ReadInt32();
			stateInt = reader.ReadInt32();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("index: " + $"{index}");
			stringBuilder.Append("stateInt: " + $"{stateInt}");
			return stringBuilder.ToString();
		}
	}

	public sealed class AuxSwitchPauseTimer : Timer
	{
		public int result;

		public override byte getTypeId()
		{
			return 13;
		}

		public AuxSwitchPauseTimer()
		{
		}

		public AuxSwitchPauseTimer(int result)
		{
			this.result = result;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in result, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			result = reader.ReadInt32();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("result: " + $"{result}");
			return stringBuilder.ToString();
		}
	}

	public sealed class AuxSwitchCheckTimer : Timer
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

	public sealed class AuxSwitchErrorTimer : Timer
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

	public sealed class SpaceSuitTextFlashTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 16;
		}

		public SpaceSuitTextFlashTimer()
		{
		}

		public SpaceSuitTextFlashTimer(int index)
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

	public sealed class SpaceSuitSequence_1Timer : Timer
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

	public sealed class SpaceSuitSequence_2Timer : Timer
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

	public sealed class SpaceSuitSequence_3Timer : Timer
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

	public sealed class AsteroidSwapStartTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 20;
		}

		public AsteroidSwapStartTimer()
		{
		}

		public AsteroidSwapStartTimer(int index)
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

	public sealed class AsteroidSwapTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 21;
		}

		public AsteroidSwapTimer()
		{
		}

		public AsteroidSwapTimer(int index)
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

	public sealed class PlanetSendWaitTimer : Timer
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

	public sealed class PlanetSendFlashTimer : Timer
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

	public sealed class PlanetSendFoundTimer : Timer
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

	public sealed class StarchartSendWaitTimer : Timer
	{
		public override byte getTypeId()
		{
			return 25;
		}

		public override void writeData(FastBinaryWriter writer)
		{
		}

		public override void readData(FastBinaryReader reader)
		{
		}
	}

	public sealed class StarchartCompleteTimer : Timer
	{
		public override byte getTypeId()
		{
			return 26;
		}

		public override void writeData(FastBinaryWriter writer)
		{
		}

		public override void readData(FastBinaryReader reader)
		{
		}
	}

	public sealed class HoloshipPowerTimer : Timer
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

	public sealed class BitParityToggleTimer : Timer
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

	public sealed class BitParityCheckTimer : Timer
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

	public sealed class FadeOutTimer : Timer
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

	public sealed class FadeInTimer : Timer
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

	public sealed class FadeInOutDelayedTimer : Timer
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

	public sealed class AsteroidFlashTimer : Timer
	{
		public override byte getTypeId()
		{
			return 33;
		}

		public override void writeData(FastBinaryWriter writer)
		{
		}

		public override void readData(FastBinaryReader reader)
		{
		}
	}

	public sealed class PowerShipFinalAnimTimer : Timer
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

	public sealed class SwitchesPowerOnOffTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 35;
		}

		public SwitchesPowerOnOffTimer()
		{
		}

		public SwitchesPowerOnOffTimer(int index)
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

	public sealed class CompartmentOpenTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 36;
		}

		public CompartmentOpenTimer()
		{
		}

		public CompartmentOpenTimer(int index)
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

	public sealed class FadeOutLockTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 37;
		}

		public FadeOutLockTimer()
		{
		}

		public FadeOutLockTimer(int index)
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

	public sealed class StarchartDriveCompleteTimer : Timer
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

	public sealed class CutsceneAllSolvedStartTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 39;
		}

		public CutsceneAllSolvedStartTimer()
		{
		}

		public CutsceneAllSolvedStartTimer(int index)
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

	public sealed class CutsceneCablesSolvedTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 40;
		}

		public CutsceneCablesSolvedTimer()
		{
		}

		public CutsceneCablesSolvedTimer(int index)
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

	public sealed class CutscenePowerAddedTimer : Timer
	{
		public int index;

		public int glowIndex;

		public int buttonIndex;

		public override byte getTypeId()
		{
			return 41;
		}

		public CutscenePowerAddedTimer()
		{
		}

		public CutscenePowerAddedTimer(int index, int glowIndex, int buttonIndex)
		{
			this.index = index;
			this.glowIndex = glowIndex;
			this.buttonIndex = buttonIndex;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in index, default(FastBinaryWriter.ForPrimitives));
			writer.Write(in glowIndex, default(FastBinaryWriter.ForPrimitives));
			writer.Write(in buttonIndex, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			index = reader.ReadInt32();
			glowIndex = reader.ReadInt32();
			buttonIndex = reader.ReadInt32();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("index: " + $"{index}");
			stringBuilder.AppendLine("glowIndex: " + $"{glowIndex}");
			stringBuilder.Append("buttonIndex: " + $"{buttonIndex}");
			return stringBuilder.ToString();
		}
	}

	public sealed class SuitCInematicTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 42;
		}

		public SuitCInematicTimer()
		{
		}

		public SuitCInematicTimer(int index)
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

	public sealed class SolveNumbersAnimTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 43;
		}

		public SolveNumbersAnimTimer()
		{
		}

		public SolveNumbersAnimTimer(int index)
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

	public sealed class PlasmaCubesDistTimer : Timer
	{
		public override byte getTypeId()
		{
			return 44;
		}

		public override void writeData(FastBinaryWriter writer)
		{
		}

		public override void readData(FastBinaryReader reader)
		{
		}
	}

	public sealed class PlasmaDistancePacket : Packet
	{
		public float distance;

		public override byte getTypeId()
		{
			return 0;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in distance, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			distance = reader.ReadSingle();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("distance: " + $"{distance}");
			return stringBuilder.ToString();
		}
	}

	public class StarchartLine
	{
		public int point1;

		public int point2;

		public int lineIndex;
	}

	[Serializable]
	public struct DistributionUIs
	{
		public GameObject parentObject;

		public Switch3D _switch;

		public GameObject[] lockedComponents;

		public GameObject[] unlockedComponents;

		public AnimationSampler openAnimation;

		public Transform lockAnimation;
	}

	[Serializable]
	public class DistributionNode
	{
		public bool isStarting;

		public bool isFinal;

		public int startNodeIndex;

		public int rotatableNodeIndex;

		public int finalNodeIndex;

		public int[] directions_1;

		public int[] directions_2;

		public Vector2[] adjacentNodeIndices;
	}

	[Serializable]
	public class AuxNumber
	{
		public GameObject[] numbers;
	}

	[Serializable]
	public class AuxOverrideGraph
	{
		[SerializeField]
		public AuxOverrideGraphNode[] nodes;

		[HideInInspector]
		public bool[] nodesToFlash = new bool[15];

		public void initNodes()
		{
			for (int i = 0; i < nodes.Length; i++)
			{
				nodes[i].updateConnections(0);
			}
		}

		public int getInteractiveIndex(Interactive interactive)
		{
			for (int i = 0; i < nodes.Length; i++)
			{
				if (interactive == nodes[i].getInteractive())
				{
					return i;
				}
			}
			return -1;
		}

		public void updateNode(int index, int offset)
		{
			nodes[index].updateConnections(offset);
		}
	}

	[Serializable]
	public struct AuxOverrideGraphNode
	{
		[SerializeField]
		public Interactive interactive;

		[SerializeField]
		public int[] adjacentNodes;

		[SerializeField]
		public int[] dialStartingConnections;

		[SerializeField]
		public GameObject flash;

		[SerializeField]
		public MaterialState highlight;

		[SerializeField]
		public GameObject highlightGo;

		[SerializeField]
		public MaterialState[] adjacentHighlights;

		[SerializeField]
		public GameObject[] adjacentHighlightsGo;

		public int[] dialConnections;

		public void updateConnections(int offset)
		{
			dialConnections = new int[dialStartingConnections.Length];
			for (int i = 0; i < dialStartingConnections.Length; i++)
			{
				dialConnections[i] = (dialStartingConnections[i] + offset) % 6;
			}
		}

		public int[] getConnections()
		{
			return dialConnections;
		}

		public int[] getAdjacentNodes()
		{
			return adjacentNodes;
		}

		public Interactive getInteractive()
		{
			return interactive;
		}
	}

	private enum RPCType
	{
		BPPuzzle = 0,
		AuxPuzzleGood = 1,
		AuxPuzzleBad = 2,
		Spacesuit = 3,
		StarScanner = 4,
		StarchartPart = 5,
		Magnet = 6,
		PowerCables = 7,
		Distributor1 = 8,
		Distributor0 = 9,
		Distributor2 = 10,
		Distributor3 = 11,
		Distributor4 = 12,
		Distributor5 = 13,
		TokenSolve = 14,
		TokenFail = 15
	}

	private enum Puzzle
	{
		[PuzzleInfo("%PuzzleS2_1%", false)]
		StarScanner = 0,
		[PuzzleInfo("%PuzzleS2_2%", false)]
		BinarySudoku = 1,
		[PuzzleInfo("%PuzzleS2_3%", false)]
		StarCharts = 2,
		[PuzzleInfo("%PuzzleS2_4%", false)]
		PowerDistribution = 3,
		[PuzzleInfo("%PuzzleS2_5%", false)]
		SpaceSuit = 4,
		[PuzzleInfo("%PuzzleS2_6%", false)]
		SolarPanels = 5,
		[PuzzleInfo("%PuzzleS2_7%", false)]
		HexagonInput = 6,
		[PuzzleInfo("%PuzzleS2_8%", false)]
		PowerCables = 7,
		[PuzzleInfo("%PuzzleS2_9%", false)]
		Plasmids = 8,
		[PuzzleInfo("%PuzzleS2_10%", false)]
		Tractors = 9,
		[PuzzleInfo("%PuzzleS2_11%", false)]
		Exit = 10
	}

	private enum StarScannerHint
	{
		GetTablet = 0,
		FindScreen = 1,
		UseScreen = 2,
		OneSolution = 3
	}

	private enum BinarySudokuHint
	{
		FindScreen = 0,
		Rules = 1,
		Solve4 = 2,
		Solve2 = 3
	}

	private enum StarChartsHint
	{
		Get2Drives = 0,
		GetDrive = 1,
		PlaceDrives = 2,
		OneSolution = 3
	}

	private enum UnlockGunHint
	{
		GetGunAndHint = 0,
		LookAtScreen = 1,
		MovePower = 2,
		Unlock = 3,
		ConnectLines = 4
	}

	private enum SpaceSuitHint
	{
		GetDots = 0,
		GetGun = 1,
		SetPull = 2,
		GetFuseBox = 3,
		GetFuses = 4,
		GetLinesAndX = 5,
		LookAtHint = 6,
		PlaceFuses = 7,
		NeedPower = 8,
		DispenseSuit = 9
	}

	private enum SolarPanelsHint
	{

	}

	private enum HexagonInputHint
	{
		LookAtHint = 0,
		GoHere = 1,
		OpenVault = 2,
		NumbersHere = 3,
		RotateThis = 4,
		Rules = 5,
		OneSolution = 6
	}

	private enum PowerCablesHint
	{
		GetPower = 0,
		GetCable1 = 1,
		GetCable2 = 2,
		GetCable3 = 3,
		PlaceCables = 4,
		Rules = 5,
		PressButton = 6
	}

	private enum PlasmidsHint
	{

	}

	private enum TractorsHint
	{
		GetPower = 0,
		LookAtPuzzle = 1,
		Rules = 2,
		Press1 = 3,
		Press2 = 4,
		Press3 = 5
	}

	private enum ExitHint
	{

	}

	[Header("Misc")]
	[DontSave]
	public StudioEventEmitter music;

	[DontSave]
	public GameObject forceFieldAudio;

	[DontSave]
	public TweenState giantSphereTS;

	[DontSave]
	public Light flickeringLight;

	[DontSave]
	public Light flickeringLight2;

	[DontSave]
	public MaterialState flickeringLightMS;

	[DontSave]
	public MaterialState flickeringLightMS2;

	public GameObject cutsceneAllSolvedStart;

	public GameObject cutsceneCablesSolved;

	public GameObject cutscenePowerAdded;

	public GameObject suitCinematic;

	private bool levelComplete;

	private const string holoshipStatusPrefix = "";

	private const string holoshipStatusPrefixPowered = "";

	private const string distributionStatusPrefixCommon = "";

	private const string distributionStatusSuffixCommon_2 = "/2";

	private const string distributionStatusSuffixCommon_6 = "/6";

	private const string distributionStatusSuffixCommon_9 = "/9";

	private const string distributionStatusSuffixCommon_12 = "/12";

	[DontSave]
	public Image blackOverlay;

	[Header("Zero Gravity")]
	[DontSave]
	public Trigger[] zeroGSpecialRespawnTriggers;

	[DontSave]
	public Trigger zeroGTrigger;

	private HashSet<GameObject> zeroGRigidbodies = new HashSet<GameObject>();

	private bool zeroGUnlocked;

	private bool suitDispenserPowered;

	private bool forcePower;

	[Header("Spacesuit")]
	[DontSave]
	public GameObject spacesuitOverlay;

	[DontSave]
	public TweenState spacesuitOverlayFlash;

	[DontSave]
	public Switch3D spacesuitSwitch;

	[DontSave]
	public Sequence spacesuitSequence;

	[DontSave]
	public GameObject holoNavigationBlocker;

	[DontSave]
	public MaterialState holoNavigationBlockerMS;

	[DontSave]
	public GameObject[] holoNavmeshObstacles;

	[DontSave]
	public TweenState suitDoorTS;

	[DontSave]
	public GameObject suitScreenMain;

	[DontSave]
	public GameObject suitScreenActivating;

	[DontSave]
	public GameObject suitDoorLowPower;

	[DontSave]
	public GameObject suitDoorUnequipped;

	[DontSave]
	public Switch3D suitDispenseButton;

	[DontSave]
	public GameObject suitDoorEquipped;

	[DontSave]
	public Item fuseBox;

	[DontSave]
	public Switch3D fuseBoxLid;

	[DontSave]
	public TweenState gunDoorTS;

	[DontSave]
	public TweenState smallCompartmentDoorTS;

	[DontSave]
	public TweenState planetPuzzleCompartmentTS;

	[DontSave]
	public TweenState distributorTS;

	[DontSave]
	public TweenState distributorPullOutTs;

	[DontSave]
	public GameObject auxPowerOfflineText;

	[DontSave]
	public GameObject auxPowerOnlineText;

	[DontSave]
	public GameObject auxFinalScreen;

	[DontSave]
	public GameObject auxPuzzleScreen;

	[DontSave]
	public Zoomable auxZoom;

	[DontSave]
	public GameObject navigationOfflineText_1;

	[DontSave]
	public GameObject crashLogMissingText_1;

	[DontSave]
	public GameObject communicationOfflineText_1;

	[DontSave]
	public GameObject navigationOfflineText_2;

	[DontSave]
	public GameObject crashLogMissingText_2;

	[DontSave]
	public GameObject communicationOfflineText_2;

	[DontSave]
	public GameObject navigationOnlineText_1;

	[DontSave]
	public GameObject crashLogFoundText_1;

	[DontSave]
	public GameObject communicationOnlineText_1;

	[DontSave]
	public GameObject navigationOnlineText_2;

	[DontSave]
	public GameObject crashLogFoundText_2;

	[DontSave]
	public GameObject communicationOnlineText_2;

	[DontSave]
	public GameObject navigationOfflineText_3;

	[DontSave]
	public GameObject crashLogMissingText_3;

	[DontSave]
	public GameObject communicationOfflineText_3;

	[DontSave]
	public GameObject distributionLockedText_1;

	[DontSave]
	public GameObject distributionLockedText_2;

	[DontSave]
	public GameObject distributionUnlockedText_1;

	[DontSave]
	public GameObject distributionUnlockedText_2;

	[DontSave]
	public GameObject auxHintOfflineText;

	[DontSave]
	public GameObject auxHintOnlineText;

	private bool navigationOnline;

	private bool crashLogSent;

	private bool communicationOnline;

	[Header("Token Puzzle")]
	[DontSave]
	public Slot[] tokenSlots;

	[DontSave]
	public RefArray<Item, Transform> tokenItems;

	[DontSave]
	public Switch3D tokenConfirmButton;

	private string[] tokenCurrentSolutionVisual = new string[5];

	[DontSave]
	public MeshFilter[] tokenMeshFilters;

	[DontSave]
	public GameObject[] tokenSymbolGos;

	[DontSave]
	public Mesh[] tokenSymbolMeshes;

	[DontSave]
	public MaterialState[] tokenSymbolMaterialStates;

	[DontSave]
	public GameObject tokenMissingChipsScreen;

	[DontSave]
	public GameObject tokenInsertedScreen;

	[DontSave]
	public GameObject tokenWriteScreen;

	[DontSave]
	public GameObject tokenLoadingScreen;

	[DontSave]
	public AnimationSampler tokenLoadingBar;

	[DontSave]
	public GameObject tokenDeniedScreen;

	[DontSave]
	public GameObject tokenGrantedScreen;

	[DontSave]
	public MaterialState tokenErrorMaterialState;

	[DontSave]
	public Slidable tabletLockSlidable;

	[DontSave]
	public Ref<TweenState, GameObject> tabletLockScreen;

	[DontSave]
	public Item tablet;

	[DontSave]
	public MaterialState tabletScreen;

	[DontSave]
	public RefArray<GameObject, Item, Transform, TweenState> food;

	[DontSave]
	public ParticleSystem foodVFXInventory;

	[DontSave]
	public Transform foodVFXTransformInventory;

	[Header("Planet Puzzle")]
	public GameObject[] planetScreen;

	public MeshRenderer planetScreenMainMesh;

	public GameObject planetWinScreen;

	public Zoomable planetsZoom;

	[DontSave]
	public Ref<GameObject, Transform, HorizontalSlider> planetsScanner;

	[Tooltip("Array of planet GameObjects; Last 4 planets should be the undiscovered planets, with the last element being the final undiscovered planet")]
	[DontSave]
	public RefArray<GameObject, Transform, MaterialState> planets;

	[DontSave]
	public LightningBoltScript[] planetLines;

	[DontSave]
	public TMP_Text[] planetDistanceTexts;

	[DontSave]
	public Switch3D planetScanSwitch;

	private Vector3 planetsDragStartOffset;

	[DontSave]
	public MaterialState planetScanButtonMS;

	[DontSave]
	public GameObject planetsScanForMissingStars;

	[DontSave]
	public GameObject planetsAlphaDetected;

	[DontSave]
	public GameObject planetsCruxDetected;

	[DontSave]
	public GameObject planetsLeoDetected;

	[DontSave]
	public GameObject planetsSerpensDetected;

	[DontSave]
	public GameObject planetsScanScanning;

	[DontSave]
	public GameObject planetsScanNoStar;

	[DontSave]
	public GameObject planetsStarsFound;

	[DontSave]
	public GameObject planetsDataMissing;

	[DontSave]
	public GameObject planetsDataFound;

	[Header("Hologram Misc")]
	[DontSave]
	public GameObject starchartPuzzleComponents;

	[DontSave]
	public GameObject tractorbeamPuzzleComponents;

	[Header("Starchart Puzzle")]
	[DontSave]
	public GameObject[] starchartDrives;

	[DontSave]
	public Slot[] starchartSlots;

	[DontSave]
	public GameObject[] starchartDriveProgress;

	[DontSave]
	public Transform starchartDriveLoadingBar;

	[DontSave]
	public GameObject starchartDriveUploading;

	[DontSave]
	public GameObject starchartDriveMissing;

	[DontSave]
	public GameObject starchartDriveComplete;

	[DontSave]
	public GameObject[] starchartDriveHolograms;

	[DontSave]
	public GameObject starchartNewHolograms;

	[DontSave]
	public Transform starchartLoadingBar;

	[DontSave]
	public TweenState starchartLoadingBarTs;

	[DontSave]
	public GameObject starChartLoadingBarParent;

	[DontSave]
	public Switch3D[] starchartSelectSwitches;

	[DontSave]
	public TweenState[] starchartSelectTSs;

	[DontSave]
	public GameObject[] starchartPatterns;

	[DontSave]
	public MeshRenderer[] starchartStarRenderers;

	[DontSave]
	public GameObject[] starchartStarGos;

	[DontSave]
	public GameObject[] starchartSelectedIcon;

	[DontSave]
	public MeshRenderer[] starchartStarTextRenderers;

	[DontSave]
	public Material starchartStarGlow;

	[DontSave]
	public Material starchartStarDefault;

	[DontSave]
	public Switch3D[] starchartMarkerSwitches;

	private List<StarchartLine> starchartActiveLines;

	private int firstStarSelected = -1;

	[DontSave]
	public GameObject[] starchartStateTexts;

	[DontSave]
	public GameObject starChartTextParent;

	[DontSave]
	private List<(int, int)[]> starchartSolutionLines = new List<(int, int)[]>
	{
		new(int, int)[4]
		{
			(13, 11),
			(8, 11),
			(8, 9),
			(8, 10)
		},
		new(int, int)[7]
		{
			(21, 12),
			(12, 7),
			(14, 7),
			(14, 15),
			(14, 7),
			(15, 20),
			(17, 2)
		},
		new(int, int)[6]
		{
			(1, 19),
			(16, 19),
			(16, 3),
			(16, 22),
			(4, 22),
			(19, 22)
		}
	};

	private int starchartSectorsFound;

	[DontSave]
	public GameObject starchartLineBase;

	[DontSave]
	public GameObject starchartLineParent;

	public LineRenderer[] starchartLinePool;

	public GameObject[] starchartLinePoolGos;

	private Queue<int> availibleLines;

	private int starchartProgress;

	[Header("Tractorbeam Puzzle")]
	[DontSave]
	public Switch3D[] powerSwitches;

	[DontSave]
	public Ref<Switch3D, GameObject> powerResetSwitch;

	[DontSave]
	public MaterialState[] arrowsMats;

	[DontSave]
	private int[][] powerDirecitons = new int[14][]
	{
		new int[1] { 2 },
		new int[1],
		new int[1] { 4 },
		new int[1] { 2 },
		new int[3] { 5, 6, 13 },
		new int[1] { 8 },
		new int[1] { 4 },
		new int[1] { 6 },
		new int[1] { 9 },
		new int[2] { 8, 12 },
		new int[1] { 9 },
		new int[1] { 12 },
		new int[2] { 9, 13 },
		new int[1] { 4 }
	};

	public TextMeshProUGUI powerActiveCurrent;

	public GameObject tractorFieldPower;

	[DontSave]
	public Sequence sphereAnimSequence;

	[DontSave]
	public MaterialState shipFadeOut;

	[DontSave]
	public MaterialState magnetHoloshipFake;

	[DontSave]
	public GameObject magnetHoloshipPowerText;

	[DontSave]
	public GameObject holoshipSolveText;

	[DontSave]
	public GameObject solveHoloshipText;

	[DontSave]
	public GameObject magnetHoloshipFakeComponents;

	[DontSave]
	public Turnable magnetHoloshipTurnable;

	[DontSave]
	public MaterialState magnetAsteroidMaterialState;

	[DontSave]
	public MaterialState magnetAsteroidTextMaterialState;

	[DontSave]
	public GameObject[] magnetOffStatesText;

	[DontSave]
	public GameObject[] magnetOnStatesText;

	[DontSave]
	public Switch3D[] magnetToggleSwitches;

	[DontSave]
	public GameObject[] magnetNodes;

	[DontSave]
	public GameObject magneticSphere;

	public MaterialState[] powerSignMagnets;

	private bool[] magnetsActive;

	[DontSave]
	public LineRenderer magnetLineGuide;

	private GameObject magnetAttachedTo;

	public Trigger magnetButtonsTrigger;

	private bool asteroidFlashState;

	[Header("Handheld Tractorbeam")]
	[DontSave]
	public Item gun;

	[DontSave]
	public MaterialState gunMS;

	public Switch3D gunDial;

	public TweenState gunDialTs;

	public int gunValue = 2;

	[DontSave]
	public LineRenderer gunLaser;

	[DontSave]
	public GameObject gunLaserGO;

	[DontSave]
	public MaterialState gunLaserMS;

	[DontSave]
	public Transform gunLaserEndPoint;

	[DontSave]
	public GameObject[] gunLaserEffectsStart;

	[DontSave]
	public GameObject[] gunLaserEffectsEnd;

	[DontSave]
	public Transform gunLaserEffectsEndParent;

	[DontSave]
	public GameObject gunLaserEffectsEndParentGO;

	[DontSave]
	public Gradient gunLaserRedGradient;

	[DontSave]
	public Gradient gunLaserBlueGradient;

	[DontSave]
	private EventInstance gunSound;

	[Header("Distributor")]
	private bool[] distributorsOpened;

	[DontSave]
	public DistributionNode[] distribution;

	[DontSave]
	public Switch3D[] distributorButtons;

	[DontSave]
	public Switch3D[] distributorTargetButtons;

	[DontSave]
	public Transform[] distributorBarTargets;

	private Vector3[] distributorButtonOriginalPositions;

	[DontSave]
	public MaterialState[] distributorSourceHighlights;

	[DontSave]
	public MaterialState[] distributorTextHighlights;

	[DontSave]
	private int[] distributorEnergyValues = new int[6] { 1, 1, 3, 2, 2, 3 };

	private int[] distributorBarsInRow = new int[6] { -1, -1, -1, -1, -1, -1 };

	private int distributorCurrentHighlightedIndex = -1;

	private int[] distributorRowsFilled = new int[4];

	[DontSave]
	public Transform[] distributorVfxParents;

	[DontSave]
	public LineRenderer[] distributorVfxRenderers;

	[DontSave]
	public MeshRenderer[] distributorCircles;

	[DontSave]
	private Color distributorCirclesStartColor;

	[DontSave]
	private Color distributorCirclesStartEmissionColor;

	[ColorUsage(true, true)]
	[DontSave]
	public Color distributorCirclesGlowEmissionColor;

	[ColorUsage(true, true)]
	[DontSave]
	public Color distributorSplitHoverColor;

	[DontSave]
	public Color distributorCirclesGlowColor;

	[DontSave]
	public Item distributor;

	[DontSave]
	public Item distributorManual;

	[DontSave]
	public LineRenderer[] distributorLasers;

	[DontSave]
	public Interactive[] distributorRotatables;

	[DontSave]
	public Material distributionWireMaterial;

	[DontSave]
	private EventInstance distributionOnHoldEventInstance;

	[ColorUsage(true, true)]
	[DontSave]
	public Color distributionWireGlowEmissionColor;

	[DontSave]
	public Color distributionWireGlowNoiseColor;

	[ColorUsage(true, true)]
	[DontSave]
	public Color distributionWireGlowFresnelColor;

	public float noiseSpeed;

	[ColorUsage(true, true)]
	[DontSave]
	private Color distributionWireDefaultEmissionColor;

	[DontSave]
	private Color distributionWireDefaultNoiseColor;

	[ColorUsage(true, true)]
	[DontSave]
	private Color distributionWireDefaultFresnelColor;

	private float defaultNoiseSpeed;

	[DontSave]
	public MeshRenderer[] distributionWireRenderers;

	[DontSave]
	public MeshRenderer[] distributionWireGlowRenderers;

	[DontSave]
	public MeshRenderer[] distributionWireSplitGlowRenderers;

	[DontSave]
	public Vector2[] distributionWireGlowKeys;

	private int[] distributorSlotStates;

	[DontSave]
	public GameObject distributionLockedScreen;

	[DontSave]
	public GameObject distributionUnlockedScreen;

	[DontSave]
	public GameObject distribution_EnergyEmergency;

	[DontSave]
	public Zoomable distributionZoomable;

	private float[] distribtuionTargetPower = new float[11];

	[DontSave]
	private (int[], int)[] distributionPowerRequirements = new(int[], int)[6]
	{
		(new int[1], 2),
		(new int[1] { 1 }, 6),
		(new int[1] { 2 }, 2),
		(new int[3] { 3, 4, 5 }, 9),
		(new int[1] { 6 }, 2),
		(new int[4] { 7, 8, 9, 10 }, 12)
	};

	[DontSave]
	public TMP_Text[] distributionPowerTexts;

	[DontSave]
	public List<DistributionUIs> distributionUIs;

	[DontSave]
	public Switch3D[] distributionCompartmentSwitches;

	[DontSave]
	public TweenState[] distributionCompartmentTweens;

	[Header("Solar Panels")]
	[DontSave]
	public RefArray<Transform, GameObject, Dial> solarPanelTransforms;

	[DontSave]
	public Transform solarLightTransform;

	[DontSave]
	public Collider[] solarPanelDebrisColliders;

	[DontSave]
	public RefArray<MaterialState, Draggable, GameObject> debrisFade;

	[DontSave]
	public Transform debrisCenter;

	[DontSave]
	private List<Transform>[] solarPanelVertices = new List<Transform>[3];

	[DontSave]
	public MaterialState[] solarPanelMaterialStates_1;

	[DontSave]
	public MaterialState[] solarPanelMaterialStates_2;

	[DontSave]
	public MaterialState[] solarPanelMaterialStates_3;

	[DontSave]
	private List<MaterialState[]> solarPanelMaterialStates;

	[DontSave]
	public MaterialState[] solarPanelCenterMaterialStates;

	[DontSave]
	public GameObject[] solarPanelUnsolved;

	[DontSave]
	public GameObject[] solarPanelSolved;

	public TMP_Text[] solarPanelTexts;

	[DontSave]
	public GameObject[] solarPanelTextsGO;

	[DontSave]
	public Transform[] solarPanelScreenBarsRed;

	[DontSave]
	public Transform[] solarPanelScreenBarsYellow;

	[DontSave]
	public Transform[] solarPanelScreenBarsGreen;

	public GameObject[] solarPanelScreenBarsRedGo;

	public GameObject[] solarPanelScreenBarsYellowGo;

	public GameObject[] solarPanelScreenBarsGreenGo;

	[DontSave]
	public GameObject solarPanelScreenOffline;

	[DontSave]
	public GameObject solarPanelScreenOnline;

	[DontSave]
	public Zoomable solarPanelZoomable;

	private bool solarSolved;

	public ParticleSystem door1Open;

	public ParticleSystem door2Open;

	public Switch3D ventDoorSwitch;

	private double[] percentages = new double[3];

	[Header("Plasma Puzzle")]
	private bool plasmaAsteroidsSolved;

	private bool plasmaCablesSolved;

	private Vector3 plasmaCenter;

	[DontSave]
	public GameObject[] plasmaAsteroidTexts;

	[DontSave]
	public Rigidbody[] plasmaAsteroids;

	[DontSave]
	public Turnable[] plasmaAsteroidsTurnable;

	[DontSave]
	public Transform[] plasmaNodesTran;

	[DontSave]
	public TMP_Text asteroidDistanceText;

	private int asteroidsState;

	[DontSave]
	private Func<float, bool>[] plasmaConditions = new Func<float, bool>[3]
	{
		(float x) => x <= 9f,
		(float x) => x >= 8.35f && x <= 8.5f,
		(float x) => x >= 10.3f
	};

	[DontSave]
	public GameObject[] plasmaAsteroidSteps;

	[DontSave]
	public Transform plasmaLoadingBar;

	[DontSave]
	public Transform plasmaRestartLoadingBar;

	[DontSave]
	public LineRenderer asteroidDistanceLine;

	[DontSave]
	public Slot[] plasmaCableSlots;

	[DontSave]
	public GameObject[] plasmaCablesMissing;

	[DontSave]
	public GameObject[] plasmaCablesIncorrect;

	[DontSave]
	public GameObject[] plasmaCablesCorrect;

	[DontSave]
	public GameObject plasmaCablesDetachedScreen;

	[DontSave]
	public GameObject plasmaCablesAttachedScreen;

	[DontSave]
	public TweenState plasmaLid;

	[DontSave]
	public TweenState[] asteroidCasingTSs;

	[DontSave]
	public Zoomable plasmaZoomable;

	[DontSave]
	public GameObject asteroidCanvas;

	[DontSave]
	public Switch3D plasmaOpenContainerButton;

	[DontSave]
	public GameObject plasmaAsteroidsScreen;

	[DontSave]
	public MaterialState plasmaEnergyPulse;

	[DontSave]
	public GameObject plasmaStepsScreen;

	[DontSave]
	public GameObject plasmaAsteroidsLoadingScreen;

	[DontSave]
	public GameObject plasmaEnergyRestoredScreen;

	private bool updateCubesDist;

	private bool checkCubesFirstFrame;

	private float lastCubesDist;

	[Header("Aux Override Power")]
	[DontSave]
	public Switch3D auxOverrideSwitch;

	[DontSave]
	public Switch3D auxOverrideDelete;

	[SerializeField]
	public AuxNumber[] auxInputNumbers;

	private int[] auxOverrideState = new int[4] { -1, -1, -1, -1 };

	private int[] auxOverrideSolution = new int[4] { 5, 7, 2, 8 };

	private int auxCurrentIndex;

	[DontSave]
	[SerializeField]
	private AuxOverrideGraph auxGraphNode;

	[DontSave]
	public Switch3D[] auxGraphToggleDials;

	[DontSave]
	public Dial[] auxGraphDials;

	[DontSave]
	public Transform centerCircle;

	[DontSave]
	public GameObject[] auxNumbers;

	public TweenState auxChecking;

	public GameObject numberLines;

	public GameObject auxError;

	public GameObject auxCorrect;

	[DontSave]
	public MaterialState[] auxHighlightsAll;

	[DontSave]
	public GameObject[] auxHighlightsAllGo;

	private int currentMaxDepth;

	private bool[] chipsInSlot = new bool[4];

	public float DEBUG_HoloShipRotationSpeed;

	public GameObject[] redSolutions;

	public GameObject[] greenSolutions;

	public GameObject[] zeros;

	public GameObject[] ones;

	public Switch3D[] numberSwitches;

	public MeshRenderer comsOffline;

	public MeshRenderer comsOffline2;

	public MeshRenderer comsOnline;

	public Zoomable numbersZoomable;

	private int[,] centerTargets = new int[3, 5]
	{
		{ 2, 3, 3, 2, 0 },
		{ 2, 4, 3, 2, 2 },
		{ 2, 2, 2, 2, 2 }
	};

	private int[,] gridValues = new int[4, 6];

	public Switch3D hologramSwitch;

	public MaterialState hologramSwitchMS;

	[DontSave]
	public Ref<Switch3D, GameObject> levelExit;

	private bool holoshipGlowState;

	private bool tokensSolved;

	private bool suitEquipped;

	private bool[] distributorPreviouslyPowered = new bool[6];

	public override void onPacket(Packet packet)
	{
		if (packet is PlasmaDistancePacket plasmaDistancePacket)
		{
			lastCubesDist = plasmaDistancePacket.distance;
			asteroidDistanceLine.SetPositions(new Vector3[2]
			{
				plasmaAsteroids[0].position + 0.2f * Vector3.down,
				plasmaAsteroids[^1].position + 0.2f * Vector3.down
			});
			asteroidDistanceText.SetText(Math.Round(lastCubesDist * 10f, 2).ToString());
		}
	}

	private void updateLightFlicker()
	{
		float weight = flickeringLight.color.maxColorComponent * 255f / 220f * 2f - 1f;
		float weight2 = flickeringLight2.color.maxColorComponent * 255f / 220f * 2f - 1f;
		flickeringLightMS.setState("Flicker", weight);
		flickeringLightMS2.setState("Flicker", weight2);
	}

	private void zeroGUpdate(Trigger trigger, TriggerEvent triggerEvent)
	{
		zeroGRigidbodies.Clear();
		foreach (Interactive item in triggerEvent.interactivesInTrigger)
		{
			if (!(item == null) && item.TryGetComponent<Rigidbody>(out var _))
			{
				zeroGRigidbodies.Add(item.gameObject);
			}
		}
	}

	private void zeroGRespawnItem(Trigger trigger, TriggerEvent triggerEvent)
	{
		if (zeroGUnlocked)
		{
			return;
		}
		foreach (Interactive item2 in triggerEvent.interactivesEnteredThisEvent)
		{
			if (item2 is Item item && !trigger.keys.Contains(item.gameObject))
			{
				ItemType itemType = item.itemType;
				if ((itemType == ItemType.Clue || itemType == ItemType.Key) && game.hasAuthority(item))
				{
					game.addItemToInventory(item.gameObject);
				}
			}
		}
	}

	[DebugButton("Power Up Suit", Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void suitDoorUnlock()
	{
		suitDoorLowPower.SetActive(value: false);
		suitDispenseButton.gameObject.SetActive(value: true);
		suitDoorUnequipped.SetActive(value: true);
	}

	private void tokenOnSlot()
	{
		bool flag = Array.TrueForAll(tokenSlots, (Slot x) => x.insertedItem != null);
		tokenMissingChipsScreen.SetActive(!flag);
		tokenInsertedScreen.SetActive(flag);
		tokenWriteScreen.SetActive(value: false);
	}

	private void tokenOnConfirm()
	{
		Array.ForEach(tokenSymbolMaterialStates, delegate(MaterialState x)
		{
			x.setState("Default");
		});
		bool flag = Array.TrueForAll(tokenSlots, (Slot x) => x.insertedItem != null);
		tokenConfirmButton.targetable = false;
		Array.ForEach(tokenSlots, delegate(Slot x)
		{
			x.targetable = false;
		});
		foreach (Ref<Item, Transform> tokenItem in tokenItems)
		{
			tokenItem.Get<Item>(0).targetable = false;
		}
		if (!flag)
		{
			for (int num = 0; num < 4; num++)
			{
				game.startTimer(new TokenErrorTimer(num == 3), 0.5f * (float)num);
			}
			return;
		}
		tokenInsertedScreen.SetActive(value: false);
		tokenWriteScreen.SetActive(value: true);
		Array.ForEach(tokenSymbolGos, delegate(GameObject x)
		{
			x.SetActive(value: false);
		});
		tokenLoadingBar.unitTime = 0f;
		game.startTimer(new TokenLoadingTimer(), 2f);
		game.startTimer(new TokenCheckTimer(), 2f, 2f);
		for (int num2 = 0; num2 < 5; num2++)
		{
			tokenMeshFilters[num2].mesh = tokenSymbolMeshes[tokenItems.IndexOf(tokenSlots[num2].insertedItem, 0)];
		}
		for (int num3 = 0; num3 < 5; num3++)
		{
			int index = num3;
			game.startTimer(new TokenUpdateSequenceTimer(index), (float)num3 * 0.4f, 2f);
		}
	}

	private void tokenOnError(bool isFinal)
	{
		tokenErrorMaterialState.setState((tokenErrorMaterialState.getWeight("Error") > 0.5f) ? "Default" : "Error");
		if (!isFinal)
		{
			return;
		}
		tokenConfirmButton.targetable = true;
		Array.ForEach(tokenSlots, delegate(Slot x)
		{
			x.targetable = true;
		});
		foreach (Ref<Item, Transform> tokenItem in tokenItems)
		{
			tokenItem.Get<Item>(0).targetable = true;
		}
	}

	private void tokenOnUpdateSequence(int index)
	{
		tokenSymbolGos[index].SetActive(value: true);
	}

	private void tokenUpdateLoadingBar(float t)
	{
		tokenWriteScreen.SetActive(value: false);
		tokenLoadingScreen.SetActive(value: true);
		tokenLoadingBar.unitTime = t;
	}

	private void tokenCheckSolution()
	{
		tokenWriteScreen.SetActive(value: false);
		if (Array.TrueForAll(tokenSlots, (Slot x) => x.insertedItem == x.acceptItems[0]))
		{
			game.callRPC(RPCType.TokenSolve);
		}
		else
		{
			game.callRPC(RPCType.TokenFail);
		}
	}

	private void tokenOnLoaded()
	{
		tokenLoadingScreen.SetActive(value: false);
		tokenWriteScreen.SetActive(value: true);
	}

	[DebugButton(null, Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void getFuseItems()
	{
		foreach (Ref<Item, Transform> tokenItem in tokenItems)
		{
			game.addItemToInventory(tokenItem.Get<Item>(0).gameObject);
		}
	}

	private void tokenReset()
	{
		tokenDeniedScreen.SetActive(value: false);
		tokenWriteScreen.SetActive(value: true);
		Array.ForEach(tokenSymbolMaterialStates, delegate(MaterialState x)
		{
			x.setState("Error");
		});
		tokenConfirmButton.targetable = true;
		Array.ForEach(tokenSlots, delegate(Slot x)
		{
			x.targetable = true;
		});
		foreach (Ref<Item, Transform> tokenItem in tokenItems)
		{
			tokenItem.Get<Item>(0).targetable = true;
		}
	}

	private void planetsDragLoop()
	{
		(float, int)[] array = planetCalculateDistances();
		planetUpdateDistanceTexts(array);
		planetUpdateHighlights(array);
		for (int i = 0; i < planetLines.Length; i++)
		{
			int item = array[i].Item2;
			planetLines[i].StartObject = planetsScanner.Get<GameObject>(0);
			planetLines[i].EndObject = planets[item];
		}
	}

	[DebugButton("Solve Navigation (Planets)", Tint.Blue, (PostClickAction)0, 0, new object[] { true })]
	private void planetsScan(bool debug = false)
	{
		if (debug)
		{
			planets.Get<GameObject>(9, 0).SetActive(value: true);
			planets.Get<GameObject>(10, 0).SetActive(value: true);
			planets.Get<GameObject>(11, 0).SetActive(value: true);
			planets.Get<GameObject>(12, 0).SetActive(value: true);
		}
		planetScanSwitch.targetable = false;
		planetsScanner.Get<HorizontalSlider>(0u).targetable = false;
		planetScanButtonMS.setState("Processing");
		planetsDataMissing.SetActive(value: false);
		for (int i = 0; i < 4; i++)
		{
			int num = i;
			game.startTimer(new PlanetSendFlashTimer(), 0.5f + 0.5f * (float)num);
		}
		game.startTimer(new PlanetSendWaitTimer(), 2f);
	}

	private (float, int)[] planetCalculateDistances()
	{
		(float, int)[] array = new(float, int)[planets.Length];
		for (int i = 0; i < planets.Length; i++)
		{
			int item = i;
			float item2 = Vector3.Distance(planets.Get<Transform>(i, 0f).position, planetsScanner.Get<Transform>(0f).position) * 1866.5863f;
			array[i] = (item2, item);
			if (!planets.Get<GameObject>(i, 0).activeSelf)
			{
				array[i].Item1 = float.PositiveInfinity;
			}
		}
		Array.Sort(array, ((float, int) x, (float, int) y) => x.Item1.CompareTo(y.Item1));
		return array;
	}

	private void planetUpdateHighlights((float, int)[] distances)
	{
		foreach (Ref<GameObject, Transform, MaterialState> planet in planets)
		{
			planet.Get<MaterialState>(0u).setState("Default");
		}
		for (int i = 0; i < 3; i++)
		{
			int item = distances[i].Item2;
			UnityEngine.Debug.Log(planets.Get<GameObject>(item, 0));
			planets.Get<MaterialState>(item, 0u).setState("Highlighted");
		}
	}

	private void planetUpdateDistanceTexts((float, int)[] distances)
	{
		for (int i = 0; i < planetDistanceTexts.Length; i++)
		{
			(float, int) tuple = distances[i];
			float item = tuple.Item1;
			int item2 = tuple.Item2;
			Vector3 position = planets.Get<Transform>(item2, 0f).position;
			Vector3 position2 = planetsScanner.Get<Transform>(0f).position;
			Vector3 position3 = (position + position2) / 2f;
			planetDistanceTexts[i].transform.position = position3;
			planetDistanceTexts[i].transform.localPosition = new Vector3(planetDistanceTexts[i].transform.localPosition.x, planetDistanceTexts[i].transform.localPosition.y, -2.71f);
			if (item >= 100f)
			{
				planetDistanceTexts[i].SetText(((int)item).ToString());
			}
			else
			{
				planetDistanceTexts[i].SetText(string.Empty);
			}
		}
	}

	private void initStarchart()
	{
		game.startSwitch(starchartSelectSwitches[0]);
		starchartActiveLines = new List<StarchartLine>();
		availibleLines = new Queue<int>();
		for (int i = 0; i < 231; i++)
		{
			availibleLines.Enqueue(i);
		}
	}

	private void starchartOnMarkerSwitch(int index)
	{
		if (firstStarSelected == -1)
		{
			firstStarSelected = index;
			starchartStarRenderers[index].material = starchartStarGlow;
			starchartStarTextRenderers[index].material = starchartStarGlow;
			starchartSelectedIcon[index].SetActive(value: true);
			return;
		}
		if (index == firstStarSelected)
		{
			starchartStarRenderers[index].material = starchartStarDefault;
			starchartStarTextRenderers[index].material = starchartStarDefault;
			starchartSelectedIcon[index].SetActive(value: false);
			firstStarSelected = -1;
			return;
		}
		StarchartLine starchartLine = new StarchartLine();
		starchartLine.point1 = firstStarSelected;
		starchartLine.point2 = index;
		StarchartLine starchartLine2 = null;
		foreach (StarchartLine starchartActiveLine in starchartActiveLines)
		{
			if ((starchartActiveLine.point1 == firstStarSelected && starchartActiveLine.point2 == index) || (starchartActiveLine.point1 == index && starchartActiveLine.point2 == firstStarSelected))
			{
				starchartLine2 = starchartActiveLine;
				break;
			}
		}
		if (starchartLine2 == null)
		{
			if (availibleLines.Count > 0)
			{
				int num = (starchartLine.lineIndex = availibleLines.Dequeue());
				starchartActiveLines.Add(starchartLine);
				starchartLinePoolGos[num].SetActive(value: true);
				starchartLinePool[num].positionCount = 2;
				starchartLinePool[num].SetPosition(0, starchartMarkerSwitches[starchartLine.point1].transform.position);
				starchartLinePool[num].SetPosition(1, starchartMarkerSwitches[starchartLine.point2].transform.position);
			}
		}
		else
		{
			starchartActiveLines.Remove(starchartLine2);
			starchartLinePoolGos[starchartLine2.lineIndex].SetActive(value: false);
			availibleLines.Enqueue(starchartLine2.lineIndex);
		}
		starchartStarRenderers[firstStarSelected].material = starchartStarDefault;
		starchartStarTextRenderers[firstStarSelected].material = starchartStarDefault;
		starchartSelectedIcon[firstStarSelected].SetActive(value: false);
		firstStarSelected = -1;
	}

	private void starchartDriveOnSlot()
	{
		starchartProgress++;
		starchartDriveProgress[starchartProgress - 1].SetActive(value: false);
		starchartDriveProgress[starchartProgress].SetActive(value: true);
		if (starchartProgress == 3)
		{
			starchartDriveProgress[3].SetActive(value: false);
			starchartDriveMissing.SetActive(value: false);
			starchartDriveUploading.SetActive(value: true);
			game.startTimer(new StarchartDriveCompleteTimer(), 2f);
			Array.ForEach(starchartSlots, delegate(Slot x)
			{
				Game obj = game;
				Transform obj2 = x.transform;
				Quaternion? rotation = Quaternion.identity;
				obj.startTransitionLocal(obj2, 2f, 0f, null, rotation);
			});
		}
	}

	[DebugButton("Get Starchart Drives", Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void getStarchartDrives()
	{
		Array.ForEach(starchartDrives, delegate(GameObject x)
		{
			game.addItemToInventory(x);
		});
	}

	private void starchartOnSend()
	{
		for (int i = 0; i < starchartSolutionLines.Count; i++)
		{
			(int, int)[] array = starchartSolutionLines[i];
			bool flag = true;
			if (array.Length != starchartActiveLines.Count)
			{
				flag = false;
			}
			else
			{
				for (int j = 0; j < array.Length; j++)
				{
					(int, int) tuple = array[j];
					bool flag2 = false;
					foreach (StarchartLine starchartActiveLine in starchartActiveLines)
					{
						if ((starchartActiveLine.point1 == tuple.Item1 && starchartActiveLine.point2 == tuple.Item2) || (starchartActiveLine.point1 == tuple.Item2 && starchartActiveLine.point2 == tuple.Item1))
						{
							flag2 = true;
						}
					}
					if (!flag2)
					{
						flag = false;
					}
				}
			}
			if (flag)
			{
				game.callRPC(RPCType.StarchartPart);
				break;
			}
		}
	}

	[DebugButton("Solve Starchart", Tint.Blue, (PostClickAction)0, 0, new object[] { })]
	private void starchartSolve()
	{
		Array.ForEach(starchartStateTexts, delegate(GameObject x)
		{
			x.SetActive(value: false);
		});
		crashLogSent = true;
		crashLogMissingText_1.SetActive(value: false);
		crashLogMissingText_2.SetActive(value: false);
		crashLogMissingText_3.SetActive(value: false);
		crashLogFoundText_1.SetActive(value: true);
		crashLogFoundText_2.SetActive(value: true);
		checkCrashState();
		starchartPuzzleComponents.SetActive(value: false);
		magnetHoloshipFakeComponents.SetActive(value: true);
		starChartLoadingBarParent.SetActive(value: false);
		magnetHoloshipPowerText.SetActive(value: true);
		game.finishPuzzle(Puzzle.StarCharts);
	}

	private void onPowerSwitches(int index)
	{
		int[] array = powerDirecitons[index];
		foreach (int num in array)
		{
			int num2 = ((powerSwitches[num].materialState.getTargetWeight("Online") != 1f) ? 1 : 0);
			powerSwitches[num].materialState.setState("Online", num2);
		}
		int num3 = ((powerSwitches[index].materialState.getTargetWeight("Online") != 1f) ? 1 : 0);
		powerSwitches[index].materialState.setState("Online", num3);
		int num4 = 0;
		Switch3D[] array2 = powerSwitches;
		for (int i = 0; i < array2.Length; i++)
		{
			if (array2[i].materialState.getTargetWeight("Online") == 1f)
			{
				num4++;
			}
		}
		powerActiveCurrent.text = num4 + "/14";
		if (num4 == 14)
		{
			game.callRPC(RPCType.Magnet);
		}
	}

	private void magnetHoloshipOnTurned()
	{
		Array.ForEach(magnetToggleSwitches, delegate(Switch3D x)
		{
			x.transform.rotation = Quaternion.identity;
		});
		if ((magnetHoloshipTurnable.currentRotation == magnetHoloshipTurnable.angleLimit1 || magnetHoloshipTurnable.currentRotation == magnetHoloshipTurnable.angleLimit2) && game.getTimers<AsteroidFlashTimer>().Count == 0)
		{
			game.startTimer(new AsteroidFlashTimer(), 0.3f);
			game.startTimer(new AsteroidFlashTimer(), 0.6f);
			game.startTimer(new AsteroidFlashTimer(), 0.9f);
			asteroidFlashState = true;
			magnetAsteroidMaterialState.setState("Glow");
			magnetAsteroidTextMaterialState.setState("Glow");
		}
	}

	private void magnetOnToggle(int index)
	{
		for (int i = 0; i < magnetsActive.Length; i++)
		{
			if (i != index && magnetsActive[i])
			{
				magnetsActive[i] = false;
				magnetOnStatesText[i].SetActive(value: false);
				magnetOffStatesText[i].SetActive(value: true);
			}
		}
		magnetsActive[index] = !magnetsActive[index];
		magnetAttachedTo = null;
		for (int j = 0; j < powerSignMagnets.Length; j++)
		{
			powerSignMagnets[j].setWeight("NewState", magnetsActive[j] ? 1 : 0);
		}
		magnetOnStatesText[index].SetActive(magnetsActive[index]);
		magnetOffStatesText[index].SetActive(!magnetsActive[index]);
		giantSphereTS.transitionToDuration("Moved", 2f);
	}

	[DebugButton(null, Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void solveMagnet()
	{
		game.startTimer(new HoloshipCompleteTimer(), 9f);
		game.startTimer(new PowerShipFinalAnimTimer(), 2f);
		game.startTimer(new SwitchesPowerOnOffTimer(0), 0.5f);
		tractorFieldPower.SetActive(value: false);
		Switch3D[] array = powerSwitches;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = false;
		}
		powerResetSwitch.Get<Switch3D>(0).targetable = false;
		game.finishPuzzle(Puzzle.Tractors);
	}

	[DebugButton("Solve Communication (Bit Parity)", Tint.Blue, (PostClickAction)0, 0, new object[] { })]
	private void solveBP()
	{
		Array.ForEach(numberSwitches, delegate(Switch3D x)
		{
			x.targetable = false;
		});
		communicationOnline = true;
		communicationOfflineText_1.SetActive(value: false);
		communicationOfflineText_2.SetActive(value: false);
		communicationOfflineText_3.SetActive(value: false);
		communicationOnlineText_1.SetActive(value: true);
		communicationOnlineText_2.SetActive(value: true);
		game.increaseZoomCounter(numbersZoomable);
		numbersZoomable.targetable = false;
		game.startTimer(new SolveNumbersAnimTimer(1), 0.3f);
		game.finishPuzzle(Puzzle.BinarySudoku);
	}

	private void gunStart()
	{
		gunLaserGO.SetActive(value: true);
		gunLaserEffectsEndParentGO.SetActive(value: true);
		PineFmod.playOneShotSoundAttached("event:/Sound Effects/04 Items/Weapons/Handheld Tractor Beam/Handheld_Tractor_Beam_Start", gun.gameObject);
	}

	private void gunUpdate(ToolContext context)
	{
		Interactive currentTarget = context.currentTarget;
		Vector3 position = ((currentTarget != null) ? context.currentTargetHitPoint : game.playerViewRay.GetPoint(100f));
		if (currentTarget != null && (currentTarget.GetType() == typeof(Item) || currentTarget.GetType() == typeof(Draggable)))
		{
			Vector3 vector = game.getPlayerWithItemInInventory(gun.gameObject).playerCameraRay.GetPoint(0.6f) - currentTarget.transform.position;
			Vector3 vector2 = ((vector.magnitude >= 0.1f) ? vector.normalized : Vector3.zero);
			vector2 = ((gunValue == 0) ? vector2 : (-vector2));
			Vector3 vector3 = Vector3.zero;
			if (currentTarget is Draggable draggable)
			{
				vector3 = draggable.draggableRigidbody.linearVelocity;
			}
			if (currentTarget is Item item)
			{
				item.hasRigidbody = true;
				Rigidbody component = item.GetComponent<Rigidbody>();
				if (component != null)
				{
					vector3 = component.linearVelocity;
				}
			}
			Vector3 vector4 = vector;
			Vector3 zero = Vector3.zero;
			if (gunValue == 1)
			{
				Vector3 vector5 = Vector3.ClampMagnitude(4f * vector4, 40f) - vector3;
				zero = Vector3.ClampMagnitude(50f * vector5, 260f);
			}
			else
			{
				zero = -vector4.normalized * 150f;
			}
			if (currentTarget is Item)
			{
				Item item2 = (Item)currentTarget;
				item2.rbOverrides = new RigidbodyOverrides
				{
					addForce = zero,
					addForceMode = ForceMode.Acceleration
				};
				if (tokenItems.IndexOf(item2, 0) != -1)
				{
					item2.interactionMaxDistance = 1f;
				}
			}
			if (currentTarget is Draggable)
			{
				((Draggable)currentTarget).draggableRigidbody.AddForce(zero, ForceMode.Acceleration);
			}
		}
		gunLaserEffectsEndParent.position = position;
	}

	private void gunEnd()
	{
		gunLaserGO.SetActive(value: false);
		PineFmod.playOneShotSoundAttached("event:/Sound Effects/04 Items/Weapons/Handheld Tractor Beam/Handheld_Tractor_Beam_End", gun.gameObject);
		gunLaserEffectsEndParentGO.SetActive(value: false);
	}

	private void distributorOnButton(int index)
	{
		for (int i = 0; i < distributorTargetButtons.Length; i++)
		{
			distributorTargetButtons[i].targetable = false;
			distributorSourceHighlights[i].setState("Default");
		}
		if (index == distributorCurrentHighlightedIndex && distributorBarsInRow[index] == -1)
		{
			distributorCurrentHighlightedIndex = -1;
			distributorButtons[index].materialState.setState("Default");
			distributorButtons[index].materialState.findStateByName("HoverDisabled").name = "Hover";
			return;
		}
		if (distributorCurrentHighlightedIndex != -1 && distributorCurrentHighlightedIndex != index)
		{
			distributorButtons[distributorCurrentHighlightedIndex].materialState.setState("Default");
			distributorButtons[distributorCurrentHighlightedIndex].materialState.findStateByName("HoverDisabled").name = "Hover";
		}
		if (distributorBarsInRow[index] == -1)
		{
			distributorCurrentHighlightedIndex = index;
			distributorButtons[index].materialState.setState("Highlighted");
			distributorButtons[index].materialState.findStateByName("Hover").name = "HoverDisabled";
			for (int j = 0; j < distributorTargetButtons.Length; j++)
			{
				if (distributorEnergyValues[index] + distributorRowsFilled[j] <= 3)
				{
					distributorTargetButtons[j].targetable = true;
					distributorSourceHighlights[j].setState("Highlighted");
				}
			}
			return;
		}
		for (int k = 0; k < distributorButtons.Length; k++)
		{
			if (distributorBarsInRow[k] == distributorBarsInRow[index])
			{
				distributorButtons[k].transform.position = distributorBarTargets[distributorBarsInRow[k]].GetChild(0).position;
			}
		}
		distributorRowsFilled[distributorBarsInRow[index]] -= distributorEnergyValues[index];
		distributorBarsInRow[index] = -1;
		distributorButtons[index].transform.position = distributorButtonOriginalPositions[index];
		if ((index == 0 || index == 1) && distributorBarsInRow[0] == -1 && distributorBarsInRow[1] == -1)
		{
			distributorButtons[index].transform.position = distributorButtonOriginalPositions[1];
		}
		else if (index == 0 || index == 1)
		{
			distributorButtons[index].transform.position = distributorButtonOriginalPositions[0];
		}
	}

	private void distributorOnPlace(int index)
	{
		for (int i = 0; i < distributorTargetButtons.Length; i++)
		{
			distributorTargetButtons[i].targetable = false;
			distributorSourceHighlights[i].setState("Default");
		}
		distributorButtons[distributorCurrentHighlightedIndex].transform.position = distributorBarTargets[index].GetChild(distributorRowsFilled[index]).position;
		distributorRowsFilled[index] += distributorEnergyValues[distributorCurrentHighlightedIndex];
		distributorBarsInRow[distributorCurrentHighlightedIndex] = index;
		distributorButtons[distributorCurrentHighlightedIndex].materialState.setState("Default");
		distributorButtons[distributorCurrentHighlightedIndex].materialState.findStateByName("HoverDisabled").name = "Hover";
		distributorCurrentHighlightedIndex = -1;
		if ((index == 0 || index == 1) && (distributorBarsInRow[0] == -1 || distributorBarsInRow[1] == -1))
		{
			distributorButtons[(distributorBarsInRow[0] != -1) ? 1u : 0u].transform.position = distributorButtonOriginalPositions[0];
		}
	}

	private void solarUpdate()
	{
		if (game.isSplitscreen && solarPanelVertices[0].Count == 0)
		{
			for (int i = 0; i < solarPanelVertices.Length; i++)
			{
				int num = (int)Math.Sqrt(676.0);
				solarPanelVertices[i] = new List<Transform>();
				Vector3 vector = solarPanelTransforms.Get<Transform>(i, 0).position + solarPanelTransforms.Get<Transform>(i, 0).forward * 5f;
				float num2 = 2f / (float)num;
				for (int j = -(num / 2); j <= num / 2; j++)
				{
					for (int k = -(num / 2); k <= num / 2; k++)
					{
						RaycastHit[] array = Physics.RaycastAll(new Ray(vector, solarPanelTransforms.Get<Transform>(i, 0).position - vector + solarPanelTransforms.Get<Transform>(i, 0).up * num2 * k + solarPanelTransforms.Get<Transform>(i, 0).right * num2 * j), float.PositiveInfinity);
						for (int l = 0; l < array.Length; l++)
						{
							RaycastHit raycastHit = array[l];
							if (raycastHit.collider.gameObject == solarPanelTransforms[i].Get<GameObject>(0f))
							{
								GameObject gameObject = new GameObject();
								game.setParent(gameObject.transform, solarPanelTransforms[i]);
								gameObject.transform.position = raycastHit.point;
								solarPanelVertices[i].Add(gameObject.transform);
							}
						}
					}
				}
			}
			return;
		}
		foreach (Ref<MaterialState, Draggable, GameObject> item in debrisFade)
		{
			if (item.Get<MaterialState>(0).getTargetWeight("NewState") == 0f && Vector3.Distance(item.Get<Draggable>(0f).transform.position, debrisCenter.position) > 6f)
			{
				item.Get<MaterialState>(0).transitionTo("NewState");
				item.Get<Draggable>(0f).targetable = false;
			}
		}
		if (solarSolved)
		{
			return;
		}
		for (int m = 0; m < solarPanelVertices.Length; m++)
		{
			float num3 = solarPanelVertices[m].Count;
			for (int n = 0; n < solarPanelVertices[m].Count; n++)
			{
				Ray ray = new Ray(solarLightTransform.position, solarPanelVertices[m][n].position - solarLightTransform.position);
				for (int num4 = 0; num4 < solarPanelDebrisColliders.Length; num4++)
				{
					if ((m != 0 || num4 != 4) && (m != 1 || num4 != 5) && (m != 2 || num4 != 6) && (m != 0 || num4 != 6) && (m != 1 || num4 != 4))
					{
						Collider collider = solarPanelDebrisColliders[num4];
						if (!(collider.gameObject == solarPanelTransforms[m].Get<GameObject>(0f)) && collider.Raycast(ray, out var _, float.PositiveInfinity))
						{
							num3 -= 1f;
							break;
						}
					}
				}
			}
			float num5 = num3 / (float)solarPanelVertices[m].Count * 100f;
			if (num5 >= 97f)
			{
				num5 = 100f;
			}
			bool flag = num5 < 70f;
			bool flag2 = num5 >= 70f && num5 < 100f;
			string text = (flag ? "Red" : (flag2 ? "Yellow" : "Green"));
			Color32 color = (flag ? new Color32(byte.MaxValue, 0, 0, byte.MaxValue) : (flag2 ? new Color32(241, byte.MaxValue, 96, byte.MaxValue) : new Color32(1, byte.MaxValue, 0, byte.MaxValue)));
			solarPanelScreenBarsRedGo[m].SetActive(flag);
			solarPanelScreenBarsYellowGo[m].SetActive(flag2);
			solarPanelScreenBarsGreenGo[m].SetActive(!flag && !flag2);
			solarPanelScreenBarsRed[m].localScale = new Vector3(num5 / 100f, 1f, 1f);
			solarPanelScreenBarsYellow[m].localScale = new Vector3(num5 / 100f, 1f, 1f);
			solarPanelScreenBarsGreen[m].localScale = new Vector3(num5 / 100f, 1f, 1f);
			for (int num6 = 0; (float)num6 < num5 / 2f; num6++)
			{
				if (solarPanelMaterialStates[m][num6].getTargetWeight(text + "Active") < 0.5f)
				{
					solarPanelMaterialStates[m][num6].setState(text + "Active");
				}
			}
			for (int num7 = (int)num5 / 2; num7 < 50; num7++)
			{
				if (solarPanelMaterialStates[m][num7].getTargetWeight(text + "Passive") < 0.5f)
				{
					solarPanelMaterialStates[m][num7].setState(text + "Passive");
				}
			}
			solarPanelCenterMaterialStates[m].setState(text + "Active");
			solarPanelTexts[m].color = color;
			solarPanelTexts[m].SetText(num5.ToString("F0") + "%");
			solarPanelTexts[m + 3].color = color;
			solarPanelTexts[m + 3].SetText(num5.ToString("F0") + "%");
			percentages[m] = num5;
		}
	}

	private void plasmaCablesOnSlot(int index)
	{
		bool flag = plasmaCableSlots[index].insertedItem != null;
		bool flag2 = flag && plasmaCableSlots[index].insertedItem == plasmaCableSlots[index].acceptItems[0];
		plasmaCablesMissing[index].SetActive(!flag);
		plasmaCablesIncorrect[index].SetActive(flag && !flag2);
		plasmaCablesCorrect[index].SetActive(flag2);
		plasmaCablesSolved = plasmaCableSlots[0].insertedItem == plasmaCableSlots[0].acceptItems[0] && plasmaCableSlots[1].insertedItem == plasmaCableSlots[1].acceptItems[0] && plasmaCableSlots[2].insertedItem == plasmaCableSlots[2].acceptItems[0];
		checkPlasmaCablesSolved();
		[DebugButton("Solve Plasma Cables", Tint.Default, PostClickAction.ReturnToGame, 0, new object[] { true })]
		void checkPlasmaCablesSolved(bool force = false)
		{
			if (force)
			{
				plasmaCablesSolved = true;
			}
			if (plasmaCablesSolved)
			{
				plasmaLid.transitionToDuration("Closed");
				plasmaAsteroidTexts[0].SetActive(value: false);
				plasmaAsteroidTexts[1].SetActive(value: true);
				asteroidCanvas.SetActive(value: true);
				plasmaCableSlots[0].targetable = false;
				plasmaCableSlots[1].targetable = false;
				plasmaCableSlots[2].targetable = false;
				if (!force)
				{
					plasmaCableSlots[0].insertedItem.targetable = false;
					plasmaCableSlots[1].insertedItem.targetable = false;
					plasmaCableSlots[2].insertedItem.targetable = false;
				}
				cutsceneCablesSolved.SetActive(value: true);
				game.startTimer(new CutsceneCablesSolvedTimer(0), 1.5f);
			}
		}
	}

	[DebugButton("Open Asteriod Casings", Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void plasmaButtonOnClick()
	{
		plasmaCablesAttachedScreen.SetActive(value: false);
		plasmaAsteroidsScreen.SetActive(value: true);
		Array.ForEach(plasmaAsteroidsTurnable, delegate(Turnable x)
		{
			x.targetable = true;
		});
		Array.ForEach(asteroidCasingTSs, delegate(TweenState x)
		{
			x.transitionToDuration("Open");
		});
		game.finishPuzzle(Puzzle.PowerCables);
	}

	private void plasmaUpdate()
	{
		if (plasmaAsteroidsSolved)
		{
			return;
		}
		bool flag = false;
		if (Array.Exists(plasmaAsteroids, (Rigidbody x) => game.isAnyPlayerInteracting(x.gameObject)))
		{
			for (int num = 0; num < plasmaAsteroids.Length; num++)
			{
				plasmaAsteroids[num].linearVelocity = Vector3.zero;
				if (Vector3.Distance(plasmaAsteroids[num].position, plasmaNodesTran[num].position) <= Time.deltaTime * 1.2f)
				{
					plasmaAsteroids[num].position = plasmaNodesTran[num].position;
				}
				else
				{
					plasmaAsteroids[num].position += (plasmaNodesTran[num].position - plasmaAsteroids[num].position).normalized * Time.deltaTime * 1.2f;
				}
			}
			checkCubesFirstFrame = true;
			flag = true;
			updateCubesDist = true;
		}
		else
		{
			if (checkCubesFirstFrame)
			{
				game.cancelTimers<PlasmaCubesDistTimer>();
				game.startTimer(new PlasmaCubesDistTimer(), 1.5f);
				checkCubesFirstFrame = false;
			}
			Rigidbody[] array = plasmaAsteroids;
			foreach (Rigidbody rigidbody in array)
			{
				rigidbody.AddForce((plasmaCenter - rigidbody.position) * Time.deltaTime * 30f, ForceMode.Acceleration);
			}
		}
		if (updateCubesDist)
		{
			if (game.session.isHost())
			{
				lastCubesDist = Vector3.Distance(plasmaAsteroids[0].position, plasmaAsteroids[^1].position);
				asteroidDistanceLine.SetPositions(new Vector3[2]
				{
					plasmaAsteroids[0].position + 0.2f * Vector3.down,
					plasmaAsteroids[^1].position + 0.2f * Vector3.down
				});
				asteroidDistanceText.SetText(Math.Round(lastCubesDist * 10f, 2).ToString());
				game.session.send(new PlasmaDistancePacket
				{
					distance = lastCubesDist
				});
			}
		}
		else
		{
			if (flag)
			{
				return;
			}
			if (plasmaConditions[asteroidsState](lastCubesDist * 10f) && Array.TrueForAll(plasmaAsteroids, (Rigidbody x) => !game.isAnyPlayerInteracting(x.gameObject)))
			{
				if (game.getTimers<AsteroidSolveTimer>().Count == 0)
				{
					game.startTimer(new AsteroidSolveTimer(), 2f);
				}
			}
			else
			{
				game.cancelTimers<AsteroidSolveTimer>();
			}
		}
	}

	private void asteroidTimerDone(Timer timer)
	{
		plasmaLoadingBar.localScale = Vector3.zero;
		if (timer.isCancelled)
		{
			return;
		}
		asteroidsState++;
		if (asteroidsState == 3)
		{
			plasmaAsteroidsSolved = true;
			Array.ForEach(plasmaAsteroidsTurnable, delegate(Turnable x)
			{
				x.targetable = false;
			});
			plasmaStepsScreen.SetActive(value: false);
			plasmaAsteroidsLoadingScreen.SetActive(value: true);
			game.startTimer(new PlasmaCompleteTimer(), 2f);
		}
		else
		{
			plasmaAsteroidSteps[asteroidsState - 1].SetActive(value: false);
			plasmaAsteroidSteps[asteroidsState].SetActive(value: true);
		}
	}

	[DebugButton("Solve Asteroids Puzzle", Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void plasmaCompleteTimerDone()
	{
		plasmaAsteroidsLoadingScreen.SetActive(value: false);
		plasmaEnergyRestoredScreen.SetActive(value: true);
		plasmaEnergyPulse.transitionToDuration("Pulse");
		asteroidsSolvePuzzle();
	}

	private void asteroidsSolvePuzzle()
	{
		Array.ForEach(asteroidCasingTSs, delegate(TweenState x)
		{
			x.transitionToDuration("Default");
		});
		game.increaseZoomCounter(plasmaZoomable);
		plasmaZoomable.targetable = false;
		activatePlasma();
		game.finishPuzzle(Puzzle.Plasmids);
	}

	private void activatePlasma()
	{
		if (plasmaAsteroidsSolved && plasmaCablesSolved)
		{
			cutscenePowerAdded.SetActive(value: true);
			game.startTimer(new CutscenePowerAddedTimer(0, 1, 2), 1.5f);
		}
	}

	private int auxBfs()
	{
		currentMaxDepth = 0;
		auxGraphNode.nodesToFlash = new bool[16];
		int num = 0;
		Queue<(int, int)> queue = new Queue<(int, int)>();
		List<int> list = new List<int>();
		queue.Enqueue((0, 0));
		list.Add(0);
		while (queue.Count > 0)
		{
			var (num2, num3) = queue.Dequeue();
			if (num3 > currentMaxDepth)
			{
				currentMaxDepth = num3;
			}
			AuxOverrideGraphNode auxOverrideGraphNode = auxGraphNode.nodes[num2];
			game.startTimer(new AuxNodeHighlightDelayTimer(auxOverrideGraphNode.highlightGo), 0.2f * (float)num3 + 0.1f);
			int[] adjacentNodes = auxOverrideGraphNode.getAdjacentNodes();
			int[] connections = auxOverrideGraphNode.getConnections();
			foreach (int num4 in connections)
			{
				if (num4 != -1 && auxOverrideGraphNode.adjacentHighlightsGo[num4] != null)
				{
					game.startTimer(new AuxNodeHighlightDelayTimer(auxOverrideGraphNode.adjacentHighlightsGo[num4]), 0.2f * (float)num3 + 0.2f);
				}
				int num5 = adjacentNodes[num4];
				if (num5 == -1 || list.Contains(num5))
				{
					continue;
				}
				if (num5 >= 7)
				{
					AuxOverrideGraphNode auxOverrideGraphNode2 = auxGraphNode.nodes[num5];
					num++;
					game.startTimer(new AuxNodeHighlightDelayTimer(auxOverrideGraphNode2.highlightGo), 0.2f * (float)num3 + 0.2f);
					auxGraphNode.nodesToFlash[num5] = true;
					continue;
				}
				AuxOverrideGraphNode auxOverrideGraphNode3 = auxGraphNode.nodes[num5];
				if (Array.IndexOf(auxOverrideGraphNode3.getConnections(), (num4 + 3) % 6) != -1)
				{
					queue.Enqueue((num5, num3 + 1));
					list.Add(num5);
				}
			}
		}
		return num;
	}

	private void auxOnConfirm()
	{
		if (auxCurrentIndex >= 4)
		{
			auxCurrentIndex = 3;
		}
		if (auxCurrentIndex == 3)
		{
			auxOverrideSwitch.materialState.transitionTo("Disabled", 5f);
		}
		int result = auxBfs();
		Game obj = game;
		Transform obj2 = centerCircle;
		Vector3? scale = Vector3.one * 1.695632f;
		obj.startTransitionLocal(obj2, 0.1f, 0f, null, null, scale);
		for (int i = 0; i < auxGraphNode.nodesToFlash.Length; i++)
		{
			int num = i;
			if (auxGraphNode.nodesToFlash[num])
			{
				game.startTimer(new AuxNodeFlashTimer(num, 1), (float)currentMaxDepth * 0.2f + 0.1f);
				game.startTimer(new AuxNodeFlashTimer(num, 0), (float)currentMaxDepth * 0.2f + 0.4f);
				game.startTimer(new AuxNodeFlashTimer(num, 1), (float)currentMaxDepth * 0.2f + 0.7f);
				game.startTimer(new AuxNodeFlashTimer(num, 0), (float)currentMaxDepth * 0.2f + 1f);
			}
		}
		game.startTimer(new AuxSwitchPauseTimer(result), 1.55f);
	}

	[DebugButton(null, Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void solveAuxPuzzle()
	{
		auxFinalScreen.SetActive(value: true);
		auxPuzzleScreen.SetActive(value: false);
		auxPowerOfflineText.SetActive(value: false);
		auxPowerOnlineText.SetActive(value: true);
		auxHintOfflineText.SetActive(value: false);
		auxHintOnlineText.SetActive(value: true);
		cutscenePowerAdded.SetActive(value: true);
		game.startTimer(new CutscenePowerAddedTimer(0, 3, 4), 1.5f);
		game.finishPuzzle(Puzzle.HexagonInput);
	}

	private void auxOnDelete()
	{
		auxOverrideSwitch.targetable = true;
		auxOverrideSwitch.materialState.transitionTo("Disabled", 5f, 0f);
		auxCurrentIndex--;
		if (auxCurrentIndex == -1)
		{
			auxCurrentIndex = 0;
		}
		if (auxCurrentIndex == 0)
		{
			auxOverrideDelete.targetable = false;
			auxOverrideDelete.materialState.transitionTo("Disabled", 5f);
		}
		auxOverrideState[auxCurrentIndex] = -1;
		GameObject[] numbers = auxInputNumbers[auxCurrentIndex].numbers;
		for (int i = 0; i < numbers.Length; i++)
		{
			numbers[i].SetActive(value: false);
		}
		PineFmod.playOneShotSound("event:/Sound Effects/03 Interactable/Buttons & Switches/Rfid_Reader/Rfid_LED_ON");
	}

	public override void onRPCCalled(int type)
	{
		if (type == 0)
		{
			solveBP();
		}
		if (type == 1)
		{
			auxCorrect.SetActive(value: true);
			solveAuxPuzzle();
		}
		if (type == 2)
		{
			auxError.SetActive(value: true);
			game.startTimer(new AuxSwitchErrorTimer(), 1f);
		}
		if (type == 3)
		{
			solveSpacesuit();
		}
		if (type == 4)
		{
			solveStarScanner();
		}
		if (type == 5)
		{
			solveStarChartPart();
		}
		if (type == 6)
		{
			solveMagnet();
		}
		if (type == 7)
		{
			plasmaButtonOnClick();
		}
		if (type == 9)
		{
			suitDoorUnlock();
			distributorPreviouslyPowered[0] = true;
		}
		if (type == 8)
		{
			updateLockerCompartment(4);
			distributorPreviouslyPowered[1] = true;
		}
		if (type == 10)
		{
			updateLockerCompartment(3);
			distributorPreviouslyPowered[2] = true;
		}
		if (type == 11)
		{
			game.startTimer(new HoloshipPowerTimer(), 2f);
			magnetHoloshipPowerText.SetActive(value: false);
			distributorPreviouslyPowered[3] = true;
		}
		if (type == 12)
		{
			updateLockerCompartment(1);
			distributorPreviouslyPowered[4] = true;
		}
		if (type == 13)
		{
			updateLockerCompartment(0);
			distributorPreviouslyPowered[5] = true;
		}
		if (type == 14)
		{
			tokenGrantedScreen.SetActive(value: true);
			tokensSolved = true;
			suitDoorTS.transitionToDuration("Open", 2f);
			door1Open.Play();
			distributorUpdatePower();
		}
		if (type == 15)
		{
			tokenDeniedScreen.SetActive(value: true);
			game.startTimer(new TokenIncorrectTimer(), 2f);
		}
	}

	public void solveStarChartPart()
	{
		Array.ForEach(starchartMarkerSwitches, delegate(Switch3D x)
		{
			x.targetable = false;
		});
		game.startTimer(new StarchartSendWaitTimer(), 2f);
		Array.ForEach(starchartStateTexts, delegate(GameObject x)
		{
			x.SetActive(value: false);
		});
	}

	public void solveSpacesuit()
	{
		suitDispenseButton.targetable = false;
		suitEquipped = true;
		suitDispenseButton.gameObject.SetActive(value: false);
		suitScreenMain.SetActive(value: false);
		suitScreenActivating.SetActive(value: true);
		suitDoorUnequipped.SetActive(value: false);
		suitDoorEquipped.SetActive(value: true);
		suitCinematic.SetActive(value: true);
		game.startTimer(new SpaceSuitSequence_1Timer(), 1.5f);
		game.startTimer(new SpaceSuitSequence_2Timer(), 2f);
		game.startTimer(new FadeInOutDelayedTimer(), 4.5f);
		game.startTimer(new SpaceSuitSequence_3Timer(), 6f);
		disableNavmeshBlocker();
	}

	public void solveStarScanner()
	{
		game.increaseZoomCounter(planetsZoom);
		planetScanSwitch.targetable = false;
		planetsScanner.Get<HorizontalSlider>(0u).targetable = false;
		navigationOnline = true;
		planetsScanForMissingStars.SetActive(value: false);
		planetsStarsFound.SetActive(value: true);
		navigationOfflineText_1.SetActive(value: false);
		navigationOfflineText_2.SetActive(value: false);
		navigationOfflineText_3.SetActive(value: false);
		navigationOnlineText_1.SetActive(value: true);
		navigationOnlineText_2.SetActive(value: true);
		planetsDataMissing.SetActive(value: false);
		planetsDataFound.SetActive(value: true);
		GameObject[] array = planetScreen;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(value: false);
		}
		planetScreenMainMesh.enabled = false;
		planetWinScreen.SetActive(value: true);
		checkCrashState();
		game.finishPuzzle(Puzzle.StarScanner);
	}

	public override void onInitPredicates()
	{
		game.registerPredicate(LevelPredicate.SolarPanels, () => percentages[0] >= 100.0 && percentages[1] >= 100.0 && percentages[2] >= 100.0 && !game.isAnyPlayerInteracting(solarPanelTransforms.Get<GameObject>(0, 0f)) && !game.isAnyPlayerInteracting(solarPanelTransforms.Get<GameObject>(1, 0f)) && !game.isAnyPlayerInteracting(solarPanelTransforms.Get<GameObject>(2, 0f)));
	}

	public override void onLevelPredicateDone(int type, int doneCounter)
	{
		if (type != 0)
		{
			return;
		}
		Game obj = game;
		Transform obj2 = solarPanelTransforms.Get<Transform>(0, 0);
		Quaternion? rotation = Quaternion.Euler(-90f, 0f, 180f);
		obj.startTransitionLocal(obj2, 2f, 0.5f, null, rotation);
		Game obj3 = game;
		Transform obj4 = solarPanelTransforms.Get<Transform>(1, 0);
		rotation = Quaternion.Euler(-90f, 0f, 180f);
		obj3.startTransitionLocal(obj4, 2f, 0.5f, null, rotation);
		Game obj5 = game;
		Transform obj6 = solarPanelTransforms.Get<Transform>(2, 0);
		rotation = Quaternion.Euler(-90f, 0f, 180f);
		obj5.startTransitionLocal(obj6, 2f, 0.5f, null, rotation);
		for (int i = 0; i < 5; i++)
		{
			game.startTimer(new SolarPanelToggleTimer(), (float)i * 0.25f, 0.5f);
		}
		game.startTimer(new SolarPanelZoomTimer(), 1.75f);
		solarPanelScreenOffline.SetActive(value: false);
		solarPanelScreenOnline.SetActive(value: true);
		foreach (Ref<Transform, GameObject, Dial> solarPanelTransform in solarPanelTransforms)
		{
			solarPanelTransform.Get<Dial>(0u).targetable = false;
		}
		solarSolved = true;
		game.finishPuzzle(Puzzle.SolarPanels);
	}

	public override void onInit()
	{
		game.syncPlayerRays = true;
		initInteractiveLinks();
		initMisc();
		initMagnets();
		initStarchart();
		initAux();
		initPlanets();
		initDistribution();
		initPipes();
		initSolar();
		initPlasma();
		powerSwitches[1].materialState.setState("Online");
		powerSwitches[3].materialState.setState("Online");
		powerSwitches[4].materialState.setState("Online");
		powerSwitches[7].materialState.setState("Online");
		powerSwitches[8].materialState.setState("Online");
		powerSwitches[10].materialState.setState("Online");
		powerSwitches[12].materialState.setState("Online");
		powerSwitches[11].materialState.setState("Online");
		updateCubesDist = true;
		gunSound = PineFmod.createInstance("event:/Sound Effects/04 Items/Weapons/Handheld Tractor Beam/Handheld_Tractor_Beam_Loop");
		gunSound.set3DAttributes(PineFmod.to3DAttributes(game.playerCollider.transform));
		void initAux()
		{
			auxOverrideDelete.targetable = false;
			auxOverrideDelete.materialState.setState("Disabled");
			auxGraphNode.initNodes();
		}
		void initDistribution()
		{
			distributionOnHoldEventInstance = PineFmod.createInstance("event:/Sound Effects/04 Items/Weapons/Electromagnetic_Distributor/Electromagnetic_Distributor_Loop");
			distributionOnHoldEventInstance.set3DAttributes(PineFmod.to3DAttributes(distributor.transform));
			distributionOnHoldEventInstance.setVolume(0f);
			distributorSlotStates = new int[distributorRotatables.Length];
			for (int i = 0; i < distributorVfxRenderers.Length; i++)
			{
				distributorVfxRenderers[i].SetPosition(1, distributorVfxParents[i].transform.position);
				distributorVfxRenderers[i].enabled = false;
			}
		}
		void initInteractiveLinks()
		{
			Interactive.linkInteractives(planetsScanner.Get<HorizontalSlider>(0u), planetScanSwitch);
		}
		void initMagnets()
		{
			magnetsActive = new bool[magnetToggleSwitches.Length];
			Array.ForEach(magnetOnStatesText, delegate(GameObject x)
			{
				x.SetActive(value: false);
			});
		}
		void initMisc()
		{
			distributorButtonOriginalPositions = new Vector3[distributorButtons.Length];
			for (int i = 0; i < distributorButtons.Length; i++)
			{
				distributorButtonOriginalPositions[i] = distributorButtons[i].transform.position;
			}
			distributorsOpened = new bool[distributionUIs.Count];
		}
		void initPipes()
		{
			distributorCirclesStartColor = distributorCircles[0].material.GetColor("_Color");
			distributorCirclesStartColor = distributorCircles[0].material.GetColor("_Emission");
			distributionWireDefaultEmissionColor = distributionWireMaterial.GetColor("_Emission_Color");
			distributionWireDefaultNoiseColor = distributionWireMaterial.GetColor("_Noise_Color");
			distributionWireDefaultFresnelColor = distributionWireMaterial.GetColor("_Fresnel_Color");
			defaultNoiseSpeed = distributionWireMaterial.GetFloat("_NoiseSpeed");
			distributorUpdatePower();
		}
		void initPlanets()
		{
			(float, int)[] distances = planetCalculateDistances();
			planetUpdateDistanceTexts(distances);
			foreach (Ref<GameObject, Transform, MaterialState> planet in planets)
			{
				planet.Get<MaterialState>(0u).setState("Default");
			}
			planets[0].Get<MaterialState>(0u).setState("Highlighted");
			planets[6].Get<MaterialState>(0u).setState("Highlighted");
			planets[8].Get<MaterialState>(0u).setState("Highlighted");
		}
		void initPlasma()
		{
			plasmaCenter = Vector3.zero;
			Array.ForEach(plasmaNodesTran, delegate(Transform x)
			{
				plasmaCenter += x.position;
			});
			plasmaCenter /= (float)plasmaNodesTran.Length;
			for (int num = 0; num < plasmaNodesTran.Length; num++)
			{
				plasmaAsteroids[num].transform.position = plasmaNodesTran[num].position;
			}
		}
		void initSolar()
		{
			solarPanelMaterialStates = new List<MaterialState[]> { solarPanelMaterialStates_1, solarPanelMaterialStates_2, solarPanelMaterialStates_3 };
			for (int i = 0; i < solarPanelVertices.Length; i++)
			{
				int num = (int)Math.Sqrt(676.0);
				solarPanelVertices[i] = new List<Transform>();
				Vector3 vector = solarPanelTransforms.Get<Transform>(i, 0).position + solarPanelTransforms.Get<Transform>(i, 0).forward * 5f;
				float num2 = 2f / (float)num;
				for (int j = -(num / 2); j <= num / 2; j++)
				{
					for (int k = -(num / 2); k <= num / 2; k++)
					{
						RaycastHit[] array = Physics.RaycastAll(new Ray(vector, solarPanelTransforms.Get<Transform>(i, 0).position - vector + solarPanelTransforms.Get<Transform>(i, 0).up * num2 * k + solarPanelTransforms.Get<Transform>(i, 0).right * num2 * j), float.PositiveInfinity);
						for (int l = 0; l < array.Length; l++)
						{
							RaycastHit raycastHit = array[l];
							if (raycastHit.collider.gameObject == solarPanelTransforms[i].Get<GameObject>(0f))
							{
								GameObject gameObject = new GameObject();
								game.setParent(gameObject.transform, solarPanelTransforms[i]);
								gameObject.transform.position = raycastHit.point;
								solarPanelVertices[i].Add(gameObject.transform);
							}
						}
					}
				}
			}
		}
	}

	public override void onFixedUpdate()
	{
		foreach (GameObject zeroGRigidbody in zeroGRigidbodies)
		{
			Rigidbody component = zeroGRigidbody.GetComponent<Rigidbody>();
			if (component != null && component.useGravity && !component.isKinematic)
			{
				component.AddForce(Vector3.up * Physics.gravity.magnitude, ForceMode.Acceleration);
			}
		}
	}

	public override void onLateUpdate()
	{
		if (game.isSelectedInPCMode(distributor.gameObject))
		{
			Array.ForEach(distributorLasers, delegate(LineRenderer x)
			{
				x.SetPosition(0, game.getImpostorInHand().transform.GetComponentInChildren<PersistantInImpostor>().transform.position);
			});
			Array.ForEach(distributorVfxRenderers, delegate(LineRenderer x)
			{
				if (x.enabled)
				{
					x.SetPosition(0, game.getImpostorInHand().transform.GetComponentInChildren<PersistantInImpostor>().transform.position);
				}
			});
		}
		if (game.isSelectedInPCMode(gun.gameObject))
		{
			gunLaser.SetPosition(0, game.getImpostorInHand().transform.GetComponentInChildren<PersistantInImpostor>().transform.position);
			Ray ray = new Ray(game.getCameraPosition(), game.getCameraForward());
			int layerMask = -5 & ~LayerMask.GetMask("RoomEditorSpecialObject", "IgnoreTrigger", "Teleportation", "VRHands");
			if (Physics.Raycast(ray, out var hitInfo, 10f, layerMask, QueryTriggerInteraction.Ignore))
			{
				gunLaser.SetPosition(1, hitInfo.point);
				return;
			}
			Vector3 position = game.getCameraPosition() + game.getCameraForward() * 3f;
			gunLaser.SetPosition(1, position);
		}
	}

	public override void onUpdate()
	{
		bool flag = fuseBox.transform.position.x < 1.45f || zeroGUnlocked;
		fuseBox.interactionMaxDistance = (flag ? (-1) : 0);
		fuseBoxLid.interactionMaxDistance = (flag ? (-1) : 0);
		distributorUpdatePower();
		plasmaUpdate();
		distributorUpdate();
		solarUpdate();
		updateLightFlicker();
	}

	public override void onTurnableMoved(Turnable turnable, MoveEvent moveEvent)
	{
		if (turnable == magnetHoloshipTurnable && moveEvent == MoveEvent.Moved)
		{
			magnetHoloshipOnTurned();
		}
	}

	public override void onTrigger(Trigger trigger, TriggerEvent triggerEvent)
	{
		if (trigger == magnetButtonsTrigger)
		{
			if (triggerEvent.playersEnteredThisEvent.Contains(game.localPlayerData.id))
			{
				Array.ForEach(magnetToggleSwitches, delegate(Switch3D x)
				{
					x.distanceExtender = 5f;
				});
			}
			if (triggerEvent.playersLeftThisEvent.Contains(game.localPlayerData.id))
			{
				Array.ForEach(magnetToggleSwitches, delegate(Switch3D x)
				{
					x.distanceExtender = 0f;
				});
			}
		}
		if (trigger == zeroGTrigger)
		{
			zeroGUpdate(trigger, triggerEvent);
		}
		else if (Array.IndexOf(zeroGSpecialRespawnTriggers, trigger) != -1)
		{
			zeroGRespawnItem(trigger, triggerEvent);
		}
	}

	public override void onAddToInventory(Item item)
	{
		if (item == distributor)
		{
			distributorPullOutTs.transitionTo("Default", 2f);
		}
		if (item.gameObject.TryGetComponent<Rigidbody>(out var _))
		{
			zeroGRigidbodies.Remove(item.gameObject);
		}
	}

	public override void onTool(Item tool, ToolContext context)
	{
		if (tool == gun)
		{
			if (gunValue != 2)
			{
				if (context.state == ToolState.Start)
				{
					UnityEngine.Debug.Log("starting gun");
					gunStart();
					PineFmod.start(gunSound);
				}
				gunUpdate(context);
				if (context.state == ToolState.End)
				{
					gunEnd();
					PineFmod.stop(gunSound, FMOD.Studio.STOP_MODE.IMMEDIATE);
				}
			}
		}
		else
		{
			if (!(tool == distributor) || context.state != ToolState.End)
			{
				return;
			}
			PineFmod.playOneShotSoundAttached("event:/Sound Effects/04 Items/Weapons/Electromagnetic_Distributor/Electromagnetic_Distributor_Switch", distributor.gameObject);
			int num = Array.IndexOf(distributorRotatables, context.currentTarget);
			if (num != -1)
			{
				distributorSlotStates[num]++;
				if (distributorSlotStates[num] >= 4)
				{
					distributorSlotStates[num] = 0;
				}
				context.currentTarget.transform.Rotate(Vector3.forward, 90f);
				distributorUpdatePower();
			}
		}
	}

	public override void onSlot(Slot targetSlot)
	{
		int num = -1;
		if ((num = Array.IndexOf(plasmaCableSlots, targetSlot)) != -1)
		{
			plasmaCablesOnSlot(num);
		}
		else if (UnityUtils.contains(starchartSlots, targetSlot))
		{
			starchartDriveOnSlot();
		}
		else if (UnityUtils.contains(tokenSlots, targetSlot))
		{
			tokenOnSlot();
		}
	}

	public override void onRemoveFromSlot(Slot slot, Item item)
	{
		int num = -1;
		if (UnityUtils.contains(tokenSlots, slot))
		{
			tokenOnSlot();
		}
		else if ((num = Array.IndexOf(plasmaCableSlots, slot)) != -1)
		{
			plasmaCablesOnSlot(num);
		}
	}

	public override void onDialMoved(Dial dial, MoveEvent moveEvent)
	{
		int interactiveIndex;
		if (dial.hasValueChanged && (interactiveIndex = auxGraphNode.getInteractiveIndex(dial)) != -1)
		{
			int offset = dial.valueCount - dial.value;
			auxGraphNode.updateNode(interactiveIndex, offset);
		}
	}

	public override void onHorizontalSliderMoved(HorizontalSlider slider, int pointIndex, MoveEvent moveEvent)
	{
		planetsDragLoop();
	}

	public bool isCenterCorrect(int x, int y)
	{
		int num = centerTargets[x, y];
		int num2 = gridValues[x + 1, y];
		int num3 = gridValues[x, y];
		int num4 = gridValues[x + 1, y + 1];
		int num5 = gridValues[x, y + 1];
		if (num2 == 0 || num3 == 0 || num4 == 0 || num5 == 0)
		{
			return false;
		}
		int num6 = num2 + num3 + num4 + num5 - 4;
		if (x == 0 && y == 0)
		{
			UnityEngine.Debug.Log(num2 + " " + num3 + " " + num4 + " " + num5);
		}
		return num6 == num;
	}

	public override void onSwitch3D(Switch3D targetSwitch, Switch3DEvent switchEvent)
	{
		if (switchEvent == Switch3DEvent.Start && targetSwitch == gunDial)
		{
			gunValue++;
			if (gunValue == 3)
			{
				gunValue = 0;
			}
			gunDialTs.transitionTo(gunValue.ToString(), 4f);
			switch (gunValue)
			{
			case 0:
				gunLaserEffectsStart[0].SetActive(value: true);
				gunLaserEffectsStart[1].SetActive(value: false);
				gunLaserEffectsEnd[0].SetActive(value: true);
				gunLaserEffectsEnd[1].SetActive(value: false);
				gunLaserMS.setState("Red");
				gunMS.setState("0");
				PineFmod.start(gunSound);
				break;
			case 2:
				gunLaserEffectsStart[0].SetActive(value: false);
				gunLaserEffectsStart[1].SetActive(value: false);
				gunLaserEffectsEnd[0].SetActive(value: false);
				gunLaserEffectsEnd[1].SetActive(value: false);
				gunMS.setState("1");
				PineFmod.stop(gunSound, FMOD.Studio.STOP_MODE.IMMEDIATE);
				break;
			case 1:
				gunLaserEffectsStart[0].SetActive(value: false);
				gunLaserEffectsStart[1].SetActive(value: true);
				gunLaserEffectsEnd[0].SetActive(value: false);
				gunLaserEffectsEnd[1].SetActive(value: true);
				gunLaserMS.setState("Blue");
				gunMS.setState("2");
				PineFmod.start(gunSound);
				break;
			}
		}
		int num = Array.IndexOf(powerSwitches, targetSwitch);
		if (switchEvent == Switch3DEvent.Start)
		{
			if (num != -1)
			{
				onPowerSwitches(num);
			}
			else if (targetSwitch == powerResetSwitch.Get<Switch3D>(0))
			{
				powerSwitches[0].materialState.setState("Online", 0f);
				powerSwitches[1].materialState.setState("Online");
				powerSwitches[2].materialState.setState("Online", 0f);
				powerSwitches[3].materialState.setState("Online");
				powerSwitches[4].materialState.setState("Online");
				powerSwitches[5].materialState.setState("Online", 0f);
				powerSwitches[6].materialState.setState("Online", 0f);
				powerSwitches[7].materialState.setState("Online");
				powerSwitches[8].materialState.setState("Online");
				powerSwitches[9].materialState.setState("Online", 0f);
				powerSwitches[10].materialState.setState("Online");
				powerSwitches[11].materialState.setState("Online");
				powerSwitches[12].materialState.setState("Online");
				powerSwitches[13].materialState.setState("Online", 0f);
			}
		}
		int num2 = Array.IndexOf(numberSwitches, targetSwitch);
		if (num2 != -1 && switchEvent == Switch3DEvent.Start)
		{
			int num3 = 6;
			int num4 = num2 / num3;
			int num5 = num2 % num3;
			gridValues[num4, num5] = (gridValues[num4, num5] + 1) % 3;
			if (gridValues[num4, num5] == 0)
			{
				ones[6 * num4 + num5].SetActive(value: false);
			}
			if (gridValues[num4, num5] == 1)
			{
				zeros[6 * num4 + num5].SetActive(value: true);
			}
			if (gridValues[num4, num5] == 2)
			{
				zeros[6 * num4 + num5].SetActive(value: false);
				ones[6 * num4 + num5].SetActive(value: true);
			}
			bool flag = true;
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 5; j++)
				{
					int num6 = 5 * i + j;
					if (isCenterCorrect(i, j))
					{
						if (!greenSolutions[num6].activeSelf)
						{
							PineFmod.playOneShotSound("event:/Sound Effects/03 Interactable/Buttons & Switches/Digital_Button_04");
						}
						greenSolutions[num6].SetActive(value: true);
						redSolutions[num6].SetActive(value: false);
						continue;
					}
					if (greenSolutions[num6].activeSelf)
					{
						PineFmod.playOneShotSound("event:/Sound Effects/03 Interactable/Buttons & Switches/Digital_Button_04");
					}
					flag = false;
					greenSolutions[num6].SetActive(value: false);
					redSolutions[num6].SetActive(value: true);
				}
			}
			if (flag)
			{
				game.callRPC(RPCType.BPPuzzle);
			}
		}
		if (switchEvent == Switch3DEvent.Start && targetSwitch == levelExit.Get<Switch3D>(0))
		{
			game.finishPuzzle(Puzzle.Exit);
			game.levelCompleted();
		}
		if (targetSwitch == ventDoorSwitch)
		{
			door2Open.Play();
		}
		int interactiveIndex;
		if (UnityUtils.contains(auxGraphToggleDials, targetSwitch) && (switchEvent == Switch3DEvent.On || switchEvent == Switch3DEvent.Off))
		{
			interactiveIndex = auxGraphNode.getInteractiveIndex(targetSwitch);
			int offset = ((targetSwitch.state == Switch3DState.On) ? 5 : 0);
			Vector3 eulerAngles = targetSwitch.transform.localRotation.eulerAngles;
			eulerAngles.z = ((targetSwitch.state == Switch3DState.On) ? (eulerAngles.z - 60f) : (eulerAngles.z + 60f));
			targetSwitch.transform.localRotation = Quaternion.Euler(eulerAngles);
			auxGraphNode.updateNode(interactiveIndex, offset);
		}
		if (targetSwitch == hologramSwitch && switchEvent == Switch3DEvent.On)
		{
			hologramSwitchMS.setState("Down");
		}
		if (targetSwitch == hologramSwitch && switchEvent == Switch3DEvent.Off)
		{
			hologramSwitchMS.setState("Default");
		}
		if (targetSwitch == auxOverrideDelete && switchEvent == Switch3DEvent.Start)
		{
			auxOnDelete();
		}
		else if (targetSwitch == auxOverrideSwitch && switchEvent == Switch3DEvent.Start)
		{
			auxOnConfirm();
		}
		if (targetSwitch == tokenConfirmButton && switchEvent == Switch3DEvent.On)
		{
			tokenOnConfirm();
		}
		if (switchEvent != Switch3DEvent.Start)
		{
			return;
		}
		if (targetSwitch == auxOverrideSwitch)
		{
			if (auxCurrentIndex >= 4)
			{
				return;
			}
			Array.ForEach(auxGraphToggleDials, delegate(Switch3D x)
			{
				x.targetable = false;
			});
			Array.ForEach(auxGraphDials, delegate(Dial x)
			{
				x.targetable = false;
			});
			auxOverrideDelete.targetable = false;
			auxOverrideSwitch.targetable = false;
			auxOverrideSwitch.targetable = false;
		}
		if (targetSwitch == planetScanSwitch)
		{
			planetsScan();
		}
		else if (targetSwitch == plasmaOpenContainerButton)
		{
			game.callRPC(RPCType.PowerCables);
		}
		else if ((interactiveIndex = Array.IndexOf(distributorTargetButtons, targetSwitch)) != -1)
		{
			distributorOnPlace(interactiveIndex);
		}
		else if ((interactiveIndex = Array.IndexOf(distributorButtons, targetSwitch)) != -1)
		{
			distributorOnButton(interactiveIndex);
		}
		else if (targetSwitch == suitDispenseButton)
		{
			game.callRPC(RPCType.Spacesuit);
		}
		else if ((interactiveIndex = Array.IndexOf(distributionCompartmentSwitches, targetSwitch)) != -1 && distributorsOpened[interactiveIndex])
		{
			targetSwitch.targetable = false;
			distributionUIs[interactiveIndex].openAnimation.play(0.8f);
			game.startTimer(new FadeOutLockTimer(interactiveIndex), 0.2f);
			game.startTimer(new CompartmentOpenTimer(interactiveIndex), 1f);
		}
		if ((interactiveIndex = Array.IndexOf(starchartMarkerSwitches, targetSwitch)) != -1)
		{
			starchartOnMarkerSwitch(interactiveIndex);
			starchartOnSend();
		}
		else if ((interactiveIndex = Array.IndexOf(magnetToggleSwitches, targetSwitch)) != -1)
		{
			magnetOnToggle(interactiveIndex);
		}
		else if ((interactiveIndex = Array.IndexOf(starchartSelectSwitches, targetSwitch)) != -1)
		{
			for (int num7 = 0; num7 < 3; num7++)
			{
				starchartSelectTSs[num7].transitionToDuration((num7 == interactiveIndex) ? "Active" : "Default", 0.2f);
				starchartPatterns[num7].SetActive(num7 == interactiveIndex);
			}
		}
		for (int num8 = 0; num8 < food.Length; num8++)
		{
			if (targetSwitch.transform.parent.gameObject == food[num8].Get<GameObject>(0) && switchEvent == Switch3DEvent.Start)
			{
				if (food[num8].Get<TweenState>((short)0) != null)
				{
					food[num8].Get<TweenState>((short)0).transitionTo("NewState", 4f);
				}
				else
				{
					OnFoodEaten(num8);
				}
			}
		}
	}

	public override void onSlidableMoved(Slidable slidable, MoveEvent moveEvent)
	{
		if (slidable == tabletLockSlidable && moveEvent == MoveEvent.Released && UnityUtils.closeEnough(tabletLockSlidable.value, 1f))
		{
			tabletLockSlidable.targetable = false;
			tabletLockSlidable.snapMode = Slidable.SnapMode.DontSnap;
			tabletLockScreen.Get<TweenState>(0).transitionTo("Off");
			tabletScreen.transitionTo("On");
		}
	}

	public override void onTimerDone(Timer timer)
	{
		if (timer is PlasmaCubesDistTimer { isCancelled: false })
		{
			updateCubesDist = false;
		}
		if (timer is SolveNumbersAnimTimer solveNumbersAnimTimer)
		{
			PineFmod.playOneShotSound("event:/Sound Effects/03 Interactable/Buttons & Switches/Digital_Button_04");
			GameObject[] array = greenSolutions;
			foreach (GameObject obj in array)
			{
				obj.SetActive(!obj.activeSelf);
			}
			if (solveNumbersAnimTimer.index == 5)
			{
				comsOffline.enabled = false;
				comsOffline2.enabled = false;
				comsOnline.enabled = true;
				checkCrashState();
			}
			else
			{
				game.startTimer(new SolveNumbersAnimTimer(solveNumbersAnimTimer.index + 1), 0.3f);
			}
		}
		if (timer is CutsceneAllSolvedStartTimer cutsceneAllSolvedStartTimer)
		{
			if (cutsceneAllSolvedStartTimer.index == 0)
			{
				afterDistributionUnlockCutscene();
			}
			else if (cutsceneAllSolvedStartTimer.index == 1)
			{
				afterDistributionUnlockCutscene2();
			}
			else if (cutsceneAllSolvedStartTimer.index == 2)
			{
				distributorButtons[0].gameObject.SetActive(value: true);
				distributorButtons[1].gameObject.SetActive(value: true);
				game.startTimer(new CutsceneAllSolvedStartTimer(3), 0.8f);
			}
			else
			{
				cutsceneAllSolvedStart.SetActive(value: false);
			}
		}
		if (timer is CutsceneCablesSolvedTimer cutsceneCablesSolvedTimer)
		{
			if (cutsceneCablesSolvedTimer.index == 0)
			{
				plasmaCablesDetachedScreen.SetActive(value: false);
				plasmaCablesAttachedScreen.SetActive(value: true);
				game.startTimer(new CutsceneCablesSolvedTimer(1), 0.8f);
			}
			else
			{
				cutsceneCablesSolved.SetActive(value: false);
			}
		}
		if (timer is CutscenePowerAddedTimer cutscenePowerAddedTimer)
		{
			if (cutscenePowerAddedTimer.index == 0)
			{
				distributorTextHighlights[cutscenePowerAddedTimer.glowIndex].transitionTo("NewState", 3f);
				game.startTimer(new CutscenePowerAddedTimer(1, cutscenePowerAddedTimer.glowIndex, cutscenePowerAddedTimer.buttonIndex), 0.6f);
			}
			else if (cutscenePowerAddedTimer.index == 1)
			{
				distributorButtons[cutscenePowerAddedTimer.buttonIndex].gameObject.SetActive(value: true);
				game.startTimer(new CutscenePowerAddedTimer(2, 0, 0), 0.8f);
			}
			else
			{
				cutscenePowerAdded.SetActive(value: false);
			}
		}
		if (timer is FadeOutLockTimer fadeOutLockTimer)
		{
			distributionUIs[fadeOutLockTimer.index]._switch.tweenState.transitionTo("Unlock", 0.75f);
		}
		if (timer is CompartmentOpenTimer compartmentOpenTimer)
		{
			openCompartment(compartmentOpenTimer.index);
		}
		if (timer is CompartmentUnlockTimer compartmentUnlockTimer)
		{
			updateLockerCompartmentDelayed(compartmentUnlockTimer.index);
		}
		if (timer is SolarPanelToggleTimer)
		{
			Array.ForEach(solarPanelTextsGO, delegate(GameObject x)
			{
				x.SetActive(!x.activeSelf);
			});
		}
		if (timer is SolarPanelZoomTimer)
		{
			Array.ForEach(solarPanelUnsolved, delegate(GameObject x)
			{
				x.SetActive(value: false);
			});
			Array.ForEach(solarPanelSolved, delegate(GameObject x)
			{
				x.SetActive(value: true);
			});
			game.increaseZoomCounter(solarPanelZoomable);
			solarPanelZoomable.targetable = false;
			cutscenePowerAdded.SetActive(value: true);
			game.startTimer(new CutscenePowerAddedTimer(0, 2, 3), 1.5f);
		}
		if (timer is PlasmaCompleteTimer)
		{
			plasmaCompleteTimerDone();
		}
		if (timer is TokenCheckTimer)
		{
			tokenCheckSolution();
		}
		if (timer is TokenIncorrectTimer)
		{
			tokenReset();
		}
		if (timer is TokenErrorTimer tokenErrorTimer)
		{
			tokenOnError(tokenErrorTimer.isFinal);
		}
		if (timer is TokenLoadingTimer)
		{
			tokenOnLoaded();
		}
		if (timer is TokenUpdateSequenceTimer tokenUpdateSequenceTimer)
		{
			tokenOnUpdateSequence(tokenUpdateSequenceTimer.index);
		}
		if (timer is AuxNodeHighlightDelayTimer auxNodeHighlightDelayTimer && auxNodeHighlightDelayTimer.target != null)
		{
			auxNodeHighlightDelayTimer.target.SetActive(value: true);
			PineFmod.playOneShotSound("event:/Sound Effects/03 Interactable/Buttons & Switches/Digital_Button_04");
		}
		if (timer is AsteroidSolveTimer)
		{
			asteroidTimerDone(timer);
		}
		if (timer is StarchartDriveCompleteTimer)
		{
			starchartDriveLoadingBar.localScale = Vector3.zero;
			starchartDriveUploading.SetActive(value: false);
			starchartDriveComplete.SetActive(value: true);
			starchartNewHolograms.SetActive(value: true);
			Array.ForEach(starchartDrives, delegate(GameObject x)
			{
				x.SetActive(value: false);
			});
		}
		if (timer is HoloshipCompleteTimer)
		{
			cutscenePowerAdded.SetActive(value: true);
			game.startTimer(new CutscenePowerAddedTimer(0, 4, 5), 1.5f);
			starChartLoadingBarParent.SetActive(value: false);
		}
		if (timer is PowerShipFinalAnimTimer)
		{
			sphereAnimSequence.play();
			giantSphereTS.transitionTo("Moved");
		}
		if (timer is SwitchesPowerOnOffTimer switchesPowerOnOffTimer)
		{
			if (switchesPowerOnOffTimer.index == 0)
			{
				Switch3D[] array2 = powerSwitches;
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i].materialState.transitionToStateAdditive("FadeOut", 2f);
				}
			}
			if (switchesPowerOnOffTimer.index == 1)
			{
				Switch3D[] array2 = powerSwitches;
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i].materialState.transitionToStateAdditive("FadeOut", 2f, 0f);
				}
			}
			if (switchesPowerOnOffTimer.index == 2)
			{
				MaterialState[] array3 = arrowsMats;
				for (int i = 0; i < array3.Length; i++)
				{
					array3[i].transitionTo("FadeOut", 2f);
				}
				shipFadeOut.transitionToStateAdditive("FadeOut", 2f);
				Switch3D[] array2 = powerSwitches;
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i].materialState.transitionToStateAdditive("FadeOut", 2f);
				}
				powerResetSwitch.Get<GameObject>(0f).SetActive(value: false);
			}
			if (switchesPowerOnOffTimer.index < 2)
			{
				game.startTimer(new SwitchesPowerOnOffTimer(switchesPowerOnOffTimer.index + 1), 0.5f);
			}
		}
		if (timer is AsteroidFlashTimer)
		{
			if (timer.isCancelled)
			{
				return;
			}
			asteroidFlashState = !asteroidFlashState;
			magnetAsteroidMaterialState.setState(asteroidFlashState ? "Glow" : "Default");
			magnetAsteroidTextMaterialState.setState(asteroidFlashState ? "Glow" : "Default");
		}
		if (timer is AuxNodeFlashTimer { index: var index } auxNodeFlashTimer)
		{
			bool active = auxNodeFlashTimer.stateInt != 0;
			auxGraphNode.nodes[index].flash.SetActive(active);
			PineFmod.playOneShotSound("event:/Sound Effects/03 Interactable/Buttons & Switches/Digital_Button_04");
		}
		if (timer is AuxSwitchPauseTimer { result: var result })
		{
			GameObject[] array = auxInputNumbers[auxCurrentIndex].numbers;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetActive(value: false);
			}
			auxInputNumbers[auxCurrentIndex].numbers[result].SetActive(value: true);
			auxOverrideState[auxCurrentIndex] = result;
			auxCurrentIndex++;
			auxOverrideDelete.targetable = true;
			auxOverrideDelete.materialState.transitionTo("Disabled", 5f, 0f);
			if (auxCurrentIndex >= 4)
			{
				Array.ForEach(auxNumbers, delegate(GameObject x)
				{
					x.SetActive(value: false);
				});
				Array.ForEach(auxGraphToggleDials, delegate(Switch3D x)
				{
					x.targetable = false;
				});
				Array.ForEach(auxGraphDials, delegate(Dial x)
				{
					x.targetable = false;
				});
				auxOverrideDelete.targetable = false;
				auxOverrideSwitch.targetable = false;
				auxChecking.transitionTo("NewState");
				numberLines.SetActive(value: false);
				game.startTimer(new AuxSwitchCheckTimer(), 1f);
			}
			else
			{
				auxOverrideSwitch.targetable = true;
			}
			PineFmod.playOneShotSound("event:/Sound Effects/03 Interactable/Buttons & Switches/Rfid_Reader/Rfid_LED_ON");
			Array.ForEach(auxGraphToggleDials, delegate(Switch3D x)
			{
				x.targetable = true;
			});
			Array.ForEach(auxGraphDials, delegate(Dial x)
			{
				x.targetable = true;
			});
			Array.ForEach(auxHighlightsAllGo, delegate(GameObject x)
			{
				x.SetActive(value: false);
			});
			centerCircle.localScale = Vector3.one * 0.012f;
		}
		if (timer is AuxSwitchCheckTimer)
		{
			Array.ForEach(auxHighlightsAllGo, delegate(GameObject x)
			{
				x.SetActive(value: false);
			});
			auxChecking.setWeight("NewState", 0f);
			int i2 = 0;
			if (Array.TrueForAll(auxOverrideSolution, (int x) => x == auxOverrideState[i2++]))
			{
				game.callRPC(RPCType.AuxPuzzleGood);
			}
			else
			{
				game.callRPC(RPCType.AuxPuzzleBad);
			}
		}
		if (timer is AuxSwitchErrorTimer)
		{
			Array.ForEach(auxGraphToggleDials, delegate(Switch3D x)
			{
				x.targetable = true;
			});
			Array.ForEach(auxGraphDials, delegate(Dial x)
			{
				x.targetable = true;
			});
			auxError.SetActive(value: false);
			Array.ForEach(auxNumbers, delegate(GameObject x)
			{
				x.SetActive(value: true);
			});
			numberLines.SetActive(value: true);
			auxOverrideDelete.targetable = true;
			auxOverrideDelete.materialState.transitionTo("Disabled", 5f, 0f);
		}
		if (timer is SpaceSuitTextFlashTimer { index: var index2 })
		{
			distributionPowerTexts[0].color = ((index2 % 2 == 0) ? Color.red : Color.white);
			if (index2 == 3)
			{
				spacesuitSwitch.targetable = true;
			}
		}
		if (timer is FadeInOutDelayedTimer)
		{
			suitCinematic.SetActive(value: false);
		}
		_ = timer is SpaceSuitSequence_1Timer;
		if (timer is SpaceSuitSequence_2Timer)
		{
			spacesuitSequence.play();
		}
		if (timer is SpaceSuitSequence_3Timer)
		{
			suitScreenActivating.SetActive(value: false);
			suitScreenMain.SetActive(value: true);
			spacesuitOverlay.SetActive(value: true);
			spacesuitOverlayFlash.transitionTo("NewState", 1.5f);
			music.SetParameter("Space 2 Music Switch", 1f);
			forceFieldAudio.SetActive(value: false);
			game.finishPuzzle(Puzzle.SpaceSuit);
		}
		if (timer is AsteroidSwapStartTimer { index: var index3 })
		{
			game.startTransitionGlobal(plasmaAsteroids[index3].transform, 0.3f, 0f, plasmaNodesTran[index3 + 1].position);
			game.startTransitionGlobal(plasmaAsteroids[index3 + 1].transform, 0.3f, 0f, plasmaNodesTran[index3].position);
			Rigidbody[] array4 = plasmaAsteroids;
			int i = index3;
			Rigidbody[] array5 = plasmaAsteroids;
			int num = index3 + 1;
			Rigidbody rigidbody = plasmaAsteroids[index3 + 1];
			Rigidbody rigidbody2 = plasmaAsteroids[index3];
			array4[i] = rigidbody;
			array5[num] = rigidbody2;
		}
		if (timer is PlanetSendFlashTimer)
		{
			planetsScanScanning.SetActive(!planetsScanScanning.activeSelf);
			if (planetsScanScanning.activeSelf)
			{
				PineFmod.playOneShotSoundAttached("event:/Sound Effects/03 Interactable/Buttons & Switches/Rfid_Reader/Rfid_LED_ON", planetsScanScanning);
			}
		}
		if (timer is PlanetSendWaitTimer)
		{
			GameObject[] obj2 = new GameObject[4]
			{
				planets[9],
				planets[10],
				planets[11],
				planets[12]
			};
			GameObject gameObject = null;
			GameObject[] array = obj2;
			foreach (GameObject gameObject2 in array)
			{
				if ((!(gameObject2 == planets.Get<GameObject>(12, 0)) || (planets.Get<GameObject>(11, 0).activeSelf && planets.Get<GameObject>(10, 0).activeSelf && planets.Get<GameObject>(9, 0).activeSelf)) && Vector3.Distance(planetsScanner.Get<Transform>(0f).position, gameObject2.transform.position) <= 0.025f)
				{
					gameObject2.SetActive(value: true);
					gameObject = gameObject2;
				}
			}
			GameObject gameObject3 = planetsScanNoStar;
			float duration = 1.5f;
			if (gameObject != null)
			{
				if (gameObject == planets.Get<GameObject>(10, 0))
				{
					gameObject3 = planetsCruxDetected;
				}
				else if (gameObject == planets.Get<GameObject>(11, 0))
				{
					gameObject3 = planetsSerpensDetected;
				}
				else if (gameObject == planets.Get<GameObject>(9, 0))
				{
					gameObject3 = planetsLeoDetected;
				}
				else if (gameObject == planets.Get<GameObject>(12, 0))
				{
					gameObject3 = planetsAlphaDetected;
				}
				planetsScanScanning.SetActive(value: false);
				PineFmod.playOneShotSoundAttached("event:/Sound Effects/03 Interactable/Buttons & Switches/Airlock_Test_Buttons/Airlock_Pass_Test", planetsScanScanning);
			}
			else
			{
				PineFmod.playOneShotSoundAttached("event:/Sound Effects/03 Interactable/Buttons & Switches/Digital_Button_01", planetsScanScanning);
			}
			gameObject3.SetActive(value: true);
			game.startTimer(new PlanetSendFoundTimer(), duration);
		}
		if (timer is PlanetSendFoundTimer)
		{
			planetScanButtonMS.setState("Default");
			planetScanSwitch.targetable = true;
			planetsCruxDetected.SetActive(value: false);
			planetsSerpensDetected.SetActive(value: false);
			planetsLeoDetected.SetActive(value: false);
			planetsAlphaDetected.SetActive(value: false);
			planetsScanNoStar.SetActive(value: false);
			planetsDataMissing.SetActive(value: true);
			planetsScanner.Get<HorizontalSlider>(0u).targetable = true;
			if (planets.Get<GameObject>(12, 0).activeSelf)
			{
				game.callRPC(RPCType.StarScanner);
			}
		}
		if (timer is StarchartSendWaitTimer)
		{
			starChartLoadingBarParent.SetActive(value: false);
			foreach (StarchartLine starchartActiveLine in starchartActiveLines)
			{
				starchartStarGos[starchartActiveLine.point1].SetActive(value: false);
				starchartStarGos[starchartActiveLine.point2].SetActive(value: false);
				starchartLinePoolGos[starchartActiveLine.lineIndex].SetActive(value: false);
				availibleLines.Enqueue(starchartActiveLine.lineIndex);
			}
			starchartActiveLines.Clear();
			starchartSectorsFound++;
			bool flag = starchartSectorsFound == 3;
			Array.ForEach(starchartStateTexts, delegate(GameObject x)
			{
				x.SetActive(value: false);
			});
			starchartStateTexts[starchartSectorsFound].SetActive(!flag);
			if (flag)
			{
				PineFmod.playOneShotSoundAttached("event:/Sound Effects/03 Interactable/Buttons & Switches/Airlock_Test_Buttons/Airlock_Fail_Test", tractorbeamPuzzleComponents);
				game.startTimer(new StarchartCompleteTimer(), 2f);
			}
			else
			{
				Array.ForEach(starchartMarkerSwitches, delegate(Switch3D x)
				{
					x.targetable = true;
				});
			}
		}
		if (timer is StarchartCompleteTimer)
		{
			starchartSolve();
		}
		if (timer is HoloshipPowerTimer)
		{
			starChartLoadingBarParent.SetActive(value: false);
			tractorbeamPuzzleComponents.SetActive(value: true);
			magnetHoloshipFakeComponents.SetActive(value: false);
			magnetHoloshipPowerText.SetActive(value: false);
			tractorFieldPower.SetActive(value: true);
		}
	}

	public override void onSequenceDone(Sequence sequence)
	{
		if (sequence == sphereAnimSequence)
		{
			solveHoloshipText.SetActive(value: true);
		}
	}

	public override void onTimerUpdate(Timer timer)
	{
		if (timer is TokenLoadingTimer)
		{
			tokenUpdateLoadingBar(timer.time / timer.duration);
		}
		if (timer is PlasmaCompleteTimer)
		{
			plasmaRestartLoadingBar.localScale = new Vector3(timer.time / timer.duration, 1f, 1f);
		}
		if (timer is StarchartSendWaitTimer || timer is StarchartCompleteTimer || timer is HoloshipPowerTimer || timer is HoloshipCompleteTimer)
		{
			starChartLoadingBarParent.SetActive(value: true);
			starchartLoadingBarTs.setWeight("Loading", timer.time / timer.duration);
			if ((int)((timer.time + 0.0001f) / timer.duration * 3f) % 2 == 0)
			{
				foreach (StarchartLine starchartActiveLine in starchartActiveLines)
				{
					starchartLinePoolGos[starchartActiveLine.lineIndex].SetActive(value: false);
				}
			}
			else
			{
				foreach (StarchartLine starchartActiveLine2 in starchartActiveLines)
				{
					starchartLinePoolGos[starchartActiveLine2.lineIndex].SetActive(value: true);
				}
			}
		}
		if (timer is AsteroidSolveTimer)
		{
			plasmaLoadingBar.localScale = new Vector3(timer.time / timer.duration, 1f, 1f);
		}
		if (timer is StarchartDriveCompleteTimer)
		{
			starchartDriveLoadingBar.localScale = new Vector3(timer.time / timer.duration, 1f, 1f);
		}
		if (timer is FadeOutTimer)
		{
			Color color = blackOverlay.color;
			color.a = timer.time / timer.duration;
			blackOverlay.color = color;
		}
		if (timer is FadeInTimer)
		{
			Color color2 = blackOverlay.color;
			color2.a = 1f - timer.time / timer.duration;
			blackOverlay.color = color2;
		}
	}

	public override void onTweenTransitionDone(TweenState tweenState, string state)
	{
		if (tweenState == distributorTS)
		{
			distributorPullOutTs.transitionTo("Open", 2f);
			distributor.targetable = true;
		}
		if (tweenState == asteroidCasingTSs[0])
		{
			Array.ForEach(plasmaAsteroidsTurnable, delegate(Turnable x)
			{
				x.targetable = true;
			});
			plasmaZoomable.targetable = true;
		}
		if (tweenState == tabletLockScreen.Get<TweenState>(0))
		{
			tabletLockScreen.Get<GameObject>(0f).SetActive(value: false);
			game.invalidateItemImpostors(tablet);
		}
		for (int num = 0; num < food.Length; num++)
		{
			if (tweenState == food[num].Get<TweenState>((short)0))
			{
				OnFoodEaten(num);
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
		foreach (Ref<MaterialState, Draggable, GameObject> item in debrisFade)
		{
			if (tweenState == item.Get<MaterialState>(0))
			{
				item.Get<GameObject>(0u).SetActive(value: false);
			}
		}
		if (tweenState == magnetHoloshipFake)
		{
			holoshipGlowState = !holoshipGlowState;
			magnetHoloshipFake.transitionToDuration(holoshipGlowState ? "Default" : "Glow", 1.5f);
		}
		if (tweenState == plasmaEnergyPulse)
		{
			if (tweenState.getTargetWeight("Pulse") > 0.5f)
			{
				tweenState.transitionToDuration("Default");
			}
			else
			{
				tweenState.transitionToDuration("Pulse");
			}
		}
	}

	public override void onZoomLeave(GameObject zoomedItem)
	{
		if (zoomedItem == gun.gameObject && gunSound.getPlaybackState(out var state) == RESULT.OK && state == PLAYBACK_STATE.PLAYING)
		{
			PineFmod.stop(gunSound, FMOD.Studio.STOP_MODE.IMMEDIATE);
		}
	}

	private void checkCrashState()
	{
		if (navigationOnline && crashLogSent && communicationOnline)
		{
			distributorUnlock();
		}
	}

	[DebugButton("Unlock Energy Distribution Puzzle", Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void distributorUnlock()
	{
		distributionLockedText_1.SetActive(value: false);
		distributionLockedText_2.SetActive(value: false);
		distributionUnlockedText_1.SetActive(value: true);
		distributionUnlockedText_2.SetActive(value: true);
		cutsceneAllSolvedStart.SetActive(value: true);
		game.startTimer(new CutsceneAllSolvedStartTimer(0), 1.5f);
	}

	private void afterDistributionUnlockCutscene()
	{
		distributorTS.transitionTo("Open", 2f);
		game.startTimer(new CutsceneAllSolvedStartTimer(1), 3f);
	}

	private void afterDistributionUnlockCutscene2()
	{
		distributorTextHighlights[0].transitionTo("NewState", 3f);
		distributionLockedScreen.SetActive(value: false);
		distributionUnlockedScreen.SetActive(value: true);
		distributionZoomable.targetable = true;
		game.startTimer(new CutsceneAllSolvedStartTimer(2), 0.6f);
	}

	private void distributorUpdateTargets()
	{
		for (int i = 0; i < distributionPowerRequirements.Length; i++)
		{
			float num = 0f;
			int[] item = distributionPowerRequirements[i].Item1;
			foreach (int num2 in item)
			{
				num += distribtuionTargetPower[num2];
			}
			bool flag = num >= (float)distributionPowerRequirements[i].Item2 || forcePower;
			if (distributorPreviouslyPowered[i])
			{
				continue;
			}
			string text = ((Math.Round(num, 2) == Math.Round(num, 0)) ? num.ToString("0") : num.ToString("0.0"));
			switch (i)
			{
			case 0:
				if (flag && tokensSolved && !suitEquipped)
				{
					game.callRPC(RPCType.Distributor0);
				}
				distributionPowerTexts[0].SetText(text + "/2");
				break;
			case 1:
				if (flag)
				{
					game.callRPC(RPCType.Distributor1);
				}
				distributionPowerTexts[1].SetText(text + "/6");
				break;
			case 2:
				if (flag)
				{
					game.callRPC(RPCType.Distributor2);
				}
				distributionPowerTexts[2].SetText(text + "/2");
				break;
			case 3:
				if (flag)
				{
					game.callRPC(RPCType.Distributor3);
				}
				distributionPowerTexts[3].SetText((flag ? "" : "") + text + "/9");
				break;
			case 4:
				if (flag)
				{
					game.callRPC(RPCType.Distributor4);
				}
				distributionPowerTexts[4].SetText(text + "/2");
				break;
			case 5:
				if (flag)
				{
					game.callRPC(RPCType.Distributor5);
				}
				distributionPowerTexts[5].SetText(text + "/12");
				break;
			}
		}
	}

	private void updateLockerCompartment(int index)
	{
		distributorsOpened[index] = true;
		Game obj = game;
		Transform lockAnimation = distributionUIs[index].lockAnimation;
		Quaternion? rotation = Quaternion.Euler(0f, 0f, 30f);
		obj.startTransitionLocal(lockAnimation, 0.5f, 0f, null, rotation);
		game.startTimer(new CompartmentUnlockTimer(index), 0.5f);
	}

	private void updateLockerCompartmentDelayed(int index)
	{
		distributionUIs[index]._switch.targetable = true;
		Array.ForEach(distributionUIs[index].lockedComponents, delegate(GameObject x)
		{
			x.SetActive(value: false);
		});
		Array.ForEach(distributionUIs[index].unlockedComponents, delegate(GameObject x)
		{
			x.SetActive(value: true);
		});
	}

	private void openCompartment(int index)
	{
		distributionUIs[index].parentObject.SetActive(value: false);
		distributionCompartmentTweens[index].transitionToDuration("Open");
		switch (index)
		{
		case 0:
			levelExit.Get<GameObject>(0f).SetActive(value: true);
			game.levelCompleted();
			break;
		case 3:
			game.finishPuzzle(Puzzle.PowerDistribution);
			break;
		}
	}

	private void distributorUpdatePower()
	{
		float[] array = new float[11];
		MeshRenderer[] array2 = distributionWireGlowRenderers;
		foreach (MeshRenderer obj in array2)
		{
			obj.material.SetColor("_Fresnel_Color", distributionWireDefaultFresnelColor);
			obj.material.SetColor("_Noise_Color", distributionWireDefaultNoiseColor);
			obj.material.SetColor("_Emission_Color", distributionWireDefaultEmissionColor);
			obj.material.SetFloat("_NoiseSpeed", defaultNoiseSpeed);
		}
		array2 = distributionWireSplitGlowRenderers;
		foreach (MeshRenderer obj2 in array2)
		{
			obj2.materials[0].SetColor("_Fresnel_Color", distributionWireDefaultFresnelColor);
			obj2.materials[0].SetColor("_Noise_Color", distributionWireDefaultNoiseColor);
			obj2.materials[0].SetColor("_Emission_Color", distributionWireDefaultEmissionColor);
			obj2.materials[0].SetFloat("_NoiseSpeed", defaultNoiseSpeed);
		}
		for (int j = 0; j <= 3; j++)
		{
			List<int> list = ((distributorRowsFilled[j] == 0) ? new List<int>() : distributorBfs(j));
			float num = ((list.Count == 0) ? 0f : ((float)distributorRowsFilled[j] / (float)list.Count));
			for (int k = 0; k < list.Count; k++)
			{
				array[list[k]] += num;
			}
		}
		distribtuionTargetPower = array;
		distributorUpdateTargets();
		List<int> distributorBfs(int startIndex)
		{
			List<int> list2 = new List<int>();
			Queue<(int, int)> queue = new Queue<(int, int)>();
			List<int> list3 = new List<int>();
			queue.Enqueue((startIndex, 0));
			list3.Add(startIndex);
			while (queue.Count > 0)
			{
				(int, int) tuple = queue.Dequeue();
				int item = tuple.Item1;
				int item2 = tuple.Item2;
				DistributionNode distributionNode = distribution[item];
				int[] array3;
				if (distributionNode.isStarting)
				{
					array3 = distributionNode.directions_1;
				}
				else
				{
					int[] array4 = new int[distributionNode.directions_1.Length];
					int[] array5 = new int[distributionNode.directions_2.Length];
					for (int l = 0; l < array4.Length; l++)
					{
						array4[l] = (distributionNode.directions_1[l] + distributorSlotStates[distributionNode.rotatableNodeIndex]) % 4;
					}
					for (int m = 0; m < array5.Length; m++)
					{
						array5[m] = (distributionNode.directions_2[m] + distributorSlotStates[distributionNode.rotatableNodeIndex]) % 4;
					}
					array3 = ((Array.IndexOf(array4, item2) != -1) ? array4 : ((Array.IndexOf(array5, item2) == -1) ? new int[0] : array5));
				}
				int[] array6 = array3;
				foreach (int num2 in array6)
				{
					int num3 = (int)distributionNode.adjacentNodeIndices[num2].x;
					int num4 = (int)distributionNode.adjacentNodeIndices[num2].y;
					if (num3 != -1)
					{
						DistributionNode distributionNode2 = distribution[num3];
						if (!list3.Contains(num3) && !distributionNode2.isStarting)
						{
							list3.Add(num3);
							if (!distributionNode.isFinal && !distributionNode.isStarting)
							{
								distributionWireSplitGlowRenderers[distributionNode.rotatableNodeIndex].materials[0].SetColor("_Fresnel_Color", distributionWireGlowFresnelColor);
								distributionWireSplitGlowRenderers[distributionNode.rotatableNodeIndex].materials[0].SetColor("_Noise_Color", distributionWireGlowNoiseColor);
								distributionWireSplitGlowRenderers[distributionNode.rotatableNodeIndex].materials[0].SetColor("_Emission_Color", distributionWireGlowEmissionColor);
								distributionWireSplitGlowRenderers[distributionNode.rotatableNodeIndex].materials[0].SetFloat("_NoiseSpeed", noiseSpeed);
							}
							for (int num5 = 0; num5 < distributionWireGlowKeys.Length; num5++)
							{
								if (((int)distributionWireGlowKeys[num5].x == num3 && (int)distributionWireGlowKeys[num5].y == item) || ((int)distributionWireGlowKeys[num5].y == num3 && (int)distributionWireGlowKeys[num5].x == item))
								{
									distributionWireGlowRenderers[num5].material.SetColor("_Fresnel_Color", distributionWireGlowFresnelColor);
									distributionWireGlowRenderers[num5].material.SetColor("_Noise_Color", distributionWireGlowNoiseColor);
									distributionWireGlowRenderers[num5].material.SetColor("_Emission_Color", distributionWireGlowEmissionColor);
									distributionWireGlowRenderers[num5].material.SetFloat("_NoiseSpeed", noiseSpeed);
								}
							}
							if (!distributionNode2.isFinal && !distributionNode2.isStarting)
							{
								int item3 = num4;
								queue.Enqueue((num3, item3));
							}
							if (distributionNode2.isFinal)
							{
								list2.Add(distributionNode2.finalNodeIndex);
							}
						}
					}
				}
			}
			return list2;
		}
	}

	private void distributorUpdate()
	{
		Array.ForEach(distributorLasers, delegate(LineRenderer x)
		{
			x.gameObject.SetActive(value: false);
		});
		Array.ForEach(distributorCircles, delegate(MeshRenderer x)
		{
			x.material.SetColor("_Emission", distributorCirclesStartEmissionColor);
			x.material.SetColor("_Color", distributorCirclesStartColor);
		});
		Array.ForEach(distributionWireSplitGlowRenderers, delegate(MeshRenderer x)
		{
			x.materials[0].SetColor("_HoverColor", Color.white);
		});
		for (int num = 0; num < distributorVfxRenderers.Length; num++)
		{
			distributorVfxRenderers[num].enabled = false;
		}
		if (!game.isSelectedInPCMode(distributor.gameObject))
		{
			distributionOnHoldEventInstance.setVolume(0f);
			disableClipping();
			return;
		}
		distributionOnHoldEventInstance.setVolume(1f);
		int num2 = (1 << LayerMask.NameToLayer("CharacterLocal")) | (1 << LayerMask.NameToLayer("Characters")) | (1 << LayerMask.NameToLayer("VisibleToSpecLight")) | (1 << LayerMask.NameToLayer("IgnoreRaycast"));
		if (Physics.Raycast(game.playerViewRay, out var hit, float.PositiveInfinity, ~num2))
		{
			int num3 = -1;
			if ((num3 = Array.IndexOf(distributor.toolTargets, hit.collider.GetComponent<Interactive>())) != -1)
			{
				MeshRenderer obj = distributorCircles[num3];
				obj.material.SetColor("_Emission", distributorCirclesGlowEmissionColor);
				obj.material.SetColor("_Color", distributorCirclesGlowColor);
				hit.transform.GetComponent<Renderer>().materials[0].SetColor("_HoverColor", distributorSplitHoverColor);
				int targetIndex = 0;
				Array.ForEach(distributorLasers, delegate(LineRenderer x)
				{
					x.gameObject.SetActive(value: true);
					x.SetPosition(1, hit.transform.GetChild(targetIndex++).transform.position);
				});
			}
			else
			{
				for (int num4 = 0; num4 < distributorVfxRenderers.Length; num4++)
				{
					if (Vector3.Distance(xz(hit.point), xz(distributorVfxParents[num4].position)) <= 0.4f)
					{
						distributorVfxRenderers[num4].enabled = true;
					}
				}
			}
			enableClipping();
		}
		else
		{
			disableClipping();
		}
		void disableClipping()
		{
			MeshRenderer[] array = distributionWireRenderers;
			for (int i = 0; i < array.Length; i++)
			{
				Material[] materials = array[i].materials;
				for (int j = 0; j < materials.Length; j++)
				{
					materials[j].SetFloat("_ClipRadius", 1E-05f);
				}
			}
		}
		void enableClipping()
		{
			MeshRenderer[] array = distributionWireRenderers;
			for (int i = 0; i < array.Length; i++)
			{
				Material[] materials = array[i].materials;
				foreach (Material obj2 in materials)
				{
					obj2.SetVector("_ClipCenter", hit.point);
					obj2.SetFloat("_ClipRadius", 1.2f);
				}
			}
		}
		static Vector3 xz(Vector3 vec)
		{
			return new Vector3(vec.x, 0f, vec.z);
		}
	}

	[DebugButton("Get guns", Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void getGuns()
	{
		game.addItemToInventory(distributor.gameObject);
		game.addItemToInventory(gun.gameObject);
	}

	[DebugButton("Force D Power On", Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void forceDPowerOn()
	{
	}

	public override void onInitHints()
	{
		game.setPuzzleConditions(Puzzle.PowerDistribution, Puzzle.StarScanner, Puzzle.BinarySudoku, Puzzle.StarCharts);
		game.setPuzzleConditions(Puzzle.SpaceSuit, Puzzle.PowerDistribution);
		game.setPuzzleConditions(Puzzle.SolarPanels, Puzzle.SpaceSuit);
		game.setPuzzleConditions(Puzzle.HexagonInput, Puzzle.SpaceSuit);
		game.setPuzzleConditions(Puzzle.PowerCables, Puzzle.SolarPanels, Puzzle.HexagonInput);
		game.setPuzzleConditions(Puzzle.Plasmids, Puzzle.PowerCables);
		game.setPuzzleConditions(Puzzle.Tractors, Puzzle.Plasmids);
		game.setPuzzleConditions(Puzzle.Exit, Puzzle.Tractors);
		game.setRelevantObjectsForPuzzle(Puzzle.StarScanner, planetsZoom.gameObject, tablet.gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.BinarySudoku, numbersZoomable.gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.StarCharts, starchartDrives[0], starchartDrives[1], starchartDrives[2]);
		game.setRelevantObjectsForPuzzle(Puzzle.SolarPanels, solarPanelTransforms[0].Get<GameObject>(0f), solarPanelTransforms[1].Get<GameObject>(0f), solarPanelTransforms[2].Get<GameObject>(0f));
		game.setRelevantObjectsForPuzzle(Puzzle.HexagonInput, ventDoorSwitch.gameObject, auxZoom.gameObject);
		game.setHintCondition(Puzzle.StarScanner, StarScannerHint.GetTablet, () => game.wasAddedToInventoryDuringCurrentPuzzle(tablet.gameObject));
		game.setHintCondition(Puzzle.StarScanner, StarScannerHint.FindScreen, () => game.wasLookedAtCurrentPuzzle(planetsZoom.gameObject));
		game.setHintCondition(Puzzle.BinarySudoku, BinarySudokuHint.FindScreen, () => game.wasLookedAtCurrentPuzzle(numbersZoomable.gameObject));
		game.setHintCondition(Puzzle.StarCharts, StarChartsHint.Get2Drives, () => starchartProgress >= 3 || (game.wasAddedToInventoryDuringCurrentPuzzle(starchartDrives[1]) && game.wasAddedToInventoryDuringCurrentPuzzle(starchartDrives[2])));
		game.setHintCondition(Puzzle.StarCharts, StarChartsHint.GetDrive, () => starchartProgress >= 3 || game.wasAddedToInventoryDuringCurrentPuzzle(starchartDrives[0]));
		game.setHintCondition(Puzzle.StarCharts, StarChartsHint.PlaceDrives, () => starchartProgress >= 3);
		game.setHintCondition(Puzzle.PowerDistribution, UnlockGunHint.GetGunAndHint, () => game.wasAddedToInventoryDuringCurrentPuzzle(distributorManual.gameObject) && game.wasAddedToInventoryDuringCurrentPuzzle(distributor.gameObject));
		game.setHintCondition(Puzzle.SpaceSuit, SpaceSuitHint.GetDots, () => tokensSolved || game.wasAddedToInventoryDuringCurrentPuzzle(tokenItems[3].Get<Item>(0).gameObject));
		game.setHintCondition(Puzzle.SpaceSuit, SpaceSuitHint.GetGun, () => tokensSolved || game.wasAddedToInventoryDuringCurrentPuzzle(gun.gameObject));
		game.setHintCondition(Puzzle.SpaceSuit, SpaceSuitHint.SetPull, () => tokensSolved);
		game.setHintCondition(Puzzle.SpaceSuit, SpaceSuitHint.GetFuseBox, () => tokensSolved || game.wasAddedToInventoryDuringCurrentPuzzle(fuseBox.gameObject));
		game.setHintCondition(Puzzle.SpaceSuit, SpaceSuitHint.GetFuses, () => tokensSolved || (game.wasAddedToInventoryDuringCurrentPuzzle(tokenItems[2].Get<Item>(0).gameObject) && game.wasAddedToInventoryDuringCurrentPuzzle(tokenItems[1].Get<Item>(0).gameObject)));
		game.setHintCondition(Puzzle.SpaceSuit, SpaceSuitHint.GetLinesAndX, () => tokensSolved || (game.wasAddedToInventoryDuringCurrentPuzzle(tokenItems[0].Get<Item>(0).gameObject) && game.wasAddedToInventoryDuringCurrentPuzzle(tokenItems[4].Get<Item>(0).gameObject)));
		game.setHintCondition(Puzzle.SpaceSuit, SpaceSuitHint.LookAtHint, () => tokensSolved);
		game.setHintCondition(Puzzle.SpaceSuit, SpaceSuitHint.PlaceFuses, () => tokensSolved);
		game.setHintCondition(Puzzle.HexagonInput, HexagonInputHint.GoHere, () => ventDoorSwitch.state == Switch3DState.On);
		game.setHintCondition(Puzzle.HexagonInput, HexagonInputHint.OpenVault, () => ventDoorSwitch.state == Switch3DState.On);
		game.setHintCondition(Puzzle.PowerCables, PowerCablesHint.GetPower, () => distributionCompartmentTweens[4].findStateByName("Open").targetWeight == 1f);
		game.setHintCondition(Puzzle.PowerCables, PowerCablesHint.GetCable1, () => game.wasAddedToInventoryDuringCurrentPuzzle(plasmaCableSlots[1].acceptItems[0].gameObject));
		game.setHintCondition(Puzzle.PowerCables, PowerCablesHint.GetCable2, () => game.wasAddedToInventoryDuringCurrentPuzzle(plasmaCableSlots[2].acceptItems[0].gameObject));
		game.setHintCondition(Puzzle.PowerCables, PowerCablesHint.GetCable3, () => game.wasAddedToInventoryDuringCurrentPuzzle(plasmaCableSlots[0].acceptItems[0].gameObject));
		game.setHintCondition(Puzzle.Tractors, TractorsHint.GetPower, () => distributorPreviouslyPowered[3]);
	}

	[DebugButton("Give All Letter Tokens", Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void getAllChips()
	{
		game.addItemToInventory(tokenItems.Get<Transform>(0, 0f).gameObject);
		game.addItemToInventory(tokenItems.Get<Transform>(1, 0f).gameObject);
		game.addItemToInventory(tokenItems.Get<Transform>(2, 0f).gameObject);
		game.addItemToInventory(tokenItems.Get<Transform>(3, 0f).gameObject);
		game.addItemToInventory(tokenItems.Get<Transform>(4, 0f).gameObject);
	}

	[DebugButton("Disable Holographic Navmesh Blocker", Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void disableNavmeshBlocker()
	{
		Array.ForEach(holoNavmeshObstacles, delegate(GameObject x)
		{
			x.SetActive(value: false);
		});
		holoNavigationBlockerMS.setState("Disabled");
		zeroGUnlocked = true;
	}

	[DebugButton("Open All Distribution Doors", Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void openDistributionDoors()
	{
		suitDoorTS.gameObject.SetActive(value: false);
		smallCompartmentDoorTS.gameObject.SetActive(value: false);
		planetPuzzleCompartmentTS.gameObject.SetActive(value: false);
		gunDoorTS.gameObject.SetActive(value: false);
	}

	[DebugButton("Open Suit Door", Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void openSuitDoor()
	{
		tokensSolved = true;
		suitDoorTS.gameObject.SetActive(value: false);
		distributorUpdatePower();
	}

	[DebugButton("Open Gun Door", Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void openGunDoor()
	{
		gunDoorTS.gameObject.SetActive(value: false);
	}

	[DebugButton("Open Small Compartment Door", Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void openSmallCompartmentDoor()
	{
		smallCompartmentDoorTS.gameObject.SetActive(value: false);
	}

	[DebugButton("Open Planet Puzzle Compartment Door", Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void openPlanetPuzzleCompartmentDoor()
	{
		planetPuzzleCompartmentTS.gameObject.SetActive(value: false);
	}

	[DebugButton("Force Power To Everything", Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void forceDistributionPower()
	{
		forcePower = true;
		distributorUpdateTargets();
	}

	[DebugButton("Finish Episode", Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void finishEpisode()
	{
		game.levelCompleted();
	}

	[DebugButton("Activate Tractorbeam Puzzle", Tint.Default, PostClickAction.ReturnToGame | PostClickAction.HideButton, 0, new object[] { })]
	private void enableHoloship()
	{
		starchartSolve();
		forceDistributionPower();
	}

	[DebugButton("Give Unknown Power", Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void giveUnknownPower()
	{
	}

	public virtual void save(FastBinaryWriter writer)
	{
		writer.WriteGameObject(cutsceneAllSolvedStart);
		writer.WriteGameObject(cutsceneCablesSolved);
		writer.WriteGameObject(cutscenePowerAdded);
		writer.WriteGameObject(suitCinematic);
		writer.Write(in levelComplete, default(FastBinaryWriter.ForPrimitives));
		writer.WriteHashSet(zeroGRigidbodies, delegate(FastBinaryWriter w, GameObject e)
		{
			w.WriteGameObject(e);
		});
		writer.Write(in zeroGUnlocked, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in suitDispenserPowered, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in forcePower, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in navigationOnline, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in crashLogSent, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in communicationOnline, default(FastBinaryWriter.ForPrimitives));
		writer.WriteArray(tokenCurrentSolutionVisual, delegate(FastBinaryWriter w, string e)
		{
			w.Write(e);
		});
		writer.WriteArray(planetScreen, delegate(FastBinaryWriter w, GameObject e)
		{
			w.WriteGameObject(e);
		});
		writer.WriteComponent(planetScreenMainMesh);
		writer.WriteGameObject(planetWinScreen);
		writer.WriteComponent(planetsZoom);
		writer.WriteVector3(in planetsDragStartOffset);
		writer.WriteList(starchartActiveLines, delegate(FastBinaryWriter w, StarchartLine e)
		{
			w.WriteStarchartLine(e);
		});
		writer.Write(in firstStarSelected, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in starchartSectorsFound, default(FastBinaryWriter.ForPrimitives));
		writer.WriteArray(starchartLinePool, delegate(FastBinaryWriter w, LineRenderer e)
		{
			w.WriteComponent(e);
		});
		writer.WriteArray(starchartLinePoolGos, delegate(FastBinaryWriter w, GameObject e)
		{
			w.WriteGameObject(e);
		});
		writer.WriteQueue(availibleLines, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.Write(in starchartProgress, default(FastBinaryWriter.ForPrimitives));
		writer.WriteComponent(powerActiveCurrent);
		writer.WriteGameObject(tractorFieldPower);
		writer.WriteArray(powerSignMagnets, delegate(FastBinaryWriter w, MaterialState e)
		{
			w.WriteComponent(e);
		});
		writer.WriteArray(magnetsActive, delegate(FastBinaryWriter w, bool e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteGameObject(magnetAttachedTo);
		writer.WriteComponent(magnetButtonsTrigger);
		writer.Write(in asteroidFlashState, default(FastBinaryWriter.ForPrimitives));
		writer.WriteComponent(gunDial);
		writer.WriteComponent(gunDialTs);
		writer.Write(in gunValue, default(FastBinaryWriter.ForPrimitives));
		writer.WriteArray(distributorsOpened, delegate(FastBinaryWriter w, bool e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteArray(distributorButtonOriginalPositions, delegate(FastBinaryWriter w, Vector3 e)
		{
			w.WriteVector3(in e);
		});
		writer.WriteArray(distributorBarsInRow, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.Write(in distributorCurrentHighlightedIndex, default(FastBinaryWriter.ForPrimitives));
		writer.WriteArray(distributorRowsFilled, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.Write(in noiseSpeed, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in defaultNoiseSpeed, default(FastBinaryWriter.ForPrimitives));
		writer.WriteArray(distributorSlotStates, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteArray(distribtuionTargetPower, delegate(FastBinaryWriter w, float e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteArray(solarPanelTexts, delegate(FastBinaryWriter w, TMP_Text e)
		{
			w.WriteComponent(e);
		});
		writer.WriteArray(solarPanelScreenBarsRedGo, delegate(FastBinaryWriter w, GameObject e)
		{
			w.WriteGameObject(e);
		});
		writer.WriteArray(solarPanelScreenBarsYellowGo, delegate(FastBinaryWriter w, GameObject e)
		{
			w.WriteGameObject(e);
		});
		writer.WriteArray(solarPanelScreenBarsGreenGo, delegate(FastBinaryWriter w, GameObject e)
		{
			w.WriteGameObject(e);
		});
		writer.Write(in solarSolved, default(FastBinaryWriter.ForPrimitives));
		writer.WriteComponent(door1Open);
		writer.WriteComponent(door2Open);
		writer.WriteComponent(ventDoorSwitch);
		writer.WriteArray(percentages, delegate(FastBinaryWriter w, double e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.Write(in plasmaAsteroidsSolved, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in plasmaCablesSolved, default(FastBinaryWriter.ForPrimitives));
		writer.WriteVector3(in plasmaCenter);
		writer.Write(in asteroidsState, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in updateCubesDist, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in checkCubesFirstFrame, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in lastCubesDist, default(FastBinaryWriter.ForPrimitives));
		writer.WriteArray(auxInputNumbers, delegate(FastBinaryWriter w, AuxNumber e)
		{
			w.WriteAuxNumber(e);
		});
		writer.WriteArray(auxOverrideState, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteArray(auxOverrideSolution, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.Write(in auxCurrentIndex, default(FastBinaryWriter.ForPrimitives));
		writer.WriteComponent(auxChecking);
		writer.WriteGameObject(numberLines);
		writer.WriteGameObject(auxError);
		writer.WriteGameObject(auxCorrect);
		writer.Write(in currentMaxDepth, default(FastBinaryWriter.ForPrimitives));
		writer.WriteArray(chipsInSlot, delegate(FastBinaryWriter w, bool e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.Write(in DEBUG_HoloShipRotationSpeed, default(FastBinaryWriter.ForPrimitives));
		writer.WriteArray(redSolutions, delegate(FastBinaryWriter w, GameObject e)
		{
			w.WriteGameObject(e);
		});
		writer.WriteArray(greenSolutions, delegate(FastBinaryWriter w, GameObject e)
		{
			w.WriteGameObject(e);
		});
		writer.WriteArray(zeros, delegate(FastBinaryWriter w, GameObject e)
		{
			w.WriteGameObject(e);
		});
		writer.WriteArray(ones, delegate(FastBinaryWriter w, GameObject e)
		{
			w.WriteGameObject(e);
		});
		writer.WriteArray(numberSwitches, delegate(FastBinaryWriter w, Switch3D e)
		{
			w.WriteComponent(e);
		});
		writer.WriteComponent(comsOffline);
		writer.WriteComponent(comsOffline2);
		writer.WriteComponent(comsOnline);
		writer.WriteComponent(numbersZoomable);
		writer.WriteArray2D(centerTargets, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteArray2D(gridValues, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteComponent(hologramSwitch);
		writer.WriteComponent(hologramSwitchMS);
		writer.Write(in holoshipGlowState, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in tokensSolved, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in suitEquipped, default(FastBinaryWriter.ForPrimitives));
		writer.WriteArray(distributorPreviouslyPowered, delegate(FastBinaryWriter w, bool e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
	}

	public virtual void load(FastBinaryReader reader)
	{
		cutsceneAllSolvedStart = reader.ReadGameObject();
		cutsceneCablesSolved = reader.ReadGameObject();
		cutscenePowerAdded = reader.ReadGameObject();
		suitCinematic = reader.ReadGameObject();
		levelComplete = reader.ReadBoolean();
		zeroGRigidbodies = reader.ReadHashSet((FastBinaryReader r) => r.ReadGameObject());
		zeroGUnlocked = reader.ReadBoolean();
		suitDispenserPowered = reader.ReadBoolean();
		forcePower = reader.ReadBoolean();
		navigationOnline = reader.ReadBoolean();
		crashLogSent = reader.ReadBoolean();
		communicationOnline = reader.ReadBoolean();
		tokenCurrentSolutionVisual = reader.ReadArray((FastBinaryReader r) => r.ReadString());
		planetScreen = reader.ReadArray((FastBinaryReader r) => r.ReadGameObject());
		planetScreenMainMesh = reader.ReadComponent<MeshRenderer>();
		planetWinScreen = reader.ReadGameObject();
		planetsZoom = reader.ReadComponent<Zoomable>();
		planetsDragStartOffset = reader.ReadVector3();
		starchartActiveLines = reader.ReadList((FastBinaryReader r) => r.ReadStarchartLine());
		firstStarSelected = reader.ReadInt32();
		starchartSectorsFound = reader.ReadInt32();
		starchartLinePool = reader.ReadArray((FastBinaryReader r) => r.ReadComponent<LineRenderer>());
		starchartLinePoolGos = reader.ReadArray((FastBinaryReader r) => r.ReadGameObject());
		availibleLines = reader.ReadQueue((FastBinaryReader r) => r.ReadInt32());
		starchartProgress = reader.ReadInt32();
		powerActiveCurrent = reader.ReadComponent<TextMeshProUGUI>();
		tractorFieldPower = reader.ReadGameObject();
		powerSignMagnets = reader.ReadArray((FastBinaryReader r) => r.ReadComponent<MaterialState>());
		magnetsActive = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		magnetAttachedTo = reader.ReadGameObject();
		magnetButtonsTrigger = reader.ReadComponent<Trigger>();
		asteroidFlashState = reader.ReadBoolean();
		gunDial = reader.ReadComponent<Switch3D>();
		gunDialTs = reader.ReadComponent<TweenState>();
		gunValue = reader.ReadInt32();
		distributorsOpened = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		distributorButtonOriginalPositions = reader.ReadArray((FastBinaryReader r) => r.ReadVector3());
		distributorBarsInRow = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		distributorCurrentHighlightedIndex = reader.ReadInt32();
		distributorRowsFilled = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		noiseSpeed = reader.ReadSingle();
		defaultNoiseSpeed = reader.ReadSingle();
		distributorSlotStates = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		distribtuionTargetPower = reader.ReadArray((FastBinaryReader r) => r.ReadSingle());
		solarPanelTexts = reader.ReadArray((FastBinaryReader r) => r.ReadComponent<TMP_Text>());
		solarPanelScreenBarsRedGo = reader.ReadArray((FastBinaryReader r) => r.ReadGameObject());
		solarPanelScreenBarsYellowGo = reader.ReadArray((FastBinaryReader r) => r.ReadGameObject());
		solarPanelScreenBarsGreenGo = reader.ReadArray((FastBinaryReader r) => r.ReadGameObject());
		solarSolved = reader.ReadBoolean();
		door1Open = reader.ReadComponent<ParticleSystem>();
		door2Open = reader.ReadComponent<ParticleSystem>();
		ventDoorSwitch = reader.ReadComponent<Switch3D>();
		percentages = reader.ReadArray((FastBinaryReader r) => r.ReadDouble());
		plasmaAsteroidsSolved = reader.ReadBoolean();
		plasmaCablesSolved = reader.ReadBoolean();
		plasmaCenter = reader.ReadVector3();
		asteroidsState = reader.ReadInt32();
		updateCubesDist = reader.ReadBoolean();
		checkCubesFirstFrame = reader.ReadBoolean();
		lastCubesDist = reader.ReadSingle();
		auxInputNumbers = reader.ReadArray((FastBinaryReader r) => r.ReadAuxNumber());
		auxOverrideState = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		auxOverrideSolution = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		auxCurrentIndex = reader.ReadInt32();
		auxChecking = reader.ReadComponent<TweenState>();
		numberLines = reader.ReadGameObject();
		auxError = reader.ReadGameObject();
		auxCorrect = reader.ReadGameObject();
		currentMaxDepth = reader.ReadInt32();
		chipsInSlot = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		DEBUG_HoloShipRotationSpeed = reader.ReadSingle();
		redSolutions = reader.ReadArray((FastBinaryReader r) => r.ReadGameObject());
		greenSolutions = reader.ReadArray((FastBinaryReader r) => r.ReadGameObject());
		zeros = reader.ReadArray((FastBinaryReader r) => r.ReadGameObject());
		ones = reader.ReadArray((FastBinaryReader r) => r.ReadGameObject());
		numberSwitches = reader.ReadArray((FastBinaryReader r) => r.ReadComponent<Switch3D>());
		comsOffline = reader.ReadComponent<MeshRenderer>();
		comsOffline2 = reader.ReadComponent<MeshRenderer>();
		comsOnline = reader.ReadComponent<MeshRenderer>();
		numbersZoomable = reader.ReadComponent<Zoomable>();
		centerTargets = reader.ReadArray2D((FastBinaryReader r) => r.ReadInt32());
		gridValues = reader.ReadArray2D((FastBinaryReader r) => r.ReadInt32());
		hologramSwitch = reader.ReadComponent<Switch3D>();
		hologramSwitchMS = reader.ReadComponent<MaterialState>();
		holoshipGlowState = reader.ReadBoolean();
		tokensSolved = reader.ReadBoolean();
		suitEquipped = reader.ReadBoolean();
		distributorPreviouslyPowered = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
	}

	public virtual void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		int position = reader.Position;
		GameObject arg = reader.ReadGameObject();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "cutsceneAllSolvedStart",
			fieldValue = $"{arg}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		GameObject arg2 = reader.ReadGameObject();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "cutsceneCablesSolved",
			fieldValue = $"{arg2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		GameObject arg3 = reader.ReadGameObject();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "cutscenePowerAdded",
			fieldValue = $"{arg3}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		GameObject arg4 = reader.ReadGameObject();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "suitCinematic",
			fieldValue = $"{arg4}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "levelComplete",
			fieldValue = $"{flag}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		HashSet<GameObject> hashSet = reader.ReadHashSet((FastBinaryReader r) => r.ReadGameObject());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "zeroGRigidbodies[" + ((hashSet == null) ? string.Empty : hashSet.Count.ToString()) + "]",
			fieldValue = (((hashSet == null) ? "null" : string.Join(", ", hashSet)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag2 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "zeroGUnlocked",
			fieldValue = $"{flag2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag3 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "suitDispenserPowered",
			fieldValue = $"{flag3}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag4 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "forcePower",
			fieldValue = $"{flag4}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag5 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "navigationOnline",
			fieldValue = $"{flag5}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag6 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "crashLogSent",
			fieldValue = $"{flag6}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag7 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "communicationOnline",
			fieldValue = $"{flag7}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		string[] array = reader.ReadArray((FastBinaryReader r) => r.ReadString());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "tokenCurrentSolutionVisual[" + ((array == null) ? string.Empty : array.Length.ToString()) + "]",
			fieldValue = (((array == null) ? "null" : string.Join<string>(", ", (IEnumerable<string>)array)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		GameObject[] array2 = reader.ReadArray((FastBinaryReader r) => r.ReadGameObject());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "planetScreen[" + ((array2 == null) ? string.Empty : array2.Length.ToString()) + "]",
			fieldValue = (((array2 == null) ? "null" : string.Join(", ", (IEnumerable<GameObject>)array2)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		MeshRenderer arg5 = reader.ReadComponent<MeshRenderer>();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "planetScreenMainMesh",
			fieldValue = $"{arg5}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		GameObject arg6 = reader.ReadGameObject();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "planetWinScreen",
			fieldValue = $"{arg6}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Zoomable arg7 = reader.ReadComponent<Zoomable>();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "planetsZoom",
			fieldValue = $"{arg7}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Vector3 vector = reader.ReadVector3();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "planetsDragStartOffset",
			fieldValue = $"{vector}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<StarchartLine> list = reader.ReadList((FastBinaryReader r) => r.ReadStarchartLine());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "starchartActiveLines[" + ((list == null) ? string.Empty : list.Count.ToString()) + "]",
			fieldValue = (((list == null) ? "null" : string.Join(", ", list)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "firstStarSelected",
			fieldValue = $"{num}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num2 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "starchartSectorsFound",
			fieldValue = $"{num2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		LineRenderer[] array3 = reader.ReadArray((FastBinaryReader r) => r.ReadComponent<LineRenderer>());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "starchartLinePool[" + ((array3 == null) ? string.Empty : array3.Length.ToString()) + "]",
			fieldValue = (((array3 == null) ? "null" : string.Join(", ", (IEnumerable<LineRenderer>)array3)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		GameObject[] array4 = reader.ReadArray((FastBinaryReader r) => r.ReadGameObject());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "starchartLinePoolGos[" + ((array4 == null) ? string.Empty : array4.Length.ToString()) + "]",
			fieldValue = (((array4 == null) ? "null" : string.Join(", ", (IEnumerable<GameObject>)array4)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Queue<int> queue = reader.ReadQueue((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "availibleLines[" + ((queue == null) ? string.Empty : queue.Count.ToString()) + "]",
			fieldValue = (((queue == null) ? "null" : string.Join(", ", queue)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num3 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "starchartProgress",
			fieldValue = $"{num3}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		TextMeshProUGUI arg8 = reader.ReadComponent<TextMeshProUGUI>();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "powerActiveCurrent",
			fieldValue = $"{arg8}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		GameObject arg9 = reader.ReadGameObject();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "tractorFieldPower",
			fieldValue = $"{arg9}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		MaterialState[] array5 = reader.ReadArray((FastBinaryReader r) => r.ReadComponent<MaterialState>());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "powerSignMagnets[" + ((array5 == null) ? string.Empty : array5.Length.ToString()) + "]",
			fieldValue = (((array5 == null) ? "null" : string.Join(", ", (IEnumerable<MaterialState>)array5)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool[] array6 = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "magnetsActive[" + ((array6 == null) ? string.Empty : array6.Length.ToString()) + "]",
			fieldValue = (((array6 == null) ? "null" : string.Join(", ", array6)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		GameObject arg10 = reader.ReadGameObject();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "magnetAttachedTo",
			fieldValue = $"{arg10}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Trigger arg11 = reader.ReadComponent<Trigger>();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "magnetButtonsTrigger",
			fieldValue = $"{arg11}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag8 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "asteroidFlashState",
			fieldValue = $"{flag8}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Switch3D arg12 = reader.ReadComponent<Switch3D>();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "gunDial",
			fieldValue = $"{arg12}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		TweenState arg13 = reader.ReadComponent<TweenState>();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "gunDialTs",
			fieldValue = $"{arg13}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num4 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "gunValue",
			fieldValue = $"{num4}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool[] array7 = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "distributorsOpened[" + ((array7 == null) ? string.Empty : array7.Length.ToString()) + "]",
			fieldValue = (((array7 == null) ? "null" : string.Join(", ", array7)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Vector3[] array8 = reader.ReadArray((FastBinaryReader r) => r.ReadVector3());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "distributorButtonOriginalPositions[" + ((array8 == null) ? string.Empty : array8.Length.ToString()) + "]",
			fieldValue = (((array8 == null) ? "null" : string.Join(", ", array8)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int[] array9 = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "distributorBarsInRow[" + ((array9 == null) ? string.Empty : array9.Length.ToString()) + "]",
			fieldValue = (((array9 == null) ? "null" : string.Join(", ", array9)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num5 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "distributorCurrentHighlightedIndex",
			fieldValue = $"{num5}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int[] array10 = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "distributorRowsFilled[" + ((array10 == null) ? string.Empty : array10.Length.ToString()) + "]",
			fieldValue = (((array10 == null) ? "null" : string.Join(", ", array10)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num6 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "noiseSpeed",
			fieldValue = $"{num6}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num7 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "defaultNoiseSpeed",
			fieldValue = $"{num7}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int[] array11 = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "distributorSlotStates[" + ((array11 == null) ? string.Empty : array11.Length.ToString()) + "]",
			fieldValue = (((array11 == null) ? "null" : string.Join(", ", array11)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float[] array12 = reader.ReadArray((FastBinaryReader r) => r.ReadSingle());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "distribtuionTargetPower[" + ((array12 == null) ? string.Empty : array12.Length.ToString()) + "]",
			fieldValue = (((array12 == null) ? "null" : string.Join(", ", array12)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		TMP_Text[] array13 = reader.ReadArray((FastBinaryReader r) => r.ReadComponent<TMP_Text>());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "solarPanelTexts[" + ((array13 == null) ? string.Empty : array13.Length.ToString()) + "]",
			fieldValue = (((array13 == null) ? "null" : string.Join(", ", (IEnumerable<TMP_Text>)array13)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		GameObject[] array14 = reader.ReadArray((FastBinaryReader r) => r.ReadGameObject());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "solarPanelScreenBarsRedGo[" + ((array14 == null) ? string.Empty : array14.Length.ToString()) + "]",
			fieldValue = (((array14 == null) ? "null" : string.Join(", ", (IEnumerable<GameObject>)array14)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		GameObject[] array15 = reader.ReadArray((FastBinaryReader r) => r.ReadGameObject());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "solarPanelScreenBarsYellowGo[" + ((array15 == null) ? string.Empty : array15.Length.ToString()) + "]",
			fieldValue = (((array15 == null) ? "null" : string.Join(", ", (IEnumerable<GameObject>)array15)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		GameObject[] array16 = reader.ReadArray((FastBinaryReader r) => r.ReadGameObject());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "solarPanelScreenBarsGreenGo[" + ((array16 == null) ? string.Empty : array16.Length.ToString()) + "]",
			fieldValue = (((array16 == null) ? "null" : string.Join(", ", (IEnumerable<GameObject>)array16)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag9 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "solarSolved",
			fieldValue = $"{flag9}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		ParticleSystem arg14 = reader.ReadComponent<ParticleSystem>();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "door1Open",
			fieldValue = $"{arg14}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		ParticleSystem arg15 = reader.ReadComponent<ParticleSystem>();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "door2Open",
			fieldValue = $"{arg15}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Switch3D arg16 = reader.ReadComponent<Switch3D>();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "ventDoorSwitch",
			fieldValue = $"{arg16}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		double[] array17 = reader.ReadArray((FastBinaryReader r) => r.ReadDouble());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "percentages[" + ((array17 == null) ? string.Empty : array17.Length.ToString()) + "]",
			fieldValue = (((array17 == null) ? "null" : string.Join(", ", array17)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag10 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "plasmaAsteroidsSolved",
			fieldValue = $"{flag10}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag11 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "plasmaCablesSolved",
			fieldValue = $"{flag11}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Vector3 vector2 = reader.ReadVector3();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "plasmaCenter",
			fieldValue = $"{vector2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num8 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "asteroidsState",
			fieldValue = $"{num8}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag12 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "updateCubesDist",
			fieldValue = $"{flag12}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag13 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "checkCubesFirstFrame",
			fieldValue = $"{flag13}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num9 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "lastCubesDist",
			fieldValue = $"{num9}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		AuxNumber[] array18 = reader.ReadArray((FastBinaryReader r) => r.ReadAuxNumber());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "auxInputNumbers[" + ((array18 == null) ? string.Empty : array18.Length.ToString()) + "]",
			fieldValue = (((array18 == null) ? "null" : string.Join(", ", (IEnumerable<AuxNumber>)array18)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int[] array19 = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "auxOverrideState[" + ((array19 == null) ? string.Empty : array19.Length.ToString()) + "]",
			fieldValue = (((array19 == null) ? "null" : string.Join(", ", array19)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int[] array20 = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "auxOverrideSolution[" + ((array20 == null) ? string.Empty : array20.Length.ToString()) + "]",
			fieldValue = (((array20 == null) ? "null" : string.Join(", ", array20)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num10 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "auxCurrentIndex",
			fieldValue = $"{num10}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		TweenState arg17 = reader.ReadComponent<TweenState>();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "auxChecking",
			fieldValue = $"{arg17}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		GameObject arg18 = reader.ReadGameObject();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "numberLines",
			fieldValue = $"{arg18}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		GameObject arg19 = reader.ReadGameObject();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "auxError",
			fieldValue = $"{arg19}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		GameObject arg20 = reader.ReadGameObject();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "auxCorrect",
			fieldValue = $"{arg20}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num11 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "currentMaxDepth",
			fieldValue = $"{num11}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool[] array21 = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "chipsInSlot[" + ((array21 == null) ? string.Empty : array21.Length.ToString()) + "]",
			fieldValue = (((array21 == null) ? "null" : string.Join(", ", array21)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num12 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "DEBUG_HoloShipRotationSpeed",
			fieldValue = $"{num12}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		GameObject[] array22 = reader.ReadArray((FastBinaryReader r) => r.ReadGameObject());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "redSolutions[" + ((array22 == null) ? string.Empty : array22.Length.ToString()) + "]",
			fieldValue = (((array22 == null) ? "null" : string.Join(", ", (IEnumerable<GameObject>)array22)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		GameObject[] array23 = reader.ReadArray((FastBinaryReader r) => r.ReadGameObject());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "greenSolutions[" + ((array23 == null) ? string.Empty : array23.Length.ToString()) + "]",
			fieldValue = (((array23 == null) ? "null" : string.Join(", ", (IEnumerable<GameObject>)array23)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		GameObject[] array24 = reader.ReadArray((FastBinaryReader r) => r.ReadGameObject());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "zeros[" + ((array24 == null) ? string.Empty : array24.Length.ToString()) + "]",
			fieldValue = (((array24 == null) ? "null" : string.Join(", ", (IEnumerable<GameObject>)array24)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		GameObject[] array25 = reader.ReadArray((FastBinaryReader r) => r.ReadGameObject());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "ones[" + ((array25 == null) ? string.Empty : array25.Length.ToString()) + "]",
			fieldValue = (((array25 == null) ? "null" : string.Join(", ", (IEnumerable<GameObject>)array25)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Switch3D[] array26 = reader.ReadArray((FastBinaryReader r) => r.ReadComponent<Switch3D>());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "numberSwitches[" + ((array26 == null) ? string.Empty : array26.Length.ToString()) + "]",
			fieldValue = (((array26 == null) ? "null" : string.Join(", ", (IEnumerable<Switch3D>)array26)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		MeshRenderer arg21 = reader.ReadComponent<MeshRenderer>();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "comsOffline",
			fieldValue = $"{arg21}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		MeshRenderer arg22 = reader.ReadComponent<MeshRenderer>();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "comsOffline2",
			fieldValue = $"{arg22}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		MeshRenderer arg23 = reader.ReadComponent<MeshRenderer>();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "comsOnline",
			fieldValue = $"{arg23}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Zoomable arg24 = reader.ReadComponent<Zoomable>();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "numbersZoomable",
			fieldValue = $"{arg24}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int[,] array27 = reader.ReadArray2D((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "centerTargets[" + ((array27 == null) ? string.Empty : array27.Length.ToString()) + "]",
			fieldValue = (((array27 == null) ? "null" : "NOT SUPPORTED") ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int[,] array28 = reader.ReadArray2D((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "gridValues[" + ((array28 == null) ? string.Empty : array28.Length.ToString()) + "]",
			fieldValue = (((array28 == null) ? "null" : "NOT SUPPORTED") ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Switch3D arg25 = reader.ReadComponent<Switch3D>();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "hologramSwitch",
			fieldValue = $"{arg25}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		MaterialState arg26 = reader.ReadComponent<MaterialState>();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "hologramSwitchMS",
			fieldValue = $"{arg26}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag14 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "holoshipGlowState",
			fieldValue = $"{flag14}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag15 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "tokensSolved",
			fieldValue = $"{flag15}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag16 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "suitEquipped",
			fieldValue = $"{flag16}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool[] array29 = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "distributorPreviouslyPowered[" + ((array29 == null) ? string.Empty : array29.Length.ToString()) + "]",
			fieldValue = (((array29 == null) ? "null" : string.Join(", ", array29)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
	}

	public override int getPacketCount()
	{
		return Space2.getPacketCount();
	}

	public override Packet getPacket(byte id)
	{
		return Space2.getPacket(id);
	}

	public override Timer getTimer(byte id)
	{
		return id switch
		{
			0 => new CompartmentUnlockTimer(), 
			1 => new PlasmaCompleteTimer(), 
			2 => new AsteroidSolveTimer(), 
			3 => new HoloshipCompleteTimer(), 
			4 => new TokenErrorTimer(), 
			5 => new SolarPanelToggleTimer(), 
			6 => new SolarPanelZoomTimer(), 
			7 => new TokenIncorrectTimer(), 
			8 => new TokenUpdateSequenceTimer(), 
			9 => new TokenLoadingTimer(), 
			10 => new TokenCheckTimer(), 
			11 => new AuxNodeHighlightDelayTimer(), 
			12 => new AuxNodeFlashTimer(), 
			13 => new AuxSwitchPauseTimer(), 
			14 => new AuxSwitchCheckTimer(), 
			15 => new AuxSwitchErrorTimer(), 
			16 => new SpaceSuitTextFlashTimer(), 
			17 => new SpaceSuitSequence_1Timer(), 
			18 => new SpaceSuitSequence_2Timer(), 
			19 => new SpaceSuitSequence_3Timer(), 
			20 => new AsteroidSwapStartTimer(), 
			21 => new AsteroidSwapTimer(), 
			22 => new PlanetSendWaitTimer(), 
			23 => new PlanetSendFlashTimer(), 
			24 => new PlanetSendFoundTimer(), 
			25 => new StarchartSendWaitTimer(), 
			26 => new StarchartCompleteTimer(), 
			27 => new HoloshipPowerTimer(), 
			28 => new BitParityToggleTimer(), 
			29 => new BitParityCheckTimer(), 
			30 => new FadeOutTimer(), 
			31 => new FadeInTimer(), 
			32 => new FadeInOutDelayedTimer(), 
			33 => new AsteroidFlashTimer(), 
			34 => new PowerShipFinalAnimTimer(), 
			35 => new SwitchesPowerOnOffTimer(), 
			36 => new CompartmentOpenTimer(), 
			37 => new FadeOutLockTimer(), 
			38 => new StarchartDriveCompleteTimer(), 
			39 => new CutsceneAllSolvedStartTimer(), 
			40 => new CutsceneCablesSolvedTimer(), 
			41 => new CutscenePowerAddedTimer(), 
			42 => new SuitCInematicTimer(), 
			43 => new SolveNumbersAnimTimer(), 
			44 => new PlasmaCubesDistTimer(), 
			_ => null, 
		};
	}
}
