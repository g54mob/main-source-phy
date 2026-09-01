using UnityEngine;
using UnityEngine.Rendering;

namespace TND.Upscaling.Framework
{
	public abstract class UpscalerBase : IUpscaler
	{
		private ShaderRef _sharpenShader;

		private Material _sharpenMaterial;

		private static readonly int MainTexProperty;

		private static readonly int SharpnessProperty;

		public virtual Vector2Int MinimumRenderSize => default(Vector2Int);

		public virtual bool RequiresOpaqueOnlyInput => false;

		public virtual bool RequiresRandomWriteOutput => false;

		public abstract bool Initialize(CommandBuffer commandBuffer, in UpscalerInitParams initParams);

		public abstract void Dispatch(CommandBuffer commandBuffer, in UpscalerDispatchParams dispatchParams);

		public virtual void Destroy(CommandBuffer commandBuffer)
		{
		}

		public virtual bool RestartRequired()
		{
			return false;
		}

		public virtual Vector2 GetJitterOffset(int frameIndex, int renderWidth, int upscaleWidth)
		{
			return default(Vector2);
		}

		public static float Halton(int index, int radix)
		{
			return 0f;
		}

		protected void SharpenPass(CommandBuffer cmd, RenderTargetIdentifier inputColor, RenderTargetIdentifier outputColor, float sharpness)
		{
		}
	}
	public abstract class UpscalerBase<TSettings> : UpscalerBase
	{
		protected TSettings Settings { get; }

		protected UpscalerBase(TSettings settings)
		{
		}

		public override bool RestartRequired()
		{
			return false;
		}
	}
}
