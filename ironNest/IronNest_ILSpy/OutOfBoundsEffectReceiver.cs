using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using TMPro;
using UnityEngine;

public class OutOfBoundsEffectReceiver : MonoBehaviour
{
	public Transform rotationTarget;

	public List<Transform> rotationTargets;

	public TMP_Text remainingDistanceText;

	public List<TMP_Text> remainingDistanceTexts;

	public string distanceFormat;

	public string unitsSuffix;

	public List<GameObject> topGroup;

	public List<GameObject> bottomGroup;

	public List<GameObject> leftGroup;

	public List<GameObject> rightGroup;

	public bool destroyUnusedGroups;

	private bool initialized;

	public unsafe void Initialize(float shellAngleDeg, float remainingDistance, MapBorderSide borderSide)
	{
		//IL_005a: Expected I, but got O
		//IL_00e5: Expected I, but got O
		//IL_0194: Expected I, but got O
		//IL_01bc: Expected O, but got Ref
		//IL_01c1: Expected I, but got O
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Expected O, but got Unknown
		//IL_031a: Expected O, but got I4
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Expected O, but got Unknown
		//IL_02db: Expected I, but got O
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		//IL_044f: Expected O, but got Unknown
		initialized = true;
		List<Transform> list = rotationTargets;
		nint num = default(nint);
		if (list._size == 0)
		{
			bool flag = rotationTarget != null;
			bool flag2 = !flag;
			num = unchecked((nint)null);
			if (!flag2)
			{
				rotationTargets.Add(rotationTarget);
				num = 0;
			}
		}
		List<TMP_Text> list2 = remainingDistanceTexts;
		bool flag3 = list2._size != 0;
		nint num2 = num;
		if (!flag3)
		{
			bool flag4 = remainingDistanceText != null;
			bool flag5 = !flag4;
			num2 = unchecked((nint)null);
			if (!flag5)
			{
				remainingDistanceTexts.Add(remainingDistanceText);
				num2 = 0;
			}
		}
		List<Transform> list3 = rotationTargets;
		bool flag6 = list3._size != 0;
		nint num3 = num2;
		if (!flag6)
		{
			Transform item = base.transform;
			list3.Add(item);
			num3 = 0;
		}
		List<Transform> list4 = rotationTargets;
		nint num4 = (nint)borderSide;
		List<GameObject> list5 = null;
		List<GameObject> list6 = null;
		UnityEngine.Object obj = default(UnityEngine.Object);
		float num5 = default(float);
		while ((nint)list6 < list4._size)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			bool flag7 = obj != null;
			num3 = unchecked((nint)null);
			if (flag7)
			{
				Vector3 localEulerAngles = ((Transform)obj).localEulerAngles;
				((Transform)obj).localEulerAngles = (Vector3)(&num5);
				num3 = unchecked((nint)null);
			}
			list4 = rotationTargets;
			list5 = (List<GameObject>)(list5 + 1);
			num4 = 0;
			list6 = list5;
		}
		List<TMP_Text> list7 = remainingDistanceTexts;
		if (list7._size != 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg = default(object);
			string text = string.Format(distanceFormat, arg);
			bool flag8 = string.IsNullOrEmpty(unitsSuffix);
			string text2 = text;
			if (!flag8)
			{
				string text3 = text + unitsSuffix;
				text2 = text3;
			}
			List<TMP_Text> list8 = remainingDistanceTexts;
			List<GameObject> list9 = null;
			List<GameObject> list10 = null;
			UnityEngine.Object obj2 = default(UnityEngine.Object);
			while ((nint)list10 < list8._size)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				if (obj2 != null)
				{
					nint num6 = (nint)obj2;
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v776 @ r8_v17 (Il2CppClass<UnityEngine.Object>)+558] (should have been resolved before IL gen)");
				}
				list8 = remainingDistanceTexts;
				list9 = (List<GameObject>)(list9 + 1);
				bool flag9 = remainingDistanceTexts != null;
				list10 = list9;
				if (flag9)
				{
					continue;
				}
				goto IL_03c3;
			}
		}
		bool flag10 = borderSide == MapBorderSide.Top;
		List<GameObject> list11;
		if (!flag10)
		{
			object obj3 = borderSide - 1;
			if (!flag10)
			{
				object obj4 = obj3 - 1;
				if (!flag10)
				{
					bool flag11 = (nint)obj4 != 1;
					list11 = null;
					if (!flag11)
					{
						list11 = rightGroup;
					}
				}
				else
				{
					list11 = leftGroup;
				}
			}
			else
			{
				list11 = bottomGroup;
			}
		}
		else
		{
			list11 = topGroup;
		}
		object obj5 = (object)list11 - (object)topGroup;
		bool activate = obj5 == null;
		_003CActivateGroupForBorder_003Eg__ProcessGroup_007C16_0(topGroup, activate);
		object obj6 = (object)list11 - (object)bottomGroup;
		bool activate2 = obj6 == null;
		_003CActivateGroupForBorder_003Eg__ProcessGroup_007C16_0(bottomGroup, activate2);
		object obj7 = (object)list11 - (object)leftGroup;
		bool activate3 = obj7 == null;
		_003CActivateGroupForBorder_003Eg__ProcessGroup_007C16_0(leftGroup, activate3);
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 622 Invalid \"Jump target not found in method: 0x1805601B0\"");
		goto IL_03c3;
		IL_03c3:
		throw new NullReferenceException();
	}

	private void EnsureLegacyFallbacks()
	{
		List<Transform> list = rotationTargets;
		if (list._size == 0 && rotationTarget != null)
		{
			rotationTargets.Add(rotationTarget);
		}
		List<TMP_Text> list2 = remainingDistanceTexts;
		if (list2._size == 0 && remainingDistanceText != null)
		{
			remainingDistanceTexts.Add(remainingDistanceText);
		}
		List<Transform> list3 = rotationTargets;
		if (list3._size == 0)
		{
			Transform item = base.transform;
			list3.Add(item);
		}
	}

	private unsafe void ApplyRotation(float angleDeg)
	{
		//IL_008e: Expected O, but got I4
		//IL_0097: Expected O, but got I4
		//IL_004b: Expected O, but got Ref
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Expected O, but got Unknown
		List<Transform> list = rotationTargets;
		object obj = 0;
		object obj2 = 0;
		UnityEngine.Object obj3 = default(UnityEngine.Object);
		float num = default(float);
		while ((nint)obj2 < list._size)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			if (obj3 != null)
			{
				Vector3 localEulerAngles = ((Transform)obj3).localEulerAngles;
				((Transform)obj3).localEulerAngles = (Vector3)(&num);
			}
			list = rotationTargets;
			obj++;
			obj2 = obj;
		}
	}

	private void ApplyRemainingDistance(float remainingDistance)
	{
		//IL_0100: Expected O, but got I4
		//IL_0109: Expected O, but got I4
		//IL_00c1: Expected I, but got O
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Expected O, but got Unknown
		List<TMP_Text> list = remainingDistanceTexts;
		if (list._size == 0)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		object arg = default(object);
		string text = string.Format(distanceFormat, arg);
		bool flag = string.IsNullOrEmpty(unitsSuffix);
		string text2 = text;
		if (!flag)
		{
			string text3 = text + unitsSuffix;
			text2 = text3;
		}
		List<TMP_Text> list2 = remainingDistanceTexts;
		object obj = 0;
		object obj2 = 0;
		UnityEngine.Object obj3 = default(UnityEngine.Object);
		while ((nint)obj2 < list2._size)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			if (obj3 != null)
			{
				nint num = (nint)obj3;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v298 @ r8_v9 (Il2CppClass<UnityEngine.Object>)+558] (should have been resolved before IL gen)");
			}
			list2 = remainingDistanceTexts;
			obj++;
			obj2 = obj;
		}
	}

	private void ActivateGroupForBorder(MapBorderSide side)
	{
		//IL_002b: Expected O, but got I4
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		bool flag = side == MapBorderSide.Top;
		List<GameObject> list;
		if (!flag)
		{
			object obj = side - 1;
			if (!flag)
			{
				object obj2 = obj - 1;
				if (!flag)
				{
					bool flag2 = (nint)obj2 != 1;
					list = null;
					if (!flag2)
					{
						list = rightGroup;
					}
				}
				else
				{
					list = leftGroup;
				}
			}
			else
			{
				list = bottomGroup;
			}
		}
		else
		{
			list = topGroup;
		}
		object obj3 = (object)list - (object)topGroup;
		bool activate = obj3 == null;
		_003CActivateGroupForBorder_003Eg__ProcessGroup_007C16_0(topGroup, activate);
		object obj4 = (object)list - (object)bottomGroup;
		bool activate2 = obj4 == null;
		_003CActivateGroupForBorder_003Eg__ProcessGroup_007C16_0(bottomGroup, activate2);
		object obj5 = (object)list - (object)leftGroup;
		bool activate3 = obj5 == null;
		_003CActivateGroupForBorder_003Eg__ProcessGroup_007C16_0(leftGroup, activate3);
		object obj6 = (object)list - (object)rightGroup;
		bool activate4 = obj6 == null;
		_003CActivateGroupForBorder_003Eg__ProcessGroup_007C16_0(rightGroup, activate4);
	}

	public OutOfBoundsEffectReceiver()
	{
		List<Transform> list = new List<Transform>();
		rotationTargets = list;
		remainingDistanceTexts = new List<TMP_Text>();
		distanceFormat = "{0:0.0}";
		unitsSuffix = "u";
		topGroup = new List<GameObject>();
		bottomGroup = new List<GameObject>();
		leftGroup = new List<GameObject>();
		rightGroup = new List<GameObject>();
		destroyUnusedGroups = true;
		base._002Ector();
	}

	private void _003CActivateGroupForBorder_003Eg__ProcessGroup_007C16_0(List<GameObject> group, bool activate)
	{
		if (group == null)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<GameObject>.Enumerator enumerator = default(List<GameObject>.Enumerator);
		UnityEngine.Object obj = default(UnityEngine.Object);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (!(obj != null))
				{
					continue;
				}
				if (!activate)
				{
					if (destroyUnusedGroups == activate)
					{
						((GameObject)obj).SetActive(false);
					}
					else
					{
						UnityEngine.Object.Destroy(obj);
					}
					continue;
				}
				if ((object)obj == null)
				{
					break;
				}
				((GameObject)obj).SetActive(true);
				continue;
			}
			enumerator.Dispose();
			return;
		}
		throw new NullReferenceException();
	}
}
