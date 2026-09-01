using System;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[AttributeUsage(AttributeTargets.Field)]
	public class SettingsFieldAttribute : PropertyAttribute, IModularField
	{
		public int Priority { get; }

		public bool Synchronised { get; }

		public string Header { get; }

		public SettingsFieldAttribute(int priority = 0, bool synchronised = false, string header = null)
		{
		}
	}
}
