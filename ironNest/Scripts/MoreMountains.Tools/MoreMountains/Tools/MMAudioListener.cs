using UnityEngine;

namespace MoreMountains.Tools
{
	[RequireComponent(typeof(AudioListener))]
	public class MMAudioListener : MonoBehaviour
	{
		protected AudioListener _audioListener;

		protected AudioListener[] _otherListeners;

		protected virtual void OnEnable()
		{
		}
	}
}
