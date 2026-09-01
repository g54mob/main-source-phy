using System.Collections;
using UnityEngine;

public class MovingWeaponPart : MonoBehaviour
{
	[Header("--Force--")]
	public Vector3 m_force;

	public Vector3 m_rndForce;

	[Header("--Torque--")]
	public Vector3 m_torque;

	public Vector3 m_rndTorque;

	private Vector3 mStartPosition;

	private Vector3 velocity;

	private PhysicsFaker mFaker;

	public float delay;

	public float duration;

	public AnimationCurve curve;

	private void Start()
	{
		mFaker = GetComponent<PhysicsFaker>();
		mStartPosition = base.transform.localPosition;
	}

	private void Update()
	{
	}

	public void PlayRecoilAnimation(float multiplier = 1f)
	{
		if (base.gameObject.activeInHierarchy)
		{
			if (delay > 0f)
			{
				StartCoroutine(DelayRecoil(multiplier));
			}
			else
			{
				DoRecoil(multiplier);
			}
		}
	}

	private IEnumerator DelayRecoil(float multiplier = 1f)
	{
		yield return new WaitForSeconds(delay);
		DoRecoil(multiplier);
	}

	private void DoRecoil(float multiplier = 1f)
	{
		if ((bool)mFaker)
		{
			if (duration > 0f)
			{
				StartCoroutine(DoRecoilOverTime(multiplier));
				return;
			}
			mFaker.AddForceLocal((m_force + m_rndForce) * multiplier);
			mFaker.AddTorqueLocal(m_torque * multiplier);
		}
	}

	private IEnumerator DoRecoilOverTime(float multiplier = 1f)
	{
		float t = 0f;
		while (t < duration)
		{
			t += Time.deltaTime;
			float num = curve.Evaluate(t / duration);
			mFaker.AddForceLocal(m_force * multiplier * num * Time.deltaTime);
			mFaker.AddTorqueLocal(m_torque * multiplier * num * Time.deltaTime);
			yield return null;
		}
	}
}
