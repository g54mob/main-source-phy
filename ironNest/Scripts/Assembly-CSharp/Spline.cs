using UnityEngine;

public class Spline : MonoBehaviour
{
	[SerializeField]
	private Transform _start;

	[SerializeField]
	private Transform _middle;

	[SerializeField]
	private Transform _end;

	[SerializeField]
	private bool showGizmos;

	[SerializeField]
	public string jumpUpTrigger;

	[SerializeField]
	public string jumpDownTrigger;

	private Vector3 CalculatePosition(float value01, Vector3 startPos, Vector3 endPos, Vector3 midPos)
	{
		return default(Vector3);
	}

	public Vector3 CalculatePosition(float interpolationAmount01)
	{
		return default(Vector3);
	}

	public Vector3 CalculatePositionCustomStart(float interpolationAmount01, Vector3 startPosition)
	{
		return default(Vector3);
	}

	public Vector3 CalculatePositionCustomEnd(float interpolationAmount01, Vector3 endPosition)
	{
		return default(Vector3);
	}

	public void SetPoints(Vector3 startPoint, Vector3 midPointPosition, Vector3 endPoint)
	{
	}

	private void OnDrawGizmos()
	{
	}
}
