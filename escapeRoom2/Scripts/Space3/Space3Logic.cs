using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using ExitGames.Client.Photon.StructWrapping;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;
using UnityEngine.VFX;

public class Space3Logic : LevelLogic, ISaveable
{
	public sealed class WelderParticleStopTimer : Timer
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

	public sealed class ElevatorSoundTimer : Timer
	{
		public int soundIndex;

		public override byte getTypeId()
		{
			return 1;
		}

		public ElevatorSoundTimer()
		{
		}

		public ElevatorSoundTimer(int soundIndex)
		{
			this.soundIndex = soundIndex;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in soundIndex, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			soundIndex = reader.ReadInt32();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("soundIndex: " + $"{soundIndex}");
			return stringBuilder.ToString();
		}
	}

	public sealed class OpenCompartmentSoundTimer : Timer
	{
		public GameObject soundRoot;

		public override byte getTypeId()
		{
			return 2;
		}

		public OpenCompartmentSoundTimer()
		{
		}

		public OpenCompartmentSoundTimer(GameObject soundRoot)
		{
			this.soundRoot = soundRoot;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.WriteGameObject(soundRoot);
		}

		public override void readData(FastBinaryReader reader)
		{
			soundRoot = reader.ReadGameObject();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("soundRoot: " + $"{soundRoot}");
			return stringBuilder.ToString();
		}
	}

