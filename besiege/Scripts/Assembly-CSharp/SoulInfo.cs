using UnityEngine;

public class SoulInfo : MonoBehaviour
{
	private void Start()
	{
		if (SoulsContainer.hasInstance)
		{
			SoulsContainer.instance.HarvestSoul();
		}
	}
}
