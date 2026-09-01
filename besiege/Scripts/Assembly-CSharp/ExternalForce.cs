using System;
using UnityEngine;

[AddComponentMenu("Physics/External Force")]
public class ExternalForce : MonoBehaviour
{
	public enum FoO
	{
		x = 0,
		y = 1,
		z = 2
	}

	protected ExternalForceObject[] EFOArray;

	protected int ExternalForceObjectCount;

	protected Matrix4x4 worldMatrix;

	protected Vector3 zero = Vector3.zero;

	public float accuracy = 0.5f;

	public FoO frontOfObject = FoO.z;

	public bool frontInverted;

	public bool SimPhysics
	{
		get
		{
			return (!StatMaster.isClient) ? StatMaster.levelSimulating : StatMaster.isLocalSim;
		}
	}

	public bool isSimulating
	{
		get
		{
			return StatMaster.levelSimulating;
		}
	}

	protected virtual void Start()
	{
		if (isSimulating)
		{
			accuracy *= accuracy;
		}
	}

	protected void UpdateTargets()
	{
		ExternalForceObjectCount = 0;
	}

	protected ExternalForceObject AddEFO(Vector3 pos, BasicInfo basic, ForceMode forceMode, float powerScale)
	{
		return AddEFO(pos, basic, Vector3.zero, forceMode, powerScale, true);
	}

	protected ExternalForceObject AddEFO(Vector3 pos, BasicInfo basic, Vector3 normalVelocity, ForceMode forceMode, float powerScale)
	{
		return AddEFO(pos, basic, normalVelocity, forceMode, powerScale, true);
	}

	protected ExternalForceObject AddEFO(Vector3 pos, BasicInfo basic, Vector3 normalVelocity, ForceMode forceMode, float powerScale, bool dontCompare)
	{
		ExternalForceObjectCount++;
		if (ExternalForceObjectCount > EFOArray.Length - 1)
		{
			ExpandArray();
		}
		ExternalForceObject externalForceObject = EFOArray[ExternalForceObjectCount - 1];
		if (object.ReferenceEquals(externalForceObject, null))
		{
			EFOArray[ExternalForceObjectCount - 1] = new ExternalForceObject(pos, basic, normalVelocity, forceMode, powerScale, dontCompare);
		}
		else
		{
			externalForceObject.Replace(pos, basic, normalVelocity, forceMode, powerScale, dontCompare);
		}
		return EFOArray[ExternalForceObjectCount - 1];
	}

	protected virtual void CheckIfFormost(Vector3 pos, BasicInfo basic)
	{
		AddEFOVelSpace(pos, basic, ForceMode.Force, 1f);
	}

	internal virtual void AddEFOVelSpace(Vector3 pos, BasicInfo basic, ForceMode forceMode, float powerScale)
	{
		if (ExternalForceObjectCount > EFOArray.Length - 1)
		{
			ExpandArray();
		}
		float num = 0f;
		float num2 = 0f;
		bool flag = false;
		for (int i = 0; i <= ExternalForceObjectCount; i++)
		{
			ExternalForceObject externalForceObject = EFOArray[i];
			if (i == ExternalForceObjectCount)
			{
				if (object.ReferenceEquals(externalForceObject, null))
				{
					EFOArray[i] = new ExternalForceObject(pos, basic, zero, forceMode, powerScale, false);
					EFOArray[i].dragScale = 1f;
				}
				else
				{
					externalForceObject.dragScale = 0f;
					externalForceObject.Replace(pos, basic, zero, forceMode, powerScale, false);
					externalForceObject.dragScale = 1f;
				}
				ExternalForceObjectCount++;
				break;
			}
			if (externalForceObject.dontCompare)
			{
				externalForceObject.dragScale = 0f;
				continue;
			}
			if (basic.infoType == BasicInfo.BasicInfoType.Block && !externalForceObject.basicInfo.transform.IsChildOf(basic.ParentMachine.SimulationMachine))
			{
				externalForceObject.dragScale = 0f;
				continue;
			}
			Vector3 position = externalForceObject.position;
			switch (frontOfObject)
			{
			case FoO.x:
				num = position.y - pos.y;
				num2 = position.z - pos.z;
				break;
			case FoO.y:
				num = position.x - pos.x;
				num2 = position.z - pos.z;
				break;
			case FoO.z:
				num = position.x - pos.x;
				num2 = position.y - pos.y;
				break;
			}
			float num3 = num * num + num2 * num2;
			if (num3 < accuracy)
			{
				switch (frontOfObject)
				{
				case FoO.x:
					flag = ((!frontInverted) ? (position.x > pos.x) : (position.x < pos.x));
					break;
				case FoO.y:
					flag = ((!frontInverted) ? (position.y > pos.y) : (position.y < pos.y));
					break;
				case FoO.z:
					flag = ((!frontInverted) ? (position.z > pos.z) : (position.z < pos.z));
					break;
				}
				if (flag)
				{
					externalForceObject.dragScale = 0f;
					externalForceObject.Replace(pos, basic, zero, forceMode, powerScale, false);
					externalForceObject.dragScale = 1f;
				}
				externalForceObject.dragScale = 0f;
				break;
			}
			externalForceObject.dragScale = 1f;
		}
	}

	protected void ExpandArray()
	{
		ExternalForceObject[] array = new ExternalForceObject[EFOArray.Length + 10];
		Array.Copy(EFOArray, array, EFOArray.Length);
		EFOArray = array;
	}

	protected virtual bool ValidateEFO(BasicInfo b)
	{
		if (object.ReferenceEquals(b, null) || b.isDestroyed || !b.isSimulating || b.noRigidbody || b.isKinematic)
		{
			return false;
		}
		return true;
	}

	protected bool intersectRect(Vector3 r1max, Vector3 r1min, Vector3 r2max, Vector3 r2min)
	{
		return r2min.x < r1max.x && r2max.x > r1min.x && r2min.y < r1max.y && r2max.y > r1min.y && r2min.z < r1max.z && r2max.z > r1min.z;
	}

	protected bool TruncatedSquareContains(Vector3 Bmax, Vector3 Bmin, Vector3 max, Vector3 min, Vector3 point)
	{
		if (!ContainsPoint(Bmax, Bmin, point))
		{
			return false;
		}
		if (min.x <= point.x && point.x <= max.x && min.z <= point.z && point.z <= max.z)
		{
			return true;
		}
		float num = point.y / Bmax.y * (Bmax.x - max.x) + max.x;
		float num2 = 0f - num;
		return num2 <= point.x && point.x <= num && num2 <= point.z && point.z <= num;
	}

	protected bool ContainsPoint(Vector3 max, Vector3 min, Vector3 point)
	{
		return min.x <= point.x && point.x <= max.x && min.y <= point.y && point.y <= max.y && min.z <= point.z && point.z <= max.z;
	}

	protected Vector3 NormalizeVector(float length, Vector3 v)
	{
		return new Vector3(v.x / length, v.y / length, v.z / length);
	}

	protected Quaternion fromtwovectors(Vector3 u, Vector3 v)
	{
		return Quaternion.FromToRotation(u, v);
	}
}
