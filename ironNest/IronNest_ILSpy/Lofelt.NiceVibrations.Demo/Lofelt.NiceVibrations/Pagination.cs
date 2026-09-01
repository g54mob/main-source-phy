using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.UI;

namespace Lofelt.NiceVibrations;

public class Pagination : MonoBehaviour
{
	public GameObject PaginationDotPrefab;

	public Color ActiveColor;

	public Color InactiveColor;

	protected List<Image> _images;

	public unsafe virtual void InitializePagination(int numberOfPages)
	{
		//IL_0132: Expected O, but got Ref
		//IL_01e8: Expected I, but got O
		//IL_00c9: Expected O, but got I4
		//IL_0151: Expected O, but got Ref
		//IL_0175: Expected O, but got Ref
		List<Image> images = new List<Image>();
		_images = images;
		if (numberOfPages > 0)
		{
			int num = default(int);
			Image item = default(Image);
			object obj;
			do
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(PaginationDotPrefab);
				Transform transform = gameObject.transform;
				Transform parentInternal = base.transform;
				transform.parentInternal = parentInternal;
				string text = num.ToString();
				string text2 = "PaginationDot" + text;
				gameObject.name = text2;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
				_images.Add(item);
				obj = num + 1;
			}
			while ((nint)obj < numberOfPages);
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<Image>.Enumerator enumerator = default(List<Image>.Enumerator);
		Graphic graphic = default(Graphic);
		Color color = default(Color);
		object obj2 = default(object);
		object obj3 = default(object);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				bool flag = (object)graphic == null;
				nint num2 = (nint)(&enumerator);
				if (!flag)
				{
					graphic.color = (Color)(&color);
					RectTransform rectTransform = graphic.rectTransform;
					bool flag2 = (object)rectTransform == null;
					num2 = (nint)typeof(Vector3);
					if (flag2)
					{
						break;
					}
					rectTransform.localScale = (Vector3)(&obj2);
					RectTransform rectTransform2 = graphic.rectTransform;
					rectTransform2.localPosition = (Vector3)(&obj3);
					graphic.SetNativeSize();
					continue;
				}
				throw new NullReferenceException();
			}
			enumerator.Dispose();
			return;
		}
		throw new NullReferenceException();
	}

	public virtual void SetCurrentPage(int numberOfPages, int currentPage)
	{
		//IL_0079: Expected O, but got I4
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Expected O, but got Unknown
		bool flag = numberOfPages <= 0;
		object obj = 0;
		if (flag)
		{
			return;
		}
		object obj3 = default(object);
		object obj4 = default(object);
		do
		{
			object obj2;
			if ((nint)obj != currentPage)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				Color inactiveColor = InactiveColor;
				nint num = 0;
				obj2 = obj3;
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				Color inactiveColor = ActiveColor;
				nint num = 0;
				obj2 = obj4;
			}
			object obj5 = obj2;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v239 @ r8_v3+2A8] (should have been resolved before IL gen)");
			obj++;
		}
		while ((nint)obj < numberOfPages);
	}
}
