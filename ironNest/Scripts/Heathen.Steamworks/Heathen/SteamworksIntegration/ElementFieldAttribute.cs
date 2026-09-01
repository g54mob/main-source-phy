using System;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[AttributeUsage(AttributeTargets.Field)]
	public class ElementFieldAttribute : PropertyAttribute, IModularField
	{
		public string Header { get; }

		public int Priority { get; }

		public bool Synchronised => false;

		public ElementFieldAttribute(string header = null, int priority = 0)
		{
		}
	}
}
