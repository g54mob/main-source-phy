using System;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class InputBindingReference
{
	[SerializeField]
	private InputActionReference _action;

	[SerializeField]
	private string _bindingId;

	public string BindingId => null;
}
