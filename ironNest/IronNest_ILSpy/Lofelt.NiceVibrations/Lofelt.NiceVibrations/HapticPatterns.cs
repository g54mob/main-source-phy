using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Lofelt.NiceVibrations;

public static class HapticPatterns
{
	public enum PresetType
	{
		Selection = 0,
		Success = 1,
		Warning = 2,
		Failure = 3,
		LightImpact = 4,
		MediumImpact = 5,
		HeavyImpact = 6,
		RigidImpact = 7,
		SoftImpact = 8,
		None = -1
	}

	private struct Pattern
	{
		public float[] time;

		public float[] amplitude;

		private static string clipJsonTemplate;

		static Pattern()
		{
			//IL_0013: Expected I, but got O
			//IL_001b: Expected I, but got O
			//IL_002b: Expected O, but got I
			//IL_0067: Expected O, but got I
			//IL_00a4: Expected O, but got I
			//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bf: Expected O, but got Unknown
			UnityEngine.Object obj = Resources.Load("nv-pattern-template");
			nint num = (nint)typeof(TextAsset);
			nint num2 = (nint)obj;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v42 @ r9_v2 (Il2CppClass<UnityEngine.TextAsset>)+130]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ rax_v5 (Il2CppClass<UnityEngine.Object>)+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v42 @ r9_v2 (Il2CppClass<UnityEngine.TextAsset>)+130]");
			if (num3 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ rax_v5 (Il2CppClass<UnityEngine.Object>)+C8]");
				object obj3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v62 @ rcx_v5+FFFFFFF8+v44 @ rcx_v4*8]");
				if (0 == (nint)typeof(TextAsset))
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v42 @ r9_v2 (Il2CppClass<UnityEngine.TextAsset>)+130]");
					object obj4 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v62 @ rcx_v5+FFFFFFF8+v86 @ r8_v1*8]");
					object obj5 = 0 - typeof(TextAsset);
					bool flag = obj5 == null;
					bool flag2 = !flag;
					TextAsset textAsset = null;
					if (!flag2)
					{
						textAsset = (TextAsset)obj;
					}
					string text = textAsset.text;
					clipJsonTemplate = text;
					return;
				}
			}
			throw new NullReferenceException();
		}

		public Pattern(float[] time, float[] amplitude)
		{
			this.time = time;
			this.amplitude = amplitude;
		}

		public unsafe GamepadRumble ToRumble()
		{
			//IL_0333: Expected native int or pointer, but got O
			//IL_033d: Expected native int or pointer, but got O
			//IL_0037: Expected O, but got I4
			//IL_004c: Expected native int or pointer, but got O
			//IL_0066: Expected O, but got I4
			//IL_007b: Expected native int or pointer, but got O
			//IL_0095: Expected O, but got I4
			//IL_00aa: Expected native int or pointer, but got O
			//IL_00c4: Expected O, but got I4
			//IL_00cd: Expected native int or pointer, but got O
			//IL_0104: Expected O, but got I4
			//IL_0120: Expected O, but got I4
			//IL_02ec: Expected native int or pointer, but got O
			//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ff: Expected O, but got Unknown
			GamepadRumble gamepadRumble = default(GamepadRumble);
			System.Runtime.CompilerServices.Unsafe.Write(&((GamepadRumble*)(nint)gamepadRumble)->durationsMs, null);
			System.Runtime.CompilerServices.Unsafe.Write(&((GamepadRumble*)(nint)gamepadRumble)->lowFrequencyMotorSpeeds, null);
			float[] array = time;
			if (array.Length > 1)
			{
				object obj = array.Length - 1;
				int[] durationsMs = new int[obj];
				System.Runtime.CompilerServices.Unsafe.Write(&((GamepadRumble*)(nint)gamepadRumble)->durationsMs, durationsMs);
				object obj2 = array.Length - 1;
				float[] lowFrequencyMotorSpeeds = new float[obj2];
				System.Runtime.CompilerServices.Unsafe.Write(&((GamepadRumble*)(nint)gamepadRumble)->lowFrequencyMotorSpeeds, lowFrequencyMotorSpeeds);
				object obj3 = array.Length - 1;
				float[] highFrequencyMotorSpeeds = new float[obj3];
				System.Runtime.CompilerServices.Unsafe.Write(&((GamepadRumble*)(nint)gamepadRumble)->highFrequencyMotorSpeeds, highFrequencyMotorSpeeds);
				object obj4 = array.Length - 1;
				((GamepadRumble*)(nint)gamepadRumble)->totalDurationMs = 0;
				if ((nint)obj4 > 0)
				{
					int num = 0;
					object obj5 = 32;
					int num2 = 0;
					while (true)
					{
						float[] array2 = time;
						object obj6 = num2 + 1;
						if ((nint)obj6 < array2.Length && num2 < array2.Length)
						{
							int[] durationsMs2 = gamepadRumble.durationsMs;
							if (num2 < durationsMs2.Length)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvttss2si eax,xmm0\"");
								float[] array3 = amplitude;
								float[] lowFrequencyMotorSpeeds2 = gamepadRumble.lowFrequencyMotorSpeeds;
								if (num2 < array3.Length && num2 < lowFrequencyMotorSpeeds2.Length)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v83 @ rdx_v11+v96 @ rax_v17 (System.Single[])]");
									_ = 0;
									float[] array4 = amplitude;
									float[] lowFrequencyMotorSpeeds3 = gamepadRumble.lowFrequencyMotorSpeeds;
									if (num2 < array4.Length)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm1,8\"");
										if (num2 < lowFrequencyMotorSpeeds3.Length)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v83 @ rdx_v11+v97 @ rax_v19 (System.Single[])]");
											_ = 0;
											int[] durationsMs3 = gamepadRumble.durationsMs;
											if (num2 < durationsMs3.Length)
											{
												num2++;
												int totalDurationMs = gamepadRumble.totalDurationMs;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v83 @ rdx_v11+v98 @ rax_v21 (System.Int32[])]");
												int totalDurationMs2 = (int)((nint)totalDurationMs + (nint)0);
												((GamepadRumble*)(nint)gamepadRumble)->totalDurationMs = totalDurationMs2;
												obj5 += 4;
												num++;
												if (num >= (nint)obj4)
												{
													break;
												}
												continue;
											}
										}
									}
								}
							}
						}
						return (GamepadRumble)new IndexOutOfRangeException();
					}
				}
			}
			return gamepadRumble;
		}

		public unsafe string ToClip()
		{
			//IL_001d: Expected O, but got I4
			//IL_002f: Expected O, but got I4
			//IL_0038: Expected O, but got I4
			//IL_007d: Expected F4, but got I
			//IL_00d9: Expected F4, but got I4
			//IL_0164: Unknown result type (might be due to invalid IL or missing references)
			//IL_0169: Expected O, but got Unknown
			//IL_0172: Unknown result type (might be due to invalid IL or missing references)
			//IL_0177: Expected O, but got Unknown
			//IL_0194: Expected Ref, but got F4
			//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_02b0: Expected O, but got Unknown
			//IL_0388: Unknown result type (might be due to invalid IL or missing references)
			//IL_038d: Expected O, but got Unknown
			//IL_0396: Unknown result type (might be due to invalid IL or missing references)
			//IL_039b: Expected O, but got Unknown
			if (clipJsonTemplate != null)
			{
				float[] array = time;
				object obj = 32;
				string text = "";
				object obj2 = 0;
				object obj3 = 0;
				float num3 = default(float);
				while (true)
				{
					if ((nint)obj3 < array.Length)
					{
						float[] array2 = amplitude;
						if ((nint)obj2 >= array2.Length)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v128 @ rax_v18 (System.Single[])+v116 @ rbp_v4]");
						float num = 0f;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v128 @ rax_v18 (System.Single[])+v116 @ rbp_v4]");
						if ((nint)0 <= (nint)0)
						{
							if (num > 1f)
							{
								num = 1f;
							}
						}
						else
						{
							num = 0f;
						}
						string[] array3 = new string[6];
						if (array3.Length <= 0)
						{
							break;
						}
						array3[0] = text;
						if (array3.Length <= 1)
						{
							break;
						}
						array3[1] = "{ \"time\":";
						float[] array4 = time;
						if ((nint)obj2 >= array4.Length)
						{
							break;
						}
						object obj4 = obj2 + 8;
						object obj5 = obj4 * 4;
						float num2 = (float)array4 + (float)obj5;
						string text2 = ((float*)num2)->ToString(numberFormat);
						if (array3.Length <= 2)
						{
							break;
						}
						array3[2] = text2;
						if (array3.Length <= 3)
						{
							break;
						}
						array3[3] = ",\"amplitude\":";
						string text3 = num3.ToString(numberFormat);
						if (array3.Length <= 4)
						{
							break;
						}
						array3[4] = text3;
						if (array3.Length <= 5)
						{
							break;
						}
						array3[5] = "}";
						string text4 = string.Concat(array3);
						float[] array5 = time;
						object obj6 = obj2 + 1;
						bool flag = (nint)obj6 >= array5.Length;
						text = text4;
						if (!flag)
						{
							string text5 = text4 + ",";
							text = text5;
						}
						array = time;
						obj2++;
						obj += 4;
						obj3 = obj2;
						continue;
					}
					return clipJsonTemplate.Replace("{amplitude-envelope}", text);
				}
				return (string)(object)new IndexOutOfRangeException();
			}
			return "";
		}
	}

	internal struct Preset
	{
		public PresetType type;

		public float[] maximumAmplitudePattern;

		public byte[] jsonClip;

		public GamepadRumble gamepadRumble;

		public Preset(PresetType type, float[] time, float[] amplitude)
		{
			Pattern pattern = default(Pattern);
			maximumAmplitudePattern = (float[])pattern;
			this.type = type;
			GamepadRumble gamepadRumble = pattern.ToRumble();
			this.gamepadRumble = (GamepadRumble)gamepadRumble.durationsMs;
			_ = gamepadRumble.lowFrequencyMotorSpeeds;
			Encoding uTF = Encoding.UTF8;
			string s = pattern.ToClip();
			byte[] bytes = uTF.GetBytes(s);
			jsonClip = bytes;
		}

		public float GetDuration()
		{
			//IL_0047: Expected O, but got I4
			//IL_0037: Expected F4, but got I4
			float[] array = maximumAmplitudePattern;
			if (array.Length == 0)
			{
				return 0f;
			}
			object obj = array.Length - 1;
			return array[obj];
		}
	}

	private static string emphasisTemplate;

	private static string constantTemplate;

	private static NumberFormatInfo numberFormat;

	private static float[] constantPatternTime;

	internal static Preset Selection;

	internal static Preset Light;

	internal static Preset Medium;

	internal static Preset Heavy;

	internal static Preset Rigid;

	internal static Preset Soft;

	internal static Preset Success;

	internal static Preset Failure;

	internal static Preset Warning;

	unsafe static HapticPatterns()
	{
		//IL_0008: Expected O, but got Ref
		//IL_001b: Expected I, but got O
		//IL_0023: Expected I, but got O
		//IL_0033: Expected O, but got I
		//IL_006f: Expected O, but got I
		//IL_00ac: Expected O, but got I
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Expected O, but got Unknown
		//IL_010f: Expected I, but got O
		//IL_0117: Expected I, but got O
		//IL_0127: Expected O, but got I
		//IL_0163: Expected O, but got I
		//IL_01a0: Expected O, but got I
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Expected O, but got Unknown
		//IL_0241: Expected F4, but got I4
		//IL_0267: Expected F4, but got I4
		//IL_027a: Expected F4, but got I4
		//IL_029e: Expected I, but got O
		//IL_02b8: Expected O, but got I4
		//IL_02f5: Expected F4, but got I4
		//IL_0327: Expected F4, but got I4
		//IL_033a: Expected F4, but got I4
		//IL_035e: Expected I, but got O
		//IL_0378: Expected O, but got I4
		//IL_03c3: Expected F4, but got I4
		//IL_03dc: Expected O, but got Ref
		//IL_040f: Expected F4, but got I4
		//IL_0422: Expected F4, but got I4
		//IL_0433: Expected native int or pointer, but got O
		//IL_0446: Expected I, but got O
		//IL_0467: Expected O, but got I
		//IL_04b9: Expected F4, but got I4
		//IL_04d2: Expected O, but got Ref
		//IL_0505: Expected F4, but got I4
		//IL_0518: Expected F4, but got I4
		//IL_0529: Expected native int or pointer, but got O
		//IL_053c: Expected I, but got O
		//IL_055d: Expected O, but got I
		//IL_05af: Expected F4, but got I4
		//IL_05c8: Expected O, but got Ref
		//IL_05fb: Expected F4, but got I4
		//IL_060e: Expected F4, but got I4
		//IL_061f: Expected native int or pointer, but got O
		//IL_0632: Expected I, but got O
		//IL_0653: Expected O, but got I
		//IL_06a5: Expected F4, but got I4
		//IL_06be: Expected O, but got Ref
		//IL_06f1: Expected F4, but got I4
		//IL_0704: Expected F4, but got I4
		//IL_0715: Expected native int or pointer, but got O
		//IL_0728: Expected I, but got O
		//IL_0749: Expected O, but got I
		//IL_07aa: Expected O, but got Ref
		//IL_07dd: Expected F4, but got I4
		//IL_07f0: Expected F4, but got I4
		//IL_0801: Expected native int or pointer, but got O
		//IL_0814: Expected I, but got O
		//IL_0835: Expected O, but got I
		//IL_08b1: Expected O, but got Ref
		//IL_08da: Expected native int or pointer, but got O
		//IL_08ed: Expected I, but got O
		//IL_090e: Expected O, but got I
		//IL_096f: Expected O, but got Ref
		//IL_09a2: Expected F4, but got I4
		//IL_09b5: Expected F4, but got I4
		//IL_09c6: Expected native int or pointer, but got O
		//IL_09d9: Expected I, but got O
		//IL_09fa: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		float[] array = new float[2];
		constantPatternTime = array;
		UnityEngine.Object obj3 = Resources.Load("nv-emphasis-template");
		nint num = (nint)typeof(TextAsset);
		nint num2 = (nint)obj3;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ r8_v3 (Il2CppClass<UnityEngine.TextAsset>)+130]");
		object obj4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ rax_v9 (Il2CppClass<UnityEngine.Object>)+130]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ r8_v3 (Il2CppClass<UnityEngine.TextAsset>)+130]");
		if (num3 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ rax_v9 (Il2CppClass<UnityEngine.Object>)+C8]");
			object obj5 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ rcx_v11+FFFFFFF8+v67 @ rcx_v10*8]");
			if (0 == (nint)typeof(TextAsset))
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ r8_v3 (Il2CppClass<UnityEngine.TextAsset>)+130]");
				object obj6 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ rcx_v11+FFFFFFF8+v455 @ rdx_v8*8]");
				object obj7 = 0 - typeof(TextAsset);
				bool flag = obj7 == null;
				bool flag2 = !flag;
				TextAsset textAsset = null;
				if (!flag2)
				{
					textAsset = (TextAsset)obj3;
				}
				string text = textAsset.text;
				emphasisTemplate = text;
				UnityEngine.Object obj8 = Resources.Load("nv-constant-template");
				nint num4 = (nint)typeof(TextAsset);
				nint num5 = (nint)obj8;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v158 @ r8_v4 (Il2CppClass<UnityEngine.TextAsset>)+130]");
				object obj9 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v197 @ rax_v15 (Il2CppClass<UnityEngine.Object>)+130]");
				nint num6 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v158 @ r8_v4 (Il2CppClass<UnityEngine.TextAsset>)+130]");
				if (num6 >= 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v197 @ rax_v15 (Il2CppClass<UnityEngine.Object>)+C8]");
					object obj10 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v217 @ rcx_v19+FFFFFFF8+v216 @ rcx_v18*8]");
					if (0 == (nint)typeof(TextAsset))
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v158 @ r8_v4 (Il2CppClass<UnityEngine.TextAsset>)+130]");
						object obj11 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v217 @ rcx_v19+FFFFFFF8+v801 @ rdx_v14*8]");
						object obj12 = 0 - typeof(TextAsset);
						bool flag3 = obj12 == null;
						bool flag4 = !flag3;
						TextAsset textAsset2 = null;
						if (!flag4)
						{
							textAsset2 = (TextAsset)obj8;
						}
						string text2 = textAsset2.text;
						constantTemplate = text2;
						NumberFormatInfo numberFormatInfo = new NumberFormatInfo();
						numberFormatInfo._002Ector();
						numberFormat = numberFormatInfo;
						numberFormat.NumberDecimalSeparator = ".";
						Preset preset = new Preset(PresetType.Selection, new float[2] { 0f, 1.025759E+09f }, new float[2] { 1.05599155E+09f, 1.05599155E+09f });
						nint num7 = (nint)typeof(HapticPatterns);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v857 @ rax_v28 (Il2CppClass<Lofelt.NiceVibrations.HapticPatterns>)+B8]");
						nint num8 = 0;
						Selection = (Preset)0;
						_ = 0;
						_ = 0;
						_ = 0;
						float[] time = new float[2] { 0f, 1.025759E+09f };
						_ = 0;
						_ = 0;
						Preset preset2 = new Preset(PresetType.LightImpact, time, new float[2] { 1.0422678E+09f, 1.0422678E+09f });
						nint num9 = (nint)typeof(HapticPatterns);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v873 @ rax_v34 (Il2CppClass<Lofelt.NiceVibrations.HapticPatterns>)+B8]");
						nint num10 = 0;
						Light = (Preset)0;
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ rbp_v1-78]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ rbp_v1-68]");
						_ = 0;
						float[] time2 = new float[2] { 0f, 1.0341476E+09f };
						_ = 0;
						Preset preset3 = (Preset)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 96));
						_ = 0;
						_ = 0;
						_ = 0;
						System.Runtime.CompilerServices.Unsafe.Write((void*)(nint)preset3, new Preset(PresetType.MediumImpact, time2, new float[2] { 1.05599155E+09f, 1.05599155E+09f }));
						nint num11 = (nint)typeof(HapticPatterns);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v889 @ rax_v40 (Il2CppClass<Lofelt.NiceVibrations.HapticPatterns>)+B8]");
						nint num12 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ rbp_v1-60]");
						Medium = (Preset)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ rbp_v1-50]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ rbp_v1-40]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ rbp_v1-30]");
						_ = 0;
						float[] time3 = new float[2] { 0f, 1.0425362E+09f };
						_ = 0;
						Preset preset4 = (Preset)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 40));
						_ = 0;
						_ = 0;
						_ = 0;
						System.Runtime.CompilerServices.Unsafe.Write((void*)(nint)preset4, new Preset(PresetType.HeavyImpact, time3, new float[2] { 1.0653532E+09f, 1.0653532E+09f }));
						nint num13 = (nint)typeof(HapticPatterns);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v905 @ rax_v46 (Il2CppClass<Lofelt.NiceVibrations.HapticPatterns>)+B8]");
						nint num14 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ rbp_v1-28]");
						Heavy = (Preset)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ rbp_v1-18]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ rbp_v1-8]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ rbp_v1+8]");
						_ = 0;
						float[] time4 = new float[2] { 0f, 1.025759E+09f };
						_ = 0;
						Preset preset5 = (Preset)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 16));
						_ = 0;
						_ = 0;
						_ = 0;
						System.Runtime.CompilerServices.Unsafe.Write((void*)(nint)preset5, new Preset(PresetType.RigidImpact, time4, new float[2] { 1.0653532E+09f, 1.0653532E+09f }));
						nint num15 = (nint)typeof(HapticPatterns);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v921 @ rax_v52 (Il2CppClass<Lofelt.NiceVibrations.HapticPatterns>)+B8]");
						nint num16 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ rbp_v1+10]");
						Rigid = (Preset)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ rbp_v1+20]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ rbp_v1+30]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ rbp_v1+40]");
						_ = 0;
						float[] time5 = new float[2] { 0f, 1.0425362E+09f };
						_ = 0;
						Preset preset6 = (Preset)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 72));
						_ = 0;
						_ = 0;
						_ = 0;
						System.Runtime.CompilerServices.Unsafe.Write((void*)(nint)preset6, new Preset(PresetType.SoftImpact, time5, new float[2] { 1.0422678E+09f, 1.0422678E+09f }));
						nint num17 = (nint)typeof(HapticPatterns);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v938 @ rax_v58 (Il2CppClass<Lofelt.NiceVibrations.HapticPatterns>)+B8]");
						nint num18 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ rbp_v1+48]");
						Soft = (Preset)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ rbp_v1+58]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ rbp_v1+68]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ rbp_v1+78]");
						_ = 0;
						float[] time6 = new float[4] { 0f, 0.04f, 0.08f, 0.24f };
						_ = 0;
						Preset preset7 = (Preset)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 128));
						_ = 0;
						_ = 0;
						_ = 0;
						System.Runtime.CompilerServices.Unsafe.Write((void*)(nint)preset7, new Preset(PresetType.Success, time6, new float[4] { 0f, 1.04233485E+09f, 0f, 1.0653532E+09f }));
						nint num19 = (nint)typeof(HapticPatterns);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v960 @ rax_v65 (Il2CppClass<Lofelt.NiceVibrations.HapticPatterns>)+B8]");
						nint num20 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ rbp_v1+80]");
						Success = (Preset)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ rbp_v1+90]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ rbp_v1+A0]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ rbp_v1+B0]");
						_ = 0;
						float[] time7 = new float[8] { 0f, 0.08f, 0.12f, 0.2f, 0.24f, 0.4f, 0.44f, 0.48f };
						float[] amplitude = new float[8] { 0f, 0.47f, 0f, 0.47f, 0f, 1f, 0f, 0.157f };
						Preset preset8 = (Preset)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 184));
						_ = 0;
						_ = 0;
						_ = 0;
						_ = 0;
						System.Runtime.CompilerServices.Unsafe.Write((void*)(nint)preset8, new Preset(PresetType.Failure, time7, amplitude));
						nint num21 = (nint)typeof(HapticPatterns);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v989 @ rax_v74 (Il2CppClass<Lofelt.NiceVibrations.HapticPatterns>)+B8]");
						nint num22 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ rbp_v1+B8]");
						Failure = (Preset)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ rbp_v1+C8]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ rbp_v1+D8]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ rbp_v1+E8]");
						_ = 0;
						float[] time8 = new float[4] { 0f, 0.12f, 0.24f, 0.28f };
						_ = 0;
						Preset preset9 = (Preset)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 240));
						_ = 0;
						_ = 0;
						_ = 0;
						System.Runtime.CompilerServices.Unsafe.Write((void*)(nint)preset9, new Preset(PresetType.Warning, time8, new float[4] { 0f, 1.0653532E+09f, 0f, 1.05595795E+09f }));
						nint num23 = (nint)typeof(HapticPatterns);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1008 @ rax_v81 (Il2CppClass<Lofelt.NiceVibrations.HapticPatterns>)+B8]");
						nint num24 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ rbp_v1+F0]");
						Warning = (Preset)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ rbp_v1+100]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ rbp_v1+110]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ rbp_v1+120]");
						_ = 0;
						return;
					}
				}
			}
		}
		throw new NullReferenceException();
	}

	public unsafe static void PlayEmphasis(float amplitude, float frequency)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Expected O, but got Unknown
		//IL_007c: Invalid comparison between I4 and F4
		//IL_00cf: Expected F4, but got I4
		//IL_0311: Invalid comparison between I4 and F4
		//IL_0113: Expected F4, but got I4
		//IL_0370: Expected Ref, but got F4
		//IL_014d: Expected Ref, but got F4
		//IL_0190: Expected Ref, but got F4
		//IL_0213: Expected F4, but got I
		//IL_0245: Expected F4, but got I
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Expected O, but got Unknown
		object obj2 = default(object);
		object obj = obj2 - 95;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		if (emphasisTemplate == null || !HapticController._hapticsEnabled)
		{
			return;
		}
		if (!HapticController.Init() && !GamepadRumbler.IsConnected())
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180A7BA10");
			return;
		}
		if (!(0f > amplitude))
		{
			bool flag = !(amplitude > 1f);
			float num = amplitude;
			if (!flag)
			{
				num = 1f;
			}
		}
		else
		{
			float num = 0f;
		}
		if (!(0f > frequency))
		{
			bool flag2 = !(frequency > 1f);
			float num2 = frequency;
			if (!flag2)
			{
				num2 = 1f;
			}
		}
		else
		{
			float num2 = 0f;
		}
		float num3 = (float)obj + 127f;
		string newValue = ((float*)num3)->ToString(numberFormat);
		string text = emphasisTemplate.Replace("{amplitude}", newValue);
		float num4 = (float)obj - 41f;
		string newValue2 = ((float*)num4)->ToString(numberFormat);
		string text2 = text.Replace("{frequency}", newValue2);
		_ = 1036831949;
		float num5 = (float)obj - 37f;
		string newValue3 = ((float*)num5)->ToString(numberFormat);
		string s = text2.Replace("{duration}", newValue3);
		_ = 0;
		_ = 0;
		int[] array = new int[1] { 100 };
		float[] array2 = new float[1];
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+7F]");
		array2[0] = 0f;
		float[] array3 = new float[1];
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-29]");
		array3[0] = 0f;
		Encoding uTF = Encoding.UTF8;
		byte[] bytes = uTF.GetBytes(s);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-21]");
		_ = 0;
		GamepadRumble rumble = (GamepadRumble)(obj + 7);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-11]");
		_ = 0;
		HapticController.Load(bytes, rumble);
		HapticController.Loop(enabled: false);
		HapticController.Play();
	}

	private static PresetType presetTypeForEmphasis(float amplitude)
	{
		//IL_0045: Invalid comparison between F4 and I4
		//IL_009f: Expected O, but got I4
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Expected I4, but got Unknown
		if (!(amplitude > 0.5f))
		{
			bool flag = 0.5f < amplitude;
			float num = 0.5f - amplitude;
			bool flag2 = num == 0f;
			if (!(0.5f < amplitude))
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm0\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm0,qword ptr [1822E7C20h]\"");
				bool flag3 = !flag;
				bool flag4 = !flag2;
				object obj = flag4 & flag3;
				return (PresetType)(obj + 4);
			}
			return PresetType.LightImpact;
		}
		return PresetType.HeavyImpact;
	}

	public unsafe static void PlayConstant(float amplitude, float frequency, float duration)
	{
		//IL_0063: Invalid comparison between I4 and F4
		//IL_00b6: Expected F4, but got I4
		//IL_02bc: Invalid comparison between F4 and I4
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Expected O, but got Unknown
		//IL_027d: Expected O, but got Ref
		//IL_018a: Expected O, but got I4
		//IL_0193: Expected O, but got I4
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Expected O, but got Unknown
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Expected O, but got Unknown
		if (constantTemplate == null || !HapticController._hapticsEnabled)
		{
			return;
		}
		float clipLevel;
		if (!(0f > amplitude))
		{
			bool flag = !(amplitude > 1f);
			clipLevel = amplitude;
			if (!flag)
			{
				clipLevel = 1f;
			}
		}
		else
		{
			clipLevel = 0f;
		}
		string text = default(string);
		if (duration > 0f)
		{
			float num = default(float);
			string newValue = num.ToString(numberFormat);
			text = constantTemplate.Replace("{duration}", newValue);
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvttss2si eax,xmm1\"");
			object obj = (object)text >> 31;
			object obj2 = obj & 0xF;
			object obj3 = text + obj2;
			object obj4 = obj3 >> 4;
			int[] array = new int[obj4];
			float[] array2 = new float[obj4];
			float[] array3 = new float[obj4];
			if ((nint)obj4 > 0)
			{
				object obj5 = 0;
				object obj6 = 32;
				do
				{
					_ = 16;
					_ = 1065353216;
					_ = 1065353216;
					obj6 += 4;
					obj5++;
				}
				while (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj5) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4));
			}
			if (HapticController.Init())
			{
				goto IL_024c;
			}
		}
		Gamepad gamepad = GamepadRumbler.GetGamepad(GamepadRumbler.currentGamepadID);
		if (gamepad == null)
		{
			return;
		}
		goto IL_024c;
		IL_024c:
		Encoding uTF = Encoding.UTF8;
		byte[] bytes = uTF.GetBytes(text);
		object obj7 = default(object);
		HapticController.Load(bytes, (GamepadRumble)(&obj7));
		bool flag2 = HapticController.Init();
		HapticController.isLoopingEnabledByUser = false;
		HapticController.clipLevel = clipLevel;
		bool flag3 = HapticController.Init();
		HapticController.Play();
	}

	private unsafe static Preset GetPresetForType(PresetType type)
	{
		//IL_0044: Expected I, but got O
		//IL_0084: Expected I4, but got O
		//IL_007f: Expected native int or pointer, but got O
		//IL_0099: Expected O, but got I
		//IL_0094: Expected native int or pointer, but got O
		//IL_0012: Expected O, but got I8
		//IL_002c: Expected O, but got I8
		if (type <= PresetType.SoftImpact)
		{
			object obj = 6442450944L;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ rdx_v1+A7EADC+type @ rdx (Lofelt.NiceVibrations.HapticPatterns+PresetType)*4]");
			object obj2 = 0 + 6442450944L;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v45 @ rcx_v5 (should have been resolved before IL gen)");
		}
		nint num = (nint)typeof(HapticPatterns);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v84 @ rax_v3 (Il2CppClass<Lofelt.NiceVibrations.HapticPatterns>)+B8]");
		nint num2 = 0;
		Preset preset = default(Preset);
		((Preset*)(nint)preset)->type = (PresetType)Medium;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v86 @ rax_v4 (Il2CppStaticFields<Lofelt.NiceVibrations.HapticPatterns>)+A0]");
		System.Runtime.CompilerServices.Unsafe.Write(&((Preset*)(nint)preset)->jsonClip, (byte[])0);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v86 @ rax_v4 (Il2CppStaticFields<Lofelt.NiceVibrations.HapticPatterns>)+B0]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v86 @ rax_v4 (Il2CppStaticFields<Lofelt.NiceVibrations.HapticPatterns>)+C0]");
		_ = 0;
		return preset;
	}

	public unsafe static void PlayPreset(PresetType presetType)
	{
		//IL_00b6: Expected O, but got Ref
		if (HapticController._hapticsEnabled && presetType != PresetType.None)
		{
			Preset presetForType = GetPresetForType(presetType);
			if (!HapticController.Init() && !GamepadRumbler.IsConnected())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180A7BA10");
				return;
			}
			object obj = default(object);
			HapticController.Load(presetForType.jsonClip, (GamepadRumble)(&obj));
			HapticController.Loop(enabled: false);
			HapticController.Play();
		}
	}

	public static float GetPresetDuration(PresetType presetType)
	{
		//IL_0071: Expected F4, but got I4
		//IL_0058: Expected O, but got I4
		if (presetType != PresetType.None)
		{
			float[] maximumAmplitudePattern = GetPresetForType(presetType).maximumAmplitudePattern;
			if (maximumAmplitudePattern.Length != 0)
			{
				object obj = maximumAmplitudePattern.Length - 1;
				return maximumAmplitudePattern[obj];
			}
		}
		return 0f;
	}
}
