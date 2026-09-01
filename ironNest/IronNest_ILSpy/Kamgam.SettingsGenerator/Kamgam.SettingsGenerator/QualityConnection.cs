using System.Collections.Generic;
using System.Linq;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class QualityConnection : ConnectionWithOptions<string>, IConnectionWithSettingsAccess
{
	public Settings Settings;

	protected List<string> _labels;

	protected List<int> _values;

	public QualityConnection(Settings settings)
	{
		Settings = settings;
	}

	public QualityConnection()
	{
	}

	public override int GetOrder()
	{
		//IL_0021: Expected O, but got I
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Expected I4, but got Unknown
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v31 @ rdx_v2 (Il2CppClass<Kamgam.SettingsGenerator.Connection`1<System.Int32>>)+80]");
		object obj = (nint)0 + (nint)96;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj2 = default(object);
		return obj2 - 1;
	}

	public override int Get()
	{
		return QualitySettings.GetQualityLevel();
	}

	public override List<string> GetOptionLabels()
	{
		if (_labels == null)
		{
			string[] names = QualitySettings.names;
			List<string> labels = Enumerable.ToList(names);
			_labels = labels;
		}
		return _labels;
	}

	public override void SetOptionLabels(List<string> optionLabels)
	{
		if (optionLabels != null)
		{
			string[] names = QualitySettings.names;
			if (optionLabels._size == names.Length)
			{
				List<string> labels = new List<string>(optionLabels);
				_labels = labels;
				return;
			}
		}
		string[] names2 = QualitySettings.names;
		int num = default(int);
		string text = num.ToString();
		string message = "Invalid new labels for QualityConnection. Need to be " + text + ".";
		Debug.LogError(message);
	}

	public override void RefreshOptionLabels()
	{
		//IL_000c: Expected I, but got O
		//IL_001c: Expected O, but got I
		//IL_002c: Expected O, but got I
		_labels = null;
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx_v2 (Il2CppClass<Kamgam.SettingsGenerator.QualityConnection>)+2C8]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx_v2 (Il2CppClass<Kamgam.SettingsGenerator.QualityConnection>)+2D0]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v10 @ rax_v2 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	public override void Set(int value)
	{
		//IL_006a: Expected I, but got O
		//IL_007a: Expected O, but got I
		//IL_008a: Expected O, but got I
		while (true)
		{
			int qualityLevel = QualitySettings.GetQualityLevel();
			QualityPresets.RestoreCurrentLevel();
			QualitySettings.SetQualityLevel(value);
			QualityPresets.RestoreCurrentLevel();
			QualityPresets.AddCurrentLevel();
			Settings.OnQualityChanged(value, excludeChanged: true);
			Settings.PullFromConnections(exceptUnapplied: true);
			Settings.RefreshRegisteredResolvers();
			nint num = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v133 @ r8_v4 (Il2CppClass<Kamgam.SettingsGenerator.QualityConnection>)+258]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v133 @ r8_v4 (Il2CppClass<Kamgam.SettingsGenerator.QualityConnection>)+260]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v115 @ rax_v13 (should have been resolved before IL gen)");
		}
	}

	public void SetSettings(Settings settings)
	{
		Settings = settings;
	}

	public Settings GetSettings()
	{
		return Settings;
	}
}
