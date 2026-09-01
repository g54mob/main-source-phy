using UnityEngine;

[AddComponentMenu("Physics/ZeroGravityZone")]
public class ZeroGravityZone : SimBehaviour
{
	public float updateSpeed = 0.5f;

	private float distance = 100f;

	public SphereCollider sphere;

	public bool disableCollider = true;

	public GameObject[] hideObjectsOnSim;

	protected override void Start()
	{
		base.Start();
		if (base.isSimulating)
		{
			float num = sphere.radius * sphere.transform.lossyScale.x;
			distance = num * num;
			if (disableCollider)
			{
				sphere.enabled = false;
			}
			InvokeRepeating("UpdateItems", Random.Range(0f, 0.1f), updateSpeed);
			for (int i = 0; i < hideObjectsOnSim.Length; i++)
			{
				hideObjectsOnSim[i].SetActive(false);
			}
		}
	}

	private void UpdateItems()
	{
		if (base.isSimulating)
		{
			for (int i = 0; i < ReferenceMaster.ExternalForceObjectsArray.Length; i++)
			{
				processItem(ReferenceMaster.ExternalForceObjectsArray[i]);
			}
			for (int i = 0; i < ReferenceMaster.ExternalForceTemp.Count; i++)
			{
				processItem(ReferenceMaster.ExternalForceTemp[i]);
			}
		}
	}

	private void processItem(BasicInfo bInfo)
	{
		if (!Validate(bInfo))
		{
			return;
		}
		float dist = float.MaxValue;
		if (base.enabled && Contains(bInfo.transform.position, out dist))
		{
			if (!bInfo.isZeroG || bInfo.zeroGZoneDistance > dist)
			{
				bInfo.Rigidbody.useGravity = false;
				bInfo.isZeroG = true;
				bInfo.zeroGZoneDistance = dist;
				bInfo.zeroGZone = this;
			}
		}
		else if (bInfo.isZeroG && bInfo.zeroGZone == this)
		{
			bInfo.Rigidbody.useGravity = true;
			bInfo.isZeroG = false;
			bInfo.zeroGZoneDistance = float.MaxValue;
			bInfo.zeroGZone = null;
		}
	}

	private void OnDisable()
	{
		UpdateItems();
	}

	private bool Contains(Vector3 pos, out float dist)
	{
		dist = (pos - base.transform.position).sqrMagnitude;
		return distance > dist;
	}

	protected bool Validate(BasicInfo b)
	{
		if (object.ReferenceEquals(b, null) || b.isDestroyed || !b.isSimulating || b.noRigidbody || b.isKinematic)
		{
			return false;
		}
		return true;
	}
}
