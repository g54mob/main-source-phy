using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.Universal;

namespace TND.Upscaling.Framework.URP
{
	public class SetupPass : ScriptableRenderPass
	{
		private const string PassName = "[Upscaler] Setup Pass";

		private UpscalerController_URP _currentController;

		private UpscalingRenderPass _upscalingRenderPass;

		private AutoReactiveMaskPass _autoReactiveMaskPass;

		public bool Setup(UpscalerController_URP controller, UpscalingRenderPass upscalingRenderPass, AutoReactiveMaskPass autoReactiveMaskPass)
		{
			return false;
		}

		public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
		{
		}

		public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
		{
		}

		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
		}

		public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
		{
		}

		private void PatchUpscalingRenderPassEvent(int offset)
		{
		}
	}
}
