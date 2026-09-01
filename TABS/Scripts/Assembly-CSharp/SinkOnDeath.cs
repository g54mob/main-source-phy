using System.Collections;
using UnityEngine;

public class SinkOnDeath : MonoBehaviour
{
	private DataHandler data;

	private bool done;

	public float time = 2f;

	public float moveMultiplier = 0.3f;

	public bool scale = true;

	public float secUntilScale = 15f;

	private float scaleMultiplier;

	private void Start()
	{
		GetComponentInChildren<HealthHandler>().AddDieAction(Sink);
	}

	public void Sink()
	{
		if (!done)
		{
			data = GetComponentInChildren<DataHandler>();
			done = true;
			StartCoroutine(DoSink());
		}
	}

	private IEnumerator DoSink()
	{
		yield return new WaitForSeconds(time);
		float c = 0f;
		while (c < 3f)
		{
			data.mainRig.drag *= 2f;
			if (data.mainRig.velocity.magnitude < 1f)
			{
				c += Time.deltaTime;
			}
			yield return null;
		}
		Rigidbody[] componentsInChildren = GetComponentsInChildren<Rigidbody>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			if ((bool)componentsInChildren[i])
			{
				componentsInChildren[i].isKinematic = true;
			}
		}
		if ((bool)data && (bool)data.mainRig)
		{
			data.mainRig.isKinematic = true;
		}
		float t = 0f;
		while (t < 30f)
		{
			base.transform.position += Mathf.Clamp(t * 0.1f, 0f, 1f) * moveMultiplier * Time.deltaTime * Vector3.down;
			t += Time.deltaTime;
			if (scale && t > secUntilScale)
			{
				scaleMultiplier += Time.deltaTime * 0.35f;
			}
			base.transform.localScale *= Mathf.Lerp(1f, 0f, scaleMultiplier);
			yield return null;
		}
	}
}
