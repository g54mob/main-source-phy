using System;
using HTraceAO.Scripts.Extensions.CameraHistorySystem;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.Universal;

namespace HTraceAO.Scripts.Passes.URP
{
	internal class PrePassURP : ScriptableRenderPass
	{
		private struct HistoryCameraData : ICameraHistoryData
		{
			private int hash;

			public Matrix4x4 previousViewProjMatrix;

			public Matrix4x4 previousInvViewProjMatrix;

			public int GetHash()
			{
				return 0;
			}

			public void SetHash(int hashIn)
			{
			}
		}

		private class PassData
		{
			public RendererListHandle RendererListHandle;

			public UniversalCameraData UniversalCameraData;
		}

		private static Vector4 s_HRenderScalePrevious;

		private static readonly CameraHistorySystem<HistoryCameraData> CameraHistorySystem;

		private static int s_FrameCount;

		private ScriptableRenderer _renderer;

		private RTHandle OwenScrambledRTHandle;

		private RTHandle ScramblingTileXSPPRTHandle;

		private RTHandle RankingTileXSPPRTHandle;

		private RTHandle ScramblingTextureRTHandle;

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

		private static void ExecutePass(PassData data, RasterGraphContext rgContext)
		{
		}

		protected internal void Dispose()
		{
		}
	}
}
