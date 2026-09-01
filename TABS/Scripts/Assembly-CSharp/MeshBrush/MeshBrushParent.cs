using System;
using System.Collections;
using UnityEngine;

namespace MeshBrush
{
	public class MeshBrushParent : MonoBehaviour
	{
		private Transform[] paintedMeshes;

		private MeshFilter[] meshFilters;

		private Matrix4x4 localTransformationMatrix;

		private Hashtable materialToMesh;

		private MeshFilter currentMeshFilter;

		private Renderer currentRenderer;

		private Material[] materials;

		private CombineUtility.MeshInstance instance;

		private CombineUtility.MeshInstance[] instances;

		private ArrayList objects;

		private ArrayList elements;

		private void Initialize()
		{
			paintedMeshes = GetComponentsInChildren<Transform>();
			meshFilters = GetComponentsInChildren<MeshFilter>();
		}

		public void FlagMeshesAsStatic()
		{
			Initialize();
			for (int num = paintedMeshes.Length - 1; num >= 0; num--)
			{
				paintedMeshes[num].gameObject.isStatic = true;
			}
		}

		public void UnflagMeshesAsStatic()
		{
			Initialize();
			for (int num = paintedMeshes.Length - 1; num >= 0; num--)
			{
				paintedMeshes[num].gameObject.isStatic = false;
			}
		}

		public int GetMeshCount()
		{
			Initialize();
			return meshFilters.Length;
		}

		public int GetTrisCount()
		{
			Initialize();
			if (meshFilters.Length != 0)
			{
				int num = 0;
				for (int num2 = meshFilters.Length - 1; num2 >= 0; num2--)
				{
					num += meshFilters[num2].sharedMesh.triangles.Length;
				}
				return num / 3;
			}
			return 0;
		}

		public void DeleteAllMeshes()
		{
			UnityEngine.Object.DestroyImmediate(base.gameObject);
		}

		public void CombinePaintedMeshes(bool autoSelect, MeshFilter[] meshFilters)
		{
			if (meshFilters == null || meshFilters.Length == 0)
			{
				Debug.LogError("MeshBrush: The meshFilters array you passed as an argument to the CombinePaintedMeshes function is empty or null... Combining action cancelled!");
				return;
			}
			localTransformationMatrix = base.transform.worldToLocalMatrix;
			materialToMesh = new Hashtable();
			int num = 0;
			for (long num2 = 0L; num2 < meshFilters.LongLength; num2++)
			{
				currentMeshFilter = meshFilters[num2];
				num += currentMeshFilter.sharedMesh.vertexCount;
				if (num > 64000)
				{
					return;
				}
			}
			for (long num3 = 0L; num3 < meshFilters.LongLength; num3++)
			{
				currentMeshFilter = meshFilters[num3];
				currentRenderer = meshFilters[num3].GetComponent<Renderer>();
				instance = default(CombineUtility.MeshInstance);
				instance.mesh = currentMeshFilter.sharedMesh;
				if (!(currentRenderer != null) || !currentRenderer.enabled || !(instance.mesh != null))
				{
					continue;
				}
				instance.transform = localTransformationMatrix * currentMeshFilter.transform.localToWorldMatrix;
				materials = currentRenderer.sharedMaterials;
				for (int i = 0; i < materials.Length; i++)
				{
					instance.subMeshIndex = Math.Min(i, instance.mesh.subMeshCount - 1);
					objects = (ArrayList)materialToMesh[materials[i]];
					if (objects != null)
					{
						objects.Add(instance);
						continue;
					}
					objects = new ArrayList();
					objects.Add(instance);
					materialToMesh.Add(materials[i], objects);
				}
				UnityEngine.Object.DestroyImmediate(currentRenderer.gameObject);
			}
			foreach (DictionaryEntry item in materialToMesh)
			{
				elements = (ArrayList)item.Value;
				instances = (CombineUtility.MeshInstance[])elements.ToArray(typeof(CombineUtility.MeshInstance));
				GameObject gameObject = new GameObject("Combined mesh");
				gameObject.transform.parent = base.transform;
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localRotation = Quaternion.identity;
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.AddComponent<MeshFilter>();
				gameObject.AddComponent<MeshRenderer>();
				gameObject.AddComponent<SaveCombinedMesh>();
				gameObject.GetComponent<Renderer>().material = (Material)item.Key;
				gameObject.isStatic = true;
				currentMeshFilter = gameObject.GetComponent<MeshFilter>();
				currentMeshFilter.mesh = CombineUtility.Combine(instances, generateStrips: false);
			}
			base.gameObject.isStatic = true;
		}
	}
}
