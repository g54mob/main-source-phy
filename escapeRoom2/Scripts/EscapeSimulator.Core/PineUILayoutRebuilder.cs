using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PineUILayoutRebuilder
{
	public List<RectTransform> rectTransformsToLayout = new List<RectTransform>(4);

	public void requestRebuild(Component mb)
	{
		if (mb != null)
		{
			requestRebuild(mb.gameObject.GetComponent<RectTransform>());
		}
	}

	public void requestRebuild(GameObject go)
	{
		if (go != null)
		{
			requestRebuild(go.GetComponent<RectTransform>());
		}
	}

	public void requestRebuild(RectTransform rt)
	{
		if (!(rt == null))
		{
			rectTransformsToLayout.Add(rt);
		}
	}

	public void lateUpdate()
	{
		if (rectTransformsToLayout.Count == 0)
		{
			return;
		}
		Canvas.ForceUpdateCanvases();
		for (int i = 0; i < 2; i++)
		{
			foreach (RectTransform item in rectTransformsToLayout)
			{
				if (item != null)
				{
					LayoutRebuilder.MarkLayoutForRebuild(item);
					LayoutRebuilder.ForceRebuildLayoutImmediate(item);
				}
			}
			Canvas.ForceUpdateCanvases();
		}
		rectTransformsToLayout.Clear();
	}
}
