using System.Collections.Generic;
using UnityEngine.Rendering.Universal;

namespace Kamgam.SettingsGenerator;

public class AmbientOcclusionConnectionSO : BoolConnectionSO
{
	protected AmbientOcclusionConnection _connection;

	public override IConnection<bool> GetConnection()
	{
		if (_connection == null)
		{
			AmbientOcclusionConnection ambientOcclusionConnection = new AmbientOcclusionConnection();
			Dictionary<UniversalRenderPipelineAsset, float> lastKnownIntensities = new Dictionary<UniversalRenderPipelineAsset, float>();
			ambientOcclusionConnection._lastKnownIntensities = lastKnownIntensities;
			ambientOcclusionConnection._002Ector();
			_connection = ambientOcclusionConnection;
			return _connection;
		}
		return _connection;
	}

	public void Create()
	{
		AmbientOcclusionConnection ambientOcclusionConnection = new AmbientOcclusionConnection();
		Dictionary<UniversalRenderPipelineAsset, float> lastKnownIntensities = new Dictionary<UniversalRenderPipelineAsset, float>();
		ambientOcclusionConnection._lastKnownIntensities = lastKnownIntensities;
		ambientOcclusionConnection._002Ector();
		_connection = ambientOcclusionConnection;
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
