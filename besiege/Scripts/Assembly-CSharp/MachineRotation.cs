using Selectors;
using UnityEngine;

[AddComponentMenu("UI/Tools/Machine Rotation")]
public class MachineRotation : MachineTransformTool
{
	public static MachineRotation Instance;

	public float speed = 1f;

	public bool clicked;

	public Renderer bgRend;

	public Material clickedMaterial;

	public Transform machineMiddle;

	public TextMesh degreesTextMesh;

	public float lerpSpeed = 0.15f;

	public float startPos;

	public Transform boundingBox;

	public MachineToolController toolControllerCode;

	public ValueHolder snappingValue;

	public GameObject advancedModule;

	public UIButtonExtended globalButton;

	public UIButtonExtended pivotButton;

	public UIButtonExtended linkedButton;

	private float endPos;

	private AudioSource audioSource;

	private Quaternion startRotForUndo;

	private Vector3 startPosForUndo;

	private MachineObjectTracker machineTracker;

	private Renderer degreesRenderer;

	private Transform buildingMachine;

	private Quaternion oldRot;

	private bool gizmoRotate
	{
		get
		{
			return true;
		}
	}

	private void Start()
	{
		machineTracker = SingleInstance<MachineObjectTracker>.Instance;
		SetTextDegrees();
		audioSource = GetComponent<AudioSource>();
		degreesRenderer = degreesTextMesh.GetComponent<Renderer>();
	}

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
			snappingValue.SetValue(StatMaster.Mode.Transform.Snap.rotation);
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
			if (advancedModule != null)
			{
				advancedModule.SetActive(true);
			}
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
		StatMaster.Mode.Transform.Snap.rotation = val;
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

	public override void OnDisable()
	{
		base.OnDisable();
		StopTool();
	}

	public void Reset()
	{
		clicked = false;
		bgRend.gameObject.SetActive(false);
		if (StatMaster.Mode.selectedTool == StatMaster.Tool.Rotate)
		{
			RotateOff();
		}
	}

	public override void OnClicked()
	{
		if (!base.enabled)
		{
			clicked = false;
			bgRend.gameObject.SetActive(false);
			return;
		}
		if (gizmoRotate)
		{
			clicked = false;
			Machine machine = Machine.Active();
			if ((bool)machine && machine.CanModify)
			{
				if (StatMaster.Mode.selectedTool == StatMaster.Tool.Rotate)
				{
					RotateOff();
					return;
				}
				RotateOn();
				Toggled();
				ReferenceMaster.ResetLevelEditor();
			}
			return;
		}
		toolControllerCode.DisableAll();
		startMachine = Machine.Active();
		if ((bool)startMachine && !startMachine.isSimulating && startMachine.CanModify)
		{
			StatMaster.StopHotKeys(true);
			clicked = true;
			degreesRenderer.enabled = true;
			SetTextDegrees();
			bgRend.gameObject.SetActive(true);
			audioSource.Play();
			StatMaster.Mode.isTranslating = true;
			StatMaster.Mode.isRotating = true;
			buildingMachine = machineTracker.BuildingMachine;
			startRotForUndo = buildingMachine.rotation;
			startPosForUndo = buildingMachine.position;
			startMachine.SetRigidInterpolation(RigidbodyInterpolation.None);
			currentInterval = 0f;
			hasNetworkedTransform = false;
		}
	}

	public override void OnClickReleased()
	{
		if (clicked)
		{
			StatMaster.StopHotKeys(false);
			SetTextDegrees();
			bgRend.gameObject.SetActive(false);
			audioSource.Play();
			degreesRenderer.enabled = false;
			StopTool();
		}
	}

	public void RotateOn()
	{
		bgRend.gameObject.SetActive(true);
		StatMaster.Mode.selectedTool = StatMaster.Tool.Rotate;
		if (!ReferenceMaster.ToolsEnabled.Contains(this))
		{
			ReferenceMaster.ToolsEnabled.Add(this);
		}
		toolControllerCode.EnableRotate();
	}

