using UnityEngine;
using UnityEngine.Rendering;

namespace FidelityFX.FSR3
{
	internal class Fsr3UpscalerSharpenPass : Fsr3UpscalerPass
	{
		private readonly ComputeBuffer _rcasConstants;

		public Fsr3UpscalerSharpenPass(Fsr3Upscaler.ContextDescription contextDescription, Fsr3UpscalerResources resources, ComputeBuffer constants, ComputeBuffer rcasConstants)
			: base(default(Fsr3Upscaler.ContextDescription), null, null)
		{
		}

		protected override void DoScheduleDispatch(CommandBuffer commandBuffer, Fsr3Upscaler.DispatchDescription dispatchParams, int frameIndex, int dispatchX, int dispatchY)
		{
		}
	}
}
