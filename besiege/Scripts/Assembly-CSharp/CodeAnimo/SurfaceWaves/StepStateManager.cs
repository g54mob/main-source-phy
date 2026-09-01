using CodeAnimo.GPGPU;
using UnityEngine;

namespace CodeAnimo.SurfaceWaves
{
	public class StepStateManager : MonoBehaviour
	{
		[SerializeField]
		private Texture2D m_startMap;

		private Texture2D clearTexture;

		protected bool m_savedStateAvailable;

		protected void OnDisable()
		{
			destroyClearTexture();
		}

		protected void OnDestroy()
		{
			destroyClearTexture();
		}

		public virtual RenderTexture LoadState(TextureFactory destination)
		{
			return initializeTextures(destination);
		}

		public RenderTexture initializeTextures(TextureFactory textureBuilder)
		{
			Texture2D startMap = m_startMap;
			if (startMap == null)
			{
				if (clearTexture == null)
				{
					clearTexture = textureBuilder.GetClearTexture();
				}
				startMap = clearTexture;
			}
			string text = "Initial Texture for " + textureBuilder.name;
			RenderTexture renderTexture = textureBuilder.CreateOutputTexture(text);
			Graphics.Blit(startMap, renderTexture);
			return renderTexture;
		}

		private void destroyClearTexture()
		{
			if (!(clearTexture == null))
			{
				Object.DestroyImmediate(clearTexture);
			}
		}
	}
}
