using System;
using UnityEngine;

namespace FidelityFX.FSR3
{
	[Serializable]
	public class Fsr3UpscalerShaders
	{
		public ComputeShader prepareInputsPass;

		public ComputeShader lumaPyramidPass;

		public ComputeShader shadingChangePyramidPass;

		public ComputeShader shadingChangePass;

		public ComputeShader prepareReactivityPass;

		public ComputeShader lumaInstabilityPass;

		public ComputeShader accumulatePass;

		public ComputeShader sharpenPass;

		public ComputeShader autoGenReactivePass;

		public ComputeShader debugViewPass;

		public Fsr3UpscalerShaders Clone()
		{
			return null;
		}

		public Fsr3UpscalerShaders DeepCopy()
		{
			return null;
		}

		public void Dispose()
		{
		}
	}
}
