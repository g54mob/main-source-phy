using System;
using Unity.Cinemachine;
using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	public class FieldOfViewConnection : Connection<float>
	{
		public const float DefaultFallback = 60f;

		public bool UseMain;

		public bool UseMarkers;

		[NonSerialized]
		protected float _fieldOfView;

		public bool trySetCinemachineValue(Camera camera, float value)
		{
			return false;
		}

		public bool tryGetCinemachineValue(Camera camera, out float fieldOfView)
		{
			fieldOfView = default(float);
			return false;
		}

		private static bool tryGetCinemachineCamera(Camera camera, out CinemachineCamera cinemaCamera)
		{
			cinemaCamera = null;
			return false;
		}

		public FieldOfViewConnection(bool useMain = true, bool useMarkers = true)
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

		public override void Set(float fieldOfView)
		{
		}
	}
}
