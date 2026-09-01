using UnityEngine;

public class MachineInfoHUDTester : MonoBehaviour
{
	private MachineInfoHUD machineInfoHud;

	private void Start()
	{
		machineInfoHud = Object.FindObjectOfType<MachineInfoHUD>();
		if (!(machineInfoHud == null))
		{
			FillDummyData();
		}
	}

	private void FillDummyData()
	{
		if (LevelEditor.Instance != null)
		{
			if (LevelEditor.Instance.Settings == null)
			{
				LevelEditor.Instance.Settings = new LevelSettings();
			}
		}
		else
		{
			Debug.LogWarning("LevelEditor instance could not be found, can't get the limits");
		}
		ServerMachine[] array = Object.FindObjectsOfType<ServerMachine>();
		ServerMachine serverMachine;
		if (array.Length == 0)
		{
			new GameObject().AddComponent<LocalMachineObjectTracker>();
			serverMachine = new GameObject().AddComponent<ServerMachine>();
			serverMachine.Name = "Omeganator";
			serverMachine.GetBlocks(0u);
			FillBlockTypeDict(serverMachine);
		}
		else
		{
			serverMachine = (ServerMachine)Machine.Active();
		}
		machineInfoHud.KeepOldItems = false;
		machineInfoHud.Setup(serverMachine);
	}

	private void FillBlockTypeDict(ServerMachine serverMachine)
	{
		foreach (int key in PrefabMaster.BlockPrefabs.Keys)
		{
			BlockType blockType = (BlockType)key;
			if (blockType != BlockType.StartingBlock && blockType != BlockType.Unused && blockType != BlockType.Unused3 && blockType != BlockType.CameraBlock && blockType != BlockType.Magnet)
			{
				BlockPrefab blockPrefab = PrefabMaster.BlockPrefabs[key];
				bool flag = Random.Range(0f, 1f) >= 0.5f;
				LevelEditor.Instance.Settings.SetBlockLimit((BlockType)blockPrefab.ID, (!flag) ? Random.Range(-1, 200) : (-1));
				serverMachine.BlockTypeCount.Add(blockPrefab.ID, Random.Range(0, 222));
			}
		}
	}
}
