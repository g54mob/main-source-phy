using Cpp2ILInjected;
using UnityEngine;

public class RandomUIRotation : MonoBehaviour
{
	public enum Axis
	{
		X,
		Y,
		Z
	}

	public float minAngle;

	public float maxAngle = 360f;

	public Axis rotationAxis = Axis.Z;

	private unsafe void Awake()
	{
		//IL_0015: Expected O, but got I4
		//IL_008b: Expected O, but got Ref
		float num = Random.Range(minAngle, maxAngle);
		bool flag = rotationAxis == Axis.X;
		if (!flag)
		{
			object obj = rotationAxis - 1;
			if (!flag && (nint)obj != 1)
			{
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		Object obj2 = default(Object);
		if (!(obj2 != null))
		{
			Debug.LogWarning("RandomUIRotation: No RectTransform found on this GameObject.");
		}
		else
		{
			Vector3 vector = default(Vector3);
			((Transform)obj2).localEulerAngles = (Vector3)(&vector);
		}
	}
}
