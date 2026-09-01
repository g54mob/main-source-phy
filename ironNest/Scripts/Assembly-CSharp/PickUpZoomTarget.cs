using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
public class PickUpZoomTarget : MonoBehaviour
{
	public enum FocusScaleMode
	{
		[Tooltip("Held local scale = (original local scale) * (Focus Scale Multiplier) [component-wise].\nThis preserves the prefab's authored size while letting you scale it up/down.\nExample:\n- Original (1,1,1) and Multiplier (1.2,1.2,1.2) => Held (1.2,1.2,1.2)\n- Original (0.5,0.5,0.5) and Multiplier (2,2,2) => Held (1,1,1)")]
		MultiplyOriginal = 0,
		[Tooltip("Held local scale = (Focus Scale Multiplier) exactly.\nUse this when you want a consistent held size regardless of the object's original authored scale.\nExample:\n- Focus Scale Multiplier set to (1,1,1) => Held is always (1,1,1)\n- Focus Scale Multiplier set to (0.2,0.2,0.2) => Held is always (0.2,0.2,0.2)")]
		SetAbsolute = 1
	}

	public enum ReleaseBehavior
	{
		ReturnToOriginal = 0,
		KeepCurrentWorldPose = 1,
		UseReleaseTagWithOffsets = 2
	}

	public enum DropTriggerMode
	{
		[Tooltip("Drop when any action in 'Drop Action References' enters 'started'.\nIf the list is empty or contains only null entries, this mode effectively does nothing (no automatic drop).")]
		UseDropActionReferences = 0,
		[Tooltip("Drop when ANY button-like action in the provided 'Drop Any Action Asset' enters 'started'.\nThis is NOT raw device input; it is 'any InputAction in the asset that looks like a button press'.\nFiltering rules:\n- Included if action.type == Button.\n- Also included (optional) if action.expectedControlType == \"Button\" (case-insensitive).\n- Non-button actions (Value/PassThrough like Look/Delta/Move) are ignored.\nIf a button action is not being detected, set its Action Type to 'Button' in the Input Actions editor.\nIf the asset is not assigned, this mode effectively does nothing (no automatic drop).")]
		UseAnyButtonActionInAsset = 1
	}

	[CompilerGenerated]
	private sealed class _003CMoveToFocus_003Ed__60 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PickUpZoomTarget _003C_003E4__this;

		private Vector3 _003CstartPos_003E5__2;

		private Quaternion _003CstartRot_003E5__3;

		private Vector3 _003CstartScale_003E5__4;

		private Vector3 _003CtargetScale_003E5__5;

		private float _003Celapsed_003E5__6;

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
		public _003CMoveToFocus_003Ed__60(int _003C_003E1__state)
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

	[CompilerGenerated]
	private sealed class _003CMoveToRelease_003Ed__61 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PickUpZoomTarget _003C_003E4__this;

		private Transform _003CtargetParent_003E5__2;

		private Vector3 _003CtargetPos_003E5__3;

		private Quaternion _003CtargetRot_003E5__4;

		private Vector3 _003CtargetScale_003E5__5;

		private bool _003CsetLocalAfterParent_003E5__6;

		private bool _003CsetScaleAfterParent_003E5__7;

		private Vector3 _003CstartPos_003E5__8;

		private Quaternion _003CstartRot_003E5__9;

		private Vector3 _003CstartScale_003E5__10;

		private float _003Celapsed_003E5__11;

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
		public _003CMoveToRelease_003Ed__61(int _003C_003E1__state)
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

	[Header("Focus Target (Tag-based)")]
	[Tooltip("Tag on the Transform that this object should follow when picked up (typically the player's Camera).\nCross-scene safe: the first active GameObject with this tag will be used.\nNotes:\n- Default 'MainCamera' is Unity's standard tag for the main camera.\n- Ensure the tag exists in Project Settings > Tags and is assigned to your camera.\nExamples:\n- MainCamera\n- PlayerCamera")]
	[SerializeField]
	private string focusRootTag;

	[Tooltip("If enabled, a child anchor will be created or found under the focus root to serve as the precise parent while held.\nThis keeps local Position/Rotation/Scale Offsets consistent even if the camera moves.\nRecommended: ON.")]
	[SerializeField]
	private bool createAnchorUnderFocusRoot;

	[Tooltip("Name of the child anchor created/found under the focus root (only used when 'Create Anchor Under Focus Root' is enabled).\nIf a direct child with this name exists, it will be reused; otherwise a new child will be created.")]
	[SerializeField]
	private string focusAnchorName;

	[Tooltip("Local position (X,Y,Z) to apply to the created/found anchor under the focus root.\nOnly used when 'Create Anchor Under Focus Root' is enabled.\nSafe default: (0,0,0).")]
	[SerializeField]
	private Vector3 focusAnchorLocalPosition;

