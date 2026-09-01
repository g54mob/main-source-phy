using UnityEngine;

public class SetLineRendererPosition : MonoBehaviour
{
	public Transform[] targets;

	private LineRenderer lineRenderer;

	private void Start()
	{
		lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.positionCount = targets.Length;
	}

	private void LateUpdate()
	{
		for (int i = 0; i < targets.Length; i++)
		{
			lineRenderer.SetPosition(i, targets[i].position);
		}
	}
}
