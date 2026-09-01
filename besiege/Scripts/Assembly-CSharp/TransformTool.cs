using System.Collections.Generic;
using UnityEngine;

public class TransformTool : ClickBehaviour
{
	protected enum MouseRayType
	{
		Normal = 0,
		Translate = 1,
		Rotate = 2
	}

	[HideInInspector]
	public bool reverse;

	[SerializeField]
	private Transform objToMove;

	public Renderer[] myRenderers;

	public Material selectedMaterial;

	public Material highlightMaterial;

	public Transform[] reverseTransforms;

	protected List<ISelectable> selectedObjects;

	protected Material startMat;

	protected Vector3[] originalPositions;

	protected Quaternion[] originalRotations;

	protected Vector3[] originalScales;

	protected Vector2 lastMove;

	protected Vector3 clickOffset;

	protected Vector3 localVec;

	protected Vector3 localTangent;

	protected Vector3 movePosition;

	protected Plane movementPlane;

	protected Vector3 localMoveVector;

	protected Vector3 relativeMoveVector;

	protected Vector3 localTanMoveVector;

	protected Vector3 moveVector;

	protected Vector3 tangentMoveVector;

	protected Vector3 viewTanMoveVector;

	protected bool isStepping;

	protected bool mouseOverTool;

	protected float rotateMultiplier = 10f;

	protected float deltaAngle;

	protected Vector3 dragAxis;

	protected float angle;

	protected Vector3 localDir;

	private float dragTime;

	private bool wasClicked;

	protected virtual bool UseDragTool
	{
		get
		{
			return true;
		}
	}

	protected bool hasMoved
	{
		get
		{
			return dragTime > 0.1f;
		}
	}

	public Vector3 GizmoPosition
	{
		get
		{
			return Gizmo().position;
		}
		set
		{
			Gizmo().position = value;
		}
	}

	public Vector3 GizmoLocalPosition
	{
		get
		{
			return Gizmo().localPosition;
		}
		set
		{
			Gizmo().localPosition = value;
		}
	}

	public static Vector3 SnapFloor(Vector3 val, float snap)
	{
		return new Vector3(SnapFloor(val.x, snap), SnapFloor(val.y, snap), SnapFloor(val.z, snap));
	}

	public static float SnapFloor(float val, float snap)
	{
		return Mathf.Floor(val / snap) * snap;
	}

	public static Vector3 SnapCeil(Vector3 val, float snap)
	{
		return new Vector3(SnapCeil(val.x, snap), SnapCeil(val.y, snap), SnapCeil(val.z, snap));
	}

	public static float SnapCeil(float val, float snap)
	{
		return Mathf.Ceil(val / snap) * snap;
	}

	public static Vector3 Snap(Vector3 val, float snap)
	{
		return new Vector3(Snap(val.x, snap), Snap(val.y, snap), Snap(val.z, snap));
	}

	public static Vector3 Snap(Vector3 val, Transform relative, float snap)
	{
		Vector3 vector = relative.InverseTransformDirection(val);
		vector = new Vector3(Snap(vector.x, snap), Snap(vector.y, snap), Snap(vector.z, snap));
		return relative.TransformDirection(vector);
	}

	public static float Snap(float val, float snap)
	{
		return Mathf.Round(val / snap) * snap;
	}

	public virtual Transform Gizmo()
	{
		return objToMove;
	}

	public Vector3 InverseGizmoDirection(Vector3 dir)
	{
		return Gizmo().InverseTransformDirection(dir);
	}

	public void RotateGizmoAround(Vector3 pos, Vector3 up, float angle)
	{
		Gizmo().RotateAround(pos, up, angle);
	}

	protected virtual void ResetTool()
	{
		if (StatMaster.ToolActive)
		{
			StatMaster.StopHotKeys(false);
			StatMaster.ToolActive = false;
		}
		StatMaster.Mode.currentBlockTool = null;
		SetSelected(false);
		isStepping = false;
	}

