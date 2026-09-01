using Cpp2ILInjected;
using UnityEngine;

namespace Koenigz.PerfectCulling.Demos;

public class FlyCamera : MonoBehaviour
{
	private float MouseSensitivity = 90f;

	private float m_rotationX;

	private float m_rotationY;

	private unsafe void LateUpdate()
	{
		//IL_00ab: Expected O, but got Ref
		//IL_01a1: Expected O, but got Ref
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39EAA]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		float deltaTime = Time.deltaTime;
		if (Input.GetKeyInt(KeyCode.LeftShift))
		{
		}
		Transform transform = base.transform;
		Vector3 localPosition = transform.localPosition;
		Transform transform2 = base.transform;
		Vector3 forward = transform2.forward;
		float axis = Input.GetAxis("Vertical");
		Transform transform3 = base.transform;
		Vector3 right = transform3.right;
		float axis2 = Input.GetAxis("Horizontal");
		Vector3 axis3 = default(Vector3);
		transform.localPosition = (Vector3)(&axis3);
		float axis4 = Input.GetAxis("Mouse X");
		float num = axis4 * MouseSensitivity;
		float num2 = num * deltaTime;
		float rotationX = num2 + m_rotationX;
		m_rotationX = rotationX;
		float axis5 = Input.GetAxis("Mouse Y");
		float num3 = axis5 * MouseSensitivity;
		float num4 = num3 * deltaTime;
		float num5 = num4 + m_rotationY;
		bool flag = -90f > num5;
		float num6 = -90f;
		if (!flag)
		{
			bool flag2 = !(num5 > 90f);
			num6 = 90f;
			if (flag2)
			{
				goto IL_01dc;
			}
		}
		num5 = num6;
		goto IL_01dc;
		IL_01dc:
		m_rotationY = num5;
		Transform transform4 = base.transform;
		Quaternion quaternion = Quaternion.Internal_AngleAxis(m_rotationX, ref axis3);
		Quaternion quaternion2 = Quaternion.Internal_AngleAxis(m_rotationY, ref axis3);
		float num7 = default(float);
		transform4.localRotation = (Quaternion)(&num7);
	}
}
