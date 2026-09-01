using FidelityFX.FSR3;
using TND.Upscaling.Framework;
using UnityEngine;
using UnityEngine.Rendering;

namespace TND.Upscaling.FSR3
{
	public class FSR3UpscalerPlugin : UpscalerPlugin<FSR3Upscaler, FSR3UpscalerSettings>
	{
		private Fsr3UpscalerAssets _assets;

		public override UpscalerName Name => default(UpscalerName);

		public override string DisplayName => null;

		public override int Priority => 0;

		public override bool IsSupported => false;

		public override bool IsTemporalUpscaler => false;

		public override bool SupportsDynamicResolution => false;

		public override bool AcceptsReactiveMask => false;

		protected override bool TryCreateUpscaler(CommandBuffer commandBuffer, FSR3UpscalerSettings settings, in UpscalerInitParams initParams, out FSR3Upscaler upscaler)
		{
			upscaler = null;
			return false;
		}

		public override void Cleanup()
		{
		}

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void RegisterUpscalerPlugin()
		{
		}
	}
}
