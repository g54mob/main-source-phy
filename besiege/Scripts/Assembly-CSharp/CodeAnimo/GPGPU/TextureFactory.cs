using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeAnimo.GPGPU
{
	[Serializable]
	[AddComponentMenu("GPGPU/Texture Factory")]
	public class TextureFactory : MonoBehaviour
	{
		public SimulationTextureSettings textureSettings;

		public int resolutionU = 512;

		public int resolutionV = 512;

		[SerializeField]
		[HideInInspector]
		private int m_allowedRecentTextureCount = 1;

		private bool _initialized;

		[HideInInspector]
		[SerializeField]
		private List<RenderTexture> createdTextures;

		private Texture2D m_clearTexture;

		public int registeredTextureCount
		{
			get
			{
				if (createdTextures != null)
				{
					return createdTextures.Count;
				}
				return 0;
			}
		}

		public int previousTexturesCount
		{
			get
			{
				if (createdTextures != null)
				{
					return Math.Max(0, createdTextures.Count - 1);
				}
				return 0;
			}
		}

		public RenderTexture LatestTexture
		{
			get
			{
				int num = registeredTextureCount;
				if (num < 1)
				{
					return null;
				}
				return createdTextures[num - 1];
			}
		}

		public List<RenderTexture> registeredTextures
		{
			get
			{
				return createdTextures;
			}
		}

		public int allowedRecentTextureCount
		{
			get
			{
				return m_allowedRecentTextureCount;
			}
			set
			{
				if (m_allowedRecentTextureCount != value)
				{
					m_allowedRecentTextureCount = value;
					DestroyRegisteredTextures(value);
				}
			}
		}

		protected void Awake()
		{
			Initialize();
		}

		protected virtual void OnDestroy()
		{
			DestroyRegisteredTextures(0);
			DestroyClearTexture();
		}

		protected void Initialize()
		{
			if (_initialized)
			{
				Debug.LogWarning("Trying to initialize Texture Factory while it is already initialized. Existing data will be used.", this);
				return;
			}
			createdTextures = new List<RenderTexture>(allowedRecentTextureCount + 1);
			_initialized = true;
		}

		public RenderTexture CreateOutputTexture(string name, bool automaticDestruction)
		{
			if (!_initialized)
			{
				Initialize();
			}
			RenderTexture renderTexture = new RenderTexture(resolutionU, resolutionV, textureSettings.textureDepth, textureSettings.dataPrecision, textureSettings.readWriteMode);
			setSimulationTextureSettings(renderTexture, name);
			renderTexture.Create();
			if (automaticDestruction)
			{
				RegisterTexture(renderTexture);
			}
			return renderTexture;
		}

		public RenderTexture CreateOutputTexture(string name)
		{
			return CreateOutputTexture(name, true);
		}

		public RenderTexture CreateOutputTexture()
		{
			return CreateOutputTexture("Unnamed Output Texture", true);
		}

		private void setSimulationTextureSettings(RenderTexture simulationTexture, string name)
		{
			simulationTexture.name = name;
			simulationTexture.enableRandomWrite = textureSettings.enableRandomWrite;
			simulationTexture.anisoLevel = textureSettings.anisoLevel;
			simulationTexture.filterMode = textureSettings.filterMode;
			simulationTexture.hideFlags = HideFlags.HideAndDontSave;
			simulationTexture.wrapMode = textureSettings.wrapMode;
		}

		public void DestroyOldTextures()
		{
			DestroyRegisteredTextures(1);
		}

		public void DestroyAllTextures()
		{
			DestroyRegisteredTextures(0);
		}

		private void RegisterTexture(RenderTexture addedTexture)
		{
			createdTextures.Add(addedTexture);
			if (registeredTextureCount > allowedRecentTextureCount)
			{
				DestroyRegisteredTextures(allowedRecentTextureCount);
			}
		}

		private void DestroyRegisteredTextures(int recentKeptCount)
		{
			while (previousTexturesCount > recentKeptCount)
			{
				RenderTexture victim = RemoveOldest();
				DestroyRenderTexture(victim);
			}
		}

		private RenderTexture RemoveOldest()
		{
			RenderTexture result = createdTextures[0];
			createdTextures.RemoveAt(0);
			return result;
		}

		private void DestroyRenderTexture(RenderTexture victim)
		{
			if (victim == null)
			{
				throw new MissingReferenceException("A RenderTexture seems to have disappeared. Perhaps the renderer threw it away?");
			}
			victim.Release();
			UnityEngine.Object.DestroyImmediate(victim);
		}

		private void DestroyClearTexture()
		{
			UnityEngine.Object.Destroy(m_clearTexture);
		}

		public Texture2D GetClearTexture()
		{
			if (m_clearTexture == null)
			{
				m_clearTexture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
				m_clearTexture.name = "clear texture";
				Color color = new Color(0f, 0f, 0f, 0f);
				m_clearTexture.SetPixel(0, 0, color);
				m_clearTexture.filterMode = FilterMode.Point;
				m_clearTexture.Apply(false, true);
			}
			return m_clearTexture;
		}
	}
}
