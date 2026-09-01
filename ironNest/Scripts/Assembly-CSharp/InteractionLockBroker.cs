using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

[AddComponentMenu("Gameplay/Interaction Lock Broker")]
[DisallowMultipleComponent]
public class InteractionLockBroker : MonoBehaviour
{
	[Serializable]
	public struct LockRequest
	{
		[Tooltip("If true, this request contributes to freezing the player's FirstPersonController (movement + look).\n\nEffective Rule:\n- If ANY active request has FreezePlayerController = true => controller is frozen.\n- Otherwise => controller is unfrozen.")]
		public bool FreezePlayerController;

		[Tooltip("If true, this request contributes to setting the cursor presentation mode to FreeMouse.\n\nEffective Rule:\n- If ANY active request has UseFreeMouse = true => DynamicCursorManager mode = FreeMouse.\n- Otherwise => DynamicCursorManager mode = FPSLocked.\n\nNote:\n- This broker does not enforce any relationship between FreeMouse and FreezePlayerController.\n  (Designers are free to request FreeMouse without freezing, if desired.)")]
		public bool UseFreeMouse;

		[Tooltip("If true, this request contributes to switching PlayerInput to the UI action map.\n\nEffective Rule:\n- If ANY active request has UseUIActionMap = true => PlayerInput map = UIActionMapName.\n- Otherwise => PlayerInput map = PlayerActionMapName.\n\nImportant:\n- This broker supports exactly ONE UI map name and ONE Player map name (configured on the broker).\n- If you discover you need multiple UI maps, treat that as a separate design/wiring issue rather than adding complexity here.")]
		public bool UseUIActionMap;

		[Tooltip("NEW: If true, this request contributes to suppressing the game's *virtual cursor* and blocking world cursor interactions.\n\nWhat suppression does (via DynamicCursorManager):\n- Hides the UI cursor (UnifiedCursorUI renderer).\n- Blocks all world cursor interactions (no raycasts, hover, clicks, drags, or related events).\n- Immediately force-releases any active click aggregation and ends any captured/active drag.\n\nEffective Rule:\n- If ANY active request has HideVirtualCursorAndBlockWorld = true => suppressed.\n- Otherwise => not suppressed.\n\nUse cases:\n- Cinematics/cutscenes\n- Fullscreen fades / transitions\n- Moments where you want NO cursor feedback and NO accidental clicks/drags.\n\nImportant:\n- This does NOT change OS/system cursor settings. System cursor handling remains owned by DynamicCursorManager's existing settings.")]
		public bool HideVirtualCursorAndBlockWorld;

		[Tooltip("Optional debug label for this request (e.g., \"CameraZoneTrigger:ConsoleA\", \"DialInteractable:ReactorKnob\").\nUsed for logging and diagnostics only.\n\nSafe examples:\n- \"Zone:MainConsole\"\n- \"Cutscene:Intro\"")]
		public string DebugLabel;
	}

	public readonly struct LockHandle : IEquatable<LockHandle>
	{
		public readonly int Id;

		public readonly int BrokerInstanceId;

		[Tooltip("True if this handle looks structurally valid (non-zero id + broker instance id).")]
		public bool IsValid => false;

		public LockHandle(int id, int brokerInstanceId)
		{
			Id = 0;
			BrokerInstanceId = 0;
		}

		public bool Equals(LockHandle other)
		{
			return false;
		}

		public override bool Equals(object obj)
		{
			return false;
		}

		public override int GetHashCode()
		{
			return 0;
		}

		public static bool operator ==(LockHandle a, LockHandle b)
		{
			return false;
		}

		public static bool operator !=(LockHandle a, LockHandle b)
		{
			return false;
		}

		public override string ToString()
		{
			return null;
		}
	}

