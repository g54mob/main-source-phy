using UnityEngine;
using UnityEngine.Events;

public class ObjectActiveToggleEvent : MonoBehaviour
{
	public GameObject target;

	public UnityEvent turnOnEvent;

	public UnityEvent turnOffEvent;

	private bool isOn;

	private void Update()
	{
		if (isOn != target.activeSelf)
		{
			isOn = target.activeSelf;
			if (isOn)
			{
				turnOnEvent.Invoke();
			}
			else
			{
				turnOffEvent.Invoke();
			}
		}
	}
}
