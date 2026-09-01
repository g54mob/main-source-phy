using UnityEngine;

namespace LevelCreator
{
	public class ScreenSpaceShaderRenderer : MonoBehaviour
	{
		[SerializeField]
		private Material material;

		public void SetMaterial(Material material)
		{
			this.material = material;
		}

		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			if (material != null)
			{
				Graphics.Blit(src, dest, material);
			}
			else
			{
				Graphics.Blit(src, dest);
			}
		}
	}
}
