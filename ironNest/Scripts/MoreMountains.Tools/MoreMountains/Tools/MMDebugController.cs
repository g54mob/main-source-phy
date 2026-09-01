using UnityEngine;

namespace MoreMountains.Tools
{
	[AddComponentMenu("More Mountains/Tools/Utilities/MMDebugController")]
	public class MMDebugController : MonoBehaviour
	{
		public bool DebugLogsEnabled;

		public bool DebugDrawEnabled;

		protected virtual void Awake()
		{
		}
	}
}
