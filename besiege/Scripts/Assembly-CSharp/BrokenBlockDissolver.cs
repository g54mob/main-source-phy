using System.Collections.Generic;
using UnityEngine;

public class BrokenBlockDissolver : MonoBehaviour
{
	public List<ObjectToDissolve> objects;

	[SerializeField]
	private float dissolvingTime = 15f;

	private float timer;

	private float lerpStep;

	private float waitingTimer;

	public float timeToWait = 3f;

	private void Start()
	{
	}

	private void Update()
	{
		if (waitingTimer < timeToWait)
		{
			waitingTimer += Time.deltaTime;
			return;
		}
		if (objects[objects.Count - 1].initialTransform.localScale.magnitude <= new Vector3(0.01f, 0.01f, 0.01f).magnitude)
		{
			Object.Destroy(base.gameObject);
		}
		timer += Time.deltaTime;
		if (timer >= dissolvingTime)
		{
			Object.Destroy(base.gameObject);
		}
		lerpStep = timer / dissolvingTime;
		foreach (ObjectToDissolve @object in objects)
		{
			@object.renderer.transform.localScale = Vector3.Lerp(@object.initialTransform.localScale, new Vector3(0.01f, 0.01f, 0.01f), lerpStep);
		}
	}

	public void AddElement(MeshRenderer renderer, Transform transf)
	{
		objects.Add(new ObjectToDissolve(renderer, transf));
	}

	public void Init()
	{
		objects = new List<ObjectToDissolve>();
	}
}
