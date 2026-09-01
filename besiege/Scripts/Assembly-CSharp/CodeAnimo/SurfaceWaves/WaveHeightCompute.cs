using System;
using CodeAnimo.GPGPU;
using UnityEngine;

namespace CodeAnimo.SurfaceWaves
{
	[AddComponentMenu("Surface Waves/Simulation Steps/Height Compute")]
	public class WaveHeightCompute : SimulationOutput
	{
		public SimulationOutput flowData;

		public WaveSourceList waveInputData;

		public float foamMultiplier = 60f;

		[Range(0f, 0.99f)]
		[SerializeField]
		private float m_foamDecay = 0.95f;

		public float foamDecay
		{
			get
			{
				return m_foamDecay;
			}
			set
			{
				m_foamDecay = Mathf.Clamp(value, 0f, 0.99f);
			}
		}

		protected override void AddMissingComponents()
		{
			base.AddMissingComponents();
			AddComponentIfMissingAndSetup<ComputeKernel2D>();
			AddComponentIfMissingAndSetup<SM3Kernel>();
			AddComponentIfMissingAndSetup<StepStateManager>();
		}

		protected void OnValidate()
		{
			foamDecay = m_foamDecay;
		}

		public override void LoadData()
		{
			FindKernel();
			LoadState();
		}

		public override void RunStep()
		{
			RenderTexture renderTexture = base.outputData;
			renderTexture.filterMode = FilterMode.Point;
			RenderTexture renderTexture2 = flowData.outputData;
			RenderTexture renderTexture3 = waveInputData.outputData;
			RenderTexture renderTexture4 = simTextureManager.CreateOutputTexture("WaveMap");
			if (renderTexture4 == null)
			{
				throw new NullReferenceException("Wave Height Output Texture was not successfully created.");
			}
			if (renderTexture3 == null)
			{
				throw new NullReferenceException("Wave Input Data missing. At least one WaveInput is required");
			}
			if (renderTexture2 == null)
			{
				throw new NullReferenceException("Flow texture missing.");
			}
			if (renderTexture == null)
			{
				throw new NullReferenceException("Previous waveHeight texture missing.");
			}
			RenderTexture newData = computeWaveHeight(renderTexture2, renderTexture4, renderTexture, renderTexture3);
			UpdateOutput(newData);
		}

		private RenderTexture computeWaveHeight(RenderTexture flowMap, RenderTexture waveOut, RenderTexture oldWaveHeight, RenderTexture inputMap)
		{
			simKernel.SetTexture("FlowMapIn", flowMap);
			simKernel.SetTexture("WaveMapIn", oldWaveHeight);
			simKernel.SetTexture("AddedWavesMap", inputMap);
			simKernel.SetTexture("WaveHeightOut", waveOut);
			simKernel.SetFloat("FoamMultiplier", foamMultiplier);
			simKernel.SetFloat("FoamDecay", foamDecay);
			simKernel.Dispatch();
			return waveOut;
		}
	}
}
