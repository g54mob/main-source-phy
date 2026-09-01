using System;

namespace Kamgam.SettingsGenerator;

public class RenderScaleConnectionSO : FloatConnectionSO
{
	public bool ReapplyOnQualityChange;

	public float DefaultRenderScale = 1f;

	protected RenderScaleConnection _connection;

	public override IConnection<float> GetConnection()
	{
		if (_connection == null)
		{
			RenderScaleConnection renderScaleConnection = new RenderScaleConnection();
			renderScaleConnection.DefaultRenderScale = 1f;
			renderScaleConnection.scale = -1f;
			_connection = renderScaleConnection;
			RenderScaleConnection connection = _connection;
			if (_connection != null)
			{
				connection.ReapplyOnQualityChange = ReapplyOnQualityChange;
				RenderScaleConnection connection2 = _connection;
				if (_connection != null)
				{
					connection2.DefaultRenderScale = DefaultRenderScale;
					goto IL_0081;
				}
			}
			return (IConnection<float>)new NullReferenceException();
		}
		goto IL_0081;
		IL_0081:
		return _connection;
	}

	public void Create()
	{
		RenderScaleConnection renderScaleConnection = new RenderScaleConnection();
		renderScaleConnection.DefaultRenderScale = 1f;
		renderScaleConnection.scale = -1f;
		_connection = renderScaleConnection;
		RenderScaleConnection connection = _connection;
		connection.ReapplyOnQualityChange = ReapplyOnQualityChange;
		RenderScaleConnection connection2 = _connection;
		connection2.DefaultRenderScale = DefaultRenderScale;
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
