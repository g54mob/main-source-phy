using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Kamgam.SettingsGenerator;

public class Settings : ScriptableObject, ISerializationCallbackReceiver
{
	public delegate void CustomStorageMethod(string key, Settings settings);

	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<ISetting, string> _003C_003E9__95_1;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal string _003CGetSettingIDsOrderedByName_003Eb__95_1(ISetting s)
		{
			if (s != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				string result = default(string);
				return result;
			}
			return (string)(object)new NullReferenceException();
		}
	}

	private sealed class _003C_003Ec__DisplayClass95_0
	{
		public bool filterByDataType;

		public SettingData.DataType[] dataTypes;

		internal unsafe bool _003CGetSettingIDsOrderedByName_003Eb__0(ISetting s)
		{
			//IL_005d: Expected I4, but got O
			if (!filterByDataType)
			{
				return true;
			}
			if (s != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				object obj = default(object);
				return Enumerable.Contains(dataTypes, (SettingData.DataType)(int)(&obj));
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	private Action<ISetting> m_OnSettingChanged;

	protected bool _isLoading;

	protected List<ISetting> _settingsCache;

	protected List<SettingBool> _bools;

	protected List<SettingOption> _options;

	protected List<SettingInt> _integers;

	protected List<SettingFloat> _floats;

	protected List<SettingString> _strings;

	protected List<SettingColor> _colors;

	protected List<SettingColorOption> _colorOptions;

	protected List<SettingKeyCombination> _keyCombinations;

	[NonSerialized]
	public static List<string> DeactivateBeforeInit;

	[NonSerialized]
	public static CustomStorageMethod CustomSaveMethod;

	[NonSerialized]
	public static CustomStorageMethod CustomLoadMethod;

	[NonSerialized]
	public static CustomStorageMethod CustomDeleteMethod;

	private static List<string> _tmpExistingIdsBeforeLoad;

	protected List<ISetting> _tmpSettingsSortedByConnectionOrder;

	protected List<ISetting> _tmpSettingsSortedByName;

	private static List<SettingOption> s_tmpRefreshSettingOptionConnectionAndResolversList;

	[NonSerialized]
	public List<ISettingResolver> RegisteredResolvers;

	[NonSerialized]
	public int ActiveResolverCount;

	public event Action<ISetting> OnSettingChanged
	{
		add
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 24;
			Delegate obj2 = this.m_OnSettingChanged;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Combine(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = obj3;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		remove
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 24;
			Delegate obj2 = this.m_OnSettingChanged;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Remove(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = obj3;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	public static void AddToDeactivateBeforeInit(string[] ids)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		//IL_001c: Expected O, but got I4
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Expected O, but got Unknown
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Expected O, but got Unknown
		if (ids != null)
		{
			object obj = ids + 32;
			object obj2 = 0;
			while ((nint)obj2 < ids.Length)
			{
				DeactivateBeforeInit.Add((string)obj);
				obj2++;
				obj += 8;
			}
		}
	}

	public void RebuildSettingsCache()
	{
		//IL_054f: Expected O, but got I
		//IL_08cd: Expected O, but got I4
		//IL_05b6: Expected O, but got I
		//IL_061a: Expected O, but got I4
		//IL_0630: Expected O, but got I
		//IL_0647: Unknown result type (might be due to invalid IL or missing references)
		//IL_064c: Expected O, but got Unknown
		//IL_0904: Expected O, but got I4
		//IL_066b: Expected O, but got I4
		//IL_0681: Expected O, but got I
		//IL_068a: Unknown result type (might be due to invalid IL or missing references)
		//IL_068f: Expected O, but got Unknown
		List<ISetting> settingsCache = _settingsCache;
		int version = settingsCache._version + 1;
		settingsCache._version = version;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
		object obj = default(object);
		if (obj == null)
		{
			settingsCache._size = 0;
		}
		else
		{
			settingsCache._size = 0;
			if (settingsCache._size > 0)
			{
				Array.Clear(settingsCache._items, 0, settingsCache._size);
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<SettingBool>.Enumerator enumerator = default(List<SettingBool>.Enumerator);
		ISetting setting = default(ISetting);
		List<SettingOption>.Enumerator enumerator2 = default(List<SettingOption>.Enumerator);
		List<SettingInt>.Enumerator enumerator3 = default(List<SettingInt>.Enumerator);
		List<SettingFloat>.Enumerator enumerator4 = default(List<SettingFloat>.Enumerator);
		List<SettingString>.Enumerator enumerator5 = default(List<SettingString>.Enumerator);
		List<SettingColor>.Enumerator enumerator6 = default(List<SettingColor>.Enumerator);
		List<SettingColorOption>.Enumerator enumerator7 = default(List<SettingColorOption>.Enumerator);
		List<SettingKeyCombination>.Enumerator enumerator8 = default(List<SettingKeyCombination>.Enumerator);
		List<ISetting>.Enumerator enumerator9 = default(List<ISetting>.Enumerator);
		object obj2 = default(object);
		object obj11 = default(object);
		object obj19 = default(object);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (setting != null)
				{
					if (_settingsCache == null)
					{
						break;
					}
					_settingsCache.Add(setting);
				}
				continue;
			}
			enumerator.Dispose();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			while (true)
			{
				if (enumerator2.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					if (setting != null)
					{
						List<ISetting> settingsCache2 = _settingsCache;
						if (_settingsCache == null)
						{
							break;
						}
						_settingsCache.Add(setting);
					}
					continue;
				}
				enumerator2.Dispose();
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
				while (true)
				{
					if (enumerator3.MoveNext())
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
						if (setting != null)
						{
							List<ISetting> settingsCache3 = _settingsCache;
							if (_settingsCache == null)
							{
								break;
							}
							_settingsCache.Add(setting);
						}
						continue;
					}
					enumerator3.Dispose();
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
					while (true)
					{
						if (enumerator4.MoveNext())
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
							if (setting != null)
							{
								List<ISetting> settingsCache4 = _settingsCache;
								if (_settingsCache == null)
								{
									break;
								}
								_settingsCache.Add(setting);
							}
							continue;
						}
						enumerator4.Dispose();
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
						while (true)
						{
							if (enumerator5.MoveNext())
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
								if (setting != null)
								{
									List<ISetting> settingsCache5 = _settingsCache;
									if (_settingsCache == null)
									{
										break;
									}
									_settingsCache.Add(setting);
								}
								continue;
							}
							enumerator5.Dispose();
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
							while (true)
							{
								if (enumerator6.MoveNext())
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
									if (setting != null)
									{
										List<ISetting> settingsCache6 = _settingsCache;
										if (_settingsCache == null)
										{
											break;
										}
										_settingsCache.Add(setting);
									}
									continue;
								}
								enumerator6.Dispose();
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
								while (true)
								{
									if (enumerator7.MoveNext())
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
										if (setting != null)
										{
											List<ISetting> settingsCache7 = _settingsCache;
											if (_settingsCache == null)
											{
												break;
											}
											_settingsCache.Add(setting);
										}
										continue;
									}
									enumerator7.Dispose();
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
									while (true)
									{
										if (enumerator8.MoveNext())
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
											if (setting != null)
											{
												if (_settingsCache == null)
												{
													break;
												}
												_settingsCache.Add(setting);
											}
											continue;
										}
										enumerator8.Dispose();
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
										while (true)
										{
											object obj10;
											if (enumerator9.MoveNext())
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
												Action<ISetting> action = onSettingChanged;
												if (obj2 == null)
												{
													break;
												}
												object obj3 = obj2;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1337 @ r10_v4+12E]");
												if ((nint)0 >= (nint)0)
												{
													goto IL_058f;
												}
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1337 @ r10_v4+B0]");
												object obj4 = 0;
												int num = 0;
												while (true)
												{
													object obj5 = num + num;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1351 @ r8_v47+v1354 @ rax_v88*8]");
													if (0 == (nint)typeof(ISetting))
													{
														break;
													}
													num++;
													int num2 = num;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1337 @ r10_v4+12E]");
													if ((nint)num2 < (nint)0)
													{
														continue;
													}
													goto IL_058f;
												}
												object obj6 = num + num;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1351 @ r8_v47+8+v1410 @ rcx_v72*8]");
												object obj7 = (nint)0 + (nint)1;
												object obj8 = obj7 << 4;
												object obj9 = obj8 + 312;
												obj10 = obj9 + obj3;
												goto IL_0954;
											}
											enumerator9.Dispose();
											return;
											IL_058f:
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
											obj10 = obj11;
											goto IL_0954;
											IL_0954:
											Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v1418 @ r8_v31] (should have been resolved before IL gen)");
											Action<ISetting> action2 = onSettingChanged;
											object obj12 = obj2;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1294 @ r10_v5+12E]");
											object obj18;
											if ((nint)0 < (nint)0)
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1294 @ r10_v5+B0]");
												object obj13 = 0;
												int num3 = 0;
												while (true)
												{
													object obj14 = num3 + num3;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1456 @ r8_v39+v1459 @ rax_v83*8]");
													if (0 == (nint)typeof(ISetting))
													{
														break;
													}
													num3++;
													int num4 = num3;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1294 @ r10_v5+12E]");
													if ((nint)num4 < (nint)0)
													{
														continue;
													}
													goto IL_05f6;
												}
												object obj15 = num3 + num3;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1456 @ r8_v39+8+v1516 @ rcx_v66*8]");
												object obj16 = (nint)0 << 4;
												object obj17 = obj16 + 312;
												obj18 = obj17 + obj12;
												goto IL_099f;
											}
											goto IL_05f6;
											IL_05f6:
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
											obj18 = obj19;
											goto IL_099f;
											IL_099f:
											Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v1522 @ r8_v34] (should have been resolved before IL gen)");
										}
										throw new NullReferenceException();
									}
									throw new NullReferenceException();
								}
								throw new NullReferenceException();
							}
							throw new NullReferenceException();
						}
						throw new NullReferenceException();
					}
					throw new NullReferenceException();
				}
				throw new NullReferenceException();
			}
			throw new NullReferenceException();
		}
		throw new NullReferenceException();
	}

	public List<ISetting> GetAllSettings()
	{
		if (CollectionExtensions.IsNullOrEmpty(_settingsCache))
		{
			RebuildSettingsCache();
		}
		return _settingsCache;
	}

	public unsafe List<ISetting> GetUnappliedSettings(List<ISetting> results = null)
	{
		//IL_014f: Expected O, but got Ref
		//IL_0165: Expected O, but got I
		//IL_032e: Expected O, but got I4
		//IL_0275: Expected O, but got I4
		//IL_028b: Expected O, but got I
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Expected O, but got Unknown
		//IL_0242: Expected O, but got I4
		//IL_025d: Expected O, but got I
		int num;
		List<ISetting> list;
		if (results != null)
		{
			int version = results._version + 1;
			results._version = version;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
			object obj = default(object);
			if (obj == null)
			{
				results._size = 0;
				num = 0;
				list = results;
			}
			else
			{
				results._size = 0;
				bool flag = results._size <= 0;
				num = 0;
				list = results;
				if (!flag)
				{
					Array.Clear(results._items, 0, results._size);
					num = 0;
					list = results;
				}
			}
		}
		else
		{
			List<ISetting> list2 = new List<ISetting>();
			num = 0;
			list = list2;
		}
		if (CollectionExtensions.IsNullOrEmpty(_settingsCache))
		{
			RebuildSettingsCache();
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		nint num2 = 0;
		List<ISetting>.Enumerator enumerator = default(List<ISetting>.Enumerator);
		IntPtr intPtr = default(IntPtr);
		object obj9 = default(object);
		object obj10 = default(object);
		object obj11 = default(object);
		while (true)
		{
			object obj8;
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				bool flag2 = intPtr == (IntPtr)0;
				List<ISetting> list3 = (List<ISetting>)(&enumerator);
				if (!flag2)
				{
					object obj2 = (nint)intPtr;
					int num3 = num;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v172 @ r10_v4+12E]");
					if ((nint)num3 >= (nint)0)
					{
						goto IL_01db;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v172 @ r10_v4+B0]");
					num2 = 0;
					int num4 = num;
					while (true)
					{
						object obj3 = num4 + num4;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v230 @ r8_v5 (Il2CppMethodInfo)+v382 @ rax_v27*8]");
						if (0 == (nint)typeof(ISetting))
						{
							break;
						}
						num4++;
						int num5 = num4;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v172 @ r10_v4+12E]");
						if ((nint)num5 < (nint)0)
						{
							continue;
						}
						goto IL_01db;
					}
					object obj4 = num4 + num4;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v230 @ r8_v5 (Il2CppMethodInfo)+8+v438 @ rcx_v21*8]");
					object obj5 = (nint)0 + (nint)6;
					object obj6 = obj5 << 4;
					object obj7 = obj6 + 312;
					obj8 = obj7 + obj2;
					goto IL_0389;
				}
				throw new NullReferenceException();
			}
			enumerator.Dispose();
			return list;
			IL_0389:
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v445 @ rdx_v12] (should have been resolved before IL gen)");
			if (obj9 == null)
			{
				continue;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			bool flag3 = obj10 == null;
			num2 = intPtr;
			if (!flag3)
			{
				bool flag4 = list == null;
				List<ISetting> list3 = (List<ISetting>)23;
				if (flag4)
				{
					break;
				}
				list.Add((ISetting)(nint)intPtr);
				num2 = 0;
			}
			continue;
			IL_01db:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			obj8 = obj11;
			num2 = 6;
			goto IL_0389;
		}
		throw new NullReferenceException();
	}

	public unsafe bool HasUnappliedSettings()
	{
		//IL_0041: Expected O, but got Ref
		if (CollectionExtensions.IsNullOrEmpty(_settingsCache))
		{
			RebuildSettingsCache();
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<ISetting>.Enumerator enumerator = default(List<ISetting>.Enumerator);
		IntPtr intPtr = default(IntPtr);
		object obj = default(object);
		object obj2 = default(object);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				bool flag = intPtr == (IntPtr)0;
				List<ISetting> list = (List<ISetting>)(&enumerator);
				if (flag)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				if (obj != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					if (obj2 != null)
					{
						enumerator.Dispose();
						return true;
					}
				}
				continue;
			}
			enumerator.Dispose();
			return false;
		}
		throw new NullReferenceException();
	}

	protected void onSettingChanged(ISetting setting)
	{
		if (_isLoading)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
		object obj = default(object);
		if (obj != null)
		{
			Action<ISetting> action = this.m_OnSettingChanged;
			if (this.m_OnSettingChanged != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v63 @ rcx_v4 (System.Action`1<Kamgam.SettingsGenerator.ISetting>)+18] (should have been resolved before IL gen)");
			}
		}
	}

	public void RemoveSetting(ISetting setting)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
		string id = default(string);
		removeSetting(_bools, id);
		removeSetting(_options, id);
		removeSetting(_integers, id);
		removeSetting(_floats, id);
		removeSetting(_strings, id);
		removeSetting(_colors, id);
		removeSetting(_colorOptions, id);
		removeSetting(_keyCombinations, id);
		removeSetting(_settingsCache, id);
	}

	public void RemoveSetting(string id)
	{
		removeSetting(_bools, id);
		removeSetting(_options, id);
		removeSetting(_integers, id);
		removeSetting(_floats, id);
		removeSetting(_strings, id);
		removeSetting(_colors, id);
		removeSetting(_colorOptions, id);
		removeSetting(_keyCombinations, id);
		removeSetting(_settingsCache, id);
	}

	protected unsafe void removeSetting<T>(List<T> list, string id)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0039: Expected O, but got I
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		//IL_0158: Expected O, but got I
		//IL_006b: Expected O, but got I8
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Expected O, but got Unknown
		//IL_01aa: Expected O, but got I
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Expected O, but got Unknown
		//IL_007d: Expected O, but got I8
		//IL_00b3: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		nint num = 0;
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v58 @ rax_v4 (Il2CppClass<T>)+FC]");
		object obj3 = (nint)0 + (nint)16;
		object obj4 = obj3 + 15;
		object obj5;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3))
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ rcx_v2 (Il2CppClass<T>)+FC]");
			obj5 = (nint)0 + (nint)15;
			object obj6 = obj5;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ rcx_v2 (Il2CppClass<T>)+FC]");
			if ((nint)obj6 > 0)
			{
				goto IL_017c;
			}
		}
		obj5 = 1152921504606846960L;
		goto IL_017c;
		IL_017c:
		object obj7 = obj5 & -16;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ rcx_v2 (Il2CppClass<T>)+FC]");
		object obj8 = (nint)0 + (nint)15;
		_ = ref obj2;
		object obj9 = obj8;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ rcx_v2 (Il2CppClass<T>)+FC]");
		if ((nint)obj9 <= 0)
		{
			obj8 = 1152921504606846960L;
		}
		object obj10 = obj8 & -16;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
		bool flag = (nint)list < 0;
		int num3 = list._size - 1;
		if (flag)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ rcx_v2 (Il2CppClass<T>)+FC]");
		obj = 0;
		while (true)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
			nint num4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A76C0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+58]");
			if ((nint)0 != 0)
			{
				break;
			}
			num3--;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+58]");
			if ((nint)0 < (nint)0)
			{
				return;
			}
		}
		list.RemoveAt(num3);
	}

	public void OnBeforeSerialize()
	{
		//IL_00d8: Expected I, but got O
		//IL_008a: Expected O, but got I4
		//IL_0100: Expected O, but got I
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Expected O, but got Unknown
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Expected O, but got Unknown
		RebuildSettingsCache();
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		nint num = 0;
		List<ISetting>.Enumerator enumerator = default(List<ISetting>.Enumerator);
		object obj = default(object);
		object obj10 = default(object);
		while (true)
		{
			object obj9;
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (obj == null)
				{
					break;
				}
				object obj2 = obj;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v123 @ r10_v3+12E]");
				if ((nint)0 < (nint)0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v123 @ r10_v3+B0]");
					num = 0;
					object obj3 = 0;
					while (true)
					{
						object obj4 = obj3 + obj3;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v130 @ r8_v3 (Il2CppMethodInfo)+v229 @ rcx_v15*8]");
						if (0 == (nint)typeof(ISerializationCallbackReceiver))
						{
							break;
						}
						obj3++;
						object obj5 = obj3;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v123 @ r10_v3+12E]");
						if ((nint)obj5 < 0)
						{
							continue;
						}
						goto IL_00c1;
					}
					object obj6 = obj3 + obj3;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v130 @ r8_v3 (Il2CppMethodInfo)+8+v283 @ rcx_v17*8]");
					object obj7 = (nint)0 << 4;
					object obj8 = obj7 + 312;
					obj9 = obj8 + obj2;
					goto IL_01b1;
				}
				goto IL_00c1;
			}
			enumerator.Dispose();
			return;
			IL_00c1:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			obj9 = obj10;
			num = unchecked((nint)null);
			goto IL_01b1;
			IL_01b1:
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v288 @ rdx_v9] (should have been resolved before IL gen)");
		}
		throw new NullReferenceException();
	}

	public void OnAfterDeserialize()
	{
		//IL_008a: Expected O, but got I4
		//IL_0104: Expected O, but got I
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Expected O, but got Unknown
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Expected O, but got Unknown
		RebuildSettingsCache();
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		nint num = 0;
		List<ISetting>.Enumerator enumerator = default(List<ISetting>.Enumerator);
		object obj = default(object);
		object obj11 = default(object);
		while (true)
		{
			object obj10;
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (obj == null)
				{
					break;
				}
				object obj2 = obj;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v123 @ r10_v3+12E]");
				if ((nint)0 < (nint)0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v123 @ r10_v3+B0]");
					num = 0;
					object obj3 = 0;
					while (true)
					{
						object obj4 = obj3 + obj3;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v130 @ r8_v3 (Il2CppMethodInfo)+v229 @ rcx_v15*8]");
						if (0 == (nint)typeof(ISerializationCallbackReceiver))
						{
							break;
						}
						obj3++;
						object obj5 = obj3;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v123 @ r10_v3+12E]");
						if ((nint)obj5 < 0)
						{
							continue;
						}
						goto IL_00c1;
					}
					object obj6 = obj3 + obj3;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v130 @ r8_v3 (Il2CppMethodInfo)+8+v283 @ rcx_v17*8]");
					object obj7 = (nint)0 + (nint)1;
					object obj8 = obj7 << 4;
					object obj9 = obj8 + 312;
					obj10 = obj9 + obj2;
					goto IL_01c3;
				}
				goto IL_00c1;
			}
			enumerator.Dispose();
			return;
			IL_00c1:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			obj10 = obj11;
			num = 1;
			goto IL_01c3;
			IL_01c3:
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v290 @ rdx_v9] (should have been resolved before IL gen)");
		}
		throw new NullReferenceException();
	}

	public void Load(string key, SettingsSaverBase settingsSaver)
	{
		SettingsGeneratorSettings orCreate = SettingsGeneratorSettings.GetOrCreate();
		SettingsProvider provider = orCreate.Provider;
		SettingsProvider provider2 = default(SettingsProvider);
		Load(key, settingsSaver, removeUnknownSettingsAfterLoad: false, provider2);
	}

	public unsafe void Load(string key, SettingsSaverBase settingsSaver, bool removeUnknownSettingsAfterLoad, SettingsProvider provider)
	{
		//IL_06e1: Expected O, but got I4
		//IL_071b: Expected O, but got I
		//IL_064e: Expected O, but got I
		//IL_065e: Expected O, but got I
		//IL_02b0: Expected I, but got O
		//IL_0257: Expected I, but got O
		//IL_0267: Expected O, but got I
		//IL_030d: Expected O, but got I4
		//IL_00f9: Expected O, but got Ref
		//IL_0106: Expected I, but got O
		//IL_0191: Expected O, but got I4
		//IL_013e: Expected O, but got I
		//IL_057d: Expected O, but got I4
		//IL_01bf: Expected O, but got I4
		//IL_01d5: Expected O, but got I
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Expected O, but got Unknown
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Expected O, but got Unknown
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Expected O, but got Unknown
		//IL_0428: Expected O, but got I4
		_isLoading = true;
		int size;
		SettingsSaverBase settingsSaverBase;
		List<ISetting>.Enumerator enumerator2;
		string text;
		Settings settings;
		if (removeUnknownSettingsAfterLoad)
		{
			RebuildSettingsCache();
			List<string> tmpExistingIdsBeforeLoad = _tmpExistingIdsBeforeLoad;
			bool flag = _tmpExistingIdsBeforeLoad == null;
			settings = this;
			if (!flag)
			{
				int version = tmpExistingIdsBeforeLoad._version + 1;
				tmpExistingIdsBeforeLoad._version = version;
				if (!RuntimeHelpers.IsReferenceOrContainsReferences<string>())
				{
					tmpExistingIdsBeforeLoad._size = 0;
				}
				else
				{
					tmpExistingIdsBeforeLoad._size = 0;
					if (tmpExistingIdsBeforeLoad._size > 0)
					{
						Array.Clear(tmpExistingIdsBeforeLoad._items, 0, tmpExistingIdsBeforeLoad._size);
					}
				}
				settings = (Settings)(object)_settingsCache;
				if (_settingsCache != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
					List<ISetting>.Enumerator enumerator = default(List<ISetting>.Enumerator);
					Settings settings2 = default(Settings);
					object obj8 = default(object);
					string item = default(string);
					while (enumerator.MoveNext())
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
						settings = (Settings)(&enumerator);
						object obj;
						object obj7;
						if ((object)settings2 != null)
						{
							nint num = (nint)settings2;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v401 @ r10_v10 (Il2CppClass<Kamgam.SettingsGenerator.Settings>)+12E]");
							if ((nint)0 >= (nint)0)
							{
								goto IL_017e;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v401 @ r10_v10 (Il2CppClass<Kamgam.SettingsGenerator.Settings>)+B0]");
							obj = 0;
							int num2 = 0;
							while (true)
							{
								object obj2 = num2 + num2;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v413 @ r8_v26+v874 @ rax_v82*8]");
								if (0 == (nint)typeof(ISetting))
								{
									break;
								}
								num2++;
								int num3 = num2;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v401 @ r10_v10 (Il2CppClass<Kamgam.SettingsGenerator.Settings>)+12E]");
								if ((nint)num3 < (nint)0)
								{
									continue;
								}
								goto IL_017e;
							}
							object obj3 = num2 + num2;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v413 @ r8_v26+8+v978 @ rcx_v64*8]");
							object obj4 = (nint)0 + (nint)4;
							object obj5 = obj4 << 4;
							object obj6 = obj5 + 312;
							obj7 = obj6 + num;
							goto IL_05d8;
						}
						throw new NullReferenceException();
						IL_017e:
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
						obj = 4;
						obj7 = obj8;
						goto IL_05d8;
						IL_05d8:
						Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v985 @ rdx_v31] (should have been resolved before IL gen)");
						bool flag2 = _tmpExistingIdsBeforeLoad == null;
						settings = settings2;
						if (!flag2)
						{
							_tmpExistingIdsBeforeLoad.Add(item);
							continue;
						}
						throw new NullReferenceException();
					}
					enumerator.Dispose();
					List<ISetting>.Enumerator enumerator3 = default(List<ISetting>.Enumerator);
					enumerator2 = enumerator3;
					size = 0;
					settingsSaverBase = settingsSaver;
					text = key;
					goto IL_0608;
				}
			}
			goto IL_04f8;
		}
		enumerator2 = (List<ISetting>.Enumerator)0;
		size = 0;
		settingsSaverBase = settingsSaver;
		text = key;
		goto IL_0608;
		IL_04e2:
		SettingsProvider provider2 = default(SettingsProvider);
		postLoad(provider2);
		_isLoading = false;
		return;
		IL_0608:
		settings = (Settings)(object)typeof(Settings);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v455 @ rcx_v36 (Kamgam.SettingsGenerator.Settings)+B8]");
		object obj9 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v178 @ rax_v8+10]");
		if ((nint)0 == 0)
		{
			if ((object)settingsSaver == null)
			{
				goto IL_04f8;
			}
			nint num4 = (nint)settingsSaverBase;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v449 @ r9_v14 (Il2CppClass<Kamgam.SettingsGenerator.SettingsSaverBase>)+178]");
			Action<ISetting> action = (Action<ISetting>)0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v449 @ r9_v14 (Il2CppClass<Kamgam.SettingsGenerator.SettingsSaverBase>)+180]");
			nint num5 = 0;
			SettingsSaverBase settingsSaverBase2 = settingsSaver;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v455 @ rcx_v36 (Kamgam.SettingsGenerator.Settings)+B8]");
			object obj10 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v243 @ rax_v50+10]");
			settings = (Settings)0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v243 @ rax_v50+10]");
			if ((nint)0 == 0)
			{
				goto IL_04f8;
			}
			Action<ISetting> action = settings.m_OnSettingChanged;
			SettingsSaverBase settingsSaverBase2 = (SettingsSaverBase)(object)settings._integers;
			nint num5 = (nint)settings._settingsCache;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v519 @ rax_v9 (System.Action`1<Kamgam.SettingsGenerator.ISetting>) (should have been resolved before IL gen)");
		if (!removeUnknownSettingsAfterLoad)
		{
			goto IL_04e2;
		}
		RebuildSettingsCache();
		List<ISetting> settingsCache = _settingsCache;
		bool flag3 = (nint)_settingsCache < 0;
		bool flag4 = _settingsCache == null;
		settings = this;
		if (!flag4)
		{
			object obj11 = settingsCache._size - 1;
			settings = this;
			if (flag3)
			{
				goto IL_06b1;
			}
			ISetting setting = default(ISetting);
			ISetting setting2 = default(ISetting);
			while (true)
			{
				settings = (Settings)(object)_settingsCache;
				if (_settingsCache == null)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				if (setting == null)
				{
					break;
				}
				string iD = setting.GetID();
				bool flag5 = _tmpExistingIdsBeforeLoad == null;
				settings = (Settings)setting;
				if (flag5)
				{
					break;
				}
				bool flag6 = _tmpExistingIdsBeforeLoad.Contains(iD);
				bool flag7 = (flag6 ? 1 : 0) < (false ? 1 : 0);
				settings = (Settings)(object)_tmpExistingIdsBeforeLoad;
				if (!flag6)
				{
					settings = (Settings)(object)_settingsCache;
					flag7 = (nint)_settingsCache < 0;
					if (_settingsCache == null)
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					RemoveSetting(setting2);
					settings = this;
				}
				obj11--;
				object obj12 = !flag7;
				if (obj12 == null)
				{
					goto IL_06b1;
				}
			}
		}
		goto IL_04f8;
		IL_04f8:
		throw new NullReferenceException();
		IL_06b1:
		List<string> tmpExistingIdsBeforeLoad2 = _tmpExistingIdsBeforeLoad;
		if (_tmpExistingIdsBeforeLoad != null)
		{
			int version2 = tmpExistingIdsBeforeLoad2._version + 1;
			tmpExistingIdsBeforeLoad2._version = version2;
			if (!RuntimeHelpers.IsReferenceOrContainsReferences<string>())
			{
				tmpExistingIdsBeforeLoad2._size = size;
			}
			else
			{
				tmpExistingIdsBeforeLoad2._size = size;
				if (tmpExistingIdsBeforeLoad2._size > 0)
				{
					Array.Clear(tmpExistingIdsBeforeLoad2._items, 0, tmpExistingIdsBeforeLoad2._size);
				}
			}
			RebuildSettingsCache();
			goto IL_04e2;
		}
		goto IL_04f8;
	}

	protected unsafe void postLoad(SettingsProvider provider)
	{
		//IL_003b: Expected O, but got Ref
		//IL_0051: Expected O, but got I
		//IL_02df: Expected O, but got Ref
		//IL_010c: Expected I, but got O
		//IL_026d: Expected O, but got I
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Expected O, but got Unknown
		//IL_02f5: Expected O, but got I
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Expected O, but got Unknown
		//IL_0150: Expected I, but got O
		//IL_03c3: Expected O, but got I
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_03df: Expected O, but got Unknown
		//IL_0175: Expected I, but got O
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Expected O, but got Unknown
		//IL_01bb: Expected I, but got O
		//IL_04b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b9: Expected O, but got Unknown
		deactivateBeforeInitialization();
		RebuildSettingsCache();
		List<ISetting> settingsCache = _settingsCache;
		if (_settingsCache != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			nint num = 0;
			List<ISetting>.Enumerator enumerator = default(List<ISetting>.Enumerator);
			IntPtr intPtr = default(IntPtr);
			object obj8 = default(object);
			object obj9 = default(object);
			object obj10 = default(object);
			IntPtr intPtr2 = default(IntPtr);
			object obj11 = default(object);
			object obj12 = default(object);
			object obj13 = default(object);
			while (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				bool flag = intPtr == (IntPtr)0;
				settingsCache = (List<ISetting>)(&enumerator);
				object obj7;
				if (!flag)
				{
					object obj = (nint)intPtr;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v232 @ r10_v9+12E]");
					if ((nint)0 >= (nint)0)
					{
						goto IL_00c5;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v232 @ r10_v9+B0]");
					num = 0;
					List<ISetting> list = null;
					while (true)
					{
						object obj2 = (object)list + (object)list;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v119 @ r8_v5 (Il2CppMethodInfo)+v435 @ rax_v75*8]");
						if (0 == (nint)typeof(ISetting))
						{
							break;
						}
						list = (List<ISetting>)(list + 1);
						List<ISetting> list2 = list;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v232 @ r10_v9+12E]");
						if ((nint)list2 < 0)
						{
							continue;
						}
						goto IL_00c5;
					}
					object obj3 = (object)list + (object)list;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v119 @ r8_v5 (Il2CppMethodInfo)+8+v495 @ rcx_v70*8]");
					object obj4 = (nint)0 + (nint)6;
					object obj5 = obj4 << 4;
					object obj6 = obj5 + 312;
					obj7 = obj6 + obj;
					goto IL_057b;
				}
				throw new NullReferenceException();
				IL_00c5:
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
				num = 6;
				obj7 = obj8;
				goto IL_057b;
				IL_057b:
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v502 @ rdx_v38] (should have been resolved before IL gen)");
				if (obj9 == null)
				{
					continue;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				bool flag2 = obj10 == null;
				nint num2 = (nint)typeof(ISetting);
				if (!flag2)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					bool flag3 = intPtr2 == (IntPtr)0;
					num2 = (nint)typeof(ISetting);
					IntPtr intPtr3 = intPtr;
					if (!flag3)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
						num2 = (nint)this;
						intPtr3 = intPtr2;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if (obj11 != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
						num2 = (nint)provider;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				bool flag4 = obj12 != null;
				num = intPtr;
				if (!flag4)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					bool flag5 = obj13 != null;
					num = intPtr;
					if (!flag5)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
						num = intPtr;
					}
				}
			}
			enumerator.Dispose();
			if (_settingsCache != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
				nint num3 = 0;
				List<ISetting>.Enumerator enumerator2 = default(List<ISetting>.Enumerator);
				IntPtr intPtr4 = default(IntPtr);
				object obj21 = default(object);
				object obj22 = default(object);
				while (enumerator2.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					bool flag6 = intPtr4 == (IntPtr)0;
					settingsCache = (List<ISetting>)(&enumerator2);
					object obj20;
					if (!flag6)
					{
						object obj14 = (nint)intPtr4;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v517 @ r10_v8+12E]");
						if ((nint)0 >= (nint)0)
						{
							goto IL_0369;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v517 @ r10_v8+B0]");
						num3 = 0;
						List<ISetting> list3 = null;
						while (true)
						{
							object obj15 = (object)list3 + (object)list3;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v519 @ r8_v7 (Il2CppMethodInfo)+v663 @ rax_v54*8]");
							if (0 == (nint)typeof(ISetting))
							{
								break;
							}
							list3 = (List<ISetting>)(list3 + 1);
							List<ISetting> list4 = list3;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v517 @ r10_v8+12E]");
							if ((nint)list4 < 0)
							{
								continue;
							}
							goto IL_0369;
						}
						object obj16 = (object)list3 + (object)list3;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v519 @ r8_v7 (Il2CppMethodInfo)+8+v737 @ rcx_v46*8]");
						object obj17 = (nint)0 + (nint)6;
						object obj18 = obj17 << 4;
						object obj19 = obj18 + 312;
						obj20 = obj19 + obj14;
						goto IL_062e;
					}
					throw new NullReferenceException();
					IL_0369:
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
					num3 = 6;
					obj20 = obj21;
					goto IL_062e;
					IL_062e:
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v744 @ rdx_v27] (should have been resolved before IL gen)");
					if (obj22 != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
						num3 = intPtr4;
					}
				}
				enumerator2.Dispose();
				List<ISetting> settingsOrderedByConnectionOrderASC = getSettingsOrderedByConnectionOrderASC(_settingsCache);
				bool flag7 = settingsOrderedByConnectionOrderASC == null;
				List<ISetting> list5 = null;
				List<ISetting> list6 = null;
				settingsCache = null;
				if (!flag7)
				{
					ISetting setting = default(ISetting);
					object obj23 = default(object);
					while (true)
					{
						if ((nint)list6 < settingsOrderedByConnectionOrderASC._size)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
							if (setting == null)
							{
								break;
							}
							if (setting.IsActive)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
								if (obj23 != null)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
								}
							}
							list5 = (List<ISetting>)(list5 + 1);
							list6 = list5;
							continue;
						}
						RefreshRegisteredResolvers();
						return;
					}
				}
			}
		}
		throw new NullReferenceException();
	}

	protected void deactivateBeforeInitialization()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<string>.Enumerator enumerator = default(List<string>.Enumerator);
		string id = default(string);
		while (enumerator.MoveNext())
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
			ISetting setting = GetSetting(id);
			if (setting != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18004DF40");
			}
		}
		enumerator.Dispose();
	}

	public void Save(string key, SettingsSaverBase settingsSaver)
	{
		//IL_000d: Expected I, but got O
		//IL_001d: Expected O, but got I
		//IL_002d: Expected O, but got I
		while (true)
		{
			if (CustomSaveMethod == null)
			{
				nint num = (nint)settingsSaver;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v82 @ r9_v2 (Il2CppClass<Kamgam.SettingsGenerator.SettingsSaverBase>)+188]");
				object obj = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v82 @ r9_v2 (Il2CppClass<Kamgam.SettingsGenerator.SettingsSaverBase>)+190]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v86 @ rax_v10 (should have been resolved before IL gen)");
			}
			CustomStorageMethod customSaveMethod = CustomSaveMethod;
			IntPtr invoke_impl = ((Delegate)customSaveMethod).invoke_impl;
			IntPtr method = ((Delegate)customSaveMethod).method;
			IntPtr method_code = ((Delegate)customSaveMethod).method_code;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v128 @ rax_v8 (System.IntPtr) (should have been resolved before IL gen)");
		}
	}

	public void Delete(string key, SettingsSaverBase settingsSaver)
	{
		//IL_000d: Expected I, but got O
		//IL_001d: Expected O, but got I
		//IL_002d: Expected O, but got I
		while (true)
		{
			if (CustomDeleteMethod == null)
			{
				nint num = (nint)settingsSaver;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v82 @ r8_v2 (Il2CppClass<Kamgam.SettingsGenerator.SettingsSaverBase>)+198]");
				object obj = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v82 @ r8_v2 (Il2CppClass<Kamgam.SettingsGenerator.SettingsSaverBase>)+1A0]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v85 @ rax_v10 (should have been resolved before IL gen)");
			}
			CustomStorageMethod customDeleteMethod = CustomDeleteMethod;
			IntPtr invoke_impl = ((Delegate)customDeleteMethod).invoke_impl;
			IntPtr method = ((Delegate)customDeleteMethod).method;
			IntPtr method_code = ((Delegate)customDeleteMethod).method_code;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v127 @ rax_v8 (System.IntPtr) (should have been resolved before IL gen)");
		}
	}

	public static void DeletePlayerPrefs(string playerPrefsKey)
	{
		PlayerPrefs.DeleteKey(playerPrefsKey);
		PlayerPrefs.Save();
	}

	public void Apply(bool changedOnly = true, bool triggerChangeEvents = false)
	{
		//IL_00ad: Expected O, but got I4
		//IL_00b6: Expected O, but got I4
		//IL_000e: Expected O, but got I4
		//IL_0017: Expected O, but got I4
		//IL_028b: Expected O, but got I4
		//IL_0294: Expected O, but got I4
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Expected O, but got Unknown
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Expected O, but got Unknown
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Expected O, but got Unknown
		List<ISetting> settingsOrderedByConnectionOrderASC = getSettingsOrderedByConnectionOrderASC(_settingsCache);
		ISetting setting = default(ISetting);
		if (!changedOnly)
		{
			object obj = 0;
			object obj2 = 0;
			while ((nint)obj2 < settingsOrderedByConnectionOrderASC._size)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				if (setting.IsActive)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				}
				obj++;
				obj2 = obj;
			}
		}
		object obj3 = 0;
		object obj5 = default(object);
		for (object obj4 = 0; (nint)obj4 < settingsOrderedByConnectionOrderASC._size; obj3++, obj4 = obj3)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			if (!setting.IsActive)
			{
				continue;
			}
			if (changedOnly)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				if (obj5 == null)
				{
					continue;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
		}
		if (!triggerChangeEvents)
		{
			return;
		}
		List<ISetting> settingsOrderedByConnectionOrderASC2 = getSettingsOrderedByConnectionOrderASC(_settingsCache);
		object obj6 = 0;
		object obj7 = 0;
		object obj8 = default(object);
		while ((nint)obj7 < settingsOrderedByConnectionOrderASC2._size)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			if (setting.IsActive)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				if (obj8 == null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				}
			}
			obj6++;
			obj7 = obj6;
		}
	}

	public void TriggerChangeEvent(bool skipSettingsWithConnections = true)
	{
		//IL_00ea: Expected O, but got I4
		//IL_00f3: Expected O, but got I4
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Expected O, but got Unknown
		List<ISetting> settingsOrderedByConnectionOrderASC = getSettingsOrderedByConnectionOrderASC(_settingsCache);
		object obj = 0;
		ISetting setting = default(ISetting);
		object obj3 = default(object);
		for (object obj2 = 0; (nint)obj2 < settingsOrderedByConnectionOrderASC._size; obj++, obj2 = obj)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			if (!setting.IsActive)
			{
				continue;
			}
			if (skipSettingsWithConnections)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				if (obj3 != null)
				{
					continue;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
		}
	}

	public void PullFromConnection(IConnection connection, bool exceptUnapplied = false, bool propagateChange = false)
	{
		//IL_0116: Expected O, but got I4
		//IL_011f: Expected O, but got I4
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Expected O, but got Unknown
		List<ISetting> settingsOrderedByConnectionOrderASC = getSettingsOrderedByConnectionOrderASC(_settingsCache);
		object obj = 0;
		ISetting setting = default(ISetting);
		object obj3 = default(object);
		object obj4 = default(object);
		object obj5 = default(object);
		for (object obj2 = 0; (nint)obj2 < settingsOrderedByConnectionOrderASC._size; obj++, obj2 = obj)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			if (!setting.IsActive)
			{
				continue;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			if (obj3 == null)
			{
				continue;
			}
			if (exceptUnapplied)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				if (obj4 != null)
				{
					continue;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			if (obj5 == connection)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18004DF40");
			}
		}
	}

	public void PushToConnection(IConnection connection, bool exceptUnapplied = false)
	{
		//IL_0116: Expected O, but got I4
		//IL_011f: Expected O, but got I4
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Expected O, but got Unknown
		List<ISetting> settingsOrderedByConnectionOrderASC = getSettingsOrderedByConnectionOrderASC(_settingsCache);
		object obj = 0;
		ISetting setting = default(ISetting);
		object obj3 = default(object);
		object obj4 = default(object);
		object obj5 = default(object);
		for (object obj2 = 0; (nint)obj2 < settingsOrderedByConnectionOrderASC._size; obj++, obj2 = obj)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			if (!setting.IsActive)
			{
				continue;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			if (obj3 == null)
			{
				continue;
			}
			if (exceptUnapplied)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				if (obj4 != null)
				{
					continue;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			if (obj5 == connection)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			}
		}
	}

	public void PullFromConnections(bool exceptUnapplied = false, bool propagateChange = false)
	{
		//IL_00ed: Expected O, but got I4
		//IL_00f6: Expected O, but got I4
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Expected O, but got Unknown
		List<ISetting> settingsOrderedByConnectionOrderASC = getSettingsOrderedByConnectionOrderASC(_settingsCache);
		object obj = 0;
		ISetting setting = default(ISetting);
		object obj3 = default(object);
		object obj4 = default(object);
		for (object obj2 = 0; (nint)obj2 < settingsOrderedByConnectionOrderASC._size; obj++, obj2 = obj)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			if (!setting.IsActive)
			{
				continue;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			if (obj3 == null)
			{
				continue;
			}
			if (exceptUnapplied)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				if (obj4 != null)
				{
					continue;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18004DF40");
		}
	}

	public void PushToConnections()
	{
		//IL_00aa: Expected O, but got I4
		//IL_00b3: Expected O, but got I4
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Expected O, but got Unknown
		List<ISetting> settingsOrderedByConnectionOrderASC = getSettingsOrderedByConnectionOrderASC(_settingsCache);
		object obj = 0;
		object obj2 = 0;
		ISetting setting = default(ISetting);
		object obj3 = default(object);
		while ((nint)obj2 < settingsOrderedByConnectionOrderASC._size)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			if (setting.IsActive)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				if (obj3 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				}
			}
			obj++;
			obj2 = obj;
		}
	}

	public void PushToConnections(string[] groups)
	{
		//IL_00f5: Expected O, but got I4
		//IL_00fe: Expected O, but got I4
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Expected O, but got Unknown
		List<ISetting> settingsOrderedByConnectionOrderASC = getSettingsOrderedByConnectionOrderASC(_settingsCache);
		object obj = 0;
		object obj2 = 0;
		ISetting setting = default(ISetting);
		object obj3 = default(object);
		object obj4 = default(object);
		while ((nint)obj2 < settingsOrderedByConnectionOrderASC._size)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			if (setting.IsActive)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
				if (obj3 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					if (obj4 != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					}
				}
			}
			obj++;
			obj2 = obj;
		}
	}

	protected unsafe List<ISetting> getSettingsOrderedByConnectionOrderASC(IEnumerable<ISetting> settings)
	{
		//IL_0108: Expected I, but got O
		//IL_013c: Expected O, but got Ref
		//IL_0197: Expected I, but got O
		//IL_0222: Expected O, but got I4
		//IL_01cf: Expected O, but got I
		//IL_044d: Expected O, but got I4
		//IL_0237: Expected I, but got O
		//IL_031d: Expected O, but got I4
		//IL_0333: Expected O, but got I
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Expected O, but got Unknown
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Expected O, but got Unknown
		//IL_02c2: Expected O, but got I4
		//IL_052f: Expected I, but got O
		//IL_026f: Expected O, but got I
		//IL_048f: Expected O, but got I4
		//IL_0360: Expected O, but got I4
		//IL_0376: Expected O, but got I
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Expected O, but got Unknown
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Expected O, but got Unknown
		//IL_030b: Expected I, but got O
		bool flag = _tmpSettingsSortedByConnectionOrder != null;
		IEnumerable<ISetting> enumerable = settings;
		if (!flag)
		{
			enumerable = (_tmpSettingsSortedByConnectionOrder = new List<ISetting>());
		}
		List<ISetting> tmpSettingsSortedByConnectionOrder = _tmpSettingsSortedByConnectionOrder;
		if (_tmpSettingsSortedByConnectionOrder != null)
		{
			int version = tmpSettingsSortedByConnectionOrder._version + 1;
			tmpSettingsSortedByConnectionOrder._version = version;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
			object obj = default(object);
			if (obj == null)
			{
				tmpSettingsSortedByConnectionOrder._size = 0;
			}
			else
			{
				tmpSettingsSortedByConnectionOrder._size = 0;
				if (tmpSettingsSortedByConnectionOrder._size > 0)
				{
					Array.Clear(tmpSettingsSortedByConnectionOrder._items, 0, tmpSettingsSortedByConnectionOrder._size);
					nint num = unchecked((nint)null);
				}
			}
			if (settings != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				List<ISetting> list = default(List<ISetting>);
				object obj2 = (object)(&list);
				List<ISetting> list2 = null;
				object obj3 = default(object);
				object obj12 = default(object);
				object obj13 = default(object);
				List<ISetting> list3 = default(List<ISetting>);
				object obj19 = default(object);
				while (true)
				{
					object obj4;
					object obj9;
					if (list != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
						if (obj3 == null)
						{
							break;
						}
						bool flag2 = list == null;
						list2 = null;
						if (!flag2)
						{
							nint num2 = (nint)list;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v218 @ r10_v7 (Il2CppClass<System.Collections.Generic.List`1<Kamgam.SettingsGenerator.ISetting>>)+12E]");
							if ((nint)0 >= (nint)0)
							{
								goto IL_020f;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v218 @ r10_v7 (Il2CppClass<System.Collections.Generic.List`1<Kamgam.SettingsGenerator.ISetting>>)+B0]");
							obj4 = 0;
							int num3 = 0;
							while (true)
							{
								object obj5 = num3 + num3;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v225 @ r8_v14+v466 @ rax_v42*8]");
								if (0 == (nint)typeof(IEnumerator<ISetting>))
								{
									break;
								}
								num3++;
								int num4 = num3;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v218 @ r10_v7 (Il2CppClass<System.Collections.Generic.List`1<Kamgam.SettingsGenerator.ISetting>>)+12E]");
								if ((nint)num4 < (nint)0)
								{
									continue;
								}
								goto IL_020f;
							}
							object obj6 = num3 + num3;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v225 @ r8_v14+8+v524 @ rcx_v39*8]");
							object obj7 = (nint)0 << 4;
							object obj8 = obj7 + 312;
							obj9 = obj8 + num2;
							goto IL_04e1;
						}
						throw new NullReferenceException();
					}
					throw new NullReferenceException();
					IL_02af:
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
					object obj10 = 6;
					object obj11 = obj12;
					goto IL_0508;
					IL_020f:
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
					obj4 = 0;
					obj9 = obj13;
					goto IL_04e1;
					IL_04e1:
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v529 @ rdx_v16] (should have been resolved before IL gen)");
					if (list3 != null)
					{
						nint num5 = (nint)list3;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v160 @ r10_v8 (Il2CppClass<System.Collections.Generic.List`1<Kamgam.SettingsGenerator.ISetting>>)+12E]");
						if ((nint)0 >= (nint)0)
						{
							goto IL_02af;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v160 @ r10_v8 (Il2CppClass<System.Collections.Generic.List`1<Kamgam.SettingsGenerator.ISetting>>)+B0]");
						obj10 = 0;
						int num6 = 0;
						while (true)
						{
							object obj14 = num6 + num6;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v167 @ r8_v15+v559 @ rax_v37*8]");
							if (0 == (nint)typeof(ISetting))
							{
								break;
							}
							num6++;
							int num7 = num6;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v160 @ r10_v8 (Il2CppClass<System.Collections.Generic.List`1<Kamgam.SettingsGenerator.ISetting>>)+12E]");
							if ((nint)num7 < (nint)0)
							{
								continue;
							}
							goto IL_02af;
						}
						object obj15 = num6 + num6;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v167 @ r8_v15+8+v615 @ rcx_v31*8]");
						object obj16 = (nint)0 + (nint)6;
						object obj17 = obj16 << 4;
						object obj18 = obj17 + 312;
						obj11 = obj18 + num5;
						goto IL_0508;
					}
					throw new NullReferenceException();
					IL_0508:
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v622 @ rdx_v19] (should have been resolved before IL gen)");
					bool flag3 = obj19 == null;
					nint num = (nint)typeof(ISetting);
					if (!flag3)
					{
						if (_tmpSettingsSortedByConnectionOrder == null)
						{
							throw new NullReferenceException();
						}
						_tmpSettingsSortedByConnectionOrder.Add((ISetting)list3);
						num = (nint)typeof(ISetting);
					}
				}
				if (obj2 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				}
				Comparison<ISetting> comparison = compareByConnectionOrder;
				if (_tmpSettingsSortedByConnectionOrder != null)
				{
					_tmpSettingsSortedByConnectionOrder.Sort(comparison);
					return _tmpSettingsSortedByConnectionOrder;
				}
			}
		}
		throw new NullReferenceException();
	}

	protected int compareByConnectionOrder(ISetting a, ISetting b)
	{
		//IL_0056: Expected I4, but got O
		//IL_0043: Expected I4, but got O
		if (a != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			if (b != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				object obj = default(object);
				object obj2 = default(object);
				return obj - obj2;
			}
		}
		NullReferenceException ex = new NullReferenceException();
		return (int)ex;
	}

	protected unsafe List<ISetting> getSettingsOrderedByID(IEnumerable<ISetting> settings)
	{
		//IL_0108: Expected I, but got O
		//IL_013c: Expected O, but got Ref
		//IL_0222: Expected O, but got I4
		//IL_01cf: Expected O, but got I
		//IL_033d: Expected O, but got I4
		//IL_024c: Expected I, but got O
		//IL_025e: Expected O, but got I4
		//IL_0274: Expected O, but got I
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Expected O, but got Unknown
		bool flag = _tmpSettingsSortedByName != null;
		IEnumerable<ISetting> enumerable = settings;
		if (!flag)
		{
			enumerable = (_tmpSettingsSortedByName = new List<ISetting>());
		}
		List<ISetting> tmpSettingsSortedByName = _tmpSettingsSortedByName;
		if (_tmpSettingsSortedByName != null)
		{
			int version = tmpSettingsSortedByName._version + 1;
			tmpSettingsSortedByName._version = version;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
			object obj = default(object);
			if (obj == null)
			{
				tmpSettingsSortedByName._size = 0;
			}
			else
			{
				tmpSettingsSortedByName._size = 0;
				if (tmpSettingsSortedByName._size > 0)
				{
					Array.Clear(tmpSettingsSortedByName._items, 0, tmpSettingsSortedByName._size);
					nint num = unchecked((nint)null);
				}
			}
			if (settings != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				object obj3 = default(object);
				object obj2 = (object)(&obj3);
				List<ISetting> list = null;
				object obj4 = default(object);
				ISetting item = default(ISetting);
				object obj12 = default(object);
				while (true)
				{
					object obj6;
					object obj11;
					if (obj3 != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
						if (obj4 == null)
						{
							break;
						}
						bool flag2 = obj3 == null;
						list = null;
						if (!flag2)
						{
							object obj5 = obj3;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v160 @ r10_v6+12E]");
							if ((nint)0 >= (nint)0)
							{
								goto IL_020f;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v160 @ r10_v6+B0]");
							obj6 = 0;
							int num2 = 0;
							while (true)
							{
								object obj7 = num2 + num2;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v167 @ r8_v13+v412 @ rax_v32*8]");
								if (0 == (nint)typeof(IEnumerator<ISetting>))
								{
									break;
								}
								num2++;
								int num3 = num2;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v160 @ r10_v6+12E]");
								if ((nint)num3 < (nint)0)
								{
									continue;
								}
								goto IL_020f;
							}
							object obj8 = num2 + num2;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v167 @ r8_v13+8+v470 @ rcx_v27*8]");
							object obj9 = (nint)0 << 4;
							object obj10 = obj9 + 312;
							obj11 = obj10 + obj5;
							goto IL_038f;
						}
						throw new NullReferenceException();
					}
					throw new NullReferenceException();
					IL_038f:
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v475 @ rdx_v15] (should have been resolved before IL gen)");
					if (_tmpSettingsSortedByName != null)
					{
						_tmpSettingsSortedByName.Add(item);
						nint num = (nint)typeof(IEnumerator<ISetting>);
						continue;
					}
					throw new NullReferenceException();
					IL_020f:
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
					obj6 = 0;
					obj11 = obj12;
					goto IL_038f;
				}
				if (obj2 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				}
				Comparison<ISetting> comparison = compareByID;
				if (_tmpSettingsSortedByName != null)
				{
					_tmpSettingsSortedByName.Sort(comparison);
					return _tmpSettingsSortedByName;
				}
			}
		}
		throw new NullReferenceException();
	}

	protected int compareByID(ISetting a, ISetting b)
	{
		//IL_0058: Expected I4, but got O
		if (a != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			if (b != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180C3F600");
				int result = default(int);
				return result;
			}
		}
		NullReferenceException ex = new NullReferenceException();
		return (int)ex;
	}

	public bool HasID(string id)
	{
		ISetting setting = GetSetting(id);
		bool flag = setting == null;
		return !flag;
	}

	public bool HasActiveID(string id)
	{
		ISetting setting = GetSetting(id);
		ISetting setting2;
		if (setting != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			object obj = default(object);
			bool flag = obj != null;
			setting2 = setting;
			if (flag)
			{
				goto IL_006c;
			}
		}
		setting2 = null;
		goto IL_006c;
		IL_006c:
		bool flag2 = setting2 == null;
		return !flag2;
	}

	public unsafe ISetting GetSetting(string id)
	{
		//IL_0036: Expected O, but got Ref
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<ISetting>.Enumerator enumerator = default(List<ISetting>.Enumerator);
		ISetting setting = default(ISetting);
		string text = default(string);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				bool flag = setting == null;
				List<ISetting> list = (List<ISetting>)(&enumerator);
				if (flag)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				if (text == id)
				{
					enumerator.Dispose();
					return setting;
				}
				continue;
			}
			enumerator.Dispose();
			return null;
		}
		throw new NullReferenceException();
	}

	public ISetting GetActiveSetting(string id)
	{
		ISetting setting = GetSetting(id);
		if (setting != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			object obj = default(object);
			bool flag = obj != null;
			ISetting result = setting;
			if (!flag)
			{
				result = null;
			}
			return result;
		}
		return setting;
	}

	protected unsafe bool doesOtherSettingExist(string id, SettingData.DataType dataType)
	{
		//IL_01c3: Expected I4, but got O
		//IL_00a1: Expected O, but got Ref
		//IL_011b: Expected O, but got Ref
		ISetting setting = GetSetting(id);
		if (setting != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			object obj = default(object);
			if ((nint)obj != (nint)dataType)
			{
				string[] array = new string[9];
				if (array != null)
				{
					array[0] = "You are trying to create '";
					array[1] = id;
					array[2] = "' (type: '";
					object obj2 = default(object);
					string text = ((Enum)(&obj2)).ToString();
					array[3] = text;
					array[4] = "') but another '";
					array[5] = id;
					array[6] = "' with a DIFFERENT type ('";
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					IntPtr intPtr = default(IntPtr);
					string text2 = ((Enum)(&intPtr)).ToString();
					array[7] = text2;
					array[8] = "') already exists. Aborting creation. Duplicate IDs are not allowed.";
					string message = string.Concat(array);
					Debug.LogError(message);
					return true;
				}
				NullReferenceException ex = new NullReferenceException();
				return (byte)(int)ex != 0;
			}
		}
		return false;
	}

	public ISetting GetOrCreate(string id, SettingData.DataType dataType)
	{
		//IL_0049: Expected O, but got I8
		//IL_0063: Expected O, but got I8
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3F377]");
		if ((nint)0 == 0)
		{
			_ = 1;
			object obj = "";
		}
		while (dataType <= SettingData.DataType.ColorOption)
		{
			object obj2 = 6442450944L;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ rdx_v1+A4CA38+dataType @ r8 (Kamgam.SettingsGenerator.SettingData+DataType)*4]");
			object obj3 = 0 + 6442450944L;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v52 @ rcx_v3 (should have been resolved before IL gen)");
		}
		return null;
	}

	public SettingBool GetOrCreateBool(string id, bool defaultValue = false, List<string> groups = null, IConnection<bool> connection = null, SettingsProvider provider = null)
	{
		SettingBool settingBool = GetBool(id);
		SettingBool result;
		if (settingBool != null)
		{
			bool flag = groups == null;
			result = settingBool;
			if (!flag)
			{
				settingBool.SetGroups(groups);
				result = settingBool;
			}
		}
		else if (!doesOtherSettingExist(id, SettingData.DataType.Bool))
		{
			SettingBool settingBool2 = new SettingBool((SettingData)(object)id, groups);
			settingBool2.SetValue(defaultValue);
			if (_bools == null)
			{
				return (SettingBool)(object)new NullReferenceException();
			}
			_bools.Add(settingBool2);
			RebuildSettingsCache();
			result = settingBool2;
		}
		else
		{
			result = null;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180746870");
		return result;
	}

	protected void initConnectionForSetting<T>(ISettingWithConnection<T> setting, IConnection<T> connection, SettingsProvider provider)
	{
		//IL_01e7: Expected O, but got I
		//IL_00bb: Expected I, but got O
		//IL_015a: Expected O, but got I
		//IL_00f3: Expected O, but got I
		//IL_00fc: Expected O, but got I4
		//IL_021e: Expected O, but got I
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Expected O, but got Unknown
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Expected O, but got Unknown
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Expected O, but got Unknown
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ stack_28+38]");
		if ((nint)0 == 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ stack_28+38]");
			if ((nint)0 == 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4B90");
			}
		}
		if (connection == null)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
		IConnectionWithSettingsAccess connectionWithSettingsAccess = default(IConnectionWithSettingsAccess);
		bool flag = connectionWithSettingsAccess == null;
		IConnection<T> connection2 = connection;
		SettingsProvider settingsProvider = provider;
		object obj8 = default(object);
		if (!flag)
		{
			nint num = (nint)connectionWithSettingsAccess;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v192 @ r10_v3 (Il2CppClass<Kamgam.SettingsGenerator.IConnectionWithSettingsAccess>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0133;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v192 @ r10_v3 (Il2CppClass<Kamgam.SettingsGenerator.IConnectionWithSettingsAccess>)+B0]");
			object obj = 0;
			object obj2 = 0;
			while (true)
			{
				object obj3 = obj2 + obj2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v242 @ r8_v16+v245 @ rax_v37*8]");
				if (0 == (nint)typeof(IConnectionWithSettingsAccess))
				{
					break;
				}
				obj2++;
				object obj4 = obj2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v192 @ r10_v3 (Il2CppClass<Kamgam.SettingsGenerator.IConnectionWithSettingsAccess>)+12E]");
				if ((nint)obj4 < 0)
				{
					continue;
				}
				goto IL_0133;
			}
			object obj5 = obj2 + obj2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v242 @ r8_v16+8+v313 @ rcx_v29*8]");
			object obj6 = (nint)0 << 4;
			object obj7 = obj6 + 312;
			obj8 = obj7 + num;
			goto IL_0142;
		}
		goto IL_023f;
		IL_0142:
		settingsProvider = (SettingsProvider)obj8;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v310 @ rax_v31+8]");
		connection2 = (IConnection<T>)0;
		connectionWithSettingsAccess.SetSettings(this);
		goto IL_023f;
		IL_023f:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
		IConnectionWithProviderAccess connectionWithProviderAccess = default(IConnectionWithProviderAccess);
		if (connectionWithProviderAccess != null)
		{
			bool flag2 = provider == null;
			bool flag3 = !flag2;
			SettingsProvider provider2 = provider;
			if (!flag3)
			{
				SettingsGeneratorSettings orCreate = SettingsGeneratorSettings.GetOrCreate();
				SettingsProvider provider3 = orCreate.Provider;
				provider2 = provider3;
			}
			connectionWithProviderAccess.SetProvider(provider2);
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ stack_28+38]");
		object obj9 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
		return;
		IL_0133:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
		goto IL_0142;
	}

	public unsafe SettingBool GetBool(string id)
	{
		//IL_0036: Expected O, but got Ref
		//IL_0060: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<SettingBool>.Enumerator enumerator = default(List<SettingBool>.Enumerator);
		SettingBool settingBool = default(SettingBool);
		object obj2 = default(object);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				bool flag = settingBool == null;
				List<SettingBool> list = (List<SettingBool>)(&enumerator);
				if (flag)
				{
					break;
				}
				nint num = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v203 @ rcx_v9 (Il2CppClass<Kamgam.SettingsGenerator.SettingWithValue`1<System.Boolean>>)+80]");
				object obj = (nint)0 + (nint)64;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				if ((string)obj2 == id)
				{
					enumerator.Dispose();
					return settingBool;
				}
				continue;
			}
			enumerator.Dispose();
			return null;
		}
		throw new NullReferenceException();
	}

	protected SettingBool addBool(string id, bool value, List<string> groups = null)
	{
		SettingBool settingBool;
		if (!doesOtherSettingExist(id, SettingData.DataType.Bool))
		{
			settingBool = new SettingBool((SettingData)(object)id, groups);
			settingBool.SetValue(value);
			if (_bools == null)
			{
				return (SettingBool)(object)new NullReferenceException();
			}
			_bools.Add(settingBool);
			RebuildSettingsCache();
		}
		else
		{
			settingBool = null;
		}
		return settingBool;
	}

	public SettingBool AddBoolFromSerializedData(SettingData data, List<string> groups = null)
	{
		if (data != null)
		{
			SettingBool settingBool;
			if (!doesOtherSettingExist(data.ID, SettingData.DataType.Bool))
			{
				settingBool = new SettingBool(data, groups);
				if (_bools == null)
				{
					goto IL_0065;
				}
				_bools.Add(settingBool);
				RebuildSettingsCache();
			}
			else
			{
				settingBool = null;
			}
			return settingBool;
		}
		goto IL_0065;
		IL_0065:
		return (SettingBool)(object)new NullReferenceException();
	}

	public unsafe SettingColor GetOrCreateColor(string id, Color defaultValue, List<string> groups = null, IConnection<Color> connection = null, SettingsProvider provider = null)
	{
		//IL_010c: Expected O, but got Ref
		SettingColor color = GetColor(id);
		SettingColor result;
		if (color != null)
		{
			bool flag = groups == null;
			result = color;
			if (!flag)
			{
				color.SetGroups(groups);
				result = color;
			}
		}
		else if (!doesOtherSettingExist(id, SettingData.DataType.Color))
		{
			SettingColor settingColor = new SettingColor((SettingData)(object)id, groups);
			object obj = default(object);
			settingColor.SetValue((Color)(&obj));
			if (_colors == null)
			{
				return (SettingColor)(object)new NullReferenceException();
			}
			_colors.Add(settingColor);
			RebuildSettingsCache();
			result = settingColor;
		}
		else
		{
			result = null;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180746870");
		return result;
	}

	public unsafe SettingColor GetColor(string id)
	{
		//IL_0036: Expected O, but got Ref
		//IL_0060: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<SettingColor>.Enumerator enumerator = default(List<SettingColor>.Enumerator);
		SettingColor settingColor = default(SettingColor);
		object obj2 = default(object);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				bool flag = settingColor == null;
				List<SettingColor> list = (List<SettingColor>)(&enumerator);
				if (flag)
				{
					break;
				}
				nint num = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v203 @ rcx_v9 (Il2CppClass<Kamgam.SettingsGenerator.SettingWithValue`1<UnityEngine.Color>>)+80]");
				object obj = (nint)0 + (nint)64;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				if ((string)obj2 == id)
				{
					enumerator.Dispose();
					return settingColor;
				}
				continue;
			}
			enumerator.Dispose();
			return null;
		}
		throw new NullReferenceException();
	}

	protected unsafe SettingColor addColor(string id, Color value, List<string> groups = null)
	{
		//IL_0081: Expected O, but got Ref
		SettingColor settingColor;
		if (!doesOtherSettingExist(id, SettingData.DataType.Color))
		{
			settingColor = new SettingColor((SettingData)(object)id, groups);
			object obj = default(object);
			settingColor.SetValue((Color)(&obj));
			if (_colors == null)
			{
				return (SettingColor)(object)new NullReferenceException();
			}
			_colors.Add(settingColor);
			RebuildSettingsCache();
		}
		else
		{
			settingColor = null;
		}
		return settingColor;
	}

	public SettingColor AddColorFromSerializedData(SettingData data, List<string> groups = null)
	{
		if (data != null)
		{
			SettingColor settingColor;
			if (!doesOtherSettingExist(data.ID, SettingData.DataType.Color))
			{
				settingColor = new SettingColor(data, groups);
				if (_colors == null)
				{
					goto IL_0065;
				}
				_colors.Add(settingColor);
				RebuildSettingsCache();
			}
			else
			{
				settingColor = null;
			}
			return settingColor;
		}
		goto IL_0065;
		IL_0065:
		return (SettingColor)(object)new NullReferenceException();
	}

	public SettingColorOption GetOrCreateColorOption(string id, int defaultOption = 0, List<string> groups = null, List<Color> options = null, IConnectionWithOptions<Color> connection = null, SettingsProvider provider = null)
	{
		SettingColorOption colorOption = GetColorOption(id);
		IEnumerable<Color> enumerable = default(IEnumerable<Color>);
		SettingColorOption result;
		if (colorOption != null)
		{
			if (groups != null && groups._size > 0)
			{
				colorOption.SetGroups(groups);
			}
			bool flag = enumerable == null;
			result = colorOption;
			if (!flag)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v113 @ stack_28 (System.Collections.Generic.IEnumerable`1<UnityEngine.Color>)+18]");
				bool flag2 = (nint)0 <= (nint)0;
				result = colorOption;
				if (!flag2)
				{
					colorOption.ClearOptions();
					colorOption.AddOptions(enumerable);
					colorOption._overrideConnectionLabels = colorOption._overrideConnectionLabels;
					if (colorOption.HasConnection())
					{
						if (colorOption.Connection == null)
						{
							goto IL_01e2;
						}
						if (!colorOption._overrideConnectionLabels)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
							if (colorOption.Connection == null)
							{
								goto IL_01e2;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
							List<Color> options2 = default(List<Color>);
							colorOption._options = options2;
						}
						else
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
						}
						((SettingWithValue<int>)colorOption).invokePulledFromConnectionListeners();
					}
					RefreshRegisteredResolvers(id);
					result = colorOption;
				}
			}
		}
		else if (!doesOtherSettingExist(id, SettingData.DataType.ColorOption))
		{
			SettingColorOption settingColorOption = (SettingColorOption)new SettingWithValue<int>(id, groups);
			settingColorOption.SetValue(defaultOption);
			settingColorOption._options = (List<Color>)enumerable;
			if (_colorOptions == null)
			{
				goto IL_01e2;
			}
			_colorOptions.Add(settingColorOption);
			RebuildSettingsCache();
			result = settingColorOption;
		}
		else
		{
			result = null;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180746870");
		return result;
		IL_01e2:
		return (SettingColorOption)(object)new NullReferenceException();
	}

	public unsafe SettingColorOption GetColorOption(string id)
	{
		//IL_0036: Expected O, but got Ref
		//IL_0060: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<SettingColorOption>.Enumerator enumerator = default(List<SettingColorOption>.Enumerator);
		SettingColorOption settingColorOption = default(SettingColorOption);
		object obj2 = default(object);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				bool flag = settingColorOption == null;
				List<SettingColorOption> list = (List<SettingColorOption>)(&enumerator);
				if (flag)
				{
					break;
				}
				nint num = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v203 @ rcx_v9 (Il2CppClass<Kamgam.SettingsGenerator.SettingWithValue`1<System.Int32>>)+80]");
				object obj = (nint)0 + (nint)64;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				if ((string)obj2 == id)
				{
					enumerator.Dispose();
					return settingColorOption;
				}
				continue;
			}
			enumerator.Dispose();
			return null;
		}
		throw new NullReferenceException();
	}

	protected SettingColorOption addColorOption(string id, int selectedIndex, List<string> groups = null, List<Color> options = null)
	{
		SettingColorOption settingColorOption;
		if (!doesOtherSettingExist(id, SettingData.DataType.ColorOption))
		{
			settingColorOption = (SettingColorOption)new SettingWithValue<int>(id, groups);
			settingColorOption.SetValue(selectedIndex);
			List<Color> options2 = default(List<Color>);
			settingColorOption._options = options2;
			if (_colorOptions == null)
			{
				return (SettingColorOption)(object)new NullReferenceException();
			}
			_colorOptions.Add(settingColorOption);
			RebuildSettingsCache();
		}
		else
		{
			settingColorOption = null;
		}
		return settingColorOption;
	}

	public SettingColorOption AddColorOptionFromSerializedData(SettingData data, List<string> groups = null, List<Color> options = null)
	{
		if (data != null)
		{
			SettingColorOption settingColorOption;
			if (!doesOtherSettingExist(data.ID, SettingData.DataType.ColorOption))
			{
				settingColorOption = (SettingColorOption)new SettingWithValue<int>(data, groups);
				settingColorOption._options = options;
				if (_colorOptions == null)
				{
					goto IL_0065;
				}
				_colorOptions.Add(settingColorOption);
				RebuildSettingsCache();
			}
			else
			{
				settingColorOption = null;
			}
			return settingColorOption;
		}
		goto IL_0065;
		IL_0065:
		return (SettingColorOption)(object)new NullReferenceException();
	}

	public SettingFloat GetOrCreateFloat(string id, float defaultValue = 0f, List<string> groups = null, IConnection<float> connection = null, SettingsProvider provider = null)
	{
		SettingFloat settingFloat = GetFloat(id);
		SettingFloat result;
		if (settingFloat != null)
		{
			bool flag = groups == null;
			result = settingFloat;
			if (!flag)
			{
				settingFloat.SetGroups(groups);
				result = settingFloat;
			}
		}
		else if (!doesOtherSettingExist(id, SettingData.DataType.Float))
		{
			SettingFloat settingFloat2 = new SettingFloat((SettingData)(object)id, groups);
			settingFloat2.SetValue(defaultValue);
			if (_floats == null)
			{
				return (SettingFloat)(object)new NullReferenceException();
			}
			_floats.Add(settingFloat2);
			RebuildSettingsCache();
			result = settingFloat2;
		}
		else
		{
			result = null;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180746870");
		return result;
	}

	public unsafe SettingFloat GetFloat(string id)
	{
		//IL_0036: Expected O, but got Ref
		//IL_0060: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<SettingFloat>.Enumerator enumerator = default(List<SettingFloat>.Enumerator);
		SettingFloat settingFloat = default(SettingFloat);
		object obj2 = default(object);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				bool flag = settingFloat == null;
				List<SettingFloat> list = (List<SettingFloat>)(&enumerator);
				if (flag)
				{
					break;
				}
				nint num = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v203 @ rcx_v9 (Il2CppClass<Kamgam.SettingsGenerator.SettingWithValue`1<System.Single>>)+80]");
				object obj = (nint)0 + (nint)64;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				if ((string)obj2 == id)
				{
					enumerator.Dispose();
					return settingFloat;
				}
				continue;
			}
			enumerator.Dispose();
			return null;
		}
		throw new NullReferenceException();
	}

	protected SettingFloat addFloat(string id, float value, List<string> groups = null)
	{
		SettingFloat settingFloat;
		if (!doesOtherSettingExist(id, SettingData.DataType.Float))
		{
			settingFloat = new SettingFloat((SettingData)(object)id, groups);
			settingFloat.SetValue(value);
			if (_floats == null)
			{
				return (SettingFloat)(object)new NullReferenceException();
			}
			_floats.Add(settingFloat);
			RebuildSettingsCache();
		}
		else
		{
			settingFloat = null;
		}
		return settingFloat;
	}

	public SettingFloat AddFloatFromSerializedData(SettingData data, List<string> groups = null)
	{
		if (data != null)
		{
			SettingFloat settingFloat;
			if (!doesOtherSettingExist(data.ID, SettingData.DataType.Float))
			{
				settingFloat = new SettingFloat(data, groups);
				if (_floats == null)
				{
					goto IL_0065;
				}
				_floats.Add(settingFloat);
				RebuildSettingsCache();
			}
			else
			{
				settingFloat = null;
			}
			return settingFloat;
		}
		goto IL_0065;
		IL_0065:
		return (SettingFloat)(object)new NullReferenceException();
	}

	public SettingInt GetOrCreateInt(string id, int defaultValue = 0, List<string> groups = null, IConnection<int> connection = null, SettingsProvider provider = null)
	{
		SettingInt settingInt = GetInt(id);
		SettingInt result;
		if (settingInt != null)
		{
			bool flag = groups == null;
			result = settingInt;
			if (!flag)
			{
				settingInt.SetGroups(groups);
				result = settingInt;
			}
		}
		else if (!doesOtherSettingExist(id, SettingData.DataType.Int))
		{
			SettingInt settingInt2 = new SettingInt((SettingData)(object)id, groups);
			settingInt2.SetValue(defaultValue);
			if (_integers == null)
			{
				return (SettingInt)(object)new NullReferenceException();
			}
			_integers.Add(settingInt2);
			RebuildSettingsCache();
			result = settingInt2;
		}
		else
		{
			result = null;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180746870");
		return result;
	}

	public unsafe SettingInt GetInt(string id)
	{
		//IL_0036: Expected O, but got Ref
		//IL_0060: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<SettingInt>.Enumerator enumerator = default(List<SettingInt>.Enumerator);
		SettingInt settingInt = default(SettingInt);
		object obj2 = default(object);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				bool flag = settingInt == null;
				List<SettingInt> list = (List<SettingInt>)(&enumerator);
				if (flag)
				{
					break;
				}
				nint num = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v203 @ rcx_v9 (Il2CppClass<Kamgam.SettingsGenerator.SettingWithValue`1<System.Int32>>)+80]");
				object obj = (nint)0 + (nint)64;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				if ((string)obj2 == id)
				{
					enumerator.Dispose();
					return settingInt;
				}
				continue;
			}
			enumerator.Dispose();
			return null;
		}
		throw new NullReferenceException();
	}

	protected SettingInt addInt(string id, int value, List<string> groups = null)
	{
		SettingInt settingInt;
		if (!doesOtherSettingExist(id, SettingData.DataType.Int))
		{
			settingInt = new SettingInt((SettingData)(object)id, groups);
			settingInt.SetValue(value);
			if (_integers == null)
			{
				return (SettingInt)(object)new NullReferenceException();
			}
			_integers.Add(settingInt);
			RebuildSettingsCache();
		}
		else
		{
			settingInt = null;
		}
		return settingInt;
	}

	public SettingInt AddIntFromSerializedData(SettingData data, List<string> groups = null)
	{
		if (data != null)
		{
			SettingInt settingInt;
			if (!doesOtherSettingExist(data.ID, SettingData.DataType.Int))
			{
				settingInt = new SettingInt(data, groups);
				if (_integers == null)
				{
					goto IL_0065;
				}
				_integers.Add(settingInt);
				RebuildSettingsCache();
			}
			else
			{
				settingInt = null;
			}
			return settingInt;
		}
		goto IL_0065;
		IL_0065:
		return (SettingInt)(object)new NullReferenceException();
	}

	public SettingKeyCombination GetOrCreateKeyCombination(string id, KeyCombination defaultValue, List<string> groups = null, IConnection<KeyCombination> connection = null, SettingsProvider provider = null)
	{
		SettingKeyCombination keyCombination = GetKeyCombination(id);
		SettingKeyCombination result;
		if (keyCombination != null)
		{
			bool flag = groups == null;
			result = keyCombination;
			if (!flag)
			{
				keyCombination.SetGroups(groups);
				result = keyCombination;
			}
		}
		else if (!doesOtherSettingExist(id, SettingData.DataType.KeyCombination))
		{
			SettingKeyCombination settingKeyCombination = new SettingKeyCombination((SettingData)(object)id, groups);
			settingKeyCombination.SetDefault(defaultValue);
			settingKeyCombination.SetValue(defaultValue);
			if (_keyCombinations == null)
			{
				return (SettingKeyCombination)(object)new NullReferenceException();
			}
			_keyCombinations.Add(settingKeyCombination);
			RebuildSettingsCache();
			result = settingKeyCombination;
		}
		else
		{
			result = null;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180746870");
		return result;
	}

	protected SettingKeyCombination addKeyCombination(string id, KeyCombination value, List<string> groups = null)
	{
		SettingKeyCombination settingKeyCombination;
		if (!doesOtherSettingExist(id, SettingData.DataType.KeyCombination))
		{
			settingKeyCombination = new SettingKeyCombination((SettingData)(object)id, groups);
			settingKeyCombination.SetDefault(value);
			settingKeyCombination.SetValue(value);
			if (_keyCombinations == null)
			{
				return (SettingKeyCombination)(object)new NullReferenceException();
			}
			_keyCombinations.Add(settingKeyCombination);
			RebuildSettingsCache();
		}
		else
		{
			settingKeyCombination = null;
		}
		return settingKeyCombination;
	}

	public SettingKeyCombination AddKeyCombinationFromSerializedData(SettingData data, List<string> groups = null)
	{
		if (data != null)
		{
			SettingKeyCombination settingKeyCombination;
			if (!doesOtherSettingExist(data.ID, SettingData.DataType.KeyCombination))
			{
				settingKeyCombination = new SettingKeyCombination(data, groups);
				if (_keyCombinations == null)
				{
					goto IL_0065;
				}
				_keyCombinations.Add(settingKeyCombination);
				RebuildSettingsCache();
			}
			else
			{
				settingKeyCombination = null;
			}
			return settingKeyCombination;
		}
		goto IL_0065;
		IL_0065:
		return (SettingKeyCombination)(object)new NullReferenceException();
	}

	public unsafe SettingKeyCombination GetKeyCombination(string id)
	{
		//IL_0036: Expected O, but got Ref
		//IL_0060: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<SettingKeyCombination>.Enumerator enumerator = default(List<SettingKeyCombination>.Enumerator);
		SettingKeyCombination settingKeyCombination = default(SettingKeyCombination);
		object obj2 = default(object);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				bool flag = settingKeyCombination == null;
				List<SettingKeyCombination> list = (List<SettingKeyCombination>)(&enumerator);
				if (flag)
				{
					break;
				}
				nint num = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v203 @ rcx_v9 (Il2CppClass<Kamgam.SettingsGenerator.SettingWithValue`1<Kamgam.SettingsGenerator.KeyCombination>>)+80]");
				object obj = (nint)0 + (nint)64;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				if ((string)obj2 == id)
				{
					enumerator.Dispose();
					return settingKeyCombination;
				}
				continue;
			}
			enumerator.Dispose();
			return null;
		}
		throw new NullReferenceException();
	}

	public SettingOption GetOrCreateOption(string id, int defaultOption = 0, List<string> groups = null, List<string> options = null, IConnectionWithOptions<string> connection = null, SettingsProvider provider = null)
	{
		SettingOption option = GetOption(id);
		IEnumerable<string> enumerable = default(IEnumerable<string>);
		SettingOption result;
		if (option != null)
		{
			if (groups != null && groups._size > 0)
			{
				option.SetGroups(groups);
			}
			bool flag = enumerable == null;
			result = option;
			if (!flag)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v111 @ stack_28 (System.Collections.Generic.IEnumerable`1<System.String>)+18]");
				bool flag2 = (nint)0 <= (nint)0;
				result = option;
				if (!flag2)
				{
					if (enumerable != option._optionLabels)
					{
						option.ClearOptionLabels();
					}
					option.AddOptionLabels(enumerable);
					option._overrideConnectionLabels = option._overrideConnectionLabels;
					if (option.HasConnection())
					{
						if (option.Connection == null)
						{
							goto IL_0203;
						}
						if (!option._overrideConnectionLabels)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
							if (option.Connection == null)
							{
								goto IL_0203;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
							List<string> optionLabels = default(List<string>);
							option._optionLabels = optionLabels;
						}
						else
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
						}
						((SettingWithValue<int>)option).invokePulledFromConnectionListeners();
					}
					RefreshRegisteredResolvers(id);
					result = option;
				}
			}
		}
		else if (!doesOtherSettingExist(id, SettingData.DataType.Option))
		{
			SettingOption settingOption = (SettingOption)new SettingWithValue<int>(id, groups);
			settingOption.SetValue(defaultOption);
			settingOption._optionLabels = (List<string>)enumerable;
			if (_options == null)
			{
				goto IL_0203;
			}
			_options.Add(settingOption);
			RebuildSettingsCache();
			result = settingOption;
		}
		else
		{
			result = null;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180746870");
		return result;
		IL_0203:
		return (SettingOption)(object)new NullReferenceException();
	}

	public unsafe SettingOption GetOption(string id)
	{
		//IL_0036: Expected O, but got Ref
		//IL_0060: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<SettingOption>.Enumerator enumerator = default(List<SettingOption>.Enumerator);
		SettingOption settingOption = default(SettingOption);
		object obj2 = default(object);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				bool flag = settingOption == null;
				List<SettingOption> list = (List<SettingOption>)(&enumerator);
				if (flag)
				{
					break;
				}
				nint num = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v203 @ rcx_v9 (Il2CppClass<Kamgam.SettingsGenerator.SettingWithValue`1<System.Int32>>)+80]");
				object obj = (nint)0 + (nint)64;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				if ((string)obj2 == id)
				{
					enumerator.Dispose();
					return settingOption;
				}
				continue;
			}
			enumerator.Dispose();
			return null;
		}
		throw new NullReferenceException();
	}

	protected SettingOption addOption(string id, int selectedIndex, List<string> groups = null, List<string> options = null)
	{
		SettingOption settingOption;
		if (!doesOtherSettingExist(id, SettingData.DataType.Option))
		{
			settingOption = (SettingOption)new SettingWithValue<int>(id, groups);
			settingOption.SetValue(selectedIndex);
			List<string> optionLabels = default(List<string>);
			settingOption._optionLabels = optionLabels;
			if (_options == null)
			{
				return (SettingOption)(object)new NullReferenceException();
			}
			_options.Add(settingOption);
			RebuildSettingsCache();
		}
		else
		{
			settingOption = null;
		}
		return settingOption;
	}

	public SettingOption AddOptionFromSerializedData(SettingData data, List<string> groups = null, List<string> options = null)
	{
		if (data != null)
		{
			SettingOption settingOption;
			if (!doesOtherSettingExist(data.ID, SettingData.DataType.Option))
			{
				settingOption = (SettingOption)new SettingWithValue<int>(data, groups);
				settingOption._optionLabels = options;
				if (_options == null)
				{
					goto IL_0065;
				}
				_options.Add(settingOption);
				RebuildSettingsCache();
			}
			else
			{
				settingOption = null;
			}
			return settingOption;
		}
		goto IL_0065;
		IL_0065:
		return (SettingOption)(object)new NullReferenceException();
	}

	public SettingString GetOrCreateString(string id, string defaultValue = "", List<string> groups = null, IConnection<string> connection = null, SettingsProvider provider = null)
	{
		SettingString settingString = GetString(id);
		SettingString result;
		if (settingString != null)
		{
			bool flag = groups == null;
			result = settingString;
			if (!flag)
			{
				settingString.SetGroups(groups);
				result = settingString;
			}
		}
		else if (!doesOtherSettingExist(id, SettingData.DataType.String))
		{
			SettingString settingString2 = new SettingString((SettingData)(object)id, groups);
			settingString2.SetValue(defaultValue);
			if (_strings == null)
			{
				return (SettingString)(object)new NullReferenceException();
			}
			_strings.Add(settingString2);
			RebuildSettingsCache();
			result = settingString2;
		}
		else
		{
			result = null;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180746870");
		return result;
	}

	public unsafe SettingString GetString(string id)
	{
		//IL_0036: Expected O, but got Ref
		//IL_0060: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<SettingString>.Enumerator enumerator = default(List<SettingString>.Enumerator);
		SettingString settingString = default(SettingString);
		object obj2 = default(object);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				bool flag = settingString == null;
				List<SettingString> list = (List<SettingString>)(&enumerator);
				if (flag)
				{
					break;
				}
				nint num = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v203 @ rcx_v9 (Il2CppClass<Kamgam.SettingsGenerator.SettingWithValue`1<System.String>>)+80]");
				object obj = (nint)0 + (nint)64;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				if ((string)obj2 == id)
				{
					enumerator.Dispose();
					return settingString;
				}
				continue;
			}
			enumerator.Dispose();
			return null;
		}
		throw new NullReferenceException();
	}

	protected SettingString addString(string id, string value, List<string> groups = null)
	{
		SettingString settingString;
		if (!doesOtherSettingExist(id, SettingData.DataType.String))
		{
			settingString = new SettingString((SettingData)(object)id, groups);
			settingString.SetValue(value);
			if (_strings == null)
			{
				return (SettingString)(object)new NullReferenceException();
			}
			_strings.Add(settingString);
			RebuildSettingsCache();
		}
		else
		{
			settingString = null;
		}
		return settingString;
	}

	public SettingString AddStringFromSerializedData(SettingData data, List<string> groups = null)
	{
		if (data != null)
		{
			SettingString settingString;
			if (!doesOtherSettingExist(data.ID, SettingData.DataType.String))
			{
				settingString = new SettingString(data, groups);
				if (_strings == null)
				{
					goto IL_0065;
				}
				_strings.Add(settingString);
				RebuildSettingsCache();
			}
			else
			{
				settingString = null;
			}
			return settingString;
		}
		goto IL_0065;
		IL_0065:
		return (SettingString)(object)new NullReferenceException();
	}

	public object GetValue(string id)
	{
		object setting = GetSetting(id);
		if (setting == null)
		{
			return setting;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
		object result = default(object);
		return result;
	}

	public unsafe T GetValue<T>(string id)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0086: Expected O, but got I
		//IL_00a4: Expected O, but got I
		//IL_03aa: Expected O, but got I
		//IL_0359: Expected O, but got Ref
		//IL_00f0: Expected O, but got I
		//IL_036e: Expected O, but got I
		//IL_019c: Expected O, but got I
		//IL_01b2: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v237 @ r9_v1+38]");
		if ((nint)0 == 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v237 @ r9_v1+38]");
			if ((nint)0 == 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4B90");
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v237 @ r9_v1+38]");
		object obj3 = 0;
		object obj4 = obj3;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v70 @ rcx_v2+FC]");
		object obj5 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v70 @ rcx_v2+FC]");
		object value = default(object);
		if ((nint)obj5 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v70 @ rcx_v2+FC]");
			object obj6 = (nint)0 + (nint)15;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v70 @ rcx_v2+FC]");
			if ((nint)obj6 <= 0)
			{
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
			value = GetValue(id);
		}
		if (value == null)
		{
			goto IL_0338;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v237 @ r9_v1+38]");
		object obj7 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
		object obj8 = default(object);
		if (obj8 == null)
		{
			string[] array = new string[9];
			if (array != null)
			{
				array[0] = "SGSettings: The value for id '";
				array[1] = id;
				array[2] = "' could not be read because of a type mismatch.\nThe type you requested (";
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v237 @ r9_v1+38]");
				object obj9 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v540 @ rbx_v7+8]");
				Type typeFromHandle = Type.GetTypeFromHandle((RuntimeTypeHandle)0);
				if ((object)typeFromHandle != null)
				{
					string text = typeFromHandle.Name;
					if (text != null)
					{
						string text2 = text.Replace("Single", "Float");
						array[3] = text2;
						array[4] = ") does not match the '";
						array[5] = id;
						array[6] = "' field in Settings (";
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_vm_object_is_inst\"");
						object obj10 = default(object);
						if (obj10 != null)
						{
							object obj11 = obj10;
							Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v566 @ rdx_v26+1B8] (should have been resolved before IL gen)");
							string text3 = default(string);
							if (text3 != null)
							{
								string text4 = text3.Replace("Single", "Float");
								array[7] = text4;
								array[8] = ").\nYou may also get an ArgumentException if you try to set this value.";
								string message = string.Concat(array);
								Debug.LogError(message);
								goto IL_0338;
							}
						}
					}
				}
			}
			return (T)new NullReferenceException();
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v237 @ r9_v1+38]");
		object obj12 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A67B0");
		object obj14 = default(object);
		object obj13 = obj14;
		goto IL_03fe;
		IL_0338:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		obj13 = (object)(&obj2);
		goto IL_03fe;
		IL_03fe:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		T result = default(T);
		return result;
	}

	public void SetValue(string id, object value)
	{
		GetSetting(id)?.SetValueFromObject(value);
	}

	public void SetActive(string id, bool active)
	{
		ISetting setting = GetSetting(id);
		if (setting != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18004DF40");
		}
	}

	public void SetAllActive(bool active)
	{
		//IL_007c: Expected O, but got I
		//IL_0085: Expected O, but got I4
		//IL_00f6: Expected O, but got I
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Expected O, but got Unknown
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Expected O, but got Unknown
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<ISetting>.Enumerator enumerator = default(List<ISetting>.Enumerator);
		object obj = default(object);
		object obj12 = default(object);
		while (true)
		{
			object obj11;
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (obj == null)
				{
					break;
				}
				object obj2 = obj;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v123 @ r10_v3+12E]");
				if ((nint)0 < (nint)0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v123 @ r10_v3+B0]");
					object obj3 = 0;
					object obj4 = 0;
					while (true)
					{
						object obj5 = obj4 + obj4;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v232 @ r8_v10+v237 @ rcx_v14*8]");
						if (0 == (nint)typeof(ISetting))
						{
							break;
						}
						obj4++;
						object obj6 = obj4;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v123 @ r10_v3+12E]");
						if ((nint)obj6 < 0)
						{
							continue;
						}
						goto IL_00bc;
					}
					object obj7 = obj4 + obj4;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v232 @ r8_v10+8+v291 @ rcx_v16*8]");
					object obj8 = (nint)0 + (nint)7;
					object obj9 = obj8 << 4;
					object obj10 = obj9 + 312;
					obj11 = obj10 + obj2;
					goto IL_01a3;
				}
				goto IL_00bc;
			}
			enumerator.Dispose();
			return;
			IL_00bc:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			obj11 = obj12;
			goto IL_01a3;
			IL_01a3:
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v299 @ r8_v5] (should have been resolved before IL gen)");
		}
		throw new NullReferenceException();
	}

	public void OnQualityChanged(int qualityLevel, bool excludeChanged = false)
	{
		//IL_00ea: Expected O, but got I4
		//IL_00f3: Expected O, but got I4
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Expected O, but got Unknown
		List<ISetting> settingsOrderedByConnectionOrderASC = getSettingsOrderedByConnectionOrderASC(_settingsCache);
		object obj = 0;
		ISetting setting = default(ISetting);
		object obj3 = default(object);
		for (object obj2 = 0; (nint)obj2 < settingsOrderedByConnectionOrderASC._size; obj++, obj2 = obj)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			if (!setting.IsActive)
			{
				continue;
			}
			if (excludeChanged)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				if (obj3 != null)
				{
					continue;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180007350");
		}
	}

	public unsafe string[] GetSettingIDsOrderedByName([Optional][DefaultParameterValue(false)] bool filterByDataType, SettingData.DataType[] dataTypes)
	{
		_003C_003Ec__DisplayClass95_0 CS_0024_003C_003E8__locals5 = new _003C_003Ec__DisplayClass95_0();
		if (CS_0024_003C_003E8__locals5 != null)
		{
			CS_0024_003C_003E8__locals5.filterByDataType = filterByDataType;
			CS_0024_003C_003E8__locals5.dataTypes = dataTypes;
			List<ISetting> settingsOrderedByID = getSettingsOrderedByID(_settingsCache);
			Func<ISetting, bool> predicate = delegate(ISetting s)
			{
				//IL_005d: Expected I4, but got O
				if (!CS_0024_003C_003E8__locals5.filterByDataType)
				{
					return true;
				}
				if (s == null)
				{
					NullReferenceException ex = new NullReferenceException();
					return (byte)(int)ex != 0;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				object obj = default(object);
				return Enumerable.Contains(CS_0024_003C_003E8__locals5.dataTypes, (SettingData.DataType)(int)(&obj));
			};
			IEnumerable<ISetting> source = Enumerable.Where(settingsOrderedByID, predicate);
			Func<ISetting, string> selector = _003C_003Ec._003C_003E9__95_1;
			if (_003C_003Ec._003C_003E9__95_1 == null)
			{
				selector = (_003C_003Ec._003C_003E9__95_1 = delegate(ISetting s)
				{
					if (s != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
						string result = default(string);
						return result;
					}
					return (string)(object)new NullReferenceException();
				});
			}
			IEnumerable<string> source2 = Enumerable.Select(source, selector);
			return Enumerable.ToArray(source2);
		}
		return (string[])(object)new NullReferenceException();
	}

	public IList<TSetting> GetSettingsWithConnectionByType<TSetting, TConnection>(IList<TSetting> results = null) where TSetting : class where TConnection : class
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ r8 (Il2CppMethodInfo)+38]");
		if ((nint)0 == 0)
		{
		}
		IList<TSetting> list;
		if (results != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			list = results;
		}
		else
		{
			IList<TSetting> list2 = new List<TSetting>();
			list = list2;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<ISetting>.Enumerator enumerator = default(List<ISetting>.Enumerator);
		IntPtr intPtr = default(IntPtr);
		object obj = default(object);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				if (intPtr == (IntPtr)0)
				{
					continue;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				if (obj != null)
				{
					if (list == null)
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
				}
				continue;
			}
			enumerator.Dispose();
			return list;
		}
		throw new NullReferenceException();
	}

	public TSetting GetFirstSettingWithConnectionByType<TSetting, TConnection>() where TSetting : class where TConnection : class
	{
		//IL_00bc: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ rdx (Il2CppMethodInfo)+38]");
		if ((nint)0 == 0)
		{
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<ISetting>.Enumerator enumerator = default(List<ISetting>.Enumerator);
		IntPtr intPtr = default(IntPtr);
		object obj = default(object);
		while (enumerator.MoveNext())
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
			if (intPtr != (IntPtr)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				if (obj != null)
				{
					enumerator.Dispose();
					return (TSetting)(nint)intPtr;
				}
			}
		}
		enumerator.Dispose();
		return null;
	}

	public TConnection GetFirstConnectionByType<TConnection>() where TConnection : class
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ rdx (Il2CppMethodInfo)+38]");
		if ((nint)0 == 0)
		{
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<ISetting>.Enumerator enumerator = default(List<ISetting>.Enumerator);
		IntPtr intPtr = default(IntPtr);
		object obj = default(object);
		TConnection val = default(TConnection);
		while (enumerator.MoveNext())
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
			if (intPtr == (IntPtr)0)
			{
				continue;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			if (obj != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				if (val != null)
				{
					enumerator.Dispose();
					return val;
				}
			}
		}
		enumerator.Dispose();
		return null;
	}

	public IList<TConnection> GetConnectionsByType<TConnection>(IList<TConnection> results = null) where TConnection : class
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ r8 (Il2CppMethodInfo)+38]");
		if ((nint)0 == 0)
		{
		}
		IList<TConnection> list;
		if (results != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			list = results;
		}
		else
		{
			IList<TConnection> list2 = new List<TConnection>();
			list = list2;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<ISetting>.Enumerator enumerator = default(List<ISetting>.Enumerator);
		IntPtr intPtr = default(IntPtr);
		object obj = default(object);
		object obj2 = default(object);
		object obj3 = default(object);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (intPtr == (IntPtr)0)
				{
					continue;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				if (obj == null)
				{
					continue;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				if (obj2 != null)
				{
					if (list == null)
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
					bool flag = obj3 != null;
					object obj4 = obj2;
					if (!flag)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
						obj4 = obj2;
					}
				}
				continue;
			}
			enumerator.Dispose();
			return list;
		}
		throw new NullReferenceException();
	}

	public IList<TSetting> GetSettingsWithConnection<TSetting>(IConnection connection, IList<TSetting> results = null) where TSetting : class
	{
		//IL_0110: Expected O, but got I4
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v166 @ r9_v2 (Il2CppMethodInfo)+38]");
		if ((nint)0 == 0)
		{
		}
		IList<TSetting> list;
		if (results != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			list = results;
		}
		else
		{
			IList<TSetting> list2 = new List<TSetting>();
			list = list2;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<ISetting>.Enumerator enumerator = default(List<ISetting>.Enumerator);
		IntPtr intPtr = default(IntPtr);
		object obj = default(object);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				if (intPtr == (IntPtr)0)
				{
					continue;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				if (obj == connection)
				{
					bool flag = list == null;
					List<ISetting> list3 = (List<ISetting>)33;
					if (flag)
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
				}
				continue;
			}
			enumerator.Dispose();
			return list;
		}
		throw new NullReferenceException();
	}

	public unsafe IList<ISetting> GetSettingsWithConnection(IConnection connection, IList<ISetting> results = null)
	{
		//IL_006e: Expected O, but got Ref
		//IL_0084: Expected I, but got O
		//IL_00c5: Expected O, but got I4
		//IL_0173: Expected O, but got I
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Expected O, but got Unknown
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Expected O, but got Unknown
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Expected O, but got Unknown
		//IL_014b: Expected I, but got O
		List<ISetting> list;
		if (results != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			list = (List<ISetting>)results;
		}
		else
		{
			List<ISetting> list2 = new List<ISetting>();
			list = list2;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		nint num = 0;
		List<ISetting>.Enumerator enumerator = default(List<ISetting>.Enumerator);
		List<ISetting> list3 = default(List<ISetting>);
		object obj9 = default(object);
		object obj10 = default(object);
		while (true)
		{
			object obj8;
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				bool flag = list3 == null;
				List<ISetting> list4 = (List<ISetting>)(&enumerator);
				if (!flag)
				{
					nint num2 = (nint)list3;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v107 @ r10_v4 (Il2CppClass<System.Collections.Generic.List`1<Kamgam.SettingsGenerator.ISetting>>)+12E]");
					if ((nint)0 >= (nint)0)
					{
						goto IL_00fc;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v107 @ r10_v4 (Il2CppClass<System.Collections.Generic.List`1<Kamgam.SettingsGenerator.ISetting>>)+B0]");
					num = 0;
					object obj = 0;
					while (true)
					{
						object obj2 = obj + obj;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v166 @ r8_v5 (Il2CppMethodInfo)+v313 @ rax_v24*8]");
						if (0 == (nint)typeof(ISetting))
						{
							break;
						}
						obj++;
						object obj3 = obj;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v107 @ r10_v4 (Il2CppClass<System.Collections.Generic.List`1<Kamgam.SettingsGenerator.ISetting>>)+12E]");
						if ((nint)obj3 < 0)
						{
							continue;
						}
						goto IL_00fc;
					}
					object obj4 = obj + obj;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v166 @ r8_v5 (Il2CppMethodInfo)+8+v369 @ rcx_v18*8]");
					object obj5 = (nint)0 + (nint)33;
					object obj6 = obj5 << 4;
					object obj7 = obj6 + 312;
					obj8 = obj7 + num2;
					goto IL_0249;
				}
				throw new NullReferenceException();
			}
			enumerator.Dispose();
			return list;
			IL_0249:
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v376 @ rdx_v10] (should have been resolved before IL gen)");
			if (obj9 == connection)
			{
				if (list == null)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
				num = (nint)list;
			}
			continue;
			IL_00fc:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			obj8 = obj10;
			num = 33;
			goto IL_0249;
		}
		throw new NullReferenceException();
	}

	public unsafe ISetting GetFirstSettingWithConnectionSO(ConnectionSO connectionSO)
	{
		//IL_0036: Expected O, but got Ref
		//IL_004c: Expected I, but got O
		//IL_00df: Expected O, but got I4
		//IL_0084: Expected O, but got I
		//IL_008d: Expected O, but got I4
		//IL_0148: Expected O, but got I
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Expected O, but got Unknown
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Expected O, but got Unknown
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Expected O, but got Unknown
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<ISetting>.Enumerator enumerator = default(List<ISetting>.Enumerator);
		ISetting setting = default(ISetting);
		object obj9 = default(object);
		UnityEngine.Object obj10 = default(UnityEngine.Object);
		while (true)
		{
			object obj8;
			object obj;
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				bool flag = setting == null;
				List<ISetting> list = (List<ISetting>)(&enumerator);
				if (flag)
				{
					break;
				}
				nint num = (nint)setting;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v129 @ r10_v4 (Il2CppClass<Kamgam.SettingsGenerator.ISetting>)+12E]");
				if ((nint)0 >= (nint)0)
				{
					goto IL_00c4;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v129 @ r10_v4 (Il2CppClass<Kamgam.SettingsGenerator.ISetting>)+B0]");
				obj = 0;
				object obj2 = 0;
				while (true)
				{
					object obj3 = obj2 + obj2;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v330 @ r8_v6+v263 @ rax_v26*8]");
					if (0 == (nint)typeof(ISettingWithConnectionSO))
					{
						break;
					}
					obj2++;
					object obj4 = obj2;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v129 @ r10_v4 (Il2CppClass<Kamgam.SettingsGenerator.ISetting>)+12E]");
					if ((nint)obj4 < 0)
					{
						continue;
					}
					goto IL_00c4;
				}
				object obj5 = obj2 + obj2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v330 @ r8_v6+8+v324 @ rcx_v19*8]");
				object obj6 = (nint)0 << 4;
				object obj7 = obj6 + 312;
				obj8 = obj7 + num;
				goto IL_01f2;
			}
			enumerator.Dispose();
			return null;
			IL_00c4:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			obj8 = obj9;
			obj = 0;
			goto IL_01f2;
			IL_01f2:
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v329 @ rdx_v9] (should have been resolved before IL gen)");
			if (obj10 == connectionSO)
			{
				enumerator.Dispose();
				return setting;
			}
		}
		throw new NullReferenceException();
	}

	public void RefreshSettingOptionConnectionAndResolvers<TConnection>(bool refreshResolvers = true)
	{
		this.RefreshSettingOptionConnectionAndResolvers<TConnection, TOption>(refreshResolvers);
	}

	public unsafe void RefreshSettingOptionConnectionAndResolvers<TConnection, TOption>(bool refreshResolvers = true)
	{
		//IL_0063: Expected O, but got Ref
		//IL_015e: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ r8 (Il2CppMethodInfo)+38]");
		if ((nint)0 == 0)
		{
		}
		IList<SettingOption> settingsWithConnectionByType = this.GetSettingsWithConnectionByType<SettingOption, TConnection>((IList<SettingOption>)s_tmpRefreshSettingOptionConnectionAndResolversList);
		if (s_tmpRefreshSettingOptionConnectionAndResolversList != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			List<SettingOption>.Enumerator enumerator = default(List<SettingOption>.Enumerator);
			object obj = default(object);
			object obj3 = default(object);
			object obj5 = default(object);
			object obj7 = default(object);
			while (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				bool flag = obj == null;
				List<SettingOption> list = (List<SettingOption>)(&enumerator);
				if (!flag)
				{
					object obj2 = obj;
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v395 @ rdx_v17+588] (should have been resolved before IL gen)");
					if (obj3 != null)
					{
						object obj4 = obj;
						Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v407 @ rdx_v19+5C8] (should have been resolved before IL gen)");
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
						if (obj5 == null)
						{
							throw new NullReferenceException();
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
						object obj6 = obj7;
						Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v474 @ rdx_v23+2E8] (should have been resolved before IL gen)");
						object obj8 = obj;
						Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v478 @ rdx_v25+5A8] (should have been resolved before IL gen)");
					}
					continue;
				}
				throw new NullReferenceException();
			}
			enumerator.Dispose();
			if (refreshResolvers)
			{
				this.RefreshRegisteredResolversWithConnection<T>();
			}
			List<SettingOption> list2 = s_tmpRefreshSettingOptionConnectionAndResolversList;
			if (s_tmpRefreshSettingOptionConnectionAndResolversList != null)
			{
				int version = list2._version + 1;
				list2._version = version;
				List<SettingOption>.Enumerator enumerator2 = ((List<SettingOption>)0).GetEnumerator();
				if ((object)enumerator2 == null)
				{
					list2._size = 0;
					return;
				}
				list2._size = 0;
				if (list2._size > 0)
				{
					Array.Clear(list2._items, 0, list2._size);
				}
				return;
			}
		}
		throw new NullReferenceException();
	}

	public unsafe void SetInputActionAsset(InputActionAsset asset, bool applyImmediately = true)
	{
		//IL_0017: Expected O, but got Ref
		//IL_0072: Expected I, but got O
		//IL_0105: Expected O, but got I4
		//IL_00aa: Expected O, but got I
		//IL_00b3: Expected O, but got I4
		//IL_0112: Expected I, but got O
		//IL_0139: Expected I, but got O
		//IL_027b: Expected O, but got I
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Expected O, but got Unknown
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Expected O, but got Unknown
		//IL_0165: Expected I, but got O
		//IL_0175: Expected O, but got I
		//IL_01a1: Expected I, but got O
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Expected O, but got Unknown
		//IL_01bf: Expected O, but got I
		//IL_01ec: Expected I, but got O
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Expected O, but got Unknown
		//IL_0226: Expected I, but got O
		//IL_024b: Expected I, but got O
		IList<SettingString> settingsWithConnectionByType = GetSettingsWithConnectionByType<SettingString, InputBindingConnection>();
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
		SettingWithValue<string> settingWithValue = default(SettingWithValue<string>);
		object obj = (object)(&settingWithValue);
		SettingWithValue<string> settingWithValue2 = null;
		object obj2 = default(object);
		object obj11 = default(object);
		SettingWithValue<string> settingWithValue3 = default(SettingWithValue<string>);
		object obj12 = default(object);
		while (true)
		{
			object obj10;
			object obj3;
			if (settingWithValue != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				if (obj2 != null)
				{
					bool flag = settingWithValue == null;
					settingWithValue2 = null;
					if (flag)
					{
						break;
					}
					nint num = (nint)settingWithValue;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ r10_v5 (Il2CppClass<Kamgam.SettingsGenerator.SettingWithValue`1<System.String>>)+12E]");
					if ((nint)0 >= (nint)0)
					{
						goto IL_00ea;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ r10_v5 (Il2CppClass<Kamgam.SettingsGenerator.SettingWithValue`1<System.String>>)+B0]");
					obj3 = 0;
					object obj4 = 0;
					while (true)
					{
						object obj5 = obj4 + obj4;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v310 @ r8_v10+v331 @ rax_v28*8]");
						if (0 == (nint)typeof(IEnumerator<SettingString>))
						{
							break;
						}
						obj4++;
						object obj6 = obj4;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ r10_v5 (Il2CppClass<Kamgam.SettingsGenerator.SettingWithValue`1<System.String>>)+12E]");
						if ((nint)obj6 < 0)
						{
							continue;
						}
						goto IL_00ea;
					}
					object obj7 = obj4 + obj4;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v310 @ r8_v10+8+v387 @ rcx_v23*8]");
					object obj8 = (nint)0 << 4;
					object obj9 = obj8 + 312;
					obj10 = obj9 + num;
					goto IL_0359;
				}
				if (obj != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				}
				return;
			}
			throw new NullReferenceException();
			IL_00ea:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			obj10 = obj11;
			obj3 = 0;
			goto IL_0359;
			IL_0359:
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v392 @ rdx_v11] (should have been resolved before IL gen)");
			nint num2 = (nint)settingWithValue3;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v407 @ rdx_v13 (Il2CppClass<Kamgam.SettingsGenerator.SettingWithValue`1<System.String>>)+5C8] (should have been resolved before IL gen)");
			bool flag2 = obj12 == null;
			nint num3 = (nint)typeof(IEnumerator<SettingString>);
			settingWithValue2 = settingWithValue3;
			if (flag2)
			{
				continue;
			}
			object obj13 = obj12;
			nint num4 = (nint)typeof(InputBindingConnection);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v128 @ r8_v12 (Il2CppClass<Kamgam.SettingsGenerator.InputBindingConnection>)+130]");
			settingWithValue2 = (SettingWithValue<string>)0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v131 @ rax_v20+130]");
			nint num5 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v128 @ r8_v12 (Il2CppClass<Kamgam.SettingsGenerator.InputBindingConnection>)+130]");
			bool flag3 = num5 < 0;
			num3 = (nint)typeof(IEnumerator<SettingString>);
			if (flag3)
			{
				continue;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v131 @ rax_v20+C8]");
			object obj14 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v132 @ rax_v21+FFFFFFF8+v85 @ rcx_v6 (Kamgam.SettingsGenerator.SettingWithValue`1<System.String>)*8]");
			bool flag4 = 0 != (nint)typeof(InputBindingConnection);
			num3 = (nint)typeof(IEnumerator<SettingString>);
			if (!flag4)
			{
				settingWithValue2 = (SettingWithValue<string>)(obj12 + 48);
				bool flag5 = !applyImmediately;
				num3 = (nint)typeof(IEnumerator<SettingString>);
				if (!flag5)
				{
					settingWithValue3.Apply();
					num3 = (nint)typeof(IEnumerator<SettingString>);
					settingWithValue2 = settingWithValue3;
				}
			}
		}
		throw new NullReferenceException();
	}

	public unsafe InputActionAsset GetInputActionAsset()
	{
		//IL_0017: Expected O, but got Ref
		//IL_009e: Expected I, but got O
		//IL_00e3: Expected I, but got O
		//IL_00f3: Expected O, but got I
		//IL_012f: Expected O, but got I
		//IL_0173: Expected O, but got I
		//IL_0192: Expected O, but got I
		//IL_01d9: Expected O, but got I
		IList<SettingString> settingsWithConnectionByType = GetSettingsWithConnectionByType<SettingString, InputBindingConnection>();
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
		object obj2 = default(object);
		object obj = (object)(&obj2);
		UnityEngine.Object obj3 = null;
		object obj4 = default(object);
		UnityEngine.Object obj5 = default(UnityEngine.Object);
		object obj6 = default(object);
		while (true)
		{
			if (obj2 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				if (obj4 != null)
				{
					bool flag = obj2 == null;
					obj3 = null;
					if (!flag)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
						bool flag2 = (object)obj5 == null;
						obj3 = null;
						if (flag2)
						{
							break;
						}
						nint num = (nint)obj5;
						Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v311 @ rdx_v11 (Il2CppClass<UnityEngine.Object>)+5C8] (should have been resolved before IL gen)");
						bool flag3 = obj6 == null;
						obj3 = obj5;
						if (flag3)
						{
							continue;
						}
						object obj7 = obj6;
						nint num2 = (nint)typeof(InputBindingConnection);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v110 @ r8_v12 (Il2CppClass<Kamgam.SettingsGenerator.InputBindingConnection>)+130]");
						obj3 = (UnityEngine.Object)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v114 @ rax_v19+130]");
						nint num3 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v110 @ r8_v12 (Il2CppClass<Kamgam.SettingsGenerator.InputBindingConnection>)+130]");
						if (num3 < 0)
						{
							continue;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v114 @ rax_v19+C8]");
						object obj8 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v115 @ rax_v20+FFFFFFF8+v229 @ rcx_v5 (UnityEngine.Object)*8]");
						if (0 != (nint)typeof(InputBindingConnection))
						{
							continue;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v113 @ rax_v18+30]");
						bool flag4 = (UnityEngine.Object)0 != null;
						bool flag5 = !flag4;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v113 @ rax_v18+30]");
						obj3 = (UnityEngine.Object)0;
						if (!flag5)
						{
							if (obj != null)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v113 @ rax_v18+30]");
							return (InputActionAsset)0;
						}
						continue;
					}
					throw new NullReferenceException();
				}
				if (obj != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				}
				return null;
			}
			throw new NullReferenceException();
		}
		throw new NullReferenceException();
	}

	public void RegisterResolver(ISettingResolver resolver)
	{
		if (resolver != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			string id = default(string);
			ISetting setting = GetSetting(id);
			if (setting != null && !RegisteredResolvers.Contains(resolver))
			{
				RegisteredResolvers.Add(resolver);
				DefragRegisteredResolvers();
			}
		}
	}

	public void UnregisterResolver(ISettingResolver resolver)
	{
		if (resolver != null)
		{
			bool flag = RegisteredResolvers.Remove(resolver);
			DefragRegisteredResolvers();
		}
	}

	public void DefragRegisteredResolvers()
	{
		//IL_024a: Expected O, but got I4
		//IL_0099: Expected I, but got O
		//IL_00a1: Expected I, but got O
		//IL_00b1: Expected O, but got I
		//IL_00ce: Expected O, but got I
		//IL_0121: Expected O, but got I
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Expected O, but got Unknown
		List<ISettingResolver> registeredResolvers = RegisteredResolvers;
		bool flag = (nint)RegisteredResolvers < 0;
		int num = registeredResolvers._size - 1;
		if (flag)
		{
			return;
		}
		object obj = default(object);
		UnityEngine.Object obj2 = default(UnityEngine.Object);
		UnityEngine.Object obj4 = default(UnityEngine.Object);
		object obj9;
		do
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			if (obj == null)
			{
				goto IL_01ff;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			bool flag2 = (nint)obj2 < 0;
			bool flag3 = (object)obj2 == null;
			UnityEngine.Object obj3 = obj4;
			if (!flag3)
			{
				nint num2 = (nint)typeof(UnityEngine.Object);
				nint num3 = (nint)obj2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v261 @ rdx_v9 (Il2CppClass<UnityEngine.Object>)+130]");
				object obj5 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v219 @ r9_v8 (Il2CppClass<UnityEngine.Object>)+130]");
				nint num4 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v261 @ rdx_v9 (Il2CppClass<UnityEngine.Object>)+130]");
				object obj6 = num4 - 0;
				flag2 = (nint)obj6 < 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v219 @ r9_v8 (Il2CppClass<UnityEngine.Object>)+130]");
				nint num5 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v261 @ rdx_v9 (Il2CppClass<UnityEngine.Object>)+130]");
				bool flag4 = num5 < 0;
				obj3 = obj4;
				if (!flag4)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v219 @ r9_v8 (Il2CppClass<UnityEngine.Object>)+C8]");
					object obj7 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v264 @ rax_v12+FFFFFFF8+v263 @ rax_v11*8]");
					object obj8 = 0 - typeof(UnityEngine.Object);
					flag2 = (nint)obj8 < 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v264 @ rax_v12+FFFFFFF8+v263 @ rax_v11*8]");
					bool flag5 = 0 != (nint)typeof(UnityEngine.Object);
					obj3 = obj4;
					if (!flag5)
					{
						flag2 = (nint)obj2 < 0;
						bool flag6 = (object)obj2 == null;
						obj3 = obj2;
						if (!flag6)
						{
							bool flag7 = obj2 == null;
							flag2 = (flag7 ? 1 : 0) < (false ? 1 : 0);
							bool flag8 = !flag7;
							obj4 = obj2;
							obj3 = obj2;
							if (!flag8)
							{
								goto IL_01ff;
							}
						}
					}
				}
			}
			goto IL_0231;
			IL_0231:
			num--;
			obj9 = !flag2;
			obj4 = obj3;
			continue;
			IL_01ff:
			flag2 = (nint)RegisteredResolvers < 0;
			RegisteredResolvers.RemoveAt(num);
			obj3 = obj4;
			goto IL_0231;
		}
		while (obj9 != null);
	}

	public void RefreshRegisteredResolvers()
	{
		//IL_0107: Expected I, but got O
		//IL_00c1: Expected O, but got I4
		//IL_0137: Expected O, but got I
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Expected O, but got Unknown
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Expected O, but got Unknown
		if (RegisteredResolvers == null)
		{
			return;
		}
		List<ISettingResolver> registeredResolvers = RegisteredResolvers;
		if (registeredResolvers._size == 0)
		{
			return;
		}
		DefragRegisteredResolvers();
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		nint num = 0;
		List<ISettingResolver>.Enumerator enumerator = default(List<ISettingResolver>.Enumerator);
		object obj = default(object);
		object obj10 = default(object);
		while (true)
		{
			object obj9;
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (obj == null)
				{
					break;
				}
				object obj2 = obj;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v220 @ r10_v4+12E]");
				if ((nint)0 < (nint)0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v220 @ r10_v4+B0]");
					num = 0;
					object obj3 = 0;
					while (true)
					{
						object obj4 = obj3 + obj3;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v87 @ r8_v4 (Il2CppMethodInfo)+v277 @ rax_v22*8]");
						if (0 == (nint)typeof(ISettingResolver))
						{
							break;
						}
						obj3++;
						object obj5 = obj3;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v220 @ r10_v4+12E]");
						if ((nint)obj5 < 0)
						{
							continue;
						}
						goto IL_00f8;
					}
					object obj6 = obj3 + obj3;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v87 @ r8_v4 (Il2CppMethodInfo)+8+v333 @ rcx_v17*8]");
					object obj7 = (nint)0 << 4;
					object obj8 = obj7 + 312;
					obj9 = obj8 + obj2;
					goto IL_01fc;
				}
				goto IL_00f8;
			}
			enumerator.Dispose();
			return;
			IL_00f8:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			num = unchecked((nint)null);
			obj9 = obj10;
			goto IL_01fc;
			IL_01fc:
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v338 @ rdx_v10] (should have been resolved before IL gen)");
		}
		throw new NullReferenceException();
	}

	public void RefreshRegisteredResolvers(string id)
	{
		//IL_007b: Expected O, but got I
		//IL_0106: Expected O, but got I4
		//IL_00b3: Expected O, but got I
		//IL_00bc: Expected O, but got I4
		//IL_0146: Expected O, but got I
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Expected O, but got Unknown
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Expected O, but got Unknown
		if (RegisteredResolvers == null)
		{
			return;
		}
		List<ISettingResolver> registeredResolvers = RegisteredResolvers;
		if (registeredResolvers._size == 0)
		{
			return;
		}
		DefragRegisteredResolvers();
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<ISettingResolver>.Enumerator enumerator = default(List<ISettingResolver>.Enumerator);
		IntPtr intPtr = default(IntPtr);
		object obj11 = default(object);
		string text = default(string);
		while (true)
		{
			object obj2;
			object obj10;
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (intPtr == (IntPtr)0)
				{
					break;
				}
				object obj = (nint)intPtr;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v232 @ r10_v4+12E]");
				if ((nint)0 >= (nint)0)
				{
					goto IL_00f3;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v232 @ r10_v4+B0]");
				obj2 = 0;
				object obj3 = 0;
				while (true)
				{
					object obj4 = obj3 + obj3;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v355 @ r8_v6+v292 @ rax_v24*8]");
					if (0 == (nint)typeof(ISettingResolver))
					{
						break;
					}
					obj3++;
					object obj5 = obj3;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v232 @ r10_v4+12E]");
					if ((nint)obj5 < 0)
					{
						continue;
					}
					goto IL_00f3;
				}
				object obj6 = obj3 + obj3;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v355 @ r8_v6+8+v348 @ rcx_v19*8]");
				object obj7 = (nint)0 + (nint)1;
				object obj8 = obj7 << 4;
				object obj9 = obj8 + 312;
				obj10 = obj9 + obj;
				goto IL_0219;
			}
			enumerator.Dispose();
			return;
			IL_00f3:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			obj2 = 1;
			obj10 = obj11;
			goto IL_0219;
			IL_0219:
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v356 @ rdx_v10] (should have been resolved before IL gen)");
			if (text == id)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			}
		}
		throw new NullReferenceException();
	}

	public void RefreshRegisteredResolvers(ISetting setting)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
		string id = default(string);
		RefreshRegisteredResolvers(id);
	}

	public void RefreshRegisteredResolversWithConnection<T>()
	{
		//IL_00c7: Expected O, but got I
		//IL_0108: Expected O, but got I4
		//IL_0181: Expected I, but got O
		//IL_0236: Expected O, but got I
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Expected O, but got Unknown
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Expected O, but got Unknown
		//IL_01b0: Expected I, but got O
		//IL_01ee: Expected I, but got O
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ rdx (Il2CppMethodInfo)+38]");
		if ((nint)0 == 0)
		{
		}
		if (RegisteredResolvers == null)
		{
			return;
		}
		List<ISettingResolver> registeredResolvers = RegisteredResolvers;
		if (registeredResolvers._size == 0)
		{
			return;
		}
		DefragRegisteredResolvers();
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		nint num = 0;
		List<ISettingResolver>.Enumerator enumerator = default(List<ISettingResolver>.Enumerator);
		IntPtr intPtr = default(IntPtr);
		object obj10 = default(object);
		string text = default(string);
		object obj11 = default(object);
		object obj12 = default(object);
		while (true)
		{
			object obj9;
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (intPtr == (IntPtr)0)
				{
					break;
				}
				object obj = (nint)intPtr;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v274 @ r10_v4+12E]");
				if ((nint)0 >= (nint)0)
				{
					goto IL_013f;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v274 @ r10_v4+B0]");
				num = 0;
				object obj2 = 0;
				while (true)
				{
					object obj3 = obj2 + obj2;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v116 @ r8_v4 (Il2CppMethodInfo)+v375 @ rax_v31*8]");
					if (0 == (nint)typeof(ISettingResolver))
					{
						break;
					}
					obj2++;
					object obj4 = obj2;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v274 @ r10_v4+12E]");
					if ((nint)obj4 < 0)
					{
						continue;
					}
					goto IL_013f;
				}
				object obj5 = obj2 + obj2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v116 @ r8_v4 (Il2CppMethodInfo)+8+v431 @ rcx_v26*8]");
				object obj6 = (nint)0 + (nint)1;
				object obj7 = obj6 << 4;
				object obj8 = obj7 + 312;
				obj9 = obj8 + obj;
				goto IL_02ea;
			}
			enumerator.Dispose();
			return;
			IL_013f:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			num = 1;
			obj9 = obj10;
			goto IL_02ea;
			IL_02ea:
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v438 @ rdx_v10] (should have been resolved before IL gen)");
			if (string.IsNullOrEmpty(text))
			{
				continue;
			}
			ISetting setting = GetSetting(text);
			bool flag = setting == null;
			num = unchecked((nint)null);
			if (flag)
			{
				continue;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			bool flag2 = obj11 == null;
			num = (nint)setting;
			if (!flag2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				bool flag3 = obj12 == null;
				num = (nint)setting;
				if (!flag3)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					num = intPtr;
				}
			}
		}
		throw new NullReferenceException();
	}

	public void RefreshRegisteredResolversWithConnection(IConnection connection)
	{
		//IL_0080: Expected O, but got I
		//IL_00c1: Expected O, but got I4
		//IL_013a: Expected I, but got O
		//IL_01e2: Expected O, but got I
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Expected O, but got Unknown
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Expected O, but got Unknown
		//IL_0169: Expected I, but got O
		//IL_019a: Expected I, but got O
		if (RegisteredResolvers == null)
		{
			return;
		}
		List<ISettingResolver> registeredResolvers = RegisteredResolvers;
		if (registeredResolvers._size == 0)
		{
			return;
		}
		DefragRegisteredResolvers();
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		nint num = 0;
		List<ISettingResolver>.Enumerator enumerator = default(List<ISettingResolver>.Enumerator);
		IntPtr intPtr = default(IntPtr);
		object obj10 = default(object);
		string text = default(string);
		object obj11 = default(object);
		object obj12 = default(object);
		while (true)
		{
			object obj9;
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (intPtr == (IntPtr)0)
				{
					break;
				}
				object obj = (nint)intPtr;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v249 @ r10_v4+12E]");
				if ((nint)0 >= (nint)0)
				{
					goto IL_00f8;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v249 @ r10_v4+B0]");
				num = 0;
				object obj2 = 0;
				while (true)
				{
					object obj3 = obj2 + obj2;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v98 @ r8_v4 (Il2CppMethodInfo)+v350 @ rax_v27*8]");
					if (0 == (nint)typeof(ISettingResolver))
					{
						break;
					}
					obj2++;
					object obj4 = obj2;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v249 @ r10_v4+12E]");
					if ((nint)obj4 < 0)
					{
						continue;
					}
					goto IL_00f8;
				}
				object obj5 = obj2 + obj2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v98 @ r8_v4 (Il2CppMethodInfo)+8+v406 @ rcx_v22*8]");
				object obj6 = (nint)0 + (nint)1;
				object obj7 = obj6 << 4;
				object obj8 = obj7 + 312;
				obj9 = obj8 + obj;
				goto IL_02b5;
			}
			enumerator.Dispose();
			return;
			IL_00f8:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			num = 1;
			obj9 = obj10;
			goto IL_02b5;
			IL_02b5:
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v413 @ rdx_v10] (should have been resolved before IL gen)");
			if (string.IsNullOrEmpty(text))
			{
				continue;
			}
			ISetting setting = GetSetting(text);
			bool flag = setting == null;
			num = unchecked((nint)null);
			if (flag)
			{
				continue;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			bool flag2 = obj11 == null;
			num = (nint)setting;
			if (!flag2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				bool flag3 = obj12 != connection;
				num = (nint)setting;
				if (!flag3)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					num = intPtr;
				}
			}
		}
		throw new NullReferenceException();
	}

	public unsafe void Reset()
	{
		//IL_0049: Expected O, but got I
		//IL_018d: Expected O, but got Ref
		//IL_008a: Expected O, but got I4
		//IL_011b: Expected O, but got I
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Expected O, but got Unknown
		//IL_0232: Expected I, but got O
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Expected O, but got Unknown
		//IL_01e4: Expected O, but got I4
		//IL_025a: Expected O, but got I
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Expected O, but got Unknown
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Expected O, but got Unknown
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		nint num = 0;
		List<ISetting>.Enumerator enumerator = default(List<ISetting>.Enumerator);
		IntPtr intPtr = default(IntPtr);
		List<ISettingResolver>.Enumerator enumerator2 = default(List<ISettingResolver>.Enumerator);
		object obj10 = default(object);
		object obj20 = default(object);
		object obj21 = default(object);
		object obj22 = default(object);
		while (true)
		{
			object obj9;
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (intPtr == (IntPtr)0)
				{
					break;
				}
				object obj = (nint)intPtr;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v154 @ r10_v6+12E]");
				if ((nint)0 >= (nint)0)
				{
					goto IL_00c1;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v154 @ r10_v6+B0]");
				num = 0;
				object obj2 = 0;
				while (true)
				{
					object obj3 = obj2 + obj2;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v106 @ r8_v3 (Il2CppMethodInfo)+v312 @ rax_v37*8]");
					if (0 == (nint)typeof(ISetting))
					{
						break;
					}
					obj2++;
					object obj4 = obj2;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v154 @ r10_v6+12E]");
					if ((nint)obj4 < 0)
					{
						continue;
					}
					goto IL_00c1;
				}
				object obj5 = obj2 + obj2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v106 @ r8_v3 (Il2CppMethodInfo)+8+v392 @ rcx_v32*8]");
				object obj6 = (nint)0 + (nint)6;
				object obj7 = obj6 << 4;
				object obj8 = obj7 + 312;
				obj9 = obj8 + obj;
				goto IL_0301;
			}
			enumerator.Dispose();
			DefragRegisteredResolvers();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			nint num2 = 0;
			while (true)
			{
				object obj19;
				if (enumerator2.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					bool flag = obj10 == null;
					object obj11 = (object)(&enumerator2);
					if (flag)
					{
						break;
					}
					object obj12 = obj10;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v413 @ r10_v5+12E]");
					if ((nint)0 < (nint)0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v413 @ r10_v5+B0]");
						num2 = 0;
						object obj13 = 0;
						while (true)
						{
							object obj14 = obj13 + obj13;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v221 @ r8_v6 (Il2CppMethodInfo)+v463 @ rax_v25*8]");
							if (0 == (nint)typeof(ISettingResolver))
							{
								break;
							}
							obj13++;
							object obj15 = obj13;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v413 @ r10_v5+12E]");
							if ((nint)obj15 < 0)
							{
								continue;
							}
							goto IL_021b;
						}
						object obj16 = obj13 + obj13;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v221 @ r8_v6 (Il2CppMethodInfo)+8+v519 @ rcx_v21*8]");
						object obj17 = (nint)0 << 4;
						object obj18 = obj17 + 312;
						obj19 = obj18 + obj12;
						goto IL_03a7;
					}
					goto IL_021b;
				}
				enumerator2.Dispose();
				return;
				IL_021b:
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
				obj19 = obj20;
				num2 = unchecked((nint)null);
				goto IL_03a7;
				IL_03a7:
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v524 @ rdx_v14] (should have been resolved before IL gen)");
			}
			throw new NullReferenceException();
			IL_00c1:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			obj9 = obj21;
			num = 6;
			goto IL_0301;
			IL_0301:
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v399 @ rdx_v24] (should have been resolved before IL gen)");
			if (obj22 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				num = intPtr;
			}
		}
		throw new NullReferenceException();
	}

	public unsafe void Reset(string[] ids)
	{
		//IL_0068: Expected O, but got I
		//IL_01eb: Expected O, but got Ref
		//IL_00a9: Expected O, but got I4
		//IL_0201: Expected O, but got I
		//IL_0179: Expected O, but got I
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Expected O, but got Unknown
		//IL_0290: Expected I, but got O
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Expected O, but got Unknown
		//IL_0242: Expected O, but got I4
		//IL_02b8: Expected O, but got I
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Expected O, but got Unknown
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Expected O, but got Unknown
		if (ids == null || ids.Length == 0)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		nint num = 0;
		List<ISetting>.Enumerator enumerator = default(List<ISetting>.Enumerator);
		IntPtr intPtr = default(IntPtr);
		List<ISettingResolver>.Enumerator enumerator2 = default(List<ISettingResolver>.Enumerator);
		object obj19 = default(object);
		object obj20 = default(object);
		object obj21 = default(object);
		string value = default(string);
		while (true)
		{
			object obj9;
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (intPtr == (IntPtr)0)
				{
					break;
				}
				object obj = (nint)intPtr;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v279 @ r10_v7+12E]");
				if ((nint)0 >= (nint)0)
				{
					goto IL_00e0;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v279 @ r10_v7+B0]");
				num = 0;
				object obj2 = 0;
				while (true)
				{
					object obj3 = obj2 + obj2;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v246 @ r8_v4 (Il2CppMethodInfo)+v387 @ rax_v40*8]");
					if (0 == (nint)typeof(ISetting))
					{
						break;
					}
					obj2++;
					object obj4 = obj2;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v279 @ r10_v7+12E]");
					if ((nint)obj4 < 0)
					{
						continue;
					}
					goto IL_00e0;
				}
				object obj5 = obj2 + obj2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v246 @ r8_v4 (Il2CppMethodInfo)+8+v466 @ rcx_v35*8]");
				object obj6 = (nint)0 + (nint)6;
				object obj7 = obj6 << 4;
				object obj8 = obj7 + 312;
				obj9 = obj8 + obj;
				goto IL_0383;
			}
			enumerator.Dispose();
			DefragRegisteredResolvers();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			nint num2 = 0;
			while (true)
			{
				object obj18;
				if (enumerator2.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					bool flag = intPtr == (IntPtr)0;
					object obj10 = (object)(&enumerator2);
					if (flag)
					{
						break;
					}
					object obj11 = (nint)intPtr;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v491 @ r10_v6+12E]");
					if ((nint)0 < (nint)0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v491 @ r10_v6+B0]");
						num2 = 0;
						object obj12 = 0;
						while (true)
						{
							object obj13 = obj12 + obj12;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v103 @ r8_v7 (Il2CppMethodInfo)+v540 @ rax_v26*8]");
							if (0 == (nint)typeof(ISettingResolver))
							{
								break;
							}
							obj12++;
							object obj14 = obj12;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v491 @ r10_v6+12E]");
							if ((nint)obj14 < 0)
							{
								continue;
							}
							goto IL_0279;
						}
						object obj15 = obj12 + obj12;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v103 @ r8_v7 (Il2CppMethodInfo)+8+v596 @ rcx_v22*8]");
						object obj16 = (nint)0 << 4;
						object obj17 = obj16 + 312;
						obj18 = obj17 + obj11;
						goto IL_0422;
					}
					goto IL_0279;
				}
				enumerator2.Dispose();
				return;
				IL_0279:
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
				obj18 = obj19;
				num2 = unchecked((nint)null);
				goto IL_0422;
				IL_0422:
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v601 @ rdx_v15] (should have been resolved before IL gen)");
			}
			throw new NullReferenceException();
			IL_00e0:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			obj9 = obj20;
			num = 6;
			goto IL_0383;
			IL_0383:
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v473 @ rdx_v25] (should have been resolved before IL gen)");
			if (obj21 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				bool flag2 = Enumerable.Contains(ids, value);
				bool flag3 = !flag2;
				num = 0;
				if (!flag3)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					num = intPtr;
				}
			}
		}
		throw new NullReferenceException();
	}

	public unsafe void ResetControls()
	{
		//IL_0049: Expected O, but got I
		//IL_033d: Expected O, but got Ref
		//IL_008a: Expected O, but got I4
		//IL_00e9: Expected O, but got I
		//IL_00f7: Expected I, but got O
		//IL_0107: Expected O, but got I
		//IL_01c7: Expected O, but got I
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Expected O, but got Unknown
		//IL_03e2: Expected I, but got O
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Expected O, but got Unknown
		//IL_0394: Expected O, but got I4
		//IL_0143: Expected O, but got I
		//IL_040a: Expected O, but got I
		//IL_0413: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Expected O, but got Unknown
		//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Expected O, but got Unknown
		//IL_025b: Expected I, but got O
		//IL_0269: Expected I, but got O
		//IL_0279: Expected O, but got I
		//IL_02b5: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		nint num = 0;
		List<ISetting>.Enumerator enumerator = default(List<ISetting>.Enumerator);
		IntPtr intPtr = default(IntPtr);
		List<ISettingResolver>.Enumerator enumerator2 = default(List<ISettingResolver>.Enumerator);
		object obj10 = default(object);
		object obj20 = default(object);
		object obj21 = default(object);
		object obj22 = default(object);
		object obj26 = default(object);
		object obj27 = default(object);
		while (true)
		{
			object obj9;
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (intPtr == (IntPtr)0)
				{
					break;
				}
				object obj = (nint)intPtr;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v154 @ r10_v6+12E]");
				if ((nint)0 >= (nint)0)
				{
					goto IL_00c1;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v154 @ r10_v6+B0]");
				num = 0;
				object obj2 = 0;
				while (true)
				{
					object obj3 = obj2 + obj2;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v106 @ r8_v3 (Il2CppMethodInfo)+v383 @ rax_v45*8]");
					if (0 == (nint)typeof(ISetting))
					{
						break;
					}
					obj2++;
					object obj4 = obj2;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v154 @ r10_v6+12E]");
					if ((nint)obj4 < 0)
					{
						continue;
					}
					goto IL_00c1;
				}
				object obj5 = obj2 + obj2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v106 @ r8_v3 (Il2CppMethodInfo)+8+v467 @ rcx_v38*8]");
				object obj6 = (nint)0 + (nint)6;
				object obj7 = obj6 << 4;
				object obj8 = obj7 + 312;
				obj9 = obj8 + obj;
				goto IL_04b1;
			}
			enumerator.Dispose();
			DefragRegisteredResolvers();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			nint num2 = 0;
			while (true)
			{
				object obj19;
				if (enumerator2.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					bool flag = obj10 == null;
					object obj11 = (object)(&enumerator2);
					if (flag)
					{
						break;
					}
					object obj12 = obj10;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v503 @ r10_v5+12E]");
					if ((nint)0 < (nint)0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v503 @ r10_v5+B0]");
						num2 = 0;
						object obj13 = 0;
						while (true)
						{
							object obj14 = obj13 + obj13;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v292 @ r8_v6 (Il2CppMethodInfo)+v578 @ rax_v25*8]");
							if (0 == (nint)typeof(ISettingResolver))
							{
								break;
							}
							obj13++;
							object obj15 = obj13;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v503 @ r10_v5+12E]");
							if ((nint)obj15 < 0)
							{
								continue;
							}
							goto IL_03cb;
						}
						object obj16 = obj13 + obj13;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v292 @ r8_v6 (Il2CppMethodInfo)+8+v634 @ rcx_v21*8]");
						object obj17 = (nint)0 << 4;
						object obj18 = obj17 + 312;
						obj19 = obj18 + obj12;
						goto IL_0557;
					}
					goto IL_03cb;
				}
				enumerator2.Dispose();
				return;
				IL_03cb:
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
				obj19 = obj20;
				num2 = unchecked((nint)null);
				goto IL_0557;
				IL_0557:
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v639 @ rdx_v14] (should have been resolved before IL gen)");
			}
			throw new NullReferenceException();
			IL_00c1:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			obj9 = obj21;
			num = 6;
			goto IL_04b1;
			IL_04b1:
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v474 @ rdx_v24] (should have been resolved before IL gen)");
			if (obj22 == null)
			{
				continue;
			}
			object obj23 = (nint)intPtr;
			nint num3 = (nint)typeof(SettingKeyCombination);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v489 @ rdx_v26 (Il2CppClass<Kamgam.SettingsGenerator.SettingKeyCombination>)+130]");
			object obj24 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v488 @ r8_v14+130]");
			nint num4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v489 @ rdx_v26 (Il2CppClass<Kamgam.SettingsGenerator.SettingKeyCombination>)+130]");
			if (num4 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v488 @ r8_v14+C8]");
				object obj25 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v538 @ rax_v39+FFFFFFF8+v490 @ rax_v32*8]");
				if (0 == (nint)typeof(SettingKeyCombination) && intPtr != (IntPtr)0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					num = intPtr;
					continue;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			bool flag2 = obj26 == null;
			num = intPtr;
			if (flag2)
			{
				continue;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			bool flag3 = obj27 == null;
			num = intPtr;
			if (flag3)
			{
				continue;
			}
			num = (nint)obj27;
			nint num5 = (nint)typeof(InputBindingConnection);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v163 @ rdx_v29 (Il2CppClass<Kamgam.SettingsGenerator.InputBindingConnection>)+130]");
			object obj28 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v106 @ r8_v3 (Il2CppMethodInfo)+130]");
			nint num6 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v163 @ rdx_v29 (Il2CppClass<Kamgam.SettingsGenerator.InputBindingConnection>)+130]");
			if (num6 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v106 @ r8_v3 (Il2CppMethodInfo)+C8]");
				object obj29 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v177 @ rax_v37+FFFFFFF8+v176 @ rax_v36*8]");
				if (0 == (nint)typeof(InputBindingConnection))
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					num = intPtr;
				}
			}
		}
		throw new NullReferenceException();
	}

	public unsafe void ResetWrongControls()
	{
		//IL_0051: Expected O, but got I
		//IL_0092: Expected O, but got I4
		//IL_021b: Expected O, but got I
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Expected O, but got Unknown
		//IL_05a4: Expected I, but got O
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Expected O, but got Unknown
		//IL_0556: Expected O, but got I4
		//IL_05cc: Expected O, but got I
		//IL_05d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05da: Expected O, but got Unknown
		//IL_014f: Expected I, but got O
		//IL_015d: Expected I, but got O
		//IL_016d: Expected O, but got I
		//IL_0564: Unknown result type (might be due to invalid IL or missing references)
		//IL_0569: Expected O, but got Unknown
		//IL_01a9: Expected O, but got I
		//IL_01ee: Expected I, but got O
		//IL_0261: Expected O, but got I
		//IL_0295: Expected I, but got O
		//IL_02b3: Expected O, but got I
		//IL_02e8: Expected I, but got O
		//IL_0365: Expected I, but got O
		//IL_0382: Expected I, but got O
		//IL_03a8: Expected O, but got I
		//IL_03dc: Expected I, but got O
		//IL_03fa: Expected O, but got I
		//IL_042f: Expected I, but got O
		//IL_0496: Expected I, but got O
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		nint num = 0;
		List<ISetting>.Enumerator enumerator = default(List<ISetting>.Enumerator);
		IntPtr intPtr = default(IntPtr);
		List<ISettingResolver>.Enumerator enumerator2 = default(List<ISettingResolver>.Enumerator);
		object obj10 = default(object);
		object obj19 = default(object);
		object obj20 = default(object);
		object obj21 = default(object);
		object obj22 = default(object);
		IntPtr intPtr3 = default(IntPtr);
		string text = default(string);
		IntPtr intPtr5 = default(IntPtr);
		string text2 = default(string);
		object obj29 = default(object);
		IntPtr intPtr4;
		while (true)
		{
			object obj9;
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				bool flag = intPtr == (IntPtr)0;
				nint num2 = (nint)(&enumerator);
				if (flag)
				{
					goto IL_0637;
				}
				object obj = (nint)intPtr;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v154 @ r10_v7+12E]");
				if ((nint)0 >= (nint)0)
				{
					goto IL_00c9;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v154 @ r10_v7+B0]");
				num = 0;
				object obj2 = 0;
				while (true)
				{
					object obj3 = obj2 + obj2;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v106 @ r8_v3 (Il2CppMethodInfo)+v428 @ rax_v66*8]");
					if (0 == (nint)typeof(ISetting))
					{
						break;
					}
					obj2++;
					object obj4 = obj2;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v154 @ r10_v7+12E]");
					if ((nint)obj4 < 0)
					{
						continue;
					}
					goto IL_00c9;
				}
				object obj5 = obj2 + obj2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v106 @ r8_v3 (Il2CppMethodInfo)+8+v512 @ rcx_v52*8]");
				object obj6 = (nint)0 + (nint)6;
				object obj7 = obj6 << 4;
				object obj8 = obj7 + 312;
				obj9 = obj8 + obj;
				goto IL_06aa;
			}
			enumerator.Dispose();
			DefragRegisteredResolvers();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			nint num3 = 0;
			while (true)
			{
				object obj18;
				if (enumerator2.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					bool flag2 = obj10 == null;
					nint num2 = (nint)(&enumerator2);
					if (flag2)
					{
						break;
					}
					object obj11 = obj10;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v535 @ r10_v5+12E]");
					if ((nint)0 < (nint)0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v535 @ r10_v5+B0]");
						num3 = 0;
						object obj12 = 0;
						while (true)
						{
							object obj13 = obj12 + obj12;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v304 @ r8_v6 (Il2CppMethodInfo)+v586 @ rax_v25*8]");
							if (0 == (nint)typeof(ISettingResolver))
							{
								break;
							}
							obj12++;
							object obj14 = obj12;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v535 @ r10_v5+12E]");
							if ((nint)obj14 < 0)
							{
								continue;
							}
							goto IL_058d;
						}
						object obj15 = obj12 + obj12;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v304 @ r8_v6 (Il2CppMethodInfo)+8+v658 @ rcx_v21*8]");
						object obj16 = (nint)0 << 4;
						object obj17 = obj16 + 312;
						obj18 = obj17 + obj11;
						goto IL_0763;
					}
					goto IL_058d;
				}
				enumerator2.Dispose();
				return;
				IL_058d:
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
				obj18 = obj19;
				num3 = unchecked((nint)null);
				goto IL_0763;
				IL_0763:
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v663 @ rdx_v14] (should have been resolved before IL gen)");
			}
			throw new NullReferenceException();
			IL_04a4:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			num = intPtr;
			continue;
			IL_06aa:
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v519 @ rdx_v25] (should have been resolved before IL gen)");
			if (obj20 == null)
			{
				continue;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			bool flag3 = obj21 == null;
			num = intPtr;
			if (flag3)
			{
				continue;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			bool flag4 = obj22 == null;
			num = intPtr;
			if (flag4)
			{
				continue;
			}
			num = (nint)obj22;
			nint num4 = (nint)typeof(InputBindingConnection);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v164 @ rdx_v29 (Il2CppClass<Kamgam.SettingsGenerator.InputBindingConnection>)+130]");
			object obj23 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v106 @ r8_v3 (Il2CppMethodInfo)+130]");
			nint num5 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v164 @ rdx_v29 (Il2CppClass<Kamgam.SettingsGenerator.InputBindingConnection>)+130]");
			if (num5 < 0)
			{
				continue;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v106 @ r8_v3 (Il2CppMethodInfo)+C8]");
			object obj24 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v178 @ rax_v36+FFFFFFF8+v177 @ rax_v35*8]");
			if (0 != (nint)typeof(InputBindingConnection))
			{
				continue;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			nint num6 = (nint)typeof(InputBindingConnection);
			IntPtr intPtr2 = intPtr3;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v645 @ rcx_v32 (Il2CppClass<Kamgam.SettingsGenerator.InputBindingConnection>)+130]");
			object obj25 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v746 @ r9_v12 (Il2CppClass<Kamgam.SettingsGenerator.ISetting>)+130]");
			nint num7 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v645 @ rcx_v32 (Il2CppClass<Kamgam.SettingsGenerator.InputBindingConnection>)+130]");
			bool flag5 = num7 < 0;
			intPtr4 = intPtr3;
			nint num8 = (nint)typeof(InputBindingConnection);
			if (!flag5)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v746 @ r9_v12 (Il2CppClass<Kamgam.SettingsGenerator.ISetting>)+C8]");
				object obj26 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v715 @ rax_v50+FFFFFFF8+v669 @ rax_v49*8]");
				bool flag6 = 0 != (nint)typeof(InputBindingConnection);
				intPtr4 = intPtr3;
				num8 = (nint)typeof(InputBindingConnection);
				if (!flag6)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v746 @ r9_v12 (Il2CppClass<Kamgam.SettingsGenerator.ISetting>)+208] (should have been resolved before IL gen)");
					bool flag7 = text == null;
					intPtr4 = intPtr3;
					num8 = intPtr3;
					if (!flag7)
					{
						if (text.Contains("<Mouse>/leftButton"))
						{
							goto IL_04a4;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
						nint num9 = (nint)typeof(InputBindingConnection);
						bool flag8 = intPtr5 == (IntPtr)0;
						nint num10 = (nint)typeof(InputBindingConnection);
						if (flag8)
						{
							break;
						}
						intPtr2 = intPtr5;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v690 @ rcx_v45 (Il2CppClass<Kamgam.SettingsGenerator.InputBindingConnection>)+130]");
						object obj27 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v746 @ r9_v12 (Il2CppClass<Kamgam.SettingsGenerator.ISetting>)+130]");
						nint num11 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v690 @ rcx_v45 (Il2CppClass<Kamgam.SettingsGenerator.InputBindingConnection>)+130]");
						bool flag9 = num11 < 0;
						intPtr4 = intPtr5;
						num10 = (nint)typeof(InputBindingConnection);
						if (!flag9)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v746 @ r9_v12 (Il2CppClass<Kamgam.SettingsGenerator.ISetting>)+C8]");
							object obj28 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v796 @ rax_v58+FFFFFFF8+v795 @ rax_v57*8]");
							bool flag10 = 0 != (nint)typeof(InputBindingConnection);
							intPtr4 = intPtr5;
							num10 = (nint)typeof(InputBindingConnection);
							if (!flag10)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v746 @ r9_v12 (Il2CppClass<Kamgam.SettingsGenerator.ISetting>)+208] (should have been resolved before IL gen)");
								bool flag11 = text2 == null;
								intPtr4 = intPtr5;
								num10 = intPtr5;
								if (!flag11)
								{
									bool flag12 = text2.Contains("<Pointer>/press");
									bool flag13 = !flag12;
									num = unchecked((nint)null);
									if (flag13)
									{
										continue;
									}
									goto IL_04a4;
								}
								throw new NullReferenceException();
							}
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
						num8 = intPtr4;
					}
					throw new NullReferenceException();
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
			goto IL_0637;
			IL_00c9:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			obj9 = obj29;
			num = 6;
			goto IL_06aa;
			IL_0637:
			throw new NullReferenceException();
		}
		intPtr4 = intPtr5;
		throw new NullReferenceException();
	}

	public unsafe void ResetGroups(string[] groups)
	{
		//IL_0068: Expected O, but got I
		//IL_01db: Expected O, but got Ref
		//IL_00a9: Expected O, but got I4
		//IL_01f1: Expected O, but got I
		//IL_0169: Expected O, but got I
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Expected O, but got Unknown
		//IL_0280: Expected I, but got O
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Expected O, but got Unknown
		//IL_0232: Expected O, but got I4
		//IL_02a8: Expected O, but got I
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Expected O, but got Unknown
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Expected O, but got Unknown
		if (groups == null || groups.Length == 0)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		nint num = 0;
		List<ISetting>.Enumerator enumerator = default(List<ISetting>.Enumerator);
		IntPtr intPtr = default(IntPtr);
		List<ISettingResolver>.Enumerator enumerator2 = default(List<ISettingResolver>.Enumerator);
		object obj19 = default(object);
		object obj20 = default(object);
		object obj21 = default(object);
		object obj22 = default(object);
		while (true)
		{
			object obj9;
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (intPtr == (IntPtr)0)
				{
					break;
				}
				object obj = (nint)intPtr;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v279 @ r10_v7+12E]");
				if ((nint)0 >= (nint)0)
				{
					goto IL_00e0;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v279 @ r10_v7+B0]");
				num = 0;
				object obj2 = 0;
				while (true)
				{
					object obj3 = obj2 + obj2;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v246 @ r8_v4 (Il2CppMethodInfo)+v386 @ rax_v39*8]");
					if (0 == (nint)typeof(ISetting))
					{
						break;
					}
					obj2++;
					object obj4 = obj2;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v279 @ r10_v7+12E]");
					if ((nint)obj4 < 0)
					{
						continue;
					}
					goto IL_00e0;
				}
				object obj5 = obj2 + obj2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v246 @ r8_v4 (Il2CppMethodInfo)+8+v465 @ rcx_v34*8]");
				object obj6 = (nint)0 + (nint)6;
				object obj7 = obj6 << 4;
				object obj8 = obj7 + 312;
				obj9 = obj8 + obj;
				goto IL_0373;
			}
			enumerator.Dispose();
			DefragRegisteredResolvers();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			nint num2 = 0;
			while (true)
			{
				object obj18;
				if (enumerator2.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					bool flag = intPtr == (IntPtr)0;
					object obj10 = (object)(&enumerator2);
					if (flag)
					{
						break;
					}
					object obj11 = (nint)intPtr;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v486 @ r10_v6+12E]");
					if ((nint)0 < (nint)0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v486 @ r10_v6+B0]");
						num2 = 0;
						object obj12 = 0;
						while (true)
						{
							object obj13 = obj12 + obj12;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v103 @ r8_v7 (Il2CppMethodInfo)+v535 @ rax_v26*8]");
							if (0 == (nint)typeof(ISettingResolver))
							{
								break;
							}
							obj12++;
							object obj14 = obj12;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v486 @ r10_v6+12E]");
							if ((nint)obj14 < 0)
							{
								continue;
							}
							goto IL_0269;
						}
						object obj15 = obj12 + obj12;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v103 @ r8_v7 (Il2CppMethodInfo)+8+v591 @ rcx_v22*8]");
						object obj16 = (nint)0 << 4;
						object obj17 = obj16 + 312;
						obj18 = obj17 + obj11;
						goto IL_0412;
					}
					goto IL_0269;
				}
				enumerator2.Dispose();
				return;
				IL_0269:
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
				obj18 = obj19;
				num2 = unchecked((nint)null);
				goto IL_0412;
				IL_0412:
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v596 @ rdx_v15] (should have been resolved before IL gen)");
			}
			throw new NullReferenceException();
			IL_00e0:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			obj9 = obj20;
			num = 6;
			goto IL_0373;
			IL_0373:
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v472 @ rdx_v25] (should have been resolved before IL gen)");
			if (obj21 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
				bool flag2 = obj22 == null;
				num = intPtr;
				if (!flag2)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					num = intPtr;
				}
			}
		}
		throw new NullReferenceException();
	}

	public unsafe void ResetWithoutGroups()
	{
		//IL_0049: Expected O, but got I
		//IL_01e9: Expected O, but got Ref
		//IL_008a: Expected O, but got I4
		//IL_0177: Expected O, but got I
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Expected O, but got Unknown
		//IL_028e: Expected I, but got O
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Expected O, but got Unknown
		//IL_0240: Expected O, but got I4
		//IL_02b6: Expected O, but got I
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Expected O, but got Unknown
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Expected O, but got Unknown
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		nint num = 0;
		List<ISetting>.Enumerator enumerator = default(List<ISetting>.Enumerator);
		IntPtr intPtr = default(IntPtr);
		List<ISettingResolver>.Enumerator enumerator2 = default(List<ISettingResolver>.Enumerator);
		object obj10 = default(object);
		object obj20 = default(object);
		object obj21 = default(object);
		object obj22 = default(object);
		object obj23 = default(object);
		while (true)
		{
			object obj9;
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (intPtr == (IntPtr)0)
				{
					break;
				}
				object obj = (nint)intPtr;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v154 @ r10_v6+12E]");
				if ((nint)0 >= (nint)0)
				{
					goto IL_00c1;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v154 @ r10_v6+B0]");
				num = 0;
				object obj2 = 0;
				while (true)
				{
					object obj3 = obj2 + obj2;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v106 @ r8_v3 (Il2CppMethodInfo)+v337 @ rax_v38*8]");
					if (0 == (nint)typeof(ISetting))
					{
						break;
					}
					obj2++;
					object obj4 = obj2;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v154 @ r10_v6+12E]");
					if ((nint)obj4 < 0)
					{
						continue;
					}
					goto IL_00c1;
				}
				object obj5 = obj2 + obj2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v106 @ r8_v3 (Il2CppMethodInfo)+8+v419 @ rcx_v33*8]");
				object obj6 = (nint)0 + (nint)6;
				object obj7 = obj6 << 4;
				object obj8 = obj7 + 312;
				obj9 = obj8 + obj;
				goto IL_035d;
			}
			enumerator.Dispose();
			DefragRegisteredResolvers();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			nint num2 = 0;
			while (true)
			{
				object obj19;
				if (enumerator2.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					bool flag = obj10 == null;
					object obj11 = (object)(&enumerator2);
					if (flag)
					{
						break;
					}
					object obj12 = obj10;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v440 @ r10_v5+12E]");
					if ((nint)0 < (nint)0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v440 @ r10_v5+B0]");
						num2 = 0;
						object obj13 = 0;
						while (true)
						{
							object obj14 = obj13 + obj13;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v246 @ r8_v6 (Il2CppMethodInfo)+v492 @ rax_v25*8]");
							if (0 == (nint)typeof(ISettingResolver))
							{
								break;
							}
							obj13++;
							object obj15 = obj13;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v440 @ r10_v5+12E]");
							if ((nint)obj15 < 0)
							{
								continue;
							}
							goto IL_0277;
						}
						object obj16 = obj13 + obj13;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v246 @ r8_v6 (Il2CppMethodInfo)+8+v548 @ rcx_v21*8]");
						object obj17 = (nint)0 << 4;
						object obj18 = obj17 + 312;
						obj19 = obj18 + obj12;
						goto IL_0403;
					}
					goto IL_0277;
				}
				enumerator2.Dispose();
				return;
				IL_0277:
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
				obj19 = obj20;
				num2 = unchecked((nint)null);
				goto IL_0403;
				IL_0403:
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v553 @ rdx_v14] (should have been resolved before IL gen)");
			}
			throw new NullReferenceException();
			IL_00c1:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			obj9 = obj21;
			num = 6;
			goto IL_035d;
			IL_035d:
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v426 @ rdx_v24] (should have been resolved before IL gen)");
			if (obj22 == null)
			{
				continue;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			bool flag2 = obj23 == null;
			num = intPtr;
			if (!flag2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v167 @ rax_v32+18]");
				bool flag3 = (nint)0 > (nint)0;
				num = intPtr;
				if (!flag3)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					num = intPtr;
				}
			}
		}
		throw new NullReferenceException();
	}

	public void ResetToUnappliedValues()
	{
		ResetToUnappliedValues(propagateChange: true);
	}

	public unsafe void ResetToUnappliedValues(bool propagateChange)
	{
		//IL_0049: Expected O, but got I
		//IL_01eb: Expected O, but got Ref
		//IL_008a: Expected O, but got I4
		//IL_0179: Expected O, but got I
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Expected O, but got Unknown
		//IL_0290: Expected I, but got O
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Expected O, but got Unknown
		//IL_0242: Expected O, but got I4
		//IL_02b8: Expected O, but got I
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Expected O, but got Unknown
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Expected O, but got Unknown
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		nint num = 0;
		List<ISetting>.Enumerator enumerator = default(List<ISetting>.Enumerator);
		IntPtr intPtr = default(IntPtr);
		List<ISettingResolver>.Enumerator enumerator2 = default(List<ISettingResolver>.Enumerator);
		object obj10 = default(object);
		object obj20 = default(object);
		object obj21 = default(object);
		object obj22 = default(object);
		object obj23 = default(object);
		object obj24 = default(object);
		while (true)
		{
			object obj9;
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (intPtr == (IntPtr)0)
				{
					break;
				}
				object obj = (nint)intPtr;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v157 @ r10_v6+12E]");
				if ((nint)0 >= (nint)0)
				{
					goto IL_00c1;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v157 @ r10_v6+B0]");
				num = 0;
				object obj2 = 0;
				while (true)
				{
					object obj3 = obj2 + obj2;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v109 @ r8_v3 (Il2CppMethodInfo)+v352 @ rax_v39*8]");
					if (0 == (nint)typeof(ISetting))
					{
						break;
					}
					obj2++;
					object obj4 = obj2;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v157 @ r10_v6+12E]");
					if ((nint)obj4 < 0)
					{
						continue;
					}
					goto IL_00c1;
				}
				object obj5 = obj2 + obj2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v109 @ r8_v3 (Il2CppMethodInfo)+8+v432 @ rcx_v34*8]");
				object obj6 = (nint)0 + (nint)6;
				object obj7 = obj6 << 4;
				object obj8 = obj7 + 312;
				obj9 = obj8 + obj;
				goto IL_035f;
			}
			enumerator.Dispose();
			DefragRegisteredResolvers();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			nint num2 = 0;
			while (true)
			{
				object obj19;
				if (enumerator2.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					bool flag = obj10 == null;
					object obj11 = (object)(&enumerator2);
					if (flag)
					{
						break;
					}
					object obj12 = obj10;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v453 @ r10_v5+12E]");
					if ((nint)0 < (nint)0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v453 @ r10_v5+B0]");
						num2 = 0;
						object obj13 = 0;
						while (true)
						{
							object obj14 = obj13 + obj13;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v259 @ r8_v6 (Il2CppMethodInfo)+v505 @ rcx_v20*8]");
							if (0 == (nint)typeof(ISettingResolver))
							{
								break;
							}
							obj13++;
							object obj15 = obj13;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v453 @ r10_v5+12E]");
							if ((nint)obj15 < 0)
							{
								continue;
							}
							goto IL_0279;
						}
						object obj16 = obj13 + obj13;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v259 @ r8_v6 (Il2CppMethodInfo)+8+v559 @ rcx_v22*8]");
						object obj17 = (nint)0 << 4;
						object obj18 = obj17 + 312;
						obj19 = obj18 + obj12;
						goto IL_0405;
					}
					goto IL_0279;
				}
				enumerator2.Dispose();
				return;
				IL_0279:
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
				obj19 = obj20;
				num2 = unchecked((nint)null);
				goto IL_0405;
				IL_0405:
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v564 @ rdx_v14] (should have been resolved before IL gen)");
			}
			throw new NullReferenceException();
			IL_00c1:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			obj9 = obj21;
			num = 6;
			goto IL_035f;
			IL_035f:
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v439 @ rdx_v24] (should have been resolved before IL gen)");
			if (obj22 == null)
			{
				continue;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			bool flag2 = obj23 == null;
			num = intPtr;
			if (!flag2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				bool flag3 = obj24 == null;
				num = intPtr;
				if (!flag3)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18004DF40");
					num = intPtr;
				}
			}
		}
		throw new NullReferenceException();
	}

	public void ResetToUnappliedValues(string[] ids)
	{
		ResetToUnappliedValues(propagateChange: true, ids);
	}

	public unsafe void ResetToUnappliedValues(bool propagateChange, string[] ids)
	{
		//IL_0068: Expected O, but got I
		//IL_0249: Expected O, but got Ref
		//IL_00a9: Expected O, but got I4
		//IL_025f: Expected O, but got I
		//IL_01d7: Expected O, but got I
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Expected O, but got Unknown
		//IL_02ee: Expected I, but got O
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Expected O, but got Unknown
		//IL_02a0: Expected O, but got I4
		//IL_0316: Expected O, but got I
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Expected O, but got Unknown
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Expected O, but got Unknown
		if (ids == null || ids.Length == 0)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		nint num = 0;
		List<ISetting>.Enumerator enumerator = default(List<ISetting>.Enumerator);
		IntPtr intPtr = default(IntPtr);
		List<ISettingResolver>.Enumerator enumerator2 = default(List<ISettingResolver>.Enumerator);
		object obj19 = default(object);
		object obj20 = default(object);
		object obj21 = default(object);
		object obj22 = default(object);
		object obj23 = default(object);
		string value = default(string);
		while (true)
		{
			object obj9;
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (intPtr == (IntPtr)0)
				{
					break;
				}
				object obj = (nint)intPtr;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v275 @ r10_v7+12E]");
				if ((nint)0 >= (nint)0)
				{
					goto IL_00e0;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v275 @ r10_v7+B0]");
				num = 0;
				object obj2 = 0;
				while (true)
				{
					object obj3 = obj2 + obj2;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v242 @ r8_v4 (Il2CppMethodInfo)+v415 @ rax_v42*8]");
					if (0 == (nint)typeof(ISetting))
					{
						break;
					}
					obj2++;
					object obj4 = obj2;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v275 @ r10_v7+12E]");
					if ((nint)obj4 < 0)
					{
						continue;
					}
					goto IL_00e0;
				}
				object obj5 = obj2 + obj2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v242 @ r8_v4 (Il2CppMethodInfo)+8+v494 @ rcx_v37*8]");
				object obj6 = (nint)0 + (nint)6;
				object obj7 = obj6 << 4;
				object obj8 = obj7 + 312;
				obj9 = obj8 + obj;
				goto IL_03e1;
			}
			enumerator.Dispose();
			DefragRegisteredResolvers();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			nint num2 = 0;
			while (true)
			{
				object obj18;
				if (enumerator2.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					bool flag = intPtr == (IntPtr)0;
					object obj10 = (object)(&enumerator2);
					if (flag)
					{
						break;
					}
					object obj11 = (nint)intPtr;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v515 @ r10_v6+12E]");
					if ((nint)0 < (nint)0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v515 @ r10_v6+B0]");
						num2 = 0;
						object obj12 = 0;
						while (true)
						{
							object obj13 = obj12 + obj12;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v106 @ r8_v7 (Il2CppMethodInfo)+v570 @ rcx_v21*8]");
							if (0 == (nint)typeof(ISettingResolver))
							{
								break;
							}
							obj12++;
							object obj14 = obj12;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v515 @ r10_v6+12E]");
							if ((nint)obj14 < 0)
							{
								continue;
							}
							goto IL_02d7;
						}
						object obj15 = obj12 + obj12;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v106 @ r8_v7 (Il2CppMethodInfo)+8+v624 @ rcx_v23*8]");
						object obj16 = (nint)0 << 4;
						object obj17 = obj16 + 312;
						obj18 = obj17 + obj11;
						goto IL_0480;
					}
					goto IL_02d7;
				}
				enumerator2.Dispose();
				return;
				IL_02d7:
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
				obj18 = obj19;
				num2 = unchecked((nint)null);
				goto IL_0480;
				IL_0480:
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v629 @ rdx_v15] (should have been resolved before IL gen)");
			}
			throw new NullReferenceException();
			IL_00e0:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			obj9 = obj20;
			num = 6;
			goto IL_03e1;
			IL_03e1:
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v501 @ rdx_v25] (should have been resolved before IL gen)");
			if (obj21 == null)
			{
				continue;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			bool flag2 = obj22 == null;
			num = intPtr;
			if (flag2)
			{
				continue;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			bool flag3 = obj23 == null;
			num = intPtr;
			if (!flag3)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				bool flag4 = Enumerable.Contains(ids, value);
				bool flag5 = !flag4;
				num = 0;
				if (!flag5)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18004DF40");
					num = intPtr;
				}
			}
		}
		throw new NullReferenceException();
	}

	public Settings()
	{
		List<ISetting> settingsCache = new List<ISetting>();
		_settingsCache = settingsCache;
		_bools = new List<SettingBool>();
		_options = new List<SettingOption>();
		_integers = new List<SettingInt>();
		_floats = new List<SettingFloat>();
		_strings = new List<SettingString>();
		_colors = new List<SettingColor>();
		_colorOptions = new List<SettingColorOption>();
		_keyCombinations = new List<SettingKeyCombination>();
		RegisteredResolvers = new List<ISettingResolver>();
		base._002Ector();
	}

	static Settings()
	{
		List<string> deactivateBeforeInit = new List<string>();
		DeactivateBeforeInit = deactivateBeforeInit;
		List<string> tmpExistingIdsBeforeLoad = new List<string>(20);
		_tmpExistingIdsBeforeLoad = tmpExistingIdsBeforeLoad;
		List<SettingOption> list = new List<SettingOption>();
		s_tmpRefreshSettingOptionConnectionAndResolversList = list;
	}
}
