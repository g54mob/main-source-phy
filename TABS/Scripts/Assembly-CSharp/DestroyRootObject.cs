using Landfall.TABS;
using Photon.Bolt;
using UnityEngine;

public class DestroyRootObject : MonoBehaviour
{
	public void DestroyRoot()
	{
		if (BoltNetwork.IsRunning)
		{
			Unit componentInParent = GetComponentInParent<Unit>();
			if (componentInParent != null && componentInParent.IsRemotelyControlled)
			{
				return;
			}
		}
		Object.Destroy(base.transform.root.gameObject);
	}
}
