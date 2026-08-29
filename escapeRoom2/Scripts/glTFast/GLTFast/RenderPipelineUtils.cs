using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace GLTFast
{
	public static class RenderPipelineUtils
	{
		private static RenderPipeline s_RenderPipeline;

		public static RenderPipeline RenderPipeline
		{
			get
			{
				if (s_RenderPipeline == RenderPipeline.Unknown)
				{
					s_RenderPipeline = DetectRenderPipeline();
				}
				return s_RenderPipeline;
			}
		}

		private static RenderPipeline DetectRenderPipeline()
		{
			RenderPipelineAsset renderPipelineAsset = (QualitySettings.renderPipeline ? QualitySettings.renderPipeline : GraphicsSettings.defaultRenderPipeline);
			if (renderPipelineAsset != null)
			{
				if (renderPipelineAsset is HDRenderPipelineAsset)
				{
					return RenderPipeline.HighDefinition;
				}
				throw new Exception("glTFast: Unknown Render Pipeline");
			}
			return RenderPipeline.BuiltIn;
		}
	}
}
