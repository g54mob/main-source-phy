using UnityEngine;

[AddComponentMenu("Image Effects/Other/Antialiasing")]
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class AntialiasingAsPostEffect : PostEffectsBase
{
	public AAMode mode = AAMode.FXAA3Console;

	public bool showGeneratedNormals;

	public float offsetScale = 0.2f;

	public float blurRadius = 18f;

	public float edgeThresholdMin = 0.05f;

	public float edgeThreshold = 0.2f;

	public float edgeSharpness = 4f;

	public bool dlaaSharp;

	public Shader ssaaShader;

	private Material ssaa;

	public Shader dlaaShader;

	private Material dlaa;

	public Shader nfaaShader;

	private Material nfaa;

	public Shader shaderFXAAPreset2;

	private Material materialFXAAPreset2;

	public Shader shaderFXAAPreset3;

	private Material materialFXAAPreset3;

	public Shader shaderFXAAII;

	private Material materialFXAAII;

	public Shader shaderFXAAIII;

	private Material materialFXAAIII;

	public void SetMode(AAMode value)
	{
		mode = value;
		if (ssaaShader.isSupported)
		{
			base.enabled = true;
			return;
		}
		Debug.Log("Disable AA not supported: " + ssaaShader.isSupported);
		base.enabled = false;
	}

	public Material CurrentAAMaterial()
	{
		Material material = null;
		switch (mode)
		{
		case AAMode.FXAA3Console:
			return materialFXAAIII;
		case AAMode.FXAA2:
			return materialFXAAII;
		case AAMode.FXAA1PresetA:
			return materialFXAAPreset2;
		case AAMode.FXAA1PresetB:
			return materialFXAAPreset3;
		case AAMode.NFAA:
			return nfaa;
		case AAMode.SSAA:
			return ssaa;
		case AAMode.DLAA:
			return dlaa;
		default:
			return null;
		}
	}

	protected override bool CheckResources()
	{
		CheckSupport(false);
		bool flag = false;
		switch (mode)
		{
		case AAMode.FXAA3Console:
			materialFXAAIII = CreateMaterial(shaderFXAAIII, materialFXAAIII);
			flag = materialFXAAIII != null;
			break;
		case AAMode.FXAA2:
			materialFXAAII = CreateMaterial(shaderFXAAII, materialFXAAII);
			flag = materialFXAAII != null;
			break;
		case AAMode.FXAA1PresetA:
			materialFXAAPreset2 = CreateMaterial(shaderFXAAPreset2, materialFXAAPreset2);
			flag = materialFXAAPreset2 != null;
			break;
		case AAMode.FXAA1PresetB:
			materialFXAAPreset3 = CreateMaterial(shaderFXAAPreset3, materialFXAAPreset3);
			flag = materialFXAAPreset3 != null;
			break;
		case AAMode.NFAA:
			nfaa = CreateMaterial(nfaaShader, nfaa);
			flag = nfaa != null;
			break;
		case AAMode.SSAA:
			ssaa = CreateMaterial(ssaaShader, ssaa);
			flag = ssaa != null;
			break;
		case AAMode.DLAA:
			dlaa = CreateMaterial(dlaaShader, dlaa);
			flag = dlaa != null;
			break;
		}
		if (!flag || !ssaaShader.isSupported)
		{
			NotSupported();
			ReportAutoDisable();
			base.enabled = false;
			Debug.Log("Disable AA not supported: " + flag + " / " + ssaaShader.isSupported);
		}
		return flag;
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!CheckResources())
		{
			Graphics.Blit(source, destination);
			return;
		}
		switch (mode)
		{
		case AAMode.FXAA3Console:
			materialFXAAIII.SetFloat("_EdgeThresholdMin", edgeThresholdMin);
			materialFXAAIII.SetFloat("_EdgeThreshold", edgeThreshold);
			materialFXAAIII.SetFloat("_EdgeSharpness", edgeSharpness);
			Graphics.Blit(source, destination, materialFXAAIII);
			break;
		case AAMode.FXAA1PresetB:
			Graphics.Blit(source, destination, materialFXAAPreset3);
			break;
		case AAMode.FXAA1PresetA:
			source.anisoLevel = 4;
			Graphics.Blit(source, destination, materialFXAAPreset2);
			source.anisoLevel = 0;
			break;
		case AAMode.FXAA2:
			Graphics.Blit(source, destination, materialFXAAII);
			break;
		case AAMode.SSAA:
			Graphics.Blit(source, destination, ssaa);
			break;
		case AAMode.DLAA:
		{
			source.anisoLevel = 0;
			RenderTexture temporary = RenderTexture.GetTemporary(source.width, source.height);
			Graphics.Blit(source, temporary, dlaa, 0);
			Graphics.Blit(temporary, destination, dlaa, (!dlaaSharp) ? 1 : 2);
			RenderTexture.ReleaseTemporary(temporary);
			break;
		}
		case AAMode.NFAA:
			source.anisoLevel = 0;
			nfaa.SetFloat("_OffsetScale", offsetScale);
			nfaa.SetFloat("_BlurRadius", blurRadius);
			Graphics.Blit(source, destination, nfaa, showGeneratedNormals ? 1 : 0);
			break;
		default:
			Graphics.Blit(source, destination);
			break;
		}
	}
}
