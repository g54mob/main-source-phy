using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

[DisallowMultipleComponent]
[AddComponentMenu("Pine/Zoomable")]
[HelpURL("https://youtu.be/j77NczDcsIc")]
public class Zoomable : Interactive
{
	[DontSave]
	public bool disableInVr;

	[DontSave]
	public bool enableInVrEditor = true;

	[DontSave]
	public Slot[] autoInsertSlot;

	[DontSave]
	public float horizontalOffsetRatioVr = 0.65f;

	[DontSave]
	public float verticalOffsetVr;

	[DontSave]
	public Vector3 afterOffsetVr;

	[DontSave]
	public Vector3 afterOffsetRotationVr;

	[DontSave]
	public float scaleModifierVR = 1f;

	[DontSave]
	[HideInInspector]
	public Vector3 eyeLocalPosition;

	[DontSave]
	[HideInInspector]
	public Vector3 eyeLocalRotation;

	[DontSave]
	public bool scrollableZoom;

	[DontSave]
	public float minZoomIn;

	[DontSave]
	public float maxZoomIn;

	[DontSave]
	public Vector2 scrollableZoomSize = new Vector2(10f, 5f);

	[DontSave]
	public HDAdditionalCameraData.AntialiasingMode antialiasingMode = HDAdditionalCameraData.AntialiasingMode.SubpixelMorphologicalAntiAliasing;

	public Vector3 zoomableEyePosition
	{
		get
		{
			return calculateEyePosition(base.transform, eyeLocalPosition);
		}
		set
		{
			eyeLocalPosition = base.transform.InverseTransformPoint(value);
		}
	}

	public Quaternion zoomableEyeRotation
	{
		get
		{
			return calculateEyeRotation(base.transform, eyeLocalRotation);
		}
		set
		{
			eyeLocalRotation = (Quaternion.Inverse(base.transform.rotation) * value).eulerAngles;
		}
	}

	protected override Game.PCCrosshair getBaseCursor()
	{
		return Game.PCCrosshair.Zoomable;
	}

	public override Game.ScreenTargetType getTargetType()
	{
		return Game.ScreenTargetType.Zoomable;
	}

	public static Vector3 calculateEyePosition(Transform t, Vector3 eyeLocalPosition)
	{
		Vector3 lossyScale = t.lossyScale;
		Vector3 vector = t.right * (eyeLocalPosition.x * lossyScale.x);
		Vector3 vector2 = t.up * (eyeLocalPosition.y * lossyScale.y);
		Vector3 vector3 = t.forward * (eyeLocalPosition.z * lossyScale.z);
		return t.position + vector + vector2 + vector3;
	}

	public static Quaternion calculateEyeRotation(Transform t, Vector3 eyeLocalRotation)
	{
		return t.rotation * Quaternion.Euler(eyeLocalRotation);
	}

	public override void save(FastBinaryWriter writer)
	{
		base.save(writer);
	}

	public override void load(FastBinaryReader reader)
	{
		base.load(reader);
	}

	public override void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		base.read(reader, saveFileProperties);
	}
}
