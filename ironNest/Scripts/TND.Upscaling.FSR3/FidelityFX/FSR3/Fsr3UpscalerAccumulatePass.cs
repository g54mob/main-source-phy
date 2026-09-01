using UnityEngine;
using UnityEngine.Rendering;

namespace FidelityFX.FSR3
{
	internal class Fsr3UpscalerAccumulatePass : Fsr3UpscalerPass
	{
		private const string SharpeningKeyword = "FFX_FSR3UPSCALER_OPTION_APPLY_SHARPENING";

		private readonly LocalKeyword _sharpeningKeyword;

		public Fsr3UpscalerAccumulatePass(Fsr3Upscaler.ContextDescription contextDescription, Fsr3UpscalerResources resources, ComputeBuffer constants)
			: base(default(Fsr3Upscaler.ContextDescription), null, null)
		{
		}

		protected override void DoScheduleDispatch(CommandBuffer commandBuffer, Fsr3Upscaler.DispatchDescription dispatchParams, int frameIndex, int dispatchX, int dispatchY)
		{
		}
	}
}
