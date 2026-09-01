using System;
using UnityEngine;

[ExecuteInEditMode]
public class FogVolume : MonoBehaviour
{
	private GameObject FogVolumeGameObject;

	[HideInInspector]
	public Material FogMaterial;

	public Shader FogMaterialShader;

	public Color InscatteringColor = Color.white;

	public Color FogColor = new Color(0.5f, 0.6f, 0.7f, 1f);

	public float Visibility = 5f;

	public float InscateringExponent = 15f;

	public float InscatteringIntensity = 2f;

	public float InscatteringStartDistance = 400f;

	public float InscatteringTransitionWideness = 1f;

	public float _3DNoiseScale = 300f;

	public float _3DNoiseStepSize = 50f;

	public float fogStartDistance = 40f;

	public float shaderMinMultiplier = 0.75f;

	public float shaderMaxMultiplier = 2.5f;

	public Texture3D _NoiseVolume;

	[Range(1f, 3f)]
	public int Quality = 1;

	[Range(0f, 10f)]
	public float NoiseIntensity = 1f;

	[Range(0f, 1f)]
	public float NoiseContrast;

	[SerializeField]
	protected Light Sun;

	[SerializeField]
	protected int DrawOrder;

	[SerializeField]
	protected bool EnableInscattering;

	[SerializeField]
	protected bool EnableNoise;

	public Vector4 Speed = new Vector4(0f, 0f, 0f, 0f);

	public Vector4 Stretch = new Vector4(0f, 0f, 0f, 0f);

	public float GetVisibility()
	{
		return Visibility;
	}

	private void OnEnable()
	{
		if (!FogMaterialShader)
		{
			FogMaterialShader = Shader.Find("Hidden/FogVolume");
		}
		if (!FogMaterial)
		{
			FogMaterial = new Material(FogMaterialShader);
			FogMaterial.name = "Fog Material";
			FogMaterial.hideFlags = HideFlags.HideAndDontSave;
		}
		FogVolumeGameObject = base.gameObject;
		FogVolumeGameObject.GetComponent<Renderer>().sharedMaterial = FogMaterial;
		ToggleKeyword();
		if (!StatMaster.isHeadless)
		{
			Camera.main.depthTextureMode |= DepthTextureMode.Depth;
		}
		Shader.SetGlobalColor("_FogVolumeColor", FogColor);
		Shader.SetGlobalColor("_FogInscatteringColor", InscatteringColor * InscatteringIntensity);
		Shader.SetGlobalFloat("_FogVolumeMin", fogStartDistance * shaderMinMultiplier);
		Shader.SetGlobalFloat("_FogVolumeMax", Visibility * shaderMaxMultiplier);
		Shader.SetGlobalVector("_FogLightDir", (!Sun) ? new Vector3(-0.5f, 0f, -0.5f).normalized : Sun.transform.forward.normalized);
	}

	public static void Wireframe(GameObject obj, bool Enable)
	{
	}

	private void Update()
	{
	}

	private void OnWillRenderObject()
	{
		FogMaterial.SetColor("_Color", FogColor);
		FogMaterial.SetColor("_InscatteringColor", InscatteringColor);
		FogMaterial.SetFloat("FogStartDistance", fogStartDistance);
		if ((bool)Sun)
		{
			FogMaterial.SetFloat("_InscatteringIntensity", InscatteringIntensity);
			FogMaterial.SetVector("L", -Sun.transform.forward);
			FogMaterial.SetFloat("_InscateringExponent", InscateringExponent);
			FogMaterial.SetFloat("InscatteringTransitionWideness", InscatteringTransitionWideness);
		}
		if (EnableNoise && (bool)_NoiseVolume)
		{
			Shader.SetGlobalTexture("_NoiseVolume", _NoiseVolume);
			FogMaterial.SetFloat("gain", NoiseIntensity);
			FogMaterial.SetFloat("threshold", NoiseContrast * 0.5f);
			FogMaterial.SetFloat("_3DNoiseScale", _3DNoiseScale * 0.001f);
			FogMaterial.SetFloat("_3DNoiseStepSize", _3DNoiseStepSize * 0.001f / (float)Quality);
			FogMaterial.SetVector("Speed", Speed);
			FogMaterial.SetVector("Stretch", new Vector4(1f, 1f, 1f, 1f) + Stretch * 0.01f);
		}
		FogMaterial.SetFloat("InscatteringStartDistance", InscatteringStartDistance);
		Vector3 localScale = FogVolumeGameObject.transform.localScale;
		float x = localScale.x;
		Math.Round(x, 2);
		base.transform.localScale = new Vector3(x, localScale.y, localScale.z);
		FogMaterial.SetVector("_BoxMin", localScale * -0.5f);
		FogMaterial.SetVector("_BoxMax", localScale * 0.5f);
		FogMaterial.SetFloat("_Visibility", Visibility);
		GetComponent<Renderer>().sortingOrder = DrawOrder;
	}

	private void ToggleKeyword()
	{
		if (!FogMaterialShader)
		{
			FogMaterialShader = Shader.Find("Hidden/FogVolume");
		}
		if (!FogMaterial)
		{
			FogMaterial = new Material(FogMaterialShader);
			FogMaterial.name = "Fog Material";
			FogMaterial.hideFlags = HideFlags.HideAndDontSave;
		}
		if (EnableNoise)
		{
			FogMaterial.EnableKeyword("_FOG_VOLUME_NOISE");
		}
		else
		{
			FogMaterial.DisableKeyword("_FOG_VOLUME_NOISE");
		}
		if (EnableInscattering && (bool)Sun)
		{
			FogMaterial.EnableKeyword("_FOG_VOLUME_INSCATTERING");
		}
		else
		{
			FogMaterial.DisableKeyword("_FOG_VOLUME_INSCATTERING");
		}
		switch (Quality)
		{
		case 1:
			FogMaterial.DisableKeyword("_MQ");
			FogMaterial.DisableKeyword("_HQ");
			break;
		case 2:
			FogMaterial.EnableKeyword("_MQ");
			FogMaterial.DisableKeyword("_HQ");
			break;
		case 3:
			FogMaterial.EnableKeyword("_HQ");
			FogMaterial.DisableKeyword("_MQ");
			break;
		}
	}
}
