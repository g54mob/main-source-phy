using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.Universal;

namespace TND.Upscaling.Framework.URP
{
	public class AutoReactiveMaskPass : ScriptableRenderPass
	{
		private class PassData
		{
			public TextureHandle activeColorTexture;

			public TextureHandle opaqueOnlyColor;

			public TextureHandle autoReactiveMask;

			public bool useTexArray;

			public int viewCount;

			public Material autoReactiveMaterial;

			public AutoReactiveSettings autoReactiveSettings;
		}

		private const string PassName = "[Upscaler] Auto Reactive Mask Pass";

		private static readonly int MainTexId;

		private static readonly int OpaqueOnlyId;

		private static readonly int ReactiveParamsId;

		private static readonly int ReactiveFlagsId;

		private RTHandle _autoReactiveMask;

		private UpscalerController_URP _currentController;

		private OpaqueCopyPass _opaqueOnlySource;

		private readonly MaterialPropertyBlock _propertyBlock;

		private TextureHandle _autoReactiveMaskHandle;

		public Texture Texture => null;

		public TextureHandle TextureHandle => default(TextureHandle);

		public bool Setup(UpscalerController_URP controller, OpaqueCopyPass opaqueOnlySource)
		{
			return false;
		}

		public void Dispose()
		{
		}

		private RenderTextureDescriptor GetTextureDescriptor(in RenderTextureDescriptor cameraTargetDescriptor)
		{
			return default(RenderTextureDescriptor);
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

		public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
		{
		}
	}
}
