using Dreamteck.Splines;
using Poly.Collide.Unity;
using Poly.Physics.Gameplay;
using UnityEngine;

public class ZedAxisVehicleUGC : MonoBehaviour
{
	public GameObject m_OutlineSplineObject;

	public Transform m_OutlineSplineParentObject;

	public Transform m_PhysicsParentTransform;

	public void CreateSplines(ZedAxisVehicle zedAxisVehicle)
	{
		if (m_OutlineSplineObject != null)
		{
			zedAxisVehicle.m_OutlineSplineComputer = m_OutlineSplineObject.AddComponent<SplineComputer>();
			zedAxisVehicle.m_OutlineSplineComputer.type = Dreamteck.Splines.Spline.Type.Linear;
			GenerateSplineFromParentObject(zedAxisVehicle.m_OutlineSplineComputer, m_OutlineSplineParentObject);
			if (zedAxisVehicle.m_CollisionInfo == null)
			{
				zedAxisVehicle.m_CollisionInfo = m_OutlineSplineObject.AddComponent<PlaceableCollisionInfo>();
			}
		}
		if (zedAxisVehicle.m_ProfileOutlines.transform.childCount == 0)
		{
			for (int i = 0; i < m_PhysicsParentTransform.childCount - 1; i++)
			{
				GameObject obj = new GameObject();
				obj.transform.parent = zedAxisVehicle.m_ProfileOutlines.transform;
				obj.transform.SetSiblingIndex(i);
				obj.transform.localPosition = m_PhysicsParentTransform.GetChild(i).localPosition;
				SplineComputer splineComputer = obj.AddComponent<SplineComputer>();
				splineComputer.type = Dreamteck.Splines.Spline.Type.Linear;
				GenerateSplineFromParentObject(splineComputer, m_PhysicsParentTransform.GetChild(i));
			}
			Object.Instantiate(m_PhysicsParentTransform.GetChild(m_PhysicsParentTransform.childCount - 1).gameObject, zedAxisVehicle.m_ProfileOutlines.transform).transform.SetSiblingIndex(zedAxisVehicle.m_ProfileOutlines.transform.childCount);
		}
		if (zedAxisVehicle.m_OutlineMeshRenderer != null)
		{
			zedAxisVehicle.m_OutlineMeshRenderer.material = Prefabs.m_Instance.m_SandboxOutlineMaterial;
		}
	}

	public GameObject CreatePhysicsObject(ZedAxisVehicle zedAxisVehicle)
	{
		GameObject gameObject = Object.Instantiate(zedAxisVehicle.m_PhysicsPrefab, zedAxisVehicle.m_SpawnPos, zedAxisVehicle.transform.rotation);
		foreach (Transform item in gameObject.transform)
		{
			item.gameObject.SetActive(value: false);
		}
		for (int i = 0; i < gameObject.transform.childCount - 1; i++)
		{
			GameObject gameObject2 = gameObject.transform.GetChild(i).gameObject;
			if (gameObject2.GetComponent<PolygonCollider>() == null)
			{
				gameObject2.AddComponent<PolygonCollider>();
				gameObject2.AddComponent<TriggerComponent>();
			}
		}
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
