using UnityEngine;

public static class WilhelmPhysicsFunctions
{
	public static void AddForceWithMinWeight(Rigidbody rig, Vector3 force, ForceMode forceMode, float minWeight)
	{
		force *= Mathf.Clamp(rig.mass / 2f, 0.2f, 1f);
		minWeight = Mathf.Clamp(minWeight, 1f, float.PositiveInfinity);
		rig.AddForce(force * Mathf.Clamp(rig.mass / minWeight, 0f, 1f), forceMode);
	}

	public static void AddTorqueWithMinWeight(Rigidbody rig, Vector3 torque, ForceMode forceMode, float minWeight)
	{
		torque *= Mathf.Clamp(rig.mass / 2f, 0.2f, 1f);
		minWeight = Mathf.Clamp(minWeight, 1f, float.PositiveInfinity);
		rig.AddTorque(torque * Mathf.Clamp(rig.mass / minWeight, 0f, 1f), forceMode);
	}

	public static void AddForceWithMinWeight(Rigidbody rig, Vector3 force, Vector3 position, ForceMode forceMode, float minWeight)
	{
		force *= Mathf.Clamp(rig.mass / 2f, 0.2f, 1f);
		minWeight = Mathf.Clamp(minWeight, 1f, float.PositiveInfinity);
		rig.AddForceAtPosition(force * Mathf.Clamp(rig.mass / minWeight, 0f, 1f), position, forceMode);
	}

	public static void AddAxplosionForceWithMinWeight(Rigidbody rig, float force, Vector3 position, float radius, ForceMode forceMode, float minWeight)
	{
		force *= Mathf.Clamp(rig.mass / 2f, 0.2f, 1f);
		minWeight = Mathf.Clamp(minWeight, 1f, float.PositiveInfinity);
		rig.AddExplosionForce(force * Mathf.Clamp(rig.mass / minWeight, 0f, 1f), position, radius, 1f, forceMode);
	}

	internal static RaycastHit GetGroundPos(Vector3 position)
	{
		Ray ray = new Ray(position, Vector3.down);
		LayerMask layerMask = LayerMask.GetMask("Map");
		Physics.Raycast(ray, out var hitInfo, 1000f, layerMask);
		return hitInfo;
	}

	internal static bool CanSee(Vector3 pos1, Vector3 pos2)
	{
		Ray ray = new Ray(pos1, pos2 - pos1);
		LayerMask layerMask = LayerMask.GetMask("Map");
		Physics.Raycast(ray, out var hitInfo, Vector3.Distance(pos1, pos2), layerMask);
		return !hitInfo.transform;
	}
}
