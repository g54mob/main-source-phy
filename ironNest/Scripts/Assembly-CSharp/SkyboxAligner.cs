using UnityEngine;

public class SkyboxAligner : MonoBehaviour
{
	[Tooltip("Transform whose Y rotation will drive the skybox rotation.")]
	public Transform target;

	[Tooltip("Skybox rotation property name (some shaders use \"_Rotation\" or \"_rotation\").")]
	public string rotationProperty;

	[Tooltip("Degrees offset applied to the skybox rotation.")]
	public float offset;

	[Tooltip("Invert the direction of the skybox rotation if the current direction is backwards.")]
	public bool invert;

	private Material runtimeSkybox;

	private void Awake()
	{
	}

	private void Update()
	{
	}

	private void OnDestroy()
	{
	}
}
