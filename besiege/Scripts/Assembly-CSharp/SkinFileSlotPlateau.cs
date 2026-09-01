using UnityEngine;

public class SkinFileSlotPlateau : MonoBehaviour
{
	[SerializeField]
	private GameObject defaultPlateau;

	[SerializeField]
	private GameObject steamPlateau;

	[SerializeField]
	private GameObject wegamePlateau;

	[SerializeField]
	private GameObject modIOPlateau;

	public void Setup(PackType skinpackType)
	{
		WorkshopType workshopType = WorkshopManager.DetermineWorkshopType();
		wegamePlateau.SetActive(false);
		steamPlateau.SetActive(false);
		modIOPlateau.SetActive(false);
		defaultPlateau.SetActive(false);
		switch (skinpackType)
		{
		case PackType.Workshop:
			switch (workshopType)
			{
			case WorkshopType.Steam:
				steamPlateau.SetActive(true);
				break;
			case WorkshopType.ModIO:
				modIOPlateau.SetActive(true);
				break;
			default:
				wegamePlateau.SetActive(true);
				break;
			}
			break;
		default:
			defaultPlateau.SetActive(true);
			break;
		}
	}
}
