using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace TMPro.Examples;

public class CameraController : MonoBehaviour
{
	public enum CameraModes
	{
		Follow,
		Isometric,
		Free
	}

	private Transform cameraTransform;

	private Transform dummyTarget;

	public Transform CameraTarget;

	public float FollowDistance;

	public float MaxFollowDistance;

	public float MinFollowDistance;

	public float ElevationAngle;

	public float MaxElevationAngle;

	public float MinElevationAngle;

	public float OrbitalAngle;

	public CameraModes CameraMode;

	public bool MovementSmoothing;

	public bool RotationSmoothing;

	private bool previousSmoothing;

	public float MovementSmoothingValue;

	public float RotationSmoothingValue;

	public float MoveSensitivity;

	private Vector3 currentVelocity;

	private Vector3 desiredPosition;

	private float mouseX;

	private float mouseY;

	private Vector3 moveVector;

	private float mouseWheel;

	private const string event_SmoothingValue = "Slider - Smoothing Value";

	private const string event_FollowDistance = "Slider - Camera Zoom";

	private void Awake()
	{
		//IL_0017: Expected I4, but got I8
		int vSyncCount = QualitySettings.vSyncCount;
		int targetFrameRate = (int)((vSyncCount > 0) ? 60 : 4294967295L);
		Application.targetFrameRate = targetFrameRate;
		RuntimePlatform platform = Application.platform;
		if (platform != RuntimePlatform.IPhonePlayer)
		{
			RuntimePlatform platform2 = Application.platform;
			if (platform2 != RuntimePlatform.Android)
			{
				goto IL_0092;
			}
		}
		Input.simulateMouseWithTouches = false;
		goto IL_0092;
		IL_0092:
		Transform transform = base.transform;
		cameraTransform = transform;
		previousSmoothing = MovementSmoothing;
	}

	private void Start()
	{
		if (CameraTarget == null)
		{
			GameObject gameObject = new GameObject("Camera Target");
			Transform transform = gameObject.transform;
			dummyTarget = transform;
			CameraTarget = dummyTarget;
		}
	}

