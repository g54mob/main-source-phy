using System.Collections.Generic;
using GLTFast.Logging;
using UnityEngine;

namespace GLTFast
{
	public class GameObjectBoundsInstantiator : GameObjectInstantiator
	{
		private Dictionary<uint, Bounds> m_NodeBounds;

		public GameObjectBoundsInstantiator(IGltfReadable gltf, Transform parent, ICodeLogger logger = null, InstantiationSettings settings = null)
			: base(gltf, parent, logger, settings)
		{
		}

		public override void BeginScene(string name, uint[] rootNodeIndices)
		{
			base.BeginScene(name, rootNodeIndices);
			m_NodeBounds = new Dictionary<uint, Bounds>();
		}

		public override void AddPrimitive(uint nodeIndex, string meshName, MeshResult meshResult, uint[] joints = null, uint? rootJoint = null, float[] morphTargetWeights = null, int meshNumeration = 0)
		{
			base.AddPrimitive(nodeIndex, meshName, meshResult, joints, rootJoint, morphTargetWeights, meshNumeration);
			if (m_NodeBounds != null)
			{
				Bounds transformedBounds = GetTransformedBounds(meshResult.mesh.bounds, m_Parent.worldToLocalMatrix * m_Nodes[nodeIndex].transform.localToWorldMatrix);
				if (m_NodeBounds.TryGetValue(nodeIndex, out var value))
				{
					transformedBounds.Encapsulate(value);
					m_NodeBounds[nodeIndex] = transformedBounds;
				}
				else
				{
					m_NodeBounds[nodeIndex] = transformedBounds;
				}
			}
		}

		public Bounds? CalculateBounds()
		{
			if (m_NodeBounds == null)
			{
				return null;
			}
			bool flag = false;
			Bounds value = default(Bounds);
			foreach (Bounds value2 in m_NodeBounds.Values)
			{
				if (flag)
				{
					value.Encapsulate(value2);
					continue;
				}
				value = value2;
				flag = true;
			}
			if (!flag)
			{
				return null;
			}
			return value;
		}

		private static Bounds GetTransformedBounds(Bounds b, Matrix4x4 transform)
		{
			Vector3[] array = new Vector3[8];
			Vector3 extents = b.extents;
			for (int i = 0; i < 8; i++)
			{
				Vector3 center = b.center;
				center.x += (((i & 1) == 0) ? extents.x : (0f - extents.x));
				center.y += (((i & 2) == 0) ? extents.y : (0f - extents.y));
				center.z += (((i & 4) == 0) ? extents.z : (0f - extents.z));
				array[i] = center;
			}
			return GeometryUtility.CalculateBounds(array, transform);
		}
	}
}
