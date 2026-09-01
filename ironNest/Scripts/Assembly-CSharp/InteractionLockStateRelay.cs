using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Gameplay/Interaction Lock State Relay")]
public class InteractionLockStateRelay : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CRetryFindRoutine_003Ed__31 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public InteractionLockStateRelay _003C_003E4__this;

		private WaitForSecondsRealtime _003Cwait_003E5__2;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		[DebuggerHidden]
		public _003CRetryFindRoutine_003Ed__31(int _003C_003E1__state)
		{
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
		}
	}

	[Header("Broker Lookup")]
	[Tooltip("Unity Tag used to find the InteractionLockBroker.\n\nSetup:\n- Place exactly ONE InteractionLockBroker in your master scene.\n- Tag the broker's GameObject with this tag.\n\nRules:\n- Tag must exist in Project Settings > Tags and Layers.\n- Lookup uses GameObject.FindGameObjectWithTag (first match).\n\nSafe examples:\n- \"LockBroker\" (recommended default)")]
	[SerializeField]
	private string brokerTag;

	[Tooltip("Optional explicit broker reference.\n\nWhen set:\n- Tag lookup is skipped.\n- Useful if you deliberately have multiple brokers (not recommended) or spawn a broker at runtime.\n\nWhen null:\n- Broker is found using Broker Tag (if Auto Find Broker is enabled).")]
	[SerializeField]
	private InteractionLockBroker explicitBroker;

	[Tooltip("If true, this relay tries to find the broker in OnEnable and again whenever Acquire/Release is called.\nRecommended for cross-scene setups and runtime-spawned relays.\n\nIf false:\n- You must assign Explicit Broker.\n\nNotes:\n- Tag lookup uses GameObject.FindGameObjectWithTag and will fail safely if the tag doesn't exist.")]
	[SerializeField]
	private bool autoFindBroker;

	[Tooltip("If true and the broker is missing, this relay retries lookup periodically (unscaled time).\n\nRecommended for additive scenes and runtime instantiation where the broker might appear later.\n\nIf false:\n- Lookup happens only on OnEnable and when calling Acquire/Release.\n\nPerformance:\n- Very low cost; uses FindGameObjectWithTag at the configured interval.")]
	[SerializeField]
	private bool retryFindIfMissing;

	[Tooltip("Seconds between retry attempts when Retry Find If Missing is enabled.\n\nRules:\n- Uses unscaled time.\n- Minimum enforced: 0.05.\n\nSafe examples:\n- 0.5\n- 1.0")]
	[SerializeField]
	[Min(0.05f)]
	private float retryFindIntervalSeconds;

	[Header("Enable/Disable Push (Optional)")]
	[Tooltip("If true, this relay calls Acquire() automatically on OnEnable.\n\nUse cases:\n- While this object exists, force a lock state (e.g., menu overlay prefab).\n- Timeline/sequence objects that are enabled/disabled.\n\nNotes:\n- Acquire is safe to call multiple times; only one handle is owned by this relay.")]
	[SerializeField]
	private bool acquireOnEnable;

	[Tooltip("If true, this relay calls Release() automatically on OnDisable.\n\nRecommended: true.\n\nWhy:\n- Prevents stuck locks if a prefab is destroyed/unloaded while holding a lock.\n\nNotes:\n- Only releases THIS relay's handle; it never affects other lock reasons.")]
	[SerializeField]
	private bool releaseOnDisable;

	[Header("Lock Request (What this relay asks the broker for)")]
	[Tooltip("If true, this relay's lock request contributes to freezing the player's FirstPersonController.\n\nResolved behavior (in broker):\n- If ANY active lock requests FreezePlayerController, player is frozen.\n- Otherwise, player is unfrozen.\n\nSafe examples:\n- true (most UI/console/cutscene locks)\n- false (unlock cursor without freezing, if a designer intentionally wants that)")]
	[SerializeField]
	private bool freezePlayerController;

	[Tooltip("If true, this relay's lock request contributes to forcing the DynamicCursorManager into FreeMouse mode.\n\nResolved behavior (in broker):\n- If ANY active lock requests UseFreeMouse, cursor mode is FreeMouse.\n- Otherwise, cursor mode is FPSLocked.\n\nSafe examples:\n- true (menus, consoles, drag interactions)\n- false (freeze player but keep FPSLocked cursor, uncommon)")]
	[SerializeField]
	private bool useFreeMouse;

	[Tooltip("If true, this relay's lock request contributes to switching PlayerInput to the broker's UI action map.\n\nResolved behavior (in broker):\n- If ANY active lock requests UseUIActionMap, action map becomes UI.\n- Otherwise, action map becomes Player.\n\nImportant:\n- The broker intentionally supports ONE UI map name and ONE Player map name.\n- Do not use this to hide map inconsistencies.\n\nSafe examples:\n- true (UI panels that need UI map)\n- false (world interactions that don't need map swap)")]
	[SerializeField]
	private bool useUIActionMap;

	[Tooltip("NEW: If true, this relay's lock request contributes to suppressing the game's *virtual cursor* and blocking world cursor interactions.\n\nWhat this does:\n- Hides the UI cursor (UnifiedCursorUI renderer).\n- Blocks all world cursor interactions (no raycasts, hover, clicks, drags, or related events).\n- Immediately force-releases any active click and ends any active/captured drag.\n\nResolved behavior (in broker):\n- If ANY active lock requests HideVirtualCursorAndBlockWorld, DynamicCursorManager is suppressed.\n\nUse cases:\n- Cinematics/cutscenes\n- Fullscreen fades / transitions\n- Prevent accidental drag/click while the game is not meant to be interactable.")]
	[SerializeField]
	private bool hideVirtualCursorAndBlockWorld;

	[Header("Diagnostics")]
	[Tooltip("Debug label included in the broker request.\n\nUsage:\n- Helps identify which system/prefab is holding locks when reading broker logs.\n\nFormat rules:\n- Any string.\n- No tokens are processed.\n\nSafe examples:\n- \"Relay:PauseMenu\"\n- \"Relay:CutsceneIntro\"")]
	[SerializeField]
	private string debugLabel;

	[Tooltip("If true, logs warnings when broker is missing (Acquire/Release requests become no-ops).\n\nRecommended:\n- true during wiring/testing\n- false for production silence")]
	[SerializeField]
	private bool logWarnings;

	[Tooltip("Invoked when this relay successfully acquires a lock handle.\n\nNotes:\n- Fires only if a broker is found and a handle is obtained.\n- Does not fire again if Acquire() is called while already locked.\n\nUse cases:\n- Enable a UI panel\n- Play a sound\n- Drive additional game logic")]
	[SerializeField]
	private UnityEvent onAcquired;

	[Tooltip("Invoked when this relay successfully releases its lock handle.\n\nNotes:\n- Fires only if a handle was active.\n- Does not imply the game is now unlocked globally; other lock reasons may remain.\n\nUse cases:\n- Hide a UI panel\n- Play a sound\n- Drive additional game logic")]
	[SerializeField]
	private UnityEvent onReleased;

	[Tooltip("Invoked once when the relay finds a broker after previously not having one.\n\nNotes:\n- Will not spam; only fires on missing->found transitions.\n\nUse cases:\n- Delay initialization until broker exists\n- Enable UI or interactables when master scene loads")]
	[SerializeField]
	private UnityEvent onBrokerFound;

	private InteractionLockBroker _broker;

	private bool _hadBroker;

	private InteractionLockBroker.LockHandle _handle;

	private Coroutine _retryRoutine;

	public bool IsLocked => false;

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	public void Acquire()
	{
	}

	public void Release()
	{
	}

	public void Toggle()
	{
	}

	public void SetLocked(bool locked)
	{
	}

	private InteractionLockBroker GetBroker()
	{
		return null;
	}

	private void TryResolveBroker()
	{
	}

	private void NotifyBrokerFoundIfNeeded()
	{
	}

	[IteratorStateMachine(typeof(_003CRetryFindRoutine_003Ed__31))]
	private IEnumerator RetryFindRoutine()
	{
		return null;
	}
}
