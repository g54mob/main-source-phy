using UnityEngine;

public class Carving : MonoBehaviour
{
	public Transform verticesParent;

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.magenta;
		Gizmos.DrawSphere(base.transform.position, 0.01f);
		if (verticesParent == null || verticesParent.childCount < 2)
		{
			return;
		}
		Gizmos.color = Color.darkOrange;
		for (int i = 0; i < verticesParent.childCount; i++)
		{
			Transform child = verticesParent.GetChild(i);
			if (!(child == null))
			{
				Transform child2 = verticesParent.GetChild((i + 1) % verticesParent.childCount);
				if (!(child2 == null) && child != null && child2 != null)
				{
					Gizmos.DrawLine(child.position, child2.position);
				}
			}
		}
	}
}
