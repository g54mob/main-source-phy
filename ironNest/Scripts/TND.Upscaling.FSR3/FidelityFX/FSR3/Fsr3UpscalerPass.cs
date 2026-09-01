using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.Rendering;

namespace FidelityFX.FSR3
{
	internal abstract class Fsr3UpscalerPass : IDisposable
	{
		protected readonly Fsr3Upscaler.ContextDescription ContextDescription;

		protected readonly Fsr3UpscalerResources Resources;

		protected readonly ComputeBuffer Constants;

		protected ComputeShader ComputeShader;

		protected int KernelIndex;

		private CustomSampler _sampler;

		protected Fsr3UpscalerPass(Fsr3Upscaler.ContextDescription contextDescription, Fsr3UpscalerResources resources, ComputeBuffer constants)
		{
		}

		public virtual void Dispose()
		{
		}

		public void ScheduleDispatch(CommandBuffer commandBuffer, Fsr3Upscaler.DispatchDescription dispatchParams, int frameIndex, int dispatchX, int dispatchY)
		{
		}

		protected abstract void DoScheduleDispatch(CommandBuffer commandBuffer, Fsr3Upscaler.DispatchDescription dispatchParams, int frameIndex, int dispatchX, int dispatchY);

		protected void InitComputeShader(string passName, ComputeShader shader)
		{
		}

		private void InitComputeShader(string passName, ComputeShader shader, Fsr3Upscaler.InitializationFlags flags)
		{
		}

		protected void SetKeyword(string keyword, bool enabled)
		{
		}

		[Conditional("ENABLE_PROFILER")]
		protected void BeginSample(CommandBuffer cmd)
		{
		}

		[Conditional("ENABLE_PROFILER")]
		protected void EndSample(CommandBuffer cmd)
		{
		}
	}
}
