using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Cpp2ILInjected;
using Newtonsoft.Json;
using UnityEngine;

public static class LeaderboardClient
{
	[StructLayout((LayoutKind)3)]
	private struct _003CDiscordLinkRequest_003Ed__11 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder<bool> _003C_003Et__builder;

		public string code;

		private TaskAwaiter<HttpResponseMessage> _003C_003Eu__1;

		private unsafe void MoveNext()
		{
			//IL_0010: Expected O, but got I4
			//IL_001f: Expected I4, but got I8
			//IL_01b4: Expected I4, but got I8
			//IL_013b: Expected O, but got Ref
			//IL_011d: Expected O, but got Ref
			if (_003C_003E1__state == 0)
			{
				_003C_003Eu__1 = (TaskAwaiter<HttpResponseMessage>)0;
				_003C_003E1__state = -1;
				TaskAwaiter<HttpResponseMessage> taskAwaiter = _003C_003Eu__1;
			}
			else
			{
				string url = _baseUrl + "/api/v2/discord/link";
				LinkDiscordRequest linkDiscordRequest = new LinkDiscordRequest();
				linkDiscordRequest._003CUserId_003Ek__BackingField = _userId;
				linkDiscordRequest._003CKey_003Ek__BackingField = code;
				HttpRequestMessage request = CreateRequest(HttpMethod.post_method, url, linkDiscordRequest);
				Task<HttpResponseMessage> task = _http.SendAsync(request);
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
			HttpResponseMessage httpResponseMessage2 = default(HttpResponseMessage);
			HttpResponseMessage httpResponseMessage = httpResponseMessage2.EnsureSuccessStatusCode();
			RegisterResponse latestRegisterResponse = LatestRegisterResponse;
			latestRegisterResponse._003CDiscordLinked_003Ek__BackingField = true;
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
	private struct _003CGenerateDiscordLinkKey_003Ed__12 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder<string> _003C_003Et__builder;

		private TaskAwaiter<HttpResponseMessage> _003C_003Eu__1;

		private TaskAwaiter<string> _003C_003Eu__2;

		private unsafe void MoveNext()
		{
			//IL_0010: Expected O, but got I4
			//IL_001f: Expected I4, but got I8
			//IL_005b: Expected O, but got I4
			//IL_006a: Expected I4, but got I8
			//IL_02bd: Expected I4, but got I8
			//IL_01c8: Expected O, but got I
			//IL_01f2: Expected O, but got Ref
			//IL_0248: Expected O, but got Ref
			//IL_022a: Expected O, but got Ref
			TaskAwaiter<string> taskAwaiter2 = default(TaskAwaiter<string>);
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
					taskAwaiter2 = _003C_003Eu__2;
					goto IL_0170;
				}
				string url = _baseUrl + "/api/v2/discord/GenerateCode";
				GenerateDiscordLinkCodeRequest generateDiscordLinkCodeRequest = new GenerateDiscordLinkCodeRequest();
				generateDiscordLinkCodeRequest._003CUserId_003Ek__BackingField = _userId;
				HttpRequestMessage request = CreateRequest(HttpMethod.post_method, url, generateDiscordLinkCodeRequest);
				Task<HttpResponseMessage> task = _http.SendAsync(request);
				TaskAwaiter<HttpResponseMessage> awaiter = task.GetAwaiter();
				TaskAwaiter<HttpResponseMessage> taskAwaiter = default(TaskAwaiter<HttpResponseMessage>);
				if (!taskAwaiter.IsCompleted)
				{
					_003C_003E1__state = 0;
					_003C_003Eu__1 = taskAwaiter;
					AsyncTaskMethodBuilder<string> asyncTaskMethodBuilder = (AsyncTaskMethodBuilder<string>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
					((AsyncTaskMethodBuilder<string>*)asyncTaskMethodBuilder)->AwaitUnsafeOnCompleted(ref taskAwaiter, ref this);
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091FAF0");
			HttpResponseMessage httpResponseMessage2 = default(HttpResponseMessage);
			HttpResponseMessage httpResponseMessage = httpResponseMessage2.EnsureSuccessStatusCode();
			Task<string> task2 = httpResponseMessage2._003CContent_003Ek__BackingField.ReadAsStringAsync();
			TaskAwaiter<string> awaiter2 = task2.GetAwaiter();
			if (taskAwaiter2.IsCompleted)
			{
				goto IL_0170;
			}
			_003C_003E1__state = 1;
			_003C_003Eu__2 = taskAwaiter2;
			AsyncTaskMethodBuilder<string> asyncTaskMethodBuilder2 = (AsyncTaskMethodBuilder<string>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			((AsyncTaskMethodBuilder<string>*)asyncTaskMethodBuilder2)->AwaitUnsafeOnCompleted(ref taskAwaiter2, ref this);
			return;
			IL_0170:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091FAF0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18070B4B0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v401 @ stack_-60_v8+10]");
			bool flag = (nint)0 == 0;
			string result = null;
			if (!flag)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v401 @ stack_-60_v8+18]");
				result = (string)0;
			}
			_003C_003E1__state = -2;
			AsyncTaskMethodBuilder<string> asyncTaskMethodBuilder3 = (AsyncTaskMethodBuilder<string>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			((AsyncTaskMethodBuilder<string>*)asyncTaskMethodBuilder3)->SetResult(result);
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			this.MoveNext();
		}

		private unsafe void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//IL_0010: Expected O, but got Ref
			AsyncTaskMethodBuilder<string> asyncTaskMethodBuilder = (AsyncTaskMethodBuilder<string>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			((AsyncTaskMethodBuilder<string>*)asyncTaskMethodBuilder)->SetStateMachine(stateMachine);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
			this.SetStateMachine(stateMachine);
		}
	}

	[StructLayout((LayoutKind)3)]
	private struct _003CGetClientCombined_003Ed__15 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder<ClientCombinedLeaderboardResponse> _003C_003Et__builder;

		private TaskAwaiter<HttpResponseMessage> _003C_003Eu__1;

		private TaskAwaiter<string> _003C_003Eu__2;

		private unsafe void MoveNext()
		{
			//IL_0010: Expected O, but got I4
			//IL_001f: Expected I4, but got I8
			//IL_005b: Expected O, but got I4
			//IL_006a: Expected I4, but got I8
			//IL_0214: Expected O, but got Ref
			//IL_0092: Expected I, but got O
			//IL_02cd: Expected I4, but got I8
			//IL_00ce: Expected O, but got Ref
			//IL_02dd: Expected O, but got Ref
			//IL_010e: Expected I, but got O
			//IL_0137: Expected O, but got I4
			//IL_013f: Expected I, but got O
			//IL_014f: Expected O, but got I
			//IL_0315: Expected O, but got Ref
			//IL_0187: Expected O, but got I4
			//IL_018c: Expected I, but got O
			//IL_01dd: Expected O, but got I4
			//IL_034d: Expected O, but got Ref
			HttpResponseMessage httpResponseMessage = default(HttpResponseMessage);
			HttpRequestMessage httpRequestMessage;
			nint num;
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
					goto IL_02a5;
				}
				string url = _baseUrl + "/api/v2/leaderboard/clientcombined";
				ClientCombinedLeaderboardRequest clientCombinedLeaderboardRequest = new ClientCombinedLeaderboardRequest();
				bool flag = clientCombinedLeaderboardRequest == null;
				num = unchecked((nint)null);
				httpRequestMessage = null;
				if (flag)
				{
					throw new NullReferenceException();
				}
				clientCombinedLeaderboardRequest._003CUserId_003Ek__BackingField = _userId;
				DateTime utcNow = DateTime.UtcNow;
				DateTime? dateTime = (clientCombinedLeaderboardRequest._003CDayUtc_003Ek__BackingField = (DateTime)(&httpResponseMessage));
				HttpRequestMessage httpRequestMessage2 = CreateRequest(HttpMethod.post_method, url, clientCombinedLeaderboardRequest);
				nint num2 = (nint)typeof(LeaderboardClient);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v630 @ rcx_v47 (Il2CppClass<LeaderboardClient>)+B8]");
				nint num3 = 0;
				bool flag2 = _http == null;
				object obj = 0;
				num = (nint)clientCombinedLeaderboardRequest;
				DateTime? dateTime2 = dateTime;
				httpRequestMessage = (HttpRequestMessage)num3;
				if (flag2)
				{
					throw new NullReferenceException();
				}
				Task<HttpResponseMessage> task = _http.SendAsync(httpRequestMessage2);
				bool flag3 = task == null;
				obj = 0;
				num = unchecked((nint)null);
				dateTime2 = dateTime;
				httpRequestMessage = httpRequestMessage2;
				if (flag3)
				{
					throw new NullReferenceException();
				}
				TaskAwaiter<HttpResponseMessage> awaiter = task.GetAwaiter();
				TaskAwaiter<HttpResponseMessage> taskAwaiter = default(TaskAwaiter<HttpResponseMessage>);
				bool isCompleted = taskAwaiter.IsCompleted;
				bool flag4 = !isCompleted;
				obj = 0;
				dateTime2 = dateTime;
				if (flag4)
				{
					_003C_003E1__state = 0;
					_003C_003Eu__1 = taskAwaiter;
					AsyncTaskMethodBuilder<ClientCombinedLeaderboardResponse> asyncTaskMethodBuilder = (AsyncTaskMethodBuilder<ClientCombinedLeaderboardResponse>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
					((AsyncTaskMethodBuilder<ClientCombinedLeaderboardResponse>*)asyncTaskMethodBuilder)->AwaitUnsafeOnCompleted(ref taskAwaiter, ref this);
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091FAF0");
			bool flag5 = httpResponseMessage == null;
			httpRequestMessage = (HttpRequestMessage)(&httpResponseMessage);
			if (!flag5)
			{
				HttpResponseMessage httpResponseMessage2 = httpResponseMessage.EnsureSuccessStatusCode();
				bool flag6 = httpResponseMessage._003CContent_003Ek__BackingField == null;
				httpRequestMessage = null;
				if (!flag6)
				{
					Task<string> task2 = httpResponseMessage._003CContent_003Ek__BackingField.ReadAsStringAsync();
					TaskAwaiter<string> awaiter2 = task2.GetAwaiter();
					TaskAwaiter<string> taskAwaiter2 = default(TaskAwaiter<string>);
					if (taskAwaiter2.IsCompleted)
					{
						goto IL_02a5;
					}
					_003C_003E1__state = 1;
					_003C_003Eu__2 = taskAwaiter2;
					AsyncTaskMethodBuilder<ClientCombinedLeaderboardResponse> asyncTaskMethodBuilder2 = (AsyncTaskMethodBuilder<ClientCombinedLeaderboardResponse>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
					((AsyncTaskMethodBuilder<ClientCombinedLeaderboardResponse>*)asyncTaskMethodBuilder2)->AwaitUnsafeOnCompleted(ref taskAwaiter2, ref this);
					return;
				}
				throw new NullReferenceException();
			}
			num = 0;
			throw new NullReferenceException();
			IL_02a5:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091FAF0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18070B4B0");
			_003C_003E1__state = -2;
			AsyncTaskMethodBuilder<ClientCombinedLeaderboardResponse> asyncTaskMethodBuilder3 = (AsyncTaskMethodBuilder<ClientCombinedLeaderboardResponse>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			ClientCombinedLeaderboardResponse result = default(ClientCombinedLeaderboardResponse);
			((AsyncTaskMethodBuilder<ClientCombinedLeaderboardResponse>*)asyncTaskMethodBuilder3)->SetResult(result);
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			this.MoveNext();
		}

		private unsafe void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//IL_0010: Expected O, but got Ref
			AsyncTaskMethodBuilder<ClientCombinedLeaderboardResponse> asyncTaskMethodBuilder = (AsyncTaskMethodBuilder<ClientCombinedLeaderboardResponse>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			((AsyncTaskMethodBuilder<ClientCombinedLeaderboardResponse>*)asyncTaskMethodBuilder)->SetStateMachine(stateMachine);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
			this.SetStateMachine(stateMachine);
		}
	}

	[StructLayout((LayoutKind)3)]
	private struct _003CGetMine_003Ed__17 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder<GetMyLeaderboardResponse> _003C_003Et__builder;

		public Gamemodes gamemode;

		private TaskAwaiter<HttpResponseMessage> _003C_003Eu__1;

		private TaskAwaiter<string> _003C_003Eu__2;

		private unsafe void MoveNext()
		{
			//IL_0010: Expected O, but got I4
			//IL_001f: Expected I4, but got I8
			//IL_005b: Expected O, but got I4
			//IL_006a: Expected I4, but got I8
			//IL_01de: Expected O, but got Ref
			//IL_0092: Expected I, but got O
			//IL_0297: Expected I4, but got I8
			//IL_02a7: Expected O, but got Ref
			//IL_00f0: Expected I, but got O
			//IL_0118: Expected I, but got O
			//IL_0121: Expected O, but got I4
			//IL_0129: Expected O, but got I
			//IL_015d: Expected I, but got O
			//IL_0166: Expected O, but got I4
			//IL_02df: Expected O, but got Ref
			//IL_01af: Expected O, but got I4
			//IL_0317: Expected O, but got Ref
			HttpRequestMessage httpRequestMessage;
			nint num;
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
					goto IL_026f;
				}
				string url = _baseUrl + "/api/v2/leaderboard/mine";
				GetSessionKeyRequest getSessionKeyRequest = new GetSessionKeyRequest();
				bool flag = getSessionKeyRequest == null;
				num = unchecked((nint)null);
				httpRequestMessage = null;
				if (flag)
				{
					throw new NullReferenceException();
				}
				getSessionKeyRequest._003CUserId_003Ek__BackingField = _userId;
				getSessionKeyRequest._003CGamemode_003Ek__BackingField = gamemode;
				HttpRequestMessage httpRequestMessage2 = CreateRequest(HttpMethod.post_method, url, getSessionKeyRequest);
				nint num2 = (nint)typeof(LeaderboardClient);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v584 @ rcx_v44 (Il2CppClass<LeaderboardClient>)+B8]");
				nint num3 = 0;
				bool flag2 = _http == null;
				num = (nint)getSessionKeyRequest;
				object obj = 0;
				httpRequestMessage = (HttpRequestMessage)num3;
				if (flag2)
				{
					throw new NullReferenceException();
				}
				Task<HttpResponseMessage> task = _http.SendAsync(httpRequestMessage2);
				bool flag3 = task == null;
				num = unchecked((nint)null);
				obj = 0;
				httpRequestMessage = httpRequestMessage2;
				if (flag3)
				{
					throw new NullReferenceException();
				}
				TaskAwaiter<HttpResponseMessage> awaiter = task.GetAwaiter();
				TaskAwaiter<HttpResponseMessage> taskAwaiter = default(TaskAwaiter<HttpResponseMessage>);
				bool isCompleted = taskAwaiter.IsCompleted;
				bool flag4 = !isCompleted;
				obj = 0;
				if (flag4)
				{
					_003C_003E1__state = 0;
					_003C_003Eu__1 = taskAwaiter;
					AsyncTaskMethodBuilder<GetMyLeaderboardResponse> asyncTaskMethodBuilder = (AsyncTaskMethodBuilder<GetMyLeaderboardResponse>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
					((AsyncTaskMethodBuilder<GetMyLeaderboardResponse>*)asyncTaskMethodBuilder)->AwaitUnsafeOnCompleted(ref taskAwaiter, ref this);
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091FAF0");
			HttpResponseMessage httpResponseMessage = default(HttpResponseMessage);
			bool flag5 = httpResponseMessage == null;
			httpRequestMessage = (HttpRequestMessage)(&httpResponseMessage);
			if (!flag5)
			{
				HttpResponseMessage httpResponseMessage2 = httpResponseMessage.EnsureSuccessStatusCode();
				bool flag6 = httpResponseMessage._003CContent_003Ek__BackingField == null;
				httpRequestMessage = null;
				if (!flag6)
				{
					Task<string> task2 = httpResponseMessage._003CContent_003Ek__BackingField.ReadAsStringAsync();
					TaskAwaiter<string> awaiter2 = task2.GetAwaiter();
					TaskAwaiter<string> taskAwaiter2 = default(TaskAwaiter<string>);
					if (taskAwaiter2.IsCompleted)
					{
						goto IL_026f;
					}
					_003C_003E1__state = 1;
					_003C_003Eu__2 = taskAwaiter2;
					AsyncTaskMethodBuilder<GetMyLeaderboardResponse> asyncTaskMethodBuilder2 = (AsyncTaskMethodBuilder<GetMyLeaderboardResponse>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
					((AsyncTaskMethodBuilder<GetMyLeaderboardResponse>*)asyncTaskMethodBuilder2)->AwaitUnsafeOnCompleted(ref taskAwaiter2, ref this);
					return;
				}
				throw new NullReferenceException();
			}
			num = 0;
			throw new NullReferenceException();
			IL_026f:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091FAF0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18070B4B0");
			_003C_003E1__state = -2;
			AsyncTaskMethodBuilder<GetMyLeaderboardResponse> asyncTaskMethodBuilder3 = (AsyncTaskMethodBuilder<GetMyLeaderboardResponse>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			GetMyLeaderboardResponse result = default(GetMyLeaderboardResponse);
			((AsyncTaskMethodBuilder<GetMyLeaderboardResponse>*)asyncTaskMethodBuilder3)->SetResult(result);
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			this.MoveNext();
		}

		private unsafe void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//IL_0010: Expected O, but got Ref
			AsyncTaskMethodBuilder<GetMyLeaderboardResponse> asyncTaskMethodBuilder = (AsyncTaskMethodBuilder<GetMyLeaderboardResponse>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			((AsyncTaskMethodBuilder<GetMyLeaderboardResponse>*)asyncTaskMethodBuilder)->SetStateMachine(stateMachine);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
			this.SetStateMachine(stateMachine);
		}
	}

	[StructLayout((LayoutKind)3)]
	private struct _003CGetSessionKey_003Ed__18 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder<GetSessionKeyResponse> _003C_003Et__builder;

		public Gamemodes gamemode;

		public string performanceJson;

		private TaskAwaiter<HttpResponseMessage> _003C_003Eu__1;

		private TaskAwaiter<string> _003C_003Eu__2;

		private unsafe void MoveNext()
		{
			//IL_0010: Expected O, but got I4
			//IL_001f: Expected I4, but got I8
			//IL_005b: Expected O, but got I4
			//IL_006a: Expected I4, but got I8
			//IL_01ed: Expected O, but got Ref
			//IL_0092: Expected I, but got O
			//IL_02a6: Expected I4, but got I8
			//IL_02b6: Expected O, but got Ref
			//IL_00ff: Expected I, but got O
			//IL_0127: Expected I, but got O
			//IL_0130: Expected O, but got I4
			//IL_0138: Expected O, but got I
			//IL_016c: Expected I, but got O
			//IL_0175: Expected O, but got I4
			//IL_02ee: Expected O, but got Ref
			//IL_01be: Expected O, but got I4
			//IL_0326: Expected O, but got Ref
			HttpRequestMessage httpRequestMessage;
			nint num;
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
					goto IL_027e;
				}
				string url = _baseUrl + "/api/v2/leaderboard/session";
				GetSessionKeyRequest getSessionKeyRequest = new GetSessionKeyRequest();
				bool flag = getSessionKeyRequest == null;
				num = unchecked((nint)null);
				httpRequestMessage = null;
				if (flag)
				{
					throw new NullReferenceException();
				}
				getSessionKeyRequest._003CUserId_003Ek__BackingField = _userId;
				getSessionKeyRequest._003CGamemode_003Ek__BackingField = gamemode;
				getSessionKeyRequest._003CPerformanceStatsJson_003Ek__BackingField = performanceJson;
				HttpRequestMessage httpRequestMessage2 = CreateRequest(HttpMethod.post_method, url, getSessionKeyRequest);
				nint num2 = (nint)typeof(LeaderboardClient);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v592 @ rcx_v45 (Il2CppClass<LeaderboardClient>)+B8]");
				nint num3 = 0;
				bool flag2 = _http == null;
				num = (nint)getSessionKeyRequest;
				object obj = 0;
				httpRequestMessage = (HttpRequestMessage)num3;
				if (flag2)
				{
					throw new NullReferenceException();
				}
				Task<HttpResponseMessage> task = _http.SendAsync(httpRequestMessage2);
				bool flag3 = task == null;
				num = unchecked((nint)null);
				obj = 0;
				httpRequestMessage = httpRequestMessage2;
				if (flag3)
				{
					throw new NullReferenceException();
				}
				TaskAwaiter<HttpResponseMessage> awaiter = task.GetAwaiter();
				TaskAwaiter<HttpResponseMessage> taskAwaiter = default(TaskAwaiter<HttpResponseMessage>);
				bool isCompleted = taskAwaiter.IsCompleted;
				bool flag4 = !isCompleted;
				obj = 0;
				if (flag4)
				{
					_003C_003E1__state = 0;
					_003C_003Eu__1 = taskAwaiter;
					AsyncTaskMethodBuilder<GetSessionKeyResponse> asyncTaskMethodBuilder = (AsyncTaskMethodBuilder<GetSessionKeyResponse>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
					((AsyncTaskMethodBuilder<GetSessionKeyResponse>*)asyncTaskMethodBuilder)->AwaitUnsafeOnCompleted(ref taskAwaiter, ref this);
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091FAF0");
			HttpResponseMessage httpResponseMessage = default(HttpResponseMessage);
			bool flag5 = httpResponseMessage == null;
			httpRequestMessage = (HttpRequestMessage)(&httpResponseMessage);
			if (!flag5)
			{
				HttpResponseMessage httpResponseMessage2 = httpResponseMessage.EnsureSuccessStatusCode();
				bool flag6 = httpResponseMessage._003CContent_003Ek__BackingField == null;
				httpRequestMessage = null;
				if (!flag6)
				{
					Task<string> task2 = httpResponseMessage._003CContent_003Ek__BackingField.ReadAsStringAsync();
					TaskAwaiter<string> awaiter2 = task2.GetAwaiter();
					TaskAwaiter<string> taskAwaiter2 = default(TaskAwaiter<string>);
					if (taskAwaiter2.IsCompleted)
					{
						goto IL_027e;
					}
					_003C_003E1__state = 1;
					_003C_003Eu__2 = taskAwaiter2;
					AsyncTaskMethodBuilder<GetSessionKeyResponse> asyncTaskMethodBuilder2 = (AsyncTaskMethodBuilder<GetSessionKeyResponse>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
					((AsyncTaskMethodBuilder<GetSessionKeyResponse>*)asyncTaskMethodBuilder2)->AwaitUnsafeOnCompleted(ref taskAwaiter2, ref this);
					return;
				}
				throw new NullReferenceException();
			}
			num = 0;
			throw new NullReferenceException();
			IL_027e:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091FAF0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18070B4B0");
			_003C_003E1__state = -2;
			AsyncTaskMethodBuilder<GetSessionKeyResponse> asyncTaskMethodBuilder3 = (AsyncTaskMethodBuilder<GetSessionKeyResponse>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			GetSessionKeyResponse result = default(GetSessionKeyResponse);
			((AsyncTaskMethodBuilder<GetSessionKeyResponse>*)asyncTaskMethodBuilder3)->SetResult(result);
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			this.MoveNext();
		}

		private unsafe void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//IL_0010: Expected O, but got Ref
			AsyncTaskMethodBuilder<GetSessionKeyResponse> asyncTaskMethodBuilder = (AsyncTaskMethodBuilder<GetSessionKeyResponse>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			((AsyncTaskMethodBuilder<GetSessionKeyResponse>*)asyncTaskMethodBuilder)->SetStateMachine(stateMachine);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
			this.SetStateMachine(stateMachine);
		}
	}

	[StructLayout((LayoutKind)3)]
	private struct _003CGetTop_003Ed__16 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder<GetTopLeaderboardResponse> _003C_003Et__builder;

		public int count;

		public Gamemodes gamemode;

		private TaskAwaiter<HttpResponseMessage> _003C_003Eu__1;

		private TaskAwaiter<string> _003C_003Eu__2;

		private unsafe void MoveNext()
		{
			//IL_0010: Expected O, but got I4
			//IL_001f: Expected I4, but got I8
			//IL_005b: Expected O, but got I4
			//IL_006a: Expected I4, but got I8
			//IL_01ed: Expected O, but got Ref
			//IL_0092: Expected I, but got O
			//IL_02a6: Expected I4, but got I8
			//IL_02b6: Expected O, but got Ref
			//IL_00ff: Expected I, but got O
			//IL_0127: Expected I, but got O
			//IL_0130: Expected O, but got I4
			//IL_0138: Expected O, but got I
			//IL_016c: Expected I, but got O
			//IL_0175: Expected O, but got I4
			//IL_02ee: Expected O, but got Ref
			//IL_01be: Expected O, but got I4
			//IL_0326: Expected O, but got Ref
			HttpRequestMessage httpRequestMessage;
			nint num;
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
					goto IL_027e;
				}
				string url = _baseUrl + "/api/v2/leaderboard/top";
				GetLeaderboardRequest getLeaderboardRequest = new GetLeaderboardRequest();
				bool flag = getLeaderboardRequest == null;
				num = unchecked((nint)null);
				httpRequestMessage = null;
				if (flag)
				{
					throw new NullReferenceException();
				}
				getLeaderboardRequest._003CAmount_003Ek__BackingField = count;
				getLeaderboardRequest._003CUserId_003Ek__BackingField = _userId;
				getLeaderboardRequest._003CGamemode_003Ek__BackingField = gamemode;
				HttpRequestMessage httpRequestMessage2 = CreateRequest(HttpMethod.post_method, url, getLeaderboardRequest);
				nint num2 = (nint)typeof(LeaderboardClient);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v592 @ rcx_v44 (Il2CppClass<LeaderboardClient>)+B8]");
				nint num3 = 0;
				bool flag2 = _http == null;
				num = (nint)getLeaderboardRequest;
				object obj = 0;
				httpRequestMessage = (HttpRequestMessage)num3;
				if (flag2)
				{
					throw new NullReferenceException();
				}
				Task<HttpResponseMessage> task = _http.SendAsync(httpRequestMessage2);
				bool flag3 = task == null;
				num = unchecked((nint)null);
				obj = 0;
				httpRequestMessage = httpRequestMessage2;
				if (flag3)
				{
					throw new NullReferenceException();
				}
				TaskAwaiter<HttpResponseMessage> awaiter = task.GetAwaiter();
				TaskAwaiter<HttpResponseMessage> taskAwaiter = default(TaskAwaiter<HttpResponseMessage>);
				bool isCompleted = taskAwaiter.IsCompleted;
				bool flag4 = !isCompleted;
				obj = 0;
				if (flag4)
				{
					_003C_003E1__state = 0;
					_003C_003Eu__1 = taskAwaiter;
					AsyncTaskMethodBuilder<GetTopLeaderboardResponse> asyncTaskMethodBuilder = (AsyncTaskMethodBuilder<GetTopLeaderboardResponse>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
					((AsyncTaskMethodBuilder<GetTopLeaderboardResponse>*)asyncTaskMethodBuilder)->AwaitUnsafeOnCompleted(ref taskAwaiter, ref this);
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091FAF0");
			HttpResponseMessage httpResponseMessage = default(HttpResponseMessage);
			bool flag5 = httpResponseMessage == null;
			httpRequestMessage = (HttpRequestMessage)(&httpResponseMessage);
			if (!flag5)
			{
				HttpResponseMessage httpResponseMessage2 = httpResponseMessage.EnsureSuccessStatusCode();
				bool flag6 = httpResponseMessage._003CContent_003Ek__BackingField == null;
				httpRequestMessage = null;
				if (!flag6)
				{
					Task<string> task2 = httpResponseMessage._003CContent_003Ek__BackingField.ReadAsStringAsync();
					TaskAwaiter<string> awaiter2 = task2.GetAwaiter();
					TaskAwaiter<string> taskAwaiter2 = default(TaskAwaiter<string>);
					if (taskAwaiter2.IsCompleted)
					{
						goto IL_027e;
					}
					_003C_003E1__state = 1;
					_003C_003Eu__2 = taskAwaiter2;
					AsyncTaskMethodBuilder<GetTopLeaderboardResponse> asyncTaskMethodBuilder2 = (AsyncTaskMethodBuilder<GetTopLeaderboardResponse>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
					((AsyncTaskMethodBuilder<GetTopLeaderboardResponse>*)asyncTaskMethodBuilder2)->AwaitUnsafeOnCompleted(ref taskAwaiter2, ref this);
					return;
				}
				throw new NullReferenceException();
			}
			num = 0;
			throw new NullReferenceException();
			IL_027e:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091FAF0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18070B4B0");
			_003C_003E1__state = -2;
			AsyncTaskMethodBuilder<GetTopLeaderboardResponse> asyncTaskMethodBuilder3 = (AsyncTaskMethodBuilder<GetTopLeaderboardResponse>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			GetTopLeaderboardResponse result = default(GetTopLeaderboardResponse);
			((AsyncTaskMethodBuilder<GetTopLeaderboardResponse>*)asyncTaskMethodBuilder3)->SetResult(result);
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			this.MoveNext();
		}

		private unsafe void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//IL_0010: Expected O, but got Ref
			AsyncTaskMethodBuilder<GetTopLeaderboardResponse> asyncTaskMethodBuilder = (AsyncTaskMethodBuilder<GetTopLeaderboardResponse>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			((AsyncTaskMethodBuilder<GetTopLeaderboardResponse>*)asyncTaskMethodBuilder)->SetStateMachine(stateMachine);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
			this.SetStateMachine(stateMachine);
		}
	}

	[StructLayout((LayoutKind)3)]
	private struct _003CPushOperationState_003Ed__14 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder<bool> _003C_003Et__builder;

		public PostOperationStateRequest request;

		private TaskAwaiter<HttpResponseMessage> _003C_003Eu__1;

		private unsafe void MoveNext()
		{
			//IL_0010: Expected O, but got I4
			//IL_001f: Expected I4, but got I8
			//IL_0186: Expected I4, but got I8
			//IL_011b: Expected O, but got Ref
			//IL_00fd: Expected O, but got Ref
			if (_003C_003E1__state == 0)
			{
				_003C_003Eu__1 = (TaskAwaiter<HttpResponseMessage>)0;
				_003C_003E1__state = -1;
				TaskAwaiter<HttpResponseMessage> taskAwaiter = _003C_003Eu__1;
			}
			else
			{
				string url = _baseUrl + "/api/v2/leaderboard/PushOperationState";
				PostOperationStateRequest postOperationStateRequest = request;
				postOperationStateRequest._003CUserId_003Ek__BackingField = _userId;
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
			HttpResponseMessage httpResponseMessage2 = default(HttpResponseMessage);
			HttpResponseMessage httpResponseMessage = httpResponseMessage2.EnsureSuccessStatusCode();
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
	private struct _003CRegister_003Ed__13 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder<RegisterResponse> _003C_003Et__builder;

		public RegisterRequest request;

		private TaskAwaiter<HttpResponseMessage> _003C_003Eu__1;

		private TaskAwaiter<string> _003C_003Eu__2;

		private unsafe void MoveNext()
		{
			//IL_0010: Expected O, but got I4
			//IL_001f: Expected I4, but got I8
			//IL_0028: Expected O, but got I4
			//IL_0538: Expected O, but got I4
			//IL_0064: Expected O, but got I4
			//IL_0073: Expected I4, but got I8
			//IL_00f7: Expected O, but got I4
			//IL_0122: Expected O, but got Ref
			//IL_05bd: Expected I4, but got I8
			//IL_0141: Expected O, but got I4
			//IL_0398: Expected O, but got Ref
			//IL_03d1: Expected O, but got Ref
			//IL_0388: Expected O, but got I4
			//IL_01e2: Expected I, but got O
			//IL_045f: Expected O, but got I
			//IL_0227: Expected I, but got O
			//IL_0409: Expected O, but got Ref
			RegisterResponse latestRegisterResponse = default(RegisterResponse);
			if (_003C_003E1__state == 0)
			{
				_003C_003Eu__1 = (TaskAwaiter<HttpResponseMessage>)0;
				_003C_003E1__state = -1;
				Guid? guid = (Guid?)(object)0;
				TaskAwaiter<HttpResponseMessage> taskAwaiter = _003C_003Eu__1;
			}
			else
			{
				if (_003C_003E1__state == 1)
				{
					_003C_003Eu__2 = (TaskAwaiter<string>)0;
					_003C_003E1__state = -1;
					TaskAwaiter<string> taskAwaiter2 = _003C_003Eu__2;
					goto IL_02f5;
				}
				RegisterRequest registerRequest = request;
				bool flag = request == null;
				Guid? guid = (Guid?)(object)0;
				string text = (string)(object)request;
				if (flag)
				{
					throw new NullReferenceException();
				}
				text = registerRequest._003CUsername_003Ek__BackingField;
				string message = "[Leaderboard] Registering user: " + _deviceID + " | " + registerRequest._003CUsername_003Ek__BackingField;
				Debug.Log(message);
				string url = _baseUrl + "/api/v2/leaderboard/register";
				bool flag2 = request == null;
				guid = (Guid?)(object)0;
				if (flag2)
				{
					throw new NullReferenceException();
				}
				_ = _deviceID;
				RegisterRequest registerRequest2 = request;
				Guid? guid2 = (Guid)(&latestRegisterResponse);
				bool flag3 = request == null;
				guid = (Guid?)(object)0;
				if (flag3)
				{
					throw new NullReferenceException();
				}
				registerRequest2._003CUserId_003Ek__BackingField = guid2;
				_ = 0;
				HttpRequestMessage httpRequestMessage = CreateRequest(HttpMethod.post_method, url, request);
				bool flag4 = _http == null;
				guid = guid2;
				text = null;
				if (flag4)
				{
					throw new NullReferenceException();
				}
				Task<HttpResponseMessage> task = _http.SendAsync(httpRequestMessage);
				bool flag5 = task == null;
				guid = guid2;
				nint num = unchecked((nint)null);
				if (flag5)
				{
					text = (string)num;
					throw new NullReferenceException();
				}
				TaskAwaiter<HttpResponseMessage> awaiter = task.GetAwaiter();
				TaskAwaiter<HttpResponseMessage> taskAwaiter = default(TaskAwaiter<HttpResponseMessage>);
				bool isCompleted = taskAwaiter.IsCompleted;
				bool flag6 = !isCompleted;
				guid = guid2;
				num = unchecked((nint)null);
				if (flag6)
				{
					_003C_003E1__state = 0;
					_003C_003Eu__1 = taskAwaiter;
					AsyncTaskMethodBuilder<RegisterResponse> asyncTaskMethodBuilder = (AsyncTaskMethodBuilder<RegisterResponse>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
					((AsyncTaskMethodBuilder<RegisterResponse>*)asyncTaskMethodBuilder)->AwaitUnsafeOnCompleted(ref taskAwaiter, ref this);
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091FAF0");
			HttpResponseMessage httpResponseMessage = default(HttpResponseMessage);
			if (httpResponseMessage != null)
			{
				HttpResponseMessage httpResponseMessage2 = httpResponseMessage.EnsureSuccessStatusCode();
				if (httpResponseMessage._003CContent_003Ek__BackingField != null)
				{
					Task<string> task2 = httpResponseMessage._003CContent_003Ek__BackingField.ReadAsStringAsync();
					if (task2 != null)
					{
						TaskAwaiter<string> awaiter2 = task2.GetAwaiter();
						TaskAwaiter<string> taskAwaiter2 = default(TaskAwaiter<string>);
						if (taskAwaiter2.IsCompleted)
						{
							goto IL_02f5;
						}
						_003C_003E1__state = 1;
						_003C_003Eu__2 = taskAwaiter2;
						AsyncTaskMethodBuilder<RegisterResponse> asyncTaskMethodBuilder2 = (AsyncTaskMethodBuilder<RegisterResponse>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
						((AsyncTaskMethodBuilder<RegisterResponse>*)asyncTaskMethodBuilder2)->AwaitUnsafeOnCompleted(ref taskAwaiter2, ref this);
						return;
					}
					throw new NullReferenceException();
				}
				throw new NullReferenceException();
			}
			throw new NullReferenceException();
			IL_02f5:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091FAF0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18070B4B0");
			LatestRegisterResponse = latestRegisterResponse;
			if (LatestRegisterResponse != null)
			{
				PlayerPrefs.SetString("IN_DeviceID", _deviceID);
				RegisterResponse latestRegisterResponse2 = LatestRegisterResponse;
				Guid guid3 = default(Guid);
				if (LatestRegisterResponse == null)
				{
					Guid? guid = guid3;
					nint num = 0;
					throw new NullReferenceException();
				}
				guid3 = latestRegisterResponse2._003CUserId_003Ek__BackingField;
				Guid guid4 = default(Guid);
				string value = guid4.ToString();
				PlayerPrefs.SetString("IN_UserID", value);
				string latestRegisterResponse3 = (string)(object)LatestRegisterResponse;
				_userId = (Guid)latestRegisterResponse3._stringLength;
				AnalyticsClient._deviceID = _deviceID;
				AnalyticsClient._userId = _userId;
			}
			_003C_003E1__state = -2;
			AsyncTaskMethodBuilder<RegisterResponse> asyncTaskMethodBuilder3 = (AsyncTaskMethodBuilder<RegisterResponse>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			((AsyncTaskMethodBuilder<RegisterResponse>*)asyncTaskMethodBuilder3)->SetResult(LatestRegisterResponse);
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			this.MoveNext();
		}

		private unsafe void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//IL_0010: Expected O, but got Ref
			AsyncTaskMethodBuilder<RegisterResponse> asyncTaskMethodBuilder = (AsyncTaskMethodBuilder<RegisterResponse>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			((AsyncTaskMethodBuilder<RegisterResponse>*)asyncTaskMethodBuilder)->SetStateMachine(stateMachine);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
			this.SetStateMachine(stateMachine);
		}
	}

	[StructLayout((LayoutKind)3)]
	private struct _003CSubmitScore_003Ed__19 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder<PostLeaderboardScoreResponse> _003C_003Et__builder;

		public PostLeaderboardScoreRequest request;

		public byte[] zipBytes;

		private HttpRequestMessage _003Creq_003E5__2;

		private HttpResponseMessage _003Cres_003E5__3;

		private TaskAwaiter<HttpResponseMessage> _003C_003Eu__1;

		private TaskAwaiter<string> _003C_003Eu__2;

		private unsafe void MoveNext()
		{
			//IL_0054: Expected O, but got I
			//IL_0061: Expected O, but got I8
			//IL_0803: Expected O, but got I
			//IL_0811: Expected I, but got O
			//IL_00a9: Expected O, but got I
			//IL_00b6: Expected O, but got I8
			//IL_04a2: Expected O, but got Ref
			//IL_0398: Expected O, but got I
			//IL_04c1: Expected O, but got I
			//IL_04cb: Expected O, but got I
			//IL_057a: Expected I, but got O
			//IL_040f: Expected O, but got I
			//IL_0599: Expected O, but got I
			//IL_0517: Expected I4, but got O
			//IL_011a: Expected O, but got I
			//IL_063d: Expected O, but got Ref
			//IL_0168: Expected I, but got O
			//IL_060e: Expected O, but got Ref
			//IL_01a0: Expected O, but got I
			//IL_01c5: Expected I, but got O
			//IL_0205: Expected I, but got O
			//IL_0234: Expected O, but got I
			//IL_0252: Expected I, but got O
			//IL_0262: Expected O, but got I
			//IL_0300: Expected O, but got I
			//IL_0316: Expected O, but got I
			//IL_032e: Expected I, but got O
			//IL_0692: Expected O, but got Ref
			//IL_02d1: Expected I, but got O
			if (_003C_003E1__state > 1)
			{
				_003Creq_003E5__2 = null;
			}
			object obj = default(object);
			nint num4;
			HttpRequestMessage httpRequestMessage;
			nint num3;
			if (obj == null)
			{
				_ = 0;
				ref _003CSubmitScore_003Ed__19 reference = ref *(_003CSubmitScore_003Ed__19*)4294967295L;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v74 @ stack_8_v2 (<SubmitScore>d__19&)+40]");
				TaskAwaiter<HttpResponseMessage> taskAwaiter = (TaskAwaiter<HttpResponseMessage>)0;
				obj = 4294967295L;
			}
			else
			{
				ref _003CSubmitScore_003Ed__19 reference = default(ref _003CSubmitScore_003Ed__19);
				if ((nint)obj == 1)
				{
					_ = 0;
					reference = ref *(_003CSubmitScore_003Ed__19*)4294967295L;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v74 @ stack_8_v2 (<SubmitScore>d__19&)+48]");
					TaskAwaiter<string> taskAwaiter2 = (TaskAwaiter<string>)0;
					obj = 4294967295L;
					goto IL_0473;
				}
				string requestUri = _baseUrl + "/api/v2/leaderboard/submit";
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v74 @ stack_8_v2 (<SubmitScore>d__19&)+20]");
				httpRequestMessage = (HttpRequestMessage)0;
				nint num = (nint)typeof(LeaderboardClient);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v275 @ rcx_v62 (Il2CppClass<LeaderboardClient>)+B8]");
				nint num2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v74 @ stack_8_v2 (<SubmitScore>d__19&)+20]");
				if ((nint)0 == 0)
				{
					throw new NullReferenceException();
				}
				httpRequestMessage.headers = (HttpRequestHeaders)_userId;
				HttpRequestMessage httpRequestMessage2 = new HttpRequestMessage(HttpMethod.post_method, requestUri);
				MultipartFormDataContent multipartFormDataContent = new MultipartFormDataContent();
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v74 @ stack_8_v2 (<SubmitScore>d__19&)+20]");
				string text = JsonConvert.SerializeObject(0);
				Encoding uTF = Encoding.UTF8;
				StringContent stringContent = new StringContent(text, uTF, "application/json");
				bool flag = multipartFormDataContent == null;
				string text2 = "application/json";
				num2 = (nint)uTF;
				httpRequestMessage = (HttpRequestMessage)(object)text;
				if (flag)
				{
					throw new NullReferenceException();
				}
				multipartFormDataContent.Add(stringContent, "RequestJson");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v74 @ stack_8_v2 (<SubmitScore>d__19&)+28]");
				byte[] array = (byte[])0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v74 @ stack_8_v2 (<SubmitScore>d__19&)+28]");
				bool flag2 = (nint)0 == 0;
				text2 = null;
				num3 = unchecked((nint)"RequestJson");
				httpRequestMessage = (HttpRequestMessage)(object)stringContent;
				ref _003CSubmitScore_003Ed__19 reference2 = ref reference;
				if (!flag2)
				{
					bool flag3 = array.Length <= 0;
					text2 = null;
					num3 = unchecked((nint)"RequestJson");
					httpRequestMessage = (HttpRequestMessage)(object)stringContent;
					reference2 = ref reference;
					if (!flag3)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v74 @ stack_8_v2 (<SubmitScore>d__19&)+28]");
						ByteArrayContent byteArrayContent = new ByteArrayContent((byte[])0);
						bool flag4 = byteArrayContent == null;
						num4 = unchecked((nint)null);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v74 @ stack_8_v2 (<SubmitScore>d__19&)+28]");
						byte[] array2 = (byte[])0;
						if (flag4)
						{
							text2 = null;
							throw new NullReferenceException();
						}
						HttpContentHeaders headers = byteArrayContent.Headers;
						MediaTypeHeaderValue contentType = new MediaTypeHeaderValue("application/zip");
						headers.ContentType = contentType;
						multipartFormDataContent.Add(byteArrayContent, "ReplayZip", "replay.zip");
						text2 = "replay.zip";
						num3 = unchecked((nint)"ReplayZip");
						httpRequestMessage = (HttpRequestMessage)(object)byteArrayContent;
						reference2 = ref reference;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v734 @ rax_v110 (<SubmitScore>d__19&)+30]");
				if ((nint)0 == 0)
				{
					num2 = num3;
					throw new NullReferenceException();
				}
				bool flag5 = _http == null;
				httpRequestMessage = (HttpRequestMessage)(object)multipartFormDataContent;
				if (flag5)
				{
					throw new NullReferenceException();
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v74 @ stack_8_v2 (<SubmitScore>d__19&)+30]");
				httpRequestMessage = (HttpRequestMessage)0;
				HttpClient http = _http;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v74 @ stack_8_v2 (<SubmitScore>d__19&)+30]");
				Task<HttpResponseMessage> task = http.SendAsync((HttpRequestMessage)0);
				bool flag6 = task == null;
				num3 = unchecked((nint)null);
				if (flag6)
				{
					throw new NullReferenceException();
				}
				TaskAwaiter<HttpResponseMessage> awaiter = task.GetAwaiter();
				TaskAwaiter<HttpResponseMessage> taskAwaiter = default(TaskAwaiter<HttpResponseMessage>);
				if (!taskAwaiter.IsCompleted)
				{
					reference = ref *(_003CSubmitScore_003Ed__19*)null;
					AsyncTaskMethodBuilder<PostLeaderboardScoreResponse> asyncTaskMethodBuilder = (AsyncTaskMethodBuilder<PostLeaderboardScoreResponse>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref reference, 8));
					((AsyncTaskMethodBuilder<PostLeaderboardScoreResponse>*)asyncTaskMethodBuilder)->AwaitUnsafeOnCompleted(ref taskAwaiter, ref reference);
					if (0 < 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v74 @ stack_8_v2 (<SubmitScore>d__19&)+30]");
						if ((nint)0 != 0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181249C70");
						}
					}
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091FAF0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v74 @ stack_8_v2 (<SubmitScore>d__19&)+38]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v74 @ stack_8_v2 (<SubmitScore>d__19&)+38]");
			bool flag7 = (nint)0 == 0;
			num3 = 0;
			HttpRequestMessage httpRequestMessage3 = default(HttpRequestMessage);
			httpRequestMessage = httpRequestMessage3;
			if (!flag7)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v248 @ rcx_v46+38]");
				bool flag8 = (nint)0 == 0;
				num3 = 0;
				httpRequestMessage = httpRequestMessage3;
				if (!flag8)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v248 @ rcx_v46+38]");
					Task<string> task2 = ((HttpContent)0).ReadAsStringAsync();
					bool flag9 = task2 == null;
					num3 = 0;
					httpRequestMessage = null;
					if (!flag9)
					{
						TaskAwaiter<string> awaiter2 = task2.GetAwaiter();
						TaskAwaiter<string> taskAwaiter2 = default(TaskAwaiter<string>);
						if (taskAwaiter2.IsCompleted)
						{
							goto IL_0473;
						}
						ref _003CSubmitScore_003Ed__19 reference = ref *(_003CSubmitScore_003Ed__19*)1;
						AsyncTaskMethodBuilder<PostLeaderboardScoreResponse> asyncTaskMethodBuilder2 = (AsyncTaskMethodBuilder<PostLeaderboardScoreResponse>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref reference, 8));
						((AsyncTaskMethodBuilder<PostLeaderboardScoreResponse>*)asyncTaskMethodBuilder2)->AwaitUnsafeOnCompleted(ref taskAwaiter2, ref reference);
						if (1 < 0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v74 @ stack_8_v2 (<SubmitScore>d__19&)+30]");
							if ((nint)0 != 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181249C70");
							}
						}
						return;
					}
					throw new NullReferenceException();
				}
				throw new NullReferenceException();
			}
			throw new NullReferenceException();
			IL_0473:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091FAF0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v74 @ stack_8_v2 (<SubmitScore>d__19&)+38]");
			bool flag10 = (nint)0 == 0;
			num4 = 0;
			object obj3 = default(object);
			httpRequestMessage = (HttpRequestMessage)(&obj3);
			if (!flag10)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v74 @ stack_8_v2 (<SubmitScore>d__19&)+38]");
				bool isSuccessStatusCode = ((HttpResponseMessage)0).IsSuccessStatusCode;
				object obj4 = 0;
				byte[] array2 = null;
				if (!isSuccessStatusCode)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v74 @ stack_8_v2 (<SubmitScore>d__19&)+38]");
					bool flag11 = (nint)0 == 0;
					num4 = 0;
					array2 = null;
					if (flag11)
					{
						throw new NullReferenceException();
					}
					object arg = (HttpStatusCode)httpRequestMessage3;
					string message = $"SubmitScore failed | Status Code: {arg} | {obj3}";
					Debug.LogError(message);
					string text2 = null;
					obj4 = obj3;
					array2 = null;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v74 @ stack_8_v2 (<SubmitScore>d__19&)+38]");
				bool flag12 = (nint)0 == 0;
				num4 = (nint)obj4;
				if (!flag12)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v74 @ stack_8_v2 (<SubmitScore>d__19&)+38]");
					HttpResponseMessage httpResponseMessage = ((HttpResponseMessage)0).EnsureSuccessStatusCode();
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18070B4B0");
					if ((nint)obj < 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v74 @ stack_8_v2 (<SubmitScore>d__19&)+30]");
						if ((nint)0 != 0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181249C70");
						}
					}
					ref _003CSubmitScore_003Ed__19 reference = ref *(_003CSubmitScore_003Ed__19*)4294967294L;
					_ = 0;
					AsyncTaskMethodBuilder<PostLeaderboardScoreResponse> asyncTaskMethodBuilder3 = (AsyncTaskMethodBuilder<PostLeaderboardScoreResponse>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref reference, 8));
					PostLeaderboardScoreResponse result = default(PostLeaderboardScoreResponse);
					((AsyncTaskMethodBuilder<PostLeaderboardScoreResponse>*)asyncTaskMethodBuilder3)->SetResult(result);
					return;
				}
				httpRequestMessage = (HttpRequestMessage)(object)array2;
				throw new NullReferenceException();
			}
			num3 = num4;
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
			AsyncTaskMethodBuilder<PostLeaderboardScoreResponse> asyncTaskMethodBuilder = (AsyncTaskMethodBuilder<PostLeaderboardScoreResponse>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			((AsyncTaskMethodBuilder<PostLeaderboardScoreResponse>*)asyncTaskMethodBuilder)->SetStateMachine(stateMachine);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
			this.SetStateMachine(stateMachine);
		}
	}

	public const string DeviceIDKey = "IN_DeviceID";

	public const string UserIDKey = "IN_UserID";

	private static readonly HttpClient _http;

	private static string _baseUrl;

	private static string _secret;

	private static string _deviceID;

	private static Guid _userId;

	public static RegisterResponse LatestRegisterResponse;

	private static readonly JsonSerializerSettings _jsonOptions;

	public static void Init(string baseUrl, string secretKey)
	{
		string baseUrl2 = baseUrl.TrimEnd('/');
		_baseUrl = baseUrl2;
		_secret = secretKey;
		string deviceUniqueIdentifier = SystemInfo.deviceUniqueIdentifier;
		string deviceID = PlayerPrefs.GetString("IN_DeviceID", deviceUniqueIdentifier);
		_deviceID = deviceID;
		string input = PlayerPrefs.GetString("IN_UserID", "");
		Guid userId = ((!Guid.TryParse(input, out var result)) ? Guid.Empty : result);
		_userId = userId;
		HttpRequestHeaders defaultRequestHeaders = _http.DefaultRequestHeaders;
		defaultRequestHeaders.Add("User-Agent", "IronNest-Unity");
		HttpRequestHeaders defaultRequestHeaders2 = _http.DefaultRequestHeaders;
		defaultRequestHeaders2.Add("x-secret-key", _secret);
	}

	private static HttpRequestMessage CreateRequest(HttpMethod method, string url, object body = null)
	{
		HttpRequestMessage httpRequestMessage = new HttpRequestMessage(method, url);
		if (body != null)
		{
			string content = JsonConvert.SerializeObject(body, _jsonOptions);
			Encoding uTF = Encoding.UTF8;
			StringContent stringContent = new StringContent(content, uTF, "application/json");
			if (httpRequestMessage == null)
			{
				return (HttpRequestMessage)(object)new NullReferenceException();
			}
			httpRequestMessage._003CContent_003Ek__BackingField = stringContent;
		}
		return httpRequestMessage;
	}

	public static Task<bool> DiscordLinkRequest(string code)
	{
		AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder = AsyncTaskMethodBuilder<bool>.Create();
		AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder2 = default(AsyncTaskMethodBuilder<bool>);
		_003CDiscordLinkRequest_003Ed__11 stateMachine = default(_003CDiscordLinkRequest_003Ed__11);
		asyncTaskMethodBuilder2.Start(ref stateMachine);
		return asyncTaskMethodBuilder2.Task;
	}

	public static Task<string> GenerateDiscordLinkKey()
	{
		AsyncTaskMethodBuilder<string> asyncTaskMethodBuilder = AsyncTaskMethodBuilder<string>.Create();
		AsyncTaskMethodBuilder<string> asyncTaskMethodBuilder2 = default(AsyncTaskMethodBuilder<string>);
		_003CGenerateDiscordLinkKey_003Ed__12 stateMachine = default(_003CGenerateDiscordLinkKey_003Ed__12);
		asyncTaskMethodBuilder2.Start(ref stateMachine);
		return asyncTaskMethodBuilder2.Task;
	}

	public static Task<RegisterResponse> Register(RegisterRequest request)
	{
		AsyncTaskMethodBuilder<RegisterResponse> asyncTaskMethodBuilder = AsyncTaskMethodBuilder<RegisterResponse>.Create();
		AsyncTaskMethodBuilder<RegisterResponse> asyncTaskMethodBuilder2 = default(AsyncTaskMethodBuilder<RegisterResponse>);
		_003CRegister_003Ed__13 stateMachine = default(_003CRegister_003Ed__13);
		asyncTaskMethodBuilder2.Start(ref stateMachine);
		return asyncTaskMethodBuilder2.Task;
	}

	public static Task<bool> PushOperationState(PostOperationStateRequest request)
	{
		AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder = AsyncTaskMethodBuilder<bool>.Create();
		AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder2 = default(AsyncTaskMethodBuilder<bool>);
		_003CPushOperationState_003Ed__14 stateMachine = default(_003CPushOperationState_003Ed__14);
		asyncTaskMethodBuilder2.Start(ref stateMachine);
		return asyncTaskMethodBuilder2.Task;
	}

	public static Task<ClientCombinedLeaderboardResponse> GetClientCombined()
	{
		AsyncTaskMethodBuilder<ClientCombinedLeaderboardResponse> asyncTaskMethodBuilder = AsyncTaskMethodBuilder<ClientCombinedLeaderboardResponse>.Create();
		AsyncTaskMethodBuilder<ClientCombinedLeaderboardResponse> asyncTaskMethodBuilder2 = default(AsyncTaskMethodBuilder<ClientCombinedLeaderboardResponse>);
		_003CGetClientCombined_003Ed__15 stateMachine = default(_003CGetClientCombined_003Ed__15);
		asyncTaskMethodBuilder2.Start(ref stateMachine);
		return asyncTaskMethodBuilder2.Task;
	}

	public static Task<GetTopLeaderboardResponse> GetTop(Gamemodes gamemode, int count)
	{
		AsyncTaskMethodBuilder<GetTopLeaderboardResponse> asyncTaskMethodBuilder = AsyncTaskMethodBuilder<GetTopLeaderboardResponse>.Create();
		AsyncTaskMethodBuilder<GetTopLeaderboardResponse> asyncTaskMethodBuilder2 = default(AsyncTaskMethodBuilder<GetTopLeaderboardResponse>);
		_003CGetTop_003Ed__16 stateMachine = default(_003CGetTop_003Ed__16);
		asyncTaskMethodBuilder2.Start(ref stateMachine);
		return asyncTaskMethodBuilder2.Task;
	}

	public static Task<GetMyLeaderboardResponse> GetMine(Gamemodes gamemode)
	{
		AsyncTaskMethodBuilder<GetMyLeaderboardResponse> asyncTaskMethodBuilder = AsyncTaskMethodBuilder<GetMyLeaderboardResponse>.Create();
		AsyncTaskMethodBuilder<GetMyLeaderboardResponse> asyncTaskMethodBuilder2 = default(AsyncTaskMethodBuilder<GetMyLeaderboardResponse>);
		_003CGetMine_003Ed__17 stateMachine = default(_003CGetMine_003Ed__17);
		asyncTaskMethodBuilder2.Start(ref stateMachine);
		return asyncTaskMethodBuilder2.Task;
	}

	public static Task<GetSessionKeyResponse> GetSessionKey(Gamemodes gamemode, string performanceJson = "")
	{
		AsyncTaskMethodBuilder<GetSessionKeyResponse> asyncTaskMethodBuilder = AsyncTaskMethodBuilder<GetSessionKeyResponse>.Create();
		AsyncTaskMethodBuilder<GetSessionKeyResponse> asyncTaskMethodBuilder2 = default(AsyncTaskMethodBuilder<GetSessionKeyResponse>);
		_003CGetSessionKey_003Ed__18 stateMachine = default(_003CGetSessionKey_003Ed__18);
		asyncTaskMethodBuilder2.Start(ref stateMachine);
		return asyncTaskMethodBuilder2.Task;
	}

	public static Task<PostLeaderboardScoreResponse> SubmitScore(PostLeaderboardScoreRequest request, byte[] zipBytes)
	{
		AsyncTaskMethodBuilder<PostLeaderboardScoreResponse> asyncTaskMethodBuilder = AsyncTaskMethodBuilder<PostLeaderboardScoreResponse>.Create();
		AsyncTaskMethodBuilder<PostLeaderboardScoreResponse> asyncTaskMethodBuilder2 = default(AsyncTaskMethodBuilder<PostLeaderboardScoreResponse>);
		_003CSubmitScore_003Ed__19 stateMachine = default(_003CSubmitScore_003Ed__19);
		asyncTaskMethodBuilder2.Start(ref stateMachine);
		return asyncTaskMethodBuilder2.Task;
	}

	static LeaderboardClient()
	{
		HttpClient http = new HttpClient();
		_http = http;
		_jsonOptions = new JsonSerializerSettings
		{
			NullValueHandling = NullValueHandling.Ignore
		};
	}
}
