using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class VapireBite : MonoBehaviour
{
	public enum TargetPoint
	{
		Neck = 0,
		Head = 1
	}

	public AnimationCurve colorCurve;

	public UnitColorInstance color;

	public float damage = 10f;

	public float rate = 0.2f;

	public float bites = 10f;

	public float dist = 0.75f;

	private DataHandler data;

	private DataHandler targetData;

	public UnityEvent dmgEvent;

	public TargetPoint targetPoint;

	private Vector3 tp;

	private void Start()
	{
		data = base.transform.root.GetComponentInChildren<DataHandler>();
	}

	public void Bite()
	{
		if (!(data.targetData == null))
		{
			targetData = data.targetData;
			StartCoroutine(DoBite());
		}
	}

	private IEnumerator DoBite()
	{
		yield return new WaitForSeconds(0.5f);
		for (int i = 0; (float)i < bites; i++)
		{
			if (!data.Dead && (bool)targetData)
			{
				if (targetPoint == TargetPoint.Neck)
				{
					tp = targetData.mainRig.position + targetData.mainRig.transform.up * 0.15f;
				}
				if (targetPoint == TargetPoint.Head)
				{
					tp = targetData.head.position;
				}
				if (Vector3.Distance(data.head.position, tp) < dist)
				{
					StartCoroutine(DoColor());
					DoAttack();
				}
			}
			yield return new WaitForSeconds(rate);
		}
	}

	private IEnumerator DoColor()
	{
		UnitColorHandler colorHandler = targetData.GetComponentInParent<UnitColorHandler>();
		if ((bool)colorHandler)
		{
			float c = 0f;
			float t = colorCurve.keys[colorCurve.keys.Length - 1].time;
			while (c < t)
			{
				colorHandler.SetColor(color, colorCurve.Evaluate(c));
				c += Time.deltaTime;
				yield return null;
			}
		}
	}

	private void DoAttack()
	{
		if ((bool)targetData)
		{
			targetData.healthHandler.TakeDamage(damage, Vector3.down, null);
			dmgEvent.Invoke();
		}
	}
}
