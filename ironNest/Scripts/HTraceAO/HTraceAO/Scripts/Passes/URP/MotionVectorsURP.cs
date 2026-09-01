using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.Universal;

namespace HTraceAO.Scripts.Passes.URP
{
	internal class MotionVectorsURP : ScriptableRenderPass
	{
		private class ObjectMVPassData
		{
			public RendererListHandle RendererListHandle;
		}

		private class CameraMVPassData
		{
			public TextureHandle ObjectMotionVectorsColor;

			public TextureHandle ObjectMotionVectorsDepth;
		}

		private const string _ObjectMotionVectorsColorURP = "_ObjectMotionVectorsColorURP";

		private const string _ObjectMotionVectorsDepthURP = "_ObjectMotionVectorsDepthURP";

		private const string _CustomCameraMotionVectorsURP_0 = "_CustomCameraMotionVectorsURP_0";

		private const string _CustomCameraMotionVectorsURP_1 = "_CustomCameraMotionVectorsURP_1";

		private static readonly int _ObjectMotionVectorsColor;

		private static readonly int _ObjectMotionVectorsDepth;

		private static readonly int _BiasOffset;

		private static readonly ShaderTagId[] motionVectorsShaderTags;

		private static readonly RenderTargetIdentifier[] motionVectorsMRT_Objects;

		private static readonly RenderTargetIdentifier[] motionVectorsMRT_Camera;

		internal static RTHandle[] CustomCameraMotionVectorsURP;

		internal static RTHandle ObjectMotionVectorsColorURP;

		internal static RTHandle ObjectMotionVectorsDepthURP;

		private static Material MotionVectorsMaterial_URP;

		private static readonly ProfilingSampler ObjectMVProfilingSampler;

		private static readonly ProfilingSampler CameraMVProfilingSampler;

		private static RenderStateBlock forwardGBufferRenderStateBlock;

		private ScriptableRenderer _renderer;

		private static int _historyCameraIndex;

		protected internal void Initialize(ScriptableRenderer renderer)
		{
		}

		[Obsolete]
		public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
		{
		}

		private static void Setup(Camera camera, float renderScale, RenderTextureDescriptor desc)
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

		private void RenderMotionVectorsNonRenderGraph(CommandBuffer cmd, Camera camera, ref RenderingData renderingData, ref ScriptableRenderContext context)
		{
		}

		public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
		{
		}

		private static void AddRendererList(RenderGraph renderGraph, UniversalCameraData universalCameraData, UniversalRenderingData universalRenderingData, ObjectMVPassData objectMvPassData, IRasterRenderGraphBuilder builder)
		{
		}

		protected internal void Dispose()
		{
		}
	}
}
