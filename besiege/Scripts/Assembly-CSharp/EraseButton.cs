using UnityEngine;

[AddComponentMenu("UI/Tools/Erase Button")]
public class EraseButton : ClickBehaviour
{
	public static EraseButton Instance;

	public Renderer bgRend;

	public Material clickedMaterial;

	public MachineToolController toolControllerCode;

	private Material startMaterial;

	protected void Awake()
	{
		Instance = this;
	}

	public override void OnClicked()
	{
		Machine machine = Machine.Active();
		if ((bool)machine && !machine.isSimulating && machine.CanModify)
		{
			if (StatMaster.Mode.selectedTool == StatMaster.Tool.Erase)
			{
				EraserOff();
				return;
			}
			EraserOn();
			ReferenceMaster.ResetLevelEditor();
		}
	}

	public void EraserOn()
	{
		bgRend.enabled = true;
		if (StatMaster.advancedBuilding && StatMaster.Mode.selectedTool != StatMaster.Tool.Erase)
		{
			AdvancedBlockEditor instance = AdvancedBlockEditor.Instance;
			if (instance.selectionController.Count > 0)
			{
				instance.selectionController.DeselectAll(true);
			}
		}
		StatMaster.Mode.selectedTool = StatMaster.Tool.Erase;
		if (!ReferenceMaster.ToolsEnabled.Contains(this))
		{
			ReferenceMaster.ToolsEnabled.Add(this);
		}
		toolControllerCode.EnableErase();
	}

	public void EraserOff()
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
