using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class EditorVolumeRefs : MonoBehaviour
{
	public Volume volume;

	public WaterSurface ocean;

	public Exposure exposure => get<Exposure>();

	public ScreenSpaceAmbientOcclusion ssao => get<ScreenSpaceAmbientOcclusion>();

	public Bloom bloom => get<Bloom>();

	public ColorAdjustments colorAdjustments => get<ColorAdjustments>();

	public ChromaticAberration chromaticAberration => get<ChromaticAberration>();

	public Vignette vignette => get<Vignette>();

	public FilmGrain filmGrain => get<FilmGrain>();

	public MotionBlur motionBlur => get<MotionBlur>();

	public WaterRendering waterRendering => get<WaterRendering>();

	public VisualEnvironment visualEnvironment => get<VisualEnvironment>();

	public UnityEngine.Rendering.HighDefinition.Fog fog => get<UnityEngine.Rendering.HighDefinition.Fog>();

	public CloudLayer clouds => get<CloudLayer>();

	public HDRISky hdriSky => get<HDRISky>();

	public GradientSky gradientSky => get<GradientSky>();

	public PhysicallyBasedSky physicallyBasedSky => get<PhysicallyBasedSky>();

	private T get<T>() where T : VolumeComponent
	{
		if (!volume.profile.TryGet<T>(out var component))
		{
			Debug.LogError($"Room Editor Volume does not have an {typeof(T)} component in its profile! Adding a default one.");
			return volume.profile.Add<T>();
		}
		return component;
	}
}
