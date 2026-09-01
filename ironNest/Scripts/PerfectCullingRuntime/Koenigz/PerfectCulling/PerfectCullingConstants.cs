using System;
using System.Collections.Generic;
using UnityEngine;

namespace Koenigz.PerfectCulling
{
	public static class PerfectCullingConstants
	{
		public const int MaxRenderers = 65535;

		public const int SampleBatchCount = 2048;

		public const int CamBakeLayer = 30;

		public const PerfectCullingRenderToggleMode ToggleRenderMode = PerfectCullingRenderToggleMode.ToggleForceRenderingOff;

		public const bool SafetyChecks = true;

		public static readonly HashSet<Type> SupportedRendererTypes;

		public static Color ClearColor;

		public static bool AllowSceneReload;

		public static readonly string MultiSceneTempPath;

		public const float VolumeInsideAlpha = 0.05f;

		public const int MaxNonEmptyCellSearchRange = 3;

		public const bool ReportInvalidRenderers = true;

		public const bool DisableConfirmationDialogs = false;

		public const bool StripEditorOnlyComponents = true;
	}
}
