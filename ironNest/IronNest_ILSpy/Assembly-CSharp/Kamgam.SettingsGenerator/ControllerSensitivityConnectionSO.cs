using System;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class ControllerSensitivityConnectionSO : FloatConnectionSO
{
	public Vector2 InputRange;

	public string TargetTag;

	public bool ResolveEverySet;

	public bool LogWarnings;

	private ControllerSensitivityConnection _connection;

	public override IConnection<float> GetConnection()
	{
		//IL_010a: Expected O, but got I
		//IL_0064: Expected O, but got I
		if (_connection == null)
		{
			string text = TargetTag;
			ControllerSensitivityConnection controllerSensitivityConnection = (ControllerSensitivityConnection)new Connection<float>();
			Vector2 inputRange = InputRange;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.ControllerSensitivityConnectionSO)+1C]");
			bool flag = (nint)inputRange <= 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.ControllerSensitivityConnectionSO)+1C]");
			Vector2 vector = (Vector2)0;
			Vector2 inputRange2 = InputRange;
			if (!flag)
			{
				vector = InputRange;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.ControllerSensitivityConnectionSO)+1C]");
				inputRange2 = (Vector2)0;
			}
			controllerSensitivityConnection._inputRange = inputRange2;
			if (string.IsNullOrWhiteSpace(text))
			{
				text = "ControllerSensitivity";
			}
			if (controllerSensitivityConnection == null)
			{
				return (IConnection<float>)new NullReferenceException();
			}
			controllerSensitivityConnection._targetTag = text;
			controllerSensitivityConnection._resolveEverySet = ResolveEverySet;
			controllerSensitivityConnection._logWarnings = LogWarnings;
			_connection = controllerSensitivityConnection;
		}
		return _connection;
	}

	public void Create()
	{
		//IL_00d4: Expected O, but got I
		//IL_0042: Expected O, but got I
		string text = TargetTag;
		ControllerSensitivityConnection controllerSensitivityConnection = (ControllerSensitivityConnection)new Connection<float>();
		Vector2 inputRange = InputRange;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.ControllerSensitivityConnectionSO)+1C]");
		bool flag = (nint)inputRange <= 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.ControllerSensitivityConnectionSO)+1C]");
		Vector2 vector = (Vector2)0;
		Vector2 inputRange2 = InputRange;
		if (!flag)
		{
			vector = InputRange;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.ControllerSensitivityConnectionSO)+1C]");
			inputRange2 = (Vector2)0;
		}
		controllerSensitivityConnection._inputRange = inputRange2;
		if (string.IsNullOrWhiteSpace(text))
		{
			text = "ControllerSensitivity";
		}
		controllerSensitivityConnection._targetTag = text;
		controllerSensitivityConnection._resolveEverySet = ResolveEverySet;
		controllerSensitivityConnection._logWarnings = LogWarnings;
		_connection = controllerSensitivityConnection;
	}

	public override void DestroyConnection()
	{
		ControllerSensitivityConnection connection = _connection;
		if (_connection != null)
		{
			connection._cachedSetter = null;
		}
		_connection = null;
	}

	public ControllerSensitivityConnectionSO()
	{
		//IL_003e: Expected O, but got I4
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A690]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		InputRange = (Vector2)1065353216;
		_ = 1082130432;
		TargetTag = "ControllerSensitivity";
		ResolveEverySet = true;
		base._002Ector();
	}
}
