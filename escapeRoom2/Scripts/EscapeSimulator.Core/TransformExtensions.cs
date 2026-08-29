using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class TransformExtensions
{
	public static string GetScenePath(this Transform transform, string separator = "/")
	{
		StringBuilder stringBuilder = new StringBuilder(transform.name);
		while (transform.parent != null)
		{
			transform = transform.parent;
			stringBuilder.Insert(0, transform.name + separator);
		}
		return stringBuilder.ToString();
	}

	public static int GetDepth(this Transform transform)
	{
		int num = 0;
		while (transform.parent != null)
		{
			transform = transform.parent;
			num++;
		}
		return num;
	}

	public static void SetGlobalScale(this Transform transform, Vector3 globalScale)
	{
		if (transform == null)
		{
			Debug.LogError("Cannot set global scale of a null transform.");
			return;
		}
		Transform parent = transform.parent;
		if (parent == null)
		{
			transform.localScale = globalScale;
			return;
		}
		Vector3 lossyScale = parent.lossyScale;
		transform.localScale = new Vector3(globalScale.x / lossyScale.x, globalScale.y / lossyScale.y, globalScale.z / lossyScale.z);
	}

	public static void ReverseChildren(this Transform transform)
	{
		int childCount = transform.childCount;
		List<Transform> list = new List<Transform>(childCount);
		for (int i = 0; i < childCount; i++)
		{
			list.Add(transform.GetChild(i));
		}
		for (int num = childCount - 1; num >= 0; num--)
		{
			list[num].SetSiblingIndex(childCount - 1 - num);
		}
	}

	public static void SetWidth(this RectTransform rectTransform, float width)
	{
		rectTransform.sizeDelta = new Vector2(width, rectTransform.sizeDelta.y);
	}

	public static void SetHeight(this RectTransform rectTransform, float height)
	{
		rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, height);
	}

	public static void SetLeft(this RectTransform rectTransform, float left)
	{
		rectTransform.offsetMin = new Vector2(left, rectTransform.offsetMin.y);
	}

	public static void SetRight(this RectTransform rectTransform, float right)
	{
		rectTransform.offsetMax = new Vector2(0f - right, rectTransform.offsetMax.y);
	}

	public static void SetTop(this RectTransform rectTransform, float top)
	{
		rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, 0f - top);
	}

	public static void SetBottom(this RectTransform rectTransform, float bottom)
	{
		rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, bottom);
	}

	public static void SetStretchValues(this RectTransform rt)
	{
		rt.anchorMin = new Vector2(0f, 0f);
		rt.anchorMax = new Vector2(1f, 1f);
		rt.pivot = new Vector2(0.5f, 0.5f);
		rt.offsetMin = Vector2.zero;
		rt.offsetMax = Vector2.zero;
		Vector3 localPosition = rt.localPosition;
		rt.localPosition = new Vector3(localPosition.x, localPosition.y, 0f);
	}
}
