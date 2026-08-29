using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace IE.RichFX
{
	[Serializable]
	[VolumeComponentMenu("Rich FX/Screen Distortions/Slice")]
	public sealed class Slice : CustomPostProcessVolumeComponent, IPostProcessComponent
	{
		private static class ShaderIDs
		{
			internal static readonly int Direction = Shader.PropertyToID("_Direction");

			internal static readonly int Displacement = Shader.PropertyToID("_Displacement");

			internal static readonly int InputTexture = Shader.PropertyToID("_InputTexture");

			internal static readonly int Rows = Shader.PropertyToID("_Rows");

			internal static readonly int Seed = Shader.PropertyToID("_Seed");
		}

		public FloatParameter rowCount = new FloatParameter(30f);

		public ClampedFloatParameter angle = new ClampedFloatParameter(0f, -90f, 90f);

		public ClampedFloatParameter displacement = new ClampedFloatParameter(0f, -1f, 1f);

		public IntParameter randomSeed = new IntParameter(0);

		private Material _material;

		public override CustomPostProcessInjectionPoint injectionPoint => CustomPostProcessInjectionPoint.AfterPostProcess;

		public bool IsActive()
		{
			if (_material != null)
			{
				return displacement.value != 0f;
			}
			return false;
		}

		public override void Setup()
		{
			_material = CoreUtils.CreateEngineMaterial("Hidden/InanEvin/RichFX/Slice");
		}

		public override void Render(CommandBuffer cmd, HDCamera camera, RTHandle srcRT, RTHandle destRT)
		{
			float f = angle.value * (MathF.PI / 180f);
			Vector2 vector = new Vector2(Mathf.Cos(f), Mathf.Sin(f));
			uint value = (uint)randomSeed.value;
			value = (value << 16) | (value >> 16);
			_material.SetVector(ShaderIDs.Direction, vector);
			_material.SetFloat(ShaderIDs.Displacement, displacement.value);
			_material.SetTexture(ShaderIDs.InputTexture, srcRT);
			_material.SetFloat(ShaderIDs.Rows, rowCount.value);
			_material.SetInt(ShaderIDs.Seed, (int)value);
			HDUtils.DrawFullScreen(cmd, _material, destRT);
		}

		public override void Cleanup()
		{
			CoreUtils.Destroy(_material);
		}
	}
}
