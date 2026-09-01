using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionLockBroker : MonoBehaviour
{
	[Serializable]
	public struct LockRequest
	{
		public bool FreezePlayerController;

		public bool UseFreeMouse;

		public bool UseUIActionMap;

		public bool HideVirtualCursorAndBlockWorld;

		public string DebugLabel;
	}

	public struct LockHandle : IEquatable<LockHandle>
	{
		public readonly int Id;

		public readonly int BrokerInstanceId;

		public bool IsValid
		{
			get
			{
				if (Id == 0)
				{
					return false;
				}
				bool flag = BrokerInstanceId < 0;
				bool flag2 = BrokerInstanceId == 0;
				bool flag3 = !flag;
				bool flag4 = !flag2;
				return flag4 & flag3;
			}
		}

		public LockHandle(int id, int brokerInstanceId)
		{
			Id = id;
			BrokerInstanceId = brokerInstanceId;
		}

		public bool Equals(LockHandle other)
		{
			//IL_003c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0041: Expected O, but got Unknown
			if (Id != (nint)other)
			{
				return false;
			}
			object obj = (object)other >> 32;
			object obj2 = BrokerInstanceId - obj;
			return obj2 == null;
		}

		public override bool Equals(object obj)
		{
			//IL_0013: Expected I, but got O
			//IL_0057: Expected I, but got O
			//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ce: Expected O, but got Unknown
			if (obj != null)
			{
				nint num = (nint)typeof(LockHandle);
				bool flag = (object)obj.GetType() != typeof(LockHandle);
				object obj2 = null;
				if (!flag)
				{
					obj2 = obj;
				}
				if (obj2 != null)
				{
					nint num2 = (nint)obj;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v136 @ rcx_v3 (Il2CppClass<System.Object>)+40]");
					nint num3 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ rdx_v2 (Il2CppClass<InteractionLockBroker+LockHandle>)+40]");
					if (num3 != 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
						bool result = default(bool);
						return result;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_object_unbox\"");
					object obj3 = default(object);
					if (Id == (nint)obj3)
					{
						object obj4 = obj3 >> 32;
						object obj5 = BrokerInstanceId - obj4;
						return obj5 == null;
					}
				}
			}
			return false;
		}

		public override int GetHashCode()
		{
			//IL_0010: Expected O, but got I4
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Expected I4, but got Unknown
			object obj = Id * 397;
			return obj ^ BrokerInstanceId;
		}

		public static bool operator ==(LockHandle a, LockHandle b)
		{
			if ((object)a != (object)b)
			{
				return false;
			}
			object obj = (object)a >> 32;
			object obj2 = (object)b >> 32;
			object obj3 = obj - obj2;
			return obj3 == null;
		}

		public static bool operator !=(LockHandle a, LockHandle b)
		{
			if ((object)a != (object)b)
			{
				return true;
			}
			object obj = (object)a >> 32;
			object obj2 = (object)b >> 32;
			object obj3 = obj - obj2;
			bool flag = obj3 == null;
			return !flag;
		}

		public override string ToString()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AC44]");
			if ((nint)0 == 0)
			{
				_ = 1;
			}
			if (Id != 0 && BrokerInstanceId > 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg = default(object);
				object arg2 = default(object);
				return $"LockHandle(Id={arg}, Broker={arg2})";
			}
			return "LockHandle(INVALID)";
		}
	}

	private string brokerTag = "LockBroker";

	private bool warnOnMultipleBrokers = true;

	private static readonly List<InteractionLockBroker> s_foundBrokers;

	private FirstPersonController playerController;

	private DynamicCursorManager dynamicCursorManager;

	private PlayerInput playerInput;

	private GameObject playerVirtualCamera;

	private bool enableTagAutoResolve = true;

	private bool retryResolveIfMissing;

	private float retryResolveIntervalSeconds = 0.5f;

	private string playerControllerTag = "Player";

	private string cursorManagerTag = "CursorManager";

	private string playerInputTag = "PlayerInput";

	private string playerVirtualCameraTag = "CMCam";

	private bool enableActionMapSwitching = true;

	private string playerActionMapName = "Player";

	private string uiActionMapName = "UI";

	private bool logStateChanges = true;

	private bool warnIfActionMapRequestedButMissingPlayerInput;

	private bool forceReapplyOnEveryChange;

	private readonly Dictionary<int, LockRequest> _requests = new Dictionary<int, LockRequest>();

	private int _nextId = 1;

	private bool _resolvedFreeze;

	private bool _resolvedUseFreeMouse;

	private bool _resolvedUseUIMap;

	private bool _resolvedHideVirtualCursorAndBlockWorld;

	private float _nextResolveAttemptTime;

	private Action m_OnRequestsChanged;

	private int BrokerInstanceId => GetInstanceID();

	public bool IsPlayerVirtualCameraActive
	{
		get
		{
			//IL_0069: Expected I4, but got O
			bool flag = playerVirtualCamera != null;
			if (!flag)
			{
				return flag;
			}
			if ((object)playerVirtualCamera != null)
			{
				return playerVirtualCamera.activeInHierarchy;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	public int ActiveRequestCount
	{
		get
		{
			//IL_0027: Expected I4, but got O
			if (_requests != null)
			{
				return _requests.Count;
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
	}

	public event Action OnRequestsChanged
	{
		add
		{
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Expected O, but got Unknown
			object obj = this + 176;
			Delegate obj2 = this.m_OnRequestsChanged;
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
			object obj = this + 176;
			Delegate obj2 = this.m_OnRequestsChanged;
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

	public unsafe static bool TryGet(out InteractionLockBroker broker, string tag = "LockBroker")
	{
		InteractionLockBroker interactionLockBroker = FindOrNull(tag);
		ref InteractionLockBroker reference = ref *(InteractionLockBroker*)interactionLockBroker;
		return broker != null;
	}

	public static InteractionLockBroker FindOrNull(string tag = "LockBroker")
	{
		if (!string.IsNullOrWhiteSpace(tag))
		{
			GameObject gameObject = GameObject.FindGameObjectWithTag(tag);
			if (gameObject != null)
			{
				if ((object)gameObject != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
					InteractionLockBroker result = default(InteractionLockBroker);
					return result;
				}
				return (InteractionLockBroker)(object)new NullReferenceException();
			}
		}
		return null;
	}

	private void Awake()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AC35]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		if (warnOnMultipleBrokers)
		{
			WarnIfMultipleBrokersExist();
		}
		ResolveReferencesIfNeeded(force: true);
		RecomputeAndApply("Awake");
	}

	private void Start()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AC36]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		ResolveReferencesIfNeeded(force: false);
		RecomputeAndApply("Start");
	}

	private void Update()
	{
		if (!enableTagAutoResolve || !retryResolveIfMissing)
		{
			return;
		}
		float unscaledTime = Time.unscaledTime;
		if (_nextResolveAttemptTime > unscaledTime)
		{
			return;
		}
		if (playerController != null)
		{
			bool flag = dynamicCursorManager == null;
			if (!flag && (enableActionMapSwitching == flag || playerInput != null) && !(playerVirtualCamera == null))
			{
				return;
			}
		}
		float unscaledTime2 = Time.unscaledTime;
		bool flag2 = !(0.05f < retryResolveIntervalSeconds);
		float num = 0.05f;
		if (!flag2)
		{
			num = retryResolveIntervalSeconds;
		}
		float nextResolveAttemptTime = num + unscaledTime2;
		_nextResolveAttemptTime = nextResolveAttemptTime;
		ResolveReferencesIfNeeded(force: false);
		int count = _requests.Count;
		if (count > 0)
		{
			RecomputeAndApply("AutoResolve(Update)");
		}
	}

	public unsafe LockHandle Acquire(LockRequest request)
	{
		//IL_0018: Expected O, but got Ref
		//IL_003b: Expected O, but got Ref
		//IL_04c8: Expected O, but got I4
		//IL_00d8: Expected I, but got O
		//IL_0419: Expected O, but got I
		//IL_0419: Expected I4, but got O
		//IL_0159: Expected I, but got O
		//IL_01a3: Expected I, but got O
		//IL_0452: Expected O, but got I
		//IL_0200: Expected I, but got O
		//IL_024a: Expected I, but got O
		//IL_047f: Expected O, but got I
		//IL_02a7: Expected I, but got O
		//IL_02f1: Expected I, but got O
		//IL_0367: Expected O, but got I4
		//IL_036c: Expected I, but got O
		//IL_04ac: Expected O, but got I
		int nextId = _nextId + 1;
		_nextId = nextId;
		int num = default(int);
		object obj = default(object);
		_requests.set_Item((int)(&num), (LockRequest)(&obj));
		bool flag = !logStateChanges;
		num = _nextId;
		LockRequest lockRequest = (LockRequest)(&obj);
		nint num2 = 0;
		if (!flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg = default(object);
			LockRequest lockRequest2 = default(LockRequest);
			string text = $"[InteractionLockBroker] Acquire id={arg} label='{lockRequest2.DebugLabel}' ";
			object[] array = new object[4];
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			if (array == null)
			{
				return (LockHandle)new NullReferenceException();
			}
			Dictionary<int, LockRequest> dictionary = default(Dictionary<int, LockRequest>);
			if (dictionary != null)
			{
				nint num3 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v366 @ rdx_v43 (Il2CppClass<System.Object[]>)+40]");
				dictionary.set_Item(0, (LockRequest)lockRequest2.DebugLabel);
				object obj2 = default(object);
				if (obj2 == null)
				{
					nint num4 = default(nint);
					_requests.set_Item((int)lockRequest2, (LockRequest)num4);
					Dictionary<int, LockRequest> dictionary2 = default(Dictionary<int, LockRequest>);
					throw dictionary2;
				}
			}
			array[0] = dictionary;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			Dictionary<int, LockRequest> dictionary3 = default(Dictionary<int, LockRequest>);
			if (dictionary3 != null)
			{
				nint num5 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v577 @ rdx_v41 (Il2CppClass<System.Object[]>)+40]");
				int key = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v577 @ rdx_v41 (Il2CppClass<System.Object[]>)+40]");
				dictionary3.set_Item(0, (LockRequest)lockRequest2.DebugLabel);
				object obj3 = default(object);
				bool flag2 = obj3 == null;
				nint num4 = (nint)lockRequest2.DebugLabel;
				Dictionary<int, LockRequest> dictionary4 = dictionary3;
				if (flag2)
				{
					dictionary4.set_Item(key, (LockRequest)num4);
					Dictionary<int, LockRequest> dictionary5 = default(Dictionary<int, LockRequest>);
					throw dictionary5;
				}
			}
			array[1] = dictionary3;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			Dictionary<int, LockRequest> dictionary6 = default(Dictionary<int, LockRequest>);
			if (dictionary6 != null)
			{
				nint num6 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v606 @ rdx_v39 (Il2CppClass<System.Object[]>)+40]");
				int key2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v606 @ rdx_v39 (Il2CppClass<System.Object[]>)+40]");
				dictionary6.set_Item(0, (LockRequest)lockRequest2.DebugLabel);
				object obj4 = default(object);
				bool flag3 = obj4 == null;
				nint num4 = (nint)lockRequest2.DebugLabel;
				Dictionary<int, LockRequest> dictionary7 = dictionary6;
				if (flag3)
				{
					dictionary7.set_Item(key2, (LockRequest)num4);
					Dictionary<int, LockRequest> dictionary8 = default(Dictionary<int, LockRequest>);
					throw dictionary8;
				}
			}
			array[2] = dictionary6;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			Dictionary<int, LockRequest> dictionary9 = default(Dictionary<int, LockRequest>);
			if (dictionary9 != null)
			{
				nint num7 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v635 @ rdx_v37 (Il2CppClass<System.Object[]>)+40]");
				int key3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v635 @ rdx_v37 (Il2CppClass<System.Object[]>)+40]");
				dictionary9.set_Item(0, (LockRequest)lockRequest2.DebugLabel);
				object obj5 = default(object);
				bool flag4 = obj5 == null;
				nint num4 = (nint)lockRequest2.DebugLabel;
				Dictionary<int, LockRequest> dictionary10 = dictionary9;
				if (flag4)
				{
					dictionary10.set_Item(key3, (LockRequest)num4);
					object obj6 = default(object);
					throw obj6;
				}
			}
			array[3] = dictionary9;
			string text2 = string.Format("freeze={0} freeMouse={1} uiMap={2} hideCursorBlockWorld={3}", array);
			string message = text + text2;
			Debug.Log(message, this);
			num = (lockRequest2.FreezePlayerController ? 1 : 0);
			lockRequest = (LockRequest)0;
			num2 = unchecked((nint)null);
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		object arg2 = default(object);
		string reason = $"Acquire({arg2})";
		RecomputeAndApply(reason);
		Action onRequestsChanged = this.m_OnRequestsChanged;
		if (this.m_OnRequestsChanged != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v208.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
		}
		int instanceID = GetInstanceID();
		return (LockHandle)_nextId;
	}

	public unsafe bool Release(LockHandle handle)
	{
		//IL_0181: Expected I4, but got O
		//IL_00f8: Expected I, but got O
		object obj = default(object);
		if ((object)handle != null && obj != null)
		{
			int instanceID = GetInstanceID();
			if ((nint)obj == instanceID)
			{
				if (_requests == null)
				{
					NullReferenceException ex = new NullReferenceException();
					return (byte)(int)ex != 0;
				}
				LockHandle lockHandle = default(LockHandle);
				if (_requests.Remove((int)(&lockHandle)))
				{
					bool flag = !logStateChanges;
					nint num = 0;
					lockHandle = handle;
					if (!flag)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
						object arg = default(object);
						string message = $"[InteractionLockBroker] Release id={arg}";
						Debug.Log(message, this);
						num = unchecked((nint)null);
						lockHandle = handle;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					object arg2 = default(object);
					string reason = $"Release({arg2})";
					RecomputeAndApply(reason);
					Action onRequestsChanged = this.m_OnRequestsChanged;
					if (this.m_OnRequestsChanged != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v224.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
					}
					return true;
				}
			}
		}
		return false;
	}

	public void ReleaseAll(string reason = "ReleaseAll")
	{
		if (_requests.Count != 0)
		{
			_requests.Clear();
			if (logStateChanges)
			{
				string message = "[InteractionLockBroker] ReleaseAll reason='" + reason + "'";
				Debug.Log(message, this);
			}
			RecomputeAndApply(reason, forceApply: true);
			Action onRequestsChanged = this.m_OnRequestsChanged;
			if (this.m_OnRequestsChanged != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v104.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
			}
		}
	}

	public void ForceRefresh(string reason = "ForceRefresh")
	{
		ResolveReferencesIfNeeded(force: false);
		RecomputeAndApply(reason, forceApply: true);
	}

	public InteractionLockBroker FindSelfByConfiguredTagOrNull()
	{
		return FindOrNull(brokerTag);
	}

	public bool IsMostRecentLock([In] ref LockHandle handle)
	{
		//IL_0021: Expected O, but got I4
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Expected O, but got Unknown
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18082BED0");
		int num = 0;
		object obj = 0;
		Dictionary<int, LockRequest>.Enumerator enumerator = default(Dictionary<int, LockRequest>.Enumerator);
		int val = default(int);
		while (enumerator.MoveNext())
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803710D0");
			int num2 = Math.Max(num, val);
			num = num2;
		}
		enumerator.Dispose();
		object obj2 = handle - num;
		return obj2 == null;
	}

	private void RecomputeAndApply(string reason, bool forceApply = false)
	{
		//IL_028e: Expected O, but got I4
		//IL_0178: Expected O, but got I4
		//IL_0836: Expected O, but got I4
		//IL_083e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0843: Expected O, but got Unknown
		//IL_084b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0850: Expected O, but got Unknown
		//IL_05fb: Expected O, but got I4
		//IL_03c2: Expected O, but got I4
		//IL_03e8: Expected O, but got I4
		//IL_0353: Expected O, but got I4
		//IL_0622: Expected O, but got I4
		//IL_0595: Expected O, but got I4
		//IL_05dc: Expected O, but got I4
		//IL_047a: Expected O, but got I4
		//IL_04c1: Expected O, but got I4
		ResolveReferencesIfNeeded(force: false);
		FirstPersonController requests = (FirstPersonController)(object)_requests;
		if (_requests != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18082BED0");
			object obj2 = default(object);
			object obj = obj2;
			object obj4 = default(object);
			object obj3 = obj4;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			Dictionary<int, LockRequest>.Enumerator enumerator = default(Dictionary<int, LockRequest>.Enumerator);
			object obj5 = default(object);
			object obj8 = default(object);
			object obj9 = default(object);
			while (true)
			{
				if (enumerator.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803711A0");
					object obj6;
					if (obj5 == null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm0,1\"");
						obj3 = obj5;
						obj6 = obj5;
					}
					else
					{
						object obj7 = obj5 >> 8;
						obj3 = obj8;
						flag5 = true;
						obj6 = obj7;
					}
					if (obj6 != null)
					{
						flag4 = true;
					}
					if (obj8 != null)
					{
						flag3 = true;
					}
					if (obj9 != null)
					{
						flag = true;
					}
					object obj10 = flag & flag3;
					object obj11 = obj10 & flag4;
					object obj12 = flag5 & obj11;
					bool flag6 = obj12 == null;
					obj = obj2;
					flag2 = flag3;
					if (!flag6)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
						obj = obj2;
						break;
					}
					continue;
				}
				enumerator.Dispose();
				break;
			}
			if (flag5 == _resolvedFreeze && flag4 == _resolvedUseFreeMouse && flag3 == _resolvedUseUIMap)
			{
				object obj13 = (flag ? 1 : 0) - (_resolvedHideVirtualCursorAndBlockWorld ? 1 : 0);
				bool flag7 = obj13 == null;
				bool flag8 = !flag7;
				if (!flag8 && forceReapplyOnEveryChange == flag8 && !forceApply)
				{
					return;
				}
			}
			_resolvedFreeze = flag5;
			_resolvedUseFreeMouse = flag4;
			_resolvedUseUIMap = flag3;
			_resolvedHideVirtualCursorAndBlockWorld = flag;
			if (playerController != null)
			{
				if ((object)playerController == null)
				{
					goto IL_0704;
				}
				playerController.SetFrozen(_resolvedFreeze);
			}
			bool flag9 = dynamicCursorManager != null;
			bool flag10 = !flag9;
			object obj14 = 0;
			if (flag10)
			{
				goto IL_0358;
			}
			if ((object)dynamicCursorManager != null)
			{
				dynamicCursorManager.SetSuppressedByLockBroker(_resolvedHideVirtualCursorAndBlockWorld);
				if ((object)dynamicCursorManager != null)
				{
					bool flag11 = !_resolvedUseFreeMouse;
					bool mode = !flag11;
					dynamicCursorManager.SwitchToPresentationMode(mode ? DynamicCursorManager.PresentationMode.FreeMouse : DynamicCursorManager.PresentationMode.FPSLocked);
					if ((object)dynamicCursorManager != null)
					{
						dynamicCursorManager.ForceRefresh(forceBroadcast: true);
						obj14 = 0;
						goto IL_0358;
					}
				}
			}
		}
		goto IL_0704;
		IL_0358:
		if (enableActionMapSwitching)
		{
			if (!_resolvedUseUIMap)
			{
				bool flag12 = this.playerInput != null;
				bool flag13 = !flag12;
				object obj14 = 0;
				if (!flag13)
				{
					bool flag14 = string.IsNullOrEmpty(playerActionMapName);
					obj14 = 0;
					if (!flag14)
					{
						PlayerInput playerInput = this.playerInput;
						if ((object)this.playerInput != null)
						{
							if (playerInput.m_CurrentActionMap != null)
							{
								InputActionMap currentActionMap = playerInput.m_CurrentActionMap;
								bool flag15 = currentActionMap.m_Name != playerActionMapName;
								bool flag16 = !flag15;
								obj14 = 0;
								if (flag16)
								{
									goto IL_0627;
								}
							}
							if ((object)this.playerInput != null)
							{
								this.playerInput.SwitchCurrentActionMap(playerActionMapName);
								obj14 = 0;
								goto IL_0627;
							}
						}
						goto IL_0704;
					}
				}
			}
			else
			{
				object obj14;
				if (this.playerInput != null && !string.IsNullOrEmpty(uiActionMapName))
				{
					PlayerInput playerInput2 = this.playerInput;
					if ((object)this.playerInput != null)
					{
						if (playerInput2.m_CurrentActionMap != null)
						{
							InputActionMap currentActionMap2 = playerInput2.m_CurrentActionMap;
							bool flag17 = currentActionMap2.m_Name != uiActionMapName;
							bool flag18 = !flag17;
							obj14 = 0;
							if (flag18)
							{
								goto IL_0627;
							}
						}
						if ((object)this.playerInput != null)
						{
							this.playerInput.SwitchCurrentActionMap(uiActionMapName);
							obj14 = 0;
							goto IL_0627;
						}
					}
					goto IL_0704;
				}
				bool flag19 = !warnIfActionMapRequestedButMissingPlayerInput;
				obj14 = 0;
				if (!flag19)
				{
					Debug.LogWarning("[InteractionLockBroker] A request asked for UI action map, but PlayerInput is missing or uiActionMapName is empty.", this);
					obj14 = 0;
				}
			}
		}
		goto IL_0627;
		IL_0704:
		throw new NullReferenceException();
		IL_0627:
		if (logStateChanges)
		{
			int count = _requests.Count;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg = default(object);
			string text = $"[InteractionLockBroker] Apply ({reason}) requests={arg} ";
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			bool flag20 = _resolvedUseFreeMouse;
			object arg2 = "FreeMouse";
			if (!flag20)
			{
				arg2 = "FPSLocked";
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg3 = default(object);
			object arg4 = default(object);
			string text2 = $"freeze={arg3} mode={arg2} uiMap={arg4} ";
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg5 = default(object);
			string text3 = $"hideCursorBlockWorld={arg5}";
			string message = text + text2 + text3;
			Debug.Log(message, this);
		}
	}

	private bool HasMissingReferences()
	{
		if (playerController != null)
		{
			bool flag = dynamicCursorManager == null;
			if (!flag && (enableActionMapSwitching == flag || playerInput != null))
			{
				bool flag2 = playerVirtualCamera == null;
				if (!flag2)
				{
					return flag2;
				}
			}
		}
		return true;
	}

	private void ResolveReferencesIfNeeded(bool force)
	{
		//IL_002c: Expected I, but got O
		//IL_014d: Expected I, but got O
		//IL_026e: Expected I, but got O
		//IL_03ae: Expected I, but got O
		if (!enableTagAutoResolve && !force)
		{
			return;
		}
		nint num = (nint)typeof(UnityEngine.Object);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ rcx_v3 (Il2CppClass<UnityEngine.Object>)+E4]");
		bool flag = (nint)0 == 0;
		bool flag2 = playerController == null;
		if (!flag)
		{
			GameObject gameObject = FindByTagSafe(playerControllerTag);
			if (gameObject != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D90C0");
				FirstPersonController firstPersonController = default(FirstPersonController);
				bool flag3 = (object)firstPersonController != null;
				FirstPersonController firstPersonController2 = firstPersonController;
				if (!flag3)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9300");
					FirstPersonController firstPersonController3 = default(FirstPersonController);
					firstPersonController2 = firstPersonController3;
				}
				playerController = firstPersonController2;
			}
			if (playerController == null)
			{
				FirstPersonController firstPersonController4 = UnityEngine.Object.FindObjectOfType<FirstPersonController>(includeInactive: true);
				playerController = firstPersonController4;
			}
		}
		nint num2 = (nint)typeof(UnityEngine.Object);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v243 @ rcx_v7 (Il2CppClass<UnityEngine.Object>)+E4]");
		bool flag4 = (nint)0 == 0;
		bool flag5 = this.dynamicCursorManager == null;
		if (!flag4)
		{
			GameObject gameObject2 = FindByTagSafe(cursorManagerTag);
			if (gameObject2 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D90C0");
				DynamicCursorManager dynamicCursorManager = default(DynamicCursorManager);
				bool flag6 = (object)dynamicCursorManager != null;
				DynamicCursorManager dynamicCursorManager2 = dynamicCursorManager;
				if (!flag6)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
					DynamicCursorManager dynamicCursorManager3 = default(DynamicCursorManager);
					dynamicCursorManager2 = dynamicCursorManager3;
				}
				this.dynamicCursorManager = dynamicCursorManager2;
			}
			if (this.dynamicCursorManager == null)
			{
				DynamicCursorManager dynamicCursorManager4 = UnityEngine.Object.FindObjectOfType<DynamicCursorManager>(includeInactive: true);
				this.dynamicCursorManager = dynamicCursorManager4;
			}
		}
		nint num3 = (nint)typeof(UnityEngine.Object);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v358 @ rcx_v10 (Il2CppClass<UnityEngine.Object>)+E4]");
		bool flag7 = (nint)0 == 0;
		bool flag8 = this.playerInput == null;
		if (!flag7 && enableActionMapSwitching)
		{
			GameObject gameObject3 = FindByTagSafe(playerInputTag);
			if (gameObject3 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D90C0");
				PlayerInput playerInput = default(PlayerInput);
				bool flag9 = (object)playerInput != null;
				PlayerInput playerInput2 = playerInput;
				if (!flag9)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
					PlayerInput playerInput3 = default(PlayerInput);
					playerInput2 = playerInput3;
				}
				this.playerInput = playerInput2;
			}
			if (this.playerInput == null)
			{
				PlayerInput playerInput4 = UnityEngine.Object.FindObjectOfType<PlayerInput>(includeInactive: true);
				this.playerInput = playerInput4;
			}
		}
		nint num4 = (nint)typeof(UnityEngine.Object);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v562 @ rcx_v13 (Il2CppClass<UnityEngine.Object>)+E4]");
		bool flag10 = (nint)0 == 0;
		bool flag11 = playerVirtualCamera == null;
		if (!flag10)
		{
			GameObject gameObject4 = FindByTagSafe(playerVirtualCameraTag);
			playerVirtualCamera = gameObject4;
		}
	}

	private static GameObject FindByTagSafe(string tag)
	{
		if (!string.IsNullOrWhiteSpace(tag))
		{
			return GameObject.FindGameObjectWithTag(tag);
		}
		return null;
	}

	private void WarnIfMultipleBrokersExist()
	{
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Expected O, but got Unknown
		//IL_010a: Expected O, but got I4
		//IL_0113: Expected O, but got I4
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Expected O, but got Unknown
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Expected O, but got Unknown
		List<InteractionLockBroker> list = s_foundBrokers;
		int version = list._version + 1;
		list._version = version;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
		object obj = default(object);
		if (obj == null)
		{
			list._size = 0;
		}
		else
		{
			int size = list._size;
			list._size = 0;
			if (list._size > 0)
			{
				Array.Clear(list._items, 0, list._size);
			}
		}
		InteractionLockBroker[] array = UnityEngine.Object.FindObjectsOfType<InteractionLockBroker>(includeInactive: true);
		if (array != null)
		{
			object obj2 = array + 32;
			object obj3 = 0;
			object obj4 = 0;
			while ((nint)obj4 < array.Length)
			{
				s_foundBrokers.Add((InteractionLockBroker)obj2);
				obj3++;
				obj2 += 8;
				int size = 0;
				obj4 = obj3;
			}
			List<InteractionLockBroker> list2 = s_foundBrokers;
			if (list2._size > 1)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg = default(object);
				string text = $"[InteractionLockBroker] Multiple brokers detected in loaded scenes: {arg}. ";
				string message = text + "This can cause confusing ownership. Ensure only one broker exists (tag='" + brokerTag + "').";
				Debug.LogWarning(message, this);
			}
		}
	}

	static InteractionLockBroker()
	{
		List<InteractionLockBroker> list = new List<InteractionLockBroker>(4);
		s_foundBrokers = list;
	}
}
