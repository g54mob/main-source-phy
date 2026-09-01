using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ActionOnInput : MonoBehaviour
{
	[SerializeField]
	private InputActionReference action;

	[SerializeField]
	private UnityEvent onEvent;

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnEvent(InputAction.CallbackContext callbackContext)
	{
	}
}
