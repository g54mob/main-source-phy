using UnityEngine;

namespace FidelityFX.FSR3
{
	[CreateAssetMenu(fileName = "FSR3 Upscaler Assets", menuName = "FidelityFX/FSR3 Upscaler Assets", order = 1103)]
	public class Fsr3UpscalerAssets : ScriptableObject
	{
		public Fsr3UpscalerShaders shaders;
	}
}
