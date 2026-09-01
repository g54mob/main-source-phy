using System;
using Steamworks;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public struct PersonaStateChange
	{
		public PersonaStateChange_t Data;

		public readonly CSteamID SubjectId => default(CSteamID);

		public readonly EPersonaChange Flags => default(EPersonaChange);

		public static implicit operator PersonaStateChange(PersonaStateChange_t native)
		{
			return default(PersonaStateChange);
		}

		public static implicit operator PersonaStateChange_t(PersonaStateChange heathen)
		{
			return default(PersonaStateChange_t);
		}
	}
}
