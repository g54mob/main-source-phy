using Poly.Base;
using Poly.Math;
using UnityEngine;

namespace Poly.Timers
{
	public class MouseActivityMonitor : SingletonBehaviour<MouseActivityMonitor>
	{
		public float minMousePositionSpeedForDetectingActivation = 300f;

		public float minMouseScrollSpeedForDetectingActivation = 1f;

		public float activityCooldown = 0.5f;

		private float timeSinceMouseInactive = float.MaxValue;

		private float lastRealTime;

		private Vec2 lastMousePosition;

		public static float smoothedActivityFactor { get; private set; } = 1f;

		public static void SetTimeSinceInactive(float value)
		{
			if ((bool)SingletonBehaviour<MouseActivityMonitor>.instance)
			{
				SingletonBehaviour<MouseActivityMonitor>.instance.timeSinceMouseInactive = value;
			}
		}

		private void Start()
		{
			lastMousePosition = (Vec2)Input.mousePosition;
			smoothedActivityFactor = 0f;
			lastRealTime = Time.realtimeSinceStartup;
		}

		private void Update()
		{
			Vec2 vec = (Vec2)Input.mousePosition - lastMousePosition;
			lastMousePosition = (Vec2)Input.mousePosition;
			float num = vec.magnitude / Time.deltaTime;
			int num2 = (int)(0u | ((minMousePositionSpeedForDetectingActivation <= num) ? 1u : 0u) | ((minMouseScrollSpeedForDetectingActivation <= Input.mouseScrollDelta.magnitude) ? 1u : 0u) | (Input.GetMouseButton(0) ? 1u : 0u) | (Input.GetMouseButton(1) ? 1u : 0u)) | (Input.GetMouseButton(2) ? 1 : 0);
			float num3 = Time.realtimeSinceStartup - lastRealTime;
			lastRealTime = Time.realtimeSinceStartup;
			if (num2 != 0)
			{
				timeSinceMouseInactive = 0f;
			}
			else
			{
				timeSinceMouseInactive += num3;
			}
			float target = ((timeSinceMouseInactive < activityCooldown) ? 1f : 0f);
			smoothedActivityFactor = Smoothing.Smooth(smoothedActivityFactor, target, 0.9f, num3);
		}
	}
}
