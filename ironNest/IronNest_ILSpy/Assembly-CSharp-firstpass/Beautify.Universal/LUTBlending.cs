using Cpp2ILInjected;
using UnityEngine;

namespace Beautify.Universal;

public class LUTBlending : MonoBehaviour
{
	private static class ShaderParams
	{
		public static int LUT2;

		public static int Phase;

		static ShaderParams()
		{
			int lUT = Shader.PropertyToID("_LUT2");
			LUT2 = lUT;
			int phase = Shader.PropertyToID("_Phase");
			Phase = phase;
		}
	}

	public Texture2D LUT1;

	public Texture2D LUT2;

	public float LUT1Intensity = 1f;

	public float LUT2Intensity = 1f;

	public float phase;

	public Shader lerpShader;

	private float oldPhase = -1f;

	private RenderTexture rt;

	private Material lerpMat;

	private void OnEnable()
	{
		UpdateBeautifyLUT();
	}

	private void OnValidate()
	{
		oldPhase = -1f;
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 2 Invalid \"Jump target not found in method: 0x1803D52B0\"");
	}

	private void OnDestroy()
	{
		if (rt != null)
		{
			rt.Release();
		}
	}

	private void LateUpdate()
	{
		UpdateBeautifyLUT();
	}

	private unsafe void UpdateBeautifyLUT()
	{
		//IL_01e2: Invalid comparison between I4 and F4
		//IL_022f: Expected F4, but got Ref
		bool flag = oldPhase == phase;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001803D533Ah\"");
		if (!flag && LUT1 != null && LUT2 != null && lerpShader != null)
		{
			oldPhase = phase;
			if (rt == null)
			{
				int width = LUT1.width;
				int height = LUT1.height;
				RenderTextureFormat format = default(RenderTextureFormat);
				RenderTextureReadWrite readWrite = default(RenderTextureReadWrite);
				RenderTexture renderTexture = new RenderTexture(width, height, 0, format, readWrite);
				rt = renderTexture;
				rt.filterMode = FilterMode.Point;
			}
			if (lerpMat == null)
			{
				Material material = new Material(lerpShader);
				lerpMat = material;
			}
			lerpMat.SetTexture(ShaderParams.LUT2, LUT2);
			lerpMat.SetFloat(ShaderParams.Phase, phase);
			Graphics.Blit(LUT1, rt, lerpMat);
			Beautify settings = BeautifySettings.settings;
			object obj = default(object);
			settings.lut.Override((byte)(&obj) != 0);
			if (0f > phase || phase > 1f)
			{
			}
			Beautify settings2 = BeautifySettings.settings;
			settings2.lutIntensity.Override((nint)(&obj));
			Beautify settings3 = BeautifySettings.settings;
			settings3.lutTexture.Override(rt);
		}
	}
}
