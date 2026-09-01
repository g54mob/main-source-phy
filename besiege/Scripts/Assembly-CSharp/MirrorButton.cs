using UnityEngine;

[AddComponentMenu("UI/Tools/Mirror Button")]
public class MirrorButton : ClickBehaviour
{
	public static MirrorButton Instance;

	public Transform mirrorTool;

	public Renderer bgRend;

	public Material clickedMaterial;

	public MachineToolController toolControllerCode;

	public GameObject advancedModule;

	public UIButtonExtended globalButton;

	public UIButtonExtended pivotButton;

	private Material startMaterial;

	private void Awake()
	{
		Instance = this;
		if (globalButton != null)
		{
			globalButton.Down += ToggleGlobal;
		}
		if (pivotButton != null)
		{
			pivotButton.Down += TogglePivot;
		}
		ToggleAdvanced(StatMaster.advancedBuilding);
	}

	public void ToggleAdvanced(bool toggle)
	{
		if (advancedModule != null)
		{
			advancedModule.SetActive(toggle);
		}
		if (toggle)
		{
			Toggled();
		}
	}

	private void Toggled()
	{
		if (globalButton != null)
		{
			globalButton.BG.SetActive(StatMaster.Mode.Transform.global);
		}
		if (pivotButton != null)
		{
			pivotButton.BG.SetActive(StatMaster.Mode.Transform.pivot);
		}
	}

	public void SetSnap(float val)
	{
		StatMaster.Mode.Transform.Snap.position = val;
		StatMaster.Mode.Transform.Snap.InvokeOnChanged();
	}

	public void ToggleGlobal()
	{
		StatMaster.Mode.Transform.global = !StatMaster.Mode.Transform.global;
		AdvancedBlockEditor.ChangedGlobalToggle(StatMaster.Mode.Transform.global);
		globalButton.BG.SetActive(StatMaster.Mode.Transform.global);
	}

	public void TogglePivot()
	{
		StatMaster.Mode.Transform.pivot = !StatMaster.Mode.Transform.pivot;
		AdvancedBlockEditor.ChangedPivotToggle(StatMaster.Mode.Transform.pivot);
		pivotButton.BG.SetActive(StatMaster.Mode.Transform.pivot);
	}

	public override void OnClicked()
	{
		if (!base.enabled)
		{
			bgRend.gameObject.SetActive(false);
			return;
		}
		Machine machine = Machine.Active();
		if ((bool)machine && machine.CanModify)
		{
			if (StatMaster.Mode.selectedTool == StatMaster.Tool.Mirror)
			{
				MirrorOff();
				return;
			}
			MirrorOn();
			Toggled();
			ReferenceMaster.ResetLevelEditor();
		}
	}

	public void MirrorOn()
	{
		bgRend.gameObject.SetActive(true);
		StatMaster.Mode.selectedTool = StatMaster.Tool.Mirror;
		if (!ReferenceMaster.ToolsEnabled.Contains(this))
		{
			ReferenceMaster.ToolsEnabled.Add(this);
		}
		toolControllerCode.EnableMirror();
	}

	public void MirrorOff()
	{
		OffExternal();
		StatMaster.Mode.selectedTool = StatMaster.Tool.None;
		toolControllerCode.DisableAll();
	}

	public void OffExternal()
	{
		bgRend.gameObject.SetActive(false);
		if (ReferenceMaster.ToolsEnabled.Contains(this))
		{
			ReferenceMaster.ToolsEnabled.Remove(this);
		}
		mirrorTool.gameObject.SetActive(false);
	}
}
