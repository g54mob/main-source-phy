using UnityEngine;

namespace MoreMountains.Tools
{
	[ExecuteAlways]
	[AddComponentMenu("More Mountains/Tools/Particles/MMChangeFogColor")]
	public class MMChangeFogColor : MonoBehaviour
	{
		[MMInformation("Adds this class to a UnityStandardAssets.ImageEffects.GlobalFog to change its color", MMInformationAttribute.InformationType.Info, false)]
		public Color FogColor;

		protected virtual void SetupFogColor()
		{
		}

		protected virtual void Start()
		{
		}

		protected virtual void OnValidate()
		{
		}
	}
}
