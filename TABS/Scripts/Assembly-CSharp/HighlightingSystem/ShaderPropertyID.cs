using UnityEngine;
using UnityEngine.Internal;

namespace HighlightingSystem
{
	[ExcludeFromDocs]
	public class ShaderPropertyID
	{
		public static readonly int _MainTex = Shader.PropertyToID("_MainTex");

		public static readonly int _Cutoff = Shader.PropertyToID("_Cutoff");

		public static readonly int _HighlightingIntensity = Shader.PropertyToID("_HighlightingIntensity");

		public static readonly int _HighlightingCull = Shader.PropertyToID("_HighlightingCull");

		public static readonly int _HighlightingColor = Shader.PropertyToID("_HighlightingColor");

		public static readonly int _HighlightingBlurOffset = Shader.PropertyToID("_HighlightingBlurOffset");

		public static readonly int _HighlightingFillAlpha = Shader.PropertyToID("_HighlightingFillAlpha");

		public static readonly int _HighlightingBuffer = Shader.PropertyToID("_HighlightingBuffer");

		public static readonly int _HighlightingBlur1 = Shader.PropertyToID("_HighlightingBlur1");

		public static readonly int _HighlightingBlur2 = Shader.PropertyToID("_HighlightingBlur2");
	}
}
