using System;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

namespace TND.Upscaling.Framework
{
	public abstract class UpscalerPlugin<TUpscaler, TSettings> : IUpscalerPlugin, IComparable
	{
		public string Identifier => null;

		public abstract UpscalerName Name { get; }

		public abstract string DisplayName { get; }

		public abstract int Priority { get; }

		public abstract bool IsSupported { get; }

		public abstract bool IsTemporalUpscaler { get; }

		public virtual bool SupportsDynamicResolution => false;

		public virtual bool UsesMachineLearning => false;

		public virtual bool IncludesAlphaUpscale => false;

		public virtual bool AcceptsReactiveMask => false;

		public virtual GraphicsFormat ReactiveMaskFormat => default(GraphicsFormat);

		public UpscalerSettingsBase CreateSettings()
		{
			return null;
		}

		public bool TryCreateUpscaler(CommandBuffer commandBuffer, UpscalerSettingsBase settings, in UpscalerInitParams initParams, out IUpscaler upscaler)
		{
			upscaler = null;
			return false;
		}

		protected abstract bool TryCreateUpscaler(CommandBuffer commandBuffer, TSettings settings, in UpscalerInitParams initParams, out TUpscaler upscaler);

		public virtual void Cleanup()
		{
		}

		[Obsolete("Use the generic version of this method")]
		protected static void RegisterUpscalerPlugin(IUpscalerPlugin upscalerPlugin)
		{
		}

		protected static void RegisterUpscalerPlugin<TUpscalerPlugin>() where TUpscalerPlugin : new()
		{
		}

		public int CompareTo(object obj)
		{
			return 0;
		}

		bool IUpscalerPlugin.TryCreateUpscaler(CommandBuffer commandBuffer, UpscalerSettingsBase settings, in UpscalerInitParams initParams, out IUpscaler upscaler)
		{
			upscaler = null;
			return false;
		}
	}
}
