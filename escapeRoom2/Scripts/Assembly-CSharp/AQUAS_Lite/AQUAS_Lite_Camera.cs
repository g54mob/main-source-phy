using UnityEngine;

namespace AQUAS_Lite
{
	[AddComponentMenu("AQUAS Lite/AQUAS Lite Camera")]
	[RequireComponent(typeof(Camera))]
	public class AQUAS_Lite_Camera : MonoBehaviour
	{
		private void Start()
		{
			Set();
		}

		private void Set()
		{
			if (GetComponent<Camera>().depthTextureMode == DepthTextureMode.None)
			{
				GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
			}
		}
	}
}
