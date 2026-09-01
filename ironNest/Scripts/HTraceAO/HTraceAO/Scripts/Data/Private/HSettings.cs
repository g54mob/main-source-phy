using HTraceAO.Scripts.Data.Public;
using UnityEngine;

namespace HTraceAO.Scripts.Data.Private
{
	internal static class HSettings
	{
		[SerializeField]
		internal static GeneralSettings GeneralSettings;

		[SerializeField]
		internal static SSAOSettings SSAOSettings;

		[SerializeField]
		internal static GTAOSettings GTAOSettings;

		[SerializeField]
		internal static RTAOSettings RTAOSettings;

		[SerializeField]
		internal static DebugSettings DebugSettings;
	}
}
