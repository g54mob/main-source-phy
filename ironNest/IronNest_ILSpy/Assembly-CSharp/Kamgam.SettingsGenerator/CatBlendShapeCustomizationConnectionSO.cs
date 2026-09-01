namespace Kamgam.SettingsGenerator;

public class CatBlendShapeCustomizationConnectionSO : FloatConnectionSO
{
	public bool Eyes;

	public bool Body;

	public bool Fur;

	public bool Whiskers;

	public int BlendShapeIndex;

	protected CatBlendShapeCustomizationConnection _connection;

	public override IConnection<float> GetConnection()
	{
		if (_connection != null)
		{
			return _connection;
		}
		CatBlendShapeCustomizationConnection catBlendShapeCustomizationConnection = (CatBlendShapeCustomizationConnection)new Connection<float>();
		catBlendShapeCustomizationConnection.eyes = Eyes;
		catBlendShapeCustomizationConnection.body = Body;
		catBlendShapeCustomizationConnection.fur = Fur;
		catBlendShapeCustomizationConnection.whiskers = Whiskers;
		catBlendShapeCustomizationConnection.blendShapeIndex = BlendShapeIndex;
		_connection = catBlendShapeCustomizationConnection;
		return _connection;
	}

	public void Create()
	{
		CatBlendShapeCustomizationConnection catBlendShapeCustomizationConnection = (CatBlendShapeCustomizationConnection)new Connection<float>();
		catBlendShapeCustomizationConnection.eyes = Eyes;
		catBlendShapeCustomizationConnection.body = Body;
		catBlendShapeCustomizationConnection.fur = Fur;
		catBlendShapeCustomizationConnection.whiskers = Whiskers;
		catBlendShapeCustomizationConnection.blendShapeIndex = BlendShapeIndex;
		_connection = catBlendShapeCustomizationConnection;
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
