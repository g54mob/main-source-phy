using UnityEngine;
using UnityEngine.Rendering;

namespace FidelityFX.FSR3
{
	public class Fsr3UpscalerContext
	{
		private const int MaxQueuedFrames = 16;

		private Fsr3Upscaler.ContextDescription _contextDescription;

		private CommandBuffer _commandBuffer;

		private Fsr3UpscalerPass _prepareInputsPass;

		private Fsr3UpscalerPass _lumaPyramidPass;

		private Fsr3UpscalerPass _shadingChangePyramidPass;

		private Fsr3UpscalerPass _shadingChangePass;

		private Fsr3UpscalerPass _prepareReactivityPass;

		private Fsr3UpscalerPass _lumaInstabilityPass;

		private Fsr3UpscalerPass _accumulatePass;

		private Fsr3UpscalerPass _sharpenPass;

		private Fsr3UpscalerPass _generateReactivePass;

		private readonly Fsr3UpscalerResources _resources;

		private ComputeBuffer _upscalerConstantsBuffer;

		private readonly Fsr3Upscaler.UpscalerConstants[] _upscalerConstantsArray;

		private ComputeBuffer _spdConstantsBuffer;

		private readonly Fsr3Upscaler.SpdConstants[] _spdConstantsArray;

		private ComputeBuffer _rcasConstantsBuffer;

		private readonly Fsr3Upscaler.RcasConstants[] _rcasConstantsArray;

		private ComputeBuffer _generateReactiveConstantsBuffer;

		private readonly Fsr3Upscaler.GenerateReactiveConstants[] _generateReactiveConstantsArray;

		private ComputeBuffer _tcrAutogenerateConstantsBuffer;

		private readonly Fsr3Upscaler.GenerateReactiveConstants2[] _tcrAutogenerateConstantsArray;

		private bool _firstExecution;

		private int _resourceFrameIndex;

		private Vector2 _previousJitterOffset;

		private float _preExposure;

		private float _previousFramePreExposure;

		private static readonly Fsr3Upscaler.RcasConstants[] RcasConfigs;

		private ref Fsr3Upscaler.UpscalerConstants UpscalerConsts
		{
			get
			{
				throw null;
			}
		}

		private ref Fsr3Upscaler.SpdConstants SpdConsts
		{
			get
			{
				throw null;
			}
		}

		private ref Fsr3Upscaler.RcasConstants RcasConsts
		{
			get
			{
				throw null;
			}
		}

		private ref Fsr3Upscaler.GenerateReactiveConstants GenReactiveConsts
		{
			get
			{
				throw null;
			}
		}

		private ref Fsr3Upscaler.GenerateReactiveConstants2 TcrAutoGenConsts
		{
			get
			{
				throw null;
			}
		}

		public void Create(Fsr3Upscaler.ContextDescription contextDescription)
		{
		}

		private void CreatePasses()
		{
		}

		public void Destroy()
		{
		}

		public void Dispatch(Fsr3Upscaler.DispatchDescription dispatchParams)
		{
		}

		public void Dispatch(Fsr3Upscaler.DispatchDescription dispatchParams, CommandBuffer commandBuffer)
		{
		}

		public void GenerateReactiveMask(Fsr3Upscaler.GenerateReactiveDescription dispatchParams)
		{
		}

		public void GenerateReactiveMask(Fsr3Upscaler.GenerateReactiveDescription dispatchParams, CommandBuffer commandBuffer)
		{
		}

		private void SetupConstants(Fsr3Upscaler.DispatchDescription dispatchParams, bool resetAccumulation)
		{
		}

		private Vector4 SetupDeviceDepthToViewSpaceDepthParams(Fsr3Upscaler.DispatchDescription dispatchParams)
		{
			return default(Vector4);
		}

		private void SetupRcasConstants(Fsr3Upscaler.DispatchDescription dispatchParams)
		{
		}

		private void SetupSpdConstants(Fsr3Upscaler.DispatchDescription dispatchParams, out Vector2Int dispatchThreadGroupCount)
		{
			dispatchThreadGroupCount = default(Vector2Int);
		}

		private static void SpdSetup(RectInt rectInfo, out Vector2Int dispatchThreadGroupCount, out Vector2Int workGroupOffset, out Vector2Int numWorkGroupsAndMips, int mips = -1)
		{
			dispatchThreadGroupCount = default(Vector2Int);
			workGroupOffset = default(Vector2Int);
			numWorkGroupsAndMips = default(Vector2Int);
		}

		private void DebugCheckDispatch(Fsr3Upscaler.DispatchDescription dispatchParams)
		{
		}

		private static ComputeBuffer CreateConstantBuffer<TConstants>() where TConstants : struct
		{
			return null;
		}

		private static void DestroyConstantBuffer(ref ComputeBuffer bufferRef)
		{
		}

		private static void DestroyPass(ref Fsr3UpscalerPass pass)
		{
		}
	}
}
