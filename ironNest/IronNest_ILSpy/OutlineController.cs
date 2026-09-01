using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

public class OutlineController : MonoBehaviour
{
	private List<Outline> outlines;

	public bool CurrentState;

	public void ChangeOutlinesState(bool value)
	{
		CurrentState = value;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<Outline>.Enumerator enumerator = default(List<Outline>.Enumerator);
		UnityEngine.Object obj = default(UnityEngine.Object);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (obj != null)
				{
					if ((object)obj == null)
					{
						break;
					}
					((Behaviour)obj).enabled = value;
				}
				continue;
			}
			enumerator.Dispose();
			return;
		}
		throw new NullReferenceException();
	}

	public void AddAllOutlines()
	{
		Outline[] collection = UnityEngine.Object.FindObjectsByType<Outline>(FindObjectsInactive.Include, FindObjectsSortMode.None);
		List<Outline> list = new List<Outline>(collection);
		outlines = list;
	}

	public void AddOutline(Outline outline)
	{
		if (!outlines.Contains(outline))
		{
			outlines.Add(outline);
		}
		outline.enabled = CurrentState;
	}

	public void RemoveOutline(Outline outline)
	{
		bool flag = outlines.Remove(outline);
	}
}
