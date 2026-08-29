using System;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

[DisallowMultipleComponent]
[AddComponentMenu("Pine/CharacterPose")]
public class CharacterPose : Interactive
{
	public enum UsableZoom
	{
		None = 0,
		AllZooms = 1,
		List = 2
	}

	[DontSave]
	public Transform poseMetarig;

	[Tooltip("Defines the position and look direction (z axis) of the player when leaving pose")]
	[DontSave]
	public Transform exitPointTransform;

	[Tooltip("(Min X, Max X); Constraints should never be less than -180 or bigger than 180")]
	[DontSave]
	public Vector2 rotationConstraintsX = new Vector2(-90f, 90f);

	[Tooltip("(Min Y, Max Y); Constraints should never be less than -180 or bigger than 180")]
	[DontSave]
	public Vector2 rotationConstraintsY = new Vector2(-90f, 90f);

	[DontSave]
	public bool canInteractWithEnviroment = true;

	[DontSave]
	public bool canDropAndThrowItems = true;

	public bool lockedInPose;

	[DontSave]
	public bool cursorVisible;

	[DontSave]
	public bool exitWithW;

	[DontSave]
	public UsableZoom usableZoom;

	[DontSave]
	public Zoomable[] usableZoomList;

	[Tooltip("Sound played when entering CharacterPose")]
	[DontSave]
	[HideInInspector]
	public EventReference enterSound;

	[Tooltip("Sound played when exiting CharacterPose")]
	[DontSave]
	[HideInInspector]
	public EventReference exitSound;

	[DontSave]
	[HideInInspector]
	public Vector3 eyeLocalPosition;

	[DontSave]
	[HideInInspector]
	public Vector3 eyeLocalRotation;

	[DontSave]
	public Avatar avatarHuman;

	[DontSave]
	public Transform skeletonRoot;

	[DontSave]
	[HideInInspector]
	public HumanPoseHandler humanPoseHandler;

	public new Vector3 eyePosition
	{
		get
		{
			Transform obj = base.transform;
			Vector3 lossyScale = obj.lossyScale;
			Vector3 vector = obj.right * (eyeLocalPosition.x * lossyScale.x);
			Vector3 vector2 = obj.up * (eyeLocalPosition.y * lossyScale.y);
			Vector3 vector3 = obj.forward * (eyeLocalPosition.z * lossyScale.z);
			return obj.position + vector + vector2 + vector3;
		}
		set
		{
			eyeLocalPosition = base.transform.InverseTransformPoint(value);
		}
	}

	public new Quaternion eyeRotation
	{
		get
		{
			return base.transform.rotation * Quaternion.Euler(eyeLocalRotation);
		}
		set
		{
			eyeLocalRotation = (Quaternion.Inverse(base.transform.rotation) * value).eulerAngles;
		}
	}

	public override Game.ScreenTargetType getTargetType()
	{
		return Game.ScreenTargetType.CharacterPose;
	}

	protected override Game.PCCrosshair getBaseCursor()
	{
		return Game.PCCrosshair.Use;
	}

	public override void init()
	{
		try
		{
			humanPoseHandler = new HumanPoseHandler(avatarHuman, skeletonRoot);
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
		}
	}

	public override void save(FastBinaryWriter writer)
	{
		base.save(writer);
		writer.Write(in lockedInPose, default(FastBinaryWriter.ForPrimitives));
	}

	public override void load(FastBinaryReader reader)
	{
		base.load(reader);
		lockedInPose = reader.ReadBoolean();
	}

	public override void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		base.read(reader, saveFileProperties);
		int position = reader.Position;
		bool flag = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "lockedInPose",
			fieldValue = $"{flag}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
	}
}