	public void RotateOff()
	{
		OffExternal();
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

	public void ResetRotation()
	{
		startMachine = Machine.Active();
		if ((bool)startMachine && !startMachine.isSimulating && startMachine.CanModify)
		{
			startMachine.SetRigidInterpolation(RigidbodyInterpolation.None);
			buildingMachine = machineTracker.BuildingMachine;
			startRotForUndo = buildingMachine.rotation;
			startPosForUndo = buildingMachine.position;
			Vector3 vector = buildingMachine.position - SingleInstanceFindOnly<AddPiece>.Instance.middleOfObject.position;
			Quaternion quaternion = Quaternion.Inverse(buildingMachine.rotation);
			if (StatMaster.isMP)
			{
				ServerMachine serverMachine = startMachine as ServerMachine;
				Transform transform = serverMachine.player.buildZone.transform;
				quaternion = transform.rotation * Quaternion.Inverse(buildingMachine.rotation);
				buildingMachine.rotation = transform.rotation;
			}
			else
			{
				buildingMachine.rotation = Quaternion.identity;
			}
			buildingMachine.position = SingleInstanceFindOnly<AddPiece>.Instance.middleOfObject.position + quaternion * vector;
			SetTextDegrees();
			Vector3 position = startMachine.Position;
			Quaternion rotation = startMachine.Rotation;
			if (position != startPosForUndo || rotation != startRotForUndo)
			{
				SendTransformInfo(startMachine);
				startMachine.RestoreRigidInterpolation();
				ApplyMachineRotation(startMachine, startPosForUndo, position, startRotForUndo, rotation);
			}
		}
	}

	public static void ApplyMachineRotation(Machine machine, Vector3 oldPos, Vector3 newPos, Quaternion oldRot, Quaternion newRot)
	{
		if (newPos != oldPos)
		{
			if (newRot != oldRot)
			{
				machine.SetTransform(newPos, newRot);
				machine.UndoSystem.ChangeTransform(oldPos, oldRot);
			}
			else
			{
				machine.SetPosition(newPos);
				machine.UndoSystem.ChangePosition(oldPos);
			}
		}
		else
		{
			if (!(newRot != oldRot))
			{
				return;
			}
			machine.SetRotation(newRot);
			machine.UndoSystem.ChangeRotation(oldRot);
		}
		SingleInstanceFindOnly<AddPiece>.Instance.UpdateMiddleOfObject();
		AdvancedBlockEditor.Instance.UpdateTool();
	}

	protected override void LateUpdate()
	{
		base.LateUpdate();
		Machine machine = Machine.Active();
		if (!clicked || !machine || machine.isSimulating)
		{
			return;
		}
		float num = InputManager.MouseX() * speed;
		if (num != 0f)
		{
			Vector3 up = Vector3.up;
			if (StatMaster.isMP)
			{
				ServerMachine serverMachine = machine as ServerMachine;
				up = serverMachine.player.buildZone.transform.up;
			}
			buildingMachine.RotateAround(machine.MiddlePosition, up, num);
			machine.CheckBounds();
			SetTextDegrees();
			UpdateTransformInfo(machine);
		}
	}

	private void StopTool()
	{
		StatMaster.Mode.isTranslating = false;
		StatMaster.Mode.isRotating = false;
		if (clicked)
		{
			clicked = false;
			if (!(startMachine == null))
			{
				startMachine.RestoreRigidInterpolation();
				SendTransformInfo(startMachine);
				ApplyMachineRotation(startMachine, startPosForUndo, startMachine.Position, startRotForUndo, startMachine.Rotation);
			}
		}
	}

	private void SetTextDegrees()
	{
		string text;
		if (buildingMachine != null)
		{
			Quaternion rotation = buildingMachine.rotation;
			if (StatMaster.isMP)
			{
				ServerMachine serverMachine = Machine.Active() as ServerMachine;
				Quaternion rotation2 = serverMachine.player.buildZone.transform.rotation;
				rotation *= Quaternion.Inverse(rotation2);
			}
			text = Mathf.RoundToInt(rotation.eulerAngles.y).ToString();
		}
		else
		{
			text = "0";
		}
		if (!text.Equals(degreesTextMesh.text))
		{
			degreesTextMesh.text = text;
		}
	}
}
