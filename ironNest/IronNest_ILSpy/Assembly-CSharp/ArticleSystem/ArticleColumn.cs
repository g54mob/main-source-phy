using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.UI;

namespace ArticleSystem;

public class ArticleColumn : MonoBehaviour
{
	private VerticalLayoutGroup layoutGroup;

	public float fillTolerance = 0.85f;

	private float _003CCapacityHeight_003Ek__BackingField;

	private float _003CUsedHeight_003Ek__BackingField;

	private float _003CArticleSpacing_003Ek__BackingField;

	private RectTransform _rect;

	private readonly List<RectTransform> _placedRects;

	public float CapacityHeight
	{
		get
		{
			return _003CCapacityHeight_003Ek__BackingField;
		}
		private set
		{
			_003CCapacityHeight_003Ek__BackingField = value;
		}
	}

	public float UsedHeight
	{
		get
		{
			return _003CUsedHeight_003Ek__BackingField;
		}
		private set
		{
			_003CUsedHeight_003Ek__BackingField = value;
		}
	}

	public float ArticleSpacing
	{
		get
		{
			return _003CArticleSpacing_003Ek__BackingField;
		}
		private set
		{
			_003CArticleSpacing_003Ek__BackingField = value;
		}
	}

	private void Awake()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		RectTransform rect = default(RectTransform);
		_rect = rect;
	}

	public void BeginPopulation()
	{
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Expected O, but got Unknown
		//IL_012d: Invalid comparison between I4 and F4
		//IL_013f: Expected F4, but got I4
		//IL_0250: Expected I, but got O
		//IL_02c2: Expected I, but got O
		//IL_02d2: Expected O, but got I
		//IL_0351: Expected I, but got O
		//IL_0361: Expected O, but got I
		//IL_03e0: Expected I, but got O
		//IL_03f0: Expected O, but got I
		if (_rect == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			RectTransform rect = default(RectTransform);
			_rect = rect;
		}
		if (layoutGroup != null)
		{
			LayoutRebuilder.ForceRebuildLayoutImmediate(_rect);
			VerticalLayoutGroup verticalLayoutGroup = layoutGroup;
			int top = ((LayoutGroup)verticalLayoutGroup).m_Padding.top;
			VerticalLayoutGroup verticalLayoutGroup2 = layoutGroup;
			int bottom = ((LayoutGroup)verticalLayoutGroup2).m_Padding.bottom;
			VerticalLayoutGroup verticalLayoutGroup3 = layoutGroup;
			_003CArticleSpacing_003Ek__BackingField = ((HorizontalOrVerticalLayoutGroup)verticalLayoutGroup3).m_Spacing;
			Rect rect2 = _rect.rect;
			List<RectTransform> placedRects = _placedRects;
			_003CUsedHeight_003Ek__BackingField = 0f;
			object obj2 = default(object);
			object obj = obj2 - top;
			float num = (float)obj - (float)bottom;
			bool flag = !(0f < num);
			float num2 = 0f;
			if (!flag)
			{
				num2 = num;
			}
			_003CCapacityHeight_003Ek__BackingField = num2;
			int version = placedRects._version + 1;
			placedRects._version = version;
			if (!RuntimeHelpers.IsReferenceOrContainsReferences<RectTransform>())
			{
				placedRects._size = 0;
				int num3 = 0;
			}
			else
			{
				int num3 = placedRects._size;
				placedRects._size = 0;
				if (placedRects._size > 0)
				{
					Array.Clear(placedRects._items, 0, placedRects._size);
				}
			}
			object[] array = new object[4];
			string text = base.name;
			if (text != null)
			{
				nint num4 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj3 = default(object);
				if (obj3 == null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					string text2 = default(string);
					throw text2;
				}
			}
			array[0] = text;
			string text3 = layoutGroup.name;
			if (text3 != null)
			{
				nint num5 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v659 @ rdx_v39 (Il2CppClass<System.Object[]>)+40]");
				object obj4 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj5 = default(object);
				bool flag2 = obj5 == null;
				string text4 = text3;
				if (flag2)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj6 = default(object);
					throw obj6;
				}
			}
			array[1] = text3;
			num2 = _003CCapacityHeight_003Ek__BackingField;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object obj7 = default(object);
			if (obj7 != null)
			{
				nint num6 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v724 @ rdx_v37 (Il2CppClass<System.Object[]>)+40]");
				object obj8 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj9 = default(object);
				bool flag3 = obj9 == null;
				object obj10 = obj7;
				if (flag3)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj11 = default(object);
					throw obj11;
				}
			}
			array[2] = obj7;
			num2 = _003CArticleSpacing_003Ek__BackingField;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object obj12 = default(object);
			if (obj12 != null)
			{
				nint num7 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v769 @ rdx_v35 (Il2CppClass<System.Object[]>)+40]");
				object obj13 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj14 = default(object);
				bool flag4 = obj14 == null;
				object obj15 = obj12;
				if (flag4)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj16 = default(object);
					throw obj16;
				}
			}
			array[3] = obj12;
			string message = string.Format("[ArticleColumn] '{0}' BeginPopulation — VLG: {1}, capacity: {2}px, spacing: {3}px", array);
			Debug.Log(message, this);
		}
		else
		{
			string text5 = base.name;
			string message2 = "[ArticleColumn] '" + text5 + "' has no VerticalLayoutGroup assigned. Please assign it in the Inspector.";
			Debug.LogError(message2, this);
		}
	}

	public unsafe void PlaceArticle(GameObject prefab, float measuredHeight)
	{
		//IL_006d: Expected F4, but got I4
		//IL_0150: Expected O, but got Ref
		//IL_01e4: Expected O, but got Ref
		if (!(prefab != null))
		{
			return;
		}
		List<RectTransform> placedRects = _placedRects;
		float num = ((placedRects._size <= 0) ? 0f : _003CArticleSpacing_003Ek__BackingField);
		Transform parent = layoutGroup.transform;
		GameObject gameObject = UnityEngine.Object.Instantiate(prefab, parent);
		string text = prefab.name;
		string text2 = text + " (Article)";
		gameObject.name = text2;
		Transform transform = gameObject.transform;
		bool flag = (object)transform == null;
		UnityEngine.Object obj = null;
		if (!flag)
		{
			bool flag2 = (object)transform.GetType() != typeof(RectTransform);
			obj = null;
			if (!flag2)
			{
				obj = transform;
			}
		}
		if (obj != null)
		{
			Vector3 vector = default(Vector3);
			((Transform)obj).localScale = (Vector3)(&vector);
			((Transform)obj).localRotation = (Quaternion)(&vector);
			Vector2 vector2 = default(Vector2);
			((RectTransform)obj).anchorMin = vector2;
			((RectTransform)obj).anchorMax = vector2;
			((RectTransform)obj).pivot = vector2;
			((RectTransform)obj).anchoredPosition = vector2;
			((RectTransform)obj).offsetMin = vector2;
			((RectTransform)obj).offsetMax = vector2;
			_placedRects.Add((RectTransform)obj);
		}
		float num2 = num + measuredHeight;
		float num3 = num2 + _003CUsedHeight_003Ek__BackingField;
		_003CUsedHeight_003Ek__BackingField = num3;
	}

	public void FlushLayout()
	{
		if (!(layoutGroup != null))
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		UnityEngine.Object obj = default(UnityEngine.Object);
		if (!(obj != null))
		{
			return;
		}
		string arg = base.name;
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		object arg2 = default(object);
		string message = $"[ArticleColumn] '{arg}' FlushLayout — placed: {arg2}";
		Debug.Log(message, this);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<RectTransform>.Enumerator enumerator = default(List<RectTransform>.Enumerator);
		UnityEngine.Object obj2 = default(UnityEngine.Object);
		while (enumerator.MoveNext())
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
			if (obj2 != null)
			{
				RebuildBottomUp((RectTransform)obj2);
			}
		}
		enumerator.Dispose();
		LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)obj);
	}

	public void Clear()
	{
		//IL_00f6: Expected I, but got O
		//IL_00b1: Expected I, but got O
		//IL_0139: Expected O, but got I4
		if (layoutGroup != null)
		{
			Transform transform = layoutGroup.transform;
			bool flag = (nint)transform < 0;
			int childCount = transform.childCount;
			int num = childCount - 1;
			if (!flag)
			{
				object obj3;
				do
				{
					Transform child = transform.GetChild(num);
					bool flag2;
					if (!Application.isPlaying)
					{
						GameObject obj = child.gameObject;
						nint num2 = (nint)typeof(UnityEngine.Object);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v297 @ rcx_v21 (Il2CppClass<UnityEngine.Object>)+E4]");
						flag2 = (nint)0 < (nint)0;
						UnityEngine.Object.DestroyImmediate(obj);
					}
					else
					{
						GameObject obj2 = child.gameObject;
						nint num3 = (nint)typeof(UnityEngine.Object);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v310 @ rcx_v19 (Il2CppClass<UnityEngine.Object>)+E4]");
						flag2 = (nint)0 < (nint)0;
						UnityEngine.Object.Destroy(obj2);
					}
					num--;
					obj3 = !flag2;
				}
				while (obj3 != null);
			}
		}
		List<RectTransform> placedRects = _placedRects;
		int version = placedRects._version + 1;
		placedRects._version = version;
		if (!RuntimeHelpers.IsReferenceOrContainsReferences<RectTransform>())
		{
			placedRects._size = 0;
		}
		else
		{
			placedRects._size = 0;
			if (placedRects._size > 0)
			{
				Array.Clear(placedRects._items, 0, placedRects._size);
			}
		}
		_003CUsedHeight_003Ek__BackingField = 0f;
	}

	private static void RebuildBottomUp(RectTransform root)
	{
		int num = 0;
		int num2 = 0;
		while (true)
		{
			int childCount = root.childCount;
			if (num2 >= childCount)
			{
				break;
			}
			Transform child = root.GetChild(num);
			bool flag = (object)child == null;
			UnityEngine.Object obj = null;
			if (!flag)
			{
				bool flag2 = (object)child.GetType() != typeof(RectTransform);
				obj = null;
				if (!flag2)
				{
					obj = child;
				}
			}
			if (obj != null)
			{
				RebuildBottomUp((RectTransform)obj);
			}
			num++;
			num2 = num;
		}
		LayoutRebuilder.ForceRebuildLayoutImmediate(root);
	}

	public ArticleColumn()
	{
		List<RectTransform> placedRects = new List<RectTransform>();
		_placedRects = placedRects;
		base._002Ector();
	}
}
