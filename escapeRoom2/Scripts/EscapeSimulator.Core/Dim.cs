using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

[Serializable]
[VolumeComponentMenu("Post-processing/Custom/Dim")]
public sealed class Dim : CustomPostProcessVolumeComponent, IPostProcessComponent
{
	[Tooltip("Controls the intensity of the effect.")]
	public ClampedFloatParameter intensity = new ClampedFloatParameter(0f, 0f, 1f);

	private Material m_Material;

	private const string kShaderName = "Hidden/Shader/Dim";

	public override CustomPostProcessInjectionPoint injectionPoint => CustomPostProcessInjectionPoint.AfterPostProcess;

	public bool IsActive()
	{
		if (m_Material != null)
		{
			return intensity.value > 0f;
		}
		return false;
	}

	public override void Setup()
	{
		if (Shader.Find("Hidden/Shader/Dim") != null)
		{
			m_Material = new Material(Shader.Find("Hidden/Shader/Dim"));
		}
		else
		{
			Debug.LogError("Unable to find shader 'Hidden/Shader/Dim'. Post Process Volume Dim is unable to load. To fix this, please edit the 'kShaderName' constant in Dim.cs or change the name of your custom post process shader.");
		}
	}

	public override void Render(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destination)
	{
		if (!(m_Material == null))
		{
			m_Material.SetFloat("_Intensity", intensity.value);
			m_Material.SetTexture("_MainTex", source);
			HDUtils.DrawFullScreen(cmd, m_Material, destination);
		}
	}

	public override void Cleanup()
	{
		CoreUtils.Destroy(m_Material);
	}
}
