using UnityEngine;

namespace MTAssets.UltimateLODSystem
{
	[AddComponentMenu(null)]
	public class UltimateLevelOfDetailMeshes : MonoBehaviour
	{
		[HideInInspector]
		public UltimateLevelOfDetail responsibleUlod;

		[HideInInspector]
		public int idOfOriginalMeshItemOfThisInResponsibleUlod;

		public UltimateLevelOfDetail GetResponsibleUlodComponent()
		{
			return null;
		}

		public int GetQuantityOfLods()
		{
			return 0;
		}

		public void SetMeshOfThisLodGroup(int level, Mesh newMesh)
		{
		}

		public Mesh GetMeshOfThisLodGroup(int level)
		{
			return null;
		}

		public bool isMaterialChangesEnabledForThisMesh()
		{
			return false;
		}

		public void SetMaterialArrayOfThisLodGroup(int level, Material[] newMaterialArray)
		{
		}

		public Material[] GetMaterialArrayOfThisLodGroup(int level)
		{
			return null;
		}
	}
}
