using System;
using Cpp2ILInjected;
using UnityEngine;

namespace VLB;

public static class TransformUtils
{
	public struct Packed
	{
		public Vector3 position;

		public Quaternion rotation;

		public Vector3 lossyScale;

		public bool IsSame(Transform transf)
		{
			//IL_0271: Expected I4, but got O
			//IL_022f: Invalid comparison between F4 and I4
			if ((object)transf != null)
			{
				Vector3 vector = transf.position;
				float num = vector.x - (float)position;
				object obj2 = default(object);
				object obj = obj2 - obj2;
				float num2 = vector.z;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (VLB.TransformUtils+Packed)+8]");
				float num3 = num2 - 0f;
				object obj3 = obj * obj;
				float num4 = num * num;
				float num5 = num3 * num3;
				float num6 = (float)obj3 + num4;
				float num7 = num6 + num5;
				if (9.9999994E-11f > num7)
				{
					Quaternion quaternion = transf.rotation;
					object obj4 = obj2 * obj2;
					float num8 = (float)rotation * quaternion.x;
					float num9 = (float)obj4 + num8;
					object obj5 = obj2 * obj2;
					object obj6 = obj2 * obj2;
					float num10 = num9 + (float)obj5;
					float num11 = num10 + (float)obj6;
					if (num11 > 0.999999f)
					{
						Vector3 vector2 = transf.lossyScale;
						float num12 = vector2.x - (float)lossyScale;
						float num13 = vector2.y - (float)obj2;
						float num14 = vector2.z;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (VLB.TransformUtils+Packed)+24]");
						float num15 = num14 - 0f;
						float num16 = num13 * num13;
						float num17 = num12 * num12;
						float num18 = num15 * num15;
						float num19 = num16 + num17;
						float num20 = num19 + num18;
						bool flag = 9.9999994E-11f < num20;
						float num21 = 9.9999994E-11f - num20;
						bool flag2 = num21 == 0f;
						bool flag3 = !flag;
						bool flag4 = !flag2;
						return flag4 & flag3;
					}
				}
				return false;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	public unsafe static Packed GetWorldPacked(Transform self)
	{
		//IL_000e: Expected O, but got I4
		//IL_0009: Expected native int or pointer, but got O
		//IL_0056: Expected O, but got F4
		//IL_0051: Expected native int or pointer, but got O
		//IL_0084: Expected O, but got F4
		//IL_007f: Expected native int or pointer, but got O
		//IL_00a3: Expected O, but got F4
		//IL_009e: Expected native int or pointer, but got O
		Packed packed = default(Packed);
		((Packed*)(nint)packed)->position = (Vector3)0;
		_ = 0;
		_ = 0;
		if ((object)self != null)
		{
			Vector3 position = self.position;
			((Packed*)(nint)packed)->position = (Vector3)position.x;
			_ = position.z;
			((Packed*)(nint)packed)->rotation = (Quaternion)self.rotation.x;
			Vector3 lossyScale = self.lossyScale;
			((Packed*)(nint)packed)->lossyScale = (Vector3)lossyScale.x;
			_ = lossyScale.z;
			return packed;
		}
		return (Packed)new NullReferenceException();
	}
}
