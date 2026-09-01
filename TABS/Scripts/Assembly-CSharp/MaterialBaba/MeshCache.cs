using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MaterialBaba
{
	[Serializable]
	public class MeshCache : SerializedScriptableObject
	{
		public struct OptimizedMeshInfo
		{
			public string m_path;

			public bool m_isCombined;

			public bool m_isBroken;
		}

		[SerializeField]
		private Dictionary<int, OptimizedMeshInfo> m_optimizedMeshes = new Dictionary<int, OptimizedMeshInfo>();

		[SerializeField]
		private Dictionary<int, Material> m_materials = new Dictionary<int, Material>();

		[SerializeField]
		private Dictionary<int, int> m_materialIndexMapping = new Dictionary<int, int>();

		[SerializeField]
		private Dictionary<int, bool[]> m_superMaterialFlags = new Dictionary<int, bool[]>();

		[SerializeField]
		private Material m_superMaterial;

		public void FlushCache()
		{
			m_superMaterialFlags.Clear();
			m_optimizedMeshes.Clear();
			m_materials.Clear();
			m_materialIndexMapping.Clear();
			m_superMaterial = null;
		}

		public bool ContainsOptimizedMesh(int hashCode, out OptimizedMeshInfo meshInfo)
		{
			if (m_optimizedMeshes.ContainsKey(hashCode))
			{
				meshInfo = m_optimizedMeshes[hashCode];
				return true;
			}
			meshInfo = default(OptimizedMeshInfo);
			return false;
		}

		public void AddSuperMaterialFlags(int completeHashCode, bool[] flags)
		{
			if (m_superMaterialFlags.ContainsKey(completeHashCode))
			{
				Debug.LogError("Failed to add Super Material Flags, they already exist!");
			}
			else
			{
				m_superMaterialFlags.Add(completeHashCode, flags);
			}
		}

		public bool ContainsSuperMaterialFlags(int completeHashCode)
		{
			return m_superMaterialFlags.ContainsKey(completeHashCode);
		}

		public bool[] GetSuperMaterialFlags(int completeHashCode)
		{
			return m_superMaterialFlags[completeHashCode];
		}

		public void SetSuperMaterial(Material superMaterial)
		{
			m_superMaterial = superMaterial;
		}

		public Material GetSuperMaterial()
		{
			return m_superMaterial;
		}

		public void AddMaterialToCache(int hashCode, Material material)
		{
			if (m_materials.ContainsKey(hashCode))
			{
				Debug.LogError("Could not add material: " + material.name + " to cache. The material seems to have already been added!");
			}
			else
			{
				m_materials.Add(hashCode, material);
			}
		}

		public void AddMaterialIndexMapping(int materialHashcode, int colorIndex)
		{
			if (m_materialIndexMapping.ContainsKey(materialHashcode))
			{
				Debug.LogError("Could not add MaterialIndexMapping. Mapping already seems to have been added!");
			}
			else
			{
				m_materialIndexMapping.Add(materialHashcode, colorIndex);
			}
		}

		public int GetMaterialIndexMapping(int materialHashcode)
		{
			return m_materialIndexMapping[materialHashcode];
		}

		public bool ContainsCachedMaterial(int hashCode)
		{
			if (m_materials.ContainsKey(hashCode))
			{
				return true;
			}
			return false;
		}

		public Material GetCachedMaterial(int hashCode)
		{
			if (m_materials.ContainsKey(hashCode))
			{
				return m_materials[hashCode];
			}
			return null;
		}

		public Material[] GetAllUniqueMaterials()
		{
			return m_materials.Values.ToArray();
		}
	}
}
