using System;
using System.Collections.Generic;

namespace Kamgam.SettingsGenerator;

public class AlteregoDLSSConnectionSO : OptionConnectionSO
{
	protected AlteregoDLSSConnection _connection;

	public bool CheckForCameraMarker = true;

	public override IConnectionWithOptions<string> GetConnection()
	{
		if (_connection == null)
		{
			AlteregoDLSSConnection alteregoDLSSConnection = new AlteregoDLSSConnection();
			alteregoDLSSConnection.CheckForCameraMarker = true;
			List<int> enumOptionsAsIntegers = new List<int>(6);
			alteregoDLSSConnection._enumOptionsAsIntegers = enumOptionsAsIntegers;
			alteregoDLSSConnection._002Ector();
			Logger.LogWarning("AlteregoDLSSConnection: Alterego DLSS is not yet set up. Please add DLSS as a post processing effect and install the NVIDIA package. Please contact Alterego Games for more info and support.");
			_connection = alteregoDLSSConnection;
			AlteregoDLSSConnection connection = _connection;
			if (_connection == null)
			{
				return (IConnectionWithOptions<string>)new NullReferenceException();
			}
			connection.CheckForCameraMarker = CheckForCameraMarker;
		}
		return _connection;
	}

	public void Create()
	{
		AlteregoDLSSConnection alteregoDLSSConnection = new AlteregoDLSSConnection();
		alteregoDLSSConnection.CheckForCameraMarker = true;
		List<int> enumOptionsAsIntegers = new List<int>(6);
		alteregoDLSSConnection._enumOptionsAsIntegers = enumOptionsAsIntegers;
		alteregoDLSSConnection._002Ector();
		Logger.LogWarning("AlteregoDLSSConnection: Alterego DLSS is not yet set up. Please add DLSS as a post processing effect and install the NVIDIA package. Please contact Alterego Games for more info and support.");
		_connection = alteregoDLSSConnection;
		AlteregoDLSSConnection connection = _connection;
		connection.CheckForCameraMarker = CheckForCameraMarker;
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
