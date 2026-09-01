using FMOD.Studio;
using UnityEngine;
using UnityEngine.Serialization;

namespace FMODUnity
{
	[AddComponentMenu("FMOD Studio/FMOD Studio Global Parameter Trigger")]
	public class StudioGlobalParameterTrigger : EventHandler
	{
		[ParamRef]
		[FormerlySerializedAs("parameter")]
		public string Parameter;

		public EmitterGameEvent TriggerEvent;

		[FormerlySerializedAs("value")]
		public float Value;

		private PARAMETER_DESCRIPTION parameterDescription;

		public PARAMETER_DESCRIPTION ParameterDescription => default(PARAMETER_DESCRIPTION);

		protected override void HandleGameEvent(EmitterGameEvent gameEvent)
		{
		}

		public void TriggerParameters()
		{
		}
	}
}
