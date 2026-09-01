using System.Collections;
using UnityEngine;

public class ExternalForceWater : ExternalForce
{
	[Header("Formost Check")]
	public float velocityAngleLimit = 0.7f;

	private ExternalForceObject EFO;

	private Vector3 bVel;

	private float dist;

	public bool advancedAreoDynamics;

	public float advancedBasePercentageDrag = 0.25f;

	[HideInInspector]
	public ExternalForceObject[] dragAffectedBlocks = new ExternalForceObject[0];

	protected int dragArrayIndex;

	private Vector3 up = Vector3.up;

	protected override void Start()
	{
		base.Start();
	}

	internal override void AddEFOVelSpace(Vector3 pos, BasicInfo basic, ForceMode forceMode, float powerScale)
	{
		bVel = basic.Rigidbody.velocity.normalized;
		bool flag = basic.infoType == BasicInfo.BasicInfoType.Block && (basic as BlockBehaviour).isParented;
		EFO = AddEFO(pos, basic, bVel, forceMode, powerScale, flag);
		EFO.CounterDrag = 1f;
		if (!advancedAreoDynamics || flag)
		{
			return;
		}
		if (!EFO.basicInfo.hasMultipleBounds)
		{
			EFO.velRot = Quaternion.FromToRotation(bVel, up);
			AddEFOtoAerodynamicCheck(EFO.basicInfo.Rigidbody.rotation * ((!EFO.basicInfo.gotBounds) ? EFO.basicInfo.DefaultBounds.extents : EFO.basicInfo.defaultExtents));
			return;
		}
		Quaternion rotation = EFO.basicInfo.Rigidbody.rotation;
		Quaternion velRot = Quaternion.FromToRotation(bVel, up);
		for (int i = 0; i < EFO.basicInfo.DefaultBoundsArray.Length; i++)
		{
			Bounds bounds = EFO.basicInfo._defaultBoundsArray[i];
			EFO = new ExternalForceObject(rotation * bounds.center + EFO.basicInfo.GetCenter(), basic, bVel, forceMode, powerScale, flag);
			EFO.CounterDrag = 1f;
			EFO.boundsNumber = i;
			EFO.velRot = velRot;
			AddEFOtoAerodynamicCheck(rotation * bounds.extents);
		}
	}

	internal void ClientAddEFOVelSpace(Vector3 pos, BasicInfo basic, ForceMode forceMode, float powerScale)
	{
		bVel = basic.NetBlock.Velocity.normalized;
		bool flag = basic.infoType == BasicInfo.BasicInfoType.Block && (basic as BlockBehaviour).isParented;
		EFO = AddEFO(pos, basic, bVel, forceMode, powerScale, flag);
		EFO.CounterDrag = 1f;
		if (!advancedAreoDynamics || flag)
		{
			return;
		}
		if (!EFO.basicInfo.hasMultipleBounds)
		{
			EFO.velRot = Quaternion.FromToRotation(bVel, up);
			AddEFOtoAerodynamicCheck(EFO.basicInfo.transform.rotation * ((!EFO.basicInfo.gotBounds) ? EFO.basicInfo.DefaultBounds.extents : EFO.basicInfo.defaultExtents));
			return;
		}
		Quaternion rotation = EFO.basicInfo.transform.rotation;
		Quaternion velRot = Quaternion.FromToRotation(bVel, up);
		for (int i = 0; i < EFO.basicInfo.DefaultBoundsArray.Length; i++)
		{
			Bounds bounds = EFO.basicInfo._defaultBoundsArray[i];
			EFO = new ExternalForceObject(rotation * bounds.center + EFO.basicInfo.GetCenter(), basic, bVel, forceMode, powerScale, flag);
			EFO.CounterDrag = 1f;
			EFO.boundsNumber = i;
			EFO.velRot = velRot;
			AddEFOtoAerodynamicCheck(rotation * bounds.extents);
		}
	}

