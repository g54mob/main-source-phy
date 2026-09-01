using BesiegeDlc;
using UnityEngine;

public class WaterExpansionChestCheck : MonoBehaviour
{
	public GameObject waterChest;

	public GameObject[] expansionObjects;

	private void Start()
	{
		if (DlcManager.Instance.HasPurchasedDlc(DlcManager.DlcType.Water) && TutorialFileManager.GetTutorialState("WaterExpansion") != 1)
		{
			waterChest.SetActive(true);
			for (int i = 0; i < expansionObjects.Length; i++)
			{
				expansionObjects[i].SetActive(false);
			}
		}
	}
}
