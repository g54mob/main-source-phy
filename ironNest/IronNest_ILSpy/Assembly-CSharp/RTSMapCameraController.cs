using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.InputSystem;

public class RTSMapCameraController : MonoBehaviour
{
	public Vector2 localMin;

	public Vector2 localMax;

	public float boundarySoftness;

	public float boundaryResistStrength;

	public float panZoneThickness;

	public float panMaxSpeed;

	public float panMinSpeed;

	public bool enableKeyboardPanning;

	public float keyboardPanBaseSpeed;

	public bool keyboardPanScaleWithZoom;

	public float panSpeedMinZoomMultiplier;

	public float panSpeedMaxZoomMultiplier;

	public bool enableScrollClickPanning;

	public float scrollClickPanBaseSpeed;

	public bool scrollClickPanScaleWithZoom;

	public float panClickSpeedMinZoomMultiplier;

	public float panClickSpeedMaxZoomMultiplier;

	public float minZoom;

	public float maxZoom;

	public float defaultZoom;

	public bool resetZoomOnEnter;

	public float scrollSensitivity;

	public bool invertScroll;

	public float zoomSmoothTime;

	public Vector3 zoomLocalAxis;

	public float minAngle;

	public float maxAngle;

	public float rotationSmoothTime;

	public float zoomInOffsetStrength;

	public float zoomInOffsetPower;

	public float cameraMoveSmoothTime;

	public bool enablePositionalTilt;

	public float maxHorizontalTilt;

	public float maxVerticalTilt;

	public float positionalTiltSmoothTime;

	public Transform cameraChild;

	public VirtualCursor virtualCursor;

	public InputActionReference scrollAction;

	public InputActionReference panAction;

	public InputActionReference pointerDelta;

	public InputActionReference scrollClick;

	public bool enableActionsOnEnable;

	private float targetZoom;

	private float zoomVel;

	private Vector3 targetLocalPosition;

	private Vector3 positionVel;

	private bool isActive;

	private float targetPitch;

	private float pitchVel;

	private float positionalTiltYaw;

	private float positionalTiltPitch;

	private float positionalTiltYawVel;

	private float positionalTiltPitchVel;

	private Quaternion cameraChildBaseRotation;

	private unsafe void Awake()
	{
		//IL_006b: Expected O, but got F4
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Expected O, but got Unknown
		//IL_0115: Expected O, but got Ref
		//IL_0138: Expected O, but got F4
		if (!cameraChild)
		{
			Debug.LogError("Assign Camera Child (Camera or Cinemachine object) in inspector.");
		}
		Transform transform = base.transform;
		Vector3 localPosition = transform.localPosition;
		float num = minZoom;
		targetLocalPosition = (Vector3)localPosition.x;
		float num2 = defaultZoom;
		_ = localPosition.z;
		float num3;
		if (!(minZoom > defaultZoom))
		{
			num = maxZoom;
			bool flag = !(defaultZoom > maxZoom);
			num3 = maxZoom;
			if (flag)
			{
				goto IL_013e;
			}
		}
		num2 = num;
		num3 = num;
		goto IL_013e;
		IL_013e:
		targetPitch = minAngle;
		targetZoom = num2;
		object obj = this + 136;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
		Vector3 vector2 = default(Vector3);
		Vector3 vector = ((!(num2 > 1E-05f)) ? Vector3.zeroVector : vector2);
		cameraChild.localPosition = (Vector3)(&vector);
		cameraChildBaseRotation = (Quaternion)cameraChild.localRotation.x;
	}

	private void OnEnable()
	{
		bool flag = !enableActionsOnEnable;
		isActive = true;
		if (!flag)
		{
			TryEnable(scrollAction);
			TryEnable(panAction);
			TryEnable(pointerDelta);
			TryEnable(scrollClick);
		}
	}

	private void OnDisable()
	{
		isActive = false;
	}

