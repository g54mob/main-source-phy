using UnityEngine;
using UnityEngine.Rendering;

namespace TND.Upscaling.Framework
{
	public interface IUpscaler
	{
		Vector2Int MinimumRenderSize { get; }

		bool RequiresOpaqueOnlyInput { get; }

		bool RequiresRandomWriteOutput { get; }

		bool Initialize(CommandBuffer commandBuffer, in UpscalerInitParams initParams);

		void Dispatch(CommandBuffer commandBuffer, in UpscalerDispatchParams dispatchParams);

		void Destroy(CommandBuffer commandBuffer);

		bool RestartRequired();

		Vector2 GetJitterOffset(int frameIndex, int renderWidth, int upscaleWidth);
	}
}
