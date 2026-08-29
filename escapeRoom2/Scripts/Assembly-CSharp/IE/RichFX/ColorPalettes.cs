using System.Collections.Generic;
using UnityEngine;

namespace IE.RichFX
{
	public static class ColorPalettes
	{
		public static List<Vector4[]> colorPalettes = new List<Vector4[]>
		{
			new Vector4[4]
			{
				new Vector4(0.612f, 0.725f, 0.08f),
				new Vector4(0.549f, 0.667f, 0.07f),
				new Vector4(0.188f, 0.392f, 0.18f),
				new Vector4(0.063f, 0.247f, 0.06f)
			},
			new Vector4[4]
			{
				Vector4.zero,
				new Vector4(0f, 0.666f, 0.666f),
				new Vector4(0.666f, 0f, 0.666f),
				new Vector4(0.666f, 0.666f, 0.666f)
			},
			new Vector4[4]
			{
				Vector4.zero,
				new Vector4(0.333f, 1f, 1f),
				new Vector4(1f, 0.333f, 1f),
				Vector4.one
			},
			new Vector4[4]
			{
				Vector4.zero,
				new Vector4(0f, 0.666f, 0f),
				new Vector4(0f, 0.666f, 0f),
				new Vector4(0.666f, 0.333f, 0f)
			},
			new Vector4[4]
			{
				Vector4.zero,
				new Vector4(0.333f, 1f, 0.333f),
				new Vector4(1f, 0.333f, 0.333f),
				new Vector4(1f, 1f, 0.333f)
			},
			new Vector4[4]
			{
				Vector4.zero,
				new Vector4(0f, 0.666f, 0.666f),
				new Vector4(0.666f, 0f, 0f),
				new Vector4(0.666f, 0.666f, 0.666f)
			},
			new Vector4[4]
			{
				Vector4.zero,
				new Vector4(0.333f, 0.666f, 0.666f),
				new Vector4(1f, 0.333f, 0.333f),
				Vector4.one
			},
			new Vector4[4]
			{
				Vector4.zero,
				new Vector4(0.333f, 0.333f, 0.333f),
				new Vector4(0.666f, 0.666f, 0.666f),
				Vector4.one
			}
		};

		public static List<Vector4> colorPalettes2 = new List<Vector4>
		{
			new Vector4(0f, 0f, 0f),
			new Vector4(0f, 0f, 0.666f),
			new Vector4(0f, 0.666f, 0f),
			new Vector4(0f, 0.666f, 0.666f),
			new Vector4(0.666f, 0f, 0f),
			new Vector4(0.666f, 0f, 0f),
			new Vector4(0.666f, 0.333f, 0f),
			new Vector4(0.666f, 0.666f, 0.666f),
			new Vector4(0.333f, 0.333f, 0.333f),
			new Vector4(0.333f, 0.333f, 1f),
			new Vector4(0.333f, 1f, 0.333f),
			new Vector4(0.333f, 1f, 1f),
			new Vector4(1f, 0.333f, 0.333f),
			new Vector4(1f, 0.333f, 1f),
			new Vector4(1f, 1f, 0.333f),
			new Vector4(1f, 1f, 1f)
		};
	}
}
