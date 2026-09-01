using System.ComponentModel;
using UnityEngine;

namespace Lofelt.NiceVibrations
{
	[AddComponentMenu("Nice Vibrations/Haptic Source")]
	public class HapticSource : MonoBehaviour
	{
		private const int DEFAULT_PRIORITY = 128;

		public HapticClip clip;

		public int priority;

		private float seekTime;

		[SerializeField]
		private HapticPatterns.PresetType _fallbackPreset;

		[SerializeField]
		private bool _loop;

		[SerializeField]
		private float _level;

		[SerializeField]
		private float _frequencyShift;

		private static HapticSource loadedHapticSource;

		private static HapticSource lastPlayedHapticSource;

		[DefaultValue(HapticPatterns.PresetType.None)]
		public HapticPatterns.PresetType fallbackPreset
		{
			get
			{
				return default(HapticPatterns.PresetType);
			}
			set
			{
			}
		}

		[DefaultValue(false)]
		public bool loop
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		[DefaultValue(1.0)]
		public float level
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		[DefaultValue(0.0)]
		public float frequencyShift
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		static HapticSource()
		{
		}

		public void Play()
		{
		}

		private bool CanPlay()
		{
			return false;
		}

		private bool IsLoaded()
		{
			return false;
		}

		public void Stop()
		{
		}

		public void Seek(float time)
		{
		}

		public void OnDisable()
		{
		}
	}
}
