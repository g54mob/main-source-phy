using UnityEngine;
using UnityEngine.Events;

public class GenericTimerSceneSync : MonoBehaviour
{
	public string TimerID;

	public float CurrentTime;

	[Header("CurrentHours(Only Hours Part), CurrentMins(Only Mins Part), CurrentSeconds(Only Seconds Part), CurrentTime(Total Seconds)")]
	public UnityEvent OnTimeUpdate;

	public float CurrentHours => 0f;

	public float CurrentMins => 0f;

	public float CurrentSeconds => 0f;

	public void LateUpdate()
	{
	}
}