	protected void UpdateLocalVecAcrossNormal(bool moveAcrossNormal)
	{
		if (moveAcrossNormal)
		{
			localVec = (base.transform.up + base.transform.right).normalized;
		}
	}

	protected void SetupRotate()
	{
		dragAxis = CastMouseRay(MouseRayType.Rotate, localVec, base.transform.position);
		deltaAngle = 0f;
	}

	protected virtual Transform RevertTransform(int index)
	{
		ISelectable selectable = selectedObjects[index];
		RevertTransform(selectable, originalPositions[index], originalRotations[index]);
		return selectable.GetTransform();
	}

	protected void RevertTransform(ISelectable sel, Vector3 iPos, Quaternion iRot)
	{
		Transform transform = sel.GetTransform();
		transform.position = iPos;
		transform.rotation = iRot;
	}

	protected void StepRotate(float step)
	{
		deltaAngle = step * (float)(reverse ? 1 : (-1));
		localDir = InverseGizmoDirection(base.transform.up);
		SetRelativeVectors();
	}

	protected void RotateEntity(ISelectable sel, Vector3 pivot, Vector3 axis, float angle, bool rotateOnlyPosition = false)
	{
		Transform transform = sel.GetTransform();
		transform.RotateAround(pivot, axis, angle);
		Quaternion rotation = transform.rotation;
		Vector3 position = transform.position;
		sel.SetRotation(rotation);
		if (!rotateOnlyPosition)
		{
			transform.rotation = rotation;
		}
		sel.SetPosition(position);
		transform.position = position;
	}

	protected void StepTranslate(float step)
	{
		float num = step * (float)((!reverse) ? 1 : (-1));
		moveVector = localVec * num;
		localMoveVector = InverseGizmoDirection(moveVector);
		SetRelativeVectors();
	}

	protected Vector3 GizmoDragTranslate()
	{
		return GizmoPosition = movePosition + moveVector;
	}

	protected void GizmoDragRotate()
	{
		angle = (InputManager.MouseX() * dragAxis.x + InputManager.MouseY() * dragAxis.y) * rotateMultiplier;
		RotateGizmoAround(GizmoPosition, base.transform.up, angle);
		deltaAngle += angle;
		localDir = InverseGizmoDirection(base.transform.up);
	}

	protected Axes GetAxis(string axisName)
	{
		if (axisName.Equals("X"))
		{
			return Axes.x;
		}
		if (axisName.Equals("Y"))
		{
			return Axes.y;
		}
		if (axisName.Equals("Z"))
		{
			return Axes.z;
		}
		if (axisName.Equals("XY"))
		{
			return Axes.xy;
		}
		if (axisName.Equals("XZ"))
		{
			return Axes.xz;
		}
		if (axisName.Equals("YZ"))
		{
			return Axes.yz;
		}
		Debug.LogError("Not a valid axis: " + axisName + "!");
		return Axes.x;
	}

	protected virtual void Awake()
	{
		startMat = myRenderers[0].material;
	}

	protected virtual void OnEnable()
	{
		SetSelected(false);
		UpdateReverse();
	}

	public override void OnDisable()
	{
		base.OnDisable();
		ResetTool();
	}

	protected virtual void OnGizmoClicked(List<ISelectable> selection)
	{
		moveVector = (localMoveVector = Vector3.zero);
		selectedObjects = selection;
		if (!StatMaster.ToolActive)
		{
			StatMaster.StopHotKeys(true);
			StatMaster.ToolActive = true;
		}
		int count = selectedObjects.Count;
		originalPositions = new Vector3[count];
		originalRotations = new Quaternion[count];
		originalScales = new Vector3[count];
		for (int i = 0; i < count; i++)
		{
			ISelectable selectable = selectedObjects[i];
			if (selectable != null)
			{
				if (selectable is BlockBehaviour)
				{
					BlockBehaviour blockBehaviour = selectable as BlockBehaviour;
					originalPositions[i] = blockBehaviour.Position;
					originalRotations[i] = blockBehaviour.Rotation;
					originalScales[i] = blockBehaviour.Scale;
				}
				else
				{
					Transform transform = selectable.GetTransform();
					originalPositions[i] = transform.position;
					originalRotations[i] = transform.rotation;
					originalScales[i] = transform.localScale;
				}
			}
			else
			{
				originalPositions[i] = Vector3.zero;
				originalRotations[i] = Quaternion.identity;
				originalScales[i] = Vector3.one;
			}
		}
		SetSelected(true);
		dragTime = 0f;
		movePosition = GizmoPosition;
		localVec = base.transform.up;
		localTangent = base.transform.right;
		lastMove = InputManager.CursorPosition();
		clickOffset = Vector3.zero;
		clickOffset = GetMouseOffset();
	}

