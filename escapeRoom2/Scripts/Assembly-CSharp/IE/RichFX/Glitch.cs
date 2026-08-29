using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace IE.RichFX
{
	[Serializable]
	[VolumeComponentMenu("Rich FX/Screen Distortions/Glitch")]
	public sealed class Glitch : CustomPostProcessVolumeComponent, IPostProcessComponent
	{
		private static class ShaderIDs
		{
			internal static readonly int BlockSeed1 = Shader.PropertyToID("_BlockSeed1");

			internal static readonly int BlockSeed2 = Shader.PropertyToID("_BlockSeed2");

			internal static readonly int BlockStrength = Shader.PropertyToID("_BlockStrength");

			internal static readonly int BlockStride = Shader.PropertyToID("_BlockStride");

			internal static readonly int Drift = Shader.PropertyToID("_Drift");

			internal static readonly int InputTexture = Shader.PropertyToID("_InputTexture");

			internal static readonly int Jitter = Shader.PropertyToID("_Jitter");

			internal static readonly int Jump = Shader.PropertyToID("_Jump");

			internal static readonly int Seed = Shader.PropertyToID("_Seed");

			internal static readonly int Shake = Shader.PropertyToID("_Shake");
		}

		public ClampedFloatParameter block = new ClampedFloatParameter(0f, 0f, 1f);

		public ClampedFloatParameter drift = new ClampedFloatParameter(0f, 0f, 1f);

		public ClampedFloatParameter jitter = new ClampedFloatParameter(0f, 0f, 1f);

		public ClampedFloatParameter jump = new ClampedFloatParameter(0f, 0f, 1f);

		public ClampedFloatParameter shake = new ClampedFloatParameter(0f, 0f, 1f);

		private Material _material;

		private float _prevTime;

		private float _jumpTime;

		private float _blockTime;

		private int _blockSeed1 = 71;

		private int _blockSeed2 = 113;

		private int _blockStride = 1;

		public override CustomPostProcessInjectionPoint injectionPoint => CustomPostProcessInjectionPoint.AfterPostProcess;

		public bool IsActive()
		{
			if (_material != null)
			{
				if (!(block.value > 0f) && !(drift.value > 0f) && !(jitter.value > 0f) && !(jump.value > 0f))
				{
					return shake.value > 0f;
				}
				return true;
			}
			return false;
		}

		public override void Setup()
		{
			_material = CoreUtils.CreateEngineMaterial("Hidden/InanEvin/RichFX/Glitch");
		}

		public override void Render(CommandBuffer cmd, HDCamera camera, RTHandle srcRT, RTHandle destRT)
		{
			if (_material == null)
			{
				return;
			}
			float time = Time.time;
			float num = time - _prevTime;
			_jumpTime += num * jump.value * 11.3f;
			_prevTime = time;
			float value = block.value * block.value * block.value;
			_blockTime += num * 60f;
			if (_blockTime > 1f)
			{
				if (UnityEngine.Random.value < 0.09f)
				{
					_blockSeed1 += 251;
				}
				if (UnityEngine.Random.value < 0.29f)
				{
					_blockSeed2 += 373;
				}
				if (UnityEngine.Random.value < 0.25f)
				{
					_blockStride = UnityEngine.Random.Range(1, 32);
				}
				_blockTime = 0f;
			}
			Vector2 vector = new Vector2(time * 606.11f % (MathF.PI * 2f), drift.value * 0.04f);
			float value2 = jitter.value;
			Vector3 vector2 = new Vector3(Mathf.Max(0f, 1.001f - value2 * 1.2f), 0.002f + value2 * value2 * value2 * 0.05f);
			Vector2 vector3 = new Vector2(_jumpTime, jump.value);
			_material.SetInt(ShaderIDs.Seed, (int)(time * 10000f));
			_material.SetFloat(ShaderIDs.BlockStrength, value);
			_material.SetInt(ShaderIDs.BlockStride, _blockStride);
			_material.SetInt(ShaderIDs.BlockSeed1, _blockSeed1);
			_material.SetInt(ShaderIDs.BlockSeed2, _blockSeed2);
			_material.SetVector(ShaderIDs.Drift, vector);
			_material.SetVector(ShaderIDs.Jitter, vector2);
			_material.SetVector(ShaderIDs.Jump, vector3);
			_material.SetFloat(ShaderIDs.Shake, shake.value * 0.2f);
			_material.SetTexture(ShaderIDs.InputTexture, srcRT);
			int num2 = 0;
			if (drift.value > 0f || jitter.value > 0f || jump.value > 0f || shake.value > 0f)
			{
				num2++;
			}
			if (block.value > 0f)
			{
				num2 += 2;
			}
			HDUtils.DrawFullScreen(cmd, _material, destRT, null, num2);
		}

		public override void Cleanup()
		{
			CoreUtils.Destroy(_material);
		}
	}
}
