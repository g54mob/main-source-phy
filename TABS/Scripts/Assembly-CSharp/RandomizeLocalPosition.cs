using UnityEngine;

public class RandomizeLocalPosition : MonoBehaviour
{
	public Vector3 min;

	public Vector3 max;

	private void Start()
	{
		RandomizePosition();
	}

	public void RandomizePosition()
	{
		base.transform.localPosition = new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z));
	}
}
