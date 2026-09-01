using UnityEngine;
using UnityEngine.Rendering;

namespace FidelityFX.FSR3
{
	internal class Fsr3UpscalerShadingChangePyramidPass : Fsr3UpscalerPass
	{
		private readonly ComputeBuffer _spdConstants;

		public Fsr3UpscalerShadingChangePyramidPass(Fsr3Upscaler.ContextDescription contextDescription, Fsr3UpscalerResources resources, ComputeBuffer constants, ComputeBuffer spdConstants)
			: base(default(Fsr3Upscaler.ContextDescription), null, null)
		{
		}

		protected override void DoScheduleDispatch(CommandBuffer commandBuffer, Fsr3Upscaler.DispatchDescription dispatchParams, int frameIndex, int dispatchX, int dispatchY)
		{
		}
	}
}
