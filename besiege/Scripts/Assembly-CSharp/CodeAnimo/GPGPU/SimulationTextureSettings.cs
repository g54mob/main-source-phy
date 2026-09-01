using System;
using UnityEngine;

namespace CodeAnimo.GPGPU
{
	[Serializable]
	public class SimulationTextureSettings : ScriptableObject
	{
		public RenderTextureFormat dataPrecision = RenderTextureFormat.ARGBFloat;

		public TextureWrapMode wrapMode = TextureWrapMode.Clamp;

		public bool enableRandomWrite = true;

		public int anisoLevel;

		public FilterMode filterMode;

		public RenderTextureReadWrite readWriteMode = RenderTextureReadWrite.Linear;

		[SerializeField]
		private int m_textureDepth;

		public int textureDepth
		{
			get
			{
				return m_textureDepth;
			}
			set
			{
				if (value > 8)
				{
					if (value > 20)
					{
						m_textureDepth = 24;
					}
					else
					{
						m_textureDepth = 16;
					}
				}
				else
				{
					m_textureDepth = 0;
				}
			}
		}

		public void MatchTexture(RenderTexture texture)
		{
			dataPrecision = texture.format;
			wrapMode = texture.wrapMode;
			enableRandomWrite = texture.enableRandomWrite;
			anisoLevel = texture.anisoLevel;
			filterMode = texture.filterMode;
			if (texture.sRGB)
			{
				readWriteMode = RenderTextureReadWrite.sRGB;
			}
			else
			{
				readWriteMode = RenderTextureReadWrite.Linear;
			}
			textureDepth = texture.depth;
		}

		protected void OnValidate()
		{
			textureDepth = m_textureDepth;
		}

		public bool supportedOnCurrentSystem()
		{
			return SystemInfo.SupportsRenderTextureFormat(dataPrecision);
		}
	}
}
