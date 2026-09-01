using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.Universal;

namespace INab.ToonDetailer.URP
{
	public class ToonDetailerPass : ScriptableRenderPass
	{
		private class PassData
		{
			public Material material;

			public TextureHandle source;

			public bool UseMask;

			public TextureHandle depthMask;

			public int shaderPass;

			public bool ControlViaVolumes;
		}

		private ToonDetailerSettings m_Settings;

		private Material m_Material;

		private static MaterialPropertyBlock s_SharedPropertyBlock;

		private static readonly int kDepthMaskTexture;

		private static readonly int kBlitTexturePropertyId;

		private static readonly int kBlitScaleBiasPropertyId;

		public ToonDetailerPass(string passName)
		{
		}

		public void Setup(ref Material material, ref ToonDetailerSettings settings)
		{
		}

		private static void ExecuteMainPass(PassData data, RasterGraphContext context)
		{
		}

		private void UpdateMaterialProperties(bool orthographic)
		{
		}

		public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
		{
		}

		public void Dispose()
		{
		}
	}
}
