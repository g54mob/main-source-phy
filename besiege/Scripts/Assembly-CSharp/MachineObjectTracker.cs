using Modding;
using UnityEngine;

[AddComponentMenu("Core/Machine Object Tracker")]
public class MachineObjectTracker : SingleInstance<MachineObjectTracker>
{
	public static bool rebuilding;

	public static bool building;

	public static MachineInfo lastBuild;

	public static ActiveMachineChanged ActiveMachineChanged;

	public AddPiece AddPieceCode;

	public static Machine activeMachine;

	public override string Name
	{
		get
		{
			return "MACHINE TRACKER";
		}
	}

	public Transform BuildingMachine
	{
		get
		{
			return (!(activeMachine != null)) ? null : activeMachine.BuildingMachine;
		}
	}

	protected virtual void Start()
	{
		CreateOrLoadMachine();
		rebuilding = false;
		building = false;
	}

	public static T CreateMachine<T>(string machineName) where T : Machine
	{
		return new GameObject(machineName, typeof(BlockLinkManager), typeof(MachineAnalyzer), typeof(UndoSystem), typeof(T)).GetComponent<T>();
	}

	public void CreateOrLoadMachine()
	{
		if (lastBuild != null)
		{
			if (activeMachine == null)
			{
				activeMachine = CreateMachine<Machine>(string.Empty);
			}
			activeMachine.LoadMachineInfo(lastBuild, activeMachine.LoadedMachinePath);
			SetActiveMachine(activeMachine);
		}
		else
		{
			CreateNewMachine();
		}
	}

	private BlockInfo CreateStartCubeBlock()
	{
		BlockInfo blockInfo = new BlockInfo();
		blockInfo.ID = BlockType.StartingBlock;
		blockInfo.Position = Vector3.zero;
		blockInfo.Rotation = Quaternion.identity;
		blockInfo.Scale = PrefabMaster.GetDefaultScale(BlockType.StartingBlock);
		blockInfo.Flipped = false;
		blockInfo.Skin = PrefabMaster.BlockPrefabs[0].DefaultSkin;
		blockInfo.BlockData = new XDataHolder();
		return blockInfo;
	}

	private MachineInfo CreateEmptyMachineInfo()
	{
		MachineInfo machineInfo = new MachineInfo();
		machineInfo.Name = "Machine";
		machineInfo.Position = Vector3.up * 5.05f;
		if (!StatMaster.isMP && WaterController.Exist && machineInfo.Position.y < WaterController.waterTransformHeight)
		{
			machineInfo.Position = Vector3.up * 3.0500002f;
		}
		machineInfo.Rotation = Quaternion.identity;
		BlockInfo item = CreateStartCubeBlock();
		machineInfo.Blocks.Add(item);
		return machineInfo;
	}

	public Machine CreateNewMachine()
	{
		MachineInfo machineInfo = CreateEmptyMachineInfo();
		if (activeMachine != null)
		{
			ReplaceMachineUndoAction action = new ReplaceMachineUndoAction(activeMachine, machineInfo);
			activeMachine.UndoSystem.AddAction(action);
		}
		else
		{
			activeMachine = CreateMachine<Machine>("Machine");
		}
		activeMachine.LoadMachineInfo(machineInfo);
		activeMachine.SetMachineCenter(activeMachine.BuildingBlocks[0].transform.position);
		SetActiveMachine(activeMachine);
		if (SingleInstanceFindOnly<AddPiece>.Instance != null)
		{
			SingleInstanceFindOnly<AddPiece>.Instance.UpdateMiddleOfObject();
		}
		else
		{
			Debug.LogWarning("Could not UpdateMiddleOfObject as AddPiece instance is null");
		}
		SingleInstance<Events>.Instance.MachineDestroyed();
		return activeMachine;
	}

	public void SetActiveMachine(Machine machine)
	{
		activeMachine = machine;
		if (ReferenceMaster.onMachineChanged != null)
		{
			ReferenceMaster.onMachineChanged(machine);
		}
	}
}
