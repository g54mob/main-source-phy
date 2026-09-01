using UnityEngine;
using UnityEngine.Rendering;

namespace TND.Upscaling.Framework
{
	public class UpscalerContext
	{
		private UpscalerInitParams _initParams;

		private IUpscaler _activeUpscaler;

		private IUpscalerPlugin _activeUpscalerPlugin;

		private UpscalerController _upscalerController;

		private string _selectedUpscalerIdentifier;

		private ShaderRef _fallbackUpscaleShader;

		private Material _fallbackUpscaleMaterial;

		private ShaderRef _autoReactiveShader;

		private Material _autoReactiveMaterial;

		private ShaderRef _mergeReactiveShader;

		private Material _mergeReactiveMaterial;

		private GlobalKeyword _texArraysKeyword;

		private GlobalKeyword _srgbKeyword;

		internal static readonly string TexArraysKeyword;

		internal static readonly string SrgbKeyword;

		private static readonly int MainTexId;

		private static readonly int RenderSizeId;

		private static readonly int InvUpscaleSizeId;

		private static readonly int InvInputSizeId;

		private static readonly int JitterOffsetId;

		public IUpscaler ActiveUpscaler => null;

		public IUpscalerPlugin ActiveUpscalerPlugin => null;

		protected internal UpscalerController UpscalerController => null;

		internal Material AutoReactiveMaterial => null;

		internal Material MergeReactiveMaterial => null;

		public virtual bool Initialize(CommandBuffer cmd, in UpscalerInitParams initParams)
		{
			return false;
		}

		public virtual void Destroy(CommandBuffer cmd)
		{
		}

		public virtual void Execute(CommandBuffer cmd, ref UpscalerDispatchParams dispatchParams)
		{
		}

		private bool RestartRequired()
		{
			return false;
		}

		private void Restart(CommandBuffer cmd)
		{
		}

		private bool GetOpaqueOnlyRequired()
		{
			return false;
		}

		private void ExecuteFallbackUpscaler(CommandBuffer cmd, in UpscalerDispatchParams dispatchParams)
		{
		}
	}
}
