using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CameraZoneTrigger : MonoBehaviour
{
	private sealed class _003C_003CInteractionLockBroker_OnRequestsChanged_003Eg__SubscribeDeactivateActionNextFrame_007C72_0_003Ed : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CameraZoneTrigger _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003C_003CInteractionLockBroker_OnRequestsChanged_003Eg__SubscribeDeactivateActionNextFrame_007C72_0_003Ed(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_0014: Expected I4, but got I8
			//IL_005d: Expected I4, but got I8
			//IL_0115: Expected I4, but got O
			//IL_00b2: Expected O, but got I
			object obj = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			if (_003C_003E1__state == 1)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (System.Object)+48]");
					if ((nint)0 != 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rdi_v1 (System.Object)+48]");
						InputAction action = ((InputActionReference)0).action;
						Action<InputAction.CallbackContext> value = _003C_003E4__this.OnDeactivatePerformed;
						if (action != null)
						{
							action.performed += value;
							return false;
						}
					}
				}
				NullReferenceException ex = new NullReferenceException();
				return (byte)(int)ex != 0;
			}
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}
	}

	private sealed class _003CPhaseSubscribeRetryRoutine_003Ed__61 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CameraZoneTrigger _003C_003E4__this;

		private int _003Cattempts_003E5__2;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CPhaseSubscribeRetryRoutine_003Ed__61(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_0014: Expected I4, but got I8
			//IL_0056: Expected I4, but got I8
			//IL_0226: Expected I4, but got O
			CameraZoneTrigger cameraZoneTrigger = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				_003Cattempts_003E5__2 = _003C_003E1__state;
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					goto IL_01dc;
				}
				_003C_003E1__state = -1;
				if (!(MissionManager._003CInstance_003Ek__BackingField == null))
				{
					_003C_003E4__this.SubscribeToPhase();
					goto IL_00f8;
				}
			}
			if (_003Cattempts_003E5__2 < 10)
			{
				int num = _003Cattempts_003E5__2 + 1;
				_003Cattempts_003E5__2 = num;
				WaitForSeconds waitForSeconds = new WaitForSeconds(0.5f);
				_003C_003E2__current = waitForSeconds;
				_003C_003E1__state = 1;
				return true;
			}
			goto IL_00f8;
			IL_00f8:
			if (MissionManager._003CInstance_003Ek__BackingField == null)
			{
				if ((object)_003C_003E4__this != null)
				{
					GameObject gameObject = _003C_003E4__this.gameObject;
					if ((object)gameObject != null)
					{
						string name = gameObject.name;
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
						object arg = default(object);
						string text = $"[CameraZoneTrigger] '{name}': MissionManager not found after {arg} attempts. ";
						string message = text + "Phase gating will not function. Interaction remains enabled by default.";
						Debug.LogWarning(message, _003C_003E4__this);
						goto IL_01cd;
					}
				}
				NullReferenceException ex = new NullReferenceException();
				return (byte)(int)ex != 0;
			}
			goto IL_01cd;
			IL_01cd:
			cameraZoneTrigger._phaseSubscribeRetryRoutine = null;
			goto IL_01dc;
			IL_01dc:
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}
	}

	private sealed class _003CReapplyNextFrame_003Ed__48 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CameraZoneTrigger _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CReapplyNextFrame_003Ed__48(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_003b: Expected I4, but got I8
			//IL_0084: Expected I4, but got I8
			//IL_00fb: Expected I4, but got O
			CameraZoneTrigger cameraZoneTrigger = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			if (_003C_003E1__state == 1)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this == null)
				{
					NullReferenceException ex = new NullReferenceException();
					return (byte)(int)ex != 0;
				}
				if (!cameraZoneTrigger.consoleActive)
				{
					_003C_003E4__this.ActivateConsole();
				}
				cameraZoneTrigger._reapplyRoutine = null;
			}
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}
	}

	private static readonly HashSet<CameraZoneTrigger> s_allZones;

	private static CameraZoneTrigger s_currentActiveZone;

	public GameObject zoneCamera;

	public GameObject playerCamera;

	public GameObject promptUI;

	public GameObject unlockUI;

	public InputActionReference activateAction;

	public InputActionReference deactivateAction;

	public UnityEvent onConsoleActivated;

	public UnityEvent onConsoleDeactivated;

	private bool activateOnStart;

	private bool autoResolveByTags;

	private bool reResolveOnSceneEvents;

	private string playerTag;

	private string playerCameraTag;

	private string promptUITag;

	private string unlockUITag;

	private bool enforceSingleActiveZone;

	private bool disalowForceDeactivationBySingleActiveZone;

	private bool useBrokerLock;

	private string lockBrokerTag;

	private bool brokerFreezePlayerController;

	private bool brokerUseFreeMouse;

	private bool brokerUseUIActionMap;

	private string brokerDebugLabel;

	private bool brokerRequireMostRecentToDeactivate;

	private bool reapplyForceStateOnEnable;

	private bool reapplyForceStateNextFrame;

	private bool resetBaselineOnEnable;

	private bool watchMissionPhase;

	private bool playerInZone;

	private bool consoleActive;

	private bool _interactionEnabled;

	private bool _wasTimeScalePaused;

	private bool _forceRequestedActive;

	private Coroutine _reapplyRoutine;

	private Coroutine _phaseSubscribeRetryRoutine;

	private InteractionLockBroker _broker;

	private InteractionLockBroker.LockHandle _brokerHandle;

	private BoxCollider _boxCollider;

	private void Awake()
	{
		InteractionLockBroker broker = InteractionLockBroker.FindOrNull(lockBrokerTag);
		_broker = broker;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		BoxCollider boxCollider = default(BoxCollider);
		_boxCollider = boxCollider;
	}

	private void OnEnable()
	{
		s_allZones.Add(this);
		if (activateAction != null)
		{
			InputAction action = activateAction.action;
			if (action != null)
			{
				InputAction action2 = activateAction.action;
				Action<InputAction.CallbackContext> value = OnActivatePerformed;
				action2.performed += value;
				InputAction action3 = activateAction.action;
				if (!action3.enabled)
				{
					InputAction action4 = activateAction.action;
					action4.Enable();
				}
			}
		}
		if (deactivateAction != null)
		{
			InputAction action5 = deactivateAction.action;
			if (action5 != null)
			{
				InputAction action6 = deactivateAction.action;
				Action<InputAction.CallbackContext> value2 = OnDeactivatePerformed;
				action6.performed += value2;
				InputAction action7 = deactivateAction.action;
				if (!action7.enabled)
				{
					InputAction action8 = deactivateAction.action;
					action8.Enable();
				}
			}
		}
		if (reResolveOnSceneEvents)
		{
			UnityAction<Scene, LoadSceneMode> value3 = OnSceneLoaded;
			SceneManager.sceneLoaded += value3;
			UnityAction<Scene> value4 = OnSceneUnloaded;
			SceneManager.sceneUnloaded += value4;
		}
		if (autoResolveByTags)
		{
			ResolveByTagsIfNeeded();
		}
		if (reapplyForceStateOnEnable)
		{
			if (resetBaselineOnEnable)
			{
				ApplyBaselineVisualState();
			}
			if (_forceRequestedActive)
			{
				if (_reapplyRoutine != null)
				{
					StopCoroutine(_reapplyRoutine);
					_reapplyRoutine = null;
				}
				if (!reapplyForceStateNextFrame)
				{
					if (!consoleActive)
					{
						ActivateConsole();
					}
				}
				else
				{
					_003CReapplyNextFrame_003Ed__48 obj = new _003CReapplyNextFrame_003Ed__48(0);
					obj._003C_003E1__state = 0;
					obj._003C_003E4__this = this;
					Coroutine reapplyRoutine = StartCoroutine(obj);
					_reapplyRoutine = reapplyRoutine;
				}
			}
		}
		if (!watchMissionPhase)
		{
			return;
		}
		if (MissionManager._003CInstance_003Ek__BackingField == null)
		{
			if (_phaseSubscribeRetryRoutine == null)
			{
				_003CPhaseSubscribeRetryRoutine_003Ed__61 obj2 = new _003CPhaseSubscribeRetryRoutine_003Ed__61(0);
				obj2._003C_003E1__state = 0;
				obj2._003C_003E4__this = this;
				Coroutine phaseSubscribeRetryRoutine = StartCoroutine(obj2);
				_phaseSubscribeRetryRoutine = phaseSubscribeRetryRoutine;
			}
		}
		else
		{
			SubscribeToPhase();
		}
	}

	private void OnDisable()
	{
		if (activateAction != null)
		{
			InputAction action = activateAction.action;
			if (action != null)
			{
				InputAction action2 = activateAction.action;
				Action<InputAction.CallbackContext> value = OnActivatePerformed;
				action2.performed -= value;
			}
		}
		if (deactivateAction != null)
		{
			InputAction action3 = deactivateAction.action;
			if (action3 != null)
			{
				InputAction action4 = deactivateAction.action;
				Action<InputAction.CallbackContext> value2 = OnDeactivatePerformed;
				action4.performed -= value2;
			}
		}
		if (reResolveOnSceneEvents)
		{
			UnityAction<Scene, LoadSceneMode> value3 = OnSceneLoaded;
			SceneManager.sceneLoaded -= value3;
			UnityAction<Scene> value4 = OnSceneUnloaded;
			SceneManager.sceneUnloaded -= value4;
		}
		if (_reapplyRoutine != null)
		{
			StopCoroutine(_reapplyRoutine);
			_reapplyRoutine = null;
		}
		if (watchMissionPhase)
		{
			if (_phaseSubscribeRetryRoutine != null)
			{
				StopCoroutine(_phaseSubscribeRetryRoutine);
				_phaseSubscribeRetryRoutine = null;
			}
			if (MissionManager._003CInstance_003Ek__BackingField != null)
			{
				Action<MissionManager.GamePhase, MissionManager.GamePhase> value5 = OnPhaseChanged;
				MissionManager._003CInstance_003Ek__BackingField.PhaseChanged -= value5;
			}
		}
		if (consoleActive)
		{
			SafeRevertFromDisable();
		}
		bool flag = s_allZones.Remove(this);
		if (s_currentActiveZone == this)
		{
			s_currentActiveZone = null;
		}
	}

	private void OnDestroy()
	{
		if (consoleActive)
		{
			SafeRevertFromDisable();
		}
		bool flag = s_allZones.Remove(this);
		if (s_currentActiveZone == this)
		{
			s_currentActiveZone = null;
		}
	}

	private void Start()
	{
		if (autoResolveByTags)
		{
			ResolveByTagsIfNeeded();
		}
		ApplyBaselineVisualState();
		if (activateOnStart)
		{
			_forceRequestedActive = true;
			if (!consoleActive)
			{
				ActivateConsole();
			}
		}
	}

	private void Update()
	{
		//IL_00f7: Invalid comparison between F4 and I4
		float timeScale = Time.timeScale;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018056EC7Fh\"");
		bool flag = ((timeScale == 0f) ? true : false);
		if (flag != _wasTimeScalePaused)
		{
			_wasTimeScalePaused = flag;
			if ((bool)promptUI && playerInZone && !consoleActive && _interactionEnabled)
			{
				bool active = (byte)((flag ? 1u : 0u) ^ 1u) != 0;
				promptUI.SetActive(active);
			}
		}
	}

	private void ApplyBaselineVisualState()
	{
		if ((bool)promptUI)
		{
			promptUI.SetActive(value: false);
		}
		if ((bool)unlockUI)
		{
			unlockUI.SetActive(value: false);
		}
		if ((bool)zoneCamera)
		{
			zoneCamera.SetActive(value: false);
		}
		if ((bool)playerCamera)
		{
			playerCamera.SetActive(value: true);
		}
	}

	private void ReapplyRequestedState()
	{
		if (_reapplyRoutine != null)
		{
			StopCoroutine(_reapplyRoutine);
			_reapplyRoutine = null;
		}
		if (!reapplyForceStateNextFrame)
		{
			if (!consoleActive)
			{
				ActivateConsole();
			}
		}
		else
		{
			_003CReapplyNextFrame_003Ed__48 obj = new _003CReapplyNextFrame_003Ed__48(0);
			obj._003C_003E1__state = 0;
			obj._003C_003E4__this = this;
			Coroutine reapplyRoutine = StartCoroutine(obj);
			_reapplyRoutine = reapplyRoutine;
		}
	}

	private IEnumerator ReapplyNextFrame()
	{
		_003CReapplyNextFrame_003Ed__48 obj = new _003CReapplyNextFrame_003Ed__48(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private void ForceActivateImmediate()
	{
		if (!consoleActive)
		{
			ActivateConsole();
		}
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (autoResolveByTags)
		{
			ResolveByTagsIfNeeded();
		}
	}

	private void OnSceneUnloaded(Scene scene)
	{
		if (autoResolveByTags)
		{
			ResolveByTagsIfNeeded();
			if (consoleActive && playerCamera == null)
			{
				SafeRevertFromDisable();
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag(playerTag))
		{
			playerInZone = true;
			if ((bool)promptUI && !consoleActive && _interactionEnabled)
			{
				promptUI.SetActive(value: true);
			}
			if (autoResolveByTags)
			{
				ResolveByTagsIfNeeded();
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag(playerTag))
		{
			playerInZone = false;
			if ((bool)promptUI)
			{
				promptUI.SetActive(value: false);
			}
			if (consoleActive)
			{
				DeactivateConsole();
			}
		}
	}

	private unsafe void OnActivatePerformed(InputAction.CallbackContext ctx)
	{
		//IL_0055: Invalid comparison between F4 and I4
		//IL_0132: Expected O, but got Ref
		//IL_0132: Expected O, but got Ref
		//IL_0132: Expected O, but got Ref
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Expected O, but got Unknown
		//IL_0152: Expected O, but got I4
		//IL_015b: Expected O, but got I4
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Expected O, but got Unknown
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Expected O, but got Unknown
		if (consoleActive || !playerInZone || !_interactionEnabled)
		{
			return;
		}
		float timeScale = Time.timeScale;
		bool flag = timeScale == 0f;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018056CF80h\"");
		if (flag)
		{
			return;
		}
		if (_boxCollider != null)
		{
			if (!_boxCollider.enabled)
			{
				return;
			}
			Bounds bounds = _boxCollider.bounds;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm2,8\"");
			Bounds bounds2 = _boxCollider.bounds;
			Transform transform = _boxCollider.transform;
			Quaternion rotation = transform.rotation;
			object obj = default(object);
			object obj2 = default(object);
			object obj3 = default(object);
			Collider[] array = Physics.OverlapBox((Vector3)(&obj), (Vector3)(&obj2), (Quaternion)(&obj3));
			object obj4 = array + 32;
			object obj5 = 0;
			object obj6 = 0;
			while (true)
			{
				if ((nint)obj6 < array.Length)
				{
					if (((Component)obj4).CompareTag(playerTag))
					{
						break;
					}
					obj5++;
					obj4 += 8;
					obj6 = obj5;
					continue;
				}
				playerInZone = false;
				promptUI.SetActive(value: false);
				return;
			}
		}
		ActivateConsole();
	}

	private void OnDeactivatePerformed(InputAction.CallbackContext ctx)
	{
		if (consoleActive)
		{
			DeactivateConsole();
		}
	}

	private void ResolveByTagsIfNeeded()
	{
		if (playerCamera == null && !string.IsNullOrEmpty(playerCameraTag))
		{
			GameObject gameObject = SafeFindWithTag(playerCameraTag);
			playerCamera = gameObject;
		}
		if (promptUI == null && !string.IsNullOrEmpty(promptUITag))
		{
			GameObject gameObject2 = SafeFindWithTag(promptUITag);
			promptUI = gameObject2;
		}
		if (unlockUI == null && !string.IsNullOrEmpty(unlockUITag))
		{
			GameObject gameObject3 = SafeFindWithTag(unlockUITag);
			unlockUI = gameObject3;
		}
		if (_broker == null && useBrokerLock)
		{
			TryFindBroker();
		}
	}

	private static GameObject SafeFindWithTag(string tag)
	{
		if (!string.IsNullOrEmpty(tag))
		{
			return GameObject.FindGameObjectWithTag(tag);
		}
		return null;
	}

	private void TryFindBroker()
	{
		InteractionLockBroker broker = InteractionLockBroker.FindOrNull(lockBrokerTag);
		_broker = broker;
	}

	public void SetInteractionEnabled(bool enabled)
	{
		_interactionEnabled = enabled;
		if (enabled)
		{
			if ((bool)promptUI && playerInZone && !consoleActive)
			{
				promptUI.SetActive(value: true);
			}
			return;
		}
		if ((bool)promptUI)
		{
			promptUI.SetActive(value: false);
		}
		if (consoleActive)
		{
			_forceRequestedActive = false;
			DeactivateConsole();
		}
		playerInZone = false;
	}

	private void TrySubscribeToPhaseOrRetry()
	{
		if (MissionManager._003CInstance_003Ek__BackingField == null)
		{
			if (_phaseSubscribeRetryRoutine == null)
			{
				_003CPhaseSubscribeRetryRoutine_003Ed__61 obj = new _003CPhaseSubscribeRetryRoutine_003Ed__61(0);
				obj._003C_003E1__state = 0;
				obj._003C_003E4__this = this;
				Coroutine phaseSubscribeRetryRoutine = StartCoroutine(obj);
				_phaseSubscribeRetryRoutine = phaseSubscribeRetryRoutine;
			}
		}
		else
		{
			SubscribeToPhase();
		}
	}

	private IEnumerator PhaseSubscribeRetryRoutine()
	{
		_003CPhaseSubscribeRetryRoutine_003Ed__61 obj = new _003CPhaseSubscribeRetryRoutine_003Ed__61(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private void SubscribeToPhase()
	{
		//IL_002b: Expected O, but got I4
		Action<MissionManager.GamePhase, MissionManager.GamePhase> value = OnPhaseChanged;
		MissionManager._003CInstance_003Ek__BackingField.PhaseChanged += value;
		MissionManager missionManager = MissionManager._003CInstance_003Ek__BackingField;
		object obj = missionManager._003CCurrentPhase_003Ek__BackingField - 2;
		bool interactionEnabled = obj == null;
		SetInteractionEnabled(interactionEnabled);
	}

	private void UnsubscribeFromPhase()
	{
		if (_phaseSubscribeRetryRoutine != null)
		{
			StopCoroutine(_phaseSubscribeRetryRoutine);
			_phaseSubscribeRetryRoutine = null;
		}
		if (MissionManager._003CInstance_003Ek__BackingField != null)
		{
			Action<MissionManager.GamePhase, MissionManager.GamePhase> value = OnPhaseChanged;
			MissionManager._003CInstance_003Ek__BackingField.PhaseChanged -= value;
		}
	}

	private void OnPhaseChanged(MissionManager.GamePhase prev, MissionManager.GamePhase next)
	{
		//IL_000e: Expected O, but got I4
		object obj = next - 2;
		bool interactionEnabled = obj == null;
		SetInteractionEnabled(interactionEnabled);
	}

	public void ActivateConsole()
	{
		//IL_0323: Expected O, but got I4
		//IL_032c: Expected O, but got I4
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Expected O, but got Unknown
		if (autoResolveByTags)
		{
			ResolveByTagsIfNeeded();
		}
		if (enforceSingleActiveZone)
		{
			List<CameraZoneTrigger> list = new List<CameraZoneTrigger>(s_allZones);
			object obj = 0;
			object obj2 = 0;
			UnityEngine.Object obj3 = default(UnityEngine.Object);
			while ((nint)obj2 < list._size)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				if (obj3 != null && obj3 != this)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v216 @ stack_8_v5 (UnityEngine.Object)+AE]");
					if ((nint)0 != 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v216 @ stack_8_v5 (UnityEngine.Object)+89]");
						if ((nint)0 != 0)
						{
							return;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v216 @ stack_8_v5 (UnityEngine.Object)+AE]");
						bool flag = (nint)0 == 0;
						_ = 0;
						if (!flag)
						{
							((CameraZoneTrigger)obj3).DeactivateConsole();
						}
					}
				}
				obj++;
				obj2 = obj;
			}
		}
		consoleActive = true;
		if ((bool)promptUI)
		{
			promptUI.SetActive(value: false);
		}
		if ((bool)unlockUI)
		{
			unlockUI.SetActive(value: true);
		}
		if ((bool)zoneCamera)
		{
			zoneCamera.SetActive(value: true);
		}
		if ((bool)playerCamera)
		{
			playerCamera.SetActive(value: false);
		}
		if (useBrokerLock)
		{
			EnsureBrokerLock();
		}
		if (enforceSingleActiveZone)
		{
			s_currentActiveZone = this;
		}
		if (onConsoleActivated != null)
		{
			onConsoleActivated.Invoke();
		}
	}

	public void DeactivateConsole()
	{
		bool flag = !useBrokerLock;
		consoleActive = false;
		if (!flag)
		{
			ReleaseBrokerLockIfHeld();
		}
		if ((bool)promptUI && playerInZone)
		{
			promptUI.SetActive(value: true);
		}
		if ((bool)unlockUI)
		{
			unlockUI.SetActive(value: false);
		}
		if ((bool)zoneCamera)
		{
			zoneCamera.SetActive(value: false);
		}
		if ((bool)playerCamera)
		{
			playerCamera.SetActive(value: true);
		}
		if (enforceSingleActiveZone && s_currentActiveZone == this)
		{
			s_currentActiveZone = null;
		}
		if (onConsoleDeactivated != null)
		{
			onConsoleDeactivated.Invoke();
		}
	}

	public void ForceActivate()
	{
		_forceRequestedActive = true;
		if (!consoleActive)
		{
			ActivateConsole();
		}
	}

	public void ForceDeactivate()
	{
		bool flag = !consoleActive;
		_forceRequestedActive = false;
		if (!flag)
		{
			DeactivateConsole();
		}
	}

	private unsafe void EnsureBrokerLock()
	{
		//IL_0095: Expected O, but got Ref
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Expected O, but got Unknown
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Expected Ref, but got Unknown
		if ((object)_brokerHandle != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (CameraZoneTrigger)+D4]");
			if ((nint)0 > (nint)0)
			{
				return;
			}
		}
		if (_broker == null)
		{
			TryFindBroker();
		}
		if (_broker != null)
		{
			bool flag = default(bool);
			InteractionLockBroker.LockHandle brokerHandle = _broker.Acquire((InteractionLockBroker.LockRequest)(&flag));
			_brokerHandle = brokerHandle;
			if (brokerRequireMostRecentToDeactivate && (object)_brokerHandle != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (CameraZoneTrigger)+D4]");
				if ((nint)0 > (nint)0 && deactivateAction != null)
				{
					InputAction action = deactivateAction.action;
					if (action != null)
					{
						InputAction action2 = deactivateAction.action;
						Action<InputAction.CallbackContext> value = OnDeactivatePerformed;
						action2.performed -= value;
						if (_broker.IsMostRecentLock(ref *(InteractionLockBroker.LockHandle*)(this + 208)))
						{
							_003C_003CInteractionLockBroker_OnRequestsChanged_003Eg__SubscribeDeactivateActionNextFrame_007C72_0_003Ed obj = new _003C_003CInteractionLockBroker_OnRequestsChanged_003Eg__SubscribeDeactivateActionNextFrame_007C72_0_003Ed(0);
							obj._003C_003E1__state = 0;
							obj._003C_003E4__this = this;
							Coroutine coroutine = StartCoroutine(obj);
						}
					}
				}
			}
			InteractionLockBroker broker = _broker;
			Action b = InteractionLockBroker_OnRequestsChanged;
			Delegate obj2 = broker.OnRequestsChanged;
			Delegate obj6 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Combine(obj2, b);
				bool flag2 = (object)obj3 == null;
				Delegate obj4 = null;
				if (!flag2)
				{
					bool flag3 = (object)obj3.GetType() != typeof(Action);
					obj4 = null;
					if (!flag3)
					{
						obj4 = obj3;
					}
					if ((object)obj4 == null)
					{
						break;
					}
				}
				object obj5 = broker + 176;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag4 = (object)obj6 != obj2;
				obj2 = obj6;
				if (!flag4)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
			throw new NullReferenceException();
		}
		string message = "[CameraZoneTrigger] InteractionLockBroker not found (tag='" + lockBrokerTag + "'). Zone will still swap cameras/UI but will not freeze/unlock via broker.";
		Debug.LogWarning(message, this);
	}

	private void ReleaseBrokerLockIfHeld()
	{
		//IL_01a9: Expected O, but got I4
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Expected O, but got Unknown
		//IL_016c: Expected I, but got O
		//IL_0213: Expected I, but got O
		if ((object)_brokerHandle == null)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (CameraZoneTrigger)+D4]");
		if ((nint)0 <= (nint)0)
		{
			return;
		}
		if (_broker == null)
		{
			TryFindBroker();
		}
		if (!(_broker != null))
		{
			goto IL_019e;
		}
		InteractionLockBroker broker = _broker;
		Action action = InteractionLockBroker_OnRequestsChanged;
		bool flag = (object)_broker == null;
		nint num = 0;
		Action action2 = action;
		if (flag)
		{
			goto IL_01ae;
		}
		Delegate obj = broker.OnRequestsChanged;
		object obj2 = _broker + 176;
		Delegate obj5 = default(Delegate);
		NullReferenceException ex;
		while (true)
		{
			Delegate obj3 = Delegate.Remove(obj, action);
			bool flag2 = (object)obj3 == null;
			Delegate obj4 = null;
			if (!flag2)
			{
				bool flag3 = (object)obj3.GetType() != typeof(Action);
				obj4 = null;
				if (!flag3)
				{
					obj4 = obj3;
				}
				bool flag4 = (object)obj4 == null;
				num = unchecked((nint)null);
				ex = (NullReferenceException)(object)obj3;
				action2 = (Action)(object)typeof(Action);
				if (flag4)
				{
					break;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
			bool flag5 = (object)obj5 != obj;
			obj = obj5;
			if (flag5)
			{
				continue;
			}
			goto IL_0153;
		}
		goto IL_026a;
		IL_0153:
		bool flag6 = (object)_broker == null;
		num = (nint)obj5;
		action2 = (Action)(object)_broker;
		if (!flag6)
		{
			bool flag7 = _broker.Release(_brokerHandle);
			goto IL_019e;
		}
		goto IL_01ae;
		IL_026a:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		return;
		IL_019e:
		_brokerHandle = (InteractionLockBroker.LockHandle)0;
		return;
		IL_01ae:
		ex = new NullReferenceException();
		goto IL_026a;
	}

	private void SafeRevertFromDisable()
	{
		if (useBrokerLock)
		{
			ReleaseBrokerLockIfHeld();
		}
		if ((bool)unlockUI)
		{
			unlockUI.SetActive(value: false);
		}
		if ((bool)zoneCamera)
		{
			zoneCamera.SetActive(value: false);
		}
		if ((bool)playerCamera)
		{
			playerCamera.SetActive(value: true);
		}
		if (enforceSingleActiveZone && s_currentActiveZone == this)
		{
			s_currentActiveZone = null;
		}
		consoleActive = false;
	}

	private unsafe void InteractionLockBroker_OnRequestsChanged()
	{
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Expected Ref, but got Unknown
		if (!brokerRequireMostRecentToDeactivate || (object)_brokerHandle == null)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (CameraZoneTrigger)+D4]");
		if ((nint)0 <= (nint)0 || !(deactivateAction != null))
		{
			return;
		}
		InputAction action = deactivateAction.action;
		if (action != null)
		{
			InputAction action2 = deactivateAction.action;
			Action<InputAction.CallbackContext> value = OnDeactivatePerformed;
			action2.performed -= value;
			if (_broker.IsMostRecentLock(ref *(InteractionLockBroker.LockHandle*)(this + 208)))
			{
				_003C_003CInteractionLockBroker_OnRequestsChanged_003Eg__SubscribeDeactivateActionNextFrame_007C72_0_003Ed obj = new _003C_003CInteractionLockBroker_OnRequestsChanged_003Eg__SubscribeDeactivateActionNextFrame_007C72_0_003Ed(0);
				obj._003C_003E1__state = 0;
				obj._003C_003E4__this = this;
				Coroutine coroutine = StartCoroutine(obj);
			}
		}
	}

	public CameraZoneTrigger()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AC2A]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		autoResolveByTags = true;
		playerTag = "Player";
		playerCameraTag = "CMCam";
		promptUITag = "";
		unlockUITag = "";
		enforceSingleActiveZone = true;
		useBrokerLock = true;
		lockBrokerTag = "LockBroker";
		brokerFreezePlayerController = true;
		brokerUseUIActionMap = true;
		brokerDebugLabel = "CameraZoneTrigger:Console";
		brokerRequireMostRecentToDeactivate = true;
		reapplyForceStateNextFrame = true;
		watchMissionPhase = true;
		_interactionEnabled = true;
		base._002Ector();
	}

	static CameraZoneTrigger()
	{
		HashSet<CameraZoneTrigger> hashSet = new HashSet<CameraZoneTrigger>();
		s_allZones = hashSet;
	}

	private IEnumerator _003CInteractionLockBroker_OnRequestsChanged_003Eg__SubscribeDeactivateActionNextFrame_007C72_0()
	{
		_003C_003CInteractionLockBroker_OnRequestsChanged_003Eg__SubscribeDeactivateActionNextFrame_007C72_0_003Ed obj = new _003C_003CInteractionLockBroker_OnRequestsChanged_003Eg__SubscribeDeactivateActionNextFrame_007C72_0_003Ed(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}
}