	private void AddEFOtoAerodynamicCheck(Vector3 boundsExtents)
	{
		EFO.extendRotated = boundsExtents;
		EFO.extentLength = EFO.basicInfo.extentLength;
		if (dragArrayIndex < dragAffectedBlocks.Length)
		{
			dragAffectedBlocks[dragArrayIndex] = EFO;
			dragArrayIndex++;
		}
	}

	protected IEnumerator AdvancedAreodynamicCheck()
	{
		Compare();
		dragArrayIndex = 0;
		yield break;
	}

	internal void Compare()
	{
		Vector3 vector2 = default(Vector3);
		for (int i = 0; i < dragArrayIndex; i++)
		{
			ExternalForceObject externalForceObject = dragAffectedBlocks[i];
			if (externalForceObject == null || externalForceObject.dontCompare)
			{
				continue;
			}
			Vector3 vector = externalForceObject.velRot * externalForceObject.extendRotated;
			float num = ((!(vector.x < 0f)) ? vector.x : (0f - vector.x));
			float num2 = ((!(vector.z < 0f)) ? vector.z : (0f - vector.z));
			Color color = Color.Lerp((Color.yellow + Color.cyan) * 0.5f, Color.white, (float)i / (float)dragArrayIndex);
			color.a = 1f;
			bVel = externalForceObject.velNormal;
			for (int j = 0; j < dragArrayIndex; j++)
			{
				ExternalForceObject externalForceObject2 = dragAffectedBlocks[j];
				if (i == j || externalForceObject2 == null || externalForceObject2.dontCompare || (StatMaster.isMP && externalForceObject.playerID != externalForceObject2.playerID) || externalForceObject2.CounterDrag == 0f || externalForceObject2.dragScale == externalForceObject.dragScale * 0.1f)
				{
					continue;
				}
				vector2.x = externalForceObject2.position.x - externalForceObject.position.x;
				vector2.y = externalForceObject2.position.y - externalForceObject.position.y;
				vector2.z = externalForceObject2.position.z - externalForceObject.position.z;
				dist = vector2.x * vector2.x + vector2.y * vector2.y + vector2.z * vector2.z;
				if (dist > (externalForceObject2.extentLength + externalForceObject.extentLength) * (externalForceObject2.extentLength + externalForceObject.extentLength) || Vector3.Dot(externalForceObject2.velNormal, bVel) < velocityAngleLimit)
				{
					continue;
				}
				vector2 = externalForceObject.velRot * vector2;
				if (vector2.y > 0f)
				{
					continue;
				}
				float num3 = ((!(vector2.x < 0f)) ? vector2.x : (0f - vector2.x));
				float num4 = ((!(vector2.z < 0f)) ? vector2.z : (0f - vector2.z));
				Vector3 vector3 = externalForceObject.velRot * externalForceObject2.extendRotated;
				float num5 = ((!(vector3.x < 0f)) ? vector3.x : (0f - vector3.x));
				float num6 = ((!(vector3.z < 0f)) ? vector3.z : (0f - vector3.z));
				float num7 = (num + num5) * 0.8f;
				float num8 = (num2 + num6) * 0.8f;
				float num9 = (num7 - num3) / num5;
				float num10 = (num8 - num4) / num6;
				if (!(num9 < -1f) && !(num10 < -1f))
				{
					float num11 = ((!(num9 < 0f) && !(num10 < 0f)) ? (Mathf.Clamp01(num9) * Mathf.Clamp01(num10)) : 0f);
					float num12 = 0f;
					num12 = Mathf.Clamp(num12 + num11, 0f, 0.9f) * externalForceObject2.velNormal.sqrMagnitude;
					float num13 = Mathf.Clamp01(externalForceObject2.dragScale - num12);
					if (num13 < externalForceObject2.dragScale)
					{
						externalForceObject2.basicInfo.dragScale = (externalForceObject2.dragScale = num13);
					}
				}
			}
		}
	}
}
