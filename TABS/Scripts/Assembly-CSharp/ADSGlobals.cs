using System.Collections.Generic;
using UnityEngine;

[HelpURL("https://docs.google.com/document/d/13vul0zDF478he8hhteKjnxoLYgfW47G0Z9TSox21_J0/edit#heading=h.rp8ji698m9wz")]
[DisallowMultipleComponent]
[ExecuteInEditMode]
[RequireComponent(typeof(MeshRenderer))]
public class ADSGlobals : MonoBehaviour
{
	public enum DebugEnum
	{
		off = -1,
		vertexColorR = 1,
		vertexColorG = 2,
		vertexColorB = 3,
		vertexAlpha = 4,
		motionMask = 11,
		motionNoise = 12,
		grassTint = 21,
		grassSize = 22
	}

	public enum GrassTintModeEnum
	{
		texture = 0,
		colors = 1
	}

	public DebugEnum debug = DebugEnum.off;

	[Space(20f)]
	public float globalAmplitude = 0.5f;

	public float globalSpeed = 4f;

	public float globalScale = 0.5f;

	[Space(20f)]
	public Texture2D noiseTexture;

	public float noiseContrast = 1f;

	public float noiseSpeed = 1f;

	public float noiseScale = 1f;

	[Space(20f)]
	public GrassTintModeEnum grassTintMode = GrassTintModeEnum.colors;

	public Texture2D grassTintTexture;

	public float grassTintIntensity = 1f;

	public Color grassTintColorOne = Color.white;

	public Color grassTintColorTwo = Color.white;

	public Vector4 grassTintScaleOffset = new Vector4(1f, 1f, 0f, 0f);

	[Space(20f)]
	public Texture2D grassSizeTexture;

	public float grassSizeMin;

	public float grassSizeMax = 1f;

	public Vector4 grassSizeScaleOffset = new Vector4(1f, 1f, 0f, 0f);

	[Space(20f)]
	public List<Mesh> ADSObjects = new List<Mesh>();

	private bool somethingChanged;

	private float old_globalAmplitude;

	private float old_globalSpeed;

	private float old_globalScale;

	private Vector3 old_globalDirection;

	private Texture2D old_noiseTexture;

	private float old_noiseContrast;

	private float old_noiseSpeed;

	private float old_noiseScale;

	private GrassTintModeEnum old_grassTintMode;

	private Texture2D old_grassTintTexture;

	private float old_grassTintIntensity;

	private Color old_grassTintColorOne;

	private Color old_grassTintColorTwo;

	private Vector4 old_grassTintScaleOffset;

	private Texture2D old_grassSizeTexture;

	private float old_grassSizeMin;

	private float old_grassSizeMax;

	private Vector4 old_grassSizeScaleOffset;

	private Shader debugShader;

	private bool debugShader_ON;

	private void Awake()
	{
		base.gameObject.name = "ADS Globals";
		ADSObjects = new List<Mesh>();
		SetGlobalShaderProperties();
		if (Application.isPlaying)
		{
			base.gameObject.GetComponent<MeshRenderer>().enabled = false;
		}
		else
		{
			base.gameObject.GetComponent<MeshRenderer>().enabled = true;
		}
	}

