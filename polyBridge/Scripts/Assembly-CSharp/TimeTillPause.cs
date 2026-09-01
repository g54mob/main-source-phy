using UnityEngine;

public class TimeTillPause : MonoBehaviour
{
	public float timeTillPause;

	private void FixedUpdate()
	{
		timeTillPause -= Time.fixedDeltaTime;
		if (timeTillPause <= 0f)
		{
			Debug.Break();
			Object.Destroy(this);
		}
	}
}
