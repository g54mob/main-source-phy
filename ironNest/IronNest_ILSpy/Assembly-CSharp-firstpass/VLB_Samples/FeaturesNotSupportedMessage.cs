using UnityEngine;
using VLB;

namespace VLB_Samples;

public class FeaturesNotSupportedMessage : MonoBehaviour
{
	private void Start()
	{
		if (!Noise3D.isSupported)
		{
			string isNotSupportedString = Noise3D.isNotSupportedString;
			Debug.LogWarning(isNotSupportedString);
		}
	}
}
