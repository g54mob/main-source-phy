using System;

namespace Kamgam.SettingsGenerator;

public class RefreshRateConnectionSO : OptionConnectionSO
{
	public bool CacheRefreshRates = true;

	public int MinRate;

	public int MaxRate = 1000;

	public bool LimitToCurrentResolution;

	protected RefreshRateConnection _connection;

	public override IConnectionWithOptions<string> GetConnection()
	{
		if (_connection == null)
		{
			RefreshRateConnection connection = new RefreshRateConnection();
			_connection = connection;
			RefreshRateConnection connection2 = _connection;
			if (_connection != null)
			{
				connection2.CacheRefreshRates = CacheRefreshRates;
				RefreshRateConnection connection3 = _connection;
				if (_connection != null)
				{
					connection3.MinRate = MinRate;
					RefreshRateConnection connection4 = _connection;
					if (_connection != null)
					{
						connection4.MaxRate = MaxRate;
						RefreshRateConnection connection5 = _connection;
						if (_connection != null)
						{
							connection5.LimitToCurrentResolution = LimitToCurrentResolution;
							goto IL_011a;
						}
					}
				}
			}
			return (IConnectionWithOptions<string>)new NullReferenceException();
		}
		goto IL_011a;
		IL_011a:
		return _connection;
	}

	public void Create()
	{
		RefreshRateConnection connection = new RefreshRateConnection();
		_connection = connection;
		RefreshRateConnection connection2 = _connection;
		connection2.CacheRefreshRates = CacheRefreshRates;
		RefreshRateConnection connection3 = _connection;
		connection3.MinRate = MinRate;
		RefreshRateConnection connection4 = _connection;
		connection4.MaxRate = MaxRate;
		RefreshRateConnection connection5 = _connection;
		connection5.LimitToCurrentResolution = LimitToCurrentResolution;
	}

	public override void DestroyConnection()
	{
		if (_connection != null)
		{
			_connection.Destroy();
		}
		_connection = null;
	}
}
