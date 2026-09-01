using UnityEngine;

public class BlockViewerController : MonoBehaviour
{
	public Transform[] visObjects;

	public void Set(int index)
	{
		for (int i = 0; i < visObjects.Length; i++)
		{
			if (i == index)
			{
				visObjects[i].gameObject.SetActive(true);
			}
			else
			{
				visObjects[i].gameObject.SetActive(false);
			}
		}
	}
}
