using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR;

namespace Battlehub.RTCommon
{
	public static class RenderPipelineInfo
	{
		public static bool ForceUseRenderTextures;

		public static bool UseForegroundLayerForUI;

		public static readonly RPType Type;

		internal static bool UseRenderGraph;

		public static readonly string DefaultShaderName;

		public static readonly string DefaultTerrainShaderName;

		public static readonly int ColorPropertyID;

		public static readonly int MainTexturePropertyID;

		private static readonly PropertyInfo m_msaaProperty;

		private static Material m_defaultMaterial;

		private static Material m_defaultTerrainMaterial;

		private static string[] m_builtInRenderPipelineAssetNames;

		public static bool UseRenderTextures => ForceUseRenderTextures;

		public static int MSAASampleCount
		{
			get
			{
				if (Type == RPType.Standard)
				{
					return QualitySettings.antiAliasing;
				}
				if (m_msaaProperty != null)
				{
					try
					{
						return Convert.ToInt32(m_msaaProperty.GetValue(GraphicsSettingsExt.renderPipelineAsset));
					}
					catch (Exception message)
					{
						Debug.LogError(message);
					}
				}
				return 1;
			}
		}

		public static Material DefaultMaterial
		{
			get
			{
				if (m_defaultMaterial == null)
				{
					m_defaultMaterial = new Material(Shader.Find(DefaultShaderName));
					m_defaultMaterial.Color(Color.white);
				}
				return m_defaultMaterial;
			}
		}

		public static Material DefaultTerrainMaterial
		{
			get
			{
				if (m_defaultTerrainMaterial == null)
				{
					m_defaultTerrainMaterial = new Material(Shader.Find(DefaultTerrainShaderName));
				}
				return m_defaultTerrainMaterial;
			}
		}

		public static bool IsBuiltInRendererPipelineAssetName(string name)
		{
			if (m_builtInRenderPipelineAssetNames == null)
			{
				return false;
			}
			return Array.IndexOf(m_builtInRenderPipelineAssetNames, name) >= 0;
		}

		public static string GetBuiltInRendererPipelineAssetName(GraphicsQuality quality)
		{
			if (m_builtInRenderPipelineAssetNames == null)
			{
				return null;
			}
			return quality switch
			{
				GraphicsQuality.High => m_builtInRenderPipelineAssetNames[0], 
				GraphicsQuality.Medium => m_builtInRenderPipelineAssetNames[1], 
				GraphicsQuality.Low => m_builtInRenderPipelineAssetNames[2], 
				_ => null, 
			};
		}

		public static RenderPipelineAsset LoadBuiltInRendererPipelineAsset(GraphicsQuality graphicsQuality)
		{
			string builtInRendererPipelineAssetName = GetBuiltInRendererPipelineAssetName(graphicsQuality);
			if (builtInRendererPipelineAssetName == null)
			{
				return null;
			}
			return Resources.Load<RenderPipelineAsset>(builtInRendererPipelineAssetName);
		}

		static RenderPipelineInfo()
		{
			ForceUseRenderTextures = false;
			UseForegroundLayerForUI = true;
			if (GraphicsSettingsExt.renderPipelineAsset == null)
			{
				Type = RPType.Standard;
				DefaultShaderName = "Standard";
				ColorPropertyID = Shader.PropertyToID("_Color");
				MainTexturePropertyID = Shader.PropertyToID("_MainTex");
				return;
			}
			Type type = GraphicsSettingsExt.renderPipelineAsset.GetType();
			if (type.Name == "UniversalRenderPipelineAsset")
			{
				Type = RPType.URP;
				m_msaaProperty = type.GetProperty("msaaSampleCount");
				DefaultShaderName = "Universal Render Pipeline/Lit";
				DefaultTerrainShaderName = "Universal Render Pipeline/Terrain/Lit";
				ColorPropertyID = Shader.PropertyToID("_BaseColor");
				MainTexturePropertyID = Shader.PropertyToID("_BaseMap");
				m_builtInRenderPipelineAssetNames = new string[3] { "HighQuality_UniversalRenderPipelineAsset", "MidQuality_UniversalRenderPipelineAsset", "LowQuality_UniversalRenderPipelineAsset" };
			}
			else if (type.Name == "HDRenderPipelineAsset")
			{
				Type = RPType.HDRP;
				m_msaaProperty = type.GetProperty("msaaSampleCount");
				DefaultShaderName = "HDRP/Lit";
				DefaultTerrainShaderName = "HDRP/TerrainLit";
				ColorPropertyID = Shader.PropertyToID("_BaseColor");
				MainTexturePropertyID = Shader.PropertyToID("_BaseColorMap");
			}
			else
			{
				Debug.Log(GraphicsSettingsExt.renderPipelineAsset.GetType());
				Type = RPType.Unknown;
				ColorPropertyID = Shader.PropertyToID("_Color");
				MainTexturePropertyID = Shader.PropertyToID("_MainTex");
			}
		}

		private static void Unity2019_UITransparencyFix()
		{
			Canvas.GetDefaultCanvasMaterial().shader = Shader.Find("UI/Default2020");
		}

		[Obsolete]
		public static bool IsXRDeviceResent()
		{
			return IsXRDevicePresent();
		}

		public static bool IsXRDevicePresent()
		{
			List<XRDisplaySubsystem> list = new List<XRDisplaySubsystem>();
			SubsystemManager.GetSubsystems(list);
			foreach (XRDisplaySubsystem item in list)
			{
				if (item.running)
				{
					return true;
				}
			}
			return false;
		}

		public static void XRFix(Camera camera)
		{
		}
	}
}
