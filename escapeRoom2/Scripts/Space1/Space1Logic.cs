using System;
using System.Collections.Generic;
using System.Text;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.UI;

public class Space1Logic : LevelLogic, ISaveable
{
	public sealed class MedBedBlinkTimer : Timer
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

	public sealed class MedBedPowerTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 1;
		}

		public MedBedPowerTimer()
		{
		}

		public MedBedPowerTimer(int index)
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

	public sealed class AudioAnalyzerTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 2;
		}

		public AudioAnalyzerTimer()
		{
		}

		public AudioAnalyzerTimer(int index)
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

	public sealed class PodAnswerTimer : Timer
	{
		public int podIndex;

		public int index;

		public int choice;

		public override byte getTypeId()
		{
			return 3;
		}

		public PodAnswerTimer()
		{
		}

		public PodAnswerTimer(int podIndex, int index, int choice)
		{
			this.podIndex = podIndex;
			this.index = index;
			this.choice = choice;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in podIndex, default(FastBinaryWriter.ForPrimitives));
			writer.Write(in index, default(FastBinaryWriter.ForPrimitives));
			writer.Write(in choice, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			podIndex = reader.ReadInt32();
			index = reader.ReadInt32();
			choice = reader.ReadInt32();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("podIndex: " + $"{podIndex}");
			stringBuilder.AppendLine("index: " + $"{index}");
			stringBuilder.Append("choice: " + $"{choice}");
			return stringBuilder.ToString();
		}
	}

	public sealed class PodMedTimer : Timer
	{
		public int podIndex;

		public override byte getTypeId()
		{
			return 4;
		}

		public PodMedTimer()
		{
		}

		public PodMedTimer(int podIndex)
		{
			this.podIndex = podIndex;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in podIndex, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			podIndex = reader.ReadInt32();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("podIndex: " + $"{podIndex}");
			return stringBuilder.ToString();
		}
	}

	public sealed class PodMedTimer0Timer : Timer
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

	public sealed class PodMedTimer1Timer : Timer
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

	public sealed class PodMedTimer2Timer : Timer
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

	public sealed class PodMedTimer3Timer : Timer
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

	public sealed class PodMedButtonTimer : Timer
	{
		public int index;

		public bool goingDown;

		public override byte getTypeId()
		{
			return 9;
		}

		public PodMedButtonTimer()
		{
		}

		public PodMedButtonTimer(int index, bool goingDown)
		{
			this.index = index;
			this.goingDown = goingDown;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in index, default(FastBinaryWriter.ForPrimitives));
			writer.Write(in goingDown, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			index = reader.ReadInt32();
			goingDown = reader.ReadBoolean();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("index: " + $"{index}");
			stringBuilder.Append("goingDown: " + $"{goingDown}");
			return stringBuilder.ToString();
		}
	}

	public sealed class MedBedComponentsTimer : Timer
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

	public sealed class MedBedSolvedTimer : Timer
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

	public sealed class HoloTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 12;
		}

		public HoloTimer()
		{
		}

		public HoloTimer(int index)
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

	public sealed class MedBedMatchingTimer : Timer
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

	public sealed class ScannedBacteriaTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 14;
		}

		public ScannedBacteriaTimer()
		{
		}

		public ScannedBacteriaTimer(int index)
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

	public sealed class MedBedCheckBacteriaTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 15;
		}

		public MedBedCheckBacteriaTimer()
		{
		}

		public MedBedCheckBacteriaTimer(int index)
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

	public sealed class BacteriaSolvedTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 16;
		}

		public BacteriaSolvedTimer()
		{
		}

		public BacteriaSolvedTimer(int index)
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

	public sealed class MedicinePuzzleScreenTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 17;
		}

		public MedicinePuzzleScreenTimer()
		{
		}

		public MedicinePuzzleScreenTimer(int index)
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

	public sealed class ReleaseMedicineTimer : Timer
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

	public sealed class AirlockSlotTimerTimer : Timer
	{
		public int index;

		public int screwIndex;

		public override byte getTypeId()
		{
			return 19;
		}

		public AirlockSlotTimerTimer()
		{
		}

		public AirlockSlotTimerTimer(int index, int screwIndex)
		{
			this.index = index;
			this.screwIndex = screwIndex;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in index, default(FastBinaryWriter.ForPrimitives));
			writer.Write(in screwIndex, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			index = reader.ReadInt32();
			screwIndex = reader.ReadInt32();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("index: " + $"{index}");
			stringBuilder.Append("screwIndex: " + $"{screwIndex}");
			return stringBuilder.ToString();
		}
	}

	public sealed class CorridorScanTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 20;
		}

		public CorridorScanTimer()
		{
		}

		public CorridorScanTimer(int index)
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

	public sealed class SolveCrateTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 21;
		}

		public SolveCrateTimer()
		{
		}

		public SolveCrateTimer(int index)
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

	public sealed class SplicerMiddleTimer : Timer
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

	public sealed class SplicerBottomTimer : Timer
	{
		public int index;

		public float totalPreviousLogTimes;

		public override byte getTypeId()
		{
			return 23;
		}

		public SplicerBottomTimer()
		{
		}

		public SplicerBottomTimer(int index, float totalPreviousLogTimes)
		{
			this.index = index;
			this.totalPreviousLogTimes = totalPreviousLogTimes;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in index, default(FastBinaryWriter.ForPrimitives));
			writer.Write(in totalPreviousLogTimes, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			index = reader.ReadInt32();
			totalPreviousLogTimes = reader.ReadSingle();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("index: " + $"{index}");
			stringBuilder.Append("totalPreviousLogTimes: " + $"{totalPreviousLogTimes}");
			return stringBuilder.ToString();
		}
	}

	public sealed class SplicedBottomTrackerTransition : Transition
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

	public sealed class ScannerSlidableTransition : Transition
	{
		public override byte getTypeId()
		{
			return 25;
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

	public sealed class HologramTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 26;
		}

		public HologramTimer()
		{
		}

		public HologramTimer(int index)
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

	public sealed class PressUIButtonAnimationTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 27;
		}

		public PressUIButtonAnimationTimer()
		{
		}

		public PressUIButtonAnimationTimer(int index)
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

	public sealed class CaptainsPodTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 28;
		}

		public CaptainsPodTimer()
		{
		}

		public CaptainsPodTimer(int index)
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

	public sealed class LockerTimer : Timer
	{
		public Switch3D door;

		public int index;

		public override byte getTypeId()
		{
			return 29;
		}

		public LockerTimer()
		{
		}

		public LockerTimer(Switch3D door, int index)
		{
			this.door = door;
			this.index = index;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.WriteComponent(door);
			writer.Write(in index, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			door = reader.ReadComponent<Switch3D>();
			index = reader.ReadInt32();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("door: " + $"{door}");
			stringBuilder.Append("index: " + $"{index}");
			return stringBuilder.ToString();
		}
	}

	public sealed class ChipScreensTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 30;
		}

		public ChipScreensTimer()
		{
		}

		public ChipScreensTimer(int index)
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

	public sealed class CrateBlinkingTimer : Timer
	{
		public TweenState tween;

		public SwapperPiece swapperPiece;

		public override byte getTypeId()
		{
			return 31;
		}

		public CrateBlinkingTimer()
		{
		}

		public CrateBlinkingTimer(TweenState tween, SwapperPiece swapperPiece)
		{
			this.tween = tween;
			this.swapperPiece = swapperPiece;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.WriteComponent(tween);
			writer.WriteComponent(swapperPiece);
		}

		public override void readData(FastBinaryReader reader)
		{
			tween = reader.ReadComponent<TweenState>();
			swapperPiece = reader.ReadComponent<SwapperPiece>();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("tween: " + $"{tween}");
			stringBuilder.Append("swapperPiece: " + $"{swapperPiece}");
			return stringBuilder.ToString();
		}
	}

	public sealed class RfidBlinkingTimer : Timer
	{
		public bool isGoingDown;

		public override byte getTypeId()
		{
			return 32;
		}

		public RfidBlinkingTimer()
		{
		}

		public RfidBlinkingTimer(bool isGoingDown)
		{
			this.isGoingDown = isGoingDown;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in isGoingDown, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			isGoingDown = reader.ReadBoolean();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("isGoingDown: " + $"{isGoingDown}");
			return stringBuilder.ToString();
		}
	}

	public sealed class AirlockKeypadTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 33;
		}

		public AirlockKeypadTimer()
		{
		}

		public AirlockKeypadTimer(int index)
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

	public sealed class WallHintTimer : Timer
	{
		public int index;

		public bool isGoingDown;

		public override byte getTypeId()
		{
			return 34;
		}

		public WallHintTimer()
		{
		}

		public WallHintTimer(int index, bool isGoingDown)
		{
			this.index = index;
			this.isGoingDown = isGoingDown;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in index, default(FastBinaryWriter.ForPrimitives));
			writer.Write(in isGoingDown, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			index = reader.ReadInt32();
			isGoingDown = reader.ReadBoolean();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("index: " + $"{index}");
			stringBuilder.Append("isGoingDown: " + $"{isGoingDown}");
			return stringBuilder.ToString();
		}
	}

	public sealed class RfidSlotTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 35;
		}

		public RfidSlotTimer()
		{
		}

		public RfidSlotTimer(int index)
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

	public sealed class RfidLEDTimer : Timer
	{
		public int index;

		public int count;

		public override byte getTypeId()
		{
			return 36;
		}

		public RfidLEDTimer()
		{
		}

		public RfidLEDTimer(int index, int count)
		{
			this.index = index;
			this.count = count;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in index, default(FastBinaryWriter.ForPrimitives));
			writer.Write(in count, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			index = reader.ReadInt32();
			count = reader.ReadInt32();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("index: " + $"{index}");
			stringBuilder.Append("count: " + $"{count}");
			return stringBuilder.ToString();
		}
	}

	public sealed class MedBedEnableHoloTimer : Timer
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

	public sealed class UiBlinkTimer : Timer
	{
		public TweenState tweenState;

		public MaterialState materialState;

		public int index;

		public string state;

		public int times;

		public override byte getTypeId()
		{
			return 38;
		}

		public UiBlinkTimer()
		{
		}

		public UiBlinkTimer(TweenState tweenState, MaterialState materialState, int index, string state, int times)
		{
			this.tweenState = tweenState;
			this.materialState = materialState;
			this.index = index;
			this.state = state;
			this.times = times;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.WriteComponent(tweenState);
			writer.WriteComponent(materialState);
			writer.Write(in index, default(FastBinaryWriter.ForPrimitives));
			writer.Write(state);
			writer.Write(in times, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			tweenState = reader.ReadComponent<TweenState>();
			materialState = reader.ReadComponent<MaterialState>();
			index = reader.ReadInt32();
			state = reader.ReadString();
			times = reader.ReadInt32();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("tweenState: " + $"{tweenState}");
			stringBuilder.AppendLine("materialState: " + $"{materialState}");
			stringBuilder.AppendLine("index: " + $"{index}");
			stringBuilder.AppendLine("state: " + ToStringHelper.Stringify(state));
			stringBuilder.Append("times: " + $"{times}");
			return stringBuilder.ToString();
		}
	}

	public sealed class LoadingSoundTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 39;
		}

		public LoadingSoundTimer()
		{
		}

		public LoadingSoundTimer(int index)
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

	public sealed class HoloCardTimer : Timer
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

	[Serializable]
	public class DialogQuestion
	{
		[DontSave]
		public string question;

		[DontSave]
		public List<string> answers;

		[DontSave]
		public int correctAnswer;
	}

	private enum PodScreens
	{
		None = -1,
		WelcomeBack = 0,
		InitialScan = 1,
		Results = 2,
		Answer3 = 3,
		NeedMedicine = 4,
		AdministeringFirstAid = 5,
		CorrectAnswer = 6,
		Answer4 = 7,
		Success = 8
	}

	public class TriangleKeypad
	{
		public List<int> solution;

		public List<int> current;

		public Switch3D[] buttons;

		public List<GameObject> screens;

		public Switch3D door;

		public Zoomable zoom;

		public Item[] items;

		public bool solved;

		public int count;

		public int currentIndex => count % solution.Count;
	}

	private enum ChipScreens
	{
		ChipPuzzle = 0,
		Loading = 1,
		ChipSolved = 2
	}

	public enum ComponentScreens
	{
		InsertKey = 0,
		AddBatteries = 1,
		ComponentCounter = 2,
		CheckingBackground = 3,
		CheckingComponents = 4,
		WrongComponents = 5,
		MissingComponents = 6,
		SystemOperational = 7
	}

	private enum BedScreens
	{
		SystemNotOperational = 0,
		StartScan = 1,
		Scanning = 2,
		Scanned = 3,
		StayStill = 4,
		StandUp = 5
	}

	private enum ScannerScreens
	{
		SystemNotOperational = 0,
		ScanAPatient = 1,
		StartManualScan = 2,
		Scanning = 3,
		ScanningComplete = 4,
		WaitForMatching = 5,
		MatchingInProgress = 6,
		BacteriaNotMatching = 7,
		CheckingMatch = 8,
		Completed = 9,
		MissingSubject = 10,
		Checking = 11
	}

	private enum MedicineScreens
	{
		InsertDiseaseSample = 0,
		PuzzleScreen = 1,
		SubstanceCreated = 2,
		DispensingCure = 3,
		CureDispensed = 4,
		Checking = 5,
		BacteriaOverlay = 6,
		IncorrectConnections = 7
	}

	private enum AirlockScreens
	{
		StartScan = 0,
		Scanning = 1,
		InfectionDetected = 2,
		AccessGranted = 3
	}

	private enum AirlockKeypadScreen
	{
		None = -1,
		Numbers = 0,
		Checking = 1,
		Error = 2,
		Solved = 3
	}

	private enum RfidScreen
	{
		None = -1,
		Scanning = 0,
		Numbers = 1,
		Splicer = 2
	}

	public class SplicedLogState
	{
		public string word;

		public int logIndex;

		public float logWidth;

		public int splicedLogIndex;

		public float startTime;

		public float endTime;

		public Switch3D button;

		public float duration => endTime - startTime;
	}

	public class CutMeshParameter
	{
		public int splicedLogIndex;

		public int middleLogIndex;

		public float rightCutValue;

		public float leftCutValue;

		public float nextLogScale;
	}

	public class CratePuzzleSegment
	{
		public GameObject segment;

		public Transform segmentTransform;

		public int row;

		public int column;

		public float rotation;

		public Vector3 startingLocalPosition;

		public Quaternion startingLocalRotation;

		public int startingRow;

		public int startingColumn;
	}

	public class CratePuzzleInstruction
	{
		public SwapperPiece piece;

		public SwapperPiece swappedEmpty;

		public int row;

		public int column;

		public int move;

		public int rotate;
	}

	private enum WaveformScreens
	{
		InsertKey = 0,
		WaitingForSlicer = 1,
		Listening = 2,
		AccessGranted = 3,
		AccessDenied = 4,
		Loading = 5
	}

	public enum HologramScreen
	{
		OnOff = 0,
		Checking = 1,
		Error = 2,
		Solved = 3
	}

	private enum Puzzle
	{
		[PuzzleInfo("%PuzzleS1_1%", true)]
		Pods = 0,
		[PuzzleInfo("%PuzzleS1_2%", false)]
		LockerPrint = 1,
		[PuzzleInfo("%PuzzleS1_3%", false)]
		LockerNote = 2,
		[PuzzleInfo("%PuzzleS1_4%", false)]
		UtilityCloset = 3,
		[PuzzleInfo("%PuzzleS1_5%", false)]
		BrokenKeypad = 4,
		[PuzzleInfo("%PuzzleS1_6%", true)]
		MedBed = 5,
		[PuzzleInfo("%PuzzleS1_7%", false)]
		BacteriaMatch = 6,
		[PuzzleInfo("%PuzzleS1_8%", true)]
		TheCure = 7,
		[PuzzleInfo("%PuzzleS1_9%", false)]
		Holograms = 8,
		[PuzzleInfo("%PuzzleS1_10%", false)]
		Crates = 9,
		[PuzzleInfo("%PuzzleS1_11%", false)]
		LockerPad = 10,
		[PuzzleInfo("%PuzzleS1_12%", false)]
		CaptainsPod = 11,
		[PuzzleInfo("%PuzzleS1_13%", false)]
		RFID = 12,
		[PuzzleInfo("%PuzzleS1_14%", false)]
		SoundSplicing = 13
	}

	private enum PodsHint
	{
		PressMed = 0,
		LookAtPlanetName = 1,
		LookAtShipName = 2
	}

	private enum LockerPrintHint
	{
		TurnHintOn = 0,
		GetPrint = 1,
		MatchPrintToKeypad = 2,
		Solve = 3
	}

	private enum LockerNoteHint
	{
		GetHint = 0,
		MatchHintToKeypad = 1,
		MatchPattern = 2,
		Solve = 3
	}

	private enum UtilityClosetHint
	{
		GetTablet = 0,
		LookAtSwapper = 1,
		MatchTabletToSwapper = 2,
		Solve = 3
	}

	private enum BrokenKeypadHint
	{
		GetScrewdriver = 0,
		WipeGlass = 1,
		ReadNumber = 2,
		LookAtKeypad = 3,
		UnscrewBolts = 4,
		SolveFirst = 5,
		SolveOthers = 6
	}

	private enum MedBedHint
	{
		GetKeyCard = 0,
		GetLocker1Battery = 1,
		GetLocker2Battery = 2,
		GetUtiliyClosetBattery = 3,
		PlaceCard = 4,
		PlaceBatteries = 5,
		GetTriangleComponent = 6,
		GetCircleAndSquareComponent = 7,
		LookAtComponentSigns = 8,
		Solve = 9
	}

	private enum BacteriaMatchHint
	{
		LayInBed = 0,
		ClickManualScan = 1,
		ScanHolo = 2,
		GetBacterias = 3,
		MatchBacterias = 4,
		Solve = 5
	}

	private enum TheCureHint
	{
		GetSample = 0,
		GetPaper = 1,
		PlaceSample = 2,
		MatchPaperToScreen = 3,
		SolveFirst = 4,
		ReleaseCure = 5
	}

	private enum HologramsHint
	{
		EnableHolos = 0,
		GetTablet = 1,
		Holo1Answer = 2,
		Holo2Answer = 3,
		Holo3Answer = 4,
		Holo4Answer = 5
	}

	private enum CratesHint
	{
		LookAtCrateScreen = 0,
		LookAtEmpties = 1,
		Solve1 = 2,
		Solve2 = 3,
		Solve3 = 4,
		Solve4 = 5
	}

	private enum LockerPadHint
	{
		GetTablet = 0,
		MatchTabletToKeypad = 1,
		LookAtTablet = 2,
		Solve = 3
	}

	private enum CaptainsPodHint
	{
		LookAtScreen = 0,
		LookAtButtons = 1,
		GetNumber = 2,
		Solve = 3
	}

	private enum RFIDHint
	{
		GetAudioDevice = 0,
		Scan1 = 1,
		Scan2 = 2,
		Scan3 = 3,
		Scan4 = 4
	}

	private enum SoundSplicingHint
	{
		GetCaptainsNote = 0,
		LookAtCode = 1,
		SpliceCode = 2,
		GetKeycard = 3,
		PlaceKeycard = 4,
		Solve = 5
	}

	private enum LevelPredicate
	{
		Pod0Solved = 0,
		Pod1Solved = 1,
		Pod2Solved = 2,
		Pod3Solved = 3,
		Locker0Solved = 4,
		Locker1Solved = 5,
		Locker2Solved = 6,
		ScanPosition0 = 7,
		ScanPosition1 = 8,
		ScanPosition2 = 9,
		CrateSolved = 10,
		CaptainsPodSolved = 11,
		SplicerSolved = 12,
		CanMedBedScan = 13,
		MedBedBatterisSolved = 14
	}

	private enum RPCs
	{
		AirlockKeypadCorrect = 0,
		AirlockKeypadWrong = 1,
		BacteriaMatchingCorrect = 2,
		BacteriaMatchingWrong = 3,
		MedicineSolved = 4,
		MedicineNotSolved = 5,
		HasTentacles = 6,
		NoTentacles = 7,
		SolveHologram = 8,
		NotSolveHologram = 9,
		AudioAnalyzerSolved = 10,
		AudioAnalyzerNotSolved = 11,
		SolvedMedicineBeforeScanning = 12,
		NotSolvedMedicineBeforeScanning = 13,
		MedBedComponentsSolved = 14,
		MedBedComponentsNotSolved = 15
	}

	[DontSave]
	public string shipName;

	[DontSave]
	public List<DialogQuestion> dialogQuestions;

	[DontSave]
	private int debug_currentPod;

	[DontSave]
	private bool debug_enterPose;

	[DontSave]
	private List<ParticleSystem> podMedicineParticleChildren;

	[DontSave]
	private List<ParticleSystem> medicineParticleChildren;

	private PodScreens podCurrentScreen;

	private int[] currentQuestions = new int[4];

	private string[] podCurrentShipNames = new string[4] { "", "", "", "" };

	private bool exitedPod;

	private bool[] podsInitiated = new bool[4];

	private bool podsPuzzleSolved;

	public List<SpawnPointToPose> podSpawnPointsToPose;

	private List<TriangleKeypad> lockers = new List<TriangleKeypad>();

	[DontSave]
	public RefArray<GameObject, TweenState, SwapperPiece> chipSwapperPieces;

	private bool isMedBedKeySlotOn;

	private ComponentScreens currentComponentScreen;

	private bool sampleProvided;

	private int batteriesAdded;

	private int bacteriaCount;

	private float medScannerTimer;

	private int lastScannedPoint = -1;

	private bool foundBacterias;

	private ScannerScreens currentScannerScreen;

	private bool scannerNeedsAPerson;

	private bool isPersonInScanner;

	private bool enableMedbedHologram;

	private bool convertedToSp;

	private bool holoSpEnabled;

	private bool canScanMedBed;

	private bool solvedComponents;

	[DontSave]
	private List<int> medBedHoloBacteriaSolution = new List<int> { 0, 8, 15 };

	private List<int> medBedHoloBacteriaCurrent = new List<int> { -1, -1, -1 };

	public List<int> medScannerSolutionPoints;

	[DontSave]
	public float medScannerMaxTime;

	[DontSave]
	public EventInstance scanInstance;

	[DontSave]
	private Dictionary<char, List<int>> medicineChart = new Dictionary<char, List<int>>
	{
		{
			'A',
			new List<int> { 3 }
		},
		{
			'C',
			new List<int> { 0 }
		},
		{
			'D',
			new List<int> { 0, 2 }
		},
		{
			'E',
			new List<int> { 2 }
		},
		{
			'G',
			new List<int> { 1 }
		},
		{
			'H',
			new List<int> { 1, 3 }
		}
	};

	private int[] medicineTentaclesLeft = new int[3] { 2, 1, 2 };

	private bool releasedMedicine;

	private bool solvedMedicineBeforeScanning;

	[DontSave]
	private List<int> airlockProgramming1 = new List<int>
	{
		0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
		-10, -20
	};

	[DontSave]
	private List<int> airlockProgramming2 = new List<int>
	{
		-1, 7, 8, 6, 1, 2, -1, 1, 3, 6,
		-1, -1
	};

	[DontSave]
	private List<int> airlockKeypadSolution = new List<int> { 4, 7, 3, 6, 8 };

	private List<int> airlockKeypadCurrentSolution = new List<int>();

	private List<int> scrambleCurrentImages;

	private readonly List<int> captainsSolution = new List<int> { 9, 3, 0 };

	private List<int> currentCaptains = new List<int> { 0, 0, 0 };

	[DontSave]
	private EventInstance splicerLogAudioInstance;

	[DontSave]
	private string[] splicerLogAudioPaths = new string[4] { "event:/Sound Effects/05 Misc/Voices/Human/S1 Captain/Who_knew_algebra", "event:/Sound Effects/05 Misc/Voices/Human/S1 Captain/The_tech_crew_came_over", "event:/Sound Effects/05 Misc/Voices/Human/S1 Captain/I_need_a_four_word", "event:/Sound Effects/05 Misc/Voices/Human/S1 Captain/It_fills_me_with_pride" };

	private List<Slot> splicerAvailableLogs = new List<Slot>();

	private int splicerCurrentMiddleLog = -1;

	private bool playLog;

	[DontSave]
	private List<SplicedLogState> splicedAudioSolution = new List<SplicedLogState>();

	private List<SplicedLogState> splicedAudio = new List<SplicedLogState>();

	[DontSave]
	private float splicedAudioPadding = 0.3f;

	private bool isSplicerBottomFull;

	[DontSave]
	private Mesh[] splicerMeshes;

	private bool splicerAddedToInventory;

	private float splicerCurrentTimeInRange;

	private bool isSplicerCorrect;

	private bool isSplicerSolved;

	private Vector3 splicerLastCutLogEndPosition;

	private bool isSplicerMiddlePlaying;

	private Vector3 splicerBottomPlayTrackerStartingPosition;

	private bool debugSolvedSplicer;

	[DontSave]
	private const float splicerAudioLength = 3.5f;

	[DontSave]
	public Ref<Text, TweenState> splicerCount;

	[DontSave]
	public Text splicerMax;

	private List<CutMeshParameter> cutMeshParameters = new List<CutMeshParameter>();

	private List<CratePuzzleSegment> cratePuzzleSegments;

	private List<CratePuzzleInstruction> currentCratePuzzleInstructions;

	private bool isCrateSolved;

	private SwapperPiece crateFirstSelected;

	[DontSave]
	public Switch3D hologramCheckScheduleButton;

	[DontSave]
	public Ref<Switch3D, GameObject> hologramOnButton;

	[DontSave]
	public GameObject hologramPeople;

	[DontSave]
	public Ref<MaterialState, GameObject> hologramErrorTextMS;

	[DontSave]
	public RefArray<GameObject, Transform> holograms;

	[DontSave]
	public MaterialState[] hologramMaterialStates;

	[DontSave]
	public Mesh[] hologramMeshes;

	[DontSave]
	public MeshFilter[] hologramMeshFilters;

	[DontSave]
	public Switch3D[] hologramNextButtons;

	[DontSave]
	public Switch3D[] hologramPreviousButtons;

	[DontSave]
	public GameObject hologramsSolvedText;

	[DontSave]
	public List<float> droneSpeeds;

	[DontSave]
	private List<Transform> hoverDroneParents;

	private int holoDronesArrived;

	private int[] hologramStates = new int[8];

	private readonly int[] hologramSolution = new int[8] { 4, 0, 0, 0, 1, 3, 0, 2 };

	[DontSave]
	public List<Item> tablets;

	[DontSave]
	public List<Slidable> tabletLockSlidables;

	[DontSave]
	public RefArray<TweenState, GameObject> tabletLockScreens;

	[DontSave]
	public MaterialState[] tabletScreens;

	[DontSave]
	private readonly string loadingSoundEvent = "event:/Sound Effects/03 Interactable/Buttons & Switches/Digital_Button_04";

	[DontSave]
	private EventInstance[] loadingSoundInstances = new EventInstance[4];

	private int loadingSoundInstanceIndex;

	private bool[] podsOpenedFirstTime = new bool[4];

	[DontSave]
	public Ref<GameObject, Switch3D> levelExitRef;

	[Header("Generated Variables")]
	[DontSave]
	public MaterialState[] MedHoloMaterialStates;

	[DontSave]
	public GameObject[] splicerTabTextHighlights;

	[DontSave]
	public GameObject[] splicerTabButtonHighlights;

	[DontSave]
	public GameObject podRoomObstacle;

	[DontSave]
	public Renderer[] medicineSwapperEmptyHighlights;

	[DontSave]
	public Renderer[] medicineSwapperPiecesHighlights;

	[DontSave]
	public Renderer[] medicineSwapperPiecesHoverHighlights;

	[DontSave]
	public SwapperPiece[] medicineSwapperPieces;

	[DontSave]
	public Transform podTarinHint;

	[DontSave]
	public Transform mpPositionPodHint;

	[DontSave]
	public Text[] pod4AnswerTexts3;

	[DontSave]
	public Text[] pod3AnswerTexts3;

	[DontSave]
	public Text[] pod4AnswerTexts2;

	[DontSave]
	public Text[] pod3AnswerTexts2;

	[DontSave]
	public Text[] pod4AnswerTexts1;

	[DontSave]
	public Text[] pod3AnswerTexts1;

	[DontSave]
	public RefArray<Switch3D, TweenState, GameObject> pod4AnswerButtons3;

	[DontSave]
	public RefArray<Switch3D, TweenState, GameObject> pod3AnswerButtons3;

	[DontSave]
	public RefArray<Switch3D, TweenState, GameObject> pod4AnswerButtons2;

	[DontSave]
	public RefArray<Switch3D, TweenState, GameObject> pod3AnswerButtons2;

	[DontSave]
	public RefArray<Switch3D, TweenState, GameObject> pod4AnswerButtons1;

	[DontSave]
	public RefArray<Switch3D, TweenState, GameObject> pod3AnswerButtons1;

	[DontSave]
	public GameObject[] podScreens3;

	[DontSave]
	public GameObject[] podScreens2;

	[DontSave]
	public GameObject[] podScreens1;

	[DontSave]
	public RefArray<Switch3D, GameObject> medBedBacteriaSphereButtons;

	[DontSave]
	public TweenState[] medBedBacteriaTweens;

	[DontSave]
	public MaterialState[] medBedSmallSphereMSs;

	private GameObject bacteriaZoomToEnter;

	[DontSave]
	public Transform debugPoint;

	[DontSave]
	public Item printedTablet;

	[DontSave]
	public GameObject airlockAccessDeniedMpOverlay;

	[DontSave]
	public Ref<GameObject, Trigger> airlockTrigger;

	[DontSave]
	public AnimationSampler listeningDeviceCapAnimation;

	[DontSave]
	public Slot splicerExitSlot;

	[DontSave]
	public Sequence[] medbedGateAnimations;

	[DontSave]
	public Sequence[] rfidScanningSequences;

	[DontSave]
	public GameObject[] rfidScreens;

	[DontSave]
	public RefArray<Switch3D, MaterialState, GameObject> wallHintButtons;

	[DontSave]
	public GameObject lockerPrintHint;

	[DontSave]
	public GameObject[] wallHintHolos;

	[DontSave]
	public Zoomable crateZoom;

	[DontSave]
	public TweenState crateLid;

	[DontSave]
	public GameObject[] medicineTentacles;

	[DontSave]
	public GameObject[] medicineRedTentacles;

	[DontSave]
	public GameObject turningOffText;

	[DontSave]
	public GameObject loadingHoloText;

	[DontSave]
	public GameObject[] podIncorrectOverlay;

	[DontSave]
	public GameObject[] deimosPlayerHints;

	[DontSave]
	public TweenState[] podPreliminaryScans;

	[DontSave]
	public Transform[] splicedLogsBoundaries;

	[DontSave]
	public RefArray<Transform, MeshFilter, Switch3D, BoxCollider> splicerSplicedLogs;

	[DontSave]
	public Transform[] splicerSliderBoundaries;

	[DontSave]
	public Transform[] splicerBottomBoundaries;

	[DontSave]
	public Ref<GameObject, MeshFilter, MaterialState> splicerDebugQuad;

	[DontSave]
	public RefArray<MeshFilter, GameObject> splicerAudioLogs;

	[DontSave]
	public MaterialState[] airlockUnderlines;

	[DontSave]
	public GameObject[] airlockScreens;

	[DontSave]
	public GameObject[] airlockNumbers4;

	[DontSave]
	public GameObject[] airlockNumbers3;

	[DontSave]
	public GameObject[] airlockNumbers2;

	[DontSave]
	public GameObject[] airlockNumbers1;

	[DontSave]
	public GameObject[] airlockNumbers0;

	[DontSave]
	public SwapperPiece[] medicineSolution;

	[DontSave]
	public SwapperPiece[] medicineEmptyPieces;

	[DontSave]
	public Swapper medicineSwapper;

	[DontSave]
	public RefArray<Switch3D, TweenState, GameObject> pod4AnswerButtons;

	[DontSave]
	public RefArray<Switch3D, TweenState, GameObject> pod3AnswerButtons;

	[DontSave]
	public Text[] pod4AnswerTexts;

	[DontSave]
	public Text[] pod3AnswerTexts;

	[DontSave]
	public GameObject[] podScreens;

	[DontSave]
	public GameObject desinfectionParticles;

	[DontSave]
	public GameObject medbedHoloBeam;

	[DontSave]
	public TweenState medbedHeadScreenTween;

	[DontSave]
	public RefArray<GameObject, Transform> hoveringDrones;

	[DontSave]
	public RefArray<TweenState, GameObject> droneAnimations;

	[DontSave]
	public GameObject corridorScanField;

	[DontSave]
	public TweenState corridorLoadingTween;

	[DontSave]
	public GameObject[] chipScreens;

	[DontSave]
	public GameObject[] lockerScreens;

	[DontSave]
	public GameObject[] hologramScreens;

	[DontSave]
	public GameObject scannerPlane;

	[DontSave]
	public TweenState bedScanningTween;

	[DontSave]
	public GameObject[] crateScreens;

	[DontSave]
	public Ref<Item, TweenState, Transform> crateItem;

	[DontSave]
	public TweenState captainsScreenAbove;

	[DontSave]
	public GameObject[] captainsScreens;

	[DontSave]
	public Zoomable captainsZoom;

	[DontSave]
	public GameObject[] componentXs;

	[DontSave]
	public Zoomable[] scannerZooms;

	[DontSave]
	public AnimationSampler holoSolvedAnimation;

	[DontSave]
	public AnimationSampler tabletPrintingAnimation;

	[DontSave]
	public TweenState dispensingCureTween;

	[DontSave]
	public TweenState[] crateSwapsTweens;

	[DontSave]
	public TweenState[] crateBordersTweens;

	[DontSave]
	public RefArray<TweenState> blinkingUITweens;

	[DontSave]
	public RefArray<Switch3D> blinkingUIButtons;

	[DontSave]
	public GameObject[] scannerBacteriaIcons2;

	[DontSave]
	public TweenState[] medBedCheckMatchTweens;

	[DontSave]
	public GameObject[] scannerBacteriaIcons;

	[DontSave]
	public GameObject[] scannerNotOperationals;

	[DontSave]
	public GameObject[] scannerScanPatients;

	[DontSave]
	public GameObject[] scannerScreenObjects;

	[DontSave]
	public Zoomable scramblerZoom;

	[DontSave]
	public RefArray<MeshFilter> scramblerPatternMeshes;

	[DontSave]
	public RefArray<MeshFilter, GameObject, Transform> ScramblerPattern4Collection;

	[DontSave]
	public RefArray<MeshFilter, GameObject, Transform> ScramblerPattern3Collection;

	[DontSave]
	public RefArray<MeshFilter, GameObject, Transform> ScramblerPattern2Collection;

	[DontSave]
	public RefArray<MeshFilter, GameObject, Transform> ScramblerPattern1Collection;

	[DontSave]
	public RefArray<MeshFilter, GameObject, Transform> ScramblerPattern0Collection;

	[DontSave]
	public Switch3D[] scramblerPatternButtons;

	[DontSave]
	public GameObject[] componentCounter;

	[DontSave]
	public GameObject[] componentBatteryCounters;

	[DontSave]
	public Transform[] medBedRotatorColliderPositionsOther;

	[DontSave]
	public RefArray<Zoomable, GameObject> podBacteriaOtherZooms;

	[DontSave]
	public MaterialState[] podFirstAidMaterialStates;

	[DontSave]
	public GameObject[] airlockTexts;

	[DontSave]
	public Transform[] crateSegmentSolutions;

	[DontSave]
	public GameObject splicerSplicingScreen;

	[DontSave]
	public Text splicerLogCount;

	[DontSave]
	public RefArray<AnimationSampler, MaterialState, GameObject> rfidSlotAnimations;

	[DontSave]
	public StudioEventEmitter[] rfidEmitters;

	[DontSave]
	public TweenState splicerAntenaTween;

	[DontSave]
	public Switch3D[] captainsPodButtons;

	[DontSave]
	public TweenState cardReaderDoor;

	[DontSave]
	public MaterialState cardReaderMaterialState;

	[DontSave]
	public MaterialState[] corridorLightsMaterialStates;

	[DontSave]
	public Light[] corridorLights;

	[DontSave]
	public Item medicineSample;

	[DontSave]
	public Sequence medBedSampleAnimation;

	[DontSave]
	public Sequence medBedMixerAnimation;

	[DontSave]
	public Slot[] componentSlots;

	[DontSave]
	public TweenState medBedBatteryTween;

	[DontSave]
	public MaterialState medBedKeyBlinker;

	[DontSave]
	public GameObject[] crateSegmentObjects;

	[DontSave]
	public Swapper crateSwapper;

	[DontSave]
	public Switch3D crateUndo;

	[DontSave]
	public GameObject[] crateSlotBorders;

	[DontSave]
	public SwapperPiece[] crateSwaps;

	[DontSave]
	public SwapperPiece[] crateEmptySwaps;

	[DontSave]
	public Light tempCaptainsLight;

	[DontSave]
	public Ref<Slot, TweenState, GameObject> exitSlot;

	[DontSave]
	public Item crateOverrideKey;

	[DontSave]
	public Switch3D medicineCreateSubstanceButton;

	[DontSave]
	public Switch3D medicineReleaseCureButton;

	[DontSave]
	public Switch3D[] medBedCheckMatchButtons;

	[DontSave]
	public Switch3D medBedConfirmComponentsButton;

	[DontSave]
	public Trigger[] wrongComponentsTriggers;

	[DontSave]
	public Switch3D[] medBedStartMatchingButton;

	[DontSave]
	public Switch3D[] medBedStartScanButton;

	[DontSave]
	public Rotatable[] medBedRotatables;

	[DontSave]
	public Transform[] medBedRotatorColliderPositions;

	[DontSave]
	public Switch3D[] medBedHoloBacterias;

	[DontSave]
	public RefArray<Zoomable, GameObject, Transform> podbacteriaZooms;

	[DontSave]
	public GameObject[] waveformScreens;

	[DontSave]
	public GameObject splicerBottomEndPiece;

	[DontSave]
	public Ref<GameObject, Transform> splicerBottomPlayTracker;

	[DontSave]
	public Ref<GameObject, Transform> splicerMiddlePlayTracker;

	[DontSave]
	public GameObject[] holoPoints;

	[DontSave]
	public Sequence corridorScanFieldSequence;

	[DontSave]
	public Switch3D corridorScanButton;

	[DontSave]
	public GameObject captainsRoomObstacle;

	[DontSave]
	public GameObject corridorObstacle;

	[DontSave]
	public TweenState[] podAdministeringTweens;

	[DontSave]
	public RefArray<ParticleSystem, GameObject> podMedicineParticles;

	[DontSave]
	public Slot[] medBedBatterySlots;

	[DontSave]
	public Swapper chipSwapper;

	[DontSave]
	public Switch3D chipCabinetDoor;

	[DontSave]
	public Zoomable medicineZoom;

	[DontSave]
	public Item screwdriver;

	[DontSave]
	public RefArray<TweenState, Transform> screwTweens;

	[DontSave]
	public Item[] screws;

	[DontSave]
	public RefArray<Slot, TweenState, GameObject> screwSlots;

	[DontSave]
	public TweenState exitDoor;

	[DontSave]
	public GameObject captainsNote;

	[DontSave]
	public Item audioSplicer;

	[DontSave]
	public GameObject audioAnalyzer;

	[DontSave]
	public Slot[] splicerRfidSlots;

	[DontSave]
	public Ref<GameObject, Switch3D, MaterialState, TweenState> splicerMiddleCut;

	[DontSave]
	public Ref<GameObject, Switch3D> splicerMiddleDelete;

	[DontSave]
	public Slidable[] splicerSlidables;

	[DontSave]
	public Switch3D[] splicerTabs;

	[DontSave]
	public Switch3D captainsPod;

	[DontSave]
	public ParticleSystem captainsPodDust;

	[DontSave]
	public Switch3D[] airLockDoors;

	[DontSave]
	public ParticleSystem[] airLockDoorDust;

	[DontSave]
	public Zoomable airlockKeypadZoom;

	[DontSave]
	public GameObject airlockGlassPaintable;

	[DontSave]
	public Switch3D[] airlockKeypadButtons;

	[DontSave]
	public Switch3D[] airlockKeypadSwitches;

	[DontSave]
	public GameObject bacteriaMedicineParticles;

	[DontSave]
	public Switch3D airlockKeypadClosure;

	[DontSave]
	public TweenState medicineSampleTween;

	[DontSave]
	public Slot sampleSlot;

	[DontSave]
	public GameObject[] medicineScreens;

	[DontSave]
	public TweenState[] medScannerTweens;

	[DontSave]
	public Slidable scannerSlidable;

	[DontSave]
	public Text[] scannerCounters;

	[DontSave]
	public Switch3D makeHoloButton;

	[DontSave]
	public GameObject[] bedScreens;

	[DontSave]
	public CharacterPose medBedPose;

	[DontSave]
	public GameObject hologram;

	[DontSave]
	public Zoomable chipScreen;

	[DontSave]
	public TweenState[] componentSlotTweens;

	[DontSave]
	public RefArray<GameObject> componentStateScreens;

	[DontSave]
	public Slot medBedOverrideSlot;

	[DontSave]
	public TweenState medBedKeyTween;

	[DontSave]
	public Zoomable[] lockerZooms;

	[DontSave]
	public Item[] locker2Items;

	[DontSave]
	public Item[] locker1Items;

	[DontSave]
	public Item[] locker0Items;

	[DontSave]
	public Switch3D[] locker2Buttons;

	[DontSave]
	public Switch3D[] locker1Buttons;

	[DontSave]
	public Switch3D[] locker0Buttons;

	[DontSave]
	public Switch3D[] lockerDoors;

	[DontSave]
	public Switch3D[] podMedButtons;

	[DontSave]
	public CharacterPose[] podPoses;

	[DontSave]
	public Switch3D[] podSwitches;

	[DontSave]
	public ParticleSystem[] podSwitchDusts;

	[DontSave]
	public RefArray<GameObject, Item, Transform, TweenState> food;

	[DontSave]
	public ParticleSystem foodVFXInventory;

	[DontSave]
	public Transform foodVFXTransformInventory;

	[DontSave]
	public Switch3D comicBookSwitch;

	public void inputNumber(TriangleKeypad locker, int input)
	{
		locker.current[locker.currentIndex] = input;
		locker.count++;
		string text = "";
		foreach (int item in locker.current)
		{
			text += item;
		}
		Debug.Log(text);
	}

	public void onKeypadButton(int lockerIndex, int button)
	{
		inputNumber(lockers[lockerIndex], button);
	}

	public bool checkSolution(int index)
	{
		TriangleKeypad triangleKeypad = lockers[index];
		if (triangleKeypad.count < triangleKeypad.solution.Count)
		{
			return false;
		}
		for (int i = 0; i < triangleKeypad.current.Count; i++)
		{
			if (triangleKeypad.solution[i] != triangleKeypad.current[(triangleKeypad.count + i) % triangleKeypad.solution.Count])
			{
				return false;
			}
		}
		return true;
	}

	public void solveLocker(int index)
	{
		TriangleKeypad triangleKeypad = lockers[index];
		Switch3D[] buttons = triangleKeypad.buttons;
		for (int i = 0; i < buttons.Length; i++)
		{
			buttons[i].targetable = false;
		}
		Item[] items = triangleKeypad.items;
		for (int i = 0; i < items.Length; i++)
		{
			items[i].targetable = true;
		}
		triangleKeypad.solved = true;
		game.startTimer(new LockerTimer(triangleKeypad.door, 1), 0.1f);
		switch (index)
		{
		case 0:
			game.finishPuzzle(Puzzle.LockerPrint);
			break;
		case 1:
			game.finishPuzzle(Puzzle.LockerNote);
			break;
		case 2:
			game.finishPuzzle(Puzzle.LockerPad);
			break;
		}
	}

	public void onLockerTimer(TriangleKeypad locker, int index)
	{
		switch (index)
		{
		case 1:
			locker.zoom.targetable = false;
			changeScreen(locker, index);
			game.startTimer(new LockerTimer(locker.door, 2), 1.7f);
			break;
		case 2:
			changeScreen(locker, index);
			game.startTimer(new LockerTimer(locker.door, 3), 1f);
			break;
		case 3:
			game.increaseZoomCounter(locker.zoom.gameObject);
			game.startSwitch(locker.door);
			locker.door.targetable = true;
			break;
		}
	}

	private void changeScreen(TriangleKeypad locker, int screen)
	{
		for (int i = 0; i < locker.screens.Count; i++)
		{
			locker.screens[i].SetActive(i == screen);
			Debug.Log("SCREEN: " + locker.screens[i].name + " - " + (i == screen));
		}
	}

	public override void onInitHints()
	{
		game.setPuzzleType(Puzzle.Pods, Game.Puzzle.Type.CoopAsk);
		game.setPuzzleConditions(Puzzle.LockerPrint, default(Puzzle));
		game.setPuzzleConditions(Puzzle.LockerNote, default(Puzzle));
		game.setPuzzleConditions(Puzzle.UtilityCloset, default(Puzzle));
		game.setPuzzleConditions(Puzzle.BrokenKeypad, Puzzle.UtilityCloset);
		game.setPuzzleConditions(Puzzle.MedBed, Puzzle.BrokenKeypad, Puzzle.LockerPrint, Puzzle.LockerNote);
		game.setPuzzleConditions(Puzzle.BacteriaMatch, Puzzle.MedBed);
		game.setPuzzleConditions(Puzzle.TheCure, Puzzle.BacteriaMatch);
		game.setPuzzleConditions(Puzzle.Holograms, Puzzle.TheCure);
		game.setPuzzleConditions(Puzzle.Crates, Puzzle.TheCure);
		game.setPuzzleConditions(Puzzle.LockerPad, Puzzle.TheCure);
		game.setPuzzleConditions(Puzzle.CaptainsPod, Puzzle.TheCure);
		game.setPuzzleConditions(Puzzle.RFID, Puzzle.Crates, Puzzle.LockerPad);
		game.setPuzzleConditions(Puzzle.SoundSplicing, Puzzle.RFID, Puzzle.CaptainsPod, Puzzle.Holograms);
		game.setRelevantObjectsForPuzzle(Puzzle.Pods, Array.ConvertAll(podMedButtons, (Switch3D b) => b.gameObject));
		game.setRelevantObjectsForPuzzle(Puzzle.LockerPrint, lockerPrintHint, wallHintButtons[0].Get<GameObject>(0u));
		game.setRelevantObjectsForPuzzle(Puzzle.LockerPrint, Array.ConvertAll(locker0Buttons, (Switch3D b) => b.gameObject));
		game.setRelevantObjectsForPuzzle(Puzzle.LockerNote, podTarinHint.gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.LockerNote, Array.ConvertAll(locker1Buttons, (Switch3D b) => b.gameObject));
		game.setRelevantObjectsForPuzzle(Puzzle.UtilityCloset, tablets[0].gameObject, chipScreen.gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.BrokenKeypad, screwdriver.gameObject, airlockKeypadZoom.gameObject, airlockGlassPaintable);
		game.setRelevantObjectsForPuzzle(Puzzle.MedBed, locker0Items[0].gameObject, medBedOverrideSlot.gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.MedBed, Array.ConvertAll(medBedBatterySlots, (Slot s) => s.gameObject));
		game.setRelevantObjectsForPuzzle(Puzzle.MedBed, Array.ConvertAll(medBedBatterySlots[0].acceptItems, (Item i) => i.gameObject));
		game.setRelevantObjectsForPuzzle(Puzzle.MedBed, Array.ConvertAll(componentSlots, (Slot s) => s.gameObject));
		game.setRelevantObjectsForPuzzle(Puzzle.MedBed, Array.ConvertAll(componentSlots, (Slot s) => s.acceptItems[0].gameObject));
		game.setRelevantObjectsForPuzzle(Puzzle.BacteriaMatch, scannerSlidable.gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.BacteriaMatch, podbacteriaZooms.ToArray<GameObject>(0f));
		game.setRelevantObjectsForPuzzle(Puzzle.BacteriaMatch, podBacteriaOtherZooms.ToArray<GameObject>(0f));
		game.setRelevantObjectsForPuzzle(Puzzle.BacteriaMatch, Array.ConvertAll(scannerZooms, (Zoomable b) => b.gameObject));
		game.setRelevantObjectsForPuzzle(Puzzle.BacteriaMatch, Array.ConvertAll(medBedStartScanButton, (Switch3D b) => b.gameObject));
		game.setRelevantObjectsForPuzzle(Puzzle.BacteriaMatch, Array.ConvertAll(medBedStartMatchingButton, (Switch3D b) => b.gameObject));
		game.setRelevantObjectsForPuzzle(Puzzle.BacteriaMatch, Array.ConvertAll(medBedCheckMatchButtons, (Switch3D b) => b.gameObject));
		game.setRelevantObjectsForPuzzle(Puzzle.BacteriaMatch, Array.ConvertAll(medBedHoloBacterias, (Switch3D b) => b.gameObject));
		game.setRelevantObjectsForPuzzle(Puzzle.TheCure, sampleSlot.gameObject, medicineSample.gameObject, locker1Items[0].gameObject, medicineZoom.gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.Holograms, hologramOnButton.Get<GameObject>(0f), hologramCheckScheduleButton.gameObject, printedTablet.gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.Holograms, Array.ConvertAll(hologramNextButtons, (Switch3D b) => b.gameObject));
		game.setRelevantObjectsForPuzzle(Puzzle.Holograms, Array.ConvertAll(hologramPreviousButtons, (Switch3D b) => b.gameObject));
		game.setRelevantObjectsForPuzzle(Puzzle.Crates, crateZoom.gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.LockerPad, tablets[1].gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.LockerPad, Array.ConvertAll(locker2Buttons, (Switch3D b) => b.gameObject));
		game.setRelevantObjectsForPuzzle(Puzzle.CaptainsPod, scramblerZoom.gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.CaptainsPod, Array.ConvertAll(captainsPodButtons, (Switch3D b) => b.gameObject));
		game.setRelevantObjectsForPuzzle(Puzzle.RFID, audioSplicer.gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.RFID, Array.ConvertAll(splicerRfidSlots, (Slot s) => s.gameObject));
		game.setRelevantObjectsForPuzzle(Puzzle.SoundSplicing, audioSplicer.gameObject, captainsNote, crateOverrideKey.gameObject, exitSlot.Get<GameObject>(0u));
		game.setHintCondition(Puzzle.Pods, PodsHint.PressMed, delegate
		{
			int index = UnityUtils.getIndex(podPoses, game.localPlayerData.characterPoseContext.pose);
			return index >= 0 && podsInitiated[index] && podMedButtons[index].state == Switch3DState.On;
		});
		game.setHintCondition(Puzzle.Pods, PodsHint.LookAtPlanetName, delegate
		{
			int index = UnityUtils.getIndex(podPoses, game.localPlayerData.characterPoseContext.pose);
			return index >= 0 && podsInitiated[index] && currentQuestions[index] > 2;
		});
		game.setHintCondition(Puzzle.LockerPrint, LockerPrintHint.TurnHintOn, () => wallHintButtons[0].Get<Switch3D>(0).state == Switch3DState.On);
		game.setHintCondition(Puzzle.LockerPrint, LockerPrintHint.GetPrint, () => game.wasAddedToInventoryDuringCurrentPuzzle(lockerPrintHint));
		game.setHintCondition(Puzzle.LockerNote, LockerNoteHint.GetHint, () => game.wasAddedToInventoryDuringCurrentPuzzle(podTarinHint.gameObject));
		game.setHintCondition(Puzzle.UtilityCloset, UtilityClosetHint.GetTablet, () => !tabletLockScreens[0].Get<GameObject>(0f).activeSelf);
		game.setHintCondition(Puzzle.UtilityCloset, UtilityClosetHint.LookAtSwapper, () => game.wasLookedAtCurrentPuzzle(chipScreen.gameObject));
		game.setHintCondition(Puzzle.BrokenKeypad, BrokenKeypadHint.GetScrewdriver, () => (!screwSlots[0].Get<GameObject>(0u).activeSelf && !screwSlots[1].Get<GameObject>(0u).activeSelf) || game.wasAddedToInventoryDuringCurrentPuzzle(screwdriver.gameObject));
		game.setHintCondition(Puzzle.BrokenKeypad, BrokenKeypadHint.LookAtKeypad, () => game.wasLookedAtCurrentPuzzle(airlockKeypadZoom.gameObject));
		game.setHintCondition(Puzzle.BrokenKeypad, BrokenKeypadHint.UnscrewBolts, () => !screwSlots[0].Get<GameObject>(0u).activeSelf && !screwSlots[1].Get<GameObject>(0u).activeSelf);
		game.setHintCondition(Puzzle.MedBed, MedBedHint.GetKeyCard, () => medBedOverrideSlot.isUnlocked || game.wasAddedToInventoryDuringCurrentPuzzle(locker0Items[0].gameObject));
		game.setHintCondition(Puzzle.MedBed, MedBedHint.GetLocker1Battery, () => batteriesAdded >= medBedBatterySlots.Length || game.wasAddedToInventoryDuringCurrentPuzzle(medBedBatterySlots[0].acceptItems[0].gameObject));
		game.setHintCondition(Puzzle.MedBed, MedBedHint.GetLocker2Battery, () => batteriesAdded >= medBedBatterySlots.Length || game.wasAddedToInventoryDuringCurrentPuzzle(medBedBatterySlots[0].acceptItems[2].gameObject));
		game.setHintCondition(Puzzle.MedBed, MedBedHint.GetUtiliyClosetBattery, () => batteriesAdded >= medBedBatterySlots.Length || game.wasAddedToInventoryDuringCurrentPuzzle(medBedBatterySlots[0].acceptItems[1].gameObject));
		game.setHintCondition(Puzzle.MedBed, MedBedHint.PlaceCard, () => medBedOverrideSlot.isUnlocked);
		game.setHintCondition(Puzzle.MedBed, MedBedHint.PlaceBatteries, () => batteriesAdded >= medBedBatterySlots.Length);
		game.setHintCondition(Puzzle.MedBed, MedBedHint.GetTriangleComponent, () => game.wasAddedToInventoryDuringCurrentPuzzle(componentSlots[2].acceptItems[0].gameObject));
		game.setHintCondition(Puzzle.MedBed, MedBedHint.GetCircleAndSquareComponent, () => game.wasAddedToInventoryDuringCurrentPuzzle(componentSlots[1].acceptItems[0].gameObject) && game.wasAddedToInventoryDuringCurrentPuzzle(componentSlots[3].acceptItems[0].gameObject));
		game.setHintCondition(Puzzle.BacteriaMatch, BacteriaMatchHint.LayInBed, () => hologram.activeSelf || game.localPlayerData.characterPoseContext.pose == medBedPose);
		game.setHintCondition(Puzzle.BacteriaMatch, BacteriaMatchHint.ClickManualScan, () => hologram.activeSelf || scannerPlane.activeSelf);
		game.setHintCondition(Puzzle.BacteriaMatch, BacteriaMatchHint.ScanHolo, () => bacteriaCount > 0);
		game.setHintCondition(Puzzle.BacteriaMatch, BacteriaMatchHint.GetBacterias, delegate
		{
			bool flag = true;
			GameObject[] array = podbacteriaZooms.ToArray<GameObject>(0f);
			foreach (GameObject item in array)
			{
				flag |= game.wasLookedAtCurrentPuzzle(item);
			}
			foreach (Ref<Zoomable, GameObject> podBacteriaOtherZoom in podBacteriaOtherZooms)
			{
				flag |= game.wasLookedAtCurrentPuzzle(podBacteriaOtherZoom);
			}
			return flag;
		});
		game.setHintCondition(Puzzle.TheCure, TheCureHint.GetSample, () => sampleSlot.isUnlocked || game.wasLookedAtCurrentPuzzle(medicineSample.gameObject));
		game.setHintCondition(Puzzle.TheCure, TheCureHint.GetPaper, () => game.wasLookedAtCurrentPuzzle(locker1Items[0].gameObject));
		game.setHintCondition(Puzzle.TheCure, TheCureHint.PlaceSample, () => sampleSlot.isUnlocked);
		game.setHintCondition(Puzzle.TheCure, TheCureHint.SolveFirst, () => medicineSolution[3].swappedPiece != medicineEmptyPieces[3]);
		game.setHintCondition(Puzzle.Holograms, HologramsHint.EnableHolos, () => !hologramCheckScheduleButton.gameObject.activeSelf);
		game.setHintCondition(Puzzle.Holograms, HologramsHint.GetTablet, () => !tabletLockScreens[2].Get<GameObject>(0f).activeSelf);
		game.setHintCondition(Puzzle.Holograms, HologramsHint.Holo1Answer, () => hologramStates[0] == hologramSolution[0]);
		game.setHintCondition(Puzzle.Holograms, HologramsHint.Holo2Answer, () => hologramStates[5] == hologramSolution[5]);
		game.setHintCondition(Puzzle.Holograms, HologramsHint.Holo3Answer, () => hologramStates[7] == hologramSolution[7]);
		game.setHintCondition(Puzzle.Holograms, HologramsHint.Holo4Answer, () => hologramStates[4] == hologramSolution[4]);
		game.setHintCondition(Puzzle.Crates, CratesHint.LookAtCrateScreen, () => game.wasLookedAtCurrentPuzzle(crateZoom.gameObject));
		game.setHintCondition(Puzzle.Crates, CratesHint.Solve1, () => crateEmptySwaps[2].swappedPiece != crateEmptySwaps[2]);
		game.setHintCondition(Puzzle.Crates, CratesHint.Solve2, () => crateEmptySwaps[9].swappedPiece != crateEmptySwaps[9]);
		game.setHintCondition(Puzzle.Crates, CratesHint.Solve3, () => crateEmptySwaps[4].swappedPiece != crateEmptySwaps[4]);
		game.setHintCondition(Puzzle.Crates, CratesHint.Solve4, () => crateEmptySwaps[5].swappedPiece != crateEmptySwaps[5]);
		game.setHintCondition(Puzzle.LockerPad, LockerPadHint.GetTablet, () => !tabletLockScreens[1].Get<GameObject>(0f).activeSelf);
		game.setHintCondition(Puzzle.CaptainsPod, CaptainsPodHint.LookAtScreen, () => game.wasAddedToInventoryDuringCurrentPuzzle(scramblerZoom.gameObject));
		game.setHintCondition(Puzzle.CaptainsPod, CaptainsPodHint.LookAtButtons, delegate
		{
			bool flag = false;
			foreach (int scrambleCurrentImage in scrambleCurrentImages)
			{
				flag = flag || scrambleCurrentImage > 0;
			}
			return flag;
		});
		game.setHintCondition(Puzzle.RFID, RFIDHint.GetAudioDevice, () => game.wasAddedToInventoryDuringCurrentPuzzle(audioSplicer.gameObject));
		game.setHintCondition(Puzzle.RFID, RFIDHint.Scan1, () => splicerAvailableLogs.Contains(splicerRfidSlots[2]));
		game.setHintCondition(Puzzle.RFID, RFIDHint.Scan2, () => splicerAvailableLogs.Contains(splicerRfidSlots[0]));
		game.setHintCondition(Puzzle.RFID, RFIDHint.Scan3, () => splicerAvailableLogs.Contains(splicerRfidSlots[1]));
		game.setHintCondition(Puzzle.RFID, RFIDHint.Scan4, () => splicerAvailableLogs.Contains(splicerRfidSlots[3]));
		game.setHintCondition(Puzzle.SoundSplicing, SoundSplicingHint.GetCaptainsNote, () => isSplicerCorrect || game.wasAddedToInventoryDuringCurrentPuzzle(captainsNote));
		game.setHintCondition(Puzzle.SoundSplicing, SoundSplicingHint.LookAtCode, () => isSplicerCorrect);
		game.setHintCondition(Puzzle.SoundSplicing, SoundSplicingHint.SpliceCode, () => isSplicerCorrect);
		game.setHintCondition(Puzzle.SoundSplicing, SoundSplicingHint.GetKeycard, () => exitSlot.Get<Slot>(0).isUnlocked || game.wasAddedToInventoryDuringCurrentPuzzle(crateOverrideKey.gameObject));
		game.setHintCondition(Puzzle.SoundSplicing, SoundSplicingHint.PlaceKeycard, () => exitSlot.Get<Slot>(0).isUnlocked);
	}

	public override void onInitPredicates()
	{
		game.registerPredicate(LevelPredicate.Pod0Solved, () => currentQuestions[0] >= dialogQuestions.Count);
		game.registerPredicate(LevelPredicate.Pod1Solved, () => currentQuestions[1] >= dialogQuestions.Count);
		game.registerPredicate(LevelPredicate.Pod2Solved, () => currentQuestions[2] >= dialogQuestions.Count);
		game.registerPredicate(LevelPredicate.Pod3Solved, () => currentQuestions[3] >= dialogQuestions.Count);
		game.registerPredicate(LevelPredicate.Locker0Solved, () => checkSolution(0));
		game.registerPredicate(LevelPredicate.Locker1Solved, () => checkSolution(1));
		game.registerPredicate(LevelPredicate.Locker2Solved, () => checkSolution(2));
		game.registerPredicate(LevelPredicate.ScanPosition0, () => lastScannedPoint == 0 && medScannerTimer >= medScannerMaxTime - 0.5f);
		game.registerPredicate(LevelPredicate.ScanPosition1, () => lastScannedPoint == 1 && medScannerTimer >= medScannerMaxTime - 0.5f);
		game.registerPredicate(LevelPredicate.ScanPosition2, () => lastScannedPoint == 2 && medScannerTimer >= medScannerMaxTime - 0.5f);
		game.registerPredicate(LevelPredicate.CanMedBedScan, 0.01f, true, () => checkCanMedBedScan());
		game.registerPredicate(LevelPredicate.MedBedBatterisSolved, 0.5f, false, () => batteriesAdded >= medBedBatterySlots.Length);
		game.registerPredicate(LevelPredicate.CrateSolved, 1.4f, false, () => checkCrateSolved());
		game.registerPredicate(LevelPredicate.CaptainsPodSolved, 1f, false, () => checkCaptainsKeypad());
		game.registerPredicate(LevelPredicate.SplicerSolved, 1.9f, false, () => isSplicerCorrect && splicerExitSlot.insertedItem != null);
	}

	public override void onLevelPredicateDone(int type, int doneCounter)
	{
		switch ((LevelPredicate)type)
		{
		case LevelPredicate.Pod0Solved:
			solvePod(0);
			break;
		case LevelPredicate.Pod1Solved:
			solvePod(1);
			break;
		case LevelPredicate.Pod2Solved:
			solvePod(2);
			break;
		case LevelPredicate.Pod3Solved:
			solvePod(3);
			break;
		case LevelPredicate.Locker0Solved:
			solveLocker(0);
			break;
		case LevelPredicate.Locker1Solved:
			solveLocker(1);
			break;
		case LevelPredicate.Locker2Solved:
			solveLocker(2);
			break;
		case LevelPredicate.ScanPosition0:
			scannedBacteria(0);
			break;
		case LevelPredicate.ScanPosition1:
			scannedBacteria(1);
			break;
		case LevelPredicate.ScanPosition2:
			scannedBacteria(2);
			break;
		case LevelPredicate.CrateSolved:
			solveCrate();
			break;
		case LevelPredicate.CaptainsPodSolved:
			solveCaptainsKeypad();
			break;
		case LevelPredicate.SplicerSolved:
			solveAudioAnalyzer();
			break;
		case LevelPredicate.CanMedBedScan:
			setCanMedBedScan(enable: true);
			break;
		case LevelPredicate.MedBedBatterisSolved:
			solveMedBedBatteries();
			break;
		}
	}

	public override void onLevelPredicateUndone(int type)
	{
		if (type == 13)
		{
			setCanMedBedScan(enable: false);
		}
	}

	public override void onRPCCalled(int type)
	{
		switch (type)
		{
		case 0:
			airlockKeypadCorrect();
			break;
		case 1:
			airlockKeypadWrong();
			break;
		case 2:
			solveBacteriaMatching();
			break;
		case 3:
			bacteriaMatchingNotSolved();
			break;
		case 4:
			medicineChangeScreen(MedicineScreens.SubstanceCreated);
			break;
		case 5:
			medicineNotSolved();
			break;
		case 6:
			onHasTentaclesLeft();
			break;
		case 7:
			onNoTentaclesLeft();
			break;
		case 8:
			solveHologram();
			break;
		case 9:
			notSolveHologram();
			break;
		case 10:
			solveAudioAnalyzer();
			break;
		case 11:
			onAudioAnalyzerNotSolved();
			break;
		case 12:
			onSolvedMedicineBeforeScanning();
			break;
		case 13:
			onNotSolvedMedicineBeforeScanning();
			break;
		case 14:
			solveComponents();
			break;
		case 15:
			componentsNotSolved();
			break;
		}
	}

	public override void onInit()
	{
		Interactive.linkInteractives(splicerMiddleCut.Get<Switch3D>(0f), splicerMiddleDelete.Get<Switch3D>(0f), splicerSlidables[0], splicerSlidables[1]);
		Interactive.linkInteractives(airlockKeypadClosure, airlockKeypadButtons[0], airlockKeypadButtons[1], airlockKeypadButtons[2], airlockKeypadButtons[4], airlockKeypadButtons[5], airlockKeypadButtons[8], airlockKeypadSwitches[0], airlockKeypadSwitches[1], airlockKeypadSwitches[2], airlockKeypadSwitches[3], airlockKeypadSwitches[4], airlockKeypadSwitches[5], airlockKeypadSwitches[6], airlockKeypadSwitches[7], airlockKeypadSwitches[8]);
		Interactive.linkInteractives(splicerTabs[0], splicerTabs[1], splicerTabs[2], splicerTabs[3], splicerMiddleCut.Get<Switch3D>(0f), splicerMiddleDelete.Get<Switch3D>(0f));
		Interactive.linkInteractives(captainsPodButtons[0], captainsPodButtons[1], captainsPodButtons[2], captainsPodButtons[3], captainsPodButtons[4], captainsPodButtons[5], captainsPodButtons[6], captainsPodButtons[7], captainsPodButtons[8], captainsPodButtons[9]);
		Interactive.linkInteractives(locker0Buttons[0], locker0Buttons[1], locker0Buttons[2], locker0Buttons[3], locker0Buttons[4], locker0Buttons[5], locker0Buttons[6], locker0Buttons[7], locker0Buttons[8]);
		Interactive.linkInteractives(locker1Buttons[0], locker1Buttons[1], locker1Buttons[2], locker1Buttons[3], locker1Buttons[4], locker1Buttons[5], locker1Buttons[6], locker1Buttons[7], locker1Buttons[8]);
		Interactive.linkInteractives(locker2Buttons[0], locker2Buttons[1], locker2Buttons[2], locker2Buttons[3], locker2Buttons[4], locker2Buttons[5], locker2Buttons[6], locker2Buttons[7], locker2Buttons[8]);
		Interactive.linkInteractives(medBedHoloBacterias[0], medBedHoloBacterias[1], medBedHoloBacterias[2], medBedHoloBacterias[3], medBedHoloBacterias[4], medBedHoloBacterias[5]);
		Interactive.linkInteractives(medBedHoloBacterias[6], medBedHoloBacterias[7], medBedHoloBacterias[8], medBedHoloBacterias[9], medBedHoloBacterias[10], medBedHoloBacterias[11]);
		Interactive.linkInteractives(medBedHoloBacterias[12], medBedHoloBacterias[13], medBedHoloBacterias[14], medBedHoloBacterias[15], medBedHoloBacterias[16], medBedHoloBacterias[17]);
		Dictionary<Transform, List<Interactive>> dictionary = new Dictionary<Transform, List<Interactive>>();
		Switch3D[] array = medBedHoloBacterias;
		foreach (Switch3D switch3D in array)
		{
			if (dictionary.TryGetValue(switch3D.transform.parent, out var value))
			{
				value.Add(switch3D);
				continue;
			}
			dictionary[switch3D.transform.parent] = new List<Interactive> { switch3D };
		}
		foreach (List<Interactive> value2 in dictionary.Values)
		{
			Interactive.linkInteractives(value2.ToArray());
		}
		Canvas[] componentsInChildren = game.levelContainer.GetComponentsInChildren<Canvas>();
		foreach (Canvas cameraAsWorldCamera in componentsInChildren)
		{
			game.setCameraAsWorldCamera(cameraAsWorldCamera);
		}
		initLockerKeypads();
		initAirlock();
		initMedicine();
		initMedBed();
		initChipScreen();
		initScrambledCode();
		initAudioSplicer();
		initCrate();
		initWaveform();
		initPods();
		initHolograms();
		for (int j = 0; j < blinkingUIButtons.Length; j++)
		{
			game.startTimer(new PressUIButtonAnimationTimer(j), 1f);
		}
		for (int k = 0; k < wallHintButtons.Length; k++)
		{
			game.startTimer(new WallHintTimer(k, isGoingDown: true), 1f);
			game.startSwitch(wallHintButtons[k].Get<Switch3D>(0));
		}
		scanInstance = PineFmod.createInstance("event:/Sound Effects/04 Items/Computers & Communications/Computers & Games/Scanning 4");
		PineFmod.set3DAttributes(scanInstance, PineFmod.to3DAttributes(hologram.transform));
	}

	public override void onInitAfterLoad()
	{
		if (cutMeshParameters.Count <= 0)
		{
			return;
		}
		Debug.Log("Loading cuts after save");
		foreach (CutMeshParameter cutMeshParameter in cutMeshParameters)
		{
			Debug.Log($"splicedLogIndex: {cutMeshParameter.splicedLogIndex}");
			cutQuadMesh(splicerSplicedLogs[cutMeshParameter.splicedLogIndex], cutMeshParameter.splicedLogIndex, cutMeshParameter.middleLogIndex, cutMeshParameter.rightCutValue, cutMeshParameter.leftCutValue, cutMeshParameter.nextLogScale, out var _, out var _);
		}
		game.invalidateItemImpostors(audioSplicer);
	}

	public override void onConvertToSinglePlayer()
	{
		convertedToSp = true;
		for (int i = 0; i < podSwitches.Length; i++)
		{
			if (i > 0 && podsInitiated[i])
			{
				unlockPod(i);
				Debug.Log("here " + i);
			}
		}
		initPodHints(1);
		scannerNeedsAPerson = false;
		if (holoSpEnabled)
		{
			medbedHeadScreenTween.transitionTo("Raise");
			bedChangeScreen(BedScreens.Scanned);
			medbedHoloBeam.SetActive(value: true);
			hologram.SetActive(value: true);
			medBedPose.targetable = false;
			medBedPose.lockedInPose = false;
			game.handlePoseLeaveLocal(medBedPose);
		}
		scannerChangeScreen(currentScannerScreen);
		medScannerTimer = 0f;
	}

	public override void onUpdate()
	{
		onBacteriaScanUpdate();
		onAudioSplicerUpdate();
		if (bacteriaZoomToEnter != null && !game.isInTopZoom(bacteriaZoomToEnter))
		{
			game.handleZoomEnter(bacteriaZoomToEnter, Game.ScreenTargetType.Zoomable);
			bacteriaZoomToEnter = null;
		}
		for (int i = 0; i < rfidSlotAnimations.Length; i++)
		{
			bool flag = game.isInAnyPlayerHand(audioSplicer) && !splicerAvailableLogs.Contains(splicerRfidSlots[i]);
			if (!rfidEmitters[i].IsPlaying() && flag)
			{
				PineFmod.play(rfidEmitters[i]);
			}
			else if (rfidEmitters[i].IsPlaying() && !flag)
			{
				PineFmod.stop(rfidEmitters[i]);
			}
		}
		if (podsPuzzleSolved)
		{
			return;
		}
		int playerCount = game.getPlayerCount();
		for (int j = 0; j < podsInitiated.Length; j++)
		{
			if (!podsInitiated[j] && j < playerCount)
			{
				Debug.Log($"POD {j} INITIATED IN UPDATE");
				initPod(j, playerCount);
				initPodHints(playerCount);
			}
		}
	}

	public override void onSwapper(Swapper swapper, Swapper.SwapperEvent swapperEvent)
	{
		if (swapper == crateSwapper)
		{
			onCrateSwapper(swapperEvent);
		}
		if (swapper == chipSwapper)
		{
			onChipSwapper(swapperEvent);
		}
		if (swapper == medicineSwapper)
		{
			onMedicineSwapper(swapperEvent);
		}
	}

	public override void onSwitch3D(Switch3D target, Switch3DEvent switchEvent)
	{
		if (target == comicBookSwitch)
		{
			game.saveAchievement("ACHIEVEMENT_READ_A_COMIC");
		}
		if (target == corridorScanButton)
		{
			onCorridorScanButton(switchEvent);
		}
		onCrateSwitch3D(target, switchEvent);
		int index = UnityUtils.getIndex(podSwitches, target);
		if (index >= 0)
		{
			onPodDoor(index);
		}
		if (target == captainsPod)
		{
			onCaptainsPodSwitch(switchEvent);
		}
		int num = medBedBacteriaSphereButtons.IndexOf(target, 0);
		if (num >= 0)
		{
			onMedbedBacteriaSphereButtons(num, switchEvent);
		}
		switch (switchEvent)
		{
		case Switch3DEvent.Start:
		{
			if (target == makeHoloButton)
			{
				makeHolo();
			}
			if (target == splicerMiddleCut.Get<Switch3D>(0f))
			{
				onSplicerMiddleCut();
			}
			if (target == hologramCheckScheduleButton)
			{
				onHologramCheckSchedule();
			}
			int index2;
			if ((index2 = Array.IndexOf(hologramNextButtons, target)) != -1)
			{
				onHologramChange(index2, 1);
			}
			if ((index2 = Array.IndexOf(hologramPreviousButtons, target)) != -1)
			{
				onHologramChange(index2, -1);
			}
			int num2 = Array.IndexOf(scramblerPatternButtons, target);
			if (num2 >= 0)
			{
				onScramblerButton(num2);
			}
			if (target == splicerMiddleDelete.Get<Switch3D>(0f))
			{
				onSplicerBottomDelete();
			}
			int index3 = UnityUtils.getIndex(captainsPodButtons, target);
			if (index3 >= 0)
			{
				onCaptainsButtons(index3);
			}
			checkPodChoice(0, pod3AnswerButtons);
			checkPodChoice(1, pod3AnswerButtons1);
			checkPodChoice(2, pod3AnswerButtons2);
			checkPodChoice(3, pod3AnswerButtons3);
			checkPodChoice(0, pod4AnswerButtons);
			checkPodChoice(1, pod4AnswerButtons1);
			checkPodChoice(2, pod4AnswerButtons2);
			checkPodChoice(3, pod4AnswerButtons3);
			if (target == medicineCreateSubstanceButton)
			{
				onMedicineCreateSubstanceButton();
			}
			if (target == medicineReleaseCureButton)
			{
				releaseMedicine();
			}
			if (target == medBedStartScanButton[0] || target == medBedStartScanButton[1])
			{
				onMedBedStartScan();
			}
			if (target == medBedStartMatchingButton[0] || target == medBedStartMatchingButton[1])
			{
				onMedBedStartMatching();
			}
			if (target == medBedCheckMatchButtons[0] || target == medBedCheckMatchButtons[1])
			{
				onMedBedCheckBacteriaButton();
			}
			if (target == medBedConfirmComponentsButton)
			{
				onMedBedConfirmComponents();
			}
			break;
		}
		case Switch3DEvent.On:
		{
			int num3 = Array.IndexOf(airlockKeypadButtons, target);
			if (num3 >= 0)
			{
				onAirlockKeypad(num3);
			}
			int num4 = Array.IndexOf(splicerTabs, target);
			if (num4 >= 0)
			{
				onSplicerTabClick(num4);
			}
			break;
		}
		case Switch3DEvent.Off:
			if (Array.Exists(airlockKeypadButtons, (Switch3D x) => x == target))
			{
				onAirlockKeypadOff(target);
			}
			break;
		}
		if (switchEvent == Switch3DEvent.On || switchEvent == Switch3DEvent.Off)
		{
			int num5 = wallHintButtons.IndexOf(target, 0);
			if (num5 >= 0)
			{
				onWallHintButton(switchEvent, num5);
			}
			if (target == hologramOnButton.Get<Switch3D>(0))
			{
				onHologramPrintSchedule();
				onHologramOn();
			}
			if (splicedAudio.Exists((SplicedLogState x) => x.button == target))
			{
				onSplicerBottomSelect(target, switchEvent);
			}
			if (Array.Exists(medBedHoloBacterias, (Switch3D x) => x == target))
			{
				onHoloBacteria(target);
			}
		}
		for (int num6 = 0; num6 < food.Length; num6++)
		{
			if (target.transform.parent.gameObject == food[num6].Get<GameObject>(0) && switchEvent == Switch3DEvent.Start)
			{
				if (food[num6].Get<TweenState>((short)0) != null)
				{
					food[num6].Get<TweenState>((short)0).transitionTo("NewState", 4f);
				}
				else
				{
					OnFoodEaten(num6);
				}
			}
		}
		if (switchEvent != Switch3DEvent.On)
		{
			return;
		}
		int num7 = Array.IndexOf(podMedButtons, target);
		if (num7 >= 0)
		{
			onPodMedButton(target, num7);
		}
		for (int num8 = 0; num8 < lockers.Count; num8++)
		{
			int index4 = UnityUtils.getIndex(lockers[num8].buttons, target);
			if (index4 >= 0)
			{
				onLockerKeypad(num8, index4);
			}
		}
		if (target == levelExitRef.Get<Switch3D>(0f))
		{
			game.levelCompleted();
		}
		void checkPodChoice(int podIndex, RefArray<Switch3D, TweenState, GameObject> answerButtons)
		{
			int num9 = answerButtons.IndexOf(target, 0);
			if (num9 >= 0)
			{
				onPodDialogChoice(podIndex, target, num9);
			}
		}
	}

	public override void onTweenTransitionDone(TweenState tweenState, string state)
	{
		int num = Array.IndexOf(componentSlotTweens, tweenState);
		if (num >= 0)
		{
			onComponentSlotTweenDone(num);
		}
		if (tweenState == medicineSampleTween)
		{
			onMedicineSampleTweenDone(state);
		}
		if (droneAnimations.Contains(tweenState, 0))
		{
			onDroneAnimationsComplete(tweenState, state);
		}
		int num2 = tabletLockScreens.IndexOf(tweenState, 0);
		if (num2 >= 0)
		{
			tabletLockScreens[num2].Get<GameObject>(0f).SetActive(value: false);
			game.invalidateItemImpostors(tablets[num2]);
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

	public override void onSequenceDone(Sequence sequence)
	{
		if (sequence == corridorScanFieldSequence)
		{
			onCorridorScanSequence();
		}
	}

	public override void onTimerUpdate(Timer timer)
	{
		if (timer is SplicerBottomTimer timer2)
		{
			onSplicerBottomTimerUpdate(timer2);
		}
	}

	public override void onTimerDone(Timer timer)
	{
		if (timer is PodAnswerTimer timer2)
		{
			onPodAnswerTimerDone(timer2);
		}
		if (timer is MedBedBlinkTimer)
		{
			medBedKeySlotBlink();
		}
		if (timer is MedBedPowerTimer timer3)
		{
			onMedBedPowerTimerDone(timer3);
		}
		if (timer is MedBedComponentsTimer timer4)
		{
			onMedBedComponentsTimerDone(timer4);
		}
		if (timer is MedBedSolvedTimer)
		{
			onMedBedSolvedTimer();
		}
		if (timer is MedBedMatchingTimer timer5)
		{
			onMedBedMatchingTimerDone(timer5);
		}
		if (timer is HoloTimer timer6)
		{
			onHoloTimer(timer6);
		}
		if (timer is ScannedBacteriaTimer timer7)
		{
			onScannedBacteriaTimerDone(timer7);
		}
		if (timer is MedBedCheckBacteriaTimer timer8)
		{
			onMedBedCheckBacteriaTimer(timer8);
		}
		if (timer is BacteriaSolvedTimer timer9)
		{
			onBacteriaSolvedTimerDone(timer9);
		}
		if (timer is MedBedEnableHoloTimer medBedEnableHoloTimer)
		{
			onMedBedEnableHoloTimerDone(medBedEnableHoloTimer);
		}
		if (timer is AudioAnalyzerTimer timer10)
		{
			onAudioAnalyzerTimerDone(timer10);
		}
		if (timer is SplicerMiddleTimer timer11)
		{
			onSplicerMiddleTimerDone(timer11);
		}
		if (timer is SplicerBottomTimer splicerBottomTimer)
		{
			onSplicerBottomTimerDone(splicerBottomTimer);
		}
		if (timer is MedicinePuzzleScreenTimer timer12)
		{
			onMedicinePuzzleScreenTimer(timer12);
		}
		if (timer is ReleaseMedicineTimer timer13)
		{
			onReleaseMedicineTimerDone(timer13);
		}
		if (timer is AirlockSlotTimerTimer timer14)
		{
			onAirlockSlotTimer(timer14);
		}
		if (timer is CorridorScanTimer timer15)
		{
			onCorridorScanTimer(timer15);
		}
		if (timer is SolveCrateTimer timer16)
		{
			onSolveCrateTimer(timer16);
		}
		if (timer is PodMedTimer timer17)
		{
			onPodMedTimerDone(timer17);
		}
		if (timer is PodMedButtonTimer timer18)
		{
			onPodMedButtonTimerDone(timer18);
		}
		if (timer is HologramTimer timer19)
		{
			onHologramTimerDone(timer19);
		}
		if (timer is PressUIButtonAnimationTimer timer20)
		{
			onPressUIButtonAnimationTimerDone(timer20);
		}
		if (timer is CaptainsPodTimer timer21)
		{
			onCaptainsPodTimerDone(timer21);
		}
		LockerTimer lockerTimer = timer as LockerTimer;
		if (lockerTimer != null)
		{
			onLockerTimer(lockers.Find((TriangleKeypad x) => x.door == lockerTimer.door), lockerTimer.index);
		}
		if (timer is ChipScreensTimer timer22)
		{
			onChipScreensTimerDone(timer22);
		}
		if (timer is CrateBlinkingTimer timer23)
		{
			onCrateBlinkingTimerDone(timer23);
		}
		if (timer is RfidBlinkingTimer timer24)
		{
			onRfidBlinkingTimerDone(timer24);
		}
		if (timer is AirlockKeypadTimer timer25)
		{
			onAirlockKeypadTimerDone(timer25);
		}
		if (timer is WallHintTimer timer26)
		{
			onWallHintTimerDone(timer26);
		}
		if (timer is RfidSlotTimer timer27)
		{
			onRfidSlotTimerDone(timer27);
		}
		if (timer is RfidLEDTimer timer28)
		{
			onRfidLEDTimerDone(timer28);
		}
		if (timer is UiBlinkTimer timer29)
		{
			onUiErrorTimerDone(timer29);
		}
		if (timer is HoloCardTimer)
		{
			crateOverrideKey.targetable = true;
		}
		if (timer is ScannerSlidableTransition)
		{
			PineFmod.stop(scannerSlidable.soundEmitter);
		}
	}

	public override void onSlot(Slot targetSlot)
	{
		if (targetSlot == medBedOverrideSlot)
		{
			solveMedBedKey();
		}
		int num = Array.IndexOf(medBedBatterySlots, targetSlot);
		if (num >= 0)
		{
			addedBattery(num);
		}
		if (targetSlot == sampleSlot)
		{
			onSampleSlot();
		}
		int num2 = Array.IndexOf(splicerRfidSlots, targetSlot);
		if (num2 >= 0)
		{
			onRfidSlot(targetSlot, num2);
		}
		int num3 = screwSlots.IndexOf(targetSlot, 0);
		if (num3 >= 0)
		{
			onAirlockSlot(num3);
		}
		if (targetSlot == exitSlot.Get<Slot>(0))
		{
			onExitSlot();
		}
		if (targetSlot == splicerExitSlot)
		{
			onSplicerSlot();
		}
		int num4 = Array.IndexOf(componentSlots, targetSlot);
		if (num4 >= 0)
		{
			onComponentSlot(num4);
		}
	}

	public override void onRemoveFromSlot(Slot targetSlot, Item item)
	{
		int num = Array.IndexOf(componentSlots, targetSlot);
		if (num >= 0)
		{
			onRemoveComponentFromSlot(num);
		}
	}

	public override void onPose(NetPlayerId playerId, CharacterPose characterPose, CharacterPoseState poseEvent)
	{
		if (debug_enterPose && poseEvent == CharacterPoseState.InPose)
		{
			debug_enterPose = false;
		}
		else if (!sampleProvided && characterPose == medBedPose && poseEvent == CharacterPoseState.TransitionOut && enableMedbedHologram)
		{
			game.startTimer(new MedBedEnableHoloTimer(), 1f);
		}
	}

	public override void onZoomEnter(GameObject zoomedItem)
	{
		int num = podbacteriaZooms.IndexOf(zoomedItem, 0f);
		if (num >= 0)
		{
			onMedBedZoom(num);
		}
		int num2 = podBacteriaOtherZooms.IndexOf(zoomedItem, 0f);
		if (num2 >= 0)
		{
			onMedBedZoom(num2);
		}
		if (zoomedItem == audioSplicer.gameObject)
		{
			onAudioSplicerZoom(enter: true);
		}
	}

	public override void onZoomLeave(GameObject zoomedItem)
	{
		int num = podbacteriaZooms.IndexOf(zoomedItem, 0f);
		if (num >= 0)
		{
			onMedBedZoomLeave(num);
		}
		int num2 = podBacteriaOtherZooms.IndexOf(zoomedItem, 0f);
		if (num2 >= 0)
		{
			onMedBedZoomLeave(num2);
		}
		if (zoomedItem == audioSplicer)
		{
			onAudioSplicerZoom(enter: false);
		}
	}

	public override void onAddToInventory(Item item)
	{
		if (item == audioSplicer)
		{
			if (!splicerAddedToInventory)
			{
				changeRfidScreen(RfidScreen.Scanning);
			}
			splicerAddedToInventory = true;
			splicerAntenaTween.transitionTo("Up");
		}
	}

	public override void onRemoveFromInventory(Item item)
	{
		if (item == audioSplicer)
		{
			splicerAntenaTween.transitionTo("Up", 1f, 0f);
		}
	}

	public override void onTrigger(Trigger trigger, TriggerEvent triggerEvent)
	{
		if (trigger == airlockTrigger.Get<Trigger>(0f))
		{
			onAirlockTrigger(triggerEvent);
		}
	}

	public override void onSlidableMoved(Slidable slidable, MoveEvent moveEvent)
	{
		int num = tabletLockSlidables.IndexOf(slidable);
		if (num >= 0 && moveEvent == MoveEvent.Released && UnityUtils.closeEnough(tabletLockSlidables[num].value, 1f))
		{
			tabletLockSlidables[num].targetable = false;
			tabletLockSlidables[num].snapMode = Slidable.SnapMode.DontSnap;
			tabletLockScreens[num].Get<TweenState>(0).transitionTo("Off");
			tabletScreens[num].transitionTo("On");
		}
	}

	private void onWallHintButton(Switch3DEvent state, int index)
	{
		if (state == Switch3DEvent.On)
		{
			wallHintHolos[index].SetActive(value: false);
			return;
		}
		wallHintHolos[index].SetActive(value: true);
		wallHintButtons[index].Get<MaterialState>(0f).transitionTo("Down", 5f);
	}

	private void onWallHintTimerDone(WallHintTimer timer)
	{
		if (wallHintButtons[timer.index].Get<Switch3D>(0).state != Switch3DState.Off)
		{
			wallHintButtons[timer.index].Get<MaterialState>(0f).transitionTo("Down", 5f, timer.isGoingDown ? 0f : 1f);
		}
		game.startTimer(new WallHintTimer(timer.index, !timer.isGoingDown), 0.7f);
	}

	private void initPod(int podIndex, int playerCount)
	{
		podPoses[podIndex].lockedInPose = podIndex < playerCount;
		podSwitches[podIndex].targetable = podIndex >= playerCount;
		if (podSwitches[podIndex].state != Switch3DState.Off)
		{
			game.startSwitch(podSwitches[podIndex]);
		}
		podMedButtons[podIndex].targetable = false;
		podFirstAidMaterialStates[podIndex].setState("Down", (podIndex >= playerCount) ? 1f : 0f);
		if (podIndex < playerCount)
		{
			game.startTimer(new PodMedButtonTimer(podIndex, goingDown: true), 0.1f);
		}
		if (podIndex < playerCount)
		{
			podDialogSetUpQuestion(podIndex, currentQuestions[podIndex]);
			podChangeScreen(podIndex, PodScreens.WelcomeBack, null, null);
			game.startTimer(new PodAnswerTimer(podIndex, -3, -1), 7f);
		}
		else
		{
			podChangeScreen(podIndex, PodScreens.None, null, null);
		}
		podsInitiated[podIndex] = podIndex < playerCount;
	}

	private void initPodHints(int playerCount)
	{
		if (playerCount >= 3)
		{
			podTarinHint.position = mpPositionPodHint.position;
			podTarinHint.rotation = mpPositionPodHint.rotation;
		}
		for (int i = 0; i < deimosPlayerHints.Length; i++)
		{
			deimosPlayerHints[i].SetActive(i == playerCount - 1);
		}
		if (playerCount > deimosPlayerHints.Length)
		{
			deimosPlayerHints[deimosPlayerHints.Length - 1].SetActive(value: true);
		}
	}

	private void initPods()
	{
		int playerCount = game.getPlayerCount();
		for (int i = 0; i < podPoses.Length; i++)
		{
			initPod(i, playerCount);
		}
		initPodHints(playerCount);
	}

	[DebugButton(null, Tint.Default, PostClickAction.ReturnToGame, 0, new object[] { })]
	private void debugPodSwitch()
	{
		game.handlePoseLeaveLocal(podPoses[debug_currentPod]);
		debug_currentPod = (debug_currentPod + 1) % podPoses.Length;
		if (!podPoses[debug_currentPod].gameObject.activeSelf)
		{
			podPoses[debug_currentPod].gameObject.SetActive(value: true);
		}
		if (!podPoses[debug_currentPod].targetable)
		{
			podPoses[debug_currentPod].targetable = true;
		}
		debug_enterPose = true;
	}

	private void podChangeScreen(int podIndex, PodScreens screen, string question, List<string> questions)
	{
		if (questions != null && questions.Count > 0)
		{
			screen = ((questions.Count == 3) ? PodScreens.Answer3 : PodScreens.Answer4);
		}
		podCurrentScreen = screen;
		if (podIndex == 0)
		{
			podChange(podScreens, pod3AnswerTexts, pod4AnswerTexts, pod3AnswerButtons, pod4AnswerButtons);
		}
		if (podIndex == 1)
		{
			podChange(podScreens1, pod3AnswerTexts1, pod4AnswerTexts1, pod3AnswerButtons1, pod4AnswerButtons1);
		}
		if (podIndex == 2)
		{
			podChange(podScreens2, pod3AnswerTexts2, pod4AnswerTexts2, pod3AnswerButtons2, pod4AnswerButtons2);
		}
		if (podIndex == 3)
		{
			podChange(podScreens3, pod3AnswerTexts3, pod4AnswerTexts3, pod3AnswerButtons3, pod4AnswerButtons3);
		}
		void podChange(GameObject[] screens, Text[] pod3texts, Text[] pod4texts, RefArray<Switch3D, TweenState, GameObject> pod3Buttons, RefArray<Switch3D, TweenState, GameObject> pod4Buttons)
		{
			for (int i = 0; i < screens.Length; i++)
			{
				screens[i].SetActive(i == (int)screen);
			}
			if (screen == PodScreens.Answer3)
			{
				pod3texts[0].text = Localization.translate(question);
				for (int j = 1; j < questions.Count + 1; j++)
				{
					pod3texts[j].text = Localization.translate(questions[j - 1]);
				}
				podIncorrectOverlay[podIndex].SetActive(value: false);
			}
			else if (screen == PodScreens.Answer4)
			{
				pod4texts[0].text = Localization.translate(question);
				for (int k = 1; k < questions.Count + 1; k++)
				{
					pod4texts[k].text = Localization.translate(questions[k - 1]);
				}
				podIncorrectOverlay[podIndex].SetActive(value: false);
			}
			foreach (Ref<Switch3D, TweenState, GameObject> pod3Button in pod3Buttons)
			{
				pod3Button.Get<TweenState>(0f).setState("Correct", 0f);
				pod3Button.Get<Switch3D>(0).targetable = true;
				pod3Button.Get<GameObject>(0u).SetActive(value: true);
			}
			foreach (Ref<Switch3D, TweenState, GameObject> pod4Button in pod4Buttons)
			{
				pod4Button.Get<TweenState>(0f).setState("Correct", 0f);
				pod4Button.Get<Switch3D>(0).targetable = true;
				pod4Button.Get<GameObject>(0u).SetActive(value: true);
			}
		}
	}

	private void onPodDialogChoice(int podIndex, Switch3D target, int choiceIndex)
	{
		TweenState choiceTween = null;
		if (podIndex == 0)
		{
			setUntargetable(pod3AnswerButtons, pod4AnswerButtons);
		}
		if (podIndex == 1)
		{
			setUntargetable(pod3AnswerButtons1, pod4AnswerButtons1);
		}
		if (podIndex == 2)
		{
			setUntargetable(pod3AnswerButtons2, pod4AnswerButtons2);
		}
		if (podIndex == 3)
		{
			setUntargetable(pod3AnswerButtons3, pod4AnswerButtons3);
		}
		bool flag = dialogQuestions[currentQuestions[podIndex]].correctAnswer == choiceIndex;
		if (currentQuestions[podIndex] == 6)
		{
			ref string reference = ref podCurrentShipNames[podIndex];
			reference = reference + "-" + dialogQuestions[currentQuestions[podIndex]].answers[choiceIndex];
			flag = podCurrentShipNames[podIndex] == shipName;
		}
		else if (currentQuestions[podIndex] >= 3)
		{
			podCurrentShipNames[podIndex] += dialogQuestions[currentQuestions[podIndex]].answers[choiceIndex];
		}
		if (choiceTween != null)
		{
			choiceTween.setState(flag ? "Correct" : "Error");
		}
		if (currentQuestions[podIndex] == 0)
		{
			game.startTimer(new PodAnswerTimer(podIndex, 0, choiceIndex), 0.4f);
		}
		else if (flag || (currentQuestions[podIndex] >= 3 && currentQuestions[podIndex] < 6))
		{
			game.startTimer(new PodAnswerTimer(podIndex, 2, choiceIndex), 0.4f);
		}
		else
		{
			game.startTimer(new PodAnswerTimer(podIndex, 3, choiceIndex), 0.4f);
		}
		void setUntargetable(RefArray<Switch3D, TweenState, GameObject> pod3Buttons, RefArray<Switch3D, TweenState, GameObject> pod4Buttons)
		{
			for (int i = 0; i < pod3Buttons.Length; i++)
			{
				pod3Buttons[i].Get<Switch3D>(0).targetable = false;
				if (pod3Buttons[i].Get<Switch3D>(0) == target)
				{
					choiceTween = pod3Buttons[i].Get<TweenState>(0f);
				}
			}
			for (int j = 0; j < pod4Buttons.Length; j++)
			{
				pod4Buttons[j].Get<Switch3D>(0).targetable = false;
				if (pod4Buttons[j].Get<Switch3D>(0) == target)
				{
					choiceTween = pod4Buttons[j].Get<TweenState>(0f);
				}
			}
		}
	}

	private void onPodAnswerTimerDone(PodAnswerTimer timer)
	{
		Debug.Log($"onPodAnswerTimerDone: {timer.podIndex}, {timer.index}");
		if (timer.index == -3)
		{
			podChangeScreen(timer.podIndex, PodScreens.InitialScan, null, null);
			podPreliminaryScans[timer.podIndex].transitionTo("Play", 0.15f);
			game.startTimer(new PodAnswerTimer(timer.podIndex, -2, -1), 7f);
		}
		else if (timer.index == -2)
		{
			podChangeScreen(timer.podIndex, PodScreens.Results, null, null);
			game.startTimer(new PodAnswerTimer(timer.podIndex, -1, -1), 5f);
		}
		else if (timer.index == -1)
		{
			podChangeScreen(timer.podIndex, PodScreens.Answer3, dialogQuestions[0].question, dialogQuestions[0].answers);
		}
		else if (timer.index == 0)
		{
			podChangeScreen(timer.podIndex, PodScreens.NeedMedicine, null, null);
			game.startTimer(new PodAnswerTimer(timer.podIndex, 1, -1), 0.3f);
		}
		else if (timer.index == 1)
		{
			podMedButtons[timer.podIndex].targetable = true;
		}
		else if (timer.index == 2)
		{
			currentQuestions[timer.podIndex]++;
			podDialogSetUpQuestion(timer.podIndex, currentQuestions[timer.podIndex]);
		}
		else if (timer.index == 3)
		{
			podIncorrectOverlay[timer.podIndex].SetActive(value: true);
			currentQuestions[timer.podIndex] = 1;
			podCurrentShipNames[timer.podIndex] = "";
			game.startTimer(new PodAnswerTimer(timer.podIndex, 4, -1), 1f);
			podActivateAnswerButtons(timer.podIndex, activate: false);
		}
		else if (timer.index == 4)
		{
			podIncorrectOverlay[timer.podIndex].SetActive(value: false);
			podDialogSetUpQuestion(timer.podIndex, currentQuestions[timer.podIndex]);
		}
		else if (timer.index == 5)
		{
			unlockPod(timer.podIndex);
		}
	}

	private void podActivateAnswerButtons(int podIndex, bool activate)
	{
		switch (podIndex)
		{
		case 0:
			activateButtons(pod3AnswerButtons);
			activateButtons(pod4AnswerButtons);
			break;
		case 1:
			activateButtons(pod3AnswerButtons1);
			activateButtons(pod4AnswerButtons1);
			break;
		case 2:
			activateButtons(pod3AnswerButtons2);
			activateButtons(pod4AnswerButtons2);
			break;
		case 3:
			activateButtons(pod3AnswerButtons3);
			activateButtons(pod4AnswerButtons3);
			break;
		}
		void activateButtons(RefArray<Switch3D, TweenState, GameObject> answerButtons)
		{
			foreach (Ref<Switch3D, TweenState, GameObject> answerButton in answerButtons)
			{
				((GameObject)answerButton).SetActive(activate);
			}
		}
	}

	private void podDialogSetUpQuestion(int podIndex, int nextQuestionIndex)
	{
		if (nextQuestionIndex >= dialogQuestions.Count)
		{
			return;
		}
		DialogQuestion dialogQuestion = dialogQuestions[nextQuestionIndex];
		string text = dialogQuestion.question;
		if (nextQuestionIndex > 3)
		{
			text = text + " " + podCurrentShipNames[podIndex] + "..";
		}
		List<string> list = new List<string>();
		for (int i = 0; i < dialogQuestion.answers.Count; i++)
		{
			string item = dialogQuestion.answers[i];
			switch (nextQuestionIndex)
			{
			case 3:
				item = dialogQuestion.answers[i] + "..";
				break;
			case 4:
				item = ".." + dialogQuestion.answers[i] + "..";
				break;
			case 5:
				item = ".." + dialogQuestion.answers[i];
				break;
			}
			list.Add(item);
		}
		podChangeScreen(podIndex, PodScreens.Answer3, text, list);
	}

	private void onPodDoor(int podIndex)
	{
		if (!podsOpenedFirstTime[podIndex])
		{
			podSwitchDusts[podIndex].Play();
			podsOpenedFirstTime[podIndex] = true;
			podChangeScreen(podIndex, PodScreens.None, null, null);
		}
		podPoses[podIndex].targetable = podSwitches[podIndex].state == Switch3DState.On;
		podPoses[podIndex].lockedInPose = podSwitches[podIndex].state != Switch3DState.On;
	}

	private void onPodMedButtonTimerDone(PodMedButtonTimer timer)
	{
		podFirstAidMaterialStates[timer.index].transitionTo("Down", 3f, (podMedButtons[timer.index].targetable && timer.goingDown) ? 0f : 1f);
		game.startTimer(new PodMedButtonTimer(timer.index, !timer.goingDown), timer.goingDown ? 0.3f : 0.7f);
	}

	private void onPodMedButton(Switch3D target, int podMedIndex)
	{
		target.targetable = false;
		currentQuestions[podMedIndex] = 1;
		podChangeScreen(podMedIndex, PodScreens.AdministeringFirstAid, null, null);
		podMedicineParticles[podMedIndex].Get<GameObject>(0f).SetActive(value: true);
		game.startTimer(new PodMedTimer(podMedIndex), 3.5f);
	}

	private void onPodMedTimerDone(PodMedTimer timer)
	{
		foreach (ParticleSystem podMedicineParticleChild in podMedicineParticleChildren)
		{
			podMedicineParticleChild.Stop();
		}
		podDialogSetUpQuestion(timer.podIndex, currentQuestions[timer.podIndex]);
		podChangeScreen(timer.podIndex, PodScreens.Answer3, dialogQuestions[currentQuestions[timer.podIndex]].question, dialogQuestions[currentQuestions[timer.podIndex]].answers);
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { 0, 1, 2, 3 })]
	private void solvePod(int podIndex)
	{
		podChangeScreen(podIndex, PodScreens.Success, null, null);
		game.startTimer(new PodAnswerTimer(podIndex, 5, -1), 1.5f);
	}

	private void unlockPod(int podIndex)
	{
		game.startSwitch(podSwitches[podIndex]);
		podSwitches[podIndex].targetable = true;
		podPoses[podIndex].lockedInPose = false;
		if (game.hasAuthority(podPoses[podIndex]))
		{
			game.updateCharacterPoseUI("%standUp%");
		}
		if (!Array.TrueForAll(podSwitches, (Switch3D s) => s.targetable))
		{
			return;
		}
		podsPuzzleSolved = true;
		game.finishPuzzle(Puzzle.Pods);
		foreach (SpawnPointToPose item in podSpawnPointsToPose)
		{
			if (item != null)
			{
				item.active = false;
			}
		}
	}

	private void initLockerKeypads()
	{
		TriangleKeypad triangleKeypad = new TriangleKeypad();
		triangleKeypad.solution = new List<int> { 0, 5, 2, 7 };
		triangleKeypad.current = new List<int> { 0, 0, 0, 0 };
		triangleKeypad.buttons = locker0Buttons;
		triangleKeypad.door = lockerDoors[0];
		triangleKeypad.zoom = lockerZooms[0];
		triangleKeypad.items = locker0Items;
		triangleKeypad.screens = new List<GameObject>();
		lockers.Add(triangleKeypad);
		Item[] items = triangleKeypad.items;
		for (int i = 0; i < items.Length; i++)
		{
			items[i].targetable = false;
		}
		TriangleKeypad triangleKeypad2 = new TriangleKeypad();
		triangleKeypad2.solution = new List<int> { 2, 7, 5, 3 };
		triangleKeypad2.current = new List<int> { 0, 0, 0, 0 };
		triangleKeypad2.buttons = locker1Buttons;
		triangleKeypad2.door = lockerDoors[1];
		triangleKeypad2.zoom = lockerZooms[1];
		triangleKeypad2.items = locker1Items;
		triangleKeypad2.screens = new List<GameObject>();
		lockers.Add(triangleKeypad2);
		items = triangleKeypad2.items;
		for (int i = 0; i < items.Length; i++)
		{
			items[i].targetable = false;
		}
		TriangleKeypad triangleKeypad3 = new TriangleKeypad();
		triangleKeypad3.solution = new List<int> { 3, 4, 2, 4 };
		triangleKeypad3.current = new List<int> { 0, 0, 0, 0 };
		triangleKeypad3.buttons = locker2Buttons;
		triangleKeypad3.door = lockerDoors[2];
		triangleKeypad3.zoom = lockerZooms[2];
		triangleKeypad3.items = locker2Items;
		triangleKeypad3.screens = new List<GameObject>();
		lockers.Add(triangleKeypad3);
		for (int j = 0; j < 3; j++)
		{
			triangleKeypad.screens.Add(lockerScreens[j]);
			triangleKeypad2.screens.Add(lockerScreens[j + 3]);
			triangleKeypad3.screens.Add(lockerScreens[j + 6]);
		}
		items = triangleKeypad3.items;
		for (int i = 0; i < items.Length; i++)
		{
			items[i].targetable = false;
		}
	}

	private void onLockerKeypad(int lockerIndex, int buttonIndex)
	{
		onKeypadButton(lockerIndex, buttonIndex);
	}

	[DebugButton(null, Tint.Default, PostClickAction.HideButton, 0, new object[] { 0, 1, 2 })]
	private void debugSolveKeypad(int index)
	{
		solveLocker(index);
	}

	private void initMedBed()
	{
		scannerPlane.SetActive(value: false);
		hologram.SetActive(value: false);
		medbedHoloBeam.SetActive(value: false);
		scannerSlidable.targetable = false;
		medBedPose.targetable = false;
		GameObject[] array = holoPoints;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(value: false);
		}
		foreach (Ref<Switch3D, GameObject> medBedBacteriaSphereButton in medBedBacteriaSphereButtons)
		{
			((GameObject)medBedBacteriaSphereButton).SetActive(value: false);
		}
		TweenState[] array2 = medBedBacteriaTweens;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].setState("Transparent");
		}
		foreach (Ref<Zoomable, GameObject, Transform> podbacteriaZoom in podbacteriaZooms)
		{
			podbacteriaZoom.Get<Zoomable>(0).targetable = false;
			podbacteriaZoom.Get<GameObject>(0f).SetActive(value: false);
		}
		foreach (Ref<Zoomable, GameObject> podBacteriaOtherZoom in podBacteriaOtherZooms)
		{
			podBacteriaOtherZoom.Get<Zoomable>(0).targetable = false;
		}
		medicineSample.targetable = false;
		medBedKeySlotBlink();
		onMedBedZoomLeave(0);
		onMedBedZoomLeave(1);
		onMedBedZoomLeave(2);
		componentsChangeScreen(ComponentScreens.InsertKey);
		bedChangeScreen(BedScreens.SystemNotOperational);
	}

	private void medBedKeySlotBlink()
	{
		medBedKeyBlinker.transitionTo("Off", 8f, isMedBedKeySlotOn ? 1f : 0f);
		isMedBedKeySlotOn = !isMedBedKeySlotOn;
		if (medBedOverrideSlot.acceptItems[0].slot == null)
		{
			game.startTimer(new MedBedBlinkTimer(), 1f);
		}
		else
		{
			medBedKeyBlinker.transitionTo("Off");
		}
	}

	private void solveMedBedKey()
	{
		medBedKeyTween.transitionTo("Rotate");
		game.startTimer(new MedBedPowerTimer(0), 1f);
	}

	[DebugButton(null, Tint.Default, PostClickAction.HideButton, 0, new object[] { })]
	private void debugGetBatteries()
	{
		Item[] acceptItems = medBedBatterySlots[0].acceptItems;
		foreach (Item item in acceptItems)
		{
			if (item.slot == null)
			{
				game.addItemToInventory(item.gameObject);
			}
		}
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void solveMedBedBatteries()
	{
		game.startTimer(new MedBedPowerTimer(-2), 0.3f);
	}

	private void onMedBedPowerTimerDone(MedBedPowerTimer timer)
	{
		Debug.Log("TIMER: " + timer.index);
		if (timer.index == 0)
		{
			componentsChangeScreen(ComponentScreens.CheckingComponents);
			game.startTimer(new MedBedPowerTimer(-1), 1f);
		}
		if (timer.index == -1)
		{
			componentsChangeScreen(ComponentScreens.AddBatteries);
			medBedBatteryTween.transitionTo("Open", 0.3f);
			game.startTimer(new MedBedPowerTimer(1), 1f);
		}
		else if (timer.index == 1)
		{
			Slot[] array = medBedBatterySlots;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].gameObject.SetActive(value: true);
			}
		}
		else if (timer.index == -2)
		{
			componentsChangeScreen(ComponentScreens.CheckingComponents);
			medBedBatteryTween.transitionTo("Close", 0.4f);
			game.startTimer(new MedBedPowerTimer(2), 1.5f);
		}
		else if (timer.index == 2)
		{
			enableComponentPuzzle();
		}
	}

	private void componentsChangeScreen(ComponentScreens screen)
	{
		Debug.Log("TIMER SCREEN: " + screen);
		for (int i = 0; i < componentStateScreens.Length; i++)
		{
			componentStateScreens[i].Get<GameObject>().SetActive(i == (int)screen);
		}
		componentStateScreens[3].Get<GameObject>().SetActive(screen == ComponentScreens.CheckingComponents || screen == ComponentScreens.WrongComponents || screen == ComponentScreens.MissingComponents || screen == ComponentScreens.SystemOperational);
		currentComponentScreen = screen;
	}

	private void addedBattery(int batteryIndex)
	{
		batteriesAdded++;
		componentBatteryCounters[batteryIndex].SetActive(value: true);
	}

	private void enableComponentPuzzle()
	{
		componentsChangeScreen(ComponentScreens.ComponentCounter);
		for (int i = 0; i < componentSlots.Length; i++)
		{
			componentSlots[i].gameObject.SetActive(value: true);
		}
	}

	private void onComponentSlot(int slotIndex)
	{
		componentCounter[slotIndex].SetActive(value: true);
		componentXs[slotIndex].SetActive(componentSlots[slotIndex].insertedItem == null);
	}

	private void onRemoveComponentFromSlot(int slotIndex)
	{
		componentCounter[slotIndex].SetActive(value: false);
		componentXs[slotIndex].SetActive(componentSlots[slotIndex].insertedItem == null);
	}

	private void onMedBedConfirmComponents()
	{
		componentsChangeScreen(ComponentScreens.CheckingComponents);
		for (int i = 0; i < componentSlotTweens.Length; i++)
		{
			componentSlots[i].targetable = false;
			if (componentSlots[i].insertedItem != null)
			{
				componentSlots[i].insertedItem.targetable = false;
			}
			componentSlotTweens[i].transitionTo("Down");
		}
		game.startTimer(new MedBedComponentsTimer(), 3f);
	}

	private void onMedBedComponentsTimerDone(MedBedComponentsTimer timer)
	{
		if (currentComponentScreen != ComponentScreens.WrongComponents && currentComponentScreen != ComponentScreens.MissingComponents)
		{
			bool flag = true;
			for (int i = 0; i < componentSlots.Length; i++)
			{
				if (!componentSlots[i].isUnlocked)
				{
					flag = false;
				}
			}
			if (flag)
			{
				game.callRPC(RPCs.MedBedComponentsSolved);
			}
			else
			{
				game.callRPC(RPCs.MedBedComponentsNotSolved);
			}
		}
		else
		{
			componentsChangeScreen(ComponentScreens.ComponentCounter);
		}
	}

	private void onComponentSlotTweenDone(int index)
	{
		bool targetable = componentSlotTweens[index].findStateByName("Down").targetWeight < 0.5f;
		componentSlots[index].targetable = targetable;
		if (componentSlots[index].insertedItem != null)
		{
			componentSlots[index].insertedItem.targetable = targetable;
		}
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void solveComponents()
	{
		if (solvedComponents)
		{
			return;
		}
		solvedComponents = true;
		for (int i = 0; i < componentSlots.Length; i++)
		{
			Slot slot = componentSlots[i];
			slot.targetable = false;
			if (slot.insertedItem != null)
			{
				slot.insertedItem.targetable = false;
			}
		}
		game.startTimer(new MedBedSolvedTimer(), 2f);
		game.finishPuzzle(Puzzle.MedBed);
	}

	private void componentsNotSolved()
	{
		bool flag = true;
		for (int i = 0; i < componentSlots.Length; i++)
		{
			if (componentSlots[i].insertedItem == null)
			{
				flag = false;
			}
		}
		componentsChangeScreen(flag ? ComponentScreens.WrongComponents : ComponentScreens.MissingComponents);
		for (int j = 0; j < componentSlotTweens.Length; j++)
		{
			componentSlotTweens[j].transitionTo("Down", 1f, 0f);
		}
		game.startTimer(new MedBedComponentsTimer(), 2f);
	}

	private void onMedBedSolvedTimer()
	{
		componentsChangeScreen(ComponentScreens.SystemOperational);
		scannerChangeScreen(ScannerScreens.ScanAPatient);
		bedChangeScreen(BedScreens.StartScan);
		scannerSlidable.targetable = false;
		Sequence[] array = medbedGateAnimations;
		foreach (Sequence sequence in array)
		{
			sequence.play(-1f, sequence.sequenceDuration);
		}
		medBedPose.targetable = true;
		PineFmod.play(scannerSlidable.soundEmitter);
		game.startTransitionGlobal(new ScannerSlidableTransition(), scannerSlidable.transform, 3f * getScannerTravelFraction(), 0f, scannerSlidable.startPoint);
	}

	private float getScannerTravelFraction()
	{
		float value = Vector3.Distance(scannerSlidable.transform.position, scannerSlidable.startPoint);
		float b = Vector3.Distance(scannerSlidable.endPoint, scannerSlidable.startPoint);
		return Mathf.InverseLerp(0f, b, value);
	}

	private void bedChangeScreen(BedScreens screen)
	{
		for (int i = 0; i < bedScreens.Length; i++)
		{
			bedScreens[i].SetActive(i == (int)screen);
		}
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void makeHolo()
	{
		float speed = 0.162f;
		scannerPlane.SetActive(value: true);
		PineFmod.play(scannerSlidable.soundEmitter);
		PineFmod.start(scanInstance);
		game.startTransitionGlobal(new ScannerSlidableTransition(), scannerSlidable.transform, 2.99f, 0f, scannerSlidable.endPoint, null, null, Interpolation.Linear);
		bedChangeScreen(BedScreens.Scanning);
		bedScanningTween.transitionTo("Play", speed);
		medBedPose.lockedInPose = true;
		medBedPose.targetable = false;
		game.startTimer(new HoloTimer(-1), 3f);
	}

	private void onHoloTimer(HoloTimer timer)
	{
		int index = timer.index;
		if (index == -1)
		{
			PineFmod.play(scannerSlidable.soundEmitter);
			game.startTransitionGlobal(new ScannerSlidableTransition(), scannerSlidable.transform, 2.99f, 0f, scannerSlidable.startPoint, null, null, Interpolation.Linear);
			game.startTimer(new HoloTimer(0), 3f);
		}
		switch (index)
		{
		case 0:
			bedChangeScreen(BedScreens.Scanned);
			game.startTimer(new HoloTimer(1), 0.6f);
			break;
		case 1:
			holoSpEnabled = true;
			if (convertedToSp || game.getPlayerCount() == 1)
			{
				game.handlePoseLeaveLocal(medBedPose);
				medbedHeadScreenTween.transitionTo("Raise");
			}
			else
			{
				medBedPose.lockedInPose = false;
				medBedPose.targetable = true;
			}
			scannerSlidable.targetable = true;
			scannerPlane.SetActive(value: false);
			PineFmod.play(scannerSlidable.soundEmitter);
			game.startTransitionGlobal(new ScannerSlidableTransition(), scannerSlidable.transform, 1.5f, 0f, Vector3.Lerp(scannerSlidable.startPoint, scannerSlidable.endPoint, 0.6f), null, null, Interpolation.Linear);
			game.startTimer(new HoloTimer(2), 0.2f);
			break;
		case 2:
			if (!convertedToSp && game.getPlayerCount() > 1)
			{
				scannerNeedsAPerson = true;
			}
			else
			{
				medbedHoloBeam.SetActive(value: true);
				hologram.SetActive(value: true);
			}
			scannerChangeScreen(ScannerScreens.StartManualScan);
			PineFmod.stop(scanInstance, FMOD.Studio.STOP_MODE.IMMEDIATE);
			break;
		}
	}

	private void onMedBedStartScan()
	{
		scannerPlane.SetActive(value: true);
		if (!convertedToSp && game.getPlayerCount() > 1)
		{
			bedChangeScreen(BedScreens.StayStill);
		}
		scannerChangeScreen(ScannerScreens.Scanning);
	}

	private void onMedBedStartMatching()
	{
		Switch3D[] array = medBedStartMatchingButton;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = false;
		}
		scannerSlidable.targetable = false;
		float num = 3f * getScannerTravelFraction();
		PineFmod.play(scannerSlidable.soundEmitter);
		game.startTransitionGlobal(new ScannerSlidableTransition(), scannerSlidable.transform, num, 0f, scannerSlidable.startPoint);
		MaterialState[] medHoloMaterialStates = MedHoloMaterialStates;
		for (int i = 0; i < medHoloMaterialStates.Length; i++)
		{
			medHoloMaterialStates[i].transitionTo("Lighten", num);
		}
		float duration = 1f / num + 1f;
		if (num < 0.1f)
		{
			duration = 0.1f;
		}
		game.startTimer(new MedBedMatchingTimer(), duration);
	}

	private void onMedBedMatchingTimerDone(MedBedMatchingTimer timer)
	{
		scannerChangeScreen(ScannerScreens.MatchingInProgress);
		foreach (Ref<Zoomable, GameObject, Transform> podbacteriaZoom in podbacteriaZooms)
		{
			((GameObject)podbacteriaZoom).SetActive(value: true);
		}
		foreach (Ref<Switch3D, GameObject> medBedBacteriaSphereButton in medBedBacteriaSphereButtons)
		{
			((GameObject)medBedBacteriaSphereButton).SetActive(value: true);
		}
		TweenState[] array = medBedBacteriaTweens;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].transitionTo("Transparent", 1f, 0f);
		}
		MaterialState[] array2 = medBedSmallSphereMSs;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].transitionTo("Transparent", 2f);
		}
	}

	private void onHoloBacteria(Switch3D target)
	{
		int num = -1;
		for (int i = 0; i < podbacteriaZooms.Length; i++)
		{
			if (target.transform.IsChildOf(podbacteriaZooms[i].Get<Transform>(0u)))
			{
				num = i;
				break;
			}
		}
		if (num < 0)
		{
			return;
		}
		int num2 = Array.IndexOf(medBedHoloBacterias, target);
		if (target.state == Switch3DState.On)
		{
			medBedHoloBacteriaCurrent[num] = num2;
			Switch3D[] array = medBedHoloBacterias;
			foreach (Switch3D switch3D in array)
			{
				if (!(switch3D == target) && !(switch3D.transform.parent != target.transform.parent) && switch3D.state == Switch3DState.On)
				{
					game.startSwitch(switch3D);
				}
			}
		}
		else if (medBedHoloBacteriaCurrent[num] == num2)
		{
			medBedHoloBacteriaCurrent[num] = -1;
		}
		int num3 = scannerBacteriaIcons.Length / 3;
		int num4 = ((num2 >= num3) ? ((num2 < num3 * 2) ? num3 : (num3 * 2)) : 0);
		for (int k = num4; k < num4 + num3; k++)
		{
			scannerBacteriaIcons[k].SetActive(k == medBedHoloBacteriaCurrent[num]);
			scannerBacteriaIcons2[k].SetActive(k == medBedHoloBacteriaCurrent[num]);
		}
	}

	private void onMedBedZoom(int index)
	{
		Switch3D[] array = medBedHoloBacterias;
		foreach (Switch3D switch3D in array)
		{
			if (switch3D.transform.IsChildOf(podbacteriaZooms[index].Get<Transform>(0u)))
			{
				switch3D.targetPriority = 1;
			}
		}
		medBedRotatables[index].targetable = true;
	}

	private void onMedBedZoomLeave(int index)
	{
		Switch3D[] array = medBedHoloBacterias;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetPriority = -1;
		}
		medBedRotatables[index].targetable = false;
	}

	private void initChipScreen()
	{
		chipCabinetDoor.targetable = false;
		chipChangeScreen(ChipScreens.ChipPuzzle);
	}

	private void chipChangeScreen(ChipScreens screen)
	{
		for (int i = 0; i < chipScreens.Length; i++)
		{
			int num = (int)(screen + 1);
			chipScreens[i].SetActive(i == num);
		}
		if (screen == ChipScreens.ChipPuzzle)
		{
			chipScreens[0].SetActive(value: true);
		}
	}

	private void playChipPieceAnimation(GameObject piece, bool up = true, bool setState = false, bool ignoreMaterial = false)
	{
		int num = chipSwapperPieces.FindIndex<GameObject>((GameObject x) => x == piece);
		if (setState)
		{
			if (num >= 0)
			{
				chipSwapperPieces[num].Get<TweenState>(0f).setState("Up", up ? 1f : 0f);
			}
			if (!ignoreMaterial)
			{
				chipMaterialState(piece, up, setState);
			}
		}
		else
		{
			if (num >= 0)
			{
				chipSwapperPieces[num].Get<TweenState>(0f).transitionToStateAdditive("Up", 8f, up ? 1f : 0f);
			}
			if (!ignoreMaterial)
			{
				chipMaterialState(piece, up, setState);
			}
		}
	}

	private void chipMaterialState(GameObject piece, bool up = true, bool setState = false)
	{
		int num = chipSwapperPieces.FindIndex<GameObject>((GameObject x) => x == piece);
		if (num >= 0)
		{
			if (setState)
			{
				chipSwapperPieces[num].Get<TweenState>(0f).transitionToStateAdditive("Hover", 1f, up ? 1f : 0f);
			}
			else
			{
				chipSwapperPieces[num].Get<TweenState>(0f).transitionToStateAdditive("Hover", 2f, up ? 1f : 0f);
			}
		}
	}

	private void onChipSwapper(Swapper.SwapperEvent swapperEvent)
	{
		if (swapperEvent.isSolved)
		{
			onChipSwap(swapperEvent.first, swapperEvent.second);
			solveChip();
		}
		else if (swapperEvent.isReset)
		{
			playChipPieceAnimation(swapperEvent.first.gameObject, up: false);
		}
		else if (swapperEvent.didSwap)
		{
			onChipSwap(swapperEvent.first, swapperEvent.second);
		}
		else
		{
			onChipSelect(swapperEvent.getCurrentSelected);
		}
		PineFmod.playOneShotSoundAttached("event:/Sound Effects/03 Interactable/Buttons & Switches/Digital_Button_03", chipSwapper.gameObject);
	}

	private void onChipSelect(SwapperPiece piece)
	{
		playChipPieceAnimation(piece.gameObject);
	}

	private void onChipSwap(SwapperPiece first, SwapperPiece second)
	{
		if (first == second)
		{
			playChipPieceAnimation(first.gameObject, up: false);
			return;
		}
		playChipPieceAnimation(first.gameObject, up: false, setState: true, ignoreMaterial: true);
		playChipPieceAnimation(first.gameObject, up: false);
		playChipPieceAnimation(second.gameObject, up: true, setState: true);
		playChipPieceAnimation(second.gameObject, up: false);
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void solveChip()
	{
		Debug.Log("chip solved");
		chipSwapper.targetable = false;
		foreach (Ref<GameObject, TweenState, SwapperPiece> chipSwapperPiece in chipSwapperPieces)
		{
			((SwapperPiece)chipSwapperPiece).targetable = false;
		}
		game.startTimer(new ChipScreensTimer(0), 0.5f);
		game.finishPuzzle(Puzzle.UtilityCloset);
	}

	private void onChipScreensTimerDone(ChipScreensTimer timer)
	{
		if (timer.index == 0)
		{
			startLoadingSound(chipScreens[0].transform.position);
			chipChangeScreen(ChipScreens.Loading);
			game.startTimer(new ChipScreensTimer(1), 1.8f);
		}
		else if (timer.index == 1)
		{
			chipChangeScreen(ChipScreens.ChipSolved);
			game.startTimer(new ChipScreensTimer(2), 1f);
		}
		else if (timer.index == 2)
		{
			chipScreen.targetable = false;
			game.increaseZoomCounter(chipScreen.gameObject);
			game.startTimer(new ChipScreensTimer(3), 1f);
		}
		else if (timer.index == 3)
		{
			chipCabinetDoor.targetable = true;
			game.startSwitch(chipCabinetDoor);
		}
	}

	private void onBacteriaScanUpdate()
	{
		bool flag = isPersonInScanner;
		isPersonInScanner = game.isAnyPlayerInteracting(medBedPose.gameObject);
		flag = isPersonInScanner != flag;
		if (canScanMedBed)
		{
			bool flag2 = false;
			int num = -1;
			for (int i = 0; i < medScannerSolutionPoints.Count; i++)
			{
				if (medScannerSolutionPoints[i] == scannerSlidable.closestSnapPointIndex)
				{
					num = (lastScannedPoint = i);
					flag2 = true;
					break;
				}
			}
			if (flag2)
			{
				Text[] array = scannerCounters;
				for (int j = 0; j < array.Length; j++)
				{
					array[j].text = Localization.lookupInDictionary("Space1_CDetecting");
				}
				medScannerTimer += Time.deltaTime;
			}
			else
			{
				Text[] array = scannerCounters;
				for (int j = 0; j < array.Length; j++)
				{
					array[j].text = bacteriaCount + "/3";
				}
				medScannerTimer = 0f;
			}
			float targetWeight = getOscillatingValue(medScannerTimer);
			TweenState[] array2 = medScannerTweens;
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j].findStateByName("Found").targetWeight = targetWeight;
			}
		}
		if (!convertedToSp && flag && scannerNeedsAPerson)
		{
			scannerChangeScreen(isPersonInScanner ? currentScannerScreen : ScannerScreens.MissingSubject);
			medScannerTimer = 0f;
		}
		float getOscillatingValue(float timer)
		{
			if (timer <= medScannerMaxTime - 1f)
			{
				return 0f;
			}
			float num2 = timer - (medScannerMaxTime - 1f);
			return Mathf.Clamp01(Mathf.Sin(MathF.PI * num2));
		}
	}

	private void setCanMedBedScan(bool enable)
	{
		canScanMedBed = enable;
	}

	private bool checkCanMedBedScan()
	{
		if (foundBacterias || (game.getPlayerCount() != 1 && !convertedToSp) || !hologram.activeInHierarchy)
		{
			if (scannerNeedsAPerson)
			{
				return isPersonInScanner;
			}
			return false;
		}
		return true;
	}

	private void scannerChangeScreen(ScannerScreens screen)
	{
		Debug.Log("CHANGE SCANNER SCREEN TO " + screen);
		int num = scannerScreenObjects.Length / 2;
		for (int i = 0; i < num; i++)
		{
			bool active = i == (int)screen;
			GameObject[] array = scannerScanPatients;
			for (int j = 0; j < array.Length; j++)
			{
				array[j].SetActive(screen == ScannerScreens.ScanAPatient);
			}
			array = scannerNotOperationals;
			for (int j = 0; j < array.Length; j++)
			{
				array[j].SetActive(screen == ScannerScreens.SystemNotOperational);
			}
			scannerScreenObjects[i].SetActive(active);
			scannerScreenObjects[i + num].SetActive(active);
		}
		Zoomable[] array2 = scannerZooms;
		for (int j = 0; j < array2.Length; j++)
		{
			array2[j].targetable = screen == ScannerScreens.MatchingInProgress;
		}
		if (screen != ScannerScreens.MissingSubject)
		{
			currentScannerScreen = screen;
		}
	}

	private void scannedBacteria(int bacteriaIndex)
	{
		if (bacteriaIndex >= 0)
		{
			bacteriaCount++;
			medScannerSolutionPoints[bacteriaIndex] = -1;
			holoPoints[bacteriaIndex].SetActive(value: true);
			if (bacteriaCount >= 3)
			{
				foundBacterias = true;
				medBedPose.lockedInPose = true;
				game.startTimer(new ScannedBacteriaTimer(0), 1f);
			}
		}
	}

	private void onScannedBacteriaTimerDone(ScannedBacteriaTimer timer)
	{
		if (timer.index == 0)
		{
			scannerPlane.SetActive(value: false);
			scannerChangeScreen(ScannerScreens.Checking);
			if (!convertedToSp && scannerNeedsAPerson)
			{
				enableMedbedHologram = true;
				scannerNeedsAPerson = false;
				medBedPose.lockedInPose = false;
				bedChangeScreen(BedScreens.StandUp);
				medBedPose.targetable = false;
				medbedHeadScreenTween.transitionTo("Raise");
			}
			game.startTimer(new ScannedBacteriaTimer(1), 1.5f);
		}
		else if (timer.index == 1)
		{
			scannerChangeScreen(ScannerScreens.WaitForMatching);
		}
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void debugSolveBacteriaScanning()
	{
		for (int i = 0; i < medScannerSolutionPoints.Count; i++)
		{
			scannedBacteria(i);
		}
	}

	private void onMedbedBacteriaSphereButtons(int buttonIndex, Switch3DEvent switchEvent)
	{
		if (switchEvent == Switch3DEvent.Start && game.hasAuthority(medBedBacteriaSphereButtons[buttonIndex].Get<GameObject>(0f)))
		{
			Vector3 origin = game.playerViewRay.origin;
			float num = Vector3.Distance(origin, medBedRotatorColliderPositions[buttonIndex].position);
			bool flag = Vector3.Distance(origin, medBedRotatorColliderPositionsOther[buttonIndex].position) < num;
			bacteriaZoomToEnter = (flag ? podbacteriaZooms[buttonIndex].Get<GameObject>(0f) : podBacteriaOtherZooms[buttonIndex].Get<GameObject>(0f));
		}
	}

	private bool checkBacteriaMatches()
	{
		for (int i = 0; i < medBedHoloBacteriaSolution.Count; i++)
		{
			if (medBedHoloBacteriaSolution[i] != medBedHoloBacteriaCurrent[i])
			{
				return false;
			}
		}
		return true;
	}

	private void onMedBedCheckBacteriaButton()
	{
		Switch3D[] array = medBedHoloBacterias;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = false;
		}
		array = medBedCheckMatchButtons;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = false;
		}
		scannerChangeScreen(ScannerScreens.CheckingMatch);
		game.startTimer(new MedBedCheckBacteriaTimer(0), 1.5f);
	}

	private void onMedBedCheckBacteriaTimer(MedBedCheckBacteriaTimer timer)
	{
		if (timer.index == 0)
		{
			if (checkBacteriaMatches())
			{
				game.callRPC(RPCs.BacteriaMatchingCorrect);
			}
			else
			{
				game.callRPC(RPCs.BacteriaMatchingWrong);
			}
		}
		else if (timer.index == 1)
		{
			Switch3D[] array = medBedHoloBacterias;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].targetable = true;
			}
			array = medBedCheckMatchButtons;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].targetable = true;
			}
			scannerChangeScreen(ScannerScreens.MatchingInProgress);
		}
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void solveBacteriaMatching()
	{
		foreach (Ref<Zoomable, GameObject, Transform> podbacteriaZoom in podbacteriaZooms)
		{
			Zoomable zoomable = podbacteriaZoom;
			zoomable.targetable = false;
			game.increaseZoomCounter(zoomable);
		}
		Rotatable[] array = medBedRotatables;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = false;
		}
		scannerChangeScreen(ScannerScreens.Completed);
		float num = 0.5f;
		medBedMixerAnimation.play(-1f, medBedMixerAnimation.sequenceDuration);
		medBedSampleAnimation.play(-1f, medBedSampleAnimation.sequenceDuration);
		game.startTimer(new BacteriaSolvedTimer(0), 1f / num);
		TweenState[] array2 = medBedBacteriaTweens;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].transitionTo("Transparent", num);
		}
		Zoomable[] array3 = scannerZooms;
		foreach (Zoomable interactive in array3)
		{
			game.increaseZoomCounter(interactive);
		}
		game.finishPuzzle(Puzzle.BacteriaMatch);
	}

	private void bacteriaMatchingNotSolved()
	{
		scannerChangeScreen(ScannerScreens.BacteriaNotMatching);
		game.startTimer(new MedBedCheckBacteriaTimer(1), 1f);
	}

	private void onBacteriaSolvedTimerDone(BacteriaSolvedTimer timer)
	{
		if (timer.index == 0)
		{
			sampleProvided = true;
			medicineSample.targetable = true;
			foreach (Ref<Zoomable, GameObject, Transform> podbacteriaZoom in podbacteriaZooms)
			{
				((GameObject)podbacteriaZoom).SetActive(value: false);
			}
			GameObject[] array = holoPoints;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetActive(value: false);
			}
			game.startTimer(new BacteriaSolvedTimer(1), 2f);
		}
		else if (timer.index == 1)
		{
			hologram.SetActive(value: false);
			medbedHoloBeam.SetActive(value: false);
			medBedPose.targetable = true;
			medBedPose.lockedInPose = false;
		}
	}

	private void onMedBedEnableHoloTimerDone(MedBedEnableHoloTimer medBedEnableHoloTimer)
	{
		medbedHoloBeam.SetActive(value: true);
		hologram.SetActive(value: true);
	}

	private void initMedicine()
	{
		medicineChangeScreen(MedicineScreens.InsertDiseaseSample);
		medicineSampleTween.transitionTo("Open", 0.5f);
		sampleSlot.gameObject.SetActive(value: true);
		medicineZoom.targetable = false;
		for (int i = 0; i < medicineSwapperPiecesHighlights.Length; i++)
		{
			medicineSwapperPiecesHoverHighlights[i].enabled = true;
			medicineSwapperPiecesHighlights[i].enabled = false;
		}
		for (int j = 0; j < medicineEmptyPieces.Length; j++)
		{
			medicineSwapperEmptyHighlights[j].enabled = false;
			medicineEmptyPieces[j].targetable = false;
		}
		podMedicineParticleChildren = new List<ParticleSystem>();
		foreach (Ref<ParticleSystem, GameObject> podMedicineParticle in podMedicineParticles)
		{
			podMedicineParticleChildren.AddRange(podMedicineParticle.Get<GameObject>(0f).GetComponentsInChildren<ParticleSystem>());
		}
		medicineParticleChildren = new List<ParticleSystem>();
		medicineParticleChildren.AddRange(bacteriaMedicineParticles.GetComponentsInChildren<ParticleSystem>());
		medicineParticleChildren.AddRange(desinfectionParticles.GetComponentsInChildren<ParticleSystem>());
	}

	[DebugButton(null, Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void onSampleSlot()
	{
		medicineZoom.targetable = true;
		medicineSample.targetable = false;
		medicineSampleTween.transitionTo("Close", 0.5f);
	}

	private void onMedicineSampleTweenDone(string state)
	{
		if (!(state != "Close"))
		{
			medicineChangeScreen(MedicineScreens.PuzzleScreen);
		}
	}

	private void onMedicineSwapper(Swapper.SwapperEvent swapperEvent)
	{
		if (swapperEvent.didSwap || swapperEvent.isReset)
		{
			onMedicineSwap(swapperEvent.first, swapperEvent.second);
		}
		else
		{
			onMedicineSelect(swapperEvent.getCurrentSelected);
		}
	}

	private void onMedicineSwap(SwapperPiece first, SwapperPiece second)
	{
		for (int i = 0; i < medicineEmptyPieces.Length; i++)
		{
			medicineSwapperEmptyHighlights[i].enabled = false;
			medicineEmptyPieces[i].targetable = false;
		}
		for (int j = 0; j < medicineSwapperPieces.Length; j++)
		{
			medicineSwapperPiecesHighlights[j].enabled = false;
			medicineSwapperPiecesHoverHighlights[j].enabled = true;
			medicineSwapperPieces[j].targetable = true;
		}
	}

	private void onMedicineSelect(SwapperPiece selected)
	{
		for (int i = 0; i < medicineEmptyPieces.Length; i++)
		{
			medicineSwapperEmptyHighlights[i].enabled = true;
			medicineEmptyPieces[i].targetable = true;
		}
		for (int j = 0; j < medicineSwapperPieces.Length; j++)
		{
			medicineSwapperPiecesHighlights[j].enabled = medicineSwapperPieces[j] == selected;
		}
	}

	private bool checkMedicineSolved()
	{
		for (int i = 0; i < medicineEmptyPieces.Length; i++)
		{
			if (medicineSolution[i].swappedPiece != medicineEmptyPieces[i])
			{
				return false;
			}
		}
		return true;
	}

	private bool hasTentaclesLeft()
	{
		int[] array = medicineTentaclesLeft;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] > 0)
			{
				return false;
			}
		}
		return true;
	}

	private void checkTentacles()
	{
		medicineTentaclesLeft = new int[3] { 2, 1, 2 };
		int i;
		for (i = 0; i < medicineSwapperPieces.Length; i++)
		{
			if (!medicineEmptyPieces.Any((SwapperPiece x) => x == medicineSwapperPieces[i].swappedPiece))
			{
				continue;
			}
			char key = medicineSwapperPieces[i].name[0];
			if (!medicineChart.ContainsKey(key))
			{
				continue;
			}
			foreach (int item in medicineChart[key])
			{
				if (item < 3 && medicineTentaclesLeft[item] > 0)
				{
					medicineTentaclesLeft[item]--;
				}
			}
		}
	}

	private void medicineChangeScreen(MedicineScreens screen)
	{
		for (int i = 0; i < medicineScreens.Length; i++)
		{
			medicineScreens[i].SetActive(i == (int)screen);
		}
	}

	private void onMedicineCreateSubstanceButton()
	{
		game.startTimer(new MedicinePuzzleScreenTimer(1), 1.5f);
		medicineChangeScreen(MedicineScreens.Checking);
	}

	private void onMedicinePuzzleScreenTimer(MedicinePuzzleScreenTimer timer)
	{
		if (timer.index == 1)
		{
			if (checkMedicineSolved())
			{
				game.callRPC(RPCs.MedicineSolved);
			}
			else
			{
				game.callRPC(RPCs.MedicineNotSolved);
			}
		}
		else if (timer.index < 10)
		{
			bool flag = timer.index % 2 == 0;
			medicineRedTentacles[0].SetActive(flag && medicineTentaclesLeft[0] > 0);
			medicineRedTentacles[1].SetActive(flag && medicineTentaclesLeft[0] > 1);
			medicineRedTentacles[2].SetActive(flag && medicineTentaclesLeft[1] > 0);
			medicineRedTentacles[3].SetActive(flag && medicineTentaclesLeft[2] > 0);
			medicineRedTentacles[4].SetActive(flag && medicineTentaclesLeft[2] > 1);
			game.startTimer(new MedicinePuzzleScreenTimer(timer.index + 1), flag ? 0.3f : 0.3f);
		}
		else if (timer.index == 10)
		{
			medicineChangeScreen(MedicineScreens.PuzzleScreen);
		}
	}

	private void medicineNotSolved()
	{
		checkTentacles();
		if (hasTentaclesLeft())
		{
			game.callRPC(RPCs.HasTentacles);
		}
		else
		{
			game.callRPC(RPCs.NoTentacles);
		}
	}

	private void onHasTentaclesLeft()
	{
		medicineChangeScreen(MedicineScreens.IncorrectConnections);
		game.startTimer(new MedicinePuzzleScreenTimer(10), 3.2f);
	}

	private void onNoTentaclesLeft()
	{
		medicineChangeScreen(MedicineScreens.BacteriaOverlay);
		game.startTimer(new MedicinePuzzleScreenTimer(2), 0.5f);
		for (int i = 0; i < medicineRedTentacles.Length; i++)
		{
			medicineRedTentacles[i].SetActive(value: false);
		}
		medicineTentacles[0].SetActive(medicineTentaclesLeft[0] > 0);
		medicineTentacles[1].SetActive(medicineTentaclesLeft[0] > 1);
		medicineTentacles[2].SetActive(medicineTentaclesLeft[1] > 0);
		medicineTentacles[3].SetActive(medicineTentaclesLeft[2] > 0);
		medicineTentacles[4].SetActive(medicineTentaclesLeft[2] > 1);
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void releaseMedicine()
	{
		medicineChangeScreen(MedicineScreens.DispensingCure);
		float num = 5f;
		dispensingCureTween.transitionTo("Loading", 1f / num);
		medicineZoom.targetable = false;
		game.increaseZoomCounter(medicineZoom.gameObject);
		bacteriaMedicineParticles.SetActive(value: true);
		desinfectionParticles.SetActive(value: true);
		game.startTimer(new ReleaseMedicineTimer(), num);
	}

	private void onReleaseMedicineTimerDone(ReleaseMedicineTimer timer)
	{
		medicineChangeScreen(MedicineScreens.CureDispensed);
		foreach (ParticleSystem medicineParticleChild in medicineParticleChildren)
		{
			medicineParticleChild.Stop();
		}
		releasedMedicine = true;
	}

	private void initAirlock()
	{
		changeAirlockScreen(AirlockScreens.StartScan);
		corridorScanField.SetActive(value: false);
		airlockKeypadSetNumbers();
		game.startTimer(new AirlockKeypadTimer(-1), 0.4f);
		if (game.getPlayerCount() > 1)
		{
			corridorScanButton.targetable = false;
			airlockAccessDeniedMpOverlay.SetActive(value: true);
		}
	}

	private void onAirlockSlot(int index)
	{
		screwdriver.hasRigidbody = false;
		screwdriver.targetable = false;
		screwSlots[index].Get<Slot>(0).targetable = false;
		screwSlots[index].Get<TweenState>(0f).transitionTo("Rotate");
		screwTweens[index].Get<TweenState>(0).transitionTo("Rotate");
		game.startTimer(new AirlockSlotTimerTimer(0, index), 1f);
	}

	private void onAirlockSlotTimer(AirlockSlotTimerTimer timer)
	{
		int index = timer.index;
		int screwIndex = timer.screwIndex;
		switch (index)
		{
		case 0:
			screwdriver.targetable = true;
			game.startTimer(new AirlockSlotTimerTimer(1, screwIndex), 0.15f);
			break;
		case 1:
		{
			if (game.hasAuthority(screwdriver))
			{
				game.transitionItemToInventory(screws[screwIndex].gameObject);
			}
			screws[screwIndex].targetable = true;
			screwSlots[screwIndex].Get<GameObject>(0u).SetActive(value: false);
			bool flag = true;
			foreach (Ref<Slot, TweenState, GameObject> screwSlot in screwSlots)
			{
				if (screwSlot.Get<GameObject>(0u).activeSelf)
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				openAirlockKeypad();
			}
			break;
		}
		}
	}

	private void openAirlockKeypad()
	{
		airlockKeypadClosure.targetable = true;
		airlockKeypadClosure.tweenState.transitionTo("Down", airlockKeypadClosure.transitionSpeed / 5f);
		airlockKeypadClosure.tweenState.findStateByName("Down").targetWeight = 0.2f;
	}

	private void onAirlockKeypad(int index)
	{
		_ = airlockKeypadButtons[index];
		switch (index)
		{
		case 10:
			if (airlockKeypadCurrentSolution.Count > 0)
			{
				airlockKeypadCurrentSolution.RemoveAt(airlockKeypadCurrentSolution.Count - 1);
			}
			airlockKeypadSetNumbers();
			return;
		case 11:
			changeAirlockKeypadScreen(AirlockKeypadScreen.Checking);
			game.startTimer(new AirlockKeypadTimer(0), 1.5f);
			return;
		}
		int num = ((airlockKeypadSwitches[index].state == Switch3DState.On) ? airlockProgramming2[index] : airlockProgramming1[index]);
		if (airlockKeypadCurrentSolution.Count < airlockKeypadSolution.Count && num != -1)
		{
			airlockKeypadCurrentSolution.Add(num);
			airlockKeypadSetNumbers();
		}
	}

	private void airlockKeypadSetNumbers()
	{
		enterNumber(airlockNumbers0, 0);
		enterNumber(airlockNumbers1, 1);
		enterNumber(airlockNumbers2, 2);
		enterNumber(airlockNumbers3, 3);
		enterNumber(airlockNumbers4, 4);
		void enterNumber(GameObject[] numbers, int solutionIndex)
		{
			int num = -1;
			if (airlockKeypadCurrentSolution.Count > solutionIndex)
			{
				num = airlockKeypadCurrentSolution[solutionIndex];
			}
			for (int i = 0; i < numbers.Length; i++)
			{
				numbers[i].SetActive(i == num);
			}
		}
	}

	private void onAirlockKeypadOff(Switch3D button)
	{
		button.targetable = true;
	}

	private bool checkAirlockKeypadSolution()
	{
		bool flag = airlockKeypadSolution.Count == airlockKeypadCurrentSolution.Count;
		for (int i = 0; i < airlockKeypadSolution.Count; i++)
		{
			if (!flag)
			{
				return flag;
			}
			flag = airlockKeypadSolution[i] == airlockKeypadCurrentSolution[i];
		}
		return flag;
	}

	private void airlockKeypadCorrect()
	{
		changeAirlockKeypadScreen(AirlockKeypadScreen.Solved);
		game.startTimer(new AirlockKeypadTimer(1), 0.5f);
	}

	private void airlockKeypadWrong()
	{
		changeAirlockKeypadScreen(AirlockKeypadScreen.Error);
		game.startTimer(new AirlockKeypadTimer(2), 0.5f);
	}

	private void onAirlockKeypadTimerDone(AirlockKeypadTimer timer)
	{
		if (timer.index == -1 || timer.index == -2)
		{
			int count = airlockKeypadCurrentSolution.Count;
			for (int i = 0; i < airlockUnderlines.Length; i++)
			{
				if (i < count || i > count)
				{
					airlockUnderlines[i].setState("Invisible", 0f);
				}
				else
				{
					airlockUnderlines[i].transitionTo("Invisible", 7f, (timer.index == -1) ? 1f : 0f);
				}
			}
			game.startTimer(new AirlockKeypadTimer((timer.index == -1) ? (-2) : (-1)), 0.4f);
		}
		else if (timer.index == 0)
		{
			if (checkAirlockKeypadSolution())
			{
				game.callRPC(RPCs.AirlockKeypadCorrect);
			}
			else
			{
				game.callRPC(RPCs.AirlockKeypadWrong);
			}
		}
		else if (timer.index == 1)
		{
			solveAirlockKeypad();
		}
		else if (timer.index == 2)
		{
			changeAirlockKeypadScreen(AirlockKeypadScreen.None);
			airlockKeypadCurrentSolution.Clear();
			airlockKeypadSetNumbers();
			game.startTimer(new AirlockKeypadTimer(3), 0.5f);
		}
		else if (timer.index == 3)
		{
			changeAirlockKeypadScreen(AirlockKeypadScreen.Error);
			game.startTimer(new AirlockKeypadTimer(4), 0.5f);
		}
		else if (timer.index == 4)
		{
			changeAirlockKeypadScreen(AirlockKeypadScreen.Numbers);
		}
	}

	private void changeAirlockKeypadScreen(AirlockKeypadScreen screen)
	{
		for (int i = 0; i < airlockScreens.Length; i++)
		{
			airlockScreens[i].SetActive(i == (int)screen);
		}
	}

	private void onAirlockTrigger(TriggerEvent triggerEvent)
	{
		int playerCount = game.getPlayerCount();
		if (captainsRoomObstacle.activeSelf)
		{
			bool flag = triggerEvent.playersInTrigger.Count == playerCount;
			Debug.Log($"airlock trigger: {triggerEvent.playersInTrigger.Count} + {flag}");
			corridorScanButton.targetable = flag;
			airlockAccessDeniedMpOverlay.SetActive(!flag && playerCount > 1);
		}
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void solveAirlockKeypad()
	{
		Switch3D[] array = airlockKeypadButtons;
		foreach (Switch3D switch3D in array)
		{
			if (switch3D != null)
			{
				switch3D.targetable = false;
			}
		}
		game.startSwitch(airLockDoors[0]);
		airLockDoorDust[0].Play();
		corridorObstacle.SetActive(value: false);
		airlockKeypadZoom.targetable = false;
		game.increaseZoomCounter(airlockKeypadZoom.gameObject);
		game.finishPuzzle(Puzzle.BrokenKeypad);
	}

	private void changeAirlockScreen(AirlockScreens screen)
	{
		for (int i = 0; i < airlockTexts.Length; i++)
		{
			airlockTexts[i].SetActive(i == (int)screen);
		}
	}

	private void onCorridorScanButton(Switch3DEvent switchEvent)
	{
		switch (switchEvent)
		{
		case Switch3DEvent.Start:
			podRoomObstacle.SetActive(value: true);
			break;
		case Switch3DEvent.On:
			startCorridorScan();
			break;
		}
	}

	private void startCorridorScan()
	{
		airLockDoors[0].tweenState.transitionTo("Default", airLockDoors[0].transitionSpeed);
		corridorScanButton.targetable = false;
		changeAirlockScreen(AirlockScreens.Scanning);
		solvedMedicineBeforeScanning = releasedMedicine;
		corridorScanField.SetActive(value: true);
		game.startTimer(new CorridorScanTimer(0), 1f / airLockDoors[0].transitionSpeed);
	}

	private void onCorridorScanTimer(CorridorScanTimer timer)
	{
		if (timer.index == 0)
		{
			corridorScanFieldSequence.play(-1f, corridorScanFieldSequence.sequenceDuration);
			corridorLoadingTween.setState("Loading", 0f);
			corridorLoadingTween.transitionTo("Loading", 0.2f);
		}
		else if (timer.index == 1)
		{
			changeAirlockScreen(AirlockScreens.StartScan);
		}
	}

	private void onCorridorScanSequence()
	{
		if (corridorScanFieldSequence.targetSequenceTime != 0f)
		{
			corridorScanFieldSequence.play(-1f, 0f);
		}
		else if (solvedMedicineBeforeScanning)
		{
			game.callRPC(RPCs.SolvedMedicineBeforeScanning);
		}
		else
		{
			game.callRPC(RPCs.NotSolvedMedicineBeforeScanning);
		}
	}

	private void onSolvedMedicineBeforeScanning()
	{
		airLockDoors[0].tweenState.transitionTo("Down", airLockDoors[0].transitionSpeed);
		corridorScanField.SetActive(value: false);
		changeAirlockScreen(AirlockScreens.AccessGranted);
		if (airLockDoors[1].state == Switch3DState.Off)
		{
			game.startSwitch(airLockDoors[1]);
			airLockDoorDust[1].Play();
		}
		captainsRoomObstacle.SetActive(value: false);
		airlockTrigger.Get<GameObject>(0).SetActive(value: false);
		airlockAccessDeniedMpOverlay.SetActive(value: false);
		MaterialState[] array = corridorLightsMaterialStates;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].transitionTo("Unlocked", 5f);
		}
		Light[] array2 = corridorLights;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].color = Color.green;
		}
		game.finishPuzzle(Puzzle.TheCure);
		PineFmod.playOneShotSoundAttached("event:/Sound Effects/03 Interactable/Buttons & Switches/Airlock_Test_Buttons/Airlock_Pass_Test", airlockTexts[0].gameObject);
		podRoomObstacle.SetActive(value: false);
	}

	private void onNotSolvedMedicineBeforeScanning()
	{
		airLockDoors[0].tweenState.transitionTo("Down", airLockDoors[0].transitionSpeed);
		corridorScanField.SetActive(value: false);
		changeAirlockScreen(AirlockScreens.InfectionDetected);
		corridorScanButton.targetable = true;
		if (corridorScanButton.state == Switch3DState.On)
		{
			game.startSwitch(corridorScanButton);
		}
		game.startTimer(new CorridorScanTimer(1), 3f);
		PineFmod.playOneShotSoundAttached("event:/Sound Effects/03 Interactable/Buttons & Switches/Airlock_Test_Buttons/Airlock_Fail_Test", airlockTexts[0].gameObject);
		podRoomObstacle.SetActive(value: false);
	}

	private void initScrambledCode()
	{
		scrambleCurrentImages = new List<int>();
		int collectionCounter = 0;
		initPattern(ScramblerPattern1Collection);
		initPattern(ScramblerPattern2Collection);
		initPattern(ScramblerPattern3Collection);
		initPattern(ScramblerPattern0Collection);
		initPattern(ScramblerPattern4Collection);
		game.increaseZoomCounter(scramblerZoom.gameObject);
		void initPattern(RefArray<MeshFilter, GameObject, Transform> collection)
		{
			scrambleCurrentImages.Add(0);
			scramblerChangeType(collectionCounter, collectionCounter + 1);
			collectionCounter++;
		}
	}

	private RefArray<MeshFilter, GameObject, Transform> scramblerPatterCollection(int index)
	{
		return index switch
		{
			0 => ScramblerPattern1Collection, 
			1 => ScramblerPattern2Collection, 
			2 => ScramblerPattern3Collection, 
			3 => ScramblerPattern0Collection, 
			4 => ScramblerPattern4Collection, 
			_ => null, 
		};
	}

	private void onScramblerButton(int index)
	{
		int num = index % 3;
		int num2 = (index - num) / 3;
		switch (num)
		{
		case 0:
			scramblerChangeType(num2, -1);
			return;
		case 1:
			scramblerChangeType(num2, 1);
			return;
		}
		foreach (Ref<MeshFilter, GameObject, Transform> item in scramblerPatterCollection(num2))
		{
			item.Get<Transform>(0u).Rotate(Vector3.forward * 90f);
		}
	}

	private void scramblerChangeType(int collection, int change)
	{
		int num = (scrambleCurrentImages[collection] + change + scramblerPatternMeshes.Length) % scramblerPatternMeshes.Length;
		scrambleCurrentImages[collection] = num;
		Mesh sharedMesh = scramblerPatternMeshes[num].Get<MeshFilter>().sharedMesh;
		foreach (Ref<MeshFilter, GameObject, Transform> item in scramblerPatterCollection(collection))
		{
			item.Get<MeshFilter>(0).sharedMesh = sharedMesh;
		}
	}

	private void onCaptainsButtons(int index)
	{
		currentCaptains.RemoveAt(0);
		currentCaptains.Add(index);
	}

	private bool checkCaptainsKeypad()
	{
		for (int i = 0; i < captainsSolution.Count; i++)
		{
			if (captainsSolution[i] != currentCaptains[i])
			{
				return false;
			}
		}
		return true;
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void solveCaptainsKeypad()
	{
		Switch3D[] array = captainsPodButtons;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = false;
		}
		game.startTimer(new CaptainsPodTimer(0), 0.8f);
		game.finishPuzzle(Puzzle.CaptainsPod);
	}

	private void onCaptainsPodTimerDone(CaptainsPodTimer timer)
	{
		if (timer.index == 0)
		{
			changeCaptainsScreen(1);
			game.startTimer(new CaptainsPodTimer(1), 1f);
		}
		else if (timer.index == 1)
		{
			captainsZoom.targetable = false;
			game.increaseZoomCounter(captainsZoom);
			changeCaptainsScreen(2);
			game.startTimer(new CaptainsPodTimer(2), 1f);
		}
		else if (timer.index == 2)
		{
			if (captainsPod.state == Switch3DState.Off)
			{
				game.startSwitch(captainsPod);
				captainsPodDust.Play();
			}
			captainsPod.targetable = true;
		}
	}

	private void changeCaptainsScreen(int index)
	{
		for (int i = 0; i < captainsScreens.Length; i++)
		{
			captainsScreens[i].SetActive(i == index);
		}
	}

	private void onCaptainsPodSwitch(Switch3DEvent switchEvent)
	{
		if (switchEvent == Switch3DEvent.Start)
		{
			float weight = ((captainsPod.state != Switch3DState.On) ? 1f : 0f);
			captainsScreenAbove.transitionTo("Down", captainsPod.transitionSpeed, weight);
		}
	}

	[DebugButton(null, Tint.Default, PostClickAction.HideButton, 0, new object[] { })]
	private void debugOpenDoors()
	{
		Switch3D[] array = airLockDoors;
		foreach (Switch3D switch3D in array)
		{
			game.startSwitch(switch3D);
		}
		captainsRoomObstacle.SetActive(value: false);
		corridorObstacle.SetActive(value: false);
	}

	private void initCrate()
	{
		cratePuzzleSegments = new List<CratePuzzleSegment>();
		for (int i = 0; i < crateSegmentObjects.Length; i++)
		{
			cratePuzzleSegments.Add(new CratePuzzleSegment
			{
				segment = crateSegmentObjects[i],
				segmentTransform = crateSegmentObjects[i].transform,
				row = ((i >= 3) ? 1 : 0),
				column = i % 3,
				rotation = 0f,
				startingLocalPosition = crateSegmentObjects[i].transform.localPosition,
				startingLocalRotation = crateSegmentObjects[i].transform.localRotation,
				startingRow = ((i >= 3) ? 1 : 0),
				startingColumn = i % 3
			});
		}
		currentCratePuzzleInstructions = new List<CratePuzzleInstruction>();
		crateUndo.targetable = false;
		audioSplicer.targetable = false;
		changeCrateScreen(0);
	}

	private void onCrateSwitch3D(Switch3D target, Switch3DEvent switchEvent)
	{
		if (target == crateUndo && switchEvent == Switch3DEvent.Start)
		{
			onCrateUndo();
		}
	}

	private void onCrateSwapper(Swapper.SwapperEvent swapperEvent)
	{
		if (swapperEvent.isReset)
		{
			onCrateReset();
		}
		else if (swapperEvent.didSwap)
		{
			onCrateSwapperSwap(swapperEvent.first, swapperEvent.second);
		}
		else
		{
			onCrateSwapperSelected(swapperEvent.getCurrentSelected);
		}
	}

	private void onCrateSwapperSelected(SwapperPiece piece)
	{
		if (crateFirstSelected != null)
		{
			return;
		}
		crateFirstSelected = piece;
		float speed = 2f;
		int num = Array.IndexOf(crateSwaps, piece);
		crateSwapsTweens[num].transitionTo("Down", speed);
		int i;
		for (i = 0; i < crateSwaps.Length; i++)
		{
			if (i != num)
			{
				bool flag = currentCratePuzzleInstructions.Exists((CratePuzzleInstruction x) => x.piece == crateSwaps[i]);
				crateSwapsTweens[i].transitionTo(flag ? "Inactive" : "Default", speed);
				crateSwaps[i].targetable = false;
			}
		}
		int i2;
		for (i2 = 0; i2 < crateEmptySwaps.Length; i2++)
		{
			bool flag2 = currentCratePuzzleInstructions.Exists((CratePuzzleInstruction x) => x.swappedEmpty == crateEmptySwaps[i2]);
			crateBordersTweens[i2].transitionTo(flag2 ? "Inactive" : "Down", speed);
			crateEmptySwaps[i2].targetable = !flag2;
		}
	}

	private void onCrateReset()
	{
		crateHighlightInstructions();
		crateFirstSelected = null;
	}

	private void onCrateSwapperSwap(SwapperPiece first, SwapperPiece second)
	{
		crateUndo.targetable = true;
		crateFirstSelected = null;
		int num = Array.IndexOf(crateSwaps, first);
		int num2 = Array.IndexOf(crateEmptySwaps, second);
		bool flag = true;
		bool flag2 = num == 3;
		CratePuzzleInstruction obj = new CratePuzzleInstruction
		{
			piece = first,
			swappedEmpty = second
		};
		int row;
		switch (num2)
		{
		default:
			row = -1;
			break;
		case 4:
		case 8:
			row = 1;
			break;
		case 3:
		case 9:
			row = 0;
			break;
		}
		obj.row = row;
		int column;
		switch (num2)
		{
		default:
			column = -1;
			break;
		case 2:
		case 5:
			column = 2;
			break;
		case 1:
		case 6:
			column = 1;
			break;
		case 0:
		case 7:
			column = 0;
			break;
		}
		obj.column = column;
		obj.move = (flag ? ((num2 <= 7 && num2 >= 3) ? (-1) : ((num2 < 3 || num2 == 8 || num2 == 9) ? 1 : 0)) : 0);
		obj.rotate = ((!flag) ? (flag2 ? 1 : (-1)) : 0);
		CratePuzzleInstruction cratePuzzleInstruction = obj;
		currentCratePuzzleInstructions.Add(cratePuzzleInstruction);
		crateRunInstruction(cratePuzzleInstruction);
		cratePuzzleInstruction.piece.targetable = false;
		cratePuzzleInstruction.swappedEmpty.targetable = false;
		Quaternion localRotation = first.transform.localRotation;
		first.transform.localRotation = second.transform.localRotation;
		second.transform.localRotation = localRotation;
		crateHighlightInstructions();
	}

	private void crateHighlightInstructions()
	{
		crateUndo.targetable = currentCratePuzzleInstructions.Count > 0;
		int i;
		for (i = 0; i < crateSwaps.Length; i++)
		{
			bool flag = currentCratePuzzleInstructions.Exists((CratePuzzleInstruction x) => x.piece == crateSwaps[i]);
			crateSwapsTweens[i].transitionTo(flag ? "Inactive" : "Default", 2f);
			crateSwaps[i].targetable = !flag;
		}
		int i2;
		for (i2 = 0; i2 < crateEmptySwaps.Length; i2++)
		{
			bool flag2 = currentCratePuzzleInstructions.Exists((CratePuzzleInstruction x) => x.swappedEmpty == crateEmptySwaps[i2]);
			crateBordersTweens[i2].transitionTo(flag2 ? "Inactive" : "Default", 2f);
			crateEmptySwaps[i2].targetable = false;
		}
	}

	private void onCrateUndo()
	{
		int num = currentCratePuzzleInstructions.Count - 1;
		if (num >= 0 && num < currentCratePuzzleInstructions.Count)
		{
			crateSwapper.firstPiece = null;
			onCrateReset();
			CratePuzzleInstruction cratePuzzleInstruction = currentCratePuzzleInstructions[num];
			currentCratePuzzleInstructions.RemoveAt(num);
			crateRunInstruction(cratePuzzleInstruction, reverse: true);
			Transform obj = cratePuzzleInstruction.piece.transform;
			Transform transform = cratePuzzleInstruction.swappedEmpty.transform;
			Vector3 position = cratePuzzleInstruction.swappedEmpty.transform.position;
			Vector3 position2 = cratePuzzleInstruction.piece.transform.position;
			Vector3 vector = (obj.position = position);
			vector = (transform.position = position2);
			Transform obj2 = cratePuzzleInstruction.piece.transform;
			transform = cratePuzzleInstruction.swappedEmpty.transform;
			Quaternion rotation = cratePuzzleInstruction.swappedEmpty.transform.rotation;
			Quaternion rotation2 = cratePuzzleInstruction.piece.transform.rotation;
			Quaternion quaternion = (obj2.rotation = rotation);
			quaternion = (transform.rotation = rotation2);
			SwapperPiece swapperPiece = ((cratePuzzleInstruction.piece.swappedPiece != null) ? cratePuzzleInstruction.piece.swappedPiece : cratePuzzleInstruction.piece);
			SwapperPiece swapperPiece2 = ((cratePuzzleInstruction.swappedEmpty.swappedPiece != null) ? cratePuzzleInstruction.swappedEmpty.swappedPiece : cratePuzzleInstruction.swappedEmpty);
			cratePuzzleInstruction.swappedEmpty.swappedPiece = ((cratePuzzleInstruction.swappedEmpty != swapperPiece) ? swapperPiece : null);
			cratePuzzleInstruction.piece.swappedPiece = ((cratePuzzleInstruction.piece != swapperPiece2) ? swapperPiece2 : null);
			cratePuzzleInstruction.piece.targetable = true;
			cratePuzzleInstruction.swappedEmpty.targetable = true;
			if (currentCratePuzzleInstructions.Count == 0)
			{
				crateUndo.targetable = false;
			}
			crateHighlightInstructions();
		}
	}

	private void crateRunInstruction(CratePuzzleInstruction instruction, bool reverse = false)
	{
		int num = instruction.move;
		int num2 = instruction.rotate;
		if (reverse)
		{
			num *= -1;
			num2 *= -1;
		}
		for (int i = 0; i < cratePuzzleSegments.Count; i++)
		{
			if (instruction.row == cratePuzzleSegments[i].row)
			{
				cratePuzzleSegments[i].column = (cratePuzzleSegments[i].column + num + 3) % 3;
				cratePuzzleSegments[i].rotation = (cratePuzzleSegments[i].rotation + (float)num2 + 4f) % 4f;
			}
			if (instruction.column == cratePuzzleSegments[i].column)
			{
				cratePuzzleSegments[i].row = (cratePuzzleSegments[i].row + num + 2) % 2;
				cratePuzzleSegments[i].rotation = (cratePuzzleSegments[i].rotation + (float)num2 + 4f) % 4f;
			}
			moveSegment(cratePuzzleSegments[i]);
			rotateSegment(cratePuzzleSegments[i]);
		}
		void moveSegment(CratePuzzleSegment segment)
		{
			CratePuzzleSegment cratePuzzleSegment = cratePuzzleSegments.Find((CratePuzzleSegment x) => x.startingRow == segment.row && x.startingColumn == segment.column);
			if (cratePuzzleSegment == null)
			{
				Debug.LogError($"Destination Segment is null for: {segment.segment.name} -> {segment.row}, {segment.column}");
			}
			else
			{
				segment.segmentTransform.localPosition = cratePuzzleSegment.startingLocalPosition;
			}
		}
		static void rotateSegment(CratePuzzleSegment segment)
		{
			segment.segmentTransform.localRotation = segment.startingLocalRotation * Quaternion.Euler(90f * segment.rotation * Vector3.forward);
		}
	}

	private bool checkCrateSolved()
	{
		for (int i = 0; i < crateSegmentSolutions.Length; i++)
		{
			if (Vector3.Distance(crateSegmentSolutions[i].position, crateSegmentObjects[i].transform.position) > 0.01f)
			{
				return false;
			}
			float num = 5f;
			bool flag = Quaternion.Angle(crateSegmentSolutions[i].rotation, crateSegmentObjects[i].transform.rotation) <= num;
			if (i == 2 && !flag)
			{
				flag = Quaternion.Angle(Quaternion.AngleAxis(180f, crateSegmentObjects[i].transform.forward) * crateSegmentObjects[i].transform.rotation, crateSegmentSolutions[i].rotation) <= num;
			}
			if (!flag)
			{
				return false;
			}
		}
		return true;
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void solveCrate()
	{
		if (!isCrateSolved)
		{
			isCrateSolved = true;
			SwapperPiece[] array = crateSwaps;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].targetable = false;
			}
			array = crateEmptySwaps;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].targetable = false;
			}
			crateUndo.targetable = false;
			game.startTimer(new SolveCrateTimer(0), 1f);
			game.finishPuzzle(Puzzle.Crates);
		}
	}

	private void onSolveCrateTimer(SolveCrateTimer timer)
	{
		switch (timer.index)
		{
		case 0:
			changeCrateScreen(2);
			crateItem.Get<TweenState>(0f).transitionTo("Unzoom", 0.8f);
			game.startTimer(new SolveCrateTimer(1), 0.5f);
			break;
		case 1:
			audioSplicer.targetable = true;
			crateLid.transitionTo("Open");
			crateZoom.targetable = false;
			game.increaseZoomCounter(crateZoom.gameObject);
			break;
		}
	}

	private void changeCrateScreen(int index)
	{
		for (int i = 0; i < crateScreens.Length; i++)
		{
			crateScreens[i].SetActive(i == index);
		}
	}

	private void onCrateLid(Switch3DEvent switchEvent)
	{
		if (audioSplicer.transform.IsChildOf(crateItem.Get<Transform>(0u)))
		{
			audioSplicer.targetable = switchEvent == Switch3DEvent.On;
		}
	}

	private void onCrateBlinkingTimerDone(CrateBlinkingTimer timer)
	{
		if (timer.swapperPiece.targetable)
		{
			bool flag = timer.tween.findStateByName("Down").targetWeight < 0.1f;
			float weight = (flag ? 1f : 0f);
			timer.tween.transitionTo("Down", 2f, weight);
			game.startTimer(new CrateBlinkingTimer(timer.tween, timer.swapperPiece), flag ? 1.3f : 0.6f);
		}
	}

	private void initAudioSplicer()
	{
		splicedAudioSolution.Add(new SplicedLogState
		{
			word = "Over",
			startTime = 0.3583741f,
			endTime = 0.4447674f,
			logIndex = 1
		});
		splicedAudioSolution.Add(new SplicedLogState
		{
			word = "ride",
			startTime = 0.2928082f,
			endTime = 0.363584f,
			logIndex = 3
		});
		splicedAudioSolution.Add(new SplicedLogState
		{
			word = "4",
			startTime = 0.1429357f,
			endTime = 0.169993f,
			logIndex = 2
		});
		splicedAudioSolution.Add(new SplicedLogState
		{
			word = "Alpha",
			startTime = 0.7239021f,
			endTime = 0.8228026f,
			logIndex = 3
		});
		splicedAudioSolution.Add(new SplicedLogState
		{
			word = "Bra",
			startTime = 0.2772484f,
			endTime = 0.3356247f,
			logIndex = 0
		});
		splicedAudioSolution.Add(new SplicedLogState
		{
			word = "vo",
			startTime = 0.7957717f,
			endTime = 0.8385264f,
			logIndex = 2
		});
		splicerBottomPlayTrackerStartingPosition = splicerBottomPlayTracker.Get<Transform>(0f).localPosition;
		game.startTimer(new RfidBlinkingTimer(isGoingDown: true), 2f);
		changeRfidScreen(RfidScreen.Scanning);
		splicerMeshes = new Mesh[splicerSplicedLogs.Length];
		splicerMax.text = splicerSplicedLogs.Length.ToString();
		splicerMiddleDelete.Get<GameObject>(0).SetActive(value: false);
	}

	private void onRfidBlinkingTimerDone(RfidBlinkingTimer timer)
	{
		for (int i = 0; i < splicerRfidSlots.Length; i++)
		{
			if (!splicerAvailableLogs.Contains(splicerRfidSlots[i]))
			{
				rfidSlotAnimations[i].Get<MaterialState>(0f).transitionTo("Off", 2.5f, timer.isGoingDown ? 0f : 1f);
			}
		}
		if (splicerAvailableLogs.Count != splicerRfidSlots.Length)
		{
			game.startTimer(new RfidBlinkingTimer(!timer.isGoingDown), timer.isGoingDown ? 1.8f : 0.5f);
		}
	}

	private void onAudioSplicerUpdate()
	{
		Vector3 normalized = (splicerSliderBoundaries[1].localPosition - splicerSliderBoundaries[0].localPosition).normalized;
		splicerSlidables[1].endNode.localPosition = splicerSlidables[0].transform.localPosition - normalized * 0.005f;
		splicerSlidables[0].startNode.localPosition = splicerSlidables[1].transform.localPosition + normalized * 0.005f;
		if (splicerLogAudioInstance.isValid())
		{
			splicerLogAudioInstance.set3DAttributes(audioSplicer.transform.To3DAttributes());
		}
	}

	public bool cutQuadMesh(MeshFilter meshFilter, int splicedLogIndex, int middleLogIndex, float rightCutValue, float leftCutValue, float nextLogScale, out float cutLength, out float reducedRightSide)
	{
		int num = cutMeshParameters.FindIndex((CutMeshParameter p) => p.splicedLogIndex == splicedLogIndex);
		if (num >= 0)
		{
			cutMeshParameters[num].middleLogIndex = middleLogIndex;
			cutMeshParameters[num].rightCutValue = rightCutValue;
			cutMeshParameters[num].leftCutValue = leftCutValue;
			cutMeshParameters[num].nextLogScale = nextLogScale;
		}
		else
		{
			cutMeshParameters.Add(new CutMeshParameter
			{
				splicedLogIndex = splicedLogIndex,
				middleLogIndex = middleLogIndex,
				rightCutValue = rightCutValue,
				leftCutValue = leftCutValue,
				nextLogScale = nextLogScale
			});
		}
		reducedRightSide = -1f;
		cutLength = 0f;
		if (meshFilter == null || middleLogIndex < 0 || middleLogIndex >= splicerAudioLogs.Length || leftCutValue > rightCutValue || splicedLogIndex < 0 || splicedLogIndex >= splicerMeshes.Length)
		{
			return false;
		}
		Mesh originalMesh = splicerAudioLogs[middleLogIndex].Get<MeshFilter>(0).sharedMesh;
		if (splicerMeshes[splicedLogIndex] == null)
		{
			splicerMeshes[splicedLogIndex] = new Mesh();
		}
		Vector3[] vertices = originalMesh.vertices;
		if (vertices.Length < 4)
		{
			return false;
		}
		if (!fixWidth(nextLogScale, out var newRightCutValue, out var reducedSize))
		{
			return false;
		}
		modifyMesh(out cutLength);
		Bounds bounds = splicerMeshes[splicedLogIndex].bounds;
		splicerSplicedLogs[splicedLogIndex].Get<BoxCollider>((short)0).center = bounds.center;
		splicerSplicedLogs[splicedLogIndex].Get<BoxCollider>((short)0).size = bounds.size;
		meshFilter.mesh = splicerMeshes[splicedLogIndex];
		if (reducedSize)
		{
			reducedRightSide = newRightCutValue;
		}
		return true;
		bool fixWidth(float scale, out float reference2, out bool reference)
		{
			reference = false;
			reference2 = rightCutValue;
			float num2 = Vector3.Distance(Vector3.Lerp(vertices[0], vertices[1], rightCutValue), Vector3.Lerp(vertices[0], vertices[1], leftCutValue)) * scale;
			float num3 = Vector3.Distance(splicerLastCutLogEndPosition, splicedLogsBoundaries[1].localPosition);
			debugPoint.position = splicerLastCutLogEndPosition;
			if (num2 <= num3)
			{
				return true;
			}
			Debug.Log($"leftWidth: {num3}, new: {num2}");
			num2 = num3;
			if (num2 < 0.001f)
			{
				return false;
			}
			float num4 = Vector3.Distance(splicedLogsBoundaries[0].localPosition, splicedLogsBoundaries[1].localPosition);
			reference2 = num2 / num4 + leftCutValue;
			reference = true;
			return true;
		}
		void modifyMesh(out float reference)
		{
			Vector2[] array = ((originalMesh.uv.Length == vertices.Length) ? originalMesh.uv : new Vector2[4]);
			splicerMeshes[splicedLogIndex].vertices = vertices;
			splicerMeshes[splicedLogIndex].uv = array;
			splicerMeshes[splicedLogIndex].triangles = originalMesh.triangles;
			Vector3[] array2 = new Vector3[4];
			array2[1] = Vector3.Lerp(vertices[0], vertices[1], newRightCutValue);
			array2[2] = Vector3.Lerp(vertices[3], vertices[2], newRightCutValue);
			array2[0] = Vector3.Lerp(vertices[0], vertices[1], leftCutValue);
			array2[3] = Vector3.Lerp(vertices[3], vertices[2], leftCutValue);
			Vector3 vector = -array2[0];
			vector.y = 0f;
			reference = Vector3.Distance(array2[0], array2[1]);
			for (int i = 0; i < 4; i++)
			{
				vertices[i] = array2[i] + vector;
			}
			array2[1] = Vector2.Lerp(array[0], array[1], newRightCutValue);
			array2[2] = Vector2.Lerp(array[3], array[2], newRightCutValue);
			array2[0] = Vector2.Lerp(array[0], array[1], leftCutValue);
			array2[3] = Vector2.Lerp(array[3], array[2], leftCutValue);
			for (int j = 0; j < 4; j++)
			{
				array[j] = array2[j];
			}
			splicerMeshes[splicedLogIndex].vertices = vertices;
			splicerMeshes[splicedLogIndex].uv = array;
			splicerMeshes[splicedLogIndex].RecalculateBounds();
			splicerMeshes[splicedLogIndex].RecalculateNormals();
		}
	}

	private void onRfidSlot(Slot slot, int index)
	{
		if (splicerAvailableLogs.Contains(slot))
		{
			Debug.Log("Already have that log");
			return;
		}
		rfidSlotAnimations[index].Get<AnimationSampler>(0).play();
		game.startTimer(new RfidSlotTimer(index), 1.6f);
		PineFmod.playOneShotSoundAttached("event:/Sound Effects/03 Interactable/Buttons & Switches/Rfid_Reader/Rfid_LED_ON", rfidSlotAnimations[index].Get<GameObject>(0u));
		game.startTimer(new RfidLEDTimer(index, 0), 0.4f);
	}

	private void onRfidSlotEjected(int index)
	{
		Slot item = splicerRfidSlots[index];
		if (splicerAvailableLogs.Contains(item))
		{
			Debug.Log("Already have that log");
			return;
		}
		int count = splicerAvailableLogs.Count;
		splicerAvailableLogs.Add(item);
		splicerTabs[count].gameObject.SetActive(value: true);
		rfidSlotAnimations[index].Get<MaterialState>(0f).transitionTo("Off", 0.3f);
		splicerLogCount.text = splicerAvailableLogs.Count + " / " + splicerTabs.Length;
		if (splicerAvailableLogs.Count == splicerTabs.Length)
		{
			changeRfidScreen(RfidScreen.Splicer);
			onSplicerTabClick(0, silent: true);
			game.finishPuzzle(Puzzle.RFID);
		}
		else
		{
			changeRfidScreen(RfidScreen.Numbers);
		}
	}

	private void onRfidSlotTimerDone(RfidSlotTimer timer)
	{
		onRfidSlotEjected(timer.index);
	}

	private void onRfidLEDTimerDone(RfidLEDTimer timer)
	{
		if (timer.count == 3)
		{
			PineFmod.playOneShotSoundAttached("event:/Sound Effects/03 Interactable/Buttons & Switches/Rfid_Reader/Rfid_LED_End", rfidSlotAnimations[timer.index].Get<GameObject>(0u));
		}
		else
		{
			PineFmod.playOneShotSoundAttached("event:/Sound Effects/03 Interactable/Buttons & Switches/Rfid_Reader/Rfid_LED_ON", rfidSlotAnimations[timer.index].Get<GameObject>(0u));
		}
		if (timer.count < 3)
		{
			game.startTimer(new RfidLEDTimer(timer.index, timer.count + 1), 0.4f);
		}
	}

	private void changeRfidScreen(RfidScreen screen)
	{
		for (int i = 0; i < rfidScreens.Length; i++)
		{
			rfidScreens[i].SetActive(i == (int)screen);
		}
	}

	private void onAudioSplicerZoom(bool enter)
	{
		splicerLogAudioInstance.setVolume(enter ? 1f : 0.3f);
	}

	private void onSplicerTabClick(int index, bool silent = false)
	{
		splicerCurrentMiddleLog = index;
		for (int i = 0; i < splicerAudioLogs.Length; i++)
		{
			splicerTabButtonHighlights[i].SetActive(i == index);
			splicerTabTextHighlights[i].SetActive(i == index);
			splicerAudioLogs[i].Get<GameObject>(0f).SetActive(i == index);
			if (i == index && splicerTabs[i].state != Switch3DState.Off)
			{
				game.startSwitch(splicerTabs[i]);
			}
		}
		cancelMiddlePlay();
		if (!silent)
		{
			onSplicerMiddlePlay();
		}
	}

	private void onSplicerMiddlePlay()
	{
		cancelSplicerBottomPlay();
		isSplicerMiddlePlaying = true;
		splicerMiddlePlayTracker.Get<GameObject>(0).SetActive(value: true);
		float duration = 3.5f;
		splicerMiddlePlayTracker.Get<Transform>(0f).localPosition = splicerSliderBoundaries[0].localPosition;
		game.startTransitionLocal(splicerMiddlePlayTracker.Get<Transform>(0f), duration, 0f, splicerSliderBoundaries[1].localPosition, null, null, Interpolation.Linear);
		playAudioLog(splicerCurrentMiddleLog, 0f);
		game.startTimer(new SplicerMiddleTimer(), duration);
		Debug.Log("Play Middle");
	}

	private void cancelMiddlePlay()
	{
		if (game.getTimers<SplicerMiddleTimer>().Count > 0)
		{
			game.cancelTimers<SplicerMiddleTimer>();
			onSplicerMiddleTimerDone(null);
		}
	}

	private void onSplicerMiddleTimerDone(SplicerMiddleTimer timer)
	{
		if (timer == null || !timer.isCancelled)
		{
			splicerMiddlePlayTracker.Get<GameObject>(0).SetActive(value: false);
			isSplicerMiddlePlaying = false;
			stopSplicerLogAudio();
			splicerBottomPlayFromStart();
		}
	}

	private void playAudioLog(int logIndex, float startTimeSeconds)
	{
		if (logIndex >= 0)
		{
			stopSplicerLogAudio();
			splicerLogAudioInstance = RuntimeManager.CreateInstance(splicerLogAudioPaths[logIndex]);
			splicerLogAudioInstance.setVolume(game.isInTopZoom(audioSplicer.gameObject) ? 1f : 0.2f);
			splicerLogAudioInstance.start();
			splicerLogAudioInstance.setTimelinePosition((int)(startTimeSeconds * 1000f));
		}
	}

	private void stopSplicerLogAudio()
	{
		if (splicerLogAudioInstance.isValid())
		{
			splicerLogAudioInstance.release();
			splicerLogAudioInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
			Debug.Log("stopSplicerLogAudio");
		}
	}

	private void onSplicerMiddleCut()
	{
		if (splicerCurrentMiddleLog < 0 || splicedAudio.Count >= splicerMeshes.Length || isSplicerBottomFull)
		{
			game.cancelTimers((UiBlinkTimer x) => x.materialState.gameObject == splicerMiddleCut.Get<GameObject>(0));
			game.startTimer(new UiBlinkTimer(splicerCount.Get<TweenState>(0f), splicerMiddleCut.Get<MaterialState>(0u), 0, "Error", 3), 0f);
			return;
		}
		game.startTimer(new UiBlinkTimer(splicerMiddleCut.Get<TweenState>((short)0), null, 0, "Select", 1), 0f);
		SplicedLogState splicedLogState = new SplicedLogState();
		splicedLogState.startTime = getSplicerCutStart();
		splicedLogState.endTime = getSplicerCutEnd();
		if (splicedLogState.startTime > splicedLogState.endTime)
		{
			splicedLogState.startTime = splicedLogState.endTime;
			splicedLogState.endTime = getSplicerCutStart();
		}
		splicedLogState.logIndex = splicerCurrentMiddleLog;
		Debug.Log($"Cut {splicerCurrentMiddleLog} -> {splicedLogState.startTime.ToString().Replace(',', '.')}f - {splicedLogState.endTime.ToString().Replace(',', '.')}f");
		for (int num = 0; num < splicerSplicedLogs.Length; num++)
		{
			if (!splicerSplicedLogs[num].Get<Switch3D>(0u).gameObject.activeSelf)
			{
				splicedLogState.splicedLogIndex = num;
				splicerSplicedLogs[num].Get<Switch3D>(0u).gameObject.SetActive(value: true);
				break;
			}
		}
		if (splicedLogState.splicedLogIndex < 0)
		{
			Debug.LogError("No more spliced logs, disable the button when all are used up");
			return;
		}
		splicedLogState.button = splicerSplicedLogs[splicedLogState.splicedLogIndex].Get<Switch3D>(0u);
		if (!cutQuadMesh(splicerSplicedLogs[splicedLogState.splicedLogIndex].Get<MeshFilter>(0f), splicedLogState.splicedLogIndex, splicerCurrentMiddleLog, splicedLogState.endTime, splicedLogState.startTime, splicerSplicedLogs[splicedLogState.splicedLogIndex].Get<Transform>(0).localScale.x, out splicedLogState.logWidth, out var reducedRightSide))
		{
			splicerSplicedLogs[splicedLogState.splicedLogIndex].Get<Switch3D>(0u).gameObject.SetActive(value: false);
			return;
		}
		if (reducedRightSide > 0f)
		{
			isSplicerBottomFull = true;
			splicedLogState.endTime = reducedRightSide;
		}
		splicedAudio.Add(splicedLogState);
		splicerCount.Get<Text>(0).text = splicedAudio.Count.ToString();
		splicerArrangeLogs();
		game.invalidateItemImpostors(audioSplicer);
		checkSplicerSolution();
		if (splicedAudio.Count >= splicerMeshes.Length)
		{
			isSplicerBottomFull = true;
		}
		cancelMiddlePlay();
		cancelSplicerBottomPlay();
		splicerBottomPlayFromStart();
	}

	private float getSplicerCutStart()
	{
		return Mathf.InverseLerp(splicerSliderBoundaries[0].localPosition.x, splicerSliderBoundaries[1].localPosition.x, splicerSlidables[1].transform.localPosition.x);
	}

	private float getSplicerCutEnd()
	{
		return Mathf.InverseLerp(splicerSliderBoundaries[0].localPosition.x, splicerSliderBoundaries[1].localPosition.x, splicerSlidables[0].transform.localPosition.x);
	}

	private void splicerArrangeLogs()
	{
		Vector3 localPosition = splicedLogsBoundaries[0].localPosition;
		Vector3 normalized = (splicedLogsBoundaries[1].localPosition - splicedLogsBoundaries[0].localPosition).normalized;
		Debug.Log($"dir: {normalized}");
		for (int i = 0; i < splicedAudio.Count; i++)
		{
			splicerSplicedLogs[splicedAudio[i].splicedLogIndex].Get<Transform>(0).localPosition = localPosition;
			localPosition += normalized * splicedAudio[i].logWidth * splicerSplicedLogs[splicedAudio[i].splicedLogIndex].Get<Transform>(0).localScale.x;
			Debug.Log($"pos: {splicerSplicedLogs[splicedAudio[i].splicedLogIndex].Get<Transform>(0).localPosition}, curr: {localPosition}");
		}
		splicerLastCutLogEndPosition = localPosition;
	}

	private void onSplicerBottomSelect(Switch3D button, Switch3DEvent switchEvent)
	{
		Debug.Log(button.name + ", " + switchEvent);
		if (switchEvent == Switch3DEvent.On)
		{
			foreach (SplicedLogState item in splicedAudio)
			{
				if (item.button != button && item.button.state == Switch3DState.On)
				{
					game.startSwitch(item.button);
				}
			}
		}
		else
		{
			Debug.Log(button.name);
		}
		splicerMiddleDelete.Get<GameObject>(0).SetActive(splicedAudio.FindAll((SplicedLogState x) => x.button.state == Switch3DState.On).Count > 0);
	}

	private void onSplicerBottomDelete()
	{
		Switch3D button = splicedAudio.Find((SplicedLogState x) => x.button.state == Switch3DState.On)?.button;
		if (button == null)
		{
			Debug.Log("No splices were selected");
			return;
		}
		splicerMiddleDelete.Get<GameObject>(0).SetActive(value: false);
		SplicedLogState buttonToRemove = splicedAudio.Find((SplicedLogState x) => x.button == button);
		cutMeshParameters.RemoveAll((CutMeshParameter p) => p.splicedLogIndex == buttonToRemove.splicedLogIndex);
		splicedAudio.Remove(buttonToRemove);
		splicerCount.Get<Text>(0).text = splicedAudio.Count.ToString();
		game.startSwitch(button);
		button.gameObject.SetActive(value: false);
		splicerArrangeLogs();
		checkSplicerSolution();
		if (splicedAudio.Count < splicerMeshes.Length)
		{
			isSplicerBottomFull = false;
		}
		cancelSplicerBottomPlay();
		splicerBottomPlayFromStart();
	}

	private void splicerBottomPlayFromStart()
	{
		Debug.Log("splicerBottomPlayFromStart");
		splicerBottomPlayTracker.Get<Transform>(0f).localPosition = splicerBottomPlayTrackerStartingPosition;
		splicerBottomPlayTracker.Get<GameObject>(0).SetActive(value: true);
		game.startTransitionLocal(new SplicedBottomTrackerTransition(), splicerBottomPlayTracker.Get<Transform>(0f), 3.5f, 0f, splicerBottomEndPiece.transform.localPosition, null, null, Interpolation.Linear);
		game.startTimer(new SplicerBottomTimer(0, 0f), 3.5f);
	}

	private void onSplicerBottomTimerUpdate(SplicerBottomTimer timer)
	{
		if (timer.isCancelled || splicedAudio.Count == 0 || timer.index < 0)
		{
			return;
		}
		if (timer.index > splicedAudio.Count - 1)
		{
			if (timer.totalPreviousLogTimes < timer.time)
			{
				stopSplicerLogAudio();
			}
		}
		else if (timer.index == 0 || timer.totalPreviousLogTimes < timer.time)
		{
			playAudioLog(splicedAudio[timer.index].logIndex, splicedAudio[timer.index].startTime * 3.5f);
			timer.totalPreviousLogTimes += splicedAudio[timer.index].duration * 3.5f;
			timer.index++;
		}
	}

	private void onSplicerBottomTimerDone(SplicerBottomTimer splicerBottomTimer)
	{
		if (!splicerBottomTimer.isCancelled)
		{
			splicerBottomPlayFromStart();
		}
	}

	private void cancelSplicerBottomPlay()
	{
		game.cancelTimers<SplicerBottomTimer>();
		game.cancelTimers<SplicedBottomTrackerTransition>();
		splicerBottomPlayTracker.Get<GameObject>(0).SetActive(value: false);
		splicerBottomPlayTracker.Get<Transform>(0f).localPosition = splicerBottomPlayTrackerStartingPosition;
	}

	[DebugButton(null, Tint.Default, PostClickAction.HideButton, 0, new object[] { })]
	private void debugGetSplicer()
	{
		audioSplicer.targetable = true;
		game.addItemToInventory(audioSplicer.gameObject);
	}

	[DebugButton(null, Tint.Default, PostClickAction.HideButton, 0, new object[] { })]
	private void debugGetScannedSplicer()
	{
		audioSplicer.targetable = true;
		game.addItemToInventory(audioSplicer.gameObject);
		changeRfidScreen(RfidScreen.Splicer);
	}

	[DebugButton(null, Tint.Default, PostClickAction.HideButton, 0, new object[] { })]
	private void debugSolveSplicer()
	{
		debugSolvedSplicer = true;
		checkSplicerSolution();
	}

	private void checkSplicerSolution()
	{
		if (debugSolvedSplicer)
		{
			isSplicerCorrect = true;
			return;
		}
		bool flag = true;
		if (splicedAudio.Count != splicedAudioSolution.Count)
		{
			flag = false;
		}
		for (int i = 0; i < splicedAudio.Count; i++)
		{
			if (!flag)
			{
				break;
			}
			SplicedLogState splicedLogState = splicedAudio[i];
			SplicedLogState splicedLogState2 = splicedAudioSolution[i];
			if (splicedLogState.logIndex != splicedLogState2.logIndex)
			{
				flag = false;
				break;
			}
			if (!isWithinBufferZone(splicedLogState2.startTime, splicedLogState.startTime) || !isWithinBufferZone(splicedLogState2.endTime, splicedLogState.endTime))
			{
				flag = false;
				break;
			}
		}
		isSplicerCorrect = flag;
		Debug.Log("CHECK SPLICER: " + isSplicerCorrect);
		bool isWithinBufferZone(float checkTime, float solutionTime)
		{
			float num = splicedAudioPadding / 2f;
			float num2 = Mathf.Max(solutionTime - num, 0f);
			float num3 = Mathf.Max(solutionTime + num, 0f);
			if (checkTime >= num2 && checkTime <= num3)
			{
				return true;
			}
			return false;
		}
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void solveAudioAnalyzer()
	{
		isSplicerSolved = true;
		changeWaveformScreen(WaveformScreens.AccessGranted);
		game.startTimer(new AudioAnalyzerTimer(3), 1f);
		game.finishPuzzle(Puzzle.SoundSplicing);
	}

	private void onUiErrorTimerDone(UiBlinkTimer timer)
	{
		if (timer.index <= timer.times)
		{
			if (timer.tweenState != null)
			{
				timer.tweenState.transitionTo(timer.state, 6f, (timer.index % 2 != 0) ? 0f : 1f);
			}
			if (timer.materialState != null)
			{
				timer.materialState.transitionTo(timer.state, 6f, (timer.index % 2 != 0) ? 0f : 1f);
			}
			game.startTimer(new UiBlinkTimer(timer.tweenState, timer.materialState, timer.index + 1, timer.state, timer.times), 1f / 6f);
		}
		else
		{
			if (timer.tweenState != null)
			{
				timer.tweenState.transitionTo(timer.state, 6f, 0f);
			}
			if (timer.materialState != null)
			{
				timer.materialState.transitionTo(timer.state, 6f, 0f);
			}
		}
	}

	private void initWaveform()
	{
		changeWaveformScreen(WaveformScreens.InsertKey);
		cardReaderDoor.setState("Open");
		exitSlot.Get<Slot>(0).targetable = true;
	}

	private void onExitSlot()
	{
		exitSlot.Get<TweenState>(0f).transitionTo("In", 2f);
		changeWaveformScreen(WaveformScreens.Loading);
		cardReaderMaterialState.transitionTo("Green", 5f);
		listeningDeviceCapAnimation.play();
		game.startTimer(new AudioAnalyzerTimer(0), 1f);
	}

	private void onSplicerSlot()
	{
		splicerExitSlot.targetable = false;
		changeWaveformScreen(WaveformScreens.Listening);
		game.startTimer(new AudioAnalyzerTimer(1), 2f);
	}

	private void changeWaveformScreen(WaveformScreens screen)
	{
		for (int i = 0; i < waveformScreens.Length; i++)
		{
			waveformScreens[i].SetActive(i == (int)screen);
		}
	}

	private void onAudioAnalyzerTimerDone(AudioAnalyzerTimer timer)
	{
		if (timer.index == 0)
		{
			changeWaveformScreen(WaveformScreens.WaitingForSlicer);
			splicerExitSlot.targetable = true;
		}
		else if (timer.index == 1)
		{
			if (isSplicerCorrect)
			{
				game.callRPC(RPCs.AudioAnalyzerSolved);
			}
			else
			{
				game.callRPC(RPCs.AudioAnalyzerNotSolved);
			}
		}
		else if (timer.index == 2)
		{
			splicerExitSlot.targetable = true;
			changeWaveformScreen(WaveformScreens.WaitingForSlicer);
		}
		else if (timer.index == 3)
		{
			exitDoor.transitionTo("Down", 0.3f);
			levelExitRef.Get<GameObject>(0).SetActive(value: true);
			game.startTimer(new AudioAnalyzerTimer(4), 2f);
		}
		else if (timer.index == 4)
		{
			game.levelCompleted();
		}
	}

	private void onAudioAnalyzerNotSolved()
	{
		changeWaveformScreen(WaveformScreens.AccessDenied);
		game.startTimer(new AudioAnalyzerTimer(2), 1f);
	}

	private void initHolograms()
	{
		for (int i = 0; i < hologramMeshFilters.Length; i++)
		{
			hologramMeshFilters[i].mesh = hologramMeshes[hologramStates[i]];
		}
		foreach (Ref<GameObject, Transform> hologram in holograms)
		{
			((GameObject)hologram).SetActive(value: false);
		}
		MaterialState[] array = hologramMaterialStates;
		for (int j = 0; j < array.Length; j++)
		{
			array[j].setState("Gray");
		}
		hoverDroneParents = new List<Transform>();
		for (int k = 0; k < hoveringDrones.Length; k++)
		{
			hoverDroneParents.Add(hoveringDrones[k].Get<Transform>(0f).parent);
			hoveringDrones[k].Get<GameObject>(0).SetActive(value: false);
		}
		printedTablet.targetable = false;
		crateOverrideKey.targetable = false;
	}

	private void hologramChangeScreen(HologramScreen screen)
	{
		for (int i = 0; i < hologramScreens.Length; i++)
		{
			hologramScreens[i].SetActive(i == (int)screen);
		}
	}

	[DebugButton(null, Tint.Default, PostClickAction.ReturnToGame, 0, new object[] { })]
	private void onHologramOn()
	{
		hologramCheckScheduleButton.gameObject.SetActive(value: false);
		hologramOnButton.Get<GameObject>(0f).SetActive(value: false);
		loadingHoloText.SetActive(value: true);
		turningOffText.SetActive(value: false);
		startHoloDrones();
	}

	private void countHoloDrones()
	{
		holoDronesArrived++;
		if (holoDronesArrived >= droneAnimations.Length)
		{
			hologramCheckScheduleButton.gameObject.SetActive(value: true);
			if (loadingHoloText.activeSelf)
			{
				hologramOnButton.Get<GameObject>(0f).SetActive(value: false);
				hologramPeople.SetActive(value: true);
				loadingHoloText.SetActive(value: false);
				turningOffText.SetActive(value: false);
			}
			else
			{
				hologramOnButton.Get<GameObject>(0f).SetActive(value: true);
				loadingHoloText.SetActive(value: false);
				turningOffText.SetActive(value: false);
			}
		}
	}

	private void onHologramCheckSchedule()
	{
		hologramCheckScheduleButton.targetable = false;
		game.startTimer(new HologramTimer(0), 0.1f);
	}

	private void startHoloDrones()
	{
		holoDronesArrived = 0;
		for (int i = 0; i < droneAnimations.Length; i++)
		{
			droneAnimations[i].Get<TweenState>(0).transitionTo("Down", droneSpeeds[i]);
		}
	}

	private void stopHoloDrones()
	{
		holoDronesArrived = 0;
		for (int i = 0; i < droneAnimations.Length; i++)
		{
			holograms[i].Get<GameObject>(0).SetActive(value: false);
			hoveringDrones[i].Get<GameObject>(0).SetActive(value: false);
			game.setParent(hoveringDrones[i].Get<Transform>(0f), hoverDroneParents[i]);
			droneAnimations[i].Get<GameObject>(0f).SetActive(value: true);
			droneAnimations[i].Get<TweenState>(0).transitionTo("Down", droneSpeeds[i], 0f);
		}
	}

	private void onDroneAnimationsComplete(TweenState tweenState, string state)
	{
		int num = droneAnimations.IndexOf(tweenState, 0);
		if (num >= 0)
		{
			if (tweenState.findStateByName("Down").targetWeight > 0.9f)
			{
				droneAnimations[num].Get<GameObject>(0f).SetActive(value: false);
				game.setParent(hoveringDrones[num].Get<Transform>(0f), holograms[num].Get<Transform>(0f));
				hoveringDrones[num].Get<GameObject>(0).SetActive(value: true);
				holograms[num].Get<GameObject>(0).SetActive(value: true);
			}
			countHoloDrones();
		}
	}

	private bool checkHolograms()
	{
		int i = 0;
		return Array.TrueForAll(hologramStates, (int x) => x == hologramSolution[i++]);
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void solveHologram()
	{
		hologramChangeScreen(HologramScreen.Solved);
		hologramErrorTextMS.Get<GameObject>(0f).SetActive(value: false);
		hologramsSolvedText.SetActive(value: true);
		hologramCheckScheduleButton.targetable = false;
		holoSolvedAnimation.play(2f);
		game.startTimer(new HoloCardTimer(), 2f);
		PineFmod.playOneShotSoundAttached("event:/Sound Effects/03 Interactable/Doors/SciFi_Metal_Box/SciFi_Metal_Box_Start", holoSolvedAnimation.gameObject);
		stopHoloDrones();
		game.finishPuzzle(Puzzle.Holograms);
	}

	private void notSolveHologram()
	{
		hologramChangeScreen(HologramScreen.Error);
		hologramErrorTextMS.Get<MaterialState>(0).setState("Error");
		game.startTimer(new HologramTimer(2), 2f);
	}

	private void onHologramTimerDone(HologramTimer timer)
	{
		if (timer.index == 0)
		{
			hologramChangeScreen(HologramScreen.Checking);
			game.startTimer(new HologramTimer(1), 1.5f);
		}
		else if (timer.index == 1)
		{
			if (checkHolograms())
			{
				game.callRPC(RPCs.SolveHologram);
			}
			else
			{
				game.callRPC(RPCs.NotSolveHologram);
			}
		}
		else if (timer.index == 2)
		{
			hologramChangeScreen(HologramScreen.OnOff);
			hologramErrorTextMS.Get<MaterialState>(0).setState("Default");
			hologramCheckScheduleButton.targetable = true;
		}
	}

	private void onHologramPrintSchedule()
	{
		tabletPrintingAnimation.play();
		printedTablet.targetable = true;
	}

	private void onHologramChange(int index, int delta)
	{
		hologramStates[index] += delta;
		if (hologramStates[index] < 0)
		{
			hologramStates[index] = hologramMeshes.Length - 1;
		}
		else if (hologramStates[index] >= hologramMeshes.Length)
		{
			hologramStates[index] = 0;
		}
		hologramMeshFilters[index].mesh = hologramMeshes[hologramStates[index]];
		hologramMaterialStates[index].transitionTo("Gray", 4f, (hologramStates[index] == 0) ? 1 : 0);
	}

	private void onPressUIButtonAnimationTimerDone(PressUIButtonAnimationTimer timer)
	{
		Switch3D switch3D = blinkingUIButtons[timer.index].Get<Switch3D>();
		if (switch3D.gameObject.activeInHierarchy)
		{
			TweenState tweenState = blinkingUITweens[timer.index].Get<TweenState>();
			TweenState.TweenStateRecord tweenStateRecord = tweenState.findStateByName("Down");
			TweenState.TweenStateRecord tweenStateRecord2 = tweenState.findStateByName("Hover");
			if (tweenStateRecord == null || tweenStateRecord2 == null)
			{
				Debug.Log("BUTTON EARLY RETURN " + timer.index);
				return;
			}
			if (switch3D.targetable && tweenStateRecord2.targetWeight == 0f)
			{
				float weight = ((tweenStateRecord.targetWeight == 1f) ? 0f : 1f);
				tweenState.transitionToStateAdditive("Down", 0.5f, weight);
			}
			else if (tweenStateRecord.targetWeight > 0f)
			{
				tweenState.transitionToStateAdditive("Down", 4f, 0f);
			}
		}
		game.startTimer(new PressUIButtonAnimationTimer(timer.index), 1.5f);
	}

	private void startLoadingSound(Vector3 position)
	{
		if (loadingSoundInstances[loadingSoundInstanceIndex].isValid())
		{
			loadingSoundInstances[loadingSoundInstanceIndex].release();
			loadingSoundInstances[loadingSoundInstanceIndex].stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
		}
		loadingSoundInstances[loadingSoundInstanceIndex] = RuntimeManager.CreateInstance(loadingSoundEvent);
		loadingSoundInstances[loadingSoundInstanceIndex].set3DAttributes(position.To3DAttributes());
		loadingSoundInstances[loadingSoundInstanceIndex].start();
		loadingSoundInstanceIndex = (loadingSoundInstanceIndex + 1) % loadingSoundInstances.Length;
	}

	[DebugButton("Get Screwdriver", Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void debugGetScrewdriver()
	{
		game.addItemToInventory(screwdriver.gameObject);
	}

	private void OnDestroy()
	{
		stopSplicerLogAudio();
	}

	public virtual void save(FastBinaryWriter writer)
	{
		int value = (int)podCurrentScreen;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		writer.WriteArray(currentQuestions, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteArray(podCurrentShipNames, delegate(FastBinaryWriter w, string e)
		{
			w.Write(e);
		});
		writer.Write(in exitedPod, default(FastBinaryWriter.ForPrimitives));
		writer.WriteArray(podsInitiated, delegate(FastBinaryWriter w, bool e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.Write(in podsPuzzleSolved, default(FastBinaryWriter.ForPrimitives));
		writer.WriteList(podSpawnPointsToPose, delegate(FastBinaryWriter w, SpawnPointToPose e)
		{
			w.WriteComponent(e);
		});
		writer.WriteList(lockers, delegate(FastBinaryWriter w, TriangleKeypad e)
		{
			w.WriteTriangleKeypad(e);
		});
		writer.Write(in isMedBedKeySlotOn, default(FastBinaryWriter.ForPrimitives));
		value = (int)currentComponentScreen;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in sampleProvided, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in batteriesAdded, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in bacteriaCount, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in medScannerTimer, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in lastScannedPoint, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in foundBacterias, default(FastBinaryWriter.ForPrimitives));
		value = (int)currentScannerScreen;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in scannerNeedsAPerson, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in isPersonInScanner, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in enableMedbedHologram, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in convertedToSp, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in holoSpEnabled, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in canScanMedBed, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in solvedComponents, default(FastBinaryWriter.ForPrimitives));
		writer.WriteList(medBedHoloBacteriaCurrent, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteList(medScannerSolutionPoints, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteArray(medicineTentaclesLeft, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.Write(in releasedMedicine, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in solvedMedicineBeforeScanning, default(FastBinaryWriter.ForPrimitives));
		writer.WriteList(airlockKeypadCurrentSolution, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteList(scrambleCurrentImages, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteList(currentCaptains, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteList(splicerAvailableLogs, delegate(FastBinaryWriter w, Slot e)
		{
			w.WriteComponent(e);
		});
		writer.Write(in splicerCurrentMiddleLog, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in playLog, default(FastBinaryWriter.ForPrimitives));
		writer.WriteList(splicedAudio, delegate(FastBinaryWriter w, SplicedLogState e)
		{
			w.WriteSplicedLogState(e);
		});
		writer.Write(in isSplicerBottomFull, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in splicerAddedToInventory, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in splicerCurrentTimeInRange, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in isSplicerCorrect, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in isSplicerSolved, default(FastBinaryWriter.ForPrimitives));
		writer.WriteVector3(in splicerLastCutLogEndPosition);
		writer.Write(in isSplicerMiddlePlaying, default(FastBinaryWriter.ForPrimitives));
		writer.WriteVector3(in splicerBottomPlayTrackerStartingPosition);
		writer.Write(in debugSolvedSplicer, default(FastBinaryWriter.ForPrimitives));
		writer.WriteList(cutMeshParameters, delegate(FastBinaryWriter w, CutMeshParameter e)
		{
			w.WriteCutMeshParameter(e);
		});
		writer.WriteList(cratePuzzleSegments, delegate(FastBinaryWriter w, CratePuzzleSegment e)
		{
			w.WriteCratePuzzleSegment(e);
		});
		writer.WriteList(currentCratePuzzleInstructions, delegate(FastBinaryWriter w, CratePuzzleInstruction e)
		{
			w.WriteCratePuzzleInstruction(e);
		});
		writer.Write(in isCrateSolved, default(FastBinaryWriter.ForPrimitives));
		writer.WriteComponent(crateFirstSelected);
		writer.Write(in holoDronesArrived, default(FastBinaryWriter.ForPrimitives));
		writer.WriteArray(hologramStates, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.Write(in loadingSoundInstanceIndex, default(FastBinaryWriter.ForPrimitives));
		writer.WriteArray(podsOpenedFirstTime, delegate(FastBinaryWriter w, bool e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteGameObject(bacteriaZoomToEnter);
	}

	public virtual void load(FastBinaryReader reader)
	{
		podCurrentScreen = (PodScreens)reader.ReadInt32();
		currentQuestions = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		podCurrentShipNames = reader.ReadArray((FastBinaryReader r) => r.ReadString());
		exitedPod = reader.ReadBoolean();
		podsInitiated = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		podsPuzzleSolved = reader.ReadBoolean();
		podSpawnPointsToPose = reader.ReadList((FastBinaryReader r) => r.ReadComponent<SpawnPointToPose>());
		lockers = reader.ReadList((FastBinaryReader r) => r.ReadTriangleKeypad());
		isMedBedKeySlotOn = reader.ReadBoolean();
		currentComponentScreen = (ComponentScreens)reader.ReadInt32();
		sampleProvided = reader.ReadBoolean();
		batteriesAdded = reader.ReadInt32();
		bacteriaCount = reader.ReadInt32();
		medScannerTimer = reader.ReadSingle();
		lastScannedPoint = reader.ReadInt32();
		foundBacterias = reader.ReadBoolean();
		currentScannerScreen = (ScannerScreens)reader.ReadInt32();
		scannerNeedsAPerson = reader.ReadBoolean();
		isPersonInScanner = reader.ReadBoolean();
		enableMedbedHologram = reader.ReadBoolean();
		convertedToSp = reader.ReadBoolean();
		holoSpEnabled = reader.ReadBoolean();
		canScanMedBed = reader.ReadBoolean();
		solvedComponents = reader.ReadBoolean();
		medBedHoloBacteriaCurrent = reader.ReadList((FastBinaryReader r) => r.ReadInt32());
		medScannerSolutionPoints = reader.ReadList((FastBinaryReader r) => r.ReadInt32());
		medicineTentaclesLeft = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		releasedMedicine = reader.ReadBoolean();
		solvedMedicineBeforeScanning = reader.ReadBoolean();
		airlockKeypadCurrentSolution = reader.ReadList((FastBinaryReader r) => r.ReadInt32());
		scrambleCurrentImages = reader.ReadList((FastBinaryReader r) => r.ReadInt32());
		currentCaptains = reader.ReadList((FastBinaryReader r) => r.ReadInt32());
		splicerAvailableLogs = reader.ReadList((FastBinaryReader r) => r.ReadComponent<Slot>());
		splicerCurrentMiddleLog = reader.ReadInt32();
		playLog = reader.ReadBoolean();
		splicedAudio = reader.ReadList((FastBinaryReader r) => r.ReadSplicedLogState());
		isSplicerBottomFull = reader.ReadBoolean();
		splicerAddedToInventory = reader.ReadBoolean();
		splicerCurrentTimeInRange = reader.ReadSingle();
		isSplicerCorrect = reader.ReadBoolean();
		isSplicerSolved = reader.ReadBoolean();
		splicerLastCutLogEndPosition = reader.ReadVector3();
		isSplicerMiddlePlaying = reader.ReadBoolean();
		splicerBottomPlayTrackerStartingPosition = reader.ReadVector3();
		debugSolvedSplicer = reader.ReadBoolean();
		cutMeshParameters = reader.ReadList((FastBinaryReader r) => r.ReadCutMeshParameter());
		cratePuzzleSegments = reader.ReadList((FastBinaryReader r) => r.ReadCratePuzzleSegment());
		currentCratePuzzleInstructions = reader.ReadList((FastBinaryReader r) => r.ReadCratePuzzleInstruction());
		isCrateSolved = reader.ReadBoolean();
		crateFirstSelected = reader.ReadComponent<SwapperPiece>();
		holoDronesArrived = reader.ReadInt32();
		hologramStates = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		loadingSoundInstanceIndex = reader.ReadInt32();
		podsOpenedFirstTime = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		bacteriaZoomToEnter = reader.ReadGameObject();
	}

	public virtual void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		int position = reader.Position;
		PodScreens podScreens = (PodScreens)reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "podCurrentScreen",
			fieldValue = $"{podScreens}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int[] array = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "currentQuestions[" + ((array == null) ? string.Empty : array.Length.ToString()) + "]",
			fieldValue = (((array == null) ? "null" : string.Join(", ", array)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		string[] array2 = reader.ReadArray((FastBinaryReader r) => r.ReadString());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "podCurrentShipNames[" + ((array2 == null) ? string.Empty : array2.Length.ToString()) + "]",
			fieldValue = (((array2 == null) ? "null" : string.Join<string>(", ", (IEnumerable<string>)array2)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "exitedPod",
			fieldValue = $"{flag}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool[] array3 = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "podsInitiated[" + ((array3 == null) ? string.Empty : array3.Length.ToString()) + "]",
			fieldValue = (((array3 == null) ? "null" : string.Join(", ", array3)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag2 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "podsPuzzleSolved",
			fieldValue = $"{flag2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<SpawnPointToPose> list = reader.ReadList((FastBinaryReader r) => r.ReadComponent<SpawnPointToPose>());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "podSpawnPointsToPose[" + ((list == null) ? string.Empty : list.Count.ToString()) + "]",
			fieldValue = (((list == null) ? "null" : string.Join(", ", list)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<TriangleKeypad> list2 = reader.ReadList((FastBinaryReader r) => r.ReadTriangleKeypad());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "lockers[" + ((list2 == null) ? string.Empty : list2.Count.ToString()) + "]",
			fieldValue = (((list2 == null) ? "null" : string.Join(", ", list2)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag3 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "isMedBedKeySlotOn",
			fieldValue = $"{flag3}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		ComponentScreens componentScreens = (ComponentScreens)reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "currentComponentScreen",
			fieldValue = $"{componentScreens}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag4 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "sampleProvided",
			fieldValue = $"{flag4}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "batteriesAdded",
			fieldValue = $"{num}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num2 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "bacteriaCount",
			fieldValue = $"{num2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num3 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "medScannerTimer",
			fieldValue = $"{num3}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num4 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "lastScannedPoint",
			fieldValue = $"{num4}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag5 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "foundBacterias",
			fieldValue = $"{flag5}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		ScannerScreens scannerScreens = (ScannerScreens)reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "currentScannerScreen",
			fieldValue = $"{scannerScreens}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag6 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "scannerNeedsAPerson",
			fieldValue = $"{flag6}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag7 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "isPersonInScanner",
			fieldValue = $"{flag7}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag8 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "enableMedbedHologram",
			fieldValue = $"{flag8}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag9 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "convertedToSp",
			fieldValue = $"{flag9}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag10 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "holoSpEnabled",
			fieldValue = $"{flag10}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag11 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "canScanMedBed",
			fieldValue = $"{flag11}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag12 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "solvedComponents",
			fieldValue = $"{flag12}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<int> list3 = reader.ReadList((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "medBedHoloBacteriaCurrent[" + ((list3 == null) ? string.Empty : list3.Count.ToString()) + "]",
			fieldValue = (((list3 == null) ? "null" : string.Join(", ", list3)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<int> list4 = reader.ReadList((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "medScannerSolutionPoints[" + ((list4 == null) ? string.Empty : list4.Count.ToString()) + "]",
			fieldValue = (((list4 == null) ? "null" : string.Join(", ", list4)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int[] array4 = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "medicineTentaclesLeft[" + ((array4 == null) ? string.Empty : array4.Length.ToString()) + "]",
			fieldValue = (((array4 == null) ? "null" : string.Join(", ", array4)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag13 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "releasedMedicine",
			fieldValue = $"{flag13}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag14 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "solvedMedicineBeforeScanning",
			fieldValue = $"{flag14}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<int> list5 = reader.ReadList((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "airlockKeypadCurrentSolution[" + ((list5 == null) ? string.Empty : list5.Count.ToString()) + "]",
			fieldValue = (((list5 == null) ? "null" : string.Join(", ", list5)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<int> list6 = reader.ReadList((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "scrambleCurrentImages[" + ((list6 == null) ? string.Empty : list6.Count.ToString()) + "]",
			fieldValue = (((list6 == null) ? "null" : string.Join(", ", list6)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<int> list7 = reader.ReadList((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "currentCaptains[" + ((list7 == null) ? string.Empty : list7.Count.ToString()) + "]",
			fieldValue = (((list7 == null) ? "null" : string.Join(", ", list7)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<Slot> list8 = reader.ReadList((FastBinaryReader r) => r.ReadComponent<Slot>());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "splicerAvailableLogs[" + ((list8 == null) ? string.Empty : list8.Count.ToString()) + "]",
			fieldValue = (((list8 == null) ? "null" : string.Join(", ", list8)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num5 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "splicerCurrentMiddleLog",
			fieldValue = $"{num5}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag15 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "playLog",
			fieldValue = $"{flag15}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<SplicedLogState> list9 = reader.ReadList((FastBinaryReader r) => r.ReadSplicedLogState());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "splicedAudio[" + ((list9 == null) ? string.Empty : list9.Count.ToString()) + "]",
			fieldValue = (((list9 == null) ? "null" : string.Join(", ", list9)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag16 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "isSplicerBottomFull",
			fieldValue = $"{flag16}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag17 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "splicerAddedToInventory",
			fieldValue = $"{flag17}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num6 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "splicerCurrentTimeInRange",
			fieldValue = $"{num6}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag18 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "isSplicerCorrect",
			fieldValue = $"{flag18}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag19 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "isSplicerSolved",
			fieldValue = $"{flag19}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Vector3 vector = reader.ReadVector3();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "splicerLastCutLogEndPosition",
			fieldValue = $"{vector}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag20 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "isSplicerMiddlePlaying",
			fieldValue = $"{flag20}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Vector3 vector2 = reader.ReadVector3();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "splicerBottomPlayTrackerStartingPosition",
			fieldValue = $"{vector2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag21 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "debugSolvedSplicer",
			fieldValue = $"{flag21}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<CutMeshParameter> list10 = reader.ReadList((FastBinaryReader r) => r.ReadCutMeshParameter());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "cutMeshParameters[" + ((list10 == null) ? string.Empty : list10.Count.ToString()) + "]",
			fieldValue = (((list10 == null) ? "null" : string.Join(", ", list10)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<CratePuzzleSegment> list11 = reader.ReadList((FastBinaryReader r) => r.ReadCratePuzzleSegment());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "cratePuzzleSegments[" + ((list11 == null) ? string.Empty : list11.Count.ToString()) + "]",
			fieldValue = (((list11 == null) ? "null" : string.Join(", ", list11)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<CratePuzzleInstruction> list12 = reader.ReadList((FastBinaryReader r) => r.ReadCratePuzzleInstruction());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "currentCratePuzzleInstructions[" + ((list12 == null) ? string.Empty : list12.Count.ToString()) + "]",
			fieldValue = (((list12 == null) ? "null" : string.Join(", ", list12)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag22 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "isCrateSolved",
			fieldValue = $"{flag22}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		SwapperPiece arg = reader.ReadComponent<SwapperPiece>();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "crateFirstSelected",
			fieldValue = $"{arg}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num7 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "holoDronesArrived",
			fieldValue = $"{num7}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int[] array5 = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "hologramStates[" + ((array5 == null) ? string.Empty : array5.Length.ToString()) + "]",
			fieldValue = (((array5 == null) ? "null" : string.Join(", ", array5)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num8 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "loadingSoundInstanceIndex",
			fieldValue = $"{num8}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool[] array6 = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "podsOpenedFirstTime[" + ((array6 == null) ? string.Empty : array6.Length.ToString()) + "]",
			fieldValue = (((array6 == null) ? "null" : string.Join(", ", array6)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		GameObject arg2 = reader.ReadGameObject();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "bacteriaZoomToEnter",
			fieldValue = $"{arg2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
	}

	public override Timer getTimer(byte id)
	{
		return id switch
		{
			0 => new MedBedBlinkTimer(), 
			1 => new MedBedPowerTimer(), 
			2 => new AudioAnalyzerTimer(), 
			3 => new PodAnswerTimer(), 
			4 => new PodMedTimer(), 
			5 => new PodMedTimer0Timer(), 
			6 => new PodMedTimer1Timer(), 
			7 => new PodMedTimer2Timer(), 
			8 => new PodMedTimer3Timer(), 
			9 => new PodMedButtonTimer(), 
			10 => new MedBedComponentsTimer(), 
			11 => new MedBedSolvedTimer(), 
			12 => new HoloTimer(), 
			13 => new MedBedMatchingTimer(), 
			14 => new ScannedBacteriaTimer(), 
			15 => new MedBedCheckBacteriaTimer(), 
			16 => new BacteriaSolvedTimer(), 
			17 => new MedicinePuzzleScreenTimer(), 
			18 => new ReleaseMedicineTimer(), 
			19 => new AirlockSlotTimerTimer(), 
			20 => new CorridorScanTimer(), 
			21 => new SolveCrateTimer(), 
			22 => new SplicerMiddleTimer(), 
			23 => new SplicerBottomTimer(), 
			24 => new SplicedBottomTrackerTransition(), 
			25 => new ScannerSlidableTransition(), 
			26 => new HologramTimer(), 
			27 => new PressUIButtonAnimationTimer(), 
			28 => new CaptainsPodTimer(), 
			29 => new LockerTimer(), 
			30 => new ChipScreensTimer(), 
			31 => new CrateBlinkingTimer(), 
			32 => new RfidBlinkingTimer(), 
			33 => new AirlockKeypadTimer(), 
			34 => new WallHintTimer(), 
			35 => new RfidSlotTimer(), 
			36 => new RfidLEDTimer(), 
			37 => new MedBedEnableHoloTimer(), 
			38 => new UiBlinkTimer(), 
			39 => new LoadingSoundTimer(), 
			40 => new HoloCardTimer(), 
			_ => null, 
		};
	}
}
