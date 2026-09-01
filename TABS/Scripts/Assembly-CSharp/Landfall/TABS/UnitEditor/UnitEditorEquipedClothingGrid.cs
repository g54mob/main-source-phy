using System.Collections.Generic;
using UnityEngine;

namespace Landfall.TABS.UnitEditor
{
	public class UnitEditorEquipedClothingGrid : MonoBehaviour
	{
		private List<GameObject> spawnedButtons = new List<GameObject>();

		public GameObject EqupedClothingItem;

		public GameObject NewButton;

		public GameObject SpawnEquipedClothes(UnitEditorManager.EquipedClothingWrapper clothingWrapper)
		{
			UnitEditorManager manager = Object.FindObjectOfType<UnitEditorManager>();
			GameObject gameObject = Object.Instantiate(EqupedClothingItem, base.transform);
			gameObject.SetActive(value: true);
			NewButton.transform.SetAsLastSibling();
			gameObject.GetComponent<UnitEditorEquipedClothingCell>().Initialize(clothingWrapper, manager);
			spawnedButtons.Add(gameObject);
			return gameObject;
		}

		public void ClearAllButtons()
		{
			for (int i = 0; i < spawnedButtons.Count; i++)
			{
				Object.Destroy(spawnedButtons[i]);
			}
		}

		public void SetNewButtonState(bool v)
		{
			NewButton.SetActive(v);
		}
	}
}