	protected virtual void OnGizmoDrag()
	{
		moveVector = GetMouseOffset();
		localMoveVector = InverseGizmoDirection(moveVector);
		SetRelativeVectors();
	}

	protected virtual void OnGizmoReleased(List<ISelectable> selection)
	{
		bool useSnap = UseSnap();
		for (int i = 0; i < selection.Count; i++)
		{
			ISelectable selectable = selection[i];
			if (selectable != null)
			{
				if (!hasMoved)
				{
					TransformEntity(selectable, i, useSnap);
				}
				AddUndo(selectable, i);
			}
		}
		ProcessUndo();
	}

	protected bool CanInteract()
	{
		Machine machine = Machine.Active();
		if (machine == null)
		{
			return false;
		}
		if (SelectionTool.BatchChange)
		{
			return false;
		}
		return machine.ReadyForSim;
	}

	protected void OnMouseEnter()
	{
		if (!StatMaster.ToolActive)
		{
			SetHighlight(true);
		}
		mouseOverTool = true;
	}

	protected void OnMouseExit()
	{
		if (!StatMaster.ToolActive)
		{
			SetHighlight(false);
		}
		mouseOverTool = false;
	}

	protected virtual bool SnapKeyHeld()
	{
		return InputManager.AdvancedBuilding.LeftCtrlKey();
	}

	protected virtual bool ReverseKey()
	{
		return InputManager.AdvancedBuilding.LeftAltKey();
	}

	protected virtual bool MultiSelectKey()
	{
		return InputManager.AdvancedBuilding.LeftShiftKey();
	}

	protected virtual bool UseSnap()
	{
		bool flag = true;
		return (!SnapKeyHeld()) ? flag : (!flag);
	}

	protected virtual void SetRelativeVectors()
	{
		int count = selectedObjects.Count;
		object obj;
		if (count > 0)
		{
			ISelectable selectable = selectedObjects[count - 1];
			obj = selectable;
		}
		else
		{
			obj = null;
		}
		ISelectable selectable2 = (ISelectable)obj;
		relativeMoveVector = ((selectable2 == null) ? localMoveVector : selectable2.GetTransform().InverseTransformDirection(moveVector));
		localTanMoveVector = InverseGizmoDirection(tangentMoveVector);
	}

	protected virtual void TransformEntity(ISelectable entity, int index, bool useSnap)
	{
	}

	protected virtual void Step()
	{
	}

	public sealed override void OnClickDrag()
	{
		if (!wasClicked || !UseDragTool)
		{
			return;
		}
		Vector2 vector = InputManager.CursorPosition();
		if (!StatMaster.ToolActive || lastMove == vector)
		{
			return;
		}
		dragTime += Time.unscaledDeltaTime;
		if (!hasMoved)
		{
			return;
		}
		lastMove = vector;
		OnGizmoDrag();
		bool useSnap = UseSnap();
		for (int i = 0; i < selectedObjects.Count; i++)
		{
			ISelectable selectable = selectedObjects[i];
			if (selectable != null)
			{
				TransformEntity(selectable, i, useSnap);
			}
		}
	}

