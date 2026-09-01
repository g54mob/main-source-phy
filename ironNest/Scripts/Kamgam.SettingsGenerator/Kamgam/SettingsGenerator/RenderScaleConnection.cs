using System;
using UnityEngine.Rendering.Universal;

namespace Kamgam.SettingsGenerator
{
	public class RenderScaleConnection : Connection<float>
	{
		public bool ReapplyOnQualityChange;

		public float DefaultRenderScale;

		[NonSerialized]
		protected float scale;

		public UniversalRenderPipelineAsset QualityRenderAsset => null;

		public override float Get()
		{
			return 0f;
		}

		public override void Set(float scale)
		{
		}

		public override void OnQualityChanged(int qualityLevel)
		{
		}
	}
}
