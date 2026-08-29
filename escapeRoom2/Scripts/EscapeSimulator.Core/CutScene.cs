using System;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[AddComponentMenu("Pine/CutScene")]
public class CutScene : MonoBehaviour, ISaveable
{
	public const float PLAYER_HEIGHT = 1.6f;

	public int priority;

	public bool playerHasControl;

	public bool playerRotationFollowingCutscene;

	[NonSerialized]
	public float cutsceneStartTimestamp;

	public bool useIntroTween;

	public float introTweenDutation = 0.5f;

	public bool useExitTween;

	public float exitTweenDutation = 0.5f;

	[DontSave]
	public LayerMask customCameraCullingMask;

	public Vector3 eyePosition => base.transform.position;

	public Quaternion eyeRotation => base.transform.rotation;

	public virtual void save(FastBinaryWriter writer)
	{
		writer.Write(in priority, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in playerHasControl, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in playerRotationFollowingCutscene, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in cutsceneStartTimestamp, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in useIntroTween, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in introTweenDutation, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in useExitTween, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in exitTweenDutation, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void load(FastBinaryReader reader)
	{
		priority = reader.ReadInt32();
		playerHasControl = reader.ReadBoolean();
		playerRotationFollowingCutscene = reader.ReadBoolean();
		cutsceneStartTimestamp = reader.ReadSingle();
		useIntroTween = reader.ReadBoolean();
		introTweenDutation = reader.ReadSingle();
		useExitTween = reader.ReadBoolean();
		exitTweenDutation = reader.ReadSingle();
	}

	public virtual void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		int position = reader.Position;
		int num = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "priority",
			fieldValue = $"{num}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "playerHasControl",
			fieldValue = $"{flag}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag2 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "playerRotationFollowingCutscene",
			fieldValue = $"{flag2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num2 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "cutsceneStartTimestamp",
			fieldValue = $"{num2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag3 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "useIntroTween",
			fieldValue = $"{flag3}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num3 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "introTweenDutation",
			fieldValue = $"{num3}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag4 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "useExitTween",
			fieldValue = $"{flag4}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num4 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "exitTweenDutation",
			fieldValue = $"{num4}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
	}
}
