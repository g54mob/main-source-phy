using UnityEngine;

public class RandomRotation : MonoBehaviour
{
	public float spread = 1f;

	private void Start()
	{
		base.transform.rotation = Quaternion.LookRotation(base.transform.forward + Random.insideUnitSphere * spread * 0.01f);
	}
}
