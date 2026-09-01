using System.Collections.Generic;

namespace Kamgam.SettingsGenerator;

public class ShadowDistanceConnectionSO : OptionConnectionSO
{
	public List<float> QualityDistances;

	public bool UseQualitySettingsAsFallback = true;

	protected ShadowDistanceConnection _connection;

	public override IConnectionWithOptions<string> GetConnection()
	{
		if (_connection == null)
		{
			ShadowDistanceConnection shadowDistanceConnection = (ShadowDistanceConnection)new ConnectionWithOptions<string>();
			shadowDistanceConnection.QualityDistances = QualityDistances;
			shadowDistanceConnection.UseQualitySettingsAsFallback = UseQualitySettingsAsFallback;
			_connection = shadowDistanceConnection;
			return _connection;
		}
		return _connection;
	}

	public void Create()
	{
		ShadowDistanceConnection shadowDistanceConnection = (ShadowDistanceConnection)new ConnectionWithOptions<string>();
		shadowDistanceConnection.QualityDistances = QualityDistances;
		shadowDistanceConnection.UseQualitySettingsAsFallback = UseQualitySettingsAsFallback;
		_connection = shadowDistanceConnection;
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
