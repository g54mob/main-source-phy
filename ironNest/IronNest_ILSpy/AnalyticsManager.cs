using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Cpp2ILInjected;
using Heathen.SteamworksIntegration;
using Newtonsoft.Json;
using UnityEngine;

public class AnalyticsManager : MonoBehaviour
{
	[StructLayout((LayoutKind)3)]
	private struct _003CAnalytics_Generic_003Ed__7 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncVoidMethodBuilder _003C_003Et__builder;

		public string eventType;

		public double value;

		public Dictionary<string, object> payload;

		private unsafe void MoveNext()
		{
			//IL_010d: Expected I4, but got I8
			//IL_0118: Expected O, but got Ref
			AnalyticsEventRequest_Generic analyticsEventRequest_Generic = new AnalyticsEventRequest_Generic();
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39F5E]");
			if ((nint)0 == 0)
			{
				_ = 1;
			}
			analyticsEventRequest_Generic._003CEventType_003Ek__BackingField = "";
			analyticsEventRequest_Generic._003CDeviceId_003Ek__BackingField = "";
			analyticsEventRequest_Generic._003CPayload_003Ek__BackingField = "{}";
			analyticsEventRequest_Generic._002Ector();
			analyticsEventRequest_Generic._003CEventType_003Ek__BackingField = eventType;
			analyticsEventRequest_Generic._003CValue_003Ek__BackingField = value;
			string text = ((payload == null) ? null : JsonConvert.SerializeObject(payload));
			analyticsEventRequest_Generic._003CPayload_003Ek__BackingField = text;
			AnalyticsClient.GenericEvent(analyticsEventRequest_Generic);
			_003C_003E1__state = -2;
			AsyncVoidMethodBuilder asyncVoidMethodBuilder = (AsyncVoidMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			((AsyncVoidMethodBuilder*)asyncVoidMethodBuilder)->SetResult();
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

	[StructLayout((LayoutKind)3)]
	private struct _003CAnalytics_Mission_003Ed__6 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncVoidMethodBuilder _003C_003Et__builder;

		public string eventType;

		public string missionId;

		public double value;

		public Dictionary<string, object> payload;

		private unsafe void MoveNext()
		{
			//IL_012f: Expected I4, but got I8
			//IL_013a: Expected O, but got Ref
			AnalyticsEventRequest_Mission analyticsEventRequest_Mission = new AnalyticsEventRequest_Mission();
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39F5D]");
			if ((nint)0 == 0)
			{
				_ = 1;
			}
			analyticsEventRequest_Mission._003CEventType_003Ek__BackingField = "";
			analyticsEventRequest_Mission._003CDeviceId_003Ek__BackingField = "";
			analyticsEventRequest_Mission._003CMissionId_003Ek__BackingField = "";
			analyticsEventRequest_Mission._003CPayload_003Ek__BackingField = "{}";
			analyticsEventRequest_Mission._002Ector();
			analyticsEventRequest_Mission._003CEventType_003Ek__BackingField = eventType;
			analyticsEventRequest_Mission._003CMissionId_003Ek__BackingField = missionId;
			analyticsEventRequest_Mission._003CValue_003Ek__BackingField = value;
			string text = ((payload == null) ? null : JsonConvert.SerializeObject(payload));
			analyticsEventRequest_Mission._003CPayload_003Ek__BackingField = text;
			AnalyticsClient.MissionEvent(analyticsEventRequest_Mission);
			_003C_003E1__state = -2;
			AsyncVoidMethodBuilder asyncVoidMethodBuilder = (AsyncVoidMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			((AsyncVoidMethodBuilder*)asyncVoidMethodBuilder)->SetResult();
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

	[StructLayout((LayoutKind)3)]
	private struct _003CStart_003Ed__4 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncVoidMethodBuilder _003C_003Et__builder;

		public AnalyticsManager _003C_003E4__this;

		private TaskAwaiter _003C_003Eu__1;

		private unsafe void MoveNext()
		{
			//IL_0010: Expected O, but got I4
			//IL_001f: Expected I4, but got I8
			//IL_00d4: Expected I4, but got I8
			//IL_00df: Expected O, but got Ref
			//IL_010e: Expected O, but got Ref
			if (_003C_003E1__state == 0)
			{
				_003C_003Eu__1 = (TaskAwaiter)0;
				_003C_003E1__state = -1;
				TaskAwaiter taskAwaiter = _003C_003Eu__1;
			}
			else
			{
				Instance = _003C_003E4__this;
				GameObject gameObject = _003C_003E4__this.gameObject;
				Object.DontDestroyOnLoad(gameObject);
				Task task = Task.Delay(5000);
				TaskAwaiter awaiter = task.GetAwaiter();
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807D2870");
				object obj = default(object);
				if (obj == null)
				{
					_003C_003E1__state = 0;
					TaskAwaiter taskAwaiter = default(TaskAwaiter);
					_003C_003Eu__1 = taskAwaiter;
					AsyncVoidMethodBuilder asyncVoidMethodBuilder = (AsyncVoidMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
					((AsyncVoidMethodBuilder*)asyncVoidMethodBuilder)->AwaitUnsafeOnCompleted(ref taskAwaiter, ref this);
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180CF3BB0");
			Analytics_Boot("GameStarted");
			_003C_003E1__state = -2;
			AsyncVoidMethodBuilder asyncVoidMethodBuilder2 = (AsyncVoidMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			((AsyncVoidMethodBuilder*)asyncVoidMethodBuilder2)->SetResult();
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

	public static AnalyticsManager Instance;

	public string APIEndpoint;

	public string SecretKey;

	private void Awake()
	{
		if (!(Instance != null))
		{
			AnalyticsClient.Init(APIEndpoint, SecretKey);
			return;
		}
		GameObject obj = base.gameObject;
		Object.Destroy(obj);
	}

	private void Start()
	{
		AsyncVoidMethodBuilder asyncVoidMethodBuilder = AsyncVoidMethodBuilder.Create();
		AsyncVoidMethodBuilder asyncVoidMethodBuilder2 = default(AsyncVoidMethodBuilder);
		_003CStart_003Ed__4 stateMachine = default(_003CStart_003Ed__4);
		asyncVoidMethodBuilder2.Start(ref stateMachine);
	}

	public unsafe static void Analytics_Boot(string eventType)
	{
		//IL_0090: Expected O, but got I4
		//IL_0052: Expected O, but got I4
		string deviceUniqueIdentifier = SystemInfo.deviceUniqueIdentifier;
		UserData me = UserData.Me;
		bool flag = (object)me == null;
		long? num = (long?)(object)0;
		if (!flag)
		{
			UserData userData = default(UserData);
			long? num2 = (nint)(&userData);
			num = num2;
		}
		AnalyticsEventRequest_Boot analyticsEventRequest_Boot = new AnalyticsEventRequest_Boot();
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39F5C]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		analyticsEventRequest_Boot._003CEventType_003Ek__BackingField = "";
		analyticsEventRequest_Boot._003CDeviceId_003Ek__BackingField = "";
		analyticsEventRequest_Boot._002Ector();
		analyticsEventRequest_Boot._003CEventType_003Ek__BackingField = eventType;
		analyticsEventRequest_Boot._003CSteamId_003Ek__BackingField = num;
		analyticsEventRequest_Boot._003CGogId_003Ek__BackingField = (long?)(object)0;
		AnalyticsClient.BootEvent(analyticsEventRequest_Boot);
	}

	public static void Analytics_Mission(string eventType, string missionId, double value = 0.0, Dictionary<string, object> payload = null)
	{
		AsyncVoidMethodBuilder asyncVoidMethodBuilder = AsyncVoidMethodBuilder.Create();
		AsyncVoidMethodBuilder asyncVoidMethodBuilder2 = default(AsyncVoidMethodBuilder);
		_003CAnalytics_Mission_003Ed__6 stateMachine = default(_003CAnalytics_Mission_003Ed__6);
		asyncVoidMethodBuilder2.Start(ref stateMachine);
	}

	public static void Analytics_Generic(string eventType, double value = 0.0, Dictionary<string, object> payload = null)
	{
		AsyncVoidMethodBuilder asyncVoidMethodBuilder = AsyncVoidMethodBuilder.Create();
		AsyncVoidMethodBuilder asyncVoidMethodBuilder2 = default(AsyncVoidMethodBuilder);
		_003CAnalytics_Generic_003Ed__7 stateMachine = default(_003CAnalytics_Generic_003Ed__7);
		asyncVoidMethodBuilder2.Start(ref stateMachine);
	}

	public AnalyticsManager()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39F64]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		APIEndpoint = "";
		SecretKey = "";
		base._002Ector();
	}
}
