using System.Runtime.CompilerServices;

namespace Kamgam.SettingsGenerator;

public class MicrophoneConnectionSO : OptionConnectionSO
{
	public float PollIntervalInSec = -1f;

	protected MicrophoneConnection _connection;

	public override IConnectionWithOptions<string> GetConnection()
	{
		if (_connection == null)
		{
			MicrophoneConnection microphoneConnection = (MicrophoneConnection)new ConnectionWithOptions<string>();
			microphoneConnection._pollIntervalInSec = -1f;
			((ConnectionWithOptions<string>)microphoneConnection)._002Ector();
			ConnectionWithOptions<string> connectionWithOptions = default(ConnectionWithOptions<string>);
			if (connectionWithOptions == null)
			{
				microphoneConnection._pollIntervalInSec = PollIntervalInSec;
				AsyncVoidMethodBuilder asyncVoidMethodBuilder = AsyncVoidMethodBuilder.Create();
				AsyncVoidMethodBuilder asyncVoidMethodBuilder2 = default(AsyncVoidMethodBuilder);
				MicrophoneConnection._003CstartStopPolling_003Ed__4 stateMachine = default(MicrophoneConnection._003CstartStopPolling_003Ed__4);
				asyncVoidMethodBuilder2.Start(ref stateMachine);
			}
			_connection = microphoneConnection;
			return _connection;
		}
		return _connection;
	}

	public void Create()
	{
		MicrophoneConnection microphoneConnection = (MicrophoneConnection)new ConnectionWithOptions<string>();
		microphoneConnection._pollIntervalInSec = -1f;
		((ConnectionWithOptions<string>)microphoneConnection)._002Ector();
		ConnectionWithOptions<string> connectionWithOptions = default(ConnectionWithOptions<string>);
		if (connectionWithOptions == null)
		{
			microphoneConnection._pollIntervalInSec = PollIntervalInSec;
			AsyncVoidMethodBuilder asyncVoidMethodBuilder = AsyncVoidMethodBuilder.Create();
			AsyncVoidMethodBuilder asyncVoidMethodBuilder2 = default(AsyncVoidMethodBuilder);
			MicrophoneConnection._003CstartStopPolling_003Ed__4 stateMachine = default(MicrophoneConnection._003CstartStopPolling_003Ed__4);
			asyncVoidMethodBuilder2.Start(ref stateMachine);
		}
		_connection = microphoneConnection;
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
