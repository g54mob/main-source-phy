using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.Universal;

namespace TND.Upscaling.Framework.URP
{
	public class UpscalingRenderPass : CameraJitterPass
	{
		private class PassData
		{
			public UniversalCameraData cameraData;

			public TextureHandle activeColorTexture;

			public TextureHandle colorBuffer;

			public TextureHandle depthBuffer;

			public TextureHandle motionVectorBuffer;

			public TextureHandle opaqueOnly;

			public TextureHandle autoReactiveMask;

			public TextureHandle outputColor;

			public TextureHandle outputDepth;

			public Vector2Int displaySize;

			public int viewCount;

			public bool upsampleDepth;
		}

		private class UpdateCameraResolutionPassData
		{
			public Vector2Int newCameraTargetSize;
		}

		private const string PassName = "[Upscaler] Upscaling Pass";

		private const GraphicsFormat DepthStencilFormat = GraphicsFormat.D32_SFloat_S8_UInt;

		private static readonly int DepthTexturePropertyID;

		private static readonly int InputDepthPropertyID;

		private static readonly int MotionTexturePropertyID;

		private static readonly int BlitScaleBiasID;

		private static readonly int ViewIndexID;

		private UniversalRenderPipelineAsset _currentRenderPipeline;

		private OpaqueCopyPass _currentOpaqueOnlySource;

		private AutoReactiveMaskPass _currentAutoReactiveSource;

		private IntPtr _currentCameraDataPtr;

		private Material _copyDepthMaterial;

		private Material _upsampleDepthMaterial;

		private readonly MaterialPropertyBlock _copyDepthProperties;

		private readonly MaterialPropertyBlock _upsampleDepthProperties;

		private RTHandle _upscalerOutput;

		private RTHandle _upsampledDepth;

		private readonly List<RTHandle> _tempTextures;

		private readonly TextureRef.BlitterDelegate _inputColorBlitter;

		private readonly TextureRef.BlitterDelegate _inputDepthBlitter;

		private readonly TextureRef.BlitterDelegate _inputMotionBlitter;

		private static readonly int ScreenSizePropertyID;

		private static readonly int ScaledScreenParamsPropertyID;

		private readonly BaseRenderFunc<PassData, UnsafeGraphContext> _executePassDelegate;

		public bool Setup(UniversalRenderPipelineAsset renderPipeline, UpscalerController_URP controller, bool usingRenderGraph, Material copyDepthMaterial, Material upsampleDepthMaterial, OpaqueCopyPass opaqueOnlySource, AutoReactiveMaskPass autoReactiveMaskPass)
		{
			return false;
		}

		public void Dispose()
		{
		}

		private void GetOutputDescriptors(in RenderTextureDescriptor cameraTargetDescriptor, in Vector2Int displaySize, out RenderTextureDescriptor outColorDescriptor, out RenderTextureDescriptor outDepthDescriptor)
		{
			outColorDescriptor = default(RenderTextureDescriptor);
			outDepthDescriptor = default(RenderTextureDescriptor);
		}

		public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
		{
		}

		public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
		{
		}

		public override void OnCameraCleanup(CommandBuffer cmd)
		{
		}

		private void CreateResources(RenderTextureDescriptor cameraTargetDescriptor, in Vector2Int displaySize)
		{
		}

		private void ReleaseResources()
		{
		}

		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
		}

		private void DispatchUpscaler(CommandBuffer cmd, in Matrix4x4 nonJitteredProjMatrix, int viewIndex, in TextureRef colorBuffer, in TextureRef depthBuffer, in TextureRef motionVectorBuffer, in TextureRef opaqueOnly, in TextureRef autoReactiveMask, in TextureRef output)
		{
		}

		private void UpsampleDepth(CommandBuffer cmd, ScriptableRenderer renderer, in Vector2Int displaySize, bool postProcessingEnabled, RenderTargetIdentifier inputDepth, int viewCount, Texture outputDepth)
		{
		}

		private void UpsampleDepth(CommandBuffer cmd, in Vector2Int displaySize, RenderTargetIdentifier inputDepth, int viewCount, Texture outputDepth)
		{
		}

		private Texture BlitColor(CommandBuffer cmd, in RenderTextureDescriptor desc, in RenderTargetIdentifier source)
		{
			return null;
		}

		private Texture BlitDepth(CommandBuffer cmd, in RenderTextureDescriptor desc, in RenderTargetIdentifier source)
		{
			return null;
		}

		private Texture BlitMotion(CommandBuffer cmd, in RenderTextureDescriptor desc, in RenderTargetIdentifier source)
		{
			return null;
		}

		private RTHandle AllocateTempTexture(in RenderTextureDescriptor desc, string name)
		{
			return null;
		}

		public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
		{
		}

		private void ExecutePass(PassData passData, UnsafeGraphContext context)
		{
		}

		private static void UpdateCameraResolution(RenderGraph renderGraph, UniversalCameraData cameraData, in RenderTextureDescriptor upscaledDesc)
		{
		}
	}
}
