using System;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

[ExecuteInEditMode]
[DisallowMultipleComponent]
[AddComponentMenu("Pine/Slidable")]
[HelpURL("https://docs.google.com/document/d/1UDr-K5duDF149t3thxqdfzbdEftFoBc7OblCHlt5HIg/edit?usp=sharing")]
public class Slidable : Interactive
{
	public enum SnapMode
	{
		[Tooltip("No snapping will be performed on release.")]
		DontSnap = 0,
		[Tooltip("Snap to the closest snap point on release.")]
		ToClosestSnapPoint = 1,
		[Tooltip("Snap to specific snap point (defined by index) on release.")]
		ToSnapPointWithIndex = 2
	}

	[Header("Positional configuration")]
	[Tooltip("Transform whose world position represents the start point of this Slidable.")]
	[DontSave]
	public Transform startNode;

	[Tooltip("Transform whose world position represents the end point of this Slidable.")]
	[DontSave]
	public Transform endNode;

	[Header("Snapping configuration")]
	[Tooltip("Defines how snapping should be performed once this Slidable is released.")]
	[DontSave]
	public SnapMode snapMode;

	[Tooltip("Only used if 'snapMode' is set to 'ToSnapPointWithIndex'.")]
	[Min(0f)]
	[DontSave]
	public int snapPointIndex;

	[Tooltip("Number of additional snap points between nodes. If zero, snap is performed between start and end nodes.")]
	[Min(0f)]
	[DontSave]
	public int additionalSnapPointCount;

	[Tooltip("Snap animation duration in seconds. It represents time it takes to travel from one snap point to the next.")]
	[Min(0f)]
	[DontSave]
	public float snapAnimationDuration = 0.1f;

	[Header("Curve (Bézier)")]
	[Tooltip("If true, you can modify control points to create a Bézier curved path between start and end nodes.")]
	[DontSave]
	public bool isCurved;

	[Tooltip("If true, piece moved on the path will be rotated to match curve's tangent.")]
	[DontSave]
	public bool isTangent;

	[Tooltip("Position relative to the start node that defines the control point of the Bézier curve.")]
	[DontSave]
	public Vector3 startControlLocalPosition;

	[Tooltip("Position relative to the end node that defines the control point of the Bézier curve.")]
	[DontSave]
	public Vector3 endControlLocalPosition;

	[Header("Interacting configuration")]
	[Tooltip("Controller specific setting that defines how fast this Slidable is moving. If zero, this setting is ignored.")]
	[Min(0f)]
	[DontSave]
	public float controllerSpeed;

	[DontSave]
	public MouseDragType mouseDragType;

	[DontSave]
	public float mouseDragSpeed = 1f;

	[Tooltip("Sound played when interaction with this Slidable starts.")]
	[DontSave]
	[HideInInspector]
	public EventReference startSound;

	[Tooltip("Sound played when this Slidable is being moved/dragged.")]
	[DontSave]
	[HideInInspector]
	public EventReference moveSound;

	[Tooltip("Sound played when this Slidable crosses any snap point.")]
	[DontSave]
	[HideInInspector]
	public EventReference snapPointSound;

	[Tooltip("Sound played when interaction with this Slidable ends.")]
	[DontSave]
	[HideInInspector]
	public EventReference endSound;

	[Tooltip("Move sound will be stopped if no movement is detected for this many frames. If negative, it will always be played while holding.")]
	[DontSave]
	[HideInInspector]
	public int idleFramesToStopMoveSound = 10;

	[Tooltip("If true, snap point sound will be played even while dragging. Otherwise, it is played only on release.")]
	[DontSave]
	[HideInInspector]
	public bool playSnapPointSoundWhileDragging;

	[Header("Room editor")]
	[DontSave]
	public List<LockArgument> locks;

	[DontSave]
	private Camera gizmoCamera;

	[NonSerialized]
	[DontSave]
	internal Vector3 cameraStartPosition;

	[NonSerialized]
	[DontSave]
	internal Quaternion cameraStartRotation;

	[NonSerialized]
	[DontSave]
	internal Vector3 pointerScreenPosition;

	[NonSerialized]
	[DontSave]
	internal Vector3 lastSyncedPosition;

	[NonSerialized]
	[DontSave]
	internal int idleFrames;

