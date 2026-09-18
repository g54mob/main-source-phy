using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemsInPotionPuzzleController : MonoBehaviour
{
	[SerializeField]
	private float rotationSpeed = 5f;

	private bool startMoving;

	public void SetController(PrefabTypes itemPrefabType, int orderInSet)
	{
		base.transform.localRotation = Quaternion.identity;
		GameObject gameObject = Object.Instantiate(MasterPool.GetGameObjectReference(itemPrefabType), base.transform);
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localScale = Vector3.one;
		gameObject.transform.localRotation = Quaternion.identity;
		List<Transform> list = gameObject.GetComponentsInChildren<Transform>().ToList();
		list.Remove(gameObject.transform);
		foreach (Transform item in Randomizer.Randomize(list))
		{
			if (orderInSet == -1)
			{
				Object.Destroy(item.gameObject);
				continue;
			}
			item.parent = base.transform;
			orderInSet--;
		}
		startMoving = true;
	}

	public void SetControllerLayer(int layer)
	{
		base.gameObject.layer = layer;
		foreach (Transform item in base.gameObject.transform)
		{
			item.gameObject.layer = layer;
		}
	}

	private void Update()
	{
		if (startMoving)
		{
			base.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
		}
	}
}
