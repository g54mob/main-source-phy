using NaughtyAttributes;
using UnityEngine;

public class NonSerializedProperties : MonoBehaviour
{
	[ShowNonSerializedField]
	private int myInt = 10;

	[ShowNonSerializedField]
	private const float PI = 3.14159f;

	[ShowNonSerializedField]
	private static readonly Vector3 CONST_VECTOR = Vector3.one;

	[ShowNativeProperty]
	public Transform Transform => base.transform;
}
