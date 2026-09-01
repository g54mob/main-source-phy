using UnityEngine;

namespace FMODUnity
{
	[AddComponentMenu("FMOD Studio/FMOD Studio Parameter Trigger")]
	public class StudioParameterTrigger : EventHandler
	{
		public EmitterRef[] Emitters;

		public EmitterGameEvent TriggerEvent;

		private void Awake()
		{
		}

		protected override void HandleGameEvent(EmitterGameEvent gameEvent)
		{
		}

		public void TriggerParameters()
		{
		}
	}
}
