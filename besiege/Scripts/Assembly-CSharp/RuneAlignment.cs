using UnityEngine;

public class RuneAlignment : MonoBehaviour
{
	public GameObject[] runes;

	public float treshold = 0.1f;

	public float radius;

	public float explisionForce;

	public ParticleSystem explosion;

	public AudioSource explosionAudio;

	private Vector3 up = Vector3.up;

	private float dot;

	private void Start()
	{
		if (StatMaster.levelSimulating)
		{
			radius *= radius;
		}
	}

	private void Update()
	{
		if (!StatMaster.levelSimulating)
		{
			return;
		}
		for (int i = 1; i < runes.Length; i++)
		{
			dot = Vector3.Dot(up, (runes[0].transform.position - runes[i].transform.position).normalized);
			if (dot < treshold)
			{
				return;
			}
		}
		explosion.Play();
		ForceExplosion();
		WinCondition.currentObjsCompleted++;
		base.enabled = false;
	}

	private void ForceExplosion()
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
			bInfo.Rigidbody.AddExplosionForce(explisionForce, base.transform.position, radius, 0f, ForceMode.Force);
		}
	}
}
