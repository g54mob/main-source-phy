using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.Universal;

namespace HTraceAO.Scripts.Passes.URP
{
	internal class GTAOPassURP : ScriptableRenderPass
	{
		private class PassData
		{
			public UniversalCameraData UniversalCameraData;
		}

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

		private static void ExecutePass(PassData data, UnsafeGraphContext rgContext)
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
