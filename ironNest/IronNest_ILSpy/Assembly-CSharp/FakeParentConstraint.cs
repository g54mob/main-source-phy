using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

public class FakeParentConstraint : MonoBehaviour
{
	public Transform fakeParent;

	public bool constraintActive;

	private Vector3 positionOffset;

	private Quaternion rotationOffset;

	private bool previousConstraintActive;

	private void OnEnable()
	{
		previousConstraintActive = constraintActive;
		if (~(constraintActive ? 1u : 0u) == 0 && fakeParent != null)
		{
			CacheOffset();
		}
		else if (!constraintActive)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 88 Invalid \"Jump target not found in method: 0x1803EAA90\"");
		}
	}

	private void Update()
	{
		bool flag = fakeParent == null;
		if (!flag)
		{
			if (constraintActive == flag)
			{
				goto IL_008f;
			}
			if (previousConstraintActive == flag)
			{
				CacheOffset();
			}
			else if (!constraintActive)
			{
				goto IL_008f;
			}
			goto IL_00b9;
		}
		return;
		IL_00b9:
		previousConstraintActive = constraintActive;
		if (~(constraintActive ? 1u : 0u) == 0)
		{
			ApplyConstraint();
		}
		return;
		IL_008f:
		if (previousConstraintActive)
		{
			ResetToLocalZero();
		}
		goto IL_00b9;
	}

	private unsafe void CacheOffset()
	{
		//IL_0008: Expected O, but got Ref
		//IL_00ac: Expected O, but got Ref
		//IL_00ba: Expected O, but got Ref
		//IL_0116: Expected O, but got F4
		object obj2 = default(object);
		object obj = (object)(&obj2);
		if (fakeParent != null)
		{
			Quaternion rotation = fakeParent.rotation;
			ref Quaternion rotation2 = ref System.Runtime.CompilerServices.Unsafe.As<object, Quaternion>(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 57));
			_ = rotation.x;
			Quaternion quaternion = Quaternion.Internal_Inverse(ref rotation2);
			Transform transform = base.transform;
			Vector3 position = transform.position;
			_ = position.x;
			Vector3 position2 = fakeParent.position;
			Vector3 vector = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 73));
			Quaternion quaternion2 = (Quaternion)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 41));
			_ = quaternion.x;
			_ = position2.x;
			float num = position.z - position2.z;
			Vector3 vector2 = quaternion2 * vector;
			positionOffset = (Vector3)vector2.x;
			_ = vector2.z;
			Quaternion rotation3 = fakeParent.rotation;
			ref Quaternion rotation4 = ref System.Runtime.CompilerServices.Unsafe.As<object, Quaternion>(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 41));
			_ = rotation3.x;
			Quaternion quaternion3 = Quaternion.Internal_Inverse(ref rotation4);
			Transform transform2 = base.transform;
			Quaternion rotation5 = transform2.rotation;
			Quaternion quaternion4 = default(Quaternion);
			rotationOffset = quaternion4;
		}
	}

	private unsafe void ApplyConstraint()
	{
		//IL_0063: Expected O, but got Ref
		//IL_0063: Expected O, but got Ref
		//IL_007f: Expected O, but got Ref
		//IL_00af: Expected O, but got Ref
		if (fakeParent != null)
		{
			Transform transform = base.transform;
			Vector3 position = fakeParent.position;
			Quaternion rotation = fakeParent.rotation;
			float num = default(float);
			object obj = default(object);
			Vector3 vector = (Quaternion)(&num) * (Vector3)(&obj);
			float num2 = default(float);
			transform.position = (Vector3)(&num2);
			Transform transform2 = base.transform;
			Quaternion rotation2 = fakeParent.rotation;
			transform2.rotation = (Quaternion)(&num);
		}
	}

	private unsafe void ResetToLocalZero()
	{
		//IL_001d: Expected O, but got Ref
		//IL_003e: Expected O, but got Ref
		Transform transform = base.transform;
		Vector3 vector = default(Vector3);
		transform.localPosition = (Vector3)(&vector);
		Transform transform2 = base.transform;
		transform2.localRotation = (Quaternion)(&vector);
	}
}
