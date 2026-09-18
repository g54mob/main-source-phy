using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDelayActivator : MonoBehaviour
{
	[SerializeField]
	private List<ObjectDelayActivatorData> objectDelayActivatorDatasList;

	private void Start()
	{
		foreach (ObjectDelayActivatorData objectDelayActivatorDatas in objectDelayActivatorDatasList)
		{
			foreach (GameObject obj in objectDelayActivatorDatas.objList)
			{
				obj.SetActive(value: false);
			}
		}
		StartCoroutine(ActivateObjects());
	}

	private IEnumerator ActivateObjects()
	{
		for (int i = 0; i < objectDelayActivatorDatasList.Count; i++)
		{
			yield return new WaitForSeconds(objectDelayActivatorDatasList[i].waitDelay);
			foreach (GameObject obj in objectDelayActivatorDatasList[i].objList)
			{
				obj.SetActive(value: true);
			}
		}
	}
}
