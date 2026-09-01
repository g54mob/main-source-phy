using System;

namespace Kamgam.SettingsGenerator;

public class MonitorConnectionSO : OptionConnectionSO
{
	public bool RefreshResolversAfterCompletion = true;

	public bool TryToPreserveResolutionOnMonitorChange;

	protected MonitorConnection _connection;

	public override IConnectionWithOptions<string> GetConnection()
	{
		if (_connection == null)
		{
			MonitorConnection monitorConnection = new MonitorConnection();
			monitorConnection.RefreshResolversAfterCompletion = true;
			monitorConnection._lastKnownMonitorIndex = -1;
			_connection = monitorConnection;
			MonitorConnection connection = _connection;
			if (_connection != null)
			{
				connection.RefreshResolversAfterCompletion = RefreshResolversAfterCompletion;
				MonitorConnection connection2 = _connection;
				if (_connection != null)
				{
					connection2.TryToPreserveResolutionOnMonitorChange = TryToPreserveResolutionOnMonitorChange;
					goto IL_0081;
				}
			}
			return (IConnectionWithOptions<string>)new NullReferenceException();
		}
		goto IL_0081;
		IL_0081:
		return _connection;
	}

	public void Create()
	{
		MonitorConnection monitorConnection = new MonitorConnection();
		monitorConnection.RefreshResolversAfterCompletion = true;
		monitorConnection._lastKnownMonitorIndex = -1;
		_connection = monitorConnection;
		MonitorConnection connection = _connection;
		connection.RefreshResolversAfterCompletion = RefreshResolversAfterCompletion;
		MonitorConnection connection2 = _connection;
		connection2.TryToPreserveResolutionOnMonitorChange = TryToPreserveResolutionOnMonitorChange;
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
