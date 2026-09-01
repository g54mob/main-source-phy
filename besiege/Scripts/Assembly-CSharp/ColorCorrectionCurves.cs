using System;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Color Adjustments/Color Correction (Curves, Saturation)")]
public class ColorCorrectionCurves : PostEffectsBase
{
	public AnimationCurve redChannel = new AnimationCurve();

	public AnimationCurve greenChannel = new AnimationCurve();

	public AnimationCurve blueChannel = new AnimationCurve();

	public bool useRedForAll;

	public bool useDepthCorrection;

	public AnimationCurve zCurve = new AnimationCurve();

	public AnimationCurve depthRedChannel = new AnimationCurve();

	public AnimationCurve depthGreenChannel = new AnimationCurve();

	public AnimationCurve depthBlueChannel = new AnimationCurve();

	private Material ccMaterial;

	private Material ccDepthMaterial;

	private Material selectiveCcMaterial;

	private Texture2D rgbChannelTex;

	private Texture2D rgbDepthChannelTex;

	private Texture2D zCurveTex;

	public float saturation = 1f;

	private float defSaturation = 1f;

	public bool respondToSettings;

	public bool selectiveCc;

	public Color selectiveFromColor = Color.white;

	public Color selectiveToColor = Color.white;

	public ColorCorrectionMode mode;

	public bool updateTextures = true;

	public Shader colorCorrectionCurvesShader;

	public Shader simpleColorCorrectionCurvesShader;

	public Shader colorCorrectionSelectiveShader;

	private bool updateTexturesOnStartup = true;

	protected void Awake()
	{
		if (respondToSettings && Application.isPlaying)
		{
			defSaturation = saturation;
			UpdateSaturation();
			ReferenceMaster.onSaturationChanged = (Action)Delegate.Combine(ReferenceMaster.onSaturationChanged, new Action(UpdateSaturation));
		}
	}

	protected void OnDestroy()
	{
		ReferenceMaster.onSaturationChanged = (Action)Delegate.Remove(ReferenceMaster.onSaturationChanged, new Action(UpdateSaturation));
	}

	protected void UpdateSaturation()
	{
		if (OptionsMaster.BesiegeConfig.Saturation != 100f)
		{
			base.enabled = true;
			saturation = defSaturation * OptionsMaster.BesiegeConfig.Saturation / 100f;
		}
		else if (base.enabled)
		{
			if (defSaturation != 1f)
			{
				saturation = defSaturation;
			}
			else
			{
				base.enabled = false;
			}
		}
	}

	protected override void Start()
	{
		base.Start();
		updateTexturesOnStartup = true;
	}

	protected override bool CheckResources()
	{
		CheckSupport(mode == ColorCorrectionMode.Advanced);
		ccMaterial = CheckShaderAndCreateMaterial(simpleColorCorrectionCurvesShader, ccMaterial);
		ccDepthMaterial = CheckShaderAndCreateMaterial(colorCorrectionCurvesShader, ccDepthMaterial);
		selectiveCcMaterial = CheckShaderAndCreateMaterial(colorCorrectionSelectiveShader, selectiveCcMaterial);
		if (!rgbChannelTex)
		{
			rgbChannelTex = new Texture2D(256, 4, TextureFormat.ARGB32, false, true);
		}
		if (!rgbDepthChannelTex)
		{
			rgbDepthChannelTex = new Texture2D(256, 4, TextureFormat.ARGB32, false, true);
		}
		if (!zCurveTex)
		{
			zCurveTex = new Texture2D(256, 1, TextureFormat.ARGB32, false, true);
		}
		rgbChannelTex.hideFlags = HideFlags.DontSave;
		rgbDepthChannelTex.hideFlags = HideFlags.DontSave;
		zCurveTex.hideFlags = HideFlags.DontSave;
		rgbChannelTex.wrapMode = TextureWrapMode.Clamp;
		rgbDepthChannelTex.wrapMode = TextureWrapMode.Clamp;
		zCurveTex.wrapMode = TextureWrapMode.Clamp;
		if (!isSupported)
		{
			ReportAutoDisable();
		}
		return isSupported;
	}

