using UnityEngine;

public class ScaleBetweenTwoPoints : MonoBehaviour
{
	public Transform startPos;

	public Transform endPos;

	public float widthy;

	public Transform obj;

	protected Vector3 startScale;

	public bool dontScaleZ;

	public void Align()
	{
		if (startPos == null || endPos == null)
		{
			Debug.Log("[ScaleBetweenTwoPoints] StartPos or EndPos is null!", base.gameObject);
			return;
		}
		startScale = obj.transform.localScale;
		ScaleBetweenPoints(startPos.position, endPos.position, widthy);
	}

	private void ScaleBetweenPoints(Vector3 start, Vector3 end, float width)
	{
		Vector3 vector = end - start;
		Vector3 localScale = new Vector3(startScale.x, vector.magnitude / 2f, startScale.y);
		localScale.y *= 2f;
		Vector3 position = start + vector / 2f;
		obj.position = position;
		obj.transform.up = vector;
		if (dontScaleZ)
		{
			localScale.z = startScale.z;
		}
		obj.transform.localScale = localScale;
	}
}
