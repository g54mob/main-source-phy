using UnityEngine;

public class TimeToLive : MonoBehaviour
{
	public float timeToLive;

	public bool useDestroyImmediate;

	public bool disableOnly;

	private void FixedUpdate()
	{
		timeToLive -= Time.fixedDeltaTime;
		if (!(timeToLive <= 0f))
		{
			return;
		}
		if (!disableOnly)
		{
			if (useDestroyImmediate)
			{
				Object.DestroyImmediate(base.gameObject);
			}
			else
			{
				Object.Destroy(base.gameObject);
			}
		}
		else
		{
			base.gameObject.SetActive(value: false);
			timeToLive = float.MaxValue;
			base.enabled = false;
		}
	}
}
