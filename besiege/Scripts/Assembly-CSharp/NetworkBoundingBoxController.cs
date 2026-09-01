using System;
using UnityEngine;

public class NetworkBoundingBoxController : BoundingBoxController
{
	private PlayerData player;

	public bool boundingEnabled = true;

	public Transform scaleTransform;

	private Transform buildZoneTransform;

	private Vector3 worldExtents;

	private Vector3 worldCenter;

	protected override void Awake()
	{
		base.Awake();
		scaleTransform = base.transform;
		ReferenceMaster.onLevelEditorEnvironmentChanged = (Action<LevelSettings.LevelEnvironment>)Delegate.Combine(ReferenceMaster.onLevelEditorEnvironmentChanged, new Action<LevelSettings.LevelEnvironment>(UpdateEnvironment));
		LevelEditor.WaterHeightUpdated = (Action<float>)Delegate.Combine(LevelEditor.WaterHeightUpdated, new Action<float>(SetWaterHeight));
	}

	protected void OnDestroy()
	{
		ReferenceMaster.onLevelEditorEnvironmentChanged = (Action<LevelSettings.LevelEnvironment>)Delegate.Remove(ReferenceMaster.onLevelEditorEnvironmentChanged, new Action<LevelSettings.LevelEnvironment>(UpdateEnvironment));
		LevelEditor.WaterHeightUpdated = (Action<float>)Delegate.Remove(LevelEditor.WaterHeightUpdated, new Action<float>(SetWaterHeight));
	}

	public override void Init()
	{
		UpdateEnvironment(LevelEditor.Instance.environmentManager.currentEnv);
	}

	protected void UpdateEnvironment(LevelSettings.LevelEnvironment e)
	{
		if (e == LevelSettings.LevelEnvironment.Water)
		{
			SetToWaterVariant();
		}
		else
		{
			SetToDefaultVariant();
		}
		NetworkAddPiece instance = NetworkAddPiece.Instance;
		if (instance.boundVisCode != null)
		{
			instance.boundVisCode.SetFloorPos(StatMaster.Bounding.Enabled);
		}
	}

	public void SetPlayer(PlayerData data)
	{
		player = data;
		playFadeAudio = player.machine.isLocalMachine;
	}

	private ZoneRotationMode CalculateRotationMode(Transform zoneTransform)
	{
		Vector3 eulerAngles = zoneTransform.eulerAngles;
		float num;
		for (num = Mathf.Abs(eulerAngles.x); num > 90f; num -= 90f)
		{
		}
		num = Mathf.Abs(num);
		float num2;
		for (num2 = Mathf.Abs(eulerAngles.y); num2 > 90f; num2 -= 90f)
		{
		}
		num2 = Mathf.Abs(num2);
		float num3;
		for (num3 = Mathf.Abs(eulerAngles.z); num3 > 90f; num3 -= 90f)
		{
		}
		num3 = Mathf.Abs(num3);
		float num4 = 0.05f;
		float num5 = 90f - num4;
		if ((num < num4 || num > num5) && (num2 < num4 || num2 > num5) && (num3 < num4 || num3 > num5))
		{
			return ZoneRotationMode.Normal;
		}
		while (num > 45f)
		{
			num -= 45f;
		}
		while (num2 > 45f)
		{
			num2 -= 45f;
		}
		while (num3 > 45f)
		{
			num3 -= 45f;
		}
		num5 = 45f - num4;
		if ((num < num4 || num > num5) && (num2 < num4 || num2 > num5) && (num3 < num4 || num3 > num5))
		{
			return ZoneRotationMode.NoWorldClamp;
		}
		return ZoneRotationMode.Custom;
	}

	public override bool Check(Machine machine, bool renewBounds)
	{
		if (!machine.isLocalMachine)
		{
			ResetRenders();
			return false;
		}
		return BoundCheck(machine.GetBounds(renewBounds));
	}

	public Vector3[] GetGlobalBoundPoints(Bounds bounds)
	{
		Vector3[] array = new Vector3[8];
		float num = 5.05f;
		array[0] = buildZoneTransform.TransformPoint(bounds.min.x, bounds.min.y - num, bounds.min.z);
		array[1] = buildZoneTransform.TransformPoint(bounds.min.x, bounds.min.y - num, bounds.max.z);
		array[2] = buildZoneTransform.TransformPoint(bounds.min.x, bounds.max.y - num, bounds.min.z);
		array[3] = buildZoneTransform.TransformPoint(bounds.max.x, bounds.min.y - num, bounds.min.z);
		array[4] = buildZoneTransform.TransformPoint(bounds.max.x, bounds.min.y - num, bounds.max.z);
		array[5] = buildZoneTransform.TransformPoint(bounds.max.x, bounds.max.y - num, bounds.min.z);
		array[6] = buildZoneTransform.TransformPoint(bounds.min.x, bounds.max.y - num, bounds.max.z);
		array[7] = buildZoneTransform.TransformPoint(bounds.max.x, bounds.max.y - num, bounds.max.z);
		return array;
	}

	public override void UpdateVis()
	{
		if (machine.isLocalMachine)
		{
			base.UpdateVis();
		}
	}

