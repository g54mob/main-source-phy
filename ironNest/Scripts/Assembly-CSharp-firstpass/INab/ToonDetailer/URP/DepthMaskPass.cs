using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.Universal;

namespace INab.ToonDetailer.URP
{
	public class DepthMaskPass : ScriptableRenderPass
	{
		private class PassData
		{
			public RendererListHandle rendererListHandle;

			public TextureHandle destination;

			public Material material;
		}

		private static List<ShaderTagId> m_ShaderTagIdList;

		private LayerMask m_LayerMask;

		private Material m_Material;

		public DepthMaskPass(string passName)
		{
		}

		public void Setup(ref Material material, ref LayerMask layerMask)
		{
		}

		private void InitRendererLists(ContextContainer frameData, ref PassData passData, RenderGraph renderGraph)
		{
		}

		public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
		{
		}

		private static void ExecutePass(PassData data, RasterGraphContext context)
		{
		}

		public void Dispose()
		{
		}
	}
}
