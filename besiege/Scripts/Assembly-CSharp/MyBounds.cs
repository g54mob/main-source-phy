using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[AddComponentMenu("Blocks/MyBounds")]
public class MyBounds : MonoBehaviour
{
	public List<Collider> childColliders;

	[NonSerialized]
	public Bounds localBounds;

	[NonSerialized]
	public bool hasLocalBounds;

	private BlockBehaviour block;

	public bool isSurface;

	public bool resetJoiningColliders;

	public virtual void SetNonJoining()
	{
		if (!resetJoiningColliders)
		{
			return;
		}
		for (int i = 0; i < childColliders.Count; i++)
		{
			Collider collider = childColliders[i];
			if ((bool)collider)
			{
				switch (collider.gameObject.layer)
				{
				case 12:
				case 14:
					collider.gameObject.layer = 25;
					break;
				}
			}
		}
	}

	public virtual void UpdateColliders()
	{
		BlockBehaviour component = GetComponent<BlockBehaviour>();
		List<GameObject> destroyOnSim = component.DestroyOnSimulate.ToList();
		childColliders = new List<Collider>();
		Collider[] components = component.GetComponents<Collider>();
		foreach (Collider collider in components)
		{
			if (ValidCollider(collider, destroyOnSim))
			{
				childColliders.Add(collider);
			}
		}
		Transform transform = base.transform;
		for (int i = 0; i < transform.childCount; i++)
		{
			Collider collider = transform.GetChild(i).GetComponent<Collider>();
			if (collider != null && ValidCollider(collider, destroyOnSim))
			{
				childColliders.Add(collider);
			}
		}
	}

	public virtual Bounds GetBounds(bool updateBounding, bool includeTriggers = true)
	{
		if (!hasLocalBounds)
		{
			block = GetComponent<BlockBehaviour>();
			isSurface = block.Prefab.Type == BlockType.BuildSurface;
		}
		localBounds = EncapsulateBounds(includeTriggers);
		hasLocalBounds = true;
		return localBounds;
	}

	protected bool ValidCollider(Collider c, List<GameObject> destroyOnSim)
	{
		int layer = c.gameObject.layer;
		return layer != 22 && layer != 20 && layer != 2 && !destroyOnSim.Contains(c.gameObject);
	}

	protected Bounds EncapsulateBounds(bool includeTriggers = true)
	{
		Bounds result = default(Bounds);
		if (isSurface)
		{
			BuildSurface buildSurface = block as BuildSurface;
			if (!buildSurface.isValid)
			{
				result.center = base.transform.position;
				return result;
			}
			Matrix4x4 localToWorldMatrix = block.ParentMachine.BuildingMachine.localToWorldMatrix;
			if (!block.isSimulating)
			{
				result.center = buildSurface.GetCenter();
			}
			for (int i = 0; i < buildSurface.edges.Length; i++)
			{
				Vector3 vector = localToWorldMatrix.MultiplyPoint3x4(buildSurface.edges[i].Position);
				if (block.isSimulating && i == 0)
				{
					result.center = vector;
				}
				else
				{
					result.Encapsulate(vector);
				}
			}
			for (int i = 0; i < buildSurface.nodes.Length; i++)
			{
				result.Encapsulate(localToWorldMatrix.MultiplyPoint3x4(buildSurface.nodes[i].Position));
			}
		}
		else if (childColliders.Count > 0)
		{
			int num = 0;
			for (int j = 0; j < childColliders.Count; j++)
			{
				Collider collider = childColliders[j];
				if (collider == null || !collider.gameObject.activeSelf || !collider.enabled || (collider.isTrigger && !includeTriggers))
				{
					continue;
				}
				Bounds bounds = collider.bounds;
				if (collider is MeshCollider)
				{
					Mesh sharedMesh = (collider as MeshCollider).sharedMesh;
					if (num == 0 && (bool)sharedMesh && sharedMesh.vertexCount > 0)
					{
						Matrix4x4 localToWorldMatrix2 = collider.transform.localToWorldMatrix;
						result = new Bounds(localToWorldMatrix2.MultiplyPoint3x4(sharedMesh.vertices[0]), Vector3.zero);
						for (int k = 1; k < sharedMesh.vertexCount; k++)
						{
							result.Encapsulate(localToWorldMatrix2.MultiplyPoint3x4(sharedMesh.vertices[k]));
						}
						num++;
					}
				}
				else
				{
					if (num == 0)
					{
						result = bounds;
					}
					else
					{
						result.Encapsulate(bounds);
					}
					num++;
				}
			}
			if (num == 0)
			{
				result.center = base.transform.position;
			}
		}
		return result;
	}
}
