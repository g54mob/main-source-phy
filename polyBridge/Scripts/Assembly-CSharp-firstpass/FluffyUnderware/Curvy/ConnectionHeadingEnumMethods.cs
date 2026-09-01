namespace FluffyUnderware.Curvy
{
	public static class ConnectionHeadingEnumMethods
	{
		public static ConnectionHeadingEnum ResolveAuto(this ConnectionHeadingEnum heading, CurvySplineSegment followUp)
		{
			if (heading == ConnectionHeadingEnum.Auto)
			{
				heading = ((followUp.Spline.FirstVisibleControlPoint == followUp) ? ConnectionHeadingEnum.Plus : ((followUp.Spline.LastVisibleControlPoint == followUp) ? ConnectionHeadingEnum.Minus : ConnectionHeadingEnum.Sharp));
			}
			return heading;
		}
	}
}
