using System;
using UnityEngine;

namespace PineUtils
{
	public static class CameraFovAdjuster
	{
		public static void adjust(Camera camera, float refAspectRatio, float refVerticalFovDeg)
		{
			if (camera.aspect >= refAspectRatio)
			{
				camera.fieldOfView = refVerticalFovDeg;
				return;
			}
			float fieldOfView = calcVerticalFovDeg(calcHorizontalFovDeg(refVerticalFovDeg, refAspectRatio), camera.aspect);
			camera.fieldOfView = fieldOfView;
		}

		public static float calcHorizontalFovDeg(float verticalFovDeg, float aspectRatio)
		{
			float num = verticalFovDeg * (MathF.PI / 180f);
			return 2f * Mathf.Atan(Mathf.Tan(num / 2f) * aspectRatio) * 57.29578f;
		}

		public static float calcVerticalFovDeg(float horizontalFovDeg, float aspectRatio)
		{
			float num = horizontalFovDeg * (MathF.PI / 180f);
			return 2f * Mathf.Atan(Mathf.Tan(num / 2f) / aspectRatio) * 57.29578f;
		}
	}
}
