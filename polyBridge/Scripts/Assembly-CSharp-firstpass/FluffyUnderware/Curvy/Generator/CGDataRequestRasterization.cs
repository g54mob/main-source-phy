using System;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator
{
	public class CGDataRequestRasterization : CGDataRequestParameter
	{
		public enum ModeEnum
		{
			Even = 0,
			Optimized = 1
		}

		public float Start;

		public float RasterizedRelativeLength;

		public int Resolution;

		[Obsolete("IOnRequestPath.PathLength and CGDataRequestRasterization.SplineAbsoluteLength are no more needed. SplineInputModuleBase.getPathLength is used instead")]
		public float SplineAbsoluteLength;

		public float AngleThreshold;

		public ModeEnum Mode;

		[Obsolete("Use another constructor")]
		public CGDataRequestRasterization(float start, float rasterizedRelativeLength, int resolution, float splineAbsoluteLength, float angle, ModeEnum mode = ModeEnum.Even)
		{
			Start = Mathf.Repeat(start, 1f);
			RasterizedRelativeLength = Mathf.Clamp01(rasterizedRelativeLength);
			Resolution = resolution;
			SplineAbsoluteLength = splineAbsoluteLength;
			AngleThreshold = angle;
			Mode = mode;
		}

		public CGDataRequestRasterization(float start, float rasterizedRelativeLength, int resolution, float angle, ModeEnum mode = ModeEnum.Even)
		{
			Start = Mathf.Repeat(start, 1f);
			RasterizedRelativeLength = Mathf.Clamp01(rasterizedRelativeLength);
			Resolution = resolution;
			AngleThreshold = angle;
			Mode = mode;
		}

		public CGDataRequestRasterization(CGDataRequestRasterization source)
			: this(source.Start, source.RasterizedRelativeLength, source.Resolution, source.AngleThreshold, source.Mode)
		{
		}

		public override bool Equals(object obj)
		{
			if (!(obj is CGDataRequestRasterization cGDataRequestRasterization))
			{
				return false;
			}
			if (Start == cGDataRequestRasterization.Start && RasterizedRelativeLength == cGDataRequestRasterization.RasterizedRelativeLength && Resolution == cGDataRequestRasterization.Resolution && AngleThreshold == cGDataRequestRasterization.AngleThreshold)
			{
				return Mode == cGDataRequestRasterization.Mode;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return new
			{
				A = Start,
				B = RasterizedRelativeLength,
				C = Resolution,
				D = AngleThreshold,
				E = Mode
			}.GetHashCode();
		}
	}
}
