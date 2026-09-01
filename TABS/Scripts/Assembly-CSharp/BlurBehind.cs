using System;
using UnityEngine;

[AddComponentMenu("Image Effects/Blur Behind")]
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class BlurBehind : MonoBehaviour
{
	public enum Mode
	{
		Absolute = 0,
		Relative = 1
	}

	public enum Settings
	{
		Standard = 0,
		Smooth = 1,
		Manual = 2
	}

	private static RenderTexture storedTexture = null;

	private static int count = 0;

	private static Rect storedRect = new Rect(0f, 0f, 1f, 1f);

	public Shader blurShader;

	private Material blurMaterial;

	public Mode mode = Mode.Relative;

	public float radius = 2.5f;

	public Settings settings;

	public float downsample = 1f;

	public int iterations = 1;

	public Rect cropRect = new Rect(0f, 0f, 1f, 1f);

	public Rect pixelOffset = new Rect(0f, 0f, 0f, 0f);

	public static void SetViewport()
	{
		Rect rect = Camera.current.rect;
		if (rect != new Rect(0f, 0f, 1f, 1f))
		{
			Vector2 vector = ((!(Camera.current.targetTexture == null)) ? new Vector2(Camera.current.targetTexture.width, Camera.current.targetTexture.height) : new Vector2(Screen.width, Screen.height));
			rect.width = Mathf.Round(Mathf.Clamp01(rect.width + rect.x) * vector.x) / vector.x;
			rect.height = Mathf.Round(Mathf.Clamp01(rect.height + rect.y) * vector.y) / vector.y;
			rect.x = Mathf.Round(Mathf.Clamp01(rect.x) * vector.x) / vector.x;
			rect.y = Mathf.Round(Mathf.Clamp01(rect.y) * vector.y) / vector.y;
			rect.width -= rect.x;
			rect.height -= rect.y;
			Shader.SetGlobalVector("_BlurBehindRect", new Vector4((storedRect.x - rect.x) / rect.width, storedRect.y / rect.height + rect.y, storedRect.width / rect.width, storedRect.height / rect.height));
		}
	}

	public static void ResetViewport()
	{
		Shader.SetGlobalVector("_BlurBehindRect", new Vector4(storedRect.x, storedRect.y, storedRect.width, storedRect.height));
	}

	private void CheckSettings(int sourceSize)
	{
		if (radius < 0f)
		{
			radius = 0f;
		}
		if (downsample < 1f)
		{
			downsample = 1f;
		}
		if (iterations < 0)
		{
			iterations = 0;
		}
		if (settings == Settings.Manual)
		{
			return;
		}
		float num = ((settings == Settings.Standard) ? 36f : 6f);
		if (mode == Mode.Absolute)
		{
			if (radius > 0f)
			{
				if (radius < num)
				{
					iterations = 1;
				}
				else
				{
					iterations = Mathf.FloorToInt(Mathf.Log(radius, num)) + 1;
				}
				downsample = radius / Mathf.Pow(3f, iterations);
				if (downsample < 1f)
				{
					downsample = 1f;
				}
			}
			else
			{
				downsample = 1f;
				iterations = 0;
			}
		}
		else if (radius > 0f)
		{
			float num2 = radius / 100f * (float)sourceSize;
			if (num2 < num)
			{
				iterations = 1;
			}
			else
			{
				iterations = Mathf.FloorToInt(Mathf.Log(num2, num)) + 1;
			}
			downsample = (float)sourceSize / (num2 / Mathf.Pow(3f, iterations));
		}
		else
		{
			downsample = float.PositiveInfinity;
			iterations = 0;
		}
	}

	private void CheckOutput(int rtW, int rtH, RenderTextureFormat format)
	{
		if (storedTexture == null)
		{
			CreateOutput(rtW, rtH, format);
		}
		else if (storedTexture.width != rtW || storedTexture.height != rtH || storedTexture.format != format)
		{
			storedTexture.Release();
			UnityEngine.Object.DestroyImmediate(storedTexture);
			CreateOutput(rtW, rtH, format);
		}
		else
		{
			storedTexture.DiscardContents();
		}
	}

	private bool CheckResources()
	{
		if (blurMaterial == null)
		{
			if (!(blurShader != null))
			{
				Debug.Log("Blur Behind UI: Shader reference missing");
				return false;
			}
			if (!blurShader.isSupported)
			{
				Debug.Log("Blur Behind UI: Shader not supported");
				return false;
			}
			blurMaterial = new Material(blurShader);
			blurMaterial.hideFlags = HideFlags.DontSave;
		}
		return true;
	}

	private bool CheckSupport()
	{
		if (!SystemInfo.supportsImageEffects)
		{
			Debug.Log("Blur Behind UI: Image effects not supported");
			return false;
		}
		if (!SystemInfo.supportsRenderTextures)
		{
			Debug.Log("Blur Behind UI: Render textures not supported");
			return false;
		}
		return true;
	}

	private void CreateOutput(int width, int height, RenderTextureFormat format)
	{
		storedTexture = new RenderTexture(width, height, 0, format);
		storedTexture.filterMode = FilterMode.Bilinear;
		storedTexture.hideFlags = HideFlags.DontSave;
		Shader.SetGlobalTexture("_BlurBehindTex", storedTexture);
		Shader.EnableKeyword("BLUR_BEHIND_SET");
	}

	private RenderTexture CropSource(RenderTexture source)
	{
		Rect rect = new Rect(cropRect.x * (float)source.width + pixelOffset.x, cropRect.y * (float)source.height + pixelOffset.y, cropRect.width * (float)source.width + pixelOffset.width, cropRect.height * (float)source.height + pixelOffset.height);
		rect.width = Mathf.Clamp01(Mathf.Round(rect.width + rect.x) / (float)source.width);
		rect.height = Mathf.Clamp01(Mathf.Round(rect.height + rect.y) / (float)source.height);
		rect.x = Mathf.Clamp01(Mathf.Round(rect.x) / (float)source.width);
		rect.y = Mathf.Clamp01(Mathf.Round(rect.y) / (float)source.height);
		rect.width -= rect.x;
		rect.height -= rect.y;
		RenderTexture renderTexture;
		if (rect != new Rect(0f, 0f, 1f, 1f))
		{
			renderTexture = RenderTexture.GetTemporary(Mathf.RoundToInt(rect.width * (float)source.width), Mathf.RoundToInt(rect.height * (float)source.height), 0, source.format);
			blurMaterial.SetVector("_Parameter", new Vector4(rect.x, rect.y, rect.width, rect.height));
			Graphics.Blit(source, renderTexture, blurMaterial, 2);
			storedRect = rect;
		}
		else
		{
			renderTexture = source;
			storedRect = new Rect(0f, 0f, 1f, 1f);
		}
		Rect rect2 = Camera.current.rect;
		if (rect2 != new Rect(0f, 0f, 1f, 1f))
		{
			Vector2 vector = ((!(Camera.current.targetTexture == null)) ? new Vector2(Camera.current.targetTexture.width, Camera.current.targetTexture.height) : new Vector2(Screen.width, Screen.height));
			rect2.width = Mathf.Round(Mathf.Clamp01(rect2.width + rect2.x) * vector.x) / vector.x;
			rect2.height = Mathf.Round(Mathf.Clamp01(rect2.height + rect2.y) * vector.y) / vector.y;
			rect2.x = Mathf.Round(Mathf.Clamp01(rect2.x) * vector.x) / vector.x;
			rect2.y = Mathf.Round(Mathf.Clamp01(rect2.y) * vector.y) / vector.y;
			rect2.width -= rect2.x;
			rect2.height -= rect2.y;
			storedRect = new Rect(rect2.x + storedRect.x * rect2.width, rect2.y + storedRect.y * rect2.height, rect2.width * storedRect.width, rect2.height * storedRect.height);
		}
		Shader.SetGlobalVector("_BlurBehindRect", new Vector4(storedRect.x, storedRect.y, storedRect.width, storedRect.height));
		return renderTexture;
	}

	private void Downsample(RenderTexture source, RenderTexture dest)
	{
		int num = 0;
		if (source.width > source.height)
		{
			for (int num2 = source.width; num2 > dest.width; num2 >>= 1)
			{
				num++;
			}
		}
		else
		{
			for (int num3 = source.height; num3 > dest.height; num3 >>= 1)
			{
				num++;
			}
		}
		if (num > 1)
		{
			RenderTexture renderTexture = source;
			while (num > 2)
			{
				int num4 = renderTexture.width >> 2;
				if (num4 < 1)
				{
					num4 = 1;
				}
				int num5 = renderTexture.height >> 2;
				if (num5 < 1)
				{
					num5 = 1;
				}
				renderTexture.filterMode = FilterMode.Bilinear;
				RenderTexture temporary = RenderTexture.GetTemporary(num4, num5, 0, renderTexture.format);
				blurMaterial.SetVector("_Parameter", new Vector4(renderTexture.texelSize.x, renderTexture.texelSize.y, 0f - renderTexture.texelSize.x, 0f - renderTexture.texelSize.y));
				Graphics.Blit(renderTexture, temporary, blurMaterial, 1);
				if (renderTexture != source)
				{
					RenderTexture.ReleaseTemporary(renderTexture);
				}
				renderTexture = temporary;
				num -= 2;
			}
			if (num > 1)
			{
				blurMaterial.SetVector("_Parameter", new Vector4(renderTexture.texelSize.x, renderTexture.texelSize.y, 0f - renderTexture.texelSize.x, 0f - renderTexture.texelSize.y));
				Graphics.Blit(renderTexture, dest, blurMaterial, 1);
			}
			else
			{
				Graphics.Blit(renderTexture, dest);
			}
			if (renderTexture != source)
			{
				RenderTexture.ReleaseTemporary(renderTexture);
			}
		}
		else
		{
			Graphics.Blit(source, dest);
		}
	}

	private void OnDisable()
	{
		if ((bool)blurMaterial)
		{
			UnityEngine.Object.Destroy(blurMaterial);
			blurMaterial = null;
		}
		count--;
		if (count == 0 && (bool)storedTexture)
		{
			storedTexture.Release();
			UnityEngine.Object.Destroy(storedTexture);
			storedTexture = null;
			Shader.SetGlobalTexture("_BlurBehindTex", null);
			Shader.DisableKeyword("BLUR_BEHIND_SET");
		}
	}

	private void OnEnable()
	{
		count++;
	}

	private void OnPreRender()
	{
		SetViewport();
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!CheckSupport() || !CheckResources())
		{
			base.enabled = false;
			Graphics.Blit(source, destination);
			return;
		}
		RenderTexture renderTexture = CropSource(source);
		int num = ((renderTexture.width > renderTexture.height) ? renderTexture.width : renderTexture.height);
		CheckSettings(num);
		SetOutputSize(source, renderTexture, out var width, out var height);
		CheckOutput(width, height, renderTexture.format);
		Downsample(renderTexture, storedTexture);
		if (renderTexture != source)
		{
			RenderTexture.ReleaseTemporary(renderTexture);
		}
		if (iterations > 0 && radius > 0f)
		{
			RenderTexture temporary = RenderTexture.GetTemporary(width, height, 0, renderTexture.format);
			temporary.filterMode = FilterMode.Bilinear;
			for (int i = 0; i < iterations; i++)
			{
				float num2 = radius / 300f * Mathf.Pow(3f, i) / Mathf.Pow(3f, iterations - 1);
				if (mode == Mode.Absolute)
				{
					num2 *= 100f / (float)num;
				}
				else if (renderTexture != source)
				{
					num2 *= (float)((source.width > source.height) ? source.width : source.height) / (float)num;
				}
				float f = (float)i * ((float)Math.PI / 4f) / (float)iterations;
				Vector2 vector = new Vector2(Mathf.Sin(f), Mathf.Cos(f)) * num2;
				Vector2 vector2 = ((width > height) ? new Vector2(1f, 1f / (float)height * (float)width) : new Vector2(1f / (float)width * (float)height, 1f));
				Vector4 value = new Vector4(vector.x * vector2.x, vector.y * vector2.y, 0f, 0f);
				value.z = 0f - value.x;
				value.w = 0f - value.y;
				blurMaterial.SetVector("_Parameter", value);
				Graphics.Blit(storedTexture, temporary, blurMaterial, 0);
				storedTexture.DiscardContents();
				value = new Vector4(vector.y * vector2.x, (0f - vector.x) * vector2.y, 0f, 0f);
				value.z = 0f - value.x;
				value.w = 0f - value.y;
				blurMaterial.SetVector("_Parameter", value);
				Graphics.Blit(temporary, storedTexture, blurMaterial, 0);
				temporary.DiscardContents();
			}
			RenderTexture.ReleaseTemporary(temporary);
		}
		Graphics.Blit(source, destination);
	}

	private void SetOutputSize(RenderTexture source, RenderTexture croppedSource, out int width, out int height)
	{
		float num = ((mode == Mode.Absolute) ? (1f / downsample) : ((source.width > source.height) ? ((!((float)source.width > downsample)) ? 1f : (downsample / (float)source.width)) : ((!((float)source.height > downsample)) ? 1f : (downsample / (float)source.height))));
		width = Mathf.RoundToInt((float)croppedSource.width * num);
		height = Mathf.RoundToInt((float)croppedSource.height * num);
		if (width < 1)
		{
			width = 1;
		}
		else if (width > croppedSource.width)
		{
			width = croppedSource.width;
		}
		if (height < 1)
		{
			height = 1;
		}
		else if (height > croppedSource.height)
		{
			height = croppedSource.height;
		}
	}
}
