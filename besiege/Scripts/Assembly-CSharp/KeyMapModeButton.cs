using UnityEngine;

[AddComponentMenu("UI/Tools/Key Map Mode Button")]
public class KeyMapModeButton : ClickBehaviour
{
	public static KeyMapModeButton Instance;

	public AddPiece addPieceCode;

	public Renderer bgRend;

	public Material clickedMaterial;

	public MachineToolController toolControllerCode;

	private Material startMaterial;

	private void Start()
	{
		Instance = this;
		addPieceCode = SingleInstanceFindOnly<AddPiece>.Instance;
	}

	public override void OnClicked()
	{
		Machine machine = Machine.Active();
		if ((bool)machine && !machine.isSimulating && machine.CanModify)
		{
			if (StatMaster.Mode.selectedTool == StatMaster.Tool.Modify)
			{
				KeyMapOff();
				return;
			}
			KeyMapOn();
			ReferenceMaster.ResetLevelEditor();
		}
	}

	public void KeyMapOn()
	{
		bgRend.enabled = true;
		StatMaster.Mode.selectedTool = StatMaster.Tool.Modify;
		if (!ReferenceMaster.ToolsEnabled.Contains(this))
		{
			ReferenceMaster.ToolsEnabled.Add(this);
		}
		AdvancedBlockEditor.Instance.CheckShowBlockMapper();
		toolControllerCode.EnableKeyMap();
	}

	private void KeyMapOff()
	{
		OffExternal();
		StatMaster.Mode.selectedTool = StatMaster.Tool.None;
		toolControllerCode.DisableAll();
	}

	public void OffExternal()
	{
		bgRend.enabled = false;
		if (ReferenceMaster.ToolsEnabled.Contains(this))
		{
			ReferenceMaster.ToolsEnabled.Remove(this);
		}
	}
}
