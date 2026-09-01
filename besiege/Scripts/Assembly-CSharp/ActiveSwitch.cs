using UnityEngine;

public class ActiveSwitch : MonoBehaviour
{
	public bool StartingState;

	public string SwitchName;

	private bool currentState;

	public bool CurrentState
	{
		get
		{
			return currentState;
		}
		set
		{
			currentState = value;
		}
	}

	private void Start()
	{
		currentState = StartingState;
	}
}
