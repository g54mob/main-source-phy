using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour
{
	public List<Rigidbody> foodItems;

	private float numberOfItems;

	private void Start()
	{
		foreach (Transform item in base.gameObject.transform)
		{
			if (!foodItems.Contains(item.GetComponent<Rigidbody>()))
			{
				foodItems.Add(item.GetComponent<Rigidbody>());
			}
		}
	}

	private void Update()
	{
		if (!StatMaster.levelSimulating || !WinCondition.hasWon)
		{
			return;
		}
		foreach (Rigidbody foodItem in foodItems)
		{
			foodItem.gameObject.SetActive(true);
		}
	}
}
