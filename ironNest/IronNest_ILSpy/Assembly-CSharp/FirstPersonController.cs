using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FirstPersonController : MonoBehaviour
{
	public enum PlatformPivotMode
	{
		PlatformTransformPosition,
		CustomPivotTransform
	}

	private CharacterController controller;

	public Transform cameraRoot;

	public GameObject actualMainGameObject;

	public float fov;

	public bool invertCamera;

	public bool cameraCanMove;

	public float mouseSensitivity;

	public float mouseSensitivityMultiplier;

	public float controllerSensitivity;

	public bool invertYCamera;

	public bool invertXCamera;

	public float maxLookAngle;

	public bool lockCameraRoll;

	public float rollReturnSpeed;

	public float cameraSmoothing;

	public bool crosshair;

	public Sprite crosshairImage;

	public Color crosshairColor;

	private Image crosshairObject;

	private float yaw;

	private float pitch;

	private Transform mainGameObjectTransform;

	public bool enableZoom;

	public bool holdToZoom;

	public float zoomFOV;

	public float zoomStepTime;

	private bool isZoomed;

	public bool adoptExternalCameraYaw;

	public bool smoothAdoptYaw;

	public float adoptYawSmoothing;

	public float adoptYawThreshold;

	public bool adoptExternalCameraPitch;

	public bool smoothAdoptPitch;

	public float adoptPitchSmoothing;

	public float adoptPitchThreshold;

	public bool playerCanMove;

	public float walkSpeed;

	public float maxVelocityChange;

	private bool isWalking;

	public bool enableSprint;

	public bool unlimitedSprint;

	public float sprintSpeed;

	public float sprintDuration;

	public float sprintCooldown;

	public float sprintFOV;

	public float sprintFOVStepTime;

	public bool useSprintBar;

	public bool hideBarWhenFull;

	public Image sprintBarBG;

	public Image sprintBar;

	public float sprintBarWidthPercent;

	public float sprintBarHeightPercent;

	private CanvasGroup sprintBarCG;

	private bool isSprinting;

	private float sprintRemaining;

	private float sprintBarWidth;

	private float sprintBarHeight;

	private bool isSprintCooldown;

	private float sprintCooldownReset;

	public bool enableJump;

	public float jumpPower;

	private float verticalVelocity;

	private bool isGrounded;

	public float GravityMultiplier;

	public bool enableCrouch;

	public bool holdToCrouch;

	public float crouchHeight;

	public float speedReduction;

	private bool resyncCrouchOnUnfreeze;

	private bool isCrouched;

	private float originalJointY;

	private float crouchedJointY;

	public bool enableHeadBob;

	public Transform joint;

	public float bobSpeed;

	public Vector3 bobAmount;

	private Vector3 jointOriginalPos;

	private float timer;

	public bool stickToMovingPlatforms;

	public bool rotateWithPlatformYaw;

	public LayerMask groundMask;

	public bool useSphereCastForGround;

	public float groundProbeExtraDistance;

	public float groundProbeRadiusMultiplier;

	public bool preferGroundRigidbodyTransform;

	public PlatformPivotMode platformPivotMode;

	public Transform customPlatformPivot;

	public bool applyRotationalCarry;

	private Transform currentGround;

	private Vector3 lastGroundPos;

	private Quaternion lastGroundRot;

	private Vector3 platformMotionThisFrame;

	public InputActionReference moveActionRef;

	public InputActionReference lookActionRef;

	public InputActionReference jumpActionRef;

	public InputActionReference sprintActionRef;

	public InputActionReference crouchActionRef;

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

	private DynamicCursorManager cursorManager;

	private Action m_OnJump;

	public bool IsZoomed => isZoomed;

	public event Action OnJump
	{
		add
		{
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Expected O, but got Unknown
			object obj = this + 600;
			Delegate obj2 = this.m_OnJump;
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
			object obj = this + 600;
			Delegate obj2 = this.m_OnJump;
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

	private void Awake()
	{
		//IL_0078: Expected O, but got F4
		//IL_0094: Expected F4, but got I
		//IL_012a: Expected O, but got F4
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		CharacterController characterController = default(CharacterController);
		controller = characterController;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696150");
		Image image = default(Image);
		crosshairObject = image;
		ResolveGameObjectReferences();
		if (joint != null)
		{
			Vector3 localPosition = joint.localPosition;
			float num = 1f - crouchHeight;
			jointOriginalPos = (Vector3)localPosition.x;
			_ = localPosition.z;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (FirstPersonController)+16C]");
			originalJointY = 0f;
			float num2 = num;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (FirstPersonController)+16C]");
			float num3 = num2 * 0f;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (FirstPersonController)+16C]");
			float num4 = 0f - num3;
			crouchedJointY = num4;
		}
		if (!unlimitedSprint)
		{
			sprintRemaining = sprintDuration;
			sprintCooldownReset = sprintCooldown;
		}
		float height = controller.height;
		standingHeight = height;
		Vector3 center = controller.center;
		standingCenter = (Vector3)center.x;
		_ = center.z;
	}

	private void OnEnable()
	{
		ResolveInputActions();
		if (moveAction != null)
		{
			moveAction.Enable();
		}
		if (lookAction != null)
		{
			lookAction.Enable();
		}
		if (jumpAction != null)
		{
			jumpAction.Enable();
		}
		if (sprintAction != null)
		{
			sprintAction.Enable();
		}
		if (crouchAction != null)
		{
			crouchAction.Enable();
		}
		if (zoomAction != null)
		{
			zoomAction.Enable();
		}
	}

	private void OnDisable()
	{
		if (moveAction != null)
		{
			moveAction.Disable();
		}
		if (lookAction != null)
		{
			lookAction.Disable();
		}
		if (jumpAction != null)
		{
			jumpAction.Disable();
		}
		if (sprintAction != null)
		{
			sprintAction.Disable();
		}
		if (crouchAction != null)
		{
			crouchAction.Disable();
		}
		if (zoomAction != null)
		{
			zoomAction.Disable();
		}
	}

	private void ResolveInputActions()
	{
		InputAction inputAction = ((!(moveActionRef != null)) ? null : moveActionRef.action);
		moveAction = inputAction;
		InputAction inputAction2 = ((!(lookActionRef != null)) ? null : lookActionRef.action);
		lookAction = inputAction2;
		InputAction inputAction3 = ((!(jumpActionRef != null)) ? null : jumpActionRef.action);
		jumpAction = inputAction3;
		InputAction inputAction4 = ((!(sprintActionRef != null)) ? null : sprintActionRef.action);
		sprintAction = inputAction4;
		InputAction inputAction5 = ((!(crouchActionRef != null)) ? null : crouchActionRef.action);
		crouchAction = inputAction5;
		bool flag = zoomActionRef != null;
		bool flag2 = !flag;
		InputAction inputAction6 = null;
		if (!flag2)
		{
			InputAction action = zoomActionRef.action;
			inputAction6 = action;
		}
		zoomAction = inputAction6;
	}

	private void EnableInputActions()
	{
		if (moveAction != null)
		{
			moveAction.Enable();
		}
		if (lookAction != null)
		{
			lookAction.Enable();
		}
		if (jumpAction != null)
		{
			jumpAction.Enable();
		}
		if (sprintAction != null)
		{
			sprintAction.Enable();
		}
		if (crouchAction != null)
		{
			crouchAction.Enable();
		}
		if (zoomAction != null)
		{
			zoomAction.Enable();
		}
	}

	private void DisableInputActions()
	{
		if (moveAction != null)
		{
			moveAction.Disable();
		}
		if (lookAction != null)
		{
			lookAction.Disable();
		}
		if (jumpAction != null)
		{
			jumpAction.Disable();
		}
		if (sprintAction != null)
		{
			sprintAction.Disable();
		}
		if (crouchAction != null)
		{
			crouchAction.Disable();
		}
		if (zoomAction != null)
		{
			zoomAction.Disable();
		}
	}

	private void ResolveGameObjectReferences()
	{
		Transform transform4;
		if (actualMainGameObject == null)
		{
			Transform transform = base.transform;
			Transform transform2 = transform.Find("MainCamera");
			bool flag = transform2 != null;
			bool flag2 = !flag;
			Transform transform3 = null;
			if (!flag2)
			{
				transform3 = transform2;
			}
			transform4 = transform3;
		}
		else
		{
			transform4 = actualMainGameObject.transform;
		}
		mainGameObjectTransform = transform4;
		if (cameraRoot == null && mainGameObjectTransform != null)
		{
			Transform parent = mainGameObjectTransform.parent;
			cameraRoot = parent;
		}
		if (mainGameObjectTransform != null)
		{
			if (cameraRoot == null)
			{
				Debug.LogWarning("[FirstPersonController] Camera Root not assigned / inferred (Main GameObject has no parent). Pitch adoption may not work as intended.");
			}
		}
		else
		{
			Debug.LogWarning("[FirstPersonController] No GameObject found. Assign 'Actual Main GameObject'.");
		}
	}

	private unsafe void Start()
	{
		//IL_0086: Expected O, but got Ref
		if (crosshair && crosshairObject != null)
		{
			if (crosshairImage != null)
			{
				crosshairObject.sprite = crosshairImage;
			}
			object obj = default(object);
			crosshairObject.color = (Color)(&obj);
		}
		else if (crosshairObject != null)
		{
			GameObject gameObject = crosshairObject.gameObject;
			gameObject.SetActive(value: false);
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696150");
		CanvasGroup canvasGroup = default(CanvasGroup);
		sprintBarCG = canvasGroup;
		if (useSprintBar && sprintBarBG != null && sprintBar != null)
		{
			GameObject gameObject2 = sprintBarBG.gameObject;
			gameObject2.SetActive(value: true);
			GameObject gameObject3 = sprintBar.gameObject;
			gameObject3.SetActive(value: true);
			int width = Screen.width;
			int height = Screen.height;
			float num = (float)width * sprintBarWidthPercent;
			sprintBarWidth = num;
			float num2 = (float)height * sprintBarHeightPercent;
			sprintBarHeight = num2;
			RectTransform rectTransform = sprintBarBG.rectTransform;
			Vector2 sizeDelta = default(Vector2);
			rectTransform.sizeDelta = sizeDelta;
			RectTransform rectTransform2 = sprintBar.rectTransform;
			rectTransform2.sizeDelta = sizeDelta;
			if (hideBarWhenFull && sprintBarCG != null)
			{
				sprintBarCG.alpha = 0f;
			}
		}
		else
		{
			if (sprintBarBG != null)
			{
				GameObject gameObject4 = sprintBarBG.gameObject;
				gameObject4.SetActive(value: false);
			}
			if (sprintBar != null)
			{
				GameObject gameObject5 = sprintBar.gameObject;
				gameObject5.SetActive(value: false);
			}
		}
	}

	private void Update()
	{
		HandleCamera();
		HandleMovement();
		HandleSprint();
		if (enableJump && jumpAction != null && isGrounded && jumpAction.WasPerformedThisFrame())
		{
			Action onJump = this.m_OnJump;
			verticalVelocity = jumpPower;
			if (this.m_OnJump != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v72.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
			}
		}
		HandleCrouch();
		if (enableHeadBob)
		{
			HeadBob();
		}
	}

	private void HandleCrouch()
	{
		//IL_00b3: Invalid comparison between I4 and F4
		//IL_00fe: Expected F4, but got I4
		if (!enableCrouch || crouchAction == null)
		{
			return;
		}
		bool flag = !isCrouched;
		float num = standingHeight;
		if (!flag)
		{
			num *= crouchHeight;
		}
		float height = controller.height;
		float deltaTime = Time.deltaTime;
		float num2 = deltaTime * 12f;
		if (!(0f > num2))
		{
			if (num2 > 1f)
			{
				num2 = 1f;
			}
		}
		else
		{
			num2 = 0f;
		}
		float num3 = num - height;
		float num4 = num3 * num2;
		float height2 = num4 + height;
		controller.height = height2;
		if (!holdToCrouch)
		{
			if (crouchAction.WasPerformedThisFrame())
			{
				bool crouched = !isCrouched;
				SetCrouched(crouched);
			}
			return;
		}
		CharacterController characterController;
		float height3;
		if (!crouchAction.WasPerformedThisFrame())
		{
			if (!crouchAction.WasReleasedThisFrame())
			{
				return;
			}
			isCrouched = false;
			characterController = controller;
			height3 = standingHeight;
		}
		else
		{
			isCrouched = true;
			height3 = standingHeight * crouchHeight;
			characterController = controller;
		}
		characterController.height = height3;
	}

	private void SetCrouched(bool crouched)
	{
		float num = standingHeight;
		isCrouched = crouched;
		if (crouched)
		{
			num *= crouchHeight;
		}
		controller.height = num;
	}

	private unsafe void HandleMovement()
	{
		//IL_074c: Expected I, but got O
		//IL_0775: Expected O, but got I
		//IL_0153: Expected O, but got Ref
		//IL_023e: Invalid comparison between I4 and F4
		//IL_0103: Invalid comparison between F4 and I4
		//IL_0289: Expected F4, but got I4
		//IL_033a: Expected O, but got Ref
		//IL_02ce: Invalid comparison between I4 and F4
		//IL_0580: Invalid comparison between I4 and F4
		//IL_05cd: Expected F4, but got I4
		//IL_0402: Invalid comparison between I4 and F4
		//IL_069d: Invalid comparison between I4 and F4
		//IL_04e0: Invalid comparison between I4 and F4
		//IL_06e9: Expected F4, but got I4
		//IL_052b: Expected F4, but got I4
		UpdateMovingPlatformMotion();
		if (!playerCanMove)
		{
			return;
		}
		Vector2 vector;
		object obj;
		object obj2 = default(object);
		if (moveAction != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807015E0");
			Vector2 vector2 = default(Vector2);
			vector = vector2;
			obj = obj2;
		}
		else
		{
			nint num = (nint)typeof(Vector2);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v283 @ rax_v40 (Il2CppClass<UnityEngine.Vector2>)+B8]");
			nint num2 = 0;
			vector = Vector2.zeroVector;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v284 @ rcx_v38 (Il2CppStaticFields<UnityEngine.Vector2>)+4]");
			obj = 0;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180503BF6h\"");
		bool flag;
		if ((object)vector == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180503BF6h\"");
			if (obj == null)
			{
				flag = false;
				goto IL_077a;
			}
		}
		flag = isGrounded;
		goto IL_077a;
		IL_086c:
		CanvasGroup canvasGroup;
		float alpha;
		canvasGroup.alpha = alpha;
		return;
		IL_0530:
		isSprinting = false;
		if (!unlimitedSprint)
		{
			float deltaTime = Time.deltaTime;
			float num3 = deltaTime + sprintRemaining;
			if (!(0f > num3))
			{
				if (num3 > sprintDuration)
				{
					num3 = sprintDuration;
				}
			}
			else
			{
				num3 = 0f;
			}
			sprintRemaining = num3;
		}
		if (!hideBarWhenFull || unlimitedSprint || !(sprintBarCG != null))
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
		object obj3 = default(object);
		if (obj3 == null)
		{
			return;
		}
		canvasGroup = sprintBarCG;
		float alpha2 = sprintBarCG.alpha;
		float deltaTime2 = Time.deltaTime;
		float num4 = deltaTime2 * 3f;
		float num5 = alpha2 - num4;
		if (!(0f > num5))
		{
			if (num5 > 1f)
			{
				alpha = 1f;
				goto IL_086c;
			}
		}
		else
		{
			num5 = 0f;
		}
		alpha = num5;
		goto IL_086c;
		IL_077a:
		bool flag2 = !flag;
		bool flag3 = !flag2;
		isWalking = flag3;
		bool flag5;
		if (enableSprint && sprintAction != null && sprintAction.IsPressed() && (unlimitedSprint || sprintRemaining > 0f))
		{
			bool flag4 = !isSprintCooldown;
			flag5 = flag4;
		}
		else
		{
			flag5 = false;
		}
		if (!flag5 || !isCrouched)
		{
			Transform transform = base.transform;
			float x = default(float);
			Vector3 vector3 = transform.TransformDirection((Vector3)(&x));
			bool flag6 = controller.isGrounded;
			if (!flag6)
			{
				isGrounded = flag6;
				Vector3 gravity = Physics.gravity;
				float deltaTime3 = Time.deltaTime;
				float num6 = deltaTime3 * gravity.y;
				float num7 = num6 * GravityMultiplier;
				float num8 = num7 + verticalVelocity;
				verticalVelocity = num8;
				x = vector3.x;
			}
			else
			{
				isGrounded = true;
				Vector3 velocity = controller.velocity;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
				float num9 = velocity.x / sprintSpeed;
				if (!(0f > num9))
				{
					if (num9 > 1f)
					{
						num9 = 1f;
					}
				}
				else
				{
					num9 = 0f;
				}
				bool flag7 = controller.isGrounded;
				bool flag8 = !flag7;
				x = velocity.x;
				if (!flag8)
				{
					bool flag9 = 0f < verticalVelocity;
					x = velocity.x;
					if (!flag9)
					{
						float num10 = num9 * -6f;
						float num11 = num10 - 2f;
						verticalVelocity = num11;
						x = velocity.x;
					}
				}
			}
			float deltaTime4 = Time.deltaTime;
			float num12 = deltaTime4 * verticalVelocity;
			float num13 = num12 + (float)obj2;
			CollisionFlags collisionFlags = controller.Move((Vector3)(&x));
			if (!flag5)
			{
				goto IL_0530;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180503E8Fh\"");
			if ((object)vector == null)
			{
				bool flag10 = obj == null;
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180503E8Fh\"");
				if (flag10)
				{
					goto IL_0530;
				}
			}
			isSprinting = true;
			if (!unlimitedSprint)
			{
				float deltaTime5 = Time.deltaTime;
				if (!(0f < (sprintRemaining -= deltaTime5)))
				{
					sprintRemaining = 0f;
					isSprinting = false;
					isSprintCooldown = true;
				}
			}
			if (!hideBarWhenFull || unlimitedSprint || !(sprintBarCG != null))
			{
				return;
			}
			canvasGroup = sprintBarCG;
			float alpha3 = sprintBarCG.alpha;
			float deltaTime6 = Time.deltaTime;
			float num14 = deltaTime6 * 5f;
			float num15 = num14 + alpha3;
			if (!(0f > num15))
			{
				if (num15 > 1f)
				{
					num15 = 1f;
				}
			}
			else
			{
				num15 = 0f;
			}
			alpha = num15;
			goto IL_086c;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Warning: Method ends with non empty stack (-C8), the output could be wrong!");
		/*Error: End of method reached without returning.*/;
	}

	private unsafe void UpdateMovingPlatformMotion()
	{
		//IL_0008: Expected O, but got Ref
		//IL_06ec: Expected I, but got O
		//IL_0106: Expected O, but got Ref
		//IL_0097: Expected O, but got Ref
		//IL_00de: Expected O, but got Ref
		//IL_06a2: Expected O, but got F4
		//IL_06c8: Expected O, but got F4
		//IL_0504: Expected O, but got Ref
		//IL_055c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0561: Expected O, but got Unknown
		//IL_056a: Invalid comparison between O and F4
		//IL_043e: Expected O, but got Ref
		//IL_044c: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		bool flag = !stickToMovingPlatforms;
		nint num = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v58 @ rax_v4 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num2 = 0;
		platformMotionThisFrame = Vector3.zeroVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ rcx_v3 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		_ = 0;
		Collider collider;
		if (!flag && controller.isGrounded && TryGetGroundHit(out System.Runtime.CompilerServices.Unsafe.As<object, RaycastHit>(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 57))))
		{
			if (preferGroundRigidbodyTransform)
			{
				RaycastHit raycastHit = (RaycastHit)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 57));
				Rigidbody rigidbody = ((RaycastHit*)raycastHit)->rigidbody;
				if (rigidbody != null)
				{
					RaycastHit raycastHit2 = (RaycastHit)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 57));
					Rigidbody rigidbody2 = ((RaycastHit*)raycastHit2)->rigidbody;
					collider = (Collider)(object)rigidbody2;
					goto IL_0118;
				}
			}
			RaycastHit raycastHit3 = (RaycastHit)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 57));
			collider = ((RaycastHit*)raycastHit3)->collider;
			goto IL_0118;
		}
		currentGround = null;
		return;
		IL_0118:
		Transform transform = collider.transform;
		if (currentGround == transform)
		{
			Vector3 position = transform.position;
			_ = position.x;
			_ = lastGroundPos;
			_ = platformMotionThisFrame;
			float num3 = position.z;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (FirstPersonController)+1B8]");
			float num4 = num3 - 0f;
			float num5 = num4;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (FirstPersonController)+1D4]");
			float num6 = num5 + 0f;
			Vector3 vector = default(Vector3);
			platformMotionThisFrame = vector;
			Quaternion rotation = transform.rotation;
			ref Quaternion rotation2 = ref System.Runtime.CompilerServices.Unsafe.As<object, Quaternion>(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 89));
			_ = lastGroundRot;
			Quaternion quaternion = Quaternion.Internal_Inverse(ref rotation2);
			float num7 = (float)vector * quaternion.x;
			float num8 = rotation.x * (float)vector;
			object obj3 = (object)vector * (object)vector;
			float num9 = num8 + num7;
			object obj4 = (object)vector * (object)vector;
			object obj5 = (object)vector * (object)vector;
			float num10 = num9 + (float)obj4;
			object obj6 = (object)vector * (object)vector;
			float num11 = num10 - (float)obj6;
			object obj7 = (object)vector * (object)vector;
			object obj8 = obj3 + obj7;
			float num12 = rotation.x * (float)vector;
			float num13 = (float)vector * quaternion.x;
			float num14 = (float)obj8 + num13;
			float num15 = num14 - num12;
			bool flag2 = !applyRotationalCarry;
			object obj9 = (object)vector * (object)vector;
			object obj10 = (object)vector * (object)vector;
			float num16 = rotation.x * quaternion.x;
			object obj11 = obj5 + obj10;
			float num17 = rotation.x * (float)vector;
			object obj12 = (object)vector * (object)vector;
			float num18 = (float)obj9 - num16;
			float num19 = (float)vector * quaternion.x;
			float num20 = (float)obj11 + num17;
			object obj13 = (object)vector * (object)vector;
			float num21 = num20 - num19;
			float num22 = num18 - (float)obj13;
			float num23 = num22 - (float)obj12;
			if (!flag2)
			{
				Transform transform2 = ((platformPivotMode != PlatformPivotMode.CustomPivotTransform || !(customPlatformPivot != null)) ? transform : customPlatformPivot);
				Vector3 position2 = transform2.position;
				_ = position2.x;
				Transform transform3 = base.transform;
				Vector3 position3 = transform3.position;
				Vector3 vector2 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 121));
				Quaternion quaternion2 = (Quaternion)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 105));
				_ = position3.x;
				float num24 = position3.z - position2.z;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-49]");
				_ = 0;
				float num25 = (quaternion2 * vector2).z - num24;
				float num26 = num25;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (FirstPersonController)+1D4]");
				float num27 = num26 + 0f;
				platformMotionThisFrame = vector;
			}
			if (rotateWithPlatformYaw)
			{
				Vector3 vector3 = Quaternion.Internal_ToEulerRad(ref System.Runtime.CompilerServices.Unsafe.As<object, Quaternion>(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 73)));
				Vector3 euler = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 121));
				float num28 = vector3.z * 57.29578f;
				float num29 = Mathf.DeltaAngle(0f, Quaternion.Internal_MakePositive(euler).y);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				object obj14 = num29 & 0;
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj14) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)0.0001f))
				{
					Transform transform4 = base.transform;
					Space relativeTo = default(Space);
					transform4.Rotate(0f, num29, 0f, relativeTo);
					Transform transform5 = base.transform;
					yaw = transform5.localEulerAngles.y;
				}
			}
		}
		else
		{
			currentGround = transform;
		}
		Vector3 position4 = transform.position;
		lastGroundPos = (Vector3)position4.x;
		_ = position4.z;
		lastGroundRot = (Quaternion)transform.rotation.x;
	}

	private unsafe void HandleSprint()
	{
		//IL_004a: Invalid comparison between I4 and F4
		//IL_0165: Invalid comparison between I4 and F4
		//IL_00f8: Invalid comparison between I4 and F4
		//IL_0099: Expected F4, but got I4
		//IL_01e4: Invalid comparison between F4 and I4
		//IL_0217: Expected O, but got Ref
		if (!enableSprint)
		{
			return;
		}
		if (!isSprinting)
		{
			float deltaTime = Time.deltaTime;
			float num = deltaTime + sprintRemaining;
			if (!(0f > num))
			{
				if (num > sprintDuration)
				{
					sprintRemaining = sprintDuration;
					goto IL_024a;
				}
			}
			else
			{
				num = 0f;
			}
			sprintRemaining = num;
		}
		else
		{
			isZoomed = false;
			if (!unlimitedSprint)
			{
				float deltaTime2 = Time.deltaTime;
				if (!(0f < (sprintRemaining -= deltaTime2)))
				{
					isSprinting = false;
					isSprintCooldown = true;
				}
			}
		}
		goto IL_024a;
		IL_024a:
		if (!isSprintCooldown)
		{
			sprintCooldown = sprintCooldownReset;
		}
		else
		{
			float deltaTime3 = Time.deltaTime;
			if (!(0f < (sprintCooldown -= deltaTime3)))
			{
				isSprintCooldown = false;
			}
		}
		if (useSprintBar && !unlimitedSprint && sprintBar != null)
		{
			if (sprintDuration > 0f)
			{
			}
			Transform transform = sprintBar.transform;
			object obj = default(object);
			transform.localScale = (Vector3)(&obj);
		}
	}

	private void HandleJump()
	{
		if (enableJump && jumpAction != null && isGrounded && jumpAction.WasPerformedThisFrame())
		{
			Action onJump = this.m_OnJump;
			verticalVelocity = jumpPower;
			if (this.m_OnJump != null)
			{
				IntPtr invoke_impl = ((Delegate)onJump).invoke_impl;
				IntPtr method = ((Delegate)onJump).method;
				IntPtr method_code = ((Delegate)onJump).method_code;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v76 @ rax_v4 (System.IntPtr) (should have been resolved before IL gen)");
			}
		}
	}

	private unsafe void HandleCamera()
	{
		//IL_01d3: Invalid comparison between I4 and F4
		//IL_01f4: Expected O, but got Ref
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Expected O, but got Unknown
		//IL_0261: Invalid comparison between O and F4
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Expected O, but got Unknown
		//IL_0383: Invalid comparison between F4 and O
		//IL_0497: Invalid comparison between I4 and F4
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Expected O, but got Unknown
		//IL_03bb: Invalid comparison between F4 and I4
		//IL_010b: Expected F4, but got I4
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Expected O, but got Unknown
		//IL_062b: Expected O, but got Ref
		//IL_05f8: Invalid comparison between I4 and F4
		//IL_04ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cf: Expected O, but got Unknown
		//IL_0533: Expected O, but got F4
		//IL_011d: Expected O, but got Ref
		bool flag = mainGameObjectTransform == null;
		if (flag)
		{
			return;
		}
		float x = default(float);
		if (cameraCanMove != flag)
		{
			float num = ((!cursorManager.IsCurrentDeviceGamepad()) ? (mouseSensitivityMultiplier * mouseSensitivity) : controllerSensitivity);
			if (lookAction != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807015E0");
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180371660");
			}
			bool flag2 = !invertYCamera;
			object obj2 = default(object);
			object obj = obj2;
			if (!flag2)
			{
				obj = obj2 ^ -0f;
			}
			bool flag3 = !invertXCamera;
			object obj4 = default(object);
			object obj3 = obj4;
			if (!flag3)
			{
				obj3 = obj4 ^ -0f;
			}
			float deltaTime = Time.deltaTime;
			float num2 = deltaTime * cameraSmoothing;
			if (!(0f > num2))
			{
				if (num2 > 1f)
				{
					num2 = 1f;
				}
			}
			else
			{
				num2 = 0f;
			}
			object obj5 = obj3 - (object)smoothedLook;
			object obj6 = obj;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (FirstPersonController)+23C]");
			object obj7 = obj6 - 0;
			float num3 = (float)obj5 * num2;
			float num4 = (float)obj7 * num2;
			float num5 = num3 + (float)smoothedLook;
			float num6 = num4;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (FirstPersonController)+23C]");
			float num7 = num6 + 0f;
			float num8 = num * num5;
			smoothedLook = (Vector2)num5;
			float num9 = num8 + yaw;
			yaw = num9;
			Transform transform = base.transform;
			transform.localEulerAngles = (Vector3)(&x);
			if (!invertCamera)
			{
				num ^= -0f;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (FirstPersonController)+23C]");
			float num10 = 0f * num;
			float num11 = maxLookAngle ^ -0f;
			float num12 = num10 + pitch;
			if (!(num11 > num12))
			{
				if (num12 > maxLookAngle)
				{
					num12 = maxLookAngle;
				}
			}
			else
			{
				num12 = num11;
			}
			pitch = num12;
		}
		if (cameraRoot != null)
		{
			Vector3 localEulerAngles = cameraRoot.localEulerAngles;
			if (0f > pitch)
			{
			}
			cameraRoot.localEulerAngles = (Vector3)(&x);
		}
		if (lockCameraRoll)
		{
			Vector3 localEulerAngles2 = mainGameObjectTransform.localEulerAngles;
			float num13 = Mathf.DeltaAngle(0f, localEulerAngles2.z);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj8 = num13 & 0;
			Transform transform2;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj8) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)0.001f))
			{
				transform2 = mainGameObjectTransform;
				x = localEulerAngles2.x;
			}
			else
			{
				float deltaTime2 = Time.deltaTime;
				float num14 = 0f - num13;
				float num15 = deltaTime2 * rollReturnSpeed;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				object obj9 = num14 & 0;
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num15) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj9))
				{
					float num16 = 0f - num13;
					bool flag4 = !(num16 < 0f);
					float num17 = 1f;
					if (!flag4)
					{
						num17 = -1f;
					}
					float num18 = num17 * num15;
					float num19 = num18 + num13;
					if (!(0f > num19))
					{
					}
				}
				transform2 = mainGameObjectTransform;
				x = localEulerAngles2.x;
			}
			transform2.localEulerAngles = (Vector3)(&x);
		}
		if (enableZoom && zoomAction != null)
		{
			bool flag6;
			if (!holdToZoom)
			{
				if (!zoomAction.WasPerformedThisFrame())
				{
					goto IL_05c0;
				}
				bool flag5 = !isZoomed;
				flag6 = flag5;
			}
			else
			{
				flag6 = zoomAction.IsPressed();
			}
			isZoomed = flag6;
		}
		goto IL_05c0;
		IL_05c0:
		AdoptExternalCameraYawIfNeeded();
		AdoptExternalCameraPitchIfNeeded();
	}

	private unsafe void AdoptExternalCameraYawIfNeeded()
	{
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Expected O, but got Unknown
		//IL_0072: Invalid comparison between F4 and O
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Expected F4, but got Unknown
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Expected O, but got Unknown
		//IL_01b5: Invalid comparison between F4 and O
		//IL_01f0: Invalid comparison between I4 and F4
		//IL_0211: Expected O, but got Ref
		//IL_01d2: Expected F4, but got I4
		if (!adoptExternalCameraYaw || !(mainGameObjectTransform != null))
		{
			return;
		}
		float num = Mathf.DeltaAngle(0f, mainGameObjectTransform.localEulerAngles.y);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj = num & 0;
		float num2 = adoptYawThreshold;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num2) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj))
		{
			return;
		}
		bool flag = !smoothAdoptYaw;
		float num3 = num;
		if (!flag)
		{
			float deltaTime = Time.deltaTime;
			float num4 = deltaTime * adoptYawSmoothing;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
			float num5 = num4 ^ 0;
			if (!(num5 > num))
			{
				bool flag2 = !(num > num4);
				num3 = num;
				if (!flag2)
				{
					num3 = num4;
				}
			}
			else
			{
				num3 = num5;
			}
		}
		Transform transform = base.transform;
		Space relativeTo = default(Space);
		transform.Rotate(0f, num3, 0f, relativeTo);
		float num6 = Mathf.DeltaAngle(0f, mainGameObjectTransform.localEulerAngles.y);
		float num7 = num6 - num3;
		float num8 = num7;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj2 = num8 & 0;
		float num9 = adoptYawThreshold;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num9) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2))
		{
			num7 = 0f;
		}
		Vector3 localEulerAngles = mainGameObjectTransform.localEulerAngles;
		if (0f > num7)
		{
		}
		float num10 = default(float);
		mainGameObjectTransform.localEulerAngles = (Vector3)(&num10);
		Transform transform2 = base.transform;
		yaw = transform2.localEulerAngles.y;
	}

	private unsafe void AdoptExternalCameraPitchIfNeeded()
	{
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Expected O, but got Unknown
		//IL_0095: Invalid comparison between F4 and O
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Expected F4, but got Unknown
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Expected F4, but got Unknown
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Expected O, but got Unknown
		//IL_01f5: Invalid comparison between F4 and O
		//IL_022f: Invalid comparison between I4 and F4
		//IL_026a: Expected O, but got Ref
		//IL_0212: Expected F4, but got I4
		//IL_028a: Invalid comparison between I4 and F4
		//IL_02ab: Expected O, but got Ref
		if (!adoptExternalCameraPitch || !(mainGameObjectTransform != null) || !(cameraRoot != null))
		{
			return;
		}
		float num = Mathf.DeltaAngle(0f, mainGameObjectTransform.localEulerAngles.x);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj = num & 0;
		float num2 = adoptPitchThreshold;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num2) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj))
		{
			return;
		}
		bool flag = !smoothAdoptPitch;
		float num3 = num;
		if (!flag)
		{
			float deltaTime = Time.deltaTime;
			float num4 = deltaTime * adoptPitchSmoothing;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
			float num5 = num4 ^ 0;
			if (!(num5 > num))
			{
				bool flag2 = !(num > num4);
				num3 = num;
				if (!flag2)
				{
					num3 = num4;
				}
			}
			else
			{
				num3 = num5;
			}
		}
		float num6 = num3 + pitch;
		float num7 = maxLookAngle;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
		float num8 = num7 ^ 0;
		if (!(num8 > num6))
		{
			if (num6 > maxLookAngle)
			{
				num6 = maxLookAngle;
			}
		}
		else
		{
			num6 = num8;
		}
		pitch = num6;
		float num9 = Mathf.DeltaAngle(0f, mainGameObjectTransform.localEulerAngles.x);
		float num10 = num9 - num3;
		float num11 = num10;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj2 = num11 & 0;
		float num12 = adoptPitchThreshold;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num12) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2))
		{
			num10 = 0f;
		}
		Vector3 localEulerAngles = mainGameObjectTransform.localEulerAngles;
		if (0f > num10)
		{
			num10 += 360f;
		}
		float num13 = default(float);
		mainGameObjectTransform.localEulerAngles = (Vector3)(&num13);
		Vector3 localEulerAngles2 = cameraRoot.localEulerAngles;
		if (0f > pitch)
		{
		}
		cameraRoot.localEulerAngles = (Vector3)(&num13);
	}

	private unsafe void HeadBob()
	{
		//IL_02b3: Expected O, but got I4
		//IL_02f3: Expected O, but got I4
		//IL_0095: Invalid comparison between I4 and F4
		//IL_023d: Expected O, but got I4
		//IL_0278: Invalid comparison between I4 and F4
		//IL_0331: Expected O, but got Ref
		//IL_0156: Invalid comparison between I4 and F4
		bool flag = joint == null;
		if (flag)
		{
			return;
		}
		Transform transform;
		if (isWalking == flag)
		{
			transform = joint;
			timer = 0f;
			Vector3 localPosition = joint.localPosition;
			float deltaTime = Time.deltaTime;
			float num = deltaTime * bobSpeed;
			if (0f > num || num > 1f)
			{
			}
			Vector3 localPosition2 = joint.localPosition;
			float num2 = default(float);
			if (isCrouched)
			{
				float deltaTime2 = Time.deltaTime;
				num2 = deltaTime2 * bobSpeed;
				if (0f > num2)
				{
					goto IL_011f;
				}
			}
			if (!(num2 > 1f))
			{
			}
			goto IL_011f;
		}
		float num3;
		float deltaTime4;
		float num4;
		if (!isSprinting)
		{
			if (!isCrouched)
			{
				float deltaTime3 = Time.deltaTime;
				num3 = deltaTime3 * bobSpeed;
				goto IL_0299;
			}
			deltaTime4 = Time.deltaTime;
			num4 = speedReduction * bobSpeed;
		}
		else
		{
			deltaTime4 = Time.deltaTime;
			num4 = sprintSpeed + bobSpeed;
		}
		num3 = num4 * deltaTime4;
		goto IL_0299;
		IL_0299:
		float num5 = num3 + timer;
		object obj = 372;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E400");
		transform = joint;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E400");
		bool flag2 = isCrouched;
		object obj2 = 328;
		if (!flag2)
		{
			obj2 = 324;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E400");
		object obj4 = default(object);
		object obj3 = obj4;
		goto IL_0324;
		IL_011f:
		Vector3 localPosition3 = joint.localPosition;
		float deltaTime5 = Time.deltaTime;
		float num6 = deltaTime5 * bobSpeed;
		if (0f > num6 || num6 > 1f)
		{
		}
		obj3 = obj4;
		goto IL_0324;
		IL_0324:
		transform.localPosition = (Vector3)(&obj3);
	}

	private float Normalize180To360(float angleSigned)
	{
		//IL_0009: Invalid comparison between I4 and F4
		float num = default(float);
		if (0f > num)
		{
			return num + 360f;
		}
		return num;
	}

	public void SetPitch(float pitch)
	{
		this.pitch = pitch;
	}

	public void SetFrozen(bool frozen)
	{
		//IL_0330: Expected I, but got O
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Expected F4, but got Unknown
		bool flag;
		if (!cameraCanMove)
		{
			flag = true;
		}
		else
		{
			bool flag2 = !playerCanMove;
			flag = flag2;
		}
		bool flag3 = (byte)((frozen ? 1u : 0u) ^ 1u) != 0;
		cameraCanMove = flag3;
		bool flag4 = (byte)((frozen ? 1u : 0u) ^ 1u) != 0;
		playerCanMove = flag4;
		if (frozen)
		{
			isSprinting = false;
			isWalking = false;
			verticalVelocity = 0f;
		}
		if (!flag || frozen)
		{
			return;
		}
		Transform transform = base.transform;
		yaw = transform.localEulerAngles.y;
		if (cameraRoot != null)
		{
			Vector3 localEulerAngles = cameraRoot.localEulerAngles;
			float num = localEulerAngles.x;
			if (localEulerAngles.x > 180f)
			{
				num -= 360f;
			}
			pitch = num;
			float num2 = maxLookAngle;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
			float num3 = num2 ^ 0;
			if (!(num3 > num))
			{
				if (num > maxLookAngle)
				{
					num = maxLookAngle;
				}
			}
			else
			{
				num = num3;
			}
			pitch = num;
		}
		nint num4 = (nint)typeof(Vector2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v342 @ rax_v15 (Il2CppClass<UnityEngine.Vector2>)+B8]");
		nint num5 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v343 @ rax_v16 (Il2CppStaticFields<UnityEngine.Vector2>)+4]");
		_ = 0;
		smoothedLook = Vector2.zeroVector;
		if (!resyncCrouchOnUnfreeze || !enableCrouch || !holdToCrouch || !(joint != null))
		{
			return;
		}
		bool crouched;
		if (crouchAction == null)
		{
			ResolveInputActions();
			if (crouchAction == null)
			{
				crouched = false;
				goto IL_021e;
			}
		}
		crouched = crouchAction.IsPressed();
		goto IL_021e;
		IL_021e:
		SetCrouched(crouched);
	}

	private void ResyncHoldCrouchFromInputIfNeeded()
	{
		if (!resyncCrouchOnUnfreeze || !enableCrouch || !holdToCrouch || !(joint != null))
		{
			return;
		}
		bool flag = crouchAction != null;
		FirstPersonController firstPersonController = this;
		if (!flag)
		{
			ResolveInputActions();
			bool flag2 = crouchAction != null;
			firstPersonController = this;
			if (!flag2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 119 Invalid \"Jump target not found in method: 0x180504CE0\"");
				FirstPersonController firstPersonController2 = default(FirstPersonController);
				firstPersonController = firstPersonController2;
			}
		}
		bool flag3 = firstPersonController.crouchAction.IsPressed();
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 129 Invalid \"Jump target not found in method: 0x180504CE0\"");
	}

	private unsafe Vector3 ResolvePlatformPivotWorld(Transform ground)
	{
		//IL_00d7: Expected native int or pointer, but got O
		//IL_00e9: Expected native int or pointer, but got O
		Transform transform;
		if (platformPivotMode == PlatformPivotMode.CustomPivotTransform && customPlatformPivot != null)
		{
			transform = customPlatformPivot;
			if ((object)customPlatformPivot == null)
			{
				goto IL_008d;
			}
		}
		else
		{
			if ((object)ground == null)
			{
				goto IL_008d;
			}
			transform = ground;
		}
		Vector3 position = transform.position;
		Vector3 vector = default(Vector3);
		((Vector3*)(nint)vector)->x = position.x;
		((Vector3*)(nint)vector)->z = position.z;
		return vector;
		IL_008d:
		return (Vector3)new NullReferenceException();
	}

	private unsafe bool TryGetGroundHit(out RaycastHit hit)
	{
		//IL_01de: Expected I4, but got O
		//IL_003e: Expected O, but got Ref
		//IL_01c7: Expected F4, but got I4
		//IL_01c7: Expected O, but got Ref
		//IL_01c7: Expected O, but got Ref
		//IL_0193: Expected O, but got Ref
		//IL_0193: Expected O, but got Ref
		Transform transform = base.transform;
		if ((object)controller != null)
		{
			Vector3 center = controller.center;
			if ((object)transform != null)
			{
				float num = default(float);
				Vector3 vector = transform.TransformPoint((Vector3)(&num));
				if ((object)controller != null)
				{
					float radius = controller.radius;
					float num2 = radius * groundProbeRadiusMultiplier;
					bool flag = !(0.01f < num2);
					float num3 = 0.01f;
					if (!flag)
					{
						num3 = num2;
					}
					if ((object)controller != null)
					{
						float height = controller.height;
						if ((object)controller != null)
						{
							float num4 = height * 0.5f;
							float radius2 = controller.radius;
							if (num4 < radius2 || (object)controller != null)
							{
								float radius3 = controller.radius;
								float num5 = num3 + groundProbeExtraDistance;
								float maxDistance = num5 + 0.02f;
								object obj = default(object);
								int num6 = default(int);
								QueryTriggerInteraction queryTriggerInteraction = default(QueryTriggerInteraction);
								if (!useSphereCastForGround)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181DC3B50");
									return Physics.Raycast((Vector3)(&num), (Vector3)(&obj), out hit, maxDistance, num6, queryTriggerInteraction);
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181DC3B50");
								QueryTriggerInteraction queryTriggerInteraction2 = default(QueryTriggerInteraction);
								return Physics.SphereCast((Vector3)(&num), num3, (Vector3)(&obj), out hit, num6, (int)queryTriggerInteraction, queryTriggerInteraction2);
							}
						}
					}
				}
			}
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	public FirstPersonController()
	{
		//IL_0012: Expected O, but got I
		//IL_0243: Expected I, but got O
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
		crosshairColor = (Color)0;
		fov = 60f;
		Vector3 vector = default(Vector3);
		bobAmount = vector;
		_ = 0;
		cameraCanMove = true;
		mouseSensitivity = 2f;
		mouseSensitivityMultiplier = 1f;
		controllerSensitivity = 3f;
		maxLookAngle = 50f;
		lockCameraRoll = true;
		rollReturnSpeed = 180f;
		cameraSmoothing = 25f;
		crosshair = true;
		enableZoom = true;
		zoomFOV = 30f;
		zoomStepTime = 5f;
		adoptExternalCameraYaw = true;
		adoptYawSmoothing = 180f;
		adoptYawThreshold = 0.25f;
		adoptExternalCameraPitch = true;
		adoptPitchSmoothing = 180f;
		adoptPitchThreshold = 0.25f;
		playerCanMove = true;
		walkSpeed = 5f;
		maxVelocityChange = 10f;
		enableSprint = true;
		sprintSpeed = 7f;
		sprintDuration = 5f;
		sprintCooldown = 0.5f;
		sprintFOV = 80f;
		sprintFOVStepTime = 10f;
		useSprintBar = true;
		sprintBarWidthPercent = 0.3f;
		sprintBarHeightPercent = 0.015f;
		enableJump = true;
		jumpPower = 5f;
		GravityMultiplier = 1f;
		enableCrouch = true;
		crouchHeight = 0.75f;
		speedReduction = 0.5f;
		resyncCrouchOnUnfreeze = true;
		enableHeadBob = true;
		bobSpeed = 10f;
		stickToMovingPlatforms = true;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181DC3B50");
		LayerMask layerMask = default(LayerMask);
		groundMask = layerMask;
		useSphereCastForGround = true;
		groundProbeExtraDistance = 0.1f;
		groundProbeRadiusMultiplier = 0.9f;
		preferGroundRigidbodyTransform = true;
		applyRotationalCarry = true;
		nint num = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rax_v3 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num2 = 0;
		platformMotionThisFrame = Vector3.zeroVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ rcx_v3 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		_ = 0;
		base._002Ector();
	}
}
