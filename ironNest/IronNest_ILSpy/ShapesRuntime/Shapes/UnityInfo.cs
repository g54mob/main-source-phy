using UnityEngine.Rendering;

namespace Shapes;

internal static class UnityInfo
{
	public const int INSTANCES_MAX = 1023;

	public static bool UsingSRP
	{
		get
		{
			RenderPipelineAsset defaultRenderPipeline = GraphicsSettings.defaultRenderPipeline;
			return defaultRenderPipeline != null;
		}
	}
}
