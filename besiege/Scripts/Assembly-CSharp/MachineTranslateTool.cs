using System.Collections;
using UnityEngine;

[AddComponentMenu("UI/Tools/Machine Translate Tool")]
public class MachineTranslateTool : MachineTransformTool
{
	public Transform arrowParent;

	public static MachineTranslateTool Instance;

	public float zDepth = 10f;

	public Plane groundPlane;

	public Material selectedMaterial;

	public Renderer[] myRenderers;

	public Axes axis;

	public bool useAlternateTranslateMode = true;

	private Vector3 posToBe;

	private Material startMat;

	private Vector3 startMaxPosition;

	private Vector3 startMinPosition;

	private Vector3 startPosForUndo;

	private Vector3 difference;

	private Vector3 offset;

	private float distancey;

	private float margin = 0.001f;

	private void Awake()
	{
		Instance = this;
		startMat = myRenderers[0].material;
	}

	private void Update()
	{
		Machine machine = Machine.Active();
		if ((bool)machine && !StatMaster.Mode.isTranslating)
		{
			arrowParent.position = machine.MiddlePosition;
		}
	}

	public override void OnDisable()
	{
		base.OnDisable();
		if (StatMaster.Mode.isTranslating)
		{
			StopTool();
			StatMaster.Mode.isTranslating = false;
		}
	}

	private void OnEnable()
	{
		SetSelected(false);
	}

	protected override void LateUpdate()
	{
		if (StatMaster.Mode.isTranslating)
		{
			base.transform.localPosition = Vector3.zero;
		}
		base.LateUpdate();
	}

	public override void OnClicked()
	{
		StatMaster.StopHotKeys(true);
		StatMaster.Mode.isTranslating = true;
		startMachine = Machine.Active();
		if ((bool)startMachine)
		{
			Vector3 position = startMachine.Position;
			startPosForUndo = position;
			difference = startPosForUndo - startMachine.MiddlePosition;
			startMachine.SetRigidInterpolation(RigidbodyInterpolation.None);
			Vector3 inNormal = ((axis != Axes.z) ? Vector3.forward : Vector3.right);
			StartTranslateMachine(startMachine, axis);
			groundPlane = new Plane(inNormal, arrowParent.position);
			offset = arrowParent.position - GetOffset(arrowParent);
			SetSelected(true);
			hasNetworkedTransform = false;
		}
	}

	public override void OnClickDrag()
	{
		if (!(startMachine == null))
		{
			Ray ray = Camera.main.ScreenPointToRay(InputManager.CursorPosition());
			if (useAlternateTranslateMode)
			{
				groundPlane.SetNormalAndPosition(Camera.main.transform.forward, arrowParent.position);
			}
			float enter;
			if (groundPlane.Raycast(ray, out enter))
			{
				posToBe = ray.GetPoint(enter) + offset;
			}
			arrowParent.position += Vector3.Project(posToBe - arrowParent.position, base.transform.up);
			TranslateMachine(arrowParent);
		}
	}

	public override void OnClickReleased()
	{
		StatMaster.StopHotKeys(false);
		SetSelected(false);
		if (!startMachine)
		{
			StatMaster.Mode.isTranslating = false;
			return;
		}
		startMachine.RestoreRigidInterpolation();
		StopTranslateMachine(startMachine, axis, arrowParent.position + difference, arrowParent);
		StartCoroutine(StopTranslating());
		StopTool();
	}

