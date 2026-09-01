using System;
using Cpp2ILInjected;
using UnityEngine;

public class EspressoCupNoteWriter : MonoBehaviour
{
	private string sectionTag;

	private NotepadSection targetSection;

	private string noteFormat;

	private NotepadSection.WriteMode writeMode;

	private NotepadSection.AddPosition addPosition;

	private float writeDelaySeconds;

	private NotepadSection.TextRevealMode revealMode;

	private float typewriterSecondsPerCharacter;

	private bool debugLog;

	private EspressoCup _cup;

	private string _snapshotLabel;

	private string _snapshotGrade;

	private float _snapshotQuality;

	private float _snapshotPressure;

	private float _snapshotTemperature;

	private float _snapshotTiming;

	private bool _hasSnapshot;

	private void Awake()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		EspressoCup cup = default(EspressoCup);
		_cup = cup;
	}

	public void SnapshotCupData()
	{
		//IL_0047: Expected O, but got I
		//IL_0057: Expected O, but got I
		//IL_00a3: Expected O, but got I
		//IL_00b3: Expected O, but got I
		EspressoCup cup = _cup;
		string snapshotLabel = cup.coffeeLabel;
		if (cup.coffeeLabel == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v131 @ rax_v45+B8]");
			object obj2 = 0;
			snapshotLabel = (string)obj2;
		}
		_snapshotLabel = snapshotLabel;
		string text = _cup.QualityGrade;
		if (text == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v357 @ rax_v43+B8]");
			object obj4 = 0;
			text = (string)obj4;
		}
		_snapshotGrade = text;
		EspressoCup cup2 = _cup;
		_snapshotQuality = cup2.quality;
		EspressoCup cup3 = _cup;
		_snapshotPressure = cup3.pressureScore;
		EspressoCup cup4 = _cup;
		_snapshotTemperature = cup4.temperatureScore;
		EspressoCup cup5 = _cup;
		bool flag = !debugLog;
		_snapshotTiming = cup5.timingScore;
		_hasSnapshot = true;
		if (!flag)
		{
			string text2 = base.name;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg = default(object);
			string text3 = $"Grade='{_snapshotGrade}' Quality={arg:F2}% ";
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg2 = default(object);
			object arg3 = default(object);
			string text4 = $"Pressure={arg2:F2}% Temperature={arg3:F2}% ";
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg4 = default(object);
			string text5 = $"Timing={arg4:F2}%";
			string message = "[" + text2 + "] SnapshotCupData captured — Label='" + _snapshotLabel + "' " + text3 + text4 + text5;
			Debug.Log(message, this);
		}
	}

	public unsafe void WriteNote()
	{
		//IL_0043: Expected O, but got I4
		//IL_00ec: Expected Ref, but got F4
		//IL_02a5: Invalid comparison between I4 and F4
		//IL_0126: Expected Ref, but got F4
		//IL_0160: Expected Ref, but got F4
		//IL_026a: Expected O, but got I4
		//IL_019a: Expected Ref, but got F4
		//IL_01c2: Expected O, but got I4
		if (!_hasSnapshot)
		{
			string text = base.name;
			string message = "[" + text + "] WriteNote called but no snapshot exists. Wire EspressoCupDrinker.OnDrinkStarted → SnapshotCupData() to fix empty note output.";
			Debug.LogWarning(message, this);
			object obj = 0;
		}
		string text8;
		if (_hasSnapshot)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AA8C]");
			if ((nint)0 == 0)
			{
				_ = 1;
			}
			string text2 = noteFormat.Replace("{label}", _snapshotLabel);
			string text3 = text2.Replace("{grade}", _snapshotGrade);
			float num = (float)this + 104f;
			string newValue = ((float*)num)->ToString("F2");
			string text4 = text3.Replace("{quality}", newValue);
			float num2 = (float)this + 108f;
			string newValue2 = ((float*)num2)->ToString("F2");
			string text5 = text4.Replace("{pressure}", newValue2);
			float num3 = (float)this + 112f;
			string newValue3 = ((float*)num3)->ToString("F2");
			string text6 = text5.Replace("{temperature}", newValue3);
			float num4 = (float)this + 116f;
			string text7 = ((float*)num4)->ToString("F2");
			text8 = text6.Replace("{timing}", text7);
			object obj = 0;
			string text9 = text7;
		}
		else
		{
			text8 = BuildNoteFromCup(_cup);
			string text9 = null;
		}
		if (debugLog)
		{
			string arg = base.name;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg2 = default(object);
			string text10 = $"[{arg}] WriteNote called. Delay={arg2}s.\n";
			string message2 = text10 + "Note preview:\n" + text8;
			Debug.Log(message2, this);
			object obj = 0;
		}
		NotepadSection notepadSection = ResolveSection();
		if (notepadSection != null)
		{
			if (0f < writeDelaySeconds)
			{
			}
			float delaySeconds = default(float);
			NotepadSection.TextRevealMode textRevealMode = default(NotepadSection.TextRevealMode);
			float num5 = default(float);
			notepadSection.Write(text8, writeMode, addPosition, delaySeconds, textRevealMode, num5);
			if (debugLog)
			{
				string text11 = base.name;
				string text12 = notepadSection.name;
				string message3 = "[" + text11 + "] Note queued on section '" + text12 + "'.";
				Debug.Log(message3, notepadSection);
			}
			_hasSnapshot = false;
		}
		else
		{
			string text13 = base.name;
			string message4 = "[" + text13 + "] WriteNote: could not resolve NotepadSection (tag='" + sectionTag + "'). Note discarded.";
			Debug.LogWarning(message4, this);
		}
	}

	private unsafe string BuildNoteFromSnapshot()
	{
		//IL_009d: Expected Ref, but got F4
		//IL_00f4: Expected Ref, but got F4
		//IL_014b: Expected Ref, but got F4
		//IL_01a2: Expected Ref, but got F4
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AA8C]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		if (noteFormat != null)
		{
			string text = noteFormat.Replace("{label}", _snapshotLabel);
			if (text != null)
			{
				string text2 = text.Replace("{grade}", _snapshotGrade);
				float num = (float)this + 104f;
				string newValue = ((float*)num)->ToString("F2");
				if (text2 != null)
				{
					string text3 = text2.Replace("{quality}", newValue);
					float num2 = (float)this + 108f;
					string newValue2 = ((float*)num2)->ToString("F2");
					if (text3 != null)
					{
						string text4 = text3.Replace("{pressure}", newValue2);
						float num3 = (float)this + 112f;
						string newValue3 = ((float*)num3)->ToString("F2");
						if (text4 != null)
						{
							string text5 = text4.Replace("{temperature}", newValue3);
							float num4 = (float)this + 116f;
							string newValue4 = ((float*)num4)->ToString("F2");
							if (text5 != null)
							{
								return text5.Replace("{timing}", newValue4);
							}
						}
					}
				}
			}
		}
		return (string)(object)new NullReferenceException();
	}

	private string BuildNoteFromCup(EspressoCup cup)
	{
		//IL_0075: Expected O, but got I
		//IL_0085: Expected O, but got I
		//IL_00ec: Expected O, but got I
		//IL_00fc: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AA8D]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		if ((object)cup != null)
		{
			string newValue = cup.coffeeLabel;
			if (cup.coffeeLabel == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
				object obj = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v181 @ rax_v19+B8]");
				object obj2 = 0;
				newValue = (string)obj2;
			}
			if (noteFormat != null)
			{
				string text = noteFormat.Replace("{label}", newValue);
				string text2 = cup.QualityGrade;
				if (text2 == null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
					object obj3 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v232 @ rax_v17+B8]");
					object obj4 = 0;
					text2 = (string)obj4;
				}
				if (text != null)
				{
					string text3 = text.Replace("{grade}", text2);
					float num = default(float);
					string newValue2 = num.ToString("F2");
					if (text3 != null)
					{
						string text4 = text3.Replace("{quality}", newValue2);
						string newValue3 = num.ToString("F2");
						if (text4 != null)
						{
							string text5 = text4.Replace("{pressure}", newValue3);
							string newValue4 = num.ToString("F2");
							if (text5 != null)
							{
								string text6 = text5.Replace("{temperature}", newValue4);
								string newValue5 = num.ToString("F2");
								if (text6 != null)
								{
									return text6.Replace("{timing}", newValue5);
								}
							}
						}
					}
				}
			}
		}
		return (string)(object)new NullReferenceException();
	}

	private NotepadSection ResolveSection()
	{
		NotepadSection notepadSection;
		if (targetSection == null)
		{
			notepadSection = NotepadSection.ResolveByTag(sectionTag);
			if (notepadSection != null && debugLog)
			{
				string[] array = new string[7];
				if (array != null)
				{
					array[0] = "[";
					string text = base.name;
					array[1] = text;
					array[2] = "] NotepadSection resolved via tag '";
					array[3] = sectionTag;
					array[4] = "': '";
					if ((object)notepadSection != null)
					{
						string text2 = notepadSection.name;
						array[5] = text2;
						array[6] = "'.";
						string message = string.Concat(array);
						Debug.Log(message, notepadSection);
						goto IL_01ac;
					}
				}
				return (NotepadSection)(object)new NullReferenceException();
			}
			goto IL_01ac;
		}
		return targetSection;
		IL_01ac:
		return notepadSection;
	}

	public EspressoCupNoteWriter()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AA8F]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		sectionTag = "MainNotes";
		noteFormat = "[ {label} ] Grade: {grade}\nQuality:     {quality}%\n  Pressure:    {pressure}%\n  Temperature: {temperature}%\n  Timing:      {timing}%";
		writeDelaySeconds = 1.5f;
		revealMode = NotepadSection.TextRevealMode.Typewriter;
		typewriterSecondsPerCharacter = 0.04f;
		base._002Ector();
	}
}
