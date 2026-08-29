using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(ReflectionProbe))]
public class ReflectionProbeOverride : MonoBehaviour
{
	private ReflectionProbe reflectionProbe;

	private void OnEnable()
	{
		reflectionProbe = GetComponent<ReflectionProbe>();
		Shader.EnableKeyword("_GLOBAL_CUSTOM_REFLECTION_PROBE");
		reflectionProbe.center = Vector3.up * 1000f;
		reflectionProbe.size = Vector3.zero;
		reflectionProbe.importance = 0;
	}

	private void OnDisable()
	{
		Shader.SetGlobalTexture("_GlobalCustomSpecCube", null);
		Shader.DisableKeyword("_GLOBAL_CUSTOM_REFLECTION_PROBE");
	}

	private void Update()
	{
		Shader.SetGlobalVector("_GlobalCustomSpecCubeHDR", reflectionProbe.textureHDRDecodeValues);
		Shader.SetGlobalTexture("_GlobalCustomSpecCube", reflectionProbe.texture);
	}
}