	[Header("Broker Discovery (Prefab-Friendly)")]
	[Tooltip("Unity Tag used to locate the InteractionLockBroker at runtime from any prefab/additive scene.\n\nUsage pattern:\n- Your broker object in the master scene should be tagged with this tag.\n- Other scripts can call InteractionLockBroker.TryGet(out var broker) or FindOrNull().\n\nDefault: \"LockBroker\".\n\nRules:\n- Must exist in Project Settings > Tags and Layers.\n- If the tag doesn't exist, FindGameObjectWithTag throws; this script catches it and returns null.")]
	[SerializeField]
	private string brokerTag;

	[Tooltip("If true, logs a warning when multiple brokers are found.\nThis can happen in additive scenes if you accidentally place more than one broker.\nRecommended: true during development, false in production.")]
	[SerializeField]
	private bool warnOnMultipleBrokers;

	private static readonly List<InteractionLockBroker> s_foundBrokers;

	[Header("Core References (Optional Overrides)")]
	[Tooltip("Optional explicit reference to the player's FirstPersonController.\n\nIf null and 'Enable Tag Auto-Resolve' is enabled, the broker will try to find it using 'Player Controller Tag'.\nIf still not found, it will fall back to FindObjectOfType<FirstPersonController>(true).\n\nNote:\n- This broker assumes SetFrozen(true/false) freezes BOTH movement and look in your FirstPersonController implementation.")]
	[SerializeField]
	private FirstPersonController playerController;

	[Tooltip("Optional explicit reference to the DynamicCursorManager.\n\nIf null and 'Enable Tag Auto-Resolve' is enabled, the broker will try to find it using 'Cursor Manager Tag'.\nIf still not found, it will fall back to FindObjectOfType<DynamicCursorManager>(true).\n\nThis is REQUIRED for cursor mode switching AND virtual cursor suppression; if missing, those changes are skipped.")]
	[SerializeField]
	private DynamicCursorManager dynamicCursorManager;

	[Tooltip("Optional explicit reference to the PlayerInput used for action map switching.\n\nIf null and 'Enable Tag Auto-Resolve' is enabled, the broker will try to find it using 'Player Input Tag'.\nIf still not found, it will fall back to FindObjectOfType<PlayerInput>(true).\n\nIf this is null, action map switching is skipped (but requests are still accepted).")]
	[SerializeField]
	private PlayerInput playerInput;

	[Tooltip("Optional explicit reference to the player's own virtual camera GameObject (e.g., Cinemachine vcam).\n\nThis is NOT controlled by the broker, but is tracked to support zone-aware logic in other systems.\nIf null and 'Enable Tag Auto-Resolve' is enabled, the broker will try to find it using 'Player Virtual Camera Tag'.\n\nExposed via:\n- IsPlayerVirtualCameraActive")]
	[SerializeField]
	private GameObject playerVirtualCamera;

	[Header("Tag Auto-Resolve (Master Scene Friendly)")]
	[Tooltip("If true, the broker tries to resolve missing references at runtime using Unity Tags.\nRecommended when this broker lives in a persistent/master scene and other systems may be loaded additively.\n\nExplicit Inspector references always take priority.")]
	[SerializeField]
	private bool enableTagAutoResolve;

	[Tooltip("If true, the broker will keep trying to resolve missing references periodically during Update.\nRecommended for additive scenes / runtime instantiation where dependencies may spawn later.\nIf false, auto-resolve is attempted only on Awake/Start.\n\nSafe default: true.")]
	[SerializeField]
	private bool retryResolveIfMissing;

	[Tooltip("Seconds between repeated auto-resolve attempts when retryResolveIfMissing is enabled.\nLower values resolve faster but do more work.\n\nSafe default: 0.5 seconds.")]
	[SerializeField]
	[Min(0.05f)]
	private float retryResolveIntervalSeconds;

	[Space(4f)]
	[Tooltip("Unity Tag used to locate the player's controller root.\nThe tagged object should contain (or have a child with) FirstPersonController.\n\nDefault: 'Player'.\n\nIf empty, tag lookup for the controller is skipped.")]
	[SerializeField]
	private string playerControllerTag;

	[Tooltip("Unity Tag used to locate the DynamicCursorManager.\n\nDefault: 'CursorManager'.\n\nIf empty, tag lookup for the manager is skipped.")]
	[SerializeField]
	private string cursorManagerTag;

