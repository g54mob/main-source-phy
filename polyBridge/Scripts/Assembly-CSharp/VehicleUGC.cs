using Dreamteck.Splines;
using Poly.Collide.Unity;
using Poly.Physics;
using UnityEngine;

public class VehicleUGC : MonoBehaviour
{
	public GameObject m_OutlineSplineObject;

	public Transform m_OutlineSplineParentObject;

	public Transform m_PhysicsParentTransform;

	public void CreateSplines(Vehicle vehicle)
	{
		if (m_OutlineSplineObject != null)
		{
			vehicle.m_OutlineSplineComputer = m_OutlineSplineObject.AddComponent<SplineComputer>();
			vehicle.m_OutlineSplineComputer.type = Dreamteck.Splines.Spline.Type.Linear;
			GenerateSplineFromParentObject(vehicle.m_OutlineSplineComputer, m_OutlineSplineParentObject);
		}
	}

	public GameObject CreatePhysicsObject(Vehicle vehicle, Vector3 spawnPos, Quaternion spawnRot)
	{
		GameObject gameObject = Object.Instantiate(vehicle.m_PhysicsPrefab, spawnPos, spawnRot);
		for (int i = 0; i < gameObject.transform.childCount; i++)
		{
			GameObject gameObject2 = gameObject.transform.GetChild(i).GetChild(0).gameObject;
			if (gameObject2.GetComponent<PolygonCollider>() == null)
			{
				gameObject2.AddComponent<PolygonCollider>();
			}
			gameObject2.GetComponent<PolygonCollider>().layer = Layer.Vehicle;
		}
		gameObject.SetActive(value: true);
		return gameObject;
	}

	private void GenerateSplineFromParentObject(SplineComputer spline, Transform parentObject)
	{
		for (int i = 0; i < parentObject.childCount - 1; i++)
		{
			Vector3 localPosition = parentObject.GetChild(i).localPosition;
			spline.SetPointPosition(i, localPosition, SplineComputer.Space.Local);
		}
		Vector3 localPosition2 = parentObject.GetChild(0).localPosition;
		spline.SetPointPosition(spline.pointCount, localPosition2, SplineComputer.Space.Local);
		spline.Close();
	}
}
