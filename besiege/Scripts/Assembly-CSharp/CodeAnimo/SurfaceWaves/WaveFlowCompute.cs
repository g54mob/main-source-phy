using System;
using CodeAnimo.GPGPU;
using UnityEngine;

namespace CodeAnimo.SurfaceWaves
{
	[AddComponentMenu("Surface Waves/Simulation Steps/Flow Compute")]
	public class WaveFlowCompute : SimulationOutput
	{
		public SimulationOutput heightOffsetData;

		public SimulationOutput waveHeightData;

		[Range(0f, 1f)]
		public float flowDamping = 0.9999f;

		[Range(0.001f, 0.45f)]
		public float deltaTime = 0.08f;

		protected override void AddMissingComponents()
		{
			base.AddMissingComponents();
			AddComponentIfMissingAndSetup<ComputeKernel2D>();
			AddComponentIfMissingAndSetup<SM3Kernel>();
			AddComponentIfMissingAndSetup<StepStateManager>();
		}

		public override void LoadData()
		{
			FindKernel();
			LoadState();
		}

		public override void RunStep()
		{
			RenderTexture renderTexture = base.outputData;
			RenderTexture renderTexture2 = simTextureManager.CreateOutputTexture("FlowMap");
			RenderTexture renderTexture3 = waveHeightData.outputData;
			RenderTexture renderTexture4 = heightOffsetData.outputData;
			if (renderTexture2 == null)
			{
				throw new NullReferenceException("Flow Output Texture not successfully created");
			}
			if (renderTexture == null)
			{
				throw new NullReferenceException("Old Flow texture missing");
			}
			if (renderTexture3 == null)
			{
				throw new NullReferenceException("Wave Height texture missing");
			}
			if (renderTexture4 == null)
			{
				throw new NullReferenceException("Wave Height Offset texture missing");
			}
			RenderTexture newData = ComputeFlow(renderTexture2, renderTexture, renderTexture3, renderTexture4);
			UpdateOutput(newData);
		}

		private RenderTexture ComputeFlow(RenderTexture flowOut, RenderTexture oldFlow, RenderTexture waveHeight, RenderTexture waveOffset)
		{
			simKernel.SetFloat("TimeStep", deltaTime);
			simKernel.SetFloat("FlowDamping", flowDamping);
			simKernel.SetTexture("FlowMapIn", oldFlow);
			simKernel.SetTexture("WaveMapIn", waveHeight);
			simKernel.SetTexture("HeightOffsetIn", waveOffset);
			simKernel.SetTexture("FlowMapOut", flowOut);
			simKernel.Dispatch();
			return flowOut;
		}
	}
}
