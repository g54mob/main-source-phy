using System.Collections;
using UnityEngine;

public class SphereRadiusChange : MonoBehaviour
{
	private SphereCollider sphereCollider;

	public AnimationCurve radiusCurve;

	private float counter;

	private void Start()
	{
		sphereCollider = GetComponent<SphereCollider>();
	}

	public void ChangeSphereRadius()
	{
		if ((bool)this && (bool)base.gameObject && (bool)sphereCollider)
		{
			StartCoroutine(ChangeRadius());
		}
	}

	private IEnumerator ChangeRadius()
	{
		if ((bool)this && (bool)base.gameObject && (bool)sphereCollider)
		{
			while (counter < radiusCurve.keys[radiusCurve.keys.Length - 1].time)
			{
				counter += Time.deltaTime;
				sphereCollider.radius = radiusCurve.Evaluate(counter);
				yield return null;
			}
		}
	}
}
