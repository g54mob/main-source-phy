using UnityEngine;

public interface IFilterableItem
{
	string FilteringName { get; }

	GameObject ItemCellGameObject { get; }
}
