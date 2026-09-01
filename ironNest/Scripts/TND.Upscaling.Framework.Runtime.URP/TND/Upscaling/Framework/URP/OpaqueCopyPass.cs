using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.Universal;

namespace TND.Upscaling.Framework.URP
{
	public class OpaqueCopyPass : ScriptableRenderPass
	{
		private class PassData
		{
			public TextureHandle activeColorTexture;

			public TextureHandle opaqueOnlyColor;
		}

		private const string PassName = "[Upscaler] Opaque-Only Copy";

		private RTHandle _opaqueOnlyColor;

		private TextureHandle _opaqueOnlyColorHandle;

		public Texture Texture => null;

		public TextureHandle TextureHandle => default(TextureHandle);

		public void Dispose()
		{
		}

		private RenderTextureDescriptor GetTextureDescriptor(in RenderTextureDescriptor cameraTargetDescriptor)
		{
			return default(RenderTextureDescriptor);
		}

		public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
		{
		}

		public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
		{
		}

		public override void OnCameraCleanup(CommandBuffer cmd)
		{
		}

		private void CreateResources(in RenderTextureDescriptor cameraTargetDescriptor)
		{
		}

		private void ReleaseResources()
		{
		}

		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
		}
	}
}
