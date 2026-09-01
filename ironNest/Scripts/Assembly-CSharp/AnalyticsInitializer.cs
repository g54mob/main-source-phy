using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Unity.Services.Analytics;
using UnityEngine;

[DefaultExecutionOrder(-10000)]
public class AnalyticsInitializer : MonoBehaviour
{
	[StructLayout((LayoutKind)3)]
	[CompilerGenerated]
	private struct _003CInitializeAsync_003Ed__12 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public AnalyticsInitializer _003C_003E4__this;

		private TaskAwaiter _003C_003Eu__1;

		private TaskAwaiter<int> _003C_003Eu__2;

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
	private struct _003CProvideAllConsentsAndStart_003Ed__13 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public AnalyticsInitializer _003C_003E4__this;

		public bool grantConsent;

		private TaskAwaiter<int> _003C_003Eu__1;

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
	private struct _003CTryCheckForRequiredConsentsAsync_003Ed__18 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder<int> _003C_003Et__builder;

		public AnalyticsInitializer _003C_003E4__this;

		private Task _003Ctask_003E5__2;

		private TaskAwaiter _003C_003Eu__1;

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

	private static AnalyticsInitializer _instance;

	[Header("Initialization")]
	[Tooltip("If enabled, Unity Gaming Services and Analytics will initialize as early as possible when this object awakens.\nDisable if you intend to initialize manually by calling InitializeAsync() yourself.")]
	[SerializeField]
	private bool initializeOnAwake;

	[Tooltip("Environment Name for Unity Gaming Services Analytics. Allowed values: 'production' or 'development'.\nRules: lowercase, no spaces. Defaults to 'production' if left empty.\nExamples: production, development")]
	[SerializeField]
	private string environmentName;

	[Tooltip("If enabled, attempts to start data collection automatically after initialization IF no additional data privacy consents are required or if the installed Analytics package does not expose a consent API.\nIf your compliance flow requires explicit consent, disable this and call ProvideAllConsentsAndStart(true) after collecting user consent.")]
	[SerializeField]
	private bool autoStartDataCollection;

	[Header("Identity (Optional)")]
	[Tooltip("Optional: A profile name for segmenting analytics across logical users or configurations. This DOES NOT set a user ID.\nNote: InitializationOptions.SetProfile() has been removed. If you also install 'com.unity.services.authentication', you can handle profiles with AuthenticationService.SwitchProfile() yourself before sign-in.\nRules: Up to 64 characters; letters, numbers, underscore (_) and dash (-) only. No spaces.\nSafe example: player_12345\nIf left empty, this field is ignored.")]
	[SerializeField]
	private string profileName;

	[Header("Verification")]
	[Tooltip("If enabled, sends one simple 'app_launch' custom event after the first successful initialization in this session, so you can verify ingestion in the dashboard. If the installed Analytics package does not support CustomData/RecordEvent, this is skipped safely.")]
	[SerializeField]
	private bool sendTestEventOnFirstInit;

	[Header("Diagnostics")]
	[Tooltip("Enable debug logs in the Console for initialization, consent flow, and event submission. Recommended while integrating.")]
	[SerializeField]
	private bool enableDebugLogs;

	private bool _initialized;

	private bool _testEventSent;

	public bool IsInitialized => false;

	private void Awake()
	{
	}

	[AsyncStateMachine(typeof(_003CInitializeAsync_003Ed__12))]
	public Task InitializeAsync()
	{
		return null;
	}

	[AsyncStateMachine(typeof(_003CProvideAllConsentsAndStart_003Ed__13))]
	public Task ProvideAllConsentsAndStart(bool grantConsent)
	{
		return null;
	}

	public void SendCustomEvent(string eventName, IDictionary<string, object> parameters = null, bool flushImmediately = false)
	{
	}

	private void SendTestLaunchEvent()
	{
	}

	private string TryGetSessionIdSafe()
	{
		return null;
	}

	private void TryStartDataCollection()
	{
	}

	[AsyncStateMachine(typeof(_003CTryCheckForRequiredConsentsAsync_003Ed__18))]
	private Task<int> TryCheckForRequiredConsentsAsync()
	{
		return null;
	}

	[AsyncStateMachine(typeof(_003CTryProvideConsentsAsync_003Ed__19))]
	private Task<int> TryProvideConsentsAsync(bool grant)
	{
		return null;
	}

	private bool TrySendCustomEvent(string eventName, IDictionary<string, object> parameters)
	{
		return false;
	}
}
