using UnityEngine;

[AddComponentMenu("UI/Tools/Symmetry Button")]
public class SymmetryButton : ClickBehaviour
{
	public static SymmetryButton Instance;

	public UIButton x;

	public UIButton y;

	public UIButton z;

	public UIButton setPivot;

	public GameObject bg;

	public Transform dropper;

	public float startY;

	public float endY;

	public bool activey;

	public Renderer bgX;

	public Renderer bgY;

	public Renderer bgZ;

	public GameObject bgPivot;

	public Material highlightMaterial;

	public Material inactiveMaterial;

	public MachineToolController toolController;

	public bool useDisableButton = true;

	public UIButton disableButton;

	public GameObject advancedModule;

	public GameObject simpleModule;

	public UIButtonExtended placementButton;

	public UIButtonExtended selectButton;

	public UIButtonExtended eraserButton;

	public UIButtonExtended modifyButton;

	protected StatMaster.Tool prevMode;

	protected bool prevDisableBlockPlacement;

	protected SymmetryController sc;

	protected bool advanced = true;

	private void Awake()
	{
		Instance = this;
		if (!useDisableButton)
		{
			disableButton.gameObject.SetActive(false);
		}
		else
		{
			disableButton.Click += TurnOffAllAxes;
		}
		x.Click += ToggleX;
		y.Click += ToggleY;
		z.Click += ToggleZ;
		setPivot.Click += ToggleSetPivotMode;
		if (placementButton != null)
		{
			placementButton.Down += TogglePlacement;
		}
		if (selectButton != null)
		{
			selectButton.Down += ToggleSelection;
		}
		if (eraserButton != null)
		{
			eraserButton.Down += ToggleEraser;
		}
		if (modifyButton != null)
		{
			modifyButton.Down += ToggleModify;
		}
		ToggleAdvanced(StatMaster.advancedBuilding);
		ReturnHidden();
	}

	public void ToggleAdvanced(bool toggle)
	{
		if (advancedModule != null)
		{
			advancedModule.SetActive(toggle && advanced);
			if (simpleModule != null)
			{
				simpleModule.SetActive(!advancedModule.activeSelf);
			}
		}
		if (toggle && advanced)
		{
			Toggled();
		}
	}

	private void Toggled()
	{
		if (placementButton != null)
		{
			placementButton.BG.SetActive(StatMaster.Mode.Symmetry.placement);
		}
		if (selectButton != null)
		{
			selectButton.BG.SetActive(StatMaster.Mode.Symmetry.selection);
		}
		if (eraserButton != null)
		{
			eraserButton.BG.SetActive(StatMaster.Mode.Symmetry.eraser);
		}
		if (modifyButton != null)
		{
			modifyButton.BG.SetActive(StatMaster.Mode.Symmetry.modifying);
		}
	}

	public void TogglePlacement()
	{
		StatMaster.Mode.Symmetry.placement = !StatMaster.Mode.Symmetry.placement;
		placementButton.BG.SetActive(StatMaster.Mode.Symmetry.placement);
		ReferenceMaster.ResetLevelEditor();
		if (sc.OnAxisChanged != null)
		{
			sc.OnAxisChanged();
		}
	}

	public void ToggleSelection()
	{
		StatMaster.Mode.Symmetry.selection = !StatMaster.Mode.Symmetry.selection;
		selectButton.BG.SetActive(StatMaster.Mode.Symmetry.selection);
		ReferenceMaster.ResetLevelEditor();
		if (sc.OnAxisChanged != null)
		{
			sc.OnAxisChanged();
		}
	}

	public void ToggleEraser()
	{
		StatMaster.Mode.Symmetry.eraser = !StatMaster.Mode.Symmetry.eraser;
		eraserButton.BG.SetActive(StatMaster.Mode.Symmetry.eraser);
	}

	public void ToggleModify()
	{
		StatMaster.Mode.Symmetry.modifying = !StatMaster.Mode.Symmetry.modifying;
		modifyButton.BG.SetActive(StatMaster.Mode.Symmetry.modifying);
	}

	private void Start()
	{
		sc = SingleInstanceFindOnly<AddPiece>.Instance.symmetryController;
		if (sc == null)
		{
			base.enabled = false;
		}
	}