	[Tooltip("Unity Tag used to locate the PlayerInput GameObject.\n\nDefault: 'PlayerInput'.\n\nIf empty, tag lookup for PlayerInput is skipped.")]
	[SerializeField]
	private string playerInputTag;

	[Tooltip("Unity Tag used to locate the player's virtual camera GameObject (e.g., Cinemachine vcam).\n\nDefault: 'CMCam'.\n\nIf empty, tag lookup for the virtual camera is skipped.")]
	[SerializeField]
	private string playerVirtualCameraTag;

	[Header("Action Map Switching (Optional)")]
	[Tooltip("If true, and PlayerInput is resolved, the broker will switch action maps based on active lock requests.\nIf false, action map switching is disabled even if requests ask for it.\n\nRule:\n- Any request with UseUIActionMap = true => switch to UIActionMapName\n- Otherwise => switch to PlayerActionMapName")]
	[SerializeField]
	private bool enableActionMapSwitching;

	[Tooltip("Action map name for normal gameplay.\nUsed when NO active request asks for UI action map.\n\nMust match your Input Actions asset.\n\nSafe example: \"Player\"")]
	[SerializeField]
	private string playerActionMapName;

	[Tooltip("Action map name for UI/console interaction.\nUsed when ANY active request asks for UI action map.\n\nMust match your Input Actions asset.\n\nSafe example: \"UI\"")]
	[SerializeField]
	private string uiActionMapName;

	[Header("Diagnostics")]
	[Tooltip("If true, logs acquisitions/releases and resolved state changes.\nUseful while migrating systems to the broker.\nDisable for production.")]
	[SerializeField]
	private bool logStateChanges;

	[Tooltip("If true, warns when a request asks for UI action map switching but PlayerInput is missing.\nThis helps catch setup errors in additive scenes/prefabs.")]
	[SerializeField]
	private bool warnIfActionMapRequestedButMissingPlayerInput;

	[Tooltip("If true, the broker will apply its resolved state every time requests change, even if the resolved values are unchanged.\nThis can help recover from external systems that incorrectly modify cursor/controller state.\n\nSafe default: false.")]
	[SerializeField]
	private bool forceReapplyOnEveryChange;

	private readonly Dictionary<int, LockRequest> _requests;

	private int _nextId;

	private bool _resolvedFreeze;

	private bool _resolvedUseFreeMouse;

	private bool _resolvedUseUIMap;

	private bool _resolvedHideVirtualCursorAndBlockWorld;

	private float _nextResolveAttemptTime;

	private int BrokerInstanceId => 0;

	public bool IsPlayerVirtualCameraActive => false;

	public int ActiveRequestCount => 0;

	public event Action OnRequestsChanged
	{
		[CompilerGenerated]
		add
		{
		}
		[CompilerGenerated]
		remove
		{
		}
	}

	public static bool TryGet(out InteractionLockBroker broker, string tag = "LockBroker")
	{
		broker = null;
		return false;
	}

	public static InteractionLockBroker FindOrNull(string tag = "LockBroker")
	{
		return null;
	}

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public LockHandle Acquire(LockRequest request)
	{
		return default(LockHandle);
	}

	public bool Release(LockHandle handle)
	{
		return false;
	}

	public void ReleaseAll(string reason = "ReleaseAll")
	{
	}

	public void ForceRefresh(string reason = "ForceRefresh")
	{
	}

	public InteractionLockBroker FindSelfByConfiguredTagOrNull()
	{
		return null;
	}

	public bool IsMostRecentLock(in LockHandle handle)
	{
		return false;
	}

	private void RecomputeAndApply(string reason, bool forceApply = false)
	{
	}

	private bool HasMissingReferences()
	{
		return false;
	}

	private void ResolveReferencesIfNeeded(bool force)
	{
	}

	private static GameObject FindByTagSafe(string tag)
	{
		return null;
	}

	private void WarnIfMultipleBrokersExist()
	{
	}
}
