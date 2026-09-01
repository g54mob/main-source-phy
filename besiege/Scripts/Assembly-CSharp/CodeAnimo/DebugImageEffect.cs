using UnityEngine;

namespace CodeAnimo
{
	[AddComponentMenu("Image Effects/Debug Image Effect")]
	[ExecuteInEditMode]
	public class DebugImageEffect : MonoBehaviour
	{
		public bool outputDepth;

		[HideInInspector]
		[SerializeField]
		private Shader m_DepthCopyShader;

		private Material m_depthCopyMaterial;

		[TextureDebug]
		public RenderTexture m_sourceCopy;

		protected Material depthCopyMaterial
		{
			get
			{
				if (m_depthCopyMaterial == null)
				{
					m_depthCopyMaterial = new Material(m_DepthCopyShader);
				}
				return m_depthCopyMaterial;
			}
		}

		protected void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			DestroyRenderTexture(m_sourceCopy);
			MakeDebugCopy(source, outputDepth);
			PassThrough(source, destination);
		}

		protected void MakeDebugCopy(RenderTexture source, bool copyDepth)
		{
			RenderTextureFormat format = ((!copyDepth) ? source.format : RenderTextureFormat.ARGBFloat);
			RenderTextureReadWrite readWrite = ((!source.sRGB) ? RenderTextureReadWrite.Linear : RenderTextureReadWrite.sRGB);
			m_sourceCopy = new RenderTexture(source.width, source.height, source.depth, format, readWrite);
			if (copyDepth)
			{
				Graphics.Blit(source, m_sourceCopy, depthCopyMaterial);
			}
			else
			{
				Graphics.Blit(source, m_sourceCopy);
			}
		}

		protected void PassThrough(RenderTexture source, RenderTexture destination)
		{
			Graphics.Blit(source, destination);
		}

		protected void OnDestroy()
		{
			DestroyRenderTexture(m_sourceCopy);
			Object.DestroyImmediate(m_depthCopyMaterial);
		}

		protected void OnDisable()
		{
			DestroyRenderTexture(m_sourceCopy);
		}

		protected void DestroyRenderTexture(RenderTexture target)
		{
			if (target != null)
			{
				target.Release();
				Object.DestroyImmediate(target);
			}
		}
	}
}