	public void StartTranslateMachine(Machine machine, Axes axis)
	{
		currentInterval = 0f;
		if (machine == null || machine.BuildingMachine == null || machine.isSimulating || !machine.CanModify)
		{
			return;
		}
		Bounds bounds = machine.GetBounds(false);
		Transform buildingMachine = machine.BuildingMachine;
		Vector3 vector = buildingMachine.position;
		Transform transform = null;
		if (StatMaster.isMP)
		{
			transform = PlayerData.localPlayer.buildZone.transform;
			vector = transform.InverseTransformPoint(vector);
			vector.y += 5.05f;
		}
		Vector3 vector2 = bounds.center - vector;
		startMaxPosition.x = StatMaster.Bounding.rightPos - (bounds.extents.x + vector2.x) - margin;
		startMinPosition.x = StatMaster.Bounding.leftPos - (0f - bounds.extents.x + vector2.x) + margin;
		startMaxPosition.y = StatMaster.Bounding.roofHeight - (bounds.extents.y + vector2.y) - margin;
		startMinPosition.y = StatMaster.Bounding.floorPos - (0f - bounds.extents.y + vector2.y) + margin;
		startMaxPosition.z = StatMaster.Bounding.frontPos - (bounds.extents.z + vector2.z) - margin;
		startMinPosition.z = StatMaster.Bounding.backPos - (0f - bounds.extents.z + vector2.z) + margin;
		if (StatMaster.Bounding.zoneRotationMode != ZoneRotationMode.Custom)
		{
			return;
		}
		Vector3 vector3 = buildingMachine.TransformDirection(Vector3.up);
		Vector3 vector4 = buildingMachine.TransformDirection(Vector3.down);
		Vector3 zero = Vector3.zero;
		Vector3 zero2 = Vector3.zero;
		bool[] array = new bool[3];
		bool[] array2 = new bool[3];
		NetworkBoundingBoxController networkBoundingBoxController = machine.boundingBoxController as NetworkBoundingBoxController;
		Vector3[] globalBoundPoints = networkBoundingBoxController.GetGlobalBoundPoints(bounds);
		foreach (Vector3 origin in globalBoundPoints)
		{
			Ray ray = new Ray(origin, vector3);
			Ray ray2 = new Ray(origin, vector4);
			for (int j = 0; j < StatMaster.Bounding.worldBounds.Length; j++)
			{
				Plane plane = StatMaster.Bounding.worldBounds[j];
				bool flag = Vector3.Dot(vector3, plane.normal) > 0f;
				bool flag2 = j % 2 == 0;
				int num = ((j >= 2) ? ((j < 4) ? 1 : 2) : 0);
				float enter;
				if (plane.Raycast(ray, out enter))
				{
					if (flag2)
					{
						zero[num] = ((array[num] && !((!flag) ? (enter > zero[num]) : (enter < zero[num]))) ? zero[num] : enter);
						array[num] = true;
					}
					else
					{
						zero2[num] = ((array2[num] && !((!flag) ? (enter > zero2[num]) : (enter < zero2[num]))) ? zero2[num] : enter);
						array2[num] = true;
					}
				}
				flag = Vector3.Dot(vector4, plane.normal) > 0f;
				if (plane.Raycast(ray2, out enter))
				{
					enter = 0f - enter;
					if (flag2)
					{
						zero[num] = ((array[num] && !((!flag) ? (enter < zero[num]) : (enter > zero[num]))) ? zero[num] : enter);
						array[num] = true;
					}
					else
					{
						zero2[num] = ((array2[num] && !((!flag) ? (enter < zero2[num]) : (enter > zero2[num]))) ? zero2[num] : enter);
						array2[num] = true;
					}
				}
			}
		}
		float num2 = float.MaxValue;
		float num3 = float.MaxValue;
		for (int i = 0; i < array.Length; i++)
		{
			num2 = ((!array[i] || !(Mathf.Abs(zero[i]) < Mathf.Abs(num2))) ? num2 : zero[i]);
			num3 = ((!array2[i] || !(Mathf.Abs(zero2[i]) < Mathf.Abs(num3))) ? num3 : zero2[i]);
		}
		AdjustStartMinMax(axis, vector, num2, num3);
	}

