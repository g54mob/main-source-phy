using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Levels/LineRenderFlag")]
public class LineRenderFlag : MonoBehaviour
{
	public LineRenderer lineRenderer;

	public int lengthOfLineRenderer = 20;

	public float height = 0.1f;

	public float speed = 5f;

	public float lerpSpeed = 10f;

	public List<Vector3> posList = new List<Vector3>();

	private Vector3 pos;

	private Transform myTransform;

	public Vector2 direction = new Vector2(1f, 0f);

	private void Start()
	{
		myTransform = base.transform;
		lineRenderer.enabled = true;
		lineRenderer.SetVertexCount(lengthOfLineRenderer);
		for (int i = 0; i < posList.Count; i++)
		{
			posList[i] = myTransform.position;
		}
		if (!Application.isPlaying)
		{
			Animate();
		}
	}

	private void LateUpdate()
	{
		Animate();
	}

	private void Animate()
	{
		float time = Time.time;
		for (int i = 0; i < lengthOfLineRenderer; i++)
		{
			pos = new Vector3(direction.x * (float)i * -0.5f, Mathf.Sin((float)i + time * speed) * (float)i * height, direction.y * (float)i * -0.5f);
			Vector3 vector = posList[(int)(((float)posList.Count - 1f) * ((float)i * 1f / ((float)lengthOfLineRenderer * 1f)))];
			lineRenderer.SetPosition(i, pos + vector);
		}
		Tick();
	}

	private void Tick()
	{
		posList.Insert(0, myTransform.position);
		posList.RemoveAt(posList.Count - 1);
	}

	private void OnDrawGizmosSelected()
	{
		if (!Application.isPlaying)
		{
			Animate();
		}
	}
}
