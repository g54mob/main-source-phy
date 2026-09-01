using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIAutoFocusSelector : MonoBehaviour
{
	private GameObject previousSelectedGameObject;

	private const int MaxNumberOfPreviousSelected = 4;

	private Queue<GameObject> allPreviousSelectedGameObjects = new Queue<GameObject>(4);

	private void Update()
	{
		if (EventSystem.current == null)
		{
			return;
		}
		GameObject currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;
		bool flag = false;
		if (currentSelectedGameObject != null && currentSelectedGameObject.activeInHierarchy && currentSelectedGameObject != previousSelectedGameObject)
		{
			previousSelectedGameObject = currentSelectedGameObject;
			if (!allPreviousSelectedGameObjects.Contains(currentSelectedGameObject))
			{
				if (allPreviousSelectedGameObjects.Count == 4)
				{
					allPreviousSelectedGameObjects.Dequeue();
				}
				allPreviousSelectedGameObjects.Enqueue(currentSelectedGameObject);
			}
		}
		else
		{
			flag = true;
			if (previousSelectedGameObject == null || !previousSelectedGameObject.activeInHierarchy)
			{
				foreach (GameObject allPreviousSelectedGameObject in allPreviousSelectedGameObjects)
				{
					if (!(allPreviousSelectedGameObject == null) && allPreviousSelectedGameObject.activeInHierarchy)
					{
						previousSelectedGameObject = allPreviousSelectedGameObject;
						break;
					}
				}
			}
		}
		if (flag && previousSelectedGameObject != null)
		{
			EventSystem.current.SetSelectedGameObject(previousSelectedGameObject);
		}
	}

	public void ClearQueue()
	{
		previousSelectedGameObject = null;
		allPreviousSelectedGameObjects.Clear();
	}
}