	private unsafe void LateUpdate()
	{
		//IL_0008: Expected O, but got Ref
		//IL_020e: Expected O, but got Ref
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Expected O, but got Unknown
		//IL_0233: Expected O, but got Ref
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Expected Ref, but got Unknown
		//IL_0297: Expected O, but got Ref
		//IL_00f2: Expected O, but got Ref
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Expected O, but got Unknown
		//IL_0117: Expected O, but got Ref
		//IL_0359: Expected O, but got Ref
		//IL_014f: Expected O, but got Ref
		//IL_04cd: Expected I, but got O
		//IL_0415: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		GetPlayerInput();
		if (!(CameraTarget != null))
		{
			return;
		}
		Vector3 vector3;
		float z;
		Vector3 vector4;
		Vector3 vector5 = default(Vector3);
		if (CameraMode != CameraModes.Isometric)
		{
			if (CameraMode != CameraModes.Follow)
			{
				goto IL_043f;
			}
			Vector3 position = CameraTarget.position;
			ref Vector3 euler = ref System.Runtime.CompilerServices.Unsafe.As<object, Vector3>(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 9));
			_ = 0;
			_ = position.x;
			float num = ElevationAngle * ((float)Math.PI / 180f);
			float num2 = OrbitalAngle * ((float)Math.PI / 180f);
			Quaternion quaternion = Quaternion.Internal_FromEulerRad(ref euler);
			Vector3 vector = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 7));
			float followDistance = FollowDistance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
			object obj3 = followDistance ^ 0;
			Quaternion quaternion2 = (Quaternion)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 39));
			_ = quaternion.x;
			Vector3 vector2 = quaternion2 * vector;
			Vector3 direction = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 7));
			_ = vector2.x;
			_ = vector2.z;
			vector3 = CameraTarget.TransformDirection(direction);
			float x = vector3.x;
			z = position.z;
			vector4 = vector5;
		}
		else
		{
			Vector3 position2 = CameraTarget.position;
			ref Vector3 euler2 = ref System.Runtime.CompilerServices.Unsafe.As<object, Vector3>(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 9));
			float num3 = ElevationAngle * ((float)Math.PI / 180f);
			_ = 0;
			float num4 = OrbitalAngle * ((float)Math.PI / 180f);
			Quaternion quaternion3 = Quaternion.Internal_FromEulerRad(ref euler2);
			Vector3 vector6 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 23));
			float followDistance2 = FollowDistance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
			object obj4 = followDistance2 ^ 0;
			Quaternion quaternion4 = (Quaternion)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 39));
			_ = quaternion3.x;
			vector3 = quaternion4 * vector6;
			_ = position2.x;
			float x = vector3.x;
			z = position2.z;
			vector4 = vector5;
		}
		float num5 = z + vector3.z;
		desiredPosition = vector4;
		goto IL_043f;
		IL_043f:
		Vector3 position3;
		if (!MovementSmoothing)
		{
			position3 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 23));
			_ = desiredPosition;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (TMPro.Examples.CameraController)+7C]");
			_ = 0;
		}
		else
		{
			Vector3 position4 = cameraTransform.position;
			_ = desiredPosition;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (TMPro.Examples.CameraController)+7C]");
			_ = 0;
			_ = position4.x;
			_ = position4.z;
			float fixedDeltaTime = Time.fixedDeltaTime;
			float deltaTime = Time.deltaTime;
			float smoothTime = default(float);
			float maxSpeed = default(float);
			float deltaTime2 = default(float);
			Vector3 vector7 = Vector3.SmoothDamp(ref System.Runtime.CompilerServices.Unsafe.As<object, Vector3>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 7)), ref System.Runtime.CompilerServices.Unsafe.As<object, Vector3>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 23)), ref *(Vector3*)(this + 104), smoothTime, maxSpeed, deltaTime2);
			position3 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 9));
			_ = vector7.x;
			_ = vector7.z;
		}
		cameraTransform.position = position3;
		if (!RotationSmoothing)
		{
			cameraTransform.LookAt(CameraTarget);
			return;
		}
		Quaternion rotation = cameraTransform.rotation;
		Vector3 position5 = CameraTarget.position;
		_ = position5.x;
		Vector3 position6 = cameraTransform.position;
		_ = position6.x;
		float num6 = position5.z - position6.z;
		_ = 0;
		_ = 0;
		nint num7 = (nint)typeof(Vector3);
		ref Vector3 upwards = ref System.Runtime.CompilerServices.Unsafe.As<object, Vector3>(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 9));
		ref Vector3 forward = ref System.Runtime.CompilerServices.Unsafe.As<object, Vector3>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 23));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v689 @ rax_v15 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num8 = 0;
		_ = Vector3.upVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v693 @ rax_v16 (Il2CppStaticFields<UnityEngine.Vector3>)+20]");
		_ = 0;
		Quaternion quaternion5 = Quaternion.Internal_LookRotation(ref forward, ref upwards);
		_ = rotation.x;
		_ = quaternion5.x;
		float deltaTime3 = Time.deltaTime;
		float t = deltaTime3 * RotationSmoothingValue;
		Quaternion quaternion6 = Quaternion.Internal_Lerp(ref System.Runtime.CompilerServices.Unsafe.As<object, Quaternion>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 23)), ref System.Runtime.CompilerServices.Unsafe.As<object, Quaternion>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 39)), t);
		Quaternion rotation2 = (Quaternion)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 39));
		_ = quaternion6.x;
		cameraTransform.rotation = rotation2;
	}

	private unsafe void GetPlayerInput()
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Expected O, but got Unknown
		//IL_0af8: Expected native int or pointer, but got O
		//IL_0b24: Expected native int or pointer, but got O
		//IL_0b38: Expected native int or pointer, but got O
		//IL_0b46: Expected native int or pointer, but got O
		//IL_0d95: Expected I, but got O
		//IL_0bd8: Invalid comparison between I4 and F4
		//IL_075e: Invalid comparison between I4 and F4
		//IL_07d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d9: Expected O, but got Unknown
		//IL_07e6: Expected native int or pointer, but got O
		//IL_0805: Expected native int or pointer, but got O
		//IL_0817: Expected native int or pointer, but got O
		//IL_0829: Expected native int or pointer, but got O
		//IL_0846: Unknown result type (might be due to invalid IL or missing references)
		//IL_084b: Expected O, but got Unknown
		//IL_04ee: Expected O, but got Ref
		//IL_0500: Unknown result type (might be due to invalid IL or missing references)
		//IL_0505: Expected Ref, but got Unknown
		//IL_051c: Expected O, but got Ref
		//IL_088f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0894: Expected O, but got Unknown
		//IL_08b1: Expected O, but got I
		//IL_08c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c6: Expected O, but got Unknown
		//IL_08eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f0: Expected O, but got Unknown
		//IL_034a: Invalid comparison between I and F4
		//IL_091a: Unknown result type (might be due to invalid IL or missing references)
		//IL_091f: Expected O, but got Unknown
		//IL_0941: Expected O, but got I
		//IL_0565: Unknown result type (might be due to invalid IL or missing references)
		//IL_056a: Expected O, but got Unknown
		//IL_0c26: Invalid comparison between I4 and F4
		//IL_096b: Invalid comparison between O and F4
		//IL_036f: Invalid comparison between F4 and I
		//IL_0707: Expected O, but got F4
		//IL_0724: Expected O, but got F4
		//IL_05a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ab: Expected O, but got Unknown
		//IL_0c75: Invalid comparison between I and F4
		//IL_0988: Invalid comparison between F4 and O
		//IL_073d: Expected O, but got Ref
		//IL_0686: Expected O, but got Ref
		//IL_0ca3: Invalid comparison between I4 and F4
		//IL_0420: Invalid comparison between F4 and I
		//IL_06ae: Expected O, but got Ref
		object obj = default(object);
		Touch touch = (Touch)(obj - 344);
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		((Touch*)(nint)touch)->m_AzimuthAngle = 0f;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		((Touch*)(nint)touch)->m_FingerId = 0;
		_ = 0;
		((Touch*)(nint)touch)->m_TapCount = 0;
		((Touch*)(nint)touch)->m_maximumPossiblePressure = 0f;
		nint num = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v59 @ rax_v5 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num2 = 0;
		moveVector = Vector3.zeroVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v67 @ rcx_v3 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		_ = 0;
		float axis = Input.GetAxis("Mouse ScrollWheel");
		mouseWheel = axis;
		int touchCount = Input.touchCount;
		if (!Input.GetKeyInt(KeyCode.LeftShift) && !Input.GetKeyInt(KeyCode.RightShift) && touchCount <= 0)
		{
			goto IL_074b;
		}
		float num3 = mouseWheel * 10f;
		mouseWheel = num3;
		if (Input.GetKeyDownInt(KeyCode.I))
		{
			CameraMode = CameraModes.Isometric;
		}
		if (Input.GetKeyDownInt(KeyCode.F))
		{
			CameraMode = CameraModes.Follow;
		}
		if (Input.GetKeyDownInt(KeyCode.S))
		{
			bool movementSmoothing = !MovementSmoothing;
			MovementSmoothing = movementSmoothing;
		}
		if (!Input.GetMouseButton(1))
		{
			goto IL_0bc5;
		}
		float axis2 = Input.GetAxis("Mouse Y");
		mouseY = axis2;
		float num4 = (mouseX = Input.GetAxis("Mouse X"));
		if (!(mouseY > 0.01f) && !(-0.01f > mouseY))
		{
			goto IL_0bef;
		}
		float num5 = mouseY * MoveSensitivity;
		float num6 = ElevationAngle - num5;
		float num7 = MinElevationAngle;
		if (!(MinElevationAngle > num6))
		{
			num7 = MaxElevationAngle;
			if (!(num6 > MaxElevationAngle))
			{
				goto IL_0c0c;
			}
		}
		num6 = num7;
		goto IL_0c0c;
		IL_0d14:
		float axis3 = Input.GetAxis("Mouse Y");
		mouseY = axis3;
		float x = (mouseX = Input.GetAxis("Mouse X"));
		float z = default(float);
		Vector3 vector = cameraTransform.TransformDirection(x, mouseY, z);
		moveVector = (Vector3)vector.x;
		_ = vector.z;
		object obj2 = vector.z ^ -0f;
		float x2 = default(float);
		dummyTarget.Translate((Vector3)(&x2), Space.World);
		int num8 = 0;
		goto IL_074b;
		IL_0d49:
		if (!(-0.01f > mouseWheel) && !(mouseWheel > 0.01f))
		{
			return;
		}
		float num9 = mouseWheel * 5f;
		float num10 = FollowDistance - num9;
		float num11 = MinFollowDistance;
		if (!(MinFollowDistance > num10))
		{
			num11 = MaxFollowDistance;
			if (!(num10 > MaxFollowDistance))
			{
				goto IL_0d78;
			}
		}
		num10 = num11;
		goto IL_0d78;
		IL_0bc5:
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp near ptr 000000018038C950h\"");
		float num13;
		if ((float)touchCount == 1f)
		{
			Touch touch2 = Input.GetTouch(0);
			_ = touch2.m_TapCount;
			_ = touch2.m_maximumPossiblePressure;
			_ = touch2.m_AzimuthAngle;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D2E6A0");
			object obj3 = default(object);
			if ((nint)obj3 == 1)
			{
				Touch touch3 = Input.GetTouch(0);
				_ = touch3.m_TapCount;
				_ = touch3.m_maximumPossiblePressure;
				_ = touch3.m_AzimuthAngle;
				Touch touch4 = default(Touch);
				Vector2 deltaPosition = touch4.deltaPosition;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (UnityEngine.Touch)+164]");
				if (!(0f > 0.01f))
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (UnityEngine.Touch)+164]");
					if (!(-0.01f > 0f))
					{
						goto IL_0c64;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (UnityEngine.Touch)+164]");
				float num12 = 0f * 0.1f;
				num13 = ElevationAngle - num12;
				float num14 = MinElevationAngle;
				if (!(MinElevationAngle > num13))
				{
					num14 = MaxElevationAngle;
					if (!(num13 > MaxElevationAngle))
					{
						goto IL_0c89;
					}
				}
				num13 = num14;
				goto IL_0c89;
			}
		}
		goto IL_0c3d;
		IL_0bef:
		if (num4 > 0.01f || -0.01f > num4)
		{
			float num15 = num4 * MoveSensitivity;
			float num16 = (OrbitalAngle = num15 + OrbitalAngle);
			if (num16 > 360f)
			{
				float orbitalAngle = num16 - 360f;
				OrbitalAngle = orbitalAngle;
			}
			if (0f > OrbitalAngle)
			{
				float orbitalAngle2 = OrbitalAngle + 360f;
				OrbitalAngle = orbitalAngle2;
			}
		}
		goto IL_0bc5;
		IL_0c0c:
		ElevationAngle = num6;
		goto IL_0bef;
		IL_0d78:
		FollowDistance = num10;
		return;
		IL_0c3d:
		Vector3 origin = default(Vector3);
		if (Input.GetMouseButton(0))
		{
			Camera main = Camera.main;
			Vector3 mousePosition = Input.mousePosition;
			Ray ray = main.ScreenPointToRay((Vector3)(&x2));
			bool flag = Physics.Raycast((Ray)(&origin), out *(RaycastHit*)(touch + 80), 300f, 23552);
			bool flag2 = !flag;
			origin = ray.m_Origin;
			x2 = mousePosition.x;
			num8 = 23552;
			if (!flag2)
			{
				RaycastHit raycastHit = (RaycastHit)(touch + 80);
				Transform transform = ((RaycastHit*)raycastHit)->transform;
				if (transform != CameraTarget)
				{
					RaycastHit raycastHit2 = (RaycastHit)(touch + 80);
					Transform cameraTarget = ((RaycastHit*)raycastHit2)->transform;
					CameraTarget = cameraTarget;
					MovementSmoothing = previousSmoothing;
				}
				OrbitalAngle = 0f;
				origin = ray.m_Origin;
				x2 = mousePosition.x;
				num8 = 23552;
			}
		}
		if (!Input.GetMouseButton(2))
		{
			goto IL_074b;
		}
		if (dummyTarget != null)
		{
			if (!(dummyTarget != CameraTarget))
			{
				goto IL_0d14;
			}
		}
		else
		{
			GameObject gameObject = new GameObject("Camera Target");
			Transform transform2 = gameObject.transform;
			dummyTarget = transform2;
		}
		Vector3 position = CameraTarget.position;
		dummyTarget.position = (Vector3)(&x2);
		Quaternion rotation = CameraTarget.rotation;
		dummyTarget.rotation = (Quaternion)(&origin);
		CameraTarget = dummyTarget;
		previousSmoothing = MovementSmoothing;
		MovementSmoothing = false;
		goto IL_0d14;
		IL_074b:
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp near ptr 000000018038CE43h\"");
		if ((float)touchCount != 2f)
		{
			goto IL_0d49;
		}
		Touch touch5 = Input.GetTouch(0);
		_ = touch5.m_FingerId;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v299 @ rax_v11 (UnityEngine.Touch)+10]");
		_ = 0;
		_ = touch5.m_TapCount;
		_ = touch5.m_maximumPossiblePressure;
		_ = touch5.m_AzimuthAngle;
		Touch touch6 = Input.GetTouch(1);
		object obj4 = touch - 80;
		((Touch*)(nint)touch)->m_FingerId = touch6.m_FingerId;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v384 @ rax_v13 (UnityEngine.Touch)+10]");
		_ = 0;
		((Touch*)(nint)touch)->m_TapCount = touch6.m_TapCount;
		((Touch*)(nint)touch)->m_maximumPossiblePressure = touch6.m_maximumPossiblePressure;
		((Touch*)(nint)touch)->m_AzimuthAngle = touch6.m_AzimuthAngle;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181E6D020");
		Touch touch7 = (Touch)(touch - 80);
		Vector2 deltaPosition2 = ((Touch*)touch7)->deltaPosition;
		Vector2 position2 = ((Touch*)touch)->position;
		Vector2 deltaPosition3 = ((Touch*)touch)->deltaPosition;
		object obj5 = touch + 352;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (UnityEngine.Touch)+164]");
		nint num17 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (UnityEngine.Touch)+174]");
		object obj6 = num17 - 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (UnityEngine.Touch)+17C]");
		object obj8 = default(object);
		object obj7 = 0 - obj8;
		object obj9 = obj6 - obj7;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180371560");
		object obj10 = touch - 80;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181E6D020");
		Vector2 position3 = ((Touch*)touch)->position;
		object obj11 = touch + 352;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (UnityEngine.Touch)+164]");
		nint num18 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (UnityEngine.Touch)+174]");
		object obj12 = num18 - 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180371560");
		object obj13 = obj7 - obj7;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj13) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)0.01f))
		{
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)(-0.01f)) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj13))
			{
				goto IL_0d49;
			}
		}
		float num19 = (float)obj13 * 0.25f;
		float num20 = MinFollowDistance;
		float num21 = num19 + FollowDistance;
		if (!(MinFollowDistance > num21))
		{
			num20 = MaxFollowDistance;
			if (!(num21 > MaxFollowDistance))
			{
				goto IL_0d68;
			}
		}
		num21 = num20;
		goto IL_0d68;
		IL_0c89:
		ElevationAngle = num13;
		goto IL_0c64;
		IL_0c64:
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (UnityEngine.Touch)+160]");
		if (!(0f > 0.01f))
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (UnityEngine.Touch)+160]");
			if (!(-0.01f > 0f))
			{
				goto IL_0c3d;
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (UnityEngine.Touch)+160]");
		float num22 = 0f * 0.1f;
		float num23 = (OrbitalAngle = num22 + OrbitalAngle);
		if (num23 > 360f)
		{
			float orbitalAngle3 = num23 - 360f;
			OrbitalAngle = orbitalAngle3;
		}
		if (0f > OrbitalAngle)
		{
			float orbitalAngle4 = OrbitalAngle + 360f;
			OrbitalAngle = orbitalAngle4;
		}
		goto IL_0c3d;
		IL_0d68:
		FollowDistance = num21;
		goto IL_0d49;
	}

	public CameraController()
	{
		//IL_0076: Expected I, but got O
		FollowDistance = 30f;
		MaxFollowDistance = 100f;
		MinFollowDistance = 2f;
		ElevationAngle = 30f;
		MaxElevationAngle = 85f;
		MovementSmoothing = true;
		MovementSmoothingValue = 25f;
		RotationSmoothingValue = 5f;
		MoveSensitivity = 2f;
		nint num = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num2 = 0;
		currentVelocity = Vector3.zeroVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v29 @ rcx_v2 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		_ = 0;
		base._002Ector();
	}
}
