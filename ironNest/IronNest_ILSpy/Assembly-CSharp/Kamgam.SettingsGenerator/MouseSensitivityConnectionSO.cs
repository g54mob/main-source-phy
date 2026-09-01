using System;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class MouseSensitivityConnectionSO : FloatConnectionSO
{
	public Vector2 InputRange;

	public string TargetTag;

	public bool ResolveEverySet;

	public bool LogWarnings;

	private MouseSensitivityConnection _connection;

	public override IConnection<float> GetConnection()
	{
		//IL_010a: Expected O, but got I
		//IL_0064: Expected O, but got I
		if (_connection == null)
		{
			string text = TargetTag;
			MouseSensitivityConnection mouseSensitivityConnection = (MouseSensitivityConnection)new Connection<float>();
			Vector2 inputRange = InputRange;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.MouseSensitivityConnectionSO)+1C]");
			bool flag = (nint)inputRange <= 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.MouseSensitivityConnectionSO)+1C]");
			Vector2 vector = (Vector2)0;
			Vector2 inputRange2 = InputRange;
			if (!flag)
			{
				vector = InputRange;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.MouseSensitivityConnectionSO)+1C]");
				inputRange2 = (Vector2)0;
			}
			mouseSensitivityConnection._inputRange = inputRange2;
			if (string.IsNullOrWhiteSpace(text))
			{
				text = "MouseSensitivity";
			}
			if (mouseSensitivityConnection == null)
			{
				return (IConnection<float>)new NullReferenceException();
			}
			mouseSensitivityConnection._targetTag = text;
			mouseSensitivityConnection._resolveEverySet = ResolveEverySet;
			mouseSensitivityConnection._logWarnings = LogWarnings;
			_connection = mouseSensitivityConnection;
		}
		return _connection;
	}

	public void Create()
	{
		//IL_00d4: Expected O, but got I
		//IL_0042: Expected O, but got I
		string text = TargetTag;
		MouseSensitivityConnection mouseSensitivityConnection = (MouseSensitivityConnection)new Connection<float>();
		Vector2 inputRange = InputRange;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.MouseSensitivityConnectionSO)+1C]");
		bool flag = (nint)inputRange <= 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.MouseSensitivityConnectionSO)+1C]");
		Vector2 vector = (Vector2)0;
		Vector2 inputRange2 = InputRange;
		if (!flag)
		{
			vector = InputRange;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.MouseSensitivityConnectionSO)+1C]");
			inputRange2 = (Vector2)0;
		}
		mouseSensitivityConnection._inputRange = inputRange2;
		if (string.IsNullOrWhiteSpace(text))
		{
			text = "MouseSensitivity";
		}
		mouseSensitivityConnection._targetTag = text;
		mouseSensitivityConnection._resolveEverySet = ResolveEverySet;
		mouseSensitivityConnection._logWarnings = LogWarnings;
		_connection = mouseSensitivityConnection;
	}

	public override void DestroyConnection()
	{
		MouseSensitivityConnection connection = _connection;
		if (_connection != null)
		{
			connection._cachedSetter = null;
		}
		_connection = null;
	}

	public MouseSensitivityConnectionSO()
	{
		//IL_003e: Expected O, but got I4
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A6B2]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		InputRange = (Vector2)1036831949;
		_ = 1073741824;
		TargetTag = "MouseSensitivity";
		ResolveEverySet = true;
		base._002Ector();
	}
}
