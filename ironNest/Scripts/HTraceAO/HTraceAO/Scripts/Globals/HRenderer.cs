using UnityEngine;

namespace HTraceAO.Scripts.Globals
{
	public static class HRenderer
	{
		private static HRenderPipeline s_CurrentHRenderPipeline;

		private static RenderTexture emptyTexture;

		private static Mesh _fullscreenTriangle;

		public static HRenderPipeline CurrentHRenderPipeline => default(HRenderPipeline);

		public static bool SupportsInlineRayTracing => false;

		public static bool SupportsRayTracing => false;

		public static bool RayTracingExecutionCheck => false;

		public static bool RenderGraphEnabled => false;

		public static int TextureXrSlices => 0;

		public static RenderTexture EmptyTexture => null;

		public static Mesh FullscreenTriangle => null;

		private static HRenderPipeline GetRenderPipeline()
		{
			return default(HRenderPipeline);
		}
	}
}
