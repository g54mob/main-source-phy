using System.Reflection;
using UnityEngine;

public class OptionsToggle : ClickBehaviour
{
	[SerializeField]
	private string optionName;

	[SerializeField]
	private Material redMaterial;

	[SerializeField]
	private Material darkMaterial;

	private bool optionValue;

	private Renderer myRenderer;

	private PropertyInfo optionProperty;

	private void Awake()
	{
		myRenderer = GetComponent<Renderer>();
		if (myRenderer == null)
		{
			Debug.LogWarning("Could not find renederer, disabling OptionsToggle.");
			base.enabled = false;
			return;
		}
		if (string.IsNullOrEmpty(optionName))
		{
			Debug.LogWarning("Option can not be null or empty.");
			base.enabled = false;
			return;
		}
		optionProperty = typeof(BesiegeConfig).GetProperty(optionName);
		if (string.IsNullOrEmpty(optionName))
		{
			Debug.LogWarning("Could not find config field '" + optionName + "'.");
			base.enabled = false;
		}
		else if (optionProperty.PropertyType != typeof(bool))
		{
			Debug.LogWarning("Config field '" + optionName + "' is not a boolean.");
			base.enabled = false;
		}
	}

	private void Start()
	{
		optionValue = (bool)optionProperty.GetValue(OptionsMaster.BesiegeConfig, null);
		SwitchMaterial();
	}

	public override void OnClicked()
	{
		Toggle();
	}

	private void SwitchMaterial()
	{
		if (optionValue)
		{
			myRenderer.material = redMaterial;
		}
		else
		{
			myRenderer.material = darkMaterial;
		}
	}

	private void Toggle()
	{
		if (optionValue)
		{
			optionValue = false;
		}
		else
		{
			optionValue = true;
		}
		SwitchMaterial();
		SetOption();
	}

	private void SetOption()
	{
		optionProperty.SetValue(OptionsMaster.BesiegeConfig, optionValue, null);
	}
}
