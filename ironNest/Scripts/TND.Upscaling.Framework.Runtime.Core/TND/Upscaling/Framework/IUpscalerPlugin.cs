using System;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

namespace TND.Upscaling.Framework
{
	public interface IUpscalerPlugin : IComparable
	{
		string Identifier { get; }

		UpscalerName Name { get; }

		string DisplayName { get; }

		int Priority { get; }

		bool IsSupported { get; }

		bool IsTemporalUpscaler { get; }

		bool SupportsDynamicResolution { get; }

		bool UsesMachineLearning { get; }

		bool IncludesAlphaUpscale { get; }

		bool AcceptsReactiveMask { get; }

		GraphicsFormat ReactiveMaskFormat { get; }

		UpscalerSettingsBase CreateSettings();

		bool TryCreateUpscaler(CommandBuffer commandBuffer, UpscalerSettingsBase settings, in UpscalerInitParams initParams, out IUpscaler upscaler);

		void Cleanup();
	}
}
