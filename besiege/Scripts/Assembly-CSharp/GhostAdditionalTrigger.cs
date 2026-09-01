using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Blocks/Ghost/GhostAdditionalTrigger")]
public class GhostAdditionalTrigger : MonoBehaviour
{
	public GhostTrigger ghostTrigger;

	public int overlayBlockLayer = 27;

	public List<int> layersToIgnore = new List<int> { 16 };

	protected int[] HUDLayers = new int[5] { 9, 13, 19, 21, 23 };

	private void OnTriggerEnter(Collider other)
	{
		if (!ColliderIsIgnored(other))
		{
			ghostTrigger.touchingCount++;
		}
		ghostTrigger.Check();
	}

	private void OnTriggerStay(Collider other)
	{
		if (ghostTrigger.touchingCount <= 0 && !ColliderIsIgnored(other))
		{
			ghostTrigger.touchingCount++;
			ghostTrigger.Check();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (!ColliderIsIgnored(other))
		{
			ghostTrigger.touchingCount--;
		}
		ghostTrigger.Check();
	}

	private bool ColliderIsIgnored(Collider col)
	{
		if (col == null)
		{
			return true;
		}
		if (col.CompareTag("DoubleBlock"))
		{
			return true;
		}
		if (col.gameObject.layer == overlayBlockLayer)
		{
			return true;
		}
		if (col.gameObject.name == "Adding Point" && col is BoxCollider && (col as BoxCollider).size.y != 0f)
		{
			return true;
		}
		for (int i = 0; i < HUDLayers.Length; i++)
		{
			if (col.gameObject.layer == HUDLayers[i])
			{
				return true;
			}
		}
		for (int j = 0; j < layersToIgnore.Count; j++)
		{
			if (col.gameObject.layer == layersToIgnore[j])
			{
				return true;
			}
		}
		return false;
	}
}
