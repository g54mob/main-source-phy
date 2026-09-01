using UnityEngine;

public class ShockwaveExplosion : MonoBehaviour
{
	public float radius;

	public float explosionForce;

	public ParticleSystem explosion;

	public AudioSource explosionAudio;

	private float dot;

	public void ForceExplosion()
	{
		explosionAudio.Play();
		for (int i = 0; i < ReferenceMaster.ExternalForceObjectsArray.Length; i++)
		{
			ProcessItem(ReferenceMaster.ExternalForceObjectsArray[i]);
		}
		for (int i = 0; i < ReferenceMaster.ExternalForceTemp.Count; i++)
		{
			ProcessItem(ReferenceMaster.ExternalForceTemp[i]);
		}
	}

	protected bool ValidateBasicInfo(BasicInfo b)
	{
		if (object.ReferenceEquals(b, null) || b.isDestroyed || !b.isSimulating || b.noRigidbody || b.isKinematic)
		{
			return false;
		}
		if (b.transform == null)
		{
			Debug.LogError("ERROR! Transform null for Please notify the devs!");
			return false;
		}
		if (b.Rigidbody == null)
		{
			Debug.LogError("ERROR! Rigidbody null for" + b.transform.name + "Please notify the devs!");
			return false;
		}
		return true;
	}

	private void ProcessItem(BasicInfo bInfo)
	{
		if (ValidateBasicInfo(bInfo))
		{
			if (bInfo.Rigidbody.IsSleeping())
			{
				bInfo.Rigidbody.WakeUp();
			}
			bInfo.Rigidbody.isKinematic = false;
			bInfo.Rigidbody.useGravity = true;
			bInfo.Rigidbody.AddExplosionForce(explosionForce, base.transform.position, radius, 0f, ForceMode.Force);
		}
	}
}
