using UnityEngine;

namespace BitCode.Profiles
{
	[CreateAssetMenu(menuName = "BitCode/Profiles/Default Capabilities Provider", fileName = "DefaultCapabilities")]
	public class DefaultCapabilityProvider : ScriptableObject
	{
		[SerializeField]
		protected PlatformCapabilitiesCollection platformCapabilities;

		public PlatformCapabilities GetDefaultsForPlatform(RuntimePlatform runtimePlatform)
		{
			return platformCapabilities.GetCapabilitiesForPlatform(runtimePlatform);
		}
	}
}
