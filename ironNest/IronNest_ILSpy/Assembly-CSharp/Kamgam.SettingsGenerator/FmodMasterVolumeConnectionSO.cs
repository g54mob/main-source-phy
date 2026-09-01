using System;
using Cpp2ILInjected;
using FMOD.Studio;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class FmodMasterVolumeConnectionSO : FloatConnectionSO
{
	public Vector2 InputRange;

	public Vector2 OutputLinearRange;

	public string BusPath;

	protected FmodMasterVolumeConnection _connection;

	public override IConnection<float> GetConnection()
	{
		//IL_0110: Expected O, but got I
		//IL_0153: Expected O, but got I
		//IL_0069: Expected O, but got I
		//IL_0088: Expected O, but got I
		if (_connection == null)
		{
			string text = BusPath;
			FmodMasterVolumeConnection fmodMasterVolumeConnection = (FmodMasterVolumeConnection)new Connection<float>();
			Vector2 inputRange = InputRange;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.FmodMasterVolumeConnectionSO)+1C]");
			bool flag = (nint)inputRange <= 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.FmodMasterVolumeConnectionSO)+1C]");
			Vector2 vector = (Vector2)0;
			Vector2 inputRange2 = InputRange;
			if (!flag)
			{
				vector = InputRange;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.FmodMasterVolumeConnectionSO)+1C]");
				inputRange2 = (Vector2)0;
			}
			Vector2 outputLinearRange = OutputLinearRange;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.FmodMasterVolumeConnectionSO)+24]");
			bool flag2 = (nint)outputLinearRange <= 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.FmodMasterVolumeConnectionSO)+24]");
			Vector2 vector2 = (Vector2)0;
			Vector2 outputLinearRange2 = OutputLinearRange;
			if (!flag2)
			{
				vector2 = OutputLinearRange;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.FmodMasterVolumeConnectionSO)+24]");
				outputLinearRange2 = (Vector2)0;
			}
			fmodMasterVolumeConnection._inputRange = inputRange2;
			fmodMasterVolumeConnection._outputLinearRange = outputLinearRange2;
			if (string.IsNullOrWhiteSpace(text))
			{
				text = "bus:/";
			}
			if (fmodMasterVolumeConnection == null)
			{
				return (IConnection<float>)new NullReferenceException();
			}
			fmodMasterVolumeConnection._busPath = text;
			_connection = fmodMasterVolumeConnection;
		}
		return _connection;
	}

	public void Create()
	{
		//IL_00da: Expected O, but got I
		//IL_011d: Expected O, but got I
		//IL_0047: Expected O, but got I
		//IL_0066: Expected O, but got I
		string text = BusPath;
		FmodMasterVolumeConnection fmodMasterVolumeConnection = (FmodMasterVolumeConnection)new Connection<float>();
		Vector2 inputRange = InputRange;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.FmodMasterVolumeConnectionSO)+1C]");
		bool flag = (nint)inputRange <= 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.FmodMasterVolumeConnectionSO)+1C]");
		Vector2 vector = (Vector2)0;
		Vector2 inputRange2 = InputRange;
		if (!flag)
		{
			vector = InputRange;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.FmodMasterVolumeConnectionSO)+1C]");
			inputRange2 = (Vector2)0;
		}
		Vector2 outputLinearRange = OutputLinearRange;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.FmodMasterVolumeConnectionSO)+24]");
		bool flag2 = (nint)outputLinearRange <= 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.FmodMasterVolumeConnectionSO)+24]");
		Vector2 vector2 = (Vector2)0;
		Vector2 outputLinearRange2 = OutputLinearRange;
		if (!flag2)
		{
			vector2 = OutputLinearRange;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.FmodMasterVolumeConnectionSO)+24]");
			outputLinearRange2 = (Vector2)0;
		}
		fmodMasterVolumeConnection._inputRange = inputRange2;
		fmodMasterVolumeConnection._outputLinearRange = outputLinearRange2;
		if (string.IsNullOrWhiteSpace(text))
		{
			text = "bus:/";
		}
		fmodMasterVolumeConnection._busPath = text;
		_connection = fmodMasterVolumeConnection;
	}

	public override void DestroyConnection()
	{
		//IL_0045: Expected O, but got I4
		if (_connection != null)
		{
			FmodMasterVolumeConnection connection = _connection;
			connection._busResolved = false;
			connection._bus = (Bus)0;
		}
		_connection = null;
	}

	public FmodMasterVolumeConnectionSO()
	{
		//IL_0044: Expected O, but got I4
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A696]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		_ = 1120403456;
		InputRange = (Vector2)0;
		_ = 1073741824;
		BusPath = "bus:/";
		base._002Ector();
	}
}
