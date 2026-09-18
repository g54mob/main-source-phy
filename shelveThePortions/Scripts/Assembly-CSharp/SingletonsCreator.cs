using System.Collections.Generic;
using UnityEngine;

public class SingletonsCreator : MonoBehaviour
{
	[SerializeField]
	private List<GameObject> prefabsList;

	private void CreateMissingSingletons()
	{
		foreach (GameObject prefabs in prefabsList)
		{
			if (!(prefabs == null))
			{
				MonoBehaviour component = prefabs.GetComponent<MonoBehaviour>();
				if (component == null)
				{
					Object.Instantiate(prefabs);
				}
				else if (!(Object.FindAnyObjectByType(component.GetType()) != null))
				{
					Object.Instantiate(prefabs);
				}
			}
		}
	}
}
