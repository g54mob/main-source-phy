using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Light))]
public class LightPulseSync : MonoBehaviour
{
	[Tooltip("The Renderer whose material uses the 'AmplifyShaderPack/Emmisive Flashing' shader. The script reads _Emmision and _PulseFrequency from this material each frame.")]
	public Renderer targetRenderer;

	[Tooltip("Index of the material slot on the Renderer that uses the flashing shader. 0 = first material, 1 = second, etc.")]
	public int materialIndex;

	[Tooltip("The Lens Flare (SRP) component to keep in sync with the light pulse. Its intensity will be scaled by the same pulse factor as the light each frame. Leave empty if you have no lens flare on this light.")]
	public LensFlareComponentSRP lensFlare;

	[Header("Phase")]
	[Tooltip("Offsets the light pulse phase relative to the material, in degrees.\n0   = perfectly in sync.\n180 = fully inverted (light bright when material is dark).\nAdjust this at runtime until the light and glow feel locked together.")]
	[Range(0f, 360f)]
	public float phaseOffsetDegrees;

	[Header("Light Intensity")]
	[Tooltip("Scales the final light intensity on top of the emissive luminance. Luminance is derived from the HDR _Emmision colour (Rec.709: 0.2126 R + 0.7152 G + 0.0722 B). Raise this if the light feels too dim relative to the glow, lower it if too bright.\nExample: 1 = direct match, 2 = twice as bright as the raw luminance.")]
	[Min(0f)]
	public float intensityMultiplier;

	[Tooltip("Hard upper cap on the light's intensity. Useful when the HDR emission colour has very high values. Set to 0 to disable the cap.")]
	[Min(0f)]
	public float maxLightIntensity;

	[Header("Lens Flare Intensity")]
	[Tooltip("The intensity the Lens Flare will have when the pulse is at its peak (pulse factor = 1). At pulse zero the lens flare intensity will be 0, eliminating the flare entirely. This is independent of the Light intensity multiplier so you can tune each separately.\nExample: 1 = standard full brightness at peak, 0.5 = half brightness at peak.")]
	[Min(0f)]
	public float maxFlareIntensity;

	private Light _light;

	private Material _material;

	private static readonly int PropEmmision;

	private static readonly int PropPulseFrequency;

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

	private void CacheMaterial()
	{
	}
}
