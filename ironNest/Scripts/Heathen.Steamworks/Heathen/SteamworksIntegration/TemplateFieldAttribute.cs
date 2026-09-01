using System;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[AttributeUsage(AttributeTargets.Field)]
	public class TemplateFieldAttribute : PropertyAttribute, IModularField
	{
		public string Header { get; }

		public int Priority { get; }

		public bool Synchronised => false;

		public TemplateFieldAttribute(string header = null, int priority = 0)
		{
		}
	}
}
