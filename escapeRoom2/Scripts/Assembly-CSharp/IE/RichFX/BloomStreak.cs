using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace IE.RichFX
{
	[Serializable]
	[VolumeComponentMenu("Rich FX/Others/Bloom Streak")]
	public sealed class BloomStreak : CustomPostProcessVolumeComponent, IPostProcessComponent
	{
		private static class ShaderIDs
		{
			internal static readonly int Color = Shader.PropertyToID("_Color");

			internal static readonly int HighTexture = Shader.PropertyToID("_HighTexture");

			internal static readonly int InputTexture = Shader.PropertyToID("_InputTexture");

			internal static readonly int Intensity = Shader.PropertyToID("_Intensity");

			internal static readonly int SourceTexture = Shader.PropertyToID("_SourceTexture");

			internal static readonly int Stretch = Shader.PropertyToID("_Stretch");

			internal static readonly int Threshold = Shader.PropertyToID("_Threshold");
		}

		public ClampedFloatParameter threshold = new ClampedFloatParameter(1f, 0f, 5f);

		public ClampedFloatParameter stretch = new ClampedFloatParameter(0.75f, 0f, 1f);

		public ClampedFloatParameter intensity = new ClampedFloatParameter(0f, 0f, 1f);

		public ColorParameter tint = new ColorParameter(new Color(0.55f, 0.55f, 1f), hdr: false, showAlpha: false, showEyeDropper: true);

		public ClampedFloatParameter hueShift = new ClampedFloatParameter(0f, -1f, 1f);

		private Material _material;

		private MaterialPropertyBlock _prop;

		private Dictionary<int, StreakPyramid> _pyramids;

		public override CustomPostProcessInjectionPoint injectionPoint => CustomPostProcessInjectionPoint.BeforePostProcess;

		private StreakPyramid GetPyramid(HDCamera camera)
		{
			int instanceID = camera.camera.GetInstanceID();
			if (!_pyramids.TryGetValue(instanceID, out var value))
			{
				value = (_pyramids[instanceID] = new StreakPyramid(camera));
			}
			else if (!value.CheckSize(camera))
			{
				value.Reallocate(camera);
			}
			return value;
		}

		public bool IsActive()
		{
			if (_material != null)
			{
				return intensity.value > 0f;
			}
			return false;
		}

		public override void Setup()
		{
			_material = CoreUtils.CreateEngineMaterial("Hidden/InanEvin/RichFX/BloomStreak");
			_prop = new MaterialPropertyBlock();
			_pyramids = new Dictionary<int, StreakPyramid>();
		}

		public override void Render(CommandBuffer cmd, HDCamera camera, RTHandle srcRT, RTHandle destRT)
		{
			StreakPyramid pyramid = GetPyramid(camera);
			_material.SetFloat("_Threshold", threshold.value);
			_material.SetFloat("_Stretch", stretch.value);
			_material.SetFloat("_Intensity", intensity.value);
			_material.SetColor("_Color", tint.value);
			_material.SetFloat("_Hue", hueShift.value);
			_material.SetTexture("_SourceTexture", srcRT);
			HDUtils.DrawFullScreen(cmd, _material, pyramid[0].down, _prop);
			int i;
			for (i = 1; i < 16 && pyramid[i].down != null; i++)
			{
				_prop.SetTexture(ShaderIDs.InputTexture, pyramid[i - 1].down);
				HDUtils.DrawFullScreen(cmd, _material, pyramid[i].down, _prop, 1);
			}
			RTHandle rTHandle = pyramid[--i].down;
			for (i--; i >= 1; i--)
			{
				(RTHandle, RTHandle) tuple = pyramid[i];
				_prop.SetTexture(ShaderIDs.InputTexture, rTHandle);
				_prop.SetTexture(ShaderIDs.HighTexture, tuple.Item1);
				HDUtils.DrawFullScreen(cmd, _material, tuple.Item2, _prop, 2);
				rTHandle = tuple.Item2;
			}
			_prop.SetTexture(ShaderIDs.InputTexture, rTHandle);
			HDUtils.DrawFullScreen(cmd, _material, destRT, _prop, 3);
		}

		public override void Cleanup()
		{
			CoreUtils.Destroy(_material);
			foreach (StreakPyramid value in _pyramids.Values)
			{
				value.Release();
			}
		}
	}
}