	private void SetGlobalShaderProperties()
	{
		Shader.SetGlobalVector("ADS_GlobalDirection", base.gameObject.transform.forward);
		Shader.SetGlobalFloat("ADS_GlobalAmplitude", globalAmplitude);
		Shader.SetGlobalFloat("ADS_GlobalSpeed", globalSpeed);
		Shader.SetGlobalFloat("ADS_GlobalScale", globalScale);
		if (noiseTexture == null || noiseContrast <= 0f)
		{
			Shader.SetGlobalFloat("ADS_NoiseTex_ON", 0f);
		}
		else
		{
			Shader.SetGlobalFloat("ADS_NoiseTex_ON", 1f);
			Shader.SetGlobalTexture("ADS_NoiseTex", noiseTexture);
			Shader.SetGlobalFloat("ADS_NoiseContrast", noiseContrast);
			Shader.SetGlobalFloat("ADS_NoiseSpeed", noiseSpeed * 0.1f);
			Shader.SetGlobalFloat("ADS_NoiseScale", noiseScale * 0.1f);
		}
		if (grassTintTexture == null || grassTintIntensity <= 0f || (grassTintColorOne == Color.white && grassTintColorTwo == Color.white))
		{
			Shader.SetGlobalFloat("ADS_GrassTintTex_ON", 0f);
		}
		else
		{
			if (grassTintMode == GrassTintModeEnum.texture)
			{
				Shader.SetGlobalFloat("ADS_GrassTintModeColors", 0f);
			}
			else
			{
				Shader.SetGlobalFloat("ADS_GrassTintModeColors", 1f);
			}
			Shader.SetGlobalFloat("ADS_GrassTintTex_ON", 1f);
			Shader.SetGlobalTexture("ADS_GrassTintTex", grassTintTexture);
			Shader.SetGlobalFloat("ADS_GrassTintIntensity", grassTintIntensity);
			Shader.SetGlobalColor("ADS_GrassTintColorOne", grassTintColorOne);
			Shader.SetGlobalColor("ADS_GrassTintColorTwo", grassTintColorTwo);
			Shader.SetGlobalVector("ADS_GrassTintScaleOffset", grassTintScaleOffset);
		}
		if (grassSizeTexture == null)
		{
			Shader.SetGlobalFloat("ADS_GrassSizeTex_ON", 0f);
			return;
		}
		Shader.SetGlobalFloat("ADS_GrassSizeTex_ON", 1f);
		Shader.SetGlobalTexture("ADS_GrassSizeTex", grassSizeTexture);
		Shader.SetGlobalFloat("ADS_GrassSizeMin", grassSizeMin - 1f);
		Shader.SetGlobalFloat("ADS_GrassSizeMax", grassSizeMax - 1f);
		Shader.SetGlobalVector("ADS_GrassSizeScaleOffset", grassSizeScaleOffset);
	}

	private void CheckSomethingChanged()
	{
		somethingChanged |= old_globalAmplitude != globalAmplitude;
		somethingChanged |= old_globalSpeed != globalSpeed;
		somethingChanged |= old_globalScale != globalScale;
		somethingChanged |= old_globalDirection != base.gameObject.transform.forward;
		somethingChanged |= old_noiseTexture != noiseTexture;
		somethingChanged |= old_noiseContrast != noiseContrast;
		somethingChanged |= old_noiseSpeed != noiseSpeed;
		somethingChanged |= old_noiseScale != noiseScale;
		somethingChanged |= old_grassTintMode != grassTintMode;
		somethingChanged |= old_grassTintTexture != grassTintTexture;
		somethingChanged |= old_grassTintIntensity != grassTintIntensity;
		somethingChanged |= old_grassTintColorOne != grassTintColorOne;
		somethingChanged |= old_grassTintColorTwo != grassTintColorTwo;
		somethingChanged |= old_grassTintScaleOffset != grassTintScaleOffset;
		somethingChanged |= old_grassSizeTexture != grassSizeTexture;
		somethingChanged |= old_grassSizeMin != grassSizeMin;
		somethingChanged |= old_grassSizeMax != grassSizeMax;
		somethingChanged |= old_grassSizeScaleOffset != grassSizeScaleOffset;
	}

	private void UpdateSomethingChanged()
	{
		somethingChanged = false;
		old_globalAmplitude = globalAmplitude;
		old_globalSpeed = globalSpeed;
		old_globalScale = globalScale;
		old_globalDirection = base.gameObject.transform.forward;
		old_noiseTexture = noiseTexture;
		old_noiseContrast = noiseContrast;
		old_noiseSpeed = noiseSpeed;
		old_noiseScale = noiseScale;
		old_grassTintMode = grassTintMode;
		old_grassTintTexture = grassTintTexture;
		old_grassTintIntensity = grassTintIntensity;
		old_grassTintColorOne = grassTintColorOne;
		old_grassTintColorTwo = grassTintColorTwo;
		old_grassTintScaleOffset = grassTintScaleOffset;
		old_grassSizeTexture = grassSizeTexture;
		old_grassSizeMin = grassSizeMin;
		old_grassSizeMax = grassSizeMax;
		old_grassSizeScaleOffset = grassSizeScaleOffset;
	}
}
