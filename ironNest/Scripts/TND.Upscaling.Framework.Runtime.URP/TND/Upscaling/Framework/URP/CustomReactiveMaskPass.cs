using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.Universal;

namespace TND.Upscaling.Framework.URP
{
	public class CustomReactiveMaskPass : ScriptableRenderPass
	{
		private class PassData
		{
			public RendererListHandle rendererListHandle;
		}

		private const string PassName = "[Upscaler] Custom Reactive Mask Pass";

		private RTHandle _customReactiveMask;

		private RenderStateBlock _renderStateBlock;

		private LayerMask _layerMask;

		private static readonly ShaderTagId[] ShaderTagIds;

		private static readonly List<ShaderTagId> ShaderTagIdsList;

		public Texture Texture => null;

		public virtual bool Setup(LayerMask layerMask)
		{
			return false;
		}

		public void Dispose()
		{
		}

		public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
		{
		}

		private void ExecutePass(PassData passData, UnsafeGraphContext context)
		{
		}

		public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
		{
		}

		public override void OnCameraCleanup(CommandBuffer cmd)
		{
		}

		private void CreateResources(RenderTextureDescriptor cameraTargetDescriptor, Camera camera)
		{
		}

		private void ReleaseResources()
		{
		}

		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
		}

		private void ExecuteCustomReactiveMask(ScriptableRenderContext context, ref RenderingData renderingData)
		{
		}
	}
}