	protected void Update()
	{
		Machine machine = Machine.Active();
		if (!machine || machine.isSimulating)
		{
			if (activey)
			{
				ReturnHidden();
			}
			return;
		}
		if (prevMode != StatMaster.Mode.selectedTool || prevDisableBlockPlacement != AddPiece.disableBlockPlacement)
		{
			UpdateBG();
		}
		if (bgPivot.activeSelf != StatMaster.Mode.selectSymmetryPivot)
		{
			bgPivot.SetActive(StatMaster.Mode.selectSymmetryPivot);
		}
		prevMode = StatMaster.Mode.selectedTool;
		prevDisableBlockPlacement = AddPiece.disableBlockPlacement;
	}

	public override void OnClicked()
	{
		Machine machine = Machine.Active();
		if ((bool)machine && !machine.isSimulating && machine.CanModify)
		{
			if (!activey)
			{
				DropDown();
				ReferenceMaster.ResetLevelEditor();
			}
			else
			{
				ReturnHidden();
			}
		}
	}

	public void CloseAll()
	{
		ReturnHidden();
	}

	public void DropDown()
	{
		activey = true;
		dropper.localPosition = new Vector3(dropper.localPosition.x, endY, dropper.localPosition.z);
	}

	protected void ReturnHidden()
	{
		activey = false;
		if (StatMaster.Mode.selectSymmetryPivot)
		{
			ToggleSetPivotMode();
		}
		dropper.localPosition = new Vector3(dropper.localPosition.x, startY, dropper.localPosition.z);
	}

	public void TurnOffAllAxes()
	{
		if (!(sc == null))
		{
			bgX.gameObject.SetActive(false);
			bgY.gameObject.SetActive(false);
			bgZ.gameObject.SetActive(false);
			sc.axis = Vector3.zero;
			UpdateBG();
			ReturnHidden();
		}
	}

	public void ToggleX()
	{
		Toggle(bgX.gameObject, 0);
	}

	public void ToggleY()
	{
		Toggle(bgY.gameObject, 1);
	}

	public void ToggleZ()
	{
		Toggle(bgZ.gameObject, 2);
	}

	public void Toggle(GameObject g, int axis)
	{
		if (!(sc == null))
		{
			bool flag = sc.axis[axis] == 0f;
			sc.axis[axis] = (flag ? 1 : 0);
			if (g.activeSelf != flag)
			{
				g.SetActive(flag);
			}
			UpdateBG();
			ReferenceMaster.ResetLevelEditor();
			sc.UpdatePivotPosition();
			sc.InvokeAxisChange();
		}
	}

	private void ToggleSetPivotMode()
	{
		StatMaster.Mode.selectSymmetryPivot = !StatMaster.Mode.selectSymmetryPivot;
		if (bgPivot.activeSelf == StatMaster.Mode.selectSymmetryPivot)
		{
			return;
		}
		bgPivot.SetActive(StatMaster.Mode.selectSymmetryPivot);
		if (StatMaster.Mode.selectSymmetryPivot)
		{
			if (!ReferenceMaster.ToolsEnabled.Contains(this))
			{
				ReferenceMaster.ToolsEnabled.Add(this);
			}
			toolController.EnableSymmetryPivot();
		}
		else
		{
			if (ReferenceMaster.ToolsEnabled.Contains(this))
			{
				ReferenceMaster.ToolsEnabled.Remove(this);
			}
			toolController.DisableSymmetryPivot();
		}
	}

	public void OffExternal()
	{
		StatMaster.Mode.selectSymmetryPivot = false;
		if (bgPivot.activeSelf != StatMaster.Mode.selectSymmetryPivot)
		{
			bgPivot.SetActive(StatMaster.Mode.selectSymmetryPivot);
		}
		if (ReferenceMaster.ToolsEnabled.Contains(this))
		{
			ReferenceMaster.ToolsEnabled.Remove(this);
		}
	}

	private void UpdateBG()
	{
		bool flag = !AddPiece.disableBlockPlacement && sc != null && (sc.axis[0] != 0f || sc.axis[1] != 0f || sc.axis[2] != 0f);
		bgX.material = ((!flag) ? inactiveMaterial : highlightMaterial);
		bgY.material = ((!flag) ? inactiveMaterial : highlightMaterial);
		bgZ.material = ((!flag) ? inactiveMaterial : highlightMaterial);
		bg.SetActive(flag);
	}
}
