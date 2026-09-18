using System.Collections.Generic;
using UnityEngine;

public class PotionsColorsTestManager : MonoBehaviour
{
	[SerializeField]
	private List<PotionController> potionsControllers;

	[SerializeField]
	private List<PotionSignController> signePotionsColors;

	private void Start()
	{
		int num = 0;
		foreach (PotionCategory potionCategorys in Singleton<PotionsDataHolder>.Instance.potionCategorysList)
		{
			potionsControllers[num].SetColor(potionCategorys.color);
			signePotionsColors[num].SetSignColor(potionCategorys.color);
			num++;
		}
	}
}
