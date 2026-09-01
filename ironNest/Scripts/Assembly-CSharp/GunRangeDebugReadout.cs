using UnityEngine;

[DisallowMultipleComponent]
public class GunRangeDebugReadout : MonoBehaviour, IFloatValueProvider
{
	[Header("Target")]
	[Tooltip("GunController to read range from.\nIf null, this component outputs 0.\nPrefab-friendly tip: enable 'Auto Find On Validate' to auto-assign the GunController from the same GameObject.")]
	[SerializeField]
	private GunController gun;

	[Header("Behavior")]
	[Tooltip("If true, when edited/added in the Editor this component will auto-assign 'gun' by calling GetComponent<GunController>() on the same GameObject.\nSafe default for prefabs; does not search the scene.")]
	[SerializeField]
	private bool autoFindOnValidate;

	[Header("Inspector Output (Read Only)")]
	[Tooltip("The range that would be used if the gun fired right now.\nReturns 0 when the gun cannot currently fire (reloading / no shell / missing reload controller).\nThis value is also returned by GetFloatValue() for UI bindings such as OdometerDisplay.")]
	[SerializeField]
	private float predictedRangeIfFiredNow;

	[Header("Output Options")]
	[Tooltip("If true, clamps the output to be non-negative.\nRecommended: true (range should not be negative).")]
	[SerializeField]
	private bool clampNonNegative;

	public float PredictedRangeIfFiredNow => 0f;

	public float GetFloatValue()
	{
		return 0f;
	}

	private void OnValidate()
	{
	}

	private void Awake()
	{
	}

	private void Update()
	{
	}

	private void UpdateReadout()
	{
	}
}
