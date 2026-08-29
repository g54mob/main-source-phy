using System;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

[DisallowMultipleComponent]
public class HorizontalSlider : Interactive
{
	public enum Type
	{
		Circular = 0,
		Rectangular = 1
	}

	[Serializable]
	public struct CircleCoordinates
	{
		[Tooltip("If HorizontalSlider type is Circular this coordinate represents radius in range [0, 1], otherwise x in range [-1, 1]")]
		public float coordinate1;

		[Tooltip("If HorizontalSlider type is Circular this coordinate represents angle in range [0, 360], otherwise y in range [-1, 1]")]
		public float coordinate2;
	}

	[DontSave]
	public Type type;

	[Tooltip("If HorizontalSlider is of type Circular, defines the maximum radius distance the slider can be moved to")]
	[Min(0f)]
	[DontSave]
	public float circularMaxRadius = 1f;

	[Tooltip("If HorizontalSlider is of type Rectangular, defines the maximum (x,y) distance the slider can be moved to. Should not be negative values")]
	[DontSave]
	public Vector2 rectangularMaxSize = Vector3.one;

	[Tooltip("Defines if the slider is snapped back to the center on release")]
	[DontSave]
	public bool snapBack;

	[Tooltip("Defines the plane axes")]
	[DontSave]
	public Transform areaLimitsPlaneOrigin;

	[Tooltip("Defines the normal of the plane the object is sliding on")]
	[DontSave]
	public Vector3 localNormal = Vector3.up;

	[Header("Important!\nIf Circular: Coordinate1 represents Radius in range [0, 1], Coordinate2 represents Angle in range [0, 360];\nIf Rectangular: Coordinate1 represents X in range [-1, 1], Coordinate2 represents Y in range [-1, 1]")]
	[Tooltip("Defines the interests points on the plane for the slider")]
	[DontSave]
	public List<CircleCoordinates> interestPointCoordinates = new List<CircleCoordinates>();

	[DontSave]
	[HideInInspector]
	public List<Vector3> interestPointPositions = new List<Vector3>();

	[Tooltip("Defines the distance of the slider to the interest point where the point is considered hit ")]
	[Min(0f)]
	[DontSave]
	public float interestPointDistance = 0.1f;

	[Tooltip("Sound played when interaction with this HorizontalSlider starts.")]
	[DontSave]
	[HideInInspector]
	public EventReference startSound;

	[Tooltip("Sound played when this HorizontalSlider is being moved/dragged.")]
	[DontSave]
	[HideInInspector]
	public EventReference soundDrag;

	[Tooltip("Sound played when interaction with this HorizontalSlider ends.")]
	[DontSave]
	[HideInInspector]
	public EventReference endSound;

	public const float SNAP_DURATION = 0.25f;

	public const float SOUND_STOP_DELAY = 0.1f;

	[NonSerialized]
	public float soundStopTimer;

	[NonSerialized]
	public Vector3 offset;

	[NonSerialized]
	internal Vector3 currentSnapStartOffset;

	[NonSerialized]
	[DontSave]
	internal Vector3 lastSyncedPosition;

	[NonSerialized]
	[DontSave]
	public Vector3 originalPosition;

	[NonSerialized]
	[DontSave]
	public Vector3 originalLocalPosition;

	[NonSerialized]
	[DontSave]
	public Plane plane;

	[NonSerialized]
	[DontSave]
	public Vector3 baseScale;

	[NonSerialized]
	[DontSave]
	public Vector3 controllerOffset;

	public Vector3 worldNormal => base.transform.TransformDirection(localNormal).normalized;

	public Vector3 worldUp => Vector3.Cross(worldNormal, Vector3.Cross(Vector3.up, worldNormal).normalized).normalized;

	public Vector3 worldRight => Vector3.Cross(worldUp, worldNormal).normalized;

	public override Game.ScreenTargetType getTargetType()
	{
		return Game.ScreenTargetType.HorizontalSlider;
	}

