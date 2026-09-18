using System;
using UnityEngine;
using VInspector;

[Serializable]
public class ShelfSpace
{
	public Vector3 position;

	[ReadOnly]
	public ShelfSpaceController shelfSpaceController;

	[ReadOnly]
	public PotionController potionController;

	[ReadOnly]
	public ShelfSpacePlacementType shelfSpacePlacementType;
}
