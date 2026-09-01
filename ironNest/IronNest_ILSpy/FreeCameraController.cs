using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class FreeCameraController : MonoBehaviour
{
	private Transform controlledTransform;

	private GameObject freeCamVisualRig;

	private GameObject gameplayCameraRig;

	private bool matchGameplayPoseOnActivate;

	private Transform gameplayPoseSource;

	public List<InputActionReference> moveActions;

	public List<InputActionReference> lookActions;

	public List<InputActionReference> elevateActions;

	public List<InputActionReference> toggleFreeCamActions;

	public float activateHoldDuration;

	public float moveSpeed;

	public float accelerationTime;

	public float decelerationTime;

	public bool normalizeDiagonal;

	public bool forwardUsesFullLookDirection;

	public float moveDeadzone;

	public float baseLookSensitivity;

	public float lookSmoothingTime;

	public bool invertX;

	public bool invertY;

	public Vector2 pitchClamp;

	public float lookDeadzone;

	public bool startActive;

	public FirstPersonController playerController;

	public PlayerInput playerInput;

	public bool switchActionMaps;

	public string gameplayActionMapName;

	public string freeCamActionMapName;

	private DynamicCursorManager dynamicCursorManager;

	private bool autoSwitchCursorModes;

	private DynamicCursorManager.PresentationMode freeCamPresentationMode;

	private DynamicCursorManager.PresentationMode gameplayPresentationMode;

	private bool delegateSystemCursorToDynamicManager;

	private bool refreshDynamicCursorAfterModeSwitch;

	private GameObject cursorUIRoot;

	public List<InputActionReference> zoomActions;

	public float zoomSensitivity;

	public Vector2 orbitDistanceLimits;

	public bool invertZoom;

	public float zoomSmoothingTime;

	public float zoomDeadzone;

	public UnityEvent onFreeCamActivated;

	public UnityEvent onFreeCamDeactivated;

	private bool _isActive;

	private Vector3 _velocity;

	private float _yawDeg;

	private float _pitchDeg;

	private Vector2 _smoothedLook;

	private bool _cachedCursorUIWasActive;

	private bool _toggleHeld;

	private float _holdTimer;

	private bool _hasOrbitPivot;

	private Vector3 _orbitPivot;

	private float _orbitDistance;

	private float _smoothedZoom;

	private unsafe void Awake()
	{
		//IL_0071: Expected O, but got Ref
		//IL_014c: Expected I, but got O
		//IL_0103: Expected O, but got F4
		if (controlledTransform == null)
		{
			Transform transform = base.transform;
			controlledTransform = transform;
		}
		Quaternion rotation = controlledTransform.rotation;
		Quaternion rotation2 = default(Quaternion);
		Vector3 vector = Quaternion.Internal_ToEulerRad(ref rotation2);
		object obj = default(object);
		Vector3 vector2 = Quaternion.Internal_MakePositive((Vector3)(&obj));
		float yawDeg = default(float);
		_yawDeg = yawDeg;
		float num = MathF.FMod(vector2.x, 360f);
		if (num > 180f)
		{
			num += -360f;
		}
		if (-180f > num)
		{
			num += 360f;
		}
		_pitchDeg = num;
		nint num2 = (nint)typeof(Vector2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v262 @ rax_v11 (Il2CppClass<UnityEngine.Vector2>)+B8]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v263 @ rax_v12 (Il2CppStaticFields<UnityEngine.Vector2>)+4]");
		_ = 0;
		_smoothedLook = Vector2.zeroVector;
		_hasOrbitPivot = false;
		Vector3 position = controlledTransform.position;
		_orbitPivot = (Vector3)position.x;
		_ = position.z;
		_orbitDistance = 0f;
	}

	private void OnEnable()
	{
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Expected O, but got Unknown
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Expected O, but got Unknown
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Expected O, but got Unknown
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Expected O, but got Unknown
		//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Expected O, but got Unknown
		List<InputActionReference> list = toggleFreeCamActions;
		InputAction inputAction = null;
		Action<InputAction.CallbackContext> action = null;
		InputActionReference inputActionReference = default(InputActionReference);
		while ((nint)action < list._size)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			if ((object)inputActionReference != null)
			{
				InputAction action2 = inputActionReference.action;
				if (action2 != null)
				{
					Action<InputAction.CallbackContext> value = OnToggleStarted;
					action2.started += value;
					Action<InputAction.CallbackContext> value2 = OnToggleCanceled;
					action2.canceled += value2;
					Action<InputAction.CallbackContext> action3 = OnTogglePerformed;
					action2.performed += action3;
					bool flag = action2.enabled;
					Action<InputAction.CallbackContext> action4 = action3;
					if (!flag)
					{
						action2.Enable();
						action4 = action3;
					}
				}
			}
			list = toggleFreeCamActions;
			inputAction = (InputAction)(inputAction + 1);
			action = (Action<InputAction.CallbackContext>)(object)inputAction;
		}
		List<InputActionReference> list2 = moveActions;
		if (moveActions != null)
		{
			Action<InputAction.CallbackContext> action4 = null;
			Action<InputAction.CallbackContext> action5 = null;
			while ((nint)action5 < list2._size)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				if ((object)inputActionReference != null)
				{
					InputAction action6 = inputActionReference.action;
					if (action6 != null && !action6.enabled)
					{
						action6.Enable();
					}
				}
				action4 = (Action<InputAction.CallbackContext>)(action4 + 1);
				action5 = action4;
			}
		}
		List<InputActionReference> list3 = lookActions;
		if (lookActions != null)
		{
			Action<InputAction.CallbackContext> action4 = null;
			Action<InputAction.CallbackContext> action7 = null;
			while ((nint)action7 < list3._size)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				if ((object)inputActionReference != null)
				{
					InputAction action8 = inputActionReference.action;
					if (action8 != null && !action8.enabled)
					{
						action8.Enable();
					}
				}
				action4 = (Action<InputAction.CallbackContext>)(action4 + 1);
				action7 = action4;
			}
		}
		List<InputActionReference> list4 = elevateActions;
		if (elevateActions != null)
		{
			Action<InputAction.CallbackContext> action4 = null;
			Action<InputAction.CallbackContext> action9 = null;
			while ((nint)action9 < list4._size)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				if ((object)inputActionReference != null)
				{
					InputAction action10 = inputActionReference.action;
					if (action10 != null && !action10.enabled)
					{
						action10.Enable();
					}
				}
				action4 = (Action<InputAction.CallbackContext>)(action4 + 1);
				action9 = action4;
			}
		}
		List<InputActionReference> list5 = zoomActions;
		bool flag2 = zoomActions == null;
		Action<InputAction.CallbackContext> action11 = null;
		if (flag2)
		{
			return;
		}
		while ((nint)action11 < list5._size)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			if ((object)inputActionReference != null)
			{
				InputAction action12 = inputActionReference.action;
				if (action12 != null && !action12.enabled)
				{
					action12.Enable();
				}
			}
			action11 = (Action<InputAction.CallbackContext>)(action11 + 1);
		}
	}

	private void OnDisable()
	{
		//IL_0175: Expected O, but got I4
		//IL_017e: Expected O, but got I4
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Expected O, but got Unknown
		List<InputActionReference> list = toggleFreeCamActions;
		object obj = 0;
		object obj2 = 0;
		InputActionReference inputActionReference = default(InputActionReference);
		while ((nint)obj2 < list._size)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			if ((object)inputActionReference != null)
			{
				InputAction action = inputActionReference.action;
				if (action != null)
				{
					Action<InputAction.CallbackContext> value = OnToggleStarted;
					action.started -= value;
					Action<InputAction.CallbackContext> value2 = OnToggleCanceled;
					action.canceled -= value2;
					Action<InputAction.CallbackContext> value3 = OnTogglePerformed;
					action.performed -= value3;
				}
			}
			list = toggleFreeCamActions;
			obj++;
			obj2 = obj;
		}
		bool flag = !_isActive;
		_toggleHeld = false;
		_holdTimer = 0f;
		if (!flag)
		{
			SetFreeCamActive(active: false, invokeEvents: true);
		}
	}

	private void Start()
	{
		SetFreeCamActive(startActive, invokeEvents: false);
	}

	private unsafe void Update()
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Expected O, but got Unknown
		//IL_017f: Expected F4, but got I
		//IL_0064: Invalid comparison between F4 and I4
		//IL_0a30: Expected F4, but got I4
		//IL_0410: Expected O, but got I
		//IL_018d: Expected F4, but got I4
		//IL_0ab8: Expected F4, but got I
		//IL_00e8: Expected I, but got O
		//IL_0b2d: Expected F4, but got I
		//IL_0b4a: Expected O, but got I
		//IL_0b54: Invalid comparison between F4 and O
		//IL_12f2: Invalid comparison between I4 and F4
		//IL_0adc: Expected I, but got O
		//IL_0af5: Expected F4, but got O
		//IL_0b05: Expected O, but got I
		//IL_0431: Expected F4, but got I4
		//IL_0b79: Expected I, but got O
		//IL_0b9a: Expected F4, but got O
		//IL_0bce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd3: Expected O, but got Unknown
		//IL_0c05: Invalid comparison between I4 and F4
		//IL_049d: Expected F4, but got I4
		//IL_0d3a: Invalid comparison between O and F4
		//IL_0c96: Expected O, but got F4
		//IL_0519: Expected F4, but got O
		//IL_04ea: Expected O, but got I
		//IL_04fa: Invalid comparison between F4 and I
		//IL_052d: Expected O, but got Ref
		//IL_0542: Invalid comparison between I4 and F4
		//IL_03bb: Expected F4, but got I4
		//IL_1339: Unknown result type (might be due to invalid IL or missing references)
		//IL_133e: Expected O, but got Unknown
		//IL_02a7: Expected O, but got I
		//IL_0db6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dbb: Expected O, but got Unknown
		//IL_0ded: Invalid comparison between I4 and F4
		//IL_05dd: Expected F4, but got O
		//IL_05c0: Expected F4, but got I4
		//IL_0ee7: Expected I, but got O
		//IL_0e51: Expected F4, but got I
		//IL_0e61: Invalid comparison between F4 and I
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Expected O, but got Unknown
		//IL_0314: Expected F4, but got I
		//IL_1388: Invalid comparison between F4 and I4
		//IL_1397: Invalid comparison between F4 and I4
		//IL_13c0: Expected O, but got I4
		//IL_13c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_13cd: Expected I4, but got Unknown
		//IL_0608: Expected F4, but got I4
		//IL_0f1c: Expected I, but got O
		//IL_0f2c: Expected O, but got I
		//IL_0f3c: Expected O, but got I
		//IL_0fcc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fd1: Expected O, but got Unknown
		//IL_10ec: Expected O, but got I
		//IL_1125: Expected O, but got I
		//IL_117a: Expected O, but got I4
		//IL_085f: Expected I, but got O
		//IL_087f: Expected F4, but got I
		//IL_0888: Expected F4, but got O
		//IL_14cd: Expected F4, but got I
		//IL_14de: Invalid comparison between F4 and I
		//IL_08bd: Invalid comparison between O and F4
		//IL_0656: Expected O, but got F4
		//IL_1198: Unknown result type (might be due to invalid IL or missing references)
		//IL_119d: Expected O, but got Unknown
		//IL_11cf: Invalid comparison between I4 and F4
		//IL_0918: Expected O, but got I4
		//IL_0962: Expected F4, but got I4
		//IL_122a: Expected O, but got F4
		//IL_0981: Expected F4, but got I4
		//IL_12d8: Expected O, but got Ref
		//IL_0999: Expected F4, but got O
		//IL_09ce: Expected O, but got F4
		//IL_128c: Expected O, but got Ref
		//IL_128c: Expected O, but got Ref
		//IL_12b4: Invalid comparison between O and F4
		object obj2 = default(object);
		object obj = obj2 - 88;
		if (_toggleHeld && !_isActive && activateHoldDuration > 0f)
		{
			float deltaTime = Time.deltaTime;
			if (!((_holdTimer = deltaTime + _holdTimer) < activateHoldDuration))
			{
				_toggleHeld = false;
				_holdTimer = 0f;
				SetFreeCamActive(active: true, invokeEvents: true);
				nint num = unchecked((nint)null);
				bool flag = true;
			}
		}
		if (!_isActive)
		{
			return;
		}
		float deltaTime2 = Time.deltaTime;
		Vector2 vector = SumVector2(moveActions);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+70]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+74]");
		_ = 0;
		Vector2 vector2 = SumVector2(lookActions);
		List<InputActionReference> list = elevateActions;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+78]");
		float num2 = 0f;
		bool flag2 = elevateActions == null;
		float num3 = 0f;
		float num4 = deltaTime2;
		Vector3 euler = default(Vector3);
		if (!flag2)
		{
			num3 = 0f;
			bool flag3 = false;
			num4 = deltaTime2;
			bool flag4 = false;
			while ((flag4 ? 1 : 0) < list._size)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				bool flag5 = (object)euler == null;
				bool flag = (byte)(&euler) != 0;
				nint num;
				if (!flag5)
				{
					InputAction action = ((InputActionReference)euler).action;
					bool flag6 = action == null;
					flag = (byte)(&euler) != 0;
					if (!flag6)
					{
						bool flag7 = action.enabled;
						bool flag8 = !flag7;
						flag = (byte)(&euler) != 0;
						if (!flag8)
						{
							InputControl activeControl = action.activeControl;
							bool flag9 = activeControl == null;
							flag = (byte)(&euler) != 0;
							if (!flag9)
							{
								InputControl activeControl2 = action.activeControl;
								Type valueType = activeControl2.valueType;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502B8]");
								RuntimeTypeHandle handle = (RuntimeTypeHandle)((nint)0 + (nint)32);
								Type typeFromHandle = Type.GetTypeFromHandle(handle);
								bool flag10 = ((object)valueType).Equals((object)typeFromHandle);
								bool flag11 = !flag10;
								flag = false;
								if (!flag11)
								{
									object obj3 = obj + 120;
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807015E0");
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+78]");
									num4 = 0f;
									flag3 = (byte)((flag3 ? 1u : 0u) + 1u) != 0;
									float num5 = num3;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+78]");
									num3 = num5 + 0f;
									num = 0;
									flag = false;
									flag4 = flag3;
									continue;
								}
							}
							if (action.IsPressed())
							{
								flag3 = (byte)((flag3 ? 1u : 0u) + 1u) != 0;
								num3++;
								num = 0;
								num4 = 1f;
								flag4 = flag3;
								continue;
							}
							num4 = 0f;
						}
					}
				}
				flag3 = (byte)((flag3 ? 1u : 0u) + 1u) != 0;
				num = 0;
				flag4 = flag3;
			}
		}
		Vector2 vector3 = SumVector2(zoomActions);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180371560");
		float num6 = moveDeadzone;
		bool flag12 = !(moveDeadzone > num4);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+74]");
		object obj4 = 0;
		if (!flag12)
		{
			nint num7 = (nint)typeof(Vector2);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v649 @ rax_v72 (Il2CppClass<UnityEngine.Vector2>)+B8]");
			nint num8 = 0;
			num6 = (float)Vector2.zeroVector;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v634 @ rdx_v27 (Il2CppStaticFields<UnityEngine.Vector2>)+4]");
			obj4 = 0;
			_ = Vector2.zeroVector;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v634 @ rdx_v27 (Il2CppStaticFields<UnityEngine.Vector2>)+4]");
			_ = 0;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180371560");
		bool flag13 = !(lookDeadzone > num4);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+7C]");
		float num9 = 0f;
		float num11 = default(float);
		nint num10 = (nint)(&num11);
		float num13 = default(float);
		if (!flag13)
		{
			nint num12 = (nint)typeof(Vector2);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v742 @ rax_v69 (Il2CppClass<UnityEngine.Vector2>)+B8]");
			num10 = 0;
			num9 = num13;
			num2 = (float)Vector2.zeroVector;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+7C]");
		float num14 = 0f;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+7C]");
		nint num15 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj5 = num15 & 0;
		float num16 = zoomDeadzone;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num16) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj5))
		{
			num14 = 0f;
		}
		float num17 = lookSmoothingTime;
		if (0f < lookSmoothingTime)
		{
			if (0.0001f > lookSmoothingTime)
			{
				num17 = 0.0001f;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
			object obj6 = deltaTime2 ^ 0;
			float num18 = (float)obj6 / num17;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033F3D0");
			float num19 = 1f - num18;
			if (!(0f > num19))
			{
				if (num19 > 1f)
				{
					num19 = 1f;
				}
			}
			else
			{
				num19 = 0f;
			}
			float num20 = num9;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (FreeCameraController)+134]");
			float num21 = num20 - 0f;
			float num22 = num21 * num19;
			float num23 = num22;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (FreeCameraController)+134]");
			num9 = num23 + 0f;
			float num24 = num2 - (float)_smoothedLook;
			float num25 = num24 * num19;
			num2 = num25 + (float)_smoothedLook;
			_smoothedLook = (Vector2)num2;
		}
		float num26 = ((!invertX) ? 1f : (-1f));
		float num27 = ((!invertY) ? 1f : (-1f));
		float num28 = num26 * baseLookSensitivity;
		float num29 = num27 * baseLookSensitivity;
		float num30 = num28 * num2;
		float num31 = num29 * num9;
		float num32 = num30 + _yawDeg;
		float num33 = num31 + _pitchDeg;
		_yawDeg = num32;
		Vector2 vector4 = pitchClamp;
		if (System.Runtime.CompilerServices.Unsafe.As<Vector2, UIntPtr>(ref pitchClamp) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num33))
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (FreeCameraController)+90]");
			vector4 = (Vector2)0;
			float num34 = num33;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (FreeCameraController)+90]");
			if (!(num34 > 0f))
			{
				goto IL_0d4e;
			}
		}
		num33 = (float)vector4;
		goto IL_0d4e;
		IL_0d4e:
		float num35 = num32 * ((float)Math.PI / 180f);
		_pitchDeg = num33;
		Quaternion quaternion = Quaternion.Internal_FromEulerRad(ref euler);
		float num36 = default(float);
		controlledTransform.rotation = (Quaternion)(&num36);
		float num37 = zoomSmoothingTime;
		if (0f < zoomSmoothingTime)
		{
			if (0.0001f > zoomSmoothingTime)
			{
				num37 = 0.0001f;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
			object obj7 = deltaTime2 ^ 0;
			float num38 = (float)obj7 / num37;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033F3D0");
			num37 = 1f - num38;
			if (!(0f > num37))
			{
				if (num37 > 1f)
				{
					num37 = 1f;
				}
			}
			else
			{
				num37 = 0f;
			}
			float num39 = num14 - _smoothedZoom;
			float num40 = num39 * num37;
			num14 = (_smoothedZoom = num40 + _smoothedZoom);
		}
		bool flag14 = invertZoom;
		float num41 = -1f;
		if (!flag14)
		{
			num41 = 1f;
		}
		float num42 = num14 * num41;
		float num43 = num42 * zoomSensitivity;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj8 = num43 & 0;
		if ((nint)obj8 > 0)
		{
			float num44 = (float)orbitDistanceLimits;
			if (0 > (nint)orbitDistanceLimits)
			{
				num44 = 0f;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (FreeCameraController)+F8]");
			float num45 = 0f;
			float num46 = num44;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (FreeCameraController)+F8]");
			if (num46 > 0f)
			{
				num45 = num44;
			}
			bool flag15 = !_hasOrbitPivot;
			float num47 = num44 + 0.0001f;
			bool flag16 = num43 < 0f;
			bool flag17 = num43 == 0f;
			bool flag18 = !flag16;
			bool flag19 = !flag17;
			object obj9 = flag19 & flag18;
			bool flag20 = (byte)((flag15 & obj9) ? 1 : 0) != 0;
			if (num47 < _orbitDistance)
			{
				flag20 = false;
			}
			if (flag20)
			{
				_hasOrbitPivot = true;
				Vector3 position = controlledTransform.position;
				_orbitPivot = (Vector3)position.x;
				_ = position.z;
				_orbitDistance = num44;
			}
			float num48 = num43 + _orbitDistance;
			if (!(num44 > num48))
			{
				if (num48 > num45)
				{
					num48 = num45;
				}
			}
			else
			{
				num48 = num44;
			}
			_orbitDistance = num48;
			float num49 = num44 + 0.0001f;
			if (num49 < num48)
			{
				_hasOrbitPivot = true;
			}
			else
			{
				_orbitDistance = num44;
				_hasOrbitPivot = false;
			}
		}
		float num50;
		float num51;
		if (forwardUsesFullLookDirection)
		{
			Vector3 forward = controlledTransform.forward;
			num50 = forward.z;
			num51 = forward.x;
			float num52 = num35;
		}
		else
		{
			Vector3 forward2 = controlledTransform.forward;
			float num53 = forward2.z;
			nint num54 = (nint)typeof(Vector3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1536 @ rax_v50 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num55 = 0;
			float num56 = num35 * num35;
			object obj10 = (object)Vector3.upVector * (object)Vector3.upVector;
			float num57 = num56 + (float)obj10;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1537 @ rcx_v38 (Il2CppStaticFields<UnityEngine.Vector3>)+20]");
			float num58 = 0f;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1537 @ rcx_v38 (Il2CppStaticFields<UnityEngine.Vector3>)+20]");
			num37 = num58 * 0f;
			float num59 = num57 + num37;
			float num70;
			float num68;
			if (!(Mathf.Epsilon > num59))
			{
				float num60 = forward2.x * (float)Vector3.upVector;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+74]");
				float num61 = 0f * num35;
				float num62 = num61 + num60;
				float num63 = forward2.z;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1537 @ rcx_v38 (Il2CppStaticFields<UnityEngine.Vector3>)+20]");
				float num64 = num63 * 0f;
				float num65 = num62 + num64;
				float num66 = num65 / num59;
				num37 = num66 * num35;
				float num67 = num66;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1537 @ rcx_v38 (Il2CppStaticFields<UnityEngine.Vector3>)+20]");
				num68 = num67 * 0f;
				float num69 = forward2.z - num68;
				num53 = num69;
				num70 = num13;
			}
			else
			{
				num70 = forward2.x;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
			if (num70 > 1E-05f)
			{
				num37 = num35 / num70;
				float num52 = num53 / num70;
				num68 = num13;
				num50 = num52;
			}
			else
			{
				nint num71 = (nint)typeof(Vector3);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1816 @ rax_v56 (Il2CppClass<UnityEngine.Vector3>)+B8]");
				nint num72 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1817 @ rcx_v44 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
				num50 = 0f;
				num68 = (float)Vector3.zeroVector;
				float num52 = num70;
			}
			num51 = num68;
		}
		Transform transform = controlledTransform;
		Vector3 right = controlledTransform.right;
		nint num73 = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+60]");
		object obj11 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+70]");
		object obj12 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1645 @ rax_v24 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num74 = 0;
		if (normalizeDiagonal)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
			bool flag21 = System.Runtime.CompilerServices.Unsafe.As<Vector3, UIntPtr>(ref Vector3.upVector) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1f);
			transform = null;
			if (!flag21)
			{
				obj11 /= (object)Vector3.upVector;
				num3 /= (float)Vector3.upVector;
				obj12 /= (object)Vector3.upVector;
				transform = null;
			}
		}
		float num75 = right.x * (float)obj11;
		float num76 = (float)Vector3.upVector * num3;
		float num77 = num51 * (float)obj12;
		float num78 = num35 * (float)obj12;
		float num79 = num77 + num75;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+74]");
		object obj13 = 0 * obj11;
		float num80 = num50 * (float)obj12;
		float num81 = num79 + num76;
		float num82 = num78 + (float)obj13;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+7C]");
		float num83 = 0f * num3;
		float num84 = right.z * (float)obj11;
		float num85 = num82 + num83;
		float num86 = num81 * moveSpeed;
		float num87 = num80 + num84;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1649 @ rcx_v20 (Il2CppStaticFields<UnityEngine.Vector3>)+20]");
		float num88 = 0f * num3;
		float num89 = num85 * moveSpeed;
		float num90 = num87 + num88;
		float num91 = num90 * moveSpeed;
		object obj14 = (object)_velocity * (object)_velocity;
		float num92 = num91 * num91;
		float num93 = num89 * num89;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (FreeCameraController)+120]");
		nint num94 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (FreeCameraController)+120]");
		object obj15 = num94 * 0;
		float num95 = num86 * num86;
		object obj16 = obj14 + obj15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (FreeCameraController)+124]");
		nint num96 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (FreeCameraController)+124]");
		object obj17 = num96 * 0;
		float num97 = num93 + num95;
		object obj18 = obj16 + obj17;
		float num98 = num97 + num92;
		float num99 = (float)obj18 + 0.0001f;
		bool flag22 = !(num98 > num99);
		object obj19 = 116;
		if (!flag22)
		{
			obj19 = 112;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1777 @ rax_v27+this @ rcx (FreeCameraController)]");
		float num100 = 0f;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1777 @ rax_v27+this @ rcx (FreeCameraController)]");
		if (0.0001f > 0f)
		{
			num100 = 0.0001f;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
		object obj20 = deltaTime2 ^ 0;
		float num101 = (float)obj20 / num100;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033F3D0");
		float num102 = 1f - num101;
		if (!(0f > num102))
		{
			if (num102 > 1f)
			{
				num102 = 1f;
			}
		}
		else
		{
			num102 = 0f;
		}
		float num103 = num91;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (FreeCameraController)+124]");
		float num104 = num103 - 0f;
		float num105 = num104 * num102;
		float num106 = num105;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (FreeCameraController)+124]");
		float num107 = num106 + 0f;
		_velocity = (Vector3)num13;
		Transform transform2;
		float num113;
		if (_hasOrbitPivot)
		{
			bool flag23 = 0 > (nint)orbitDistanceLimits;
			float num108 = 0f;
			if (!flag23)
			{
				num108 = (float)orbitDistanceLimits;
			}
			float num109 = num108 + 0.0001f;
			if (_orbitDistance > num109)
			{
				float num110 = num107 * deltaTime2;
				float num111 = num110;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (FreeCameraController)+14C]");
				float num112 = num111 + 0f;
				_orbitPivot = (Vector3)num13;
				object obj21 = default(object);
				Vector3 vector6 = default(Vector3);
				Vector3 vector5 = (Quaternion)(&obj21) * (Vector3)(&vector6);
				transform2 = controlledTransform;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
				if (System.Runtime.CompilerServices.Unsafe.As<Vector3, UIntPtr>(ref _orbitPivot) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1E-05f))
				{
				}
				num113 = num13;
				goto IL_12cb;
			}
		}
		transform2 = controlledTransform;
		Vector3 position2 = controlledTransform.position;
		num113 = num13;
		goto IL_12cb;
		IL_12cb:
		transform2.position = (Vector3)(&num113);
	}

	private void OnToggleStarted(InputAction.CallbackContext ctx)
	{
		//IL_002d: Invalid comparison between I4 and F4
		if (!_isActive && 0f < activateHoldDuration)
		{
			_toggleHeld = true;
			_holdTimer = 0f;
		}
	}

	private void OnToggleCanceled(InputAction.CallbackContext ctx)
	{
		_toggleHeld = false;
		_holdTimer = 0f;
	}

	private void OnTogglePerformed(InputAction.CallbackContext ctx)
	{
		//IL_002d: Invalid comparison between I4 and F4
		if (!_isActive)
		{
			if (!(0f < activateHoldDuration))
			{
				SetFreeCamActive(active: true, invokeEvents: true);
			}
		}
		else
		{
			_toggleHeld = false;
			_holdTimer = 0f;
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 36 Invalid \"Jump target not found in method: 0x1805A3D10\"");
		}
	}

	public void ActivateFreeCam()
	{
		SetFreeCamActive(active: true, invokeEvents: true);
	}

	public void DeactivateFreeCam()
	{
		SetFreeCamActive(active: false, invokeEvents: true);
	}

	private unsafe void SetFreeCamActive(bool active, bool invokeEvents)
	{
		//IL_02bb: Expected O, but got I4
		//IL_0132: Expected O, but got Ref
		//IL_0132: Expected O, but got Ref
		//IL_059a: Expected I, but got O
		//IL_0600: Expected I, but got O
		//IL_0161: Expected O, but got Ref
		//IL_064b: Expected I, but got O
		//IL_06a6: Expected O, but got I4
		//IL_0637: Expected F4, but got O
		//IL_0507: Expected O, but got F4
		//IL_04a7: Expected O, but got I
		//IL_04d0: Expected O, but got I
		if (_isActive == active)
		{
			return;
		}
		_isActive = active;
		if (active && matchGameplayPoseOnActivate)
		{
			UnityEngine.Object obj;
			if (gameplayPoseSource != null)
			{
				obj = gameplayPoseSource;
			}
			else if (gameplayCameraRig != null)
			{
				Transform transform = gameplayCameraRig.transform;
				obj = transform;
			}
			else
			{
				obj = null;
			}
			if (obj != null)
			{
				Vector3 position = ((Transform)obj).position;
				Quaternion rotation = ((Transform)obj).rotation;
				float num = default(float);
				object obj2 = default(object);
				controlledTransform.SetPositionAndRotation((Vector3)(&num), (Quaternion)(&obj2));
				nint num2 = (nint)typeof(Vector3);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v838 @ rax_v68 (Il2CppClass<UnityEngine.Vector3>)+B8]");
				nint num3 = 0;
				_velocity = Vector3.zeroVector;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v839 @ rcx_v60 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180371660");
				Vector2 smoothedLook = default(Vector2);
				_smoothedLook = smoothedLook;
				Quaternion rotation2 = controlledTransform.rotation;
				Quaternion rotation3 = default(Quaternion);
				Vector3 vector = Quaternion.Internal_ToEulerRad(ref rotation3);
				Vector3 vector2 = Quaternion.Internal_MakePositive((Vector3)(&num));
				float yawDeg = default(float);
				_yawDeg = yawDeg;
				float pitchDeg = Wrap180(vector2.x);
				_pitchDeg = pitchDeg;
			}
		}
		if ((bool)freeCamVisualRig)
		{
			freeCamVisualRig.SetActive(active);
		}
		if ((bool)gameplayCameraRig)
		{
			bool active2 = (byte)((active ? 1u : 0u) ^ 1u) != 0;
			gameplayCameraRig.SetActive(active2);
		}
		if ((bool)playerController)
		{
			playerController.SetFrozen(active);
		}
		if ((bool)dynamicCursorManager && autoSwitchCursorModes)
		{
			object obj3 = (active ? 1 : 0) ^ 1;
			DynamicCursorManager obj4 = dynamicCursorManager;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (FreeCameraController)+D4+v428 @ rdx_v30*4]");
			obj4.SwitchToPresentationMode(DynamicCursorManager.PresentationMode.FPSLocked);
			if (refreshDynamicCursorAfterModeSwitch)
			{
				dynamicCursorManager.ForceRefresh();
			}
		}
		if ((bool)cursorUIRoot)
		{
			GameObject gameObject;
			bool active3;
			if (!active)
			{
				if (_cachedCursorUIWasActive == active || cursorUIRoot.activeSelf)
				{
					goto IL_03f2;
				}
				gameObject = cursorUIRoot;
				active3 = true;
			}
			else
			{
				if (!(_cachedCursorUIWasActive = cursorUIRoot.activeSelf))
				{
					goto IL_03f2;
				}
				gameObject = cursorUIRoot;
				active3 = false;
			}
			gameObject.SetActive(active3);
		}
		goto IL_03f2;
		IL_03f2:
		bool flag = dynamicCursorManager;
		if (!flag && delegateSystemCursorToDynamicManager == flag)
		{
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}
		if (switchActionMaps && playerInput != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (FreeCameraController)+B8+active @ rdx (System.Boolean)*8]");
			if (!string.IsNullOrEmpty((string)0))
			{
				PlayerInput obj5 = playerInput;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (FreeCameraController)+B8+active @ rdx (System.Boolean)*8]");
				obj5.SwitchCurrentActionMap((string)0);
			}
		}
		nint num4 = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1100 @ rax_v24 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num5 = 0;
		_velocity = Vector3.zeroVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1101 @ rcx_v24 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		_ = 0;
		nint num6 = (nint)typeof(Vector2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1120 @ rax_v27 (Il2CppClass<UnityEngine.Vector2>)+B8]");
		nint num7 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1121 @ rax_v28 (Il2CppStaticFields<UnityEngine.Vector2>)+4]");
		_ = 0;
		_smoothedLook = Vector2.zeroVector;
		_smoothedZoom = 0f;
		_hasOrbitPivot = false;
		bool flag2 = 0 >= (nint)orbitDistanceLimits;
		Vector2 vector3 = (Vector2)0;
		if (!flag2)
		{
			vector3 = orbitDistanceLimits;
		}
		_orbitDistance = (float)vector3;
		Vector3 position2 = controlledTransform.position;
		_orbitPivot = (Vector3)position2.x;
		_ = position2.z;
		if (invokeEvents)
		{
			UnityEvent unityEvent = (active ? onFreeCamActivated : onFreeCamDeactivated);
			unityEvent.Invoke();
		}
	}

	private static void EnableActions(List<InputActionReference> actions)
	{
		//IL_000e: Expected O, but got I4
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Expected O, but got Unknown
		if (actions == null)
		{
			return;
		}
		object obj = 0;
		InputActionReference inputActionReference = default(InputActionReference);
		while ((nint)obj < actions._size)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			if ((object)inputActionReference != null)
			{
				InputAction action = inputActionReference.action;
				if (action != null && !action.enabled)
				{
					action.Enable();
				}
			}
			obj++;
		}
	}

	private unsafe static Vector2 SmoothDelta(Vector2 rawDelta, float dt, float timeConst, ref Vector2 state)
	{
		//IL_0009: Invalid comparison between I4 and F4
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Expected O, but got Unknown
		//IL_00de: Invalid comparison between I4 and F4
		//IL_00a2: Expected F4, but got I4
		//IL_015a: Expected Ref, but got F4
		if (0f < timeConst)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
			object obj = dt ^ 0;
			bool flag = !(0.0001f < timeConst);
			float num = 0.0001f;
			if (!flag)
			{
				num = timeConst;
			}
			float num2 = (float)obj / num;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033F3D0");
			float num3 = 1f - num2;
			if (!(0f > num3))
			{
				if (num3 > 1f)
				{
					num3 = 1f;
				}
			}
			else
			{
				num3 = 0f;
			}
			object obj2 = rawDelta - state;
			object obj4 = default(object);
			object obj5 = default(object);
			object obj3 = obj4 - obj5;
			float num4 = (float)obj2 * num3;
			float num5 = (float)obj3 * num3;
			float num6 = num4 + (float)state;
			float num7 = num5 + (float)obj5;
			ref Vector2 reference = ref *(Vector2*)num6;
			return state;
		}
		return rawDelta;
	}

	private unsafe static float SmoothScalar(float raw, float dt, float timeConst, ref float state)
	{
		//IL_0009: Invalid comparison between I4 and F4
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Expected O, but got Unknown
		//IL_00de: Invalid comparison between I4 and F4
		//IL_00a2: Expected F4, but got I4
		//IL_0131: Expected Ref, but got F4
		float num7 = default(float);
		if (0f < timeConst)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
			object obj = dt ^ 0;
			bool flag = !(0.0001f < timeConst);
			float num = 0.0001f;
			if (!flag)
			{
				num = timeConst;
			}
			float num2 = (float)obj / num;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033F3D0");
			float num3 = 1f - num2;
			if (!(0f > num3))
			{
				if (num3 > 1f)
				{
					num3 = 1f;
				}
			}
			else
			{
				num3 = 0f;
			}
			float num5 = default(float);
			float num4 = num5 - state;
			float num6 = num4 * num3;
			num7 = num6 + state;
			ref float reference = ref *(float*)num7;
		}
		return num7;
	}

	private static float Wrap180(float angle)
	{
		float num = MathF.FMod(angle, 360f);
		if (num > 180f)
		{
			num += -360f;
		}
		if (-180f > num)
		{
			num += 360f;
		}
		return num;
	}

	private static Vector2 SumVector2(List<InputActionReference> actions)
	{
		//IL_000e: Expected O, but got I4
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Expected O, but got Unknown
		if (actions != null)
		{
			object obj = 0;
			InputActionReference inputActionReference = default(InputActionReference);
			while ((nint)obj < actions._size)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				if ((object)inputActionReference != null)
				{
					InputAction action = inputActionReference.action;
					if (action != null && action.enabled)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807015E0");
					}
				}
				obj++;
			}
		}
		Vector2 result = default(Vector2);
		return result;
	}

	private static float SumAxis(List<InputActionReference> actions)
	{
		//IL_01ba: Expected F4, but got I4
		//IL_000e: Expected F4, but got I4
		//IL_0017: Expected O, but got I4
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Expected O, but got Unknown
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Expected O, but got Unknown
		//IL_00f7: Expected O, but got I
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Expected O, but got Unknown
		if (actions != null)
		{
			float num = 0f;
			object obj = 0;
			InputActionReference inputActionReference = default(InputActionReference);
			float num2 = default(float);
			while ((nint)obj < actions._size)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				if ((object)inputActionReference != null)
				{
					InputAction action = inputActionReference.action;
					if (action != null && action.enabled)
					{
						InputControl activeControl = action.activeControl;
						if (activeControl != null)
						{
							InputControl activeControl2 = action.activeControl;
							Type valueType = activeControl2.valueType;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502B8]");
							RuntimeTypeHandle handle = (RuntimeTypeHandle)((nint)0 + (nint)32);
							Type typeFromHandle = Type.GetTypeFromHandle(handle);
							if (((object)valueType).Equals((object)typeFromHandle))
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807015E0");
								obj++;
								num += num2;
								continue;
							}
						}
						if (action.IsPressed())
						{
							obj++;
							num++;
							continue;
						}
					}
				}
				obj++;
			}
			return num;
		}
		return 0f;
	}

	public FreeCameraController()
	{
		//IL_00c7: Expected O, but got I8
		matchGameplayPoseOnActivate = true;
		List<InputActionReference> list = new List<InputActionReference>();
		moveActions = list;
		lookActions = new List<InputActionReference>();
		elevateActions = new List<InputActionReference>();
		toggleFreeCamActions = new List<InputActionReference>();
		activateHoldDuration = 1f;
		moveSpeed = 6f;
		accelerationTime = 0.12f;
		decelerationTime = 0.12f;
		normalizeDiagonal = true;
		moveDeadzone = 0.01f;
		baseLookSensitivity = 0.12f;
		lookSmoothingTime = 0.06f;
		invertY = true;
		pitchClamp = (Vector2)3266445312L;
		_ = 1118961664;
		gameplayActionMapName = "Player";
		freeCamActionMapName = "FreeCam";
		autoSwitchCursorModes = true;
		delegateSystemCursorToDynamicManager = true;
		zoomActions = new List<InputActionReference>();
		zoomSensitivity = 0.25f;
		_ = 1084227584;
		base._002Ector();
	}
}