	[Tooltip("Local rotation (Euler degrees) to apply to the created/found anchor under the focus root.\nOnly used when 'Create Anchor Under Focus Root' is enabled.\nSafe default: (0,0,0).")]
	[SerializeField]
	private Vector3 focusAnchorLocalRotation;

	[Tooltip("Local scale to apply to the created/found anchor under the focus root.\nOnly used when 'Create Anchor Under Focus Root' is enabled.\nSafe default: (1,1,1).")]
	[SerializeField]
	private Vector3 focusAnchorLocalScale;

	[Tooltip("Local position offset from the resolved focus Transform applied to THIS object while picked up.\nZ moves forward from the focus. Example puts the object ~0.7m in front of the camera.")]
	public Vector3 positionOffset;

	[Tooltip("Local rotation offset (Euler degrees) from the resolved focus Transform applied to THIS object while picked up.\nUse (0,0,0) to match the focus rotation exactly.")]
	public Vector3 rotationOffset;

	[Tooltip("If enabled, the object will be scaled while picked up.\nThe exact scaling rule is controlled by 'Focus Scale Mode'.")]
	[SerializeField]
	private bool applyScaleOnFocus;

	[Tooltip("Determines how the held scale is computed when 'Apply Scale On Focus' is enabled.\nModes:\n- MultiplyOriginal: heldScale = originalLocalScale * Focus Scale Multiplier\n- SetAbsolute: heldScale = Focus Scale Multiplier (exact)\nSafe default: MultiplyOriginal (preserves previous behavior).")]
	[SerializeField]
	private FocusScaleMode focusScaleMode;

	[Tooltip("Scale value used while picked up.\nMeaning depends on 'Focus Scale Mode':\n- MultiplyOriginal: this is a multiplier applied to the object's ORIGINAL local scale.\n- SetAbsolute: this is the exact held local scale.\nSafe examples:\n- MultiplyOriginal + (1,1,1) => no scale change\n- MultiplyOriginal + (1.2,1.2,1.2) => 20% larger than original\n- SetAbsolute + (1,1,1) => held scale becomes exactly (1,1,1)\n- SetAbsolute + (0.25,0.25,0.25) => held scale becomes exactly (0.25,0.25,0.25)")]
	public Vector3 focusScaleMultiplier;

	[Header("Release Target (Optional, Tag-based)")]
	[Tooltip("Behavior when releasing:\n- ReturnToOriginal: go back to where it started and reparent to original parent; scale resets to ORIGINAL local scale.\n- KeepCurrentWorldPose: stop following; keep current world pose and current local scale.\n- UseReleaseTagWithOffsets: move to the first active object with 'Release Target Tag' and parent under it; scale = ORIGINAL * Release Scale Multiplier.")]
	[SerializeField]
	private ReleaseBehavior releaseMode;

	[Tooltip("Tag used to locate the release target when 'Release Mode' is 'UseReleaseTagWithOffsets'.\nEnsure the tag exists (Project Settings > Tags) and is assigned to the desired release target.\nIf not found at runtime, release will fall back to 'ReturnToOriginal'.")]
	[SerializeField]
	private string releaseTargetTag;

	[Tooltip("Local position offset from the release target (tag-resolved) applied to THIS object on release.\nOnly used when 'UseReleaseTagWithOffsets' is selected.")]
	public Vector3 releasePositionOffset;

	[Tooltip("Local rotation offset (Euler degrees) from the release target (tag-resolved) applied to THIS object on release.\nOnly used when 'UseReleaseTagWithOffsets' is selected.")]
	public Vector3 releaseRotationOffset;

	[Tooltip("If enabled and 'Release Mode' is 'UseReleaseTagWithOffsets', the object will be scaled on release.\nScaling uses the object's ORIGINAL local scale as the baseline and multiplies it by 'Release Scale Multiplier'.")]
	[SerializeField]
	private bool applyScaleOnRelease;

	[Tooltip("Scale multiplier applied to the object's ORIGINAL local scale when released using 'UseReleaseTagWithOffsets'.\nFinal release local scale = (original local scale) * (Release Scale Multiplier) [component-wise].")]
	public Vector3 releaseScaleMultiplier;

	[Header("Movement")]
	[Tooltip("Duration in seconds for pick up and release animations.\nSet to 0 for instant snap.")]
	[Min(0f)]
	public float moveDuration;

	[Tooltip("Interpolation curve for animations (time 0..1 on X, interpolation factor on Y).\nSafe default: Ease In-Out.")]
	public AnimationCurve easing;

	[Tooltip("If true, uses unscaled time (ignores Time.timeScale) for animations and input checks.")]
	public bool useUnscaledTime;

	[Header("Drop / Input (Input System)")]
	[Tooltip("How this component decides when to drop while held.\nModes:\n- UseDropActionReferences: drops when any configured InputActionReference enters 'started'. If none are assigned, nothing triggers.\n- UseAnyButtonActionInAsset: drops when any button-like action in the provided InputActionAsset enters 'started'.\nNotes:\n- This component does not enable/disable actions; your PlayerInput/Input System setup must manage that.\n- Only Input Actions are used (no KeyCode/legacy input).")]
	[SerializeField]
	private DropTriggerMode dropTriggerMode;

