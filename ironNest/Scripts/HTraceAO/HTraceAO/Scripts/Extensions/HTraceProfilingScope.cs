using System;
using System.Runtime.InteropServices;
using UnityEngine.Rendering;

namespace HTraceAO.Scripts.Extensions
{
	[StructLayout((LayoutKind)0, Size = 1)]
	public struct HTraceProfilingScope : IDisposable
	{
		public HTraceProfilingScope(CommandBuffer cmd, ProfilingSamplerHTrace samplerHTrace)
		{
		}

		public void Dispose()
		{
		}
	}
}
