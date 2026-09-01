using System;
using Cpp2ILInjected;
using UnityEngine;

namespace VLB;

[Serializable]
public struct MinMaxRangeFloat : IEquatable<MinMaxRangeFloat>
{
	private float m_MinValue;

	private float m_MaxValue;

	public float minValue => m_MinValue;

	public float maxValue => m_MaxValue;

	public float randomValue => UnityEngine.Random.Range(m_MinValue, m_MaxValue);

	public Vector2 asVector2
	{
		get
		{
			Vector2 result = default(Vector2);
			return result;
		}
	}

	public float GetLerpedValue(float lerp01)
	{
		//IL_0009: Invalid comparison between I4 and F4
		float num = default(float);
		if (!(0f > num) && num > 1f)
		{
			float num2 = m_MaxValue - m_MinValue;
			float num3 = num2 * 1f;
			return num3 + m_MinValue;
		}
		float num4 = m_MaxValue - m_MinValue;
		float num5 = num4 * 0f;
		return num5 + m_MinValue;
	}

	public MinMaxRangeFloat(float min, float max)
	{
		m_MinValue = min;
		m_MaxValue = max;
	}

	public override bool Equals(object obj)
	{
		//IL_0013: Expected I, but got O
		//IL_0057: Expected I, but got O
		//IL_00a4: Invalid comparison between F4 and O
		//IL_00d7: Invalid comparison between F4 and I
		if (obj != null)
		{
			nint num = (nint)typeof(MinMaxRangeFloat);
			bool flag = (object)obj.GetType() != typeof(MinMaxRangeFloat);
			object obj2 = null;
			if (!flag)
			{
				obj2 = obj;
			}
			if (obj2 != null)
			{
				nint num2 = (nint)obj;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v141 @ rcx_v3 (Il2CppClass<System.Object>)+40]");
				nint num3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ rdx_v2 (Il2CppClass<VLB.MinMaxRangeFloat>)+40]");
				if (num3 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
					bool result = default(bool);
					return result;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_object_unbox\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018037FCD0h\"");
				object obj3 = default(object);
				if ((object)m_MinValue == obj3)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018037FCD0h\"");
					float num4 = m_MaxValue;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v58 @ rax_v8+4]");
					if (num4 == 0f)
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	public bool Equals(MinMaxRangeFloat other)
	{
		//IL_0014: Invalid comparison between F4 and O
		//IL_003f: Invalid comparison between F4 and O
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018037FD13h\"");
		if ((object)m_MinValue == (object)other)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018037FD13h\"");
			object obj = default(object);
			if ((object)m_MaxValue == obj)
			{
				return true;
			}
		}
		return false;
	}

	public unsafe override int GetHashCode()
	{
		//IL_0016: Expected F4, but got Ref
		//IL_0016: Expected F4, but got Ref
		object obj = default(object);
		object obj2 = default(object);
		(float, float) tuple = ((nint)(&obj), (nint)(&obj2));
		(float, float) tuple2 = default((float, float));
		return tuple2.GetHashCode();
	}

	public static bool operator ==(MinMaxRangeFloat lhs, MinMaxRangeFloat rhs)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018037FE91h\"");
		if ((object)lhs == (object)rhs)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018037FE91h\"");
			object obj = default(object);
			object obj2 = default(object);
			if (obj == obj2)
			{
				return true;
			}
		}
		return false;
	}

	public static bool operator !=(MinMaxRangeFloat lhs, MinMaxRangeFloat rhs)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018037FED1h\"");
		if ((object)lhs == (object)rhs)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018037FED1h\"");
			object obj = default(object);
			object obj2 = default(object);
			if (obj == obj2)
			{
				return false;
			}
		}
		return true;
	}
}
