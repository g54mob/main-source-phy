using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace TND.Upscaling.Framework.URP
{
	[DisallowMultipleRendererFeature("TND Upscaling")]
	public class UpscalingRendererFeature : ScriptableRendererFeature
	{
		public Shader copyDepthShader;

		public Shader upsampleDepthShader;

		private UniversalRenderPipelineAsset _renderPipelineAsset;

		private bool _usingRenderGraph;

		private UpscalingRenderPass _upscalingRenderPass;

		private OpaqueCopyPass _opaqueCopyPass;

		private AutoReactiveMaskPass _autoReactiveMaskPass;

		private CustomReactiveMaskPass _customReactiveMaskPass;

		private SetupPass _setupPass;

		private CleanupPass _cleanupPass;

		private Material _copyDepthMaterial;

		private Material _upsampleDepthMaterial;

		public override void Create()
		{
		}

		private void AddBeginCameraRenderingDelegate()
		{
		}

		protected override void Dispose(bool disposing)
		{
		}

		public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
		{
		}

		public override void OnCameraPreCull(ScriptableRenderer renderer, in CameraData cameraData)
		{
		}

		private void BeginCameraRendering(ScriptableRenderContext context, Camera camera)
		{
		}

		private void EndCameraRendering(ScriptableRenderContext context, Camera camera)
		{
		}
	}
}