	[NonSerialized]
	internal float currentSnapStartPercent;

	public Vector3 startControlPosition => startNode.TransformPoint(startControlLocalPosition);

	public Vector3 endControlPosition => endNode.TransformPoint(endControlLocalPosition);

	public Vector3 edge => endNode.position - startNode.position;

	public Vector3 segment => edge / (additionalSnapPointCount + 1);

	public Vector3 startPoint => startNode.position;

	public Vector3 endPoint => endNode.position;

	public float value
	{
		get
		{
			if (!isCurved)
			{
				return Mathf.Clamp01(Vector3.Dot(base.transform.position - startPoint, edge.normalized) / edge.magnitude);
			}
			Vector3 position = startNode.position;
			Vector3 p = startControlPosition;
			Vector3 p2 = endControlPosition;
			Vector3 position2 = endNode.position;
			return Maths.closestCubicBezierPercent(position, p, p2, position2, base.transform.position);
		}
	}

	public int closestSnapPointIndex => Mathf.RoundToInt(value * (float)(additionalSnapPointCount + 1));

	public override Game.ScreenTargetType getTargetType()
	{
		return Game.ScreenTargetType.Slidable;
	}

	protected override Game.PCCrosshair getBaseCursor()
	{
		return Game.PCCrosshair.Draggable;
	}

	public void init(Camera gizmoCamera)
	{
		this.gizmoCamera = gizmoCamera;
		soundEmitter = PineFmod.addSoundEmitter(base.gameObject, moveSound);
		setParentOfNodes(shouldSlidableBeParent: false);
	}

	public void setParentOfNodes(bool shouldSlidableBeParent)
	{
		if (shouldSlidableBeParent)
		{
			if (startNode != null)
			{
				startNode.SetParent(base.transform);
			}
			if (endNode != null)
			{
				endNode.SetParent(base.transform);
			}
		}
		else
		{
			if (startNode != null && isNodeChildOfSlidable(startNode))
			{
				startNode.SetParent(base.transform.parent);
			}
			if (endNode != null && isNodeChildOfSlidable(endNode))
			{
				endNode.SetParent(base.transform.parent);
			}
		}
	}

	public void initRuntimeSlidablePosition(int targetIndex)
	{
		setParentOfNodes(shouldSlidableBeParent: false);
		base.transform.position = startPoint + segment * targetIndex;
	}

	private bool isNodeChildOfSlidable(Transform node)
	{
		bool result = false;
		Transform parent = node.parent;
		while (parent != null)
		{
			if (parent == base.transform)
			{
				result = true;
				break;
			}
			parent = parent.parent;
		}
		return result;
	}

	public void setAtPercent(float percent)
	{
		base.transform.position = getPositionForPercent(percent);
		base.transform.rotation = getRotationForPercent(percent);
	}

	public Vector3 getPositionForPercent(float percent)
	{
		if (!isCurved)
		{
			return startPoint + Mathf.Clamp01(percent) * edge;
		}
		return getCurvePoint(percent);
	}

	public Quaternion getRotationForPercent(float percent)
	{
		if (!isCurved || !isTangent)
		{
			return base.transform.rotation;
		}
		return Quaternion.LookRotation(getCurveTangent(percent));
	}

	public Vector3 getCurvePoint(float t)
	{
		Vector3 position = startNode.position;
		Vector3 p = startControlPosition;
		Vector3 p2 = endControlPosition;
		Vector3 position2 = endNode.position;
		return Maths.cubicBezier(position, p, p2, position2, t);
	}

	public Vector3 getCurveTangent(float t)
	{
		Vector3 position = startNode.position;
		Vector3 p = startControlPosition;
		Vector3 p2 = endControlPosition;
		Vector3 position2 = endNode.position;
		return Maths.cubicBezierDerivative(position, p, p2, position2, t);
	}

	public override void save(FastBinaryWriter writer)
	{
		base.save(writer);
		writer.Write(in currentSnapStartPercent, default(FastBinaryWriter.ForPrimitives));
	}

	public override void load(FastBinaryReader reader)
	{
		base.load(reader);
		currentSnapStartPercent = reader.ReadSingle();
	}

	public override void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		base.read(reader, saveFileProperties);
		int position = reader.Position;
		float num = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "currentSnapStartPercent",
			fieldValue = $"{num}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
	}
}
