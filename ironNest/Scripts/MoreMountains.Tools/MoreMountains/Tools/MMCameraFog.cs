using UnityEngine;

namespace MoreMountains.Tools
{
	[ExecuteAlways]
	public class MMCameraFog : MonoBehaviour
	{
		public FogSettings Settings;

		protected FogSettings _previousSettings;

		protected void Awake()
		{
		}

		protected virtual void OnPreRender()
		{
		}

		protected virtual void OnPostRender()
		{
		}
	}
}
