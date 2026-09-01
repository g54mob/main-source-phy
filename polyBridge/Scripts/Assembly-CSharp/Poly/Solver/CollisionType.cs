namespace Poly.Solver
{
	public enum CollisionType : byte
	{
		Invalid = 0,
		TwoPolygons = 1,
		PolygonSegment = 2,
		PolygonCircle = 3,
		TwoSegments = 4,
		SegmentCircle = 5,
		TwoCircles = 6
	}
}
