using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TFBGames
{
	[CreateAssetMenu(fileName = "SettingsProfile", menuName = "TABS/SettingsProfile", order = 1)]
	public class SettingsProfile : ScriptableObject, ISerializationCallbackReceiver
	{
		public enum ResolutionLabel
		{
			Low = 0,
			Medium = 1,
			High = 2
		}

		[Serializable]
		public struct ForcedResolutions
		{
			public ResolutionLabel mode;

			public Vector2Int size;
		}

		[Serializable]
		public struct ProfileResolution
		{
			public int width;

			public int height;
		}

		[Serializable]
		public struct SettingsOverride
		{
			public string settingKey;

			public int value;

			public float sliderValue;
		}

		[SerializeField]
		protected SettingsInstance.Platform profilePlatform;

		[SerializeField]
		[Tooltip("Force the ProjectileStick to be hard.")]
		protected bool useHardProjectileStick;

		[SerializeField]
		[Tooltip("Max number of units the user can place. (Set to zero or less to disable.)")]
		protected int userPlacementMaxUnits;

		[SerializeField]
		[Header("Switch Platform Settings")]
		[Tooltip("Switch specific platform settings for changing resolutions when docking and undocking")]
		protected ForcedResolutions[] forceResolutions;

		[SerializeField]
		protected bool applyResolutionOnProfileChange;

		[SerializeField]
		protected ProfileResolution resolution;

		[SerializeField]
		protected float foliageFadeDistanceMultiplier = 1f;

		[Header("Project Mars")]
		[SerializeField]
		[Tooltip("Max martians. (Set to zero or less to disable.)")]
		protected int maxMartians = 40;

		[SerializeField]
		protected int currentMartians;

		[Header("Settings overrides")]
		[SerializeField]
		protected SettingsOverride[] settingsOverrides;

		[SerializeField]
		protected bool enforceControllerConnection;

		[SerializeField]
		protected bool setFrameRateTarget = true;

		[SerializeField]
		protected PerformanceCounterService.FrameRateTarget frameRateTarget;

		[SerializeField]
		protected bool allowForcedSsao;

		private Dictionary<string, SettingsOverride> settingsOverridesMap;

		public List<MultiplayerPlatform> AllowedMultiplayerPlatforms;

		public bool UseHardProjectileStick => useHardProjectileStick;

		public int? UserPlacementMaxUnits
		{
			get
			{
				if (userPlacementMaxUnits <= 0)
				{
					return null;
				}
				return userPlacementMaxUnits;
			}
		}

		public int? MultiplayerMaxUnits
		{
			get
			{
				if (maxMartians <= 0)
				{
					return null;
				}
				return maxMartians;
			}
		}

		public int? CurrentMartians
		{
			get
			{
				return currentMartians;
			}
			set
			{
				currentMartians = ((value <= maxMartians) ? value : new int?(maxMartians)).Value;
			}
		}

		public ForcedResolutions[] ForceResolutions => forceResolutions;

		public bool ApplyResolution => applyResolutionOnProfileChange;

		public ProfileResolution Resolution => resolution;

		public float FoliageFadeDistanceMultiplier => foliageFadeDistanceMultiplier;

		public bool EnforceControllerConnection => enforceControllerConnection;

		public PerformanceCounterService.FrameRateTarget? FrameRateTarget
		{
			get
			{
				if (!setFrameRateTarget)
				{
					return null;
				}
				return frameRateTarget;
			}
		}

		public bool AllowForcedSsao => allowForcedSsao;

		public bool TryGetSettingsOverride(string settingsName, out SettingsOverride value)
		{
			return settingsOverridesMap.TryGetValue(settingsName, out value);
		}

		public void OnAfterDeserialize()
		{
			settingsOverridesMap = settingsOverrides?.ToDictionary((SettingsOverride setting) => setting.settingKey) ?? new Dictionary<string, SettingsOverride>();
		}

		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
		}
	}
}
