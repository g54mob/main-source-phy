using UnityEngine;

[AddComponentMenu("Image Effects/Blur/Blur (Optimized)")]
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class Blur : PostEffectsBase
{
	public enum BlurType
	{
		StandardGauss = 0,
		SgxGauss = 1
	}

	[Range(0f, 2f)]
	public int downsample = 1;

	[Range(0f, 10f)]
	public float blurSize = 3f;

	[Range(1f, 4f)]
	public int blurIterations = 2;

	public BlurType blurType;

	public Shader blurShader;

	private Material blurMaterial;

	protected override bool CheckResources()
	{
		CheckSupport(false);
		blurMaterial = CheckShaderAndCreateMaterial(blurShader, blurMaterial);
		if (!isSupported)
		{
			ReportAutoDisable();
		}
		return isSupported;
	}

	private void OnDisable()
	{
		if ((bool)blurMaterial)
		{
			Object.DestroyImmediate(blurMaterial);
		}
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!CheckResources())
		{
			Graphics.Blit(source, destination);
			return;
		}
		float num = Screen.height;
		float num2 = blurSize * (num / 1080f);
		int num3 = ((!(num > 1080f)) ? downsample : (downsample + 1));
		float num4 = 1f / (1f * (float)(1 << num3));
		blurMaterial.SetVector("_Parameter", new Vector4(num2 * num4, (0f - num2) * num4, 0f, 0f));
		source.filterMode = FilterMode.Bilinear;
		int width = source.width >> num3;
		int height = source.height >> num3;
		RenderTexture renderTexture = RenderTexture.GetTemporary(width, height, 0, source.format);
		renderTexture.filterMode = FilterMode.Bilinear;
		Graphics.Blit(source, renderTexture, blurMaterial, 0);
		int num5 = ((blurType != BlurType.StandardGauss) ? 2 : 0);
		for (int i = 0; i < blurIterations; i++)
		{
			float num6 = (float)i * 1f;
			blurMaterial.SetVector("_Parameter", new Vector4(num2 * num4 + num6, (0f - num2) * num4 - num6, 0f, 0f));
			RenderTexture temporary = RenderTexture.GetTemporary(width, height, 0, source.format);
			temporary.filterMode = FilterMode.Bilinear;
			Graphics.Blit(renderTexture, temporary, blurMaterial, 1 + num5);
			RenderTexture.ReleaseTemporary(renderTexture);
			renderTexture = temporary;
			temporary = RenderTexture.GetTemporary(width, height, 0, source.format);
			temporary.filterMode = FilterMode.Bilinear;
			Graphics.Blit(renderTexture, temporary, blurMaterial, 2 + num5);
			RenderTexture.ReleaseTemporary(renderTexture);
			renderTexture = temporary;
		}
		Graphics.Blit(renderTexture, destination);
		RenderTexture.ReleaseTemporary(renderTexture);
	}
}
