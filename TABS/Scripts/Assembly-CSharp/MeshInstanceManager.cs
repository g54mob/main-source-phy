using System;
using System.Collections.Generic;
using System.Linq;
using TFBGames;
using UnityEngine;

public class MeshInstanceManager : MonoBehaviour
{
	public static MeshInstanceManager Instance;

	public Material SimpleVertexColorMaterial;

	private Dictionary<Mesh, Mesh> MirroredMeshCache;

	private Dictionary<int, OptimizedItem> CombinedMeshCache;

	private const int HASH_BASE = 397;

	private List<Vector3> vertexCache = new List<Vector3>(7000);

	private List<Vector3> normalCache = new List<Vector3>(7000);

	private List<int> triangleCache = new List<int>(10500);

	public Mesh RequestMirroredMesh(Mesh sourceMesh)
	{
		if (MirroredMeshCache == null)
		{
			MirroredMeshCache = new Dictionary<Mesh, Mesh>();
		}
		if (sourceMesh == null)
		{
			return null;
		}
		if (MirroredMeshCache.TryGetValue(sourceMesh, out var value))
		{
			return value;
		}
		Mesh mesh = UnityEngine.Object.Instantiate(sourceMesh);
		int vertexCount = mesh.vertexCount;
		mesh.GetVertices(vertexCache);
		for (int i = 0; i < vertexCount; i++)
		{
			Vector3 value2 = vertexCache[i];
			value2.x = 0f - value2.x;
			vertexCache[i] = value2;
		}
		mesh.GetNormals(normalCache);
		for (int j = 0; j < vertexCount; j++)
		{
			Vector3 value3 = normalCache[j];
			value3.x = 0f - value3.x;
			normalCache[j] = value3;
		}
		mesh.SetVertices(vertexCache);
		mesh.SetNormals(normalCache);
		int subMeshCount = mesh.subMeshCount;
		for (int k = 0; k < subMeshCount; k++)
		{
			mesh.GetTriangles(triangleCache, k);
			for (int l = 0; l < triangleCache.Count; l += 3)
			{
				int value4 = triangleCache[l + 1];
				int value5 = triangleCache[l + 2];
				triangleCache[l + 1] = value5;
				triangleCache[l + 2] = value4;
			}
			mesh.SetTriangles(triangleCache, k);
		}
		mesh.name = sourceMesh.name + "_Mir";
		MirroredMeshCache.Add(sourceMesh, mesh);
		mesh.RecalculateBounds();
		sourceMesh.UploadMeshData(markNoLongerReadable: false);
		mesh.UploadMeshData(markNoLongerReadable: false);
		return mesh;
	}

	public OptimizedItem RequestOptimizedMesh(Mesh baseMesh, Material[] materials, bool hasMirror)
	{
		RemoveArrayNullItems(ref materials);
		if (CombinedMeshCache == null)
		{
			CombinedMeshCache = new Dictionary<int, OptimizedItem>();
		}
		int assetComboHash = GetAssetComboHash(baseMesh, materials);
		if (CombinedMeshCache.TryGetValue(assetComboHash, out var value))
		{
			return value;
		}
		OptimizedItem optimizedItem = GetOptimizedItem(baseMesh, materials, !hasMirror);
		CombinedMeshCache.Add(assetComboHash, optimizedItem);
		return optimizedItem;
	}

	private OptimizedItem GetOptimizedItem(Mesh baseMesh, Material[] materials, bool createReadOnly)
	{
		Mesh mesh = UnityEngine.Object.Instantiate(baseMesh);
		mesh.name = baseMesh.name + "_RTOpt";
		int num = Math.Min(materials.Length, mesh.subMeshCount);
		MapVertexColors(mesh, materials, num);
		Dictionary<Material, List<int>> dictionary = new Dictionary<Material, List<int>>();
		for (int i = 0; i < num; i++)
		{
			Material material = materials[i];
			Material key = SimpleVertexColorMaterial;
			if (material.IsMaterialTransparent())
			{
				key = UnityEngine.Object.Instantiate(material);
			}
			if (material.mainTexture != null)
			{
				key = UnityEngine.Object.Instantiate(material);
			}
			if (!dictionary.ContainsKey(key))
			{
				dictionary.Add(key, new List<int>());
			}
			dictionary[key].AddRange(baseMesh.GetTriangles(i));
		}
		mesh.subMeshCount = dictionary.Count;
		int num2 = 0;
		foreach (Material key2 in dictionary.Keys)
		{
			mesh.SetTriangles(dictionary[key2], num2);
			num2++;
		}
		mesh.RecalculateBounds();
		mesh.UploadMeshData(markNoLongerReadable: false);
		return new OptimizedItem
		{
			mesh = mesh,
			materials = dictionary.Keys.ToArray()
		};
	}

	private void MapVertexColors(Mesh newMesh, Material[] materials, int maxBakeIndex)
	{
		Color[] array = new Color[newMesh.vertexCount];
		for (int i = 0; i < maxBakeIndex; i++)
		{
			newMesh.GetTriangles(triangleCache, i);
			Color color = materials[i].SafeColor();
			if (QualitySettings.activeColorSpace == ColorSpace.Linear)
			{
				color = color.linear;
			}
			foreach (int item in triangleCache)
			{
				array[item] = color;
			}
		}
		newMesh.colors = array;
	}

	private int GetAssetComboHash(Mesh baseMesh, Material[] materials)
	{
		RemoveArrayDuplicates(ref materials);
		int num = 397 * baseMesh.GetHashCode();
		Material[] array = materials;
		foreach (Material material in array)
		{
			num ^= 397 * material.GetHashCode();
		}
		return num;
	}

	private void RemoveArrayNullItems(ref Material[] materials)
	{
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < materials.Length; i++)
		{
			if (materials[i] == null)
			{
				num++;
				continue;
			}
			materials[num2] = materials[i];
			num2++;
		}
		materials = CopyMaterialsArray(materials.Length - num, materials);
	}

	private void RemoveArrayDuplicates(ref Material[] materials)
	{
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < materials.Length; i++)
		{
			bool flag = false;
			for (int j = 0; j < i; j++)
			{
				if (materials[i] == materials[j])
				{
					flag = true;
					num++;
					break;
				}
			}
			if (!flag)
			{
				materials[num2] = materials[i];
				num2++;
			}
		}
		materials = CopyMaterialsArray(materials.Length - num, materials);
	}

	private Material[] CopyMaterialsArray(int length, Material[] sourceArray)
	{
		Material[] array = new Material[length];
		Array.Copy(sourceArray, array, array.Length);
		return array;
	}

	protected void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Debug.LogError("Mesh instance manager already has an active instance. This should not be the case. Ensure the existing instance is being properly disposed of.");
		}
	}

	protected void OnDestroy()
	{
		if (Instance == this)
		{
			Instance = null;
		}
		vertexCache.Clear();
		normalCache.Clear();
		triangleCache.Clear();
		if (MirroredMeshCache != null)
		{
			foreach (Mesh value in MirroredMeshCache.Values)
			{
				if (value != null)
				{
					UnityEngine.Object.Destroy(value);
				}
			}
		}
		if (CombinedMeshCache == null)
		{
			return;
		}
		foreach (OptimizedItem value2 in CombinedMeshCache.Values)
		{
			if (value2.mesh != null)
			{
				UnityEngine.Object.Destroy(value2.mesh);
			}
			Material[] materials = value2.materials;
			foreach (Material material in materials)
			{
				if (material != null && material != SimpleVertexColorMaterial)
				{
					UnityEngine.Object.Destroy(material);
				}
			}
		}
	}
}