	public override bool BoundCheck(Bounds bounds)
	{
		if (bounds.size == Vector3.zero)
		{
			ResetRenders();
			return false;
		}
		StatMaster.Bounding.inLeftWall = bounds.min.x < StatMaster.Bounding.leftPos;
		StatMaster.Bounding.inRightWall = bounds.max.x > StatMaster.Bounding.rightPos;
		StatMaster.Bounding.inGround = bounds.min.y < StatMaster.Bounding.floorPos;
		StatMaster.Bounding.inRoof = bounds.max.y > StatMaster.Bounding.roofHeight;
		StatMaster.Bounding.inBackWall = bounds.min.z < StatMaster.Bounding.backPos;
		StatMaster.Bounding.inFrontWall = bounds.max.z > StatMaster.Bounding.frontPos;
		if (StatMaster.Bounding.zoneRotationMode != ZoneRotationMode.Normal)
		{
			if (!StatMaster.Bounding.Enabled)
			{
				StatMaster.Bounding.inLeftWall = (StatMaster.Bounding.inRightWall = (StatMaster.Bounding.inGround = (StatMaster.Bounding.inRoof = (StatMaster.Bounding.inBackWall = (StatMaster.Bounding.inFrontWall = false)))));
			}
			Vector3[] globalBoundPoints = GetGlobalBoundPoints(bounds);
			float num = worldCenter.x - worldExtents.x;
			float num2 = worldCenter.x + worldExtents.x;
			float num3 = worldCenter.z + worldExtents.z;
			float num4 = worldCenter.z - worldExtents.z;
			float num5 = worldCenter.y - worldExtents.y;
			float num6 = worldCenter.y + worldExtents.y;
			for (int i = 0; i < globalBoundPoints.Length; i++)
			{
				Vector3 vector = globalBoundPoints[i];
				StatMaster.Bounding.inLeftWall = vector.x < num || StatMaster.Bounding.inLeftWall;
				StatMaster.Bounding.inRightWall = vector.x > num2 || StatMaster.Bounding.inRightWall;
				StatMaster.Bounding.inGround = vector.y < num5 || StatMaster.Bounding.inGround;
				StatMaster.Bounding.inRoof = vector.y > num6 || StatMaster.Bounding.inRoof;
				StatMaster.Bounding.inBackWall = vector.z < num4 || StatMaster.Bounding.inBackWall;
				StatMaster.Bounding.inFrontWall = vector.z > num3 || StatMaster.Bounding.inFrontWall;
			}
		}
		UpdateVis();
		bool flag = StatMaster.Bounding.inRoof || StatMaster.Bounding.inGround || StatMaster.Bounding.inRightWall || StatMaster.Bounding.inLeftWall || StatMaster.Bounding.inFrontWall || StatMaster.Bounding.inBackWall;
		addPiece.SetOutOfBounds(flag);
		return flag;
	}

	public void RemoteToggleBounds(bool toggle)
	{
		if (boundingEnabled != toggle)
		{
			ToggleBoundsVisual(toggle);
			boundingEnabled = toggle;
		}
	}

	public override void SetFloorPos(bool toggle)
	{
		if (player == null || player.buildZone == null)
		{
			return;
		}
		buildZoneTransform = player.buildZone.transform;
		float num = 5.05f;
		StatMaster.Bounding.zoneRotationMode = CalculateRotationMode(buildZoneTransform);
		float a = buildZoneTransform.InverseTransformPoint(floorPos.position).y + num;
		float num2 = buildZoneTransform.InverseTransformPoint(roofPos.position).y + num;
		float z = buildZoneTransform.InverseTransformPoint(frontPos.position).z;
		float z2 = buildZoneTransform.InverseTransformPoint(backPos.position).z;
		float x = buildZoneTransform.InverseTransformPoint(leftPos.position).x;
		float x2 = buildZoneTransform.InverseTransformPoint(rightPos.position).x;
		if (StatMaster.Bounding.zoneRotationMode == ZoneRotationMode.Normal)
		{
			worldCenter = buildZoneTransform.InverseTransformPoint(StatMaster.Bounding.worldCenter);
			worldCenter.y += num;
			worldExtents = buildZoneTransform.InverseTransformVector(StatMaster.Bounding.worldExtents);
			float num3 = ((!(worldExtents.x > 0f)) ? (0f - worldExtents.x) : worldExtents.x);
			float num4 = ((!(worldExtents.y > 0f)) ? (0f - worldExtents.y) : worldExtents.y);
			float num5 = ((!(worldExtents.z > 0f)) ? (0f - worldExtents.z) : worldExtents.z);
			StatMaster.Bounding.floorPos = worldCenter.y - num4;
			StatMaster.Bounding.roofHeight = worldCenter.y + num4;
			StatMaster.Bounding.frontPos = worldCenter.z + num5;
			StatMaster.Bounding.backPos = worldCenter.z - num5;
			StatMaster.Bounding.leftPos = worldCenter.x - num3;
			StatMaster.Bounding.rightPos = worldCenter.x + num3;
			if (toggle)
			{
				StatMaster.Bounding.floorPos = Mathf.Max(a, StatMaster.Bounding.floorPos);
				StatMaster.Bounding.roofHeight = Mathf.Min(num2, StatMaster.Bounding.roofHeight);
				StatMaster.Bounding.frontPos = Mathf.Min(z, StatMaster.Bounding.frontPos);
				StatMaster.Bounding.backPos = Mathf.Max(z2, StatMaster.Bounding.backPos);
				StatMaster.Bounding.leftPos = Mathf.Max(x, StatMaster.Bounding.leftPos);
				StatMaster.Bounding.rightPos = Mathf.Min(x2, StatMaster.Bounding.rightPos);
			}
		}
		else
		{
			worldCenter = StatMaster.Bounding.worldCenter;
			worldExtents = StatMaster.Bounding.worldExtents;
			StatMaster.Bounding.floorPos = a;
			StatMaster.Bounding.roofHeight = num2;
			StatMaster.Bounding.frontPos = z;
			StatMaster.Bounding.backPos = z2;
			StatMaster.Bounding.leftPos = x;
			StatMaster.Bounding.rightPos = x2;
		}
		SetWaterHeight();
	}

	private void SetWaterHeight(float h)
	{
		SetWaterHeight();
	}
}
