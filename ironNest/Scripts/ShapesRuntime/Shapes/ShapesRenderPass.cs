using System;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.Universal;

namespace Shapes
{
	internal class ShapesRenderPass : ScriptableRenderPass
	{
		private class PassData
		{
			public DrawCommand drawCommand;
		}

		private DrawCommand drawCommand;

		private readonly CommandBuffer cmdBuf;

		public ShapesRenderPass Init(DrawCommand drawCommand)
		{
			return null;
		}

		public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
		{
		}

		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled)", false)]
		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
		}

		public override void FrameCleanup(CommandBuffer cmd)
		{
		}
	}
}
