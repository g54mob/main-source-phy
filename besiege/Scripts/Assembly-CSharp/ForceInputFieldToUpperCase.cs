using System;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/Canvas/Force InputField To Upper Case")]
[RequireComponent(typeof(InputField))]
public class ForceInputFieldToUpperCase : MonoBehaviour
{
	private InputField inputField;

	private void Awake()
	{
		inputField = GetComponent<InputField>();
		InputField obj = inputField;
		obj.onValidateInput = (InputField.OnValidateInput)Delegate.Combine(obj.onValidateInput, new InputField.OnValidateInput(ValidateInput));
	}

	private void OnDestroy()
	{
		InputField obj = inputField;
		obj.onValidateInput = (InputField.OnValidateInput)Delegate.Remove(obj.onValidateInput, new InputField.OnValidateInput(ValidateInput));
	}

	private char ValidateInput(string input, int charIndex, char addedChar)
	{
		return char.ToUpper(addedChar);
	}
}
