using UnityEngine;

namespace Shapes
{
	internal static class TexturePlacement
	{
		private static readonly Rect fitUvs;

		internal static (Rect, Rect) Fit(Texture texture, Rect rect, TextureFillMode mode)
		{
			return default((Rect, Rect));
		}

		internal static (Rect, Rect) Size(Texture texture, Vector2 c, float size, TextureSizeMode mode)
		{
			return default((Rect, Rect));
		}

		private static (Rect, Rect) FitWidth(Vector2 c, float w, float aspect)
		{
			return default((Rect, Rect));
		}

		private static (Rect, Rect) FitHeight(Vector2 c, float h, float aspect)
		{
			return default((Rect, Rect));
		}

		private static (Rect, Rect) FitRadius(Texture tex, Vector2 c, float r)
		{
			return default((Rect, Rect));
		}

		private static (Rect, Rect) SimpleRect(Vector2 c, float w, float h)
		{
			return default((Rect, Rect));
		}

		private static Rect RectCnt(float cx, float cy, float w, float h)
		{
			return default(Rect);
		}

		private static Rect RectCnt(Vector2 c, float w, float h)
		{
			return default(Rect);
		}

		private static (Rect, Rect) StretchToFill(Rect rect)
		{
			return default((Rect, Rect));
		}

		private static (Rect, Rect) ScaleToFit(Texture texture, Rect rect)
		{
			return default((Rect, Rect));
		}

		private static (Rect, Rect) ScaleAndCropToFill(Texture texture, Rect rect)
		{
			return default((Rect, Rect));
		}

		private static (Rect, Rect) TexelSized(Texture texture, Vector2 center, float pixelsPerMeter)
		{
			return default((Rect, Rect));
		}
	}
}
