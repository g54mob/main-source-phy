using System.Collections.Generic;
using UnityEngine;

namespace TFBGames
{
	[RequireComponent(typeof(LODGroup))]
	public class LodGroupMaterialIndexer : MonoBehaviour
	{
		[SerializeField]
		private bool isManuallySetup;

		[Space]
		[SerializeField]
		private List<LodMaterialContainer> indexedMaterials;

		[Space]
		[SerializeField]
		private List<int> duplicateIndices = new List<int>();

		private bool hasInitializedMatrix;

		private LODGroup lodGroup;

		private LodGroupMaterialIndexer mirroredObject;

		public bool IsManuallySetup => isManuallySetup;

		public void SetMirroredObject(LodGroupMaterialIndexer mirrored)
		{
			mirroredObject = mirrored;
		}

		public void SetMaterial(int submeshIndex, Material materialToSet)
		{
			if (submeshIndex < 0 || submeshIndex >= indexedMaterials.Count || indexedMaterials == null || indexedMaterials.Count <= 0)
			{
				return;
			}
			LodMaterialContainer lodMaterialContainer = indexedMaterials[submeshIndex];
			if (lodMaterialContainer != null)
			{
				int lod0Index = lodMaterialContainer.lod0Index;
				int lod1Index = lodMaterialContainer.lod1Index;
				int lod2Index = lodMaterialContainer.lod2Index;
				Renderer lod0Renderer = lodMaterialContainer.lod0Renderer;
				Renderer lod1Renderer = lodMaterialContainer.lod1Renderer;
				Renderer lod2Renderer = lodMaterialContainer.lod2Renderer;
				if (lod0Renderer != null)
				{
					SetLodMaterialAtIndex(lod0Renderer, lod0Index, materialToSet);
				}
				if (lod1Renderer != null)
				{
					SetLodMaterialAtIndex(lod1Renderer, lod1Index, materialToSet);
				}
				if (lod2Renderer != null)
				{
					SetLodMaterialAtIndex(lod2Renderer, lod2Index, materialToSet);
				}
				if (mirroredObject != null)
				{
					mirroredObject.SetMaterial(submeshIndex, materialToSet);
				}
			}
		}

		private void SetLodMaterialAtIndex(Renderer lodRenderer, int lodIndex, Material materialToSet)
		{
			Material[] materials = lodRenderer.materials;
			if (materials != null && materials.Length != 0 && lodIndex >= 0 && lodIndex < materials.Length && materials[lodIndex] != null)
			{
				materials[lodIndex] = materialToSet;
			}
			lodRenderer.materials = materials;
		}

		public void ResetAllMaterialsToDefaults()
		{
			if (indexedMaterials != null && indexedMaterials.Count > 0)
			{
				for (int i = 0; i < indexedMaterials.Count; i++)
				{
					ResetMaterialAtIndex(i);
				}
			}
		}

		public void ResetMaterialAtIndex(int index)
		{
			if (indexedMaterials == null || indexedMaterials.Count <= 0)
			{
				return;
			}
			LodMaterialContainer lodMaterialContainer = indexedMaterials[index];
			if (lodMaterialContainer != null)
			{
				Material material = ((!(lodMaterialContainer.customMaterial != null)) ? lodMaterialContainer.material : lodMaterialContainer.customMaterial);
				if (!(material == null))
				{
					SetMaterial(index, material);
				}
			}
		}

		public void SetCustomMaterial(int index, Material material)
		{
			if (indexedMaterials.Count > 0 && index < indexedMaterials.Count && index >= 0)
			{
				indexedMaterials[index].customMaterial = material;
			}
			else
			{
				Debug.LogError("Failed to set material because index is out of range of indexedMaterials, " + base.gameObject.name);
			}
		}

		public Material GetMaterialAtIndex(int index)
		{
			if (indexedMaterials == null || indexedMaterials.Count <= 0 || index < 0 || index >= indexedMaterials.Count)
			{
				return null;
			}
			LodMaterialContainer lodMaterialContainer = indexedMaterials[index];
			if (lodMaterialContainer == null)
			{
				return null;
			}
			if (lodMaterialContainer.customMaterial != null)
			{
				return lodMaterialContainer.customMaterial;
			}
			return lodMaterialContainer.material;
		}
	}
}
