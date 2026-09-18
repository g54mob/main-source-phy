using System;
using UnityEngine;

[Serializable]
public class ItemSaveData
{
	public PrefabTypes itemPrefabType;

	[SerializeField]
	public bool isInInventory;

	[SerializeField]
	public Vector3 position;

	[SerializeField]
	public Vector3 eulerAngles;

	public ItemSaveData(ItemController itemController)
	{
		itemPrefabType = itemController.prefabType;
		position = itemController.transform.position;
		eulerAngles = itemController.transform.eulerAngles;
		isInInventory = SceneSingleton<CharacterInventory>.Instance.itemControllersList.Contains(itemController);
	}
}