	private void AdjustStartMinMax(Axes axis, Vector3 localPos, float uMin, float uMax)
	{
		bool flag = axis == Axes.x || axis == Axes.xy || axis == Axes.xz;
		bool flag2 = axis == Axes.y || axis == Axes.xy || axis == Axes.yz;
		bool flag3 = axis == Axes.z || axis == Axes.xz || axis == Axes.yz;
		if (axis != Axes.x && axis != Axes.y && axis != Axes.z)
		{
			if (flag)
			{
				AdjustStartMinMax(Axes.x, localPos, uMin, uMax);
			}
			if (flag2)
			{
				AdjustStartMinMax(Axes.y, localPos, uMin, uMax);
			}
			if (flag3)
			{
				AdjustStartMinMax(Axes.z, localPos, uMin, uMax);
			}
		}
		else
		{
			float num = localPos[(int)axis] + Mathf.Min(uMin, uMax) + margin;
			float num2 = localPos[(int)axis] + Mathf.Max(uMin, uMax) - margin;
			startMinPosition[(int)axis] = ((!StatMaster.Bounding.Enabled) ? num : Mathf.Max(startMinPosition[(int)axis], num));
			startMaxPosition[(int)axis] = ((!StatMaster.Bounding.Enabled) ? num2 : Mathf.Min(startMaxPosition[(int)axis], num2));
		}
	}

	public void TranslateMachine(Transform pivot)
	{
		Vector3 position = pivot.position + difference;
		startMachine.BuildingMachine.position = position;
		UpdateTransformInfo(startMachine);
	}

	public void StopTranslateMachine(Machine machine, Axes axis, Vector3 newPos, Transform pivot = null)
	{
		Transform transform = null;
		if (StatMaster.isMP)
		{
			transform = PlayerData.localPlayer.buildZone.transform;
			newPos = transform.InverseTransformPoint(newPos);
			newPos.y += 5.05f;
		}
		if (StatMaster.Bounding.zoneRotationMode != ZoneRotationMode.NoWorldClamp || StatMaster.Bounding.Enabled)
		{
			bool flag = axis == Axes.x || axis == Axes.xy || axis == Axes.xz;
			bool flag2 = axis == Axes.y || axis == Axes.xy || axis == Axes.yz;
			bool flag3 = axis == Axes.z || axis == Axes.xz || axis == Axes.yz;
			if (pivot != null)
			{
				pivot.position = new Vector3((!flag) ? pivot.position.x : posToBe.x, (!flag2) ? pivot.position.y : posToBe.y, (!flag3) ? pivot.position.z : posToBe.z);
			}
			newPos.x = ((!flag) ? newPos.x : Mathf.Clamp(newPos.x, startMinPosition.x, startMaxPosition.x));
			newPos.y = ((!flag2) ? newPos.y : Mathf.Clamp(newPos.y, startMinPosition.y, startMaxPosition.y));
			newPos.z = ((!flag3) ? newPos.z : Mathf.Clamp(newPos.z, startMinPosition.z, startMaxPosition.z));
		}
		if (StatMaster.isMP)
		{
			newPos = transform.TransformPoint(new Vector3(newPos.x, newPos.y - 5.05f, newPos.z));
		}
		machine.SetPosition(newPos);
	}

	private void StopTool()
	{
		SendTransformInfo(startMachine);
		startMachine.SetPosition(startMachine.Position);
		startMachine.UndoSystem.ChangePosition(startPosForUndo);
		SingleInstanceFindOnly<AddPiece>.Instance.UpdateMiddleOfObject();
	}

	private Vector3 GetOffset(Transform pivot = null)
	{
		Ray ray = Camera.main.ScreenPointToRay(InputManager.CursorPosition());
		if (useAlternateTranslateMode)
		{
			pivot = ((!(pivot == null)) ? pivot : arrowParent);
			groundPlane.SetNormalAndPosition(Camera.main.transform.forward, pivot.position);
		}
		float enter;
		if (!groundPlane.Raycast(ray, out enter))
		{
			Debug.LogWarning("Raycast didn't hit the ground!");
			return Vector3.zero;
		}
		return ray.GetPoint(enter);
	}

	private void SetSelected(bool toggle)
	{
		for (int i = 0; i < myRenderers.Length; i++)
		{
			myRenderers[i].material = ((!toggle) ? startMat : selectedMaterial);
		}
	}

	private IEnumerator StopTranslating()
	{
		if (StatMaster.Mode.isTranslating)
		{
			yield return null;
			StatMaster.Mode.isTranslating = false;
		}
	}
}
