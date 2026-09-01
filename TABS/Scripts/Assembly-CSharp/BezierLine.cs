using UnityEngine;

public class BezierLine : MonoBehaviour
{
	public int segments = 10;

	public Transform position1;

	public Transform position2;

	public Transform position3;

	public Transform position4;

	private LineRenderer line;

	private Vector3[] positions;

	[HideInInspector]
	public bool done;

	private Vector3 doneVelocity;

	private void Start()
	{
		line = GetComponent<LineRenderer>();
		positions = new Vector3[segments];
		line.positionCount = segments;
	}

	private void Update()
	{
		for (int i = 0; i < positions.Length; i++)
		{
			if ((bool)position1 && (bool)position2 && (bool)position3 && (bool)position4)
			{
				positions[i] = BezierCurve.CubicBezier(position1.position, position2.position, position3.position, position4.position, (float)i / ((float)positions.Length - 1f));
			}
		}
		line.SetPositions(positions);
	}
}
