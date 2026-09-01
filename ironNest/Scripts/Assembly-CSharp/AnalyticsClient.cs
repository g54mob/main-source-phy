using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

public static class AnalyticsClient
{
	[StructLayout((LayoutKind)3)]
	[CompilerGenerated]
	private struct _003CBootEvent_Immediate_003Ed__22 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder<bool> _003C_003Et__builder;

		public AnalyticsEventRequest_Boot request;

		private HttpResponseMessage _003Cres_003E5__2;

		private TaskAwaiter<HttpResponseMessage> _003C_003Eu__1;

		private object _003C_003E7__wrap2;

		private TaskAwaiter<string> _003C_003Eu__2;

		private void MoveNext()
		{
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			this.MoveNext();
		}

		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
			this.SetStateMachine(stateMachine);
		}
	}

	[StructLayout((LayoutKind)3)]
	[CompilerGenerated]
	private struct _003CFlush_003Ed__27 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder<bool> _003C_003Et__builder;

		private TaskAwaiter<bool> _003C_003Eu__1;

		private void MoveNext()
		{
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			this.MoveNext();
		}

		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
			this.SetStateMachine(stateMachine);
		}
	}

	[StructLayout((LayoutKind)3)]
	[CompilerGenerated]
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

		private void MoveNext()
		{
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			this.MoveNext();
		}

		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
			this.SetStateMachine(stateMachine);
		}
	}

	[StructLayout((LayoutKind)3)]
	[CompilerGenerated]
	private struct _003CGenericEvent_Immediate_003Ed__24 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder<bool> _003C_003Et__builder;

		public AnalyticsEventRequest_Generic request;

		private HttpResponseMessage _003Cres_003E5__2;

		private TaskAwaiter<HttpResponseMessage> _003C_003Eu__1;

		private object _003C_003E7__wrap2;

		private TaskAwaiter<string> _003C_003Eu__2;

		private void MoveNext()
		{
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			this.MoveNext();
		}

		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
			this.SetStateMachine(stateMachine);
		}
	}

	[StructLayout((LayoutKind)3)]
	[CompilerGenerated]
	private struct _003CMissionEvent_Immediate_003Ed__23 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder<bool> _003C_003Et__builder;

		public AnalyticsEventRequest_Mission request;

		private HttpResponseMessage _003Cres_003E5__2;

		private TaskAwaiter<HttpResponseMessage> _003C_003Eu__1;

		private object _003C_003E7__wrap2;

		private TaskAwaiter<string> _003C_003Eu__2;

		private void MoveNext()
		{
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			this.MoveNext();
		}

		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
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
		return false;
	}

	[RuntimeInitializeOnLoadMethod]
	private static void RunOnStart()
	{
	}

	public static void Init(string baseUrl, string analyticsKey)
	{
	}

	private static HttpRequestMessage CreateRequest(HttpMethod method, string url, object body = null)
	{
		return null;
	}

	public static void BootEvent(AnalyticsEventRequest_Boot request)
	{
	}

	public static void MissionEvent(AnalyticsEventRequest_Mission request)
	{
	}

	public static void GenericEvent(AnalyticsEventRequest_Generic request)
	{
	}

	[AsyncStateMachine(typeof(_003CBootEvent_Immediate_003Ed__22))]
	public static Task<bool> BootEvent_Immediate(AnalyticsEventRequest_Boot request)
	{
		return null;
	}

	[AsyncStateMachine(typeof(_003CMissionEvent_Immediate_003Ed__23))]
	public static Task<bool> MissionEvent_Immediate(AnalyticsEventRequest_Mission request)
	{
		return null;
	}

	[AsyncStateMachine(typeof(_003CGenericEvent_Immediate_003Ed__24))]
	public static Task<bool> GenericEvent_Immediate(AnalyticsEventRequest_Generic request)
	{
		return null;
	}

	private static void StartBatchLoop()
	{
	}

	private static void TryFlushIfFull()
	{
	}

	[AsyncStateMachine(typeof(_003CFlush_003Ed__27))]
	public static Task<bool> Flush()
	{
		return null;
	}

	[AsyncStateMachine(typeof(_003CFlush_003Ed__28))]
	private static Task<bool> Flush(CancellationToken token)
	{
		return null;
	}

	private static void StopBatchLoop(bool clearQueue)
	{
	}
}
