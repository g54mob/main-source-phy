using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator.Examples;

public class Rotator : MonoBehaviour
{
	public float Speed;

	public Vector3 Axis;

	private unsafe void Update()
	{
		//IL_004a: Expected O, but got Ref
		Transform transform = base.transform;
		float deltaTime = Time.deltaTime;
		float num = deltaTime * Speed;
		float angle = num * 60f;
		object obj = default(object);
		transform.Rotate((Vector3)(&obj), angle);
	}

	public Rotator()
	{
		//IL_001e: Expected I, but got O
		Speed = 1f;
		nint num = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num2 = 0;
		Axis = Vector3.upVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v29 @ rdx_v1 (Il2CppStaticFields<UnityEngine.Vector3>)+20]");
		_ = 0;
		base._002Ector();
	}
}
