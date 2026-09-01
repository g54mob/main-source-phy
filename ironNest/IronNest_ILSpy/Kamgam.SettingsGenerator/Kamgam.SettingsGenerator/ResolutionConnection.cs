using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class ResolutionConnection : ConnectionWithOptions<string>, IConnectionWithSettingsAccess
{
	[Serializable]
	public class CustomResolution
	{
		public int Width = 1024;

		public int Height = 768;

		public uint RefreshNumerator = 60000u;

		public uint RefreshDenominator = 1001u;

		public unsafe Resolution ToResolution()
		{
			//IL_0009: Expected native int or pointer, but got O
			//IL_0040: Expected O, but got I4
			Resolution resolution = default(Resolution);
			((Resolution*)(nint)resolution)->m_Width = 0;
			((Resolution*)resolution)->width = Width;
			((Resolution*)resolution)->height = Height;
			((Resolution*)resolution)->refreshRateRatio = (RefreshRate)RefreshNumerator;
			return resolution;
		}

		public static Resolution[] ToResolutions(List<CustomResolution> customResolutions)
		{
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Expected O, but got Unknown
			//IL_002e: Expected O, but got I4
			//IL_0094: Unknown result type (might be due to invalid IL or missing references)
			//IL_0099: Expected O, but got Unknown
			//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
			//IL_00af: Expected O, but got Unknown
			Resolution[] array = new Resolution[customResolutions._size];
			object obj = array + 32;
			object obj2 = 0;
			object obj3 = default(object);
			while (true)
			{
				if ((nint)obj2 < customResolutions._size)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1805E11B0");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180623550");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180974FC0");
					if ((nint)obj2 >= array.Length)
					{
						break;
					}
					obj2++;
					obj = obj3;
					obj += 16;
					continue;
				}
				return array;
			}
			return (Resolution[])(object)new IndexOutOfRangeException();
		}
	}

	public static bool AllowResolutionChangeOnMobile = false;

	public bool CacheResolutions = true;

	public bool LimitToCurrentRefreshRate;

	public bool LimitToUniqueResolutions = true;

	public bool LimitMaxResolutionToDisplayResolution;

	public bool SkipResolutinsSmallerThanHD = true;

	public bool SkipRefreshRatesWith59Hz;

	public bool AddRefreshRateToLabels;

	public bool RefreshRateResolversAfterCompletion = true;

	protected bool _addCustomResolutionOptionIfWindowed;

	private Action m_OnMaxResolutionChanged;

	public List<Vector2Int> AllowedAspectRatios;

	public float AllowedAspectRatioDelta;

	public List<CustomResolution> CustomResolutions;

	protected Settings _settings;

	protected List<Resolution> _values;

	protected List<string> _labels;

	protected string _resolutionFormat;

	protected string _refreshRateFormat;

	protected Vector2Int _lastMonitorMaxResolution;

	protected Resolution? _windowedResolution;

	protected Resolution? lastKnownResolution;

	protected int lastSetFrame;

	private static List<SettingOption> s_tmpOptionSettingsList;

	public unsafe bool AddCustomResolutionOptionIfWindowed
	{
		get
		{
			return _addCustomResolutionOptionIfWindowed;
		}
		set
		{
			//IL_033b: Expected O, but got I4
			//IL_0340: Expected I, but got O
			//IL_0161: Expected O, but got I4
			//IL_0166: Expected I, but got O
			//IL_0362: Expected O, but got I4
			//IL_0367: Expected I, but got O
			//IL_0136: Expected I4, but got O
			//IL_03a0: Expected I4, but got O
			//IL_03a9: Expected O, but got I4
			//IL_03ae: Expected I, but got O
			//IL_026c: Expected O, but got Ref
			//IL_03d9: Expected I4, but got O
			//IL_03e2: Expected O, but got I4
			//IL_03e7: Expected I, but got O
			//IL_02c3: Expected O, but got I4
			//IL_02c8: Expected I, but got O
			bool flag = default(bool);
			if (flag == _addCustomResolutionOptionIfWindowed)
			{
				return;
			}
			_addCustomResolutionOptionIfWindowed = flag;
			if (!flag)
			{
				return;
			}
			ScreenSizeObserver instance = ScreenSizeObserver.Instance;
			Delegate obj4;
			Delegate obj5 = default(Delegate);
			NullReferenceException ex;
			Delegate typeFromHandle;
			if ((object)instance != null)
			{
				ScreenSizeObserver.OnScreenSizeChangedDelegate value2 = onScreenSizeChanged;
				Delegate obj = Delegate.Remove(instance.OnScreenSizeChanged, value2);
				object obj3;
				nint num;
				if ((object)obj == null)
				{
					instance.OnScreenSizeChanged = null;
					flag = false;
				}
				else
				{
					bool flag2 = (object)obj.GetType() != typeof(ScreenSizeObserver.OnScreenSizeChangedDelegate);
					Delegate obj2 = null;
					if (!flag2)
					{
						obj2 = obj;
					}
					bool flag3 = (object)obj2 == null;
					typeFromHandle = (Delegate)(object)typeof(ScreenSizeObserver.OnScreenSizeChangedDelegate);
					obj3 = 0;
					num = unchecked((nint)null);
					if (flag3)
					{
						goto IL_03fd;
					}
					instance.OnScreenSizeChanged = (ScreenSizeObserver.OnScreenSizeChangedDelegate)obj2;
					bool flag4 = (object)obj.GetType() != typeof(ScreenSizeObserver.OnScreenSizeChangedDelegate);
					flag = false;
					if (!flag4)
					{
						flag = (byte)(int)obj != 0;
					}
					bool flag5 = !flag;
					obj3 = 0;
					num = unchecked((nint)null);
					obj4 = (Delegate)(object)typeof(ScreenSizeObserver.OnScreenSizeChangedDelegate);
					if (flag5)
					{
						goto IL_0408;
					}
				}
				ScreenSizeObserver instance2 = ScreenSizeObserver.Instance;
				bool flag6 = (object)instance2 == null;
				obj3 = 0;
				num = unchecked((nint)null);
				if (!flag6)
				{
					ScreenSizeObserver.OnScreenSizeChangedDelegate b = onScreenSizeChanged;
					obj5 = Delegate.Combine(instance2.OnScreenSizeChanged, b);
					if ((object)obj5 == null)
					{
						instance2.OnScreenSizeChanged = null;
					}
					else
					{
						bool flag7 = (object)obj5.GetType() != typeof(ScreenSizeObserver.OnScreenSizeChangedDelegate);
						Delegate obj6 = null;
						if (!flag7)
						{
							obj6 = obj5;
						}
						bool flag8 = (object)obj6 == null;
						flag = (byte)(int)typeof(ScreenSizeObserver.OnScreenSizeChangedDelegate) != 0;
						obj3 = 0;
						num = unchecked((nint)null);
						if (flag8)
						{
							goto IL_0420;
						}
						instance2.OnScreenSizeChanged = (ScreenSizeObserver.OnScreenSizeChangedDelegate)obj6;
						bool flag9 = (object)obj5.GetType() != typeof(ScreenSizeObserver.OnScreenSizeChangedDelegate);
						Delegate obj7 = null;
						if (!flag9)
						{
							obj7 = obj5;
						}
						bool flag10 = (object)obj7 == null;
						flag = (byte)(int)typeof(ScreenSizeObserver.OnScreenSizeChangedDelegate) != 0;
						obj3 = 0;
						num = unchecked((nint)null);
						ex = (NullReferenceException)(object)obj5;
						if (flag10)
						{
							goto IL_0438;
						}
					}
					Resolution currentResolution = ScreenOrchestrator.GetCurrentResolution();
					object obj8 = default(object);
					addOrRemoveCustomResolutionValue((Resolution)(&obj8));
					RefreshOptionLabels();
					if (!(_settings != null))
					{
						return;
					}
					bool flag11 = (object)_settings == null;
					flag = false;
					obj3 = 0;
					num = unchecked((nint)null);
					if (!flag11)
					{
						_settings.RefreshRegisteredResolversWithConnection(this);
						return;
					}
				}
			}
			ex = new NullReferenceException();
			goto IL_0438;
			IL_0420:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
			obj4 = obj5;
			goto IL_0408;
			IL_0408:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
			typeFromHandle = obj4;
			goto IL_03fd;
			IL_0438:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
			goto IL_0420;
			IL_03fd:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	protected bool isWindowed
	{
		get
		{
			//IL_0017: Expected O, but got I4
			FullScreenMode fullScreenMode = Screen.fullScreenMode;
			object obj = fullScreenMode - 3;
			return obj == null;
		}
	}

	public event Action OnMaxResolutionChanged
	{
		add
		{
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Expected O, but got Unknown
			object obj = this + 56;
			Delegate obj2 = this.m_OnMaxResolutionChanged;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Combine(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = null;
				if (!flag)
				{
					bool flag2 = (object)obj3.GetType() != typeof(Action);
					obj4 = null;
					if (!flag2)
					{
						obj4 = obj3;
					}
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag3 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag3)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		remove
		{
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Expected O, but got Unknown
			object obj = this + 56;
			Delegate obj2 = this.m_OnMaxResolutionChanged;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Remove(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = null;
				if (!flag)
				{
					bool flag2 = (object)obj3.GetType() != typeof(Action);
					obj4 = null;
					if (!flag2)
					{
						obj4 = obj3;
					}
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag3 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag3)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	private Resolution[] getResolutions()
	{
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Expected O, but got Unknown
		//IL_0063: Expected O, but got I4
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Expected O, but got Unknown
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Expected O, but got Unknown
		if (!CollectionExtensions.HasValuesThatAreNotNull(CustomResolutions))
		{
			return Screen.resolutions;
		}
		List<CustomResolution> customResolutions = CustomResolutions;
		Resolution[] array = new Resolution[customResolutions._size];
		object obj = array + 32;
		object obj2 = 0;
		object obj3 = default(object);
		while (true)
		{
			if ((nint)obj2 < customResolutions._size)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1805E11B0");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180623550");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180974FC0");
				if ((nint)obj2 >= array.Length)
				{
					break;
				}
				obj2++;
				obj = obj3;
				obj += 16;
				continue;
			}
			return array;
		}
		return (Resolution[])(object)new IndexOutOfRangeException();
	}

	protected Vector2Int getCurrentMaxResolution()
	{
		//IL_001f: Expected O, but got I4
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Expected O, but got Unknown
		//IL_0083: Expected O, but got I4
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Expected O, but got Unknown
		Resolution[] resolutions = getResolutions();
		object obj = resolutions.Length - 1;
		if ((nint)obj < resolutions.Length)
		{
			object obj2 = obj + 2;
			object obj3 = obj2 << 4;
			object obj4 = obj3 + (object)resolutions;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3E0");
			object obj5 = resolutions.Length - 1;
			if ((nint)obj5 < resolutions.Length)
			{
				object obj6 = obj5 + 2;
				object obj7 = obj6 << 4;
				object obj8 = obj7 + (object)resolutions;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9CF10");
				Vector2Int result = default(Vector2Int);
				return result;
			}
		}
		return (Vector2Int)new IndexOutOfRangeException();
	}

	protected unsafe virtual List<Resolution> getUniqueResolutions()
	{
		//IL_00c3: Expected O, but got I4
		//IL_0105: Expected O, but got I4
		//IL_014c: Expected O, but got I4
		//IL_0563: Expected O, but got I4
		//IL_04ff: Expected O, but got Ref
		//IL_02cd: Expected O, but got I4
		//IL_05a9: Expected O, but got I4
		//IL_05ef: Expected O, but got I4
		//IL_03fb: Expected O, but got I4
		//IL_063d: Expected O, but got I4
		//IL_0431: Expected O, but got Ref
		//IL_0450: Expected O, but got I4
		//IL_0458: Expected O, but got Ref
		if (_values != null)
		{
			List<Resolution> values = _values;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ rax_v46 (System.Collections.Generic.List`1<UnityEngine.Resolution>)+18]");
			if ((nint)0 != 0 && CacheResolutions)
			{
				goto IL_0504;
			}
		}
		List<Resolution> values2 = new List<Resolution>();
		_values = values2;
		Resolution[] resolutions = getResolutions();
		filterResolutionsAndAddToValues(resolutions, limitAspectRatios: true);
		List<Resolution> values3 = _values;
		bool flag = _values == null;
		int num = 0;
		bool flag2 = true;
		object obj = 0;
		Resolution[] array = resolutions;
		Delegate obj4;
		Delegate obj5 = default(Delegate);
		int width = default(int);
		Delegate typeFromHandle;
		NullReferenceException ex;
		if (!flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v236 @ rcx_v8 (System.Collections.Generic.List`1<UnityEngine.Resolution>)+18]");
			bool flag3 = (nint)0 != 0;
			flag2 = true;
			obj = 0;
			array = resolutions;
			if (!flag3)
			{
				Logger.LogWarning("Resolution aspect ratio limiting resulted in an empty list. Disabling filtering (all resolutions will be listed).");
				filterResolutionsAndAddToValues(resolutions, limitAspectRatios: false);
				flag2 = false;
				obj = 0;
				array = resolutions;
			}
			bool flag4 = !_addCustomResolutionOptionIfWindowed;
			num = 0;
			if (flag4)
			{
				goto IL_045d;
			}
			ScreenSizeObserver instance = ScreenSizeObserver.Instance;
			bool flag5 = (object)instance == null;
			num = 0;
			if (!flag5)
			{
				ScreenSizeObserver.OnScreenSizeChangedDelegate value = onScreenSizeChanged;
				Delegate obj2 = Delegate.Remove(instance.OnScreenSizeChanged, value);
				if ((object)obj2 == null)
				{
					instance.OnScreenSizeChanged = null;
					array = null;
				}
				else
				{
					bool flag6 = (object)obj2.GetType() != typeof(ScreenSizeObserver.OnScreenSizeChangedDelegate);
					Delegate obj3 = null;
					if (!flag6)
					{
						obj3 = obj2;
					}
					bool flag7 = (object)obj3 == null;
					num = 0;
					flag2 = false;
					obj = 0;
					typeFromHandle = (Delegate)(object)typeof(ScreenSizeObserver.OnScreenSizeChangedDelegate);
					if (flag7)
					{
						goto IL_066f;
					}
					instance.OnScreenSizeChanged = (ScreenSizeObserver.OnScreenSizeChangedDelegate)obj3;
					bool flag8 = (object)obj2.GetType() != typeof(ScreenSizeObserver.OnScreenSizeChangedDelegate);
					array = null;
					if (!flag8)
					{
						array = (Resolution[])(object)obj2;
					}
					bool flag9 = array == null;
					num = 0;
					flag2 = false;
					obj = 0;
					obj4 = (Delegate)(object)typeof(ScreenSizeObserver.OnScreenSizeChangedDelegate);
					if (flag9)
					{
						goto IL_067f;
					}
				}
				ScreenSizeObserver instance2 = ScreenSizeObserver.Instance;
				bool flag10 = (object)instance2 == null;
				num = 0;
				flag2 = false;
				obj = 0;
				if (!flag10)
				{
					ScreenSizeObserver.OnScreenSizeChangedDelegate b = onScreenSizeChanged;
					obj5 = Delegate.Combine(instance2.OnScreenSizeChanged, b);
					Resolution[] array2;
					if ((object)obj5 == null)
					{
						instance2.OnScreenSizeChanged = null;
						array2 = null;
					}
					else
					{
						bool flag11 = (object)obj5.GetType() != typeof(ScreenSizeObserver.OnScreenSizeChangedDelegate);
						Delegate obj6 = null;
						if (!flag11)
						{
							obj6 = obj5;
						}
						bool flag12 = (object)obj6 == null;
						num = 0;
						flag2 = false;
						obj = 0;
						array = (Resolution[])(object)typeof(ScreenSizeObserver.OnScreenSizeChangedDelegate);
						if (flag12)
						{
							goto IL_0697;
						}
						instance2.OnScreenSizeChanged = (ScreenSizeObserver.OnScreenSizeChangedDelegate)obj6;
						bool flag13 = (object)obj5.GetType() != typeof(ScreenSizeObserver.OnScreenSizeChangedDelegate);
						Delegate obj7 = null;
						if (!flag13)
						{
							obj7 = obj5;
						}
						bool flag14 = (object)obj7 == null;
						array2 = (Resolution[])(object)obj7;
						num = 0;
						flag2 = false;
						obj = 0;
						array = (Resolution[])(object)typeof(ScreenSizeObserver.OnScreenSizeChangedDelegate);
						ex = (NullReferenceException)(object)obj5;
						if (flag14)
						{
							goto IL_06af;
						}
					}
					FullScreenMode fullScreenMode = Screen.fullScreenMode;
					bool flag15 = fullScreenMode != FullScreenMode.Windowed;
					num = 0;
					flag2 = false;
					obj = 0;
					array = array2;
					if (!flag15)
					{
						Resolution currentResolution = ScreenOrchestrator.GetCurrentResolution();
						num = currentResolution.m_Width;
						addOrRemoveCustomResolutionValue((Resolution)(&width));
						width = currentResolution.m_Width;
						flag2 = false;
						obj = 0;
						array = (Resolution[])(&width);
					}
					goto IL_045d;
				}
			}
		}
		goto IL_050b;
		IL_067f:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		typeFromHandle = obj4;
		goto IL_066f;
		IL_06af:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		goto IL_0697;
		IL_0697:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		obj4 = obj5;
		goto IL_067f;
		IL_0661:
		return (List<Resolution>)(object)new NullReferenceException();
		IL_066f:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		goto IL_0661;
		IL_045d:
		List<Resolution> values4 = _values;
		if (_values != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v127 @ rax_v24 (System.Collections.Generic.List`1<UnityEngine.Resolution>)+18]");
			if ((nint)0 == 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1805E11B0");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180623550");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180974FC0");
				if (_values == null)
				{
					goto IL_0661;
				}
				_values.Add((Resolution)(&width));
			}
			goto IL_0504;
		}
		goto IL_050b;
		IL_050b:
		ex = new NullReferenceException();
		goto IL_06af;
		IL_0504:
		return _values;
	}

	private unsafe void filterResolutionsAndAddToValues(Resolution[] resolutions, bool limitAspectRatios)
	{
		//IL_00de: Expected O, but got I4
		//IL_00e7: Expected O, but got I4
		//IL_00f0: Expected O, but got I4
		//IL_0101: Expected O, but got I4
		//IL_010a: Expected O, but got I4
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Expected O, but got Unknown
		//IL_0a5c: Expected O, but got I4
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Expected O, but got Unknown
		//IL_016c: Expected O, but got I
		//IL_0a0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a10: Expected O, but got Unknown
		//IL_0623: Expected O, but got Ref
		//IL_0634: Expected I, but got O
		//IL_0649: Expected I, but got O
		//IL_0db2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0db7: Expected O, but got Unknown
		//IL_05b2: Expected I4, but got O
		//IL_05bb: Expected O, but got I4
		//IL_05d0: Expected I4, but got O
		//IL_05d9: Expected O, but got I4
		//IL_02e2: Expected O, but got I4
		//IL_02f2: Expected O, but got I
		//IL_0e5a: Expected I, but got O
		//IL_09b7: Expected O, but got Ref
		//IL_0b0b: Unsupported input type for neg.
		//IL_0b0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b10: Expected O, but got Unknown
		//IL_0b59: Expected O, but got I4
		//IL_033f: Expected I4, but got O
		//IL_033f: Expected O, but got Ref
		//IL_037a: Expected O, but got I
		//IL_038a: Expected I4, but got O
		//IL_03a3: Expected O, but got I4
		//IL_03ac: Expected O, but got I4
		//IL_03bd: Expected O, but got I4
		//IL_03cd: Expected O, but got I
		//IL_03ed: Expected O, but got I
		//IL_03fd: Expected I4, but got O
		//IL_0416: Expected O, but got I4
		//IL_041f: Expected O, but got I4
		//IL_0430: Expected O, but got I4
		//IL_0440: Expected O, but got I
		//IL_0834: Expected I4, but got O
		//IL_0883: Unknown result type (might be due to invalid IL or missing references)
		//IL_0888: Expected O, but got Unknown
		//IL_0898: Unknown result type (might be due to invalid IL or missing references)
		//IL_089d: Expected O, but got Unknown
		//IL_08a7: Invalid comparison between F4 and O
		//IL_08f9: Expected O, but got Ref
		bool flag = !LimitMaxResolutionToDisplayResolution;
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		Display[] array = default(Display[]);
		int num4 = default(int);
		if (!flag)
		{
			array = Display.displays;
			object obj = Display.displays + 32;
			num = 0;
			num4 = 0;
			num2 = 0;
			num3 = 0;
			int num5 = 0;
			while (num5 < array.Length)
			{
				if (num4 < array.Length)
				{
					int systemWidth = ((Display)obj).systemWidth;
					if (num3 <= systemWidth)
					{
						num3 = systemWidth;
					}
					int systemHeight = ((Display)obj).systemHeight;
					if (num2 <= systemHeight)
					{
						num2 = systemHeight;
					}
					num4++;
					obj += 8;
					num = num2;
					num5 = num4;
					continue;
				}
				goto IL_0d12;
			}
		}
		List<Vector2Int>.Enumerator enumerator = (List<Vector2Int>.Enumerator)0;
		object obj2 = 0;
		object obj3 = 0;
		bool flag2 = limitAspectRatios;
		object obj4 = 0;
		object obj5 = 0;
		object obj8 = default(object);
		object obj9 = default(object);
		nint num8 = default(nint);
		object obj12 = default(object);
		object obj14 = default(object);
		object obj16 = default(object);
		int num11 = default(int);
		object obj18 = default(object);
		List<Vector2Int>.Enumerator enumerator2 = default(List<Vector2Int>.Enumerator);
		int num12 = default(int);
		int refreshRate = default(int);
		object obj19 = default(object);
		object obj20 = default(object);
		object obj21 = default(object);
		List<Resolution>.Enumerator enumerator4 = default(List<Resolution>.Enumerator);
		Display[] array2 = default(Display[]);
		object obj24 = default(object);
		Display[] array3 = default(Display[]);
		object obj25 = default(object);
		object obj26 = default(object);
		object obj27 = default(object);
		object obj28 = default(object);
		object obj29 = default(object);
		object obj30 = default(object);
		object obj31 = default(object);
		object obj32 = default(object);
		object obj33 = default(object);
		object obj35 = default(object);
		int num17 = default(int);
		while (true)
		{
			object obj10;
			object obj11;
			object obj13;
			object obj15;
			int num9;
			object obj17;
			int num10;
			bool flag8;
			nint num14;
			if ((nint)obj5 < resolutions.Length)
			{
				if ((nint)obj5 >= resolutions.Length)
				{
					break;
				}
				object obj6 = obj5 + 2;
				object obj7 = obj6 + obj6;
				bool flag3 = !SkipResolutinsSmallerThanHD;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [resolutions @ rdx (UnityEngine.Resolution[])+v687 @ rax_v58*8]");
				Resolution resolution = (Resolution)0;
				if (!flag3)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3E0");
					if ((nint)obj8 < 1280)
					{
						goto IL_0a02;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9CF10");
					if ((nint)obj9 < 720)
					{
						goto IL_0a02;
					}
				}
				bool flag4 = !SkipRefreshRatesWith59Hz;
				obj10 = obj2;
				int num6 = (flag2 ? 1 : 0);
				int num7 = (int)num8;
				obj11 = obj12;
				obj13 = obj14;
				obj15 = obj16;
				num9 = num4;
				obj17 = obj4;
				num10 = num11;
				if (!flag4)
				{
					if (LimitToCurrentRefreshRate)
					{
						goto IL_04be;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E583B0");
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm1,rcx\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm0,rax\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm1,xmm0\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
					bool flag5 = (nint)obj18 != 59;
					obj10 = obj2;
					num6 = (flag2 ? 1 : 0);
					num7 = (int)num8;
					obj11 = obj12;
					obj13 = obj14;
					obj15 = obj16;
					num9 = num4;
					obj17 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [resolutions @ rdx (UnityEngine.Resolution[])+v687 @ rax_v58*8]");
					Resolution resolution2 = (Resolution)0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [resolutions @ rdx (UnityEngine.Resolution[])+v687 @ rax_v58*8]");
					num10 = 0;
					if (!flag5)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3E0");
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9CF10");
						Resolution? resolution3 = ((ResolutionConnection)(&enumerator2)).findResolution((IList<Resolution>)this, (int)resolutions, num12, refreshRate);
						nint num13 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
						bool flag6 = obj19 != null;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1523 @ rax_v99 (System.Nullable`1<UnityEngine.Resolution>)+10]");
						obj10 = 0;
						obj3 = resolution3;
						num6 = (int)resolutions;
						num7 = num12;
						obj11 = obj20;
						obj13 = 60;
						obj15 = 0;
						num9 = num12;
						obj17 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [resolutions @ rdx (UnityEngine.Resolution[])+v687 @ rax_v58*8]");
						resolution2 = (Resolution)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [resolutions @ rdx (UnityEngine.Resolution[])+v687 @ rax_v58*8]");
						num10 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1523 @ rax_v99 (System.Nullable`1<UnityEngine.Resolution>)+10]");
						obj2 = 0;
						obj3 = resolution3;
						flag2 = (byte)(int)resolutions != 0;
						num8 = num12;
						obj12 = obj20;
						obj14 = 60;
						obj16 = 0;
						num4 = num12;
						obj4 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [resolutions @ rdx (UnityEngine.Resolution[])+v687 @ rax_v58*8]");
						resolution2 = (Resolution)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [resolutions @ rdx (UnityEngine.Resolution[])+v687 @ rax_v58*8]");
						num11 = 0;
						if (flag6)
						{
							goto IL_0a02;
						}
					}
				}
				bool flag7 = !LimitToCurrentRefreshRate;
				obj2 = obj10;
				flag2 = (byte)num6 != 0;
				num8 = num7;
				obj12 = obj11;
				obj14 = obj13;
				obj16 = obj15;
				flag8 = (byte)num6 != 0;
				num14 = num7;
				if (!flag7)
				{
					goto IL_04be;
				}
				goto IL_05f4;
			}
			if (!LimitToUniqueResolutions)
			{
				return;
			}
			List<Resolution> values = _values;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v447 @ r12_v6 (System.Collections.Generic.List`1<UnityEngine.Resolution>)+18]");
			int num15 = 0;
			List<Vector2Int>.Enumerator enumerator3 = (List<Vector2Int>.Enumerator)0;
			int width;
			while (true)
			{
				num15--;
				if (num15 < 0)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				Resolution currentResolution = Screen.currentResolution;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E583B0");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm1,rcx\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm0,rax\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm1,xmm0\"");
				Resolution resolution4 = ((List<Resolution>)null).get_Item((int)(&obj21));
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E583B0");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm1,rcx\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm0,rax\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm1,xmm0\"");
				Resolution resolution5 = ((List<Resolution>)null).get_Item((int)(&obj21));
				nint num16 = (nint)typeof(Math);
				object obj22 = (object)resolution4 - (object)resolution5;
				object obj23 = 0 - obj22;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1582 @ rcx_v19 (Il2CppClass<System.Math>)+E4]");
				if ((nint)0 < (nint)0)
				{
					obj23 = obj22;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
				Resolution resolution6 = (Resolution)2147483647;
				while (true)
				{
					Display[] array4;
					if (enumerator4.MoveNext())
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3E0");
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3E0");
						bool flag9 = array2 != obj24;
						array = array2;
						if (flag9)
						{
							continue;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9CF10");
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9CF10");
						bool flag10 = array3 != obj25;
						array = array3;
						if (flag10)
						{
							continue;
						}
						Resolution currentResolution2 = Screen.currentResolution;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E583B0");
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm1,rcx\"");
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm0,rax\"");
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm1,xmm0\"");
						Resolution resolution7 = ((List<Resolution>)null).get_Item(0);
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E583B0");
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm1,rcx\"");
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm0,rax\"");
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm1,xmm0\"");
						Resolution resolution8 = ((List<Resolution>)null).get_Item(0);
						Resolution resolution9 = ((List<Resolution>)null).get_Item(0);
						bool flag11 = System.Runtime.CompilerServices.Unsafe.As<Resolution, UIntPtr>(ref resolution9) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj23);
						array = array3;
						width = currentResolution2.m_Width;
						Resolution resolution2 = (Resolution)enumerator3;
						resolution6 = resolution9;
						if (flag11)
						{
							continue;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
						array4 = array3;
						width = currentResolution2.m_Width;
						resolution2 = (Resolution)enumerator3;
					}
					else
					{
						enumerator4.Dispose();
						bool flag12 = System.Runtime.CompilerServices.Unsafe.As<Resolution, UIntPtr>(ref resolution6) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj23);
						array4 = array;
						if (flag12)
						{
							break;
						}
					}
					_values.RemoveAt(num15);
					array = array4;
					break;
				}
			}
			return;
			IL_04be:
			Resolution currentResolution3 = Screen.currentResolution;
			num10 = currentResolution3.m_Width;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E583B0");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm1,rcx\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm0,rax\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm1,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E583B0");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm1,rcx\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm0,rax\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm1,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
			ResolutionConnection resolutionConnection = (ResolutionConnection)(obj26 - obj27);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180439880");
			bool flag13 = (nint)obj28 > 1;
			width = currentResolution3.m_Width;
			obj10 = obj2;
			flag8 = flag2;
			num14 = num8;
			obj11 = obj12;
			obj13 = obj14;
			obj15 = obj16;
			num9 = (int)resolutionConnection;
			obj17 = 0;
			width = currentResolution3.m_Width;
			num4 = (int)resolutionConnection;
			obj4 = 0;
			num11 = currentResolution3.m_Width;
			if (flag13)
			{
				goto IL_0a02;
			}
			goto IL_05f4;
			IL_05f4:
			if (LimitToUniqueResolutions)
			{
				bool flag14 = contains(_values, (Resolution)(&enumerator2));
				flag8 = (byte)(&enumerator2) != 0;
				num14 = unchecked((nint)null);
				obj2 = obj10;
				flag2 = (byte)(&enumerator2) != 0;
				num8 = unchecked((nint)null);
				obj12 = obj11;
				obj14 = obj13;
				obj16 = obj15;
				num4 = num9;
				obj4 = obj17;
				num11 = num10;
				if (flag14)
				{
					goto IL_0a02;
				}
			}
			if (LimitMaxResolutionToDisplayResolution && num3 > 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3E0");
				bool flag15 = (nint)obj29 > num3;
				obj2 = obj10;
				flag2 = flag8;
				num8 = num14;
				obj12 = obj11;
				obj14 = obj13;
				obj16 = obj15;
				num4 = num9;
				obj4 = obj17;
				num11 = num10;
				if (flag15)
				{
					goto IL_0a02;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9CF10");
				bool flag16 = (nint)obj30 > num2;
				obj2 = obj10;
				flag2 = flag8;
				num8 = num14;
				obj12 = obj11;
				obj14 = obj13;
				obj16 = obj15;
				num4 = num9;
				obj4 = obj17;
				num11 = num10;
				if (flag16)
				{
					goto IL_0a02;
				}
			}
			List<Resolution> values2;
			if (limitAspectRatios && AllowedAspectRatios != null)
			{
				List<Vector2Int> allowedAspectRatios = AllowedAspectRatios;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1440 @ rax_v66 (System.Collections.Generic.List`1<UnityEngine.Vector2Int>)+18]");
				if ((nint)0 > (nint)0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3E0");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9CF10");
					num11 = obj31 / obj32;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
					flag2 = false;
					obj4 = obj33;
					while (true)
					{
						if (enumerator.MoveNext())
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
							object obj34 = obj35 >> 32;
							object obj36 = obj35 / obj34;
							object obj37 = num11 - obj36;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
							obj4 = obj37 & 0;
							float allowedAspectRatioDelta = AllowedAspectRatioDelta;
							bool flag17 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)allowedAspectRatioDelta) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4);
							flag2 = false;
							if (!flag17)
							{
								values2 = _values;
								if (_values != null)
								{
									_values.Add((Resolution)(&num17));
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
									num17 = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref resolutions[obj5]);
									enumerator = enumerator2;
									obj2 = obj10;
									flag2 = false;
									num8 = num14;
									obj12 = obj11;
									obj14 = obj13;
									obj16 = obj15;
									num4 = (int)(&enumerator);
									break;
								}
								throw new NullReferenceException();
							}
							continue;
						}
						enumerator.Dispose();
						enumerator = enumerator2;
						obj2 = obj10;
						num8 = num14;
						obj12 = obj11;
						obj14 = obj13;
						obj16 = obj15;
						num4 = (int)(&enumerator);
						break;
					}
					goto IL_0a02;
				}
			}
			bool flag18 = _values == null;
			values2 = _values;
			if (!flag18)
			{
				_values.Add((Resolution)(&obj21));
				obj2 = obj10;
				flag2 = false;
				num8 = num14;
				obj12 = obj11;
				obj14 = obj13;
				obj16 = obj15;
				num4 = num9;
				obj4 = obj17;
				num11 = num10;
				goto IL_0a02;
			}
			throw new NullReferenceException();
			IL_0a02:
			obj5++;
		}
		goto IL_0d12;
		IL_0d12:
		throw new IndexOutOfRangeException();
	}

	protected unsafe Resolution? findResolution(IList<Resolution> resolutions, int width, int height, int refreshRate)
	{
		//IL_0028: Expected O, but got Ref
		//IL_0031: Expected O, but got I4
		//IL_0390: Expected O, but got I4
		//IL_00cc: Expected O, but got I
		//IL_00d5: Expected O, but got I4
		//IL_017e: Expected O, but got I4
		//IL_0187: Expected O, but got I4
		//IL_0195: Expected I4, but got O
		//IL_019d: Expected O, but got Ref
		//IL_02b2: Expected O, but got I
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Expected O, but got Unknown
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Expected O, but got Unknown
		//IL_01cf: Expected O, but got I4
		//IL_01d8: Expected O, but got I4
		//IL_01e6: Expected I4, but got O
		//IL_01ee: Expected O, but got Ref
		//IL_0216: Expected O, but got I4
		//IL_021f: Expected O, but got I4
		//IL_022d: Expected I4, but got O
		//IL_0235: Expected O, but got Ref
		//IL_0250: Expected O, but got Ref
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
		object obj2 = default(object);
		object obj = (object)(&obj2);
		object obj3 = 0;
		int num = height;
		ResolutionConnection resolutionConnection = null;
		object obj4 = default(object);
		object obj14 = default(object);
		object obj15 = default(object);
		object obj17 = default(object);
		object obj18 = default(object);
		object obj19 = default(object);
		object obj20 = default(object);
		object obj21 = default(object);
		object obj22 = default(object);
		while (true)
		{
			object obj13;
			if (obj2 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				if (obj4 != null)
				{
					bool flag = obj2 == null;
					resolutionConnection = null;
					if (!flag)
					{
						object obj5 = obj2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v140 @ r10_v5+12E]");
						if ((nint)0 >= (nint)0)
						{
							goto IL_010c;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v140 @ r10_v5+B0]");
						object obj6 = 0;
						object obj7 = 0;
						while (true)
						{
							object obj8 = obj7 + obj7;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ r8_v17+v381 @ rax_v35*8]");
							if (0 == (nint)typeof(IEnumerator<Resolution>))
							{
								break;
							}
							obj7++;
							object obj9 = obj7;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v140 @ r10_v5+12E]");
							if ((nint)obj9 < 0)
							{
								continue;
							}
							goto IL_010c;
						}
						object obj10 = obj7 + obj7;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ r8_v17+8+v443 @ rcx_v25*8]");
						object obj11 = (nint)0 << 4;
						object obj12 = obj11 + 312;
						obj13 = obj12 + obj5;
						goto IL_0377;
					}
					throw new NullReferenceException();
				}
				if (obj != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				}
				ResolutionConnection resolutionConnection2 = (ResolutionConnection)0;
				_ = 0;
				break;
			}
			throw new NullReferenceException();
			IL_010c:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			obj13 = obj14;
			goto IL_0377;
			IL_0377:
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v449 @ r8_v9] (should have been resolved before IL gen)");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E583B0");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm1,rcx\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm0,rax\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm1,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3E0");
			bool flag2 = (nint)obj15 != height;
			obj3 = 0;
			object obj16 = 0;
			num = (int)typeof(IEnumerator<Resolution>);
			resolutionConnection = (ResolutionConnection)(&obj17);
			if (flag2)
			{
				continue;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9CF10");
			bool flag3 = obj18 != obj19;
			obj3 = 0;
			obj16 = 0;
			num = (int)typeof(IEnumerator<Resolution>);
			resolutionConnection = (ResolutionConnection)(&obj17);
			if (flag3)
			{
				continue;
			}
			bool flag4 = obj20 != obj21;
			obj3 = 0;
			obj16 = 0;
			num = (int)typeof(IEnumerator<Resolution>);
			resolutionConnection = (ResolutionConnection)(&obj17);
			if (!flag4)
			{
				Resolution? resolution = (Resolution)(&obj22);
				if (obj != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				}
				ResolutionConnection resolutionConnection2 = (ResolutionConnection)resolution;
				_ = 0;
				break;
			}
		}
		return (Resolution?)this;
	}

	public void ClearResolutionCache()
	{
		CacheResolutions = false;
		List<Resolution> uniqueResolutions = getUniqueResolutions();
		CacheResolutions = CacheResolutions;
	}

	public int FindClosestResolutionIndex(int width, int height, int refreshRate)
	{
		IList<Resolution> uniqueResolutions = getUniqueResolutions();
		int refreshRate2 = default(int);
		return findClosestResolutionIndex(uniqueResolutions, width, height, refreshRate2);
	}

	protected int findClosestResolutionIndex(IList<Resolution> resolutions, int width, int height, int refreshRate)
	{
		//IL_05b3: Expected I4, but got O
		//IL_002a: Expected O, but got I4
		//IL_0072: Expected O, but got I4
		//IL_02f4: Expected O, but got I4
		//IL_06ce: Expected I, but got O
		//IL_0098: Expected O, but got I
		//IL_032d: Expected I, but got O
		//IL_05dd: Expected O, but got I4
		//IL_0272: Expected O, but got I4
		//IL_0288: Expected O, but got I
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Expected O, but got Unknown
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Expected O, but got Unknown
		//IL_0364: Expected O, but got I
		//IL_0666: Expected O, but got I4
		//IL_0702: Expected O, but got I4
		//IL_068f: Expected O, but got I4
		//IL_078b: Expected O, but got I4
		//IL_0524: Expected O, but got I4
		//IL_053a: Expected O, but got I
		//IL_0543: Unknown result type (might be due to invalid IL or missing references)
		//IL_0548: Expected O, but got Unknown
		//IL_0550: Unknown result type (might be due to invalid IL or missing references)
		//IL_0555: Expected O, but got Unknown
		//IL_07ba: Expected I, but got O
		//IL_0468: Unsupported input type for neg.
		//IL_0468: Unknown result type (might be due to invalid IL or missing references)
		//IL_046d: Expected O, but got Unknown
		//IL_07cc: Expected O, but got I4
		if (resolutions != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			object obj = default(object);
			bool flag = (nint)obj <= 0;
			object obj2 = 2147483647;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			int num7 = 0;
			object obj3 = 2147483647;
			int num8 = width;
			object obj13 = default(object);
			if (!flag)
			{
				object obj10 = default(object);
				object obj11 = default(object);
				object obj12 = default(object);
				int num13 = default(int);
				int num15 = default(int);
				object obj15 = default(object);
				object obj16 = default(object);
				object obj19 = default(object);
				while (true)
				{
					nint num9 = (nint)resolutions;
					int num10 = num4;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v180 @ r10_v3 (Il2CppClass<System.Collections.Generic.IList`1<UnityEngine.Resolution>>)+12E]");
					if ((nint)num10 >= (nint)0)
					{
						goto IL_00d7;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v180 @ r10_v3 (Il2CppClass<System.Collections.Generic.IList`1<UnityEngine.Resolution>>)+B0]");
					object obj4 = 0;
					int num11 = num4;
					while (true)
					{
						object obj5 = num11 + num11;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ r8_v9+v357 @ rax_v27*8]");
						if (0 == (nint)typeof(IList<Resolution>))
						{
							break;
						}
						num11++;
						int num12 = num11;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v180 @ r10_v3 (Il2CppClass<System.Collections.Generic.IList`1<UnityEngine.Resolution>>)+12E]");
						if ((nint)num12 < (nint)0)
						{
							continue;
						}
						goto IL_00d7;
					}
					object obj6 = num11 + num11;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ r8_v9+8+v447 @ rcx_v23*8]");
					object obj7 = (nint)0 << 4;
					object obj8 = obj7 + 312;
					object obj9 = obj8 + num9;
					goto IL_00e6;
					IL_00e6:
					Resolution resolution = resolutions.get_Item(num3);
					num2 = resolution.m_Width;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3E0");
					bool flag2 = (nint)obj10 != num8;
					num6 = resolution.m_Width;
					if (!flag2)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9CF10");
						if ((nint)obj11 == height)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E583B0");
							Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm1,rcx\"");
							Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm0,rax\"");
							Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm1,xmm0\"");
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
							bool flag3 = obj12 == obj13;
							num5 = 0;
							int width2 = resolution.m_Width;
							num2 = 0;
							if (flag3)
							{
								return num3;
							}
						}
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3E0");
					bool flag4 = num8 >= num13;
					int num14 = num13;
					if (!flag4)
					{
						num14 = num8;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9CF10");
					bool flag5 = height >= num15;
					int num16 = num15;
					if (!flag5)
					{
						num16 = height;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3E0");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9CF10");
					object obj14 = obj15 * obj16;
					object obj17 = width * height;
					if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj17) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj14))
					{
						obj17 = obj14;
					}
					object obj18 = num16 * num14;
					obj2 = obj17 - obj18;
					num3++;
					if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3))
					{
						obj2 = obj3;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					bool flag6 = num3 >= (nint)obj19;
					num = num5;
					if (flag6)
					{
						break;
					}
					num4 = 0;
					num7 = num2;
					obj3 = obj2;
					num8 = width;
					continue;
					IL_00d7:
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
					goto IL_00e6;
				}
			}
			int num17 = 0;
			int num18 = 0;
			int num19 = 0;
			int result = 0;
			int num20 = 0;
			int num21 = 0;
			object obj20 = 10000;
			int num26 = default(int);
			int num28 = default(int);
			object obj28 = default(object);
			object obj29 = default(object);
			object obj32 = default(object);
			while (true)
			{
				int count = resolutions.Count;
				if (num21 >= count)
				{
					break;
				}
				nint num22 = (nint)resolutions;
				int num23 = num19;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v546 @ r10_v8 (Il2CppClass<System.Collections.Generic.IList`1<UnityEngine.Resolution>>)+12E]");
				if ((nint)num23 >= (nint)0)
				{
					goto IL_03a3;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v546 @ r10_v8 (Il2CppClass<System.Collections.Generic.IList`1<UnityEngine.Resolution>>)+B0]");
				object obj21 = 0;
				int num24 = num19;
				while (true)
				{
					object obj22 = num24 + num24;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v612 @ r8_v18+v615 @ rax_v61*8]");
					if (0 == (nint)typeof(IList<Resolution>))
					{
						break;
					}
					num24++;
					int num25 = num24;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v546 @ r10_v8 (Il2CppClass<System.Collections.Generic.IList`1<UnityEngine.Resolution>>)+12E]");
					if ((nint)num25 < (nint)0)
					{
						continue;
					}
					goto IL_03a3;
				}
				object obj23 = num24 + num24;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v612 @ r8_v18+8+v694 @ rcx_v47*8]");
				object obj24 = (nint)0 << 4;
				object obj25 = obj24 + 312;
				object obj26 = obj25 + num22;
				goto IL_03b2;
				IL_03a3:
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
				goto IL_03b2;
				IL_03b2:
				Resolution resolution2 = resolutions.get_Item(num20);
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3E0");
				bool flag7 = width >= num26;
				int num27 = num26;
				if (!flag7)
				{
					num27 = width;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9CF10");
				bool flag8 = height >= num28;
				int num29 = num28;
				if (!flag8)
				{
					num29 = height;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3E0");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9CF10");
				object obj27 = obj28 * obj29;
				object obj30 = width * height;
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj30) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj27))
				{
					obj30 = obj27;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E583B0");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm1,rcx\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm0,rax\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm1,xmm0\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
				object obj31 = obj32 - obj13;
				nint num30 = (nint)typeof(Math);
				object obj33 = 0 - obj31;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v300 @ rcx_v41 (Il2CppClass<System.Math>)+E4]");
				if ((nint)0 < (nint)0)
				{
					obj33 = obj31;
				}
				object obj34 = num29 * num27;
				object obj35 = obj30 - obj34;
				if (obj35 == obj2 && System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj33) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj20))
				{
					int num31 = num20 + 1;
					num18 = num20;
					num19 = 0;
					result = num20;
					num20 = num31;
					num = 0;
					num2 = 0;
					num21 = num31;
					obj20 = obj33;
				}
				else
				{
					num20++;
					num19 = 0;
					result = num18;
					num = 0;
					num2 = 0;
					num21 = num20;
				}
			}
			return result;
		}
		NullReferenceException ex = new NullReferenceException();
		return (int)ex;
	}

	public unsafe static int GetRoundedRefreshRate(Resolution res)
	{
		RefreshRate refreshRateRatio = ((Resolution*)res)->refreshRateRatio;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm1,rcx\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm0,rax\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm1,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
		int result = default(int);
		return result;
	}

	protected unsafe bool contains(List<Resolution> resolutions, Resolution resolution)
	{
		//IL_0033: Expected O, but got I4
		//IL_003c: Expected O, but got I4
		//IL_0045: Expected O, but got I4
		//IL_0232: Expected I, but got O
		//IL_00e5: Unsupported input type for neg.
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Expected O, but got Unknown
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Expected O, but got Unknown
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Expected O, but got Unknown
		if (resolutions != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [resolutions @ rdx (System.Collections.Generic.List`1<UnityEngine.Resolution>)+18]");
			if ((nint)0 != 0)
			{
				object obj = 0;
				object obj2 = 0;
				object obj3 = 0;
				object obj5 = default(object);
				object obj8 = default(object);
				object obj9 = default(object);
				object obj11 = default(object);
				object obj12 = default(object);
				while (true)
				{
					object obj4 = obj3;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [resolutions @ rdx (System.Collections.Generic.List`1<UnityEngine.Resolution>)+18]");
					if ((nint)obj4 >= 0)
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E583B0");
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm1,rcx\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm0,rax\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm1,xmm0\"");
					Resolution resolution2 = ((List<Resolution>)null).get_Item((int)(&obj5));
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E583B0");
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm1,rcx\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm0,rax\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm1,xmm0\"");
					Resolution resolution3 = ((List<Resolution>)null).get_Item((int)(&obj5));
					object obj6 = (object)resolution2 - (object)resolution3;
					nint num = (nint)typeof(Math);
					object obj7 = 0 - obj6;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v271 @ rcx_v12 (Il2CppClass<System.Math>)+E4]");
					if ((nint)0 < (nint)0)
					{
						obj7 = obj6;
					}
					int width = ((Resolution*)resolution)->width;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3E0");
					bool flag = width != (nint)obj8;
					obj = obj9;
					if (!flag)
					{
						int height = ((Resolution*)resolution)->height;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9CF10");
						object obj10 = height - obj11;
						bool flag2 = obj10 == null;
						bool flag3 = (nint)obj7 > 1;
						bool flag4 = false;
						if (!flag3)
						{
							flag4 = flag2;
						}
						obj = obj12;
						if (flag4)
						{
							return true;
						}
					}
					obj2++;
					int width2 = resolution.m_Width;
					object obj13 = obj5;
					obj3 = obj2;
				}
			}
		}
		return false;
	}

	public override List<string> GetOptionLabels()
	{
		//IL_0015: Expected O, but got I4
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Expected O, but got Unknown
		//IL_0079: Expected O, but got I4
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Expected O, but got Unknown
		//IL_0239: Expected O, but got I4
		//IL_02d4: Expected I, but got O
		//IL_0366: Expected O, but got I4
		//IL_036f: Expected O, but got I4
		Resolution[] resolutions = getResolutions();
		if (resolutions != null)
		{
			object obj = resolutions.Length - 1;
			if ((nint)obj < resolutions.Length)
			{
				object obj2 = obj + 2;
				object obj3 = obj2 << 4;
				object obj4 = obj3 + (object)resolutions;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3E0");
				object obj5 = resolutions.Length - 1;
				if ((nint)obj5 < resolutions.Length)
				{
					object obj6 = obj5 + 2;
					object obj7 = obj6 << 4;
					object obj8 = obj7 + (object)resolutions;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9CF10");
					Vector2Int vector2Int = default(Vector2Int);
					bool flag;
					if ((object)vector2Int != (object)_lastMonitorMaxResolution)
					{
						flag = true;
					}
					else
					{
						object obj9 = (object)vector2Int >> 32;
						object obj10 = (object)_lastMonitorMaxResolution >> 32;
						object obj11 = obj9 - obj10;
						bool flag2 = obj11 == null;
						flag = !flag2;
					}
					if (flag)
					{
						_lastMonitorMaxResolution = vector2Int;
						_values = null;
						_labels = null;
						Action onMaxResolutionChanged = this.m_OnMaxResolutionChanged;
						if (this.m_OnMaxResolutionChanged != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v385.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
						}
					}
					if (_labels != null)
					{
						List<string> labels = _labels;
						if (labels._size != 0 && CacheResolutions)
						{
							goto IL_0417;
						}
					}
					List<string> labels2 = new List<string>();
					_labels = labels2;
					List<Resolution> uniqueResolutions = getUniqueResolutions();
					if (uniqueResolutions == null)
					{
						goto IL_039f;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
					List<Resolution>.Enumerator enumerator = (List<Resolution>.Enumerator)0;
					object obj13 = default(object);
					object obj12 = obj13;
					List<Resolution>.Enumerator enumerator2 = default(List<Resolution>.Enumerator);
					object arg = default(object);
					object arg2 = default(object);
					object arg3 = default(object);
					while (enumerator2.MoveNext())
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3E0");
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9CF10");
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
						string text = string.Format(_resolutionFormat, arg, arg2);
						bool flag3 = !AddRefreshRateToLabels;
						string item = text;
						if (!flag3)
						{
							nint num = (nint)typeof(ResolutionConnection);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v562 @ rcx_v38 (Il2CppClass<Kamgam.SettingsGenerator.ResolutionConnection>)+E4]");
							bool flag4 = (nint)0 != 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E583B0");
							Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm1,rcx\"");
							Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm0,rax\"");
							Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm1,xmm0\"");
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
							Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
							string text2 = string.Format(_refreshRateFormat, arg3);
							string text3 = text + text2;
							obj12 = 0;
							List<Resolution>.Enumerator enumerator3 = (List<Resolution>.Enumerator)0;
							item = text3;
						}
						if (_labels != null)
						{
							_labels.Add(item);
							continue;
						}
						throw new NullReferenceException();
					}
					enumerator2.Dispose();
					goto IL_0417;
				}
			}
			return (List<string>)(object)new IndexOutOfRangeException();
		}
		goto IL_039f;
		IL_039f:
		throw new NullReferenceException();
		IL_0417:
		return _labels;
	}

	public override void RefreshOptionLabels()
	{
		//IL_000c: Expected I, but got O
		//IL_001c: Expected O, but got I
		//IL_002c: Expected O, but got I
		_labels = null;
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx_v2 (Il2CppClass<Kamgam.SettingsGenerator.ResolutionConnection>)+2C8]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx_v2 (Il2CppClass<Kamgam.SettingsGenerator.ResolutionConnection>)+2D0]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v10 @ rax_v2 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	public override void SetOptionLabels(List<string> optionLabels)
	{
		List<Resolution> uniqueResolutions = getUniqueResolutions();
		if (optionLabels != null)
		{
			int size = optionLabels._size;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rax_v3 (System.Collections.Generic.List`1<UnityEngine.Resolution>)+18]");
			if ((nint)size == 0)
			{
				goto IL_006b;
			}
		}
		int num = default(int);
		string text = num.ToString();
		string message = "Invalid new labels. Need to be " + text + ".";
		Logger.LogError(message);
		goto IL_006b;
		IL_006b:
		List<string> labels = new List<string>(optionLabels);
		_labels = labels;
	}

	public string GetResolutionFormat()
	{
		return _resolutionFormat;
	}

	public void SetResolutionFormat(string format)
	{
		//IL_000f: Expected I, but got O
		//IL_001f: Expected O, but got I
		//IL_002f: Expected O, but got I
		_resolutionFormat = format;
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx_v1 (Il2CppClass<Kamgam.SettingsGenerator.ResolutionConnection>)+2E8]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx_v1 (Il2CppClass<Kamgam.SettingsGenerator.ResolutionConnection>)+2F0]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v10 @ rax_v2 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	public string GetRefreshRateFormat()
	{
		return _refreshRateFormat;
	}

	public void SetRefreshRateFormat(string format)
	{
		//IL_000f: Expected I, but got O
		//IL_001f: Expected O, but got I
		//IL_002f: Expected O, but got I
		_refreshRateFormat = format;
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx_v1 (Il2CppClass<Kamgam.SettingsGenerator.ResolutionConnection>)+2E8]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx_v1 (Il2CppClass<Kamgam.SettingsGenerator.ResolutionConnection>)+2F0]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v10 @ rax_v2 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	protected unsafe void onScreenSizeChanged(Resolution resolution)
	{
		//IL_0036: Expected O, but got Ref
		if (_settings != null)
		{
			Resolution currentResolution = ScreenOrchestrator.GetCurrentResolution();
			object obj = default(object);
			addOrRemoveCustomResolutionValue((Resolution)(&obj));
			RefreshOptionLabels();
			_settings.RefreshRegisteredResolversWithConnection(this);
			_settings.PullFromConnection(this);
		}
	}

	private unsafe void addOrRemoveCustomResolutionValue(Resolution resolution)
	{
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Expected O, but got Unknown
		//IL_0132: Expected O, but got I4
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Expected O, but got Unknown
		//IL_00af: Expected O, but got Ref
		//IL_00ba: Expected O, but got I4
		//IL_00d9: Expected O, but got Ref
		//IL_0072: Expected O, but got Ref
		//IL_007d: Expected O, but got I4
		//IL_009c: Expected O, but got Ref
		FullScreenMode fullScreenMode = Screen.fullScreenMode;
		if (fullScreenMode == FullScreenMode.Windowed && _addCustomResolutionOptionIfWindowed)
		{
			object obj = this + 136;
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			object obj2 = default(object);
			int num2 = default(int);
			int num3 = default(int);
			if (obj2 == null)
			{
				Resolution? resolution2 = (Resolution)(&num2);
				_windowedResolution = (Resolution?)(object)0;
				_ = 0;
				_values.Insert(0, (Resolution)(&num3));
			}
			else
			{
				Resolution? resolution2 = (Resolution)(&num3);
				_windowedResolution = (Resolution?)(object)0;
				_ = 0;
				_values.set_Item(0, (Resolution)(&num2));
			}
		}
		else
		{
			object obj3 = this + 136;
			nint num4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			object obj4 = default(object);
			if (obj4 != null)
			{
				_values.RemoveAt(0);
				_windowedResolution = (Resolution?)(object)0;
				_ = 0;
			}
		}
	}

	public override int Get()
	{
		//IL_0217: Expected O, but got I4
		//IL_0010: Expected O, but got I4
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Expected O, but got Unknown
		//IL_026e: Expected I, but got O
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Expected O, but got Unknown
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Expected O, but got Unknown
		int frameCount = Time.frameCount;
		object obj = frameCount - lastSetFrame;
		bool flag = (nint)obj <= 3;
		int num = 0;
		if (!flag)
		{
			lastKnownResolution = (Resolution?)(object)0;
			_ = 0;
			num = 0;
		}
		FullScreenMode fullScreenMode = Screen.fullScreenMode;
		int width = default(int);
		if (fullScreenMode == FullScreenMode.Windowed)
		{
			width = Screen.width;
			int num2 = 0;
		}
		else
		{
			Resolution currentResolution = Screen.currentResolution;
			num = currentResolution.m_Width;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3E0");
			int num2 = currentResolution.m_Width;
		}
		FullScreenMode fullScreenMode2 = Screen.fullScreenMode;
		int height = default(int);
		if (fullScreenMode2 == FullScreenMode.Windowed)
		{
			height = Screen.height;
		}
		else
		{
			Resolution currentResolution2 = Screen.currentResolution;
			num = currentResolution2.m_Width;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9CF10");
			int num2 = currentResolution2.m_Width;
		}
		object obj2 = this + 156;
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj3 = default(object);
		bool flag2 = obj3 == null;
		int height2 = height;
		int width2 = width;
		if (!flag2)
		{
			object obj4 = this + 156;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808CBE10");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3E0");
			object obj5 = this + 156;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808CBE10");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9CF10");
			int num4 = default(int);
			height2 = num4;
			int num5 = default(int);
			width2 = num5;
		}
		nint num6 = (nint)this;
		List<Resolution> uniqueResolutions = getUniqueResolutions();
		Resolution currentResolution3 = Screen.currentResolution;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E583B0");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm1,rcx\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2sd xmm0,rax\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm1,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
		int refreshRate = default(int);
		int num7 = findClosestResolutionIndex(uniqueResolutions, width2, height2, refreshRate);
		if (num7 < 0)
		{
			num7 = 0;
		}
		return num7;
	}

	public unsafe override void Set(int index)
	{
		//IL_0322: Expected I, but got O
		//IL_001b: Expected O, but got I
		//IL_006a: Expected I4, but got O
		//IL_0074: Expected I, but got O
		//IL_008a: Expected O, but got I
		//IL_009a: Expected O, but got I
		//IL_00aa: Expected O, but got I
		//IL_03ce: Expected O, but got I4
		//IL_03e1: Expected I, but got O
		//IL_0407: Expected O, but got I4
		//IL_040c: Expected I, but got O
		//IL_0440: Expected O, but got I4
		//IL_044e: Expected I, but got O
		//IL_0453: Expected I, but got O
		//IL_02cd: Expected O, but got Ref
		//IL_0479: Expected O, but got I4
		//IL_0487: Expected I, but got O
		//IL_048c: Expected I, but got O
		//IL_02f2: Expected O, but got Ref
		//IL_02fd: Expected O, but got I4
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v34 @ rdx_v1 (Il2CppClass<Kamgam.SettingsGenerator.ResolutionConnection>)+320]");
		nint num2 = 0;
		List<Resolution> uniqueResolutions = getUniqueResolutions();
		bool flag = (nint)uniqueResolutions < 0;
		int num3;
		if (uniqueResolutions != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ rax_v3 (System.Collections.Generic.List`1<UnityEngine.Resolution>)+18]");
			Delegate obj = (Delegate)(-1);
			Delegate obj2 = null;
			if (!flag)
			{
				obj2 = obj;
			}
			if (index >= 0)
			{
				bool flag2 = index <= (nint)obj2;
				num3 = index;
				if (!flag2)
				{
					num3 = (int)obj2;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				if (num3 != -1)
				{
					goto IL_00cc;
				}
				nint num4 = (nint)this;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ rax_v3 (System.Collections.Generic.List`1<UnityEngine.Resolution>)+18]");
				object obj3 = -1;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v234 @ r8_v18 (Il2CppClass<Kamgam.SettingsGenerator.ResolutionConnection>)+248]");
				object obj4 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v234 @ r8_v18 (Il2CppClass<Kamgam.SettingsGenerator.ResolutionConnection>)+250]");
				object obj5 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v238 @ rax_v39 (should have been resolved before IL gen)");
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			num3 = 0;
			goto IL_00cc;
		}
		NullReferenceException ex = new NullReferenceException();
		goto IL_04e2;
		IL_04e2:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		goto IL_04ca;
		IL_04a2:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		throw new NullReferenceException();
		IL_00cc:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
		ScreenOrchestrator instance = ScreenOrchestrator.Instance;
		ScreenOrchestrator.OnCompleteDelegate value = onComplete;
		Delegate obj6 = Delegate.Remove(instance.OnComplete, value);
		Delegate obj10;
		Delegate typeFromHandle;
		if ((object)obj6 == null)
		{
			instance.OnComplete = null;
		}
		else
		{
			bool flag3 = (object)obj6.GetType() != typeof(ScreenOrchestrator.OnCompleteDelegate);
			Delegate obj7 = null;
			if (!flag3)
			{
				obj7 = obj6;
			}
			bool flag4 = (object)obj7 == null;
			object obj8 = 0;
			typeFromHandle = (Delegate)(object)typeof(ScreenOrchestrator.OnCompleteDelegate);
			nint num5 = unchecked((nint)null);
			if (flag4)
			{
				goto IL_04a2;
			}
			instance.OnComplete = (ScreenOrchestrator.OnCompleteDelegate)obj7;
			bool flag5 = (object)obj6.GetType() != typeof(ScreenOrchestrator.OnCompleteDelegate);
			Delegate obj9 = null;
			if (!flag5)
			{
				obj9 = obj6;
			}
			bool flag6 = (object)obj9 == null;
			obj8 = 0;
			num5 = unchecked((nint)null);
			obj10 = (Delegate)(object)typeof(ScreenOrchestrator.OnCompleteDelegate);
			if (flag6)
			{
				goto IL_04b2;
			}
		}
		ScreenOrchestrator instance2 = ScreenOrchestrator.Instance;
		ScreenOrchestrator.OnCompleteDelegate b = onComplete;
		Delegate obj11 = Delegate.Combine(instance2.OnComplete, b);
		if ((object)obj11 == null)
		{
			instance2.OnComplete = null;
		}
		else
		{
			bool flag7 = (object)obj11.GetType() != typeof(ScreenOrchestrator.OnCompleteDelegate);
			Delegate obj12 = null;
			if (!flag7)
			{
				obj12 = obj11;
			}
			bool flag8 = (object)obj12 == null;
			object obj8 = 0;
			num2 = (nint)typeof(ScreenOrchestrator.OnCompleteDelegate);
			nint num5 = unchecked((nint)null);
			if (flag8)
			{
				goto IL_04ca;
			}
			instance2.OnComplete = (ScreenOrchestrator.OnCompleteDelegate)obj12;
			bool flag9 = (object)obj11.GetType() != typeof(ScreenOrchestrator.OnCompleteDelegate);
			Delegate obj13 = null;
			if (!flag9)
			{
				obj13 = obj11;
			}
			bool flag10 = (object)obj13 == null;
			obj8 = 0;
			num2 = (nint)typeof(ScreenOrchestrator.OnCompleteDelegate);
			num5 = unchecked((nint)null);
			ex = (NullReferenceException)(object)obj11;
			if (flag10)
			{
				goto IL_04e2;
			}
		}
		ScreenOrchestrator instance3 = ScreenOrchestrator.Instance;
		object obj14 = default(object);
		instance3.RequestResolution((Resolution)(&obj14));
		int frameCount = Time.frameCount;
		lastSetFrame = frameCount;
		object obj15 = default(object);
		Resolution? resolution = (Resolution)(&obj15);
		lastKnownResolution = (Resolution?)(object)0;
		_ = 0;
		base.NotifyListenersIfChanged(num3);
		return;
		IL_04b2:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		typeFromHandle = obj10;
		goto IL_04a2;
		IL_04ca:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		obj10 = obj11;
		goto IL_04b2;
	}

	private unsafe void onComplete(Resolution? resolution, bool? fullscreen, FullScreenMode? fullscreenmode)
	{
		//IL_0406: Expected O, but got I4
		//IL_03c4: Expected O, but got I4
		//IL_03cc: Expected I, but got O
		//IL_042c: Expected O, but got I4
		//IL_043a: Expected I, but got O
		//IL_043f: Expected I, but got O
		//IL_0448: Expected O, but got I4
		//IL_046e: Expected O, but got I4
		//IL_0473: Expected I, but got O
		//IL_047c: Expected O, but got I4
		//IL_048a: Expected I, but got O
		//IL_04b2: Expected O, but got I4
		//IL_04bb: Expected O, but got I4
		//IL_01a7: Expected O, but got I
		//IL_01b0: Expected O, but got I4
		//IL_0206: Expected O, but got I4
		//IL_028c: Expected O, but got I4
		//IL_0507: Expected I, but got O
		//IL_02b8: Expected I, but got O
		//IL_02c8: Expected O, but got I
		//IL_0305: Expected O, but got I4
		//IL_0323: Expected O, but got I
		//IL_0361: Expected O, but got I4
		ScreenOrchestrator instance = ScreenOrchestrator.Instance;
		bool flag = (object)instance == null;
		Resolution? resolution2 = (Resolution?)(object)0;
		FullScreenMode? fullScreenMode;
		List<SettingOption>.Enumerator enumerator;
		nint num2;
		if (!flag)
		{
			ScreenOrchestrator.OnCompleteDelegate value = onComplete;
			Delegate obj = Delegate.Remove(instance.OnComplete, value);
			if ((object)obj == null)
			{
				instance.OnComplete = (ScreenOrchestrator.OnCompleteDelegate)obj;
				goto IL_00e8;
			}
			bool flag2 = (object)obj.GetType() != typeof(ScreenOrchestrator.OnCompleteDelegate);
			Delegate obj2 = null;
			if (!flag2)
			{
				obj2 = obj;
			}
			bool flag3 = (object)obj2 == null;
			enumerator = (List<SettingOption>.Enumerator)0;
			nint num = (nint)typeof(ScreenOrchestrator.OnCompleteDelegate);
			num2 = unchecked((nint)null);
			fullScreenMode = (FullScreenMode?)(object)0;
			if (!flag3)
			{
				instance.OnComplete = (ScreenOrchestrator.OnCompleteDelegate)obj2;
				bool flag4 = (object)obj.GetType() != typeof(ScreenOrchestrator.OnCompleteDelegate);
				Delegate obj3 = null;
				if (!flag4)
				{
					obj3 = obj;
				}
				bool flag5 = (object)obj3 == null;
				enumerator = (List<SettingOption>.Enumerator)0;
				num2 = unchecked((nint)null);
				fullScreenMode = (FullScreenMode?)(object)0;
				nint num3 = (nint)typeof(ScreenOrchestrator.OnCompleteDelegate);
				if (!flag5)
				{
					goto IL_00e8;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
				num = num3;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
			return;
		}
		goto IL_03bb;
		IL_00e8:
		if (!RefreshRateResolversAfterCompletion || !(_settings != null))
		{
			return;
		}
		nint num4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj4 = default(object);
		if (obj4 == null)
		{
			return;
		}
		resolution2 = resolution;
		bool flag6 = (object)_settings == null;
		bool? flag7 = (bool?)(object)0;
		fullScreenMode = (FullScreenMode?)(object)0;
		if (!flag6)
		{
			IList<SettingOption> settingsWithConnectionByType = _settings.GetSettingsWithConnectionByType<SettingOption, RefreshRateConnection>(s_tmpOptionSettingsList);
			resolution2 = (Resolution?)s_tmpOptionSettingsList;
			bool flag8 = s_tmpOptionSettingsList == null;
			flag7 = (bool?)(object)0;
			fullScreenMode = (FullScreenMode?)(object)0;
			if (!flag8)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
				List<SettingOption>.Enumerator enumerator2 = default(List<SettingOption>.Enumerator);
				Resolution? resolution3 = default(Resolution?);
				object obj6 = default(object);
				List<SettingOption>.Enumerator enumerator3 = default(List<SettingOption>.Enumerator);
				object obj8 = default(object);
				object obj10 = default(object);
				nint num3;
				while (true)
				{
					if (enumerator2.MoveNext())
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
						bool flag9 = resolution3 == null;
						object obj5 = obj6;
						enumerator = enumerator3;
						num2 = 0;
						fullScreenMode = (FullScreenMode?)(object)0;
						num3 = (nint)(&enumerator2);
						if (!flag9)
						{
							object obj7 = resolution3;
							Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v626 @ rdx_v22+588] (should have been resolved before IL gen)");
							if (obj8 != null)
							{
								object obj9 = resolution3;
								Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v628 @ rdx_v24+5C8] (should have been resolved before IL gen)");
								bool flag10 = obj10 == null;
								obj5 = obj6;
								enumerator = enumerator3;
								num2 = 0;
								fullScreenMode = (FullScreenMode?)(object)0;
								resolution2 = resolution3;
								if (flag10)
								{
									break;
								}
								object obj11 = obj10;
								nint num5 = (nint)typeof(RefreshRateConnection);
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v145 @ rdx_v26 (Il2CppClass<Kamgam.SettingsGenerator.RefreshRateConnection>)+130]");
								resolution2 = (Resolution?)(object)0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v157 @ rax_v42+130]");
								nint num6 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v145 @ rdx_v26 (Il2CppClass<Kamgam.SettingsGenerator.RefreshRateConnection>)+130]");
								bool flag11 = num6 < 0;
								obj5 = obj6;
								enumerator = enumerator3;
								num2 = 0;
								fullScreenMode = (FullScreenMode?)(object)0;
								if (flag11)
								{
									break;
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v157 @ rax_v42+C8]");
								object obj12 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v158 @ rax_v43+FFFFFFF8+v159 @ rcx_v7 (System.Nullable`1<UnityEngine.Resolution>)*8]");
								bool flag12 = 0 != (nint)typeof(RefreshRateConnection);
								obj5 = obj6;
								enumerator = enumerator3;
								num2 = 0;
								fullScreenMode = (FullScreenMode?)(object)0;
								if (flag12)
								{
									break;
								}
								_ = 0;
								object obj13 = obj10;
								Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v631 @ rdx_v27+2E8] (should have been resolved before IL gen)");
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v156 @ rax_v41+28]");
								_ = 0;
								object obj14 = resolution3;
								Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v636 @ rdx_v29+5A8] (should have been resolved before IL gen)");
							}
							continue;
						}
						throw new NullReferenceException();
					}
					enumerator2.Dispose();
					_settings.RefreshRegisteredResolversWithConnection<RefreshRateConnection>();
					return;
				}
				num3 = (nint)resolution2;
				throw new NullReferenceException();
			}
		}
		goto IL_03bb;
		IL_03bb:
		enumerator = (List<SettingOption>.Enumerator)0;
		num2 = (nint)flag7;
		throw new NullReferenceException();
	}

	public void SetSettings(Settings settings)
	{
		_settings = settings;
	}

	public Settings GetSettings()
	{
		return _settings;
	}

	public ResolutionConnection()
	{
		List<Vector2Int> allowedAspectRatios = new List<Vector2Int>();
		AllowedAspectRatios = allowedAspectRatios;
		AllowedAspectRatioDelta = 0.02f;
		CustomResolutions = new List<CustomResolution>();
		_resolutionFormat = "{0}x{1}";
		_refreshRateFormat = " ({0}Hz)";
		base._002Ector();
	}

	static ResolutionConnection()
	{
		List<SettingOption> list = new List<SettingOption>();
		s_tmpOptionSettingsList = list;
	}
}
