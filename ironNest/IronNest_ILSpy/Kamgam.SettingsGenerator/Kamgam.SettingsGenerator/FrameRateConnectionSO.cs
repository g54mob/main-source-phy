using System;
using System.Collections.Generic;

namespace Kamgam.SettingsGenerator;

public class FrameRateConnectionSO : OptionConnectionSO
{
	public bool RemoveUnlimited;

	public List<int> CustomFrameRates;

	protected FrameRateConnection _connection;

	public override IConnectionWithOptions<string> GetConnection()
	{
		if (_connection == null)
		{
			FrameRateConnection connection = new FrameRateConnection();
			_connection = connection;
			FrameRateConnection connection2 = _connection;
			if (_connection != null)
			{
				connection2.RemoveUnlimited = RemoveUnlimited;
				FrameRateConnection connection3 = _connection;
				if (_connection != null)
				{
					connection3.CustomFrameRates = CustomFrameRates;
					goto IL_0073;
				}
			}
			return (IConnectionWithOptions<string>)new NullReferenceException();
		}
		goto IL_0073;
		IL_0073:
		return _connection;
	}

	public void Create()
	{
		FrameRateConnection connection = new FrameRateConnection();
		_connection = connection;
		FrameRateConnection connection2 = _connection;
		connection2.RemoveUnlimited = RemoveUnlimited;
		FrameRateConnection connection3 = _connection;
		connection3.CustomFrameRates = CustomFrameRates;
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
