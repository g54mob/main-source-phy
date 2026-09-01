using UnityEngine;
using UnityEngine.Rendering;

namespace FidelityFX.FSR3
{
	internal class Fsr3UpscalerGenerateReactivePass : Fsr3UpscalerPass
	{
		private readonly ComputeBuffer _generateReactiveConstants;

		public Fsr3UpscalerGenerateReactivePass(Fsr3Upscaler.ContextDescription contextDescription, Fsr3UpscalerResources resources, ComputeBuffer generateReactiveConstants)
			: base(default(Fsr3Upscaler.ContextDescription), null, null)
		{
		}

		protected override void DoScheduleDispatch(CommandBuffer commandBuffer, Fsr3Upscaler.DispatchDescription dispatchParams, int frameIndex, int dispatchX, int dispatchY)
		{
		}

		public void ScheduleDispatch(CommandBuffer commandBuffer, Fsr3Upscaler.GenerateReactiveDescription dispatchParams, int dispatchX, int dispatchY)
		{
		}
	}
}
