using System;
using UnityEngine.InputSystem;

[Serializable]
public class InputBindingReference
{
	private InputActionReference _action;

	private string _bindingId;

	public string BindingId => _bindingId;
}
