using UnityEngine;

public class CurrentAnisotropic : MonoBehaviour
{
	public TextMesh resText;

	private void OnStart()
	{
		Set();
	}

	private void Set()
	{
		resText.text = QualitySettings.anisotropicFiltering.ToString();
	}

	private void Set(AnisotropicFiltering type)
	{
		resText.text = type.ToString();
	}
}
