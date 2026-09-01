using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

public class TurretRotationFollowerRigidbody : MonoBehaviour
{
	private TurretController turret;

	private Rigidbody targetRigidbody;

	private Vector3 localRotationAxis;

	private Vector3 eulerOffsetDegrees;

	private bool invertAngle;

	private bool wrapAngle360;

	private bool useFixedUpdate;

	private bool preserveInitialRotationAsBase;

	private Quaternion initialBaseRotation;

	private bool hasInitialized;

	private void Reset()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		Rigidbody rigidbody = default(Rigidbody);
		targetRigidbody = rigidbody;
	}

	private void Awake()
	{
		//IL_007d: Expected O, but got F4
		if (targetRigidbody == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			Rigidbody rigidbody = default(Rigidbody);
			targetRigidbody = rigidbody;
		}
		Transform transform = base.transform;
		Quaternion rotation = transform.rotation;
		hasInitialized = true;
		initialBaseRotation = (Quaternion)rotation.x;
	}

	private void OnValidate()
	{
		//IL_0068: Expected O, but got F4
		bool isPlaying = Application.isPlaying;
		if (!isPlaying && hasInitialized == isPlaying)
		{
			Transform transform = base.transform;
			initialBaseRotation = (Quaternion)transform.rotation.x;
		}
	}

	private void FixedUpdate()
	{
		if (useFixedUpdate)
		{
			float fixedDeltaTime = Time.fixedDeltaTime;
			ApplyRotation(fixedDeltaTime);
		}
	}

	private void Update()
	{
		if (!useFixedUpdate)
		{
			float deltaTime = Time.deltaTime;
			ApplyRotation(deltaTime);
		}
	}

	private unsafe void ApplyRotation(float dt)
	{
		//IL_0090: Expected O, but got Ref
		//IL_0064: Expected O, but got Ref
		if (turret != null)
		{
			object obj = default(object);
			if (!(targetRigidbody == null))
			{
				Quaternion quaternion = ComputeTargetRotation();
				targetRigidbody.MoveRotation((Quaternion)(&obj));
			}
			else
			{
				Transform transform = base.transform;
				Quaternion quaternion2 = ComputeTargetRotation();
				transform.rotation = (Quaternion)(&obj);
			}
		}
	}

	private unsafe Quaternion ComputeTargetRotation()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_02eb: Expected O, but got I
		//IL_0308: Expected O, but got I
		//IL_032b: Invalid comparison between O and F4
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Expected F4, but got Unknown
		//IL_00db: Invalid comparison between I4 and F4
		//IL_03ac: Expected I, but got O
		//IL_03cc: Expected O, but got I
		//IL_0126: Expected F4, but got I4
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_034d: Expected O, but got Unknown
		//IL_0360: Invalid comparison between O and F4
		//IL_0165: Expected I, but got O
		//IL_0185: Expected O, but got I
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Expected O, but got Unknown
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Expected Ref, but got Unknown
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Expected O, but got Unknown
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Expected Ref, but got Unknown
		//IL_027e: Expected native int or pointer, but got O
		//IL_0264: Expected native int or pointer, but got O
		object obj2 = default(object);
		object obj = obj2 - 95;
		TurretController turretController = turret;
		if ((object)turret != null)
		{
			bool flag = !invertAngle;
			float num = turretController._003CCurrentAngle_003Ek__BackingField;
			if (!flag)
			{
				float num2 = num;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
				num = num2 ^ 0;
			}
			if (wrapAngle360)
			{
				float x = num / 360f;
				float num3 = MathF.Floor(x);
				float num4 = num3 * 360f;
				num -= num4;
				if (!(0f > num))
				{
					if (num > 360f)
					{
						num = 360f;
					}
				}
				else
				{
					num = 0f;
				}
			}
			object obj3 = (object)localRotationAxis * (object)localRotationAxis;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (TurretRotationFollowerRigidbody)+34]");
			nint num5 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (TurretRotationFollowerRigidbody)+34]");
			object obj4 = num5 * 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (TurretRotationFollowerRigidbody)+38]");
			nint num6 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (TurretRotationFollowerRigidbody)+38]");
			object obj5 = num6 * 0;
			object obj6 = obj3 + obj4;
			object obj7 = obj6 + obj5;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj7) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)0.0001f))
			{
				object obj8 = this + 48;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1E-05f))
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (TurretRotationFollowerRigidbody)+38]");
					object obj9 = 0 / obj4;
					object obj10 = obj9;
				}
				else
				{
					nint num7 = (nint)typeof(Vector3);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v360 @ rax_v18 (Il2CppClass<UnityEngine.Vector3>)+B8]");
					nint num8 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v348 @ rcx_v12 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
					object obj10 = 0;
					_ = Vector3.zeroVector;
				}
			}
			else
			{
				nint num9 = (nint)typeof(Vector3);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v316 @ rax_v14 (Il2CppClass<UnityEngine.Vector3>)+B8]");
				nint num10 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v317 @ rcx_v8 (Il2CppStaticFields<UnityEngine.Vector3>)+20]");
				object obj10 = 0;
				_ = Vector3.upVector;
			}
			Transform transform = base.transform;
			if ((object)transform != null)
			{
				Vector3 direction = (Vector3)(obj - 73);
				Vector3 vector = transform.TransformDirection(direction);
				ref Vector3 axis = ref *(Vector3*)(obj - 73);
				_ = vector.x;
				_ = vector.z;
				Quaternion quaternion = Quaternion.Internal_AngleAxis(num, ref axis);
				ref Vector3 euler = ref *(Vector3*)(obj - 73);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (TurretRotationFollowerRigidbody)+44]");
				float num11 = 0f * ((float)Math.PI / 180f);
				_ = eulerOffsetDegrees;
				Quaternion quaternion2 = Quaternion.Internal_FromEulerRad(ref euler);
				Quaternion quaternion3 = default(Quaternion);
				float x2 = default(float);
				if (!preserveInitialRotationAsBase)
				{
					((Quaternion*)(nint)quaternion3)->x = x2;
					return quaternion3;
				}
				((Quaternion*)(nint)quaternion3)->x = x2;
				return quaternion3;
			}
		}
		return (Quaternion)new NullReferenceException();
	}

	public TurretRotationFollowerRigidbody()
	{
		//IL_0013: Expected I, but got O
		//IL_004e: Expected I, but got O
		nint num = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num2 = 0;
		localRotationAxis = Vector3.upVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v28 @ rdx_v1 (Il2CppStaticFields<UnityEngine.Vector3>)+20]");
		_ = 0;
		nint num3 = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v46 @ rax_v5 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num4 = 0;
		eulerOffsetDegrees = Vector3.zeroVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rcx_v3 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		_ = 0;
		wrapAngle360 = true;
		preserveInitialRotationAsBase = true;
		base._002Ector();
	}
}