	[Tooltip("List of InputActionReference(s) that will cause a release when any one of them enters 'started'.\nAssign actions from your InputActionAsset/PlayerInput.\nNull/empty entries are ignored.\nBehavior:\n- OR-aggregate: any enabled action in this list may trigger the release.\n- This component does not enable/disable the referenced actions; they are expected to be managed by your InputActionAsset/PlayerInput.\nSafe examples:\n- Assign a single 'Drop' action.\n- Assign both 'Interact' and 'Cancel' to allow either to drop.\nIf you leave this empty, the object will NOT auto-drop in this mode (Release must be called by other code/events).")]
	[SerializeField]
	private InputActionReference[] dropActionReferences;

	[Tooltip("InputActionAsset used when 'Drop Trigger Mode' is 'Use Any Button Action In Asset'.\nAll actions inside this asset are scanned; only button-like actions are subscribed.\nPrefab-friendly: assign your one central controls asset here (typically the same asset used by PlayerInput).\nIf left unassigned, the object will NOT auto-drop in this mode (Release must be called by other code/events).")]
	[SerializeField]
	private InputActionAsset dropAnyActionAsset;

	[Tooltip("If true, actions are considered 'button-like' if they are marked as Button OR if their Expected Control Type is 'Button'.\nRecommended ON.\nTurn OFF if you want to require Action Type == Button strictly.")]
	[SerializeField]
	private bool includeExpectedControlTypeButtonAsButton;

	[Tooltip("If true, actions in the asset are only subscribed if they are currently enabled at the moment of subscription.\nRecommended OFF (default) so actions that become enabled later can still fire and drop.\nNote: subscribing to a disabled action is safe; it simply won't trigger until enabled by your input setup.")]
	[SerializeField]
	private bool onlySubscribeEnabledActions;

	[Header("Auto-Resolve")]
	[Tooltip("If true, attempts to resolve the focus root by tag during Awake().\nIf resolution fails in Awake (e.g., camera scene loads later), no error is thrown; resolution will be attempted again on PickUp if allowed.")]
	public bool resolveOnAwake;

	[Tooltip("If true, when PickUp() is called and the focus is missing, tries to resolve it by tag at that moment.\nRecommended ON for multi-scene setups.")]
	public bool resolveIfMissingOnPickUp;

	[Header("Events")]
	[Tooltip("Invoked immediately after a successful PickUp() call (before the move completes).")]
	public UnityEvent onPickedUp;

	[Tooltip("Invoked immediately after a Release() call (before the move completes).")]
	public UnityEvent onReleased;

	private Vector3 originalLocalPosition;

	private Quaternion originalRotation;

	private Transform originalParent;

	private Vector3 originalLocalScale;

	private bool isHeld;

	private Coroutine moveCoroutine;

	private Transform resolvedFocusRoot;

	private Transform resolvedFocus;

	private readonly List<InputAction> dropSubscribedActions;

	public bool IsHeld => false;

	private bool IsMoving => false;

	private float DeltaTime => 0f;

	[Tooltip("Convenience runtime setter to change drop trigger mode from UnityEvents or code.")]
	public void SetDropTriggerMode(DropTriggerMode mode)
	{
	}

	[Tooltip("Convenience runtime setter to configure drop actions programmatically (only used in 'Use Drop Action References' mode).\nPassing null clears the list (no actions configured).")]
	public void SetDropActionReferences(InputActionReference[] actions)
	{
	}

	[Tooltip("Convenience runtime setter to configure the asset used for 'Use Any Button Action In Asset' mode.\nPassing null clears the asset reference (no asset configured).")]
	public void SetDropAnyActionAsset(InputActionAsset asset)
	{
	}

	private void Awake()
	{
	}

	private void CaptureOriginalState()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	public void PickUp()
	{
	}

	public void Release()
	{
	}

	public void TogglePickUp()
	{
	}

	public void ResetToOriginalImmediate()
	{
	}

	public bool TryResolveFocus(out Transform focusRoot, out Transform focus)
	{
		focusRoot = null;
		focus = null;
		return false;
	}

	private Vector3 ComputeHeldLocalScale(Vector3 currentLocalScale)
	{
		return default(Vector3);
	}

	[IteratorStateMachine(typeof(_003CMoveToFocus_003Ed__60))]
	private IEnumerator MoveToFocus()
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CMoveToRelease_003Ed__61))]
	private IEnumerator MoveToRelease()
	{
		return null;
	}

	private void SubscribeDropActions()
	{
	}

	private void UnsubscribeDropActions()
	{
	}

	private bool IsButtonLike(InputAction act)
	{
		return false;
	}

	private void OnDropActionStarted(InputAction.CallbackContext ctx)
	{
	}
}
