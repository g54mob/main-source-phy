using UnityEngine;

public class BoyancyDebug : MonoBehaviour
{
	private Vector3 pos;

	public float offset;

	private void Awake()
	{
		pos = base.transform.position;
	}

	private void Update()
	{
		base.transform.position = pos + Vector3.up * PirateWaterManager.GetValue(pos) + Vector3.up * offset;
	}
}
