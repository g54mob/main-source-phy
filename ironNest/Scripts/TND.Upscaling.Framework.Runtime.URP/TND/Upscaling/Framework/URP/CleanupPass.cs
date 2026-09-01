using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.Universal;

namespace TND.Upscaling.Framework.URP
{
	public class CleanupPass : ScriptableRenderPass
	{
		private const string PassName = "[Upscaler] Cleanup Pass";

		private Vector2Int _currentRenderSize;

		private Vector2Int _currentDisplaySize;

		private IntPtr _currentCameraDataPtr;

		public virtual bool Setup(in Vector2Int renderSize, in Vector2Int displaySize)
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
	}
}
