using System.Collections;
using UnityEngine;

public class EnableLaserOnSim : MonoBehaviour
{
	public LaserOrbAudio sfx;

	public ParticleSystem chargeRing;

	public ParticleSystemRenderer starRenderer;

	public GameObject laserToEnable;

	public SmoothLookAtMachine laserFollowToEnable;

	public float Timer;

	public bool environmentBlocksVision;

	public LayerMask mask;

	public float distanceToActive = 50f;

	public float angleToActive = 10f;

	private bool laserEnabled;

	private float startAlpha = 1f;

	private Vector3 startScale = Vector3.one;

	protected IEnumerator Start()
	{
		Color c = starRenderer.material.GetColor("_TintColor");
		startAlpha = c.a;
		startScale = chargeRing.transform.localScale;
		if (StatMaster.levelSimulating)
		{
			chargeRing.transform.localScale = Vector3.zero;
			starRenderer.material.SetColor("_TintColor", new Color(c.r, c.g, c.b, 0f));
			if (!environmentBlocksVision)
			{
				StartAnimation(true);
			}
			chargeRing.gameObject.SetActive(true);
		}
		yield break;
	}

	public void StartAnimation(bool enable, bool wait = true)
	{
		if (laserEnabled != enable)
		{
			StopAllCoroutines();
			float time = ((!enable) ? 1f : Timer);
			StartCoroutine(OnAnimation(enable, time, false));
			laserEnabled = enable;
			if (enable)
			{
				sfx.Charge();
			}
			else
			{
				sfx.Stop();
			}
		}
	}

	protected IEnumerator OnAnimation(bool enable, float time, bool wait = true)
	{
		if (StatMaster.levelSimulating)
		{
			if (wait)
			{
				yield return new WaitForSeconds(1f);
			}
			Color c = starRenderer.material.GetColor("_TintColor");
			Vector3 scale = chargeRing.transform.localScale;
			float targ = ((!enable) ? 0f : 1f);
			for (float t = 0f; t < time; t += Time.deltaTime)
			{
				float pct = t / time;
				chargeRing.transform.localScale = Vector3.Lerp(scale, startScale * targ, pct);
				starRenderer.material.SetColor("_TintColor", new Color(c.r, c.g, c.b, Mathf.Lerp(c.a, startAlpha * targ, pct)));
				yield return null;
			}
			chargeRing.transform.localScale = startScale * targ;
			starRenderer.material.SetColor("_TintColor", new Color(c.r, c.g, c.b, startAlpha * targ));
			if (!environmentBlocksVision)
			{
				Activate(true);
				laserFollowToEnable.enabled = true;
			}
			else
			{
				Activate(enable);
			}
		}
	}

	protected void Update()
	{
		if (!StatMaster.levelSimulating || !environmentBlocksVision || laserFollowToEnable == null)
		{
			return;
		}
		Vector3 direct;
		if (laserFollowToEnable.target == null)
		{
			laserFollowToEnable.GetNewTarget();
		}
		else if (CanSee(laserFollowToEnable.target, out direct))
		{
			RaycastHit hitInfo;
			if (!laserEnabled && Physics.Raycast(base.transform.position, base.transform.forward, out hitInfo, 450f) && (Vector3.Dot(base.transform.forward, direct) > Mathf.Cos(angleToActive) || (hitInfo.point - laserFollowToEnable.target.position).magnitude < distanceToActive))
			{
				EnableLaser();
			}
			laserFollowToEnable.enabled = true;
		}
		else
		{
			DisableLaser();
			laserFollowToEnable.enabled = false;
		}
	}

	private void EnableLaser()
	{
		StartAnimation(true, false);
	}

	private void DisableLaser()
	{
		StartAnimation(false, false);
	}

	private void Activate(bool a)
	{
		if (a)
		{
			sfx.Activate();
			sfx.Loop(sfx.activate.clip.length);
		}
		laserToEnable.SetActive(a);
	}

	private bool CanSee(Transform target, out Vector3 direct)
	{
		direct = (target.position - base.transform.position).normalized;
		RaycastHit hitInfo;
		return Physics.SphereCast(base.transform.position, 1f, direct, out hitInfo, 250f, mask) && !IsStaticEnvironment(hitInfo.collider.transform);
	}

	private bool IsStaticEnvironment(Transform t)
	{
		if (t.root == Machine.Active().transform || ((bool)t.parent && t.parent.name == "Knights") || ((bool)t.parent.parent && t.parent.parent.name == "Knights"))
		{
			return false;
		}
		return true;
	}
}
