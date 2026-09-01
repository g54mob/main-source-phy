using System;
using Cpp2ILInjected;
using UnityEngine;

public class Translator : MonoBehaviour
{
	public float Speed;

	public float Amplitude;

	public Vector3 Direction;

	public bool AlongOwnAxis;

	public float SinOffset;

	public bool ResetOnDisable;

	protected float _angleInRad;

	protected Vector3 _startPos;

	private void Awake()
	{
		//IL_002b: Expected O, but got F4
		Transform transform = base.transform;
		Vector3 localPosition = transform.localPosition;
		_startPos = (Vector3)localPosition.x;
		_ = localPosition.z;
	}

	private unsafe void Update()
	{
		//IL_0082: Expected O, but got I
		//IL_008b: Expected O, but got I4
		//IL_00f7: Expected O, but got Ref
		//IL_00c2: Expected O, but got Ref
		//IL_00c2: Expected O, but got Ref
		//IL_00dd: Expected O, but got Ref
		//IL_00e5: Expected O, but got Ref
		float deltaTime = Time.deltaTime;
		bool flag = !AlongOwnAxis;
		float num = Speed * 0.03f;
		float num2 = deltaTime * num;
		float num3 = num2 * 60f;
		float angleInRad = num3 + _angleInRad;
		_angleInRad = angleInRad;
		IntPtr intPtr = default(IntPtr);
		Quaternion quaternion = (Quaternion)(nint)intPtr;
		object obj = 0;
		Vector3 direction = default(Vector3);
		if (!flag)
		{
			Transform transform = base.transform;
			Quaternion localRotation = transform.localRotation;
			object obj2 = default(object);
			Vector3 vector = (Quaternion)(&obj2) * (Vector3)(&direction);
			direction = Direction;
			quaternion = (Quaternion)(&obj2);
			object obj3 = default(object);
			obj = (object)(&obj3);
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E400");
		Transform transform2 = base.transform;
		transform2.localPosition = (Vector3)(&direction);
	}

	private unsafe void OnDisable()
	{
		//IL_003b: Expected O, but got Ref
		if (ResetOnDisable)
		{
			Transform transform = base.transform;
			object obj = default(object);
			transform.localPosition = (Vector3)(&obj);
			_angleInRad = 0f;
		}
	}

	public void Toggle()
	{
		bool flag = base.enabled;
		bool flag2 = (byte)((flag ? 1u : 0u) ^ 1u) != 0;
		base.enabled = flag2;
	}

	public Translator()
	{
		//IL_0029: Expected I, but got O
		Speed = 1f;
		Amplitude = 1f;
		nint num = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num2 = 0;
		Direction = Vector3.upVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v29 @ rdx_v1 (Il2CppStaticFields<UnityEngine.Vector3>)+20]");
		_ = 0;
		ResetOnDisable = true;
		base._002Ector();
	}
}
