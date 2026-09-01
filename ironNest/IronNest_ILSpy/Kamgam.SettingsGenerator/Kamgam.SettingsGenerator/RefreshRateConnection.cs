using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class RefreshRateConnection : ConnectionWithOptions<string>
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Comparison<RefreshRate> _003C_003E9__7_0;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal int _003CgetRefreshRates_003Eb__7_0(RefreshRate a, RefreshRate b)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm2,rax\"");
			object obj = (object)b >> 32;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm0,rdx\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm1,rax\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm2,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm0,r8\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm1,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm2,xmm1\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
			int result = default(int);
			return result;
		}
	}

	public bool CacheRefreshRates = true;

	public bool LimitToCurrentResolution;

	public int MinRate;

	public int MaxRate = 1000;

	protected List<RefreshRate> _values;

	protected List<string> _labels;

	protected string _rateNameInOptionLabel = "Hz";

	protected RefreshRate? lastKnownRefreshRate;

	protected int lastSetFrame;

	protected unsafe List<RefreshRate> getRefreshRates()
	{
		//IL_006c: Expected O, but got Ref
		//IL_0085: Expected O, but got I4
		//IL_00a4: Expected O, but got I4
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Expected O, but got Unknown
		//IL_0287: Expected O, but got Ref
		if (_values == null || !CacheRefreshRates)
		{
			List<RefreshRate> values = new List<RefreshRate>();
			_values = values;
			Resolution currentResolution = Screen.currentResolution;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E583B0");
			object obj = default(object);
			_values.Add((RefreshRate)(&obj));
			Resolution[] resolutions = Screen.resolutions;
			object obj2 = 0;
			object obj3 = default(object);
			obj = obj3;
			int width = currentResolution.m_Width;
			object obj5 = default(object);
			object obj6 = default(object);
			object obj7 = default(object);
			object obj8 = default(object);
			RefreshRate rate = default(RefreshRate);
			for (object obj4 = 0; (nint)obj4 < resolutions.Length; obj2++, obj4 = obj2)
			{
				if ((nint)obj2 < resolutions.Length)
				{
					if (LimitToCurrentResolution)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3E0");
						Resolution currentResolution2 = Screen.currentResolution;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3E0");
						bool flag = obj5 != obj6;
						width = currentResolution2.m_Width;
						if (flag)
						{
							continue;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9CF10");
						Resolution currentResolution3 = Screen.currentResolution;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9CF10");
						bool flag2 = obj7 != obj8;
						width = currentResolution3.m_Width;
						width = currentResolution3.m_Width;
						if (flag2)
						{
							continue;
						}
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E583B0");
					bool flag3 = contains(_values, rate);
					if (flag3)
					{
						continue;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E583B0");
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm0,rax\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm2,rcx\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm2,xmm0\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm1,xmm2\"");
					if ((flag3 ? 1 : 0) <= (false ? 1 : 0))
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E583B0");
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm0,rax\"");
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm2,rcx\"");
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm2,xmm0\"");
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm2,xmm1\"");
						if ((flag3 ? 1 : 0) <= (false ? 1 : 0))
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E583B0");
							_values.Add((RefreshRate)(&obj));
						}
					}
					continue;
				}
				return (List<RefreshRate>)(object)new IndexOutOfRangeException();
			}
			Comparison<RefreshRate> comparison = _003C_003Ec._003C_003E9__7_0;
			if (_003C_003Ec._003C_003E9__7_0 == null)
			{
				comparison = (_003C_003Ec._003C_003E9__7_0 = delegate(RefreshRate a, RefreshRate b)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm2,rax\"");
					object obj9 = (object)b >> 32;
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm0,rdx\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm1,rax\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm2,xmm0\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm0,r8\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm1,xmm0\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm2,xmm1\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
					int result = default(int);
					return result;
				});
			}
			_values.Sort(comparison);
		}
		return _values;
	}

	protected bool contains(List<RefreshRate> rates, RefreshRate rate)
	{
		//IL_005b: Expected O, but got I4
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Expected O, but got Unknown
		if (rates != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [rates @ rdx (System.Collections.Generic.List`1<UnityEngine.RefreshRate>)+18]");
			if ((nint)0 != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm1,rax\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm0,rax\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm1,xmm0\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
				object obj = 0;
				object obj3 = default(object);
				object obj4 = default(object);
				while (true)
				{
					object obj2 = obj;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [rates @ rdx (System.Collections.Generic.List`1<UnityEngine.RefreshRate>)+18]");
					if ((nint)obj2 >= 0)
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm1,rcx\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm0,rax\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm1,xmm0\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
					if (obj3 != obj4)
					{
						obj++;
						continue;
					}
					return true;
				}
			}
		}
		return false;
	}

	public override List<string> GetOptionLabels()
	{
		//IL_02b3: Expected I, but got O
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Expected O, but got Unknown
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Expected O, but got Unknown
		if (_labels == null || !CacheRefreshRates)
		{
			List<string> labels = new List<string>();
			_labels = labels;
			List<RefreshRate> refreshRates = getRefreshRates();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			List<RefreshRate>.Enumerator enumerator = default(List<RefreshRate>.Enumerator);
			double num2 = default(double);
			int num4 = default(int);
			while (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm6,rcx\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm0,rax\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm6,xmm0\"");
				nint num = (nint)typeof(Math);
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033EEB0");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm6,xmm10\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v320 @ rcx_v15 (Il2CppClass<System.Math>)+E4]");
				if ((nint)0 >= (nint)0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,xmm7\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180A18540h\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v320 @ rcx_v15 (Il2CppClass<System.Math>)+E4]");
					if ((nint)0 == 0)
					{
						object obj = num2 & 1;
						if (obj != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm0,xmm8\"");
						}
					}
					else
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm6,xmm7\"");
						double num3 = Math.Floor(0.0);
					}
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,xmm9\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180A18570h\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v320 @ rcx_v15 (Il2CppClass<System.Math>)+E4]");
					if ((nint)0 == 0)
					{
						object obj2 = num2 & 1;
						if (obj2 != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm0,xmm8\"");
						}
					}
					else
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm6,xmm7\"");
						double num3 = Math.Ceiling(0.0);
					}
				}
				string text = num4.ToString();
				string item = text + _rateNameInOptionLabel;
				if (_labels != null)
				{
					_labels.Add(item);
					continue;
				}
				throw new NullReferenceException();
			}
			enumerator.Dispose();
		}
		return _labels;
	}

	public override void RefreshOptionLabels()
	{
		//IL_000c: Expected I, but got O
		//IL_001c: Expected O, but got I
		//IL_002c: Expected O, but got I
		_labels = null;
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx_v2 (Il2CppClass<Kamgam.SettingsGenerator.RefreshRateConnection>)+2C8]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx_v2 (Il2CppClass<Kamgam.SettingsGenerator.RefreshRateConnection>)+2D0]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v10 @ rax_v2 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	public override void SetOptionLabels(List<string> optionLabels)
	{
		if (optionLabels != null && optionLabels._size != 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			string rateNameInOptionLabel = default(string);
			_rateNameInOptionLabel = rateNameInOptionLabel;
			RefreshOptionLabels();
			Logger.LogWarning("Setting each label name is not supported. Use SetOptionLabel() instead. Using the firast given as the new base label.");
		}
	}

	public void SetOptionLabel(string rateNameInOptionLabel)
	{
		//IL_000f: Expected I, but got O
		//IL_001f: Expected O, but got I
		//IL_002f: Expected O, but got I
		_rateNameInOptionLabel = rateNameInOptionLabel;
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx_v1 (Il2CppClass<Kamgam.SettingsGenerator.RefreshRateConnection>)+2E8]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx_v1 (Il2CppClass<Kamgam.SettingsGenerator.RefreshRateConnection>)+2F0]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v10 @ rax_v2 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	public override int Get()
	{
		//IL_014f: Expected O, but got I4
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		//IL_0010: Expected O, but got I4
		//IL_0137: Expected I4, but got O
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Expected O, but got Unknown
		//IL_00df: Invalid comparison between F4 and I4
		int frameCount = Time.frameCount;
		object obj = frameCount - lastSetFrame;
		if ((nint)obj > 3)
		{
			lastKnownRefreshRate = (RefreshRate?)(object)0;
			_ = 0;
		}
		Resolution currentResolution = Screen.currentResolution;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E583B0");
		object obj2 = this + 80;
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj3 = default(object);
		if (obj3 != null)
		{
			object obj4 = this + 80;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808CBE10");
		}
		List<RefreshRate> refreshRates = getRefreshRates();
		if (refreshRates != null)
		{
			int num2 = 0;
			int num3 = 0;
			while (true)
			{
				int num4 = num2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v113 @ rax_v13 (System.Collections.Generic.List`1<UnityEngine.RefreshRate>)+18]");
				if ((nint)num4 >= (nint)0)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm2,rcx\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm0,rax\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm1,rax\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm2,xmm0\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm0,rbp\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm1,xmm0\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm2,xmm1\"");
				if (!(0.01f > 0f))
				{
					num3++;
					num2 = num3;
					continue;
				}
				return num3;
			}
			return 0;
		}
		NullReferenceException ex = new NullReferenceException();
		return (int)ex;
	}

	public unsafe override void Set(int index)
	{
		//IL_00cf: Expected O, but got Ref
		//IL_00dd: Expected O, but got I4
		//IL_0103: Expected O, but got Ref
		//IL_008a: Expected O, but got I4
		List<RefreshRate> refreshRates = getRefreshRates();
		int value;
		if (index >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rax_v2 (System.Collections.Generic.List`1<UnityEngine.RefreshRate>)+18]");
			int num = (int)(-1);
			bool flag = index <= num;
			value = index;
			if (!flag)
			{
				value = num;
			}
		}
		else
		{
			value = 0;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
		ScreenOrchestrator instance = ScreenOrchestrator.Instance;
		object obj = default(object);
		RefreshRate? refreshRate = (RefreshRate)(&obj);
		instance.requestedRefreshRate = (RefreshRate?)(object)0;
		_ = 0;
		int frameCount = Time.frameCount;
		lastSetFrame = frameCount;
		RefreshRate? refreshRate2 = (RefreshRate)(&obj);
		lastKnownRefreshRate = (RefreshRate?)(object)0;
		_ = 0;
		base.NotifyListenersIfChanged(value);
	}
}
