using TMPro;
using UnityEngine;

public class CharacterUI : SceneSingleton<CharacterUI>
{
	[SerializeField]
	private TextMeshProUGUI inventoryText;

	public void UpdateInventory(int currentSize, int maxSize)
	{
		inventoryText.text = currentSize + " / " + maxSize;
	}
}
