using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace TND.Upscaling.Framework.URP
{
	[RequireComponent(typeof(Camera), typeof(UniversalAdditionalCameraData))]
	public abstract class UpscalerController_URP : UpscalerController
	{
		protected Camera _camera;

		protected UniversalAdditionalCameraData _additionalCameraData;

		protected UniversalRenderPipelineAsset _renderPipelineAsset;

		protected int _historyResetFrame;

		private readonly List<UpscalerContext> _upscalerContexts;

		private Vector2Int _displaySize;

		private Vector2Int _maxRenderSize;

		private Vector2Int _scaledRenderSize;

		private bool _isSinglePassXR;

		private int _viewCount;

		private Vector2Int _prevDisplaySize;

		private UpscalerQuality _prevQualityMode;

		private bool _prevHDR;

		private bool _prevSinglePassXR;

		private int _prevViewCount;

		protected override Vector2Int DisplaySize
		{
			protected internal get
			{
				return default(Vector2Int);
			}
		}

		protected override Vector2Int MaxRenderSize
		{
			protected internal get
			{
				return default(Vector2Int);
			}
		}

		protected override Vector2Int ScaledRenderSize
		{
			protected internal get
			{
				return default(Vector2Int);
			}
		}

		internal bool XREnabled => false;

		internal bool IsSinglePassXR => false;

		internal IUpscaler ActiveUpscaler => null;

		internal IUpscalerPlugin ActiveUpscalerPlugin => null;

		protected override bool ResetHistory
		{
			protected internal get
			{
				return false;
			}
		}

		private bool EnableHDR => false;

		protected override void Awake()
		{
		}

		protected override void OnEnable()
		{
		}

		protected override void OnDisable()
		{
		}

		protected override void Update()
		{
		}

		protected override void LateUpdate()
		{
		}

		internal UpscalerContext GetUpscalerContext(int viewIndex = 0)
		{
			return null;
		}

		private Vector2Int CalculateDisplaySize(out int viewCount, out bool isSinglePassXR)
		{
			viewCount = default(int);
			isSinglePassXR = default(bool);
			return default(Vector2Int);
		}

		private Vector2Int CalculateMaxRenderSize()
		{
			return default(Vector2Int);
		}

		private void UpdateScaledRenderSize()
		{
		}

		private static bool RendererFeatureExists()
		{
			return false;
		}
	}
}
