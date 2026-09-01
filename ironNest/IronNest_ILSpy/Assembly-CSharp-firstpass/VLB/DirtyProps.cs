namespace VLB;

public enum DirtyProps
{
	None = 0,
	Intensity = 2,
	HDRPExposureWeight = 4,
	ColorMode = 8,
	Color = 16,
	BlendingMode = 32,
	Cone = 64,
	SideSoftness = 128,
	Attenuation = 256,
	Dimensions = 512,
	RaymarchingQuality = 1024,
	Jittering = 2048,
	NoiseMode = 4096,
	NoiseIntensity = 8192,
	NoiseVelocityAndScale = 16384,
	CookieProps = 32768,
	ShadowProps = 65536,
	AllWithoutMaterialChange = 125142,
	OnlyMaterialChangeOnly = 5928,
	All = 131070
}
