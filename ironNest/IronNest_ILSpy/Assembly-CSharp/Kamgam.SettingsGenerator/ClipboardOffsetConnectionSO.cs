using System;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class ClipboardOffsetConnectionSO : FloatConnectionSO
{
	public Vector2 InputRange;

	public Vector2 OutputOffsetUnitsRange;

	public string TargetTag;

	public bool ResolveEverySet;

	public bool LogWarnings;

	private ClipboardOffsetConnection _connection;

	public override IConnection<float> GetConnection()
	{
		//IL_0138: Expected O, but got I
		//IL_017b: Expected O, but got I
		//IL_0073: Expected O, but got I
		//IL_0092: Expected O, but got I
		if (_connection == null)
		{
			string text = TargetTag;
			ClipboardOffsetConnection clipboardOffsetConnection = (ClipboardOffsetConnection)new Connection<float>();
			Vector2 inputRange = InputRange;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.ClipboardOffsetConnectionSO)+1C]");
			bool flag = (nint)inputRange <= 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.ClipboardOffsetConnectionSO)+1C]");
			Vector2 vector = (Vector2)0;
			Vector2 inputRange2 = InputRange;
			if (!flag)
			{
				vector = InputRange;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.ClipboardOffsetConnectionSO)+1C]");
				inputRange2 = (Vector2)0;
			}
			Vector2 outputOffsetUnitsRange = OutputOffsetUnitsRange;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.ClipboardOffsetConnectionSO)+24]");
			bool flag2 = (nint)outputOffsetUnitsRange <= 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.ClipboardOffsetConnectionSO)+24]");
			Vector2 vector2 = (Vector2)0;
			Vector2 outputUnitsRange = OutputOffsetUnitsRange;
			if (!flag2)
			{
				vector2 = OutputOffsetUnitsRange;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.ClipboardOffsetConnectionSO)+24]");
				outputUnitsRange = (Vector2)0;
			}
			clipboardOffsetConnection._inputRange = inputRange2;
			clipboardOffsetConnection._outputUnitsRange = outputUnitsRange;
			if (string.IsNullOrWhiteSpace(text))
			{
				text = "Clipboard";
			}
			if (clipboardOffsetConnection == null)
			{
				return (IConnection<float>)new NullReferenceException();
			}
			clipboardOffsetConnection._targetTag = text;
			clipboardOffsetConnection._resolveEverySet = ResolveEverySet;
			clipboardOffsetConnection._logWarnings = LogWarnings;
			_connection = clipboardOffsetConnection;
		}
		return _connection;
	}

	public void Create()
	{
		//IL_0102: Expected O, but got I
		//IL_0145: Expected O, but got I
		//IL_0051: Expected O, but got I
		//IL_0070: Expected O, but got I
		string text = TargetTag;
		ClipboardOffsetConnection clipboardOffsetConnection = (ClipboardOffsetConnection)new Connection<float>();
		Vector2 inputRange = InputRange;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.ClipboardOffsetConnectionSO)+1C]");
		bool flag = (nint)inputRange <= 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.ClipboardOffsetConnectionSO)+1C]");
		Vector2 vector = (Vector2)0;
		Vector2 inputRange2 = InputRange;
		if (!flag)
		{
			vector = InputRange;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.ClipboardOffsetConnectionSO)+1C]");
			inputRange2 = (Vector2)0;
		}
		Vector2 outputOffsetUnitsRange = OutputOffsetUnitsRange;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.ClipboardOffsetConnectionSO)+24]");
		bool flag2 = (nint)outputOffsetUnitsRange <= 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.ClipboardOffsetConnectionSO)+24]");
		Vector2 vector2 = (Vector2)0;
		Vector2 outputUnitsRange = OutputOffsetUnitsRange;
		if (!flag2)
		{
			vector2 = OutputOffsetUnitsRange;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.ClipboardOffsetConnectionSO)+24]");
			outputUnitsRange = (Vector2)0;
		}
		clipboardOffsetConnection._inputRange = inputRange2;
		clipboardOffsetConnection._outputUnitsRange = outputUnitsRange;
		if (string.IsNullOrWhiteSpace(text))
		{
			text = "Clipboard";
		}
		clipboardOffsetConnection._targetTag = text;
		clipboardOffsetConnection._resolveEverySet = ResolveEverySet;
		clipboardOffsetConnection._logWarnings = LogWarnings;
		_connection = clipboardOffsetConnection;
	}

	public override void DestroyConnection()
	{
		ClipboardOffsetConnection connection = _connection;
		if (_connection != null)
		{
			connection._cachedFader = null;
		}
		_connection = null;
	}

	public ClipboardOffsetConnectionSO()
	{
		//IL_003e: Expected O, but got I4
		//IL_0053: Expected O, but got I8
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A68A]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		InputRange = (Vector2)0;
		_ = 1120403456;
		OutputOffsetUnitsRange = (Vector2)3192704205L;
		_ = 1045220557;
		TargetTag = "Clipboard";
		ResolveEverySet = true;
		base._002Ector();
	}
}
