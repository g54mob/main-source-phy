using UnityEngine;

public class UnitEditorSaveCheckmark : MonoBehaviour
{
	public GameObject failedStateObject;

	public GameObject successStateObject;

	private bool state;

	public void SetState(bool newState)
	{
		state = newState;
		failedStateObject.SetActive(!newState);
		successStateObject.SetActive(newState);
	}

	public bool GetState()
	{
		return state;
	}
}
