using UnityEngine.Rendering;

namespace Battlehub
{
	public static class GraphicsSettingsExt
	{
		public static RenderPipelineAsset renderPipelineAsset
		{
			get
			{
				return GraphicsSettings.defaultRenderPipeline;
			}
			set
			{
				GraphicsSettings.defaultRenderPipeline = value;
			}
		}
	}
}
