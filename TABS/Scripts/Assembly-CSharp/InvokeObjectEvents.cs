using System.Collections.Generic;
using UnityEngine;

public class InvokeObjectEvents : MonoBehaviour
{
	private List<ObjectToActivate> objectsToActivate;

	private int activateIndex;

	private void Start()
	{
		objectsToActivate = new List<ObjectToActivate>();
		objectsToActivate.AddRange(base.transform.GetComponentsInChildren<ObjectToActivate>());
	}

	public void ActivateFirst(int numberToActivate)
	{
		for (int i = 0; i < numberToActivate; i++)
		{
			if (activateIndex + 1 > objectsToActivate.Count)
			{
				ResetIndex();
			}
			objectsToActivate[activateIndex].FirstActivateEvent();
			activateIndex++;
		}
	}

	public void ActivateSecond(int numberToActivate)
	{
		for (int i = 0; i < numberToActivate; i++)
		{
			if (activateIndex + 1 > objectsToActivate.Count)
			{
				ResetIndex();
			}
			objectsToActivate[activateIndex].SecondActivateEvent();
			activateIndex++;
		}
	}

	public void ResetIndex()
	{
		activateIndex = 0;
	}
}
