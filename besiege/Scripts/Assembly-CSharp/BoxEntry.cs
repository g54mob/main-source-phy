using UnityEngine;

internal class BoxEntry
{
	public Transform transform;

	public InfoBoxController infoBoxController;

	public BoxEntry(Transform go)
	{
		transform = go;
		infoBoxController = go.GetComponent<InfoBoxController>();
	}
}
