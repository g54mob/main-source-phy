using UnityEngine;

namespace Knife.HologramEffect
{
	[ExecuteAlways]
	public class MatrixProvider : MonoBehaviour
	{
		[SerializeField]
		private string propertyName = "_CustomMatrix";

		[SerializeField]
		private Renderer[] targetRenderers;

		[SerializeField]
		private bool eliminateRootBoneMatrix;

		private MaterialPropertyBlock materialPropertyBlock;

		private void OnEnable()
		{
			if (this == null)
			{
				return;
			}
			if (materialPropertyBlock == null)
			{
				materialPropertyBlock = new MaterialPropertyBlock();
			}
			if (targetRenderers == null)
			{
				return;
			}
			Renderer[] array = targetRenderers;
			foreach (Renderer renderer in array)
			{
				if (!(renderer != null))
				{
					continue;
				}
				Matrix4x4 matrix4x = base.transform.localToWorldMatrix;
				if (eliminateRootBoneMatrix)
				{
					SkinnedMeshRenderer skinnedMeshRenderer = renderer as SkinnedMeshRenderer;
					if (skinnedMeshRenderer != null)
					{
						Transform rootBone = skinnedMeshRenderer.rootBone;
						if (rootBone != null)
						{
							matrix4x = rootBone.localToWorldMatrix * matrix4x;
						}
					}
				}
				renderer.GetPropertyBlock(materialPropertyBlock);
				materialPropertyBlock.SetMatrix(propertyName, matrix4x);
				renderer.SetPropertyBlock(materialPropertyBlock);
			}
		}

		private void OnValidate()
		{
			OnEnable();
		}
	}
}
