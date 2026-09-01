using UnityEngine;

namespace TND.Upscaling.Framework
{
	public class ShaderRef
	{
		private readonly string _shaderResourceName;

		private Shader _shader;

		private int _refCount;

		internal ShaderRef(string shaderResourceName)
		{
		}

		internal Shader Acquire()
		{
			return null;
		}

		internal void Release()
		{
		}
	}
}
