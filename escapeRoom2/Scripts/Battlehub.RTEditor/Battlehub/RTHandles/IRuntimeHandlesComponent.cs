namespace Battlehub.RTHandles
{
	public interface IRuntimeHandlesComponent
	{
		RTHColors Colors { get; set; }

		float HandleScale { get; set; }

		float SelectionMargin { get; set; }

		float SelectionMarginPixels { get; set; }

		bool InvertZAxis { get; set; }

		bool PositionHandleArrowOnly { get; set; }

		float SceneGizmoScale { get; set; }

		void ApplySettings();
	}
}
