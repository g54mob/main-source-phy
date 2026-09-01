using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.Universal;

namespace INab.ToonDetailer.URP
{
	[SupportedOnRenderer(typeof(UniversalRendererData))]
	[DisallowMultipleRendererFeature("Toon Detailer")]
	public class ToonDetailer : ScriptableRendererFeature
	{
		public class TextureRefData : ContextItem
		{
			public TextureHandle depthMaskTexture;

			public override void Reset()
			{
			}
		}

		[SerializeField]
		private ToonDetailerSettings m_Settings;

		[SerializeField]
		[HideInInspector]
		private Shader m_Shader;

		[SerializeField]
		[HideInInspector]
		private Shader m_DepthShader;

		private Material m_ToonDetailerMaterial;

		private Material m_DepthMaterial;

		private ToonDetailerPass m_ToonDetailerPass;

		private DepthMaskPass m_DepthMaskPass;

		public const string k_UseContours = "_USE_CONTOURS";

		public const string k_UseCavity = "_USE_CAVITY";

		public const string k_Orthographic = "_ORTHOGRAPHIC";

		public const string k_FadeContoursOnly = "_FADE_COUNTOURS_ONLY";

		public const string k_FadeOn = "_FADE_ON";

		public override void Create()
		{
		}

		public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
		{
		}

		protected override void Dispose(bool disposing)
		{
		}
	}
}
