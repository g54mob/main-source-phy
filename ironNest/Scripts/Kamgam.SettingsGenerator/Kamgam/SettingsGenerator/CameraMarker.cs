using System.Collections.Generic;
using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	public class CameraMarker<T> : MonoBehaviour
	{
		public static List<CameraMarker<T>> Markers;

		protected Camera _camera;

		public Camera Camera => null;

		public static bool HasValidMarkers()
		{
			return false;
		}

		public static CameraMarker<T> GetFirstValidMarker()
		{
			return null;
		}

		public void OnEnable()
		{
		}

		public void Awake()
		{
		}

		public void OnDestroy()
		{
		}

		public bool IsValid()
		{
			return false;
		}
	}
}
