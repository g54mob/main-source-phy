using System.Collections.Generic;
using UnityEngine;

namespace JigglePhysics
{
	[CreateAssetMenu(fileName = "JiggleSettingsBlend", menuName = "JigglePhysics/Blend Settings", order = 1)]
	public class JiggleSettingsBlend : JiggleSettingsBase
	{
		[Tooltip("The list of jiggle settings to blend between.")]
		public List<JiggleSettings> blendSettings;

		[Range(0f, 1f)]
		[Tooltip("A value from 0 to 1 that linearly blends between all of the blendSettings.")]
		public float normalizedBlend;

		public override float GetParameter(JiggleSettingParameter parameter)
		{
			float num = Mathf.Clamp01(normalizedBlend);
			int num2 = Mathf.FloorToInt(num * (float)blendSettings.Count);
			int value = Mathf.FloorToInt(num * (float)blendSettings.Count) + 1;
			return Mathf.Lerp(blendSettings[Mathf.Clamp(num2, 0, blendSettings.Count - 1)].GetParameter(parameter), blendSettings[Mathf.Clamp(value, 0, blendSettings.Count - 1)].GetParameter(parameter), Mathf.Clamp01(num * (float)blendSettings.Count - (float)num2));
		}

		public override float GetRadius(float normalizedIndex)
		{
			float num = Mathf.Clamp01(normalizedBlend);
			int num2 = Mathf.FloorToInt(num * (float)blendSettings.Count);
			int value = Mathf.FloorToInt(num * (float)blendSettings.Count) + 1;
			return Mathf.Lerp(blendSettings[Mathf.Clamp(num2, 0, blendSettings.Count - 1)].GetRadius(normalizedIndex), blendSettings[Mathf.Clamp(value, 0, blendSettings.Count - 1)].GetRadius(normalizedIndex), Mathf.Clamp01(num * (float)blendSettings.Count - (float)num2));
		}
	}
}
