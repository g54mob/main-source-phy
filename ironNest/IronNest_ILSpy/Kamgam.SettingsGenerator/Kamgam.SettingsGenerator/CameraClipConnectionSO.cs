namespace Kamgam.SettingsGenerator;

public class CameraClipConnectionSO : FloatConnectionSO
{
	public CameraClipConnection.ClippingMode Mode = CameraClipConnection.ClippingMode.Far;

	public float ClipMin = 1f;

	public float ClipMax = 1000f;

	public bool UseMain = true;

	public bool UseMarkers;

	protected CameraClipConnection _connection;

	public override IConnection<float> GetConnection()
	{
		if (_connection == null)
		{
			bool useMain = default(bool);
			bool useMarkers = default(bool);
			CameraClipConnection connection = new CameraClipConnection(Mode, ClipMin, ClipMax, useMain, useMarkers);
			_connection = connection;
			return _connection;
		}
		return _connection;
	}

	public void Create()
	{
		bool useMain = default(bool);
		bool useMarkers = default(bool);
		CameraClipConnection connection = new CameraClipConnection(Mode, ClipMin, ClipMax, useMain, useMarkers);
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
