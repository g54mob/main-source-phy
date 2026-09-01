using UnityEngine;

[AddComponentMenu("UI/Tools/Paint Button")]
public class PaintButton : ClickBehaviour
{
	public static PaintButton Instance;

	public GameObject BG;

	public GameObject ActiveBG;

	public MachineToolController toolControllerCode;

	public GameObject advancedModule;

	private Material startMaterial;

	private void Awake()
	{
		Instance = this;
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
	}

	public override void OnClicked()
	{
		if (!base.enabled)
		{
			BG.SetActive(false);
			return;
		}
		Machine machine = Machine.Active();
		if ((bool)machine && machine.CanModify)
		{
			if ((!SelectionPaintMode()) ? (StatMaster.Mode.selectedTool == StatMaster.Tool.Paint) : BG.activeSelf)
			{
				PaintToolOff();
				return;
			}
			PaintToolOn();
			Toggled();
			ReferenceMaster.ResetLevelEditor();
		}
	}

	public void PaintToolOn()
	{
		BG.SetActive(true);
		SetActiveBG();
		if (!SelectionPaintMode())
		{
			StatMaster.Mode.selectedTool = StatMaster.Tool.Paint;
			if (!ReferenceMaster.ToolsEnabled.Contains(this))
			{
				ReferenceMaster.ToolsEnabled.Add(this);
			}
			toolControllerCode.EnablePaint();
		}
	}

	public void PaintToolOff()
	{
		OffExternal();
		if (!SelectionPaintMode())
		{
			StatMaster.Mode.selectedTool = StatMaster.Tool.None;
			toolControllerCode.DisableAll();
		}
	}

	public void OffExternal()
	{
		BG.SetActive(false);
		SetActiveBG();
		if (ReferenceMaster.ToolsEnabled.Contains(this))
		{
			ReferenceMaster.ToolsEnabled.Remove(this);
		}
	}

	public void SetActiveBG()
	{
		ActiveBG.SetActive(!SelectionPaintMode());
	}

	public bool SelectionPaintMode()
	{
		bool flag = false;
		switch (StatMaster.Mode.selectedTool)
		{
		case StatMaster.Tool.Translate:
		case StatMaster.Tool.Rotate:
		case StatMaster.Tool.Scale:
		case StatMaster.Tool.Mirror:
		case StatMaster.Tool.Modify:
			flag = true;
			break;
		}
		return flag && AdvancedBlockEditor.Instance.selectionController.Count > 0;
	}
}
