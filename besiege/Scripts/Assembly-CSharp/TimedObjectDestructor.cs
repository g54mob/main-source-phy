using UnityEngine;

public class TimedObjectDestructor : MonoBehaviour
{
	public float timeOut = 1f;

	public bool detachChildren;

	public bool disableOffScreen;

	public bool hasInfo;

	public BasicInfo info;

	public GlobalParticles.ParticleType particle = GlobalParticles.ParticleType.BreakWood;

	private void Awake()
	{
		Invoke("DestroyNow", timeOut);
	}

	private void DestroyNow()
	{
		if (detachChildren)
		{
			base.transform.DetachChildren();
		}
		base.gameObject.SetActive(false);
		if (hasInfo)
		{
			GlobalParticles.EmitParticleBursts((int)particle, info.GetCenter());
		}
	}

	private void OnBecameInvisible()
	{
		if (disableOffScreen)
		{
			DestroyNow();
		}
	}
}
