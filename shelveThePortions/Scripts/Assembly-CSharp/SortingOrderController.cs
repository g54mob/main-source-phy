using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(SortingGroup))]
public class SortingOrderController : MonoBehaviour
{
	[SerializeField]
	private SortingGroup sortingGroup;

	[SerializeField]
	private int offsetAddedSortLayer;

	private Transform trans;

	[SerializeField]
	private bool updateOnEnable;

	private void Awake()
	{
		trans = GetComponent<Transform>();
		sortingGroup = GetComponent<SortingGroup>();
	}

	private void OnEnable()
	{
		if (updateOnEnable)
		{
			UpdateSorting();
		}
	}

	public void SetHoldedPlacableLayer()
	{
		sortingGroup.sortingLayerName = "HoldedPlacable";
	}

	public void SetVillageObjectLayer()
	{
		sortingGroup.sortingLayerName = "VillageObject";
	}

	public void UpdateSorting()
	{
		sortingGroup.sortingOrder = -(int)(trans.position.y * 100f) + offsetAddedSortLayer;
	}
}
