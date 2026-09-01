using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

[Serializable]
public class CatFloor
{
	public string floorName;

	public List<Transform> spots;

	public Transform defaultSpot;

	public unsafe float GetAverageY()
	{
		//IL_0108: Expected F4, but got I4
		//IL_0022: Expected F4, but got I4
		//IL_0044: Expected O, but got I4
		//IL_0174: Expected F4, but got O
		//IL_017c: Expected O, but got Ref
		//IL_006a: Expected F4, but got O
		//IL_0072: Expected O, but got Ref
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Expected O, but got Unknown
		List<Transform> list = spots;
		bool flag = spots == null;
		float num = 0f;
		if (!flag)
		{
			bool flag2 = list._size == 0;
			float result = 0f;
			if (!flag2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
				object obj = 0;
				List<Transform>.Enumerator enumerator = default(List<Transform>.Enumerator);
				Transform transform = default(Transform);
				List<Transform>.Enumerator enumerator2 = default(List<Transform>.Enumerator);
				CatFloor catFloor;
				while (enumerator.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					bool flag3 = (object)transform == null;
					num = (float)enumerator2;
					catFloor = (CatFloor)(&enumerator);
					if (!flag3)
					{
						obj += transform.position.y;
						continue;
					}
					throw new NullReferenceException();
				}
				enumerator.Dispose();
				List<Transform> list2 = spots;
				bool flag4 = spots == null;
				num = (float)enumerator2;
				catFloor = (CatFloor)(&enumerator);
				if (flag4)
				{
					goto IL_00d4;
				}
				float num2 = (float)obj / (float)list2._size;
				result = num2;
			}
			return result;
		}
		goto IL_00d4;
		IL_00d4:
		throw new NullReferenceException();
	}

	public CatFloor()
	{
		List<Transform> list = new List<Transform>();
		spots = list;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
	}
}