	public sealed override void OnClicked()
	{
		if (CanInteract())
		{
			wasClicked = true;
			OnGizmoClicked();
		}
	}

	public sealed override void OnClickReleased()
	{
		if (wasClicked)
		{
			wasClicked = false;
			if (!hasMoved)
			{
				isStepping = true;
				Step();
			}
			OnGizmoReleased();
			ResetTool();
			if (UseSnap())
			{
				UpdateGizmo();
			}
		}
	}

	protected virtual void OnGizmoClicked()
	{
		OnGizmoClicked(ReferenceMaster.Selectables);
	}

	protected virtual void OnGizmoReleased()
	{
		OnGizmoReleased(selectedObjects);
	}

	protected virtual void UpdateGizmo()
	{
		AdvancedBlockEditor.Instance.UpdateGizmo();
	}

	public virtual void AddUndo(ISelectable entity, int i)
	{
	}

	public virtual void ProcessUndo()
	{
	}

	protected void SetHighlight(bool toggle)
	{
		for (int i = 0; i < myRenderers.Length; i++)
		{
			myRenderers[i].material = ((!toggle) ? ((!StatMaster.ToolActive) ? startMat : selectedMaterial) : highlightMaterial);
		}
	}

	protected void SetSelected(bool toggle)
	{
		for (int i = 0; i < myRenderers.Length; i++)
		{
			myRenderers[i].material = ((!toggle) ? ((!mouseOverTool) ? startMat : highlightMaterial) : selectedMaterial);
		}
	}

	protected virtual void Update()
	{
		UpdateReverse();
	}

	protected virtual void UpdateReverseVisual()
	{
		Vector3 localEulerAngles = new Vector3(0f, 0f, reverse ? 180 : 0);
		for (int i = 0; i < reverseTransforms.Length; i++)
		{
			reverseTransforms[i].localEulerAngles = localEulerAngles;
		}
	}

	protected virtual void UpdateReverse()
	{
		if (!StatMaster.ToolActive && ReverseKey() != reverse)
		{
			reverse = !reverse;
			UpdateReverseVisual();
		}
	}

	protected virtual Vector3 SnapVector(Vector3 oldPos, Vector3 delta, float snapValue)
	{
		Vector3 val = oldPos + delta;
		val = Snap(val, snapValue);
		float num = 0.001f;
		bool flag = Mathf.Abs(delta.x) > num;
		bool flag2 = Mathf.Abs(delta.y) > num;
		bool flag3 = Mathf.Abs(delta.z) > num;
		return new Vector3((!flag) ? oldPos.x : val.x, (!flag2) ? oldPos.y : val.y, (!flag3) ? oldPos.z : val.z);
	}

	protected Vector3 CastMouseRay(MouseRayType t, Vector3 fwd, Vector3 pos)
	{
		Ray ray = Camera.main.ScreenPointToRay(InputManager.CursorPosition());
		movementPlane = new Plane(fwd, pos);
		float enter;
		if (movementPlane.Raycast(ray, out enter))
		{
			Vector3 point = ray.GetPoint(enter);
			switch (t)
			{
			case MouseRayType.Normal:
			{
				Vector3 vector = point - movePosition - clickOffset;
				tangentMoveVector = Vector3.Project(vector, localTangent);
				Transform transform2 = Camera.main.transform;
				viewTanMoveVector = transform2.InverseTransformDirection(Vector3.Project(vector, transform2.right));
				return Vector3.Project(vector, localVec);
			}
			case MouseRayType.Translate:
				return point - movePosition - clickOffset;
			case MouseRayType.Rotate:
			{
				Transform transform = Camera.main.transform;
				Vector3 direction = Vector3.Cross(localVec, point - base.transform.position);
				return transform.InverseTransformDirection(direction).normalized;
			}
			}
		}
		return Vector3.zero;
	}

	protected virtual Vector3 GetMouseOffset()
	{
		Transform transform = Camera.main.transform;
		return CastMouseRay(MouseRayType.Normal, transform.forward, movePosition);
	}
}
