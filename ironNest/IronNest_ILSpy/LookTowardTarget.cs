using System;
using Cpp2ILInjected;
using UnityEngine;

public class LookTowardTarget : MonoBehaviour
{
	private Transform _target;

	private float _weight;

	private bool _captureBaseOnEnable = true;

	private float _smoothTime;

	private Quaternion _baseLocalRotation;

	private float _smoothedWeight;

	private float _weightVelocity;

	public float Weight
	{
		get
		{
			return _weight;
		}
		set
		{
			//IL_0009: Invalid comparison between I4 and F4
			//IL_0018: Expected F4, but got I4
			bool flag = 0f > value;
			float weight = 0f;
			if (!flag)
			{
				bool flag2 = value > 1f;
				weight = 1f;
				if (!flag2)
				{
					_weight = value;
					return;
				}
			}
			_weight = weight;
		}
	}

	public Transform Target
	{
		get
		{
			return _target;
		}
		set
		{
			_target = value;
		}
	}

	public unsafe void ResetBaseRotation()
	{
		//IL_0073: Expected O, but got Ref
		//IL_0073: Expected O, but got Ref
		//IL_008a: Expected I, but got O
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Expected O, but got Unknown
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Expected O, but got Unknown
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Expected O, but got Unknown
		//IL_0202: Expected F4, but got O
		//IL_01d0: Expected O, but got F4
		//IL_01e5: Invalid comparison between I4 and F4
		//IL_0061: Expected F4, but got I4
		Transform transform = base.transform;
		Quaternion localRotation = transform.localRotation;
		float num = default(float);
		Vector3 upwards = default(Vector3);
		Vector3 vector = (Quaternion)(&num) * (Vector3)(&upwards);
		nint num2 = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v196 @ rdx_v4 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v199 @ rax_v10 (Il2CppStaticFields<UnityEngine.Vector3>)+20]");
		object obj2 = default(object);
		object obj = obj2 * 0;
		float num4 = vector.z * (float)Vector3.upVector;
		object obj3 = obj2 * (object)Vector3.upVector;
		object obj4 = default(object);
		float num5 = vector.z * (float)obj4;
		float num6 = (float)obj - num5;
		float x = vector.x;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v199 @ rax_v10 (Il2CppStaticFields<UnityEngine.Vector3>)+20]");
		object obj5 = x * 0;
		object obj6 = vector.x * obj4;
		float num7 = num4 - (float)obj5;
		float num8 = num6 * num6;
		object obj7 = obj6 - obj3;
		float num9 = num7 * num7;
		object obj8 = obj7 * obj7;
		float num10 = num9 + num8;
		float num11 = num10 + (float)obj8;
		Vector3 forward = default(Vector3);
		float num12 = ((0.0001f > num11) ? ((float)Quaternion.identityQuaternion) : Quaternion.Internal_LookRotation(ref forward, ref upwards).x);
		_baseLocalRotation = (Quaternion)num12;
		float num13 = _weight;
		if (!(0f > _weight))
		{
			if (num13 > 1f)
			{
				_smoothedWeight = 1f;
				return;
			}
		}
		else
		{
			num13 = 0f;
		}
		_smoothedWeight = num13;
	}

	private void OnEnable()
	{
		//IL_0035: Invalid comparison between I4 and F4
		//IL_0044: Expected F4, but got I4
		if (_captureBaseOnEnable)
		{
			ResetBaseRotation();
		}
		bool flag = 0f > _weight;
		float smoothedWeight = 0f;
		if (!flag)
		{
			bool flag2 = _weight > 1f;
			smoothedWeight = 1f;
			if (!flag2)
			{
				_smoothedWeight = _weight;
				return;
			}
		}
		_smoothedWeight = smoothedWeight;
	}

	private unsafe void LateUpdate()
	{
		//IL_0015: Invalid comparison between I4 and F4
		//IL_0060: Expected F4, but got I4
		//IL_00c9: Invalid comparison between F4 and I4
		//IL_00bd: Expected O, but got Ref
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Expected Ref, but got Unknown
		float num = _weight;
		if (!(0f > _weight))
		{
			if (num > 1f)
			{
				num = 1f;
			}
		}
		else
		{
			num = 0f;
		}
		if (_smoothTime > 0f)
		{
			float deltaTime = Time.deltaTime;
			float maxSpeed = default(float);
			float deltaTime2 = default(float);
			float num2 = Mathf.SmoothDamp(_smoothedWeight, num, ref *(float*)(this + 72), _smoothTime, maxSpeed, deltaTime2);
			num = num2;
		}
		_smoothedWeight = num;
		Transform transform = base.transform;
		Quaternion quaternion = ComputeDesiredLocalRotation();
		object obj = default(object);
		transform.localRotation = (Quaternion)(&obj);
	}

	private unsafe void ApplyRotation()
	{
		//IL_0015: Invalid comparison between I4 and F4
		//IL_0060: Expected F4, but got I4
		//IL_00c9: Invalid comparison between F4 and I4
		//IL_00bd: Expected O, but got Ref
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Expected Ref, but got Unknown
		float num = _weight;
		if (!(0f > _weight))
		{
			if (num > 1f)
			{
				num = 1f;
			}
		}
		else
		{
			num = 0f;
		}
		if (_smoothTime > 0f)
		{
			float deltaTime = Time.deltaTime;
			float maxSpeed = default(float);
			float deltaTime2 = default(float);
			float num2 = Mathf.SmoothDamp(_smoothedWeight, num, ref *(float*)(this + 72), _smoothTime, maxSpeed, deltaTime2);
			num = num2;
		}
		_smoothedWeight = num;
		Transform transform = base.transform;
		Quaternion quaternion = ComputeDesiredLocalRotation();
		object obj = default(object);
		transform.localRotation = (Quaternion)(&obj);
	}

	private unsafe Quaternion ComputeDesiredLocalRotation()
	{
		//IL_02a4: Expected F4, but got O
		//IL_029f: Expected native int or pointer, but got O
		//IL_028c: Expected O, but got F4
		if (_target != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
			object obj = default(object);
			if (obj == null)
			{
				if ((object)_target != null)
				{
					Vector3 position = _target.position;
					Transform transform = base.transform;
					if ((object)transform != null)
					{
						Vector3 position2 = transform.position;
						float num = position.z - position2.z;
						float num2 = position.x - position2.x;
						object obj3 = default(object);
						Quaternion quaternion = default(Quaternion);
						object obj2 = obj3 - (object)quaternion;
						float num3 = num * num;
						object obj4 = obj2 * obj2;
						float num4 = num2 * num2;
						float num5 = (float)obj4 + num4;
						float num6 = num5 + num3;
						if (0.0001f > num6)
						{
							goto IL_0200;
						}
						Vector3 forward = default(Vector3);
						Vector3 upwards = default(Vector3);
						Quaternion quaternion2 = Quaternion.Internal_LookRotation(ref forward, ref upwards);
						Transform transform2 = base.transform;
						if ((object)transform2 != null)
						{
							Transform parent = transform2.parent;
							if (!(parent != null))
							{
								goto IL_025b;
							}
							Transform transform3 = base.transform;
							if ((object)transform3 != null)
							{
								Transform parent2 = transform3.parent;
								if ((object)parent2 != null)
								{
									Quaternion rotation = parent2.rotation;
									goto IL_025b;
								}
							}
						}
					}
				}
				return (Quaternion)new NullReferenceException();
			}
		}
		goto IL_0200;
		IL_0200:
		Quaternion quaternion3 = _baseLocalRotation;
		goto IL_0297;
		IL_0297:
		Quaternion quaternion4 = default(Quaternion);
		((Quaternion*)(nint)quaternion4)->x = (float)quaternion3;
		return quaternion4;
		IL_025b:
		Quaternion rotation2 = default(Quaternion);
		Quaternion quaternion5 = Quaternion.Internal_Inverse(ref rotation2);
		Quaternion a = default(Quaternion);
		Quaternion b = default(Quaternion);
		quaternion3 = (Quaternion)Quaternion.Internal_Slerp(ref a, ref b, _smoothedWeight).x;
		goto IL_0297;
	}

	private unsafe static Quaternion StripRoll(Quaternion q)
	{
		//IL_001c: Expected O, but got Ref
		//IL_001c: Expected O, but got Ref
		//IL_0033: Expected I, but got O
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Expected O, but got Unknown
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Expected O, but got Unknown
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Expected O, but got Unknown
		//IL_018a: Expected F4, but got O
		//IL_0177: Expected native int or pointer, but got O
		object obj = default(object);
		Vector3 upwards = default(Vector3);
		Vector3 vector = (Quaternion)(&obj) * (Vector3)(&upwards);
		nint num = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v78 @ rdx_v2 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v81 @ rax_v7 (Il2CppStaticFields<UnityEngine.Vector3>)+20]");
		object obj3 = default(object);
		object obj2 = obj3 * 0;
		object obj4 = default(object);
		float num3 = vector.z * (float)obj4;
		float num4 = vector.z * (float)Vector3.upVector;
		object obj5 = obj3 * (object)Vector3.upVector;
		float num5 = (float)obj2 - num3;
		object obj6 = vector.x * obj4;
		float x = vector.x;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v81 @ rax_v7 (Il2CppStaticFields<UnityEngine.Vector3>)+20]");
		object obj7 = x * 0;
		object obj8 = obj6 - obj5;
		float num6 = num5 * num5;
		float num7 = num4 - (float)obj7;
		object obj9 = obj8 * obj8;
		float num8 = num7 * num7;
		float num9 = num8 + num6;
		float num10 = num9 + (float)obj9;
		Vector3 forward = default(Vector3);
		float x2 = ((0.0001f > num10) ? ((float)Quaternion.identityQuaternion) : Quaternion.Internal_LookRotation(ref forward, ref upwards).x);
		Quaternion quaternion = default(Quaternion);
		((Quaternion*)(nint)quaternion)->x = x2;
		return quaternion;
	}
}
