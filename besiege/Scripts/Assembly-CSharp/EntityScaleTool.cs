using UnityEngine;

public class EntityScaleTool : EntityTransformTool
{
	public enum ScaleAxis
	{
		X = 0,
		Y = 1,
		Z = 2,
		All = 3
	}

	public static float SNAP_VALUE = 0.1f;

	public static bool usePivotScale = true;

	public ScaleAxis scaleAxis;

	public EntityScaleTool[] scaleTools;

	public Transform scaleParent;

	public Transform toolTransform;

	private float basePosY;

	private float baseReverseY;

	private float currentScale;

	private static readonly Vector3 right = Vector3.right;

	private static readonly Vector3 up = Vector3.up;

	private static readonly Vector3 forward = Vector3.forward;

	protected override void Awake()
	{
		base.Awake();
		currentScale = 1f;
		basePosY = 2f;
		baseReverseY = -2f;
		scaleParent.parent = null;
	}

	protected override void ResetTool()
	{
		base.ResetTool();
		StatMaster.Mode.isScaling = false;
		if (scaleAxis != ScaleAxis.All)
		{
			UpdateTool((!reverse) ? 1f : 0.5f);
			return;
		}
		EntityScaleTool[] array = scaleTools;
		foreach (EntityScaleTool entityScaleTool in array)
		{
			entityScaleTool.UpdateTool((!reverse) ? 1f : 0.5f);
		}
	}

	public void ToolStateUpdate(EntityScaleBox.MouseState state)
	{
		switch (state)
		{
		case EntityScaleBox.MouseState.Enter:
			OnMouseEnter();
			break;
		case EntityScaleBox.MouseState.Exit:
			OnMouseExit();
			break;
		case EntityScaleBox.MouseState.Drag:
			OnClickDrag();
			break;
		case EntityScaleBox.MouseState.Down:
			OnClicked();
			break;
		case EntityScaleBox.MouseState.Up:
			OnClickReleased();
			break;
		}
	}

	protected override void UpdateReverse()
	{
		if (!StatMaster.ToolActive && ReverseKey() != reverse)
		{
			reverse = !reverse;
			if (myRenderers.Length > 1)
			{
				UpdateTool((!reverse) ? 1f : 0.5f);
			}
		}
	}

	protected override void OnGizmoDrag()
	{
		base.OnGizmoDrag();
		if (scaleAxis != ScaleAxis.All && !reverse)
		{
			base.GizmoPosition = movePosition + moveVector;
			float scale = base.GizmoLocalPosition.y / basePosY;
			UpdateTool(scale);
		}
	}

	public void UpdateTool(float scale)
	{
		float num = ((!reverse) ? basePosY : baseReverseY) * scale;
		base.GizmoLocalPosition = new Vector3(base.GizmoLocalPosition.x, num, base.GizmoLocalPosition.z);
		Transform transform = myRenderers[0].transform;
		transform.localPosition = new Vector3(transform.localPosition.x, num / 2f, transform.localPosition.z);
		transform.localScale = new Vector3(transform.localScale.x, num, transform.localScale.z);
		currentScale = scale;
	}

	protected override void OnGizmoClicked()
	{
		StatMaster.Mode.isScaling = true;
		base.OnGizmoClicked();
		scaleParent.position = toolTransform.position;
		scaleParent.rotation = toolTransform.rotation;
	}

	protected override LevelUndoAction CreateUndoAction(LevelEntity entity, Vector3 oldPosition, Quaternion oldRotation, Vector3 oldScale)
	{
		if (entity.Scale == oldScale)
		{
			return null;
		}
		return new LUAScaleEntity(entity, oldPosition, oldRotation, oldScale);
	}

	public static float GetScaleValue(float s)
	{
		if (OptionsMaster.negativeScaling)
		{
			return s;
		}
		s = ((!(s < 0f)) ? s : (0f - s));
		return (!(s < OptionsMaster.minComponentUnit)) ? s : OptionsMaster.minComponentUnit;
	}

