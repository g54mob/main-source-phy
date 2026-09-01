using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Kamgam.SettingsGenerator
{
	public static class UniversalRenderPipelineUtils
	{
		private static FieldInfo MainLightCastShadows_FieldInfo;

		private static FieldInfo AdditionalLightCastShadows_FieldInfo;

		private static FieldInfo MainLightShadowmapResolution_FieldInfo;

		private static FieldInfo AdditionalLightShadowmapResolution_FieldInfo;

		private static FieldInfo Cascade2Split_FieldInfo;

		private static FieldInfo Cascade4Split_FieldInfo;

		private static FieldInfo SoftShadowsEnabled_FieldInfo;

		private static FieldInfo RenderDataList_FieldInfo;

		static UniversalRenderPipelineUtils()
		{
		}

		public static void SetMainLightCastShadows(bool value, UniversalRenderPipelineAsset asset = null)
		{
		}

		public static void SetAdditionalLightCastShadows(bool value, UniversalRenderPipelineAsset asset = null)
		{
		}

		public static void SetMainLightShadowResolution(int value, UniversalRenderPipelineAsset asset = null)
		{
		}

		public static int GetMainLightShadowResolution(UniversalRenderPipelineAsset asset = null)
		{
			return 0;
		}

		public static void SetAdditionalLightShadowResolution(int value, UniversalRenderPipelineAsset asset = null)
		{
		}

		public static void SetCascade2Split(float value, UniversalRenderPipelineAsset asset = null)
		{
		}

		public static void SetCascade4Split(Vector3 value, UniversalRenderPipelineAsset asset = null)
		{
		}

		public static void SetSoftShadowsEnabled(bool value, UniversalRenderPipelineAsset asset = null)
		{
		}

		public static ScriptableRendererData[] GetRendererDataList(UniversalRenderPipelineAsset asset = null)
		{
			return null;
		}

		public static T GetRendererFeature<T>(UniversalRenderPipelineAsset asset = null)
		{
			return default(T);
		}

		public static ScriptableRendererFeature GetRendererFeature(string typeName, UniversalRenderPipelineAsset asset = null)
		{
			return null;
		}

		public static T GetRendererFeatureChild<T>(ScriptableRendererFeature feature, string fieldName, string subFieldName = null)
		{
			return default(T);
		}

		public static void SetRendererFeatureChild<T>(T value, ScriptableRendererFeature feature, string fieldName, string subFieldName = null)
		{
		}

		public static bool IsRendererFeatureActive<T>(UniversalRenderPipelineAsset asset = null, bool defaultValue = false)
		{
			return false;
		}

		public static bool IsRendererFeatureActive(string typeName, UniversalRenderPipelineAsset asset = null, bool defaultValue = false)
		{
			return false;
		}

		public static void SetRendererFeatureActive<T>(bool active, UniversalRenderPipelineAsset asset = null)
		{
		}

		public static void SetRendererFeatureActive(string typeName, bool active, UniversalRenderPipelineAsset asset = null)
		{
		}
	}
}
