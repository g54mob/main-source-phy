using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Controller/Outline controller")]
public class OutlineController : MonoBehaviour
{
	[SerializeField]
	private List<Outline> outlines;

	public bool CurrentState;

	public void ChangeOutlinesState(bool value)
	{
	}

	[ContextMenu("Add outlines from scene")]
	public void AddAllOutlines()
	{
	}

	public void AddOutline(Outline outline)
	{
	}

	public void RemoveOutline(Outline outline)
	{
	}
}
