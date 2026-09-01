using System;

namespace Kamgam.SettingsGenerator;

public class QualityConnectionSO : OptionConnectionSO
{
	[NonSerialized]
	public SettingsProvider SettingsProvider;

	protected QualityConnection _connection;

	public override IConnectionWithOptions<string> GetConnection()
	{
		if (_connection != null)
		{
			return _connection;
		}
		QualityConnection connection = new QualityConnection();
		_connection = connection;
		return _connection;
	}

	public void Create()
	{
		QualityConnection connection = new QualityConnection();
		_connection = connection;
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
