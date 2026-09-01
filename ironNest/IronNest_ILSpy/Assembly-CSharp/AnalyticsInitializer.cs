using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Cpp2ILInjected;
using Unity.Services.Analytics;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;

public class AnalyticsInitializer : MonoBehaviour
{
	[StructLayout((LayoutKind)3)]
	private struct _003CInitializeAsync_003Ed__12 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public AnalyticsInitializer _003C_003E4__this;

		private TaskAwaiter _003C_003Eu__1;

		private TaskAwaiter<int> _003C_003Eu__2;

		private unsafe void MoveNext()
		{
			//IL_008a: Expected O, but got I4
			//IL_0099: Expected I4, but got I8
			//IL_00d5: Expected O, but got I4
			//IL_00e4: Expected I4, but got I8
			//IL_069e: Expected I4, but got I8
			//IL_05aa: Expected O, but got Ref
			//IL_025c: Expected O, but got I4
			//IL_0554: Expected O, but got Ref
			//IL_058c: Expected O, but got Ref
			//IL_021b: Expected O, but got I4
			//IL_0492: Expected O, but got Ref
			AnalyticsInitializer analyticsInitializer = _003C_003E4__this;
			if (_003C_003E1__state > 1 && analyticsInitializer._initialized)
			{
				if (analyticsInitializer.enableDebugLogs)
				{
					Debug.Log("[AnalyticsInitializer] Already initialized. Skipping.");
				}
				goto IL_068f;
			}
			TaskAwaiter<int> taskAwaiter2 = default(TaskAwaiter<int>);
			if (_003C_003E1__state == 0)
			{
				_003C_003Eu__1 = (TaskAwaiter)0;
				_003C_003E1__state = -1;
				TaskAwaiter taskAwaiter = _003C_003Eu__1;
			}
			else
			{
				if (_003C_003E1__state == 1)
				{
					_003C_003Eu__2 = (TaskAwaiter<int>)0;
					_003C_003E1__state = -1;
					taskAwaiter2 = _003C_003Eu__2;
					goto IL_032f;
				}
				InitializationOptions initializationOptions = new InitializationOptions();
				if ((object)analyticsInitializer == null)
				{
					throw new NullReferenceException();
				}
				string text;
				string text2;
				if (string.IsNullOrWhiteSpace(analyticsInitializer.environmentName))
				{
					text = "production";
					text2 = "production";
				}
				else
				{
					string text3 = analyticsInitializer.environmentName.Trim();
					string text4 = text3.ToLowerInvariant();
					text = text4;
					text2 = "production";
				}
				if (text != text2 && text != "development")
				{
					if (analyticsInitializer.enableDebugLogs)
					{
						string message = "[AnalyticsInitializer] Invalid environment '" + analyticsInitializer.environmentName + "'. Falling back to 'production'.";
						Debug.LogWarning(message);
						object obj = 0;
					}
					text = "production";
				}
				InitializationOptions initializationOptions2 = EnvironmentsOptionsExtensions.SetEnvironmentName(initializationOptions, text);
				if (analyticsInitializer.enableDebugLogs)
				{
					string message2 = "[AnalyticsInitializer] Initializing Unity Services (env: " + text + ")...";
					Debug.Log(message2);
					object obj = 0;
				}
				Task task = UnityServices.InitializeAsync(initializationOptions);
				TaskAwaiter awaiter = task.GetAwaiter();
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807D2870");
				object obj2 = default(object);
				if (obj2 == null)
				{
					_003C_003E1__state = 0;
					TaskAwaiter taskAwaiter = default(TaskAwaiter);
					_003C_003Eu__1 = taskAwaiter;
					AsyncTaskMethodBuilder asyncTaskMethodBuilder = (AsyncTaskMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
					((AsyncTaskMethodBuilder*)asyncTaskMethodBuilder)->AwaitUnsafeOnCompleted(ref taskAwaiter, ref this);
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180CF3BB0");
			AsyncTaskMethodBuilder<int> asyncTaskMethodBuilder2 = AsyncTaskMethodBuilder<int>.Create();
			AsyncTaskMethodBuilder<int> asyncTaskMethodBuilder3 = default(AsyncTaskMethodBuilder<int>);
			_003CTryCheckForRequiredConsentsAsync_003Ed__18 stateMachine = default(_003CTryCheckForRequiredConsentsAsync_003Ed__18);
			asyncTaskMethodBuilder3.Start(ref stateMachine);
			Task<int> task2 = asyncTaskMethodBuilder3.Task;
			TaskAwaiter<int> awaiter2 = task2.GetAwaiter();
			if (taskAwaiter2.IsCompleted)
			{
				goto IL_032f;
			}
			_003C_003E1__state = 1;
			_003C_003Eu__2 = taskAwaiter2;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder4 = (AsyncTaskMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			((AsyncTaskMethodBuilder*)asyncTaskMethodBuilder4)->AwaitUnsafeOnCompleted(ref taskAwaiter2, ref this);
			return;
			IL_068f:
			_003C_003E1__state = -2;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder5 = (AsyncTaskMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			((AsyncTaskMethodBuilder*)asyncTaskMethodBuilder5)->SetResult();
			return;
			IL_032f:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091FAF0");
			object obj3 = default(object);
			if (obj3 == null || (nint)obj3 <= 0)
			{
				if (analyticsInitializer.autoStartDataCollection)
				{
					analyticsInitializer.TryStartDataCollection();
				}
			}
			else if (analyticsInitializer.enableDebugLogs)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg = default(object);
				string message3 = $"[AnalyticsInitializer] {arg} consent(s) required. Call ProvideAllConsentsAndStart(true) after collecting user consent.";
				Debug.Log(message3);
			}
			analyticsInitializer._initialized = true;
			if (analyticsInitializer.sendTestEventOnFirstInit && !analyticsInitializer._testEventSent)
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				string version = Application.version;
				dictionary.Add("app_version", version);
				RuntimePlatform platform = Application.platform;
				IntPtr intPtr = default(IntPtr);
				string value = ((Enum)(&intPtr)).ToString();
				dictionary.Add("platform", value);
				string unityVersion = Application.unityVersion;
				dictionary.Add("unity", unityVersion);
				string value2 = analyticsInitializer.TryGetSessionIdSafe();
				dictionary.Add("session_id", value2);
				analyticsInitializer.SendCustomEvent("app_launch", dictionary);
				analyticsInitializer._testEventSent = true;
			}
			if (analyticsInitializer.enableDebugLogs)
			{
				Debug.Log("[AnalyticsInitializer] Analytics initialization complete.");
			}
			goto IL_068f;
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			this.MoveNext();
		}

		private unsafe void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//IL_0010: Expected O, but got Ref
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = (AsyncTaskMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			((AsyncTaskMethodBuilder*)asyncTaskMethodBuilder)->SetStateMachine(stateMachine);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
			this.SetStateMachine(stateMachine);
		}
	}

	[StructLayout((LayoutKind)3)]
	private struct _003CProvideAllConsentsAndStart_003Ed__13 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public AnalyticsInitializer _003C_003E4__this;

		public bool grantConsent;

		private TaskAwaiter<int> _003C_003Eu__1;

		private unsafe void MoveNext()
		{
			//IL_0010: Expected O, but got I4
			//IL_001f: Expected I4, but got I8
			//IL_025c: Expected I4, but got I8
			//IL_01f6: Expected O, but got Ref
			//IL_01d8: Expected O, but got Ref
			AnalyticsInitializer analyticsInitializer = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003Eu__1 = (TaskAwaiter<int>)0;
				_003C_003E1__state = -1;
				TaskAwaiter<int> taskAwaiter = _003C_003Eu__1;
			}
			else
			{
				if ((object)_003C_003E4__this == null)
				{
					throw new NullReferenceException();
				}
				AsyncTaskMethodBuilder<int> asyncTaskMethodBuilder = AsyncTaskMethodBuilder<int>.Create();
				AsyncTaskMethodBuilder<int> asyncTaskMethodBuilder2 = default(AsyncTaskMethodBuilder<int>);
				_003CTryProvideConsentsAsync_003Ed__19 stateMachine = default(_003CTryProvideConsentsAsync_003Ed__19);
				asyncTaskMethodBuilder2.Start(ref stateMachine);
				Task<int> task = asyncTaskMethodBuilder2.Task;
				TaskAwaiter<int> awaiter = task.GetAwaiter();
				TaskAwaiter<int> taskAwaiter = default(TaskAwaiter<int>);
				if (!taskAwaiter.IsCompleted)
				{
					_003C_003E1__state = 0;
					_003C_003Eu__1 = taskAwaiter;
					AsyncTaskMethodBuilder asyncTaskMethodBuilder3 = (AsyncTaskMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
					((AsyncTaskMethodBuilder*)asyncTaskMethodBuilder3)->AwaitUnsafeOnCompleted(ref taskAwaiter, ref this);
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091FAF0");
			if (!grantConsent)
			{
				if (analyticsInitializer.enableDebugLogs)
				{
					Debug.LogWarning("[AnalyticsInitializer] Consent denied. Data collection will not start.");
				}
			}
			else
			{
				_003C_003E4__this.TryStartDataCollection();
				if (analyticsInitializer.enableDebugLogs)
				{
					object obj = default(object);
					if ((nint)obj < 0)
					{
						Debug.Log("[AnalyticsInitializer] Consent API not found. Started data collection best-effort.");
					}
					else
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
						object arg = default(object);
						string message = $"[AnalyticsInitializer] Provided {arg} consent(s) and started data collection.";
						Debug.Log(message);
					}
				}
			}
			_003C_003E1__state = -2;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder4 = (AsyncTaskMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			((AsyncTaskMethodBuilder*)asyncTaskMethodBuilder4)->SetResult();
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			this.MoveNext();
		}

		private unsafe void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//IL_0010: Expected O, but got Ref
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = (AsyncTaskMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			((AsyncTaskMethodBuilder*)asyncTaskMethodBuilder)->SetStateMachine(stateMachine);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
			this.SetStateMachine(stateMachine);
		}
	}

	[StructLayout((LayoutKind)3)]
	private struct _003CTryCheckForRequiredConsentsAsync_003Ed__18 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder<int> _003C_003Et__builder;

		public AnalyticsInitializer _003C_003E4__this;

		private Task _003Ctask_003E5__2;

		private TaskAwaiter _003C_003Eu__1;

		private unsafe void MoveNext()
		{
			//IL_0010: Expected O, but got I4
			//IL_001f: Expected I4, but got I8
			//IL_081f: Expected I4, but got I8
			//IL_062b: Expected O, but got Ref
			//IL_0376: Expected I, but got O
			//IL_0163: Expected I, but got O
			//IL_0121: Expected I, but got O
			//IL_0362: Expected I, but got O
			//IL_017c: Expected I, but got O
			//IL_018a: Expected I, but got O
			//IL_019a: Expected O, but got I
			//IL_01d6: Expected O, but got I
			//IL_01fb: Expected O, but got I4
			//IL_0844: Expected I, but got O
			//IL_0854: Expected O, but got I
			//IL_0257: Expected O, but got I4
			//IL_0495: Expected I, but got O
			//IL_03a0: Expected O, but got Ref
			//IL_051c: Expected O, but got I4
			//IL_080b: Expected I, but got O
			//IL_04cd: Expected O, but got I
			//IL_054c: Expected O, but got I
			//IL_0563: Unknown result type (might be due to invalid IL or missing references)
			//IL_0568: Expected O, but got Unknown
			//IL_0570: Unknown result type (might be due to invalid IL or missing references)
			//IL_0575: Expected O, but got Unknown
			//IL_04e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_04e5: Expected O, but got Unknown
			AnalyticsInitializer analyticsInitializer = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003Eu__1 = (TaskAwaiter)0;
				_003C_003E1__state = -1;
				TaskAwaiter taskAwaiter = _003C_003Eu__1;
				goto IL_02aa;
			}
			IAnalyticsService instance = AnalyticsService.Instance;
			object obj7;
			object obj3;
			object[] array;
			if (instance != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_vm_object_is_inst\"");
				Type type = default(Type);
				MethodInfo method = type.GetMethod("CheckForRequiredConsents", (BindingFlags)20);
				bool flag = (object)method != null;
				MethodInfo methodInfo = method;
				if (!flag)
				{
					MethodInfo method2 = type.GetMethod("CheckForRequiredConsentsAsync", (BindingFlags)20);
					methodInfo = method2;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D12FD0");
				object obj = default(object);
				if (obj == null)
				{
					ParameterInfo[] parameters = methodInfo.GetParameters();
					if (parameters.Length == 0)
					{
						object obj2 = methodInfo.Invoke(instance, null);
						obj3 = obj2;
						array = null;
						nint num = unchecked((nint)null);
					}
					else
					{
						object[] array2 = new object[0];
						object obj4 = methodInfo.Invoke(instance, array2);
						obj3 = obj4;
						array = array2;
						nint num = unchecked((nint)null);
					}
					if (obj3 == null)
					{
						_003Ctask_003E5__2 = null;
						goto IL_0729;
					}
					nint num2 = (nint)obj3;
					nint num3 = (nint)typeof(Task);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1022 @ rdx_v55 (Il2CppClass<System.Threading.Tasks.Task>)+130]");
					object obj5 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1021 @ r8_v35 (Il2CppClass<System.Object>)+130]");
					nint num4 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1022 @ rdx_v55 (Il2CppClass<System.Threading.Tasks.Task>)+130]");
					if (num4 >= 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1021 @ r8_v35 (Il2CppClass<System.Object>)+C8]");
						object obj6 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1075 @ rax_v101+FFFFFFF8+v1023 @ rax_v89*8]");
						bool flag2 = 0 == (nint)typeof(Task);
						obj7 = 1;
						if (flag2)
						{
							goto IL_0748;
						}
					}
					obj7 = null;
					goto IL_0748;
				}
				if (analyticsInitializer.enableDebugLogs)
				{
					Debug.Log("[AnalyticsInitializer] Consent check API not found. Skipping consent detection.");
				}
				goto IL_0810;
			}
			throw new NullReferenceException();
			IL_02aa:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180CF3BB0");
			if (_003Ctask_003E5__2 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_vm_object_is_inst\"");
				Type type2 = default(Type);
				if ((object)type2 != null)
				{
					PropertyInfo property = type2.GetProperty("Result", (BindingFlags)20);
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D16FE0");
					object obj8 = default(object);
					if (obj8 != null)
					{
						object value = property.GetValue(_003Ctask_003E5__2);
						obj3 = value;
						array = null;
						nint num = unchecked((nint)null);
					}
					else
					{
						obj3 = null;
						array = null;
						nint num = unchecked((nint)null);
					}
					goto IL_03b3;
				}
				goto IL_0782;
			}
			throw new NullReferenceException();
			IL_03b3:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
			object obj9 = default(object);
			if (obj9 == null)
			{
				if ((object)analyticsInitializer == null)
				{
					throw new NullReferenceException();
				}
				if ((analyticsInitializer.enableDebugLogs ? 1 : 0) != (nint)obj9)
				{
					Debug.Log("[AnalyticsInitializer] Consent check returned unknown type. Assuming no extra consent required.");
				}
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				PropertyInfo propertyInfo = null;
				PropertyInfo propertyInfo2 = default(PropertyInfo);
				object obj10 = default(object);
				object obj20 = default(object);
				while (true)
				{
					object obj11;
					object obj19;
					if ((object)propertyInfo2 != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
						if (obj10 == null)
						{
							break;
						}
						bool flag3 = (object)propertyInfo2 == null;
						propertyInfo = null;
						if (!flag3)
						{
							nint num5 = (nint)propertyInfo2;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v950 @ r10_v12 (Il2CppClass<System.Reflection.PropertyInfo>)+12E]");
							if ((nint)0 < (nint)0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v950 @ r10_v12 (Il2CppClass<System.Reflection.PropertyInfo>)+B0]");
								obj11 = 0;
								object obj12 = null;
								while (true)
								{
									object obj13 = obj12 + obj12;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v953 @ r8_v19+v1157 @ rax_v52*8]");
									if (0 == (nint)typeof(IEnumerator))
									{
										break;
									}
									obj12++;
									object obj14 = obj12;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v950 @ r10_v12 (Il2CppClass<System.Reflection.PropertyInfo>)+12E]");
									if ((nint)obj14 < 0)
									{
										continue;
									}
									goto IL_0509;
								}
								object obj15 = obj12 + obj12;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v953 @ r8_v19+8+v1260 @ rcx_v33*8]");
								object obj16 = (nint)0 + (nint)1;
								object obj17 = obj16 << 4;
								object obj18 = obj17 + 312;
								obj19 = obj18 + num5;
								goto IL_07f2;
							}
							goto IL_0509;
						}
						throw new NullReferenceException();
					}
					throw new NullReferenceException();
					IL_07f2:
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v1267 @ rdx_v23] (should have been resolved before IL gen)");
					nint num = (nint)typeof(IEnumerator);
					continue;
					IL_0509:
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
					obj11 = 1;
					obj19 = obj20;
					goto IL_07f2;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803DB4A0");
				if (analyticsInitializer.enableDebugLogs)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					object arg = default(object);
					string message = $"[AnalyticsInitializer] Consent check returned {arg} required consent(s).";
					Debug.Log(message);
				}
			}
			goto IL_0810;
			IL_0810:
			_003C_003E1__state = -2;
			AsyncTaskMethodBuilder<int> asyncTaskMethodBuilder = (AsyncTaskMethodBuilder<int>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			object obj21 = default(object);
			((AsyncTaskMethodBuilder<int>*)asyncTaskMethodBuilder)->SetResult((int)(&obj21));
			return;
			IL_0729:
			if (_003Ctask_003E5__2 != null)
			{
				TaskAwaiter awaiter = _003Ctask_003E5__2.GetAwaiter();
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807D2870");
				object obj22 = default(object);
				if (obj22 != null)
				{
					goto IL_02aa;
				}
				_003C_003E1__state = 0;
				TaskAwaiter taskAwaiter = default(TaskAwaiter);
				_003C_003Eu__1 = taskAwaiter;
				AsyncTaskMethodBuilder<int> asyncTaskMethodBuilder2 = (AsyncTaskMethodBuilder<int>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
				((AsyncTaskMethodBuilder<int>*)asyncTaskMethodBuilder2)->AwaitUnsafeOnCompleted(ref taskAwaiter, ref this);
				return;
			}
			goto IL_03b3;
			IL_076a:
			object obj23;
			if (obj23 == null)
			{
				goto IL_0729;
			}
			goto IL_0782;
			IL_0748:
			bool flag4 = obj7 == null;
			object obj24 = null;
			if (!flag4)
			{
				obj24 = obj3;
			}
			_003Ctask_003E5__2 = (Task)obj24;
			array = (object[])obj3;
			nint num6 = (nint)typeof(Task);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1141 @ rdx_v56 (Il2CppClass<System.Threading.Tasks.Task>)+130]");
			object obj25 = 0;
			object obj26 = array[34];
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1141 @ rdx_v56 (Il2CppClass<System.Threading.Tasks.Task>)+130]");
			if ((nint)obj26 >= 0)
			{
				object obj27 = array[21];
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1188 @ rax_v97 (System.Object)+FFFFFFF8+v1142 @ rax_v93*8]");
				bool flag5 = 0 == (nint)typeof(Task);
				obj23 = 1;
				if (flag5)
				{
					goto IL_076a;
				}
			}
			obj23 = null;
			goto IL_076a;
			IL_0782:
			throw new NullReferenceException();
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			this.MoveNext();
		}

		private unsafe void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//IL_0010: Expected O, but got Ref
			AsyncTaskMethodBuilder<int> asyncTaskMethodBuilder = (AsyncTaskMethodBuilder<int>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			((AsyncTaskMethodBuilder<int>*)asyncTaskMethodBuilder)->SetStateMachine(stateMachine);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
			this.SetStateMachine(stateMachine);
		}
	}

	[StructLayout((LayoutKind)3)]
	private struct _003CTryProvideConsentsAsync_003Ed__19 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder<int> _003C_003Et__builder;

		public AnalyticsInitializer _003C_003E4__this;

		public bool grant;

		private IAnalyticsService _003Cservice_003E5__2;

		private Type _003Ct_003E5__3;

		private IEnumerable _003Crequired_003E5__4;

		private Task _003Ctask_003E5__5;

		private TaskAwaiter _003C_003Eu__1;

		private unsafe void MoveNext()
		{
			//IL_0010: Expected O, but got I4
			//IL_001f: Expected I4, but got I8
			//IL_0971: Expected O, but got I4
			//IL_00b8: Expected I, but got O
			//IL_02cc: Expected O, but got I4
			//IL_00ec: Expected I, but got O
			//IL_010e: Expected O, but got I4
			//IL_0862: Expected I, but got O
			//IL_0356: Expected I, but got O
			//IL_0342: Expected I, but got O
			//IL_0171: Expected I, but got O
			//IL_017f: Expected I, but got O
			//IL_018f: Expected O, but got I
			//IL_0b86: Expected I4, but got I8
			//IL_083c: Expected O, but got Ref
			//IL_0442: Expected O, but got I4
			//IL_01cb: Expected O, but got I
			//IL_01f0: Expected O, but got I4
			//IL_0b39: Expected I, but got O
			//IL_0b49: Expected O, but got I
			//IL_03a0: Expected I, but got O
			//IL_024c: Expected O, but got I4
			//IL_0380: Expected O, but got Ref
			//IL_02a4: Expected I, but got O
			//IL_0525: Expected O, but got I4
			//IL_04d6: Expected O, but got I
			//IL_054a: Expected O, but got I4
			//IL_0558: Expected I, but got O
			//IL_05eb: Expected O, but got I
			//IL_0602: Unknown result type (might be due to invalid IL or missing references)
			//IL_0607: Expected O, but got Unknown
			//IL_063b: Expected I, but got O
			//IL_04e9: Unknown result type (might be due to invalid IL or missing references)
			//IL_04ee: Expected O, but got Unknown
			//IL_056e: Expected I, but got O
			//IL_057e: Expected O, but got I
			//IL_05a5: Expected I, but got O
			//IL_05ad: Expected I, but got O
			//IL_0a87: Expected O, but got I4
			//IL_05c3: Expected I, but got O
			//IL_0690: Expected I, but got O
			//IL_06a0: Expected O, but got I
			//IL_06c7: Expected I, but got O
			//IL_0711: Expected O, but got I
			//IL_071a: Unknown result type (might be due to invalid IL or missing references)
			//IL_071f: Expected O, but got Unknown
			//IL_078e: Expected O, but got I4
			//IL_07e7: Expected O, but got I4
			AnalyticsInitializer analyticsInitializer = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003Eu__1 = (TaskAwaiter)0;
				_003C_003E1__state = -1;
				TaskAwaiter taskAwaiter = _003C_003Eu__1;
				goto IL_0943;
			}
			IAnalyticsService instance = AnalyticsService.Instance;
			_003Cservice_003E5__2 = instance;
			object obj3;
			object obj6;
			PropertyInfo propertyInfo;
			object obj2;
			object[] array;
			if (_003Cservice_003E5__2 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_vm_object_is_inst\"");
				Type type = default(Type);
				_003Ct_003E5__3 = type;
				MethodInfo method = _003Ct_003E5__3.GetMethod("CheckForRequiredConsents", (BindingFlags)20);
				bool flag = (object)method != null;
				MethodInfo methodInfo = method;
				nint num = unchecked((nint)null);
				if (!flag)
				{
					MethodInfo method2 = _003Ct_003E5__3.GetMethod("CheckForRequiredConsentsAsync", (BindingFlags)20);
					methodInfo = method2;
					num = unchecked((nint)null);
				}
				_003Crequired_003E5__4 = null;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D16FE0");
				object obj = default(object);
				if (obj != null)
				{
					bool flag2 = (object)methodInfo == null;
					array = null;
					obj2 = 0;
					propertyInfo = (PropertyInfo)(object)methodInfo;
					if (!flag2)
					{
						obj3 = methodInfo.Invoke(_003Cservice_003E5__2, null);
						if (obj3 == null)
						{
							_003Ctask_003E5__5 = null;
							array = null;
							goto IL_09ad;
						}
						nint num2 = (nint)obj3;
						nint num3 = (nint)typeof(Task);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1121 @ rdx_v78 (Il2CppClass<System.Threading.Tasks.Task>)+130]");
						object obj4 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1120 @ r8_v47 (Il2CppClass<System.Object>)+130]");
						nint num4 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1121 @ rdx_v78 (Il2CppClass<System.Threading.Tasks.Task>)+130]");
						if (num4 >= 0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1120 @ r8_v47 (Il2CppClass<System.Object>)+C8]");
							object obj5 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1215 @ rax_v128+FFFFFFF8+v1122 @ rax_v116*8]");
							bool flag3 = 0 == (nint)typeof(Task);
							obj6 = 1;
							if (flag3)
							{
								goto IL_09cc;
							}
						}
						obj6 = null;
						goto IL_09cc;
					}
					nint num5 = (nint)propertyInfo;
					throw new NullReferenceException();
				}
				goto IL_03c7;
			}
			throw new NullReferenceException();
			IL_09ee:
			object obj7;
			if (obj7 == null)
			{
				goto IL_09ad;
			}
			goto IL_0a06;
			IL_09ad:
			if (_003Ctask_003E5__5 != null)
			{
				TaskAwaiter awaiter = _003Ctask_003E5__5.GetAwaiter();
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807D2870");
				object obj8 = default(object);
				if (obj8 != null)
				{
					nint num = unchecked((nint)null);
					goto IL_0943;
				}
				_003C_003E1__state = 0;
				TaskAwaiter taskAwaiter = default(TaskAwaiter);
				_003C_003Eu__1 = taskAwaiter;
				AsyncTaskMethodBuilder<int> asyncTaskMethodBuilder = (AsyncTaskMethodBuilder<int>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
				((AsyncTaskMethodBuilder<int>*)asyncTaskMethodBuilder)->AwaitUnsafeOnCompleted(ref taskAwaiter, ref this);
				return;
			}
			object obj9 = obj3;
			nint num6 = unchecked((nint)null);
			goto IL_0a11;
			IL_03c7:
			MethodInfo method3 = _003Ct_003E5__3.GetMethod("ProvideOptInConsent", (BindingFlags)20);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D12FD0");
			object obj10 = default(object);
			if (obj10 == null)
			{
				if (_003Crequired_003E5__4 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					object obj11 = 0;
					MethodBase methodBase = null;
					object obj12 = default(object);
					object obj13 = default(object);
					object obj23 = default(object);
					object obj24 = default(object);
					IntPtr intPtr = default(IntPtr);
					IntPtr intPtr2 = default(IntPtr);
					object obj25 = default(object);
					MethodBase methodBase2 = default(MethodBase);
					object obj27 = default(object);
					while (true)
					{
						object obj22;
						if (obj12 != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
							if (obj13 == null)
							{
								break;
							}
							bool flag4 = obj12 == null;
							methodBase = null;
							if (!flag4)
							{
								object obj14 = obj12;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v340 @ r10_v5+12E]");
								if ((nint)0 >= (nint)0)
								{
									goto IL_0512;
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v340 @ r10_v5+B0]");
								array = (object[])0;
								object obj15 = null;
								while (true)
								{
									object obj16 = obj15 + obj15;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v610 @ r8_v37 (System.Object[])+v1575 @ rax_v48*8]");
									if (0 == (nint)typeof(IEnumerator))
									{
										break;
									}
									obj15++;
									object obj17 = obj15;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v340 @ r10_v5+12E]");
									if ((nint)obj17 < 0)
									{
										continue;
									}
									goto IL_0512;
								}
								object obj18 = obj15 + obj15;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v610 @ r8_v37 (System.Object[])+8+v1631 @ rcx_v37*8]");
								object obj19 = (nint)0 + (nint)1;
								object obj20 = obj19 << 4;
								object obj21 = obj20 + 312;
								obj22 = obj21 + obj14;
								goto IL_0a99;
							}
							throw new NullReferenceException();
						}
						throw new NullReferenceException();
						IL_0a99:
						Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v1638 @ rdx_v15] (should have been resolved before IL gen)");
						object[] array2 = new object[2];
						bool flag5 = array2 == null;
						methodBase = (MethodBase)(object)typeof(object[]);
						if (!flag5)
						{
							bool flag6 = obj23 == null;
							obj2 = 2;
							nint num7 = (nint)typeof(object[]);
							nint num;
							if (!flag6)
							{
								nint num8 = (nint)array2;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1665 @ rdx_v28 (Il2CppClass<System.Object[]>)+40]");
								obj2 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
								bool flag7 = obj24 == null;
								num = (nint)typeof(IEnumerator);
								nint num5 = (nint)obj23;
								if (flag7)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
									obj2 = 0;
									num7 = intPtr;
									throw intPtr;
								}
								num7 = (nint)obj23;
							}
							bool flag8 = array2.Length <= 0;
							num = (nint)typeof(IEnumerator);
							if (!flag8)
							{
								array2[0] = obj23;
								Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
								if (intPtr2 != (IntPtr)0)
								{
									nint num9 = (nint)array2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1694 @ rdx_v26 (Il2CppClass<System.Object[]>)+40]");
									obj2 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
									bool flag9 = obj25 == null;
									num = (nint)typeof(IEnumerator);
									num7 = intPtr2;
									if (flag9)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
										throw methodBase2;
									}
								}
								if (array2.Length > 1)
								{
									array2[1] = (nint)intPtr2;
									methodBase = (MethodBase)(array2 + 40);
									if ((object)method3 != null)
									{
										object obj26 = method3.Invoke(_003Cservice_003E5__2, array2);
										if ((object)analyticsInitializer != null)
										{
											bool flag10 = !analyticsInitializer.enableDebugLogs;
											obj11 = 0;
											if (!flag10)
											{
												bool flag11 = grant;
												object arg = "granted";
												if (!flag11)
												{
													arg = "denied";
												}
												string message = $"[AnalyticsInitializer] Provided consent '{obj23}': {arg}";
												Debug.Log(message);
												obj11 = 0;
											}
											continue;
										}
										throw new NullReferenceException();
									}
									throw new NullReferenceException();
								}
								throw new IndexOutOfRangeException();
							}
							throw new IndexOutOfRangeException();
						}
						throw new NullReferenceException();
						IL_0512:
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
						array = (object[])1;
						obj22 = obj27;
						goto IL_0a99;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803DB4A0");
				}
			}
			else if (analyticsInitializer.enableDebugLogs)
			{
				Debug.Log("[AnalyticsInitializer] ProvideOptInConsent() not available in this Analytics package version.");
			}
			_003C_003E1__state = -2;
			AsyncTaskMethodBuilder<int> asyncTaskMethodBuilder2 = (AsyncTaskMethodBuilder<int>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			object obj28 = default(object);
			((AsyncTaskMethodBuilder<int>*)asyncTaskMethodBuilder2)->SetResult((int)(&obj28));
			return;
			IL_0a06:
			throw new NullReferenceException();
			IL_09cc:
			bool flag12 = obj6 == null;
			object obj29 = null;
			if (!flag12)
			{
				obj29 = obj3;
			}
			_003Ctask_003E5__5 = (Task)obj29;
			array = (object[])obj3;
			nint num10 = (nint)typeof(Task);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1303 @ rdx_v79 (Il2CppClass<System.Threading.Tasks.Task>)+130]");
			object obj30 = 0;
			object obj31 = array[34];
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1303 @ rdx_v79 (Il2CppClass<System.Threading.Tasks.Task>)+130]");
			if ((nint)obj31 >= 0)
			{
				object obj32 = array[21];
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1373 @ rax_v124 (System.Object)+FFFFFFF8+v1304 @ rax_v120*8]");
				bool flag13 = 0 == (nint)typeof(Task);
				obj7 = 1;
				if (flag13)
				{
					goto IL_09ee;
				}
			}
			obj7 = null;
			goto IL_09ee;
			IL_0943:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180CF3BB0");
			propertyInfo = (PropertyInfo)(object)_003Ctask_003E5__5;
			bool flag14 = _003Ctask_003E5__5 == null;
			obj2 = 0;
			if (!flag14)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_vm_object_is_inst\"");
				Type type2 = default(Type);
				bool flag15 = (object)type2 == null;
				obj2 = 0;
				if (flag15)
				{
					goto IL_0a06;
				}
				PropertyInfo property = type2.GetProperty("Result", (BindingFlags)20);
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D16FE0");
				object obj33 = default(object);
				if (obj33 != null)
				{
					object value = property.GetValue(_003Ctask_003E5__5);
					obj9 = value;
					array = null;
					num6 = unchecked((nint)null);
				}
				else
				{
					obj9 = null;
					array = null;
					num6 = unchecked((nint)null);
				}
				goto IL_0a11;
			}
			throw new NullReferenceException();
			IL_0a11:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
			IEnumerable enumerable = default(IEnumerable);
			if (enumerable != null)
			{
				_003Crequired_003E5__4 = enumerable;
			}
			_003Ctask_003E5__5 = null;
			goto IL_03c7;
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			this.MoveNext();
		}

		private unsafe void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//IL_0010: Expected O, but got Ref
			AsyncTaskMethodBuilder<int> asyncTaskMethodBuilder = (AsyncTaskMethodBuilder<int>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			((AsyncTaskMethodBuilder<int>*)asyncTaskMethodBuilder)->SetStateMachine(stateMachine);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
			this.SetStateMachine(stateMachine);
		}
	}

	private static AnalyticsInitializer _instance;

	private bool initializeOnAwake;

	private string environmentName;

	private bool autoStartDataCollection;

	private string profileName;

	private bool sendTestEventOnFirstInit;

	private bool enableDebugLogs;

	private bool _initialized;

	private bool _testEventSent;

	public bool IsInitialized => _initialized;

	private void Awake()
	{
		if (_instance != null && _instance != this)
		{
			if (enableDebugLogs)
			{
				Debug.Log("[AnalyticsInitializer] Duplicate instance detected, destroying this one.");
			}
			GameObject obj = base.gameObject;
			UnityEngine.Object.Destroy(obj);
			return;
		}
		_instance = this;
		GameObject target = base.gameObject;
		UnityEngine.Object.DontDestroyOnLoad(target);
		if (initializeOnAwake)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180788D50");
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = default(AsyncTaskMethodBuilder);
			_003CInitializeAsync_003Ed__12 stateMachine = default(_003CInitializeAsync_003Ed__12);
			asyncTaskMethodBuilder.Start(ref stateMachine);
			Task task = asyncTaskMethodBuilder.Task;
		}
	}

	public Task InitializeAsync()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180788D50");
		AsyncTaskMethodBuilder asyncTaskMethodBuilder = default(AsyncTaskMethodBuilder);
		_003CInitializeAsync_003Ed__12 stateMachine = default(_003CInitializeAsync_003Ed__12);
		asyncTaskMethodBuilder.Start(ref stateMachine);
		return asyncTaskMethodBuilder.Task;
	}

	public Task ProvideAllConsentsAndStart(bool grantConsent)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180788D50");
		AsyncTaskMethodBuilder asyncTaskMethodBuilder = default(AsyncTaskMethodBuilder);
		_003CProvideAllConsentsAndStart_003Ed__13 stateMachine = default(_003CProvideAllConsentsAndStart_003Ed__13);
		asyncTaskMethodBuilder.Start(ref stateMachine);
		return asyncTaskMethodBuilder.Task;
	}

	public void SendCustomEvent(string eventName, IDictionary<string, object> parameters = null, bool flushImmediately = false)
	{
		object message2;
		if (_initialized)
		{
			if (!string.IsNullOrWhiteSpace(eventName))
			{
				if (eventName != null)
				{
					string eventName2 = eventName.Trim();
					bool flag = parameters != null;
					Dictionary<string, object> parameters2 = (Dictionary<string, object>)parameters;
					if (!flag)
					{
						Dictionary<string, object> dictionary = new Dictionary<string, object>();
						parameters2 = dictionary;
					}
					bool flag2 = TrySendCustomEvent(eventName2, parameters2);
					if (enableDebugLogs)
					{
						if (!flag2)
						{
							Debug.LogWarning("[AnalyticsInitializer] No supported API found to send custom events in this Analytics package version.");
							return;
						}
						string message = "[AnalyticsInitializer] Custom event sent: " + eventName;
						Debug.Log(message);
					}
					return;
				}
				throw new NullReferenceException();
			}
			if (!enableDebugLogs)
			{
				return;
			}
			message2 = "[AnalyticsInitializer] Event name is empty. Skipping.";
		}
		else
		{
			if (!enableDebugLogs)
			{
				return;
			}
			message2 = "[AnalyticsInitializer] SendCustomEvent called before initialization.";
		}
		Debug.LogWarning(message2);
	}

	private unsafe void SendTestLaunchEvent()
	{
		//IL_003c: Expected O, but got Ref
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		string version = Application.version;
		dictionary.Add("app_version", version);
		RuntimePlatform platform = Application.platform;
		IntPtr intPtr = default(IntPtr);
		string value = ((Enum)(&intPtr)).ToString();
		dictionary.Add("platform", value);
		string unityVersion = Application.unityVersion;
		dictionary.Add("unity", unityVersion);
		string value2 = TryGetSessionIdSafe();
		dictionary.Add("session_id", value2);
		SendCustomEvent("app_launch", dictionary);
		_testEventSent = true;
	}

	private string TryGetSessionIdSafe()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39F1F]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		IAnalyticsService instance = AnalyticsService.Instance;
		if (instance != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_vm_object_is_inst\"");
			Type type = default(Type);
			PropertyInfo property = type.GetProperty("SessionID", (BindingFlags)20);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D16FE0");
			object obj = default(object);
			if (obj != null)
			{
				object value = property.GetValue(instance);
				bool flag = value == null;
				string text = null;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
					bool flag2 = value != null;
					text = null;
					if (!flag2)
					{
						text = (string)value;
					}
				}
				if (!string.IsNullOrEmpty(text))
				{
					return text;
				}
			}
			return "unknown";
		}
		return (string)(object)new NullReferenceException();
	}

	private void TryStartDataCollection()
	{
		IAnalyticsService instance = AnalyticsService.Instance;
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_vm_object_is_inst\"");
		Type type = default(Type);
		MethodInfo method = type.GetMethod("StartDataCollection", (BindingFlags)20);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D16FE0");
		object obj = default(object);
		if (obj == null)
		{
			if ((enableDebugLogs ? 1 : 0) != (nint)obj)
			{
				Debug.Log("[AnalyticsInitializer] StartDataCollection() not available in this Analytics package version. Assuming collection is auto-managed.");
			}
			return;
		}
		object obj2 = method.Invoke(instance, null);
		if (enableDebugLogs)
		{
			Debug.Log("[AnalyticsInitializer] Data collection started.");
		}
	}

	private Task<int> TryCheckForRequiredConsentsAsync()
	{
		AsyncTaskMethodBuilder<int> asyncTaskMethodBuilder = AsyncTaskMethodBuilder<int>.Create();
		AsyncTaskMethodBuilder<int> asyncTaskMethodBuilder2 = default(AsyncTaskMethodBuilder<int>);
		_003CTryCheckForRequiredConsentsAsync_003Ed__18 stateMachine = default(_003CTryCheckForRequiredConsentsAsync_003Ed__18);
		asyncTaskMethodBuilder2.Start(ref stateMachine);
		return asyncTaskMethodBuilder2.Task;
	}

	private Task<int> TryProvideConsentsAsync(bool grant)
	{
		AsyncTaskMethodBuilder<int> asyncTaskMethodBuilder = AsyncTaskMethodBuilder<int>.Create();
		AsyncTaskMethodBuilder<int> asyncTaskMethodBuilder2 = default(AsyncTaskMethodBuilder<int>);
		_003CTryProvideConsentsAsync_003Ed__19 stateMachine = default(_003CTryProvideConsentsAsync_003Ed__19);
		asyncTaskMethodBuilder2.Start(ref stateMachine);
		return asyncTaskMethodBuilder2.Task;
	}

	private bool TrySendCustomEvent(string eventName, IDictionary<string, object> parameters)
	{
		//IL_103a: Expected I4, but got O
		//IL_003d: Expected O, but got I
		//IL_0091: Expected O, but got I
		//IL_0154: Expected O, but got I
		//IL_0185: Expected O, but got I
		//IL_0c41: Expected O, but got I4
		//IL_0c65: Expected I, but got O
		//IL_0c75: Expected O, but got I
		//IL_0cb0: Expected O, but got I
		//IL_079b: Expected O, but got I
		//IL_0d29: Expected I, but got O
		//IL_0d39: Expected O, but got I
		//IL_0291: Expected O, but got I
		//IL_080c: Expected O, but got I
		//IL_083d: Expected O, but got I
		//IL_037b: Expected I, but got O
		//IL_030c: Expected O, but got I
		//IL_0337: Expected I, but got O
		//IL_0347: Expected O, but got I
		//IL_08e7: Expected O, but got I
		//IL_0918: Expected O, but got I
		//IL_046a: Expected I, but got O
		//IL_03fb: Expected O, but got I
		//IL_0426: Expected I, but got O
		//IL_0436: Expected O, but got I
		//IL_0493: Unknown result type (might be due to invalid IL or missing references)
		//IL_0498: Expected O, but got Unknown
		//IL_09e6: Expected O, but got I4
		//IL_0a1a: Expected O, but got I4
		//IL_0a3e: Expected I, but got O
		//IL_0a4e: Expected O, but got I
		//IL_0a89: Expected O, but got I
		//IL_0544: Expected I, but got O
		//IL_054d: Expected O, but got I4
		//IL_055b: Expected I, but got O
		//IL_0ae6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aeb: Expected O, but got Unknown
		//IL_0ebb: Expected O, but got I
		//IL_0ec3: Expected O, but got I
		//IL_0581: Expected O, but got I4
		//IL_0b10: Expected I, but got O
		//IL_0b20: Expected O, but got I
		//IL_0b5b: Expected O, but got I
		//IL_0624: Expected I, but got O
		//IL_0bc8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bcd: Expected O, but got Unknown
		//IL_05a5: Expected I, but got O
		//IL_05b5: Expected O, but got I
		//IL_05e0: Expected I, but got O
		//IL_05f0: Expected O, but got I
		//IL_064d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0652: Expected O, but got Unknown
		//IL_06f6: Expected I, but got O
		//IL_0e9d: Expected I, but got O
		//IL_0677: Expected I, but got O
		//IL_0687: Expected O, but got I
		//IL_06b2: Expected I, but got O
		//IL_06c2: Expected O, but got I
		//IL_0727: Unknown result type (might be due to invalid IL or missing references)
		//IL_072c: Expected I, but got Unknown
		//IL_0745: Expected I, but got O
		IAnalyticsService instance = AnalyticsService.Instance;
		if (instance != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_vm_object_is_inst\"");
			Type[] array = new Type[2];
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
			RuntimeTypeHandle runtimeTypeHandle = (RuntimeTypeHandle)((nint)0 + (nint)32);
			Type typeFromHandle = Type.GetTypeFromHandle(runtimeTypeHandle);
			bool flag = (object)typeFromHandle == null;
			string text = null;
			Type type = (Type)runtimeTypeHandle;
			if (!flag)
			{
				object obj = array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v245 @ rdx_v120+40]");
				text = (string)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj2 = default(object);
				bool flag2 = obj2 == null;
				type = typeFromHandle;
				if (flag2)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					text = null;
					Type type2 = default(Type);
					type = type2;
					throw type2;
				}
			}
			if (array.Length > 0)
			{
				array[0] = typeFromHandle;
				Type typeFromHandle2 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(IDictionary<string, object>));
				bool flag3 = (object)typeFromHandle2 == null;
				string text2 = null;
				Type typeFromHandle3 = typeof(IDictionary<string, object>);
				if (!flag3)
				{
					object obj3 = array;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v352 @ rdx_v118+40]");
					text2 = (string)0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					object obj4 = default(object);
					bool flag4 = obj4 == null;
					typeFromHandle3 = typeFromHandle2;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v352 @ rdx_v118+40]");
					text = (string)0;
					type = typeFromHandle2;
					if (flag4)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
						text2 = null;
						Type type3 = default(Type);
						typeFromHandle3 = type3;
						throw type3;
					}
				}
				IDictionary<string, object> dictionary;
				if (array.Length > 1)
				{
					array[1] = typeFromHandle2;
					Type type4 = default(Type);
					Type[] types = default(Type[]);
					ParameterModifier[] modifiers = default(ParameterModifier[]);
					MethodInfo method = type4.GetMethod("CustomData", (BindingFlags)20, null, types, modifiers);
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D16FE0");
					object obj5 = default(object);
					string text6 = default(string);
					if (obj5 == null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A7190");
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1805DABC0");
						object obj6 = default(object);
						bool flag5 = obj6 == null;
						dictionary = null;
						Binder binder = null;
						if (!flag5)
						{
							Type[] array2 = new Type[2];
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
							RuntimeTypeHandle runtimeTypeHandle2 = (RuntimeTypeHandle)((nint)0 + (nint)32);
							Type typeFromHandle4 = Type.GetTypeFromHandle(runtimeTypeHandle2);
							bool flag6 = array2 == null;
							dictionary = null;
							binder = null;
							string text3 = null;
							if (flag6)
							{
								throw new NullReferenceException();
							}
							bool flag7 = (object)typeFromHandle4 == null;
							string text4 = null;
							Type type5 = (Type)runtimeTypeHandle2;
							nint num;
							if (!flag7)
							{
								object obj7 = array2;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1338 @ rdx_v116+40]");
								text4 = (string)0;
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
								object obj8 = default(object);
								bool flag8 = obj8 == null;
								type5 = typeFromHandle4;
								dictionary = null;
								num = unchecked((nint)null);
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1338 @ rdx_v116+40]");
								text2 = (string)0;
								typeFromHandle3 = typeFromHandle4;
								if (flag8)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
									text4 = null;
									Type type6 = default(Type);
									type5 = type6;
									throw type6;
								}
							}
							bool flag9 = array2.Length <= 0;
							dictionary = null;
							num = unchecked((nint)null);
							if (flag9)
							{
								throw new IndexOutOfRangeException();
							}
							array2[0] = typeFromHandle4;
							Type typeFromHandle5 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(IDictionary<string, object>));
							bool flag10 = (object)typeFromHandle5 == null;
							string text5 = null;
							Type typeFromHandle6 = typeof(IDictionary<string, object>);
							if (!flag10)
							{
								object obj9 = array2;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1598 @ rdx_v114+40]");
								text5 = (string)0;
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
								object obj10 = default(object);
								bool flag11 = obj10 == null;
								typeFromHandle6 = typeFromHandle5;
								dictionary = null;
								num = unchecked((nint)null);
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1598 @ rdx_v114+40]");
								text4 = (string)0;
								type5 = typeFromHandle5;
								if (flag11)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
									text5 = null;
									Type type7 = default(Type);
									typeFromHandle6 = type7;
									throw type7;
								}
							}
							bool flag12 = array2.Length <= 1;
							dictionary = null;
							num = unchecked((nint)null);
							if (flag12)
							{
								throw new IndexOutOfRangeException();
							}
							array2[1] = typeFromHandle5;
							runtimeTypeHandle2 = (RuntimeTypeHandle)(array2 + 40);
							Type type8 = default(Type);
							bool flag13 = (object)type8 == null;
							dictionary = null;
							binder = null;
							text3 = (string)(object)typeFromHandle5;
							if (flag13)
							{
								throw new NullReferenceException();
							}
							MethodInfo method2 = type8.GetMethod("CustomData", (BindingFlags)24, null, types, modifiers);
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D16FE0");
							object obj11 = default(object);
							bool flag14 = obj11 == null;
							dictionary = null;
							binder = null;
							if (!flag14)
							{
								object[] array3 = new object[2];
								bool flag15 = array3 == null;
								dictionary = null;
								num = unchecked((nint)null);
								text3 = (string)2;
								nint num2 = (nint)typeof(object[]);
								if (!flag15)
								{
									bool flag16 = text6 == null;
									string text7 = (string)2;
									string typeFromHandle7 = (string)(object)typeof(object[]);
									if (!flag16)
									{
										nint num3 = (nint)array3;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1898 @ rdx_v112 (Il2CppClass<System.Object[]>)+40]");
										text7 = (string)0;
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
										object obj12 = default(object);
										bool flag17 = obj12 == null;
										typeFromHandle7 = text6;
										dictionary = null;
										num = unchecked((nint)null);
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1898 @ rdx_v112 (Il2CppClass<System.Object[]>)+40]");
										text5 = (string)0;
										typeFromHandle6 = (Type)(object)text6;
										if (flag17)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
											text7 = null;
											string text8 = default(string);
											typeFromHandle7 = text8;
											throw text8;
										}
									}
									bool flag18 = array3.Length <= 0;
									dictionary = null;
									num = unchecked((nint)null);
									if (!flag18)
									{
										array3[0] = text6;
										IDictionary<string, object> dictionary2 = (IDictionary<string, object>)(array3 + 32);
										if (parameters != null)
										{
											nint num4 = (nint)array3;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1949 @ rdx_v110 (Il2CppClass<System.Object[]>)+40]");
											text6 = (string)0;
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
											object obj13 = default(object);
											bool flag19 = obj13 == null;
											dictionary2 = parameters;
											dictionary = null;
											num = unchecked((nint)null);
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1949 @ rdx_v110 (Il2CppClass<System.Object[]>)+40]");
											text7 = (string)0;
											typeFromHandle7 = (string)(object)parameters;
											if (flag19)
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
												text3 = null;
												IDictionary<string, object> dictionary3 = default(IDictionary<string, object>);
												dictionary2 = dictionary3;
												throw dictionary3;
											}
										}
										bool flag20 = array3.Length <= 1;
										dictionary = null;
										num = unchecked((nint)null);
										text3 = text6;
										if (!flag20)
										{
											array3[1] = parameters;
											num2 = (nint)(array3 + 40);
											bool flag21 = (object)method2 == null;
											dictionary = null;
											num = unchecked((nint)null);
											text3 = (string)(object)parameters;
											if (!flag21)
											{
												object obj14 = method2.Invoke(null, array3);
												goto IL_1081;
											}
											throw new NullReferenceException();
										}
										num2 = (nint)dictionary2;
										throw new IndexOutOfRangeException();
									}
									throw new IndexOutOfRangeException();
								}
								binder = (Binder)num;
								runtimeTypeHandle2 = (RuntimeTypeHandle)num2;
								throw new NullReferenceException();
							}
						}
						Type[] array4 = new Type[2];
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
						RuntimeTypeHandle runtimeTypeHandle3 = (RuntimeTypeHandle)((nint)0 + (nint)32);
						Type typeFromHandle8 = Type.GetTypeFromHandle(runtimeTypeHandle3);
						bool flag22 = array4 == null;
						string text9 = null;
						string text10 = (string)runtimeTypeHandle3;
						if (flag22)
						{
							throw new NullReferenceException();
						}
						bool flag23 = (object)typeFromHandle8 == null;
						string text11 = null;
						if (!flag23)
						{
							object obj15 = array4;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1387 @ rdx_v93+40]");
							text11 = (string)0;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
							object obj16 = default(object);
							bool flag24 = obj16 == null;
							runtimeTypeHandle3 = (RuntimeTypeHandle)typeFromHandle8;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1387 @ rdx_v93+40]");
							string text3 = (string)0;
							RuntimeTypeHandle runtimeTypeHandle2 = (RuntimeTypeHandle)typeFromHandle8;
							if (flag24)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
								text11 = null;
								RuntimeTypeHandle runtimeTypeHandle4 = default(RuntimeTypeHandle);
								runtimeTypeHandle3 = runtimeTypeHandle4;
								throw runtimeTypeHandle4;
							}
						}
						if (array4.Length <= 0)
						{
							throw new IndexOutOfRangeException();
						}
						array4[0] = typeFromHandle8;
						Type typeFromHandle9 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(IDictionary<string, object>));
						bool flag25 = (object)typeFromHandle9 == null;
						string text12 = null;
						RuntimeTypeHandle typeFromHandle10 = (RuntimeTypeHandle)typeof(IDictionary<string, object>);
						if (!flag25)
						{
							object obj17 = array4;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1611 @ rdx_v91+40]");
							text12 = (string)0;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
							object obj18 = default(object);
							bool flag26 = obj18 == null;
							typeFromHandle10 = (RuntimeTypeHandle)typeFromHandle9;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1611 @ rdx_v91+40]");
							text11 = (string)0;
							runtimeTypeHandle3 = (RuntimeTypeHandle)typeFromHandle9;
							if (flag26)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
								text12 = null;
								RuntimeTypeHandle runtimeTypeHandle5 = default(RuntimeTypeHandle);
								typeFromHandle10 = runtimeTypeHandle5;
								throw runtimeTypeHandle5;
							}
						}
						if (array4.Length <= 1)
						{
							throw new IndexOutOfRangeException();
						}
						array4[1] = typeFromHandle9;
						MethodInfo method3 = type4.GetMethod("RecordEvent", (BindingFlags)20, null, types, modifiers);
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D16FE0");
						object obj19 = default(object);
						if (obj19 == null)
						{
							return false;
						}
						object[] array5 = new object[2];
						bool flag27 = array5 == null;
						dictionary = null;
						binder = null;
						text9 = (string)2;
						text10 = (string)(object)typeof(object[]);
						if (flag27)
						{
							throw new NullReferenceException();
						}
						bool flag28 = text6 == null;
						string text13 = (string)2;
						string typeFromHandle11 = (string)(object)typeof(object[]);
						if (!flag28)
						{
							nint num5 = (nint)array5;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1885 @ rdx_v89 (Il2CppClass<System.Object[]>)+40]");
							text13 = (string)0;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
							object obj20 = default(object);
							bool flag29 = obj20 == null;
							typeFromHandle11 = text6;
							dictionary = null;
							binder = null;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1885 @ rdx_v89 (Il2CppClass<System.Object[]>)+40]");
							text12 = (string)0;
							typeFromHandle10 = (RuntimeTypeHandle)text6;
							if (flag29)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
								text13 = null;
								string text14 = default(string);
								typeFromHandle11 = text14;
								throw text14;
							}
						}
						bool flag30 = array5.Length <= 0;
						dictionary = null;
						binder = null;
						if (flag30)
						{
							throw new IndexOutOfRangeException();
						}
						array5[0] = text6;
						IDictionary<string, object> dictionary4 = (IDictionary<string, object>)(array5 + 32);
						if (parameters != null)
						{
							nint num6 = (nint)array5;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1935 @ rdx_v87 (Il2CppClass<System.Object[]>)+40]");
							text6 = (string)0;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
							object obj21 = default(object);
							bool flag31 = obj21 == null;
							dictionary4 = parameters;
							dictionary = null;
							binder = null;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1935 @ rdx_v87 (Il2CppClass<System.Object[]>)+40]");
							text13 = (string)0;
							typeFromHandle11 = (string)(object)parameters;
							if (flag31)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
								text9 = null;
								string text15 = default(string);
								text10 = text15;
								throw text15;
							}
						}
						bool flag32 = array5.Length <= 1;
						dictionary = null;
						binder = null;
						text9 = text6;
						text10 = (string)(object)dictionary4;
						if (flag32)
						{
							throw new IndexOutOfRangeException();
						}
						array5[1] = parameters;
						text10 = (string)(array5 + 40);
						bool flag33 = (object)method3 == null;
						dictionary = null;
						binder = null;
						text9 = (string)(object)parameters;
						if (flag33)
						{
							throw new NullReferenceException();
						}
						object obj22 = method3.Invoke(instance, array5);
					}
					else
					{
						object[] array6 = new object[2];
						bool flag34 = text6 == null;
						string text16 = (string)2;
						string typeFromHandle12 = (string)(object)typeof(object[]);
						Binder binder;
						if (!flag34)
						{
							nint num7 = (nint)array6;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v741 @ rdx_v67 (Il2CppClass<System.Object[]>)+40]");
							text16 = (string)0;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
							object obj23 = default(object);
							bool flag35 = obj23 == null;
							typeFromHandle12 = text6;
							dictionary = null;
							binder = null;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v741 @ rdx_v67 (Il2CppClass<System.Object[]>)+40]");
							string text9 = (string)0;
							string text10 = text6;
							if (flag35)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
								text16 = null;
								string text17 = default(string);
								typeFromHandle12 = text17;
								throw text17;
							}
						}
						bool flag36 = array6.Length <= 0;
						dictionary = null;
						binder = null;
						if (flag36)
						{
							throw new IndexOutOfRangeException();
						}
						array6[0] = text6;
						if (parameters != null)
						{
							nint num8 = (nint)array6;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1052 @ rdx_v65 (Il2CppClass<System.Object[]>)+40]");
							text16 = (string)0;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
							object obj24 = default(object);
							bool flag37 = obj24 == null;
							dictionary = null;
							binder = null;
							typeFromHandle12 = (string)(object)parameters;
							if (flag37)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
								object obj25 = default(object);
								throw obj25;
							}
						}
						array6[1] = parameters;
						object obj26 = method.Invoke(instance, array6);
					}
					goto IL_1081;
				}
				dictionary = parameters;
				throw new IndexOutOfRangeException();
			}
			throw new IndexOutOfRangeException();
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
		IL_1081:
		return true;
	}

	public AnalyticsInitializer()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39F24]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		initializeOnAwake = true;
		environmentName = "production";
		autoStartDataCollection = true;
		profileName = "";
		sendTestEventOnFirstInit = true;
		base._002Ector();
	}
}
