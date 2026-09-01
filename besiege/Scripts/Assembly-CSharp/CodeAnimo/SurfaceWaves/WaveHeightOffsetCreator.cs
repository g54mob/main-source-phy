using System;
using CodeAnimo.GPGPU;
using UnityEngine;

namespace CodeAnimo.SurfaceWaves
{
	[AddComponentMenu("Surface Waves/Simulation Steps/Wave Height Offset Creator")]
	public class WaveHeightOffsetCreator : SimulationOutput
	{
		public TerrainHeightRenderer groundDepthData;

		public WaveDisplaceRenderer displaceDepthData;

		public WaveHeightCompute waveMapSource;

		public float WaveHeightScale = 10f;

		protected override void AddMissingComponents()
		{
			base.AddMissingComponents();
			AddComponentIfMissingAndSetup<ComputeKernel2D>();
			AddComponentIfMissingAndSetup<SM3Kernel>();
			AddComponentIfMissingAndSetup<StepStateManager>();
		}

		public override void LoadData()
		{
			FindTextureManager();
			FindKernel();
		}

		public override void RunStep()
		{
			RenderTexture renderTexture = simTextureManager.CreateOutputTexture("waveHeightOffset");
			RenderTexture renderTexture2 = groundDepthData.outputData;
			RenderTexture displaceDepth = ((!displaceDepthData) ? null : displaceDepthData.outputData);
			RenderTexture waveMap = ((!waveMapSource) ? null : waveMapSource.outputData);
			if (renderTexture == null)
			{
				throw new NullReferenceException();
			}
			if (renderTexture2 == null)
			{
				throw new NullReferenceException();
			}
			CalculateWaveHeightOffset(renderTexture, renderTexture2, displaceDepth, waveMap, WaveHeightScale);
			UpdateOutput(renderTexture);
		}

		private RenderTexture CalculateWaveHeightOffset(RenderTexture waveHeightOffset, RenderTexture groundDepth, RenderTexture displaceDepth, RenderTexture waveMap, float waveHeightScale)
		{
			simKernel.SetTexture("GroundDepth", groundDepth);
			if (displaceDepth != null)
			{
				simKernel.SetTexture("DisplaceDepth", displaceDepth);
			}
			else
			{
				simKernel.SetTexture("DisplaceDepth", simTextureManager.GetClearTexture());
			}
			if (waveMap != null)
			{
				simKernel.SetTexture("WaveMapIn", waveMap);
			}
			else
			{
				simKernel.SetTexture("WaveMapIn", simTextureManager.GetClearTexture());
			}
			simKernel.SetFloat("customFarClip", groundDepthData.FarClipPlane);
			simKernel.SetFloat("customNearClip", groundDepthData.NearClipPlane);
			simKernel.SetFloat("groundDepthOffset", groundDepthData.CameraHeightOffset);
			simKernel.SetFloat("groundDepthScale", waveHeightScale);
			simKernel.SetTexture("DisplacementOut", waveHeightOffset);
			simKernel.Dispatch();
			return waveHeightOffset;
		}
	}
}
