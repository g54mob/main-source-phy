using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class BeautifyChromaticAberrationConnectionSO : FloatConnectionSO
{
	public Vector2 InputRange;

	public Vector2 OutputRange;

	public bool ResolveEveryAccess;

	public bool LogWarnings;

	private BeautifyChromaticAberrationConnection _connection;

	public override IConnection<float> GetConnection()
	{
		//IL_00e9: Expected O, but got I
		//IL_012c: Expected O, but got I
		//IL_0073: Expected O, but got I
		//IL_0092: Expected O, but got I
		if (_connection == null)
		{
			BeautifyChromaticAberrationConnection beautifyChromaticAberrationConnection = (BeautifyChromaticAberrationConnection)new Connection<float>();
			Vector2 inputRange = InputRange;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.BeautifyChromaticAberrationConnectionSO)+1C]");
			bool flag = (nint)inputRange <= 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.BeautifyChromaticAberrationConnectionSO)+1C]");
			Vector2 vector = (Vector2)0;
			Vector2 inputRange2 = InputRange;
			if (!flag)
			{
				vector = InputRange;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.BeautifyChromaticAberrationConnectionSO)+1C]");
				inputRange2 = (Vector2)0;
			}
			Vector2 outputRange = OutputRange;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.BeautifyChromaticAberrationConnectionSO)+24]");
			bool flag2 = (nint)outputRange <= 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.BeautifyChromaticAberrationConnectionSO)+24]");
			Vector2 vector2 = (Vector2)0;
			Vector2 outputRange2 = OutputRange;
			if (!flag2)
			{
				vector2 = OutputRange;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.BeautifyChromaticAberrationConnectionSO)+24]");
				outputRange2 = (Vector2)0;
			}
			beautifyChromaticAberrationConnection._inputRange = inputRange2;
			beautifyChromaticAberrationConnection._outputRange = outputRange2;
			bool logWarnings = default(bool);
			BeautifyConnectionResolver beautifyConnectionResolver = new BeautifyConnectionResolver(resolveEveryAccess: false, logWarnings);
			beautifyConnectionResolver._resolveEveryAccess = ResolveEveryAccess;
			beautifyConnectionResolver._logWarnings = LogWarnings;
			beautifyChromaticAberrationConnection._resolver = beautifyConnectionResolver;
			_connection = beautifyChromaticAberrationConnection;
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
		BeautifyChromaticAberrationConnection beautifyChromaticAberrationConnection = (BeautifyChromaticAberrationConnection)new Connection<float>();
		Vector2 inputRange = InputRange;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.BeautifyChromaticAberrationConnectionSO)+1C]");
		bool flag = (nint)inputRange <= 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.BeautifyChromaticAberrationConnectionSO)+1C]");
		Vector2 vector = (Vector2)0;
		Vector2 inputRange2 = InputRange;
		if (!flag)
		{
			vector = InputRange;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.BeautifyChromaticAberrationConnectionSO)+1C]");
			inputRange2 = (Vector2)0;
		}
		Vector2 outputRange = OutputRange;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.BeautifyChromaticAberrationConnectionSO)+24]");
		bool flag2 = (nint)outputRange <= 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.BeautifyChromaticAberrationConnectionSO)+24]");
		Vector2 vector2 = (Vector2)0;
		Vector2 outputRange2 = OutputRange;
		if (!flag2)
		{
			vector2 = OutputRange;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.BeautifyChromaticAberrationConnectionSO)+24]");
			outputRange2 = (Vector2)0;
		}
		beautifyChromaticAberrationConnection._inputRange = inputRange2;
		beautifyChromaticAberrationConnection._outputRange = outputRange2;
		bool logWarnings = default(bool);
		BeautifyConnectionResolver beautifyConnectionResolver = new BeautifyConnectionResolver(resolveEveryAccess: false, logWarnings);
		beautifyConnectionResolver._resolveEveryAccess = ResolveEveryAccess;
		beautifyConnectionResolver._logWarnings = LogWarnings;
		beautifyChromaticAberrationConnection._resolver = beautifyConnectionResolver;
		_connection = beautifyChromaticAberrationConnection;
	}

	public override void DestroyConnection()
	{
		BeautifyChromaticAberrationConnection connection = _connection;
		if (_connection != null)
		{
			BeautifyConnectionResolver resolver = connection._resolver;
			resolver._cached = null;
		}
		_connection = null;
	}

	public BeautifyChromaticAberrationConnectionSO()
	{
		//IL_0011: Expected O, but got I4
		_ = 1120403456;
		InputRange = (Vector2)0;
		_ = 1036831949;
		LogWarnings = true;
		base._002Ector();
	}
}
