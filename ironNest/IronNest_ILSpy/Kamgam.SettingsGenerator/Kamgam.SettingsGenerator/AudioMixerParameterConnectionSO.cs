using UnityEngine.Audio;

namespace Kamgam.SettingsGenerator;

public class AudioMixerParameterConnectionSO : FloatConnectionSO
{
	public AudioMixer Mixer;

	public string ExposedParameterName;

	protected AudioMixerParameterConnection _connection;

	public override IConnection<float> GetConnection()
	{
		//IL_004a: Expected O, but got I
		if (_connection == null)
		{
			string exposedParameterName = default(string);
			AudioMixerParameterConnection audioMixerParameterConnection = new AudioMixerParameterConnection((AudioMixer)0, exposedParameterName);
			audioMixerParameterConnection.Mixer = Mixer;
			audioMixerParameterConnection.ExposedParameterName = ExposedParameterName;
			_connection = audioMixerParameterConnection;
			return _connection;
		}
		return _connection;
	}

	public void Create()
	{
		//IL_001b: Expected O, but got I
		string exposedParameterName = default(string);
		AudioMixerParameterConnection audioMixerParameterConnection = new AudioMixerParameterConnection((AudioMixer)0, exposedParameterName);
		audioMixerParameterConnection.Mixer = Mixer;
		audioMixerParameterConnection.ExposedParameterName = ExposedParameterName;
		_connection = audioMixerParameterConnection;
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
