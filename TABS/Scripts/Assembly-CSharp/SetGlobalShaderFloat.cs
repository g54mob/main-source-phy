using UnityEngine;

public class SetGlobalShaderFloat : MonoBehaviour
{
	public string propertyName = "Foliage_GlobalAmplitude";

	public float value;

	private void OnEnable()
	{
		Shader.SetGlobalFloat(propertyName, value);
	}
}
