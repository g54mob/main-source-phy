using System.Collections.Generic;
using Dreamteck.Splines;
using Poly.Physics;
using UnityEngine;

public class Spline
{
	public static Dictionary<int, Dreamteck.Splines.Spline.Type> m_SplineTypeDict = new Dictionary<int, Dreamteck.Splines.Spline.Type>();

	public static void Init()
	{
		m_SplineTypeDict.Add(0, Dreamteck.Splines.Spline.Type.BSpline);
		m_SplineTypeDict.Add(1, Dreamteck.Splines.Spline.Type.Hermite);
		m_SplineTypeDict.Add(2, Dreamteck.Splines.Spline.Type.Linear);
	}

	public static Poly.Physics.Node AddPhysicsNode(Vector3 pos)
	{
		Poly.Physics.Node component = Object.Instantiate(Prefabs.m_Instance.m_PhysicsNode).GetComponent<Poly.Physics.Node>();
		if ((bool)component)
		{
			component.gameObject.name = "Node";
			component.define.isKinematic = true;
			component.transform.position = pos;
			component.transform.parent = Platforms.GetPlatformNodesContainerTransform();
			component.shapeDefine.collisionGroup = CollisionGroup.Fixed;
			component.shapeDefine.layer = Layer.CollideNothing;
			component.shapeDefine.enableCollision = false;
			component.ReInitPosition();
		}
		return component;
	}

	public static Edge AddPhysicsEdge(Poly.Physics.Node A, Poly.Physics.Node B)
	{
		Edge component = Object.Instantiate(Prefabs.m_Instance.m_PhysicsEdge).GetComponent<Edge>();
		if ((bool)component)
		{
			component.transform.parent = A.transform.parent;
			component.gameObject.name = "Edge";
			component.material = BridgeMaterials.GetBridgeMaterial(BridgeMaterialType.REINFORCED_ROAD).m_EdgeMaterial;
			component.InitBeforeStart(A, B);
			component.collisionGroup = CollisionGroup.Fixed;
			component.layer = Layer.PlatformSurface;
		}
		return component;
	}
}
