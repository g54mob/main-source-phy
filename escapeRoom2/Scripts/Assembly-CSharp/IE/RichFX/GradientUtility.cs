using UnityEngine;

namespace IE.RichFX
{
	public static class GradientUtility
	{
		private static readonly GradientColorKey[] _defaultColorKeys = new GradientColorKey[2]
		{
			new GradientColorKey(Color.blue, 0f),
			new GradientColorKey(Color.red, 1f)
		};

		private static readonly GradientAlphaKey[] _defaultAlphaKeys = new GradientAlphaKey[2]
		{
			new GradientAlphaKey(1f, 0f),
			new GradientAlphaKey(1f, 1f)
		};

		private static readonly int[] _colorKeyPropertyIDs = new int[8]
		{
			Shader.PropertyToID("_ColorKey0"),
			Shader.PropertyToID("_ColorKey1"),
			Shader.PropertyToID("_ColorKey2"),
			Shader.PropertyToID("_ColorKey3"),
			Shader.PropertyToID("_ColorKey4"),
			Shader.PropertyToID("_ColorKey5"),
			Shader.PropertyToID("_ColorKey6"),
			Shader.PropertyToID("_ColorKey7")
		};

		public static Gradient DefaultGradient
		{
			get
			{
				Gradient gradient = new Gradient();
				gradient.SetKeys(_defaultColorKeys, _defaultAlphaKeys);
				return gradient;
			}
		}

		public static int GetColorKeyPropertyID(int index)
		{
			return _colorKeyPropertyIDs[index];
		}

		public static void SetColorKeys(Material material, GradientColorKey[] colorKeys)
		{
			for (int i = 0; i < 8; i++)
			{
				material.SetVector(GetColorKeyPropertyID(i), colorKeys[Mathf.Min(i, colorKeys.Length - 1)].ToVector());
			}
		}
	}
}
