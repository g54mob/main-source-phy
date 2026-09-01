using HTraceAO.Scripts.Globals;
using HTraceAO.Scripts.Passes.URP;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace HTraceAO.Scripts.Infrastructure.URP
{
	[DisallowMultipleRendererFeature(null)]
	[ExecuteAlways]
	[HelpURL("https://ipgames.gitbook.io/htrace-ao")]
	public class HTraceAORendererFeature : ScreenSpaceAmbientOcclusion
	{
		private PrePassURP _prePass;

		private MotionVectorsURP _motionVectors;

		private SSAOPassURP _ssaoPass;

		private GTAOPassURP _gtaoPass;

		private RTAOPassURP _rtaoPass;

		private FinalPassURP _finalPass;

		private bool _initialized;

		private AmbientOcclusionMode _previousAmbientOcclusionMode;

		public override void Create()
		{
		}

		public override void SetupRenderPasses(ScriptableRenderer renderer, in RenderingData renderingData)
		{
		}

		public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
		{
		}

		private void SettingsBuild(HTraceAOVolume hTraceAOVolume)
		{
		}

		protected override void Dispose(bool disposing)
		{
		}
	}
}
