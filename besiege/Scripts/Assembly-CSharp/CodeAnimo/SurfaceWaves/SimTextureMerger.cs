using UnityEngine;

namespace CodeAnimo.SurfaceWaves
{
	public class SimTextureMerger : SimulationOutput
	{
		[SerializeField]
		private SimulationOutput m_textureSource;

		[TextureDebug]
		[SerializeField]
		private RenderTexture m_targetTexture;

		private bool texturesMatched;

		public SimulationOutput textureSource
		{
			get
			{
				return m_textureSource;
			}
			set
			{
				if (value != m_textureSource)
				{
					ChangeSource(value);
				}
			}
		}

		public RenderTexture targetTexture
		{
			get
			{
				return m_targetTexture;
			}
			set
			{
				if (value != m_targetTexture)
				{
					ChangeDestination(value);
				}
			}
		}

		public override void LoadData()
		{
			texturesMatched = false;
		}

		public override void RunStep()
		{
			if (!(m_targetTexture == null) && !(m_textureSource == null) && m_textureSource.isDataAvailable)
			{
				if (!texturesMatched)
				{
					MatchTextureSettings();
				}
				Graphics.Blit(m_textureSource.outputData, m_targetTexture);
			}
		}

		private void ChangeSource(SimulationOutput source)
		{
			m_textureSource = source;
			MatchTextureSettings();
		}

		private void ChangeDestination(RenderTexture destination)
		{
			m_targetTexture = destination;
			MatchTextureSettings();
		}

		private void MatchTextureSettings()
		{
			if (textureSource.isDataAvailable)
			{
				RenderTexture renderTexture = textureSource.outputData;
				m_targetTexture.anisoLevel = renderTexture.anisoLevel;
				m_targetTexture.enableRandomWrite = false;
				m_targetTexture.filterMode = renderTexture.filterMode;
				m_targetTexture.dimension = renderTexture.dimension;
				m_targetTexture.mipMapBias = renderTexture.mipMapBias;
				m_targetTexture.name = "Merged " + renderTexture.name;
				m_targetTexture.useMipMap = renderTexture.useMipMap;
				m_targetTexture.volumeDepth = renderTexture.volumeDepth;
				m_targetTexture.wrapMode = renderTexture.wrapMode;
				if (m_targetTexture.format != renderTexture.format || m_targetTexture.depth != renderTexture.depth || m_targetTexture.width != renderTexture.width || m_targetTexture.height != renderTexture.height)
				{
					m_targetTexture.Release();
					m_targetTexture.depth = renderTexture.depth;
					m_targetTexture.format = renderTexture.format;
					m_targetTexture.height = renderTexture.height;
					m_targetTexture.width = renderTexture.width;
					m_targetTexture.Create();
				}
				texturesMatched = true;
			}
			else
			{
				texturesMatched = false;
			}
		}

		protected void OnValidate()
		{
			targetTexture = m_targetTexture;
			textureSource = m_textureSource;
		}
	}
}