	protected override Game.PCCrosshair getBaseCursor()
	{
		return Game.PCCrosshair.Draggable;
	}

	public override void init()
	{
		originalPosition = base.transform.position;
		originalLocalPosition = base.transform.localPosition;
		interestPointPositions = getPointPositions(originalPosition);
		soundEmitter = PineFmod.addSoundEmitter(base.gameObject, soundDrag);
		if (areaLimitsPlaneOrigin != null && areaLimitsPlaneOrigin.parent == base.transform)
		{
			areaLimitsPlaneOrigin.SetParent(base.transform.parent);
		}
		baseScale = base.transform.lossyScale;
	}

	public void initPlane()
	{
		plane = ((areaLimitsPlaneOrigin != null) ? new Plane(-areaLimitsPlaneOrigin.forward, areaLimitsPlaneOrigin.position) : new Plane(worldNormal, originalPosition));
	}

	public int getNearestInterestPoint()
	{
		int result = -1;
		float num = float.PositiveInfinity;
		for (int i = 0; i < interestPointPositions.Count; i++)
		{
			float num2 = Vector3.Distance(base.transform.position, interestPointPositions[i]);
			if (num2 <= interestPointDistance && num2 < num)
			{
				num = num2;
				result = i;
			}
		}
		return result;
	}

	private List<Vector3> getPointPositions(Vector3 center)
	{
		List<Vector3> list = new List<Vector3>();
		Vector3 vector = ((areaLimitsPlaneOrigin != null) ? areaLimitsPlaneOrigin.right : worldRight);
		Vector3 vector2 = ((areaLimitsPlaneOrigin != null) ? areaLimitsPlaneOrigin.up : worldUp);
		if (type == Type.Circular)
		{
			foreach (CircleCoordinates interestPointCoordinate in interestPointCoordinates)
			{
				float num = Mathf.Lerp(0f, circularMaxRadius, interestPointCoordinate.coordinate1);
				float f = interestPointCoordinate.coordinate2 * (MathF.PI / 180f);
				float num2 = num * Mathf.Cos(f);
				float num3 = num * Mathf.Sin(f);
				list.Add(center + vector * num2 + vector2 * num3);
			}
		}
		else if (type == Type.Rectangular)
		{
			foreach (CircleCoordinates interestPointCoordinate2 in interestPointCoordinates)
			{
				float num4 = Mathf.Lerp(0f, rectangularMaxSize.x, Mathf.Abs(interestPointCoordinate2.coordinate1)) * Mathf.Sign(interestPointCoordinate2.coordinate1);
				float num5 = Mathf.Lerp(0f, rectangularMaxSize.y, Mathf.Abs(interestPointCoordinate2.coordinate2)) * Mathf.Sign(interestPointCoordinate2.coordinate2);
				list.Add(center + vector * num4 + vector2 * num5);
			}
		}
		return list;
	}

	public void setLocalPosition(Vector3 localPosition)
	{
		offset = localPosition - originalLocalPosition;
		base.transform.localPosition = localPosition;
	}

	public override void save(FastBinaryWriter writer)
	{
		base.save(writer);
		writer.Write(in soundStopTimer, default(FastBinaryWriter.ForPrimitives));
		writer.WriteVector3(in offset);
		writer.WriteVector3(in currentSnapStartOffset);
	}

	public override void load(FastBinaryReader reader)
	{
		base.load(reader);
		soundStopTimer = reader.ReadSingle();
		offset = reader.ReadVector3();
		currentSnapStartOffset = reader.ReadVector3();
	}

	public override void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		base.read(reader, saveFileProperties);
		int position = reader.Position;
		float num = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "soundStopTimer",
			fieldValue = $"{num}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Vector3 vector = reader.ReadVector3();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "offset",
			fieldValue = $"{vector}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Vector3 vector2 = reader.ReadVector3();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "currentSnapStartOffset",
			fieldValue = $"{vector2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
	}
}
