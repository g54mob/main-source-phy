using UnityEngine;

public static class Extensions
{
	public static Vector3 RotateAroundPivot(this Vector3 point, Vector3 pivot, Vector3 axis)
	{
		return Vector3Util.RotatePointAroundPivot(point, pivot, axis);
	}

	public static void SetLayerRecursively(this GameObject gameObject, int newLayer)
	{
		if (!gameObject)
		{
			return;
		}
		gameObject.layer = newLayer;
		foreach (Transform item in gameObject.transform)
		{
			item.gameObject.SetLayerRecursively(newLayer);
		}
	}

	public static T[] Slice<T>(this T[] source, int start, int end)
	{
		if (end < 0)
		{
			end = source.Length + end;
		}
		int num = end - start;
		T[] array = new T[num];
		for (int i = 0; i < num; i++)
		{
			array[i] = source[i + start];
		}
		return array;
	}

	public static void CopyJoint(this ConfigurableJoint to, ConfigurableJoint from)
	{
		to.anchor = from.anchor;
		to.axis = from.axis;
		to.secondaryAxis = from.secondaryAxis;
		to.angularXMotion = from.angularXMotion;
		to.angularYMotion = from.angularYMotion;
		to.angularZMotion = from.angularZMotion;
		to.xMotion = from.xMotion;
		to.yMotion = from.yMotion;
		to.zMotion = from.zMotion;
		to.projectionMode = from.projectionMode;
		to.projectionDistance = from.projectionDistance;
		to.projectionAngle = from.projectionAngle;
		to.breakForce = from.breakForce;
		to.breakTorque = from.breakTorque;
		to.enablePreprocessing = from.enablePreprocessing;
	}

	public static void CopyJoint(this CharacterJoint to, CharacterJoint from)
	{
		to.anchor = from.anchor;
		to.axis = from.axis;
		to.swingAxis = from.swingAxis;
		to.twistLimitSpring = from.twistLimitSpring;
		to.highTwistLimit = from.highTwistLimit;
		to.lowTwistLimit = from.lowTwistLimit;
		to.swingLimitSpring = from.swingLimitSpring;
		to.swing1Limit = from.swing1Limit;
		to.swing2Limit = from.swing2Limit;
		to.enableProjection = from.enableProjection;
		to.projectionDistance = from.projectionDistance;
		to.projectionAngle = from.projectionAngle;
		to.breakForce = from.breakForce;
		to.breakTorque = from.breakTorque;
	}
}
