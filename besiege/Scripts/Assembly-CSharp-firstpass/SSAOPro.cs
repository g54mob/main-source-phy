using UnityEngine;

[AddComponentMenu("Image Effects/SSAO Pro")]
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class SSAOPro : MonoBehaviour
{
	public enum BlurMode
	{
		None = 0,
		Gaussian = 1,
		Bilateral = 2,
		HighQualityBilateral = 3
	}

	public enum SampleCount
	{
		VeryLow = 0,
		Low = 1,
		Medium = 2,
		High = 3,
		Ultra = 4
	}

	public enum AOMode
	{
		V11 = 0,
		V12 = 1
	}

	public AOMode Mode = AOMode.V12;

	public Texture2D NoiseTexture;

	public readonly bool UseHighPrecisionDepthMap;

	public SampleCount Samples = SampleCount.Medium;

	[Range(1f, 4f)]
	public int Downsampling = 1;

	[Range(0.01f, 1.25f)]
	public float Radius = 0.125f;

	[Range(0f, 16f)]
	public float Intensity = 2f;

	[Range(0f, 10f)]
	public float Distance = 1f;

	[Range(0f, 1f)]
	public float Bias = 0.1f;

	[Range(0f, 1f)]
	public float LumContribution = 0.5f;

	public Color OcclusionColor = Color.black;

	public float CutoffDistance = 150f;

	public float CutoffFalloff = 50f;

	public BlurMode Blur;

	public bool BlurDownsampling;

	[Range(1f, 4f)]
	public int BlurPasses = 1;

	[Range(0.05f, 1f)]
	public float BlurBilateralThreshold = 0.1f;

	public bool DebugAO;

	protected Shader m_ShaderSSAO_v1;

	protected Shader m_ShaderSSAO_v2;

	protected Shader m_ShaderHighPrecisionDepth;

	protected Material m_Material_v1;

	protected Material m_Material_v2;

	protected Camera m_Camera;

	protected Camera m_RWSCamera;

	protected RenderTextureFormat m_RTFormat = RenderTextureFormat.RFloat;

	private bool started;

	private float near = 0.3f;

	private string[] keywords = new string[2];

	public Material Material
	{
		get
		{
			if (Mode == AOMode.V11)
			{
				if (m_Material_v1 == null)
				{
					m_Material_v1 = new Material(ShaderSSAO);
					m_Material_v1.hideFlags = HideFlags.HideAndDontSave;
				}
				return m_Material_v1;
			}
			if (m_Material_v2 == null)
			{
				m_Material_v2 = new Material(ShaderSSAO);
				m_Material_v2.hideFlags = HideFlags.HideAndDontSave;
			}
			return m_Material_v2;
		}
	}

	public Shader ShaderSSAO
	{
		get
		{
			if (Mode == AOMode.V11)
			{
				if (m_ShaderSSAO_v1 == null)
				{
					m_ShaderSSAO_v1 = Shader.Find("Hidden/SSAO Pro V1");
				}
				return m_ShaderSSAO_v1;
			}
			if (m_ShaderSSAO_v2 == null)
			{
				m_ShaderSSAO_v2 = Shader.Find("Hidden/SSAO Pro V2");
			}
			return m_ShaderSSAO_v2;
		}
	}

	private void Start()
	{
		started = true;
		if (!SystemInfo.supportsImageEffects)
		{
			Debug.LogWarning("Image Effects are not supported on this platform.");
			base.enabled = false;
		}
		else if (!SystemInfo.supportsRenderTextures)
		{
			Debug.LogWarning("RenderTextures are not supported on this platform.");
			base.enabled = false;
		}
		else if (ShaderSSAO != null && !ShaderSSAO.isSupported)
		{
			Debug.LogWarning("Unsupported shader (SSAO).");
			base.enabled = false;
		}
	}

	private void OnEnable()
	{
		m_Camera = GetComponent<Camera>();
		if (started)
		{
			Start();
		}
	}

	private void OnDestroy()
	{
		if (m_Material_v1 != null)
		{
			Object.DestroyImmediate(m_Material_v1);
		}
		if (m_Material_v2 != null)
		{
			Object.DestroyImmediate(m_Material_v2);
		}
		if (m_RWSCamera != null)
		{
			Object.DestroyImmediate(m_RWSCamera.gameObject);
		}
	}

	private void OnPreRender()
	{
		near = m_Camera.nearClipPlane;
		m_Camera.nearClipPlane = 0.3f;
	}

	private void OnPostRender()
	{
		m_Camera.nearClipPlane = near;
	}

	[ImageEffectOpaque]
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (ShaderSSAO == null)
		{
			Graphics.Blit(source, destination);
			return;
		}
		int pass = SetShaderStates();
		if (Mode == AOMode.V11)
		{
			Material.SetMatrix("_InverseViewProject", m_Camera.projectionMatrix.inverse);
		}
		else
		{
			Material.SetMatrix("_InverseViewProject", (m_Camera.projectionMatrix * m_Camera.worldToCameraMatrix).inverse);
			Material.SetMatrix("_CameraModelView", m_Camera.cameraToWorldMatrix);
		}
		Material.SetTexture("_NoiseTex", NoiseTexture);
		Material.SetVector("_Params1", new Vector4((!(NoiseTexture == null)) ? ((float)NoiseTexture.width) : 0f, Radius, Intensity, Distance));
		Material.SetVector("_Params2", new Vector4(Bias, LumContribution, CutoffDistance, CutoffFalloff));
		Material.SetColor("_OcclusionColor", OcclusionColor);
		if (Blur == BlurMode.None)
		{
			RenderTexture temporary = RenderTexture.GetTemporary(source.width / Downsampling, source.height / Downsampling, 0, RenderTextureFormat.ARGB32);
			Graphics.Blit(temporary, temporary, Material, 0);
			if (DebugAO)
			{
				Graphics.Blit(source, temporary, Material, pass);
				Graphics.Blit(temporary, destination);
				RenderTexture.ReleaseTemporary(temporary);
			}
			else
			{
				Graphics.Blit(source, temporary, Material, pass);
				Material.SetTexture("_SSAOTex", temporary);
				Graphics.Blit(source, destination, Material, 8);
				RenderTexture.ReleaseTemporary(temporary);
			}
			return;
		}
		int pass2 = 5;
		if (Blur == BlurMode.Bilateral)
		{
			pass2 = 6;
		}
		else if (Blur == BlurMode.HighQualityBilateral)
		{
			pass2 = 7;
		}
		int num = ((!BlurDownsampling) ? 1 : Downsampling);
		RenderTexture temporary2 = RenderTexture.GetTemporary(source.width / num, source.height / num, 0, RenderTextureFormat.ARGB32);
		RenderTexture temporary3 = RenderTexture.GetTemporary(source.width / Downsampling, source.height / Downsampling, 0, RenderTextureFormat.ARGB32);
		Graphics.Blit(temporary2, temporary2, Material, 0);
		Graphics.Blit(source, temporary2, Material, pass);
		if (Blur == BlurMode.HighQualityBilateral)
		{
			Material.SetFloat("_BilateralThreshold", BlurBilateralThreshold / 10000f);
		}
		for (int i = 0; i < BlurPasses; i++)
		{
			Material.SetVector("_Direction", new Vector2(1f / (float)source.width, 0f));
			Graphics.Blit(temporary2, temporary3, Material, pass2);
			Material.SetVector("_Direction", new Vector2(0f, 1f / (float)source.height));
			Graphics.Blit(temporary3, temporary2, Material, pass2);
		}
		if (!DebugAO)
		{
			Material.SetTexture("_SSAOTex", temporary2);
			Graphics.Blit(source, destination, Material, 8);
		}
		else
		{
			Graphics.Blit(temporary2, destination);
		}
		RenderTexture.ReleaseTemporary(temporary2);
		RenderTexture.ReleaseTemporary(temporary3);
	}

	private int SetShaderStates()
	{
		m_Camera.depthTextureMode |= DepthTextureMode.Depth;
		m_Camera.depthTextureMode |= DepthTextureMode.DepthNormals;
		keywords[0] = ((Samples == SampleCount.Low) ? "SAMPLES_LOW" : ((Samples == SampleCount.Medium) ? "SAMPLES_MEDIUM" : ((Samples == SampleCount.High) ? "SAMPLES_HIGH" : ((Samples != SampleCount.Ultra) ? "SAMPLES_VERY_LOW" : "SAMPLES_ULTRA"))));
		keywords[1] = "HIGH_PRECISION_DEPTHMAP_OFF";
		Material.shaderKeywords = keywords;
		int num = 0;
		if (NoiseTexture != null)
		{
			num = 1;
		}
		if (LumContribution >= 0.001f)
		{
			num += 2;
		}
		return 1 + num;
	}
}
