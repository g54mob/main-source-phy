using System;
using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	public class CameraClipConnection : Connection<float>
	{
		public enum ClippingMode
		{
			Near = 0,
			Far = 1
		}

		public const float DefaultFallbackNear = 0.3f;

		public const float DefaultFallbackFar = 1000f;

		public bool UseMain;

		public bool UseMarkers;

		public ClippingMode Mode;

		public float ClipMin;

		public float ClipMax;

		[NonSerialized]
		protected float _clipValue;

		public CameraClipConnection(ClippingMode mode = ClippingMode.Far, float clipMin = 1f, float clipMax = 1000f, bool useMain = true, bool useMarkers = true)
		{
		}

		protected void onNewCamera(Camera cam)
		{
		}

		public void Apply()
		{
		}

		public override float Get()
		{
			return 0f;
		}

		public override void Set(float value)
		{
		}

		public float getClipValue(Camera cam)
		{
			return 0f;
		}

		public void setClipValue(Camera cam, float value)
		{
		}
	}
}
