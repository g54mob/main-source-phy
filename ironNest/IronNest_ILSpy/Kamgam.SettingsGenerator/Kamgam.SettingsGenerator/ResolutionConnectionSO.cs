using System.Collections.Generic;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class ResolutionConnectionSO : OptionConnectionSO
{
	public bool CacheResolutions = true;

	public bool LimitToCurrentRefreshRate;

	public bool LimitToUniqueResolutions = true;

	public bool LimitMaxResolutionToDisplayResolution;

	public bool SkipResolutinsSmallerThanHD = true;

	public bool SkipRefreshRatesWith59Hz;

	public bool AddRefreshRateToLabels;

	public List<Vector2Int> AllowedAspectRatios;

	public float AllowedAspectRatioDelta;

	public List<ResolutionConnection.CustomResolution> CustomResolutions;

	public string ResolutionFormat;

	public string RefreshRateFormat;

	public bool AddCustomResolutionOptionIfWindowed;

	protected ResolutionConnection _connection;

	public override IConnectionWithOptions<string> GetConnection()
	{
		if (_connection == null)
		{
			Create();
			return _connection;
		}
		return _connection;
	}

	public void Create()
	{
		ResolutionConnection resolutionConnection = new ResolutionConnection();
		resolutionConnection.CacheResolutions = true;
		resolutionConnection.LimitToUniqueResolutions = true;
		resolutionConnection.SkipResolutinsSmallerThanHD = true;
		resolutionConnection.RefreshRateResolversAfterCompletion = true;
		List<Vector2Int> allowedAspectRatios = new List<Vector2Int>();
		resolutionConnection.AllowedAspectRatios = allowedAspectRatios;
		resolutionConnection.AllowedAspectRatioDelta = 0.02f;
		List<ResolutionConnection.CustomResolution> customResolutions = new List<ResolutionConnection.CustomResolution>();
		resolutionConnection.CustomResolutions = customResolutions;
		resolutionConnection._resolutionFormat = "{0}x{1}";
		resolutionConnection._refreshRateFormat = " ({0}Hz)";
		resolutionConnection._002Ector();
		_connection = resolutionConnection;
		ResolutionConnection connection = _connection;
		connection.CustomResolutions = CustomResolutions;
		ResolutionConnection connection2 = _connection;
		connection2.CacheResolutions = CacheResolutions;
		ResolutionConnection connection3 = _connection;
		connection3.LimitToCurrentRefreshRate = LimitToCurrentRefreshRate;
		ResolutionConnection connection4 = _connection;
		connection4.LimitToUniqueResolutions = LimitToUniqueResolutions;
		ResolutionConnection connection5 = _connection;
		connection5.LimitMaxResolutionToDisplayResolution = LimitMaxResolutionToDisplayResolution;
		ResolutionConnection connection6 = _connection;
		connection6.SkipRefreshRatesWith59Hz = SkipRefreshRatesWith59Hz;
		ResolutionConnection connection7 = _connection;
		connection7.AddRefreshRateToLabels = AddRefreshRateToLabels;
		ResolutionConnection connection8 = _connection;
		connection8.AllowedAspectRatios = AllowedAspectRatios;
		ResolutionConnection connection9 = _connection;
		connection9.AllowedAspectRatioDelta = AllowedAspectRatioDelta;
		ResolutionConnection connection10 = _connection;
		connection10.SkipResolutinsSmallerThanHD = SkipResolutinsSmallerThanHD;
		ResolutionConnection connection11 = _connection;
		connection11._refreshRateFormat = RefreshRateFormat;
		connection11.RefreshOptionLabels();
		ResolutionConnection connection12 = _connection;
		connection12._resolutionFormat = ResolutionFormat;
		connection12.RefreshOptionLabels();
		_connection.AddCustomResolutionOptionIfWindowed = AddCustomResolutionOptionIfWindowed;
	}

	public override void DestroyConnection()
	{
		if (_connection != null)
		{
			_connection.Destroy();
		}
		_connection = null;
	}

	public ResolutionConnectionSO()
	{
		List<Vector2Int> allowedAspectRatios = new List<Vector2Int>();
		AllowedAspectRatios = allowedAspectRatios;
		AllowedAspectRatioDelta = 0.02f;
		CustomResolutions = new List<ResolutionConnection.CustomResolution>();
		ResolutionFormat = "{0}x{1}";
		RefreshRateFormat = " ({0}Hz)";
		((ConnectionSO)this)._002Ector();
	}
}
