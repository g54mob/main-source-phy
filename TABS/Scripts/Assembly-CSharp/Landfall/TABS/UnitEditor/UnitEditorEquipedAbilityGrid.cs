using System.Collections.Generic;
using UnityEngine;

namespace Landfall.TABS.UnitEditor
{
	[DefaultExecutionOrder(-10)]
	public class UnitEditorEquipedAbilityGrid : MonoBehaviour
	{
		private List<GameObject> spawnedButtons = new List<GameObject>();

		public GameObject EquipedAbilityCell;

		public GameObject NewButton;

		private UnitEditorManager manager;

		private void Awake()
		{
			manager = Object.FindObjectOfType<UnitEditorManager>();
		}

		public GameObject SpawnEquipedAbility(UnitEditorManager.EquipedSpecialAbility abilityWrapper)
		{
			if (manager == null)
			{
				manager = Object.FindObjectOfType<UnitEditorManager>();
			}
			GameObject gameObject = Object.Instantiate(EquipedAbilityCell, base.transform);
			gameObject.SetActive(value: true);
			NewButton.transform.SetAsLastSibling();
			gameObject.GetComponent<UnitEditorEquipedAbilityCell>().Initialize(abilityWrapper, manager);
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
