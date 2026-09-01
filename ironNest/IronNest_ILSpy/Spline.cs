using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

public class Spline : MonoBehaviour
{
	private Transform _start;

	private Transform _middle;

	private Transform _end;

	private bool showGizmos;

	public string jumpUpTrigger;

	public string jumpDownTrigger;

	private unsafe Vector3 CalculatePosition(float value01, Vector3 startPos, Vector3 endPos, Vector3 midPos)
	{
		//IL_0009: Invalid comparison between I4 and F4
		//IL_0054: Expected F4, but got I4
		//IL_012a: Invalid comparison between I4 and F4
		//IL_009a: Expected F4, but got I4
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Expected O, but got Unknown
		//IL_0184: Invalid comparison between I4 and F4
		//IL_00e0: Expected F4, but got I4
		//IL_01b5: Expected O, but got I
		//IL_01e4: Invalid comparison between I4 and F4
		//IL_011c: Expected F4, but got I4
		//IL_022d: Expected native int or pointer, but got O
		//IL_023a: Expected native int or pointer, but got O
		float num = default(float);
		if (!(0f > num))
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
		float num2 = ((0f > num) ? 0f : ((num > 1f) ? 1f : num));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ stack_30+8]");
		object obj = 0 - startPos.z;
		float num3 = (float)obj * num2;
		float num4 = num3 + startPos.z;
		float num5 = ((0f > num) ? 0f : ((num > 1f) ? 1f : num));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v128 @ stack_28+8]");
		nint num6 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ stack_30+8]");
		object obj2 = num6 - 0;
		float num7 = (float)obj2 * num5;
		float num8 = num7;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ stack_30+8]");
		float num9 = num8 + 0f;
		if (!(0f > num))
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
		float num10 = num9 - num4;
		float num11 = num10 * num;
		float z = num11 + num4;
		Vector3 vector = default(Vector3);
		float x = default(float);
		((Vector3*)(nint)vector)->x = x;
		((Vector3*)(nint)vector)->z = z;
		return vector;
	}

	public unsafe Vector3 CalculatePosition(float interpolationAmount01)
	{
		//IL_00be: Expected O, but got Ref
		//IL_00d4: Expected native int or pointer, but got O
		//IL_00e6: Expected native int or pointer, but got O
		if ((object)_start != null)
		{
			Vector3 position = _start.position;
			if ((object)_end != null)
			{
				Vector3 position2 = _end.position;
				if ((object)_middle != null)
				{
					Vector3 position3 = _middle.position;
					object obj = default(object);
					Vector3 endPos = default(Vector3);
					Vector3 midPos = default(Vector3);
					Vector3 vector = CalculatePosition(interpolationAmount01, (Vector3)(&obj), endPos, midPos);
					Vector3 vector2 = default(Vector3);
					((Vector3*)(nint)vector2)->x = vector.x;
					((Vector3*)(nint)vector2)->z = vector.z;
					return vector2;
				}
			}
		}
		return (Vector3)new NullReferenceException();
	}

	public unsafe Vector3 CalculatePositionCustomStart(float interpolationAmount01, Vector3 startPosition)
	{
		//IL_008b: Expected O, but got Ref
		//IL_00a1: Expected native int or pointer, but got O
		//IL_00b3: Expected native int or pointer, but got O
		if ((object)_end != null)
		{
			Vector3 position = _end.position;
			if ((object)_middle != null)
			{
				Vector3 position2 = _middle.position;
				object obj = default(object);
				Vector3 endPos = default(Vector3);
				Vector3 midPos = default(Vector3);
				Vector3 vector = CalculatePosition(interpolationAmount01, (Vector3)(&obj), endPos, midPos);
				Vector3 vector2 = default(Vector3);
				((Vector3*)(nint)vector2)->x = vector.x;
				((Vector3*)(nint)vector2)->z = vector.z;
				return vector2;
			}
		}
		return (Vector3)new NullReferenceException();
	}

	public unsafe Vector3 CalculatePositionCustomEnd(float interpolationAmount01, Vector3 endPosition)
	{
		//IL_008b: Expected O, but got Ref
		//IL_00a1: Expected native int or pointer, but got O
		//IL_00b3: Expected native int or pointer, but got O
		if ((object)_start != null)
		{
			Vector3 position = _start.position;
			if ((object)_middle != null)
			{
				Vector3 position2 = _middle.position;
				object obj = default(object);
				Vector3 endPos = default(Vector3);
				Vector3 midPos = default(Vector3);
				Vector3 vector = CalculatePosition(interpolationAmount01, (Vector3)(&obj), endPos, midPos);
				Vector3 vector2 = default(Vector3);
				((Vector3*)(nint)vector2)->x = vector.x;
				((Vector3*)(nint)vector2)->z = vector.z;
				return vector2;
			}
		}
		return (Vector3)new NullReferenceException();
	}

	public unsafe void SetPoints(Vector3 startPoint, Vector3 midPointPosition, Vector3 endPoint)
	{
		//IL_009a: Expected O, but got Ref
		//IL_00af: Expected O, but got Ref
		//IL_00c3: Expected O, but got Ref
		if (_start != null && _middle != null && _end != null)
		{
			float num = default(float);
			_start.position = (Vector3)(&num);
			_middle.position = (Vector3)(&num);
			_end.position = (Vector3)(&num);
		}
	}

	private unsafe void OnDrawGizmos()
	{
		//IL_0008: Expected O, but got Ref
		//IL_00a5: Expected O, but got Ref
		//IL_00dd: Expected O, but got Ref
		//IL_0121: Expected O, but got Ref
		//IL_0165: Expected O, but got Ref
		//IL_019a: Expected O, but got Ref
		//IL_01b9: Expected O, but got I4
		//IL_030b: Expected O, but got Ref
		//IL_032d: Expected O, but got Ref
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Expected O, but got Unknown
		//IL_0285: Expected O, but got Ref
		//IL_0293: Expected O, but got Ref
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Expected O, but got Unknown
		object obj2 = default(object);
		object obj = (object)(&obj2);
		if (!showGizmos || !(_start != null) || !(_middle != null) || !(_end != null))
		{
			return;
		}
		Color color = (Color)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 39));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C40]");
		_ = 0;
		Gizmos.color = color;
		Vector3 position = _start.position;
		Vector3 center = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 9));
		_ = position.x;
		_ = position.z;
		Gizmos.DrawSphere(center, 0.1f);
		Vector3 position2 = _end.position;
		Vector3 center2 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 9));
		_ = position2.x;
		_ = position2.z;
		Gizmos.DrawSphere(center2, 0.1f);
		Vector3 position3 = _middle.position;
		Vector3 center3 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 9));
		_ = position3.x;
		_ = position3.z;
		Gizmos.DrawSphere(center3, 0.1f);
		Color color2 = (Color)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 39));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206FE0]");
		_ = 0;
		Gizmos.color = color2;
		object obj3 = 0;
		Vector3 vector;
		while (true)
		{
			if (obj3 == null)
			{
				Vector3 position4 = _start.position;
				float x = position4.x;
				float z = position4.z;
			}
			else
			{
				float interpolationAmount = (float)obj3 / 5f;
				vector = CalculatePosition(interpolationAmount);
				float x = vector.x;
				float z = vector.z;
				if ((nint)obj3 == 5)
				{
					break;
				}
			}
			object obj4 = obj3 + 1;
			float interpolationAmount2 = (float)obj4 / 5f;
			Vector3 vector2 = CalculatePosition(interpolationAmount2);
			_ = vector2.z;
			Vector3 to = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
			Vector3 vector3 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 9));
			_ = vector2.x;
			Gizmos.DrawLine(vector3, to);
			obj3++;
			if ((nint)obj3 >= 5)
			{
				return;
			}
		}
		Vector3 position5 = _end.position;
		_ = vector.x;
		Vector3 to2 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
		_ = vector.z;
		_ = position5.z;
		Vector3 vector4 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 9));
		_ = position5.x;
		Gizmos.DrawLine(vector4, to2);
	}

	public Spline()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A8B1]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		showGizmos = true;
		jumpUpTrigger = "JumpUp";
		jumpDownTrigger = "JumpDown";
		base._002Ector();
	}
}
