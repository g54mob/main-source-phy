namespace Shapes
{
	public interface IDashable
	{
		bool MatchDashSpacingToSize { get; set; }

		bool Dashed { get; set; }

		float DashSize { get; set; }

		float DashSpacing { get; set; }

		float DashOffset { get; set; }

		DashSpace DashSpace { get; set; }

		DashSnapping DashSnap { get; set; }

		DashType DashType { get; set; }

		float DashShapeModifier { get; set; }
	}
}
