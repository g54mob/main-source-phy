using System;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Rendering;

public class LightPulseSync : MonoBehaviour
{
	public Renderer targetRenderer;

	public int materialIndex;

	public LensFlareComponentSRP lensFlare;

	public float phaseOffsetDegrees;

	public float intensityMultiplier = 1f;

	public float maxLightIntensity;

	public float maxFlareIntensity = 1f;

	private Light _light;

	private Material _material;

	private static readonly int PropEmmision;

	private static readonly int PropPulseFrequency;

	private void Awake()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		Light light = default(Light);
		_light = light;
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 32 Invalid \"Jump target not found in method: 0x1803FEF70\"");
	}

	private void OnEnable()
	{
		CacheMaterial();
	}

	private void OnDisable()
	{
		if (_light != null)
		{
			_light.intensity = 0f;
		}
		if (lensFlare != null)
		{
			LensFlareComponentSRP lensFlareComponentSRP = lensFlare;
			lensFlareComponentSRP.intensity = 0f;
		}
	}

	private void Update()
	{
		//IL_00a6: Invalid comparison between I4 and F4
		//IL_00b8: Expected F4, but got I4
		//IL_0255: Invalid comparison between F4 and I4
		if (_material != null)
		{
			float floatImpl = _material.GetFloatImpl(PropPulseFrequency);
			Color color = _material.GetColor(PropEmmision);
			float num = phaseOffsetDegrees * ((float)Math.PI / 180f);
			float time = Time.time;
			float num2 = time * floatImpl;
			float num3 = num2 + num;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E400");
			bool flag = !(0f < num3);
			float num4 = 0f;
			if (!flag)
			{
				num4 = num3;
			}
			float num5 = color.r * 0.2126f;
			object obj = default(object);
			float num6 = (float)obj * 0.7152f;
			float num7 = (float)obj * 0.0722f;
			float num8 = num6 + num5;
			float num9 = num8 + num7;
			float num10 = num9 * num4;
			float num11 = num10 * intensityMultiplier;
			if (maxLightIntensity > 0f && num11 > maxLightIntensity)
			{
				num11 = maxLightIntensity;
			}
			_light.intensity = num11;
			if (lensFlare != null)
			{
				LensFlareComponentSRP lensFlareComponentSRP = lensFlare;
				float intensity = num4 * maxFlareIntensity;
				lensFlareComponentSRP.intensity = intensity;
			}
		}
		else
		{
			_light.intensity = 0f;
			if (lensFlare != null)
			{
				LensFlareComponentSRP lensFlareComponentSRP2 = lensFlare;
				lensFlareComponentSRP2.intensity = 0f;
			}
		}
	}

	private void CacheMaterial()
	{
		//IL_0096: Expected O, but got I
		if (!(targetRenderer != null))
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9AD10");
		if (materialIndex >= 0)
		{
			int num = materialIndex;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v131 @ rax_v11+18]");
			if ((nint)num < (nint)0)
			{
				int num2 = materialIndex;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v131 @ rax_v11+20+v262 @ rax_v23 (System.Int32)*8]");
				_material = (Material)0;
				return;
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		object arg = default(object);
		string text = $"[LightPulseSync] materialIndex {arg} is out of range ";
		string arg2 = targetRenderer.name;
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		object arg3 = default(object);
		string text2 = $"for {arg2} ({arg3} material(s)).";
		string message = text + text2;
		Debug.LogWarning(message, this);
		_material = null;
	}

	static LightPulseSync()
	{
		int propEmmision = Shader.PropertyToID("_Emmision");
		PropEmmision = propEmmision;
		int propPulseFrequency = Shader.PropertyToID("_PulseFrequency");
		PropPulseFrequency = propPulseFrequency;
	}
}
