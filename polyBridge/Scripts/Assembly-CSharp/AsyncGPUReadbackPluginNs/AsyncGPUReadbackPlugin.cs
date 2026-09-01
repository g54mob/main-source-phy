using UnityEngine;

namespace AsyncGPUReadbackPluginNs
{
	public class AsyncGPUReadbackPlugin
	{
		public static AsyncGPUReadbackPluginRequest Request(Texture src)
		{
			return new AsyncGPUReadbackPluginRequest(src);
		}
	}
}