	public void UpdateParameters()
	{
		CheckResources();
		if (redChannel != null && greenChannel != null && blueChannel != null)
		{
			for (float num = 0f; num <= 1f; num += 0.003921569f)
			{
				float num2 = Mathf.Clamp(redChannel.Evaluate(num), 0f, 1f);
				float num3 = Mathf.Clamp(greenChannel.Evaluate(num), 0f, 1f);
				float num4 = Mathf.Clamp(blueChannel.Evaluate(num), 0f, 1f);
				Color color = new Color(num2, num2, num2);
				Color color2 = ((!useRedForAll) ? new Color(num3, num3, num3) : color);
				Color color3 = ((!useRedForAll) ? new Color(num4, num4, num4) : color);
				rgbChannelTex.SetPixel(Mathf.FloorToInt(num * 255f), 0, color);
				rgbChannelTex.SetPixel(Mathf.FloorToInt(num * 255f), 1, color2);
				rgbChannelTex.SetPixel(Mathf.FloorToInt(num * 255f), 2, color3);
				float num5 = Mathf.Clamp(zCurve.Evaluate(num), 0f, 1f);
				zCurveTex.SetPixel(Mathf.FloorToInt(num * 255f), 0, new Color(num5, num5, num5));
				num2 = Mathf.Clamp(depthRedChannel.Evaluate(num), 0f, 1f);
				num3 = Mathf.Clamp(depthGreenChannel.Evaluate(num), 0f, 1f);
				num4 = Mathf.Clamp(depthBlueChannel.Evaluate(num), 0f, 1f);
				color = new Color(num2, num2, num2);
				color2 = ((!useRedForAll) ? new Color(num3, num3, num3) : color);
				color3 = ((!useRedForAll) ? new Color(num4, num4, num4) : color);
				rgbDepthChannelTex.SetPixel(Mathf.FloorToInt(num * 255f), 0, color);
				rgbDepthChannelTex.SetPixel(Mathf.FloorToInt(num * 255f), 1, color2);
				rgbDepthChannelTex.SetPixel(Mathf.FloorToInt(num * 255f), 2, color3);
			}
			rgbChannelTex.Apply();
			rgbDepthChannelTex.Apply();
			zCurveTex.Apply();
		}
	}

	private void UpdateTextures()
	{
		UpdateParameters();
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!CheckResources())
		{
			Graphics.Blit(source, destination);
			return;
		}
		if (updateTexturesOnStartup)
		{
			UpdateParameters();
			updateTexturesOnStartup = false;
		}
		if (useDepthCorrection)
		{
			GetComponent<Camera>().depthTextureMode |= DepthTextureMode.Depth;
		}
		RenderTexture renderTexture = destination;
		if (selectiveCc)
		{
			renderTexture = RenderTexture.GetTemporary(source.width, source.height);
		}
		if (useDepthCorrection)
		{
			ccDepthMaterial.SetTexture("_RgbTex", rgbChannelTex);
			ccDepthMaterial.SetTexture("_ZCurve", zCurveTex);
			ccDepthMaterial.SetTexture("_RgbDepthTex", rgbDepthChannelTex);
			ccDepthMaterial.SetFloat("_Saturation", saturation);
			Graphics.Blit(source, renderTexture, ccDepthMaterial);
		}
		else
		{
			ccMaterial.SetTexture("_RgbTex", rgbChannelTex);
			ccMaterial.SetFloat("_Saturation", saturation);
			Graphics.Blit(source, renderTexture, ccMaterial);
		}
		if (selectiveCc)
		{
			selectiveCcMaterial.SetColor("selColor", selectiveFromColor);
			selectiveCcMaterial.SetColor("targetColor", selectiveToColor);
			Graphics.Blit(renderTexture, destination, selectiveCcMaterial);
			RenderTexture.ReleaseTemporary(renderTexture);
		}
	}
}
