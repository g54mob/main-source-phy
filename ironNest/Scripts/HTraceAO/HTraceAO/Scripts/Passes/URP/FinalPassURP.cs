using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.Universal;

namespace HTraceAO.Scripts.Passes.URP
{
	internal class FinalPassURP : ScriptableRenderPass
	{
		private class PassData
		{
			public TextureHandle ColorTexture;

			public UniversalCameraData UniversalCameraData;

			public TextureHandle OutputTarget;
		}

		private const string _OutputTarget = "_OutputTarget";

		private static string s_motionVectorsKeyword;

		internal static ComputeShader HDebug;

		internal static RTHandle OutputTarget;

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

		private static bool DebugModule(CommandBuffer cmd, int width, int height, RTHandle outputTarget)
		{
			return false;
		}

		protected internal void Dispose()
		{
		}
	}
}
