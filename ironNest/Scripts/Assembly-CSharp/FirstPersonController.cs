using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
	public enum PlatformPivotMode
	{
		[Tooltip("Use the detected platform transform.position as the rotation pivot.")]
		PlatformTransformPosition = 0,
		[Tooltip("Use a custom pivot transform as the rotation pivot (assign Custom Platform Pivot).")]
		CustomPivotTransform = 1
	}

	private CharacterController controller;

	[Header("Camera References")]
	[Tooltip("Root transform used as the pivot for vertical (pitch) rotation. Typically the parent of the camera. If left null, the script will try to infer it from the 'Actual Main GameObject' parent at Awake().")]
	public Transform cameraRoot;

	[Tooltip("The camera GameObject (or pivot) whose local rotation may be modified by external systems. Used for optional external yaw/pitch adoption and roll lock. If left null, a child named 'MainCamera' will be searched at Awake().")]
	public GameObject actualMainGameObject;

	[Header("View Settings")]
	[Tooltip("Reference Field of View value for documentation and external systems. This script does not set Camera.fieldOfView.")]
	public float fov;

	[Tooltip("Invert vertical look input. When enabled, moving look up will look down, and vice versa.")]
	public bool invertCamera;

	[Tooltip("When enabled, look input rotates the player/camera. When disabled, view rotation is frozen.")]
	public bool cameraCanMove;

	[Tooltip("Multiplier applied to Look input (mouse delta).")]
	public float mouseSensitivity;

	[Tooltip("Additional multiplier applied to Look input (mouse delta).")]
	public float mouseSensitivityMultiplier;

	[Tooltip("Multiplier applied to Look input (right stick).")]
	public float controllerSensitivity;

	public bool invertYCamera;

	public bool invertXCamera;

	[Tooltip("Maximum absolute pitch angle (degrees) away from forward. Pitch is clamped to [-Max, +Max].")]
	public float maxLookAngle;

	[Tooltip("If enabled, the Z (roll) rotation of the Actual Main GameObject is driven toward zero each frame, keeping the horizon level.\n\nWhen this is toggled OFF (e.g. by an external sway system releasing control), the roll is NOT snapped — it is left at whatever value the external system placed it at.\n\nWhen this is toggled back ON after being OFF, the roll smoothly interpolates back to zero over the time controlled by Roll Return Speed, rather than snapping instantly.\n\nSet Roll Return Speed to a very high value (e.g. 999) if you want the old snap behaviour.")]
	public bool lockCameraRoll;

	[Tooltip("Speed (degrees per second) at which the camera roll is driven back to zero when Lock Camera Roll is enabled.\n\nThis controls the smooth return when re-enabling Lock Camera Roll after an external system (such as MechSwayController) has introduced roll on the camera pivot above this transform.\n\nNote: this does NOT affect MechSwayController's own pivot — the sway pivot is a separate GameObject above this camera. This only governs any residual roll that has leaked onto the Actual Main GameObject (CM vcam1) itself.\n\nTypical range: 60–360 deg/s. High values (999+) approximate the original snap behaviour.\nSafe example: 180")]
	[Min(0.01f)]
	public float rollReturnSpeed;

	[Tooltip("Camera Smoothing. Lower = More Smoothed but 'Lags' behind player intention")]
	public float cameraSmoothing;

	[Header("Crosshair")]
	[Tooltip("If enabled, the first UnityEngine.UI.Image found under this GameObject will be treated as a crosshair and configured. If disabled, it will be hidden.")]
	public bool crosshair;

	[Tooltip("Sprite applied to the crosshair Image when Auto Crosshair is enabled. Leave null to keep the Image's existing sprite.")]
	public Sprite crosshairImage;

	[Tooltip("Tint color applied to the crosshair Image when Auto Crosshair is enabled.")]
	public Color crosshairColor;

	private Image crosshairObject;

	private float yaw;

	private float pitch;

	private Transform mainGameObjectTransform;

	[Header("Zoom (State Only)")]
	[Tooltip("Enable zoom state toggling. This script does not change camera FOV; it only exposes IsZoomed for external camera/FOV systems.")]
	public bool enableZoom;

	[Tooltip("If enabled, zoom is active while Zoom input is held. If disabled, Zoom input toggles zoom state on press.")]
	public bool holdToZoom;

	[Tooltip("Reference target zoom FOV for external systems. This script does not set Camera.fieldOfView.")]
	public float zoomFOV;

	[Tooltip("Reference interpolation speed hint (seconds) for external systems. This script does not interpolate FOV.")]
	public float zoomStepTime;

	private bool isZoomed;

	[Header("External Rotation Adoption - Yaw")]
	[Tooltip("If enabled, any local yaw (Y) applied to the Actual Main GameObject by external systems is adopted into the player root rotation, keeping the camera local yaw near zero.")]
	public bool adoptExternalCameraYaw;

	[Tooltip("If enabled, yaw adoption is smoothed using Adopt Yaw Smoothing. If disabled, adoption is immediate (subject to threshold).")]
	public bool smoothAdoptYaw;

	[Tooltip("Maximum yaw adoption speed in degrees/second when smoothing is enabled.")]
	public float adoptYawSmoothing;

	[Tooltip("Minimum absolute yaw difference (degrees) required before adopting external yaw. Values under this are ignored (treated as zero).")]
	public float adoptYawThreshold;

	[Header("External Rotation Adoption - Pitch")]
	[Tooltip("If enabled, any local pitch (X) applied to the Actual Main GameObject by external systems is adopted into the controller pitch, keeping the camera local pitch near zero.")]
	public bool adoptExternalCameraPitch;

	[Tooltip("If enabled, pitch adoption is smoothed using Adopt Pitch Smoothing. If disabled, adoption is immediate (subject to threshold).")]
	public bool smoothAdoptPitch;

	[Tooltip("Maximum pitch adoption speed in degrees/second when smoothing is enabled.")]
	public float adoptPitchSmoothing;

	[Tooltip("Minimum absolute pitch difference (degrees) required before adopting external pitch. Values under this are ignored (treated as zero).")]
	public float adoptPitchThreshold;

	[Header("Movement")]
	[Tooltip("When enabled, movement input affects the character. When disabled, movement is frozen.")]
	public bool playerCanMove;

	[Tooltip("Base walking speed in meters/second.")]
	public float walkSpeed;

	[Tooltip("Legacy placeholder; not used by this CharacterController implementation.")]
	public float maxVelocityChange;

	private bool isWalking;

	[Header("Sprint")]
	[Tooltip("When enabled, holding Sprint input increases movement speed and drains stamina (unless Unlimited Sprint is enabled).")]
	public bool enableSprint;

	[Tooltip("If enabled, sprinting does not consume stamina and there is no cooldown.")]
	public bool unlimitedSprint;

	[Tooltip("Sprint speed in meters/second.")]
	public float sprintSpeed;

	[Tooltip("Total seconds of sprint stamina available when Unlimited Sprint is disabled.")]
	public float sprintDuration;

	[Tooltip("Seconds to wait after stamina is depleted before it starts recovering.")]
	public float sprintCooldown;

	[Tooltip("Reference sprint FOV value for external camera systems. This script does not set Camera.fieldOfView.")]
	public float sprintFOV;

	[Tooltip("Reference interpolation speed hint (seconds) for external systems. This script does not interpolate FOV.")]
	public float sprintFOVStepTime;

	[Tooltip("If enabled, shows a UI bar indicating remaining sprint stamina (requires Sprint Bar BG and Sprint Bar Image).")]
	public bool useSprintBar;

	[Tooltip("If enabled, the sprint bar fades out when full and fades in when draining (requires a CanvasGroup in children).")]
	public bool hideBarWhenFull;

	[Tooltip("Background Image for the sprint bar.")]
	public Image sprintBarBG;

	[Tooltip("Fill Image for the sprint bar.")]
	public Image sprintBar;

	[Tooltip("Sprint bar width as a percentage of screen width.")]
	[Range(0.05f, 0.9f)]
	public float sprintBarWidthPercent;

	[Tooltip("Sprint bar height as a percentage of screen height.")]
	[Range(0.005f, 0.05f)]
	public float sprintBarHeightPercent;

	private CanvasGroup sprintBarCG;

	private bool isSprinting;

	private float sprintRemaining;

	private float sprintBarWidth;

	private float sprintBarHeight;

	private bool isSprintCooldown;

	private float sprintCooldownReset;

	[Header("Jump")]
	[Tooltip("When enabled, Jump input applies an upward velocity while grounded.")]
	public bool enableJump;

	[Tooltip("Upward velocity applied when jumping.")]
	public float jumpPower;

	private float verticalVelocity;

	private bool isGrounded;

	public float GravityMultiplier;

	[Header("Crouch")]
	[Tooltip("When enabled, Crouch input lowers the camera joint and reduces movement speed.")]
	public bool enableCrouch;

	[Tooltip("If enabled, crouch is active while Crouch input is held. If disabled, Crouch input toggles crouch state.")]
	public bool holdToCrouch;

	[Tooltip("Proportional camera joint height while crouched relative to its original height. 1 = full height, 0.5 = half height.")]
	[Range(0.1f, 1f)]
	public float crouchHeight;

	[Tooltip("Multiplier applied to movement speed while crouched.")]
	[Range(0.1f, 1f)]
	public float speedReduction;

	[Tooltip("If enabled (recommended), when the controller is UNFROZEN via SetFrozen(false), the crouch state is immediately re-synced from the crouch InputAction's current pressed state.\n\nWhy this exists:\n- If inputs/action maps are disabled/swapped while interacting with UI (e.g., via InteractionLockBroker), the controller may miss the crouch 'release' event.\n- Without re-sync, the character can remain passively crouched until the player presses/releases crouch again.\n\nBehavior:\n- Hold-to-crouch only: on unfreeze, if the crouch action is NOT currently pressed, the controller stands up; if it IS pressed, it stays crouched.\n\nSafe defaults:\n- Does nothing if crouch is disabled, joint is missing, or crouch action is unassigned.")]
	[SerializeField]
	private bool resyncCrouchOnUnfreeze;

	private bool isCrouched;

	private float originalJointY;

	private float crouchedJointY;

	[Header("Head Bob")]
	[Tooltip("When enabled, applies a bobbing motion to the Camera Joint while moving.")]
	public bool enableHeadBob;

	[Tooltip("Transform to move for head bob (typically the immediate parent of the Camera).")]
	public Transform joint;

	[Tooltip("Speed of the head bob oscillation.")]
	public float bobSpeed;

	[Tooltip("Amplitude of the bob motion on each axis.")]
	public Vector3 bobAmount;

	private Vector3 jointOriginalPos;

	private float timer;

	[Header("Moving / Rotating Platforms")]
	[Tooltip("If enabled, while grounded the character is carried by the platform beneath it using platform delta translation + rotational (tangential) displacement.")]
	public bool stickToMovingPlatforms;

	[Tooltip("If enabled, while grounded the character orientation rotates with the platform's yaw (Y axis). This keeps movement input aligned with the platform as it turns.")]
	public bool rotateWithPlatformYaw;

	[Tooltip("Layers considered ground for platform detection. The platform collider MUST be on a layer included in this mask.")]
	public LayerMask groundMask;

	[Header("Platform Ground Detection")]
	[Tooltip("If enabled, uses a SphereCast near the CharacterController bottom for stable ground detection. Recommended for centered pivots and stepping.")]
	public bool useSphereCastForGround;

	[Tooltip("Extra distance (meters) beyond the CharacterController bottom used for ground probing. Increase if detection is inconsistent (typical range: 0.05–0.25).")]
	[Range(0f, 0.5f)]
	public float groundProbeExtraDistance;

	[Tooltip("Multiplier applied to CharacterController.radius to determine SphereCast radius. Slightly smaller than 1 can reduce accidental wall hits (typical range: 0.85–0.95).")]
	[Range(0.2f, 1f)]
	public float groundProbeRadiusMultiplier;

	[Tooltip("If enabled, uses the hit Rigidbody's transform as the platform root when present. Recommended when platforms are driven by a Rigidbody.")]
	public bool preferGroundRigidbodyTransform;

	[Header("Platform Rotation Carry")]
	[Tooltip("How to choose the pivot point used to compute rotational carry (tangential displacement) from a rotating platform.")]
	public PlatformPivotMode platformPivotMode;

	[Tooltip("Custom pivot transform used when Platform Pivot Mode is CustomPivotTransform. Use this if the platform rotates around an offset pivot (e.g., a hinged arm).")]
	public Transform customPlatformPivot;

	[Tooltip("If enabled, applies tangential displacement from platform rotation even if the platform does not translate (delta position is zero). Disable only if you want rotation to affect orientation but not carry the player around.")]
	public bool applyRotationalCarry;

	private Transform currentGround;

	private Vector3 lastGroundPos;

	private Quaternion lastGroundRot;

	private Vector3 platformMotionThisFrame;

	[Header("Input Actions (New Input System)")]
	[Tooltip("Vector2 action for movement input (e.g., WASD / left stick). Must be assigned; this script does not create bindings.")]
	public InputActionReference moveActionRef;

	[Tooltip("Vector2 action for look input (e.g., mouse delta / right stick). Must be assigned; this script does not create bindings.")]
	public InputActionReference lookActionRef;

	[Tooltip("Button action for jump input. Must be assigned; this script does not create bindings.")]
	public InputActionReference jumpActionRef;

	[Tooltip("Button action for sprint input. Must be assigned; this script does not create bindings.")]
	public InputActionReference sprintActionRef;

	[Tooltip("Button action for crouch input. Must be assigned; this script does not create bindings.")]
	public InputActionReference crouchActionRef;

	[Tooltip("Button (or axis-as-button) action for zoom input. Must be assigned; this script does not create bindings.")]
	public InputActionReference zoomActionRef;

	private InputAction moveAction;

	private InputAction lookAction;

	private InputAction jumpAction;

	private InputAction sprintAction;

	private InputAction crouchAction;

	private InputAction zoomAction;

	private Vector2 smoothedLook;

	private float standingHeight;

	private Vector3 standingCenter;

	[SerializeField]
	private DynamicCursorManager cursorManager;

	public bool IsZoomed => false;

	public event Action OnJump
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

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void ResolveInputActions()
	{
	}

	private void EnableInputActions()
	{
	}

	private void DisableInputActions()
	{
	}

	private void ResolveGameObjectReferences()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void HandleCrouch()
	{
	}

	private void SetCrouched(bool crouched)
	{
	}

	private void HandleMovement()
	{
	}

	private void UpdateMovingPlatformMotion()
	{
	}

	private void HandleSprint()
	{
	}

	private void HandleJump()
	{
	}

	private void HandleCamera()
	{
	}

	private void AdoptExternalCameraYawIfNeeded()
	{
	}

	private void AdoptExternalCameraPitchIfNeeded()
	{
	}

	private void HeadBob()
	{
	}

	private float Normalize180To360(float angleSigned)
	{
		return 0f;
	}

	public void SetPitch(float pitch)
	{
	}

	public void SetFrozen(bool frozen)
	{
	}

	private void ResyncHoldCrouchFromInputIfNeeded()
	{
	}

	private Vector3 ResolvePlatformPivotWorld(Transform ground)
	{
		return default(Vector3);
	}

	private bool TryGetGroundHit(out RaycastHit hit)
	{
		hit = default(RaycastHit);
		return false;
	}
}
