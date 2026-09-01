using UnityEngine;

public class ResetTimeScaleForMenus : MonoBehaviour
{
	public float customTime = 1f;

	private void Awake()
	{
		Time.timeScale = customTime;
	}
}
