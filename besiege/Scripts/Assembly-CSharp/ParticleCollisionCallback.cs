using System;
using UnityEngine;

public class ParticleCollisionCallback : MonoBehaviour
{
	public Action<BasicInfo> callback;

	private Collider col;

	private BasicInfo bInfo;

	protected void OnParticleCollision(GameObject other)
	{
		bInfo = other.GetComponent<BasicInfo>();
		if (!(bInfo == null) && !bInfo.noRigidbody && callback != null)
		{
			callback(bInfo);
		}
	}
}
