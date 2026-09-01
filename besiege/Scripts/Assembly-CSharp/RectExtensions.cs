using UnityEngine;

public static class RectExtensions
{
	public static Rect Intersection(this Rect rect, Rect other)
	{
		if (!rect.Overlaps(other))
		{
			return default(Rect);
		}
		return new Rect
		{
			xMin = Mathf.Max(rect.xMin, other.xMin),
			yMin = Mathf.Max(rect.yMin, other.yMin),
			xMax = Mathf.Min(rect.xMax, other.xMax),
			yMax = Mathf.Min(rect.yMax, other.yMax)
		};
	}
}
