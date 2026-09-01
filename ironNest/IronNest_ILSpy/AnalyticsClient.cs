using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Cpp2ILInjected;
using Newtonsoft.Json;
using UnityEngine;

public static class AnalyticsClient
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		[StructLayout((LayoutKind)3)]
		private struct _003C_003CWantsToQuit_003Eb__14_0_003Ed : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			private object _003C_003E7__wrap1;

			private int _003C_003E7__wrap2;

			private TaskAwaiter<bool> _003C_003Eu__1;

			private MainThreadAwaitable _003C_003Eu__2;

			private unsafe void MoveNext()
			{
				//IL_0072: Expected O, but got I4
				//IL_0081: Expected I4, but got I8
				//IL_003c: Expected O, but got I4
				//IL_004b: Expected I4, but got I8
				//IL_0119: Expected O, but got Ref
				//IL_02a6: Expected I4, but got I8
				//IL_02b6: Expected O, but got Ref
				//IL_01ae: Expected I, but got O
				//IL_01bc: Expected I, but got O
				//IL_01cc: Expected O, but got I
				//IL_01f8: Expected I, but got O
				//IL_02ea: Expected O, but got Ref
				//IL_0216: Expected O, but got I
				//IL_0243: Expected I, but got O
				//IL_0274: Expected I, but got O
				MainThreadAwaitable mainThreadAwaitable;
				if (_003C_003E1__state != 0)
				{
					if (_003C_003E1__state == 1)
					{
						mainThreadAwaitable = _003C_003Eu__2;
						_003C_003Eu__2 = (MainThreadAwaitable)0;
						_003C_003E1__state = -1;
						goto IL_0178;
					}
					_003C_003E7__wrap1 = null;
					_003C_003E7__wrap2 = 0;
				}
				if (_003C_003E1__state == 0)
				{
					_003C_003Eu__1 = (TaskAwaiter<bool>)0;
					_003C_003E1__state = -1;
					TaskAwaiter<bool> taskAwaiter = _003C_003Eu__1;
				}
				else
				{
					AnalyticsManager.Analytics_Boot("GameClosed");
					Task<bool> task = Flush();
					TaskAwaiter<bool> awaiter = task.GetAwaiter();
					TaskAwaiter<bool> taskAwaiter = default(TaskAwaiter<bool>);
					if (!taskAwaiter.IsCompleted)
					{
						_003C_003E1__state = 0;
						_003C_003Eu__1 = taskAwaiter;
						AsyncTaskMethodBuilder asyncTaskMethodBuilder = (AsyncTaskMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
						((AsyncTaskMethodBuilder*)asyncTaskMethodBuilder)->AwaitUnsafeOnCompleted(ref taskAwaiter, ref this);
						return;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091FAF0");
				MainThreadAwaitable mainThreadAwaitable2 = Awaitable.MainThreadAsync();
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180724280");
				object obj = default(object);
				mainThreadAwaitable = (MainThreadAwaitable)obj;
				MainThreadAwaitable awaiter2 = default(MainThreadAwaitable);
				if (awaiter2.IsCompleted)
				{
					goto IL_0178;
				}
				_003C_003E1__state = 1;
				_003C_003Eu__2 = awaiter2;
				AsyncTaskMethodBuilder asyncTaskMethodBuilder2 = (AsyncTaskMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
				((AsyncTaskMethodBuilder*)asyncTaskMethodBuilder2)->AwaitOnCompleted(ref awaiter2, ref this);
				return;
				IL_0178:
				Application.Quit();
				Exception ex = (Exception)_003C_003E7__wrap1;
				if (_003C_003E7__wrap1 != null)
				{
					nint num = (nint)ex;
					nint num2 = (nint)typeof(Exception);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v235 @ rdx_v22 (Il2CppClass<System.Exception>)+130]");
					object obj2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v315 @ r8_v5 (Il2CppClass<System.Exception>)+130]");
					nint num3 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v235 @ rdx_v22 (Il2CppClass<System.Exception>)+130]");
					bool flag = num3 < 0;
					nint num4 = (nint)typeof(Exception);
					if (!flag)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v315 @ r8_v5 (Il2CppClass<System.Exception>)+C8]");
						object obj3 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v299 @ rax_v43+FFFFFFF8+v236 @ rax_v42*8]");
						bool flag2 = 0 != (nint)typeof(Exception);
						num4 = (nint)typeof(Exception);
						if (!flag2)
						{
							ExceptionDispatchInfo exceptionDispatchInfo = ExceptionDispatchInfo.Capture((Exception)_003C_003E7__wrap1);
							bool flag3 = exceptionDispatchInfo == null;
							num4 = unchecked((nint)null);
							if (!flag3)
							{
								exceptionDispatchInfo.Throw();
								goto IL_0290;
							}
							throw new NullReferenceException();
						}
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
					throw _003C_003E7__wrap1;
				}
				goto IL_0290;
				IL_0290:
				_003C_003E7__wrap1 = null;
				_003C_003E1__state = -2;
				AsyncTaskMethodBuilder asyncTaskMethodBuilder3 = (AsyncTaskMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
				((AsyncTaskMethodBuilder*)asyncTaskMethodBuilder3)->SetResult();
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

		public static readonly _003C_003Ec _003C_003E9;

		public static Func<Task> _003C_003E9__14_0;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal Task _003CWantsToQuit_003Eb__14_0()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180788D50");
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = default(AsyncTaskMethodBuilder);
			_003C_003CWantsToQuit_003Eb__14_0_003Ed stateMachine = default(_003C_003CWantsToQuit_003Eb__14_0_003Ed);
			asyncTaskMethodBuilder.Start(ref stateMachine);
			return asyncTaskMethodBuilder.Task;
		}
	}

	private sealed class _003C_003Ec__DisplayClass25_0
	{
		[StructLayout((LayoutKind)3)]
		private struct _003C_003CStartBatchLoop_003Eb__0_003Ed : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			public _003C_003Ec__DisplayClass25_0 _003C_003E4__this;

			private TaskAwaiter _003C_003Eu__1;

			private TaskAwaiter<bool> _003C_003Eu__2;

			private unsafe void MoveNext()
			{
				//IL_0337: Expected O, but got I4
				//IL_036c: Expected O, but got I4
				//IL_027e: Unknown result type (might be due to invalid IL or missing references)
				//IL_0283: Expected O, but got Unknown
				//IL_02e0: Expected I4, but got I8
				//IL_0030: Expected O, but got I4
				//IL_003f: Expected I4, but got I8
				//IL_004c: Expected I4, but got I8
				//IL_0059: Expected I4, but got I8
				//IL_02f0: Expected O, but got Ref
				//IL_0093: Expected O, but got I4
				//IL_00a2: Expected I4, but got I8
				//IL_00b9: Expected I4, but got I8
				//IL_00c6: Expected I4, but got I8
				//IL_025b: Expected O, but got Ref
				//IL_0223: Expected O, but got Ref
				int num = _003C_003E1__state;
				_003C_003Ec__DisplayClass25_0 obj = _003C_003E4__this;
				bool flag = _003C_003E1__state > 1;
				int num2 = _003C_003E1__state;
				TaskAwaiter taskAwaiter = (TaskAwaiter)0;
				CancellationToken cancellationToken2 = default(CancellationToken);
				CancellationToken cancellationToken = cancellationToken2;
				TimeSpan timeSpan2 = default(TimeSpan);
				TimeSpan timeSpan = timeSpan2;
				double num4 = default(double);
				double num3 = num4;
				int num5 = _003C_003E1__state;
				int num6 = _003C_003E1__state;
				taskAwaiter = (TaskAwaiter)0;
				if (flag)
				{
					goto IL_0273;
				}
				goto IL_0005;
				IL_0005:
				TaskAwaiter<bool> awaiter = default(TaskAwaiter<bool>);
				if (num == 0)
				{
					_003C_003Eu__1 = (TaskAwaiter)0;
					_003C_003E1__state = -1;
					num5 = -1;
					num6 = -1;
					taskAwaiter = _003C_003Eu__1;
				}
				else
				{
					if (num == 1)
					{
						_003C_003Eu__2 = (TaskAwaiter<bool>)0;
						_003C_003E1__state = -1;
						awaiter = _003C_003Eu__2;
						num5 = -1;
						num6 = -1;
						goto IL_01d5;
					}
					TimeSpan timeSpan3 = TimeSpan.FromSeconds(10.0);
					Task task = Task.Delay(timeSpan3, obj.token);
					TaskAwaiter awaiter2 = task.GetAwaiter();
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807D2870");
					object obj2 = default(object);
					bool flag2 = obj2 == null;
					timeSpan2 = timeSpan3;
					num4 = 10.0;
					num5 = num2;
					num6 = num;
					if (flag2)
					{
						_003C_003E1__state = 0;
						_003C_003Eu__1 = taskAwaiter;
						AsyncTaskMethodBuilder asyncTaskMethodBuilder = (AsyncTaskMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
						((AsyncTaskMethodBuilder*)asyncTaskMethodBuilder)->AwaitUnsafeOnCompleted(ref taskAwaiter, ref this);
						return;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180CF3BB0");
				cancellationToken2 = obj.token;
				Task<bool> task2 = Flush(obj.token);
				TaskAwaiter<bool> awaiter3 = task2.GetAwaiter();
				if (awaiter.IsCompleted)
				{
					goto IL_01d5;
				}
				_003C_003E1__state = 1;
				_003C_003Eu__2 = _003C_003Eu__2;
				AsyncTaskMethodBuilder asyncTaskMethodBuilder2 = (AsyncTaskMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
				((AsyncTaskMethodBuilder*)asyncTaskMethodBuilder2)->AwaitUnsafeOnCompleted(ref awaiter, ref this);
				return;
				IL_0273:
				CancellationToken cancellationToken3 = (CancellationToken)(_003C_003E4__this + 16);
				bool isCancellationRequested = ((CancellationToken*)cancellationToken3)->IsCancellationRequested;
				bool flag3 = !isCancellationRequested;
				cancellationToken2 = cancellationToken;
				timeSpan2 = timeSpan;
				num4 = num3;
				num2 = num5;
				num = num6;
				if (!flag3)
				{
					_003C_003E1__state = -2;
					AsyncTaskMethodBuilder asyncTaskMethodBuilder3 = (AsyncTaskMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
					((AsyncTaskMethodBuilder*)asyncTaskMethodBuilder3)->SetResult();
					return;
				}
				goto IL_0005;
				IL_01d5:
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091FAF0");
				cancellationToken = cancellationToken2;
				timeSpan = timeSpan2;
				num3 = num4;
				goto IL_0273;
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

		public CancellationToken token;

		internal Task _003CStartBatchLoop_003Eb__0()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180788D50");
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = default(AsyncTaskMethodBuilder);
			_003C_003CStartBatchLoop_003Eb__0_003Ed stateMachine = default(_003C_003CStartBatchLoop_003Eb__0_003Ed);
			asyncTaskMethodBuilder.Start(ref stateMachine);
			return asyncTaskMethodBuilder.Task;
		}
	}

	[StructLayout((LayoutKind)3)]
	private struct _003CBootEvent_Immediate_003Ed__22 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder<bool> _003C_003Et__builder;

		public AnalyticsEventRequest_Boot request;

		private HttpResponseMessage _003Cres_003E5__2;

		private TaskAwaiter<HttpResponseMessage> _003C_003Eu__1;

		private object _003C_003E7__wrap2;

		private TaskAwaiter<string> _003C_003Eu__2;

		private unsafe void MoveNext()
		{
			//IL_0010: Expected O, but got I4
			//IL_001f: Expected I4, but got I8
			//IL_005b: Expected O, but got I4
			//IL_006a: Expected I4, but got I8
			//IL_0335: Expected I4, but got I8
			//IL_0189: Expected I4, but got O
			//IL_02ca: Expected O, but got Ref
			//IL_0274: Expected O, but got Ref
			//IL_02ac: Expected O, but got Ref
			if (_003C_003E1__state == 0)
			{
				_003C_003Eu__1 = (TaskAwaiter<HttpResponseMessage>)0;
				_003C_003E1__state = -1;
				TaskAwaiter<HttpResponseMessage> taskAwaiter = _003C_003Eu__1;
			}
			else
			{
				if (_003C_003E1__state == 1)
				{
					_003C_003Eu__2 = (TaskAwaiter<string>)0;
					_003C_003E1__state = -1;
					TaskAwaiter<string> taskAwaiter2 = _003C_003Eu__2;
					goto IL_01fa;
				}
				string url = _baseUrl + "/api/v2/analytics/boot_event";
				AnalyticsEventRequest_Boot analyticsEventRequest_Boot = request;
				DateTime utcNow = DateTime.UtcNow;
				analyticsEventRequest_Boot._003CCreatedAtUtc_003Ek__BackingField = utcNow;
				AnalyticsEventRequest_Boot analyticsEventRequest_Boot2 = request;
				analyticsEventRequest_Boot2._003CDeviceId_003Ek__BackingField = _deviceID;
				AnalyticsEventRequest_Boot analyticsEventRequest_Boot3 = request;
				analyticsEventRequest_Boot3._003CUserId_003Ek__BackingField = _userId;
				HttpRequestMessage httpRequestMessage = CreateRequest(HttpMethod.post_method, url, request);
				Task<HttpResponseMessage> task = _http.SendAsync(httpRequestMessage);
				TaskAwaiter<HttpResponseMessage> awaiter = task.GetAwaiter();
				TaskAwaiter<HttpResponseMessage> taskAwaiter = default(TaskAwaiter<HttpResponseMessage>);
				if (!taskAwaiter.IsCompleted)
				{
					_003C_003E1__state = 0;
					_003C_003Eu__1 = taskAwaiter;
					AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder = (AsyncTaskMethodBuilder<bool>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
					((AsyncTaskMethodBuilder<bool>*)asyncTaskMethodBuilder)->AwaitUnsafeOnCompleted(ref taskAwaiter, ref this);
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091FAF0");
			HttpResponseMessage httpResponseMessage = default(HttpResponseMessage);
			_003Cres_003E5__2 = httpResponseMessage;
			if (!_003Cres_003E5__2.IsSuccessStatusCode)
			{
				object obj = (HttpStatusCode)httpResponseMessage;
				_003C_003E7__wrap2 = obj;
				HttpResponseMessage httpResponseMessage2 = _003Cres_003E5__2;
				Task<string> task2 = httpResponseMessage2._003CContent_003Ek__BackingField.ReadAsStringAsync();
				TaskAwaiter<string> awaiter2 = task2.GetAwaiter();
				TaskAwaiter<string> taskAwaiter2 = default(TaskAwaiter<string>);
				if (taskAwaiter2.IsCompleted)
				{
					goto IL_01fa;
				}
				_003C_003E1__state = 1;
				_003C_003Eu__2 = taskAwaiter2;
				AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder2 = (AsyncTaskMethodBuilder<bool>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
				((AsyncTaskMethodBuilder<bool>*)asyncTaskMethodBuilder2)->AwaitUnsafeOnCompleted(ref taskAwaiter2, ref this);
				return;
			}
			goto IL_023b;
			IL_023b:
			HttpResponseMessage httpResponseMessage3 = _003Cres_003E5__2.EnsureSuccessStatusCode();
			_003C_003E1__state = -2;
			AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder3 = (AsyncTaskMethodBuilder<bool>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			object obj2 = default(object);
			((AsyncTaskMethodBuilder<bool>*)asyncTaskMethodBuilder3)->SetResult((byte)(&obj2) != 0);
			return;
			IL_01fa:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091FAF0");
			object arg = default(object);
			string message = $"{_003C_003E7__wrap2} | {arg}";
			Debug.LogError(message);
			_003C_003E7__wrap2 = null;
			goto IL_023b;
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			this.MoveNext();
		}

		private unsafe void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//IL_0010: Expected O, but got Ref
			AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder = (AsyncTaskMethodBuilder<bool>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			((AsyncTaskMethodBuilder<bool>*)asyncTaskMethodBuilder)->SetStateMachine(stateMachine);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
			this.SetStateMachine(stateMachine);
		}
	}

	[StructLayout((LayoutKind)3)]
	private struct _003CFlush_003Ed__27 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder<bool> _003C_003Et__builder;

		private TaskAwaiter<bool> _003C_003Eu__1;

		private unsafe void MoveNext()
		{
			//IL_0010: Expected O, but got I4
			//IL_001f: Expected I4, but got I8
			//IL_00d2: Expected I4, but got I8
			//IL_00e2: Expected O, but got Ref
			//IL_011a: Expected O, but got Ref
			if (_003C_003E1__state == 0)
			{
				_003C_003Eu__1 = (TaskAwaiter<bool>)0;
				_003C_003E1__state = -1;
				TaskAwaiter<bool> taskAwaiter = _003C_003Eu__1;
			}
			else
			{
				CancellationToken token2;
				if (_batchCts != null)
				{
					CancellationToken token = _batchCts.Token;
					token2 = token;
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180384A50");
					CancellationToken cancellationToken = default(CancellationToken);
					token2 = cancellationToken;
				}
				Task<bool> task = Flush(token2);
				TaskAwaiter<bool> awaiter = task.GetAwaiter();
				TaskAwaiter<bool> taskAwaiter = default(TaskAwaiter<bool>);
				if (!taskAwaiter.IsCompleted)
				{
					_003C_003E1__state = 0;
					_003C_003Eu__1 = taskAwaiter;
					AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder = (AsyncTaskMethodBuilder<bool>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
					((AsyncTaskMethodBuilder<bool>*)asyncTaskMethodBuilder)->AwaitUnsafeOnCompleted(ref taskAwaiter, ref this);
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091FAF0");
			_003C_003E1__state = -2;
			AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder2 = (AsyncTaskMethodBuilder<bool>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			object obj = default(object);
			((AsyncTaskMethodBuilder<bool>*)asyncTaskMethodBuilder2)->SetResult((byte)(&obj) != 0);
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			this.MoveNext();
		}

		private unsafe void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//IL_0010: Expected O, but got Ref
			AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder = (AsyncTaskMethodBuilder<bool>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			((AsyncTaskMethodBuilder<bool>*)asyncTaskMethodBuilder)->SetStateMachine(stateMachine);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
			this.SetStateMachine(stateMachine);
		}
	}

	[StructLayout((LayoutKind)3)]
	private struct _003CFlush_003Ed__28 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder<bool> _003C_003Et__builder;

		public CancellationToken token;

		private AnalyticsEventsBatchRequest _003Cbatch_003E5__2;

		private HttpResponseMessage _003Cres_003E5__3;

		private TaskAwaiter<HttpResponseMessage> _003C_003Eu__1;

		private object _003C_003E7__wrap3;

		private TaskAwaiter<string> _003C_003Eu__2;

		private unsafe void MoveNext()
		{
			//IL_02af: Expected O, but got I4
			//IL_02be: Expected I4, but got I8
			//IL_0015: Expected O, but got Ref
			//IL_02f8: Expected O, but got I4
			//IL_0307: Expected I4, but got I8
			//IL_080a: Expected I4, but got I8
			//IL_0630: Expected O, but got Ref
			//IL_074c: Expected I, but got O
			//IL_0370: Expected I, but got O
			//IL_0456: Expected I4, but got O
			//IL_046f: Expected O, but got Ref
			//IL_0612: Expected O, but got Ref
			//IL_07a8: Expected I, but got O
			//IL_05da: Expected O, but got Ref
			//IL_01d6: Expected O, but got I
			//IL_0200: Expected O, but got I
			//IL_022a: Expected O, but got I
			bool flag = _003C_003E1__state <= 1;
			int num = _003C_003E1__state;
			if (!flag)
			{
				CancellationToken cancellationToken = (CancellationToken)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 32));
				if (((CancellationToken*)cancellationToken)->IsCancellationRequested)
				{
					goto IL_07fb;
				}
				object obj = default(object);
				bool lockTaken = default(bool);
				Monitor.Enter(obj, ref lockTaken);
				nint num2 = (nint)typeof(AnalyticsClient);
				List<AnalyticsEventRequest_Boot> bootEvents = _bootEvents;
				if (_bootEvents == null)
				{
					throw new NullReferenceException();
				}
				int num4 = default(int);
				if (bootEvents._size == 0)
				{
					List<AnalyticsEventRequest_Mission> missionEvents = _missionEvents;
					if (_missionEvents == null)
					{
						throw new NullReferenceException();
					}
					if (missionEvents._size == 0)
					{
						nint num3 = (nint)_genericEvents;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v827 @ rcx_v107 (Il2CppClass<AnalyticsClient>)+18]");
						if ((nint)0 == 0)
						{
							if (num4 < 0 && lockTaken)
							{
								Monitor.Exit(obj);
							}
							goto IL_07fb;
						}
					}
				}
				AnalyticsEventsBatchRequest analyticsEventsBatchRequest = new AnalyticsEventsBatchRequest();
				List<AnalyticsEventRequest_Boot> list = new List<AnalyticsEventRequest_Boot>();
				analyticsEventsBatchRequest._003CBootEvents_003Ek__BackingField = list;
				List<AnalyticsEventRequest_Mission> list2 = new List<AnalyticsEventRequest_Mission>();
				analyticsEventsBatchRequest._003CMissionEvents_003Ek__BackingField = list2;
				List<AnalyticsEventRequest_Generic> list3 = new List<AnalyticsEventRequest_Generic>();
				analyticsEventsBatchRequest._003CGenericEvents_003Ek__BackingField = list3;
				List<AnalyticsEventRequest_Boot> list4 = new List<AnalyticsEventRequest_Boot>(_bootEvents);
				if (analyticsEventsBatchRequest == null)
				{
					throw new NullReferenceException();
				}
				analyticsEventsBatchRequest._003CBootEvents_003Ek__BackingField = list4;
				List<AnalyticsEventRequest_Mission> list5 = new List<AnalyticsEventRequest_Mission>(_missionEvents);
				analyticsEventsBatchRequest._003CMissionEvents_003Ek__BackingField = list5;
				List<AnalyticsEventRequest_Generic> list6 = new List<AnalyticsEventRequest_Generic>(_genericEvents);
				analyticsEventsBatchRequest._003CGenericEvents_003Ek__BackingField = list6;
				_003Cbatch_003E5__2 = analyticsEventsBatchRequest;
				if (_bootEvents == null)
				{
					throw new NullReferenceException();
				}
				((List<AnalyticsEventRequest_Generic>)(object)_bootEvents)._002Ector((IEnumerable<AnalyticsEventRequest_Generic>)0);
				if (_missionEvents == null)
				{
					throw new NullReferenceException();
				}
				((List<AnalyticsEventRequest_Generic>)(object)_missionEvents)._002Ector((IEnumerable<AnalyticsEventRequest_Generic>)0);
				if (_genericEvents == null)
				{
					throw new NullReferenceException();
				}
				_genericEvents._002Ector((IEnumerable<AnalyticsEventRequest_Generic>)0);
				if (num4 < 0 && lockTaken)
				{
					Monitor.Exit(obj);
				}
				num = num4;
			}
			HttpResponseMessage http;
			if (num == 0)
			{
				_003C_003Eu__1 = (TaskAwaiter<HttpResponseMessage>)0;
				_003C_003E1__state = -1;
				TaskAwaiter<HttpResponseMessage> taskAwaiter = _003C_003Eu__1;
			}
			else
			{
				if (num == 1)
				{
					_003C_003Eu__2 = (TaskAwaiter<string>)0;
					_003C_003E1__state = -1;
					TaskAwaiter<string> taskAwaiter2 = _003C_003Eu__2;
					goto IL_0540;
				}
				string url = _baseUrl + "/api/v2/analytics/events";
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803DB550");
				HttpMethod method = default(HttpMethod);
				HttpRequestMessage httpRequestMessage = CreateRequest(method, url, _003Cbatch_003E5__2);
				http = (HttpResponseMessage)(object)_http;
				if (_http == null)
				{
					throw new NullReferenceException();
				}
				nint num5 = (nint)http;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v966 @ r9_v17 (Il2CppClass<System.Net.Http.HttpResponseMessage>)+198] (should have been resolved before IL gen)");
				Task<HttpResponseMessage> task = default(Task<HttpResponseMessage>);
				if (task == null)
				{
					throw new NullReferenceException();
				}
				TaskAwaiter<HttpResponseMessage> awaiter = task.GetAwaiter();
				TaskAwaiter<HttpResponseMessage> taskAwaiter = default(TaskAwaiter<HttpResponseMessage>);
				if (!taskAwaiter.IsCompleted)
				{
					_003C_003E1__state = 0;
					_003C_003Eu__1 = taskAwaiter;
					AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder = (AsyncTaskMethodBuilder<bool>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
					((AsyncTaskMethodBuilder<bool>*)asyncTaskMethodBuilder)->AwaitUnsafeOnCompleted(ref taskAwaiter, ref this);
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091FAF0");
			HttpResponseMessage httpResponseMessage = default(HttpResponseMessage);
			_003Cres_003E5__3 = httpResponseMessage;
			http = _003Cres_003E5__3;
			if (_003Cres_003E5__3 != null)
			{
				if (!_003Cres_003E5__3.IsSuccessStatusCode)
				{
					if (_003Cres_003E5__3 != null)
					{
						object obj2 = (HttpStatusCode)httpResponseMessage;
						_003C_003E7__wrap3 = obj2;
						http = (HttpResponseMessage)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 64));
						HttpResponseMessage httpResponseMessage2 = _003Cres_003E5__3;
						if (_003Cres_003E5__3 != null)
						{
							bool flag2 = httpResponseMessage2._003CContent_003Ek__BackingField == null;
							http = (HttpResponseMessage)(object)httpResponseMessage2._003CContent_003Ek__BackingField;
							if (!flag2)
							{
								Task<string> task2 = httpResponseMessage2._003CContent_003Ek__BackingField.ReadAsStringAsync();
								bool flag3 = task2 == null;
								http = (HttpResponseMessage)(object)httpResponseMessage2._003CContent_003Ek__BackingField;
								if (!flag3)
								{
									TaskAwaiter<string> awaiter2 = task2.GetAwaiter();
									TaskAwaiter<string> taskAwaiter2 = default(TaskAwaiter<string>);
									if (taskAwaiter2.IsCompleted)
									{
										goto IL_0540;
									}
									_003C_003E1__state = 1;
									_003C_003Eu__2 = taskAwaiter2;
									AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder2 = (AsyncTaskMethodBuilder<bool>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
									((AsyncTaskMethodBuilder<bool>*)asyncTaskMethodBuilder2)->AwaitUnsafeOnCompleted(ref taskAwaiter2, ref this);
									return;
								}
								throw new NullReferenceException();
							}
							throw new NullReferenceException();
						}
						throw new NullReferenceException();
					}
					throw new NullReferenceException();
				}
				goto IL_0581;
			}
			throw new NullReferenceException();
			IL_0540:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091FAF0");
			object arg = default(object);
			string message = $"{_003C_003E7__wrap3} | {arg}";
			Debug.LogError(message);
			_003C_003E7__wrap3 = null;
			goto IL_0581;
			IL_07fb:
			_003C_003E1__state = -2;
			_003Cbatch_003E5__2 = null;
			AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder3 = (AsyncTaskMethodBuilder<bool>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			object obj3 = default(object);
			((AsyncTaskMethodBuilder<bool>*)asyncTaskMethodBuilder3)->SetResult((byte)(&obj3) != 0);
			return;
			IL_0581:
			if (_003Cres_003E5__3 != null)
			{
				HttpResponseMessage httpResponseMessage3 = _003Cres_003E5__3.EnsureSuccessStatusCode();
				goto IL_07fb;
			}
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
			AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder = (AsyncTaskMethodBuilder<bool>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			((AsyncTaskMethodBuilder<bool>*)asyncTaskMethodBuilder)->SetStateMachine(stateMachine);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
			this.SetStateMachine(stateMachine);
		}
	}

	[StructLayout((LayoutKind)3)]
	private struct _003CGenericEvent_Immediate_003Ed__24 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder<bool> _003C_003Et__builder;

		public AnalyticsEventRequest_Generic request;

		private HttpResponseMessage _003Cres_003E5__2;

		private TaskAwaiter<HttpResponseMessage> _003C_003Eu__1;

		private object _003C_003E7__wrap2;

		private TaskAwaiter<string> _003C_003Eu__2;

		private unsafe void MoveNext()
		{
			//IL_0010: Expected O, but got I4
			//IL_001f: Expected I4, but got I8
			//IL_005b: Expected O, but got I4
			//IL_006a: Expected I4, but got I8
			//IL_0335: Expected I4, but got I8
			//IL_0189: Expected I4, but got O
			//IL_02ca: Expected O, but got Ref
			//IL_0274: Expected O, but got Ref
			//IL_02ac: Expected O, but got Ref
			if (_003C_003E1__state == 0)
			{
				_003C_003Eu__1 = (TaskAwaiter<HttpResponseMessage>)0;
				_003C_003E1__state = -1;
				TaskAwaiter<HttpResponseMessage> taskAwaiter = _003C_003Eu__1;
			}
			else
			{
				if (_003C_003E1__state == 1)
				{
					_003C_003Eu__2 = (TaskAwaiter<string>)0;
					_003C_003E1__state = -1;
					TaskAwaiter<string> taskAwaiter2 = _003C_003Eu__2;
					goto IL_01fa;
				}
				string url = _baseUrl + "/api/v2/analytics/generic_event";
				AnalyticsEventRequest_Generic analyticsEventRequest_Generic = request;
				DateTime utcNow = DateTime.UtcNow;
				analyticsEventRequest_Generic._003CCreatedAtUtc_003Ek__BackingField = utcNow;
				AnalyticsEventRequest_Generic analyticsEventRequest_Generic2 = request;
				analyticsEventRequest_Generic2._003CDeviceId_003Ek__BackingField = _deviceID;
				AnalyticsEventRequest_Generic analyticsEventRequest_Generic3 = request;
				analyticsEventRequest_Generic3._003CUserId_003Ek__BackingField = _userId;
				HttpRequestMessage httpRequestMessage = CreateRequest(HttpMethod.post_method, url, request);
				Task<HttpResponseMessage> task = _http.SendAsync(httpRequestMessage);
				TaskAwaiter<HttpResponseMessage> awaiter = task.GetAwaiter();
				TaskAwaiter<HttpResponseMessage> taskAwaiter = default(TaskAwaiter<HttpResponseMessage>);
				if (!taskAwaiter.IsCompleted)
				{
					_003C_003E1__state = 0;
					_003C_003Eu__1 = taskAwaiter;
					AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder = (AsyncTaskMethodBuilder<bool>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
					((AsyncTaskMethodBuilder<bool>*)asyncTaskMethodBuilder)->AwaitUnsafeOnCompleted(ref taskAwaiter, ref this);
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091FAF0");
			HttpResponseMessage httpResponseMessage = default(HttpResponseMessage);
			_003Cres_003E5__2 = httpResponseMessage;
			if (!_003Cres_003E5__2.IsSuccessStatusCode)
			{
				object obj = (HttpStatusCode)httpResponseMessage;
				_003C_003E7__wrap2 = obj;
				HttpResponseMessage httpResponseMessage2 = _003Cres_003E5__2;
				Task<string> task2 = httpResponseMessage2._003CContent_003Ek__BackingField.ReadAsStringAsync();
				TaskAwaiter<string> awaiter2 = task2.GetAwaiter();
				TaskAwaiter<string> taskAwaiter2 = default(TaskAwaiter<string>);
				if (taskAwaiter2.IsCompleted)
				{
					goto IL_01fa;
				}
				_003C_003E1__state = 1;
				_003C_003Eu__2 = taskAwaiter2;
				AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder2 = (AsyncTaskMethodBuilder<bool>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
				((AsyncTaskMethodBuilder<bool>*)asyncTaskMethodBuilder2)->AwaitUnsafeOnCompleted(ref taskAwaiter2, ref this);
				return;
			}
			goto IL_023b;
			IL_023b:
			HttpResponseMessage httpResponseMessage3 = _003Cres_003E5__2.EnsureSuccessStatusCode();
			_003C_003E1__state = -2;
			AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder3 = (AsyncTaskMethodBuilder<bool>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			object obj2 = default(object);
			((AsyncTaskMethodBuilder<bool>*)asyncTaskMethodBuilder3)->SetResult((byte)(&obj2) != 0);
			return;
			IL_01fa:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091FAF0");
			object arg = default(object);
			string message = $"{_003C_003E7__wrap2} | {arg}";
			Debug.LogError(message);
			_003C_003E7__wrap2 = null;
			goto IL_023b;
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			this.MoveNext();
		}

		private unsafe void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//IL_0010: Expected O, but got Ref
			AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder = (AsyncTaskMethodBuilder<bool>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			((AsyncTaskMethodBuilder<bool>*)asyncTaskMethodBuilder)->SetStateMachine(stateMachine);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
			this.SetStateMachine(stateMachine);
		}
	}

	[StructLayout((LayoutKind)3)]
	private struct _003CMissionEvent_Immediate_003Ed__23 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder<bool> _003C_003Et__builder;

		public AnalyticsEventRequest_Mission request;

		private HttpResponseMessage _003Cres_003E5__2;

		private TaskAwaiter<HttpResponseMessage> _003C_003Eu__1;

		private object _003C_003E7__wrap2;

		private TaskAwaiter<string> _003C_003Eu__2;

		private unsafe void MoveNext()
		{
			//IL_0010: Expected O, but got I4
			//IL_001f: Expected I4, but got I8
			//IL_005b: Expected O, but got I4
			//IL_006a: Expected I4, but got I8
			//IL_0390: Expected I4, but got I8
			//IL_032a: Expected O, but got Ref
			//IL_01e2: Expected I4, but got O
			//IL_00fd: Expected I, but got O
			//IL_02ce: Expected O, but got Ref
			//IL_0195: Expected O, but got I4
			//IL_0306: Expected O, but got Ref
			if (_003C_003E1__state == 0)
			{
				_003C_003Eu__1 = (TaskAwaiter<HttpResponseMessage>)0;
				_003C_003E1__state = -1;
				TaskAwaiter<HttpResponseMessage> taskAwaiter = _003C_003Eu__1;
			}
			else
			{
				if (_003C_003E1__state == 1)
				{
					_003C_003Eu__2 = (TaskAwaiter<string>)0;
					_003C_003E1__state = -1;
					TaskAwaiter<string> taskAwaiter2 = _003C_003Eu__2;
					goto IL_0253;
				}
				AnalyticsEventRequest_Mission analyticsEventRequest_Mission = request;
				if (string.IsNullOrEmpty(analyticsEventRequest_Mission._003CMissionId_003Ek__BackingField))
				{
					goto IL_0381;
				}
				string url = _baseUrl + "/api/v2/analytics/mission_event";
				AnalyticsEventRequest_Mission analyticsEventRequest_Mission2 = request;
				DateTime utcNow = DateTime.UtcNow;
				analyticsEventRequest_Mission2._003CCreatedAtUtc_003Ek__BackingField = utcNow;
				AnalyticsEventRequest_Mission analyticsEventRequest_Mission3 = request;
				analyticsEventRequest_Mission3._003CDeviceId_003Ek__BackingField = _deviceID;
				AnalyticsEventRequest_Mission analyticsEventRequest_Mission4 = request;
				nint num = (nint)typeof(AnalyticsClient);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v624 @ rax_v64 (Il2CppClass<AnalyticsClient>)+B8]");
				nint num2 = 0;
				analyticsEventRequest_Mission4._003CUserId_003Ek__BackingField = _userId;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803DB550");
				HttpMethod method = default(HttpMethod);
				HttpRequestMessage httpRequestMessage = CreateRequest(method, url, request);
				Task<HttpResponseMessage> task = _http.SendAsync(httpRequestMessage);
				TaskAwaiter<HttpResponseMessage> awaiter = task.GetAwaiter();
				TaskAwaiter<HttpResponseMessage> taskAwaiter = default(TaskAwaiter<HttpResponseMessage>);
				bool isCompleted = taskAwaiter.IsCompleted;
				bool flag = !isCompleted;
				object obj = 0;
				if (flag)
				{
					_003C_003E1__state = 0;
					_003C_003Eu__1 = taskAwaiter;
					AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder = (AsyncTaskMethodBuilder<bool>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
					((AsyncTaskMethodBuilder<bool>*)asyncTaskMethodBuilder)->AwaitUnsafeOnCompleted(ref taskAwaiter, ref this);
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091FAF0");
			HttpResponseMessage httpResponseMessage = default(HttpResponseMessage);
			_003Cres_003E5__2 = httpResponseMessage;
			if (!_003Cres_003E5__2.IsSuccessStatusCode)
			{
				object obj2 = (HttpStatusCode)httpResponseMessage;
				_003C_003E7__wrap2 = obj2;
				HttpResponseMessage httpResponseMessage2 = _003Cres_003E5__2;
				Task<string> task2 = httpResponseMessage2._003CContent_003Ek__BackingField.ReadAsStringAsync();
				TaskAwaiter<string> awaiter2 = task2.GetAwaiter();
				TaskAwaiter<string> taskAwaiter2 = default(TaskAwaiter<string>);
				if (taskAwaiter2.IsCompleted)
				{
					goto IL_0253;
				}
				_003C_003E1__state = 1;
				_003C_003Eu__2 = taskAwaiter2;
				AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder2 = (AsyncTaskMethodBuilder<bool>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
				((AsyncTaskMethodBuilder<bool>*)asyncTaskMethodBuilder2)->AwaitUnsafeOnCompleted(ref taskAwaiter2, ref this);
				return;
			}
			goto IL_0294;
			IL_0253:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091FAF0");
			object arg = default(object);
			string message = $"{_003C_003E7__wrap2} | {arg}";
			Debug.LogError(message);
			_003C_003E7__wrap2 = null;
			goto IL_0294;
			IL_0294:
			HttpResponseMessage httpResponseMessage3 = _003Cres_003E5__2.EnsureSuccessStatusCode();
			goto IL_0381;
			IL_0381:
			_003C_003E1__state = -2;
			AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder3 = (AsyncTaskMethodBuilder<bool>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			object obj3 = default(object);
			((AsyncTaskMethodBuilder<bool>*)asyncTaskMethodBuilder3)->SetResult((byte)(&obj3) != 0);
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			this.MoveNext();
		}

		private unsafe void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//IL_0010: Expected O, but got Ref
			AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder = (AsyncTaskMethodBuilder<bool>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			((AsyncTaskMethodBuilder<bool>*)asyncTaskMethodBuilder)->SetStateMachine(stateMachine);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
			this.SetStateMachine(stateMachine);
		}
	}

	private static readonly HttpClient _http;

	private static string _baseUrl;

	private static string _key;

	public static string _deviceID;

	public static Guid _userId;

	private const float BatchIntervalSeconds = 10f;

	private const int MaxBatchSize = 100;

	private static readonly object _batchLock;

	private static readonly List<AnalyticsEventRequest_Boot> _bootEvents;

	private static readonly List<AnalyticsEventRequest_Mission> _missionEvents;

	private static readonly List<AnalyticsEventRequest_Generic> _genericEvents;

	private static CancellationTokenSource _batchCts;

	private static bool _batchLoopStarted;

	private static bool isQuitting;

	private static readonly JsonSerializerSettings _jsonOptions;

	private static bool WantsToQuit()
	{
		if (!isQuitting)
		{
			isQuitting = true;
			Func<bool> value = WantsToQuit;
			Application.wantsToQuit -= value;
			Func<Task> function = _003C_003Ec._003C_003E9__14_0;
			if (_003C_003Ec._003C_003E9__14_0 == null)
			{
				function = (_003C_003Ec._003C_003E9__14_0 = delegate
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180788D50");
					AsyncTaskMethodBuilder asyncTaskMethodBuilder = default(AsyncTaskMethodBuilder);
					_003C_003Ec._003C_003CWantsToQuit_003Eb__14_0_003Ed stateMachine = default(_003C_003Ec._003C_003CWantsToQuit_003Eb__14_0_003Ed);
					asyncTaskMethodBuilder.Start(ref stateMachine);
					return asyncTaskMethodBuilder.Task;
				});
			}
			Task task = Task.Run(function);
			return false;
		}
		return true;
	}

	private static void RunOnStart()
	{
		Func<bool> value = WantsToQuit;
		Application.wantsToQuit -= value;
		Func<bool> value2 = WantsToQuit;
		Application.wantsToQuit += value2;
	}

	public static void Init(string baseUrl, string analyticsKey)
	{
		string baseUrl2 = baseUrl.TrimEnd('/');
		_baseUrl = baseUrl2;
		_key = analyticsKey;
		string deviceUniqueIdentifier = SystemInfo.deviceUniqueIdentifier;
		string deviceID = PlayerPrefs.GetString("IN_DeviceID", deviceUniqueIdentifier);
		_deviceID = deviceID;
		_003C_003Ec__DisplayClass25_0 CS_0024_003C_003E8__locals1 = new _003C_003Ec__DisplayClass25_0();
		if (!_batchLoopStarted)
		{
			_batchLoopStarted = true;
			CancellationTokenSource batchCts = new CancellationTokenSource();
			_batchCts = batchCts;
			CancellationToken token = _batchCts.Token;
			CS_0024_003C_003E8__locals1.token = token;
			Func<Task> function = delegate
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180788D50");
				AsyncTaskMethodBuilder asyncTaskMethodBuilder = default(AsyncTaskMethodBuilder);
				_003C_003Ec__DisplayClass25_0._003C_003CStartBatchLoop_003Eb__0_003Ed stateMachine = default(_003C_003Ec__DisplayClass25_0._003C_003CStartBatchLoop_003Eb__0_003Ed);
				asyncTaskMethodBuilder.Start(ref stateMachine);
				return asyncTaskMethodBuilder.Task;
			};
			Task task = Task.Run(function);
		}
	}

	private static HttpRequestMessage CreateRequest(HttpMethod method, string url, object body = null)
	{
		HttpRequestMessage httpRequestMessage = new HttpRequestMessage(method, url);
		if (httpRequestMessage != null)
		{
			HttpRequestHeaders headers = httpRequestMessage.Headers;
			if (headers != null)
			{
				headers.Add("User-Agent", "IronNest-Unity");
				HttpRequestHeaders headers2 = httpRequestMessage.Headers;
				if (headers2 != null)
				{
					headers2.Add("x-analytics-key", _key);
					if (body != null)
					{
						string content = JsonConvert.SerializeObject(body, _jsonOptions);
						Encoding uTF = Encoding.UTF8;
						StringContent stringContent = new StringContent(content, uTF, "application/json");
						httpRequestMessage._003CContent_003Ek__BackingField = stringContent;
					}
					return httpRequestMessage;
				}
			}
		}
		return (HttpRequestMessage)(object)new NullReferenceException();
	}

	public static void BootEvent(AnalyticsEventRequest_Boot request)
	{
		DateTime utcNow = DateTime.UtcNow;
		request._003CCreatedAtUtc_003Ek__BackingField = utcNow;
		request._003CDeviceId_003Ek__BackingField = _deviceID;
		request._003CUserId_003Ek__BackingField = _userId;
		object obj = default(object);
		bool lockTaken = default(bool);
		Monitor.Enter(obj, ref lockTaken);
		if (_bootEvents != null)
		{
			_bootEvents.Add(request);
			if (lockTaken)
			{
				Monitor.Exit(obj);
			}
			TryFlushIfFull();
			return;
		}
		throw new NullReferenceException();
	}

	public static void MissionEvent(AnalyticsEventRequest_Mission request)
	{
		DateTime utcNow = DateTime.UtcNow;
		request._003CCreatedAtUtc_003Ek__BackingField = utcNow;
		request._003CDeviceId_003Ek__BackingField = _deviceID;
		request._003CUserId_003Ek__BackingField = _userId;
		object obj = default(object);
		bool lockTaken = default(bool);
		Monitor.Enter(obj, ref lockTaken);
		if (_missionEvents != null)
		{
			_missionEvents.Add(request);
			if (lockTaken)
			{
				Monitor.Exit(obj);
			}
			TryFlushIfFull();
			return;
		}
		throw new NullReferenceException();
	}

	public static void GenericEvent(AnalyticsEventRequest_Generic request)
	{
		DateTime utcNow = DateTime.UtcNow;
		request._003CCreatedAtUtc_003Ek__BackingField = utcNow;
		request._003CDeviceId_003Ek__BackingField = _deviceID;
		request._003CUserId_003Ek__BackingField = _userId;
		object obj = default(object);
		bool lockTaken = default(bool);
		Monitor.Enter(obj, ref lockTaken);
		if (_genericEvents != null)
		{
			_genericEvents.Add(request);
			if (lockTaken)
			{
				Monitor.Exit(obj);
			}
			TryFlushIfFull();
			return;
		}
		throw new NullReferenceException();
	}

	public static Task<bool> BootEvent_Immediate(AnalyticsEventRequest_Boot request)
	{
		AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder = AsyncTaskMethodBuilder<bool>.Create();
		AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder2 = default(AsyncTaskMethodBuilder<bool>);
		_003CBootEvent_Immediate_003Ed__22 stateMachine = default(_003CBootEvent_Immediate_003Ed__22);
		asyncTaskMethodBuilder2.Start(ref stateMachine);
		return asyncTaskMethodBuilder2.Task;
	}

	public static Task<bool> MissionEvent_Immediate(AnalyticsEventRequest_Mission request)
	{
		AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder = AsyncTaskMethodBuilder<bool>.Create();
		AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder2 = default(AsyncTaskMethodBuilder<bool>);
		_003CMissionEvent_Immediate_003Ed__23 stateMachine = default(_003CMissionEvent_Immediate_003Ed__23);
		asyncTaskMethodBuilder2.Start(ref stateMachine);
		return asyncTaskMethodBuilder2.Task;
	}

	public static Task<bool> GenericEvent_Immediate(AnalyticsEventRequest_Generic request)
	{
		AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder = AsyncTaskMethodBuilder<bool>.Create();
		AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder2 = default(AsyncTaskMethodBuilder<bool>);
		_003CGenericEvent_Immediate_003Ed__24 stateMachine = default(_003CGenericEvent_Immediate_003Ed__24);
		asyncTaskMethodBuilder2.Start(ref stateMachine);
		return asyncTaskMethodBuilder2.Task;
	}

	private static void StartBatchLoop()
	{
		_003C_003Ec__DisplayClass25_0 CS_0024_003C_003E8__locals1 = new _003C_003Ec__DisplayClass25_0();
		if (!_batchLoopStarted)
		{
			_batchLoopStarted = true;
			CancellationTokenSource batchCts = new CancellationTokenSource();
			_batchCts = batchCts;
			CancellationToken token = _batchCts.Token;
			CS_0024_003C_003E8__locals1.token = token;
			Func<Task> function = delegate
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180788D50");
				AsyncTaskMethodBuilder asyncTaskMethodBuilder = default(AsyncTaskMethodBuilder);
				_003C_003Ec__DisplayClass25_0._003C_003CStartBatchLoop_003Eb__0_003Ed stateMachine = default(_003C_003Ec__DisplayClass25_0._003C_003CStartBatchLoop_003Eb__0_003Ed);
				asyncTaskMethodBuilder.Start(ref stateMachine);
				return asyncTaskMethodBuilder.Task;
			};
			Task task = Task.Run(function);
		}
	}

	private static void TryFlushIfFull()
	{
		//IL_00e2: Expected I, but got O
		//IL_0038: Expected O, but got I4
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Expected O, but got Unknown
		object obj = default(object);
		bool lockTaken = default(bool);
		Monitor.Enter(obj, ref lockTaken);
		nint num = (nint)typeof(AnalyticsClient);
		List<AnalyticsEventRequest_Boot> bootEvents = _bootEvents;
		if (_bootEvents != null)
		{
			List<AnalyticsEventRequest_Mission> missionEvents = _missionEvents;
			List<AnalyticsEventRequest_Generic> genericEvents = _genericEvents;
			object obj2 = genericEvents._size + missionEvents._size;
			object obj3 = obj2 + bootEvents._size;
			if (lockTaken)
			{
				Monitor.Exit(obj);
			}
			if ((nint)obj3 >= 100)
			{
				Task<bool> task = Flush();
			}
			return;
		}
		throw new NullReferenceException();
	}

	public static Task<bool> Flush()
	{
		AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder = AsyncTaskMethodBuilder<bool>.Create();
		AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder2 = default(AsyncTaskMethodBuilder<bool>);
		_003CFlush_003Ed__27 stateMachine = default(_003CFlush_003Ed__27);
		asyncTaskMethodBuilder2.Start(ref stateMachine);
		return asyncTaskMethodBuilder2.Task;
	}

	private static Task<bool> Flush(CancellationToken token)
	{
		AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder = AsyncTaskMethodBuilder<bool>.Create();
		AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder2 = default(AsyncTaskMethodBuilder<bool>);
		_003CFlush_003Ed__28 stateMachine = default(_003CFlush_003Ed__28);
		asyncTaskMethodBuilder2.Start(ref stateMachine);
		return asyncTaskMethodBuilder2.Task;
	}

	private static void StopBatchLoop(bool clearQueue)
	{
		if (_batchCts != null)
		{
			_batchCts.Cancel();
		}
		_batchCts = null;
		_batchLoopStarted = false;
		if (!clearQueue)
		{
			return;
		}
		object obj = default(object);
		bool lockTaken = default(bool);
		Monitor.Enter(obj, ref lockTaken);
		List<AnalyticsEventRequest_Boot> bootEvents = _bootEvents;
		if (_bootEvents != null)
		{
			int version = bootEvents._version + 1;
			bootEvents._version = version;
			if (!RuntimeHelpers.IsReferenceOrContainsReferences<AnalyticsEventRequest_Boot>())
			{
				bootEvents._size = 0;
			}
			else
			{
				bootEvents._size = 0;
				if (bootEvents._size > 0)
				{
					Array.Clear(bootEvents._items, 0, bootEvents._size);
				}
			}
			List<AnalyticsEventRequest_Mission> missionEvents = _missionEvents;
			if (_missionEvents != null)
			{
				int version2 = missionEvents._version + 1;
				missionEvents._version = version2;
				if (!RuntimeHelpers.IsReferenceOrContainsReferences<AnalyticsEventRequest_Mission>())
				{
					missionEvents._size = 0;
				}
				else
				{
					missionEvents._size = 0;
					if (missionEvents._size > 0)
					{
						Array.Clear(missionEvents._items, 0, missionEvents._size);
					}
				}
				List<AnalyticsEventRequest_Generic> genericEvents = _genericEvents;
				int version3 = genericEvents._version + 1;
				genericEvents._version = version3;
				if (!RuntimeHelpers.IsReferenceOrContainsReferences<AnalyticsEventRequest_Generic>())
				{
					genericEvents._size = 0;
				}
				else
				{
					genericEvents._size = 0;
					if (genericEvents._size > 0)
					{
						Array.Clear(genericEvents._items, 0, genericEvents._size);
					}
				}
				if (lockTaken)
				{
					Monitor.Exit(obj);
				}
				return;
			}
			throw new NullReferenceException();
		}
		throw new NullReferenceException();
	}

	static AnalyticsClient()
	{
		HttpClient http = new HttpClient();
		_http = http;
		object batchLock = new object();
		_batchLock = batchLock;
		List<AnalyticsEventRequest_Boot> bootEvents = new List<AnalyticsEventRequest_Boot>();
		_bootEvents = bootEvents;
		List<AnalyticsEventRequest_Mission> missionEvents = new List<AnalyticsEventRequest_Mission>();
		_missionEvents = missionEvents;
		List<AnalyticsEventRequest_Generic> genericEvents = new List<AnalyticsEventRequest_Generic>();
		_genericEvents = genericEvents;
		_jsonOptions = new JsonSerializerSettings
		{
			NullValueHandling = NullValueHandling.Ignore
		};
	}
}
