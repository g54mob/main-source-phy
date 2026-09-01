using UnityEngine;

public class LineRenderPos : MonoBehaviour
{
	public LineRenderer myLineRender;

	public Transform[] positionTargets;

	private void Start()
	{
		myLineRender.SetVertexCount(positionTargets.Length);
	}

	private void Update()
	{
		for (int i = 0; i < positionTargets.Length; i++)
		{
			myLineRender.SetPosition(i, positionTargets[i].position);
		}
	}
}
