namespace Kamgam.SettingsGenerator;

public class ColorGradeConnectionSO : FloatConnectionSO
{
	public ColorGradeConnection.ColorGradeEffect Effect = ColorGradeConnection.ColorGradeEffect.Gamma;

	protected ColorGradeConnection _connection;

	public override IConnection<float> GetConnection()
	{
		if (_connection == null)
		{
			ColorGradeConnection connection = new ColorGradeConnection(Effect);
			_connection = connection;
			return _connection;
		}
		return _connection;
	}

	public void Create()
	{
		ColorGradeConnection connection = new ColorGradeConnection(Effect);
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
