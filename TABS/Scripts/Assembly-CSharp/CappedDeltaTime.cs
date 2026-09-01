using UnityEngine;

public class CappedDeltaTime : MonoBehaviour
{
	public static float Time;

	private void Update()
	{
		Time = Mathf.Clamp(UnityEngine.Time.deltaTime, 0f, 0.05f);
	}
}