	public sealed class ConnectorsTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 3;
		}

		public ConnectorsTimer()
		{
		}

		public ConnectorsTimer(int index)
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

	public sealed class CargoTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 4;
		}

		public CargoTimer()
		{
		}

		public CargoTimer(int index)
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

	public sealed class LadderTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 5;
		}

		public LadderTimer()
		{
		}

		public LadderTimer(int index)
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

	public sealed class LockerScreenTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 6;
		}

		public LockerScreenTimer()
		{
		}

		public LockerScreenTimer(int index)
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

	public sealed class FabricatorFixObjectTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 7;
		}

		public FabricatorFixObjectTimer()
		{
		}

		public FabricatorFixObjectTimer(int index)
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

	public sealed class FabricatorStartScanTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 8;
		}

		public FabricatorStartScanTimer()
		{
		}

		public FabricatorStartScanTimer(int index)
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

	public sealed class FabricatorScannedTimer : Timer
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

	public sealed class DelayedConnectorsInitTimer : Timer
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

	public sealed class DroneFlyoutTimer : Timer
	{
		public int index;

		public int drone;

		public override byte getTypeId()
		{
			return 11;
		}

		public DroneFlyoutTimer()
		{
		}

		public DroneFlyoutTimer(int index, int drone)
		{
			this.index = index;
			this.drone = drone;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in index, default(FastBinaryWriter.ForPrimitives));
			writer.Write(in drone, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			index = reader.ReadInt32();
			drone = reader.ReadInt32();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("index: " + $"{index}");
			stringBuilder.Append("drone: " + $"{drone}");
			return stringBuilder.ToString();
		}
	}

	public sealed class UpdateDroneCamTimer : Timer
	{
		public int index;

		public int targetIndex;

		public override byte getTypeId()
		{
			return 12;
		}

		public UpdateDroneCamTimer()
		{
		}

		public UpdateDroneCamTimer(int index, int targetIndex)
		{
			this.index = index;
			this.targetIndex = targetIndex;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in index, default(FastBinaryWriter.ForPrimitives));
			writer.Write(in targetIndex, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			index = reader.ReadInt32();
			targetIndex = reader.ReadInt32();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("index: " + $"{index}");
			stringBuilder.Append("targetIndex: " + $"{targetIndex}");
			return stringBuilder.ToString();
		}
	}

	public sealed class DronesTimer : Timer
	{
		public int index;

		public int droneIndex;

		public override byte getTypeId()
		{
			return 13;
		}

		public DronesTimer()
		{
		}

		public DronesTimer(int index, int droneIndex)
		{
			this.index = index;
			this.droneIndex = droneIndex;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in index, default(FastBinaryWriter.ForPrimitives));
			writer.Write(in droneIndex, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			index = reader.ReadInt32();
			droneIndex = reader.ReadInt32();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("index: " + $"{index}");
			stringBuilder.Append("droneIndex: " + $"{droneIndex}");
			return stringBuilder.ToString();
		}
	}

	public sealed class CargoChestConfirmTimer : Timer
	{
		public int index;

		public bool solved;

		public override byte getTypeId()
		{
			return 14;
		}

		public CargoChestConfirmTimer()
		{
		}

		public CargoChestConfirmTimer(int index, bool solved)
		{
			this.index = index;
			this.solved = solved;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in index, default(FastBinaryWriter.ForPrimitives));
			writer.Write(in solved, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			index = reader.ReadInt32();
			solved = reader.ReadBoolean();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("index: " + $"{index}");
			stringBuilder.Append("solved: " + $"{solved}");
			return stringBuilder.ToString();
		}
	}

	public sealed class SolveArtefactsTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 15;
		}

		public SolveArtefactsTimer()
		{
		}

		public SolveArtefactsTimer(int index)
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

	public sealed class ShieldCargoTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 16;
		}

		public ShieldCargoTimer()
		{
		}

		public ShieldCargoTimer(int index)
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

	public sealed class ShieldSolvedTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 17;
		}

		public ShieldSolvedTimer()
		{
		}

		public ShieldSolvedTimer(int index)
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

	public sealed class SphereRotateTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 18;
		}

		public SphereRotateTimer()
		{
		}

		public SphereRotateTimer(int index)
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

	public sealed class ToolLockerTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 19;
		}

		public ToolLockerTimer()
		{
		}

		public ToolLockerTimer(int index)
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
			return 20;
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

	public sealed class ElevatorKeycardTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 21;
		}

		public ElevatorKeycardTimer()
		{
		}

		public ElevatorKeycardTimer(int index)
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

	public sealed class WeldTargetedTimer : Timer
	{
		public int weldIndex;

		public override byte getTypeId()
		{
			return 22;
		}

		public WeldTargetedTimer()
		{
		}

		public WeldTargetedTimer(int weldIndex)
		{
			this.weldIndex = weldIndex;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in weldIndex, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			weldIndex = reader.ReadInt32();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("weldIndex: " + $"{weldIndex}");
			return stringBuilder.ToString();
		}
	}

	public sealed class WelderTimer : Timer
	{
		public int index;

		public int line1;

		public int line2;

		public override byte getTypeId()
		{
			return 23;
		}

		public WelderTimer()
		{
		}

		public WelderTimer(int index, int line1, int line2)
		{
			this.index = index;
			this.line1 = line1;
			this.line2 = line2;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in index, default(FastBinaryWriter.ForPrimitives));
			writer.Write(in line1, default(FastBinaryWriter.ForPrimitives));
			writer.Write(in line2, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			index = reader.ReadInt32();
			line1 = reader.ReadInt32();
			line2 = reader.ReadInt32();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("index: " + $"{index}");
			stringBuilder.AppendLine("line1: " + $"{line1}");
			stringBuilder.Append("line2: " + $"{line2}");
			return stringBuilder.ToString();
		}
	}

	public sealed class SphereCoverTimer : Timer
	{
		public int coverIndex;

		public override byte getTypeId()
		{
			return 24;
		}

		public SphereCoverTimer()
		{
		}

		public SphereCoverTimer(int coverIndex)
		{
			this.coverIndex = coverIndex;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in coverIndex, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			coverIndex = reader.ReadInt32();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("coverIndex: " + $"{coverIndex}");
			return stringBuilder.ToString();
		}
	}

	public enum CargoChemicalType
	{
		Flammable = 0,
		Oxidizing = 1,
		Radioactive = 2,
		Neutral = 3,
		Stars = 4
	}

	public enum CargoTitleType
	{
		Fixed = 0,
		Locked = 1,
		Missing = 2
	}

	private enum ElevatorKeycardScreen
	{
		InsertKeycard = 0,
		Checking = 1,
		Denied = 2,
		Granted = 3
	}

	private enum ElevatorTextType
	{
		None = -1,
		PlatformRaised = 0,
		PlatformLowered = 1,
		MovingForward = 2,
		MovingBackward = 3,
		Turning = 4
	}

	private enum ElevatorScreenType
	{
		Operating = 0,
		Warning = 1
	}

	public class ChestRow
	{
		[DontSave]
		public List<char> letters = new List<char>();

		[DontSave]
		public List<MaterialState> letterStates = new List<MaterialState>();

		[DontSave]
		public int slidableNodeIndex;

		public bool checkIconMatch(ChestIcon icon)
		{
			bool result = true;
			for (int i = 0; i < letters.Count; i++)
			{
				char value = letters[i];
				MaterialState materialState = letterStates[i];
				if (icon == null)
				{
					materialState.transitionTo("Off", 10f);
					result = false;
					continue;
				}
				bool flag = icon.letters.Contains(value);
				if (!flag)
				{
					result = false;
				}
				materialState.transitionTo("Off", 10f, (!flag) ? 1 : 0);
			}
			return result;
		}

		public void turnOffLight()
		{
			for (int i = 0; i < letters.Count; i++)
			{
				letterStates[i].transitionTo("Off", 10f);
			}
		}
	}

	public class ChestIcon
	{
		public enum IconType
		{
			Arrowhead = 0,
			Mercedes = 1,
			Romb = 2,
			Pentagon = 3,
			Funnel = 4
		}

		[DontSave]
		public IconType type;

		[DontSave]
		public string letters;

		[DontSave]
		public GameObject piece;
	}

	private enum WelderScreenType
	{
		LoadingBar = 0,
		Text = 1,
		Laser = 2
	}

	private enum LockerScreen
	{
		Checking = 0,
		LockerOpen = 1,
		Error = 2,
		Input = 3
	}

	public class ConnectorPuzzle
	{
		public enum Type
		{
			Whole = 0,
			Top = 1,
			Bottom = 2
		}

		public class Light
		{
			[DontSave]
			public List<SwapperPiece> connectedSwappers;

			[DontSave]
			public MaterialState light;

			[DontSave]
			public MaterialState lightBottom;

			[DontSave]
			public bool topStartsOn;

			[DontSave]
			public bool bottomStartsOn;
		}

		[DontSave]
		public GameObject root;

		[DontSave]
		public List<Light> lights;

		[DontSave]
		public SwapperPiece[] allSwappers;

		[DontSave]
		public SwapperPiece[] centerSwappers;

		[DontSave]
		public SwapperPiece[] batterySwappers;

		[DontSave]
		public RefArray<GameObject, MaterialState> selectors;

		[DontSave]
		public List<Type> batteryTypes;

		public int[] connectorOnBatterySlots;

		public SwapperPiece getOriginalSwapperAtPosition(Vector3 localPosition)
		{
			SwapperPiece result = batterySwappers[0];
			float num = float.MaxValue;
			SwapperPiece[] array = batterySwappers;
			foreach (SwapperPiece swapperPiece in array)
			{
				float num2 = Vector3.Distance(swapperPiece.startingLocalPosition, localPosition);
				if (num2 < num)
				{
					num = num2;
					result = swapperPiece;
				}
			}
			array = centerSwappers;
			foreach (SwapperPiece swapperPiece2 in array)
			{
				float num3 = Vector3.Distance(swapperPiece2.startingLocalPosition, localPosition);
				if (num3 < num)
				{
					num = num3;
					result = swapperPiece2;
				}
			}
			return result;
		}

		public bool checkSolution()
		{
			bool flag = true;
			List<SwapperPiece> list = new List<SwapperPiece>();
			for (int i = 0; i < batterySwappers.Length; i++)
			{
				SwapperPiece closest = getOriginalSwapperAtPosition(batterySwappers[i].transform.localPosition);
				list.Add(closest);
				if (Array.Exists(batterySwappers, (SwapperPiece x) => x == closest))
				{
					flag = false;
				}
			}
			int num = 0;
			foreach (Light light in lights)
			{
				(int, int) tuple = (0, 0);
				for (int num2 = 0; num2 < list.Count; num2++)
				{
					SwapperPiece item = list[num2];
					if (light.connectedSwappers.Contains(item))
					{
						if (batteryTypes[num2] == Type.Whole)
						{
							tuple = (tuple.Item1 + 1, tuple.Item2 + 1);
						}
						else if (batteryTypes[num2] == Type.Top)
						{
							tuple = (tuple.Item1 + 1, tuple.Item2);
						}
						else if (batteryTypes[num2] == Type.Bottom)
						{
							tuple = (tuple.Item1, tuple.Item2 + 1);
						}
					}
				}
				bool flag2 = (tuple.Item1 % 2 == 1 && !light.topStartsOn) || (tuple.Item1 % 2 == 0 && light.topStartsOn);
				bool flag3 = (tuple.Item2 % 2 == 1 && !light.bottomStartsOn) || (tuple.Item2 % 2 == 0 && light.bottomStartsOn);
				if (flag3 && flag2)
				{
					num++;
				}
				light.light.transitionTo(flag2 ? "Default" : "Off", 10f);
				if (light.lightBottom != null)
				{
					light.lightBottom.transitionTo(flag3 ? "Default" : "Off", 10f);
				}
			}
			if (flag)
			{
				return num == lights.Count;
			}
			return false;
		}
	}

	private enum FabricatorScreens
	{
		ScanButton = 0,
		Scanning = 1,
		Repairing = 2,
		NoObjectInScanner = 3,
		DoesntNeedRepair = 4,
		RepairDone = 5,
		RepairWrong = 6,
		Puzzle = 7
	}

	public class ShieldTriangleSet
	{
		[DontSave]
		public Ref<Transform> swivelPoint;

		[DontSave]
		public Ref<GameObject, Transform, TweenState> scaler;

		[DontSave]
		public Ref<Transform> rotateTriangle;

		[DontSave]
		public Trigger[] triggers;

		[DontSave]
		public Switch3D button;

		[DontSave]
		public MaterialState materialState;

		[DontSave]
		public List<GameObject> lasers;

		public ShieldTriangleSet(Ref<Transform> swiwelPoint, Ref<GameObject, Transform, TweenState> parent, Ref<Transform> rotateTriangle, Trigger[] triggers, Switch3D button, MaterialState materialState, List<GameObject> lasers)
		{
			swivelPoint = swiwelPoint;
			scaler = parent;
			this.rotateTriangle = rotateTriangle;
			this.triggers = triggers;
			this.button = button;
			this.materialState = materialState;
			this.lasers = lasers;
		}

		public void init(bool isActivated, float currentScale)
		{
			updateTriangleScale(currentScale);
			deactivate();
			scaler.Get<GameObject>(0).SetActive(value: false);
		}

		private void updateTriangleScale(float currentScale)
		{
			scaler.Get<TweenState>(0u).transitionTo("Scale", 3f, currentScale);
		}

		public void activate(float currentScale)
		{
			updateTriangleScale(currentScale);
			materialState.transitionTo("Transparent", 5f, 0f);
			foreach (GameObject laser in lasers)
			{
				laser.SetActive(value: true);
			}
		}

		public void deactivate()
		{
			materialState.transitionTo("Transparent", 5f);
			foreach (GameObject laser in lasers)
			{
				laser.SetActive(value: false);
			}
		}
	}

	public enum ShieldScreenType
	{
		ScanObjectButton = 0,
		Scanning = 1,
		Puzzle = 2,
		WeakPointsFound = 3,
		CutShieldButton = 4,
		Cutting = 5,
		CutComplete = 6,
		FinalReport = 7
	}

	private enum RPCs
	{
		TrailSolved = 0,
		TrailNotSolved = 1,
		CargoChestSolved = 2,
		CargoChestNotSolved = 3
	}

	private enum LevelPredicate
	{
		SolveShield = 0,
		SolveArtefacts = 1,
		SolveCargo = 2,
		SphereCover0 = 3,
		SphereCover1 = 4,
		SphereCover2 = 5,
		SphereCover3 = 6
	}

	private enum Puzzle
	{
		[PuzzleInfo("%PuzzleS3_1%", false)]
		Lockbox = 0,
		[PuzzleInfo("%PuzzleS3_2%", false)]
		Locker = 1,
		[PuzzleInfo("%PuzzleS3_3%", false)]
		ControlRoomDoor = 2,
		[PuzzleInfo("%PuzzleS3_4%", false)]
		Cargo = 3,
		[PuzzleInfo("%PuzzleS3_5%", false)]
		Welder = 4,
		[PuzzleInfo("%PuzzleS3_6%", false)]
		SphereShield = 5,
		[PuzzleInfo("%PuzzleS3_7%", false)]
		ElevatorDoor = 6,
		[PuzzleInfo("%PuzzleS3_8%", false)]
		UnweldingElevator = 7,
		[PuzzleInfo("%PuzzleS3_9%", false)]
		ArtifactRepair = 8,
		[PuzzleInfo("%PuzzleS3_10%", false)]
		Trail = 9,
		[PuzzleInfo("%PuzzleS3_11%", false)]
		Drones = 10,
		[PuzzleInfo("%PuzzleS3_12%", false)]
		Connectors = 11,
		[PuzzleInfo("%PuzzleS3_13%", false)]
		Artifacts = 12,
		[PuzzleInfo("%PuzzleS3_14%", false)]
		Unwelding = 13
	}

	private enum LockboxHint
	{
		PickUpManual = 0,
		LookAtBox = 1,
		Rules = 2,
		OneSolution = 3
	}

	private enum LockerHint
	{

	}

	private enum ControlRoomDoorHint
	{
		GetKey = 0,
		OpenDoor = 1
	}

	private enum CargoHint
	{
		OpenDoor = 0,
		PickUpCargo = 1,
		FireRule = 2,
		OxygenRule = 3,
		RadioactiveRule = 4
	}

	private enum WelderHint
	{
		PickUpContainer = 0,
		GetWelderAndManual = 1,
		ConnectThis = 2,
		FirstStep = 3,
		SecondStep = 4,
		Profit = 5
	}

	private enum SphereShieldHint
	{
		PressButton = 0,
		UseControls = 1,
		PlaceTriangle = 2,
		GetSeven = 3,
		PlaceOthers = 4
	}

	private enum ElevatorDoorHint
	{
		GetKey = 0,
		PlaceKey = 1
	}

	private enum UnweldingElevatorHint
	{

	}

	private enum ArtifactRepairHint
	{
		GetArtifactAndHints = 0,
		GetHint = 1,
		PlaceArtifact = 2,
		Rules = 3,
		SolveOthers = 4
	}

	private enum TrailHint
	{

	}

	private enum DronesHint
	{

	}

	private enum ConnectorsHint
	{

	}

	private enum ArtifactsHint
	{
		GetConnectors = 0,
		GetFabricator = 1,
		GetDrone = 2,
		GetTrail = 3,
		PlaceAll = 4
	}

	private enum UnweldingHint
	{

	}

	public sealed class FabricatorButtonFixPacket : Packet
	{
		public bool isCorrectSolution;

		public override byte getTypeId()
		{
			return 0;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in isCorrectSolution, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			isCorrectSolution = reader.ReadBoolean();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("isCorrectSolution: " + $"{isCorrectSolution}");
			return stringBuilder.ToString();
		}
	}

	public sealed class WeldPacket : Packet
	{
		public int weldIndex;

		public override byte getTypeId()
		{
			return 1;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in weldIndex, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			weldIndex = reader.ReadInt32();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("weldIndex: " + $"{weldIndex}");
			return stringBuilder.ToString();
		}
	}

	public sealed class ElevatorMovePacket : Packet
	{
		public bool isPos;

		public Vector3 position;

		public Quaternion rotation;

		public override byte getTypeId()
		{
			return 2;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in isPos, default(FastBinaryWriter.ForPrimitives));
			writer.WriteVector3(in position);
			writer.WriteQuaternion(in rotation);
		}

		public override void readData(FastBinaryReader reader)
		{
			isPos = reader.ReadBoolean();
			position = reader.ReadVector3();
			rotation = reader.ReadQuaternion();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("isPos: " + $"{isPos}");
			stringBuilder.AppendLine("position: " + $"{position}");
			stringBuilder.Append("rotation: " + $"{rotation}");
			return stringBuilder.ToString();
		}
	}

	private List<GameObject> blinkingErrorsActivated = new List<GameObject>();

	private float blinkingTimer;

	[Header("Hazmat Cargo")]
	[DontSave]
	public Item[] cargoPieces;

	[DontSave]
	public Slot[] cargoSlots;

	[DontSave]
	public GameObject cargoEffect;

	[DontSave]
	public GameObject[] cargoTitles;

	[DontSave]
	public GameObject[] cargoCorrectTexts;

	[DontSave]
	public GameObject[] cargoErrorTexts;

	[DontSave]
	public MaterialState[] cargoErrorsMaterialStates;

	[DontSave]
	public TweenState cargoRoomDoor;

	[DontSave]
	public TweenState hazmatCargoDoor;

	[DontSave]
	public TweenState cargoTween;

	[DontSave]
	public GameObject cargoPallet;

	[DontSave]
	private List<CargoChemicalType> cargoStartingTypes = new List<CargoChemicalType>
	{
		CargoChemicalType.Oxidizing,
		CargoChemicalType.Oxidizing,
		CargoChemicalType.Oxidizing,
		CargoChemicalType.Oxidizing,
		CargoChemicalType.Oxidizing,
		CargoChemicalType.Oxidizing,
		CargoChemicalType.Flammable,
		CargoChemicalType.Radioactive,
		CargoChemicalType.Radioactive,
		CargoChemicalType.Neutral,
		CargoChemicalType.Oxidizing,
		CargoChemicalType.Flammable,
		CargoChemicalType.Radioactive,
		CargoChemicalType.Flammable,
		CargoChemicalType.Radioactive,
		CargoChemicalType.Radioactive,
		CargoChemicalType.Flammable,
		CargoChemicalType.Flammable,
		CargoChemicalType.Radioactive,
		CargoChemicalType.Radioactive
	};

	private List<int> cargoButtons = new List<int>();

	private bool solvedCargo;

	[Header("Elevator")]
	[DontSave]
	public Slot cargoElevatorSlot;

	[DontSave]
	public GameObject[] elevatorKeypadScreens;

	[DontSave]
	public Zoomable[] elevatorReachableZooms;

	[DontSave]
	public Ref<GameObject, Transform, Interactive> elevator;

	[DontSave]
	public Ref<Transform> elevatorTopPart;

	[DontSave]
	public CharacterPose elevatorPose;

	[DontSave]
	public Dial elevatorRotateDial;

	[DontSave]
	public Slidable elevatorMoveHandle;

	[DontSave]
	public TweenState cargoElevatorDoor;

	[DontSave]
	public GameObject[] elevatorBackBox;

	[DontSave]
	public GameObject[] elevatorFrontBox;

	[DontSave]
	public GameObject elevatorObstacle;

	[DontSave]
	public AnimationSampler elevatorAnimation;

	[DontSave]
	public GameObject[] elevatorScreens;

	[DontSave]
	private NavMeshQueryFilter elevatorNavMeshQuery;

	private bool isMovingElevator;

	private bool isRaisingElevator;

	[DontSave]
	private Vector3 elevatorMoveHandleStartingPosition;

	[DontSave]
	private EventInstance[] elevatorSoundInstances;

	private Quaternion elevatorStartingRotation;

	private float elevatorCurrentAngle;

	private float elevatorUpdatingAngle;

	private bool isLoweringElevator;

	private bool elevatorHitOnUp;

	private ElevatorScreenType currentElevatorScreenType;

	private bool elevatorCheck;

	[DontSave]
	private List<ChestRow> chestRows = new List<ChestRow>();

	[DontSave]
	private List<ChestIcon> chestIcons = new List<ChestIcon>();

	[Header("Cargo Chest")]
	[DontSave]
	public Switch3D newChestConfirmButton;

	[DontSave]
	public GameObject[] chestSlidablePieces;

	[DontSave]
	public Zoomable newChestLockZoom;

	[DontSave]
	public List<Interactive> newChestLockZoomInteractives = new List<Interactive>();

	[DontSave]
	public SlidableGraph newChestSlidableGraph;

	[DontSave]
	public MaterialState[] chestLockLetters;

	[DontSave]
	public Switch3D cargoLockLid;

	private bool ladderRaised;

	[Header("Welder")]
	[DontSave]
	public Ref<Transform, VisualEffect> welderHitParticles;

	[DontSave]
	public Ref<Transform, VisualEffect> welderHitDefaultParticles;

	private bool solvedWelderGraph;

	private float welderSolvedTimer;

	private int[] welderCurrentLineSwaps;

	private int[] welderCurrentGateSwaps;

	private Dictionary<int, int[]> welderLineCombinations = new Dictionary<int, int[]>();

	private Dictionary<int, int[]> welderGateCombinations = new Dictionary<int, int[]>();

	[DontSave]
	private RaycastHit[] sharedHits = new RaycastHit[16];

	[DontSave]
	private EventInstance welderSoundInstance;

	private List<Interactive> hitWelds = new List<Interactive>();

	private ToolState welderToolState;

	[Header("Tool System")]
	[DontSave]
	public TweenState[] toolLockerDoors;

	[DontSave]
	public Switch3D toolLockerOpenButton;

	[DontSave]
	public Switch3D toolLockerXButton;

	[DontSave]
	public Switch3D[] toolLockerNumberButtons;

	[DontSave]
	public Zoomable toolLockerZoomable;

	[DontSave]
	public GameObject[] lockerScreens;

	private List<string> toolLockerSolutions = new List<string>
	{
		"100", "113", "115", "175", "220", "223", "224", "225", "330", "343",
		"408", "415", "459", "620", "678", "745", "755", "763", "793", "794",
		"823", "893", "909", "967", "999"
	};

	private List<int> toolLockerCurrentValues = new List<int>();

	private bool lockerOpened;

	private bool solvedLocker;

	[DontSave]
	private List<ConnectorPuzzle> connectorPuzzles;

	private int currentConnectorPuzzle;

	private bool solvedConnectors;

	private List<int> trailCurrentValues = new List<int> { 4, 8, 13, 21, 29, 31 };

	[DontSave]
	private List<int> trailSolution = new List<int> { 5, 10, 14, 19, 24, 33 };

	private bool solvedTrail;

	private readonly float welderMaxDuration = 0.03f;

	[DontSave]
	private List<List<Collider>> sphereCoverColliders = new List<List<Collider>>();

	private Interactive welderCurrentTarget;

	private float welderCurrentTimer;

	[DontSave]
	private MaterialState[] droneMaterialStates;

	[DontSave]
	public RefArray<HDAdditionalReflectionData, Transform> droneReflectionProbes;

	[DontSave]
	public RefArray<HDAdditionalReflectionData, Transform> droneInventoryReflectionProbes;

	private bool[] dronesPickedUp = new bool[4];

	private int dronesFlew;

	private int droneCurrentCamIndex;

	private bool[] solvedDrones = new bool[4];

	[Header("Fabricator")]
	[DontSave]
	public RefArray<Slot, Collider> gemSlots;

	[DontSave]
	public Ref<Item, MeshRenderer, Transform> brokenGem;

	[DontSave]
	public GameObject fabricatorPlane;

	[DontSave]
	public Switch3D fabricatorFixObjectButton;

	[DontSave]
	public GameObject[] fabricatorTexts;

	[DontSave]
	public Switch3D fabricatorStartScanButton;

	[DontSave]
	public Slidable[] fabricatorSlidables;

	[DontSave]
	public Camera[] fabricatorCameras;

	[DontSave]
	public Slot fabricatorSlot;

	[DontSave]
	public Ref<Renderer, MaterialState, GameObject> fixedArtifact;

	[DontSave]
	public GameObject fabricatorPlaneProjections;

	[DontSave]
	private List<float> fabricatorSolution = new List<float> { 0.24732377f, 0.45274305f, 0.6526998f };

	[DontSave]
	private float fabricatorWiggleRoom = 0.1f;

	[DontSave]
	private Vector3 fabricatorSlotStartingPosition;

	[DontSave]
	private Quaternion fabricatorSlotStartingRotation;

	[DontSave]
	private EventInstance fabricatorRepairSoundInstance;

	private bool fixedGem;

	[Header("Shield")]
	[DontSave]
	public TweenState shieldScanningUiAnimation;

	[DontSave]
	public GameObject[] shieldScreens;

	[DontSave]
	public TweenState shieldControlsLid;

	[DontSave]
	public RefArray<Transform> shieldRotateTriangleEmpties;

	[DontSave]
	public RefArray<Switch3D, GameObject, MaterialState> shieldUIAttempts;

	[DontSave]
	public GameObject[] shieldSphereXs;

	[DontSave]
	public GameObject sphereScanningAnimation;

	[DontSave]
	public Ref<GameObject, MaterialState> shield;

	[DontSave]
	public RefArray<GameObject, Collider, MaterialState> shieldSpheres;

	[DontSave]
	public RefArray<Transform> triangleSwivels;

	[DontSave]
	public RefArray<GameObject, Transform, TweenState> triangleParents;

	[DontSave]
	public Ref<Zoomable, GameObject> shieldScreenZoom;

	[DontSave]
	public Ref<Slidable, GameObject, Transform> shieldTriangleScaleSlidable;

	[DontSave]
	public Ref<Slidable, GameObject, Transform> shieldTriangleRotationSlidable;

	[DontSave]
	public Ref<Slidable, GameObject, Transform> shieldSphereRotationSlidable;

	[DontSave]
	public Ref<Switch3D> shieldScanCargoButton;

	[DontSave]
	public Trigger[] triangleTriggers4;

	[DontSave]
	public Trigger[] triangleTriggers3;

	[DontSave]
	public Trigger[] triangleTriggers2;

	[DontSave]
	public Trigger[] triangleTriggers1;

	[DontSave]
	public Trigger[] triangleTriggers0;

	[DontSave]
	public Text shieldSpheresLeftText;

	[DontSave]
	public Ref<Switch3D, GameObject, TweenState> shieldCutNowButton;

	[DontSave]
	public float shieldSphereRotationSpeed;

	[DontSave]
	public float shieldTriangleRotationSpeed;

	[DontSave]
	public float shieldRotateSlerpTime;

	private List<GameObject> shieldEnteredSpheres = new List<GameObject>();

	private bool cutShield;

	private Quaternion shieldRotatingTriangle;

	[DontSave]
	private Quaternion shieldTriangleStartingRotation;

	private Quaternion shieldRotatingSphere;

	[DontSave]
	private Quaternion[] shieldTrianglesStartingRotations;

	[DontSave]
	private Quaternion shieldSphereStartingRotation;

	private bool isShieldChangingTriangle;

	[DontSave]
	private List<ShieldTriangleSet> shieldTriangleSets = new List<ShieldTriangleSet>();

	private List<float> shieldTriangleScales = new List<float>();

	private List<float> shieldTriangleRotations = new List<float>();

	private float[] shieldTriangleRotationsCurrent;

	private List<float> shieldSphereSwivelValues = new List<float>();

	private float[] shieldSphereSwivelCurrentValues;

	private float shieldSphereRotationValue;

	private float shieldSphereRotationsCurrent;

	private int shieldCurrentSelectedSet;

	private bool solvedShield;

	private float shieldSolvedTime = 2f;

	private bool shieldTrianglePressed;

	private bool initingShield = true;

	private bool isCargoScanned;

	[DontSave]
	public RefArray<Item, MaterialState> artefacts;

	[Header("Generated Variables")]
	[DontSave]
	public GameObject welderManual;

	[DontSave]
	public Item welderBox;

	[DontSave]
	public Item cargoManual;

	[DontSave]
	public GameObject chestKeyGO;

	[DontSave]
	public GameObject[] chestBottles;

	[DontSave]
	public Ref<GameObject, Switch3D> endPlane;

	[DontSave]
	public Item lockerKey;

	[DontSave]
	public Ref<Transform, Camera> droneDummyCamera;

	[DontSave]
	public MaterialState[] droneFlyMaterials;

	[DontSave]
	public AnimationSampler droneFlyAnimation;

	[DontSave]
	public TweenState welderTween;

	[DontSave]
	public SwapperPiece[] welderGatesSwapperPieces;

	[DontSave]
	public SwapperPiece[] welderLineSwapperPieces;

	[DontSave]
	public Swapper welderGatesSwapper;

	[DontSave]
	public Swapper welderLineSwapper;

	[DontSave]
	public GameObject[] welderTexts;

	[DontSave]
	public RefArray<Transform, MaterialState> welderWavyLines;

	[DontSave]
	public Transform[] welderGates;

	[DontSave]
	public Switch3D[] elevatorRaiseButtons;

	[DontSave]
	public AnimationSampler[] trailHintAnimations;

	[DontSave]
	public Switch3D trailRestartButton;

	[DontSave]
	public GameObject shieldCuttingSound;

	[DontSave]
	public Transform[] elevatorSounds;

	[DontSave]
	public List<Item> tablets;

	[DontSave]
	public Collider[] elevatorIgnoreColliders;

	[DontSave]
	public Transform trailZoomTransform;

	[DontSave]
	public RefArray<TweenState, GameObject> tabletLockScreens;

	[DontSave]
	public MaterialState[] tabletScreens;

	[DontSave]
	public List<Slidable> tabletLockSlidables;

	[DontSave]
	public GameObject controlRoomDoorParticles;

	[DontSave]
	public GameObject cargoRoomDoorParticles;

	[DontSave]
	public TweenState connectorTransitionTween;

	[DontSave]
	public GameObject cargoHoles;

	[DontSave]
	public TweenState cargoRaiseTween;

	[DontSave]
	public Rigidbody elevatorRb;

	[DontSave]
	public AnimationSampler artefactSolvedAnimationSampler;

	[DontSave]
	public GameObject[] artefactSpinAnimations;

	[DontSave]
	public GameObject artefactSolvedParticles;

	[DontSave]
	public MaterialState shieldDissolve;

	[DontSave]
	public GameObject WelderStartParticles;

	[DontSave]
	public Transform welderShootStart;

	[DontSave]
	public GameObject welderHitPoint;

	[DontSave]
	public RefArray<ToolTarget, MaterialState> welderTargets;

	[DontSave]
	public TweenState trailArtefactTween;

	[DontSave]
	public Sequence trailArtefactDoorSeq;

	[DontSave]
	public GameObject[] trailImages;

	[DontSave]
	public TweenState droneArtifactTween;

	[DontSave]
	public TweenState droneSlotsTween;

	[DontSave]
	public Slot droneSlot;

	[DontSave]
	public TweenState[] droneButtonDoors;

	[DontSave]
	public Ref<GameObject, MaterialState> droneCameraDisabled;

	[DontSave]
	public Ref<Sequence, GameObject> droneDoorSeq;

	[DontSave]
	public TweenState shieldCuttingUiTween;

	[DontSave]
	public GameObject sphereCuttingAnimation;

	[DontSave]
	public GameObject[] shieldTriangleLasers;

	[DontSave]
	public MaterialState[] shieldTriangleMaterialStates;

	[DontSave]
	public GameObject cargoObstacle;

	[DontSave]
	public Switch3D cargoDoorHandle;

	[DontSave]
	public Switch3D welderCutterBoxButton;

	[DontSave]
	public Switch3D welderCutterBoxLid;

	[DontSave]
	public Draggable cargoBattery;

	[DontSave]
	public GameObject[] elevatorSpeedBars;

	[DontSave]
	public GameObject[] elevatorTexts;

	[DontSave]
	public TweenState elevatorKeypadTween;

	[DontSave]
	public GameObject fabricatorRobotSparks;

	[DontSave]
	public TweenState fabricatorRobotTween;

	[DontSave]
	public TweenState fabricatorLoadingTween;

	[DontSave]
	public GameObject[] welderScreens;

	[DontSave]
	public RefArray<Switch3D, MaterialState> wallHintButtons;

	[DontSave]
	public GameObject[] wallHintHolos;

	[DontSave]
	public GameObject controlRoomObstacle;

	[DontSave]
	public TweenState controlDoor;

	[DontSave]
	public MaterialState[] toolLockerUnderlines;

	[DontSave]
	public GameObject[] lockerNumbers2;

	[DontSave]
	public GameObject[] lockerNumbers1;

	[DontSave]
	public GameObject[] lockerNumbers0;

	[DontSave]
	public Transform sphere;

	[DontSave]
	public MaterialState artefactPlateMaterialState;

	[DontSave]
	public Swapper[] connectorSwappers;

	[DontSave]
	public Sequence ladderSequence;

	[DontSave]
	public TweenState elevatorRaiseTween;

	[DontSave]
	public Item controlRoomKey;

	[DontSave]
	public Slot controlRoomSlot;

	[DontSave]
	public GameObject obstacleSphere;

	[DontSave]
	public RefArray<BoxCollider, GameObject> poseColliders;

	[DontSave]
	public Sequence sphereSequence;

	[DontSave]
	public Ref<LineRenderer, GameObject> laserLineRenderer;

	[DontSave]
	public GameObject finalLiftUpPosition;

	[DontSave]
	public Item[] drones;

	[DontSave]
	public Item droneArtifact;

	[DontSave]
	public Renderer droneCameraDisplay;

	[DontSave]
	public Switch3D[] droneButtons;

	[DontSave]
	public Ref<Item, Transform, GameObject> trailArtifact;

	[DontSave]
	public Switch3D[] trailButtons;

	[DontSave]
	public Item connectorsArtifact;

	[DontSave]
	public SwapperPiece[] connectors3BatterySwappers;

	[DontSave]
	public SwapperPiece[] connectors3CenterSwappers;

	[DontSave]
	public MaterialState[] connectors3Lights;

	[DontSave]
	public SwapperPiece[] connectors2CenterSwappers;

	[DontSave]
	public SwapperPiece[] connectors2BatterySwappers;

	[DontSave]
	public MaterialState[] connectors2Lights;

	[DontSave]
	public GameObject[] connectors;

	[DontSave]
	public SwapperPiece[] connectors1BatterySwappers;

	[DontSave]
	public SwapperPiece[] connectors1CenterSwappers;

	[DontSave]
	public SwapperPiece[] connectors1AllSwappers;

	[DontSave]
	public SwapperPiece[] connectors2AllSwappers;

	[DontSave]
	public SwapperPiece[] connectors3AllSwappers;

	[DontSave]
	public MaterialState[] connectors1Lights;

	[DontSave]
	public RefArray<GameObject, MaterialState> connectors1SelectionLights;

	[DontSave]
	public RefArray<GameObject, MaterialState> connectors2SelectionLights;

	[DontSave]
	public RefArray<GameObject, MaterialState> connectors3SelectionLights;

	[DontSave]
	public Zoomable[] sphereZooms;

	[DontSave]
	public RefArray<GameObject, MaterialState> sphereCovers;

	[DontSave]
	public Item welder;

	[DontSave]
	public GameObject[] blinkingErrors;

	[DontSave]
	public TweenState sphereOpenDoors;

	[DontSave]
	public TweenState sphereCloseScreen;

	[DontSave]
	public RefArray<GameObject, Item, Transform, TweenState> food;

	[DontSave]
	public ParticleSystem foodVFXInventory;

	[DontSave]
	public Transform foodVFXTransformInventory;

	public override void onRPCCalled(int type)
	{
		switch (type)
		{
		case 0:
			solveTrail();
			break;
		case 1:
			trailNotSolved();
			break;
		case 2:
			solveChestLock();
			break;
		case 3:
			cargoChestNotSolved();
			break;
		}
	}

	public override void onInitPredicates()
	{
		game.registerPredicate(LevelPredicate.SolveShield, 1f, false, () => checkIsShieldSolved());
		game.registerPredicate(LevelPredicate.SolveArtefacts, 1f, false, () => checkArtefactSlots());
		game.registerPredicate(LevelPredicate.SolveCargo, 1f, false, () => isCargoFull() && checkFlammable() && checkRadioactive() && checkOxidizing());
		game.registerPredicate(LevelPredicate.SphereCover0, 0.5f, false, () => isSphereCoverSolved(0));
		game.registerPredicate(LevelPredicate.SphereCover1, 0.5f, false, () => isSphereCoverSolved(1));
		game.registerPredicate(LevelPredicate.SphereCover2, 0.5f, false, () => isSphereCoverSolved(2));
		game.registerPredicate(LevelPredicate.SphereCover3, 0.5f, false, () => isSphereCoverSolved(3));
	}

	public override void onLevelPredicateDone(int type, int doneCounter)
	{
		switch (type)
		{
		case 0:
			solveShield();
			break;
		case 1:
			startSolveArtefactSlotsTimer();
			break;
		case 2:
			solveCargo();
			break;
		case 3:
			solveSphereCover(0);
			break;
		case 4:
			solveSphereCover(1);
			break;
		case 5:
			solveSphereCover(2);
			break;
		case 6:
			solveSphereCover(3);
			break;
		}
	}

	public override void onInit()
	{
		game.syncPlayerRays = true;
		Interactive.linkInteractives(shieldScanCargoButton.Get<Switch3D>(), shieldTriangleScaleSlidable.Get<Slidable>(0), shieldTriangleRotationSlidable.Get<Slidable>(0), shieldSphereRotationSlidable.Get<Slidable>(0), shieldUIAttempts[0].Get<Switch3D>(0), shieldUIAttempts[1].Get<Switch3D>(0), shieldUIAttempts[2].Get<Switch3D>(0));
		Interactive.linkInteractives(elevator.Get<Interactive>(0u), elevatorMoveHandle, elevatorRotateDial, elevatorPose);
		Interactive.linkInteractives(droneButtons[0], droneButtons[1], droneButtons[2], droneButtons[3]);
		Interactive.linkInteractives(trailButtons[0], trailButtons[1], trailButtons[2], trailButtons[3], trailButtons[4], trailButtons[5]);
		Interactive.linkInteractives(fabricatorSlidables[0], fabricatorSlidables[1], fabricatorSlidables[2], fabricatorFixObjectButton);
		Interactive.linkInteractives(toolLockerNumberButtons[0], toolLockerNumberButtons[1], toolLockerNumberButtons[2], toolLockerNumberButtons[3], toolLockerNumberButtons[4], toolLockerNumberButtons[5], toolLockerNumberButtons[6], toolLockerNumberButtons[7], toolLockerNumberButtons[8], toolLockerNumberButtons[9], toolLockerOpenButton, toolLockerXButton);
		GameObject[] array = blinkingErrors;
		foreach (GameObject item in array)
		{
			blinkingErrorsActivated.Add(item);
		}
		foreach (Ref<Slot, Collider> gemSlot in gemSlots)
		{
			((Slot)gemSlot).targetable = false;
		}
		initToolLockers();
		initElevator();
		initFabricator();
		initHazmatCargo();
		initWelder();
		initControlRoom();
		initChestLock();
		initWallHints();
		initSphereCovers();
		initConnectors();
		initDrones();
		initShield();
		initTrail();
		initArtefact();
	}

	public override void onUpdate()
	{
		if (blinkingErrorsActivated.Count > 0)
		{
			blinkingTimer -= Time.deltaTime;
			if (blinkingTimer <= 0f)
			{
				bool activeSelf = blinkingErrorsActivated[0].activeSelf;
				blinkingTimer = ((!activeSelf) ? 1f : 0.5f);
				foreach (GameObject item in blinkingErrorsActivated)
				{
					item.SetActive(!activeSelf);
				}
			}
		}
		updateElevator();
		onShieldUpdate();
		onFabricatorUpdate();
		onDroneUpdate();
		onTrailUpdate();
	}

	public override void onLateUpdate()
	{
		onWelderLateUpdate();
	}

	public override void onSwitch3D(Switch3D targetSwitch, Switch3DEvent switchEvent)
	{
		if (targetSwitch == endPlane.Get<Switch3D>(0f) && switchEvent == Switch3DEvent.Start)
		{
			game.levelCompleted();
		}
		onElevatorButtons(targetSwitch, switchEvent);
		if (targetSwitch == trailRestartButton && switchEvent == Switch3DEvent.On)
		{
			onTrailHintRestartButton();
		}
		int num = Array.IndexOf(trailButtons, targetSwitch);
		if (num >= 0)
		{
			onTrailButton(num, switchEvent);
		}
		onShieldButtons(targetSwitch, switchEvent);
		if (switchEvent == Switch3DEvent.On || switchEvent == Switch3DEvent.Off)
		{
			int num2 = wallHintButtons.IndexOf(targetSwitch, 0);
			if (num2 >= 0)
			{
				onWallHintButton(switchEvent, num2);
			}
		}
		if (switchEvent == Switch3DEvent.On)
		{
			int num3 = Array.IndexOf(toolLockerNumberButtons, targetSwitch);
			if (num3 >= 0)
			{
				onToolLockerNumber(num3);
			}
			if (targetSwitch == toolLockerXButton)
			{
				onToolLockerX();
			}
			if (targetSwitch == toolLockerOpenButton)
			{
				onToolLockerOpen();
			}
			if (targetSwitch == cargoDoorHandle)
			{
				onCargoDoorHandle();
			}
		}
		if (switchEvent == Switch3DEvent.On && targetSwitch == welderCutterBoxButton)
		{
			onWelderCutterBoxButton();
		}
		if (switchEvent == Switch3DEvent.Off && targetSwitch == welderCutterBoxLid)
		{
			onWelderCutterBoxClose();
		}
		if (targetSwitch == fabricatorFixObjectButton && switchEvent == Switch3DEvent.On)
		{
			onFabricatorFixObjectButton();
		}
		if (targetSwitch == fabricatorStartScanButton)
		{
			onFabricatorStartScanButton();
		}
		if (switchEvent != Switch3DEvent.On && switchEvent != Switch3DEvent.Off)
		{
			return;
		}
		if (switchEvent == Switch3DEvent.On && targetSwitch == newChestConfirmButton)
		{
			onCargoChestConfirmButton();
		}
		int num4 = Array.IndexOf(droneButtons, targetSwitch);
		if (num4 >= 0 && switchEvent == Switch3DEvent.On)
		{
			onDroneButton(targetSwitch, num4);
		}
		for (int i = 0; i < food.Length; i++)
		{
			if (targetSwitch.transform.parent.gameObject == food[i].Get<GameObject>(0) && switchEvent == Switch3DEvent.Start)
			{
				if (food[i].Get<TweenState>((short)0) != null)
				{
					food[i].Get<TweenState>((short)0).transitionTo("NewState", 4f);
				}
				else
				{
					OnFoodEaten(i);
				}
			}
		}
	}

	public override void onSlot(Slot targetSlot)
	{
		if (gemSlots.IndexOf(targetSlot, 0) >= 0)
		{
			onArtefactSlot(targetSlot);
		}
		if (targetSlot == droneSlot)
		{
			onDroneSlot();
		}
		if (targetSlot == cargoElevatorSlot)
		{
			onElevatorKeycardSlot();
		}
		if (targetSlot == controlRoomSlot)
		{
			openControlDoor();
		}
		int num = Array.IndexOf(cargoSlots, targetSlot);
		if (num >= 0)
		{
			onCargoSlot(targetSlot, num);
		}
	}

	public override void onRemoveFromSlot(Slot targetSlot, Item item)
	{
		if (targetSlot == fabricatorSlot)
		{
			onRemoveFromFabricatorSlot(item);
		}
		if (gemSlots.IndexOf(targetSlot, 0) >= 0)
		{
			onArtefactRemoveFromSlot(item);
		}
		int num = Array.IndexOf(cargoSlots, targetSlot);
		if (num >= 0)
		{
			onRemoveFromCargoSlot(num);
		}
	}

	public override void onAddToInventory(Item item)
	{
		if (item == controlRoomKey)
		{
			controlRoomKey.targetPriority = -1;
		}
		int num = Array.IndexOf(drones, item);
		if (num >= 0)
		{
			onDroneAddToInventory(num);
		}
	}

	public override void onRemoveFromInventory(Item item)
	{
		int num = Array.IndexOf(drones, item);
		if (num == droneCurrentCamIndex)
		{
			onDroneRemoveFromInventory(num);
		}
	}

	public override void onSwapper(Swapper swapper, Swapper.SwapperEvent swapperEvent)
	{
		onConnectorSwapper(swapper, swapperEvent);
		onWelderGatesSwapper(swapper, swapperEvent);
		onWelderLineSwapper(swapper, swapperEvent);
	}

	public override void onTool(Item tool, ToolContext context)
	{
		onWelderTool(tool, context);
	}

	public override void onPose(NetPlayerId playerId, CharacterPose characterPose, CharacterPoseState poseEvent)
	{
		switch (poseEvent)
		{
		case CharacterPoseState.TransitionIn:
			if (!(characterPose == elevatorPose))
			{
				break;
			}
			elevatorObstacle.SetActive(value: false);
			if (playerId != game.localPlayerData.id)
			{
				break;
			}
			enableElevatorControls();
			{
				foreach (Ref<BoxCollider, GameObject> poseCollider in poseColliders)
				{
					((GameObject)poseCollider).SetActive(value: false);
				}
				break;
			}
		case CharacterPoseState.TransitionOut:
			if (!(characterPose == elevatorPose))
			{
				break;
			}
			elevatorObstacle.SetActive(value: true);
			if (isRaisingElevator || elevatorRaiseTween.findStateByName("Raise").weight < 0.9f)
			{
				isLoweringElevator = true;
				isRaisingElevator = false;
				elevatorRaiseTween.transitionTo("Raise", 2f);
				elevatorPose.targetable = false;
			}
			if (playerId != game.localPlayerData.id)
			{
				break;
			}
			disableElevatorControls();
			{
				foreach (Ref<BoxCollider, GameObject> poseCollider2 in poseColliders)
				{
					((GameObject)poseCollider2).SetActive(value: true);
				}
				break;
			}
		}
	}

	public override void onSlidableMoved(Slidable slidable, MoveEvent moveEvent)
	{
		if (slidable == elevatorMoveHandle)
		{
			isMovingElevator = moveEvent == MoveEvent.Moved;
			if (moveEvent == MoveEvent.Released)
			{
				slidable.transform.localPosition = elevatorMoveHandleStartingPosition;
			}
		}
		if (slidable == shieldTriangleScaleSlidable.Get<Slidable>(0))
		{
			onShieldScaleTrianglesSlidable(moveEvent);
		}
		if (slidable == shieldTriangleRotationSlidable.Get<Slidable>(0))
		{
			onShieldTriangleRotationSlidable(moveEvent);
		}
		if (slidable == shieldSphereRotationSlidable.Get<Slidable>(0))
		{
			onShieldSphereRotationSlidable(moveEvent);
		}
		int num = Array.IndexOf(fabricatorSlidables, slidable);
		if (num >= 0)
		{
			onFabricatorSlidable(num);
		}
		int num2 = tabletLockSlidables.IndexOf(slidable);
		if (num2 >= 0 && moveEvent == MoveEvent.Released && UnityUtils.closeEnough(tabletLockSlidables[num2].value, 1f))
		{
			tabletLockSlidables[num2].targetable = false;
			tabletLockSlidables[num2].snapMode = Slidable.SnapMode.DontSnap;
			tabletLockScreens[num2].Get<TweenState>(0).transitionTo("Off");
			tabletScreens[num2].transitionTo("On");
		}
	}

	public override void onDialMoved(Dial dial, MoveEvent moveEvent)
	{
		if (dial == elevatorRotateDial)
		{
			elevatorRotate(dial.angleChange * -0.15f);
		}
	}

	public override void onTweenTransitionDone(TweenState tweenState, string state)
	{
		int num = tabletLockScreens.IndexOf(tweenState, 0);
		if (num >= 0)
		{
			tabletLockScreens[num].Get<GameObject>(0f).SetActive(value: false);
			game.invalidateItemImpostors(tablets[num]);
		}
		if (tweenState == trailArtefactTween)
		{
			onTrailArtefactTweenDone();
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
		if (sequence == trailArtefactDoorSeq)
		{
			onTrailArtefactDoorSeqDone();
		}
	}

	public override void onTrigger(Trigger trigger, TriggerEvent triggerEvent)
	{
		foreach (ShieldTriangleSet shieldTriangleSet in shieldTriangleSets)
		{
			if (Array.IndexOf(shieldTriangleSet.triggers, trigger) >= 0)
			{
				onShieldTriangleTrigger(triggerEvent);
			}
		}
	}

	private void initControlRoom()
	{
		controlRoomDoorParticles.SetActive(value: false);
	}

	[DebugButton(null, Tint.Default, PostClickAction.HideButton, 0, new object[] { })]
	private void debugGetWelder()
	{
		welder.targetable = true;
		changeWelderScreen(WelderScreenType.Laser);
		game.addItemToInventory(welder.gameObject);
	}

	private void enableLadder()
	{
		if (!ladderRaised)
		{
			ladderRaised = true;
			game.startTimer(new LadderTimer(0), 1f);
		}
	}

	[DebugButton(null, Tint.Default, PostClickAction.HideButton, 0, new object[] { })]
	private void openControlDoor()
	{
		controlRoomSlot.targetable = false;
		checkCargoSolution(full: true);
		game.startTimer(new LadderTimer(-2), 0.8f);
	}

	private void initHazmatCargo()
	{
		for (int i = 0; i < cargoSlots.Length; i++)
		{
			cargoButtons.Add(i);
		}
		cargoPallet.SetActive(value: false);
		cargoRoomDoorParticles.SetActive(value: false);
	}

	private void onCargoSlot(Slot slot, int slotIndex)
	{
		int num = Array.IndexOf(cargoPieces, slot.insertedItem);
		if (num < 0)
		{
			Debug.LogError("onCargoSlot: couldn't find item " + slot.insertedItem.name);
			return;
		}
		cargoButtons[slotIndex] = num;
		bool flag = isCargoFull();
		cargoChangeTitle(flag ? CargoTitleType.Locked : CargoTitleType.Missing);
		checkCargoSolution(flag);
	}

	private void onRemoveFromCargoSlot(int slotIndex)
	{
		cargoButtons[slotIndex] = -1;
		cargoChangeTitle(CargoTitleType.Missing);
	}

	public void checkCargoSolution(bool full)
	{
		bool[] array = new bool[3]
		{
			checkOxidizing(),
			checkFlammable(),
			checkRadioactive()
		};
		for (int i = 0; i < array.Length; i++)
		{
			cargoErrorsMaterialStates[i].transitionTo("Correct", 10f, array[i] ? 1f : 0f);
			cargoCorrectTexts[i].SetActive(array[i]);
			cargoErrorTexts[i].SetActive(!array[i]);
		}
	}

	private bool isCargoFull()
	{
		Slot[] array = cargoSlots;
		foreach (Slot slot in array)
		{
			if (slot.targetable && slot.insertedItem == null)
			{
				return false;
			}
		}
		return true;
	}

	private bool checkFlammable()
	{
		for (int i = 0; i < cargoButtons.Count; i++)
		{
			if (cargoButtons[i] < 0 || cargoStartingTypes[cargoButtons[i]] != CargoChemicalType.Flammable)
			{
				continue;
			}
			foreach (int adjacentIndex in getAdjacentIndices(i))
			{
				if (cargoButtons[adjacentIndex] >= 0 && cargoStartingTypes[cargoButtons[adjacentIndex]] == CargoChemicalType.Flammable)
				{
					return false;
				}
			}
		}
		return true;
	}

	private bool checkRadioactive()
	{
		bool[] visited = new bool[cargoButtons.Count];
		for (int i = 0; i < cargoButtons.Count; i++)
		{
			if (cargoButtons[i] >= 0 && cargoStartingTypes[cargoButtons[i]] == CargoChemicalType.Radioactive && !visited[i] && getRadioactiveChainLength(i, ref visited) >= 4)
			{
				return false;
			}
			visited[i] = true;
		}
		return true;
	}

	private int getRadioactiveChainLength(int index, ref bool[] visited)
	{
		visited[index] = true;
		int num = 1;
		foreach (int adjacentIndex in getAdjacentIndices(index, ignoreDiagonal: true))
		{
			if (cargoButtons[adjacentIndex] >= 0 && cargoStartingTypes[cargoButtons[adjacentIndex]] == CargoChemicalType.Radioactive && !visited[adjacentIndex])
			{
				num += getRadioactiveChainLength(adjacentIndex, ref visited);
			}
		}
		return num;
	}

	private bool checkOxidizing()
	{
		for (int i = 0; i < cargoButtons.Count; i++)
		{
			if (cargoButtons[i] >= 0 && cargoStartingTypes[cargoButtons[i]] == CargoChemicalType.Oxidizing && !IsOnEdge(i))
			{
				return false;
			}
		}
		return true;
		static bool IsOnEdge(int index)
		{
			int num = index / 4;
			int num2 = index % 4;
			if (num != 0 && num != 4 && num2 != 0)
			{
				return num2 == 3;
			}
			return true;
		}
	}

	private List<int> getAdjacentIndices(int index, bool ignoreDiagonal = false)
	{
		List<int> list = new List<int>();
		int num = index / 4;
		int num2 = index % 4;
		for (int i = -1; i <= 1; i++)
		{
			for (int j = -1; j <= 1; j++)
			{
				if ((i != 0 || j != 0) && (!ignoreDiagonal || Mathf.Abs(i) != Mathf.Abs(j)))
				{
					int num3 = num + i;
					int num4 = num2 + j;
					if (num3 >= 0 && num3 < 5 && num4 >= 0 && num4 < 4)
					{
						list.Add(num3 * 4 + num4);
					}
				}
			}
		}
		return list;
	}

	private void cargoChangeTitle(CargoTitleType title)
	{
		for (int i = 0; i < cargoTitles.Length; i++)
		{
			cargoTitles[i].SetActive(i == (int)title);
		}
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void solveCargo()
	{
		Debug.Log("Solved cargo");
		Slot[] array = cargoSlots;
		foreach (Slot slot in array)
		{
			slot.targetable = false;
			if (slot.insertedItem != null)
			{
				slot.insertedItem.targetable = false;
			}
		}
		cargoChangeTitle(CargoTitleType.Fixed);
		hazmatCargoDoor.transitionTo("Open");
		game.startTimer(new CargoTimer(0), 1.2f);
		cargoEffect.SetActive(value: true);
		game.finishPuzzle(Puzzle.Cargo);
	}

	private void initWelder()
	{
		laserLineRenderer.Get<LineRenderer>(0).positionCount = 2;
		laserLineRenderer.Get<GameObject>(0f).SetActive(value: false);
		changeWelderScreen(WelderScreenType.Laser);
		welderHitParticles.Get<VisualEffect>(0f).Stop();
		welderHitDefaultParticles.Get<VisualEffect>(0f).Stop();
		WelderStartParticles.SetActive(value: false);
		welderSoundInstance = PineFmod.createInstance("event:/Sound Effects/05 Misc/Electricity/Welding");
		welderCurrentGateSwaps = new int[3] { 0, 1, 2 };
		welderCurrentLineSwaps = new int[5] { 0, 1, 2, 3, 4 };
		welderLineCombinations[0] = new int[2] { -1, -1 };
		welderLineCombinations[1] = new int[2] { 1, 0 };
		welderLineCombinations[2] = new int[2] { 0, 1 };
		welderLineCombinations[3] = new int[2] { -1, 0 };
		welderLineCombinations[4] = new int[2] { 1, 1 };
		welderGateCombinations[0] = new int[3] { -1, -1, 0 };
		welderGateCombinations[1] = new int[3] { 1, 1, -1 };
		welderGateCombinations[2] = new int[3] { 0, 1, 0 };
		checkWelderSolution();
	}

	private void onWelderCutterBoxButton()
	{
		game.startSwitch(welderCutterBoxLid);
		welderCutterBoxLid.targetable = true;
	}

	private void onWelderCutterBoxClose()
	{
		welderCutterBoxLid.targetable = false;
		welderCutterBoxButton.targetable = true;
	}

	private void changeWelderScreen(WelderScreenType type)
	{
		for (int i = 0; i < welderScreens.Length; i++)
		{
			welderScreens[i].SetActive(i == (int)type);
		}
	}

	private void onWelderGatesSwapper(Swapper swapper, Swapper.SwapperEvent swapperEvent)
	{
		if (swapper != welderGatesSwapper)
		{
			return;
		}
		if (!swapperEvent.isReset)
		{
			game.cancelSwapperSelection(welderLineSwapper);
		}
		if (swapperEvent.isReset)
		{
			swapperEvent.first.tweenState.transitionTo("Down", 2f, 0f);
		}
		else if (swapperEvent.didSwap)
		{
			swapperEvent.second.tweenState.setState("Down");
			swapperEvent.second.tweenState.transitionTo("Down", 2f, 0f);
			swapperEvent.first.tweenState.transitionTo("Down", 2f, 0f);
			int num = Array.IndexOf(welderGatesSwapperPieces, swapperEvent.first);
			int num2 = Array.IndexOf(welderGatesSwapperPieces, swapperEvent.second);
			Vector3 localPosition = welderGates[num].localPosition;
			welderGates[num].localPosition = welderGates[num2].localPosition;
			welderGates[num2].localPosition = localPosition;
			SwapperPiece value = ((swapperEvent.first.swappedPiece == null) ? swapperEvent.first : swapperEvent.first.swappedPiece);
			SwapperPiece value2 = ((swapperEvent.second.swappedPiece == null) ? swapperEvent.second : swapperEvent.second.swappedPiece);
			int num3 = Array.IndexOf(welderGatesSwapperPieces, value);
			int num4 = Array.IndexOf(welderGatesSwapperPieces, value2);
			int num5 = welderCurrentGateSwaps[num3];
			welderCurrentGateSwaps[num3] = welderCurrentGateSwaps[num4];
			welderCurrentGateSwaps[num4] = num5;
			Debug.Log($"welder GATE: {welderCurrentGateSwaps[0]}, {welderCurrentGateSwaps[1]}, {welderCurrentGateSwaps[2]}");
			if (checkWelderSolution())
			{
				solveWelderGraph();
			}
		}
		else
		{
			swapperEvent.first.tweenState.transitionTo("Down", 3f);
		}
	}

	private void onWelderLineSwapper(Swapper swapper, Swapper.SwapperEvent swapperEvent)
	{
		if (!(swapper != welderLineSwapper))
		{
			if (!swapperEvent.isReset)
			{
				game.cancelSwapperSelection(welderGatesSwapper);
			}
			if (swapperEvent.isReset)
			{
				swapperEvent.first.tweenState.transitionTo("Down", 2f, 0f);
			}
			else if (swapperEvent.didSwap)
			{
				swapperEvent.second.tweenState.setState("Down");
				swapperEvent.second.tweenState.transitionTo("Down", 2f, 0f);
				swapperEvent.first.tweenState.transitionTo("Down", 2f, 0f);
				SwapperPiece value = ((swapperEvent.first.swappedPiece == null) ? swapperEvent.first : swapperEvent.first.swappedPiece);
				SwapperPiece value2 = ((swapperEvent.second.swappedPiece == null) ? swapperEvent.second : swapperEvent.second.swappedPiece);
				int num = Array.IndexOf(welderLineSwapperPieces, value);
				int num2 = Array.IndexOf(welderLineSwapperPieces, value2);
				int num3 = welderCurrentLineSwaps[num];
				welderCurrentLineSwaps[num] = welderCurrentLineSwaps[num2];
				welderCurrentLineSwaps[num2] = num3;
				Debug.Log($"welder LINE: {welderCurrentLineSwaps[0]}, {welderCurrentLineSwaps[1]}, {welderCurrentLineSwaps[2]}, {welderCurrentLineSwaps[3]}, {welderCurrentLineSwaps[4]}");
				int num4 = Array.IndexOf(welderLineSwapperPieces, swapperEvent.first);
				int num5 = Array.IndexOf(welderLineSwapperPieces, swapperEvent.second);
				welderWavyLines[num4].Get<MaterialState>(0f).transitionToDuration("Transparent", 0.25f);
				welderWavyLines[num5].Get<MaterialState>(0f).transitionToDuration("Transparent", 0.25f);
				game.startTimer(new WelderTimer(0, num4, num5), 0.25f);
			}
			else
			{
				swapperEvent.first.tweenState.transitionTo("Down", 3f);
			}
		}
	}

	private bool checkWelderSolution()
	{
		int[] array = new int[welderLineSwapperPieces.Length * 2];
		int num = 0;
		for (int i = 0; i < welderGatesSwapperPieces.Length; i++)
		{
			int key = welderCurrentGateSwaps[i];
			for (int j = 0; j < welderGateCombinations[key].Length; j++)
			{
				array[num] = welderGateCombinations[key][j];
				num++;
			}
		}
		array[num] = 1;
		bool result = true;
		num = 0;
		for (int k = 0; k < welderLineSwapperPieces.Length; k++)
		{
			int num2 = welderCurrentLineSwaps[k];
			welderWavyLines[num2].Get<MaterialState>(0f).transitionTo("Red", 3f, 0f);
			for (int l = 0; l < welderLineCombinations[num2].Length; l++)
			{
				int num3 = welderLineCombinations[num2][l];
				if (array[num] != num3)
				{
					result = false;
					welderWavyLines[num2].Get<MaterialState>(0f).transitionTo("Red", 3f);
				}
				num++;
			}
		}
		return result;
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void solveWelderGraph()
	{
		if (!solvedWelderGraph)
		{
			solvedWelderGraph = true;
			SwapperPiece[] array = welderGatesSwapperPieces;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].targetable = false;
			}
			array = welderLineSwapperPieces;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].targetable = false;
			}
			game.startTimer(new WelderTimer(1, -1, -1), 0.3f);
		}
	}

	private void initToolLockers()
	{
		lockerKey.targetable = false;
		toolLockerEnterNumber(lockerNumbers0, -1);
		toolLockerEnterNumber(lockerNumbers1, -1);
		toolLockerEnterNumber(lockerNumbers2, -1);
		game.startTimer(new ToolLockerTimer(-1), 0.4f);
		changeLockerKeypadScreen(LockerScreen.Input);
	}

	private bool onSwitchFlipped(Switch3D button)
	{
		return button.state == Switch3DState.On;
	}

	private void onToolLockerNumber(int index)
	{
		if (onSwitchFlipped(toolLockerNumberButtons[index]))
		{
			return;
		}
		if (lockerOpened)
		{
			lockerOpened = false;
			changeLockerKeypadScreen(LockerScreen.Input);
		}
		if (toolLockerCurrentValues.Count < 3)
		{
			toolLockerCurrentValues.Add(index);
			int count = toolLockerCurrentValues.Count;
			if (count > 0)
			{
				toolLockerEnterNumber(lockerNumbers0, toolLockerCurrentValues[0]);
			}
			if (count > 1)
			{
				toolLockerEnterNumber(lockerNumbers1, toolLockerCurrentValues[1]);
			}
			if (count > 2)
			{
				toolLockerEnterNumber(lockerNumbers2, toolLockerCurrentValues[2]);
			}
		}
	}

	private void onToolLockerX()
	{
		if (onSwitchFlipped(toolLockerXButton))
		{
			return;
		}
		if (lockerOpened)
		{
			lockerOpened = false;
			changeLockerKeypadScreen(LockerScreen.Input);
		}
		else if (toolLockerCurrentValues.Count != 0)
		{
			toolLockerCurrentValues.RemoveAt(toolLockerCurrentValues.Count - 1);
			int count = toolLockerCurrentValues.Count;
			if (count == 0)
			{
				toolLockerEnterNumber(lockerNumbers0, -1);
			}
			if (count <= 1)
			{
				toolLockerEnterNumber(lockerNumbers1, -1);
			}
			if (count <= 2)
			{
				toolLockerEnterNumber(lockerNumbers2, -1);
			}
		}
	}

	private void toolLockerEnterNumber(GameObject[] numbers, int index)
	{
		for (int i = 0; i < numbers.Length; i++)
		{
			numbers[i].SetActive(i == index);
		}
	}

	private void onToolLockerOpen()
	{
		if (onSwitchFlipped(toolLockerOpenButton))
		{
			return;
		}
		if (lockerOpened)
		{
			lockerOpened = false;
			changeLockerKeypadScreen(LockerScreen.Input);
		}
		else
		{
			if (toolLockerCurrentValues.Count < 3)
			{
				return;
			}
			changeLockerKeypadScreen(LockerScreen.Checking);
			int index = -1;
			string value = string.Join("", toolLockerCurrentValues);
			for (int i = 0; i < toolLockerSolutions.Count; i++)
			{
				if (toolLockerSolutions[i].Equals(value))
				{
					index = i;
				}
			}
			Switch3D[] array = toolLockerNumberButtons;
			for (int j = 0; j < array.Length; j++)
			{
				array[j].targetable = false;
			}
			game.startTimer(new LockerScreenTimer(index), 2f);
			toolLockerCurrentValues.Clear();
			toolLockerEnterNumber(lockerNumbers0, -1);
			toolLockerEnterNumber(lockerNumbers1, -1);
			toolLockerEnterNumber(lockerNumbers2, -1);
		}
	}

	private void openLocker(int index)
	{
		game.increaseZoomCounter(toolLockerZoomable);
		float targetWeight = toolLockerDoors[index].findStateByName("Down").targetWeight;
		toolLockerDoors[index].transitionTo("Down", 1f, (targetWeight < 0.1f) ? 1f : 0f);
		if (!solvedLocker && index == 12)
		{
			solvedLocker = true;
			game.finishPuzzle(Puzzle.Locker);
			lockerKey.targetable = true;
		}
	}

	private void changeLockerKeypadScreen(LockerScreen screen)
	{
		for (int i = 0; i < lockerScreens.Length; i++)
		{
			lockerScreens[i].SetActive(i == (int)screen);
		}
	}

	private void initElevator()
	{
		elevatorStartingRotation = elevator.Get<Transform>(0f).rotation;
		elevatorMoveHandleStartingPosition = elevatorMoveHandle.transform.localPosition;
		elevatorRaiseTween.transitionTo("Raise", 22f);
		disableElevatorControls();
		elevatorNavMeshQuery = new NavMeshQueryFilter
		{
			agentTypeID = NavMesh.GetSettingsByIndex(3).agentTypeID,
			areaMask = -1
		};
		changeElevatorKeycardScreen(ElevatorKeycardScreen.InsertKeycard);
		changeElevatorScreen(ElevatorScreenType.Operating);
		elevatorSoundInstances = new EventInstance[2];
		elevatorSoundInstances[0] = PineFmod.createInstance("event:/Sound Effects/03 Interactable/Scissor_Lift/Scissor_Lift_Drive_Loop");
		elevatorSoundInstances[1] = PineFmod.createInstance("event:/Sound Effects/03 Interactable/Scissor_Lift/Scissor_Lift_Up_Loop");
	}

	private void updateElevator()
	{
		elevatorUpdateRotation();
		if (isMovingElevator)
		{
			elevatorMove();
		}
		if (isRaisingElevator || isLoweringElevator)
		{
			elevatorRise();
		}
		if (!solvedConnectors && !solvedTrail)
		{
			if (elevatorRaiseTween.findStateByName("Raise").weight > 0.4f)
			{
				if (sphereZooms[0].targetable)
				{
					Zoomable[] array = sphereZooms;
					for (int i = 0; i < array.Length; i++)
					{
						array[i].targetable = false;
					}
				}
			}
			else
			{
				bool targetable = game.getCameraPosition().y > 2.5f;
				Zoomable[] array = sphereZooms;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].targetable = targetable;
				}
			}
		}
		float weight = elevatorRaiseTween.findStateByName("Raise").weight;
		ElevatorTextType text = ElevatorTextType.PlatformLowered;
		int speed = 0;
		if (isMovingElevator)
		{
			text = (((double)elevatorMoveHandle.value < 0.5) ? ElevatorTextType.MovingBackward : ElevatorTextType.MovingForward);
			speed = Mathf.CeilToInt(Mathf.Abs(elevatorMoveHandle.value - 0.5f) * 10f);
		}
		else if (elevatorHitOnUp)
		{
			changeElevatorScreen(ElevatorScreenType.Warning);
		}
		else if (elevatorCurrentAngle > 0.01f || elevatorCurrentAngle < -0.01f)
		{
			if (elevatorCheckCollision(isRotating: true, 0f))
			{
				changeElevatorScreen(ElevatorScreenType.Warning);
			}
			else
			{
				changeElevatorScreen(ElevatorScreenType.Operating);
			}
			text = ElevatorTextType.Turning;
		}
		else if (weight < 0.5f)
		{
			text = ElevatorTextType.PlatformRaised;
		}
		changeElevatorScreen(currentElevatorScreenType, text, speed);
	}

	private void onElevatorButtons(Switch3D targetSwitch, Switch3DEvent switchEvent)
	{
		if (switchEvent == Switch3DEvent.On)
		{
			if (targetSwitch == elevatorRaiseButtons[0])
			{
				isRaisingElevator = true;
				isLoweringElevator = false;
				elevatorRaiseTween.transitionTo("Raise", 1f, 0f);
			}
			else if (targetSwitch == elevatorRaiseButtons[1])
			{
				isRaisingElevator = false;
				isLoweringElevator = true;
				elevatorRaiseTween.transitionTo("Raise");
			}
		}
	}

	private void enableElevatorControls()
	{
		elevatorMoveHandle.targetable = true;
		elevatorRotateDial.targetable = true;
		elevatorRaiseButtons[0].targetable = true;
		elevatorRaiseButtons[1].targetable = true;
	}

	private void disableElevatorControls()
	{
		elevatorMoveHandle.targetable = false;
		elevatorRotateDial.targetable = false;
		elevatorRaiseButtons[0].targetable = false;
		elevatorRaiseButtons[1].targetable = false;
	}

	private void elevatorMove()
	{
		if (!game.session.isHost())
		{
			return;
		}
		float num = -6f;
		Vector3 position = elevator.Get<Transform>(0f).position;
		float num2 = elevatorMoveHandle.value - 0.5f;
		if (Mathf.Abs(num2) < 0.1f)
		{
			return;
		}
		if (elevatorCheckCollision(isRotating: false, num2))
		{
			changeElevatorScreen(ElevatorScreenType.Warning);
			return;
		}
		elevatorSoundInstances[0].getPlaybackState(out var state);
		if (state == PLAYBACK_STATE.STOPPED)
		{
			elevatorSoundInstances[0].start();
		}
		PineFmod.set3DAttributes(elevatorSoundInstances[0], PineFmod.to3DAttributes(elevatorSounds[0]));
		game.cancelTimers((ElevatorSoundTimer x) => x.soundIndex == 0);
		game.startTimer(new ElevatorSoundTimer(0), 0.2f);
		changeElevatorScreen(ElevatorScreenType.Operating);
		Vector3 b = position + elevator.Get<Transform>(0f).up * num2 * num * Time.deltaTime;
		Vector3 vector = Vector3.Slerp(position, b, Time.deltaTime * 50f);
		NavMeshPath navMeshPath = new NavMeshPath();
		if (NavMesh.CalculatePath(position, vector, elevatorNavMeshQuery, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete)
		{
			elevatorRb.MovePosition(vector);
		}
		game.session.send(new ElevatorMovePacket
		{
			isPos = true,
			position = vector,
			rotation = Quaternion.identity
		});
	}

	private bool elevatorCheckCollision(bool isRotating, float moveDirection)
	{
		if (isRotating)
		{
			if (boxHasCollision(elevatorFrontBox))
			{
				return true;
			}
			if (boxHasCollision(elevatorBackBox))
			{
				return true;
			}
		}
		else if (moveDirection > 0f)
		{
			if (boxHasCollision(elevatorFrontBox))
			{
				changeElevatorScreen(ElevatorScreenType.Warning);
				return true;
			}
		}
		else if (boxHasCollision(elevatorBackBox))
		{
			changeElevatorScreen(ElevatorScreenType.Warning);
			return true;
		}
		return false;
		bool boxHasCollision(GameObject[] points)
		{
			GameObject gameObject = points[0];
			GameObject gameObject2 = points[1];
			GameObject gameObject3 = points[2];
			GameObject gameObject4 = points[3];
			Vector3 vector = (gameObject2.transform.position + gameObject.transform.position) / 2f;
			Vector3 halfExtents = new Vector3(Vector3.Distance(gameObject2.transform.position, gameObject3.transform.position) / 2f, Vector3.Distance(gameObject4.transform.position, gameObject.transform.position) / 2f, Vector3.Distance(gameObject3.transform.position, gameObject4.transform.position) / 2f);
			Vector3 normalized = (gameObject4.transform.position - gameObject3.transform.position).normalized;
			Vector3 normalized2 = (gameObject.transform.position - gameObject4.transform.position).normalized;
			Quaternion orientation = Quaternion.LookRotation(normalized, normalized2);
			Debug.DrawLine(vector, vector + normalized * halfExtents.z, Color.red);
			Debug.DrawLine(vector, vector + normalized2 * halfExtents.y, Color.green);
			Debug.DrawLine(vector, vector + Vector3.Cross(normalized2, normalized) * halfExtents.x, Color.blue);
			Collider[] array = Physics.OverlapBox(vector, halfExtents, orientation);
			foreach (Collider collider in array)
			{
				if (collider != null && !collider.isTrigger && !collider.transform.IsChildOf(elevator.Get<Transform>(0f)) && !collider.transform.IsChildOf(game.transform) && collider.transform.GetComponentInParent<Interactive>() == null && collider.transform.GetComponentInParent<Token>() == null && collider.gameObject.layer != LayerMask.NameToLayer("Characters") && Array.IndexOf(elevatorIgnoreColliders, collider) < 0)
				{
					Debug.Log("HIT " + collider.name);
					return true;
				}
			}
			return false;
		}
	}

	private void elevatorRise()
	{
		float weight = elevatorRaiseTween.findStateByName("Raise").weight;
		if (weight == 1f && isLoweringElevator)
		{
			isLoweringElevator = false;
			elevatorPose.targetable = true;
			elevatorHitOnUp = false;
		}
		else if (weight == 0f && isRaisingElevator)
		{
			isRaisingElevator = false;
			elevatorHitOnUp = false;
		}
		Vector3 origin = elevatorPose.transform.position + 1.5f * Vector3.up;
		if (isRaisingElevator && Physics.SphereCast(origin, 0.2f, Vector3.up, out var hitInfo, 0.5f))
		{
			isLoweringElevator = true;
			isRaisingElevator = false;
			elevatorHitOnUp = true;
			elevatorRaiseTween.transitionTo("Raise", 2f);
			if (hitInfo.distance < 0.05f)
			{
				return;
			}
		}
		elevatorSoundInstances[1].getPlaybackState(out var state);
		if (state == PLAYBACK_STATE.STOPPED)
		{
			elevatorSoundInstances[1].start();
		}
		PineFmod.set3DAttributes(elevatorSoundInstances[1], PineFmod.to3DAttributes(elevatorSounds[1]));
		game.cancelTimers((ElevatorSoundTimer x) => x.soundIndex == 1);
		game.startTimer(new ElevatorSoundTimer(1), 0.2f);
	}

	private void elevatorRotate(float targetAngle)
	{
		elevatorCurrentAngle += targetAngle;
	}

	private void elevatorUpdateRotation()
	{
		if (game.session.isHost())
		{
			float num = 5f;
			float num2 = 0.0001f;
			float num3 = elevatorCurrentAngle - elevatorUpdatingAngle;
			if (Mathf.Abs(num3) < num2)
			{
				elevatorUpdatingAngle = elevatorCurrentAngle;
				return;
			}
			float f = num3 * (1f - Mathf.Exp((0f - num) * Time.deltaTime));
			elevatorUpdatingAngle += Mathf.Sign(f) * Mathf.Min(100f * Time.deltaTime, Mathf.Abs(f));
			Quaternion quaternion = elevatorStartingRotation * Quaternion.AngleAxis(elevatorUpdatingAngle, Vector3.forward);
			elevatorRb.MoveRotation(quaternion);
			game.session.send(new ElevatorMovePacket
			{
				isPos = false,
				position = Vector3.zero,
				rotation = quaternion
			});
		}
	}

	[DebugButton(null, Tint.Default, PostClickAction.HideButton, 0, new object[] { })]
	private void debugOpenElevatorDoor()
	{
		cargoElevatorDoor.transitionTo("Open");
	}

	private void onElevatorKeycardSlot()
	{
		if (cargoElevatorSlot.insertedItem != cargoElevatorSlot.acceptItems[0])
		{
			changeElevatorKeycardScreen(ElevatorKeycardScreen.Checking);
			game.startTimer(new ElevatorKeycardTimer(-2), 0.5f);
		}
		else
		{
			changeElevatorKeycardScreen(ElevatorKeycardScreen.Checking);
			game.startTimer(new ElevatorKeycardTimer(0), 2f);
		}
	}

	private void changeElevatorKeycardScreen(ElevatorKeycardScreen screen)
	{
		for (int i = 0; i < elevatorKeypadScreens.Length; i++)
		{
			elevatorKeypadScreens[i].SetActive(i == (int)screen);
		}
	}

	private void changeElevatorScreen(ElevatorScreenType screen, ElevatorTextType text = ElevatorTextType.None, int speed = -1)
	{
		currentElevatorScreenType = screen;
		for (int i = 0; i < elevatorScreens.Length; i++)
		{
			elevatorScreens[i].SetActive(i == (int)screen);
		}
		for (int j = 0; j < elevatorSpeedBars.Length; j++)
		{
			elevatorSpeedBars[j].SetActive(j < speed);
		}
		if (text != ElevatorTextType.None)
		{
			for (int k = 0; k < elevatorTexts.Length; k++)
			{
				elevatorTexts[k].SetActive(k == (int)text);
			}
		}
	}

	private void initFabricator()
	{
		fabricatorPlane.SetActive(value: false);
		fabricatorPlaneProjections.SetActive(value: false);
		fabricatorSlotStartingRotation = fabricatorSlot.transform.localRotation;
		fabricatorSlotStartingPosition = fabricatorSlot.transform.localPosition;
		fabricatorRepairSoundInstance = PineFmod.createInstance("event:/Sound Effects/05 Misc/Electricity/Welding");
		fabricatorChangeScreen(FabricatorScreens.ScanButton);
	}

	private void onFabricatorFixObjectButton()
	{
		if (game.isHost())
		{
			bool isCorrectSolution = checkFabricatorSolution();
			handleFabricatorFixObjectButton(isCorrectSolution, shouldSync: true);
		}
	}

	private void handleFabricatorFixObjectButton(bool isCorrectSolution, bool shouldSync)
	{
		if (shouldSync)
		{
			game.session.send(new FabricatorButtonFixPacket
			{
				isCorrectSolution = isCorrectSolution
			}, allowSendInMessageResponse: true);
		}
		fabricatorChangeScreen(FabricatorScreens.Repairing);
		fabricatorLoadingTween.setState("Play", 0f);
		fabricatorLoadingTween.transitionTo("Play", 0.125f);
		fabricatorRobotTween.setState("Play", 0f);
		fabricatorRobotTween.transitionTo("Play", 0.125f);
		brokenGem.Get<Item>(0).targetable = false;
		fabricatorSlot.targetable = false;
		fabricatorRobotSparks.SetActive(value: true);
		game.startTimer(new FabricatorFixObjectTimer(-2), 4f);
		game.startTimer(new FabricatorFixObjectTimer((!isCorrectSolution) ? 1 : (-1)), 5f);
	}

	private void onFabricatorStartScanButton()
	{
		fabricatorChangeScreen(FabricatorScreens.Scanning);
		fabricatorLoadingTween.setState("Play", 0f);
		fabricatorLoadingTween.transitionTo("Play", 0.5f);
		if (fabricatorSlot.insertedItem == null)
		{
			game.startTimer(new FabricatorStartScanTimer(0), 2f);
			return;
		}
		fabricatorSlot.targetable = false;
		fabricatorSlot.insertedItem.targetable = false;
		if (!(fabricatorSlot.insertedItem == fabricatorSlot.acceptItems[0]) || fixedGem)
		{
			game.startTimer(new FabricatorStartScanTimer(1), 2f);
		}
		else
		{
			game.startTimer(new FabricatorStartScanTimer(2), 2f);
		}
	}

	private void fabricatorChangeScreen(FabricatorScreens screen)
	{
		if (screen == FabricatorScreens.NoObjectInScanner || screen == FabricatorScreens.DoesntNeedRepair)
		{
			game.startTimer(new FabricatorScannedTimer(), 1.5f);
		}
		GameObject[] array = fabricatorTexts;
		foreach (GameObject gameObject in array)
		{
			if (gameObject != null)
			{
				gameObject.SetActive(value: false);
			}
		}
		int[] array2 = screen switch
		{
			FabricatorScreens.ScanButton => new int[4] { 0, 1, 2, 3 }, 
			FabricatorScreens.Scanning => new int[3] { 0, 4, 6 }, 
			FabricatorScreens.Repairing => new int[3] { 0, 4, 5 }, 
			FabricatorScreens.NoObjectInScanner => new int[2] { 7, 8 }, 
			FabricatorScreens.DoesntNeedRepair => new int[2] { 7, 9 }, 
			FabricatorScreens.RepairDone => new int[2] { 7, 10 }, 
			FabricatorScreens.RepairWrong => new int[2] { 7, 11 }, 
			FabricatorScreens.Puzzle => new int[4] { 0, 12, 13, 14 }, 
			_ => new int[0], 
		};
		foreach (int num in array2)
		{
			if (num >= 0 && num < fabricatorTexts.Length && fabricatorTexts[num] != null)
			{
				fabricatorTexts[num].SetActive(value: true);
			}
		}
	}

	private void onFabricatorSlidable(int index)
	{
		fabricatorCameras[index].Render();
	}

	private void onFabricatorUpdate()
	{
		if (fabricatorSlot.insertedItem != null)
		{
			float num = Mathf.Sin(Time.time * 0.7f) * 0.02f;
			fabricatorSlot.transform.localPosition = new Vector3(fabricatorSlotStartingPosition.x, fabricatorSlotStartingPosition.y + num, fabricatorSlotStartingPosition.z);
			fabricatorSlot.transform.Rotate(Vector3.forward * Time.deltaTime * 7f);
		}
		else
		{
			fabricatorSlot.transform.localPosition = fabricatorSlotStartingPosition;
			fabricatorSlot.transform.localRotation = fabricatorSlotStartingRotation;
		}
	}

	private bool checkFabricatorSolution()
	{
		bool result = true;
		Debug.Log("Fabricator current: " + fabricatorSlidables[0].value.ToString("R", CultureInfo.InvariantCulture) + "f, " + fabricatorSlidables[1].value.ToString("R", CultureInfo.InvariantCulture) + "f, " + fabricatorSlidables[2].value.ToString("R", CultureInfo.InvariantCulture) + "f");
		for (int i = 0; i < fabricatorSlidables.Length; i++)
		{
			if (Mathf.Abs(fabricatorSlidables[i].value - fabricatorSolution[i]) > fabricatorWiggleRoom)
			{
				result = false;
				break;
			}
		}
		return result;
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void solveFabricator()
	{
		if (!fixedGem)
		{
			fixedGem = true;
			fixedArtifact.Get<GameObject>(0u).SetActive(value: true);
			brokenGem.Get<MeshRenderer>(0f).enabled = false;
			game.startTimer(new FabricatorFixObjectTimer(0), 3f);
			game.finishPuzzle(Puzzle.ArtifactRepair);
		}
	}

	private void onRemoveFromFabricatorSlot(Item item)
	{
		fabricatorChangeScreen(FabricatorScreens.ScanButton);
	}

	private void initConnectors()
	{
		connectorPuzzles = new List<ConnectorPuzzle>();
		ConnectorPuzzle connectorPuzzle = new ConnectorPuzzle();
		connectorPuzzle.root = connectors[0];
		connectorPuzzle.centerSwappers = connectors1CenterSwappers;
		connectorPuzzle.batterySwappers = connectors1BatterySwappers;
		connectorPuzzle.allSwappers = connectors1AllSwappers;
		connectorPuzzle.batteryTypes = new List<ConnectorPuzzle.Type>
		{
			ConnectorPuzzle.Type.Whole,
			ConnectorPuzzle.Type.Whole,
			ConnectorPuzzle.Type.Whole
		};
		connectorPuzzle.selectors = connectors1SelectionLights;
		connectorPuzzle.connectorOnBatterySlots = new int[3] { 6, 7, 8 };
		ConnectorPuzzle connectorPuzzle2 = connectorPuzzle;
		connectorPuzzle2.lights = new List<ConnectorPuzzle.Light>();
		connectorPuzzle2.lights.Add(new ConnectorPuzzle.Light
		{
			light = connectors1Lights[0],
			topStartsOn = true,
			bottomStartsOn = true,
			connectedSwappers = new List<SwapperPiece>
			{
				connectors1CenterSwappers[0],
				connectors1CenterSwappers[4],
				connectors1CenterSwappers[5]
			}
		});
		connectorPuzzle2.lights.Add(new ConnectorPuzzle.Light
		{
			light = connectors1Lights[1],
			connectedSwappers = new List<SwapperPiece>
			{
				connectors1CenterSwappers[3],
				connectors1CenterSwappers[4],
				connectors1CenterSwappers[5]
			}
		});
		connectorPuzzle2.lights.Add(new ConnectorPuzzle.Light
		{
			light = connectors1Lights[2],
			connectedSwappers = new List<SwapperPiece>
			{
				connectors1CenterSwappers[2],
				connectors1CenterSwappers[3],
				connectors1CenterSwappers[5]
			}
		});
		connectorPuzzle2.lights.Add(new ConnectorPuzzle.Light
		{
			light = connectors1Lights[3],
			connectedSwappers = new List<SwapperPiece>
			{
				connectors1CenterSwappers[0],
				connectors1CenterSwappers[1],
				connectors1CenterSwappers[5]
			}
		});
		connectorPuzzle2.lights.Add(new ConnectorPuzzle.Light
		{
			topStartsOn = true,
			bottomStartsOn = true,
			light = connectors1Lights[4],
			connectedSwappers = new List<SwapperPiece>
			{
				connectors1CenterSwappers[1],
				connectors1CenterSwappers[2],
				connectors1CenterSwappers[5]
			}
		});
		connectorPuzzles.Add(connectorPuzzle2);
		connectorPuzzle = new ConnectorPuzzle();
		connectorPuzzle.root = connectors[1];
		connectorPuzzle.centerSwappers = connectors2CenterSwappers;
		connectorPuzzle.batterySwappers = connectors2BatterySwappers;
		connectorPuzzle.allSwappers = connectors2AllSwappers;
		connectorPuzzle.batteryTypes = new List<ConnectorPuzzle.Type>
		{
			ConnectorPuzzle.Type.Top,
			ConnectorPuzzle.Type.Top,
			ConnectorPuzzle.Type.Bottom
		};
		connectorPuzzle.connectorOnBatterySlots = new int[3] { 5, 6, 7 };
		connectorPuzzle.selectors = connectors2SelectionLights;
		connectorPuzzle2 = connectorPuzzle;
		connectorPuzzle2.lights = new List<ConnectorPuzzle.Light>();
		connectorPuzzle2.lights.Add(new ConnectorPuzzle.Light
		{
			topStartsOn = false,
			bottomStartsOn = true,
			light = connectors2Lights[4],
			lightBottom = connectors2Lights[0],
			connectedSwappers = new List<SwapperPiece>
			{
				connectors2CenterSwappers[1],
				connectors2CenterSwappers[2]
			}
		});
		connectorPuzzle2.lights.Add(new ConnectorPuzzle.Light
		{
			topStartsOn = true,
			bottomStartsOn = false,
			light = connectors2Lights[2],
			lightBottom = connectors2Lights[7],
			connectedSwappers = new List<SwapperPiece>
			{
				connectors2CenterSwappers[0],
				connectors2CenterSwappers[2]
			}
		});
		connectorPuzzle2.lights.Add(new ConnectorPuzzle.Light
		{
			light = connectors2Lights[3],
			lightBottom = connectors2Lights[6],
			connectedSwappers = new List<SwapperPiece>
			{
				connectors2CenterSwappers[0],
				connectors2CenterSwappers[1],
				connectors2CenterSwappers[4]
			}
		});
		connectorPuzzle2.lights.Add(new ConnectorPuzzle.Light
		{
			topStartsOn = true,
			bottomStartsOn = true,
			light = connectors2Lights[1],
			lightBottom = connectors2Lights[5],
			connectedSwappers = new List<SwapperPiece>
			{
				connectors2CenterSwappers[1],
				connectors2CenterSwappers[3]
			}
		});
		connectorPuzzles.Add(connectorPuzzle2);
		connectorPuzzle = new ConnectorPuzzle();
		connectorPuzzle.root = connectors[2];
		connectorPuzzle.centerSwappers = connectors3CenterSwappers;
		connectorPuzzle.batterySwappers = connectors3BatterySwappers;
		connectorPuzzle.allSwappers = connectors3AllSwappers;
		connectorPuzzle.batteryTypes = new List<ConnectorPuzzle.Type>
		{
			ConnectorPuzzle.Type.Whole,
			ConnectorPuzzle.Type.Top,
			ConnectorPuzzle.Type.Bottom,
			ConnectorPuzzle.Type.Bottom
		};
		connectorPuzzle.connectorOnBatterySlots = new int[4] { 5, 6, 7, 8 };
		connectorPuzzle.selectors = connectors3SelectionLights;
		connectorPuzzle2 = connectorPuzzle;
		connectorPuzzle2.lights = new List<ConnectorPuzzle.Light>();
		connectorPuzzle2.lights.Add(new ConnectorPuzzle.Light
		{
			light = connectors3Lights[1],
			lightBottom = connectors3Lights[0],
			connectedSwappers = new List<SwapperPiece>
			{
				connectors3CenterSwappers[4],
				connectors3CenterSwappers[3],
				connectors3CenterSwappers[1],
				connectors3CenterSwappers[0]
			}
		});
		connectorPuzzle2.lights.Add(new ConnectorPuzzle.Light
		{
			light = connectors3Lights[5],
			lightBottom = connectors3Lights[4],
			connectedSwappers = new List<SwapperPiece>
			{
				connectors3CenterSwappers[3],
				connectors3CenterSwappers[1],
				connectors3CenterSwappers[2]
			}
		});
		connectorPuzzle2.lights.Add(new ConnectorPuzzle.Light
		{
			topStartsOn = true,
			bottomStartsOn = true,
			light = connectors3Lights[3],
			lightBottom = connectors3Lights[2],
			connectedSwappers = new List<SwapperPiece>
			{
				connectors3CenterSwappers[2],
				connectors3CenterSwappers[1],
				connectors3CenterSwappers[0]
			}
		});
		connectorPuzzle2.lights.Add(new ConnectorPuzzle.Light
		{
			topStartsOn = false,
			bottomStartsOn = true,
			light = connectors3Lights[7],
			lightBottom = connectors3Lights[6],
			connectedSwappers = new List<SwapperPiece> { connectors3CenterSwappers[2] }
		});
		connectorPuzzles.Add(connectorPuzzle2);
		for (int i = 0; i < 3; i++)
		{
			SwapperPiece[] centerSwappers = connectorPuzzles[i].centerSwappers;
			for (int j = 0; j < centerSwappers.Length; j++)
			{
				centerSwappers[j].targetable = false;
			}
			centerSwappers = connectorPuzzles[i].batterySwappers;
			for (int j = 0; j < centerSwappers.Length; j++)
			{
				centerSwappers[j].targetable = true;
			}
		}
		connectorsArtifact.targetable = false;
		game.startTimer(new DelayedConnectorsInitTimer(), 2f);
	}

	private void onConnectorSwapper(Swapper swapper, Swapper.SwapperEvent swapperEvent)
	{
		int num = Array.IndexOf(connectorSwappers, swapper);
		if (num < 0)
		{
			return;
		}
		ConnectorPuzzle connectorPuzzle = connectorPuzzles[num];
		if (swapperEvent.didSwap)
		{
			int num2 = Array.IndexOf(connectorPuzzle.batterySwappers, swapperEvent.first);
			int num3 = Array.IndexOf(connectorPuzzle.batterySwappers, swapperEvent.second);
			if (num2 != -1 && num3 != -1)
			{
				int num4 = connectorPuzzle.connectorOnBatterySlots[num2];
				connectorPuzzle.connectorOnBatterySlots[num2] = connectorPuzzle.connectorOnBatterySlots[num3];
				connectorPuzzle.connectorOnBatterySlots[num3] = num4;
			}
			else if (num2 != -1)
			{
				int num5 = Array.IndexOf(connectorPuzzle.allSwappers, connectorPuzzle.getOriginalSwapperAtPosition(swapperEvent.first.transform.localPosition));
				connectorPuzzle.connectorOnBatterySlots[num2] = num5;
			}
			else if (num3 != -1)
			{
				int num6 = Array.IndexOf(connectorPuzzle.allSwappers, connectorPuzzle.getOriginalSwapperAtPosition(swapperEvent.second.transform.localPosition));
				connectorPuzzle.connectorOnBatterySlots[num3] = num6;
			}
			checkConnectorsSolution();
			foreach (Ref<GameObject, MaterialState> selector in connectorPuzzle.selectors)
			{
				selector.Get<GameObject>(0).SetActive(value: false);
				selector.Get<MaterialState>(0f).transitionTo("NewState", 3f, 0f);
			}
			for (int i = 0; i < connectorPuzzle.selectors.Length; i++)
			{
				int num7 = Array.IndexOf(connectorPuzzle.connectorOnBatterySlots, i);
				connectorPuzzle.selectors[i].Get<GameObject>(0).SetActive(num7 != -1);
			}
			SwapperPiece[] centerSwappers = connectorPuzzle.centerSwappers;
			for (int j = 0; j < centerSwappers.Length; j++)
			{
				centerSwappers[j].targetable = false;
			}
			centerSwappers = connectorPuzzle.batterySwappers;
			for (int j = 0; j < centerSwappers.Length; j++)
			{
				centerSwappers[j].targetable = true;
			}
		}
		else
		{
			for (int k = 0; k < connectorPuzzle.selectors.Length; k++)
			{
				int num8 = Array.IndexOf(connectorPuzzle.connectorOnBatterySlots, k);
				connectorPuzzle.selectors[k].Get<GameObject>(0).SetActive(num8 == -1);
			}
			int i2 = Array.IndexOf(connectorPuzzle.allSwappers, connectorPuzzle.getOriginalSwapperAtPosition(swapperEvent.first.transform.localPosition));
			connectorPuzzle.selectors[i2].Get<GameObject>(0).SetActive(value: true);
			connectorPuzzle.selectors[i2].Get<MaterialState>(0f).transitionTo("NewState", 3f);
			SwapperPiece[] centerSwappers = connectorPuzzle.centerSwappers;
			for (int j = 0; j < centerSwappers.Length; j++)
			{
				centerSwappers[j].targetable = true;
			}
			centerSwappers = connectorPuzzle.batterySwappers;
			for (int j = 0; j < centerSwappers.Length; j++)
			{
				centerSwappers[j].targetable = false;
			}
			swapperEvent.first.targetable = true;
		}
	}

	private void checkConnectorsSolution()
	{
		if (!connectorPuzzles[currentConnectorPuzzle].checkSolution())
		{
			return;
		}
		foreach (ConnectorPuzzle.Light light in connectorPuzzles[currentConnectorPuzzle].lights)
		{
			light.light.transitionToDuration("Glow", 0.1f);
			if (light.lightBottom != null)
			{
				light.lightBottom.transitionToDuration("Glow", 0.1f);
			}
		}
		game.startTimer(new ConnectorsTimer(0), 0.5f);
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void solveConnectors()
	{
		if (!solvedConnectors)
		{
			solvedConnectors = true;
			connectorsArtifact.targetable = true;
			connectorsArtifact.gameObject.SetActive(value: true);
			sphereOpenDoors.transitionTo("Open");
			sphereCloseScreen.transitionTo("NewState");
			sphereZooms[0].targetable = false;
			connectorTransitionTween.transitionToDuration("Tiny", 0.1f);
			game.finishPuzzle(Puzzle.Connectors);
		}
	}

	private void initTrail()
	{
		int num = 0;
		for (int i = 0; i < trailImages.Length; i++)
		{
			if (num < trailCurrentValues.Count && trailCurrentValues[num] == i)
			{
				trailImages[i].SetActive(value: true);
				num++;
			}
			else
			{
				trailImages[i].SetActive(value: false);
			}
		}
		onTrailHintRestartButton();
	}

	private void onTrailHintRestartButton()
	{
		AnimationSampler[] array = trailHintAnimations;
		foreach (AnimationSampler obj in array)
		{
			obj.setUnitTime(0f);
			obj.play();
		}
	}

	private void onTrailUpdate()
	{
		if (trailHintAnimations[0].unitTime >= 1f)
		{
			onTrailHintRestartButton();
		}
	}

	private void onTrailButton(int buttonIndex, Switch3DEvent switchEvent)
	{
		switch (switchEvent)
		{
		case Switch3DEvent.Start:
		{
			Switch3D[] array = trailButtons;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].targetable = false;
			}
			break;
		}
		case Switch3DEvent.On:
		{
			int num = trailCurrentValues[buttonIndex];
			int num2 = (1 + buttonIndex) * 6;
			int num3 = num + 1;
			if (num3 >= num2)
			{
				num3 -= 6;
			}
			trailImages[num].SetActive(value: false);
			trailImages[num3].SetActive(value: true);
			trailCurrentValues[buttonIndex] = num3;
			game.startSwitch(trailButtons[buttonIndex]);
			break;
		}
		case Switch3DEvent.Off:
			if (checkTrail())
			{
				game.callRPC(RPCs.TrailSolved);
			}
			else
			{
				game.callRPC(RPCs.TrailNotSolved);
			}
			break;
		}
	}

	private bool checkTrail()
	{
		bool result = true;
		for (int i = 0; i < trailCurrentValues.Count; i++)
		{
			if (trailCurrentValues[i] != trailSolution[i])
			{
				result = false;
				break;
			}
		}
		return result;
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void solveTrail()
	{
		if (!solvedTrail)
		{
			solvedTrail = true;
			trailArtefactDoorSeq.play();
			game.finishPuzzle(Puzzle.Trail);
		}
	}

	private void trailNotSolved()
	{
		Switch3D[] array = trailButtons;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = true;
		}
	}

	private void onTrailArtefactDoorSeqDone()
	{
		trailArtifact.Get<Item>(0).targetable = true;
		trailArtifact.Get<GameObject>(0u).SetActive(value: true);
		sphereZooms[1].targetable = false;
		trailArtefactTween.transitionTo("Display");
	}

	private void onTrailArtefactTweenDone()
	{
		game.setParent(trailArtifact.Get<Transform>(0f), trailZoomTransform);
	}

	private void initArtefact()
	{
		endPlane.Get<GameObject>(0).SetActive(value: false);
		GameObject[] array = artefactSpinAnimations;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(value: false);
		}
	}

	private void onArtefactSlot(Slot slot)
	{
		if (!(slot.insertedItem != null))
		{
			return;
		}
		int i = artefacts.FindIndex<Item>((Item x) => x == slot.insertedItem);
		if (slot.insertedItem == brokenGem.Get<Item>(0))
		{
			if (!fixedGem)
			{
				artefacts[i].Get<MaterialState>(0f).transitionTo("Red", 2f);
			}
			else
			{
				fixedArtifact.Get<MaterialState>(0f).transitionTo("Blue", 2f);
			}
		}
		else
		{
			artefacts[i].Get<MaterialState>(0f).transitionTo("Blue", 2f);
		}
	}

	private bool checkArtefactSlots()
	{
		bool result = true;
		foreach (Ref<Slot, Collider> gemSlot in gemSlots)
		{
			Slot slot = gemSlot;
			if ((slot.insertedItem != null && game.checkSlotSolution(slot, brokenGem.Get<Item>(0)) && !fixedGem) || !slot.isUnlocked)
			{
				result = false;
				break;
			}
		}
		return result;
	}

	private void startSolveArtefactSlotsTimer()
	{
		foreach (Ref<Slot, Collider> gemSlot in gemSlots)
		{
			((Slot)gemSlot).targetable = false;
		}
		game.startTimer(new SolveArtefactsTimer(0), 1f);
	}

	private void onArtefactRemoveFromSlot(Item item)
	{
		int i = artefacts.FindIndex<Item>((Item x) => x == item);
		artefacts[i].Get<MaterialState>(0f).transitionTo("Red", 1f, 0f);
		artefacts[i].Get<MaterialState>(0f).transitionTo("Blue", 1f, 0f);
		if (item == brokenGem.Get<Item>(0))
		{
			fixedArtifact.Get<MaterialState>(0f).transitionTo("Red", 1f, 0f);
			fixedArtifact.Get<MaterialState>(0f).transitionTo("Blue", 1f, 0f);
		}
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void solveArtefactSlots()
	{
		artefactPlateMaterialState.transitionTo("Dissolve", 0.4f);
		foreach (Ref<Slot, Collider> gemSlot in gemSlots)
		{
			gemSlot.Get<Slot>(0).gameObject.SetActive(value: false);
			if (gemSlot.Get<Slot>(0).insertedItem != null)
			{
				gemSlot.Get<Slot>(0).insertedItem.gameObject.SetActive(value: false);
			}
		}
		artefactSpinAnimations[0].SetActive(value: true);
		game.finishPuzzle(Puzzle.Artifacts);
		PineFmod.playOneShotSound("event:/Sound Effects/05 Misc/Electricity/Sphere_Open");
		game.startTimer(new SolveArtefactsTimer(1), 2f);
	}

	private void initSphereCovers()
	{
		for (int i = 0; i < sphereCovers.Length; i++)
		{
			sphereCoverColliders.Add(new List<Collider>());
			Collider[] componentsInChildren = sphereCovers[i].Get<GameObject>(0).GetComponentsInChildren<Collider>();
			foreach (Collider item in componentsInChildren)
			{
				sphereCoverColliders[i].Add(item);
			}
		}
	}

	private void onWelderTool(Item tool, ToolContext context)
	{
		if (!(tool == welder) || !solvedWelderGraph)
		{
			return;
		}
		welderToolState = context.state;
		GameObject impostorInHand = game.getImpostorInHand();
		Game.GamePlayerData playerWithItemInInventory = game.getPlayerWithItemInInventory(welder.gameObject);
		if (context.state == ToolState.Start)
		{
			welderHitDefaultParticles.Get<VisualEffect>(0f).Play();
			laserLineRenderer.Get<GameObject>(0f).SetActive(value: true);
			laserLineRenderer.Get<LineRenderer>(0).positionCount = 2;
			if (playerWithItemInInventory.id == game.localPlayerData.id)
			{
				PineFmod.playOneShotSoundAttached("event:/Sound Effects/04 Items/Weapons/Handheld Tractor Beam/Handheld_Tractor_Beam_Start", impostorInHand);
			}
			else
			{
				PineFmod.playOneShotSoundAttached("event:/Sound Effects/04 Items/Weapons/Handheld Tractor Beam/Handheld_Tractor_Beam_Start", playerWithItemInInventory.playerObject);
			}
			welderSoundInstance.start();
		}
		else if (context.state == ToolState.End)
		{
			game.cancelTimers<WelderParticleStopTimer>();
			welderHitParticles.Get<VisualEffect>(0f).Stop();
			welderHitDefaultParticles.Get<VisualEffect>(0f).Stop();
			laserLineRenderer.Get<GameObject>(0f).SetActive(value: false);
			laserLineRenderer.Get<LineRenderer>(0).positionCount = 2;
			welderSoundInstance.stop(STOP_MODE.ALLOWFADEOUT);
			if (game.localPlayerData.id == playerWithItemInInventory.id)
			{
				PineFmod.playOneShotSoundAttached("event:/Sound Effects/04 Items/Weapons/Handheld Tractor Beam/Handheld_Tractor_Beam_End", impostorInHand);
			}
			else
			{
				PineFmod.playOneShotSoundAttached("event:/Sound Effects/04 Items/Weapons/Handheld Tractor Beam/Handheld_Tractor_Beam_End", playerWithItemInInventory.playerObject);
			}
		}
		else if (game.localPlayerData.id == playerWithItemInInventory.id)
		{
			PineFmod.set3DAttributes(welderSoundInstance, PineFmod.to3DAttributes(impostorInHand.transform));
		}
		else
		{
			PineFmod.set3DAttributes(welderSoundInstance, PineFmod.to3DAttributes(playerWithItemInInventory.playerObject.transform));
		}
		if (!cutShield)
		{
			return;
		}
		int num = welderTargets.FindIndex<ToolTarget>((ToolTarget x) => x == context.currentTarget);
		if (context.currentTarget == null || num < 0)
		{
			stopWelderHitParticle();
			return;
		}
		float num2 = Mathf.Abs(context.currentRay.origin.y - context.currentTargetHitPoint.y);
		if ((!(context.currentRay.origin.y < 1f) || !(num2 < 1.8f)) && num2 > 1.2f)
		{
			stopWelderHitParticle();
			return;
		}
		if (welderCurrentTarget != context.currentTarget)
		{
			welderCurrentTarget = context.currentTarget;
			welderCurrentTimer = welderMaxDuration;
		}
		if (context.state == ToolState.Update)
		{
			if (welderCurrentTarget != null)
			{
				playWelderHitParticle();
				welderCurrentTimer -= context.deltaTime;
				if (welderCurrentTimer < 0f)
				{
					game.session.send(new WeldPacket
					{
						weldIndex = num
					});
					onWelderTargeted(num);
					welderCurrentTarget = null;
				}
			}
			else
			{
				stopWelderHitParticle();
			}
		}
		if (context.state == ToolState.End)
		{
			stopWelderHitParticle();
			welderCurrentTarget = null;
		}
	}

	private void playWelderHitParticle()
	{
		welderHitParticles.Get<VisualEffect>(0f).Play();
		game.cancelTimers<WelderParticleStopTimer>();
	}

	private void stopWelderHitParticle()
	{
		game.startTimer(new WelderParticleStopTimer(), 0.3f);
	}

	private void onWelderLateUpdate()
	{
		if (welderToolState == ToolState.End)
		{
			return;
		}
		Game.GamePlayerData playerWithItemInInventory = game.getPlayerWithItemInInventory(welder.gameObject);
		if (playerWithItemInInventory == null)
		{
			return;
		}
		Ray ray = playerWithItemInInventory.playerCameraRay;
		Vector3 vector;
		if (playerWithItemInInventory.id == game.localPlayerData.id)
		{
			if (!game.isSelectedInPCMode(welder.gameObject))
			{
				return;
			}
			ray = new Ray(game.getCameraPosition(), game.getCameraForward());
			vector = game.getImpostorInHand().transform.GetComponentInChildren<PersistantInImpostor>().transform.position;
		}
		else
		{
			vector = playerWithItemInInventory.playerCameraRay.GetPoint(0.6f);
		}
		laserLineRenderer.Get<LineRenderer>(0).positionCount = 2;
		laserLineRenderer.Get<LineRenderer>(0).SetPosition(0, vector);
		Vector3 point = ray.GetPoint(20f);
		int layerMask = -5 & ~LayerMask.GetMask("RoomEditorSpecialObject", "IgnoreTrigger", "Teleportation", "VRHands");
		int num = Physics.RaycastNonAlloc(ray, sharedHits, 40f, layerMask, QueryTriggerInteraction.Ignore);
		if (num > 0)
		{
			int num2 = -1;
			float num3 = float.MaxValue;
			for (int i = 0; i < num; i++)
			{
				float num4 = Vector3.Distance(vector, sharedHits[i].point);
				if (num4 < num3)
				{
					num3 = num4;
					num2 = i;
				}
			}
			point = sharedHits[num2].point;
		}
		laserLineRenderer.Get<LineRenderer>(0).SetPosition(1, point);
		welderHitDefaultParticles.Get<Transform>(0).position = point;
		welderHitParticles.Get<Transform>(0).position = point;
	}

	private void onWelderTargeted(int targetIndex)
	{
		Ref<ToolTarget, MaterialState> obj = welderTargets[targetIndex];
		if (!hitWelds.Contains((ToolTarget)obj))
		{
			hitWelds.Add((ToolTarget)obj);
		}
		obj.Get<ToolTarget>(0).targetable = false;
		welderTargets[targetIndex].Get<MaterialState>(0f).transitionTo("Dissolve", 2f);
		game.startTimer(new WeldTargetedTimer(targetIndex), 0.5f);
	}

	private bool isSphereCoverSolved(int coverIndex)
	{
		GameObject gameObject = sphereCovers[coverIndex].Get<GameObject>(0);
		Interactive[] componentsInChildren = gameObject.GetComponentsInChildren<Interactive>(includeInactive: true);
		foreach (Interactive interactive in componentsInChildren)
		{
			if (!(interactive.transform == gameObject) && !hitWelds.Contains(interactive))
			{
				return false;
			}
		}
		return true;
	}

	private void solveSphereCover(int coverIndex)
	{
		if (coverIndex < 0)
		{
			Debug.LogError("Cover index not compatible");
			return;
		}
		sphereCovers[coverIndex].Get<MaterialState>(0f).transitionTo("Dissolve", 0.2f);
		PineFmod.playOneShotSoundAttached("event:/Sound Effects/05 Misc/Magic/Woosh_2", sphereCovers[coverIndex].Get<GameObject>(0));
		Debug.Log("COVER: " + coverIndex);
		foreach (Collider item in sphereCoverColliders[coverIndex])
		{
			item.enabled = false;
		}
		if (coverIndex == 1)
		{
			game.startTimer(new DroneFlyoutTimer(-1, -1), 4f);
		}
		game.startTimer(new SphereCoverTimer(coverIndex), 5f);
		switch (coverIndex)
		{
		case 2:
			sphereZooms[1].targetable = true;
			break;
		case 3:
			sphereZooms[0].targetable = true;
			break;
		}
		if (coverIndex == 0 || coverIndex == 1)
		{
			game.finishPuzzle(Puzzle.Unwelding);
		}
		else
		{
			game.finishPuzzle(Puzzle.UnweldingElevator);
		}
	}

	[DebugButton(null, Tint.Default, PostClickAction.HideButton, 0, new object[] { })]
	private void debugOpenSphereCovers()
	{
		for (int i = 0; i < sphereCovers.Length; i++)
		{
			solveSphereCover(i);
		}
	}

	[DebugButton(null, Tint.Default, PostClickAction.ReturnToGame, 0, new object[] { 0, 1 })]
	private void debugEnterSphereZoom(int index)
	{
		sphereZooms[index].targetable = true;
		game.handleZoomEnter(sphereZooms[index].gameObject, Game.ScreenTargetType.Zoomable);
	}

	private void initDrones()
	{
		droneMaterialStates = new MaterialState[drones.Length];
		for (int i = 0; i < drones.Length; i++)
		{
			droneMaterialStates[i] = drones[i].GetComponent<MaterialState>();
		}
		changeDroneDisplay(-1);
		droneDoorSeq.Get<Sequence>(0).setTime(droneDoorSeq.Get<Sequence>(0).sequenceDuration);
		for (int j = 0; j < drones.Length; j++)
		{
			drones[j].gameObject.SetActive(value: false);
			droneMaterialStates[j].setState("Transparent");
			droneButtonDoors[j].setState("Close");
			droneButtons[j].setValue(1f);
			droneButtons[j].targetable = false;
		}
	}

	private void startDroneFlyout()
	{
		droneFlyAnimation.play();
		game.startTimer(new DroneFlyoutTimer(0, 0), 0.5f);
		game.startTimer(new DroneFlyoutTimer(0, 1), 0.6f);
		game.startTimer(new DroneFlyoutTimer(0, 2), 0.7f);
		game.startTimer(new DroneFlyoutTimer(0, 3), 0.8f);
	}

	private void onDroneButton(Switch3D button, int index)
	{
		if (button.gameObject.activeSelf)
		{
			if (button.state == Switch3DState.On)
			{
				game.startSwitch(button);
			}
			droneDoorSeq.Get<Sequence>(0).play();
			game.startTimer(new OpenCompartmentSoundTimer(droneDoorSeq.Get<GameObject>(0f)), 0.01f);
			game.startTimer(new OpenCompartmentSoundTimer(droneDoorSeq.Get<GameObject>(0f)), 0.25f);
			game.startTimer(new OpenCompartmentSoundTimer(droneDoorSeq.Get<GameObject>(0f)), 0.5f);
			droneCurrentCamIndex = index;
			changeDroneDisplay(index);
		}
	}

	private void onDroneUpdate()
	{
		if (dronesFlew < 4)
		{
			return;
		}
		for (int i = 0; i < drones.Length; i++)
		{
			if (!drones[i].gameObject.activeSelf)
			{
				continue;
			}
			if ((bool)game.isInSlot(drones[i]))
			{
				droneMaterialStates[i].setState("Transparent", 0f);
				droneMaterialStates[i].setState("InSlot");
			}
			else if (!game.isInAnyPlayerInventory(drones[i].gameObject) && !dronesPickedUp[i])
			{
				droneMaterialStates[i].setState("InSlot", 0f);
				Vector3 cameraPosition = game.getCameraPosition();
				float num = Vector3.Distance(cameraPosition, drones[i].transform.position);
				float num2 = Mathf.Abs(cameraPosition.y - drones[i].transform.position.y);
				float num3 = ((num2 > 0.5f) ? 3f : 4f);
				if (i == 3)
				{
					num3 = ((num2 > 0.5f) ? 0.5f : 5f);
				}
				if (num > num3)
				{
					droneMaterialStates[i].setState("Transparent");
					drones[i].targetable = false;
				}
				else
				{
					float num4 = Mathf.InverseLerp(0.5f, 3f, num);
					droneMaterialStates[i].setState("Transparent", num4);
					drones[i].targetable = num4 < 0.7f;
				}
			}
		}
	}

	private void onDroneAddToInventory(int droneIndex)
	{
		Game.GamePlayerData playerWithItemInInventory = game.getPlayerWithItemInInventory(drones[droneIndex].gameObject);
		if (playerWithItemInInventory != null)
		{
			game.setParent(droneInventoryReflectionProbes[droneIndex].Get<Transform>(0f), playerWithItemInInventory.models.transform);
			droneInventoryReflectionProbes[droneIndex].Get<Transform>(0f).localPosition = Vector3.up;
		}
		droneMaterialStates[droneIndex].setState("Transparent", 0f);
		drones[droneIndex].targetable = true;
		dronesPickedUp[droneIndex] = true;
	}

	private void onDroneRemoveFromInventory(int droneIndex)
	{
		game.setParent(droneInventoryReflectionProbes[droneIndex].Get<Transform>(0f), drones[droneIndex].transform);
		droneInventoryReflectionProbes[droneIndex].Get<Transform>(0f).localPosition = Vector3.zero;
	}

	private void changeDroneDisplay(int targetIndex)
	{
		float num = 2f;
		droneCameraDisabled.Get<GameObject>(0).SetActive(value: true);
		droneCameraDisabled.Get<MaterialState>(0f).transitionTo("Dissolve", num, 0f);
		if (targetIndex >= 0)
		{
			Ref<HDAdditionalReflectionData, Transform> obj = (game.isInAnyPlayerInventory(drones[targetIndex].gameObject) ? droneInventoryReflectionProbes[droneCurrentCamIndex] : droneReflectionProbes[droneCurrentCamIndex]);
			droneDummyCamera.Get<Transform>(0).position = obj.Get<Transform>(0f).position - droneDummyCamera.Get<Transform>(0).forward * 0.1f;
			droneDummyCamera.Get<Camera>(0f).enabled = true;
			obj.Get<HDAdditionalReflectionData>(0).RequestRenderNextUpdate();
			game.startTimer(new UpdateDroneCamTimer(0, targetIndex), 1f / num);
		}
	}

	private void onDroneSlot()
	{
		droneSlot.targetable = false;
		if (droneSlot.insertedItem != null)
		{
			droneSlot.insertedItem.targetable = false;
		}
		for (int i = 0; i < droneButtons.Length; i++)
		{
			droneButtons[i].targetable = false;
		}
		int droneIndex = Array.IndexOf(drones, droneSlot.insertedItem);
		game.startTimer(new DronesTimer(0, droneIndex), 0.01f);
	}

	[DebugButton(null, Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void debugGetDrones()
	{
		Item[] array = drones;
		foreach (Item item in array)
		{
			item.targetable = true;
			game.addItemToInventory(item.gameObject);
		}
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void solveDrones()
	{
		droneSlotsTween.setState("Hide");
		droneArtifact.gameObject.SetActive(value: true);
		changeDroneDisplay(-1);
		droneDoorSeq.Get<Sequence>(0).play(-1f, droneDoorSeq.Get<Sequence>(0).sequenceDuration);
		game.startTimer(new OpenCompartmentSoundTimer(droneDoorSeq.Get<GameObject>(0f)), 0.01f);
		game.startTimer(new OpenCompartmentSoundTimer(droneDoorSeq.Get<GameObject>(0f)), 0.25f);
		game.startTimer(new OpenCompartmentSoundTimer(droneDoorSeq.Get<GameObject>(0f)), 0.5f);
		game.startTimer(new DronesTimer(6, -1), droneDoorSeq.Get<Sequence>(0).sequenceDuration);
	}

	private void onCargoDoorHandle()
	{
		cargoRoomDoor.transitionTo("Open");
		cargoObstacle.SetActive(value: false);
		cargoRoomDoorParticles.SetActive(value: true);
	}

	private void initChestLock()
	{
		controlRoomKey.targetable = false;
		chestKeyGO.SetActive(value: false);
		GameObject[] array = chestBottles;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(value: false);
		}
		Interactive[] componentsInChildren = newChestLockZoom.gameObject.GetComponentsInChildren<Interactive>();
		foreach (Interactive item in componentsInChildren)
		{
			newChestLockZoomInteractives.Add(item);
		}
		chestIcons.Add(new ChestIcon
		{
			letters = "ACE",
			type = ChestIcon.IconType.Funnel,
			piece = chestSlidablePieces[3]
		});
		chestIcons.Add(new ChestIcon
		{
			letters = "AD",
			type = ChestIcon.IconType.Romb,
			piece = chestSlidablePieces[4]
		});
		chestIcons.Add(new ChestIcon
		{
			letters = "ABCDE",
			type = ChestIcon.IconType.Pentagon,
			piece = chestSlidablePieces[1]
		});
		chestIcons.Add(new ChestIcon
		{
			letters = "ABCD",
			type = ChestIcon.IconType.Arrowhead,
			piece = chestSlidablePieces[2]
		});
		chestIcons.Add(new ChestIcon
		{
			letters = "ABCD",
			type = ChestIcon.IconType.Mercedes,
			piece = chestSlidablePieces[0]
		});
		chestRows.Add(new ChestRow
		{
			letters = new List<char> { 'A' },
			letterStates = new List<MaterialState> { chestLockLetters[0] },
			slidableNodeIndex = 1
		});
		chestRows.Add(new ChestRow
		{
			letters = new List<char> { 'D' },
			letterStates = new List<MaterialState> { chestLockLetters[1] },
			slidableNodeIndex = 3
		});
		chestRows.Add(new ChestRow
		{
			letters = new List<char> { 'D', 'E' },
			letterStates = new List<MaterialState>
			{
				chestLockLetters[3],
				chestLockLetters[2]
			},
			slidableNodeIndex = 5
		});
		chestRows.Add(new ChestRow
		{
			letters = new List<char> { 'A', 'B' },
			letterStates = new List<MaterialState>
			{
				chestLockLetters[5],
				chestLockLetters[4]
			},
			slidableNodeIndex = 7
		});
		chestRows.Add(new ChestRow
		{
			letters = new List<char> { 'A', 'B', 'C' },
			letterStates = new List<MaterialState>
			{
				chestLockLetters[8],
				chestLockLetters[7],
				chestLockLetters[6]
			},
			slidableNodeIndex = 9
		});
		MaterialState[] array2 = chestLockLetters;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].setState("Off");
		}
	}

	private void onCargoChestConfirmButton()
	{
		newChestConfirmButton.targetable = false;
		bool solved = checkChestLockSolution();
		game.startTimer(new CargoChestConfirmTimer(0, solved), 1f / newChestConfirmButton.transitionSpeed);
	}

	private void chestLockLightsOff()
	{
		foreach (ChestRow chestRow in chestRows)
		{
			chestRow.turnOffLight();
		}
	}

	private bool checkChestLockSolution()
	{
		bool result = true;
		foreach (ChestRow chestRow in chestRows)
		{
			newChestSlidableGraph.tryGetPiece(newChestSlidableGraph.nodes[chestRow.slidableNodeIndex], out var piece);
			if (!chestRow.checkIconMatch((piece == null) ? null : chestIcons.Find((ChestIcon x) => x.piece == piece.gameObject)))
			{
				result = false;
			}
		}
		return result;
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void solveChestLock()
	{
		game.startSwitch(cargoLockLid);
		newChestLockZoom.targetable = false;
		chestKeyGO.SetActive(value: true);
		GameObject[] array = chestBottles;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(value: true);
		}
		game.handleZoomLeave();
		controlRoomKey.targetable = true;
		controlRoomKey.targetPriority = 1;
		foreach (Interactive newChestLockZoomInteractive in newChestLockZoomInteractives)
		{
			newChestLockZoomInteractive.targetable = false;
		}
		game.finishPuzzle(Puzzle.Lockbox);
	}

	private void cargoChestNotSolved()
	{
		game.startTimer(new CargoChestConfirmTimer(1, solved: false), 1f / newChestConfirmButton.transitionSpeed);
	}

	private void onShieldScanCargoButton()
	{
		isCargoScanned = true;
		changeShieldScreen(ShieldScreenType.Scanning);
		float speed = 0.1f;
		shieldScanningUiAnimation.transitionTo("Load", speed);
		sphereScanningAnimation.SetActive(value: true);
		game.startTimer(new ShieldCargoTimer(0), 8f);
	}

	private void initShield()
	{
		shieldSphereSwivelCurrentValues = new float[triangleParents.Length];
		shieldTriangleRotationsCurrent = new float[triangleParents.Length];
		sphereScanningAnimation.SetActive(value: false);
		for (int i = 0; i < shieldSpheres.Length; i++)
		{
			foreach (Ref<GameObject, Collider, MaterialState> shieldSphere in shieldSpheres)
			{
				((MaterialState)shieldSphere).setState("Transparent");
			}
			shieldSphereXs[i].SetActive(value: false);
		}
		int num = 0;
		for (int j = 0; j < triangleParents.Length; j++)
		{
			List<GameObject> list = new List<GameObject>();
			for (int k = 0; k < 3; k++)
			{
				shieldTriangleLasers[num].SetActive(value: false);
				list.Add(shieldTriangleLasers[num]);
				num++;
			}
			Trigger[] triggers = triangleTriggers0;
			switch (j)
			{
			case 1:
				triggers = triangleTriggers1;
				break;
			case 2:
				triggers = triangleTriggers2;
				break;
			case 3:
				triggers = triangleTriggers3;
				break;
			case 4:
				triggers = triangleTriggers4;
				break;
			}
			ShieldTriangleSet shieldTriangleSet = new ShieldTriangleSet(triangleSwivels[j], triangleParents[j], shieldRotateTriangleEmpties[j], triggers, shieldUIAttempts[j], shieldTriangleMaterialStates[j], list);
			shieldTriangleScales.Add(shieldTriangleScaleSlidable.Get<Slidable>(0).value);
			shieldTriangleSets.Add(shieldTriangleSet);
			shieldTriangleRotations.Add(0f);
			shieldTriangleRotationsCurrent[j] = 0f;
			shieldTriangleSet.init(j == 0, shieldTriangleScaleSlidable.Get<Slidable>(0).value);
		}
		shieldSphereSwivelValues.Add(0f);
		shieldSphereSwivelValues.Add(0.666f);
		shieldSphereSwivelValues.Add(0.333f);
		shieldSphereSwivelCurrentValues[0] = 0f;
		shieldSphereSwivelCurrentValues[1] = 0.666f;
		shieldSphereSwivelCurrentValues[2] = 0.333f;
		game.startSwitch(shieldUIAttempts[0].Get<Switch3D>(0));
		changeShieldScreen(ShieldScreenType.ScanObjectButton);
		shield.Get<GameObject>(0).SetActive(value: true);
		shieldTriangleStartingRotation = shieldTriangleSets[0].rotateTriangle.Get<Transform>().localRotation;
		shieldTrianglesStartingRotations = new Quaternion[3];
		shieldTrianglesStartingRotations[0] = shieldTriangleSets[0].swivelPoint.Get<Transform>().rotation;
		shieldTrianglesStartingRotations[1] = shieldTriangleSets[1].swivelPoint.Get<Transform>().rotation;
		shieldTrianglesStartingRotations[2] = shieldTriangleSets[2].swivelPoint.Get<Transform>().rotation;
		shieldSphereStartingRotation = sphere.rotation;
	}

	private void changeShieldScreen(ShieldScreenType screen)
	{
		for (int i = 0; i < shieldScreens.Length; i++)
		{
			shieldScreens[i].SetActive(i == (int)screen);
		}
	}

	private void onShieldButtons(Switch3D targetSwitch, Switch3DEvent switchEvent)
	{
		int num = shieldUIAttempts.IndexOf(targetSwitch, 0);
		if (switchEvent == Switch3DEvent.On && num >= 0)
		{
			onShieldTriangleButton(num);
		}
		if (switchEvent == Switch3DEvent.Start && targetSwitch == shieldScanCargoButton.Get<Switch3D>())
		{
			onShieldScanCargoButton();
		}
		if (switchEvent == Switch3DEvent.Start && targetSwitch == shieldCutNowButton.Get<Switch3D>(0))
		{
			onShieldCutNowButton();
		}
	}

	private void onShieldTriangleButton(int index)
	{
		if (shieldTrianglePressed)
		{
			if (!shieldTrianglePressed && shieldTriangleSets[index].button.state == Switch3DState.On)
			{
				game.startSwitch(shieldTriangleSets[index].button);
			}
			shieldTrianglePressed = false;
			return;
		}
		shieldCurrentSelectedSet = index;
		shieldSphereRotationValue = shieldSphereSwivelValues[shieldCurrentSelectedSet];
		for (int i = 0; i < shieldTriangleSets.Count; i++)
		{
			shieldTriangleSets[i].deactivate();
			shieldTriangleSets[i].button.targetable = false;
			if (!initingShield && shieldTriangleSets[i].button.state == Switch3DState.On)
			{
				game.startSwitch(shieldTriangleSets[i].button);
			}
			shieldUIAttempts[i].Get<MaterialState>(0u).transitionTo("Disabled", 3f);
		}
		if (!initingShield)
		{
			shieldTrianglePressed = true;
		}
		Slidable slidable = shieldTriangleScaleSlidable.Get<Slidable>(0);
		float duration = 0.8f;
		game.startTransitionGlobal(shieldTriangleScaleSlidable.Get<Transform>(0u), duration, 0f, Vector3.Lerp(slidable.startNode.position, slidable.endNode.position, shieldTriangleScales[index]));
		Slidable slidable2 = shieldTriangleRotationSlidable.Get<Slidable>(0);
		game.startTransitionGlobal(shieldTriangleRotationSlidable.Get<Transform>(0u), duration, 0f, Vector3.Lerp(slidable2.startNode.position, slidable2.endNode.position, shieldTriangleRotations[shieldCurrentSelectedSet]));
		Slidable slidable3 = shieldSphereRotationSlidable.Get<Slidable>(0);
		game.startTransitionGlobal(shieldSphereRotationSlidable.Get<Transform>(0u), duration, 0f, Vector3.Lerp(slidable3.startNode.position, slidable3.endNode.position, shieldSphereSwivelValues[shieldCurrentSelectedSet]));
		shieldEnableControls(enable: false);
		isShieldChangingTriangle = true;
		game.startTimer(new SphereRotateTimer(index), duration);
	}

	private void shieldEnableControls(bool enable = true)
	{
		shieldTriangleScaleSlidable.Get<Slidable>(0).targetable = enable;
		shieldSphereRotationSlidable.Get<Slidable>(0).targetable = enable;
		shieldTriangleRotationSlidable.Get<Slidable>(0).targetable = enable;
	}

	private void onShieldScaleTrianglesSlidable(MoveEvent moveEvent)
	{
		for (int i = 0; i < shieldTriangleSets.Count; i++)
		{
			if (i == shieldCurrentSelectedSet)
			{
				shieldTriangleScales[i] = shieldTriangleScaleSlidable.Get<Slidable>(0).value;
				shieldTriangleSets[i].activate(shieldTriangleScales[i]);
			}
			else
			{
				shieldTriangleSets[i].deactivate();
			}
		}
	}

	private void onShieldTriangleRotationSlidable(MoveEvent moveEvent)
	{
		if (moveEvent == MoveEvent.Moved)
		{
			shieldTriangleRotations[shieldCurrentSelectedSet] = shieldTriangleRotationSlidable.Get<Slidable>(0).value;
		}
	}

	private void onShieldSphereRotationSlidable(MoveEvent moveEvent)
	{
		if (moveEvent == MoveEvent.Moved)
		{
			shieldSphereRotationValue = shieldSphereRotationSlidable.Get<Slidable>(0).value;
			shieldSphereSwivelValues[shieldCurrentSelectedSet] = shieldSphereRotationSlidable.Get<Slidable>(0).value;
		}
	}

	private void onShieldUpdate()
	{
		onShieldRotateSphere();
		onShieldRotateTriangles();
		void onShieldRotateSphere()
		{
			if (!solvedShield)
			{
				setCurrent(ref shieldSphereRotationsCurrent, shieldSphereRotationValue);
				sphere.rotation = shieldSphereStartingRotation * Quaternion.AngleAxis(shieldSphereRotationsCurrent * 360f, Vector3.forward);
				for (int i = 0; i < shieldTriangleSets.Count; i++)
				{
					setCurrent(ref shieldSphereSwivelCurrentValues[i], shieldSphereSwivelValues[i]);
					shieldTriangleSets[i].swivelPoint.Get<Transform>().rotation = shieldTrianglesStartingRotations[0] * Quaternion.AngleAxis((0f - (shieldSphereSwivelCurrentValues[i] - shieldSphereRotationsCurrent)) * 360f, Vector3.forward);
				}
			}
		}
		void onShieldRotateTriangles()
		{
			setCurrent(ref shieldTriangleRotationsCurrent[shieldCurrentSelectedSet], shieldTriangleRotations[shieldCurrentSelectedSet], 10f);
			shieldTriangleSets[shieldCurrentSelectedSet].rotateTriangle.Get<Transform>().localRotation = shieldTriangleStartingRotation * Quaternion.AngleAxis(shieldTriangleRotationsCurrent[shieldCurrentSelectedSet] * 360f, Vector3.right);
		}
		static void setCurrent(ref float current, float value, float speed = 5f)
		{
			float f = (value - current) * (1f - Mathf.Exp((0f - speed) * Time.deltaTime));
			current += Mathf.Sign(f) * Mathf.Min(1f * Time.deltaTime, Mathf.Abs(f));
		}
	}

	private void onShieldTriangleTrigger(TriggerEvent triggerEvent)
	{
		if (solvedShield)
		{
			return;
		}
		shieldEnteredSpheres.Clear();
		GameObject[] array = shieldSphereXs;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(value: false);
		}
		foreach (ShieldTriangleSet shieldTriangleSet in shieldTriangleSets)
		{
			Trigger[] triggers = shieldTriangleSet.triggers;
			for (int i = 0; i < triggers.Length; i++)
			{
				foreach (Interactive item in triggers[i].interactivesInTriggerLastEvent)
				{
					if (!shieldEnteredSpheres.Contains(item.gameObject))
					{
						int num = shieldSpheres.IndexOf(item.gameObject, 0);
						shieldEnteredSpheres.Add(item.gameObject);
						shieldSphereXs[num].SetActive(value: true);
					}
				}
			}
		}
		shieldSpheresLeftText.text = shieldEnteredSpheres.Count + "/" + shieldSpheres.Length;
	}

	[DebugButton(null, Tint.Default, PostClickAction.HideButton, 0, new object[] { })]
	private void debugInitShieldOperations()
	{
		openControlDoor();
		changeShieldScreen(ShieldScreenType.Puzzle);
		shieldControlsLid.transitionTo("Open");
		shield.Get<GameObject>(0).SetActive(value: true);
		foreach (Ref<GameObject, Collider, MaterialState> shieldSphere in shieldSpheres)
		{
			shieldSphere.Get<GameObject>(0).SetActive(value: true);
		}
	}

	private bool checkIsShieldSolved()
	{
		if (shieldEnteredSpheres.Count == shieldSpheres.Length && !game.isAnyPlayerInteracting(shieldSphereRotationSlidable.Get<GameObject>(0f)) && !game.isAnyPlayerInteracting(shieldTriangleRotationSlidable.Get<GameObject>(0f)))
		{
			return !game.isAnyPlayerInteracting(shieldTriangleScaleSlidable.Get<GameObject>(0f));
		}
		return false;
	}

	private void solveShield()
	{
		if (!solvedShield)
		{
			solvedShield = true;
			shieldEnableControls(enable: false);
			shieldControlsLid.transitionTo("Open", 1f, 0f);
			game.startTimer(new ShieldSolvedTimer(0), 0.5f);
			game.finishPuzzle(Puzzle.SphereShield);
		}
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void debugSolveShield()
	{
		cutShield = true;
		solvedShield = true;
		shieldEnableControls(enable: false);
		shieldControlsLid.transitionTo("Open", 1f, 0f);
		game.startTimer(new ShieldSolvedTimer(2), 0.5f);
	}

	private void onShieldCutNowButton()
	{
		if (!cutShield)
		{
			cutShield = true;
			GameObject[] array = shieldSphereXs;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetActive(value: false);
			}
			game.startTimer(new ShieldSolvedTimer(2), 1f);
		}
	}

	private void initWallHints()
	{
		for (int i = 0; i < wallHintButtons.Length; i++)
		{
			game.startTimer(new WallHintTimer(i, isGoingDown: true), 1f);
			game.startSwitch(wallHintButtons[i].Get<Switch3D>(0));
		}
	}

	private void onWallHintButton(Switch3DEvent state, int index)
	{
		if (state == Switch3DEvent.On)
		{
			wallHintHolos[index].SetActive(value: false);
		}
		else
		{
			wallHintHolos[index].SetActive(value: true);
		}
	}

	[DebugButton("Get Artefacts", Tint.Default, (PostClickAction)0, 0, new object[] { })]
	private void debugGetArtefacts()
	{
		Item[] acceptItems = gemSlots[0].Get<Slot>(0).acceptItems;
		foreach (Item item in acceptItems)
		{
			if (item.slot == null)
			{
				game.addItemToInventory(item.gameObject);
				item.targetable = true;
			}
		}
		acceptItems = gemSlots[0].Get<Slot>(0).rejectItems;
		foreach (Item item2 in acceptItems)
		{
			if (item2.slot == null)
			{
				game.addItemToInventory(item2.gameObject);
				item2.targetable = true;
			}
		}
		if (brokenGem.Get<Item>(0).slot == null)
		{
			game.addItemToInventory(brokenGem.Get<Item>(0).gameObject);
			brokenGem.Get<Item>(0).targetable = true;
		}
	}

	public override void onInitHints()
	{
		game.setPuzzleConditions(Puzzle.ElevatorDoor, default(Puzzle));
		game.setPuzzleConditions(Puzzle.ControlRoomDoor, Puzzle.Locker);
		game.setPuzzleConditions(Puzzle.Cargo, Puzzle.ControlRoomDoor);
		game.setPuzzleConditions(Puzzle.Welder, Puzzle.Cargo);
		game.setPuzzleConditions(Puzzle.SphereShield, Puzzle.ControlRoomDoor);
		game.setPuzzleConditions(Puzzle.UnweldingElevator, Puzzle.Welder, Puzzle.SphereShield, Puzzle.ElevatorDoor);
		game.setPuzzleConditions(Puzzle.Trail, Puzzle.UnweldingElevator);
		game.setPuzzleConditions(Puzzle.Drones, Puzzle.Unwelding);
		game.setPuzzleConditions(Puzzle.Connectors, Puzzle.UnweldingElevator);
		game.setPuzzleConditions(Puzzle.Artifacts, Puzzle.Unwelding, Puzzle.ArtifactRepair, Puzzle.Trail, Puzzle.Drones, Puzzle.Connectors);
		game.setPuzzleConditions(Puzzle.Unwelding, Puzzle.SphereShield, Puzzle.Welder);
		game.setRelevantObjectsForPuzzle(Puzzle.Lockbox, cargoManual.gameObject, newChestLockZoom.gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.ControlRoomDoor, controlRoomKey.gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.Cargo, cargoPieces[0].gameObject, cargoPieces[1].gameObject, cargoPieces[2].gameObject, cargoPieces[3].gameObject, cargoPieces[4].gameObject, cargoPieces[5].gameObject, cargoPieces[7].gameObject, cargoPieces[8].gameObject, cargoPieces[10].gameObject, cargoPieces[11].gameObject, cargoPieces[12].gameObject, cargoPieces[13].gameObject, cargoPieces[14].gameObject, cargoPieces[15].gameObject, cargoPieces[16].gameObject, cargoPieces[17].gameObject, cargoPieces[18].gameObject, cargoPieces[19].gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.Welder, welder.gameObject, welderManual, welderBox.gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.SphereShield, shieldScreenZoom.Get<GameObject>(0f));
		game.setRelevantObjectsForPuzzle(Puzzle.ElevatorDoor, cargoElevatorSlot.acceptItems[0].gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.ArtifactRepair, tablets[0].gameObject, tablets[1].gameObject, tablets[2].gameObject, brokenGem.Get<Item>(0).gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.Drones, drones[0].gameObject, drones[1].gameObject, drones[2].gameObject, drones[3].gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.Trail, sphereZooms[1].gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.Connectors, sphereZooms[0].gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.Unwelding, welder.gameObject);
		game.setRelevantObjectsForPuzzle(Puzzle.UnweldingElevator, welder.gameObject);
		game.setHintCondition(Puzzle.Lockbox, LockboxHint.PickUpManual, () => game.wasAddedToInventoryDuringCurrentPuzzle(cargoManual.gameObject));
		game.setHintCondition(Puzzle.Lockbox, LockboxHint.LookAtBox, () => game.wasLookedAtCurrentPuzzle(newChestLockZoom.gameObject));
		game.setHintCondition(Puzzle.ControlRoomDoor, ControlRoomDoorHint.GetKey, () => game.wasAddedToInventoryDuringCurrentPuzzle(controlRoomKey.gameObject));
		game.setHintCondition(Puzzle.Cargo, CargoHint.OpenDoor, () => cargoDoorHandle.state == Switch3DState.On);
		game.setHintCondition(Puzzle.Welder, WelderHint.PickUpContainer, () => game.wasAddedToInventoryDuringCurrentPuzzle(welderBox.gameObject));
		game.setHintCondition(Puzzle.Welder, WelderHint.GetWelderAndManual, () => game.wasAddedToInventoryDuringCurrentPuzzle(welder.gameObject) && game.wasAddedToInventoryDuringCurrentPuzzle(welderManual));
		game.setHintCondition(Puzzle.SphereShield, SphereShieldHint.PressButton, () => isCargoScanned);
		game.setHintCondition(Puzzle.ElevatorDoor, ElevatorDoorHint.GetKey, () => game.wasAddedToInventoryDuringCurrentPuzzle(cargoElevatorSlot.acceptItems[0].gameObject));
		game.setHintCondition(Puzzle.ArtifactRepair, ArtifactRepairHint.GetArtifactAndHints, () => game.wasAddedToInventoryDuringCurrentPuzzle(StructWrapperUtility.Get<GameObject>(brokenGem)) && game.wasAddedToInventoryDuringCurrentPuzzle(tablets[0].gameObject) && game.wasAddedToInventoryDuringCurrentPuzzle(tablets[2].gameObject));
		game.setHintCondition(Puzzle.ArtifactRepair, ArtifactRepairHint.GetHint, () => game.wasAddedToInventoryDuringCurrentPuzzle(tablets[1].gameObject));
		game.setHintCondition(Puzzle.ArtifactRepair, ArtifactRepairHint.PlaceArtifact, () => brokenGem.Get<Item>(0).slot == fabricatorSlot);
		game.setHintCondition(Puzzle.Artifacts, ArtifactsHint.GetConnectors, () => game.wasAddedToInventoryDuringCurrentPuzzle(connectorsArtifact.gameObject));
		game.setHintCondition(Puzzle.Artifacts, ArtifactsHint.GetFabricator, () => game.wasAddedToInventoryDuringCurrentPuzzle(brokenGem.Get<Item>(0).gameObject));
		game.setHintCondition(Puzzle.Artifacts, ArtifactsHint.GetDrone, () => game.wasAddedToInventoryDuringCurrentPuzzle(droneArtifact.gameObject));
		game.setHintCondition(Puzzle.Artifacts, ArtifactsHint.GetTrail, () => game.wasAddedToInventoryDuringCurrentPuzzle(trailArtifact.Get<GameObject>(0u)));
	}

	public override void onPacket(Packet packet)
	{
		if (packet is FabricatorButtonFixPacket fabricatorButtonFixPacket)
		{
			handleFabricatorFixObjectButton(fabricatorButtonFixPacket.isCorrectSolution, shouldSync: false);
		}
		else if (packet is ElevatorMovePacket elevatorMovePacket)
		{
			if (elevatorMovePacket.isPos)
			{
				elevatorRb.MovePosition(elevatorMovePacket.position);
			}
			else
			{
				elevatorRb.MoveRotation(elevatorMovePacket.rotation);
			}
		}
		else if (packet is WeldPacket weldPacket)
		{
			onWelderTargeted(weldPacket.weldIndex);
		}
	}

	public virtual void save(FastBinaryWriter writer)
	{
		writer.WriteList(blinkingErrorsActivated, delegate(FastBinaryWriter w, GameObject e)
		{
			w.WriteGameObject(e);
		});
		writer.Write(in blinkingTimer, default(FastBinaryWriter.ForPrimitives));
		writer.WriteList(cargoButtons, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.Write(in solvedCargo, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in isMovingElevator, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in isRaisingElevator, default(FastBinaryWriter.ForPrimitives));
		writer.WriteQuaternion(in elevatorStartingRotation);
		writer.Write(in elevatorCurrentAngle, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in elevatorUpdatingAngle, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in isLoweringElevator, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in elevatorHitOnUp, default(FastBinaryWriter.ForPrimitives));
		int value = (int)currentElevatorScreenType;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in elevatorCheck, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in ladderRaised, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in solvedWelderGraph, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in welderSolvedTimer, default(FastBinaryWriter.ForPrimitives));
		writer.WriteArray(welderCurrentLineSwaps, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteArray(welderCurrentGateSwaps, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteDictionary(welderLineCombinations, delegate(FastBinaryWriter w, int k)
		{
			w.Write(in k, default(FastBinaryWriter.ForPrimitives));
		}, delegate(FastBinaryWriter w, int[] v)
		{
			w.WriteArray(v, delegate(FastBinaryWriter fastBinaryWriter, int e)
			{
				fastBinaryWriter.Write(in e, default(FastBinaryWriter.ForPrimitives));
			});
		});
		writer.WriteDictionary(welderGateCombinations, delegate(FastBinaryWriter w, int k)
		{
			w.Write(in k, default(FastBinaryWriter.ForPrimitives));
		}, delegate(FastBinaryWriter w, int[] v)
		{
			w.WriteArray(v, delegate(FastBinaryWriter fastBinaryWriter, int e)
			{
				fastBinaryWriter.Write(in e, default(FastBinaryWriter.ForPrimitives));
			});
		});
		writer.WriteList(hitWelds, delegate(FastBinaryWriter w, Interactive e)
		{
			w.WriteComponent(e);
		});
		value = (int)welderToolState;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		writer.WriteList(toolLockerSolutions, delegate(FastBinaryWriter w, string e)
		{
			w.Write(e);
		});
		writer.WriteList(toolLockerCurrentValues, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.Write(in lockerOpened, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in solvedLocker, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in currentConnectorPuzzle, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in solvedConnectors, default(FastBinaryWriter.ForPrimitives));
		writer.WriteList(trailCurrentValues, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.Write(in solvedTrail, default(FastBinaryWriter.ForPrimitives));
		writer.WriteComponent(welderCurrentTarget);
		writer.Write(in welderCurrentTimer, default(FastBinaryWriter.ForPrimitives));
		writer.WriteArray(dronesPickedUp, delegate(FastBinaryWriter w, bool e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.Write(in dronesFlew, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in droneCurrentCamIndex, default(FastBinaryWriter.ForPrimitives));
		writer.WriteArray(solvedDrones, delegate(FastBinaryWriter w, bool e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.Write(in fixedGem, default(FastBinaryWriter.ForPrimitives));
		writer.WriteList(shieldEnteredSpheres, delegate(FastBinaryWriter w, GameObject e)
		{
			w.WriteGameObject(e);
		});
		writer.Write(in cutShield, default(FastBinaryWriter.ForPrimitives));
		writer.WriteQuaternion(in shieldRotatingTriangle);
		writer.WriteQuaternion(in shieldRotatingSphere);
		writer.Write(in isShieldChangingTriangle, default(FastBinaryWriter.ForPrimitives));
		writer.WriteList(shieldTriangleScales, delegate(FastBinaryWriter w, float e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteList(shieldTriangleRotations, delegate(FastBinaryWriter w, float e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteArray(shieldTriangleRotationsCurrent, delegate(FastBinaryWriter w, float e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteList(shieldSphereSwivelValues, delegate(FastBinaryWriter w, float e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteArray(shieldSphereSwivelCurrentValues, delegate(FastBinaryWriter w, float e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.Write(in shieldSphereRotationValue, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in shieldSphereRotationsCurrent, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in shieldCurrentSelectedSet, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in solvedShield, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in shieldSolvedTime, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in shieldTrianglePressed, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in initingShield, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in isCargoScanned, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void load(FastBinaryReader reader)
	{
		blinkingErrorsActivated = reader.ReadList((FastBinaryReader r) => r.ReadGameObject());
		blinkingTimer = reader.ReadSingle();
		cargoButtons = reader.ReadList((FastBinaryReader r) => r.ReadInt32());
		solvedCargo = reader.ReadBoolean();
		isMovingElevator = reader.ReadBoolean();
		isRaisingElevator = reader.ReadBoolean();
		elevatorStartingRotation = reader.ReadQuaternion();
		elevatorCurrentAngle = reader.ReadSingle();
		elevatorUpdatingAngle = reader.ReadSingle();
		isLoweringElevator = reader.ReadBoolean();
		elevatorHitOnUp = reader.ReadBoolean();
		currentElevatorScreenType = (ElevatorScreenType)reader.ReadInt32();
		elevatorCheck = reader.ReadBoolean();
		ladderRaised = reader.ReadBoolean();
		solvedWelderGraph = reader.ReadBoolean();
		welderSolvedTimer = reader.ReadSingle();
		welderCurrentLineSwaps = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		welderCurrentGateSwaps = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		welderLineCombinations = reader.ReadDictionary((FastBinaryReader kr) => kr.ReadInt32(), (FastBinaryReader vr) => vr.ReadArray((FastBinaryReader r) => r.ReadInt32()));
		welderGateCombinations = reader.ReadDictionary((FastBinaryReader kr) => kr.ReadInt32(), (FastBinaryReader vr) => vr.ReadArray((FastBinaryReader r) => r.ReadInt32()));
		hitWelds = reader.ReadList((FastBinaryReader r) => r.ReadComponent<Interactive>());
		welderToolState = (ToolState)reader.ReadInt32();
		toolLockerSolutions = reader.ReadList((FastBinaryReader r) => r.ReadString());
		toolLockerCurrentValues = reader.ReadList((FastBinaryReader r) => r.ReadInt32());
		lockerOpened = reader.ReadBoolean();
		solvedLocker = reader.ReadBoolean();
		currentConnectorPuzzle = reader.ReadInt32();
		solvedConnectors = reader.ReadBoolean();
		trailCurrentValues = reader.ReadList((FastBinaryReader r) => r.ReadInt32());
		solvedTrail = reader.ReadBoolean();
		welderCurrentTarget = reader.ReadComponent<Interactive>();
		welderCurrentTimer = reader.ReadSingle();
		dronesPickedUp = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		dronesFlew = reader.ReadInt32();
		droneCurrentCamIndex = reader.ReadInt32();
		solvedDrones = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		fixedGem = reader.ReadBoolean();
		shieldEnteredSpheres = reader.ReadList((FastBinaryReader r) => r.ReadGameObject());
		cutShield = reader.ReadBoolean();
		shieldRotatingTriangle = reader.ReadQuaternion();
		shieldRotatingSphere = reader.ReadQuaternion();
		isShieldChangingTriangle = reader.ReadBoolean();
		shieldTriangleScales = reader.ReadList((FastBinaryReader r) => r.ReadSingle());
		shieldTriangleRotations = reader.ReadList((FastBinaryReader r) => r.ReadSingle());
		shieldTriangleRotationsCurrent = reader.ReadArray((FastBinaryReader r) => r.ReadSingle());
		shieldSphereSwivelValues = reader.ReadList((FastBinaryReader r) => r.ReadSingle());
		shieldSphereSwivelCurrentValues = reader.ReadArray((FastBinaryReader r) => r.ReadSingle());
		shieldSphereRotationValue = reader.ReadSingle();
		shieldSphereRotationsCurrent = reader.ReadSingle();
		shieldCurrentSelectedSet = reader.ReadInt32();
		solvedShield = reader.ReadBoolean();
		shieldSolvedTime = reader.ReadSingle();
		shieldTrianglePressed = reader.ReadBoolean();
		initingShield = reader.ReadBoolean();
		isCargoScanned = reader.ReadBoolean();
	}

	public virtual void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		int position = reader.Position;
		List<GameObject> list = reader.ReadList((FastBinaryReader r) => r.ReadGameObject());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "blinkingErrorsActivated[" + ((list == null) ? string.Empty : list.Count.ToString()) + "]",
			fieldValue = (((list == null) ? "null" : string.Join(", ", list)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "blinkingTimer",
			fieldValue = $"{num}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<int> list2 = reader.ReadList((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "cargoButtons[" + ((list2 == null) ? string.Empty : list2.Count.ToString()) + "]",
			fieldValue = (((list2 == null) ? "null" : string.Join(", ", list2)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "solvedCargo",
			fieldValue = $"{flag}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag2 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "isMovingElevator",
			fieldValue = $"{flag2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag3 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "isRaisingElevator",
			fieldValue = $"{flag3}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Quaternion quaternion = reader.ReadQuaternion();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "elevatorStartingRotation",
			fieldValue = $"{quaternion}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num2 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "elevatorCurrentAngle",
			fieldValue = $"{num2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num3 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "elevatorUpdatingAngle",
			fieldValue = $"{num3}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag4 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "isLoweringElevator",
			fieldValue = $"{flag4}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag5 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "elevatorHitOnUp",
			fieldValue = $"{flag5}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		ElevatorScreenType elevatorScreenType = (ElevatorScreenType)reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "currentElevatorScreenType",
			fieldValue = $"{elevatorScreenType}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag6 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "elevatorCheck",
			fieldValue = $"{flag6}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag7 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "ladderRaised",
			fieldValue = $"{flag7}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag8 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "solvedWelderGraph",
			fieldValue = $"{flag8}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num4 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "welderSolvedTimer",
			fieldValue = $"{num4}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int[] array = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "welderCurrentLineSwaps[" + ((array == null) ? string.Empty : array.Length.ToString()) + "]",
			fieldValue = (((array == null) ? "null" : string.Join(", ", array)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int[] array2 = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "welderCurrentGateSwaps[" + ((array2 == null) ? string.Empty : array2.Length.ToString()) + "]",
			fieldValue = (((array2 == null) ? "null" : string.Join(", ", array2)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Dictionary<int, int[]> dictionary = reader.ReadDictionary((FastBinaryReader kr) => kr.ReadInt32(), (FastBinaryReader vr) => vr.ReadArray((FastBinaryReader r) => r.ReadInt32()));
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "welderLineCombinations[" + ((dictionary == null) ? string.Empty : dictionary.Count.ToString()) + "]",
			fieldValue = (((dictionary == null) ? "null" : string.Join(", ", dictionary)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Dictionary<int, int[]> dictionary2 = reader.ReadDictionary((FastBinaryReader kr) => kr.ReadInt32(), (FastBinaryReader vr) => vr.ReadArray((FastBinaryReader r) => r.ReadInt32()));
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "welderGateCombinations[" + ((dictionary2 == null) ? string.Empty : dictionary2.Count.ToString()) + "]",
			fieldValue = (((dictionary2 == null) ? "null" : string.Join(", ", dictionary2)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<Interactive> list3 = reader.ReadList((FastBinaryReader r) => r.ReadComponent<Interactive>());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "hitWelds[" + ((list3 == null) ? string.Empty : list3.Count.ToString()) + "]",
			fieldValue = (((list3 == null) ? "null" : string.Join(", ", list3)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		ToolState toolState = (ToolState)reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "welderToolState",
			fieldValue = $"{toolState}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<string> list4 = reader.ReadList((FastBinaryReader r) => r.ReadString());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "toolLockerSolutions[" + ((list4 == null) ? string.Empty : list4.Count.ToString()) + "]",
			fieldValue = (((list4 == null) ? "null" : string.Join(", ", list4)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<int> list5 = reader.ReadList((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "toolLockerCurrentValues[" + ((list5 == null) ? string.Empty : list5.Count.ToString()) + "]",
			fieldValue = (((list5 == null) ? "null" : string.Join(", ", list5)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag9 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "lockerOpened",
			fieldValue = $"{flag9}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag10 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "solvedLocker",
			fieldValue = $"{flag10}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num5 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "currentConnectorPuzzle",
			fieldValue = $"{num5}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag11 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "solvedConnectors",
			fieldValue = $"{flag11}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<int> list6 = reader.ReadList((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "trailCurrentValues[" + ((list6 == null) ? string.Empty : list6.Count.ToString()) + "]",
			fieldValue = (((list6 == null) ? "null" : string.Join(", ", list6)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag12 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "solvedTrail",
			fieldValue = $"{flag12}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Interactive arg = reader.ReadComponent<Interactive>();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "welderCurrentTarget",
			fieldValue = $"{arg}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num6 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "welderCurrentTimer",
			fieldValue = $"{num6}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool[] array3 = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "dronesPickedUp[" + ((array3 == null) ? string.Empty : array3.Length.ToString()) + "]",
			fieldValue = (((array3 == null) ? "null" : string.Join(", ", array3)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num7 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "dronesFlew",
			fieldValue = $"{num7}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num8 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "droneCurrentCamIndex",
			fieldValue = $"{num8}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool[] array4 = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "solvedDrones[" + ((array4 == null) ? string.Empty : array4.Length.ToString()) + "]",
			fieldValue = (((array4 == null) ? "null" : string.Join(", ", array4)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag13 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "fixedGem",
			fieldValue = $"{flag13}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<GameObject> list7 = reader.ReadList((FastBinaryReader r) => r.ReadGameObject());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "shieldEnteredSpheres[" + ((list7 == null) ? string.Empty : list7.Count.ToString()) + "]",
			fieldValue = (((list7 == null) ? "null" : string.Join(", ", list7)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag14 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "cutShield",
			fieldValue = $"{flag14}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Quaternion quaternion2 = reader.ReadQuaternion();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "shieldRotatingTriangle",
			fieldValue = $"{quaternion2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Quaternion quaternion3 = reader.ReadQuaternion();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "shieldRotatingSphere",
			fieldValue = $"{quaternion3}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag15 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "isShieldChangingTriangle",
			fieldValue = $"{flag15}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<float> list8 = reader.ReadList((FastBinaryReader r) => r.ReadSingle());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "shieldTriangleScales[" + ((list8 == null) ? string.Empty : list8.Count.ToString()) + "]",
			fieldValue = (((list8 == null) ? "null" : string.Join(", ", list8)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<float> list9 = reader.ReadList((FastBinaryReader r) => r.ReadSingle());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "shieldTriangleRotations[" + ((list9 == null) ? string.Empty : list9.Count.ToString()) + "]",
			fieldValue = (((list9 == null) ? "null" : string.Join(", ", list9)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float[] array5 = reader.ReadArray((FastBinaryReader r) => r.ReadSingle());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "shieldTriangleRotationsCurrent[" + ((array5 == null) ? string.Empty : array5.Length.ToString()) + "]",
			fieldValue = (((array5 == null) ? "null" : string.Join(", ", array5)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<float> list10 = reader.ReadList((FastBinaryReader r) => r.ReadSingle());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "shieldSphereSwivelValues[" + ((list10 == null) ? string.Empty : list10.Count.ToString()) + "]",
			fieldValue = (((list10 == null) ? "null" : string.Join(", ", list10)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float[] array6 = reader.ReadArray((FastBinaryReader r) => r.ReadSingle());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "shieldSphereSwivelCurrentValues[" + ((array6 == null) ? string.Empty : array6.Length.ToString()) + "]",
			fieldValue = (((array6 == null) ? "null" : string.Join(", ", array6)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num9 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "shieldSphereRotationValue",
			fieldValue = $"{num9}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num10 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "shieldSphereRotationsCurrent",
			fieldValue = $"{num10}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num11 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "shieldCurrentSelectedSet",
			fieldValue = $"{num11}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag16 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "solvedShield",
			fieldValue = $"{flag16}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num12 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "shieldSolvedTime",
			fieldValue = $"{num12}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag17 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "shieldTrianglePressed",
			fieldValue = $"{flag17}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag18 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "initingShield",
			fieldValue = $"{flag18}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag19 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "isCargoScanned",
			fieldValue = $"{flag19}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
	}

	public override int getPacketCount()
	{
		return Space3.getPacketCount();
	}

	public override Packet getPacket(byte id)
	{
		return Space3.getPacket(id);
	}

	public override Timer getTimer(byte id)
	{
		return id switch
		{
			0 => new WelderParticleStopTimer(), 
			1 => new ElevatorSoundTimer(), 
			2 => new OpenCompartmentSoundTimer(), 
			3 => new ConnectorsTimer(), 
			4 => new CargoTimer(), 
			5 => new LadderTimer(), 
			6 => new LockerScreenTimer(), 
			7 => new FabricatorFixObjectTimer(), 
			8 => new FabricatorStartScanTimer(), 
			9 => new FabricatorScannedTimer(), 
			10 => new DelayedConnectorsInitTimer(), 
			11 => new DroneFlyoutTimer(), 
			12 => new UpdateDroneCamTimer(), 
			13 => new DronesTimer(), 
			14 => new CargoChestConfirmTimer(), 
			15 => new SolveArtefactsTimer(), 
			16 => new ShieldCargoTimer(), 
			17 => new ShieldSolvedTimer(), 
			18 => new SphereRotateTimer(), 
			19 => new ToolLockerTimer(), 
			20 => new WallHintTimer(), 
			21 => new ElevatorKeycardTimer(), 
			22 => new WeldTargetedTimer(), 
			23 => new WelderTimer(), 
			24 => new SphereCoverTimer(), 
			_ => null, 
		};
	}

	[CompilerGenerated]
	public override void onTimerUpdate(Timer timer)
	{
		if (!(timer is WelderParticleStopTimer) && !(timer is ElevatorSoundTimer) && !(timer is OpenCompartmentSoundTimer) && !(timer is ConnectorsTimer) && !(timer is CargoTimer) && !(timer is LadderTimer) && !(timer is LockerScreenTimer) && !(timer is FabricatorFixObjectTimer) && !(timer is FabricatorStartScanTimer) && !(timer is FabricatorScannedTimer) && !(timer is DelayedConnectorsInitTimer) && !(timer is DroneFlyoutTimer) && !(timer is UpdateDroneCamTimer) && !(timer is DronesTimer) && !(timer is CargoChestConfirmTimer) && !(timer is SolveArtefactsTimer) && !(timer is ShieldCargoTimer) && !(timer is ShieldSolvedTimer) && !(timer is SphereRotateTimer) && !(timer is ToolLockerTimer) && !(timer is WallHintTimer) && !(timer is ElevatorKeycardTimer) && !(timer is WeldTargetedTimer) && !(timer is WelderTimer))
		{
			_ = timer is SphereCoverTimer;
		}
	}

	[CompilerGenerated]
	public override void onTimerDone(Timer timer)
	{
		if (timer is WelderParticleStopTimer timer2)
		{
			onWelderParticleStopTimerDone(timer2);
		}
		else if (timer is ElevatorSoundTimer timer3)
		{
			onElevatorSoundTimerDone(timer3);
		}
		else if (timer is OpenCompartmentSoundTimer timer4)
		{
			onOpenCompartmentSoundTimerDone(timer4);
		}
		else if (timer is ConnectorsTimer timer5)
		{
			onConnectorsTimerDone(timer5);
		}
		else if (timer is CargoTimer timer6)
		{
			onCargoTimerDone(timer6);
		}
		else if (timer is LadderTimer timer7)
		{
			onLadderTimerDone(timer7);
		}
		else if (timer is LockerScreenTimer timer8)
		{
			onLockerScreenTimerDone(timer8);
		}
		else if (timer is FabricatorFixObjectTimer timer9)
		{
			onFabricatorFixObjectTimerDone(timer9);
		}
		else if (timer is FabricatorStartScanTimer timer10)
		{
			onFabricatorStartScanTimerDone(timer10);
		}
		else if (timer is FabricatorScannedTimer timer11)
		{
			onFabricatorScannedTimerDone(timer11);
		}
		else if (timer is DelayedConnectorsInitTimer timer12)
		{
			onDelayedConnectorsInitTimerDone(timer12);
		}
		else if (timer is DroneFlyoutTimer timer13)
		{
			onDroneFlyoutTimerDone(timer13);
		}
		else if (timer is UpdateDroneCamTimer timer14)
		{
			onUpdateDroneCamTimerDone(timer14);
		}
		else if (timer is DronesTimer timer15)
		{
			onDronesTimerDone(timer15);
		}
		else if (timer is CargoChestConfirmTimer timer16)
		{
			onCargoChestConfirmTimerDone(timer16);
		}
		else if (timer is SolveArtefactsTimer timer17)
		{
			onSolveArtefactsTimerDone(timer17);
		}
		else if (timer is ShieldCargoTimer timer18)
		{
			onShieldCargoTimerDone(timer18);
		}
		else if (timer is ShieldSolvedTimer timer19)
		{
			onShieldSolvedTimerDone(timer19);
		}
		else if (timer is SphereRotateTimer timer20)
		{
			onSphereRotateTimerDone(timer20);
		}
		else if (timer is ToolLockerTimer timer21)
		{
			onToolLockerTimerDone(timer21);
		}
		else if (timer is WallHintTimer timer22)
		{
			onWallHintTimerDone(timer22);
		}
		else if (timer is ElevatorKeycardTimer timer23)
		{
			onElevatorKeycardTimerDone(timer23);
		}
		else if (timer is WeldTargetedTimer timer24)
		{
			onWeldTargetedTimerDone(timer24);
		}
		else if (timer is WelderTimer timer25)
		{
			onWelderTimerDone(timer25);
		}
		else if (timer is SphereCoverTimer timer26)
		{
			onSphereCoverTimerDone(timer26);
		}
	}

	private void onWelderParticleStopTimerDone(WelderParticleStopTimer timer)
	{
		if (!timer.isCancelled)
		{
			welderHitParticles.Get<VisualEffect>(0f).Stop();
		}
	}

	private void onElevatorSoundTimerDone(ElevatorSoundTimer timer)
	{
		if (!timer.isCancelled)
		{
			elevatorSoundInstances[timer.soundIndex].stop(STOP_MODE.ALLOWFADEOUT);
		}
	}

	private void onOpenCompartmentSoundTimerDone(OpenCompartmentSoundTimer timer)
	{
		PineFmod.playOneShotSoundAttached("event:/Sound Effects/03 Interactable/Doors/Airlock/AirLock_Open_Short", timer.soundRoot);
	}

	private void onConnectorsTimerDone(ConnectorsTimer timer)
	{
		if (timer.index == 0)
		{
			foreach (ConnectorPuzzle.Light light in connectorPuzzles[currentConnectorPuzzle].lights)
			{
				light.light.transitionToDuration("Glow", 0.1f, 0f);
				if (light.lightBottom != null)
				{
					light.lightBottom.transitionToDuration("Glow", 0.1f, 0f);
				}
			}
			game.startTimer(new ConnectorsTimer(1), 0.5f);
		}
		else if (timer.index == 1)
		{
			foreach (ConnectorPuzzle.Light light2 in connectorPuzzles[currentConnectorPuzzle].lights)
			{
				light2.light.transitionToDuration("Glow", 0.1f);
				if (light2.lightBottom != null)
				{
					light2.lightBottom.transitionToDuration("Glow", 0.1f);
				}
			}
			game.startTimer(new ConnectorsTimer(2), 0.5f);
		}
		else if (timer.index == 2)
		{
			foreach (ConnectorPuzzle.Light light3 in connectorPuzzles[currentConnectorPuzzle].lights)
			{
				light3.light.transitionToDuration("Glow", 0.1f);
				if (light3.lightBottom != null)
				{
					light3.lightBottom.transitionToDuration("Glow", 0.1f);
				}
			}
			game.startTimer(new ConnectorsTimer(3), 0.4f);
		}
		else if (timer.index == 3)
		{
			connectorTransitionTween.transitionToDuration("Tiny", 0.1f);
			game.startTimer(new ConnectorsTimer(4), 0.1f);
		}
		else
		{
			if (timer.index != 4)
			{
				return;
			}
			currentConnectorPuzzle++;
			if (currentConnectorPuzzle >= connectorPuzzles.Count)
			{
				solveConnectors();
				return;
			}
			connectorTransitionTween.transitionToDuration("Tiny", 0.3f, 0f);
			for (int i = 0; i < connectors.Length; i++)
			{
				connectors[i].SetActive(i == currentConnectorPuzzle);
			}
			connectorPuzzles[currentConnectorPuzzle].checkSolution();
		}
	}

	private void onCargoTimerDone(CargoTimer timer)
	{
		if (timer.index == 0)
		{
			cargoTween.transitionTo("MoveAway");
			game.startTimer(new CargoTimer(1), 1.3f);
		}
		else if (timer.index == 1)
		{
			cargoRaiseTween.transitionTo("MoveUp", 0.7f);
			game.startTimer(new CargoTimer(2), 1.7f);
			cargoEffect.SetActive(value: false);
		}
		else if (timer.index == 2)
		{
			cargoPallet.SetActive(value: true);
			cargoHoles.SetActive(value: false);
			cargoRaiseTween.transitionTo("MoveUp", 0.7f, 0f);
			game.startTimer(new CargoTimer(3), 1.2f);
		}
		else if (timer.index == 3)
		{
			cargoBattery.isKinematic = false;
			cargoTween.transitionTo("MoveAway", 1f, 0f);
			game.startTimer(new CargoTimer(4), 1f);
		}
		else if (timer.index == 4)
		{
			hazmatCargoDoor.transitionTo("Open", 1f, 0f);
		}
	}

	private void onLadderTimerDone(LadderTimer timer)
	{
		if (timer.index == -2)
		{
			controlRoomObstacle.SetActive(value: false);
			controlDoor.transitionTo("Down");
			controlRoomDoorParticles.SetActive(value: true);
			game.startTimer(new LadderTimer(-1), 0.5f);
			game.finishPuzzle(Puzzle.ControlRoomDoor);
		}
		else if (timer.index == -1)
		{
			enableLadder();
		}
	}

	private void onLockerScreenTimerDone(LockerScreenTimer timer)
	{
		if (timer.index >= 0)
		{
			changeLockerKeypadScreen(LockerScreen.LockerOpen);
			openLocker(timer.index);
			lockerOpened = true;
			toolLockerSolutions[timer.index] = "";
			Switch3D[] array = toolLockerNumberButtons;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].targetable = true;
			}
		}
		else if (timer.index == -1)
		{
			changeLockerKeypadScreen(LockerScreen.Error);
			game.startTimer(new LockerScreenTimer(-2), 2f);
		}
		else if (timer.index == -2)
		{
			changeLockerKeypadScreen(LockerScreen.Input);
			Switch3D[] array = toolLockerNumberButtons;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].targetable = true;
			}
		}
	}

	private void onFabricatorFixObjectTimerDone(FabricatorFixObjectTimer timer)
	{
		fabricatorRepairSoundInstance.stop(STOP_MODE.ALLOWFADEOUT);
		if (timer.index == -2)
		{
			fabricatorRepairSoundInstance.start();
			PineFmod.set3DAttributes(fabricatorRepairSoundInstance, PineFmod.to3DAttributes(fabricatorSlot.transform));
		}
		else if (timer.index == -1)
		{
			solveFabricator();
		}
		else if (timer.index == 0)
		{
			fabricatorPlane.SetActive(value: false);
			fabricatorPlaneProjections.SetActive(value: false);
			fabricatorChangeScreen(FabricatorScreens.RepairDone);
			brokenGem.Get<Item>(0).targetable = true;
			fabricatorSlot.targetable = true;
			Camera[] array = fabricatorCameras;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Render();
			}
		}
		else if (timer.index == 1)
		{
			game.startTimer(new FabricatorFixObjectTimer(2), 1.5f);
			fabricatorChangeScreen(FabricatorScreens.RepairWrong);
		}
		else if (timer.index == 2)
		{
			fabricatorChangeScreen(FabricatorScreens.Puzzle);
		}
	}

	private void onFabricatorStartScanTimerDone(FabricatorStartScanTimer timer)
	{
		if (timer.index == 0)
		{
			fabricatorChangeScreen(FabricatorScreens.NoObjectInScanner);
		}
		else if (timer.index == 1)
		{
			fabricatorChangeScreen(FabricatorScreens.DoesntNeedRepair);
			fabricatorSlot.targetable = true;
			fabricatorSlot.insertedItem.targetable = true;
		}
		else if (timer.index == 2)
		{
			fabricatorChangeScreen(FabricatorScreens.Puzzle);
			fabricatorPlaneProjections.SetActive(value: true);
			fabricatorPlane.SetActive(value: true);
			Camera[] array = fabricatorCameras;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Render();
			}
		}
	}

	private void onFabricatorScannedTimerDone(FabricatorScannedTimer timer)
	{
		fabricatorChangeScreen(FabricatorScreens.ScanButton);
	}

	private void onDelayedConnectorsInitTimerDone(DelayedConnectorsInitTimer timer)
	{
		for (int i = 1; i < connectorPuzzles.Count; i++)
		{
			connectorPuzzles[i].root.SetActive(value: false);
		}
		connectorPuzzles[0].checkSolution();
	}

	private void onDroneFlyoutTimerDone(DroneFlyoutTimer timer)
	{
		if (timer.index == -1)
		{
			startDroneFlyout();
		}
		else if (timer.index == 0)
		{
			float duration = 2.5f;
			droneFlyMaterials[timer.drone].transitionToDuration("Transparent", duration);
			game.startTimer(new DroneFlyoutTimer(1, timer.drone), duration);
		}
		else
		{
			if (timer.index != 1)
			{
				return;
			}
			dronesFlew++;
			if (dronesFlew >= 4)
			{
				for (int i = 0; i < droneButtons.Length; i++)
				{
					droneButtonDoors[i].transitionTo("Close", 2f, 0f);
					game.startSwitch(droneButtons[i]);
					droneButtons[i].targetable = true;
					drones[i].gameObject.SetActive(value: true);
				}
			}
		}
	}

	private void onUpdateDroneCamTimerDone(UpdateDroneCamTimer timer)
	{
		if (timer.index == 0)
		{
			float num = 2f;
			Ref<HDAdditionalReflectionData, Transform> obj = (game.isInAnyPlayerInventory(drones[timer.targetIndex].gameObject) ? droneInventoryReflectionProbes[droneCurrentCamIndex] : droneReflectionProbes[droneCurrentCamIndex]);
			droneCameraDisplay.sharedMaterial.SetTexture("_Tex", obj.Get<HDAdditionalReflectionData>(0).realtimeTexture);
			droneCameraDisabled.Get<MaterialState>(0f).transitionTo("Dissolve", num);
			game.startTimer(new UpdateDroneCamTimer(1, timer.targetIndex), 1f / num);
			droneDummyCamera.Get<Camera>(0f).enabled = false;
		}
		else if (timer.index == 1)
		{
			droneCameraDisabled.Get<GameObject>(0).SetActive(value: false);
		}
	}

	private void onDronesTimerDone(DronesTimer timer)
	{
		if (timer.index == 0)
		{
			if (!solvedDrones[timer.droneIndex])
			{
				solvedDrones[timer.droneIndex] = true;
				changeDroneDisplay(-1);
				droneButtonDoors[timer.droneIndex].transitionTo("Close", 2f);
				droneSlotsTween.transitionTo("Hide");
				game.startTimer(new DronesTimer(2, timer.droneIndex), 0.5f);
			}
		}
		else if (timer.index == 2)
		{
			droneDoorSeq.Get<Sequence>(0).play(-1f, 0f);
			game.startTimer(new OpenCompartmentSoundTimer(droneDoorSeq.Get<GameObject>(0f)), 0.01f);
			game.startTimer(new OpenCompartmentSoundTimer(droneDoorSeq.Get<GameObject>(0f)), 0.25f);
			game.startTimer(new OpenCompartmentSoundTimer(droneDoorSeq.Get<GameObject>(0f)), 0.5f);
			droneButtons[timer.droneIndex].gameObject.SetActive(value: false);
			if (droneButtons[timer.droneIndex].state == Switch3DState.Off)
			{
				game.startSwitch(droneButtons[timer.droneIndex]);
			}
			game.startTimer(new DronesTimer(3, timer.droneIndex), droneDoorSeq.Get<Sequence>(0).sequenceDuration);
		}
		else if (timer.index == 3)
		{
			Item insertedItem = droneSlot.insertedItem;
			game.removeItemFromSlot(droneSlot.insertedItem);
			insertedItem.gameObject.SetActive(value: false);
			droneSlotsTween.setState("Hide", 0f);
			for (int i = 0; i < droneButtons.Length; i++)
			{
				droneButtons[i].targetable = true;
			}
			game.startTimer(new DronesTimer(4, timer.droneIndex), 0.4f);
		}
		else if (timer.index == 4)
		{
			droneDoorSeq.Get<Sequence>(0).play();
			game.startTimer(new OpenCompartmentSoundTimer(droneDoorSeq.Get<GameObject>(0f)), 0.01f);
			game.startTimer(new OpenCompartmentSoundTimer(droneDoorSeq.Get<GameObject>(0f)), 0.25f);
			game.startTimer(new OpenCompartmentSoundTimer(droneDoorSeq.Get<GameObject>(0f)), 0.5f);
			bool solved = true;
			Array.ForEach(solvedDrones, delegate(bool x)
			{
				solved &= x;
			});
			if (solved)
			{
				solveDrones();
			}
			game.startTimer(new DronesTimer(5, timer.droneIndex), 0.4f);
		}
		else if (timer.index == 5)
		{
			droneSlot.targetable = true;
		}
		else if (timer.index == 6)
		{
			droneArtifactTween.transitionTo("Display");
			droneArtifact.targetable = true;
			game.finishPuzzle(Puzzle.Drones);
		}
	}

	private void onCargoChestConfirmTimerDone(CargoChestConfirmTimer timer)
	{
		if (timer.index == 0)
		{
			newChestConfirmButton.tweenState.transitionTo("Down", newChestConfirmButton.transitionSpeed, 0f);
			if (timer.solved)
			{
				game.callRPC(RPCs.CargoChestSolved);
			}
			else
			{
				game.callRPC(RPCs.CargoChestNotSolved);
			}
		}
		else if (timer.index == 1)
		{
			chestLockLightsOff();
			newChestConfirmButton.targetable = true;
		}
	}

	private void onSolveArtefactsTimerDone(SolveArtefactsTimer timer)
	{
		if (timer.index == 0)
		{
			solveArtefactSlots();
		}
		else if (timer.index == 1)
		{
			artefactSolvedAnimationSampler.play();
			game.startTimer(new SolveArtefactsTimer(2), 8f);
		}
		else if (timer.index == 2)
		{
			artefactSolvedParticles.SetActive(value: true);
			artefactSpinAnimations[0].SetActive(value: false);
			artefactSpinAnimations[1].SetActive(value: true);
			game.startTimer(new SolveArtefactsTimer(3), 2.5f);
		}
		else if (timer.index == 3)
		{
			game.levelCompleted();
			endPlane.Get<GameObject>(0).SetActive(value: true);
		}
	}

	private void onShieldCargoTimerDone(ShieldCargoTimer timer)
	{
		if (timer.index == 0)
		{
			shield.Get<GameObject>(0).SetActive(value: true);
			changeShieldScreen(ShieldScreenType.Puzzle);
			shieldControlsLid.transitionTo("Open");
			sphereScanningAnimation.SetActive(value: false);
			foreach (Ref<GameObject, Collider, MaterialState> shieldSphere in shieldSpheres)
			{
				((MaterialState)shieldSphere).transitionTo("Transparent", 1f, 0f);
			}
			foreach (ShieldTriangleSet shieldTriangleSet in shieldTriangleSets)
			{
				shieldTriangleSet.scaler.Get<GameObject>(0).SetActive(value: true);
			}
			onShieldScaleTrianglesSlidable(MoveEvent.Moved);
			game.startTimer(new ShieldCargoTimer(1), 0.5f);
		}
		else if (timer.index == 1)
		{
			shieldTriangleSets[0].activate(shieldTriangleScaleSlidable.Get<Slidable>(0).value);
		}
	}

	private void onShieldSolvedTimerDone(ShieldSolvedTimer timer)
	{
		if (timer.index == 0)
		{
			foreach (ShieldTriangleSet shieldTriangleSet in shieldTriangleSets)
			{
				shieldTriangleSet.scaler.Get<GameObject>(0).SetActive(value: false);
			}
			changeShieldScreen(ShieldScreenType.WeakPointsFound);
			game.startTimer(new ShieldSolvedTimer(1), 1.5f);
		}
		else if (timer.index == 1)
		{
			foreach (ShieldTriangleSet shieldTriangleSet2 in shieldTriangleSets)
			{
				shieldTriangleSet2.deactivate();
			}
			changeShieldScreen(ShieldScreenType.CutShieldButton);
		}
		else if (timer.index == 2)
		{
			shieldScreenZoom.Get<Zoomable>(0).targetable = false;
			changeShieldScreen(ShieldScreenType.Cutting);
			float speed = 0.1f;
			shieldCuttingUiTween.transitionTo("Load", speed);
			sphereCuttingAnimation.SetActive(value: true);
			shield.Get<MaterialState>(0f).transitionTo("Transparent", 0.125f);
			shieldDissolve.transitionTo("Dissolve", 0.125f);
			foreach (Ref<GameObject, Collider, MaterialState> shieldSphere in shieldSpheres)
			{
				((MaterialState)shieldSphere).transitionTo("Transparent", 0.125f);
			}
			shieldCuttingSound.SetActive(value: true);
			game.startTimer(new ShieldSolvedTimer(3), 8f);
		}
		else if (timer.index == 3)
		{
			shield.Get<GameObject>(0).SetActive(value: false);
			shieldCuttingSound.SetActive(value: false);
			changeShieldScreen(ShieldScreenType.CutComplete);
			game.increaseZoomCounter(shieldScreenZoom.Get<GameObject>(0f));
			game.startTimer(new ShieldSolvedTimer(4), 4f);
			sphereCuttingAnimation.SetActive(value: false);
			Game obj = game;
			Transform obj2 = sphere;
			Quaternion? rotation = Quaternion.Euler(-90f, 0f, 0f);
			obj.startTransitionLocal(obj2, 1f, 0f, null, rotation);
		}
		else if (timer.index == 4)
		{
			changeShieldScreen(ShieldScreenType.FinalReport);
		}
	}

	private void onSphereRotateTimerDone(SphereRotateTimer timer)
	{
		initingShield = false;
		isShieldChangingTriangle = false;
		shieldTriangleSets[timer.index].activate(shieldTriangleScales[timer.index]);
		toggleButtons(timer.index);
		shieldEnableControls();
		void toggleButtons(int index)
		{
			for (int i = 0; i < shieldUIAttempts.Length; i++)
			{
				shieldUIAttempts[i].Get<MaterialState>(0u).transitionTo("Disabled", 2f, 0f);
				Switch3D switch3D = shieldUIAttempts[i].Get<Switch3D>(0);
				if (switch3D == shieldUIAttempts[index].Get<Switch3D>(0))
				{
					if (switch3D.state == Switch3DState.Off)
					{
						game.startSwitch(switch3D);
					}
					switch3D.targetable = false;
				}
				else
				{
					if (switch3D.state == Switch3DState.On)
					{
						game.startSwitch(switch3D);
					}
					switch3D.targetable = true;
				}
			}
		}
	}

	private void onToolLockerTimerDone(ToolLockerTimer timer)
	{
		if (timer.index != -1 && timer.index != -2)
		{
			return;
		}
		int count = toolLockerCurrentValues.Count;
		for (int i = 0; i < toolLockerUnderlines.Length; i++)
		{
			if (i < count || i > count)
			{
				toolLockerUnderlines[i].setState("Invisible", 0f);
			}
			else
			{
				toolLockerUnderlines[i].transitionTo("Invisible", 7f, (timer.index == -1) ? 1f : 0f);
			}
		}
		game.startTimer(new ToolLockerTimer((timer.index == -1) ? (-2) : (-1)), 0.4f);
	}

	private void onWallHintTimerDone(WallHintTimer timer)
	{
		if (wallHintButtons[timer.index].Get<Switch3D>(0).state != Switch3DState.Off)
		{
			wallHintButtons[timer.index].Get<MaterialState>(0f).transitionTo("Down", 5f, timer.isGoingDown ? 0f : 1f);
		}
		game.startTimer(new WallHintTimer(timer.index, !timer.isGoingDown), 0.7f);
	}

	private void onElevatorKeycardTimerDone(ElevatorKeycardTimer timer)
	{
		if (timer.index == -2)
		{
			changeElevatorKeycardScreen(ElevatorKeycardScreen.Denied);
			game.startTimer(new ElevatorKeycardTimer(-1), 1f);
		}
		if (timer.index == -1)
		{
			changeElevatorKeycardScreen(ElevatorKeycardScreen.InsertKeycard);
		}
		if (timer.index == 0)
		{
			changeElevatorKeycardScreen(ElevatorKeycardScreen.Granted);
			elevatorKeypadTween.transitionTo("Rotate");
			game.startTimer(new ElevatorKeycardTimer(1), 0.8f);
		}
		else if (timer.index == 1)
		{
			cargoElevatorDoor.transitionTo("Open", 0.5f);
			game.finishPuzzle(Puzzle.ElevatorDoor);
		}
	}

	private void onWeldTargetedTimerDone(WeldTargetedTimer timer)
	{
		welderTargets[timer.weldIndex].Get<ToolTarget>(0).gameObject.SetActive(value: false);
	}

	private void onWelderTimerDone(WelderTimer timer)
	{
		if (timer.index == 0)
		{
			Vector3 localPosition = welderWavyLines[timer.line1].Get<Transform>(0).localPosition;
			welderWavyLines[timer.line1].Get<Transform>(0).localPosition = welderWavyLines[timer.line2].Get<Transform>(0).localPosition;
			welderWavyLines[timer.line2].Get<Transform>(0).localPosition = localPosition;
			if (checkWelderSolution())
			{
				solveWelderGraph();
			}
		}
		else if (timer.index == 1)
		{
			foreach (Ref<Transform, MaterialState> welderWavyLine in welderWavyLines)
			{
				((MaterialState)welderWavyLine).transitionToDuration("Glow", 0.4f);
			}
			game.startTimer(new WelderTimer(2, -1, -1), 1.2f);
		}
		else if (timer.index == 2)
		{
			welderTween.transitionTo("ZoomOut", 0.4f);
			changeWelderScreen(WelderScreenType.LoadingBar);
			game.startTimer(new WelderTimer(3, -1, -1), 3f);
		}
		else if (timer.index == 3)
		{
			changeWelderScreen(WelderScreenType.Text);
			WelderStartParticles.SetActive(value: true);
			game.finishPuzzle(Puzzle.Welder);
		}
	}

	private void onSphereCoverTimerDone(SphereCoverTimer timer)
	{
		sphereCovers[timer.coverIndex].Get<GameObject>(0).SetActive(value: false);
		if (timer.coverIndex != 0)
		{
			return;
		}
		foreach (Ref<Slot, Collider> gemSlot in gemSlots)
		{
			((Slot)gemSlot).targetable = true;
		}
	}
}
