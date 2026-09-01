using UnityEngine;

namespace MoreMountains.Tools
{
	[AddComponentMenu("More Mountains/Tools/Activation/MMConditionalActivation")]
	public class MMConditionalActivation : MonoBehaviour
	{
		public MonoBehaviour[] EnableThese;

		public MonoBehaviour[] AfterTheseAreAllDisabled;

		protected bool _enabled;

		protected virtual void Update()
		{
		}
	}
}
