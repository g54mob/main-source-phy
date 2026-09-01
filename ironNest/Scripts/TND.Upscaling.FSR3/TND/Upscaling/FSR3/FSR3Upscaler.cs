using FidelityFX;
using FidelityFX.FSR3;
using TND.Upscaling.Framework;
using UnityEngine;
using UnityEngine.Rendering;

namespace TND.Upscaling.FSR3
{
	public class FSR3Upscaler : UpscalerBase<FSR3UpscalerSettings>
	{
		public static readonly string DisplayName;

		private readonly Fsr3UpscalerAssets _assets;

		private readonly Fsr3Upscaler.DispatchDescription _dispatchDescription;

		private Fsr3UpscalerContext _context;

		private UpscalerInitParams _initParams;

		private Texture _defaultReactive;

		public FSR3Upscaler(FSR3UpscalerSettings settings, Fsr3UpscalerAssets assets)
			: base((FSR3UpscalerSettings)default(_00210))
		{
		}

		public override bool Initialize(CommandBuffer commandBuffer, in UpscalerInitParams initParams)
		{
			return false;
		}

		public override void Destroy(CommandBuffer commandBuffer)
		{
		}

		public override void Dispatch(CommandBuffer commandBuffer, in UpscalerDispatchParams dispatchParams)
		{
		}

		private static ResourceView ToResourceView(in TextureRef textureRef)
		{
			return default(ResourceView);
		}

		private static ResourceView ToResourceView(Texture texture)
		{
			return default(ResourceView);
		}
	}
}
