using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine.Profiling;
using UnityEngine.Rendering;

namespace HTraceAO.Scripts.Extensions
{
	internal class ProfilingSamplerHTrace<TEnum> : ProfilingSamplerHTrace
	{
		internal static Dictionary<TEnum, ProfilingSamplerHTrace<TEnum>> samples;

		static ProfilingSamplerHTrace()
		{
		}

		public ProfilingSamplerHTrace(string name)
			: base(null, null, 0)
		{
		}
	}
	[IgnoredByDeepProfiler]
	public class ProfilingSamplerHTrace
	{
		private Recorder m_Recorder;

		private Recorder m_InlineRecorder;

		internal CustomSampler sampler { get; private set; }

		internal CustomSampler inlineSampler { get; private set; }

		public string name { get; private set; }

		public string parentName { get; private set; }

		public int order { get; private set; }

		public bool enableRecording
		{
			set
			{
			}
		}

		public float gpuElapsedTime => 0f;

		public int gpuSampleCount => 0;

		public float cpuElapsedTime => 0f;

		public int cpuSampleCount => 0;

		public float inlineCpuElapsedTime => 0f;

		public int inlineCpuSampleCount => 0;

		public static ProfilingSamplerHTrace Get<TEnum>(TEnum marker)
		{
			return null;
		}

		public ProfilingSamplerHTrace(string name, string parentName = null, int order = -1)
		{
		}

		public void Begin(CommandBuffer cmd)
		{
		}

		public void End(CommandBuffer cmd)
		{
		}

		internal bool IsValid()
		{
			return false;
		}

		private ProfilingSamplerHTrace()
		{
		}
	}
}