	private unsafe void Update()
	{
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Expected Ref, but got Unknown
		//IL_00c4: Expected O, but got Ref
		//IL_016b: Invalid comparison between I4 and F4
		//IL_017a: Expected O, but got I4
		//IL_029b: Expected F4, but got I4
		//IL_00fa: Expected O, but got I4
		//IL_02a8: Invalid comparison between O and F4
		//IL_01a3: Expected O, but got I4
		//IL_01d1: Expected F4, but got I4
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Expected Ref, but got Unknown
		//IL_01ba: Expected O, but got I4
		//IL_0212: Expected O, but got Ref
		if (!isActive)
		{
			return;
		}
		HandleKeyboardPanning();
		HandleEdgePanning();
		HandleScrollPanning();
		HandleZoom();
		Transform transform = base.transform;
		Transform transform2 = base.transform;
		Vector3 localPosition = transform2.localPosition;
		float deltaTime = Time.deltaTime;
		Vector3 current = default(Vector3);
		Vector3 target = default(Vector3);
		float smoothTime = default(float);
		float maxSpeed = default(float);
		float deltaTime2 = default(float);
		Vector3 vector = Vector3.SmoothDamp(ref current, ref target, ref *(Vector3*)(this + 264), smoothTime, maxSpeed, deltaTime2);
		Vector3 euler = default(Vector3);
		transform.localPosition = (Vector3)(&euler);
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018047AFE0h\"");
		object obj;
		float num3;
		if (minZoom == maxZoom)
		{
			obj = 0;
		}
		else
		{
			float num = maxZoom - minZoom;
			float num2 = targetZoom - minZoom;
			num3 = num2 / num;
			bool flag = 0f > num3;
			obj = 0;
			if (!flag)
			{
				bool flag2 = !(num3 > 1f);
				obj = 0;
				if (!flag2)
				{
					obj = 0;
					num3 = 1f;
				}
				goto IL_02a0;
			}
		}
		num3 = 0f;
		goto IL_02a0;
		IL_02a0:
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num3))
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
		float num4 = maxAngle - minAngle;
		ref float currentVelocity = ref *(float*)(this + 284);
		float num5 = num4 * num3;
		float target2 = num5 + minAngle;
		float num6 = Mathf.SmoothDampAngle(targetPitch, target2, ref currentVelocity, rotationSmoothTime);
		targetPitch = num6;
		Transform transform3 = base.transform;
		Vector3 localEulerAngles = transform3.localEulerAngles;
		Transform transform4 = base.transform;
		Quaternion quaternion = Quaternion.Internal_FromEulerRad(ref euler);
		object obj2 = default(object);
		transform4.localRotation = (Quaternion)(&obj2);
		ApplyPositionalTilt();
	}

	public unsafe void CenterOnFocusPointLocal(Vector3 localFocusPoint)
	{
		//IL_0019: Invalid comparison between O and F4
		//IL_01c6: Invalid comparison between I and F4
		//IL_0046: Invalid comparison between F4 and O
		//IL_005c: Expected O, but got F4
		//IL_00c3: Expected F4, but got I
		//IL_0087: Invalid comparison between F4 and I
		//IL_00d5: Expected O, but got Ref
		//IL_00ae: Expected F4, but got I
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Expected O, but got Unknown
		//IL_019e: Expected O, but got Ref
		Vector2 vector = localMin;
		Vector2 vector3;
		if (System.Runtime.CompilerServices.Unsafe.As<Vector2, UIntPtr>(ref localMin) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)localFocusPoint.x))
		{
			vector = localMax;
			float x = localFocusPoint.x;
			Vector2 vector2 = localMax;
			bool flag = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)x) <= System.Runtime.CompilerServices.Unsafe.As<Vector2, UIntPtr>(ref vector2);
			vector3 = (Vector2)localFocusPoint.x;
			if (flag)
			{
				goto IL_01a4;
			}
		}
		vector3 = vector;
		goto IL_01a4;
		IL_01f8:
		float num;
		targetZoom = num;
		object obj = this + 136;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
		Vector3 vector5 = default(Vector3);
		Vector3 vector4 = ((!(num > 1E-05f)) ? Vector3.zeroVector : vector5);
		cameraChild.localPosition = (Vector3)(&vector4);
		return;
		IL_01a4:
		float z = localFocusPoint.z;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (RTSMapCameraController)+24]");
		if (!(0f > localFocusPoint.z))
		{
			float num2 = z;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (RTSMapCameraController)+2C]");
			if (num2 > 0f)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (RTSMapCameraController)+2C]");
				z = 0f;
			}
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (RTSMapCameraController)+24]");
			z = 0f;
		}
		targetLocalPosition = vector3;
		Transform transform = base.transform;
		Vector2 vector6 = default(Vector2);
		transform.localPosition = (Vector3)(&vector6);
		if (!resetZoomOnEnter)
		{
			return;
		}
		float num3 = minZoom;
		num = defaultZoom;
		float num4;
		if (!(minZoom > defaultZoom))
		{
			num3 = maxZoom;
			bool flag2 = !(defaultZoom > maxZoom);
			num4 = maxZoom;
			if (flag2)
			{
				goto IL_01f8;
			}
		}
		num4 = num3;
		num = num3;
		goto IL_01f8;
	}

	public unsafe void ResetZoomToDefault()
	{
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Expected O, but got Unknown
		//IL_00aa: Expected O, but got Ref
		float num = minZoom;
		float num2 = defaultZoom;
		float num3;
		if (!(minZoom > defaultZoom))
		{
			num = maxZoom;
			bool flag = !(defaultZoom > maxZoom);
			num3 = maxZoom;
			if (flag)
			{
				goto IL_00ab;
			}
		}
		num2 = num;
		num3 = num;
		goto IL_00ab;
		IL_00ab:
		targetZoom = num2;
		object obj = this + 136;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
		Vector3 vector2 = default(Vector3);
		Vector3 vector = ((!(num2 > 1E-05f)) ? Vector3.zeroVector : vector2);
		cameraChild.localPosition = (Vector3)(&vector);
	}

	public float GetCurrentZoom()
	{
		if ((bool)cameraChild)
		{
			Vector3 localPosition = cameraChild.localPosition;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
			return localPosition.x;
		}
		return targetZoom;
	}

	public unsafe void SetZoomDirect(float zoom)
	{
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Expected O, but got Unknown
		//IL_00b5: Expected O, but got Ref
		float num2;
		if (!(minZoom > zoom))
		{
			float num = maxZoom;
			bool flag = !(zoom > maxZoom);
			num2 = zoom;
			if (!flag)
			{
				num2 = maxZoom;
			}
		}
		else
		{
			float num = zoom;
			num2 = minZoom;
		}
		targetZoom = num2;
		if ((bool)cameraChild)
		{
			object obj = this + 136;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
			Vector3 vector2 = default(Vector3);
			Vector3 vector = ((!(minZoom > 1E-05f)) ? Vector3.zeroVector : vector2);
			cameraChild.localPosition = (Vector3)(&vector);
		}
	}

	private unsafe void HandleEdgePanning()
	{
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Expected O, but got Unknown
		//IL_009f: Expected O, but got I
		//IL_00ae: Invalid comparison between F4 and O
		//IL_00cc: Invalid comparison between F4 and O
		//IL_01ce: Expected F4, but got O
		//IL_01e0: Expected F4, but got O
		//IL_01f2: Expected F4, but got O
		//IL_020c: Expected F4, but got I
		//IL_02e8: Expected O, but got I4
		//IL_02f7: Expected I, but got O
		//IL_0305: Expected I, but got O
		//IL_00ea: Invalid comparison between F4 and O
		//IL_04d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d8: Expected O, but got Unknown
		//IL_04f1: Invalid comparison between I4 and F4
		//IL_025b: Expected I, but got O
		//IL_0341: Expected F4, but got I4
		//IL_0110: Invalid comparison between F4 and I
		//IL_0138: Invalid comparison between F4 and I4
		//IL_0161: Expected O, but got I4
		//IL_0560: Invalid comparison between I4 and F4
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Expected O, but got Unknown
		//IL_0385: Expected F4, but got I4
		//IL_052a: Unknown result type (might be due to invalid IL or missing references)
		//IL_052f: Expected O, but got Unknown
		//IL_0413: Expected F4, but got I4
		//IL_05da: Invalid comparison between I4 and F4
		//IL_03c8: Invalid comparison between I4 and F4
		//IL_044f: Expected F4, but got I4
		//IL_0649: Unknown result type (might be due to invalid IL or missing references)
		//IL_064e: Expected O, but got Unknown
		//IL_06bb: Expected O, but got Ref
		//IL_0473: Invalid comparison between I4 and F4
		//IL_04be: Expected F4, but got I4
		if (!(this.virtualCursor != null))
		{
			return;
		}
		InputAction action = scrollClick.action;
		if (action.IsPressed())
		{
			return;
		}
		VirtualCursor virtualCursor = this.virtualCursor;
		int width = Screen.width;
		object obj = width - virtualCursor._position;
		int height = Screen.height;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v219 @ rax_v13 (VirtualCursor)+70]");
		object obj2 = (nint)height - (nint)0;
		float num = panZoneThickness;
		Vector2 position = virtualCursor._position;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num) <= System.Runtime.CompilerServices.Unsafe.As<Vector2, UIntPtr>(ref position))
		{
			float num2 = panZoneThickness;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num2) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj))
			{
				float num3 = panZoneThickness;
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num3) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2))
				{
					float num4 = panZoneThickness;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v219 @ rax_v13 (VirtualCursor)+70]");
					bool flag = num4 < 0f;
					float num5 = panZoneThickness;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v219 @ rax_v13 (VirtualCursor)+70]");
					float num6 = num5 - 0f;
					bool flag2 = num6 == 0f;
					bool flag3 = !flag;
					bool flag4 = !flag2;
					object obj3 = flag4 & flag3;
					if (obj3 == null)
					{
						return;
					}
				}
			}
		}
		int width2 = Screen.width;
		int height2 = Screen.height;
		Vector2 value = default(Vector2);
		Vector2 vector = Vector2.Normalize(ref value);
		float[] array = new float[4]
		{
			(float)virtualCursor._position,
			(float)obj,
			(float)obj2,
			0f
		};
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v219 @ rax_v13 (VirtualCursor)+70]");
		array[3] = 0f;
		Vector2 vector2;
		if (array.Length != 0)
		{
			bool flag5 = array.Length <= 1;
			vector2 = virtualCursor._position;
			nint num7 = array.Length;
			nint num8 = unchecked((nint)null);
			nint num9 = 1;
			if (!flag5)
			{
				object obj4 = array + 36;
				vector2 = virtualCursor._position;
				num7 = 1;
				num9 = 1;
				do
				{
					if (System.Runtime.CompilerServices.Unsafe.As<Vector2, UIntPtr>(ref vector2) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4))
					{
						vector2 = (Vector2)obj4;
					}
					num9++;
					num7++;
					obj4 += 4;
				}
				while (num7 < array.Length);
				num8 = array.Length;
			}
		}
		else
		{
			vector2 = (Vector2)0;
			nint num7 = array.Length;
			nint num8 = unchecked((nint)null);
			nint num9 = (nint)typeof(float[]);
		}
		object obj5 = vector2 / panZoneThickness;
		float num10 = 1f - (float)obj5;
		if (!(0f > num10))
		{
			if (num10 > 1f)
			{
				num10 = 1f;
			}
		}
		else
		{
			num10 = 0f;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
		float num11;
		if (!(0f > num10))
		{
			bool flag6 = !(num10 > 1f);
			num11 = num10;
			if (!flag6)
			{
				num11 = 1f;
			}
		}
		else
		{
			num11 = 0f;
		}
		float num12 = panMaxSpeed - panMinSpeed;
		bool flag7 = minZoom == maxZoom;
		float num13 = num12 * num11;
		float num14 = num13 + panMinSpeed;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180478B97h\"");
		float num17;
		if (!flag7)
		{
			float num15 = targetZoom - minZoom;
			float num16 = maxZoom - minZoom;
			num17 = num15 / num16;
			if (!(0f > num17))
			{
				if (num17 > 1f)
				{
					num17 = 1f;
				}
				goto IL_05d1;
			}
		}
		num17 = 0f;
		goto IL_05d1;
		IL_05d1:
		if (!(0f > num17))
		{
			if (num17 > 1f)
			{
				num17 = 1f;
			}
		}
		else
		{
			num17 = 0f;
		}
		float num18 = panSpeedMaxZoomMultiplier - panSpeedMinZoomMultiplier;
		float num19 = num18 * num17;
		float num20 = num19 + panSpeedMinZoomMultiplier;
		float num21 = num20 * num14;
		float deltaTime = Time.deltaTime;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
		object obj7 = default(object);
		object obj6 = obj7 ^ 0;
		float num22 = (float)obj6 * num21;
		float num23 = num22 * deltaTime;
		float num24 = num21 * 0f;
		float num25 = num23;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (RTSMapCameraController)+104]");
		float num26 = num25 + 0f;
		float num27 = num24 * deltaTime;
		Vector3 vector3 = default(Vector3);
		float num28 = num27 + (float)vector3;
		Vector3 vector5 = default(Vector3);
		Vector3 vector4 = ClampToMapBoundsSoft((Vector3)(&vector5));
		float num29 = (float)vector3 - num28;
		float num30 = vector4.z - num26;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
		float num31 = vector4.x / boundaryResistStrength;
		if (!(0f > num31))
		{
			if (num31 > 1f)
			{
				num31 = 1f;
			}
		}
		else
		{
			num31 = 0f;
		}
		float num32 = 1f - num31;
		float num33 = num23 * num32;
		float num34 = num33;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (RTSMapCameraController)+104]");
		float num35 = num34 + 0f;
		targetLocalPosition = vector3;
	}

	private void HandleKeyboardPanning()
	{
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Expected O, but got Unknown
		//IL_02b4: Invalid comparison between I4 and F4
		//IL_0093: Expected I, but got O
		//IL_02e2: Expected F4, but got O
		//IL_02e2: Expected F4, but got O
		//IL_030c: Expected F4, but got I
		//IL_030c: Expected F4, but got I
		//IL_017b: Invalid comparison between I4 and F4
		//IL_01c6: Expected F4, but got I4
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Expected O, but got Unknown
		//IL_00da: Expected F8, but got I4
		if (!enableKeyboardPanning)
		{
			return;
		}
		Vector2 vector = ReadVector2(panAction);
		double num2 = default(double);
		double num = num2 * num2;
		object obj = vector * vector;
		double num3 = num + (double)obj;
		if (9.999999747378752E-05 > num3)
		{
			return;
		}
		double num4 = num2 * num2;
		object obj2 = vector * vector;
		double num5 = num4 + (double)obj2;
		bool flag = !(num5 > 1.0);
		Vector2 vector2 = vector;
		double num6 = num2;
		if (!flag)
		{
			nint num7 = (nint)typeof(Math);
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,xmm1\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v249 @ rcx_v12 (Il2CppClass<System.Math>)+E4]");
			double num8;
			if ((nint)0 <= (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtpd xmm0,xmm1\"");
				num8 = 0.0;
			}
			else
			{
				num8 = Math.Sqrt(num5);
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm0,xmm0\"");
			vector2 = vector / num8;
			num6 = num2 / num8;
		}
		bool flag2 = !keyboardPanScaleWithZoom;
		float num9 = keyboardPanBaseSpeed;
		if (!flag2)
		{
			float panSpeedZoomMultiplier = GetPanSpeedZoomMultiplier();
			num9 *= panSpeedZoomMultiplier;
		}
		float deltaTime = Time.deltaTime;
		double num10 = num6;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
		object obj3 = num10 ^ 0;
		float num11 = (float)obj3 * num9;
		float num12 = num11 * deltaTime;
		float num13 = (float)vector2 * num9;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (RTSMapCameraController)+104]");
		float num14 = 0f + num12;
		float num15 = num13 * deltaTime;
		float v = (float)targetLocalPosition + num15;
		if (0f > boundarySoftness || boundarySoftness > 1f)
		{
		}
		float softZone = default(float);
		float num16 = SoftClamp(v, (float)localMin, (float)localMax, softZone);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (RTSMapCameraController)+24]");
		nint num17 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (RTSMapCameraController)+2C]");
		float num18 = SoftClamp(num14, num17, 0f, softZone);
		float num19 = num18 - num14;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
		float num20 = num19 / boundaryResistStrength;
		if (!(0f > num20))
		{
			if (num20 > 1f)
			{
				num20 = 1f;
			}
		}
		else
		{
			num20 = 0f;
		}
		float num21 = 1f - num20;
		float num22 = num12 * num21;
		float num23 = num22;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (RTSMapCameraController)+104]");
		float num24 = num23 + 0f;
		Vector3 vector3 = default(Vector3);
		targetLocalPosition = vector3;
	}

	private unsafe void HandleScrollPanning()
	{
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Expected O, but got Unknown
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Expected O, but got Unknown
		//IL_00ce: Invalid comparison between F4 and O
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Expected O, but got Unknown
		//IL_0312: Expected O, but got Ref
		//IL_021e: Invalid comparison between I4 and F4
		//IL_0269: Expected F4, but got I4
		//IL_01be: Expected F4, but got I4
		//IL_0351: Invalid comparison between I4 and F4
		//IL_0173: Invalid comparison between I4 and F4
		//IL_01fa: Expected F4, but got I4
		if (!enableScrollClickPanning || !(virtualCursor != null))
		{
			return;
		}
		InputAction action = scrollClick.action;
		if (!action.IsPressed())
		{
			return;
		}
		TryEnable(pointerDelta);
		Vector2 vector = ReadVector2(pointerDelta);
		object obj = vector ^ -0f;
		object obj3 = default(object);
		object obj2 = obj3 ^ -0f;
		object obj4 = obj * obj;
		object obj5 = obj2 * obj2;
		object obj6 = obj5 + obj4;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)0.02f) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj6))
		{
			return;
		}
		bool flag = !scrollClickPanScaleWithZoom;
		float num = scrollClickPanBaseSpeed;
		if (flag)
		{
			goto IL_028e;
		}
		bool flag2 = minZoom == maxZoom;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180479213h\"");
		float num4;
		if (!flag2)
		{
			float num2 = targetZoom - minZoom;
			float num3 = maxZoom - minZoom;
			num4 = num2 / num3;
			if (!(0f > num4))
			{
				if (num4 > 1f)
				{
					num4 = 1f;
				}
				goto IL_0348;
			}
		}
		num4 = 0f;
		goto IL_0348;
		IL_0348:
		if (!(0f > num4))
		{
			if (num4 > 1f)
			{
				num4 = 1f;
			}
		}
		else
		{
			num4 = 0f;
		}
		float num5 = panClickSpeedMaxZoomMultiplier - panClickSpeedMinZoomMultiplier;
		float num6 = num5 * num4;
		float num7 = num6 + panClickSpeedMinZoomMultiplier;
		num *= num7;
		goto IL_028e;
		IL_028e:
		float deltaTime = Time.deltaTime;
		object obj7 = obj2 ^ -0f;
		float num8 = (float)obj7 * num;
		float num9 = num8 * deltaTime;
		float num10 = num * 0f;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (RTSMapCameraController)+104]");
		float num11 = 0f + num9;
		float num12 = num10 * deltaTime;
		Vector3 vector2 = default(Vector3);
		float num13 = (float)vector2 + num12;
		Vector3 vector4 = default(Vector3);
		Vector3 vector3 = ClampToMapBoundsSoft((Vector3)(&vector4));
		float num14 = (float)vector2 - num13;
		float num15 = vector3.z - num11;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
		float num16 = vector3.x / boundaryResistStrength;
		if (!(0f > num16))
		{
			if (num16 > 1f)
			{
				num16 = 1f;
			}
		}
		else
		{
			num16 = 0f;
		}
		float num17 = 1f - num16;
		float num18 = num17 * num9;
		float num19 = num18;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (RTSMapCameraController)+104]");
		float num20 = num19 + 0f;
		targetLocalPosition = vector2;
	}

	private unsafe void HandleZoom()
	{
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Expected O, but got Unknown
		//IL_031e: Invalid comparison between O and F4
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Expected O, but got Unknown
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Expected Ref, but got Unknown
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Expected O, but got Unknown
		//IL_042b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0430: Expected O, but got Unknown
		//IL_020b: Expected O, but got Ref
		//IL_046a: Expected O, but got I
		//IL_02c5: Expected O, but got I
		//IL_0117: Expected O, but got Ref
		//IL_014c: Invalid comparison between I4 and F4
		//IL_015e: Expected F4, but got I4
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Expected O, but got Unknown
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_038e: Expected O, but got Unknown
		//IL_02b0: Expected O, but got I
		Vector2 vector = ReadVector2(scrollAction);
		bool flag = !invertScroll;
		Vector3 vector3 = default(Vector3);
		Vector3 vector2 = vector3;
		if (!flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
			vector2 = (Vector3)(vector3 ^ 0);
		}
		Vector3 vector4 = vector2;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj = vector4 & 0;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)0.01f))
		{
			goto IL_017b;
		}
		float num = maxZoom - minZoom;
		vector2 *= scrollSensitivity;
		float num2 = num * (float)vector2;
		float num3 = targetZoom - num2;
		float num4 = minZoom;
		if (!(minZoom > num3))
		{
			num4 = maxZoom;
			if (!(num3 > maxZoom))
			{
				goto IL_0335;
			}
		}
		num3 = num4;
		goto IL_0335;
		IL_045a:
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (RTSMapCameraController)+104]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (RTSMapCameraController)+24]");
		nint num5 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (RTSMapCameraController)+104]");
		if (num5 <= 0)
		{
			object obj3 = obj2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (RTSMapCameraController)+2C]");
			if ((nint)obj3 > 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (RTSMapCameraController)+2C]");
				obj2 = 0;
			}
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (RTSMapCameraController)+24]");
			obj2 = 0;
		}
		Vector2 vector5;
		targetLocalPosition = vector5;
		return;
		IL_017b:
		Vector3 localPosition = cameraChild.localPosition;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
		float deltaTime = Time.deltaTime;
		float maxSpeed = default(float);
		float deltaTime2 = default(float);
		float num6 = Mathf.SmoothDamp(localPosition.x, targetZoom, ref *(float*)(this + 248), zoomSmoothTime, maxSpeed, deltaTime2);
		object obj4 = this + 136;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
		Vector3 vector7 = default(Vector3);
		Vector3 vector6 = ((!(num6 > 1E-05f)) ? Vector3.zeroVector : vector7);
		cameraChild.localPosition = (Vector3)(&vector6);
		Vector2 vector8 = localMin;
		Vector2 vector9 = localMin;
		Vector3 vector10 = targetLocalPosition;
		if (System.Runtime.CompilerServices.Unsafe.As<Vector2, UIntPtr>(ref vector9) <= System.Runtime.CompilerServices.Unsafe.As<Vector3, UIntPtr>(ref vector10))
		{
			vector8 = localMax;
			Vector3 vector11 = targetLocalPosition;
			Vector2 vector12 = localMax;
			bool flag2 = System.Runtime.CompilerServices.Unsafe.As<Vector3, UIntPtr>(ref vector11) <= System.Runtime.CompilerServices.Unsafe.As<Vector2, UIntPtr>(ref vector12);
			vector5 = targetLocalPosition;
			if (flag2)
			{
				goto IL_045a;
			}
		}
		vector5 = vector8;
		goto IL_045a;
		IL_0335:
		targetZoom = num3;
		if (virtualCursor != null && targetZoom > targetZoom)
		{
			Vector3 vector13 = ScreenToLocalMapPoint((Vector3)(&vector6));
			float num7 = targetZoom - targetZoom;
			float num8 = maxZoom - minZoom;
			bool flag3 = !(0f < zoomInOffsetPower);
			float num9 = 0f;
			if (!flag3)
			{
				num9 = zoomInOffsetPower;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj5 = num7 & 0;
			float num10 = (float)obj5 / num8;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
			object obj6 = vector7 - vector7;
			object obj7 = obj6 * zoomInOffsetStrength;
			float num11 = (float)obj7 * num10;
			float num12 = num11 + (float)vector7;
			float num13 = vector13.z;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (RTSMapCameraController)+104]");
			float num14 = num13 - 0f;
			float num15 = num14 * zoomInOffsetStrength;
			targetLocalPosition = vector7;
			float num16 = num15 * num10;
			float num17 = num16;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (RTSMapCameraController)+104]");
			num3 = num17 + 0f;
			vector6 = vector7;
			vector2 = targetLocalPosition;
		}
		goto IL_017b;
	}

	private unsafe void MoveCameraRigRoot()
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Expected Ref, but got Unknown
		//IL_0074: Expected O, but got Ref
		Transform transform = base.transform;
		Transform transform2 = base.transform;
		Vector3 localPosition = transform2.localPosition;
		float deltaTime = Time.deltaTime;
		Vector3 current = default(Vector3);
		Vector3 target = default(Vector3);
		float smoothTime = default(float);
		float maxSpeed = default(float);
		float deltaTime2 = default(float);
		Vector3 vector = Vector3.SmoothDamp(ref current, ref target, ref *(Vector3*)(this + 264), smoothTime, maxSpeed, deltaTime2);
		object obj = default(object);
		transform.localPosition = (Vector3)(&obj);
	}

	private unsafe void ApplyZoomBasedRotation()
	{
		//IL_00a7: Invalid comparison between I4 and F4
		//IL_00b6: Expected O, but got I4
		//IL_01cc: Expected F4, but got I4
		//IL_0036: Expected O, but got I4
		//IL_01d9: Invalid comparison between O and F4
		//IL_00df: Expected O, but got I4
		//IL_010d: Expected F4, but got I4
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Expected Ref, but got Unknown
		//IL_00ff: Expected O, but got I4
		//IL_014e: Expected O, but got Ref
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180477E6Fh\"");
		object obj;
		float num3;
		if (minZoom == maxZoom)
		{
			obj = 0;
		}
		else
		{
			float num = maxZoom - minZoom;
			float num2 = targetZoom - minZoom;
			num3 = num2 / num;
			bool flag = 0f > num3;
			obj = 0;
			if (!flag)
			{
				bool flag2 = !(num3 > 1f);
				obj = 0;
				if (!flag2)
				{
					num3 = 1f;
					obj = 0;
				}
				goto IL_01d1;
			}
		}
		num3 = 0f;
		goto IL_01d1;
		IL_01d1:
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num3))
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
		float num4 = maxAngle - minAngle;
		ref float currentVelocity = ref *(float*)(this + 284);
		float num5 = num4 * num3;
		float target = num5 + minAngle;
		float num6 = Mathf.SmoothDampAngle(targetPitch, target, ref currentVelocity, rotationSmoothTime);
		targetPitch = num6;
		Transform transform = base.transform;
		Vector3 localEulerAngles = transform.localEulerAngles;
		Transform transform2 = base.transform;
		Vector3 euler = default(Vector3);
		Quaternion quaternion = Quaternion.Internal_FromEulerRad(ref euler);
		object obj2 = default(object);
		transform2.localRotation = (Quaternion)(&obj2);
	}

	private unsafe void ApplyPositionalTilt()
	{
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Expected Ref, but got Unknown
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Expected Ref, but got Unknown
		//IL_0060: Expected O, but got I
		//IL_01c0: Expected O, but got Ref
		//IL_00e8: Expected F4, but got I4
		//IL_0326: Expected F4, but got I4
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Expected O, but got Unknown
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Expected Ref, but got Unknown
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Expected Ref, but got Unknown
		Transform transform2;
		object obj6 = default(object);
		Vector3 euler = default(Vector3);
		float num14;
		if (enablePositionalTilt && (bool)cameraChild)
		{
			object obj = localMax - localMin;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (RTSMapCameraController)+2C]");
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (RTSMapCameraController)+24]");
			object obj2 = num - 0;
			Transform transform = base.transform;
			Vector3 localPosition = transform.localPosition;
			float num2;
			if ((nint)obj > 0)
			{
				object obj3 = localPosition.x - localMin;
				object obj4 = obj3 / obj;
				object obj5 = obj4 + obj4;
				num2 = (float)obj5 - 1f;
			}
			else
			{
				num2 = 0f;
			}
			bool flag = (nint)obj2 <= 0;
			float num3 = 0f;
			if (!flag)
			{
				float num4 = localPosition.z;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (RTSMapCameraController)+24]");
				float num5 = num4 - 0f;
				float num6 = num5 / (float)obj2;
				float num7 = num6 + num6;
				num3 = num7 - 1f;
			}
			float target = num2 * maxHorizontalTilt;
			ref float currentVelocity = ref *(float*)(this + 296);
			float target2 = num3 * maxVerticalTilt;
			float num8 = Mathf.SmoothDampAngle(positionalTiltYaw, target, ref currentVelocity, positionalTiltSmoothTime);
			ref float currentVelocity2 = ref *(float*)(this + 300);
			positionalTiltYaw = num8;
			float num9 = Mathf.SmoothDampAngle(positionalTiltPitch, target2, ref currentVelocity2, positionalTiltSmoothTime);
			transform2 = cameraChild;
			positionalTiltPitch = num9;
			float num10 = (float)obj6 * Quaternion.Internal_FromEulerRad(ref euler).x;
			object obj7 = (object)cameraChildBaseRotation * obj6;
			float num11 = num10 + (float)obj7;
			object obj8 = obj6 * obj6;
			float num12 = num11 + (float)obj8;
			object obj9 = obj6 * obj6;
			float num13 = num12 - (float)obj9;
			num14 = num13;
		}
		else
		{
			if (!cameraChild)
			{
				return;
			}
			float num15 = Mathf.SmoothDampAngle(positionalTiltYaw, 0f, ref *(float*)(this + 296), positionalTiltSmoothTime);
			ref float currentVelocity3 = ref *(float*)(this + 300);
			positionalTiltYaw = num15;
			float num16 = Mathf.SmoothDampAngle(positionalTiltPitch, 0f, ref currentVelocity3, positionalTiltSmoothTime);
			transform2 = cameraChild;
			positionalTiltPitch = num16;
			float num17 = (float)obj6 * Quaternion.Internal_FromEulerRad(ref euler).x;
			object obj10 = (object)cameraChildBaseRotation * obj6;
			float num18 = (float)obj10 + num17;
			object obj11 = obj6 * obj6;
			float num19 = num18 + (float)obj11;
			object obj12 = obj6 * obj6;
			float num20 = num19 - (float)obj12;
			num14 = num20;
		}
		transform2.localRotation = (Quaternion)(&num14);
	}

	private unsafe Vector3 ClampToMapBounds(Vector3 localPos)
	{
		//IL_0019: Invalid comparison between O and F4
		//IL_00d0: Expected F4, but got I
		//IL_00dd: Expected F4, but got O
		//IL_00d8: Expected native int or pointer, but got O
		//IL_00ff: Invalid comparison between I and F4
		//IL_0046: Invalid comparison between F4 and O
		//IL_005c: Expected O, but got F4
		//IL_011b: Expected native int or pointer, but got O
		//IL_012d: Expected native int or pointer, but got O
		//IL_013f: Expected native int or pointer, but got O
		//IL_0087: Expected F4, but got I
		//IL_009c: Invalid comparison between F4 and I
		Vector2 vector = localMin;
		Vector2 vector3;
		if (System.Runtime.CompilerServices.Unsafe.As<Vector2, UIntPtr>(ref localMin) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)localPos.x))
		{
			vector = localMax;
			float x = localPos.x;
			Vector2 vector2 = localMax;
			bool flag = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)x) <= System.Runtime.CompilerServices.Unsafe.As<Vector2, UIntPtr>(ref vector2);
			vector3 = (Vector2)localPos.x;
			if (flag)
			{
				goto IL_00c0;
			}
		}
		vector3 = vector;
		goto IL_00c0;
		IL_0113:
		float z;
		((Vector3*)(nint)localPos)->z = z;
		Vector3 vector4 = default(Vector3);
		((Vector3*)(nint)vector4)->x = localPos.x;
		((Vector3*)(nint)vector4)->z = localPos.z;
		return vector4;
		IL_00c0:
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (RTSMapCameraController)+24]");
		float num = 0f;
		((Vector3*)(nint)localPos)->x = (float)vector3;
		z = localPos.z;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (RTSMapCameraController)+24]");
		if (!(0f > localPos.z))
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (RTSMapCameraController)+2C]");
			num = 0f;
			float z2 = localPos.z;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (RTSMapCameraController)+2C]");
			if (!(z2 > 0f))
			{
				goto IL_0113;
			}
		}
		z = num;
		goto IL_0113;
	}

	private unsafe Vector3 ClampToMapBoundsSoft(Vector3 localPos)
	{
		//IL_000b: Invalid comparison between I4 and F4
		//IL_0065: Expected F4, but got O
		//IL_0065: Expected F4, but got O
		//IL_0094: Expected F4, but got I
		//IL_0094: Expected F4, but got I
		//IL_00a0: Expected native int or pointer, but got O
		//IL_00ad: Expected native int or pointer, but got O
		//IL_00ba: Expected native int or pointer, but got O
		//IL_00cc: Expected native int or pointer, but got O
		if (0f > boundarySoftness || boundarySoftness > 1f)
		{
		}
		float softZone = default(float);
		float x = SoftClamp(localPos.x, (float)localMin, (float)localMax, softZone);
		float z = localPos.z;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (RTSMapCameraController)+24]");
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (RTSMapCameraController)+2C]");
		float z2 = SoftClamp(z, num, 0f, softZone);
		((Vector3*)(nint)localPos)->z = z2;
		Vector3 vector = default(Vector3);
		((Vector3*)(nint)vector)->x = x;
		((Vector3*)(nint)localPos)->x = x;
		((Vector3*)(nint)vector)->z = localPos.z;
		return vector;
	}

	private float SoftClamp(float v, float min, float max, float softZone)
	{
		//IL_0228: Invalid comparison between I4 and F4
		//IL_0237: Expected O, but got I4
		//IL_035c: Expected F4, but got I4
		//IL_01bf: Expected O, but got I4
		//IL_0369: Invalid comparison between O and F4
		//IL_0260: Expected O, but got I4
		//IL_028e: Expected F4, but got I4
		//IL_0113: Invalid comparison between I4 and F4
		//IL_0122: Expected O, but got I4
		//IL_0280: Expected O, but got I4
		//IL_0325: Expected F4, but got I4
		//IL_00a2: Expected O, but got I4
		//IL_0332: Invalid comparison between O and F4
		//IL_014b: Expected O, but got I4
		//IL_0179: Expected F4, but got I4
		//IL_016b: Expected O, but got I4
		object obj = default(object);
		float num = min + (float)obj;
		float num3;
		object obj2;
		float num7;
		if (!(num > v))
		{
			float num2 = max - (float)obj;
			bool flag = !(v > num2);
			num3 = v;
			if (flag)
			{
				goto IL_02c1;
			}
			float num4 = max - (float)obj;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018047AD33h\"");
			if (max == num4)
			{
				obj2 = 0;
			}
			else
			{
				float num5 = num4 - max;
				float num6 = v - max;
				num7 = num6 / num5;
				bool flag2 = 0f > num7;
				obj2 = 0;
				if (!flag2)
				{
					bool flag3 = !(num7 > 1f);
					obj2 = 0;
					if (!flag3)
					{
						num7 = 1f;
						obj2 = 0;
					}
					goto IL_032a;
				}
			}
			num7 = 0f;
			goto IL_032a;
		}
		float num8 = min + (float)obj;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018047AD87h\"");
		object obj3;
		if (min == num8)
		{
			obj3 = 0;
		}
		else
		{
			float num9 = num8 - min;
			float num10 = v - min;
			num7 = num10 / num9;
			bool flag4 = 0f > num7;
			obj3 = 0;
			if (!flag4)
			{
				bool flag5 = !(num7 > 1f);
				obj3 = 0;
				if (!flag5)
				{
					num7 = 1f;
					obj3 = 0;
				}
				goto IL_0361;
			}
		}
		num7 = 0f;
		goto IL_0361;
		IL_02ea:
		float num12;
		float num11 = num12 - v;
		float num13 = num11 * num7;
		num3 = v + num13;
		goto IL_02c1;
		IL_032a:
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num7))
		{
			if (num7 > 1f)
			{
				num7 = 1f;
				num12 = max;
				goto IL_02ea;
			}
		}
		else
		{
			num7 = 0f;
		}
		num12 = max;
		goto IL_02ea;
		IL_0361:
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num7))
		{
			if (num7 > 1f)
			{
				num7 = 1f;
			}
		}
		else
		{
			num7 = 0f;
		}
		num12 = min;
		goto IL_02ea;
		IL_02c1:
		if (!(min > num3))
		{
			if (num3 > max)
			{
				return max;
			}
			return num3;
		}
		return min;
	}

	private unsafe Vector3 ScreenToLocalMapPoint(Vector3 screenPos)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0436: Expected F4, but got O
		//IL_0431: Expected native int or pointer, but got O
		//IL_044b: Expected F4, but got I
		//IL_0446: Expected native int or pointer, but got O
		//IL_005d: Expected O, but got Ref
		//IL_0181: Expected O, but got I4
		//IL_0189: Expected O, but got Ref
		//IL_0282: Expected F4, but got I
		//IL_013a: Expected O, but got Ref
		//IL_014b: Expected O, but got F4
		//IL_0158: Expected O, but got F4
		//IL_0161: Expected O, but got I4
		//IL_0169: Expected O, but got Ref
		//IL_0298: Expected F4, but got I4
		//IL_02a1: Expected F4, but got I4
		//IL_02aa: Expected F4, but got I4
		//IL_0676: Expected I, but got O
		//IL_0694: Expected I, but got O
		//IL_04e8: Invalid comparison between O and F4
		//IL_023e: Expected O, but got Ref
		//IL_0265: Expected O, but got I4
		//IL_026d: Expected O, but got Ref
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Expected O, but got Unknown
		//IL_0541: Unknown result type (might be due to invalid IL or missing references)
		//IL_0546: Expected O, but got Unknown
		//IL_0590: Unknown result type (might be due to invalid IL or missing references)
		//IL_0595: Expected O, but got Unknown
		//IL_05f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fc: Expected O, but got Unknown
		//IL_02ea: Expected O, but got I4
		//IL_02f3: Expected O, but got I4
		//IL_02fc: Expected O, but got I4
		//IL_06a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ae: Expected O, but got Unknown
		//IL_061e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0623: Expected O, but got Unknown
		//IL_06e6: Invalid comparison between F4 and O
		//IL_0342: Expected O, but got I4
		//IL_040d: Expected F4, but got O
		//IL_0408: Expected native int or pointer, but got O
		//IL_0422: Expected F4, but got I
		//IL_041d: Expected native int or pointer, but got O
		//IL_036c: Invalid comparison between F4 and I4
		//IL_037b: Invalid comparison between F4 and I4
		//IL_03a4: Expected O, but got I4
		//IL_03e7: Expected native int or pointer, but got O
		//IL_03f4: Expected native int or pointer, but got O
		object obj2 = default(object);
		object obj = (object)(&obj2);
		Camera main = Camera.main;
		Vector3 vector3;
		Vector3 vector4;
		if ((bool)main)
		{
			if ((object)main != null)
			{
				Vector3 vector = default(Vector3);
				Ray ray = main.ScreenPointToRay((Vector3)(&vector));
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v94 @ rax_v11 (UnityEngine.Ray)+10]");
				_ = 0;
				Transform transform = base.transform;
				if ((object)transform != null)
				{
					Transform parent = transform.parent;
					if (!parent)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm6,8\"");
						object obj3 = 0;
						Vector3 vector2 = (Vector3)(&vector);
						vector3 = ray.m_Origin;
						vector4 = ray.m_Origin;
						goto IL_046c;
					}
					Transform transform2 = base.transform;
					if ((object)transform2 != null)
					{
						Transform parent2 = transform2.parent;
						if ((object)parent2 != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm6,8\"");
							Vector3 vector5 = parent2.InverseTransformPoint((Vector3)(&vector));
							vector3 = (Vector3)vector5.x;
							vector4 = (Vector3)vector5.z;
							object obj3 = 0;
							Vector3 vector2 = (Vector3)(&vector);
							goto IL_046c;
						}
					}
				}
			}
			goto IL_0450;
		}
		Vector3 vector6 = default(Vector3);
		((Vector3*)(nint)vector6)->x = (float)targetLocalPosition;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (RTSMapCameraController)+104]");
		((Vector3*)(nint)vector6)->z = 0f;
		goto IL_0663;
		IL_046c:
		Transform transform3 = base.transform;
		float num;
		float num2;
		if ((object)transform3 != null)
		{
			Transform parent3 = transform3.parent;
			if (!parent3)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-7C]");
				num = 0f;
				float num3 = default(float);
				num2 = num3;
				goto IL_0493;
			}
			Transform transform4 = base.transform;
			if ((object)transform4 != null)
			{
				Transform parent4 = transform4.parent;
				if ((object)parent4 != null)
				{
					object obj4 = default(object);
					Vector3 vector7 = parent4.InverseTransformDirection((Vector3)(&obj4));
					num2 = vector7.x;
					num = vector7.z;
					object obj3 = 0;
					Vector3 vector2 = (Vector3)(&obj4);
					goto IL_0493;
				}
			}
		}
		goto IL_0450;
		IL_0493:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
		float num4;
		float num5;
		float num6;
		object obj5 = default(object);
		if (!(num2 > 1E-05f))
		{
			num4 = 0f;
			num5 = 0f;
			num6 = 0f;
		}
		else
		{
			num6 = num / num2;
			num5 = (float)obj5 / num2;
			num4 = num2 / num2;
		}
		nint num7 = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v597 @ rdx_v14 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num8 = 0;
		nint num9 = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v603 @ rdx_v15 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num10 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
		object obj6;
		object obj7;
		object obj8;
		if (System.Runtime.CompilerServices.Unsafe.As<Vector3, UIntPtr>(ref Vector3.zeroVector) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1E-05f))
		{
			obj6 = 0;
			obj7 = 0;
			obj8 = 0;
		}
		else
		{
			obj8 = (object)Vector3.upVector / (object)Vector3.zeroVector;
			obj7 = obj5 / (object)Vector3.zeroVector;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v598 @ rax_v24 (Il2CppStaticFields<UnityEngine.Vector3>)+20]");
			obj6 = 0 / Vector3.zeroVector;
		}
		object obj9 = (object)Vector3.zeroVector * obj8;
		object obj11 = default(object);
		object obj10 = obj11 * obj7;
		object obj13 = default(object);
		object obj12 = obj13 * obj7;
		object obj14 = obj12 + obj9;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v607 @ rax_v26 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		object obj15 = 0 * obj6;
		float num11 = num5 * (float)obj7;
		object obj16 = obj14 + obj15;
		float num12 = num4 * (float)obj8;
		float num13 = num6 * (float)obj6;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
		object obj17 = obj16 ^ 0;
		float num14 = num11 + num12;
		object obj18 = (object)vector3 * obj8;
		float num15 = num14 + num13;
		object obj19 = obj10 + obj18;
		object obj20 = (object)vector4 * obj6;
		object obj21 = obj19 + obj20;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
		object obj22 = obj21 ^ 0;
		object obj23 = obj22 - obj17;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj24 = num15 & 0;
		float num16 = 0f - num15;
		if ((nint)obj24 < 0)
		{
			obj24 = 0;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj25 = num16 & 0;
		float num17 = Mathf.Epsilon * 8f;
		float num18 = (float)obj24 * 1E-06f;
		if (num18 < num17)
		{
			num18 = num17;
		}
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num18) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj25))
		{
			float num19 = (float)obj23 / num15;
			bool flag = num19 < 0f;
			bool flag2 = num19 == 0f;
			bool flag3 = !flag;
			bool flag4 = !flag2;
			object obj26 = flag4 & flag3;
			if (obj26 != null)
			{
				float num20 = num6 * num19;
				float z = num20 + (float)vector4;
				float x = default(float);
				((Vector3*)(nint)vector6)->x = x;
				((Vector3*)(nint)vector6)->z = z;
				goto IL_0663;
			}
		}
		((Vector3*)(nint)vector6)->x = (float)targetLocalPosition;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (RTSMapCameraController)+104]");
		((Vector3*)(nint)vector6)->z = 0f;
		goto IL_0663;
		IL_0663:
		return vector6;
		IL_0450:
		return (Vector3)new NullReferenceException();
	}

	private float GetPanSpeedZoomMultiplier()
	{
		//IL_00d2: Invalid comparison between I4 and F4
		//IL_00e1: Expected O, but got I4
		//IL_017e: Expected F4, but got I4
		//IL_0036: Expected O, but got I4
		//IL_018b: Invalid comparison between O and F4
		//IL_010a: Expected O, but got I4
		//IL_0138: Expected F4, but got I4
		//IL_012a: Expected O, but got I4
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001804787FEh\"");
		object obj;
		float num3;
		if (minZoom == maxZoom)
		{
			obj = 0;
		}
		else
		{
			float num = targetZoom - minZoom;
			float num2 = maxZoom - minZoom;
			num3 = num / num2;
			bool flag = 0f > num3;
			obj = 0;
			if (!flag)
			{
				bool flag2 = !(num3 > 1f);
				obj = 0;
				if (!flag2)
				{
					num3 = 1f;
					obj = 0;
				}
				goto IL_0183;
			}
		}
		num3 = 0f;
		goto IL_0183;
		IL_0183:
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num3))
		{
			if (num3 > 1f)
			{
				float num4 = panSpeedMaxZoomMultiplier - panSpeedMinZoomMultiplier;
				float num5 = num4 * 1f;
				return num5 + panSpeedMinZoomMultiplier;
			}
		}
		else
		{
			num3 = 0f;
		}
		float num6 = panSpeedMaxZoomMultiplier - panSpeedMinZoomMultiplier;
		float num7 = num6 * num3;
		return num7 + panSpeedMinZoomMultiplier;
	}

	private float GetScrollClickPanSpeedZoomMultiplier()
	{
		//IL_00d2: Invalid comparison between I4 and F4
		//IL_00e1: Expected O, but got I4
		//IL_017e: Expected F4, but got I4
		//IL_0036: Expected O, but got I4
		//IL_018b: Invalid comparison between O and F4
		//IL_010a: Expected O, but got I4
		//IL_0138: Expected F4, but got I4
		//IL_012a: Expected O, but got I4
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018047887Eh\"");
		object obj;
		float num3;
		if (minZoom == maxZoom)
		{
			obj = 0;
		}
		else
		{
			float num = targetZoom - minZoom;
			float num2 = maxZoom - minZoom;
			num3 = num / num2;
			bool flag = 0f > num3;
			obj = 0;
			if (!flag)
			{
				bool flag2 = !(num3 > 1f);
				obj = 0;
				if (!flag2)
				{
					num3 = 1f;
					obj = 0;
				}
				goto IL_0183;
			}
		}
		num3 = 0f;
		goto IL_0183;
		IL_0183:
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num3))
		{
			if (num3 > 1f)
			{
				float num4 = panClickSpeedMaxZoomMultiplier - panClickSpeedMinZoomMultiplier;
				float num5 = num4 * 1f;
				return num5 + panClickSpeedMinZoomMultiplier;
			}
		}
		else
		{
			num3 = 0f;
		}
		float num6 = panClickSpeedMaxZoomMultiplier - panClickSpeedMinZoomMultiplier;
		float num7 = num6 * num3;
		return num7 + panClickSpeedMinZoomMultiplier;
	}

	private void TryEnable(InputActionReference actionRef)
	{
		if (!(actionRef != null))
		{
			return;
		}
		InputAction action = actionRef.action;
		if (action != null)
		{
			InputAction action2 = actionRef.action;
			if (!action2.enabled)
			{
				InputAction action3 = actionRef.action;
				action3.Enable();
			}
		}
	}

	private static Vector2 ReadVector2(InputActionReference actionRef)
	{
		if (actionRef != null)
		{
			if ((object)actionRef == null)
			{
				goto IL_00e3;
			}
			InputAction action = actionRef.action;
			if (action != null)
			{
				InputAction action2 = actionRef.action;
				if (action2 == null)
				{
					goto IL_00e3;
				}
				if (action2.enabled)
				{
					InputAction action3 = actionRef.action;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807015E0");
					Vector2 result = default(Vector2);
					return result;
				}
			}
		}
		Vector2 result2 = default(Vector2);
		return result2;
		IL_00e3:
		return (Vector2)new NullReferenceException();
	}

	private unsafe void OnDrawGizmosSelected()
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Expected O, but got Unknown
		//IL_04b7: Expected O, but got Ref
		//IL_009c: Expected O, but got Ref
		//IL_0125: Expected O, but got Ref
		//IL_01ae: Expected O, but got Ref
		//IL_0237: Expected O, but got Ref
		//IL_0249: Expected O, but got Ref
		//IL_0249: Expected O, but got Ref
		//IL_025b: Expected O, but got Ref
		//IL_025b: Expected O, but got Ref
		//IL_0268: Expected O, but got Ref
		//IL_0268: Expected O, but got Ref
		//IL_027a: Expected O, but got Ref
		//IL_027a: Expected O, but got Ref
		//IL_0283: Expected O, but got Ref
		//IL_04cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d1: Expected O, but got Unknown
		//IL_04ec: Invalid comparison between I and F4
		//IL_02e1: Expected O, but got Ref
		//IL_02f2: Expected O, but got Ref
		//IL_02f2: Expected O, but got Ref
		//IL_030e: Expected O, but got Ref
		//IL_030e: Expected O, but got Ref
		//IL_0318: Expected O, but got Ref
		//IL_0395: Expected O, but got Ref
		//IL_03a3: Expected O, but got Ref
		//IL_03b5: Expected O, but got Ref
		//IL_03b5: Expected O, but got Ref
		//IL_03c2: Expected O, but got Ref
		//IL_03c2: Expected O, but got Ref
		//IL_03d8: Expected O, but got I4
		//IL_055d: Invalid comparison between I4 and F4
		//IL_0511: Expected O, but got Ref
		//IL_051a: Unknown result type (might be due to invalid IL or missing references)
		//IL_051f: Expected O, but got Unknown
		//IL_0439: Expected O, but got Ref
		//IL_045c: Expected O, but got Ref
		//IL_046a: Expected O, but got Ref
		//IL_048b: Expected O, but got Ref
		//IL_048b: Expected O, but got Ref
		//IL_04a7: Expected O, but got Ref
		//IL_04a7: Expected O, but got Ref
		object obj2 = default(object);
		object obj = obj2 - 56;
		object obj3 = default(object);
		Gizmos.color = (Color)(&obj3);
		Transform transform = base.transform;
		Transform parent = transform.parent;
		Transform parent2;
		if ((bool)parent)
		{
			Transform transform2 = base.transform;
			parent2 = transform2.parent;
		}
		else
		{
			parent2 = base.transform;
		}
		Vector3 vector2 = default(Vector3);
		Vector3 vector = parent2.TransformPoint((Vector3)(&vector2));
		Transform transform3 = base.transform;
		Transform parent3 = transform3.parent;
		Transform parent4;
		if ((bool)parent3)
		{
			Transform transform4 = base.transform;
			parent4 = transform4.parent;
		}
		else
		{
			parent4 = base.transform;
		}
		Vector3 vector3 = parent4.TransformPoint((Vector3)(&vector2));
		Transform transform5 = base.transform;
		Transform parent5 = transform5.parent;
		Transform parent6;
		if ((bool)parent5)
		{
			Transform transform6 = base.transform;
			parent6 = transform6.parent;
		}
		else
		{
			parent6 = base.transform;
		}
		Vector3 vector4 = parent6.TransformPoint((Vector3)(&vector2));
		Transform transform7 = base.transform;
		Transform parent7 = transform7.parent;
		Transform parent8;
		if ((bool)parent7)
		{
			Transform transform8 = base.transform;
			parent8 = transform8.parent;
		}
		else
		{
			parent8 = base.transform;
		}
		Vector3 vector5 = parent8.TransformPoint((Vector3)(&vector2));
		float num = default(float);
		float num2 = default(float);
		Gizmos.DrawLine((Vector3)(&num), (Vector3)(&num2));
		float num3 = default(float);
		Gizmos.DrawLine((Vector3)(&num), (Vector3)(&num3));
		float num4 = default(float);
		Gizmos.DrawLine((Vector3)(&num), (Vector3)(&num4));
		float num5 = default(float);
		Gizmos.DrawLine((Vector3)(&num), (Vector3)(&num5));
		Gizmos.color = (Color)(&obj3);
		Transform transform9 = base.transform;
		Vector3 position = transform9.position;
		Transform transform10 = base.transform;
		object obj4 = this + 136;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206FD0]");
		Vector3 vector6 = default(Vector3);
		vector2 = ((!(0f > 1E-05f)) ? Vector3.zeroVector : vector6);
		Vector3 vector7 = transform10.TransformDirection((Vector3)(&vector2));
		Vector3 vector8 = default(Vector3);
		Gizmos.DrawLine((Vector3)(&vector2), (Vector3)(&vector8));
		float num6 = default(float);
		float angle = default(float);
		DrawArrowHead((Vector3)(&num6), (Vector3)(&vector8), 0.7f, angle);
		Gizmos.color = (Color)(&obj3);
		float num7 = minZoom * vector7.x;
		object obj5 = default(object);
		float num8 = minZoom * (float)obj5;
		float num9 = num7 + (float)vector8;
		float num10 = num8 + (float)obj5;
		float num11 = maxZoom * vector7.x;
		float num12 = default(float);
		Gizmos.DrawSphere((Vector3)(&num12), 0.3f);
		Gizmos.DrawSphere((Vector3)(&vector8), 0.2f);
		Gizmos.DrawLine((Vector3)(&vector2), (Vector3)(&vector8));
		float num13 = default(float);
		Gizmos.DrawLine((Vector3)(&num13), (Vector3)(&vector8));
		vector8 = vector6;
		object obj6 = 1;
		bool flag;
		do
		{
			float num14 = (float)obj6 * 0.0625f;
			if (0f > num14 || num14 > 1f)
			{
			}
			Gizmos.DrawSphere((Vector3)(&vector8), 0.08f);
			obj6++;
			flag = (nint)obj6 < 16;
			vector8 = vector6;
		}
		while (flag);
		if ((bool)cameraChild)
		{
			Gizmos.color = (Color)(&obj3);
			Vector3 position2 = cameraChild.position;
			Gizmos.DrawSphere((Vector3)(&vector8), 0.25f);
			Gizmos.color = (Color)(&obj3);
			Vector3 forward = cameraChild.forward;
			float num15 = default(float);
			float num16 = default(float);
			Gizmos.DrawLine((Vector3)(&num15), (Vector3)(&num16));
			float num17 = default(float);
			DrawArrowHead((Vector3)(&num17), (Vector3)(&vector8), 0.5f, angle);
		}
	}

	private unsafe void DrawArrowHead(Vector3 pos, Vector3 dir, float size, float angle)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Expected Ref, but got Unknown
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Expected O, but got Unknown
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Expected O, but got Unknown
		//IL_00b3: Expected O, but got F4
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Expected O, but got Unknown
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Expected Ref, but got Unknown
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Expected O, but got Unknown
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Expected O, but got Unknown
		//IL_0172: Expected O, but got F4
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Expected O, but got Unknown
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Expected O, but got Unknown
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Expected O, but got Unknown
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Expected O, but got Unknown
		object obj2 = default(object);
		object obj = obj2 - 79;
		ref Vector3 euler = ref *(Vector3*)(obj - 57);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rbp_v1+77]");
		float num = 0f * ((float)Math.PI / 180f);
		_ = 0;
		_ = 0;
		Quaternion quaternion = Quaternion.Internal_FromEulerRad(ref euler);
		Vector3 vector = (Vector3)(obj - 41);
		Quaternion quaternion2 = (Quaternion)(obj - 25);
		_ = dir.x;
		object obj3 = dir.z ^ -0f;
		_ = quaternion.x;
		Vector3 vector2 = quaternion2 * vector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rbp_v1+77]");
		object obj4 = 0 ^ -0f;
		ref Vector3 euler2 = ref *(Vector3*)(obj - 57);
		float num2 = (float)obj4 * ((float)Math.PI / 180f);
		_ = 0;
		_ = 0;
		Quaternion quaternion3 = Quaternion.Internal_FromEulerRad(ref euler2);
		Vector3 vector3 = (Vector3)(obj - 41);
		Quaternion quaternion4 = (Quaternion)(obj - 25);
		_ = dir.x;
		_ = quaternion3.x;
		object obj5 = dir.z ^ -0f;
		Vector3 vector4 = quaternion4 * vector3;
		_ = vector2.x;
		_ = pos.x;
		Vector3 to = (Vector3)(obj - 41);
		Vector3 vector5 = (Vector3)(obj - 57);
		float num3 = vector2.z * size;
		_ = pos.z;
		float num4 = num3 + pos.z;
		_ = pos.x;
		Gizmos.DrawLine(vector5, to);
		Vector3 to2 = (Vector3)(obj - 41);
		Vector3 vector6 = (Vector3)(obj - 57);
		_ = vector4.x;
		_ = pos.x;
		_ = pos.z;
		float num5 = vector4.z * size;
		_ = pos.x;
		float num6 = num5 + pos.z;
		Gizmos.DrawLine(vector6, to2);
	}

	public RTSMapCameraController()
	{
		//IL_000f: Expected O, but got I8
		//IL_0034: Expected O, but got I4
		localMin = (Vector2)3259498496L;
		Vector3 vector = default(Vector3);
		zoomLocalAxis = vector;
		_ = -1f;
		_ = 3259498496L;
		localMax = (Vector2)1112014848;
		_ = 1112014848;
		boundarySoftness = 0.16f;
		boundaryResistStrength = 2.5f;
		panZoneThickness = 80f;
		panMaxSpeed = 28f;
		panMinSpeed = 2f;
		enableKeyboardPanning = true;
		keyboardPanBaseSpeed = 16f;
		keyboardPanScaleWithZoom = true;
		panSpeedMinZoomMultiplier = 0.5f;
		panSpeedMaxZoomMultiplier = 2f;
		enableScrollClickPanning = true;
		scrollClickPanBaseSpeed = 10f;
		scrollClickPanScaleWithZoom = true;
		panClickSpeedMinZoomMultiplier = 0.25f;
		panClickSpeedMaxZoomMultiplier = 1.5f;
		minZoom = 9f;
		maxZoom = 38f;
		defaultZoom = 22f;
		resetZoomOnEnter = true;
		scrollSensitivity = 0.22f;
		zoomSmoothTime = 0.12f;
		minAngle = 90f;
		maxAngle = 45f;
		rotationSmoothTime = 0.1f;
		zoomInOffsetStrength = 0.55f;
		zoomInOffsetPower = 1f;
		cameraMoveSmoothTime = 0.09f;
		maxHorizontalTilt = 8f;
		maxVerticalTilt = 5f;
		positionalTiltSmoothTime = 0.25f;
		enableActionsOnEnable = true;
		isActive = true;
		base._002Ector();
	}
}
