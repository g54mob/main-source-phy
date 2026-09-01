using Cpp2ILInjected;
using UnityEngine;

public class WorldUpChild : MonoBehaviour
{
	public enum LocalAxis
	{
		PositiveY,
		NegativeY,
		PositiveX,
		NegativeX,
		PositiveZ,
		NegativeZ
	}

	private LocalAxis localUpAxis;

	private Vector3 worldUpDirection;

	private bool smoothCorrection;

	private float smoothSpeed;

	private bool correctionEnabled;

	public bool CorrectionEnabled
	{
		get
		{
			return correctionEnabled;
		}
		set
		{
			correctionEnabled = value;
		}
	}

	private unsafe void LateUpdate()
	{
		//IL_004d: Expected O, but got I
		//IL_006a: Expected O, but got I
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Expected O, but got Unknown
		//IL_01a9: Invalid comparison between I4 and F4
		//IL_0106: Expected O, but got Ref
		//IL_017b: Expected O, but got Ref
		if (!correctionEnabled)
		{
			return;
		}
		object obj = (object)worldUpDirection * (object)worldUpDirection;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (WorldUpChild)+28]");
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (WorldUpChild)+28]");
		object obj2 = num * 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (WorldUpChild)+2C]");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (WorldUpChild)+2C]");
		object obj3 = num2 * 0;
		object obj4 = obj + obj2;
		object obj5 = obj4 + obj3;
		if ((nint)obj5 > 0)
		{
			object obj6 = this + 36;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
			if (!(0f > 1E-05f))
			{
			}
		}
		Vector3 localAxisVector = GetLocalAxisVector();
		Vector3 fromDirection = default(Vector3);
		Vector3 toDirection = default(Vector3);
		Quaternion quaternion = Quaternion.Internal_FromToRotation(ref fromDirection, ref toDirection);
		Transform transform = base.transform;
		Quaternion rotation = transform.rotation;
		Quaternion rotation2;
		Transform transform3;
		if (!smoothCorrection)
		{
			Transform transform2 = base.transform;
			float num3 = default(float);
			rotation2 = (Quaternion)(&num3);
			transform3 = transform2;
		}
		else
		{
			Transform transform4 = base.transform;
			Transform transform5 = base.transform;
			Quaternion rotation3 = transform5.rotation;
			float deltaTime = Time.deltaTime;
			float t = deltaTime * smoothSpeed;
			Quaternion a = default(Quaternion);
			Quaternion b = default(Quaternion);
			Quaternion quaternion2 = Quaternion.Internal_Slerp(ref a, ref b, t);
			Quaternion quaternion3 = default(Quaternion);
			rotation2 = (Quaternion)(&quaternion3);
			transform3 = transform4;
		}
		transform3.rotation = rotation2;
	}

	private unsafe Vector3 GetLocalAxisVector()
	{
		//IL_0009: Expected native int or pointer, but got O
		//IL_0017: Expected native int or pointer, but got O
		//IL_002d: Expected O, but got I4
		Vector3 vector = default(Vector3);
		((Vector3*)(nint)vector)->x = 0f;
		((Vector3*)(nint)vector)->z = 0f;
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 22 Invalid \"Jump target not found in method: 0x180556451\"");
		return (Vector3)localUpAxis;
	}

	public WorldUpChild()
	{
		//IL_0013: Expected I, but got O
		nint num = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num2 = 0;
		worldUpDirection = Vector3.upVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v29 @ rdx_v1 (Il2CppStaticFields<UnityEngine.Vector3>)+20]");
		_ = 0;
		smoothSpeed = 12f;
		correctionEnabled = true;
		base._002Ector();
	}
}
