using System;
using UnityEngine;

namespace TFBGames
{
	[Serializable]
	public class AudioPathData
	{
		public string Category;

		public string Effect;

		public float EffectPitch = 1f;

		public float EffectVolume = 1f;

		public AudioPathData(string category, string effect, float effectPitch = 1f, float effectVolume = 1f)
		{
			Category = category;
			Effect = effect;
			EffectPitch = effectPitch;
			EffectVolume = effectVolume;
		}

		public static bool ValidateAndAssignPathData(string stringRef, ref AudioPathData result, UnityEngine.Object context = null)
		{
			if (string.IsNullOrEmpty(stringRef))
			{
				return true;
			}
			return GetAudioPathComponents(stringRef, ref result, context);
		}

		private static bool GetAudioPathComponents(string path, ref AudioPathData audioPathData, UnityEngine.Object context = null)
		{
			string[] array = path.Split('/');
			if (array.Length != 2)
			{
				return false;
			}
			audioPathData = new AudioPathData(array[0], array[1]);
			return true;
		}
	}
}
