using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Kamgam.SettingsGenerator
{
	public class SettingsVolume : MonoBehaviour
	{
		private static SettingsVolume _instance;

		[NonSerialized]
		public Volume Volume;

		private static float _defaultPriority;

		protected List<ISettingsVolumeControl> _controls;

		[NonSerialized]
		protected bool _volumeWasRegisteredWithMananger;

		public static SettingsVolume Instance => null;

		public static float Priority
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		protected virtual void createVolume()
		{
		}

		public void MatchMainCameraLayer()
		{
		}

		public TComp GetOrAddComponent<TComp>()
		{
			return default(TComp);
		}

		public T GetOrCreateControl<T>() where T : new()
		{
			return default(T);
		}

		public T GetOrCreateControl<T>(out bool isNew) where T : new()
		{
			isNew = default(bool);
			return default(T);
		}

		public T FindDefaultVolumeComponent<T>(bool useStackAsFallback = false, int layerMask = -1)
		{
			return default(T);
		}

		private static Volume[] findVolumesInActiveScene(bool includeInactive = false)
		{
			return null;
		}

		private static void registerWithVolumeManager(Volume volume, int layer)
		{
		}

		private static void unregisterFromVolumeManager(Volume volume, int layer)
		{
		}

		public void Update()
		{
		}
	}
}
