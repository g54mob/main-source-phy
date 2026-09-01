namespace Kamgam.SettingsGenerator
{
	public static class RenderPipelineDetector
	{
		public enum RenderPiplelineType
		{
			URP = 0,
			HDRP = 1,
			BuiltIn = 2
		}

		public static RenderPiplelineType GetCurrentRenderPiplelineType()
		{
			return default(RenderPiplelineType);
		}

		public static bool IsURP()
		{
			return false;
		}

		public static bool IsHDRP()
		{
			return false;
		}

		public static bool IsBuiltIn()
		{
			return false;
		}
	}
}
