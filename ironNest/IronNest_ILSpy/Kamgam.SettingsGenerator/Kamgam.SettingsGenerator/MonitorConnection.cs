using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class MonitorConnection : ConnectionWithOptions<string>, IConnectionWithSettingsAccess
{
	[StructLayout((LayoutKind)3)]
	private struct _003CwaitForMonitorSwitchToComplete_003Ed__26 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncVoidMethodBuilder _003C_003Et__builder;

		public MonitorConnection _003C_003E4__this;

		private YieldAwaitable.YieldAwaiter _003C_003Eu__1;

		private unsafe void MoveNext()
		{
			//IL_0de2: Expected O, but got Ref
			//IL_0010: Expected O, but got I4
			//IL_001f: Expected I4, but got I8
			//IL_002c: Expected I4, but got I8
			//IL_003b: Expected O, but got Ref
			//IL_0e02: Expected O, but got I4
			//IL_0cdc: Expected O, but got Ref
			//IL_0096: Expected O, but got I4
			//IL_1094: Expected I4, but got I8
			//IL_109f: Expected O, but got Ref
			//IL_00cc: Expected O, but got I4
			//IL_00d1: Expected I, but got O
			//IL_0576: Unknown result type (might be due to invalid IL or missing references)
			//IL_057b: Expected O, but got Unknown
			//IL_059d: Unknown result type (might be due to invalid IL or missing references)
			//IL_05a2: Expected O, but got Unknown
			//IL_0106: Expected O, but got I4
			//IL_010b: Expected I, but got O
			//IL_05c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_05c9: Expected O, but got Unknown
			//IL_0686: Expected I, but got O
			//IL_05e1: Expected O, but got Ref
			//IL_0610: Expected I, but got O
			//IL_06ea: Expected O, but got Ref
			//IL_070b: Expected I, but got O
			//IL_0c46: Expected O, but got I4
			//IL_1080: Expected O, but got I4
			//IL_0778: Expected I, but got O
			//IL_077d: Expected I, but got O
			//IL_017c: Expected O, but got Ref
			//IL_01a3: Expected O, but got Ref
			//IL_01b9: Expected I, but got O
			//IL_0329: Expected O, but got Ref
			//IL_01e9: Expected I, but got O
			//IL_022e: Expected I, but got O
			//IL_023e: Expected O, but got I
			//IL_09ca: Expected O, but got Ref
			//IL_09f2: Expected I, but got O
			//IL_0355: Expected O, but got Ref
			//IL_027a: Expected O, but got I
			//IL_0bbc: Expected I, but got O
			//IL_0bc9: Expected I, but got O
			//IL_0a2d: Expected I, but got O
			//IL_036b: Expected I, but got O
			//IL_0841: Expected O, but got I
			//IL_02d4: Expected I, but got O
			//IL_0a64: Expected I, but got O
			//IL_08cc: Expected O, but got I4
			//IL_03a0: Expected I, but got O
			//IL_0879: Expected O, but got I
			//IL_0faa: Expected O, but got I4
			//IL_03e5: Expected I, but got O
			//IL_03f5: Expected O, but got I
			//IL_098c: Expected O, but got I4
			//IL_09a2: Expected O, but got I
			//IL_09ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_09b0: Expected O, but got Unknown
			//IL_0431: Expected O, but got I
			//IL_048b: Expected I, but got O
			MonitorConnection monitorConnection = _003C_003E4__this;
			bool flag = _003C_003E1__state != 0;
			int num = _003C_003E1__state;
			_003CwaitForMonitorSwitchToComplete_003Ed__26 obj = (_003CwaitForMonitorSwitchToComplete_003Ed__26)System.Runtime.CompilerServices.Unsafe.AsPointer(ref this);
			if (flag)
			{
				goto IL_0040;
			}
			_003C_003Eu__1 = (YieldAwaitable.YieldAwaiter)0;
			_003C_003E1__state = -1;
			num = -1;
			YieldAwaitable.YieldAwaiter awaiter = _003C_003Eu__1;
			obj = (_003CwaitForMonitorSwitchToComplete_003Ed__26)System.Runtime.CompilerServices.Unsafe.AsPointer(ref this);
			goto IL_0cb0;
			IL_0f4e:
			Action onComplete = monitorConnection.m_OnComplete;
			if (monitorConnection.m_OnComplete != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v710.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
			}
			_003C_003E1__state = -2;
			AsyncVoidMethodBuilder asyncVoidMethodBuilder = (AsyncVoidMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			((AsyncVoidMethodBuilder*)asyncVoidMethodBuilder)->SetResult();
			return;
			IL_0cb0:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
			goto IL_0040;
			IL_0040:
			nint num2 = default(nint);
			nint num24;
			List<SettingOption>.Enumerator enumerator;
			if (monitorConnection._moveOperation.isDone)
			{
				int frameCount = Time.frameCount;
				obj = (_003CwaitForMonitorSwitchToComplete_003Ed__26)(frameCount - monitorConnection._lastSetFrame);
				if ((nint)obj > FramesToWaitAfterMonitorSwitch)
				{
					bool flag2 = !monitorConnection.RefreshResolversAfterCompletion;
					enumerator = (List<SettingOption>.Enumerator)0;
					if (!flag2)
					{
						bool flag3 = SettingsProvider.LastUsedSettingsProvider != null;
						bool flag4 = !flag3;
						enumerator = (List<SettingOption>.Enumerator)0;
						num2 = unchecked((nint)null);
						if (!flag4)
						{
							SettingsProvider lastUsedSettingsProvider = SettingsProvider.LastUsedSettingsProvider;
							if ((object)SettingsProvider.LastUsedSettingsProvider == null)
							{
								throw new NullReferenceException();
							}
							bool flag5 = SettingsProvider.LastUsedSettingsProvider.HasSettings();
							bool flag6 = !flag5;
							enumerator = (List<SettingOption>.Enumerator)0;
							num2 = unchecked((nint)null);
							if (!flag6)
							{
								lastUsedSettingsProvider = SettingsProvider.LastUsedSettingsProvider;
								if ((object)SettingsProvider.LastUsedSettingsProvider == null)
								{
									throw new NullReferenceException();
								}
								Settings settings = SettingsProvider.LastUsedSettingsProvider.Settings;
								if ((object)settings == null)
								{
									throw new NullReferenceException();
								}
								IList<SettingOption> settingsWithConnectionByType = settings.GetSettingsWithConnectionByType<SettingOption, ResolutionConnection>(s_tmpOptionSettingsList);
								lastUsedSettingsProvider = (SettingsProvider)(object)s_tmpOptionSettingsList;
								if (s_tmpOptionSettingsList == null)
								{
									throw new NullReferenceException();
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
								object obj2 = (object)(&num);
								List<SettingOption>.Enumerator enumerator2 = default(List<SettingOption>.Enumerator);
								SettingsProvider settingsProvider = default(SettingsProvider);
								object obj3 = default(object);
								object obj4 = default(object);
								while (enumerator2.MoveNext())
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
									bool flag7 = (object)settingsProvider == null;
									lastUsedSettingsProvider = (SettingsProvider)(&enumerator2);
									if (!flag7)
									{
										nint num3 = (nint)settingsProvider;
										Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v1926 @ rdx_v118 (Il2CppClass<Kamgam.SettingsGenerator.SettingsProvider>)+588] (should have been resolved before IL gen)");
										if (obj3 == null)
										{
											continue;
										}
										nint num4 = (nint)settingsProvider;
										Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v1964 @ rdx_v120 (Il2CppClass<Kamgam.SettingsGenerator.SettingsProvider>)+5C8] (should have been resolved before IL gen)");
										bool flag8 = obj4 == null;
										lastUsedSettingsProvider = settingsProvider;
										if (!flag8)
										{
											object obj5 = obj4;
											nint num5 = (nint)typeof(ResolutionConnection);
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1993 @ rdx_v123 (Il2CppClass<Kamgam.SettingsGenerator.ResolutionConnection>)+130]");
											lastUsedSettingsProvider = (SettingsProvider)0;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1992 @ rax_v223+130]");
											nint num6 = 0;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1993 @ rdx_v123 (Il2CppClass<Kamgam.SettingsGenerator.ResolutionConnection>)+130]");
											if (num6 >= 0)
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1992 @ rax_v223+C8]");
												object obj6 = 0;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2008 @ rax_v224+FFFFFFF8+v1178 @ rcx_v124 (Kamgam.SettingsGenerator.SettingsProvider)*8]");
												if (0 == (nint)typeof(ResolutionConnection))
												{
													_ = 0;
													object obj7 = obj4;
													Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v2154 @ rdx_v124+2E8] (should have been resolved before IL gen)");
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1968 @ rax_v220+28]");
													_ = 0;
													nint num7 = (nint)settingsProvider;
													Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v2159 @ rdx_v126 (Il2CppClass<Kamgam.SettingsGenerator.SettingsProvider>)+5A8] (should have been resolved before IL gen)");
													continue;
												}
											}
										}
									}
									throw new NullReferenceException();
								}
								if ((nint)obj2 < 0)
								{
									enumerator2.Dispose();
								}
								settings.RefreshRegisteredResolversWithConnection<ResolutionConnection>();
								IList<SettingOption> settingsWithConnectionByType2 = settings.GetSettingsWithConnectionByType<SettingOption, RefreshRateConnection>(s_tmpOptionSettingsList);
								lastUsedSettingsProvider = (SettingsProvider)(object)s_tmpOptionSettingsList;
								if (s_tmpOptionSettingsList == null)
								{
									throw new NullReferenceException();
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
								object obj8 = (object)(&num);
								nint num8 = 0;
								List<SettingOption>.Enumerator enumerator3 = default(List<SettingOption>.Enumerator);
								object obj9 = default(object);
								object obj10 = default(object);
								while (enumerator3.MoveNext())
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
									bool flag9 = (object)settingsProvider == null;
									lastUsedSettingsProvider = (SettingsProvider)(&enumerator3);
									if (!flag9)
									{
										nint num9 = (nint)settingsProvider;
										Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v2350 @ rdx_v105 (Il2CppClass<Kamgam.SettingsGenerator.SettingsProvider>)+588] (should have been resolved before IL gen)");
										bool flag10 = obj9 == null;
										num8 = 0;
										if (flag10)
										{
											continue;
										}
										nint num10 = (nint)settingsProvider;
										Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v2361 @ rdx_v107 (Il2CppClass<Kamgam.SettingsGenerator.SettingsProvider>)+5C8] (should have been resolved before IL gen)");
										bool flag11 = obj10 == null;
										lastUsedSettingsProvider = settingsProvider;
										if (!flag11)
										{
											object obj11 = obj10;
											nint num11 = (nint)typeof(RefreshRateConnection);
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2029 @ rdx_v109 (Il2CppClass<Kamgam.SettingsGenerator.RefreshRateConnection>)+130]");
											lastUsedSettingsProvider = (SettingsProvider)0;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2033 @ rax_v205+130]");
											nint num12 = 0;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2029 @ rdx_v109 (Il2CppClass<Kamgam.SettingsGenerator.RefreshRateConnection>)+130]");
											if (num12 >= 0)
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2033 @ rax_v205+C8]");
												object obj12 = 0;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2034 @ rax_v206+FFFFFFF8+v1178 @ rcx_v124 (Kamgam.SettingsGenerator.SettingsProvider)*8]");
												if (0 == (nint)typeof(RefreshRateConnection))
												{
													_ = 0;
													object obj13 = obj10;
													Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v2448 @ rdx_v110+2E8] (should have been resolved before IL gen)");
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2032 @ rax_v204+28]");
													_ = 0;
													nint num13 = (nint)settingsProvider;
													Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v2453 @ rdx_v112 (Il2CppClass<Kamgam.SettingsGenerator.SettingsProvider>)+5A8] (should have been resolved before IL gen)");
													num8 = 0;
													continue;
												}
											}
										}
									}
									throw new NullReferenceException();
								}
								if ((nint)obj8 < 0)
								{
									enumerator3.Dispose();
								}
								settings.RefreshRegisteredResolversWithConnection<RefreshRateConnection>();
								List<SettingOption>.Enumerator enumerator4 = default(List<SettingOption>.Enumerator);
								enumerator = enumerator4;
								num2 = num8;
							}
						}
					}
					bool flag12 = !monitorConnection.TryToPreserveResolutionOnMonitorChange;
					int num14 = (int)num2;
					if (!flag12)
					{
						nint num15 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v730 @ rdx_v24 (Il2CppClass<System.Nullable`1<UnityEngine.Resolution>>)+80]");
						bool flag13 = ((Resolution?*)null)->Value.m_Width == 0;
						num14 = (int)num2;
						if (!flag13)
						{
							FullScreenMode fullScreenMode = Screen.fullScreenMode;
							int num17;
							int num19;
							int num20;
							if (fullScreenMode != FullScreenMode.Windowed)
							{
								object obj14 = monitorConnection + 52;
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808CBE10");
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3E0");
								object obj15 = monitorConnection + 52;
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808CBE10");
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9CF10");
								object obj16 = monitorConnection + 52;
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808CBE10");
								int num16 = default(int);
								int roundedRefreshRate = ResolutionConnection.GetRoundedRefreshRate((Resolution)(&num16));
								int num18 = default(int);
								num17 = num18;
								num19 = roundedRefreshRate;
								int num21 = default(int);
								num20 = num21;
								int num23 = default(int);
								int num22 = num23;
								num2 = 0;
								num24 = unchecked((nint)null);
							}
							else
							{
								Resolution currentResolution = Screen.currentResolution;
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3E0");
								Vector2Int windowSizeBeforeMonitorChange = monitorConnection._windowSizeBeforeMonitorChange;
								object obj17 = default(object);
								if (System.Runtime.CompilerServices.Unsafe.As<Vector2Int, UIntPtr>(ref windowSizeBeforeMonitorChange) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj17))
								{
									Resolution currentResolution2 = Screen.currentResolution;
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9CF10");
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v57 @ rbx_v1 (Kamgam.SettingsGenerator.MonitorConnection)+4C]");
									object obj18 = default(object);
									bool flag14 = 0 <= (nint)obj18;
									num24 = unchecked((nint)null);
									if (flag14)
									{
										goto IL_1023;
									}
								}
								Resolution currentResolution3 = Screen.currentResolution;
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3E0");
								int num22 = Screen.currentResolution.m_Width;
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9CF10");
								Resolution currentResolution4 = Screen.currentResolution;
								int num25 = default(int);
								int roundedRefreshRate2 = ResolutionConnection.GetRoundedRefreshRate((Resolution)(&num25));
								int num26 = default(int);
								num17 = num26;
								num19 = roundedRefreshRate2;
								int num27 = default(int);
								num20 = num27;
								num24 = unchecked((nint)null);
							}
							if (num20 > 0 && num17 > 0 && num19 > 0)
							{
								bool flag15 = monitorConnection._settings != null;
								bool flag16 = !flag15;
								num2 = unchecked((nint)null);
								num24 = unchecked((nint)null);
								if (!flag16)
								{
									IList<ResolutionConnection> connectionsByType = monitorConnection._settings.GetConnectionsByType(MonitorConnection.s_tmpResolutionConnections);
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
									object obj19 = default(object);
									if (obj19 != null)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
										SettingsProvider lastUsedSettingsProvider = null;
										IntPtr intPtr = default(IntPtr);
										ResolutionConnection resolutionConnection = default(ResolutionConnection);
										object obj27 = default(object);
										while (true)
										{
											object obj21;
											object obj26;
											if (intPtr != (IntPtr)0)
											{
												Resolution value = ((Resolution?*)typeof(IEnumerator))->Value;
												if ((object)value == null)
												{
													break;
												}
												bool flag17 = intPtr == (IntPtr)0;
												lastUsedSettingsProvider = null;
												if (!flag17)
												{
													object obj20 = (nint)intPtr;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v879 @ r10_v14+12E]");
													if ((nint)0 >= (nint)0)
													{
														goto IL_08b9;
													}
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v879 @ r10_v14+B0]");
													obj21 = 0;
													int num28 = 0;
													while (true)
													{
														object obj22 = num28 + num28;
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v909 @ r8_v29+v2381 @ rax_v112*8]");
														if (0 == (nint)typeof(IEnumerator<ResolutionConnection>))
														{
															break;
														}
														num28++;
														int num29 = num28;
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v879 @ r10_v14+12E]");
														if ((nint)num29 < (nint)0)
														{
															continue;
														}
														goto IL_08b9;
													}
													object obj23 = num28 + num28;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v909 @ r8_v29+8+v2457 @ rcx_v80*8]");
													object obj24 = (nint)0 << 4;
													object obj25 = obj24 + 312;
													obj26 = obj25 + obj20;
													goto IL_0ffc;
												}
												throw new NullReferenceException();
											}
											throw new NullReferenceException();
											IL_0ffc:
											Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v2462 @ rdx_v44] (should have been resolved before IL gen)");
											if (resolutionConnection != null)
											{
												resolutionConnection.ClearResolutionCache();
												int num30 = resolutionConnection.FindClosestResolutionIndex(num20, num17, num19);
												bool flag18 = num30 < 0;
												int num31 = num19;
												if (!flag18)
												{
													resolutionConnection.Set(num30);
													IList<ISetting> settingsWithConnection = monitorConnection._settings.GetSettingsWithConnection(resolutionConnection);
													IList<ISetting> list = SettingCollectionExtensions.PullFromConnection(settingsWithConnection);
													IList<ISetting> list2 = SettingCollectionExtensions.RefreshRegisteredResolvers(settingsWithConnection, monitorConnection._settings);
													num31 = 0;
												}
												continue;
											}
											throw new NullReferenceException();
											IL_08b9:
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
											obj21 = 0;
											obj26 = obj27;
											goto IL_0ffc;
										}
										object obj28 = (object)(&num);
										bool flag19 = (nint)obj28 >= 0;
										num2 = intPtr;
										num24 = (nint)typeof(IEnumerator);
										if (!flag19)
										{
											nint num32 = (nint)(&intPtr);
											bool flag20 = num32 == 0;
											num2 = (nint)(&intPtr);
											num24 = (nint)typeof(IEnumerator);
											if (!flag20)
											{
												num2 = num32;
												Resolution value2 = ((Resolution?*)typeof(IDisposable))->Value;
												num24 = (nint)typeof(IDisposable);
											}
										}
									}
									else
									{
										if (MonitorConnection.s_tmpResolutionConnection == null)
										{
											ResolutionConnection s_tmpResolutionConnection = new ResolutionConnection();
											MonitorConnection.s_tmpResolutionConnection = s_tmpResolutionConnection;
											ResolutionConnection s_tmpResolutionConnection2 = MonitorConnection.s_tmpResolutionConnection;
											s_tmpResolutionConnection2.RefreshRateResolversAfterCompletion = true;
										}
										MonitorConnection.s_tmpResolutionConnection.ClearResolutionCache();
										int num33 = MonitorConnection.s_tmpResolutionConnection.FindClosestResolutionIndex(num20, num17, num19);
										bool flag21 = num33 < 0;
										int num31 = num19;
										num2 = num17;
										num24 = num20;
										if (!flag21)
										{
											MonitorConnection.s_tmpResolutionConnection.Set(num33);
											IList<ISetting> settingsWithConnection2 = monitorConnection._settings.GetSettingsWithConnection(MonitorConnection.s_tmpResolutionConnection);
											IList<ISetting> list3 = SettingCollectionExtensions.PullFromConnection(settingsWithConnection2);
											IList<ISetting> list4 = SettingCollectionExtensions.RefreshRegisteredResolvers(settingsWithConnection2, monitorConnection._settings);
											num31 = 0;
											num2 = unchecked((nint)null);
											num24 = (nint)monitorConnection._settings;
										}
									}
								}
							}
							goto IL_1023;
						}
					}
					goto IL_0f4e;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180387260");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180387260");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180387260");
			object obj29 = default(object);
			if (obj29 != null)
			{
				goto IL_0cb0;
			}
			_003C_003E1__state = 0;
			_ = _003C_003Eu__1;
			AsyncVoidMethodBuilder asyncVoidMethodBuilder2 = (AsyncVoidMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			((AsyncVoidMethodBuilder*)asyncVoidMethodBuilder2)->AwaitUnsafeOnCompleted(ref awaiter, ref this);
			return;
			IL_1023:
			List<ResolutionConnection> s_tmpResolutionConnections = MonitorConnection.s_tmpResolutionConnections;
			int version = s_tmpResolutionConnections._version + 1;
			s_tmpResolutionConnections._version = version;
			Resolution value3 = ((Resolution?*)num24)->Value;
			if ((object)value3 == null)
			{
				s_tmpResolutionConnections._size = 0;
				int num14 = (int)num2;
			}
			else
			{
				int num14 = s_tmpResolutionConnections._size;
				s_tmpResolutionConnections._size = 0;
				if (s_tmpResolutionConnections._size > 0)
				{
					Array.Clear(s_tmpResolutionConnections._items, 0, s_tmpResolutionConnections._size);
					int num31 = 0;
				}
			}
			monitorConnection._resolutionBeforeMonitorChange = (Resolution?)(object)0;
			_ = 0;
			monitorConnection._windowSizeBeforeMonitorChange = Vector2Int.s_Zero;
			enumerator = (List<SettingOption>.Enumerator)0;
			goto IL_0f4e;
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			this.MoveNext();
		}

		private unsafe void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//IL_000b: Expected O, but got Ref
			AsyncVoidMethodBuilder asyncVoidMethodBuilder = (AsyncVoidMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			((AsyncVoidMethodBuilder*)asyncVoidMethodBuilder)->SetStateMachine(stateMachine);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
			this.SetStateMachine(stateMachine);
		}
	}

	public static bool AllowMonitorChangeOnMobile = false;

	public static bool ForceMonitorUpdate = false;

	public static int FramesToWaitAfterMonitorSwitch = 3;

	private Action m_OnComplete;

	public bool RefreshResolversAfterCompletion = true;

	public bool TryToPreserveResolutionOnMonitorChange;

	protected Resolution? _resolutionBeforeMonitorChange;

	protected Vector2Int _windowSizeBeforeMonitorChange;

	protected List<DisplayInfo> _values;

	protected List<string> _labels;

	protected int _lastKnownMonitorIndex = -1;

	protected int _lastSetFrame;

	protected AsyncOperation _moveOperation;

	protected bool _moveOperationFailed;

	private static List<SettingOption> s_tmpOptionSettingsList;

	private static List<ResolutionConnection> s_tmpResolutionConnections;

	private static ResolutionConnection s_tmpResolutionConnection;

	protected Settings _settings;

	public event Action OnComplete
	{
		add
		{
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Expected O, but got Unknown
			object obj = this + 40;
			Delegate obj2 = this.m_OnComplete;
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
			object obj = this + 40;
			Delegate obj2 = this.m_OnComplete;
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

	protected unsafe List<DisplayInfo> getDisplayInfos()
	{
		//IL_0008: Expected O, but got Ref
		//IL_00f6: Expected O, but got I
		//IL_0134: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		if (_values != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			object obj3 = default(object);
			if (obj3 != null)
			{
				goto IL_017c;
			}
		}
		List<DisplayInfo> values = new List<DisplayInfo>();
		_values = values;
		Screen.GetDisplayLayout(_values);
		List<DisplayInfo> values2 = _values;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ rax_v11 (System.Collections.Generic.List`1<UnityEngine.DisplayInfo>)+18]");
		if ((nint)0 == 0)
		{
			_ = 0;
			_ = 0;
			_ = 0;
			_ = 0;
			_ = 0;
			_ = 60000;
			_ = 1001;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+67]");
			_ = 0;
			_ = 1920;
			_ = 1080;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-21]");
			object obj4 = (nint)0 >> 32;
			_ = 0;
			_ = 1920;
			if (_values == null)
			{
				return (List<DisplayInfo>)(object)new NullReferenceException();
			}
			DisplayInfo item = (DisplayInfo)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 23));
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-29]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-19]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-9]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+7]");
			_ = 0;
			_values.Add(item);
		}
		goto IL_017c;
		IL_017c:
		return _values;
	}

	public override List<string> GetOptionLabels()
	{
		//IL_0171: Expected I, but got O
		//IL_0235: Expected O, but got I4
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3F053]");
		bool flag = (nint)0 != 0;
		if (_labels != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			object obj = default(object);
			if (obj != null)
			{
				goto IL_01e7;
			}
		}
		List<string> labels = new List<string>();
		_labels = labels;
		List<DisplayInfo> displayInfos = getDisplayInfos();
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		int num = 1;
		List<DisplayInfo>.Enumerator enumerator = default(List<DisplayInfo>.Enumerator);
		string text = default(string);
		string text3 = default(string);
		while (enumerator.MoveNext())
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
			if (_labels != null)
			{
				int num2 = _labels.IndexOf(text);
				bool flag2 = num2 < 0;
				string item = text;
				if (!flag2)
				{
					if (_labels == null)
					{
						throw new NullReferenceException();
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					string text2 = num.ToString();
					string value = text3 + " " + text2;
					_labels.set_Item(num2, value);
					string text4 = num.ToString();
					string text5 = text + " " + text4;
					nint num3 = (nint)typeof(MonitorConnection);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v507 @ rax_v33 (Il2CppClass<Kamgam.SettingsGenerator.MonitorConnection>)+E4]");
					flag = (nint)0 != 0;
					ForceMonitorUpdate = true;
					object obj2 = 0;
					item = text5;
				}
				bool flag3 = _labels == null;
				List<string> labels2 = _labels;
				if (!flag3)
				{
					_labels.Add(item);
					continue;
				}
				throw new NullReferenceException();
			}
			throw new NullReferenceException();
		}
		enumerator.Dispose();
		goto IL_01e7;
		IL_01e7:
		return _labels;
	}

	public override void RefreshOptionLabels()
	{
		//IL_000c: Expected I, but got O
		//IL_001c: Expected O, but got I
		//IL_002c: Expected O, but got I
		_labels = null;
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx_v2 (Il2CppClass<Kamgam.SettingsGenerator.MonitorConnection>)+2C8]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx_v2 (Il2CppClass<Kamgam.SettingsGenerator.MonitorConnection>)+2D0]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v10 @ rax_v2 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	public override void SetOptionLabels(List<string> optionLabels)
	{
		List<DisplayInfo> displayInfos = getDisplayInfos();
		if (optionLabels != null)
		{
			int size = optionLabels._size;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rax_v2 (System.Collections.Generic.List`1<UnityEngine.DisplayInfo>)+18]");
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

	public unsafe override int Get()
	{
		//IL_0121: Expected O, but got I4
		//IL_00d1: Expected I4, but got O
		//IL_00b3: Expected O, but got Ref
		if ((_moveOperation != null && _moveOperation.isDone) || _moveOperationFailed)
		{
			int frameCount = Time.frameCount;
			object obj = frameCount - _lastSetFrame;
			if ((nint)obj > FramesToWaitAfterMonitorSwitch)
			{
				_lastKnownMonitorIndex = -1;
				_moveOperation = null;
				_moveOperationFailed = false;
			}
		}
		if (_lastKnownMonitorIndex < 0)
		{
			List<DisplayInfo> displayInfos = getDisplayInfos();
			DisplayInfo mainWindowDisplayInfo = Screen.mainWindowDisplayInfo;
			object obj2 = default(object);
			if (displayInfos != null)
			{
				return displayInfos.IndexOf((DisplayInfo)(&obj2));
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
		return _lastKnownMonitorIndex;
	}

	public override void Set(int index)
	{
		//IL_0031: Expected I, but got O
		//IL_0041: Expected O, but got I
		//IL_0051: Expected O, but got I
		int frameCount = Time.frameCount;
		_lastSetFrame = frameCount;
		_lastKnownMonitorIndex = index;
		moveToMonitor(index);
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v13 @ r8_v2 (Il2CppClass<Kamgam.SettingsGenerator.MonitorConnection>)+258]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v13 @ r8_v2 (Il2CppClass<Kamgam.SettingsGenerator.MonitorConnection>)+260]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v16 @ rax_v3 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	private unsafe void moveToMonitor(int index)
	{
		//IL_0071: Expected O, but got I4
		//IL_007c: Expected O, but got I4
		//IL_00b8: Expected O, but got Ref
		_moveOperationFailed = false;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
		DisplayInfo mainWindowDisplayInfo = Screen.mainWindowDisplayInfo;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm1,8\"");
		string text = default(string);
		if (!(mainWindowDisplayInfo.name == text) || ForceMonitorUpdate)
		{
			int width = Screen.width;
			int height = Screen.height;
			_windowSizeBeforeMonitorChange = (Vector2Int)width;
			_resolutionBeforeMonitorChange = (Resolution?)(object)0;
			_ = 0;
			if (TryToPreserveResolutionOnMonitorChange)
			{
				Resolution currentResolution = Screen.currentResolution;
				int num = default(int);
				Resolution? resolutionBeforeMonitorChange = (Resolution)(&num);
				_resolutionBeforeMonitorChange = resolutionBeforeMonitorChange;
				_ = 0;
			}
			DisplayInfo display = default(DisplayInfo);
			AsyncOperation moveOperation = Screen.MoveMainWindowTo(ref display, Vector2Int.s_Zero);
			_moveOperation = moveOperation;
			AsyncVoidMethodBuilder asyncVoidMethodBuilder = AsyncVoidMethodBuilder.Create();
			AsyncVoidMethodBuilder asyncVoidMethodBuilder2 = default(AsyncVoidMethodBuilder);
			_003CwaitForMonitorSwitchToComplete_003Ed__26 stateMachine = default(_003CwaitForMonitorSwitchToComplete_003Ed__26);
			asyncVoidMethodBuilder2.Start(ref stateMachine);
		}
	}

	private void waitForMonitorSwitchToComplete()
	{
		AsyncVoidMethodBuilder asyncVoidMethodBuilder = AsyncVoidMethodBuilder.Create();
		AsyncVoidMethodBuilder asyncVoidMethodBuilder2 = default(AsyncVoidMethodBuilder);
		_003CwaitForMonitorSwitchToComplete_003Ed__26 stateMachine = default(_003CwaitForMonitorSwitchToComplete_003Ed__26);
		asyncVoidMethodBuilder2.Start(ref stateMachine);
	}

	protected void updateResolutionConnectionToClosetsResolution(ResolutionConnection connection, Settings settings, int width, int height, int refreshRate)
	{
		connection.ClearResolutionCache();
		int height2 = default(int);
		int refreshRate2 = default(int);
		int num = connection.FindClosestResolutionIndex(width, height2, refreshRate2);
		if (num >= 0)
		{
			connection.Set(num);
			IList<ISetting> settingsWithConnection = settings.GetSettingsWithConnection(connection);
			IList<ISetting> list = SettingCollectionExtensions.PullFromConnection(settingsWithConnection);
			IList<ISetting> list2 = SettingCollectionExtensions.RefreshRegisteredResolvers(settingsWithConnection, settings);
		}
	}

	public void SetSettings(Settings settings)
	{
		_settings = settings;
	}

	public Settings GetSettings()
	{
		return _settings;
	}

	static MonitorConnection()
	{
		List<SettingOption> list = new List<SettingOption>();
		s_tmpOptionSettingsList = list;
		List<ResolutionConnection> list2 = new List<ResolutionConnection>();
		s_tmpResolutionConnections = list2;
	}
}
