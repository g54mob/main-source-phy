using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Whip : MonoBehaviour
{
	public LayerMask mask;

	public UnityEvent alwaysEvent;

	public UnityEvent hitEvent;

	public UnityEvent dontHitEvent;

	public Transform top;

	public Transform rest;

	public Transform targetTop;

	public Transform targetMid;

	public Transform followTop;

	public Transform followMid;

	public float swingDelay;

	public float swingTime;

	public float duringSwingMultiplier = 1f;

	public float topGrav;

	public float midGrav;

	public float topSpring;

	public float topDrag;

	public float midSpring;

	public float midDrag;

	private Vector3 topVel;

	private Vector3 midVel;

	private DataHandler data;

	private LineRenderer line;

	private MeleeWeapon weapon;

	private List<FollowParticle> followParticles;

	private bool isSwinging;

	public float force;

	public float minMass;

	private Rigidbody target;

	public void Init()
	{
		line = GetComponentInChildren<LineRenderer>();
		data = base.transform.root.GetComponentInChildren<DataHandler>();
		weapon = GetComponentInParent<MeleeWeapon>();
	}

	public void Swing()
	{
		if (Vector3.Distance(data.targetMainRig.transform.position, data.mainRig.transform.position) < weapon.maxRange + 2f)
		{
			StartCoroutine(DoSwing());
		}
	}

	public void AddForceToTarget()
	{
		if ((bool)target)
		{
			WilhelmPhysicsFunctions.AddForceWithMinWeight(target, targetTop.transform.forward * force, ForceMode.Impulse, minMass);
		}
	}

	private IEnumerator DoSwing()
	{
		target = data.targetMainRig;
		alwaysEvent.Invoke();
		Ray ray = new Ray(base.transform.position, target.position - base.transform.position);
		RaycastHit hitInfo = default(RaycastHit);
		Physics.Raycast(ray, out hitInfo, Vector3.Distance(target.position, base.transform.position), mask);
		if ((bool)hitInfo.rigidbody)
		{
			target = hitInfo.rigidbody;
			dontHitEvent.Invoke();
		}
		else
		{
			hitEvent.Invoke();
		}
		float c = 0f;
		yield return new WaitForSeconds(swingDelay);
		isSwinging = true;
		while (c < swingTime && target != null)
		{
			c += Time.deltaTime;
			targetTop.position = target.position;
			yield return null;
		}
		isSwinging = false;
	}

	private void Start()
	{
		followParticles = new List<FollowParticle>();
		followParticles.AddRange(GetComponentsInChildren<FollowParticle>());
	}

	private void Update()
	{
		if (!isSwinging)
		{
			targetTop.position = top.position;
		}
		targetMid.position = (top.position + rest.position) * 0.5f;
		topVel = Vector3.Lerp(topVel, (targetTop.position - followTop.position) * topSpring, Time.deltaTime * topDrag);
		midVel = Vector3.Lerp(midVel, (targetMid.position - followMid.position) * midSpring, Time.deltaTime * midDrag);
		float num = duringSwingMultiplier;
		if (!isSwinging)
		{
			topVel += Time.deltaTime * topGrav * Vector3.down;
			midVel += midGrav * Time.deltaTime * Vector3.down;
			duringSwingMultiplier = 1f;
		}
		followTop.position += num * Time.deltaTime * topVel;
		followMid.position += num * Time.deltaTime * midVel;
		targetTop.rotation = Quaternion.LookRotation(targetTop.position - rest.position);
		followTop.rotation = Quaternion.LookRotation(rest.position - followTop.position);
		int num2 = 0;
		for (int i = 0; i < line.positionCount; i++)
		{
			line.SetPosition(i, BezierCurve.QuadraticBezier(rest.position, followMid.position, followTop.position, (float)i / (float)(line.positionCount - 1)));
			if (i + 1 == line.positionCount / followParticles.Count * (num2 + 1))
			{
				Vector3 position = line.GetPosition(i);
				followParticles[num2].transform.SetPositionAndRotation(position, Quaternion.LookRotation(line.GetPosition(i - 1) - line.GetPosition(i)));
				num2++;
			}
		}
	}
}
