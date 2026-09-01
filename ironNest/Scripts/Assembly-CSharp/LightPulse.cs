using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightPulse : MonoBehaviour
{
	[Tooltip("How many complete pulse cycles (bright → off → bright) occur per second.\nExample: 1 = one full pulse per second, 0.5 = one pulse every two seconds.")]
	[Min(0.01f)]
	public float frequency;

	[Tooltip("Controls the shape of the pulse curve.\n• 0 = hard square wave (instant snap between bright and off).\n• 1 = smooth sine wave (gradual, organic fade).\nValues in between blend between the two behaviours.")]
	[Range(0f, 1f)]
	public float smoothing;

	private Light _light;

	private float _baseIntensity;

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void Update()
	{
	}
}
