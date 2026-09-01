using UnityEngine;

public class MoveBackAndForth : MonoBehaviour
{
	public Vector3 localPositionToMoveTo;

	public float t;

	private int dir = 1;

	private Vector3 originalPosition;

	private Vector3 globalPositionToMoveTo;

	private void Start()
	{
		originalPosition = base.transform.position;
		globalPositionToMoveTo = base.transform.InverseTransformPoint(originalPosition + localPositionToMoveTo);
		t = 0f;
	}

	private void Update()
	{
		if (t < 3f)
		{
			t += Time.deltaTime * (float)dir * Mathf.Pow(2f, -1f + t);
		}
		else if (t > 13f)
		{
			t += Time.deltaTime * (float)dir * Mathf.Pow(2f, 15f - t);
		}
		else
		{
			t += 4f * Time.deltaTime * (float)dir;
		}
		if (t > 16f)
		{
			t = 16f;
			dir = -1;
		}
		else if (t < 0f)
		{
			t = 0f;
			dir = 1;
		}
		base.transform.position = Vector3.Lerp(originalPosition, globalPositionToMoveTo, t / 16f);
	}
}
