using System.Collections.Generic;
using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "ResolutionConnection", menuName = "SettingsGenerator/Connection/ResolutionConnection", order = 4)]
	public class ResolutionConnectionSO : OptionConnectionSO
	{
		[Tooltip("Disable if the resolutions change very often.")]
		public bool CacheResolutions;

		[Tooltip("If enabled then only those resolution options which match the current resolution refresh rate are listed. That list may be much shorter than the full list.")]
		public bool LimitToCurrentRefreshRate;

		[Tooltip("If enabled then only one resolution per frequency will be listed. For example there may be two resolutions: 640x480 @60Hz and 640x480 @75Hz\nIf enabled then only one of these will be in the list. It will choose the one which has the closest frequency to the currently used frequency.")]
		public bool LimitToUniqueResolutions;

		[Tooltip("If enabled then any resolution that is bigger than the width or height of the biggest screen (hradware resolution) will be skipped.\n\nNOTICE: This does nothing in the EDITOR since the API does not return the correct size there. Please test it in a real build.")]
		public bool LimitMaxResolutionToDisplayResolution;

		[Tooltip("If enabled then any resolution that is smaller than 1280x720 will be skipped.")]
		public bool SkipResolutinsSmallerThanHD;

		[Tooltip("If enabled then then resolutions with a refresh rate of 59 Hz will be skipped if (and only if) there is an alternative with 60 Hz.")]
		public bool SkipRefreshRatesWith59Hz;

		[Tooltip("Should the refresh rate (frequency) be added to the labels.\nExample without: 1024x768\nExample with: 1024x768 (60Hz)")]
		public bool AddRefreshRateToLabels;

		[Tooltip("A list of aspect ratios (x = width, y = height) to use as a positive filter criteria for the list of resolutions.\nIf the list is empty then no filtering will occur and all resolutions will be listed.")]
		public List<Vector2Int> AllowedAspectRatios;

		[Tooltip("Threshold of how much a resolution can differ from the defined AllowedAspectRatios.\nLike if the allowed aspect is 16:9 (w:h), i.e.: 1.77 and this is 0.02f then all ratios between 1.75 and 1.79 are valid.")]
		public float AllowedAspectRatioDelta;

		[Tooltip("If not empty then this will be used as the base list of resolutions instead of whatever Unity detects.\nNOTICE: Since Unity 2022.2 the refresh reate is defined by a numerator and a denominator (60000 and 1001 for 60 Hz for example).")]
		public List<ResolutionConnection.CustomResolution> CustomResolutions;

		[Tooltip("Resolution format {0} = width in pixels. {1} = height in pixels.")]
		public string ResolutionFormat;

		[Tooltip("Will be appended to the normal resolution string if AddRefreshRateToLabels is enabled. {0} is the refresh rate as an integer.")]
		public string RefreshRateFormat;

		[Tooltip("If enabled then a custom resolution option will be added as the very first option.")]
		public bool AddCustomResolutionOptionIfWindowed;

		protected ResolutionConnection _connection;

		public override IConnectionWithOptions<string> GetConnection()
		{
			return null;
		}

		public void Create()
		{
		}

		public override void DestroyConnection()
		{
		}
	}
}
