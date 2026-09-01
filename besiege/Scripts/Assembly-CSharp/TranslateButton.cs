using Selectors;
using UnityEngine;

[AddComponentMenu("UI/Translate Button")]
public class TranslateButton : ClickBehaviour
{
	public static TranslateButton Instance;

	public Renderer bgRend;

	public Material clickedMaterial;

	public MachineToolController toolControllerCode;

	public ValueHolder snappingValue;

	public GameObject advancedModule;

	public UIButtonExtended globalButton;

	public UIButtonExtended pivotButton;

	public UIButtonExtended linkedButton;

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
		if (linkedButton != null)
		{
			linkedButton.Down += ToggleLinked;
		}
		if (snappingValue != null)
		{
			snappingValue.SetValue(StatMaster.Mode.Transform.Snap.position);
			snappingValue.ValueChanged += SetSnap;
		}
		ToggleAdvanced(StatMaster.advancedBuilding);
	}

	public void ToggleAdvanced(bool toggle)
	{
		bool flag = snappingValue != null;
		if (advancedModule != null)
		{
			advancedModule.SetActive(toggle && flag);
		}
		if (toggle && flag)
		{
			SetSnap(snappingValue.GetValue());
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
		if (linkedButton != null)
		{
			linkedButton.BG.SetActive(StatMaster.Mode.Transform.linked);
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

	public void ToggleLinked()
	{
		StatMaster.Mode.Transform.linked = !StatMaster.Mode.Transform.linked;
		AdvancedBlockEditor.ChangedLinkToggle(StatMaster.Mode.Transform.linked);
		linkedButton.BG.SetActive(StatMaster.Mode.Transform.linked);
	}

	public override void OnClicked()
	{
		if (!base.enabled)
		{
			bgRend.gameObject.SetActive(false);
			return;
		}
		Machine machine = Machine.Active();
		if ((bool)machine && !machine.isSimulating && machine.CanModify)
		{
			if (StatMaster.Mode.selectedTool == StatMaster.Tool.Translate)
			{
				TranslateOff();
				return;
			}
			TranslateOn();
			Toggled();
			ReferenceMaster.ResetLevelEditor();
		}
	}

	public void TranslateOn()
	{
		bgRend.gameObject.SetActive(true);
		StatMaster.Mode.selectedTool = StatMaster.Tool.Translate;
		if (!ReferenceMaster.ToolsEnabled.Contains(this))
		{
			ReferenceMaster.ToolsEnabled.Add(this);
		}
		toolControllerCode.EnableTranslate();
	}

	public void TranslateOff()
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
	}
}
