using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	public class ResolutionConnection : ConnectionWithOptions<string>, IConnectionWithSettingsAccess
	{
		[Serializable]
		public class CustomResolution
		{
			public int Width;

			public int Height;

			public uint RefreshNumerator;

			public uint RefreshDenominator;

			public Resolution ToResolution()
			{
				return default(Resolution);
			}

			public static Resolution[] ToResolutions(List<CustomResolution> customResolutions)
			{
				return null;
			}
		}

		public static bool AllowResolutionChangeOnMobile;

		public bool CacheResolutions;

		public bool LimitToCurrentRefreshRate;

		public bool LimitToUniqueResolutions;

		public bool LimitMaxResolutionToDisplayResolution;

		public bool SkipResolutinsSmallerThanHD;

		public bool SkipRefreshRatesWith59Hz;

		public bool AddRefreshRateToLabels;

		public bool RefreshRateResolversAfterCompletion;

		protected bool _addCustomResolutionOptionIfWindowed;

		public List<Vector2Int> AllowedAspectRatios;

		public float AllowedAspectRatioDelta;

		public List<CustomResolution> CustomResolutions;

		protected Settings _settings;

		protected List<Resolution> _values;

		protected List<string> _labels;

		protected string _resolutionFormat;

		protected string _refreshRateFormat;

		protected Vector2Int _lastMonitorMaxResolution;

		protected Resolution? _windowedResolution;

		protected Resolution? lastKnownResolution;

		protected int lastSetFrame;

		private static List<SettingOption> s_tmpOptionSettingsList;

		public bool AddCustomResolutionOptionIfWindowed
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		protected bool isWindowed => false;

		public event Action OnMaxResolutionChanged
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		private Resolution[] getResolutions()
		{
			return null;
		}

		protected Vector2Int getCurrentMaxResolution()
		{
			return default(Vector2Int);
		}

		protected virtual List<Resolution> getUniqueResolutions()
		{
			return null;
		}

		private void filterResolutionsAndAddToValues(Resolution[] resolutions, bool limitAspectRatios)
		{
		}

		protected Resolution? findResolution(IList<Resolution> resolutions, int width, int height, int refreshRate)
		{
			return null;
		}

		public void ClearResolutionCache()
		{
		}

		public int FindClosestResolutionIndex(int width, int height, int refreshRate)
		{
			return 0;
		}

		protected int findClosestResolutionIndex(IList<Resolution> resolutions, int width, int height, int refreshRate)
		{
			return 0;
		}

		public static int GetRoundedRefreshRate(Resolution res)
		{
			return 0;
		}

		protected bool contains(List<Resolution> resolutions, Resolution resolution)
		{
			return false;
		}

		public override List<string> GetOptionLabels()
		{
			return null;
		}

		public override void RefreshOptionLabels()
		{
		}

		public override void SetOptionLabels(List<string> optionLabels)
		{
		}

		public string GetResolutionFormat()
		{
			return null;
		}

		public void SetResolutionFormat(string format)
		{
		}

		public string GetRefreshRateFormat()
		{
			return null;
		}

		public void SetRefreshRateFormat(string format)
		{
		}

		protected void onScreenSizeChanged(Resolution resolution)
		{
		}

		private void addOrRemoveCustomResolutionValue(Resolution resolution)
		{
		}

		public override int Get()
		{
			return 0;
		}

		public override void Set(int index)
		{
		}

		private void onComplete(Resolution? resolution, bool? fullscreen, FullScreenMode? fullscreenmode)
		{
		}

		public void SetSettings(Settings settings)
		{
		}

		public Settings GetSettings()
		{
			return null;
		}
	}
}