	protected override void TransformEntity(ISelectable entity, int index, bool useSnap)
	{
		LevelEntity levelEntity = entity as LevelEntity;
		if (entity == null)
		{
			return;
		}
		Transform transform = entity.GetTransform();
		Cloth[] componentsInChildren = transform.GetComponentsInChildren<Cloth>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			if (!componentsInChildren[i].gameObject.activeSelf)
			{
				componentsInChildren[i] = null;
			}
			else
			{
				componentsInChildren[i].gameObject.SetActive(false);
			}
		}
		bool flag = StatMaster.Mode.LevelEditor.objectPivot && usePivotScale;
		bool flag2 = StatMaster.Mode.LevelEditor.global || StatMaster.Mode.LevelEditor.linked;
		int num = (int)scaleAxis;
		float num2 = SNAP_VALUE;
		Vector3 vector = ConvertScaleToWorld(originalRotations[originalRotations.Length - 1], originalScales[originalScales.Length - 1]);
		float num3 = ((scaleAxis != ScaleAxis.All) ? vector[num] : Mathf.Max(1f, Mathf.Min(vector.x, vector.y, vector.z)));
		if (flag2 && flag)
		{
			num2 = num3 * num2;
		}
		if (originalScales.Length > 1)
		{
			vector.x = (vector.y = (vector.z = num3));
		}
		Vector3 vector2 = originalPositions[index];
		Vector3 vector3 = originalScales[index];
		Quaternion rotation = originalRotations[index];
		transform.position = vector2;
		transform.rotation = rotation;
		transform.localScale = vector3;
		Vector3 vector4 = ((!flag2) ? vector3 : ((!flag) ? Vector3.one : vector));
		if (!isStepping)
		{
			if (scaleAxis == ScaleAxis.All)
			{
				Vector2 vector5 = new Vector2(viewTanMoveVector.x, moveVector.y);
				float num4 = vector5.magnitude * Vector2.Dot(vector5.normalized, new Vector2(0.5f, 0.5f));
				currentScale = 1f + num4 / basePosY;
				vector4 *= currentScale;
				EntityScaleTool[] array = scaleTools;
				foreach (EntityScaleTool entityScaleTool in array)
				{
					entityScaleTool.UpdateTool(currentScale);
				}
			}
			else if (levelEntity.behaviour.prefab.uniformScale)
			{
				vector4 *= currentScale;
				EntityScaleTool[] array2 = scaleTools;
				foreach (EntityScaleTool entityScaleTool2 in array2)
				{
					entityScaleTool2.UpdateTool(currentScale);
				}
			}
			else
			{
				int index3;
				int index2 = (index3 = num);
				float num5 = vector4[index3];
				vector4[index2] = num5 * currentScale;
			}
			if (useSnap)
			{
				vector4 = TransformTool.Snap(vector4, num2);
			}
		}
		else
		{
			float num6 = ((!reverse) ? num2 : (0f - num2));
			if (scaleAxis == ScaleAxis.All || levelEntity.behaviour.prefab.uniformScale)
			{
				vector4 += new Vector3(num6, num6, num6);
			}
			else
			{
				int index3;
				int index4 = (index3 = num);
				float num5 = vector4[index3];
				vector4[index4] = num5 + num6;
			}
		}
		Vector3 vector6 = vector4;
		if (!OptionsMaster.negativeScaling)
		{
			vector6.x = GetScaleValue(vector4.x);
			vector6.y = GetScaleValue(vector4.y);
			vector6.z = GetScaleValue(vector4.z);
		}
		if (!flag2)
		{
			if (!StatMaster.Mode.LevelEditor.objectPivot)
			{
				Vector3 center = entity.GetCenter();
				transform.localScale = vector6;
				Vector3 center2 = entity.GetCenter();
				Vector3 vector7 = center - center2;
				Vector3 position = vector2 + vector7;
				levelEntity.SetPosition(position);
				transform.position = position;
			}
			if (levelEntity.behaviour.prefab.canScale)
			{
				levelEntity.SetScale(vector6);
				transform.localScale = vector6;
			}
			else
			{
				transform.localScale = vector3;
			}
		}
		else
		{
			scaleParent.localScale = ((!flag) ? Vector3.one : vector);
			Transform parent = transform.parent;
			transform.SetParent(scaleParent, true);
			scaleParent.localScale = vector6;
			transform.SetParent(parent, true);
			levelEntity.SetPosition(transform.position);
			levelEntity.SetRotation(transform.rotation);
			if (levelEntity.behaviour.prefab.canScale)
			{
				levelEntity.SetScale(transform.localScale);
			}
			else
			{
				transform.localScale = vector3;
			}
		}
		for (int l = 0; l < componentsInChildren.Length; l++)
		{
			if (componentsInChildren[l] != null)
			{
				componentsInChildren[l].gameObject.SetActive(true);
			}
		}
	}

	public Vector3 ConvertScaleToWorld(Quaternion rotation, Vector3 localScale)
	{
		Vector3 a = rotation * right;
		Vector3 a2 = rotation * up;
		Vector3 a3 = rotation * forward;
		float x = localScale.x;
		float y = localScale.y;
		float z = localScale.z;
		float x2 = Abs(Dot(a, right)) * x + Abs(Dot(a2, right)) * y + Abs(Dot(a3, right)) * z;
		float y2 = Abs(Dot(a, up)) * x + Abs(Dot(a2, up)) * y + Abs(Dot(a3, up)) * z;
		float z2 = Abs(Dot(a, forward)) * x + Abs(Dot(a2, forward)) * y + Abs(Dot(a3, forward)) * z;
		return new Vector3(x2, y2, z2);
	}

	private static float Dot(Vector3 a, Vector3 b)
	{
		return a.x * b.x + a.y * b.y + a.z * b.z;
	}

	private static float Abs(float value)
	{
		return (!(value < 0f)) ? value : (0f - value);
	}
}
