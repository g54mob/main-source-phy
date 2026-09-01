using System;
using UnityEngine;

internal static class CameraExtensions
{
	public static bool IsVisible(this Camera camera, Bounds bounds)
	{
		return GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(camera), bounds);
	}

	public static float FrustumHeight(this Camera camera, float distance)
	{
		if (camera.orthographic)
		{
			return camera.orthographicSize * 2f;
		}
		return 2f * distance * Mathf.Tan(camera.fieldOfView * 0.5f * (MathF.PI / 180f));
	}

	public static float FrustumWidth(this Camera camera, float distance)
	{
		return camera.FrustumHeight(distance) * camera.aspect;
	}
}
