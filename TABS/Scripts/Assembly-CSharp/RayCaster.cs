using UnityEngine;

public class RayCaster : MonoBehaviour
{
	public int raysPerFrame;

	public float spread;

	public float range = 5f;

	public bool scaleWithRange;

	private ProjectileHit projectileHit;

	private Holdable holdable;

	private DataHandler data;

	private void Start()
	{
		projectileHit = GetComponent<ProjectileHit>();
		holdable = GetComponentInParent<Holdable>();
	}

	private void LateUpdate()
	{
		if ((bool)holdable && !holdable.held)
		{
			return;
		}
		if (!data)
		{
			if ((bool)holdable && (bool)holdable.holderData)
			{
				data = holdable.holderData;
			}
			return;
		}
		for (int i = 0; i < raysPerFrame; i++)
		{
			Vector3 vector = base.transform.forward + Random.insideUnitSphere * 0.01f * spread;
			Debug.DrawRay(base.transform.position, vector * range, Color.red);
			Physics.Raycast(new Ray(base.transform.position, vector), out var hitInfo, range);
			if (!hitInfo.transform)
			{
				continue;
			}
			if (!hitInfo.rigidbody || hitInfo.transform.root == base.transform.root)
			{
				break;
			}
			DataHandler dataHandler = hitInfo.transform.GetComponentInParent<DataHandler>();
			if (!dataHandler)
			{
				Holdable componentInParent = hitInfo.transform.GetComponentInParent<Holdable>();
				if ((bool)componentInParent && (bool)componentInParent.holderData)
				{
					dataHandler = componentInParent.holderData;
				}
			}
			if ((bool)dataHandler && (bool)data && dataHandler.team == data.team)
			{
				break;
			}
			float multiplier = 1f - Vector3.Distance(base.transform.position, hitInfo.point) / range;
			projectileHit.Hit(hitInfo, multiplier);
		}
	}
}
