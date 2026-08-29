using UnityEngine;

public class GlitchImageEffect : MonoBehaviour
{
	public enum GlitchType
	{
		Type1 = 0,
		Type2 = 1,
		Type3 = 2,
		Type4 = 3
	}

	public GlitchType type = GlitchType.Type2;

	[Range(0f, 1f)]
	public float blend = 1f;

	[Header("Parameters of Type1")]
	[Range(0f, 10f)]
	public float frequency = 1f;

	[Range(0f, 500f)]
	public float interference = 130f;

	[Range(0f, 5f)]
	public float noise = 0.15f;

	[Range(0f, 20f)]
	public float scanLine = 1f;

	[Range(0f, 1f)]
	public float colored = 0.25f;

	[Header("Parameters of Type3")]
	[Range(0f, 30f)]
	public float intensityType3 = 10f;

	[Header("Parameters of Type4")]
	[Range(100f, 500f)]
	public float lines = 240f;

	[Range(1f, 6f)]
	public float scanSpeed = 2f;

	[Range(0.1f, 0.9f)]
	public float linesThreshold = 0.7f;

	[Range(0f, 0.8f)]
	public float exposure = 0.3f;

	private Shader shader;

	private Material mtrl;

	private Texture2D noiseTex;

	private void Awake()
	{
		shader = Shader.Find("Hidden/GlitchImageEffect");
		if (!shader.isSupported)
		{
			base.enabled = false;
			return;
		}
		mtrl = new Material(shader);
		noiseTex = Resources.Load<Texture2D>("GlitchNoiseTex");
	}

	private void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		if (mtrl == null || mtrl.shader == null || !mtrl.shader.isSupported)
		{
			base.enabled = false;
			return;
		}
		mtrl.SetFloat("_Blend", blend);
		mtrl.SetFloat("_Frequency", frequency);
		mtrl.SetFloat("_Interference", interference);
		mtrl.SetFloat("_Noise", noise);
		mtrl.SetFloat("_ScanLine", scanLine);
		mtrl.SetFloat("_Colored", colored);
		mtrl.SetTexture("_NoiseTex", noiseTex);
		mtrl.SetFloat("_IntensityType3", intensityType3);
		mtrl.SetFloat("_Lines", lines);
		mtrl.SetFloat("_ScanSpeed", scanSpeed);
		mtrl.SetFloat("_LinesThreshold", linesThreshold);
		mtrl.SetFloat("_Exposure", exposure);
		Graphics.Blit(src, dest, mtrl, (int)type);
	}

	private void OnDestroy()
	{
		shader = null;
		if (mtrl != null)
		{
			Object.DestroyImmediate(mtrl);
			mtrl = null;
		}
		if (noiseTex != null)
		{
			Resources.UnloadAsset(noiseTex);
			noiseTex = null;
		}
	}
}
