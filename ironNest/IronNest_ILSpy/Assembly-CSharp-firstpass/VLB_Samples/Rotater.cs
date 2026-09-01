using Cpp2ILInjected;
using UnityEngine;

namespace VLB_Samples;

public class Rotater : MonoBehaviour
{
	public Vector3 EulerSpeed;

	private unsafe void Update()
	{
		//IL_0037: Expected O, but got Ref
		//IL_0073: Expected O, but got Ref
		Transform transform = base.transform;
		Quaternion rotation = transform.rotation;
		Quaternion rotation2 = default(Quaternion);
		Vector3 vector = Quaternion.Internal_ToEulerRad(ref rotation2);
		Vector3 euler = default(Vector3);
		Vector3 vector2 = Quaternion.Internal_MakePositive((Vector3)(&euler));
		float deltaTime = Time.deltaTime;
		Transform transform2 = base.transform;
		Quaternion quaternion = Quaternion.Internal_FromEulerRad(ref euler);
		float num = default(float);
		transform2.rotation = (Quaternion)(&num);
	}

	public Rotater()
	{
		//IL_0013: Expected I, but got O
		nint num = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num2 = 0;
		EulerSpeed = Vector3.zeroVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v29 @ rdx_v1 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		_ = 0;
		base._002Ector();
	}
}
