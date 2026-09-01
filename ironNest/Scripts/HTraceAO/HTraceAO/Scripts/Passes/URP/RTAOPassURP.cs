using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.Universal;

namespace HTraceAO.Scripts.Passes.URP
{
	internal class RTAOPassURP : ScriptableRenderPass
	{
		private class PassData
		{
			public UniversalCameraData UniversalCameraData;
		}

		private const string RT_IS_NOT_SUPPORTED_MESSAGE = "Realtime RayTracing is not supported!";

		private const string INLINE_RT_IS_NOT_SUPPORTED_MESSAGE = "Inline RayTracing is not supported!";

		private static readonly int CameraNormalsTexture;

		private ScriptableRenderer _renderer;

		protected internal void Initialize(ScriptableRenderer renderer)
		{
		}

		[Obsolete]
		public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
		{
		}

		[Obsolete]
		public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
		{
		}

		[Obsolete]
		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
		}

		public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
		{
		}

		private void ExecutePass(PassData data, UnsafeGraphContext rgContext)
		{
		}

		private static void SetupShared(Camera camera, float renderScale, RenderTextureDescriptor desc)
		{
		}

		protected internal void Dispose()
		{
		}
	}
}
