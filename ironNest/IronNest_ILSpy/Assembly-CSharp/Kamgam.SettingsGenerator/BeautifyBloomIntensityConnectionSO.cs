using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class BeautifyBloomIntensityConnectionSO : FloatConnectionSO
{
	public Vector2 InputRange;

	public Vector2 OutputRange;

	public bool ResolveEveryAccess;

	public bool LogWarnings;

	private BeautifyBloomIntensityConnection _connection;

	public override IConnection<float> GetConnection()
	{
		//IL_00e9: Expected O, but got I
		//IL_012c: Expected O, but got I
		//IL_0073: Expected O, but got I
		//IL_0092: Expected O, but got I
		if (_connection == null)
		{
			BeautifyBloomIntensityConnection beautifyBloomIntensityConnection = (BeautifyBloomIntensityConnection)new Connection<float>();
			Vector2 inputRange = InputRange;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.BeautifyBloomIntensityConnectionSO)+1C]");
			bool flag = (nint)inputRange <= 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.BeautifyBloomIntensityConnectionSO)+1C]");
			Vector2 vector = (Vector2)0;
			Vector2 inputRange2 = InputRange;
			if (!flag)
			{
				vector = InputRange;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.BeautifyBloomIntensityConnectionSO)+1C]");
				inputRange2 = (Vector2)0;
			}
			Vector2 outputRange = OutputRange;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.BeautifyBloomIntensityConnectionSO)+24]");
			bool flag2 = (nint)outputRange <= 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.BeautifyBloomIntensityConnectionSO)+24]");
			Vector2 vector2 = (Vector2)0;
			Vector2 outputRange2 = OutputRange;
			if (!flag2)
			{
				vector2 = OutputRange;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.BeautifyBloomIntensityConnectionSO)+24]");
				outputRange2 = (Vector2)0;
			}
			beautifyBloomIntensityConnection._inputRange = inputRange2;
			beautifyBloomIntensityConnection._outputRange = outputRange2;
			bool logWarnings = default(bool);
			BeautifyConnectionResolver beautifyConnectionResolver = new BeautifyConnectionResolver(resolveEveryAccess: false, logWarnings);
			beautifyConnectionResolver._resolveEveryAccess = ResolveEveryAccess;
			beautifyConnectionResolver._logWarnings = LogWarnings;
			beautifyBloomIntensityConnection._resolver = beautifyConnectionResolver;
			_connection = beautifyBloomIntensityConnection;
			return _connection;
		}
		return _connection;
	}

	public void Create()
	{
		//IL_00ba: Expected O, but got I
		//IL_00fd: Expected O, but got I
		//IL_0051: Expected O, but got I
		//IL_0070: Expected O, but got I
		BeautifyBloomIntensityConnection beautifyBloomIntensityConnection = (BeautifyBloomIntensityConnection)new Connection<float>();
		Vector2 inputRange = InputRange;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.BeautifyBloomIntensityConnectionSO)+1C]");
		bool flag = (nint)inputRange <= 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.BeautifyBloomIntensityConnectionSO)+1C]");
		Vector2 vector = (Vector2)0;
		Vector2 inputRange2 = InputRange;
		if (!flag)
		{
			vector = InputRange;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.BeautifyBloomIntensityConnectionSO)+1C]");
			inputRange2 = (Vector2)0;
		}
		Vector2 outputRange = OutputRange;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.BeautifyBloomIntensityConnectionSO)+24]");
		bool flag2 = (nint)outputRange <= 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.BeautifyBloomIntensityConnectionSO)+24]");
		Vector2 vector2 = (Vector2)0;
		Vector2 outputRange2 = OutputRange;
		if (!flag2)
		{
			vector2 = OutputRange;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.BeautifyBloomIntensityConnectionSO)+24]");
			outputRange2 = (Vector2)0;
		}
		beautifyBloomIntensityConnection._inputRange = inputRange2;
		beautifyBloomIntensityConnection._outputRange = outputRange2;
		bool logWarnings = default(bool);
		BeautifyConnectionResolver beautifyConnectionResolver = new BeautifyConnectionResolver(resolveEveryAccess: false, logWarnings);
		beautifyConnectionResolver._resolveEveryAccess = ResolveEveryAccess;
		beautifyConnectionResolver._logWarnings = LogWarnings;
		beautifyBloomIntensityConnection._resolver = beautifyConnectionResolver;
		_connection = beautifyBloomIntensityConnection;
	}

	public override void DestroyConnection()
	{
		BeautifyBloomIntensityConnection connection = _connection;
		if (_connection != null)
		{
			BeautifyConnectionResolver resolver = connection._resolver;
			resolver._cached = null;
		}
		_connection = null;
	}

	public BeautifyBloomIntensityConnectionSO()
	{
		//IL_0011: Expected O, but got I4
		_ = 1120403456;
		InputRange = (Vector2)0;
		_ = 1077936128;
		LogWarnings = true;
		base._002Ector();
	}
}
