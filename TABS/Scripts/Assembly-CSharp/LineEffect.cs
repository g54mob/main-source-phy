using System;
using System.Collections;
using UnityEngine;

public class LineEffect : TargetableEffect, GameObjectPooling.IPoolable
{
	private LineRenderer line;

	public AnimationCurve widthCurve;

	public float t = 1f;

	public bool IsManagedByPool { get; set; }

	public Action ReleaseSelf { get; set; }

	public void Initialize()
	{
	}

	public void Reset()
	{
	}

	public void Release()
	{
	}

	public override void DoEffect(Transform startPoint, Transform endPoint)
	{
		line = GetComponentInChildren<LineRenderer>();
		StartCoroutine(DoHeal(startPoint, endPoint));
	}

	private IEnumerator DoHeal(Transform startPoint, Transform target)
	{
		float c = 0f;
		Damagable health = target.transform.root.GetComponentInChildren<Damagable>();
		health.GetComponent<UnitColorHandler>();
		while (c < t && (bool)health)
		{
			for (int i = 0; i < line.positionCount; i++)
			{
				line.SetPosition(i, Vector3.Lerp(startPoint.position, target.position, (float)i / ((float)line.positionCount - 1f)));
			}
			line.widthMultiplier = widthCurve.Evaluate(c / t);
			c += Time.deltaTime;
			yield return null;
		}
		if (IsManagedByPool)
		{
			ReleaseSelf?.Invoke();
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	public override void DoEffect(Vector3 startPoint, Vector3 endPoint, Rigidbody targetRig = null)
	{
	}
}
