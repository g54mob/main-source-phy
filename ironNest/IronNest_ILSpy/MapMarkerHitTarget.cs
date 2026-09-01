using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using Shapes;
using UnityEngine;
using UnityEngine.Events;

public class MapMarkerHitTarget : MonoBehaviour
{
	private Disc disc;

	private bool ringOnly = true;

	private float ringHitWidthLocalUnits = 0.1f;

	private float ringHitPaddingLocalUnits = 0.02f;

	private bool enableLineBandHitTest;

	private float lineBandHalfWidthLocalUnits = 0.06f;

	private float lineBandPaddingLocalUnits = 0.01f;

	private bool requireMinimumDistanceForLineHit = true;

	private float minimumDistanceForLineHit = 0.02f;

	private RectTransform[] extraRectHitAreas;

	public UnityEvent onHoverEnter;

	public UnityEvent onHoverExit;

	private bool _003CIsHovered_003Ek__BackingField;

	public bool IsHovered
	{
		get
		{
			return _003CIsHovered_003Ek__BackingField;
		}
		private set
		{
			_003CIsHovered_003Ek__BackingField = value;
		}
	}

	public unsafe bool HitTest(Vector2 pointerScreen, Camera canvasCamera, Vector2 pointerLocalOnMap, Vector2 markerOriginLocalOnMap, float markerDistanceLocalUnits, float markerAngleDegrees)
	{
		//IL_0076: Invalid comparison between I4 and F4
		//IL_033b: Expected O, but got I4
		//IL_0344: Expected O, but got I4
		//IL_034d: Expected O, but got I4
		//IL_0229: Invalid comparison between I4 and F4
		//IL_01f1: Invalid comparison between O and F4
		//IL_00f5: Invalid comparison between I4 and F4
		//IL_0096: Expected F4, but got I4
		//IL_0249: Expected F4, but got I4
		//IL_053a: Invalid comparison between I4 and F4
		//IL_00b5: Invalid comparison between I4 and F4
		//IL_0257: Expected O, but got I4
		//IL_0692: Expected O, but got Ref
		//IL_0115: Expected F4, but got I4
		//IL_04d4: Expected O, but got F4
		//IL_04e1: Expected O, but got F4
		//IL_0506: Invalid comparison between F4 and I4
		//IL_0123: Expected F4, but got I4
		//IL_0661: Expected O, but got Ref
		//IL_00d5: Expected F4, but got I4
		//IL_0618: Expected I4, but got O
		//IL_02bb: Invalid comparison between I4 and F4
		//IL_0164: Expected O, but got F4
		//IL_0171: Expected O, but got F4
		//IL_0196: Invalid comparison between F4 and I4
		//IL_0399: Expected O, but got I
		//IL_03bc: Expected O, but got I
		//IL_0437: Unknown result type (might be due to invalid IL or missing references)
		//IL_043c: Expected O, but got Unknown
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_044a: Expected O, but got Unknown
		//IL_0409: Expected O, but got I
		//IL_0416: Expected O, but got I4
		bool flag = this.disc != null;
		bool flag2 = !flag;
		UnityEngine.Object obj = null;
		UnityEngine.Object obj2 = this.disc;
		object obj3 = default(object);
		object obj4 = default(object);
		float num5 = default(float);
		if (!flag2)
		{
			Disc disc = this.disc;
			float num = disc.radius;
			if (0f > disc.radius)
			{
				num = 0f;
			}
			float num2 = (float)obj3 - (float)obj4;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180371560");
			bool flag3;
			bool flag4;
			Vector2 vector = default(Vector2);
			float num8;
			if (!ringOnly)
			{
				float num3 = num + ringHitPaddingLocalUnits;
				if (0f > num3)
				{
					num3 = 0f;
				}
				float num4 = num3 - num5;
				object obj5 = num3 ^ num5;
				object obj6 = num3 ^ num4;
				object obj7 = obj5 & obj6;
				flag3 = (nint)obj7 < 0;
				flag4 = num4 < 0f;
			}
			else
			{
				float num6 = ringHitWidthLocalUnits * 0.5f;
				if (0f > num6)
				{
					num6 = 0f;
				}
				float num7 = num - num6;
				num2 = num7 - ringHitPaddingLocalUnits;
				if (0f > num2)
				{
					num2 = 0f;
				}
				bool flag5 = num5 < num2;
				num8 = num5;
				obj = null;
				obj2 = (UnityEngine.Object)(&vector);
				if (flag5)
				{
					goto IL_01a1;
				}
				float num9 = num6 + num;
				float num10 = num9 + ringHitPaddingLocalUnits;
				float num11 = num10 - num5;
				object obj8 = num10 ^ num5;
				object obj9 = num10 ^ num11;
				object obj10 = obj8 & obj9;
				flag3 = (nint)obj10 < 0;
				flag4 = num11 < 0f;
			}
			bool flag6 = flag4 == flag3;
			num8 = num5;
			obj = null;
			obj2 = (UnityEngine.Object)(&vector);
			if (flag6)
			{
				goto IL_045c;
			}
		}
		goto IL_01a1;
		IL_0551:
		Vector2 screenPoint;
		if (extraRectHitAreas != null)
		{
			RectTransform[] array = extraRectHitAreas;
			object obj11 = 32;
			object obj12 = 0;
			for (object obj13 = 0; (nint)obj13 < array.Length; array = extraRectHitAreas, obj12++, obj11 += 8, obj13 = obj12)
			{
				RectTransform[] array2 = extraRectHitAreas;
				if ((nint)obj12 < array2.Length)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v203 @ r14_v8+v200 @ rbx_v9 (UnityEngine.RectTransform[])]");
					if (!((UnityEngine.Object)0 != null))
					{
						continue;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v203 @ r14_v8+v200 @ rbx_v9 (UnityEngine.RectTransform[])]");
					GameObject gameObject = ((Component)0).gameObject;
					if (!gameObject.activeInHierarchy)
					{
						continue;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v203 @ r14_v8+v200 @ rbx_v9 (UnityEngine.RectTransform[])]");
					bool flag7 = RectTransformUtility.RectangleContainsScreenPoint((RectTransform)0, screenPoint, canvasCamera);
					Vector2 vector2 = (Vector2)0;
					if (!flag7)
					{
						continue;
					}
					goto IL_045c;
				}
				IndexOutOfRangeException ex = new IndexOutOfRangeException();
				return (byte)(int)ex != 0;
			}
		}
		return false;
		IL_01a1:
		bool flag8 = !enableLineBandHitTest;
		screenPoint = pointerScreen;
		if (!flag8)
		{
			object obj14 = default(object);
			if (requireMinimumDistanceForLineHit)
			{
				bool flag9 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj14) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)minimumDistanceForLineHit);
				screenPoint = pointerScreen;
				if (flag9)
				{
					goto IL_0551;
				}
			}
			float num12 = lineBandPaddingLocalUnits + lineBandHalfWidthLocalUnits;
			if (0f > num12)
			{
				num12 = 0f;
			}
			object obj15 = default(object);
			float num13 = (float)obj15 * ((float)Math.PI / 180f);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E400");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033DE70");
			bool flag10 = 0 <= (nint)obj14;
			object obj16 = obj14;
			if (!flag10)
			{
				obj16 = 0;
			}
			float num14 = num13 * (float)obj16;
			float num15 = num13 * (float)obj16;
			object obj17 = default(object);
			float num16 = num14 + (float)obj17;
			float num17 = num15 + (float)obj4;
			float num18 = num16 - (float)obj17;
			float num19 = num17 - (float)obj4;
			float num20 = num18 * num18;
			float num21 = num19 * num19;
			float num22 = num21 + num20;
			float num23 = Mathf.Epsilon;
			if (Mathf.Epsilon < num22)
			{
				object obj18 = obj3 - obj4;
				Vector2 vector2 = default(Vector2);
				object obj19 = (object)vector2 - obj17;
				float num24 = (float)obj18 * num19;
				float num25 = (float)obj19 * num18;
				float num26 = num24 + num25;
				float num27 = num26 / num22;
				if (!(0f > num27))
				{
					bool flag11 = !(num27 > 1f);
					num23 = 1f;
					if (!flag11)
					{
						num23 = 1f;
					}
				}
				float num28 = num5;
			}
			else
			{
				num23 = num5;
				float num28 = num5;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180407E10");
			if (!(num12 < num23))
			{
				goto IL_045c;
			}
			screenPoint = pointerScreen;
		}
		goto IL_0551;
		IL_045c:
		return true;
	}

	public void SetHovered(bool hovered)
	{
		if (_003CIsHovered_003Ek__BackingField != hovered)
		{
			_003CIsHovered_003Ek__BackingField = hovered;
			(hovered ? onHoverEnter : onHoverExit)?.Invoke();
		}
	}

	private static float DistancePointToSegment(Vector2 p, Vector2 a, Vector2 b)
	{
		//IL_0062: Invalid comparison between I4 and F4
		//IL_00bf: Expected F4, but got I4
		//IL_00c8: Expected F4, but got I4
		object obj = default(object);
		object obj2 = default(object);
		float num = (float)obj - (float)obj2;
		Vector2 vector2 = default(Vector2);
		Vector2 vector = b - vector2;
		float num2 = num * num;
		object obj3 = vector * vector;
		float num3 = num2 + (float)obj3;
		float result = Mathf.Epsilon;
		if (Mathf.Epsilon < num3)
		{
			object obj4 = p - vector2;
			object obj6 = default(object);
			object obj5 = obj6 - obj2;
			object obj7 = obj4 * (object)vector;
			float num4 = (float)obj5 * num;
			float num5 = num4 + (float)obj7;
			float num6 = num5 / num3;
			if (!(0f > num6))
			{
				bool flag = !(num6 > 1f);
				result = 1f;
				if (!flag)
				{
					num6 = 1f;
					result = 1f;
				}
			}
			else
			{
				num6 = 0f;
				result = 0f;
			}
			float num7 = num * num6;
			num = num7 + (float)obj2;
			Vector2 vector3 = default(Vector2);
			vector = vector3;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180407E10");
		return result;
	}
}
